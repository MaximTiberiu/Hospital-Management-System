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
    }
}
