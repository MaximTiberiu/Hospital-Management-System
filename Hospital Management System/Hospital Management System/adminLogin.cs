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
    public partial class adminLogin : Form
    {
        public adminLogin()
        {
            InitializeComponent();
            textBoxUser.Text = "spitaluser1";
            textBoxPass.Text = "spitalpass";
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            StartApp startApp = new StartApp();
            startApp.Show();
            this.Close();
        }

        void Verify()
        {
            if (String.IsNullOrEmpty(textBoxUser.Text.Trim()) || String.IsNullOrEmpty(textBoxPass.Text.Trim()))
            {
                MessageBox.Show("Completați toate câmpurile!", "Câmp gol.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (doctorLogin.hasSpaces(textBoxUser))
            {
                textBoxUser.Clear();
                MessageBox.Show("Nu pot exista spații în username. Vă rugăm să tastați din nou!", "Fara spații.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (doctorLogin.hasSpaces(textBoxPass))
            {
                textBoxPass.Clear();
                MessageBox.Show("Nu pot exista spații în parolă. Vă rugăm să tastați din nou!", "Fara spații.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonAdminLogin_Click(object sender, EventArgs e)
        {
            Verify();

            string user = textBoxUser.Text.Trim();
            string pass = textBoxPass.Text.Trim();

            if (user == "spitaluser1" && pass == "spitalpass")
            {
                adminMain adminMain = new adminMain();
                adminMain.Show();
                this.Hide();
            }    
            else
            {
                textBoxUser.Clear();
                textBoxPass.Clear();
                MessageBox.Show("Datele introduse nu sunt corecte!", "Date incorecte.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
