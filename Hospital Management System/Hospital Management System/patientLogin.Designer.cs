
namespace Hospital_Management_System
{
    partial class patientLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(patientLogin));
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonPatientLogin = new System.Windows.Forms.Button();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelApp = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelCNP = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxCNP = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.Color.PeachPuff;
            this.buttonBack.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBack.Location = new System.Drawing.Point(688, 312);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(272, 56);
            this.buttonBack.TabIndex = 13;
            this.buttonBack.Text = "Înapoi";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonPatientLogin
            // 
            this.buttonPatientLogin.BackColor = System.Drawing.Color.PeachPuff;
            this.buttonPatientLogin.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPatientLogin.Location = new System.Drawing.Point(688, 248);
            this.buttonPatientLogin.Name = "buttonPatientLogin";
            this.buttonPatientLogin.Size = new System.Drawing.Size(272, 56);
            this.buttonPatientLogin.TabIndex = 11;
            this.buttonPatientLogin.Text = "Autentificare";
            this.buttonPatientLogin.UseVisualStyleBackColor = false;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBoxLogo.Image = global::Hospital_Management_System.Properties.Resources.logo;
            this.pictureBoxLogo.Location = new System.Drawing.Point(0, 419);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(985, 143);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 10;
            this.pictureBoxLogo.TabStop = false;
            // 
            // labelLogin
            // 
            this.labelLogin.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLogin.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelLogin.Location = new System.Drawing.Point(0, 88);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(985, 112);
            this.labelLogin.TabIndex = 9;
            this.labelLogin.Text = "Autentificare - Pacient";
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
            this.labelApp.TabIndex = 8;
            this.labelApp.Text = "Sistemul de Management al Spitalelor";
            this.labelApp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelName.Location = new System.Drawing.Point(16, 256);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(126, 48);
            this.labelName.TabIndex = 14;
            this.labelName.Text = "Nume:";
            // 
            // labelCNP
            // 
            this.labelCNP.AutoSize = true;
            this.labelCNP.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCNP.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelCNP.Location = new System.Drawing.Point(40, 312);
            this.labelCNP.Name = "labelCNP";
            this.labelCNP.Size = new System.Drawing.Size(78, 38);
            this.labelCNP.TabIndex = 15;
            this.labelCNP.Text = "CNP:";
            // 
            // textBoxName
            // 
            this.textBoxName.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxName.Location = new System.Drawing.Point(112, 256);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(272, 43);
            this.textBoxName.TabIndex = 16;
            // 
            // textBoxCNP
            // 
            this.textBoxCNP.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCNP.Location = new System.Drawing.Point(112, 312);
            this.textBoxCNP.Name = "textBoxCNP";
            this.textBoxCNP.Size = new System.Drawing.Size(272, 43);
            this.textBoxCNP.TabIndex = 17;
            // 
            // patientLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(985, 562);
            this.Controls.Add(this.textBoxCNP);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelCNP);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonPatientLogin);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.labelApp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "patientLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Patient - Login";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonPatientLogin;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelApp;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelCNP;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxCNP;
    }
}