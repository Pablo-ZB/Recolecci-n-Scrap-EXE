using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Collections;
using System.Reflection.Metadata;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Image;
using System.IO;


namespace RecoleccionScrap
{
    public partial class FormPrincipal : Form
    {
        private int employeeId;
        public FormPrincipal(int employeeId)
        {
            InitializeComponent();
            this.employeeId = employeeId;
            InitializeTableLayoutEvents();

            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        private bool recorridoIniciado = false;
        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            CargarTiposDeCable();

            BloquearInterfaz(true);

            btnRecorrido.Text = "Iniciar recorrido";
            btnRecorrido.BackColor = SystemColors.Control;

            comboB_Area.Items.Clear();
            comboB_Material.Items.Clear();

            string connectionString = ConfigurationManager.ConnectionStrings["DAWS"].ConnectionString;

            string queryAreas = "SELECT Linea FROM Scrap.Lineas";
            string queryMaterials = "SELECT Type FROM Scrap.Types";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmdAreas = new SqlCommand(queryAreas, conn))
                    {
                        using (SqlDataReader reader = cmdAreas.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboB_Area.Items.Add(reader["Linea"].ToString());
                            }
                        }
                    }

                    using (SqlCommand cmdMaterials = new SqlCommand(queryMaterials, conn))
                    {
                        using (SqlDataReader reader = cmdMaterials.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboB_Material.Items.Add(reader["Type"].ToString());
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las áreas: " + ex.Message);
                }
            }

            if (comboB_Area.Items.Count > 0)
                comboB_Area.SelectedIndex = 0;

            if (comboB_Material.Items.Count > 0)
                comboB_Material.SelectedIndex = 0;

            SetTableLayoutPanelEnabled(false);

            dataGridView1.Columns.Add("KicPno", "KicPno");
            dataGridView1.Columns.Add("Maquina", "Máquina/Area");
            dataGridView1.Columns.Add("Tipo", "Tipo");
            dataGridView1.Columns.Add("Peso", "Peso");
            dataGridView1.Columns.Add("Comentarios", "Comentarios");
            dataGridView1.Columns.Add("Linea", "Linea");
            dataGridView1.Columns.Add("Fecha", "Fecha");
            dataGridView1.Columns.Add("Code", "Code");
            dataGridView1.Columns.Add("Area", "Area");
            dataGridView1.Columns.Add("BoxNo", "BoxNo");
            dataGridView1.Columns.Add("Turno", "Turno");
            dataGridView1.Columns.Add("TipoCable", "TipoCable");

            dataGridView1.Columns["KicPno"].ReadOnly = true;
            dataGridView1.Columns["Maquina"].ReadOnly = true;
            dataGridView1.Columns["Tipo"].ReadOnly = true;
            dataGridView1.Columns["Peso"].ReadOnly = true;
            dataGridView1.Columns["Fecha"].ReadOnly = true;
            dataGridView1.Columns["Code"].Visible = false;
            dataGridView1.Columns["Area"].Visible = false;
            dataGridView1.Columns["BoxNo"].Visible = false;
            dataGridView1.Columns["Turno"].Visible = false;
            dataGridView1.Columns["Linea"].Visible = false;
            dataGridView1.Columns["TipoCable"].Visible = false;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

            this.MaximizeBox = false;

            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
            btnEliminar.Name = "Eliminar";
            btnEliminar.HeaderText = "Eliminar";
           
            btnEliminar.UseColumnTextForButtonValue = true; 
            dataGridView1.Columns.Add(btnEliminar);

            dataGridView1.Columns["Eliminar"].Width = 65;
            btnEliminar.Text = "Borrar";
        }

        private void CargarTiposDeCable()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DAWS"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM [Scrap].[Wire_Types]";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int rowIndex = 0;
                        while (reader.Read())
                        {
                            switch (rowIndex)
                            {
                                case 0:
                                    labelD.Text = reader["Symbol"].ToString();
                                    radioDelgado.Text = $"{reader["Type"]}\n {reader["medidas"]}";
                                    break;
                                case 1:
                                    labelM1.Text = reader["Symbol"].ToString();
                                    radioMediano1.Text = $"{reader["Type"]}\n {reader["medidas"]}";
                                    break;
                                case 2:
                                    labelM2.Text = reader["Symbol"].ToString();
                                    radioMediano2.Text = $"{reader["Type"]}\n {reader["medidas"]}";
                                    break;
                                case 3:
                                    labelG.Text = reader["Symbol"].ToString();
                                    radioGrueso.Text = $"{reader["Type"]}\n {reader["medidas"]}";
                                    break;
                            }
                            rowIndex++;
                        }
                    }
                }
            }
        }


        private void BloquearInterfaz(bool bloquear)
        {
            foreach (Control control in this.Controls)
            {
                if (control != btnRecorrido)
                {
                    control.Enabled = !bloquear;
                }
            }
        }

        private void SetTableLayoutPanelEnabled(bool isEnabled)
        {
            foreach (Control control in CablesPLayout.Controls)
            {
                control.Enabled = isEnabled;
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboB_Area_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboB_Maquina.Items.Clear();

            string connectionString = ConfigurationManager.ConnectionStrings["DAWS"].ConnectionString;

            string query = "SELECT Machine FROM Scrap.Machines WHERE Linea_id = (SELECT id FROM Scrap.Lineas WHERE Linea = @linea)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@linea", comboB_Area.SelectedItem.ToString());
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboB_Maquina.Items.Add(reader["Machine"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las máquinas: " + ex.Message);
                }
            }

            if (comboB_Maquina.Items.Count > 0)
            {
                comboB_Maquina.SelectedIndex = 0;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Eliminar"].Index)
            {
                if (e.RowIndex == dataGridView1.NewRowIndex)
                {
                    MessageBox.Show("No se puede eliminar la fila nueva.");
                    return;
                }

                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("No hay registros para eliminar.");
                    return;
                }

                var result = MessageBox.Show("¿Estás seguro de que deseas eliminar este registro?", "Confirmación", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string seleccionCable = "";

            if (radioDelgado.Checked)
                seleccionCable = "Delgado";
            else if (radioMediano1.Checked)
                seleccionCable = "Mediano 1";
            else if (radioMediano2.Checked)
                seleccionCable = "Mediano 2";
            else if (radioGrueso.Checked)
                seleccionCable = "Grueso";

            if (!string.IsNullOrWhiteSpace(tBPeso.Text))
            {
                if (decimal.TryParse(tBPeso.Text, out decimal peso))
                {
                    string materialSeleccionado = comboB_Material.SelectedItem?.ToString() ?? "";
                    string maquinaSeleccionada = comboB_Maquina.SelectedItem?.ToString() ?? "";
                    string areaSeleccionada = comboB_Area.SelectedItem?.ToString() ?? "";

                    if (materialSeleccionado == "Cable" && string.IsNullOrWhiteSpace(seleccionCable))
                    {
                        MessageBox.Show("Por favor, seleccione un tipo de cable.");
                        return;
                    }
                    labelPeso.Text = peso.ToString() + " Kg";

                    labelLoading.Text = "Cargando...";
                    labelLoading.Visible = true;
                    this.Enabled = false;

                    int rowIndex = dataGridView1.Rows.Add();
                    DataGridViewRow newRow = dataGridView1.Rows[rowIndex];
                    
                    if (materialSeleccionado == "Cable")
                    {
                        materialSeleccionado += " " + seleccionCable;
                    }

                    newRow.Cells["Maquina"].Value = maquinaSeleccionada;
                    newRow.Cells["Tipo"].Value = materialSeleccionado;
                    newRow.Cells["Peso"].Value = peso + " Kg";
                    newRow.Cells["Fecha"].Value = DateTime.Now;
                    newRow.Cells["Linea"].Value = areaSeleccionada;
                    ActualizarCodeYArea(newRow);

                    if (areaSeleccionada == "Procesos")
                    {
                        CargarDatosProcesos(materialSeleccionado, maquinaSeleccionada, rowIndex, seleccionCable);
                    }
                    else
                    {
                        CargarDatos(materialSeleccionado, maquinaSeleccionada, rowIndex, seleccionCable);
                    }

                    labelLoading.Visible = false;
                    this.Enabled = true;

                    tBPeso.Clear();
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese un valor numérico válido para el peso.");
                }
            }
            else
            {
                MessageBox.Show("El campo de peso no puede estar vacío.");
            }

        }

        private void CargarDatos(string materialSeleccionado, string machineCode, int rowIndex, string seleccionCable)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HISS"].ConnectionString;
            string query = "";

            DateTime fechaInicio = ObtenerUltimaFecha(machineCode);

            if (fechaInicio == DateTime.MinValue)
            {
                fechaInicio = DateTime.Today.AddHours(7);
            }

            if (materialSeleccionado.Contains("Sello c/Terminal"))
            {

                query = @"
            SELECT DISTINCT PCA.Terminal1Real, PCA.Terminal2Real, PCA.Seal1Real, PCA.Seal2Real
            FROM [HIMES_DAWS].[dbo].[TMES_PROCESSRESULT_CIRCUITAUTO] PCA
            WHERE PCA.StartDate >= @FechaInicio
              AND PCA.StartDate < @FechaFinal
              AND PCA.MachineCode = @MachineCode
              AND (PCA.Terminal1Real IS NOT NULL OR PCA.Terminal2Real IS NOT NULL OR PCA.Seal1Real IS NOT NULL OR PCA.Seal2Real IS NOT NULL)";
            }
            else if (materialSeleccionado.Contains("Laina c/Terminal"))
            {
                query = @"
            SELECT DISTINCT PCA.Terminal1Real, PCA.Terminal2Real
            FROM [HIMES_DAWS].[dbo].[TMES_PROCESSRESULT_CIRCUITAUTO] PCA
            WHERE PCA.StartDate >= @FechaInicio
              AND PCA.StartDate < @FechaFinal
              AND PCA.MachineCode = @MachineCode
              AND (PCA.Terminal1Real IS NOT NULL OR PCA.Terminal2Real IS NOT NULL)";
            }
            else if (materialSeleccionado.Contains("Terminal"))
            {
                query = @"
            SELECT DISTINCT PCA.Terminal1Real, PCA.Terminal2Real
            FROM [HIMES_DAWS].[dbo].[TMES_PROCESSRESULT_CIRCUITAUTO] PCA
            WHERE PCA.StartDate >= @FechaInicio
              AND PCA.StartDate < @FechaFinal
              AND PCA.MachineCode = @MachineCode
              AND (PCA.Terminal1Real IS NOT NULL OR PCA.Terminal2Real IS NOT NULL)";
            }
            else if (materialSeleccionado.Contains("Sello"))
            {
                query = @"
            SELECT DISTINCT PCA.Seal1Real, PCA.Seal2Real
            FROM [HIMES_DAWS].[dbo].[TMES_PROCESSRESULT_CIRCUITAUTO] PCA
            WHERE PCA.StartDate >= @FechaInicio
              AND PCA.StartDate < @FechaFinal
              AND PCA.MachineCode = @MachineCode
              AND (PCA.Seal1Real IS NOT NULL OR PCA.Seal2Real IS NOT NULL)";
            }
            else if (materialSeleccionado.Contains("Cable"))
            {
                query = @"
            SELECT DISTINCT MM.KicPNo, CMD.Wire_Name
            FROM [HIMES_DAWS].[dbo].[TMES_PROCESSRESULT_CIRCUITAUTO] PCA
            LEFT JOIN [HIMES_DAWS].[dbo].[TMES_COMMONSETCODEDETAIL] CMD
                ON PCA.SetCode = CMD.SetCode
            LEFT JOIN [HIMES_DAWS].[dbo].[TMES_MATERIALMASTER] MM
                ON CMD.Wire_Name = MM.Description
            WHERE PCA.StartDate >= @FechaInicio 
              AND PCA.StartDate < @FechaFinal
              AND PCA.MachineCode = @MachineCode
              AND (
                (CASE 
                    WHEN SUBSTRING(CMD.Wire_Name, CHARINDEX('_', CMD.Wire_Name) + 1, CHARINDEX('_', CMD.Wire_Name, CHARINDEX('_', CMD.Wire_Name) + 1) - CHARINDEX('_', CMD.Wire_Name) - 1) BETWEEN '0.22' AND '0.35' THEN 'Delgado'
                    WHEN SUBSTRING(CMD.Wire_Name, CHARINDEX('_', CMD.Wire_Name) + 1, CHARINDEX('_', CMD.Wire_Name, CHARINDEX('_', CMD.Wire_Name) + 1) - CHARINDEX('_', CMD.Wire_Name) - 1) BETWEEN '0.5' AND '0.85' THEN 'Mediano 1'
                    WHEN SUBSTRING(CMD.Wire_Name, CHARINDEX('_', CMD.Wire_Name) + 1, CHARINDEX('_', CMD.Wire_Name, CHARINDEX('_', CMD.Wire_Name) + 1) - CHARINDEX('_', CMD.Wire_Name) - 1) BETWEEN '0.85' AND '2.5' THEN 'Mediano 2'
                    WHEN SUBSTRING(CMD.Wire_Name, CHARINDEX('_', CMD.Wire_Name) + 1, CHARINDEX('_', CMD.Wire_Name, CHARINDEX('_', CMD.Wire_Name) + 1) - CHARINDEX('_', CMD.Wire_Name) - 1) > '2.5' THEN 'Grueso'
                END) = @CalibreSeleccionado
              )";
            }
            else if (materialSeleccionado.Contains("Laina") || materialSeleccionado.Contains("Desforre"))
            {
                dataGridView1.Rows[rowIndex].Cells["KicPno"].Value = "";
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MachineCode", machineCode);
                        cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFinal", DateTime.Now);
                        cmd.Parameters.AddWithValue("@CalibreSeleccionado", seleccionCable);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            HashSet<string> resultados = new HashSet<string>();

                            while (reader.Read())
                            {
                                if (materialSeleccionado.Contains("Sello c/Terminal"))
                                {
                                    if (!reader.IsDBNull(reader.GetOrdinal("Terminal1Real")))
                                    {
                                        resultados.Add(reader["Terminal1Real"].ToString());
                                    }
                                    if (!reader.IsDBNull(reader.GetOrdinal("Terminal2Real")))
                                    {
                                        resultados.Add(reader["Terminal2Real"].ToString());
                                    }
                                    if (!reader.IsDBNull(reader.GetOrdinal("Seal1Real")))
                                    {
                                        resultados.Add(reader["Seal1Real"].ToString());
                                    }
                                    if (!reader.IsDBNull(reader.GetOrdinal("Seal2Real")))
                                    {
                                        resultados.Add(reader["Seal2Real"].ToString());
                                    }

                                }
                                else if (materialSeleccionado.Contains("Laina c/Terminal"))
                                {
                                    if (!reader.IsDBNull(reader.GetOrdinal("Terminal1Real")))
                                    {
                                        resultados.Add(reader["Terminal1Real"].ToString());
                                    }
                                    if (!reader.IsDBNull(reader.GetOrdinal("Terminal2Real")))
                                    {
                                        resultados.Add(reader["Terminal2Real"].ToString());
                                    }
                                }
                                else if (materialSeleccionado.Contains("Sello"))
                                {
                                    if (!reader.IsDBNull(reader.GetOrdinal("Seal1Real")))
                                    {
                                        resultados.Add(reader["Seal1Real"].ToString());
                                    }
                                    if (!reader.IsDBNull(reader.GetOrdinal("Seal2Real")))
                                    {
                                        resultados.Add(reader["Seal2Real"].ToString());
                                    }
                                }
                                else if (materialSeleccionado.Contains("Cable"))
                                {
                                    if (!reader.IsDBNull(reader.GetOrdinal("KicPNo")))
                                    {
                                        resultados.Add(reader["KicPNo"].ToString());
                                    }
                                }
                                else if (materialSeleccionado.Contains("Terminal"))
                                {
                                    if (!reader.IsDBNull(reader.GetOrdinal("Terminal1Real")))
                                    {
                                        resultados.Add(reader["Terminal1Real"].ToString());
                                    }
                                    if (!reader.IsDBNull(reader.GetOrdinal("Terminal2Real")))
                                    {
                                        resultados.Add(reader["Terminal2Real"].ToString());
                                    }
                                }
                                else if (materialSeleccionado.Contains("Laina"))
                                {
                                 
                                }
                                else if (materialSeleccionado.Contains("Desforre"))
                                {
                                 
                                }
                            }
                            dataGridView1.Rows[rowIndex].Cells["KicPno"].Value = string.Join(", ", resultados.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los datos: " + ex.Message);
                }
            }
        }

        private void CargarDatosProcesos(string material, string maquina, int rowIndex, string seleccionCable)
        {
            string connectionStringDAWS = ConfigurationManager.ConnectionStrings["DAWS"].ConnectionString;
            string connectionStringHISS = ConfigurationManager.ConnectionStrings["HISS"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionStringDAWS))
            {
                connection.Open();

                string areaSeleccionada = comboB_Maquina.SelectedItem.ToString();

                SqlCommand cmd = new SqlCommand("Scrap.ObtenerSetCodes", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Area", areaSeleccionada);

                List<string> setCodes = new List<string>();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        setCodes.Add(reader["SetCode"].ToString());
                    }
                }

                        using (SqlConnection connectionHISS = new SqlConnection(connectionStringHISS))
                {
                    connectionHISS.Open();

                    string setCodesConcatenados = string.Join("','", setCodes);
                    string query = "";

                    string materialSeleccionado = comboB_Material.SelectedItem.ToString();


                    if (materialSeleccionado == "Laina" || materialSeleccionado == "Desforre")
                    {
                        return;
                    }


                    if (materialSeleccionado == "Cable")
                    {
                        query = $@"
        SELECT DISTINCT MM.KicPNo, CMD.Wire_Name
        FROM [HIMES_DAWS].[dbo].[TMES_COMMONSETCODEDETAIL] CMD
        LEFT JOIN [HIMES_DAWS].[dbo].[TMES_MATERIALMASTER] MM
            ON CMD.Wire_Name = MM.Description
        WHERE CMD.SetCode IN ('{setCodesConcatenados}')
        AND (
            (CASE 
                WHEN SUBSTRING(CMD.Wire_Name, CHARINDEX('_', CMD.Wire_Name) + 1, CHARINDEX('_', CMD.Wire_Name, CHARINDEX('_', CMD.Wire_Name) + 1) - CHARINDEX('_', CMD.Wire_Name) - 1) BETWEEN '0.22' AND '0.35' THEN 'Delgado'
                WHEN SUBSTRING(CMD.Wire_Name, CHARINDEX('_', CMD.Wire_Name) + 1, CHARINDEX('_', CMD.Wire_Name, CHARINDEX('_', CMD.Wire_Name) + 1) - CHARINDEX('_', CMD.Wire_Name) - 1) BETWEEN '0.5' AND '0.85' THEN 'Mediano 1'
                WHEN SUBSTRING(CMD.Wire_Name, CHARINDEX('_', CMD.Wire_Name) + 1, CHARINDEX('_', CMD.Wire_Name, CHARINDEX('_', CMD.Wire_Name) + 1) - CHARINDEX('_', CMD.Wire_Name) - 1) BETWEEN '0.85' AND '2.5' THEN 'Mediano 2'
                WHEN SUBSTRING(CMD.Wire_Name, CHARINDEX('_', CMD.Wire_Name) + 1, CHARINDEX('_', CMD.Wire_Name, CHARINDEX('_', CMD.Wire_Name) + 1) - CHARINDEX('_', CMD.Wire_Name) - 1) > '2.5' THEN 'Grueso'
            END) = @CalibreSeleccionado
        )";

                    }
                    else if (materialSeleccionado == "Terminal")
                    {
                        query = $@"
            SELECT TOP (1000) 
                [Tml1], [Tml2]
            FROM [HIMES_DAWS].[dbo].[TMES_COMMONSETCODEDETAIL]
            WHERE SetCode IN ('{setCodesConcatenados}')
        ";
                    }
                    else if (materialSeleccionado == "Laina c/Terminal")
                    {
                         query = $@"
            SELECT TOP(1000) 
                [Tml1], [Tml2]
            FROM[HIMES_DAWS].[dbo].[TMES_COMMONSETCODEDETAIL]
            WHERE SetCode IN('{setCodesConcatenados}')
        ";
                    }
                    else if (materialSeleccionado == "Sello")
                    {
                        query = $@"
            SELECT TOP (1000) 
                [Seal1], [Seal2]
            FROM [HIMES_DAWS].[dbo].[TMES_COMMONSETCODEDETAIL]
            WHERE SetCode IN ('{setCodesConcatenados}')
        ";
                    }
                    else if (materialSeleccionado == "Sello c/Terminal")
                    {
                        query = $@"
            SELECT TOP (1000) 
                [Tml1], [Seal1], [Tml2], [Seal2]
            FROM [HIMES_DAWS].[dbo].[TMES_COMMONSETCODEDETAIL]
            WHERE SetCode IN ('{setCodesConcatenados}')
        ";
                    }


                    SqlCommand cmdHISS = new SqlCommand(query, connectionHISS);
                    cmdHISS.Parameters.AddWithValue("@CalibreSeleccionado", seleccionCable);
                    using (SqlDataReader reader = cmdHISS.ExecuteReader())
                    {
                        HashSet<string> resultados = new HashSet<string>();

                        while (reader.Read())
                        {
                            if (materialSeleccionado == "Cable")
                            {
                                string kicPno = reader["KicPno"].ToString();

                                if (!string.IsNullOrWhiteSpace(kicPno)) resultados.Add(kicPno);

                            }
                            else if (materialSeleccionado == "Terminal")
                            {
                                string tml1 = reader["Tml1"].ToString();
                                string tml2 = reader["Tml2"].ToString();

                                if (!string.IsNullOrWhiteSpace(tml1)) resultados.Add(tml1);
                                if (!string.IsNullOrWhiteSpace(tml2)) resultados.Add(tml2);

                            }
                            else if (materialSeleccionado == "Laina c/Terminal")
                            {
                                string tml1 = reader["Tml1"].ToString();

                                if (!string.IsNullOrWhiteSpace(tml1)) resultados.Add(tml1);
                            }
                            else if (materialSeleccionado == "Sello")
                            {
                                string seal1 = reader["Seal1"].ToString();
                                string seal2 = reader["Seal2"].ToString();

                                if (!string.IsNullOrWhiteSpace(seal1)) resultados.Add(seal1);
                                if (!string.IsNullOrWhiteSpace(seal2)) resultados.Add(seal2);
                            }
                            else if (materialSeleccionado == "Sello c/Terminal")
                            {
                                string tml1 = reader["Tml1"]?.ToString();
                                string tml2 = reader["Tml2"]?.ToString();
                                string seal1 = reader["Seal1"]?.ToString();
                                string seal2 = reader["Seal2"]?.ToString();

                                if (!string.IsNullOrWhiteSpace(tml1)) resultados.Add(tml1);
                                if (!string.IsNullOrWhiteSpace(tml2)) resultados.Add(tml2);
                                if (!string.IsNullOrWhiteSpace(seal1)) resultados.Add(seal1);
                                if (!string.IsNullOrWhiteSpace(seal2)) resultados.Add(seal2);
                            }

                            string concatenatedValues = string.Join(", ", resultados);

                            DataGridViewRow newRow = dataGridView1.Rows[rowIndex];
                            newRow.Cells["KicPno"].Value = concatenatedValues;

                        }
                    }
                }

            }

        }


        private DateTime ObtenerUltimaFecha(string machineCode)
        {
            DateTime ultimaFecha = DateTime.MinValue;
            string connectionString = ConfigurationManager.ConnectionStrings["DAWS"].ConnectionString;

            string query = @"
    SELECT MAX(Date) 
    FROM [ITApps].[Scrap].[Scrap_Data]
    WHERE Machine_id = (SELECT id FROM [Scrap].[Machines] WHERE Machine = @MachineCode)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MachineCode", machineCode);

                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        ultimaFecha = Convert.ToDateTime(result);
                    }
                }
            }

            return ultimaFecha;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboB_Material.SelectedItem != null && comboB_Material.SelectedItem.ToString() == "Cable")
            {
                SetTableLayoutPanelEnabled(true);
            }
            else
            {
                SetTableLayoutPanelEnabled(false);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnRecorrido_Click(object sender, EventArgs e)
        {

                if (!recorridoIniciado)
            {
                BloquearInterfaz(false);

                btnRecorrido.Text = "Finalizar recorrido";

                btnRecorrido.BackColor = Color.DarkSlateGray;
                btnRecorrido.ForeColor = SystemColors.Control;

                DateTime fechaInicioRecorrido = DateTime.Now;

                if (InsertarLogUsuario(1))
                {
                    recorridoIniciado = true;
                }
                else
                {
                    MessageBox.Show("Error al insertar el log de inicio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                if (dataGridView1.Rows.Count == 0 || dataGridView1.Rows.Cast<DataGridViewRow>().All(r => r.IsNewRow))
                {
                    MessageBox.Show("No se cargó ningún número de parte.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    btnRecorrido.Text = "Iniciar recorrido";
                    btnRecorrido.BackColor = SystemColors.Control;
                    btnRecorrido.ForeColor = SystemColors.ControlText;
                    recorridoIniciado = false;

                    return;
                }


                BloquearInterfaz(true);

                

                if (GenerarReportePdf())
                {
                    InsertarDatos();
                    MessageBox.Show("Datos insertados con éxito.");
                    dataGridView1.Rows.Clear();


                    if (InsertarLogUsuario(2))
                    {
                        btnRecorrido.Text = "Iniciar recorrido";
                        btnRecorrido.BackColor = SystemColors.Control;
                        btnRecorrido.ForeColor = SystemColors.ControlText;
                        recorridoIniciado = false;
                    }
                    else
                    {
                        MessageBox.Show("Error al insertar el log de finalización.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private bool InsertarLogUsuario(int actionId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DAWS"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO [ITApps].[Scrap].[Log_Usuarios] (Employee, Log_Date, Action_id) VALUES ((SELECT Employee FROM Scrap.Users WHERE id = @Employee), @LogDate, @ActionId)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Employee", this.employeeId);
                        command.Parameters.AddWithValue("@LogDate", DateTime.Now);
                        command.Parameters.AddWithValue("@ActionId", actionId);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }

            private bool GenerarReportePdf()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            string connectionString = ConfigurationManager.ConnectionStrings["DAWS"].ConnectionString;

            using (var package = new ExcelPackage())
            {
                var areaGroupedData = new Dictionary<string, List<DataGridViewRow>>();

                foreach (DataGridViewRow gridRow in dataGridView1.Rows)
                {
                    if (!gridRow.IsNewRow)
                    {
                        string area = gridRow.Cells["Area"].Value?.ToString() ?? "SinCodigo";

                        if (!areaGroupedData.ContainsKey(area))
                        {
                            areaGroupedData[area] = new List<DataGridViewRow>();
                        }
                        areaGroupedData[area].Add(gridRow);
                    }
                }

                foreach (var areaEntry in areaGroupedData)
                {
                    string areaCodigo = areaEntry.Key;
                    var rows = areaEntry.Value;

                    string hojaNombre = $"Formato de Scrap ({areaCodigo})";
                    int contador = 1;

                    while (package.Workbook.Worksheets.Any(sheet => sheet.Name == hojaNombre))
                    {
                        hojaNombre = $"Formato de Scrap ({areaCodigo})_{contador}";
                        contador++;
                    }

                    var hoja1 = package.Workbook.Worksheets.Add(hojaNombre);

                    string rutaProyecto = AppDomain.CurrentDomain.BaseDirectory;
                    string imagePath = Path.Combine(rutaProyecto, "wwwroot", "img", "DAWS_Color.png");

                    if (File.Exists(imagePath))
                    {
                        var picture = hoja1.Drawings.AddPicture("DAWSLogo", new FileInfo(imagePath));

                        picture.SetPosition(2, 10, 0, 10);
                        picture.SetSize(220, 55);
                    }
                    else
                    {
                        MessageBox.Show("Imagen no encontrada: " + imagePath);
                    }



                    hoja1.Cells["C3"].Value = "SCRAP";
                    hoja1.Cells["D3"].Value = "FORM";
                    hoja1.Cells["C4"].Value = "Formato de Scrap";
                    hoja1.Cells["E3"].Value = "Linea:";
                    hoja1.Cells["E4"].Value = "Turno:";
                    hoja1.Cells["G4"].Value = "Supervisor:";
                    hoja1.Cells["H4"].Value = "____________________";
                    hoja1.Cells["H5"].Value = "(Firma)";
                    hoja1.Cells["E5"].Value = "Area:";
                    hoja1.Cells["F3"].Value = areaCodigo.Contains("Cutting") ? "Corte" : "Procesos";

                    hoja1.Cells["C3"].Style.Font.Bold = true;
                    hoja1.Cells["D3"].Style.Font.Bold = true;
                    hoja1.Cells["C4"].Style.Font.Bold = true;
                    hoja1.Cells["E3"].Style.Font.Bold = true;
                    hoja1.Cells["E4"].Style.Font.Bold = true;
                    hoja1.Cells["G4"].Style.Font.Bold = true;
                    hoja1.Cells["E5"].Style.Font.Bold = true;

                    hoja1.Cells["E3"].Style.Font.Size = 12;
                    hoja1.Cells["E4"].Style.Font.Size = 12;
                    hoja1.Cells["G4"].Style.Font.Size = 12;
                    hoja1.Cells["E5"].Style.Font.Size = 12;

                    hoja1.Cells["C3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    hoja1.Cells["E3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                    hoja1.Cells["E4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                    hoja1.Cells["G4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                    hoja1.Cells["H5"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    hoja1.Cells["E5"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                    hoja1.Cells["D2"].Value = "Formato de Procedimiento - Durango Automotive Wiring Systems";
                    hoja1.Cells["C7"].Value = "Responsible: Finanzas Departament";
                    hoja1.Cells["G7"].Value = "Nombre:";

                    hoja1.Cells["C7:F7"].Merge = true;
                    hoja1.Cells["D2:H2"].Merge = true;

                    hoja1.Cells["C7:F7"].Style.Font.Bold = true;
                    hoja1.Cells["D2:H2"].Style.Font.Italic = true;
                    hoja1.Cells["C7"].Style.Font.Bold = true;
                    hoja1.Cells["G7"].Style.Font.Bold = true;

                    hoja1.Cells["C7:F7"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    hoja1.Cells["D2:H2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    hoja1.Cells["G7"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

                    hoja1.Cells["C7:F7"].Style.Font.Size = 12;

                    hoja1.Cells[9, 1].Value = "Date";
                    hoja1.Cells[9, 2].Value = "Part Number or Description";
                    hoja1.Cells[9, 3].Value = "Quantity";
                    hoja1.Cells[9, 4].Value = "UOM";
                    hoja1.Cells[9, 5].Value = "Box No.";
                    hoja1.Cells[9, 6].Value = "Work Center";
                    hoja1.Cells[9, 7].Value = "Reason Code";
                    hoja1.Cells[9, 8].Value = "Remarks";

                    hoja1.Column(1).Width = 13; // A
                    hoja1.Column(2).Width = 23; // B
                    hoja1.Column(6).Width = 16; // F
                    hoja1.Column(7).Width = 14.33; // G
                    hoja1.Column(8).Width = 22.33; // H

                    hoja1.Row(3).Height = 20;
                    hoja1.Row(4).Height = 20;
                    hoja1.Row(8).Height = 7.20;

                    using (var range = hoja1.Cells[9, 1, 9, 8])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    }

                    hoja1.Cells["F4"].Value = "A";
                    hoja1.Cells["F5"].Value = "LeadPrep";
                    hoja1.Cells["H7"].Value = "____________________";

                    Dictionary<string, double> materialPesos = new Dictionary<string, double>();
                    Dictionary<string, (DateTime fecha, string boxNo, string area, string code)> infoAdicional = new Dictionary<string, (DateTime, string, string, string)>();
                    List<string> comentariosList = new List<string>();


                    foreach (var gridRow in rows)
                    {
                        string tipo = gridRow.Cells["Tipo"].Value?.ToString() ?? "";
                        string pesoStr = gridRow.Cells["Peso"].Value?.ToString()?.Replace(" Kg", "").Trim() ?? "0";
                        double peso = double.TryParse(pesoStr, out double parsedPeso) ? parsedPeso : 0;

                        if (!materialPesos.ContainsKey(tipo))
                        {
                            materialPesos[tipo] = peso;
                            DateTime fecha = Convert.ToDateTime(gridRow.Cells["Fecha"].Value);
                            string boxNo = gridRow.Cells["BoxNo"].Value?.ToString() ?? "";
                            string area = gridRow.Cells["Area"].Value?.ToString() ?? "";
                            string code = gridRow.Cells["Code"].Value?.ToString() ?? "";
                            string comentarios = gridRow.Cells["Comentarios"].Value?.ToString() ?? "";

                            if (!string.IsNullOrEmpty(comentarios))
                            {
                                comentariosList.Add(comentarios);
                            }

                            infoAdicional[tipo] = (fecha, boxNo, area, code);
                        }
                        else
                        {
                            materialPesos[tipo] += peso;
                        }
                    }



                    int row = 10;
                    foreach (var entry in materialPesos)
                    {
                        string tipo = entry.Key;
                        double pesoTotal = entry.Value;
                        var info = infoAdicional[tipo];

                        hoja1.Cells[row, 1].Value = info.fecha;
                        hoja1.Cells[row, 1].Style.Numberformat.Format = "dd/mm/yyyy";
                        hoja1.Cells[row, 2].Value = tipo;
                        hoja1.Cells[row, 3].Value = pesoTotal;
                        hoja1.Cells[row, 4].Value = "Kg";
                        hoja1.Cells[row, 5].Value = info.boxNo;
                        hoja1.Cells[row, 6].Value = info.area;
                        hoja1.Cells[row, 7].Value = info.code;
                        row++;
                    }

                    string comentariosUnificados = string.Join(" | ", comentariosList);
                    hoja1.Cells[10, 8, row - 1, 8].Merge = true;
                    hoja1.Cells[10, 8].Value = comentariosUnificados;

                    hoja1.Cells[10, 8].Style.WrapText = true;
                    hoja1.Cells[10, 8].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify;


                    using (var range = hoja1.Cells[9, 1, row - 1, 8])
                    {
                        range.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    }

                    hoja1.Cells[row + 2, 2].Value = "Recibió:";
                    hoja1.Cells[row + 2, 2].Style.Font.Bold = true;
                    hoja1.Cells[row + 2, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                    hoja1.Cells[row + 2, 3].Value = "____________________";

                    hoja1.Cells[row + 2, 8].Value = "8.3-02-B";
                    hoja1.Cells[row + 2, 8].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    hoja1.Cells[row + 2, 8].Style.Font.Bold = true;
                    hoja1.Cells[row + 2, 8].Style.Font.Size = 12;

                }

                using (var memoryStream = new MemoryStream())
                {
                    package.SaveAs(memoryStream);
                    memoryStream.Position = 0;

                    Spire.Xls.Workbook workbook = new Spire.Xls.Workbook();
                    workbook.LoadFromStream(memoryStream);

                    foreach (Spire.Xls.Worksheet sheet in workbook.Worksheets)
                    {
                        sheet.PageSetup.LeftMargin = 0.5;
                        sheet.PageSetup.RightMargin = 0.5;
                        sheet.PageSetup.TopMargin = 0.5;
                        sheet.PageSetup.BottomMargin = 0.5;

                        sheet.PageSetup.FitToPagesWide = 1;
                        sheet.PageSetup.FitToPagesTall = 0;
                    }

                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                        saveFileDialog.Title = "Guardar reporte como";
                        saveFileDialog.FileName = "reporte_scrap.pdf";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string pdfPath = saveFileDialog.FileName;
                            workbook.SaveToFile(pdfPath, Spire.Xls.FileFormat.PDF);
                            MessageBox.Show($"Reporte generado en PDF: {pdfPath}");
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("El guardado fue cancelado.");
                            return false;
                        }
                    }
                }
            }
        }


        private void ActualizarCodeYArea(DataGridViewRow row)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["DAWS"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string area = comboB_Area.SelectedItem.ToString();

                DateTime fecha = (DateTime)row.Cells["Fecha"].Value;
                string turno = DeterminarTurno(fecha);
                string linea = comboB_Maquina.SelectedItem.ToString();

                int codeAreaId = ObtenerCodeAreaId(turno, area, conn, linea);

                string query = "SELECT Code, Work_Area FROM Scrap.Area_Codes WHERE id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", codeAreaId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            row.Cells["Code"].Value = reader["Code"].ToString();
                            row.Cells["Area"].Value = reader["Work_Area"].ToString();
                            row.Cells["Turno"].Value = turno;
                        }
                    }
                }

                string tipo = row.Cells["Tipo"].Value?.ToString() ?? "";
                int typeId = ObtenerTypeId(tipo, conn);
                int? boxId = ObtenerBoxId(typeId, conn);

                int wireTypeId = ObtenerWireTypeId(tipo, conn);
                row.Cells["TipoCable"].Value = wireTypeId;

                if (boxId != null)
                {
                    string queryBoxNo = "SELECT Box_No FROM [Scrap].[Boxes] WHERE id = @BoxId";
                    using (SqlCommand cmdBox = new SqlCommand(queryBoxNo, conn))
                    {
                        cmdBox.Parameters.AddWithValue("@BoxId", boxId);

                        object boxNoResult = cmdBox.ExecuteScalar();
                        if (boxNoResult != null)
                        {
                            row.Cells["BoxNo"].Value = boxNoResult.ToString();
                        }
                    }
                }
                else
                {
                    row.Cells["BoxNo"].Value = "No BoxNo";
                }
            }
        }

        private void InsertarDatos()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DAWS"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string kicpno = row.Cells["KicPno"].Value?.ToString() ?? "";
                        string maquina = row.Cells["Maquina"].Value?.ToString() ?? "";
                        string tipo = row.Cells["Tipo"].Value?.ToString() ?? "";
                        string pesoStr = row.Cells["Peso"].Value?.ToString()?.Replace(" Kg", "").Trim() ?? "";
                        string comentarios = row.Cells["Comentarios"].Value?.ToString() ?? "";
                        DateTime fecha = (DateTime)row.Cells["Fecha"].Value;
                        string linea = row.Cells["Linea"].Value?.ToString() ?? "";
                        string TipoCable = row.Cells["TipoCable"].Value?.ToString() ?? "";

                        int.TryParse(TipoCable, out int TipoCableId);

                        if (!decimal.TryParse(pesoStr, out decimal peso))
                        {
                            MessageBox.Show("El valor del peso no es numérico.");
                            continue;
                        }

                        string turno = DeterminarTurno(fecha);

                        int codeAreaId = ObtenerCodeAreaId(turno, linea, conn, maquina);

                        int employeeId = this.employeeId;
                        int machineId = ObtenerMachineId(maquina, conn);
                        int lineaId = ObtenerLineaId(linea, conn);
                        int typeId = ObtenerTypeId(tipo, conn);
                        int wireTypeId = tipo.Contains("Cable") ? TipoCableId : 5;
                        int? boxId = ObtenerBoxId(typeId, conn);


                        decimal qSeal = 0;
                        decimal quantityMM = 0;

                        if (tipo.Contains("Laina c/Terminal"))
                        {
                            decimal pesoNew = peso * (1 - (ObtenerPorcentajeTara(tipo, conn) / 100));


                            decimal tara = ObtenerTara(tipo, conn);
                            quantityMM = pesoNew * tara;
                        }
                        else if (tipo.Contains("Sello c/Terminal"))
                        {
                            
                            decimal pesoNew = peso * (ObtenerPorcentajeTara(tipo, conn) / 100);
                            decimal pesoSeal = peso - pesoNew;
                            decimal sello = (ObtenerTara("Sello", conn));
                            qSeal = (ObtenerTara("Sello", conn) * pesoSeal);


                            decimal tara = ObtenerTara(tipo, conn);
                            quantityMM = pesoNew * tara;
                        }
                        else
                        {
                            decimal tara = ObtenerTara(tipo, conn);
                            quantityMM = peso * tara;
                        }
                     
                        

                        string query = @"
                    INSERT INTO [ITApps].[Scrap].[Scrap_Data] 
                    ([Date], [KicPno], [Quantity], [UOM], [Remarks], [Turn], [Type_id], [Box_id], [Plant_id], [Code_Area_id], [Employee_id], [Linea_id], [Machine_id], [Wire_Type_id], [Quantity_mm], [Q_Seal]) 
                    VALUES (@Date, @KicPno, @Quantity, @UOM, @Remarks, @Turn, @Type_id, @Box_id, @Plant_id, @Code_Area_id, @Employee_id, @Linea_id, @Machine_id, @Wire_Type_id, @Quantity_mm, @Q_Seal)";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Date", fecha);
                            cmd.Parameters.AddWithValue("@KicPno", kicpno);
                            cmd.Parameters.AddWithValue("@Quantity", peso);
                            cmd.Parameters.AddWithValue("@UOM", "Kg");
                            cmd.Parameters.AddWithValue("@Remarks", comentarios);
                            cmd.Parameters.AddWithValue("@Turn", turno);
                            cmd.Parameters.AddWithValue("@Type_id", typeId);
                            cmd.Parameters.AddWithValue("@Box_id", boxId);
                            cmd.Parameters.AddWithValue("@Plant_id", employeeId);
                            cmd.Parameters.AddWithValue("@Code_Area_id", codeAreaId);
                            cmd.Parameters.AddWithValue("@Employee_id", employeeId);
                            cmd.Parameters.AddWithValue("@Linea_id", lineaId);
                            cmd.Parameters.AddWithValue("@Machine_id", machineId);
                            cmd.Parameters.AddWithValue("@Wire_Type_id", wireTypeId);
                            cmd.Parameters.AddWithValue("@Quantity_mm", quantityMM);
                            cmd.Parameters.AddWithValue("@Q_Seal", qSeal);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private string DeterminarTurno(DateTime fecha)
        {
            TimeSpan hora = fecha.TimeOfDay;

            if (hora >= new TimeSpan(7, 1, 0) && hora <= new TimeSpan(16, 36, 0))
            {
                return "A";
            }
            else if (hora >= new TimeSpan(16, 36, 1) && hora <= new TimeSpan(23, 59, 59))
            {
                return "B";
            }
            else if (hora >= new TimeSpan(0, 0, 0) && hora <= new TimeSpan(1, 10, 0))
            {
                return "B";
            }
            else
            {
                return "C";
            }
        }

        private int ObtenerCodeAreaId(string turno, string areaSeleccionada, SqlConnection conn, string area)
        {
            if (areaSeleccionada == "Procesos")
            {
                    string query = "SELECT id FROM Scrap.Area_Codes WHERE Work_Area = @Area";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Area", area);
                
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        throw new Exception("No se encontró el código para el área seleccionada.");
                    }
                
            }
            
            switch (turno)
            {
                case "A":
                    return 1;
                case "B":
                    return 2;
                case "C":
                    return 3;
                default:
                    throw new ArgumentException("Turno no válido");
            }
        }


        private int ObtenerMachineId(string maquina, SqlConnection conn)
        {
            string query = "SELECT id FROM Scrap.Machines WHERE Machine = @Machine";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Machine", maquina);
                return (int)cmd.ExecuteScalar();
            }
        }

        private int ObtenerTypeId(string tipo, SqlConnection conn)
        {
            string query = "SELECT id FROM Scrap.Types WHERE Type = @Type";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Type", tipo.Contains("Cable") ? "Cable" : tipo);
                return (int)cmd.ExecuteScalar();
            }
        }
        private int ObtenerWireTypeId(string tipo, SqlConnection conn)
        {
            string seleccionCable = "";

            if (radioDelgado.Checked)
                seleccionCable = "Delgado";
            else if (radioMediano1.Checked)
                seleccionCable = "Mediano 1";
            else if (radioMediano2.Checked)
                seleccionCable = "Mediano 2";
            else if (radioGrueso.Checked)
                seleccionCable = "Grueso";
            else
                seleccionCable = "N/A";

            string query = "SELECT id FROM Scrap.Wire_Types WHERE Type = @calibre";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@calibre", seleccionCable);
                return (int)cmd.ExecuteScalar();
            }
        }

        private int ObtenerLineaId(string linea, SqlConnection conn)
        {
            int lineaId = 0;
            string query = "SELECT id FROM Scrap.Lineas WHERE linea = @linea";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@linea", linea);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    lineaId = Convert.ToInt32(result);
                }
            }
            return lineaId;

        }

        private int? ObtenerBoxId(int typeId, SqlConnection conn)
        {
            string query = @"
        SELECT TOP 1 id 
        FROM [Scrap].[Boxes]
        WHERE Type_id = @Type_id 
        AND Status = 'Active'
        AND Close_Date IS NULL
        ORDER BY Start_Date DESC";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Type_id", typeId);

                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int boxId))
                {
                    return boxId;
                }
                else
                {
                    return null;
                }
            }
        }


        private decimal ObtenerTara(string tipoMaterial, SqlConnection conn)
        {
            string material = "";

            if (tipoMaterial.Contains("Cable Delgado")) material = "WIRE D";
            else if (tipoMaterial.Contains("Cable Mediano 1")) material = "WIRE M-1";
            else if (tipoMaterial.Contains("Cable Mediano 2")) material = "WIRE M-2";
            else if (tipoMaterial.Contains("Cable Grueso")) material = "WIRE G";
            else if (tipoMaterial.Contains("Terminal")) material = "TML";
            else if (tipoMaterial.Contains("Sello")) material = "SEAL";
            else if (tipoMaterial.Contains("Laina") || tipoMaterial.Contains("Desforre"))
            {
                return 0;
            }
            

            string query = "SELECT TOP 1 Value FROM [Scrap].[Taras] WHERE Material = @Material ORDER BY Update_Date DESC";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Material", material);

                object result = cmd.ExecuteScalar();
                if (result != null && decimal.TryParse(result.ToString(), out decimal valor))
                {
                    return valor;
                }
                else
                {
                    throw new Exception("No se pudo obtener el valor de la tara para el material seleccionado.");
                }
            }
        }


        private decimal ObtenerPorcentajeTara(string material, SqlConnection conn)
        {
            string query = "SELECT Percentage FROM [Scrap].[Percentages] WHERE Material = @Material";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Material", material);

                object result = cmd.ExecuteScalar();
                if (result != null && decimal.TryParse(result.ToString(), out decimal porcentaje))
                {
                    return porcentaje;
                }
                else
                {
                    throw new Exception("No se pudo obtener el porcentaje para el material seleccionado.");
                }
            }
        }

        private int activeColumnIndex = -1;

        private void Column_MouseEnter(object sender, EventArgs e)
        {
            Control control = sender as Control;
            int columnIndex = CablesPLayout.GetColumn(control);

            if (columnIndex != activeColumnIndex)
            {
                for (int row = 0; row < CablesPLayout.RowCount; row++)
                {
                    Control cellControl = CablesPLayout.GetControlFromPosition(columnIndex, row);
                    if (cellControl != null)
                    {
                        cellControl.BackColor = Color.LightGray;
                    }
                }
            }
        }

        private void Column_MouseLeave(object sender, EventArgs e)
        {
            Control control = sender as Control;
            int columnIndex = CablesPLayout.GetColumn(control);

            if (columnIndex != activeColumnIndex)
            {
                for (int row = 0; row < CablesPLayout.RowCount; row++)
                {
                    Control cellControl = CablesPLayout.GetControlFromPosition(columnIndex, row);
                    if (cellControl != null)
                    {
                        cellControl.BackColor = Color.Transparent;
                    }
                }
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.RadioButton radioButton = sender as System.Windows.Forms.RadioButton;

            if (radioButton.Checked)
            {
                int columnIndex = CablesPLayout.GetColumn(radioButton);

                RestoreColumnBackground(activeColumnIndex);

                activeColumnIndex = columnIndex;

                for (int row = 0; row < CablesPLayout.RowCount; row++)
                {
                    Control cellControl = CablesPLayout.GetControlFromPosition(columnIndex, row);
                    if (cellControl != null)
                    {
                        cellControl.BackColor = Color.LightGray;
                    }
                }
            }
        }

        private void RestoreColumnBackground(int columnIndex)
        {
            if (columnIndex < 0) return;

            for (int row = 0; row < CablesPLayout.RowCount; row++)
            {
                Control cellControl = CablesPLayout.GetControlFromPosition(columnIndex, row);
                if (cellControl != null)
                {
                    cellControl.BackColor = Color.Transparent;
                }
            }
        }

        private void Column_Click(object sender, EventArgs e)
        {
            Control control = sender as Control;
            int columnIndex = CablesPLayout.GetColumn(control);

            for (int row = 0; row < CablesPLayout.RowCount; row++)
            {
                Control cellControl = CablesPLayout.GetControlFromPosition(columnIndex, row);
                if (cellControl is System.Windows.Forms.RadioButton radioButton)
                {
                    radioButton.Checked = true;
                    break;
                }
            }
        }
        private void InitializeTableLayoutEvents()
        {
            foreach (Control control in CablesPLayout.Controls)
            {
                control.MouseEnter += new EventHandler(Column_MouseEnter);
                control.MouseLeave += new EventHandler(Column_MouseLeave);

                control.Click += new EventHandler(Column_Click);

                if (control is System.Windows.Forms.RadioButton radioButton)
                {
                    radioButton.CheckedChanged += new EventHandler(RadioButton_CheckedChanged);
                }
            }
        }


        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Comentarios")
            {
                DataGridViewRow currentRow = dataGridView1.Rows[e.RowIndex];

                if (string.IsNullOrWhiteSpace(currentRow.Cells["KicPno"].Value?.ToString()) ||
                    string.IsNullOrWhiteSpace(currentRow.Cells["Maquina"].Value?.ToString()) ||
                    string.IsNullOrWhiteSpace(currentRow.Cells["Tipo"].Value?.ToString()) ||
                    string.IsNullOrWhiteSpace(currentRow.Cells["Peso"].Value?.ToString()) ||
                    string.IsNullOrWhiteSpace(currentRow.Cells["Fecha"].Value?.ToString()))
                {
                    e.Cancel = true;
                }
            }
        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void radioDelgado_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioGrueso_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void labelPeso_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
