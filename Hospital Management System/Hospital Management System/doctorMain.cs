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
    public partial class doctorMain : Form
    {
        private bool addOrUpdate = false; // false -> add, true -> update
        private void hidePanels()
        {
            panelDatePersonale.SendToBack();
            panelDatePersonale.Hide();
            panelPacienti.SendToBack();
            panelPacienti.Hide();
            panelPacientiiMei.SendToBack();
            panelPacientiiMei.Hide();
            panelConsultatii.SendToBack();
            panelConsultatii.Hide();
            panelDiagnostice.SendToBack();
            panelDiagnostice.Hide();
        }
        public doctorMain()
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

        private void doctorMain_Load(object sender, EventArgs e)
        {
            hidePanels();
        }

        #region DATE
        private void buttonDate_Click(object sender, EventArgs e)
        {
            hidePanels();
            panelDatePersonale.BringToFront();
            panelDatePersonale.Show();

            pictureBoxCheckDoctori.Visible = false;
            pictureBoxCancelDoctori.Visible = false;
            textBoxNume.Enabled = false;
            textBoxPrenume.Enabled = false;
            textBoxCNP.Enabled = false;
            textBoxTelefon.Enabled = false;
            textBoxEmail.Enabled = false;
            textBoxcodFunctie.Enabled = false;
            textBoxidSectie.Enabled = false;
            labelcodParafa.Text = "";

            String queryString = String.Format(@"SELECT * FROM DOCTORI WHERE codParafa = " + doctorLogin.get_codParafa());
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
                        labelcodParafa.Text = doctorLogin.get_codParafa();
                        textBoxNume.Text = dataReader["nume"].ToString();
                        textBoxPrenume.Text = dataReader["prenume"].ToString();
                        textBoxCNP.Text = dataReader["CNP"].ToString();
                        textBoxTelefon.Text = dataReader["telefon"].ToString();
                        textBoxEmail.Text = dataReader["email"].ToString();
                        textBoxcodFunctie.Text = dataReader["codFunctie"].ToString();
                        textBoxidSectie.Text = dataReader["idSectie"].ToString();
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

        private void buttonEditeazaDoctori_Click(object sender, EventArgs e)
        {
            pictureBoxCheckDoctori.Visible = true;
            pictureBoxCancelDoctori.Visible = true;
            textBoxNume.Enabled = true;
            textBoxPrenume.Enabled = true;
            textBoxCNP.Enabled = true;
            textBoxTelefon.Enabled = true;
            textBoxEmail.Enabled = true;
            textBoxcodFunctie.Enabled = true;
            textBoxidSectie.Enabled = true;
        }

        private void pictureBoxCheckDoctori_Click(object sender, EventArgs e)
        {
                String queryString = String.Format(@"UPDATE DOCTORI 
                                                    SET nume = :newNume,
                                                        prenume = :newPrenume,
                                                        CNP = :newCNP,
                                                        telefon = :newTelefon,
                                                        email = :newEmail,
                                                        codFunctie = :newCodFunctie,
                                                        idSectie = :newidSectie
                                                    WHERE codParafa = " + labelcodParafa.Text);
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
                    command.Parameters.Add("newTelefon", textBoxTelefon.Text);
                    command.Parameters.Add("newEmail", textBoxEmail.Text);
                    command.Parameters.Add("newCodFunctie", textBoxcodFunctie.Text);
                    command.Parameters.Add("newidSectie", textBoxidSectie.Text);
                    int rowsUpdated = command.ExecuteNonQuery();

                    if (rowsUpdated > 0)
                    {
                        MessageBox.Show("Date actualizate cu succes!", "Update.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pictureBoxCheckDoctori.Visible = false;
                        pictureBoxCancelDoctori.Visible = false;
                        textBoxNume.Enabled = false;
                        textBoxPrenume.Enabled = false;
                        textBoxCNP.Enabled = false;
                        textBoxTelefon.Enabled = false;
                        textBoxEmail.Enabled = false;
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

        private void pictureBoxCancelDoctori_Click(object sender, EventArgs e)
        {
            textBoxNume.Undo();
            textBoxPrenume.Undo();
            textBoxCNP.Undo();
            textBoxTelefon.Undo();
            textBoxEmail.Undo();
            textBoxcodFunctie.Undo();
            textBoxidSectie.Undo();
            pictureBoxCheckDoctori.Visible = false;
            pictureBoxCancelDoctori.Visible = false;
            textBoxNume.Enabled = false;
            textBoxPrenume.Enabled = false;
            textBoxCNP.Enabled = false;
            textBoxTelefon.Enabled = false;
            textBoxEmail.Enabled = false;
            textBoxcodFunctie.Enabled = false;
            textBoxidSectie.Enabled = false;
        }
        #endregion

        #region INTERNARI/EXTERNARI
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
            {
                dateTimePickerExternarePacienti.Visible = false;
                checkBoxExternare.Checked = false;
            }

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
                                    checkBoxExternare.Enabled = false;
                                    dateTimePickerInternarePacienti.Enabled = false;
                                    dateTimePickerExternarePacienti.Enabled = false;

                                    labelidPacient_print.Text = "";
                                    textBoxNumePacient.Text = "";
                                    textBoxPrenumePacient.Text = "";
                                    textBoxCNPPacient.Text = "";
                                    textBoxStradaPacient.Text = "";
                                    textBoxNumarPacient.Text = "";
                                    textBoxLocalitatePacient.Text = "";
                                    textBoxEmailPacient.Text = "";
                                    textBoxTelefonPacient.Text = "";
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

        private void buttonIntExt_Click(object sender, EventArgs e)
        {
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
            checkBoxExternare.Enabled = false;
            dateTimePickerInternarePacienti.Enabled = false;
            dateTimePickerExternarePacienti.Enabled = false;
            labelidPacient_print.Text = "";

            pictureBoxCheckPacienti.Visible = false;
            pictureBoxCancelPacienti.Visible = false;

            // load data
            loadPacienti();
            loadComboSortPacienti();
        }

        private void buttonAdaugaPacienti_Click(object sender, EventArgs e)
        {
            addOrUpdate = false;
            pictureBoxCheckPacienti.Visible = true;
            pictureBoxCancelPacienti.Visible = true;
            textBoxNumePacient.Enabled = true;
            textBoxPrenumePacient.Enabled = true;
            textBoxCNPPacient.Enabled = true;
            textBoxStradaPacient.Enabled = true;
            textBoxNumarPacient.Enabled = true;
            textBoxLocalitatePacient.Enabled = true;
            textBoxTelefonPacient.Enabled = true;
            textBoxEmailPacient.Enabled = true;
            checkBoxAsigurat.Enabled = true;
            checkBoxExternare.Enabled = true;
            dateTimePickerInternarePacienti.Enabled = true;

            labelidPacient_print.Text = "";
            textBoxNumePacient.Text = "";
            textBoxPrenumePacient.Text = "";
            textBoxCNPPacient.Text = "";
            textBoxStradaPacient.Text = "";
            textBoxNumarPacient.Text = "";
            textBoxLocalitatePacient.Text = "";
            textBoxEmailPacient.Text = "";
            textBoxTelefonPacient.Text = "";

            String queryString = String.Format(@"SELECT PACIENTI_idPacient_SEQ.NEXTVAL FROM DUAL");
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    labelidPacient_print.Text = command.ExecuteScalar().ToString();

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

        private void buttonEditeazaPacienti_Click(object sender, EventArgs e)
        {
            addOrUpdate = true;
            if (textBoxNumarPacient.Text == "")
            {
                MessageBox.Show("Selectează o intrare!", "Eroare.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                pictureBoxCheckPacienti.Visible = true;
                pictureBoxCancelPacienti.Visible = true;
                textBoxNumePacient.Enabled = true;
                textBoxPrenumePacient.Enabled = true;
                textBoxCNPPacient.Enabled = true;
                textBoxStradaPacient.Enabled = true;
                textBoxNumarPacient.Enabled = true;
                textBoxLocalitatePacient.Enabled = true;
                textBoxTelefonPacient.Enabled = true;
                textBoxEmailPacient.Enabled = true;
                checkBoxAsigurat.Enabled = true;
                checkBoxExternare.Enabled = true;
                dateTimePickerInternarePacienti.Enabled = true;
            }
        }

        private void pictureBoxCheckPacienti_Click(object sender, EventArgs e)
        {
            if (addOrUpdate == true)
            {
                String queryString = String.Format(@"UPDATE PACIENTI 
                                                    SET nume = :newNume,
                                                        prenume = :newPrenume,
                                                        CNP = :newCNP,
                                                        strada = :newStrada,
                                                        numar = :newNumar,
                                                        localitate = :newLocalitate,
                                                        telefon = :newTelefon,
                                                        email = :newEmail,
                                                        asigurat = :newAsigurat,
                                                        dataInternare = TO_DATE(:newDataInt, 'DD.MM.YYYY HH24:MI:SS'),
                                                        dataExternare = TO_DATE(:newDataExt, 'DD.MM.YYYY HH24:MI:SS')
                                                    WHERE idPacient = " + labelidPacient_print.Text);
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newNume", textBoxNumePacient.Text);
                        command.Parameters.Add("newPrenume", textBoxPrenumePacient.Text);
                        command.Parameters.Add("newCNP", textBoxCNPPacient.Text);
                        command.Parameters.Add("newStrada", textBoxStradaPacient.Text);
                        command.Parameters.Add("newNumar", textBoxNumarPacient.Text);
                        command.Parameters.Add("newLocalitate", textBoxLocalitatePacient.Text);
                        command.Parameters.Add("newTelefon", textBoxTelefonPacient.Text);
                        command.Parameters.Add("newEmail", textBoxEmailPacient.Text);

                        if (checkBoxAsigurat.Checked == true)
                            command.Parameters.Add("newAsigurat", "1");
                        else
                            command.Parameters.Add("newAsigurat", "0");

                        command.Parameters.Add("newDataInt", dateTimePickerInternarePacienti.Value.ToString());

                        if (checkBoxExternare.Checked == true)
                            command.Parameters.Add("newDataExt", dateTimePickerExternarePacienti.Value.ToString());
                        else
                            command.Parameters.Add("newDataExt", null);

                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date actualizate cu succes!", "Update.", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            checkBoxExternare.Enabled = false;
                            dateTimePickerInternarePacienti.Enabled = false;
                            dateTimePickerExternarePacienti.Enabled = false;

                            pictureBoxCheckPacienti.Visible = false;
                            pictureBoxCancelPacienti.Visible = false;
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
                String queryString = String.Format(@"INSERT INTO PACIENTI(idPacient, nume, prenume, CNP, strada, numar, localitate, telefon, email, asigurat, dataInternare, dataExternare)
                                                     VALUES ( :newidPacient, :newNume, :newPrenume, :newCNP, :newStrada, :newNumar, :newLocalitate, :newTelefon, :newEmail, :newAsigurat, TO_DATE(:newDataInt, 'DD.MM.YYYY HH24:MI:SS'), TO_DATE(:newDataExt, 'DD.MM.YYYY HH24:MI:SS'))");
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newidPacient", labelidPacient_print.Text);
                        command.Parameters.Add("newNume", textBoxNumePacient.Text);
                        command.Parameters.Add("newPrenume", textBoxPrenumePacient.Text);
                        command.Parameters.Add("newCNP", textBoxCNPPacient.Text);
                        command.Parameters.Add("newStrada", textBoxStradaPacient.Text);
                        command.Parameters.Add("newNumar", textBoxNumarPacient.Text);
                        command.Parameters.Add("newLocalitate", textBoxLocalitatePacient.Text);
                        command.Parameters.Add("newTelefon", textBoxTelefonPacient.Text);
                        if(textBoxEmailPacient.Text == "")
                            command.Parameters.Add("newEmail", null);
                        else
                            command.Parameters.Add("newEmail", textBoxEmailPacient.Text);

                        if (checkBoxAsigurat.Checked == true)
                            command.Parameters.Add("newAsigurat", "1");
                        else
                            command.Parameters.Add("newAsigurat", "0");

                        command.Parameters.Add("newDataInt", dateTimePickerInternarePacienti.Value.ToString());

                        if (checkBoxExternare.Checked == true)
                            command.Parameters.Add("newDataExt", dateTimePickerExternarePacienti.Value.ToString());
                        else
                            command.Parameters.Add("newDataExt", null);

                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date inserate cu succes!", "Insert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            checkBoxExternare.Enabled = false;
                            dateTimePickerInternarePacienti.Enabled = false;
                            dateTimePickerExternarePacienti.Enabled = false;

                            pictureBoxCheckPacienti.Visible = false;
                            pictureBoxCancelPacienti.Visible = false;
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
        private void pictureBoxCancelPacienti_Click(object sender, EventArgs e)
        {
            textBoxNumePacient.Undo();
            textBoxPrenumePacient.Undo();
            textBoxCNPPacient.Undo();
            textBoxStradaPacient.Undo();
            textBoxNumarPacient.Undo();
            textBoxLocalitatePacient.Undo();
            textBoxTelefonPacient.Undo();
            textBoxEmailPacient.Undo();

            pictureBoxCheckPacienti.Visible = false;
            pictureBoxCancelPacienti.Visible = false;

            textBoxNumePacient.Enabled = false;
            textBoxPrenumePacient.Enabled = false;
            textBoxCNPPacient.Enabled = false;
            textBoxStradaPacient.Enabled = false;
            textBoxNumarPacient.Enabled = false;
            textBoxLocalitatePacient.Enabled = false;
            textBoxTelefonPacient.Enabled = false;
            textBoxEmailPacient.Enabled = false;
            checkBoxAsigurat.Enabled = false;
            checkBoxExternare.Enabled = false;
            dateTimePickerInternarePacienti.Enabled = false;
            dateTimePickerExternarePacienti.Enabled = false;

            pictureBoxCheckPacienti.Visible = false;
            pictureBoxCancelPacienti.Visible = false;
        }

        private void checkBoxExternare_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerExternarePacienti.Visible = true;
            dateTimePickerExternarePacienti.Enabled = checkBoxExternare.Checked;
        }
        #endregion

        private void loadPacientiiMei()
        {
            String queryString = String.Format(@"SELECT ID_PACIENT, NUME_PACIENT, DATA_INTERNARE, DATA_EXTERNARE FROM PACIENTII_MEI WHERE codParafa = " + doctorLogin.get_codParafa() + " ORDER BY ID_PACIENT ASC");
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
                    dataGridViewPacientiiMei.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewPacientiiMei);
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

        private void buttonPacienti_Click(object sender, EventArgs e)
        {
            hidePanels();
            panelPacientiiMei.Show();
            panelPacientiiMei.BringToFront();
            loadPacientiiMei();
            checkBoxIntPrez.Checked = false;
        }

        private void checkBoxIntPrez_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIntPrez.Checked == true)
            {
                String queryString = String.Format(@"SELECT ID_PACIENT, NUME_PACIENT, DATA_INTERNARE, DATA_EXTERNARE FROM PACIENTII_MEI WHERE codParafa = " + doctorLogin.get_codParafa() + " AND DATA_EXTERNARE = 'In prezent, internat' ORDER BY ID_PACIENT ASC");
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
                        dataGridViewPacientiiMei.DataSource = dataTable;

                        autoSizeDataGridView(dataGridViewPacientiiMei);
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
            else loadPacientiiMei();
        }

        #region CONSULTATII
        private void loadConsultatii()
        {
            String queryString = String.Format(@"SELECT * FROM CONSULTATII_DOCTOR WHERE codParafa = " + doctorLogin.get_codParafa());
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
                    dataGridViewConsultatii.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewConsultatii);
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

        void loadComboPacienti()
        {
            comboBoxidPacient.Items.Clear();
            String queryString = String.Format(@"SELECT * FROM PACIENTI WHERE dataExternare IS NULL ORDER BY idPacient");
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
                        string pacient;
                        pacient = dataReader["idPacient"].ToString() + ", " + dataReader["nume"].ToString() + " " + dataReader["prenume"].ToString();
                        comboBoxidPacient.Items.Add(pacient);
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

        private void loadComboSortConsultatii()
        {
            comboBoxColoaneConsultatii.Items.Clear();
            comboBoxColoaneConsultatii.Items.Add("idConsultatie");
            comboBoxColoaneConsultatii.Items.Add("idPacient");
            comboBoxColoaneConsultatii.Items.Add("dataConsultatie");

            comboBoxOrdineConsultatii.Items.Clear();
            comboBoxOrdineConsultatii.Items.Add("Ascendent");
            comboBoxOrdineConsultatii.Items.Add("Descendent");
        }

        private void buttonConsultatii_Click(object sender, EventArgs e)
        {
            // initializations
            hidePanels();
            panelConsultatii.Visible = true;
            panelConsultatii.BringToFront();
            textBoxcodParafa.Visible = false;
            dateTimePickerDataConsultatie.Enabled = false;
            textBoxNumePrenume.Enabled = false;
            textBoxCNP_Pacient.Enabled = false;
            labelidConsultatie_print.Text = "";

            pictureBoxCheckConsultatii.Visible = false;
            pictureBoxCancelConsultatii.Visible = false;

            // load data
            loadConsultatii();
            loadComboSortConsultatii();
            loadComboPacienti();
        }

        private void dataGridViewConsultatii_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridViewConsultatii.Rows[index];
            labelidConsultatie_print.Text = selectedRow.Cells[0].Value.ToString();
            textBoxcodParafa.Text = selectedRow.Cells[1].Value.ToString();

            for (int i = 0; i < comboBoxidPacient.Items.Count; i++)
                if (comboBoxidPacient.Items[i].ToString().IndexOf(selectedRow.Cells[2].Value.ToString() + ", " + selectedRow.Cells[3].Value.ToString()) >= 0)
                    comboBoxidPacient.SelectedIndex = i;

            textBoxNumePrenume.Text = selectedRow.Cells[3].Value.ToString();
            textBoxCNP_Pacient.Text = selectedRow.Cells[4].Value.ToString();
            dateTimePickerDataConsultatie.Value = Convert.ToDateTime(selectedRow.Cells[5].Value);

        }

        private void buttonEditeazaConsultatii_Click(object sender, EventArgs e)
        {
            addOrUpdate = true;
            if (textBoxNumePrenume.Text == "")
            {
                MessageBox.Show("Selectează o intrare!", "Eroare.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                pictureBoxCheckConsultatii.Visible = true;
                pictureBoxCancelConsultatii.Visible = true;
                comboBoxidPacient.Enabled = true;
                textBoxNumePrenume.Text = "";
                textBoxCNP_Pacient.Text = "";
                dateTimePickerDataConsultatie.Enabled = true;
                textBoxcodParafa.Visible = true;
                textBoxcodParafa.Enabled = true;
            }
        }

        private void buttonClearConsultatii_Click(object sender, EventArgs e)
        {
            labelidConsultatie_print.Text = "";
            textBoxNumePrenume.Text = "";
            comboBoxidPacient.SelectedIndex = -1;
            textBoxCNP_Pacient.Text = "";
            textBoxNumePrenume.Text = "";
            textBoxcodParafa.Text = "";
        }

        private void pictureBoxCheckConsultatii_Click(object sender, EventArgs e)
        {
            if (addOrUpdate == true)
            {
                String queryString = String.Format(@"UPDATE CONSULTATII_DOCTOR 
                                                SET idPacient= :newidPacient,
                                                    dataConsultatie = TO_DATE(:newDataCons, 'DD.MM.YYYY HH24:MI:SS'),
                                                    codParafa = :newCodParafa
                                                WHERE idConsultatie = " + labelidConsultatie_print.Text);
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        MessageBox.Show(comboBoxidPacient.SelectedItem.ToString().Substring(0, comboBoxidPacient.SelectedItem.ToString().IndexOf(", ")));
                        command.Parameters.Add("newidPacient", comboBoxidPacient.SelectedItem.ToString().Substring(0, comboBoxidPacient.SelectedItem.ToString().IndexOf(", ")));
                        command.Parameters.Add("newDataCons", dateTimePickerDataConsultatie.Value.ToString());
                        command.Parameters.Add("newCodParafa", textBoxcodParafa.Text);
                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date actualizate cu succes!", "Update.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadConsultatii();
                            dateTimePickerDataConsultatie.Enabled = false;
                            textBoxNumePrenume.Enabled = false;
                            textBoxCNP_Pacient.Enabled = false;
                            textBoxcodParafa.Visible = false;
                            pictureBoxCheckConsultatii.Visible = false;
                            pictureBoxCancelConsultatii.Visible = false;
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
                String queryString = String.Format(@"INSERT INTO CONSULTATII_DOCTOR(idConsultatie, idPacient, dataConsultatie, codParafa)
                                                     VALUES ( :newidCons, :newidPacient, TO_DATE(TO_DATE(:newDataCons, 'DD.MM.YYYY HH24:MI:SS'), 'DD.MM.YYYY'), :newCodParafa)");
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newidCons", labelidConsultatie_print.Text);
                        command.Parameters.Add("newidPacient", comboBoxidPacient.SelectedItem.ToString().Substring(0, comboBoxidPacient.SelectedItem.ToString().IndexOf(", ")));
                        command.Parameters.Add("newDataCons", dateTimePickerDataConsultatie.Value.ToString());
                        textBoxcodParafa.Text = doctorLogin.get_codParafa();
                        command.Parameters.Add("newCodParafa", textBoxcodParafa.Text);
                        
                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date inserate cu succes!", "Insert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadConsultatii();
                            dateTimePickerDataConsultatie.Enabled = false;
                            textBoxNumePrenume.Enabled = false;
                            textBoxCNP_Pacient.Enabled = false;
                            textBoxcodParafa.Visible = false;
                            pictureBoxCheckConsultatii.Visible = false;
                            pictureBoxCancelConsultatii.Visible = false;
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

        private void pictureBoxCancelConsultatii_Click(object sender, EventArgs e)
        {
            dateTimePickerDataConsultatie.Enabled = false;
            textBoxNumePrenume.Enabled = false;
            textBoxCNP_Pacient.Enabled = false;
            textBoxcodParafa.Visible = false;
            pictureBoxCheckConsultatii.Visible = false;
            pictureBoxCancelConsultatii.Visible = false;
        }

        private void comboBoxColoaneConsultatii_SelectedIndexChanged(object sender, EventArgs e)
        {
            String queryString = String.Format(@"SELECT * FROM CONSULTATII_DOCTOR ORDER BY " + comboBoxColoaneConsultatii.SelectedItem.ToString());
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
                    dataGridViewConsultatii.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewConsultatii);
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

        private void comboBoxOrdineConsultatii_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ord;
            if (comboBoxOrdineConsultatii.SelectedIndex == 0)
            {
                ord = "ASC";
            }
            else
            {
                ord = "DESC";
            }

            String queryString = String.Format(@"SELECT * FROM CONSULTATII_DOCTOR ORDER BY " + comboBoxColoaneConsultatii.SelectedItem.ToString() + " " + ord);
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
                    dataGridViewConsultatii.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewConsultatii);
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

        private void buttonStergeConsultatii_Click(object sender, EventArgs e)
        {
            String queryString = String.Format(@"DELETE FROM CONSULTATII_DOCTOR
                                                WHERE idConsultatie = " + labelidConsultatie_print.Text);
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    switch (MessageBox.Show("Ștergerea acestei consultatii poate duce la ștergerea sau modificarea unor date care au legătură cu respectiva intrare. Continuați?",
                        "ATENȚIE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                    {
                        case DialogResult.Yes:
                            {
                                command.Connection.Open();
                                int rowsUpdated = command.ExecuteNonQuery();

                                if (rowsUpdated > 0)
                                {
                                    MessageBox.Show("Date șterse cu succes!", "Delete.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    loadConsultatii();
                                    dateTimePickerDataConsultatie.Enabled = false;
                                    textBoxNumePrenume.Enabled = false;
                                    textBoxCNP_Pacient.Enabled = false;

                                    pictureBoxCheckConsultatii.Visible = false;
                                    pictureBoxCancelConsultatii.Visible = false;
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

        private void buttonAdaugaConsultatii_Click(object sender, EventArgs e)
        {
            addOrUpdate = false;
            pictureBoxCheckConsultatii.Visible = true;
            pictureBoxCancelConsultatii.Visible = true;
            textBoxcodParafa.Visible = false;
            comboBoxidPacient.Enabled = true;
            dateTimePickerDataConsultatie.Enabled = true;

            labelidConsultatie_print.Text = "";

            String queryString = String.Format(@"SELECT CONSULTATII_idConsultatie_SEQ.NEXTVAL FROM DUAL");
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    labelidConsultatie_print.Text = command.ExecuteScalar().ToString();

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

        #region DIAGNOSTICE
        private void loadDiagnostice()
        {
            String queryString = String.Format(@"SELECT * FROM DIAGNOSTICE_DOCTOR WHERE codParafa = " + doctorLogin.get_codParafa());
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
                    dataGridViewDiagnostice.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewDiagnostice);
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

        private void loadComboSortDiagnostice()
        {
            comboBoxColoaneDiagnostice.Items.Clear();
            comboBoxColoaneDiagnostice.Items.Add("idDiagnostic");
            comboBoxColoaneDiagnostice.Items.Add("idConsultatie");
            comboBoxColoaneDiagnostice.Items.Add("codBoala");

            comboBoxOrdineDiagnostice.Items.Clear();
            comboBoxOrdineDiagnostice.Items.Add("Ascendent");
            comboBoxOrdineDiagnostice.Items.Add("Descendent");
        }

        void loadSimptome()
        {
            checkedListBoxSimptome.Items.Clear();
            String queryString = String.Format(@"SELECT * FROM SIMPTOME ORDER BY codSimptom");
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
                        checkedListBoxSimptome.Items.Add(dataReader["codSimptom"].ToString() + ". " + dataReader["denumireSimptom"].ToString(), false);
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

        void checkCheck(string idDiag)
        {
            String queryString = String.Format(@"SELECT * FROM DIAGNOSTICE_SIMPTOME WHERE idDiagnostic = " + idDiag + " ORDER BY codSimptom");
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
                        string codSimptom = dataReader["codSimptom"].ToString();

                        for(int i = 0; i < checkedListBoxSimptome.Items.Count; i++)
                        {
                            string cod = checkedListBoxSimptome.Items[i].ToString().Substring(0, checkedListBoxSimptome.Items[i].ToString().IndexOf(". "));
                            if (codSimptom == cod)
                                checkedListBoxSimptome.SetItemChecked(i, true);
                            else
                                checkedListBoxSimptome.SetItemChecked(i, false);
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


        private void buttonDiagnostice_Click(object sender, EventArgs e)
        {
            // initializations
            hidePanels();
            panelDiagnostice.Visible = true;
            panelDiagnostice.BringToFront();
            
            textBoxidPacient.Enabled = false;
            textBoxidConsultatie.Enabled = false;
            textBoxcodBoala.Enabled = false;
            pictureBoxCheckDiagnostice.Visible = false;
            pictureBoxCancelDiagnostice.Visible = false;

            // load data
            loadDiagnostice();
            loadComboSortDiagnostice();
            loadSimptome();
        }

        private void dataGridViewDiagnostice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridViewDiagnostice.Rows[index];
            labelidDiagnostic_print.Text = selectedRow.Cells[0].Value.ToString();
            textBoxidPacient.Text = selectedRow.Cells[5].Value.ToString();
            textBoxidConsultatie.Text = selectedRow.Cells[1].Value.ToString();
            textBoxcodBoala.Text = selectedRow.Cells[2].Value.ToString();
            checkCheck(labelidDiagnostic_print.Text);
        }

        private void buttonEditeazaDiagnostice_Click(object sender, EventArgs e)
        {
            addOrUpdate = true;
            if (textBoxidPacient.Text == "")
            {
                MessageBox.Show("Selectează o intrare!", "Eroare.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                textBoxidPacient.Enabled = false;
                textBoxidConsultatie.Enabled = true;
                textBoxcodBoala.Enabled = true;
                pictureBoxCheckDiagnostice.Visible = true;
                pictureBoxCancelDiagnostice.Visible = true;
            }
        }

        private void buttonClearDiagnostice_Click(object sender, EventArgs e)
        {
            labelidDiagnostic_print.Text = "";
            textBoxidPacient.Text = "";
            textBoxidConsultatie.Text = "";
            textBoxcodBoala.Text = "";
        }

        private void pictureBoxCheckDiagnostice_Click(object sender, EventArgs e)
        {
            if (addOrUpdate == true)
            {
                String queryString = String.Format(@"UPDATE DIAGNOSTICE_DOCTOR 
                                                SET idConsultatie = :newCons,
                                                    codBoala = :newCod
                                                WHERE idDiagnostic = " + labelidDiagnostic_print.Text);
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newCons", textBoxidConsultatie.Text);
                        if (textBoxcodBoala.Text == "")
                            command.Parameters.Add("newCod", null);
                        else
                            command.Parameters.Add("newCod", textBoxcodBoala.Text);

                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date actualizate cu succes!", "Update.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadDiagnostice();
                            textBoxidPacient.Enabled = false;
                            textBoxidConsultatie.Enabled = false;
                            textBoxcodBoala.Enabled = false;
                            pictureBoxCheckDiagnostice.Visible = false;
                            pictureBoxCancelDiagnostice.Visible = false;
                            checkCheck(labelidDiagnostic_print.Text);
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
                String queryString = String.Format(@"INSERT INTO DIAGNOSTICE_DOCTOR(idDiagnostic, idConsultatie, codBoala)
                                                     VALUES ( :newDiag, :newCons, :newCod)");
                StringBuilder errorMessages = new StringBuilder();

                using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    try
                    {
                        command.Connection.Open();
                        command.Parameters.Add("newDiag", labelidDiagnostic_print.Text);
                        command.Parameters.Add("newCons", textBoxidConsultatie.Text);
                        if (textBoxcodBoala.Text == "")
                            command.Parameters.Add("newCod", null);
                        else
                            command.Parameters.Add("newCod", textBoxcodBoala.Text);

                        int rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated > 0)
                        {
                            MessageBox.Show("Date inserate cu succes!", "Insert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadDiagnostice();
                            textBoxidPacient.Enabled = false;
                            textBoxidConsultatie.Enabled = false;
                            textBoxcodBoala.Enabled = false;
                            pictureBoxCheckDiagnostice.Visible = false;
                            pictureBoxCancelDiagnostice.Visible = false;
                            checkCheck(labelidDiagnostic_print.Text);
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

        private void pictureBoxCancelDiagnostice_Click(object sender, EventArgs e)
        {
            textBoxidConsultatie.Undo();
            textBoxcodBoala.Undo();
            textBoxidPacient.Enabled = false;
            textBoxidConsultatie.Enabled = false;
            textBoxcodBoala.Enabled = false;
            pictureBoxCheckDiagnostice.Visible = false;
            pictureBoxCancelDiagnostice.Visible = false;
        }

        private void comboBoxColoaneDiagnostice_SelectedIndexChanged(object sender, EventArgs e)
        {
            String queryString = String.Format(@"SELECT * FROM DIAGNOSTICE_DOCTOR WHERE codParafa = " + doctorLogin.get_codParafa() + " ORDER BY " + comboBoxColoaneDiagnostice.SelectedItem.ToString());
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
                    dataGridViewDiagnostice.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewDiagnostice);
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

        private void comboBoxOrdineDiagnostice_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ord;
            if (comboBoxOrdineDiagnostice.SelectedIndex == 0)
            {
                ord = "ASC";
            }
            else
            {
                ord = "DESC";
            }

            String queryString = String.Format(@"SELECT * FROM DIAGNOSTICE_DOCTOR WHERE codParafa = " + doctorLogin.get_codParafa() + " ORDER BY " + comboBoxColoaneDiagnostice.SelectedItem.ToString() + " " + ord);
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
                    dataGridViewDiagnostice.DataSource = dataTable;

                    autoSizeDataGridView(dataGridViewDiagnostice);
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

        private void buttonStergeDiagnostice_Click(object sender, EventArgs e)
        {
            String queryString = String.Format(@"DELETE FROM DIAGNOSTICE_DOCTOR
                                                WHERE idDiagnostic = " + labelidDiagnostic_print.Text);
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    switch (MessageBox.Show("Ștergerea acestui diagnostic poate duce la ștergerea sau modificarea unor date care au legătură cu respectiva intrare. Continuați?",
                        "ATENȚIE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                    {
                        case DialogResult.Yes:
                            {
                                command.Connection.Open();
                                int rowsUpdated = command.ExecuteNonQuery();

                                if (rowsUpdated > 0)
                                {
                                    MessageBox.Show("Date șterse cu succes!", "Delete.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    loadDiagnostice();
                                    textBoxidPacient.Enabled = false;
                                    textBoxidConsultatie.Enabled = false;
                                    textBoxcodBoala.Enabled = false;
                                    pictureBoxCheckDiagnostice.Visible = false;
                                    pictureBoxCancelDiagnostice.Visible = false;
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

        private void buttonAdaugaDiagnostice_Click(object sender, EventArgs e)
        {
            addOrUpdate = false;
            textBoxidConsultatie.Enabled = true;
            textBoxcodBoala.Enabled = true;
            pictureBoxCheckDiagnostice.Visible = true;
            pictureBoxCancelDiagnostice.Visible = true;

            labelidConsultatie_print.Text = "";

            String queryString = String.Format(@"SELECT DIAGNOSTICE_idDiagnostic_SEQ.NEXTVAL FROM DUAL");
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    labelidDiagnostic_print.Text = command.ExecuteScalar().ToString();

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

        void AddSimptom(string idDiag, string codSim)
        {
            String queryString = String.Format(@"INSERT INTO DIAGNOSTICE_SIMPTOME VALUES(" + idDiag + ", " + codSim + ")");
            
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();
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

        void DeleteSimptom(string idDiag, string codSim)
        {
            String queryString = String.Format(@"DELETE FROM DIAGNOSTICE_SIMPTOME WHERE idDiagnostic = " + idDiag + " AND codSimptom = " + codSim);
            StringBuilder errorMessages = new StringBuilder();

            using (OracleConnection connection = new OracleConnection(StartApp.connectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                try
                {
                    command.Connection.Open();
                    command.ExecuteNonQuery();
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

        private void buttonConfirmaSimptome_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < checkedListBoxSimptome.Items.Count; i++)
            {
                string cod = checkedListBoxSimptome.Items[i].ToString().Substring(0, checkedListBoxSimptome.Items[i].ToString().IndexOf(". "));
                if (checkedListBoxSimptome.GetItemChecked(i) == true)
                    AddSimptom(labelidDiagnostic_print.Text, cod); 
                else
                    DeleteSimptom(labelidDiagnostic_print.Text, cod);
            }
        }

        #endregion
    }
}
