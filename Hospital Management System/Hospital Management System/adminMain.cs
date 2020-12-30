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
        private bool addOrUpdate = false; // false -> add, true -> update

        private void hidePanels()
        {
            //panelBoli.Visible = false;
            panelJudet.SendToBack();
            panelJudet.Visible = false;
            panelLocalitati.SendToBack();
            panelLocalitati.Visible = false;
            panelSpitale.SendToBack();
            panelSpitale.Visible = false;
            /* panelMedicamente.Visible = false;
             panelPacienti.Visible = false;
             panelDoctori.Visible = false;
             panelSectiiSpitale.Visible = false;
             panelSimptome.Visible = false;

             panelTratamente.Visible = false;*/
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
            comboBoxColoaneJudete.Items.Add("codJudet");
            comboBoxColoaneJudete.Items.Add("numeJudet");

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
            //textBoxnumeJudet.Text = undoName.GetLastChange();
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

        private void loadCheckedListLocalitati()
        {
            checkedListBoxJudete.Items.Clear();
            String queryString = String.Format(@"SELECT * FROM JUDETE ORDER BY codJudet ASC");
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    OracleDataReader dataReader = command.ExecuteReader();

                    string numeJudet;

                    while(dataReader.Read())
                    {
                        numeJudet = dataReader["numeJudet"].ToString();
                        checkedListBoxJudete.Items.Insert(checkedListBoxJudete.Items.Count, numeJudet);
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

        private void buttonFiltruLocalitati_Click(object sender, EventArgs e)
        {
            while (checkedListBoxJudete.CheckedIndices.Count > 0)
            {
                checkedListBoxJudete.SetItemChecked(checkedListBoxJudete.CheckedIndices[0], false);
            }
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
            loadCheckedListLocalitati();
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
                {   switch(MessageBox.Show("Stergerea acestei localități poate duce la ștergerea unor date care au legătură cu respectiva intrare. Continuați?", 
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
            comboBoxColoaneSpitale.Items.Add("idSpital");
            comboBoxColoaneSpitale.Items.Add("numeSpital");
            comboBoxColoaneSpitale.Items.Add("idLocalitate");

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
                    switch (MessageBox.Show("Stergerea acestui spital poate duce la ștergerea unor date care au legătură cu respectiva intrare. Continuați?",
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

        #region altele
        private void buttonSectiiSpitale_Click(object sender, EventArgs e)
        {
            hidePanels();
            //panelSectiiSpitale.Visible = true;
        }

        private void buttonDoctori_Click(object sender, EventArgs e)
        {
            hidePanels();
            //panelDoctori.Visible = true;
        }

        private void buttonPacient_Click(object sender, EventArgs e)
        {
            hidePanels();
            //panelPacienti.Visible = true;
        }

        private void buttonSimptome_Click(object sender, EventArgs e)
        {
            hidePanels();
            //panelSimptome.Visible = true;
        }

        private void buttonBoli_Click(object sender, EventArgs e)
        {
            /*hidePanels();
            panelBoli.Visible = true;*/
        }

        private void buttonTratamente_Click(object sender, EventArgs e)
        {
            hidePanels();
            //panelTratamente.Visible = true;
        }

        private void buttonMedicamente_Click(object sender, EventArgs e)
        {
            hidePanels();
            //panelMedicamente.Visible = true;
        }

        private void displayData()
        {
            /*String queryString = String.Format(@"SELECT * FROM LOCALITATI WHERE codJudet = " + codJudet);
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
            }*/
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if (comboBox1.SelectedIndex != -1)
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
            }*/
        }
        #endregion
    }
}
