using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital_Management_System
{
    public partial class StartApp : Form
    {
        public const string connectionString = @"DATA SOURCE = 192.168.56.1:1521/xe; PERSIST SECURITY INFO=True; USER ID = spitaluser1; password=spitalpass; Pooling = False;";
        public StartApp()
        {
            InitializeComponent();
        }

        private void buttonPatient_Click(object sender, EventArgs e)
        {
            patientLogin patientLogin = new patientLogin();
            patientLogin.Show();
            this.Hide();
        }

        private void buttonDoctor_Click(object sender, EventArgs e)
        {
            doctorLogin doctorLogin = new doctorLogin();
            doctorLogin.Show();
            this.Hide();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            adminLogin adminLogin = new adminLogin();
            adminLogin.Show();
            this.Hide();
        }
    }
}
