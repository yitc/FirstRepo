using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Model;
using System.Resources;
using BIS.Business;
using Telerik.WinControls.UI;
using Telerik.WinControls.Data;
using System.IO;
using Outlook = Microsoft.Office.Interop.Outlook;
using NUnit.Framework;
using GUI.User_Controls;
using System.Text.RegularExpressions;
using System.Linq;

namespace GUI
{
    [TestFixture]
    public partial class frmPerson : frmTemplate
    {
        PersonModel Person;
        PersonModel PersonFirst;
        PersonPassportModel persPassport;
        List<TitleModel> persTitle;
        List<NotesModel> personNotes;
        List<NotesModel> personNotes2;
        List<DocumentsModel> personDoc;
        List<PersonAddressModel> PersonAddress;
        List<LabelForPerson> PersonLabel;
        List<FilterForPerson> PersonFilter;
        List<PersonTelModel> persTel;
        List<PersonTelModel> persTelFirst;
        List<PersonEmailModel> persEmail;
        List<PersonEmailModel> persEmailFirst;
        List<DocumentsModel> persDoc;
        List<MedicalVoluntaryModel> personMedical;
        List<MedicalVoluntaryModel> personVoluntary;
        List<VolontaryFunctionModel> personVoluntary1; // drugi tab na volonterima
        List<VolontaryTripModel> personVoluntary2; // treci tab na volonterima
        List<MeetingsModel> personMeetings;
        List<ToDoModel> personToDo;
        List<DocumentsModel> objDoc;
        List<ContactsModel> personContacts;
        List<ArrangementAllModel> newArrangement = new List<ArrangementAllModel>(); // lista dodatih aranzmana
        List<ArrangementAllModel> AllArrangement;// Lista svih aranzmana iz baze 
        List<ArrangementAllModel> AllNewArrangement;// Lista svih aranzmana iz baze + dodati aranzmani
        BindingList<VolAvailabilityModel> volAvailabilityList;
        BindingList<ClientPersonModel> clientAndPersonList; //Lista svih ClientPersona
        AccDebCreModel debCre;
        AccountData pg;  // kontrola sa account gridovima 
        BindingList<VolFeaturesModel> volFeaturesList; //Lista VolFeatures
        CertificatesModel certificateModel;
        VolFeaturesModel volFeaturesModel;
        TrainingModel trainingModel;
        private List<PersonReasonInModel> gm10;
        private List<PersonReasonOutModel> gm11;
        private List<VoluntaryReasonInModel> gm12;
        private List<VoluntaryReasonOutModel> gm13;
        private AccSettingsModel asmo;

        private BindingList<AccIbanModel> ibanLista;

        //private int checkBoxes = 0;
        ContactPersonTripDataModel conPersTripDataModel;
        BindingList<ContactPersonTripDataModel> conPersTripDataList; //Lista za Travel data

        bool documentsLoaded = false;

        // contact person user control
        // ContactPersonAddress cpa;
        AdrOne cpa;
        bool showEmergencyAddressForFillPErsonData;

        private int idClientFromClientGrid; //promenljiva za clijenta

        private int idCertificateFromGrid; // promenljiva za Certifikate
        private int idTrainingFromGrid; // promenljiva za Training 
        private string xCertificateName;
        private int xidCertificate;
        private int xidTraining;
        private string xTrainingName;
        private int indTravVol = 0;  // indikator za pronalazanje Reason 0 traveler; 1 volonter

        //for saving voluntary helper
        Boolean isVolChanged = false;
        public decimal questSort;

        //for saving medical
        Boolean isMedChanged = false;
        public bool isSaved = false;
        public bool isOkAcc = false;

        ContextMenuStrip notesStripMenu = new ContextMenuStrip();
        ContextMenuStrip documentsStripMenu = new ContextMenuStrip();
        ContextMenuStrip meetingsStripMenu = new ContextMenuStrip();
        ContextMenuStrip contactsStripMenu = new ContextMenuStrip();
        ContextMenuStrip tasksStripMenu = new ContextMenuStrip();

        // Layout file names for all grids
        private string layoutDocuments;
        private string layoutMemo;
        private string layoutMeetings;
        private string layoutContacts;
        private string layoutTasks;
        private string xClientName;
        private int xidClient;
        private string layoutArrangement;
        public string layoutVolFeaturesView;
        public string layoutTravelDataView;
        private int lengthInitials = 0;
        private bool pageLoaded = false;

        DateTime dtOfActive;

        public frmPerson(IModel model)
        {
            Person = (PersonModel)model;
            PersonFirst = new PersonModel((PersonModel)model);
            this.Icon = Login.iconForm;

            string formName = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(this.Name.Replace("frm", "")) != null)
                    formName = formName + " " + resxSet.GetString(this.Name.Replace("frm", ""));
                else
                    formName = formName + " " + this.Name.Replace("frm", "");
            }

            if (Person.fullname != null)
                formName = formName + " " + Person.fullname + " [ " + Person.idContPers + " ] ";

            this.Text = formName;
            ribbonExampleMenu.Text = "";

            PersonPassportBUS cpb = new PersonPassportBUS();
            persPassport = cpb.GetPassport(Person.idContPers);

            personNotes = null;
            InitializeComponent();
            btnSave.Click += btnSaveUpdate_Click;
        }

        public frmPerson()
        {
            Person = new PersonModel();
            PersonFirst = new PersonModel();

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();
            InitializeComponent();
            btnSave.Click += btnSaveInsert_Click;

            for (int i = 0; i < pagePerson.Pages.Count; i++)
            {
                if (pagePerson.Pages[i].Name != "tabRelation")
                {
                    pagePerson.Pages[i].Enabled = false;
                }
            }


        }
        [Test]
        private void frmPerson_Load(object sender, EventArgs e)
        {
          
            btnReport.Visibility = ElementVisibility.Visible;
            radRibbonContact.Visibility = ElementVisibility.Visible;
            radRibbonTask.Visibility = ElementVisibility.Visible;
            btnDelContact.Visibility = ElementVisibility.Collapsed;
            btnDelTask.Visibility = ElementVisibility.Collapsed;
            btnDeleteDoc.Visibility = ElementVisibility.Collapsed;
            btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Visible;
            btnNewMeeting.Visibility = ElementVisibility.Visible;
            // btnNewContact.Visibility = ElementVisibility.Visible;
            //btnNewTask.Visibility = ElementVisibility.Visible;
            tabTravelData.Item.Visibility = ElementVisibility.Collapsed;

            RadMessageBox.SetThemeName("Windows8");

            layoutDocuments = MainForm.gridFiltersFolder + "\\layoutPersonDocuments.xml";
            layoutMemo = MainForm.gridFiltersFolder + "\\layoutPersonMemo.xml";
            layoutMeetings = MainForm.gridFiltersFolder + "\\layoutPersonMeetings.xml";
            layoutContacts = MainForm.gridFiltersFolder + "\\layoutPersonContacts.xml";
            layoutTasks = MainForm.gridFiltersFolder + "\\layoutPersonTasks.xml";
            layoutArrangement = MainForm.gridFiltersFolder + "\\layoutPersonArrangement.xml";
            //============Layout VolFeatures============
            layoutVolFeaturesView = MainForm.gridFiltersFolder + "\\layoutVolFeaturesView.xml";
            layoutTravelDataView = MainForm.gridFiltersFolder + "\\layoutTravelDataView.xml";
            //if (File.Exists(layoutVolFeaturesView))
            //{
            //    rgvVolFeatures.LoadLayout(layoutVolFeaturesView);
            //}
            //============Layout VolFeatures============
            //====Prikaz u grid za tj. punjenje grida podacima iz baze =======
            ClientPersonBUS cpbus = new ClientPersonBUS();
            List<ClientPersonModel> cpmodel = new List<ClientPersonModel>();
            cpmodel = cpbus.GetAllClientsFromPerson(Person.idContPers);
            if (cpmodel != null)
                clientAndPersonList = new BindingList<ClientPersonModel>(cpmodel);

            radGridViewContactPerson.DataSource = clientAndPersonList;


            //====Prikaz u grid za tj. punjenje grida podacima iz baze =======


            notesStripMenu.Items.Add("Save Layout", null, NoteMenuSaveClick);
            rgvNote.ContextMenuStrip = null;

            documentsStripMenu.Items.Add("Save Layout", null, DocumentsMenuSaveClick);
            rgvDocuments.ContextMenuStrip = null;

            meetingsStripMenu.Items.Add("Save Layout", null, MeetingsMenuSaveClick);

            //CONTACTS
            contactsStripMenu.Items.Add("Save Layout", null, ContactsMenuSaveClick);
            rgvContacts.ContextMenuStrip = null;

            //TASKS   ovo je sve prebaceno na dugmice
            tasksStripMenu.Items.Add("Save Layout", null, TasksMenuSaveClick);
            rgvToDo.ContextMenuStrip = null;

           // cpa = new ContactPersonAddress(Person.idContPers);
            cpa = new AdrOne(Person.idContPers);

            if (chkisPaperByMail.Checked == true)
                cpa.showPostAddress = true;
            else
                cpa.showPostAddress = false;


            TypesAddressBUS typeAddBus = new TypesAddressBUS();
            TypesAddresslModel typeAddModel = typeAddBus.GetTypeAddressById(3, Login._user.lngUser);
            // prosledjuje se u fillpersondata()
            showEmergencyAddressForFillPErsonData = true;
            if (typeAddModel != null)
            {
                if (typeAddModel.showInControl == true)
                {
                    cpa.showEmergencyAddress = true;
                    showEmergencyAddressForFillPErsonData = true;
                }
                else
                {
                    cpa.showEmergencyAddress = false;
                    showEmergencyAddressForFillPErsonData = false;
                }
            }
            cpa.Dock = DockStyle.Fill;
            //cpa.Dock = System.Windows.Forms.DockStyle.Fill;
            panelAddress.Controls.Add(cpa);
            // == account gridovi ====
            pg = new AccountData(Person.idContPers);
            pg.Dock = System.Windows.Forms.DockStyle.Fill;
            rpAccount.Controls.Add(pg);
            //=================

            setTranslation();
            //set medical and voluntary helper tabs
            tabMedical.Item.Visibility = ElementVisibility.Collapsed;
            tabVoluntary.Item.Visibility = ElementVisibility.Collapsed;
            tabCperson.Item.Visibility = ElementVisibility.Collapsed;


            //-------------------- ubaceno  Saki da proveri da li ima Notu status 2 i ako ima da ofarba tab u crveno 
            if (Login._user.isDontSeeMedVol == false)
            {
                NotesBUS nbus1 = new NotesBUS();
                List<NotesModel> specialNotes = new List<NotesModel>();
                specialNotes = nbus1.GetPersonStatus2(Person.idContPers);
                if (specialNotes != null)
                {
                    if (specialNotes.Count > 0)
                    {
                        // tabMemo.BackColor = RadPageViewLabelElement.BackColorProperty.Equals(Text);
                        this.tabMemo.Item.NumberOfColors = 1;
                        this.tabMemo.Item.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        // this.tabMemo.Item.ForeColor = this.tabAccount.BackColor;
                        this.tabMemo.Item.ForeColor = System.Drawing.Color.Black;
                    }

                }
            }
            else
            {
                tabMemo.Item.Visibility = ElementVisibility.Collapsed;
            }
            //--------------------------------------------------------------------------------------------


            //add title
            TitleBUS tb = new TitleBUS();
            persTitle = tb.GetAllTitle();

            ddlTitle.DataSource = persTitle;
            ddlTitle.DisplayMember = "nameTitle";
            ddlTitle.ValueMember = "idTitle";

            this.ddlTitle.SelectedIndex = -1;
            this.ddlTitle.Text = " ";

            //add gender
            GenderBUS gb = new GenderBUS();
            List<GenderModel> gm = new List<GenderModel>();
            gm = gb.GetAllGenders(Login._user.lngUser);

            ddlGender.DataSource = gm;
            ddlGender.DisplayMember = "nameGender";
            ddlGender.ValueMember = "idGender";

            CountryBUS gb6 = new CountryBUS();
            List<CountryModel> gm6 = new List<CountryModel>();
            gm6 = gb6.GetCountriesWithCountryModel();

            ddlNacionality.DataSource = gm6;
            ddlNacionality.DisplayMember = "nacionality";
            ddlNacionality.ValueMember = "idCountry";

            // add living place
            PersonLivingBUS gb7 = new PersonLivingBUS();
            List<PersonLivingModel> gm7 = new List<PersonLivingModel>();
            gm7 = gb7.GetAllLiving();

            ddlLives.DataSource = gm7;
            ddlLives.DisplayMember = "nameLiving";
            ddlLives.ValueMember = "idLiving";

            // add function as contact person

            ContactPersonFunctionBUS gb8 = new ContactPersonFunctionBUS();
            List<ContactPersonFunctionModel> gm8 = new List<ContactPersonFunctionModel>();
            gm8 = gb8.GetAllContactPersonFunction(Login._user.lngUser);
            ddlCpFunction.DataSource = gm8;
            ddlCpFunction.DisplayMember = "nameFunction";
            ddlCpFunction.ValueMember = "idFunction";

            //Combo Target group
            ArrangementTargetGroupBUS gb9 = new ArrangementTargetGroupBUS();
            List<TargetGroupModel> gm9 = new List<TargetGroupModel>();
            gm9 = gb9.GetAllTargetGroup();
            ddlTargetGroup.DataSource = gm9;
            ddlTargetGroup.DisplayMember = "shortcutTargeGroup";
            ddlTargetGroup.ValueMember = "idTargetGroup";

            // Reason IN/OUT
            PersonReasonInBUS gb10 = new PersonReasonInBUS();
            gm10 = new List<PersonReasonInModel>();
            gm10 = gb10.GetAllReasonIn();
            //ddlReasonIn.DataSource = gm10;
            ddlReasonIn.DisplayMember = "nameReasonIn";
            ddlReasonIn.ValueMember = "idReasonIn";

            PersonReasonOutBUS gb11 = new PersonReasonOutBUS();
            gm11 = new List<PersonReasonOutModel>();
            gm11 = gb11.GetAllReasonOut();
            //ddlReasonOut.DataSource = gm11;
            ddlReasonOut.DisplayMember = "nameReasonOut";
            ddlReasonOut.ValueMember = "idReasonOut";

            //=== volontary reason

            VolontaryReasonInBUS gb12 = new VolontaryReasonInBUS();
            gm12 = new List<VoluntaryReasonInModel>();
            gm12 = gb12.GetAllReasonIn();
            //ddlReasonIn.DataSource = gm12;
            ddlReasonIn.DisplayMember = "nameReasonIn";
            ddlReasonIn.ValueMember = "idReasonIn";

            VolontaryReasonOutBUS gb13 = new VolontaryReasonOutBUS();
            gm13 = new List<VoluntaryReasonOutModel>();
            gm13 = gb13.GetAllReasonOut();
            //ddlReasonOut.DataSource = gm11;
            ddlReasonOut.DisplayMember = "nameReasonOut";
            ddlReasonOut.ValueMember = "idReasonOut";
            //========== cita accsettings za account tab
            AccSettingsBUS asbu = new AccSettingsBUS();
            asmo = new AccSettingsModel();
            string cyear = DateTime.Now.Year.ToString();
            asmo = asbu.GetSettingsByID(cyear);
            //================================================== ovde ako nije account user disabe-uje polja na formi ==============
            if (Login._user.isAccountUser == false)
            {
                txtCreAcc.Enabled = false;
                txtDebAcc.Enabled = false;
                chkDebitor.Enabled = false;
                chkCreditor.Enabled = false;
                btnCreAcc.Enabled = false;
                btnDebAcc.Enabled = false;

                gridIbans.AllowAddNewRow = false;
                gridIbans.AllowEditRow = false;
                gridIbans.AllowDeleteRow = false;

                txtPayCon.Enabled = false;
                btnPayCon.Enabled = false;

            }
            //=======================================================================================================================
            int Y = 0;


            RadCheckBox rchk = new RadCheckBox();

            for (int i = 1; i < Login._personFilters.Count; i++)
            {
                rchk = new RadCheckBox();
                rchk.Font = new Font("Verdana", 9);
                rchk.ThemeName = pagePerson.ThemeName;
                rchk.Name = "chkFilter" + Login._personFilters[i].idFilter.ToString();
                rchk.Text = Login._personFilters[i].nameFilter;
                rchk.Location = new Point(0, Y);
                rchk.AutoSize = true;
                rchk.CheckStateChanged += rchk_CheckStateChanged;
                Y = Y + 3 + rchk.Height;
                panelFilters.Controls.Add(rchk);
            }


            Y = 0;
            for (int i = 0; i < Login._personLabels.Count; i++)
            {
                rchk = new RadCheckBox();
                rchk.Font = new Font("Verdana", 9);
                rchk.ThemeName = pagePerson.ThemeName;
                rchk.Name = "chkLabel" + Login._personLabels[i].idLabel.ToString();
                rchk.CheckStateChanged += rchk_LabelCheckStateChanged;
                rchk.Text = Login._personLabels[i].nameLabel;
                rchk.Location = new Point(0, Y);
                rchk.AutoSize = true;
                Y = Y + 3 + rchk.Height;
                panelLabels.Controls.Add(rchk);
            }


            if (Person.livesIn != 0 && Person.livesIn != null)
                ddlLives.SelectedItem = ddlLives.Items[gm7.FindIndex(item => item.idLiving == Person.livesIn)];
            if (ddlLives.SelectedIndex == 1)
            {

                txtClientR.Visible = true;
                btnClientR.Visible = true;
            }
            if (Person.idClient != 0 && Person.idClient != null)
            {
                ClientBUS ncb = new ClientBUS();
                ClientModel ncm = new ClientModel();
                ncm = ncb.GetClient(Person.idClient);
                if (ncm != null)
                {
                    txtClientR.Text = ncm.nameClient;
                    xClientName = ncm.nameClient;
                    xidClient = ncm.idClient;
                }
            }
            persEmail = new List<PersonEmailModel>();
            persEmailFirst = new List<PersonEmailModel>();
            persTel = new List<PersonTelModel>();
            persTelFirst = new List<PersonTelModel>();

            fillPersonData(showEmergencyAddressForFillPErsonData);

            if (Person != null && Person.idContPers != 0)
            {

                if (Person.idTitle != 0)
                    ddlTitle.SelectedItem = ddlTitle.Items[persTitle.FindIndex(item => item.idTitle == Person.idTitle)];
                ddlGender.SelectedItem = ddlGender.Items[gm.FindIndex(item => item.idGender == Person.idGender)];
                if (persPassport != null)
                {
                    if (persPassport.idCountry != 0 && persPassport.idCountry != null)
                        ddlNacionality.SelectedItem = ddlNacionality.Items[gm6.FindIndex(item => item.idCountry == persPassport.idCountry)];
                }
                //=== puni koliko je ukupno puta putovao 
                PersonBUS nrtr = new PersonBUS();

                int nrTravel = nrtr.CountNrTraveling(Person.idContPers);
                txtWithUs.Text = nrTravel.ToString();
                //====================

                // REASON IN/OUT  prikazuje podatke u odnosu da li je traveler(0) ili volonter(1)
                if (Person.idReasonIn != null && Person.idReasonIn != 0)
                    if (indTravVol == 0)
                        ddlReasonIn.SelectedItem = ddlReasonIn.Items[gm10.FindIndex(item => item.idReasonIn == Person.idReasonIn)];
                    else
                        ddlReasonIn.SelectedItem = ddlReasonIn.Items[gm12.FindIndex(item => item.idReasonIn == Person.idReasonIn)];
                if (Person.idReasonOut != null && Person.idReasonOut != 0)
                    if (indTravVol == 0)
                        ddlReasonOut.SelectedItem = ddlReasonOut.Items[gm11.FindIndex(item => item.idReasonOut == Person.idReasonOut)];
                    else
                        ddlReasonOut.SelectedItem = ddlReasonOut.Items[gm13.FindIndex(item => item.idReasonOut == Person.idReasonOut)];
                txtProfession.Text = Person.volProfession;



            }
            else
            {
                rgvEmail.DataSource = persEmail;
                this.rgvEmail.Columns["idContPers"].IsVisible = false;
                rgvEmail.Show();
                rgvTel.DataSource = persTel;
                this.rgvTel.Columns["idContPers"].IsVisible = false;
                rgvTel.Show();
            }

            //load combo types for tel and emails

            TypesTelBUS ttb = new TypesTelBUS();
            List<TypesTelModel> ttm = ttb.GetAllTypeTel(Login._user.lngUser);
            GridViewComboBoxColumn ddl = new GridViewComboBoxColumn();
            ddl.DataSource = ttm;
            ddl.DisplayMember = "nameTelType";
            ddl.ValueMember = "idTelType";
            ddl.FieldName = "idTelType";
            ddl.Name = "Type";
            ddl.HeaderText = "Type";
            rgvTel.Columns.Add(ddl);
            rgvTel.Columns["idTel"].IsVisible = false;
            rgvTel.Columns["idTelType"].IsVisible = false;

            TypesEmailBUS teb = new TypesEmailBUS();
            List<TypesEmailModel> tem = teb.GetAllTypeEmail(Login._user.lngUser);
            GridViewComboBoxColumn ddlemail = new GridViewComboBoxColumn();
            ddlemail.DataSource = tem;
            ddlemail.DisplayMember = "nameEmailType";
            ddlemail.ValueMember = "idEmailType";
            ddlemail.FieldName = "idEmailType";
            ddlemail.Name = "Type";
            ddlemail.HeaderText = "Type";
            rgvEmail.Columns.Add(ddlemail);
            rgvEmail.Columns["idEmail"].IsVisible = false;
            rgvEmail.Columns["idEmailType"].IsVisible = false;

            this.rgvEmail.Columns["email"].Width = 300;
            this.rgvEmail.Columns["isCommunication"].Width = 100;
            this.rgvEmail.Columns["isProspect"].Width = 100;
            this.rgvEmail.Columns["isInvoicing"].Width = 100;
            this.rgvEmail.Columns["isNewsletters"].Width = 100;
            this.rgvEmail.Columns["lastQuestionForm"].Width = 100;
            this.rgvEmail.Columns["Type"].Width = 150;
            //rgvEmail.Columns["idEmailType"].IsVisible = false;

            //load notes grid
            //============Omogucava dodavanja IdCertificate u txtCertificatesOrDiploma i VolFeatures
            certificateModel = new CertificatesModel();
            trainingModel = new TrainingModel();
            volFeaturesModel = new VolFeaturesModel();

            //========Prikaz u grid Training i Certificate tj. punjenje grida iz baze===============

            VolFeaturesBUS volFeBUS = new VolFeaturesBUS();
            List<VolFeaturesModel> volFeModelList = new List<VolFeaturesModel>();
            volFeModelList = volFeBUS.GetAllFeaturesFromPersonGrid(Person.idContPers);

            if (volFeModelList != null)
            {
                for (int i = 0; i < volFeModelList.Count; i++)
                {
                    if (volFeModelList[i].expireDate.ToString() == "1-1-1900 00:00:00")
                        volFeModelList[i].expireDate = null;
                    if (volFeModelList[i].archiveDate.ToString() == "1-1-1900 00:00:00")
                        volFeModelList[i].archiveDate = null;
                    if (volFeModelList[i].scheduleDate.ToString() == "1-1-1900 00:00:00")
                        volFeModelList[i].scheduleDate = null;

                }
                volFeaturesList = new BindingList<VolFeaturesModel>(volFeModelList);
            }
            rgvVolFeatures.DataSource = volFeaturesList;

            //=========Prikaz u grid Training i Certificate tj. punjenje grida iz baze===============


            //Prikaz u grid Za Travel Data tab

            ContactPersonTripDataBUS conptrBUS = new ContactPersonTripDataBUS();
            List<ContactPersonTripDataModel> codeTraModelList = new List<ContactPersonTripDataModel>();
            codeTraModelList = conptrBUS.GetTripByPerson(Person.idContPers);

            if (codeTraModelList != null)
            {
                for (int i = 0; i < codeTraModelList.Count; i++)
                {
                    if (codeTraModelList[i].dtFrom.ToString() == "1-1-1900 00:00:00")
                        codeTraModelList[i].dtFrom = null;
                    if (codeTraModelList[i].dtTo.ToString() == "1-1-1900 00:00:00")
                        codeTraModelList[i].dtTo = null;
                }

                conPersTripDataList = new BindingList<ContactPersonTripDataModel>(codeTraModelList);
                rgvTripData.DataSource = conPersTripDataList;
            }

            //Prikaz u grid Za Travel Data tab

            //==========Omogucava dodavanja IdCertificate u txtCertificatesOrDiploma i VolFeatures
            //load notes grid
            personNotes = new NotesBUS().GetPersonNotes(Person.idContPers);
            rgvNote.DataSource = personNotes;
            if (rgvNote.DataSource == null)
            {
                List<NotesModel> notemdl = new List<NotesModel>();
                rgvNote.DataSource = notemdl;
            }

            rgvNote.Show();

            pageLoaded = true;
           
        }

        private void btnSaveUpdate_Click(object sender, EventArgs e)
        {
           

            bool validate = ValidatePerson();
            if (validate == true)
            {
                savePerson();
                updateData();
            }
        }

        private void updateData()
        {
            PersonBUS pb = new PersonBUS();
            PersonAddressBUS pab = new PersonAddressBUS();
            PersonPassportBUS ppb = new PersonPassportBUS();
            PersonEmailBUS peb = new PersonEmailBUS();
            MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
            VolontaryFunctionBUS vfb = new VolontaryFunctionBUS();
            VolontaryTripBUS vtb = new VolontaryTripBUS();
            PersonTelBUS ptb = new PersonTelBUS();
            Boolean isSuccessfully = false;

            if (pb.Update(Person, this.Name, Login._user.idUser) == true)
            {
                isSuccessfully = true;
                saveAddress(showEmergencyAddressForFillPErsonData);
                for (int n = 0; n < PersonAddress.Count; n++)
                {
                    if (PersonAddress[n].street != "" || PersonAddress[n].housenr.Trim() != "" || PersonAddress[n].postalCode.Trim() != ""
                        || PersonAddress[n].city.Trim() != "")
                    {
                        if (pab.Update(PersonAddress[n], this.Name, Login._user.idUser) == true)
                        {
                            isSuccessfully = true;
                        }
                        else
                        {
                            RadMessageBox.Show("You have not successfully save data for email " + (n + 1).ToString());
                        }
                    }
                    else
                    {
                        pab.Delete(Person.idContPers, (int)PersonAddress[n].idAddressType, this.Name, Login._user.idUser);
                    }
                }
                savePassport();
                if (ppb.Update(persPassport, this.Name, Login._user.idUser) == true)
                {
                    isSuccessfully = true;

                }
                else
                {
                    RadMessageBox.Show("You have not successfully save passport data.");
                }
                if (persEmail != null)
                {
                    saveEmail();
                    for (int i = 0; i < persEmail.Count; i++)
                    {
                        if (peb.Update(persEmail[i], this.Name, Login._user.idUser) == true)
                        {
                            isSuccessfully = true;
                        }
                        else
                        {
                            RadMessageBox.Show("You have not successfully save data for email " + (i + 1).ToString());
                        }
                    }
                }
                if (persTel != null)
                {
                    saveTel();
                    for (int j = 0; j < persTel.Count; j++)
                    {
                        if (ptb.Update(persTel[j], this.Name, Login._user.idUser) == true)
                        {
                            isSuccessfully = true;
                        }
                        else
                        {
                            RadMessageBox.Show("You have not successfully save data for tel " + (j + 1).ToString());
                        }
                    }
                }
                saveFilters();
                if (PersonFilter != null)
                {
                    if (pb.DeleteFilter(Person.idContPers, this.Name, Login._user.idUser) == true)
                    {

                        for (int m = 0; m < PersonFilter.Count; m++)
                        {
                            if (pb.SaveFilter(PersonFilter[m], this.Name, Login._user.idUser) == true)
                            {
                                isSuccessfully = true;
                            }
                            else
                            {
                                RadMessageBox.Show("You have not successfully save data for filter " + (m + 1).ToString());
                            }
                        }
                    }
                    else
                    {
                        RadMessageBox.Show("You have not successfully save data for filters");
                    }
                }
                saveLabels();
                if (PersonLabel != null)
                {

                    if (pb.DeleteLabel(Person.idContPers, this.Name, Login._user.idUser) == true)
                    {
                        for (int d = 0; d < PersonLabel.Count; d++)
                        {
                            if (pb.SaveLabel(PersonLabel[d], this.Name, Login._user.idUser) == true)
                            {
                                isSuccessfully = true;
                            }
                            else
                            {
                                RadMessageBox.Show("You have not successfully save data for label " + (d + 1).ToString());
                            }
                        }
                    }
                    else
                    {
                        RadMessageBox.Show("You have not successfully save data for labels");
                    }
                }
                saveMedical();
                if (isMedChanged == true)
                {
                    if (mvb.DeleteMedical(Person.idContPers, this.Name, Login._user.idUser) == true)
                    {
                        for (int j = 0; j < personMedical.Count; j++)
                        {
                            if (mvb.SaveMedical(personMedical[j], this.Name, Login._user.idUser) == false)
                            {
                                isSuccessfully = false;
                                RadMessageBox.Show("You have not succesufully inserted medical data. Please check!");
                            }

                        }
                    }
                    else
                    {
                        isSuccessfully = false;
                        RadMessageBox.Show("You have not succesufully inserted medical data. Please check!");
                    }
                }
                if (isVolChanged == true)
                {
                    if (tabSkils.Controls.Count == 0)
                    {
                        loadOnVoluntaryForm();
                    }
                    if (tabFunction.Controls.Count == 0)
                    {
                        loadOnVoluntarySectab();
                    }
                    if (tabTrips.Controls.Count == 0)
                    {
                        loadOnVoluntaryThird();
                    }


                    saveVoluntary();
                    if (isVolChanged == true)
                    {
                        if (mvb.Delete(Person.idContPers, this.Name, Login._user.idUser) == true)
                        {
                            for (int j = 0; j < personVoluntary.Count; j++)
                            {
                                if (mvb.Save(personVoluntary[j], this.Name, Login._user.idUser) == false)
                                {
                                    isSuccessfully = false;
                                    RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                                    break;
                                }

                            }
                        }
                        else
                        {
                            isSuccessfully = false;
                            RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                        }
                    }
                    saveVoluntaryFunction();
                    if (isVolChanged == true)
                    {
                        if (vfb.Delete(Person.idContPers, this.Name, Login._user.idUser) == true)
                        {
                            for (int j = 0; j < personVoluntary1.Count; j++)
                            {
                                if (vfb.Save(personVoluntary1[j], this.Name, Login._user.idUser) == false)
                                {
                                    isSuccessfully = false;
                                    RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                                    break;
                                }

                            }
                        }
                        else
                        {
                            isSuccessfully = false;
                            RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                        }
                    }
                    saveVoluntaryTrip();
                    if (isVolChanged == true)
                    {
                        if (vtb.Delete(Person.idContPers, this.Name, Login._user.idUser) == true)
                        {
                            for (int j = 0; j < personVoluntary2.Count; j++)
                            {
                                if (vtb.Save(personVoluntary2[j], this.Name, Login._user.idUser) == false)
                                {
                                    isSuccessfully = false;
                                    RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                                    break;
                                }

                            }
                        }
                        else
                        {
                            isSuccessfully = false;
                            RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                        }
                    }
                }
            }
            else
            {
                isSuccessfully = false;
            }

            // ---- save acc data ----------------------------------------------
            if (debCre != null)
            {
                isOkAcc = false;
                saveAcc();
                if (isOkAcc == true)
                {
                    AccDebCreBUS dbbus = new AccDebCreBUS();
                    AccDebCreModel aa = new AccDebCreModel();
                    isSaved = false;
                    // provera da  postoji slog
                    aa = dbbus.GetPersonDebCre(Person.idContPers);
                    if (aa != null)
                    {
                        debCre.idContPerson = Person.idContPers;
                        debCre.idAccDebCre = aa.idAccDebCre;
                        isSaved = dbbus.Update(debCre, this.Name, Login._user.idUser);
                        if (isSaved == false)
                        {
                            RadMessageBox.Show("Error updating Account data ");
                        }
                    }
                    else
                    {
                        debCre.idContPerson = Person.idContPers;
                        isSaved = dbbus.Save(debCre, this.Name, Login._user.idUser);
                        if (isSaved == false)
                        {
                            RadMessageBox.Show("Error saving Account data ");
                        }
                    }
                }
            }

            //------------------------------------------------------------------

            if (isSuccessfully == true)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("You have successfully save data") != null)
                        RadMessageBox.Show(resxSet.GetString("You have successfully save data"));
                    else
                        RadMessageBox.Show("You have successfully save data");
                }
                // RadMessageBox.Show("You have successfully save data");

                UpdateOriginalValuesAfterSave();
                isVolChanged = false;
            }
            else
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("You have not successfully save data") != null)
                        RadMessageBox.Show(resxSet.GetString("You have not successfully save data"));
                    else
                        RadMessageBox.Show("You have not successfully save data");
                }
                //  RadMessageBox.Show("You have not successfully save data");
            }
        }

        private void btnSaveInsert_Click(object sender, EventArgs e)
        {

            //  debCre = new AccDebCreModel();

            bool validate = ValidatePerson();
            if (validate == true)
            {
                savePerson();
                insertData();
                for (int i = 0; i < pagePerson.Pages.Count; i++)
                {
                    if (pagePerson.Pages[i].Name != "tabRelation")
                    {
                        pagePerson.Pages[i].Enabled = true;
                    }
                }
            }
        }

        private void insertData()
        {
            if (Person.idContPers != null && Person.idContPers > 0)
            {
                updateData();

            }
            else
            {
                debCre = new AccDebCreModel();
                Person.idUserCreated = Login._user.idUser;
                Person.dtCreated = DateTime.Now;
                PersonBUS pb = new PersonBUS();
                PersonAddressBUS pab = new PersonAddressBUS();
                PersonPassportBUS ppb = new PersonPassportBUS();
                PersonEmailBUS peb = new PersonEmailBUS();
                MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
                VolontaryFunctionBUS vfb = new VolontaryFunctionBUS();
                VolontaryTripBUS vtb = new VolontaryTripBUS();
                PersonTelBUS ptb = new PersonTelBUS();
                Boolean isSuccessfully = false;


                if (pb.Save(Person, this.Name, Login._user.idUser) == true)
                {
                    isSuccessfully = true;
                    Person.idContPers = pb.GeLastPersonID();
                    //================  account number ===========
                    // debCre = new AccDebCreModel();
                    AccDebCreBUS dbbus1 = new AccDebCreBUS();
                    //=============================== ubacuje za novog DEBITOR / CREDITOR BROJ
                    txtPersAccNo.Text = Person.idContPers.ToString().PadLeft(6, '0');
                    debCre.accNumber = txtPersAccNo.Text;
                    debCre.idContPerson = Person.idContPers;
                    isSaved = dbbus1.Save(debCre, this.Name, Login._user.idUser);
                    //============================================
                    saveAddress(showEmergencyAddressForFillPErsonData);
                    for (int n = 0; n < PersonAddress.Count; n++)
                    {
                        if (PersonAddress[n].street != "" || PersonAddress[n].housenr.Trim() != "" || PersonAddress[n].postalCode.Trim() != ""
                        || PersonAddress[n].city.Trim() != "")
                        {
                            if (pab.Save(PersonAddress[n], this.Name, Login._user.idUser) == true)
                            {
                                isSuccessfully = true;
                            }
                            else
                            {
                                RadMessageBox.Show("You have not successfully save data for email " + (n + 1).ToString());
                            }
                        }
                    }
                    savePassport();
                    if (ppb.Save(persPassport, this.Name, Login._user.idUser) == true)
                    {
                        isSuccessfully = true;

                    }
                    else
                    {
                        RadMessageBox.Show("You have not successfully save email data");
                    }
                    if (persEmail != null)
                    {
                        saveEmail();
                        for (int i = 0; i < persEmail.Count; i++)
                        {
                            if (peb.Save(persEmail[i], this.Name, Login._user.idUser) == true)
                            {
                                isSuccessfully = true;
                            }
                            else
                            {
                                RadMessageBox.Show("You have not successfully save data for email " + (i + 1).ToString());
                            }
                        }
                    }
                    if (persTel != null)
                    {
                        saveTel();
                        for (int j = 0; j < persTel.Count; j++)
                        {
                            if (ptb.Save(persTel[j], this.Name, Login._user.idUser) == true)
                            {
                                isSuccessfully = true;
                            }
                            else
                            {
                                RadMessageBox.Show("You have not successfully save data for tel " + (j + 1).ToString());
                            }
                        }
                    }
                    saveFilters();
                    if (PersonFilter != null)
                    {

                        if (pb.DeleteFilter(Person.idContPers, this.Name, Login._user.idUser) == true)
                        {
                            for (int m = 0; m < PersonFilter.Count; m++)
                            {
                                if (pb.SaveFilter(PersonFilter[m], this.Name, Login._user.idUser) == true)
                                {
                                    isSuccessfully = true;
                                }
                                else
                                {
                                    RadMessageBox.Show("You have not successfully save data for filter " + (m + 1).ToString());
                                }
                            }
                        }
                        else
                        {
                            RadMessageBox.Show("You have not successfully save data for filters");
                        }
                    }
                    saveLabels();
                    if (PersonLabel != null)
                    {

                        if (pb.DeleteLabel(Person.idContPers, this.Name, Login._user.idUser) == true)
                        {
                            for (int d = 0; d < PersonLabel.Count; d++)
                            {
                                if (pb.SaveLabel(PersonLabel[d], this.Name, Login._user.idUser) == true)
                                {
                                    isSuccessfully = true;
                                }
                                else
                                {
                                    RadMessageBox.Show("You have not successfully save data for label " + (d + 1).ToString());
                                }
                            }
                        }
                        else
                        {
                            RadMessageBox.Show("You have not successfully save data for labels");
                        }
                    }
                    saveMedical();
                    if (isMedChanged == true)
                    {
                        if (mvb.DeleteMedical(Person.idContPers, this.Name, Login._user.idUser) == true)
                        {
                            for (int j = 0; j < personMedical.Count; j++)
                            {
                                if (mvb.SaveMedical(personMedical[j], this.Name, Login._user.idUser) == false)
                                {
                                    isSuccessfully = false;
                                    RadMessageBox.Show("You have not succesufully inserted medical data. Please check!");
                                }

                            }
                        }
                        else
                        {
                            isSuccessfully = false;
                            RadMessageBox.Show("You have not succesufully inserted medical data. Please check!");
                        }
                    }
                    if (isVolChanged == true)
                    {
                        if (tabSkils.Controls.Count == 0)
                        {
                            loadOnVoluntaryForm();
                        }
                        if (tabFunction.Controls.Count == 0)
                        {
                            loadOnVoluntarySectab();
                        }
                        if (tabTrips.Controls.Count == 0)
                        {
                            loadOnVoluntaryThird();
                        }


                        saveVoluntary();
                        if (isVolChanged == true)
                        {
                            if (mvb.Delete(Person.idContPers, this.Name, Login._user.idUser) == true)
                            {
                                for (int j = 0; j < personVoluntary.Count; j++)
                                {
                                    if (mvb.Save(personVoluntary[j], this.Name, Login._user.idUser) == false)
                                    {
                                        isSuccessfully = false;
                                        RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                                        break;
                                    }

                                }
                            }
                            else
                            {
                                isSuccessfully = false;
                                RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                            }
                        }
                        saveVoluntaryFunction();
                        if (isVolChanged == true)
                        {
                            if (vfb.Delete(Person.idContPers, this.Name, Login._user.idUser) == true)
                            {
                                for (int j = 0; j < personVoluntary1.Count; j++)
                                {
                                    if (vfb.Save(personVoluntary1[j], this.Name, Login._user.idUser) == false)
                                    {
                                        isSuccessfully = false;
                                        RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                                        break;
                                    }

                                }
                            }
                            else
                            {
                                isSuccessfully = false;
                                RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                            }
                        }
                        saveVoluntaryTrip();
                        if (isVolChanged == true)
                        {
                            if (vtb.Delete(Person.idContPers, this.Name, Login._user.idUser) == true)
                            {
                                for (int j = 0; j < personVoluntary2.Count; j++)
                                {
                                    if (vtb.Save(personVoluntary2[j], this.Name, Login._user.idUser) == false)
                                    {
                                        isSuccessfully = false;
                                        RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                                        break;
                                    }

                                }
                            }
                            else
                            {
                                isSuccessfully = false;
                                RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                            }
                        }
                    }

                }
                else
                {
                    RadMessageBox.Show("You have not successfully save person");
                }

                if (isSuccessfully == true)
                {
                    RadMessageBox.Show("You have successfully save data");
                    UpdateOriginalValuesAfterSave();
                    isVolChanged = false;
                }

            }

        }

        private void rchk_LabelCheckStateChanged(object sender, EventArgs e)
        {
            if (pageLoaded == true)
            {
                if (tabSkils.Controls.Count == 0)
                {
                    loadOnVoluntaryForm();
                }
                if (tabFunction.Controls.Count == 0)
                {
                    loadOnVoluntarySectab();
                }
                if (tabTrips.Controls.Count == 0)
                {
                    loadOnVoluntaryThird();
                }
                isVolChanged = true;
            }
        }

        private void firstPartMedical()
        {
            tabMedical.Controls.Clear();

        }

        private void secondPartMedical()
        {
            List<string> idQueryType = new List<string>();

            foreach (Control c in panelLabels.Controls)
            {
                RadCheckBox rchk = (RadCheckBox)c;
                if (rchk.Checked == true)
                    idQueryType.Add(rchk.Name.Replace("chkLabel", ""));
            }

            RadToggleButton btnSort = (RadToggleButton)tabMedical.Controls.Find("btnSort", true)[0];
            RadToggleButton btnAll = (RadToggleButton)tabMedical.Controls.Find("btnAll", true)[0];

            Boolean isDefaultSort = false;

            if (btnSort.CheckState == CheckState.Checked)
                isDefaultSort = true;

            Boolean isAll = false;

            if (btnAll.CheckState == CheckState.Checked)
                isAll = true;


            MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
            personMedical = mvb.GetMedicalDetails(idQueryType, Person.idContPers, isDefaultSort, isAll);

            //Y
            int lastBottom = 60;
            //X
            //Neta for question with more then one row
            int lastBottomQuestion = 60;
            int left = 26;
            string oldQuest = "";
            string oldQuestGroup = "";  // Saki

            if (personMedical != null)
            {
                if (personMedical.Count > 0)
                {
                    //create dynamic controls
                    for (int i = 0; i < personMedical.Count; i++)
                    {

                        string questGroup = ""; // saki
                        string quest = "";
                        string questText = "";
                        string ans = "";
                        string ansText = "";

                        Boolean ischecked = false;
                        //checkbox width
                        int chkWidth = 150;
                        //radiobutton width
                        int rbWidth = 150;
                        //question label width 
                        int rlWidth = (int)(30 * pagePerson.Width / 100) - 20;
                        //answer width (for all controls)
                        int aWidth = (int)(70 * pagePerson.Width / 100) - 80;
                        //question label height 
                        //     int rlheight = 30;  sale i goga
                        int rlheight = 20;
                        //answer row height
                        //     int height = 30;  sale i goga
                        int height = 20;
                        //Neta for question with more then one row
                        int rlquestheight = 20;



                        //====== Saki
                        questGroup = personMedical[i].nameQuestGroup.TrimEnd();
                        //==========
                        quest = personMedical[i].idQuest.ToString().TrimEnd();
                        if (personMedical[i].questSort != null && personMedical[i].questSort != 0)
                            questSort = Convert.ToDecimal(personMedical[i].questSort.ToString());
                        if (personMedical[i].idAns.ToString() != "")
                        {
                            ans = personMedical[i].idAns.ToString().TrimEnd();
                        }
                        questText = personMedical[i].txtQuest.ToString().TrimEnd();

                        ansText = personMedical[i].txtAns.ToString().TrimEnd();
                        if (personMedical[i].idcpr.ToString() != "")
                        {
                            ischecked = true;
                        }

                        //question label
                        RadLabel rl = new RadLabel();
                        RadLabel rlTitle = new RadLabel();  // Saki

                        //if (quest != oldQuest)      ORIGINAL
                        //{
                        //    rl.Text = questText;
                        //    rl.Location = new Point(left, lastBottom);
                        //    rl.Font = new Font("Verdana", 9);
                        //    //set multi lines
                        //    rl.MaximumSize = new Size(rlWidth, height * 3);
                        //    rl.AutoSize = true;
                        //    //
                        //    rl.Width = rlWidth;
                        //    tabMedical.Controls.Add(rl);
                        //    oldQuest = quest;

                        //    //set rows height depends on number of lines in label question
                        //    rlheight = height * numberOfLines(rl);
                        //}
                        //Neta for question with more then one row
                        if (quest != oldQuest)
                        {
                            if (lastBottomQuestion > lastBottom)
                                lastBottom = lastBottomQuestion;
                        }


                        // ============  ova dva ifa su umesto ORIGINALA ============================
                        if (questGroup.ToUpper() != oldQuestGroup.ToUpper())
                        {
                            rlTitle.Text = questGroup.ToUpper();   //==== Saki
                            rlTitle.Location = new Point(left, lastBottom);
                            rlTitle.Font = new Font("Verdana", 9);
                            rlTitle.ForeColor = Color.DarkOrange;
                            //set multi lines
                            rlTitle.MaximumSize = new Size(rlWidth, height * 3);
                            rlTitle.AutoSize = true;
                            //
                            rlTitle.Width = rlWidth;
                            tabMedical.Controls.Add(rlTitle);
                            //oldQuest = quest;

                            //set rows height depends on number of lines in label question
                            rlheight = height * numberOfLines(rlTitle);
                            lastBottom = lastBottom + rlheight + 5;
                            oldQuestGroup = questGroup;
                        }


                        if (quest != oldQuest)
                        {
                            string s = questSort.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                            string[] parts = s.Split('.');
                            int i1 = int.Parse(parts[0]);
                            int i2 = int.Parse(parts[1]);

                            rl.Text = i1.ToString() + "-" + i2.ToString() + "  " + questText;

                            // rl.Text = questSort.ToString("#-##") +" "+questText;   //==== Saki
                            rl.Location = new Point(left, lastBottom);
                            rl.Font = new Font("Verdana", 9);
                            //set multi lines
                            rl.MaximumSize = new Size(rlWidth, height * 3);
                            rl.AutoSize = true;
                            //
                            rl.Width = rlWidth;
                            tabMedical.Controls.Add(rl);
                            oldQuest = quest;

                            //Neta for question with more then one row
                            rlquestheight = height * numberOfLines(rl);
                            lastBottomQuestion = lastBottom + rlquestheight + 5;

                            ////set rows height depends on number of lines in label question
                            //rlheight = height * numberOfLines(rl);
                        }

                        //=========================================================================


                        //ANSWER
                        //checkbox type
                        if (personMedical[i].idAnsType.ToString() == "1")
                        {
                            RadCheckBox chk = new RadCheckBox();
                            chk.Name = "Q" + quest + "A" + ans;
                            chk.Text = ansText;
                            chk.Width = aWidth;
                            chk.Height = height;
                            chk.Font = new Font("Verdana", 9);
                            chk.Location = new Point(left + rlWidth + 20, lastBottom);
                            chk.CheckStateChanged += radCheckBox_CheckStateChanged_type1;
                            if (ischecked == true)
                            {
                                chk.CheckState = CheckState.Checked;
                            }
                            tabMedical.Controls.Add(chk);

                        }
                        //radio button type
                        else if (personMedical[i].idAnsType.ToString() == "2")
                        {
                            RadRadioButton rb = new RadRadioButton();
                            rb.Name = "Q" + quest + "A" + ans;
                            rb.Text = ansText;
                            rb.MaximumSize = new Size(400, 0);
                            rb.MinimumSize = new Size(40, 0);
                            rb.AutoSize = true;
                            rb.Width = rbWidth;
                            rb.Height = height;
                            rb.Font = new Font("Verdana", 9);

                            if (ischecked == true)
                            {
                                rb.CheckState = CheckState.Checked;
                            }

                            RadGroupBox rgb = new RadGroupBox();
                            rgb.MaximumSize = new Size(400, 0);
                            rgb.MinimumSize = new Size(40, 0);
                            rgb.AutoSize = true;
                            if (tabMedical.Controls.Find("Group" + quest, true).Length > 0)
                            {
                                rgb = (RadGroupBox)tabMedical.Controls.Find("Group" + quest, true)[0];
                                rgb.Height = lastBottom - rgb.Location.Y + height;
                                if (rb.Width > rgb.Width)
                                    rgb.Width = rb.Width;
                            }
                            else
                            {
                                rgb.Name = "Group" + quest;
                                ((Telerik.WinControls.Primitives.FillPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).InnerColor = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).ForeColor = System.Drawing.Color.Transparent;
                                tabMedical.Controls.Add(rgb);
                                rgb.Width = rbWidth;
                                rgb.Height = height;
                                rgb.Font = new Font("Verdana", 9);
                                rgb.Location = new Point(left + rlWidth + 20, lastBottom);
                                rgb.Width = rb.Width;
                            }

                            rb.Location = new Point(0, lastBottom - rgb.Location.Y);
                            rb.Click += radRadioButtonMedical_Click;
                            rgb.Controls.Add(rb);
                        }
                        //checkbox + textbox type
                        else if (personMedical[i].idAnsType.ToString() == "3")
                        {

                            RadCheckBox chk = new RadCheckBox();
                            chk.Name = "Q" + quest + "A" + ans;
                            chk.Text = ansText;
                            chk.MaximumSize = new Size(400, 0);
                            chk.MinimumSize = new Size(40, 0);
                            chk.AutoSize = true;
                            chk.Width = chkWidth;
                            chk.Height = height;
                            chk.Font = new Font("Verdana", 9);
                            chk.Location = new Point(left + rlWidth + 20, lastBottom);
                            chk.CheckStateChanged += radCheckBox_CheckStateChanged_type3;
                            tabMedical.Controls.Add(chk);
                            RadTextBox rtb = new RadTextBox();
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Width = aWidth - chk.Width - 20;
                            rtb.Height = height;
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Location = new Point(left + rlWidth + 20 + chk.Width + 20, lastBottom);
                            rtb.TextChanged += radTextBoxMedical_TextChanged;
                            if (ischecked == true)
                            {
                                chk.CheckState = CheckState.Checked;
                                if (personMedical[i].txt != null)
                                {
                                    rtb.Text = personMedical[i].txt.ToString();
                                }

                            }
                            tabMedical.Controls.Add(rtb);
                        }
                        //textbox type
                        else if (personMedical[i].idAnsType.ToString() == "4")
                        {
                            RadTextBox rtb = new RadTextBox();
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Width = aWidth;
                            rtb.Height = height;
                            rtb.Location = new Point(left + rlWidth + 20, lastBottom);
                            rtb.TextChanged += radTextBoxMedical_TextChanged;
                            if (ischecked == true)
                            {
                                if (personMedical[i].txt != null)
                                {
                                    rtb.Text = personMedical[i].txt.ToString();
                                }
                            }
                            tabMedical.Controls.Add(rtb);
                        }
                        //radio button + textbox type
                        else if (personMedical[i].idAnsType.ToString() == "5")
                        {
                            RadRadioButton rb = new RadRadioButton();
                            rb.CheckStateChanged += radRadioButton_CheckStateChanged;
                            rb.Name = "Q" + quest + "A" + ans;
                            rb.Text = ansText;
                            rb.MaximumSize = new Size(400, 0);
                            rb.MinimumSize = new Size(40, 0);
                            rb.AutoSize = true;
                            rb.Width = rbWidth;
                            rb.Height = height;
                            rb.Font = new Font("Verdana", 9);

                            if (ischecked == true)
                            {
                                rb.CheckState = CheckState.Checked;
                            }

                            RadGroupBox rgb = new RadGroupBox();
                            rgb.MaximumSize = new Size(400, 0);
                            rgb.MinimumSize = new Size(40, 0);
                            rgb.AutoSize = true;
                            if (tabMedical.Controls.Find("Group" + quest, true).Length > 0)
                            {
                                rgb = (RadGroupBox)tabMedical.Controls.Find("Group" + quest, true)[0];
                                rgb.Height = lastBottom - rgb.Location.Y + height;
                            }
                            else
                            {
                                rgb.Name = "Group" + quest;
                                ((Telerik.WinControls.Primitives.FillPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).InnerColor = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).ForeColor = System.Drawing.Color.Transparent;
                                tabMedical.Controls.Add(rgb);
                                rgb.Width = rbWidth;
                                rgb.Height = height;
                                rgb.Font = new Font("Verdana", 9);
                                rgb.Location = new Point(left + rlWidth + 20, lastBottom);
                            }

                            rb.Location = new Point(0, lastBottom - rgb.Location.Y);
                            rb.Click += radRadioButtonMedical_Click;
                            //rgb.Width = 50;
                            rgb.Controls.Add(rb);

                            if (tabMedical.Controls.Find("Group" + quest, true).Length > 0)
                            {
                                if (rb.Width > rgb.Width)
                                    rgb.Width = rb.Width;
                            }
                            else
                            {
                                rgb.Width = rb.Width;
                            }

                            RadTextBox rtb = new RadTextBox();
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Width = aWidth - rb.Width - 20;
                            rtb.Height = height;
                            rtb.Location = new Point(left + rlWidth + 20 + rb.Width + 20, lastBottom);
                            rtb.TextChanged += radTextBoxMedical_TextChanged;
                            if (ischecked == true)
                            {
                                rb.CheckState = CheckState.Checked;
                                if (personMedical[i].txt != null)
                                {
                                    rtb.Text = personMedical[i].txt.ToString();
                                }

                            }
                            tabMedical.Controls.Add(rtb);
                        }
                        //set X for next row
                        lastBottom = lastBottom + rlheight + 5;
                    }
                }
            }

            isMedChanged = false;
        }

        private void loadOnMedicalForm()
        {
            Cursor.Current = Cursors.WaitCursor;
            firstPartMedical();

            RadToggleButton btnAll = new RadToggleButton();
            btnAll.CheckState = CheckState.Checked;
            btnAll.ThemeName = pagePerson.ThemeName;
            btnAll.Name = "btnAll";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("Filled data") != null)
                    btnAll.Text = resxSet.GetString("Filled data");
                else
                    btnAll.Text = "Filled data";
            }
            btnAll.Location = new Point(26, 10);
            btnAll.Font = new Font("Verdana", 9);
            btnAll.CheckStateChanged += btnMedicalAll_Click;

            RadToggleButton btnSort = new RadToggleButton();
            btnSort.CheckState = CheckState.Checked;
            btnSort.ThemeName = pagePerson.ThemeName;
            btnSort.Width = 200;
            btnSort.Name = "btnSort";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("Alfabetical sort") != null)
                    btnSort.Text = resxSet.GetString("Alfabetical sort");
                else
                    btnSort.Text = "Alfabetical sort";
            }
            btnSort.Location = new Point(250, 10);
            btnSort.Font = new Font("Verdana", 9);
            btnSort.CheckStateChanged += btnMedicalSort_Click;

            tabMedical.Controls.Add(btnAll);
            tabMedical.Controls.Add(btnSort);

            secondPartMedical();

            Cursor.Current = Cursors.Default;
        }

        private void btnMedicalAll_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (Person.idContPers != null && Person.idContPers != 0)
            {
                MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
                saveMedical();
                if (isMedChanged == true)
                {
                    if (mvb.DeleteMedical(Person.idContPers, this.Name, Login._user.idUser) == true)
                    {
                        for (int j = 0; j < personMedical.Count; j++)
                        {
                            if (mvb.SaveMedical(personMedical[j], this.Name, Login._user.idUser) == false)
                            {
                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                {
                                    if (resxSet.GetString("You have not succesufully inserted medical data. Please check!") != null)
                                        RadMessageBox.Show(resxSet.GetString("You have not succesufully inserted medical data. Please check!"));
                                    else
                                        RadMessageBox.Show("You have not succesufully inserted medical data. Please check!");
                                }
                            }

                        }
                    }
                    else
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("You have not succesufully inserted medical data. Please check!") != null)
                                RadMessageBox.Show(resxSet.GetString("You have not succesufully inserted medical data. Please check!"));
                            else
                                RadMessageBox.Show("You have not succesufully inserted medical data. Please check!");
                        }
                    }
                }


                RadToggleButton btnAll = (RadToggleButton)tabMedical.Controls.Find("btnAll", true)[0];

                if (btnAll.CheckState == CheckState.Checked)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Filled data") != null)
                            btnAll.Text = resxSet.GetString("Filled data");
                        else
                            btnAll.Text = "Filled data";
                    }
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("All data") != null)
                            btnAll.Text = resxSet.GetString("All data");
                        else
                            btnAll.Text = "All data";
                    }

                }


                RadToggleButton btnSort = (RadToggleButton)tabMedical.Controls.Find("btnSort", true)[0];

                if (btnSort.CheckState == CheckState.Checked)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Alfabetical sort") != null)
                            btnSort.Text = resxSet.GetString("Alfabetical sort");
                        else
                            btnSort.Text = "Alfabetical sort";
                    }
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Default sort") != null)
                            btnSort.Text = resxSet.GetString("Default sort");
                        else
                            btnSort.Text = "Default sort";
                    }
                }

                firstPartMedical();
                tabMedical.Controls.Add(btnAll);
                tabMedical.Controls.Add(btnSort);
                secondPartMedical();

                Cursor.Current = Cursors.Default;
            }
        }

        private void btnMedicalSort_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (Person.idContPers != null && Person.idContPers != 0)
            {
                MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
                saveMedical();
                if (isMedChanged == true)
                {
                    if (mvb.DeleteMedical(Person.idContPers, this.Name, Login._user.idUser) == true)
                    {
                        for (int j = 0; j < personMedical.Count; j++)
                        {
                            if (mvb.SaveMedical(personMedical[j], this.Name, Login._user.idUser) == false)
                            {
                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                {
                                    if (resxSet.GetString("You have not succesufully inserted medical data. Please check!") != null)
                                        RadMessageBox.Show(resxSet.GetString("You have not succesufully inserted medical data. Please check!"));
                                    else
                                        RadMessageBox.Show("You have not succesufully inserted medical data. Please check!");
                                }
                            }
                        }
                    }
                    else
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("You have not succesufully inserted medical data. Please check!") != null)
                                RadMessageBox.Show(resxSet.GetString("You have not succesufully inserted medical data. Please check!"));
                            else
                                RadMessageBox.Show("You have not succesufully inserted medical data. Please check!");
                        }
                    }
                }
            }


            RadToggleButton btnAll = (RadToggleButton)tabMedical.Controls.Find("btnAll", true)[0];

            if (btnAll.CheckState == CheckState.Checked)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Filled data") != null)
                        btnAll.Text = resxSet.GetString("Filled data");
                    else
                        btnAll.Text = "Filled data";
                }
            }
            else
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("All data") != null)
                        btnAll.Text = resxSet.GetString("All data");
                    else
                        btnAll.Text = "All data";
                }

            }


            RadToggleButton btnSort = (RadToggleButton)tabMedical.Controls.Find("btnSort", true)[0];

            if (btnSort.CheckState == CheckState.Checked)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Alfabetical sort") != null)
                        btnSort.Text = resxSet.GetString("Alfabetical sort");
                    else
                        btnSort.Text = "Alfabetical sort";
                }
            }
            else
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Default sort") != null)
                        btnSort.Text = resxSet.GetString("Default sort");
                    else
                        btnSort.Text = "Default sort";
                }
            }
            firstPartMedical();
            tabMedical.Controls.Add(btnAll);
            tabMedical.Controls.Add(btnSort);
            secondPartMedical();

            Cursor.Current = Cursors.Default;
        }

        private void firstPartVoluntary()
        {
            tabSkils.Controls.Clear();

        }

        private void loadOnVoluntaryForm()
        {
            Cursor.Current = Cursors.WaitCursor;
            firstPartVoluntary();

            RadToggleButton btnAllVoluntary = new RadToggleButton();
            btnAllVoluntary.CheckState = CheckState.Checked;
            btnAllVoluntary.ThemeName = pagePerson.ThemeName;
            btnAllVoluntary.Name = "btnAllVoluntary";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("Filled data") != null)
                    btnAllVoluntary.Text = resxSet.GetString("Filled data");
                else
                    btnAllVoluntary.Text = "Filled data";
            }
            btnAllVoluntary.Location = new Point(26, 10);
            btnAllVoluntary.Font = new Font("Verdana", 9);
            btnAllVoluntary.CheckStateChanged += btnVoluntaryAll_Click;

            RadToggleButton btnSortVoluntary = new RadToggleButton();
            btnSortVoluntary.CheckState = CheckState.Checked;
            btnSortVoluntary.ThemeName = pagePerson.ThemeName;
            btnSortVoluntary.Width = 200;
            btnSortVoluntary.Name = "btnSortVoluntary";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("Alfabetical sort") != null)
                    btnSortVoluntary.Text = resxSet.GetString("Alfabetical sort");
                else
                    btnSortVoluntary.Text = "Alfabetical sort";
            }
            btnSortVoluntary.Location = new Point(250, 10);
            btnSortVoluntary.Font = new Font("Verdana", 9);
            btnSortVoluntary.CheckStateChanged += btnVoluntarySort_Click;

            tabSkils.Controls.Add(btnAllVoluntary);
            tabSkils.Controls.Add(btnSortVoluntary);

            secondPartVoluntary();

            Cursor.Current = Cursors.Default;

        }

        private void btnVoluntaryAll_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (Person.idContPers != null && Person.idContPers != 0)
            {
                MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
                saveVoluntary();
                if (isVolChanged == true)
                {
                    if (mvb.Delete(Person.idContPers, this.Name, Login._user.idUser) == true)
                    {
                        for (int j = 0; j < personVoluntary.Count; j++)
                        {
                            if (mvb.Save(personVoluntary[j], this.Name, Login._user.idUser) == false)
                            {
                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                {
                                    if (resxSet.GetString("You have not succesufully inserted voluntary data. Please check!") != null)
                                        RadMessageBox.Show(resxSet.GetString("You have not succesufully inserted voluntary data. Please check!"));
                                    else
                                        RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                                }
                            }

                        }
                    }
                    else
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("You have not succesufully inserted voluntary data. Please check!") != null)
                                RadMessageBox.Show(resxSet.GetString("You have not succesufully inserted voluntary data. Please check!"));
                            else
                                RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                        }
                    }
                }


                RadToggleButton btnAllVoluntary = (RadToggleButton)tabSkils.Controls.Find("btnAllVoluntary", true)[0];

                if (btnAllVoluntary.CheckState == CheckState.Checked)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Filled data") != null)
                            btnAllVoluntary.Text = resxSet.GetString("Filled data");
                        else
                            btnAllVoluntary.Text = "Filled data";
                    }
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("All data") != null)
                            btnAllVoluntary.Text = resxSet.GetString("All data");
                        else
                            btnAllVoluntary.Text = "All data";
                    }

                }


                RadToggleButton btnSortVoluntary = (RadToggleButton)tabSkils.Controls.Find("btnSortVoluntary", true)[0];

                if (btnSortVoluntary.CheckState == CheckState.Checked)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Alfabetical sort") != null)
                            btnSortVoluntary.Text = resxSet.GetString("Alfabetical sort");
                        else
                            btnSortVoluntary.Text = "Alfabetical sort";
                    }
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Default sort") != null)
                            btnSortVoluntary.Text = resxSet.GetString("Default sort");
                        else
                            btnSortVoluntary.Text = "Default sort";
                    }
                }

                firstPartVoluntary();
                tabSkils.Controls.Add(btnAllVoluntary);   //
                tabSkils.Controls.Add(btnSortVoluntary);   //
                secondPartVoluntary();

                Cursor.Current = Cursors.Default;
            }
        }

        private void btnVoluntarySort_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (Person.idContPers != null && Person.idContPers != 0)
            {
                MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
                saveVoluntary();
                if (isVolChanged == true)
                {
                    if (mvb.Delete(Person.idContPers, this.Name, Login._user.idUser) == true)
                    {
                        for (int j = 0; j < personVoluntary.Count; j++)
                        {
                            if (mvb.Save(personVoluntary[j], this.Name, Login._user.idUser) == false)    // 
                            {
                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                {
                                    if (resxSet.GetString("You have not succesufully inserted voluntary data. Please check!") != null)
                                        RadMessageBox.Show(resxSet.GetString("You have not succesufully inserted voluntary data. Please check!"));
                                    else
                                        RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                                }
                            }
                        }
                    }
                    else
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("You have not succesufully inserted voluntary data. Please check!") != null)
                                RadMessageBox.Show(resxSet.GetString("You have not succesufully inserted voluntary data. Please check!"));
                            else
                                RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                        }
                    }
                }
            }


            RadToggleButton btnAllVoluntary = (RadToggleButton)tabSkils.Controls.Find("btnAllVoluntary", true)[0];

            if (btnAllVoluntary.CheckState == CheckState.Checked)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Filled data") != null)
                        btnAllVoluntary.Text = resxSet.GetString("Filled data");
                    else
                        btnAllVoluntary.Text = "Filled data";
                }
            }
            else
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("All data") != null)
                        btnAllVoluntary.Text = resxSet.GetString("All data");
                    else
                        btnAllVoluntary.Text = "All data";
                }

            }


            RadToggleButton btnSortVoluntary = (RadToggleButton)tabSkils.Controls.Find("btnSortVoluntary", true)[0];

            if (btnSortVoluntary.CheckState == CheckState.Checked)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Alfabetical sort") != null)
                        btnSortVoluntary.Text = resxSet.GetString("Alfabetical sort");
                    else
                        btnSortVoluntary.Text = "Alfabetical sort";
                }
            }
            else
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Default sort") != null)
                        btnSortVoluntary.Text = resxSet.GetString("Default sort");
                    else
                        btnSortVoluntary.Text = "Default sort";
                }
            }
            firstPartVoluntary();
            tabSkils.Controls.Add(btnAllVoluntary);
            tabSkils.Controls.Add(btnSortVoluntary);
            secondPartVoluntary();

            Cursor.Current = Cursors.Default;
        }

        private void secondPartVoluntary()
        {
            List<string> idQueryType = new List<string>();

            foreach (Control c in panelLabels.Controls)
            {
                RadCheckBox rchk = (RadCheckBox)c;
                if (rchk.Checked == true)
                    idQueryType.Add(rchk.Name.Replace("chkLabel", ""));
            }

            RadToggleButton btnSortVoluntary = (RadToggleButton)tabSkils.Controls.Find("btnSortVoluntary", true)[0];
            RadToggleButton btnAllVoluntary = (RadToggleButton)tabSkils.Controls.Find("btnAllVoluntary", true)[0];

            Boolean isDefaultSort = false;

            if (btnSortVoluntary.CheckState == CheckState.Checked)
                isDefaultSort = true;

            Boolean isAll = false;

            if (btnAllVoluntary.CheckState == CheckState.Checked)
                isAll = true;


            //MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
            MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
            personVoluntary = mvb.GetVoluntaryDetails(idQueryType, Person.idContPers, isDefaultSort, isAll);

            //Y
            int lastBottom = 60;
            //X
            int left = 26;
            string oldQuest = "";
            string oldQuestGroup = "";  // Saki

            if (personVoluntary != null)
            {
                if (personVoluntary.Count > 0)
                {
                    //create dynamic controls
                    for (int i = 0; i < personVoluntary.Count; i++)
                    {
                        string questGroup = ""; // saki
                        string quest = "";
                        string questText = "";
                        string ans = "";
                        string ansText = "";
                        Boolean ischecked = false;
                        //checkbox width
                        int chkWidth = 150;
                        //radiobutton width
                        int rbWidth = 150;
                        //datetimepicker width
                        int dtWidth = 100;
                        //question label width 
                        int rlWidth = (int)(30 * tabSkils.Width / 100) - 20;
                        //answer width (for all controls)
                        int aWidth = (int)(70 * tabSkils.Width / 100) - 80;
                        //question label height 
                        int rlheight = 30;
                        //answer row height  sa 30 na 20 sale
                        int height = 20;

                        if (personVoluntary[i].idQuest.ToString() != "")
                        {
                            quest = personVoluntary[i].idQuest.ToString().TrimEnd();
                        }
                        if (personVoluntary[i].idAns.ToString() != "")
                        {
                            ans = personVoluntary[i].idAns.ToString().TrimEnd();
                        }
                        if (personVoluntary[i].txtQuest.ToString() != "")
                        {
                            questText = personVoluntary[i].txtQuest.ToString().TrimEnd();
                        }
                        if (personVoluntary[i].txtAns.ToString() != "")
                        {
                            ansText = personVoluntary[i].txtAns.ToString().TrimEnd();
                        }
                        if (personVoluntary[i].idcpr != null)
                        {
                            ischecked = true;
                        }


                        //====== Saki
                        questGroup = personVoluntary[i].nameQuestGroup.TrimEnd();
                        //==========
                        quest = personVoluntary[i].idQuest.ToString().TrimEnd();
                        if (personVoluntary[i].questSort != null && personVoluntary[i].questSort != 0)
                            questSort = Convert.ToDecimal(personVoluntary[i].questSort.ToString());
                        if (personVoluntary[i].idAns.ToString() != "")
                        {
                            ans = personVoluntary[i].idAns.ToString().TrimEnd();
                        }
                        questText = personVoluntary[i].txtQuest.ToString().TrimEnd();

                        ansText = personVoluntary[i].txtAns.ToString().TrimEnd();
                        if (personVoluntary[i].idcpr.ToString() != "")
                        {
                            ischecked = true;
                        }


                        //question label
                        RadLabel rl = new RadLabel();
                        RadLabel rlTitle = new RadLabel();  // Saki


                        // ============  ova dva ifa su umesto ORIGINALA ============================
                        if (questGroup.ToUpper() != oldQuestGroup.ToUpper())
                        {
                            rlTitle.Text = questGroup.ToUpper();   //==== Saki
                            rlTitle.Location = new Point(left, lastBottom);
                            rlTitle.Font = new Font("Verdana", 9);
                            rlTitle.ForeColor = Color.DarkOrange;
                            //set multi lines
                            rlTitle.MaximumSize = new Size(rlWidth, height * 3);
                            rlTitle.AutoSize = true;
                            //
                            rlTitle.Width = rlWidth;
                            tabSkils.Controls.Add(rlTitle);
                            //oldQuest = quest;

                            //set rows height depends on number of lines in label question
                            rlheight = height * numberOfLines(rlTitle);
                            lastBottom = lastBottom + rlheight + 5;
                            oldQuestGroup = questGroup;
                        }

                        if (quest != oldQuest)
                        {
                            string s = questSort.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                            string[] parts = s.Split('.');
                            int i1 = int.Parse(parts[0]);
                            int i2 = int.Parse(parts[1]);

                            rl.Text = i1.ToString() + "-" + i2.ToString() + "  " + questText;
                            rl.Location = new Point(left, lastBottom);
                            rl.Font = new Font("Verdana", 9);
                            //set multi lines
                            rl.MaximumSize = new Size(rlWidth, height * 3);
                            rl.AutoSize = true;
                            //
                            rl.Width = rlWidth;
                            tabSkils.Controls.Add(rl);
                            oldQuest = quest;

                            //set rows height depends on number of lines in label question
                            rlheight = height * numberOfLines(rl);
                        }

                        //ANSWER
                        //checkbox type
                        if (personVoluntary[i].idAnsType.ToString() == "1")
                        {
                            RadCheckBox chk = new RadCheckBox();
                            chk.Name = "Q" + quest + "A" + ans;
                            chk.Text = ansText;
                            chk.Width = aWidth;
                            chk.Height = height;
                            chk.Font = new Font("Verdana", 9);
                            chk.Location = new Point(left + rlWidth + 20, lastBottom);
                            chk.CheckStateChanged += radCheckBoxVH_CheckStateChanged;
                            if (ischecked == true)
                            {
                                chk.CheckState = CheckState.Checked;
                            }
                            tabSkils.Controls.Add(chk);

                        }
                        //radio button type
                        else if (personVoluntary[i].idAnsType.ToString() == "2")
                        {
                            RadRadioButton rb = new RadRadioButton();
                            rb.Name = "Q" + quest + "A" + ans;
                            rb.Text = ansText;
                            rb.MaximumSize = new Size(400, 0);
                            rb.MinimumSize = new Size(40, 0);
                            rb.AutoSize = true;
                            rb.Width = rbWidth;
                            rb.Height = height;
                            rb.Font = new Font("Verdana", 9);
                            rb.CheckStateChanged += radRadioButtonVH_CheckStateChanged;
                            if (ischecked == true)
                            {
                                rb.CheckState = CheckState.Checked;
                            }

                            RadGroupBox rgb = new RadGroupBox();
                            rgb.MaximumSize = new Size(400, 0);
                            rgb.MinimumSize = new Size(40, 0);
                            rgb.AutoSize = true;
                            if (tabSkils.Controls.Find("Group" + quest, true).Length > 0)
                            {
                                rgb = (RadGroupBox)tabSkils.Controls.Find("Group" + quest, true)[0];
                                rgb.Height = lastBottom - rgb.Location.Y + height;
                                if (rb.Width > rgb.Width)
                                    rgb.Width = rb.Width;
                            }
                            else
                            {
                                rgb.Name = "Group" + quest;
                                ((Telerik.WinControls.Primitives.FillPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).InnerColor = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).ForeColor = System.Drawing.Color.Transparent;
                                tabSkils.Controls.Add(rgb);
                                rgb.Width = rbWidth;
                                rgb.Height = height;
                                rgb.Font = new Font("Verdana", 9);
                                rgb.Location = new Point(left + rlWidth + 20, lastBottom);
                                rgb.Width = rb.Width;
                            }

                            rb.Location = new Point(0, lastBottom - rgb.Location.Y);

                            rgb.Controls.Add(rb);
                        }
                        //checkbox + textbox type
                        else if (personVoluntary[i].idAnsType.ToString() == "3")
                        {

                            RadCheckBox chk = new RadCheckBox();
                            chk.Name = "Q" + quest + "A" + ans;
                            chk.Text = ansText;
                            chk.MaximumSize = new Size(400, 0);
                            chk.MinimumSize = new Size(40, 0);
                            chk.AutoSize = true;
                            chk.Width = chkWidth;
                            chk.Height = height;
                            chk.Font = new Font("Verdana", 9);
                            chk.Location = new Point(left + rlWidth + 20, lastBottom);
                            chk.CheckStateChanged += radCheckBoxVH_CheckStateChanged;
                            tabSkils.Controls.Add(chk);
                            RadTextBox rtb = new RadTextBox();
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Width = aWidth - chk.Width - 20;
                            rtb.Height = height;
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Location = new Point(left + rlWidth + 20 + chk.Width + 20, lastBottom);
                            rtb.TextChanged += txtVH_TextChanged;
                            if (ischecked == true)
                            {
                                chk.CheckState = CheckState.Checked;
                                if (personVoluntary[i].txt != null)
                                {
                                    rtb.Text = personVoluntary[i].txt.ToString();
                                }

                            }
                            tabSkils.Controls.Add(rtb);
                        }
                        //textbox type
                        else if (personVoluntary[i].idAnsType.ToString() == "4")
                        {
                            RadTextBox rtb = new RadTextBox();
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Width = aWidth;
                            rtb.Height = height;
                            rtb.Location = new Point(left + rlWidth + 20, lastBottom);
                            rtb.TextChanged += txtVH_TextChanged;
                            if (ischecked == true)
                            {
                                if (personVoluntary[i].txt != null)
                                {
                                    rtb.Text = personVoluntary[i].txt.ToString();
                                }
                            }
                            tabSkils.Controls.Add(rtb);
                        }
                        //radio button + textbox type
                        else if (personVoluntary[i].idAnsType.ToString() == "5")
                        {
                            RadRadioButton rb = new RadRadioButton();
                            rb.CheckStateChanged += radRadioButtonVH_CheckStateChanged;
                            rb.Name = "Q" + quest + "A" + ans;
                            rb.Text = ansText;
                            rb.MaximumSize = new Size(400, 0);
                            rb.MinimumSize = new Size(40, 0);
                            rb.AutoSize = true;
                            rb.Width = rbWidth;
                            rb.Height = height;
                            rb.Font = new Font("Verdana", 9);

                            if (ischecked == true)
                            {
                                rb.CheckState = CheckState.Checked;
                            }

                            RadGroupBox rgb = new RadGroupBox();
                            rgb.MaximumSize = new Size(400, 0);
                            rgb.MinimumSize = new Size(40, 0);
                            rgb.AutoSize = true;
                            if (tabSkils.Controls.Find("Group" + quest, true).Length > 0)
                            {
                                rgb = (RadGroupBox)tabSkils.Controls.Find("Group" + quest, true)[0];
                                rgb.Height = lastBottom - rgb.Location.Y + height;
                            }
                            else
                            {
                                rgb.Name = "Group" + quest;
                                ((Telerik.WinControls.Primitives.FillPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).InnerColor = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).ForeColor = System.Drawing.Color.Transparent;
                                tabSkils.Controls.Add(rgb);
                                rgb.Width = rbWidth;
                                rgb.Height = height;
                                rgb.Font = new Font("Verdana", 9);
                                rgb.Location = new Point(left + rlWidth + 20, lastBottom);
                            }

                            rb.Location = new Point(0, lastBottom - rgb.Location.Y);
                            //rb.Click += radRadioButtonVolFuncTrip_Click;
                            //rgb.Width = 50;
                            rgb.Controls.Add(rb);

                            if (tabSkils.Controls.Find("Group" + quest, true).Length > 0)
                            {
                                if (rb.Width > rgb.Width)
                                    rgb.Width = rb.Width;
                            }
                            else
                            {
                                rgb.Width = rb.Width;
                            }

                            RadTextBox rtb = new RadTextBox();
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Width = aWidth - rb.Width - 20;
                            rtb.Height = height;
                            rtb.Location = new Point(left + rlWidth + 20 + rb.Width + 20, lastBottom);
                            rtb.TextChanged += txtVH_TextChanged;
                            if (ischecked == true)
                            {
                                rb.CheckState = CheckState.Checked;
                                if (personVoluntary[i].txt != null)
                                {
                                    rtb.Text = personVoluntary[i].txt.ToString();
                                }

                            }
                            tabSkils.Controls.Add(rtb);
                        }
                        //checkbox + textbox + datetime
                        else if (personVoluntary[i].idAnsType.ToString() == "6")
                        {
                            /*RadCheckBox chk = new RadCheckBox();
                            chk.Name = "Q" + quest + "A" + ans;
                            chk.Text = ansText;
                            chk.MaximumSize = new Size(400, 0);
                            chk.MinimumSize = new Size(40, 0);
                            chk.AutoSize = true;
                            chk.Width = chkWidth;
                            chk.Height = height;
                            chk.Font = new Font("Verdana", 9);
                            chk.Location = new Point(left + rlWidth + 20, lastBottom);
                            chk.CheckStateChanged += radCheckBoxVH_CheckStateChanged;
                            tabSkils.Controls.Add(chk);
                            RadTextBox rtb = new RadTextBox();
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Width = aWidth - chk.Width - 20 - dtWidth - 20;
                            rtb.Height = height;
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Location = new Point(left + rlWidth + 20 + chk.Width + 20, lastBottom);
                            if (ischecked == true)
                            {
                                chk.CheckState = CheckState.Checked;
                                if (mvm[i].txt != null)
                                {
                                    rtb.Text = mvm[i].txt.ToString();
                                }

                            }
                            tabSkils.Controls.Add(rtb);
                            RadDateTimePicker dt = new RadDateTimePicker();
                            dt.Format = DateTimePickerFormat.Short;
                            dt.Name = "dtQ" + quest + "A" + ans;
                            dt.Width = dtWidth;
                            dt.Height = height;
                            dt.Font = new Font("Verdana", 9);
                            dt.Location = new Point(left + rlWidth + 20 + chk.Width + 20 + rtb.Width + 20, lastBottom);
                            if (ischecked == true)
                            {
                                if (mvm[i].dateAns != null)
                                {
                                    if (mvm[i].dateAns.ToString() != "")
                                    {
                                        dt.Value = Convert.ToDateTime(ds.Tables[0].Rows[i]["dateAns"].ToString());
                                    }
                                }

                            }
                            Skils.Controls.Add(dt);*/
                        }
                        //checkbox + textbox + datetime
                        /*else if (mvm[i].mvm[i]..ToString() == "7")
                        {
                            RadTextBox rtb = new RadTextBox();
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Width = aWidth - dtWidth - 20;
                            rtb.Height = height;
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Location = new Point(left + rlWidth + 20, lastBottom);
                            if (ischecked == true)
                            {
                                if (mvm[i].txt!= null)
                                {
                                    rtb.Text = mvm[i].txt.ToString();
                                }
                            }
                            tabSkils.Controls.Add(rtb);
                            RadDateTimePicker dt = new RadDateTimePicker();
                            dt.Format = DateTimePickerFormat.Short;
                            dt.Name = "dtQ" + quest + "A" + ans;
                            dt.Width = dtWidth;
                            dt.Height = height;
                            dt.Font = new Font("Verdana", 9);
                            dt.Location = new Point(left + rlWidth + 20 + rtb.Width + 20, lastBottom);
                            if (ischecked == true)
                            {
                                if (mvm[i].dateAns != null)
                                {
                                    if (mvm[i].dateAns.ToString() != "")
                                    {
                                        dt.Value = Convert.ToDateTimemvm[i].dateAns.ToString());
                                    }
                                }

                            }
                            tabSkils.Controls.Add(dt);
                        }*/
                        //set X for next row
                        lastBottom = lastBottom + rlheight + 5;
                    }
                }
            }
            isVolChanged = false;
        }

        private void saveMedical()
        {
            if (Person.idContPers != 0)
            {
                if (tabMedical.Controls != null)
                {
                    personMedical = new List<MedicalVoluntaryModel>();
                    MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
                    int idFilter = Login._personFilters.Find(item => item.nameFilter.TrimEnd() == tabMedical.Text.TrimEnd()).idFilter;
                    if (tabMedical.Controls != null)
                    {
                        for (int i = 0; i < tabMedical.Controls.Count; i++)
                        {
                            Control c = tabMedical.Controls[i];
                            MedicalVoluntaryModel mvm = new MedicalVoluntaryModel();
                            if (c is RadCheckBox)
                            {
                                string idQuest = c.Name.Split('A')[0].Substring(1, c.Name.Split('A')[0].Length - 1);
                                string idAns = c.Name.Split('A')[1].Substring(0, c.Name.Split('A')[1].Length);
                                RadCheckBox chk = (RadCheckBox)c;
                                if (chk.CheckState == CheckState.Checked)
                                {
                                    mvm.idcpr = Person.idContPers;
                                    mvm.idQuest = Convert.ToInt32(idQuest);
                                    mvm.idAns = Convert.ToInt32(idAns);
                                    personMedical.Add(mvm);

                                }
                            }
                            if (c is RadGroupBox)
                            {
                                RadGroupBox rgb = (RadGroupBox)c;
                                for (int j = 0; j < rgb.Controls.Count; j++)
                                {
                                    string idQuest = rgb.Controls[j].Name.Split('A')[0].Substring(1, rgb.Controls[j].Name.Split('A')[0].Length - 1);
                                    string idAns = rgb.Controls[j].Name.Split('A')[1].Substring(0, rgb.Controls[j].Name.Split('A')[1].Length);
                                    RadRadioButton rb = (RadRadioButton)rgb.Controls[j];
                                    //=============
                                    //mvm = personMedical.Find(item => item.idcpr == Person.idContPers && item.idQuest == Convert.ToInt32(idQuest) && item.idAns == Convert.ToInt32(idAns));
                                    //if (mvm != null)  //
                                    //{             //

                                    if (rb.CheckState == CheckState.Checked)
                                    {
                                        mvm = new MedicalVoluntaryModel();
                                        mvm.idcpr = Person.idContPers;
                                        mvm.idQuest = Convert.ToInt32(idQuest);
                                        mvm.idAns = Convert.ToInt32(idAns);
                                        personMedical.Add(mvm);
                                    }
                                    // } 
                                }
                            }
                            if (c is RadTextBox)
                            {
                                string idQuest = c.Name.Split('A')[0].Substring(4, c.Name.Split('A')[0].Length - 4);
                                string idAns = c.Name.Split('A')[1].Substring(0, c.Name.Split('A')[1].Length);
                                RadTextBox rtb = (RadTextBox)c;
                                mvm = personMedical.Find(item => item.idcpr == Person.idContPers && item.idQuest == Convert.ToInt32(idQuest) && item.idAns == Convert.ToInt32(idAns));
                                if (mvm != null)
                                {
                                    if (rtb.Text != "")
                                    {
                                        mvm.txt = rtb.Text;
                                    }
                                }
                                else
                                {
                                    if (rtb.Text != "")
                                    {
                                        mvm = new MedicalVoluntaryModel();  // Saki ubacio crkavao ...
                                        mvm.idcpr = Convert.ToInt32(Person.idContPers);
                                        mvm.idQuest = Convert.ToInt32(idQuest);
                                        mvm.idAns = Convert.ToInt32(idAns);
                                        mvm.txt = rtb.Text;
                                        personMedical.Add(mvm);
                                    }

                                }
                            }
                        }
                    }
                }
                else
                    isMedChanged = false;
            }
            else
                isMedChanged = false;
        }

        // ================================================
        private void saveVoluntary()
        {
            if (Person.idContPers != 0)
            {
                if (tabVoluntary.Controls != null)
                {
                    personVoluntary = new List<MedicalVoluntaryModel>();
                    MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
                    int idFilter = Login._personFilters.Find(item => item.nameFilter.TrimEnd() == tabVoluntary.Text.TrimEnd()).idFilter;

                    saveLabels();
                    if (tabSkils.Controls != null)
                    {
                        for (int i = 0; i < tabSkils.Controls.Count; i++)
                        {
                            Control c = tabSkils.Controls[i];
                            MedicalVoluntaryModel mvm = new MedicalVoluntaryModel();
                            if (c is RadCheckBox)
                            {
                                RadCheckBox chk = (RadCheckBox)c;
                                if (chk.CheckState == CheckState.Checked)
                                {
                                    string idQuest = c.Name.Split('A')[0].Substring(1, c.Name.Split('A')[0].Length - 1);
                                    string idAns = c.Name.Split('A')[1].Substring(0, c.Name.Split('A')[1].Length);
                                    List<MedicalVoluntaryModel> volSameQuestAns = new List<MedicalVoluntaryModel>();
                                    if (PersonLabel.Count > 1)
                                    {
                                        volSameQuestAns = new MedicalVoluntaryBUS().GetSameQuestAnswer(idQuest, idAns);
                                    }

                                    if (volSameQuestAns != null)
                                    {
                                        if (volSameQuestAns.Count > 0)
                                        {
                                            foreach (MedicalVoluntaryModel m in volSameQuestAns)
                                            {
                                                if (personVoluntary1.Find(s => s.idQuest == m.idQuest && s.idAns == m.idAns) == null)
                                                {
                                                    mvm = new MedicalVoluntaryModel();
                                                    mvm.idcpr = Person.idContPers;
                                                    mvm.idQuest = m.idQuest;
                                                    mvm.idAns = m.idAns;
                                                    personVoluntary.Add(mvm);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            mvm = new MedicalVoluntaryModel();
                                            mvm.idcpr = Person.idContPers;
                                            mvm.idQuest = Convert.ToInt32(idQuest);
                                            mvm.idAns = Convert.ToInt32(idAns);
                                            personVoluntary.Add(mvm);
                                        }

                                    }
                                    else
                                    {
                                        mvm = new MedicalVoluntaryModel();
                                        mvm.idcpr = Person.idContPers;
                                        mvm.idQuest = Convert.ToInt32(idQuest);
                                        mvm.idAns = Convert.ToInt32(idAns);
                                        personVoluntary.Add(mvm);
                                    }

                                }
                            }
                            if (c is RadGroupBox)
                            {
                                RadGroupBox rgb = (RadGroupBox)c;
                                for (int j = 0; j < rgb.Controls.Count; j++)
                                {
                                    string idQuest = rgb.Controls[j].Name.Split('A')[0].Substring(1, rgb.Controls[j].Name.Split('A')[0].Length - 1);
                                    string idAns = rgb.Controls[j].Name.Split('A')[1].Substring(0, rgb.Controls[j].Name.Split('A')[1].Length);
                                    RadRadioButton rb = (RadRadioButton)rgb.Controls[j];
                                    //=============
                                    //mvm = personMedical.Find(item => item.idcpr == Person.idContPers && item.idQuest == Convert.ToInt32(idQuest) && item.idAns == Convert.ToInt32(idAns));
                                    //if (mvm != null)  //
                                    //{             //
                                    List<MedicalVoluntaryModel> volSameQuestAns = new List<MedicalVoluntaryModel>();
                                    if (PersonLabel.Count > 1)
                                    {
                                        volSameQuestAns = new MedicalVoluntaryBUS().GetSameQuestAnswer(idQuest, idAns);
                                    }
                                    if (rb.CheckState == CheckState.Checked)
                                    {
                                        if (volSameQuestAns != null)
                                        {
                                            if (volSameQuestAns.Count > 0)
                                            {
                                                foreach (MedicalVoluntaryModel m in volSameQuestAns)
                                                {
                                                    if (personVoluntary1.Find(s => s.idQuest == m.idQuest && s.idAns == m.idAns) == null)
                                                    {
                                                        mvm = new MedicalVoluntaryModel();
                                                        mvm.idcpr = Person.idContPers;
                                                        mvm.idQuest = m.idQuest;
                                                        mvm.idAns = m.idAns;
                                                        personVoluntary.Add(mvm);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                mvm = new MedicalVoluntaryModel();
                                                mvm.idcpr = Person.idContPers;
                                                mvm.idQuest = Convert.ToInt32(idQuest);
                                                mvm.idAns = Convert.ToInt32(idAns);
                                                personVoluntary.Add(mvm);
                                            }

                                        }
                                        else
                                        {
                                            mvm = new MedicalVoluntaryModel();
                                            mvm.idcpr = Person.idContPers;
                                            mvm.idQuest = Convert.ToInt32(idQuest);
                                            mvm.idAns = Convert.ToInt32(idAns);
                                            personVoluntary.Add(mvm);
                                        }
                                    }
                                    // } 
                                }
                            }
                            if (c is RadTextBox)
                            {
                                string idQuest = c.Name.Split('A')[0].Substring(4, c.Name.Split('A')[0].Length - 4);
                                string idAns = c.Name.Split('A')[1].Substring(0, c.Name.Split('A')[1].Length);
                                RadTextBox rtb = (RadTextBox)c;
                                //Control[] dt = Controls.Find(c.Name.Replace("txt", "dt"), true);
                                //string date = null;
                                //if (dt.Length > 0)
                                //{
                                //  RadDateTimePicker rdt = (RadDateTimePicker)dt[0];
                                //set datetime format
                                //      date = rdt.Value.ToString("MM/dd/yyyy hh:mm:ss");
                                // }
                                List<MedicalVoluntaryModel> volSameQuestAns = new List<MedicalVoluntaryModel>();
                                if (PersonLabel.Count > 1)
                                {
                                    volSameQuestAns = new MedicalVoluntaryBUS().GetSameQuestAnswer(idQuest, idAns);
                                }
                                mvm = personVoluntary.Find(item => item.idcpr == Person.idContPers && item.idQuest == Convert.ToInt32(idQuest) && item.idAns == Convert.ToInt32(idAns));
                                if (mvm != null)
                                {
                                    if (rtb.Text != "")
                                    {
                                        mvm.txt = rtb.Text;
                                    }
                                }
                                else
                                {
                                    if (rtb.Text != "")
                                    {
                                        if (volSameQuestAns != null)
                                        {
                                            if (volSameQuestAns.Count > 0)
                                            {
                                                foreach (MedicalVoluntaryModel m in volSameQuestAns)
                                                {
                                                    if (personVoluntary1.Find(s => s.idQuest == m.idQuest && s.idAns == m.idAns) == null)
                                                    {
                                                        mvm = new MedicalVoluntaryModel();
                                                        mvm.idcpr = Person.idContPers;
                                                        mvm.idQuest = m.idQuest;
                                                        mvm.idAns = m.idAns;
                                                        mvm.txt = rtb.Text;
                                                        personVoluntary.Add(mvm);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                mvm = new MedicalVoluntaryModel();
                                                mvm.idcpr = Person.idContPers;
                                                mvm.idQuest = Convert.ToInt32(idQuest);
                                                mvm.idAns = Convert.ToInt32(idAns);
                                                mvm.txt = rtb.Text;
                                                personVoluntary.Add(mvm);
                                            }

                                        }
                                        else
                                        {
                                            mvm = new MedicalVoluntaryModel();
                                            mvm.idcpr = Person.idContPers;
                                            mvm.idQuest = Convert.ToInt32(idQuest);
                                            mvm.idAns = Convert.ToInt32(idAns);
                                            mvm.txt = rtb.Text;
                                            personVoluntary.Add(mvm);
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
                else
                    isVolChanged = false;
            }
            else
                isVolChanged = false;
        }

        private void saveVoluntaryFunction()
        {
            if (Person.idContPers != 0)
            {
                if (tabVoluntary.Controls != null)
                {
                    personVoluntary1 = new List<VolontaryFunctionModel>();
                    VolontaryFunctionBUS vfb = new VolontaryFunctionBUS();
                    int idFilter = Login._personFilters.Find(item => item.nameFilter.TrimEnd() == tabVoluntary.Text.TrimEnd()).idFilter;
                    saveLabels();
                    if (tabFunction.Controls != null)
                    {
                        for (int i = 0; i < tabFunction.Controls.Count; i++)
                        {
                            Control c = tabFunction.Controls[i];
                            VolontaryFunctionModel mvm = new VolontaryFunctionModel();
                            if (c is RadCheckBox)
                            {
                                RadCheckBox chk = (RadCheckBox)c;
                                if (chk.CheckState == CheckState.Checked)
                                {
                                    string idQuest = c.Name.Split('A')[0].Substring(1, c.Name.Split('A')[0].Length - 1);
                                    string idAns = c.Name.Split('A')[1].Substring(0, c.Name.Split('A')[1].Length);
                                    List<VolontaryFunctionModel> volSameQuestAns = new List<VolontaryFunctionModel>();
                                    if (PersonLabel.Count > 1)
                                    {
                                        volSameQuestAns = new VolontaryFunctionBUS().GetSameQuestAnswer(idQuest, idAns, Person.idContPers.ToString());
                                    }

                                    if (volSameQuestAns != null)
                                    {
                                        if (volSameQuestAns.Count > 0)
                                        {

                                            foreach (VolontaryFunctionModel m in volSameQuestAns)
                                            {
                                                if (personVoluntary1.Find(s => s.idQuest == m.idQuest && s.idAns == m.idAns) == null)
                                                {
                                                    mvm = new VolontaryFunctionModel();
                                                    mvm.idcpr = Person.idContPers;
                                                    mvm.idQuest = m.idQuest;
                                                    mvm.idAns = m.idAns;
                                                    personVoluntary1.Add(mvm);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            mvm = new VolontaryFunctionModel();
                                            mvm.idcpr = Person.idContPers;
                                            mvm.idQuest = Convert.ToInt32(idQuest);
                                            mvm.idAns = Convert.ToInt32(idAns);
                                            personVoluntary1.Add(mvm);
                                        }

                                    }
                                    else
                                    {
                                        mvm = new VolontaryFunctionModel();
                                        mvm.idcpr = Person.idContPers;
                                        mvm.idQuest = Convert.ToInt32(idQuest);
                                        mvm.idAns = Convert.ToInt32(idAns);
                                        personVoluntary1.Add(mvm);
                                    }

                                }
                            }
                            if (c is RadGroupBox)
                            {
                                RadGroupBox rgb = (RadGroupBox)c;
                                for (int j = 0; j < rgb.Controls.Count; j++)
                                {
                                    string idQuest = rgb.Controls[j].Name.Split('A')[0].Substring(1, rgb.Controls[j].Name.Split('A')[0].Length - 1);
                                    string idAns = rgb.Controls[j].Name.Split('A')[1].Substring(0, rgb.Controls[j].Name.Split('A')[1].Length);
                                    List<VolontaryFunctionModel> volSameQuestAns = new List<VolontaryFunctionModel>();
                                    if (PersonLabel.Count > 1)
                                    {
                                        volSameQuestAns = new VolontaryFunctionBUS().GetSameQuestAnswer(idQuest, idAns, Person.idContPers.ToString());
                                    }
                                    RadRadioButton rb = (RadRadioButton)rgb.Controls[j];
                                    //=============
                                    //mvm = personMedical.Find(item => item.idcpr == Person.idContPers && item.idQuest == Convert.ToInt32(idQuest) && item.idAns == Convert.ToInt32(idAns));
                                    //if (mvm != null)  //
                                    //{             //

                                    if (rb.CheckState == CheckState.Checked)
                                    {
                                        if (volSameQuestAns != null)
                                        {
                                            if (volSameQuestAns.Count > 0)
                                            {
                                                foreach (VolontaryFunctionModel m in volSameQuestAns)
                                                {
                                                    if (personVoluntary1.Find(s => s.idQuest == m.idQuest && s.idAns == m.idAns) == null)
                                                    {
                                                        mvm = new VolontaryFunctionModel();
                                                        mvm.idcpr = Person.idContPers;
                                                        mvm.idQuest = m.idQuest;
                                                        mvm.idAns = m.idAns;
                                                        personVoluntary1.Add(mvm);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                mvm = new VolontaryFunctionModel();
                                                mvm.idcpr = Person.idContPers;
                                                mvm.idQuest = Convert.ToInt32(idQuest);
                                                mvm.idAns = Convert.ToInt32(idAns);
                                                personVoluntary1.Add(mvm);
                                            }

                                        }
                                        else
                                        {
                                            mvm = new VolontaryFunctionModel();
                                            mvm.idcpr = Person.idContPers;
                                            mvm.idQuest = Convert.ToInt32(idQuest);
                                            mvm.idAns = Convert.ToInt32(idAns);
                                            personVoluntary1.Add(mvm);
                                        }
                                    }
                                    // } 
                                }
                            }
                            if (c is RadTextBox)
                            {
                                string idQuest = c.Name.Split('A')[0].Substring(4, c.Name.Split('A')[0].Length - 4);
                                string idAns = c.Name.Split('A')[1].Substring(0, c.Name.Split('A')[1].Length);
                                List<VolontaryFunctionModel> volSameQuestAns = new List<VolontaryFunctionModel>();
                                if (PersonLabel.Count > 1)
                                {
                                    volSameQuestAns = new VolontaryFunctionBUS().GetSameQuestAnswer(idQuest, idAns, Person.idContPers.ToString());
                                }
                                RadTextBox rtb = (RadTextBox)c;
                                //Control[] dt = Controls.Find(c.Name.Replace("txt", "dt"), true);
                                //string date = null;
                                //if (dt.Length > 0)
                                //{
                                //  RadDateTimePicker rdt = (RadDateTimePicker)dt[0];
                                //set datetime format
                                //      date = rdt.Value.ToString("MM/dd/yyyy hh:mm:ss");
                                // }
                                mvm = personVoluntary1.Find(item => item.idcpr == Person.idContPers && item.idQuest == Convert.ToInt32(idQuest) && item.idAns == Convert.ToInt32(idAns));
                                if (mvm != null)
                                {
                                    if (rtb.Text != "")
                                    {
                                        mvm.txt = rtb.Text;
                                    }
                                }
                                else
                                {
                                    if (rtb.Text != "")
                                    {
                                        if (volSameQuestAns != null)
                                        {
                                            if (volSameQuestAns.Count > 0)
                                            {
                                                foreach (VolontaryFunctionModel m in volSameQuestAns)
                                                {
                                                    if (personVoluntary1.Find(s => s.idQuest == m.idQuest && s.idAns == m.idAns) == null)
                                                    {
                                                        mvm = new VolontaryFunctionModel();
                                                        mvm.idcpr = Person.idContPers;
                                                        mvm.idQuest = m.idQuest;
                                                        mvm.idAns = m.idAns;
                                                        mvm.txt = rtb.Text;
                                                        personVoluntary1.Add(mvm);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                mvm = new VolontaryFunctionModel();
                                                mvm.idcpr = Person.idContPers;
                                                mvm.idQuest = Convert.ToInt32(idQuest);
                                                mvm.idAns = Convert.ToInt32(idAns);
                                                mvm.txt = rtb.Text;
                                                personVoluntary1.Add(mvm);
                                            }

                                        }
                                        else
                                        {
                                            mvm = new VolontaryFunctionModel();
                                            mvm.idcpr = Person.idContPers;
                                            mvm.idQuest = Convert.ToInt32(idQuest);
                                            mvm.idAns = Convert.ToInt32(idAns);
                                            mvm.txt = rtb.Text;
                                            personVoluntary1.Add(mvm);
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
                else
                    isVolChanged = false;
            }
            else
                isVolChanged = false;
        }

        private void saveVoluntaryTrip()
        {
            if (Person.idContPers != 0)
            {
                if (tabVoluntary.Controls != null)
                {

                    personVoluntary2 = new List<VolontaryTripModel>();
                    VolontaryTripBUS vtb = new VolontaryTripBUS();
                    int idFilter = Login._personFilters.Find(item => item.nameFilter.TrimEnd() == tabVoluntary.Text.TrimEnd()).idFilter;

                    saveLabels();
                    if (tabTrips.Controls != null)
                    {
                        for (int i = 0; i < tabTrips.Controls.Count; i++)
                        {
                            Control c = tabTrips.Controls[i];
                            VolontaryTripModel mvm = new VolontaryTripModel();
                            if (c is RadCheckBox)
                            {
                                RadCheckBox chk = (RadCheckBox)c;
                                if (chk.CheckState == CheckState.Checked)
                                {
                                    string idQuest = c.Name.Split('A')[0].Substring(1, c.Name.Split('A')[0].Length - 1);
                                    string idAns = c.Name.Split('A')[1].Substring(0, c.Name.Split('A')[1].Length);
                                    List<VolontaryTripModel> volSameQuestAns = new List<VolontaryTripModel>();
                                    if (PersonLabel.Count > 1)
                                    {
                                        volSameQuestAns = new VolontaryTripBUS().GetSameQuestAnswer(idQuest, idAns);
                                    }

                                    if (volSameQuestAns != null)
                                    {
                                        if (volSameQuestAns.Count > 0)
                                        {
                                            foreach (VolontaryTripModel m in volSameQuestAns)
                                            {
                                                if (personVoluntary1.Find(s => s.idQuest == m.idQuest && s.idAns == m.idAns) == null)
                                                {
                                                    mvm = new VolontaryTripModel();
                                                    mvm.idcpr = Person.idContPers;
                                                    mvm.idQuest = m.idQuest;
                                                    mvm.idAns = m.idAns;
                                                    personVoluntary2.Add(mvm);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            mvm = new VolontaryTripModel();
                                            mvm.idcpr = Person.idContPers;
                                            mvm.idQuest = Convert.ToInt32(idQuest);
                                            mvm.idAns = Convert.ToInt32(idAns);
                                            personVoluntary2.Add(mvm);
                                        }

                                    }
                                    else
                                    {
                                        mvm = new VolontaryTripModel();
                                        mvm.idcpr = Person.idContPers;
                                        mvm.idQuest = Convert.ToInt32(idQuest);
                                        mvm.idAns = Convert.ToInt32(idAns);
                                        personVoluntary2.Add(mvm);
                                    }
                                }
                            }
                            if (c is RadGroupBox)
                            {
                                RadGroupBox rgb = (RadGroupBox)c;
                                for (int j = 0; j < rgb.Controls.Count; j++)
                                {
                                    string idQuest = rgb.Controls[j].Name.Split('A')[0].Substring(1, rgb.Controls[j].Name.Split('A')[0].Length - 1);
                                    string idAns = rgb.Controls[j].Name.Split('A')[1].Substring(0, rgb.Controls[j].Name.Split('A')[1].Length);
                                    List<VolontaryTripModel> volSameQuestAns = new List<VolontaryTripModel>();
                                    if (PersonLabel.Count > 1)
                                    {
                                        volSameQuestAns = new VolontaryTripBUS().GetSameQuestAnswer(idQuest, idAns);
                                    }
                                    RadRadioButton rb = (RadRadioButton)rgb.Controls[j];
                                    //=============
                                    //mvm = personMedical.Find(item => item.idcpr == Person.idContPers && item.idQuest == Convert.ToInt32(idQuest) && item.idAns == Convert.ToInt32(idAns));
                                    //if (mvm != null)  //
                                    //{             //

                                    if (rb.CheckState == CheckState.Checked)
                                    {
                                        if (volSameQuestAns != null)
                                        {
                                            if (volSameQuestAns.Count > 0)
                                            {
                                                foreach (VolontaryTripModel m in volSameQuestAns)
                                                {
                                                    if (personVoluntary1.Find(s => s.idQuest == m.idQuest && s.idAns == m.idAns) == null)
                                                    {
                                                        mvm = new VolontaryTripModel();
                                                        mvm.idcpr = Person.idContPers;
                                                        mvm.idQuest = m.idQuest;
                                                        mvm.idAns = m.idAns;
                                                        personVoluntary2.Add(mvm);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                mvm = new VolontaryTripModel();
                                                mvm.idcpr = Person.idContPers;
                                                mvm.idQuest = Convert.ToInt32(idQuest);
                                                mvm.idAns = Convert.ToInt32(idAns);
                                                personVoluntary2.Add(mvm);
                                            }

                                        }
                                        else
                                        {
                                            mvm = new VolontaryTripModel();
                                            mvm.idcpr = Person.idContPers;
                                            mvm.idQuest = Convert.ToInt32(idQuest);
                                            mvm.idAns = Convert.ToInt32(idAns);
                                            personVoluntary2.Add(mvm);
                                        }
                                    }
                                    // } 
                                }
                            }
                            if (c is RadTextBox)
                            {
                                string idQuest = c.Name.Split('A')[0].Substring(4, c.Name.Split('A')[0].Length - 4);
                                string idAns = c.Name.Split('A')[1].Substring(0, c.Name.Split('A')[1].Length);
                                List<VolontaryTripModel> volSameQuestAns = new List<VolontaryTripModel>();
                                if (PersonLabel.Count > 1)
                                {
                                    volSameQuestAns = new VolontaryTripBUS().GetSameQuestAnswer(idQuest, idAns);
                                }
                                RadTextBox rtb = (RadTextBox)c;
                                //Control[] dt = Controls.Find(c.Name.Replace("txt", "dt"), true);
                                //string date = null;
                                //if (dt.Length > 0)
                                //{
                                //  RadDateTimePicker rdt = (RadDateTimePicker)dt[0];
                                //set datetime format
                                //      date = rdt.Value.ToString("MM/dd/yyyy hh:mm:ss");
                                // }
                                mvm = personVoluntary2.Find(item => item.idcpr == Person.idContPers && item.idQuest == Convert.ToInt32(idQuest) && item.idAns == Convert.ToInt32(idAns));
                                if (mvm != null)
                                {
                                    if (rtb.Text != "")
                                    {
                                        mvm.txt = rtb.Text;
                                    }
                                }
                                else
                                {
                                    if (rtb.Text != "")
                                    {
                                        if (volSameQuestAns != null)
                                        {
                                            if (volSameQuestAns.Count > 0)
                                            {
                                                foreach (VolontaryTripModel m in volSameQuestAns)
                                                {
                                                    if (personVoluntary1.Find(s => s.idQuest == m.idQuest && s.idAns == m.idAns) == null)
                                                    {
                                                        mvm = new VolontaryTripModel();
                                                        mvm.idcpr = Person.idContPers;
                                                        mvm.idQuest = m.idQuest;
                                                        mvm.idAns = m.idAns;
                                                        mvm.txt = rtb.Text;
                                                        personVoluntary2.Add(mvm);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                mvm = new VolontaryTripModel();
                                                mvm.idcpr = Person.idContPers;
                                                mvm.idQuest = Convert.ToInt32(idQuest);
                                                mvm.idAns = Convert.ToInt32(idAns);
                                                mvm.txt = rtb.Text;
                                                personVoluntary2.Add(mvm);
                                            }

                                        }
                                        else
                                        {
                                            mvm = new VolontaryTripModel();
                                            mvm.idcpr = Person.idContPers;
                                            mvm.idQuest = Convert.ToInt32(idQuest);
                                            mvm.idAns = Convert.ToInt32(idAns);
                                            mvm.txt = rtb.Text;
                                            personVoluntary2.Add(mvm);
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
                else
                    isVolChanged = false;
            }
            else
                isVolChanged = false;
        }
        //=======================================

        private void radCheckBoxVH_CheckStateChanged(object sender, EventArgs e)
        {
            RadCheckBox chk = (RadCheckBox)sender;
            string txtName = "txt" + chk.Name;
            RadTextBox rtb = new RadTextBox();
            rtb.Name = txtName;
            if (chk.CheckState != CheckState.Checked)
            {
                if (tabVoluntary.Controls.Find(txtName, true) != null)
                    if (tabVoluntary.Controls.Find(txtName, true).Length > 0)
                    {
                        rtb = (RadTextBox)tabVoluntary.Controls.Find(txtName, true)[0];
                        rtb.Text = "";
                        Control[] dt = tabVoluntary.Controls.Find(txtName.Replace("txt", "dt"), true);
                        if (dt.Length > 0)
                        {
                            RadDateTimePicker rdt = (RadDateTimePicker)dt[0];
                            rdt.Value = DateTime.Now;
                        }
                    }
            }
            isVolChanged = true;
        }

        private void radRadioButtonVH_CheckStateChanged(object sender, EventArgs e)
        {
            RadRadioButton rrb = (RadRadioButton)sender;
            string txtName = "txt" + rrb.Name;
            RadTextBox rtb = new RadTextBox();
            rtb.Name = txtName;
            if (rrb.CheckState != CheckState.Checked)
            {
                if (tabVoluntary.Controls.Find(txtName, true) != null)
                    if (tabVoluntary.Controls.Find(txtName, true).Length > 0)
                    {
                        rtb = (RadTextBox)tabVoluntary.Controls.Find(txtName, true)[0];
                        rtb.Text = "";
                    }
            }
            isVolChanged = true;
        }

        private void txtVH_TextChanged(object sender, EventArgs e)
        {
            isVolChanged = true;
        }

        //count number of lines in label
        private int numberOfLines(RadLabel rl)
        {
            Graphics g = rl.CreateGraphics();
            Single LineHeight = g.MeasureString("X", rl.Font).Height;
            Single TotalHeight = g.MeasureString(rl.Text, rl.Font, rl.Width).Height;
            int nl = (int)Math.Round(TotalHeight / LineHeight);

            return nl;
        }

        private void radCheckBox_CheckStateChanged_type1(object sender, EventArgs e)
        {
            isMedChanged = true;
        }

        private void radRadioButtonMedical_Click(object sender, EventArgs e)
        {
            isMedChanged = true;
        }

        private void radTextBoxMedical_TextChanged(object sender, EventArgs e)
        {
            isMedChanged = true;
        }

        private void radCheckBox_CheckStateChanged_type3(object sender, EventArgs e)
        {
            RadCheckBox chk = (RadCheckBox)sender;
            string txtName = "txt" + chk.Name;
            RadTextBox rtb = new RadTextBox();
            rtb.Name = txtName;
            if (chk.CheckState != CheckState.Checked)
            {
                rtb = (RadTextBox)tabMedical.Controls.Find(txtName, true)[0];
                rtb.Text = "";
            }
            isMedChanged = true;
        }

        private void radRadioButton_CheckStateChanged(object sender, EventArgs e)
        {
            RadRadioButton rrb = (RadRadioButton)sender;
            string txtName = "txt" + rrb.Name;
            RadTextBox rtb = new RadTextBox();
            rtb.Name = txtName;
            if (rrb.CheckState != CheckState.Checked)
            {
                rtb = (RadTextBox)tabMedical.Controls.Find(txtName, true)[0];
                rtb.Text = "";
            }
        }

        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                tabRelation.Text = resxSet.GetString("Relation");
                tabAccount.Text = resxSet.GetString("Accounting");
                tabMedical.Text = resxSet.GetString("Medical");
                tabVoluntary.Text = resxSet.GetString("Voluntary helper");
                tabDocuments.Text = resxSet.GetString("Document");
                tabCperson.Text = resxSet.GetString("Contact person");
                tabAvailability.Text = resxSet.GetString("Availability");
                tabMemo.Text = resxSet.GetString("Memo");
                lblFirstName.Text = resxSet.GetString("First name");
                lblLastName.Text = resxSet.GetString("Last name");
                lblMidName.Text = resxSet.GetString("Middle name");
                //lblMaidenName.Text = resxSet.GetString("Maiden name");
                chkIsMaried.Text = resxSet.GetString("Married");
                chkDied.Text = resxSet.GetString("Died");
                lblBirthDate.Text = resxSet.GetString("Birth date");
                lblInitialsTitle.Text = resxSet.GetString("Initials") + " / " + resxSet.GetString("Title");
                lblGender.Text = resxSet.GetString("Gender");
                chkPayInvoice.Text = resxSet.GetString("Pay invoice");
                chkActive.Text = resxSet.GetString("Active");
                tabCommunication.Text = resxSet.GetString("Activities");
                chkSendProspect.Text = resxSet.GetString("Send mailing");
                chkPicturePermission.Text = resxSet.GetString("No permission pictures");
                btnDeletePicture.Text = resxSet.GetString("Delete");
                btnUpload.Text = resxSet.GetString("Upload");
                if (resxSet.GetString(lblWithUs.Text) != null)
                    lblWithUs.Text = resxSet.GetString(lblWithUs.Text);
                //lblWithUs.Text = resxSet.GetString("Travel with us");

                //passport
                lblPassportName.Text = resxSet.GetString("Passport") + " " + resxSet.GetString("Name").ToLower();
                lblPassportNr.Text = resxSet.GetString("Passport") + " " + resxSet.GetString("Number").ToLower();
                lblPassLastname.Text = resxSet.GetString("Passport") + " " + resxSet.GetString("Last name").ToLower();
                lblBirthPlace.Text = resxSet.GetString("Birth place");
                lblIssuePlace.Text = resxSet.GetString("Issue place");
                lblIssueDate.Text = resxSet.GetString("Issue date");
                lblValidTo.Text = resxSet.GetString("Valid to");
                lblNacionality.Text = resxSet.GetString("Nacionality");

                tabSkils.Text = resxSet.GetString("Skills");
                tabTrips.Text = resxSet.GetString("Trip preferences");
                tabFeatures.Text = resxSet.GetString("Features");
                tabFunction.Text = resxSet.GetString("Function");
                //-- account
                chkDebitor.Text = resxSet.GetString("Debitor");
                chkCreditor.Text = resxSet.GetString("Creditor");
                lblDebitor.Text = resxSet.GetString("Debitor account");
                lblCreditor.Text = resxSet.GetString("Creditor account");
                lblPayCond.Text = resxSet.GetString("Payment conditions");
                //
                //ribbon bar
                btnSave.Text = resxSet.GetString("Save");
                btnNewDoc.Text = resxSet.GetString("New");
                btnDeleteDoc.Text = resxSet.GetString("Delete");
                btnNewMemo.Text = resxSet.GetString("New");
                btnDeleteMemo.Text = resxSet.GetString("Delete");
                btnEmail.Text = resxSet.GetString("Email");
                btnReport.Text = resxSet.GetString("Report");
                radRibbonMemo.Text = resxSet.GetString("Memo");
                radRibbonDocuments.Text = resxSet.GetString("Document");
                ribbonTab1.Text = resxSet.GetString("HOME");
                radRibbonTask.Text = resxSet.GetString("Task");
                btnDelContact.Text = resxSet.GetString("Delete");
                btnDelTask.Text = resxSet.GetString("Delete");
                btnNewContact.Text = resxSet.GetString("New");
                btnNewTask.Text = resxSet.GetString("New");
                btnNewMeeting.Text = resxSet.GetString("New");
                radRibbonMeeting.Text = resxSet.GetString("Meetings");
                tabMeetings.Text = resxSet.GetString("Meetings");
                tabContacts.Text = resxSet.GetString("Contacts");
                tabTasks.Text = resxSet.GetString("Tasks");

                radMenuItemSaveMemoLayout.Text = resxSet.GetString("Save Layout");
                radMenuItemSaveMeetingsLayout.Text = resxSet.GetString("Save Layout");
                radMenuItemSaveTasksLayout.Text = resxSet.GetString("Save Layout");
                radMenuItemSaveContactsLayout.Text = resxSet.GetString("Save Layout");
                radMenuItemSavelayoutDocuments.Text = resxSet.GetString("Save Layout");
                radMenuItemSaveArrangementLayout.Text = resxSet.GetString("Save Layout");

                //===
                chkCPerson.Text = resxSet.GetString("Contact person");
                chkisPaperByMail.Text = resxSet.GetString("Papers by post");
                lblCPfunct.Text = resxSet.GetString("Function");
                lblLives.Text = resxSet.GetString("Live at");
                lblCpCient.Text = resxSet.GetString("Client");
                //contactsStripMenu.Text = resxSet.GetString("Save Layout");

                if (resxSet.GetString(lblArrangement.Text) != null)
                    lblArrangement.Text = resxSet.GetString(lblArrangement.Text);

                // volonteer similarities
                if (resxSet.GetString(lblVolSim_effectivedate.Text) != null)
                    lblVolSim_effectivedate.Text = resxSet.GetString(lblVolSim_effectivedate.Text);

                if (resxSet.GetString(lblVolSim_expirationdate.Text) != null)
                    lblVolSim_expirationdate.Text = resxSet.GetString(lblVolSim_expirationdate.Text);

                if (resxSet.GetString(lblVolSimVOG_expired.Text) != null)
                    lblVolSimVOG_expired.Text = resxSet.GetString(lblVolSimVOG_expired.Text);

                if (resxSet.GetString(lblVolSimCOK_expired.Text) != null)
                    lblVolSimCOK_expired.Text = resxSet.GetString(lblVolSimCOK_expired.Text);

                if (resxSet.GetString(lblVolSimVOK_expired.Text) != null)
                    lblVolSimVOK_expired.Text = resxSet.GetString(lblVolSimVOK_expired.Text);

                if (resxSet.GetString(radioVolSimVOG_submited.Text) != null)
                    radioVolSimVOG_submited.Text = resxSet.GetString(radioVolSimVOG_submited.Text);
                if (resxSet.GetString(radioVolSimVOG_final.Text) != null)
                    radioVolSimVOG_final.Text = resxSet.GetString(radioVolSimVOG_final.Text);

                if (resxSet.GetString(radioVolSimCOK_submited.Text) != null)
                    radioVolSimCOK_submited.Text = resxSet.GetString(radioVolSimCOK_submited.Text);
                if (resxSet.GetString(radioVolSimCOK_final.Text) != null)
                    radioVolSimCOK_final.Text = resxSet.GetString(radioVolSimCOK_final.Text);

                if (resxSet.GetString(radioVolSimVOK_submited.Text) != null)
                    radioVolSimVOK_submited.Text = resxSet.GetString(radioVolSimVOK_submited.Text);
                if (resxSet.GetString(radioVolSimVOK_final.Text) != null)
                    radioVolSimVOK_final.Text = resxSet.GetString(radioVolSimVOK_final.Text);

                if (resxSet.GetString(lblDiplomaOrCertificate.Text) != null)
                    lblDiplomaOrCertificate.Text = resxSet.GetString(lblDiplomaOrCertificate.Text);
                if (resxSet.GetString(lblTraining.Text) != null)
                    lblTraining.Text = resxSet.GetString(lblTraining.Text);
                if (resxSet.GetString(lblExpirationDate.Text) != null)
                    lblExpirationDate.Text = resxSet.GetString(lblExpirationDate.Text);

                if (resxSet.GetString(lblDateArchieved.Text) != null)
                    lblDateArchieved.Text = resxSet.GetString(lblDateArchieved.Text);
                if (resxSet.GetString(lblScheaduledDate.Text) != null)
                    lblScheaduledDate.Text = resxSet.GetString(lblScheaduledDate.Text);
                if (resxSet.GetString(btnAddFeatures.Text) != null)
                    btnAddFeatures.Text = resxSet.GetString(btnAddFeatures.Text);
                if (resxSet.GetString(tabSimilarity.Text) != null)
                    tabSimilarity.Text = resxSet.GetString(tabSimilarity.Text);

                if (resxSet.GetString(lblReasonIN.Text) != null)
                    lblReasonIN.Text = resxSet.GetString(lblReasonIN.Text);
                if (resxSet.GetString(lblProfession.Text) != null)
                    lblProfession.Text = resxSet.GetString(lblProfession.Text);
                if (resxSet.GetString(lblTravelInsurance.Text) != null)
                    lblTravelInsurance.Text = resxSet.GetString(lblTravelInsurance.Text);
                if (resxSet.GetString(lblPolisNumber.Text) != null)
                    lblPolisNumber.Text = resxSet.GetString(lblPolisNumber.Text);
                if (resxSet.GetString(lblAlarmNumber.Text) != null)
                    lblAlarmNumber.Text = resxSet.GetString(lblAlarmNumber.Text);

            }
        }

        private void rchk_CheckStateChanged(object sender, EventArgs e)
        {
            RadCheckBox radchk = (RadCheckBox)sender;
            for (int i = 0; i < pagePerson.Pages.Count; i++)
            {
                if (pagePerson.Pages[i].Text.TrimEnd() == radchk.Text.TrimEnd())
                {
                    if (radchk.CheckState == CheckState.Checked)
                        pagePerson.Pages[i].Item.Visibility = ElementVisibility.Visible;
                    else
                        pagePerson.Pages[i].Item.Visibility = ElementVisibility.Collapsed;
                    break;
                }
            }
            ////Neta added because Traveler and filter 
            if (radchk.Name == "chkFilter4")
                if (radchk.CheckState == CheckState.Checked)
                {

                    RadCheckBox rch = (RadCheckBox)Controls.Find("chkFilter3", true)[0];
                    if (rch.CheckState == CheckState.Checked)
                        rch.CheckState = CheckState.Unchecked;
                    ddlReasonIn.DataSource = gm12;
                    ddlReasonOut.DataSource = gm13;
                    indTravVol = 1;
                }

            if (radchk.Name == "chkFilter3")
                if (radchk.CheckState == CheckState.Checked)
                {

                    RadCheckBox rch = (RadCheckBox)Controls.Find("chkFilter4", true)[0];
                    if (rch.CheckState == CheckState.Checked)
                        rch.CheckState = CheckState.Unchecked;
                    ddlReasonIn.DataSource = gm10;
                    ddlReasonOut.DataSource = gm11;
                    indTravVol = 0;
                }
        }

        private void fillPersonData(bool showEmergencyAddress)
        {

            if (txtFirstName.Text != null)
            {
                txtFirstName.Text = Person.firstname.ToString();
            }
            txtMidName.Text = Person.midname.ToString();
            txtLastName.Text = Person.lastname.ToString();
            txtinitialsContPers.Text = Person.initialsContPers.ToString();
            //txtmaidenname.Text = Person.maidenname.ToString();
            //dtbirthdate.Value = Person.birthdate;
            if (Person.birthdate != null)
            {
                dtbirthdate.Value = (DateTime)Person.birthdate;
                dtbirthdate.Checked = true;
                
            }
            else
            {
                dtbirthdate.Value = DateTime.Now;
                dtbirthdate.Checked = false;
            }

            txtTravelInsurance.Text = Person.travelInsurance;
            txtPolisNumber.Text = Person.polisNumber;
            txtAlarmNumber.Text = Person.alarmNumber;

            if (Person.identBSN != null)
                txtIdentBSN.Text = Person.identBSN.ToString();


            PersonBUS bus = new PersonBUS();
            object objImage = bus.GetPersonImage(Person.idContPers);
            string strImage = "";
            if (objImage != null && objImage != DBNull.Value)
                strImage = (string)objImage;
            if (strImage != "")
            {
                BIS.Core.ImageDB im = new BIS.Core.ImageDB();
                picUser.BackgroundImage = null;
                picUser.Image = im.BytesToImage(Convert.FromBase64String(strImage));
            }

            //txtGender.Text = Person.nameGender;
            if (Person.isMaried == true)
                chkIsMaried.Checked = true;

            if (Person.isDied == true)
                chkDied.Checked = true;
            dtDied.Value = Person.dtOfDeath;

            if (Person.isNeedProspect == true)
                chkSendProspect.Checked = true;
            if (Person.isPayInvoice == true)
                chkPayInvoice.Checked = true;
            if (Person.isActive == true)
                chkActive.Checked = true;
            if (Person.isSharePicture == true)
                chkPicturePermission.Checked = true;
            //==
            if (Person.isPaperByMail == true)
                chkisPaperByMail.Checked = true;
            if (Person.isContactPerson == true)
            {
                chkCPerson.Checked = true;
                tabCperson.Item.Visibility = ElementVisibility.Visible;
            }

            //if (Person.idClient != null && Person.idClient != 0)
            // {
            //        ClientBUS wbus = new ClientBUS();
            //        ClientModel wmod = new ClientModel();
            //        wmod = wbus.GetClient(Person.idClient);
            //        if (wmod != null)
            //        {
            //            txtClient.Text = wmod.nameClient;
            //            txtClientR.Text = wmod.nameClient;
            //            xClientName = wmod.nameClient;
            //            xidClient = wmod.idClient;
            //        }

            txtClient.Text = "";
            //}

            //if (Person.livesIn != null)
            //    ddlLives.SelectedItem =
            //      ddlStatus.SelectedItem = ddlStatus.Items[status.FindIndex(item => item.idDocumentStatus == model.idDocumentStatus)];
            if (persPassport != null)
            {
                txtPassName.Text = persPassport.namePassport.ToString();
                txtPassNumber.Text = persPassport.numberPassport.ToString();
                txtBirthPlace.Text = persPassport.birthPlacePassport.ToString();
                txtBirthPlace.Text = persPassport.birthPlacePassport.ToString();
                txtPassIssuePlace.Text = persPassport.issuePlacePassport.ToString();

                if (persPassport.dtPassportIssued != null)
                {
                    dtIssueDate.Value = (DateTime)persPassport.dtPassportIssued;
                    dtIssueDate.Checked = true;
                }
                else
                {
                    dtIssueDate.Value = DateTime.Now;
                    dtIssueDate.Checked = false;
                }

                if (persPassport.dtPassportValid != null)
                {
                    dtValidTo.Value = (DateTime)persPassport.dtPassportValid;
                    dtValidTo.Checked = true;
                }
                else
                {
                    dtValidTo.Value = DateTime.Now;
                    dtValidTo.Checked = false;
                }
                txtPassLastName.Text = persPassport.lastNamePassport.ToString();

            }

            PersonLabel = new List<LabelForPerson>();
            PersonBUS pb = new PersonBUS();
            PersonLabel = pb.GetLabelsPerson(Person.idContPers);

            for (int i = 0; i < PersonLabel.Count; i++)
            {
                //RadCheckBox chk = (RadCheckBox)tabRelation.Controls.Find("chkIsMaried", true)[0];
                RadCheckBox chk = (RadCheckBox)tabRelation.Controls.Find("chkLabel" + PersonLabel[i].idLabel.ToString(), true)[0];
                //CheckBox chk = (CheckBox)  this.Controls.fFindControl(this, "btn3");
                chk.CheckState = CheckState.Checked;
            }

            PersonFilter = new List<FilterForPerson>();
            pb = new PersonBUS();
            PersonFilter = pb.GetFiltersPerson(Person.idContPers);

            for (int i = 0; i < PersonFilter.Count; i++)
            {
                if (tabRelation.Controls.Find("chkFilter" + PersonFilter[i].idFilter.ToString(), true) != null)
                    if (tabRelation.Controls.Find("chkFilter" + PersonFilter[i].idFilter.ToString(), true).Length > 0)
                    {
                        RadCheckBox chk = (RadCheckBox)tabRelation.Controls.Find("chkFilter" + PersonFilter[i].idFilter.ToString(), true)[0];
                        chk.CheckState = CheckState.Checked;
                    }
            }

            //load address

            PersonAddress = new List<PersonAddressModel>();
            PersonAddressBUS pab = new PersonAddressBUS();
            PersonAddress = pab.GetPersonAddresses(Person.idContPers);

            if (PersonAddress != null)
            {
                for (int i = 0; i < PersonAddress.Count; i++)
                {
                    if (PersonAddress[i].idAddressType == 1)
                    {
                        panelAddress.Controls.Find("txt_adr_street", true)[0].Text = PersonAddress[i].street;
                        panelAddress.Controls.Find("txt_adr_city", true)[0].Text = PersonAddress[i].city;
                        panelAddress.Controls.Find("txt_adr_houseno", true)[0].Text = PersonAddress[i].housenr;
                        panelAddress.Controls.Find("txt_adr_zip", true)[0].Text = PersonAddress[i].postalCode;
                        panelAddress.Controls.Find("txt_adr_ext", true)[0].Text = PersonAddress[i].extension;
                        if (PersonAddress[i].isInternational == true)
                        {
                            RadRadioButton rchk = (RadRadioButton)panelAddress.Controls.Find("rad_adr_inter", true)[0];
                            rchk.CheckState = CheckState.Checked;
                            RadRadioButton rchkNL = (RadRadioButton)panelAddress.Controls.Find("rad_adr_nl", true)[0];
                            rchkNL.CheckState = CheckState.Unchecked;
                            panelAddress.Controls.Find("btn_adr_get", true)[0].Visible = false;
                            panelAddress.Controls.Find("lbl_adr_country", true)[0].Visible = true;
                            panelAddress.Controls.Find("txt_adr_country", true)[0].Visible = true;
                            panelAddress.Controls.Find("txt_adr_country", true)[0].Text = PersonAddress[i].country;
                        }

                    }
                    else if (PersonAddress[i].idAddressType == 2)
                    {
                        panelAddress.Controls.Find("txt_badr_street", true)[0].Text = PersonAddress[i].street;
                        panelAddress.Controls.Find("txt_badr_city", true)[0].Text = PersonAddress[i].city;
                        panelAddress.Controls.Find("txt_badr_houseno", true)[0].Text = PersonAddress[i].housenr;
                        panelAddress.Controls.Find("txt_badr_zip", true)[0].Text = PersonAddress[i].postalCode;
                        panelAddress.Controls.Find("txt_badr_ext", true)[0].Text = PersonAddress[i].extension;
                        if (PersonAddress[i].isInternational == true)
                        {
                            RadRadioButton rchk = (RadRadioButton)panelAddress.Controls.Find("rad_badr_inter", true)[0];
                            rchk.CheckState = CheckState.Checked;
                            RadRadioButton rchkNL = (RadRadioButton)panelAddress.Controls.Find("rad_badr_nl", true)[0];
                            rchkNL.CheckState = CheckState.Unchecked;
                            panelAddress.Controls.Find("btn_badr_get", true)[0].Visible = false;
                            panelAddress.Controls.Find("lbl_badr_country", true)[0].Visible = true;
                            panelAddress.Controls.Find("txt_badr_country", true)[0].Visible = true;
                            panelAddress.Controls.Find("txt_badr_country", true)[0].Text = PersonAddress[i].country;
                        }

                    }
                    else if (PersonAddress[i].idAddressType == 3 && showEmergencyAddress == true)
                    {
                        panelAddress.Controls.Find("txt_em_street", true)[0].Text = PersonAddress[i].street;
                        panelAddress.Controls.Find("txt_em_city", true)[0].Text = PersonAddress[i].city;
                        panelAddress.Controls.Find("txt_em_houseno", true)[0].Text = PersonAddress[i].housenr;
                        panelAddress.Controls.Find("txt_em_zip", true)[0].Text = PersonAddress[i].postalCode;
                        panelAddress.Controls.Find("txt_em_ext", true)[0].Text = PersonAddress[i].extension;
                        if (PersonAddress[i].isInternational == true)
                        {
                            RadRadioButton rchk = (RadRadioButton)panelAddress.Controls.Find("rad_em_inter", true)[0];
                            rchk.CheckState = CheckState.Checked;
                            RadRadioButton rchkNL = (RadRadioButton)panelAddress.Controls.Find("rad_em_nl", true)[0];
                            rchkNL.CheckState = CheckState.Unchecked;
                            panelAddress.Controls.Find("btn_em_get", true)[0].Visible = false;
                            panelAddress.Controls.Find("lbl_em_country", true)[0].Visible = true;
                            panelAddress.Controls.Find("txt_em_country", true)[0].Visible = true;
                            panelAddress.Controls.Find("txt_em_country", true)[0].Text = PersonAddress[i].country;

                        }

                    }
                    else if (PersonAddress[i].idAddressType == 4 && chkisPaperByMail.Checked == true)
                    {
                        panelAddress.Controls.Find("txt_post_street", true)[0].Text = PersonAddress[i].street;
                        panelAddress.Controls.Find("txt_post_city", true)[0].Text = PersonAddress[i].city;
                        panelAddress.Controls.Find("txt_post_houseno", true)[0].Text = PersonAddress[i].housenr;
                        panelAddress.Controls.Find("txt_post_zip", true)[0].Text = PersonAddress[i].postalCode;
                        panelAddress.Controls.Find("txt_post_ext", true)[0].Text = PersonAddress[i].extension;
                        if (PersonAddress[i].isInternational == true)
                        {
                            RadRadioButton rchk = (RadRadioButton)panelAddress.Controls.Find("rad_post_inter", true)[0];
                            rchk.CheckState = CheckState.Checked;
                            RadRadioButton rchkNL = (RadRadioButton)panelAddress.Controls.Find("rad_post_nl", true)[0];
                            rchkNL.CheckState = CheckState.Unchecked;
                            panelAddress.Controls.Find("btn_post_get", true)[0].Visible = false;
                            panelAddress.Controls.Find("lbl_post_country", true)[0].Visible = true;
                            panelAddress.Controls.Find("txt_post_country", true)[0].Visible = true;
                            panelAddress.Controls.Find("txt_post_country", true)[0].Text = PersonAddress[i].country;
                        }

                    }
                }
            }


            PersonFilter = pb.GetFiltersPerson(Person.idContPers);

            for (int i = 0; i < PersonFilter.Count; i++)
            {
                if (tabRelation.Controls.Find("chkFilter" + PersonFilter[i].idFilter.ToString(), true) != null)
                    if (tabRelation.Controls.Find("chkFilter" + PersonFilter[i].idFilter.ToString(), true).Length > 0)
                    {
                        RadCheckBox chk = (RadCheckBox)tabRelation.Controls.Find("chkFilter" + PersonFilter[i].idFilter.ToString(), true)[0];
                        chk.CheckState = CheckState.Checked;
                    }
            }

            if (Person.idContPersBookingTo != 0)
            {
                PersonBUS pbb = new PersonBUS();
                PersonModel pm = new PersonModel();
                pm = pbb.GetPerson(Person.idContPersBookingTo);
                if (pm != null)
                    txtBookingTo.Text = pm.firstname + " " + pm.midname + " " + pm.lastname;
            }

            // ---------- ubacen prikaz tab account----------------------------------------------------------

            // Load IBANS for Person
            AccIbanBUS ibanBUS = new AccIbanBUS();
            List<AccIbanModel> ibanTmpList = new List<AccIbanModel>();

            ibanTmpList = ibanBUS.GetIBANForPerson(Person.idContPers);
            if (ibanTmpList != null)
                ibanLista = new BindingList<AccIbanModel>(ibanTmpList);
            else
                ibanLista = new BindingList<AccIbanModel>();

            gridIbans.DataSource = ibanLista;
            gridIbans.Columns["Id"].IsVisible = false;
            gridIbans.Columns["accNumber"].IsVisible = false;
            gridIbans.Columns["idClient"].IsVisible = false;
            gridIbans.Columns["idContPers"].IsVisible = false;
            gridIbans.Columns["ibanNumber"].Width = 200;


            AccDebCreBUS dbbus = new AccDebCreBUS();
            if (Person.idContPers != 0)
            {
                debCre = dbbus.GetPersonDebCre(Person.idContPers);
                if (debCre != null)
                {
                    if (debCre.accNumber != null)
                        txtPersAccNo.Text = debCre.accNumber;
                    if (debCre.isDebitor == true)
                        chkDebitor.Checked = true;
                    if (debCre.isCreditor == true)
                        chkCreditor.Checked = true;
                    if (debCre.debAccount != "" && debCre.debAccount != null)
                    {
                        LedgerAccountBUS dbus = new LedgerAccountBUS(Login._bookyear);
                        LedgerAccountModel md = new LedgerAccountModel();
                        md = dbus.GetAccount(debCre.debAccount, Login._bookyear);
                        if (md != null)
                            txtDebAcc.Text = md.numberLedgerAccount + "  " + md.descLedgerAccount;
                    }
                    if (debCre.creditAccount != "" && debCre.creditAccount != null)
                    {
                        LedgerAccountBUS dbus1 = new LedgerAccountBUS(Login._bookyear);
                        LedgerAccountModel md1 = new LedgerAccountModel();
                        md1 = dbus1.GetAccount(debCre.creditAccount, Login._bookyear);
                        if (md1 != null)
                            txtCreAcc.Text = md1.numberLedgerAccount + "  " + md1.descLedgerAccount;
                    }
                    if (debCre.payCondition != null && debCre.payCondition != 0)
                    {
                        AccPaymentBUS apb = new AccPaymentBUS();
                        AccPaymentModel apm = new AccPaymentModel();
                        apm = apb.GetPaymentByID(debCre.payCondition);
                        if (apm != null)
                        {
                            txtPayCon.Text = apm.description;
                        }
                    }
                }
                else
                {
                    debCre = new AccDebCreModel();
                }
            }
            //--------------------------------------------------------------------------------------------------


            //Load Emails and Tels
            PersonTelBUS persTelBUS = new PersonTelBUS();
            PersonEmailBUS persEmailBUS = new PersonEmailBUS();
            persEmail = persEmailBUS.GetPersonEmails(Person.idContPers);
            if (persEmail != null)
            {
                foreach (PersonEmailModel m in persEmail)
                {
                    persEmailFirst.Add(m.ReturnCopy());
                }
            }

            persTel = persTelBUS.GetPersonTels(Person.idContPers);
            if (persTel != null)
            {
                foreach (PersonTelModel m in persTel)
                {
                    persTelFirst.Add(m.ReturnCopy());
                }
            }


            rgvTel.DataSource = persTel;
            // saki
            this.rgvTel.Columns["idContPers"].IsVisible = false;
            //
            rgvTel.Show();
            rgvEmail.DataSource = persEmail;
            // saki
            this.rgvEmail.Columns["idContPers"].IsVisible = false;
            //
            rgvEmail.Show();

            pagePerson.SelectedPage = tabRelation;

            //PersonModel objPerson = new PersonBUS().GetContactPerson(Person.idContPers);
            //Bind(objPerson);
        }

        public bool ValidateElfProef(int value)
        {
            int divisor = 1000000000;
            int total = 0;
            int result = value;
            for (int i = 9; i > 1; i--)
                total += i * Math.DivRem(result, divisor /= 10, out result);

            int rest;
            Math.DivRem(total, 11, out rest);

            return result == rest;
        }

        public bool ValidateElfProef(string value)
        {
            return ValidateElfProef(Convert.ToInt32(value));
        }

        private bool ValidatePerson()
        {
            PersonBUS pb = new PersonBUS();
            //string initials.Text; 
            //string midname 
            //string lastname 
            if (Person.idContPers == -1 || Person.idContPers == 0)
            {
                int result = pb.GetIfExistValidatePerson(txtinitialsContPers.Text, txtMidName.Text, txtLastName.Text, dtbirthdate.Value);

                if (result > 0)
                {
                    translateRadMessageBox msgbox = new translateRadMessageBox();
                    msgbox.translateAllMessageBox("This person already exist.");

                    return false;

                }
            }


            //validate firstname and lastname
            if (txtFirstName.Text.Trim() == "" || txtLastName.Text.Trim() == "")
            {
                translateRadMessageBox msgbox = new translateRadMessageBox();
                msgbox.translateAllMessageBox("First name and last name must be entered.");
                return false;
            }

            //Mitar i Aleksa
            if (txtinitialsContPers.Text.Trim() == "")
            {
                translateRadMessageBox msgbox = new translateRadMessageBox();
                msgbox.translateAllMessageBox("Initials must be entered.");
                return false;
            }

            if (ddlTitle.SelectedIndex == -1)
            {
                translateRadMessageBox msgbox = new translateRadMessageBox();
                msgbox.translateAllMessageBox("Title must be seleted.");
                return false;
            }


            //if (panelAddress.Controls.Find("txt_adr_street", true)[0].Text == "")
            //{
            //    translateRadMessageBox msgbox = new translateRadMessageBox();
            //    msgbox.translateAllMessageBox("Street must be entered.");
            //    return false;
            //}

            //if (panelAddress.Controls.Find("txt_adr_houseno", true)[0].Text == "")
            //{
            //    translateRadMessageBox msgbox = new translateRadMessageBox();
            //    msgbox.translateAllMessageBox("House no must be entered.");
            //    return false;
            //}
            //if (panelAddress.Controls.Find("txt_adr_zip", true)[0].Text == "")
            //{
            //    translateRadMessageBox msgbox = new translateRadMessageBox();
            //    msgbox.translateAllMessageBox("Zip must be entered.");
            //    return false;
            //}
            //if (panelAddress.Controls.Find("txt_adr_city", true)[0].Text == "")
            //{
            //    translateRadMessageBox msgbox = new translateRadMessageBox();
            //    msgbox.translateAllMessageBox("City must be entered.");
            //    return false;
            //}

            //Mitar i Aleksa 

            //validate Ident BSN first
            if (txtIdentBSN.Text.Trim() != "")
            {
                bool isNumber = Regex.IsMatch(txtIdentBSN.Text.Trim(), @"^\d+$");
                if (isNumber == false)
                {
                    translateRadMessageBox msgbox = new translateRadMessageBox();
                    msgbox.translateAllMessageBox("Ident BSN must be number.");
                    return false;
                }
                else
                {
                    bool isValid = ValidateElfProef(txtIdentBSN.Text.Trim());

                    if (isValid == false)
                    {
                        translateRadMessageBox msgbox = new translateRadMessageBox();
                        msgbox.translateAllMessageBox("Ident BSN is wrong.");
                        return false;
                    }
                    else
                    {
                        if (txtIdentBSN.Text.Trim().Length == 8)
                        {
                            txtIdentBSN.Text = txtIdentBSN.Text.Trim().Insert(0, "0");
                        }
                    }
                }
            }

            return true;
        }

        private void savePerson()
        {
            Person.firstname = txtFirstName.Text;
            Person.lastname = txtLastName.Text;
            Person.midname = txtMidName.Text;
            //Person.maidenname = txtmaidenname.Text;
            //Person.birthdate = dtbirthdate.Value;
            if (dtbirthdate.Checked == true)
                Person.birthdate = dtbirthdate.Value;
            else
                Person.birthdate = null;


            Person.initialsContPers = txtinitialsContPers.Text;
            Person.idTitle = Convert.ToInt32(ddlTitle.SelectedValue);
            Person.idGender = Convert.ToInt32(ddlGender.SelectedValue);
            Person.livesIn = Convert.ToInt32(ddlLives.SelectedValue);
            Person.idCpFunction = Convert.ToInt32(ddlCpFunction.SelectedValue);
            Person.nameGender = ddlGender.SelectedText;
            Person.identBSN = txtIdentBSN.Text;

            Person.travelInsurance = txtTravelInsurance.Text;
            Person.polisNumber = txtPolisNumber.Text;
            Person.alarmNumber = txtAlarmNumber.Text;


            if (xidClient != null)
                Person.idClient = xidClient;
            if (chkIsMaried.Checked == true)
                Person.isMaried = true;
            else
                Person.isMaried = false;
            if (chkDied.Checked == true)
            {
                Person.isDied = true;
                Person.dtOfDeath = Convert.ToDateTime(dtDied.Text);
            }
            else
                Person.isDied = false;
            if (chkSendProspect.Checked == true)
                Person.isNeedProspect = true;
            else
                Person.isNeedProspect = false;
            if (picUser.Image != GUI.Properties.Resources.DefaultPerson && picUser.Image != null)
            {
                BIS.Core.ImageDB im = new BIS.Core.ImageDB();
                Person.imageContPers = Convert.ToBase64String(im.ImageToBytes(picUser.Image));
            }
            Person.idUserModified = Login._user.idUser;
            Person.dtModified = DateTime.Now;

            if (chkPicturePermission.Checked == true)
                Person.isSharePicture = true;
            else
                Person.isSharePicture = false;
            if (chkPayInvoice.Checked == true)
                Person.isPayInvoice = true;
            else
                Person.isPayInvoice = false;
            if (chkActive.Checked == true)
                Person.isActive = true;
            else
                Person.isActive = false;
            if (chkCPerson.Checked == true)
                Person.isContactPerson = true;
            else
                Person.isContactPerson = false;
            if (chkisPaperByMail.Checked == true)
                Person.isPaperByMail = true;
            else
                Person.isPaperByMail = false;

            if (ddlReasonIn.SelectedValue != null && ddlReasonIn.SelectedValue != "")
                Person.idReasonIn = Convert.ToInt32(ddlReasonIn.SelectedValue);
            else
                Person.idReasonIn = 0;
            if (ddlReasonOut.SelectedValue != null && ddlReasonOut.SelectedValue != "")
                Person.idReasonOut = Convert.ToInt32(ddlReasonOut.SelectedValue);
            else
                Person.idReasonOut = 0;
            Person.volProfession = txtProfession.Text;

        }

        private void saveAcc()
        {
            debCre.accNumber = txtPersAccNo.Text;
            if (txtPayCon.Text == "")
                debCre.payCondition = 0;

            if (chkDebitor.Checked == true)
            {
                debCre.isDebitor = true;
            }
            else
            {
                debCre.isDebitor = false;
            }
            if (chkCreditor.Checked == true)
            {
                debCre.isCreditor = true;
            }
            else
            {
                debCre.isCreditor = false;
            }
            isOkAcc = true;
            //}
        }

        private void saveAddress(bool saveEmergencyAddress)
        {
            PersonAddress = new List<PersonAddressModel>();
            PersonAddressModel pm = new PersonAddressModel();
            pm.idContPers = Person.idContPers;
            pm.idAddressType = 1;
            pm.street = panelAddress.Controls.Find("txt_adr_street", true)[0].Text;
            pm.city = panelAddress.Controls.Find("txt_adr_city", true)[0].Text;
            pm.housenr = panelAddress.Controls.Find("txt_adr_houseno", true)[0].Text;
            pm.postalCode = panelAddress.Controls.Find("txt_adr_zip", true)[0].Text;
            pm.extension = panelAddress.Controls.Find("txt_adr_ext", true)[0].Text;
            RadRadioButton rchkNL = (RadRadioButton)panelAddress.Controls.Find("rad_adr_nl", true)[0];
            if (rchkNL.IsChecked == true)
                pm.isInternational = false;
            else
                pm.isInternational = true;
            pm.country = panelAddress.Controls.Find("txt_adr_country", true)[0].Text;
            PersonAddress.Add(pm);
            pm = new PersonAddressModel();
            pm.idContPers = Person.idContPers;
            pm.idAddressType = 2;
            pm.street = panelAddress.Controls.Find("txt_badr_street", true)[0].Text;
            pm.city = panelAddress.Controls.Find("txt_badr_city", true)[0].Text;
            pm.housenr = panelAddress.Controls.Find("txt_badr_houseno", true)[0].Text;
            pm.postalCode = panelAddress.Controls.Find("txt_badr_zip", true)[0].Text;
            pm.extension = panelAddress.Controls.Find("txt_badr_ext", true)[0].Text;
            RadRadioButton rchkNL2 = (RadRadioButton)panelAddress.Controls.Find("rad_badr_nl", true)[0];
            if (rchkNL2.IsChecked == true)
                pm.isInternational = false;
            else
                pm.isInternational = true;
            pm.country = panelAddress.Controls.Find("txt_badr_country", true)[0].Text;
            PersonAddress.Add(pm);

            if (saveEmergencyAddress == true)
            {
                pm = new PersonAddressModel();
                pm.idContPers = Person.idContPers;
                pm.idAddressType = 3;
                pm.street = panelAddress.Controls.Find("txt_em_street", true)[0].Text;
                pm.city = panelAddress.Controls.Find("txt_em_city", true)[0].Text;
                pm.housenr = panelAddress.Controls.Find("txt_em_houseno", true)[0].Text;
                pm.postalCode = panelAddress.Controls.Find("txt_em_zip", true)[0].Text;
                pm.extension = panelAddress.Controls.Find("txt_em_ext", true)[0].Text;
                RadRadioButton rchkNL3 = (RadRadioButton)panelAddress.Controls.Find("rad_em_nl", true)[0];
                if (rchkNL3.IsChecked == true)
                    pm.isInternational = false;
                else
                    pm.isInternational = true;
                pm.country = panelAddress.Controls.Find("txt_em_country", true)[0].Text;
                PersonAddress.Add(pm);
            }
            if (chkisPaperByMail.Checked == true)
            {
                pm = new PersonAddressModel();
                pm.idContPers = Person.idContPers;
                pm.idAddressType = 4;
                pm.street = panelAddress.Controls.Find("txt_post_street", true)[0].Text;
                pm.city = panelAddress.Controls.Find("txt_post_city", true)[0].Text;
                pm.housenr = panelAddress.Controls.Find("txt_post_houseno", true)[0].Text;
                pm.postalCode = panelAddress.Controls.Find("txt_post_zip", true)[0].Text;
                pm.extension = panelAddress.Controls.Find("txt_post_ext", true)[0].Text;
                RadRadioButton rchkNL4 = (RadRadioButton)panelAddress.Controls.Find("rad_post_nl", true)[0];
                if (rchkNL4.IsChecked == true)
                    pm.isInternational = false;
                else
                    pm.isInternational = true;
                pm.country = panelAddress.Controls.Find("txt_post_country", true)[0].Text;
                PersonAddress.Add(pm);
            }
        }

        private void savePassport()
        {
            persPassport = new PersonPassportModel();
            persPassport.idContPers = Person.idContPers;
            persPassport.idCountry = Convert.ToInt32(ddlNacionality.SelectedValue);
            persPassport.issuePlacePassport = txtPassIssuePlace.Text;

            if (dtIssueDate.Checked == true)
                persPassport.dtPassportIssued = dtIssueDate.Value;
            else
                persPassport.dtPassportIssued = null;

            if (dtValidTo.Checked == true)
                persPassport.dtPassportValid = dtValidTo.Value;
            else
                persPassport.dtPassportValid = null;

            persPassport.birthPlacePassport = txtBirthPlace.Text;
            persPassport.namePassport = txtPassName.Text;
            persPassport.numberPassport = txtPassNumber.Text;
            persPassport.lastNamePassport = txtPassLastName.Text;
        }

        private void saveEmail()
        {
            persEmail = new List<PersonEmailModel>();
            for (int i = 0; i < rgvEmail.Rows.Count; i++)
            {
                PersonEmailModel pem = new PersonEmailModel();
                pem.idEmail = Convert.ToInt32(rgvEmail.Rows[i].Cells["idEmail"].Value.ToString());
                if (rgvEmail.Rows[i].Cells["email"].Value != null)
                {
                    pem.email = rgvEmail.Rows[i].Cells["email"].Value.ToString();
                }
                if (rgvEmail.Rows[i].Cells["idContPers"].Value != null)
                {
                    pem.idContPers = Person.idContPers;
                }
                else
                    RadMessageBox.Show("You have to insert ID person");
                if (rgvEmail.Rows[i].Cells["idEmailType"].Value != null)
                {
                    pem.idEmailType = Convert.ToInt32(rgvEmail.Rows[i].Cells["idEmailType"].Value.ToString());
                }
                pem.isCommunication = Convert.ToBoolean(rgvEmail.Rows[i].Cells["isCommunication"].Value.ToString());
                pem.isInvoicing = Convert.ToBoolean(rgvEmail.Rows[i].Cells["isInvoicing"].Value.ToString());
                pem.isProspect = Convert.ToBoolean(rgvEmail.Rows[i].Cells["isProspect"].Value.ToString());
                pem.isNewsletters = Convert.ToBoolean(rgvEmail.Rows[i].Cells["isNewsletters"].Value.ToString());
                pem.lastQuestionForm = Convert.ToBoolean(rgvEmail.Rows[i].Cells["lastQuestionForm"].Value.ToString());
                persEmail.Add(pem);

            }
        }

        private void saveTel()
        {
            persTel = new List<PersonTelModel>();
            for (int i = 0; i < rgvTel.Rows.Count; i++)
            {
                PersonTelModel ptm = new PersonTelModel();
                ptm.idTel = Convert.ToInt32(rgvTel.Rows[i].Cells["idTel"].Value.ToString());
                if (rgvTel.Rows[i].Cells["numberTel"].Value != null)
                {
                    ptm.numberTel = rgvTel.Rows[i].Cells["numberTel"].Value.ToString();
                }
                if (rgvTel.Rows[i].Cells["idContPers"].Value != null)
                {
                    ptm.idContPers = Person.idContPers;
                }
                else
                    RadMessageBox.Show("You have to insert ID person");
                if (rgvTel.Rows[i].Cells["idTelType"].Value != null)
                {
                    ptm.idTelType = Convert.ToInt32(rgvTel.Rows[i].Cells["idTelType"].Value.ToString());
                }
                if (rgvTel.Rows[i].Cells["descriptionTel"].Value != null)
                    ptm.descriptionTel = rgvTel.Rows[i].Cells["descriptionTel"].Value.ToString();
                ptm.isDefaultTel = Convert.ToBoolean(rgvTel.Rows[i].Cells["isDefaultTel"].Value.ToString());
                persTel.Add(ptm);
            }
        }

        private void saveFilters()
        {
            PersonFilter = new List<FilterForPerson>();
            foreach (Control c in panelFilters.Controls)
            {
                RadCheckBox rch = (RadCheckBox)c;
                if (rch.Checked == true)
                {
                    FilterForPerson fil = new FilterForPerson();
                    fil.idFilter = Login._personFilters.Find(item => item.nameFilter.TrimEnd() == rch.Text.TrimEnd()).idFilter;
                    fil.idContPers = Person.idContPers;
                    PersonFilter.Add(fil);
                }
            }
        }

        private void saveLabels()
        {
            PersonLabel = new List<LabelForPerson>();
            foreach (Control c in panelLabels.Controls)
            {
                RadCheckBox rch = (RadCheckBox)c;
                if (rch.Checked == true)
                {
                    LabelForPerson lab = new LabelForPerson();
                    lab.idLabel = Login._personLabels.Find(item => item.nameLabel.TrimEnd() == rch.Text.TrimEnd()).idLabel;
                    lab.idContPers = Person.idContPers;
                    PersonLabel.Add(lab);
                }
            }
        }

        private void saveArrangement()
        {
            PersonBUS pbus = new PersonBUS();
            for (int i = 0; i < newArrangement.Count; i++)
            {
                if (pbus.SaveArrangement(newArrangement[i].idArrangement, Person.idContPers, -1, -1, -1, this.Name, Login._user.idUser) == false)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Something went wrong with inserting arrangement!");
                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You inserted arrangement successfully!");
                }

            }

        }

        private void radPageView1_Click(object sender, EventArgs e)
        {
            rgvNote.AllowAutoSizeColumns = true;
        }

        private void pagePerson_SelectedPageChanged(object sender, EventArgs e)
        {
            RadPageView rpv = (RadPageView)sender;
            string sName = ((RadPageView)sender).SelectedPage.Name;
            if (sName != "tabVoluntary")
            {
                if (isVolChanged == true)
                {
                    if (Person.idContPers != 0)
                    {
                    }
                    else
                    {
                        bool validate = ValidatePerson();
                        if (validate == true)
                        {
                            savePerson();
                            insertData();

                        }
                    }
                    //isVolChanged = false;
                }
            }
            if (sName != "tabMedical")
            {
                if (isMedChanged == true)
                {
                    if (Person.idContPers != 0)
                    {
                        saveMedical();
                        MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
                        if (mvb.DeleteMedical(Person.idContPers, this.Name, Login._user.idUser) == true)
                        {
                            for (int j = 0; j < personMedical.Count; j++)
                            {
                                if (mvb.SaveMedical(personMedical[j], this.Name, Login._user.idUser) == false)
                                {
                                    RadMessageBox.Show("You have not succesufully inserted medical data. Please check!");
                                    break;
                                }

                            }
                        }
                        else
                        {
                            RadMessageBox.Show("You have not succesufully inserted medical data. Please check!");
                        }
                    }
                    else
                    {
                        bool validate = ValidatePerson();
                        if (validate == true)
                        {
                            savePerson();
                            insertData();
                        }
                    }
                    isMedChanged = false;
                }
            }
            switch (sName)
            {

                case "tabRelation":
                    radRibbonContact.Visibility = ElementVisibility.Visible;
                    radRibbonTask.Visibility = ElementVisibility.Visible;
                    btnDelContact.Visibility = ElementVisibility.Collapsed;
                    btnDelTask.Visibility = ElementVisibility.Collapsed;
                    //radRibbonContact.Visibility = ElementVisibility.Collapsed;
                    // radRibbonTask.Visibility = ElementVisibility.Collapsed;
                    btnDeleteDoc.Visibility = ElementVisibility.Collapsed;
                    btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
                    btnCancelTraveler.Visibility = ElementVisibility.Collapsed;
                    radRibbonBarGroupTraveler.Visibility = ElementVisibility.Collapsed;

                    break;

                case "tabMedical":
                    radRibbonContact.Visibility = ElementVisibility.Visible;
                    radRibbonTask.Visibility = ElementVisibility.Visible;
                    btnDelContact.Visibility = ElementVisibility.Collapsed;
                    btnDelTask.Visibility = ElementVisibility.Collapsed;
                    // radRibbonContact.Visibility = ElementVisibility.Collapsed;
                    //   radRibbonTask.Visibility = ElementVisibility.Collapsed;
                    btnDeleteDoc.Visibility = ElementVisibility.Collapsed;
                    btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
                    //cccNew.Visibility = ElementVisibility.Collapsed;
                    btnCancelTraveler.Visibility = ElementVisibility.Collapsed;
                    radRibbonBarGroupTraveler.Visibility = ElementVisibility.Collapsed;

                    if (Login._user.isDontSeeMedVol == false)
                    {
                        loadOnMedicalForm();
                    }
                    else
                    {
                        tabMedical.Controls.Clear();
                        RadLabel labelMessage = new RadLabel();
                        labelMessage.Text = "You dont have persmission for medical section.";

                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString(labelMessage.Text) != null)
                                labelMessage.Text = resxSet.GetString(labelMessage.Text);
                        }

                        labelMessage.ForeColor = Color.Black;
                        tabMedical.Controls.Add(labelMessage);
                    }
                    //isMedChanged = true;
                    break;
                case "tabVoluntary":
                    //radRibbonContact.Visibility = ElementVisibility.Collapsed;
                    //radRibbonTask.Visibility = ElementVisibility.Collapsed;
                    radRibbonContact.Visibility = ElementVisibility.Visible;
                    radRibbonTask.Visibility = ElementVisibility.Visible;
                    btnDelContact.Visibility = ElementVisibility.Collapsed;
                    btnDelTask.Visibility = ElementVisibility.Collapsed;
                    btnDeleteDoc.Visibility = ElementVisibility.Collapsed;
                    btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
                    btnCancelTraveler.Visibility = ElementVisibility.Collapsed;
                    radRibbonBarGroupTraveler.Visibility = ElementVisibility.Collapsed;

                    //cccNew.Visibility = ElementVisibility.Collapsed;

                    loadOnVoluntaryForm();
                    //isVolChanged = true;
                    break;
                case "tabDocuments":
                    radRibbonContact.Visibility = ElementVisibility.Visible;
                    radRibbonTask.Visibility = ElementVisibility.Visible;
                    btnDelContact.Visibility = ElementVisibility.Collapsed;
                    btnDelTask.Visibility = ElementVisibility.Collapsed;
                    //radRibbonContact.Visibility = ElementVisibility.Collapsed;
                    //radRibbonTask.Visibility = ElementVisibility.Collapsed;
                    btnDeleteDoc.Visibility = ElementVisibility.Visible;
                    btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
                    btnCancelTraveler.Visibility = ElementVisibility.Collapsed;

                    rgvDocuments.DataSource = new DocumentsBUS().GetPersonDoc(Person.idContPers, Login._user.lngUser);
                    // saki

                    if (rgvDocuments.DataSource == null)
                    {
                        List<DocumentsModel> docmdl = new List<DocumentsModel>();
                        rgvDocuments.DataSource = docmdl;
                    }
                    // rgvDocuments.Columns["dinoutdoc"].IsVisible = false;
                    rgvDocuments.Columns["idProject"].IsVisible = false;
                    rgvDocuments.Columns["idClient"].IsVisible = false;
                    rgvDocuments.Columns["idContPers"].IsVisible = false;
                    rgvDocuments.Columns["idEmployee"].IsVisible = false;
                    rgvDocuments.Columns["idResponsableEmployee"].IsVisible = false;
                    rgvDocuments.Columns["idDocumentStatus"].IsVisible = false;
                    // rgvDocuments.Columns["sttdesdoc"].IsVisible = false;
                    // rgvDocuments.Columns["archivedoc"].IsVisible = false;
                    // rgvDocuments.Columns["arhnamdoc"].IsVisible = false;
                    // rgvDocuments.Columns["idscl"].IsVisible = false;
                    rgvDocuments.Columns["dtModified"].IsVisible = false;
                    rgvDocuments.Columns["userModified"].IsVisible = false;
                    rgvDocuments.Columns["userCreated"].IsVisible = false;
                    //  rgvDocuments.Columns["isprivdoc"].IsVisible = false;
                    rgvDocuments.Columns["idLayout"].IsVisible = false;
                    radRibbonBarGroupTraveler.Visibility = ElementVisibility.Collapsed;

                    //saki end
                    rgvDocuments.Show();
                    break;
                case "tabMemo":
                    radRibbonContact.Visibility = ElementVisibility.Visible;
                    radRibbonTask.Visibility = ElementVisibility.Visible;
                    btnDelContact.Visibility = ElementVisibility.Collapsed;
                    btnDelTask.Visibility = ElementVisibility.Collapsed;
                    //radRibbonContact.Visibility = ElementVisibility.Collapsed;
                    //radRibbonTask.Visibility = ElementVisibility.Collapsed;
                    btnDeleteDoc.Visibility = ElementVisibility.Collapsed;
                    btnDeleteMemo.Visibility = ElementVisibility.Visible;
                    btnCancelTraveler.Visibility = ElementVisibility.Collapsed;
                    radRibbonBarGroupTraveler.Visibility = ElementVisibility.Collapsed;


                    break;
                case "tabAccount":
                    radRibbonContact.Visibility = ElementVisibility.Visible;
                    radRibbonTask.Visibility = ElementVisibility.Visible;
                    btnDelContact.Visibility = ElementVisibility.Collapsed;
                    btnDelTask.Visibility = ElementVisibility.Collapsed;
                    //radRibbonContact.Visibility = ElementVisibility.Collapsed;
                    //radRibbonTask.Visibility = ElementVisibility.Collapsed;
                    btnDeleteDoc.Visibility = ElementVisibility.Collapsed;
                    btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
                    btnCancelTraveler.Visibility = ElementVisibility.Collapsed;
                    radRibbonBarGroupTraveler.Visibility = ElementVisibility.Collapsed;

                    break;
                case "tabCommunication":
                    radRibbonContact.Visibility = ElementVisibility.Visible;
                    radRibbonTask.Visibility = ElementVisibility.Visible;
                    btnDelContact.Visibility = ElementVisibility.Collapsed;
                    btnDelTask.Visibility = ElementVisibility.Collapsed;
                    btnDeleteDoc.Visibility = ElementVisibility.Collapsed;
                    btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
                    btnCancelTraveler.Visibility = ElementVisibility.Collapsed;
                    radRibbonBarGroupTraveler.Visibility = ElementVisibility.Collapsed;

                    LoadMeetings();
                    radPageComm_SelectedPageChanged(sender, e);
                    break;

                case "tabArrangement":
                    radRibbonBarGroupTraveler.Visibility = ElementVisibility.Visible;
                    radRibbonContact.Visibility = ElementVisibility.Visible;
                    radRibbonTask.Visibility = ElementVisibility.Visible;
                    btnDelContact.Visibility = ElementVisibility.Collapsed;
                    btnDelTask.Visibility = ElementVisibility.Collapsed;
                    //radRibbonContact.Visibility = ElementVisibility.Collapsed;
                    //radRibbonTask.Visibility = ElementVisibility.Collapsed;
                    btnDeleteDoc.Visibility = ElementVisibility.Visible;
                    btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
                    PersonBUS pb = new PersonBUS();
                    btnCancelTraveler.Visibility = ElementVisibility.Visible;

                    // lista svih arrangement-a za personu
                    AllArrangement = new List<ArrangementAllModel>();

                    AllArrangement = pb.GetArrangementsForPerson(Person.idContPers);

                    rgvArrangment.DataSource = AllArrangement;


                    if (rgvArrangment.DataSource == null)
                    {
                        List<ArrangementAllModel> docmdl = new List<ArrangementAllModel>();
                        rgvArrangment.DataSource = docmdl;
                    }

                    //rgvArrangment.Columns["countryArrangement"].IsVisible = false;
                    //rgvArrangment.Columns["typeArrangement"].IsVisible = false;
                    //rgvArrangment.Columns["idStatus"].IsVisible = false;
                    //rgvArrangment.Columns["idArrangementBook"].IsVisible = false;
                    //rgvArrangment.Columns["idTravelPapers"].IsVisible = false;
                    //rgvArrangment.Columns["codeArrangement"].IsVisible = false;
                    //rgvArrangment.Columns["nrMaximumWheelchairs"].IsVisible = false;
                    //rgvArrangment.Columns["whoseElectricWheelchairs"].IsVisible = false;
                    //rgvArrangment.Columns["buSupportingArms"].IsVisible = false;
                    //rgvArrangment.Columns["nrAnchorage"].IsVisible = false;

                    for (int i = 0; i < rgvArrangment.Columns.Count; i++)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            // if (resxSet.GetString(rgvArrangment.Columns[i].HeaderText) != "" || resxSet.GetString(rgvArrangment.Columns[i].HeaderText)!=null)
                            if ((rgvArrangment.Columns[i].HeaderText) != "" && (rgvArrangment.Columns[i].HeaderText) != null)
                            {
                                if (resxSet.GetString(rgvArrangment.Columns[i].HeaderText) != null)
                                    rgvArrangment.Columns[i].HeaderText = resxSet.GetString(rgvArrangment.Columns[i].HeaderText);
                            }
                        }
                        //rgvArrangment.Columns[i].MaxWidth = (int)(this.CreateGraphics().MeasureString(rgvArrangment.Columns[i].HeaderText, this.Font).Width + 46);
                        //rgvArrangment.Columns[i].Width = rgvArrangment.Columns[i].MaxWidth;
                        //rgvArrangment.Columns[i].MinWidth = rgvArrangment.Columns[i].MaxWidth;
                    }
                    if (File.Exists(layoutArrangement))
                    {
                        rgvArrangment.LoadLayout(layoutArrangement);
                    }
                    rgvArrangment.Show();
                    break;
                //case "tabMeetings":

                //    rgvMeetings.DataSource = new MeetingsBUS().GetMeetingByPerson(Person.idContPers);
                //    // saki

                //    if (rgvMeetings.DataSource == null)
                //    {
                //        List<MeetingsModel> personMeetings = new List<MeetingsModel>();
                //        rgvMeetings.DataSource = personMeetings;
                //    }

                //    rgvMeetings.Columns["idMeeting"].IsVisible = false;
                //    rgvMeetings.Columns["durationMeeting"].IsVisible = false;
                //    rgvMeetings.Columns["isAllDayMeeting"].IsVisible = false;
                //    rgvMeetings.Columns["clientId"].IsVisible = false;
                //    rgvMeetings.Columns["contactPersonId"].IsVisible = false;
                //    rgvMeetings.Columns["projectId"].IsVisible = false;
                //    rgvMeetings.Columns["emploeeOwner"].IsVisible = false;
                //    rgvMeetings.Columns["employeeResponsible"].IsVisible = false;
                //    rgvMeetings.Columns["isRemind"].IsVisible = false;
                //    rgvMeetings.Columns["timeRemind"].IsVisible = false;
                //    rgvMeetings.Columns["dteRemind"].IsVisible = false;
                //    rgvMeetings.Columns["endDateMeeting"].IsVisible = false;
                //    rgvMeetings.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
                //    //  rgvMeetings.Columns[""].IsVisible = false;
                //    ////  rgvMeetings.Columns[""].IsVisible = false;
                //    //  rgvMeetings.Columns[""].IsVisible = false;

                //    //saki end
                //    rgvMeetings.Show();
                //    break;
                //case "tabContacts":
                //    break;
                //case "tabTasks":
                //    break;
                // endSAKI
            }
        }

        private void chkDied_CheckStateChanged(object sender, EventArgs e)
        {
            RadCheckBox rchk = (RadCheckBox)sender;
            if (rchk.Checked == true)
            {
                dtDied.Visible = true;
            }
            else
            {
                dtDied.Visible = false;
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                dialog.InitialDirectory = @"C:\";
                dialog.Title = "Please select an image file to encrypt.";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the file's path
                    var filePath = dialog.FileName;
                    //save image
                    picUser.Image = Image.FromFile(dialog.FileName);
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void btnDeletePicture_Click(object sender, EventArgs e)
        {
            picUser.Image = GUI.Properties.Resources.DefaultPerson;
        }

        private void rgvTel_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvTel.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvTel.Columns[i].HeaderText != null && resxSet.GetString(rgvTel.Columns[i].HeaderText) != null)
                        rgvTel.Columns[i].HeaderText = resxSet.GetString(rgvTel.Columns[i].HeaderText);
                }
            }
        }

        private void rgvEmail_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvEmail.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvEmail.Columns[i].HeaderText != null && resxSet.GetString(rgvEmail.Columns[i].HeaderText) != null)
                        rgvEmail.Columns[i].HeaderText = resxSet.GetString(rgvEmail.Columns[i].HeaderText);
                }
            }
        }

        private void rgvDocuments_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {


            for (int i = 0; i < rgvDocuments.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvDocuments.Columns[i].HeaderText != null && resxSet.GetString(rgvDocuments.Columns[i].HeaderText) != null)
                        rgvDocuments.Columns[i].HeaderText = resxSet.GetString(rgvDocuments.Columns[i].HeaderText);
                }
            }

            //Novo Gorance 31 8
            if (documentsLoaded==false)
            {

                GridViewCommandColumn commandColumn = new GridViewCommandColumn(" ");
                commandColumn.UseDefaultText = false;
                //commandColumn.DefaultText = "Document";
                commandColumn.Width = (int)(this.CreateGraphics().MeasureString(commandColumn.HeaderText, this.Font).Width + 9);
                commandColumn.Image = Properties.Resources.view_x20;
                this.rgvDocuments.Columns.Add(commandColumn);
                documentsLoaded = true;
            }

            if (File.Exists(layoutDocuments))
            {
                rgvDocuments.LoadLayout(layoutDocuments);
            }
            else
            {
                rgvDocuments.Columns["idProject"].IsVisible = false;
                rgvDocuments.Columns["idClient"].IsVisible = false;
                rgvDocuments.Columns["idContPers"].IsVisible = false;
                rgvDocuments.Columns["idEmployee"].IsVisible = false;
                rgvDocuments.Columns["idResponsableEmployee"].IsVisible = false;
                rgvDocuments.Columns["idDocumentStatus"].IsVisible = false;
                rgvDocuments.Columns["dtModified"].IsVisible = false;
                rgvDocuments.Columns["userModified"].IsVisible = false;
                rgvDocuments.Columns["userCreated"].IsVisible = false;
                rgvDocuments.Columns["idLayout"].IsVisible = false;
                rgvDocuments.Columns["inOutDocument"].IsVisible = false;
                rgvDocuments.Columns["selected"].IsVisible = false;
            }
        }

        private void rgvDocuments_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {

            GridViewRowInfo info = this.rgvDocuments.CurrentRow;
            if (info != null && e.RowIndex >= 0)
            {
                if (rgvDocuments.Rows.Count > 0)
                {
                    int index = rgvDocuments.SelectedRows[0].Index;
                    int iID = Int32.Parse(rgvDocuments.SelectedRows[0].Cells["idDocument"].Value.ToString());
                    int idConstPers = Person.idContPers;


                    using (frmDocuments frm = new frmDocuments(iID, idConstPers))
                    {
                        frm.ShowDialog();
                        if (frm.modelChanged == true)
                        {
                            DocumentsBUS nbus = new DocumentsBUS();

                            personDoc = nbus.GetPersonDoc(Person.idContPers, Login._user.lngUser);
                            rgvDocuments.DataSource = null;
                            rgvDocuments.DataSource = personDoc;
                            if (rgvDocuments.Rows.Count > index)
                            {
                                rgvDocuments.Rows[index].IsCurrent = true;
                                rgvDocuments.Rows[index].IsSelected = true;
                            }
                        }
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }

            }




        }

        private void rgvNote_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvNote.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvNote.Columns[i].HeaderText != null && resxSet.GetString(rgvNote.Columns[i].HeaderText) != null)
                        rgvNote.Columns[i].HeaderText = resxSet.GetString(rgvNote.Columns[i].HeaderText);
                }
            }

            if (File.Exists(layoutMemo))
            {
                rgvNote.LoadLayout(layoutMemo);
            }
            else
            {
                rgvNote.Columns["idContPers"].IsVisible = false;
                rgvNote.Columns["idEmployee"].IsVisible = false;
                rgvNote.Columns["dtModified"].IsVisible = false;
                rgvNote.Columns["idUserModified"].IsVisible = false;
                rgvNote.Columns["idUserCreated"].IsVisible = false;
                rgvNote.Columns["idTypeNote"].IsVisible = false;
            }
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            if (Login.isOutlookInstalled == true)
            {
                if (Person.idContPers != 0)
                {
                    PersonEmailBUS pbus = new PersonEmailBUS();
                    DataTable dt = pbus.GetPersonEmailsIsCommunicationTable(Person.idContPers);

                    string emailTo = "";
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            emailTo = dr["email"].ToString();
                        }
                    }

                    try
                    {
                        List<string> lstAllRecipients = new List<string>();
                        if (emailTo.Trim() != "")
                            lstAllRecipients.Add(emailTo);

                        if (lstAllRecipients.Count > 0)
                        {
                            Outlook.Application outlookApp = new Outlook.Application();
                            outlookApp.ItemSend += new Outlook.ApplicationEvents_11_ItemSendEventHandler(outlookApp_ItemSend);

                            Outlook._MailItem oMailItem = (Outlook._MailItem)outlookApp.CreateItem(Outlook.OlItemType.olMailItem);
                            Outlook.Inspector oInspector = oMailItem.GetInspector;
                            oMailItem.DeleteAfterSubmit = false;

                            // Recipient
                            Outlook.Recipients oRecips = (Outlook.Recipients)oMailItem.Recipients;
                            foreach (String recipient in lstAllRecipients)
                            {
                                Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add(recipient);
                                oRecip.Resolve();
                            }
                            //Add CC
                            // Outlook.Recipient oCCRecip = oRecips.Add("THIYAGARAJAN.DURAIRAJAN@testmail.com");
                            //oCCRecip.Type = (int)Outlook.OlMailRecipientType.olCC;
                            //oCCRecip.Resolve();

                            //Add Subject
                            string personame = "";
                            if (Person != null)
                            {
                                if (Person.firstname.Trim() != "")
                                    personame += Person.firstname.Trim();

                                personame += " ";

                                if (Person.midname.Trim() != "")
                                    personame += Person.midname.Trim();

                                personame += " ";

                                if (Person.lastname.Trim() != "")
                                    personame += Person.lastname.Trim();

                            }
                            oMailItem.Subject = "";
                            oMailItem.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatRichText;
                            oMailItem.Body = "Beste " + personame + ", \r\n";

                            Outlook.Folder outlookfolder = Login.GetOutlookBisFolder();
                            if (outlookfolder != null)
                                oMailItem.SaveSentMessageFolder = outlookfolder;
                            //  oMailItem.SaveSentMessageFolder = Login.sentFolder;

                            //Display the mailbox
                            oMailItem.Display(true);
                        }
                        else
                        {
                            translateRadMessageBox msgbox = new translateRadMessageBox();
                            msgbox.translateAllMessageBox("Invalid mail address.");
                        }

                    }
                    catch (Exception objEx)
                    {
                        RadMessageBox.Show(objEx.ToString());
                    }
                }
                else
                {
                    RadMessageBox.Show("You need to add person first.");
                }
            }
            else
            {
                translateRadMessageBox msgbox = new translateRadMessageBox();
                msgbox.translateAllMessageBox("Cannot find Outlook.");
            }
        }

        void outlookApp_ItemSend(object Item, ref bool Cancel)
        {
            if (Item is Microsoft.Office.Interop.Outlook.MailItem)
            {
                Microsoft.Office.Interop.Outlook.MailItem item = (Microsoft.Office.Interop.Outlook.MailItem)Item;
                item.Save();

                DocumentsBUS sbus = new DocumentsBUS();
                PersonEmailBUS emailbus = new PersonEmailBUS();


                string locationOnDisk = MainForm.myEmailFolder + "\\" + item.EntryID + ".msg";

                if (!File.Exists(locationOnDisk))
                    item.SaveAs(locationOnDisk, Microsoft.Office.Interop.Outlook.OlSaveAsType.olMSG);

                if (Person.idContPers != 0)
                {
                    DocumentsModel model = new DocumentsModel();
                    model.idContPers = Person.idContPers;
                    model.idClient = 0;
                    model.descriptionDocument = "Email";
                    model.fileDocument = item.EntryID + ".msg";
                    model.typeDocument = "EML";
                    model.idDocumentStatus = 2;
                    model.idEmployee = 0;
                    model.idResponsableEmployee = 0;
                    model.inOutDocument = 0;
                    model.noteDocument = "Sent Email";
                    model.idArrangement = 0;
                    //model.id

                    model.dtCreated = DateTime.Now;
                    model.dtModified = DateTime.Now;
                    model.userCreated = Login._user.idUser;
                    model.userModified = Login._user.idUser;

                    sbus.Save(model, this.Name, Login._user.idUser);
                }

                Cancel = false;
            }
        }

        void ThisAddIn_Close(ref bool Cancel)
        {
            //MessageBox.Show("MailItem is closed");
        }

        private void btnDeleteMemo_Click(object sender, EventArgs e)
        {
            if (rgvNote.SelectedRows.Count > 0)
            {
                int id;
                string text = "";

                if (rgvNote.SelectedRows[0].Cells["idNote"].Value != null)
                {
                    id = Int32.Parse(rgvNote.SelectedRows[0].Cells["idNote"].Value.ToString().Trim());
                    text = rgvNote.SelectedRows[0].Cells["noteText"].Value.ToString();
                    DialogResult dr = RadMessageBox.Show("Are you sure that you want to delete note ? \n " + text, "", MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        NotesBUS nb = new NotesBUS();
                        nb.Delete(id, this.Name, Login._user.idUser);
                        personNotes = nb.GetPersonNotes(Person.idContPers);
                        rgvNote.DataSource = null;
                        rgvNote.DataSource = personNotes;
                    }
                }
            }
            else
            {
                if (rgvNote.Rows.Count > 0)
                    RadMessageBox.Show("First you have to select note that you want to delete");
                else
                    RadMessageBox.Show("You don't have notes to delete");
            }
        }

        private void btnNewMemo_Click(object sender, EventArgs e)
        {
            using (frmPersonNotes frm = new frmPersonNotes(Person.idContPers))
            {
                frm.ShowDialog();
                if (frm.modelChanged == true)
                {
                    NotesBUS nbus = new NotesBUS();
                    personNotes = nbus.GetPersonNotes(Person.idContPers);
                    rgvNote.DataSource = null;
                    rgvNote.DataSource = personNotes;
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void rgvTel_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                MasterGridViewTemplate mgvt = (MasterGridViewTemplate)sender;
                PersonTelBUS ptb = new PersonTelBUS();
                if (ptb.Delete(Convert.ToInt32(mgvt.CurrentRow.Cells["idTel"].Value.ToString()), this.Name, Login._user.idUser) == false)
                {
                    RadMessageBox.Show("Something went wrong with deleting this tell");
                    e.Cancel = true;
                }
            }
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                MasterGridViewTemplate mgvt = (MasterGridViewTemplate)sender;
                if (mgvt.CurrentRow.Cells["numberTel"].Value != null)
                {
                    if (mgvt.CurrentRow.Cells["numberTel"].Value.ToString().Trim() == "")
                        e.Cancel = true;
                }
                else
                    e.Cancel = true;
            }
        }

        private void rgvEmail_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                MasterGridViewTemplate mgvt = (MasterGridViewTemplate)sender;
                PersonEmailBUS peb = new PersonEmailBUS();
                if (peb.Delete(Convert.ToInt32(mgvt.CurrentRow.Cells["idEmail"].Value.ToString()), this.Name, Login._user.idUser) == false)
                {
                    RadMessageBox.Show("Something went wrong with deleting this email");
                    e.Cancel = true;
                }
            }
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                MasterGridViewTemplate mgvt = (MasterGridViewTemplate)sender;
                if (mgvt.CurrentRow.Cells["email"].Value != null)
                {
                    if (mgvt.CurrentRow.Cells["email"].Value.ToString().Trim() == "")
                        e.Cancel = true;
                }
                else
                    e.Cancel = true;
            }
        }

        private void btnNewDoc_Click(object sender, EventArgs e)
        {
            string what = "person";
            using (frmDocuments frm = new frmDocuments(Person.idContPers, what))
            {
                frm.ShowDialog();
                if (frm.modelChanged == true)
                {
                    DocumentsBUS nbus = new DocumentsBUS();

                    personDoc = nbus.GetPersonDoc(Person.idContPers, Login._user.lngUser);
                    rgvDocuments.DataSource = null;
                    rgvDocuments.DataSource = personDoc;
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }


        #region Documents

        private System.Collections.ArrayList alControls = new System.Collections.ArrayList();

        private void GetAllFormControls()
        {
            alControls.Clear();
            //sve kontrole u alControls, ali samo prvi nivo
            foreach (Control c in mainForm.Controls)
                foreach (Control c1 in c.Controls)
                    foreach (Control ctlP in c1.Controls)
                    {

                        switch (ctlP.GetType().FullName)
                        {
                            case "Telerik.WinControls.UI.RadPageView":
                                foreach (Control ctlGB1 in ((Telerik.WinControls.UI.RadPageView)ctlP).Controls)
                                    foreach (Control ctl1 in ((Telerik.WinControls.UI.RadPageViewPage)ctlGB1).Controls)
                                        alControls.Add(ctl1);
                                break;
                            case "Telerik.WinControls.UI.RadCollapsiblePanel":
                                foreach (Control ctl1 in ((Telerik.WinControls.UI.RadCollapsiblePanel)ctlP).Controls)
                                    alControls.Add(ctl1);
                                break;
                            case "Telerik.WinControls.UI.RadGroupBox":
                                foreach (Control ctl1 in ((Telerik.WinControls.UI.RadGroupBox)ctlP).Controls)
                                    alControls.Add(ctl1);
                                break;

                            default:
                                alControls.Add(ctlP);
                                break;
                        }
                    }
        }

        private Microsoft.Office.Interop.Word.Application wordApp;

        private void btnWord_Click(object sender, EventArgs e)
        {
            // ReadTemplateFile(wordApp, "ContactPerson", "idContPers", Person.idContPers);

            List<BIS.Model.IModel> lookupModel = new List<BIS.Model.IModel>();
            BIS.Business.LayoutsBUS bBUS = new BIS.Business.LayoutsBUS();
            lookupModel = bBUS.GetAllLayoutsbyTemplateTable("ContactPerson");

            using (var lookfrm = new GridLookupForm(lookupModel, "Templates"))
            {
                if (lookfrm.ShowDialog(this) == DialogResult.Yes)
                {
                    BookmarkFunctions.ReadTemplateFile(wordApp, "ContactPerson", "idContPers", Person.idContPers, null, (BIS.Model.LayoutsModel)lookfrm.selectedRow, this.Name, Login._user.idUser);
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }


        #endregion

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // new Task
            string what = "new";
            using (frmTasks frm = new frmTasks(Person.idContPers, what))
            {
                frm.ShowDialog();
                if (frm.modelChanged == true)
                {
                    ToDoBUS nbus = new ToDoBUS();

                    personToDo = nbus.GetToDoPerson(Person.idContPers);
                    rgvToDo.DataSource = null;
                    rgvToDo.DataSource = personToDo;
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // Delete Task
            if (DialogResult.Yes == RadMessageBox.Show("Are you sure to delete this Task?", "Delete", MessageBoxButtons.YesNo, RadMessageIcon.Question))
            {
                try
                {
                    int iID = Int32.Parse(rgvToDo.SelectedRows[0].Cells["idToDo"].Value.ToString());
                    ToDoBUS del = new ToDoBUS();
                    del.Delete(iID, this.Name, Login._user.idUser);
                    personToDo = del.GetToDoPerson(Person.idContPers);
                    rgvToDo.DataSource = null;
                    rgvToDo.DataSource = personToDo;
                }
                catch (Exception ex)
                {
                    RadMessageBox.Show("Error deleting document. \nMessage: " + ex.Message, "Error delete", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
            }

        }

        private void LoadMeetings()
        {
            radRibbonContact.Visibility = ElementVisibility.Visible;
            radRibbonTask.Visibility = ElementVisibility.Visible;
            btnDelContact.Visibility = ElementVisibility.Collapsed;
            btnDelTask.Visibility = ElementVisibility.Collapsed;
            rgvMeetings.DataSource = null;
            rgvMeetings.DataSource = new AppointmentsBUS().GetAppointmentsByPerson(Person.idContPers, Login._user.lngUser);
            // saki

            if (rgvMeetings.DataSource == null)
            {
                List<BISAppointment> personMeetings = new List<BISAppointment>();
                rgvMeetings.DataSource = personMeetings;
            }
            rgvMeetings.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            rgvMeetings.Show();


        }

        private void radPageComm_SelectedPageChanged(object sender, EventArgs e)
        {
            RadPageView rpv1 = (RadPageView)sender;
            string sName1 = ((RadPageView)sender).SelectedPage.Name;

            switch (sName1)
            {
                case "tabMeetings":

                    LoadMeetings();


                    break;
                case "tabContacts":
                    radRibbonContact.Visibility = ElementVisibility.Visible;
                    btnNewContact.Visibility = ElementVisibility.Visible;
                    btnNewTask.Visibility = ElementVisibility.Visible;
                    btnDelContact.Visibility = ElementVisibility.Visible;
                    btnDelTask.Visibility = ElementVisibility.Collapsed;

                    //  radRibbonTask.Visibility = ElementVisibility.Collapsed;

                    //5.7.2015

                    rgvContacts.DataSource = new ContactsBUS().GetContactsByPerson(Person.idContPers);
                    // saki

                    if (rgvContacts.DataSource == null)
                    {
                        List<ContactsModel> personContacts = new List<ContactsModel>();
                        rgvContacts.DataSource = personContacts;
                    }

                    rgvContacts.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

                    rgvContacts.Show();
                    // 5.7.2015
                    break;
                case "tabTasks":
                    radRibbonContact.Visibility = ElementVisibility.Visible;
                    radRibbonTask.Visibility = ElementVisibility.Visible;
                    btnNewContact.Visibility = ElementVisibility.Visible;
                    btnDelContact.Visibility = ElementVisibility.Collapsed;
                    btnDelTask.Visibility = ElementVisibility.Visible;



                    rgvToDo.DataSource = new ToDoBUS().GetToDoPerson(Person.idContPers);
                    // saki

                    if (rgvToDo.DataSource == null)
                    {
                        List<ToDoModel> personToDo = new List<ToDoModel>();
                        rgvToDo.DataSource = personToDo;
                    }

                    rgvToDo.Columns["idToDo"].IsVisible = false;
                    rgvToDo.Columns["idClient"].IsVisible = false;
                    rgvToDo.Columns["idContPers"].IsVisible = false;
                    rgvToDo.Columns["idProject"].IsVisible = false;
                    rgvToDo.Columns["idOwner"].IsVisible = false;
                    rgvToDo.Columns["idEmployee"].IsVisible = false;
                    rgvToDo.Columns["idPriorityToDo"].IsVisible = false;
                    rgvToDo.Columns["idStatusToDo"].IsVisible = false;
                    rgvToDo.Columns["idToDoType"].IsVisible = false;
                    //rgvToDo.Columns["isRemider"].IsVisible = false;

                    rgvToDo.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

                    rgvToDo.Show();
                    break;
            }
        }

        private void rgvToDo_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.rgvToDo.CurrentRow;
            if (info != null && e.RowIndex >= 0)
            {
                if (rgvToDo.Rows.Count > 0)
                {
                    int iID = Int32.Parse(rgvToDo.SelectedRows[0].Cells["idToDo"].Value.ToString());
                    int idConstPers = Person.idContPers;

                    string what = "open";
                    using (frmTasks frm = new frmTasks(iID, what, idConstPers))
                    {
                        frm.ShowDialog();
                        if (frm.modelChanged == true)
                        {
                            ToDoBUS nbus = new ToDoBUS();

                            personToDo = nbus.GetToDoPerson(Person.idContPers);
                            rgvToDo.DataSource = null;
                            rgvToDo.DataSource = personToDo;
                        }
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();

                }
            }
        }

        private void btnDeleteDoc_Click(object sender, EventArgs e)
        {
            if (rgvDocuments.SelectedRows.Count > 0)
            {
                int id;
                string text = "";

                if (rgvDocuments.SelectedRows[0].Cells["idDocument"].Value != null)
                {
                    id = Int32.Parse(rgvDocuments.SelectedRows[0].Cells["idDocument"].Value.ToString().Trim());
                    text = rgvDocuments.SelectedRows[0].Cells["fileDocument"].Value.ToString();
                    DialogResult dr = RadMessageBox.Show("Are you sure that you want to delete document ? \n " + text, "", MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        DocumentsBUS db = new DocumentsBUS();

                        bool bCllient = db.CheckDocumentIdClient(id);
                        bool bProject = db.CheckDocumentIdProject(id);
                        bool bEmployee = db.CheckDocumentIdEmployee(id);
                        bool bArrangement = db.CheckDocumentidArrangement(id);

                        if (bCllient == true || bProject == true || bEmployee == true || bArrangement == true)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Document cannot be deleted.");
                        }
                        else
                        {
                            db.Delete(id, this.Name, Login._user.idUser);
                            personDoc = db.GetPersonDoc(Person.idContPers, Login._user.lngUser);
                            rgvDocuments.DataSource = null;
                            rgvDocuments.DataSource = personDoc;
                        }
                    }
                }
            }
            else
            {
                if (rgvDocuments.Rows.Count > 0)
                    RadMessageBox.Show("First you have to select document that you want to delete");
                else
                    RadMessageBox.Show("You don't have documents to delete");
            }
        }

        private void rgvMeetings_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = rgvMeetings.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                BISAppointment appID = (BISAppointment)info.DataBoundItem;
                IEvent ev = MainForm.meetingScheduler.radScheduler1.Appointments.GetById(appID.Id);
                MeetingEditAppointment editAppForm = new MeetingEditAppointment();
                editAppForm.ThemeName = "Windows8";
                editAppForm.EditAppointment(ev, MainForm.meetingScheduler.radScheduler1);
                editAppForm.ShowDialog();
                //dodala Neta jer nije radio refresh
                rgvMeetings.DataSource = null;
                rgvMeetings.DataSource = new AppointmentsBUS().GetAppointmentsByPerson(Person.idContPers, Login._user.lngUser);
            }
        }

        private void rgvContacts_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {

            for (int i = 0; i < rgvContacts.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvContacts.Columns[i].HeaderText != null && resxSet.GetString(rgvContacts.Columns[i].HeaderText) != null)
                        rgvContacts.Columns[i].HeaderText = resxSet.GetString(rgvContacts.Columns[i].HeaderText);
                }
            }

            if (File.Exists(layoutContacts))
            {
                rgvContacts.LoadLayout(layoutContacts);
            }
            else
            {
                //rgvContacts.Columns["idContact"].IsVisible = false;
                //rgvContacts.Columns["idClient"].IsVisible = false;
                //rgvContacts.Columns["idContPers"].IsVisible = false;
                //rgvContacts.Columns["idProject"].IsVisible = false;
                //rgvContacts.Columns["idContactReason"].IsVisible = false;
                //rgvContacts.Columns["idContactType"].IsVisible = false;
                //rgvContacts.Columns["idCreator"].IsVisible = false;
            }
        }

        private void rgvToDo_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvToDo.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvToDo.Columns[i].HeaderText != null && resxSet.GetString(rgvToDo.Columns[i].HeaderText) != null)
                        rgvToDo.Columns[i].HeaderText = resxSet.GetString(rgvToDo.Columns[i].HeaderText);
                }
            }
            if (File.Exists(layoutTasks))
            {
                rgvToDo.LoadLayout(layoutTasks);
            }
            else
            {
                rgvToDo.Columns["idToDo"].IsVisible = false;
                rgvToDo.Columns["idClient"].IsVisible = false;
                rgvToDo.Columns["idContPers"].IsVisible = false;
                rgvToDo.Columns["idProject"].IsVisible = false;
                rgvToDo.Columns["idOwner"].IsVisible = false;
                rgvToDo.Columns["idEmployee"].IsVisible = false;
                rgvToDo.Columns["idPriorityToDo"].IsVisible = false;
                rgvToDo.Columns["idStatusToDo"].IsVisible = false;
                rgvToDo.Columns["idToDoType"].IsVisible = false;
                rgvToDo.Columns["idContact"].IsVisible = false;
            }
        }

        private void rgvMeetings_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvMeetings.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvMeetings.Columns[i].HeaderText != null && resxSet.GetString(rgvMeetings.Columns[i].HeaderText) != null)
                        rgvMeetings.Columns[i].HeaderText = resxSet.GetString(rgvMeetings.Columns[i].HeaderText);
                }
            }

            if (File.Exists(layoutMeetings))
            {
                rgvMeetings.LoadLayout(layoutMeetings);
            }
            else
            {
                rgvMeetings.Columns["id"].IsVisible = false;
                rgvMeetings.Columns["category"].IsVisible = false;
                rgvMeetings.Columns["priority"].IsVisible = false;
                rgvMeetings.Columns["status"].IsVisible = false;
                rgvMeetings.Columns["client"].IsVisible = false;
                rgvMeetings.Columns["clientName"].IsVisible = false;
                rgvMeetings.Columns["contact"].IsVisible = false;
                rgvMeetings.Columns["contactName"].IsVisible = false;
                rgvMeetings.Columns["project"].IsVisible = false;
                rgvMeetings.Columns["projectName"].IsVisible = false;
                rgvMeetings.Columns["owner"].IsVisible = false;
                rgvMeetings.Columns["ownerName"].IsVisible = false;
                rgvMeetings.Columns["responsible"].IsVisible = false;
                rgvMeetings.Columns["responsibleName"].IsVisible = false;
                rgvMeetings.Columns["isAllDay"].IsVisible = false;
                rgvMeetings.Columns["background"].IsVisible = false;
                rgvMeetings.Columns["showtime"].IsVisible = false;
                rgvMeetings.Columns["Reminder"].IsVisible = false;
                rgvMeetings.Columns["ReminderSnoozed"].IsVisible = false;
                rgvMeetings.Columns["ReminderDismissed"].IsVisible = false;
            }
        }

        private void rgvContacts_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = rgvContacts.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                if (rgvContacts.Rows.Count > 0)
                {

                    int iID = Int32.Parse(rgvContacts.SelectedRows[0].Cells["idContact"].Value.ToString());
                    int idConstPers = Person.idContPers;

                    string what = "open";
                    using (frmContacts frm = new frmContacts(iID, what, idConstPers))
                    {
                        //frmContacts frm = new frmContacts();
                        frm.ShowDialog();
                        if (frm.modelChanged == true)
                        {
                            ContactsBUS nbus1 = new ContactsBUS();

                            personContacts = nbus1.GetContactsByPerson(Person.idContPers);
                            rgvContacts.DataSource = null;
                            rgvContacts.DataSource = personContacts;
                        }
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            int iID = -1;               //Int32.Parse(rgvContacts.SelectedRows[0].Cells["idContact"].Value.ToString());
            int idConstPers = Person.idContPers;

            string what = "new";
            using (frmContacts frm = new frmContacts(iID, what, idConstPers))
            {
                //frmContacts frm = new frmContacts();
                frm.Show();
                if (frm.modelChanged == true)
                {
                    ContactsBUS nbus6 = new ContactsBUS();

                    personContacts = nbus6.GetContactsByPerson(Person.idContPers);
                    rgvContacts.DataSource = null;
                    rgvContacts.DataSource = personContacts;
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == RadMessageBox.Show("Are you sure to delete this Contact?", "Delete", MessageBoxButtons.YesNo, RadMessageIcon.Question))
            {
                try
                {
                    int iID = Int32.Parse(rgvContacts.SelectedRows[0].Cells["idContact"].Value.ToString());
                    ContactsBUS del1 = new ContactsBUS();
                    del1.Delete(iID, this.Name, Login._user.idUser);
                    personContacts = del1.GetContactsByPerson(Person.idContPers);
                    rgvContacts.DataSource = null;
                    rgvContacts.DataSource = personContacts;
                }
                catch (Exception ex)
                {
                    RadMessageBox.Show("Error deleting Contact. \nMessage: " + ex.Message, "Error delete", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
            }
        }

        private void rgvNote_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.rgvNote.CurrentRow;
            if (info != null && e.RowIndex >= 0)
            {
                if (rgvNote.Rows.Count > 0)
                {

                    int iID = Int32.Parse(rgvNote.SelectedRows[0].Cells["idNote"].Value.ToString());
                    int idConstPers = Person.idContPers;


                    using (frmPersonNotes frm = new frmPersonNotes(iID, idConstPers))
                    {
                        frm.ShowDialog();



                        NotesBUS nbus1 = new NotesBUS();
                        List<NotesModel> specialNotes = new List<NotesModel>();
                        specialNotes = nbus1.GetPersonStatus2(Person.idContPers);
                        if (specialNotes != null)
                        {
                            if (specialNotes.Count > 0)
                            {
                                this.tabMemo.Item.NumberOfColors = 1;
                                this.tabMemo.Item.ForeColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                this.tabMemo.Item.ForeColor = System.Drawing.Color.Black;
                            }

                        }
                        else
                        {
                            this.tabMemo.Item.ForeColor = System.Drawing.Color.Black;
                        }




                        if (frm.modelChanged == true)
                        {
                            NotesBUS nbus = new NotesBUS();

                            personNotes = nbus.GetPersonNotes(Person.idContPers);
                            rgvNote.DataSource = null;
                            rgvNote.DataSource = personNotes;
                        }
                    }

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }

            }
        }

        private void rgvNote_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                int index = this.rgvNote.Rows.IndexOf(this.rgvNote.CurrentRow as GridViewDataRowInfo);
                if (index >= 0)
                {
                    notesStripMenu.Show(Cursor.Position);
                }
            }
        }

        private void rgvDocuments_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.Button == System.Windows.Forms.MouseButtons.Right)
            //{
            //    int index = this.rgvDocuments.Rows.IndexOf(this.rgvDocuments.CurrentRow as GridViewDataRowInfo);
            //    if (index >= 0)
            //    {
            //        documentsStripMenu.Show(Cursor.Position);
            //    }
            //}
        }

        private void rgvMeetings_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                int index = this.rgvMeetings.Rows.IndexOf(this.rgvMeetings.CurrentRow as GridViewDataRowInfo);
                if (index >= 0)
                {
                    meetingsStripMenu.Show(Cursor.Position);
                }
            }
        }

        private void rgvContacts_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                int index = this.rgvContacts.Rows.IndexOf(this.rgvContacts.CurrentRow as GridViewDataRowInfo);
                if (index >= 0)
                {
                    contactsStripMenu.Show(Cursor.Position);
                }
            }
        }

        private void rgvToDo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                int index = this.rgvToDo.Rows.IndexOf(this.rgvToDo.CurrentRow as GridViewDataRowInfo);



                if (this.rgvToDo.CurrentCell != null)
                {
                    GridViewRowInfo rowinfo = this.rgvToDo.CurrentCell.RowInfo;
                    int index1 = this.rgvToDo.CurrentCell.RowIndex;
                }


                if (index >= 0)
                {
                    tasksStripMenu.Show(Cursor.Position);
                }
            }
        }

        private void NoteMenuSaveClick(object sender, EventArgs e)
        {
            //SAVE NOTE
            if (File.Exists(layoutMemo))
            {
                File.Delete(layoutMemo);
            }
            rgvNote.SaveLayout(layoutMemo);
        }

        private void DocumentsMenuSaveClick(object sender, EventArgs e)
        {
            // SAVE DOCUMENTS
            if (File.Exists(layoutDocuments))
            {
                File.Delete(layoutDocuments);
            }
            rgvDocuments.SaveLayout(layoutDocuments);
        }

        private void MeetingsMenuSaveClick(object sender, EventArgs e)
        {
            //SAVE NOTE
            if (File.Exists(layoutMeetings))
            {
                File.Delete(layoutMeetings);
            }
            rgvMeetings.SaveLayout(layoutMeetings);
        }

        private void ContactsMenuSaveClick(object sender, EventArgs e)
        {
            //SAVE NOTE
            if (File.Exists(layoutContacts))
            {
                File.Delete(layoutContacts);
            }
            rgvContacts.SaveLayout(layoutContacts);
        }


        private void TasksMenuSaveClick(object sender, EventArgs e)
        {
            //SAVE NOTE
            if (File.Exists(layoutTasks))
            {
                File.Delete(layoutTasks);
            }
            rgvToDo.SaveLayout(layoutTasks);
        }

        private void btnNewContact_Click(object sender, EventArgs e)
        {
            int iID = -1;
            int idConstPers = Person.idContPers;

            string what = "new";
            using (frmContacts frm = new frmContacts(iID, what, idConstPers))
            {
                //frmContacts frm = new frmContacts();
                frm.ShowDialog();

                if (frm.modelChanged == true)
                {
                    ContactsBUS nbus1 = new ContactsBUS();

                    personContacts = nbus1.GetContactsByPerson(Person.idContPers);
                    rgvContacts.DataSource = null;
                    rgvContacts.DataSource = personContacts;
                }
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void btnDelContact_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == RadMessageBox.Show("Are you sure to delete this Contact?", "Delete", MessageBoxButtons.YesNo, RadMessageIcon.Question))
            {
                try
                {
                    int iID = Int32.Parse(rgvContacts.SelectedRows[0].Cells["idContact"].Value.ToString());
                    ContactsBUS del1 = new ContactsBUS();
                    del1.Delete(iID, this.Name, Login._user.idUser);
                    personContacts = del1.GetContactsByPerson(Person.idContPers);
                    rgvContacts.DataSource = null;
                    rgvContacts.DataSource = personContacts;
                }
                catch (Exception ex)
                {
                    RadMessageBox.Show("Error deleting Contact. \nMessage: " + ex.Message, "Error delete", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
            }
        }

        private void btnNewTask_Click(object sender, EventArgs e)
        {
            // new Task
            string what = "new";

            using (frmTasks frm = new frmTasks(Person.idContPers, what))
            {
                frm.ShowDialog();
                if (frm.modelChanged == true)
                {
                    ToDoBUS nbus = new ToDoBUS();

                    personToDo = nbus.GetToDoPerson(Person.idContPers);
                    rgvToDo.DataSource = null;
                    rgvToDo.DataSource = personToDo;
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void btnDelTask_Click(object sender, EventArgs e)
        {
            // Delete Task
            if (DialogResult.Yes == RadMessageBox.Show("Are you sure to delete this Task?", "Delete", MessageBoxButtons.YesNo, RadMessageIcon.Question))
            {
                try
                {
                    int iID = Int32.Parse(rgvToDo.SelectedRows[0].Cells["idToDo"].Value.ToString());
                    ToDoBUS del = new ToDoBUS();
                    del.Delete(iID, this.Name, Login._user.idUser);
                    personToDo = del.GetToDoPerson(Person.idContPers);
                    rgvToDo.DataSource = null;
                    rgvToDo.DataSource = personToDo;
                }
                catch (Exception ex)
                {
                    RadMessageBox.Show("Error deleting document. \nMessage: " + ex.Message, "Error delete", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
            }
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutMemo))
            {
                File.Delete(layoutMemo);
            }
            rgvNote.SaveLayout(layoutMemo);

            MessageBox.Show("Layout Saved");
        }

        private void radMenuItemSavelayoutDocuments_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutDocuments))
            {
                File.Delete(layoutDocuments);
            }
            rgvDocuments.SaveLayout(layoutDocuments);

            MessageBox.Show("Layout Saved");
        }

        private void radMenuItemSaveMeetingsLayout_Click(object sender, EventArgs e)
        {
            //SAVE NOTE
            if (File.Exists(layoutMeetings))
            {
                File.Delete(layoutMeetings);
            }
            rgvMeetings.SaveLayout(layoutMeetings);

            MessageBox.Show("Layout Saved");
        }

        private void radMenuItemSaveContactsLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutContacts))
            {
                File.Delete(layoutContacts);
            }
            rgvContacts.SaveLayout(layoutContacts);

            MessageBox.Show("Layout Saved");
        }

        private void radMenuItemSaveTasksLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutTasks))
            {
                File.Delete(layoutTasks);
            }
            rgvToDo.SaveLayout(layoutTasks);

            MessageBox.Show("Layout Saved");

        }

        private void btnNewMeeting_Click(object sender, EventArgs e)
        {
            using (frmMeetingPerson frmMeeting = new frmMeetingPerson(Person.idContPers, Person.fullname))
            {
                frmMeeting.ShowDialog();
                MainForm.meetingScheduler.ReloadAppointments(1, Login._user.idEmployee);                
            }
            

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

        }

        private void chkDebitor_CheckStateChanged(object sender, EventArgs e)
        {
            // ako decekira brise se sadrzaj lookapa i anulira u modelu
            if (chkDebitor.Checked == false)
            {
                txtDebAcc.Text = "";
                debCre.debAccount = "";
            }
            else
            {
                if (asmo != null)
                    if (asmo.defDebitorAccount != null && asmo.defDebitorAccount != "")
                    {
                        AccAcountUpdate acuu = new AccAcountUpdate();
                        string nameacc = "";
                        nameacc = acuu.AccountName(asmo.defDebitorAccount.ToString());
                        txtDebAcc.Text = asmo.defDebitorAccount.ToString() + " " + nameacc;
                        debCre.debAccount = asmo.defDebitorAccount;
                    }
            }
        }

        private void chkCreditor_CheckStateChanged(object sender, EventArgs e)
        {
            // ako decekira brise se sadrzaj lookapa i anulira u modelu
            if (chkCreditor.Checked == false)
            {
                txtCreAcc.Text = "";
                debCre.creditAccount = "";
            }
            else
            {
                if (asmo != null)
                    if (asmo.defCreditorAccount != null && asmo.defCreditorAccount != "")
                    {
                        AccAcountUpdate acuu = new AccAcountUpdate();
                        string nameacc = "";
                        nameacc = acuu.AccountName(asmo.defCreditorAccount.ToString());
                        txtCreAcc.Text = asmo.defCreditorAccount.ToString() + " " + nameacc;
                        debCre.creditAccount = asmo.defCreditorAccount;
                    }
            }
        }

        private void btnDebAcc_Click(object sender, EventArgs e)
        {
            if (chkDebitor.Checked == true)
            {
                LedgerAccountBUS accBUS = new LedgerAccountBUS(Login._bookyear);
                List<IModel> am = new List<IModel>();

                am = accBUS.GetAllAccounts();


                using (var dlgClient = new GridLookupForm(am, "Ledger"))
                {

                    if (dlgClient.ShowDialog(this) == DialogResult.Yes)
                    {
                        LedgerAccountModel okm = new LedgerAccountModel();
                        okm = (LedgerAccountModel)dlgClient.selectedRow;
                        txtDebAcc.Text = okm.numberLedgerAccount + " " + okm.descLedgerAccount;
                        debCre.debAccount = okm.numberLedgerAccount;

                    }
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

            }
            else
            {
                RadMessageBox.Show("Check Debitor first !");
                chkDebitor.Focus();
            }
        }

        private void btnCreAcc_Click(object sender, EventArgs e)
        {
            if (chkCreditor.Checked == true)
            {
                LedgerAccountBUS accBUS1 = new LedgerAccountBUS(Login._bookyear);
                List<IModel> am1 = new List<IModel>();

                am1 = accBUS1.GetAllAccounts();


                using (var dlgClient = new GridLookupForm(am1, "Ledger"))
                {

                    if (dlgClient.ShowDialog(this) == DialogResult.Yes)
                    {
                        LedgerAccountModel okm1 = new LedgerAccountModel();
                        okm1 = (LedgerAccountModel)dlgClient.selectedRow;
                        txtCreAcc.Text = okm1.numberLedgerAccount + " " + okm1.descLedgerAccount;
                        debCre.creditAccount = okm1.numberLedgerAccount;

                    }
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            else
            {
                RadMessageBox.Show("Check Creditor first !");
                chkCreditor.Focus();
            }
        }

        private void radPageVH_SelectedPageChanged(object sender, EventArgs e)
        {
            RadPageView rpvVV = (RadPageView)sender;
            string sName2 = ((RadPageView)sender).SelectedPage.Name;

            switch (sName2)
            {
                case "tabSkils":
                    loadOnVoluntaryForm();
                    break;
                case "tabFunction":
                    loadOnVoluntarySectab();
                    break;
                case "tabTrips":
                    loadOnVoluntaryThird();
                    break;
                case "tabFeatures":
                    break;
                case "tabAvailability":
                    LoadOnAvailabilityTab();
                    break;
                case "tabSimilarity":
                    LoadSimilarities();
                    break;
            }
        }
        private void LoadOnAvailabilityTab()
        {
            // fill data just once. if already filled skip
            if (volAvailabilityList == null)
            {
                VolAvailabilityBUS volbus = new VolAvailabilityBUS();
                List<VolAvailabilityModel> volmodel = new List<VolAvailabilityModel>();

                volmodel = volbus.GetAvailabilityByVolontary(Person.idContPers);
                if (volmodel != null)
                    volAvailabilityList = new BindingList<VolAvailabilityModel>(volmodel);
                else
                    volAvailabilityList = new BindingList<VolAvailabilityModel>();

                radGridViewAvailability.DataSource = volAvailabilityList;

                radGridViewAvailability.Columns["id"].IsVisible = false;
                radGridViewAvailability.Columns["idContPers"].IsVisible = false;
                radGridViewAvailability.Columns["dateFrom"].Width = 170;
                radGridViewAvailability.Columns["dateFrom"].FormatString = "{0: d/M/yyyy}";
                radGridViewAvailability.Columns["dateFrom"].ReadOnly = true;
                radGridViewAvailability.Columns["dateTo"].Width = 170;
                radGridViewAvailability.Columns["dateTo"].FormatString = "{0: d/M/yyyy}";
                radGridViewAvailability.Columns["dateTo"].ReadOnly = true;
                radGridViewAvailability.Columns["nrTimes"].Width = 100;


                GridViewDecimalColumn myDecimalColumn = (GridViewDecimalColumn)radGridViewAvailability.Columns["nrTimes"];
                myDecimalColumn.DataType = typeof(int);
                myDecimalColumn.Minimum = 0;

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(radButtonAvailabilityAdd.Text) != null)
                        radButtonAvailabilityAdd.Text = resxSet.GetString(radButtonAvailabilityAdd.Text);
                    if (resxSet.GetString(lblDateFrom.Text) != null)
                        lblDateFrom.Text = resxSet.GetString(lblDateFrom.Text);
                    if (resxSet.GetString(lblDateTo.Text) != null)
                        lblDateTo.Text = resxSet.GetString(lblDateTo.Text);

                    for (int i = 0; i < radGridViewAvailability.Columns.Count; i++)
                    {
                        if (resxSet.GetString(radGridViewAvailability.Columns[i].HeaderText) != null)
                            radGridViewAvailability.Columns[i].HeaderText = resxSet.GetString(radGridViewAvailability.Columns[i].HeaderText);
                    }
                }
            }
        }
        private void radGridViewAvailability_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {
            translateRadMessageBox msgbox = new translateRadMessageBox();
            DialogResult dr = msgbox.translateAllMessageBoxDialog("Delete entry ?", "Delete");

            if (dr != DialogResult.Yes)
                e.Cancel = true;
            else
            {
                if (radGridViewAvailability.CurrentRow.DataBoundItem != null)
                {
                    VolAvailabilityBUS bus = new VolAvailabilityBUS();
                    VolAvailabilityModel model = (VolAvailabilityModel)radGridViewAvailability.CurrentRow.DataBoundItem;
                    bus.Delete(model.id, this.Name, Login._user.idUser);
                }
            }
        }


        int oldNrTImes = 0;
        private void radGridViewAvailability_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (e.Column.Name == "nrTimes")
            {
                if (e.Row.DataBoundItem != null)
                {
                    VolAvailabilityModel m = (VolAvailabilityModel)e.Row.DataBoundItem;
                    //MessageBox.Show(m.nrTimes.ToString());
                    oldNrTImes = m.nrTimes;
                }
            }
        }

        private void radGridViewAvailability_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            if (e.Column.Name == "nrTimes")
            {
                if (e.Row.DataBoundItem != null)
                {
                    VolAvailabilityModel m = (VolAvailabilityModel)e.Row.DataBoundItem;

                    if (oldNrTImes != m.nrTimes)
                    {
                        int id = (int)radGridViewAvailability.CurrentRow.Cells["id"].Value;

                        VolAvailabilityBUS vbus = new VolAvailabilityBUS();
                        vbus.UpdateNrTimes(m.nrTimes, id, this.Name, Login._user.idUser);
                        //MessageBox.Show(m.nrTimes.ToString());

                    }
                }
                oldNrTImes = 0;
            }
        }


        private void radButtonAvailabilityAdd_Click(object sender, EventArgs e)
        {
            try
            {
                VolAvailabilityBUS bus = new VolAvailabilityBUS();
                VolAvailabilityModel model = new VolAvailabilityModel();
                model.idContPers = Person.idContPers;
                //parsing date from short date time so time is always 00:00:00
                DateTime date1 = DateTime.Parse(dateAvailabilityFrom.Value.ToShortDateString());
                DateTime date2 = DateTime.Parse(dateAvailabilityTo.Value.ToShortDateString());
                //=== checking if the same interval exist ===
                if (volAvailabilityList != null)
                {
                    for (int j = 0; j < volAvailabilityList.Count; j++)
                    {
                        if (volAvailabilityList[j].dateFrom == date1 && volAvailabilityList[j].dateTo == date2)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("You can't enter same period twice !!!");
                            return;
                        }

                    }
                }

                //===========================

                model.dateFrom = date1;
                model.dateTo = date2;
                model.nrTimes = Convert.ToInt32(maskedVolNrTimes.Value);

                //int n = Convert.ToInt32((date2 - date1).TotalDays);
                //if(model.nrTimes > n)
                //{
                //    translateRadMessageBox msgbox = new translateRadMessageBox();
                //    msgbox.translateAllMessageBox("Invalid number of avaiability for selected period.");
                //    return;
                //}

                int idvol = bus.SaveAndReturnID(model, this.Name, Login._user.idUser);
                model.id = idvol;

                volAvailabilityList.Add(model);
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex.Message);
            }
        }

        private void firstPartVoluntarySectab()
        {
            tabFunction.Controls.Clear();

        }

        private void loadOnVoluntarySectab()
        {
            Cursor.Current = Cursors.WaitCursor;
            firstPartVoluntarySectab();

            RadToggleButton btnAllVoluntary = new RadToggleButton();
            btnAllVoluntary.CheckState = CheckState.Checked;
            btnAllVoluntary.ThemeName = pagePerson.ThemeName;
            btnAllVoluntary.Name = "btnAllVoluntary";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("Filled data") != null)
                    btnAllVoluntary.Text = resxSet.GetString("Filled data");
                else
                    btnAllVoluntary.Text = "Filled data";
            }
            btnAllVoluntary.Location = new Point(26, 10);
            btnAllVoluntary.Font = new Font("Verdana", 9);
            btnAllVoluntary.CheckStateChanged += btnVoluntaryFunctionAll_Click;

            RadToggleButton btnSortVoluntary = new RadToggleButton();
            btnSortVoluntary.CheckState = CheckState.Checked;
            btnSortVoluntary.ThemeName = pagePerson.ThemeName;
            btnSortVoluntary.Width = 200;
            btnSortVoluntary.Name = "btnSortVoluntary";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("Alfabetical sort") != null)
                    btnSortVoluntary.Text = resxSet.GetString("Alfabetical sort");
                else
                    btnSortVoluntary.Text = "Alfabetical sort";
            }
            btnSortVoluntary.Location = new Point(250, 10);
            btnSortVoluntary.Font = new Font("Verdana", 9);
            btnSortVoluntary.CheckStateChanged += btnVoluntaryFunctionSort_Click;

            tabFunction.Controls.Add(btnAllVoluntary);
            tabFunction.Controls.Add(btnSortVoluntary);

            secondPartVoluntaryFunction();

            Cursor.Current = Cursors.Default;

        }

        private void secondPartVoluntaryFunction()
        {
            List<string> idQueryType = new List<string>();

            foreach (Control c in panelLabels.Controls)
            {
                RadCheckBox rchk = (RadCheckBox)c;
                if (rchk.Checked == true)
                    idQueryType.Add(rchk.Name.Replace("chkLabel", ""));
            }

            RadToggleButton btnSortVoluntary = (RadToggleButton)tabFunction.Controls.Find("btnSortVoluntary", true)[0];
            RadToggleButton btnAllVoluntary = (RadToggleButton)tabFunction.Controls.Find("btnAllVoluntary", true)[0];

            Boolean isDefaultSort = false;

            if (btnSortVoluntary.CheckState == CheckState.Checked)
                isDefaultSort = true;

            //  Boolean isAll = false;
            Boolean isAll = false;

            if (btnAllVoluntary.CheckState == CheckState.Checked)
                isAll = true;


            VolontaryFunctionBUS vfb = new VolontaryFunctionBUS();
            personVoluntary1 = vfb.GetVoluntaryDetails(idQueryType, Person.idContPers, isDefaultSort, isAll);

            //Y
            int lastBottom = 60;
            //X
            int left = 26;
            string oldQuest = "";

            if (personVoluntary1 != null)
            {
                if (personVoluntary1.Count > 0)
                {
                    //create dynamic controls
                    for (int i = 0; i < personVoluntary1.Count; i++)
                    {
                        string quest = "";
                        string questText = "";
                        string ans = "";
                        string ansText = "";
                        Boolean ischecked = false;
                        //checkbox width
                        int chkWidth = 150;
                        //radiobutton width
                        int rbWidth = 150;
                        //datetimepicker width
                        int dtWidth = 100;
                        //question label width 
                        int rlWidth = (int)(30 * tabFunction.Width / 100) - 20;
                        //answer width (for all controls)
                        int aWidth = (int)(70 * tabFunction.Width / 100) - 80;
                        //question label height 
                        int rlheight = 30;
                        //answer row height  sa 30 na 20 sale
                        int height = 20;

                        if (personVoluntary1[i].idQuest.ToString() != "")
                        {
                            quest = personVoluntary1[i].idQuest.ToString().TrimEnd();
                        }
                        if (personVoluntary1[i].idAns.ToString() != "")
                        {
                            ans = personVoluntary1[i].idAns.ToString().TrimEnd();
                        }
                        if (personVoluntary1[i].txtQuest.ToString() != "")
                        {
                            questText = personVoluntary1[i].txtQuest.ToString().TrimEnd();
                        }
                        if (personVoluntary1[i].txtAns.ToString() != "")
                        {
                            ansText = personVoluntary1[i].txtAns.ToString().TrimEnd();
                        }
                        if (personVoluntary1[i].idcpr != null)
                        {
                            ischecked = true;
                        }

                        //question label
                        RadLabel rl = new RadLabel();

                        if (quest != oldQuest)
                        {
                            rl.Text = questText;
                            rl.Location = new Point(left, lastBottom);
                            rl.Font = new Font("Verdana", 9);
                            //set multi lines
                            rl.MaximumSize = new Size(rlWidth, height * 3);
                            rl.AutoSize = true;
                            //
                            rl.Width = rlWidth;
                            tabFunction.Controls.Add(rl);
                            oldQuest = quest;

                            //set rows height depends on number of lines in label question
                            rlheight = height * numberOfLines(rl);
                        }

                        //ANSWER
                        //checkbox type
                        if (personVoluntary1[i].idAnsType.ToString() == "1")
                        {
                            RadCheckBox chk = new RadCheckBox();
                            chk.Name = "Q" + quest + "A" + ans;
                            chk.Text = ansText;
                            chk.Width = aWidth;
                            chk.Height = height;
                            chk.Font = new Font("Verdana", 9);
                            chk.Location = new Point(left + rlWidth + 20, lastBottom);
                            chk.CheckStateChanged += radCheckBoxVH_CheckStateChanged;
                            if (ischecked == true)
                            {
                                chk.CheckState = CheckState.Checked;
                            }
                            tabFunction.Controls.Add(chk);

                        }
                        //radio button type
                        else if (personVoluntary1[i].idAnsType.ToString() == "2")
                        {
                            RadRadioButton rb = new RadRadioButton();
                            rb.Name = "Q" + quest + "A" + ans;
                            rb.Text = ansText;
                            rb.MaximumSize = new Size(400, 0);
                            rb.MinimumSize = new Size(40, 0);
                            rb.AutoSize = true;
                            rb.Width = rbWidth;
                            rb.Height = height;
                            rb.Font = new Font("Verdana", 9);
                            rb.CheckStateChanged += radRadioButtonVH_CheckStateChanged;
                            if (ischecked == true)
                            {
                                rb.CheckState = CheckState.Checked;
                            }

                            RadGroupBox rgb = new RadGroupBox();
                            rgb.MaximumSize = new Size(400, 0);
                            rgb.MinimumSize = new Size(40, 0);
                            rgb.AutoSize = true;
                            if (tabFunction.Controls.Find("Group" + quest, true).Length > 0)
                            {
                                rgb = (RadGroupBox)tabFunction.Controls.Find("Group" + quest, true)[0];
                                rgb.Height = lastBottom - rgb.Location.Y + height;
                                if (rb.Width > rgb.Width)
                                    rgb.Width = rb.Width;
                            }
                            else
                            {
                                rgb.Name = "Group" + quest;
                                ((Telerik.WinControls.Primitives.FillPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).InnerColor = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).ForeColor = System.Drawing.Color.Transparent;
                                tabFunction.Controls.Add(rgb);
                                rgb.Width = rbWidth;
                                rgb.Height = height;
                                rgb.Font = new Font("Verdana", 9);
                                rgb.Location = new Point(left + rlWidth + 20, lastBottom);
                                rgb.Width = rb.Width;
                            }

                            rb.Location = new Point(0, lastBottom - rgb.Location.Y);
                            rgb.Controls.Add(rb);
                        }
                        //checkbox + textbox type
                        else if (personVoluntary1[i].idAnsType.ToString() == "3")
                        {

                            RadCheckBox chk = new RadCheckBox();
                            chk.Name = "Q" + quest + "A" + ans;
                            chk.Text = ansText;
                            chk.MaximumSize = new Size(400, 0);
                            chk.MinimumSize = new Size(40, 0);
                            chk.AutoSize = true;
                            chk.Width = chkWidth;
                            chk.Height = height;
                            chk.Font = new Font("Verdana", 9);
                            chk.Location = new Point(left + rlWidth + 20, lastBottom);
                            chk.CheckStateChanged += radCheckBoxVH_CheckStateChanged;
                            tabFunction.Controls.Add(chk);
                            RadTextBox rtb = new RadTextBox();
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Width = aWidth - chk.Width - 20;
                            rtb.Height = height;
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Location = new Point(left + rlWidth + 20 + chk.Width + 20, lastBottom);
                            rtb.TextChanged += txtVH_TextChanged;
                            if (ischecked == true)
                            {
                                chk.CheckState = CheckState.Checked;
                                if (personVoluntary1[i].txt != null)
                                {
                                    rtb.Text = personVoluntary1[i].txt.ToString();
                                }

                            }
                            tabFunction.Controls.Add(rtb);
                        }
                        //textbox type
                        else if (personVoluntary1[i].idAnsType.ToString() == "4")
                        {
                            RadTextBox rtb = new RadTextBox();
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Width = aWidth;
                            rtb.Height = height;
                            rtb.Location = new Point(left + rlWidth + 20, lastBottom);
                            rtb.TextChanged += txtVH_TextChanged;
                            if (ischecked == true)
                            {
                                if (personVoluntary1[i].txt != null)
                                {
                                    rtb.Text = personVoluntary1[i].txt.ToString();
                                }
                            }
                            tabFunction.Controls.Add(rtb);
                        }
                        //radio button + textbox type
                        else if (personVoluntary1[i].idAnsType.ToString() == "5")
                        {
                            RadRadioButton rb = new RadRadioButton();
                            rb.CheckStateChanged += radRadioButtonVH_CheckStateChanged;
                            rb.Name = "Q" + quest + "A" + ans;
                            rb.Text = ansText;
                            rb.MaximumSize = new Size(400, 0);
                            rb.MinimumSize = new Size(40, 0);
                            rb.AutoSize = true;
                            rb.Width = rbWidth;
                            rb.Height = height;
                            rb.Font = new Font("Verdana", 9);

                            if (ischecked == true)
                            {
                                rb.CheckState = CheckState.Checked;
                            }

                            RadGroupBox rgb = new RadGroupBox();
                            rgb.MaximumSize = new Size(400, 0);
                            rgb.MinimumSize = new Size(40, 0);
                            rgb.AutoSize = true;
                            if (tabFunction.Controls.Find("Group" + quest, true).Length > 0)
                            {
                                rgb = (RadGroupBox)tabFunction.Controls.Find("Group" + quest, true)[0];
                                rgb.Height = lastBottom - rgb.Location.Y + height;
                            }
                            else
                            {
                                rgb.Name = "Group" + quest;
                                ((Telerik.WinControls.Primitives.FillPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).InnerColor = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).ForeColor = System.Drawing.Color.Transparent;
                                tabFunction.Controls.Add(rgb);
                                rgb.Width = rbWidth;
                                rgb.Height = height;
                                rgb.Font = new Font("Verdana", 9);
                                rgb.Location = new Point(left + rlWidth + 20, lastBottom);
                            }

                            rb.Location = new Point(0, lastBottom - rgb.Location.Y);
                            //rgb.Width = 50;
                            rgb.Controls.Add(rb);

                            if (tabFunction.Controls.Find("Group" + quest, true).Length > 0)
                            {
                                if (rb.Width > rgb.Width)
                                    rgb.Width = rb.Width;
                            }
                            else
                            {
                                rgb.Width = rb.Width;
                            }

                            RadTextBox rtb = new RadTextBox();
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Width = aWidth - rb.Width - 20;
                            rtb.Height = height;
                            rtb.Location = new Point(left + rlWidth + 20 + rb.Width + 20, lastBottom);
                            rtb.TextChanged += txtVH_TextChanged;
                            if (ischecked == true)
                            {
                                rb.CheckState = CheckState.Checked;
                                if (personVoluntary1[i].txt != null)
                                {
                                    rtb.Text = personVoluntary1[i].txt.ToString();
                                }

                            }
                            tabFunction.Controls.Add(rtb);
                        }
                        //checkbox + textbox + datetime
                        else if (personVoluntary1[i].idAnsType.ToString() == "6")
                        {

                        }

                        lastBottom = lastBottom + rlheight + 5;
                    }
                }
            }
            isVolChanged = false;
        }

        private void firstPartVoluntaryThird()
        {
            tabTrips.Controls.Clear();

        }

        private void loadOnVoluntaryThird()
        {
            Cursor.Current = Cursors.WaitCursor;
            firstPartVoluntaryThird();

            RadToggleButton btnAllVoluntary = new RadToggleButton();
            btnAllVoluntary.CheckState = CheckState.Checked;
            btnAllVoluntary.ThemeName = pagePerson.ThemeName;
            btnAllVoluntary.Name = "btnAllVoluntary";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("Filled data") != null)
                    btnAllVoluntary.Text = resxSet.GetString("Filled data");
                else
                    btnAllVoluntary.Text = "Filled data";
            }
            btnAllVoluntary.Location = new Point(26, 10);
            btnAllVoluntary.Font = new Font("Verdana", 9);
            btnAllVoluntary.CheckStateChanged += btnVoluntaryTripAll_Click;

            RadToggleButton btnSortVoluntary = new RadToggleButton();
            btnSortVoluntary.CheckState = CheckState.Checked;
            btnSortVoluntary.ThemeName = pagePerson.ThemeName;
            btnSortVoluntary.Width = 200;
            btnSortVoluntary.Name = "btnSortVoluntary";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("Alfabetical sort") != null)
                    btnSortVoluntary.Text = resxSet.GetString("Alfabetical sort");
                else
                    btnSortVoluntary.Text = "Alfabetical sort";
            }
            btnSortVoluntary.Location = new Point(250, 10);
            btnSortVoluntary.Font = new Font("Verdana", 9);
            btnSortVoluntary.CheckStateChanged += btnVoluntaryTripSort_Click;

            tabTrips.Controls.Add(btnAllVoluntary);
            tabTrips.Controls.Add(btnSortVoluntary);

            secondPartVoluntaryTrip();

            Cursor.Current = Cursors.Default;

        }
        //==========treci tab       
        private void secondPartVoluntaryTrip()
        {
            List<string> idQueryType = new List<string>();

            foreach (Control c in panelLabels.Controls)
            {
                RadCheckBox rchk = (RadCheckBox)c;
                if (rchk.Checked == true)
                    idQueryType.Add(rchk.Name.Replace("chkLabel", ""));
            }

            RadToggleButton btnSortVoluntary = (RadToggleButton)tabTrips.Controls.Find("btnSortVoluntary", true)[0];
            RadToggleButton btnAllVoluntary = (RadToggleButton)tabTrips.Controls.Find("btnAllVoluntary", true)[0];

            Boolean isDefaultSort = false;

            if (btnSortVoluntary.CheckState == CheckState.Checked)
                isDefaultSort = true;

            //  Boolean isAll = false;
            Boolean isAll = false;

            if (btnAllVoluntary.CheckState == CheckState.Checked)
                isAll = true;


            VolontaryTripBUS vtb = new VolontaryTripBUS();
            personVoluntary2 = vtb.GetVoluntaryDetails(idQueryType, Person.idContPers, isDefaultSort, isAll);

            //Y
            int lastBottom = 60;
            //X
            int left = 26;
            string oldQuest = "";

            string oldQuestGroup = "";  // Saki

            if (personVoluntary2 != null)
            {
                if (personVoluntary2.Count > 0)
                {
                    //create dynamic controls
                    for (int i = 0; i < personVoluntary2.Count; i++)
                    {
                        string questGroup = ""; // saki
                        string quest = "";
                        string questText = "";
                        string ans = "";
                        string ansText = "";
                        Boolean ischecked = false;
                        //checkbox width
                        int chkWidth = 150;
                        //radiobutton width
                        int rbWidth = 150;
                        //datetimepicker width
                        int dtWidth = 100;
                        //question label width 
                        int rlWidth = (int)(30 * tabTrips.Width / 100) - 20;
                        //answer width (for all controls)
                        int aWidth = (int)(70 * tabTrips.Width / 100) - 80;
                        //question label height 
                        int rlheight = 30;
                        //answer row height  sa 30 na 20 sale
                        int height = 20;

                        if (personVoluntary2[i].idQuest.ToString() != "")
                        {
                            quest = personVoluntary2[i].idQuest.ToString().TrimEnd();
                        }
                        if (personVoluntary2[i].idAns.ToString() != "")
                        {
                            ans = personVoluntary2[i].idAns.ToString().TrimEnd();
                        }
                        if (personVoluntary2[i].txtQuest.ToString() != "")
                        {
                            questText = personVoluntary2[i].txtQuest.ToString().TrimEnd();
                        }
                        if (personVoluntary2[i].txtAns.ToString() != "")
                        {
                            ansText = personVoluntary2[i].txtAns.ToString().TrimEnd();
                        }
                        if (personVoluntary2[i].idcpr != null)
                        {
                            ischecked = true;
                        }


                        //====== Saki
                        questGroup = personVoluntary2[i].nameQuestGroup.TrimEnd();
                        //==========
                        quest = personVoluntary2[i].idQuest.ToString().TrimEnd();
                        if (personVoluntary2[i].questSort != null && personVoluntary2[i].questSort != 0)
                            questSort = Convert.ToDecimal(personVoluntary2[i].questSort.ToString());
                        if (personVoluntary2[i].idAns.ToString() != "")
                        {
                            ans = personVoluntary2[i].idAns.ToString().TrimEnd();
                        }
                        questText = personVoluntary2[i].txtQuest.ToString().TrimEnd();

                        ansText = personVoluntary2[i].txtAns.ToString().TrimEnd();
                        if (personVoluntary2[i].idcpr.ToString() != "")
                        {
                            ischecked = true;
                        }


                        //question label
                        RadLabel rl = new RadLabel();
                        RadLabel rlTitle = new RadLabel();  // Saki


                        // ============  ova dva ifa su umesto ORIGINALA ============================
                        if (questGroup.ToUpper() != oldQuestGroup.ToUpper())
                        {
                            rlTitle.Text = questGroup.ToUpper();   //==== Saki
                            rlTitle.Location = new Point(left, lastBottom);
                            rlTitle.Font = new Font("Verdana", 9);
                            rlTitle.ForeColor = Color.DarkOrange;
                            //set multi lines
                            rlTitle.MaximumSize = new Size(rlWidth, height * 3);
                            rlTitle.AutoSize = true;
                            //
                            rlTitle.Width = rlWidth;
                            tabTrips.Controls.Add(rlTitle);
                            //oldQuest = quest;

                            //set rows height depends on number of lines in label question
                            rlheight = height * numberOfLines(rlTitle);
                            lastBottom = lastBottom + rlheight + 5;
                            oldQuestGroup = questGroup;
                        }

                        if (quest != oldQuest)
                        {
                            string s = questSort.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                            string[] parts = s.Split('.');
                            int i1 = int.Parse(parts[0]);
                            int i2 = int.Parse(parts[1]);

                            rl.Text = i1.ToString() + "-" + i2.ToString() + "  " + questText;
                            rl.Location = new Point(left, lastBottom);
                            rl.Font = new Font("Verdana", 9);
                            //set multi lines
                            rl.MaximumSize = new Size(rlWidth, height * 3);
                            rl.AutoSize = true;
                            //
                            rl.Width = rlWidth;
                            tabTrips.Controls.Add(rl);
                            oldQuest = quest;

                            //set rows height depends on number of lines in label question
                            rlheight = height * numberOfLines(rl);
                        }

                        //ANSWER
                        //checkbox type
                        if (personVoluntary2[i].idAnsType.ToString() == "1")
                        {
                            RadCheckBox chk = new RadCheckBox();
                            chk.Name = "Q" + quest + "A" + ans;
                            chk.Text = ansText;
                            chk.Width = aWidth;
                            chk.Height = height;
                            chk.Font = new Font("Verdana", 9);
                            chk.Location = new Point(left + rlWidth + 20, lastBottom);
                            chk.CheckStateChanged += radCheckBoxVH_CheckStateChanged;
                            if (ischecked == true)
                            {
                                chk.CheckState = CheckState.Checked;
                            }
                            tabTrips.Controls.Add(chk);

                        }
                        //radio button type
                        else if (personVoluntary2[i].idAnsType.ToString() == "2")
                        {
                            RadRadioButton rb = new RadRadioButton();
                            rb.Name = "Q" + quest + "A" + ans;
                            rb.Text = ansText;
                            rb.MaximumSize = new Size(400, 0);
                            rb.MinimumSize = new Size(40, 0);
                            rb.AutoSize = true;
                            rb.Width = rbWidth;
                            rb.Height = height;
                            rb.Font = new Font("Verdana", 9);
                            rb.CheckStateChanged += radRadioButtonVH_CheckStateChanged;
                            if (ischecked == true)
                            {
                                rb.CheckState = CheckState.Checked;
                            }

                            RadGroupBox rgb = new RadGroupBox();
                            rgb.MaximumSize = new Size(400, 0);
                            rgb.MinimumSize = new Size(40, 0);
                            rgb.AutoSize = true;
                            if (tabTrips.Controls.Find("Group" + quest, true).Length > 0)
                            {
                                rgb = (RadGroupBox)tabTrips.Controls.Find("Group" + quest, true)[0];
                                rgb.Height = lastBottom - rgb.Location.Y + height;
                                if (rb.Width > rgb.Width)
                                    rgb.Width = rb.Width;
                            }
                            else
                            {
                                rgb.Name = "Group" + quest;
                                ((Telerik.WinControls.Primitives.FillPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).InnerColor = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).ForeColor = System.Drawing.Color.Transparent;
                                tabTrips.Controls.Add(rgb);
                                rgb.Width = rbWidth;
                                rgb.Height = height;
                                rgb.Font = new Font("Verdana", 9);
                                rgb.Location = new Point(left + rlWidth + 20, lastBottom);
                                rgb.Width = rb.Width;
                            }

                            rb.Location = new Point(0, lastBottom - rgb.Location.Y);
                            rgb.Controls.Add(rb);
                        }
                        //checkbox + textbox type
                        else if (personVoluntary2[i].idAnsType.ToString() == "3")
                        {

                            RadCheckBox chk = new RadCheckBox();
                            chk.Name = "Q" + quest + "A" + ans;
                            chk.Text = ansText;
                            chk.MaximumSize = new Size(400, 0);
                            chk.MinimumSize = new Size(40, 0);
                            chk.AutoSize = true;
                            chk.Width = chkWidth;
                            chk.Height = height;
                            chk.Font = new Font("Verdana", 9);
                            chk.Location = new Point(left + rlWidth + 20, lastBottom);
                            chk.CheckStateChanged += radCheckBoxVH_CheckStateChanged;
                            tabTrips.Controls.Add(chk);
                            RadTextBox rtb = new RadTextBox();
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Width = aWidth - chk.Width - 20;
                            rtb.Height = height;
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Location = new Point(left + rlWidth + 20 + chk.Width + 20, lastBottom);
                            rtb.TextChanged += txtVH_TextChanged;
                            if (ischecked == true)
                            {
                                chk.CheckState = CheckState.Checked;
                                if (personVoluntary2[i].txt != null)
                                {
                                    rtb.Text = personVoluntary2[i].txt.ToString();
                                }

                            }
                            tabTrips.Controls.Add(rtb);
                        }
                        //textbox type
                        else if (personVoluntary2[i].idAnsType.ToString() == "4")
                        {
                            RadTextBox rtb = new RadTextBox();
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Width = aWidth;
                            rtb.Height = height;
                            rtb.Location = new Point(left + rlWidth + 20, lastBottom);
                            rtb.TextChanged += txtVH_TextChanged;
                            if (ischecked == true)
                            {
                                if (personVoluntary2[i].txt != null)
                                {
                                    rtb.Text = personVoluntary2[i].txt.ToString();
                                }
                            }
                            tabTrips.Controls.Add(rtb);
                        }
                        //radio button + textbox type
                        else if (personVoluntary2[i].idAnsType.ToString() == "5")
                        {
                            RadRadioButton rb = new RadRadioButton();
                            rb.CheckStateChanged += radRadioButtonVH_CheckStateChanged;
                            rb.Name = "Q" + quest + "A" + ans;
                            rb.Text = ansText;
                            rb.MaximumSize = new Size(400, 0);
                            rb.MinimumSize = new Size(40, 0);
                            rb.AutoSize = true;
                            rb.Width = rbWidth;
                            rb.Height = height;
                            rb.Font = new Font("Verdana", 9);

                            if (ischecked == true)
                            {
                                rb.CheckState = CheckState.Checked;
                            }

                            RadGroupBox rgb = new RadGroupBox();
                            rgb.MaximumSize = new Size(400, 0);
                            rgb.MinimumSize = new Size(40, 0);
                            rgb.AutoSize = true;
                            if (tabTrips.Controls.Find("Group" + quest, true).Length > 0)
                            {
                                rgb = (RadGroupBox)tabTrips.Controls.Find("Group" + quest, true)[0];
                                rgb.Height = lastBottom - rgb.Location.Y + height;
                            }
                            else
                            {
                                rgb.Name = "Group" + quest;
                                ((Telerik.WinControls.Primitives.FillPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).InnerColor = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).ForeColor = System.Drawing.Color.Transparent;
                                tabTrips.Controls.Add(rgb);
                                rgb.Width = rbWidth;
                                rgb.Height = height;
                                rgb.Font = new Font("Verdana", 9);
                                rgb.Location = new Point(left + rlWidth + 20, lastBottom);
                            }

                            rb.Location = new Point(0, lastBottom - rgb.Location.Y);
                            //rgb.Width = 50;
                            rgb.Controls.Add(rb);

                            if (tabTrips.Controls.Find("Group" + quest, true).Length > 0)
                            {
                                if (rb.Width > rgb.Width)
                                    rgb.Width = rb.Width;
                            }
                            else
                            {
                                rgb.Width = rb.Width;
                            }

                            RadTextBox rtb = new RadTextBox();
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Width = aWidth - rb.Width - 20;
                            rtb.Height = height;
                            rtb.Location = new Point(left + rlWidth + 20 + rb.Width + 20, lastBottom);
                            rtb.TextChanged += txtVH_TextChanged;
                            if (ischecked == true)
                            {
                                rb.CheckState = CheckState.Checked;
                                if (personVoluntary2[i].txt != null)
                                {
                                    rtb.Text = personVoluntary2[i].txt.ToString();
                                }

                            }
                            tabTrips.Controls.Add(rtb);
                        }
                        //checkbox + textbox + datetime
                        else if (personVoluntary2[i].idAnsType.ToString() == "6")
                        {

                        }

                        lastBottom = lastBottom + rlheight + 5;
                    }
                }
            }
            isVolChanged = false;
        }

        private void btnVoluntarySkillAll_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (Person.idContPers != null && Person.idContPers != 0)
            {
                //VolontaryFunctionBUS vfb = new VolontaryFunctionBUS();
                MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
                saveVoluntaryFunction();
                if (isVolChanged == true)
                {
                    if (mvb.Delete(Person.idContPers, this.Name, Login._user.idUser) == true)      //(vfb.Delete(Person.idContPers) == true)
                    {
                        for (int j = 0; j < personVoluntary.Count; j++)   //(int j = 0; j < personVoluntary1.Count; j++)
                        {
                            if (mvb.Save(personVoluntary[j], this.Name, Login._user.idUser) == false)     //(vfb.Save(personVoluntary1[j]) == false)
                            {
                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                {
                                    if (resxSet.GetString("You have not succesufully inserted voluntary data. Please check!") != null)
                                        RadMessageBox.Show(resxSet.GetString("You have not succesufully inserted voluntary data. Please check!"));
                                    else
                                        RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                                }
                            }

                        }
                    }
                    else
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("You have not succesufully inserted voluntary data. Please check!") != null)
                                RadMessageBox.Show(resxSet.GetString("You have not succesufully inserted voluntary data. Please check!"));
                            else
                                RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                        }
                    }
                }


                RadToggleButton btnAllVoluntary = (RadToggleButton)tabFunction.Controls.Find("btnAllVoluntary", true)[0];

                if (btnAllVoluntary.CheckState == CheckState.Checked)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Filled data") != null)
                            btnAllVoluntary.Text = resxSet.GetString("Filled data");
                        else
                            btnAllVoluntary.Text = "Filled data";
                    }
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("All data") != null)
                            btnAllVoluntary.Text = resxSet.GetString("All data");
                        else
                            btnAllVoluntary.Text = "All data";
                    }

                }


                RadToggleButton btnSortVoluntary = (RadToggleButton)tabFunction.Controls.Find("btnSortVoluntary", true)[0];

                if (btnSortVoluntary.CheckState == CheckState.Checked)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Alfabetical sort") != null)
                            btnSortVoluntary.Text = resxSet.GetString("Alfabetical sort");
                        else
                            btnSortVoluntary.Text = "Alfabetical sort";
                    }
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Default sort") != null)
                            btnSortVoluntary.Text = resxSet.GetString("Default sort");
                        else
                            btnSortVoluntary.Text = "Default sort";
                    }
                }

                //firstPartVoluntarySectab();
                //tabFunction.Controls.Add(btnAllVoluntary);   //
                //tabFunction.Controls.Add(btnSortVoluntary);   //
                //secondPartVoluntaryFunction();

                firstPartVoluntary();
                tabSkils.Controls.Add(btnAllVoluntary);   //
                tabSkils.Controls.Add(btnSortVoluntary);   //
                secondPartVoluntary();

                Cursor.Current = Cursors.Default;
            }
        }

        private void btnVoluntaryFunctionAll_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (Person.idContPers != null && Person.idContPers != 0)
            {
                VolontaryFunctionBUS vtb = new VolontaryFunctionBUS();
                saveVoluntaryFunction();
                if (isVolChanged == true)
                {
                    if (vtb.Delete(Person.idContPers, this.Name, Login._user.idUser) == true)
                    {
                        for (int j = 0; j < personVoluntary1.Count; j++)
                        {
                            if (vtb.Save(personVoluntary1[j], this.Name, Login._user.idUser) == false)
                            {
                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                {
                                    if (resxSet.GetString("You have not succesufully inserted voluntary data. Please check!") != null)
                                        RadMessageBox.Show(resxSet.GetString("You have not succesufully inserted voluntary data. Please check!"));
                                    else
                                        RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                                }
                            }

                        }
                    }
                    else
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("You have not succesufully inserted voluntary data. Please check!") != null)
                                RadMessageBox.Show(resxSet.GetString("You have not succesufully inserted voluntary data. Please check!"));
                            else
                                RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                        }
                    }
                }


                RadToggleButton btnAllVoluntary = (RadToggleButton)tabFunction.Controls.Find("btnAllVoluntary", true)[0];

                if (btnAllVoluntary.CheckState == CheckState.Checked)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Filled data") != null)
                            btnAllVoluntary.Text = resxSet.GetString("Filled data");
                        else
                            btnAllVoluntary.Text = "Filled data";
                    }
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("All data") != null)
                            btnAllVoluntary.Text = resxSet.GetString("All data");
                        else
                            btnAllVoluntary.Text = "All data";
                    }

                }


                RadToggleButton btnSortVoluntary = (RadToggleButton)tabFunction.Controls.Find("btnSortVoluntary", true)[0];

                if (btnSortVoluntary.CheckState == CheckState.Checked)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Alfabetical sort") != null)
                            btnSortVoluntary.Text = resxSet.GetString("Alfabetical sort");
                        else
                            btnSortVoluntary.Text = "Alfabetical sort";
                    }
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Default sort") != null)
                            btnSortVoluntary.Text = resxSet.GetString("Default sort");
                        else
                            btnSortVoluntary.Text = "Default sort";
                    }
                }

                //firstPartVoluntaryThird();
                //tabFunction.Controls.Add(btnAllVoluntary);   //
                //tabFunction.Controls.Add(btnSortVoluntary);   //
                //secondPartVoluntaryTrip();


                firstPartVoluntarySectab();
                tabFunction.Controls.Add(btnAllVoluntary);   //
                tabFunction.Controls.Add(btnSortVoluntary);   //
                secondPartVoluntaryFunction();

                Cursor.Current = Cursors.Default;
            }
        }

        private void btnVoluntaryFunctionSort_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (Person.idContPers != null && Person.idContPers != 0)
            {
                VolontaryFunctionBUS mvb = new VolontaryFunctionBUS();
                saveVoluntaryFunction();
                if (isVolChanged == true)
                {
                    if (mvb.Delete(Person.idContPers, this.Name, Login._user.idUser) == true)
                    {
                        for (int j = 0; j < personVoluntary1.Count; j++)
                        {
                            if (mvb.Save(personVoluntary1[j], this.Name, Login._user.idUser) == false)    // 
                            {
                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                {
                                    if (resxSet.GetString("You have not succesufully inserted voluntary data. Please check!") != null)
                                        RadMessageBox.Show(resxSet.GetString("You have not succesufully inserted voluntary data. Please check!"));
                                    else
                                        RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                                }
                            }
                        }
                    }
                    else
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("You have not succesufully inserted voluntary data. Please check!") != null)
                                RadMessageBox.Show(resxSet.GetString("You have not succesufully inserted voluntary data. Please check!"));
                            else
                                RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                        }
                    }
                }
            }


            RadToggleButton btnAllVoluntary = (RadToggleButton)tabFunction.Controls.Find("btnAllVoluntary", true)[0];

            if (btnAllVoluntary.CheckState == CheckState.Checked)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Filled data") != null)
                        btnAllVoluntary.Text = resxSet.GetString("Filled data");
                    else
                        btnAllVoluntary.Text = "Filled data";
                }
            }
            else
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("All data") != null)
                        btnAllVoluntary.Text = resxSet.GetString("All data");
                    else
                        btnAllVoluntary.Text = "All data";
                }

            }


            RadToggleButton btnSortVoluntary = (RadToggleButton)tabFunction.Controls.Find("btnSortVoluntary", true)[0];

            if (btnSortVoluntary.CheckState == CheckState.Checked)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Alfabetical sort") != null)
                        btnSortVoluntary.Text = resxSet.GetString("Alfabetical sort");
                    else
                        btnSortVoluntary.Text = "Alfabetical sort";
                }
            }
            else
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Default sort") != null)
                        btnSortVoluntary.Text = resxSet.GetString("Default sort");
                    else
                        btnSortVoluntary.Text = "Default sort";
                }
            }
            firstPartVoluntarySectab();
            tabFunction.Controls.Add(btnAllVoluntary);
            tabFunction.Controls.Add(btnSortVoluntary);
            secondPartVoluntaryFunction();

            Cursor.Current = Cursors.Default;
        }

        private void btnVoluntaryTripAll_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (Person.idContPers != null && Person.idContPers != 0)
            {
                VolontaryTripBUS vtb = new VolontaryTripBUS();
                saveVoluntaryTrip();
                if (isVolChanged == true)
                {
                    if (vtb.Delete(Person.idContPers, this.Name, Login._user.idUser) == true)
                    {
                        for (int j = 0; j < personVoluntary2.Count; j++)
                        {
                            if (vtb.Save(personVoluntary2[j], this.Name, Login._user.idUser) == false)
                            {
                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                {
                                    if (resxSet.GetString("You have not succesufully inserted voluntary data. Please check!") != null)
                                        RadMessageBox.Show(resxSet.GetString("You have not succesufully inserted voluntary data. Please check!"));
                                    else
                                        RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                                }
                            }

                        }
                    }
                    else
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("You have not succesufully inserted voluntary data. Please check!") != null)
                                RadMessageBox.Show(resxSet.GetString("You have not succesufully inserted voluntary data. Please check!"));
                            else
                                RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                        }
                    }
                }


                RadToggleButton btnAllVoluntary = (RadToggleButton)tabTrips.Controls.Find("btnAllVoluntary", true)[0];

                if (btnAllVoluntary.CheckState == CheckState.Checked)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Filled data") != null)
                            btnAllVoluntary.Text = resxSet.GetString("Filled data");
                        else
                            btnAllVoluntary.Text = "Filled data";
                    }
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("All data") != null)
                            btnAllVoluntary.Text = resxSet.GetString("All data");
                        else
                            btnAllVoluntary.Text = "All data";
                    }

                }


                RadToggleButton btnSortVoluntary = (RadToggleButton)tabTrips.Controls.Find("btnSortVoluntary", true)[0];

                if (btnSortVoluntary.CheckState == CheckState.Checked)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Alfabetical sort") != null)
                            btnSortVoluntary.Text = resxSet.GetString("Alfabetical sort");
                        else
                            btnSortVoluntary.Text = "Alfabetical sort";
                    }
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Default sort") != null)
                            btnSortVoluntary.Text = resxSet.GetString("Default sort");
                        else
                            btnSortVoluntary.Text = "Default sort";
                    }
                }

                firstPartVoluntaryThird();
                tabTrips.Controls.Add(btnAllVoluntary);   //
                tabTrips.Controls.Add(btnSortVoluntary);   //
                secondPartVoluntaryTrip();

                Cursor.Current = Cursors.Default;
            }
        }

        private void btnVoluntaryTripSort_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (Person.idContPers != null && Person.idContPers != 0)
            {
                VolontaryTripBUS mvb = new VolontaryTripBUS();
                saveVoluntaryTrip();
                if (isVolChanged == true)
                {
                    if (mvb.Delete(Person.idContPers, this.Name, Login._user.idUser) == true)
                    {
                        for (int j = 0; j < personVoluntary2.Count; j++)
                        {
                            if (mvb.Save(personVoluntary2[j], this.Name, Login._user.idUser) == false)    // 
                            {
                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                {
                                    if (resxSet.GetString("You have not succesufully inserted voluntary data. Please check!") != null)
                                        RadMessageBox.Show(resxSet.GetString("You have not succesufully inserted voluntary data. Please check!"));
                                    else
                                        RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                                }
                            }
                        }
                    }
                    else
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("You have not succesufully inserted voluntary data. Please check!") != null)
                                RadMessageBox.Show(resxSet.GetString("You have not succesufully inserted voluntary data. Please check!"));
                            else
                                RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                        }
                    }
                }
            }


            RadToggleButton btnAllVoluntary = (RadToggleButton)tabTrips.Controls.Find("btnAllVoluntary", true)[0];

            if (btnAllVoluntary.CheckState == CheckState.Checked)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Filled data") != null)
                        btnAllVoluntary.Text = resxSet.GetString("Filled data");
                    else
                        btnAllVoluntary.Text = "Filled data";
                }
            }
            else
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("All data") != null)
                        btnAllVoluntary.Text = resxSet.GetString("All data");
                    else
                        btnAllVoluntary.Text = "All data";
                }

            }


            RadToggleButton btnSortVoluntary = (RadToggleButton)tabTrips.Controls.Find("btnSortVoluntary", true)[0];

            if (btnSortVoluntary.CheckState == CheckState.Checked)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Alfabetical sort") != null)
                        btnSortVoluntary.Text = resxSet.GetString("Alfabetical sort");
                    else
                        btnSortVoluntary.Text = "Alfabetical sort";
                }
            }
            else
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Default sort") != null)
                        btnSortVoluntary.Text = resxSet.GetString("Default sort");
                    else
                        btnSortVoluntary.Text = "Default sort";
                }
            }
            //firstPartVoluntarySectab();
            //tabFunction.Controls.Add(btnAllVoluntary);
            //tabFunction.Controls.Add(btnSortVoluntary);
            //secondPartVoluntaryFunction();

            firstPartVoluntaryThird();
            tabTrips.Controls.Add(btnAllVoluntary);   //
            tabTrips.Controls.Add(btnSortVoluntary);   //
            secondPartVoluntaryTrip();

            Cursor.Current = Cursors.Default;
        }

        private void btnClientR_Click(object sender, EventArgs e)
        {
            ClientBUS ClientBUS = new ClientBUS();
            List<IModel> km = new List<IModel>();

            km = ClientBUS.GetAllClients(Login._user.lngUser);


            using (var dlgClient = new GridLookupForm(km, "Client"))
            {

                if (dlgClient.ShowDialog(this) == DialogResult.Yes)
                {
                    ClientModel okm = new ClientModel();
                    okm = (ClientModel)dlgClient.selectedRow;
                    txtClientR.Text = okm.nameClient;
                    Person.idClient = okm.idClient;
                    xClientName = okm.nameClient;
                    xidClient = okm.idClient;

                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void ddlLives_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (ddlLives.SelectedIndex == 1)
            {
                txtClientR.Visible = true;
                btnClientR.Visible = true;
                if (xClientName != "")
                {
                    txtClientR.Text = xClientName;
                }
            }
            else
            {
                txtClientR.Visible = false;
                btnClientR.Visible = false;
                // Person.idClient = 0;
                // xClientName = "";
                txtClientR.Text = "";
            }
        }

        private void chkCPerson_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkCPerson.CheckState == CheckState.Checked)
                tabCperson.Item.Visibility = ElementVisibility.Visible;
            else
                tabCperson.Item.Visibility = ElementVisibility.Collapsed;
        }

        private void btnCPerson_Click(object sender, EventArgs e)
        {
            ClientBUS ClientBUS = new ClientBUS();
            List<IModel> km = new List<IModel>();

            km = ClientBUS.GetAllClients(Login._user.lngUser);


            var dlgClient = new GridLookupForm(km, "Client");

            if (dlgClient.ShowDialog(this) == DialogResult.Yes)
            {
                ClientModel okm = new ClientModel();
                okm = (ClientModel)dlgClient.selectedRow;
                txtClient.Text = okm.nameClient;
                Person.idClient = okm.idClient;
                idClientFromClientGrid = okm.idClient; //idClientFromGrid promenljiva za smestanje IdClient-a.
                xClientName = okm.nameClient;
                xidClient = okm.idClient;
            }

        }

        private void chkisPaperByMail_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkisPaperByMail.Checked == true)
                cpa.ShowHidePostAddress(true);
            else
                cpa.ShowHidePostAddress(false);
        }

        private void btnPayCon_Click(object sender, EventArgs e)
        {
            AccPaymentBUS ccentar = new AccPaymentBUS();
            List<IModel> gmX = new List<IModel>();

            gmX = ccentar.GetAllAccPayment();

            using (var dlgSave = new GridLookupForm(gmX, "Pay Conditions"))
            {

                if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                {
                    AccPaymentModel genmX = new AccPaymentModel();
                    genmX = (AccPaymentModel)dlgSave.selectedRow;

                    if (genmX != null)
                    {
                        //set textbox
                        if (genmX.idPayment != null)
                        {
                            txtPayCon.Text = genmX.description;
                            debCre.payCondition = genmX.idPayment;
                        }
                    }
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private bool IsVolontary()
        {
            bool isVolontary = false;
            PersonBUS pb = new PersonBUS();
            List<LastIdModel> lista = new List<LastIdModel>();
            lista = pb.IsPersonVolHelp();
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].ID == Person.idContPers)
                {
                    isVolontary = true;
                    break;
                }
            }
            return isVolontary;
        }

        private void btnArrangement_Click(object sender, EventArgs e)
        {
            if (Person.isActive == true || Person.isDied == true)
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("Person is not active.");
                return;
            }



            PersonBUS PersonBUS = new PersonBUS();
            List<IModel> gm3 = new List<IModel>();


            gm3 = PersonBUS.GetAllArrangement(Person.idContPers);

            bool isVolontary;
            isVolontary = IsVolontary();
            ArrangementBookModel bookModel = new ArrangementBookModel();
            bookModel.idContPers = Person.idContPers;

            using (var dlgSave = new GridLookupAragementForm(gm3, "Arrangment", isVolontary, bookModel))
            {


                if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                {

                    ArrangementModel genm3 = new ArrangementModel();
                    genm3 = (ArrangementModel)dlgSave.selectedRow;
                    int nr = genm3.nrTraveler;

                    PersonBUS pbs = new PersonBUS();
                    //rgvArrangment.DataSource = pbs.GetArrangementsForPerson(Person.idContPers);
                    //rgvArrangment.Columns["countryArrangement"].IsVisible = false;
                    //rgvArrangment.Columns["typeArrangement"].IsVisible = false;
                    //rgvArrangment.Columns["idStatus"].IsVisible = false;
                    //rgvArrangment.Columns["idArrangementBook"].IsVisible = false;
                    //rgvArrangment.Columns["idTravelPapers"].IsVisible = false;
                    //rgvArrangment.Columns["codeArrangement"].IsVisible = false;

                    //rgvArrangment.Columns["nrMaximumWheelchairs"].IsVisible = false;
                    //rgvArrangment.Columns["whoseElectricWheelchairs"].IsVisible = false;
                    //rgvArrangment.Columns["buSupportingArms"].IsVisible = false;
                    //rgvArrangment.Columns["nrAnchorage"].IsVisible = false;
                  
                    rgvArrangment.DataSource = null;
                    PersonBUS pbb = new PersonBUS();
                    rgvArrangment.Columns.Clear();
                    rgvArrangment.DataSource = pbb.GetArrangementsForPerson(Person.idContPers);

                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void rgvArrangment_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {

            if (e.RowIndex != null & e.RowIndex >= 0)
            {
                GridViewRowInfo info = this.rgvArrangment.CurrentRow;

                if (info != null && info.Index >= 0)
                {
                    ArrangementAllModel selectedArrangment = (ArrangementAllModel)info.DataBoundItem;
                    ArrangementBookModel bookModel = new ArrangementBookModel();
                    bookModel.idArrangementBook = selectedArrangment.idArrangementBook;
                    bookModel.idArrangement = selectedArrangment.idArrangement;
                    bookModel.idContPers = Person.idContPers;
                    bookModel.idStatus = selectedArrangment.idStatus;
                    bookModel.idTravelPapers = selectedArrangment.idTravelPapers;
                    bookModel.price = selectedArrangment.price;


                    if (IsVolontary() == true)
                    {
                        using (var newdgl = new frmArrangementBookingPerson_VH(bookModel, false))
                        {
                            newdgl.ShowDialog();
                        }
                    }
                    else
                    {
                        using (var newdgl = new frmArrangementBookingPerson(bookModel, selectedArrangment.nrMaximumWheelchairs, selectedArrangment.whoseElectricWheelchairs, selectedArrangment.buSupportingArms, selectedArrangment.nrAnchorage, false, false))
                        {
                            newdgl.ShowDialog();
                        }
                    }

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }



        }

        private void radMenuItemSaveArrangementLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutArrangement))
            {
                File.Delete(layoutArrangement);
            }
            rgvArrangment.SaveLayout(layoutArrangement);

            MessageBox.Show("Layout Save");
        }

        private void radButtonContactPersonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (clientAndPersonList == null) //ako nije prazan grid dodaje nov slog u grid
                {

                    clientAndPersonList = new BindingList<ClientPersonModel>();

                }

                ClientPersonModel clientAndPersonModel = new ClientPersonModel();
                ClientPersonBUS clientAndPersonBUS = new ClientPersonBUS();
                ClientPersonBUS cpbus = new ClientPersonBUS();
                List<ClientPersonModel> cpmodel = new List<ClientPersonModel>();

                clientAndPersonModel.idCLient = idClientFromClientGrid;

                clientAndPersonModel.idFunction = Convert.ToInt32(ddlCpFunction.SelectedValue);
                //    txtClient.Text.ToString();
                clientAndPersonModel.idContPerson = Person.idContPers;
                ddlCpFunction.SelectedValue.ToString();
                cpmodel = cpbus.GetAllClientsFromPerson(Person.idContPers);
                //=======Ne dozvoljava dodavanje klijenta koji je vec dodat
                if (cpmodel != null)
                {
                    //for (int i = 0; cpmodel.Count < i++; )
                    for (int i = 0; i < cpmodel.Count; i++)
                    {
                        if (idClientFromClientGrid == cpmodel[i].idCLient)
                        {
                            RadMessageBox.Show("You alredy enter this client");
                            return;
                        }
                    }
                }
                //============================Ne dozvoljava dodavanje klijenta koji je vec dodat


                int idCliPer = clientAndPersonBUS.SaveAndReturnID(clientAndPersonModel, this.Name, Login._user.idUser);
                // clientAndPersonBUS.Save(clientAndPersonModel);
                clientAndPersonModel.idCliPer = idCliPer;


                clientAndPersonList.Add(clientAndPersonModel);
                // refresh grida

                cpmodel = cpbus.GetAllClientsFromPerson(Person.idContPers);
                if (cpmodel != null)
                    clientAndPersonList = new BindingList<ClientPersonModel>(cpmodel);

                radGridViewContactPerson.DataSource = clientAndPersonList;


            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex.Message);
            }
        }

        private void radGridViewContactPerson_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < radGridViewContactPerson.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (radGridViewContactPerson.Columns[i].HeaderText != null && resxSet.GetString(radGridViewContactPerson.Columns[i].HeaderText) != null)
                        radGridViewContactPerson.Columns[i].HeaderText = resxSet.GetString(radGridViewContactPerson.Columns[i].HeaderText);
                }
            }


        }

        private void radGridViewContactPerson_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {
            translateRadMessageBox msgbox = new translateRadMessageBox();
            DialogResult dr = msgbox.translateAllMessageBoxDialog("Delete entry ?", "Delete");

            if (dr != DialogResult.Yes)
                e.Cancel = true;
            else
            {
                if (radGridViewContactPerson.CurrentRow.DataBoundItem != null)
                {
                    ClientPersonBUS bus = new ClientPersonBUS();
                    ClientPersonModel model = (ClientPersonModel)radGridViewContactPerson.CurrentRow.DataBoundItem;
                    bus.Delete(model.idCliPer, this.Name, Login._user.idUser);
                }
            }
        }
        #region similarities

        private bool bExpiredVOG = false;
        private bool bExpiredCOK = false;
        private bool bExpiredVOK = false;

        private void LoadSimilarities()
        {
            bExpiredVOG = false;
            bExpiredCOK = false;
            bExpiredVOK = false;

            VolSimilarityBUS volbus = new VolSimilarityBUS();
            VolSimilarityModel volmodel = new VolSimilarityModel();

            //VOG
            volmodel = volbus.GetSimilarityById(lblVolSimVOG.Text, Person.idContPers);
            if (volmodel != null)
            {
                chkVolSimVOG.Checked = true;

                if (volmodel.optionSimilarity == "Submited")
                {
                    radioVolSimVOG_submited.CheckState = CheckState.Checked;
                    dateVolSimVOG_effective.Visible = false;
                    dateVolSimVOG_expiration.Visible = false;
                    dateVolSimVOG_sent.Visible = true;
                }
                if (volmodel.optionSimilarity == "Final")
                {
                    radioVolSimVOG_final.CheckState = CheckState.Checked;
                    dateVolSimVOG_sent.Visible = false;
                }

                dateVolSimVOG_effective.Value = (DateTime)volmodel.dtEffectiveDate;
                dateVolSimVOG_expiration.Value = (DateTime)volmodel.dtExpirationDate;
                dateVolSimVOG_sent.Value = (DateTime)volmodel.dtSent;

                //if VOG expired
                if (dateVolSimVOG_expiration.Value < DateTime.Today && volmodel.optionSimilarity == "Final")
                {
                    bExpiredVOG = true;
                    if (dateVolSimVOG_expiration.Value != DateTime.MinValue)
                        lblVolSimVOG_expired.Visible = true;
                    dateVolSimVOG_sent.Visible = false;

                    VolSimilarityArchiveModel model = volbus.GetSimilarityArchiveByDate(lblVolSimVOG.Text, Person.idContPers, dateVolSimVOG_expiration.Value);
                    //ako ne postoji upisi u arhivu
                    if (model == null)
                    {
                        volbus.SaveToArchive(volmodel, this.Name, Login._user.idUser);
                    }
                }
                else
                {
                    lblVolSimVOG_expired.Visible = false;
                    dateVolSimVOG_sent.Visible = true;
                }
            }
            else
            {
                volmodel = new VolSimilarityModel();

                chkVolSimVOG.Checked = false;

                radioVolSimVOG_final.CheckState = CheckState.Unchecked;
                radioVolSimVOG_submited.CheckState = CheckState.Unchecked;
                radioVolSimVOG_final.Enabled = false;
                radioVolSimVOG_submited.Enabled = false;

                dateVolSimVOG_effective.Value = (DateTime)volmodel.dtEffectiveDate;
                dateVolSimVOG_expiration.Value = (DateTime)volmodel.dtExpirationDate;
                dateVolSimVOG_sent.Value = (DateTime)volmodel.dtSent;

                dateVolSimVOG_effective.Visible = false;
                dateVolSimVOG_expiration.Visible = false;
                dateVolSimVOG_sent.Visible = false;


            }

            //COK            
            volmodel = volbus.GetSimilarityById(lblVolSimCOK.Text, Person.idContPers);
            if (volmodel != null)
            {
                chkVolSimCOK.Checked = true;

                if (volmodel.optionSimilarity == "Submited")
                {
                    radioVolSimCOK_submited.CheckState = CheckState.Checked;
                    dateVolSimCOK_effective.Visible = false;
                    dateVolSimCOK_expiration.Visible = false;
                    dateVolSimCOK_sent.Visible = true;
                }
                if (volmodel.optionSimilarity == "Final")
                {
                    radioVolSimCOK_final.CheckState = CheckState.Checked;
                    dateVolSimCOK_sent.Visible = false;
                }

                dateVolSimCOK_effective.Value = (DateTime)volmodel.dtEffectiveDate;
                dateVolSimCOK_expiration.Value = (DateTime)volmodel.dtExpirationDate;
                dateVolSimCOK_sent.Value = (DateTime)volmodel.dtSent;

                //if COK expired
                if (dateVolSimCOK_expiration.Value < DateTime.Today && volmodel.optionSimilarity == "Final")
                {
                    bExpiredCOK = true;
                    if (dateVolSimCOK_expiration.Value != DateTime.MinValue)
                        lblVolSimCOK_expired.Visible = true;
                    dateVolSimCOK_sent.Visible = false;

                    VolSimilarityArchiveModel model = volbus.GetSimilarityArchiveByDate(lblVolSimVOG.Text, Person.idContPers, dateVolSimCOK_expiration.Value);
                    //ako ne postoji upisi u arhivu
                    if (model == null)
                    {
                        volbus.SaveToArchive(volmodel, this.Name, Login._user.idUser);
                    }
                }
                else
                {
                    lblVolSimCOK_expired.Visible = false;
                    dateVolSimCOK_sent.Visible = true;
                }

            }
            else
            {
                volmodel = new VolSimilarityModel();

                chkVolSimCOK.Checked = false;

                radioVolSimCOK_submited.CheckState = CheckState.Unchecked;
                radioVolSimCOK_final.CheckState = CheckState.Unchecked;
                radioVolSimCOK_submited.Enabled = false;
                radioVolSimCOK_final.Enabled = false;

                dateVolSimCOK_effective.Value = (DateTime)volmodel.dtEffectiveDate;
                dateVolSimCOK_expiration.Value = (DateTime)volmodel.dtExpirationDate;
                dateVolSimCOK_sent.Value = (DateTime)volmodel.dtSent;

                dateVolSimCOK_effective.Visible = false;
                dateVolSimCOK_expiration.Visible = false;
                dateVolSimCOK_sent.Visible = false;
            }

            //VOK
            volmodel = volbus.GetSimilarityById(lblVolSimVOK.Text, Person.idContPers);
            if (volmodel != null)
            {
                chkVolSimVOK.Checked = true;

                if (volmodel.optionSimilarity == "Submited")
                {
                    radioVolSimVOK_submited.CheckState = CheckState.Checked;
                    dateVolSimVOK_effective.Visible = false;
                    dateVolSimVOK_expiration.Visible = false;
                    dateVolSimVOK_sent.Visible = true;
                }
                if (volmodel.optionSimilarity == "Final")
                {
                    radioVolSimVOK_final.CheckState = CheckState.Checked;
                    dateVolSimVOK_sent.Visible = false;
                }

                dateVolSimVOK_effective.Value = (DateTime)volmodel.dtEffectiveDate;
                dateVolSimVOK_expiration.Value = (DateTime)volmodel.dtExpirationDate;
                dateVolSimVOK_sent.Value = (DateTime)volmodel.dtSent;

                //if VOK expired
                if (dateVolSimVOK_expiration.Value < DateTime.Today && volmodel.optionSimilarity == "Final")
                {
                    bExpiredVOK = true;
                    if (dateVolSimVOK_expiration.Value != DateTime.MinValue)
                        lblVolSimVOK_expired.Visible = true;
                    dateVolSimVOK_sent.Visible = false;

                    VolSimilarityArchiveModel model = volbus.GetSimilarityArchiveByDate(lblVolSimVOG.Text, Person.idContPers, dateVolSimVOK_expiration.Value);
                    //ako ne postoji upisi u arhivu
                    if (model == null)
                    {
                        volbus.SaveToArchive(volmodel, this.Name, Login._user.idUser);
                    }
                }
                else
                {
                    lblVolSimVOK_expired.Visible = false;
                    dateVolSimVOK_sent.Visible = true;
                }


            }
            else
            {
                volmodel = new VolSimilarityModel();

                chkVolSimVOK.Checked = false;

                radioVolSimVOK_final.CheckState = CheckState.Unchecked;
                radioVolSimVOK_submited.CheckState = CheckState.Unchecked;

                radioVolSimVOK_final.Enabled = false;
                radioVolSimVOK_submited.Enabled = false;

                dateVolSimVOK_effective.Value = (DateTime)volmodel.dtEffectiveDate;
                dateVolSimVOK_expiration.Value = (DateTime)volmodel.dtExpirationDate;
                dateVolSimVOK_sent.Value = (DateTime)volmodel.dtSent;

                dateVolSimVOK_effective.Visible = false;
                dateVolSimVOK_expiration.Visible = false;
                dateVolSimVOK_sent.Visible = false;

            }
        }

        public void SaveSimilarities()
        {
            List<VolSimilarityModel> modelDelete = new List<VolSimilarityModel>();
            List<VolSimilarityModel> modelInsert = new List<VolSimilarityModel>();
            VolSimilarityModel model = new VolSimilarityModel();

            //VOG
            if (chkVolSimVOG.CheckState == CheckState.Checked)
            {
                model.idSimilarity = lblVolSimVOG.Text;
                model.idContPers = Person.idContPers;

                if (radioVolSimVOG_final.CheckState == CheckState.Checked)
                {
                    model.optionSimilarity = "Final";
                }
                else if (radioVolSimVOG_submited.CheckState == CheckState.Checked)
                {
                    model.optionSimilarity = "Submited";
                }
                else
                {
                    translateRadMessageBox msgbox = new translateRadMessageBox();
                    msgbox.translateAllMessageBox("Submited or Final must be selected in order to save.");
                    return;

                }
                model.dtEffectiveDate = dateVolSimVOG_effective.Value;
                model.dtExpirationDate = dateVolSimVOG_expiration.Value;
                model.dtSent = dateVolSimVOG_sent.Value;
                modelInsert.Add(model);

                if (dateVolSimVOG_expiration.Value < DateTime.Today && model.optionSimilarity == "Final")
                {
                    if (dateVolSimVOG_expiration.Value != DateTime.MinValue)
                        lblVolSimVOG_expired.Visible = true;
                }
                else
                    lblVolSimVOG_expired.Visible = false;
            }
            else
            {
                model.idSimilarity = lblVolSimVOG.Text;
                model.idContPers = Person.idContPers;
                modelDelete.Add(model);
            }

            //COK
            model = new VolSimilarityModel();
            if (chkVolSimCOK.CheckState == CheckState.Checked)
            {
                model.idSimilarity = lblVolSimCOK.Text;
                model.idContPers = Person.idContPers;

                if (radioVolSimCOK_final.CheckState == CheckState.Checked)
                {
                    model.optionSimilarity = "Final";
                }
                else if (radioVolSimCOK_submited.CheckState == CheckState.Checked)
                {
                    model.optionSimilarity = "Submited";
                }
                else
                {
                    translateRadMessageBox msgbox = new translateRadMessageBox();
                    msgbox.translateAllMessageBox("Submited or Final must be selected in order to save.");
                    return;

                }
                model.dtEffectiveDate = dateVolSimCOK_effective.Value;
                model.dtExpirationDate = dateVolSimCOK_expiration.Value;
                model.dtSent = dateVolSimCOK_sent.Value;

                modelInsert.Add(model);

                if (dateVolSimCOK_expiration.Value < DateTime.Today && model.optionSimilarity == "Final")
                {
                    if (dateVolSimCOK_expiration.Value != DateTime.MinValue)
                        lblVolSimCOK_expired.Visible = true;
                }
                else
                    lblVolSimCOK_expired.Visible = false;
            }
            else
            {
                model.idSimilarity = lblVolSimCOK.Text;
                model.idContPers = Person.idContPers;
                modelDelete.Add(model);
            }

            //VOK
            model = new VolSimilarityModel();
            if (chkVolSimVOK.CheckState == CheckState.Checked)
            {
                model.idSimilarity = lblVolSimVOK.Text;
                model.idContPers = Person.idContPers;

                if (radioVolSimVOK_final.CheckState == CheckState.Checked)
                {
                    model.optionSimilarity = "Final";
                }
                else if (radioVolSimVOK_submited.CheckState == CheckState.Checked)
                {
                    model.optionSimilarity = "Submited";
                }
                else
                {
                    translateRadMessageBox msgbox = new translateRadMessageBox();
                    msgbox.translateAllMessageBox("Submited or Final must be selected in order to save.");
                    return;

                }
                model.dtEffectiveDate = dateVolSimVOK_effective.Value;
                model.dtExpirationDate = dateVolSimVOK_expiration.Value;
                model.dtSent = dateVolSimVOK_sent.Value;

                modelInsert.Add(model);

                if (dateVolSimVOK_expiration.Value < DateTime.Today && model.optionSimilarity == "Final")
                {
                    if (dateVolSimVOK_expiration.Value != DateTime.MinValue)
                        lblVolSimVOK_expired.Visible = true;
                }
                else
                    lblVolSimVOK_expired.Visible = false;
            }
            else
            {
                model.idSimilarity = lblVolSimVOK.Text;
                model.idContPers = Person.idContPers;
                modelDelete.Add(model);
            }

            try
            {
                if (modelDelete.Count > 0)
                {
                    VolSimilarityBUS volbus = new VolSimilarityBUS();
                    volbus.Delete(modelDelete, "frmPerson", Login._user.idUser);
                }
                if (modelInsert.Count > 0)
                {
                    VolSimilarityBUS volbus = new VolSimilarityBUS();
                    volbus.Save(modelInsert, "frmPerson", Login._user.idUser);
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex.Message);
            }
        }
        #endregion similarities

        private void radioVolSimVOG_submited_Click(object sender, EventArgs e)
        {
            dateVolSimVOG_effective.Visible = false;
            dateVolSimVOG_expiration.Visible = false;
            dateVolSimVOG_sent.Visible = true;
        }

        private void radioVolSimVOG_final_Click(object sender, EventArgs e)
        {
            dateVolSimVOG_effective.Visible = true;
            dateVolSimVOG_expiration.Visible = true;
            dateVolSimVOG_sent.Visible = false;
        }

        private void radioVolSimCOK_submited_Click(object sender, EventArgs e)
        {
            dateVolSimCOK_effective.Visible = false;
            dateVolSimCOK_expiration.Visible = false;
            dateVolSimCOK_sent.Visible = true;
        }

        private void radioVolSimCOK_final_Click(object sender, EventArgs e)
        {
            dateVolSimCOK_effective.Visible = true;
            dateVolSimCOK_expiration.Visible = true;
            dateVolSimCOK_sent.Visible = false;
        }

        private void radioVolSimVOK_submited_Click(object sender, EventArgs e)
        {
            dateVolSimVOK_effective.Visible = false;
            dateVolSimVOK_expiration.Visible = false;
            dateVolSimVOK_sent.Visible = true;
        }

        private void radioVolSimVOK_final_Click(object sender, EventArgs e)
        {
            dateVolSimVOK_effective.Visible = true;
            dateVolSimVOK_expiration.Visible = true;
            dateVolSimVOK_sent.Visible = false;
        }

        private void chkVolSimVOG_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkVolSimVOG.CheckState == CheckState.Checked)
            {
                radioVolSimVOG_final.Enabled = true;
                radioVolSimVOG_submited.Enabled = true;

            }
            else if (chkVolSimVOG.CheckState == CheckState.Unchecked)
            {
                radioVolSimVOG_final.Enabled = false;
                radioVolSimVOG_submited.Enabled = false;
            }
            else
            {

            }
        }

        private void chkVolSimCOK_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkVolSimCOK.CheckState == CheckState.Checked)
            {
                radioVolSimCOK_final.Enabled = true;
                radioVolSimCOK_submited.Enabled = true;

            }
            else if (chkVolSimCOK.CheckState == CheckState.Unchecked)
            {
                radioVolSimCOK_final.Enabled = false;
                radioVolSimCOK_submited.Enabled = false;
            }
            else
            {

            }
        }

        private void chkVolSimVOK_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkVolSimVOK.CheckState == CheckState.Checked)
            {
                radioVolSimVOK_final.Enabled = true;
                radioVolSimVOK_submited.Enabled = true;

            }
            else if (chkVolSimVOK.CheckState == CheckState.Unchecked)
            {
                radioVolSimVOK_final.Enabled = false;
                radioVolSimVOK_submited.Enabled = false;
            }
            else
            {

            }
        }

        private void btnVolSimSave_Click(object sender, EventArgs e)
        {
            SaveSimilarities();
        }

        private void btnDiplomaOrCertificate_Click(object sender, EventArgs e)
        {
            CertificatesBUS CertificatesBUS = new CertificatesBUS();
            List<IModel> cerm = new List<IModel>();

            cerm = CertificatesBUS.GetAllCertificates();


            using (var dlgCertificates = new GridLookupForm(cerm, "Certificate"))
            {

                if (dlgCertificates.ShowDialog(this) == DialogResult.Yes)
                {
                    CertificatesModel cerModel = new CertificatesModel();
                    cerModel = (CertificatesModel)dlgCertificates.selectedRow;
                    txtDiplomaOrCertificate.Text = cerModel.nameCertificate;

                    certificateModel.idCertificate = cerModel.idCertificate;
                    idCertificateFromGrid = cerModel.idCertificate; //idCertificateFromGrid promenljiva za smestanje idCertifcate.
                    xCertificateName = cerModel.nameCertificate;
                    xidCertificate = cerModel.idCertificate;

                    volFeaturesModel.codeCertificate = cerModel.codeCertificate;


                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void btnTraining_Click(object sender, EventArgs e)
        {
            TrainingBUS TrainingBUS = new TrainingBUS();
            List<IModel> tm = new List<IModel>();

            tm = TrainingBUS.GetAllTrainings();


            using (var dlgTraining = new GridLookupForm(tm, "Training"))
            {

                if (dlgTraining.ShowDialog(this) == DialogResult.Yes)
                {
                    TrainingModel traModel = new TrainingModel();
                    traModel = (TrainingModel)dlgTraining.selectedRow;
                    txtTraining.Text = traModel.nameTraining;

                    trainingModel.idTraining = traModel.idTraining;
                    idTrainingFromGrid = traModel.idTraining; //idTrainingFromGrid promenljiva za smestanje idTraining.
                    xTrainingName = traModel.nameTraining;
                    xidTraining = traModel.idTraining;

                    volFeaturesModel.codeTraining = traModel.codeTraining;// uzima codeTraining i smesta u model
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void btnAddFeatures_Click(object sender, EventArgs e)
        {
            if ((volFeaturesModel.codeCertificate == null || volFeaturesModel.codeCertificate == "") && (volFeaturesModel.codeTraining == null || volFeaturesModel.codeTraining == ""))
            {
                RadMessageBox.Show("You have enter some data");
            }
            else
            {
                try
                {
                    if (volFeaturesList == null)//ako nije prazan grid dodaje nov slog u grid
                    {
                        volFeaturesList = new BindingList<VolFeaturesModel>();
                    }

                    VolFeaturesBUS volFeaBus = new VolFeaturesBUS();
                    List<VolFeaturesModel> volfModel = new List<VolFeaturesModel>();

                    volFeaturesModel.idContPers = Person.idContPers;


                    DateTime date1 = DateTime.Parse(dtExpirationDate1.Value.ToShortDateString());
                    DateTime date2 = DateTime.Parse(dtDateArchieved.Value.ToShortDateString());
                    DateTime date3 = DateTime.Parse(dtScheaduledDate.Value.ToShortDateString());

                    if (volFeaturesModel.codeCertificate == null)
                        volFeaturesModel.codeCertificate = "";
                    if (volFeaturesModel.codeTraining == null)
                        volFeaturesModel.codeTraining = "";


                    volFeaturesModel.expireDate = date1;
                    volFeaturesModel.archiveDate = date2;
                    volFeaturesModel.scheduleDate = date3;

                    //============== Za datume ukoliko je prazno polje da ne izbacuje Sql upozorenje==========
                    if (dtExpirationDate1.Text.ToString() == "")
                    {
                        volFeaturesModel.expireDate = Convert.ToDateTime("1900-01-01");
                    }

                    if (dtDateArchieved.Text.ToString() == "")
                    {
                        volFeaturesModel.archiveDate = Convert.ToDateTime("1900-01-01");
                    }

                    if (dtScheaduledDate.Text.ToString() == "")
                    {
                        volFeaturesModel.scheduleDate = Convert.ToDateTime("1900-01-01");
                    }
                    //============== Za datume ukoliko je prazno polje da ne izbacuje Sql upozorenje==========                   


                    volFeaturesList.Add(volFeaturesModel); //dodaje u grid

                    volfModel = volFeaBus.GetAllFeature();

                    if (volFeaBus.Save(volFeaturesModel, this.Name, Login._user.idUser) != true) //upis u bazu
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Someting went wrong with save data");
                    }

                    // refresh grida dodaje novu stavku u model i ponovo povlaci podatke iz baze

                    rgvVolFeatures.DataSource = null;
                    VolFeaturesBUS volFeBus = new VolFeaturesBUS();
                    List<VolFeaturesModel> volfFeModelList = new List<VolFeaturesModel>();
                    volfFeModelList = volFeBus.GetAllFeaturesFromPersonGrid(Person.idContPers);
                    if (volfFeModelList != null)
                    {
                        //for (int i = 0; cpmodel.Count < i++; )
                        for (int i = 0; i < volfFeModelList.Count; i++)
                        {
                            if (volfFeModelList[i].expireDate.ToString() == "1-1-1900 00:00:00")
                                volfFeModelList[i].expireDate = null;
                            if (volfFeModelList[i].archiveDate.ToString() == "1-1-1900 00:00:00")
                                volfFeModelList[i].archiveDate = null;
                            if (volfFeModelList[i].scheduleDate.ToString() == "1-1-1900 00:00:00")
                                volfFeModelList[i].scheduleDate = null;

                        }
                    }


                    rgvVolFeatures.DataSource = volfFeModelList;
                    txtDiplomaOrCertificate.Text = "";
                    txtTraining.Text = "";
                    volFeaturesModel = new VolFeaturesModel();
                    //dtExpirationDate1.Text = "";
                    //dtDateArchieved.Text = "";
                    //dtScheaduledDate.Text = "";

                }
                catch (Exception ex)
                {
                    RadMessageBox.Show(ex.Message);
                }
            }

        }

        private void rgvVolFeatures_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvVolFeatures.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvVolFeatures.Columns[i].HeaderText != null && resxSet.GetString(rgvVolFeatures.Columns[i].HeaderText) != null)
                        rgvVolFeatures.Columns[i].HeaderText = resxSet.GetString(rgvVolFeatures.Columns[i].HeaderText);
                }
            }

            if (File.Exists(layoutVolFeaturesView))
            {
                rgvVolFeatures.LoadLayout(layoutVolFeaturesView);
            }
        }

        private void rgvVolFeatures_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            string saveLayout = "Save Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem = new RadMenuItem();
            customMenuItem.Text = saveLayout;
            customMenuItem.Click += SaveLayout;
            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);

        }

        private void SaveLayout(object sender, EventArgs e)
        {
            if (File.Exists(layoutVolFeaturesView))
            {
                File.Delete(layoutVolFeaturesView);
            }
            rgvVolFeatures.SaveLayout(layoutVolFeaturesView);
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("You have successfully save layout!") != null)
                    RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
                else
                    RadMessageBox.Show("You have successfully save layout!");
            }

        }

        private void SaveLayoutTravelData(object sender, EventArgs e)
        {
            if (File.Exists(layoutTravelDataView))
            {
                File.Delete(layoutTravelDataView);
            }
            rgvTripData.SaveLayout(layoutTravelDataView);

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("You have successfully save layout!") != null)
                    RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
                else
                    RadMessageBox.Show("You have successfully save layout!");
            }
        }

        private void rgvVolFeatures_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {
            translateRadMessageBox msgbox = new translateRadMessageBox();
            DialogResult dr = msgbox.translateAllMessageBoxDialog("Delete entry ?", "Delete");

            if (dr != DialogResult.Yes)
                e.Cancel = true;
            else
            {
                if (rgvVolFeatures.CurrentRow.DataBoundItem != null)
                {
                    VolFeaturesBUS bus = new VolFeaturesBUS();
                    VolFeaturesModel model = (VolFeaturesModel)rgvVolFeatures.CurrentRow.DataBoundItem;
                    bus.Delete(model.idFeatures, this.Name, Login._user.idUser);
                }
            }
        }

        private void btnAddTravelData_Click(object sender, EventArgs e)
        {

            if (ddlCalc.SelectedItem == null || dpFrom.Text == "" || dpTo.Text == "")
            {

                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You have enter some data");
                return;
            }


            else
            {
                try
                {
                    if (conPersTripDataList == null)
                    {
                        conPersTripDataList = new BindingList<ContactPersonTripDataModel>();
                    }

                    ContactPersonTripDataBUS cPerTrBUS = new ContactPersonTripDataBUS();
                    List<ContactPersonTripDataModel> cPerTrModel = new List<ContactPersonTripDataModel>();
                    conPersTripDataModel = new ContactPersonTripDataModel();

                    conPersTripDataModel.idTargetGroup = Convert.ToInt32(ddlTargetGroup.SelectedValue);
                    conPersTripDataModel.descriptionTripSort = Convert.ToString(ddlCalc.SelectedItem.Text);
                    conPersTripDataModel.helpP = Convert.ToString(ddlHelP.Text);

                    conPersTripDataModel.idContactPerson = Person.idContPers;

                    DateTime date1 = DateTime.Parse(dpFrom.Value.ToShortDateString());
                    DateTime date2 = DateTime.Parse(dpTo.Value.ToShortDateString());

                    conPersTripDataModel.dtFrom = date1;
                    conPersTripDataModel.dtTo = date2;

                    if (dpTo.Value < dpFrom.Value) // kada je datum dpTo manje ne moze da unese vrednost!
                    {
                        // RadMessageBox.Show("Not validate date!");
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("Not validate date!");
                        return;
                    }

                    //============== Za datume ukoliko je prazno polje da ne izbacuje Sql upozorenje==========
                    if (dpFrom.Text.ToString() == "")
                    {
                        conPersTripDataModel.dtFrom = Convert.ToDateTime("1900-01-01");
                    }

                    if (dpTo.Text.ToString() == "")
                    {
                        conPersTripDataModel.dtTo = Convert.ToDateTime("1900-01-01");
                    }
                    //============== Za datume ukoliko je prazno polje da ne izbacuje Sql upozorenje==========

                    //Provera da li je cekiran vise od jednog check box-a
                    int checkBoxes = 0;

                    if (chk1op1.Checked == true)
                    {
                        checkBoxes = checkBoxes + 1;
                        conPersTripDataModel.op1 = true;
                    }
                    if (chk1op2.Checked == true)
                    {
                        checkBoxes = checkBoxes + 1;
                        conPersTripDataModel.op2 = true;
                    }
                    if (chk1op3.Checked == true)
                    {
                        checkBoxes = checkBoxes + 1;
                        conPersTripDataModel.op3 = true;
                    }
                    if (checkBoxes > 1)
                    {

                        //RadMessageBox.Show("Only one must be checked!");
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("Only one must be checked!");
                        return;
                    }


                    conPersTripDataList.Add(conPersTripDataModel); //dodaje u grid

                    if (cPerTrBUS.Save(conPersTripDataModel, this.Name, Login._user.idUser) != true)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Someting went wrong with save data");
                    }

                    // refresh grida dodaje novu stavku u model i ponovo povlaci podatke iz baze

                    rgvTripData.DataSource = null;
                    ContactPersonTripDataBUS cptBUS = new ContactPersonTripDataBUS();
                    List<ContactPersonTripDataModel> cptModelList = new List<ContactPersonTripDataModel>();
                    cptModelList = cptBUS.GetTripByPerson(Person.idContPers);

                    if (cptModelList != null)
                    {
                        for (int i = 0; i < cptModelList.Count; i++)
                        {
                            if (cptModelList[i].dtFrom.ToString() == "1-1-1900 00:00:00")
                                cptModelList[i].dtFrom = null;
                            if (cptModelList[i].dtTo.ToString() == "1-1-1900 00:00:00")
                                cptModelList[i].dtTo = null;
                        }
                    }

                    rgvTripData.DataSource = cptModelList;
                    conPersTripDataModel = new ContactPersonTripDataModel();

                }
                catch (Exception ex)
                {
                    RadMessageBox.Show(ex.Message);
                }
            }
        }

        private void rgvTripData_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {
            translateRadMessageBox msgbox = new translateRadMessageBox();
            DialogResult dr = msgbox.translateAllMessageBoxDialog("Delete entry ?", "Delete");

            if (dr != DialogResult.Yes)
                e.Cancel = true;
            else
            {
                if (rgvTripData.CurrentRow.DataBoundItem != null)
                {
                    ContactPersonTripDataBUS bus = new ContactPersonTripDataBUS();
                    ContactPersonTripDataModel model = (ContactPersonTripDataModel)rgvTripData.CurrentRow.DataBoundItem;

                    bus.Delete(model.idContPersTravel, this.Name, Login._user.idUser);
                }
            }
        }

        private void rgvTripData_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if (rgvTripData.Columns.Count > 0) //provera redova u gridu da ne bi pucao
            {
                for (int i = 0; i < rgvTripData.Columns.Count; i++)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (rgvTripData.Columns[i].HeaderText != null && resxSet.GetString(rgvTripData.Columns[i].HeaderText) != null)
                            rgvTripData.Columns[i].HeaderText = resxSet.GetString(rgvTripData.Columns[i].HeaderText);
                    }

                }

                // this.rgvTripData.Columns["idTargetGroup"].IsVisible = false;


                if (File.Exists(layoutTravelDataView))
                {
                    rgvTripData.LoadLayout(layoutTravelDataView);
                }
            }
        }

        private void rgvTripData_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            string saveLayout = "Save Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem = new RadMenuItem();
            customMenuItem.Text = saveLayout;
            customMenuItem.Click += SaveLayoutTravelData;
            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            using (frmVolunteerReport ffr = new frmVolunteerReport())
            {
                ffr.Show();
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void chkActive_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkActive.Checked == true && pageLoaded)
            {

                dtOfActive = Person.dtOfActive;
                ddlReasonOut.Visible = true;
                MessageBoxCalendar messageBox = new MessageBoxCalendar("Active/not active", "Select the date please!");
                if (messageBox.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                {
                    Person.dtOfActive = messageBox.selectedDate;
                }
                else {
                    chkActive.Checked = false;
                }
            }
            else
            {
                Person.dtOfActive = dtOfActive;
                ddlReasonOut.Visible = false;
            }
        }

        private void txtIdentBSN_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void dtIssueDate_ValueChanged(object sender, EventArgs e)
        {
            dtIssueDate.Checked = true;
        }

        private void dtValidTo_ValueChanged(object sender, EventArgs e)
        {
            dtValidTo.Checked = true;
        }

        private void frmPerson_FormClosing(object sender, FormClosingEventArgs e)
        {
            savePerson();
            saveTel();
            saveEmail();

            //saveVoluntary();

            // changes in PErson basic data (ContactPerson table            )
            bool changes = Person.CompareWith(PersonFirst);

            // changes in tel and email
            //var DifferencesList = persTel.Where(x => !persTelFirst.Any(x1 => x1.numberTel == x.numberTel))
            //.Union(persTelFirst.Where(x => !persTel.Any(x1 => x1.numberTel == x.numberTel)));
            PersonTelModelComparer persTelComparer = new PersonTelModelComparer();
            IEnumerable<PersonTelModel> differenceTel = persTel.Except(persTelFirst, persTelComparer);

            PersonEmailModelComparer persEmailComparer = new PersonEmailModelComparer();
            IEnumerable<PersonEmailModel> differenceEmail = persEmail.Except(persEmailFirst, persEmailComparer);

            bool resultTel = Utils.IsAny(differenceTel);
            bool resultEmail = Utils.IsAny(differenceEmail);

            if (changes == true || resultTel == true || resultEmail == true || isVolChanged == true || isMedChanged == true)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                DialogResult dr = tr.translateAllMessageBoxDialog("There is changes on form. Save before close ?", "Save");
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    bool validate = ValidatePerson();
                    if (validate == true)
                    {
                        insertData();

                        bool errorOnSave = false;
                        // SAVE MEDICAL
                        MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
                        VolontaryFunctionBUS vfb = new VolontaryFunctionBUS();
                        VolontaryTripBUS vtb = new VolontaryTripBUS();

                        if (isMedChanged == true)
                        {
                            saveMedical();
                            if (mvb.DeleteMedical(Person.idContPers, this.Name, Login._user.idUser) == true)
                            {
                                for (int j = 0; j < personMedical.Count; j++)
                                {
                                    if (mvb.SaveMedical(personMedical[j], this.Name, Login._user.idUser) == false)
                                        errorOnSave = true;

                                }
                            }
                            else
                                errorOnSave = true;
                        }


                        // SAVE VOLONTARY                         
                        if (isVolChanged == true)
                        {
                            saveVoluntary();
                            if (mvb.Delete(Person.idContPers, this.Name, Login._user.idUser) == true)
                            {
                                for (int s = 0; s < personVoluntary.Count; s++)
                                {
                                    if (mvb.Save(personVoluntary[s], this.Name, Login._user.idUser) == false)
                                    {
                                        errorOnSave = true;
                                        break;
                                    }
                                }
                            }
                            else
                                errorOnSave = true;

                            saveVoluntaryFunction();
                            if (vfb.Delete(Person.idContPers, this.Name, Login._user.idUser) == true)
                            {
                                for (int q = 0; q < personVoluntary1.Count; q++)
                                {
                                    if (vfb.Save(personVoluntary1[q], this.Name, Login._user.idUser) == false)
                                    {
                                        errorOnSave = true;
                                        break;
                                    }

                                }
                            }
                            else
                                errorOnSave = true;

                            saveVoluntaryTrip();
                            if (vtb.Delete(Person.idContPers, this.Name, Login._user.idUser) == true)
                            {
                                for (int t = 0; t < personVoluntary2.Count; t++)
                                {
                                    if (vtb.Save(personVoluntary2[t], this.Name, Login._user.idUser) == false)
                                    {
                                        errorOnSave = true;
                                        break;
                                    }
                                }
                            }
                            else
                                errorOnSave = true;

                            if (errorOnSave == true)
                            {
                                RadMessageBox.Show("You have not succesufully inserted data. Please check!");
                            }
                        }
                        // END SAVE VOLONTARY
                    }

                }
                else if (dr == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    // NO option
                    Person.CopyValues(PersonFirst);
                }
            }
        }

        private void UpdateOriginalValuesAfterSave()
        {
            PersonFirst = new PersonModel(Person);
            persEmailFirst = new List<PersonEmailModel>();
            persTelFirst = new List<PersonTelModel>();
            if (persEmail != null)
            {
                foreach (PersonEmailModel m in persEmail)
                {
                    persEmailFirst.Add(m.ReturnCopy());
                }
            }
            if (persTel != null)
            {
                foreach (PersonTelModel m in persTel)
                {
                    persTelFirst.Add(m.ReturnCopy());
                }
            }

            isMedChanged = false;
            isVolChanged = false;
        }

        private void gridIbans_UserAddingRow(object sender, GridViewRowCancelEventArgs e)
        {
            GridViewRowInfo rowToBeAdded = e.Rows.FirstOrDefault();
            if (rowToBeAdded != null && rowToBeAdded.Cells["ibanNumber"].Value != null)
            {
                string strIban = rowToBeAdded.Cells["ibanNumber"].Value.ToString();
                if (strIban.Trim() != "")
                {
                    MakeInvoice cc = new MakeInvoice();
                    bool b = cc.ValidateIban(strIban.Trim());
                    if (b == true)
                    {
                        AccIbanBUS ibanbus = new AccIbanBUS();
                        AccIbanModel model = new AccIbanModel();

                        List<AccIbanModel> checkIBAN = ibanbus.CheckIbanForPerson(strIban.Trim(), Person.idContPers);
                        if (checkIBAN == null || checkIBAN.Count <= 0)
                        {

                            model.accNumber = txtPersAccNo.Text;
                            model.idContPers = Person.idContPers;
                            model.ibanNumber = strIban;
                            model.idClient = 0;
                            model.Id = ibanbus.Save(model, this.Name, Login._user.idUser);

                            //za save u grid
                            rowToBeAdded.Cells["accNumber"].Value = model.accNumber;
                            rowToBeAdded.Cells["idClient"].Value = model.idClient;
                            rowToBeAdded.Cells["idContPers"].Value = model.idContPers;
                            rowToBeAdded.Cells["Id"].Value = model.Id;

                            if (model.Id == 0)
                            {
                                translateRadMessageBox trr = new translateRadMessageBox();
                                trr.translateAllMessageBox("Error while saving.");
                                e.Cancel = true;
                            }
                        }
                        else
                        {
                            translateRadMessageBox trr = new translateRadMessageBox();
                            trr.translateAllMessageBox("IBAN  already exist for that person.");
                            e.Cancel = true;
                        }

                    }
                    else
                    {
                        translateRadMessageBox trr = new translateRadMessageBox();
                        trr.translateAllMessageBox("IBAN  NOT CORRECT.");
                        e.Cancel = true;
                    }
                }
                else
                    e.Cancel = true;
            }
            else
                e.Cancel = true;

        }

        private void gridIbans_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {
            GridViewRowInfo rowToBeDeleted = e.Rows.FirstOrDefault();
            AccIbanModel model = (AccIbanModel)rowToBeDeleted.DataBoundItem;

            if (model != null)
            {
                translateRadMessageBox trr = new translateRadMessageBox();
                DialogResult dr = trr.translateAllMessageBoxDialogYesNo("Delete IBAN number: " + model.ibanNumber + " ?", "Delete");

                if (dr == DialogResult.Yes)
                {
                    AccIbanBUS ibanbus = new AccIbanBUS();
                    ibanbus.Delete(model.Id, this.Name, Login._user.idUser);

                }
                else
                    e.Cancel = true;
            }
            else
                e.Cancel = true;

        }

        private void radPageVH_SelectedPageChanging(object sender, RadPageViewCancelEventArgs e)
        {
            RadPageView rpvVV = (RadPageView)sender;
            string sName2 = ((RadPageView)sender).SelectedPage.Name;
            if (isVolChanged == true)
            {
                if (Person.idContPers != 0)
                {
                    MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
                    if (sName2 == "tabSkils")
                    {
                        saveVoluntary();
                        if (mvb.Delete(Person.idContPers, this.Name, Login._user.idUser) == true)
                        {
                            for (int j = 0; j < personVoluntary.Count; j++)
                            {
                                if (mvb.Save(personVoluntary[j], this.Name, Login._user.idUser) == false)
                                {
                                    RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                                    break;
                                }

                            }
                        }
                        else
                        {
                            RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                        }
                    }
                    if (sName2 == "tabTrips")
                    {
                        VolontaryTripBUS vtb = new VolontaryTripBUS();
                        saveVoluntaryTrip();
                        if (vtb.Delete(Person.idContPers, this.Name, Login._user.idUser) == true)
                        {
                            for (int j = 0; j < personVoluntary2.Count; j++)
                            {
                                if (vtb.Save(personVoluntary2[j], this.Name, Login._user.idUser) == false)
                                {
                                    RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                                    break;
                                }

                            }
                        }
                        else
                        {
                            RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                        }
                    }
                    if (sName2 == "tabFunction")
                    {
                        VolontaryFunctionBUS vfb = new VolontaryFunctionBUS();
                        saveVoluntaryFunction();
                        if (vfb.Delete(Person.idContPers, this.Name, Login._user.idUser) == true)
                        {
                            for (int j = 0; j < personVoluntary1.Count; j++)
                            {
                                if (vfb.Save(personVoluntary1[j], this.Name, Login._user.idUser) == false)
                                {
                                    RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                                    break;
                                }

                            }
                        }
                        else
                        {
                            RadMessageBox.Show("You have not succesufully inserted voluntary data. Please check!");
                        }
                    }
                }
            }
            if (isVolChanged == true)
                isVolChanged = false;
        }

        private void btnBookingTo_Click(object sender, EventArgs e)
        {
            if (Person.idContPers != 0 && Person.idContPers != -1)
            {
                List<IModel> contactPersons = new List<IModel>();
                PersonBUS pb = new PersonBUS();
                contactPersons = pb.GetPersonsButThis(Person.idContPers);

                using (GridLookupForm frm = new GridLookupForm(contactPersons, "Contact person booking to"))
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        PersonShortModel im = (PersonShortModel)frm.selectedRow;
                        txtBookingTo.Text = im.firstname + " " + im.midname + " " + im.lastname;
                        Person.idContPersBookingTo = im.idContPers;


                        PersonAddressBUS pab = new PersonAddressBUS();
                        PersonAddressModel pam = pab.GetPersonAddressesByType(1, im.idContPers).FirstOrDefault();

                        panelAddress.Controls.Find("txt_badr_street", true)[0].Text = pam.street;
                        panelAddress.Controls.Find("txt_badr_city", true)[0].Text = pam.city;
                        panelAddress.Controls.Find("txt_badr_houseno", true)[0].Text = pam.housenr;
                        panelAddress.Controls.Find("txt_badr_zip", true)[0].Text = pam.postalCode;
                        panelAddress.Controls.Find("txt_badr_ext", true)[0].Text = pam.extension;
                        if (pam.isInternational == true)
                        {
                            RadRadioButton rchk = (RadRadioButton)panelAddress.Controls.Find("rad_badr_inter", true)[0];
                            rchk.CheckState = CheckState.Checked;
                            RadRadioButton rchkNL = (RadRadioButton)panelAddress.Controls.Find("rad_badr_nl", true)[0];
                            rchkNL.CheckState = CheckState.Unchecked;
                            panelAddress.Controls.Find("btn_badr_get", true)[0].Visible = false;
                            panelAddress.Controls.Find("lbl_badr_country", true)[0].Visible = true;
                            panelAddress.Controls.Find("txt_badr_country", true)[0].Visible = true;
                            panelAddress.Controls.Find("txt_badr_country", true)[0].Text = pam.country;
                        }

                    }
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            else
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("First you need to save that person so you can see which person you can add for booking to.");
            }

        }

        private void btnCancelTraveler_Click(object sender, EventArgs e)
        {
            if (rgvArrangment != null)
            {
                if (rgvArrangment.SelectedRows != null)
                {
                    if (rgvArrangment.SelectedRows.Count > 0 && rgvArrangment.CurrentRow.DataBoundItem != null)
                    {
                      //  string idArrangementBook= rgvArrangment.SelectedRows
                        ArrangementAllModel pb = (ArrangementAllModel)rgvArrangment.CurrentRow.DataBoundItem;
                        ArrangementModel arrange = new ArrangementModel();
                        arrange = new ArrangementBUS().GetArrangementById(pb.idArrangement);
                        PersonBookModel selectedModel = new PersonBookModel();
                        selectedModel = new PersonBUS().GetExactPersonsForArrangement(pb.idArrangementBook, Login._user.lngUser, Person.idContPers);
                        CancelArrangement ca = new CancelArrangement();
                        if (ca.cancel(selectedModel, arrange,this.Name,Login._user.idUser )== true)
                        {
                            rgvArrangment.DataSource = null;
                            PersonBUS pbb = new PersonBUS();
                            rgvArrangment.Columns.Clear();
                            rgvArrangment.DataSource = pbb.GetArrangementsForPerson(Person.idContPers);
                        }

                    }
                }
            }
        }

        private void ddlTitle_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (ddlTitle != null)
            {
                if (ddlGender != null)
                {
                    if (ddlGender.Items.Count >= 3)
                    {
                        if (ddlTitle.Items.Count == 6)
                        {
                            int i = ddlTitle.SelectedIndex;
                            switch (i)
                            {
                                case 1:
                                    ddlGender.SelectedIndex = 1;
                                    break;
                                case 2:
                                    ddlGender.SelectedIndex = 2;
                                    break;
                                default:
                                    break;
                            }

                        }
                    }
                }
            }

        }

        private void ddlGender_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

            if (ddlGender != null)
            {
                if (ddlTitle != null)
                {
                    if (ddlTitle.Items.Count == 6)
                    {
                        if (ddlGender.Items.Count >= 3)
                        {
                            int i = ddlGender.SelectedIndex;
                            switch (i)
                            {
                                case 1:
                                    ddlTitle.SelectedIndex = 1;
                                    break;
                                case 2:
                                    ddlTitle.SelectedIndex = 2;
                                    break;
                                default:
                                    break;
                            }

                        }
                    }
                }
            }
        }

        private void txtinitialsContPers_TextChanged(object sender, EventArgs e)
        {
            if (!txtinitialsContPers.Text.EndsWith(".") && txtinitialsContPers.Text.Length > lengthInitials)
            {
                txtinitialsContPers.Text += ".";

                txtinitialsContPers.SelectionStart = txtinitialsContPers.Text.Length;
                lengthInitials = txtinitialsContPers.Text.Length;
            }
        }

        private void txtinitialsContPers_TextChanging(object sender, TextChangingEventArgs e)
        {
            lengthInitials = e.OldValue.Length;
        }

        private void ddlNacionality_Leave(object sender, EventArgs e)
        {
            if (ddlNacionality != null)
            {
                if (ddlNacionality.Items.Count > 0)
                {
                    CountryBUS gb6 = new CountryBUS();
                    List<CountryModel> gm6 = new List<CountryModel>();
                    gm6 = gb6.GetCountriesWithCountryModel();
                    foreach (CountryModel cm in gm6)
                        if (cm.nameCountry.StartsWith(ddlNacionality.Text, System.StringComparison.OrdinalIgnoreCase))
                        {
                            ddlNacionality.SelectedValue = cm.idCountry;
                            ddlNacionality.SelectedText = cm.nacionality;
                            break;
                        }
                }
            }
        }

        private void rgvArrangment_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if (rgvArrangment != null)
            {
                if(rgvArrangment.RowCount>0)
                {
                    for (int i = 0; i < rgvArrangment.Columns.Count; i++)
                    {
                       
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (rgvArrangment.Columns[i].HeaderText != null && resxSet.GetString(rgvArrangment.Columns[i].HeaderText) != null)
                                rgvArrangment.Columns[i].HeaderText = resxSet.GetString(rgvArrangment.Columns[i].HeaderText);
                        }
                       
                    }                   

                }
              
            }
           
          
        }

        private void rgvArrangment_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Column.Name == "dtFromArrangement" || e.Column.Name == "dtToArrangement")
            {
                if (e.Column.IsVisible == true)
                {
                    try
                    {
                        DateTime temp = DateTime.Parse(e.CellElement.Value.ToString());
                        e.CellElement.Text = temp.ToString("dd-MM-yyyy");
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        private void dtbirthdate_ValueChanged(object sender, EventArgs e)
        {
            dtbirthdate.Checked = true;
        }

        //Novo Gorance 31 8
        private void rgvDocuments_CommandCellClick(object sender, GridViewCellEventArgs e)
        {
            DocumentsModel model = (DocumentsModel)rgvDocuments.CurrentRow.DataBoundItem;
            List<DocumentTypeModel> dtm = new DocumentTypeBUS().GetALLDocumentTypes();
            string ext = dtm.Find(item => item.typeDocument.TrimEnd() == model.typeDocument.TrimEnd()).extendDocumentType;


            string sDest = "";
            if (ext.Trim() == "MSG" || ext.Trim() == "EML")
            {
                sDest = MainForm.myEmailFolder;
            }
            //else if (ext.Trim() == "WCPLET")
            //{
            //    sDest = MainForm.TemplatesFolder;
            //}
            else
            {
                sDest = MainForm.DocumentsFolder;
            }

            string fullname = sDest + "\\" + model.fileDocument;
            if (System.IO.File.Exists(fullname))
                OpenDocument(sDest, model.fileDocument);
            else
                RadMessageBox.Show("Error opening document", "Open error", MessageBoxButtons.OK, RadMessageIcon.Error);
        }

        private void OpenDocument(string sDest, string sFileName)
        {
            try
            {
                string sExtention = Path.GetExtension(sFileName).Replace(".", "");
                string sFullName = sDest + "\\" + sFileName;
                System.Diagnostics.Process.Start(sFullName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rgvDocuments_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            string saveLayout = "Save Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem = new RadMenuItem();
            customMenuItem.Text = saveLayout;
            customMenuItem.Click += SaveLayoutEB;
            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);

            //=== delete
            string saveLayout1 = "Delete Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem1 = new RadMenuItem();
            customMenuItem1.Text = saveLayout1;
            customMenuItem1.Click += SaveLayoutEB1;
            RadMenuSeparatorItem separator1 = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator1);
            e.ContextMenu.Items.Add(customMenuItem1);
        }

        private void SaveLayoutEB(object sender, EventArgs e)
        {
            if (File.Exists(layoutDocuments))
            {
                File.Delete(layoutDocuments);
            }
            rgvDocuments.SaveLayout(layoutDocuments);

            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully save layout!");

        }
        private void SaveLayoutEB1(object sender, EventArgs e)
        {
            if (File.Exists(layoutDocuments))
            {
                File.Delete(layoutDocuments);
            }
            //  gridItems.SaveLayout(layoutLineBook);
            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully delete layout!");

        }

        private void rgvMeetings_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            string saveLayout = "Save Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem = new RadMenuItem();
            customMenuItem.Text = saveLayout;
            customMenuItem.Click += SaveLayoutM;
            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);

            //=== delete
            string saveLayout1 = "Delete Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem1 = new RadMenuItem();
            customMenuItem1.Text = saveLayout1;
            customMenuItem1.Click += SaveLayoutM1;
            RadMenuSeparatorItem separator1 = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator1);
            e.ContextMenu.Items.Add(customMenuItem1);
        }


        private void SaveLayoutM(object sender, EventArgs e)
        {
            if (File.Exists(layoutMeetings))
            {
                File.Delete(layoutMeetings);
            }
            rgvMeetings.SaveLayout(layoutMeetings);

            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully save layout!");

        }
        private void SaveLayoutM1(object sender, EventArgs e)
        {
            if (File.Exists(layoutMeetings))
            {
                File.Delete(layoutMeetings);
            }
            //  gridItems.SaveLayout(layoutLineBook);
            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully delete layout!");

        }

        private void rgvContacts_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            string saveLayout = "Save Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem = new RadMenuItem();
            customMenuItem.Text = saveLayout;
            customMenuItem.Click += SaveLayoutC;
            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);

            //=== delete
            string saveLayout1 = "Delete Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem1 = new RadMenuItem();
            customMenuItem1.Text = saveLayout1;
            customMenuItem1.Click += SaveLayoutC1;
            RadMenuSeparatorItem separator1 = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator1);
            e.ContextMenu.Items.Add(customMenuItem1);
        }


        private void SaveLayoutC(object sender, EventArgs e)
        {
            if (File.Exists(layoutContacts))
            {
                File.Delete(layoutContacts);
            }
            rgvContacts.SaveLayout(layoutContacts);

            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully save layout!");

        }
        private void SaveLayoutC1(object sender, EventArgs e)
        {
            if (File.Exists(layoutContacts))
            {
                File.Delete(layoutContacts);
            }
            //  gridItems.SaveLayout(layoutLineBook);
            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully delete layout!");

        }

        private void rgvToDo_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            string saveLayout = "Save Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem = new RadMenuItem();
            customMenuItem.Text = saveLayout;
            customMenuItem.Click += SaveLayoutToDo;
            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);

            //=== delete
            string saveLayout1 = "Delete Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem1 = new RadMenuItem();
            customMenuItem1.Text = saveLayout1;
            customMenuItem1.Click += SaveLayoutToDo1;
            RadMenuSeparatorItem separator1 = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator1);
            e.ContextMenu.Items.Add(customMenuItem1);
        }

        private void SaveLayoutToDo(object sender, EventArgs e)
        {
            if (File.Exists(layoutTasks))
            {
                File.Delete(layoutTasks);
            }
            rgvToDo.SaveLayout(layoutTasks);

            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully save layout!");

        }
        private void SaveLayoutToDo1(object sender, EventArgs e)
        {
            if (File.Exists(layoutTasks))
            {
                File.Delete(layoutTasks);
            }
            //  gridItems.SaveLayout(layoutLineBook);
            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully delete layout!");

        }

        private void rgvNote_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            string saveLayout = "Save Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem = new RadMenuItem();
            customMenuItem.Text = saveLayout;
            customMenuItem.Click += SaveLayoutNote;
            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);

            //=== delete
            string saveLayout1 = "Delete Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem1 = new RadMenuItem();
            customMenuItem1.Text = saveLayout1;
            customMenuItem1.Click += SaveLayoutNote1;
            RadMenuSeparatorItem separator1 = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator1);
            e.ContextMenu.Items.Add(customMenuItem1);
        }

        private void SaveLayoutNote(object sender, EventArgs e)
        {
            if (File.Exists(layoutMemo))
            {
                File.Delete(layoutMemo);
            }
            rgvNote.SaveLayout(layoutMemo);

            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully save layout!");

        }
        private void SaveLayoutNote1(object sender, EventArgs e)
        {
            if (File.Exists(layoutMemo))
            {
                File.Delete(layoutMemo);
            }
            //  gridItems.SaveLayout(layoutLineBook);
            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully delete layout!");

        }

        private void rgvArrangment_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            string saveLayout = "Save Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem = new RadMenuItem();
            customMenuItem.Text = saveLayout;
            customMenuItem.Click += SaveLayoutAr;
            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);

            //=== delete
            string saveLayout1 = "Delete Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem1 = new RadMenuItem();
            customMenuItem1.Text = saveLayout1;
            customMenuItem1.Click += SaveLayoutAr1;
            RadMenuSeparatorItem separator1 = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator1);
            e.ContextMenu.Items.Add(customMenuItem1);
        }


        private void SaveLayoutAr(object sender, EventArgs e)
        {
            if (File.Exists(layoutArrangement))
            {
                File.Delete(layoutArrangement);
            }
            rgvArrangment.SaveLayout(layoutArrangement);

            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully save layout!");

        }
        private void SaveLayoutAr1(object sender, EventArgs e)
        {
            if (File.Exists(layoutArrangement))
            {
                File.Delete(layoutArrangement);
            }
            //  gridItems.SaveLayout(layoutLineBook);
            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully delete layout!");

        }
        
    }



    public static class Utils
    {
        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data != null && data.Any();
        }
    }

}
