﻿using System;
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
using Microsoft;
using Telerik.WinControls.UI;
using System.Linq;
using System.Threading;
using System.IO;




namespace GUI
{
    public partial class frmLineEntry : Telerik.WinControls.UI.RadForm
    {
        public string xSideBooking;      // save account from Daily
        public string xSideBookingSplit = ""; // save account for first split line
        public bool manDebitor = false;
        public bool manCreditor = false;
        public bool manCost = false;
        public bool manProject = false;
        public bool manBTW = false;
        public AccLineModel linesmodel;
        public AccOpenLinesModel olmodel;
        private int iID = -1;
        public bool modelChanged = true;
        public bool isOk = false;
        public int xDaily = -1;
        private List<AccLineModel> multimodel;
        List<AccLineModel> listlines;
        AccLineModel model;
        AccLineModel model1;
        AccLineModel model2;
        AccLineModel model3;
        AccLineModel model4;
        AccLineModel model5;
        public int xDailyType;
        public decimal dtot = 0;
        public decimal ctot = 0;
        public decimal adiff = 0;
        private decimal btwPercent = 0;
        private decimal btwtype;
        private decimal debcreAmt;
        private decimal btwAmt;
        private string xConto;   // account Daily
        private string xContoSplit = ""; // account for first split line 
        private string xBtwConto;
        public bool isSuccessfully = false;
        private decimal debTotal = 0;
        private decimal creTotal = 0;
        private decimal totalLines = 0;
        public string gClient;
        private string xcodeBTW = "";
        private string xIncopNr;
        private bool splitClicked = false;
        private bool blcheck = false;
        private bool lledit = false;
        private bool bsplit = true;
        private DateTime xdtProjectStart = Convert.ToDateTime("1900-01-01");
        public AccLineModel linesmodelR;
        private AccAcountUpdate acUpdate;
        private List<AccLineModel> listOldlines;
        public AccLineModel oldlinesmodel;
        public AccSettingsBUS asb;
        public AccSettingsModel asm;
        public bool hasBTW = false;
        private int pc;
        bool isClose = false;
        private string creditReservation;
        private string layoutLineEntry;

        BaseGridEditor _gridEditor;

        public frmLineEntry()
        {
            InitializeComponent();
            this.Icon = Login.iconForm;
        }


        public frmLineEntry(int iID, int idDaily)
        {
            iID = -1;
            xDaily = idDaily;
            InitializeComponent();
            getIncopNr();
            this.Icon = Login.iconForm;
        }

        public frmLineEntry(int ixID, int idDaily, string em)
        {
            linesmodel = model;
            iID = ixID;
            xDaily = idDaily;
            InitializeComponent();
            this.Icon = Login.iconForm;
        }



        private void frmLineEntry_Load(object sender, EventArgs e)
        {
            this.txtAmountD.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.txtAmountD.Mask = "N2";

            this.txtAmountC.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.txtAmountC.Mask = "N2";
            labelName.Text = "";
            //txtDateStatement.Text = "";
            //txtStatement.Text = "";
            //txtEndSaldo.Text = "";
            Translation();
            labelProject1.Text = "";
            labelCost.Text = "";
            labelProject.Text = "";
            txtArrdate.Text = "";
            labelClient.Text = "";
            labelKonto.Text = "";
            labelBtw.Text = "";
            labelBasicKonto.Text = "";

            layoutLineEntry = MainForm.gridFiltersFolder + "\\layoutLineEntry.xml";

            txtAmountC.MaskedEditBoxElement.EnableMouseWheel = false;
            txtAmountD.MaskedEditBoxElement.EnableMouseWheel = false;
            //txtTcredit.Text = "0";
            //txtTdebit.Text = "0";
            //txtTdiff.Text = "0";
            //AccAcountUpdate a = new AccAcountUpdate();  // ovo je bio test zaa brojac faktura
            //int inv;
            //inv = a.InvoiceNr();

            //==========================Telerikov code za masked polja ======================
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("nl-NL");
            txtAmountD.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            txtAmountD.Mask = "N2";
            txtAmountD.Culture = new System.Globalization.CultureInfo("nl-NL");

            this.txtAmountD.KeyUp += txtAmountD_KeyUp;


            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("nl-NL");
            txtAmountC.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            txtAmountC.Mask = "N2";
            txtAmountC.Culture = new System.Globalization.CultureInfo("nl-NL");

            this.txtAmountC.KeyUp += txtAmountC_KeyUp;

            //================================================


            // Read parameters
            asb = new AccSettingsBUS();
            asm = new AccSettingsModel();
            asm = asb.GetSettingsByID(Login._bookyear);    //DateTime.Now.Year.ToString()
            if (asm != null)
            {
                creditReservation = asm.defReservationAcc;
                if (creditReservation == "")
                    creditReservation = "1610";


                if (asm.isVat == false)
                {
                    lblBtw.Visible = false;
                    txtBtwCode.Visible = false;
                    labelBtw.Visible = false;
                    btnBtw.Visible = false;
                }
            }
            

            if (xDaily != -1)
            {
                //======== cita Daily da pokupi konto i vrstu naloga ====
                AccDailyBUS dyb = new AccDailyBUS(Login._bookyear);
                AccDailyModel dym = new AccDailyModel();
                dym = dyb.GetDailysById(xDaily);
                if (dym != null)
                    xConto = dym.numberLedgerAccount;
                if (dym.idDailyType != null)
                    xDailyType = Convert.ToInt32(dym.idDailyType);
            }


            if (iID != -1)
            {
                //=========== cita stavku ================
                linesmodel = new AccLineModel();
                oldlinesmodel = new AccLineModel();
                AccLineBUS bus = new AccLineBUS(Login._bookyear);
                linesmodel = bus.GetLine(iID);
                oldlinesmodel = bus.GetLine(iID);

               // linesmodelR = bus.GetAllLinesByNumber(linesmodelR.incopNr,0);

                xDaily = linesmodel.idAccDaily;
                if (linesmodel == null)
                {
                    RadMessageBox.Show("Error !! Can't read record ");
                    return;
                }
                if (linesmodel.statusLine == true)
                {
                    btnOK.Enabled = false;
                    btnSplit.Enabled = false;
                    gridLines.AllowEditRow = false;
                }
                //==== posltavlja indikator da je edit u pitanju da bi mogao da uradi save
                lledit = true;

                //======== ako je u stavci upisan konto uzima njega, ako ne konto Daily-a
                string mDaily;
                if (linesmodel.numberLedAccount == "")
                {
                    mDaily = xConto;
                }
                else
                {
                    mDaily = linesmodel.numberLedAccount;
                }


                LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lam = new LedgerAccountModel();

                lam = ledbus.GetAccount(mDaily, Login._bookyear);
                labelBasicKonto.Text = lam.numberLedgerAccount+" "+lam.descLedgerAccount;
                string aSide = lam.sideBooking;
                xSideBooking = lam.sideBooking;
               // txtAccount.Text = lam.numberLedgerAccount; //linesmodel.numberLedAccount.ToString();
               // labelKonto.Text = lam.descLedgerAccount;
                // === kupi sta je obavezno za unos ===
                manCreditor = lam.mandatoryCreditorAccount;
                manDebitor = lam.mandatoryDebitorAccount;
                manCost = lam.mandatoryCostAccount;
                manProject = lam.mandatoryProjectAccount;
                manBTW = lam.isBTWLedgerAccount;
                //=======================================
                if (linesmodel.incopNr != null)
                    txtIncop.Text = linesmodel.incopNr.ToString();
                txtDate.Text = linesmodel.dtLine.ToString();
                if (linesmodel.dtBooking != null)
                {
                    dpValutaDate.Text = linesmodel.dtBooking.ToString();
                    if (linesmodel.dtLine < linesmodel.dtBooking)
                    {
                        txtPayingDays.Text = (Convert.ToDateTime(linesmodel.dtBooking) - Convert.ToDateTime(linesmodel.dtLine)).Days.ToString();
                    }
                }
                //   if (aSide == "D")
                txtAmountD.Text = linesmodel.debitLine.ToString();
                //  if (aSide == "C")
                txtAmountC.Text = linesmodel.creditLine.ToString();
                if (linesmodel.invoiceNr != null)
                    txtInvoice.Text = linesmodel.invoiceNr.ToString();
                if (linesmodel.descLine != null)
                    txtDesc.Text = linesmodel.descLine.ToString();
                if (linesmodel.incopNr != null)
                    txtIncop.Text = linesmodel.incopNr.ToString();
                if (linesmodel.idClientLine != "" && linesmodel.idClientLine != null)
                    txtClient.Text = linesmodel.idClientLine;
                {
                    AccDebCreBUS adcbus = new AccDebCreBUS();
                    AccDebCreModel cam1 = new AccDebCreModel();
                    cam1 = adcbus.GetCustomerByAccCode(linesmodel.idClientLine);
                    if (cam1 != null)
                    {

                        if (cam1.nameClient == null)
                        {
                            labelClient.Text = cam1.namePerson;
                            
                        }
                        else
                        {
                            labelClient.Text = cam1.nameClient;
                        }
                    }

                }
                // prikazuje total
                dtot = linesmodel.debitLine;
                txtTdebit.Text = dtot.ToString();
                ctot = linesmodel.creditLine;
                txtTcredit.Text = ctot.ToString();
                adiff = dtot - ctot;
                txtTdiff.Text = adiff.ToString();

                //


                //==== cost ======
                if (linesmodel.idCostLine != "" && linesmodel.idCostLine != null)
                {
                    txtCost.Text = linesmodel.idCostLine.ToString();

                    AccCostBUS adcbus1 = new AccCostBUS();
                    AccCostModel cam2 = new AccCostModel();
                    cam2 = adcbus1.GetCostByID(linesmodel.idCostLine.ToString());
                    if (cam2 != null)
                        labelCost.Text = cam2.descCost;
                }
                //======= project  ===========
                if (linesmodel.idProjectLine != "" && linesmodel.idProjectLine != null)
                {
                    txtProject.Text = linesmodel.idProjectLine.ToString();
                    ArrangementBUS ccentar = new ArrangementBUS();
                    ArrangementModel gmX = new ArrangementModel();

                    gmX = ccentar.GetArrangementByCode(linesmodel.idProjectLine);
                    if (gmX != null)
                    {
                        labelProject.Text = gmX.nameArrangement;
                        txtArrdate.Text = "Start " + Convert.ToString(gmX.dtFromArrangement) + " End  " + Convert.ToString(gmX.dtToArrangement);
                    }
                    if (gmX.dtFromArrangement != null)
                        xdtProjectStart = Convert.ToDateTime(gmX.dtFromArrangement);
                }
                //======== btw ==============
                if (linesmodel.idBTW != 0 && linesmodel.idBTW != null)
                {
                    AccTaxBUS porez = new AccTaxBUS();
                    AccTaxModel pm = new AccTaxModel();
                    pm = porez.GetTaxByID(Convert.ToInt32(linesmodel.idBTW));
                    txtBtwCode.Text = pm.idTax.ToString();
                    labelBtw.Text = pm.descTax;
                    if (pm.typeTax != null)
                        btwtype = Convert.ToDecimal(pm.typeTax);
                    if (pm.numberLedAccount != null)
                        xBtwConto = pm.numberLedAccount;

                }
                //==============
                AccLineBUS lin2 = new AccLineBUS(Login._bookyear);
                multimodel = new List<AccLineModel>();
                listOldlines = new List<AccLineModel>();
            //    multimodel = lin2.GetAllLinesByinvoice(linesmodel.invoiceNr, 0);
                multimodel = lin2.GetAllLinesByNumberAutomatic(linesmodel.incopNr, 0);
                gridLines.DataSource = null;
                gridLines.DataSource = multimodel;
                listOldlines = lin2.GetAllLinesByNumberAutomatic(linesmodel.incopNr, 0);
                gridLines.Show();
                // totals();

            }
            else
            {

                // unosi novi slog =============
                txtDate.Text = DateTime.Now.ToShortDateString();
                linesmodel = new AccLineModel();
                multimodel = new List<AccLineModel>();
                gridLines.DataSource = null;
                gridLines.DataSource = multimodel;
                //AccDailyBUS dyb = new AccDailyBUS();
                //AccDailyModel dym = new AccDailyModel();
                //dym = dyb.GetDailysById(xDaily);
                //if (dym != null)
                //{
                //    xConto = dym.numberLedgerAccount;
                //}
                //else
                //{
                //    if (dym.idDailyType != null)
                //        xDailyType = Convert.ToInt32(dym.idDailyType);
                //}

                //======== ako je u stavci upisan konto uzima njega, ako ne konto Daily-a
                //string mDaily1;
                //if (linesmodel.numberLedAccount == "" || linesmodel.numberLedAccount == null)
                //{
                //    mDaily1 = xConto;
                //}
                //else
                //{
                //    mDaily1 = linesmodel.numberLedAccount;
                //}


                LedgerAccountBUS ledbus1 = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lam1 = new LedgerAccountModel();


                lam1 = ledbus1.GetAccount(xConto, Login._bookyear);
                string aSide = lam1.sideBooking;
                xSideBooking = lam1.sideBooking;
                xConto = lam1.numberLedgerAccount;
                // === kupi sta je obavezno za unos ===
                manCreditor = lam1.mandatoryCreditorAccount;
                manDebitor = lam1.mandatoryDebitorAccount;
                manCost = lam1.mandatoryCostAccount;
                manProject = lam1.mandatoryProjectAccount;
                manBTW = lam1.isBTWLedgerAccount;
                if (manBTW == true)
                {
                    txtBtwCode.Text = lam1.btwId.ToString();
                    AccTaxBUS porez = new AccTaxBUS();
                    AccTaxModel pm = new AccTaxModel();
                    pm = porez.GetTaxByID(Convert.ToInt32(txtBtwCode.Text));
                    labelBtw.Text = pm.descTax;
                    if (pm.typeTax != null)
                        btwtype = Convert.ToDecimal(pm.typeTax);
                    if (pm.numberLedAccount != null)
                        xBtwConto = pm.numberLedAccount;
                }
                //=======================================
                linesmodel.idAccDaily = xDaily;
                AccDailyBUS acdb = new AccDailyBUS(Login._bookyear);
                xDailyType = Convert.ToInt32(acdb.GetDailysById(linesmodel.idAccDaily).idDailyType);

            }



        }
        private void fillLines()
        {
            if (iID == -1)
                linesmodel.idAccDaily = xDaily;
            //====== brzo popunjavanje
            linesmodel.idClientLine = "";
            linesmodel.idPersonLine = "";
            linesmodel.idCostLine = "";

            linesmodel.periodLine = 0;
            linesmodel.statusLine = false;
            linesmodel.idCurrency = 0;
            linesmodel.idBTW = 0;
            linesmodel.booksort = 0;
            linesmodel.creditBTW = 0;
            linesmodel.debitBTW = 0;
            linesmodel.creditCurr = 0;
            linesmodel.debitCurr = 0;
            linesmodel.debitLine = 0;
            linesmodel.creditLine = 0;
            if (dpValutaDate.Value == DateTime.MinValue)
                dpValutaDate.Value = txtDate.Value.AddDays(Convert.ToInt32(txtPayingDays.Text));
            linesmodel.dtBooking = dpValutaDate.Value;             //DateTime.Now;
            linesmodel.currrate = 0;
            //========================

            AccAcountUpdate au = new AccAcountUpdate();
            linesmodel.numberLedAccount = xConto;
            //DateTime mper = Convert.ToDateTime(linesmodel.dtLine);
            linesmodel.periodLine = au.Period(Convert.ToDateTime(txtDate.Text)); //mper.Month;
            linesmodel.dtLine = Convert.ToDateTime(txtDate.Text);
            linesmodel.dtBooking = dpValutaDate.Value; 
            linesmodel.debitLine = Convert.ToDecimal(txtAmountD.Text);
            linesmodel.creditLine = Convert.ToDecimal(txtAmountC.Text);
            //AccAcountUpdate Nau = new AccAcountUpdate();
            //int per = Nau.Period(linesmodel.dtLine);
            //linesmodel.periodLine = per;
            linesmodel.invoiceNr = txtInvoice.Text;
            linesmodel.descLine = txtDesc.Text;


            if (txtClient.Text != "")
                linesmodel.idClientLine = txtClient.Text;

            if (txtCost.Text != "")
                linesmodel.idCostLine = txtCost.Text;
            //  if (txtProject.Text != "")
            linesmodel.idProjectLine = txtProject.Text;
            linesmodel.booksort = 1;
            if (txtBtwCode.Text != "")
                linesmodel.idBTW = Convert.ToInt32(txtBtwCode.Text);
            linesmodel.incopNr = txtIncop.Text;
            linesmodel.bookingYear = Login._bookyear;
           
            //============================ puni stavku za open line =================================
            olmodel = new AccOpenLinesModel();

            olmodel.invoiceOpenLine = linesmodel.invoiceNr;
            olmodel.descOpenLine = linesmodel.descLine;
            olmodel.idDebCre = linesmodel.idClientLine;
            olmodel.account = linesmodel.numberLedAccount;
            olmodel.codeCost = linesmodel.idCostLine;
            olmodel.debitOpenLine = linesmodel.debitLine;
            olmodel.creditOpenLine = linesmodel.creditLine;
            olmodel.periodOnenLines = linesmodel.periodLine;
            olmodel.dtOpenLine = linesmodel.dtLine;
            olmodel.codeArr = linesmodel.idProjectLine;
            olmodel.typeOpenLine = xSideBooking;
            olmodel.idProject = linesmodel.idProjectLine;
            olmodel.idPayCondition = 0;
            olmodel.discauntDays = 0;
            olmodel.creditDays = 0;
            olmodel.referencePay = linesmodel.incopNr;
            olmodel.idOption = 0;
            olmodel.bookingYear = Login._bookyear;
            //  model.idBTW = linesmodel.idBTW;
            // model.idProjectLine = linesmodel.idProjectLine;
            //  model.incopNr = linesmodel.incopNr;
            


        }

        private void radTextBox1_KeyPress(object sender, KeyPressEventHandler e)
        {

        }

        #region Field Controls

        private void txtAccount_Leave(object sender, EventArgs e)
        {
            AccAcountUpdate au = new AccAcountUpdate();
            labelKonto.Text = au.AccountName(txtAccount.Text);
            if (asm.isVat == true)
            {
                txtBtwCode.Focus();
            }
            else
            {
                txtAmountC.Focus();
            }
            //if (txtAccount.Text != "")
            //{
            //    LedgerAccountBUS ledbus = new LedgerAccountBUS();
            //    LedgerAccountModel lam = new LedgerAccountModel();

            //    lam = ledbus.GetAccount(txtAccount.Text);
            //    if (lam != null)
            //    {
            //        labelKonto.Text = lam.descLedgerAccount;
            //        xContoSplit = lam.numberLedgerAccount;
            //        //   xSideBooking = lam.sideBooking;
            //        txtBtwCode.Focus();
            //    }
            //}
        }
        //==============================================  kretanje sa Enterom ===========================================
        private void txtDate_KeyDown(object sender, KeyEventArgs e)
        {
           // string xidClient;
           // xidClient = "W0001";

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                //AccOpenLinesBUS olb = new AccOpenLinesBUS();
                //List<IModel> olm = new List<IModel>();
                //olm = olb.GetAccOpenLinesByID(xidClient);
                //var dlgSave = new OpenLookupForm(olm, "Pera", xidClient);

                //if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                //{
                //    AccOpenLinesModel pm1X = new AccOpenLinesModel();
                //    pm1X = (AccOpenLinesModel)dlgSave.selectedRow;
                //    //set textbox

               //}


                dpValutaDate.Focus();
                //txtInvoice.Focus();

            }
        }
        private void txtInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtDesc.Focus();
            }
        }

        private void txtDesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtClient.Focus();
            }
        }
        private void txtClient_KeyDown(object sender, KeyEventArgs e)
        {
            ClientBUS cb = new ClientBUS();
            ClientModel cm = new ClientModel();
            PersonBUS pbs = new PersonBUS();
            PersonModel pmd = new PersonModel();

            if (e.KeyCode == Keys.F2)
            {
                DebCreLookupBUS debpers = new DebCreLookupBUS();
                List<IModel> pm1 = new List<IModel>();

                pm1 = debpers.GetCreditors();
                var dlgSave = new GridLookupForm(pm1, "Creditor");

                if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                {
                    DebCreLookupModel pm1X = new DebCreLookupModel();
                    pm1X = (DebCreLookupModel)dlgSave.selectedRow;
                    //set textbox
                    txtClient.Text = pm1X.accNumber;
                    gClient = pm1X.accNumber;
                    labelClient.Text = pm1X.name;
                    AccDebCreBUS dcb = new AccDebCreBUS();
                    AccDebCreModel dcm = new AccDebCreModel();
                    dcm = dcb.GetCustomerByAccCode(pm1X.accNumber);

                    if (dcm != null && dcm.payCondition != 0)
                    {
                        AccPaymentBUS apb = new AccPaymentBUS();
                        AccPaymentModel apm = new AccPaymentModel();
                        apm = apb.GetPaymentByID(dcm.payCondition);
                        if (apm != null)
                            txtPayingDays.Value = apm.numberDays;
                    }
                    txtPayingDays.Focus();
                    //txtAccount.Focus();
                }
                //int xX = this.Location.X;
                //int yY = this.Location.Y;
                //debcreCMenu.Show(xX + 220, yY + 100);
               // txtClient.Focus();
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtClient.Text != "")
                {
                    AccDebCreBUS debpers = new AccDebCreBUS();
                    AccDebCreModel pm1 = new AccDebCreModel();


                    pm1 = debpers.GetCustomerByAccCode(txtClient.Text.ToString());
                    if (pm1 != null)
                    {
                        if (pm1.idClient != null && pm1.idContPerson == 0)
                        {
                            cm = cb.GetClient(pm1.idClient);
                            if (cm != null)
                            {
                                txtClient.Text = cm.accountCodeClient;
                                gClient = cm.accountCodeClient;
                                labelClient.Text = cm.nameClient;
                                txtAccount.Focus();
                            }
                            else
                            {
                                txtClient.Text = "";
                                txtClient.Focus();
                            }
                        }
                        else
                        {
                            if (pm1.idContPerson != null && pm1.idClient == 0)
                                pmd = pbs.GetPerson(pm1.idContPerson);
                            if (pmd != null)
                            {
                                labelClient.Text = pmd.firstname + " " + pmd.midname + " " + pmd.lastname;
                                txtAccount.Focus();
                            }
                            else
                            {
                                txtClient.Text = "";
                                txtClient.Focus();
                            }
                           // txtClient.Focus();
                        }
                        txtAccount.Focus();
                    }
                    txtAccount.Focus();
                }
                txtAccount.Focus();
            }


        }
        private void txtAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                LedgerAccountBUS ccentar = new LedgerAccountBUS(Login._bookyear);
                List<IModel> gmX = new List<IModel>();

                gmX = ccentar.GetAllAccounts();
                var dlgSave = new GridLookupForm(gmX, "Ledger");

                if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                {
                    LedgerAccountModel genmX = new LedgerAccountModel();
                    genmX = (LedgerAccountModel)dlgSave.selectedRow;
                    //set textbox
                    txtAccount.Text = genmX.numberLedgerAccount;
                    xContoSplit = genmX.numberLedgerAccount;
                    labelKonto.Text = genmX.descLedgerAccount;
                    // xSideBooking = genmX.sideBooking;
                   // txtBtwCode.Focus();
                    if (asm.isVat == true)
                    {
                        txtBtwCode.Focus();
                    }
                    else
                    {
                        txtAmountC.Focus();
                    }
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {

                if (txtAccount.Text != "")
                {
                    LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = ledbus.GetAccount(txtAccount.Text, Login._bookyear);
                    if (lam != null)
                    {
                        if (lam.isActiveLedgerAccount == true)
                        {
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {
                                if (resxSet.GetString("Sorry, this account is not active!") != null)
                                    RadMessageBox.Show(resxSet.GetString("Sorry, this account is not active!"));
                                else
                                    RadMessageBox.Show("Sorry, this account is not active!");
                            }
                            txtAccount.Text = "";
                            txtAccount.Focus();
                            return;
                        }
                        labelKonto.Text = lam.descLedgerAccount;
                        xContoSplit = lam.numberLedgerAccount;
                        //   xSideBooking = lam.sideBooking;
                        //txtBtwCode.Focus();
                        if (asm.isVat == true)
                        {
                            txtBtwCode.Focus();
                        }
                        else
                        {
                            txtAmountC.Focus();
                        }
                    }
                    else
                    {
                        RadMessageBox.Show("Wrong accont");
                        txtAccount.Text = "";
                        labelKonto.Text = "";
                        //txtAccount.Focus();
                    }
                }
                else
                {
                    if (asm.isVat == true)
                    {
                        txtBtwCode.Focus();
                    }
                    else
                    {
                        txtAmountC.Focus();
                    }
                    //txtBtwCode.Focus();
                }

            }
            // txtBtwCode.Focus();
        }
        private void txtBtwCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                AccTaxBUS ccentar = new AccTaxBUS();
                List<IModel> gmX = new List<IModel>();

                gmX = ccentar.GetAllTax(Login._user.lngUser);
                var dlgSave = new GridLookupForm(gmX, "Type");

                if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                {
                    AccTaxModel genmX = new AccTaxModel();
                    genmX = (AccTaxModel)dlgSave.selectedRow;
                    //set textbox
                    txtBtwCode.Text = genmX.idTax.ToString();
                    labelBtw.Text = genmX.descTax;
                    xcodeBTW = genmX.codeTax;
                    xBtwConto = genmX.numberLedAccount;
                    if (genmX.typeTax != 0 && genmX.typeTax != null)
                        btwtype = Convert.ToDecimal(genmX.typeTax);

                    if (xSideBooking == "D")
                        txtAmountD.Focus();
                    if (xSideBooking == "C")
                        txtAmountC.Focus();
                }
                //if (xSideBooking == "D")
                //    txtAmountD.Focus();
                //if (xSideBooking == "C")
                //    txtAmountC.Focus();
            }
            else
            {
                
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    if (txtBtwCode.Text != "")
                    {
                        AccTaxBUS ccentar8 = new AccTaxBUS();
                        AccTaxModel gmX8 = new AccTaxModel();
                        int xidBtw = 0;
                        bool aContr = int.TryParse(txtBtwCode.Text, out xidBtw);
                        if (aContr == false)
                        {
                            RadMessageBox.Show("Only digit ...");
                            labelBtw.Text = "";
                            txtBtwCode.Text = "";
                            txtBtwCode.Focus();
                        }
                        else
                        {

                            gmX8 = ccentar8.GetTaxByID(Convert.ToInt32(xidBtw));
                            if (gmX8 != null)
                            {
                                labelBtw.Text = gmX8.descTax;
                                xcodeBTW = gmX8.codeTax;
                                xBtwConto = gmX8.numberLedAccount;
                                if (gmX8.typeTax != 0 && gmX8.typeTax != null)
                                    btwtype = Convert.ToDecimal(gmX8.typeTax);

                                if (xSideBooking == "D")
                                {
                                    txtAmountD.Focus();
                                }
                                else
                                {
                                    if (xSideBooking == "C")
                                    {
                                        txtAmountC.Focus();
                                    }
                                    else
                                    {
                                        txtAmountC.Focus();
                                    }
                                }
                            }
                            else
                            {
                                labelBtw.Text = "";
                                txtBtwCode.Text = "";
                                RadMessageBox.Show("Wrong BTW code");
                                txtBtwCode.Focus();
                            }
                        }
                        //if (xSideBooking == "D")
                        //    txtAmountD.Focus();
                        //if (xSideBooking == "C")
                        //    txtAmountC.Focus();
                    }
                    else
                    {
                        labelKonto.Text = "";
                        if (xSideBooking == "D")
                            txtAmountD.Focus();
                        if (xSideBooking == "C")
                            txtAmountC.Focus();
                    }
                }

            }
        }
        private void txtAmountD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtCost.Focus();
            }

            if(e.KeyCode==Keys.Up || e.KeyCode==Keys.Down)
            {
                e.Handled = true;
            }
        }
        private void txtAmountC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtCost.Focus();
            }

            if(e.KeyCode==Keys.Up || e.KeyCode==Keys.Down)
            {
                e.Handled = true;
            }

        }

        private void txtCost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtCost.Text != "")
                {
                    AccCostBUS acc = new AccCostBUS();
                    AccCostModel amc = new AccCostModel();
                    amc = acc.GetCostByID(txtCost.Text);
                    if (amc != null)
                    {
                        labelCost.Text = amc.descCost;
                        txtProject.Focus();
                    }
                    else
                    {
                        labelCost.Text = "";
                        txtCost.Text = "";
                        txtCost.Focus();
                    }
                }
                else
                {
                    txtProject.Focus();
                }

            }
            else
            {
                if (e.KeyCode == Keys.F2)
                {
                    AccCostBUS ccentar = new AccCostBUS();
                    List<IModel> gmX = new List<IModel>();

                    gmX = ccentar.GetAllCost();
                    var dlgSave = new GridLookupForm(gmX, "Cost");

                    if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                    {
                        AccCostModel genmX = new AccCostModel();
                        genmX = (AccCostModel)dlgSave.selectedRow;
                        //set textbox
                        txtCost.Text = genmX.codeCost;
                        labelCost.Text = genmX.descCost;

                        txtProject.Focus();
                    }
                }
            }

        }
        private void txtProject_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtProject.Text != "")
                {
                    ArrangementBUS arb = new ArrangementBUS();
                    ArrangementModel arm = new ArrangementModel();
                    arm = arb.GetArrangementByCode(txtProject.Text);
                    if (arm != null)
                    {
                        labelProject.Text = arm.nameArrangement;
                        txtArrdate.Text = "Start " + Convert.ToString(arm.dtFromArrangement) + " End " + Convert.ToString(arm.dtToArrangement);
                        if (arm.dtFromArrangement != null)
                            xdtProjectStart = Convert.ToDateTime(arm.dtFromArrangement);
                        if (btnSplit.Enabled == true)
                        {
                            btnSplit.Focus();
                        }
                        else
                        {
                            btnOK.Focus();
                        }
                    }
                    else
                    {
                        txtProject.Text = "";
                        labelProject.Text = "";
                        txtArrdate.Text = "";
                        txtProject.Focus();
                    }

                }
                else
                {
                    if (btnSplit.Enabled == true)
                        btnSplit.Focus();
                    else
                        btnOK.Focus();
                }
            }
            else
                if (e.KeyCode == Keys.F2)
                {
                    ArrangementBUS ccentar = new ArrangementBUS();
                    List<IModel> gmX = new List<IModel>();

                    gmX = ccentar.GetAllArrangementsAccount(Login._bookyear);
                    var dlgSave = new GridLookupForm(gmX, "Arrangement");

                    if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                    {
                        ArrangementModel genmX = new ArrangementModel();
                        genmX = (ArrangementModel)dlgSave.selectedRow;
                        //set textbox
                        txtProject.Text = genmX.codeProject;
                        labelProject.Text = genmX.nameArrangement;
                        txtArrdate.Text = "Start " + Convert.ToString(genmX.dtFromArrangement) + " End " + Convert.ToString(genmX.dtToArrangement);
                        if (genmX.dtFromArrangement != null)
                            xdtProjectStart = Convert.ToDateTime(genmX.dtFromArrangement);
                    }
                }
        }

        private void txtAmountC_Leave(object sender, EventArgs e)
        {
            dtot = Decimal.Parse(txtAmountD.Text.ToString());
            txtTdebit.Text = dtot.ToString();
            ctot = Decimal.Parse(txtAmountC.Text.ToString());
            txtTcredit.Text = ctot.ToString();
            totals();
            //adiff = dtot - ctot;
            //txtTdiff.Text = adiff.ToString();
            txtCost.Focus();

        }

        private void txtAmountD_Leave(object sender, EventArgs e)
        {
            dtot = Decimal.Parse(txtAmountD.Text.ToString());
            txtTdebit.Text = dtot.ToString();
            ctot = Decimal.Parse(txtAmountC.Text.ToString());
            txtTcredit.Text = ctot.ToString();
            totals();
            //adiff = dtot - ctot;
            //txtTdiff.Text = adiff.ToString();
            txtCost.Focus();
        }
        #endregion Field Controls

        //================================================================================================================
        private void radButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        # region Save
        private void btnOK_Click(object sender, EventArgs e)
        {

            if (txtDate.Value.Year.ToString() != Login._bookyear)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Booking year NOT same as date !!");
                txtDate.Focus();
                return;
            }


            checkGriddata();
            AccOpenLinesBUS olb = new AccOpenLinesBUS();
            if (lledit == true)  // ovo je da bi mogao da prodje na save kad etituje stavke
                splitClicked = true;
            if (splitClicked == true && multimodel.Count > 0 && Convert.ToDecimal(txtTdiff.Text) == 0 && blcheck == true)
            {
                AccLineBUS ptb = new AccLineBUS(Login._bookyear);
                
                fillLines();
                // AccLineBUS aclbus = new AccLineBUS();
                if (iID != -1)
                {
                    // update open lines ...
                    AccAcountUpdate ilm = new AccAcountUpdate();

                    if (linesmodel.invoiceNr != null && linesmodel.invoiceNr != "")
                    {
                        isClose = ilm.CheckOpenLines(linesmodel.invoiceNr);

                    }
                    if (isClose == true) // ne moze da se menja jer je stavka zatvorena
                    {
                        openLinesUpdate();
                        //=================

                        AccAcountUpdate aaU = new AccAcountUpdate();
                        isSuccessfully = aaU.SubstractAmount(oldlinesmodel, this.Name, Login._user.idUser);

                        isOk = ptb.Update(linesmodel, this.Name, Login._user.idUser);
                        if (isOk == true)
                        {
                            RadMessageBox.Show("Updated");
                            isSuccessfully = aaU.AddAmount(linesmodel, this.Name, Login._user.idUser);

                            iID = 0;
                        }
                        else
                        {
                            RadMessageBox.Show("Error updating line");

                        }

                        if (multimodel != null)
                        {

                            AccAcountUpdate aaU1 = new AccAcountUpdate();
                            if (listOldlines != null)
                            {
                                for (int js = 0; js < listOldlines.Count; js++)
                                {
                                    isSuccessfully = aaU1.SubstractAmount(listOldlines[js], this.Name, Login._user.idUser);
                                }
                            }
                            ptb.DeleteByReference(linesmodel.incopNr, this.Name, Login._user.idUser);
                            saveItems();
                            for (int jm = 0; jm < listlines.Count; jm++)
                            {
                                if (ptb.Save(listlines[jm], this.Name, Login._user.idUser) == true)
                                {

                                    isSuccessfully = true;
                                    isSuccessfully = aaU1.AddAmount(listlines[jm], this.Name, Login._user.idUser);
                                    // RadMessageBox.Show("Saved lines ");
                                }
                                else
                                {
                                    RadMessageBox.Show("You have NOT successfully save lines " + (jm + 1).ToString());
                                }
                            }
                        }
                    }
                    else
                    {
                        RadMessageBox.Show("Can'update line, open line are closed ! ");
                    }
                }
                else
                {
                    isOk = ptb.Save(linesmodel, this.Name, Login._user.idUser);
                    if (isOk == true)
                    {
                        RadMessageBox.Show("Saved");
                        AccAcountUpdate acUpdate = new AccAcountUpdate();
                        isSuccessfully = acUpdate.AddAmount(linesmodel, this.Name, Login._user.idUser);
                    }
                    else
                    {
                        RadMessageBox.Show("Error saving line");

                    }

                    if (multimodel != null)
                    {
                        saveItems();
                        for (int jm = 0; jm < listlines.Count; jm++)
                        {
                            if (ptb.Save(listlines[jm], this.Name, Login._user.idUser) == true)
                            {
                                isSuccessfully = true;
                                AccAcountUpdate acUpdate = new AccAcountUpdate();
                                isSuccessfully = acUpdate.AddAmount(listlines[jm], this.Name, Login._user.idUser);
                                //    RadMessageBox.Show("Saved lines ");
                            }
                            else
                            {
                                RadMessageBox.Show("You have not successfully save lines " + (jm + 1).ToString());
                            }
                        }
                    }
                    //==========  ubacuje broj dana za placanje ===
                    int daysp = 0;
                    AccDebCreBUS dbb = new AccDebCreBUS();
                    AccDebCreModel dbm = new AccDebCreModel();
                    AccPaymentBUS apb = new AccPaymentBUS();
                    AccPaymentModel apm = new AccPaymentModel();
                    dbm = dbb.GetCustomerByAccCode(linesmodel.idClientLine);
                    if (dbm != null)
                    {
                        if (dbm.payCondition != null && dbm.payCondition != 0)
                        {
                            pc = dbm.payCondition;
                        }
                        else
                        {
                            pc = Convert.ToInt32(asm.defPayCondition);
                        }
                        apm = apb.GetPaymentByID(pc);
                        if (apm != null)
                        {
                            if (apm.numberDays != null && apm.numberDays != 0)
                                daysp = Convert.ToInt32(apm.numberDays);
                            else
                                daysp = 0;
                        }
                        if (Convert.ToInt32(txtPayingDays.Value) != 0)
                            olmodel.creditDays = Convert.ToInt32(txtPayingDays.Value);
                        else
                            olmodel.creditDays = daysp;
                        olmodel.idPayCondition = pc;
                    }
                    //
                    olmodel.dtCreationLine = Convert.ToDateTime(txtDate.Text);
                    if (dpValutaDate.Text != "")
                       olmodel.dtOpenLine = Convert.ToDateTime(dpValutaDate.Text);
                    isOk = olb.Save(olmodel, this.Name, Login._user.idUser);
                    if (isOk == false)
                    {
                        RadMessageBox.Show("Error writitng Open line!");
                    }

                }

                clearControl();
            }
            else
            {
                RadMessageBox.Show("No conditions for save");
                txtInvoice.Focus();
            }
      


        }
        #endregion Save
        private void clearControl()
        {
            // kontrole na formi
            txtInvoice.Text = "";
            txtDesc.Text = "";
            txtIncop.Text = "";
            txtClient.Text = "";
        //    txtStatement.Text = "";
            txtAccount.Text = "";
        //    txtDateStatement.Text = "";
            txtBtwCode.Text = "";
        //    txtBeginSaldo.Text = "";
            txtAmountC.Text = "0,00";
            txtAmountD.Text = "0,00";
            txtCost.Text = "";
        //    txtEndSaldo.Text = "";
            txtProject.Text = "";
            labelProject1.Text = "";
            txtArrdate1.Text = "";
            txtTcredit.Text = "0,00";
            txtTdebit.Text = "0,00";
            txtTdiff.Text = "0,00";
            txtPayingDays.Value = 0;
            dpValutaDate.Text = "";
            // promenjive i modeli
            this.gridLines.DataSource = null;
             manDebitor = false;
         manCreditor = false;
         manCost = false;
         manProject = false;
         manBTW = false;
        AccLineModel linesmodel;
         iID = -1;
         modelChanged = true;
        isOk = false;
        // xDaily = -1;
        List<AccLineModel> multimodel;
        List<AccLineModel> listlines;
        AccLineModel model;
        AccLineModel model1;
         xDailyType=0;
         dtot = 0;
         ctot = 0;
         adiff = 0;
         btwPercent = 0;
         btwtype=0;
         debcreAmt=0;
        btwAmt=0;
        xConto="";   // account Daily
        xContoSplit = ""; // account for first split line 
        xBtwConto="";
        isSuccessfully = false;
        debTotal = 0;
        creTotal = 0;
        totalLines = 0;
        gClient="";
        xcodeBTW = "";
        xIncopNr="";
        splitClicked = false;
        blcheck = false;
        lledit = false;
        bsplit = true;
        xdtProjectStart = Convert.ToDateTime("1900-01-01");
        getIncopNr();


        if (xDaily != -1)
         {
            //======== cita Daily da pokupi konto i vrstu naloga ====
             AccDailyBUS dyb = new AccDailyBUS(Login._bookyear);
            AccDailyModel dym = new AccDailyModel();
            dym = dyb.GetDailysById(xDaily);
            if (dym != null)
                xConto = dym.numberLedgerAccount;
            if (dym.idDailyType != null)
                xDailyType = Convert.ToInt32(dym.idDailyType);
        }
        btnSplit.Enabled = true;
        labelBtw.Text = "";
        labelKonto.Text = "";
        labelBasicKonto.Text = "";
        labelClient.Text = "";
        labelCost.Text = "";
        labelProject.Text = "";
        txtArrdate.Text = "";
        txtAmountC.Text = "0,00";
        txtAmountD.Text = "0,00";
        txtTcredit.Text = "0,00";
        txtTdebit.Text = "0,00";
        txtTdiff.Text = "0,00";


       

        }


        private void frmLineEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
        }
        
        private void gridLines_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < gridLines.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (gridLines.Columns[i].HeaderText != null && resxSet.GetString(gridLines.Columns[i].HeaderText) != null)
                        gridLines.Columns[i].HeaderText = resxSet.GetString(gridLines.Columns[i].HeaderText);
                }
            }
            if (asm.isVat == false)
            {
                gridLines.Columns["btw"].IsVisible = false;
            }
            //if (gridLines.Columns != null && gridLines.Columns.Count > 0)
            //    gridLines.Columns["dtLine"].FormatString = "{0: dd/MM/yyyy}";

            if(gridLines!=null)
            {
                if(File.Exists(layoutLineEntry))
                {
                    gridLines.LoadLayout(layoutLineEntry);
                }
            }
        }

        private void debitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DebCreLookupBUS debpers = new DebCreLookupBUS();
            List<IModel> pm1 = new List<IModel>();

            pm1 = debpers.GetDebitors();
            var dlgSave = new GridLookupForm(pm1, "Debitor");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                DebCreLookupModel pm1X = new DebCreLookupModel();
                pm1X = (DebCreLookupModel)dlgSave.selectedRow;
                //set textbox
                txtClient.Text = pm1X.accNumber;
                gClient = pm1X.accNumber;
                labelClient.Text = pm1X.name;
                txtAccount.Focus();
            }
        }

        private void creditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DebCreLookupBUS debpers = new DebCreLookupBUS();
            List<IModel> pm1 = new List<IModel>();

            pm1 = debpers.GetCreditors();
            var dlgSave = new GridLookupForm(pm1, "Creditor");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                DebCreLookupModel pm1X = new DebCreLookupModel();
                pm1X = (DebCreLookupModel)dlgSave.selectedRow;
                //set textbox
                txtClient.Text = pm1X.accNumber;
                gClient = pm1X.accNumber;
                labelClient.Text = pm1X.name;
                txtAccount.Focus();
            }
        }

        private void contextMenuStrip1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) 
            {
                debcreCMenu.Show();
            }
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ledbus1 = new LedgerAccountBUS(Login._bookyear);
            LedgerAccountModel lam1 = new LedgerAccountModel();

            if (txtAccount.Text != "")
            {
                lam1 = ledbus1.GetAccount(txtAccount.Text, Login._bookyear);
                // === kupi sta je obavezno za unos ===
                manCreditor = lam1.mandatoryCreditorAccount;
                manDebitor = lam1.mandatoryDebitorAccount;
                manCost = lam1.mandatoryCostAccount;
                manProject = lam1.mandatoryProjectAccount;
                manBTW = lam1.isBTWLedgerAccount;
            }
            // ponovna kontrola
            splitClicked = true;
            bsplit = true;

            if (Convert.ToDecimal(txtAmountC.Text) != 0 && Convert.ToDecimal(txtAmountD.Text) != 0)
            {
                RadMessageBox.Show("Check amounts, please!");
                bsplit = false;
                txtAmountC.Focus();
            }
            else
            {
                if (txtAccount.Text == "")
                {
                    RadMessageBox.Show("Can't save without Account !!!");
                    bsplit = false;
                    txtAccount.Focus();
                }
                else
                {
                    if (txtClient.Text == "" && manCreditor == true)
                    {
                        RadMessageBox.Show("Can't save without Creditor !!!");
                        bsplit = false;
                        txtClient.Focus();
                    }
                    else
                    {
                        if (txtInvoice.Text == "")
                        {
                            RadMessageBox.Show("Can't save without Invoice number !!!");
                            bsplit = false;
                            txtInvoice.Focus();
                        }
                        else
                        {
                            if (txtCost.Text == "" && manCost == true)
                            {
                                RadMessageBox.Show("Cost mandatory !!!");
                                bsplit = false;
                                txtCost.Focus();
                            }
                            else
                            {

                                if (txtProject.Text == "" && manProject == true)
                                {
                                    RadMessageBox.Show("Project mandatory !!!");
                                    bsplit = false;
                                    txtProject.Focus();
                                }
                                else
                                {
                                    if (Convert.ToDecimal(txtAmountC.Text) == 0 && Convert.ToDecimal(txtAmountD.Text) == 0)
                                    {
                                        RadMessageBox.Show("Amount 0 !!!");
                                        bsplit = false;
                                        txtAmountC.Focus();
                                    }
                                    else
                                    {
                                        if (txtBtwCode.Text == "" && manBTW)
                                        {
                                            RadMessageBox.Show("BTW mandatory");
                                            bsplit = false;
                                            txtBtwCode.Focus();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (bsplit == true)
            {
                model = new AccLineModel();
                multimodel = new List<AccLineModel>();
                fillLines();
              //  splittingRecords();
                SplitLines();
            }

        }
        #region Splitting


        private void splittingRecords()
        {

           
            if (Convert.ToDecimal(txtAmountC.Text) != 0 && Convert.ToDecimal(txtAmountD.Text) != 0)
            {
                RadMessageBox.Show("Check amounts, please!");
                txtAmountC.Focus();
            }

            if (linesmodel.idBTW != 0 && linesmodel.idBTW != null)  //==== uzima procenat ===
            {
                if (xcodeBTW != "" && xcodeBTW != null)
                {
                    // int b = Convert.ToInt32(linesmodel.idBTW);
                    AccTaxValidityBUS txb = new AccTaxValidityBUS();
                    AccTaxValidityModel tm = new AccTaxValidityModel();
                    tm = txb.GetTaxValidityByCode(xcodeBTW);
                    if (tm != null)
                    {
                        btwPercent = Convert.ToDecimal(tm.percentTax);
                    }
                    else
                    {
                        btwPercent = 0;
                    }
                }
                else
                {
                    btwPercent = 0;
                }


            }
            else
            {
                btwPercent = 0;
            }                                                   //==============================
            // first line creditor debitor
            //AccDebCreBUS adcbus = new AccDebCreBUS();
            //AccDebCreModel cam1 = new AccDebCreModel();
            //cam1 = adcbus.GetCustomerByAccCode(linesmodel.idClientLine);
            //string accD = cam1.debAccount;
            //string accC = cam1.creditAccount;
            if (txtAccount.Text != "")
            {
                model.numberLedAccount = txtAccount.Text;
            }
            else
            {
                model.numberLedAccount = "";
                //if (xSideBooking == "D")
                //{
                //    if (accC != "" && accC != null)
                //        model.numberLedAccount = accC;
                //}

                //if (xSideBooking == "C")
                //{
                //    if (accC != "" && accC != null)
                //        model.numberLedAccount = accD;
                //}
            }


            model.idAccDaily = linesmodel.idAccDaily;
            if (linesmodel.idProjectLine != null && linesmodel.idProjectLine != "")  // ako nije prazan projekat uzima start datum za split stavke
            {
                if (xdtProjectStart != null && xdtProjectStart.ToString() != "1900-01-01")
                {
                    model.dtLine = xdtProjectStart;
                }
            }
            else
            {
                model.dtLine = linesmodel.dtLine;
            }

            model.invoiceNr = linesmodel.invoiceNr;
            model.descLine = linesmodel.descLine;
            model.idClientLine = linesmodel.idClientLine;
            model.idCostLine = linesmodel.idCostLine;
            model.debitLine = linesmodel.debitLine;
            model.creditLine = linesmodel.creditLine;
            model.idBTW = linesmodel.idBTW;
            model.idProjectLine = linesmodel.idProjectLine;
            model.incopNr = linesmodel.incopNr;
            debcreAmt = 0;
            if (txtAccount.Text != "")
            {
                if (txtBtwCode.Text != "")
                {
                    if (Convert.ToDecimal(txtAmountD.Text) != 0)
                    {
                        decimal aa = Convert.ToDecimal(this.txtAmountD.Text);
                        if (btwtype == 1)
                        {
                            debcreAmt = aa / (1 + btwPercent / 100);
                            btwAmt = aa - (aa / (1 + btwPercent / 100));
                        }
                        if (btwtype == 2)
                        {
                            debcreAmt = aa;
                            if (btwPercent != 0)
                            {
                                btwAmt = aa * btwPercent / 100;
                            }
                            else
                            {
                                btwAmt = 0;
                            }
                        }
                    }
                    else
                    {
                        decimal aa = Convert.ToDecimal(this.txtAmountC.Text);
                        if (btwtype == 1)
                        {
                            debcreAmt = aa / (1 + btwPercent / 100);
                            btwAmt = aa - (aa / (1 + btwPercent / 100));
                        }
                        if (btwtype == 2)
                        {
                            debcreAmt = aa;
                            if (btwPercent != 0)
                            {
                                btwAmt = aa * btwPercent / 100;
                            }
                            else
                            {
                                btwAmt = 0;
                            }
                        }
                    }



                }
                else
                {


                      //if (Convert.ToDecimal(txtAmountD.Text) != 0)
                      //  {
                      //      decimal aa = Convert.ToDecimal(this.txtAmountD.Text);
                      //      if (btwtype == 1)
                      //      {
                      //          debcreAmt = aa / (1 + btwPercent / 100);
                      //          btwAmt = aa - (aa / (1 + btwPercent / 100));
                      //      }
                      //      if (btwtype == 2)
                      //      {
                      //          debcreAmt = aa;
                      //          if (btwPercent != 0)
                      //          {
                      //              btwAmt = aa * btwPercent / 100;
                      //          }
                      //          else
                      //          {
                      //              btwAmt = 0;
                      //          }
                      //      }
                      //  }
                      //  else
                      //  {
                      //      decimal aa = Convert.ToDecimal(this.txtAmountC.Text);
                      //      if (btwtype == 1)
                      //      {
                      //          debcreAmt = aa / (1 + btwPercent / 100);
                      //          btwAmt = aa - (aa / (1 + btwPercent / 100));
                      //      }
                      //      if (btwtype == 2)
                      //      {
                      //          debcreAmt = aa;
                      //          if (btwPercent != 0)
                      //          {
                      //              btwAmt = aa * btwPercent / 100;
                      //          }
                      //          else
                      //          {
                      //              btwAmt = 0;
                      //          }
                      //      }
                      //  }

                    }
                }
                if (txtAmountC.Text != "")
                {
                    if (txtAccount.Text == "" && txtBtwCode.Text == "")
                    {
                        model.debitLine = Convert.ToDecimal(txtAmountC.Text);
                        model.creditLine = 0;
                    }
                    else
                    {
                        model.debitLine = Convert.ToDecimal(debcreAmt);
                        // model.debitLine = Convert.ToDecimal(txtAmountC.Text);
                        model.creditLine = 0;
                    }
                }

                if (xSideBooking == "D")
                {
                    model.creditLine = Math.Round(Convert.ToDecimal(txtAmountC.Text), 2);
                    model.debitLine = 0;
                }
                else
                {
                    if (xSideBooking == "C")
                    {
                        if (txtAccount.Text == "" && txtBtwCode.Text == "")
                        {
                            model.debitLine = Convert.ToDecimal(txtAmountC.Text);
                            model.creditLine = 0;
                        }
                        else
                        {
                            // model.debitLine = Math.Round(Convert.ToDecimal(model.debitLine), 2);
                            model.debitLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);
                            model.creditLine = 0;
                        }
                    }
                    else
                    {
                        model.creditLine = 0;
                        model.debitLine = 0;
                    }
                }

                multimodel.Add(model);
                // second line --------------------------------------------------------------------------
                model1 = new AccLineModel();
             
                if (txtBtwCode.Text != "")
                {

                    model1.idAccDaily = linesmodel.idAccDaily;
                        if (linesmodel.idProjectLine != null && linesmodel.idProjectLine != "")  // ako nije prazan projekat uzima start datum za split stavke
                        {
                            if (xdtProjectStart != null && xdtProjectStart.ToString() != "1900-01-01")
                            {
                                model1.dtLine = xdtProjectStart;
                            }
                        }
                        else
                        {
                            model1.dtLine = linesmodel.dtLine;
                        }
                        // model1.dtLine = linesmodel.dtLine;
                        model1.invoiceNr = linesmodel.invoiceNr;
                        model1.descLine = linesmodel.descLine;

                        if (xBtwConto != "" || xBtwConto != null)
                        {
                            model1.numberLedAccount = xBtwConto;
                        }
                        else
                        {
                            model1.numberLedAccount = "1510";
                        }

                        if (xSideBooking == "D")
                        {
                            model1.creditLine = Math.Round(btwAmt, 2);
                            model1.debitLine = 0;
                        }
                        else
                        {
                            if (xSideBooking == "C")
                            {
                                model1.debitLine = Math.Round(btwAmt, 2);
                                model1.creditLine = 0;
                            }
                            else
                            {
                                model1.creditLine = 0;
                                model1.debitLine = 0;
                            }
                        }


                    // model1.debitLine = Math.Round(btwAmt);
                    model1.idBTW = linesmodel.idBTW;
                    model1.idProjectLine = linesmodel.idProjectLine;
                    model1.idClientLine = linesmodel.idClientLine;
                    model1.idCostLine = linesmodel.idCostLine;
                    model1.incopNr = linesmodel.incopNr;

                    multimodel.Add(model1);
            
      
                //dtot = model1.debitLine + Decimal.Parse(txtTdebit.Text.ToString());
                //txtTdebit.Text = dtot.ToString();
                //ctot = model1.creditLine + Decimal.Parse(txtTcredit.Text.ToString());
                //txtTcredit.Text = ctot.ToString();
                //adiff = dtot - ctot;
                //txtTdiff.Text = adiff.ToString();


            }
                gridLines.DataSource = null;
                gridLines.DataSource = multimodel;
                gridLines.Show();
              //  btnSplit.Enabled = false;
   
        }

      
        #endregion Spliting


        # region SplittLines

        private void SplitLines()
        {
         

            if (linesmodel.idBTW != 0 && linesmodel.idBTW != null)  //==== uzima procenat ===
            {
                if (xcodeBTW != "" && xcodeBTW != null)
                {
                    // int b = Convert.ToInt32(linesmodel.idBTW);
                    AccTaxValidityBUS txb = new AccTaxValidityBUS();
                    AccTaxValidityModel tm = new AccTaxValidityModel();
                    tm = txb.GetTaxValidityByCode(xcodeBTW);
                    if (tm != null)
                    {
                        btwPercent = Convert.ToDecimal(tm.percentTax);
                    }
                    else
                    {
                        btwPercent = 0;
                    }
                }
                else
                {
                    btwPercent = 0;
                }


            }
            else
            {
                btwPercent = 0;
            }                                                   //==============================

            if (txtAccount.Text != "")                          // ako konto nije prazno uzima ga za prvu stavku ili ga ostavlja praznog
            {
                model.numberLedAccount = txtAccount.Text;
            }
            else
            {
                model.numberLedAccount = "";

            }
            model.idAccDaily = linesmodel.idAccDaily;
            if (linesmodel.idProjectLine != null && linesmodel.idProjectLine != "")  // ako nije prazan projekat uzima start datum za split stavke
            {
                if (xdtProjectStart != null && xdtProjectStart.ToString() != "1900-01-01")
                {
                    model.dtLine = xdtProjectStart;
                }
            }
            else
            {
                model.dtLine = linesmodel.dtLine;
            }
          //  model.dtLine = linesmodel.dtLine;
            model.invoiceNr = linesmodel.invoiceNr;
            model.descLine = linesmodel.descLine;
            model.idClientLine = linesmodel.idClientLine;
            model.idCostLine = linesmodel.idCostLine;
            model.debitLine = linesmodel.debitLine;
            model.creditLine = linesmodel.creditLine;
            model.idBTW = linesmodel.idBTW;
            model.idProjectLine = linesmodel.idProjectLine;
            model.incopNr = linesmodel.incopNr;
            debcreAmt = 0;

            string sw = "";
            if (txtAccount.Text != "" && txtBtwCode.Text != "")
                sw = "1";   // ima i account i porez
            if (txtAccount.Text == "" && txtBtwCode.Text != "")
                sw = "2";  // nema account ima porez
            if (txtAccount.Text == "" && txtBtwCode.Text == "")
                sw = "3";  // nema konto nema porez
            if (txtAccount.Text != "" && txtBtwCode.Text == "")
                sw = "4"; // ima account nema porez

            switch(sw)
            {
                case "1":   // ima i account i porez
                    if (Convert.ToDecimal(txtAmountD.Text) != 0)
                    {
                        decimal aa = Convert.ToDecimal(this.txtAmountD.Text);
                        if (btwtype == 1)
                        {
                            debcreAmt = aa / (1 + btwPercent / 100);
                            btwAmt = aa - (aa / (1 + btwPercent / 100));
                        }
                        if (btwtype == 2)
                        {
                            debcreAmt = aa;
                            if (btwPercent != 0)
                            {
                                btwAmt = aa * btwPercent / 100;
                            }
                            else
                            {
                                btwAmt = 0;
                            }
                        }
                    }
                    else
                    {
                        decimal aa = Convert.ToDecimal(this.txtAmountC.Text);
                        if (btwtype == 1)
                        {
                            debcreAmt = aa / (1 + btwPercent / 100);
                            btwAmt = aa - (aa / (1 + btwPercent / 100));
                        }
                        if (btwtype == 2)
                        {
                            debcreAmt = aa;
                            if (btwPercent != 0)
                            {
                                btwAmt = aa * btwPercent / 100;
                            }
                            else
                            {
                                btwAmt = 0;
                            }
                        }
                    }

                  //  if (xSideBooking == "D")
                    if (Convert.ToDecimal(txtAmountD.Text) != 0)
                    {
                        model.creditLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);
                        model.debitLine = 0;
                    }
                    else
                    {
                     //   if (xSideBooking == "C")
                        if (Convert.ToDecimal(txtAmountC.Text) != 0)
                        {
                            model.debitLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);
                            model.creditLine = 0;
                        }
                        else
                        {

                            model.debitLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);
                            model.creditLine = 0;
                        }
                    }
                    //if (xSideBooking == "D")
                    //{
                    //    model.creditLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);
                    //    model.debitLine = 0;
                    //}
                    //else
                    //{
                    //    if (xSideBooking == "C")
                    //    {
                    //        model.debitLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);
                    //        model.creditLine = 0;
                    //    }
                    //    else
                    //    {
                        
                    //        model.debitLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);
                    //        model.creditLine = 0;
                    //    }
                    //}
                    break;

                case "2" :

                   if (Convert.ToDecimal(txtAmountD.Text) != 0)
                    {
                        decimal aa = Convert.ToDecimal(this.txtAmountD.Text);
                        if (btwtype == 1)
                        {
                            debcreAmt = aa / (1 + btwPercent / 100);
                            btwAmt = aa - (aa / (1 + btwPercent / 100));
                        }
                        if (btwtype == 2)
                        {
                            debcreAmt = aa;
                            if (btwPercent != 0)
                            {
                                btwAmt = aa * btwPercent / 100;
                            }
                            else
                            {
                                btwAmt = 0;
                            }
                        }
                    }
                    else
                    {
                        decimal aa = Convert.ToDecimal(this.txtAmountC.Text);
                        if (btwtype == 1)
                        {
                            debcreAmt = aa / (1 + btwPercent / 100);
                            btwAmt = aa - (aa / (1 + btwPercent / 100));
                        }
                        if (btwtype == 2)
                        {
                            debcreAmt = aa;
                            if (btwPercent != 0)
                            {
                                btwAmt = aa * btwPercent / 100;
                            }
                            else
                            {
                                btwAmt = 0;
                            }
                        }
                    }

                  //  if (xSideBooking == "D")
                    if (Convert.ToDecimal(txtAmountD.Text) != 0)
                    {
                        model.creditLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);
                        model.debitLine = 0;
                    }
                    else
                    {
                     //   if (xSideBooking == "C")
                        if (Convert.ToDecimal(txtAmountC.Text) != 0)
                        {
                            model.debitLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);
                            model.creditLine = 0;
                        }
                        else
                        {

                            model.debitLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);
                            model.creditLine = 0;
                        }
                    }


                    break;

                case "3" :

                    if (Convert.ToDecimal(txtAmountC.Text) != 0)
                    {
                        model.debitLine = Convert.ToDecimal(txtAmountC.Text);
                        model.creditLine = 0;
                    }
                    else
                    {
                        model.creditLine = Convert.ToDecimal(txtAmountD.Text);
                        model.debitLine = 0;
                    }

                    break;

                case "4" :

                    if (Convert.ToDecimal(txtAmountC.Text) != 0)
                    {
                        model.debitLine = Convert.ToDecimal(txtAmountC.Text);
                        model.creditLine = 0;
                    }
                    else
                    {
                        model.creditLine = Convert.ToDecimal(txtAmountD.Text);
                        model.debitLine = 0;
                    }

                    break;

            }
                 
                multimodel.Add(model);
            
            //==================================  druga stavka ============================================
                model1 = new AccLineModel();

                if (txtBtwCode.Text != "")
                {

                    model1.idAccDaily = linesmodel.idAccDaily;
                    if (linesmodel.idProjectLine != null && linesmodel.idProjectLine != "")  // ako nije prazan projekat uzima start datum za split stavke
                    {
                        if (xdtProjectStart != null && xdtProjectStart.ToString() != "1900-01-01")
                        {
                            model1.dtLine = xdtProjectStart;
                        }
                    }
                    else
                    {
                        model1.dtLine = linesmodel.dtLine;
                    }
                    // model1.dtLine = linesmodel.dtLine;
                    model1.invoiceNr = linesmodel.invoiceNr;
                    model1.descLine = linesmodel.descLine;

                    if (xBtwConto != "" || xBtwConto != null)
                    {
                        model1.numberLedAccount = xBtwConto;
                    }
                    else
                    {
                        model1.numberLedAccount = "1510";
                    }

                    //if (xSideBooking == "D")
                    //{
                    //    model1.creditLine = Math.Round(btwAmt, 2);
                    //    model1.debitLine = 0;
                    //}
                    //else
                    //{
                    //    if (xSideBooking == "C")
                    //    {
                    //        model1.debitLine = Math.Round(btwAmt, 2);
                    //        model1.creditLine = 0;
                    //    }
                    //    else
                    //    {
                    //        model1.creditLine = 0;
                    //        model1.debitLine = 0;
                    //    }
                    //}
                    if (Convert.ToDecimal(txtAmountD.Text) != 0)
                    {
                        model1.creditLine = Math.Round(btwAmt, 2);
                        model1.debitLine = 0;
                    }
                    else
                    {
                        //   if (xSideBooking == "C")
                        if (Convert.ToDecimal(txtAmountC.Text) != 0)
                        {
                            model1.debitLine = Math.Round(btwAmt, 2);
                            model1.creditLine = 0;
                        }
                        else
                        {

                            model1.debitLine = Math.Round(btwAmt, 2);
                            model1.creditLine = 0;
                        }
                    }

                    // model1.debitLine = Math.Round(btwAmt);
                    model1.idBTW = linesmodel.idBTW;
                    model1.idProjectLine = linesmodel.idProjectLine;
                    model1.idClientLine = linesmodel.idClientLine;
                    model1.idCostLine = linesmodel.idCostLine;
                    model1.incopNr = linesmodel.incopNr;

                    multimodel.Add(model1);
                }
//=========================================================================
                    //===================== ovde idu stavke za 1610 =======================================================  
                    //== prva stavka koja zatvara prvu splitovanu stavku 
                    if (txtProject.Text != "")  //linesmodel.idProjectLine
                    {
                      //  AccLineModel model2 = new AccLineModel();
                       // model2 = null;
                        model2 = new AccLineModel();
                      //  model2 = model;   // model  je Debit

                        model2.invoiceNr = linesmodel.invoiceNr;
                        model2.descLine = linesmodel.descLine;
                        model2.idClientLine = linesmodel.idClientLine;
                        model2.idCostLine = linesmodel.idCostLine;
                        model2.idBTW = linesmodel.idBTW;
                        model2.idProjectLine = linesmodel.idProjectLine;
                        model2.incopNr = linesmodel.incopNr;

                        if (xdtProjectStart != null && xdtProjectStart != Convert.ToDateTime("1900-01-01"))
                            model2.dtLine = xdtProjectStart;
                        model2.numberLedAccount = creditReservation;
                        //== ovde ubaciti konta stavku
                        if (model.debitLine > 0)
                        {
                            model2.creditLine = model.debitLine;
                            model2.debitLine = 0;
                        }
                        else
                        {
                            model2.debitLine = model.creditLine;
                            model2.creditLine = 0;
                        }
                        multimodel.Add(model2);

                        //================= druga stavka ==== koja zatvara period =====
                     //   AccLineModel model3 = new AccLineModel();
                      //  model3 = null;
                        model3 = new AccLineModel();
                      //  model3 = model2;
                        model3.invoiceNr = linesmodel.invoiceNr;
                        model3.descLine = linesmodel.descLine;
                        model3.idClientLine = linesmodel.idClientLine;
                        model3.idCostLine = linesmodel.idCostLine;
                      //  model3.debitLine = linesmodel.debitLine;
                      //  model3.creditLine = linesmodel.creditLine;
                        model3.idBTW = linesmodel.idBTW;
                        model3.idProjectLine = linesmodel.idProjectLine;
                        model3.incopNr = linesmodel.incopNr;

                        model3.dtLine = linesmodel.dtLine;
                        model3.numberLedAccount = creditReservation;
                        if (model2.debitLine > 0)
                        {
                            model3.creditLine = model2.debitLine;
                            model3.debitLine = 0;
                        }
                        else
                        {
                            model3.debitLine = model2.creditLine;
                            model3.creditLine = 0;
                        }


                        multimodel.Add(model3);

                    }
                    //=======za BWT ===========================================================================
                    if (txtBtwCode.Text != "" && txtProject.Text != "")
                    {
                        model4 = new AccLineModel();

                        model4.invoiceNr = linesmodel.invoiceNr;
                        model4.descLine = linesmodel.descLine;
                        model4.idClientLine = linesmodel.idClientLine;
                        model4.idCostLine = linesmodel.idCostLine;
                        //    model2.debitLine = linesmodel.debitLine;
                        //    model2.creditLine = linesmodel.creditLine;
                        model4.idBTW = linesmodel.idBTW;
                        model4.idProjectLine = linesmodel.idProjectLine;
                        model4.incopNr = linesmodel.incopNr;

                        if (xdtProjectStart != null && xdtProjectStart != Convert.ToDateTime("1900-01-01"))
                            model4.dtLine = xdtProjectStart;
                        model4.numberLedAccount = creditReservation;
                        //== ovde ubaciti konta stavku
                        if (model1.debitLine > 0)
                        {
                            model4.creditLine = model1.debitLine;
                            model4.debitLine = 0;
                        }
                        else
                        {
                            model4.debitLine = model1.creditLine;
                            model4.creditLine = 0;
                        }
                        multimodel.Add(model4);

                        //=== druga stavka za btw ======================================================
                        model5 = new AccLineModel();

                        model5.invoiceNr = linesmodel.invoiceNr;
                        model5.descLine = linesmodel.descLine;
                        model5.idClientLine = linesmodel.idClientLine;
                        model5.idCostLine = linesmodel.idCostLine;
                        //    model2.debitLine = linesmodel.debitLine;
                        //    model2.creditLine = linesmodel.creditLine;
                        model5.idBTW = linesmodel.idBTW;
                        model5.idProjectLine = linesmodel.idProjectLine;
                        model5.incopNr = linesmodel.incopNr;

                        //if (xdtProjectStart != null && xdtProjectStart != Convert.ToDateTime("1900-01-01"))
                        model5.dtLine = linesmodel.dtLine;
                        model5.numberLedAccount = creditReservation;
                        //== ovde ubaciti konta stavku
                        if (model4.debitLine > 0)
                        {
                            model5.creditLine = model4.debitLine;
                            model5.debitLine = 0;
                        }
                        else
                        {
                            model5.debitLine = model4.creditLine;
                            model5.creditLine = 0;
                        }
                        multimodel.Add(model5);
                    }
//==========================================================================
                    //gridLines.DataSource = null;
                    //gridLines.DataSource = multimodel;
                    //gridLines.Show();
                    //btnSplit.Enabled = false;



                //}
                gridLines.DataSource = null;
                gridLines.DataSource = multimodel;
                gridLines.Show();
             //   btnSplit.Enabled = false;
        }
        # endregion SplittLines

        void gridLines_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.gridLines.CurrentCell != null)
            {
                string aa = this.gridLines.CurrentCell.ColumnInfo.Name;
                if (e.KeyCode == Keys.Enter && aa == "incop")
                {
                    int bb = gridLines.Rows.Count;
                    //  this.gridLines.Rows.AddNew();
                    gridLines.CurrentRow = gridLines.Rows.AddNew();
                    gridLines.CurrentRow.Cells["date"].Value = gridLines.Rows[bb - 1].Cells["date"].Value;
                    gridLines.CurrentRow.Cells["invoice"].Value = gridLines.Rows[bb - 1].Cells["invoice"].Value;
                    gridLines.CurrentRow.Cells["description"].Value = gridLines.Rows[bb - 1].Cells["description"].Value;
                    gridLines.CurrentRow.Cells["client"].Value = gridLines.Rows[bb - 1].Cells["client"].Value;
                    gridLines.CurrentRow.Cells["btw"].Value = gridLines.Rows[bb - 1].Cells["btw"].Value;
                    //  gridLines.CurrentRow.Cells["debit"].Value = gridLines.Rows[bb - 1].Cells["debit"].Value;
                    //  gridLines.CurrentRow.Cells["credit"].Value = gridLines.Rows[bb - 1].Cells["credit"].Value;
                    gridLines.CurrentRow.Cells["cost"].Value = gridLines.Rows[bb - 1].Cells["cost"].Value;
                    gridLines.CurrentRow.Cells["project"].Value = gridLines.Rows[bb - 1].Cells["project"].Value;
                    gridLines.CurrentRow.Cells["incop"].Value = gridLines.Rows[bb - 1].Cells["incop"].Value;
                    //    gridLines.CurrentRow = gridLines.Rows.AddNew();
                    gridLines.CurrentRow.Cells["date"].BeginEdit();
                    // gridLines.Rows[bb + 1].Cells["date"].EndEdit();

                }
                if (e.KeyCode == Keys.F2)
                {
                    if (aa == "client")  //this.gridLines.CurrentCell != null)
                    {
                        DebCreLookupBUS debpers = new DebCreLookupBUS();
                        List<IModel> pm1 = new List<IModel>();

                        pm1 = debpers.GetCreditors();
                        var dlgSave = new GridLookupForm(pm1, "Creditor");

                        if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                        {
                            DebCreLookupModel pm1X = new DebCreLookupModel();
                            pm1X = (DebCreLookupModel)dlgSave.selectedRow;
                            //set textbox
                            gClient = pm1X.accNumber;

                        }
                        gridLines.CurrentRow.Cells["client"].Value = gClient;


                    }
                    if (aa == "cost")
                    {
                        AccCostBUS ccentar = new AccCostBUS();
                        List<IModel> gmX = new List<IModel>();

                        gmX = ccentar.GetAllCost();
                        var dlgSave = new GridLookupForm(gmX, "Cost");

                        if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                        {
                            AccCostModel genmX = new AccCostModel();
                            genmX = (AccCostModel)dlgSave.selectedRow;
                            //set textbox
                            //  gridLines.SelectedRows[0].Cells["cost"].Value = genmX.codeCost.ToString();
                            gridLines.CurrentRow.Cells["cost"].Value = genmX.codeCost.ToString();
                            // labelCost.Text = genmX.descCost;
                        }
                    }
                    if (aa == "account")
                    {
                        LedgerAccountBUS ccentar3 = new LedgerAccountBUS(Login._bookyear);
                        List<IModel> gmX3 = new List<IModel>();

                        gmX3 = ccentar3.GetAllAccounts();
                        var dlgSave3 = new GridLookupForm(gmX3, "Ledger");

                        if (dlgSave3.ShowDialog(this) == DialogResult.Yes)
                        {
                            LedgerAccountModel genmX3 = new LedgerAccountModel();
                            genmX3 = (LedgerAccountModel)dlgSave3.selectedRow;
                            //set textbox
                            gridLines.CurrentRow.Cells["account"].Value = genmX3.numberLedgerAccount;
                        }
                    }
                    if (aa == "project")
                    {
                        ArrangementBUS ccentar1 = new ArrangementBUS();
                        List<IModel> gmX1 = new List<IModel>();

                        gmX1 = ccentar1.GetAllArrangementsAccount(Login._bookyear);
                        var dlgSave1 = new GridLookupForm(gmX1, "Arrangement");

                        if (dlgSave1.ShowDialog(this) == DialogResult.Yes)
                        {
                            ArrangementModel genmX1 = new ArrangementModel();
                            genmX1 = (ArrangementModel)dlgSave1.selectedRow;
                            //set textbox
                            gridLines.CurrentRow.Cells["project"].Value = genmX1.codeProject;
                            //if (genmX1.dtFromArrangement != null)
                            //     xdtProjectStart = Convert.ToDateTime(genmX1.dtFromArrangement);

                        }
                    }
                }
            }
        }

        private void txtClient_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int xX = this.Location.X;
            int yY = this.Location.Y;
            debcreCMenu.Show(xX + 220, yY + 100);
        }

        private void saveItems()
        {
            listlines = new List<AccLineModel>();
            
            int j = 2;
            for (int i = 0; i < gridLines.Rows.Count; i++)
            {
                AccLineModel itm = new AccLineModel();
                itm.idAccDaily = xDaily;
                itm.currrate = 0;

                itm.statusLine = false;
                itm.creditBTW = 0;
                itm.debitBTW = 0;
                itm.creditCurr = 0;
                itm.debitCurr = 0;
                itm.dtBooking = dpValutaDate.Value;
                //itm.dtBooking = DateTime.Now;
                itm.idProjectLine = "";
                itm.idCostLine = "";
                itm.numberLedAccount = "";
                itm.idPersonLine = "";
                itm.incopNr = "";
                itm.dtLine = Convert.ToDateTime(gridLines.Rows[i].Cells["date"].Value.ToString());
                DateTime mper = Convert.ToDateTime(itm.dtLine);
                itm.periodLine = mper.Month;
                if (gridLines.Rows[i].Cells["invoice"].Value != null)
                    itm.invoiceNr = gridLines.Rows[i].Cells["invoice"].Value.ToString();
                if (gridLines.Rows[i].Cells["description"].Value != null)
                    itm.descLine = gridLines.Rows[i].Cells["description"].Value.ToString();
                if (gridLines.Rows[i].Cells["client"].Value != null)
                    itm.idClientLine = gridLines.Rows[i].Cells["client"].Value.ToString();
                if (gridLines.Rows[i].Cells["account"].Value != null)
                    itm.numberLedAccount = gridLines.Rows[i].Cells["account"].Value.ToString();
                if (gridLines.Rows[i].Cells["btw"].Value != null)
                    itm.idBTW = Convert.ToInt32(gridLines.Rows[i].Cells["btw"].Value.ToString());
                if (gridLines.Rows[i].Cells["debit"].Value != null)
                    itm.debitLine = Convert.ToDecimal(gridLines.Rows[i].Cells["debit"].Value.ToString());
                if (gridLines.Rows[i].Cells["credit"].Value != null)
                    itm.creditLine = Convert.ToDecimal(gridLines.Rows[i].Cells["credit"].Value.ToString());
                if (gridLines.Rows[i].Cells["cost"].Value != null)
                    itm.idCostLine = gridLines.Rows[i].Cells["cost"].Value.ToString();
                if (gridLines.Rows[i].Cells["project"].Value != null)
                    itm.idProjectLine = gridLines.Rows[i].Cells["project"].Value.ToString();
                if (gridLines.Rows[i].Cells["incop"].Value != null)
                    itm.incopNr = gridLines.Rows[i].Cells["incop"].Value.ToString();

                itm.booksort = j++;
                itm.bookingYear = Login._bookyear;
                listlines.Add(itm);
            }
           // openLinesUpdate();
        }


        private void MasterTemplate_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            debTotal = 0;
            creTotal = 0;
            totalLines = 0;
            for (int i = 0; i < gridLines.Rows.Count; i++)
            {
                if (gridLines.Rows[i].Cells["debit"].Value != null)
                    debTotal = debTotal + Convert.ToDecimal(gridLines.Rows[i].Cells["debit"].Value.ToString());
                if (gridLines.Rows[i].Cells["credit"].Value != null)
                    creTotal = creTotal + Convert.ToDecimal(gridLines.Rows[i].Cells["credit"].Value.ToString());

            }

            totalLines = debTotal - creTotal;
            txtTdebit.Text = debTotal.ToString();
            txtTcredit.Text = creTotal.ToString();
            txtTdiff.Text = totalLines.ToString();
        }
        private void totals()
        {
            debTotal = 0;
            creTotal = 0;
            totalLines = 0;
            for (int i = 0; i < gridLines.Rows.Count; i++)
            {
                debTotal = debTotal + Convert.ToDecimal(gridLines.Rows[i].Cells["debit"].Value.ToString());
                creTotal = creTotal + Convert.ToDecimal(gridLines.Rows[i].Cells["credit"].Value.ToString());

            }
            totalLines = debTotal- creTotal;
            txtTdebit.Text = debTotal.ToString();                //debTotal.ToString();
            txtTcredit.Text = creTotal.ToString();                                 //creTotal.ToString();
            txtTdiff.Text = totalLines.ToString();
        }

        private void MasterTemplate_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (this.gridLines.CurrentCell != null)
            {
                string aa = this.gridLines.CurrentCell.ColumnInfo.Name;
                switch (aa)
                {
                    case "client":
                        int xX = this.Location.X;
                        int yY = this.Location.Y;
                        debcreCMenu.Show(xX + 220, yY + 100);
                        break;
                    case "btw":
                        AccTaxBUS ccentar4 = new AccTaxBUS();
                        List<IModel> gmX4 = new List<IModel>();

                        gmX4 = ccentar4.GetAllTax(Login._user.lngUser);
                        var dlgSave4 = new GridLookupForm(gmX4, "Btw");

                        if (dlgSave4.ShowDialog(this) == DialogResult.Yes)
                        {
                            AccTaxModel genmX4 = new AccTaxModel();
                            genmX4 = (AccTaxModel)dlgSave4.selectedRow;
                            gridLines.CurrentRow.Cells["project"].Value = genmX4.idTax;
                            //set textbox
                            //txtBtwCode.Text = genmX.idTax.ToString();
                            //labelBtw.Text = genmX.descTax;
                            //xBtwConto = genmX.numberLedAccount;
                            //if (genmX.typeTax != 0 && genmX.typeTax != null)
                            //    btwtype = Convert.ToDecimal(genmX.typeTax);
                        }
                        break;

                    case "cost":
                        AccCostBUS ccentar = new AccCostBUS();
                        List<IModel> gmX = new List<IModel>();

                        gmX = ccentar.GetAllCost();
                        var dlgSave = new GridLookupForm(gmX, "Cost");

                        if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                        {
                            AccCostModel genmX = new AccCostModel();
                            genmX = (AccCostModel)dlgSave.selectedRow;
                            //set textbox
                            //  gridLines.SelectedRows[0].Cells["cost"].Value = genmX.codeCost.ToString();
                            gridLines.CurrentRow.Cells["cost"].Value = genmX.codeCost.ToString();
                            // labelCost.Text = genmX.descCost;
                        }
                        break;

                    case "project":
                        ArrangementBUS ccentar1 = new ArrangementBUS();
                        List<IModel> gmX1 = new List<IModel>();

                        gmX1 = ccentar1.GetAllArrangementsAccount(Login._bookyear);
                        var dlgSave1 = new GridLookupForm(gmX1, "Arrangement");

                        if (dlgSave1.ShowDialog(this) == DialogResult.Yes)
                        {
                            ArrangementModel genmX1 = new ArrangementModel();
                            genmX1 = (ArrangementModel)dlgSave1.selectedRow;
                            //set textbox
                            gridLines.CurrentRow.Cells["project"].Value = genmX1.codeProject;
                            if (genmX1.dtFromArrangement != null)
                                xdtProjectStart = Convert.ToDateTime(genmX1.dtFromArrangement);
                            //txtProject.Text = genmX1.codeProject;
                            //labelProject.Text = genmX1.nameArrangement;
                        }
                        break;

                    case "account":

                        LedgerAccountBUS ccentar3 = new LedgerAccountBUS(Login._bookyear);
                        List<IModel> gmX3 = new List<IModel>();

                        gmX3 = ccentar3.GetAllAccounts();
                        var dlgSave3 = new GridLookupForm(gmX3, "Ledger");

                        if (dlgSave3.ShowDialog(this) == DialogResult.Yes)
                        {
                            LedgerAccountModel genmX3 = new LedgerAccountModel();
                            genmX3 = (LedgerAccountModel)dlgSave3.selectedRow;
                            //set textbox
                            gridLines.CurrentRow.Cells["account"].Value = genmX3.numberLedgerAccount;
                            //txtAccount.Text = genmX3.numberLedgerAccount;
                            //labelKonto.Text = genmX3.descLedgerAccount;
                            //xSideBooking = genmX3.sideBooking;
                        }
                        break;
                }
            }
        }
        #region Mandatory


        private void txtInvoice_Leave(object sender, EventArgs e)
        {
            if (txtInvoice.Text == "")
            {
                RadMessageBox.Show("Invoice number is mandatory !!!");
                txtInvoice.Focus();
            }
        }
        private void txtClient_Leave(object sender, EventArgs e)
        {
            ClientBUS cb = new ClientBUS();
            ClientModel cm = new ClientModel();
            PersonBUS pbs = new PersonBUS();
            PersonModel pmd = new PersonModel();
           
                if (txtClient.Text != "")
                {
                    AccDebCreBUS debpers = new AccDebCreBUS();
                    AccDebCreModel pm1 = new AccDebCreModel();
                   
                    pm1 = debpers.GetCustomerByAccCode(txtClient.Text);
                    if (pm1 != null)
                    {
                        if (pm1.idClient != null && pm1.idContPerson == 0)
                        {
                            cm = cb.GetClient(pm1.idClient);
                            if (cm != null)
                            {
                                txtClient.Text = cm.accountCodeClient;
                                gClient = cm.accountCodeClient;
                                labelClient.Text = cm.nameClient;
                                if (pm1.payCondition != null && pm1.payCondition != 0)
                                {
                                    AccPaymentBUS apb = new AccPaymentBUS();
                                    AccPaymentModel apm = new AccPaymentModel();
                                    apm = apb.GetPaymentByID(pm1.payCondition);
                                    if (apm != null)
                                        txtPayingDays.Value = apm.numberDays;
                                }
                                txtPayingDays.Focus();
                               // txtAccount.Focus();
                            }
                            else
                            {
                                txtClient.Text = "";
                                txtClient.Focus();
                            }
                        }
                        else
                        {
                            if (pm1.idContPerson != null && pm1.idClient == 0)
                                pmd = pbs.GetPerson(pm1.idContPerson);
                            if (pmd != null)
                            {
                                labelClient.Text = pmd.firstname + " " + pmd.midname + " " + pmd.lastname;
                                if (pm1.payCondition != null && pm1.payCondition != 0)
                                {
                                    AccPaymentBUS apb = new AccPaymentBUS();
                                    AccPaymentModel apm = new AccPaymentModel();
                                    apm = apb.GetPaymentByID(pm1.payCondition);
                                    if (apm != null)
                                        txtPayingDays.Value = apm.numberDays;
                                }
                                txtPayingDays.Focus();
                              //  txtAccount.Focus();
                            }
                            else
                            {
                                txtClient.Text = "";
                                txtClient.Focus();
                            }
                            //txtClient.Focus();
                        }
                        //txtClient.Focus();
                    }
                    else
                    {
                        RadMessageBox.Show("Wrong id");
                        txtClient.Focus();
                    }
                }
           
        }

        private void txtBtwCode_Leave(object sender, EventArgs e)
        {
            //if (manBTW == true)
            //{
            //    if (txtBtwCode.Text == "")
            //    {
            //        RadMessageBox.Show("BTW  mandatory");
            //        txtProject.Focus();
            //    }
            //    else
            //    {
            //        AccTaxBUS porez = new AccTaxBUS();
            //        AccTaxModel pm = new AccTaxModel();
            //        pm = porez.GetTaxByID(Convert.ToInt32(txtBtwCode.Text));
            //        if (pm != null)
            //        {
            //            txtBtwCode.Text = pm.idTax.ToString();
            //            labelBtw.Text = pm.descTax;
            //            if (pm.typeTax != null)
            //            {
            //                btwtype = Convert.ToDecimal(pm.typeTax);
            //            }
            //            else
            //            {
            //                if (pm.numberLedAccount != null)
            //                {
            //                    xBtwConto = pm.numberLedAccount;
            //                    txtAmountC.Focus();
            //                }
            //            }
            //        }
            //        else
            //        {
            //            RadMessageBox.Show("Wrong BTW code");
            //            txtBtwCode.Focus();
            //        }



            //    }
            //}
            //else
            //{
                if (txtBtwCode.Text != "")
                {
                    AccTaxBUS porez = new AccTaxBUS();
                    AccTaxModel pm = new AccTaxModel();
                    pm = porez.GetTaxByID(Convert.ToInt32(txtBtwCode.Text));
                    if (pm != null)
                    {
                        labelBtw.Text = pm.descTax;
                        xcodeBTW = pm.codeTax;
                        xBtwConto = pm.numberLedAccount;
                        if (pm.typeTax != 0 && pm.typeTax != null)
                            btwtype = Convert.ToDecimal(pm.typeTax);
                        txtBtwCode.Text = pm.idTax.ToString();
                        labelBtw.Text = pm.descTax;
                        //if (pm.typeTax != null)
                        //{
                        //    btwtype = Convert.ToDecimal(pm.typeTax);
                        //}
                        if (pm.numberLedAccount != null)
                        {
                            xBtwConto = pm.numberLedAccount;
                            txtAmountC.Focus();
                        }
                    }
                    else
                    {
                        RadMessageBox.Show("Wrong BTW code");
                        txtBtwCode.Focus();
                    }
                }

           // }
        }

        private void txtCost_Leave(object sender, EventArgs e)
        {
            //if (manCost == true)
            //{
            //    if (txtCost.Text == "")
            //    {
            //        RadMessageBox.Show("Cost mandatory");
            //        txtCost.Focus();
            //    }
            //    else
            //    {
            //        AccCostBUS acc = new AccCostBUS();
            //        AccCostModel amc = new AccCostModel();
            //        amc = acc.GetCostByID(txtCost.Text);
            //        if (amc != null)
            //        {
            //            labelCost.Text = amc.descCost;
            //            txtProject.Focus();
            //        }
            //        else
            //        {
            //            labelCost.Text = "";
            //            txtCost.Text = "";
            //            RadMessageBox.Show("Wrong Cost code");
            //            txtCost.Focus();
            //        }
            //    }

            //}
            //else
            //{
                if (txtCost.Text == "")
                {
                    txtProject.Focus();
                }
                else
                {
                    AccCostBUS acc = new AccCostBUS();
                    AccCostModel amc = new AccCostModel();
                    amc = acc.GetCostByID(txtCost.Text);
                    if (amc != null)
                    {
                        labelCost.Text = amc.descCost;
                        txtProject.Focus();
                    }
                    else
                    {
                        labelCost.Text = "";
                        txtCost.Text = "";
                        RadMessageBox.Show("Wrong Cost code");
                        txtCost.Focus();
                    }
                }
            //}
        }

        private void txtProject_Leave(object sender, EventArgs e)
        {
            //if (manProject == true)
            //{
            //    if (txtProject.Text == "")
            //    {
            //        RadMessageBox.Show("Project mandatory");
            //        txtProject.Focus();
            //    }
            //    else
            //    {
            //        ArrangementBUS arb = new ArrangementBUS();
            //        ArrangementModel arm = new ArrangementModel();
            //        arm = arb.GetArrangementByCode(txtProject.Text);
            //        if (arm != null)
            //        {
            //            labelProject.Text = arm.nameArrangement;
            //            txtArrdate.Text = "Start " + Convert.ToString(arm.dtFromArrangement) + " End " + Convert.ToString(arm.dtToArrangement);
            //            if (btnSplit.Enabled == true)
            //            {
            //                btnSplit.Focus();
            //            }
            //            else
            //            {
            //                btnOK.Focus();
            //            }
            //        }
            //        else
            //        {
            //            txtProject.Text = "";
            //            labelProject.Text = "";
            //            txtArrdate.Text = "";
            //            RadMessageBox.Show("Wrong Project code");
            //            txtProject.Focus();
            //        }
            //    }
            //}
            //else
            //{
                if (txtProject.Text == "")
                {
                    if (btnSplit.Enabled == true)
                        btnSplit.Focus();
                    else
                        btnOK.Focus();
                }
                else
                {
                    ArrangementBUS arb = new ArrangementBUS();
                    ArrangementModel arm = new ArrangementModel();
                    arm = arb.GetArrangementByCode(txtProject.Text);
                    if (arm != null)
                    {
                        labelProject.Text = arm.nameArrangement;
                        txtArrdate.Text = "Start " + Convert.ToString(arm.dtFromArrangement) + " End " + Convert.ToString(arm.dtToArrangement);
                        if (arm.dtFromArrangement != null)
                        {
                            xdtProjectStart = Convert.ToDateTime(arm.dtFromArrangement);
                        }
                        if (arm.dtFromArrangement != null)
                            xdtProjectStart = Convert.ToDateTime(arm.dtFromArrangement);
                        if (btnSplit.Enabled == true)
                        {
                            btnSplit.Focus();
                        }
                        else
                        {
                            btnOK.Focus();
                        }
                    }
                }
            //}

        }
        #endregion Mandatory

        private void getIncopNr()
        {
            AccLineBUS gn = new AccLineBUS(Login._bookyear);
            IdModel nid = new IdModel();
            int idDaily = 0;
            string yearId = Login._bookyear; //DateTime.Now.Year.ToString();
            if (xDaily != -1)
                idDaily = xDaily;
            nid = gn.GetIncop(yearId, idDaily, this.Name, Login._user.idUser);
            var result = nid.idNumber.ToString().PadLeft(6, '0');
           // DateTime YearDate = DateTime.Now;
           // string year2 = YearDate.ToString("yy");
            var aa = nid.idDaily.ToString().PadRight(6, '0');
            string SubString = nid.yearId.Substring(yearId.Length - 2);
            txtIncop.Text = SubString + aa + result;

        }

        private void MasterTemplate_UserAddingRow(object sender, GridViewRowCancelEventArgs e)
        {
            AccLineModel item = new AccLineModel();
            multimodel.Add(item);
            int aa = gridLines.RowCount;
                      
            gridLines.CurrentRow.Cells["date"].Value = gridLines.Rows[aa - 1].Cells["date"].Value;
            gridLines.CurrentRow.Cells["invoice"].Value = gridLines.Rows[aa - 1].Cells["invoice"].Value;
            gridLines.CurrentRow.Cells["description"].Value = gridLines.Rows[aa - 1].Cells["description"].Value;
            gridLines.CurrentRow.Cells["client"].Value = gridLines.Rows[aa - 1].Cells["client"].Value;
            gridLines.CurrentRow.Cells["btw"].Value = gridLines.Rows[aa - 1].Cells["btw"].Value;
            gridLines.CurrentRow.Cells["debit"].Value = gridLines.Rows[aa - 1].Cells["debit"].Value;
            gridLines.CurrentRow.Cells["credit"].Value = gridLines.Rows[aa - 1].Cells["credit"].Value;
            gridLines.CurrentRow.Cells["cost"].Value = gridLines.Rows[aa - 1].Cells["cost"].Value;
            gridLines.CurrentRow.Cells["project"].Value = gridLines.Rows[aa - 1].Cells["project"].Value;
            gridLines.CurrentRow.Cells["incop"].Value = gridLines.Rows[aa - 1].Cells["incop"].Value;
           
            gridLines.Show();
        }

        private void btnAccount_MouseClick(object sender, MouseEventArgs e)
        {
            LedgerAccountBUS ccentar3 = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX3 = new List<IModel>();

            gmX3 = ccentar3.GetAllAccounts();
            var dlgSave3 = new GridLookupForm(gmX3, "Ledger");

            if (dlgSave3.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX3 = new LedgerAccountModel();
                genmX3 = (LedgerAccountModel)dlgSave3.selectedRow;
                //set textbox
                gridLines.CurrentRow.Cells["account"].Value = genmX3.numberLedgerAccount;
                txtAccount.Text = genmX3.numberLedgerAccount;
                labelKonto.Text = genmX3.descLedgerAccount;
                //xSideBooking = genmX3.sideBooking;
            }
        }

        private void btnBtw_MouseClick(object sender, MouseEventArgs e)
        {
            AccTaxBUS ccentar4 = new AccTaxBUS();
            List<IModel> gmX4m = new List<IModel>();

            gmX4m = ccentar4.GetAllTax(Login._user.lngUser);
            var dlgSave4 = new GridLookupForm(gmX4m, "Btw");
            if (dlgSave4.ShowDialog(this) == DialogResult.Yes)
            {
                AccTaxModel gmX4ml = new AccTaxModel();
                gmX4ml = (AccTaxModel)dlgSave4.selectedRow;
                gridLines.CurrentRow.Cells["Btw"].Value = gmX4ml.idTax;
                //set textbox
                txtBtwCode.Text = gmX4ml.idTax.ToString();
                labelBtw.Text = gmX4ml.descTax;
                xcodeBTW = gmX4ml.codeTax;
                xBtwConto = gmX4ml.numberLedAccount;
                if (gmX4ml.typeTax != 0 && gmX4ml.typeTax != null)
                    btwtype = Convert.ToDecimal(gmX4ml.typeTax);
                //txtBtwCode.Text = gmX4ml.idTax.ToString();
                //labelBtw.Text = gmX4ml.descTax;
                //xBtwConto = gmX4ml.numberLedAccount;
                //if (gmX4ml.typeTax != 0 && gmX4ml.typeTax != null)
                //    btwtype = Convert.ToDecimal(gmX4ml.typeTax);
            }
        }

        private void btnCost_MouseClick(object sender, MouseEventArgs e)
        {
            AccCostBUS ccentar = new AccCostBUS();
            List<IModel> gmX = new List<IModel>();

            gmX = ccentar.GetAllCost();
            var dlgSave = new GridLookupForm(gmX, "Cost");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                AccCostModel genmX = new AccCostModel();
                genmX = (AccCostModel)dlgSave.selectedRow;
                //set textbox
                //  gridLines.SelectedRows[0].Cells["cost"].Value = genmX.codeCost.ToString();
                gridLines.CurrentRow.Cells["cost"].Value = genmX.codeCost.ToString();
                labelCost.Text = genmX.descCost;
                txtCost.Text = genmX.codeCost;
            }

        }

        private void btnProject_MouseClick(object sender, MouseEventArgs e)
        {
            ArrangementBUS ccentar1 = new ArrangementBUS();
            List<IModel> gmX1 = new List<IModel>();

            gmX1 = ccentar1.GetAllArrangementsAccount(Login._bookyear);
            var dlgSave1 = new GridLookupForm(gmX1, "Arrangement");

            if (dlgSave1.ShowDialog(this) == DialogResult.Yes)
            {
                ArrangementModel genmX1 = new ArrangementModel();
                genmX1 = (ArrangementModel)dlgSave1.selectedRow;
                //set textbox
                gridLines.CurrentRow.Cells["project"].Value = genmX1.codeProject;
                txtProject.Text = genmX1.codeProject;
                labelProject.Text = genmX1.nameArrangement;
                txtArrdate.Text = "Start " + Convert.ToString(genmX1.dtFromArrangement) + " End " + Convert.ToString(genmX1.dtToArrangement);
                if (genmX1.dtFromArrangement != null)
                    xdtProjectStart = Convert.ToDateTime(genmX1.dtFromArrangement);
            }
        }



        private void btnClient_MouseClick(object sender, MouseEventArgs e)
        {
            DebCreLookupBUS debpers = new DebCreLookupBUS();
            List<IModel> pm1 = new List<IModel>();

            pm1 = debpers.GetCreditors();
            var dlgSave = new GridLookupForm(pm1, "Creditor");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                DebCreLookupModel pm1X = new DebCreLookupModel();
                pm1X = (DebCreLookupModel)dlgSave.selectedRow;
                //set textbox
                txtClient.Text = pm1X.accNumber;
                gClient = pm1X.accNumber;
                labelClient.Text = pm1X.name;
                AccDebCreBUS dcb = new AccDebCreBUS();
                AccDebCreModel dcm = new AccDebCreModel();
                dcm = dcb.GetCustomerByAccCode(pm1X.accNumber);

                if (dcm != null && dcm.payCondition != 0)
                {
                    AccPaymentBUS apb = new AccPaymentBUS();
                    AccPaymentModel apm = new AccPaymentModel();
                    apm = apb.GetPaymentByID(dcm.payCondition);
                    if (apm != null)
                        txtPayingDays.Value = apm.numberDays;
                }
                txtPayingDays.Focus();
               // txtAccount.Focus();
            }

        }
        private void checkGriddata()
        {
            //if (multimodel.Count > 0)
            //{
            //    for (int i = 0; i < multimodel.Count; i++)
            //    {
            int i = 0;
                foreach(AccLineModel itm in multimodel)
                {
                    //AccLineModel itm = multimodel[i];
                    if (itm != null)
                    {
                        if (gridLines.RowCount - 1 >= i)
                        {


                            //    AccLineModel itm = new AccLineModel();
                            itm.idAccDaily = xDaily;
                            itm.currrate = 0;
                            //DateTime mper = Convert.ToDateTime(itm.dtLine);
                            //itm.periodLine = mper.Month;
                            itm.statusLine = false;
                            itm.creditBTW = 0;
                            itm.debitBTW = 0;
                            itm.creditCurr = 0;
                            itm.debitCurr = 0;
                            itm.dtBooking = dpValutaDate.Value;
                            //itm.dtBooking = DateTime.Now;
                            //itm.idProjectLine = "";
                            //itm.idCostLine = "";
                            //itm.numberLedAccount = "";
                            //itm.idPersonLine = "";
                            //itm.incopNr = "";
                            if (gridLines.Rows[i].Cells["date"].Value != null)
                                itm.dtLine = Convert.ToDateTime(gridLines.Rows[i].Cells["date"].Value.ToString());

                            if (gridLines.Rows[i].Cells["invoice"].Value != null)
                                itm.invoiceNr = gridLines.Rows[i].Cells["invoice"].Value.ToString();
                            if (itm.invoiceNr.ToString() == "")
                            {
                                RadMessageBox.Show("No invoice number !");
                                blcheck = false;
                                break;
                            }
                            if (gridLines.Rows[i].Cells["description"].Value != null)
                                itm.descLine = gridLines.Rows[i].Cells["description"].Value.ToString();
                            if (gridLines.Rows[i].Cells["client"].Value != null)
                                itm.idClientLine = gridLines.Rows[i].Cells["client"].Value.ToString();
                            if (itm.idClientLine == null)
                            {
                                RadMessageBox.Show("No Customer number !");
                                blcheck = false;
                                break;
                            }
                            if (gridLines.Rows[i].Cells["account"].Value != null)
                                itm.numberLedAccount = gridLines.Rows[i].Cells["account"].Value.ToString();
                            if (itm.numberLedAccount == null )
                            {
                                RadMessageBox.Show("No Account !");
                                blcheck = false;
                                break;
                            }
                            if (gridLines.Rows[i].Cells["btw"].Value != null)
                                itm.idBTW = Convert.ToInt32(gridLines.Rows[i].Cells["btw"].Value.ToString());
                            if (gridLines.Rows[i].Cells["debit"].Value != null)
                                itm.debitLine = Convert.ToDecimal(gridLines.Rows[i].Cells["debit"].Value.ToString());
                            if (gridLines.Rows[i].Cells["credit"].Value != null)
                                itm.creditLine = Convert.ToDecimal(gridLines.Rows[i].Cells["credit"].Value.ToString());
                            if (gridLines.Rows[i].Cells["cost"].Value != null)
                                itm.idCostLine = gridLines.Rows[i].Cells["cost"].Value.ToString();
                            if (itm.idCostLine == null && manCost == true)
                            {
                                RadMessageBox.Show("No Account !");
                                blcheck = false;
                                break;
                            }
                            if (gridLines.Rows[i].Cells["project"].Value != null)
                                itm.idProjectLine = gridLines.Rows[i].Cells["project"].Value.ToString();
                            if (gridLines.Rows[i].Cells["incop"].Value != null)
                                itm.incopNr = gridLines.Rows[i].Cells["incop"].Value.ToString();

                            blcheck = true;
                        }
                    }
                  i++;

            }



        }

        private void MasterTemplate_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridSummaryCellElement)
            {
                if (!String.IsNullOrEmpty(e.CellElement.Text))
                {
                    e.CellElement.BorderLeftWidth = e.CellElement.BorderRightWidth = 1;
                    e.CellElement.Font = new Font("Verdana", 9, FontStyle.Bold);
                    e.CellElement.TextAlignment = ContentAlignment.MiddleRight;



                }
                else
                {
                    e.CellElement.ResetValue(LightVisualElement.BorderLeftWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
                    e.CellElement.ResetValue(LightVisualElement.BorderRightWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
                }
            }
        }
        private void openLinesUpdate()
        {
            //AccOpenLinesBUS olbus = new AccOpenLinesBUS();
            //AccOpenLinesModel olmod = new AccOpenLinesModel();
            //olmod = olbus.GetAccOpenLinesByInvoice(linesmodel.invoiceNr);
            //if (olmod != null)
            //{
            //    if (olmod.invoiceOpenLine == linesmodel.invoiceNr)
            //        //if (olmod.typeOpenLine == "C")
            //       {

            //            olmod.debitOpenLine = linesmodel.debitLine;     //itmol.creditLine; 
            //            olmod.creditOpenLine = linesmodel.creditLine;
            //            olbus.Update(olmod);
            //        //}
            //        //else
            //        //{
            //           //itmol.debitLine;                   
            //        }

            //}

            //if (lledit == true)
            //{
                AccAcountUpdate ilm = new AccAcountUpdate();
               
                if (linesmodel.invoiceNr != null && linesmodel.invoiceNr != "")
                {
                    isClose=ilm.CheckOpenLines(linesmodel.invoiceNr);

                }
           // }
            if (isClose == true) // moze da se menja nije zatvorena ili delimicno zatvorena
            {



                int daysp = 0;

                AccOpenLinesBUS olbus = new AccOpenLinesBUS();
                AccOpenLinesModel olmod = new AccOpenLinesModel();
                if (linesmodel != null)               //multimodel != null
                {
                    //if (multimodel.Count > 0)
                    //{
                    AccDebCreBUS dbb = new AccDebCreBUS();
                    AccDebCreModel dbm = new AccDebCreModel();
                    AccPaymentBUS apb = new AccPaymentBUS();
                    AccPaymentModel apm = new AccPaymentModel();
                    dbm = dbb.GetCustomerByAccCode(linesmodel.idClientLine);
                    if (dbm != null)
                    {
                        if (dbm.payCondition != null && dbm.payCondition != 0)
                        {
                            pc = dbm.payCondition;
                        }
                        else
                        {
                            pc = Convert.ToInt32(asm.defPayCondition);
                        }
                        apm = apb.GetPaymentByID(pc);
                        if (apm != null)
                        {
                            if (apm.numberDays != null && apm.numberDays != 0)
                                daysp = Convert.ToInt32(apm.numberDays);
                            else
                                daysp = 0;
                        }


                    }

                    //    foreach (AccLineModel itmol in multimodel)
                    //    {
                    olmod = olbus.GetAccOpenLinesByInvoice(linesmodel.invoiceNr, linesmodel.term);  //itmol.invoiceNr
                    if (olmod != null)
                    {

                        if (olmod.invoiceOpenLine.Trim() == linesmodel.invoiceNr.Trim() && olmod.term == linesmodel.term)
                        {


                            if (linesmodel.dtBooking != DateTime.MinValue)
                                //olmod.dtPayOpenLine = Convert.ToDateTime(linesmodel.dtBooking); //linesmodel.dtLine;
                                olmod.dtOpenLine = Convert.ToDateTime(linesmodel.dtBooking);
                            else
                                olmod.dtOpenLine = DateTime.Now;
                            //olmod.dtPayOpenLine = DateTime.Now;
                          //  olmod.dtPayOpenLine = Convert.ToDateTime(linesmodel.dtBooking); //linesmodel.dtLine;
                            olmod.referencePay = linesmodel.incopNr; // ubacujemo referecu koja stavka je zatvara
                            //if (xSideBooking == "C")
                            //{
                            // olmod.debitOpenLine = olmod.debitOpenLine + itmol.debitLine;                     //itmol.debitLine;
                            olmod.debitOpenLine = linesmodel.debitLine;                //olmod.debitOpenLine +
                            //}
                            //else
                            //{
                            // olmod.creditOpenLine = olmod.creditOpenLine + itmol.creditLine;                   // itmol.creditLine;
                            olmod.creditOpenLine = linesmodel.creditLine;           //olmod.creditOpenLine + 
                            //}
                            olmod.typeOpenLine = xSideBooking;
                            olmod.creditDays = daysp;
                            olmod.idPayCondition = pc;
                            olbus.Update(olmod, this.Name, Login._user.idUser);  // upisujemo open line stavku
                        }

                        // }
                    }

                    //}
                    // }
                }
            }
            else
            {
                RadMessageBox.Show("Can't edit open lines they are closed");
            }
        }
        private void Translation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblDate.Text) != null)
                    lblDate.Text = resxSet.GetString(lblDate.Text);

                if (resxSet.GetString(lblDesc.Text) != null)
                    lblDesc.Text = resxSet.GetString(lblDesc.Text);

                if (resxSet.GetString(lblAmount.Text) != null)
                    lblAmount.Text = resxSet.GetString(lblAmount.Text);

                if (resxSet.GetString(lblInvoice.Text) != null)
                    lblInvoice.Text = resxSet.GetString(lblInvoice.Text);

                if (resxSet.GetString(lblClient.Text) != null)
                    lblClient.Text = resxSet.GetString(lblClient.Text);

                if (resxSet.GetString(lblAccount.Text) != null)
                    lblAccount.Text = resxSet.GetString(lblAccount.Text);

                if (resxSet.GetString(lblBtw.Text) != null)
                    lblBtw.Text = resxSet.GetString(lblBtw.Text);

                if (resxSet.GetString(lblCost.Text) != null)
                    lblCost.Text = resxSet.GetString(lblCost.Text);

                if (resxSet.GetString(lblProject.Text) != null)
                    lblProject.Text = resxSet.GetString(lblProject.Text);

                if (resxSet.GetString(lblDebit.Text) != null)
                    lblDebit.Text = resxSet.GetString(lblDebit.Text);

                if (resxSet.GetString(lblCredit.Text) != null)
                    lblCredit.Text = resxSet.GetString(lblCredit.Text);
                               
                if (resxSet.GetString(lblDiff.Text) != null)
                    lblDiff.Text = resxSet.GetString(lblDiff.Text);
                if (resxSet.GetString(lblTdebit.Text) != null)
                    lblTdebit.Text = resxSet.GetString(lblTdebit.Text);
                if (resxSet.GetString(lblTcredit.Text) != null)
                    lblTcredit.Text = resxSet.GetString(lblTcredit.Text);
                if (resxSet.GetString(lblDiff.Text) != null)
                    lblDiff.Text = resxSet.GetString(lblDiff.Text);
                if (resxSet.GetString(btnOK.Text) != null)
                    btnOK.Text = resxSet.GetString(btnOK.Text);
                if (resxSet.GetString(btnCancel.Text) != null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);
                if (resxSet.GetString(btnSplit.Text) != null)
                    btnSplit.Text = resxSet.GetString(btnSplit.Text);
                if (resxSet.GetString(lblIncop.Text) != null)
                    lblIncop.Text = resxSet.GetString(lblIncop.Text);

            }
        }

        private void gridLines_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (e.Column.Name == "invoice")
            {
                _gridEditor = this.gridLines.ActiveEditor as BaseGridEditor;
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown += new KeyEventHandler(cellEditorInvoice_KeyDown);
                }
            }
            if (e.Column.Name == "account")
            {
                _gridEditor = this.gridLines.ActiveEditor as BaseGridEditor;
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown += new KeyEventHandler(cellEditorAccount_KeyDown);
                }
            }

            if (e.Column.Name == "client")
            {
                _gridEditor = this.gridLines.ActiveEditor as BaseGridEditor;
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown += new KeyEventHandler(cellEditorClient_KeyDown);
                }
            }
            if (e.Column.Name == "btw" && asm.isVat == true)
            {
                _gridEditor = this.gridLines.ActiveEditor as BaseGridEditor;
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown += new KeyEventHandler(cellEditorBtw_KeyDown);
                }
            }

            if (e.Column.Name == "cost")
            {
                _gridEditor = this.gridLines.ActiveEditor as BaseGridEditor;
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown += new KeyEventHandler(cellEditorCost_KeyDown);
                }
            }

            if (e.Column.Name == "project")
            {
                _gridEditor = this.gridLines.ActiveEditor as BaseGridEditor;
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown += new KeyEventHandler(cellEditorProject_KeyDown);
                }
            }
        }

        //private void gridLines_CellEndEdit(object sender, GridViewCellEventArgs e)
        //{
        //    if (e.Column.Name == "account")
        //    {
        //        if (_gridEditor != null)
        //        {
        //            RadItem element = _gridEditor.EditorElement as RadItem;
        //            element.KeyDown -= cellEditorAccount_KeyDown;
        //        }
        //        _gridEditor = null;
        //    }

        //    if (e.Column.Name == "client")
        //    {
        //        if (_gridEditor != null)
        //        {
        //            RadItem element = _gridEditor.EditorElement as RadItem;
        //            element.KeyDown -= cellEditorClient_KeyDown;
        //        }
        //        _gridEditor = null;
        //    }

        //    if (e.Column.Name == "cost")
        //    {
        //        if (_gridEditor != null)
        //        {
        //            RadItem element = _gridEditor.EditorElement as RadItem;
        //            element.KeyDown -= cellEditorCost_KeyDown;
        //        }
        //        _gridEditor = null;
        //    }

        //    if (e.Column.Name == "btw" && asm.isVat==true)
        //    {
        //        if (_gridEditor != null)
        //        {
        //            RadItem element = _gridEditor.EditorElement as RadItem;
        //            element.KeyDown -= cellEditorBtw_KeyDown;
        //        }
        //        _gridEditor = null;
        //    }

        //    if (e.Column.Name == "project")
        //    {
        //        if (_gridEditor != null)
        //        {
        //            RadItem element = _gridEditor.EditorElement as RadItem;
        //            element.KeyDown -= cellEditorProject_KeyDown;
        //        }
        //        _gridEditor = null;
        //    }

        //    if (e.Column.Name == "debit")
        //    {
        //        if (e.Value != null)
        //        {
        //            decimal d = (decimal)e.Value;
        //            if (d != 0)
        //            {
        //                GridViewRowInfo info = e.Row.ViewInfo.CurrentRow;
        //                info.Cells["credit"].Value = "0,00";
        //            }
        //        }
        //    }

        //    if (e.Column.Name == "credit")
        //    {
        //        if (e.Value != null)
        //        {
        //            decimal d = (decimal)e.Value;
        //            if (d != 0)
        //            {
        //                GridViewRowInfo info = e.Row.ViewInfo.CurrentRow;
        //                info.Cells["debit"].Value = "0,00";
        //            }
        //        }
        //    }
        //}

        #region GridEvants

        void cellEditorAccount_KeyDown(object sender, KeyEventArgs e)
        {
            RadTextBoxEditorElement editor = sender as RadTextBoxEditorElement;

            if (e.KeyCode == Keys.F2)
            {
                LedgerAccountBUS ccentar = new LedgerAccountBUS(Login._bookyear);
                List<IModel> gmX = new List<IModel>();

                gmX = ccentar.GetAllAccounts();
                var dlgSave = new GridLookupForm(gmX, "Ledger");

                if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                {
                    LedgerAccountModel genmX = new LedgerAccountModel();
                    genmX = (LedgerAccountModel)dlgSave.selectedRow;

                    if (genmX != null)
                    {
                        //set textbox
                        if (genmX.numberLedgerAccount != null)
                            editor.Text = genmX.numberLedgerAccount;
                    }
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {

                if (editor.Text != "")
                {
                    LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = ledbus.GetAccount(editor.Text, Login._bookyear);
                    if (lam != null)
                    {
                        if (lam.isActiveLedgerAccount == true)
                        {
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {
                                if (resxSet.GetString("Sorry, this account is not active!") != null)
                                    RadMessageBox.Show(resxSet.GetString("Sorry, this account is not active!"));
                                else
                                    RadMessageBox.Show("Sorry, this account is not active!");
                            }
                            editor.Text = "";
                            editor.Focus();
                        }
                        labelName.Text = lam.descLedgerAccount;

                    }
                    else
                    {
                        labelName.Text = "Wrong account";
                        editor.Focus();
                    }
                }
                else
                {
                    labelName.Text = "";
                }

            }


        }

        void cellEditorClient_KeyDown(object sender, KeyEventArgs e)
        {
            RadTextBoxEditorElement editor = sender as RadTextBoxEditorElement;

            if (e.KeyCode == Keys.F2)
            {
                //int xX = this.Location.X;
                //int yY = this.Location.Y;
                //int fWidth = this.Width / 2;
                //int fHight = this.Height / 2;
                //debcreGrid.Show(xX + fWidth, yY + fHight);

                DebCreLookupBUS debpers = new DebCreLookupBUS();
                List<IModel> pm1 = new List<IModel>();

                pm1 = debpers.GetCreditors();
                var dlgSave = new GridLookupForm(pm1, "Creditor");

                if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                {
                    DebCreLookupModel pm1X = new DebCreLookupModel();
                    pm1X = (DebCreLookupModel)dlgSave.selectedRow;

                    if (pm1X != null)
                    {
                        //set textbox
                        if (pm1X.accNumber != null)
                        {
                            editor.Text = pm1X.accNumber;
                            labelName.Text = pm1X.name;
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (editor.Text != "")
                {
                    AccDebCreBUS debpers = new AccDebCreBUS();
                    AccDebCreModel pm1 = new AccDebCreModel();
                    ClientBUS cb = new ClientBUS();
                    ClientModel cm = new ClientModel();
                    PersonBUS pbs = new PersonBUS();
                    PersonModel pmd = new PersonModel();

                    pm1 = debpers.GetCustomerByAccCode(editor.Text.ToString());
                    if (pm1 != null)
                    {
                        if (pm1.idClient != null && pm1.idContPerson == 0)
                        {
                            cm = cb.GetClient(pm1.idClient);
                            if (cm != null)
                            {

                                labelName.Text = cm.nameClient;

                            }
                            else
                            {
                                labelName.Text = "";

                            }
                        }
                        else
                        {
                            if (pm1.idContPerson != null && pm1.idClient == 0)
                                pmd = pbs.GetPerson(pm1.idContPerson);
                            if (pmd != null)
                            {
                                labelName.Text = pmd.firstname + " " + pmd.midname + " " + pmd.lastname;
                            }
                            else
                            {
                                labelName.Text = "";

                            }

                        }

                    }
                    else
                    {
                        labelName.Text = "Wrong id Customer !!!";
                        editor.Focus();
                    }


                }
                else
                {
                    labelName.Text = "";
                }
            }

        }
        void cellEditorInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            RadTextBoxEditorElement editor = sender as RadTextBoxEditorElement;

            if (e.KeyCode == Keys.F2)
            {
                labelName.Text = "";
                //AccOpenLinesBUS olb = new AccOpenLinesBUS();
                //List<IModel> olm = new List<IModel>();
                //string strClient = "";
                //olm = olb.GetAllOpenLines();
                //var dlgSave99 = new OpenLookupForm(olm, "Open Lines", strClient);

                //if (dlgSave99.ShowDialog(this) == DialogResult.Yes)
                //{
                //    AccOpenLinesModel pm1X = new AccOpenLinesModel();
                //    pm1X = (AccOpenLinesModel)dlgSave99.selectedRow;
                //    //set textbox
                //    //if (gridLines.CurrentRow.Cells["invoice"].Value != null)
                //    gridLines.CurrentRow.Cells["invoice"].Value = pm1X.invoiceOpenLine;
                //    gridLines.CurrentRow.Cells["description"].Value = pm1X.descOpenLine;
                //    gridLines.CurrentRow.Cells["account"].Value = pm1X.account;
                //    gridLines.CurrentRow.Cells["client"].Value = pm1X.idDebCre;
                //    if (gridLines.CurrentRow.Cells["cost"].Value == null)
                //        gridLines.CurrentRow.Cells["cost"].Value = pm1X.codeCost;
                //    if (gridLines.CurrentRow.Cells["project"].Value == null)
                //        gridLines.CurrentRow.Cells["project"].Value = pm1X.codeArr;
                //    //addmodelopl = new AccLineModel();
                //    //addmodelopl.invoiceNr = pm1X.invoiceOpenLine;
                //    //addmodelopl.descLine = pm1X.descOpenLine;
                //    //addmodelopl.idCostLine = pm1X.codeCost;
                //    //addmodelopl.idProjectLine = pm1X.codeArr;
                //    //addmodelopl.numberLedAccount = pm1X.account;
                //    //addmodelopl.idClientLine = pm1X.idDebCre;
                //    if (pm1X.typeOpenLine == "C")
                //    {
                //        gridLines.CurrentRow.Cells["debit"].Value = Convert.ToDecimal(pm1X.creditOpenLine);
                //        // addmodelopl.debitLine = Convert.ToDecimal(pm1X.creditOpenLine);
                //        // txtAmountC.Text = opm.creditOpenLine.ToString();
                //    }
                //    else
                //    {
                //        gridLines.CurrentRow.Cells["credit"].Value = Convert.ToDecimal(pm1X.debitOpenLine);
                //        //   addmodelopl.creditLine = Convert.ToDecimal(pm1X.debitOpenLine);
                //        //  txtAmountD.Text = opm.debitOpenLine.ToString();
                //    }
                //    //addmodel.debitLine = Convert.ToDecimal(itmol.debitOpenLine);
                //    //addmodel.creditLine = Convert.ToDecimal(itmol.creditOpenLine);
                //    //addmodelopl.incopNr = txtIncop.Text;
                //    //multimodel.Add(addmodelopl);
                //    //gridLines.DataSource = multimodel;
                //}
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {

                labelName.Text = "";
            }

        }
        void cellEditorBtw_KeyDown(object sender, KeyEventArgs e)
        {
            RadTextBoxEditorElement editor = sender as RadTextBoxEditorElement;

            if (e.KeyCode == Keys.F2)
            {
                AccTaxBUS ccentar = new AccTaxBUS();
                List<IModel> gmX = new List<IModel>();

                gmX = ccentar.GetAllTax(Login._user.lngUser);
                var dlgSave = new GridLookupForm(gmX, "Btw");

                if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                {
                    AccTaxModel genmX = new AccTaxModel();
                    genmX = (AccTaxModel)dlgSave.selectedRow;

                    if (genmX != null)
                    {
                        //set textbox
                        if (genmX.idTax != null)
                            editor.Text = genmX.idTax.ToString();
                    }
                }
            }
            //if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            //{
            //    if (editor.Text != "")
            //    {
            //        AccTaxBUS acc = new AccTaxBUS();
            //        AccTaxModel amc = new AccTaxModel();
            //        amc = acc.GetTaxByID(Convert.ToInt32(editor.Text));
            //        if (amc != null)
            //        {
            //            labelName.Text = amc.descTax;

            //        }
            //        else
            //        {
            //            labelName.Text = "Wrong Cost code";
            //            editor.Focus();
            //        }
            //    }
            //    else
            //    {
            //        labelName.Text = "";
            //    }
            //}

        }
        void cellEditorCost_KeyDown(object sender, KeyEventArgs e)
        {
            RadTextBoxEditorElement editor = sender as RadTextBoxEditorElement;

            if (e.KeyCode == Keys.F2)
            {
                AccCostBUS ccentar = new AccCostBUS();
                List<IModel> gmX = new List<IModel>();

                gmX = ccentar.GetAllCost();
                var dlgSave = new GridLookupForm(gmX, "Cost");

                if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                {
                    AccCostModel genmX = new AccCostModel();
                    genmX = (AccCostModel)dlgSave.selectedRow;

                    if (genmX != null)
                    {
                        //set textbox
                        if (genmX.codeCost != null)
                            editor.Text = genmX.codeCost;
                    }
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (editor.Text != "")
                {
                    AccCostBUS acc = new AccCostBUS();
                    AccCostModel amc = new AccCostModel();
                    amc = acc.GetCostByID(editor.Text);
                    if (amc != null)
                    {
                        labelName.Text = amc.descCost;

                    }
                    else
                    {
                        labelName.Text = "Wrong Cost code";
                        editor.Focus();
                    }
                }
                else
                {
                    labelName.Text = "";
                }
            }

        }

        void cellEditorProject_KeyDown(object sender, KeyEventArgs e)
        {
            RadTextBoxEditorElement editor = sender as RadTextBoxEditorElement;

            if (e.KeyCode == Keys.F2)
            {
                ArrangementBUS ccentar = new ArrangementBUS();
                List<IModel> gmX = new List<IModel>();

                gmX = ccentar.GetAllArrangementsAccount(Login._bookyear);
                var dlgSave = new GridLookupForm(gmX, "Arrangement");

                if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                {
                    ArrangementModel genmX = new ArrangementModel();
                    genmX = (ArrangementModel)dlgSave.selectedRow;

                    if (genmX != null)
                    {
                        //set textbox
                        if (genmX.codeProject != null)
                            editor.Text = genmX.codeProject;
                    }
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (editor.Text != "")
                {
                    ArrangementBUS arb = new ArrangementBUS();
                    ArrangementModel arm = new ArrangementModel();
                    arm = arb.GetArrangementByCode(editor.Text);
                    if (arm != null)
                    {
                        labelName.Text = arm.nameArrangement;

                    }
                    else
                    {
                        labelName.Text = "Wrong Arrangement code";
                        editor.Focus();
                    }

                }
                else
                {
                    labelName.Text = "";
                }
            }

        }

        //private void gridLines_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        //{
         
        //}

        //private void gridLines_CellEndEdit(object sender, GridViewCellEventArgs e)
        //{
          
        //}

        private void gridLines_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            if (e.PropertyName != null)
            {
                if (e.PropertyName != "")
                {
                    if (e.PropertyName == "debit" || e.PropertyName == "credit")
                        CalculateDebitCredit();
                }
            }
        }

        //private void gridLines_UserDeletedRow(object sender, GridViewRowEventArgs e)
        //{
        //    CalculateDebitCredit();
        //}

        private void gridLines_RowsChanged_1(object sender, GridViewCollectionChangedEventArgs e)
        {
            CalculateDebitCredit();
        }

        private void CalculateDebitCredit()
        {

        }




        private void gridLines_UserDeletedRow_1(object sender, GridViewRowEventArgs e)
        {
            RadMessageBox.Show("Deleting row");



        }

        #endregion GridEvants

        private void gridLines_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {
            DialogResult dr = RadMessageBox.Show("Do you want to DELETE this line ?", "Delete", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                if (gridLines.CurrentRow.DataBoundItem != null)
                {
                    AccLineModel selectedLine = new AccLineModel();
                    selectedLine = (AccLineModel)gridLines.CurrentRow.DataBoundItem;

                    if (lledit == true)
                    {
                        AccAcountUpdate asl = new AccAcountUpdate();
                        isSuccessfully = asl.SubstractAmount(selectedLine, this.Name, Login._user.idUser);
                        if (isSuccessfully == false)
                        {
                            RadMessageBox.Show("Error updating Ledger amount !!");
                            return;
                        }

                    }
                }
                gridLines.CurrentRow.Delete();
            }
            else
            {
                e.Cancel = true;
                return;
            }
            gridLines.Show();
        }

        private void txtPayingDays_ValueChanged(object sender, EventArgs e)
        {
            dpValutaDate.Value = txtDate.Value.AddDays(Convert.ToInt32(txtPayingDays.Value));
        }

        private void txtPayingDays_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtAccount.Focus();
            }
        }

        private void dpValutaDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtInvoice.Focus();
            }
        }

        private void gridLines_CellValidating(object sender, CellValidatingEventArgs e)
        {
            if (e.Row != null)
            {
                if(e.Column!=null)
                switch (e.Column.Name)
                {

                    case "account":



                        e.Row.ErrorText = string.Empty;
                        RadTextBoxEditor tbEditor = e.ActiveEditor as RadTextBoxEditor;
                        if (tbEditor != null && e.Column.Name == "account" && tbEditor.Value + "" == string.Empty && e.Row.Tag == null)
                        {
                            e.Row.ErrorText = "Empty value is not allowed!";
                            e.Cancel = true;
                            if (_gridEditor != null)
                            {
                                RadItem element = _gridEditor.EditorElement as RadItem;
                                element.KeyDown -= cellEditorAccount_KeyDown;
                            }
                            _gridEditor = null;
                            GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                            endEdit(cell);
                        }
                        else
                        {
                            if (tbEditor != null)
                            {
                                if (tbEditor.Value.ToString() != "")
                                {
                                    LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                                    LedgerAccountModel lam = new LedgerAccountModel();

                                    lam = ledbus.GetAccount(tbEditor.Value.ToString(), Login._bookyear);
                                    if (lam == null)
                                    {
                                        RadMessageBox.Show("Wrong account");

                                        e.Row.ErrorText = "Non extisting account";
                                        e.Cancel = true;

                                        GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                                        endEdit(cell);
                                    }
                                //    else
                                //    {
                                //        msg = true;
                                //    }
                                //}
                                //else
                                //{
                                //    msg = false;
                                }
                            }
                            //if (msg == false && e.Column.Name == "numberLedAccount")
                            //{
                            //    e.Row.ErrorText = "Non extisting account";
                            //    e.Cancel = true;

                            //    if (_gridEditor != null)
                            //    {
                            //        RadItem element = _gridEditor.EditorElement as RadItem;
                            //        element.KeyDown -= cellEditorAccount_KeyDown;
                            //    }

                            //    _gridEditor = null;
                            //    GridViewCellInfo cell = gridItems.Rows[e.Row.Index].Cells[e.Column.Index];

                            //    endEdit(cell);
                            //}
                        }

                        break;

                    case "invoice":


                        e.Row.ErrorText = string.Empty;
                        RadTextBoxEditor tbEditor1 = e.ActiveEditor as RadTextBoxEditor;
                        if (tbEditor1 != null && e.Column.Name == "invoice" && tbEditor1.Value + "" == string.Empty && e.Row.Tag == null)
                        {
                            e.Row.ErrorText = "Empty value is not allowed!";
                            e.Cancel = true;

                            GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                            endEdit(cell);
                            break;
                        }
                        else
                        {
                            if (tbEditor1 != null)
                            {
                                String s = ((Object)tbEditor1.Value ?? "").ToString();
                                if (txtInvoice.Text != s)   //tbEditor1.Value.ToString())
                                {
                                    e.Row.ErrorText = "Not the same invoice number";
                                    e.Cancel = true;


                                    GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                                    endEdit(cell);
                                    break;
                                }
                            }
                        }

                        break;

                    case "client":
                        e.Row.ErrorText = string.Empty;
                        RadTextBoxEditor tbEditor2 = e.ActiveEditor as RadTextBoxEditor;
                        if (tbEditor2 != null && e.Column.Name == "client" && tbEditor2.Value + "" == string.Empty && e.Row.Tag == null)
                        {
                            e.Row.ErrorText = "Empty value is not allowed!";
                            e.Cancel = true;
                            if (_gridEditor != null)
                            {
                                RadItem element = _gridEditor.EditorElement as RadItem;
                                element.KeyDown -= cellEditorClient_KeyDown;
                            }
                            _gridEditor = null;
                            GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                            endEdit(cell);
                        }
                        else
                        {
                            if (tbEditor2 != null && txtClient.Text != tbEditor2.Value.ToString() && e.Column.Name == "client")
                            {
                                RadMessageBox.Show("Not the same Client number");
                                e.Row.ErrorText = "Not the same Client number";
                                e.Cancel = true;
                                if (_gridEditor != null)
                                {
                                    RadItem element = _gridEditor.EditorElement as RadItem;
                                    element.KeyDown -= cellEditorClient_KeyDown;
                                }
                                _gridEditor = null;
                                GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                                endEdit(cell);
                            }
                        }
                        break;

                    case "btw":
                        e.Row.ErrorText = string.Empty;
                        RadTextBoxEditor tbEditor3 = e.ActiveEditor as RadTextBoxEditor;

                        if (tbEditor3 != null)
                        {
                            if (tbEditor3.Value.ToString() != "" && tbEditor3.Value.ToString() != "0")
                            {
                                AccTaxBUS ledbus = new AccTaxBUS();
                                AccTaxModel lam = new AccTaxModel();

                                lam = ledbus.GetTaxByID(Convert.ToInt32(tbEditor3.Value.ToString()));
                                if (lam == null)
                                {
                                    RadMessageBox.Show("Wrong BTW ");

                                    e.Row.ErrorText = "Non extisting BTW";
                                    e.Cancel = true;

                                    GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                                    endEdit(cell);
                                }
                            }
                        }
                        break;

                    case "project":
                        e.Row.ErrorText = string.Empty;
                        RadTextBoxEditor tbEditor4 = e.ActiveEditor as RadTextBoxEditor;

                        if (tbEditor4 != null)
                        {
                            if (tbEditor4.Value.ToString() != "" && tbEditor4.Value.ToString() != "0")
                            {
                                ArrangementBUS ledbus = new ArrangementBUS();
                                ArrangementModel lam = new ArrangementModel();

                                lam = ledbus.GetArrangementCodeProject(tbEditor4.Value.ToString());
                                if (lam == null)
                                {
                                    RadMessageBox.Show("Wrong project code ");

                                    e.Row.ErrorText = "Non project ";
                                    e.Cancel = true;

                                    GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                                    endEdit(cell);
                                }
                            }
                        }
                       

                        //if (e.Row.Cells["idBTW"].Value != null && e.Row.Cells["idBTW"].Value != "0")
                        //{
                        //    int id_tax = Convert.ToInt32(e.Row.Cells["idBTW"].Value);
                        //    string konto = getBTWKonto(id_tax);
                        //    decimal tax_amount = Convert.ToDecimal(e.Row.Cells["debitBTW"].Value);
                        //    e.Cancel = false;
                        //    GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                        //    endEdit(cell);

                        //    addBtw(tax_amount, konto);


                        //}

                        //   gridItems.DataSource = null;
                        //   gridItems.DataSource = lines;
                        break;

                    default:

                        break;
                }
            }
        }
        private void gridLines_CellEndEdit(object sender, GridViewCellEventArgs e)
        {


            GridViewCellInfo i = (GridViewCellInfo)e.Row.Cells[e.ColumnIndex];
            endEdit(i);

        }

        private void endEdit(GridViewCellInfo e)
        {


            //if (e.ColumnInfo.Name == "dtLine")
            //{
            //    if (_gridEditor != null)
            //    {
            //        RadItem element = _gridEditor.EditorElement as RadItem;
            //        element.KeyDown -= cellEditordtLine_KeyDown;
            //    }
            //    _gridEditor = null;
            //}
            if (e.ColumnInfo.Name == "invoiceNr")
            {
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown -= cellEditorInvoice_KeyDown;
                }
                _gridEditor = null;


            }
            if (e.ColumnInfo.Name == "account")
            {
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown -= cellEditorAccount_KeyDown;
                }
                _gridEditor = null;
            }
            //if (e.ColumnInfo.Name == "descLine")
            //{
            //    if (_gridEditor != null)
            //    {
            //        RadItem element = _gridEditor.EditorElement as RadItem;
            //        element.KeyDown -= cellEditordescLine_KeyDown;
            //    }
            //    _gridEditor = null;
            //}
            if (e.ColumnInfo.Name == "btw")
            {
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown -= cellEditorBtw_KeyDown;
                }
                _gridEditor = null;
            }
                //if (e.ColumnInfo.Name == "idBTW")
                //{
                //    if (e.Value != null)
                //    {
                //        int d = (int)e.Value;
                //        if (d != 0)
                //        {
                //            decimal percent = getPercent(d);
                //            GridViewRowInfo info = e.RowInfo.ViewInfo.CurrentRow;
                //            if (inc_excl == 2)
                //                info.Cells["debitBTW"].Value = Math.Abs(Convert.ToDecimal(gridItems.CurrentRow.Cells["debitLine"].Value) - Convert.ToDecimal(gridItems.CurrentRow.Cells["creditLine"].Value)) * percent / 100;
                //            else
                //            {
                //                if (inc_excl == 1)
                //                {

                //                }
                //            }
                //        }
                //    }
                //}
           // }

            if (e.ColumnInfo.Name == "client")
            {
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown -= cellEditorClient_KeyDown;
                }
                _gridEditor = null;
            }

            if (e.ColumnInfo.Name == "cost")
            {
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown -= cellEditorCost_KeyDown;
                }
                _gridEditor = null;
            }

            if (e.ColumnInfo.Name == "project")
            {
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown -= cellEditorProject_KeyDown;
                }
                _gridEditor = null;
            }

            //if (e.ColumnInfo.Name == "debitLine")
            //{
            //    if (e.Value != null)
            //    {
            //        decimal d = (decimal)e.Value;
            //        if (d != 0)
            //        {
            //            GridViewRowInfo info = e.RowInfo.ViewInfo.CurrentRow;
            //            info.Cells["creditLine"].Value = "0,00";
            //        }
            //    }
            //}

            //if (e.ColumnInfo.Name == "creditLine")
            //{
            //    if (e.Value != null)
            //    {
            //        decimal d = (decimal)e.Value;
            //        if (d != 0)
            //        {
            //            GridViewRowInfo info = e.RowInfo.ViewInfo.CurrentRow;
            //            info.Cells["debitLine"].Value = "0,00";
            //        }
            //    }
            //}
        }

        private void txtAmountD_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46 || e.KeyValue == 110)
            {
                var textBoxItem = this.txtAmountD.MaskedEditBoxElement.TextBoxItem;

                int indexOfDecimalSeparator = this.txtAmountD.MaskedEditBoxElement.TextBoxItem.Text.ToLower().IndexOf(',');
                if (indexOfDecimalSeparator + 1 <= textBoxItem.Text.Length)
                {
                    textBoxItem.SelectionStart = indexOfDecimalSeparator + 1;
                }
                else
                {
                    textBoxItem.SelectionStart = indexOfDecimalSeparator;
                }

                e.Handled = true;
            }
        }

        private void txtAmountC_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46 || e.KeyValue == 110)
            {
                var textBoxItem = this.txtAmountC.MaskedEditBoxElement.TextBoxItem;

                int indexOfDecimalSeparator = this.txtAmountC.MaskedEditBoxElement.TextBoxItem.Text.ToLower().IndexOf(',');
                if (indexOfDecimalSeparator + 1 <= textBoxItem.Text.Length)
                {
                    textBoxItem.SelectionStart = indexOfDecimalSeparator + 1;
                }
                else
                {
                    textBoxItem.SelectionStart = indexOfDecimalSeparator;
                }

                e.Handled = true;
            }
        }

        private void txtDate_Leave(object sender, EventArgs e)
        {
            if (txtDate.Value.Year.ToString() != Login._bookyear)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Booking year NOT same as date !!");
                txtDate.Focus();
            }
        }

        private void gridLines_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            string saveLayout = "Save Layout";
            using(ResXResourceSet resXset = new ResXResourceSet(Login.resxFile))
            {
                if (resXset.GetString(saveLayout) != null && resXset.GetString(saveLayout) != "")
                    saveLayout = resXset.GetString(saveLayout);
            }

            RadMenuItem customMenuItem = new RadMenuItem();
            customMenuItem.Text = saveLayout;
            customMenuItem.Click += SaveLayoutLineEntry;

            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);

            //===delete

            string saveLayout1 = "Delete Layout";

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                {
                    saveLayout = resxSet.GetString(saveLayout);
                }
            }
            RadMenuItem customMenuItem1 = new RadMenuItem();
            customMenuItem1.Text = saveLayout1;
            customMenuItem1.Click += SaveLayoutEntryD;
            RadMenuSeparatorItem separator1 = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator1);
            e.ContextMenu.Items.Add(customMenuItem1);
        }


        private void SaveLayoutLineEntry(object sender, EventArgs e)
        {
            if(File.Exists(layoutLineEntry))
            {
                File.Delete(layoutLineEntry);
            }
            gridLines.SaveLayout(layoutLineEntry);

            translateRadMessageBox trs = new translateRadMessageBox();
            trs.translateAllMessageBox("You have successfully save layout!");
        }

        private void SaveLayoutEntryD(object sender, EventArgs e)
        {
            if (File.Exists(layoutLineEntry))
            {
                File.Delete(layoutLineEntry);
            }

            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully delete layout!");
        }
      

    
                  
    }
}
