using BIS.Business;
using BIS.Model;
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

namespace GUI
{
    public partial class frmArrangementBookingPerson_VH : frmTemplate
    {

        List<PersonModel> arrangementBookPerson;    
        ArrangementBookModel arrBookModel;
        ArrangementBookBUS arrBookBUS;
        int iID = -1;
        int arrStatus = -1;

        InvoiceModel _selectedRowInvoice;
        InvoiceModel _clickedInvoice;

        Boolean isLoaded = false;

        public List<ArrangementRoomsArticle> selArticlesAccomodation;

        //all articles variables
        //
        List<ArrangementRoomsArticle> modelArticlesAccomodation;
        ArrangementBookBUS abm;
        int idArr = 0;
        //
        //end all articles variables

        // Layout file names for all grids
        private string layoutArrangementBookPersonVH = MainForm.gridFiltersFolder + "\\layoutArrangementBookPersonVH.xml";
        private string layoutArrangementBookPersonArticlesVH = MainForm.gridFiltersFolder + "\\layoutArrangementBookPersonArticlesVHAccomodation.xml";
        private string layoutInvoiceBookPersonArticles = MainForm.gridFiltersFolder + "\\layoutInvoiceBookPersonArticles.xml";

        private bool pageLoaded = false;
        Boolean isBookedFull = false;

        public frmArrangementBookingPerson_VH(ArrangementBookModel arrBook, bool addnew)
        {
            this.Icon = Login.iconForm;
            string formName = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();

            formName = formName + " " + new ArrangementBUS().GetArrangementById(arrBook.idArrangement).nameArrangement;


            this.Text = formName;

            InitializeComponent();
            arrBookModel = arrBook;
            btnSave.Click += btnSave_Click;
            arrBookBUS = new ArrangementBookBUS();
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
        }

        public frmArrangementBookingPerson_VH(ArrangementBookModel arrBook, bool addnew, bool isReserved)
        {
            this.Icon = Login.iconForm;
            string formName = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();

            formName = formName + " " + new ArrangementBUS().GetArrangementById(arrBook.idArrangement).nameArrangement;


            this.Text = formName;

            InitializeComponent();
            arrBookModel = arrBook;
            btnSave.Click += btnSave_Click;
            arrBookBUS = new ArrangementBookBUS();
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

        private void btnperson_Click(object sender, EventArgs e)
        {

            if (arrBookModel == null)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to add status because of the reseved one to not book skills or functions!");
            }
            else
            {
                if (arrBookModel.idStatus <= 0)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You have to add status because of the reseved one to not book skills or functions!");
                }
                else
                {
                    using (var dlgSave = new GridLookupVoluntary(arrBookModel.idArrangement, arrBookModel.idContPers, "BookVoluntary"))
                    {
                        if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                        {
                            PersonModel genm = new PersonModel();
                            genm = (PersonModel)dlgSave.selectedRow;

                            PersonBookModel pbm = new PersonBUS().GetPersonBookedForArrangement(Login._user.lngUser, genm.idContPers);

                            txtperson.Text = pbm.fullname;
                            txtFirstName.Text = pbm.firstname;
                            txtMidName.Text = pbm.midname;
                            txtinitialsContPers.Text = pbm.initialsContPers;
                            txtLastName.Text = pbm.lastname;
                            dtbirthdate.Value = pbm.birthdate;
                            arrBookModel.idContPers = genm.idContPers;


                            ArrangementBookBUS bus = new ArrangementBookBUS();
                            bool deleted = bus.DeleteVolLookup(arrBookModel.idArrangement, genm.idContPers, this.Name, Login._user.idUser);

                            if (dlgSave.selectedIDQuests != null)
                            {
                                foreach (int num in dlgSave.selectedIDQuests)
                                {
                                    if (deleted == true)
                                        if (arrBookModel.idStatus != 3)
                                            bus.SaveVolLookup(arrBookModel.idArrangement, genm.idContPers, num, "F", this.Name, Login._user.idUser);
                                        else
                                            bus.SaveVolLookup(arrBookModel.idArrangement, genm.idContPers, num, "RF", this.Name, Login._user.idUser);
                                }
                            }
                            if (dlgSave.selectedIDQuests_Trips != null)
                            {
                                foreach (int num in dlgSave.selectedIDQuests_Trips)
                                {
                                    if (deleted == true)
                                        if (arrBookModel.idStatus != 3)
                                            bus.SaveVolLookup(arrBookModel.idArrangement, genm.idContPers, num, "T", this.Name, Login._user.idUser);
                                        else
                                            bus.SaveVolLookup(arrBookModel.idArrangement, genm.idContPers, num, "RT", this.Name, Login._user.idUser);
                                }
                            }

                            if (dlgSave.selectedIDQuests_Skills != null)
                            {
                                foreach (int num in dlgSave.selectedIDQuests_Skills)
                                {
                                    if (deleted == true)
                                        if (arrBookModel.idStatus != 3)
                                            bus.SaveVolLookup(arrBookModel.idArrangement, genm.idContPers, num, "S", this.Name, Login._user.idUser);
                                        else
                                            bus.SaveVolLookup(arrBookModel.idArrangement, genm.idContPers, num, "RS", this.Name, Login._user.idUser);
                                }
                            }
                        }
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }
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
            btnAddTraveler.Visibility = ElementVisibility.Collapsed;
            btnDeleteTraveler.Visibility = ElementVisibility.Collapsed;

            txtinitialsContPers.ReadOnly = true;
            txtFirstName.ReadOnly = true;
            txtLastName.ReadOnly = true;
            txtMidName.ReadOnly = true;

            rpvVoucher.SelectedPage = tabAccomodation;
            fillPanelStatus();
            panelTravelPapers.Visible = false;
            setTranslation();

            selArticlesAccomodation = new List<ArrangementRoomsArticle>();        

            if(arrBookModel.idArrangementBook>0)
            {
                arrBookBUS = new ArrangementBookBUS();
                iID = arrBookModel.idArrangementBook;
                arrBookModel = arrBookBUS.GetArrangementBook(arrBookModel.idArrangementBook);
                fillData();
                arrangementBookPerson = new PersonBUS().GetArrangementVHPersons(arrBookModel.idArrangementBook, Login._user.lngUser);
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
                if(arrBookModel!=null)
                    if(arrBookModel.idContPers>0)
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

            LoadBookArticleGrids();

            checkedRows();

            LoadBoardingPoint();

            pageLoaded = true;
        }

        private void LoadBookArticleGrids()
        {

            //if (arrBookModel.idContPers != 0)
            //{
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

           // }
        }

        public List<GridViewRowInfo> GetAllRows(GridViewTemplate template)
        {
            List<GridViewRowInfo> allRows = new List<GridViewRowInfo>();
            allRows.AddRange(template.Rows);
            foreach (GridViewTemplate childTemplate in template.Templates)
            {
                List<GridViewRowInfo> childRows = this.GetAllRows(childTemplate);
                allRows.AddRange(childRows);
            }

            return allRows;
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

        private void fillData()
        {
            PersonBookModel pbm = new PersonBUS().GetPersonBookedForArrangement( Login._user.lngUser, arrBookModel.idContPers);

            txtperson.Text = pbm.fullname;
            txtFirstName.Text = pbm.firstname;
            txtMidName.Text = pbm.midname;
            txtinitialsContPers.Text = pbm.initialsContPers;
            txtLastName.Text = pbm.lastname;
            dtbirthdate.Value = pbm.birthdate;

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
                btnSave.Enabled = false;
                btnperson.Enabled = false;
                btnAddPersons.Enabled = false;
                panelTravelPapers.Enabled = false;
                int invoiceNr = 0;
                invoiceNr = new ArrangementBookBUS().checkIfArrangementBookIsInInvoice(arrBookModel.idArrangementBook);
                if (invoiceNr != 0)
                {
                    btnInvoice.Enabled = false;
                    int idAccLine = 0;
                    idAccLine = new ArrangementBookBUS().checkIfInvoiceIsInAccLine(invoiceNr);
                    if(idAccLine!=0)
                        btnAccountBooking.Enabled = false;
                    else
                        btnAccountBooking.Enabled = true;
                }
                else
                {
                    btnInvoice.Enabled = true;
                    btnAccountBooking.Enabled = true;
                }
            }

            //ako je status canceled
            if (arrBookModel.idStatus == 4)
            {
                btnOpenPerson.Enabled = false;
                btnperson.Enabled = false;
                btnAddPersons.Enabled = false;
                panelStatus.Enabled = false;
                panelTravelPapers.Enabled = false;                
                //rpvVoucher.Enabled = false;

                btnInvoice.Enabled = false;                
                btnAccountBooking.Enabled = false;
                gridInvoice.Enabled = false;

                rgvPersons.Enabled = false;
                rgvExtraArticles.Enabled = false;
                dropdownBoardingPoint.Enabled = false;


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
                        rbn.CheckStateChanged += rbnTravelPapers_CheckStateChanged;
                        rbn.Location = new Point(0, Y);
                        rbn.AutoSize = true;
                        Y = Y + 3 + rbn.Height;
                        panelTravelPapers.Controls.Add(rbn);
                    }
                }
            }
        }

        private void rbnTravelPapers_CheckStateChanged(object sender, EventArgs e)
        {
            RadRadioButton rrb = (RadRadioButton)sender;
            arrBookModel.idTravelPapers = Convert.ToInt32(rrb.Name.Replace("rbnTravelPapers", ""));
        }
        // Neta 15.12
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
            }
        }

        private void setTranslation()
        {
            

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(radMenuItemArrangementBookPersonVH.Text) != null)
                    radMenuItemArrangementBookPersonVH.Text = resxSet.GetString(radMenuItemArrangementBookPersonVH.Text);
                if (resxSet.GetString(radMenuItemArrangementBookPersonArticlesVH.Text) != null)
                    radMenuItemArrangementBookPersonArticlesVH.Text = resxSet.GetString(radMenuItemArrangementBookPersonArticlesVH.Text);
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
                if (resxSet.GetString(lblTravelPapers.Text) != null)
                    lblTravelPapers.Text = resxSet.GetString(lblTravelPapers.Text);

                if (resxSet.GetString(radMenuItemInvoice.Text) != null)
                    radMenuItemInvoice.Text = resxSet.GetString(radMenuItemInvoice.Text);

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (anyRoomSelected())
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to select a room!");
                return;
            }

            if (boardingPointNotSelected())
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to select boarding point");
                return;
            }
            arrBookBUS = new ArrangementBookBUS();
            int checkfields = checkFields();
            if (checkfields == 1)
            {
                if(iID==-1)
                {
                    if (dtBooked.Text != "")
                        arrBookModel.dtBooked = Convert.ToDateTime(dtBooked.Text);
                    else
                        arrBookModel.dtBooked = DateTime.Now;

                    arrBookModel.idUserCreated = Login._user.idUser;
                    arrBookModel.dtUserCreated = DateTime.Now;
                    arrBookModel.idUserModified = Login._user.idUser;
                    arrBookModel.dtUserModified = DateTime.Now;

                    int result = arrBookBUS.Save(arrBookModel, this.Name, Login._user.idUser);
                    if (result>0)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have  succesufully inserted booking data.");
                        if (arrBookModel.idStatus == 2 && arrStatus != 2)
                        {
                            arrStatus = 2;
                            btnSave.Enabled = false;
                            btnperson.Enabled = false;
                            btnAddPersons.Enabled = false;
                            panelStatus.Enabled = false;
                            panelTravelPapers.Enabled = false;
                            btnInvoice.Enabled = true;
                            btnAccountBooking.Enabled = true;
                        }
                        iID = result;
                        savePersons();
                        saveExtraArticles();
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have not succesufully inserted booking data. Please check!");
                    }
                }
                else
                {
                    if (dtBooked.Text != "")
                        arrBookModel.dtBooked = Convert.ToDateTime(dtBooked.Text);
                    else
                        arrBookModel.dtBooked = DateTime.Now;

                    arrBookModel.idUserModified = Login._user.idUser;
                    arrBookModel.dtUserModified = DateTime.Now;

                    if (arrBookBUS.Update(arrBookModel, this.Name, Login._user.idUser) == true)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have  succesufully updated booking data.");
                        if (arrBookModel.idStatus == 2 && arrStatus != 2)
                        {
                            arrStatus = 2;
                            btnSave.Enabled = false;
                            btnperson.Enabled = false;
                            btnAddPersons.Enabled = false;
                            panelStatus.Enabled = false;
                            btnInvoice.Enabled = true;
                            btnAccountBooking.Enabled = true;
                        }
                        savePersons();
                        saveExtraArticles();
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have not succesufully updated booking data. Please check!");
                    }
                }
            }
            else if (checkfields == 2)
            {
                if (iID != -1)
                {
                    saveExtraArticles();
                    updateBoardingPoint();
                   // this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                }
            }
            else
            {

            }
        }
        private bool updateBoardingPoint()
        {
            ArrangementBookBUS abb = new ArrangementBookBUS();
            return abb.UpdateBoardingPoint(arrBookModel.idArrangementBook, arrBookModel.idBoarding, this.Name, Login._user.idUser);

        }
        private void savePersons()
        {
            arrBookBUS = new ArrangementBookBUS();
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
                                break;
                            }
                        }
                        if (arrBookBUS.SavePersons(iID, arrangementBookPerson[i].idContPers, Login._user.idUser, DateTime.Now, Login._user.idUser, DateTime.Now, this.Name, Login._user.idUser) == false)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translatePartAndNonTranslatedPart("You have not succesufully inserted person", arrangementBookPerson[i].fullname);
                            break;
                        }
                    }
                }
                if (iID > 0 && arrangementBookPerson.Count == 0)
                {
                    if (arrBookBUS.Delete(iID, this.Name, Login._user.idUser) == false)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                    }
                }
            }
        }

        private void saveExtraArticles()
        {
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
                                break;
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
                                    if (selArticles[i].idUserCreated != 0)
                                    {
                                        uCreated = selArticles[i].idUserCreated;
                                        dCreated = selArticles[i].dtUserCreated;
                                    }

                            if (arrBookBUS.SaveArticles(iID, selArticlesAccomodation[i].idArticle, selArticlesAccomodation[i].isContract, selArticlesAccomodation[i].id, selArticlesAccomodation[i].idRoom, uCreated, dCreated, Login._user.idUser, DateTime.Now, this.Name, Login._user.idUser) == false)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translatePartAndNonTranslatedPart("You have not succesufully inserted article for", new PersonBUS().GetPerson(arrBookModel.idContPers).fullname);
                                break;
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
                    }
                }
            }
        }

        private int checkFields()
        {
            // ako je final vraca status 2

            if (arrBookModel.idStatus == 2 && arrStatus==2)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                //tr.translateAllMessageBox("You cannot change booking when status is final!");
                
                DialogResult dr = tr.translateAllMessageBoxDialog("Booking status is final. Only room changes will be saved.", "Warning");

                if(dr == DialogResult.Yes)
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
                
            }
            else if(txtperson.Text=="")
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to add person!");
                return 0;
            }
            else if(arrBookModel.idStatus==0)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to add status!");
                return 0;
            }
            else return 1;
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
                using (GridLookupFormPersons frm = new GridLookupFormPersons(new PersonBUS().GetArrangementVHPersonsLookup(arrBookModel.idArrangement, arrBookModel.idContPers, Login._user.lngUser), arrangementBookPerson, this.Name, arrBookModel.idArrangement, arrBookModel.idArrangementBook))
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        rgvPersons.DataSource = null;
                        arrangementBookPerson = frm.selMenus1;
                        rgvPersons.DataSource = arrangementBookPerson;
                    }
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        private void rgvPersons_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if (File.Exists(layoutArrangementBookPersonVH))
            {
                rgvPersons.LoadLayout(layoutArrangementBookPersonVH);
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

        private void rgvExtraArticles_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
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

        private void radMenuItemArrangementBookPersonArticlesVH_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutArrangementBookPersonArticlesVH))
            {
                File.Delete(layoutArrangementBookPersonArticlesVH);
            }
            rgvExtraArticles.SaveLayout(layoutArrangementBookPersonArticlesVH);
            translateRadMessageBox tr = new translateRadMessageBox();
            tr.translateAllMessageBox("Layout saved");
        }

        private void radMenuItemArrangementBookPersonVH_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutArrangementBookPersonVH))
            {
                File.Delete(layoutArrangementBookPersonVH);
            }
            rgvPersons.SaveLayout(layoutArrangementBookPersonVH);
            translateRadMessageBox tr = new translateRadMessageBox();
            tr.translateAllMessageBox("Layout saved");
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
                                frm.ShowDialog();
                            }
                            GC.Collect();
                            GC.WaitForPendingFinalizers();
                            GC.Collect();
                           
                        }
                }
            }
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutInvoiceBookPersonArticles))
            {
                File.Delete(layoutInvoiceBookPersonArticles);
            }
            gridInvoice.SaveLayout(layoutInvoiceBookPersonArticles);
            translateRadMessageBox tr = new translateRadMessageBox();
            tr.translateAllMessageBox("Layout saved");
        }

        private List<string> codeArticlesList = new List<string>();

        private List<ArrangementRoomsArticle> extraArticlesListMod = new List<ArrangementRoomsArticle>();

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
                                        model.name = pbm.firstname + " " + pbm.lastname;
                                        model.nameGender = pbm.nameGender;
                                        model.idContPers = arrBookModel.idContPers;
                                        model.type = "Voluntary";
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

        private void btnOpenPerson_Click(object sender, EventArgs e)
        {
            if(arrBookModel.idContPers != 0)
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


            if(lista != null)
            {

                foreach(BoardingPointModel m in lista)
                {
                    descriptionItem = new DescriptionTextListDataItem();
                    descriptionItem.Height = height;
                    descriptionItem.Value = m.idBoardingPoint;
                    descriptionItem.Text = m.nameBoardingPoint;
                    descriptionItem.DescriptionText = m.dtDeparture.ToString("dd.MM.y H:mm 'h'") + " - " + m.dtArrival.ToString("dd.MM.y H:mm 'h'") + "\n" + m.addressBoardingPoint;
                    this.dropdownBoardingPoint.Items.Add(descriptionItem);
                }

            }

            ArrangementBookBUS abus = new ArrangementBookBUS();
            int boardingpoint = abus.GetArrangementBookBoardingPoint(arrBookModel.idArrangementBook, arrBookModel.idArrangement, arrBookModel.idContPers);

            dropdownBoardingPoint.SelectedValue = boardingpoint;

            DescriptionTextListDataItem item = dropdownBoardingPoint.SelectedItem as DescriptionTextListDataItem;
            lblBoardingPoint.Text = item.DescriptionText;
        }

        private void dropdownBoardingPoint_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if(pageLoaded == true)
            {
                arrBookModel.idBoarding = (int)dropdownBoardingPoint.SelectedValue;

                DescriptionTextListDataItem item = dropdownBoardingPoint.SelectedItem as DescriptionTextListDataItem;
                lblBoardingPoint.Text = item.DescriptionText;
                
            }
        }

        private void frmArrangementBookingPerson_VH_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
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
                    dt2= new TravelerPapersReportBUS().GetPapers(arrBookModel.idArrangement);
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

        #region TravelPapers
        private void btnTravelPapersLookup_Click(object sender, EventArgs e)
        {
            TravelerPapersReportBUS accBUS = new TravelerPapersReportBUS();
            List<IModel> am = new List<IModel>();

            am = accBUS.GetAllTravelerPapers();


            var dlgClient = new GridLookupForm(am, "Travel papers");

            if (dlgClient.ShowDialog(this) == DialogResult.Yes)
            {
                TravelPapersModel okm = new TravelPapersModel();
                okm = (TravelPapersModel)dlgClient.selectedRow;
                txtNameTravelPapers.Text = okm.nameTravelPapers.ToString();

            }
        }
        #endregion
    }
}
