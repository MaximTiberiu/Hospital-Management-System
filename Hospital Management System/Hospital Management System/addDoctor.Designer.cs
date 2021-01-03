
namespace Hospital_Management_System
{
    partial class addDoctor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(addDoctor));
            this.textBoxNume = new System.Windows.Forms.TextBox();
            this.labelNume = new System.Windows.Forms.Label();
            this.labelParafa = new System.Windows.Forms.Label();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonDoctorAdd = new System.Windows.Forms.Button();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelApp = new System.Windows.Forms.Label();
            this.textBoxCNP = new System.Windows.Forms.TextBox();
            this.textBoxPrenume = new System.Windows.Forms.TextBox();
            this.labelCNP = new System.Windows.Forms.Label();
            this.labelPrenume = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.textBoxTelefon = new System.Windows.Forms.TextBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.labelTelefon = new System.Windows.Forms.Label();
            this.textBoxIDSectie = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.labelCodFunctie = new System.Windows.Forms.Label();
            this.comboBoxFunctii = new System.Windows.Forms.ComboBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBoxIDSecție = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxSectie = new System.Windows.Forms.ComboBox();
            this.labelSpital = new System.Windows.Forms.Label();
            this.comboBoxSpital = new System.Windows.Forms.ComboBox();
            this.labelcodParafa_print = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.groupBoxIDSecție.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxNume
            // 
            this.textBoxNume.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNume.Location = new System.Drawing.Point(176, 208);
            this.textBoxNume.Name = "textBoxNume";
            this.textBoxNume.Size = new System.Drawing.Size(272, 43);
            this.textBoxNume.TabIndex = 35;
            // 
            // labelNume
            // 
            this.labelNume.AutoSize = true;
            this.labelNume.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNume.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelNume.Location = new System.Drawing.Point(72, 208);
            this.labelNume.Name = "labelNume";
            this.labelNume.Size = new System.Drawing.Size(101, 38);
            this.labelNume.TabIndex = 33;
            this.labelNume.Text = "Nume:";
            // 
            // labelParafa
            // 
            this.labelParafa.AutoSize = true;
            this.labelParafa.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelParafa.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelParafa.Location = new System.Drawing.Point(16, 152);
            this.labelParafa.Name = "labelParafa";
            this.labelParafa.Size = new System.Drawing.Size(163, 38);
            this.labelParafa.TabIndex = 32;
            this.labelParafa.Text = "Cod Parafă:";
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonBack.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBack.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonBack.Location = new System.Drawing.Point(728, 536);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(272, 56);
            this.buttonBack.TabIndex = 31;
            this.buttonBack.Text = "Înapoi";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonDoctorAdd
            // 
            this.buttonDoctorAdd.BackColor = System.Drawing.Color.PowderBlue;
            this.buttonDoctorAdd.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDoctorAdd.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonDoctorAdd.Location = new System.Drawing.Point(728, 472);
            this.buttonDoctorAdd.Name = "buttonDoctorAdd";
            this.buttonDoctorAdd.Size = new System.Drawing.Size(272, 56);
            this.buttonDoctorAdd.TabIndex = 30;
            this.buttonDoctorAdd.Text = "Adăugare";
            this.buttonDoctorAdd.UseVisualStyleBackColor = false;
            this.buttonDoctorAdd.Click += new System.EventHandler(this.buttonDoctorAdd_Click);
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBoxLogo.Image = global::Hospital_Management_System.Properties.Resources.logo;
            this.pictureBoxLogo.Location = new System.Drawing.Point(1005, 144);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(195, 456);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 29;
            this.pictureBoxLogo.TabStop = false;
            // 
            // labelLogin
            // 
            this.labelLogin.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLogin.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelLogin.Location = new System.Drawing.Point(0, 72);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(1200, 72);
            this.labelLogin.TabIndex = 28;
            this.labelLogin.Text = "Adăugare Medic";
            this.labelLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelApp
            // 
            this.labelApp.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelApp.Font = new System.Drawing.Font("Segoe UI Semibold", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelApp.ForeColor = System.Drawing.Color.Brown;
            this.labelApp.Location = new System.Drawing.Point(0, 0);
            this.labelApp.Name = "labelApp";
            this.labelApp.Size = new System.Drawing.Size(1200, 72);
            this.labelApp.TabIndex = 27;
            this.labelApp.Text = "Sistemul de Management al Spitalelor";
            this.labelApp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxCNP
            // 
            this.textBoxCNP.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCNP.Location = new System.Drawing.Point(176, 320);
            this.textBoxCNP.Name = "textBoxCNP";
            this.textBoxCNP.Size = new System.Drawing.Size(272, 43);
            this.textBoxCNP.TabIndex = 39;
            // 
            // textBoxPrenume
            // 
            this.textBoxPrenume.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPrenume.Location = new System.Drawing.Point(176, 264);
            this.textBoxPrenume.Name = "textBoxPrenume";
            this.textBoxPrenume.Size = new System.Drawing.Size(272, 43);
            this.textBoxPrenume.TabIndex = 38;
            // 
            // labelCNP
            // 
            this.labelCNP.AutoSize = true;
            this.labelCNP.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCNP.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelCNP.Location = new System.Drawing.Point(96, 320);
            this.labelCNP.Name = "labelCNP";
            this.labelCNP.Size = new System.Drawing.Size(78, 38);
            this.labelCNP.TabIndex = 37;
            this.labelCNP.Text = "CNP:";
            // 
            // labelPrenume
            // 
            this.labelPrenume.AutoSize = true;
            this.labelPrenume.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPrenume.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelPrenume.Location = new System.Drawing.Point(40, 264);
            this.labelPrenume.Name = "labelPrenume";
            this.labelPrenume.Size = new System.Drawing.Size(137, 38);
            this.labelPrenume.TabIndex = 36;
            this.labelPrenume.Text = "Prenume:";
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxEmail.Location = new System.Drawing.Point(176, 432);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(272, 43);
            this.textBoxEmail.TabIndex = 43;
            // 
            // textBoxTelefon
            // 
            this.textBoxTelefon.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTelefon.Location = new System.Drawing.Point(176, 376);
            this.textBoxTelefon.Name = "textBoxTelefon";
            this.textBoxTelefon.Size = new System.Drawing.Size(272, 43);
            this.textBoxTelefon.TabIndex = 42;
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEmail.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelEmail.Location = new System.Drawing.Point(80, 432);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(93, 38);
            this.labelEmail.TabIndex = 41;
            this.labelEmail.Text = "Email:";
            // 
            // labelTelefon
            // 
            this.labelTelefon.AutoSize = true;
            this.labelTelefon.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTelefon.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelTelefon.Location = new System.Drawing.Point(56, 376);
            this.labelTelefon.Name = "labelTelefon";
            this.labelTelefon.Size = new System.Drawing.Size(116, 38);
            this.labelTelefon.TabIndex = 40;
            this.labelTelefon.Text = "Telefon:";
            // 
            // textBoxIDSectie
            // 
            this.textBoxIDSectie.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxIDSectie.Location = new System.Drawing.Point(176, 544);
            this.textBoxIDSectie.Name = "textBoxIDSectie";
            this.textBoxIDSectie.Size = new System.Drawing.Size(272, 43);
            this.textBoxIDSectie.TabIndex = 47;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.OrangeRed;
            this.label5.Location = new System.Drawing.Point(40, 544);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 38);
            this.label5.TabIndex = 45;
            this.label5.Text = "ID Secție:";
            // 
            // labelCodFunctie
            // 
            this.labelCodFunctie.AutoSize = true;
            this.labelCodFunctie.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCodFunctie.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelCodFunctie.Location = new System.Drawing.Point(56, 488);
            this.labelCodFunctie.Name = "labelCodFunctie";
            this.labelCodFunctie.Size = new System.Drawing.Size(115, 38);
            this.labelCodFunctie.TabIndex = 44;
            this.labelCodFunctie.Text = "Funcție:";
            // 
            // comboBoxFunctii
            // 
            this.comboBoxFunctii.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxFunctii.FormattingEnabled = true;
            this.comboBoxFunctii.Location = new System.Drawing.Point(176, 488);
            this.comboBoxFunctii.Name = "comboBoxFunctii";
            this.comboBoxFunctii.Size = new System.Drawing.Size(272, 45);
            this.comboBoxFunctii.TabIndex = 48;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.CadetBlue;
            this.linkLabel1.Location = new System.Drawing.Point(464, 552);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(196, 32);
            this.linkLabel1.TabIndex = 49;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Nu știu ID Secție";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // groupBoxIDSecție
            // 
            this.groupBoxIDSecție.Controls.Add(this.button1);
            this.groupBoxIDSecție.Controls.Add(this.label1);
            this.groupBoxIDSecție.Controls.Add(this.comboBoxSectie);
            this.groupBoxIDSecție.Controls.Add(this.labelSpital);
            this.groupBoxIDSecție.Controls.Add(this.comboBoxSpital);
            this.groupBoxIDSecție.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxIDSecție.ForeColor = System.Drawing.Color.CadetBlue;
            this.groupBoxIDSecție.Location = new System.Drawing.Point(472, 176);
            this.groupBoxIDSecție.Name = "groupBoxIDSecție";
            this.groupBoxIDSecție.Size = new System.Drawing.Size(528, 280);
            this.groupBoxIDSecție.TabIndex = 50;
            this.groupBoxIDSecție.TabStop = false;
            this.groupBoxIDSecție.Text = "Află ID Secție";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PowderBlue;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12.2F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(16, 224);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 48);
            this.button1.TabIndex = 51;
            this.button1.Text = "Confirmă";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12.2F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(16, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 30);
            this.label1.TabIndex = 52;
            this.label1.Text = "Selectează Secția Spitalului:";
            // 
            // comboBoxSectie
            // 
            this.comboBoxSectie.Font = new System.Drawing.Font("Segoe UI Semibold", 12.2F, System.Drawing.FontStyle.Bold);
            this.comboBoxSectie.FormattingEnabled = true;
            this.comboBoxSectie.Location = new System.Drawing.Point(16, 176);
            this.comboBoxSectie.Name = "comboBoxSectie";
            this.comboBoxSectie.Size = new System.Drawing.Size(480, 36);
            this.comboBoxSectie.TabIndex = 53;
            // 
            // labelSpital
            // 
            this.labelSpital.AutoSize = true;
            this.labelSpital.Font = new System.Drawing.Font("Segoe UI Semibold", 12.2F, System.Drawing.FontStyle.Bold);
            this.labelSpital.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelSpital.Location = new System.Drawing.Point(16, 40);
            this.labelSpital.Name = "labelSpital";
            this.labelSpital.Size = new System.Drawing.Size(329, 30);
            this.labelSpital.TabIndex = 51;
            this.labelSpital.Text = "Selectează Spitalul în care lucrezi:";
            // 
            // comboBoxSpital
            // 
            this.comboBoxSpital.Font = new System.Drawing.Font("Segoe UI Semibold", 12.2F, System.Drawing.FontStyle.Bold);
            this.comboBoxSpital.FormattingEnabled = true;
            this.comboBoxSpital.Location = new System.Drawing.Point(16, 80);
            this.comboBoxSpital.Name = "comboBoxSpital";
            this.comboBoxSpital.Size = new System.Drawing.Size(480, 36);
            this.comboBoxSpital.TabIndex = 51;
            this.comboBoxSpital.SelectedIndexChanged += new System.EventHandler(this.comboBoxSpital_SelectedIndexChanged);
            // 
            // labelcodParafa_print
            // 
            this.labelcodParafa_print.AutoSize = true;
            this.labelcodParafa_print.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelcodParafa_print.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelcodParafa_print.Location = new System.Drawing.Point(192, 152);
            this.labelcodParafa_print.Name = "labelcodParafa_print";
            this.labelcodParafa_print.Size = new System.Drawing.Size(0, 38);
            this.labelcodParafa_print.TabIndex = 51;
            // 
            // addDoctor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.Controls.Add(this.labelcodParafa_print);
            this.Controls.Add(this.groupBoxIDSecție);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.comboBoxFunctii);
            this.Controls.Add(this.textBoxIDSectie);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelCodFunctie);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.textBoxTelefon);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.labelTelefon);
            this.Controls.Add(this.textBoxCNP);
            this.Controls.Add(this.textBoxPrenume);
            this.Controls.Add(this.labelCNP);
            this.Controls.Add(this.labelPrenume);
            this.Controls.Add(this.textBoxNume);
            this.Controls.Add(this.labelNume);
            this.Controls.Add(this.labelParafa);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonDoctorAdd);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.labelApp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "addDoctor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "addDoctor";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.groupBoxIDSecție.ResumeLayout(false);
            this.groupBoxIDSecție.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxNume;
        private System.Windows.Forms.Label labelNume;
        private System.Windows.Forms.Label labelParafa;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonDoctorAdd;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelApp;
        private System.Windows.Forms.TextBox textBoxCNP;
        private System.Windows.Forms.TextBox textBoxPrenume;
        private System.Windows.Forms.Label labelCNP;
        private System.Windows.Forms.Label labelPrenume;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.TextBox textBoxTelefon;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.Label labelTelefon;
        private System.Windows.Forms.TextBox textBoxIDSectie;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelCodFunctie;
        private System.Windows.Forms.ComboBox comboBoxFunctii;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.GroupBox groupBoxIDSecție;
        private System.Windows.Forms.Label labelSpital;
        private System.Windows.Forms.ComboBox comboBoxSpital;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxSectie;
        private System.Windows.Forms.Label labelcodParafa_print;
    }
}