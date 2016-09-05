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
using System.Threading;

namespace GUI
{
    public partial class frmTasks : frmTemplate
    {

        private int iTask;
        private int iCLient;
        private int idContPers;
        private int ixContact;
        public int idArr = -1;
        private string nameArr = "";        
        public ToDoModel model;
        public EmployeeModel modelEmp;
        public PersonModel modelPers;
        public ClientModel modelCli;
        private string novi;
        private string sfrom;
        public bool modelChanged = false;
        public int idTsk;
        List<ToDoPriorityModel> priority;
        // List<ToDoTypeModel> status;
        //private int identDToDo;
        //private int idConstPers;
        //public ToDoModel model;
        //public bool modelChanged = false;
        //List<ToDoModel> layouts;
        List<ToDoStatusModel> status;
        // List<ToDoPriorityModel> priority;
        List<ToDoTypeModel> type;
        public string Namef;


        public frmTasks()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Task");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            InitializeComponent();
        }

        // public frmTasks(int idPerson)
        //{
        //     idContPers = idPerson; 
        //    InitializeComponent();
        //}

        public frmTasks(int idPerson, string what)
        {

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Task");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            InitializeComponent();

            novi = what;
            idContPers = idPerson;

        }

        public frmTasks(int iDToDo, string what, int idPerson)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Task");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            InitializeComponent();

            novi = what;
            iTask = iDToDo;
            idContPers = idPerson;

        }

        public frmTasks(int iID, int ixPerson, string what, string stype, int ixClient)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Task");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;

            InitializeComponent();

            novi = what;
            sfrom = stype;
            if (iID > 0)
            {
                iTask = iID;
            }
            else
            {
                iTask = -1;
            }
            idContPers = ixPerson;
            iCLient = ixClient;

        }
        public frmTasks(int iID, int ixPerson, string what, string stype, int ixClient, int iContact)
        {

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Task");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            InitializeComponent();

            novi = what;
            sfrom = stype;
            if (iID > 0)
            {
                iTask = iID;
            }
            else
            {
                iTask = -1;
            }
            idContPers = ixPerson;
            iCLient = ixClient;
            ixContact = iContact;
        }


        private void frmTasks_Load(object sender, EventArgs e)
        {
            radRibbonDocuments.Visibility = ElementVisibility.Collapsed;
            btnReport.Visibility = ElementVisibility.Collapsed;
            btnEmail.Visibility = ElementVisibility.Collapsed;
            btnWord.Visibility = ElementVisibility.Collapsed;
            btnNewMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;
            radRibbonMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;

            setTranslation();

            // ispituje da li je user manager i ako jeste enabluje dugme za izbor Responsiblea

            //Mitar and Aleksa - cause all users can change resposible person
            //if (Login._user.isUserManager == true)
            //    btnEmployee.Visible = true;
            //
            // ** Puni Combo box-ove

            //add priority
            ToDoPriorityBUS tb = new ToDoPriorityBUS();
            priority = tb.GetAllToDoPriority(Login._user.lngUser);

            ddlTaskPriority.DataSource = priority;
            ddlTaskPriority.DisplayMember = "descriptionPriority";
            ddlTaskPriority.ValueMember = "idPriorityToDo";

            ////add status
            ToDoStatusBUS tb1 = new ToDoStatusBUS();
            status = tb1.GetAllToDoStatus(Login._user.lngUser);

            ddlStatus.DataSource = status;
            ddlStatus.DisplayMember = "descriptionStatus";
            ddlStatus.ValueMember = "idStatusToDo";

            ////add type
            ToDoTypeBUS tb2 = new ToDoTypeBUS();
            type = tb2.GetAllToDoTypes(Login._user.lngUser);

            ddlTaskType.DataSource = type;
            ddlTaskType.DisplayMember = "descriptionToDoType";
            ddlTaskType.ValueMember = "idToDoType";
            // enable disable buttons
            if (sfrom == "client")
            {
                btnClient.Enabled = false;
                btnPerson.Text = "...";
            }
            else if (sfrom == "person")
            {
                btnPerson.Enabled = false;
                btnClient.Text = "...";
            }

            if (novi != "new")
            //if (idContPers != -1)
            {

                model = new ToDoBUS().GetTaskById(iTask);
                //

                txtTaskId.Text = model.idToDo.ToString();


                string[] plt;
                if (model.planedTime.ToString().Contains(","))
                    plt = Convert.ToString(model.planedTime).Split(',');
                else
                    plt = Convert.ToString(model.planedTime).Split('.');


                if (plt.Length > 1)
                {
                    if ((plt[0]).ToString() != "")
                    {

                        decimal d1 = Convert.ToDecimal(plt[0]);
                        decimal d2 = Convert.ToDecimal(plt[1]);
                        string strP1 = d1.ToString("##");
                        if (strP1.Length == 1 || strP1.Length == 0)
                            strP1 = "0" + strP1;
                        if (strP1 == "")
                            strP1 = "00";
                        string strP2 = d2.ToString("##");
                        if (strP2 == "")
                            strP2 = "00";
                        string s = strP1 + ":" + strP2;
                        txtPlanedTime.NullText = "0";
                        txtPlanedTime.Text = strP1 + ":" + strP2;
                    }
                }

                string[] plt1;

                if (model.actualTime.ToString().Contains(","))
                    plt1 = Convert.ToString(model.actualTime).Split(',');
                else
                    plt1 = Convert.ToString(model.actualTime).Split('.');

                if (plt1.Length > 1)
                {
                    if ((plt1[0]).ToString() != "")
                    {

                        decimal d1 = Convert.ToDecimal(plt1[0]);
                        decimal d2 = Convert.ToDecimal(plt1[1]);
                        string strP1 = d1.ToString("##");
                        if (strP1.Length == 1 || strP1.Length == 0)
                            strP1 = "0" + strP1;
                        if (strP1 == "")
                            strP1 = "00";
                        string strP2 = d2.ToString("##");
                        if (strP2 == "")
                            strP2 = "00";
                        string s = strP1 + ":" + strP2;
                        txtxActualTime.NullText = "0";
                        txtxActualTime.Text = strP1 + ":" + strP2;

                    }
                }



                if (model.idToDoType != 0 && model.idToDoType != null)
                    ddlTaskType.SelectedItem = ddlTaskType.Items[type.FindIndex(item => item.idToDoType == model.idToDoType)];

                if (model.idPriorityToDo != 0 && model.idPriorityToDo != null)
                    ddlTaskPriority.SelectedItem = ddlTaskPriority.Items[priority.FindIndex(item => item.idPriorityToDo == model.idPriorityToDo)];
                dtOpenDate.Text = model.dtOpenDate.ToString();
                dtCloseDate.Text = model.dtCloseDate.ToString();
                dtEndDate.Text = model.dtEndDate.ToString();

                if (model.idStatusToDo != 0 && model.idStatusToDo != null)
                    ddlStatus.SelectedItem = ddlStatus.Items[status.FindIndex(item => item.idStatusToDo == model.idStatusToDo)];
                ClientBUS ucl = new ClientBUS();
                if (model.idClient != null)
                {
                    if (model.idClient != 0)
                    {
                        modelCli = ucl.GetClient(Convert.ToInt32(model.idClient));
                        txtClient.Text = modelCli.nameClient;
                    }

                }

                if (idContPers != 0)
                {
                    modelPers = new PersonBUS().GetPerson(idContPers);
                    txtPerson.Text = modelPers.fullname;
                    model.idContPers = modelPers.idContPers;
                }

                if (idArr > 0)
                {
                    ArrangementBUS abus = new ArrangementBUS();
                    ArrangementModel amod = new ArrangementModel();

                    amod = abus.GetArrangementById(idArr);
                    if (amod != null)
                    {
                        txtArrangement.Text = amod.nameArrangement;
                    }
                }

                txtProject.Text = model.idProject.ToString();
                txtResponsibile.Text = model.idEmployee.ToString();
                txtCreator.Text = model.idOwner.ToString();
                txtNote.Text = model.descriptionToDo.ToString();
                txtContactId.Text = model.idContact.ToString();

              
                if (model.nameArrangement != "" && model.nameArrangement != null)
                    txtArrangement.Text = model.nameArrangement.ToString();
               
                //dodato 15 8 2016              
                if (model.nameContPers != "" && model.nameContPers != null)
                    txtPerson.Text = model.nameContPers.ToString();

                EmployeeBUS ub = new EmployeeBUS();
                EmployeeModel em = new EmployeeModel();
                if (model.idEmployee != null && model.idEmployee != null)
                {
                    if (model.idEmployee != 0)
                        em = ub.GetEmployee(Convert.ToInt32(model.idEmployee), Login._user.lngUser);
                    txtResponsibile.Text = em.firstNameEmployee + " " + em.lastNameEmployee;
                }
                if (model.idOwner != null && model.idOwner != null)
                {
                    if (model.idEmployee != 0)
                        em = ub.GetEmployee(Convert.ToInt32(model.idOwner), Login._user.lngUser);
                    txtCreator.Text = em.firstNameEmployee + " " + em.lastNameEmployee;
                }
            }
            else
            {
                model = new ToDoModel();
                if (idContPers != 0)
                {
                    modelPers = new PersonBUS().GetPerson(idContPers);
                    txtPerson.Text = modelPers.fullname;
                    model.idContPers = idContPers;
                }
                if (iCLient != 0)
                {
                    modelCli = new ClientBUS().GetClient(iCLient);
                    txtClient.Text = modelCli.nameClient;
                    model.idClient = iCLient;
                }
                if (ixContact != null || ixContact != 0)
                    txtContactId.Text = ixContact.ToString();
                //    txtPerson.Text = idContPers.ToString();
                //  txtResponsibile.Text = Login._user.idEmployee.ToString();
                //  txtCreator.Text = Login._user.idEmployee.ToString();
                if (ixContact != 0 || ixContact != null)
                    model.idContact = ixContact;
                model.idEmployee = Login._user.idEmployee;
                model.idOwner = Login._user.idEmployee;


                //string sPl = "0,00";
                string sPl = "0" + Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator + "00";

                model.planedTime = Convert.ToDecimal(sPl);
                model.actualTime = Convert.ToDecimal(sPl);

                string[] plt;
                if (model.planedTime.ToString().Contains(","))
                    plt = Convert.ToString(model.planedTime).Split(',');
                else
                    plt = Convert.ToString(model.planedTime).Split('.');

                if (plt.Length > 1)
                {
                    //plt = Convert.ToString(model.planedTime).Split(',');
                    if ((plt[0]).ToString() != "")
                    {
                        txtPlanedTime.NullText = "0";
                        decimal d1 = Convert.ToDecimal(plt[0]);
                        decimal d2 = Convert.ToDecimal(plt[1]);
                        string strP1 = d1.ToString("##");
                        if (strP1.Length == 1)
                            strP1 = "0" + strP1;
                        string strP2 = d2.ToString("##");
                        string s = strP1 + ":" + strP2;
                        strP1 = "00";
                        strP2 = "00";
                        txtPlanedTime.Text = strP1 + ":" + strP2;
                    }
                }


                string[] plt1;
                if (model.actualTime.ToString().Contains(","))
                    plt1 = Convert.ToString(model.actualTime).Split(',');
                else
                    plt1 = Convert.ToString(model.actualTime).Split('.');

                //string[] plt1 = Convert.ToString(model.actualTime).Split(',');
                if (plt1.Length > 1)
                {
                    if ((plt1[0]).ToString() != "")
                    {
                        txtxActualTime.NullText = "0";
                        decimal d1 = Convert.ToDecimal(plt1[0]);
                        decimal d2 = Convert.ToDecimal(plt1[1]);
                        string strP1 = d1.ToString("##");
                        if (strP1.Length == 1)
                            strP1 = "0" + strP1;
                        string strP2 = d2.ToString("##");
                        string s = strP1 + ":" + strP2;
                        strP1 = "00";
                        strP2 = "00";
                        txtxActualTime.Text = strP1 + ":" + strP2;
                    }
                }

                dtOpenDate.Text = DateTime.Now.ToShortDateString();
                dtCloseDate.Text = DateTime.Now.ToShortDateString();
                dtEndDate.Text = DateTime.Now.ToShortDateString();
                //  TimeSpan pltime = Convert.ToDateTime(sttime);
                //string s = radDropDownListReminder.SelectedValue.ToString();
                //TimeSpan sp = TimeSpan.Parse(s);
                //  appModel.Reminder = new TimeSpan(sp.Hours, sp.Minutes, sp.Seconds);



                EmployeeBUS ub1 = new EmployeeBUS();
                EmployeeModel em1 = new EmployeeModel();
                if (model.idEmployee != null && model.idEmployee != null)
                {
                    if (model.idEmployee != 0)
                        em1 = ub1.GetEmployee(Convert.ToInt32(model.idEmployee), Login._user.lngUser);
                    txtResponsibile.Text = em1.firstNameEmployee + " " + em1.lastNameEmployee;

                }
                EmployeeModel em2 = new EmployeeModel();
                if (model.idOwner != null && model.idOwner != null)
                {
                    if (model.idOwner != 0)
                        em2 = ub1.GetEmployee(Convert.ToInt32(model.idOwner), Login._user.lngUser);
                    txtCreator.Text = em2.firstNameEmployee + " " + em2.lastNameEmployee;
                }

                if (idArr > 0)
                {
                    ArrangementBUS abus = new ArrangementBUS();
                    ArrangementModel amod = new ArrangementModel();

                    amod = abus.GetArrangementById(idArr);
                    if (amod != null)
                    {
                        txtArrangement.Text = amod.nameArrangement;
                    }
                }
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
            PersonBUS pbs = new PersonBUS();
            List<IModel> gm = new List<IModel>();

            gm = pbs.GetPersonsNoFilter();


            var dlgSave = new GridLookupForm(gm, "Person");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                PersonModel genm = new PersonModel();
                genm = (PersonModel)dlgSave.selectedRow;
                // set textbox
                txtPerson.Text = genm.fullname.Trim();
                //update model
                model.idContPers = genm.idContPers;
            }
        }


        private void btnProject_Click(object sender, EventArgs e)
        {

        }




        private void setTranslation()
        {

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                // lblIdTask.Text = resxSet.GetString("Task id");
                if (resxSet.GetString(lblIdTask.Text) != null)
                    lblIdTask.Text = resxSet.GetString(lblIdTask.Text);

                //lblType.Text = resxSet.GetString("Task type");
                if (resxSet.GetString(lblType.Text) != null)
                    lblType.Text = resxSet.GetString(lblType.Text);

                // lblPriority.Text = resxSet.GetString("Priority");
                if (resxSet.GetString(lblPriority.Text) != null)
                    lblPriority.Text = resxSet.GetString(lblPriority.Text);

                //  lblOpenDate.Text = resxSet.GetString("Open date");
                if (resxSet.GetString(lblOpenDate.Text) != null)
                    lblOpenDate.Text = resxSet.GetString(lblOpenDate.Text);

                //  lblCloseDate.Text = resxSet.GetString("Close date");
                if (resxSet.GetString(lblCloseDate.Text) != null)
                    lblCloseDate.Text = resxSet.GetString(lblCloseDate.Text);

                // lblEndDate.Text = resxSet.GetString("End date");
                if (resxSet.GetString(lblEndDate.Text) != null)
                    lblEndDate.Text = resxSet.GetString(lblEndDate.Text);

                //  lblPlanedTime.Text = resxSet.GetString("Planed time");
                if (resxSet.GetString(lblPlanedTime.Text) != null)
                    lblPlanedTime.Text = resxSet.GetString(lblPlanedTime.Text);

                // lblActualTime.Text = resxSet.GetString("Actual time");
                if (resxSet.GetString(lblActualTime.Text) != null)
                    lblActualTime.Text = resxSet.GetString(lblActualTime.Text);

                //  lblPerson.Text = resxSet.GetString("Person");
                if (resxSet.GetString(lblPerson.Text) != null)
                    lblPerson.Text = resxSet.GetString(lblPerson.Text);

                //  lblClient.Text = resxSet.GetString("Client");
                if (resxSet.GetString(lblClient.Text) != null)
                    lblClient.Text = resxSet.GetString(lblClient.Text);

                //  lblStatus.Text = resxSet.GetString("Status");
                if (resxSet.GetString(lblStatus.Text) != null)
                    lblStatus.Text = resxSet.GetString(lblStatus.Text);

                //  lblProject.Text = resxSet.GetString("Project");
                if (resxSet.GetString(lblProject.Text) != null)
                    lblProject.Text = resxSet.GetString(lblProject.Text);

                // lblResponsibile.Text = resxSet.GetString("Responsible");
                if (resxSet.GetString(lblResponsibile.Text) != null)
                    lblResponsibile.Text = resxSet.GetString(lblResponsibile.Text);

                // lblCreator.Text = resxSet.GetString("Creator");
                if (resxSet.GetString(lblCreator.Text) != null)
                    lblCreator.Text = resxSet.GetString(lblCreator.Text);

                // lblDesript.Text = resxSet.GetString("Description");
                if (resxSet.GetString(lblDesript.Text) != null)
                    lblDesript.Text = resxSet.GetString(lblDesript.Text);

                // lblContact.Text = resxSet.GetString("Contact relation");
                if (resxSet.GetString(lblContact.Text) != null)
                    lblContact.Text = resxSet.GetString(lblContact.Text);

                //   btnSave.Text = resxSet.GetString("Save");
                if (resxSet.GetString(btnSave.Text) != null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);

                if (resxSet.GetString(lblArrangement.Text) != null)
                    lblArrangement.Text = resxSet.GetString(lblArrangement.Text);

                //rbInOut.Text = resxSet.GetString("Ours");
                //rbIncome.Text = resxSet.GetString("Incoming");
                //rbNew.Text = resxSet.GetString("New document");
                //rbExisting.Text = resxSet.GetString("Add existing document");

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
               
                if ((dtOpenDate.Text).ToString() != "")
                    model.dtOpenDate = Convert.ToDateTime(dtOpenDate.Text);
                if ((dtCloseDate.Text).ToString() != "")
                    model.dtCloseDate = Convert.ToDateTime(dtCloseDate.Text);
                if ((dtEndDate.Text).ToString() != "")
                    model.dtEndDate = Convert.ToDateTime(dtEndDate.Text);

                string[] plt = txtPlanedTime.Text.Split(':');
                // kontrola za minute
                if (Convert.ToDecimal(plt[1]) > 59)
                {
                    //RadMessageBox.Show("You entered more minutes, than hour has ! Icrease hours, please");
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You entered more minutes, than hour has ! Icrease hours, please");
                    txtPlanedTime.Focus();
                    return;
                }
                if ((plt[0]).ToString() != "")
                    model.planedTime = Convert.ToDecimal(plt[0] + "," + plt[1]);
                string[] act = txtxActualTime.Text.Split(':');
                // kontrola za minute
                if (Convert.ToDecimal(plt[1]) > 59)
                {
                    //RadMessageBox.Show("You entered more minutes, than hour has ! Icrease hours, please");
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You entered more minutes, than hour has ! Icrease hours, please");
                    txtxActualTime.Focus();
                    return;
                }
                if ((txtxActualTime.Text).ToString() != "")
                    model.actualTime = Convert.ToDecimal(act[0] + "," + act[1]);

                model.idStatusToDo = Convert.ToInt32(ddlStatus.SelectedValue);
                model.idPriorityToDo = Convert.ToInt32(ddlTaskPriority.SelectedValue);
                model.idToDoType = Convert.ToInt32(ddlTaskType.SelectedValue);
                if ((txtContactId.Text).ToString() != "")
                    model.idContact = Convert.ToInt32(txtContactId.Text);
                if ((txtNote.Text).ToString() != "")
                    model.descriptionToDo = txtNote.Text;
                //if ((txtClient.Text).ToString() != "")
                //   model.idClient = Convert.ToInt32(txtClient.Text);
                //if ((txtProject.Text).ToString() != "")
                //   model.idProject = Convert.ToInt32(txtProject.Text);
                //if (model.idContPers == 0)
                //      model.idContPers = idContPers;
                //  model.idOwner = Login._user.idEmployee;

                //   model.idEmployee = Convert.ToInt32(txtResponsibile.Text);
                // model.idContact = Convert.ToInt32(txtContactId.Text);

                //model.idArrangement = idArr;
                //model.nameArrangement = nameArr;

               
                

                ToDoBUS bus = new ToDoBUS();
                if (novi != "new")
                {
                    model.idToDo = iTask;
                    bus.Update(model, iTask, this.Name, Login._user.idUser);
                }
                else
                {

                    idTsk = bus.Save(model, this.Name, Login._user.idUser);

                }
                modelChanged = true;
                this.Close();
            }
            catch (Exception ex)
            {
                RadMessageBox.Show("Error saving.\nMessage: " + ex.Message, "Save", MessageBoxButtons.OK, RadMessageIcon.Error);
            }


        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            EmployeeBUS EmployeeBUS = new EmployeeBUS();
            List<IModel> gm3 = new List<IModel>();

            gm3 = EmployeeBUS.GetAllEmpl(Login._user.lngUser);


            var dlgSave = new GridLookupForm(gm3, "Employee");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                EmployeeModel genm3 = new EmployeeModel();
                genm3 = (EmployeeModel)dlgSave.selectedRow;
                //set textbox
                txtResponsibile.Text = genm3.firstNameEmployee + " " + genm3.lastNameEmployee;
                //update model
                //   if (genm3.idEmployee != null)
                model.idEmployee = genm3.idEmployee; //Person.idGender = Convert.ToInt32(ddlGender.SelectedValue)
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

                if(model!=null)
                {
                    model.idArrangement = m.idArrangement;
                    model.nameArrangement = m.nameArrangement;
                }

                //Sakijev kod od ranije ne znamo da l se negde koristi
                idArr = m.idArrangement;
                nameArr = m.nameArrangement;

                

             
            }
            Cursor.Current = Cursors.Default;
        }
    }

}
