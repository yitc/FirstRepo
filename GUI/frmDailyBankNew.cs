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
using Microsoft;
using Telerik.WinControls.UI;
using System.Linq;
using System.Threading;
using System.IO;


namespace GUI
{
    public partial class frmDailyBankNew : Telerik.WinControls.UI.RadForm
    {
        public bool manDebitor = false;
        public bool manCreditor = false;
        public bool manCost = false;
        public bool manProject = false;
        public bool manBTW = false;
        public AccLineModel linesmodel;
        private int iID = -1;
        public int xDaily = -1;
        public BindingList<AccLineModel> multimodel;
        List<AccLineModel> listlines;
        AccLineModel model;
        private decimal btwtype;
        private decimal btwAmt;
        private string xConto;   // account Daily
        private string xContoSplit = ""; // account for first split line 
        private string xBtwConto;
        public string gClient;
        private string xcodeBTW = "";
        private DateTime xdtProjectStart = Convert.ToDateTime("1900-01-01");
        private AccDailyBankModel selectedDailyBank;
        private AccDailyModel selectedDaily;
        private int xCurr;
        public List<AccOpenLinesModel> oplinemodel;
        private AccLineModel addmodelopl;
        private bool isGrab = false; // kad izabere otvorenu stavku
        BaseGridEditor _gridEditor;
        private List<AccLineModel> listOldlines;
        public AccLineModel oldlinesmodel;
        public AccSettingsModel asm;
        public BindingList<AccOpenLinesModel> lookopenlines;
        private bool isShowMessage = true;
        private bool ledit = false;
        private string defaultSepa;
        private string xBankConto;

        public string layoutDailyBankNew;

        //clean code
        private GridViewCellInfo cellForKeyDown = null;
        private string defCreditor;
        private Boolean isGridOK;
        private Boolean canCloseForm = false;


        //update
        public frmDailyBankNew(AccDailyModel selectedDaily1, AccDailyBankModel selectedDailyBank1, AccLineModel selectedLine1)
        {
            selectedDailyBank = selectedDailyBank1;
            selectedDaily = selectedDaily1;
            ledit = true;
            iID = selectedLine1.idAccLine;
            xDaily = selectedDaily.idDaily;
            xCurr = selectedDailyBank.idDailyBank;
            InitializeComponent();
            //getIncopNr();
            this.Icon = Login.iconForm;

            if(selectedLine1.invoiceNr!="")
            {
                disableClearInvoiceAndSplit();
            }
        }
        //new
        public frmDailyBankNew(int ixID, int idDaily, string em, AccDailyBankModel selectedDailyBank1)
        {

            iID = ixID;
            xDaily = idDaily;
            selectedDailyBank = selectedDailyBank1;
            xCurr = selectedDailyBank.idDailyBank;
            InitializeComponent();
            getIncopNr();
            this.Icon = Login.iconForm;
            
        }

        private void disableClearInvoiceAndSplit()
        {
            btnSplit.Enabled = false;
            txtInvoice.Enabled = false;
        }



        private void frmDailyBankNew_Load(object sender, EventArgs e)
        {
            //clean code // depends on form type kass or bank
            AccDailyModel acdm = new AccDailyModel();
            acdm = new AccDailyBUS(selectedDailyBank.bookingYear).GetDailysByCode(selectedDailyBank.codeDaily);

            txtInvoice.ReadOnly = true; 

            string descDaily = "";
            if(acdm!=null)
            {
                descDaily = acdm.descDaily;
            }
            this.Text = descDaily;
            lblDebit.Text = lblDebit.Text + " " + descDaily.ToLower();
            lblCredit.Text = lblCredit.Text + " " + descDaily.ToLower();

            lblTdebit.Text = lblTdebit.Text + " " + descDaily.ToLower();
            lblTcredit.Text = lblTcredit.Text + " " + descDaily.ToLower();

            if (selectedDailyBank.codeDaily == "90")
            {
                lblStatement.Visible = false;
                txtStatement.Visible = false;
                lblDateStat.Visible = false;
                dpDateStatement.Visible = false;
                lblBegin.Visible = false;
                txtBeginSaldo.Visible = false;
                lblEndSald.Visible = false;
                txtEndSaldo.Visible = false;
                lblDiff.Visible = false;
                txtDiffBook.Visible = false;
                lblBooked.Visible = false;
                txtBook.Visible = false;
            }

            layoutDailyBankNew = MainForm.gridFiltersFolder + "\\layoutDailyBankNew.xml";

            //Restriction for mouse wheel
            txtAmountD.MaskedEditBoxElement.EnableMouseWheel = false;
            txtAmountC.MaskedEditBoxElement.EnableMouseWheel = false;

            this.gridItems.CellEditorInitialized += gridItems_CellEditorInitialized;

            //clean code // depends on form type kass or bank
            
            //List<AccOpenLinesModel> lookopenlines = new List<AccOpenLinesModel>();

            List<AccLineModel> dbmod = new List<AccLineModel>();

            //================= telerik code za mask polje
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("nl-NL");
            txtAmountC.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            txtAmountC.Mask = "N2";
            txtAmountC.Culture = new System.Globalization.CultureInfo("nl-NL");

            this.txtAmountC.KeyUp += txtAmountC_KeyUp;

            txtAmountD.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            txtAmountD.Mask = "N2";
            txtAmountD.Culture = new System.Globalization.CultureInfo("nl-NL");

            this.txtAmountD.KeyUp += txtAmountC_KeyUp;
            //================================================


            labelProject1.Text = "";
            labelCost.Text = "";
            labelProject.Text = "";
            txtArrdate.Text = "";
            labelClient.Text = "";
            labelKonto.Text = "";
            labelBtw.Text = "";
            labelBasicKonto.Text = "";


            labelName.Text = "";
            // Read parameters
            AccSettingsBUS asb = new AccSettingsBUS();
            asm = new AccSettingsModel();
            asm = asb.GetSettingsByID(DateTime.Now.Year.ToString());
            if (asm != null)
            {
                if (asm.isVat == false)
                {
                    lblBtw.Visible = false;
                    txtBtwCode.Visible = false;
                    labelBtw.Visible = false;
                    btnBtw.Visible = false;
                }
                defaultSepa = asm.defSepaAcc;   // default account for Sepa booking
                if (defaultSepa == "")
                    defaultSepa = "1610";
            }


            Translation();

            if (xDaily != -1)
            {
                //======== cita Daily da pokupi konto i vrstu naloga ====
                AccDailyBUS dyb = new AccDailyBUS(Login._bookyear);
                AccDailyModel dym = new AccDailyModel();
                dym = dyb.GetDailysById(xDaily);
                if (dym != null)
                {
                    xConto = dym.numberLedgerAccount;
                    xBankConto = dym.numberLedgerAccount;
                }
            }

            txtStatement.Text = selectedDailyBank.refNo.ToString();
            dpDateStatement.Text = selectedDailyBank.dtStatement.Value.ToShortDateString();
            txtBeginSaldo.Text = selectedDailyBank.begSaldo.ToString();
            txtEndSaldo.Text = selectedDailyBank.endSaldo.ToString();

            if (iID != -1)
            {
                txtAmountC.ValueChanged -= txtAmountC_ValueChanged;
                txtAmountD.ValueChanged -= txtAmountD_ValueChanged;


                //=========== cita stavku ================
                linesmodel = new AccLineModel();
                oldlinesmodel = new AccLineModel();
                AccLineBUS bus = new AccLineBUS(Login._bookyear);
                linesmodel = bus.GetLine(iID);
                oldlinesmodel = bus.GetLine(iID);                    //linesmodel;

                xDaily = linesmodel.idAccDaily;


                //code for changing only last one
                AccDailyBankModel acbm = new AccDailyBankModel();
                //List<int> list =  new List<int>(); 
                List<AccDailyBankModel> acbmList = new List<AccDailyBankModel>();
                AccDailyBankBUS dbBus = new AccDailyBankBUS(Login._bookyear);
                acbmList = dbBus.GetAllByDaily(selectedDaily.codeDaily);

                if (acbmList != null)
                {
                    acbm = acbmList.OrderByDescending(item => item.refNo).FirstOrDefault();
                }

                if (acbm != null)
                    if (acbm.refNo != selectedDailyBank.refNo && descDaily != "Kas")   //&& descDaily!="Kass"
                    {
                        btnSplit.Enabled = false;
                        btnOK.Enabled = true;
                    }
                //////

                if (linesmodel == null)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Error !! Can't read record ");
                    return;
                }
                
                
                
                if (linesmodel.statusLine == true)      // disabluje izmene ako je status proknjizen
                {
                    btnOK.Enabled = false;
                    btnSplit.Enabled = false;
                    gridItems.AllowEditRow = false;
                }


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
                // === kupi sta je obavezno za unos ===
                manCreditor = lam.mandatoryCreditorAccount;
                manDebitor = lam.mandatoryDebitorAccount;
                manCost = lam.mandatoryCostAccount;
                manProject = lam.mandatoryProjectAccount;
                manBTW = lam.isBTWLedgerAccount;
                //=======================================
                labelBasicKonto.Text = lam.numberLedgerAccount + "  " + lam.descLedgerAccount;
                if (linesmodel.incopNr != null)
                    txtIncop.Text = linesmodel.incopNr.ToString();
                txtDate.Text = linesmodel.dtLine.ToString();
                //   if (aSide == "D")
                txtAmountD.Text = linesmodel.debitLine.ToString();
                if (linesmodel.versil != null)
                txtBook.Text = linesmodel.versil.ToString();
                //  if (aSide == "C")
                txtAmountC.Text = linesmodel.creditLine.ToString();
                if (linesmodel.invoiceNr != null)
                    txtInvoice.Text = linesmodel.invoiceNr.ToString();
                if (linesmodel.descLine != null)
                    txtDesc.Text = linesmodel.descLine.ToString();
                if (linesmodel.incopNr != null)
                    txtIncop.Text = linesmodel.incopNr.ToString();
                if (linesmodel.numberLedAccount != null)
                    txtAccount.Text = linesmodel.numberLedAccount;

                if (linesmodel.idClientLine != "" && linesmodel.idClientLine != null)
                {
                    txtClient.Text = linesmodel.idClientLine.ToString();

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
                else
                {

                    labelClient.Text = "";
                }
                // prikazuje total
                decimal dtot = linesmodel.debitLine;
                txtTdebit.Text = dtot.ToString();
                decimal ctot = linesmodel.creditLine;
                txtTcredit.Text = ctot.ToString();
                decimal adiff = dtot - ctot;
                txtTdiff.Text = adiff.ToString();
                //
                CalcDiff();

                //==== cost ======
                if (linesmodel.idCostLine != "" && linesmodel.idCostLine != null)
                {
                    txtCost.Text = linesmodel.idCostLine.ToString();

                    AccCostBUS adcbus1 = new AccCostBUS();
                    AccCostModel cam2 = new AccCostModel();
                    cam2 = adcbus1.GetCostByID(linesmodel.idCostLine.ToString());
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
                        //txtArrdate.Text = "Start " + Convert.ToString(gmX.dtFromArrangement) + " End  " + Convert.ToString(gmX.dtToArrangement);
                        txtArrdate.Text = "Start date =>" + gmX.dtFromArrangement.ToShortDateString() + " - " + "end date =>" + gmX.dtToArrangement.ToShortDateString();

                        if (gmX.dtFromArrangement != null)
                            xdtProjectStart = Convert.ToDateTime(gmX.dtFromArrangement);
                    }
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
                dbmod = new List<AccLineModel>();
                //  multimodel = new List<AccLineModel>();
                listOldlines = new List<AccLineModel>();

                dbmod = lin2.GetAllLinesByNumber(linesmodel.incopNr, 0);
                if (dbmod != null)
                {
                    if (dbmod.Count <= 2)  // ovde puni gornji deo forme ako ima jednu ili jednu sa porezom stavku
                    {
                        txtAccount.Text = dbmod[0].numberLedAccount;
                        if (dbmod[0].idBTW != 0)
                            txtBtwCode.Text = dbmod[0].idBTW.ToString();
                    }
                }

                if (dbmod != null)
                    multimodel = new BindingList<AccLineModel>(dbmod);
                listOldlines = lin2.GetAllLinesByNumber(linesmodel.incopNr, 0);

                if (txtAccount.Text != "")
                {
                    LedgerAccountBUS ledbus3 = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam3 = new LedgerAccountModel();

                    lam3 = ledbus3.GetAccount(txtAccount.Text, Login._bookyear);
                    if (lam3 != null)
                    {
                        if (txtAccount.Text.Trim() == lam3.numberLedgerAccount.Trim())
                        {
                            labelKonto.Text = lam3.descLedgerAccount;
                            xContoSplit = lam3.numberLedgerAccount;

                        }
                    }
                }

                txtAmountC.ValueChanged += txtAmountC_ValueChanged;
                txtAmountD.ValueChanged += txtAmountD_ValueChanged;

                gridItems.DataSource = null;
                gridItems.DataSource = multimodel;
                if (asm.isVat == false)
                {
                    gridItems.Columns["btw"].IsVisible = false;
                }
                gridItems.Show();
                gridItems.Focus();
                // totals();
                txtDate.TabIndex = 2;
                txtAmountD.TabIndex = 1;
                txtAmountD.Focus();
            }
            else
            {
                txtDate.Text = selectedDailyBank.dtStatement.ToString(); // DateTime.Now.ToShortDateString();
                CalcDiff();
                // unosi novi slog =============
                linesmodel = new AccLineModel();

                LedgerAccountBUS ledbus1 = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lam1 = new LedgerAccountModel();


                lam1 = ledbus1.GetAccount(xConto, Login._bookyear);
                xConto = lam1.numberLedgerAccount;
                labelBasicKonto.Text = lam1.numberLedgerAccount + "  " + lam1.descLedgerAccount;
                // === kupi sta je obavezno za unos ===
                manCreditor = lam1.mandatoryCreditorAccount;
                manDebitor = lam1.mandatoryDebitorAccount;
                manCost = lam1.mandatoryCostAccount;
                manProject = lam1.mandatoryProjectAccount;
                manBTW = lam1.isBTWLedgerAccount;
                //=======================================
                linesmodel.idAccDaily = xDaily;

            }



            //clean code
            AccSettingsBUS sb = new AccSettingsBUS();
            AccSettingsModel sm = new AccSettingsModel();
            sm = sb.GetSettingsByID(Login._bookyear);
            if (sm != null)
            {
                if (sm.defReservationAcc != null && sm.defReservationAcc != "")
                {
                    defCreditor = sm.defCreditorAccount;

                }
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
            linesmodel.idCurrency = xCurr;
            linesmodel.idBTW = 0;
            linesmodel.booksort = 0;
            linesmodel.creditBTW = 0;
            linesmodel.debitBTW = 0;
            linesmodel.creditCurr = 0;
            linesmodel.debitCurr = 0;
            linesmodel.debitLine = 0;
            linesmodel.creditLine = 0;
            linesmodel.dtBooking = DateTime.Now;                    //Convert.ToDateTime("2000-01-01");
            linesmodel.currrate = 0;
            //========================

            AccAcountUpdate aup = new AccAcountUpdate();
            linesmodel.numberLedAccount = xConto;
            //  DateTime mper = Convert.ToDateTime(linesmodel.dtLine);
            linesmodel.periodLine = aup.Period(linesmodel.dtLine); //mper.Month;
            linesmodel.dtLine = Convert.ToDateTime(txtDate.Text);
            linesmodel.debitLine = Convert.ToDecimal(txtAmountD.Text);
            linesmodel.creditLine = Convert.ToDecimal(txtAmountC.Text);

            if (txtInvoice.Text != "")
                linesmodel.invoiceNr = txtInvoice.Text;
            else
            {
                if (txtClient.Text != "")
                {
                    if (multimodel.Count == 1)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Can't save without Invoice number !!!");
                        return;
                    }
                }
            }
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


        }


        #region Field Controls

        private void txtAccount_Leave(object sender, EventArgs e)
        {
            if (txtAccount.Text != "")
            {
                LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lam = new LedgerAccountModel();

                lam = ledbus.GetAccount(txtAccount.Text, Login._bookyear);
                if (lam != null)
                {
                    if (txtAccount.Text.Trim() == lam.numberLedgerAccount.Trim())
                    {
                        labelKonto.Text = lam.descLedgerAccount;
                        xContoSplit = lam.numberLedgerAccount;
                        //   xSideBooking = lam.sideBooking;
                        txtBtwCode.Focus();
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Wrong account");
                        txtAccount.Focus();
                    }
                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Wrong account");
                    labelKonto.Text = "";
                    txtAccount.Focus();
                }
            }
            else
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Wrong account");
                labelKonto.Text = "";
                //txtAccount.Focus();
            }
        }
        //==============================================  kretanje sa Enterom ===========================================
        private void txtDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {

                txtAmountD.Focus();
                //txtDesc.Focus();
            }
        }
        private void txtInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            // multimodel = new List<AccLineModel>();
            if (multimodel == null)
                multimodel = new BindingList<AccLineModel>();

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {


                if (txtInvoice.Text == "")
                {
                    txtDesc.Focus();
                    hideOpenLine(false);
                    //  txtClient.Focus();
                }
                else
                {
                    if (txtInvoice.Text != "")
                    {
                        txtInvoice.Leave -= txtInvoice_Leave;
                        hideOpenLine(true);
                        AccOpenLinesBUS opb = new AccOpenLinesBUS();
                        AccOpenLinesModel opm = new AccOpenLinesModel();
                        //  multimodel = new List<AccLineModel>();
                        //multimodel = new BindingList<AccLineModel>();
                        opm = opb.GetAccOpenLinesByInvoiceNoTerm(txtInvoice.Text);
                        if (opm != null)
                        {
                            if (opm.invoiceOpenLine != null)
                            {
                                if (txtInvoice.Text == opm.invoiceOpenLine.ToString())
                                {
                                    txtClient.Text = opm.idDebCre.ToString();
                                    //================================== 
                                    txtInvoice.Text = opm.invoiceOpenLine;      // prebacuje na formu
                                    txtDesc.Text = opm.descOpenLine;            // prebacuje na formu
                                    txtAccount.Text = opm.account;
                                    giveLabels();
                                    //==================================
                                    isGrab = true;
                                    addmodelopl = new AccLineModel();
                                    addmodelopl.invoiceNr = opm.invoiceOpenLine;
                                    addmodelopl.descLine = opm.descOpenLine;
                                    addmodelopl.idCostLine = opm.codeCost;
                                    addmodelopl.idProjectLine = opm.codeArr;
                                    addmodelopl.numberLedAccount = opm.account;
                                    addmodelopl.idClientLine = opm.idDebCre;
                                    addmodelopl.debitLine = 0;
                                    addmodelopl.creditLine = 0;
                                    if (opm.creditOpenLine != 0)
                                    {
                                        if (Convert.ToDecimal(txtAmountC.Text) < Convert.ToDecimal(opm.creditOpenLine))
                                            addmodelopl.debitLine = Convert.ToDecimal(txtAmountC.Text);
                                        else
                                            addmodelopl.debitLine = Convert.ToDecimal(opm.creditOpenLine);
                                        //  txtAmountC.Text = opm.creditOpenLine.ToString();
                                    }
                                    else
                                    {
                                        if (Convert.ToDecimal(txtAmountD.Text) < Convert.ToDecimal(opm.creditOpenLine))
                                            addmodelopl.creditLine = Convert.ToDecimal(txtAmountD.Text);
                                        else
                                            addmodelopl.creditLine = Convert.ToDecimal(opm.debitOpenLine);
                                        //  txtAmountD.Text = opm.debitOpenLine.ToString();
                                    }
                                    addmodelopl.incopNr = txtIncop.Text;
                                    txtDesc.Focus();
                                    // txtAccount.Focus();
                                }
                                multimodel.Add(addmodelopl);
                                gridItems.DataSource = multimodel;
                                btnSplit.Enabled = false;
                                txtDesc.Focus();
                            }
                        }
                        else
                        {
                            txtDesc.Focus();
                        }
                    }
                }
            }
            else
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (txtAmountD.Text == "0,00" && txtAmountC.Text == "0,00")
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Amount is mandatory !!!");
                        return;
                    }
                    else
                    {
                        if (txtAmountC.Text == "0,00" && txtAmountD.Text == "0,00")
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Amount is mandatory !!");
                            return;
                        }
                        else
                        {
                            btnOpenLines.PerformClick();
                            //if (lookopenlines == null)
                            //{
                            //    lookopenlines = new BindingList<AccOpenLinesModel>();
                            //}
                            //AccOpenLinesBUS debpers = new AccOpenLinesBUS();
                            //List<AccOpenLinesModel> oplinemodel = new List<AccOpenLinesModel>();
                            //if (txtClient.Text != "")
                            //{
                            //    oplinemodel = debpers.GetAccOpenLinesByID(txtClient.Text);   //debpers.GetAccOpenLinesByID(txtClient.Text);          //GetCreditors();
                            //    if (oplinemodel != null)
                            //    {
                            //        if (oplinemodel.Count > 0)
                            //        {

                            //            for (int i = 0; i < oplinemodel.Count; i++)
                            //            {
                            //                oplinemodel[i].iselected = false;
                            //            }
                            //        }

                            //    }
                            //    lookopenlines = new BindingList<AccOpenLinesModel>(oplinemodel);
                            //}
                            //else
                            //{
                            //    oplinemodel = debpers.GetAllOpenLinesM();
                            //    if (oplinemodel != null)
                            //    {
                            //        if (oplinemodel.Count > 0)
                            //        {

                            //            for (int i = 0; i < oplinemodel.Count; i++)
                            //            {
                            //                oplinemodel[i].iselected = false;
                            //            }
                            //        }

                            //    }
                            //    lookopenlines = new BindingList<AccOpenLinesModel>(oplinemodel);
                            //}
                            //if (multimodel == null)
                            //    multimodel = new BindingList<AccLineModel>();
                            //if (oplinemodel.Count > 0)
                            //{
                            //    for (int q = 0; q < oplinemodel.Count; q++)
                            //    {
                            //        for (int w = 0; w < multimodel.Count; w++)
                            //        {
                            //            if (oplinemodel[q].invoiceOpenLine == multimodel[w].invoiceNr)
                            //                oplinemodel.RemoveAt(q);
                            //        }
                            //    }
                            //}

                        //    decimal amount = 0;
                        //    //if (Convert.ToDecimal(txtAmountD.Text) != 0)
                        //    //    amount = Convert.ToDecimal(txtAmountD.Text);
                        //    //else
                        //    //    amount = Convert.ToDecimal(txtAmountC.Text);
                        //    if (Convert.ToDecimal(txtTdiff.Text) != 0)
                        //        amount = Convert.ToDecimal(txtTdiff.Text);
                        //    else
                        //        if (Convert.ToDecimal(txtAmountD.Text) != 0)
                        //            amount = Convert.ToDecimal(txtAmountD.Text);
                        //        else
                        //            amount = Convert.ToDecimal(txtAmountC.Text) * -1;
                        //    string side;
                        //    if (Convert.ToDecimal(txtAmountC.Text) != 0)
                        //        side = "C";
                        //    else
                        //        side = "D";
                        //     multimodel = new BindingList<AccLineModel>();
                        //    var dlgSave = new OpenLookupForm(oplinemodel, "Open Lines", txtClient.Text, amount, xDaily, multimodel, side);

                        //    if (dlgSave.ShowDialog() == DialogResult.Yes)
                        //    {
                        //        decimal razl = 0;
                        //        AccLineModel addmodel = new AccLineModel();
                        //        if (multimodel == null)
                        //            multimodel = new BindingList<AccLineModel>();
                        //        gridItems.DataSource = null;
                        //        gridItems.DataSource = multimodel;
                        //        if (gridItems != null)
                        //        {
                        //            if (gridItems.RowCount == 1)
                        //            {
                        //                if (txtClient.Text == "")
                        //                    txtClient.Text = gridItems.Rows[0].Cells["client"].Value.ToString();
                        //                if (txtDesc.Text == "")
                        //                    txtDesc.Text = gridItems.Rows[0].Cells["description"].Value.ToString();
                        //                if (txtCost.Text == "")
                        //                    txtCost.Text = gridItems.Rows[0].Cells["cost"].Value.ToString();
                        //                if (txtProject.Text == "")
                        //                    txtProject.Text = gridItems.Rows[0].Cells["project"].Value.ToString();
                        //                if (txtAccount.Text == "")
                        //                    txtAccount.Text = gridItems.Rows[0].Cells["account"].Value.ToString();
                        //                if (txtInvoice.Text == "")
                        //                    txtInvoice.Text = gridItems.Rows[0].Cells["invoice"].Value.ToString();

                        //                giveLabels();
                        //            }
                        //            if (multimodel != null && multimodel.Count > 0)
                        //            {
                        //                for (int y = 0; y < multimodel.Count; y++)
                        //                {
                        //                    multimodel[y].dtLine = txtDate.Value;//Convert.ToDateTime(txtDate.Text);
                        //                }

                        //            }
                        //            gridItems.DataSource = null;
                        //            gridItems.DataSource = multimodel;
                        //            gridItems.Show();
                        //        }

                        //    }
                        //    totals();
                        //    txtDesc.Focus();
                       }
                    }
                }

            }

        }

        private void txtDesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtDesc.Text == "")
                {
                   
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Enter description, please ");
                   
                    txtDesc.Focus();
                }
                else
                    txtAccount.Focus();
                //txtAmountC.Focus();
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

                int xX = this.Location.X;
                int yY = this.Location.Y;
                debcreCMenu.Show(xX + 220, yY + 100);

                txtClient.Focus();
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
                                txtInvoice.Focus();
                            }
                            else
                            {
                                txtClient.Text = "";
                                labelClient.Text = "";
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
                                txtInvoice.Focus();
                            }
                            else
                            {
                                txtClient.Text = "";
                                labelClient.Text = "";
                                txtClient.Focus();
                            }
                            txtClient.Focus();
                        }
                        // txtAccount.Focus();
                        txtInvoice.Focus();
                    }
                    // txtAccount.Focus();
                    txtInvoice.Focus();
                }
                else
                {
                    labelClient.Text = "";
                    if (txtClient.Text == "" && (manCreditor == true || manDebitor == true))
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Client mandatory !");
                        txtClient.Focus();
                    }
                    else
                    {

                        // txtAccount.Focus();
                        txtInvoice.Focus();
                    }
                }
                lookupOpenLine();
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
                    txtBtwCode.Focus();
                }
            }
            else
            {
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
                               

                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translateAllMessageBox("Sorry, this account is not active!");
                                txtAccount.Text = "";
                                txtAccount.Focus();
                                return;
                            }


                            labelKonto.Text = lam.descLedgerAccount;
                            xContoSplit = lam.numberLedgerAccount;
                            //   xSideBooking = lam.sideBooking;
                            txtBtwCode.Focus();
                        }
                        else
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Wrong accont");
                            txtAccount.Text = "";
                            labelKonto.Text = "";
                            txtAccount.Focus();
                        }
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Wrong accont");
                        txtAccount.Text = "";
                        labelKonto.Text = "";
                        txtAccount.Focus();
                    }

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

                    txtCost.Focus();
                }

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
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Only digit ...");
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

                                txtCost.Focus();
                            }
                            else
                            {
                                labelBtw.Text = "";
                                txtBtwCode.Text = "";
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("Wrong BTW code");
                                txtBtwCode.Focus();
                            }
                        }
                    }
                    else
                    {
                        labelBtw.Text = "";
                        txtCost.Focus();
                    }
                    txtCost.Focus();
                }

            }
        }
        private void txtAmountD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (Convert.ToDecimal(txtAmountD.Text) != 0)
                {
                    txtClient.Focus();
                }
                else
                {
                    txtAmountC.Focus();
                }
            }

            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                e.Handled = true;
        }
        private void txtAmountC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {

                txtClient.Focus();
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
                    labelCost.Text = "";
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
                        //txtArrdate.Text = "Start " + Convert.ToString(arm.dtFromArrangement) + " End " + Convert.ToString(arm.dtToArrangement);
                        txtArrdate.Text = "Start date =>" + arm.dtFromArrangement.ToShortDateString() + " - " + "end date =>" + arm.dtToArrangement.ToShortDateString();
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
                    //if (btnSplit.Enabled == true)
                    btnSplit.Focus();
                    labelProject.Text = "";
                    txtArrdate.Text = "";
                    //else
                    //    btnOK.Focus();
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
                        //txtArrdate.Text = "Start " + Convert.ToString(genmX.dtFromArrangement) + " End " + Convert.ToString(genmX.dtToArrangement);
                        txtArrdate.Text = "Start date => " + genmX.dtFromArrangement.ToShortDateString() + " - " + "end date =>" + genmX.dtToArrangement.ToShortDateString(); 
                        if (genmX.dtFromArrangement != null)
                            xdtProjectStart = Convert.ToDateTime(genmX.dtFromArrangement);
                    }
                }
        }

        private void txtAmountC_Leave(object sender, EventArgs e)
        {
            decimal dtot = Decimal.Parse(txtAmountD.Text.ToString());
            txtTdebit.Text = dtot.ToString();
            decimal ctot = Decimal.Parse(txtAmountC.Text.ToString());
            txtTcredit.Text = ctot.ToString();
            totals();
            if (ctot != 0)
                txtClient.Focus();
            //  txtInvoice.Focus();
            else
                txtAmountD.Focus();

        }

        private void txtAmountD_Leave(object sender, EventArgs e)
        {
            decimal dtot = Decimal.Parse(txtAmountD.Text.ToString());
            txtTdebit.Text = dtot.ToString();
            decimal ctot = Decimal.Parse(txtAmountC.Text.ToString());
            txtTcredit.Text = ctot.ToString();
            totals();
            if (dtot != 0 && ctot != 0)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Check amount, please");
                txtAmountD.Focus();
            }
            else
            {
                if (Convert.ToDecimal(txtAmountD.Text) != 0)
                    txtClient.Focus();
                else
                    txtAmountC.Focus();
                //txtInvoice.Focus();
            }
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
            Save();
           // if (isGridOK == true)
           //     canCloseForm = true;
          //  if (canCloseForm = true)
            //    this.Close();
        }

        private void Save()
        {
            bool isSuccessfully = false;
            isShowMessage = true;
            // GetDifference();
            isGridOK = true;
            if (txtTdiff.Text != "0,00" && isShowMessage == true)
            {

                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Not in Balance !");
                return;

            }
            else
            {
                if (txtInvoice.Text == "" && txtClient.Text != "" && multimodel.Count == 1)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Can't save without invoice number");
                    return;
                }
                if (txtInvoice.Text != "" && txtClient.Text == "" && txtAccount.Text == "")
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Can't save without customer number");
                    return;
                }

                isGridOK = checkGriddata();
                AccOpenLinesBUS olb = new AccOpenLinesBUS();

                bool isOk = false;
                if (multimodel.Count > 0 && Convert.ToDecimal(txtTdiff.Text) == 0 && isGridOK == true)
                {
                    updateOpenLinesOLD();  // rasknjizava open lines sa starim vrednostima //
                    AccLineBUS ptb = new AccLineBUS(Login._bookyear);

                    fillLines();
                    
                    // AccLineBUS aclbus = new AccLineBUS();
                    if (iID != -1)
                    {
                        AccAcountUpdate aaU = new AccAcountUpdate();
                        isSuccessfully = aaU.SubstractAmount(oldlinesmodel, this.Name, Login._user.idUser);

                        isOk = ptb.Update(linesmodel, this.Name, Login._user.idUser);
                        if (isOk == true)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Updated");
                            // iID = 0;
                        }
                        else
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Error updating line");

                        }
                        if (multimodel != null)
                        {
                            iID = 0;
                            saveItems();

                            AccAcountUpdate aaU1 = new AccAcountUpdate();
                            if (listOldlines != null)
                            {
                                for (int js = 0; js < listOldlines.Count; js++)
                                {
                                    isSuccessfully = aaU1.SubstractAmount(listOldlines[js], this.Name, Login._user.idUser);
                                }
                            }
                            ptb.DeleteByReference(linesmodel.incopNr, this.Name, Login._user.idUser);

                            for (int jm = 1; jm < listlines.Count; jm++)
                            {
                                if (ptb.Save(listlines[jm], this.Name, Login._user.idUser) == true)
                                {
                                    isSuccessfully = true;
                                    isSuccessfully = aaU1.AddAmount(listlines[jm], this.Name, Login._user.idUser);
                                    iID = -1;

                                }
                                else
                                {
                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translateAllMessageBox("You have NOT successfully save lines " + (jm + 1).ToString());
                                }
                            }
                            updateOpenLines();
                        }
                    }
                    else
                    {
                        isOk = ptb.Save(linesmodel, this.Name, Login._user.idUser);
                        if (isOk == true)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Saved");
                            AccAcountUpdate acUpdate = new AccAcountUpdate();
                            isSuccessfully = acUpdate.AddAmount(linesmodel, this.Name, Login._user.idUser);
                        }
                        else
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Error saving line");

                        }

                        if (multimodel != null)
                        {
                            saveItems();
                            //for (int jm = 0; jm < listlines.Count; jm++)
                            for (int jm = 1; jm < listlines.Count; jm++)
                            {
                                if (ptb.Save(listlines[jm], this.Name, Login._user.idUser) == true)
                                {
                                    isSuccessfully = true;
                                    AccAcountUpdate acUpdate = new AccAcountUpdate();
                                    isSuccessfully = acUpdate.AddAmount(listlines[jm], this.Name, Login._user.idUser);
                                    // updateOpenLines();
                                    //    RadMessageBox.Show("Saved lines ");
                                }
                                else
                                {
                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translatePartAndNonTranslatedPart("You have not successfully save lines ", (jm + 1).ToString());
                                }
                            }
                        }
                        updateOpenLines();
                    }


                    clearControl();
                    CalcDiff();

                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("No conditions for save");

                    txtInvoice.Focus();
                }



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
            txtAccount.Text = "";
            txtBtwCode.Text = "";
            txtAmountC.Text = "0,00";
            txtAmountD.Text = "0,00";
            txtCost.Text = "";
            txtProject.Text = "";
            labelProject1.Text = "";
            txtArrdate1.Text = "";
            txtTcredit.Text = "0,00";
            txtTdebit.Text = "0,00";
            txtTdiff.Text = "0,00";
            // promenjive i modeli
            gridItems.DataSource = null;
            gridItems.Show();
            manDebitor = false;
            manCreditor = false;
            manCost = false;
            manProject = false;
            manBTW = false;
            iID = -1;
            // xDaily = -1;
            multimodel = new BindingList<AccLineModel>();
            listlines = new List<AccLineModel>();
            model = new AccLineModel();
            btwtype = 0;
            btwAmt = 0;
            xConto = "";   // account Daily
            xContoSplit = ""; // account for first split line 
            xBtwConto = "";
            gClient = "";
            xcodeBTW = "";
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
            }
            btnSplit.Enabled = true;
            labelBtw.Text = "";
            labelKonto.Text = "";
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


        private void frmDailyBankNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                GetDifference();
            }
            else
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();

                }
            }
        }

        private void gridItems_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
          

            if (asm.isVat == false)
            {
                if (gridItems.Columns["btw"] != null)
                      gridItems.Columns["btw"].IsVisible = false;

            }
            for (int i = 0; i < gridItems.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (gridItems.Columns[i].HeaderText != null && resxSet.GetString(gridItems.Columns[i].HeaderText) != null)
                        gridItems.Columns[i].HeaderText = resxSet.GetString(gridItems.Columns[i].HeaderText);
                }
            }
            if (gridItems != null)
                if (gridItems.ColumnCount > 0)
                {
                    if (gridItems.RowCount > 0)
                    {

                        if (txtInvoice.Text=="")
                        {
                            for (int i = 0; i < gridItems.RowCount; i++)
                            {
                                AccLineModel am = new AccLineModel();
                                am = (AccLineModel)gridItems.Rows[i].DataBoundItem;
                                if (am != null)
                                {
                                    if (am.idDetail != "")
                                        for (int j = 0; j < gridItems.ColumnCount; j++)
                                        {
                                            gridItems.Rows[i].Cells[j].ReadOnly = true;
                                        }
                                    if (am.idBTW != 0)
                                    {
                                        decimal percent = getPercent(Convert.ToInt32(am.idBTW));
                                        int inc_excl = getInc_exlKonto(Convert.ToInt32(am.idBTW));
                                        if (inc_excl == 2)
                                        {
                                            gridItems.Rows[i].Cells["debitBTW"].Value = Math.Abs(Convert.ToDecimal(gridItems.Rows[i].Cells["debitLine"].Value) - Convert.ToDecimal(gridItems.Rows[i].Cells["creditLine"].Value)) * percent / 100;
                                        }
                                        else
                                        {
                                            decimal aa = 0;
                                            decimal amount = 0;
                                            decimal btw_amount = 0;
                                            if (inc_excl == 1)
                                            {

                                            }
                                        }
                                    }
                                    else
                                    {
                                    }

                                }
                            }
                        }
                        else
                        {
                            gridItems.AllowAddNewRow = false;
                            gridItems.AllowDeleteRow = false;
                            for (int i = 0; i < gridItems.RowCount; i++)
                            {
                                for (int j = 0; j < gridItems.ColumnCount; j++)
                                {
                                    if (gridItems.Columns[j].Name!="descLine")
                                    gridItems.Rows[i].Cells[j].ReadOnly = true;
                                }
                            }

                        }
                        for (int i = 0; i < gridItems.RowCount; i++)
                        {
                            for (int j = 0; j < gridItems.ColumnCount; j++)
                            {
                                if (gridItems.Columns[j].Name == "dtLine")
                                    gridItems.Columns[j].ReadOnly = true;
                            }
                        }
                    }

                }

            if (File.Exists(layoutDailyBankNew))
            {
                gridItems.LoadLayout(layoutDailyBankNew);
            }

            if (gridItems.Columns != null && gridItems.Columns.Count > 0)
                gridItems.Columns["dtLine"].FormatString = "{0: dd/MM/yyyy}";


            GridViewDateTimeColumn column = (GridViewDateTimeColumn)this.gridItems.Columns["dtLine"];
            column.Format = DateTimePickerFormat.Short;

            if (gridItems.Columns != null && gridItems.Columns.Count > 0)
            {
                gridItems.Columns["debitLine"].FormatString = "{0:N2}";

                //  gridItems.Columns["debitLine"].InitializeEditor(_gridEditor);
                //   if (gridItems.Columns != null && gridItems.Columns.Count > 0)
                gridItems.Columns["creditLine"].FormatString = "{0:N2}";

                // if (gridItems.Columns != null && gridItems.Columns.Count > 0)
                gridItems.Columns["debitBTW"].FormatString = "{0:N2}";
            }
            totals();
        }

        private void debitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DebCreLookupBUS debpers = new DebCreLookupBUS();
            List<IModel> pm1 = new List<IModel>();

            pm1 = debpers.GetDebitors();
            var dlgSave = new GridLookupForm(pm1, "Debitor");

            string oldDeb = txtClient.Text;

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                DebCreLookupModel pm1X = new DebCreLookupModel();
                pm1X = (DebCreLookupModel)dlgSave.selectedRow;
                //set textbox
                txtClient.Text = pm1X.accNumber;
                gClient = pm1X.accNumber;
                labelClient.Text = pm1X.name;
                txtInvoice.Focus();

                if(pm1X.accNumber!=oldDeb)
                {
                    txtInvoice.Text = "";
                    txtDesc.Text = "";
                    txtProject.Text = "";
                    txtBtwCode.Text = "";
                    txtAccount.Text = "";
                    txtCost.Text = "";
                    multimodel = new BindingList<AccLineModel>();
                    gridItems.DataSource = null;

                }

            }
        }

        private void creditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DebCreLookupBUS debpers = new DebCreLookupBUS();
            List<IModel> pm1 = new List<IModel>();

            pm1 = debpers.GetCreditors();
            var dlgSave = new GridLookupForm(pm1, "Creditor");

            string oldCre = txtClient.Text;

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                DebCreLookupModel pm1X = new DebCreLookupModel();
                pm1X = (DebCreLookupModel)dlgSave.selectedRow;
                //set textbox
                txtClient.Text = pm1X.accNumber;
                gClient = pm1X.accNumber;
                labelClient.Text = pm1X.name;
                txtInvoice.Focus();

                if (pm1X.accNumber != oldCre)
                {
                    txtInvoice.Text = "";
                    txtDesc.Text = "";
                    txtProject.Text = "";
                    txtBtwCode.Text = "";
                    txtAccount.Text = "";
                    txtCost.Text = "";
                    multimodel = new BindingList<AccLineModel>();
                    gridItems.DataSource = null;

                }
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
            split();  

        }

        private void split()
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

            bool bsplit = true;

            if (Convert.ToDecimal(txtAmountC.Text) != 0 && Convert.ToDecimal(txtAmountD.Text) != 0)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Check amounts, please!");
                bsplit = false;
                txtAmountC.Focus();
            }
            else
            {

                if (txtClient.Text == "" && manCreditor == true)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Can't save without Customer !!!");
                    bsplit = false;
                    txtClient.Focus();
                }
                else
                {
                    if (txtInvoice.Text == "")
                    {
                    }
                    else
                    {
                        if (txtCost.Text == "" && manCost == true)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Cost mandatory !!!");
                            bsplit = false;
                            txtCost.Focus();
                        }
                        else
                        {

                            if (txtProject.Text == "" && manProject == true)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("Project mandatory !!!");
                                bsplit = false;
                                txtProject.Focus();
                            }
                            else
                            {
                                if (Convert.ToDecimal(txtAmountC.Text) == 0 && Convert.ToDecimal(txtAmountD.Text) == 0)
                                {
                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translateAllMessageBox("Amount 0 !!!");
                                    bsplit = false;
                                    txtAmountC.Focus();
                                }
                                else
                                {
                                    if (txtBtwCode.Text == "" && manBTW)
                                    {
                                        translateRadMessageBox tr = new translateRadMessageBox();
                                        tr.translateAllMessageBox("BTW mandatory");
                                        bsplit = false;
                                        txtBtwCode.Focus();
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
                //multimodel = new List<AccLineModel>();
                multimodel = new BindingList<AccLineModel>();
                fillLines();
                //  splittingRecords();
                SplitLines();
            }
            addIdMaster();
        }


       


        # region SplittLines

        private void SplitLines()
        {
            decimal btwPercent = 0;

            if (linesmodel.idBTW != 0 && linesmodel.idBTW != null)  //==== uzima procenat ===
            {

                 AccTaxBUS porez = new AccTaxBUS();
                AccTaxModel pm = new AccTaxModel();
                pm = porez.GetTaxByID(Convert.ToInt32(txtBtwCode.Text));
                if (pm != null)
                {
                    //
                    labelBtw.Text = pm.descTax;
                    xcodeBTW = pm.codeTax;
                }
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
                }

            }                                                  //==============================

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
            decimal debcreAmt = 0;

            string sw = "";
            if ((oplinemodel != null || addmodelopl != null) && isGrab == true)
                sw = "5";  // izabrao je otvorenu stavku
            if (txtAccount.Text != "" && txtBtwCode.Text != "")
                sw = "1";   // ima i account i porez
            if (txtAccount.Text == "" && txtBtwCode.Text != "" && isGrab == false)
                sw = "2";  // nema account ima porez
            if (txtAccount.Text == "" && txtBtwCode.Text == "")
                sw = "3";  // nema konto nema porez
            if (txtAccount.Text != "" && txtBtwCode.Text == "")
                sw = "4"; // ima account nema porez
            //if (txtAccount.Text == "" && txtBtwCode.Text != "")
            //    sw = "6";  // nema konto ima porez

            switch (sw)
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
                               // btwAmt = aa * btwPercent / 100;
                                debcreAmt = aa / (100 + btwPercent) * 100;
                                btwAmt = aa - debcreAmt;
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
                        model.creditLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);           //    debcreAmt txtAmountD.Text
                        model.debitLine = 0;

                    }
                    else
                    {
                        //   if (xSideBooking == "C")
                        if (Convert.ToDecimal(txtAmountC.Text) != 0)
                        {
                            model.debitLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);               //     debcreAmt txtAmountC.Text
                            model.creditLine = 0;
                        }
                        else
                        {

                            model.debitLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);
                            model.creditLine = 0;
                        }
                    }
                   
                    multimodel.Add(model);
                    break;

                case "2":

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

                                debcreAmt = aa / (100 + btwPercent) * 100;
                                btwAmt = aa - debcreAmt;
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
                        model.creditLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);          // debcreAmt
                        model.debitLine = 0;
                    }
                    else
                    {
                        //   if (xSideBooking == "C")
                        if (Convert.ToDecimal(txtAmountC.Text) != 0)
                        {
                            model.debitLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);               // debcreAmt
                            model.creditLine = 0;
                        }
                        else
                        {

                            model.debitLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);
                            model.creditLine = 0;
                        }
                    }

                    multimodel.Add(model);
                    break;

                case "3":

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
                    multimodel.Add(model);
                    break;

                case "4":

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
                    multimodel.Add(model);
                    break;
                case "5":
                    if (isGrab == true)
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
                        if (txtBtwCode.Text != "")
                        {
                            if (Convert.ToDecimal(txtAmountD.Text) != 0)
                            {
                                addmodelopl.creditLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);           //      txtAmountD.Text
                                addmodelopl.debitLine = 0;

                            }
                            else
                            {
                                //   if (xSideBooking == "C")
                                if (Convert.ToDecimal(txtAmountC.Text) != 0)
                                {
                                    addmodelopl.debitLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);               //       txtAmountC.Text
                                    addmodelopl.creditLine = 0;
                                }
                                else
                                {

                                    addmodelopl.debitLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);
                                    addmodelopl.creditLine = 0;
                                }
                            }
                        }


                    }
                    multimodel.Add(addmodelopl);

                    break;

            }

            //  multimodel.Add(model);

            //==================================  druga stavka ============================================
            AccLineModel model1 = new AccLineModel();

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
                    model1.dtLine = model.dtLine;
                }
                // model1.dtLine = linesmodel.dtLine;
                model1.invoiceNr = linesmodel.invoiceNr;
                model1.descLine = model.descLine;

                if (xBtwConto != "" || xBtwConto != null)
                {
                    model1.numberLedAccount = xBtwConto;
                }
                else
                {
                    model1.numberLedAccount = "1510";
                }

               
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
                model1.idBTW = 0;
                model1.idProjectLine = linesmodel.idProjectLine;
                model1.idClientLine = linesmodel.idClientLine;
                model1.idCostLine = linesmodel.idCostLine;
                model1.incopNr = linesmodel.incopNr;

                multimodel.Add(model1);

            }
            gridItems.DataSource = null;
            //alexa ubacivanje prve linije
            fillLines();
            linesmodel.idBTW = 0;
            multimodel.Insert(0, linesmodel);
            //
            gridItems.DataSource = multimodel;
            if (asm.isVat == false)
            {
                if (gridItems.Columns["btw"] != null)
                    gridItems.Columns["btw"].IsVisible = false;
            }
            gridItems.Show();
            btnSplit.Enabled = false;
        }
        # endregion SplittLines


        private void txtClient_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int xX = this.Location.X;
            int yY = this.Location.Y;
            debcreCMenu.Show(xX + 220, yY + 100);
        }

        private void saveItems()
        {
            AccAcountUpdate aaU = new AccAcountUpdate();
            listlines = new List<AccLineModel>();
            int j = 2;
            for (int i = 0; i < gridItems.Rows.Count; i++)
            {
                AccLineModel itm = new AccLineModel();
                itm.idAccDaily = xDaily;
                itm.currrate = 0;
                itm.idCurrency = xCurr;
                itm.statusLine = false;
                itm.creditBTW = 0;
                itm.debitBTW = 0;
                itm.creditCurr = 0;
                itm.debitCurr = 0;
                itm.dtBooking = DateTime.Now;            //Convert.ToDateTime("2000-01-01");
                itm.idProjectLine = "";
                itm.idCostLine = "";
                itm.numberLedAccount = "";
                itm.idPersonLine = "";
                itm.incopNr = "";
                itm.dtLine = Convert.ToDateTime(gridItems.Rows[i].Cells["dtLine"].Value.ToString());
                DateTime mper = Convert.ToDateTime(itm.dtLine);
                itm.periodLine = aaU.Period(Convert.ToDateTime(itm.dtLine));                   //mper.Month;
                if (gridItems.Rows[i].Cells["invoiceNr"].Value != null)
                    itm.invoiceNr = gridItems.Rows[i].Cells["invoiceNr"].Value.ToString();
                if (gridItems.Rows[i].Cells["descLine"].Value != null)
                    itm.descLine = gridItems.Rows[i].Cells["descLine"].Value.ToString();
                if (gridItems.Rows[i].Cells["idClientLine"].Value != null)
                    itm.idClientLine = gridItems.Rows[i].Cells["idClientLine"].Value.ToString();
                if (gridItems.Rows[i].Cells["numberLedAccount"].Value != null)
                    itm.numberLedAccount = gridItems.Rows[i].Cells["numberLedAccount"].Value.ToString();
                if (gridItems.Rows[i].Cells["idBTW"].Value != null && gridItems.Rows[i].Cells["idBTW"].Value != "")
                    itm.idBTW = Convert.ToInt32(gridItems.Rows[i].Cells["idBTW"].Value.ToString());
                if (gridItems.Rows[i].Cells["debitLine"].Value != null && gridItems.Rows[i].Cells["debitLine"].Value != "")
                    itm.debitLine = Convert.ToDecimal(gridItems.Rows[i].Cells["debitLine"].Value.ToString());
                if (gridItems.Rows[i].Cells["creditLine"].Value != null && gridItems.Rows[i].Cells["creditLine"].Value != "")
                    itm.creditLine = Convert.ToDecimal(gridItems.Rows[i].Cells["creditLine"].Value.ToString());
                if (gridItems.Rows[i].Cells["idCostLine"].Value != null)
                    itm.idCostLine = gridItems.Rows[i].Cells["idCostLine"].Value.ToString();
                if (gridItems.Rows[i].Cells["idProjectLine"].Value != null)
                    itm.idProjectLine = gridItems.Rows[i].Cells["idProjectLine"].Value.ToString();

                itm.incopNr = txtIncop.Text;
                itm.bookingYear = Login._bookyear;
                itm.term = multimodel[i].term;
                itm.booksort = j++;

                listlines.Add(itm);
            }
            //      updateOpenLines();
        }


        private void totals()
        {
            decimal debTotal = 0;
            decimal creTotal = 0;
            decimal totalLines = 0;
            if (gridItems != null)
            {
                for (int i = 0; i < gridItems.Rows.Count; i++)
                {
                    if (gridItems.Rows[i].Cells["debitLine"].Value != null)
                        debTotal = debTotal + Convert.ToDecimal(gridItems.Rows[i].Cells["debitLine"].Value.ToString());
                    if (gridItems.Rows[i].Cells["creditLine"].Value != null)
                        creTotal = creTotal + Convert.ToDecimal(gridItems.Rows[i].Cells["creditLine"].Value.ToString());

                }
            }
       ////////////////////
            //decimal mdeb;
            //decimal mcre;
            //if (iID == -1)
            //{
            //     mdeb = debTotal + Convert.ToDecimal(txtAmountD.Text); // dtot;
            //     mcre = creTotal + Convert.ToDecimal(txtAmountC.Text); //ctot;
            //}
            //else 
            //{
            //     mdeb = debTotal; // dtot;
            //     mcre = creTotal; //ctot;
            //}

            totalLines = debTotal - creTotal;
            txtTdebit.Text = debTotal.ToString();                //debTotal.ToString();
            txtTcredit.Text = creTotal.ToString();                                 //creTotal.ToString();
            txtTdiff.Text = totalLines.ToString();
            //totalLines = mdeb - mcre;
            //txtTdebit.Text = mdeb.ToString();                //debTotal.ToString();
            //txtTcredit.Text = mcre.ToString();                                 //creTotal.ToString();
            //txtTdiff.Text = totalLines.ToString();
            //if (totalLines != 0)
            //    GetDifference();
         //////////////////////////

            //decimal mdeb = debTotal + Convert.ToDecimal(txtAmountD.Text); // dtot;
            //decimal mcre = creTotal + Convert.ToDecimal(txtAmountC.Text);                                      //ctot;
            //totalLines = (debTotal + Convert.ToDecimal(txtAmountD.Text)) - (creTotal + Convert.ToDecimal(txtAmountC.Text));
            //txtTdebit.Text = mdeb.ToString();                //debTotal.ToString();
            //txtTcredit.Text = mcre.ToString();                                 //creTotal.ToString();
            //txtTdiff.Text = totalLines.ToString();
            //if (totalLines != 0)
            //    GetDifference();
        }

        #region Mandatory


        private void txtInvoice_Leave(object sender, EventArgs e)
        {
           
            if (txtInvoice.Text != "")
            {
                AccOpenLinesBUS opb = new AccOpenLinesBUS();
                AccOpenLinesModel opm = new AccOpenLinesModel();
                //  multimodel = new List<AccLineModel>();
                if (multimodel == null)
                    multimodel = new BindingList<AccLineModel>();
                opm = opb.GetAccOpenLinesByInvoiceNoTerm(txtInvoice.Text);
                if (opm != null)
                {
                    if (opm.invoiceOpenLine != null)
                    {
                        if (txtInvoice.Text == opm.invoiceOpenLine.ToString())
                        {
                            txtClient.Text = opm.idDebCre.ToString();
                            AccAcountUpdate aau = new AccAcountUpdate();
                            txtDesc.Text = opm.descOpenLine.ToString();
                            
                        }
                    }
                }
                hideOpenLine(true);
            }
            else
            {
                hideOpenLine(false);
            }
            txtDesc.Focus();

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
                            txtInvoice.Focus();
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
                            txtInvoice.Focus();
                            // txtAccount.Focus();
                        }
                        else
                        {
                            txtClient.Text = "";
                            txtClient.Focus();
                        }
                    }
                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Wrong id");
                    txtClient.Focus();
                }
            }
            else
            {
                txtInvoice.Focus();
            }


        }

        private void txtBtwCode_Leave(object sender, EventArgs e)
        {
            
            if (txtBtwCode.Text != "")
            {
                AccTaxBUS porez = new AccTaxBUS();
                AccTaxModel pm = new AccTaxModel();
                pm = porez.GetTaxByID(Convert.ToInt32(txtBtwCode.Text));
                if (pm != null)
                {
                    //
                    labelBtw.Text = pm.descTax;
                    xcodeBTW = pm.codeTax;
                    xBtwConto = pm.numberLedAccount;
                    if (pm.typeTax != 0 && pm.typeTax != null)
                        btwtype = Convert.ToDecimal(pm.typeTax);
                    //
                    txtBtwCode.Text = pm.idTax.ToString();
                    labelBtw.Text = pm.descTax;
                    if (pm.numberLedAccount != null)
                    {
                        xBtwConto = pm.numberLedAccount;
                        txtCost.Focus();
                    }
                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Wrong BTW code");
                    txtBtwCode.Focus();
                }
            }
            else
            {
                labelBtw.Text = "";
            }
        }

        private void txtCost_Leave(object sender, EventArgs e)
        {
           
            if (txtCost.Text == "")
            {
                txtProject.Focus();
                labelCost.Text = "";
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
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Wrong Cost code");
                    txtCost.Focus();
                }
            }
            //}
        }

        private void txtProject_Leave(object sender, EventArgs e)
        {
           
            if (txtProject.Text == "")
            {
                if (btnSplit.Enabled == true)
                    btnSplit.Focus();
                else
                    btnOK.Focus();

                labelProject.Text = "";
                txtArrdate.Text = "";
            }
            else
            {
                ArrangementBUS arb = new ArrangementBUS();
                ArrangementModel arm = new ArrangementModel();
                arm = arb.GetArrangementByCode(txtProject.Text);
                if (arm != null)
                {
                    labelProject.Text = arm.nameArrangement;
                    //txtArrdate.Text = "Start " + Convert.ToString(arm.dtFromArrangement) + " End " + Convert.ToString(arm.dtToArrangement);
                    txtArrdate.Text = "Start date =>" + arm.dtFromArrangement.ToShortDateString() + " - " + "end date =>" + arm.dtToArrangement.ToShortDateString();
                    if (arm.dtFromArrangement != null)
                    {
                        xdtProjectStart = Convert.ToDateTime(arm.dtFromArrangement);
                    }
                    if (arm.dtFromArrangement != null)
                        xdtProjectStart = Convert.ToDateTime(arm.dtFromArrangement);
                    //if (btnSplit.Enabled == true)
                    //{
                    btnSplit.Focus();
                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Wrong project code !!!");
                    txtProject.Text = "";
                    txtProject.Focus();
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
            string yearId = DateTime.Now.Year.ToString();
            if (xDaily != -1)
                idDaily = xDaily;
            nid = gn.GetIncop(yearId, idDaily, this.Name, Login._user.idUser);
            if (nid != null)
            {
                var result = nid.idNumber.ToString().PadLeft(6, '0');
                var aa = nid.idDaily.ToString().PadRight(6, '0');
                string SubString = nid.yearId.Substring(yearId.Length - 2);

                txtIncop.Text = SubString + aa + result;
            }

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
                //gridItems.CurrentRow.Cells["account"].Value = genmX3.numberLedgerAccount;
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
               // gridItems.CurrentRow.Cells["Btw"].Value = gmX4ml.idTax;
                //set textbox
                txtBtwCode.Text = gmX4ml.idTax.ToString();
                labelBtw.Text = gmX4ml.descTax;
                xBtwConto = gmX4ml.numberLedAccount;
                if (gmX4ml.typeTax != 0 && gmX4ml.typeTax != null)
                {
                    btwtype = Convert.ToDecimal(gmX4ml.typeTax);
                    xcodeBTW = gmX4ml.codeTax;
                    xBtwConto = gmX4ml.numberLedAccount;
                    if (gmX4ml.typeTax != 0 && gmX4ml.typeTax != null)
                        btwtype = Convert.ToDecimal(gmX4ml.typeTax);
                }
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
                //  gridItems.SelectedRows[0].Cells["cost"].Value = genmX.codeCost.ToString();
                //gridItems.CurrentRow.Cells["cost"].Value = genmX.codeCost.ToString();
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
                //gridItems.CurrentRow.Cells["project"].Value = genmX1.codeProject;
                txtProject.Text = genmX1.codeProject;
                labelProject.Text = genmX1.nameArrangement;
                //txtArrdate.Text = "Start " + Convert.ToString(genmX1.dtFromArrangement) + " End " + Convert.ToString(genmX1.dtToArrangement);
                txtArrdate.Text = "Start date =>" + genmX1.dtFromArrangement.ToShortDateString() + " - " + "end date =>" + genmX1.dtToArrangement.ToShortDateString();
                if (genmX1.dtFromArrangement != null)
                    xdtProjectStart = Convert.ToDateTime(genmX1.dtFromArrangement);
            }
        }



        private void btnClient_MouseClick(object sender, MouseEventArgs e)
        {
            int xX = this.Location.X;
            int yY = this.Location.Y;
            debcreCMenu.Show(xX + 220, yY + 100);


        }
        private Boolean checkGriddata()
        {
            Boolean blcheck = true;
            int i = 0;
            foreach (AccLineModel itm in multimodel)
            {
                 bool manDebitor1 = false;
                 bool manCreditor1 = false;
                 bool manCost1 = false;
                //AccLineModel itm = multimodel[i];
                if (itm != null)
                {
                    if (gridItems.RowCount - 1 >= i)
                    {

                        if (gridItems.Rows[i].Cells["numberLedAccount"].Value != null)
                            itm.numberLedAccount = gridItems.Rows[i].Cells["numberLedAccount"].Value.ToString();

                        LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                        LedgerAccountModel lam = new LedgerAccountModel();

                        lam = ledbus.GetAccount(itm.numberLedAccount, Login._bookyear);
                        //   labelKonto.Text = lam.descLedgerAccount;
                        if (lam != null)
                        {
                            // === kupi sta je obavezno za unos ===
                            manCreditor1 = lam.mandatoryCreditorAccount;
                            manDebitor1 = lam.mandatoryDebitorAccount;
                            manCost1 = lam.mandatoryCostAccount;
                        }
                        if (itm.numberLedAccount == "")
                        {
                           
                           
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("No Account !");
                           
                            blcheck = false;
                            break;
                        }

                        if (gridItems.Rows[i].Cells["dtLine"].Value != null && gridItems.Rows[i].Cells["dtLine"].Value != "")
                            itm.dtLine = Convert.ToDateTime(gridItems.Rows[i].Cells["dtLine"].Value.ToString());

                        if (gridItems.Rows[i].Cells["invoiceNr"].Value != null)
                            itm.invoiceNr = gridItems.Rows[i].Cells["invoiceNr"].Value.ToString();
                        if (itm.invoiceNr == "")
                        {
                            if (gridItems.Rows[i].Cells["idClientLine"].Value != null)
                            {
                                itm.idClientLine = gridItems.Rows[i].Cells["idClientLine"].Value.ToString();
                                if (itm.idClientLine != "")
                                {
                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translateAllMessageBox("No invoice number !");

                                    blcheck = false;
                                    break;
                                }

                            }
                        }
                        else
                        {
                            if (gridItems.Rows[i].Cells["idClientLine"].Value != null)
                                itm.idClientLine = gridItems.Rows[i].Cells["idClientLine"].Value.ToString();
                            if (gridItems.Rows[i].Cells["idClientLine"].Value != null)
                            {
                                if (gridItems.Rows[i].Cells["numberLedAccount"].Value != null)
                                    itm.numberLedAccount = gridItems.Rows[i].Cells["numberLedAccount"].Value.ToString();
                                itm.idClientLine = gridItems.Rows[i].Cells["idClientLine"].Value.ToString();
                                if (itm.idClientLine == "")
                                    if ((itm.numberLedAccount.Trim() != defaultSepa.Trim()) && (itm.numberLedAccount.Trim() != xBankConto.Trim()))
                                    {
                                        translateRadMessageBox tr = new translateRadMessageBox();
                                        tr.translateAllMessageBox("No client number !");

                                        blcheck = false;
                                        break;
                                    }
                            }
                        }
                        if (gridItems.Rows[i].Cells["descLine"].Value != null)
                            itm.descLine = gridItems.Rows[i].Cells["descLine"].Value.ToString();
                        if (itm.descLine == "")
                        {
                            
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("Enter description, please ");
                            blcheck = false;
                            break;
                        }
                        if (gridItems.Rows[i].Cells["idClientLine"].Value != null)
                            itm.idClientLine = gridItems.Rows[i].Cells["idClientLine"].Value.ToString();
                        if (itm.idClientLine == "" && (manCreditor1 == true || manDebitor1 == true))
                        {
                            
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("Enter client, please ");
                            blcheck = false;
                            break;
                        }

                        if (gridItems.Rows[i].Cells["idBTW"].Value != null && asm.isVat == true && gridItems.Rows[i].Cells["idBTW"].Value != "")
                            itm.idBTW = Convert.ToInt32(gridItems.Rows[i].Cells["idBTW"].Value.ToString());
                        if (gridItems.Rows[i].Cells["debitLine"].Value != null && gridItems.Rows[i].Cells["debitLine"].Value != "")
                            itm.debitLine = Convert.ToDecimal(gridItems.Rows[i].Cells["debitLine"].Value.ToString());
                        if (gridItems.Rows[i].Cells["creditLine"].Value != null && gridItems.Rows[i].Cells["creditLine"].Value != "")
                            itm.creditLine = Convert.ToDecimal(gridItems.Rows[i].Cells["creditLine"].Value.ToString());
                        if (gridItems.Rows[i].Cells["idCostLine"].Value != null)
                            itm.idCostLine = gridItems.Rows[i].Cells["idCostLine"].Value.ToString();
                        if (itm.idCostLine == "" && manCost1 == true)
                        {
                            
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("No cost !");
                           
                            blcheck = false;
                            break;
                        }
                        if (gridItems.Rows[i].Cells["idProjectLine"].Value != null)
                            itm.idProjectLine = gridItems.Rows[i].Cells["idProjectLine"].Value.ToString();
                        if (gridItems.Rows[i].Cells["incopNr"].Value != null)
                            itm.incopNr = gridItems.Rows[i].Cells["incopNr"].Value.ToString();


                        if (gridItems.Rows[i].Cells["debitLine"].Value != null && gridItems.Rows[i].Cells["debitLine"].Value != "" && gridItems.Rows[i].Cells["creditLine"].Value != null && gridItems.Rows[i].Cells["creditLine"].Value != "")
                        if (Convert.ToDecimal(gridItems.Rows[i].Cells["debitLine"].Value) == 0 && Convert.ToDecimal(gridItems.Rows[i].Cells["creditLine"].Value) == 0)
                        {
                           
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("Amount cannot be 0 !");
                            blcheck = false;
                            break;
                        }

                        blcheck = true;
                    }
                }
                i++;

            }

            return blcheck;

        }


        private void btnOpenLines_Click(object sender, EventArgs e)
        {
           lookupOpenLine();

        }
        private void lookupOpenLine()
        {
            multimodel = new BindingList<AccLineModel>();
            goOpenLines();
            if (multimodel != null)
            {
                for (int n = 0; n < multimodel.Count; n++)
                {
                    if (multimodel[n].invoiceNr == "")
                        multimodel[n].idClientLine = "";
                }
            }
            else
            {
                hideOpenLine(true);
            }

        }


        private void goOpenLines()
        {
            if (txtAmountC.Text == "0,00" && txtAmountD.Text == "0,00")
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Amount is mandatory !!");
                return;
            }
            else
            {

                if (lookopenlines == null)
                {
                    lookopenlines = new BindingList<AccOpenLinesModel>();
                }
                AccOpenLinesBUS debpers = new AccOpenLinesBUS();
                List<AccOpenLinesModel> oplinemodel = new List<AccOpenLinesModel>();
                if (txtClient.Text != "")
                {
                    oplinemodel = debpers.GetAccOpenLinesByID(txtClient.Text);   //debpers.GetAccOpenLinesByID(txtClient.Text);          //GetCreditors();
                    if (oplinemodel != null)
                    {
                        if (oplinemodel.Count > 0)
                        {

                            for (int i = 0; i < oplinemodel.Count; i++)
                            {
                                oplinemodel[i].iselected = false;
                            }
                        }

                    }
                    lookopenlines = new BindingList<AccOpenLinesModel>(oplinemodel);
                }
                else
                {
                    oplinemodel = debpers.GetAllOpenLinesM();
                    if (oplinemodel != null)
                    {
                        if (oplinemodel.Count > 0)
                        {

                            for (int i = 0; i < oplinemodel.Count; i++)
                            {
                                oplinemodel[i].iselected = false;
                            }
                        }

                    }
                    lookopenlines = new BindingList<AccOpenLinesModel>(oplinemodel);
                }
                if (multimodel == null)
                    multimodel = new BindingList<AccLineModel>();
                if (oplinemodel.Count > 0)
                {
                    for (int q = 0; q < oplinemodel.Count; q++)
                    {
                        for (int w = 0; w < multimodel.Count; w++)
                        {
                            if (w == oplinemodel.Count)
                                break;
                            if (oplinemodel[q].invoiceOpenLine == multimodel[w].invoiceNr && oplinemodel[q].term == multimodel[w].term)
                                oplinemodel.RemoveAt(q);
                        }
                    }
                }

                decimal amount = 0;
                //if (Convert.ToDecimal(txtTdiff.Text) != 0)
                //    amount = Convert.ToDecimal(txtTdiff.Text);
                //else
                    if (Convert.ToDecimal(txtAmountD.Text) != 0)
                        amount = Convert.ToDecimal(txtAmountD.Text);
                    else
                        amount = Convert.ToDecimal(txtAmountC.Text) * -1;
                string side;
                if (Convert.ToDecimal(txtAmountC.Text) != 0)
                    side = "C";
                else
                    side = "D";
                 multimodel = new BindingList<AccLineModel>();
                var dlgSave = new OpenLookupForm(oplinemodel, "Open Lines", txtClient.Text, amount, xDaily, multimodel, side);

                if (dlgSave.ShowDialog() == DialogResult.Yes)
                {
                    AccLineModel addmodel = new AccLineModel();
                    if (multimodel == null)
                        multimodel = new BindingList<AccLineModel>();
                    else if (multimodel.Count > 0)
                        disableClearInvoiceAndSplit();

                    gridItems.DataSource = null;

                    addIdMaster();

                    gridItems.DataSource = multimodel;
                    if (gridItems != null)
                    {
                        if (gridItems.RowCount >= 1)
                        {
                           
                                txtClient.Text = multimodel[0].idClientLine;
                          
                                txtDesc.Text = multimodel[0].descLine;
                          
                                txtCost.Text = multimodel[0].idCostLine;
                           
                                txtProject.Text = multimodel[0].idProjectLine;
                           
                                txtAccount.Text = multimodel[0].numberLedAccount;
                           
                                txtInvoice.Text =  multimodel[0].invoiceNr;

                            giveLabels();
                        }
                        if (multimodel != null && multimodel.Count > 0)  // === ubacuje datum sa vrha forme u stavke
                        {
                            for (int y = 0; y < multimodel.Count; y++)
                            {
                                multimodel[y].dtLine = txtDate.Value;//Convert.ToDateTime(txtDate.Text);
                            }
                        }
                        gridItems.DataSource = null;
                        fillLines();
                        linesmodel.idBTW = 0;
                        multimodel.Insert(0, linesmodel);

                        gridItems.DataSource = multimodel;
                        gridItems.Show();
                    }

                }
                if (txtInvoice.Text != "")
                    hideOpenLine(true);
                else
                    hideOpenLine(false);


                totals();
                txtDesc.Focus();
            }
        }

        public void addIdMaster()
        {
            int i = 0;
            if(multimodel!=null)
            foreach(AccLineModel ac in multimodel)
            {
                if(txtIncop.Text!="")
                {
                    ac.idMaster = txtIncop.Text + (i + 1).ToString();
                }
                i++;


            }
        }


        public void updateOpenLines()
        {
            AccOpenLinesBUS olbus = new AccOpenLinesBUS();
            AccOpenLinesModel olmod = new AccOpenLinesModel();
            if (multimodel != null)
            {
                if (multimodel.Count > 0)
                {

                    foreach (AccLineModel itmol in multimodel)
                    {
                        if (labelBasicKonto.Text.StartsWith(itmol.numberLedAccount.Trim()))
                        {
                            continue;
                        }
                        if (itmol.invoiceNr == "" && itmol.idClientLine == "")
                        {
                            return;

                        }
                        else
                        {
                            olmod = olbus.GetAccOpenLinesByInvoice(itmol.invoiceNr, itmol.term);
                            if (olmod != null)
                            {
                                if (olmod.invoiceOpenLine != null)
                                {
                                    if (olmod.invoiceOpenLine == itmol.invoiceNr && olmod.term == itmol.term)
                                    {

                                        olmod.iselected = false;
                                        olmod.dtPayOpenLine = itmol.dtLine;//DateTime.Now;
                                        if (olmod.referencePay == itmol.incopNr && ledit == true) // edituje istu stavku od koje je napravljena otvorena stavka
                                        {                                                          // onda samo upisuje novi iznos;
                                            if (itmol.debitLine > 0)
                                                olmod.debitOpenLine = itmol.debitLine;
                                            if (itmol.creditLine > 0)
                                                olmod.creditOpenLine = itmol.creditLine;
                                            olmod.referencePay = itmol.incopNr;
                                        }
                                        else
                                        {
                                            if (iID != -1)
                                            {
                                                if (olmod.debitOpenLine > 0)
                                                    olmod.debitOpenLine = Math.Abs(Convert.ToDecimal(olmod.debitOpenLine) - Convert.ToDecimal(itmol.debitLine));
                                                if (olmod.creditOpenLine > 0)
                                                    olmod.creditOpenLine = Math.Abs(Convert.ToDecimal(olmod.creditOpenLine) - Convert.ToDecimal(itmol.creditLine));

                                                // olmod.referencePay = itmol.incopNr;
                                            }
                                            else
                                            {
                                                //   olmod.referencePay = itmol.incopNr;
                                                olmod.debitOpenLine = olmod.debitOpenLine + itmol.debitLine;                     //itmol.debitLine;
                                                olmod.creditOpenLine = olmod.creditOpenLine + itmol.creditLine;                   // itmol.creditLine;
                                            }
                                        }
                                        olbus.Update(olmod, this.Name, Login._user.idUser);  // upisujemo open line stavku

                                    }

                                    // }
                                }
                            }
                        }

                    }
                }
            }

        }

        public void updateOpenLinesOLD()
        {
            AccOpenLinesBUS olbus = new AccOpenLinesBUS();
            AccOpenLinesModel olmod = new AccOpenLinesModel();
            if (listOldlines != null)
            {
                if (listOldlines.Count > 0)
                {


                    foreach (AccLineModel itmol in listOldlines)
                    {
                        if (itmol.invoiceNr == "" && itmol.idClientLine == "")
                        {
                            return;

                        }
                        else
                        {
                            olmod = olbus.GetAccOpenLinesByInvoice(itmol.invoiceNr, itmol.term);
                            if (olmod != null)
                            {
                                if (olmod.invoiceOpenLine != null)
                                {
                                    if (olmod.invoiceOpenLine == itmol.invoiceNr && olmod.term == itmol.term)
                                    {

                                        olmod.iselected = false;
                                        olmod.dtPayOpenLine = itmol.dtLine;//DateTime.Now;

                                        if (olmod.referencePay == itmol.incopNr && ledit == true) // edituje istu stavku od koje je napravljena otvorena stavka
                                        {                                                          // onda samo upisuje novi iznos;
                                            if (itmol.debitLine > 0)
                                                olmod.debitOpenLine = itmol.debitLine;
                                            if (itmol.creditLine > 0)
                                                olmod.creditOpenLine = itmol.creditLine;
                                            olmod.referencePay = itmol.incopNr;
                                        }
                                        else
                                        {
                                            if (iID != -1)
                                            {
                                                if (olmod.debitOpenLine > 0)
                                                    olmod.debitOpenLine = olmod.debitOpenLine - itmol.debitLine;
                                                if (olmod.creditOpenLine > 0)
                                                    olmod.creditOpenLine = olmod.creditOpenLine - itmol.creditLine;

                                                // olmod.referencePay = itmol.incopNr;
                                            }
                                            else
                                            {
                                                //   olmod.referencePay = itmol.incopNr;
                                                olmod.debitOpenLine = olmod.debitOpenLine + itmol.debitLine;                     //itmol.debitLine;

                                                olmod.creditOpenLine = olmod.creditOpenLine + itmol.creditLine;                   // itmol.creditLine;
                                                // olmod.debitOpenLine = olmod.debitOpenLine + itmol.creditLine; 
                                                //}
                                            }
                                        }
                                        olbus.Update(olmod, this.Name, Login._user.idUser);  // upisujemo open line stavku
                                    }
                                    // }
                                }
                            }
                        }

                    }
                }
            }

        }


        private void creditorToolStripMenuItem1_Click(object sender, EventArgs e)
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
                labelName.Text = pm1X.name;
                gridItems.CurrentRow.Cells["client"].Value = gClient;
            }

        }

        private void debitorToolStripMenuItem1_Click(object sender, EventArgs e)
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
                gClient = pm1X.accNumber;
                labelName.Text = pm1X.name;
                gridItems.CurrentRow.Cells["client"].Value = gClient;

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

                if (resxSet.GetString(lblStatement.Text) != null)
                    lblStatement.Text = resxSet.GetString(lblStatement.Text);
                if (resxSet.GetString(lblDateStat.Text) != null)
                    lblDateStat.Text = resxSet.GetString(lblDateStat.Text);
                if (resxSet.GetString(lblBegin.Text) != null)
                    lblBegin.Text = resxSet.GetString(lblBegin.Text);
                if (resxSet.GetString(lblEndSald.Text) != null)
                    lblEndSald.Text = resxSet.GetString(lblEndSald.Text);
                if (resxSet.GetString(lblDiff.Text) != null)
                    lblDiff.Text = resxSet.GetString(lblDiff.Text);
                if (resxSet.GetString(lblTdebit.Text) != null)
                    lblTdebit.Text = resxSet.GetString(lblTdebit.Text);
                if (resxSet.GetString(lblTcredit.Text) != null)
                    lblTcredit.Text = resxSet.GetString(lblTcredit.Text);
                if (resxSet.GetString(lblTdiff.Text) != null)
                    lblTdiff.Text = resxSet.GetString(lblTdiff.Text);
                if (resxSet.GetString(btnOK.Text) != null)
                    btnOK.Text = resxSet.GetString(btnOK.Text);
                if (resxSet.GetString(btnCancel.Text) != null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);
                if (resxSet.GetString(btnSplit.Text) != null)
                    btnSplit.Text = resxSet.GetString(btnSplit.Text);
                if (resxSet.GetString(lblIncop.Text) != null)
                    lblIncop.Text = resxSet.GetString(lblIncop.Text);

                if (resxSet.GetString(lblBooked.Text) != null)
                    lblBooked.Text = resxSet.GetString(lblBooked.Text);
            }
        }
        private void CalcDiff()
        {
            decimal sumDebT = 0;
            decimal sumCreT = 0;

            AccLineDAO sumis = new AccLineDAO(Login._bookyear);
            object adeb = sumis.SumDebitLinesByNalog(selectedDailyBank.idDailyBank);
            object acre = sumis.SumCreditLinesByNalog(selectedDailyBank.idDailyBank);
            if (adeb != null && acre != null)
            {

                sumDebT = Convert.ToDecimal(sumis.SumDebitLinesByNalog(selectedDailyBank.idDailyBank));

                sumCreT = Convert.ToDecimal(sumis.SumCreditLinesByNalog(selectedDailyBank.idDailyBank));

                decimal stDif = 0;
                stDif = (Convert.ToDecimal(sumDebT) - Convert.ToDecimal(sumCreT));  //Convert.ToDecimal(selectedDailyBank.endSaldo) - Convert.ToDecimal(selectedDailyBank.begSaldo) - 
                txtDiffBook.Text = stDif.ToString();
            }
            txtBeginSaldo.Text = selectedDailyBank.begSaldo.ToString();
            txtEndSaldo.Text = selectedDailyBank.endSaldo.ToString();


        }

        private void GetDifference()
        {
            if (multimodel != null && multimodel.Count != 0)
            {
                if (txtTdiff.Text != "0,00" && isShowMessage == true)
                {
                    DialogResult dr = RadMessageBox.Show("Do you want to CLOSE this line ?" + " " + txtTdiff.Text, "Delete", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        string invoiceNr = txtInvoice.Text;
                        string customerId = txtClient.Text;
                        decimal bbb = Convert.ToDecimal(txtTdiff.Text);
                        frmDifferenceAmount frm = new frmDifferenceAmount(lookopenlines, bbb, xDaily, multimodel, invoiceNr, customerId);
                        frm.ShowDialog();

                    }
                }
            }
            gridItems.Show();
        }

        private void giveLabels()
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
                            labelClient.Text = cm.nameClient;

                        }
                        else
                        {
                            txtClient.Text = "";
                        }
                    }
                    else
                    {
                        if (pm1.idContPerson != null && pm1.idClient == 0)
                            pmd = pbs.GetPerson(pm1.idContPerson);
                        if (pmd != null)
                        {
                            labelClient.Text = pmd.firstname + " " + pmd.midname + " " + pmd.lastname;
                        }
                        else
                        {
                            txtClient.Text = "";
                        }
                    }
                }
                else
                {
                    labelClient.Text = "";
                }
            }
            else
            {
                labelClient.Text = "";
            }
            //================================ cost
            if (txtCost.Text == "")
            {
                labelCost.Text = "";
            }
            else
            {
                AccCostBUS acc = new AccCostBUS();
                AccCostModel amc = new AccCostModel();
                amc = acc.GetCostByID(txtCost.Text);
                if (amc != null)
                {
                    labelCost.Text = amc.descCost;
                }
                else
                {
                    labelCost.Text = "";
                    txtCost.Text = "";
                }
            }
            //=============================== project
            if (txtProject.Text == "")
            {
                labelProject.Text = "";
                labelProject1.Text = "";
            }
            else
            {
                ArrangementBUS arb = new ArrangementBUS();
                ArrangementModel arm = new ArrangementModel();
                arm = arb.GetArrangementByCode(txtProject.Text);
                if (arm != null)
                {
                    labelProject.Text = arm.nameArrangement;
                    //txtArrdate.Text = "Start " + Convert.ToString(arm.dtFromArrangement) + " End " + Convert.ToString(arm.dtToArrangement);
                    txtArrdate.Text = "Start date =>" + arm.dtFromArrangement.ToShortDateString() + " - " + "end date =>" + arm.dtToArrangement.ToShortDateString();
                    if (arm.dtFromArrangement != null)
                    {
                        xdtProjectStart = Convert.ToDateTime(arm.dtFromArrangement);
                    }
                    if (arm.dtFromArrangement != null)
                        xdtProjectStart = Convert.ToDateTime(arm.dtFromArrangement);

                }
            }

            //============================== account
            if (txtAccount.Text != "")
            {
                LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lam = new LedgerAccountModel();

                lam = ledbus.GetAccount(txtAccount.Text, Login._bookyear);
                if (lam != null)
                {
                    if (txtAccount.Text.Trim() == lam.numberLedgerAccount.Trim())
                    {
                        labelKonto.Text = lam.descLedgerAccount;

                    }
                    else
                    {
                        labelKonto.Text = "";
                    }
                }
                else
                {
                    labelKonto.Text = "";
                }
            }
            else
            {
                labelKonto.Text = "";
            }

        }

        private void txtAmountD_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtAmountC.Value) != 0)
                txtAmountC.Value = 0;
        }

        private void txtAmountC_ValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtAmountD.Value) != 0)
                txtAmountD.Value = 0;
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


        //clean code
        private void gridItems_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (e.Column.Name == "dtLine")
            {
                _gridEditor = this.gridItems.ActiveEditor as BaseGridEditor;
                if (_gridEditor != null)
                {

                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown += new KeyEventHandler(cellEditordtLine_KeyDown);
                }
            }
            
            if (e.Column.Name == "invoiceNr")
            {

                _gridEditor = this.gridItems.ActiveEditor as BaseGridEditor;
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown += new KeyEventHandler(cellEditorInvoice_KeyDown);
                }



            }
            if (e.Column.Name == "numberLedAccount")
            {
                _gridEditor = this.gridItems.ActiveEditor as BaseGridEditor;
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown += new KeyEventHandler(cellEditorAccount_KeyDown);
                }
            }
            if (e.Column.Name == "descLine")
            {
                _gridEditor = this.gridItems.ActiveEditor as BaseGridEditor;
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown += new KeyEventHandler(cellEditordescLine_KeyDown);
                }
            }
            if (e.Column.Name == "idBTW")
            {
                _gridEditor = this.gridItems.ActiveEditor as BaseGridEditor;
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown += new KeyEventHandler(cellEditorBTW_KeyDown);
                }
            }
            if (e.Column.Name == "idClientLine")
            {
                _gridEditor = this.gridItems.ActiveEditor as BaseGridEditor;
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown += new KeyEventHandler(cellEditorClient_KeyDown);
                }
            }

            if (e.Column.Name == "idCostLine")
            {
                _gridEditor = this.gridItems.ActiveEditor as BaseGridEditor;
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown += new KeyEventHandler(cellEditorCost_KeyDown);
                }
            }

            if (e.Column.Name == "idProjectLine")
            {
                _gridEditor = this.gridItems.ActiveEditor as BaseGridEditor;
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown += new KeyEventHandler(cellEditorProject_KeyDown);
                }


            }
        }
        private void gridItems_CellEndEdit(object sender, GridViewCellEventArgs e)
        {


            GridViewCellInfo i = (GridViewCellInfo)e.Row.Cells[e.ColumnIndex];
            endEdit(i);

        }
        private void gridItems_CellClick(object sender, GridViewCellEventArgs e)
        {
            GridCellElement clickedCell = (GridCellElement)sender;


            if (clickedCell.RowElement is GridNewRowElement)
            {

                if (gridItems != null)
                {
                    DialogResult dr = RadMessageBox.Show("Do you want to Add row with values ?", "Delete", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        int aa = gridItems.RowCount;
                        if (aa >= 1)
                        {
                            // RadMessageBox.Show("New row is clicked");
                            gridItems.CurrentRow.Cells["dtLine"].Value = gridItems.Rows[aa - 1].Cells["dtLine"].Value;
                            gridItems.CurrentRow.Cells["invoiceNr"].Value = gridItems.Rows[aa - 1].Cells["invoiceNr"].Value;
                            gridItems.CurrentRow.Cells["numberLedAccount"].Value = gridItems.Rows[aa - 1].Cells["numberLedAccount"].Value;
                            gridItems.CurrentRow.Cells["descLine"].Value = gridItems.Rows[aa - 1].Cells["descLine"].Value;
                            gridItems.CurrentRow.Cells["idClientLine"].Value = gridItems.Rows[aa - 1].Cells["idClientLine"].Value;
                            gridItems.CurrentRow.Cells["idCostLine"].Value = gridItems.Rows[aa - 1].Cells["idCostLine"].Value;
                            gridItems.CurrentRow.Cells["idProjectLine"].Value = gridItems.Rows[aa - 1].Cells["idProjectLine"].Value;
                        }
                    }
                }

            }
        }
        private void gridItems_UserAddingRow(object sender, GridViewRowCancelEventArgs e)
        {
            int aa = gridItems.RowCount;
            if (aa > 1)
            {
                // RadMessageBox.Show("New row is clicked");
                gridItems.CurrentRow.Cells["dtLine"].Value = gridItems.Rows[aa - 1].Cells["dtLine"].Value;
                gridItems.CurrentRow.Cells["invoiceNr"].Value = gridItems.Rows[aa - 1].Cells["invoiceNr"].Value;
                gridItems.CurrentRow.Cells["numberLedAccount"].Value = "";//gridItems.Rows[aa - 1].Cells["numberLedAccount"].Value;
                gridItems.CurrentRow.Cells["descLine"].Value = gridItems.Rows[aa - 1].Cells["descLine"].Value;
                gridItems.CurrentRow.Cells["idClientLine"].Value = gridItems.Rows[aa - 1].Cells["idClientLine"].Value;
                //gridItems.CurrentRow.Cells["idBTW"].Value = 0;//gridItems.Rows[aa - 1].Cells["idBTW"].Value;
                gridItems.CurrentRow.Cells["idCostLine"].Value = gridItems.Rows[aa - 1].Cells["idCostLine"].Value;
                gridItems.CurrentRow.Cells["idProjectLine"].Value = gridItems.Rows[aa - 1].Cells["idProjectLine"].Value;
                //  gridItems.CurrentRow.Cells["project"].Value = gridItems.Rows[aa - 1].Cells["project"].Value;
                if (gridItems.Rows[aa - 1].Cells["incopNr"].Value != null)
                    gridItems.CurrentRow.Cells["incopNr"].Value = gridItems.Rows[aa - 1].Cells["incopNr"].Value;
                if (gridItems.Rows[aa - 1].Cells["iban"].Value != null)
                    gridItems.CurrentRow.Cells["iban"].Value = gridItems.Rows[aa - 1].Cells["iban"].Value;
                if (gridItems.Rows[aa - 1].Cells["dtBooking"].Value != null)
                    gridItems.CurrentRow.Cells["dtBooking"].Value = gridItems.Rows[aa - 1].Cells["dtBooking"].Value;
               

               
            }

            BindingList<AccLineModel> list = new BindingList<AccLineModel>();
            list = (BindingList<AccLineModel>)gridItems.DataSource;
            int numberNext = 0;

            foreach (AccLineModel m in list)
            {
                int c = 0;
                if (m.idMaster.Replace(txtIncop.Text, "") != "")
                    c = Convert.ToInt32(m.idMaster.Replace(txtIncop.Text, ""));

                if (c > numberNext)
                    numberNext = Convert.ToInt32(m.idMaster.Replace(txtIncop.Text, ""));
            }
            numberNext = numberNext + 1;
            if (txtIncop.Text != null)
                gridItems.CurrentRow.Cells["idMaster"].Value = txtIncop.Text + numberNext.ToString();
            gridItems.CurrentRow.Cells["dtLine"].BeginEdit();
        }
        private void gridItems_UserAddedRow(object sender, GridViewRowEventArgs e)
        {
            AccLineModel acn = new AccLineModel();
            
            if (e.Row != null)
            {
                acn = (AccLineModel)e.Row.DataBoundItem;

                AccTaxBUS tb = new AccTaxBUS();
                AccTaxModel tm = new AccTaxModel();
                if (Convert.ToInt32(acn.idBTW) != null)
                    tm = tb.GetTaxByID(Convert.ToInt32(acn.idBTW));

                if (Convert.ToInt32(acn.idBTW) != 0)
                if (tm != null)
                    addBtw(acn.debitBTW, tm.numberLedAccount, acn.idMaster, acn.idMaster);

                totals();
            }
        }
        private void gridItems_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {


            DialogResult dr = RadMessageBox.Show("Do you want to DELETE this line ?", "Delete", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                if (gridItems.CurrentRow != null)
                {
                    GridViewRowInfo info = this.gridItems.CurrentRow;
                    AccLineModel selectedRow = (AccLineModel)info.DataBoundItem;
                    int aaa = selectedRow.idAccLine;
                        if (selectedRow.idDetail != "")
                        {
                            e.Cancel = true;
                            return;
                        }
                        else
                        {
                            AccCreditLineBUS crb = new AccCreditLineBUS();
                            //  if (crb.DeleteIdMaster(selectedRow.idMaster) == true)
                            //  {

                            var itemToRemove = multimodel.Where(s => s.idMaster == selectedRow.idMaster).ToList();
                            foreach (var item in itemToRemove)
                            {
                                multimodel.Remove(item);
                            }
                            var itemToRemove2 = multimodel.Where(s => s.idDetail == selectedRow.idMaster).ToList();
                            foreach (var item in itemToRemove2)
                            {
                                multimodel.Remove(item);
                            }
                            gridItems.DataSource = null;
                            gridItems.DataSource = multimodel;
                            totals();
                        }
                }

            }
            else
            {
                e.Cancel = true;
                return;
            }
        }
        private void gridItems_KeyDown(object sender, KeyEventArgs e)
        {

            if (gridItems != null)
                if (gridItems.CurrentCell != null)
                {
                    if (gridItems.CurrentCell.RowInfo != null)

                        if (gridItems.CurrentCell.ColumnInfo.Name != "dtLine")
                        {
                            cellForKeyDown = gridItems.CurrentRow.Cells["idBTW"];
                        }

                    if (cellForKeyDown != null)
                    {


                        string aa = this.gridItems.CurrentCell.ColumnInfo.Name;


                        if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && aa == "dtLine") //"idProjectLine"
                        {

                            if (cellForKeyDown.RowInfo.Cells["debitBTW"].Value != null && Convert.ToInt32(cellForKeyDown.RowInfo.Cells["debitBTW"].Value) != 0)
                            {
                                int id_tax = Convert.ToInt32(cellForKeyDown.RowInfo.Cells["idBTW"].Value);
                                string konto = getBTWKonto(id_tax);
                                decimal tax_amount = Convert.ToDecimal(cellForKeyDown.RowInfo.Cells["debitBTW"].Value);
                                addBtw(tax_amount, konto, cellForKeyDown.RowInfo.Cells["idMaster"].Value.ToString(), cellForKeyDown.RowInfo.Cells["idDetail"].Value.ToString());
                            }

                        }
                        //}
                    }
                }
        }
        private void gridItems_SelectionChanged(object sender, EventArgs e)
        {

            if (gridItems.CurrentCell != null)
            {
                if (gridItems.CurrentCell.ColumnInfo.Name == "idProjectLine")
                {
                    cellForKeyDown = gridItems.CurrentRow.Cells["idBTW"];
                }

            }

        }
        private void gridItems_ViewCellFormatting(object sender, CellFormattingEventArgs e)
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
        void gridItems_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (gridItems.Columns["debitLine"].IsCurrent || gridItems.Columns["creditLine"].IsCurrent)
            {
                GridSpinEditor spinEditor = this.gridItems.ActiveEditor as GridSpinEditor;
                ((GridSpinEditorElement)spinEditor.EditorElement).ShowUpDownButtons = false;
            }

            //Restriction  mouse wheel and KayUp, KeyDown for grid when is in Edit mode Gorance 26 08
            var editor = e.ActiveEditor as GridSpinEditor;
            if (editor != null)
            {
                var element = editor.EditorElement as GridSpinEditorElement;
                element.InterceptArrowKeys = false;
                element.EnableMouseWheel = false;
            }

        }
        private void endEdit(GridViewCellInfo e)
        {
            try
            {

                    if (e.ColumnInfo.Name == "dtLine")
                {
                    if (_gridEditor != null)
                    {
                        RadItem element = _gridEditor.EditorElement as RadItem;
                        element.KeyDown -= cellEditordtLine_KeyDown;
                    }
                    _gridEditor = null;
                }
                if (e.ColumnInfo.Name == "invoiceNr")
                {
                    if (_gridEditor != null)
                    {
                        RadItem element = _gridEditor.EditorElement as RadItem;
                        element.KeyDown -= cellEditorInvoice_KeyDown;
                    }
                    _gridEditor = null;


                }
                if (e.ColumnInfo.Name == "numberLedAccount")
                {
                    if (_gridEditor != null)
                    {
                        RadItem element = _gridEditor.EditorElement as RadItem;
                        element.KeyDown -= cellEditorAccount_KeyDown;
                    }
                    _gridEditor = null;
                }
                if (e.ColumnInfo.Name == "descLine")
                {
                    if (_gridEditor != null)
                    {
                        RadItem element = _gridEditor.EditorElement as RadItem;
                        element.KeyDown -= cellEditordescLine_KeyDown;
                    }
                    _gridEditor = null;
                }

                if (e.ColumnInfo.Name == "idBTW")
                {
                    if (_gridEditor != null)
                    {
                        RadItem element = _gridEditor.EditorElement as RadItem;
                        element.KeyDown -= cellEditorBTW_KeyDown;
                    }
                    _gridEditor = null;
                    if (e.ColumnInfo.Name == "idBTW")
                    {
                        if (e.Value != null)
                        {
                            int d = (int)e.Value;
                            // if (d != 0)
                            // {
                            decimal percent = getPercent(d);
                            int inc_excl = getInc_exlKonto(d);
                            GridViewRowInfo info = e.RowInfo.ViewInfo.CurrentRow;
                            if (inc_excl == 2)
                                info.Cells["debitBTW"].Value = Math.Abs(Convert.ToDecimal(gridItems.CurrentRow.Cells["debitLine"].Value) - Convert.ToDecimal(gridItems.CurrentRow.Cells["creditLine"].Value)) * percent / 100;
                            else
                            {
                                if (inc_excl != 0)
                                {
                                   
                                }
                            }
                        }
                    }
                }

                if (e.ColumnInfo.Name == "idClientLine")
                {
                    if (_gridEditor != null)
                    {
                        RadItem element = _gridEditor.EditorElement as RadItem;
                        element.KeyDown -= cellEditorClient_KeyDown;
                    }
                    _gridEditor = null;
                }

                if (e.ColumnInfo.Name == "idCostLine")
                {
                    if (_gridEditor != null)
                    {
                        RadItem element = _gridEditor.EditorElement as RadItem;
                        element.KeyDown -= cellEditorCost_KeyDown;
                    }
                    _gridEditor = null;
                }

                if (e.ColumnInfo.Name == "idProjectLine")
                {
                    if (_gridEditor != null)
                    {
                        RadItem element = _gridEditor.EditorElement as RadItem;
                        element.KeyDown -= cellEditorProject_KeyDown;
                    }
                    _gridEditor = null;
                }

                if (e.ColumnInfo.Name == "debitLine")
                {
                    if (e.Value != null)
                    {
                        decimal d = (decimal)e.Value;
                        if (d != 0)
                        {
                            GridViewRowInfo info = e.RowInfo.ViewInfo.CurrentRow;
                            info.Cells["creditLine"].Value = "0,00";
                        }
                    }
                    totals();
                }

                if (e.ColumnInfo.Name == "creditLine")
                {
                    if (e.Value != null)
                    {
                        decimal d = (decimal)e.Value;
                        if (d != 0)
                        {
                            GridViewRowInfo info = e.RowInfo.ViewInfo.CurrentRow;
                            info.Cells["debitLine"].Value = "0,00";
                        }
                    }
                    totals();
                }
            }
            catch (Exception exep)
            {

            }
        }
        void cellEditordtLine_KeyDown(object sender, KeyEventArgs e)
        {
            RadTextBoxEditorElement editor = sender as RadTextBoxEditorElement;
            if (e.KeyCode == Keys.F1)
            {
                int aa = gridItems.RowCount;
                if (aa > 1)
                {
                    if (gridItems.Rows[aa - 1].Cells["dtLine"].Value != null)
                        editor.Text = gridItems.Rows[aa - 1].Cells["dtLine"].Value.ToString();
                }
            }
        }
        void cellEditordescLine_KeyDown(object sender, KeyEventArgs e)
        {
            RadTextBoxEditorElement editor = sender as RadTextBoxEditorElement;
            if (e.KeyCode == Keys.F1)
            {
                int aa = gridItems.RowCount;

                editor.Text = gridItems.Rows[aa - 1].Cells["descLine"].Value.ToString();
            }
        }
        void cellEditorBTW_KeyDown(object sender, KeyEventArgs e)
        {
            RadTextBoxEditorElement editor = sender as RadTextBoxEditorElement;
            if (e.KeyCode == Keys.F1)
            {
                int aa = gridItems.RowCount;

                editor.Text = gridItems.Rows[aa - 1].Cells["idBTW"].Value.ToString();
            }
            if (e.KeyCode == Keys.F2)
            {
                AccTaxBUS ccentar4 = new AccTaxBUS();
                List<IModel> gmX4m = new List<IModel>();

                gmX4m = ccentar4.GetAllTax(Login._user.lngUser);
                var dlgSave4 = new GridLookupForm(gmX4m, "Btw");
                if (dlgSave4.ShowDialog(this) == DialogResult.Yes)
                {
                    AccTaxModel gmX4ml = new AccTaxModel();
                    gmX4ml = (AccTaxModel)dlgSave4.selectedRow;
                    editor.Text = gmX4ml.idTax.ToString();
                    //labelBtw2.Text = gmX4ml.descTax;
                }
            }
        }
        void cellEditorInvoice_KeyDown(object sender, KeyEventArgs e)
        {

            RadTextBoxEditorElement editor = sender as RadTextBoxEditorElement;
            if (e.KeyCode == Keys.F1)
            {
                int aa = gridItems.RowCount;

                editor.Text = gridItems.Rows[aa - 1].Cells["invoiceNr"].Value.ToString();
            }

        }
        void cellEditorClient_KeyDown(object sender, KeyEventArgs e)
        {
            RadTextBoxEditorElement editor = sender as RadTextBoxEditorElement;
            if (e.KeyCode == Keys.F1)
            {
                int aa = gridItems.RowCount;

                editor.Text = gridItems.Rows[aa - 1].Cells["idClientLine"].Value.ToString();
            }
            if (editor.Text != txtClient.Text)
            {
                RadMessageBox.Show("Not valid client number!");
            }
        }
        void cellEditorAccount_KeyDown(object sender, KeyEventArgs e)
        {
            RadTextBoxEditorElement editor = sender as RadTextBoxEditorElement;
            if (e.KeyCode == Keys.F1)
            {
                int aa = gridItems.RowCount;

                editor.Text = gridItems.Rows[aa - 1].Cells["numberLedAccount"].Value.ToString();
            }
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
                    if (lam == null)
                    {
                        RadMessageBox.Show("Wrong account");
                        editor.Focus();

                    }
                }
            }


        }
        void cellEditorCost_KeyDown(object sender, KeyEventArgs e)
        {
            RadTextBoxEditorElement editor = sender as RadTextBoxEditorElement;
            if (e.KeyCode == Keys.F1)
            {
                int aa = gridItems.RowCount;

                editor.Text = gridItems.Rows[aa - 1].Cells["idCostLine"].Value.ToString();
            }
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
                    if (amc == null)
                    {
                        RadMessageBox.Show("Wrong Cost code");
                        editor.Focus();
                    }
                }

            }

        }
        void cellEditorProject_KeyDown(object sender, KeyEventArgs e)
        {
            RadTextBoxEditorElement editor = sender as RadTextBoxEditorElement;
            if (e.KeyCode == Keys.F1)
            {
                int aa = gridItems.RowCount;

                editor.Text = gridItems.Rows[aa - 1].Cells["idProjectLine"].Value.ToString();
            }
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
                        {
                            editor.Text = genmX.codeProject;
                            if (gridItems.CurrentRow.Cells["numberLedAccount"].Value.ToString().Trim() != defCreditor.Trim())
                                gridItems.CurrentRow.Cells["dtLine"].Value = genmX.dtFromArrangement;

                        }

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
                    if (arm == null)
                    {
                        RadMessageBox.Show("Wrong Arrangement code");
                        editor.Focus();
                    }
                    else
                    {
                        if (gridItems.CurrentRow.Cells["debitBTW"].Value != null && Convert.ToInt32(gridItems.CurrentRow.Cells["debitBTW"].Value) != 0)
                        {
                            int id_tax = Convert.ToInt32(gridItems.CurrentRow.Cells["idBTW"].Value);
                            string konto = getBTWKonto(id_tax);
                            decimal tax_amount = Convert.ToDecimal(gridItems.CurrentRow.Cells["debitBTW"].Value);
                            if (gridItems.CurrentRow.Cells["idMaster"].Value != null && gridItems.CurrentRow.Cells["idDetail"].Value != null)
                                addBtw(tax_amount, konto, gridItems.CurrentRow.Cells["idMaster"].Value.ToString(), gridItems.CurrentRow.Cells["idDetail"].Value.ToString());
                        }
                    }


                }
                else
                {
                    if (gridItems.CurrentRow.Cells["debitBTW"].Value != null && Convert.ToInt32(gridItems.CurrentRow.Cells["debitBTW"].Value) != 0)
                    {
                        int id_tax = Convert.ToInt32(gridItems.CurrentRow.Cells["idBTW"].Value);
                        string konto = getBTWKonto(id_tax);
                        decimal tax_amount = Convert.ToDecimal(gridItems.CurrentRow.Cells["debitBTW"].Value);
                        if (gridItems.CurrentRow.Cells["idMaster"].Value != null && gridItems.CurrentRow.Cells["idDetail"].Value != null)
                            addBtw(tax_amount, konto, gridItems.CurrentRow.Cells["idMaster"].Value.ToString(), gridItems.CurrentRow.Cells["idDetail"].Value.ToString());
                        //   gridItems.CurrentRow.Cells["debitBTW"].Value = 0;
                    }
                }
            }

        }
       
        private void addBtw(decimal amt, string konto, string idMaster, string idDetail)
        {
            try
            {

                BindingList<AccLineModel> listFirst = new BindingList<AccLineModel>();
                listFirst =  (BindingList<AccLineModel>) gridItems.DataSource;

                List<AccLineModel> listAll = new List<AccLineModel>();
                listAll =  new List<AccLineModel>(listFirst);

                List<AccLineModel> listDetail = new List<AccLineModel>();
                listDetail = listAll.FindAll(s => s.idDetail == idMaster);

                List<AccLineModel> listMaster = new List<AccLineModel>();
                listMaster = listAll.FindAll(s => s.idMaster == idMaster);

                if (idDetail == "")
                    if (listDetail != null)
                        if (listDetail.Count > 0)
                        {
                            listAll.Remove(listDetail[0]);
                            if (listMaster != null)
                                if (listMaster.Count > 0)
                                    if (listMaster[0].idBTW == 0)
                                    {
                                        listMaster[0].debitBTW = 0;
                                        gridItems.DataSource = null;
                                        gridItems.DataSource = listAll;
                                        return;
                                    }
                        }


                linesmodel = new AccLineModel();                // druga stavka 1610 sa ravnotezom 

                linesmodel.dtLine = Convert.ToDateTime(txtDate.Value);
                linesmodel.invoiceNr = txtInvoice.Text;
                linesmodel.numberLedAccount = konto;
                linesmodel.descLine = txtDesc.Text;
                linesmodel.idClientLine = txtClient.Text;
                linesmodel.idBTW = 0;
                linesmodel.idCostLine = txtCost.Text;
                linesmodel.idProjectLine = txtProject.Text;

                linesmodel.debitLine = 0;
                linesmodel.creditLine = 0;
                if (Convert.ToDecimal(txtAmountC.Text) > 0)
                    linesmodel.debitLine = Math.Abs(amt);
                else
                    linesmodel.creditLine = amt;

                linesmodel.incopNr = txtIncop.Text;
                //linesmodel.iban = txtIban.Text;
                //linesmodel.dtBooking = Convert.ToDateTime(dpValuta.Value);
                //linesmodel.booksort = lines.Count + 1;

                List<AccLineModel> list = new List<AccLineModel>();
                list = (List<AccLineModel>)gridItems.DataSource;

                int numberNext = 0;

                foreach (AccLineModel m in list)
                {
                    if (Convert.ToInt32(m.idMaster.Replace(txtIncop.Text, "")) > numberNext)
                        numberNext = Convert.ToInt32(m.idMaster.Replace(txtIncop.Text, ""));
                }

                numberNext = numberNext + 1;
                if (listDetail.Count > 0)
                    linesmodel.idMaster = listDetail[0].idMaster;
                else
                    linesmodel.idMaster = txtIncop.Text + numberNext.ToString();
                linesmodel.idDetail = idMaster;

                list.Add(linesmodel);
                gridItems.DataSource = null;
                gridItems.DataSource = list;
                int rw = list.Count;
                if (rw >= 1)
                {
                    gridItems.Rows[rw - 1].Cells["numberLedAccount"].IsSelected = true;
                    gridItems.Rows[rw - 1].Cells["numberLedAccount"].BeginEdit();
                }

            }
            catch (Exception e)
            {

            }
        }
        private string getBTWKonto(int btw)
        {
            xBtwConto = "";
            int gridBtw;
            gridBtw = btw;
            if (gridBtw != 0)  //==== uzima procenat ===
            {
                AccTaxBUS atb = new AccTaxBUS();
                AccTaxModel atm = new AccTaxModel();
                atm = atb.GetTaxByID(gridBtw);
                if (atm != null)
                {
                    xBtwConto = atm.numberLedAccount;
                }

            }
            return xBtwConto;

        }

        private int getInc_exlKonto(int btw)
        {
            int gridBtw = 0;
            int inc_excl = 0;
            gridBtw = btw;
            if (gridBtw != 0)  //==== uzima procenat ===
            {
                AccTaxBUS atb = new AccTaxBUS();
                AccTaxModel atm = new AccTaxModel();
                atm = atb.GetTaxByID(gridBtw);
                if (atm != null)
                {
                    inc_excl = Convert.ToInt32(atm.typeTax);
                }

            }
            return inc_excl;

        }

        private decimal getPercent(int btw)
        {

            decimal btw_percent = 0;
            int gridBtw = 0;
            gridBtw = btw;
            if (gridBtw != 0)  //==== uzima procenat ===
            {
                AccTaxBUS atb = new AccTaxBUS();
                AccTaxModel atm = new AccTaxModel();
                atm = atb.GetTaxByID(gridBtw);
                if (atm != null)
                {
                    xcodeBTW = atm.codeTax;
                }

                if (xcodeBTW != "" && xcodeBTW != null)
                {
                    // int b = Convert.ToInt32(linesmodel.idBTW);
                    AccTaxValidityBUS txb = new AccTaxValidityBUS();
                    AccTaxValidityModel tm = new AccTaxValidityModel();
                    tm = txb.GetTaxValidityByCode(xcodeBTW);                 
             
                    
                    
                    if (tm != null)
                    {
                        btw_percent = Convert.ToDecimal(tm.percentTax);

                    }
                }

            }
            return btw_percent;

        }

        private void frmDailyBankNew_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void txtDate_ValueChanged(object sender, EventArgs e)
        {
           
            if (multimodel != null)
                if (txtDate.Value != null)
                    for (int n = 0; n < multimodel.Count; n++)
                    {
                        multimodel[n].dtLine = txtDate.Value;
                        gridItems.DataSource = null;
                        gridItems.DataSource = multimodel; 
                    }

        }
        private void hideOpenLine(bool hide)
        {
            if (hide == true)
            {
                txtProject.Hide();
                btnProject.Hide();
                lblProject.Hide();
                labelProject.Hide();
                txtArrdate.Hide();
                txtCost.Hide();
                btnCost.Hide();
                lblCost.Hide();
                labelCost.Hide();
                labelBtw.Hide();
                btnBtw.Hide();
                lblBtw.Hide();
                txtBtwCode.Hide();
                lblAccount.Hide();
                txtAccount.Hide();
                btnAccount.Hide();
                labelKonto.Hide();
            }
            else
            {
                txtArrdate.Visible = true;
                txtProject.Visible = true;
                btnProject.Visible = true;
                lblProject.Visible = true;
                labelProject.Visible = true;
                txtCost.Visible = true;
                btnCost.Visible = true;
                lblCost.Visible = true;
                labelCost.Visible = true;
                labelBtw.Visible = true;
                btnBtw.Visible = true;
                lblBtw.Visible = true;
                txtBtwCode.Visible = true;
                lblAccount.Visible = true;
                txtAccount.Visible = true;
                btnAccount.Visible = true;
                labelKonto.Visible = true;

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

        private void txtDesc_TextChanged(object sender, EventArgs e)
        {
            if (gridItems != null && gridItems.Rows.Count > 0)
            {
                AccLineModel acm = new AccLineModel();
                acm = (AccLineModel)gridItems.Rows[0].DataBoundItem;
                acm.descLine = txtDesc.Text;
                gridItems.Rows[0].InvalidateRow();
            }
        }

        private void gridItems_CellValueChanged(object sender, GridViewCellEventArgs e)
        {

            if (txtDesc.Text != gridItems.Rows[0].Cells["descLine"].Value.ToString()) 
            {
                txtDesc.Text = gridItems.Rows[0].Cells["descLine"].Value.ToString();
            }
        }

        private void txtInvoice_Click(object sender, EventArgs e)
        {
            lookupOpenLine();
        }

        private void gridItems_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            string saveLayout = "Save Layout";
            using(ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }

            RadMenuItem customMenuItem = new RadMenuItem();
            customMenuItem.Text = saveLayout;
            customMenuItem.Click += SaveLayoutDailyBankNew;

            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);

            //==delete 
            string saveLayout1 = "Delete Layout";
            using(ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }

            RadMenuItem customMenuItem1 = new RadMenuItem();
            customMenuItem1.Text = saveLayout1;
            customMenuItem1.Click += SaveLayoutDailyBankD;

            RadMenuSeparatorItem separator1 = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator1);
            e.ContextMenu.Items.Add(customMenuItem1);
        }

        private void SaveLayoutDailyBankNew(object sender, EventArgs e)
        {
            if(File.Exists(layoutDailyBankNew))
            {
                File.Delete(layoutDailyBankNew);
            }

            gridItems.SaveLayout(layoutDailyBankNew);

            translateRadMessageBox trs = new translateRadMessageBox();
            trs.translateAllMessageBox("You have successfully save layout!");
        }

        private void SaveLayoutDailyBankD(object sender, EventArgs e)
        {
            if(File.Exists(layoutDailyBankNew))
            {
                File.Delete(layoutDailyBankNew);
            }

            translateRadMessageBox trs = new translateRadMessageBox();
            trs.translateAllMessageBox("You have successfully delete layout!");
        }

        private void txtInvoice_Enter(object sender, EventArgs e)
        {
            if (txtInvoice.Focused && txtInvoice.Text == "") 
            {
                btnOpenLines.PerformClick();
            }
        }


        /*private void addBtw(decimal amt, string konto, string idMaster, string idDetail)
        {
            try
            {
                AccAcountUpdate aUr = new AccAcountUpdate();

                List<AccLineModel> listAll = new List<AccLineModel>();
                listAll = (List<AccLineModel>)gridItems.DataSource;

                List<AccLineModel> listDetail = new List<AccLineModel>();
                listDetail = listAll.FindAll(s => s.idDetail == idMaster);

                List<AccLineModel> listMaster = new List<AccLineModel>();
                listMaster = listAll.FindAll(s => s.idMaster == idMaster);

                if (idDetail == "")
                    if (listDetail != null)
                        if (listDetail.Count > 0)
                        {
                            listAll.Remove(listDetail[0]);
                            if (listMaster != null)
                                if (listMaster.Count > 0)
                                    if (listMaster[0].idBTW == 0)
                                    {
                                        listMaster[0].debitBTW = 0;
                                        gridItems.DataSource = null;
                                        gridItems.DataSource = listAll;
                                        return;
                                    }
                        }


                linesmodel = new AccLineModel();                // druga stavka 1610 sa ravnotezom 

                linesmodel.dtLine = Convert.ToDateTime(dpDate2.Value);
                linesmodel.invoiceNr = txtInvoiceNr2.Text;
                linesmodel.numberLedAccount = konto;
                linesmodel.descLedgerAccount = aUr.AccountName(konto);
                linesmodel.descLine = txtDescription2.Text;
                linesmodel.idClientLine = txtClient2.Text;
                linesmodel.idBTW = 0;
                linesmodel.idCostLine = txtCost2.Text;
                linesmodel.idProjectLine = txtProject2.Text;

                linesmodel.debitLine = 0;
                linesmodel.creditLine = 0;
                if (Convert.ToDecimal(txtAmount2Credit.Text) > 0)
                    linesmodel.debitLine = Math.Abs(amt);
                else
                    linesmodel.creditLine = amt;

                linesmodel.incopNr = txtInkop2.Text;
                linesmodel.iban = txtIban2.Text;
                linesmodel.dtBooking = Convert.ToDateTime(dpValuta2.Value);
                linesmodel.booksort = lines.Count + 1;

                List<AccLineModel> list = new List<AccLineModel>();
                list = (List<AccLineModel>)gridItems.DataSource;

                int numberNext = 0;

                foreach (AccLineModel m in list)
                {
                    if (Convert.ToInt32(m.idMaster.Replace(txtInkop2.Text, "")) > numberNext)
                        numberNext = Convert.ToInt32(m.idMaster.Replace(txtInkop2.Text, ""));
                }

                numberNext = numberNext + 1;
                if (listDetail.Count > 0)
                    linesmodel.idMaster = listDetail[0].idMaster;
                else
                    linesmodel.idMaster = txtInkop2.Text + numberNext.ToString();
                linesmodel.idDetail = idMaster;

                lines.Add(linesmodel);
                gridItems.DataSource = null;
                gridItems.DataSource = lines;
                int rw = lines.Count;
                if (rw >= 1)
                {
                    gridItems.Rows[rw - 1].Cells["dtLine"].IsSelected = true;
                    gridItems.Rows[rw - 1].Cells["dtLine"].BeginEdit();
                }

            }
            catch (Exception e)
            {

            }


        }*/
    }
}
