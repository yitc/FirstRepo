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
using BIS.Business;
using System.Resources;
using Telerik.WinControls.UI;
using System.IO;


namespace GUI
{
    public partial class frmContacts : frmTemplate
    {
        private int iTask;
        public int iCLient;
        private int idContPers;
        public int idCl;
        private int idArr = -1;
        private string nameArr="";
        public ContactsModel model;
        List<ContactReasonModel> reason;
        List<ContactTypeModel> types;
        //public EmployeeModel modelEmp;
        public PersonModel modelPers;
        public ClientModel modelCli;
        //public ClientModel modelCli;
        private string novi;
        private string sfrom;
        public bool modelChanged = false;
        public int newID;
        List<ToDoModel> modelToDo;
        private string layoutTasks;
        ContextMenuStrip tasksStripMenu = new ContextMenuStrip();
        public string Namef;
        public bool isOK = false;
        public int ixContact = 0;

        public frmContacts()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Contact");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            InitializeComponent();
        }
        public frmContacts(int iDContact, string what, int idPerson)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Contact");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            
            InitializeComponent();

            novi = what;
            iTask = iDContact;
            idContPers = idPerson;            

            setTranslation();
        }

        // isto kao gore samo u arrangement za NEW CONTACT da se koristi
        public frmContacts(int iDContact, string what, int idPerson, int idArrangement)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Contact");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;

            InitializeComponent();

            novi = what;
            iTask = iDContact;
            idContPers = idPerson;
            idArr = idArrangement;

            setTranslation();
        }

        public frmContacts(int iDContact, string what, string stype, int idClient, int IdPerson)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Contact");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            InitializeComponent();

            novi = what;
            sfrom = stype;
            iTask = iDContact;
            iCLient = idClient;
            idContPers = IdPerson;            

            setTranslation();
        }

        public frmContacts(int iDContact, string what, string stype, int idClient, int IdPerson, int idArrangement)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Contact");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            InitializeComponent();

            novi = what;
            sfrom = stype;
            iTask = iDContact;
            iCLient = idClient;
            idContPers = IdPerson;
            idArr = idArrangement;

            setTranslation();
        }

        private void frmContacts_Load(object sender, EventArgs e)
        {
            radRibbonDocuments.Visibility = ElementVisibility.Collapsed;
            btnReport.Visibility = ElementVisibility.Collapsed;
            btnEmail.Visibility = ElementVisibility.Collapsed;
            btnWord.Visibility = ElementVisibility.Collapsed;
            btnNewMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Visible;
            btnDelTask.Visibility = ElementVisibility.Collapsed;
            radRibbonMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;


            layoutTasks = MainForm.gridFiltersFolder + "\\layoutPersonTasks.xml";

            tasksStripMenu.Items.Add("Save Layout", null, TasksMenuSaveClick);
            rgvTasks.ContextMenuStrip = null;


            //add reason
            ContactReasonBUS tb = new ContactReasonBUS();
            reason = tb.GetAllContactReason(Login._user.lngUser);

            ddlReason.DataSource = reason;
            ddlReason.DisplayMember = "descContactReason";
            ddlReason.ValueMember = "idContactReason";

            //add type
            ContactTypeBUS tb1 = new ContactTypeBUS();
            types = tb1.GetAllContactType(Login._user.lngUser);

            ddlContactType.DataSource = types;
            ddlContactType.DisplayMember = "descContactType";
            ddlContactType.ValueMember = "idContactType";

            // enable , disable button clik for person or client
            if (sfrom == "client")
            {
                btnClient.Enabled = false;                
            }
            else if (sfrom == "person")
            {
                btnPerson.Enabled = false;                
            }

            if (iTask != -1)
            {
                model = new ContactsBUS().GetContactById(iTask);

                txtContactId.Text = model.idContact.ToString();
                txtReasonText.Text = model.reasonContact.ToString();
                txtNoteContact.Text = model.noteContact.ToString();
                txtCreator.Text = model.idCreator.ToString();
                if (sfrom != null && sfrom != "")
                {
             // txtClient.Text = iCLient;
                }
                else
                {
                    txtClient.Text = model.idClient.ToString();
                }
                txtProject.Text = model.idProject.ToString();
                txtPerson.Text = model.idContPers.ToString();
                dtDate.Text = model.dateContact.ToString();
                tpOpenTime.Value = Convert.ToDateTime(model.openTimeContact.ToString());
                tpCloseTime.Value = Convert.ToDateTime(model.closeTimeCOntact.ToString());
                tpDuration.Value = Convert.ToDateTime(model.durationContact.ToString());
                if (model.idContactReason != 0 && model.idContactReason != null)
                    ddlReason.SelectedItem = ddlReason.Items[reason.FindIndex(item => item.idContactReason == model.idContactReason)];
                if (model.idContactType != 0 && model.idContactType != null)
                    ddlContactType.SelectedItem = ddlContactType.Items[types.FindIndex(item => item.idContactType == model.idContactType)];
                // ucitava podatke o taskovima koji su napravljeni od ovog Contacta
                ToDoBUS tdb = new ToDoBUS();
                modelToDo = tdb.GetToDoContact(iTask);
                rgvTasks.DataSource = null;
                rgvTasks.DataSource = modelToDo;

                if (model.nameArrangement != "")
                    txtArrangement.Text = model.nameArrangement;

                idArr = model.idArrangement;

                ClientBUS ucl = new ClientBUS();
                ClientModel cmo = new ClientModel();
                if (model.idClient != null)
                {
                    if (model.idClient != 0)
                    {
                        idCl = Convert.ToInt32(model.idClient);
                        cmo = ucl.GetClient(idCl);
                        txtClient.Text = cmo.nameClient;
                    }

                       // txtClient.Text = ucl.GetClient(Convert.ToInt32(model.idClient)).nameClient;
                }

                if (idContPers != 0)
                {
                    modelPers = new PersonBUS().GetPerson(idContPers);
                    txtPerson.Text = modelPers.fullname;
                }


                EmployeeBUS ub = new EmployeeBUS();
                EmployeeModel em = new EmployeeModel();
                if (model.idCreator != null && model.idCreator != null)
                {
                    if (model.idCreator != 0)
                        em = ub.GetEmployee(Convert.ToInt32(model.idCreator),Login._user.lngUser);
                    txtCreator.Text = em.firstNameEmployee + " " + em.lastNameEmployee;
                }
            }
            else
            {
                model = new ContactsModel();
                if (idContPers != 0)
                {
                    modelPers = new PersonBUS().GetPerson(idContPers);
                    txtPerson.Text = modelPers.fullname;
                    model.idContPers = idContPers;
                }
                if (sfrom != null && sfrom != "")
                {
                    ClientBUS cb = new ClientBUS();
                    ClientModel cm = new ClientModel();
                    cm = cb.GetClient(iCLient);
                    if (cm != null)
                    {
                        txtClient.Text = cm.nameClient;
                        model.idClient = cm.idClient;
                    }
                }
                else
                {
                    txtClient.Text = iCLient.ToString();
                    model.idClient = iCLient;
                }

                if(idArr > 0)
                {
                    ArrangementBUS abus = new ArrangementBUS();
                    ArrangementModel amod = new ArrangementModel();

                    amod = abus.GetArrangementById(idArr);
                    if(amod != null)
                    {
                        txtArrangement.Text = amod.nameArrangement;
                    }
                }
                model.idCreator = Login._user.idEmployee;
                EmployeeBUS ub = new EmployeeBUS();
                EmployeeModel em = new EmployeeModel();
                if (model.idCreator != null && model.idCreator != null)
                {
                    if (model.idCreator != 0)
                        em = ub.GetEmployee(Convert.ToInt32(model.idCreator), Login._user.lngUser);
                    txtCreator.Text = em.firstNameEmployee + " " + em.lastNameEmployee;
                }
                model.dateContact = System.DateTime.Now;
                dtDate.Text = System.DateTime.Now.ToString();
                string snow = System.DateTime.Now.ToString();
               
                tpOpenTime.Value = Convert.ToDateTime(snow);
             

               

            }
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            ClientBUS ClientBUS = new ClientBUS();
            List<IModel> gm2 = new List<IModel>();

            gm2 = ClientBUS.GetAllClients(Login._user.lngUser);
            var dlgSave = new GridLookupForm(gm2, "Client");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                ClientModel genm2 = new ClientModel();
                genm2 = (ClientModel)dlgSave.selectedRow;
                //set textbox
                txtClient.Text = genm2.nameClient.Trim();
                //set model
                model.idClient = genm2.idClient;
            }
        }

        private void btnPerson_Click(object sender, EventArgs e)
        {
           // PersonModel m = (PersonModel)varijablaIModela;
            PersonBUS pbs = new PersonBUS();
            List<IModel> gm = new List<IModel>();

            gm = pbs.GetPersonsNoFilter();
            var dlgSave = new GridLookupForm(gm, "Person");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                PersonModel genm = new PersonModel();
                genm = (PersonModel)dlgSave.selectedRow;
                // set textbox
                txtPerson.Text = genm.fullname;
                //update model
                model.idContPers = genm.idContPers;
            }
        }

        private void btnProject_Click(object sender, EventArgs e)
        {
            // nije implementirano
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            isOK = false;
            try
            {
                if (iTask == -1)
                {
                    string snowEnd = System.DateTime.Now.ToString();

                    tpCloseTime.Value = Convert.ToDateTime(snowEnd);
                    model.idCreator = Login._user.idEmployee;
                }

                    DateTime prob = DateTime.Parse(tpOpenTime.Value.ToString());
                    TimeSpan ot = new TimeSpan(prob.Hour, prob.Minute, prob.Second);
                    model.openTimeContact = ot;

                    DateTime prob1 = DateTime.Parse(tpCloseTime.Value.ToString());
                    TimeSpan ct = new TimeSpan(prob1.Hour, prob1.Minute, prob1.Second);
                    model.closeTimeCOntact = ct;

                    model.durationContact = ct.Subtract(ot);
                  
               
                model.idContactReason = Convert.ToInt32(ddlReason.SelectedValue);
                model.idContactType = Convert.ToInt32(ddlContactType.SelectedValue);
                if ((txtNoteContact.Text).ToString() != "")
                    model.noteContact = txtNoteContact.Text;
                if ((txtProject.Text).ToString() != "")
                    model.idProject = Convert.ToInt32(txtProject.Text);
                if ((txtReasonText.Text).ToString() != "")
                    model.reasonContact = txtReasonText.Text;
                model.idProject = 0;
                if (model.idClient == null)
                    model.idClient = 0;
                if (model.idContPers == null)
                    model.idContPers = 0;
                model.idContPers = idContPers;
               // model.idCreator = Login._user.idEmployee;
                model.idArrangement = idArr;
                model.nameArrangement = nameArr;

                ContactsBUS bus = new ContactsBUS();
                if (novi != "new")
                {
                    model.idContact = iTask;
                    isOK = bus.Update(model, iTask, this.Name, Login._user.idUser);
                    modelChanged = true;
                    if (isOK == true)
                        RadMessageBox.Show("Updated");
                }
                else
                {
                    if (iTask == -1)
                    {
                       isOK= bus.Save(model, this.Name, Login._user.idUser);
                        modelChanged = true;
                        if (isOK == true)
                            RadMessageBox.Show("Saved");
                        ContactsBUS bus1 = new ContactsBUS();
                        newID = bus1.GetID(model);
                        txtContactId.Text = newID.ToString();
                        iTask = newID;
                        ixContact = newID;
                        modelChanged = true;
                    }
                    else
                    {  // ovo je ubaceno radi kreiranja novog taska iz contacta pa da ne upisuje duplo vec da azurira
                        if(txtContactId.Text != "")
                            iTask = Convert.ToInt32(txtContactId.Text.ToString());
                        else
                             model.idContact = iTask;
                        isOK=bus.Update(model, iTask, this.Name, Login._user.idUser);
                        modelChanged = true;
                        if (isOK == true)
                            RadMessageBox.Show("Updated");
;
                    }
                }
              //  modelChanged = true;
               // this.Close();
            }
            catch (Exception ex)
            {
                RadMessageBox.Show("Error saving.\nMessage: " + ex.Message, "Save", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {

        //    btnSave_Click();
            // new Task
             //ixContact = Convert.ToInt32(model.idContact);
            if (ixContact == null || ixContact == 0 && txtContactId.Text== "")
                    {
               // btnSave_Click(sender, e);
                         string snowEnd = System.DateTime.Now.ToString();

                            tpCloseTime.Value = Convert.ToDateTime(snowEnd);
                            model.idCreator = Login._user.idEmployee;
           

                            DateTime prob = DateTime.Parse(tpOpenTime.Value.ToString());
                            TimeSpan ot = new TimeSpan(prob.Hour, prob.Minute, prob.Second);
                            model.openTimeContact = ot;

                            DateTime prob1 = DateTime.Parse(tpCloseTime.Value.ToString());
                            TimeSpan ct = new TimeSpan(prob1.Hour, prob1.Minute, prob1.Second);
                            model.closeTimeCOntact = ct;

                            model.durationContact = ct.Subtract(ot);
                  
               
                        model.idContactReason = Convert.ToInt32(ddlReason.SelectedValue);
                        model.idContactType = Convert.ToInt32(ddlContactType.SelectedValue);
                        if ((txtNoteContact.Text).ToString() != "")
                            model.noteContact = txtNoteContact.Text;
                        if ((txtProject.Text).ToString() != "")
                            model.idProject = Convert.ToInt32(txtProject.Text);
                        if ((txtReasonText.Text).ToString() != "")
                            model.reasonContact = txtReasonText.Text;
                        model.idProject = 0;
                        if (model.idClient == null)
                            model.idClient = 0;
                        if (model.idContPers == null)
                            model.idContPers = 0;
                      //  model.idContPers = idContPers;
                       // model.idCreator = Login._user.idEmployee;

                        ContactsBUS bus1 = new ContactsBUS();
                        newID = bus1.SaveID(model, this.Name, Login._user.idUser);
                        txtContactId.Text = newID.ToString();
                        iTask = newID;
                        ixContact = newID;
                        modelChanged = true;

                       
                 }
            string what = "new";
            int ixClient;
            string stype = "client";
            int ixPerson;
            int iID = -1;
            if (txtContactId.Text != "")
                ixContact = Convert.ToInt32(txtContactId.Text);
            ixClient = Convert.ToInt32(model.idClient);
            ixPerson = Convert.ToInt32(model.idContPers);
            frmTasks frm = new frmTasks(iID, ixPerson, what, stype, ixClient, ixContact);
            frm.ShowDialog();
          
                ToDoBUS tdb = new ToDoBUS();
                modelToDo = tdb.GetToDoContact(iTask);
                rgvTasks.DataSource = null;
                rgvTasks.DataSource = modelToDo;
            
            
           
        }
        private void setTranslation()
        {

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                //.Text = resxSet.GetString("Contact id");
                //.Text = resxSet.GetString("Contact type");
                //.Text = resxSet.GetString("Reason");
                //.Text = resxSet.GetString("Reason text");
                //.Text = resxSet.GetString("Date");
                //lblOpenTime.Text = resxSet.GetString("Open time");
                //lblCloseTime.Text = resxSet.GetString("Close time");
                //lblDuration.Text = resxSet.GetString("Duration");
                //lblPerson.Text = resxSet.GetString("Person");
                //lblClient.Text = resxSet.GetString("Client");
                //lblProject.Text = resxSet.GetString("Project");
                //lblCreator.Text = resxSet.GetString("Creator");
                //lblDesription.Text = resxSet.GetString("Description");
                //btnSave.Text = resxSet.GetString("Save");
                if (resxSet.GetString(lblIdContact.Text) != null)
                    lblIdContact.Text = resxSet.GetString(lblIdContact.Text);

                if (resxSet.GetString(lblType.Text) != null)
                    lblType.Text = resxSet.GetString(lblType.Text);

                if (resxSet.GetString(lblReason.Text) != null)
                    lblReason.Text = resxSet.GetString(lblReason.Text);

                if (resxSet.GetString(lblReasonText.Text) != null)
                    lblReasonText.Text = resxSet.GetString(lblReasonText.Text);

                if (resxSet.GetString(lblDate.Text) != null)
                    lblDate.Text = resxSet.GetString(lblDate.Text);

                if (resxSet.GetString(lblOpenTime.Text) != null)
                    lblOpenTime.Text = resxSet.GetString(lblOpenTime.Text);

                if (resxSet.GetString(lblCloseTime.Text) != null)
                    lblCloseTime.Text = resxSet.GetString(lblCloseTime.Text);

                if (resxSet.GetString(lblDuration.Text) != null)
                    lblDuration.Text = resxSet.GetString(lblDuration.Text);

                if (resxSet.GetString(lblPerson.Text) != null)
                    lblPerson.Text = resxSet.GetString(lblPerson.Text);

                if (resxSet.GetString(lblClient.Text) != null)
                    lblClient.Text = resxSet.GetString(lblClient.Text);

                if (resxSet.GetString(lblProject.Text) != null)
                    lblProject.Text = resxSet.GetString(lblProject.Text);

                if (resxSet.GetString(lblCreator.Text) != null)
                    lblCreator.Text = resxSet.GetString(lblCreator.Text);

                if (resxSet.GetString(lblDesription.Text) != null)
                    lblDesription.Text = resxSet.GetString(lblDesription.Text);

                if (resxSet.GetString(btnSave.Text) != null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);

                if (resxSet.GetString(lblArrangement.Text) != null)
                    lblArrangement.Text = resxSet.GetString(lblArrangement.Text);
               

            }
        }

        private void rgvTasks_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvTasks.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvTasks.Columns[i].HeaderText != null && resxSet.GetString(rgvTasks.Columns[i].HeaderText) != null)
                        rgvTasks.Columns[i].HeaderText = resxSet.GetString(rgvTasks.Columns[i].HeaderText);
                }
            }
            if (File.Exists(layoutTasks))
            {
                rgvTasks.LoadLayout(layoutTasks);
            }
            //else
            //{
            //    rgvTasks.Columns["idToDo"].IsVisible = false;
            //    rgvTasks.Columns["idClient"].IsVisible = false;
            //    rgvTasks.Columns["idContPers"].IsVisible = false;
            //    rgvTasks.Columns["idProject"].IsVisible = false;
            //    rgvTasks.Columns["idOwner"].IsVisible = false;
            //    rgvTasks.Columns["idEmployee"].IsVisible = false;
            //    rgvTasks.Columns["idPriorityToDo"].IsVisible = false;
            //    rgvTasks.Columns["idStatusToDo"].IsVisible = false;
            //    rgvTasks.Columns["idToDoType"].IsVisible = false;
            //    rgvTasks.Columns["idContact"].IsVisible = false;
            //}
        }

        private void TasksMenuSaveClick(object sender, EventArgs e)
        {
            //SAVE NOTE
            if (File.Exists(layoutTasks))
            {
                File.Delete(layoutTasks);
            }
            rgvTasks.SaveLayout(layoutTasks);
        }

        private void rgvTasks_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.rgvTasks.CurrentRow;
            if (info != null && e.RowIndex >= 0)
            {
                if (rgvTasks.Rows.Count > 0)
                {
                    int iID = Int32.Parse(rgvTasks.SelectedRows[0].Cells["idToDo"].Value.ToString());
                    int idConstPers = Int32.Parse(rgvTasks.SelectedRows[0].Cells["idContPers"].Value.ToString());

                    string what = "open";
                    frmTasks frm = new frmTasks(iID, what, idConstPers);
                    frm.ShowDialog();
                    if (frm.modelChanged == true)
                    {
                        ToDoBUS nbus = new ToDoBUS();

                        modelToDo = nbus.GetToDoContact(iTask);
                        rgvTasks.DataSource = null;
                        rgvTasks.DataSource = modelToDo;
                    }

                }
            }
        }

        private void btnArrangement_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            ArrangementBUS bus = new ArrangementBUS();
            List<IModel> gm = new List<IModel>();
            gm = bus.GetAllArrangements();

            var dlgSave = new GridLookupForm(gm, "Arrangement");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                ArrangementModel m = new ArrangementModel();
                m = (ArrangementModel)dlgSave.selectedRow;
                //set textbox
                txtArrangement.Text = m.nameArrangement.ToString();
              //  txtIdArrangement.Text = m.idArrangement.ToString();
                //update model
                idArr = m.idArrangement;
               nameArr = m.nameArrangement;
            }
            Cursor.Current = Cursors.Default;
        }
    }
}
