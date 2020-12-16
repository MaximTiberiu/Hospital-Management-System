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
    public partial class adminMain : Form
    {
        public adminMain()
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
        private string codJudet = "";

        private void hidePanels()
        {
            panelBoli.Visible = false;
            panelLocalitati.Visible = false;
            panelMedicamente.Visible = false;
            panelPacienti.Visible = false;
            panelDoctori.Visible = false;
            panelSectiiSpitale.Visible = false;
            panelSimptome.Visible = false;
            panelSpitale.Visible = false;
            panelTratamente.Visible = false;
        }

        private void adminMain_Load(object sender, EventArgs e)
        {
            hidePanels();
        }

        private void buttonLocalitati_Click(object sender, EventArgs e)
        {
            hidePanels();
            panelLocalitati.Visible = true;

            String queryString = String.Format(@"SELECT * FROM JUDETE");
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
                        string judet = dataReader["numeJudet"].ToString();
                        codJudet = dataReader["codJudet"].ToString();
                        comboBox1.Items.Add(judet);
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

        private void buttonSpitale_Click(object sender, EventArgs e)
        {
            hidePanels();
            panelSpitale.Visible = true;
        }

        private void buttonSectiiSpitale_Click(object sender, EventArgs e)
        {
            hidePanels();
            panelSectiiSpitale.Visible = true;
        }

        private void buttonDoctori_Click(object sender, EventArgs e)
        {
            hidePanels();
            panelDoctori.Visible = true;
        }

        private void buttonPacient_Click(object sender, EventArgs e)
        {
            hidePanels();
            panelPacienti.Visible = true;
        }

        private void buttonSimptome_Click(object sender, EventArgs e)
        {
            hidePanels();
            panelSimptome.Visible = true;
        }

        private void buttonBoli_Click(object sender, EventArgs e)
        {
            hidePanels();
            panelBoli.Visible = true;
        }

        private void buttonTratamente_Click(object sender, EventArgs e)
        {
            hidePanels();
            panelTratamente.Visible = true;
        }

        private void buttonMedicamente_Click(object sender, EventArgs e)
        {
            hidePanels();
            panelMedicamente.Visible = true;
        }

        void autoSizeDataGridView(DataGridView grd)
        {
            for(int i = 0; i < grd.Columns.Count - 1; i++)
                grd.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd.Columns[grd.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            for (int i = 0; i <= grd.Columns.Count - 1; i++)
            {
                int colw = grd.Columns[i].Width;
                grd.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grd.Columns[i].Width = colw;
            }
        }

        private void displayData()
        {
            String queryString = String.Format(@"SELECT * FROM LOCALITATI WHERE codJudet = " + codJudet + "");
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
                    dataGridView1.DataSource = dataTable;

                    autoSizeDataGridView(dataGridView1);
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                String queryString = String.Format(@"SELECT * FROM JUDETE WHERE numeJudet = '" + comboBox1.SelectedItem.ToString() + "'");
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        OracleDataReader dataReader = command.ExecuteReader();

                        dataReader.Read();
                        codJudet = dataReader["codJudet"].ToString();
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
                displayData();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            labelidLoc.Text = selectedRow.Cells[0].Value.ToString();
            textBoxLocalitate.Text = selectedRow.Cells[1].Value.ToString();
            textBoxcodPostal.Text = selectedRow.Cells[2].Value.ToString();
            textBoxcodJudet.Text = selectedRow.Cells[3].Value.ToString();
        }
    }
}
