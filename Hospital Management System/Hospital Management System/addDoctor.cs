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

        public static bool isAllLetters(TextBox textToBeVerified)
        {
            string stringToBeVerified = textToBeVerified.Text.Trim();

            foreach (char c in stringToBeVerified)
                if (!char.IsLetter (c)) return true;
            return false;
        }

        public static bool IsValidEmail(TextBox textToBeVerified)
        {
            string stringToBeVerified = textToBeVerified.Text.Trim();
            try
            {
                var addr = new System.Net.Mail.MailAddress(stringToBeVerified);
                return !(addr.Address == stringToBeVerified);
            }
            catch
            {
                return true;
            }
        }

        private void Verify()
        {
            if (String.IsNullOrEmpty(textBoxParafa.Text.Trim()) || String.IsNullOrEmpty(textBoxNume.Text.Trim())
                || String.IsNullOrEmpty(textBoxPrenume.Text.Trim()) || String.IsNullOrEmpty(textBoxCNP.Text.Trim())
                || String.IsNullOrEmpty(textBoxTelefon.Text.Trim()) || String.IsNullOrEmpty(textBoxEmail.Text.Trim())
                || String.IsNullOrEmpty(textBoxIDSectie.Text.Trim()) || comboBoxFunctii.SelectedIndex == -1)
            {
                MessageBox.Show("Completați toate câmpurile!", "Câmp gol.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (doctorLogin.hasSpaces(textBoxParafa))
            {
                textBoxParafa.Clear();
                MessageBox.Show("Nu pot exista spații în codul parafă. Vă rugăm să tastați din nou!", "Fara spații.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (doctorLogin.hasSpaces(textBoxCNP))
            {
                textBoxCNP.Clear();
                MessageBox.Show("Nu pot exista spații în CNP. Vă rugăm să tastați din nou!", "Fara spații.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (doctorLogin.hasSpaces(textBoxTelefon))
            {
                textBoxTelefon.Clear();
                MessageBox.Show("Nu pot exista spații în numărul de telefon. Vă rugăm să tastați din nou!", "Fara spații.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (doctorLogin.hasSpaces(textBoxEmail))
            {
                textBoxEmail.Clear();
                MessageBox.Show("Nu pot exista spații adresa de email. Vă rugăm să tastați din nou!", "Fara spații.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (doctorLogin.hasSpaces(textBoxIDSectie))
            {
                textBoxIDSectie.Clear();
                MessageBox.Show("Nu pot exista spații în ID-ul secției. Vă rugăm să tastați din nou!", "Fara spații.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IsValidEmail(textBoxEmail))
            {
                textBoxEmail.Clear();
                MessageBox.Show("Email invalid. Vă rugăm să tastați din nou!", "Email invalid.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (isAllLetters(textBoxNume))
            {
                textBoxNume.Clear();
                MessageBox.Show("Numele este format doar litere. Vă rugăm să tastați din nou!", "Caractere incorecte.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (isAllLetters(textBoxPrenume))
            {
                textBoxPrenume.Clear();
                MessageBox.Show("Prenumele este format doar litere. Vă rugăm să tastați din nou!", "Caractere incorecte.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (doctorLogin.isAllDigits(textBoxIDSectie))
            {
                textBoxIDSectie.Clear();
                MessageBox.Show("ID-ul secției este un număr. Vă rugăm să tastați din nou!", "Caractere incorecte.", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (doctorLogin.isAllDigits(textBoxTelefon) || doctorLogin.hasCorrectLength(textBoxTelefon, 10))
            {
                textBoxTelefon.Clear();
                MessageBox.Show("Numărul de telefon este un număr de 10 cifre. Vă rugăm să tastați din nou!", "Lungime greșită.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonDoctorAdd_Click(object sender, EventArgs e)
        {
            Verify();

            string codParafa = textBoxParafa.Text.Trim();
            string nume = textBoxNume.Text.Trim();
            string prenume = textBoxPrenume.Text.Trim();
            string CNP = textBoxCNP.Text.Trim();
            string telefon = textBoxTelefon.Text.Trim();
            string email = textBoxEmail.Text.Trim();
            int codFunctie = (comboBoxFunctii.SelectedIndex + 1);
            string idSectie = textBoxIDSectie.Text.Trim();

            String queryString = String.Format(@"INSERT INTO DOCTORI VALUES({0}, '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7})",
                codParafa, nume, prenume, CNP, telefon, email, codFunctie, idSectie);
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    int rows = command.ExecuteNonQuery();

                    if (rows != 0)
                    {
                        MessageBox.Show("ADAUGAT!");
                    }
                    else
                    {
                        MessageBox.Show("NU A FOST ADAUGAT!");
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
