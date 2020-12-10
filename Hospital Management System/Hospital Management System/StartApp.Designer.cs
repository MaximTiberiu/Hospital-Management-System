
namespace Hospital_Management_System
{
    partial class StartApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartApp));
            this.labelApp = new System.Windows.Forms.Label();
            this.labelLogin = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.buttonPatient = new System.Windows.Forms.Button();
            this.buttonDoctor = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // labelApp
            // 
            this.labelApp.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelApp.Font = new System.Drawing.Font("Segoe UI Semibold", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelApp.ForeColor = System.Drawing.Color.Brown;
            this.labelApp.Location = new System.Drawing.Point(0, 0);
            this.labelApp.Name = "labelApp";
            this.labelApp.Size = new System.Drawing.Size(985, 96);
            this.labelApp.TabIndex = 0;
            this.labelApp.Text = "Sistemul de Management al Spitalelor";
            this.labelApp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelLogin
            // 
            this.labelLogin.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLogin.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelLogin.Location = new System.Drawing.Point(0, 96);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(985, 80);
            this.labelLogin.TabIndex = 1;
            this.labelLogin.Text = "Autentificare";
            this.labelLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBoxLogo.Image = global::Hospital_Management_System.Properties.Resources.logo;
            this.pictureBoxLogo.Location = new System.Drawing.Point(0, 429);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(985, 133);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 2;
            this.pictureBoxLogo.TabStop = false;
            // 
            // buttonPatient
            // 
            this.buttonPatient.BackColor = System.Drawing.Color.PeachPuff;
            this.buttonPatient.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPatient.Location = new System.Drawing.Point(336, 184);
            this.buttonPatient.Name = "buttonPatient";
            this.buttonPatient.Size = new System.Drawing.Size(312, 72);
            this.buttonPatient.TabIndex = 3;
            this.buttonPatient.Text = "Sunt Pacient";
            this.buttonPatient.UseVisualStyleBackColor = false;
            this.buttonPatient.Click += new System.EventHandler(this.buttonPatient_Click);
            // 
            // buttonDoctor
            // 
            this.buttonDoctor.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonDoctor.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDoctor.Location = new System.Drawing.Point(336, 264);
            this.buttonDoctor.Name = "buttonDoctor";
            this.buttonDoctor.Size = new System.Drawing.Size(312, 72);
            this.buttonDoctor.TabIndex = 4;
            this.buttonDoctor.Text = "Sunt Medic";
            this.buttonDoctor.UseVisualStyleBackColor = false;
            this.buttonDoctor.Click += new System.EventHandler(this.buttonDoctor_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.Pink;
            this.buttonExit.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.Location = new System.Drawing.Point(336, 344);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(312, 72);
            this.buttonExit.TabIndex = 5;
            this.buttonExit.Text = "Ieșire";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // StartApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(985, 562);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonDoctor);
            this.Controls.Add(this.buttonPatient);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.labelApp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "StartApp";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hospital Management System";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelApp;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Button buttonPatient;
        private System.Windows.Forms.Button buttonDoctor;
        private System.Windows.Forms.Button buttonExit;
    }
}