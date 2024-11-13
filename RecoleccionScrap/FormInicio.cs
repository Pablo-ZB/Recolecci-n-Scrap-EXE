using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;

namespace RecoleccionScrap
{
    public partial class FormIniciar : Form
    {
        public FormIniciar()
        { 

            InitializeComponent();

            btnLogin.MouseEnter += Button1_MouseEnter;
            btnLogin.MouseLeave += Button1_MouseLeave;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

            this.MaximizeBox = false;

            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
        }


        private void button1_Click(object sender, EventArgs e)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["DAWS"].ConnectionString;

            string query = "SELECT id FROM Scrap.Users WHERE Employee = @usuario";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);

                      //  int count = Convert.ToInt32(cmd.ExecuteScalar());
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            int employeeId = Convert.ToInt32(result);
                            FormPrincipal formPrincipal = new FormPrincipal(employeeId);
                            formPrincipal.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Número de empleado no válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

       
            private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                btnLogin.PerformClick();
            }
        }

        private void Button1_MouseEnter(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.LightBlue;
            btnLogin.ForeColor = Color.White;
        }

        private void Button1_MouseLeave(object sender, EventArgs e)
        {
            btnLogin.BackColor = Color.White;
            btnLogin.ForeColor = Color.Black;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtContraseñá_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
