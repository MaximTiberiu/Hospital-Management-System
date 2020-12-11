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
    public partial class addDoctor : Form
    {

        public addDoctor()
        {
            InitializeComponent();
            fillComboBox(comboBoxFunctii);
            groupBoxIDSecție.Hide();
        }

        private string idSectie = "";

        public static void fillComboBox(ComboBox comboBoxToBeFilled)
        {
            String queryString = String.Format(@"SELECT * FROM FUNCTII");
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    OracleDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        string functie = dataReader["numeFunctie"].ToString();
                        comboBoxToBeFilled.Items.Add(functie);
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

        private void buttonBack_Click(object sender, EventArgs e)
        {
            StartApp startApp = new StartApp();
            startApp.Show();
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            groupBoxIDSecție.Show();
            String queryString = String.Format(@"SELECT * FROM SPITALE");
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    OracleDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        string spital = dataReader["numeSpital"].ToString();
                        comboBoxSpital.Items.Add(spital);
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
            if (comboBoxSpital.SelectedIndex == -1)
            {
                comboBoxSectie.Items.Add("Selectează un spital, pentru a afișa secțiile!");
            }
        }

        private void comboBoxSpital_SelectedIndexChanged(object sender, EventArgs e)
        {
            String queryString = String.Format(@"SELECT se.numeSectie, ss.idSectie, ss.corp, ss.etaj " +
                                                "FROM SECTII_SPITALE ss JOIN SPITALE s ON ss.idSpital = s.idSpital " +
                                                "JOIN SECTII se on ss.codSectie = se.codSectie " +
                                                "WHERE s.numeSpital = '" + comboBoxSpital.SelectedItem.ToString()) + "'";
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    if (comboBoxSpital.SelectedIndex != -1)
                    {
                        OracleDataReader dataReader = command.ExecuteReader();
                        comboBoxSectie.Items.RemoveAt(0);

                        while (dataReader.Read())
                        {
                            string sectie;
                            if (dataReader["corp"].ToString() == "")
                            {
                                sectie = dataReader["numeSectie"].ToString() + ", etaj " + dataReader["etaj"].ToString();
                                idSectie = dataReader["idSectie"].ToString();
                            }
                            else
                            {
                                sectie = dataReader["numeSectie"].ToString() + ", corpul " + dataReader["corp"].ToString() + ", etaj " + dataReader["etaj"].ToString();
                                idSectie = dataReader["idSectie"].ToString();
                            }
                            comboBoxSectie.Items.Add(sectie);
                        }
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

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            textBoxIDSectie.Text = idSectie;
            groupBoxIDSecție.Hide();
        }

        public static bool isAllDigits(TextBox textToBeVerified)
        {
            string stringToBeVerified = textToBeVerified.Text.Trim();

            foreach (char c in stringToBeVerified)
                if (!char.IsDigit(c)) return true;
            return false;
        }

        private void buttonDoctorAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxParafa.Text.Trim()) || String.IsNullOrEmpty(textBoxNume.Text.Trim())
                || String.IsNullOrEmpty(textBoxPrenume.Text.Trim()) || String.IsNullOrEmpty(textBoxCNP.Text.Trim())
                || String.IsNullOrEmpty(textBoxTelefon.Text.Trim()) || String.IsNullOrEmpty(textBoxEmail.Text.Trim())
                || String.IsNullOrEmpty(textBoxIDSectie.Text.Trim()) || comboBoxFunctii.SelectedIndex == -1) 
            {
                MessageBox.Show("Completați toate câmpurile!", "Câmp gol.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (doctorLogin.isAllDigits(textBoxParafa) || doctorLogin.hasCorrectLength(textBoxParafa, 6))
            {
                textBoxParafa.Clear();
                MessageBox.Show("Codul parafă este un număr de 6 cifre. Vă rugăm să tastați din nou!", "Lungime greșită.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (doctorLogin.isAllDigits(textBoxCNP) || doctorLogin.hasCorrectLength(textBoxCNP, 13))
            {
                textBoxCNP.Clear();
                MessageBox.Show("CNP este un număr de 13 cifre. Vă rugăm să tastați din nou!", "Lungime greșită.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
