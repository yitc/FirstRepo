using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Model;
using BIS.DAO;
using System.Resources;
using BIS.Business;
using Telerik.WinControls.UI;

namespace GUI
{
    public partial class frmClientNotes : frmTemplate
    {
        private int iNoteID = -1;
        private int idCli;
        public ClientNotesModel model;
        public bool modelChanged = false;


        public frmClientNotes(int idClient)
        {            
            InitializeComponent();
            idCli = idClient;            
        }

        public frmClientNotes(int uiNoteID, int idClient)
        {
            InitializeComponent();

            iNoteID = uiNoteID;
            idCli = idClient;
        }

        private void frmClientNotes_Load(object sender, EventArgs e)
        {
            radRibbonDocuments.Visibility = ElementVisibility.Collapsed;
            btnReport.Visibility = ElementVisibility.Collapsed;
            btnEmail.Visibility = ElementVisibility.Collapsed;
            btnWord.Visibility = ElementVisibility.Collapsed;
            btnNewMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {                
                lbldtNoteDate.Text = resxSet.GetString("Note date");
                lblnoteText.Text = resxSet.GetString("Note text");
            }

            TypeNotesBUS types = new TypeNotesBUS();
            List<TypeNotesModel> modelTypes = new List<TypeNotesModel>();
            modelTypes = types.GetAllTypeNotes(Login._user.lngUser);
            comboTypeNotes.DataSource = modelTypes;
            comboTypeNotes.DisplayMember = "nameTypeNote";
            comboTypeNotes.ValueMember = "idTypeNote";

            if (iNoteID != -1)
            {
                model = new ClientNotesBUS().GetClientNote(iNoteID);
                datdtNoteDate.Text = model.dtNoteDate.ToString();
                txtnoteText.Text = model.noteText.ToString();

                comboTypeNotes.SelectedValue = model.idTypeNote;

            }
            else
            {
                datdtNoteDate.Text = DateTime.Now.ToString();
            }


          
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                model = new ClientNotesModel();

                model.idTypeNote = (int) comboTypeNotes.SelectedValue;
                model.idClient = idCli;                
                model.idEmployee = Login._user.idEmployee; // ???????                
                model.noteText = txtnoteText.Text;
                model.idUserModified = Login._user.idUser;

                ClientNotesBUS bus = new ClientNotesBUS();
                if(iNoteID != -1)
                {

                    model.dtNoteDate = DateTime.Parse(datdtNoteDate.Text);
                    model.idNote = iNoteID;
                    model.dtModified = DateTime.Now;
                    model.idUserModified = Login._user.idUser;
                    bus.Update(model, this.Name, Login._user.idUser);
                    modelChanged = true;
                }
                else
                {
                    model.dtNoteDate = DateTime.Now;
                    model.dtCreated = DateTime.Now;                    
                    model.idUserCreated = Login._user.idUser;
                    bus.Save(model, this.Name, Login._user.idUser);
                    modelChanged = true;
                }
                
                //RadMessageBox.Show("Note successfully saved.", "Save", MessageBoxButtons.OK, RadMessageIcon.Info);
                this.Close();
            }
            catch (Exception ex)
            {
                RadMessageBox.Show("Error saving.\nMessage: " + ex.Message, "Save", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void numidNote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnDeleteMemo_Click(object sender, EventArgs e)
        {
            ClientNotesBUS bus = new ClientNotesBUS();
            bus.Delete(iNoteID, this.Name, Login._user.idUser);
            modelChanged = true;
            this.Close();
        }
    }

   // public delegate void AppointmentRefreshEventHandler(Object sender, EventArgs e);
   // public delegate void GridRefreshEventHandler(Object sender, GridRefreshEventArgs e);

    //public class GridRefreshEventArgs : EventArgs
    //{
    //    public Int32 iFieldID { get; set; }
    //}
}
