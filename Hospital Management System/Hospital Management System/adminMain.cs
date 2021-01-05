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
        private bool addOrUpdate = false; // false -> add, true -> update

        private void hidePanels()
        {
            panelJudet.SendToBack();
            panelJudet.Visible = false;
            panelLocalitati.SendToBack();
            panelLocalitati.Visible = false;
            panelSpitale.SendToBack();
            panelSpitale.Visible = false;
            panelSectiiSpitale.SendToBack();
            panelSectiiSpitale.Visible = false;
            panelSectii.SendToBack();
            panelSectii.Visible = false;
            panelFunctii.SendToBack();
            panelFunctii.Visible = false;
            panelDoctori.SendToBack();
            panelDoctori.Visible = false;
            panelPacienti.SendToBack();
            panelPacienti.Visible = false;
            panelSimptome.SendToBack();
            panelSimptome.Visible = false;
            panelBoli.SendToBack();
            panelBoli.Visible = false;
            panelTratamente.SendToBack();
            panelTratamente.Visible = false;
            panelMedicamente.SendToBack();
            panelMedicamente.Visible = false;
            panelStatistici.SendToBack();
            panelStatistici.Hide();
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

        private void adminMain_Load(object sender, EventArgs e)
        {
            hidePanels();
        }

        #region JUDETE

        private void loadJudete()
        {
            String queryString = String.Format(@"SELECT * FROM JUDETE ORDER BY codJudet ASC");
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
                    dataGridViewJudete.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewJudete);
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

        private void loadComboSortJudete()
        {
            comboBoxColoaneJudete.Items.Clear();
            comboBoxColoaneJudete.Items.Add("codJudet");
            comboBoxColoaneJudete.Items.Add("numeJudet");

            comboBoxOrdineJudete.Items.Clear();
            comboBoxOrdineJudete.Items.Add("Ascendent");
            comboBoxOrdineJudete.Items.Add("Descendent");
        }

        private void buttonJudete_Click(object sender, EventArgs e)
        {
            // initializations
            hidePanels();
            panelJudet.Visible = true;
            panelJudet.BringToFront();
            textBoxnumeJudet.Enabled = false;
            labelcodJudet_print.Text = "";
            pictureBoxCheckJudete.Visible = false;
            pictureBoxCancelJudete.Visible = false;

            // load data
            loadJudete();
            loadComboSortJudete();
        }

        private void dataGridViewJudete_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridViewJudete.Rows[index];
            labelcodJudet_print.Text = selectedRow.Cells[0].Value.ToString();
            textBoxnumeJudet.Text = selectedRow.Cells[1].Value.ToString();
        }

        private void buttonEditeazaJudete_Click(object sender, EventArgs e)
        {
            if (textBoxnumeJudet.Text == "")
            {
                MessageBox.Show("Selectează o intrare!", "Eroare.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                pictureBoxCheckJudete.Visible = true;
                pictureBoxCancelJudete.Visible = true;
                textBoxnumeJudet.Enabled = true;
            }
        }

        private void buttonClearJudete_Click(object sender, EventArgs e)
        {
            labelcodJudet_print.Text = "";
            textBoxnumeJudet.Text = "";
        }

        private void pictureBoxCheckJudete_Click(object sender, EventArgs e)
        {
            String queryString = String.Format(@"UPDATE JUDETE 
                                                SET numeJudet= :newNumeJudet 
                                                 WHERE codJudet = " + labelcodJudet_print.Text);
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    command.Parameters.Add("newNumeJudet", textBoxnumeJudet.Text);
                    int rowsUpdated = command.ExecuteNonQuery();

                    if(rowsUpdated > 0)
                    {
                        MessageBox.Show("Date actualizate cu succes!", "Update.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadJudete();
                        pictureBoxCheckJudete.Visible = false;
                        pictureBoxCancelJudete.Visible = false;
                        textBoxnumeJudet.Enabled = false;
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

        private void pictureBoxCancelJudete_Click(object sender, EventArgs e)
        {
            textBoxnumeJudet.Undo();
            pictureBoxCheckJudete.Visible = false;
            pictureBoxCancelJudete.Visible = false;
            textBoxnumeJudet.Enabled = false;
        }

        private void comboBoxColoaneJudete_SelectedIndexChanged(object sender, EventArgs e)
        {
            String queryString = String.Format(@"SELECT * FROM JUDETE ORDER BY " + comboBoxColoaneJudete.SelectedItem.ToString());
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
                    dataGridViewJudete.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewJudete);
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

        private void comboBoxOrdineJudete_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ord;
            if(comboBoxOrdineJudete.SelectedIndex == 0)
            {
                ord = "ASC";
            }
            else
            {
                ord = "DESC";
            }

            String queryString = String.Format(@"SELECT * FROM JUDETE ORDER BY " + comboBoxColoaneJudete.SelectedItem.ToString() + " " + ord);
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
                    dataGridViewJudete.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewJudete);
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

        #endregion

        #region LOCALITATI

        private void loadLocalitati()
        {
            /*if(checkedListBoxJudete.CheckedItems.Count > 0)
            {
                String queryString = String.Format(@"SELECT * FROM LOCALITATI WHERE codJudet in ");
            }
            else
            {*/
                String queryString = String.Format(@"SELECT * FROM LOCALITATI ORDER BY idLocalitate ASC");
            //}
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
                    dataGridViewLocalitati.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewLocalitati);
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

        private void loadComboSortLocalitati()
        {
            comboBoxColoaneLocalitati.Items.Add("idLocalitate");
            comboBoxColoaneLocalitati.Items.Add("numeLocalitate");
            comboBoxColoaneLocalitati.Items.Add("codPostal");
            comboBoxColoaneLocalitati.Items.Add("codJudet");

            comboBoxOrdineLocalitati.Items.Add("Ascendent");
            comboBoxOrdineLocalitati.Items.Add("Descendent");
        }

        private void buttonLocalitati_Click(object sender, EventArgs e)
        {
            // initializations
            hidePanels();
            panelLocalitati.Visible = true;
            panelLocalitati.BringToFront();
            textBoxnumeLocalitate.Enabled = false;
            labelidLocalitate_print.Text = "";
            labelcodPostal_print.Text = "";
            textBoxcodJudet.Enabled = false;

            pictureBoxCheckLocalitati.Visible = false;
            pictureBoxCancelLocalitati.Visible = false;

            // load data
            loadLocalitati();
            loadComboSortLocalitati();
        }

        private void dataGridViewLocalitati_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridViewLocalitati.Rows[index];
            labelidLocalitate_print.Text = selectedRow.Cells[0].Value.ToString();
            textBoxnumeLocalitate.Text = selectedRow.Cells[1].Value.ToString();
            labelcodPostal_print.Text = selectedRow.Cells[2].Value.ToString();
            textBoxcodJudet.Text = selectedRow.Cells[3].Value.ToString();
        }

        private void buttonEditeazaLocalitati_Click(object sender, EventArgs e)
        {
            addOrUpdate = true;
            if (textBoxnumeLocalitate.Text == "")
            {
                MessageBox.Show("Selectează o intrare!", "Eroare.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                pictureBoxCheckLocalitati.Visible = true;
                pictureBoxCancelLocalitati.Visible = true;
                textBoxnumeLocalitate.Enabled = true;
                textBoxcodJudet.Enabled = true;
            }
        }

        private void buttonClearLocalitati_Click(object sender, EventArgs e)
        {
            labelidLocalitate_print.Text = "";
            textBoxnumeLocalitate.Text = "";
            labelcodPostal_print.Text = "";
            textBoxcodJudet.Text = "";
        }

        private void pictureBoxCheckLocalitati_Click(object sender, EventArgs e)
        {
            if(addOrUpdate == true)
            {
                String queryString = String.Format(@"UPDATE LOCALITATI 
                                                SET numeLocalitate= :newNumeLocalitate,
                                                    codJudet = :newCodJudet
                                                WHERE idLocalitate = " + labelidLocalitate_print.Text);
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newNumeLocalitate", textBoxnumeLocalitate.Text);
                        command.Parameters.Add("newCodJudet", textBoxcodJudet.Text);
                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date actualizate cu succes!", "Update.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadLocalitati();
                            pictureBoxCheckLocalitati.Visible = false;
                            pictureBoxCancelLocalitati.Visible = false;
                            textBoxnumeLocalitate.Enabled = false;
                            textBoxcodJudet.Enabled = false;
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
            else
            {
                String queryString = String.Format(@"INSERT INTO LOCALITATI(idLocalitate, numeLocalitate, codPostal, codJudet)
                                                     VALUES ( :newidLocalitate, :newNumeLocalitate, :newCodPostal, :newCodJudet)");
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newidLocalitate", labelidLocalitate_print.Text);
                        command.Parameters.Add("newNumeLocalitate", textBoxnumeLocalitate.Text);
                        command.Parameters.Add("newCodPostal", labelcodPostal_print.Text);
                        command.Parameters.Add("newCodJudet", textBoxcodJudet.Text);
                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date inserate cu succes!", "Insert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadLocalitati();
                            pictureBoxCheckLocalitati.Visible = false;
                            pictureBoxCancelLocalitati.Visible = false;
                            textBoxnumeLocalitate.Enabled = false;
                            textBoxcodJudet.Enabled = false;
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

        private void pictureBoxCancelLocalitati_Click(object sender, EventArgs e)
        {
            textBoxnumeLocalitate.Undo();
            textBoxcodJudet.Undo();
            pictureBoxCheckLocalitati.Visible = false;
            pictureBoxCancelLocalitati.Visible = false;
            textBoxnumeLocalitate.Enabled = false;
            textBoxcodJudet.Enabled = false;
        }

        private void comboBoxColoaneLocalitati_SelectedIndexChanged(object sender, EventArgs e)
        {
            String queryString = String.Format(@"SELECT * FROM LOCALITATI ORDER BY " + comboBoxColoaneLocalitati.SelectedItem.ToString());
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
                    dataGridViewLocalitati.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewLocalitati);
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

        private void comboBoxOrdineLocalitati_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ord;
            if (comboBoxOrdineLocalitati.SelectedIndex == 0)
            {
                ord = "ASC";
            }
            else
            {
                ord = "DESC";
            }

            String queryString = String.Format(@"SELECT * FROM LOCALITATI ORDER BY " + comboBoxColoaneLocalitati.SelectedItem.ToString() + " " + ord);
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
                    dataGridViewLocalitati.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewLocalitati);
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

        private void buttonStergeLocalitati_Click(object sender, EventArgs e)
        {
            String queryString = String.Format(@"DELETE FROM LOCALITATI
                                                WHERE idLocalitate = " + labelidLocalitate_print.Text);
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {   switch(MessageBox.Show("Ștergerea acestei localități poate duce la ștergerea sau modificarea unor date care au legătură cu respectiva intrare. Continuați?", 
                        "ATENȚIE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                    {
                        case DialogResult.Yes:
                            {
                                command.Connection.Open();
                                int rowsUpdated = command.ExecuteNonQuery();

                                if (rowsUpdated > 0)
                                {
                                    MessageBox.Show("Date șterse cu succes!", "Delete.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    loadLocalitati();
                                    pictureBoxCheckLocalitati.Visible = false;
                                    pictureBoxCancelLocalitati.Visible = false;
                                    textBoxnumeLocalitate.Enabled = false;
                                    textBoxcodJudet.Enabled = false;
                                }
                                break;
                            }
                        case DialogResult.No:
                        case DialogResult.Cancel: { break; }
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

        private void buttonAdaugaLocalitate_Click(object sender, EventArgs e)
        {
            addOrUpdate = false;
            pictureBoxCheckLocalitati.Visible = true;
            pictureBoxCancelLocalitati.Visible = true;
            textBoxnumeLocalitate.Enabled = true;
            textBoxcodJudet.Enabled = true;

            labelidLocalitate_print.Text = "";
            textBoxnumeLocalitate.Text = "";
            labelcodPostal_print.Text = "";
            textBoxcodJudet.Text = "";

            String queryString = String.Format(@"SELECT LOCALITATI_idLocalitate_SEQ.NEXTVAL FROM DUAL");
            StringBuilder errorMessages = new StringBuilder();
            
            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    labelidLocalitate_print.Text = command.ExecuteScalar().ToString();
                    queryString = String.Format(@"SELECT LOCALITATI_codPostal_SEQ.NEXTVAL FROM DUAL");
                    command.CommandText = queryString;
                    labelcodPostal_print.Text = command.ExecuteScalar().ToString();

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


        #endregion

        #region SPITALE
        private void loadSpitale()
        {
            String queryString = String.Format(@"SELECT * FROM SPITALE ORDER BY idSpital ASC");
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
                    dataGridViewSpitale.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewSpitale);
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

        private void loadComboSortSpitale()
        {
            comboBoxColoaneSpitale.Items.Clear();
            comboBoxColoaneSpitale.Items.Add("idSpital");
            comboBoxColoaneSpitale.Items.Add("numeSpital");
            comboBoxColoaneSpitale.Items.Add("idLocalitate");

            comboBoxOrdineSpitale.Items.Clear();
            comboBoxOrdineSpitale.Items.Add("Ascendent");
            comboBoxOrdineSpitale.Items.Add("Descendent");
        }

        private void buttonSpitale_Click(object sender, EventArgs e)
        {
            // initializations
            hidePanels();
            panelSpitale.Visible = true;
            panelSpitale.BringToFront();
            textBoxNumeSpital.Enabled = false;
            textBoxidLocalitate.Enabled = false;
            textBoxStrada.Enabled = false;
            textBoxNumar.Enabled = false;
            textBoxTelefon.Enabled = false;
            textBoxEmail.Enabled = false;
            labelidSpital_print.Text = "";

            pictureBoxCheckSpitale.Visible = false;
            pictureBoxCancelSpitale.Visible = false;

            // load data
            loadSpitale();
            loadComboSortSpitale();
        }

        private void dataGridViewSpitale_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridViewSpitale.Rows[index];
            labelidSpital_print.Text = selectedRow.Cells[0].Value.ToString();
            textBoxNumeSpital.Text = selectedRow.Cells[1].Value.ToString();
            textBoxidLocalitate.Text = selectedRow.Cells[2].Value.ToString();
            textBoxStrada.Text = selectedRow.Cells[3].Value.ToString();
            textBoxNumar.Text = selectedRow.Cells[4].Value.ToString();
            textBoxTelefon.Text = selectedRow.Cells[5].Value.ToString();
            textBoxEmail.Text = selectedRow.Cells[6].Value.ToString();
        }

        private void buttonEditeazaSpitale_Click(object sender, EventArgs e)
        {
            addOrUpdate = true;
            if (textBoxNumeSpital.Text == "")
            {
                MessageBox.Show("Selectează o intrare!", "Eroare.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                pictureBoxCheckSpitale.Visible = true;
                pictureBoxCancelSpitale.Visible = true;
                textBoxNumeSpital.Enabled = true;
                textBoxidLocalitate.Enabled = true;
                textBoxStrada.Enabled = true;
                textBoxNumar.Enabled = true;
                textBoxTelefon.Enabled = true;
                textBoxEmail.Enabled = true;
            }
        }

        private void buttonClearSpitale_Click(object sender, EventArgs e)
        {
            labelidSpital_print.Text = "";
            textBoxNumeSpital.Text = "";
            textBoxidLocalitate.Text = "";
            textBoxStrada.Text = "";
            textBoxNumar.Text = "";
            textBoxTelefon.Text = "";
            textBoxEmail.Text = "";
        }

        private void pictureBoxCheckSpitale_Click(object sender, EventArgs e)
        {
            if (addOrUpdate == true)
            {
                String queryString = String.Format(@"UPDATE SPITALE 
                                                SET numeSpital= :newNumeSpital,
                                                    idLocalitate = :newidLocalitate,
                                                    strada = :newStrada,
                                                    numar = :newNumar,
                                                    telefon = :newNumar,
                                                    email = :newSpital
                                                WHERE idSpital = " + labelidSpital_print.Text);
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newNumeSpital", textBoxNumeSpital.Text);
                        command.Parameters.Add("newidLocalitate", textBoxidLocalitate.Text);
                        command.Parameters.Add("newStrada", textBoxStrada.Text);
                        command.Parameters.Add("newNumar", textBoxNumar.Text);
                        command.Parameters.Add("newTelefon", textBoxTelefon.Text);
                        command.Parameters.Add("newEmail", textBoxEmail.Text);
                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date actualizate cu succes!", "Update.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadSpitale();
                            pictureBoxCheckSpitale.Visible = false;
                            pictureBoxCancelSpitale.Visible = false;
                            textBoxNumeSpital.Enabled = false;
                            textBoxidLocalitate.Enabled = false;
                            textBoxStrada.Enabled = false;
                            textBoxNumar.Enabled = false;
                            textBoxTelefon.Enabled = false;
                            textBoxEmail.Enabled = false;
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
            else
            {
                String queryString = String.Format(@"INSERT INTO SPITALE(idSpital, numeSpital, idLocalitate, strada, numar, telefon, email)
                                                     VALUES ( :newidSpital, :newNumeSpital, :newidLocalitate, :newStrada, :newNumar, :newTelefon, :newEmail)");
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newidSpital", labelidSpital_print.Text);
                        command.Parameters.Add("newNumeSpital", textBoxNumeSpital.Text);
                        command.Parameters.Add("newidLocalitate", textBoxidLocalitate.Text);
                        command.Parameters.Add("newStrada", textBoxStrada.Text);
                        command.Parameters.Add("newNumar", textBoxNumar.Text);
                        command.Parameters.Add("newTelefon", textBoxTelefon.Text);
                        command.Parameters.Add("newEmail", textBoxEmail.Text);
                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date inserate cu succes!", "Insert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadSpitale();
                            pictureBoxCheckSpitale.Visible = false;
                            pictureBoxCancelSpitale.Visible = false;
                            textBoxNumeSpital.Enabled = false;
                            textBoxidLocalitate.Enabled = false;
                            textBoxStrada.Enabled = false;
                            textBoxNumar.Enabled = false;
                            textBoxTelefon.Enabled = false;
                            textBoxEmail.Enabled = false;
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

        private void pictureBoxCancelSpitale_Click(object sender, EventArgs e)
        {
            textBoxNumeSpital.Undo();
            textBoxidLocalitate.Undo();
            textBoxStrada.Undo();
            textBoxNumar.Undo();
            textBoxTelefon.Undo();
            textBoxEmail.Undo();
            pictureBoxCheckSpitale.Visible = false;
            pictureBoxCancelSpitale.Visible = false;
            textBoxNumeSpital.Enabled = false;
            textBoxidLocalitate.Enabled = false;
            textBoxStrada.Enabled = false;
            textBoxNumar.Enabled = false;
            textBoxTelefon.Enabled = false;
            textBoxEmail.Enabled = false;
        }

        private void comboBoxColoaneSpitale_SelectedIndexChanged(object sender, EventArgs e)
        {
            String queryString = String.Format(@"SELECT * FROM SPITALE ORDER BY " + comboBoxColoaneSpitale.SelectedItem.ToString());
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
                    dataGridViewSpitale.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewSpitale);
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

        private void comboBoxOrdineSpitale_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ord;
            if (comboBoxOrdineSpitale.SelectedIndex == 0)
            {
                ord = "ASC";
            }
            else
            {
                ord = "DESC";
            }

            String queryString = String.Format(@"SELECT * FROM SPITALE ORDER BY " + comboBoxColoaneSpitale.SelectedItem.ToString() + " " + ord);
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
                    dataGridViewSpitale.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewSpitale);
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

        private void buttonStergeSpitale_Click(object sender, EventArgs e)
        {
            String queryString = String.Format(@"DELETE FROM SPITALE
                                                WHERE idSpital = " + labelidSpital_print.Text);
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    switch (MessageBox.Show("Ștergerea acestui spital poate duce la ștergerea sau modificarea unor date care au legătură cu respectiva intrare. Continuați?",
                        "ATENȚIE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                    {
                        case DialogResult.Yes:
                            {
                                command.Connection.Open();
                                int rowsUpdated = command.ExecuteNonQuery();

                                if (rowsUpdated > 0)
                                {
                                    MessageBox.Show("Date șterse cu succes!", "Delete.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    loadSpitale();
                                    pictureBoxCheckSpitale.Visible = false;
                                    pictureBoxCancelSpitale.Visible = false;
                                    textBoxNumeSpital.Enabled = false;
                                    textBoxidLocalitate.Enabled = false;
                                    textBoxStrada.Enabled = false;
                                    textBoxNumar.Enabled = false;
                                    textBoxTelefon.Enabled = false;
                                    textBoxEmail.Enabled = false;
                                }
                                break;
                            }
                        case DialogResult.No:
                        case DialogResult.Cancel: { break; }
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

        private void buttonAdaugaSpitale_Click(object sender, EventArgs e)
        {
            addOrUpdate = false;
            pictureBoxCheckSpitale.Visible = true;
            pictureBoxCancelSpitale.Visible = true;
            textBoxNumeSpital.Enabled = true;
            textBoxidLocalitate.Enabled = true;
            textBoxStrada.Enabled = true;
            textBoxNumar.Enabled = true;
            textBoxTelefon.Enabled = true;
            textBoxEmail.Enabled = true;

            labelidSpital_print.Text = "";
            textBoxNumeSpital.Text = "";
            textBoxidLocalitate.Text = "";
            textBoxStrada.Text = "";
            textBoxNumar.Text = "";
            textBoxTelefon.Text = "";
            textBoxEmail.Text = "";

            String queryString = String.Format(@"SELECT SPITALE_idSpital_SEQ.NEXTVAL FROM DUAL");
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    labelidSpital_print.Text = command.ExecuteScalar().ToString();

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
        #endregion

        #region SECTII SPITALE
        private void loadSectiiSpitale()
        {
            String queryString = String.Format(@"SELECT * FROM SECTII_SPITALE ORDER BY idSectie ASC");
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
                    dataGridViewSectiiSpitale.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewSectiiSpitale);
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

        private void loadComboSortSectiiSpitale()
        {
            comboBoxColoaneSectiiSpitale.Items.Clear();
            comboBoxColoaneSectiiSpitale.Items.Add("idSectie");
            comboBoxColoaneSectiiSpitale.Items.Add("idSpital");
            comboBoxColoaneSectiiSpitale.Items.Add("codSectie");

            comboBoxOrdineSectiiSpitale.Items.Clear();
            comboBoxOrdineSectiiSpitale.Items.Add("Ascendent");
            comboBoxOrdineSectiiSpitale.Items.Add("Descendent");
        }

        private void buttonSectiiSpitale_Click(object sender, EventArgs e)
        {
            // initializations
            hidePanels();
            panelSectiiSpitale.Visible = true;
            panelSectiiSpitale.BringToFront();
            textBoxidSpital.Enabled = false;
            textBoxcodSectie.Enabled = false;
            textBoxCorp.Enabled = false;
            textBoxEtaj.Enabled = false;
            textBoxTelefonSectie.Enabled = false;
            textBoxEmailSectie.Enabled = false;
            labelidSectie_print.Text = "";

            pictureBoxCheckSectiiSpitale.Visible = false;
            pictureBoxCancelSectiiSpitale.Visible = false;

            // load data
            loadSectiiSpitale();
            loadComboSortSectiiSpitale();
        }

        private void dataGridViewSectiiSpitale_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridViewSectiiSpitale.Rows[index];
            labelidSectie_print.Text = selectedRow.Cells[0].Value.ToString();
            textBoxidSpital.Text = selectedRow.Cells[1].Value.ToString();
            textBoxcodSectie.Text = selectedRow.Cells[2].Value.ToString();
            
            if(selectedRow.Cells[3].Value == null)
                textBoxCorp.Text = "";
            else
                textBoxCorp.Text = selectedRow.Cells[3].Value.ToString();
            textBoxEtaj.Text = selectedRow.Cells[4].Value.ToString();

            if (selectedRow.Cells[5].Value == null)
                textBoxTelefonSectie.Text = "";
            else
                textBoxTelefonSectie.Text = selectedRow.Cells[5].Value.ToString();

            if (selectedRow.Cells[6].Value == null)
                textBoxEmailSectie.Text = "";
            else
                textBoxEmailSectie.Text = selectedRow.Cells[6].Value.ToString();
        }

        private void buttonEditeazaSectiiSpitale_Click(object sender, EventArgs e)
        {
            addOrUpdate = true;
            if (textBoxidSpital.Text == "")
            {
                MessageBox.Show("Selectează o intrare!", "Eroare.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                pictureBoxCheckSectiiSpitale.Visible = true;
                pictureBoxCancelSectiiSpitale.Visible = true;
                textBoxidSpital.Enabled = true;
                textBoxcodSectie.Enabled = true;
                textBoxCorp.Enabled = true;
                textBoxEtaj.Enabled = true;
                textBoxTelefonSectie.Enabled = true;
                textBoxEmailSectie.Enabled = true;
            }
        }

        private void buttonClearSectiiSpitale_Click(object sender, EventArgs e)
        {
            labelidSectie_print.Text = "";
            textBoxidSpital.Text = "";
            textBoxcodSectie.Text = "";
            textBoxCorp.Text = "";
            textBoxEtaj.Text = "";
            textBoxTelefonSectie.Text = "";
            textBoxEmailSectie.Text = "";
        }

        private void pictureBoxCheckSectiiSpitale_Click(object sender, EventArgs e)
        {
            if (addOrUpdate == true)
            {
                String queryString = String.Format(@"UPDATE SECTII_SPITALE 
                                                    SET idSpital= :newidSpital,
                                                        codSectie = :newCodSectie,
                                                        corp = :newCorp,
                                                        etaj = :newEtaj,
                                                        telefon = :newTelefon,
                                                        email = :newSpital
                                                    WHERE idSectie = " + labelidSectie_print.Text);
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newidSpital", textBoxidSpital.Text);
                        command.Parameters.Add("newCodSectie", textBoxcodSectie.Text);
                        
                        if (textBoxCorp.Text == "")
                            command.Parameters.Add("newCorp", null);
                        else
                            command.Parameters.Add("newCorp", textBoxCorp.Text);
                        command.Parameters.Add("newEtaj", textBoxEtaj.Text);

                        if (textBoxTelefonSectie.Text == "")
                            command.Parameters.Add("newTelefon", null);
                        else
                            command.Parameters.Add("newTelefon", textBoxTelefonSectie.Text);

                        if (textBoxEmailSectie.Text == "")
                            command.Parameters.Add("newEmail", null);
                        else
                            command.Parameters.Add("newEmail", textBoxEmailSectie.Text);
                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date actualizate cu succes!", "Update.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadSectiiSpitale();
                            pictureBoxCheckSectiiSpitale.Visible = false;
                            pictureBoxCancelSectiiSpitale.Visible = false;
                            textBoxidSpital.Enabled = false;
                            textBoxcodSectie.Enabled = false;
                            textBoxCorp.Enabled = false;
                            textBoxEtaj.Enabled = false;
                            textBoxTelefonSectie.Enabled = false;
                            textBoxEmailSectie.Enabled = false;
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
            else
            {
                String queryString = String.Format(@"INSERT INTO SECTII_SPITALE(idSectie, idSpital, codSectie, corp, etaj, telefon, email)
                                                     VALUES ( :newidSectie, :newidSpital, :newCodSectie, :newCorp, :newEtaj, :newTelefon, :newEmail)");
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newidSectie", labelidSectie_print.Text);
                        command.Parameters.Add("newidSpital", textBoxidSpital.Text);
                        command.Parameters.Add("newCodSectie", textBoxcodSectie.Text);
                        
                        if(textBoxCorp.Text == "")
                            command.Parameters.Add("newCorp", null);
                        else
                            command.Parameters.Add("newCorp", textBoxCorp.Text);
                        command.Parameters.Add("newEtaj", textBoxEtaj.Text);
                        
                        if(textBoxTelefonSectie.Text == "")
                            command.Parameters.Add("newTelefon", null);
                        else
                            command.Parameters.Add("newTelefon", textBoxTelefonSectie.Text);

                        if(textBoxEmailSectie.Text == "")
                            command.Parameters.Add("newEmail", null);
                        else
                            command.Parameters.Add("newEmail", textBoxEmailSectie.Text);
                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date inserate cu succes!", "Insert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadSectiiSpitale();
                            pictureBoxCheckSectiiSpitale.Visible = false;
                            pictureBoxCancelSectiiSpitale.Visible = false;
                            textBoxidSpital.Enabled = false;
                            textBoxcodSectie.Enabled = false;
                            textBoxCorp.Enabled = false;
                            textBoxEtaj.Enabled = false;
                            textBoxTelefonSectie.Enabled = false;
                            textBoxEmailSectie.Enabled = false;
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

        private void pictureBoxCancelSectiiSpitale_Click(object sender, EventArgs e)
        {
            textBoxidSpital.Undo();
            textBoxcodSectie.Undo();
            textBoxCorp.Undo();
            textBoxEtaj.Undo();
            textBoxTelefonSectie.Undo();
            textBoxEmailSectie.Undo();

            pictureBoxCheckSectiiSpitale.Visible = false;
            pictureBoxCancelSectiiSpitale.Visible = false;
            textBoxidSpital.Enabled = false;
            textBoxcodSectie.Enabled = false;
            textBoxCorp.Enabled = false;
            textBoxEtaj.Enabled = false;
            textBoxTelefonSectie.Enabled = false;
            textBoxEmailSectie.Enabled = false;
        }

        private void comboBoxColoaneSectiiSpitale_SelectedIndexChanged(object sender, EventArgs e)
        {
            String queryString = String.Format(@"SELECT * FROM SECTII_SPITALE ORDER BY " + comboBoxColoaneSectiiSpitale.SelectedItem.ToString());
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
                    dataGridViewSectiiSpitale.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewSectiiSpitale);
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

        private void comboBoxOrdineSectiiSpitale_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ord;
            if (comboBoxOrdineSectiiSpitale.SelectedIndex == 0)
            {
                ord = "ASC";
            }
            else
            {
                ord = "DESC";
            }

            String queryString = String.Format(@"SELECT * FROM SECTII_SPITALE ORDER BY " + comboBoxColoaneSectiiSpitale.SelectedItem.ToString() + " " + ord);
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
                    dataGridViewSectiiSpitale.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewSectiiSpitale);
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

        private void buttonStergeSectiiSpitale_Click(object sender, EventArgs e)
        {
            String queryString = String.Format(@"DELETE FROM SECTII_SPITALE
                                                WHERE idSectie = " + labelidSectie_print.Text);
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    switch (MessageBox.Show("Ștergerea acestei sectii poate duce la ștergerea sau modificarea unor date care au legătură cu respectiva intrare. Continuați?",
                        "ATENȚIE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                    {
                        case DialogResult.Yes:
                            {
                                command.Connection.Open();
                                int rowsUpdated = command.ExecuteNonQuery();

                                if (rowsUpdated > 0)
                                {
                                    MessageBox.Show("Date șterse cu succes!", "Delete.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    loadSectiiSpitale();
                                    pictureBoxCheckSectiiSpitale.Visible = false;
                                    pictureBoxCancelSectiiSpitale.Visible = false;
                                    textBoxidSpital.Enabled = false;
                                    textBoxcodSectie.Enabled = false;
                                    textBoxCorp.Enabled = false;
                                    textBoxEtaj.Enabled = false;
                                    textBoxTelefonSectie.Enabled = false;
                                    textBoxEmailSectie.Enabled = false;

                                    labelidSectie_print.Text = "";
                                    textBoxidSpital.Text = "";
                                    textBoxcodSectie.Text = "";
                                    textBoxCorp.Text = "";
                                    textBoxEtaj.Text = "";
                                    textBoxTelefonSectie.Text = "";
                                    textBoxEmailSectie.Text = "";
                                }
                                break;
                            }
                        case DialogResult.No:
                        case DialogResult.Cancel: { break; }
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

        private void buttonAdaugaSectiiSpitale_Click(object sender, EventArgs e)
        {
            addOrUpdate = false;
            pictureBoxCheckSectiiSpitale.Visible = true;
            pictureBoxCancelSectiiSpitale.Visible = true;
            textBoxidSpital.Enabled = true;
            textBoxcodSectie.Enabled = true;
            textBoxCorp.Enabled = true;
            textBoxEtaj.Enabled = true;
            textBoxTelefonSectie.Enabled = true;
            textBoxEmailSectie.Enabled = true;

            labelidSectie_print.Text = "";
            textBoxidSpital.Text = "";
            textBoxcodSectie.Text = "";
            textBoxCorp.Text = "";
            textBoxEtaj.Text = "";
            textBoxTelefonSectie.Text = "";
            textBoxEmailSectie.Text = "";

            String queryString = String.Format(@"SELECT SECTII_SPITALE_idSectie_SEQ.NEXTVAL FROM DUAL");
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    labelidSectie_print.Text = command.ExecuteScalar().ToString();

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
        #endregion

        #region SECTII
        private void loadSectii()
        {
            String queryString = String.Format(@"SELECT * FROM SECTII ORDER BY codSectie ASC");
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
                    dataGridViewSectii.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewSectii);
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

        private void loadComboSortSectii()
        {
            comboBoxColoaneSectii.Items.Clear();
            comboBoxColoaneSectii.Items.Add("codSectie");
            comboBoxColoaneSectii.Items.Add("numeSectie");

            comboBoxOrdineSectii.Items.Clear();
            comboBoxOrdineSectii.Items.Add("Ascendent");
            comboBoxOrdineSectii.Items.Add("Descendent");
        }

        private void buttonSectii_Click(object sender, EventArgs e)
        {
            // initializations
            hidePanels();
            panelSectii.Visible = true;
            panelSectii.BringToFront();
            textBoxnumeSectie.Enabled = false;
            labelcodSectie_print.Text = "";

            pictureBoxCheckSectii.Visible = false;
            pictureBoxCancelSectii.Visible = false;

            // load data
            loadSectii();
            loadComboSortSectii();
        }

        private void dataGridViewSectii_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridViewSectii.Rows[index];
            labelcodSectie_print.Text = selectedRow.Cells[0].Value.ToString();
            textBoxnumeSectie.Text = selectedRow.Cells[1].Value.ToString();
        }

        private void buttonEditeazaSectii_Click(object sender, EventArgs e)
        {
            addOrUpdate = true;
            if (textBoxnumeSectie.Text == "")
            {
                MessageBox.Show("Selectează o intrare!", "Eroare.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                pictureBoxCheckSectii.Visible = true;
                pictureBoxCancelSectii.Visible = true;
                textBoxnumeSectie.Enabled = true;
            }
        }

        private void buttonClearSectii_Click(object sender, EventArgs e)
        {
            labelcodSectie_print.Text = "";
            textBoxnumeSectie.Text = "";
        }

        private void pictureBoxCheckSectii_Click(object sender, EventArgs e)
        {
            if (addOrUpdate == true)
            {
                String queryString = String.Format(@"UPDATE SECTII
                                                    SET numeSectie = :newNumeSectie
                                                    WHERE codSectie = " + labelcodSectie_print.Text);
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newNumeSectie", textBoxnumeSectie.Text);

                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date actualizate cu succes!", "Update.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadSectii();
                            pictureBoxCheckSectii.Visible = false;
                            pictureBoxCancelSectii.Visible = false;
                            textBoxnumeSectie.Enabled = false;
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
            else
            {
                String queryString = String.Format(@"INSERT INTO SECTII(codSectie, numeSectie)
                                                     VALUES ( :newcodSectie, :newNumeSectie)");
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newcodSectie", labelcodSectie_print.Text);
                        command.Parameters.Add("newNumeSectie", textBoxnumeSectie.Text);

                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date inserate cu succes!", "Insert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadSectii();
                            pictureBoxCheckSectii.Visible = false;
                            pictureBoxCancelSectii.Visible = false;
                            textBoxnumeSectie.Enabled = false;
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

        private void pictureBoxCancelSectii_Click(object sender, EventArgs e)
        {
            textBoxnumeSectie.Undo();

            pictureBoxCheckSectii.Visible = false;
            pictureBoxCancelSectii.Visible = false;
            textBoxnumeSectie.Enabled = false;
        }

        private void comboBoxColoaneSectii_SelectedIndexChanged(object sender, EventArgs e)
        {
            String queryString = String.Format(@"SELECT * FROM SECTII ORDER BY " + comboBoxColoaneSectii.SelectedItem.ToString());
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
                    dataGridViewSectii.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewSectii);
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

        private void comboBoxOrdineSectii_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ord;
            if (comboBoxOrdineSectii.SelectedIndex == 0)
            {
                ord = "ASC";
            }
            else
            {
                ord = "DESC";
            }

            String queryString = String.Format(@"SELECT * FROM SECTII ORDER BY " + comboBoxColoaneSectii.SelectedItem.ToString() + " " + ord);
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
                    dataGridViewSectii.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewSectii);
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

        private void buttonStergeSectii_Click(object sender, EventArgs e)
        {
            String queryString = String.Format(@"DELETE FROM SECTII
                                                WHERE codSectie = " + labelcodSectie_print.Text);
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    switch (MessageBox.Show("Ștergerea acestei sectii poate duce la ștergerea sau modificarea unor date care au legătură cu respectiva intrare. Continuați?",
                        "ATENȚIE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                    {
                        case DialogResult.Yes:
                            {
                                command.Connection.Open();
                                int rowsUpdated = command.ExecuteNonQuery();

                                if (rowsUpdated > 0)
                                {
                                    MessageBox.Show("Date șterse cu succes!", "Delete.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    loadSectii();
                                    pictureBoxCheckSectii.Visible = false;
                                    pictureBoxCancelSectii.Visible = false;
                                    textBoxnumeSectie.Enabled = false;

                                    labelcodSectie_print.Text = "";
                                    textBoxnumeSectie.Text = "";
                                }
                                break;
                            }
                        case DialogResult.No:
                        case DialogResult.Cancel: { break; }
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

        private void buttonAdaugaSectii_Click(object sender, EventArgs e)
        {
            addOrUpdate = false;
            pictureBoxCheckSectii.Visible = true;
            pictureBoxCancelSectii.Visible = true;
            textBoxnumeSectie.Enabled = true;

            labelcodSectie_print.Text = "";
            textBoxnumeSectie.Text = "";

            String queryString = String.Format(@"SELECT SECTII_codSectie_SEQ.NEXTVAL FROM DUAL");
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    labelcodSectie_print.Text = command.ExecuteScalar().ToString();

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
        #endregion

        #region FUNCTII
        private void loadFunctii()
        {
            String queryString = String.Format(@"SELECT * FROM FUNCTII ORDER BY codFunctie ASC");
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
                    dataGridViewFunctii.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewFunctii);
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

        private void loadComboSortFunctii()
        {
            comboBoxColoaneFunctii.Items.Clear();
            comboBoxColoaneFunctii.Items.Add("codFunctie");
            comboBoxColoaneFunctii.Items.Add("numeFunctie");

            comboBoxOrdineFunctii.Items.Clear();
            comboBoxOrdineFunctii.Items.Add("Ascendent");
            comboBoxOrdineFunctii.Items.Add("Descendent");
        }

        private void buttonFunctii_Click(object sender, EventArgs e)
        {
            // initializations
            hidePanels();
            panelFunctii.Visible = true;
            panelFunctii.BringToFront();
            textBoxnumeFunctie.Enabled = false;
            labelcodFunctie_print.Text = "";

            pictureBoxCheckFunctii.Visible = false;
            pictureBoxCancelFunctii.Visible = false;

            // load data
            loadFunctii();
            loadComboSortFunctii();
        }

        private void dataGridViewFunctii_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridViewFunctii.Rows[index];
            labelcodFunctie_print.Text = selectedRow.Cells[0].Value.ToString();
            textBoxnumeFunctie.Text = selectedRow.Cells[1].Value.ToString();
        }

        private void buttonEditeazaFunctii_Click(object sender, EventArgs e)
        {
            addOrUpdate = true;
            if (textBoxnumeFunctie.Text == "")
            {
                MessageBox.Show("Selectează o intrare!", "Eroare.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                pictureBoxCheckFunctii.Visible = true;
                pictureBoxCancelFunctii.Visible = true;
                textBoxnumeFunctie.Enabled = true;
            }
        }

        private void buttonClearFunctii_Click(object sender, EventArgs e)
        {
            labelcodFunctie_print.Text = "";
            textBoxnumeFunctie.Text = "";
        }

        private void pictureBoxCheckFunctii_Click(object sender, EventArgs e)
        {
            if (addOrUpdate == true)
            {
                String queryString = String.Format(@"UPDATE FUNCTII
                                                    SET numeFunctie = :newNumeFunctie
                                                    WHERE codFunctie = " + labelcodFunctie_print.Text);
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newNumeFunctie", textBoxnumeFunctie.Text);

                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date actualizate cu succes!", "Update.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadFunctii();
                            pictureBoxCheckFunctii.Visible = false;
                            pictureBoxCancelFunctii.Visible = false;
                            textBoxnumeFunctie.Enabled = false;
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
            else
            {
                String queryString = String.Format(@"INSERT INTO FUNCTII(codFunctie, numeFunctie)
                                                     VALUES ( :newcodFunctie, :newNumeFunctie)");
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newcodFunctie", labelcodFunctie_print.Text);
                        command.Parameters.Add("newNumeFunctie", textBoxnumeFunctie.Text);

                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date inserate cu succes!", "Insert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadFunctii();
                            pictureBoxCheckFunctii.Visible = false;
                            pictureBoxCancelFunctii.Visible = false;
                            textBoxnumeFunctie.Enabled = false;
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

        private void pictureBoxCancelFunctii_Click(object sender, EventArgs e)
        {
            textBoxnumeFunctie.Undo();

            pictureBoxCheckFunctii.Visible = false;
            pictureBoxCancelFunctii.Visible = false;
            textBoxnumeFunctie.Enabled = false;
        }

        private void comboBoxColoaneFunctii_SelectedIndexChanged(object sender, EventArgs e)
        {
            String queryString = String.Format(@"SELECT * FROM FUNCTII ORDER BY " + comboBoxColoaneFunctii.SelectedItem.ToString());
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
                    dataGridViewFunctii.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewFunctii);
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

        private void comboBoxOrdineFunctii_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ord;
            if (comboBoxOrdineFunctii.SelectedIndex == 0)
            {
                ord = "ASC";
            }
            else
            {
                ord = "DESC";
            }

            String queryString = String.Format(@"SELECT * FROM FUNCTII ORDER BY " + comboBoxColoaneFunctii.SelectedItem.ToString() + " " + ord);
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
                    dataGridViewFunctii.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewFunctii);
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

        private void buttonStergeFunctii_Click(object sender, EventArgs e)
        {
            String queryString = String.Format(@"DELETE FROM FUNCTII
                                                WHERE codFunctie = " + labelcodFunctie_print.Text);
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    switch (MessageBox.Show("Ștergerea acestei funcții poate duce la ștergerea sau modificarea unor date care au legătură cu respectiva intrare. Continuați?",
                        "ATENȚIE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                    {
                        case DialogResult.Yes:
                            {
                                command.Connection.Open();
                                int rowsUpdated = command.ExecuteNonQuery();

                                if (rowsUpdated > 0)
                                {
                                    MessageBox.Show("Date șterse cu succes!", "Delete.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    loadFunctii();
                                    pictureBoxCheckFunctii.Visible = false;
                                    pictureBoxCancelFunctii.Visible = false;
                                    textBoxnumeFunctie.Enabled = false;

                                    labelcodFunctie_print.Text = "";
                                    textBoxnumeFunctie.Text = "";
                                }
                                break;
                            }
                        case DialogResult.No:
                        case DialogResult.Cancel: { break; }
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

        private void buttonAdaugaFunctii_Click(object sender, EventArgs e)
        {
            addOrUpdate = false;
            pictureBoxCheckFunctii.Visible = true;
            pictureBoxCancelFunctii.Visible = true;
            textBoxnumeFunctie.Enabled = true;

            labelcodFunctie_print.Text = "";
            textBoxnumeFunctie.Text = "";

            String queryString = String.Format(@"SELECT FUNCTII_codFunctie_SEQ.NEXTVAL FROM DUAL");
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    labelcodFunctie_print.Text = command.ExecuteScalar().ToString();

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
        #endregion

        #region DOCTORI
        private void loadDoctori()
        {
            String queryString = String.Format(@"SELECT * FROM DOCTORI ORDER BY codParafa ASC");
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
                    dataGridViewDoctori.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewDoctori);
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

        private void loadComboSortDoctori()
        {
            comboBoxColoaneDoctori.Items.Clear();
            comboBoxColoaneDoctori.Items.Add("codParafa");
            comboBoxColoaneDoctori.Items.Add("Nume");
            comboBoxColoaneDoctori.Items.Add("codFunctie");
            comboBoxColoaneDoctori.Items.Add("idSectie");

            comboBoxOrdineDoctori.Items.Clear();
            comboBoxOrdineDoctori.Items.Add("Ascendent");
            comboBoxOrdineDoctori.Items.Add("Descendent");
        }

        private void buttonDoctori_Click(object sender, EventArgs e)
        {
            // initializations
            hidePanels();
            panelDoctori.Visible = true;
            panelDoctori.BringToFront();
            textBoxNume.Enabled = false;
            textBoxPrenume.Enabled = false;
            textBoxCNP.Enabled = false;
            textBoxTelefonDoctor.Enabled = false;
            textBoxEmailDoctor.Enabled = false;
            textBoxcodFunctie.Enabled = false;
            textBoxidSectie.Enabled = false;
            labelcodParafa_print.Text = "";

            pictureBoxCheckDoctori.Visible = false;
            pictureBoxCancelDoctori.Visible = false;

            // load data
            loadDoctori();
            loadComboSortDoctori();
        }

        private void dataGridViewDoctori_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridViewDoctori.Rows[index];
            labelcodParafa_print.Text = selectedRow.Cells[0].Value.ToString();
            textBoxNume.Text = selectedRow.Cells[1].Value.ToString();
            textBoxPrenume.Text = selectedRow.Cells[2].Value.ToString();
            textBoxCNP.Text = selectedRow.Cells[3].Value.ToString();
            textBoxTelefonDoctor.Text = selectedRow.Cells[4].Value.ToString();
            textBoxEmailDoctor.Text = selectedRow.Cells[5].Value.ToString();
            textBoxcodFunctie.Text = selectedRow.Cells[6].Value.ToString();
            textBoxidSectie.Text = selectedRow.Cells[7].Value.ToString();
        }

        private void buttonEditeazaDoctori_Click(object sender, EventArgs e)
        {
            addOrUpdate = true;
            if (textBoxNume.Text == "")
            {
                MessageBox.Show("Selectează o intrare!", "Eroare.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                pictureBoxCheckDoctori.Visible = true;
                pictureBoxCancelDoctori.Visible = true;
                textBoxNume.Enabled = true;
                textBoxPrenume.Enabled = true;
                textBoxCNP.Enabled = true;
                textBoxTelefonDoctor.Enabled = true;
                textBoxEmailDoctor.Enabled = true;
                textBoxcodFunctie.Enabled = true;
                textBoxidSectie.Enabled = true;
            }
        }

        private void buttonClearDoctori_Click(object sender, EventArgs e)
        {
            labelcodParafa_print.Text = "";
            textBoxNume.Text = "";
            textBoxPrenume.Text = "";
            textBoxCNP.Text = "";
            textBoxTelefonDoctor.Text = "";
            textBoxEmailDoctor.Text = "";
            textBoxcodFunctie.Text = "";
            textBoxidSectie.Text = "";
        }

        private void pictureBoxCheckDoctori_Click(object sender, EventArgs e)
        {
            if (addOrUpdate == true)
            {
                String queryString = String.Format(@"UPDATE DOCTORI 
                                                SET nume = :newNume,
                                                    prenume = :newPrenume,
                                                    CNP = :newCNP,
                                                    telefon = :newTelefon,
                                                    email = :newEmail,
                                                    codFunctie = :newCodFunctie,
                                                    idSectie = :newidSectie
                                                WHERE codParafa = " + labelcodParafa_print.Text);
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newNume", textBoxNume.Text);
                        command.Parameters.Add("newPrenume", textBoxPrenume.Text);
                        command.Parameters.Add("newCNP", textBoxCNP.Text);
                        command.Parameters.Add("newTelefon", textBoxTelefonDoctor.Text);
                        command.Parameters.Add("newEmail", textBoxEmailDoctor.Text);
                        command.Parameters.Add("newCodFunctie", textBoxcodFunctie.Text);
                        command.Parameters.Add("newidSectie", textBoxidSectie.Text);
                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date actualizate cu succes!", "Update.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadDoctori();
                            pictureBoxCheckDoctori.Visible = false;
                            pictureBoxCancelDoctori.Visible = false;
                            textBoxNume.Enabled = false;
                            textBoxPrenume.Enabled = false;
                            textBoxCNP.Enabled = false;
                            textBoxTelefonDoctor.Enabled = false;
                            textBoxEmailDoctor.Enabled = false;
                            textBoxcodFunctie.Enabled = false;
                            textBoxidSectie.Enabled = false;
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
            else
            {
                String queryString = String.Format(@"INSERT INTO DOCTORI(codParafa, nume, prenume, CNP, telefon, email, codFunctie, idSectie)
                                                     VALUES ( :newcodParafa, :newNume, :newPrenume, :newCNP, :newTelefon, :newEmail, :newCodFunctie, :newidSpital)");
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newcodParafa", labelcodParafa_print.Text);
                        command.Parameters.Add("newNume", textBoxNume.Text);
                        command.Parameters.Add("newPrenume", textBoxPrenume.Text);
                        command.Parameters.Add("newCNP", textBoxCNP.Text);
                        command.Parameters.Add("newTelefon", textBoxTelefonDoctor.Text);
                        command.Parameters.Add("newEmail", textBoxEmailDoctor.Text);
                        command.Parameters.Add("newCodFunctie", textBoxcodFunctie.Text);
                        command.Parameters.Add("newidSpital", textBoxidSpital.Text);
                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date inserate cu succes!", "Insert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadDoctori();
                            pictureBoxCheckDoctori.Visible = false;
                            pictureBoxCancelDoctori.Visible = false;
                            textBoxNume.Enabled = false;
                            textBoxPrenume.Enabled = false;
                            textBoxCNP.Enabled = false;
                            textBoxTelefonDoctor.Enabled = false;
                            textBoxEmailDoctor.Enabled = false;
                            textBoxcodFunctie.Enabled = false;
                            textBoxidSectie.Enabled = false;
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

        private void pictureBoxCancelDoctori_Click(object sender, EventArgs e)
        {
            textBoxNume.Undo();
            textBoxPrenume.Undo();
            textBoxCNP.Undo();
            textBoxTelefonDoctor.Undo();
            textBoxEmailDoctor.Undo();
            textBoxcodFunctie.Undo();
            textBoxidSectie.Undo();
            pictureBoxCheckDoctori.Visible = false;
            pictureBoxCancelDoctori.Visible = false;
            textBoxNume.Enabled = false;
            textBoxPrenume.Enabled = false;
            textBoxCNP.Enabled = false;
            textBoxTelefonDoctor.Enabled = false;
            textBoxEmailDoctor.Enabled = false;
            textBoxcodFunctie.Enabled = false;
            textBoxidSectie.Enabled = false;
        }

        private void comboBoxColoaneDoctori_SelectedIndexChanged(object sender, EventArgs e)
        {
            String queryString = String.Format(@"SELECT * FROM DOCTORI ORDER BY " + comboBoxColoaneDoctori.SelectedItem.ToString());
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
                    dataGridViewDoctori.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewDoctori);
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

        private void comboBoxOrdineDoctori_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ord;
            if (comboBoxOrdineDoctori.SelectedIndex == 0)
            {
                ord = "ASC";
            }
            else
            {
                ord = "DESC";
            }

            String queryString = String.Format(@"SELECT * FROM DOCTORI ORDER BY " + comboBoxColoaneDoctori.SelectedItem.ToString() + " " + ord);
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
                    dataGridViewDoctori.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewDoctori);
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

        private void buttonStergeDoctori_Click(object sender, EventArgs e)
        {
            String queryString = String.Format(@"DELETE FROM DOCTORI
                                                WHERE codParafa = " + labelcodParafa_print.Text);
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    switch (MessageBox.Show("Ștergerea acestui doctor poate duce la ștergerea sau modificarea unor date care au legătură cu respectiva intrare. Continuați?",
                        "ATENȚIE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                    {
                        case DialogResult.Yes:
                            {
                                command.Connection.Open();
                                int rowsUpdated = command.ExecuteNonQuery();

                                if (rowsUpdated > 0)
                                {
                                    MessageBox.Show("Date șterse cu succes!", "Delete.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    loadDoctori();
                                    pictureBoxCheckDoctori.Visible = false;
                                    pictureBoxCancelDoctori.Visible = false;
                                    textBoxNume.Enabled = false;
                                    textBoxPrenume.Enabled = false;
                                    textBoxCNP.Enabled = false;
                                    textBoxTelefonDoctor.Enabled = false;
                                    textBoxEmailDoctor.Enabled = false;
                                    textBoxcodFunctie.Enabled = false;
                                    textBoxidSectie.Enabled = false;
                                }
                                break;
                            }
                        case DialogResult.No:
                        case DialogResult.Cancel: { break; }
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
        #endregion

        #region PACIENTI
        private void loadPacienti()
        {
            String queryString = String.Format(@"SELECT * FROM PACIENTI ORDER BY idPacient ASC");
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
                    dataGridViewPacienti.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewPacienti);
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

        private void loadComboSortPacienti()
        {
            comboBoxColoanePacienti.Items.Clear();
            comboBoxColoanePacienti.Items.Add("idPacient");
            comboBoxColoanePacienti.Items.Add("Nume");

            comboBoxOrdinePacienti.Items.Clear();
            comboBoxOrdinePacienti.Items.Add("Ascendent");
            comboBoxOrdinePacienti.Items.Add("Descendent");
        }

        private void buttonPacienti_Click(object sender, EventArgs e)
        {
            // initializations
            hidePanels();
            panelPacienti.Visible = true;
            panelPacienti.BringToFront();
            textBoxNumePacient.Enabled = false;
            textBoxPrenumePacient.Enabled = false;
            textBoxCNPPacient.Enabled = false;
            textBoxStradaPacient.Enabled = false;
            textBoxNumarPacient.Enabled = false;
            textBoxLocalitatePacient.Enabled = false;
            textBoxTelefonPacient.Enabled = false;
            textBoxEmailPacient.Enabled = false;
            checkBoxAsigurat.Enabled = false;
            dateTimePickerInternarePacienti.Enabled = false;
            dateTimePickerExternarePacienti.Enabled = false;
            labelidPacient_print.Text = "";

            pictureBoxCheckPacienti.Visible = false;
            pictureBoxCancelPacienti.Visible = false;

            // load data
            loadPacienti();
            loadComboSortPacienti();
        }

        private void dataGridViewPacienti_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridViewPacienti.Rows[index];
            labelidPacient_print.Text = selectedRow.Cells[0].Value.ToString();
            textBoxNumePacient.Text = selectedRow.Cells[1].Value.ToString();
            textBoxPrenumePacient.Text = selectedRow.Cells[2].Value.ToString();
            textBoxCNPPacient.Text = selectedRow.Cells[3].Value.ToString();
            textBoxStradaPacient.Text = selectedRow.Cells[4].Value.ToString();
            textBoxNumarPacient.Text = selectedRow.Cells[5].Value.ToString();
            textBoxLocalitatePacient.Text = selectedRow.Cells[6].Value.ToString();
            textBoxTelefonPacient.Text = selectedRow.Cells[7].Value.ToString();
            textBoxEmailPacient.Text = selectedRow.Cells[8].Value.ToString();

            if (int.Parse(selectedRow.Cells[9].Value.ToString()) == 1)
                checkBoxAsigurat.Checked = true;
            else
                checkBoxAsigurat.Checked = false;

            dateTimePickerInternarePacienti.Value = Convert.ToDateTime(selectedRow.Cells[10].Value);

            if (selectedRow.Cells[11].Value != DBNull.Value)
            {
                dateTimePickerExternarePacienti.Visible = true;
                dateTimePickerExternarePacienti.Value = Convert.ToDateTime(selectedRow.Cells[11].Value);
            }
            else
                dateTimePickerExternarePacienti.Visible = false;
                
        }

        private void buttonClearPacienti_Click(object sender, EventArgs e)
        {
            labelidPacient_print.Text = "";
            textBoxNumePacient.Text = "";
            textBoxPrenumePacient.Text = "";
            textBoxCNPPacient.Text = "";
            textBoxTelefonPacient.Text = "";
            textBoxEmailPacient.Text = "";
            textBoxStradaPacient.Text = "";
            textBoxNumarPacient.Text = "";
            textBoxLocalitatePacient.Text = "";
        }

        private void comboBoxColoanePacienti_SelectedIndexChanged(object sender, EventArgs e)
        {
            String queryString = String.Format(@"SELECT * FROM PACIENTI ORDER BY " + comboBoxColoanePacienti.SelectedItem.ToString());
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
                    dataGridViewPacienti.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewPacienti);
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

        private void comboBoxOrdinePacienti_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ord;
            if (comboBoxOrdinePacienti.SelectedIndex == 0)
            {
                ord = "ASC";
            }
            else
            {
                ord = "DESC";
            }

            String queryString = String.Format(@"SELECT * FROM PACIENTI ORDER BY " + comboBoxColoanePacienti.SelectedItem.ToString() + " " + ord);
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
                    dataGridViewPacienti.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewPacienti);
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

        private void buttonStergePacienti_Click(object sender, EventArgs e)
        {
            String queryString = String.Format(@"DELETE FROM PACIENTI
                                                WHERE idPacient = " + labelidPacient_print.Text);
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    switch (MessageBox.Show("Ștergerea acestui pacient poate duce la ștergerea sau modificarea unor date care au legătură cu respectiva intrare. Continuați?",
                        "ATENȚIE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                    {
                        case DialogResult.Yes:
                            {
                                command.Connection.Open();
                                int rowsUpdated = command.ExecuteNonQuery();

                                if (rowsUpdated > 0)
                                {
                                    MessageBox.Show("Date șterse cu succes!", "Delete.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    loadPacienti();
                                    textBoxNumePacient.Enabled = false;
                                    textBoxPrenumePacient.Enabled = false;
                                    textBoxCNPPacient.Enabled = false;
                                    textBoxStradaPacient.Enabled = false;
                                    textBoxNumarPacient.Enabled = false;
                                    textBoxLocalitatePacient.Enabled = false;
                                    textBoxTelefonPacient.Enabled = false;
                                    textBoxEmailPacient.Enabled = false;
                                    checkBoxAsigurat.Enabled = false;
                                    dateTimePickerInternarePacienti.Enabled = false;
                                    dateTimePickerExternarePacienti.Enabled = false;
                                }
                                break;
                            }
                        case DialogResult.No:
                        case DialogResult.Cancel: { break; }
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
        #endregion

        #region SIMPTOME
        private void loadSimptome()
        {
            String queryString = String.Format(@"SELECT * FROM SIMPTOME ORDER BY codSimptom ASC");
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
                    dataGridViewSimptome.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewSimptome);
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

        private void loadComboSortSimptome()
        {
            comboBoxColoaneSimptome.Items.Clear();
            comboBoxColoaneSimptome.Items.Add("codSimptom");
            comboBoxColoaneSimptome.Items.Add("denumireSimptom");

            comboBoxOrdineSimptome.Items.Clear();
            comboBoxOrdineSimptome.Items.Add("Ascendent");
            comboBoxOrdineSimptome.Items.Add("Descendent");
        }

        private void buttonSimptome_Click(object sender, EventArgs e)
        {
            // initializations
            hidePanels();
            panelSimptome.Visible = true;
            panelSimptome.BringToFront();
            textBoxdenumireSimptom.Enabled = false;
            labelcodSimptom_print.Text = "";

            pictureBoxCheckSimptome.Visible = false;
            pictureBoxCancelSimptome.Visible = false;

            // load data
            loadSimptome();
            loadComboSortSimptome();
        }

        private void dataGridViewSimptome_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridViewSimptome.Rows[index];
            labelcodSimptom_print.Text = selectedRow.Cells[0].Value.ToString();
            textBoxdenumireSimptom.Text = selectedRow.Cells[1].Value.ToString();
        }

        private void buttonEditeazaSimptome_Click(object sender, EventArgs e)
        {
            addOrUpdate = true;
            if (textBoxdenumireSimptom.Text == "")
            {
                MessageBox.Show("Selectează o intrare!", "Eroare.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                pictureBoxCheckSimptome.Visible = true;
                pictureBoxCancelSimptome.Visible = true;
                textBoxdenumireSimptom.Enabled = true;
            }
        }

        private void buttonClearSimptome_Click(object sender, EventArgs e)
        {
            labelcodSimptom_print.Text = "";
            textBoxdenumireSimptom.Text = "";
        }

        private void pictureBoxCheckSimptome_Click(object sender, EventArgs e)
        {
            if (addOrUpdate == true)
            {
                String queryString = String.Format(@"UPDATE SIMPTOME
                                                    SET denumireSimptom = :newDenumireSimptom
                                                    WHERE codSimptom = " + labelcodSimptom_print.Text);
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newDenumireSimptom", textBoxdenumireSimptom.Text);

                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date actualizate cu succes!", "Update.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadSimptome();
                            pictureBoxCheckSimptome.Visible = false;
                            pictureBoxCancelSimptome.Visible = false;
                            textBoxdenumireSimptom.Enabled = false;
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
            else
            {
                String queryString = String.Format(@"INSERT INTO SIMPTOME(codSimptom, denumireSimptom)
                                                     VALUES ( :newCodSimptom, :newDenumireSimptom)");
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newCodSimptom", labelcodSimptom_print.Text);
                        command.Parameters.Add("newDenumireSimptom", textBoxdenumireSimptom.Text);

                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date inserate cu succes!", "Insert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadSimptome();
                            pictureBoxCheckSimptome.Visible = false;
                            pictureBoxCancelSimptome.Visible = false;
                            textBoxdenumireSimptom.Enabled = false;
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

        private void pictureBoxCancelSimptome_Click(object sender, EventArgs e)
        {
            textBoxdenumireSimptom.Undo();

            pictureBoxCheckSimptome.Visible = false;
            pictureBoxCancelSimptome.Visible = false;
            textBoxdenumireSimptom.Enabled = false;
        }

        private void comboBoxColoaneSimptome_SelectedIndexChanged(object sender, EventArgs e)
        {
            String queryString = String.Format(@"SELECT * FROM SIMPTOME ORDER BY " + comboBoxColoaneSimptome.SelectedItem.ToString());
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
                    dataGridViewSimptome.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewSimptome);
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

        private void comboBoxOrdineSimptome_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ord;
            if (comboBoxOrdineSimptome.SelectedIndex == 0)
            {
                ord = "ASC";
            }
            else
            {
                ord = "DESC";
            }

            String queryString = String.Format(@"SELECT * FROM SIMPTOME ORDER BY " + comboBoxColoaneSimptome.SelectedItem.ToString() + " " + ord);
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
                    dataGridViewSimptome.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewSimptome);
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

        private void buttonStergeSimptome_Click(object sender, EventArgs e)
        {
            String queryString = String.Format(@"DELETE FROM SIMPTOME
                                                WHERE codSimptom = " + labelcodSimptom_print.Text);
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    switch (MessageBox.Show("Ștergerea acestui simptom poate duce la ștergerea sau modificarea unor date care au legătură cu respectiva intrare. Continuați?",
                        "ATENȚIE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                    {
                        case DialogResult.Yes:
                            {
                                command.Connection.Open();
                                int rowsUpdated = command.ExecuteNonQuery();

                                if (rowsUpdated > 0)
                                {
                                    MessageBox.Show("Date șterse cu succes!", "Delete.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    loadSimptome();
                                    pictureBoxCheckSimptome.Visible = false;
                                    pictureBoxCancelSimptome.Visible = false;
                                    textBoxdenumireSimptom.Enabled = false;

                                    labelcodSimptom_print.Text = "";
                                    textBoxdenumireSimptom.Text = "";
                                }
                                break;
                            }
                        case DialogResult.No:
                        case DialogResult.Cancel: { break; }
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

        private void buttonAdaugaSimptome_Click(object sender, EventArgs e)
        {
            addOrUpdate = false;
            pictureBoxCancelSimptome.Visible = true;
            pictureBoxCheckSimptome.Visible = true;
            textBoxdenumireSimptom.Enabled = true;

            labelcodSimptom_print.Text = "";
            textBoxdenumireSimptom.Text = "";

            String queryString = String.Format(@"SELECT SIMPTOME_codSimptom_SEQ.NEXTVAL FROM DUAL");
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    labelcodSimptom_print.Text = command.ExecuteScalar().ToString();

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
        #endregion

        #region BOLI
        private void loadBoli()
        {
            String queryString = String.Format(@"SELECT * FROM BOLI ORDER BY codBoala ASC");
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
                    dataGridViewBoli.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewBoli);
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

        private void loadComboSortBoli()
        {
            comboBoxColoaneBoli.Items.Clear();
            comboBoxColoaneBoli.Items.Add("codBoala");
            comboBoxColoaneBoli.Items.Add("denumireBoala");

            comboBoxOrdineBoli.Items.Clear();
            comboBoxOrdineBoli.Items.Add("Ascendent");
            comboBoxOrdineBoli.Items.Add("Descendent");
        }

        private void buttonBoli_Click(object sender, EventArgs e)
        {
            // initializations
            hidePanels();
            panelBoli.Visible = true;
            panelBoli.BringToFront();
            textBoxdenumireBoala.Enabled = false;
            labelcodBoala_print.Text = "";

            pictureBoxCheckBoli.Visible = false;
            pictureBoxCancelBoli.Visible = false;

            // load data
            loadBoli();
            loadComboSortBoli();
        }

        private void dataGridViewBoli_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridViewBoli.Rows[index];
            labelcodBoala_print.Text = selectedRow.Cells[0].Value.ToString();
            textBoxdenumireBoala.Text = selectedRow.Cells[1].Value.ToString();
        }

        private void buttonEditeazaBoli_Click(object sender, EventArgs e)
        {
            addOrUpdate = true;
            if (textBoxdenumireBoala.Text == "")
            {
                MessageBox.Show("Selectează o intrare!", "Eroare.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                pictureBoxCheckBoli.Visible = true;
                pictureBoxCancelBoli.Visible = true;
                textBoxdenumireBoala.Enabled = true;
            }
        }

        private void buttonClearBoli_Click(object sender, EventArgs e)
        {
            labelcodBoala_print.Text = "";
            textBoxdenumireBoala.Text = "";
        }

        private void pictureBoxCheckBoli_Click(object sender, EventArgs e)
        {
            if (addOrUpdate == true)
            {
                String queryString = String.Format(@"UPDATE BOLI
                                                    SET denumireBoala = :newDenumireBoala
                                                    WHERE codBoala = " + labelcodBoala_print.Text);
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newDenumireBoala", textBoxdenumireBoala.Text);

                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date actualizate cu succes!", "Update.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadBoli();
                            pictureBoxCheckBoli.Visible = false;
                            pictureBoxCancelBoli.Visible = false;
                            textBoxdenumireBoala.Enabled = false;
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
            else
            {
                String queryString = String.Format(@"INSERT INTO BOLI(codBoala, denumireBoala)
                                                     VALUES ( :newCodBoala, :newDenumireBoala)");
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newCodBoala", labelcodBoala_print.Text);
                        command.Parameters.Add("newDenumireBoala", textBoxdenumireBoala.Text);

                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date inserate cu succes!", "Insert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadBoli();
                            pictureBoxCheckBoli.Visible = false;
                            pictureBoxCancelBoli.Visible = false;
                            textBoxdenumireBoala.Enabled = false;
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

        private void pictureBoxCancelBoli_Click(object sender, EventArgs e)
        {
            textBoxdenumireBoala.Undo();

            pictureBoxCheckBoli.Visible = false;
            pictureBoxCancelBoli.Visible = false;
            textBoxdenumireBoala.Enabled = false;
        }

        private void comboBoxColoaneBoli_SelectedIndexChanged(object sender, EventArgs e)
        {
            String queryString = String.Format(@"SELECT * FROM BOLI ORDER BY " + comboBoxColoaneBoli.SelectedItem.ToString());
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
                    dataGridViewBoli.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewBoli);
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

        private void comboBoxOrdineBoli_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ord;
            if (comboBoxOrdineBoli.SelectedIndex == 0)
            {
                ord = "ASC";
            }
            else
            {
                ord = "DESC";
            }

            String queryString = String.Format(@"SELECT * FROM BOLI ORDER BY " + comboBoxColoaneBoli.SelectedItem.ToString() + " " + ord);
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
                    dataGridViewBoli.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewBoli);
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

        private void buttonStergeBoli_Click(object sender, EventArgs e)
        {
            String queryString = String.Format(@"DELETE FROM BOLI
                                                WHERE codBoala = " + labelcodBoala_print.Text);
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    switch (MessageBox.Show("Ștergerea acestei boli poate duce la ștergerea sau modificarea unor date care au legătură cu respectiva intrare. Continuați?",
                        "ATENȚIE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                    {
                        case DialogResult.Yes:
                            {
                                command.Connection.Open();
                                int rowsUpdated = command.ExecuteNonQuery();

                                if (rowsUpdated > 0)
                                {
                                    MessageBox.Show("Date șterse cu succes!", "Delete.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    loadBoli();
                                    pictureBoxCheckBoli.Visible = false;
                                    pictureBoxCancelBoli.Visible = false;
                                    textBoxdenumireBoala.Enabled = false;

                                    labelcodBoala_print.Text = "";
                                    textBoxdenumireBoala.Text = "";
                                }
                                break;
                            }
                        case DialogResult.No:
                        case DialogResult.Cancel: { break; }
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

        private void buttonAdaugaBoli_Click(object sender, EventArgs e)
        {
            addOrUpdate = false;
            pictureBoxCancelBoli.Visible = true;
            pictureBoxCheckBoli.Visible = true;
            textBoxdenumireBoala.Enabled = true;

            labelcodBoala_print.Text = "";
            textBoxdenumireBoala.Text = "";

            String queryString = String.Format(@"SELECT BOLI_codBoala_SEQ.NEXTVAL FROM DUAL");
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    labelcodBoala_print.Text = command.ExecuteScalar().ToString();

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
        #endregion

        #region TRATAMENTE
        private void loadTratamente()
        {
            String queryString = String.Format(@"SELECT * FROM TRATAMENTE ORDER BY idTratament ASC");
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

        private void loadComboSortTratamente()
        {
            comboBoxColoaneTratamente.Items.Clear();
            comboBoxColoaneTratamente.Items.Add("idTratament");
            comboBoxColoaneTratamente.Items.Add("codBoala");
            comboBoxColoaneTratamente.Items.Add("perioadaAdministrare");

            comboBoxOrdineTratamente.Items.Clear();
            comboBoxOrdineTratamente.Items.Add("Ascendent");
            comboBoxOrdineTratamente.Items.Add("Descendent");
        }

        private void buttonTratamente_Click(object sender, EventArgs e)
        {
            // initializations
            hidePanels();
            panelTratamente.Visible = true;
            panelTratamente.BringToFront();
            textBoxcodBoala.Enabled = false;
            textBoxperioadaAdministrare.Enabled = false;
            richTextBoxTratamente.Enabled = false;
            labelidTratament_print.Text = "";

            pictureBoxCheckTratamente.Visible = false;
            pictureBoxCancelTratamente.Visible = false;

            // load data
            loadTratamente();
            loadComboSortTratamente();
        }

        private void dataGridViewTratamente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridViewTratamente.Rows[index];
            labelidTratament_print.Text = selectedRow.Cells[0].Value.ToString();
            textBoxcodBoala.Text = selectedRow.Cells[1].Value.ToString();
            textBoxperioadaAdministrare.Text = selectedRow.Cells[2].Value.ToString();
            richTextBoxTratamente.Text = selectedRow.Cells[3].Value.ToString();
        }

        private void buttonEditeazaTratamente_Click(object sender, EventArgs e)
        {
            addOrUpdate = true;
            if (textBoxcodBoala.Text == "")
            {
                MessageBox.Show("Selectează o intrare!", "Eroare.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                pictureBoxCheckTratamente.Visible = true;
                pictureBoxCancelTratamente.Visible = true;
                textBoxcodBoala.Enabled = true;
                textBoxperioadaAdministrare.Enabled = true;
                richTextBoxTratamente.Enabled = true;
            }
        }

        private void buttonClearTratamente_Click(object sender, EventArgs e)
        {
            labelidTratament_print.Text = "";
            textBoxcodBoala.Text = "";
            textBoxperioadaAdministrare.Text = "";
            richTextBoxTratamente.Text = "";
        }

        private void pictureBoxCheckTratamente_Click(object sender, EventArgs e)
        {
            if (addOrUpdate == true)
            {
                String queryString = String.Format(@"UPDATE TRATAMENTE
                                                    SET codBoala = :newCodBoala,
                                                        perioadaAdministrare = :newPerioada,
                                                        indicatii = :newIndicatii
                                                    WHERE idTratament = " + labelidTratament_print.Text);
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newCodBoala", textBoxcodBoala.Text);
                        command.Parameters.Add("newPerioada", textBoxperioadaAdministrare.Text);
                        command.Parameters.Add("newIndicatii", richTextBoxTratamente.Text);

                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date actualizate cu succes!", "Update.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadTratamente();
                            textBoxcodBoala.Enabled = false;
                            textBoxperioadaAdministrare.Enabled = false;
                            richTextBoxTratamente.Enabled = false;
                            pictureBoxCheckTratamente.Visible = false;
                            pictureBoxCancelTratamente.Visible = false;
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
            else
            {
                String queryString = String.Format(@"INSERT INTO TRATAMENTE(idTratament, codBoala, perioadaAdministrare, indicatii)
                                                     VALUES (:newidTratament, :newCodBoala, :newPerioada, :newIndicatii)");
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newidTratament", labelidTratament_print.Text);
                        command.Parameters.Add("newCodBoala", textBoxcodBoala.Text);
                        command.Parameters.Add("newPerioada", textBoxperioadaAdministrare.Text);
                        command.Parameters.Add("newIndicatii", richTextBoxTratamente.Text);

                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date inserate cu succes!", "Insert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadTratamente();
                            textBoxcodBoala.Enabled = false;
                            textBoxperioadaAdministrare.Enabled = false;
                            richTextBoxTratamente.Enabled = false;
                            pictureBoxCheckTratamente.Visible = false;
                            pictureBoxCancelTratamente.Visible = false;
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

        private void pictureBoxCancelTratamente_Click(object sender, EventArgs e)
        {
            textBoxcodBoala.Undo();
            textBoxperioadaAdministrare.Undo();
            richTextBoxTratamente.Undo();

            textBoxcodBoala.Enabled = false;
            textBoxperioadaAdministrare.Enabled = false;
            richTextBoxTratamente.Enabled = false;
            pictureBoxCheckTratamente.Visible = false;
            pictureBoxCancelTratamente.Visible = false;
        }

        private void comboBoxColoaneTratamente_SelectedIndexChanged(object sender, EventArgs e)
        {
            String queryString = String.Format(@"SELECT * FROM TRATAMENTE ORDER BY " + comboBoxColoaneTratamente.SelectedItem.ToString());
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

        private void comboBoxOrdineTratamente_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ord;
            if (comboBoxOrdineTratamente.SelectedIndex == 0)
            {
                ord = "ASC";
            }
            else
            {
                ord = "DESC";
            }

            String queryString = String.Format(@"SELECT * FROM TRATAMENTE ORDER BY " + comboBoxColoaneTratamente.SelectedItem.ToString() + " " + ord);
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

        private void buttonStergeTratamente_Click(object sender, EventArgs e)
        {
            String queryString = String.Format(@"DELETE FROM TRATAMENTE
                                                WHERE idTratament = " + labelidTratament_print.Text);
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    switch (MessageBox.Show("Ștergerea acestui tratament poate duce la ștergerea sau modificarea unor date care au legătură cu respectiva intrare. Continuați?",
                        "ATENȚIE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                    {
                        case DialogResult.Yes:
                            {
                                command.Connection.Open();
                                int rowsUpdated = command.ExecuteNonQuery();

                                if (rowsUpdated > 0)
                                {
                                    MessageBox.Show("Date șterse cu succes!", "Delete.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    loadTratamente();
                                    textBoxcodBoala.Enabled = false;
                                    textBoxperioadaAdministrare.Enabled = false;
                                    richTextBoxTratamente.Enabled = false;
                                    pictureBoxCheckTratamente.Visible = false;
                                    pictureBoxCancelTratamente.Visible = false;

                                    labelidTratament_print.Text = "";
                                    textBoxcodBoala.Text = "";
                                    textBoxperioadaAdministrare.Text = "";
                                    richTextBoxTratamente.Text = "";
                                }
                                break;
                            }
                        case DialogResult.No:
                        case DialogResult.Cancel: { break; }
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

        private void buttonAdaugaTratamente_Click(object sender, EventArgs e)
        {
            addOrUpdate = false;
            textBoxcodBoala.Enabled = true;
            textBoxperioadaAdministrare.Enabled = true;
            richTextBoxTratamente.Enabled = true;
            pictureBoxCheckTratamente.Visible = true;
            pictureBoxCancelTratamente.Visible = true;

            labelidTratament_print.Text = "";
            textBoxcodBoala.Text = "";
            textBoxperioadaAdministrare.Text = "";
            richTextBoxTratamente.Text = "";

            String queryString = String.Format(@"SELECT TRATAMENTE_idTratament_SEQ.NEXTVAL FROM DUAL");
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    labelidTratament_print.Text = command.ExecuteScalar().ToString();

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
        #endregion

        #region MEDICAMENTE
        private void loadMedicamente()
        {
            String queryString = String.Format(@"SELECT * FROM MEDICAMENTE ORDER BY idMedicament ASC");
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
                    dataGridViewMedicamente.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewMedicamente);
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

        private void loadComboSortMedicamente()
        {
            comboBoxColoaneMedicamente.Items.Clear();
            comboBoxColoaneMedicamente.Items.Add("idMedicament");
            comboBoxColoaneMedicamente.Items.Add("numeMedicament");
            comboBoxColoaneMedicamente.Items.Add("dozaUnitate");

            comboBoxOrdineMedicamente.Items.Clear();
            comboBoxOrdineMedicamente.Items.Add("Ascendent");
            comboBoxOrdineMedicamente.Items.Add("Descendent");
        }

        private void buttonMedicamente_Click(object sender, EventArgs e)
        {
            // initializations
            hidePanels();
            panelMedicamente.Visible = true;
            panelMedicamente.BringToFront();
            textBoxnumeMedicament.Enabled = false;
            textBoxtipMedicament.Enabled = false;
            textBoxdozaUnitate.Enabled = false;
            labelidMedicament_print.Text = "";

            pictureBoxCheckMedicamente.Visible = false;
            pictureBoxCancelMedicamente.Visible = false;

            // load data
            loadMedicamente();
            loadComboSortMedicamente();
        }

        private void dataGridViewMedicamente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridViewMedicamente.Rows[index];
            labelidMedicament_print.Text = selectedRow.Cells[0].Value.ToString();
            textBoxnumeMedicament.Text = selectedRow.Cells[1].Value.ToString();
            textBoxtipMedicament.Text = selectedRow.Cells[2].Value.ToString();
            textBoxdozaUnitate.Text = selectedRow.Cells[3].Value.ToString();
        }

        private void buttonEditeazaMedicamente_Click(object sender, EventArgs e)
        {
            addOrUpdate = true;
            if (textBoxnumeMedicament.Text == "")
            {
                MessageBox.Show("Selectează o intrare!", "Eroare.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                pictureBoxCheckMedicamente.Visible = true;
                pictureBoxCancelMedicamente.Visible = true;
                textBoxnumeMedicament.Enabled = true;
                textBoxtipMedicament.Enabled = true;
                textBoxdozaUnitate.Enabled = true;
            }
        }

        private void buttonClearMedicamente_Click(object sender, EventArgs e)
        {
            labelidMedicament_print.Text = "";
            textBoxnumeMedicament.Text = "";
            textBoxtipMedicament.Text = "";
            textBoxdozaUnitate.Text = "";
        }

        private void pictureBoxCheckMedicamente_Click(object sender, EventArgs e)
        {
            if (addOrUpdate == true)
            {
                String queryString = String.Format(@"UPDATE MEDICAMENTE
                                                    SET numeMedicament = :newNume,
                                                        tipMedicament = :newTip,
                                                        dozaUnitate = :newDoza
                                                    WHERE idMedicament = " + labelidMedicament_print.Text);
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newNume", textBoxnumeMedicament.Text);
                        command.Parameters.Add("newTip", textBoxtipMedicament.Text);
                        command.Parameters.Add("newDoza", textBoxdozaUnitate.Text);

                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date actualizate cu succes!", "Update.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadMedicamente();
                            textBoxnumeMedicament.Enabled = false;
                            textBoxtipMedicament.Enabled = false;
                            textBoxdozaUnitate.Enabled = false;
                            pictureBoxCheckMedicamente.Visible = false;
                            pictureBoxCancelMedicamente.Visible = false;
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
            else
            {
                String queryString = String.Format(@"INSERT INTO MEDICAMENTE(idMedicament, numeMedicament, tipMedicament, dozaUnitate)
                                                     VALUES (:newid, :newNume, :newTip, :newDoza)");
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newid", labelidMedicament_print.Text);
                        command.Parameters.Add("newNume", textBoxnumeMedicament.Text);
                        command.Parameters.Add("newTip", textBoxtipMedicament.Text);
                        command.Parameters.Add("newDoza", textBoxdozaUnitate.Text);

                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date inserate cu succes!", "Insert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadMedicamente();
                            textBoxnumeMedicament.Enabled = false;
                            textBoxtipMedicament.Enabled = false;
                            textBoxdozaUnitate.Enabled = false;
                            pictureBoxCheckMedicamente.Visible = false;
                            pictureBoxCancelMedicamente.Visible = false;
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

        private void pictureBoxCancelMedicamente_Click(object sender, EventArgs e)
        {
            textBoxnumeMedicament.Undo();
            textBoxtipMedicament.Undo();
            textBoxdozaUnitate.Undo();

            textBoxnumeMedicament.Enabled = false;
            textBoxtipMedicament.Enabled = false;
            textBoxdozaUnitate.Enabled = false;
            pictureBoxCheckMedicamente.Visible = false;
            pictureBoxCancelMedicamente.Visible = false;
        }

        private void comboBoxColoaneMedicamente_SelectedIndexChanged(object sender, EventArgs e)
        {
            String queryString = String.Format(@"SELECT * FROM MEDICAMENTE ORDER BY " + comboBoxColoaneMedicamente.SelectedItem.ToString());
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
                    dataGridViewMedicamente.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewMedicamente);
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

        private void comboBoxOrdineMedicamente_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ord;
            if (comboBoxOrdineMedicamente.SelectedIndex == 0)
            {
                ord = "ASC";
            }
            else
            {
                ord = "DESC";
            }

            String queryString = String.Format(@"SELECT * FROM MEDICAMENTE ORDER BY " + comboBoxColoaneMedicamente.SelectedItem.ToString() + " " + ord);
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
                    dataGridViewMedicamente.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewMedicamente);
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

        private void buttonStergeMedicamente_Click(object sender, EventArgs e)
        {
            String queryString = String.Format(@"DELETE FROM MEDICAMENTE
                                                WHERE idMedicament = " + labelidMedicament_print.Text);
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    switch (MessageBox.Show("Ștergerea acestui medicament poate duce la ștergerea sau modificarea unor date care au legătură cu respectiva intrare. Continuați?",
                        "ATENȚIE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                    {
                        case DialogResult.Yes:
                            {
                                command.Connection.Open();
                                int rowsUpdated = command.ExecuteNonQuery();

                                if (rowsUpdated > 0)
                                {
                                    MessageBox.Show("Date șterse cu succes!", "Delete.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    loadMedicamente();
                                    textBoxnumeMedicament.Enabled = false;
                                    textBoxtipMedicament.Enabled = false;
                                    textBoxdozaUnitate.Enabled = false;
                                    pictureBoxCheckMedicamente.Visible = false;
                                    pictureBoxCancelMedicamente.Visible = false;

                                    labelidMedicament_print.Text = "";
                                    textBoxnumeMedicament.Text = "";
                                    textBoxtipMedicament.Text = "";
                                    textBoxdozaUnitate.Text = "";
                                }
                                break;
                            }
                        case DialogResult.No:
                        case DialogResult.Cancel: { break; }
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

        private void buttonAdaugaMedicamente_Click(object sender, EventArgs e)
        {
            addOrUpdate = false;
            textBoxnumeMedicament.Enabled = true;
            textBoxtipMedicament.Enabled = true;
            textBoxdozaUnitate.Enabled = true;
            pictureBoxCheckMedicamente.Visible = true;
            pictureBoxCancelMedicamente.Visible = true;

            labelidMedicament_print.Text = "";
            textBoxnumeMedicament.Text = "";
            textBoxtipMedicament.Text = "";
            textBoxdozaUnitate.Text = "";

            String queryString = String.Format(@"SELECT MEDICAMENTE_idMedicament_SEQ.NEXTVAL FROM DUAL");
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    labelidMedicament_print.Text = command.ExecuteScalar().ToString();

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
        #endregion

        private void linkLabelGroupHaving_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            String queryString = String.Format(@"SELECT d.codParafa, d.nume, d.prenume, d.telefon, d.email, d.codFunctie, d.idSectie, COUNT(c.idConsultatie) totalConsultatii
                                                FROM DOCTORI d JOIN CONSULTATII c ON d.codParafa = c.codParafa
                                                GROUP BY d.codParafa, d.nume, d.prenume, d.telefon, d.email, d.codFunctie, d.idSectie
                                                HAVING COUNT(c.idConsultatie) > 5
                                                ORDER BY totalConsultatii DESC");
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
                    dataGridViewStatistici.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewStatistici);
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

        private void buttonStatistici_Click(object sender, EventArgs e)
        {
            panelStatistici.Show();
            panelStatistici.BringToFront();
        }

        private void linkLabelNumarSpitale_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            String queryString = String.Format(@"SELECT * FROM NUMAR_SPITALE");
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
                    dataGridViewStatistici.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewStatistici);
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
