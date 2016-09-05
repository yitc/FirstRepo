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

namespace GUI
{
    public partial class frmDaily : frmTemplate
    {

        private int idDay = -1;
        public AccDailyModel model;
        public LedgerAccountModel modeLedger;
        public AccLedgerClassModel modelClass;
        public bool modelChanged = false;
        List<AccDailyTypeModel> dltype;
        List<AccDailyVerInModel> dclass;
        private string Namef;
        private int createUser;

        public frmDaily()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Daily");
            }
            this.Text = "";
            ribbonExampleMenu.Text = "";
            this.Text = Login._companyModelList[0].nameCompany + " " + Login._bookyear + " - " + Namef;
            InitializeComponent();
        }

        public frmDaily(AccDailyModel dailymodel)
        {
            model = dailymodel;
            // heder forme
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Daily");
            }
            ribbonExampleMenu.Text = "";
           // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + Login._bookyear + " - " + Namef;
            
            if (model.idDaily.ToString() != "")
                idDay = model.idDaily;
            InitializeComponent();
        }

        private void frmDaily_Load(object sender, EventArgs e)
        {
            radRibbonMemo.Visibility = ElementVisibility.Collapsed;
            btnReport.Visibility = ElementVisibility.Collapsed;
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


            //this.txtCodDaily.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            //this.txtCodDaily.Mask = "######";
           
           // this.txtCodDaily.PromptChar =MaskFormat = exl
             setTranslation();

            // Punjenje combo-a
            AccDailyTypeBUS tb1 = new AccDailyTypeBUS();
            dltype = tb1.GetAllTypes();

            ddlDailytype.DataSource = dltype;
            ddlDailytype.DisplayMember = "descDailyType";
            ddlDailytype.ValueMember = "idDailyType";


            AccDailyVerInBUS tb2 = new AccDailyVerInBUS();
            dclass = tb2.GetAllClass();

            ddlVerIn.DataSource = dclass;
            ddlVerIn.DisplayMember = "nameDailyVerIn";
            ddlVerIn.ValueMember = "idDailyVerIn";

            txtCodDaily.MaskedEditBoxElement.EnableMouseWheel = false;
            
            // Provera da li ima knjizenja -- ako ima zabraniti edit svih polja sem naziva

            if (idDay != -1)
            {
                txtIdDaily.Text = model.idDaily.ToString();
                //if (model.codeDaily != null)                          // skinuto i prebaceno da se puni idDaily
                //   txtDaily.Text = model.codeDaily.ToString();
                txtCodDaily.Text = model.idDaily.ToString();

                if (model.userCreated != 0)
                    createUser = model.userCreated;
                //================================================
                if (model.descDaily != null)
                   txtDailyName.Text = model.descDaily.ToString();
                if (model.ibanBank != null)
                    txtIban.Text = model.ibanBank.ToString();
                //if (model.isLocked == true)
                //    chkLocked.Checked = true;
                if (model.idDailyType != 0 && model.idDailyType != null)
                {
                    ddlDailytype.SelectedItem = ddlDailytype.Items[dltype.FindIndex(item => item.idDailyType == model.idDailyType)];
                    if (model.idDailyType == 2 || model.idDailyType == 3 || model.idDailyType == 4)
                    {
                        if (model.idDailyType == 2)
                        {
                            lblAutomatic.Text = "Credit pay";
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {
                                if (resxSet.GetString(lblAutomatic.Text) != null)
                                    lblCounter.Text = resxSet.GetString(lblCounter.Text);
                                chkBegin.Visible = false;
                                lblBegin.Visible = false;
                            }
                        }
                        else
                        {
                            if (model.idDailyType == 3)
                            {
                                lblAutomatic.Text = "Invoicing";
                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                {
                                    if (resxSet.GetString(lblAutomatic.Text) != null)
                                        lblCounter.Text = resxSet.GetString(lblCounter.Text);
                                }
                                chkBegin.Visible = false;
                                lblBegin.Visible = false;
                            }
                            else
                            {
                                if (model.idDailyType == 4)
                                {
                                    lblAutomatic.Text = "Memorial SEPA";
                                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                    {
                                        if (resxSet.GetString(lblAutomatic.Text) != null)
                                            lblCounter.Text = resxSet.GetString(lblCounter.Text);
                                    }
                                    chkBegin.Visible = true;
                                    lblBegin.Visible = true;
                                }
                            }
                        }
                        chkAutomatic.Visible = true;
                        lblAutomatic.Visible = true;
                        if (model.automaticBook == true)
                        {
                            chkAutomatic.CheckState = CheckState.Checked;
                            chkAutomatic.Enabled = false;
                        }
                        else
                            chkAutomatic.CheckState = CheckState.Unchecked;
                    }
                    else
                    {
                        chkAutomatic.Visible = false;
                        lblAutomatic.Visible = false;
                    }

                }

                AccAcountUpdate up = new AccAcountUpdate();
                txtUserCreated.Text = up.getUsername(model.userCreated);
                dpCreatdDate.Text = model.dtCreated.ToString();
                txtUserModified.Text = up.getUsername(model.userModified);
                dpModifiedDate.Text = model.dtModified.ToString();

                //if ( model.idDailyVerIn != null)
                //    ddlVerIn.SelectedItem = ddlVerIn.Items[dclass.FindIndex(item => item.idDailyVerIn == model.idDailyVerIn)];
               // model.idDailyVerIn != 0 &&
                LedgerAccountBUS lb = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lm = new LedgerAccountModel();
                if (model.numberLedgerAccount != null && model.numberLedgerAccount != null)
                {
                    lm = lb.GetAccount(model.numberLedgerAccount, Login._bookyear);
                    txtAccount.Text = lm.numberLedgerAccount + " " + lm.descLedgerAccount;
                }
                if (model.isUseCounter == true)
                    chkUseCounter.Checked = true;
                else
                    chkUseCounter.Checked = false;

                if (model.bookingYear != null && model.bookingYear != "")
                    txtBookYear.Text = model.bookingYear;

                if (model.inkop != null && model.inkop != 0)
                {
                    if (chkUseCounter.Checked)
                    {
                        AccLineBUS gn = new AccLineBUS(Login._bookyear);
                        IdModel nid = new IdModel();
                        int idDaily = 0;
                        int xDaily = model.idDaily;
                        string yearId = Login._bookyear;       //DateTime.Now.Year.ToString();
                        if (xDaily != -1)
                            idDaily = xDaily;
                        nid = gn.GetIncopView(yearId, xDaily);
                        var result = nid.idNumber.ToString().PadLeft(6, '0');
                        var aa = nid.idDaily.ToString().PadRight(6, '0');
                        string SubString = nid.yearId.Substring(yearId.Length - 2);
                        txtYear.Text = SubString;
                        txtDailyCount.Text = aa;
                        //  txtInkop.Text = SubString + aa + result;
                      //  txtIncop.Text = nid.idNumber.ToString();
                        txtIncop.Text = result.ToString(); //model.inkop.ToString();
                        txtIncop.ReadOnly = true;
                    }
                }
                AccLineBUS alb = new AccLineBUS(Login._bookyear);
                List<AccLineModel> alm = new List<AccLineModel>();
                alm = alb.GetAllLinesByDaily(model.idDaily,0);
                if (alm != null)
                {
                    chkUseCounter.Enabled = false;
                    txtIncop.Enabled = false;
                    ddlDailytype.Enabled = false;
                }
                // ovde treba da dodje lookup za banku
                //LedgerAccountBUS lb = new LedgerAccountBUS(Login._bookyear);
                //LedgerAccountModel lm = new LedgerAccountModel();
                //if (model.numberLedgerAccount != null && model.numberLedgerAccount != null)
                //{
                //    lm = lb.GetAccount(model.numberLedgerAccount);
                //    txtAccount.Text = lm.numberLedgerAccount + " " + lm.descLedgerAccount;
                //}
                if (model.idDailyType == 4)
                {
                    chkBegin.Visible = true;
                    lblBegin.Visible = true;

                    if (model.beginPeriod == true)
                    {
                        chkBegin.Enabled = false;
                        chkBegin.CheckState = CheckState.Checked;
                       
                    }
                }

             
            }
            else
            {
                model = new AccDailyModel();
                model.bookingYear = Login._bookyear;
                txtBookYear.Text = Login._bookyear;
                model.userCreated = Login._user.idUser;
            }
       
        }

        private void setTranslation()
        {

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                lblIdDaily.Text = resxSet.GetString("Id Daily");
                if (resxSet.GetString(lblDaily.Text) != null)
                    lblDaily.Text = resxSet.GetString(lblDaily.Text);
               // lblDaily.Text = resxSet.GetString("Daily code");
                lblDescription.Text = resxSet.GetString("Description");
                lblDailyType.Text = resxSet.GetString("Daily type");
                lblAccount.Text = resxSet.GetString("Account");
                lblBank.Text = resxSet.GetString("Bank");
                lblIban.Text = resxSet.GetString("IBAN");
                lblLock.Text = resxSet.GetString("Locked");
                lblInkVer.Text = resxSet.GetString("Sales/Purchase");
                //tabCard.Text = resxSet.GetString("Daily page");
                tabGeneral.Text = resxSet.GetString("General");
                lblDescription.Text = resxSet.GetString("Description");

                if (resxSet.GetString(lblIncopUse.Text) != null)
                    lblIncopUse.Text = resxSet.GetString(lblIncopUse.Text);
                if (resxSet.GetString(lblCounter.Text) != null)
                    lblCounter.Text = resxSet.GetString(lblCounter.Text);
                if (resxSet.GetString(lblBegin.Text) != null)
                    lblBegin.Text = resxSet.GetString(lblBegin.Text);
                if (resxSet.GetString(lblCreated.Text) != null)
                    lblCreated.Text = resxSet.GetString(lblCreated.Text);
                if (resxSet.GetString(lblModified.Text) != null)
                    lblModified.Text = resxSet.GetString(lblModified.Text);
               

                btnSave.Text = resxSet.GetString("Save");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtCodDaily.Text == "")
                {
                    RadMessageBox.Show("Enter a Daily code, please!");
                    return;
                }
                else
                {
                    if (idDay == -1)
                    {
                        AccDailyBUS acdbus = new AccDailyBUS(Login._bookyear);
                        AccDailyModel acm = new AccDailyModel();
                        acm = acdbus.GetDailysByCode(txtCodDaily.Text);
                        if (acm != null)
                        {
                            RadMessageBox.Show("Daily code, exist, enter new one, please!");
                            return;
                        }
                    }
                }

                //if (chkLocked.Checked == true)
                //{
                //    model.isLocked = true;
                //}
                //else
                //{
                //    model.isLocked = false;
                //}
                model.codeDaily = txtCodDaily.Text.Replace("_","");
                if (txtCodDaily.Text != "")
                model.idDaily = Convert.ToInt32(txtCodDaily.Text.Replace("_",""));              
                if ((txtDailyName.Text).ToString() != "")
                    model.descDaily = txtDailyName.Text;
                if ((txtBank.Text).ToString() != "")
                    model.idBank = Convert.ToInt32(txtBank.Text);
               // if ((txtIban.Text).ToString() != "")
                MakeInvoice cc = new MakeInvoice();
                bool yesno = false;
                string rac = "";
                rac = txtIban.Text;
                if (rac != "")
                {
                    yesno = cc.ValidateIban(rac);
                    if (yesno == true)
                        model.ibanBank = txtIban.Text;   
                    else
                    {
                        RadMessageBox.Show("IBAN  NOT CORRECT ... Account data not be saved !!!!");
                        txtIban.Focus();
                        return;
                    }
                }

                if (chkUseCounter.CheckState == CheckState.Checked)
                    model.isUseCounter = true;
                else
                    model.isUseCounter = false;
                if (model.isUseCounter == true)
                    model.inkop = Convert.ToInt32(txtIncop.Value);

                    //model.ibanBank = txtIban.Text;
                if (txtAccount.Text == "")
                    model.numberLedgerAccount = txtAccount.Text;
                if (Convert.ToInt32(ddlDailytype.SelectedValue) != 4)
                {
                    if (model.numberLedgerAccount == "")
                    {
                        RadMessageBox.Show("Can't save without account !!");
                        return;
                    }
                }
                model.idDailyType = Convert.ToInt32(ddlDailytype.SelectedValue);
                if (Convert.ToInt32(ddlDailytype.SelectedValue) == 2 || Convert.ToInt32(ddlDailytype.SelectedValue) == 3 || Convert.ToInt32(ddlDailytype.SelectedValue) == 4)
                {
                    if (chkAutomatic.CheckState == CheckState.Checked)
                        model.automaticBook = true;
                    else
                        model.automaticBook = false;

                }
                if (Convert.ToInt32(ddlDailytype.SelectedValue) == 4 )
                {
                    if (chkBegin.CheckState == CheckState.Checked)
                        model.beginPeriod = true;
                    else
                        model.beginPeriod = false;
                }

               
                //int a = Convert.ToInt32(ddlVerIn.SelectedValue);
                //if ( a == 0)
                //{
                //    model.idDailyVerIn = 0;
                //}
                //else
                //{
                //    model.idDailyVerIn = Convert.ToInt32(ddlVerIn.SelectedValue);
                //}
                //if (model.idDailyVerIn == null)
                //    model.idDailyVerIn = 0;
                AccDailyBUS bus = new AccDailyBUS(Login._bookyear);
                if (idDay != -1)
                {
                   // model.idDaily = idDay;
                    model.userModified = Login._user.idUser;
                    if (dpCreatdDate.Text != "")
                        model.dtCreated = Convert.ToDateTime(dpCreatdDate.Text);
                    if (createUser != 0)
                        model.userCreated = Convert.ToInt32(createUser);

                    bus.Update(model, this.Name, Login._user.idUser);
                }
                else
                {
                    model.userCreated = Login._user.idUser;
                    bus.Save(model, this.Name, Login._user.idUser);
                    if (model.isUseCounter == true)
                    makeCounter();            // pravi brojac za nalog

                }
                modelChanged = true;
                this.Close();
            }
            catch (Exception ex)
            {
                RadMessageBox.Show("Error saving.\nMessage: " + ex.Message, "Save", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS accBUS = new LedgerAccountBUS(Login._bookyear);
            List<IModel> am = new List<IModel>();

            am = accBUS.GetAllAccounts();


            var dlgClient = new GridLookupForm(am, "Ledger");

            if (dlgClient.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel okm = new LedgerAccountModel();
                okm = (LedgerAccountModel)dlgClient.selectedRow;
                txtAccount.Text = okm.numberLedgerAccount + " " + okm.descLedgerAccount;
                model.numberLedgerAccount = okm.numberLedgerAccount;
              
            }

        }

        private void btnBank_Click(object sender, EventArgs e)  // ovde je lookup za banku
        {

        }
        private void makeCounter()
        {
            string year = Login._bookyear;//DateTime.Now.Year.ToString();
            int idDaily = model.idDaily;
            AccLineBUS acc = new AccLineBUS(Login._bookyear);
            acc.MakeCounter(year, idDaily, Convert.ToInt32(txtIncop.Value));


        }

        private void chkUseCounter_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkUseCounter.CheckState == CheckState.Checked)
            {
                if (txtCodDaily.Text != "0")
                {
                    int idDaily = 0;
                    int xDaily = Convert.ToInt32(txtCodDaily.Value);
                    string yearId = Login._bookyear;       //DateTime.Now.Year.ToString();
                    var aa = xDaily.ToString().PadRight(6, '0');
                    string SubString = yearId.Substring(yearId.Length - 2);
                    txtYear.Text = SubString;
                    txtDailyCount.Text = aa;
                    txtIncop.Text = model.inkop.ToString();
                  
                }
            }
            else
            {
                txtDailyCount.Text = "";
                txtYear.Text = "";
                txtIncop.Text = "0";
            }
        }

        private void txtAccount_KeyDown(object sender, KeyEventArgs e)
        {
            LedgerAccountBUS ab = new LedgerAccountBUS(Login._bookyear);
            LedgerAccountModel am = new LedgerAccountModel();
            
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtAccount.Text != "" && txtAccount.TextLength > 0)
                {
                    string aa = "";
                    
                    aa = txtAccount.Text.PadLeft(6);
                    am = ab.GetAccount(aa.Trim(), Login._bookyear);
                    if (am != null)
                    {
                        txtAccount.Text = am.numberLedgerAccount + "    " + am.descLedgerAccount;
                        model.numberLedgerAccount = am.numberLedgerAccount;
                    }
                    else
                    {
                        RadMessageBox.Show("Wrong account");
                        txtAccount.Focus();

                    }
                }
                else
                    txtAccount.Text = "";

            }
        }

        private void ddlDailytype_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if ((ddlDailytype.SelectedIndex+1) == 2)
            {
                lblAutomatic.Text = "Credit pay";
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(lblAutomatic.Text) != null)
                        lblCounter.Text = resxSet.GetString(lblCounter.Text);
                }
                chkAutomatic.Visible = true;
                lblAutomatic.Visible = true;
                lblBegin.Visible = false;
                chkBegin.Visible = false;
            }
            else
            {
                if ((ddlDailytype.SelectedIndex+1) == 3)
                {
                    lblAutomatic.Text = "Invoicing";
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString(lblAutomatic.Text) != null)
                            lblCounter.Text = resxSet.GetString(lblCounter.Text);
                    }
                    chkAutomatic.Visible = true;
                    lblAutomatic.Visible = true;
                    lblBegin.Visible = false;
                    chkBegin.Visible = false;
                }
                else
                {
                    if ((ddlDailytype.SelectedIndex + 1) == 4)
                    {
                        lblAutomatic.Text = "Memorial SEPA";
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString(lblAutomatic.Text) != null)
                                lblCounter.Text = resxSet.GetString(lblCounter.Text);
                        }
                        lblBegin.Visible = true;
                        chkBegin.Visible = true;
                    }
                }
            }

      //      if ((ddlDailytype.SelectedIndex + 1) == 2 || (ddlDailytype.SelectedIndex + 1) == 3 || (ddlDailytype.SelectedIndex + 1) == 4)
            if ((ddlDailytype.SelectedIndex + 1) == 1 || (ddlDailytype.SelectedIndex + 1) == 5)
            //{
            //    chkAutomatic.Visible = true;
            //    lblAutomatic.Visible = true;
               
            //}
            //else
            {
                chkAutomatic.Visible = false;
                lblAutomatic.Visible = false;
                lblBegin.Visible = false;
                chkBegin.Visible = false;
            }
          
        }

        private void chkAutomatic_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (idDay == -1)
            {
                if (chkAutomatic.CheckState == CheckState.Checked)
                {

                    AccDailyBUS adb = new AccDailyBUS(Login._bookyear);
                    List<IModel> adm = new List<IModel>();
                    int bk = ddlDailytype.SelectedIndex + 1;
                    if (bk == 2)
                        adm = adb.GetBookingDailys();
                    else
                        if (bk == 3)
                            adm = adb.GetBookingDailysInkop();
                        else
                            if (bk == 4)
                                adm = adb.GetBookingDailysMemo();

                    if (adm != null && adm.Count > 0)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("Alredy exist that daily book !! ") != null)
                                RadMessageBox.Show(resxSet.GetString("Alredy exist that daily book !! "));
                            else
                                RadMessageBox.Show("Alredy exist that daily book !! ");
                        }

                        chkAutomatic.CheckState = CheckState.Unchecked;
                        return;
                    }
                }
              
            }
            else
            {
                if (chkAutomatic.Enabled == true)
                {
                    if (chkAutomatic.CheckState == CheckState.Checked )
                    {

                        AccDailyBUS adb = new AccDailyBUS(Login._bookyear);
                        List<IModel> adm = new List<IModel>();
                        int bk = ddlDailytype.SelectedIndex + 1;
                        if (bk == 2)
                            adm = adb.GetBookingDailysInkop();   
                        else
                            if (bk == 3)
                                adm = adb.GetBookingDailys();
                            else
                                if (bk == 4)
                                    adm = adb.GetBookingDailysMemo();

                        if (adm != null && adm.Count > 0)
                        {
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {
                                if (resxSet.GetString("Alredy exist that daily book !! ") != null)
                                    RadMessageBox.Show(resxSet.GetString("Alredy exist that daily book !! "));
                                else
                                    RadMessageBox.Show("Alredy exist that daily book !! ");
                            }
                            if (model != null)
                            {
                                if (model.automaticBook == true)
                                    chkAutomatic.Enabled = false;             //CheckState = CheckState.Unchecked;
                                else
                                    chkAutomatic.CheckState = CheckState.Unchecked;
                            }
                            return;
                        }
                    }
                }
            }
        }

        private void chkBegin_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (idDay == -1)
            {
                if (chkBegin.CheckState == CheckState.Checked)
                {
                    AccDailyBUS adb = new AccDailyBUS(Login._bookyear);
                    List<IModel> adm = new List<IModel>();
                    int bk = ddlDailytype.SelectedIndex + 1;
                    if (bk == 4)
                        adm = adb.GetMemoBeginPeriod();
                    if (adm != null && adm.Count > 0)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("Alredy exist Memoriaal Begin period !! ") != null)
                                RadMessageBox.Show(resxSet.GetString("Alredy exist Memoriaal Begin period !! "));
                            else
                                RadMessageBox.Show("Alredy exist Memoriaal Begin period !! ");
                        }

                        chkBegin.CheckState = CheckState.Unchecked;
                        return;
                    }
                }
            }
            else
            {
                if (chkBegin.CheckState == CheckState.Checked && chkBegin.Enabled == true)
                {
                    AccDailyBUS adb = new AccDailyBUS(Login._bookyear);
                    List<IModel> adm = new List<IModel>();
                    int bk = ddlDailytype.SelectedIndex + 1;
                    if (bk == 4)
                        adm = adb.GetMemoBeginPeriod();
                    if (adm != null && adm.Count > 0)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("Alredy exist Memoriaal Begin period !! ") != null)
                                RadMessageBox.Show(resxSet.GetString("Alredy exist Memoriaal Begin period !! "));
                            else
                                RadMessageBox.Show("Alredy exist Memoriaal Begin period !! ");
                        }
                        chkBegin.CheckState = CheckState.Unchecked;
                        chkBegin.Enabled = false;

                        return;
                    }
                }
            }
        }

        private void txtCodDaily_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Up || e.KeyCode==Keys.Down)
            {
                e.Handled = true;
            }
        }
 

           
       
        
    }
}
