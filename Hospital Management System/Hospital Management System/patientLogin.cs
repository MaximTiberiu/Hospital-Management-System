using Oracle.ManagedDataAccess.Client;
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
    public partial class patientLogin : Form
    {
        static private string CNP;
        static public string getCNP()
        {
            return CNP;
        }
        public patientLogin()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            StartApp startApp = new StartApp();
            startApp.Show();
            this.Close();
        }

        private void buttonPatientLogin_Click(object sender, EventArgs e)
        {
            string nume = textBoxNume.Text.Trim();
            CNP = textBoxCNP.Text.Trim();

            String queryString = String.Format(@"SELECT * FROM PACIENTI WHERE lower(nume) = lower('{0}') AND CNP = '{1}'", nume, CNP);
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    OracleDataReader dataReader = command.ExecuteReader();

                    if (dataReader.HasRows)
                    {
                        patientMain patientMain = new patientMain();
                        patientMain.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Datele introduse nu sunt corecte!", "Date incorecte.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxNume.Clear();
                        textBoxCNP.Clear();
                    }
                }
                catch (OracleException ex)
                {
                    for (int i = 0; i < ex.Errors.Count; i++)
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                            "Message: " + ex.Errors[i].Message + "\n" +
                            "LineNumber: " + ex.Errors[i].Number + "\n" +
                            "Source: " + ex.Errors[i].Source + "\n" +
                            "Procedure: " + ex.Errors[i].Procedure + "\n");
                    }
                    MessageBox.Show(errorMessages.ToString(), "Eroare.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
