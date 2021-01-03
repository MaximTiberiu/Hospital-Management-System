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
    }
}
