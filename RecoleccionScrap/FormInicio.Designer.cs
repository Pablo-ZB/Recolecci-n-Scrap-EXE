namespace RecoleccionScrap
{
    partial class FormIniciar
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIniciar));
            label1 = new Label();
            txtUsuario = new TextBox();
            btnLogin = new Button();
            LogoDaws = new PictureBox();
            label_Inicio = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)LogoDaws).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(361, 175);
            label1.Name = "label1";
            label1.Size = new Size(0, 20);
            label1.TabIndex = 1;
            label1.Click += label1_Click;
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(331, 214);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(125, 27);
            txtUsuario.TabIndex = 3;
            txtUsuario.TextAlign = HorizontalAlignment.Center;
            txtUsuario.TextChanged += textBox1_TextChanged;
            txtUsuario.KeyPress += txtUsuario_KeyPress;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(340, 279);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(107, 29);
            btnLogin.TabIndex = 0;
            btnLogin.Text = "Iniciar Sesión";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += button1_Click;
            // 
            // LogoDaws
            // 
            LogoDaws.Image = (Image)resources.GetObject("LogoDaws.Image");
            LogoDaws.Location = new Point(331, 29);
            LogoDaws.Name = "LogoDaws";
            LogoDaws.Size = new Size(125, 62);
            LogoDaws.SizeMode = PictureBoxSizeMode.Zoom;
            LogoDaws.TabIndex = 4;
            LogoDaws.TabStop = false;
            LogoDaws.Click += pictureBox1_Click;
            // 
            // label_Inicio
            // 
            label_Inicio.AutoSize = true;
            label_Inicio.Font = new Font("Segoe Condensed", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_Inicio.Location = new Point(313, 115);
            label_Inicio.Name = "label_Inicio";
            label_Inicio.Size = new Size(164, 35);
            label_Inicio.TabIndex = 5;
            label_Inicio.Text = "Iniciar Sesión";
            label_Inicio.Click += label2_Click_1;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(289, 214);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(36, 27);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // FormIniciar
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pictureBox1);
            Controls.Add(label_Inicio);
            Controls.Add(LogoDaws);
            Controls.Add(txtUsuario);
            Controls.Add(label1);
            Controls.Add(btnLogin);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormIniciar";
            Text = "Iniciar Sesión - Recolección de Scrap";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)LogoDaws).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label_Inicio;
        private TextBox txtUsuario;
        private TextBox txtContraseña;
        private CheckBox checkBox1;
        private Button btnLogin;
        private PictureBox LogoDaws;
        private PictureBox pictureBox1;
    }
}
