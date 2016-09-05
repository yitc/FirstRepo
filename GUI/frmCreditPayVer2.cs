using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.Windows;
using Telerik.WinForms;
using BIS.Business;
using BIS.Model;
using BIS.DAO;
using System.Resources;
using System.IO;
using Telerik.WinControls.UI;
using NUnit.Framework;
using System.Threading;


namespace GUI
{
     [TestFixture]
    public partial class frmCreditPayVer2 : Telerik.WinControls.UI.RadForm
    {
        private AccLineModel model;
        private AccLineModel model1;
        private AccLineModel linesmodel;
        public List<AccCreditPayModel> listm;
        public List<AccCreditPayModel> listmNo;
        public List<AccCreditPayModel> listmApp;
        public List<AccCreditPayModel> listmBook;
        public List<AccCreditPayModel> listEB;
       
        public List<AccLineModel> accmodel;
        public AccCreditPayModel entereb; 
        private AccLineModel accline;
        private List<AccLineModel> gridaccline;
        private List<AccLineModel> lines;
        private List<AccLineModel> splitlist;
        private string layoutEnter;
        private string layoutApproved;
        private string layoutNoApproved;
        private string layoutBook;
        private string layoutEnterBook;
        private string layoutLineBook;
        private AccCreditPayBUS bus;
        private AccLineBUS aclbus;
        private AccCreditPayModel enterm;
        public AccCreditPayModel enterb;
        private bool isNew;
        private bool isEdit;
        private bool isNew2;
        private bool isEdit2;
        private AccCreditPayModel selectedRowNo;
        private AccCreditPayModel selectedRowGrid;
        private int xDaily = 0;
        private string xConto;
        private int idCli = 0;
        public int idDoc { get; set; }
        public int idCP = 0;
        private bool manCreditor = false;
        private bool manDebitor = false;
        private bool manCost = false;
        private bool manProject = false;
        private bool manBTW = false;
        private string xAccount;
        private string masterAccount;
        BaseGridEditor _gridEditor;
        private bool existdiff=false;
        private int iD = 0;
        private int idTsk = 0;
        private bool notBooked = false;
        private bool isSplitOk = false;
        private int iDcredit = 0;
        private decimal debit = 0;
        private decimal credit = 0;
        private DateTime fromdate;
       // bool isRowChange = false;
        private string reserve_acc;
        private int idArange;
        private AccLineModel row_toMake;
        private List<AccCreditLinePayModel> multimodel;
        private  decimal btw_percent=0;
        private int inc_excl = 0;
        private string xBtwConto = "";
        private string acc_reservationAcc = "";
        private string defCreditor;
        private bool right_panel_change = false;
        private bool proj_update = false;

        private int eventCount = 0;

        private GridViewCellInfo cellForKeyDown = null;
       
        

        public frmCreditPayVer2()
        {
            InitializeComponent();
        }
        [Test]
        private void frmCreditPayVer2_Load(object sender, EventArgs e)
        {
           

            right_panel_change = false;
            labelPayDays.Text = "";

            radPagesPay.SelectedPage = rpvEnterBook;


            AccSettingsBUS sb = new AccSettingsBUS();
            AccSettingsModel sm = new AccSettingsModel();
            sm = sb.GetSettingsByID(Login._bookyear);
            if (sm != null)
            {
                if (sm.defReservationAcc != null && sm.defReservationAcc != "")
                {
                    reserve_acc = sm.defReservationAcc;
                    defCreditor = sm.defCreditorAccount;
                   
                }
                else
                {
                    reserve_acc = "1610";
                   
                }
                if (sm.defCreditorAccount != null && sm.defCreditorAccount != "")
                              masterAccount = sm.defCreditorAccount;
                else
                              masterAccount = "1600";
                //if (sm.idDailyFak != null && sm.idDailyFak != 0)
                //{
                //    xDaily = Convert.ToInt32(sm.idDailyFak);
                //    AccDailyBUS adb = new AccDailyBUS();
                //    AccDailyModel adm = new AccDailyModel();
                //    adm = adb.GetDailysById(xDaily);
                //    if (adm != null)
                //    {
                //        xConto = adm.numberLedgerAccount;
                //        txtDaily.Text = sm.idDailyFak.ToString() + " " + adm.descDaily;
                //    }
                //}
            }
            else
            {
                reserve_acc = "1610";
                masterAccount = "1600";
            }

            //=============== ako ima slogova \Crveno =====================
            bus = new AccCreditPayBUS();
            listmBook = bus.GetAllPaysApproved();
            if (listmBook != null && listmBook.Count > 0)
            {
                this.rpvBook.Item.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                this.rpvBook.Item.ForeColor = System.Drawing.Color.Black;
            }

            List<AccDailyModel> adm = new List<AccDailyModel>();
            adm = new AccDailyBUS(Login._bookyear).GetBookingDailysInkop2();
            if (adm != null)
            {
                xDaily = adm[0].idDaily;
                xConto = adm[0].numberLedgerAccount;
                txtDaily.Text = adm[0].codeDaily + "   " + adm[0].descDaily;
            }
            //==========================================================
            //==========================================================
            //================= telerik code za mask polje
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("nl-NL");
            txtAmount2Credit.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            txtAmount2Credit.Mask = "N2";
            txtAmount2Credit.Culture = new System.Globalization.CultureInfo("nl-NL");

            this.txtAmount2Credit.KeyUp += txtAmount2Credit_KeyUp;

            txtAmount2Debit.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            txtAmount2Debit.Mask = "N2";
            txtAmount2Debit.Culture = new System.Globalization.CultureInfo("nl-NL");

            this.txtAmount2Debit.KeyUp += txtAmount2Debit_KeyUp;
            //================================================

            //==========================================================
            //if (Login._user.isUserManager == false)
            //    rpvApproved.Enabled = false;
            if (Login._user.isAccountUser == false)
            {
                rpvEnterBook.Enabled = false;
                rpvBook.Enabled = false;
            }
          
            clearform();
            layoutEnter = MainForm.gridFiltersFolder + "\\layoutCreditPay.xml";
            layoutApproved = MainForm.gridFiltersFolder + "\\layoutApproved.xml";
            layoutNoApproved = MainForm.gridFiltersFolder + "\\layoutNoApproved.xml";
            layoutBook = MainForm.gridFiltersFolder + "\\layoutBook.xml";
            layoutEnterBook = MainForm.gridFiltersFolder + "\\layoutEnterBook.xml";
            layoutLineBook = MainForm.gridFiltersFolder + "\\layoutLineBook.xml";
           // layoutEnter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_lookup\\" + "layoutCreditPay.xml");
            
            this.gridBookPay.GotFocus += gridBookPay_GotFocus;
            setTranslate();
            
            dpDate2.Value = DateTime.Now;
            dpValuta2.Value = DateTime.Now;
           
            ddlOption.DataSource = new AccCreditOptionBUS().GetAllOptions();
            ddlOption.DisplayMember = "descriptionOption";
            ddlOption.ValueMember = "idOption";


           
            ddlCurr2.SelectedIndex = 0;
           // radPagesPay.SelectedPage.Name = "rpvEnter";
            bus = new AccCreditPayBUS();
            listm = new List<AccCreditPayModel>();
            if (Login._user.isAccountUser == true)
           {
               EnterBookKeeper();
           }
           else
           {
            listm =  bus.GetAllPays();
          //  model = new BindingList<AccCreditPayModel>(listm);
          

            
           }
        }

     
        private void setTranslate()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblDate2.Text) != null)
                    lblDate2.Text = resxSet.GetString(lblDate2.Text);

                if (resxSet.GetString(lblValuta2.Text) != null)
                    lblValuta2.Text = resxSet.GetString(lblValuta2.Text);

                if (resxSet.GetString(lblAmountC2.Text) != null)
                    lblAmountC2.Text = resxSet.GetString(lblAmountC2.Text);

                if (resxSet.GetString(lblInvoice2.Text) != null)
                    lblInvoice2.Text = resxSet.GetString(lblInvoice2.Text);

                if (resxSet.GetString(lblClient2.Text) != null)
                    lblClient2.Text = resxSet.GetString(lblClient2.Text);

                if (resxSet.GetString(lblAccount2.Text) != null)
                    lblAccount2.Text = resxSet.GetString(lblAccount2.Text);

                if (resxSet.GetString(lblBtw2.Text) != null)
                    lblBtw2.Text = resxSet.GetString(lblBtw2.Text);

                if (resxSet.GetString(lblCost2.Text) != null)
                    lblCost2.Text = resxSet.GetString(lblCost2.Text);

                if (resxSet.GetString(lblProject2.Text) != null)
                    lblProject2.Text = resxSet.GetString(lblProject2.Text);

                if (resxSet.GetString(lblIncop2.Text) != null)
                    lblIncop2.Text = resxSet.GetString(lblIncop2.Text);

                if (resxSet.GetString(lblDescription2.Text) != null)
                    lblDescription2.Text = resxSet.GetString(lblDescription2.Text);

               
                if (resxSet.GetString(btnNew2.Text) != null)
                    btnNew2.Text = resxSet.GetString(btnNew2.Text);
                if (resxSet.GetString(btnSave2.Text) != null)
                    btnSave2.Text = resxSet.GetString(btnSave2.Text);
                if (resxSet.GetString(btnCancel2.Text) != null)
                    btnCancel2.Text = resxSet.GetString(btnCancel2.Text);
                if (resxSet.GetString(btnGetDoc2.Text) != null)
                    btnGetDoc2.Text = resxSet.GetString(btnGetDoc2.Text);
                if (resxSet.GetString(btnDelete.Text) != null)
                    btnDelete.Text = resxSet.GetString(btnDelete.Text);

                if (resxSet.GetString(btnViewDoc2.Text) != null)
                    btnViewDoc2.Text = resxSet.GetString(btnViewDoc2.Text);
                if (resxSet.GetString(btnViewPayment.Text) != null)
                    btnViewPayment.Text = resxSet.GetString(btnViewPayment.Text);

                if (resxSet.GetString(btnSplit.Text) != null)
                    btnSplit.Text = resxSet.GetString(btnSplit.Text);
                if (resxSet.GetString(btnBooking.Text) != null)
                    btnBooking.Text = resxSet.GetString(btnBooking.Text);
                if (resxSet.GetString(btnCancel2.Text) != null)
                    btnCancel2.Text = resxSet.GetString(btnCancel2.Text);

                if (resxSet.GetString(lblApproveBook.Text) != null)
                    lblApproveBook.Text = resxSet.GetString(lblApproveBook.Text);
                if (resxSet.GetString(rpvBook.Text) != null)
                    rpvBook.Text = resxSet.GetString(rpvBook.Text);
            
                if (resxSet.GetString(lblApproveBook.Text) != null)
                    lblApproveBook.Text = resxSet.GetString(lblApproveBook.Text);
              
                if (resxSet.GetString(lblDaily.Text) != null)
                    lblDaily.Text = resxSet.GetString(lblDaily.Text);
                if (resxSet.GetString(lblOption.Text) != null)
                    lblOption.Text = resxSet.GetString(lblOption.Text);
                if (resxSet.GetString(lblPayDays.Text) != null)
                    lblPayDays.Text = resxSet.GetString(lblPayDays.Text);

                if (resxSet.GetString(btnEnter.Text) != null)
                    btnEnter.Text = resxSet.GetString(btnEnter.Text);
               // enterb = new AccCreditPayModel();
            }
        }
        private void clearform()
        {
          
            txtAmount2Debit.Text = "";
            //idTsk = 0;
            idDoc = 0;
            
        }


     
        #region buttons
  

    

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //if (isEdit2 == false)
            //{
                //DialogResult dr = RadMessageBox.Show("Do you want to Cancel ?", "Cancel", MessageBoxButtons.YesNo);
            translateRadMessageBox msgbox = new translateRadMessageBox();
            DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("Do you want to Cancel ?", "Cancel");
                if (dr == DialogResult.Yes)
                {
                    right_panel_change = false;
                    ddlOption.SelectedIndex = 0;

                    if (isNew2 == true)
                    {
                        clearform2();
                        clearform();
                        //isEdit2 = false;
                        //isNew2 = true;
                        isEdit2 = true;
                        isNew2 = false;
                        gridItems.DataSource = null;
                        lines.Clear();
                        lines = new List<AccLineModel>();
                      
                        //dpDate2.Focus();
                    }
                    //else
                    //{
                        if (isEdit2==true)
                            gridBookPay.Focus();
                        if (gridBookPay.SelectedRows != null)
                            if (gridBookPay.SelectedRows.Count > 0)
                            {

                                AccCreditPayModel selectedRowGrid = (AccCreditPayModel)gridBookPay.SelectedRows[0].DataBoundItem;
                                enterb = new AccCreditPayModel();
                                if (selectedRowGrid != null)
                                {
                                    enterb = selectedRowGrid;
                                    iD = 0;
                                    iD = selectedRowGrid.idCreditPay;
                                    iDcredit = selectedRowGrid.idCreditPay;
                                    fillform2(enterb);

                                    lines = new AccCreditLineBUS().GetLine(iD);
                                    gridItems.DataSource = null;
                                    gridItems.DataSource = lines;
                                   // isRowChange = false;
                                    gridItems.Show();
                                }
                            }
                    //}
                }
          
         
        }

        private void btnCreditor_Click(object sender, EventArgs e)
        {
            DebCreLookupBUS debpers = new DebCreLookupBUS();
            List<IModel> pm1 = new List<IModel>();

            pm1 = debpers.GetCreditors();
            var dlgSave = new GridLookupForm(pm1, "Creditor");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                if (enterm == null)
                {
                    translateRadMessageBox msg = new translateRadMessageBox();
                    msg.translateAllMessageBox("You have to press New first !!");

                    //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    //{
                    //    if (resxSet.GetString("You have to press New first !!") != null)
                    //        RadMessageBox.Show(resxSet.GetString("You have to press New first !!"));
                    //    else
                    //        RadMessageBox.Show("You have to press New first !!");
                    //}
                }
                else
                {
                    DebCreLookupModel pm1X = new DebCreLookupModel();
                    pm1X = (DebCreLookupModel)dlgSave.selectedRow;
                    //set textbox
                   
                    enterm.accNumber = pm1X.accNumber;
                    
                    //=== payment days
                    AccDebCreBUS adc = new AccDebCreBUS();
                    AccDebCreModel adm = new AccDebCreModel();
                    adm = adc.GetCustomerByAccCode(pm1X.accNumber);
                    if (adm != null)
                    {
                        if (adm.payCondition != null && adm.payCondition != 0)
                        {
                            AccPaymentBUS pmb = new AccPaymentBUS();
                            AccPaymentModel pmm = new AccPaymentModel();
                            pmm = pmb.GetPaymentByID(adm.payCondition);
                            if (pmm != null)
                            {
                                //txtPaydays2.Text = pmm.numberDays.ToString();
                                //labelPayment2.Text = pmm.description;
                            }
                        }
                    }
                    //=========
                  
                }
                
                
            }
        }
        private void btnCreditor2_Click(object sender, EventArgs e)
        {
            DebCreLookupBUS debpers = new DebCreLookupBUS();
            List<IModel> pm1 = new List<IModel>();
            AccDebCreBUS adc = new AccDebCreBUS();
            AccDebCreModel adm = new AccDebCreModel();

            pm1 = debpers.GetCreditorsCredPay();
            var dlgSave = new GridLookupForm(pm1, "Creditor");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                if (enterb == null)
                {
                    translateRadMessageBox msg = new translateRadMessageBox();
                    msg.translateAllMessageBox("You have to press New first !!");
                    //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    //{
                    //    if (resxSet.GetString("You have to press New first !!") != null)
                    //        RadMessageBox.Show(resxSet.GetString("You have to press New first !!"));
                    //    else
                    //        RadMessageBox.Show("You have to press New first !!");
                    //}
                    return;
                }
                else
                {
                    try
                    {
                        DebCreLookupModel pm1X = new DebCreLookupModel();
                        pm1X = (DebCreLookupModel)dlgSave.selectedRow;
                        if (pm1X != null)
                        {

                            //set textbox
                            //=================================
                            if (enterb != null && isEdit2 == true)
                            {
                                if (pm1X.accNumber != null && pm1X.accNumber != enterb.accNumber)
                                {
                                   // DialogResult dr = RadMessageBox.Show("You change the client !! Do You want to change client in items?", "Change", MessageBoxButtons.YesNo);
                                    translateRadMessageBox msgbox = new translateRadMessageBox();
                                    DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("You change the client !! Do You want to change client in items?", "Change");
                                    if (dr == DialogResult.Yes)
                                    {
                                        if (lines != null)
                                            for (int i = 0; i < lines.Count; i++)
                                            {
                                                if (lines[i].idClientLine == enterb.accNumber)
                                                    lines[i].idClientLine = pm1X.accNumber;
                                            }
                                        txtClient2.Text = pm1X.accNumber;
                                        enterb.accNumber = pm1X.accNumber;
                                        labelClient2.Text = pm1X.name;

                                        adc = new AccDebCreBUS();
                                        adm = new AccDebCreModel();
                                        adm = adc.GetCustomerByAccCode(pm1X.accNumber);


                                        if (adm != null)
                                        {
                                            if (adm.payCondition != null && adm.payCondition != 0)
                                            {
                                                AccPaymentBUS pmb = new AccPaymentBUS();
                                                AccPaymentModel pmm = new AccPaymentModel();
                                                pmm = pmb.GetPaymentByID(adm.payCondition);
                                                if (pmm != null)
                                                {
                                                    txtPayDays2.Text = pmm.numberDays.ToString();
                                                    labelPayDays.Text = pmm.description;   //labelPayment2.Text
                                                }
                                            }
                                        }

                                        txtIban2.Text = "";
                                        gridItems.DataSource = null;
                                        gridItems.DataSource = lines;


                                    }
                                    else
                                    {
                                        txtClient2.Text = enterb.accNumber;
                                    }
                                }
                            }
                            else
                            {
                                //=================================
                                if (isNew2 == true)
                                {
                                    // txtClient2.Text = pm1X.accNumber;

                                    if (lines != null && lines.Count > 0)
                                    {
                                        for (int i = 0; i < lines.Count; i++)
                                        {
                                           // if (lines[i].idClientLine == txtClient2.Text)
                                                lines[i].idClientLine = pm1X.accNumber;
                                        }
                                    }
                                        txtClient2.Text = pm1X.accNumber;
                                        if (enterb != null)
                                            enterb.accNumber = pm1X.accNumber;
                                        labelClient2.Text = pm1X.name;
                                    



                                    //=== payment days
                                    adc = new AccDebCreBUS();
                                    adm = new AccDebCreModel();
                                    adm = adc.GetCustomerByAccCode(pm1X.accNumber);
                                    if (adm != null)
                                    {
                                        if (adm.payCondition != null && adm.payCondition != 0)
                                        {
                                            AccPaymentBUS pmb = new AccPaymentBUS();
                                            AccPaymentModel pmm = new AccPaymentModel();
                                            pmm = pmb.GetPaymentByID(adm.payCondition);
                                            if (pmm != null)
                                            {
                                                txtPayDays2.Text = pmm.numberDays.ToString();
                                                // labelPayment2.Text = pmm.description;
                                            }
                                            else
                                            {
                                                txtPayDays2.Value = 0;
                                            }
                                        }
                                        else
                                        {
                                            txtPayDays2.Value = 0;
                                        }
                                        txtIban2.Text = "";
                                    }
                                    else
                                    {
                                        txtPayDays2.Value = 0;
                                        txtIban2.Text = "";
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        translateRadMessageBox msg = new translateRadMessageBox();
                        msg.translateAllMessageBox("Wrong Client account number");
                      //  MessageBox.Show("Wrong Client account number");    //ex.Message
                    }
                  
                }
  
            }

            if (txtClient2.Text != "")
            {
                AccDebCreBUS debpers1 = new AccDebCreBUS();
                AccDebCreModel pm1X = new AccDebCreModel();
                string fn = "";
                string mn = "";
                string ln = "";
                pm1X = debpers1.GetCreditorName(txtClient2.Text);
                if (pm1X != null)
                {
                    if (enterb != null)
                    {
                        if (pm1X.idClient != 0)
                        {
                            ClientBUS cb = new ClientBUS();
                            ClientModel cm = new ClientModel();
                            cm = cb.GetClient(pm1X.idClient);
                            if (cm != null)
                                labelClient2.Text = cm.nameClient;
                            enterb.idClient = cm.idClient;
                        }
                        else
                        {
                            if (pm1X.idContPerson != 0)
                            {
                                PersonBUS pb = new PersonBUS();
                                PersonModel pm = new PersonModel();
                                pm = pb.GetPerson(pm1X.idContPerson);
                                if (pm != null)
                                {
                                    if (pm.firstname == null)
                                        fn = "";
                                    else
                                        fn = pm.firstname;
                                    if (pm.midname == null)
                                        mn = "";
                                    else
                                        mn = pm.midname;
                                    if (pm.lastname == null)
                                        ln = "";
                                    else
                                        ln = pm.lastname;

                                    labelClient2.Text = fn + " " + mn + " " + ln;

                                }
                                else
                                {
                                    labelClient2.Text = "";
                                }

                            }
                        }
                    }
                    else
                    {
                        translateRadMessageBox msg = new translateRadMessageBox();
                        msg.translateAllMessageBox("You have to press New first !!");

                        //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        //{
                        //    if (resxSet.GetString("You have to press New first !!") != null)
                        //        RadMessageBox.Show(resxSet.GetString("You have to press New first !!"));
                        //    else
                        //        RadMessageBox.Show("You have to press New first !!");
                        //}
                        txtClient2.Focus();
                        return;
                    }
                    
                }
            }
            else
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("Client mandatory !!");

                //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                //{
                //    if (resxSet.GetString("Client mandatory !!") != null)
                //        RadMessageBox.Show(resxSet.GetString("Client mandatory !!"));
                //    else
                //        RadMessageBox.Show("Client mandatory !!");
                //}
                txtClient2.Focus();
                return;
            }
        }
        private void btnAccount2_Click(object sender, EventArgs e)
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
             
                txtAccount2.Text = genmX3.numberLedgerAccount;
                labelAccount2.Text = genmX3.descLedgerAccount;
                if (genmX3.btwId != null && genmX3.btwId != 0)
                    txtBtw2.Text = genmX3.btwId.ToString();
               
            }
        }

        private void btnBtw2_Click(object sender, EventArgs e)
        {
            AccTaxBUS ccentar4 = new AccTaxBUS();
            List<IModel> gmX4m = new List<IModel>();

            gmX4m = ccentar4.GetAllTax(Login._user.lngUser);
            var dlgSave4 = new GridLookupForm(gmX4m, "Btw");
            if (dlgSave4.ShowDialog(this) == DialogResult.Yes)
            {
                AccTaxModel gmX4ml = new AccTaxModel();
                gmX4ml = (AccTaxModel)dlgSave4.selectedRow;
                txtBtw2.Text = gmX4ml.idTax.ToString();
                labelBtw2.Text = gmX4ml.descTax;
            }
        }

        private void btnCost2_Click(object sender, EventArgs e)
        {
            AccCostBUS ccentar = new AccCostBUS();
            List<IModel> gmX = new List<IModel>();

            gmX = ccentar.GetAllCost();
            var dlgSave = new GridLookupForm(gmX, "Cost");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                AccCostModel genmX = new AccCostModel();
                genmX = (AccCostModel)dlgSave.selectedRow;
              //==========================
                   if (enterb != null)
                {
                    if (genmX.codeCost != enterb.cost)
                    {
                        translateRadMessageBox msgbox = new translateRadMessageBox();
                        DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("You changed cost code! Do you want to update items?", "Change");
                      // DialogResult dr = RadMessageBox.Show("You changed cost code! Do you want to update items?", "Change", MessageBoxButtons.YesNo);
                       if (dr == DialogResult.Yes)
                       {
                           if (lines != null)
                           {
                               for (int q = 0; q < lines.Count; q++)
                               {
                                   if (lines[q].idCostLine == enterb.cost || lines[q].idCostLine == "")
                                       lines[q].idCostLine = genmX.codeCost;
                               }
                           }
                       }
                    }
                    gridItems.DataSource = null;
                    gridItems.DataSource = lines;
                }
                //===============================
                labelCost2.Text = genmX.descCost;
                txtCost2.Text = genmX.codeCost;
            }
        }

    
        private void btnProject2_Click(object sender, EventArgs e)
        {
            ArrangementBUS ccentar1 = new ArrangementBUS();
            List<IModel> gmX1 = new List<IModel>();
            if (Login._bookyear == "")
                Login._bookyear = DateTime.Now.Year.ToString();
            gmX1 = ccentar1.GetAllArrangementsAccount(Login._bookyear);
            var dlgSave1 = new GridLookupForm(gmX1, "Arrangement");

            if (dlgSave1.ShowDialog(this) == DialogResult.Yes)
            {
                ArrangementModel genmX1 = new ArrangementModel();
                genmX1 = (ArrangementModel)dlgSave1.selectedRow;
                //set textbox
                //======================== ako se promenio projekat
                if (enterb != null && isEdit2 == true)
                {
                    if (genmX1.codeProject.Trim() != enterb.project.Trim())
                    {
                        proj_update = false;
                        translateRadMessageBox msg = new translateRadMessageBox();
                        msg.translateAllMessageBox("You changed project code! You have to split items again !!! Press SPLIT button, please !");
                        //DialogResult dr = RadMessageBox.Show("You changed project code! You have to split items again !!! Press SPLIT button, please !", "Change", MessageBoxButtons.YesNo);

                        //if (dr == DialogResult.Yes)
                        //{
                           
                            txtProject2.Text = genmX1.codeProject;
                            fromdate = genmX1.dtFromArrangement;
                            idArange = genmX1.idArrangement;
                            labelPayment2.Text = "Start date => " + fromdate.ToShortDateString() + " - " + "end date =>  " + genmX1.dtToArrangement.ToShortDateString();

                            btnEnter.Focus();
                            //proj_update = true;
                            //btnEnter.PerformClick();
                      //  }
                            //if (lines != null)
                            //{
                            //    for (int q = 0; q < lines.Count; q++)
                            //    {
                            //        if (lines[q].idProjectLine.Trim() == enterb.project.Trim() || lines[q].idProjectLine.Trim() == "")
                            //            lines[q].idProjectLine = genmX1.codeProject;
                            //        if (lines[q].numberLedAccount == masterAccount)
                            //            lines[q].idProjectLine = genmX1.codeProject;
                            //    }
                            //}

                            //txtProject2.Text = genmX1.codeProject;
                            //fromdate = genmX1.dtFromArrangement;
                            //idArange = genmX1.idArrangement;
                            //labelPayment2.Text = "Start date => " + fromdate.ToShortDateString() + " - " + "end date =>  " + genmX1.dtToArrangement.ToShortDateString();
                            //proj_update = true;
                            //ddlOption.Focus();

                            //gridItems.DataSource = null;
                            //gridItems.DataSource = lines;

                            //if (genmX1.statusArrangement == "cxd")
                            //{
                            //    labelProject2.Text = genmX1.nameArrangement + " - CANCELED";
                            //    labelProject2.ForeColor = Color.Red;
                            //}
                            //else
                            //{
                            //    labelProject2.Text = genmX1.nameArrangement;
                            //    labelProject2.ForeColor = Color.Black;
                            //}


                        //}
                        //else
                        // {
                        //     txtProject2.Text = enterb.project;
                        // }
                    }
                    
                }
                if (isNew2 == true)
                {
                    txtProject2.Text = genmX1.codeProject;
                    fromdate = genmX1.dtFromArrangement;
                    idArange = genmX1.idArrangement;
                    labelPayment2.Text = "Start date => " + fromdate.ToShortDateString() + " - " + "end date =>  " + genmX1.dtToArrangement.ToShortDateString();
                    proj_update = true;
                    if (genmX1.statusArrangement == "cxd")
                    {
                        labelProject2.Text = genmX1.nameArrangement + " - CANCELED";
                        labelProject2.ForeColor = Color.Red;
                    }
                    else
                    {
                        labelProject2.Text = genmX1.nameArrangement;
                        labelProject2.ForeColor = Color.Black;
                    }
                }
                ddlOption.Focus();// txtProject2.Focus();
            }
        }
        #endregion buttons

      
        private void getIncopNr2()
        {
           
            List<AccDailyModel> adm = new List<AccDailyModel>();
            adm = new AccDailyBUS(Login._bookyear).GetBookingDailysInkop2();
            if (adm == null)
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("There is no Daily ... Program will close !");

                //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                //{
                //    if (resxSet.GetString("There is no Daily ... Program will close !") != null)
                //        RadMessageBox.Show(resxSet.GetString("There is no Daily ... Program will close !"));
                //    else
                //        RadMessageBox.Show("There is no Daily ... Program will close !");
                //}
                this.Close();
            }
            xDaily = adm[0].idDaily;
            AccLineBUS gn = new AccLineBUS(Login._bookyear);
            IdModel nid = new IdModel();
            int idDaily = 0;
            
            string yearId = Login._bookyear;       //DateTime.Now.Year.ToString();
            if (xDaily != -1)
                idDaily = xDaily;
            nid = gn.GetIncop(yearId, idDaily, this.Name, Login._user.idUser);
            var result = nid.idNumber.ToString().PadLeft(6, '0');
            var aa = nid.idDaily.ToString().PadRight(6, '0');
            string SubString = nid.yearId.Substring(yearId.Length - 2);
            txtInkop2.Text = SubString + aa + result;

        }
       
      
  

        #region textfields

     
        private void dpDate2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                getnames2();
                txtClient2.Focus();
                
               // dpValuta2.Focus();

            }
        }
      
        private void dpValuta2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                getnames2();
                txtClient2.Focus();

            }
        }

     
        private void txtClient2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                getnames2();
                txtInvoiceNr2.Focus();
            }
            else
            {
                if (e.KeyCode == Keys.F2)
                    btnClient2.PerformClick();   //_Click(sender, e);
            }

            
            
        }

        private void txtAccount2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                getnames2();
                txtBtw2.Focus();

            }
            else
            {
                if (e.KeyCode == Keys.F2)
                    // btnAccount2_Click(sender, e);
                    btnAccount2.PerformClick();
            }
        }

        private void txtBtw2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                getnames2();
                txtInvoiceNr2.Focus();
            }
            else
            {
                if (e.KeyCode == Keys.F2)
                    btnBtw2_Click(sender, e);
            }
        }

      
        private void txtInvoiceNr2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                getnames2();
                txtDescription2.Focus();
            }
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                getnames2();
                txtIban2.Focus();
            }
        }
        private void txtDescription2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                getnames2();
                btnIbans.Focus();
            }
        }

       
        private void txtIban2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                getnames2();
                txtAmount2Credit.Focus();
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    AccIbanBUS aib = new AccIbanBUS();
                    List<IModel> aim = new List<IModel>();
                    aim = aib.GetIBANForClientString(txtClient2.Text);

                    var dlgSave4 = new GridLookupForm(aim, "Iban");
                    if (dlgSave4.ShowDialog(this) == DialogResult.Yes)
                    {
                        AccIbanModel gmX4ml = new AccIbanModel();
                        gmX4ml = (AccIbanModel)dlgSave4.selectedRow;
                        txtIban2.Text = gmX4ml.ibanNumber.ToString();

                    }
                }

            }
        }

        private void txtAmount2Credit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
               // getnames2();
                if (Convert.ToDecimal(txtAmount2Credit.Text) != 0)
                {
                    if (enterb != null && isEdit2 == true)
                    {
                        if (Convert.ToDecimal(txtAmount2Credit.Value) != Convert.ToDecimal(enterb.amountC))
                        {
                            translateRadMessageBox msgbox = new translateRadMessageBox();
                            DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("You change the amount !! Change  amount in grid ?", "Change");
                          //  DialogResult dr = RadMessageBox.Show("You change the amount !! Change  amount in grid ?", "Change", MessageBoxButtons.YesNo);
                            if (dr == DialogResult.Yes)
                            {
                                if (lines != null)
                                {
                                    for (int w = 0; w < lines.Count; w++)
                                    {
                                        if (lines[w].numberLedAccount == defCreditor)
                                        {
                                            lines[w].creditLine = Convert.ToDecimal(txtAmount2Credit.Value);
                                            lines[w].debitLine = 0;
                                        }
                                    }
                                    enterb.amountC = Convert.ToDecimal(txtAmount2Credit.Value);
                                    gridItems.DataSource = null;
                                    gridItems.DataSource = lines;
                                }
                            }
                            else
                            {
                                txtAmount2Credit.Value = Convert.ToDecimal(enterb.amountC);
                            }
                        }
                    }
                    if (isNew2 == true && lines != null && lines.Count > 0)
                    {
                        decimal crack_credit = 0;
                        crack_credit = lines[0].creditLine;
                        if (Convert.ToDecimal(txtAmount2Credit.Text) != crack_credit)
                        {
                            translateRadMessageBox msgbox = new translateRadMessageBox();
                            DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("You change the amount !! Change  amount in grid ?", "Change");
                           // DialogResult dr = RadMessageBox.Show("You change the amount !! Grid amount will be changed !!!", "Save", MessageBoxButtons.YesNo);
                            if (dr == DialogResult.Yes)
                            {
                                if (lines != null)
                                {
                                    for (int w = 0; w < lines.Count; w++)
                                    {
                                        if (lines[w].numberLedAccount == defCreditor)
                                        {
                                            lines[w].creditLine = Convert.ToDecimal(txtAmount2Credit.Value);
                                            lines[w].debitLine = 0;
                                        }
                                    }
                                    enterb.amountC = Convert.ToDecimal(txtAmount2Credit.Value);
                                    gridItems.DataSource = null;
                                    gridItems.DataSource = lines;
                                }
                            }
                            else
                            {
                                txtAmount2Credit.Value = Convert.ToDecimal(crack_credit);
                            }
                        }
                    }

                    txtCost2.Focus();
                }
                else
                {
                    txtAmount2Debit.Focus();
                }
            }
        }
        private void txtAmount2Debit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (enterb != null && isEdit2 == true)
                {
                    if (Convert.ToDecimal(txtAmount2Debit.Value) != Convert.ToDecimal(enterb.amountD))
                    {
                        translateRadMessageBox msgbox = new translateRadMessageBox();
                        DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("You change the amount !! Change  amount in grid ?", "Change");
                      //  DialogResult dr = RadMessageBox.Show("You change the amount !! Grid amount will be changed !!!", "Save", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.Yes)
                        {
                            if (lines != null)
                            {
                                for (int w = 0; w < lines.Count; w++)
                                {
                                    if (lines[w].numberLedAccount == defCreditor)
                                    {
                                        lines[w].debitLine = Convert.ToDecimal(txtAmount2Debit.Value);
                                        lines[w].creditLine = 0;
                                    }
                                }
                                enterb.amountD = Convert.ToDecimal(txtAmount2Debit.Value);
                                gridItems.DataSource = null;
                                gridItems.DataSource = lines;
                            }
                        }
                        else
                        {
                            txtAmount2Debit.Value = Convert.ToDecimal(enterb.amountD);
                        }

                    }
                }
                if (isNew2 == true && lines != null && lines.Count > 0)
                {
                    decimal crack_debit = 0;
                    crack_debit = lines[0].debitLine;
                    if (Convert.ToDecimal(txtAmount2Debit.Text) != crack_debit)
                    {
                        translateRadMessageBox msgbox = new translateRadMessageBox();
                        DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("You change the amount !! Change  amount in grid ?", "Change");
                      //  DialogResult dr = RadMessageBox.Show("You change the amount !! Grid amount will be changed !!!", "Save", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.Yes)
                        {
                            if (lines != null)
                            {
                                for (int w = 0; w < lines.Count; w++)
                                {
                                    if (lines[w].numberLedAccount == defCreditor)
                                    {
                                        lines[w].debitLine = Convert.ToDecimal(txtAmount2Debit.Value);
                                        lines[w].creditLine = 0;
                                    }
                                }
                                enterb.amountC = Convert.ToDecimal(txtAmount2Debit.Value);
                                gridItems.DataSource = null;
                                gridItems.DataSource = lines;
                            }
                        }
                        else
                        {
                            txtAmount2Debit.Value = Convert.ToDecimal(crack_debit);
                        }
                    }
                }



                txtCost2.Focus();
            }
        }
     
        private void ddlCurr2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                getnames2();
                txtCost2.Focus();
            }
        }
        private void txtCost2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                getnames2();
                txtProject2.Focus();
            }
            else
            {
                if (e.KeyCode == Keys.F2)
                    btnCost2_Click(sender, e);
            }
        }

      
        private void txtProject2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                getnames2();
                ddlOption.Focus();
            }
            else
            {
                if (e.KeyCode == Keys.F2)
                    btnProject2.PerformClick();
            }
        }
        private void ddlOption_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
              txtPayDays2.Focus();
               // btnSave2.Focus();
            }
           
        }
        private void txtPaydays2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                btnEnter.Focus();

            }
        }


        #endregion textfields

        private void radPagesPay_SelectedPageChanged(object sender, EventArgs e)
        {
            RadPageView rpv = (RadPageView)sender;
            string sName = ((RadPageView)sender).SelectedPage.Name;

            switch (sName)
            {
    
                case "rpvEnterBook":

                    EnterBookKeeper();

                    break;


       
                case "rpvBook":

                    bus = new AccCreditPayBUS();
                    listmBook = new List<AccCreditPayModel>();

                    listmBook = bus.GetAllPaysApproved();
                    gridBook.DataSource = null;
                    gridBook.DataSource = listmBook;

                    break;

                case "rpvCredit":

                    break;

            }
        }
    
        #region Booking
        private void gridBook_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if (gridBook != null)
            {
                if (gridBook.Columns.Count > 0)
                {
                    for (int i = 0; i < gridBook.Columns.Count; i++)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (gridBook.Columns[i].HeaderText != null && resxSet.GetString(gridBook.Columns[i].HeaderText) != null)
                                gridBook.Columns[i].HeaderText = resxSet.GetString(gridBook.Columns[i].HeaderText);
                        }
                    }

                    gridBook.Columns["idCreditPay"].IsVisible = false;
                    gridBook.Columns["idClient"].IsVisible = false;
                    gridBook.Columns["idContPers"].IsVisible = false;
                    gridBook.Columns["isApproved"].IsVisible = false;
                    gridBook.Columns["isBooked"].IsVisible = false;
                    gridBook.Columns["isSent"].IsVisible = false;
                    gridBook.Columns["dtSent"].IsVisible = false;
                    gridBook.Columns["namefile"].IsVisible = false;
                    gridBook.Columns["approvedUser"].IsVisible = false;
                    gridBook.Columns["createUser"].IsVisible = false;
                    gridBook.Columns["dtCreation"].IsVisible = false;
                    gridBook.Columns["payIban"].IsVisible = false;

                    gridBook.Columns["isSelected"].IsVisible = true;
                    gridBook.Columns["isSelected"].ReadOnly = false;

                    gridBook.Columns["dtItem"].ReadOnly = true;
                    gridBook.Columns["dtValuta"].ReadOnly = true;
                    gridBook.Columns["accNumber"].ReadOnly = true;
                    gridBook.Columns["account"].ReadOnly = true;
                    gridBook.Columns["invoiceNr"].ReadOnly = true;
                    gridBook.Columns["inkopNr"].ReadOnly = true;
                    gridBook.Columns["iban"].ReadOnly = true;
                    gridBook.Columns["descItem"].ReadOnly = true;
                    gridBook.Columns["amountC"].ReadOnly = true;
                    gridBook.Columns["amountD"].ReadOnly = true;
                    //   gridBook.Columns["idBtw"].ReadOnly = true;
                    gridBook.Columns["currency"].ReadOnly = true;
                    gridBook.Columns["amountInCurr"].ReadOnly = true;
                    gridBook.Columns["cost"].ReadOnly = true;
                    gridBook.Columns["project"].ReadOnly = true;



                    gridBook.Columns["dtItem"].FormatString = "{0: dd/MM/yyyy}";
                    gridBook.Columns["dtValuta"].FormatString = "{0: dd/MM/yyyy}";

                }
            }

            if (File.Exists(layoutBook))
            {
                gridBook.LoadLayout(layoutBook);
            }
        }

        private void gridBook_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
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
        }
        private void SaveLayoutC(object sender, EventArgs e)
        {
            if (File.Exists(layoutBook))
            {
                File.Delete(layoutBook);
            }
            gridBook.SaveLayout(layoutBook);

            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully save layout!");

            //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            //{
            //    if (resxSet.GetString("You have successfully save layout!") != null)
            //        RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
            //    else
            //        RadMessageBox.Show("You have successfully save layout!");
            //}
        }

        private void btnDaily_Click(object sender, EventArgs e)
        {
            AccDailyBUS acd = new AccDailyBUS(Login._bookyear);
            List<IModel> acm = new List<IModel>();


            acm = acd.GetBookingDailysInkop();
            var dlgSave = new GridLookupForm(acm, "Daily");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                AccDailyModel genmX = new AccDailyModel();
                genmX = (AccDailyModel)dlgSave.selectedRow;

                if (genmX != null)
                {
                    //set textbox
                    if (genmX.codeDaily != null)
                    {
                        txtDaily.Text = genmX.codeDaily + "   " + genmX.descDaily;
                        xDaily = genmX.idDaily;
                        xConto = genmX.numberLedgerAccount;
                        
                    }
                }
                else
                {
                    translateRadMessageBox msg = new translateRadMessageBox();
                    msg.translateAllMessageBox("Can't booking without Inkoop book !!!");

                    //RadMessageBox.Show("Can't booking without Inkoop book !!!");
                    btnBooking.Enabled = false;
                }
            }
        }

        private void btnBooking_Click(object sender, EventArgs e)
        {
            int noforbooking = 0;
            List<AccDailyModel> adm = new List<AccDailyModel>();
            adm = new AccDailyBUS(Login._bookyear).GetBookingDailysInkop2();
            if (adm != null)
            {
                xDaily = adm[0].idDaily;
                xConto = adm[0].numberLedgerAccount;
            }
           // if (xDaily == 0 && xDaily == null)
            if (txtDaily.Text == "")
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("Can't booking without Inkoop book !!!");
              //  RadMessageBox.Show("Can't booking without Inkoop book !!!");
                return;
            }
            else
            {
                for (int i = 0; i < listmBook.Count; i++)
                {
                    if (listmBook[i].isSelected == true)
                    {
                        noforbooking++;
                    }
                }
                if (noforbooking == 0)
                {
                    translateRadMessageBox msg = new translateRadMessageBox();
                    msg.translateAllMessageBox("No lines for booking");

                    //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    //{
                    //    if (resxSet.GetString("No lines for booking") != null)
                    //        RadMessageBox.Show(resxSet.GetString("No lines for booking"));
                    //    else
                    //        RadMessageBox.Show("No lines for booking");
                    //}
                }
                else
                {
                    translateRadMessageBox msgbox = new translateRadMessageBox();
                    DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("Booking lines ?", "Booking");
                   // DialogResult dr = RadMessageBox.Show("Booking lines ?", "Booking", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {

                        for (int i = 0; i < listmBook.Count; i++)
                        {
                            if (listmBook[i].isSelected == true)
                            {
                                dobooking(listmBook[i]);
                                if (notBooked == false)
                                {
                                    listmBook[i].isBooked = true;
                                    AccCreditPayBUS payb = new AccCreditPayBUS();
                                    payb.Update(listmBook[i], this.Name, Login._user.idUser);
                                }
                            }

                        }
                        translateRadMessageBox msg = new translateRadMessageBox();
                        msg.translateAllMessageBox("Finished");
                        //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        //{
                        //    if (resxSet.GetString("Finished") != null)
                        //        RadMessageBox.Show(resxSet.GetString("Finished"));
                        //    else
                        //        RadMessageBox.Show("Finished");
                        //}
                        bus = new AccCreditPayBUS();
                        listmBook = new List<AccCreditPayModel>();

                        listmBook = bus.GetAllPaysApproved();
                        gridBook.DataSource = null;
                        gridBook.DataSource = listmBook;
                        gridItems.DataSource = null;
                        clearform2();
                    }
                }
            }


            //=============== ako ima slogova \Crveno =====================
            bus = new AccCreditPayBUS();
            listmBook = bus.GetAllPaysApproved();
            if (listmBook != null && listmBook.Count > 0)
            {
                this.rpvBook.Item.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                this.rpvBook.Item.ForeColor = System.Drawing.Color.Black;
            }

             adm = new List<AccDailyModel>();
            adm = new AccDailyBUS(Login._bookyear).GetBookingDailysInkop2();
            if (adm != null)
            {
                xDaily = adm[0].idDaily;
                xConto = adm[0].numberLedgerAccount;
                txtDaily.Text = adm[0].codeDaily + "   " + adm[0].descDaily;
            }
            //==========================================================
             
        }

        private void gridBook_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridSummaryCellElement)
            {
                if (!String.IsNullOrEmpty(e.CellElement.Text))
                {
                    e.CellElement.BorderLeftWidth = e.CellElement.BorderRightWidth = 1;
                    e.CellElement.Font = new Font("Verdana", 8, FontStyle.Bold);
                    e.CellElement.TextAlignment = ContentAlignment.MiddleRight;

                }
                else
                {
                    e.CellElement.ResetValue(LightVisualElement.BorderLeftWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
                    e.CellElement.ResetValue(LightVisualElement.BorderRightWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
                }
            }
        }

        private void dobooking(AccCreditPayModel listB)
        {
            DateTime date_invoice = new DateTime();

            AccSettingsBUS acbs = new AccSettingsBUS();
            AccSettingsModel acbm = new AccSettingsModel();
            acbm = acbs.GetSettingsByID(Login._bookyear);
            if (acbm != null)
            {
                acc_reservationAcc = acbm.defReservationAcc;
            }
            else
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("No account for creditor reservation !!!");

                //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                //{
                //    if (resxSet.GetString("No account for creditor reservation !!!") != null)
                //        RadMessageBox.Show(resxSet.GetString("No account for creditor reservation  !!!"));
                //    else
                //        RadMessageBox.Show("No account for creditor reservation  !!!");
                //}
                notBooked = true;
                return;
            }

            AccCreditPayModel lista = new AccCreditPayModel();
            AccLineBUS alb = new AccLineBUS(Login._bookyear);
            AccOpenLinesBUS opl = new AccOpenLinesBUS();
            AccAcountUpdate au = new AccAcountUpdate();
            lista = listB;
            AccCreditLineBUS aclb = new AccCreditLineBUS();
            List<AccLineModel> acm = new List<AccLineModel>();
            acm = aclb.GetLine(lista.idCreditPay);                  //uzima razbijene stavke za odredjenu fakturu iz AccCreditPay (stavke AccCreditLine)
            if (acm == null)
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("No items to book !!!");

                //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                //{
                //    if (resxSet.GetString("No items to book !!!") != null)
                //        RadMessageBox.Show(resxSet.GetString("No items to book !!!"));
                //    else
                //        RadMessageBox.Show("No items to book !!!");
                //}
                notBooked = true;
                return;

               
            }

            decimal sumdebit = 0;
            decimal sumcredit = 0;

            if (acm != null && acm.Count > 0)
            {
                for (int r=0; r < acm.Count ; r++)
                {
                    sumdebit = sumdebit + Convert.ToDecimal(acm[r].debitLine);
                    sumcredit = sumcredit + Convert.ToDecimal(acm[r].creditLine);
                }

            }
            decimal tot = sumdebit - sumcredit;
            if (tot != 0)
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("Not in balance !!!");

                // using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                //{
                //    if (resxSet.GetString("Not in balance !!!") != null)
                //        RadMessageBox.Show(resxSet.GetString("Not in balance !!!"));
                //    else
                //        RadMessageBox.Show("Not in balance !!!");
                //}
                notBooked = true;
                return;
            }
            int booksort = 1;
            bool isOk = false;
            for (int i = 0; i < acm.Count; i++)
            {
                accline = new AccLineModel();

                if (acm[i].numberLedAccount == xConto)  // glavni konto za izabranu knjigu ... preko ovoga trazimo osnovnu stavku
                {
                    accline.idAccDaily = xDaily;
                    accline.booksort = booksort;            //osovna stavka broj 1
                    accline.creditLine = Convert.ToDecimal(acm[i].creditLine);
                    accline.debitLine = Convert.ToDecimal(acm[i].debitLine);
                    accline.descLine = acm[i].descLine;
                    accline.dtLine = Convert.ToDateTime(acm[i].dtLine);
                    date_invoice = Convert.ToDateTime(acm[i].dtLine);     // sluzi za uporedjivanje da li pravi split staku ili ne
                    accline.periodLine = au.Period(acm[i].dtLine);
                    accline.numberLedAccount = xConto;
                    accline.idClientLine = acm[i].idClientLine;
                    accline.idBTW = acm[i].idBTW;
                    accline.idCostLine = acm[i].idCostLine;
                    accline.idProjectLine = acm[i].idProjectLine;
                    accline.invoiceNr = acm[i].invoiceNr;
                    accline.incopNr = acm[i].incopNr;
                    accline.iban = acm[i].iban;
                    accline.dtBooking = acm[i].dtBooking;
                    accline.bookingYear = Login._bookyear;
                    accline.idMaster = acm[i].idMaster;
                    accline.idDetail = acm[i].idDetail;
                    accline.statusLine = true;


                    isOk = alb.Save(accline, this.Name, Login._user.idUser);          // save osnovne stavke
                    if (isOk == false)
                    {
                        translateRadMessageBox msg = new translateRadMessageBox();
                        msg.translateAllMessageBox("Error booking line");

                        //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        //{
                        //    if (resxSet.GetString("Error booking line") != null)
                        //        RadMessageBox.Show(resxSet.GetString("Error booking line"));
                        //    else
                        //        RadMessageBox.Show("Error booking line");
                        //}
                        notBooked = true;

                        return;
                    }
                    isOk = au.AddAmount(accline, this.Name, Login._user.idUser);    // ovde sabira iznose po kontima
                    if (isOk == false)
                    {
                        translateRadMessageBox msg = new translateRadMessageBox();
                        msg.translateAllMessageBox("Error updating Ledger accounts");

                        //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        //{
                        //    if (resxSet.GetString("Error updating Ledger accounts") != null)
                        //        RadMessageBox.Show(resxSet.GetString("Error updating Ledger accounts"));
                        //    else
                        //        RadMessageBox.Show("Error updating Ledger accounts");
                        //}
                        notBooked = true;
                    }
                    // ovde pravi otvorenu stavku
                    AccCreditLinePayBUS acpb = new AccCreditLinePayBUS();
                    List<AccCreditLinePayModel> acpm = new List<AccCreditLinePayModel>();
                    acpm = acpb.GetAllLinesByCreditor(accline.idClientLine, accline.invoiceNr);
                    if (acpm == null)
                    {

                        AccOpenLinesBUS opb = new AccOpenLinesBUS();
                        AccOpenLinesModel opline = new AccOpenLinesModel();
                        DateTime valutaPay = new DateTime();
                        if (lista.paydays != 0)
                            valutaPay = Convert.ToDateTime(acm[i].dtLine.AddDays(lista.paydays));
                        opline.idDebCre = acm[i].idClientLine;
                        opline.dtOpenLine = Convert.ToDateTime(valutaPay);                            //Convert.ToDateTime(acm[i].dtBooking);
                        opline.dtCreationLine = Convert.ToDateTime(acm[i].dtLine);
                        opline.descOpenLine = acm[i].descLine;
                        opline.creditOpenLine = acm[i].creditLine;
                        opline.debitOpenLine = acm[i].debitLine;
                        if (acm[i].creditLine > 0)
                            opline.typeOpenLine = "C";
                        else
                            if (acm[i].debitLine > 0)
                                opline.typeOpenLine = "D";
                        opline.periodOnenLines = au.Period(Convert.ToDateTime(acm[i].dtLine));
                        opline.creditDays = lista.paydays;
                        opline.referencePay = acm[i].incopNr;
                        opline.invoiceOpenLine = acm[i].invoiceNr;
                        opline.idOption = lista.idOption;
                        opline.dtPayOpenLine = Convert.ToDateTime("1900-01-01");
                        opline.account = acm[i].numberLedAccount;
                        opline.codeCost = acm[i].idCostLine;
                        opline.codeArr = acm[i].idProjectLine;
                        opline.idProject = acm[i].idProjectLine;
                        opline.iban = acm[i].iban;
                        opline.bookingYear = Login._bookyear;



                        isOk = opb.Save(opline, this.Name, Login._user.idUser);
                        if (isOk == false)
                        {

                            translateRadMessageBox msg = new translateRadMessageBox();
                            msg.translateAllMessageBox("Error writing open line");

                            //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            //{
                            //    if (resxSet.GetString("Error writing open line") != null)
                            //        RadMessageBox.Show(resxSet.GetString("Error writing open line"));
                            //    else
                            //        RadMessageBox.Show("Error writing open line");
                            //}
                            notBooked = true;
                        }
                    }
                    else
                    {
                        for (int q = 0; q < acpm.Count; q++)
                        {
                            AccOpenLinesBUS opb = new AccOpenLinesBUS();
                            AccOpenLinesModel opline = new AccOpenLinesModel();
                            
                            opline.idDebCre = acpm[q].accNumber;
                            opline.dtOpenLine = Convert.ToDateTime(acpm[q].dtDate);                            //Convert.ToDateTime(acm[i].dtBooking);
                            opline.descOpenLine = acm[i].descLine;
                            opline.term = Convert.ToInt32(acpm[q].term);
                            //opline.creditOpenLine = acpm[q].amount;
                            //opline.debitOpenLine = acm[i].debitLine;
                            if (acm[i].creditLine > 0)
                            {
                                opline.typeOpenLine = "C";
                                opline.creditOpenLine = acpm[q].amount;
                            }
                            else
                                if (acm[i].debitLine > 0)
                                {
                                    opline.typeOpenLine = "D";
                                    opline.debitOpenLine = acpm[q].amount;
                                }
                            opline.periodOnenLines = au.Period(Convert.ToDateTime(acpm[q].dtDate));
                            opline.creditDays = lista.paydays;
                            opline.referencePay = accline.incopNr;
                            opline.invoiceOpenLine = acpm[q].invoiceNr;
                            opline.idOption = lista.idOption;
                            opline.dtPayOpenLine = Convert.ToDateTime("1900-01-01");
                            opline.account = accline.numberLedAccount;
                            opline.codeCost = accline.idCostLine;
                            opline.codeArr = accline.idProjectLine;
                            opline.idProject = accline.idProjectLine;
                            opline.iban = accline.iban;
                            opline.bookingYear = Login._bookyear;
                            opline.dtCreationLine = accline.dtLine;

                            isOk = opb.Save(opline, this.Name, Login._user.idUser);
                            if (isOk == false)
                            {
                                translateRadMessageBox msg = new translateRadMessageBox();
                                msg.translateAllMessageBox("Error writing open line");

                                //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                //{
                                //    if (resxSet.GetString("Error writing open line") != null)
                                //        RadMessageBox.Show(resxSet.GetString("Error writing open line"));
                                //    else
                                //        RadMessageBox.Show("Error writing open line");
                                //}
                                notBooked = true;
                            }
                        }
                    }



                }
                else                 // otale stavke iz AccCreditLine
                {
                    booksort = booksort + 1;    // inkrement stavke
                    //if (acm[i].dtLine != lista.dtItem)   // ovde treba da razbije stavku na dve 1610 konto 
                    //{
                        accline.idAccDaily = xDaily;
                        accline.booksort = booksort;
                        //if (Convert.ToDecimal(acm[i].creditLine) != 0)
                              accline.creditLine = Convert.ToDecimal(acm[i].creditLine);
                        //else
                              accline.debitLine = Convert.ToDecimal(acm[i].debitLine);
                       // accline.debitLine = 0; // Convert.ToDecimal(acm[i].debitLine);
                        accline.descLine = acm[i].descLine;
                        accline.dtLine = Convert.ToDateTime(acm[i].dtLine);   //Convert.ToDateTime(lista.dtItem);
                        accline.periodLine = au.Period(Convert.ToDateTime(acm[i].dtLine));                                                 //acm[i].dtLine);
                        accline.numberLedAccount = acm[i].numberLedAccount; //reserve_acc;
                        accline.idClientLine = acm[i].idClientLine;
                        accline.idBTW = acm[i].idBTW;
                        accline.idCostLine = acm[i].idCostLine;
                        accline.idProjectLine = acm[i].idProjectLine;
                        accline.invoiceNr = acm[i].invoiceNr;
                        accline.incopNr = acm[i].incopNr;
                        accline.dtBooking = acm[i].dtBooking;
                        accline.bookingYear = Login._bookyear;
                        accline.idMaster = acm[i].idMaster;
                        accline.idDetail = acm[i].idDetail;
                        accline.statusLine = true;

                        isOk = alb.Save(accline, this.Name, Login._user.idUser);
                        isOk = au.AddAmount(accline, this.Name, Login._user.idUser);

                        booksort = booksort + 1;

                        if (acm[i].dtLine.ToShortDateString() != Convert.ToDateTime(lista.dtItem).ToShortDateString())   // ovde treba da razbije stavku na dve 1610 konto 
                        {
                            accline = new AccLineModel();

                            accline.idAccDaily = xDaily;
                            accline.booksort = booksort;
                            //if (Convert.ToDecimal(acm[i].creditLine) != 0)
                            if (Convert.ToDecimal(acm[i].debitLine) > 0)
                                accline.creditLine = Convert.ToDecimal(acm[i].debitLine);
                            else
                                accline.debitLine = Convert.ToDecimal(acm[i].creditLine);
                            // accline.debitLine = 0; // Convert.ToDecimal(acm[i].debitLine);
                            accline.descLine = acm[i].descLine;
                            accline.dtLine = Convert.ToDateTime(acm[i].dtLine);   //Convert.ToDateTime(lista.dtItem);
                            accline.periodLine = au.Period(Convert.ToDateTime(acm[i].dtLine));                                                 //acm[i].dtLine);
                            accline.numberLedAccount = acc_reservationAcc; //acm[i].numberLedAccount; //reserve_acc;
                            accline.idClientLine = acm[i].idClientLine;
                            accline.idBTW = acm[i].idBTW;
                            accline.idCostLine = acm[i].idCostLine;
                            accline.idProjectLine = acm[i].idProjectLine;
                            accline.invoiceNr = acm[i].invoiceNr;
                            accline.incopNr = acm[i].incopNr;
                            accline.dtBooking = acm[i].dtBooking;
                            accline.bookingYear = Login._bookyear;
                            accline.idMaster = acm[i].idMaster;
                            accline.idDetail = acm[i].idDetail;
                            accline.statusLine = true;

                            isOk = alb.Save(accline, this.Name, Login._user.idUser);
                            isOk = au.AddAmount(accline, this.Name, Login._user.idUser);

                            booksort = booksort + 1;
                            //================== druga stavka

                            accline = new AccLineModel();

                            accline.idAccDaily = xDaily;
                            accline.booksort = booksort;
                            if (Convert.ToDecimal(acm[i].debitLine) > 0)
                                accline.debitLine = Convert.ToDecimal(acm[i].debitLine);
                            else
                                accline.creditLine = Convert.ToDecimal(acm[i].creditLine);
                            // accline.debitLine = 0; // Convert.ToDecimal(acm[i].debitLine);
                            accline.descLine = acm[i].descLine;

                            accline.dtLine = Convert.ToDateTime(lista.dtItem); // Convert.ToDateTime(acm[i].dtLine);   //Convert.ToDateTime(lista.dtItem);
                            accline.periodLine = au.Period(Convert.ToDateTime(acm[i].dtLine));                                                 //acm[i].dtLine);
                            accline.numberLedAccount = acc_reservationAcc; //acm[i].numberLedAccount; //reserve_acc;
                            accline.idClientLine = acm[i].idClientLine;
                            accline.idBTW = acm[i].idBTW;
                            accline.idCostLine = acm[i].idCostLine;
                            accline.idProjectLine = acm[i].idProjectLine;
                            accline.invoiceNr = acm[i].invoiceNr;
                            accline.incopNr = acm[i].incopNr;
                            accline.dtBooking = acm[i].dtBooking;
                            accline.bookingYear = Login._bookyear;
                            accline.idMaster = acm[i].idMaster;
                            accline.idDetail = acm[i].idDetail;
                            accline.statusLine = true;


                            isOk = alb.Save(accline, this.Name, Login._user.idUser);
                            isOk = au.AddAmount(accline, this.Name, Login._user.idUser);

                            booksort = booksort + 1;

                        }
                        booksort = booksort + 1;
                        
                
                    //if (isOk == false)
                    //{
                    //    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    //    {
                    //        if (resxSet.GetString("Error booking line") != null)
                    //            RadMessageBox.Show(resxSet.GetString("Error booking line"));
                    //        else
                    //            RadMessageBox.Show("Error booking line");
                    //    }
                    //    notBooked = true;
                    //    return;
                    //}
                    //isOk = au.AddAmount(accline);    // ovde sabira iznose po kontima
                    //if (isOk == false)
                    //{
                    //    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    //    {
                    //        if (resxSet.GetString("Error updating Ledger accounts") != null)
                    //            RadMessageBox.Show(resxSet.GetString("Error updating Ledger accounts"));
                    //        else
                    //            RadMessageBox.Show("Error updating Ledger accounts");
                    //    }
                    //}
                }
            }
         
           
        }

        #endregion Booking

  
        private void OpenDocument(string sDest, string sFileName)
        {
            string sExtention = sFileName.Split('.')[sFileName.Split('.').Length - 1];
            string sFullName = sDest + sFileName;
            System.Diagnostics.Process.Start(sFullName);
        }

       

        # region EnterBook

        private void EnterBookKeeper()
        {
            bus = new AccCreditPayBUS();
            listEB = new List<AccCreditPayModel>();
            isEdit2 = true;
            btnSave2.Text = "Update";
            isNew2 = false;

            listEB = bus.GetAllPays();
            if (listEB == null )
            {
                isEdit2 = false;
                btnSave2.Text = "Save";
                isNew2 = true;
            }
            gridBookPay.DataSource = null;
            gridBookPay.DataSource = listEB;
            if (gridBookPay.DataSource == null)
                gridItems.DataSource = null;
            gridBookPay.Show();

             //DialogResult dr = RadMessageBox.Show("Add New ?", "Add", MessageBoxButtons.YesNo);
             //if (dr == DialogResult.Yes)
             //{
             //    isNew2 = true;
             //    isEdit2 = false;
             //    btnNew2.PerformClick();
             //    dpDate2.Focus();
             //}
             //else
             //{
             //    isEdit2 = true;
             //    isNew2 = false;
             //}
        }
        private void gridBookPay_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < gridBookPay.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (gridBookPay.Columns[i].HeaderText != null && resxSet.GetString(gridBookPay.Columns[i].HeaderText) != null)
                        gridBookPay.Columns[i].HeaderText = resxSet.GetString(gridBookPay.Columns[i].HeaderText);
                }
            }
            if (gridBookPay.ColumnCount > 0)
            {

                gridBookPay.Columns["idCreditPay"].IsVisible = false;
                gridBookPay.Columns["idClient"].IsVisible = false;
                gridBookPay.Columns["idContPers"].IsVisible = false;
                gridBookPay.Columns["isApproved"].IsVisible = false;
                gridBookPay.Columns["isBooked"].IsVisible = false;
                gridBookPay.Columns["isSent"].IsVisible = false;
                gridBookPay.Columns["dtSent"].IsVisible = false;
                gridBookPay.Columns["namefile"].IsVisible = false;
                gridBookPay.Columns["approvedUser"].IsVisible = false;
                gridBookPay.Columns["createUser"].IsVisible = false;
                gridBookPay.Columns["dtCreation"].IsVisible = false;
                gridBookPay.Columns["payIban"].IsVisible = false;
                gridBookPay.Columns["isSelected"].IsVisible = false;
                gridBookPay.Columns["idDocument"].IsVisible = false;

                gridBookPay.Columns["inkopNr"].IsVisible = false;
                gridBookPay.Columns["iban"].IsVisible = false;
                gridBookPay.Columns["account"].IsVisible = false;


                gridBookPay.Columns["dtItem"].Width = 90;
                gridBookPay.Columns["dtValuta"].Width = 90;
                gridBookPay.Columns["accNumber"].Width = 60;
                gridBookPay.Columns["invoiceNr"].Width = 90;
                gridBookPay.Columns["descItem"].Width =120;
                gridBookPay.Columns["amountC"].Width = 70;
                gridBookPay.Columns["amountD"].Width = 70;
                gridBookPay.Columns["idBtw"].Width = 50;
                gridBookPay.Columns["project"].Width = 80;

            }
            if (File.Exists(layoutEnterBook))
            {
                gridBookPay.LoadLayout(layoutEnterBook);
            }
            if (gridBookPay.Columns != null && gridBookPay.Columns.Count > 0)
                gridBookPay.Columns["dtItem"].FormatString = "{0: dd/MM/yyyy}";
            if (gridBookPay.Columns != null && gridBookPay.Columns.Count > 0)
                gridBookPay.Columns["dtValuta"].FormatString = "{0: dd/MM/yyyy}";

            if (gridBookPay != null)
                if (gridBookPay.RowCount > 0)
                    if (gridBookPay.SelectedRows != null)
                        if (gridBookPay.SelectedRows.Count > 0)
                        {

                            AccCreditPayModel selectedRowGrid = (AccCreditPayModel)gridBookPay.SelectedRows[0].DataBoundItem;
                            enterb = new AccCreditPayModel();
                            if (selectedRowGrid != null)
                            {
                                enterb = selectedRowGrid;
                                iD = 0;
                                iD = selectedRowGrid.idCreditPay;
                                iDcredit = selectedRowGrid.idCreditPay;
                                fillform2(enterb);

                                lines = new AccCreditLineBUS().GetLine(iD);
                                gridItems.DataSource = null;
                                gridItems.DataSource = lines;
                               // isRowChange = false;
                                gridItems.Show();
                            }
                        }

        }
        private void gridBookPay_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {

            string saveLayout = "Save Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem = new RadMenuItem();
            customMenuItem.Text = saveLayout;
            customMenuItem.Click += SaveLayoutE;
            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);
        }
        private void SaveLayoutE(object sender, EventArgs e)
        {
            if (File.Exists(layoutEnterBook))
            {
                File.Delete(layoutEnterBook);
            }
            gridBookPay.SaveLayout(layoutEnter);

            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully save layout!");

            //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            //{
            //    if (resxSet.GetString("You have successfully save layout!") != null)
            //        RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
            //    else
            //        RadMessageBox.Show("You have successfully save layout!");
            //}
        }

        private void gridBookPay_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridSummaryCellElement)
            {
                if (!String.IsNullOrEmpty(e.CellElement.Text))
                {
                    e.CellElement.BorderLeftWidth = e.CellElement.BorderRightWidth = 1;
                    e.CellElement.Font = new Font("Verdana", 8, FontStyle.Bold);
                    e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                }
                else
                {
                    e.CellElement.ResetValue(LightVisualElement.BorderLeftWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
                    e.CellElement.ResetValue(LightVisualElement.BorderRightWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
                }
            }
        }

        private void gridBookPay_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            //isEdit2 = true;
            isNew2 = false;
            if (isNew2 == true)
                btnSave2.Text = "Save";
            if (isEdit2 == true)
                btnSave2.Text = "Update";
        }

        private void gridBookPay_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (gridBookPay.CurrentRow != null)
            {


                GridViewRowInfo info = this.gridBookPay.CurrentRow;
                selectedRowGrid = (AccCreditPayModel)info.DataBoundItem; //AccCreditPayModel
                enterb = new AccCreditPayModel();
                if (selectedRowGrid != null)
                {
                    enterb = selectedRowGrid;
                    iD = 0;
                    iD = selectedRowGrid.idCreditPay;
                    iDcredit = selectedRowGrid.idCreditPay;
                    fillform2(enterb);

                    lines = new AccCreditLineBUS().GetLine(iD);
                    gridItems.ReadOnly = false;
                    gridItems.DataSource = null;
                    gridItems.DataSource = lines;
                    
                    gridItems.Show();
                }
            }
        }

        private void gridItems_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < gridItems.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (gridItems.Columns[i].HeaderText != null && resxSet.GetString(gridItems.Columns[i].HeaderText) != null)
                        gridItems.Columns[i].HeaderText = resxSet.GetString(gridItems.Columns[i].HeaderText);
                }
            }
            if (File.Exists(layoutLineBook))
            {
                gridItems.LoadLayout(layoutLineBook);
            }

            if (gridItems!=null)
            if (gridItems.ColumnCount > 0)
            {
                if (gridItems.RowCount > 0)
                {
                    for(int i = 0;i<gridItems.RowCount;i++)
                    {
                        AccLineModel am = new AccLineModel();
                        am = (AccLineModel)gridItems.Rows[i].DataBoundItem;
                        if(am!=null)
                        {
                            if(am.numberLedAccount==masterAccount)
                            {
                                for (int j = 0; j < gridItems.ColumnCount; j++)
                                {
                                        gridItems.Rows[i].Cells[j].ReadOnly = true;
                                }
                            }
                            if(am.idDetail!="")
                                for (int j = 0; j < gridItems.ColumnCount; j++)
                                {
                                    if(gridItems.Columns[j].Name!="descLine")
                                    gridItems.Rows[i].Cells[j].ReadOnly = true;
                                }
                            if(am.idBTW!=0)
                            {
                                decimal percent = getPercent(Convert.ToInt32(am.idBTW));
                                if (inc_excl == 2)
                                {
                                    gridItems.Rows[i].Cells["debitBTW"].Value = Math.Abs(Convert.ToDecimal(gridItems.Rows[i].Cells["debitLine"].Value) - Convert.ToDecimal(gridItems.Rows[i].Cells["creditLine"].Value)) * percent / 100;
                                }
                                else
                                {
                                    decimal aa = 0;
                                    decimal amount=0;
                                    decimal btw_amount=0;
                                    if (inc_excl == 1)
                                    {
                                        //if (Convert.ToDecimal(gridItems.Rows[i].Cells["debitLine"].Value) > 0)
                                        //    aa = Convert.ToDecimal(gridItems.Rows[i].Cells["debitLine"].Value);
                                        //else
                                        //    aa = Convert.ToDecimal(gridItems.Rows[i].Cells["creditLine"].Value);

                                       
                                        //btw_amount = aa - (aa / (1 + percent / 100));
                                        //amount = amount - btw_amount;//aa / (1 + percent / 100);
                                        //gridItems.Rows[i].Cells["debitBTW"].Value = btw_amount;
                                        //if (Convert.ToDecimal(txtAmount2Credit.Text) > 0)
                                        //    gridItems.Rows[i].Cells["debitLine"].Value = amount;
                                        //else
                                        //    gridItems.Rows[i].Cells["creditLine"].Value = amount;
                                    }
                                }
                            }
                            else
                            {
                            //    gridItems.Rows[i].Cells["debitBTW"].Value = 0;
                            }

                        }
                    }
                }
          
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

                gridItems.Columns["idMaster"].IsVisible = false;
                gridItems.Columns["idDetail"].IsVisible = false;
                gridItems.Columns["idClientLine"].IsVisible = false;
                gridItems.Columns["incopNr"].IsVisible = false;
                gridItems.Columns["iban"].IsVisible = false;
                gridItems.Columns["dtBooking"].IsVisible = false;
            }
        }

        private void gridBookPay_GotFocus(object sender, EventArgs e)
        {
                isEdit2 = true;
                isNew2 = false;
                if (isNew2 == true)
                    btnSave2.Text = "Save";
                if (isEdit2 == true)
                    btnSave2.Text = "Update";
            
        }
        private void gridItems_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
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
            if (File.Exists(layoutLineBook))
            {
                File.Delete(layoutLineBook);
            }
            gridItems.SaveLayout(layoutLineBook);

            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully save layout!");

            //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            //{
            //    if (resxSet.GetString("You have successfully save layout!") != null)
            //        RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
            //    else
            //        RadMessageBox.Show("You have successfully save layout!");
            //}
        }
        private void SaveLayoutEB1(object sender, EventArgs e)
        {
            if (File.Exists(layoutLineBook))
            {
                File.Delete(layoutLineBook);
            }
          //  gridItems.SaveLayout(layoutLineBook);
            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully delete layout!");

            //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            //{
            //    if (resxSet.GetString("You have successfully delete layout!") != null)
            //        RadMessageBox.Show(resxSet.GetString("You have successfully delete layout!"));
            //    else
            //        RadMessageBox.Show("You have successfully delete layout!");
            //}
        }
        

        private void gridItems_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridSummaryCellElement)
            {
                if (!String.IsNullOrEmpty(e.CellElement.Text))
                {
                    e.CellElement.BorderLeftWidth = e.CellElement.BorderRightWidth = 1;
                    e.CellElement.Font = new Font("Verdana", 8, FontStyle.Bold);
                    e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                    
                }
                else
                {
                    e.CellElement.ResetValue(LightVisualElement.BorderLeftWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
                    e.CellElement.ResetValue(LightVisualElement.BorderRightWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
                }
            }
        }

        private void fillform2(AccCreditPayModel enterb)
        {
            
            if (enterb != null)
            {
                idDoc = enterb.idDocument;
                idTsk = enterb.idTask;
                idCP = enterb.idCreditPay;
                txtInkop2.Text = enterb.inkopNr.ToString();
                dpDate2.Text = enterb.dtItem.ToString();
                dpValuta2.Text = enterb.dtValuta.ToString();
                txtClient2.Text = enterb.accNumber.ToString();
                txtAccount2.Text = enterb.account.ToString();
                if (enterb.idBtw != 0)
                    txtBtw2.Text = enterb.idBtw.ToString();
                else
                    txtBtw2.Text = "";
                txtInvoiceNr2.Text = enterb.invoiceNr.ToString();
                txtDescription2.Text = enterb.descItem.ToString();
                txtIban2.Text = enterb.iban.ToString();
                if (enterb.amountC != null)
                    txtAmount2Credit.Value = enterb.amountC;
                if (enterb.amountD != null)
                    txtAmount2Debit.Value = enterb.amountD;
                if (enterb.cost != null && enterb.cost != "")
                    txtCost2.Text = enterb.cost.ToString();
                txtProject2.Text = enterb.project.ToString();
                //ddlCurr2.SelectedText = enterb.currency.ToString();
                ddlCurr2.SelectedItem.Text = enterb.currency.ToString();
                if (enterb.idOption != 0)
                    ddlOption.SelectedValue = enterb.idOption;
                if (enterb.paydays != null && enterb.paydays != 0)
                   txtPayDays2.Text = enterb.paydays.ToString();
                if (enterb.idTask != 0)
                    btnTask.Text = "Taak*";
                else
                    btnTask.Text = "Taak";
                if (enterb.idDocument != 0)
                    btnGetDoc2.Text = "Document*";
                else
                    btnGetDoc2.Text = "Document";
                if (enterb.idDocument != 0)
                    idDoc = enterb.idDocument;
                if (enterb.idTask != 0)
                    idTsk = enterb.idTask;
                if (enterb.isAprBook == true)
                    chkApproveBook.CheckState = CheckState.Checked;
                else
                    chkApproveBook.CheckState = CheckState.Unchecked;
                if (enterb.paydays != 0)
                    txtPayDays2.Text = enterb.paydays.ToString();
                labelPayDays.Text = "";
                labelProject2.Text = "";
                labelPayment2.Text = "";
                //else
                //{
                //    txtPaydays2.Text = "";
                //    labelPayment2.Text = "";
                //}

                getnames2();
            }

        }
        private void getnames2()
        {
            if (txtClient2.Text != "")
            {
                AccDebCreBUS debpers = new AccDebCreBUS();
                AccDebCreModel pm1X = new AccDebCreModel();
                string fn = "";
                string mn = "";
                string ln = "";
                pm1X = debpers.GetCreditorName(txtClient2.Text);
                if (pm1X != null)
                {
                    if (pm1X.idClient != 0)
                    {
                        ClientBUS cb = new ClientBUS();
                        ClientModel cm = new ClientModel();
                        cm = cb.GetClient(pm1X.idClient);
                         if (cm != null)
                          {
                            labelClient2.Text = cm.nameClient;
                            enterb.idClient = cm.idClient;
                            if (pm1X.payCondition != null && pm1X.payCondition != 0)
                            {
                                AccPaymentBUS pmb = new AccPaymentBUS();
                                AccPaymentModel pmm = new AccPaymentModel();
                                pmm = pmb.GetPaymentByID(pm1X.payCondition);
                                if (pmm != null)
                                {
                                   txtPayDays2.Text = pmm.numberDays.ToString();
                                   labelPayDays.Text = pmm.description;     // labelPayment2.Text
                                }
                            }
                            }
                      
                    }
                    else
                    {
                        if (pm1X.idContPerson != 0)
                        {
                            PersonBUS pb = new PersonBUS();
                            PersonModel pm = new PersonModel();
                            pm = pb.GetPerson(pm1X.idContPerson);
                            if (pm != null)
                            {
                                if (pm.firstname == null)
                                    fn = "";
                                else
                                    fn = pm.firstname;
                                if (pm.midname == null)
                                    mn = "";
                                else
                                    mn = pm.midname;
                                if (pm.lastname == null)
                                    ln = "";
                                else
                                    ln = pm.lastname;

                                labelClient2.Text = fn + " " + mn + " " + ln;

                            }
                            else
                            {
                                labelClient2.Text = "";
                            }

                        }
                    }
                }
                else
                {
                    labelClient2.Text = "";
                }



                //}
            }
            else
            {
                labelClient2.Text = "";
            }
            if (txtBtw2.Text != "")
            {
                AccTaxBUS tb = new AccTaxBUS();
                AccTaxModel tm = new AccTaxModel();
                tm = tb.GetTaxByID(Convert.ToInt32(txtBtw2.Text));
                if (tm != null)
                    labelBtw2.Text = tm.descTax;
                else
                    labelBtw2.Text = "";
            }
            else
            {
                labelBtw2.Text = "";
            }

            if (txtAccount2.Text != "")
            {
                LedgerAccountBUS lb = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lm = new LedgerAccountModel();
                lm = lb.GetAccount(txtAccount2.Text, Login._bookyear);
                if (lm != null)
                {
                    labelAccount2.Text = lm.descLedgerAccount;
                    if (lm.btwId != 0 && lm.btwId != null)
                        txtBtw2.Text = lm.btwId.ToString();
                }
                else
                    labelAccount2.Text = "";
            }
            else
            {
                labelAccount2.Text = "";
            }

            if (txtCost2.Text != "")
            {
                AccCostBUS ab = new AccCostBUS();
                AccCostModel am = new AccCostModel();
                am = ab.GetCostByID(txtCost2.Text);
                if (am != null)
                    labelCost2.Text = am.descCost;
                else
                    labelCost2.Text = "";
            }
            else
            {
                labelCost2.Text = "";
            }
            if (txtProject2.Text != "")
            {
                ArrangementBUS arb = new ArrangementBUS();
                ArrangementModel arm = new ArrangementModel();
                arm = arb.GetArrangementByCode(txtProject2.Text);
                if (arm != null)
                {
                    if (arm.statusArrangement == "cxd")
                    {
                        labelProject2.Text = arm.nameArrangement + " - CANCELED";
                        labelProject2.ForeColor = Color.Red;
                    }
                    else
                    {
                        labelProject2.Text = arm.nameArrangement;
                        labelProject2.ForeColor = Color.Black;
                    }
                    
                    fromdate = arm.dtFromArrangement;
                    idArange = arm.idArrangement;
                    labelPayment2.Text = "Start date => " + fromdate.ToShortDateString() + " - " + "end date =>  " + arm.dtToArrangement.ToShortDateString();
                }
                else
                {
                    labelProject2.Text = "";
                }
            }
            else
            {
                labelProject2.Text = "";
            }

        }
        private void clearform2()
        {
            if (isNew2 == true)
                   dpDate2.Value = DateTime.Now;
            dpValuta2.Value = DateTime.Now;
            labelClient2.Text = "";
            labelAccount2.Text = "";
            labelBtw2.Text = "";
            labelCost2.Text = "";
            labelProject2.Text = "";
            txtProject2.Text = "";
            txtInvoiceNr2.Text = "";
            txtIban2.Text = "";
            txtInkop2.Text = "";
            txtAccount2.Text = "";
            txtAmount2Credit.Value = Convert.ToDecimal("0,00");
            txtAmount2Debit.Value = Convert.ToDecimal("0,00");
            txtBtw2.Text = "";
            txtClient2.Text = "";
            txtCost2.Text = "";
            txtProject2.Text = "";
           // txtInkop2.Text = "";
            txtDescription2.Text = "";
            txtPayDays2.Text = "";
            txtAmount2Debit.Text = "";
            labelPayment2.Text = "";
            labelPayDays.Text = "";
            idTsk = 0;
            idDoc = 0;
            chkApproveBook.CheckState = CheckState.Unchecked;

        }



        #endregion EnterBook

        private void btnNew2_Click(object sender, EventArgs e)
        {
            clearform2();
            enterb = new AccCreditPayModel();
            lines.Clear();
            lines = new List<AccLineModel>();
            
            getIncopNr2();
            isNew2 = true;
            isEdit2 = false;
            if (isNew2 == true)
                btnSave2.Text = "Save";
            if (isEdit == true)
                btnSave2.Text = "Update";
            btnTask.Text = "Taak";
            btnGetDoc2.Text = "Document";
            dpDate2.Value = DateTime.Now;
            dpDate2.Focus();
            gridItems.DataSource = null;
            gridItems.ReadOnly = true;
            chkApproveBook.ReadOnly = true;
            btnPaying.Enabled = false;
            ddlOption.SelectedIndex = 0;
        }

        private void btnSave2_Click(object sender, EventArgs e)
        {
            //==== brisanje viska reda ako postoji (konto je prazan)

            if (splitlist != null && splitlist.Count > 0)
            {

                lines = new List<AccLineModel>();
                for (int i=0; i < splitlist.Count; i++)
                {
                    lines.Add(splitlist[i]);
                }
            }

            if (lines != null && lines.Count > 0)
            {
                List<AccLineModel> listAll = new List<AccLineModel>();
                listAll = (List<AccLineModel>)gridItems.DataSource;

                List<AccLineModel> listDetail = new List<AccLineModel>();
                listDetail = listAll.FindAll(s => s.numberLedAccount == "");

                if (listDetail != null)
                    if (listDetail.Count > 0)
                    {
                        for (int n = 0; n < listDetail.Count; n++)
                            listAll.Remove(listDetail[n]);
                    }
            }

            if (lines != null)
            {
                sumgrid();
            }
            if (lines != null)
            {
                if (txtProject2.Text != lines[0].idProjectLine)
                {
                    existdiff = true;
                    translateRadMessageBox msg = new translateRadMessageBox();
                    msg.translateAllMessageBox("Not a valid project !!");
                    //  RadMessageBox.Show("Not a valid project !! ");
                }
            }
           
            if (existdiff == false)
            {
                      //  fillLines();
                        if (isNew2 == true)
                            btnSave2.Text = "Save";
                        if (isEdit2 == true)
                            btnSave2.Text = "Update";
                        enterb.inkopNr = txtInkop2.Text;
                        enterb.dtCreation = DateTime.Now;
                        enterb.dtItem = Convert.ToDateTime(dpDate2.Text);
                        enterb.dtValuta = Convert.ToDateTime(dpValuta2.Text);
                        enterb.accNumber = txtClient2.Text;
                      //  enterb.account = txtAccount2.Text;
                        enterb.approvedUser = 0;
                        enterb.createUser = Login._user.idUser;
                        enterb.iban = txtIban2.Text;
                        //if (txtBtw2.Text != "")
                        //    enterb.idBtw = Convert.ToInt32(txtBtw2.Text);
                        //else
                        //    enterb.idBtw = 0;
                        enterb.invoiceNr = txtInvoiceNr2.Text;
                        enterb.descItem = txtDescription2.Text;
                        enterb.amountC = Convert.ToDecimal(txtAmount2Credit.Text);
                        enterb.amountD = Convert.ToDecimal(txtAmount2Debit.Text);
                       // enterb.amountD = Convert.ToDecimal(txtAmount2Debit.Text);
                        enterb.idOption = Convert.ToInt32(ddlOption.SelectedValue);
                        enterb.currency = ddlCurr2.SelectedItem.Text;
                        enterb.idDocument = idDoc;
                        enterb.idTask = idTsk;
                        if (chkApproveBook.CheckState == CheckState.Checked)
                            enterb.isAprBook = true;
                        else
                            enterb.isAprBook = false;
                        enterb.inkopNr = txtInkop2.Text;
                        //       enterb.currency = ddlCurr2.SelectedItem.Text;
                        if (txtCost2.Text != "")
                            enterb.cost = txtCost2.Text;
                        if (txtProject2.Text != "")
                            enterb.project = txtProject2.Text;
                        enterb.amountInCurr = 0;
                        enterb.idCreditPay = idCP;
                        if (txtPayDays2.Text != "")
                        {
                            int numb;
                            bool result = Int32.TryParse(txtPayDays2.Text, out numb);
                            if (result)
                            {
                                enterb.paydays = numb;
                            }
                            else
                                enterb.paydays = 0;
                        }

                        right_panel_change = false;

                        //=======================================================
                        if (isEdit == true)
                        {
                            selectedRowGrid.inkopNr = txtInkop2.Text;
                            selectedRowGrid.dtCreation = DateTime.Now;
                            selectedRowGrid.dtItem = Convert.ToDateTime(dpDate2.Text);
                            selectedRowGrid.dtValuta = Convert.ToDateTime(dpValuta2.Text);
                            selectedRowGrid.accNumber = txtClient2.Text;
                            selectedRowGrid.account = txtAccount2.Text;
                            selectedRowGrid.approvedUser = 0;
                            selectedRowGrid.createUser = Login._user.idUser;
                            selectedRowGrid.iban = txtIban2.Text;
                            if (txtBtw2.Text != "")
                                selectedRowGrid.idBtw = Convert.ToInt32(txtBtw2.Text);
                            else
                                selectedRowGrid.idBtw = 0;
                            selectedRowGrid.invoiceNr = txtInvoiceNr2.Text;
                            selectedRowGrid.descItem = txtDescription2.Text;
                            selectedRowGrid.amountC = Convert.ToDecimal(txtAmount2Credit.Text);
                            selectedRowGrid.amountD = Convert.ToDecimal(txtAmount2Debit.Text);
                            selectedRowGrid.amountD = Convert.ToDecimal(txtAmount2Debit.Text);
                            selectedRowGrid.idOption = Convert.ToInt32(ddlOption.SelectedValue);
                            selectedRowGrid.currency = ddlCurr2.SelectedItem.Text;
                            selectedRowGrid.idDocument = idDoc;
                            selectedRowGrid.idTask = idTsk;
                            if (chkApproveBook.CheckState == CheckState.Checked)
                                selectedRowGrid.isAprBook = true;
                            else
                                selectedRowGrid.isAprBook = false;
                            selectedRowGrid.inkopNr = txtInkop2.Text;
                            //       enterb.currency = ddlCurr2.SelectedItem.Text;
                            if (txtCost2.Text != "")
                                selectedRowGrid.cost = txtCost2.Text;
                            if (txtProject2.Text != "")
                                selectedRowGrid.project = txtProject2.Text;
                            selectedRowGrid.amountInCurr = 0;
                            selectedRowGrid.idCreditPay = idCP;
                            if (txtPayDays2.Text != "")
                                selectedRowGrid.paydays = Convert.ToInt32(txtPayDays2.Text);

                            if (selectedRowGrid.idTask != 0)
                                btnTask.Text = "Taak*";
                            else
                                btnTask.Text = "Taak";
                            if (selectedRowGrid.idDocument != 0)
                                btnGetDoc2.Text = "Document*";
                            else
                                btnGetDoc2.Text = "Document";
                        }
            
                        //======================================================
                        if (isNew2 == true)
                        {
                  
                            isSplitOk = false;
                            btnSplit.PerformClick();
                            if (isSplitOk == true)
                            {
                                iDcredit = bus.Save(enterb, this.Name, Login._user.idUser);
                                enterb.idCreditPay = iDcredit;
                                idCP = iDcredit;

                                fillLines();   // save grid with splittin lines
                                clearform2();
                                lines.Clear();
                                gridItems.DataSource = null;
                                lines = new List<AccLineModel>();
                              //  clearform2();
                               // gridItems.DataSource = null;
                              //  isRowChange = false;
                                isNew2 = true;
                                isEdit2 = false;
                                listEB.Clear();
                                enterb = null;
                                enterb = new AccCreditPayModel();
                                listEB = new List<AccCreditPayModel>();
                                listEB = bus.GetAllPays();
                                gridBookPay.DataSource = null;
                                gridBookPay.DataSource = listEB;

                              //  fillLines();   // save grid with splittin lines
                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                {
                                    if (resxSet.GetString("Saved") != null)
                                        RadMessageBox.Show(resxSet.GetString("Saved"));
                                    else
                                        RadMessageBox.Show("Saved");
                                }
                               // isRowChange = false;

                               
                                
                            }
                            else
                            {
                        
                                return;
                            }

                        }
                        else
                        {
                            if (isEdit2 == true)
                            {
                                isNew2 = false;
                                bus.Update(enterb, this.Name, Login._user.idUser);
                              //  delLines();
                               // btnSplit_Click(sender, e);
                                fillLines();
                                translateRadMessageBox msg = new translateRadMessageBox();
                                msg.translateAllMessageBox("Updated");
                                //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                //{
                                //    if (resxSet.GetString("Updated") != null)
                                //        RadMessageBox.Show(resxSet.GetString("Updated"));
                                //    else
                                //        RadMessageBox.Show("Updated");
                                //}
                               // isRowChange = false;
                                if (selectedRowGrid.idTask != 0)
                                    btnTask.Text = "Taak*";
                                else
                                    btnTask.Text = "Taak";
                                if (selectedRowGrid.idDocument != 0)
                                    btnGetDoc2.Text = "Document*";
                                else
                                    btnGetDoc2.Text = "Document";

                                gridBookPay.Focus();
                                isEdit2 = true;
                                btnSave2.Text = "Update";


                               
                            }

                  
                        }

                        //if (isEdit2 == true)
                            gridBookPay.Focus();
                        if (gridBookPay.SelectedRows != null)
                            if (gridBookPay.SelectedRows.Count > 0)
                            {

                                AccCreditPayModel selectedRowGrid1 = (AccCreditPayModel)gridBookPay.SelectedRows[0].DataBoundItem;
                                enterb = new AccCreditPayModel();
                                if (selectedRowGrid != null)
                                {
                                    enterb = selectedRowGrid1;
                                    iD = 0;
                                    iD = selectedRowGrid1.idCreditPay;
                                    iDcredit = selectedRowGrid1.idCreditPay;
                                    fillform2(enterb);
                                    lines = null;
                                    lines = new AccCreditLineBUS().GetLine(iD);
                                    gridItems.DataSource = null;
                                    gridItems.DataSource = lines;
                                    // isRowChange = false;
                                    gridItems.Show();
                                }
                            }
                        //clearform2();
                        //gridItems.DataSource = null;
                        //lines = null;
                        //lines = new List<AccLineModel>();
                        //linesmodel = new AccLineModel();
                        //model = new AccLineModel();
                        //model1 = new AccLineModel();
                        //enterb = null;
                        //enterb = new AccCreditPayModel();
                        isEdit2 = true;
                        isNew = false;
                        btnSave2.Text = "Update";
                        chkApproveBook.ReadOnly = false;
                        //ddlOption.SelectedIndex = 0;
                        btnPaying.Enabled = true;
            }
            else
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("No condition for save");

                //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                //{
                //    if (resxSet.GetString("No condition for save") != null)
                //        RadMessageBox.Show(resxSet.GetString("No condition for save"));
                //    else
                //        RadMessageBox.Show("No condition for save");
                //}
            }
        }

        private void btnGetDoc2_Click(object sender, EventArgs e)
        {
            if (txtClient2.Text != "")
            {
                AccDebCreBUS clib = new AccDebCreBUS();
                AccDebCreModel clim = new AccDebCreModel();
                clim = clib.GetCustomerByAccCode(txtClient2.Text);
                if (clim != null)
                {
                    idCli = clim.idClient;
                    if (idCli != 0)
                    {
                        //idDoc = 0;
                        using (var form = new frmLookClientDoc(idCli, idDoc))
                        {
                            var result = form.ShowDialog();
                            if (result == DialogResult.OK)
                            {
                                idDoc = form.idDoc;
                                if (enterb != null)
                                    enterb.idDocument = idDoc;

                            }
                        }
                    }
                    else
                    {
                        translateRadMessageBox msg = new translateRadMessageBox();
                        msg.translateAllMessageBox("Not Client !!");

                        //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        //{
                        //    if (resxSet.GetString("Not Client !!") != null)
                        //        RadMessageBox.Show(resxSet.GetString("Not Client !!"));
                        //    else
                        //        RadMessageBox.Show("Not Client !!");
                        //}
                        return;
                    }
                }
                else
                {
                    translateRadMessageBox msg = new translateRadMessageBox();
                    msg.translateAllMessageBox("Wrong Client Id !!");

                    //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    //{
                    //    if (resxSet.GetString("Wrong Client Id !!") != null)
                    //        RadMessageBox.Show(resxSet.GetString("Wrong Client Id !!"));
                    //    else
                    //        RadMessageBox.Show("Wrong Client Id !!");
                    //}
                    txtClient2.Focus();
                    return;
                }
                isEdit2 = true;
               // btnSave2.PerformClick();
            }
            else
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("Enter Client, please !!");

                //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                //{
                //    if (resxSet.GetString("Enter Client, please !!") != null)
                //        RadMessageBox.Show(resxSet.GetString("Enter Client, please !!"));
                //    else
                //        RadMessageBox.Show("Enter Client, please !!");
                //}
            }
        }

        private void btnViewDoc2_Click(object sender, EventArgs e)
        {
            if (gridBookPay.CurrentRow != null)
            {
                GridViewRowInfo info = this.gridBookPay.CurrentRow;
                AccCreditPayModel selectedRow = (AccCreditPayModel)info.DataBoundItem;
                if (enterb == null)
                    enterb = new AccCreditPayModel();
                enterb = selectedRow;
                if (enterb.idDocument == null && enterb.idDocument == 0)
                {
                    translateRadMessageBox msg = new translateRadMessageBox();
                    msg.translateAllMessageBox("No document to view ...");

                    //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    //{
                    //    if (resxSet.GetString("No document to view ...") != null)
                    //        RadMessageBox.Show(resxSet.GetString("No document to view ..."));
                    //    else
                    //        RadMessageBox.Show("No document to view ...");
                    //}
                }
                else
                {
                    DocumentsBUS dbs = new DocumentsBUS();
                    DocumentsModel dbm = new DocumentsModel();
                    dbm = dbs.GetDocument(enterb.idDocument);
                    if (dbm == null)
                    {
                        translateRadMessageBox msg = new translateRadMessageBox();
                        msg.translateAllMessageBox("No document to view ...");

                        //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        //{
                        //    if (resxSet.GetString("No document to view ...") != null)
                        //        RadMessageBox.Show(resxSet.GetString("No document to view ..."));
                        //    else
                        //        RadMessageBox.Show("No document to view ...");
                        //}
                    }
                    else
                    {
                        string sDest = System.Reflection.Assembly.GetEntryAssembly().Location;
                        sDest = sDest.Substring(0, sDest.LastIndexOf('\\')) + "\\Documents\\";
                        string fullname = sDest + dbm.fileDocument;
                        if (System.IO.File.Exists(fullname))
                            OpenDocument(sDest, dbm.fileDocument);
                        else
                        {
                            translateRadMessageBox msg = new translateRadMessageBox();
                            msg.translateAllMessageBox("Error opening document");
                        }
                          //  RadMessageBox.Show("Error opening document", "Open error", MessageBoxButtons.OK, RadMessageIcon.Error);
                    }
                }
            }

        }
        private void btnTask_Click(object sender, EventArgs e)
        {
            if (txtClient2.Text != "")
            {
                if (enterb != null)
                {
                    int idta = -1;
                    string what = "new";
                    string forwho = "client";
                    int cli = 0;
                    if (enterb.idClient != 0)
                        cli = Convert.ToInt32(enterb.idClient);
                    if (enterb.idTask != 0)
                    {
                        what = "old";
                        idta = enterb.idTask;
                    }
                    frmTasks fts = new frmTasks(idta, 0, what, forwho, cli, 0);
                    fts.ShowDialog();
                    if (fts.idTsk != 0)
                        idTsk = fts.idTsk;
                    if (enterb != null && what == "new")
                        enterb.idTask = idTsk;
                    isEdit2 = true;
                  //  btnSave2.PerformClick();
                }
                else
                {
                    translateRadMessageBox msg = new translateRadMessageBox();
                    msg.translateAllMessageBox("Add new record, please!");

                    //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    //{
                    //    if (resxSet.GetString("Add new record, please!") != null)
                    //        RadMessageBox.Show(resxSet.GetString("Add new record, please!"));
                    //    else
                    //        RadMessageBox.Show("Add new record, please!");
                    //}
                }
            }
            else
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("Enter client first, please!");

                //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                //{
                //    if (resxSet.GetString("Enter client first, please!") != null)
                //        RadMessageBox.Show(resxSet.GetString("Enter client first, please!"));
                //    else
                //        RadMessageBox.Show("Enter client first, please!");
                //}
            }
        }
        private void btnSplit_Click(object sender, EventArgs e)
        {
            List<AccDailyModel> adm = new List<AccDailyModel>();
            adm = new AccDailyBUS(Login._bookyear).GetBookingDailysInkop2();
            if (adm != null)
            {
                xDaily = adm[0].idDaily;
            }
            else
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("No Daily !!!");
                //RadMessageBox.Show("No Daily !!!");
                isSplitOk = false;
                return;
            }
            LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
            LedgerAccountModel lam = new LedgerAccountModel();
            if (adm[0].numberLedgerAccount != null && adm[0].numberLedgerAccount != "")
            {
                lam = ledbus.GetAccount(adm[0].numberLedgerAccount, Login._bookyear);
                if (lam != null)
                {
                    // === kupi sta je obavezno za unos ===
                    //manCreditor = lam.mandatoryCreditorAccount;
                    //manDebitor = lam.mandatoryDebitorAccount;
                    //manCost = lam.mandatoryCostAccount;
                    //manProject = lam.mandatoryProjectAccount;
                    //manBTW = lam.isBTWLedgerAccount;  
                    //xAccount = lam.numberLedgerAccount;
                }
            }
            if (lines != null && lines.Count > 0)
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    lam = new LedgerAccountModel();
                    if (lines[i].numberLedAccount == "")
                    {
                        translateRadMessageBox msg = new translateRadMessageBox();
                        msg.translateAllMessageBox("Can't save without Account !!!");
                      //  RadMessageBox.Show("Can't save without Account !!!");
                        isSplitOk = false;
                        return;
                    }
                    lam = ledbus.GetAccount(lines[i].numberLedAccount, Login._bookyear);
                    if (lam != null)
                    {
                        // === kupi sta je obavezno za unos ===
                        manCreditor = lam.mandatoryCreditorAccount;
                        manDebitor = lam.mandatoryDebitorAccount;
                        manCost = lam.mandatoryCostAccount;
                        manProject = lam.mandatoryProjectAccount;
                        manBTW = lam.isBTWLedgerAccount;
                        xAccount = lam.numberLedgerAccount;
                    }
                    if (lines[i].idClientLine == "" && manCreditor == true)
                    {
                        translateRadMessageBox msg = new translateRadMessageBox();
                        msg.translateAllMessageBox("Can't save without Creditor !!!");
                     //   RadMessageBox.Show("Can't save without Creditor !!!");
                        isSplitOk = false;
                        txtClient2.Focus();

                        return;
                    }
                    else
                    {
                        if (lines[i].invoiceNr == "")
                        {
                            translateRadMessageBox msg = new translateRadMessageBox();
                            msg.translateAllMessageBox("Can't save without Invoice number !!!");
                          //  RadMessageBox.Show("Can't save without Invoice number !!!");
                            isSplitOk = false;
                            txtInvoiceNr2.Focus();

                            return;
                        }
                        else
                        {
                            if (lines[i].idCostLine == "" && manCost == true)
                            {
                                translateRadMessageBox msg = new translateRadMessageBox();
                                msg.translateAllMessageBox("Cost mandatory !!!");
                               // RadMessageBox.Show("Cost mandatory !!!");
                                isSplitOk = false;
                                txtCost2.Focus();

                                return;
                            }
                            else
                            {

                                if (lines[i].idProjectLine == "" && manProject == true)
                                {
                                    translateRadMessageBox msg = new translateRadMessageBox();
                                    msg.translateAllMessageBox("Project mandatory !!!"+"  "+ (i+1).ToString() + ".  line");

                                    RadMessageBox.Show("Project mandatory !!!" +"  "+ (i+1).ToString() + ".  line");
                                     isSplitOk = false;
                                     txtProject2.Focus();

                                    return;
                                }
                                else
                                {
                                    if (Convert.ToDecimal(txtAmount2Credit.Text) == 0 && Convert.ToDecimal(txtAmount2Debit.Text) == 0)
                                    {
                                        translateRadMessageBox msg = new translateRadMessageBox();
                                        msg.translateAllMessageBox("Amount 0 !!!");
                                      //  RadMessageBox.Show("Amount 0 !!!");
                                        isSplitOk = false;
                                        txtAmount2Credit.Focus();

                                        return;
                                    }
                                    else
                                    {
                                        if (lines[i].idBTW == 0 && manBTW)
                                        {
                                            translateRadMessageBox msg = new translateRadMessageBox();
                                            msg.translateAllMessageBox("BTW mandatory");
                                          //  RadMessageBox.Show("BTW mandatory");
                                            isSplitOk = false;
                                            txtBtw2.Focus();

                                            return;
                                        }
                                        else
                                        {
                                            if (lines[i].numberLedAccount == "")
                                            {
                                                translateRadMessageBox msg = new translateRadMessageBox();
                                                msg.translateAllMessageBox("Account is mandatory");
                                               // RadMessageBox.Show("Account is mandatory");
                                                isSplitOk = false;
                                                txtAccount2.Focus();

                                                return;
                                            }
                                            else
                                            {
                                                if (txtIban2.Text == "")
                                                {
                                                    translateRadMessageBox msg = new translateRadMessageBox();
                                                    msg.translateAllMessageBox("Iban is mandatory");
                                                 //   RadMessageBox.Show("Iban is mandatory");
                                                    isSplitOk = false;
                                                    txtIban2.Focus();

                                                    return;
                                                }
                                                else
                                                {
                                                    isSplitOk = true;
                                                  //  splitting();
                                                }
                                            }

                                        }

                                    }
                                }
                            }
                        }

                    }
                }
            }
           
        
           
        }
        private void splitting()
        {
            decimal debcreAmt = 0;
            string xcodeBTW = "";
            decimal btwPercent;
            int btwtype=0;
            decimal btwAmt=0;
            string xBtwConto="";
            AccAcountUpdate au = new AccAcountUpdate();
            
            lines = new List<AccLineModel>();
            linesmodel = new AccLineModel();
            model = new AccLineModel();
            model1 = new AccLineModel();

            //prva - osnovna stavka  booksoort 1

            linesmodel.idAccDaily = enterb.idCreditPay;
            if (txtBtw2.Text != "")
                linesmodel.idBTW = Convert.ToInt32(txtBtw2.Text);
            linesmodel.idClientLine = txtClient2.Text;
            linesmodel.periodLine = au.Period(Convert.ToDateTime(dpValuta2.Text));
            linesmodel.incopNr = txtInkop2.Text;
            linesmodel.invoiceNr = txtInvoiceNr2.Text;
            linesmodel.numberLedAccount = xAccount;
            if (Convert.ToDecimal(txtAmount2Credit.Text) != 0)
                linesmodel.creditLine = Convert.ToDecimal(txtAmount2Credit.Text);
            if (Convert.ToDecimal(txtAmount2Debit.Text) != 0)
                linesmodel.debitLine = Convert.ToDecimal(txtAmount2Debit.Text);
            linesmodel.descLine = txtDescription2.Text;
            linesmodel.dtLine = Convert.ToDateTime(dpDate2.Text);
            linesmodel.dtBooking = Convert.ToDateTime(dpValuta2.Text);
            linesmodel.idCostLine = txtCost2.Text;
            linesmodel.idProjectLine = txtProject2.Text;

            lines.Add(linesmodel);

            
            if (Convert.ToInt32(txtBtw2.Text) != 0)  //==== uzima procenat ===
            {
                AccTaxBUS atb = new AccTaxBUS();
                AccTaxModel atm = new AccTaxModel();
                atm = atb.GetTaxByID(Convert.ToInt32(txtBtw2.Text));
                if (atm != null)
                {
                    xcodeBTW = atm.codeTax;
                    btwtype = Convert.ToInt32(atm.typeTax);
                    xBtwConto = atm.numberLedAccount;
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
            //================== prva splitovana
            if (txtAccount2.Text != "")                          // ako konto nije prazno uzima ga za prvu stavku ili ga ostavlja praznog
            {
                model.numberLedAccount = txtAccount2.Text;
            }
            else
            {
                model.numberLedAccount = "";

            }
            model.idAccDaily = linesmodel.idAccDaily;
         
            model.dtLine = linesmodel.dtLine;
            if (txtProject2.Text != "")
                model.dtLine = fromdate;
            else
                model.dtLine = linesmodel.dtLine;
            //if (linesmodel.idProjectLine != "")
            //{
            //    model.dtLine = fromdate;
            //}
            model.dtBooking = linesmodel.dtBooking;
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
            if (txtAccount2.Text != "" && txtBtw2.Text != "")
                sw = "1";   // ima i account i porez
            if (txtAccount2.Text == "" && txtBtw2.Text != "")
                sw = "2";  // nema account ima porez
            if (txtAccount2.Text == "" && txtBtw2.Text == "")
                sw = "3";  // nema konto nema porez
            if (txtAccount2.Text != "" && txtBtw2.Text == "")
                sw = "4"; // ima account nema porez

            decimal aa=0;
            switch (sw)
            {
                case "1":   // ima i account i porez
                    //if (Convert.ToDecimal(txtAmount2Credit.Text) != 0)
                    //{
                        if (Convert.ToDecimal(this.txtAmount2Credit.Text)!= 0)
                             aa = Convert.ToDecimal(this.txtAmount2Credit.Text);
                        else
                             aa = Convert.ToDecimal(this.txtAmount2Debit.Text);
                        
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
                    //}
                   //  if (xSideBooking == "D")
                    if (Convert.ToDecimal(txtAmount2Credit.Text) != 0)
                    {
                        model.debitLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);
                        model.creditLine = 0;
                    }
                    else
                    {
                        model.creditLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);
                        model.debitLine = 0;
                    }
                  
                    break;

                case "2":

                    //if (Convert.ToDecimal(txtAmount2Credit.Text) != 0)
                    //{
                    if (Convert.ToDecimal(this.txtAmount2Credit.Text) != 0)
                        aa = Convert.ToDecimal(this.txtAmount2Credit.Text);
                    else
                        aa = Convert.ToDecimal(this.txtAmount2Debit.Text);


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
                  //  }
                   

                    //  if (xSideBooking == "D")
                    if (Convert.ToDecimal(txtAmount2Credit.Text) != 0)
                    {
                        model.debitLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);
                        model.creditLine = 0;
                    }
                    else
                    {
                        model.creditLine = Math.Round(Convert.ToDecimal(debcreAmt), 2);
                        model.debitLine = 0;
                    }
                   

                    break;

                case "3":

                    if (Convert.ToDecimal(txtAmount2Credit.Text) != 0)
                    {
                        model.debitLine = Convert.ToDecimal(txtAmount2Credit.Text);
                        model.creditLine = 0;
                    }
                    else
                    {
                        model.creditLine = Convert.ToDecimal(txtAmount2Credit.Text);
                        model.debitLine = 0;
                    }
                   

                    break;

                case "4":

                    if (Convert.ToDecimal(txtAmount2Credit.Text) != 0)
                    {
                        model.debitLine = Convert.ToDecimal(txtAmount2Credit.Text);
                        model.creditLine = 0;
                    }
                    else
                    {
                        model.creditLine = Convert.ToDecimal(txtAmount2Credit.Text);
                        model.debitLine = 0;
                    }
                  

                    break;

            }

            lines.Add(model);

            //==================================  druga stavka ============================================
            model1 = new AccLineModel();

            if (txtBtw2.Text != "")
            {

                model1.idAccDaily = linesmodel.idAccDaily;
                model1.dtLine = linesmodel.dtLine;
                model1.dtBooking = linesmodel.dtBooking;
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

                if (Convert.ToDecimal(txtAmount2Credit.Text) != 0)
                {
                    model1.debitLine = Math.Round(btwAmt, 2);
                    model1.creditLine = 0;
                }
                else
                {
                    model1.creditLine = Math.Round(btwAmt, 2);
                    model1.debitLine = 0;
                }
               
                // model1.debitLine = Math.Round(btwAmt);
                model1.idBTW = linesmodel.idBTW;
                model1.idProjectLine = linesmodel.idProjectLine;
                model1.idClientLine = linesmodel.idClientLine;
                model1.idCostLine = linesmodel.idCostLine;
                model1.incopNr = linesmodel.incopNr;

                lines.Add(model1);

           }
            gridItems.DataSource = null;
            gridItems.DataSource = lines;
           // isRowChange = false;
            gridItems.Show();
            //   btnSplit.Enabled = false;

        }

        #region gridEdit

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

            if (e.ColumnIndex != -1)
            {
                GridViewCellInfo i = (GridViewCellInfo)e.Row.Cells[e.ColumnIndex];
                endEdit(i);
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
                                GridViewRowInfo info = e.RowInfo.ViewInfo.CurrentRow;
                                //if (d == 0)
                                //    info.Cells["debitBTW"].Value = 0;
                                    
                                   
                            
                            
                            
                            
                            if (inc_excl == 2)
                                    info.Cells["debitBTW"].Value = Math.Abs(Convert.ToDecimal(gridItems.CurrentRow.Cells["debitLine"].Value) - Convert.ToDecimal(gridItems.CurrentRow.Cells["creditLine"].Value)) * percent / 100;
                                else
                                {
                                    if (inc_excl != 0)
                                    {
                                        //decimal aa = 0;
                                        //decimal btw_amount = 0;
                                        //decimal amount = 0;
                                        //if (inc_excl == 1)
                                        //{
                                        //    if (Convert.ToDecimal(info.Cells["debitLine"].Value) > 0)
                                        //        aa = Convert.ToDecimal(info.Cells["debitLine"].Value);
                                        //    else
                                        //        aa = Convert.ToDecimal(info.Cells["creditLine"].Value);

                                        //    btw_amount = aa - (aa / (1 + percent / 100));
                                        //    amount = aa - btw_amount;//aa / (1 + percent / 100);
                                        //    info.Cells["debitBTW"].Value = btw_amount;
                                        //    if (Convert.ToDecimal(txtAmount2Credit.Text) > 0)
                                        //        info.Cells["debitLine"].Value = amount;
                                        //    else
                                        //        info.Cells["creditLine"].Value = amount;

                                       // }
                                    }
                                }
                          //  }
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
                }
            }
            catch(Exception exep)
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
            if (editor.Text != txtClient2.Text)
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("Not valid client number!");
               // RadMessageBox.Show("Not valid client number!");
            }
        }
        void cellEditorAccount_KeyDown(object sender, KeyEventArgs e)
        {
            RadTextBoxEditorElement editor = sender as RadTextBoxEditorElement;
            if(e.KeyCode == Keys.F1)
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
                        translateRadMessageBox msg = new translateRadMessageBox();
                        msg.translateAllMessageBox("Wrong account");
                     //   RadMessageBox.Show("Wrong account");
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
                        translateRadMessageBox msg = new translateRadMessageBox();
                        msg.translateAllMessageBox("Wrong Arrangement code");
                        //RadMessageBox.Show("Wrong Arrangement code");
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
                    if (gridItems.CurrentRow.Cells["debitBTW"].Value != null && Convert.ToInt32(gridItems.CurrentRow.Cells["debitBTW"].Value )!= 0)
                    {
                        int id_tax = Convert.ToInt32(gridItems.CurrentRow.Cells["idBTW"].Value);
                        string konto = getBTWKonto(id_tax);
                        decimal tax_amount = Convert.ToDecimal(gridItems.CurrentRow.Cells["debitBTW"].Value);
                        if (gridItems.CurrentRow.Cells["idMaster"].Value != null && gridItems.CurrentRow.Cells["idDetail"].Value!=null)
                        addBtw(tax_amount, konto, gridItems.CurrentRow.Cells["idMaster"].Value.ToString(), gridItems.CurrentRow.Cells["idDetail"].Value.ToString());
                     //   gridItems.CurrentRow.Cells["debitBTW"].Value = 0;
                    }
                }
            }

        }
        private void gridBookPay_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {
            translateRadMessageBox msgbox = new translateRadMessageBox();
            DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("Do you want to DELETE this line ?", "Delete");
          //  DialogResult dr = RadMessageBox.Show("Do you want to DELETE this line ?", "Delete", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                if (gridBookPay.CurrentRow != null)
                {
                    GridViewRowInfo info = this.gridBookPay.CurrentRow;
                    AccCreditPayModel selectedRow = (AccCreditPayModel)info.DataBoundItem;
                    AccCreditPayBUS pb = new AccCreditPayBUS();
                    int aaa = selectedRow.idCreditPay;
                    pb.Delete(selectedRow.idCreditPay, this.Name, Login._user.idUser);
                    if (lines != null && lines.Count > 0)
                    {
                        AccCreditLineBUS crb = new AccCreditLineBUS();
                        crb.Delete(aaa, this.Name, Login._user.idUser);
                    }
                }

            }
            else
            {
                e.Cancel = true;
                return;
            }

        }
         private void gridItems_UserAddingRow(object sender, GridViewRowCancelEventArgs e)
        {
          //  isRowChange = true;
           
            int aa = gridItems.RowCount;
            if (aa > 1)
            {
              //  // RadMessageBox.Show("New row is clicked");
                gridItems.CurrentRow.Cells["dtLine"].Value = gridItems.Rows[aa - 1].Cells["dtLine"].Value;
                gridItems.CurrentRow.Cells["invoiceNr"].Value = gridItems.Rows[aa - 1].Cells["invoiceNr"].Value;
                //  gridItems.CurrentRow.Cells["numberLedAccount"].Value = "";//gridItems.Rows[aa - 1].Cells["numberLedAccount"].Value;
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
                List<AccLineModel> list = new List<AccLineModel>();
                list = (List<AccLineModel>)gridItems.DataSource;

                int numberNext = 0;

                foreach (AccLineModel m in list)
                {
                    if (Convert.ToInt32(m.idMaster.Replace(txtInkop2.Text, "")) > numberNext)
                        numberNext = Convert.ToInt32(m.idMaster.Replace(txtInkop2.Text, ""));
                }
                numberNext = numberNext + 1;
                if (gridItems.Rows[aa - 1].Cells["incopNr"].Value != null)
                    gridItems.CurrentRow.Cells["idMaster"].Value = gridItems.CurrentRow.Cells["incopNr"].Value + numberNext.ToString();
                 }
           
             
               gridItems.CurrentRow.Cells["dtLine"].BeginEdit();
        }

         private void gridItems_KeyDown(object sender, KeyEventArgs e)
         {
             //int cc = gridItems.RowCount;

             //if (e.KeyCode == Keys.F5)
             //{
                
             //   gridItems.Rows.AddNew();

             //   gridItems.CurrentRow.Cells["dtLine"].Value = gridItems.Rows[cc - 1].Cells["dtLine"].Value;
             //    int a = gridItems.RowCount;
             //    if (a >= 1)
             //        gridItems.Rows[a - 1].Cells["dtLine"].BeginEdit();
             //}



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
                             else
                             {
                                   //int dd = gridItems.RowCount;
                                   //if (dd > 1)
                                   //{
                                   //    //  // RadMessageBox.Show("New row is clicked");
                                   //    gridItems.CurrentRow.Cells["dtLine"].Value = gridItems.Rows[dd - 1].Cells["dtLine"].Value;
                                   //    gridItems.CurrentRow.Cells["invoiceNr"].Value = gridItems.Rows[dd - 1].Cells["invoiceNr"].Value;
                                   //    //  gridItems.CurrentRow.Cells["numberLedAccount"].Value = "";//gridItems.Rows[aa - 1].Cells["numberLedAccount"].Value;
                                   //    gridItems.CurrentRow.Cells["descLine"].Value = gridItems.Rows[dd - 1].Cells["descLine"].Value;
                                   //    gridItems.CurrentRow.Cells["idClientLine"].Value = gridItems.Rows[dd - 1].Cells["idClientLine"].Value;
                                   //    //gridItems.CurrentRow.Cells["idBTW"].Value = 0;//gridItems.Rows[aa - 1].Cells["idBTW"].Value;
                                   //    gridItems.CurrentRow.Cells["idCostLine"].Value = gridItems.Rows[dd - 1].Cells["idCostLine"].Value;
                                   //    gridItems.CurrentRow.Cells["idProjectLine"].Value = gridItems.Rows[dd - 1].Cells["idProjectLine"].Value;
                                   //    //  gridItems.CurrentRow.Cells["project"].Value = gridItems.Rows[aa - 1].Cells["project"].Value;
                                   //    if (gridItems.Rows[dd - 1].Cells["incopNr"].Value != null)
                                   //        gridItems.CurrentRow.Cells["incopNr"].Value = gridItems.Rows[dd - 1].Cells["incopNr"].Value;
                                   //    if (gridItems.Rows[dd - 1].Cells["iban"].Value != null)
                                   //        gridItems.CurrentRow.Cells["iban"].Value = gridItems.Rows[dd - 1].Cells["iban"].Value;
                                   //    if (gridItems.Rows[dd - 1].Cells["dtBooking"].Value != null)
                                   //        gridItems.CurrentRow.Cells["dtBooking"].Value = gridItems.Rows[dd - 1].Cells["dtBooking"].Value;
                                   ////    SendKeys.Send("UP");
                                      gridItems.CurrentRow.Cells["dtLine"].BeginEdit();
                                  /// }
                             }

                         }
                        
                         //}
                     }
                 }
             //int a = gridItems.RowCount;
             //if (a >= 1)
             //    gridItems.Rows[a - 1].Cells["dtLine"].BeginEdit();
         }


    
        private void sumgrid()
         {
           
             existdiff = false;
             debit = 0;
             credit = 0;
             decimal difference = 0;

             if (txtIban2.Text == "")
             {
                 existdiff = true;
                 translateRadMessageBox msg = new translateRadMessageBox();
                 msg.translateAllMessageBox("No Iban number");

                // RadMessageBox.Show("No Iban number");
                 return;
             }

             if (txtInvoiceNr2.Text == "")
             {
                 existdiff = true;
                 translateRadMessageBox msg = new translateRadMessageBox();
                 msg.translateAllMessageBox("No Invoice number");
               //  RadMessageBox.Show("No Invoice number");
                 return;
             }
            decimal test_amount=0;
            if (Convert.ToDecimal(txtAmount2Credit.Value) != 0)
            {
                for (int i=0; i < lines.Count; i++)
                {
                    if (lines[i].numberLedAccount == defCreditor)
                        test_amount = test_amount + lines[i].creditLine;
                }
                if (test_amount != Convert.ToDecimal(txtAmount2Credit.Value))
                {
                    existdiff = true;
                    translateRadMessageBox msg = new translateRadMessageBox();
                    msg.translateAllMessageBox("Wrong amount in items");
                  //  RadMessageBox.Show("Wrong amount in items");
                    return;
                }
            }
            if (Convert.ToDecimal(txtAmount2Debit.Value) != 0)
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].numberLedAccount == defCreditor)
                        test_amount = test_amount + lines[i].debitLine;
                }
                if (test_amount != Convert.ToDecimal(txtAmount2Debit.Value))
                {
                    existdiff = true;
                    translateRadMessageBox msg = new translateRadMessageBox();
                    msg.translateAllMessageBox("Wrong amount in items");
                  //  RadMessageBox.Show("Wrong amount in items");
                    return;
                }
            }
             if (lines.Count > 0)
             {
                 LedgerAccountBUS lb = new LedgerAccountBUS(Login._bookyear);
                 LedgerAccountModel lm = new LedgerAccountModel();

                 for (int i = 0; i < lines.Count; i++)
                 {
                     if (lines[i].creditLine != 0)
                         credit = credit + Convert.ToDecimal(lines[i].creditLine);
                     if (lines[i].debitLine != 0)
                         debit = debit + Convert.ToDecimal(lines[i].debitLine);
                // === check account ===
                     if (lines[i].numberLedAccount != "")
                     {
                         lm = new LedgerAccountModel();


                         manCreditor = lm.mandatoryCreditorAccount;
                         manDebitor = lm.mandatoryDebitorAccount;
                         manCost = lm.mandatoryCostAccount;
                         manProject = lm.mandatoryProjectAccount;
                         manBTW = lm.isBTWLedgerAccount;
                         xAccount = lm.numberLedgerAccount;
                         
                         lm = lb.GetAccount(lines[i].numberLedAccount, Login._bookyear);
                         if (lm != null)
                         {
                             if (lm.numberLedgerAccount.Trim() != lines[i].numberLedAccount.Trim())
                             {
                                 existdiff = true;
                                 translateRadMessageBox msg = new translateRadMessageBox();
                                 msg.translateAllMessageBox("Wrong account");
                                // RadMessageBox.Show("Wrong account");
                                 return;
                             }
                         }
                         else
                         {
                             existdiff = true;
                             translateRadMessageBox msg = new translateRadMessageBox();
                             msg.translateAllMessageBox("Wrong account");
                          //   RadMessageBox.Show("Wrong account");
                             return;
                         }

                     }
                     else
                     {
                         existdiff = true;
                         translateRadMessageBox msg = new translateRadMessageBox();
                         msg.translateAllMessageBox("Wrong account");
                       // RadMessageBox.Show("Wrong account");
                         return;
                     }
                     //========= check client 
                     if (lines[i].idClientLine != txtClient2.Text )
                     {
                         existdiff = true;
                         RadMessageBox.Show("Wrong Client " + (i + 1).ToString() + ". line ");
                         return;
                     }
                     //========= check invoice 
                     if (lines[i].invoiceNr != txtInvoiceNr2.Text)
                     {
                         existdiff = true;
                         translateRadMessageBox msg = new translateRadMessageBox();
                         msg.translateAllMessageBox("Wrong invoice number, split again,please");
                       //  RadMessageBox.Show("Wrong invoice number, split again,please ");
                         return;
                     }
                     //======== check cost ======
                     if (lines[i].idCostLine != "")
                     {
                         AccCostModel lm1 = new AccCostModel();
                         AccCostBUS lb1 = new AccCostBUS();

                         lm1 = lb1.GetCostByID(lines[i].idCostLine);
                         if (lm1 != null)
                         {
                             if (lm1.codeCost != lines[i].idCostLine)
                             {
                                 existdiff = true;
                                 translateRadMessageBox msg = new translateRadMessageBox();
                                 msg.translateAllMessageBox("Wrong cost" +" "+ (i + 1).ToString() + ". line ");
                              //   RadMessageBox.Show("Wrong cost" + (i + 1).ToString() + ". line ");
                                 return;
                             }
                         }
                         else
                         {
                             existdiff = true;
                             translateRadMessageBox msg = new translateRadMessageBox();
                             msg.translateAllMessageBox("Wrong cost");
                          //   RadMessageBox.Show("Wrong cost");
                             return;
                         }

                     }
                     else
                     {
                         if (manCost == true)
                         {
                             existdiff = true;
                             translateRadMessageBox msg = new translateRadMessageBox();
                             msg.translateAllMessageBox("Cost mandatory");
                            // RadMessageBox.Show("Cost mandatory");
                             return;
                         }
                     }
                     //========== project
                     if (lines[i].idProjectLine != "")
                     {
                         ArrangementModel lm2 = new ArrangementModel();
                         ArrangementBUS lb2 = new ArrangementBUS();

                         lm2 = lb2.GetArrangementCodeProject(lines[i].idProjectLine);
                         if (lm2 != null)
                         {
                             if (lm2.codeProject != lines[i].idProjectLine)
                             {
                                 existdiff = true;
                                 translateRadMessageBox msg = new translateRadMessageBox();
                                 msg.translateAllMessageBox("Wrong project");
                                 //RadMessageBox.Show("Wrong project");
                                 return;
                             }
                         }
                         else
                         {
                             existdiff = true;
                             translateRadMessageBox msg = new translateRadMessageBox();
                             msg.translateAllMessageBox("Wrong project");
                          //   RadMessageBox.Show("Wrong project");
                             return;
                         }

                     }
                     else
                     {
                         if (manProject == true)
                         {
                             existdiff = true;
                             translateRadMessageBox msg = new translateRadMessageBox();
                             msg.translateAllMessageBox("Project mandatory" + " "+(i + 1).ToString() + ". line ");
                        //     RadMessageBox.Show("Project mandatory  "+ (i+1).ToString() + ". line ");
                             return;
                         }
                     }
                 }

             }
            //if (Convert.ToDecimal(txtAmount2Credit.Value) != Convert.ToDecimal(enterb.amountC))
            //{
            //    //if (Convert.ToDecimal(txtAmount2Credit.Value) != credit)
            //    //{
            //    //    RadMessageBox.Show("Amount credit does not match sum of items  ?");
            //    //    existdiff = true;
            //    //    return;
            //    //}
            //   DialogResult dr = RadMessageBox.Show("Your credit amount changed! do you realy want to save ?", "Save", MessageBoxButtons.YesNo);
            //        if (dr == DialogResult.Yes)
            //        {

            //        }
            //        else
            //        {
            //            txtAmount2Credit.Value = Convert.ToDecimal(enterb.amountC); //existdiff = true;
            //            return;
            //        }
            //}
            //if (Convert.ToDecimal(txtAmount2Debit.Value) != Convert.ToDecimal(enterb.amountD))
            //{
               
            //    DialogResult dr = RadMessageBox.Show("Your debit amount changed! do you realy want to save ?", "Save", MessageBoxButtons.YesNo);
            //    if (dr == DialogResult.Yes)
            //    {

            //    }
            //    else
            //    {
            //        txtAmount2Debit.Value = Convert.ToDecimal(enterb.amountD);
            //       // existdiff = true;
            //        return;
            //    }
            //}

            difference = debit - credit;
            if (difference != 0)
                existdiff = true;
         }

        private void gridItems_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {
            translateRadMessageBox msgbox = new translateRadMessageBox();
            DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("Do you want to DELETE this line ?", "Delete");
          //  DialogResult dr = RadMessageBox.Show("Do you want to DELETE this line ?", "Delete", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                if (gridItems.CurrentRow != null)
                {
                    GridViewRowInfo info = this.gridItems.CurrentRow;
                    AccLineModel selectedRow = (AccLineModel)info.DataBoundItem;
                    int aaa = selectedRow.idAccLine;
                    if (lines != null && lines.Count > 0)
                    {
                        if (selectedRow.idDetail != "" || selectedRow.numberLedAccount == masterAccount)
                        {
                            e.Cancel = true;
                            return;
                        }
                        else
                        {
                            AccCreditLineBUS crb = new AccCreditLineBUS();
                          //  if (crb.DeleteIdMaster(selectedRow.idMaster) == true)
                          //  {
                                List<AccLineModel> list = new List<AccLineModel>();
                                list = (List<AccLineModel>)gridItems.DataSource;

                                list.RemoveAll(s => s.idMaster == selectedRow.idMaster);
                                list.RemoveAll(s => s.idDetail == selectedRow.idMaster);
                                gridItems.DataSource = null;
                                gridItems.DataSource = list;

                         //   }



                        }
                    }
                }

            }
            else
            {
                e.Cancel = true;
                return;
            }
        }


        #endregion gridEdit

      private void fillLines()
        {
            bool isOk = false;
           AccCreditLineBUS lb = new AccCreditLineBUS();
          //  gridaccline = new List<AccLineModel>();
           if (isEdit2 == true)
           {
               int aaa = iD;
               lb.DeleteDaily(aaa, this.Name, Login._user.idUser);
           }

           if (isNew2 == true)
           {
               if (lines != null)
               {
                   if (lines.Count > 0)
                   {
                       for (int w = 0; w < lines.Count; w++)
                       {

                           accline = new AccLineModel();
                           accline.idAccDaily = iDcredit;
                           accline.dtLine = Convert.ToDateTime(lines[w].dtLine);
                           accline.idClientLine = lines[w].idClientLine;
                           accline.numberLedAccount = lines[w].numberLedAccount;
                           accline.invoiceNr = lines[w].invoiceNr;
                           accline.incopNr = lines[w].incopNr;
                           accline.idBTW = lines[w].idBTW;
                           accline.descLine = lines[w].descLine;
                           accline.idCostLine = lines[w].idCostLine;
                           accline.idProjectLine = lines[w].idProjectLine;
                           accline.dtBooking = lines[w].dtBooking;
                           accline.debitLine = lines[w].debitLine;
                           accline.creditLine = lines[w].creditLine;
                           accline.iban = txtIban2.Text;
                           accline.idSepa = Convert.ToInt32(ddlOption.SelectedIndex);  // ovde cuva vrstu placanja SEPA, ICASo da bi prilikom knjizenja prebacio u otvorene stavke;
                           accline.debitBTW = lines[w].debitBTW;
                           accline.idMaster = lines[w].idMaster;
                           accline.idDetail = lines[w].idDetail;
                           accline.booksort = w + 1;

                           isOk = lb.Save(accline, this.Name, Login._user.idUser);
                           if (isOk == false)
                           {
                               translateRadMessageBox msg = new translateRadMessageBox();
                               msg.translateAllMessageBox("Error writing lines");

                              // RadMessageBox.Show("Error writing lines");
                               return;
                           }
                       }

                   }
               }
           }
           else
           {
               if (isEdit2 == true)
               {
                   for (int w = 0; gridItems.RowCount - 1 >= w; w++)
                   {

                       accline = new AccLineModel();
                       accline.idAccDaily = iDcredit;
                       accline.dtLine = Convert.ToDateTime(gridItems.Rows[w].Cells["dtLine"].Value);   //Convert.ToDateTime(lines[w].dtLine);
                       accline.idClientLine = gridItems.Rows[w].Cells["idClientLine"].Value.ToString();   //lines[w].idClientLine;
                       accline.numberLedAccount = gridItems.Rows[w].Cells["numberLedAccount"].Value.ToString();//lines[w].numberLedAccount;
                       accline.invoiceNr = gridItems.Rows[w].Cells["invoiceNr"].Value.ToString(); // lines[w].invoiceNr;
                       accline.incopNr = gridItems.Rows[w].Cells["incopNr"].Value.ToString(); //lines[w].incopNr;
                       accline.idBTW = Convert.ToInt32(gridItems.Rows[w].Cells["idBTW"].Value);//lines[w].idBTW;
                       accline.descLine = gridItems.Rows[w].Cells["descLine"].Value.ToString();//lines[w].descLine;
                       accline.idCostLine = gridItems.Rows[w].Cells["idCostLine"].Value.ToString();//lines[w].idCostLine;
                       accline.idProjectLine = gridItems.Rows[w].Cells["idProjectLine"].Value.ToString();//lines[w].idProjectLine;
                       accline.dtBooking = Convert.ToDateTime(gridItems.Rows[w].Cells["dtBooking"].Value);//lines[w].dtBooking;
                       accline.debitLine = Convert.ToDecimal(gridItems.Rows[w].Cells["debitLine"].Value);//lines[w].debitLine;
                       accline.creditLine = Convert.ToDecimal(gridItems.Rows[w].Cells["creditLine"].Value);//lines[w].creditLine;
                       accline.idMaster = gridItems.Rows[w].Cells["idMaster"].Value.ToString();
                       accline.idDetail = gridItems.Rows[w].Cells["idDetail"].Value.ToString();
                       accline.idSepa = Convert.ToInt32(ddlOption.SelectedIndex);  // opcija palacanja
                       accline.iban = txtIban2.Text;

                       isOk = lb.Save(accline, this.Name, Login._user.idUser);
                       if (isOk == false)
                       {
                           translateRadMessageBox msg = new translateRadMessageBox();
                           msg.translateAllMessageBox("Error writing lines");
                           //RadMessageBox.Show("Error writing lines");
                           return;
                       }

                   }
               }
           }
       

        }

          
                 

      private void txtIban2_TextChanging(object sender, TextChangingEventArgs e)
      {
      //    right_panel_change = true;
      }
    

      private void txtAmount2Credit_ValueChanged(object sender, EventArgs e)
      {
          if (Convert.ToDecimal(txtAmount2Credit.Value) != 0)
              txtAmount2Debit.Text = "0,00";

         
        //  right_panel_change = true;
      }

      private void txtAmount2Debit_ValueChanged(object sender, EventArgs e)
      {
          if (Convert.ToDecimal(txtAmount2Debit.Text) != 0)
              txtAmount2Credit.Text = "0,00";
        
         // right_panel_change = true;
      }

      

      private void chkApproveBook_ToggleStateChanged(object sender, StateChangedEventArgs args)
      {
         
          if (chkApproveBook.CheckState == CheckState.Checked)
          {
              translateRadMessageBox msgbox = new translateRadMessageBox();
              DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("Do you want to APPROVE this line ?", "Approve");
           // DialogResult dr = RadMessageBox.Show("Do you want to APPROVE this line ?", "Approve", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                // iD = enterb.idCreditPay;
                if (isNew2 == true)
                    iD = 0;
                lines = new AccCreditLineBUS().GetLine(iD);
                if (lines != null)
                {
                    if (isNew2 == true)
                    {
                        btnSplit.PerformClick(); //btnSave2.PerformClick();
                        if (isSplitOk == true)
                        {
                            btnSave2.PerformClick();
                            btnPaying.Enabled = true;
                            isNew2 = false;
                            isEdit2 = true;
                            btnSave2.Text = "Update";
                          //  btnPaying.Focus();
                            //RadMessageBox.Show("Saved");
                        }
                        else
                        {
                            isNew2 = true;
                            isEdit2 = false;
                            //btnSave2.Text = "Update";
                            //btnPaying.Enabled = true;
                            //btnPaying.Focus();
                        }
                    }
                    else
                    {
                        isEdit2 = true;
                        isNew2 = false;
                        btnSplit.PerformClick();
                        if (isSplitOk == true)
                        {
                            btnSave2.PerformClick();
                        }
                        // clearform2();

                        listEB = new List<AccCreditPayModel>();
                        listEB = bus.GetAllPays();
                        gridBookPay.DataSource = null;
                        gridBookPay.DataSource = listEB;
                        if (gridBookPay.DataSource == null)
                            gridItems.DataSource = null;

                    }
                }

                //=============== ako ima slogova \Crveno =====================
                bus = new AccCreditPayBUS();
                listmBook = bus.GetAllPaysApproved();
                if (listmBook != null && listmBook.Count > 0)
                {
                    this.rpvBook.Item.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    this.rpvBook.Item.ForeColor = System.Drawing.Color.Black;
                }

                List<AccDailyModel> adm = new List<AccDailyModel>();
                adm = new AccDailyBUS(Login._bookyear).GetBookingDailysInkop2();
                if (adm != null)
                {
                    xDaily = adm[0].idDaily;
                    xConto = adm[0].numberLedgerAccount;
                    txtDaily.Text = adm[0].codeDaily + "   " + adm[0].descDaily;
                }
                //==========================================================

            }
            else
            {
                chkApproveBook.CheckState = CheckState.Unchecked;
            }
          
          }
      }

   
      private void txtClient2_Leave(object sender, EventArgs e)
      {
          if (enterb != null && isEdit2 == true)
          {
              if (txtClient2.Text != enterb.accNumber)
              {
                  translateRadMessageBox msgbox = new translateRadMessageBox();
                  DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("You change the client !! Do You want to change client in items?", "Save");
              //    DialogResult dr = RadMessageBox.Show("You change the client !! Do You want to change client in items?", "Save", MessageBoxButtons.YesNo);
                  if (dr == DialogResult.Yes)
                  {
                      if (lines != null)
                          for (int i = 0; i < lines.Count; i++)
                          {
                              if (lines[i].idClientLine == enterb.accNumber)
                                  lines[i].idClientLine = txtClient2.Text;
                          }

                      AccDebCreBUS adc = new AccDebCreBUS();
                      AccDebCreModel adm = new AccDebCreModel();
                      adm = adc.GetCustomerByAccCode(txtClient2.Text);

                      if (adm != null)
                      {
                          if (adm.payCondition != null && adm.payCondition != 0)
                          {
                              AccPaymentBUS pmb = new AccPaymentBUS();
                              AccPaymentModel pmm = new AccPaymentModel();
                              pmm = pmb.GetPaymentByID(adm.payCondition);
                              if (pmm != null)
                              {
                                  txtPayDays2.Text = pmm.numberDays.ToString();
                                  labelPayDays.Text = pmm.description;  //labelPayment2.Text
                              }
                          }
                      }
                      txtIban2.Text = "";

                      gridItems.DataSource = null;
                      gridItems.DataSource = lines;


                  }
                  else
                  {
                      txtClient2.Text = enterb.accNumber;
                  }
              }
          
          }

          if (isNew2 == true)
              if (lines != null && lines.Count > 0)
                  if (txtClient2.Text != lines[0].idClientLine)
                  {
                      string crack_cli = "";
                      crack_cli = lines[0].idClientLine;
                      if (txtClient2.Text != crack_cli)
                      {
                          translateRadMessageBox msgbox = new translateRadMessageBox();
                          DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("You change the client !! Do You want to change client in items?", "Save");
                      //    DialogResult dr = RadMessageBox.Show("You change the client !! Do You want to change client in items?", "Save", MessageBoxButtons.YesNo);
                          if (dr == DialogResult.Yes)
                          {
                              if (lines != null)
                                  for (int i = 0; i < lines.Count; i++)
                                  {
                                      if (lines[i].idClientLine == crack_cli)
                                          lines[i].idClientLine = txtClient2.Text;
                                  }

                              AccDebCreBUS adc = new AccDebCreBUS();
                              AccDebCreModel adm = new AccDebCreModel();
                              adm = adc.GetCustomerByAccCode(txtClient2.Text);

                              if (adm != null)
                              {
                                  if (adm.payCondition != null && adm.payCondition != 0)
                                  {
                                      AccPaymentBUS pmb = new AccPaymentBUS();
                                      AccPaymentModel pmm = new AccPaymentModel();
                                      pmm = pmb.GetPaymentByID(adm.payCondition);
                                      if (pmm != null)
                                      {
                                          txtPayDays2.Text = pmm.numberDays.ToString();
                                          labelPayDays.Text = pmm.description;  //labelPayment2.Text
                                      }
                                  }
                              }
                              txtIban2.Text = "";

                              gridItems.DataSource = null;
                              gridItems.DataSource = lines;


                          }
                          else
                          {
                              txtClient2.Text = crack_cli;
                          }
                      }
                  }

           getnames2();
                if (txtClient2.Text != "")
                {
                    
                    AccDebCreBUS adc = new AccDebCreBUS();
                    AccDebCreModel adm = new AccDebCreModel();
                    adm = adc.GetCustomerByAccCode(txtClient2.Text);
                    if (adm != null)
                    {
                        if (adm.payCondition != null && adm.payCondition != 0)
                        {
                            AccPaymentBUS pmb = new AccPaymentBUS();
                            AccPaymentModel pmm = new AccPaymentModel();
                            pmm = pmb.GetPaymentByID(adm.payCondition);
                            if (pmm != null)
                            {
                                txtPayDays2.Text = pmm.numberDays.ToString();
                                labelPayDays.Text = pmm.description;   //labelPayment2.Text
                                //txtIban2.Text = "";
                            }
                        }
                      
                    }
                    else
                    {
                        translateRadMessageBox msg = new translateRadMessageBox();
                        msg.translateAllMessageBox("Wrong client number");

                        //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        //{
                        //    if (resxSet.GetString("Wrong client number") != null)
                        //        RadMessageBox.Show(resxSet.GetString("Wrong client number"));
                        //    else
                        //        RadMessageBox.Show("Wrong client number");
                        //}
                        txtClient2.Text = "";
                        txtClient2.Focus();
                        return;
                    }

                   // txtIban2.Text = "";
                }
                else
                {
                    txtIban2.Text = "";
                }
             

      }

      private void txtAccount2_Leave(object sender, EventArgs e)
      {
          LedgerAccountBUS lb = new LedgerAccountBUS(Login._bookyear);
          LedgerAccountModel lm = new LedgerAccountModel();
          
          getnames2();
          if (txtAccount2.Text != "")
          {
              lm = lb.GetAccount(txtAccount2.Text, Login._bookyear);
              if (lm == null)
              {
                  translateRadMessageBox msg = new translateRadMessageBox();
                  msg.translateAllMessageBox("Wrong account number");

                  //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                  //{
                  //    if (resxSet.GetString("Wrong account number") != null)
                  //        RadMessageBox.Show(resxSet.GetString("Wrong account number"));
                  //    else
                  //        RadMessageBox.Show("Wrong account number");
                  //}
                  txtAccount2.Focus();
              }
              else
              {
                  labelAccount2.Text = lm.descLedgerAccount;
                  if (lm.btwId != 0 && lm.btwId != null)
                      txtBtw2.Text = lm.btwId.ToString();
              }
         

          }
      }

      private void txtBtw2_Leave(object sender, EventArgs e)
      {
          getnames2();
          if (txtBtw2.Text != "")
          {
              AccTaxBUS tb = new AccTaxBUS();
              AccTaxModel tm = new AccTaxModel();
              tm = tb.GetTaxByID(Convert.ToInt32(txtBtw2.Text));
              if (tm == null)
              {
                  translateRadMessageBox msg = new translateRadMessageBox();
                  msg.translateAllMessageBox("Wrong Btw number");

                  // using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                  //{
                  //    if (resxSet.GetString("Wrong Btw number") != null)
                  //        RadMessageBox.Show(resxSet.GetString("Wrong Btw number"));
                  //    else
                  //        RadMessageBox.Show("Wrong Btw number");
                  //}
                  txtBtw2.Focus();  
              }
                 
              else
                  labelBtw2.Text = tm.descTax;
          }
          else
          {
              labelBtw2.Text = "";
          }
      }

      private void txtCost2_Leave(object sender, EventArgs e)
      {
          getnames2();
          //===================

          if (enterb != null && isEdit2 == true)
                {
                    if (txtCost2.Text.Trim() != enterb.cost.Trim())
                    {
                        translateRadMessageBox msgbox = new translateRadMessageBox();
                        DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("You changed cost code! Do you want to update items?", "Change");

                      // DialogResult dr = RadMessageBox.Show("You changed cost code! Do you want to update items?", "Change", MessageBoxButtons.YesNo);
                       if (dr == DialogResult.Yes)
                       {
                           if (lines != null)
                           {
                               for (int q = 0; q < lines.Count; q++)
                               {
                                   if (lines[q].idCostLine.Trim() == enterb.cost.Trim() || lines[q].idCostLine.Trim() == "")
                                       lines[q].idCostLine = txtCost2.Text;
                               }
                           }
                       }
                       else
                       {
                           txtCost2.Text = enterb.cost;
                       }

                    }
                    gridItems.DataSource = null;
                    gridItems.DataSource = lines;
                }
          //===================

          if (txtCost2.Text != "")
          {
              AccCostBUS ab = new AccCostBUS();
              AccCostModel am = new AccCostModel();
              am = ab.GetCostByID(txtCost2.Text);
              if (am == null)
              {
                  translateRadMessageBox msg = new translateRadMessageBox();
                  msg.translateAllMessageBox("Wrong Cost number");

                  //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                  //{
                  //    if (resxSet.GetString("Wrong Cost number") != null)
                  //        RadMessageBox.Show(resxSet.GetString("Wrong Cost number"));
                  //    else
                  //        RadMessageBox.Show("Wrong Cost number");
                  //}
                  txtCost2.Focus();
              }

              else
              {
                  labelCost2.Text = am.descCost;
              }
          }
          else
          {
              labelCost2.Text = "";
          }
          if (lines != null && lines.Count > 0 && isNew2 == true)
          {
              string crack_cost = "";
              crack_cost = lines[0].idCostLine.Trim();
              if (txtCost2.Text.Trim() != crack_cost.Trim())
              {
                  translateRadMessageBox msgbox = new translateRadMessageBox();
                  DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("You changed cost code! Do you want to update items?", "Change");
                  //DialogResult dr = RadMessageBox.Show("You changed cost code! Do you want to update items?", "Change", MessageBoxButtons.YesNo);
                  if (dr == DialogResult.Yes)
                  {
                      if (lines != null)
                      {
                          for (int q = 0; q < lines.Count; q++)
                          {
                              if (lines[q].idCostLine.Trim() == crack_cost.Trim() || lines[q].idCostLine.Trim() == "")
                                  lines[q].idCostLine = txtCost2.Text;
                          }
                      }
                  }
                  else
                  {
                      txtCost2.Text = crack_cost;
                  }
              }
              gridItems.DataSource = null;
              gridItems.DataSource = lines;
          }
         
      }

      private void txtProject2_Leave(object sender, EventArgs e)
      {
          //if (txtProject2.Text != "")
          //{
          //    ArrangementBUS arb = new ArrangementBUS();
          //    ArrangementModel arm = new ArrangementModel();
          //    arm = arb.GetArrangementByCode(txtProject2.Text);
          //    if (arm == null)
          //    {
          //        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
          //        {
          //            if (resxSet.GetString("Wrong Project number") != null)
          //                RadMessageBox.Show(resxSet.GetString("Wrong Project number"));
          //            else
          //                RadMessageBox.Show("Wrong Project number");
          //        }
          //        labelProject2.Text = "";
          //        labelPayment2.Text = "";

          //        txtProject2.Text = "";
          //        txtProject2.Focus();
          //    }
          //    else
          //    {
          //        labelProject2.Text = arm.nameArrangement;
          //        fromdate = arm.dtFromArrangement;
          //        idArange = arm.idArrangement;
          //        labelPayment2.Text = "Start date => " + fromdate.ToShortDateString() + " - " + "end date =>  " + arm.dtToArrangement.ToShortDateString();
          //    }
          //}
          //else
          //{
          //    labelProject2.Text = "";
          //    labelPayment2.Text = "";

          //}

          getnames2();

          if (enterb != null && isEdit2 == true)
          {
              if (txtProject2.Text != enterb.project) // && proj_update == false)
              {
                  translateRadMessageBox msgbox = new translateRadMessageBox();
                  DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("You changed project code! You have to split items again !!! Press SPLIT button, please ?", "Change");
                 // DialogResult dr = RadMessageBox.Show("You changed project code! You have to split items again !!! Press SPLIT button, please ?", "Change", MessageBoxButtons.YesNo);

                  if (dr == DialogResult.Yes)
                  {
                              
                     // proj_update = true;
                      btnSplit.Focus();
                    // btnEnter.PerformClick();
                  }
                  //DialogResult dr = RadMessageBox.Show("You changed project code! Do you want to update items?", "Change", MessageBoxButtons.YesNo);
                  //if (dr == DialogResult.Yes)
                  //{
                  //    if (lines != null)
                  //    {
                  //        for (int q = 0; q < lines.Count; q++)
                  //        {
                  //            if (lines[q].idProjectLine == enterb.project || lines[q].idProjectLine == "")
                  //            {
                  //                if (lines[q].dtLine != Convert.ToDateTime(dpDate2.Text))
                  //                {
                  //                    lines[q].idProjectLine = txtProject2.Text;
                  //                    lines[q].dtLine = fromdate;
                  //                }
                  //                if (lines[q].numberLedAccount == masterAccount)  // 1600
                  //                {
                  //                    lines[q].idProjectLine = txtProject2.Text;
                  //                }
                  //            }
                  //        }
                  //    }
                  //}
                  //else
                  //{
                  //    txtProject2.Text = enterb.project;
                  //}
              }
              gridItems.DataSource = null;
              gridItems.DataSource = lines;
          }
              
          
         

          if (lines != null && lines.Count > 0 && isNew2 == true)
          {
              string crack_prj = "";
              crack_prj = lines[0].idProjectLine;
              if (txtProject2.Text.Trim() != crack_prj.Trim())
              {
                  translateRadMessageBox msgbox = new translateRadMessageBox();
                  DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("You changed project code! You have to split items again !!! Press SPLIT button, please ?", "Change");
                 //DialogResult dr = RadMessageBox.Show("You changed project code! You have to split items again !!! Do you want to split items?", "Change", MessageBoxButtons.YesNo);

                  if (dr == DialogResult.Yes)
                  {
                              
                      proj_update = true;
                      btnEnter.PerformClick();
                  }
                 
                  //DialogResult dr = RadMessageBox.Show("You changed project code! Do you want to update items?", "Change", MessageBoxButtons.YesNo);
                  //if (dr == DialogResult.Yes)
                  //{
                  //    if (lines != null)
                  //    {
                  //        for (int q = 0; q < lines.Count; q++)
                  //        {
                  //            if (lines[q].idProjectLine == crack_prj.Trim() || lines[q].idProjectLine.Trim() == "")
                  //                if (lines[q].dtLine.ToShortDateString() != Convert.ToDateTime(dpDate2.Value).ToShortDateString())
                  //                {
                  //                    lines[q].idProjectLine = txtProject2.Text;
                  //                    lines[q].dtLine = fromdate;
                  //                }
                  //        }
                  //    }
                  //}
                  //else
                  //{
                  //    txtProject2.Text = crack_prj;
                  //}
              }
              gridItems.DataSource = null;
              gridItems.DataSource = lines;
          }
          
      }

      private void btnViewPayment_Click(object sender, EventArgs e)
      {
                   
          frmClientPayment fcp = new frmClientPayment(txtClient2.Text, txtProject2.Text, idArange);  //enterb.accNumber
          fcp.Show();
      }

      private void txtIban2_Leave(object sender, EventArgs e)
      {
          if (txtIban2.Text != "")
          {
                MakeInvoice cc = new MakeInvoice();
              bool b = cc.ValidateIban(txtIban2.Text.Trim());
              if (b == false)
              {
                  translateRadMessageBox trr = new translateRadMessageBox();
                  trr.translateAllMessageBox("IBAN  NOT CORRECT.");
                  // e.Cancel = true;
                  txtIban2.Focus();
              }
              
          }
      }

      private void btnViewClient_Click(object sender, EventArgs e)
      {
          if (txtClient2.Text == "")
          {
              frmClient fcli = new frmClient();
              fcli.ShowDialog();

          }
          else
          {
              AccDebCreBUS dcb = new AccDebCreBUS();
              AccDebCreModel dcm = new AccDebCreModel();
              dcm = dcb.GetCustomerByAccCode(txtClient2.Text);
              if (dcm != null)
              {
                  ClientBUS cb = new ClientBUS();
                  ClientModel cm = new ClientModel();
                  if (dcm.idClient != 0)
                  {
                      cm = cb.GetClient(dcm.idClient);
                      if (cm != null)
                      {
                          frmClient frmc = new frmClient(cm);
                          frmc.ShowDialog();
                      }
                      else
                      {
                          frmClient fcli = new frmClient();
                          fcli.ShowDialog();
                      }
                  }
              }
          }
      }

      private void btnIbans_Click(object sender, EventArgs e)
      {
       AccIbanBUS aib = new AccIbanBUS();
       List<IModel> aim = new List<IModel>();
       aim = aib.GetIBANForClientString(txtClient2.Text);
     
          var dlgSave4 = new GridLookupForm(aim, "Iban");
          if (dlgSave4.ShowDialog(this) == DialogResult.Yes)
          {
              try
              {
                  AccIbanModel gmX4ml = new AccIbanModel();
                  gmX4ml = (AccIbanModel)dlgSave4.selectedRow;
                  if (gmX4ml != null)
                  {
                      txtIban2.Text = gmX4ml.ibanNumber.ToString();
                      if (txtIban2.Text != "")
                      {
                          if (lines != null && lines.Count > 0)
                          {
                              for (int i = 0; i < lines.Count; i++)
                              {
                                  lines[i].iban = txtIban2.Text;
                              }
                          }
                      }
                  }
                  else
                  {
                      txtIban2.Text = "";
                  }

                  if (txtIban2.Text != "")
                      txtAmount2Credit.Focus();
              }
              catch  (Exception ex)
              {
                
                    MessageBox.Show(ex.Message);
               }
          }
      }
        private void delLines()
      {
          AccCreditLineBUS aclb = new AccCreditLineBUS();

            if (lines != null)
            {
                for (int q=0; q < lines.Count; q++)
                {
                    aclb.Delete(lines[q].idAccLine, this.Name, Login._user.idUser);      
                }
            }
      }

        private void gridBookPay_UserDeletedRow(object sender, GridViewRowEventArgs e)
        {
            RadMessageBox.Show("Delete");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            AccCreditPayBUS dcp = new AccCreditPayBUS();
            if (isEdit2 == true)
            {
                translateRadMessageBox msgbox = new translateRadMessageBox();
                DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("Do you want to DELETE this line and split lines ?", "Delete");
              //      DialogResult dr = RadMessageBox.Show("Do you want to DELETE this line and split lines ?", "Delete", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        delLines();
                        dcp.Delete(enterb.idCreditPay, this.Name, Login._user.idUser);
                        listEB = new List<AccCreditPayModel>();
                        listEB = bus.GetAllPays();
                        gridBookPay.DataSource = null;
                        gridBookPay.DataSource = listEB;
                        gridItems.DataSource = null;
                      //  isRowChange = false;
                        clearform2();
                    }
                   

            }
        }

        private void gridItems_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
           // isRowChange = true;
        }

        private void gridItems_CellClick(object sender, GridViewCellEventArgs e)
        {
            GridCellElement clickedCell = (GridCellElement)sender;


            if (clickedCell.RowElement is GridNewRowElement)
            {

                if (gridItems != null)
                {

                    translateRadMessageBox msgbox = new translateRadMessageBox();
                    DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("Do you want to Add row with values ?", "Adding");
                 //   DialogResult dr = RadMessageBox.Show("Do you want to Add row with values ?", "Adding", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        int aa = gridItems.RowCount;
                        if (aa > 1)
                        {
                            // RadMessageBox.Show("New row is clicked");
                            gridItems.CurrentRow.Cells["dtLine"].Value = gridItems.Rows[aa - 1].Cells["dtLine"].Value;
                            gridItems.CurrentRow.Cells["invoiceNr"].Value = gridItems.Rows[aa - 1].Cells["invoiceNr"].Value;
                            gridItems.CurrentRow.Cells["numberLedAccount"].Value = ""; // null; // gridItems.Rows[aa - 1].Cells["numberLedAccount"].Value;
                            gridItems.CurrentRow.Cells["descLine"].Value = gridItems.Rows[aa - 1].Cells["descLine"].Value;
                            gridItems.CurrentRow.Cells["idClientLine"].Value = gridItems.Rows[aa - 1].Cells["idClientLine"].Value;
                            gridItems.CurrentRow.Cells["idCostLine"].Value = gridItems.Rows[aa - 1].Cells["idCostLine"].Value;
                            gridItems.CurrentRow.Cells["idProjectLine"].Value = gridItems.Rows[aa - 1].Cells["idProjectLine"].Value;
                        }
                    }
                }

            }
        }
         
        private void btnPaying_Click(object sender, EventArgs e)
        {
            decimal amount = 0;
            if (Convert.ToDecimal(txtAmount2Credit.Text) != 0)
                amount = Convert.ToDecimal(txtAmount2Credit.Text);
            else
                if (Convert.ToDecimal(txtAmount2Debit.Text) != 0)
                    amount = Convert.ToDecimal(txtAmount2Debit.Text);
           // List<AccCreditLinePayModel> cbmold = new List<AccCreditLinePayModel>();
            AccCreditLinePayBUS cbp = new AccCreditLinePayBUS();
            List<AccCreditLinePayModel> cbm = new List<AccCreditLinePayModel>();
            cbm = cbp.GetAllLinesByCreditor(txtClient2.Text, txtInvoiceNr2.Text);
            multimodel = new List<AccCreditLinePayModel>();
          
            frmSplitOpenLines fsp = new frmSplitOpenLines(cbm, amount, txtClient2.Text, txtInvoiceNr2.Text);
            fsp.ShowDialog();
            
            multimodel = fsp.multimodel;

            if (fsp.exit == "yes")
            {
                if (multimodel != null)
                {
                    List<AccCreditLinePayModel> cbmold = new List<AccCreditLinePayModel>();
                    cbmold = cbp.GetAllLinesByCreditor(txtClient2.Text, txtInvoiceNr2.Text);
                    if (cbmold != null)
                    {
                        
                        for (int w = 0; w < cbmold.Count; w++)
                        {
                            cbp.Delete(cbmold[w].idCreditLinePay, this.Name, Login._user.idUser);
                        }
                    }

                    for (int i = 0; i < multimodel.Count; i++)
                    {
                        AccCreditLinePayModel amod = new AccCreditLinePayModel();
                        amod.dtDate = multimodel[i].dtDate;
                        amod.accNumber = txtClient2.Text;
                        amod.invoiceNr = txtInvoiceNr2.Text;
                        amod.percentpay = multimodel[i].percentpay;
                        amod.term = i + 1;
                        amod.amount = multimodel[i].amount;
                        cbp.Save(amod, this.Name, Login._user.idUser);
                    }
                }
                else
                {
                    List<AccCreditLinePayModel> cbmold = new List<AccCreditLinePayModel>();
                    cbmold = cbp.GetAllLinesByCreditor(txtClient2.Text, txtInvoiceNr2.Text);
                    if (cbmold != null)
                    {

                        for (int w = 0; w < cbmold.Count; w++)
                        {
                            cbp.Delete(cbmold[w].idCreditLinePay, this.Name, Login._user.idUser);
                        }
                    }
                }
                
            }

        }
        void gridItems_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (gridItems.Columns["debitLine"].IsCurrent || gridItems.Columns["creditLine"].IsCurrent )
            {
                GridSpinEditor spinEditor = this.gridItems.ActiveEditor as GridSpinEditor;
                ((GridSpinEditorElement)spinEditor.EditorElement).ShowUpDownButtons = false;
            }
        }

        private void ddlOption_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                txtPayDays2.Focus();
        }

        private void txtInvoiceNr2_Leave(object sender, EventArgs e)
        {
            if (txtClient2.Text != "" && txtInvoiceNr2.Text != "")
            {
                AccSettingsBUS asb = new AccSettingsBUS();
                bool empty = false;
                empty = asb.ClientInvoice(txtClient2.Text, txtInvoiceNr2.Text.Trim(), Login._bookyear);
                if (empty == false)
                {
                    translateRadMessageBox msg = new translateRadMessageBox();
                    msg.translateAllMessageBox("Invoice number EXIST !!!!! Please check !!!!!");

                  //  RadMessageBox.Show("Invoice number EXIST !!!!! Please check !!!!!");
                    txtInvoiceNr2.Focus();
                }
            }
            //}
            //else
            //{
            //    if (txtInvoiceNr2.Text == "")
            //    {
            //        RadMessageBox.Show("Invoice mandatory !!!");
            //        txtInvoiceNr2.Focus();
            //    }
            //}
            if (enterb != null && isEdit2== true )
            {
                if (txtInvoiceNr2.Text != enterb.invoiceNr)
                {
                    translateRadMessageBox msgbox = new translateRadMessageBox();
                    DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("You changed invoice number!! Do you want to update lines?", "Change");
               //       DialogResult dr = RadMessageBox.Show("You changed invoice number!! Do you want to update lines?", "Change", MessageBoxButtons.YesNo);
                      if (dr == DialogResult.Yes)
                      {
                          if (lines != null)
                          {
                              for (int i = 0; i < lines.Count; i++)
                              {
                                  if (lines[i].invoiceNr == enterb.invoiceNr)
                                      lines[i].invoiceNr = txtInvoiceNr2.Text;
                              }
                                                                                         
                              gridItems.DataSource = null;
                              gridItems.DataSource = lines;
                              enterb.invoiceNr = txtInvoiceNr2.Text;
                          }
                      }
                      else
                      {
                          txtInvoiceNr2.Text = enterb.invoiceNr;
                      }
                }


            }
            if (isNew2 == true && lines != null && lines.Count > 0)
            {
                string crack_fak = "";
                crack_fak = lines[0].invoiceNr; 
                if (txtInvoiceNr2.Text != crack_fak)
                {
                    translateRadMessageBox msgbox = new translateRadMessageBox();
                    DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("You changed invoice number!! Do you want to update lines?", "Change");
                   //  DialogResult dr = RadMessageBox.Show("You changed invoice number!! Do you want to update lines?", "Change", MessageBoxButtons.YesNo);
                      if (dr == DialogResult.Yes)
                      {
                          if (lines != null)
                          {
                              for (int i = 0; i < lines.Count; i++)
                              {
                                  if (lines[i].invoiceNr == crack_fak)
                                      lines[i].invoiceNr = txtInvoiceNr2.Text;
                              }
                                                                                         
                              gridItems.DataSource = null;
                              gridItems.DataSource = lines;
                              enterb.invoiceNr = txtInvoiceNr2.Text;
                          }
                      }
                      else
                      {
                          txtInvoiceNr2.Text = crack_fak;
                      }
                }
            }
        }

        private void txtPayDays2_ValueChanged(object sender, EventArgs e)
        {
            dpValuta2.Value= dpDate2.Value.AddDays(Convert.ToInt32(txtPayDays2.Value));
        }

      private void makenewrow()
        {
         decimal calculate_btw=0;


            if (gridItems != null)
                if (gridItems.RowCount > 0)
                    if (gridItems.SelectedRows != null)
                        if (gridItems.SelectedRows.Count > 0)
                        {

                            AccLineModel selectRowToMake = (AccLineModel)gridItems.SelectedRows[0].DataBoundItem;
                          
                            if (selectRowToMake != null)
                            {
                                
                                row_toMake = selectRowToMake;
                                //if (row_toMake.idBTW != 0)
                                //    calculate_btw = calcBTW(Convert.ToInt32(row_toMake.idBTW), row_toMake.debitLine);
                                iD = 0;
                                iD = selectRowToMake.idAccDaily;
                                iDcredit = selectedRowGrid.idCreditPay;
                                fillform2(enterb);

                                lines = new AccCreditLineBUS().GetLine(iD);
                                gridItems.DataSource = null;
                                gridItems.DataSource = lines;
                              //  isRowChange = false;
                                gridItems.Show();
                            }
                        }
        }

     

      private void calcBTW(int idBtw, decimal debit)
     {
          btw_percent=0;
          if (idBtw != 0)
          {
              AccTaxBUS atxb = new AccTaxBUS();
              AccTaxModel atxm = new AccTaxModel();
              atxm = atxb.GetTaxByID(idBtw);
              if (atxm != null)
              {
                  if (atxm.codeTax != "" && atxm.codeTax != null)
                  {

                      AccTaxValidityBUS txb = new AccTaxValidityBUS();
                      AccTaxValidityModel tm = new AccTaxValidityModel();
                      tm = txb.GetTaxValidityByCode(atxm.codeTax);
                      if (tm != null)
                      {
                          btw_percent = Convert.ToDecimal(tm.percentTax);
                      }
                      else
                      {
                          btw_percent = 0;
                      }
                  }

              }
          }
                        
                     
     }
         bool isokvalc = false;
        bool isok = false;
      private void gridItems_ValueChanged(object sender, EventArgs e)
      {
          if (gridItems.CurrentCell != null)
          {
              string aname = gridItems.CurrentCell.ColumnInfo.Name;

              if (aname == "idBTW")   //this.gridItems.ActiveEditor is RadTextBoxEditor &&
              {
                  if (isokvalc == false)
                  {
                      isokvalc = true;
                      decimal valueM = 0;
                      AccLineModel mod = (AccLineModel)gridItems.CurrentRow.DataBoundItem;
                      if (mod != null)
                          if (btw_percent > 0)
                              valueM = (decimal)mod.debitLine * (decimal)btw_percent;
                          else
                              valueM = 0;

                  }
              }
          }
      }

      

     private void splitNew()
     {
         if (lines != null && gridItems.RowCount > 0)
         {
             translateRadMessageBox msgbox = new translateRadMessageBox();
             DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("This operation will erase all items in grid, continue ?", "Delete");
             if (dr == DialogResult.Yes)
             {

                 //=== cita projekat
                 if (txtProject2.Text != "")
                 {
                     ArrangementBUS arb = new ArrangementBUS();
                     ArrangementModel arm = new ArrangementModel();
                     arm = arb.GetArrangementByCode(txtProject2.Text);
                     if (arm != null)
                     {
                         fromdate = arm.dtFromArrangement;
                         idArange = arm.idArrangement;

                     }
                 }

                 AccAcountUpdate au = new AccAcountUpdate();
                 lines.Clear();
                 lines = new List<AccLineModel>();
                 linesmodel = null;
                 linesmodel = new AccLineModel();
                 model = null;
                 model = new AccLineModel();
                 model1 = null;
                 model1 = new AccLineModel();

                 AccAcountUpdate aUw = new AccAcountUpdate();
                 //prva - osnovna stavka  booksoort 1

                 //   linesmodel.idAccDaily = enterb.idCreditPay;
                 //if (txtBtw2.Text != "")
                 //    linesmodel.idBTW = Convert.ToInt32(txtBtw2.Text);
                 linesmodel.idClientLine = txtClient2.Text;
                 linesmodel.periodLine = au.Period(Convert.ToDateTime(dpDate2.Text));
                 linesmodel.incopNr = txtInkop2.Text;
                 linesmodel.invoiceNr = txtInvoiceNr2.Text;
                 linesmodel.numberLedAccount = masterAccount;
                 linesmodel.descLedgerAccount = aUw.AccountName(masterAccount);
                 if (Convert.ToDecimal(txtAmount2Credit.Text) != 0)
                     linesmodel.creditLine = Convert.ToDecimal(txtAmount2Credit.Text);
                 if (Convert.ToDecimal(txtAmount2Debit.Text) != 0)
                     linesmodel.debitLine = Convert.ToDecimal(txtAmount2Debit.Text);
                 linesmodel.descLine = txtDescription2.Text;
                 linesmodel.dtLine = Convert.ToDateTime(dpDate2.Text);
                 linesmodel.dtBooking = Convert.ToDateTime(dpValuta2.Text);
                 linesmodel.idCostLine = txtCost2.Text;
                 linesmodel.idProjectLine = txtProject2.Text;
                 linesmodel.booksort = 1;
                 linesmodel.idMaster = txtInkop2.Text + 1;
                 lines.Add(linesmodel);

                 linesmodel = new AccLineModel();

                 linesmodel.idClientLine = txtClient2.Text;
                 linesmodel.periodLine = au.Period(Convert.ToDateTime(fromdate));
                 if (linesmodel.periodLine == -1)
                     au.Period(Convert.ToDateTime(dpValuta2.Text));
                 linesmodel.incopNr = txtInkop2.Text;
                 linesmodel.invoiceNr = txtInvoiceNr2.Text;
                 linesmodel.numberLedAccount = "";
                 if (Convert.ToDecimal(txtAmount2Credit.Text) != 0)
                     linesmodel.debitLine = Convert.ToDecimal(txtAmount2Credit.Text);
                 if (Convert.ToDecimal(txtAmount2Debit.Text) != 0)
                     linesmodel.creditLine = Convert.ToDecimal(txtAmount2Debit.Text);
                 linesmodel.descLine = txtDescription2.Text;
                 if (txtProject2.Text != "")
                     linesmodel.dtLine = fromdate;//Convert.ToDateTime(dpDate2.Text);
                 else
                     linesmodel.dtLine = Convert.ToDateTime(dpDate2.Text);
                 linesmodel.dtBooking = Convert.ToDateTime(dpValuta2.Text);
                 linesmodel.idCostLine = txtCost2.Text;
                 linesmodel.idProjectLine = txtProject2.Text;
                 linesmodel.booksort = 2;

                 linesmodel.idMaster = txtInkop2.Text + 2;
                 lines.Add(linesmodel);


                 gridItems.DataSource = null;
                 gridItems.DataSource = lines;
                 gridItems.ReadOnly = false;
                 gridItems.ClearSelection();
                 gridItems.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect;
                 int rw = lines.Count;
                 if (rw >= 1)
                 {
                     gridItems.Rows[rw - 1].Cells["numberLedAccount"].IsSelected = true;
                     gridItems.Rows[rw - 1].Cells["numberLedAccount"].BeginEdit();
                 }
             }
         }
         else
         {
                 AccAcountUpdate au = new AccAcountUpdate();
                 lines = new List<AccLineModel>();
                 linesmodel = null;
                 linesmodel = new AccLineModel();
                 model = null;
                 model = new AccLineModel();
                 model1 = null;
                 model1 = new AccLineModel();

                 AccAcountUpdate aUw = new AccAcountUpdate();
                 //prva - osnovna stavka  booksoort 1

                 //   linesmodel.idAccDaily = enterb.idCreditPay;
                 //if (txtBtw2.Text != "")
                 //    linesmodel.idBTW = Convert.ToInt32(txtBtw2.Text);
                 linesmodel.idClientLine = txtClient2.Text;
                 linesmodel.periodLine = au.Period(Convert.ToDateTime(dpDate2.Text));
                 linesmodel.incopNr = txtInkop2.Text;
                 linesmodel.invoiceNr = txtInvoiceNr2.Text;
                 linesmodel.numberLedAccount = masterAccount;
                 linesmodel.descLedgerAccount = aUw.AccountName(masterAccount);
                 if (Convert.ToDecimal(txtAmount2Credit.Text) != 0)
                     linesmodel.creditLine = Convert.ToDecimal(txtAmount2Credit.Text);
                 if (Convert.ToDecimal(txtAmount2Debit.Text) != 0)
                     linesmodel.debitLine = Convert.ToDecimal(txtAmount2Debit.Text);
                 linesmodel.descLine = txtDescription2.Text;
                 linesmodel.dtLine = Convert.ToDateTime(dpDate2.Text);
                 linesmodel.dtBooking = Convert.ToDateTime(dpValuta2.Text);
                 linesmodel.idCostLine = txtCost2.Text;
                 linesmodel.idProjectLine = txtProject2.Text;
                 linesmodel.booksort = 1;
                 linesmodel.idMaster = txtInkop2.Text + 1;
                 lines.Add(linesmodel);

                 linesmodel = new AccLineModel();

                 linesmodel.idClientLine = txtClient2.Text;
                 linesmodel.periodLine = au.Period(Convert.ToDateTime(fromdate));
                 if (linesmodel.periodLine == -1)
                     au.Period(Convert.ToDateTime(dpValuta2.Text));
                 linesmodel.incopNr = txtInkop2.Text;
                 linesmodel.invoiceNr = txtInvoiceNr2.Text;
                 linesmodel.numberLedAccount = "";
                 if (Convert.ToDecimal(txtAmount2Credit.Text) != 0)
                     linesmodel.debitLine = Convert.ToDecimal(txtAmount2Credit.Text);
                 if (Convert.ToDecimal(txtAmount2Debit.Text) != 0)
                     linesmodel.creditLine = Convert.ToDecimal(txtAmount2Debit.Text);
                 linesmodel.descLine = txtDescription2.Text;
                 if (txtProject2.Text != "")
                     linesmodel.dtLine = fromdate;//Convert.ToDateTime(dpDate2.Text);
                 else
                     linesmodel.dtLine = Convert.ToDateTime(dpDate2.Text);
                 linesmodel.dtBooking = Convert.ToDateTime(dpValuta2.Text);
                 linesmodel.idCostLine = txtCost2.Text;
                 linesmodel.idProjectLine = txtProject2.Text;
                 linesmodel.booksort = 2;

                 linesmodel.idMaster = txtInkop2.Text + 2;
                 lines.Add(linesmodel);


                 gridItems.DataSource = null;
                 gridItems.DataSource = lines;
                 gridItems.ReadOnly = false;
                 gridItems.ClearSelection();
                 gridItems.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect;
                 int rw = lines.Count;
                 if (rw >= 1)
                 {
                     gridItems.Rows[rw - 1].Cells["numberLedAccount"].IsSelected = true;
                     gridItems.Rows[rw - 1].Cells["numberLedAccount"].BeginEdit();
                 }
             //}
         }
     }
      private decimal getPercent(int btw)
      {
          
          string xcodeBTW = "";
          int btwtype = 0;
          xBtwConto = "";
          btw_percent = 0;
          int gridBtw;
          inc_excl = 0;
          gridBtw = btw;
          if (gridBtw != 0)  //==== uzima procenat ===
          {
              AccTaxBUS atb = new AccTaxBUS();
              AccTaxModel atm = new AccTaxModel();
              atm = atb.GetTaxByID(gridBtw);
              if (atm != null)
              {
                  xcodeBTW = atm.codeTax;
                  btwtype = Convert.ToInt32(atm.typeTax);
                  inc_excl = Convert.ToInt32(atm.typeTax);
                  xBtwConto = atm.numberLedAccount;
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
                  else
                  {
                      btw_percent = 0;
                  }
              }
              else
              {
                  btw_percent = 0;
              }


          }
          else
          {
              btw_percent = 0;
          }
          return btw_percent;                      
    
      }
      private string getBTWKonto(int btw)
      {

          string xcodeBTW = "";
          int btwtype = 0;
          xBtwConto = "";
          btw_percent = 0;
          int gridBtw;
          inc_excl = 0;
          gridBtw = btw;
          if (gridBtw != 0)  //==== uzima procenat ===
          {
              AccTaxBUS atb = new AccTaxBUS();
              AccTaxModel atm = new AccTaxModel();
              atm = atb.GetTaxByID(gridBtw);
              if (atm != null)
              {
                  xcodeBTW = atm.codeTax;
                  btwtype = Convert.ToInt32(atm.typeTax);
                  inc_excl = Convert.ToInt32(atm.typeTax);
                  xBtwConto = atm.numberLedAccount;
              }

          }
          return xBtwConto;

      }

     private void btnEnter_Click(object sender, EventArgs e)
     {
         if (txtClient2.Text == "")
         {
             translateRadMessageBox msg = new translateRadMessageBox();
             msg.translateAllMessageBox("Client is mandatory");

             //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
             //{
             //    if (resxSet.GetString("Client is mandatory") != null)
             //        RadMessageBox.Show(resxSet.GetString("Client is mandatory"));
             //    else
             //        RadMessageBox.Show("Client is mandatory");
             //}
             txtClient2.Focus();
             return;
         }
         if (txtInvoiceNr2.Text == "")
         {
             translateRadMessageBox msg = new translateRadMessageBox();
             msg.translateAllMessageBox("Invoice is mandatory");

             //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
             //{
             //    if (resxSet.GetString("Invoice is mandatory") != null)
             //        RadMessageBox.Show(resxSet.GetString("Invoice is mandatory"));
             //    else
             //        RadMessageBox.Show("Invoice is mandatory");
             //}
             txtInvoiceNr2.Focus();
             return;
         }
         if (Convert.ToDecimal(txtAmount2Credit.Value) == 0 && Convert.ToDecimal(txtAmount2Debit.Value) == 0)
         {
             translateRadMessageBox msg = new translateRadMessageBox();
             msg.translateAllMessageBox("Amount is mandatory");

             //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
             //{
             //    if (resxSet.GetString("Amount is mandatory") != null)
             //        RadMessageBox.Show(resxSet.GetString("Amount is mandatory"));
             //    else
             //        RadMessageBox.Show("Amount is mandatory");
             //}
             txtAmount2Credit.Focus();
             return;
         }
         if (txtIban2.Text == "")
         {
             translateRadMessageBox msg = new translateRadMessageBox();
             msg.translateAllMessageBox("Iban is mandatory");

             //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
             //{
             //    if (resxSet.GetString("Iban is mandatory") != null)
             //        RadMessageBox.Show(resxSet.GetString("Iban is mandatory"));
             //    else
             //        RadMessageBox.Show("Iban is mandatory");
             //}
             txtInvoiceNr2.Focus();
             return;
         }

         gridItems.ReadOnly = false;

         right_panel_change = true;
         if (lines != null)
             lines.Clear();
         lines = new List<AccLineModel>();

         splitNew();
        // gridItems.Focus();
     }

    private void addLine1(decimal amt)
     {
         linesmodel = new AccLineModel();

         linesmodel.dtLine = Convert.ToDateTime(dpDate2.Text);  // prva stavka 1610 sa datumom projekta
         linesmodel.invoiceNr = txtInvoiceNr2.Text;
         linesmodel.numberLedAccount = reserve_acc;
         linesmodel.descLine = txtDescription2.Text;
         linesmodel.idClientLine = txtClient2.Text;
         linesmodel.idBTW = 0;
         linesmodel.idCostLine = txtCost2.Text;
         linesmodel.idProjectLine = txtProject2.Text;

         linesmodel.debitLine = 0;
         linesmodel.creditLine = 0;
         if (amt < 0)
             linesmodel.creditLine = Math.Abs(amt);
         else
             linesmodel.debitLine = amt;

         linesmodel.incopNr = txtInkop2.Text;
         linesmodel.iban = txtIban2.Text;
         linesmodel.dtBooking = Convert.ToDateTime(dpValuta2.Value);
         linesmodel.booksort = lines.Count+1;

         splitlist.Add(linesmodel);
             
     }

     private void addLine2(decimal amt)
     {
         linesmodel = new AccLineModel();                // druga stavka 1610 sa ravnotezom 

         linesmodel.dtLine = Convert.ToDateTime(fromdate);
         linesmodel.invoiceNr = txtInvoiceNr2.Text;
         linesmodel.numberLedAccount = reserve_acc;
         linesmodel.descLine = txtDescription2.Text;
         linesmodel.idClientLine = txtClient2.Text;
         linesmodel.idBTW = 0;
         linesmodel.idCostLine = txtCost2.Text;
         linesmodel.idProjectLine = txtProject2.Text;

         linesmodel.debitLine = 0;
         linesmodel.creditLine = 0;
         if (amt < 0)
             linesmodel.debitLine = Math.Abs(amt);
         else
             linesmodel.creditLine = amt;

         linesmodel.incopNr = txtInkop2.Text;
         linesmodel.iban = txtIban2.Text;
         linesmodel.dtBooking = Convert.ToDateTime(dpValuta2.Text);
         linesmodel.booksort = lines.Count+1;

         splitlist.Add(linesmodel);
             
     }

     private void addBtw(decimal amt, string konto, string idMaster,string idDetail)
     {
         try
         {
             AccAcountUpdate aUr = new AccAcountUpdate();

             List<AccLineModel> listAll = new List<AccLineModel>();
             listAll = (List<AccLineModel>) gridItems.DataSource;

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
                                     gridItems.DataSource = listAll ;
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
                             gridItems.Rows[rw - 1].Cells["numberLedAccount"].IsSelected = true;
                             gridItems.Rows[rw - 1].Cells["numberLedAccount"].BeginEdit();
                         }

         }
         catch(Exception e)
         {

         }


     }
     private void btnDo_Click(object sender, EventArgs e)
     {
         splitlist = new List<AccLineModel>();

         foreach (AccLineModel itm in lines)
         {
             if (itm.booksort == 1)      //dodaje osnovnu stavku 1600
             {
                 splitlist.Add(itm);
             }
             else
             {
                 if (itm.idProjectLine == "")    
                 {
                     if (itm.dtLine.ToShortDateString() == dpDate2.Text)
                     {
                         splitlist.Add(itm);
                     }
                     else
                     {
                         
                         splitlist.Add(itm);
                         if (itm.debitLine != 0)
                             addLine1(itm.debitLine);   // salje sledecoj stavci da je u debitu
                         else
                             addLine1(-itm.creditLine);   // salje sledecoj stavci da je u creditu

                         if (itm.creditLine != 0)
                             addLine2(-itm.creditLine);
                         else
                             addLine2(itm.debitLine);
                     }
                 }
                 else
                 {

                     splitlist.Add(itm);
                     if (itm.debitLine != 0)
                         addLine1(itm.debitLine);   // salje sledecoj stavci da je u debitu
                     else
                         addLine1(-itm.creditLine);   // salje sledecoj stavci da je u creditu

                     if (itm.creditLine != 0)
                         addLine2(-itm.creditLine);
                     else
                         addLine2(itm.debitLine);
                 }
                    
                
                     
                 
             }
         }
         AccTaxBUS atb = new AccTaxBUS();
         
         decimal suma = 0;
         decimal suma1 = 0;
         xBtwConto = "";
         string xBtwConto1 = "";
         int idb = 0;
         int idb1 = 0;
         for (int i = 0; i < splitlist.Count; i++ )
         {
             if (splitlist[i].idBTW !=0)
             {
                 AccTaxModel atm = new AccTaxModel();
                 atm = atb.GetTaxByID(Convert.ToInt32(splitlist[i].idBTW));
                 if (atm != null)
                 {
                     if (xBtwConto == "")
                         xBtwConto = atm.numberLedAccount;
                     else
                     {
                         if (xBtwConto != atm.numberLedAccount)
                             xBtwConto1 = atm.numberLedAccount;
                     }


                     if (idb == 0)
                         idb = Convert.ToInt32(splitlist[i].idBTW);
                     else
                         if (idb != Convert.ToInt32(splitlist[i].idBTW))
                             idb1 = Convert.ToInt32(splitlist[i].idBTW);

                     if (idb == splitlist[i].idBTW)
                         suma = suma + Convert.ToDecimal(splitlist[i].debitBTW);
                     else
                     {
                         if (idb1 == splitlist[i].idBTW)
                             suma1 = suma1 + Convert.ToDecimal(splitlist[i].debitBTW);
                     }
                 }
                 else
                 {
                     translateRadMessageBox msg = new translateRadMessageBox();
                     msg.translateAllMessageBox("BTW problem !!");

                    // RadMessageBox.Show("BTW problem !!");
                 }

             }
         }
         //if (suma != 0)
         //     addBtw(suma, xBtwConto,splitlist[i].idSuperior);
         //if (suma1 != 0)
         //    addBtw(suma1, xBtwConto1, splitlist[i].idSuperior);
         

             gridItems.DataSource = null;
             gridItems.DataSource = splitlist;
         

     }

     private void gridItems_CellValidating(object sender, CellValidatingEventArgs e)
     {
         if (e.Row != null)
         {
             if (e.Column != null)
             {
                 switch (e.Column.Name)
                 {

                     case "numberLedAccount":



                         e.Row.ErrorText = string.Empty;
                         RadTextBoxEditor tbEditor = e.ActiveEditor as RadTextBoxEditor;
                         RadTextBoxEditorElement editor = e.ActiveEditor as RadTextBoxEditorElement;

                         if (tbEditor != null && e.Column.Name == "numberLedAccount" && tbEditor.Value + "" == string.Empty && e.Row.Tag == null)
                         {
                             e.Row.ErrorText = "Empty value is not allowed!";
                             e.Cancel = true;
                             //if (_gridEditor != null)
                             //{
                             //    RadItem element = _gridEditor.EditorElement as RadItem;
                             //    element.KeyDown -= cellEditorAccount_KeyDown;
                             //}
                             //_gridEditor = null;
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
                                        // RadMessageBox.Show("Wrong account");
                                         //=====================================================

                                         LedgerAccountBUS ccentar3 = new LedgerAccountBUS(Login._bookyear);
                                         List<IModel> gmX3 = new List<IModel>();

                                         gmX3 = ccentar3.GetAllAccounts();
                                         var dlgSave3 = new GridLookupForm(gmX3, "Ledger");

                                         if (dlgSave3.ShowDialog(this) == DialogResult.Yes)
                                         {
                                             LedgerAccountModel genmX3 = new LedgerAccountModel();
                                             genmX3 = (LedgerAccountModel)dlgSave3.selectedRow;
                                             //set textbox
                                             if (genmX3.btwId != null && genmX3.btwId != 0)
                                                 e.Row.Cells["idBTW"].Value = genmX3.btwId;
                                               tbEditor.Value = genmX3.numberLedgerAccount;
                                      
                                         }
                                         //======================================================
                                        // e.Row.ErrorText = "Non extisting account";
                                         e.Cancel = true;

                                         GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                                         endEdit(cell);
                                     
                                     }
                                     else
                                     {
                                         e.Row.Cells["descLedgerAccount"].Value = lam.descLedgerAccount;
                                         if (lam.btwId != null && lam.btwId != 0)
                                             e.Row.Cells["idBTW"].Value = lam.btwId;

                                        // e.Cancel = true;

                                         GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                                         endEdit(cell);
                                     }
                                    
                                
                                 }
                             }
                         }

                         break;

                     case "invoiceNr":


                         e.Row.ErrorText = string.Empty;
                         RadTextBoxEditor tbEditor1 = e.ActiveEditor as RadTextBoxEditor;
                         if (tbEditor1 != null && e.Column.Name == "invoiceNr" && tbEditor1.Value + "" == string.Empty && e.Row.Tag == null)
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
                                 if (txtInvoiceNr2.Text != s)   //tbEditor1.Value.ToString())
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

                     case "idClientLine":
                         e.Row.ErrorText = string.Empty;
                         RadTextBoxEditor tbEditor2 = e.ActiveEditor as RadTextBoxEditor;
                         if (tbEditor2 != null && e.Column.Name == "idClientLine" && tbEditor2.Value + "" == string.Empty && e.Row.Tag == null)
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
                             if (txtClient2.Text != tbEditor2.Value.ToString() && e.Column.Name == "idClientLine")
                             {
                                 translateRadMessageBox msg = new translateRadMessageBox();
                                 msg.translateAllMessageBox("Not the same Client number");

                                // RadMessageBox.Show("Not the same Client number");
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

                     case "idBTW":
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
                                     translateRadMessageBox msg = new translateRadMessageBox();
                                     msg.translateAllMessageBox("Wrong Btw number");
                                    // RadMessageBox.Show("Wrong BTW ");

                                     e.Row.ErrorText = "Non extisting BTW";
                                     e.Cancel = true;

                                     GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                                     endEdit(cell);
                                 }
                             }
                         }
                         break;

                     case "idProjectLine":
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
                                     translateRadMessageBox msg = new translateRadMessageBox();
                                     msg.translateAllMessageBox("Wrong project code");
                                   //  RadMessageBox.Show("Wrong project code ");

                                     e.Row.ErrorText = "Not valid project ";
                                     e.Cancel = true;

                                     GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                                     endEdit(cell);
                                 }
                             }

                             e.Cancel = false;

                             GridViewCellInfo cell1 = e.Row.Cells[e.Column.Index];

                             endEdit(cell1);
                             SendKeys.Send("{ESC}");

                         }


                         break;

                     default:

                         break;

                 }
             }

         }
    }


     private void gridItems_SelectionChanged(object sender, EventArgs e)
     {
         
         if(gridItems.CurrentCell!=null)
         {
             if (gridItems.CurrentCell.ColumnInfo.Name == "idProjectLine")
            {
                 cellForKeyDown = gridItems.CurrentRow.Cells["idBTW"];
             }

         }

     }

     private void gridItems_UserAddedRow(object sender, GridViewRowEventArgs e)
     {
         AccLineModel acn = new AccLineModel();
         if (e.Row != null)
         {
             acn = (AccLineModel)e.Row.DataBoundItem;
            

             AccTaxBUS tb = new AccTaxBUS();
             AccTaxModel tm = new AccTaxModel();
             if (Convert.ToInt32(acn.idBTW) != null && Convert.ToInt32(acn.idBTW) !=0 )
                 tm = tb.GetTaxByID(Convert.ToInt32(acn.idBTW));
             if (Convert.ToInt32(acn.idBTW) != 0)
             if (tm != null)
                 addBtw(acn.debitBTW, tm.numberLedAccount, acn.idMaster, acn.idMaster);
         }
     }


     private void frmCreditPayVer2_FormClosing(object sender, FormClosingEventArgs e)
     {
         translateRadMessageBox msgbox = new translateRadMessageBox();
         DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("Do you want to Save ?", "Save");
        //    DialogResult dr = RadMessageBox.Show("Do you want to Save ?", "Save", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                btnSave2.PerformClick();

            }
           
            e.Cancel = false;
 
     }

     private void dpDate2_Leave(object sender, EventArgs e)
     {
         //  right_panel_change = true;
         if (enterb != null && isEdit2 == true)
         {
             if (Convert.ToDateTime(dpDate2.Text).ToShortDateString() != Convert.ToDateTime(enterb.dtItem).ToShortDateString())
             {
                 translateRadMessageBox msgbox = new translateRadMessageBox();
                 DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("You change the date !! Do You want to change dates in grid?", "Change");
               //  DialogResult dr = RadMessageBox.Show("You change the date !! Do You want to change dates in grid?", "Save", MessageBoxButtons.YesNo);

                 if (dr == DialogResult.Yes)
                 {
                     if (lines != null)
                         for (int i = 0; i < lines.Count; i++)
                         {
                             if (lines[i].dtLine.ToShortDateString() == Convert.ToDateTime(enterb.dtItem).ToShortDateString())
                                 lines[i].dtLine = dpDate2.Value;
                         }

                     enterb.dtItem = Convert.ToDateTime(dpDate2.Text);
                     gridItems.DataSource = null;
                     gridItems.DataSource = lines;
                 }
                 else
                 {
                     dpDate2.Text = enterb.dtItem.ToString();
                 }
             }
         }
         if (isNew2 == true) // && enterb != null)
             if (lines != null && lines.Count > 0)
             {
                 DateTime crack = new DateTime();
                 crack = lines[0].dtLine;
                 if (Convert.ToDateTime(dpDate2.Text).ToShortDateString() != Convert.ToDateTime(lines[0].dtLine).ToShortDateString())
                 {
                     translateRadMessageBox msgbox = new translateRadMessageBox();
                     DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("You change the date !! Do You want to change dates in grid?", "Change");
                     //DialogResult dr = RadMessageBox.Show("You change the date !! Do You want to change dates in grid?", "Save", MessageBoxButtons.YesNo);
                     if (dr == DialogResult.Yes)
                     {
                         if (lines != null && lines.Count > 0)
                             for (int i = 0; i < lines.Count; i++)
                             {
                                 if (lines[i].dtLine.ToShortDateString() == Convert.ToDateTime(crack).ToShortDateString())
                                     lines[i].dtLine = dpDate2.Value;
                             }


                         gridItems.DataSource = null;
                         gridItems.DataSource = lines;
                     }
                     else
                     {
                         dpDate2.Value = Convert.ToDateTime(crack);
                     }
                 }
             } 
             
     }

     private void txtAmount2Credit_Leave(object sender, EventArgs e)
     {
      
     }

     private void txtAmount2Debit_Leave(object sender, EventArgs e)
     {
      
     }

     private void txtAmount2Credit_KeyUp(object sender, KeyEventArgs e)
     {
         if (e.KeyValue == 46 || e.KeyValue == 110)
         {
             var textBoxItem = this.txtAmount2Credit.MaskedEditBoxElement.TextBoxItem;

             int indexOfDecimalSeparator = this.txtAmount2Credit.MaskedEditBoxElement.TextBoxItem.Text.ToLower().IndexOf(',');
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

     private void txtAmount2Debit_KeyUp(object sender, KeyEventArgs e)
     {
         if (e.KeyValue == 46 || e.KeyValue == 110)
         {
             var textBoxItem = this.txtAmount2Debit.MaskedEditBoxElement.TextBoxItem;

             int indexOfDecimalSeparator = this.txtAmount2Debit.MaskedEditBoxElement.TextBoxItem.Text.ToLower().IndexOf(',');
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

     private void gridBookPay_CurrentRowChanging(object sender, CurrentRowChangingEventArgs e)
     {
         if (e.CurrentRow != null && e.CurrentRow.DataBoundItem != null)
         {
             AccCreditPayModel selectedRowCompare = (AccCreditPayModel)e.CurrentRow.DataBoundItem;

            if (selectedRowCompare.accNumber != txtClient2.Text)
                  right_panel_change = true;

             if (selectedRowCompare.cost != txtCost2.Text)
                  right_panel_change = true;

              if (selectedRowCompare.dtItem != Convert.ToDateTime(dpDate2.Value))
                  right_panel_change = true;

             if (selectedRowCompare.descItem != txtDescription2.Text)
                  right_panel_change = true;

              if (selectedRowCompare.amountC != Convert.ToDecimal(txtAmount2Credit.Value))
                  right_panel_change = true;

                if (selectedRowCompare.amountD != Convert.ToDecimal(txtAmount2Debit.Value))
                  right_panel_change = true;

                if (selectedRowCompare.project != txtProject2.Text)
                  right_panel_change = true;

             //  if (selectedRowCompare.payIban != txtIban2.Text)
             //     right_panel_change = true;

             if (selectedRowCompare.invoiceNr != txtInvoiceNr2.Text)
                  right_panel_change = true;

             if (selectedRowCompare.dtValuta != Convert.ToDateTime(dpValuta2.Value))
                  right_panel_change = true;


         }



        // RadMessageBox.Show("Before row changing");  // Kad menja red u levom gridu;
         if (right_panel_change == true)
         {
             translateRadMessageBox msgbox = new translateRadMessageBox();
             DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("Do you want to SAVE your changes ?", "Save");
            // DialogResult dr = RadMessageBox.Show("Do you want to SAVE your changes ?", "Save", MessageBoxButtons.YesNo);
             if (dr == DialogResult.Yes)
             {
                 btnSave2.PerformClick();
             }
             else
             {
                 right_panel_change = false;
             }
         }
     }

     private void gridBookPay_MouseClick(object sender, MouseEventArgs e)
     {
         ////RadMessageBox.Show("Mouse");   // kad klikne misem na isti red u levom gridu
         //DialogResult dr = RadMessageBox.Show("Do you want to SAVE your changes ?", "Save",  MessageBoxButtons.YesNo);
         //if (dr == DialogResult.Yes)
         //{
         //    btnSave2.PerformClick();
         //}

     }

     private void txtInvoiceNr2_TextChanged(object sender, EventArgs e)
     {
         //right_panel_change = true;
     }

     private void txtClient2_TextChanged(object sender, EventArgs e)
     {
         //right_panel_change = true;
     }

     private void txtDescription2_TextChanged(object sender, EventArgs e)
     {
       //right_panel_change = true;
     }

     private void txtCost2_TextChanged(object sender, EventArgs e)
     {

     }

     
        
    }

    
}
