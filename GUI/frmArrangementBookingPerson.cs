using BIS.Business;
using BIS.Model;
using GUI.User_Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.Linq;

namespace GUI
{
    public partial class frmArrangementBookingPerson : frmTemplate
    {

        List<PersonModel> arrangementBookPerson;
        BindingList<ArrangementTravelersModel> travelersList;
        ArrangementBookModel arrBookModel;
        ArrangementBookBUS arrBookBUS;
        int iID = -1;
        int arrStatus = -1;
        int idArrBook = -1;
        InvoiceModel _selectedRowInvoice;
        InvoiceModel _clickedInvoice;
        Boolean isLoaded = false;


        public List<ArrangementRoomsArticle> selArticlesAccomodation;

        List<ArrangementRoomsArticle> modelArticlesAccomodation;
        private int xrollator;
        private int xrolstool;
        private int xarmafa;
        private int xnrAnchorage;
        private int rfld1;
        private int rfld2;
        private int rfld4;
        private int rnrAnchorage;
        private int idLabel = 0;

      

        private List<string> codeArticlesList = new List<string>();
        private List<string> extraArticlesList = new List<string>();
        public List<ArticalExtraOptionalModel> selArticleExtraOptionalPerson = new List<ArticalExtraOptionalModel>();
        public List<ArticalExtraOptionalModel> selArticleExtraOptionalPersonDelete = new List<ArticalExtraOptionalModel>();
        private List<ArrangementRoomsArticle> extraArticlesListMod = new List<ArrangementRoomsArticle>();
        public List<ArticalExtraOptionalModel> selArticleExtraOptional;
       
        // Layout file names for all grids
        private string layoutArrangementBookPerson = MainForm.gridFiltersFolder + "\\layoutArrangementBookPerson.xml";
        private string layoutArrangementBookPersonArticles = MainForm.gridFiltersFolder + "\\layoutArrangementBookPersonArticlesAccomodation.xml";
        private string layoutInvoiceBookPersonArticles = MainForm.gridFiltersFolder + "\\layoutInvoiceBookPersonArticles.xml";

        private string layoutTravelersGrid = MainForm.gridFiltersFolder + "\\layoutTravelersGrid.xml";

        Boolean isBookedFull = false;

        private bool pageLoaded = false;

        public frmArrangementBookingPerson(ArrangementBookModel arrBook, int fld1, int fld2, int fld4, int nrAnchorage , bool addnew, Boolean isReserved)
        {
            //==== prenosi sadrzaj 4 polja za medical (weelchairs ...
            xrollator = fld2;
            xrolstool = fld1;  // obrnuo 1 2 
            //xarmalt = fld3;
            xarmafa = fld4;
            xnrAnchorage = nrAnchorage;
            //============================================

            this.Icon = Login.iconForm;
            string formName = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();
            formName = formName + " " + new ArrangementBUS().GetArrangementById(arrBook.idArrangement).nameArrangement;

            this.Text = formName;

            InitializeComponent();

            arrBookModel = arrBook;
            btnSave.Click += btnSave_Click;
            arrBookBUS = new ArrangementBookBUS();
            ////  pronalazi koliko je vec knjizeno medicala

            //Rollator
            rfld2 = arrBookBUS.GetBookPersMedic(new List<int> { 446, 447, 448 }, arrBookModel.idArrangement);  // ovde umesto rfld1 -> rfld2

            //Rolstoel
            rfld1 = arrBookBUS.GetBookPersMedic(new List<int> { 441, 442, 449, 450, 451, 452, 453 }, arrBookModel.idArrangement);

            //Arm sometimes
            rfld4 = arrBookBUS.GetBookPersMedic(new List<int> { 439, 440 }, arrBookModel.idArrangement);

            //Anchorage
            int fldAnchorage = arrBookBUS.GetBookPersMedicMoreAns(new List<int> { 823 }, arrBookModel.idArrangement);

            rnrAnchorage = (fldAnchorage);

           if (arrBookModel.dtBooked == null)
           {
               dtBooked.Text = DateTime.Now.ToShortDateString();
           }
           else
           {
               dtBooked.Text = arrBookModel.dtBooked.ToShortDateString();
           }

           if (addnew == true)
           {
               btnperson.Visible = true;
           }
           else
           {
               btnperson.Visible = false;
           }
           isBookedFull = isReserved;
                   
        }
        void mySubMenuPerson_Click(object sender, EventArgs e)
        {
            // PersonModel m = (PersonModel)varijablaIModela;            
            PersonBUS pbus = new PersonBUS();
            List<IModel> lista = new List<IModel>();

            Cursor.Current = Cursors.WaitCursor;
            List<int> labels = new List<int>();
            lista = pbus.GetAllPersons(0, labels, Login._user.lngUser);
            Cursor.Current = Cursors.Default;


            using (var dlgSave = new GridLookupForm(lista, "Person"))
            {

                if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                {
                    PersonModel genm = new PersonModel();
                    genm = (PersonModel)dlgSave.selectedRow;

                    arrBookModel.idDebitor = genm.idContPers;
                    arrBookModel.typeDebitor = "P";
                    txtDebitor.Text = genm.fullname;
                    
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

        }

        void mySubMenuClient_Click(object sender, EventArgs e)
        {
            // PersonModel m = (PersonModel)varijablaIModela;
            ClientBUS bus = new ClientBUS();
            List<IModel> lista = new List<IModel>();

            Cursor.Current = Cursors.WaitCursor;
            lista = bus.GetAllClients(Login._user.lngUser);
            Cursor.Current = Cursors.Default;
            using (var dlgSave = new GridLookupForm(lista, "Clients"))
            {

                if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                {
                    ClientModel genm = new ClientModel();
                    genm = (ClientModel)dlgSave.selectedRow;

                    arrBookModel.idDebitor = genm.idClient;
                    arrBookModel.typeDebitor = "C";
                    txtDebitor.Text = genm.nameClient.Trim();
                    

                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

        }

        void mySubMenuClear_Click(object sender, EventArgs e)
        {
            arrBookModel.idDebitor = 0;
            arrBookModel.typeDebitor = String.Empty;
            txtDebitor.Text = "";
            
        }
        private void frmArrangementBookingPerson_Load(object sender, EventArgs e)
        {
            radRibbonMemo.Visibility = ElementVisibility.Collapsed;
            btnReport.Visibility = ElementVisibility.Visible;
            btnEmail.Visibility = ElementVisibility.Collapsed;
            btnWord.Visibility = ElementVisibility.Collapsed;
            btnNewMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteDoc.Visibility = ElementVisibility.Visible;
            btnNewDoc.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;
            btnDeleteDoc.Visibility = ElementVisibility.Collapsed;
            radRibbonDocuments.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupPurchase.Visibility = ElementVisibility.Collapsed;
            btnPurchase.Visibility = ElementVisibility.Collapsed;
            btnDelPurchase.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupTraveler.Visibility = ElementVisibility.Collapsed;
            //btnTravelerAdd.Visibility = ElementVisibility.Collapsed;
            //btnDeleteTraveler.Visibility = ElementVisibility.Collapsed;

            radMenuButtonItem1.Click -= mySubMenuPerson_Click;
            radMenuButtonItem1.Click += mySubMenuPerson_Click;
            radMenuButtonItem2.Click -= mySubMenuClient_Click;
            radMenuButtonItem2.Click += mySubMenuClient_Click;
            radMenuButtonItem3.Click -= mySubMenuClear_Click;
            radMenuButtonItem3.Click += mySubMenuClear_Click;


            txtinitialsContPers.ReadOnly = true;
            txtFirstName.ReadOnly = true;
            txtLastName.ReadOnly = true;
            txtMidName.ReadOnly = true;


            //===== butoni za invoice ======================================
            RadMenuItem myRadMenuItem = new RadMenuItem();
            myRadMenuItem.Text = "Make invoice";
            ddlInvoice.Items.Add(myRadMenuItem);
            myRadMenuItem.Click += new EventHandler(btnInvoice_Click);

            RadMenuItem myRadMenuItem1 = new RadMenuItem();
            myRadMenuItem1.Text = "Make PBG invoice";
            ddlInvoice.Items.Add(myRadMenuItem1);
            myRadMenuItem1.Click += new EventHandler(btnInvoice_Click);
            myRadMenuItem1.Enabled = false;

            RadMenuItem myRadMenuItem2 = new RadMenuItem();
            myRadMenuItem2.Text = "Add invoice";
            ddlInvoice.Items.Add(myRadMenuItem2);
            myRadMenuItem2.Click += new EventHandler(btnAddInvoice_Click);

            RadMenuItem myRadMenuItem3 = new RadMenuItem();
            myRadMenuItem3.Text = "Cancel invoice";
            ddlInvoice.Items.Add(myRadMenuItem3);
            myRadMenuItem3.Click += new EventHandler(btnCancel_Click);

            RadMenuItem myRadMenuItem4 = new RadMenuItem();
            myRadMenuItem4.Text = "Delete invoice";
            ddlInvoice.Items.Add(myRadMenuItem4);
            myRadMenuItem4.Click += new EventHandler(btnDelete_Click);

            //============================================================
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(myRadMenuItem.Text) != null)
                    myRadMenuItem.Text = resxSet.GetString(myRadMenuItem.Text);
                if (resxSet.GetString(myRadMenuItem1.Text) != null)
                    myRadMenuItem1.Text = resxSet.GetString(myRadMenuItem1.Text);
                if (resxSet.GetString(myRadMenuItem2.Text) != null)
                    myRadMenuItem2.Text = resxSet.GetString(myRadMenuItem2.Text);
                if (resxSet.GetString(myRadMenuItem3.Text) != null)
                    myRadMenuItem3.Text = resxSet.GetString(myRadMenuItem3.Text);
                if (resxSet.GetString(myRadMenuItem4.Text) != null)
                    myRadMenuItem4.Text = resxSet.GetString(myRadMenuItem4.Text);

            }
            //==============================================================

            rpvVoucher.SelectedPage = tabAccomodation;


            fillPanelStatus();
            fillPanelTravelPapers();
            setTranslation();
            selArticlesAccomodation = new List<ArrangementRoomsArticle>();

            if(arrBookModel.idArrangementBook>0)
            {
                arrBookBUS = new ArrangementBookBUS();
                iID = arrBookModel.idArrangementBook;
                arrBookModel = arrBookBUS.GetArrangementBook(arrBookModel.idArrangementBook);
                fillData();
                arrangementBookPerson = new PersonBUS().GetArrangementPersons(arrBookModel.idArrangementBook, Login._user.lngUser);
                rgvPersons.DataSource = arrangementBookPerson;
                arrBookBUS = new ArrangementBookBUS();
                // ubaceno punjenje grida za fakturu //
                InvoiceBUS iib = new InvoiceBUS();
                List<InvoiceModel> iim = new List<InvoiceModel>();
                iim = iib.GetInvoiceCustomerAndVoucher(arrBookModel.idArrangementBook);
                gridInvoice.DataSource = null;
                gridInvoice.DataSource = iim;
                gridInvoice.Show();
                //=====================================//               
            }
            else
            {
                if (arrBookModel != null)
                {
                    if (arrBookModel.idContPers > 0)
                    {
                        PersonBookModel pbm = new PersonBUS().GetPersonBookedForArrangement(Login._user.lngUser, arrBookModel.idContPers);

                        txtperson.Text = pbm.fullname;
                        txtFirstName.Text = pbm.firstname;
                        txtMidName.Text = pbm.midname;
                        txtinitialsContPers.Text = pbm.initialsContPers;
                        txtLastName.Text = pbm.lastname;
                        dtbirthdate.Value = pbm.birthdate;
                    }
                }
                else
                {
                    // ubaciti status option true;
                }
            }


            loadInvoiceOptions();

            //all articles load

            LoadBookArticleGrids();
            checkedRows();

            LoadExtraOptionalGrid();
            checkedRowsExtraOptional();

            LoadBoardingPoint();
            LoadTravelersDropDown();
            LoadTravelersGrid();
            pageLoaded = true;
            
        }

        private void LoadBookArticleGrids()
        {
            if (arrBookModel.idContPers != 0)
            {
                ArticalBUS abus = new ArticalBUS();
                selArticlesAccomodation = abus.GetAllBookedRoomsForArrangement(arrBookModel.idArrangement);
                if (selArticlesAccomodation == null)
                    selArticlesAccomodation = new List<ArrangementRoomsArticle>();
               
                //grid accomodatio
                modelArticlesAccomodation = new List<ArrangementRoomsArticle>();
                modelArticlesAccomodation = abus.GetAllArticalsRoomsForArrangement(arrBookModel.idArrangement, Login._user.lngUser);

                rgvExtraArticles.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;
                rgvExtraArticles.DataSource = null;
                rgvExtraArticles.Columns.Clear();

                rgvExtraArticles.DataSource = modelArticlesAccomodation;
                rgvExtraArticles.AllowAutoSizeColumns = true;

                GridViewCheckBoxColumn chk2 = new GridViewCheckBoxColumn();
                chk2.Name = "isChecked";
                chk2.HeaderText = "Add/Not";
                rgvExtraArticles.Columns.Add(chk2);

                rgvExtraArticles.Columns.Move(rgvExtraArticles.Columns.Count - 1, 0);
                rgvExtraArticles.Columns["isChecked"].MaxWidth = (int)(this.CreateGraphics().MeasureString(rgvExtraArticles.Columns["isChecked"].HeaderText, this.Font).Width + 10);

            }
        }
       
        private void fillData()
        {
            PersonBookModel pbm = new PersonBUS().GetPersonBookedForArrangement( Login._user.lngUser, arrBookModel.idContPers);

            txtperson.Text = pbm.fullname;
            txtFirstName.Text = pbm.firstname;
            txtMidName.Text = pbm.midname;
            txtinitialsContPers.Text = pbm.initialsContPers;
            txtLastName.Text = pbm.lastname;
            dtbirthdate.Value = pbm.birthdate;
            if (arrBookModel.isInsurance == true)
            {
                chkInsurance.Checked = true;
            }
            else
            {
                chkInsurance.Checked = false;
            }
            if (arrBookModel.isCancelInsurance == true)
            {
                chkCancelInsurance.Checked = true;
            }
            else
            {
                chkCancelInsurance.Checked = false;
            }

            if (arrBookModel.isMedicalDevices == true)
            {
                chkMedicalDevices.Checked = true;
            }
            else
            {
                chkMedicalDevices.Checked = false;
            }

            if(arrBookModel.idDebitor > 0)
            {
                if(arrBookModel.typeDebitor == "P")
                {
                    PersonBUS npb = new PersonBUS();
                    PersonModel npm = new PersonModel();                    
                    npm = npb.GetPerson(arrBookModel.idDebitor);
                    
                    if(npm != null)
                        txtDebitor.Text = npm.firstname + " " + npm.midname + npm.lastname;
                }
                else if(arrBookModel.typeDebitor == "C")
                {
                    ClientBUS cbus = new ClientBUS();
                    ClientModel cmod = new ClientModel();
                    cmod = cbus.GetClient(arrBookModel.idDebitor);

                    if (cmod != null)
                        txtDebitor.Text = cmod.nameClient;
                }
                else
                {
                    txtDebitor.Text = "";
                }
            }


            if(arrBookModel.idStatus>0)
            {
                if (panelStatus.Controls.Find("rbnStatus" + arrBookModel.idStatus.ToString(), true).Length > 0)
                {
                    RadRadioButton rbn = (RadRadioButton)panelStatus.Controls.Find("rbnStatus" + arrBookModel.idStatus.ToString(), true)[0];
                    rbn.CheckState = CheckState.Checked;
                }
                if (arrBookModel.idStatus == 3)
                {
                    for (int i = 0; i < rpvVoucher.Pages.Count; i++)
                    {
                        rpvVoucher.Pages[i].Enabled = false;
                    }
                }
            }
            if (arrBookModel.idTravelPapers > 0)
            {
                if (panelTravelPapers.Controls.Find("rbnTravelPapers" + arrBookModel.idTravelPapers.ToString(), true).Length > 0)
                {
                    RadRadioButton rbn = (RadRadioButton)panelTravelPapers.Controls.Find("rbnTravelPapers" + arrBookModel.idTravelPapers.ToString(), true)[0];
                    rbn.CheckState = CheckState.Checked;
                }
            }

            arrStatus = arrBookModel.idStatus;

            //ako je final 
            if (arrBookModel.idStatus == 2)
            {
                btnperson.Enabled = false;
                btnAddPersons.Enabled = false;
                panelStatus.Enabled = false;
                panelTravelPapers.Enabled = false;
                btnTravelerAdd.Enabled = false;
                btnTravelerRemove.Enabled = false;
                rgvExtraOptional.ReadOnly = true;
            }


            //ako je status canceled
            if (arrBookModel.idStatus == 4)
            {
                //btnOpenPerson.Enabled = false;
                btnperson.Enabled = false;
                btnAddPersons.Enabled = false;
                panelStatus.Enabled = false;
                panelTravelPapers.Enabled = false;
                btnTravelerAdd.Enabled = false;
                btnTravelerRemove.Enabled = false;

                rgvPersons.Enabled = false;
                rgvExtraArticles.Enabled = false;
                rgvExtraOptional.Enabled = false;
                dropdownBoardingPoint.Enabled = false;

            }

            // Ako neko drugi placa za njega
            if (arrBookModel.idContPers != arrBookModel.idPayInvoice && arrBookModel.idPayInvoice != 0)
            {
                PersonBUS npb = new PersonBUS();
                PersonModel npm = new PersonModel();
                npm = npb.GetPerson(arrBookModel.idPayInvoice);
                if (npm != null)
                {
                    txtPayInvoice.Text = npm.firstname + " " + npm.midname + " " + npm.lastname;
                    lblPayInvoice.Visible = true;
                    txtPayInvoice.Visible = true;
                }
            }
        }

        private void fillPanelStatus()
        {
            ArrangementBookStatusBUS arrBookStatus = new ArrangementBookStatusBUS();
            List<ArrangementBookStatusModel> arrBookStatusModel = new List<ArrangementBookStatusModel>();
            arrBookStatusModel = arrBookStatus.GetAllStatus(Login._user.lngUser);
           if(arrBookStatusModel!=null)
           {
               if(arrBookStatusModel.Count>0)
               {
                   int Y = 0;
                   
                   for(int i = 0;i<arrBookStatusModel.Count;i++)
                   {
                       RadRadioButton rbn = new RadRadioButton();
                       rbn.Font = new Font("Verdana", 9);
                       rbn.ThemeName = panelInformation.ThemeName;
                       rbn.Name = "rbnStatus" + arrBookStatusModel[i].idStatus.ToString();
                       rbn.Text = arrBookStatusModel[i].nameStatus;
                       rbn.CheckStateChanging += rbnStatus_CheckStateChanging;
                       rbn.CheckStateChanged += rbnStatus_CheckStateChanged;
                       rbn.Location = new Point(0, Y);
                       rbn.AutoSize = true;
                       Y = Y + 3 + rbn.Height;
                       if (isBookedFull == true && arrBookStatusModel[i].idStatus != 3)
                           rbn.Enabled = false;

                       if (arrBookStatusModel[i].idStatus == 4 || arrBookStatusModel[i].idStatus == 5)
                           rbn.Enabled = false;

                       panelStatus.Controls.Add(rbn);
                   }
               }
           }
        }

        private void setTranslation()
        {

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(radMenuItemArrangementBookPerson.Text) != null)
                    radMenuItemArrangementBookPerson.Text = resxSet.GetString(radMenuItemArrangementBookPerson.Text);
                if (resxSet.GetString(radMenuItemArrangementBookPersonArticles.Text) != null)
                    radMenuItemArrangementBookPersonArticles.Text = resxSet.GetString(radMenuItemArrangementBookPersonArticles.Text);
                if (resxSet.GetString(lblPerson.Text) != null)
                    lblPerson.Text = resxSet.GetString(lblPerson.Text);

                for (int i = 0; i < ribbonExampleMenu.CommandTabs.Count; i++)
                {
                    if (resxSet.GetString(ribbonExampleMenu.CommandTabs[i].Text) != null)
                        ribbonExampleMenu.CommandTabs[i].Text = resxSet.GetString(ribbonExampleMenu.CommandTabs[i].Text);
                    RibbonTab ri = (RibbonTab)ribbonExampleMenu.CommandTabs[i];
                    for (int j = 0; j < ri.Items.Count; j++)
                    {
                        if (ri.Items[j].Visibility == ElementVisibility.Visible)
                        {
                            if (resxSet.GetString(ri.Items[j].Text) != null)
                                ri.Items[j].Text = resxSet.GetString(ri.Items[j].Text);
                            RadRibbonBarGroup rgb = (RadRibbonBarGroup)ri.Items[j];
                            for (int n = 0; n < rgb.Items.Count; n++)
                            {
                                if (resxSet.GetString(rgb.Items[n].Text) != null)
                                    rgb.Items[n].Text = resxSet.GetString(rgb.Items[n].Text);
                            }
                        }
                    }
                }

                if (resxSet.GetString(lblFirstName.Text) != null)
                    lblFirstName.Text = resxSet.GetString(lblFirstName.Text);
                if (resxSet.GetString(lblLastName.Text) != null)
                    lblLastName.Text = resxSet.GetString(lblLastName.Text);
                if (resxSet.GetString(lblMidName.Text) != null)
                    lblMidName.Text = resxSet.GetString(lblMidName.Text);
                if (resxSet.GetString(lblInitialsTitle.Text) != null)
                    lblInitialsTitle.Text = resxSet.GetString(lblInitialsTitle.Text);
                if (resxSet.GetString(lblDateBooked.Text) != null)
                    lblDateBooked.Text = resxSet.GetString(lblDateBooked.Text);
                if (resxSet.GetString(lblBirthDate.Text) != null)
                    lblBirthDate.Text = resxSet.GetString(lblBirthDate.Text);
                if (resxSet.GetString(chkInsurance.Text) != null)
                    chkInsurance.Text = resxSet.GetString(chkInsurance.Text);
                if (resxSet.GetString(chkCancelInsurance.Text) != null)
                    chkCancelInsurance.Text = resxSet.GetString(chkCancelInsurance.Text);

                if (resxSet.GetString(lblTravelers.Text) != null)
                    lblTravelers.Text = resxSet.GetString(lblTravelers.Text);
                if (resxSet.GetString(btnTravelerAdd.Text) != null)
                    btnTravelerAdd.Text = resxSet.GetString(btnTravelerAdd.Text);
                if (resxSet.GetString(btnTravelerRemove.Text) != null)
                    btnTravelerRemove.Text = resxSet.GetString(btnTravelerRemove.Text);
                if (resxSet.GetString(lblTravelPapers.Text) != null)
                    lblTravelPapers.Text = resxSet.GetString(lblTravelPapers.Text);

                if (resxSet.GetString(radMenuItemIvoice.Text) != null)
                    radMenuItemIvoice.Text = resxSet.GetString(radMenuItemIvoice.Text);
            }
        }

        private void checkedRows()
        {
            isLoaded = true;
            if (selArticlesAccomodation != null && rgvExtraArticles != null)
            {
                if (arrBookModel.idStatus != 4)
                {
                    for (int n = 0; n < rgvExtraArticles.RowCount; n++)
                    {
                        if (selArticlesAccomodation.Find(s => s.idArticle == rgvExtraArticles.Rows[n].Cells["idArticle"].Value.ToString() && s.isContract == Convert.ToBoolean(rgvExtraArticles.Rows[n].Cells["isContract"].Value.ToString()) && s.id == Convert.ToInt32(rgvExtraArticles.Rows[n].Cells["id"].Value.ToString()) && s.idRoom == rgvExtraArticles.Rows[n].Cells["idRoom"].Value.ToString() && s.idContPers == Convert.ToInt32(rgvExtraArticles.Rows[n].Cells["idContPers"].Value.ToString())) != null)
                        {
                            if (Convert.ToInt32(rgvExtraArticles.Rows[n].Cells["idContPers"].Value.ToString()) == arrBookModel.idContPers)
                                rgvExtraArticles.Rows[n].Cells["isChecked"].Value = true;
                            else
                                rgvExtraArticles.Rows[n].Cells["isChecked"].Value = false;
                        }
                        else
                            rgvExtraArticles.Rows[n].Cells["isChecked"].Value = false;
                    }
                }
            }
            isLoaded = false;
        }

        private bool anyRoomSelected()
        {
            if (arrBookModel.idStatus == 2 || arrBookModel.idStatus == 1)
            {
                if (modelArticlesAccomodation != null)
                    foreach (ArrangementRoomsArticle arr in modelArticlesAccomodation)
                    {
                        if (arr.name == txtperson.Text)
                            return false;
                    }
                return true;
            }

            else
                return false;

        }

        private int checkFields()
        {
            // ako je final vraca status 2

            if (arrBookModel.idStatus == 2 && arrStatus == 2)
            {
                translateRadMessageBox tr = new translateRadMessageBox();

                DialogResult dr = tr.translateAllMessageBoxDialog("Booking status is final. Only room changes and boardingpoints will be saved.", "Warning");

                if (dr == DialogResult.Yes)
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
            }
            else if (txtperson.Text == "")
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to add person!");
                return 0;
            }
            else if (arrBookModel.idStatus == 0)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to add status!");
                return 0;
            }
            else if (arrBookModel.idTravelPapers == 0)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to add status for traveler papers!");
                return 0;
            }
            else return 1;
        }

        private Boolean savePersons()
        {

            Boolean isSuccessful = true;
            arrBookBUS = new ArrangementBookBUS();
            ArrangementBookPersonsBUS nabb = new ArrangementBookPersonsBUS();  //== za ubacivanje ko placa fakturu
            if (arrangementBookPerson != null)
            {
                for (int i = 0; i < arrangementBookPerson.Count; i++)
                {
                    if (iID > 0 && arrangementBookPerson[i].idContPers > 0)
                    {
                        
                        if (i == 0)
                        {
                            if (arrBookBUS.Delete(iID, this.Name, Login._user.idUser) == false)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                                return false;
                            }
                        }
                        if (arrBookBUS.SavePersons(iID, arrangementBookPerson[i].idContPers, Login._user.idUser, DateTime.Now, Login._user.idUser, DateTime.Now, this.Name, Login._user.idUser) == false)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translatePartAndNonTranslatedPart("You have not succesufully inserted person", arrangementBookPerson[i].fullname);
                            return false;
                        }
                    }
                    //=== ubacuje u vouchere osoba iz grida da on placa za njih
                    bool isOk = false;
                    isOk = nabb.UpdatePayInvoicePerson(arrBookModel.idArrangement, arrangementBookPerson[i].idContPers, arrBookModel.idContPers, this.Name, Login._user.idUser);
                    if (isOk == false)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translatePartAndNonTranslatedPart("Error Updatin who pay invoice", arrangementBookPerson[i].fullname);
                        return false;
                    }
                }
                if (iID > 0 && arrangementBookPerson.Count == 0)
                {
                    if (arrBookBUS.Delete(iID, this.Name, Login._user.idUser) == false)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                        return false;
                    }
                }
            }
         
          
            return isSuccessful;
        }
 
        private Boolean saveExtraArticles()
        {
            Boolean isSuccessful = true;
            arrBookBUS = new ArrangementBookBUS();

            if (selArticlesAccomodation != null)
            {
                for (int i = 0; i < selArticlesAccomodation.Count; i++)
                {
                    if (iID > 0 && selArticlesAccomodation[i].idArticle != "")
                    {

                        if (i == 0)
                        {
                            if (arrBookBUS.DeleteExtraArticles(iID, this.Name, Login._user.idUser) == false)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                                return false;
                            }
                        }
                        if (selArticlesAccomodation[i].idContPers == arrBookModel.idContPers)
                        {
                            int uCreated = Login._user.idUser;
                            DateTime dCreated = DateTime.Now;
                            arrBookBUS = new ArrangementBookBUS();
                            List<ArrangementArticalForBookPersonModel> selArticles = new List<ArrangementArticalForBookPersonModel>();
                            selArticles = arrBookBUS.GetArrangementArticals(arrBookModel.idArrangement, arrBookModel.idArrangementBook);
                            if (selArticles != null)
                                if (selArticles.Find(s => s.isContract == selArticlesAccomodation[i].isContract && s.id == selArticlesAccomodation[i].id && s.idArticle == selArticlesAccomodation[i].idArticle && selArticlesAccomodation[i].idContPers == arrBookModel.idContPers) != null)
                                    if (selArticles.SingleOrDefault(s => s.isContract == selArticlesAccomodation[i].isContract && s.id == selArticlesAccomodation[i].id && s.idArticle == selArticlesAccomodation[i].idArticle && selArticlesAccomodation[i].idContPers == arrBookModel.idContPers).idUserCreated != 0)
                                    {
                                        uCreated = selArticles.SingleOrDefault(s => s.isContract == selArticlesAccomodation[i].isContract && s.id == selArticlesAccomodation[i].id && s.idArticle == selArticlesAccomodation[i].idArticle && selArticlesAccomodation[i].idContPers == arrBookModel.idContPers).idUserCreated;
                                        dCreated = selArticles.SingleOrDefault(s => s.isContract == selArticlesAccomodation[i].isContract && s.id == selArticlesAccomodation[i].id && s.idArticle == selArticlesAccomodation[i].idArticle && selArticlesAccomodation[i].idContPers == arrBookModel.idContPers).dtUserCreated;
                                    }

                            if (arrBookBUS.SaveArticles(iID, selArticlesAccomodation[i].idArticle, selArticlesAccomodation[i].isContract, selArticlesAccomodation[i].id, selArticlesAccomodation[i].idRoom, uCreated, dCreated, Login._user.idUser, DateTime.Now, this.Name, Login._user.idUser) == false)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translatePartAndNonTranslatedPart("You have not succesufully inserted article for", new PersonBUS().GetPerson(arrBookModel.idContPers).fullname);
                                return false;
                            }
                        }
                    }
                }
                if (iID > 0 && selArticlesAccomodation.Count == 0)
                {
                    if (arrBookBUS.DeleteExtraArticles(iID, this.Name, Login._user.idUser) == false)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                        return false;
                    }
                }
            }
            return isSuccessful;
        }

        private void rbnStatus_CheckStateChanging(object sender, CheckStateChangingEventArgs args)
        {
            if (pageLoaded == true)
            {
                RadRadioButton rrb = (RadRadioButton)sender;
                Control[] travelPapersStatusId1 = this.Controls.Find("rbnTravelPapers1", true);
                Boolean travelPapersOk = true;
                if (travelPapersStatusId1 != null)
                {
                    if (travelPapersStatusId1.Length > 0)
                    {
                        RadRadioButton rrbTravelPapers = (RadRadioButton)travelPapersStatusId1[0];
                        if (rrbTravelPapers.CheckState == CheckState.Unchecked)
                            if (Convert.ToInt32(rrb.Name.Replace("rbnStatus", "")) == 2)
                                travelPapersOk = false;
                    }
                }

                if (travelPapersOk == false)
                {
                    args.Cancel = true;
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You cannot check final status if status for travel papers isn't AF received!");
                }
            }
        }

        private void rbnStatus_CheckStateChanged(object sender, EventArgs e)
        {
            RadRadioButton rrb = (RadRadioButton)sender;
            
            if (rrb != null)
            {
                 if (rrb.CheckState == CheckState.Unchecked)
                    {
                        if (Convert.ToInt32(rrb.Name.Replace("rbnStatus", "")) == 3)
                        {
                            for (int i = 0; i < rpvVoucher.Pages.Count; i++)
                            {
                                rpvVoucher.Pages[i].Enabled = true;
                            }
                            if (arrBookModel != null)
                                if (arrBookModel.idContPers > 0)
                                {
                                    ArrangementBookBUS bus = new ArrangementBookBUS();
                                    // za status podeseno 0 bitno da je razlicito od Reserve - 3
                                    bus.UpdateVolLookup(arrBookModel.idArrangement, arrBookModel.idContPers, 0, this.Name, Login._user.idUser);
                                }
                        }
                    }
                    else if (rrb.CheckState == CheckState.Checked)
                    {
                        if (Convert.ToInt32(rrb.Name.Replace("rbnStatus", "")) == 3)
                        {
                            Boolean isClean = true;
                            if (selArticlesAccomodation != null)
                                if (selArticlesAccomodation.Count > 0)
                                {
                                    if (selArticlesAccomodation.Find(s => s.idContPers == arrBookModel.idContPers) != null)
                                    {
                                        translateRadMessageBox tr = new translateRadMessageBox();
                                        tr.translateAllMessageBox("First you have to delete articles.");
                                        rrb.CheckState = CheckState.Unchecked;
                                        if (panelStatus.Controls.Find("rbnStatus" + arrBookModel.idStatus.ToString(), true).Length > 0)
                                        {
                                            RadRadioButton rbn = (RadRadioButton)panelStatus.Controls.Find("rbnStatus" + arrBookModel.idStatus.ToString(), true)[0];
                                            rbn.CheckState = CheckState.Checked;
                                        }
                                        isClean = false;
                                    }
                                }
                                else if (rgvPersons != null)
                                {
                                    if (rgvPersons.Rows.Count > 0)
                                    {
                                        translateRadMessageBox tr = new translateRadMessageBox();
                                        tr.translateAllMessageBox("First you have to delete persons.");
                                        rrb.CheckState = CheckState.Unchecked;
                                        if (panelStatus.Controls.Find("rbnStatus" + arrBookModel.idStatus.ToString(), true).Length > 0)
                                        {
                                            RadRadioButton rbn = (RadRadioButton)panelStatus.Controls.Find("rbnStatus" + arrBookModel.idStatus.ToString(), true)[0];
                                            rbn.CheckState = CheckState.Checked;
                                        }
                                        isClean = false;
                                    }
                                }
                                else if (gridInvoice != null)
                                {
                                    if (gridInvoice.Rows.Count > 0)
                                    {
                                        translateRadMessageBox tr = new translateRadMessageBox();
                                        tr.translateAllMessageBox("First you have to clean invoices.");
                                        rrb.CheckState = CheckState.Unchecked;
                                        if (panelStatus.Controls.Find("rbnStatus" + arrBookModel.idStatus.ToString(), true).Length > 0)
                                        {
                                            RadRadioButton rbn = (RadRadioButton)panelStatus.Controls.Find("rbnStatus" + arrBookModel.idStatus.ToString(), true)[0];
                                            rbn.CheckState = CheckState.Checked;
                                        }
                                        isClean = false;
                                    }
                                }
                            if (isClean == true)
                            {
                                arrBookModel.idStatus = Convert.ToInt32(rrb.Name.Replace("rbnStatus", ""));
                                ArrangementBookBUS bus = new ArrangementBookBUS();
                                bus.UpdateVolLookup(arrBookModel.idArrangement, arrBookModel.idContPers, arrBookModel.idStatus, this.Name, Login._user.idUser);
                                if (arrBookModel.idStatus == 3)
                                {
                                    for (int i = 0; i < rpvVoucher.Pages.Count; i++)
                                    {
                                        rpvVoucher.Pages[i].Enabled = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            arrBookModel.idStatus = Convert.ToInt32(rrb.Name.Replace("rbnStatus", ""));
                        }
                    }
                        loadInvoiceOptions();
            }
        }

        private void btnAddPersons_Click(object sender, EventArgs e)
        {
            if (txtperson.Text == "")
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to add person!");
            }
            else
            {
                using (GridLookupFormPersons frm = new GridLookupFormPersons(new PersonBUS().GetArrangementPersonsLookup(arrBookModel.idArrangement, arrBookModel.idContPers, Login._user.lngUser), arrangementBookPerson, this.Name, arrBookModel.idArrangement, arrBookModel.idArrangementBook))
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        rgvPersons.DataSource = null;
                        arrangementBookPerson = frm.selMenus1;
                        if(arrangementBookPerson!=null)
                        foreach (PersonModel m in frm.selMenus1)
                        {
                            if (checkTravelerInList(m.idContPers) == false)
                            {
                                addTraveler(m);
                                int i = 0;
                                i = bookRoom(checkForEmptyBeds(m), m.idContPers);
                                if (i == 2)
                                {
                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("Room id is set to be the same as the person this person is traveling with");
                                }
                                else if (i == 1)
                                {
                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("There are no beds to put this person in the same room with the ones he is traveling with");
                                }
                                else if (i == 3)
                                {
                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("This person is already in this room!");
                                }
                            }
                        }
                        rgvPersons.DataSource = arrangementBookPerson;
                    }
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            dtBooked.Text = DateTime.Now.ToShortDateString();
        }

        private Boolean checkTravelerInList(int idContPers)
        {
            foreach(ArrangementTravelersModel atm in travelersList)
            {
                if (atm.idTravelWithPerson == idContPers)
                    return true;
            }
            return false;
        }



        private void btnOpenPerson_Click(object sender, EventArgs e)
        {
            if (arrBookModel.idContPers != 0)
            {
                PersonBUS bus = new PersonBUS();
                PersonModel model = new PersonModel();
                model = bus.GetPerson(arrBookModel.idContPers);

                if (model != null)
                {
                    using (frmPerson frm = new frmPerson(model))
                    {
                        frm.ShowDialog();
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Cannot find selected person");
                }

            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("No person selected");
            }
        }
        private bool updateBoardingPoint(){
            ArrangementBookBUS abb = new ArrangementBookBUS();
            return abb.UpdateBoardingPoint(arrBookModel.idArrangementBook, arrBookModel.idBoarding, this.Name, Login._user.idUser);
        
        }
        private void btnperson_Click(object sender, EventArgs e)
        {
            using (var dlgSave = new GridLookupTraveler(arrBookModel.idArrangement, arrBookModel.idContPers, "BookPerson"))
            {

                if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                {
                    PersonModel genm = new PersonModel();
                    genm = (PersonModel)dlgSave.selectedRow;
                    // set textbox
                    //==========  provera da li prekoracuje broj rollstula i ostalih polja
                    //Rollator
                    ArrangementBookBUS arbus = new ArrangementBookBUS();
                    int idcpr = arbus.GetBookPersMedicPers(new List<int> { 446, 447, 448 }, genm.idContPers);
                    if (idcpr == genm.idContPers)
                    {

                        if (xrollator == 0 && rfld2 != 0)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("There is no possibillity for Rollator");
                            return;
                        }
                        else
                        {
                            if (xrollator < rfld2 + 1)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("Exceeded number of Rollator");
                                return;
                            }
                        }
                    }
                    //Rolstoel

                    int idcpr1 = arbus.GetBookPersMedicPers(new List<int> { 441, 442, 449, 450, 451, 452, 453 }, genm.idContPers);
                    int xrolstoolNew = 0;
                    if (idcpr1 == genm.idContPers)
                    {
                        xrolstoolNew = rfld1 + 1;
                    }

                    if (xrolstool == 0 && xrolstoolNew != 0)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("There is no possibillity for wheelchair");
                        return;
                    }
                    if (xrolstool < xrolstoolNew)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Exceeded number of wheelchair");
                        return;
                    }

                    //Arm sometimes

                    int idcpr3 = arbus.GetBookPersMedicPers(new List<int> { 439, 440 }, genm.idContPers);
                    if (idcpr3 == genm.idContPers)
                    {
                        if (xarmafa == 0 && rfld4 != 0)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("There is no possibillity for arm sometimes");
                            return;
                        }
                        else
                            if (xarmafa < rfld4 + 1)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("Exceeded number of  arm sometimes");
                                return;
                            }
                    }

                    //Anchorage

                    int idcpra = arbus.GetBookPersMedicPers(new List<int> { 823 }, genm.idContPers);

                    int xAnchorageNew = 0;
                    if (idcpra == genm.idContPers)
                    {
                        xAnchorageNew = rnrAnchorage + 1;
                    }
                    if (xnrAnchorage == 0 && xAnchorageNew != 0)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("There is no possibillity for anchorage");
                        return;
                    }
                    if (xnrAnchorage < xAnchorageNew)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Exceeded number of anchorage");
                        return;
                    }


                    //=====================================================================

                    PersonBookModel pbm = new PersonBUS().GetPersonBookedForArrangement(Login._user.lngUser, genm.idContPers);

                    txtperson.Text = pbm.fullname;
                    txtFirstName.Text = pbm.firstname;
                    txtMidName.Text = pbm.midname;
                    txtinitialsContPers.Text = pbm.initialsContPers;
                    txtLastName.Text = pbm.lastname;
                    dtbirthdate.Value = pbm.birthdate;

                    int idContPers = 0;
                    idContPers = arrBookModel.idContPers;

                    //update model
                    arrBookModel.idContPers = genm.idContPers;
                    if (idContPers == 0)
                        loadInvoiceOptions();
                    if (dtBooked.Text == "")
                        dtBooked.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());


                    LoadBookArticleGrids();
                    LoadExtraOptionalGrid();
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            save();
        }

        private Boolean save()
        {
            Boolean isSuccessfull = true;
            if (anyRoomSelected())
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to select a room!");
                return false;
            }

            if (boardingPointNotSelected())
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to select boarding point");
                return false;
            }
            arrBookBUS = new ArrangementBookBUS();
            int checkfields = checkFields();
            if (checkfields == 1)
            {
                if (iID == -1)
                {
                    if (dtBooked.Text != "")
                        arrBookModel.dtBooked = Convert.ToDateTime(dtBooked.Text);
                    else
                        arrBookModel.dtBooked = DateTime.Now;

                    if (chkInsurance.Checked == true)
                        arrBookModel.isInsurance = true;
                    else
                        arrBookModel.isInsurance = false;

                    if (chkCancelInsurance.Checked == true)
                        arrBookModel.isCancelInsurance = true;
                    else
                        arrBookModel.isCancelInsurance = false;

                    if (chkMedicalDevices.Checked == true)
                        arrBookModel.isMedicalDevices = true;
                    else
                        arrBookModel.isMedicalDevices = false;

                    arrBookModel.idUserCreated = Login._user.idUser;
                    arrBookModel.dtUserCreated = DateTime.Now;
                    arrBookModel.idUserModified = Login._user.idUser;
                    arrBookModel.dtUserModified = DateTime.Now;


                    int result = arrBookBUS.Save(arrBookModel, this.Name, Login._user.idUser);
                    if (result > 0)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have  succesufully inserted booking data.");
                        if (arrBookModel.idStatus == 2 && arrStatus != 2)
                        {
                            arrStatus = 2;
                            btnperson.Enabled = false;
                            btnAddPersons.Enabled = false;
                            panelStatus.Enabled = false;
                            panelTravelPapers.Enabled = false;
                            loadInvoiceOptions();
                            rgvExtraOptional.ReadOnly = true;
                        }
                        iID = result;
                        arrBookModel.idArrangementBook = iID;
                        isSuccessfull = savePersons();
                        isSuccessfull = saveExtraArticles();
                        isSuccessfull = saveExtraOptional();
                        isSuccessfull = SaveTravelersGrid();
                        recalculationVolArr();

                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have not succesufully inserted booking data. Please check!");
                        isSuccessfull = false;
                    }
                }
                else
                {
                    if (dtBooked.Text != "")
                        arrBookModel.dtBooked = Convert.ToDateTime(dtBooked.Text);
                    else
                        arrBookModel.dtBooked = DateTime.Now;
                    if (chkInsurance.Checked == true)
                        arrBookModel.isInsurance = true;
                    else
                        arrBookModel.isInsurance = false;
                    if (chkCancelInsurance.Checked == true)
                        arrBookModel.isCancelInsurance = true;
                    else
                        arrBookModel.isCancelInsurance = false;

                    if (chkMedicalDevices.Checked == true)
                        arrBookModel.isMedicalDevices = true;
                    else
                        arrBookModel.isMedicalDevices = false;

                    arrBookModel.idUserModified = Login._user.idUser;
                    arrBookModel.dtUserModified = DateTime.Now;

                    if (arrBookBUS.Update(arrBookModel, this.Name, Login._user.idUser) == true)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have  succesufully updated booking data.");
                        if (arrBookModel.idStatus == 2 && arrStatus != 2)
                        {
                            arrStatus = 2;
                            btnperson.Enabled = false;
                            btnAddPersons.Enabled = false;
                            panelStatus.Enabled = false;
                            panelTravelPapers.Enabled = false;
                            loadInvoiceOptions();
                            
                        }
                        isSuccessfull = savePersons();
                        isSuccessfull = saveExtraArticles();
                        isSuccessfull = saveExtraOptional();
                        isSuccessfull = SaveTravelersGrid();
                        recalculationVolArr();

                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have not succesufully updated booking data. Please check!");
                        isSuccessfull = false;
                    }
                }

                //=====================================//
            }
            else if (checkfields == 2)
            {
                if (iID != -1)
                {
                    isSuccessfull = saveExtraArticles() && updateBoardingPoint();

                }
            }
            else if (checkfields == 0)
            {
                isSuccessfull = false;
            }
            else
            {

            }

            updateStatus();

            return isSuccessfull;
        }
        private Boolean updateDebitor(ArrangementBookModel model)
        {
            ArrangementBookBUS bus = new ArrangementBookBUS();
            return bus.UpdateDebitor(model, this.Name, Login._user.idUser);            
        }
        
        private Boolean saveExtraOptional()
        {
            Boolean isSuccessful = true;
            ArticalBUS abus = new ArticalBUS();
            if (selArticleExtraOptionalPerson != null)
            {
                if (selArticleExtraOptionalPerson.Count > 0)
                {

                    if (abus.DeleteSaveScript(iID, selArticleExtraOptionalPerson, this.Name, Login._user.idUser) == false)
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                    }
                }
            }


            return isSuccessful;
        }
        private void rgvPersons_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if (File.Exists(layoutArrangementBookPerson))
            {
                rgvPersons.LoadLayout(layoutArrangementBookPerson);
            }
            if (rgvPersons.Columns.Count > 0)
            {
                for (int i = 0; i < rgvPersons.Columns.Count; i++)
                {
                   
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString(rgvPersons.Columns[i].HeaderText) != null)
                            rgvPersons.Columns[i].HeaderText = resxSet.GetString(rgvPersons.Columns[i].HeaderText);
                    }
                }
            }
        }

        private void rgvExtraArticles_DataBindingComplete_1(object sender, GridViewBindingCompleteEventArgs e)
        {
            var grid = sender as RadGridView;

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                foreach (var column in rgvExtraArticles.Columns)
                {
                    if (resxSet.GetString(column.HeaderText) != null && resxSet.GetString(column.HeaderText) != "")
                        column.HeaderText = resxSet.GetString(column.HeaderText);
                    if (column.Name != "isChecked")
                        column.ReadOnly = true;

                    column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                }
            }
        }

        private void rgvExtraArticles_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (isLoaded == false)
            {

                if (this.rgvExtraArticles.ActiveEditor is RadCheckBoxEditor)
                {
                    if (rgvExtraArticles.CurrentRow.Cells["idArticle"].Value != null)
                    {
                        ArrangementRoomsArticle model = (ArrangementRoomsArticle)rgvExtraArticles.CurrentRow.DataBoundItem;


                        string codeArticles = rgvExtraArticles.CurrentRow.Cells["idArticle"].Value.ToString();
                        Boolean checkeds = Convert.ToBoolean(rgvExtraArticles.CurrentRow.Cells["isChecked"].Value);
                        bool chechstate = Convert.ToBoolean(rgvExtraArticles.ActiveEditor.Value);

                        int idcontpers = 0;
                        idcontpers = Convert.ToInt32(rgvExtraArticles.CurrentRow.Cells["idContPers"].Value);


                        if (idcontpers != 0 && arrBookModel.idContPers != idcontpers)
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("You cannot change bed for another person");
                            e.Cancel = true;
                        }
                        else
                        {
                            if (chechstate == false)
                            {
                                var bedexistinroom = selArticlesAccomodation.Find(s => s.idArticle == codeArticles && s.isContract == model.isContract & s.id == model.id && s.idContPers == arrBookModel.idContPers);

                                if (bedexistinroom == null)
                                {
                                    selArticlesAccomodation.Add(model);
                                    PersonBookModel pbm = new PersonBUS().GetPersonBookedForArrangement(Login._user.lngUser, arrBookModel.idContPers);
                                    if (pbm != null)
                                    {
                                        model.name = pbm.firstname + " " + pbm.midname + " " + pbm.lastname;
                                        model.nameGender = pbm.nameGender;
                                        model.idContPers = arrBookModel.idContPers;
                                        model.type = "Traveler";
                                        rgvExtraArticles.CurrentRow.InvalidateRow();
                                    }
                                    ArrangementRoomsArticle pm = new ArrangementRoomsArticle();
                                    pm = modelArticlesAccomodation.Find(item => item.idArticle == codeArticles && item.isContract == model.isContract && item.id == model.id);
                                    if (pm != null)
                                    {
                                        pm.idRoom = model.idRoom;
                                    }
                                }
                                else
                                {

                                    translateRadMessageBox trs = new translateRadMessageBox();
                                    trs.translateAllMessageBox("You already booked bed (" + codeArticles + ") for " + txtFirstName.Text + " " + txtLastName.Text);
                                    e.Cancel = true;
                                }
                            }

                            else
                            {
                                var result = selArticlesAccomodation.Find(s => s.idArticle == codeArticles && s.isContract == model.isContract && s.id == model.id && s.idContPers == arrBookModel.idContPers);
                                selArticlesAccomodation.Remove(result);

                                model.name = "";
                                model.nameGender = "";
                                model.idContPers = 0;
                                model.type = "";
                                rgvExtraArticles.CurrentRow.InvalidateRow();

                            }
                        }
                    }

                }
            }

        }

        private void radMenuItemArrangementBookPerson_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutArrangementBookPerson))
            {
                File.Delete(layoutArrangementBookPerson);
            }
            rgvPersons.SaveLayout(layoutArrangementBookPerson);
            translateRadMessageBox tr = new translateRadMessageBox();
            tr.translateAllMessageBox("Layout saved");
        }

        private void radMenuItemArrangementBookPersonArticles_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutArrangementBookPersonArticles))
            {
                File.Delete(layoutArrangementBookPersonArticles);
            }
            rgvExtraArticles.SaveLayout(layoutArrangementBookPersonArticles);
            translateRadMessageBox tr = new translateRadMessageBox();
            tr.translateAllMessageBox("Layout saved");
        }

        private void frmArrangementBookingPerson_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
        }

        # region Invoice 



        private void btnInvoice_Click(object sender, EventArgs e)
        {
            if (arrBookModel.idArrangementBook == 0)
            {
                if (save() == true)
                    makeInvoice();
            }
            else
            {
                    ArrangementBookModel abm = new ArrangementBookModel();
                    abm = new ArrangementBookBUS().GetArrangementBook(arrBookModel.idArrangementBook);
                    if (abm != null)
                    {
                        if (abm.idStatus != 2)
                            save();
                        else
                            updateDebitor(arrBookModel);
                        
                        makeInvoice();
                    }
            }


        }

        private void makeInvoice()
        {
            ArrangementCalculationBUS acb = new ArrangementCalculationBUS();
            if (acb.isCalculationFinished(arrBookModel.idArrangement) == true)
            {                
                ArrangementBUS arbs = new ArrangementBUS();
                List<LabelForArrangement> lbm = new List<LabelForArrangement>();
                lbm = arbs.GetLabelsArrangement(arrBookModel.idArrangement);
                if (lbm != null)
                {
                    if (lbm.Count > 1)
                        idLabel = 1;
                    else
                        idLabel = lbm[0].idLabel;

                }
                List<ArrangementInvoicePriceModel> www = new List<ArrangementInvoicePriceModel>();
                MakeInvoice inv = new MakeInvoice();
                Boolean isOk = inv.DoIt1(arrBookModel, www, idLabel, this.Name, Login._user.idUser);
                if (isOk == true)
                {
                    loadInvoiceOptions();                    
                }
                //===== citanje faktura
                InvoiceBUS iib = new InvoiceBUS();
                List<InvoiceModel> iim = new List<InvoiceModel>();
                iim = iib.GetInvoiceCustomerAndVoucher(arrBookModel.idArrangementBook);
                gridInvoice.DataSource = null;
                gridInvoice.DataSource = iim;
                gridInvoice.Show();
                //====
                if (iim != null)
                {
                    GridViewRowInfo info = this.gridInvoice.CurrentRow;
                    InvoiceModel selectedInvoice = (InvoiceModel)info.DataBoundItem;
                    _selectedRowInvoice = new InvoiceModel();
                    _clickedInvoice = new InvoiceModel();

                    if (info != null)
                        if (selectedInvoice != null)
                        {
                            using (frmInvoice frm = new frmInvoice(selectedInvoice))
                            {
                                frm.ShowDialog();
                            }
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            GC.Collect();

                            InvoiceBUS iibvv = new InvoiceBUS();
                            List<InvoiceModel> iimvv = new List<InvoiceModel>();
                            iimvv = iibvv.GetInvoiceCustomerAndVoucher(iID);  //arrBookModel.idArrangementBook
                            gridInvoice.DataSource = null;
                            gridInvoice.DataSource = iimvv;
                        }
                }
                else
                {
                    iim = iib.GetInvoiceCustomerAndVoucher(arrBookModel.idArrangementBook);
                    gridInvoice.DataSource = null;
                    gridInvoice.DataSource = iim;
                }
            }
            else
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("First you have to finish arrangement calculation.");
            }
            loadInvoiceOptions();
        }
  

        private void btnAddInvoice_Click(object sender, EventArgs e)
        {
            ArrangementCalculationBUS acb = new ArrangementCalculationBUS();
            if (acb.isCalculationFinished(arrBookModel.idArrangement) == true)
            {
              translateRadMessageBox tr = new translateRadMessageBox();
              if (tr.translateAllMessageBoxDialog("Make a new invoice ?", " ") == System.Windows.Forms.DialogResult.Yes)
              {
                  ArrangementBUS arbs = new ArrangementBUS();
                  ArrangementBookBUS bookbus = new ArrangementBookBUS();
                  bookbus.UpdateDebitor(arrBookModel, this.Name, Login._user.idUser);

                  List<LabelForArrangement> lbm = new List<LabelForArrangement>();
                  lbm = arbs.GetLabelsArrangement(arrBookModel.idArrangement);
                  if (lbm != null)
                  {
                      if (lbm.Count > 1)
                          idLabel = 1;
                      else
                          idLabel = lbm[0].idLabel;

                  }

                  
                  int rbr = 0;
                  string invNumber = "";
                  InvoiceBUS iibl = new InvoiceBUS();
                  List<InvoiceModel> iiml = new List<InvoiceModel>();
                  iiml = iibl.GetInvoiceCustomerAndVoucher(arrBookModel.idArrangementBook);
                  int b = iibl.GetCountExtension(arrBookModel.idArrangementBook);
                  if (iiml != null)
                  {
                      if (iiml[0].idVoucher != arrBookModel.idArrangementBook)
                      {
                          using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                          {
                              if (resxSet.GetString("Make basic invoice first") != null)
                              {
                                  RadMessageBox.Show(resxSet.GetString("Make basic invoice first"));
                                  return;
                              }
                              else
                              {
                                  RadMessageBox.Show("Make basic invoice first");
                                  return;
                              }
                          }
                      }
                      else
                      {

                          if (iiml.Count > 0)
                          {
                              int a = iiml.Count;

                              if (a == 1)
                              {
                                  rbr = 0;
                              }
                              else
                              {
                                  rbr = a - 1;
                              }
                              invNumber = iiml[rbr].invoiceNr.ToString();
                              MakeInvoice inv = new MakeInvoice();
                              if (b==Convert.ToInt32(iiml[rbr].invoiceRbr))
                                  b = b + 1;
                              rbr = b - 1;

                              int IiD = inv.DoItBlank(arrBookModel, rbr, invNumber, idLabel, this.Name, Login._user.idUser);

                              InvoiceModel im = new InvoiceModel();
                              im = new InvoiceBUS().GetInvoiceByIntID(IiD);

                              if(IiD!=0)
                                  if (im != null)
                                  {
                                      using (frmInvoice frm = new frmInvoice(im))
                                      {
                                          frm.ShowDialog();
                                      }
                                      GC.Collect();
                                      GC.WaitForPendingFinalizers();
                                      GC.Collect();
                                  }


                          }
                          else
                          {
                              using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                              {
                                  if (resxSet.GetString("Make basic invoice first") != null)
                                      RadMessageBox.Show(resxSet.GetString("Make basic invoice first"));
                                  else
                                      RadMessageBox.Show("Make basic invoice first");
                              }
                          }
                      }

                 
                  }
                  else
                  {
                      using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                      {
                          if (resxSet.GetString("Make basic invoice first") != null)
                              RadMessageBox.Show(resxSet.GetString("Make basic invoice first"));
                          else
                              RadMessageBox.Show("Make basic invoice first");
                      }

                  }

                  iiml = iibl.GetInvoiceCustomerAndVoucher(arrBookModel.idArrangementBook);
                  gridInvoice.DataSource = null;
                  gridInvoice.DataSource = iiml;
                  gridInvoice.Show();
              }
             }
            else
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("First you have to finish arrangement calculation.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            translateRadMessageBox tr = new translateRadMessageBox();
            if (tr.translateAllMessageBoxDialog("Cancel invoice ?", " ") == System.Windows.Forms.DialogResult.Yes)
            {
                if (gridInvoice.CurrentRow != null && gridInvoice.CurrentRow.Cells["idInvoice"].Value != null)
                {
                    //int invoice = Convert.ToInt32(gridInvoice.CurrentRow.Cells["idInvoice"].Value);
                    //MakeInvoice invCanc = new MakeInvoice();
                    //int IiD = invCanc.DoCancel(invoice);
                    //if (IiD < 1)
                    //{
                    //    translateRadMessageBox tr1 = new translateRadMessageBox();
                    //    tr1.translateAllMessageBox("Problem with Cancelation invoice !");
                    //}
                    //List<InvoiceModel> iiml = new List<InvoiceModel>();
                    //InvoiceBUS iibl = new InvoiceBUS();
                    //iiml = iibl.GetInvoiceCustomerAndVoucher(arrBookModel.idArrangementBook);
                    //gridInvoice.DataSource = null;
                    //gridInvoice.DataSource = iiml;
                    //gridInvoice.Show();

                    InvoiceModel inv = new InvoiceModel();
                    inv = (InvoiceModel)gridInvoice.CurrentRow.DataBoundItem;

                    PersonBookModel selectedModel = new PersonBookModel();
                    selectedModel = new PersonBUS().GetExactPersonBookForArrangementByIdArrangementBook(Convert.ToInt32(inv.idVoucher), Login._user.lngUser);
                    CancelArrangement ca = new CancelArrangement();
                    ArrangementModel arrange = new ArrangementModel();
                    arrange = new ArrangementBUS().GetArrangementById(arrBookModel.idArrangement);
                    if (arrange != null)
                        if (ca.cancel(selectedModel, arrange,this.Name,Login._user.idUser) == true)
                        {
                            updateStatus();
                            List<InvoiceModel> iiml = new List<InvoiceModel>();
                            InvoiceBUS iibl = new InvoiceBUS();
                            iiml = iibl.GetInvoiceCustomerAndVoucher(arrBookModel.idArrangementBook);
                            gridInvoice.DataSource = null;
                            gridInvoice.DataSource = iiml;
                            gridInvoice.Show();
                        }


                }
                else
                {
                    translateRadMessageBox tr1 = new translateRadMessageBox();
                    tr1.translateAllMessageBox("No invoice to cancel!");
                }

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            translateRadMessageBox tr = new translateRadMessageBox();
            if (tr.translateAllMessageBoxDialog("Delete invoice ?", " ") == System.Windows.Forms.DialogResult.Yes)
            {
                if (gridInvoice.CurrentRow != null)
                {
                    int invoice = Convert.ToInt32(gridInvoice.CurrentRow.Cells["idInvoice"].Value);
                    MakeInvoice invCanc = new MakeInvoice();
                    int IiD = invCanc.DoDelete(invoice, this.Name, Login._user.idUser);
                    if (IiD < 1)
                    {
                        translateRadMessageBox tr1 = new translateRadMessageBox();
                        tr1.translateAllMessageBox("Problem with Delete invoice !");
                    }
                    else
                    {
                        loadInvoiceOptions();
                    }
                    List<InvoiceModel> iiml = new List<InvoiceModel>();
                    InvoiceBUS iibl = new InvoiceBUS();
                    iiml = iibl.GetInvoiceCustomerAndVoucher(arrBookModel.idArrangementBook);
                    gridInvoice.DataSource = null;
                    gridInvoice.DataSource = iiml;
                    gridInvoice.Show();

                    if (gridInvoice.Rows.Count > 0)
                        btnLookupCliPers.Enabled = false;
                    else
                        btnLookupCliPers.Enabled = true;
                }
                else
                {
                    translateRadMessageBox tr1 = new translateRadMessageBox();
                    tr1.translateAllMessageBox("No invoice to delete!");
                }

            }
        }

        private void gridInvoice_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if (File.Exists(layoutInvoiceBookPersonArticles))
            {
                gridInvoice.LoadLayout(layoutInvoiceBookPersonArticles);
            }
            if (gridInvoice.Columns.Count > 0)
            {
                for (int i = 0; i < gridInvoice.Columns.Count; i++)
                {

                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString(gridInvoice.Columns[i].HeaderText) != null)
                            gridInvoice.Columns[i].HeaderText = resxSet.GetString(gridInvoice.Columns[i].HeaderText);
                    }
                }
            }

            if (gridInvoice.Rows.Count > 0)
                btnLookupCliPers.Enabled = false;
            else
                btnLookupCliPers.Enabled = true;
        }

        private void gridInvoice_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row.DataBoundItem != null)
            {
                Type t = e.Row.DataBoundItem.GetType();
                if (t == typeof(InvoiceModel))
                {
                    GridViewRowInfo info = this.gridInvoice.CurrentRow;
                    InvoiceModel selectedInvoice = (InvoiceModel)info.DataBoundItem;
                    _selectedRowInvoice = new InvoiceModel();
                    _clickedInvoice = new InvoiceModel();

                    if (info != null && e.RowIndex >= 0)
                        if (selectedInvoice != null)
                        {
                            using (frmInvoice frm = new frmInvoice(selectedInvoice))
                            {
                                if (selectedInvoice.invoiceRbr.Trim() != "000")
                                {
                                    frm.ShowDialog();
                                    InvoiceBUS iib = new InvoiceBUS();
                                    List<InvoiceModel> iim = new List<InvoiceModel>();
                                    iim = iib.GetInvoiceCustomerAndVoucher(arrBookModel.idArrangementBook);
                                    gridInvoice.DataSource = null;
                                    gridInvoice.DataSource = iim;
                                }
                                else
                                {
                                    translateRadMessageBox msgbox = new translateRadMessageBox();
                                    msgbox.translateAllMessageBox("Cannot open 000 invoice. Please choose 001 instead.");
                                }
                            }

                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            GC.Collect();
                        }
                }
            }
        }

        private void loadInvoiceOptions()
        {
            bool noprint = false;
            ArrangementBookBUS anb = new ArrangementBookBUS();
            ArrangementBookModel anm = new ArrangementBookModel();
            if (arrangementBookPerson != null)
            {
                if (arrangementBookPerson.Count > 0)
                {
                    for (int q = 0; q < arrangementBookPerson.Count; q++)
                    {
                        anm = anb.GetArrangementBookForTraveler(arrBookModel.idArrangement, arrangementBookPerson[q].idContPers);
                        if (anm != null)
                            if (anm.idStatus < 2)  // nije final za onog kog placa;
                                noprint = true;
                    }
                }
            }
           
                if (arrBookModel.idStatus == 2)
                {
                    if (arrBookModel.idContPers != 0)
                    {
                        int invoiceNr = 0;
                        invoiceNr = new ArrangementBookBUS().checkIfArrangementBookIsInInvoice(arrBookModel.idArrangementBook);
                        if (invoiceNr != 0)
                        {
                            ddlInvoice.Items[0].Enabled = false;

                        }
                        else
                        {
                            ddlInvoice.Items[0].Enabled = true;
                        }
                        for (int i = 1; i < ddlInvoice.Items.Count; i++)
                        {
                            ddlInvoice.Items[i].Enabled = true;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < ddlInvoice.Items.Count; i++)
                        {
                            ddlInvoice.Items[i].Enabled = false;
                        }
                    }
                }


            if (arrBookModel.idStatus != 2 || txtPayInvoice.Visible==true)
            {
                for (int i = 0; i < ddlInvoice.Items.Count; i++)
                {
                    ddlInvoice.Items[i].Enabled = false;
                }
            }

            if (noprint == true)    // ne dozvoljava pravljeneje fakture ako svi ostali za koje placa nemaju status final
            {
                ddlInvoice.Items[0].Enabled = false;
              //  RadMessageBox.Show("Other travelers are not in status Final !!!");

            }
        }

        private void radMenuItemIvoice_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutInvoiceBookPersonArticles))
            {
                File.Delete(layoutInvoiceBookPersonArticles);
            }
            gridInvoice.SaveLayout(layoutInvoiceBookPersonArticles);
            translateRadMessageBox tr = new translateRadMessageBox();
            tr.translateAllMessageBox("Layout saved");
        }

        #endregion Invoice 

       

        # region Boarding point

        private void LoadBoardingPoint()
        {

            ArrangementBoardingPointBUS bus = new ArrangementBoardingPointBUS();
            List<BoardingPointModel> lista = new List<BoardingPointModel>();


            lista = bus.GetArrangementBoardingPoint(arrBookModel.idArrangement);

            DescriptionTextListDataItem descriptionItem = new DescriptionTextListDataItem();
            descriptionItem.Value = 0;
            descriptionItem.Text = "None";
            descriptionItem.DescriptionText = "None" + "\n";
            int height = 25 * 2;
            descriptionItem.Height = height;
            this.dropdownBoardingPoint.Items.Add(descriptionItem);
            this.dropdownBoardingPoint.SelectedIndex = 0;


            if (lista != null)
            {

                foreach (BoardingPointModel m in lista)
                {
                    descriptionItem = new DescriptionTextListDataItem();
                    descriptionItem.Height = height;
                    descriptionItem.Value = m.idBoardingPoint;
                    descriptionItem.Text = m.sortBoardingPoint + " - " + m.nameBoardingPoint;
                    descriptionItem.DescriptionText =  m.dtDeparture.ToString("dd.MM.y H:mm 'h'") + " - " + m.dtArrival.ToString("dd.MM.y H:mm 'h'") +"\n"+ m.addressBoardingPoint;
                    this.dropdownBoardingPoint.Items.Add(descriptionItem);
                }

            }

            ArrangementBookBUS abus = new ArrangementBookBUS();
            int boardingpoint = abus.GetArrangementBookBoardingPoint(arrBookModel.idArrangementBook, arrBookModel.idArrangement, arrBookModel.idContPers);

            dropdownBoardingPoint.SelectedValue = boardingpoint;

            DescriptionTextListDataItem item = dropdownBoardingPoint.SelectedItem as DescriptionTextListDataItem;
            lblBoardingPoint.Text = item.DescriptionText;
        }

        private bool boardingPointNotSelected()
        {
            if (arrBookModel.idStatus == 2 || arrBookModel.idStatus == 1)
            {

                if (dropdownBoardingPoint.SelectedIndex == 0)
                {
                    return true;
                }
            }
            return false;

        }

        private void dropdownBoardingPoint_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (pageLoaded == true)
            {
                arrBookModel.idBoarding = (int)dropdownBoardingPoint.SelectedValue;

                DescriptionTextListDataItem item = dropdownBoardingPoint.SelectedItem as DescriptionTextListDataItem;
                lblBoardingPoint.Text = item.DescriptionText;

            }
        }

        #endregion Boarding point

        #region Travelers

        private void LoadTravelersDropDown()
        {
            ArrangementBookPersonsBUS bus = new ArrangementBookPersonsBUS();
            List<PersonModel> travelers = new List<PersonModel>();
            travelers = bus.GetAllTravelersForArrangement(arrBookModel.idArrangement, arrBookModel.idContPers);

            DescriptionTextListDataItem descriptionItem = new DescriptionTextListDataItem();
            descriptionItem.Value = 0;
            this.dropdownTravelers.Items.Add(descriptionItem);
            this.dropdownTravelers.SelectedIndex = 0;

            if (travelers != null)
            {
                dropdownTravelers.DataSource = travelers;
                dropdownTravelers.ValueMember = "idContPers";
                dropdownTravelers.DisplayMember = "fullname";

            }

            this.dropdownTravelers.SelectedItem = descriptionItem;
        }

        private void LoadTravelersGrid()
        {
            ArrangementBookPersonsBUS bus = new ArrangementBookPersonsBUS();

            travelersList = bus.GetAllTravelersWith(arrBookModel.idContPers, arrBookModel.idArrangement);
            if (travelersList == null)
                travelersList = new BindingList<ArrangementTravelersModel>();


            gridTravelers.DataSource = travelersList;

            gridTravelers.Columns["id"].IsVisible = false;
            gridTravelers.Columns["idArrangement"].IsVisible = false;
            gridTravelers.Columns["idContPers"].IsVisible = false;
            gridTravelers.Columns["idTravelWithPerson"].IsVisible = false;
            gridTravelers.Columns["fullname"].IsVisible = false;
          
            //gridTravelers.Columns["firstnameTraveler"].Width = 150;
            //gridTravelers.Columns["midnameTraveler"].Width = 150;
            //gridTravelers.Columns["lastnameTraveler"].Width = 150;

        }

        private bool SaveTravelersGrid()
        {
            bool retval = false;

            ArrangementBookPersonsBUS bus = new ArrangementBookPersonsBUS();

            retval = bus.SaveTravelWith(travelersList, arrBookModel.idArrangement, arrBookModel.idContPers, this.Name, Login._user.idUser);
         
            return retval;

        }
        private int bookRoom(string idRoom, int idContPers) {
            if (idRoom == "")
                return 1;

            foreach (ArrangementRoomsArticle ara in modelArticlesAccomodation)
            {
                if (ara.idRoom.StartsWith(idRoom) && ara.idContPers == 0)
                {
                    ara.idContPers = idContPers;
                    idRoom = ara.idRoom;
                    break;
                }
                
                }
            for (int n = 0;n < rgvExtraArticles.RowCount; n++)
                {
                    if (rgvExtraArticles.Rows[n].Cells["idRoom"].Value.ToString() == idRoom)
                    {   
                        
                        
                       PersonBookModel pbm = new PersonBUS().GetPersonBookedForArrangement(Login._user.lngUser, arrBookModel.idContPers);
                        ArrangementRoomsArticle model = (ArrangementRoomsArticle)rgvExtraArticles.Rows[n].DataBoundItem;
                        model.name = pbm.firstname + " " + pbm.lastname;
                        model.nameGender = pbm.nameGender;
                        model.idContPers = arrBookModel.idContPers;
                        model.type = "Traveler";
                        model.idRoom = idRoom;
                        if (selArticlesAccomodation.SingleOrDefault(s => s.idArticle == model.idArticle && s.id == s.id && s.isContract == model.isContract && s.idContPers == model.idContPers) != null)
                            return 3;
                        selArticlesAccomodation.Add(model);
                        rgvExtraArticles.Rows[n].InvalidateRow();
                        //LoadBookArticleGrids();
                        rgvExtraArticles.Rows[n].Cells["isChecked"].Value = true;
                        return 2;
                    }
                       
                 }
                
            return 1;

        }
        private string checkForEmptyBeds(PersonModel m) 
        {
            string idRoom="";
            foreach (ArrangementRoomsArticle ara in selArticlesAccomodation) {
                if (ara.idContPers == m.idContPers) {
                    idRoom = ara.idRoom;
                    
                    break;
                }
            }
            if (idRoom != "")
            {
                idRoom = idRoom.Substring(0, idRoom.IndexOf('-')+1);
            }
            
                return idRoom;
        }
        private void btnTravelerAdd_Click(object sender, EventArgs e)
        {
            if (dropdownTravelers.DataSource != null)
            {
                if (dropdownTravelers.SelectedIndex != -1)
                {
                    PersonModel m = (PersonModel)dropdownTravelers.SelectedItem.DataBoundItem;

                    addTraveler(m);

                    int i = 0;
                    i = bookRoom(checkForEmptyBeds(m), m.idContPers);
                    if (i == 2)
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("Room id is set to be the same as the person this person is traveling with");
                    }
                    else if (i == 1)
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("There are no beds to put this person in the same room with the ones he is traveling with");
                    }
                    else if (i == 3)
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("This person is already in this room!");
                    }
                

                }
            }
        }

   
        private void addTraveler(PersonModel m)
        {
            bool found = false;
                    foreach (ArrangementTravelersModel s in travelersList)
                    {
                        if (s.idTravelWithPerson == m.idContPers)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (found == true)
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("Already in list");
                        return;
                    }

                    if (m != null)
                    {
                        ArrangementTravelersModel a = new ArrangementTravelersModel();

                        a.idContPers = arrBookModel.idContPers;
                        BindingList<ArrangementTravelersModel> ltm = new BindingList<ArrangementTravelersModel>();
                        ltm = new ArrangementBookPersonsBUS().GetTravelerForTravelerWith(arrBookModel.idContPers, arrBookModel.idArrangement);
                        if (ltm != null)
                        {
                            a.idContPers = ltm[0].idContPers;
                        }
                        a.idArrangement = arrBookModel.idArrangement;
                        a.idTravelWithPerson = m.idContPers;
                        a.firstnameTraveler = m.firstname;
                        a.midnameTraveler = m.midname;
                        a.lastnameTraveler = m.lastname;

                        travelersList.Add(a);


                        List<PersonModel> travelers = new List<PersonModel>();
                        travelers = (List<PersonModel>)dropdownTravelers.DataSource;
                        travelers.Remove(travelers.Find(s => s.idContPers == a.idTravelWithPerson));
                        dropdownTravelers.DataSource = null;

                        DescriptionTextListDataItem descriptionItem = new DescriptionTextListDataItem();
                        descriptionItem.Value = 0;
                        this.dropdownTravelers.Items.Add(descriptionItem);
                        this.dropdownTravelers.SelectedIndex = 0;

                        if (travelers != null)
                        {
                            dropdownTravelers.DataSource = travelers;
                            dropdownTravelers.ValueMember = "idContPers";
                            dropdownTravelers.DisplayMember = "fullname";

                        }

                        this.dropdownTravelers.SelectedItem = descriptionItem;
                    }
        }
        private void btnTravelerRemove_Click(object sender, EventArgs e)
        {
            if (gridTravelers.CurrentRow != null)
            {
                ArrangementTravelersModel m = (ArrangementTravelersModel)gridTravelers.CurrentRow.DataBoundItem;

                if (m != null)
                {
                    travelersList.Remove(m);
                    List<PersonModel> travelers = new List<PersonModel>();
                    travelers = (List<PersonModel>)dropdownTravelers.DataSource;
                    travelers.Add(new PersonBUS().GetPerson(m.idTravelWithPerson));
                    dropdownTravelers.DataSource = null;

                    DescriptionTextListDataItem descriptionItem = new DescriptionTextListDataItem();
                    descriptionItem.Value = 0;
                    this.dropdownTravelers.Items.Add(descriptionItem);
                    this.dropdownTravelers.SelectedIndex = 0;

                    if (travelers != null)
                    {
                        dropdownTravelers.DataSource = travelers;
                        dropdownTravelers.ValueMember = "idContPers";
                        dropdownTravelers.DisplayMember = "fullname";

                    }

                    this.dropdownTravelers.SelectedItem = descriptionItem;

                }
            }
        }

        private void gridTravelers_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if(gridTravelers!=null)

            if (gridTravelers.Columns.Count > 0)
            {
                for (int i = 0; i < gridTravelers.Columns.Count; i++)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString(gridTravelers.Columns[i].HeaderText) != null)
                            gridTravelers.Columns[i].HeaderText = resxSet.GetString(gridTravelers.Columns[i].HeaderText);
                    }
                }
            }

            if(gridTravelers!=null)
            {
                if(File.Exists(layoutTravelersGrid))
                {
                    gridTravelers.LoadLayout(layoutTravelersGrid);
                }
            }

            gridTravelers.Columns["birthdate"].IsVisible = false;
        }

        #endregion Travelers

        #region TravelPapers

        private void fillPanelTravelPapers()
        {
            ArrangementBookTravelPapersBUS arrBookTravelPapers = new ArrangementBookTravelPapersBUS();
            List<ArrangementBookTravelPapersModel> arrBookTravelPapersModel = new List<ArrangementBookTravelPapersModel>();
            arrBookTravelPapersModel = arrBookTravelPapers.GetAllTravelPapers(Login._user.lngUser);
            if (arrBookTravelPapersModel != null)
            {
                if (arrBookTravelPapersModel.Count > 0)
                {
                    int Y = 0;

                    for (int i = 0; i < arrBookTravelPapersModel.Count; i++)
                    {
                        RadRadioButton rbn = new RadRadioButton();
                        rbn.Font = new Font("Verdana", 9);
                        rbn.ThemeName = panelInformation.ThemeName;
                        rbn.Name = "rbnTravelPapers" + arrBookTravelPapersModel[i].idTravelPapers.ToString();
                        rbn.Text = arrBookTravelPapersModel[i].nameTravelPapers;
                        rbn.CheckStateChanging += rbnTravelPapers_CheckStateChanging;
                        rbn.CheckStateChanged += rbnTravelPapers_CheckStateChanged;
                        rbn.Location = new Point(0, Y);
                        rbn.AutoSize = true;
                        Y = Y + 3 + rbn.Height;
                        panelTravelPapers.Controls.Add(rbn);
                    }
                }
            }
        }

        private void rbnTravelPapers_CheckStateChanging(object sender, CheckStateChangingEventArgs args)
        {
            if (pageLoaded == true)
            {
                RadRadioButton rrb = (RadRadioButton)sender;
                Control[] statusId2 = this.Controls.Find("rbnStatus2", true);
                if (statusId2 != null)
                {
                    if (statusId2.Length > 0)
                    {
                        RadRadioButton rrbStatusFinal = (RadRadioButton)statusId2[0];
                        if (rrbStatusFinal.CheckState == CheckState.Checked && Convert.ToInt32(rrb.Name.Replace("rbnTravelPapers", "")) != 1)
                        {
                            args.Cancel = true;
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("You cannot check final status if status for travel papers isn't AF received!");

                        }

                    }
                }
            }
        }

        private void rbnTravelPapers_CheckStateChanged(object sender, EventArgs e)
        {

            RadRadioButton rrb = (RadRadioButton)sender;
                arrBookModel.idTravelPapers = Convert.ToInt32(rrb.Name.Replace("rbnTravelPapers", ""));
        }

        private void btnTravelPapersLookup_Click(object sender, EventArgs e)
        {
            TravelerPapersReportBUS accBUS = new TravelerPapersReportBUS();
            List<IModel> am = new List<IModel>();

            am = accBUS.GetAllTravelerPapers();

            // ako se promeni "Travel papers" mora i u lookupu, jer je po nazivu lookup-a definisa sirina kolone u gridLookup_DataBindingComplete
            using (var dlgClient = new GridLookupForm(am, "Travel papers"))
            {
                if (dlgClient.ShowDialog(this) == DialogResult.Yes)
                {
                    TravelPapersModel okm = new TravelPapersModel();
                    okm = (TravelPapersModel)dlgClient.selectedRow;
                    txtNameTravelPapers.Text = okm.nameTravelPapers.ToString();

                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void updateStatus()
        {
            List<ArrangementStatusModel> statuslist = new ArrangementStatusBUS().GetAllArrangementStatus();
            ArrangementModel arrange = new ArrangementBUS().GetArrangementById(arrBookModel.idArrangement);
            int nrOfBookedTravelers = new ArrangementBookBUS().GetNrTravelerByGender(2, arrange.idArrangement) + new ArrangementBookBUS().GetNrTravelerByGender(1, arrange.idArrangement);
            if (arrange.nrTraveler == nrOfBookedTravelers && statuslist != null && arrange.statusArrangement == statuslist.SingleOrDefault(s => s.idArrangementStatus == 2).nameArrangementStatus)
            {
                arrange.statusArrangement = statuslist.SingleOrDefault(s => s.idArrangementStatus == 3).nameArrangementStatus;
                arrBookModel.idStatus = 3;
            }
            ArrangementBUS ab = new ArrangementBUS();
            ab.Update(arrange, this.Name, Login._user.idUser);

        }

        private void btnReport_Click(object sender, EventArgs e)
        {

            if (txtNameTravelPapers.Text.ToString() != "")
            {
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                DataTable dt4 = new DataTable();

                // ako je nova forma da li ima idjeve
                if (arrBookModel.idArrangement != 0 && arrBookModel.idContPers != 0)
                {
                    dt1 = new TravelerPapersReportBUS().GetTravelerPaper(arrBookModel.idArrangement, arrBookModel.idContPers);
                    dt2 = new TravelerPapersReportBUS().GetPapers(arrBookModel.idArrangement);
                    dt3 = new TravelerPapersReportBUS().GetTekst(arrBookModel.idArrangement);
                    dt4 = new TravelerPapersReportBUS().GetArrangementRemaining(arrBookModel.idArrangement);

                    string formName;
                    formName = txtNameTravelPapers.Text.ToString();


                    using (var form = (Form)Activator.CreateInstance(Type.GetType("GUI.frm" + formName), dt1, dt2, dt3, dt4))
                    {

                        form.ShowDialog();
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Name travel paper is required!");
            }
        }

        #endregion

        private void LoadExtraOptionalGrid()
        {

            if (arrBookModel.idContPers != 0)
            {
                ArticalBUS adao = new ArticalBUS();
                //DataTable table = new DataTable();

                selArticleExtraOptional = adao.GetExtraOptionalData(arrBookModel.idArrangement);
                if (selArticleExtraOptional == null)
                    selArticleExtraOptional = new List<ArticalExtraOptionalModel>();
                if (selArticleExtraOptional != null)
                {
                    if (selArticleExtraOptional.Count > 0)
                    {

                        rgvExtraOptional.DataSource = selArticleExtraOptional;
                        rgvExtraOptional.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None;

                        rgvExtraOptional.AllowAutoSizeColumns = true;
                        GridViewCheckBoxColumn chk2 = new GridViewCheckBoxColumn();
                        chk2.Name = "isChecked";
                        chk2.HeaderText = "Add/Not";
                        rgvExtraOptional.Columns.Add(chk2);

                        rgvExtraOptional.Columns.Move(rgvExtraOptional.Columns.Count - 1, 0);
                        rgvExtraOptional.Columns["arranementBookID"].IsVisible = false;
                        idArrBook = adao.GetArrangementBookID(arrBookModel.idContPers, arrBookModel.idArrangement);

                        ArticalBUS ab = new ArticalBUS();
                        selArticleExtraOptionalPerson = ab.GetArrangementBookOptionalForPerson(arrBookModel.idArrangementBook);
                        if (selArticleExtraOptionalPerson != null)
                        {

                            for (int n = 0; n < rgvExtraOptional.RowCount; n++)
                            {

                                if (selArticleExtraOptionalPerson.Find(s => s.idArticle == rgvExtraOptional.Rows[n].Cells["idArticle"].Value.ToString() && s.isExtra == Convert.ToBoolean(rgvExtraOptional.Rows[n].Cells["isExtra"].Value.ToString()) && s.isOptional == Convert.ToBoolean(rgvExtraOptional.Rows[n].Cells["isOptional"].Value.ToString())) != null)
                                {


                                    ArticalExtraOptionalModel modelArticalOM = selArticleExtraOptionalPerson.Find(s => s.idArticle == rgvExtraOptional.Rows[n].Cells["idArticle"].Value.ToString() && s.isExtra == Convert.ToBoolean(rgvExtraOptional.Rows[n].Cells["isExtra"].Value.ToString()) && s.isOptional == Convert.ToBoolean(rgvExtraOptional.Rows[n].Cells["isOptional"].Value.ToString()));

                                    rgvExtraOptional.Rows[n].Cells["sellingPrice"].Value = modelArticalOM.sellingPrice.ToString();

                                }
                            }
                        }
                    }

                }

            }



        }
        private void rgvExtraOptional_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            var grid = sender as RadGridView;

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                foreach (var column in rgvExtraOptional.Columns)
                {
                    if (resxSet.GetString(column.HeaderText) != null && resxSet.GetString(column.HeaderText) != "")
                        column.HeaderText = resxSet.GetString(column.HeaderText);
                    if (column.Name != "isChecked")
                        column.ReadOnly = true;

                    column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                }
            }
        }

        private void rgvExtraOptional_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (isLoaded == false)
            {

                if (this.rgvExtraOptional.ActiveEditor is RadCheckBoxEditor)
                {
                    if (rgvExtraOptional.CurrentRow.Cells["idArticle"].Value != null)
                    {
                        ArticalExtraOptionalModel model = (ArticalExtraOptionalModel)rgvExtraOptional.CurrentRow.DataBoundItem;


                        string codeArticles = rgvExtraOptional.CurrentRow.Cells["idArticle"].Value.ToString();
                        bool chechstate = Convert.ToBoolean(rgvExtraOptional.ActiveEditor.Value);

                        //ako se tek cekira
                        if (chechstate == false)
                        {
                            bool exist = false;
                            //bool existDelete = false;

                            if (selArticleExtraOptionalPerson != null)
                            {
                                if (selArticleExtraOptionalPerson.Count > 0)
                                {
                                    if (selArticleExtraOptionalPerson.Find(s => s.idArticle == codeArticles && s.isExtra == model.isExtra & s.isOptional == model.isOptional) != null)
                                    {
                                        exist = true;
                                    }
                                }
                            }
                            else
                            {
                                selArticleExtraOptionalPerson = new List<ArticalExtraOptionalModel>();
                            }

                            if (exist == false)
                            {
                                selArticleExtraOptionalPerson.Add(model);

                            }

                            else if (exist == true)
                            {

                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You already checked " + codeArticles);
                                e.Cancel = true;
                            }
                            rgvExtraOptional.CurrentRow.InvalidateRow();
                        }

                        else
                        {
                            var result = selArticleExtraOptionalPerson.Find(s => s.idArticle == codeArticles && s.isExtra == model.isExtra & s.isOptional == model.isOptional);

                            if (result != null)
                            {
                                selArticleExtraOptionalPerson.Remove(result);
                                rgvExtraOptional.CurrentRow.InvalidateRow();
                            }

                        }
                    }
                }
            }
        }


        private void checkedRowsExtraOptional()
        {
            isLoaded = true;
            ArticalBUS ab = new ArticalBUS();
            selArticleExtraOptionalPerson = ab.GetArrangementBookOptionalForPerson(arrBookModel.idArrangementBook);
            if (selArticleExtraOptionalPerson != null && rgvExtraOptional != null)
            {
                for (int n = 0; n < rgvExtraOptional.RowCount; n++)
                {
                    if (selArticleExtraOptionalPerson.Find(s => s.idArticle == rgvExtraOptional.Rows[n].Cells["idArticle"].Value.ToString() && s.isExtra == Convert.ToBoolean(rgvExtraOptional.Rows[n].Cells["isExtra"].Value.ToString()) && s.isOptional == Convert.ToBoolean(rgvExtraOptional.Rows[n].Cells["isOptional"].Value.ToString())) != null)
                    {

                        ArticalExtraOptionalModel modelArticalOM = selArticleExtraOptionalPerson.Find(s => s.idArticle == rgvExtraOptional.Rows[n].Cells["idArticle"].Value.ToString() && s.isExtra == Convert.ToBoolean(rgvExtraOptional.Rows[n].Cells["isExtra"].Value.ToString()) && s.isOptional == Convert.ToBoolean(rgvExtraOptional.Rows[n].Cells["isOptional"].Value.ToString()));

                        rgvExtraOptional.Rows[n].Cells["isChecked"].Value = true;
                        rgvExtraOptional.Rows[n].Cells["sellingPrice"].Value = modelArticalOM.sellingPrice.ToString();

                    }
                    else
                        rgvExtraOptional.Rows[n].Cells["isChecked"].Value = false;
                }
            }
            isLoaded = false;
        }
        private void recalculationVolArr()
        {
            // ako je cekiran status optional ili final
            if (panelStatus.Controls.Find("rbnStatus" + arrBookModel.idStatus.ToString(), true).Length > 0)
            {
                RadRadioButton rbn = (RadRadioButton)panelStatus.Controls.Find("rbnStatus" + arrBookModel.idStatus.ToString(), true)[0];
                if (rbn.Name.Substring(rbn.Name.Length - 1, 1) == "1" || rbn.Name.Substring(rbn.Name.Length - 1, 1) == "2")
                {

                    ArrangementBookBUS ab = new ArrangementBookBUS();
                    // lista svih skilova za tu osobu
                    List<MedicalVoluntaryQuestModel> listSkill = new List<MedicalVoluntaryQuestModel>();
                    // broj skilova iz tabele medLookup
                    List<MedicalVoluntaryQuestModel> listNrSkill = new List<MedicalVoluntaryQuestModel>();
                    // svi skilovi iz tabele volArr za taj arrangement
                    List<MedicalVoluntaryQuestModel> listSkillVolArr = new List<MedicalVoluntaryQuestModel>();

                    listSkill = ab.GetSkillForPerson(arrBookModel.idContPers);
                    ab.MedLookupSriptSave(arrBookModel.idContPers, arrBookModel.idArrangement, listSkill);
                    listNrSkill = ab.GetNrForSkillsArrangement(arrBookModel.idArrangement);
                    listSkillVolArr = ab.GetAllSkillsVolArr(arrBookModel.idArrangement);


                    if (listSkillVolArr != null)
                    {
                        if (listNrSkill != null)
                        {
                            for (int i = 0; i < listNrSkill.Count; i++)
                            {
                                // List<MedicalVoluntaryQuestModel> telForPerson = new List<MedicalVoluntaryQuestModel>();

                                var skillExistInVolarr = listSkillVolArr.Find(s => s.idQuest == listNrSkill[i].idQuest);

                                //insert
                                if (skillExistInVolarr == null)
                                {
                                    if (ab.SaveVolArr(listNrSkill[i], arrBookModel.idArrangement) == false)
                                    {
                                        translateRadMessageBox tr = new translateRadMessageBox();
                                        tr.translateAllMessageBox("You have not succesufully inserted skill. Please check!");
                                    }


                                }
                                //update
                                else
                                {
                                    string txt = listNrSkill[i].nameQuestGroup;
                                    int idQuest = listNrSkill[i].idQuest;

                                    if (ab.UpdateVolArr(listNrSkill[i], txt, idQuest, arrBookModel.idArrangement) == false)
                                    {
                                        translateRadMessageBox tr = new translateRadMessageBox();
                                        tr.translateAllMessageBox("You have not succesufully update skill. Please check!");
                                    }
                                }
                            }
                        }
                    }


                    else
                    {
                        if (listNrSkill != null)
                        {
                            for (int i = 0; i < listNrSkill.Count; i++)
                            {


                                if (ab.SaveVolArr(listNrSkill[i], arrBookModel.idArrangement) == false)
                                {

                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translateAllMessageBox("You have not succesufully inserted skill. Please check!");

                                }

                            }
                        }


                    }

                }

            }



        }

        private void gridTravelers_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            string saveLayout = "Save Layout";
            using(ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }

            RadMenuItem customMenuItem = new RadMenuItem();
            customMenuItem.Text = saveLayout;
            customMenuItem.Click += SaveLayoutTravelersGrid;

            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);

            //==delete 

            string saveLayout1 = "Delete Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }

            RadMenuItem customMenuItem1 = new RadMenuItem();
            customMenuItem1.Text = saveLayout1;
            customMenuItem1.Click += SaveLayoutTravelersD;

            RadMenuSeparatorItem separator1 = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator1);
            e.ContextMenu.Items.Add(customMenuItem1);


        }

        private void SaveLayoutTravelersD(object sender , EventArgs e)
        {
            if(File.Exists(layoutTravelersGrid))
            {
                File.Delete(layoutTravelersGrid);
            }

            translateRadMessageBox trs = new translateRadMessageBox();
            trs.translateAllMessageBox("You have successfully delete layout!");
        }


        private void SaveLayoutTravelersGrid(object sender, EventArgs e)
        {
            if(File.Exists(layoutTravelersGrid))
            {
                File.Delete(layoutTravelersGrid);
            }

            gridTravelers.SaveLayout(layoutTravelersGrid);

            translateRadMessageBox trs = new translateRadMessageBox();
            trs.translateAllMessageBox("You have successfully save layout!");
        }
    }
}
