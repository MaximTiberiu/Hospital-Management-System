using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Hospital_Management_System
{
    public partial class doctorLogin : Form
    {
        static private string codParafa;
        static public string get_codParafa()
        {
            return codParafa;
        }
        public doctorLogin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StartApp startApp = new StartApp();
            startApp.Show();
            this.Close();
        }

        public static bool isAllDigits(TextBox textToBeVerified)
        {
            string stringToBeVerified = textToBeVerified.Text.Trim();

            foreach (char c in stringToBeVerified)
                if (!char.IsDigit(c)) return true;
            return false;
        }

        public static bool hasSpaces(TextBox textToBeVerified)
        {
            string stringToBeVerified = textToBeVerified.Text;

            foreach (char c in stringToBeVerified)
                if (c == ' ') return true;
            return false;
        }

        public static bool hasCorrectLength(TextBox textToBeVerified, int num)
        {
            string stringToBeVerified = textToBeVerified.Text;

            return !(stringToBeVerified.Length == num);
        }

        private void buttonDoctor_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxParafa.Text.Trim()) || String.IsNullOrEmpty(textBoxCNP.Text.Trim())) 
            {
                MessageBox.Show("Completați toate câmpurile!", "Câmp gol.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (hasSpaces(textBoxParafa))
            {
                textBoxParafa.Clear();
                MessageBox.Show("Nu pot exista spații în codul parafă. Vă rugăm să tastați din nou!", "Fara spații.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (hasSpaces(textBoxCNP))
            {
                textBoxCNP.Clear();
                MessageBox.Show("Nu pot exista spații în CNP. Vă rugăm să tastați din nou!", "Fara spații.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (isAllDigits(textBoxParafa) || hasCorrectLength(textBoxParafa, 6))
            {
                textBoxParafa.Clear();
                MessageBox.Show("Codul parafă este un număr de 6 cifre. Vă rugăm să tastați din nou!", "Lungime greșită.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (isAllDigits(textBoxCNP) || hasCorrectLength(textBoxCNP, 13))
            {
                textBoxCNP.Clear();
                MessageBox.Show("CNP este un număr de 13 cifre. Vă rugăm să tastați din nou!", "Lungime greșită.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            codParafa = textBoxParafa.Text.Trim();
            string CNP = textBoxCNP.Text.Trim();

            String queryString = String.Format(@"SELECT * FROM DOCTORI WHERE codParafa = '{0}' AND CNP = '{1}'", codParafa, CNP);
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
                        doctorMain doctorMain = new doctorMain();
                        doctorMain.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Datele introduse nu sunt corecte!", "Date incorecte.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxParafa.Clear();
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

        private void button1_Click(object sender, EventArgs e)
        {
            addDoctor addDoctor = new addDoctor();
            addDoctor.Show();
            this.Hide();
        }
    }
}
