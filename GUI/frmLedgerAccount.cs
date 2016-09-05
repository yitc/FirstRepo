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
using System.IO;

namespace GUI
{
    public partial class frmLedgerAccount : frmTemplate
    {
        private int iLedger = -1;
        public LedgerAccountModel model;
        public AccLedgerAmountsModel model_amount;
        public AccCostModel modelCst;
        public AccLedgerClassModel modelClass;
        public string xidCostCenar;
        public int xidClass1;
        public int xidClass2;
        public int xidClass3;
        public int xidClass4;
        public int xidClass5;
        public bool modelChanged = false;
        List<LedgerAccountModel> ledgermodel;
        public string Namef;
        private string layoutTrans;
        

        public frmLedgerAccount()

        {
            iLedger = -1;
            LedgerAccountModel model = new LedgerAccountModel();
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Ledger");
            }
            ribbonExampleMenu.Text = "";
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            this.Icon = Login.iconForm;
            InitializeComponent();
        }
        public frmLedgerAccount(LedgerAccountModel modelLedger)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Ledger");
            }
            ribbonExampleMenu.Text = "";
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            this.Icon = Login.iconForm;
            LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
            model = new LedgerAccountModel();
            model = lab.GetAccount(modelLedger.numberLedgerAccount,Login._bookyear);
            if (model == null)
            {
                RadMessageBox.Show("Error reading data !!!");
                return;
            }
           // model = modelLedger;
            if (model.idLedgerAccount.ToString() != "")
                   iLedger = model.idLedgerAccount;
            InitializeComponent();
           
        }

        private void frmLedgerAccount_Load(object sender, EventArgs e)
        {

        }

        private void frmLedgerAccount_Load_1(object sender, EventArgs e)
        {

            radRibbonMemo.Visibility = ElementVisibility.Collapsed;
            btnReport.Visibility = ElementVisibility.Collapsed;
            btnEmail.Visibility = ElementVisibility.Collapsed;
            btnWord.Visibility = ElementVisibility.Collapsed;
            radRibbonReports.Visibility = ElementVisibility.Collapsed;
            btnNewMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteDoc.Visibility = ElementVisibility.Visible;
            btnNewDoc.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;
            btnDeleteDoc.Visibility = ElementVisibility.Collapsed;
            radRibbonDocuments.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;
            btnNewContract.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupContracts.Visibility = ElementVisibility.Collapsed;

            layoutTrans = MainForm.gridFiltersFolder + "\\layoutLedger.xml";

            
            setTranslation();

            if (iLedger != -1)
            {
                txtDescription.Text = model.descLedgerAccount.ToString();
                txtAccount.Text = model.numberLedgerAccount.ToString();
                AccLedgerAmountsBUS amtb = new AccLedgerAmountsBUS();
                AccLedgerAmountsModel amtm = new AccLedgerAmountsModel();
                if (txtAccount.Text != "")
                {
                    amtm = amtb.GetAmountPerYear(model.numberLedgerAccount, Login._bookyear);
                    if (amtm != null)
                    {
                        this.txtOpenCredit.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
                        this.txtOpenCredit.Mask = "N2";
                        txtOpenCredit.Text = amtm.beginCredit.ToString();

                        this.txtOpenDebit.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
                        this.txtOpenDebit.Mask = "N2";
                        txtOpenDebit.Text = amtm.beginDebit.ToString();

                        this.txtDebitAmount.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
                        this.txtDebitAmount.Mask = "N2";
                        txtDebitAmount.Text = amtm.debitAmount.ToString();

                        this.txtCreditAmount.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
                        this.txtCreditAmount.Mask = "N2";
                        txtCreditAmount.Text = amtm.creditAmount.ToString();

                        this.txtCalcBalance.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
                        this.txtCalcBalance.Mask = "N2";

                        decimal rez=0;
                        if (amtm.debitAmount != null && amtm.creditAmount != null)
                        rez = Convert.ToDecimal(amtm.debitAmount - amtm.creditAmount);
                        txtCalcBalance.Text = rez.ToString();

                        txtTransactions.Text = model.transactionNoAccount.ToString();
                    }
                }
       
                txtClass1.Text = model.class1Account.ToString();
                txtClass2.Text = model.class2Account.ToString();
                txtBtwId.Text = model.btwId.ToString();
                xidClass1 = model.class1Account;
                xidClass2 = model.class2Account;
                xidClass3 = model.class3Account;
                xidClass4 = model.class4Account;
                xidClass5 = model.class5Account;
                xidCostCenar = model.idCostCenter;
                if (model.accountTypeAccount == 1)
                {
                    
                    ddlAcctype.Text = "Balans";
                }
                else
                {
                   
                    ddlAcctype.Text = "Winst Verlies";
                }
                if (model.sideBooking == "D")
                {

                    ddlBookSide.Text = "Debit";
                    
                }
                else
                {
                    if (model.sideBooking == "C")
                    {
                        ddlBookSide.Text = "Credit";
                    }
                    else
                    {
                        ddlBookSide.Text = "";
                    }
                }
                
                AccCostBUS bcst = new AccCostBUS();
                if (model.idCostCenter != null)
                {
                    if (model.idCostCenter != null)
                    {
                        //   modelCst = new AccCostBUS().GetCostByID(Convert.ToInt32(model.idCostCenter));
                        modelCst = bcst.GetCostByID(model.idCostCenter);
                        txtCostCentar.Text = modelCst.descCost;
                    }
                }
                AccLedgerClassBUS aclass = new  AccLedgerClassBUS();
                if (model.class1Account != null)
                {
                    if (model.class1Account != 0)
                    {
                        AccLedgerClassModel modelClass = null;
                        modelClass = aclass.GetClassById(Convert.ToInt32(model.class1Account));
                        txtClass1.Text = modelClass.descClass;
                    }
                    else
                    {
                        txtClass1.Text = "";
                    }
                }
                AccLedgerClassBUS aclass1 = new AccLedgerClassBUS();
                if (model.class2Account != null)
                {
                    if (model.class2Account != 0)
                    {
                        AccLedgerClassModel modelClass1 = null;
                        modelClass1 = aclass1.GetClassById(Convert.ToInt32(model.class2Account));
                        txtClass2.Text = modelClass1.descClass;
                    }
                    else
                    {
                        txtClass2.Text = "";
                    }
                }
                AccLedgerClassBUS aclass2 = new AccLedgerClassBUS();
                if (model.class3Account != null)
                {
                    if (model.class3Account != 0)
                    {
                        AccLedgerClassModel modelClass2 = null;
                        modelClass2 = aclass2.GetClassById(Convert.ToInt32(model.class3Account));
                        txtClass3.Text = modelClass2.descClass;
                    }
                    else
                    {
                        txtClass3.Text = "";
                    }
                }
                AccLedgerClassBUS aclass3 = new AccLedgerClassBUS();
                if (model.class4Account != null)
                {
                    if (model.class4Account != 0)
                    {
                        AccLedgerClassModel modelClass3 = null;
                        modelClass3 = aclass3.GetClassById(Convert.ToInt32(model.class4Account));
                        txtClass4.Text = modelClass3.descClass;
                    }
                    else
                    {
                        txtClass4.Text = "";
                    }
                }
                AccLedgerClassBUS aclass4 = new AccLedgerClassBUS();
                if (model.class5Account != null)
                {
                    if (model.class5Account != 0)
                    {
                        AccLedgerClassModel modelClass4 = null;
                        modelClass4 = aclass4.GetClassById(Convert.ToInt32(model.class5Account));
                        txtClass5.Text = modelClass4.descClass;
                    }
                    else
                    {
                        txtClass5.Text = "";
                    }
                }
                


                //Decimal rez = 0;
                //rez = model.debitAccount - Math.Abs(model.creditAccount);
                //txtCalcBalance.Text = rez.ToString();
                if (model.mandatoryCostAccount == true)
                    chkCost.Checked = true;
                if (model.mandatoryDebitorAccount == true)
                    chkDebitor.Checked = true;
                if (model.mandatoryCreditorAccount == true)
                    chkCreditor.Checked = true;
                if (model.isBudgetAccount == true)
                    chkBudget.Checked = true;
                if (model.mandatoryProjectAccount == true)
                    chkProject.Checked = true;
                if (model.isBTWLedgerAccount == true)
                    chkBtw.Checked = true;
                if (model.isActiveLedgerAccount == true)
                    chkActive.Checked = true;
                if (model.sideBooking != null)
                    txtSideBooking.Text = model.sideBooking;
                if (model.isBlockMemorial == true)
                    chkBlockMemo.Checked = true;

               

                //// totals
                AccLineDAO acd = new AccLineDAO(Login._bookyear);
                // za debit i credit iznos
                string xConto = txtAccount.Text;
                //object adeb = acd.GetSumDebit(xConto);
                //object acre = acd.GetSumCredit(xConto);
                //object atrans = acd.GetSumTrans(xConto);
                //if (adeb != null)
                //    txtDebitAmount.Text = Convert.ToDecimal(adeb).ToString();
                //else
                //    txtDebitAmount.Text = "0";
                //if (acre != null)
                //    txtCreditAmount.Text = Convert.ToDecimal(acre).ToString();
                //else
                //    txtCreditAmount.Text = "0";
                //if (adeb != null || acre != null)
                //{
                //    txtTransactions.Text = Convert.ToDecimal(atrans).ToString();
                //    txtCalcBalance.Text = (Convert.ToDecimal(txtDebitAmount.Text) - Convert.ToDecimal(txtCreditAmount.Text)).ToString();
                //}
                //===============
                // za Begin debit i credit
                object adebBegin = acd.GetSumDebitBegin(xConto);
                object acreBegin = acd.GetSumCreditBegin(xConto);
                if (adebBegin != null)
                    txtOpenDebit.Text = Convert.ToDecimal(adebBegin).ToString();
                if (acreBegin != null)
                    txtOpenCredit.Text = Convert.ToDecimal(acreBegin).ToString();


                AccLineBUS alb = new AccLineBUS(Login._bookyear);
                List<AccLineModel> alm = new List<AccLineModel>();
                alm = alb.GetLinesByAccount(model.numberLedgerAccount, 0);
                rgvTrans.DataSource = null;
                rgvTrans.DataSource = alm;
                rgvTrans.Show();

            }
            else
            {
                ddlAcctype.Text = "Balans";

            }
        }

        private void setTranslation()
        {

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                lblAccount.Text = resxSet.GetString("Account");
                lblDescription.Text = resxSet.GetString("Name");
                lblAcctype.Text = resxSet.GetString("Account type");
                lblCostCentar.Text = resxSet.GetString("Cost center");
                lblOpenDebit.Text = resxSet.GetString("Open debit amount");
                lblOpenCredit.Text = resxSet.GetString("Open credit amount");
                lblClass1.Text = resxSet.GetString("Classification 1");
                lblClass2.Text = resxSet.GetString("Classification 2");
                lblClass3.Text = resxSet.GetString("Classification 3");
                lblClass4.Text = resxSet.GetString("Classification 4");
                lblClass5.Text = resxSet.GetString("Classification 5");

                lblValutaDeb.Text = resxSet.GetString("Valuta debit");
                lblValutaCre.Text = resxSet.GetString("Valuta credit");
             
                lblDebit.Text = resxSet.GetString("Debit amount");
                lblCredit.Text = resxSet.GetString("Credit amount");
                lblBalance.Text = resxSet.GetString("Balance");
                lblTransactions.Text = resxSet.GetString("Transaction count");
                chkCost.Text = resxSet.GetString("Cost center mandatory");
                chkDebitor.Text = resxSet.GetString("Debitor mandatory");
                chkCreditor.Text = resxSet.GetString("Creditor mandatory");
                chkProject.Text = resxSet.GetString("Project mandatory");
                chkBtw.Text = resxSet.GetString("BTW");
                chkActive.Text = resxSet.GetString("Is Active");
                chkBudget.Text = resxSet.GetString("Budget account");
                btnSave.Text = resxSet.GetString("Save");
                lblBookside.Text = resxSet.GetString("Booking side D/C");
                chkBlockMemo.Text = resxSet.GetString("Block memorial booking");
                rpvLedger.Text = resxSet.GetString("General");
                rpvLedgerCard.Text = resxSet.GetString("LedgerCard");
                lblOpenDebit.Text = resxSet.GetString("Begin Debit amount");
                lblOpenCredit.Text = resxSet.GetString("Begin Credit amount");
            }
        }

        private void fillDataLedger()
        {
          //  model = new LedgerAccountModel();
            model.numberLedgerAccount = txtAccount.Text;
            model.descLedgerAccount = txtDescription.Text;
            if (chkCost.Checked == true) 
            {
                model.mandatoryCostAccount = true;
            }
            else
            {
                model.mandatoryCostAccount = false;
            }
            if (chkDebitor.Checked == true)
            {
                model.mandatoryDebitorAccount = true;
            }
            else
            {
                model.mandatoryDebitorAccount = false;
            }
            if (chkCreditor.Checked == true)
            {
                model.mandatoryCreditorAccount = true;
            }
            else
            {
                model.mandatoryCreditorAccount = false;
            }
            if (chkProject.Checked == true)
            {
                model.mandatoryProjectAccount = true;
            }
            else
            {
                model.mandatoryProjectAccount = false;
            }
            if (chkBudget.Checked == true)
            {
                model.isBudgetAccount = true;
            }
            else
            {
                model.isBudgetAccount = false;
            }

            if (chkBtw.Checked == true)
            {
                model.isBTWLedgerAccount = true;
            }
            else
            {
                model.isBTWLedgerAccount = false;
            }
            if (chkActive.Checked == true)
            {
                model.isActiveLedgerAccount = true;
            }
            else
            {
                model.isActiveLedgerAccount = false;
            }

            if (chkBlockMemo.Checked == true)
            {
                model.isBlockMemorial = true;
            }
            else
            {
                model.isBlockMemorial = false;
            }



            if (txtClass1.Text != "")
            {
                model.class1Account = xidClass1;
            }
            else
            {
                model.class1Account = 0;
            }
            if (txtClass2.Text != "")
            {
                model.class2Account = xidClass2;
            }
            else
            {
                model.class2Account = 0;
            }
            if (txtClass3.Text != "")
            {
                model.class3Account = xidClass3;
            }
            else
            {
                model.class3Account = 0;
            }
            if (txtClass4.Text != "")
            {
                model.class4Account = xidClass4;
            }
            else
            {
                model.class4Account = 0;
            }
            if (txtClass5.Text != "")
            {
                model.class5Account = xidClass5;
            }
            else
            {
                model.class5Account = 0;
            }
            if (ddlAcctype.Text == "Balans")
            {
                model.accountTypeAccount = 1;
            }
            else
            {
                model.accountTypeAccount = 2;
            }
            //if (ddlBookSide.Text == "Debit")
            //{
            //    model.sideBooking = "D";
            //}
            //else
            //{
            //    if (ddlBookSide.Text == "Credit")
            //         model.sideBooking = "C";
            //}

            if (txtCostCentar.Text != "")
            {
                model.idCostCenter = xidCostCenar;
            }
            //if(txtSideBooking.Text != "")
            //model.sideBooking = txtSideBooking.Text.ToUpper();
            //if (Person.idTitle != 0)
            //    ddlTitle.SelectedItem = ddlTitle.Items[persTitle.FindIndex(item => item.idTitle == Person.idTitle)];

            
            if (ddlBookSide.SelectedIndex == 0 )
                                                        //if (ddlBookSide.Text == "Debit")
            {
                model.sideBooking = "D";
            }
            else
                if(ddlBookSide.SelectedIndex == 1)
            {
                model.sideBooking = "C";
            }
                else
                {
                    model.sideBooking = "";
                }
            if (chkBlockMemo.Checked == true)
            {
                model.isBlockMemorial = true;
            }
            else
            {
                model.isBlockMemorial = false;
            }

            if (txtOpenCredit.Text != "")
                model.openCreditAccount = Convert.ToDecimal(txtOpenCredit.Text);
            if (txtOpenDebit.Text != "")
                model.openDebitAccount = Convert.ToDecimal(txtOpenDebit.Text);
            if (txtDebitAmount.Text != "")
                model.debitAccount = Convert.ToDecimal(txtDebitAmount.Text);
            if (txtCreditAmount.Text != "")
                model.creditAccount = Convert.ToDecimal(txtCreditAmount.Text);
            if (txtTransactions.Text != "")
                model.transactionNoAccount = Convert.ToInt32(txtTransactions.Text);
            if (txtBtwId.Text != "")
                model.btwId = Convert.ToInt32(txtBtwId.Text);
            else
                model.btwId = 0;
            model.bookingYear = Login._bookyear;
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
               // 
                if(txtAccount.Text == "")
                {
                    RadMessageBox.Show("You didn't enter Account !!!");
                    txtAccount.Focus();
                    return;
                }
             
               
                   
                
                //if (txtSideBooking.Text == "")
                //{
                //    RadMessageBox.Show("Enter a Booking side, please!");
                //    return;
                //}
                //else
                //{
                //    if (txtSideBooking.Text.ToUpper() != "D" && txtSideBooking.Text.ToUpper() != "C")
                //    {
                //        RadMessageBox.Show("Only D or C allowed, correct field value please!");
                //        return;
                //    }
                //}

                LedgerAccountBUS bus = new LedgerAccountBUS(Login._bookyear);
                if (iLedger != -1)
                {
                    fillDataLedger();
                    model.idLedgerAccount = iLedger;
                    bus.Update(model, this.Name, Login._user.idUser);
                }
                else
                {
                    if (txtAccount.Text != "")
                    {
                        LedgerAccountModel lamod = new LedgerAccountModel();
                        lamod = new LedgerAccountBUS(Login._bookyear).GetAccount(txtAccount.Text, Login._bookyear);
                        if (lamod != null)
                        {
                            RadMessageBox.Show("Alredy exist account !");
                            txtAccount.Focus();
                            return;
                        }
                    }
                    model = new LedgerAccountModel();
                    fillDataLedger();
                  //  model.openCreditAccount = 0;
                  //  model.openDebitAccount = 0;
                 //   model.creditAccount = 0;
                  //  model.debitAccount = 0;
                 //   model.transactionNoAccount = 0;
                   // model.accountTypeAccount = 1;


                    bus.Save(model, this.Name, Login._user.idUser);

                }  
                modelChanged = true;
                this.Close();
            }
            catch (Exception ex)
            {
                RadMessageBox.Show("Error saving.\nMessage: " + ex.Message, "Save", MessageBoxButtons.OK, RadMessageIcon.Error);
            }

           
     

        }

        private void btnCostSentar_Click(object sender, EventArgs e)
        {
           
            AccCostBUS ccentar = new AccCostBUS();
            List<IModel> gmX = new List<IModel>();

            gmX = ccentar.GetAllCost();
            var dlgSave = new GridLookupForm(gmX, "Type");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                AccCostModel genmX = new AccCostModel();
                genmX = (AccCostModel)dlgSave.selectedRow;
                //set textbox
                txtCostCentar.Text = genmX.descCost;
                if (model != null)
                {
                    model.idCostCenter = genmX.codeCost;
                }
                xidCostCenar = genmX.codeCost;
               
            }
        }

        private void btnClass1_Click(object sender, EventArgs e)
        {
            AccLedgerClassBUS cclass1 = new AccLedgerClassBUS();
            List<IModel> gm1 = new List<IModel>();

             gm1 = cclass1.GetCostByLevel(1);
           
            var dlgSave = new GridLookupForm(gm1, "Class");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                AccLedgerClassModel genm1 = new AccLedgerClassModel();
                genm1 = (AccLedgerClassModel)dlgSave.selectedRow;
                //set textbox
                txtClass1.Text = genm1.descClass;
                if (model != null)
                {
                    model.class1Account = genm1.idClass;
                    xidClass1 = genm1.idClass;
                }
                

            }
        }

        private void btnClass2_Click(object sender, EventArgs e)
        {
            AccLedgerClassBUS cclass2 = new AccLedgerClassBUS();
            List<IModel> gm2 = new List<IModel>();

            gm2 = cclass2.GetCostByLevel(2);

            var dlgSave = new GridLookupForm(gm2, "Class");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                AccLedgerClassModel genm2 = new AccLedgerClassModel();
                genm2 = (AccLedgerClassModel)dlgSave.selectedRow;
                //set textbox
                txtClass2.Text = genm2.descClass;
                if (model != null)
                {
                    model.class2Account = genm2.idClass;
                    xidClass2 = genm2.idClass;
                }
                

            }
        }

        private void btnClass3_Click(object sender, EventArgs e)
        {
            AccLedgerClassBUS cclass3 = new AccLedgerClassBUS();
            List<IModel> gm3 = new List<IModel>();

            gm3 = cclass3.GetCostByLevel(3);

            var dlgSave = new GridLookupForm(gm3, "Class");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                AccLedgerClassModel genm3 = new AccLedgerClassModel();
                genm3 = (AccLedgerClassModel)dlgSave.selectedRow;
                //set textbox
                txtClass3.Text = genm3.descClass;
                if (model != null)
                {
                    model.class3Account = genm3.idClass;
                    xidClass3 = genm3.idClass;
                }
                //xidClass3 = genm3.idClass;

            }
        }

        private void btnClass4_Click(object sender, EventArgs e)
        {
            AccLedgerClassBUS cclass4 = new AccLedgerClassBUS();
            List<IModel> gm4 = new List<IModel>();

            gm4 = cclass4.GetCostByLevel(4);

            var dlgSave = new GridLookupForm(gm4, "Class");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                AccLedgerClassModel genm4 = new AccLedgerClassModel();
                genm4 = (AccLedgerClassModel)dlgSave.selectedRow;
                //set textbox
                txtClass4.Text = genm4.descClass;
                if (model != null)
                {
                    model.class4Account = genm4.idClass;
                    xidClass4 = genm4.idClass;
                }
               

            }
        }

        private void btnClass5_Click(object sender, EventArgs e)
        {
            AccLedgerClassBUS cclass5 = new AccLedgerClassBUS();
            List<IModel> gm5 = new List<IModel>();

            gm5 = cclass5.GetCostByLevel(5);

            var dlgSave = new GridLookupForm(gm5, "Class");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                AccLedgerClassModel genm5 = new AccLedgerClassModel();
                genm5 = (AccLedgerClassModel)dlgSave.selectedRow;
                //set textbox
                txtClass5.Text = genm5.descClass;
                if (model != null)
                {
                    model.class5Account = genm5.idClass;
                    xidClass5 = genm5.idClass;
                }
               

            }
        }

        private void rgvTrans_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvTrans.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvTrans.Columns[i].HeaderText != null && resxSet.GetString(rgvTrans.Columns[i].HeaderText) != null)
                        rgvTrans.Columns[i].HeaderText = resxSet.GetString(rgvTrans.Columns[i].HeaderText);
                }
            }
            if (File.Exists(layoutTrans))
            {
                rgvTrans.LoadLayout(layoutTrans);
            }
        }

        private void radMenuItem1_Click(object sender, EventArgs e) /// los
        {
            if (File.Exists(layoutTrans))
            {
                File.Delete(layoutTrans);
            }
            rgvTrans.SaveLayout(layoutTrans);

            MessageBox.Show("Layout Saved");
        }

        private void SaveLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutTrans))
            {
                File.Delete(layoutTrans);
            }
            rgvTrans.SaveLayout(layoutTrans);

            MessageBox.Show("Layout Saved");
        }

      

        private void rgvTrans_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridSummaryCellElement)
            {
                if (!String.IsNullOrEmpty(e.CellElement.Text))
                {
                    e.CellElement.BorderLeftWidth = e.CellElement.BorderRightWidth = 1;
                    e.CellElement.Font =  new Font("Verdana", 9,FontStyle.Bold);
                    e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                    
                   
              

                }
                else
                {
                    e.CellElement.ResetValue(LightVisualElement.BorderLeftWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
                    e.CellElement.ResetValue(LightVisualElement.BorderRightWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
                }
            }
        }

        private void rgvTrans_ViewRowFormatting(object sender, RowFormattingEventArgs e)
        {

        }

        private void rgvTrans_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            int iID = Int32.Parse(rgvTrans.SelectedRows[0].Cells["idAccLine"].Value.ToString());
            int idDaily = Int32.Parse(rgvTrans.SelectedRows[0].Cells["idAccDaily"].Value.ToString());
        //    int idType = Convert.ToInt32(model.idDailyType);

        }

        private void btnBtw_Click(object sender, EventArgs e)
        {
            if (chkBtw.CheckState == CheckState.Checked)
            {
                AccTaxBUS cclass1 = new AccTaxBUS();
                List<IModel> gm1 = new List<IModel>();

                gm1 = cclass1.GetAllTax(Login._user.lngUser);

                var dlgSave = new GridLookupForm(gm1, "BTW");

                if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                {
                    AccTaxModel genm1 = new AccTaxModel();
                    genm1 = (AccTaxModel)dlgSave.selectedRow;
                    //set textbox
                    txtBtwId.Text = genm1.idTax.ToString();

                }
            }
            else
            {
                RadMessageBox.Show("Check BTW, please");
            }
        }

        private void txtAccount_Leave(object sender, EventArgs e)
        {
         
        }

        private void chkBtw_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkBtw.CheckState == CheckState.Unchecked)
                txtBtwId.Text = "";
        }

  

    }
}
