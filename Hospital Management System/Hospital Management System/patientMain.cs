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
    public partial class patientMain : Form
    {
        private void hidePanels()
        {
            panelDatePersonale.SendToBack();
            panelDatePersonale.Hide();
            panelInternari.SendToBack();
            panelInternari.Hide();
            panelIstoric.SendToBack();
            panelIstoric.Hide();
            panelTratamente.SendToBack();
            panelTratamente.Hide();
        }

        public patientMain()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void buttonBack1_Click(object sender, EventArgs e)
        {
            StartApp startApp = new StartApp();
            startApp.Show();
            this.Close();
        }

        void autoSizeDataGridView(DataGridView grd)
        {
            for (int i = 0; i < grd.Columns.Count - 1; i++)
                grd.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd.Columns[grd.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            for (int i = 0; i <= grd.Columns.Count - 1; i++)
            {
                int colw = grd.Columns[i].Width;
                grd.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grd.Columns[i].Width = colw;
            }
        }

        private void buttonDatePersonale_Click(object sender, EventArgs e)
        {
            hidePanels();
            panelDatePersonale.Show();
            panelDatePersonale.BringToFront();

            textBoxNumePacient.Enabled = false;
            textBoxPrenumePacient.Enabled = false;
            textBoxCNPPacient.Enabled = false;
            textBoxStradaPacient.Enabled = false;
            textBoxNumarPacient.Enabled = false;
            textBoxLocalitatePacient.Enabled = false;
            textBoxTelefonPacient.Enabled = false;
            textBoxEmailPacient.Enabled = false;
            checkBoxAsigurat.Enabled = false;

            String queryString = String.Format(@"SELECT * FROM (SELECT * FROM PACIENTI WHERE CNP = " + patientLogin.getCNP() + " ORDER BY dataInternare DESC) WHERE ROWNUM = 1");
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    OracleDataReader dataReader = command.ExecuteReader();
                    
                    while(dataReader.Read())
                    {
                        textBoxNumePacient.Text = dataReader["nume"].ToString();
                        textBoxPrenumePacient.Text = dataReader["prenume"].ToString();
                        textBoxCNPPacient.Text = dataReader["CNP"].ToString();
                        textBoxTelefonPacient.Text = dataReader["telefon"].ToString();
                        textBoxEmailPacient.Text = dataReader["email"].ToString();
                        textBoxStradaPacient.Text = dataReader["strada"].ToString();
                        textBoxNumarPacient.Text = dataReader["numar"].ToString();
                        textBoxLocalitatePacient.Text = dataReader["localitate"].ToString();

                        if (int.Parse(dataReader["asigurat"].ToString()) == 1)
                            checkBoxAsigurat.Checked = true;
                        else
                            checkBoxAsigurat.Checked = false;
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

        private void patientMain_Load(object sender, EventArgs e)
        {
            hidePanels();
        }

        private void buttonDoctori_Click(object sender, EventArgs e)
        {
            hidePanels();
            panelInternari.Show();
            panelInternari.BringToFront();
            loadInternari();

        }

        private void loadInternari()
        {
            String queryString = String.Format(@"SELECT * FROM INTERNARI_PACIENTI WHERE ID_PACIENT IN (SELECT idPacient FROM PACIENTI WHERE CNP = " + patientLogin.getCNP() + ") ORDER BY ID_PACIENT ASC");
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    DataTable dataTable = new DataTable();
                    OracleDataAdapter dataAdapter = new OracleDataAdapter(command);
                    dataAdapter.Fill(dataTable);
                    dataGridViewInternari.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewInternari);
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

        void loadTratamente()
        {
            String queryString = String.Format(@"SELECT * FROM TRATAMENTE_PACIENT WHERE ID_DIAGNOSTIC IN (SELECT ID_DIAGNOSTIC FROM ISTORIC_PACIENT WHERE ID_PACIENT IN (SELECT idPacient FROM PACIENTI WHERE CNP = " + patientLogin.getCNP() + ")) ORDER BY ID_DIAGNOSTIC, ID_TRATAMENT");
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    DataTable dataTable = new DataTable();
                    OracleDataAdapter dataAdapter = new OracleDataAdapter(command);
                    dataAdapter.Fill(dataTable);
                    dataGridViewTratamente.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewTratamente);
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

        private void loadIstoric()
        {
            String queryString = String.Format(@"SELECT * FROM ISTORIC_PACIENT WHERE ID_PACIENT IN (SELECT idPacient FROM PACIENTI WHERE CNP = " + patientLogin.getCNP() + ") ORDER BY ID_PACIENT, ID_CONSULTATIE, ID_DIAGNOSTIC");
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    DataTable dataTable = new DataTable();
                    OracleDataAdapter dataAdapter = new OracleDataAdapter(command);
                    dataAdapter.Fill(dataTable);
                    dataGridViewIstoric.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewIstoric);
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

        private void buttonIstoric_Click(object sender, EventArgs e)
        {
            hidePanels();
            panelIstoric.Show();
            panelIstoric.BringToFront();
            loadIstoric();
        }

        private void buttonTratamente_Click(object sender, EventArgs e)
        {
            hidePanels();
            panelTratamente.Show();
            panelTratamente.BringToFront();
            loadTratamente();
        }
    }
}