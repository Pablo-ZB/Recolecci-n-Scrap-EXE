namespace RecoleccionScrap
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            comboB_Area = new ComboBox();
            comboB_Maquina = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            btnPesar = new Button();
            comboB_Material = new ComboBox();
            dataGridView1 = new DataGridView();
            radioDelgado = new RadioButton();
            radioMediano1 = new RadioButton();
            radioMediano2 = new RadioButton();
            radioGrueso = new RadioButton();
            btnRecorrido = new Button();
            CablesPLayout = new TableLayoutPanel();
            labelM1 = new Label();
            labelD = new Label();
            labelM2 = new Label();
            labelG = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            labelPeso = new Label();
            panel3 = new Panel();
            pictureBox1 = new PictureBox();
            tBPeso = new TextBox();
            labelLoading = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            CablesPLayout.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // comboB_Area
            // 
            comboB_Area.FormattingEnabled = true;
            comboB_Area.Location = new Point(160, 32);
            comboB_Area.Name = "comboB_Area";
            comboB_Area.Size = new Size(151, 28);
            comboB_Area.TabIndex = 1;
            comboB_Area.SelectedIndexChanged += comboB_Area_SelectedIndexChanged;
            // 
            // comboB_Maquina
            // 
            comboB_Maquina.FormattingEnabled = true;
            comboB_Maquina.Location = new Point(317, 32);
            comboB_Maquina.Name = "comboB_Maquina";
            comboB_Maquina.Size = new Size(151, 28);
            comboB_Maquina.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(164, 9);
            label1.Name = "label1";
            label1.Size = new Size(40, 20);
            label1.TabIndex = 3;
            label1.Text = "Area";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(317, 9);
            label2.Name = "label2";
            label2.Size = new Size(67, 20);
            label2.TabIndex = 4;
            label2.Text = "Maquina";
            // 
            // btnPesar
            // 
            btnPesar.Location = new Point(1013, 120);
            btnPesar.Name = "btnPesar";
            btnPesar.Size = new Size(94, 29);
            btnPesar.TabIndex = 5;
            btnPesar.Text = "Pesar";
            btnPesar.UseVisualStyleBackColor = true;
            btnPesar.Click += button1_Click;
            // 
            // comboB_Material
            // 
            comboB_Material.FormattingEnabled = true;
            comboB_Material.Location = new Point(160, 67);
            comboB_Material.Name = "comboB_Material";
            comboB_Material.Size = new Size(151, 28);
            comboB_Material.TabIndex = 6;
            comboB_Material.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 157);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1214, 509);
            dataGridView1.TabIndex = 7;
            dataGridView1.CellBeginEdit += dataGridView1_CellBeginEdit;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick_1;
            // 
            // radioDelgado
            // 
            radioDelgado.Anchor = AnchorStyles.None;
            radioDelgado.AutoSize = true;
            radioDelgado.BackColor = SystemColors.Control;
            radioDelgado.CheckAlign = ContentAlignment.BottomCenter;
            radioDelgado.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            radioDelgado.Location = new Point(3, 74);
            radioDelgado.MinimumSize = new Size(105, 66);
            radioDelgado.Name = "radioDelgado";
            radioDelgado.Size = new Size(105, 66);
            radioDelgado.TabIndex = 8;
            radioDelgado.TabStop = true;
            radioDelgado.TextAlign = ContentAlignment.MiddleCenter;
            radioDelgado.UseVisualStyleBackColor = false;
            radioDelgado.CheckedChanged += radioDelgado_CheckedChanged;
            // 
            // radioMediano1
            // 
            radioMediano1.Anchor = AnchorStyles.None;
            radioMediano1.AutoSize = true;
            radioMediano1.CheckAlign = ContentAlignment.BottomCenter;
            radioMediano1.Location = new Point(114, 74);
            radioMediano1.MinimumSize = new Size(105, 66);
            radioMediano1.Name = "radioMediano1";
            radioMediano1.Size = new Size(105, 66);
            radioMediano1.TabIndex = 9;
            radioMediano1.TabStop = true;
            radioMediano1.TextAlign = ContentAlignment.MiddleCenter;
            radioMediano1.UseVisualStyleBackColor = true;
            // 
            // radioMediano2
            // 
            radioMediano2.Anchor = AnchorStyles.None;
            radioMediano2.AutoSize = true;
            radioMediano2.CheckAlign = ContentAlignment.BottomCenter;
            radioMediano2.Location = new Point(225, 74);
            radioMediano2.MinimumSize = new Size(105, 66);
            radioMediano2.Name = "radioMediano2";
            radioMediano2.Size = new Size(105, 66);
            radioMediano2.TabIndex = 10;
            radioMediano2.TabStop = true;
            radioMediano2.TextAlign = ContentAlignment.MiddleCenter;
            radioMediano2.UseVisualStyleBackColor = true;
            // 
            // radioGrueso
            // 
            radioGrueso.Anchor = AnchorStyles.None;
            radioGrueso.AutoSize = true;
            radioGrueso.CheckAlign = ContentAlignment.BottomCenter;
            radioGrueso.Location = new Point(337, 74);
            radioGrueso.MinimumSize = new Size(105, 66);
            radioGrueso.Name = "radioGrueso";
            radioGrueso.Size = new Size(105, 66);
            radioGrueso.TabIndex = 11;
            radioGrueso.TabStop = true;
            radioGrueso.TextAlign = ContentAlignment.MiddleCenter;
            radioGrueso.UseVisualStyleBackColor = true;
            radioGrueso.CheckedChanged += radioGrueso_CheckedChanged;
            // 
            // btnRecorrido
            // 
            btnRecorrido.Location = new Point(23, 33);
            btnRecorrido.Name = "btnRecorrido";
            btnRecorrido.Size = new Size(102, 52);
            btnRecorrido.TabIndex = 12;
            btnRecorrido.Text = "Iniciar Recorrido";
            btnRecorrido.UseVisualStyleBackColor = true;
            btnRecorrido.Click += btnRecorrido_Click;
            // 
            // CablesPLayout
            // 
            CablesPLayout.ColumnCount = 4;
            CablesPLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            CablesPLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            CablesPLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            CablesPLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            CablesPLayout.Controls.Add(labelM1, 1, 0);
            CablesPLayout.Controls.Add(labelD, 0, 0);
            CablesPLayout.Controls.Add(labelM2, 2, 0);
            CablesPLayout.Controls.Add(radioMediano2, 2, 1);
            CablesPLayout.Controls.Add(labelG, 3, 0);
            CablesPLayout.Controls.Add(radioMediano1, 1, 1);
            CablesPLayout.Controls.Add(radioDelgado, 0, 1);
            CablesPLayout.Controls.Add(radioGrueso, 3, 1);
            CablesPLayout.Location = new Point(500, 9);
            CablesPLayout.Name = "CablesPLayout";
            CablesPLayout.RowCount = 2;
            CablesPLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50.4854355F));
            CablesPLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 49.5145645F));
            CablesPLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            CablesPLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            CablesPLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            CablesPLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            CablesPLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            CablesPLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            CablesPLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            CablesPLayout.Size = new Size(446, 142);
            CablesPLayout.TabIndex = 13;
            CablesPLayout.Paint += tableLayoutPanel1_Paint;
            // 
            // labelM1
            // 
            labelM1.Anchor = AnchorStyles.None;
            labelM1.AutoSize = true;
            labelM1.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelM1.ForeColor = Color.SlateGray;
            labelM1.Location = new Point(114, 2);
            labelM1.MinimumSize = new Size(105, 66);
            labelM1.Name = "labelM1";
            labelM1.Size = new Size(105, 66);
            labelM1.TabIndex = 14;
            labelM1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelD
            // 
            labelD.Anchor = AnchorStyles.None;
            labelD.AutoSize = true;
            labelD.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelD.ForeColor = Color.SlateGray;
            labelD.Location = new Point(3, 2);
            labelD.MinimumSize = new Size(105, 66);
            labelD.Name = "labelD";
            labelD.Size = new Size(105, 66);
            labelD.TabIndex = 0;
            labelD.TextAlign = ContentAlignment.MiddleCenter;
            labelD.Click += label3_Click_1;
            // 
            // labelM2
            // 
            labelM2.Anchor = AnchorStyles.None;
            labelM2.AutoSize = true;
            labelM2.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelM2.ForeColor = Color.SlateGray;
            labelM2.Location = new Point(225, 2);
            labelM2.MinimumSize = new Size(105, 66);
            labelM2.Name = "labelM2";
            labelM2.Size = new Size(105, 66);
            labelM2.TabIndex = 15;
            labelM2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelG
            // 
            labelG.Anchor = AnchorStyles.None;
            labelG.AutoSize = true;
            labelG.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelG.ForeColor = Color.SlateGray;
            labelG.Location = new Point(337, 2);
            labelG.MinimumSize = new Size(105, 66);
            labelG.Name = "labelG";
            labelG.Size = new Size(105, 66);
            labelG.TabIndex = 16;
            labelG.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ActiveBorder;
            panel1.Location = new Point(493, 9);
            panel1.Name = "panel1";
            panel1.Size = new Size(1, 147);
            panel1.TabIndex = 14;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveBorder;
            panel2.Location = new Point(952, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(1, 147);
            panel2.TabIndex = 15;
            // 
            // labelPeso
            // 
            labelPeso.AutoSize = true;
            labelPeso.Font = new Font("Segoe UI", 22.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelPeso.ForeColor = SystemColors.ControlDark;
            labelPeso.Location = new Point(3, 3);
            labelPeso.MinimumSize = new Size(100, 50);
            labelPeso.Name = "labelPeso";
            labelPeso.Size = new Size(110, 50);
            labelPeso.TabIndex = 16;
            labelPeso.Text = "0.000";
            labelPeso.Click += labelPeso_Click;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.ControlLightLight;
            panel3.Controls.Add(pictureBox1);
            panel3.Controls.Add(labelPeso);
            panel3.Location = new Point(1013, 12);
            panel3.Name = "panel3";
            panel3.Size = new Size(216, 91);
            panel3.TabIndex = 17;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(164, 55);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(52, 36);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 18;
            pictureBox1.TabStop = false;
            // 
            // tBPeso
            // 
            tBPeso.Location = new Point(1122, 120);
            tBPeso.Name = "tBPeso";
            tBPeso.Size = new Size(107, 27);
            tBPeso.TabIndex = 18;
            // 
            // labelLoading
            // 
            labelLoading.AutoSize = true;
            labelLoading.Location = new Point(572, 356);
            labelLoading.Name = "labelLoading";
            labelLoading.Size = new Size(0, 20);
            labelLoading.TabIndex = 19;
            // 
            // FormPrincipal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1236, 674);
            Controls.Add(labelLoading);
            Controls.Add(tBPeso);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(CablesPLayout);
            Controls.Add(btnRecorrido);
            Controls.Add(dataGridView1);
            Controls.Add(comboB_Material);
            Controls.Add(btnPesar);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboB_Maquina);
            Controls.Add(comboB_Area);
            Controls.Add(panel3);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormPrincipal";
            Text = "Recolección de Scrap";
            Load += FormPrincipal_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            CablesPLayout.ResumeLayout(false);
            CablesPLayout.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox comboB_Area;
        private ComboBox comboB_Maquina;
        private Label label1;
        private Label label2;
        private Button btnPesar;
        private ComboBox comboB_Material;
        private DataGridView dataGridView1;
        private Label lblDelgado;
        private Label lblMediano1;
        private Label lblMediano2;
        private Label lblGrueso;
        private RadioButton radioDelgado;
        private RadioButton radioMediano1;
        private RadioButton radioMediano2;
        private RadioButton radioGrueso;
        private Button btnRecorrido;
        private TableLayoutPanel CablesPLayout;
        private Label labelM1;
        private Label labelD;
        private Label labelM2;
        private Label labelG;
        private Panel panel1;
        private Panel panel2;
        private Label labelPeso;
        private Panel panel3;
        private PictureBox pictureBox1;
        private TextBox tBPeso;
        private Label labelLoading;
    }
}