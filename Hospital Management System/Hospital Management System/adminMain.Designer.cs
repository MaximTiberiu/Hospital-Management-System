
namespace Hospital_Management_System
{
    partial class adminMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(adminMain));
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelApp = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonBack1 = new System.Windows.Forms.Button();
            this.buttonLocalitati = new System.Windows.Forms.Button();
            this.panelLocalitati = new System.Windows.Forms.Panel();
            this.textBoxcodJudet = new System.Windows.Forms.TextBox();
            this.textBoxcodPostal = new System.Windows.Forms.TextBox();
            this.textBoxLocalitate = new System.Windows.Forms.TextBox();
            this.labelidLoc = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.buttonSpitale = new System.Windows.Forms.Button();
            this.buttonSectiiSpitale = new System.Windows.Forms.Button();
            this.buttonDoctori = new System.Windows.Forms.Button();
            this.buttonPacient = new System.Windows.Forms.Button();
            this.buttonSimptome = new System.Windows.Forms.Button();
            this.buttonBoli = new System.Windows.Forms.Button();
            this.buttonTratamente = new System.Windows.Forms.Button();
            this.buttonMedicamente = new System.Windows.Forms.Button();
            this.panelSpitale = new System.Windows.Forms.Panel();
            this.panelSectiiSpitale = new System.Windows.Forms.Panel();
            this.panelDoctori = new System.Windows.Forms.Panel();
            this.panelSimptome = new System.Windows.Forms.Panel();
            this.panelBoli = new System.Windows.Forms.Panel();
            this.panelTratamente = new System.Windows.Forms.Panel();
            this.panelMedicamente = new System.Windows.Forms.Panel();
            this.panelPacienti = new System.Windows.Forms.Panel();
            this.buttonSterge = new System.Windows.Forms.Button();
            this.buttonAdauga = new System.Windows.Forms.Button();
            this.buttonEditeaza = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.panelLocalitati.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelLogin
            // 
            this.labelLogin.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLogin.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelLogin.Location = new System.Drawing.Point(0, 96);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(1900, 52);
            this.labelLogin.TabIndex = 21;
            this.labelLogin.Text = "Admin";
            this.labelLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelApp
            // 
            this.labelApp.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelApp.Font = new System.Drawing.Font("Segoe UI Semibold", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelApp.ForeColor = System.Drawing.Color.Brown;
            this.labelApp.Location = new System.Drawing.Point(0, 0);
            this.labelApp.Name = "labelApp";
            this.labelApp.Size = new System.Drawing.Size(1900, 96);
            this.labelApp.TabIndex = 20;
            this.labelApp.Text = "Sistemul de Management al Spitalelor";
            this.labelApp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = global::Hospital_Management_System.Properties.Resources.logo;
            this.pictureBoxLogo.Location = new System.Drawing.Point(1632, 0);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(273, 904);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 22;
            this.pictureBoxLogo.TabStop = false;
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.Pink;
            this.buttonExit.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonExit.Location = new System.Drawing.Point(0, 848);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(272, 56);
            this.buttonExit.TabIndex = 24;
            this.buttonExit.Text = "Ieșire";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonBack1
            // 
            this.buttonBack1.BackColor = System.Drawing.Color.Pink;
            this.buttonBack1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBack1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonBack1.Location = new System.Drawing.Point(0, 792);
            this.buttonBack1.Name = "buttonBack1";
            this.buttonBack1.Size = new System.Drawing.Size(272, 56);
            this.buttonBack1.TabIndex = 23;
            this.buttonBack1.Text = "Înapoi";
            this.buttonBack1.UseVisualStyleBackColor = false;
            this.buttonBack1.Click += new System.EventHandler(this.buttonBack1_Click);
            // 
            // buttonLocalitati
            // 
            this.buttonLocalitati.BackColor = System.Drawing.Color.MistyRose;
            this.buttonLocalitati.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLocalitati.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonLocalitati.Location = new System.Drawing.Point(0, 152);
            this.buttonLocalitati.Name = "buttonLocalitati";
            this.buttonLocalitati.Size = new System.Drawing.Size(272, 56);
            this.buttonLocalitati.TabIndex = 25;
            this.buttonLocalitati.Text = "Localități";
            this.buttonLocalitati.UseVisualStyleBackColor = false;
            this.buttonLocalitati.Click += new System.EventHandler(this.buttonLocalitati_Click);
            // 
            // panelLocalitati
            // 
            this.panelLocalitati.BackColor = System.Drawing.Color.MistyRose;
            this.panelLocalitati.Controls.Add(this.buttonEditeaza);
            this.panelLocalitati.Controls.Add(this.buttonAdauga);
            this.panelLocalitati.Controls.Add(this.buttonSterge);
            this.panelLocalitati.Controls.Add(this.textBoxcodJudet);
            this.panelLocalitati.Controls.Add(this.textBoxcodPostal);
            this.panelLocalitati.Controls.Add(this.textBoxLocalitate);
            this.panelLocalitati.Controls.Add(this.labelidLoc);
            this.panelLocalitati.Controls.Add(this.label5);
            this.panelLocalitati.Controls.Add(this.label4);
            this.panelLocalitati.Controls.Add(this.label3);
            this.panelLocalitati.Controls.Add(this.label2);
            this.panelLocalitati.Controls.Add(this.dataGridView1);
            this.panelLocalitati.Controls.Add(this.label1);
            this.panelLocalitati.Controls.Add(this.comboBox1);
            this.panelLocalitati.Location = new System.Drawing.Point(272, 152);
            this.panelLocalitati.Name = "panelLocalitati";
            this.panelLocalitati.Size = new System.Drawing.Size(1360, 744);
            this.panelLocalitati.TabIndex = 26;
            // 
            // textBoxcodJudet
            // 
            this.textBoxcodJudet.Font = new System.Drawing.Font("Segoe UI Semibold", 14.2F, System.Drawing.FontStyle.Bold);
            this.textBoxcodJudet.Location = new System.Drawing.Point(240, 304);
            this.textBoxcodJudet.Name = "textBoxcodJudet";
            this.textBoxcodJudet.Size = new System.Drawing.Size(232, 39);
            this.textBoxcodJudet.TabIndex = 10;
            // 
            // textBoxcodPostal
            // 
            this.textBoxcodPostal.Font = new System.Drawing.Font("Segoe UI Semibold", 14.2F, System.Drawing.FontStyle.Bold);
            this.textBoxcodPostal.Location = new System.Drawing.Point(240, 256);
            this.textBoxcodPostal.Name = "textBoxcodPostal";
            this.textBoxcodPostal.Size = new System.Drawing.Size(232, 39);
            this.textBoxcodPostal.TabIndex = 9;
            // 
            // textBoxLocalitate
            // 
            this.textBoxLocalitate.Font = new System.Drawing.Font("Segoe UI Semibold", 14.2F, System.Drawing.FontStyle.Bold);
            this.textBoxLocalitate.Location = new System.Drawing.Point(240, 208);
            this.textBoxLocalitate.Name = "textBoxLocalitate";
            this.textBoxLocalitate.Size = new System.Drawing.Size(232, 39);
            this.textBoxLocalitate.TabIndex = 8;
            // 
            // labelidLoc
            // 
            this.labelidLoc.AutoSize = true;
            this.labelidLoc.BackColor = System.Drawing.Color.Transparent;
            this.labelidLoc.Font = new System.Drawing.Font("Segoe UI Semibold", 14.2F, System.Drawing.FontStyle.Bold);
            this.labelidLoc.Location = new System.Drawing.Point(240, 168);
            this.labelidLoc.Name = "labelidLoc";
            this.labelidLoc.Size = new System.Drawing.Size(35, 32);
            this.labelidLoc.TabIndex = 7;
            this.labelidLoc.Text = "id";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(88, 304);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 38);
            this.label5.TabIndex = 6;
            this.label5.Text = "codJudet:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(80, 256);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 38);
            this.label4.TabIndex = 5;
            this.label4.Text = "codPostal:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 208);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(219, 38);
            this.label3.TabIndex = 4;
            this.label3.Text = "numeLocalitate:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(56, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 38);
            this.label2.TabIndex = 3;
            this.label2.Text = "idLocalitate:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Azure;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.dataGridView1.Location = new System.Drawing.Point(480, 72);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(864, 592);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "Județ:";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(144, 72);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(328, 45);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // buttonSpitale
            // 
            this.buttonSpitale.BackColor = System.Drawing.Color.MistyRose;
            this.buttonSpitale.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSpitale.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonSpitale.Location = new System.Drawing.Point(0, 208);
            this.buttonSpitale.Name = "buttonSpitale";
            this.buttonSpitale.Size = new System.Drawing.Size(272, 56);
            this.buttonSpitale.TabIndex = 27;
            this.buttonSpitale.Text = "Spitale";
            this.buttonSpitale.UseVisualStyleBackColor = false;
            this.buttonSpitale.Click += new System.EventHandler(this.buttonSpitale_Click);
            // 
            // buttonSectiiSpitale
            // 
            this.buttonSectiiSpitale.BackColor = System.Drawing.Color.MistyRose;
            this.buttonSectiiSpitale.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSectiiSpitale.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonSectiiSpitale.Location = new System.Drawing.Point(0, 264);
            this.buttonSectiiSpitale.Name = "buttonSectiiSpitale";
            this.buttonSectiiSpitale.Size = new System.Drawing.Size(272, 56);
            this.buttonSectiiSpitale.TabIndex = 28;
            this.buttonSectiiSpitale.Text = "Secții Spitale";
            this.buttonSectiiSpitale.UseVisualStyleBackColor = false;
            this.buttonSectiiSpitale.Click += new System.EventHandler(this.buttonSectiiSpitale_Click);
            // 
            // buttonDoctori
            // 
            this.buttonDoctori.BackColor = System.Drawing.Color.Honeydew;
            this.buttonDoctori.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDoctori.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonDoctori.Location = new System.Drawing.Point(0, 336);
            this.buttonDoctori.Name = "buttonDoctori";
            this.buttonDoctori.Size = new System.Drawing.Size(272, 56);
            this.buttonDoctori.TabIndex = 29;
            this.buttonDoctori.Text = "Doctori";
            this.buttonDoctori.UseVisualStyleBackColor = false;
            this.buttonDoctori.Click += new System.EventHandler(this.buttonDoctori_Click);
            // 
            // buttonPacient
            // 
            this.buttonPacient.BackColor = System.Drawing.Color.Honeydew;
            this.buttonPacient.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPacient.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonPacient.Location = new System.Drawing.Point(0, 392);
            this.buttonPacient.Name = "buttonPacient";
            this.buttonPacient.Size = new System.Drawing.Size(272, 56);
            this.buttonPacient.TabIndex = 30;
            this.buttonPacient.Text = "Pacienți";
            this.buttonPacient.UseVisualStyleBackColor = false;
            this.buttonPacient.Click += new System.EventHandler(this.buttonPacient_Click);
            // 
            // buttonSimptome
            // 
            this.buttonSimptome.BackColor = System.Drawing.Color.LemonChiffon;
            this.buttonSimptome.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSimptome.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonSimptome.Location = new System.Drawing.Point(0, 464);
            this.buttonSimptome.Name = "buttonSimptome";
            this.buttonSimptome.Size = new System.Drawing.Size(272, 56);
            this.buttonSimptome.TabIndex = 31;
            this.buttonSimptome.Text = "Simptome";
            this.buttonSimptome.UseVisualStyleBackColor = false;
            this.buttonSimptome.Click += new System.EventHandler(this.buttonSimptome_Click);
            // 
            // buttonBoli
            // 
            this.buttonBoli.BackColor = System.Drawing.Color.LemonChiffon;
            this.buttonBoli.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBoli.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonBoli.Location = new System.Drawing.Point(0, 520);
            this.buttonBoli.Name = "buttonBoli";
            this.buttonBoli.Size = new System.Drawing.Size(272, 56);
            this.buttonBoli.TabIndex = 32;
            this.buttonBoli.Text = "Boli";
            this.buttonBoli.UseVisualStyleBackColor = false;
            this.buttonBoli.Click += new System.EventHandler(this.buttonBoli_Click);
            // 
            // buttonTratamente
            // 
            this.buttonTratamente.BackColor = System.Drawing.Color.LemonChiffon;
            this.buttonTratamente.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTratamente.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonTratamente.Location = new System.Drawing.Point(0, 576);
            this.buttonTratamente.Name = "buttonTratamente";
            this.buttonTratamente.Size = new System.Drawing.Size(272, 56);
            this.buttonTratamente.TabIndex = 33;
            this.buttonTratamente.Text = "Tratamente";
            this.buttonTratamente.UseVisualStyleBackColor = false;
            this.buttonTratamente.Click += new System.EventHandler(this.buttonTratamente_Click);
            // 
            // buttonMedicamente
            // 
            this.buttonMedicamente.BackColor = System.Drawing.Color.LemonChiffon;
            this.buttonMedicamente.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMedicamente.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonMedicamente.Location = new System.Drawing.Point(0, 632);
            this.buttonMedicamente.Name = "buttonMedicamente";
            this.buttonMedicamente.Size = new System.Drawing.Size(272, 56);
            this.buttonMedicamente.TabIndex = 34;
            this.buttonMedicamente.Text = "Medicamente";
            this.buttonMedicamente.UseVisualStyleBackColor = false;
            this.buttonMedicamente.Click += new System.EventHandler(this.buttonMedicamente_Click);
            // 
            // panelSpitale
            // 
            this.panelSpitale.BackColor = System.Drawing.Color.MistyRose;
            this.panelSpitale.Location = new System.Drawing.Point(272, 152);
            this.panelSpitale.Name = "panelSpitale";
            this.panelSpitale.Size = new System.Drawing.Size(1360, 744);
            this.panelSpitale.TabIndex = 27;
            // 
            // panelSectiiSpitale
            // 
            this.panelSectiiSpitale.BackColor = System.Drawing.Color.MistyRose;
            this.panelSectiiSpitale.Location = new System.Drawing.Point(272, 152);
            this.panelSectiiSpitale.Name = "panelSectiiSpitale";
            this.panelSectiiSpitale.Size = new System.Drawing.Size(1360, 744);
            this.panelSectiiSpitale.TabIndex = 28;
            // 
            // panelDoctori
            // 
            this.panelDoctori.BackColor = System.Drawing.Color.Honeydew;
            this.panelDoctori.Location = new System.Drawing.Point(272, 152);
            this.panelDoctori.Name = "panelDoctori";
            this.panelDoctori.Size = new System.Drawing.Size(1360, 744);
            this.panelDoctori.TabIndex = 29;
            // 
            // panelSimptome
            // 
            this.panelSimptome.BackColor = System.Drawing.Color.LightYellow;
            this.panelSimptome.Location = new System.Drawing.Point(272, 152);
            this.panelSimptome.Name = "panelSimptome";
            this.panelSimptome.Size = new System.Drawing.Size(1360, 744);
            this.panelSimptome.TabIndex = 31;
            // 
            // panelBoli
            // 
            this.panelBoli.BackColor = System.Drawing.Color.LightYellow;
            this.panelBoli.Location = new System.Drawing.Point(272, 152);
            this.panelBoli.Name = "panelBoli";
            this.panelBoli.Size = new System.Drawing.Size(1360, 744);
            this.panelBoli.TabIndex = 32;
            // 
            // panelTratamente
            // 
            this.panelTratamente.BackColor = System.Drawing.Color.LightYellow;
            this.panelTratamente.Location = new System.Drawing.Point(272, 152);
            this.panelTratamente.Name = "panelTratamente";
            this.panelTratamente.Size = new System.Drawing.Size(1360, 744);
            this.panelTratamente.TabIndex = 33;
            // 
            // panelMedicamente
            // 
            this.panelMedicamente.BackColor = System.Drawing.Color.LightYellow;
            this.panelMedicamente.Location = new System.Drawing.Point(272, 152);
            this.panelMedicamente.Name = "panelMedicamente";
            this.panelMedicamente.Size = new System.Drawing.Size(1360, 744);
            this.panelMedicamente.TabIndex = 34;
            // 
            // panelPacienti
            // 
            this.panelPacienti.BackColor = System.Drawing.Color.Honeydew;
            this.panelPacienti.Location = new System.Drawing.Point(272, 152);
            this.panelPacienti.Name = "panelPacienti";
            this.panelPacienti.Size = new System.Drawing.Size(1360, 744);
            this.panelPacienti.TabIndex = 35;
            // 
            // buttonSterge
            // 
            this.buttonSterge.BackColor = System.Drawing.Color.Red;
            this.buttonSterge.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSterge.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSterge.Location = new System.Drawing.Point(320, 424);
            this.buttonSterge.Name = "buttonSterge";
            this.buttonSterge.Size = new System.Drawing.Size(160, 56);
            this.buttonSterge.TabIndex = 36;
            this.buttonSterge.Text = "Șterge";
            this.buttonSterge.UseVisualStyleBackColor = false;
            // 
            // buttonAdauga
            // 
            this.buttonAdauga.BackColor = System.Drawing.Color.Green;
            this.buttonAdauga.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdauga.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonAdauga.Location = new System.Drawing.Point(0, 424);
            this.buttonAdauga.Name = "buttonAdauga";
            this.buttonAdauga.Size = new System.Drawing.Size(160, 56);
            this.buttonAdauga.TabIndex = 37;
            this.buttonAdauga.Text = "Adaugă";
            this.buttonAdauga.UseVisualStyleBackColor = false;
            // 
            // buttonEditeaza
            // 
            this.buttonEditeaza.BackColor = System.Drawing.Color.Yellow;
            this.buttonEditeaza.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEditeaza.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonEditeaza.Location = new System.Drawing.Point(160, 424);
            this.buttonEditeaza.Name = "buttonEditeaza";
            this.buttonEditeaza.Size = new System.Drawing.Size(160, 56);
            this.buttonEditeaza.TabIndex = 38;
            this.buttonEditeaza.Text = "Editează";
            this.buttonEditeaza.UseVisualStyleBackColor = false;
            // 
            // adminMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1900, 900);
            this.Controls.Add(this.panelLocalitati);
            this.Controls.Add(this.panelPacienti);
            this.Controls.Add(this.panelMedicamente);
            this.Controls.Add(this.panelTratamente);
            this.Controls.Add(this.panelBoli);
            this.Controls.Add(this.panelSimptome);
            this.Controls.Add(this.panelDoctori);
            this.Controls.Add(this.panelSectiiSpitale);
            this.Controls.Add(this.panelSpitale);
            this.Controls.Add(this.buttonMedicamente);
            this.Controls.Add(this.buttonTratamente);
            this.Controls.Add(this.buttonBoli);
            this.Controls.Add(this.buttonSimptome);
            this.Controls.Add(this.buttonPacient);
            this.Controls.Add(this.buttonDoctori);
            this.Controls.Add(this.buttonSectiiSpitale);
            this.Controls.Add(this.buttonSpitale);
            this.Controls.Add(this.buttonLocalitati);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonBack1);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.labelApp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "adminMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "adminMain";
            this.Load += new System.EventHandler(this.adminMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.panelLocalitati.ResumeLayout(false);
            this.panelLocalitati.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelApp;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonBack1;
        private System.Windows.Forms.Button buttonLocalitati;
        private System.Windows.Forms.Panel panelLocalitati;
        private System.Windows.Forms.Button buttonSpitale;
        private System.Windows.Forms.Button buttonSectiiSpitale;
        private System.Windows.Forms.Button buttonDoctori;
        private System.Windows.Forms.Button buttonPacient;
        private System.Windows.Forms.Button buttonSimptome;
        private System.Windows.Forms.Button buttonBoli;
        private System.Windows.Forms.Button buttonTratamente;
        private System.Windows.Forms.Button buttonMedicamente;
        private System.Windows.Forms.Panel panelSpitale;
        private System.Windows.Forms.Panel panelSectiiSpitale;
        private System.Windows.Forms.Panel panelDoctori;
        private System.Windows.Forms.Panel panelSimptome;
        private System.Windows.Forms.Panel panelBoli;
        private System.Windows.Forms.Panel panelTratamente;
        private System.Windows.Forms.Panel panelMedicamente;
        private System.Windows.Forms.Panel panelPacienti;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label labelidLoc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxcodJudet;
        private System.Windows.Forms.TextBox textBoxcodPostal;
        private System.Windows.Forms.TextBox textBoxLocalitate;
        private System.Windows.Forms.Button buttonEditeaza;
        private System.Windows.Forms.Button buttonAdauga;
        private System.Windows.Forms.Button buttonSterge;
    }
}