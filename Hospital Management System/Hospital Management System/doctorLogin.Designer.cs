
namespace Hospital_Management_System
{
    partial class doctorLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(doctorLogin));
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelApp = new System.Windows.Forms.Label();
            this.buttonDoctor = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBoxCNP = new System.Windows.Forms.TextBox();
            this.textBoxParafa = new System.Windows.Forms.TextBox();
            this.labelCNP = new System.Windows.Forms.Label();
            this.labelParafa = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // labelLogin
            // 
            this.labelLogin.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLogin.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelLogin.Location = new System.Drawing.Point(0, 88);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(985, 112);
            this.labelLogin.TabIndex = 3;
            this.labelLogin.Text = "Autentificare - Medic";
            this.labelLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelApp
            // 
            this.labelApp.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelApp.Font = new System.Drawing.Font("Segoe UI Semibold", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelApp.ForeColor = System.Drawing.Color.Brown;
            this.labelApp.Location = new System.Drawing.Point(0, 0);
            this.labelApp.Name = "labelApp";
            this.labelApp.Size = new System.Drawing.Size(985, 88);
            this.labelApp.TabIndex = 2;
            this.labelApp.Text = "Sistemul de Management al Spitalelor";
            this.labelApp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonDoctor
            // 
            this.buttonDoctor.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonDoctor.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDoctor.Location = new System.Drawing.Point(688, 208);
            this.buttonDoctor.Name = "buttonDoctor";
            this.buttonDoctor.Size = new System.Drawing.Size(272, 56);
            this.buttonDoctor.TabIndex = 5;
            this.buttonDoctor.Text = "Autentificare";
            this.buttonDoctor.UseVisualStyleBackColor = false;
            this.buttonDoctor.Click += new System.EventHandler(this.buttonDoctor_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PowderBlue;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(688, 280);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(272, 56);
            this.button1.TabIndex = 6;
            this.button1.Text = "Adăugare Medic";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.PowderBlue;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(688, 352);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(272, 56);
            this.button2.TabIndex = 7;
            this.button2.Text = "Înapoi";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBoxCNP
            // 
            this.textBoxCNP.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCNP.Location = new System.Drawing.Point(184, 304);
            this.textBoxCNP.Name = "textBoxCNP";
            this.textBoxCNP.PasswordChar = '*';
            this.textBoxCNP.Size = new System.Drawing.Size(272, 43);
            this.textBoxCNP.TabIndex = 21;
            // 
            // textBoxParafa
            // 
            this.textBoxParafa.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxParafa.Location = new System.Drawing.Point(184, 248);
            this.textBoxParafa.Name = "textBoxParafa";
            this.textBoxParafa.Size = new System.Drawing.Size(272, 43);
            this.textBoxParafa.TabIndex = 20;
            // 
            // labelCNP
            // 
            this.labelCNP.AutoSize = true;
            this.labelCNP.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCNP.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelCNP.Location = new System.Drawing.Point(104, 304);
            this.labelCNP.Name = "labelCNP";
            this.labelCNP.Size = new System.Drawing.Size(78, 38);
            this.labelCNP.TabIndex = 19;
            this.labelCNP.Text = "CNP:";
            // 
            // labelParafa
            // 
            this.labelParafa.AutoSize = true;
            this.labelParafa.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelParafa.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelParafa.Location = new System.Drawing.Point(24, 248);
            this.labelParafa.Name = "labelParafa";
            this.labelParafa.Size = new System.Drawing.Size(163, 38);
            this.labelParafa.TabIndex = 18;
            this.labelParafa.Text = "Cod Parafă:";
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBoxLogo.Image = global::Hospital_Management_System.Properties.Resources.logo;
            this.pictureBoxLogo.Location = new System.Drawing.Point(0, 419);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(985, 143);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 4;
            this.pictureBoxLogo.TabStop = false;
            // 
            // doctorLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(985, 562);
            this.Controls.Add(this.textBoxCNP);
            this.Controls.Add(this.textBoxParafa);
            this.Controls.Add(this.labelCNP);
            this.Controls.Add(this.labelParafa);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonDoctor);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.labelApp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "doctorLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Doctor - Login";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelApp;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Button buttonDoctor;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBoxCNP;
        private System.Windows.Forms.TextBox textBoxParafa;
        private System.Windows.Forms.Label labelCNP;
        private System.Windows.Forms.Label labelParafa;
    }
}