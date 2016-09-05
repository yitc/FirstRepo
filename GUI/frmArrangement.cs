
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
using Telerik.WinForms.Documents.Model;
using Telerik.WinForms.Documents.FormatProviders.Html;
using Telerik.WinForms.Documents.FormatProviders;
using System.Globalization;
using NUnit.Framework;
using System.Linq;



namespace GUI
{
    [TestFixture]
    public partial class frmArrangement : frmTemplate
    {
        private string layoutDocuments;
        private string layoutMeetings;
        private string layoutContacts;
        private string layoutTasks;

        private List<ArrangementStatusModel> statusList;
        
        public ArrangementModel arrange;
        public ArrangementModel arrangeFirst;
        List<DocumentsModel> arrDocuments;
        List<ContactsModel> arrContacts;
        List<ToDoModel> arrToDo;

        public int iID = -1;
        public string Namef;
        public bool modelChanged = false;
        public bool isOk = false;
        List<LabelForArrangement> ArrangementLabel;
        public decimal T2 = 0;  // polje za izracunavanje osiguranja
        int minNumber = 0;
        int minNumber1 = 0;
        decimal test = 0;
        public int insLabel;
        public decimal extra = Convert.ToDecimal(0.65);  // extra dodatek
        private decimal provision;
        private decimal premieNo1;
        private decimal premieNo2;
        private bool isfirst = true;
        private decimal totalGridExtra = 0;
        private decimal totalTotalGrid = 0;


        public List<ArrangementInvoicePriceModel> totalLista;
        public List<ArrangementInvoicePriceModel> invoicePrice;
        public decimal questSort;

        ArrangementCalculationModel arrCalcModel;
        private ArrangementCalculationModel firstCalculationModel = null;
        private ArrangementCalculationSecondModel recalcCalculationModel = null;

        public ArrangementCalculationFirstNotArticlesModel firstNotArticles;
        private Decimal commission = -1;
        private Decimal commissionFirst = 0;

        private Boolean isRecalculationLoaded = false;
        List<LabelForArrangement> arrangementLabelFirst = new List<LabelForArrangement>();

        // Layout file names for all grids
        private string layoutPurchase;
        private string layoutArrangementTransport = MainForm.gridFiltersFolder + "\\layoutArrangementTransport.xml";
        private string layoutArrangementPrice = MainForm.gridFiltersFolder + "\\layoutArrangementPrice.xml";
        private string layoutArrangementAccomodation = MainForm.gridFiltersFolder + "\\layoutArrangementAccomodation.xml";
        private string layoutArrangementExtras = MainForm.gridFiltersFolder + "\\layoutArrangementExtras.xml";
        private string layoutArrangementRooms = MainForm.gridFiltersFolder + "\\layoutArrangementRooms.xml";


        List<ArrangementThemeTripModel> arrangeThemeTrip = new List<ArrangementThemeTripModel>();
        List<ArrangementBoardingPointModel> arrangeBoardingPoint = new List<ArrangementBoardingPointModel>();
        List<ArrangementTargetGroupModel> arrangeTargetGroup = new List<ArrangementTargetGroupModel>();


        private Boolean isFinished = false;


        public List<ArrangementInvoicePriceModel> lista;

        public frmArrangement()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Arrangement");
            }
            this.Text = "";
            ribbonExampleMenu.Text = "";
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;

            iID = -1;

            InitializeComponent();

        }

        public frmArrangement(ArrangementModel model)
        {

            arrange = model;
            arrangeFirst = new ArrangementModel((ArrangementModel)model);

            iID = arrange.idArrangement;

            InitializeComponent();

            if (iID != -1)
            {
                for (int i = 0; i < radPageArrange.Pages.Count; i++)
                {
                    radPageArrange.Pages[i].Enabled = true;
                }

            }
        }

        private void  ShowHideDocumentButtons(bool show)
        {
            if(show == true)
            {
                radRibbonDocuments.Visibility = ElementVisibility.Visible;                
                btnDeleteDoc.Visibility = ElementVisibility.Visible;
                btnDeleteDoc.ForeColor = Color.Black;
                btnNewDoc.Visibility = ElementVisibility.Visible;
                btnNewDoc.ForeColor = Color.Black;                                                
            }
            else
            {
                radRibbonDocuments.Visibility = ElementVisibility.Collapsed;
                btnDeleteDoc.Visibility = ElementVisibility.Collapsed;
                btnNewDoc.Visibility = ElementVisibility.Collapsed;                
            }
        }

        private void ShowHideCommunicationButtons(bool show)
        {
            if (show == true)
            {
                radRibbonMeeting.Visibility = ElementVisibility.Collapsed;
                btnNewMeeting.ForeColor = Color.Black;
                radRibbonContact.Visibility = ElementVisibility.Visible;
                btnNewContact.ForeColor = Color.Black;
                btnDelContact.ForeColor = Color.Black;
                radRibbonTask.Visibility = ElementVisibility.Visible;
                btnNewTask.ForeColor = Color.Black;
                btnDelTask.ForeColor = Color.Black;

            }
            else
            {
                radRibbonMeeting.Visibility = ElementVisibility.Collapsed;
                radRibbonContact.Visibility = ElementVisibility.Collapsed;
                radRibbonTask.Visibility = ElementVisibility.Collapsed;
            }
        }
        
       // [Test]
        private void frmArrangement_Load(object sender, EventArgs e)
        {
            firstNotArticles = new ArrangementCalculationFirstNotArticlesModel();
            layoutDocuments = MainForm.gridFiltersFolder + "\\layoutPersonDocuments.xml";
            layoutMeetings = MainForm.gridFiltersFolder + "\\layoutPersonMeetings.xml";
            layoutContacts = MainForm.gridFiltersFolder + "\\layoutPersonContacts.xml";
            layoutTasks = MainForm.gridFiltersFolder + "\\layoutPersonTasks.xml";
            layoutPurchase = MainForm.gridFiltersFolder + "\\layoutArrangementPurchase.xml";
            radRibbonMemo.Visibility = ElementVisibility.Collapsed;
            btnReport.Visibility = ElementVisibility.Visible;
            btnEmail.Visibility = ElementVisibility.Collapsed;
            btnWord.Visibility = ElementVisibility.Collapsed;
            btnNewMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteMemo.Visibility = ElementVisibility.Collapsed;                                    
            radRibbonBarGroupPurchase.Visibility = ElementVisibility.Visible;
            btnPurchase.Visibility = ElementVisibility.Visible;
            btnDelPurchase.Visibility = ElementVisibility.Visible;

            ShowHideDocumentButtons(false);
            ShowHideCommunicationButtons(false);
            btnNewDoc.Click += btnNewDoc_Click;
            btnDeleteDoc.Click += btnDeleteDoc_Click;
            btnNewContact.Click += btnNewContactClick;
            btnDelContact.Click += btnDeleteContactClick;
            btnNewTask.Click += btnNewTask_Click;
            btnDelTask.Click += btnDelTask_Click;
            btnNewMeeting.Click += btnNewMeeting_Click;

            txtdaydFirstPayment.Value = Convert.ToInt32("14");
            txtDaysLastPayment.Value = Convert.ToInt32("42");
            txtFrstpaymentPercent.Value = Convert.ToDecimal("15,00");
            statusList = new ArrangementStatusBUS().GetAllArrangementStatus();
            ddlStatus.DataSource = statusList;
            ddlStatus.DisplayMember = "nameArrangementStatus";
            ddlStatus.ValueMember = "nameArrangementStatus";

            dpDateFrom.Value = DateTime.Now;
            dpDateTo.Value = DateTime.Now;

            //disable mouse wheel
            maskedNrTravelers.MaskedEditBoxElement.EnableMouseWheel = false;
            maskedMinNrTravelers.MaskedEditBoxElement.EnableMouseWheel = false;
            maskedNrVoluntary.MaskedEditBoxElement.EnableMouseWheel = false;
            numMinNumberTravelers.MaskedEditBoxElement.EnableMouseWheel = false;
            numNrVoluntary.MaskedEditBoxElement.EnableMouseWheel = false;
            maskedVolDays.MaskedEditBoxElement.EnableMouseWheel = false;
            numSurcharge.MaskedEditBoxElement.EnableMouseWheel = false;
            numDiscount.MaskedEditBoxElement.EnableMouseWheel = false;
            numGroupMoney.MaskedEditBoxElement.EnableMouseWheel = false;
            maskedDiverseCorrection.MaskedEditBoxElement.EnableMouseWheel = false;
            numtxtAmount.MaskedEditBoxElement.EnableMouseWheel = false;
            maskedNrMaximumWheelchairs.MaskedEditBoxElement.EnableMouseWheel = false;
            maskedWhooseWheelchairs.MaskedEditBoxElement.EnableMouseWheel = false;
            maskedArms.MaskedEditBoxElement.EnableMouseWheel = false;
            maskedAnchorage.MaskedEditBoxElement.EnableMouseWheel = false;
            txtdaydFirstPayment.MaskedEditBoxElement.EnableMouseWheel = false;
            txtDaysLastPayment.MaskedEditBoxElement.EnableMouseWheel = false;
            txtFrstpaymentPercent.MaskedEditBoxElement.EnableMouseWheel = false;
            maskedReservationCosts.MaskedEditBoxElement.EnableMouseWheel = false;




            ddlHotelService.DataSource = new ArrangementBUS().GetArrangementHotelService();
            ddlHotelService.DisplayMember = "nameHotelService";
            ddlHotelService.ValueMember = "idHotelService";
           
            //Mitar i ALeksa Default values
            if (iID == -1)
            {
                if (ddlHotelService != null)
                    if (ddlHotelService.Items.Count >= 3)
                        ddlHotelService.SelectedIndex = 2;
            }
            //Mitar i ALeksa Default values

            AgeCategoryBUS agebus = new AgeCategoryBUS();
            List<AgeCategoryModel> list = new List<AgeCategoryModel>();
            list = agebus.GetAllAgeCategoryes();
            dropdownAgeCategory.DataSource = list;
            dropdownAgeCategory.ValueMember = "idAgeCategory";
            dropdownAgeCategory.DisplayMember = "descAgeCategory";

            //Mitar i ALeksa Default values
            if (iID == -1)
            {
                if (dropdownAgeCategory != null)
                    if (dropdownAgeCategory.Items.Count >= 4)
                        dropdownAgeCategory.SelectedIndex = 3;
                if (ddlStatus != null) {
                    if (ddlStatus.Items.Count > 0) {
                        ddlStatus.SelectedIndex = 0;
                        ddlStatus.ReadOnly = true;
                    }
                }
            }
            //Mitar i ALeksa Default values

            maskedReservationCosts.Value = Convert.ToDecimal("20,00");


            //insert labels in panel
            RadCheckBox rchk;
            int Y = 0;
            for (int i = 0; i < Login._arrLabels.Count; i++)
            {
                rchk = new RadCheckBox();
                rchk.Font = new Font("Verdana", 9);
                rchk.ThemeName = radPageArrange.ThemeName;
                rchk.Name = "chkLabel" + Login._arrLabels[i].idLabel.ToString();
                rchk.Text = Login._arrLabels[i].nameLabel;
                rchk.Location = new Point(0, Y);
                rchk.AutoSize = true;
                Y = Y + 3 + rchk.Height;
                panelLabels.Controls.Add(rchk);
            }

            if (iID != -1) // edit
            {



                txtArrId.Text = arrange.idArrangement.ToString();
                txtArrCode.Text = arrange.codeArrangement.ToString();
                txtArrName.Text = arrange.nameArrangement.ToString();


                if (arrange.statusArrangement != null)
                    ddlStatus.SelectedValue = arrange.statusArrangement;
                if (arrange.cityArrangement != null)
                    txtCity.Text = arrange.cityArrangement.ToString();
                if (arrange.dtFromArrangement.ToString() != "")
                    dpDateFrom.Text = arrange.dtFromArrangement.ToString();
                if (arrange.dtToArrangement.ToString() != "")
                    dpDateTo.Text = arrange.dtToArrangement.ToString();
                if (arrange.nrOfNights.ToString() != "")
                    numNrOfNights.Value = Convert.ToInt32(arrange.nrOfNights.ToString());

                if (arrange.countryNameArrangement != null)
                {
                    txtCountry.Text = arrange.countryNameArrangement.ToString();
                    CountryBUS cbb = new CountryBUS();
                    CountryModel cmb = new CountryModel();
                    cmb = cbb.GetCountryByID(Convert.ToInt32(arrange.countryArrangement));
                    if (cmb != null)
                    {
                        txtInsProvision.Text = cmb.provision.ToString();
                        txtInsPremie.Text = cmb.premie.ToString();
                    }
                }
                if (arrange.typeNameArrangement != null)
                    txtType.Text = arrange.typeNameArrangement.ToString();


                //Load za lookup
                if (arrange.idClientInvoice != null)
                    if (arrange.idClientInvoice != 0)
                    {
                        ClientModel cmodel = new ClientModel();
                        cmodel = new ClientBUS().GetClient(arrange.idClientInvoice);
                        if (cmodel != null)
                            txtClientInvoice.Text = cmodel.nameClient;
                        arrange.nameClient = cmodel.nameClient;
                        arrange.idClientInvoice = cmodel.idClient;
                    }
                //


                if (arrange.nrTraveler != null)
                {
                    maskedNrTravelers.Value = arrange.nrTraveler;
                    lblNum_numberoftravelers.Text = arrange.nrTraveler.ToString();
                }
                if (arrange.minNrTraveler != null)
                {
                    maskedMinNrTravelers.Value = arrange.minNrTraveler;
                    lblNum_minimumnumberoftravelers.Text = arrange.minNrTraveler.ToString();
                }
                if (arrange.nrVoluntaryHelper != null)
                {
                    maskedNrVoluntary.Value = arrange.nrVoluntaryHelper;
                    lblNum_numberofvoluntaryhelpers.Text = arrange.nrVoluntaryHelper.ToString();
                    int a = (arrange.nrVoluntaryHelper - 1);
                    //if (a > 1)
                    numNumberLeader.Text = a.ToString();
                    // else
                    //    numNumberLeader.Text = "1";
                    ddlHotelService.SelectedValue = arrange.idHotelService;
                }

                if (arrange.isWeb == true)
                    chkWeb.CheckState = CheckState.Checked;
                if (arrange.nrMaleVoluntary != null)
                {
                    maskedNrMaleVoluntary.Value = arrange.nrMaleVoluntary;
                }

                dropdownAgeCategory.SelectedValue = arrange.idAgeCategory;

                maskedNrMaximumWheelchairs.Value = arrange.nrMaximumWheelchairs;
                maskedWhooseWheelchairs.Value = arrange.whoseElectricWheelchairs;
                maskedArms.Value = arrange.buSupportingArms;
                maskedAnchorage.Value = arrange.nrAnchorage;

                DateTime dt1 = new DateTime(dpDateTo.Value.Year, dpDateTo.Value.Month, dpDateTo.Value.Day);
                DateTime dt2 = new DateTime(dpDateFrom.Value.Year, dpDateFrom.Value.Month, dpDateFrom.Value.Day);
                int days = Convert.ToInt32((dt1 - dt2).TotalDays) + 1;
                lblNum_numberoftraveldays.Text = days.ToString();
                maskedDayOfArr.Text = days.ToString();
                if (arrange.daysFirstPayment != null && arrange.daysFirstPayment != 0)
                    txtdaydFirstPayment.Value = arrange.daysFirstPayment;
                if (arrange.daysLastPayment != null && arrange.daysLastPayment != 0)
                    txtDaysLastPayment.Value = arrange.daysLastPayment;
                if (arrange.percentFirstPayment != null && arrange.percentFirstPayment != 0)
                    txtFrstpaymentPercent.Value = arrange.percentFirstPayment;
                if (arrange.reservationCosts != null && arrange.reservationCosts != 0)
                    maskedReservationCosts.Value = arrange.reservationCosts;
                if (arrange.invoiceDescription != "")
                    txtInvoiceDescription.Text = arrange.invoiceDescription;


                ArrangementLabel = new List<LabelForArrangement>();
                ArrangementBUS ab = new ArrangementBUS();
                ArrangementLabel = ab.GetLabelsArrangement(arrange.idArrangement);

                foreach (Control c in panelLabels.Controls)
                {
                    RadCheckBox chk = (RadCheckBox)c;
                    if (ArrangementLabel.Find(s => s.idLabel.ToString() == chk.Name.Replace("chkLabel", "")) != null)
                    {
                        insLabel = ArrangementLabel.Find(s => s.idLabel.ToString() == chk.Name.Replace("chkLabel", "")).idLabel;
                        chk.CheckState = CheckState.Checked;
                    }
                    else
                        chk.CheckState = CheckState.Unchecked;
                }
                LoadTabCalculation();
            }
            else
            {
                arrange = new ArrangementModel();
                arrangeFirst = new ArrangementModel();
            }
            double d = 5.5;
            numTravelInsurance.Text = d.ToString();

            // set flag isFinished for calculation
            ArrangementCalculationBUS acb = new ArrangementCalculationBUS();
            if (acb.isCalculationFinished(iID) == true)
            {
                isFinished = true;
                chkRecalculation.Text = "Recalculation";
                tabPurchaseCopy.Item.Visibility = ElementVisibility.Visible;
                tabPurchaseCopy.Text = "Recalculation";
                firstNotArticles = new ArrangementCalculationFirstNotArticlesModel();
                firstNotArticles = new ArrangementCalculationFirstArticlesBUS().GetNotArticles(arrange.idArrangement);

                maskedNrTravelers.Value = firstNotArticles.nrTraveler;
                lblNum_numberoftravelers.Text = firstNotArticles.nrTraveler.ToString();

                maskedNrVoluntary.Value = firstNotArticles.nrVoluntaryHelper;
                lblNum_numberofvoluntaryhelpers.Text = firstNotArticles.nrVoluntaryHelper.ToString();
                int a = (firstNotArticles.nrVoluntaryHelper - 1);
                //if (a > 1)
                numNumberLeader.Text = a.ToString();
                //else
                //numNumberLeader.Text = "1";
                if (firstNotArticles!=null)
                if (firstNotArticles.idUserFinished!=0)
                {
                    string username = "";
                    UsersModel um = new UsersModel();
                    um = new UsersBUS().getUserExact(firstNotArticles.idUserFinished);
                    if(um!=null)
                    {
                         username = um.nameUser;
                         lblUserFinished.Text = username;
                         lblDtUserFinished.Text = firstNotArticles.dtUserFinished.ToString("dd-MM-yyyy");
                    }
                }

                dpDateFrom.Value = firstNotArticles.dtFromArrangement;
                dpDateTo.Value = firstNotArticles.dtToArrangement;
                DateTime dt1 = new DateTime(dpDateTo.Value.Year, dpDateTo.Value.Month, dpDateTo.Value.Day);
                DateTime dt2 = new DateTime(dpDateFrom.Value.Year, dpDateFrom.Value.Month, dpDateFrom.Value.Day);
                int days = Convert.ToInt32((dt1 - dt2).TotalDays) + 1;
                if (maskedDayOfArr.Text != days.ToString())
                    maskedDayOfArr.Text = days.ToString();
                numNrOfNights.Value = firstNotArticles.nrOfNights;
                if (firstNotArticles.idCountry == 0)
                    txtCountry.Text = "";
                else
                {
                    txtCountry.Text = new CountryBUS().GetCountryByID(firstNotArticles.idCountry).nameCountry.ToString();

                }
                CountryBUS cbb = new CountryBUS();
                CountryModel cmb = new CountryModel();
                cmb = cbb.GetCountryByID(Convert.ToInt32(firstNotArticles.idCountry));
                if (cmb != null)
                {
                    txtInsProvision.Text = cmb.provision.ToString();
                    txtInsPremie.Text = cmb.premie.ToString();
                }
                arrangementLabelFirst = new ArrangementCalculationFirstArticlesBUS().GetLabelsFirst(arrange.idArrangement);

                foreach (Control c in panelLabels.Controls)
                {
                    RadCheckBox chk = (RadCheckBox)c;
                    if (arrangementLabelFirst.Find(s => s.idLabel.ToString() == chk.Name.Replace("chkLabel", "")) != null)
                    {
                        insLabel = arrangementLabelFirst.Find(s => s.idLabel.ToString() == chk.Name.Replace("chkLabel", "")).idLabel;
                        chk.CheckState = CheckState.Checked;
                    }
                    else
                        chk.CheckState = CheckState.Unchecked;
                }

                lblNum_numberoftraveldays.Text = days.ToString();
                //maskedNrMaximumWheelchairs.Value = firstNotArticles.nrMaximumWheelchairs;
                //maskedWhooseWheelchairs.Value = firstNotArticles.whoseElectricWheelchairs;
                //maskedArms.Value = firstNotArticles.buSupportingArms;
                //maskedAnchorage.Value = firstNotArticles.nrAnchorage;
                isRecalculationLoaded = true;
            }
            else
            {
                tabPurchaseCopy.Item.Visibility = ElementVisibility.Collapsed;
            }

            btnPurchase.Click += btnPurchase_Click;
            btnDelPurchase.Click += btnDelPurchase_Click;

            radPageArrange.SelectedPage = tabArrangement;
            
            radPageComm.SelectedPage = tabMeetings;

            if (Login._companyModelList[0].flag == "B")
            {
                radPageExtras.SelectedPage = tabBoardingPoint;
                tabTargetGroup.Item.Visibility = ElementVisibility.Collapsed;
                //  tabThemeTrip.Item.Visibility = ElementVisibility.Collapsed;
            }
            else
            {
                radPageExtras.SelectedPage = tabTargetGroup;
                LoadTargetGroup();
            }

            LoadBoardingPoint();
            setTranslation();


            LoadCalculationRecalculation(true);
        }
        //count ratio
        static int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        private ArrangementCalculationFirstNotArticlesModel saveNotArticles()
        {
            ArrangementCalculationFirstArticlesBUS arrcalcbus = new ArrangementCalculationFirstArticlesBUS();
            ArrangementCalculationFirstNotArticlesModel arrCalcNotArticlesModel = new ArrangementCalculationFirstNotArticlesModel();
            arrCalcNotArticlesModel.idArrangement = arrange.idArrangement;
            arrCalcNotArticlesModel.dtFromArrangement = arrange.dtFromArrangement;
            arrCalcNotArticlesModel.dtToArrangement = arrange.dtToArrangement;
            arrCalcNotArticlesModel.nrTraveler = arrange.nrTraveler;
            arrCalcNotArticlesModel.minNrTraveler = arrange.minNrTraveler;
            arrCalcNotArticlesModel.nrVoluntaryHelper = arrange.nrVoluntaryHelper;
            arrCalcNotArticlesModel.nrOfNights = arrange.nrOfNights;
            arrCalcNotArticlesModel.idCountry = Convert.ToInt32(arrange.countryArrangement);
            arrCalcNotArticlesModel.idUserFinished = Login._user.idUser;
            arrCalcNotArticlesModel.dtUserFinished = DateTime.Now;
            //arrCalcNotArticlesModel.nrMaximumWheelchairs = Convert.ToInt32(arrange.nrMaximumWheelchairs);
            //arrCalcNotArticlesModel.whoseElectricWheelchairs = arrange.whoseElectricWheelchairs;
           // arrCalcNotArticlesModel.buSupportingArms = Convert.ToInt32(arrange.buSupportingArms);
           // arrCalcNotArticlesModel.nrAnchorage = arrange.nrAnchorage;
            return arrCalcNotArticlesModel;

        }

        private ArrangementCalculationModel saveCalculation()
        {
            ArrangementCalculationBUS arrcalcbus = new ArrangementCalculationBUS();
            arrCalcModel = new ArrangementCalculationModel();

            arrCalcModel.idArrangement = arrange.idArrangement;
            arrCalcModel.correction = Convert.ToDecimal(maskedDiverseCorrection.Value);
            arrCalcModel.provision = Convert.ToDecimal(maskedDiversePRovision.Value);
            arrCalcModel.travelInsurace = Convert.ToDecimal(maskedDiverseTravelInsurance.Value);
            arrCalcModel.calamiteitenFonds = Convert.ToDecimal(maskedCalamiteit.Value);
            arrCalcModel.travelInsurance2 = Convert.ToDecimal(numTravelInsurance.Value);
            arrCalcModel.polisCosts = Convert.ToDecimal(numPolisCosts.Value);
            arrCalcModel.price = Convert.ToDecimal(numFinalPrice.Text);
            arrCalcModel.insuranceVolontary = Convert.ToDecimal(numVerzBeg.Value);
            arrCalcModel.singleRoomPrice = Convert.ToDecimal(numSurcharge.Value);
            arrCalcModel.discount = Convert.ToDecimal(numDiscount.Value);
            arrCalcModel.txt = txt.Text;
            arrCalcModel.txtAmount = Convert.ToDecimal(numtxtAmount.Value);
            arrCalcModel.moneyGroup = Convert.ToDecimal(numGroupMoney.Value);
            arrCalcModel.numberLeader = Convert.ToInt32(numNumberLeader.Value);
            arrCalcModel.premie1 = Convert.ToDecimal(maskedPremie1.Value);
            arrCalcModel.premie2 = Convert.ToDecimal(maskedPremie2.Value);
            arrCalcModel.numberCO = Convert.ToInt32(numCO.Value);
            arrCalcModel.minNumberTravelers = Convert.ToInt32(numMinNumberTravelers.Value);
            arrCalcModel.nrVoluntary = Convert.ToInt32(numNrVoluntary.Value);
            arrCalcModel.volontaryDays = Convert.ToInt32(maskedVolDays.Value);
            if (chkSport.Checked == true)
                arrCalcModel.isSport = true;
            else
                arrCalcModel.isSport = false;
            return arrCalcModel;

        }


        private Boolean saveLabels()
        {
            Boolean result = false;
            ArrangementLabel = new List<LabelForArrangement>();
            foreach (Control c in panelLabels.Controls)
            {
                RadCheckBox rch = (RadCheckBox)c;
                if (rch.Checked == true)
                {
                    LabelForArrangement lab = new LabelForArrangement();
                    lab.idLabel = Login._arrLabels.Find(item => item.nameLabel.TrimEnd() == rch.Text.TrimEnd()).idLabel;
                    lab.idArrangement = arrange.idArrangement;
                    ArrangementLabel.Add(lab);
                    if (isFinished == false)
                    {
                        arrangementLabelFirst = new List<LabelForArrangement>();
                        arrangementLabelFirst.Add(lab);
                    }
                    result = true;
                }
            }
            return result;
        }

        private void setTranslation()
        {

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("Document") != null)
                    radRibbonDocuments.Text = resxSet.GetString("Document");

                if (resxSet.GetString("New") != null)
                    btnNewDoc.Text = resxSet.GetString("New");

                if (resxSet.GetString("Delete") != null)
                    btnDeleteDoc.Text = resxSet.GetString("Delete");

                if (resxSet.GetString(lblArrId.Text) != null)
                    lblArrId.Text = resxSet.GetString(lblArrId.Text);
                if (resxSet.GetString(lblArrCode.Text) != null)
                    lblArrCode.Text = resxSet.GetString(lblArrCode.Text);
                if (resxSet.GetString(lblArrName.Text) != null)
                    lblArrName.Text = resxSet.GetString(lblArrName.Text);
                if (resxSet.GetString(lblDateFrom.Text) != null)
                    lblDateFrom.Text = resxSet.GetString(lblDateFrom.Text);
                if (resxSet.GetString(lblDateTo.Text) != null)
                    lblDateTo.Text = resxSet.GetString(lblDateTo.Text);
                //for days and nigths number
                if (resxSet.GetString(lblDaysArrangement.Text) != null)
                    lblDaysArrangement.Text = resxSet.GetString(lblDaysArrangement.Text);
                if (resxSet.GetString(lblNoOfNights.Text) != null)
                    lblNoOfNights.Text = resxSet.GetString(lblNoOfNights.Text);

                if (resxSet.GetString(lblCity.Text) != null)
                    lblCity.Text = resxSet.GetString(lblCity.Text);
                if (resxSet.GetString(lblCountry.Text) != null)
                    lblCountry.Text = resxSet.GetString(lblCountry.Text);
                if (resxSet.GetString(lblArrType.Text) != null)
                    lblArrType.Text = resxSet.GetString(lblArrType.Text);
                if (resxSet.GetString(btnSave.Text) != null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);
                if (resxSet.GetString(lblNrTravelers.Text) != null)
                    lblNrTravelers.Text = resxSet.GetString(lblNrTravelers.Text);
                if (resxSet.GetString(lblMinNrTravelers.Text) != null)
                    lblMinNrTravelers.Text = resxSet.GetString(lblMinNrTravelers.Text);
                if (resxSet.GetString(lblNrVoluntaryHelpers.Text) != null)
                    lblNrVoluntaryHelpers.Text = resxSet.GetString(lblNrVoluntaryHelpers.Text);
                if (resxSet.GetString(lblNumberOfTravelers.Text) != null)
                    lblNumberOfTravelers.Text = resxSet.GetString(lblNumberOfTravelers.Text);
                if (resxSet.GetString(lblMinimumNumberOfTravelers.Text) != null)
                    lblMinimumNumberOfTravelers.Text = resxSet.GetString(lblMinimumNumberOfTravelers.Text);
                if (resxSet.GetString(lblNumberofVoluntaryHelpers.Text) != null)
                    lblNumberofVoluntaryHelpers.Text = resxSet.GetString(lblNumberofVoluntaryHelpers.Text);
                if (resxSet.GetString(lblNumberofTravelDays.Text) != null)
                    lblNumberofTravelDays.Text = resxSet.GetString(lblNumberofTravelDays.Text);
                if (resxSet.GetString(lblDiverseProvision.Text) != null)
                    lblDiverseProvision.Text = resxSet.GetString(lblDiverseProvision.Text);
                if (resxSet.GetString(lblDiverseCalamiteitenfonds.Text) != null)
                    lblDiverseCalamiteitenfonds.Text = resxSet.GetString(lblDiverseCalamiteitenfonds.Text);
                if (resxSet.GetString(lblDivereSubtotal.Text) != null)
                    lblDivereSubtotal.Text = resxSet.GetString(lblDivereSubtotal.Text);
                if (resxSet.GetString(lblDiverseTravelInsurance.Text) != null)
                    lblDiverseTravelInsurance.Text = resxSet.GetString(lblDiverseTravelInsurance.Text);
                if (resxSet.GetString(lblPrice.Text) != null)
                    lblPrice.Text = resxSet.GetString(lblPrice.Text);
                if (resxSet.GetString(lblTravelInsurance2.Text) != null)
                    lblTravelInsurance2.Text = resxSet.GetString(lblTravelInsurance2.Text);
                if (resxSet.GetString(lblPolisCosts.Text) != null)
                    lblPolisCosts.Text = resxSet.GetString(lblPolisCosts.Text);
                if (resxSet.GetString(lblTotalTravelInsurance.Text) != null)
                    lblTotalTravelInsurance.Text = resxSet.GetString(lblTotalTravelInsurance.Text);
                if (resxSet.GetString(lblFinalPrice.Text) != null)
                    lblFinalPrice.Text = resxSet.GetString(lblFinalPrice.Text);
                if (resxSet.GetString(lblHotelService.Text) != null)
                    lblHotelService.Text = resxSet.GetString(lblHotelService.Text);
                if (resxSet.GetString(chkWeb.Text) != null)
                    chkWeb.Text = resxSet.GetString(chkWeb.Text);
                if (resxSet.GetString(lblMaleHelpers.Text) != null)
                    lblMaleHelpers.Text = resxSet.GetString(lblMaleHelpers.Text);
                // tab Accomp
                if (resxSet.GetString(lblAccompTrasnport.Text) != null)
                    lblAccompTrasnport.Text = resxSet.GetString(lblAccompTrasnport.Text);
                if (resxSet.GetString(lblAccompAccomodation.Text) != null)
                    lblAccompAccomodation.Text = resxSet.GetString(lblAccompAccomodation.Text);
                if (resxSet.GetString(lblAccompArrangementPrice.Text) != null)
                    lblAccompArrangementPrice.Text = resxSet.GetString(lblAccompArrangementPrice.Text);
                if (resxSet.GetString(lblAccompExtras.Text) != null)
                    lblAccompExtras.Text = resxSet.GetString(lblAccompExtras.Text);
                if (resxSet.GetString(lblAccompCalamiteitenfonds.Text) != null)
                    lblAccompCalamiteitenfonds.Text = resxSet.GetString(lblAccompCalamiteitenfonds.Text);
                if (resxSet.GetString(lblAcompTotal.Text) != null)
                    lblAcompTotal.Text = resxSet.GetString(lblAcompTotal.Text);
                if (resxSet.GetString(lblAcoompSubtotal.Text) != null)
                    lblAcoompSubtotal.Text = resxSet.GetString(lblAcoompSubtotal.Text);
                if (resxSet.GetString(lblSurcharge.Text) != null)
                    lblSurcharge.Text = resxSet.GetString(lblSurcharge.Text);
                if (resxSet.GetString(lblVerzBeg.Text) != null)
                    lblVerzBeg.Text = resxSet.GetString(lblVerzBeg.Text);
                if (resxSet.GetString(lblDiscount.Text) != null)
                    lblDiscount.Text = resxSet.GetString(lblDiscount.Text);
                if (resxSet.GetString(lblDiverseCorrection.Text) != null)
                    lblDiverseCorrection.Text = resxSet.GetString(lblDiverseCorrection.Text);
                if (resxSet.GetString(lblSupportingArms.Text) != null)
                    lblSupportingArms.Text = resxSet.GetString(lblSupportingArms.Text);
                if (resxSet.GetString(lblStatus.Text) != null)
                    lblStatus.Text = resxSet.GetString(lblStatus.Text);
                if (resxSet.GetString(btnNewItem.Text) != null)
                    btnNewItem.Text = resxSet.GetString(btnNewItem.Text);
                //
                if (resxSet.GetString(lbldaysFirsPayment.Text) != null)
                    lbldaysFirsPayment.Text = resxSet.GetString(lbldaysFirsPayment.Text);
                if (resxSet.GetString(lbldaysLastPayment.Text) != null)
                    lbldaysLastPayment.Text = resxSet.GetString(lbldaysLastPayment.Text);
                if (resxSet.GetString(lblPercentFrstpayment.Text) != null)
                    lblPercentFrstpayment.Text = resxSet.GetString(lblPercentFrstpayment.Text);


                if (resxSet.GetString(radMenuItemSavePurchase.Text) != null)
                    radMenuItemSavePurchase.Text = resxSet.GetString(radMenuItemSavePurchase.Text);

                if (resxSet.GetString(radMenuItemArrangementTransport.Text) != null)
                    radMenuItemArrangementTransport.Text = resxSet.GetString(radMenuItemArrangementTransport.Text);

                if (resxSet.GetString(radMenuItemArrangementPrice.Text) != null)
                    radMenuItemArrangementPrice.Text = resxSet.GetString(radMenuItemArrangementPrice.Text);

                if (resxSet.GetString(radMenuItemAccomodSaveLayout.Text) != null)
                    radMenuItemAccomodSaveLayout.Text = resxSet.GetString(radMenuItemAccomodSaveLayout.Text);

                if (resxSet.GetString(radMenuItemExtrasSaveLayout.Text) != null)
                    radMenuItemExtrasSaveLayout.Text = resxSet.GetString(radMenuItemExtrasSaveLayout.Text);

                if (resxSet.GetString(btnAll.Text) != null)
                    btnAll.Text = resxSet.GetString(btnAll.Text);
                if (resxSet.GetString(btnNewBoardingPoint.Text) != null)
                    btnNewBoardingPoint.Text = resxSet.GetString(btnNewBoardingPoint.Text);
                if (resxSet.GetString(btnNewThemeTrip.Text) != null)
                    btnNewThemeTrip.Text = resxSet.GetString(btnNewThemeTrip.Text);
                if (resxSet.GetString(btnTgAll.Text) != null)
                    btnTgAll.Text = resxSet.GetString(btnTgAll.Text);


                //tab wheelcharis translate
                if (resxSet.GetString(lblAgeCateogory.Text) != null)
                    lblAgeCateogory.Text = resxSet.GetString(lblAgeCateogory.Text);
                if (resxSet.GetString(lblNrMaximumWheelchairs.Text) != null)
                    lblNrMaximumWheelchairs.Text = resxSet.GetString(lblNrMaximumWheelchairs.Text);
                if (resxSet.GetString(lblWhoseWheelchairs.Text) != null)
                    lblWhoseWheelchairs.Text = resxSet.GetString(lblWhoseWheelchairs.Text);

                if (resxSet.GetString(lblAnchorage.Text) != null)
                    lblAnchorage.Text = resxSet.GetString(lblAnchorage.Text);
                //
                if (resxSet.GetString(lblInsuranceProvision.Text) != null)
                    lblInsuranceProvision.Text = resxSet.GetString(lblInsuranceProvision.Text);
                if (resxSet.GetString(lblInsurancePremie.Text) != null)
                    lblInsurancePremie.Text = resxSet.GetString(lblInsurancePremie.Text);
                if (resxSet.GetString(lblVolDays.Text) != null)
                    lblVolDays.Text = resxSet.GetString(lblVolDays.Text);
                if (resxSet.GetString(lblInsuranceSport.Text) != null)
                    lblInsuranceSport.Text = resxSet.GetString(lblInsuranceSport.Text);
                if (resxSet.GetString(lblReservationCosts.Text) != null)
                    lblReservationCosts.Text = resxSet.GetString(lblReservationCosts.Text);

                //translate all tabs
                for (int i = 0; i < radPageArrange.Pages.Count; i++)
                {
                    if (resxSet.GetString(radPageArrange.Pages[i].Text) != null)
                        radPageArrange.Pages[i].Text = resxSet.GetString(radPageArrange.Pages[i].Text);
                }

                //translate all tabs
                

                for (int i = 0; i < radPageViewCalculation.Pages.Count; i++)
                {
                    if (resxSet.GetString(radPageViewCalculation.Pages[i].Text) != null)
                        radPageViewCalculation.Pages[i].Text = resxSet.GetString(radPageViewCalculation.Pages[i].Text);
                }

                for (int i = 0; i < radPageExtras.Pages.Count; i++)
                {
                    if (resxSet.GetString(radPageExtras.Pages[i].Text) != null)
                        radPageExtras.Pages[i].Text = resxSet.GetString(radPageExtras.Pages[i].Text);
                }

                if (resxSet.GetString(chkRecalculation.Text) != null)
                    chkRecalculation.Text = resxSet.GetString(chkRecalculation.Text);
            }
        }


        private void saveArrangement()
        {
            if (iID != -1)
                arrange.idArrangement = Convert.ToInt32(txtArrId.Text);
            arrange.codeArrangement = txtArrCode.Text;
            arrange.nameArrangement = txtArrName.Text;
            arrange.dtFromArrangement = Convert.ToDateTime(dpDateFrom.Text);
            arrange.dtToArrangement = Convert.ToDateTime(dpDateTo.Text);
            arrange.nrOfNights = Convert.ToInt32(numNrOfNights.Value);
            arrange.cityArrangement = txtCity.Text;
            if (txtCountry.Text.Trim() == "")
                arrange.countryArrangement = 0;
            if (txtType.Text.Trim() == "")
                arrange.typeArrangement = 0;
            arrange.nrTraveler = Convert.ToInt32(maskedNrTravelers.Value);
            arrange.nrVoluntaryHelper = Convert.ToInt32(maskedNrVoluntary.Value);
            arrange.minNrTraveler = Convert.ToInt32(maskedMinNrTravelers.Value);
            arrange.nrMaleVoluntary = Convert.ToInt32(maskedNrMaleVoluntary.Value);
            arrange.idHotelService = Convert.ToInt32(ddlHotelService.SelectedValue);
            if (chkWeb.CheckState == CheckState.Checked)
                arrange.isWeb = true;
            else
                arrange.isWeb = false;

            arrange.idAgeCategory = Convert.ToInt32(dropdownAgeCategory.SelectedValue);

            arrange.nrMaximumWheelchairs = Convert.ToInt32(maskedNrMaximumWheelchairs.Value);
            arrange.nrAnchorage = Convert.ToInt32(maskedAnchorage.Value);
            arrange.whoseElectricWheelchairs = Convert.ToInt32(maskedWhooseWheelchairs.Value);
            arrange.buSupportingArms = Convert.ToInt32(maskedArms.Value);
            //================= polje sadrzi codeArrangement+dtFromArrangement i koristi se kao idProjekat u finansijama ==========
            string codeAccount = "";

            string dd = dpDateFrom.Value.Day.ToString();
            if (dd.Length == 1)
                dd = dd.PadLeft(2, '0');
            string mm = dpDateFrom.Value.Month.ToString();
            if (mm.Length == 1)
                mm = mm.PadLeft(2, '0');
            string yy = dpDateFrom.Value.Year.ToString();
            yy = yy.Substring(2, 2);
            string SubString = yy + mm + dd;
            codeAccount = txtArrCode.Text + SubString;
            arrange.codeProject = codeAccount;
            //=====================================================================================================================
            arrange.daysFirstPayment = Convert.ToInt32(txtdaydFirstPayment.Value);
            arrange.daysLastPayment = Convert.ToInt32(txtDaysLastPayment.Value);
            arrange.percentFirstPayment = Convert.ToDecimal(txtFrstpaymentPercent.Value);
            arrange.reservationCosts = Convert.ToDecimal(maskedReservationCosts.Value);
            if (txtInvoiceDescription.Text.Length <= 180)
                arrange.invoiceDescription = txtInvoiceDescription.Text;
            else
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Maximum length of invoice desription is 180 characters");
            }



        }
        private bool InsertArrangement()
        {
            Boolean ReloadCalculation = false;
            Boolean isSuccessfully = false;
            ArrangementBUS bus = new ArrangementBUS();
            
            if (iID != -1)
            {

                arrange.idArrangement = iID;
                saveThemeTrip();
                saveBoardingPoint();
                saveTargetGroup();
                isOk = bus.Update(arrange, this.Name, Login._user.idUser);
                if (isOk == true)
                {
                    isSuccessfully = true;
                    modelChanged = true;
                    saveLabels();
                    if (ArrangementLabel != null)
                    {

                        if (bus.DeleteLabel(arrange.idArrangement, this.Name, Login._user.idUser) == true)
                        {
                            for (int d = 0; d < ArrangementLabel.Count; d++)
                            {
                                ArrangementLabel[d].idArrangement = arrange.idArrangement;
                                if (bus.SaveLabel(ArrangementLabel[d], this.Name, Login._user.idUser) == true)
                                {
                                    isSuccessfully = true;
                                }
                                else
                                {
                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translatePartAndNonTranslatedPart("You have not successfully save data for label ", (d + 1).ToString());
                                    isSuccessfully = false;
                                }
                            }
                        }
                        else
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("You have not successfully save data for labels");
                            isSuccessfully = false;
                        }
                       
                    }
                }
                else
                {
                    isSuccessfully = false;
                }

            }
            else
            {
                int lastId = bus.Save(arrange, this.Name, Login._user.idUser);
                if (lastId >= 0)
                {
                    modelChanged = true;
                    isSuccessfully = true;
                    arrange.idArrangement = lastId;
                    iID = lastId;
                    saveThemeTrip();
                    saveBoardingPoint();
                    txtArrId.Text = iID.ToString();
                    isOk = true;
                    translateRadMessageBox trm = new translateRadMessageBox();
                    saveLabels();
                    if (ArrangementLabel != null)
                    {

                        if (bus.DeleteLabel(arrange.idArrangement, this.Name, Login._user.idUser) == true)
                        {
                            for (int d = 0; d < ArrangementLabel.Count; d++)
                            {
                                if (bus.SaveLabel(ArrangementLabel[d], this.Name, Login._user.idUser) == true)
                                {

                                    modelChanged = true;

                                }
                                else
                                {
                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translatePartAndNonTranslatedPart("You have not successfully save data for label ", (d + 1).ToString());
                                }
                            }
                        }
                        else
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("You have not successfully save data for labels");
                        }

                    }
                }
                else
                {
                    isSuccessfully = false;
                }
                for (int i = 0; i < radPageArrange.Pages.Count; i++)
                {
                    radPageArrange.Pages[i].Enabled = true;
                }
            }
            if (isFinished == true && chkRecalculation.CheckState == CheckState.Unchecked)
            {
                if (isSuccessfully == true)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You have successfully save basic data for arrangement, boardint point, theme trip, rooms and invoice.");
                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You have not successfully save basic data for arrangement, boardint point, theme trip, rooms and invoice.");
                }
            }
            else
            {
                if (isFinished == true && chkRecalculation.CheckState == CheckState.Checked)
                {
                    saveRecalculationToModel();
                    if (new ArrangementCalculationBUS().SaveSecond(recalcCalculationModel, this.Name, Login._user.idUser) == false)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have not successfully finish recalculation");
                    }

                }
                else
                {
                    rgvTotalExtra.DataSource = null;
                    FIllTotalExtra();
                    ArrangementCalculationModel arrm = new ArrangementCalculationModel();
                    arrm = saveCalculation();
                    if (new ArrangementCalculationBUS().Save(arrm, this.Name, Login._user.idUser) == false)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have not successfully save calculation");
                    }

                    else if (chkRecalculation.CheckState == CheckState.Checked && isFinished == false)
                    {
                        saveRecalculationToModel();
                        firstNotArticles = new ArrangementCalculationFirstNotArticlesModel();
                        firstNotArticles = saveNotArticles();
                        loadPurchase(arrange.idArrangement, false);
                        if (new ArrangementCalculationFirstArticlesBUS().SaveFirst(arrange.idArrangement, (List<IModel>)rgvPurchase.DataSource, this.Name, Login._user.idUser) == false)
                        {
                            if (rgvPurchase != null)
                                if (rgvPurchase.RowCount > 0)
                                {
                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translateAllMessageBox("You have not successfully save first calculation");
                                }
                                else
                                {
                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translateAllMessageBox("You don't have any article so calculation can not be saved!");
                                }
                        }
                        else if (new ArrangementCalculationBUS().SaveFinished(arrm.idArrangement, this.Name, Login._user.idUser) == false)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("You have not successfully finish first calculation");
                        }
                        else if (rgvArrangementPrice.Rows.Count > 0)
                        {
                            if (new ArrangementCalculationFirstArticlesBUS().SaveNotArticles(firstNotArticles, this.Name, Login._user.idUser) == false)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("You have not successfully save non articles settings for first calculation");
                            }
                            else if (new ArrangementCalculationFirstArticlesBUS().SaveLabelsFirst(arrangementLabelFirst, this.Name, Login._user.idUser) == false)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("You have not successfully save labels for first calculation");
                            }
                            else if (new ArrangementCalculationBUS().SaveSecond(recalcCalculationModel, this.Name, Login._user.idUser) == false)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("You have not successfully finish recalculation");
                            }
                            else
                            {
                                isFinished = true;
                                ReloadCalculation = true;
                                chkRecalculation.Text = "Recalculation";
                                chkRecalculation.CheckState = CheckState.Unchecked;
                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                {
                                    if (resxSet.GetString(chkRecalculation.Text) != null)
                                        chkRecalculation.Text = resxSet.GetString(chkRecalculation.Text);
                                }

                            }
                            //  }
                        }
                        else if (new ArrangementCalculationFirstArticlesBUS().SaveNotArticles(firstNotArticles, this.Name, Login._user.idUser) == false)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("You have not successfully save non articles settings for first calculation");
                        }
                        else if (new ArrangementCalculationFirstArticlesBUS().SaveLabelsFirst(arrangementLabelFirst, this.Name, Login._user.idUser) == false)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("You have not successfully save labels for first calculation");
                        }
                        else if (new ArrangementCalculationBUS().SaveSecond(recalcCalculationModel, this.Name, Login._user.idUser) == false)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("You have not successfully finish recalculation");
                        }
                        else
                        {
                            isFinished = true;
                            ReloadCalculation = true;
                            chkRecalculation.Text = "Recalculation";
                            chkRecalculation.CheckState = CheckState.Unchecked;
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {
                                if (resxSet.GetString(chkRecalculation.Text) != null)
                                    chkRecalculation.Text = resxSet.GetString(chkRecalculation.Text);
                            }

                        }

                    }
                }

                if (isSuccessfully == true)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You have successfully save data");
                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You have not successfully save data");
                }


                //============================================= ponovo preracunavanje calculacije

                for (int i = 0; i < ArrangementLabel.Count; i++)
                {
                    //RadCheckBox chk = (RadCheckBox)tabRelation.Controls.Find("chkIsMaried", true)[0];
                    RadCheckBox chk = (RadCheckBox)tabArrangement.Controls.Find("chkLabel" + ArrangementLabel[i].idLabel.ToString(), true)[0];
                    if (ArrangementLabel[i].idLabel.ToString() != "")
                        insLabel = ArrangementLabel[i].idLabel;
                    //CheckBox chk = (CheckBox)  this.Controls.fFindControl(this, "btn3");
                    chk.CheckState = CheckState.Checked;
                }
                string nameLab = "";
                if (insLabel != null && insLabel != 0)
                {
                    LabelsBUS lbb = new LabelsBUS();
                    LabelModel lbm = new LabelModel();
                    lbm = lbb.GetLabelById(insLabel);
                    if (lbm != null)
                        nameLab = lbm.nameLabel;
                    ArrangementInsuranceBUS inb = new ArrangementInsuranceBUS();
                    ArrangementInsuranceModel inm = new ArrangementInsuranceModel();
                    inm = inb.GetArrangementInsuranceWithCountry(nameLab, txtInsProvision.Text, arrange.dtFromArrangement);
                    if (inm == null)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Can't read Provision !!! Calculation not be well");
                    }
                    else
                    {
                        provision = Convert.ToDecimal(inm.amountInsurance);
                        lblDiverseProvision_Value2.Text = provision.ToString();

                    }

                }
                ArrangementInsurancePremieBUS prb = new ArrangementInsurancePremieBUS();
                ArrangementInsurancePremieModel prm = new ArrangementInsurancePremieModel();
                string prem = "Premie 1";

                prm = prb.GetArrangementInsurancePremie(prem, txtInsPremie.Text, arrange.dtFromArrangement);
                if (prm != null)
                {
                    maskedPremie1.Value = prm.amountPremie;
                    premieNo1 = Convert.ToDecimal(prm.amountPremie);
                    if (chkSport.Checked == true)
                        maskedPremie1.Value = (premieNo1 + extra).ToString();
                    maskedDiverseTravelInsurance.Value = maskedPremie1.Value;
                }
                string prem2 = "Premie 2";
                ArrangementInsurancePremieModel prm1 = new ArrangementInsurancePremieModel();
                prm1 = prb.GetArrangementInsurancePremie(prem2, txtInsPremie.Text, arrange.dtFromArrangement);
                if (prm != null)
                {
                    maskedPremie2.Value = prm1.amountPremie;
                    premieNo2 = Convert.ToDecimal(prm1.amountPremie);
                    if (chkSport.Checked == true)
                        maskedPremie2.Value = (premieNo2 + extra).ToString();
                }


                //================================================= zavrsetak

                if (ReloadCalculation == true)
                {
                    LoadTabCalculation();
                }
            }
            if (ddlStatus.SelectedItem != null)
            {
                arrange.statusArrangement = ddlStatus.SelectedItem.ToString();
                if (statusList != null)
                {
                    if (isFinished && statusList.Count > 1 && arrange.statusArrangement == statusList.SingleOrDefault(s => s.idArrangementStatus == 1).nameArrangementStatus)
                    {
                        arrange.statusArrangement = statusList.SingleOrDefault(s => s.idArrangementStatus == 2).nameArrangementStatus;
                        ddlStatus.SelectedValue = statusList.SingleOrDefault(s => s.idArrangementStatus == 2).nameArrangementStatus;
                    }
                }
            }
            UpdateOriginalValuesAfterSave();
            return isSuccessfully;
        }

        private bool ValidateArrangement()
        {
            if (txtArrName.Text == "")
            {
                RadMessageBox.Show("Enter a Arrangement name, please!");
                return false;
            }

            //if (isFinished == true && chkRecalculation.CheckState == CheckState.Unchecked)
            //{
            //    translateRadMessageBox tr = new translateRadMessageBox();
            //    tr.translateAllMessageBox("You can't save first calculation please go to recalculation and save it!");
            //    return false;
            //}

            if (saveLabels() == false)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to check at least one label!");

                return false;
            }
            
            return true;
        }

        private void saveThemeTrip()
        {
            Boolean isSuccessfull = false;
            ArrangementThemeTripBUS ttb = new ArrangementThemeTripBUS();
            if (arrangeThemeTrip != null)
                if (arrangeThemeTrip.Count > 0)
                    for (int i = 0; i < arrangeThemeTrip.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (ttb.Delete(arrange.idArrangement, this.Name, Login._user.idUser) == true)
                                isSuccessfull = true;
                            else
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("You have not succesufully inserted theme trip. Please check!");
                            }
                        }
                        if (isSuccessfull == true)
                        {
                            if (arrangeThemeTrip[i].idArrangement == 0)
                                arrangeThemeTrip[i].idArrangement = arrange.idArrangement;
                            if (ttb.Save(arrangeThemeTrip[i], this.Name, Login._user.idUser) == false)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translatePartAndNonTranslatedPart("You have not succesufully inserted theme trip ", ttb.GetThemeTripName(arrangeThemeTrip[i].idThemeTrip));
                            }
                        }
                    }
        }

        private void saveBoardingPoint()
        {

            arrangeBoardingPoint.Clear();
            foreach (BoardingPointModel m in listaBoardingPoint)
            {
                if (m.isChecked == true)
                {
                    ArrangementBoardingPointModel model = new ArrangementBoardingPointModel();
                    model.idArrangement = arrange.idArrangement;
                    model.idBoardingPoint = m.idBoardingPoint;
                    model.dtDeparture = m.dtDeparture;
                    model.dtArrival = m.dtArrival;
                    model.sortBoardingPoint = m.sortBoardingPoint;
                    arrangeBoardingPoint.Add(model);
                }
            }


            Boolean isSuccessfull = false;
            ArrangementBoardingPointBUS ttb = new ArrangementBoardingPointBUS();
            if (arrangeBoardingPoint != null)
                if (arrangeBoardingPoint.Count > 0)
                    for (int i = 0; i < arrangeBoardingPoint.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (ttb.Delete(arrange.idArrangement, this.Name, Login._user.idUser) == true)
                                isSuccessfull = true;
                            else
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("You have not succesufully inserted boarding point. Please check!");
                            }
                        }
                        if (isSuccessfull == true)
                        {
                            if (ttb.Save(arrangeBoardingPoint[i], this.Name, Login._user.idUser) == false)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translatePartAndNonTranslatedPart("You have not succesufully inserted boarding point ", ttb.GetBoardingPointName(arrangeBoardingPoint[i].idBoardingPoint));
                            }
                        }
                    }
        }


        private void saveTargetGroup()
        {
            Boolean isSuccessfull = false;
            ArrangementTargetGroupBUS ttb = new ArrangementTargetGroupBUS();
            if (arrangeTargetGroup != null)
                if (arrangeTargetGroup.Count > 0)
                    for (int i = 0; i < arrangeTargetGroup.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (ttb.Delete(arrange.idArrangement, this.Name, Login._user.idUser) == true)
                                isSuccessfull = true;
                            else
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("You have not succesufully inserted target group. Please check!");
                            }
                        }
                        if (isSuccessfull == true)
                        {
                            if (arrangeTargetGroup[i].idArrangement == 0)
                                arrangeTargetGroup[i].idArrangement = arrange.idArrangement;
                            if (ttb.Save(arrangeTargetGroup[i], this.Name, Login._user.idUser) == false)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translatePartAndNonTranslatedPart("You have not succesufully inserted target group ", ttb.GetTargetGroupName(arrangeTargetGroup[i].idTargetGroup));
                            }
                        }
                    }
        }
        private void loadPurchase(int idArrangment, Boolean isFirst2)
        {
            rgvPurchase.DataSource = new ArrangementPriceBUS().GetAllArrangementPrices(idArrangment, isFirst2);
        }

        private void loadPurchaseCopy(int idArrangment, Boolean isFirst2)
        {
            if (isFinished == true)
            {
                Boolean differentFromPurchase = false;
                if (isFirst2 == true)
                    differentFromPurchase = false;
                else
                    differentFromPurchase = true;
                rgvPurchaseCopy.DataSource = new ArrangementPriceBUS().GetAllArrangementPrices(idArrangment, differentFromPurchase);
            }
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {

            if (iID == -1)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("First you have to add arrangement and after then you would be able to add purchase!");
            }
            else if (isFinished == true && chkRecalculation.CheckState == CheckState.Unchecked)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You can't make any changes in first calculation!");
            }
            else
            {
                Boolean isFirst2 = false;
                if (isFinished == true && chkRecalculation.CheckState == CheckState.Unchecked)
                    isFirst2 = true;

                using (frmArrangementPurchase frm = new frmArrangementPurchase(iID))
                {
                    frm.ShowDialog();
                    rgvPurchase.DataSource = null;
                    loadPurchase(iID, isFirst2);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }

        private void btnDelPurchase_Click(object sender, EventArgs e)
        {
            if (iID == -1)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("First you have to add arrangement and after then you would be able to add purchase!");
            }
            else if (isFinished == true && chkRecalculation.CheckState == CheckState.Unchecked)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You can't make any changes in first calculation!");
            }
            else
            {
                if (rgvPurchase != null)
                {
                    if (rgvPurchase.SelectedRows != null)
                    {
                        if (rgvPurchase.SelectedRows.Count > 0)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            string strContract = rgvPurchase.SelectedRows[0].Cells["nameContract"].Value.ToString();
                            if (strContract != "Contract")
                            {
                                if (tr.translateAllMessageBoxDialog("Are you sure delete arrangement purchase?", "Arrangement Purchase") == System.Windows.Forms.DialogResult.Yes)
                                {
                                    if (new ArrangementPriceBUS().checkIfArrangementPriceAlreadyInRooms(Convert.ToInt32(rgvPurchase.SelectedRows[0].Cells["idArrangementPrice"].Value.ToString()), Convert.ToInt32(rgvPurchase.SelectedRows[0].Cells["idArrangement"].Value.ToString()), rgvPurchase.SelectedRows[0].Cells["idArticle"].Value.ToString()) == false)
                                    {
                                        if (new ArrangementPriceBUS().Delete(Convert.ToInt32(rgvPurchase.SelectedRows[0].Cells["idArrangementPrice"].Value.ToString()), this.Name, Login._user.idUser) == true)
                                        {
                                            translateRadMessageBox trs = new translateRadMessageBox();
                                            trs.translateAllMessageBox("You have successfully delete arrangement purchase!");
                                            rgvPurchase.DataSource = null;
                                            Boolean isFirst2 = false;
                                            if (isFinished == true && chkRecalculation.CheckState == CheckState.Unchecked)
                                                isFirst2 = true;
                                            loadPurchase(iID, isFirst2);

                                        }
                                    }
                                    else
                                    {
                                        translateRadMessageBox trr = new translateRadMessageBox();
                                        trr.translateAllMessageBox("You can't delete this article because you have rooms for it!");
                                        return;


                                    }
                                }

                                else
                                {
                                    translateRadMessageBox trr = new translateRadMessageBox();
                                    trr.translateAllMessageBox("Something went wrong with deleting!");
                                    return;


                                }
                            }
                            else
                            {
                                translateRadMessageBox trr = new translateRadMessageBox();
                                trr.translateAllMessageBox("Cannot delete article that is contract.");
                                return;
                            }
                        }
                        else
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("There is no purchase selected!");
                        }
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("There is no purchase selected!");
                    }
                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("There is no purchase for this arrangment!");
                }
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            Boolean isSuccessfully = false;
            isOk = false;

            try
            {

                bool b = ValidateArrangement();
                if (b == false)
                {
                    return;
                }
                saveArrangement();
                isSuccessfully = InsertArrangement();

                       
                //if (isSuccessfully == true)
                //{
                //    translateRadMessageBox tr = new translateRadMessageBox();
                //    tr.translateAllMessageBox("You have successfully save data");
                //}
                //else
                //{
                //    translateRadMessageBox tr = new translateRadMessageBox();
                //    tr.translateAllMessageBox("You have not successfully save data");
                //}
            }
            catch (Exception ex)
            {
                translateRadMessageBox trm = new translateRadMessageBox();
                trm.translateAllMessageBox("Error saving.");
            }                                                                       
        }

        private void rbCountry_Click(object sender, EventArgs e)
        {
            CountryBUS accBUS = new CountryBUS();
            List<IModel> am = new List<IModel>();

            am = accBUS.GetCountries();


            using (var dlgClient = new GridLookupForm(am, "Country"))
            {

                if (dlgClient.ShowDialog(this) == DialogResult.Yes)
                {
                    CountryModel okm = new CountryModel();
                    okm = (CountryModel)dlgClient.selectedRow;
                    txtCountry.Text = okm.nameCountry;
                    txtInsProvision.Text = okm.provision.ToString();
                    txtInsPremie.Text = okm.premie.ToString();
                    string nameLab = "";
                    LabelsBUS lbb = new LabelsBUS();
                    LabelModel lbm = new LabelModel();
                    lbm = lbb.GetLabelById(insLabel);
                    if (lbm != null)
                        nameLab = lbm.nameLabel;
                    ArrangementInsuranceBUS inb = new ArrangementInsuranceBUS();
                    ArrangementInsuranceModel inm = new ArrangementInsuranceModel();
                    inm = inb.GetArrangementInsuranceWithCountry(nameLab, txtInsProvision.Text, dpDateFrom.Value);

                    if (isFinished == true && chkRecalculation.CheckState == CheckState.Unchecked)
                    {
                        firstNotArticles.idCountry = okm.idCountry;
                        if (inm != null)
                        {

                            firstCalculationModel.provision = inm.amountInsurance;
                            maskedDiversePRovision.Value = firstCalculationModel.provision;
                        }

                    }
                    else
                    {
                        arrange.countryArrangement = okm.idCountry;
                        arrange.countryNameArrangement = okm.nameCountry;
                        if (isFinished == true && chkRecalculation.CheckState == CheckState.Checked)
                        {
                            if (inm != null)
                            {
                                recalcCalculationModel.provision = inm.amountInsurance;
                                maskedDiversePRovision.Value = recalcCalculationModel.provision;
                            }

                        }
                    }

                    calculatePremies();

                }
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

        }

        private void rbType_Click(object sender, EventArgs e)
        {
            ArrTypeBUS accBUS = new ArrTypeBUS();
            List<IModel> am = new List<IModel>();

            am = accBUS.GetAllArrTypes();


            using (var dlgClient = new GridLookupForm(am, "Type"))
            {

                if (dlgClient.ShowDialog(this) == DialogResult.Yes)
                {
                    ArrTypeModel okm = new ArrTypeModel();
                    okm = (ArrTypeModel)dlgClient.selectedRow;
                    txtType.Text = okm.nameArrType;
                    arrange.typeArrangement = okm.idArrType;
                    arrange.typeNameArrangement = okm.nameArrType;

                }

            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }


        private void saveRecalculationToModel()
        {
            recalcCalculationModel = new ArrangementCalculationSecondModel();

            recalcCalculationModel.idArrangement = arrange.idArrangement;
            recalcCalculationModel.correction = Convert.ToDecimal(maskedDiverseCorrection.Value);
            recalcCalculationModel.provision = Convert.ToDecimal(maskedDiversePRovision.Value);
            recalcCalculationModel.travelInsurace = Convert.ToDecimal(maskedDiverseTravelInsurance.Value);
            recalcCalculationModel.calamiteitenFonds = Convert.ToDecimal(maskedCalamiteit.Value);
            recalcCalculationModel.travelInsurance2 = Convert.ToDecimal(numTravelInsurance.Value);
            recalcCalculationModel.polisCosts = Convert.ToDecimal(numPolisCosts.Value);
            recalcCalculationModel.price = Convert.ToDecimal(numFinalPrice.Text);
            recalcCalculationModel.insuranceVolontary = Convert.ToDecimal(numVerzBeg.Value);
            recalcCalculationModel.singleRoomPrice = Convert.ToDecimal(numSurcharge.Value);
            recalcCalculationModel.discount = Convert.ToDecimal(numDiscount.Value);
            recalcCalculationModel.txt = txt.Text;
            recalcCalculationModel.txtAmount = Convert.ToDecimal(numtxtAmount.Value);
            recalcCalculationModel.moneyGroup = Convert.ToDecimal(numGroupMoney.Value);
            recalcCalculationModel.numberLeader = Convert.ToInt32(numNumberLeader.Value);
            recalcCalculationModel.premie1 = Convert.ToDecimal(maskedPremie1.Value);
            recalcCalculationModel.premie2 = Convert.ToDecimal(maskedPremie2.Value);
            recalcCalculationModel.numberCO = Convert.ToInt32(numCO.Value);
            recalcCalculationModel.minNumberTravelers = Convert.ToInt32(numMinNumberTravelers.Value);
            recalcCalculationModel.nrVoluntary = Convert.ToInt32(numNrVoluntary.Value);
            recalcCalculationModel.volontaryDays = Convert.ToInt32(maskedVolDays.Value);
            if (chkSport.Checked == true)
                recalcCalculationModel.isSport = true;
            else
                recalcCalculationModel.isSport = false;


            //fields that are not in model
            recalcCalculationModel.nrTraveler = Convert.ToInt32(maskedNrTravelers.Value);
            recalcCalculationModel.nrVoluntaryHelper = Convert.ToInt32(maskedNrVoluntary.Value);

            arrange.nrTraveler = Convert.ToInt32(maskedNrTravelers.Value);
            arrange.nrVoluntaryHelper = Convert.ToInt32(maskedNrVoluntary.Value);
            arrange.minNrTraveler = Convert.ToInt32(maskedMinNrTravelers.Value);
            arrange.dtToArrangement = Convert.ToDateTime(dpDateTo.Value);
            arrange.dtFromArrangement = Convert.ToDateTime(dpDateFrom.Value);
            arrange.nrOfNights = Convert.ToInt32(numNrOfNights.Value);
            arrange.nrMaximumWheelchairs = Convert.ToInt32(maskedNrMaximumWheelchairs.Value);
            arrange.whoseElectricWheelchairs = Convert.ToInt32(maskedWhooseWheelchairs.Value);
            arrange.buSupportingArms = Convert.ToInt32(maskedArms.Value);
            arrange.nrAnchorage = Convert.ToInt32(maskedAnchorage.Value);

        }

        private void loadRecalculation()
        {
            recalcCalculationModel = new ArrangementCalculationSecondModel();
            ArrangementCalculationBUS acb = new ArrangementCalculationBUS();
            recalcCalculationModel = acb.GetArrangementCalculationSecond(arrange.idArrangement);
        }

        private bool LoadRecalculationFromModel()
        {
            foreach (Control c in panelLabels.Controls)
            {
                RadCheckBox chk = (RadCheckBox)c;
                if (ArrangementLabel.Find(s => s.idLabel.ToString() == chk.Name.Replace("chkLabel", "")) != null)
                {
                    insLabel = ArrangementLabel.Find(s => s.idLabel.ToString() == chk.Name.Replace("chkLabel", "")).idLabel;
                    chk.CheckState = CheckState.Checked;
                }
                else
                    chk.CheckState = CheckState.Unchecked;
            }

            //set all fields out of model
            maskedNrTravelers.Value = arrange.nrTraveler;
            lblNum_numberoftravelers.Text = arrange.nrTraveler.ToString();

            numNrVoluntary.Value = recalcCalculationModel.nrVoluntary;
            maskedNrVoluntary.Value = arrange.nrVoluntaryHelper;
            lblNum_numberofvoluntaryhelpers.Text = arrange.nrVoluntaryHelper.ToString();



            int a = (arrange.nrVoluntaryHelper - 1);

            if (Convert.ToInt32(numNrVoluntary.Value) != 0)
                a = (Convert.ToInt32(numNrVoluntary.Value) - 1);

            //if (a > 1)
            numNumberLeader.Text = a.ToString();
            //else
            //numNumberLeader.Text = "1";


            if (recalcCalculationModel != null)
            {

                if (arrange.countryArrangement != 0)
                {
                    txtCountry.Text = new CountryBUS().GetCountryByID(Convert.ToInt32(arrange.countryArrangement)).nameCountry;
                    CountryBUS cbb = new CountryBUS();
                    CountryModel cmb = new CountryModel();
                    cmb = cbb.GetCountryByID(Convert.ToInt32(arrange.countryArrangement));
                    if (cmb != null)
                    {
                        txtInsProvision.Text = cmb.provision.ToString();
                        txtInsPremie.Text = cmb.premie.ToString();
                    }
                }
                maskedDiverseCorrection.Value = recalcCalculationModel.correction;


                maskedDiversePRovision.Value = recalcCalculationModel.provision;
                maskedDiverseTravelInsurance.Value = recalcCalculationModel.travelInsurace;
                if (txtInsProvision.Text != "NL")
                {
                    maskedCalamiteit.Value = Convert.ToDecimal("2,50");
                }
                else
                {
                    maskedCalamiteit.Value = Convert.ToDecimal("0,00");
                }


                numGroupMoney.Value = recalcCalculationModel.moneyGroup;
                numFinalPrice.Text = recalcCalculationModel.price.ToString();
                numVerzBeg.Value = recalcCalculationModel.insuranceVolontary;
                numSurcharge.Value = recalcCalculationModel.singleRoomPrice;
                numDiscount.Value = recalcCalculationModel.discount;
                txt.Text = recalcCalculationModel.txt;
                numtxtAmount.Value = recalcCalculationModel.txtAmount;

                maskedPremie1.Value = recalcCalculationModel.premie1;
                maskedPremie2.Value = recalcCalculationModel.premie2;
                numCO.Value = recalcCalculationModel.numberCO;
                numMinNumberTravelers.Value = recalcCalculationModel.minNumberTravelers;
                maskedVolDays.Value = recalcCalculationModel.volontaryDays;
                //reset check
                chkSport.CheckState = CheckState.Unchecked;
                if (recalcCalculationModel.isSport == true)
                    chkSport.Checked = true;


                calculatePremies();

                dpDateFrom.Value = arrange.dtFromArrangement;
                dpDateTo.Value = arrange.dtToArrangement;
                DateTime dt1 = new DateTime(dpDateTo.Value.Year, dpDateTo.Value.Month, dpDateTo.Value.Day);
                DateTime dt2 = new DateTime(dpDateFrom.Value.Year, dpDateFrom.Value.Month, dpDateFrom.Value.Day);
                int days = Convert.ToInt32((dt1 - dt2).TotalDays) + 1;
                if (maskedDayOfArr.Text != days.ToString())
                    maskedDayOfArr.Text = days.ToString();
                numNrOfNights.Value = arrange.nrOfNights;
                maskedNrMaximumWheelchairs.Value = arrange.nrMaximumWheelchairs;
                maskedWhooseWheelchairs.Value = Convert.ToInt32(arrange.whoseElectricWheelchairs);
                maskedArms.Value = Convert.ToInt32(arrange.buSupportingArms);
                maskedAnchorage.Value = arrange.nrAnchorage;
                lblNum_numberoftraveldays.Text = days.ToString();
                //

                LoadAccomodation();
                loadArrangementsPrice();
                LoadExtras();
                if (Int32.Parse(maskedVolDays.Text) != days && Int32.Parse(maskedVolDays.Text) != 0)
                {
                    numDays.Value = Decimal.Parse(maskedVolDays.Text);
                }
                else
                    numDays.Value = Decimal.Parse(days.ToString());
                LoadAccompaniment();
                LoadDiverse();
                FIllTotalExtra();
                loadPrice();
                lblNum_numberoftravelers.Text = maskedNrTravelers.Value.ToString();
                lblNum_minimumnumberoftravelers.Text = maskedMinNrTravelers.Value.ToString();
                lblNum_numberofvoluntaryhelpers.Text = maskedNrVoluntary.Value.ToString();
                loadTransport();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void LoadTabCalculation()
        {
            if (isfirst == true)
            {
                isfirst = false;


                ArrangementCalculationBUS arrcalcbus = new ArrangementCalculationBUS();
                arrCalcModel = arrcalcbus.GetArrangementCalculation(arrange.idArrangement);
                if (arrCalcModel != null)
                {
                    maskedDiverseCorrection.Value = arrCalcModel.correction;

                    maskedDiversePRovision.Value = arrCalcModel.provision;
                    maskedDiverseTravelInsurance.Value = arrCalcModel.travelInsurace;

                    numGroupMoney.Value = arrCalcModel.moneyGroup;
                    numFinalPrice.Text = arrCalcModel.price.ToString();
                    numVerzBeg.Value = arrCalcModel.insuranceVolontary;
                    numSurcharge.Value = arrCalcModel.singleRoomPrice;
                    numDiscount.Value = arrCalcModel.discount;
                    txt.Text = arrCalcModel.txt;
                    numtxtAmount.Value = arrCalcModel.txtAmount;
                    maskedPremie1.Value = arrCalcModel.premie1;
                    maskedPremie2.Value = arrCalcModel.premie2;
                    numCO.Value = arrCalcModel.numberCO;
                    numMinNumberTravelers.Value = arrCalcModel.minNumberTravelers;
                    maskedVolDays.Value = arrCalcModel.volontaryDays;
                    numNrVoluntary.Value = arrCalcModel.nrVoluntary;
                    if (arrCalcModel.isSport == true)
                        chkSport.Checked = true;
                }

            }

            if (txtInsProvision.Text != "NL")
            {
                maskedCalamiteit.Value = Convert.ToDecimal("2,50");
            }
            else
            {
                maskedCalamiteit.Value = Convert.ToDecimal("0,00");
            }

            if (txtInsProvision.Text == "" && txtInsPremie.Text == "")
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("No fill Country Premie and Provision !!! Calculation not be well");
            }
            //=== reading insurance and premie
            string nameLab = "";
            if (insLabel != 0)
            {
                LabelsBUS lbb = new LabelsBUS();
                LabelModel lbm = new LabelModel();
                lbm = lbb.GetLabelById(insLabel);
                if (lbm != null)
                    nameLab = lbm.nameLabel;
                ArrangementInsuranceBUS inb = new ArrangementInsuranceBUS();
                ArrangementInsuranceModel inm = new ArrangementInsuranceModel();
                inm = inb.GetArrangementInsuranceWithCountry(nameLab, txtInsProvision.Text, dpDateFrom.Value);
                if (inm == null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Can't read Provision !!! Calculation not be well");
                }
                else
                {
                   
                        ////////provision = Convert.ToDecimal(inm.amountInsurance);  // ovo je parce starog koda
                        ////////maskedDiversePRovision.Value = provision;
                   
                    if (Convert.ToDecimal(maskedDiverseCorrection.Value) == 0)
                    {
                        provision = Convert.ToDecimal(inm.amountInsurance);
                        maskedDiversePRovision.Value = provision;
                    }
                    else
                    {
                        provision = Convert.ToDecimal(maskedDiverseCorrection.Value) / Convert.ToDecimal(lblDiverseProvision_Value1.Text);
                        maskedDiversePRovision.Value = provision;
                    }

                }

            }
            calculatePremies();

            DateTime dt1 = new DateTime(dpDateTo.Value.Year, dpDateTo.Value.Month, dpDateTo.Value.Day);
            DateTime dt2 = new DateTime(dpDateFrom.Value.Year, dpDateFrom.Value.Month, dpDateFrom.Value.Day);
            int days = Convert.ToInt32((dt1 - dt2).TotalDays) + 1;
            lblNum_numberoftraveldays.Text = days.ToString();

            LoadAccomodation();
            loadArrangementsPrice();
            LoadExtras();
            if (Int32.Parse(maskedVolDays.Text) != days && Int32.Parse(maskedVolDays.Text) != 0)
            {
                numDays.Value = Decimal.Parse(maskedVolDays.Text);
            }
            else
                numDays.Value = Decimal.Parse(days.ToString());
            LoadAccompaniment();
            LoadDiverse();
            loadPrice();
            lblNum_numberoftravelers.Text = maskedNrTravelers.Value.ToString();
            lblNum_minimumnumberoftravelers.Text = maskedMinNrTravelers.Value.ToString();
            lblNum_numberofvoluntaryhelpers.Text = maskedNrVoluntary.Value.ToString();
            loadTransport();
            if (arrCalcModel != null && isFinished == true)
            {
                firstCalculationModel = new ArrangementCalculationModel(arrCalcModel);
                loadRecalculation();
            }
        }

        private void calculatePremies()
        {
            ArrangementInsurancePremieBUS prb = new ArrangementInsurancePremieBUS();
            ArrangementInsurancePremieModel prm = new ArrangementInsurancePremieModel();
            string prem = "Premie 1";

            prm = prb.GetArrangementInsurancePremie(prem, txtInsPremie.Text, dpDateFrom.Value);
            if (prm != null)
            {
                maskedPremie1.Value = prm.amountPremie;
                premieNo1 = Convert.ToDecimal(prm.amountPremie);
                maskedDiverseTravelInsurance.Value = maskedPremie1.Value;
                if (chkSport.Checked == true)
                {
                    maskedPremie1.Value = (premieNo1 + extra).ToString();
                    maskedDiverseTravelInsurance.Value = maskedPremie1.Value;
                }
            }
            string prem2 = "Premie 2";
            ArrangementInsurancePremieModel prm1 = new ArrangementInsurancePremieModel();
            prm1 = prb.GetArrangementInsurancePremie(prem2, txtInsPremie.Text, dpDateFrom.Value);
            if (prm != null)
            {
                maskedPremie2.Value = prm1.amountPremie;
                premieNo2 = Convert.ToDecimal(prm1.amountPremie);
                if (chkSport.Checked == true)
                    maskedPremie2.Value = (premieNo2 + extra).ToString();
            }
        }

        private bool LoadTabCalculationFromModel()
        {


            arrangementLabelFirst = new ArrangementCalculationFirstArticlesBUS().GetLabelsFirst(arrange.idArrangement);

            foreach (Control c in panelLabels.Controls)
            {
                RadCheckBox chk = (RadCheckBox)c;
                if (arrangementLabelFirst.Find(s => s.idLabel.ToString() == chk.Name.Replace("chkLabel", "")) != null)
                {
                    insLabel = arrangementLabelFirst.Find(s => s.idLabel.ToString() == chk.Name.Replace("chkLabel", "")).idLabel;
                    chk.CheckState = CheckState.Checked;
                }
                else
                    chk.CheckState = CheckState.Unchecked;
            }

            //set all fields out of model
           maskedNrTravelers.Value = firstNotArticles.nrTraveler;
            lblNum_numberoftravelers.Text = firstNotArticles.nrTraveler.ToString();

            if (firstCalculationModel != null)
                numNrVoluntary.Value = firstCalculationModel.nrVoluntary;

            maskedNrVoluntary.Value = firstNotArticles.nrVoluntaryHelper;
            lblNum_numberofvoluntaryhelpers.Text = firstNotArticles.nrVoluntaryHelper.ToString();
            int a = (firstNotArticles.nrVoluntaryHelper - 1);
            if (Convert.ToInt32(numNrVoluntary.Value) != 0)
                a = (Convert.ToInt32(numNrVoluntary.Value) - 1);
            //if (a > 1)
            numNumberLeader.Text = a.ToString();
            //else
            //numNumberLeader.Text = "1";

            if (firstNotArticles != null)
                if (firstNotArticles.idUserFinished != 0)
                {
                    string username = "";
                    UsersModel um = new UsersModel();
                    um = new UsersBUS().getUserExact(firstNotArticles.idUserFinished);
                    if (um != null)
                    {
                        username = um.nameUser;
                        lblUserFinished.Text = username;
                        lblDtUserFinished.Text = firstNotArticles.dtUserFinished.ToString("dd-MM-yyyy");
                    }
                }

            if (firstNotArticles.idCountry == 0)
                txtCountry.Text = "";
            else
            {
                txtCountry.Text = new CountryBUS().GetCountryByID(firstNotArticles.idCountry).nameCountry.ToString();

            }
            CountryBUS cbb = new CountryBUS();
            CountryModel cmb = new CountryModel();
            cmb = cbb.GetCountryByID(Convert.ToInt32(firstNotArticles.idCountry));
            if (cmb != null)
            {
                txtInsProvision.Text = cmb.provision.ToString();
                txtInsPremie.Text = cmb.premie.ToString();
            }

           // maskedNrMaximumWheelchairs.Value = firstNotArticles.nrMaximumWheelchairs;
           // maskedWhooseWheelchairs.Value = firstNotArticles.whoseElectricWheelchairs;
          //  maskedArms.Value = firstNotArticles.buSupportingArms;
         //   maskedAnchorage.Value = firstNotArticles.nrAnchorage;

            if (firstCalculationModel != null)
            {
                maskedDiverseCorrection.Value = firstCalculationModel.correction;
                maskedDiversePRovision.Value = firstCalculationModel.provision;
                maskedDiverseTravelInsurance.Value = firstCalculationModel.travelInsurace;
                if (txtInsProvision.Text != "NL")
                {
                    maskedCalamiteit.Value = Convert.ToDecimal("2,50");
                }
                else
                {
                    maskedCalamiteit.Value = Convert.ToDecimal("0,00");
                }

                numGroupMoney.Value = firstCalculationModel.moneyGroup;
                numFinalPrice.Text = firstCalculationModel.price.ToString();
                numVerzBeg.Value = firstCalculationModel.insuranceVolontary;
                numSurcharge.Value = firstCalculationModel.singleRoomPrice;
                numDiscount.Value = firstCalculationModel.discount;
                txt.Text = firstCalculationModel.txt;
                numtxtAmount.Value = firstCalculationModel.txtAmount;


                maskedPremie1.Value = firstCalculationModel.premie1;
                maskedPremie2.Value = firstCalculationModel.premie2;
                numCO.Value = firstCalculationModel.numberCO;
                numMinNumberTravelers.Value = firstCalculationModel.minNumberTravelers;
                maskedVolDays.Value = firstCalculationModel.volontaryDays;
                //reset check
                chkSport.CheckState = CheckState.Unchecked;
                if (firstCalculationModel.isSport == true)
                    chkSport.Checked = true;
                if (txtInsProvision.Text == "" && txtInsPremie.Text == "")
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("No fill Country Premie and Provision !!! Calculation not be well");
                }



                calculatePremies();


                dpDateFrom.Value = firstNotArticles.dtFromArrangement;
                dpDateTo.Value = firstNotArticles.dtToArrangement;
                DateTime dt1 = new DateTime(dpDateTo.Value.Year, dpDateTo.Value.Month, dpDateTo.Value.Day);
                DateTime dt2 = new DateTime(dpDateFrom.Value.Year, dpDateFrom.Value.Month, dpDateFrom.Value.Day);
                int days = Convert.ToInt32((dt1 - dt2).TotalDays) + 1;
                if (maskedDayOfArr.Text != days.ToString())
                    maskedDayOfArr.Text = days.ToString();
                numNrOfNights.Value = firstNotArticles.nrOfNights;
                lblNum_numberoftraveldays.Text = days.ToString();
                //

                LoadAccomodation();
                loadArrangementsPrice();
                LoadExtras();
                if (Int32.Parse(maskedVolDays.Text) != days && Int32.Parse(maskedVolDays.Text) != 0)
                {
                    numDays.Value = Decimal.Parse(maskedVolDays.Text);
                }
                else
                    numDays.Value = Decimal.Parse(days.ToString());

                LoadAccompaniment();
                LoadDiverse();
                FIllTotalExtra();
                loadPrice();
                lblNum_numberoftravelers.Text = maskedNrTravelers.Value.ToString();
                lblNum_minimumnumberoftravelers.Text = maskedMinNrTravelers.Value.ToString();
                lblNum_numberofvoluntaryhelpers.Text = maskedNrVoluntary.Value.ToString();
                loadTransport();


                return true;
            }
            else
            {
                return false;
            }


        }
        private void DisableTabNumericFields(Control control)
        {
            foreach (Control x in control.Controls)
            {
                if (x is RadMaskedEditBox)
                {
                    ((RadMaskedEditBox)x).Enabled = false;
                }
            }
        }
        private void EnableTabNumericFields(Control control)
        {
            foreach (Control x in control.Controls)
            {
                if (x is RadMaskedEditBox)
                {
                    RadMaskedEditBox rme = (RadMaskedEditBox)x;
                    if (control.Name == "tabTotal")
                    {
                        if (rme.Name == "numGroupMoney")
                            ((RadMaskedEditBox)x).Enabled = true;
                        else
                            ((RadMaskedEditBox)x).Enabled = false;
                    }
                    else if (control.Name == "tabAccompaniment")
                    {
                        if (rme.Name == "numSurcharge" || rme.Name == "numDiscount")
                            ((RadMaskedEditBox)x).Enabled = true;
                        else
                            ((RadMaskedEditBox)x).Enabled = false;
                    }
                    else
                        ((RadMaskedEditBox)x).Enabled = true;
                }
            }
        }

        private void EnableCheckBoxFields(Control control)
        {
            foreach (Control x in control.Controls)
            {
                if (x is RadCheckBox)
                {
                    ((RadCheckBox)x).Enabled = true;
                }
            }
        }

        private void DisableCheckBoxFields(Control control)
        {
            foreach (Control x in control.Controls)
            {
                if (x is RadCheckBox)
                {
                    ((RadCheckBox)x).Enabled = false;
                }
            }
        }

        private void LoadCalculationRecalculation(bool onpageload)
        {
            //onpageload = true znaci u slucaju pageload ucitava samo ovaj deo.
           
            if (onpageload == true)
            {
                if (isFinished == true && chkRecalculation.Checked == false)
                {
                    DisableCheckBoxFields(panelLabels);
                    maskedNrTravelers.Enabled = false;
                    maskedNrVoluntary.Enabled = false;
                    rbCountry.Enabled = false;

                    maskedMinNrTravelers.Enabled = false;
                    dpDateFrom.Enabled = false;
                    maskedDayOfArr.Enabled = false;
                    dpDateTo.Enabled = false;
                    numNrOfNights.Enabled = false;
                    btnSaveCalc.Enabled = false;
                    numNrVoluntary.Enabled = false;
                    lblUserFinished.Visible = true;
                    lblUserText.Visible = true;
                    lblDtUserFinished.Visible = true;
                    lblDtUserFinishedText.Visible = true;
                    
                }
                else if (isFinished == true && chkRecalculation.Checked == true)
                {
                    EnableCheckBoxFields(panelLabels);
                    maskedNrTravelers.Enabled = true;
                    maskedNrVoluntary.Enabled = true;
                    rbCountry.Enabled = true;

                    maskedMinNrTravelers.Enabled = true;
                    dpDateFrom.Enabled = true;
                    maskedDayOfArr.Enabled = true;
                    dpDateTo.Enabled = true;
                    numNrOfNights.Enabled = true;
                    btnSaveCalc.Enabled = false;
                    numNrVoluntary.Enabled = true;
                    lblUserFinished.Visible = true;
                    lblUserText.Visible = true;
                    lblDtUserFinished.Visible = true;
                    lblDtUserFinishedText.Visible = true;
                }
                if (isFinished == false)
                {
                    maskedNrTravelers.Enabled = true;
                    maskedNrVoluntary.Enabled = true;
                    rbCountry.Enabled = true;
                    btnSaveCalc.Enabled = true;
                    lblUserFinished.Visible = false;
                    lblUserText.Visible = false;
                    lblDtUserFinished.Visible = false;
                    lblDtUserFinishedText.Visible = false;
                }
            }
            else
            {
                if (isFinished == true && chkRecalculation.Checked == false)
                {
                    DisableTabNumericFields(tabTotal);
                    DisableTabNumericFields(tabAccompaniment);
                    DisableTabNumericFields(tabDiverseCustomer);
                    DisableTabNumericFields(tabExcursions);
                    DisableTabNumericFields(tabArrangementsPrice);
                    DisableTabNumericFields(tabAccomodation);
                    DisableCheckBoxFields(panelLabels);
                    maskedNrTravelers.Enabled = false;
                    maskedNrVoluntary.Enabled = false;
                    numMinNumberTravelers.Enabled = false;
                    maskedVolDays.Enabled = false;
                    chkSport.Enabled = false;
                    rbCountry.Enabled = false;
                    txt.Enabled = false;
                    btnNewItem.Enabled = false;
                    rgvTotalExtra.Enabled = false;
                    maskedMinNrTravelers.Enabled = false;
                    dpDateFrom.Enabled = false;
                    maskedDayOfArr.Enabled = false;
                    dpDateTo.Enabled = false;
                    numNrOfNights.Enabled = false;
                    btnSaveCalc.Enabled = false;
                    numNrVoluntary.Enabled = false;
                    lblUserFinished.Visible = true;
                    lblUserText.Visible = true;
                    lblDtUserFinished.Visible = true;
                    lblDtUserFinishedText.Visible = true;
                    if (isRecalculationLoaded == false)
                    {
                        saveRecalculationToModel();
                        isRecalculationLoaded = true;
                    }

                    bool b = LoadTabCalculationFromModel();

                    if (b == false)
                        LoadTabCalculation();

                }
                else if (isFinished == true && chkRecalculation.Checked == true)
                {
                    EnableTabNumericFields(tabTotal);
                    EnableTabNumericFields(tabAccompaniment);
                    EnableTabNumericFields(tabDiverseCustomer);
                    EnableTabNumericFields(tabExcursions);
                    EnableTabNumericFields(tabArrangementsPrice);
                    EnableTabNumericFields(tabAccomodation);
                    EnableCheckBoxFields(panelLabels);
                    maskedNrTravelers.Enabled = true;
                    maskedNrVoluntary.Enabled = true;
                    numMinNumberTravelers.Enabled = true;
                    maskedVolDays.Enabled = true;
                    chkSport.Enabled = true;
                    rbCountry.Enabled = true;
                    txt.Enabled = true;
                    btnNewItem.Enabled = true;
                    rgvTotalExtra.Enabled = true;
                    maskedMinNrTravelers.Enabled = true;
                    dpDateFrom.Enabled = true;
                    maskedDayOfArr.Enabled = true;
                    dpDateTo.Enabled = true;
                    numNrOfNights.Enabled = true;
                    btnSaveCalc.Enabled = false;
                    numNrVoluntary.Enabled = true;
                    lblUserFinished.Visible = true;
                    lblUserText.Visible = true;
                    lblDtUserFinished.Visible = true;
                    lblDtUserFinishedText.Visible = true;
                    bool b = LoadRecalculationFromModel();
                    isRecalculationLoaded = false;
                }

                if (isFinished == false)
                {
                    LoadTabCalculation();
                    EnableTabNumericFields(tabTotal);
                    EnableTabNumericFields(tabAccompaniment);
                    EnableTabNumericFields(tabDiverseCustomer);
                    EnableTabNumericFields(tabExcursions);
                    EnableTabNumericFields(tabArrangementsPrice);
                    EnableTabNumericFields(tabAccomodation);
                    maskedNrTravelers.Enabled = true;
                    maskedNrVoluntary.Enabled = true;
                    numMinNumberTravelers.Enabled = true;
                    maskedVolDays.Enabled = true;
                    rbCountry.Enabled = true;
                    chkSport.Enabled = true;
                    txt.Enabled = true;
                    btnNewItem.Enabled = true;
                    btnSaveCalc.Enabled = true;
                    rgvTotalExtra.Enabled = true;
                    lblUserFinished.Visible = false;
                    lblUserText.Visible = false;
                    lblDtUserFinished.Visible = false;
                    lblDtUserFinishedText.Visible = false;
                }
            }
            maskedDiversePRovision.Enabled = false;
        }
        private void radPageArrange_SelectedPageChanged(object sender, EventArgs e)
        {
            Boolean isFirst2 = false;
            if (isFinished == true && chkRecalculation.CheckState == CheckState.Unchecked)
                isFirst2 = true;
            RadPageView rpv = (RadPageView)sender;
            string sName = ((RadPageView)sender).SelectedPage.Name;
            //if (sName != "tabVoluntary")
            //{
            //    if (arrange.idArrangement != 0)
            //    {
            //        if (isVolChanged == true)
            //        {
            //        MedicalVoluntaryBUS vtb = new MedicalVoluntaryBUS();

            //        saveSkills();
                   
            //            if (vtb.DeleteVoluntaryArrangement(arrange.idArrangement) == true)
            //            {
            //                for (int j = 0; j < arrVoluntary3.Count; j++)
            //                {
            //                    if (vtb.SaveVoluntaryArrangement(arrVoluntary3[j]) == false)
            //                    {
            //                        translateRadMessageBox tr = new translateRadMessageBox();
            //                        tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
            //                    }

            //                }
            //            }
            //            else
            //            {
            //                translateRadMessageBox tr = new translateRadMessageBox();
            //                tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
            //            }
            //            isVolChanged = false;
            //        }
            //    }
            //}
            if (sName == "tabCalculation" && isFinished == true && chkRecalculation.CheckState == CheckState.Checked)
                saveRecalculationToModel();
            switch (sName)
            {
                case "tabArrangement":
                    ShowHideDocumentButtons(false);
                    ShowHideCommunicationButtons(false);
                    break;
                case "tabPurchase":
                    ShowHideDocumentButtons(false);
                    ShowHideCommunicationButtons(false);
                    loadPurchase(iID, isFirst2);
                    break;
                case "tabPurchaseCopy":
                    ShowHideDocumentButtons(false);
                    ShowHideCommunicationButtons(false);
                    loadPurchaseCopy(iID, isFirst2);
                    break;

                case "tabCalculation":
                    ShowHideDocumentButtons(false);
                    ShowHideCommunicationButtons(false);
                    LoadCalculationRecalculation(false);
                    FIllTotalExtra();
                    break;
                case "tabDocument":
                    ShowHideDocumentButtons(true);
                    ShowHideCommunicationButtons(false);
                    rgvDocuments.DataSource = new DocumentsBUS().GetArrangementDoc(arrange.idArrangement,Login._user.lngUser);
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

                    //saki end
                    rgvDocuments.Show();
                    break;
                case "tabCommunication":                    
                    ShowHideDocumentButtons(false);
                    ShowHideCommunicationButtons(true);
                    LoadMeetings();
                    LoadContacts();
                    LoadTasks();
                    //LoadCalculationRecalculation(false);
                    //FIllTotalExtra();
                    break;
                default:
                    break;
            }
        }

        private void radPageViewCalculation_SelectedPageChanged(object sender, EventArgs e)
        {
            RadPageView rpv = (RadPageView)sender;
            string sName = ((RadPageView)sender).SelectedPage.Name;
            switch (sName)
            {
                case "tabTransport":
                    loadTransport();
                    break;
                case "tabAccomodation":
                    LoadAccomodation();
                    break;
                case "tabArrangementsPrice":
                    loadArrangementsPrice();
                    break;
                case "tabExcursions":
                    LoadExtras();
                    break;
                case "tabAccompaniment":
                    //=== puni broj dana arranzmana za preracun premije osiguranja ===
                    VolInsurance();  // preracun osiguranja
                    numVerzBeg.Text = T2.ToString();
                    LoadAccompaniment();
                    break;
                case "tabDiverseCustomer":
                    LoadDiverse();
                    break;
                case "tabTotal":
                    FIllTotalExtra();
                    loadPrice();
                    break;
                default:
                    break;
            }
        }

        private void loadTransport()
        {
            ArrangementPriceBUS bus = new ArrangementPriceBUS();
            ArrangementPriceModel model = new ArrangementPriceModel();

            if (Convert.ToInt32(numMinNumberTravelers.Value) != 0)
                minNumber = Convert.ToInt32(numMinNumberTravelers.Value);
            else
                minNumber = Convert.ToInt32(maskedMinNrTravelers.Value);
            Boolean isFirst2 = false;
            if (isFinished == true && chkRecalculation.CheckState == CheckState.Unchecked)
                isFirst2 = true;
            try
            {
                if (minNumber > 0)
                {
                    List<IModel> lista = bus.GetAllArrangementPricesByArticleGroup(arrange.idArrangement, "Trans", minNumber, isFirst2);

                    if (lista == null)
                        lista = new List<IModel>();

                    rgvTransport.DataSource = lista;
                }

            }
            catch (Exception e)
            {
                RadMessageBox.Show(e.Message);
            }
        }

        private void loadArrangementsPrice()
        {
            ArrangementPriceBUS bus = new ArrangementPriceBUS();
            ArrangementPriceModel model = new ArrangementPriceModel();

            if (Convert.ToInt32(numMinNumberTravelers.Value) != 0)
                minNumber = Convert.ToInt32(numMinNumberTravelers.Value);
            else
                minNumber = Convert.ToInt32(maskedMinNrTravelers.Value);
            Boolean isFirst2 = false;
            if (isFinished == true && chkRecalculation.CheckState == CheckState.Unchecked)
                isFirst2 = true;
            try
            {
                if (minNumber > 0)
                {
                    List<IModel> lista = bus.GetAllArrangementPricesByArticleGroup(arrange.idArrangement, "TotalReis", minNumber, isFirst2);

                    if (lista == null)
                        lista = new List<IModel>();

                    rgvArrangementPrice.DataSource = lista;
                }
            }
            catch (Exception e)
            {
                RadMessageBox.Show(e.Message);
            }
        }

        private void rgvPurchase_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (isFinished == true && chkRecalculation.CheckState == CheckState.Unchecked)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You can't make any changes in first calculation!");
            }
            else
            {
                Boolean isFirst2 = false;
                if (isFinished == true && chkRecalculation.CheckState == CheckState.Unchecked)
                    isFirst2 = true;
                GridViewRowInfo info = this.rgvPurchase.CurrentRow;
                if (info != null && e.RowIndex >= 0)
                {
                    if (rgvPurchase.Rows.Count > 0)
                    {
                        if (e.Row.DataBoundItem != null)
                        {
                            ArrangementPriceModel ap = (ArrangementPriceModel)e.Row.DataBoundItem;
                            
                            if (ap.idContract <= 0)
                            {
                                using (frmArrangementPurchase frm = new frmArrangementPurchase(ap))
                                {
                                    frm.ShowDialog();
                                    rgvPurchase.DataSource = null;
                                    loadPurchase(iID, isFirst2);

                                }
                            }
                            else
                            {
                                PriceListModel pm = new PriceListBUS().GetPriceList(ap.idContract);
                                using (frmPriceList frm = new frmPriceList(pm, pm.idClient))
                                {
                                    frm.ShowDialog();
                                    rgvPurchase.DataSource = null;
                                    loadPurchase(iID, isFirst2);
                                }
                            }                            
                        }

                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();

                    }
                }
            }
        }
        private void radMenuItemSavePurchase_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutPurchase))
            {
                File.Delete(layoutPurchase);
            }
            rgvPurchase.SaveLayout(layoutPurchase);

            RadMessageBox.Show("Layout saved");
        }

        private void rgvPurchase_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
           
            for (int i = 0; i < rgvPurchase.Columns.Count; i++)
                   {
                       using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                       {
                           if (rgvPurchase.Columns[i].HeaderText != null && resxSet.GetString(rgvPurchase.Columns[i].HeaderText) != null)
                               rgvPurchase.Columns[i].HeaderText = resxSet.GetString(rgvPurchase.Columns[i].HeaderText);
                       }
                       if (rgvPurchase.Columns[i].GetType() == typeof(GridViewDateTimeColumn))
                       {
                           if (rgvPurchase.Columns[i].Name.ToLower() != "dtUserModified".ToLower() && rgvPurchase.Columns[i].Name.ToLower() != "dtUserCreated".ToLower())
                           {
                               rgvPurchase.Columns[i].FormatString = "{0: dd-MM-yyyy}";
                           }
                       }
                        
                   }
                   
                    if (File.Exists(layoutPurchase))
                    {
                        rgvPurchase.LoadLayout(layoutPurchase);
                    }
                    else
                    {

                        this.rgvPurchase.SummaryRowsTop.Clear();
                        rgvPurchase.MasterTemplate.EnablePaging = false;
                        rgvPurchase.MasterTemplate.ShowTotals = true;
                        GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                        summaryItem.Name = rgvPurchase.Columns["priceTotal"].Name;
                        summaryItem.Aggregate = GridAggregateFunction.Sum;

                        GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                        summaryRowItem.Add(summaryItem);
                        this.rgvPurchase.SummaryRowsTop.Add(summaryRowItem);

                        rgvPurchase.Columns["idArrangementPrice"].IsVisible = false;
                        rgvPurchase.Columns["idArrangement"].IsVisible = false;
                        rgvPurchase.Columns["idClient"].IsVisible = false;
                        rgvPurchase.Columns["idUserCreated"].IsVisible = false;
                        rgvPurchase.Columns["dtUserCreated"].IsVisible = false;
                        rgvPurchase.Columns["idUserModified"].IsVisible = false;
                        rgvPurchase.Columns["dtUserModified"].IsVisible = false;
                    }

                    rgvPurchase.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
                    rgvPurchase.Show();
                }
                 
        private void rgvPurchase_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is Telerik.WinControls.UI.GridSummaryCellElement)
            {

                e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
            }
        }

        private void frmArrangement_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                radRibbonBarGroupPurchase.Visibility = ElementVisibility.Visible;
                btnPurchase.Visibility = ElementVisibility.Visible;
                btnDelPurchase.Visibility = ElementVisibility.Visible;
            }
            catch (Exception ex)
            {

            }
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


        private void radMenuItemArrangementTransport_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutArrangementTransport))
            {
                File.Delete(layoutArrangementTransport);
            }
            rgvTransport.SaveLayout(layoutArrangementTransport);
            translateRadMessageBox tr = new translateRadMessageBox();
            tr.translateAllMessageBox("Layout saved");
        }

        private void rgvTransport_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvTransport.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvTransport.Columns[i].HeaderText != null && resxSet.GetString(rgvTransport.Columns[i].HeaderText) != null)
                        rgvTransport.Columns[i].HeaderText = resxSet.GetString(rgvTransport.Columns[i].HeaderText);
                }
            }

            if (rgvTransport.Columns.Count > 0)
            {

                if (File.Exists(layoutArrangementTransport))
                {
                    rgvTransport.LoadLayout(layoutArrangementTransport);
                }
                else
                {
                    rgvTransport.Columns["idArrangementPrice"].IsVisible = false;
                    rgvTransport.Columns["idArrangement"].IsVisible = false;
                    rgvTransport.Columns["idClient"].IsVisible = false;
                    rgvTransport.Columns["idUserCreated"].IsVisible = false;
                    rgvTransport.Columns["dtUserCreated"].IsVisible = false;
                    rgvTransport.Columns["idUserModified"].IsVisible = false;
                    rgvTransport.Columns["dtUserModified"].IsVisible = false;
                    this.rgvTransport.SummaryRowsTop.Clear();
                    rgvTransport.MasterTemplate.EnablePaging = false;
                    rgvTransport.MasterTemplate.ShowTotals = true;

                    string expression = "Sum(IIf(isNotForTraveler=True,0,total))";
                    GridViewSummaryItem summaryItem = new GridViewSummaryItem("total", "{0}", expression);
                    summaryItem.FormatString = "{0:N2}";
                    this.rgvTransport.SummaryRowsTop.Add(new GridViewSummaryRowItem(new GridViewSummaryItem[] { summaryItem }));

                }



            }

            rgvTransport.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            rgvTransport.Show();
        }



        private void radMenuItemArrangementPrice_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutArrangementPrice))
            {
                File.Delete(layoutArrangementPrice);
            }
            rgvArrangementPrice.SaveLayout(layoutArrangementPrice);
            translateRadMessageBox tr = new translateRadMessageBox();
            tr.translateAllMessageBox("Layout saved");
        }

        private void rgvArrangementPrice_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if (rgvArrangementPrice != null)
            {
                for (int i = 0; i < rgvArrangementPrice.Columns.Count; i++)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (rgvArrangementPrice.Columns[i].HeaderText != null && resxSet.GetString(rgvArrangementPrice.Columns[i].HeaderText) != null)
                            rgvArrangementPrice.Columns[i].HeaderText = resxSet.GetString(rgvArrangementPrice.Columns[i].HeaderText);
                    }
                }

                if (rgvArrangementPrice.Columns.Count > 0)
                {
                    if (File.Exists(layoutArrangementPrice))
                    {
                        rgvTransport.LoadLayout(layoutArrangementPrice);
                    }
                    else
                    {
                        rgvArrangementPrice.Columns["idArrangementPrice"].IsVisible = false;
                        rgvArrangementPrice.Columns["idArrangement"].IsVisible = false;
                        rgvArrangementPrice.Columns["idClient"].IsVisible = false;
                        rgvArrangementPrice.Columns["idUserCreated"].IsVisible = false;
                        rgvArrangementPrice.Columns["dtUserCreated"].IsVisible = false;
                        rgvArrangementPrice.Columns["idUserModified"].IsVisible = false;
                        rgvArrangementPrice.Columns["dtUserModified"].IsVisible = false;



                    }

                    if (this.rgvArrangementPrice.SummaryRowsTop != null)
                        this.rgvArrangementPrice.SummaryRowsTop.Clear();
                    rgvArrangementPrice.MasterTemplate.EnablePaging = false;
                    rgvArrangementPrice.MasterTemplate.ShowTotals = true;
                    //string Commission = numCommission.Value.ToString();
                    //string Tax = numTax.Value.ToString();
                    string expression = "Sum(IIf(isNotForTraveler=True,0,total))";
                    NumberFormatInfo nfi = new NumberFormatInfo { NumberGroupSeparator = "", NumberDecimalDigits = 2, NumberDecimalSeparator = "." };
                    // if (Convert.ToDecimal(Commission) != 0)
                    //     expression = "Sum(total)-((Sum(total) - " + Convert.ToDecimal(Tax).ToString("n", nfi) + ")*" + Convert.ToDecimal(Commission).ToString("n", nfi) + "/100) "; // +Convert.ToDecimal(Tax).ToString("n", nfi) + "";
                    GridViewSummaryItem summaryItem = new GridViewSummaryItem("total", "{0}", expression);
                    summaryItem.FormatString = "{0:N2}";
                    this.rgvArrangementPrice.SummaryRowsTop.Add(new GridViewSummaryRowItem(new GridViewSummaryItem[] { summaryItem }));
                }

            }

            rgvArrangementPrice.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            rgvArrangementPrice.Show();
        }


        private void maskedCalamiteit_ValueChanged(object sender, EventArgs e)
        {
            LoadAccompaniment();
        }

        private void LoadAccompaniment()
        {
            try
            {
                decimal totalAccomod = 0;
                decimal totalExtra = 0;
                decimal totalReis = 0;
                decimal totalTrans = 0;

                if (Convert.ToInt32(lblNum_minimumnumberoftravelers.Text) != 0)
                    minNumber1 = Convert.ToInt32(lblNum_minimumnumberoftravelers.Text);
                else
                    minNumber1 = 1;

                Boolean isFirst2 = false;
                if (isFinished == true && chkRecalculation.CheckState == CheckState.Unchecked)
                    isFirst2 = true;
                ArrangementPriceBUS bus = new ArrangementPriceBUS();
                ArrangementPriceModel model = new ArrangementPriceModel();
                List<IModel> lista = new List<IModel>();
                //====  NoGroup

                lista = bus.GetAllArrangementPricesByArticleNoGroup(arrange.idArrangement, "Accomod", minNumber1, isFirst2);  // novi upit koji izuzima grupu

                // aded condition if(price.isNotInAccompaniment==false) for all articles group
                if (lista != null)
                {
                    foreach (IModel m in lista)
                    {

                        ArrangementPriceModel price = (ArrangementPriceModel)m;
                        if (price.isNotInAccompaniment == false)
                            totalAccomod += price.total;
                    }
                }

                lista = bus.GetAllArrangementPricesByArticleNoGroup(arrange.idArrangement, "Extra", minNumber1, isFirst2); // novi upit koji izuzima grupu ubaceno 2-11-2015
                if (lista != null)
                {
                    foreach (IModel m in lista)
                    {
                        ArrangementPriceModel price = (ArrangementPriceModel)m;
                        if (price.isNotInAccompaniment == false)
                            totalExtra += price.priceTotal;
                    }
                }

                lista = bus.GetAllArrangementPricesByArticleNoGroup(arrange.idArrangement, "Trans", minNumber1, isFirst2); // novi upit koji izuzima grupu
                if (lista != null)
                {
                    foreach (IModel m in lista)
                    {
                        ArrangementPriceModel price = (ArrangementPriceModel)m;
                        if (price.isNotInAccompaniment == false)
                            totalTrans += price.total;
                    }
                }
                lista = bus.GetAllArrangementPricesByArticleGroup(arrange.idArrangement, "TotalReis", minNumber1, isFirst2);
                if (lista != null)
                {
                    foreach (IModel m in lista)
                    {
                        ArrangementPriceModel price = (ArrangementPriceModel)m;
                        if (price.isNotInAccompaniment == false)
                            totalReis += price.priceTotal;
                    }
                    // if (lista.Count > 0)
                    // totalReis = totalReis - ((totalReis - Convert.ToDecimal(numTax.Value)) * Convert.ToDecimal(numCommission.Value) / 100); //+ Convert.ToDecimal(numTax.Value);
                }



                //=========
                lblAccompAccomodation_Value.Text = totalAccomod.ToString("#.##");
                lblAccompExtras_Value.Text = totalExtra.ToString("#.##");
                lblAccompTrasnport_Value.Text = totalTrans.ToString("#.##");
                lblAccompArrangementPrice_Value.Text = totalReis.ToString("#.##");

                decimal total = totalAccomod + totalExtra + totalTrans + totalReis + Convert.ToDecimal(maskedCalamiteit.Value) +
                   Convert.ToDecimal(numSurcharge.Value) + Convert.ToDecimal(numVerzBeg.Value) - Convert.ToDecimal(numDiscount.Value) +
                    //Convert.ToDecimal(numAccomSerChargVol.Value) + Convert.ToDecimal(numExtrasVol.Value) + totalGridExtra; // +Convert.ToDecimal(numVerzBeg.Value);
                    +totalGridExtra;
                lblAcompTotal_Value.Text = total.ToString("#.##");

                decimal subtotal = 0;
                if (minNumber1 > 0)
                {
                    if (Convert.ToDecimal(numNrVoluntary.Value) != 0)
                        subtotal = total * Convert.ToDecimal(numNrVoluntary.Value) / minNumber1;
                    else
                        subtotal = total * Convert.ToDecimal(maskedNrVoluntary.Value) / minNumber1;
                }
                test = subtotal;
                if (subtotal != 0)
                {
                    lblAcoompSubtotal_Value.Text = subtotal.ToString("#.##");
                }
                else
                {
                    lblAcoompSubtotal_Value.Text = subtotal.ToString();
                }


            }
            catch (Exception e)
            {
                RadMessageBox.Show(e.Message);
            }
        }


        private void loadPrice()
        {

            try
            {
                decimal totalAccomod = 0;
                decimal totalExtra = 0;
                decimal totalReis = 0;
                decimal totalTrans = 0;
                decimal total = 0;


                if (Convert.ToInt32(numMinNumberTravelers.Value) != 0)
                    minNumber = Convert.ToInt32(numMinNumberTravelers.Value);
                else
                    minNumber = Convert.ToInt32(maskedMinNrTravelers.Value);

                ArrangementPriceBUS bus = new ArrangementPriceBUS();
                ArrangementPriceModel model = new ArrangementPriceModel();
                List<IModel> lista = new List<IModel>();

                Boolean isFirst2 = false;
                if (isFinished == true && chkRecalculation.CheckState == CheckState.Unchecked)
                    isFirst2 = true;
                lista = bus.GetAllArrangementPricesByArticleGroup(arrange.idArrangement, "Accomod", minNumber, isFirst2);

                if (lista != null)
                {
                    foreach (IModel m in lista)
                    {
                        ArrangementPriceModel price = (ArrangementPriceModel)m;
                        if (price.isNotForTraveler == false)
                            totalAccomod += price.total;
                    }

                }
                lista = bus.GetAllArrangementPricesByArticleGroup(arrange.idArrangement, "Extra", minNumber, isFirst2);      //GetAllArrangementPricesByArticleGroup izbaceno 18-12
                if (lista != null)
                {
                    foreach (ArrangementPriceModel m in lista)
                    {
                        if (m.isGroup == true)
                        {
                            if (m.isNotForTraveler == false)
                                totalExtra += m.priceTotal / minNumber;
                        }
                        else
                        {
                            if (m.isNotForTraveler == false)
                                totalExtra += m.priceTotal;
                        }
                    }
                }
                lista = bus.GetAllArrangementPricesByArticleGroup(arrange.idArrangement, "Trans", minNumber, isFirst2);
                if (lista != null)
                {
                    foreach (IModel m in lista)
                    {
                        ArrangementPriceModel price = (ArrangementPriceModel)m;
                        if (price.isNotForTraveler == false)
                            totalTrans += price.total;
                    }
                }
                lista = bus.GetAllArrangementPricesByArticleGroup(arrange.idArrangement, "TotalReis", minNumber, isFirst2);
                if (lista != null)
                {
                    foreach (IModel m in lista)
                    {
                        ArrangementPriceModel price = (ArrangementPriceModel)m;
                        if (price.isNotForTraveler == false)
                            totalReis += price.priceTotal;
                    }
                    //if (lista.Count > 0)
                    //totalReis = totalReis - ((totalReis - Convert.ToDecimal(numTax.Value)) * Convert.ToDecimal(numCommission.Value)) / 100;  // + Convert.ToDecimal(numTax.Value);

                }

                //    //==== prebaceno osiguranje 
                decimal travelInsuranceVol = Decimal.Parse(lblDiverseTravelInsurance_Value1.Text) * Convert.ToDecimal(maskedDiverseTravelInsurance.Value);

                lblDiverseTravelInsurance_Value2.Text = travelInsuranceVol.ToString();
                //    //
                //// == dodat u total osiguranje sa predhodnog taba
                decimal subTotal = Math.Ceiling(totalAccomod + totalExtra + totalTrans + totalReis + Decimal.Parse(lblAcoompSubtotal_Value.Text) +
                    Decimal.Parse(lblDivereSubtotal_Value.Text));


                lblNumSubtotal.Text = subTotal.ToString();
                total = Math.Ceiling(totalAccomod + totalExtra + totalTrans + totalReis + Decimal.Parse(lblAcoompSubtotal_Value.Text) +
                    Decimal.Parse(lblDivereSubtotal_Value.Text)) + Decimal.Parse(lblDiverseTravelInsurance_Value2.Text) + totalTotalGrid; //dodat totalgrid za extra artikle

                numPrice.Text = total.ToString();

                numFinalPrice.Text = (Math.Ceiling(Decimal.Parse(numPrice.Text))).ToString();

                //numTravelInsuranceDiverse i numPolisCosts1 promenjeno na 5 decimala i sakriveno
                //numTravelInsuranceDiverse.Text = (Math.Round((Decimal.Parse(numFinalPrice.Text) - Decimal.Parse(lblDiverseTravelInsurance_Value2.Text) - Convert.ToDecimal(maskedCalamiteit.Value)) / 100, 5)).ToString();
                numTravelInsuranceDiverse.Text = (Math.Round((Decimal.Parse(lblNumSubtotal.Text)) / 100, 5)).ToString();
                numTravelInsuranceValue.Text = (Math.Round(Decimal.Parse(numTravelInsuranceDiverse.Text) * Decimal.Parse(numTravelInsurance.Text), 2)).ToString();



                numPolisCosts1.Text = (Math.Round(Decimal.Parse(numTravelInsuranceValue.Text) / 100, 5)).ToString();
                numPoliseCostsValue.Text = (Math.Round(Decimal.Parse(numPolisCosts.Text) * Decimal.Parse(numPolisCosts1.Text), 2)).ToString();

                numTravelTotalInsuranceValue.Text = (Math.Round(Decimal.Parse(numTravelInsuranceValue.Text) + Decimal.Parse(numPoliseCostsValue.Text), 2)).ToString();
            }

            catch (Exception e)
            {
                RadMessageBox.Show(e.Message);
            }
        }

        private void LoadAccomodation()
        {
            ArrangementPriceBUS bus = new ArrangementPriceBUS();
            ArrangementPriceModel model = new ArrangementPriceModel();

            if (Convert.ToInt32(numMinNumberTravelers.Value) != 0)
                minNumber = Convert.ToInt32(numMinNumberTravelers.Value);
            else
                minNumber = Convert.ToInt32(maskedMinNrTravelers.Value);

            Boolean isFirst2 = false;
            if (isFinished == true && chkRecalculation.CheckState == CheckState.Unchecked)
                isFirst2 = true;
            try
            {
                if (minNumber > 0)
                {
                    List<IModel> lista = bus.GetAllArrangementPricesByArticleGroup(arrange.idArrangement, "Accomod", minNumber, isFirst2);

                    if (lista == null)
                        lista = new List<IModel>();

                    rgvAccomodation.DataSource = lista;
                }
            }
            catch (Exception e)
            {
                RadMessageBox.Show(e.Message);
            }

        }




        private void radCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            RadCheckBox chk = (RadCheckBox)sender;
            if (chk.CheckState == CheckState.Checked)
            {
                if (arrangeBoardingPoint.Find(s => s.idBoardingPoint == Convert.ToInt32(chk.Name)) == null)
                {
                    ArrangementBoardingPointModel abpm = new ArrangementBoardingPointModel();
                    abpm.idBoardingPoint = Convert.ToInt32(chk.Name);
                    abpm.idArrangement = arrange.idArrangement;
                    arrangeBoardingPoint.Add(abpm);
                }
            }
            else
            {
                arrangeBoardingPoint.Remove(arrangeBoardingPoint.Find(s => s.idBoardingPoint == Convert.ToInt32(chk.Name)));
            }
        }



        // arrangement target group
        private void LoadTargetGroup()
        {
            getCheckedTargetGroup();

        }
        private void getCheckedTargetGroup()
        {
            panelTargetGroup.Controls.Clear();
            int lastBottom = 15;

            if (arrangeTargetGroup.Count == 0)
            {
                ArrangementTargetGroupBUS abpb = new ArrangementTargetGroupBUS();
                List<TargetGroupModel> arrTargetGroup = new List<TargetGroupModel>();
                arrTargetGroup = abpb.GetArrangementTargetGroup(arrange.idArrangement);


                if (arrTargetGroup != null)
                    if (arrTargetGroup.Count > 0)
                        for (int i = 0; i < arrTargetGroup.Count; i++)
                        {
                            ArrangementTargetGroupModel abpm = new ArrangementTargetGroupModel();
                            abpm.idTargetGroup = arrTargetGroup[i].idTargetGroup;
                            abpm.idArrangement = arrange.idArrangement;
                            arrangeTargetGroup.Add(abpm);

                            RadCheckBox chk = new RadCheckBox();
                            chk.Font = new Font("Verdana", 9);
                            chk.CheckState = CheckState.Checked;
                            chk.CheckStateChanged += radCheckBoxTG_CheckStateChanged;
                            chk.Name = arrTargetGroup[i].idTargetGroup.ToString();
                            chk.Text = arrTargetGroup[i].nameTargetGroup.ToString() + "(" + arrTargetGroup[i].shortcutTargeGroup.Trim() + ")";
                            chk.Location = new Point(15, lastBottom);
                            lastBottom = lastBottom + 20;
                            panelTargetGroup.Controls.Add(chk);
                        }
            }
            else
            {
                if (arrangeTargetGroup.Count > 0)
                    for (int i = 0; i < arrangeTargetGroup.Count; i++)
                    {

                        RadCheckBox chk = new RadCheckBox();
                        chk.Font = new Font("Verdana", 9);
                        chk.CheckState = CheckState.Checked;
                        chk.CheckStateChanged += radCheckBoxTG_CheckStateChanged;
                        chk.Name = arrangeTargetGroup[i].idTargetGroup.ToString();
                        chk.Text = new ArrangementTargetGroupBUS().GetTargetGroupName(arrangeTargetGroup[i].idTargetGroup);
                        chk.Location = new Point(15, lastBottom);
                        lastBottom = lastBottom + 20;
                        panelTargetGroup.Controls.Add(chk);
                    }
            }
        }


        private void getAllTargetGroup()
        {
            if (btnTgAll.CheckState == CheckState.Checked)
            {
                panelTargetGroup.Controls.Clear();
                ArrangementTargetGroupBUS abpb = new ArrangementTargetGroupBUS();
                List<TargetGroupModel> arrTargetGroup = new List<TargetGroupModel>();
                arrTargetGroup = abpb.GetAllTargetGroup();

                int lastBottom = 15;
                if (arrTargetGroup != null)
                    if (arrTargetGroup.Count > 0)
                        for (int i = 0; i < arrTargetGroup.Count; i++)
                        {
                            RadCheckBox chk = new RadCheckBox();
                            chk.Font = new Font("Verdana", 9);
                            chk.CheckStateChanged += radCheckBoxTG_CheckStateChanged;
                            chk.Name = arrTargetGroup[i].idTargetGroup.ToString();
                            chk.Text = arrTargetGroup[i].nameTargetGroup.ToString() + "(" + arrTargetGroup[i].shortcutTargeGroup.Trim() + ")";
                            chk.Location = new Point(15, lastBottom);
                            if (arrangeTargetGroup.Find(s => s.idTargetGroup == Convert.ToInt32(chk.Name)) != null)
                            {
                                chk.CheckState = CheckState.Checked;
                            }
                            lastBottom = lastBottom + 20;
                            panelTargetGroup.Controls.Add(chk);
                        }
            }
            else
            {
                getCheckedTargetGroup();
            }
        }


        private void radCheckBoxTG_CheckStateChanged(object sender, EventArgs e)
        {
            RadCheckBox chk = (RadCheckBox)sender;
            if (chk.CheckState == CheckState.Checked)
            {
                if (arrangeTargetGroup.Find(s => s.idTargetGroup == Convert.ToInt32(chk.Name)) == null)
                {
                    ArrangementTargetGroupModel abpm = new ArrangementTargetGroupModel();
                    abpm.idTargetGroup = Convert.ToInt32(chk.Name);
                    abpm.idArrangement = arrange.idArrangement;
                    arrangeTargetGroup.Add(abpm);
                }
            }
            else
            {
                arrangeTargetGroup.Remove(arrangeTargetGroup.Find(s => s.idTargetGroup == Convert.ToInt32(chk.Name)));
            }
        }

        private void LoadThemeTrip()
        {
            getCheckedThemeTrip();

        }

        private void getCheckedThemeTrip()
        {
            panelThemeTrip.Controls.Clear();
            int lastBottom = 15;

            if (arrangeThemeTrip.Count == 0)
            {
                ArrangementThemeTripBUS abpb = new ArrangementThemeTripBUS();
                List<ThemeTripModel> arrThemeTrip = new List<ThemeTripModel>();
                arrThemeTrip = abpb.GetArrangementThemeTrip(arrange.idArrangement);


                if (arrThemeTrip != null)
                    if (arrThemeTrip.Count > 0)
                        for (int i = 0; i < arrThemeTrip.Count; i++)
                        {
                            ArrangementThemeTripModel abpm = new ArrangementThemeTripModel();
                            abpm.idThemeTrip = arrThemeTrip[i].idThemeTrip;
                            abpm.idArrangement = arrange.idArrangement;
                            arrangeThemeTrip.Add(abpm);
                            RadCheckBox chk = new RadCheckBox();
                            chk.Font = new Font("Verdana", 9);
                            chk.CheckState = CheckState.Checked;
                            chk.CheckStateChanged += radCheckBoxTT_CheckStateChanged;
                            chk.Name = arrThemeTrip[i].idThemeTrip.ToString();
                            chk.Text = arrThemeTrip[i].nameThemeTrip.ToString();
                            chk.Location = new Point(15, lastBottom);
                            lastBottom = lastBottom + 20;
                            panelThemeTrip.Controls.Add(chk);
                        }
            }
            else
            {
                if (arrangeThemeTrip.Count > 0)
                    for (int i = 0; i < arrangeThemeTrip.Count; i++)
                    {

                        RadCheckBox chk = new RadCheckBox();
                        chk.Font = new Font("Verdana", 9);
                        chk.CheckState = CheckState.Checked;
                        chk.CheckStateChanged += radCheckBoxTT_CheckStateChanged;
                        chk.Name = arrangeThemeTrip[i].idThemeTrip.ToString();
                        chk.Text = new ArrangementThemeTripBUS().GetThemeTripName(arrangeThemeTrip[i].idThemeTrip);
                        chk.Location = new Point(15, lastBottom);
                        lastBottom = lastBottom + 20;
                        panelThemeTrip.Controls.Add(chk);
                    }
            }
        }

        private void btnAll_CheckStateChanged(object sender, EventArgs e)
        {
            getAllThemeTrips();
        }

        private void btnAllTG_CheckStateChanged(object sender, EventArgs e)
        {
            getAllTargetGroup();
        }
        private void getAllThemeTrips()
        {
            if (btnAll.CheckState == CheckState.Checked)
            {
                panelThemeTrip.Controls.Clear();
                ArrangementThemeTripBUS abpb = new ArrangementThemeTripBUS();
                List<ThemeTripModel> arrThemeTrip = new List<ThemeTripModel>();
                arrThemeTrip = abpb.GetAllThemeTrip();

                int lastBottom = 15;
                if (arrThemeTrip != null)
                    if (arrThemeTrip.Count > 0)
                        for (int i = 0; i < arrThemeTrip.Count; i++)
                        {
                            RadCheckBox chk = new RadCheckBox();
                            chk.Font = new Font("Verdana", 9);
                            chk.CheckStateChanged += radCheckBoxTT_CheckStateChanged;
                            chk.Name = arrThemeTrip[i].idThemeTrip.ToString();
                            chk.Text = arrThemeTrip[i].nameThemeTrip.ToString();
                            chk.Location = new Point(15, lastBottom);
                            if (arrangeThemeTrip.Find(s => s.idThemeTrip == Convert.ToInt32(chk.Name)) != null)
                            {
                                chk.CheckState = CheckState.Checked;
                            }
                            lastBottom = lastBottom + 20;
                            panelThemeTrip.Controls.Add(chk);
                        }
            }
            else
            {
                getCheckedThemeTrip();
            }
        }

        private void radCheckBoxTT_CheckStateChanged(object sender, EventArgs e)
        {
            RadCheckBox chk = (RadCheckBox)sender;
            if (chk.CheckState == CheckState.Checked)
            {
                if (arrangeThemeTrip.Find(s => s.idThemeTrip == Convert.ToInt32(chk.Name)) == null)
                {
                    ArrangementThemeTripModel abpm = new ArrangementThemeTripModel();
                    abpm.idThemeTrip = Convert.ToInt32(chk.Name);
                    abpm.idArrangement = arrange.idArrangement;
                    arrangeThemeTrip.Add(abpm);
                }
            }
            else
            {
                arrangeThemeTrip.Remove(arrangeThemeTrip.Find(s => s.idThemeTrip == Convert.ToInt32(chk.Name)));
            }
        }

        private void LoadExtras()
        {
            ArrangementPriceBUS bus = new ArrangementPriceBUS();
            ArrangementPriceModel model = new ArrangementPriceModel();

            if (Convert.ToInt32(numMinNumberTravelers.Value) != 0)
                minNumber = Convert.ToInt32(numMinNumberTravelers.Value);
            else
                minNumber = Convert.ToInt32(maskedMinNrTravelers.Value);
            Boolean isFirst2 = false;
            if (isFinished == true && chkRecalculation.CheckState == CheckState.Unchecked)
                isFirst2 = true;
            try
            {
                if (minNumber > 0)
                {
                    List<IModel> lista = bus.GetAllArrangementPricesByArticleGroup(arrange.idArrangement, "Extra", minNumber, isFirst2);

                    if (lista == null)
                        lista = new List<IModel>();

                    rgvExtras.DataSource = lista;
                }
            }
            catch (Exception e)
            {
                RadMessageBox.Show(e.Message);
            }

        }

        private void rgvAccomodation_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvAccomodation.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvAccomodation.Columns[i].HeaderText != null && resxSet.GetString(rgvAccomodation.Columns[i].HeaderText) != null)
                        rgvAccomodation.Columns[i].HeaderText = resxSet.GetString(rgvAccomodation.Columns[i].HeaderText);
                }
            }

            if (File.Exists(layoutArrangementAccomodation))
            {
                rgvAccomodation.LoadLayout(layoutArrangementAccomodation);
            }
            else
            {

                this.rgvAccomodation.SummaryRowsTop.Clear();
                rgvAccomodation.MasterTemplate.EnablePaging = false;
                rgvAccomodation.MasterTemplate.ShowTotals = true;

                if (rgvAccomodation.Columns.Count > 0)
                {

                    rgvAccomodation.Columns["idArrangementPrice"].IsVisible = false;
                    rgvAccomodation.Columns["idArrangement"].IsVisible = false;
                    rgvAccomodation.Columns["idClient"].IsVisible = false;
                    rgvAccomodation.Columns["idUserCreated"].IsVisible = false;
                    rgvAccomodation.Columns["dtUserCreated"].IsVisible = false;
                    rgvAccomodation.Columns["idUserModified"].IsVisible = false;
                    rgvAccomodation.Columns["dtUserModified"].IsVisible = false;


                    string expression = "Sum(IIf(isNotForTraveler=True,0,total))";
                    GridViewSummaryItem summaryItem = new GridViewSummaryItem("total", "{0}", expression);
                    summaryItem.FormatString = "{0:N2}";
                    this.rgvAccomodation.SummaryRowsTop.Add(new GridViewSummaryRowItem(new GridViewSummaryItem[] { summaryItem }));
                }

            }

            rgvAccomodation.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            rgvAccomodation.Show();

        }

        private void radMenuItemAccomodSaveLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutArrangementAccomodation))
            {
                File.Delete(layoutArrangementAccomodation);
            }
            rgvAccomodation.SaveLayout(layoutArrangementAccomodation);

            RadMessageBox.Show("Layout saved");
        }

        private void rgvExtras_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvExtras.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvExtras.Columns[i].HeaderText != null && resxSet.GetString(rgvExtras.Columns[i].HeaderText) != null)
                        rgvExtras.Columns[i].HeaderText = resxSet.GetString(rgvExtras.Columns[i].HeaderText);
                }
            }

            if (File.Exists(layoutArrangementExtras))
            {
                rgvExtras.LoadLayout(layoutArrangementExtras);
            }
            else
            {

                this.rgvExtras.SummaryRowsTop.Clear();
                rgvExtras.MasterTemplate.EnablePaging = false;
                rgvExtras.MasterTemplate.ShowTotals = true;

                if (rgvExtras.Columns.Count > 0)
                {


                    rgvExtras.Columns["idArrangementPrice"].IsVisible = false;
                    rgvExtras.Columns["idArrangement"].IsVisible = false;
                    rgvExtras.Columns["idClient"].IsVisible = false;
                    rgvExtras.Columns["idUserCreated"].IsVisible = false;
                    rgvExtras.Columns["dtUserCreated"].IsVisible = false;
                    rgvExtras.Columns["idUserModified"].IsVisible = false;
                    rgvExtras.Columns["dtUserModified"].IsVisible = false;

                    string expression = "Sum(IIf(isNotForTraveler=True,0,total))";
                    GridViewSummaryItem summaryItem = new GridViewSummaryItem("total", "{0}", expression);
                    summaryItem.FormatString = "{0:N2}";
                    this.rgvExtras.SummaryRowsTop.Add(new GridViewSummaryRowItem(new GridViewSummaryItem[] { summaryItem }));
                }
            }

            rgvExtras.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            rgvExtras.Show();
        }


        private void radMenuItemExtrasSaveLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutArrangementExtras))
            {
                File.Delete(layoutArrangementExtras);
            }
            rgvExtras.SaveLayout(layoutArrangementExtras);

            RadMessageBox.Show("Layout saved");
        }


        private void rgvTransport_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is Telerik.WinControls.UI.GridSummaryCellElement)
            {
                e.CellElement.TextAlignment = ContentAlignment.MiddleRight;

            }
        }

        private void rgvAccomodation_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is Telerik.WinControls.UI.GridSummaryCellElement)
            {

                e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
            }
        }

        private void rgvArrangementPrice_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            try
            {
                if (e.CellElement is Telerik.WinControls.UI.GridSummaryCellElement)
                {
                    e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void rgvExtras_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is Telerik.WinControls.UI.GridSummaryCellElement)
            {
                e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
            }
        }

        //private void LoadDiverse()
        //{
        //    lblDiverseProvision_Value1.Text = lblNum_numberoftraveldays.Text;
        //    lblDiverseTravelInsurance_Value1.Text = lblNum_numberoftraveldays.Text;

        //    decimal diverseProvision = Math.Round(Decimal.Parse(lblDiverseProvision_Value1.Text) * Convert.ToDecimal(maskedDiversePRovision.Value));
        //    decimal travelInsurance = Decimal.Parse(lblDiverseProvision_Value1.Text) * Convert.ToDecimal(maskedDiverseTravelInsurance.Value);

        //    lblDiverseProvision_Value2.Text = diverseProvision.ToString();
        //    lblDiverseTravelInsurance_Value2.Text = travelInsurance.ToString();
        //    decimal subototal = Math.Round(diverseProvision + Convert.ToDecimal(numtxtAmount.Value), 2);
        //    lblDivereSubtotal_Value.Text = subototal.ToString();

        //}
        private void LoadDiverse()
        {
            lblDiverseProvision_Value1.Text = lblNum_numberoftraveldays.Text;
            lblDiverseTravelInsurance_Value1.Text = lblNum_numberoftraveldays.Text;

            decimal diverseProvision = Math.Round(Decimal.Parse(lblDiverseProvision_Value1.Text) * Convert.ToDecimal(maskedDiversePRovision.Value));
            decimal travelInsurance = Decimal.Parse(lblDiverseProvision_Value1.Text) * Convert.ToDecimal(maskedDiverseTravelInsurance.Value);

            lblDiverseProvision_Value2.Text = diverseProvision.ToString();
            lblDiverseTravelInsurance_Value2.Text = travelInsurance.ToString();
            decimal subototal = Math.Round(diverseProvision + Convert.ToDecimal(numtxtAmount.Value), 2);
            lblDivereSubtotal_Value.Text = subototal.ToString();

            ArrangementInvoicePriceBUS aipbus = new ArrangementInvoicePriceBUS();
            decimal sells = 0;
            if (isFinished == true && chkRecalculation.CheckState == CheckState.Checked)
            {
                if (arrange != null)
                {
                    totalLista = aipbus.GetArrangementById(arrange.idArrangement);
                    if (totalLista != null)
                    {
                        lista = new List<ArrangementInvoicePriceModel>();
                        lista = aipbus.GetSellingByIdArrangement(arrange.idArrangement);
                        if (lista != null)
                        {
                            if (lista.Count > 0)
                            {
                                sells = Convert.ToDecimal(lista[0].sellingPrice);

                            }
                        }
                        // decimal diverseProvision = Math.Round(Decimal.Parse(lblDiverseProvision_Value1.Text) * Convert.ToDecimal(maskedDiversePRovision.Value));
                        if (Convert.ToDecimal(lblNumSubtotal.Text) - sells != 0)
                            lblDiverseProvision_Value2.Text = (diverseProvision + Convert.ToDecimal(lblNumSubtotal.Text) - sells).ToString();



                    }
                }
            }

        }


        //=====
        private void VolInsurance()
        {
            decimal T1 = 0;
            T2 = 0;
            T1 = (Convert.ToDecimal(numNumberLeader.Value) * Convert.ToDecimal(maskedPremie1.Value) * Convert.ToDecimal(numDays.Value)) + (Convert.ToDecimal(numCO.Value) * Convert.ToDecimal(maskedPremie2.Value) * Convert.ToDecimal(numDays.Value));
            if (T1 != null && T1 != 0 && (Convert.ToDecimal(numNumberLeader.Value) + Convert.ToDecimal(numCO.Value)) != 0)
                T2 = T1 / (Convert.ToDecimal(numNumberLeader.Value) + Convert.ToDecimal(numCO.Value));
            maskedTotalIns.Value = T1.ToString();
            maskedPP.Value = T2.ToString();
            numVerzBeg.Text = T2.ToString();

        }
        //====



        private void maskedDiversePRovision_ValueChanged(object sender, EventArgs e)
        {
            LoadDiverse();
        }

        private void maskedDiverseTravelInsurance_ValueChanged(object sender, EventArgs e)
        {
            LoadDiverse();
        }

        private void numTravelInsurance_ValueChanged(object sender, EventArgs e)
        {
            loadPrice();
        }

        private void numPolisCosts_ValueChanged(object sender, EventArgs e)
        {
            loadPrice();
        }

        private void numSurcharge_ValueChanged(object sender, EventArgs e)
        {
            LoadAccompaniment();
        }

        private void numVerzBeg_ValueChanged(object sender, EventArgs e)
        {
            LoadAccompaniment();
        }

        private void numDiscount_ValueChanged(object sender, EventArgs e)
        {
            LoadAccompaniment();
        }

        private void numtxtAmount_ValueChanged(object sender, EventArgs e)
        {
            LoadDiverse();
        }


        private void numNumberLeader_ValueChanged(object sender, EventArgs e)
        {
            VolInsurance();
        }

        private void maskedPremie1_ValueChanged(object sender, EventArgs e)
        {
            VolInsurance();
        }

        private void numCO_ValueChanged(object sender, EventArgs e)
        {
            VolInsurance();
        }

        private void maskedPremie2_ValueChanged(object sender, EventArgs e)
        {
            VolInsurance();
        }


        private void maskedNrVoluntary_ValueChanged(object sender, EventArgs e)
        {
            int a = (Convert.ToInt32(maskedNrVoluntary.Text) - 1);

            // added because of new field where they can change the number of voluntary
            if (Convert.ToInt32(numNrVoluntary.Value) != 0)
                a = (Convert.ToInt32(numNrVoluntary.Text) - 1);

            //if (a > 1)
            numNumberLeader.Text = a.ToString();
            //else
            // numNumberLeader.Text = "1";

            if (isFinished == true && chkRecalculation.CheckState == CheckState.Checked)
                recalcCalculationModel.nrVoluntaryHelper = Convert.ToInt32(maskedNrVoluntary.Value);

        }

        private void maskedVolDays_ValueChanged(object sender, EventArgs e)
        {
            numDays.Value = maskedVolDays.Value;
        }

        private void btnNewBoardingPoint_Click(object sender, EventArgs e)
        {
            using (frmBoardingPoint frm = new frmBoardingPoint())
            {
                frm.ShowDialog();

                if (frm.DialogResult == System.Windows.Forms.DialogResult.Yes)
                    LoadBoardingPoint();
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();


        }

        private void btnNewThemeTrip_Click(object sender, EventArgs e)
        {
            using (frmThemeTrip frm = new frmThemeTrip())
            {
                frm.ShowDialog();
                if (frm.DialogResult == System.Windows.Forms.DialogResult.Yes)
                    if (btnAll.CheckState == CheckState.Checked)
                        getAllThemeTrips();
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

        }

        private void radPageExtras_SelectedPageChanged(object sender, EventArgs e)
        {
            RadPageView rpv = (RadPageView)sender;
            string sName = ((RadPageView)sender).SelectedPage.Name;
            switch (sName)
            {
                case "tabBoardingPoint":
                    //LoadBoardingPoint();
                    break;
                case "tabThemeTrip":
                    LoadThemeTrip();
                    break;
                case "tabTargetGroup":
                    LoadTargetGroup();
                    break;
                case "tabRooms":
                    loadRooms();
                    break;
                default:
                    break;
            }
        }

        private void numMinNumberTravelers_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(numMinNumberTravelers.Value) != 0)
                minNumber = Convert.ToInt32(numMinNumberTravelers.Value);
            else
                minNumber = Convert.ToInt32(maskedMinNrTravelers.Value);

            LoadAccomodation();
            loadTransport();
            loadArrangementsPrice();
            LoadExtras();
            LoadAccompaniment();
            loadPrice();
        }

        private void maskedDiverseCorrection_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(lblDiverseProvision_Value1.Text) != 0)
            {
                if (Convert.ToDecimal(maskedDiverseCorrection.Value) != 0)
                {
                    if (arrCalcModel != null)
                    {
                        arrCalcModel.provision = Convert.ToDecimal(maskedDiverseCorrection.Value) / Convert.ToDecimal(lblDiverseProvision_Value1.Text);
                        maskedDiversePRovision.Value = arrCalcModel.provision;
                    }
                }
            }

        }


        private void maskedDayOfArr_ValueChanged(object sender, EventArgs e)
        {
            DateTime dfr = new DateTime(dpDateFrom.Value.Year, dpDateFrom.Value.Month, dpDateFrom.Value.Day);
            if (Convert.ToInt32(maskedDayOfArr.Value) >= 1)
            {
                DateTime dto = dfr.AddDays(Convert.ToInt32(maskedDayOfArr.Value) - 1);
                if (dpDateTo.Text != dto.ToString())
                    dpDateTo.Text = dto.ToString();


            }
        }

        private void dpDateTo_ValueChanged(object sender, EventArgs e)
        {
            DateTime dt1 = new DateTime(dpDateTo.Value.Year, dpDateTo.Value.Month, dpDateTo.Value.Day);
            DateTime dt2 = new DateTime(dpDateFrom.Value.Year, dpDateFrom.Value.Month, dpDateFrom.Value.Day);
            int days = Convert.ToInt32((dt1 - dt2).TotalDays) + 1;
            if (maskedDayOfArr.Text != days.ToString())
                maskedDayOfArr.Text = days.ToString();

        }

        private void numDays_ValueChanged(object sender, EventArgs e)
        {
            VolInsurance();
        }



        private void chkSport_CheckStateChanged(object sender, EventArgs e)
        {
            RadCheckBox rchk = (RadCheckBox)sender;
            if (chkSport.Checked == true)
            {
                maskedPremie1.Value = premieNo1 + extra;
                maskedPremie2.Value = premieNo2 + extra;
                maskedDiverseTravelInsurance.Value = maskedPremie1.Value;
            }
            else
            {
                maskedPremie1.Value = premieNo1;
                maskedPremie2.Value = premieNo2;
                maskedDiverseTravelInsurance.Value = maskedPremie1.Value;
            }
        }



        private void rgvTotalExtra_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if (rgvTotalExtra != null)
            {
                for (int i = 0; i < rgvTotalExtra.Columns.Count; i++)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (rgvTotalExtra.Columns[i].HeaderText != null && resxSet.GetString(rgvTotalExtra.Columns[i].HeaderText) != null)
                            rgvTotalExtra.Columns[i].HeaderText = resxSet.GetString(rgvTotalExtra.Columns[i].HeaderText);
                    }
                }
            }

            if (rgvTotalExtra.Rows.Count > 0)
            {
                for (int i = 0; i < rgvTotalExtra.Rows.Count; i++)
                {
                    if (rgvTotalExtra.Rows[i].Cells["idArticle"] != null)
                        if (rgvTotalExtra.Rows[i].Cells["idArticle"].Value != null)
                            if (rgvTotalExtra.Rows[i].Cells["idArticle"].Value.ToString() == "Reis Pakket" || rgvTotalExtra.Rows[i].Cells["idArticle"].Value.ToString() == "Insurance")     //Reisverzekering
                            {
                                for (int j = 0; j < rgvTotalExtra.Columns.Count; j++)
                                {
                                    rgvTotalExtra.Rows[i].Cells[j].ReadOnly = true;
                                }
                            }
                }
            }
            if (rgvTotalExtra.Columns.Count > 0)
            {
                rgvTotalExtra.Columns["idArrangement"].IsVisible = false;
                rgvTotalExtra.Columns["purchasePriceTotal"].IsVisible = false;
                rgvTotalExtra.Columns["isExtra"].IsVisible = false;
                rgvTotalExtra.Columns["calculation"].IsVisible = false;

            }
        }
        private void FIllTotalExtra()
        {
            ArrangementInvoicePriceBUS aipbus = new ArrangementInvoicePriceBUS();
            totalLista = new List<ArrangementInvoicePriceModel>();
            rgvTotalExtra.DataSource = null;
            if (arrange != null)
            {
                totalLista = aipbus.GetArrangementById(arrange.idArrangement);
                if (totalLista != null)
                {
                    // totlist = lista1;
                    rgvTotalExtra.DataSource = totalLista;
                    //   SumTotGrid(totlist);

                }
            }
            rgvTotalExtra.Show();

        }
        private void SumTotGrid(List<IModel> lista)
        {
            int i = 0;
            totalTotalGrid = 0;
            foreach (ArrangementPriceModel itm in lista)
            {
                if (itm != null)
                {
                    if (rgvTotalExtra.RowCount - 1 >= i)
                    {

                        if (rgvTotalExtra.Rows[i].Cells["priceTotal"].Value != null)
                            totalTotalGrid = totalTotalGrid + Convert.ToDecimal(rgvTotalExtra.Rows[i].Cells["priceTotal"].Value.ToString());


                    }
                }
                i++;
            }
            //   loadPrice();
        }

        private void btnNewItem_Click(object sender, EventArgs e)
        {
            ArrangementPriceBUS ccentar1 = new ArrangementPriceBUS();
            List<IModel> gmX1 = new List<IModel>();
            ArrangementInvoicePriceModel apm = new ArrangementInvoicePriceModel();

            Boolean isFirst2 = false;
            if (isFinished == true && chkRecalculation.CheckState == CheckState.Unchecked)
                isFirst2 = true;

            gmX1 = ccentar1.GetAllTotalWithExtra(arrange.idArrangement, minNumber, isFirst2, false);

            using (var dlgSave1 = new GridLookupForm(gmX1, "Extra articals"))
            {
                if (dlgSave1.ShowDialog(this) == DialogResult.Yes)
                {
                    ArrangementPriceModel genmX1 = new ArrangementPriceModel();
                    genmX1 = (ArrangementPriceModel)dlgSave1.selectedRow;

                    // puni grid na totalu
                    apm.idArticle = genmX1.idArticle;
                    apm.descriptionArticle = genmX1.nameArticle;
                    apm.isExtra = true;
                    apm.isOption = true;
                    apm.nrArticle = genmX1.nrArticle;
                    apm.purchasePrice = genmX1.purchasePrice;
                    apm.idArrangement = Convert.ToInt32(txtArrId.Text);
                    if (totalLista == null)
                        totalLista = new List<ArrangementInvoicePriceModel>();
                    for (int i = 0; i < totalLista.Count; i++)
                    {
                        if (totalLista != null)
                        {
                            if (totalLista[i].idArticle == apm.idArticle)
                            {
                                translateRadMessageBox trs = new translateRadMessageBox();
                                trs.translateAllMessageBox("You allready have that artical!");
                                return;
                            }
                        }

                    }
                    totalLista.Add(apm);
                    rgvTotalExtra.DataSource = null;
                    rgvTotalExtra.DataSource = totalLista;
                    rgvTotalExtra.Show();

                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

        }
        private void saveArrInvoicePrice()
        {
            invoicePrice = new List<ArrangementInvoicePriceModel>();
            ArrangementInvoicePriceModel aiv = new ArrangementInvoicePriceModel();

            if (txtArrId.Text != "")
            {

                //prvi slog cena puta
                //if (llrais == false)
                //{
                aiv.idArrangement = arrange.idArrangement;  // Convert.ToInt32(txtArrId.Text);
                aiv.idArticle = "Reis Pakket";
                aiv.nrArticle = 1;
                aiv.calculation = true;
                aiv.descriptionArticle = "Reis Pakket Prijs";
                if (arrange.idArrangement != 0)
                {
                    aiv.descriptionArticle = arrange.nameArrangement;
                }
                aiv.isExtra = false;
                aiv.isOption = false;
                aiv.purchasePrice = 0;
                aiv.purchasePriceTotal = 0;
                aiv.sellingPrice = Decimal.Parse(lblNumSubtotal.Text);
                invoicePrice.Add(aiv);
                aiv = new ArrangementInvoicePriceModel();
                aiv.idArrangement = arrange.idArrangement;  // Convert.ToInt32(txtArrId.Text);
                aiv.idArticle = "Insurance";   //Reisverzekering
                aiv.nrArticle = 1;
                aiv.calculation = false;
                aiv.descriptionArticle = "";
                if (arrange.countryArrangement != null && arrange.countryArrangement != 0)
                {
                    string codeCountry = "";
                    CountryModel cm = new CountryModel();
                    cm = new CountryBUS().GetCountryByID(arrange.countryArrangement);
                    if (cm != null)
                        codeCountry = cm.premie;
                    bool isSport = false;
                    if (chkSport.CheckState == CheckState.Checked)
                        isSport = true;
                    ArrangementTravelInsuranceModel atim = new ArrangementTravelInsuranceModel();
                    atim = new ArrangementInsuranceBUS().GetArrangementTravelInsurance(codeCountry, isSport);
                    aiv.descriptionArticle = atim.description;
                }
                aiv.isExtra = false;
                aiv.isOption = true;
                aiv.purchasePrice = 0;
                aiv.purchasePriceTotal = 0;
                aiv.sellingPrice = Decimal.Parse(lblDiverseTravelInsurance_Value2.Text);
                invoicePrice.Add(aiv);
                aiv = new ArrangementInvoicePriceModel();
                if (totalLista != null)
                {
                    foreach (ArrangementInvoicePriceModel aa in totalLista)
                    {
                        aiv = new ArrangementInvoicePriceModel();
                        aiv.idArrangement = arrange.idArrangement;  // Convert.ToInt32(txtArrId.Text);
                        if (aa.idArticle != "Reis Pakket" && aa.idArticle != "Insurance")  //Reisverzekering
                        {

                            aiv.idArticle = aa.idArticle;
                            aiv.nrArticle = aa.nrArticle;
                            aiv.calculation = false;
                            aiv.descriptionArticle = aa.descriptionArticle;
                            aiv.isExtra = aa.isExtra;
                            aiv.isOption = aa.isOption;
                            aiv.purchasePrice = aa.purchasePrice;
                            aiv.purchasePriceTotal = aa.purchasePriceTotal;
                            aiv.sellingPrice = aa.sellingPrice;

                            invoicePrice.Add(aiv);
                        }
                    }
                }
                ArrangementInvoicePriceBUS aipb = new ArrangementInvoicePriceBUS();
                bool isOk = false;
                isOk = aipb.Delete(Convert.ToInt32(txtArrId.Text), this.Name, Login._user.idUser);
                if (isOk == false)
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Error deleting Arrangement Invoicing");
                    return;
                }
                else
                {
                    for (int d = 0; d < invoicePrice.Count; d++)
                    {

                        if (aipb.Save(invoicePrice[d], this.Name, Login._user.idUser) == true)
                        {
                            isOk = true;
                        }
                        else
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translatePartAndNonTranslatedPart("Error saving Arrangement Invoicing ", (d + 1).ToString());
                            isOk = false;
                        }
                    }
                }


            }
        }

        private void btnAddRooms_Click(object sender, EventArgs e)
        {

            //grid accomodatio
            List<ArrangementArticalModel_Rooms> modelArticlesAccomodation = new List<ArrangementArticalModel_Rooms>();
            modelArticlesAccomodation = new ArticalBUS().GetAllArticalsForArrangemetAccomodation1(iID);

            ArrangementRoomsBUS arb = new ArrangementRoomsBUS();


            using (GridLookupFormRooms glfr = new GridLookupFormRooms(modelArticlesAccomodation, null, iID, "Rooms"))
            {
                glfr.ShowDialog();
                loadRooms();
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

        }

        private void loadRooms()
        {
            rgvRooms.DataSource = null;
            rgvRooms.DataSource = new ArrangementRoomsBUS().GetAllRoomsForArrangement(iID);

        }

        private void btnDelRooms_Click(object sender, EventArgs e)
        {
            if (rgvRooms.CurrentRow != null)
            {
                if (rgvRooms.CurrentRow.DataBoundItem != null)
                {
                    ArrangementRoomsModel m = (ArrangementRoomsModel)rgvRooms.CurrentRow.DataBoundItem;
                    ArticalBUS abus = new ArticalBUS();
                    ArticalModel amod = new ArticalModel();

                    amod = abus.GetArticalByID(m.idArticle);
                    translateRadMessageBox trr = new translateRadMessageBox();
                    DialogResult dr = trr.translateAllMessageBoxDialog("Delete Rooms for article: " + amod.nameArtical, "Delete Room");
                    if (dr == DialogResult.Yes)
                    {
                        ArrangementRoomsBUS bus = new ArrangementRoomsBUS();
                        if (bus.checkIfRoomsAreBooked(m.idArrangement, m.idArticle, m.id) == false)
                        {
                            bool b = bus.Delete(m, this.Name, Login._user.idUser);

                            if (b == true)
                            {
                                loadRooms();
                            }
                            else
                            {
                                trr.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                            }
                        }
                        else
                        {
                            trr.translateAllMessageBox("You can't delete this room numbers because you have booked some of it!");
                        }
                    }
                }
            }
        }

        private void rgvRooms_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvRooms.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvRooms.Columns[i].HeaderText != null && resxSet.GetString(rgvRooms.Columns[i].HeaderText) != null)
                        rgvRooms.Columns[i].HeaderText = resxSet.GetString(rgvRooms.Columns[i].HeaderText);
                }
            }

            if (File.Exists(layoutArrangementRooms))
            {
                rgvRooms.LoadLayout(layoutArrangementRooms);
            }
        }

        private void radMenuItemRooms_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutArrangementRooms))
            {
                File.Delete(layoutArrangementRooms);
            }
            rgvRooms.SaveLayout(layoutArrangementRooms);

            RadMessageBox.Show("Layout saved");
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            if (arrange != null)
            {
                using (frmPassengerSelection frm = new frmPassengerSelection(arrange))
                {
                    frm.ShowDialog();
                }
            }
            else
            {
                using (frmPassengerSelection frm = new frmPassengerSelection())
                {
                    frm.ShowDialog();
                }
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

        }

        BindingList<BoardingPointModel> listaBoardingPoint;

        private void LoadBoardingPoint()
        {
            ArrangementBoardingPointBUS abpb = new ArrangementBoardingPointBUS();
            List<BoardingPointModel> arrBoardingPoint = new List<BoardingPointModel>();
            arrBoardingPoint = abpb.GetAllBoardingPoint(iID);


            List<BoardingPointModel> checkedBoardingPoint = new List<BoardingPointModel>();
            checkedBoardingPoint = abpb.GetArrangementBoardingPoint(arrange.idArrangement);



            if (arrBoardingPoint != null)
                listaBoardingPoint = new BindingList<BoardingPointModel>(arrBoardingPoint);
            else
                listaBoardingPoint = new BindingList<BoardingPointModel>();


            foreach (BoardingPointModel m in listaBoardingPoint)
            {
                if (checkedBoardingPoint != null)
                {
                    var f = checkedBoardingPoint.Find(s => s.idBoardingPoint == m.idBoardingPoint);
                    if (f != null)
                    {
                        m.isChecked = true;

                    }
                }
            }

            gridBoardingPoint.DataSource = listaBoardingPoint;

        }

        private void gridBoardingPoint_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            GridViewEditManager editManager = sender as GridViewEditManager;

            //Aleksa i Mitar for enabling filter on nameBoarding point
            /*
            if (editManager.GridViewElement.CurrentCell is GridFilterCellElement && e.Column.Name == "nameBoardingPoint")
            {
                e.Cancel = true;
            }*/

            if (editManager.GridViewElement.CurrentCell is GridFilterCellElement && e.Column.Name == "dtDeparture")
            {
                e.Cancel = true;
            }

            if (editManager.GridViewElement.CurrentCell is GridFilterCellElement && e.Column.Name == "dtArrival")
            {
                e.Cancel = true;
            }
        }


        private void gridBoardingPoint_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            var grid = sender as RadGridView;
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                foreach (var column in grid.Columns)
                {
                    if (resxSet.GetString(column.HeaderText) != null && resxSet.GetString(column.HeaderText) != "")
                        column.HeaderText = resxSet.GetString(column.HeaderText);
                    column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                }
            }

            if (gridBoardingPoint != null)
                if (gridBoardingPoint.ColumnCount > 0)
                {
                    gridBoardingPoint.Columns["isChecked"].Width = 50;

                    gridBoardingPoint.Columns["idBoardingPoint"].IsVisible = false;

                    gridBoardingPoint.Columns["nameBoardingPoint"].ReadOnly = true;
                    gridBoardingPoint.Columns["addressBoardingPoint"].ReadOnly = true;

                    gridBoardingPoint.Columns["dtDeparture"].IsVisible = false;
                    gridBoardingPoint.Columns["dtArrival"].IsVisible = false;
                    gridBoardingPoint.Columns["sortBoardingPoint"].IsVisible = false;
                }
        }

        private void gridBoardingPoint_ValueChanged(object sender, EventArgs e)
        {
            if (this.gridBoardingPoint.ActiveEditor is RadCheckBoxEditor)
            {
                gridBoardingPoint.EndEdit();
            }
        }

        private void chkRecalculation_CheckStateChanged(object sender, EventArgs e)
        {


            if (isFinished == true && chkRecalculation.CheckState == CheckState.Checked)
            {
                tabPurchaseCopy.Item.Visibility = ElementVisibility.Visible;
                tabPurchaseCopy.Text = "First calculation";
            }
            else if (isFinished == true && chkRecalculation.CheckState == CheckState.Unchecked)
            {
                tabPurchaseCopy.Item.Visibility = ElementVisibility.Visible;
                tabPurchaseCopy.Text = "Recalculation";
            }
            else if (isFinished == false && chkRecalculation.CheckState == CheckState.Checked)
            {
                List<ArrangementInvoicePriceModel> laip = new List<ArrangementInvoicePriceModel>();
                laip = new ArrangementInvoicePriceBUS().GetArrangementById(arrange.idArrangement);
                if (laip == null)
                {
                    chkRecalculation.CheckState = CheckState.Unchecked;
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You didn't click to save the calculation so you can't finish it.");
                }
                else if (laip.Count == 0)
                {
                    chkRecalculation.CheckState = CheckState.Unchecked;
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You didn't click to save the calculation so you can't finish it.");
                }

            }

            LoadCalculationRecalculation(false);
        }

        private void maskedNrTravelers_ValueChanged(object sender, EventArgs e)
        {

            if (isFinished == true && chkRecalculation.CheckState == CheckState.Checked)
                recalcCalculationModel.nrTraveler = Convert.ToInt32(maskedNrTravelers.Value);
        }

        private void btnSaveCalc_Click(object sender, EventArgs e)
        {
            saveArrInvoicePrice();
            rgvTotalExtra.DataSource = null;
            FIllTotalExtra();
        }

        private void rgvPurchaseCopy_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvPurchaseCopy.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvPurchaseCopy.Columns[i].HeaderText != null && resxSet.GetString(rgvPurchaseCopy.Columns[i].HeaderText) != null)
                        rgvPurchaseCopy.Columns[i].HeaderText = resxSet.GetString(rgvPurchaseCopy.Columns[i].HeaderText);
                }
                if (rgvPurchaseCopy.Columns[i].GetType() == typeof(GridViewDateTimeColumn))
                {
                    if (rgvPurchaseCopy.Columns[i].Name.ToLower() != "dtUserModified".ToLower() && rgvPurchaseCopy.Columns[i].Name.ToLower() != "dtUserCreated".ToLower())
                    {
                        rgvPurchaseCopy.Columns[i].FormatString = "{0: dd-MM-yyyy}";
                    }
                }
            }

            if (File.Exists(layoutPurchase))
            {
                rgvPurchaseCopy.LoadLayout(layoutPurchase);
            }
            else
            {

                this.rgvPurchaseCopy.SummaryRowsTop.Clear();
                rgvPurchaseCopy.MasterTemplate.EnablePaging = false;
                rgvPurchaseCopy.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = rgvPurchaseCopy.Columns["priceTotal"].Name;
                summaryItem.Aggregate = GridAggregateFunction.Sum;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.rgvPurchaseCopy.SummaryRowsTop.Add(summaryRowItem);

                rgvPurchaseCopy.Columns["idArrangementPrice"].IsVisible = false;
                rgvPurchaseCopy.Columns["idArrangement"].IsVisible = false;
                rgvPurchaseCopy.Columns["idClient"].IsVisible = false;
                rgvPurchaseCopy.Columns["idUserCreated"].IsVisible = false;
                rgvPurchaseCopy.Columns["dtUserCreated"].IsVisible = false;
                rgvPurchaseCopy.Columns["idUserModified"].IsVisible = false;
                rgvPurchaseCopy.Columns["dtUserModified"].IsVisible = false;
            }

            rgvPurchaseCopy.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            rgvPurchaseCopy.Show();
        }

        private void rgvPurchaseCopy_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is Telerik.WinControls.UI.GridSummaryCellElement)
            {
                e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
            }
        }

        private void radMenuItemSavePurchase_Click_1(object sender, EventArgs e)
        {
            if (File.Exists(layoutPurchase))
            {
                File.Delete(layoutPurchase);
            }
            rgvPurchase.SaveLayout(layoutPurchase);

            RadMessageBox.Show("Layout saved");
        }

        private void numNrVoluntary_ValueChanged(object sender, EventArgs e)
        {
            int a = (Convert.ToInt32(maskedNrVoluntary.Text) - 1);

            // added because of new field where they can change the number of voluntary
            if (Convert.ToInt32(numNrVoluntary.Value) != 0)
                a = (Convert.ToInt32(numNrVoluntary.Text) - 1);

            //if (a > 1)
            numNumberLeader.Text = a.ToString();
            //else
            // numNumberLeader.Text = "1";

            if (isFinished == true && chkRecalculation.CheckState == CheckState.Checked)
                recalcCalculationModel.nrVoluntary = Convert.ToInt32(numNrVoluntary.Value);


            if (isFinished == false)
                if (arrCalcModel != null)
                    arrCalcModel.nrVoluntary = Convert.ToInt32(numNrVoluntary.Value);


            //LoadAccompaniment();

        }

        private void txtCountry_Leave(object sender, EventArgs e)
        {
            if (txtCountry.Text != "")
            {
                CountryBUS cb = new CountryBUS();
                CountryModel cm = new CountryModel();
                cm = cb.GetCountryByCodeOrName(txtCountry.Text);

                if (cb != null)
                {
                    if (cm.idCountry != 0)
                    {
                        arrange.codeArrangement = cm.idCountry.ToString();
                        txtCountry.Text = cm.nameCountry;
                        txtInsProvision.Text = cm.provision.ToString();
                        txtInsPremie.Text = cm.premie.ToString();
                        if (isFinished == true && chkRecalculation.CheckState == CheckState.Unchecked)
                            firstNotArticles.idCountry = cm.idCountry;                            
                        else
                        {
                            arrange.countryArrangement = cm.idCountry;
                            arrange.countryNameArrangement = cm.nameCountry;
                        }
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You didn't fill right the country.");
                        arrange.countryArrangement = 0;
                        arrange.countryNameArrangement = "";
                    }
                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You didn't fill right the country.");
                    arrange.countryArrangement = 0;
                    arrange.countryNameArrangement = "";
                }
            }


        }

        private void UpdateOriginalValuesAfterSave()
        {
            arrangeFirst = new ArrangementModel(arrange);
        }
        private void frmArrangement_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveArrangement();

            // changes in Arrangement data 
            bool changes = arrange.CompareWith(arrangeFirst);

            if (changes == true)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                DialogResult dr = tr.translateAllMessageBoxDialog("There is changes on form. Save before close ?", "Save");
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    bool validate = ValidateArrangement();
                    if (validate == true)
                    {
                        InsertArrangement();
                    }
                    else
                        e.Cancel = true;
                    
                    //btnSave.PerformClick();
                }
                else if (dr == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    // NO option
                    arrange.CopyValues(arrangeFirst);
                }
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

                    int idPers = 0;
                    int idCli = 0;

                    if (rgvDocuments.SelectedRows[0].Cells["idContPers"].Value != null)
                        idPers = Int32.Parse(rgvDocuments.SelectedRows[0].Cells["idContPers"].Value.ToString());

                    if (rgvDocuments.SelectedRows[0].Cells["idClient"].Value != null)
                        idCli = Int32.Parse(rgvDocuments.SelectedRows[0].Cells["idClient"].Value.ToString());

                    using (frmDocuments frm = new frmDocuments(iID, arrange.idArrangement, idPers, idCli, "arr"))
                    {
                        frm.ShowDialog();
                        if (frm.modelChanged == true)
                        {
                            DocumentsBUS nbus = new DocumentsBUS();

                            arrDocuments = nbus.GetArrangementDoc(arrange.idArrangement, Login._user.lngUser);
                            rgvDocuments.DataSource = null;
                            rgvDocuments.DataSource = arrDocuments;
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
            }
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

        private void btnNewDoc_Click(object sender, EventArgs e)
        {
            string what = "arr";
            using (frmDocuments frm = new frmDocuments(arrange.idArrangement, what))
            {
                //frmDocuments frm = new frmDocuments(iID, arrange.idArrangement, 0, 0, "arr");
                frm.ShowDialog();
                if (frm.modelChanged == true)
                {
                    DocumentsBUS nbus = new DocumentsBUS();

                    arrDocuments = nbus.GetArrangementDoc(arrange.idArrangement, Login._user.lngUser);
                    rgvDocuments.DataSource = null;
                    rgvDocuments.DataSource = arrDocuments;
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

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
                            arrDocuments = db.GetArrangementDoc(arrange.idArrangement, Login._user.lngUser);
                            rgvDocuments.DataSource = null;
                            rgvDocuments.DataSource = arrDocuments;
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

        private void btnNewContactClick(object sender, EventArgs e)
        {
            int iID = -1;               //Int32.Parse(rgvContacts.SelectedRows[0].Cells["idContact"].Value.ToString());
            
            string what = "new";
            frmContacts frm = new frmContacts(iID, what, 0, arrange.idArrangement);
            //frmContacts frm = new frmContacts();
            frm.Show();
            if (frm.modelChanged == true)
            {
                ContactsBUS nbus6 = new ContactsBUS();

                arrContacts = nbus6.GetContactsByArrangament(arrange.idArrangement);
                rgvContacts.DataSource = null;
                rgvContacts.DataSource = arrContacts;
            }
        }

        private void btnDeleteContactClick(object sender, EventArgs e)
        {
            if (rgvContacts.SelectedRows.Count > 0)
            {
                if (DialogResult.Yes == RadMessageBox.Show("Are you sure to delete this Contact?", "Delete", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {
                    try
                    {
                        if (rgvDocuments.SelectedRows[0].Cells["idContact"].Value != null)
                        {
                            int iID = Int32.Parse(rgvContacts.SelectedRows[0].Cells["idContact"].Value.ToString());
                            ContactsBUS del1 = new ContactsBUS();
                            del1.Delete(iID, this.Name, Login._user.idUser);
                            arrContacts = del1.GetContactsByPerson(arrange.idArrangement);
                            rgvContacts.DataSource = null;
                            rgvContacts.DataSource = arrContacts;
                        }
                    }
                    catch (Exception ex)
                    {
                        RadMessageBox.Show("Error deleting Contact. \nMessage: " + ex.Message, "Error delete", MessageBoxButtons.OK, RadMessageIcon.Error);
                    }
                }
            }
            else
            {
                if (rgvContacts.Rows.Count > 0)
                    RadMessageBox.Show("First you have to select contact that you want to delete");
                else
                    RadMessageBox.Show("You don't have contacts to delete");
            }
        }

        private void btnNewTask_Click(object sender, EventArgs e)
        {
            // new Task
            string what = "new";
            using (frmTasks frm = new frmTasks(0, what))
            {
                frm.idArr = arrange.idArrangement;
                frm.ShowDialog();
                if (frm.modelChanged == true)
                {
                    ToDoBUS nbus = new ToDoBUS();

                    arrToDo = nbus.GetToDoArrangement(arrange.idArrangement);
                    rgvToDo.DataSource = null;
                    rgvToDo.DataSource = arrToDo;
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

        }

        private void btnDelTask_Click(object sender, EventArgs e)
        {
            if (rgvToDo.SelectedRows.Count > 0)
            {
                // Delete Task
                if (DialogResult.Yes == RadMessageBox.Show("Are you sure to delete this Task?", "Delete", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {
                    try
                    {
                        if (rgvToDo.SelectedRows[0].Cells["idToDo"].Value != null)
                        {
                            int iID = Int32.Parse(rgvToDo.SelectedRows[0].Cells["idToDo"].Value.ToString());
                            ToDoBUS del = new ToDoBUS();
                            del.Delete(iID, this.Name, Login._user.idUser);
                            arrToDo = del.GetToDoPerson(arrange.idArrangement);
                            rgvToDo.DataSource = null;
                            rgvToDo.DataSource = arrToDo;
                        }
                    }
                    catch (Exception ex)
                    {
                        RadMessageBox.Show("Error deleting document. \nMessage: " + ex.Message, "Error delete", MessageBoxButtons.OK, RadMessageIcon.Error);
                    }
                }
            }
            else
            {
                if (rgvToDo.Rows.Count > 0)
                    RadMessageBox.Show("First you have to select task that you want to delete");
                else
                    RadMessageBox.Show("You don't have tasks to delete");
            }
        }
        private void btnNewMeeting_Click(object sender, EventArgs e)
        {                     
            //IEvent ev = MainForm.meetingScheduler.radScheduler1.Appointments.Add();
            using (MeetingEditAppointment editAppForm = new MeetingEditAppointment())
            {
                editAppForm.idArr = arrange.idArrangement;
                editAppForm.ThemeName = "Windows8";
                //editAppForm.Appointment.EditAppointment(ev, MainForm.meetingScheduler.radScheduler1);
                editAppForm.ShowDialog();
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();


        }
        private void LoadMeetings()
        {            
            rgvMeetings.DataSource = null;
            rgvMeetings.DataSource = new AppointmentsBUS().GetAppointmentsByArrangement(arrange.idArrangement, Login._user.lngUser);
            // saki

            if (rgvMeetings.DataSource == null)
            {
                List<BISAppointment> personMeetings = new List<BISAppointment>();
                rgvMeetings.DataSource = personMeetings;
            }
            rgvMeetings.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            rgvMeetings.Show();
            
        }
        private void LoadContacts()
        {
            rgvContacts.DataSource = new ContactsBUS().GetContactsByArrangament(arrange.idArrangement);
            // saki

            if (rgvContacts.DataSource == null)
            {
                List<ContactsModel> personContacts = new List<ContactsModel>();
                rgvContacts.DataSource = personContacts;
            }

            rgvContacts.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            rgvContacts.Show();
        }

        private void LoadTasks()
        {            
            rgvToDo.DataSource = new ToDoBUS().GetToDoArrangement(arrange.idArrangement);
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
        }
        private void rgvMeetings_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = rgvMeetings.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                BISAppointment appID = (BISAppointment)info.DataBoundItem;
                IEvent ev = MainForm.meetingScheduler.radScheduler1.Appointments.GetById(appID.Id);
                using (MeetingEditAppointment editAppForm = new MeetingEditAppointment())
                {
                    editAppForm.ThemeName = "Windows8";
                    editAppForm.EditAppointment(ev, MainForm.meetingScheduler.radScheduler1);
                    editAppForm.ShowDialog();
                    //dodala Neta jer nije radio refresh
                    rgvMeetings.DataSource = null;
                    rgvMeetings.DataSource = new AppointmentsBUS().GetAppointmentsByArrangement(arrange.idArrangement, Login._user.lngUser);
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

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

        private void radPageComm_SelectedPageChanged(object sender, EventArgs e)
        {
            RadPageView rpv1 = (RadPageView)sender;
            string sName1 = ((RadPageView)sender).SelectedPage.Name;

            switch (sName1)
            {
                case "tabMeetings":                    
                    break;
                case "tabContacts":                    
                    //rgvContacts.DataSource = new ContactsBUS().GetContactsByPerson(Person.idContPers);
                    //// saki
                    //if (rgvContacts.DataSource == null)
                    //{
                    //    List<ContactsModel> personContacts = new List<ContactsModel>();
                    //    rgvContacts.DataSource = personContacts;
                    //}

                    //rgvContacts.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

                    //rgvContacts.Show();                    
                    break;
                case "tabTasks":                  
                    ////rgvToDo.DataSource = new ToDoBUS().GetToDoPerson(Person.idContPers);
                    ////// saki

                    ////if (rgvToDo.DataSource == null)
                    ////{
                    ////    List<ToDoModel> personToDo = new List<ToDoModel>();
                    ////    rgvToDo.DataSource = personToDo;
                    ////}

                    ////rgvToDo.Columns["idToDo"].IsVisible = false;
                    ////rgvToDo.Columns["idClient"].IsVisible = false;
                    ////rgvToDo.Columns["idContPers"].IsVisible = false;
                    ////rgvToDo.Columns["idProject"].IsVisible = false;
                    ////rgvToDo.Columns["idOwner"].IsVisible = false;
                    ////rgvToDo.Columns["idEmployee"].IsVisible = false;
                    ////rgvToDo.Columns["idPriorityToDo"].IsVisible = false;
                    ////rgvToDo.Columns["idStatusToDo"].IsVisible = false;
                    ////rgvToDo.Columns["idToDoType"].IsVisible = false;
                    //////rgvToDo.Columns["isRemider"].IsVisible = false;

                    ////rgvToDo.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

                    ////rgvToDo.Show();
                    break;
            }
        }

        private void radMenuItemSaveMeetingsLayout_Click(object sender, EventArgs e)
        {
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

        private void rgvContacts_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = rgvContacts.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                if (rgvContacts.Rows.Count > 0)
                {

                    int iID = Int32.Parse(rgvContacts.SelectedRows[0].Cells["idContact"].Value.ToString());
                    int idConstPers = 0;

                    if (rgvContacts.SelectedRows[0].Cells["idContPers"].Value != null)
                        Int32.Parse(rgvContacts.SelectedRows[0].Cells["idContPers"].Value.ToString());

                    string what = "open";
                    using (frmContacts frm = new frmContacts(iID, what, idConstPers))
                    {
                        //frmContacts frm = new frmContacts();
                        frm.ShowDialog();
                        if (frm.modelChanged == true)
                        {
                            ContactsBUS nbus1 = new ContactsBUS();

                            arrContacts = nbus1.GetContactsByArrangament(arrange.idArrangement);
                            rgvContacts.DataSource = null;
                            rgvContacts.DataSource = arrContacts;
                        }
                    }

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();

                }
            }
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

        private void rgvToDo_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.rgvToDo.CurrentRow;
            if (info != null && e.RowIndex >= 0)
            {
                if (rgvToDo.Rows.Count > 0)
                {
                    if (rgvToDo.SelectedRows[0].Cells["idToDo"].Value != null)
                    {
                        int iID = Int32.Parse(rgvToDo.SelectedRows[0].Cells["idToDo"].Value.ToString());
                        string what = "open";
                        using (frmTasks frm = new frmTasks(iID, what, 0))
                        {
                            frm.idArr = arrange.idArrangement;
                            frm.ShowDialog();
                            if (frm.modelChanged == true)
                            {
                                ToDoBUS nbus = new ToDoBUS();

                                arrToDo = nbus.GetToDoArrangement(arrange.idArrangement);
                                rgvToDo.DataSource = null;
                                rgvToDo.DataSource = arrToDo;
                            }
                        }

                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();

                    }
                }
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


        //Mitar i Aleksa Leave
        private void txtType_Leave(object sender, EventArgs e)
        {

            ArrTypeBUS accBUS = new ArrTypeBUS();
            List<ArrTypeModel> am = new List<ArrTypeModel>();
            int idArrType;
            bool dataFilled = false;
            am = new ArrTypeBUS().GetArrTypes();
            if (txtType.Text != "")
            {
                bool isNumeric = int.TryParse(txtType.Text, out idArrType);
                if (isNumeric == true)

                if (am.Find(s => s.idArrType == idArrType) != null)
                {
                    ArrTypeModel atp = new ArrTypeModel();
                    atp = am.Find(s => s.idArrType == idArrType);
                    arrange.typeArrangement = atp.idArrType;
                    arrange.typeNameArrangement = atp.nameArrType;
                    txtType.Text = atp.nameArrType;
                    dataFilled = true;
                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Arrangement type wasnt filled.");
                }
            }
        }

        //Gorance lookup 16 5
        private void btnClientInvoice_Click(object sender, EventArgs e)
        {
            ClientBUS bus = new ClientBUS();
            List<IModel> pm1 = new List<IModel>();
            pm1 = bus.GetAllClients(Login._user.lngUser);
            using (var dlgSave = new GridLookupForm(pm1, "Invoice"))
            {
                if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                {
                    ClientModel pmx1 = new ClientModel();
                    pmx1 = (ClientModel)dlgSave.selectedRow;
                    txtClientInvoice.Text = pmx1.nameClient;
                    arrange.idClientInvoice = pmx1.idClient;
                    arrange.nameClient = pmx1.nameClient;
                }
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (arrange.idArrangement != -1 && arrange.idArrangement != 0)
            {
                List<ArrangementArticalModel_RoomsUpdate> lista = new List<ArrangementArticalModel_RoomsUpdate>();
                lista = new ArticalBUS().GetAllArticalsForUpdateArrangementAccomodation(arrange.idArrangement);

                if(lista !=null)
                    if (lista.Count > 0)
                    {
                        List<ArrangementArticalModel_RoomsUpdate> listaForDeleting = new List<ArrangementArticalModel_RoomsUpdate>();
                        listaForDeleting = lista.FindAll(s => s.nrArticle < 0);
                        if (listaForDeleting != null)
                            if (listaForDeleting.Count > 0)
                            {
                                for (int i = 0; i < listaForDeleting.Count; i++)
                                {
                                    if (new ArrangementRoomsBUS().checkifRoomCanBeDeleted(listaForDeleting[i], arrange.idArrangement) == false)
                                    {
                                        translateRadMessageBox tr = new translateRadMessageBox();
                                        tr.translateAllMessageBox("You can't delete  " + listaForDeleting[i].nameArticle + " on less then " + new ArrangementRoomsBUS().getNumberofBookedRooms(arrange.idArrangement, listaForDeleting[i].idArticle) + " because other rooms are booked!");
                                    }
                                    else
                                    {
                                        if(new ArrangementRoomsBUS().getNumberofBookedRooms(arrange.idArrangement, listaForDeleting[i].idArticle)<-listaForDeleting[i].nrArticle)
                                        {
                                            translateRadMessageBox tr = new translateRadMessageBox();
                                            tr.translateAllMessageBox("You delete  " + listaForDeleting[i].nameArticle + " on " + new ArrangementRoomsBUS().getNumberofBookedRooms(arrange.idArrangement, listaForDeleting[i].idArticle) + " rooms because other rooms are booked!");
                                 
                                        }
                                    }

                                }
                            }

                        if (new ArrangementRoomsBUS().UpdateArrangamentRooms(lista, arrange.idArrangement, this.Name, Login._user.idUser) == false)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Something went wrong with updating");
                        }
                        else
                            loadRooms();
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("There is no rooms for update!");
                    }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("There is no rooms for update!");
                }
                
            }
            else
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("First you need to save arrangement");
            }
        }

        private void btnClearClientInvoice_Click(object sender, EventArgs e)
        {
            txtClientInvoice.Text = "";
            arrange.idClientInvoice = 0;
            txtClientInvoice.Focus();     
        }

        private void chkRecalculation_CheckStateChanging(object sender, CheckStateChangingEventArgs args)
        {
            if (Login._user.isFinishCalculation == false && isFinished == false)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You are not autorized to do that.");
                args.Cancel = true;

            }
        }

        // restriction for arrows Goran 23.8.2016
        #region arrows restriction
        private void maskedNrTravelers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

        private void maskedMinNrTravelers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

        private void maskedNrVoluntary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

        private void maskedNrMaleVoluntary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

        private void numMinNumberTravelers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

        private void numNrVoluntary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

        private void maskedVolDays_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

        private void numSurcharge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

        private void numDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

        private void numGroupMoney_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }      

        private void numtxtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

        private void maskedDiverseCorrection_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

        private void maskedNrMaximumWheelchairs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

        private void maskedWhooseWheelchairs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

        private void maskedArms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

        private void maskedAnchorage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

        private void txtdaydFirstPayment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

        private void txtDaysLastPayment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

        private void txtFrstpaymentPercent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

        private void maskedReservationCosts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }

        #endregion

        //Mitar i Aleksa Leave
    }

    // sakriva filter na grid koloni
    public class CustomFilterCell : GridFilterCellElement
    {
        public CustomFilterCell(GridViewDataColumn column, GridRowElement row)
            : base(column, row)
        {
        }

        protected override void CreateChildElements()
        {
            base.CreateChildElements();
            this.Children.Clear();
        }
    }
}
