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

namespace GUI
{
    public partial class frmDifferenceAmount : Telerik.WinControls.UI.RadForm
    {

        public AccSettingsBUS asb;
        public AccSettingsModel asm;
        public decimal amount;
        public decimal xamount;
        public BindingList<AccOpenLinesModel> ollines;
       // public BindingList<AccOpenLinesModel> lookopenlines;
        public BindingList<AccLineModel> multimodel;
        AccLineModel linemodel;
        private int idDaily;
        private int xDaily;
        public string st = "";
        private string bookSide = "";
        private string invoiceNr;
        private string customerId;

        public frmDifferenceAmount(BindingList<AccOpenLinesModel> lookopenlines, decimal xamount, int xDaily, BindingList<AccLineModel> multimodelA, string invoice, string customer)
        {
            ollines = new BindingList<AccOpenLinesModel>();
            multimodel = multimodelA;
            ollines = lookopenlines;
            idDaily = xDaily;
            amount = xamount;
            invoiceNr = invoice;
            customerId = customer;


            InitializeComponent();
        }

        private void frmDifferenceAmount_Load(object sender, EventArgs e)
        {
            
            linemodel = new AccLineModel();

            if (multimodel == null)
                multimodel = new BindingList<AccLineModel>();
           
            txtAmount.Text = amount.ToString();

            labelKonto.Text = "";
            setTranslation();
            //==== read account settings
            asb = new AccSettingsBUS();
            asm = new AccSettingsModel();
            asm = asb.GetSettingsByID(DateTime.Now.Year.ToString());
            //==================
            if (asm != null)
            {
                if (asm.paymentDiferenceAccount != null && asm.paymentDiferenceAccount != "")
                {
                    txtAccount.Text = asm.paymentDiferenceAccount;
                    txtAccount_Leave(sender, e);
                }

                radButton1.Text = asm.paymentDiferenceAccount.ToString();
                radButton2.Text = asm.defTransferingAcc.ToString();
            }
            gridLookup.DataSource = ollines;
            gridLookup.Show();
        }
        private void setTranslation()
        {

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                lblAction.Text = resxSet.GetString("Action");
                lblAmount.Text = resxSet.GetString("Unbooked amount");
                btnOk.Text = resxSet.GetString("OK");
                btnCancel.Text = resxSet.GetString("Cancel");
                rbOpenLine.Text = resxSet.GetString("Leave line open");
                rbAccount.Text = resxSet.GetString("Booked on account");
               
            }
        }

        private void gridLookup_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < gridLookup.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (gridLookup.Columns[i].HeaderText != null && resxSet.GetString(gridLookup.Columns[i].HeaderText) != null)
                        gridLookup.Columns[i].HeaderText = resxSet.GetString(gridLookup.Columns[i].HeaderText);
                }
            }
            if (asm.isVat == false)
            {
               // gridLookup.Columns["btw"].IsVisible = false;
            }
        }
        #region Button CLick
        private void btnAccount_Click(object sender, EventArgs e)
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
                labelKonto.Text = genmX.descLedgerAccount;

            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            if (rbAccount.CheckState == CheckState.Checked)
            {

                if (txtAccount.Text != "")
                {
                    st = txtAccount.Text;
                    linemodel.numberLedAccount = txtAccount.Text;
                    //if (ollines != null)
                    //{
                    //    for (int w = 0; w < ollines.Count; w++)
                    //    {
                    //        if (ollines[w].iselected == true)
                    //        {
                                linemodel.idClientLine =  customerId;          //ollines[w].idDebCre;
                                linemodel.invoiceNr = invoiceNr;    //ollines[w].invoiceOpenLine;
                                //linemodel.descLine = ollines[w].descOpenLine;
                                //if (ollines[w].typeOpenLine == "D")
                                    if (amount > 0)
                                       linemodel.creditLine = amount;
                                    else
                                        linemodel.debitLine = amount*-1;
                        //        else
                        //            if (amount > 0)
                        //               linemodel.debitLine = amount;
                        //            else
                        //                linemodel.creditLine = amount*-1;
                        //    }
                        //}
                 }
                 else
                 {
                        linemodel.numberLedAccount ="9999999";
                        linemodel.idClientLine = customerId;
                        linemodel.invoiceNr = invoiceNr;
                        //if (bookSide != null && bookSide != "")
                        //{
                        //    if (bookSide == "D")
                                if (amount > 0)
                                    linemodel.creditLine = amount;
                                else
                                    linemodel.debitLine = amount * -1;
                        //}
                        //else
                        //{
                        //    if (amount > 0)
                        //        linemodel.creditLine = amount;
                        //    else
                        //        linemodel.debitLine = amount * -1;
                      //}

                  }
               // }
                //else
                //{
                //    st = "";

                //}
            }
            else
            {
                if (rbOpenLine.CheckState == CheckState.Checked)
                {
                    if (ollines != null)
                    {
                        for (int w = 0; w < ollines.Count; w++)
                        {
                            if (ollines[w].iselected == true)
                            {
                                linemodel.idClientLine = ollines[w].idDebCre;
                                linemodel.invoiceNr = ollines[w].invoiceOpenLine;
                                linemodel.numberLedAccount = ollines[w].account;
                                linemodel.descLine = ollines[w].descOpenLine;
                                if (ollines[w].typeOpenLine == "D")
                                    if (amount > 0)
                                        linemodel.creditLine = amount;
                                               //Convert.ToDecimal(ollines[w].debitOpenLine) - amount;
                                    else
                                        linemodel.debitLine = amount * -1;    //Convert.ToDecimal(ollines[w].creditOpenLine)- amount; //Convert.ToDecimal(ollines[w].creditOpenLine) + xamount;
                                else
                                    if (amount > 0)
                                        linemodel.debitLine = amount;    //Convert.ToDecimal(ollines[w].creditOpenLine) - amount; 
                                    else
                                        linemodel.creditLine = amount * -1;    //Convert.ToDecimal(ollines[w].debitOpenLine) - amount; // Convert.ToDecimal(ollines[w].debitOpenLine) + xamount;
                            }
                        }
                    }
                }
            }

           
            linemodel.dtLine = DateTime.Now;
            linemodel.idAccDaily = idDaily;
            //if (Convert.ToDecimal(txtAmount.Text) > 0)
            //{
            //   // linemodel.debitLine = Convert.ToDecimal(txtAmount.Text);
            //    linemodel.creditLine = Convert.ToDecimal(txtAmount.Text);
            //}
            //else
            //{
            //  //  linemodel.creditLine = Convert.ToDecimal(txtAmount.Text);
            //    linemodel.debitLine = Convert.ToDecimal(txtAmount.Text);
            //}
            multimodel.Add(linemodel);
            //    }
            //}
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion Button Click

        #region Fields Evants
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
                    labelKonto.Text = genmX.descLedgerAccount;
                   
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
                        labelKonto.Text = lam.descLedgerAccount;
                       
                    }
                    else
                    {
                        RadMessageBox.Show("Wrong accont");
                        txtAccount.Text = "";
                        labelKonto.Text = "";
                    }
                }
                else
                {
                    labelKonto.Text = "";
             
                }

            }
             
        }

        private void txtAccount_Leave(object sender, EventArgs e)
        {
            if (txtAccount.Text != "")
            {
                LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lam = new LedgerAccountModel();

                lam = ledbus.GetAccount(txtAccount.Text, Login._bookyear);
                if (lam != null)
                {
                    labelKonto.Text = lam.descLedgerAccount;
                    
                }
            }
            else
            {
                labelKonto.Text = "";
            }
        }
        #endregion Field Evants

        private void rbOpenLine_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (rbOpenLine.CheckState == CheckState.Checked)
            {
                gridLookup.Visible = true;
                //radbDiff1.Visible = false;
                //radbDiff2.Visible = false;
                radButton1.Visible = false;
                radButton2.Visible = false;
            }
            else
            {
                gridLookup.Visible = false;
                radButton1.Visible = true;
                radButton2.Visible = true;
                //radbDiff1.Visible = true;
                //radbDiff2.Visible = true;
                txtAccount.Visible = true;
                btnAccount.Visible = true;
                labelKonto.Visible = true;
            }
        }

        private void rbAccount_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
             if (rbAccount.CheckState == CheckState.Checked)
             {
                 if (ollines != null)
                 {
                     txtAccount.Visible = true;
                     btnAccount.Visible = true;
                     labelKonto.Visible = true;
                     gridLookup.Visible = false;
                     //radbDiff1.Visible = true;
                     //radbDiff2.Visible = true;
                     radButton1.Visible = true;
                     radButton2.Visible = true;
                     
                 }
                 else
                 {
                     txtAccount.Visible = true;
                     btnAccount.Visible = true;
                     labelKonto.Visible = true;
                     gridLookup.Visible = false;
                     //radbDiff1.Visible = true;
                     //radbDiff2.Visible = true;
                     radButton1.Visible = true;
                     radButton2.Visible = true;
                 }
             }
             else
             {
                 txtAccount.Visible = false;
                 btnAccount.Visible = false;
                 labelKonto.Visible = false;
                 //radbDiff1.Visible = false;
                 //radbDiff2.Visible = false;
                 radButton1.Visible = false;
                 radButton2.Visible = false;
             }
        }

        private void radbDiff1_ToggleStateChanged(object sender, StateChangedEventArgs args)

        {
            txtAccount.Visible = true;
            btnAccount.Visible = true;
            labelKonto.Visible = true;
            if (radbDiff1.CheckState == CheckState.Checked)
            {
                if (asm.paymentDiferenceAccount != null && asm.paymentDiferenceAccount != "")
                {
                    txtAccount.Text = asm.paymentDiferenceAccount.ToString();

                }
            }
            else
            {
                txtAccount.Text = "";
            }
        }

        private void radbDiff2_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            txtAccount.Visible = true;
            btnAccount.Visible = true;
            labelKonto.Visible = true;
            if (radbDiff2.CheckState == CheckState.Checked)
            {
                if (asm.defTransferingAcc != null && asm.defTransferingAcc != "")
                {
                    txtAccount.Text = asm.defTransferingAcc.ToString();

                }
            }
            else
            {
                txtAccount.Text = "";
            }
        }

        private void txtAccount_TextChanged(object sender, EventArgs e)
        {
            if (txtAccount.Text != "")
            {
                LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lam = new LedgerAccountModel();

                lam = ledbus.GetAccount(txtAccount.Text, Login._bookyear);
                if (lam != null)
                {
                    labelKonto.Text = lam.descLedgerAccount;
                    bookSide = lam.sideBooking;

                }
            }
            else
            {
                labelKonto.Text = "";
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            if (asm.paymentDiferenceAccount != null && asm.paymentDiferenceAccount != "")
            {
                txtAccount.Text = asm.paymentDiferenceAccount.ToString();

            }
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            if (asm.defTransferingAcc != null && asm.defTransferingAcc != "")
            {
                txtAccount.Text = asm.defTransferingAcc.ToString();

            }
        }

    }
}
