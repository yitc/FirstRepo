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
using System.IO;

namespace GUI
{
    public partial class frmAccSettings : frmTemplate
    {
        int iID = -1;
        public string Namef;
        public AccSettingsModel model;
        public bool modelChanged = false;
        private string xAccountSplit;
        private Boolean result = false;
        public int insLabel;
        private AccLineBUS lineBUS;
        private List<AccLineModel> lineModel;
        private int createUser;
        private int disableDel=9;
        private int year;

        public frmAccSettings(int year)
        {
            InitializeComponent();
            this.Icon = Login.iconForm;
            this.year = year;
        }

        public frmAccSettings(AccSettingsModel emodel)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Acc Settings");
            }
            ribbonExampleMenu.Text = "";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " " + Namef;
            model = emodel;
            iID = model.idSettings;
            InitializeComponent();
        }
        public frmAccSettings(AccSettingsModel emodel, int disableDel)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Acc Settings");
            }
            ribbonExampleMenu.Text = "";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " " + Namef;
            this.Icon = Login.iconForm;
            model = emodel;
            iID = model.idSettings;
            this.disableDel = disableDel;
            InitializeComponent();
        }

        private void frmAccSettings_Load(object sender, EventArgs e)
        {
            lineBUS = new AccLineBUS(Login._bookyear);
            radRibbonDocuments.Visibility = ElementVisibility.Visible;
            btnNewDoc.Visibility = ElementVisibility.Collapsed;
            btnReport.Visibility = ElementVisibility.Collapsed;
            btnEmail.Visibility = ElementVisibility.Collapsed;
            btnWord.Visibility = ElementVisibility.Collapsed;
            btnNewMemo.Visibility = ElementVisibility.Collapsed;
            if (disableDel == 9)
                btnDeleteDoc.Visibility = ElementVisibility.Visible;
            else
                btnDeleteDoc.Visibility = ElementVisibility.Collapsed;
            
            btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;
            radRibbonMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;
            radRibbonDocuments.Text = "";


            // Clean labele
            labelDefDebitorAccount.Text = "";
            labelDefCreditorAccount.Text = "";
            labelDefVatDebitor.Text = "";
            labelDefVatCreditor.Text = "";
            labelDefPayCondition.Text = "";
            labelBankCostAccount.Text = "";
            labelCurrDeferenceAccount.Text = "";
            labelPaymentDiferenceAccount.Text = "";
            labelTransferingAcc.Text = "";
            labelReservation.Text = "";
            labelFirstPayment.Text = "";
            labelReservationCost.Text = "";
            labelDebitorReservation.Text = "";
            labelSepaAcc.Text = "";
            labelDefDifferenceAcc.Text = "";
            //za ucitavanje podataka iz forme

            int Y = 2;  //
            RadCheckBox rchk = new RadCheckBox();
            Y = 0;
            for (int i = 0; i < Login._personLabels.Count; i++)
            {
                rchk = new RadCheckBox();
                rchk.Font = new Font("Verdana", 9);
               // rchk.ThemeName = pagePerson.ThemeName;
                rchk.Name = "chkLabel" + Login._personLabels[i].idLabel.ToString();
                rchk.Text = Login._personLabels[i].nameLabel;
                rchk.Location = new Point(3, Y); //0
                rchk.AutoSize = true;
                Y = Y + 4 + rchk.Height;   //3
                panelLabels.Controls.Add(rchk);
            }




            if (iID != -1)
            {
                //for (int i = 0; i < Login._personLabels.Count; i++)
                //{
                //    //RadCheckBox chk = (RadCheckBox)tabRelation.Controls.Find("chkIsMaried", true)[0];
                //  //  RadCheckBox chk = (RadCheckBox)panelLabels.Controls.Find("chkLabel" + model.labelSettings.idLabel//Login._personLabels[i].idLabel.ToString(), true)[0];
                //    RadCheckBox chk = (RadCheckBox)panelLabels.Controls.Find(item => item.nameLabel.TrimEnd() == chk.Text.TrimEnd()).idLabel;
                //    if (Login._personLabels[i].idLabel.ToString() != "")
                //        insLabel = Login._personLabels[i].idLabel;
                //    //CheckBox chk = (CheckBox)  this.Controls.fFindControl(this, "btn3");
                //    chk.CheckState = CheckState.Checked;
                //}

                AccLineBUS al = new AccLineBUS(Login._bookyear);
                List<AccLineModel> am = new List<AccLineModel>();

                am = al.GetAllLinesYear(model.yearSettings.ToString());
                if (am != null && am.Count > 0)
                {
                    ddlNoPeriod.Enabled = false;
                    txtBeginBookYear.Enabled = false;
                    txtEndBookYear.Enabled = false;
                    btnDeleteDoc.Visibility = ElementVisibility.Hidden;
                   
                }
                if (model.userCreated != null)
                    createUser = model.userCreated;
                txtYearSettings.Enabled = false;

                //txtIDSettings.Text = model.idSettings.ToString();
                txtYearSettings.Text = model.yearSettings.ToString();
                ddlNoPeriod.Text = model.noPeriods.ToString();
                txtBeginBookYear.Text = model.beginBookYear.ToString();
                txtEndBookYear.Text = model.endBookYear.ToString();
                txtSepaPath.Text = model.sepaPath;
                lineModel = lineBUS.GetLinesByOnlyAccount(txtDefCreditorAccount.Text);
                if (lineModel != null)
                {
                    ddlNoPeriod.Enabled = false;
                }
                if (model.isVat == true)
                {
                    chkIsVat.CheckState = CheckState.Checked;
                }
                txtmyBic.Text = model.myBic;
                txtmyIban.Text = model.myIban;
                // txtDefDebitorAccount.Text = model.defCreditorAccount.ToString();

                txtDefDebitorAccount.Text = model.defDebitorAccount.ToString();
                //za citanje labela prilikom duplog klika na gridview (upisuje string u labelu za vrednost koja je izabrana 
                if (txtDefDebitorAccount.Text != "")
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = lab.GetAccount(txtDefDebitorAccount.Text, Login._bookyear);
                    if (lam != null)
                    {
                        labelDefDebitorAccount.Text = lam.descLedgerAccount;
                    }
                }

                txtDefCreditorAccount.Text = model.defCreditorAccount.ToString();
                if (txtDefCreditorAccount.Text != "")
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = lab.GetAccount(txtDefCreditorAccount.Text, Login._bookyear);
                    if (lam != null)
                    {
                        labelDefCreditorAccount.Text = lam.descLedgerAccount;
                    }
                    //lineModel = lineBUS.GetLinesByOnlyAccount(txtDefCreditorAccount.Text);
                    //if (lineModel != null)
                    //{
                    //    txtDefCreditorAccount.ReadOnly = true;
                    //    btnDefCreditorAccount.Enabled = false;
                    //}
                }

                txtDefVatDebitor.Text = model.defVatDebitor.ToString();
                if (txtDefVatDebitor.Text != "")
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = lab.GetAccount(txtDefVatDebitor.Text, Login._bookyear);
                    if (lam != null)
                    {
                        labelDefVatDebitor.Text = lam.descLedgerAccount;
                    }
                }

                txtDefVatCreditor.Text = model.defVatCreditor.ToString();
                if (txtDefVatCreditor.Text != "")
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = lab.GetAccount(txtDefVatCreditor.Text, Login._bookyear);
                    if (lam != null)
                    {
                        labelDefVatCreditor.Text = lam.descLedgerAccount;
                    }
                }

                txtCurrDeferenceAccount.Text = model.currDeferenceAccount.ToString();
                if (txtCurrDeferenceAccount.Text != "")
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = lab.GetAccount(txtCurrDeferenceAccount.Text, Login._bookyear);
                    if (lam != null)
                    {
                        labelCurrDeferenceAccount.Text = lam.descLedgerAccount;
                    }
                }

                txtPaymentDiferenceAccount.Text = model.paymentDiferenceAccount.ToString();
                if (txtPaymentDiferenceAccount.Text != "")
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = lab.GetAccount(txtPaymentDiferenceAccount.Text, Login._bookyear);
                    if (lam != null)
                    {
                        labelPaymentDiferenceAccount.Text = lam.descLedgerAccount;
                    }
                }

                txtBankCostAccount.Text = model.bankCostAccount.ToString();
                if (txtBankCostAccount.Text != "")
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = lab.GetAccount(txtBankCostAccount.Text, Login._bookyear);
                    if (lam != null)
                    {
                        labelBankCostAccount.Text = lam.descLedgerAccount;
                    }
                }
                if (model.defPayCondition != null && model.defPayCondition != 0)
                {
                    txtDefPayCondition.Text = model.defPayCondition.ToString();
                    AccPaymentBUS ccentar = new AccPaymentBUS();
                    AccPaymentModel gmX = new AccPaymentModel();

                    gmX = ccentar.GetPaymentByID(Convert.ToInt32(model.defPayCondition));
                    if (gmX != null)
                      labelDefPayCondition.Text =gmX.description;
                }

                txtNoDayFirstWarning.Text = model.noDayFrstWarning.ToString();
                txtNoDaySecondWarning.Text = model.noDaySecondWorning.ToString();
          
               //===
                txtPaketPrice.Text = model.defLedgerPrice.ToString();
                if (txtPaketPrice.Text != "")
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = lab.GetAccount(txtPaketPrice.Text, Login._bookyear);
                    if (lam != null)
                    {
                        labelPaketPrice.Text = lam.descLedgerAccount;
                    }
                }
                txtInsurance.Text = model.defLedgerIncurance.ToString();
                if (txtInsurance.Text != "")
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = lab.GetAccount(txtInsurance.Text, Login._bookyear);
                    if (lam != null)
                    {
                        labelInsurance.Text = lam.descLedgerAccount;
                    }
                }
                txtCancelInsurance.Text = model.defLedgerCancel.ToString();
                if (txtCancelInsurance.Text != "")
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = lab.GetAccount(txtCancelInsurance.Text, Login._bookyear);
                    if (lam != null)
                    {
                        labelCancelInsurance.Text = lam.descLedgerAccount;
                    }
                }
                txtCalamitait.Text = model.defLedgerCalamitu.ToString();
                if (txtCalamitait.Text != "")
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = lab.GetAccount(txtCalamitait.Text, Login._bookyear);
                    if (lam != null)
                    {
                        labelCalamitait.Text = lam.descLedgerAccount;
                    }
                }
                txtMoneyGroup.Text = model.defLedgerMoneyGr.ToString();
                if (txtMoneyGroup.Text != "")
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = lab.GetAccount(txtMoneyGroup.Text, Login._bookyear);
                    if (lam != null)
                    {
                        labelMoneyGroup.Text = lam.descLedgerAccount;
                    }
                }
                txtTransferingAcc.Text = model.defTransferingAcc.ToString();
                if (txtTransferingAcc.Text != "")
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = lab.GetAccount(txtTransferingAcc.Text, Login._bookyear);
                    if (lam != null)
                    {
                        labelTransferingAcc.Text = lam.descLedgerAccount;
                    }
                }

                txtBtwInvoicing.Text = model.defBTWinvoicing.ToString();
                if (txtBtwInvoicing.Text != "")
                {
                    AccTaxBUS lab = new AccTaxBUS();
                    AccTaxModel lam = new AccTaxModel();

                    lam = lab.GetTaxByID(Convert.ToInt32(txtBtwInvoicing.Text));
                    if (lam != null)
                    {
                        labelBtwInvoicing.Text = lam.descTax;
                    }
                }
                txtDailyInvoice.Text = model.idDailyFak.ToString();
                if (txtDailyInvoice.Text != "")
                {
                    AccDailyBUS lab = new AccDailyBUS(Login._bookyear);
                    AccDailyModel lam = new AccDailyModel();

                    lam = lab.GetDailysById(Convert.ToInt32(txtDailyInvoice.Text));
                    if (lam != null)
                    {
                        labelDailyInvoice.Text = lam.descDaily;
                    }
                }

                txtReservation.Text = model.defReservationAcc.ToString();
                if (txtReservation.Text != "")
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = lab.GetAccount(txtReservation.Text, Login._bookyear);
                    if (lam != null)
                    {
                        labelReservation.Text = lam.descLedgerAccount;
                    }
                }

                txtCancelation.Text = model.defLedgerCancelation.ToString();
                if (txtCancelation.Text != "")
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = lab.GetAccount(txtCancelation.Text, Login._bookyear);
                    if (lam != null)
                    {
                        labelCancelation.Text = lam.descLedgerAccount;
                    }
                }
                txtFirstPayment.Text = model.defFirstPayment.ToString();
                if (txtFirstPayment.Text != "")
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = lab.GetAccount(txtFirstPayment.Text, Login._bookyear);
                    if (lam != null)
                    {
                        labelFirstPayment.Text = lam.descLedgerAccount;
                    }
                }
                txtReservationCost.Text = model.defLedReservationCost.ToString();
                if (txtReservationCost.Text != "")
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = lab.GetAccount(txtReservationCost.Text, Login._bookyear);
                    if (lam != null)
                    {
                        labelReservationCost.Text = lam.descLedgerAccount;
                    }
                }
                txtDebitorReservation.Text = model.debitorReservationAccount.ToString();
                if (txtDebitorReservation.Text != "")
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = lab.GetAccount(txtDebitorReservation.Text, Login._bookyear);
                    if (lam != null)
                    {
                        labelDebitorReservation.Text = lam.descLedgerAccount;
                    }
                }
                txtSepaAcc.Text = model.defSepaAcc.ToString();
                if (txtSepaAcc.Text != "")
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = lab.GetAccount(txtSepaAcc.Text, Login._bookyear);
                    if (lam != null)
                    {
                        labelSepaAcc.Text = lam.descLedgerAccount;
                    }
                }
                txtDefDifferenceAcc.Text = model.defDifferenceAcc.ToString();
                if (txtDefDifferenceAcc.Text != "")
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = lab.GetAccount(txtDefDifferenceAcc.Text, Login._bookyear);
                    if (lam != null)
                    {
                        labelDefDifferenceAcc.Text = lam.descLedgerAccount;
                    }
                }
                //===
                AccAcountUpdate up = new AccAcountUpdate();
                txtUserCreated.Text = up.getUsername(model.userCreated);
                dpCreatdDate.Text = model.dtCreated.ToString();
                txtUserModified.Text = up.getUsername(model.userModified);
                dpModifiedDate.Text = model.dtModified.ToString();
                if (disableDel != 9) // ovde omogucava da da moze da unese novi sa kopiranim podacima
                    iID = -1;
            }
            else
            {
                AccSettingsModel model = new AccSettingsModel();
                txtYearSettings.Text = year.ToString(); //Login._bookyear;
                txtYearSettings.Enabled = false;
            }

            setTranslation();
        }

        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                //lblIDSettings.Text = resxSet.GetString("Id Settings");
                lblYearSettings.Text = resxSet.GetString("Year Settings");
                lblNoPeriod.Text = resxSet.GetString("No Periods");
                lblBeginBookYear.Text = resxSet.GetString("Begin Book Year");
                lblEndBookYear.Text = resxSet.GetString("End Book Year");
                lblIsVat.Text = resxSet.GetString("Is Vat");
                chkIsVat.Text = resxSet.GetString("Is Vat");
                lblDefDebitorAccount.Text = resxSet.GetString("Def Debitor Account");
                lblDefCreditorAccount.Text = resxSet.GetString("Def Creditor Account");
                lblDefVatDebitor.Text = resxSet.GetString("Def Vat Debitor");
                lblDefVatCreditor.Text = resxSet.GetString("Def Vat Creditor");
                lblCurrDeferenceAccount.Text = resxSet.GetString("Currency Difference Account");
                lblPaymentDiferenceAccount.Text = resxSet.GetString("Payment Difference Account");
                lblBankCostAccount.Text = resxSet.GetString("Bank Cost Account");
                lblDefPayCondition.Text = resxSet.GetString("Def Pay Condition");
                if (resxSet.GetString(lblTransferingAcc.Text) != null)
                    lblTransferingAcc.Text = resxSet.GetString(lblTransferingAcc.Text);
                //lblTransferingAcc.Text = resxSet.GetString("Transfering Account");
                lblNoDayFirsWarning.Text = resxSet.GetString("No Day Frst Warning");
                lblNoDaySecondWarning.Text = resxSet.GetString("No Day Second Warning");
                btnSave.Text = resxSet.GetString("Save");
                btnDeleteDoc.Text = resxSet.GetString("Delete");
                btnDeleteMemo.Text = resxSet.GetString("Delete");

                if (resxSet.GetString(lblFirstPayment.Text) != null)
                    lblFirstPayment.Text = resxSet.GetString(lblFirstPayment.Text);
                if (resxSet.GetString(lblReservationCost.Text) != null)
                    lblReservationCost.Text = resxSet.GetString(lblReservationCost.Text);
                //====
                if (resxSet.GetString(lblBtwInvoicing.Text) != null)
                    lblBtwInvoicing.Text = resxSet.GetString(lblBtwInvoicing.Text);
                if (resxSet.GetString(lblPriceAccount.Text) != null)
                    lblPriceAccount.Text = resxSet.GetString(lblPriceAccount.Text);

                if (resxSet.GetString(lblLedInsurance.Text) != null)
                    lblLedInsurance.Text = resxSet.GetString(lblLedInsurance.Text);

                if (resxSet.GetString(lblCancelnsurance.Text) != null)
                    lblCancelnsurance.Text = resxSet.GetString(lblCancelnsurance.Text);

                if (resxSet.GetString(lblCalamitait.Text) != null)
                    lblCalamitait.Text = resxSet.GetString(lblCalamitait.Text);

                if (resxSet.GetString(lblMoneygroup.Text) != null)
                    lblMoneygroup.Text = resxSet.GetString(lblMoneygroup.Text);

                if (resxSet.GetString(lblCancelation.Text) != null)
                    lblCancelation.Text = resxSet.GetString(lblCancelation.Text);

                if (resxSet.GetString(lblDailyInvoice.Text) != null)
                    lblDailyInvoice.Text = resxSet.GetString(lblDailyInvoice.Text);

                if (resxSet.GetString(lblSepa.Text) != null)
                    lblSepa.Text = resxSet.GetString(lblSepa.Text);

                if (resxSet.GetString(lblDebitorReservation.Text) != null)
                    lblDebitorReservation.Text = resxSet.GetString(lblDebitorReservation.Text);

                if (resxSet.GetString(lblSepaAcc.Text) != null)
                    lblSepaAcc.Text = resxSet.GetString(lblSepaAcc.Text);
                if (resxSet.GetString(lblDefDifferenceAcc.Text) != null)
                    lblDefDifferenceAcc.Text = resxSet.GetString(lblDefDifferenceAcc.Text);

                //=====
            }

        }

        //Save
        private void btnSave_Click(object sender, EventArgs e)
        {

            AccSettingsBUS bus = new AccSettingsBUS();
            //countLabels();
            //if (result == false)
            //{
            //    translateRadMessageBox trs = new translateRadMessageBox();
            //    trs.translateAllMessageBox("Cannot save, wrong labels !");
            //    return;
            //}
            //else
            //{

                if (iID != -1)
                {
                    
                    model.idSettings = iID;
                    model.yearSettings = txtYearSettings.Text;
                    saveLabels();

                    model.myIban = txtmyIban.Text;
                    model.myBic = txtmyBic.Text;
                    model.sepaPath = txtSepaPath.Text;
                    if (ddlNoPeriod.Text != "")
                    {
                        model.noPeriods = Convert.ToInt32(ddlNoPeriod.Text);
                    }

                    if (txtBeginBookYear.Text != "")
                    {
                        model.beginBookYear = Convert.ToDateTime(txtBeginBookYear.Text);
                    }

                    if (txtEndBookYear.Text != "")
                    {
                        model.endBookYear = Convert.ToDateTime(txtEndBookYear.Text);
                    }

                    if (chkIsVat.Checked == true)
                    {
                        model.isVat = true;
                    }
                    else
                    {
                        model.isVat = false;
                    }

                    model.defDebitorAccount = txtDefDebitorAccount.Text;
                    model.defCreditorAccount = txtDefCreditorAccount.Text;
                    model.defVatDebitor = txtDefVatDebitor.Text;
                    model.defVatCreditor = txtDefVatCreditor.Text;
                    model.currDeferenceAccount = txtCurrDeferenceAccount.Text;
                    model.paymentDiferenceAccount = txtPaymentDiferenceAccount.Text;
                    model.bankCostAccount = txtBankCostAccount.Text;

                    if (txtDefPayCondition.Text != "")
                    {
                        model.defPayCondition = Convert.ToInt32(txtDefPayCondition.Text);
                    }

                    if (txtNoDayFirstWarning.Text != "")
                    {
                        model.noDayFrstWarning = Convert.ToInt32(txtNoDayFirstWarning.Text);
                    }

                    if (txtNoDaySecondWarning.Text != "")
                    {
                        model.noDaySecondWorning = Convert.ToInt32(txtNoDaySecondWarning.Text);
                    }
                    model.defLedgerPrice = txtPaketPrice.Text;
                    model.defLedgerIncurance = txtInsurance.Text;
                    model.defLedgerCancel = txtCancelInsurance.Text;
                    model.defLedgerCalamitu = txtCalamitait.Text;
                    model.defLedgerMoneyGr = txtMoneyGroup.Text;
                    model.defTransferingAcc = txtTransferingAcc.Text;
                    model.defReservationAcc = txtReservation.Text;
                    model.defLedgerCancelation = txtCancelation.Text;
                    model.defFirstPayment = txtFirstPayment.Text;
                    model.defLedReservationCost = txtReservationCost.Text;
                    model.debitorReservationAccount = txtDebitorReservation.Text;
                    model.defSepaAcc = txtSepaAcc.Text;
                    model.defDifferenceAcc = txtDefDifferenceAcc.Text;
                    if (dpCreatdDate.Text != "")
                        model.dtCreated = Convert.ToDateTime(dpCreatdDate.Text);
                    if (createUser != 0)
                        model.userCreated = Convert.ToInt32(createUser);
                    model.userModified = Login._user.idUser;


                    if (txtBtwInvoicing.Text != "")
                        model.defBTWinvoicing = Convert.ToInt32(txtBtwInvoicing.Text);
                    if (txtDailyInvoice.Text != "")
                        model.idDailyFak = Convert.ToInt32(txtDailyInvoice.Text);
                    if (bus.Update(model, this.Name, Login._user.idUser) == true)
                    {
                        modelChanged = true;
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("Saved") != null)
                            {
                                RadMessageBox.Show(resxSet.GetString("Saved"));
                            }
                            else
                            {
                                RadMessageBox.Show("Saved");
                            }
                        }
                    }
             
                }

                else //ulazi u else kada se dodaje novi red
                {
                    

                    if (ddlNoPeriod.Text == "")
                    {
                        RadMessageBox.Show("Can't SAVE witout Year Settings ! ");
                        ddlNoPeriod.Focus();
                        return;
                    }
                    AccSettingsModel model = new AccSettingsModel();


                    model.userCreated = Login._user.idUser;
                    model.yearSettings = txtYearSettings.Text;
                    model.noPeriods = Convert.ToInt32(ddlNoPeriod.Text);

                    if (txtBeginBookYear.Text != "")
                    {
                        model.beginBookYear = Convert.ToDateTime(txtBeginBookYear.Text);
                    }

                    if (txtEndBookYear.Text != "")
                    {
                        model.endBookYear = Convert.ToDateTime(txtEndBookYear.Text);
                    }

                    if (chkIsVat.Checked == true)
                    {
                        model.isVat = true;
                    }

                    else
                    {
                        model.isVat = false;
                    }
                    model.defDebitorAccount = txtDefDebitorAccount.Text;
                    model.defCreditorAccount = txtDefCreditorAccount.Text;
                    model.defVatDebitor = txtDefVatDebitor.Text;
                    model.defVatCreditor = txtDefVatCreditor.Text;
                    model.currDeferenceAccount = txtCurrDeferenceAccount.Text;
                    model.paymentDiferenceAccount = txtPaymentDiferenceAccount.Text;
                    model.bankCostAccount = txtBankCostAccount.Text;

                    if (txtDefPayCondition.Text != "")
                    {
                        model.defPayCondition = Convert.ToInt32(txtDefPayCondition.Text);
                    }

                    if (txtNoDayFirstWarning.Text != "")
                    {
                        model.noDayFrstWarning = Convert.ToInt32(txtNoDayFirstWarning.Text);
                    }

                    if (txtNoDaySecondWarning.Text != "")
                    {
                        model.noDaySecondWorning = Convert.ToInt32(txtNoDaySecondWarning.Text);
                    }
                    model.defLedgerPrice = txtPaketPrice.Text;
                    model.defLedgerIncurance = txtInsurance.Text;
                    model.defLedgerCancel = txtCancelInsurance.Text;
                    model.defLedgerCancelation = txtCancelation.Text;
                    model.defLedgerCalamitu = txtCalamitait.Text;
                    model.defLedgerMoneyGr = txtMoneyGroup.Text;
                    model.defTransferingAcc = txtTransferingAcc.Text;
                    model.defFirstPayment = txtFirstPayment.Text;
                    model.defReservationAcc = txtReservation.Text;
                    model.defLedReservationCost = txtReservationCost.Text;
                    model.debitorReservationAccount = txtDebitorReservation.Text;
                    model.defDifferenceAcc = txtDefDifferenceAcc.Text;

                    model.sepaPath = txtSepaPath.Text;
                    model.myIban = txtmyIban.Text;
                    model.myBic = txtmyBic.Text;

                    model.defSepaAcc = txtSepaAcc.Text;
                    if (txtBtwInvoicing.Text != "")
                        model.defBTWinvoicing = Convert.ToInt32(txtBtwInvoicing.Text);
                    if (txtDailyInvoice.Text != "")
                        model.idDailyFak = Convert.ToInt32(txtDailyInvoice.Text);
                    bool llok = false;
                    llok = bus.Save(model, this.Name, Login._user.idUser);
                    if (llok == true)
                    {
                        modelChanged = true;
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            if (resxSet.GetString("Inserted") != null)
                                RadMessageBox.Show(resxSet.GetString("Inserted"));
                            else
                                RadMessageBox.Show("Inserted");
                        this.Close();
                    }
                }
                modelChanged = true;
            //}
        }

        private void btnDeleteDoc_Click(object sender, EventArgs e)
        {
            translateRadMessageBox tr = new translateRadMessageBox();
            if (tr.translateAllMessageBoxDialog("Do you want to DELETE this line?", "Acc Settings") == System.Windows.Forms.DialogResult.Yes)
            {
                if (iID != -1)
                {
                    AccSettingsBUS db = new AccSettingsBUS();
                    db.Delete(iID, this.Name, Login._user.idUser);

                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have delete successfully!");

                    this.Close();
                    modelChanged = true;
                }
            }
            else
            {
               
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong!Please try again or contact your administrator!");
                return;
            }            
              //DialogResult dr = RadMessageBox.Show("Do you want to DELETE this line ?", "Delete", MessageBoxButtons.YesNo);
              //if (dr == DialogResult.Yes)
              //{
              //    if (iID != -1)
              //    {
              //        AccSettingsBUS db = new AccSettingsBUS();
              //        db.Delete(iID);
              //        this.Close();
              //        modelChanged = true;
              //    }
              //}
        }

        #region Field Controls

        private void txtDefDebitorAccount_Leave(object sender, EventArgs e)
        {
            if (txtDefDebitorAccount.Text != "")
            {
                LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lam = new LedgerAccountModel();

                lam = ledbus.GetAccount(txtDefDebitorAccount.Text, Login._bookyear);
                if (lam != null)
                {
                    labelDefDebitorAccount.Text = lam.descLedgerAccount;
                    xAccountSplit = lam.numberLedgerAccount;
                    //   xSideBooking = lam.sideBooking;
                    txtDefCreditorAccount.Focus();
                }
                // za polja koja su prazna da ukine labelu i ne dozvoli unosenje nepostojeceg Account-a
                else
                {
                    RadMessageBox.Show("Wrong Account");
                    txtDefDebitorAccount.Focus();

                }
            }
            else
            {
                labelDefDebitorAccount.Text = "";
            }
        }

        private void txtDefCreditorAccount_Leave(object sender, EventArgs e)
        {
            if (txtDefCreditorAccount.Text != "")
            {
                LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lam = new LedgerAccountModel();

                lam = ledbus.GetAccount(txtDefCreditorAccount.Text, Login._bookyear);
                if (lam != null)
                {
                    labelDefCreditorAccount.Text = lam.descLedgerAccount;
                    xAccountSplit = lam.numberLedgerAccount;

                    txtDefVatDebitor.Focus();
                }
                else
                {
                    RadMessageBox.Show("Wrong Account");
                    txtDefCreditorAccount.Focus();
                }
            }
            else
            {
                labelDefCreditorAccount.Text = "";
            }
        }

        private void txtDefVatDebitor_Leave(object sender, EventArgs e)
        {
            if (txtDefVatDebitor.Text != "")
            {
                LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lam = new LedgerAccountModel();

                lam = ledbus.GetAccount(txtDefVatDebitor.Text, Login._bookyear);
                if (lam != null)
                {
                    labelDefVatDebitor.Text = lam.descLedgerAccount;
                    xAccountSplit = lam.numberLedgerAccount;

                    txtDefVatCreditor.Focus();
                }
                else
                {
                    RadMessageBox.Show("Wrong Account");
                    txtDefVatDebitor.Focus();
                }
            }
            else
            {
                labelDefVatDebitor.Text = "";
            }
        }


        private void txtDefVatCreditor_Leave(object sender, EventArgs e)
        {
            if (txtDefVatCreditor.Text != "")
            {
                LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lam = new LedgerAccountModel();

                lam = ledbus.GetAccount(txtDefVatCreditor.Text, Login._bookyear);
                if (lam != null)
                {
                    labelDefVatCreditor.Text = lam.descLedgerAccount;
                    xAccountSplit = lam.numberLedgerAccount;

                    txtCurrDeferenceAccount.Focus();
                }
                // za polja koja su prazna da ukine labelu i ne dozvoli unosenje nepostojeceg Account-a
                else
                {
                    RadMessageBox.Show("Wrong Account");
                    txtDefVatCreditor.Focus();
                }

            }
            else
            {
                labelDefVatCreditor.Text = "";
            }
        }

        private void txtCurrDeferenceAccount_Leave(object sender, EventArgs e)
        {
            if (txtCurrDeferenceAccount.Text != "")
            {
                LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lam = new LedgerAccountModel();

                lam = ledbus.GetAccount(txtCurrDeferenceAccount.Text, Login._bookyear);
                if (lam != null)
                {
                    labelCurrDeferenceAccount.Text = lam.descLedgerAccount;
                    xAccountSplit = lam.numberLedgerAccount;

                    txtPaymentDiferenceAccount.Focus();
                }
                else
                {
                    RadMessageBox.Show("Wrong Account");
                    txtCurrDeferenceAccount.Focus();
                }
            }
            else
            {
                labelCurrDeferenceAccount.Text = "";
            }
        }

        private void txtPaymentDiferenceAccount_Leave(object sender, EventArgs e)
        {
            if (txtPaymentDiferenceAccount.Text != "")
            {
                LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lam = new LedgerAccountModel();

                lam = ledbus.GetAccount(txtPaymentDiferenceAccount.Text, Login._bookyear);
                if (lam != null)
                {
                    labelPaymentDiferenceAccount.Text = lam.descLedgerAccount;
                    xAccountSplit = lam.numberLedgerAccount;

                    txtBankCostAccount.Focus();
                }
                else
                {
                    RadMessageBox.Show("Wrong Account");
                    txtPaymentDiferenceAccount.Focus();
                }
            }
            else
            {
                labelPaymentDiferenceAccount.Text = "";
            }
        }

        private void txtBankCostAccount_Leave(object sender, EventArgs e)
        {
            if (txtBankCostAccount.Text != "")
            {
                LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lam = new LedgerAccountModel();

                lam = ledbus.GetAccount(txtBankCostAccount.Text, Login._bookyear);
                if (lam != null)
                {
                    labelBankCostAccount.Text = lam.descLedgerAccount;
                    xAccountSplit = lam.numberLedgerAccount;

                    txtDefPayCondition.Focus();
                }
                else
                {
                    RadMessageBox.Show("Wrong Account");
                    txtBankCostAccount.Focus();
                }
            }
            else
            {
                labelBankCostAccount.Text = "";
            }
        }

        private void txtDefPayCondition_Leave(object sender, EventArgs e)
        {
            labelDefPayCondition.Text = "";
        }

        private void txtNoDayFirstWarning_Leave(object sender, EventArgs e)
        {
     

        }

        private void txtNoDaySecondWarning_Leave(object sender, EventArgs e)
        {
   
        }

        #endregion Field Controls

        #region Btn Click

        private void btnDefDebitorAccount_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ccentar3g = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllAccounts();
            var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX3g = new LedgerAccountModel();
                genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
                //set textbox

                txtDefDebitorAccount.Text = genmX3g.numberLedgerAccount;
                labelDefDebitorAccount.Text = genmX3g.descLedgerAccount;

                //xSideBooking = genmX3.sideBooking;
            }
        }

        private void btnDefCreditorAccount_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ccentar3g = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllAccounts();
            var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX3g = new LedgerAccountModel();
                genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
                //set textbox

                txtDefCreditorAccount.Text = genmX3g.numberLedgerAccount;
                labelDefCreditorAccount.Text = genmX3g.descLedgerAccount;
            }
        }

        private void btnDefVatDebitor_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ccentar3g = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllAccounts();
            var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX3g = new LedgerAccountModel();
                genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
                //set textbox

                txtDefVatDebitor.Text = genmX3g.numberLedgerAccount;
                labelDefVatDebitor.Text = genmX3g.descLedgerAccount;
            }
        }

        private void btnDefVatCreditor_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ccentar3g = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllAccounts();
            var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX3g = new LedgerAccountModel();
                genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
                //set textbox

                txtDefVatCreditor.Text = genmX3g.numberLedgerAccount;
                labelDefVatCreditor.Text = genmX3g.descLedgerAccount;
            }
        }

        private void btnCurrDeferenceAccount_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ccentar3g = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllAccounts();
            var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX3g = new LedgerAccountModel();
                genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
                //set textbox

                txtCurrDeferenceAccount.Text = genmX3g.numberLedgerAccount;
                labelCurrDeferenceAccount.Text = genmX3g.descLedgerAccount;
            }
        }

        private void btnPaymentDiferenceAccount_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ccentar3g = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllAccounts();
            var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX3g = new LedgerAccountModel();
                genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
                //set textbox

                txtPaymentDiferenceAccount.Text = genmX3g.numberLedgerAccount;
                labelPaymentDiferenceAccount.Text = genmX3g.descLedgerAccount;
            }
        }

        private void btnBankCostAccount_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ccentar3g = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllAccounts();
            var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX3g = new LedgerAccountModel();
                genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
                //set textbox

                txtBankCostAccount.Text = genmX3g.numberLedgerAccount;
                labelBankCostAccount.Text = genmX3g.descLedgerAccount;
            }
        }

        private void btnDefPayCondition_Click(object sender, EventArgs e)
        {
            AccPaymentBUS ccentar = new AccPaymentBUS();
            List<IModel> gmX = new List<IModel>();

            gmX = ccentar.GetAllAccPayment();
            var dlgSave = new GridLookupForm(gmX, "Pay Conditions");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                AccPaymentModel genmX = new AccPaymentModel();
                genmX = (AccPaymentModel)dlgSave.selectedRow;

                if (genmX != null)
                {
                    //set textbox
                    if (genmX.idPayment != null)
                    {
                        txtDefPayCondition.Text = genmX.idPayment.ToString();
                        labelDefPayCondition.Text = genmX.description;
                    }
                }
            }

        }

        private void btnNoDayFirstWarning_Click(object sender, EventArgs e)
        {
            //LedgerAccountBUS ccentar3g = new LedgerAccountBUS();
            //List<IModel> gmX3g = new List<IModel>();

            //gmX3g = ccentar3g.GetAllAccounts();
            //var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            //if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            //{
            //    LedgerAccountModel genmX3g = new LedgerAccountModel();
            //    genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
            //    //set textbox

            //    txtNoDayFirstWarning.Text = genmX3g.numberLedgerAccount;
            //    labelNoDayFirstWarning.Text = genmX3g.descLedgerAccount;
            //}
        }

        private void btnNoDaySecondWarning_Click(object sender, EventArgs e)
        {
            //LedgerAccountBUS ccentar3g = new LedgerAccountBUS();
            //List<IModel> gmX3g = new List<IModel>();

            //gmX3g = ccentar3g.GetAllAccounts();
            //var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            //if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            //{
            //    LedgerAccountModel genmX3g = new LedgerAccountModel();
            //    genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
            //    //set textbox

            //    txtNoDaySecondWarning.Text = genmX3g.numberLedgerAccount;
            //    labelNoDaySecondWarning.Text = genmX3g.descLedgerAccount;
            //}
        }
        private void btnFirstPayment_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ccentar3g = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllAccounts();
            var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX3g = new LedgerAccountModel();
                genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
                //set textbox

                txtFirstPayment.Text = genmX3g.numberLedgerAccount;
                labelFirstPayment.Text = genmX3g.descLedgerAccount;
            }
        }

        private void btnReservationCost_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ccentar3g = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllAccounts();
            var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX3g = new LedgerAccountModel();
                genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
                //set textbox

                txtReservationCost.Text = genmX3g.numberLedgerAccount;
                labelReservationCost.Text = genmX3g.descLedgerAccount;
            }
        }

        #endregion Btn Click


        #region Kretanje Enterom
        //==================================   Kretanje ENTEROM   i pozivanje forme na F2 taster ============================================================================

        private void txtDefDebitorAccount_KeyDown(object sender, KeyEventArgs e)
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

                    txtDefDebitorAccount.Text = genmX.numberLedgerAccount;
                    xAccountSplit = genmX.numberLedgerAccount;
                    labelDefDebitorAccount.Text = genmX.descLedgerAccount;

                    txtDefCreditorAccount.Focus();
                }

            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtDefDebitorAccount.Text != "")
                {
                    LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel ledmodel = new LedgerAccountModel();

                    ledmodel = ledbus.GetAccount(txtDefDebitorAccount.Text, Login._bookyear);
                    if (ledmodel != null)
                    {
                        labelDefDebitorAccount.Text = ledmodel.descLedgerAccount;
                        xAccountSplit = ledmodel.numberLedgerAccount;

                        txtDefCreditorAccount.Focus();
                    }
                    else
                    {
                        RadMessageBox.Show("Wrong Account");
                        txtDefDebitorAccount.Text = "";
                        labelDefDebitorAccount.Text = "";
                    }
                }
                else
                {
                    txtDefCreditorAccount.Focus();
                }
            }
        }

        private void txtDefCreditorAccount_KeyDown(object sender, KeyEventArgs e)
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

                    txtDefCreditorAccount.Text = genmX.numberLedgerAccount;
                    xAccountSplit = genmX.numberLedgerAccount;
                    labelDefCreditorAccount.Text = genmX.descLedgerAccount;

                    txtDefVatDebitor.Focus();
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtDefCreditorAccount.Text != "")
                {
                    LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel ledmodel = new LedgerAccountModel();

                    ledmodel = ledbus.GetAccount(txtDefCreditorAccount.Text, Login._bookyear);
                    if (ledmodel != null)
                    {
                        labelDefCreditorAccount.Text = ledmodel.descLedgerAccount;
                        xAccountSplit = ledmodel.numberLedgerAccount;

                        txtDefVatDebitor.Focus();
                    }
                    else
                    {
                        RadMessageBox.Show("Wrong Account");
                        txtDefCreditorAccount.Text = "";
                        labelDefCreditorAccount.Text = "";
                    }
                }
                else
                {
                    txtDefVatDebitor.Focus();
                }
            }
        }

        private void txtDefVatDebitor_KeyDown(object sender, KeyEventArgs e)
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

                    txtDefVatDebitor.Text = genmX.numberLedgerAccount;
                    xAccountSplit = genmX.numberLedgerAccount;
                    labelDefVatDebitor.Text = genmX.descLedgerAccount;

                    txtDefVatCreditor.Focus();
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtDefVatDebitor.Text != "")
                {
                    LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel ledmodel = new LedgerAccountModel();

                    ledmodel = ledbus.GetAccount(txtDefVatDebitor.Text, Login._bookyear);
                    if (ledmodel != null)
                    {
                        labelDefVatDebitor.Text = ledmodel.descLedgerAccount;
                        xAccountSplit = ledmodel.numberLedgerAccount;

                        txtDefVatCreditor.Focus();
                    }
                    else
                    {
                        RadMessageBox.Show("Wrong Account");
                        txtDefVatDebitor.Text = "";
                        labelDefVatDebitor.Text = "";
                    }
                }
                else
                {
                    txtDefVatCreditor.Focus();
                }
            }
        }

        private void txtDefVatCreditor_KeyDown(object sender, KeyEventArgs e)
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

                    txtDefVatCreditor.Text = genmX.numberLedgerAccount;
                    xAccountSplit = genmX.numberLedgerAccount;
                    labelDefVatCreditor.Text = genmX.descLedgerAccount;

                    txtCurrDeferenceAccount.Focus();
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtDefVatCreditor.Text != "")
                {
                    LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel ledmodel = new LedgerAccountModel();

                    ledmodel = ledbus.GetAccount(txtDefVatDebitor.Text, Login._bookyear);
                    if (ledmodel != null)
                    {
                        labelDefVatCreditor.Text = ledmodel.descLedgerAccount;
                        xAccountSplit = ledmodel.numberLedgerAccount;

                        txtCurrDeferenceAccount.Focus();
                    }
                    else
                    {
                        RadMessageBox.Show("Wrong Account");
                        txtDefVatCreditor.Text = "";
                        labelDefVatCreditor.Text = "";
                    }
                }
                else
                {
                    txtCurrDeferenceAccount.Focus();
                }
            }
        }

        private void txtCurrDeferenceAccount_KeyDown(object sender, KeyEventArgs e)
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

                    txtCurrDeferenceAccount.Text = genmX.numberLedgerAccount;
                    xAccountSplit = genmX.numberLedgerAccount;
                    labelCurrDeferenceAccount.Text = genmX.descLedgerAccount;

                    txtPaymentDiferenceAccount.Focus();
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtCurrDeferenceAccount.Text != "")
                {
                    LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel ledmodel = new LedgerAccountModel();

                    ledmodel = ledbus.GetAccount(txtCurrDeferenceAccount.Text, Login._bookyear);
                    if (ledmodel != null)
                    {
                        labelCurrDeferenceAccount.Text = ledmodel.descLedgerAccount;
                        xAccountSplit = ledmodel.numberLedgerAccount;

                        txtPaymentDiferenceAccount.Focus();
                    }
                    else
                    {
                        RadMessageBox.Show("Wrong Account");
                        txtCurrDeferenceAccount.Text = "";
                        labelCurrDeferenceAccount.Text = "";
                    }
                }
                else
                {
                    txtPaymentDiferenceAccount.Focus();
                }
            }
        }

        private void txtPaymentDiferenceAccount_KeyDown(object sender, KeyEventArgs e)
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

                    txtPaymentDiferenceAccount.Text = genmX.numberLedgerAccount;
                    xAccountSplit = genmX.numberLedgerAccount;
                    labelPaymentDiferenceAccount.Text = genmX.descLedgerAccount;

                    txtBankCostAccount.Focus();
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtPaymentDiferenceAccount.Text != "")
                {
                    LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel ledmodel = new LedgerAccountModel();

                    ledmodel = ledbus.GetAccount(txtCurrDeferenceAccount.Text, Login._bookyear);
                    if (ledmodel != null)
                    {
                        labelPaymentDiferenceAccount.Text = ledmodel.descLedgerAccount;
                        xAccountSplit = ledmodel.numberLedgerAccount;

                        txtBankCostAccount.Focus();
                    }
                    else
                    {
                        RadMessageBox.Show("Wrong Account");
                        txtPaymentDiferenceAccount.Text = "";
                        labelPaymentDiferenceAccount.Text = "";
                    }
                }
                else
                {
                    txtBankCostAccount.Focus();
                }
            }
        }

        private void txtBankCostAccount_KeyDown(object sender, KeyEventArgs e)
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

                    txtBankCostAccount.Text = genmX.numberLedgerAccount;
                    xAccountSplit = genmX.numberLedgerAccount;
                    labelBankCostAccount.Text = genmX.descLedgerAccount;

                    txtDefPayCondition.Focus();
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtBankCostAccount.Text != "")
                {
                    LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel ledmodel = new LedgerAccountModel();

                    ledmodel = ledbus.GetAccount(txtBankCostAccount.Text, Login._bookyear);
                    if (ledmodel != null)
                    {
                        labelBankCostAccount.Text = ledmodel.descLedgerAccount;
                        xAccountSplit = ledmodel.numberLedgerAccount;

                        txtDefPayCondition.Focus();
                    }
                    else
                    {
                        RadMessageBox.Show("Wrong Account");
                        txtBankCostAccount.Text = "";
                        labelBankCostAccount.Text = "";
                    }
                }
                else
                {
                    txtDefPayCondition.Focus();
                }
            }
        }

        private void txtDefPayCondition_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtDefPayCondition.Text != "")
                {
                    LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel ledmodel = new LedgerAccountModel();

                    ledmodel = ledbus.GetAccount(txtBankCostAccount.Text, Login._bookyear);
                    
                    if (ledmodel != null)
                    {
                        labelDefPayCondition.Text = ledmodel.descLedgerAccount;
                        xAccountSplit = ledmodel.numberLedgerAccount;

                        txtNoDayFirstWarning.Focus();

                    }

                    txtNoDayFirstWarning.Focus();
                }
                else
                {
                    txtNoDayFirstWarning.Focus();
                }
            }
        }

        private void txtNoDayFirstWarning_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtNoDaySecondWarning.Text != "")
                {
                    LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel ledmodel = new LedgerAccountModel();

                    ledmodel = ledbus.GetAccount(txtBankCostAccount.Text, Login._bookyear);

                    if (ledmodel != null)
                    {
                        labelDefPayCondition.Text = ledmodel.descLedgerAccount;
                        xAccountSplit = ledmodel.numberLedgerAccount;

                        txtNoDaySecondWarning.Focus();

                    }

                    txtNoDaySecondWarning.Focus();
                }
                else
                {
                    txtNoDaySecondWarning.Focus();
                }
            }
        }

        private void txtNoDaySecondWarning_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtNoDaySecondWarning.Text != "")
                {
                    LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel ledmodel = new LedgerAccountModel();

                    ledmodel = ledbus.GetAccount(txtBankCostAccount.Text, Login._bookyear);

                    if (ledmodel != null)
                    {
                        labelNoDaySecondWarning.Text = ledmodel.descLedgerAccount;
                        xAccountSplit = ledmodel.numberLedgerAccount;

                        txtYearSettings.Focus();

                    }

                    txtYearSettings.Focus();
                }
                else
                {
                    txtYearSettings.Focus();
                }
            }
        }

        private void btnBtwInvoicing_Click(object sender, EventArgs e)
        {
            AccTaxBUS ccentar3g = new AccTaxBUS();
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllTax(Login._user.lngUser);
            var dlgSave3g = new GridLookupForm(gmX3g, "BTW");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                AccTaxModel genmX3g = new AccTaxModel();
                genmX3g = (AccTaxModel)dlgSave3g.selectedRow;
                //set textbox

                txtBtwInvoicing.Text = genmX3g.idTax.ToString();
                labelBtwInvoicing.Text = genmX3g.descTax;
            }

        }

        private void btnPaketPrice_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ccentar3g = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllAccounts();
            var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX3g = new LedgerAccountModel();
                genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
                //set textbox

                txtPaketPrice.Text = genmX3g.numberLedgerAccount;
                labelPaketPrice.Text = genmX3g.descLedgerAccount;
            }
        }

        private void btnInsurance_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ccentar3g = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllAccounts();
            var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX3g = new LedgerAccountModel();
                genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
                //set textbox

                txtInsurance.Text = genmX3g.numberLedgerAccount;
                labelInsurance.Text = genmX3g.descLedgerAccount;
            }
        }

        private void btnCancelInsurance_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ccentar3g = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllAccounts();
            var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX3g = new LedgerAccountModel();
                genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
                //set textbox

                txtCancelInsurance.Text = genmX3g.numberLedgerAccount;
                labelCancelInsurance.Text = genmX3g.descLedgerAccount;
            }
        }

        private void btnCalamitait_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ccentar3g = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllAccounts();
            var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX3g = new LedgerAccountModel();
                genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
                //set textbox

                txtCalamitait.Text = genmX3g.numberLedgerAccount;
                labelCalamitait.Text = genmX3g.descLedgerAccount;
            }
        }

        private void btnMoneyGroup_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ccentar3g = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllAccounts();
            var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX3g = new LedgerAccountModel();
                genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
                //set textbox

                txtMoneyGroup.Text = genmX3g.numberLedgerAccount;
                labelMoneyGroup.Text = genmX3g.descLedgerAccount;
            }
        }

        private void btnDailyInvoice_Click(object sender, EventArgs e)
        {
            AccDailyBUS ccentar3g = new AccDailyBUS(Login._bookyear);
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllDailys();
            var dlgSave3g = new GridLookupForm(gmX3g, "Daily");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                AccDailyModel genmX3g = new AccDailyModel();
                genmX3g = (AccDailyModel)dlgSave3g.selectedRow;
                //set textbox

                txtDailyInvoice.Text = genmX3g.idDaily.ToString();
                labelDailyInvoice.Text = genmX3g.descDaily;
            }
        }
        private void btnDefDifferenceAcc_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ccentar3g = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllAccounts();
            var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX3g = new LedgerAccountModel();
                genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
                //set textbox

                txtDefDifferenceAcc.Text = genmX3g.numberLedgerAccount;
                labelDefDifferenceAcc.Text = genmX3g.descLedgerAccount;
            }
           
        }
        private Boolean countLabels()
        {
         
            int a = 0;
           
            foreach (Control c in panelLabels.Controls)
            {
                RadCheckBox rch = (RadCheckBox)c;
                if (rch.Checked == true)
                {
                    a = a+1;
                  
                }
            }
           if (a == 1)
           {
                    result = true;
           }
           else
           {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Check the labels, please!");
                    result = false;
            }
            
            return result;
        }
        private Boolean saveLabels()
        {
            Boolean resultWr = false;
          
            foreach (Control c in panelLabels.Controls)
            {
                RadCheckBox rch = (RadCheckBox)c;
                if (rch.Checked == true)
                {
                    model.labelSettings = Login._arrLabels.Find(item => item.nameLabel.TrimEnd() == rch.Text.TrimEnd()).idLabel;
                    //lab.idArrangement = arrange.idArrangement;
                    //ArrangementLabel.Add(lab);
                    resultWr = true;
                }
            }
            return resultWr;
        }

        private void btnTransveringAcc_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ccentar3g = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllAccounts();
            var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX3g = new LedgerAccountModel();
                genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
                //set textbox

                txtTransferingAcc.Text = genmX3g.numberLedgerAccount;
                labelTransferingAcc.Text = genmX3g.descLedgerAccount;
            }
        }
        private void btnReservation_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ccentar3g = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllAccounts();
            var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX3g = new LedgerAccountModel();
                genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
                //set textbox

                txtReservation.Text = genmX3g.numberLedgerAccount;
                labelReservation.Text = genmX3g.descLedgerAccount;
            }
        }

        private void btnCancelation_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ccentar3g = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllAccounts();
            var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX3g = new LedgerAccountModel();
                genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
                //set textbox

                txtCancelation.Text = genmX3g.numberLedgerAccount;
                labelCancelation.Text = genmX3g.descLedgerAccount;
            }
        }

        private void txtTransferingAcc_KeyDown(object sender, KeyEventArgs e)
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

                    txtTransferingAcc.Text = genmX.numberLedgerAccount;
                    xAccountSplit = genmX.numberLedgerAccount;
                    labelTransferingAcc.Text = genmX.descLedgerAccount;

                    txtBankCostAccount.Focus();
                }
            }
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtTransferingAcc.Text != "")
                {
                    LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel ledmodel = new LedgerAccountModel();

                    ledmodel = ledbus.GetAccount(txtCurrDeferenceAccount.Text, Login._bookyear);
                    if (ledmodel != null)
                    {
                        labelTransferingAcc.Text = ledmodel.descLedgerAccount;
                        xAccountSplit = ledmodel.numberLedgerAccount;

                        txtBankCostAccount.Focus();
                    }
                    else
                    {
                        RadMessageBox.Show("Wrong Account");
                        txtTransferingAcc.Text = "";
                        labelTransferingAcc.Text = "";
                    }
                }
                else
                {
                    txtBankCostAccount.Focus();
                }
            }
        }

        private void txtTransferingAcc_Leave(object sender, EventArgs e)
        {
            if (txtTransferingAcc.Text != "")
            {
                LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lam = new LedgerAccountModel();

                lam = ledbus.GetAccount(txtBankCostAccount.Text, Login._bookyear);
                if (lam != null)
                {
                    labelTransferingAcc.Text = lam.descLedgerAccount;
                    xAccountSplit = lam.numberLedgerAccount;

                    txtDefPayCondition.Focus();
                }
                else
                {
                    RadMessageBox.Show("Wrong Account");
                    txtTransferingAcc.Focus();
                }
            }
            else
            {
                labelTransferingAcc.Text = "";
            }
        }

        private void txtmyIban_Leave(object sender, EventArgs e)
        {
            MakeInvoice au = new MakeInvoice();
            bool aa = false;
            if (txtmyIban.Text != "")
            {
                 aa= au.ValidateIban(txtmyIban.Text.Trim());
                if (aa == false)
                {
                    RadMessageBox.Show("Wrong IBAN !!!");
                    txtmyIban.Focus();
                }
            }
        }

        private void btnSepa_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                string folderica = fbd.SelectedPath;
                txtSepaPath.Text = folderica;
            }
                      
        }

        private void txtFirstPayment_Leave(object sender, EventArgs e)
        {
            if (txtFirstPayment.Text != "")
            {
                LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lam = new LedgerAccountModel();

                lam = ledbus.GetAccount(txtFirstPayment.Text, Login._bookyear);
                if (lam != null)
                {
                    labelFirstPayment.Text = lam.descLedgerAccount;
                   // xAccountSplit = lam.numberLedgerAccount;

                    txtReservationCost.Focus();
                }
                else
                {
                    RadMessageBox.Show("Wrong Account");
                    txtFirstPayment.Focus();
                }
            }
            else
            {
                labelFirstPayment.Text = "";
            }
        }

        private void txtFirstPayment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtFirstPayment.Text != "")
                {
                    LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel ledmodel = new LedgerAccountModel();

                    ledmodel = ledbus.GetAccount(txtFirstPayment.Text, Login._bookyear);

                    if (ledmodel != null)
                    {
                        labelFirstPayment.Text = ledmodel.descLedgerAccount;
                        xAccountSplit = ledmodel.numberLedgerAccount;

                        txtReservationCost.Focus();

                    }

                    txtReservationCost.Focus();
                }
                else
                {
                    txtReservationCost.Focus();
                }
            }
        }

        private void txtReservationCost_Leave(object sender, EventArgs e)
        {
            if (txtReservationCost.Text != "")
            {
                LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lam = new LedgerAccountModel();

                lam = ledbus.GetAccount(txtReservationCost.Text, Login._bookyear);
                if (lam != null)
                {
                    labelReservationCost.Text = lam.descLedgerAccount;
                    // xAccountSplit = lam.numberLedgerAccount;

                    txtSepaPath.Focus();
                }
                else
                {
                    RadMessageBox.Show("Wrong Account");
                    txtReservationCost.Focus();
                }
            }
            else
            {
                labelReservationCost.Text = "";
            }
        }

        private void txtReservationCost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtReservationCost.Text != "")
                {
                    LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel ledmodel = new LedgerAccountModel();

                    ledmodel = ledbus.GetAccount(txtReservationCost.Text, Login._bookyear);

                    if (ledmodel != null)
                    {
                        labelReservationCost.Text = ledmodel.descLedgerAccount;
                       // xAccountSplit = ledmodel.numberLedgerAccount;

                        txtDebitorReservation.Focus();

                    }

                    txtDebitorReservation.Focus();
                }
                else
                {
                    txtDebitorReservation.Focus();
                }
            }
        }

        private void btnDebitorReservation_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ccentar3g = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllAccounts();
            var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX3g = new LedgerAccountModel();
                genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
                //set textbox

                txtDebitorReservation.Text = genmX3g.numberLedgerAccount;
                labelDebitorReservation.Text = genmX3g.descLedgerAccount;
            }
        }
        private void btnSepaAcc_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ccentar3g = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX3g = new List<IModel>();

            gmX3g = ccentar3g.GetAllAccounts();
            var dlgSave3g = new GridLookupForm(gmX3g, "Ledger");

            if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX3g = new LedgerAccountModel();
                genmX3g = (LedgerAccountModel)dlgSave3g.selectedRow;
                //set textbox

                txtSepaAcc.Text = genmX3g.numberLedgerAccount;
                labelSepaAcc.Text = genmX3g.descLedgerAccount;
            }
        }

        private void txtDebitorReservation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtDebitorReservation.Text != "")
                {
                    LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel ledmodel = new LedgerAccountModel();

                    ledmodel = ledbus.GetAccount(txtDebitorReservation.Text, Login._bookyear);

                    if (ledmodel != null)
                    {
                        labelDebitorReservation.Text = ledmodel.descLedgerAccount;
                        // xAccountSplit = ledmodel.numberLedgerAccount;

                        txtSepaAcc.Focus();

                    }

                    txtSepaAcc.Focus();
                }
                else
                {
                    txtSepaAcc.Focus();
                }
            }
        }


        private void txt_Leave(object sender, EventArgs e)
        {
            RadTextBox rtb = (RadTextBox)sender;
            string nameLabel = "";
            nameLabel = "label" + rtb.Name.Replace("txt", "");

            RadLabel lbl = new RadLabel();
            if (this.Controls.Find(nameLabel, true)[0] != null)
                lbl = (RadLabel)this.Controls.Find(nameLabel, true)[0];

            if (rtb.Text != "")
            {
                LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lam = new LedgerAccountModel();


                lam = ledbus.GetAccount(rtb.Text, Login._bookyear);
                if (lam != null)
                {

                    lbl.Text = lam.descLedgerAccount;
                    // xAccountSplit = lam.numberLedgerAccount;

                    Control ctl = (Control)sender;
                    Control nextControl = ctl.Parent.GetNextControl(ctl, true);
                }
                else
                {
                    RadMessageBox.Show("Wrong Account");
                    lbl.Text = "";
                    //Control ctl = (Control)sender;
                    //Control nextControl = ctl.Parent.GetNextControl(ctl, false);
                    //rtb.Focus();
                }
            }

            else
            {
                lbl.Text = "";
            }
        }

        private void txtPayment_Leave(object sender, EventArgs e)
        {
            RadTextBox rtb = (RadTextBox)sender;
            string nameLabel = "";
            nameLabel = "label" + rtb.Name.Replace("txt", "");

            RadLabel lbl = new RadLabel();
            if (this.Controls.Find(nameLabel, true)[0] != null)
                lbl = (RadLabel)this.Controls.Find(nameLabel, true)[0];

            if (rtb.Text != "")
            {
                AccPaymentBUS ledbus = new AccPaymentBUS();
                AccPaymentModel lam = new AccPaymentModel();
                int n;
                bool isNumeric = int.TryParse(rtb.Text, out n);

                if (isNumeric == true)
                {
                    lam = ledbus.GetPaymentByID(Convert.ToInt32(rtb.Text));
                    if (lam != null)
                    {

                        lbl.Text = lam.description;
                        // xAccountSplit = lam.numberLedgerAccount;

                        Control ctl = (Control)sender;
                        Control nextControl = ctl.Parent.GetNextControl(ctl, true);
                    }
                    else
                    {
                        RadMessageBox.Show("Wrong Payment");
                        lbl.Text = "";
                        //Control ctl = (Control)sender;
                        //Control nextControl = ctl.Parent.GetNextControl(ctl, false);
                        //rtb.Focus();
                    }
                }
                else
                {
                    RadMessageBox.Show("You need to fill number!");
                    lbl.Text = "";
                }
                

            }
            else
            {
                lbl.Text = "";
            }
        }

        private void txtBtwInvoicing_Leave(object sender, EventArgs e)
        {
            RadTextBox rtb = (RadTextBox)sender;
            string nameLabel = "";
            nameLabel = "label" + rtb.Name.Replace("txt", "");

            RadLabel lbl = new RadLabel();
            if (this.Controls.Find(nameLabel, true)[0] != null)
                lbl = (RadLabel)this.Controls.Find(nameLabel, true)[0];

            if (rtb.Text != "")
            {
                AccTaxBUS acBus = new AccTaxBUS();
                AccTaxModel acm = new AccTaxModel();
                int n;
                bool isNumeric = int.TryParse(rtb.Text, out n);

                if (isNumeric == true)
                {
                    acm = acBus.GetTaxByID(Convert.ToInt32(rtb.Text));
                    if (acm != null)
                    {

                        lbl.Text = acm.descTax;
                        // xAccountSplit = lam.numberLedgerAccount;

                        Control ctl = (Control)sender;
                        Control nextControl = ctl.Parent.GetNextControl(ctl, true);
                    }
                    else
                    {
                        RadMessageBox.Show("Wrong Payment");
                        lbl.Text = "";
                        //Control ctl = (Control)sender;
                        //Control nextControl = ctl.Parent.GetNextControl(ctl, false);
                        //rtb.Focus();
                    }
                }
                else
                {
                    RadMessageBox.Show("You need to fill number!");
                    lbl.Text = "";
                }


            }
            else
            {
                lbl.Text = "";
            }
        }

        private void txtDailyInvoice_Leave(object sender, EventArgs e)
        {
            RadTextBox rtb = (RadTextBox)sender;
            string nameLabel = "";
            nameLabel = "label" + rtb.Name.Replace("txt", "");

            RadLabel lbl = new RadLabel();
            if (this.Controls.Find(nameLabel, true)[0] != null)
                lbl = (RadLabel)this.Controls.Find(nameLabel, true)[0];

            if (rtb.Text != "")
            {
                AccDailyBUS acBus = new AccDailyBUS(Login._bookyear);
                AccDailyModel acm = new AccDailyModel();
                int n;
                bool isNumeric = int.TryParse(rtb.Text, out n);

                if (isNumeric == true)
                {
                    acm = acBus.GetDailysById(Convert.ToInt32(rtb.Text));
                    if (acm != null)
                    {

                        lbl.Text = acm.descDaily;
                        // xAccountSplit = lam.numberLedgerAccount;

                        Control ctl = (Control)sender;
                        Control nextControl = ctl.Parent.GetNextControl(ctl, true);
                    }
                    else
                    {
                        RadMessageBox.Show("Wrong Payment");
                        lbl.Text = "";
                        //Control ctl = (Control)sender;
                        //Control nextControl = ctl.Parent.GetNextControl(ctl, false);
                        //rtb.Focus();
                    }
                }
                else
                {
                    RadMessageBox.Show("You need to fill number!");
                    lbl.Text = "";
                }
            }
            else
            {
                lbl.Text = "";
            }
        }

        
    }

        #endregion Kretanje Enterom
      

}
     

