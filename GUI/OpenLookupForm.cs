using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Model;
using Telerik.WinControls.UI;
using System.Resources;
using System.IO;
using BIS.Business;

namespace GUI
{
    public partial class OpenLookupForm : Telerik.WinControls.UI.RadForm
    {
        public IModel selectedRow;
        string layoutLookup;
         public string idClient;
         public List<AccOpenLinesModel> oplmodel;
         public BindingList<AccLineModel> multimodel;
         private decimal totselect = 0;
         private decimal totamount = 0;
         private decimal amount = 0;
         AccSettingsBUS asb;
         AccSettingsModel asm;
         List<AccOpenLinesModel> modelp;
         private string bookSide = "";
         AccLineModel linemodel;
         private int idDaily;
         private decimal diffamount=0;
         private string side;
         private bool combo=false;
         private string gridSide = "";
         private int noselected = 0;
         private decimal sum_nonselected = 0;
        

         BindingList<InvoiceCombo> listaInvCombo = new BindingList<InvoiceCombo>();
         public OpenLookupForm(List<AccOpenLinesModel> model, string nameForm, string Customer, decimal amnt, int xDaily, BindingList<AccLineModel> mmodel, string xside)
        {
            InitializeComponent();

      
           
            modelp = model;
            oplmodel = new List<AccOpenLinesModel>();

            ddlInvoices.DataSource = listaInvCombo;
            ddlInvoices.DisplayMember = "invoice";
            ddlInvoices.ValueMember = "broj";

            gridLookup.DataSource = modelp;
            gridLookup.ValueChanged -= gridLookup_ValueChanged;
            gridLookup.CellValueChanged -= MasterTemplate_CellValueChanged;

             if (gridLookup != null)
                 if (gridLookup.RowCount > 0)
                 {
                     for (int b = 0; b < gridLookup.RowCount; b++)
                     {
                         if (gridLookup.Rows[b].Cells["debit"].Value != null && gridLookup.Rows[b].Cells["credit"].Value != null)
                             gridLookup.Rows[b].Cells["closeamount"].Value = Convert.ToDecimal(gridLookup.Rows[b].Cells["debit"].Value) - Convert.ToDecimal(gridLookup.Rows[b].Cells["credit"].Value);
                     }
                 }
             gridLookup.ValueChanged += gridLookup_ValueChanged;
             gridLookup.CellValueChanged += MasterTemplate_CellValueChanged;

            gridLookup.AllowAutoSizeColumns = true;
            idClient = Customer;
            amount = amnt;
            idDaily = xDaily;
            multimodel = mmodel;
            side = xside;
            //set name form and icon
            this.Name = nameForm;
            this.Icon = Login.iconForm;
            setTranslation();

            

            //==== read account settings =======================================================================
            asb = new AccSettingsBUS();
            asm = new AccSettingsModel();
            asm = asb.GetSettingsByID(DateTime.Now.Year.ToString());
            //==================
            if (asm != null)
            {
                if (asm.paymentDiferenceAccount != null && asm.paymentDiferenceAccount != "")
                {
                    txtAccount.Text = asm.paymentDiferenceAccount;
                   // txtAccount.Leave += txtAccount_Leave(object sender, EventArgs e);
                }

                btnPdiff.Text = asm.paymentDiferenceAccount.ToString();
                btnTransf.Text = asm.defTransferingAcc.ToString();
            }
            //==================================================================================================
            TranslationBUS tb = new TranslationBUS();
            List<TranslationModel> tm = new List<TranslationModel>();
            tm = tb.CheckIfTranslationExists(Login._user.lngUser, nameForm);
            if(tm!=null)
            {
                if(tm.Count>0)
                {
                    nameForm = tm[0].stringKey;
                }
            }

            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_lookup")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_lookup"));

            }

            txtCloseAmount.Value = amount;

            layoutLookup = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_lookup\\" + nameForm.Replace("frm", "")+".xml");
             List<RadListDataItem> dataitems = new List<RadListDataItem>();
           

            //do translate form form text 
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(nameForm)!=null)
                     this.Text = resxSet.GetString(nameForm);
                if (resxSet.GetString(radMenuItemSaveLookupLayout.Text) != null)
                    radMenuItemSaveLookupLayout.Text = resxSet.GetString(radMenuItemSaveLookupLayout.Text);
            }

            //============================
            //GridViewCheckBoxColumn checkBoxColumn = new GridViewCheckBoxColumn();
            //checkBoxColumn.DataType = typeof(int);
            //checkBoxColumn.Name = "selected";
            //checkBoxColumn.FieldName = "iselected";
            //checkBoxColumn.HeaderText = "Select 11";
            //gridLookup.MasterTemplate.Columns.Add(checkBoxColumn);
            //checkBoxColumn.EditMode = EditMode.OnValueChange;
            //=============================================
            if (File.Exists(layoutLookup))
            {
                gridLookup.LoadLayout(layoutLookup);
            }
            if (idClient != "")
            {
                AccDebCreBUS debpers = new AccDebCreBUS();
                AccDebCreModel pm1 = new AccDebCreModel();
                ClientBUS cb = new ClientBUS();
                ClientModel cm = new ClientModel();
                PersonBUS pbs = new PersonBUS();
                PersonModel pmd = new PersonModel();
                pm1 = debpers.GetCustomerByAccCode(idClient);
                if (pm1 != null)
                {
                    if (pm1.idClient != null && pm1.idContPerson == 0)
                    {
                        cm = cb.GetClient(pm1.idClient);
                        if (cm != null)
                        {
                            labelClient.Text = cm.accountCodeClient + " " + cm.nameClient;  
                           // labelClient.Text = "";
                        }
                        else
                        {
                            labelClient.Text = "";
                        }
                    }
                    else
                    {
                        if (pm1.idContPerson != null && pm1.idClient == 0)
                            pmd = pbs.GetPerson(pm1.idContPerson);
                        if (pmd != null)
                        {
                            labelClient.Text = idClient + " " +pmd.firstname + " " + pmd.midname + " " + pmd.lastname;
                           // labelClient.Text = "";

                        }
                        else
                        {
                            labelClient.Text = "";

                        }

                    }

                }
            }

        }


        // do on double click
        private void radGridView1_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            //GridViewRowInfo info = this.gridLookup.CurrentRow;
            //if (e.RowIndex >= 0)
            //{
            //    //set selected row in model so you can use any data you'll need
            //    selectedRow = (IModel)info.DataBoundItem;
            //    this.DialogResult = DialogResult.Yes;
            //    this.Close();
                
            //}
        }

        //sets column width
        private void gridLookup_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            var grid = sender as RadGridView;

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                foreach (var column in grid.Columns)
                {
                     if (resxSet.GetString(column.HeaderText) != null && resxSet.GetString(column.HeaderText) != "")
                        column.HeaderText = resxSet.GetString(column.HeaderText);

                }
            }
         
           
        }

        private void radMenuItemSaveLookupLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutLookup))
            {
                File.Delete(layoutLookup);
            }
            gridLookup.SaveLayout(layoutLookup);
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("You have successfully save layout!") != null)
                    RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
                else
                    RadMessageBox.Show("You have successfully save layout!");
            }
        }
        private void gridLookup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5)
            {
                btnGetLines.PerformClick();
            }
            else
            {
                if (e.KeyData == Keys.Escape)
                {
                    DialogResult dr = RadMessageBox.Show("Do you want to Exit this form ?", "Delete", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
            }
        //    if (e.KeyData == Keys.Enter)  // namesteno da se i sa ENTER bira slog
        //    {
        //        //totamount = 0;
        //        //for (int i = 0; i < gridLookup.Rows.Count; i++)
        //        //{

        //        //    if (Convert.ToBoolean(gridLookup.Rows[i].Cells["selected"].Value) == true) ;
        //        //    {
        //        //        totamount = totamount + Convert.ToDecimal(gridLookup.Rows[i].Cells["closeamount"].Value);
        //        //    }


        //        //}
        //        //decimal diff = 0;
        //        //diff = Convert.ToDecimal(txtCloseAmount.Text) - totamount;
        //        //txtDiffAmount.Value = diff;
                
        //    }

        //    if (e.KeyData == Keys.Escape)
        //    {
        //        this.Close();
        //    }
        }

        private void gridLookup_ViewCellFormatting(object sender, CellFormattingEventArgs e)
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
        private void btnGetLines_Click(object sender, EventArgs e)
        {
            if (combo == false)
            {
                List<RadListDataItem> dataItems = new List<RadListDataItem>();
                RadListDataItem dataItem = new RadListDataItem();
                //RadListDataItem term = new RadListDataItem();

                noselected = 0;  //=== broj selektovanih slogova
                listaInvCombo.Clear();


                if (modelp.Count > 0)   //=== puni kombo sa brojevima faktura koje su selektovane
                {
                    for (int q = 0; q < gridLookup.Rows.Count; q++)
                    {
                        if (Convert.ToBoolean(gridLookup.Rows[q].Cells["selected"].Value) == true)
                        {
                            InvoiceCombo ic = new InvoiceCombo();
                            noselected++;
                            ic.invoice = gridLookup.Rows[q].Cells["invoice"].Value.ToString();
                            ic.broj = Convert.ToInt32(gridLookup.Rows[q].Cells["term"].Value.ToString());

                            listaInvCombo.Add(ic);


                            dataItem = new RadListDataItem();

                        }
                    }
                }

            }
            // ddlInvoices.DataSource = dataItems;
            //ddlInvoices.ValueMember = dataItem.Text;

            linemodel = new AccLineModel();
            if (rbAccount.CheckState == CheckState.Checked)
            {

                if (txtAccount.Text != "")
                {
                    diffamount = Convert.ToDecimal(txtDiffAmount.Text);
                    txtDiffAmount.Value = Convert.ToDecimal("0,00");
                }
            }
            if (rbOpenLine.CheckState == CheckState.Checked)
            {

                diffamount = Convert.ToDecimal(txtDiffAmount.Text);
                txtDiffAmount.Value = Convert.ToDecimal("0,00");

            }

            //========================
            if (txtDiffAmount.Text != "0,00")
            {


                DialogResult dr = RadMessageBox.Show("Enter option for difference" + "  " + txtDiffAmount.Text, "Enter", MessageBoxButtons.OK);
                if (dr == DialogResult.OK)
                {
                    rbAccount.Visible = true;
                    rbOpenLine.Visible = true;
                    rbOpenLine.Focus();
                    combo = true;
                    return;

                }
            }
            AccLineModel bookedDiff = new AccLineModel();
            if (rbAccount.IsChecked)
            {

                if (txtAccount.Text != "")
                {
                    bookedDiff = new AccLineModel();
                    bookedDiff.numberLedAccount = txtAccount.Text;
                    bookedDiff.idClientLine = idClient;
                    bookedDiff.dtLine = DateTime.Now;
                    bookedDiff.idAccDaily = idDaily;
                    bookedDiff.descLine = " ";
                }
                else
                {
                    bookedDiff.numberLedAccount = "999999";
                    bookedDiff.idClientLine = idClient;
                    linemodel.dtLine = DateTime.Now;
                    linemodel.descLine = " ";
                    linemodel.idAccDaily = idDaily;
                }

            }
            if (rbOpenLine.IsChecked) 
            {
                if (ddlInvoices.SelectedItem.Text != null) 
                {
                    bookedDiff.numberLedAccount = ddlInvoices.SelectedItem.Text;
                }
            }
            decimal diff = amount;
            for (int q = 0; q < gridLookup.Rows.Count; q++)
            {
                if (Convert.ToBoolean(gridLookup.Rows[q].Cells["selected"].Value) == true)
                {
                    linemodel = new AccLineModel();
                    try
                    {
                        linemodel.numberLedAccount = modelp[q].invoiceOpenLine;
                    }
                    catch (Exception)
                    {

                    }
                    if (combo)
                    {
                        if (linemodel.numberLedAccount == bookedDiff.numberLedAccount)
                        {
                            if (gridLookup.Rows[q].Cells["invoice"].Value.ToString() != "")
                                bookedDiff.invoiceNr = gridLookup.Rows[q].Cells["invoice"].Value.ToString();

                            bookedDiff.descLine = gridLookup.Rows[q].Cells["description"].Value.ToString();
                            if (gridLookup.Rows[q].Cells["cost"].Value.ToString() != "")
                                bookedDiff.idCostLine = gridLookup.Rows[q].Cells["cost"].Value.ToString();
                            if (gridLookup.Rows[q].Cells["project"].Value.ToString() != "")
                                bookedDiff.idProjectLine = gridLookup.Rows[q].Cells["project"].Value.ToString();
                            bookedDiff.numberLedAccount = modelp[q].account;
                            if (gridLookup.Rows[q].Cells["client"].Value.ToString() != "")
                                bookedDiff.idClientLine = gridLookup.Rows[q].Cells["client"].Value.ToString();
                            try
                            {
                                bookedDiff.numberLedAccount = modelp[q].account;
                                bookedDiff.term = modelp[q].term;
                            }
                            catch (Exception)
                            {

                            }
                            continue;
                        }
                    }
                    if (gridLookup.Rows[q].Cells["invoice"].Value.ToString() != "")
                        linemodel.invoiceNr = gridLookup.Rows[q].Cells["invoice"].Value.ToString();

                    linemodel.descLine = gridLookup.Rows[q].Cells["description"].Value.ToString();
                    if (gridLookup.Rows[q].Cells["cost"].Value.ToString() != "")
                        linemodel.idCostLine = gridLookup.Rows[q].Cells["cost"].Value.ToString();
                    if (gridLookup.Rows[q].Cells["project"].Value.ToString() != "")
                        linemodel.idProjectLine = gridLookup.Rows[q].Cells["project"].Value.ToString();
                    linemodel.numberLedAccount = modelp[q].account;
                    if (gridLookup.Rows[q].Cells["client"].Value.ToString() != "")
                        linemodel.idClientLine = gridLookup.Rows[q].Cells["client"].Value.ToString();
                    try
                    {
                        linemodel.numberLedAccount = modelp[q].account;
                        linemodel.term = modelp[q].term;
                    }
                    catch (Exception) 
                    {
                    
                    }
                    decimal closingdiff = 0;
                    try
                    {                   
                        closingdiff = Convert.ToDecimal(gridLookup.Rows[q].Cells["closeamount"].Value.ToString());
                    }
                    catch(Exception)
                    {
                    
                    }
                    if(closingdiff!=0)
                        diff -= closeLine(linemodel,closingdiff);

                    multimodel.Add(linemodel);
                }
            }
            if (combo) 
            {
                if (diff > 0) 
                {
                    bookedDiff.creditLine = Math.Abs(diff);
                    multimodel.Add(bookedDiff);
                }
                if (diff < 0) 
                {
                    bookedDiff.debitLine = Math.Abs(diff);
                    multimodel.Add(bookedDiff);
                }
            }
            this.DialogResult = DialogResult.Yes;

        }
        private decimal closeLine(AccLineModel linemodel, decimal sum) 
        {
            if (sum > 0)
            {
                linemodel.creditLine = Math.Abs(sum);
                return sum;
            }
            if(sum < 0)
            {
                linemodel.debitLine = Math.Abs(sum);
                return sum;
            }
            return 0;
        }
        private void btnGetLines_Click1(object sender, EventArgs e)
        {
            if (combo == false)
            {
                List<RadListDataItem> dataItems = new List<RadListDataItem>();
                RadListDataItem dataItem = new RadListDataItem();
                //RadListDataItem term = new RadListDataItem();

                noselected = 0;  //=== broj selektovanih slogova
                listaInvCombo.Clear();

             
                if (modelp.Count > 0)   //=== puni kombo sa brojevima faktura koje su selektovane
                {
                    for (int q = 0; q < gridLookup.Rows.Count; q++)
                    {
                        if (Convert.ToBoolean(gridLookup.Rows[q].Cells["selected"].Value) == true)
                        {
                            InvoiceCombo ic = new InvoiceCombo();
                            noselected++;
                            ic.invoice = gridLookup.Rows[q].Cells["invoice"].Value.ToString();
                            ic.broj = Convert.ToInt32(gridLookup.Rows[q].Cells["term"].Value.ToString());
                      
                            listaInvCombo.Add(ic);

 
                            dataItem = new RadListDataItem();
                      
                        }
                    }
                }
               
            }
           // ddlInvoices.DataSource = dataItems;
            //ddlInvoices.ValueMember = dataItem.Text;

            linemodel = new AccLineModel();
                if (rbAccount.CheckState == CheckState.Checked)
                {

                    if (txtAccount.Text != "")
                    {
                        diffamount = Convert.ToDecimal(txtDiffAmount.Text);
                        txtDiffAmount.Value = Convert.ToDecimal("0,00");
                    }
                }
                if (rbOpenLine.CheckState == CheckState.Checked)
                {

                        diffamount = Convert.ToDecimal(txtDiffAmount.Text);
                        txtDiffAmount.Value = Convert.ToDecimal("0,00");
                    
                }
          
                //========================
                if (txtDiffAmount.Text != "0,00")
                {


                    DialogResult dr = RadMessageBox.Show("Enter option for difference" + "  " + txtDiffAmount.Text, "Enter", MessageBoxButtons.OK);
                    if (dr == DialogResult.OK)
                    {
                        rbAccount.Visible = true;
                        rbOpenLine.Visible = true;
                        rbOpenLine.Focus();
                        combo = true;
                        return;

                    }
                }
                else
                {
               
                    //========================  ako je tacan iznos tj razlika je 0
                    if (rbOpenLine.CheckState == CheckState.Unchecked)  
                    {
                        if (modelp.Count > 0)
                        {
                            decimal razl = 0;
                            decimal total_diff = amount;
                            //foreach (AccOpenLinesModel itmol in oplinemodel)
                            for (int q = 0; q < gridLookup.Rows.Count; q++)
                            {
                                if (Convert.ToBoolean(gridLookup.Rows[q].Cells["selected"].Value) == true)
                                {
                                    linemodel = new AccLineModel();
                                    if (gridLookup.Rows[q].Cells["invoice"].Value.ToString() != "")
                                        linemodel.invoiceNr = gridLookup.Rows[q].Cells["invoice"].Value.ToString();

                                    linemodel.descLine = gridLookup.Rows[q].Cells["description"].Value.ToString();
                                    if (gridLookup.Rows[q].Cells["cost"].Value.ToString() != "")
                                        linemodel.idCostLine = gridLookup.Rows[q].Cells["cost"].Value.ToString();
                                    if (gridLookup.Rows[q].Cells["project"].Value.ToString() != "")
                                        linemodel.idProjectLine = gridLookup.Rows[q].Cells["project"].Value.ToString();
                                    linemodel.numberLedAccount = modelp[q].account;
                                    if (gridLookup.Rows[q].Cells["client"].Value.ToString() != "")
                                        linemodel.idClientLine = gridLookup.Rows[q].Cells["client"].Value.ToString();
                                    if (gridLookup.Rows[q].Cells["closeamount"].Value.ToString() != "")
                                        razl = Convert.ToDecimal(gridLookup.Rows[q].Cells["closeamount"].Value);// Convert.ToDecimal(modelp[q].debitOpenLine) - Convert.ToDecimal(modelp[q].creditOpenLine);
                                    linemodel.numberLedAccount = modelp[q].account;
                                    linemodel.term = modelp[q].term;
                                    if (Math.Abs(razl) < Math.Abs(total_diff))
                                    {
                                        if (side == "C")
                                        {
                                            linemodel.debitLine = Math.Abs(razl); // * -1;
                                            total_diff = Math.Abs(total_diff) - Math.Abs(linemodel.debitLine);
                                        }
                                        else
                                        {
                                            linemodel.creditLine = Math.Abs(razl);
                                            total_diff = Math.Abs(total_diff) - Math.Abs(linemodel.creditLine);
                                        }
                                    }
                                    else
                                    {
                                        if (rbOpenLine.CheckState == CheckState.Checked)
                                        {
                                            if (side == "C")
                                            {
                                                linemodel.debitLine = Math.Abs(razl) - Math.Abs(total_diff); // * -1;
                                                total_diff = Math.Abs(total_diff) - Math.Abs(linemodel.debitLine);
                                            }
                                            else
                                            {
                                                linemodel.creditLine = Math.Abs(razl) - Math.Abs(total_diff); //Math.Abs(razl);
                                                total_diff = Math.Abs(total_diff) - Math.Abs(linemodel.creditLine);
                                            }
                                        }
                                        else
                                        {
                                            if (side == "C")
                                            {
                                                linemodel.debitLine = Math.Abs(razl); // * -1;
                                                total_diff = Math.Abs(total_diff) - Math.Abs(linemodel.debitLine);
                                                diffamount = total_diff;
                                            }
                                            else
                                            {
                                                linemodel.creditLine = Math.Abs(razl);
                                                total_diff = Math.Abs(total_diff) - Math.Abs(linemodel.creditLine);
                                                diffamount = total_diff;
                                            }
                                        }
                                    }

                                    multimodel.Add(linemodel);
                                    razl = 0;

                                }
                            }

                        }
                    }
                }
                if (rbAccount.CheckState == CheckState.Checked)  //=== zatvaranje preko konta
                {

                    if (txtAccount.Text != "")
                    {
                        linemodel = new AccLineModel();
                        linemodel.numberLedAccount = txtAccount.Text;
                        linemodel.idClientLine = idClient;          //ollines[w].idDebCre;
                        if (side == "D")
                        {
                            if (diffamount > 0)
                                linemodel.creditLine = Math.Abs(diffamount);
                            else
                                linemodel.debitLine = Math.Abs(diffamount);
                        }
                        else
                        {
                            if (diffamount > 0)
                                linemodel.debitLine = Math.Abs(diffamount); // *-1;
                            else
                                linemodel.creditLine = Math.Abs(diffamount);
                        }
                        linemodel.dtLine = DateTime.Now;
                        linemodel.idAccDaily = idDaily;
                        linemodel.descLine = " ";
                        multimodel.Add(linemodel);

                    }
                    else
                    {
                        linemodel.numberLedAccount = "999999";
                        linemodel.idClientLine = idClient;
                        if (side == "D")
                        {
                            if (diffamount > 0)
                                linemodel.creditLine = Math.Abs(diffamount);
                            else
                                linemodel.debitLine = Math.Abs(diffamount);
                        }
                        else
                        {
                            if (diffamount > 0)
                                linemodel.debitLine = Math.Abs(diffamount); // *-1;
                            else
                                linemodel.creditLine = Math.Abs(diffamount);
                        }
                      
                        linemodel.dtLine = DateTime.Now;
                        linemodel.descLine = " ";
                        linemodel.idAccDaily = idDaily;
                        multimodel.Add(linemodel);

                    }

                }
                else
                {
                    if (rbOpenLine.CheckState == CheckState.Checked)
                    {

                        decimal razl = 0;
                        decimal total_diff = amount;
                        if (modelp != null)
                        {
                            for (int q = 0; q < gridLookup.Rows.Count; q++)
                            {

                                if (Convert.ToBoolean(gridLookup.Rows[q].Cells["selected"].Value) == true)
                                {
                                    linemodel = new AccLineModel();
                                    gridSide = "";
                                    if (gridLookup.Rows[q].Cells["type"].Value.ToString() != "")
                                        gridSide = gridLookup.Rows[q].Cells["type"].Value.ToString();

                                    if (gridLookup.Rows[q].Cells["invoice"].Value.ToString() != "")
                                        linemodel.invoiceNr = gridLookup.Rows[q].Cells["invoice"].Value.ToString();

                                    linemodel.descLine = gridLookup.Rows[q].Cells["description"].Value.ToString();
                                    if (gridLookup.Rows[q].Cells["cost"].Value.ToString() != "")
                                        linemodel.idCostLine = gridLookup.Rows[q].Cells["cost"].Value.ToString();
                                    if (gridLookup.Rows[q].Cells["project"].Value.ToString() != "")
                                        linemodel.idProjectLine = gridLookup.Rows[q].Cells["project"].Value.ToString();
                                    linemodel.numberLedAccount = modelp[q].account;
                                    if (gridLookup.Rows[q].Cells["client"].Value.ToString() != "")
                                        linemodel.idClientLine = gridLookup.Rows[q].Cells["client"].Value.ToString();
                                    if (gridLookup.Rows[q].Cells["closeamount"].Value.ToString() != "")
                                        razl = Convert.ToDecimal(gridLookup.Rows[q].Cells["closeamount"].Value);// Convert.ToDecimal(modelp[q].debitOpenLine) - Convert.ToDecimal(modelp[q].creditOpenLine);
                                    //if (Math.Abs(razl) < Math.Abs(total_diff))
                                    //{
                                        if (side == "C")
                                        {
                                            linemodel.debitLine = Math.Abs(razl); // * -1;
                                            total_diff = Math.Abs(total_diff) - Math.Abs(linemodel.debitLine);
                                        }
                                        else
                                        {
                                            linemodel.creditLine = Math.Abs(razl);
                                            total_diff = total_diff - linemodel.creditLine;
                                        }
                                    //}
                                    //else
                                    //{
                                    //    if (side == "C")
                                    //    {
                                    //        linemodel.debitLine = Math.Abs(Math.Abs(razl) - Math.Abs(total_diff)); // * -1;
                                    //      //  linemodel.debitLine = Math.Abs(Math.Abs(creditsum) - Math.Abs(razl));
                                    //        total_diff = total_diff - linemodel.debitLine;
                                    //    }
                                    //    else
                                    //    {
                                    //        linemodel.creditLine = Math.Abs(Math.Abs(razl) - Math.Abs(total_diff)); // Math.Abs(razl);
                                    //       // linemodel.creditLine = Math.Abs(Math.Abs(debitsum) - Math.Abs(razl));
                                    //        total_diff = total_diff - linemodel.creditLine;
                                    //    }
                                    //}

                                    linemodel.numberLedAccount = modelp[q].account;
                                    linemodel.bookingYear = Login._bookyear;
                                    linemodel.term = modelp[q].term;
                                    if (noselected == 1)   // ako je selektovana samo jedna stavka
                                    {
                                        if (Math.Abs(Convert.ToDecimal(txtCloseAmount.Text)) < Math.Abs(razl))
                                        {
                                            if (Convert.ToDecimal(txtCloseAmount.Text) < 0)
                                            {
                                                linemodel.debitLine = Math.Abs(Convert.ToDecimal(txtCloseAmount.Text)); // * -1;
                                                linemodel.creditLine = 0;
                                            }
                                            else
                                            {
                                                linemodel.creditLine = Math.Abs(Convert.ToDecimal(txtCloseAmount.Text));
                                                linemodel.debitLine = 0;
                                            }
                                        }

                                        else
                                        {
                                            if (side == "C")
                                            {
                                                linemodel.debitLine = Math.Abs(Convert.ToDecimal(txtCloseAmount.Text));
                                                linemodel.creditLine = 0;
                                            }
                                            else
                                            {
                                                linemodel.creditLine = Math.Abs(Convert.ToDecimal(txtCloseAmount.Text));
                                                linemodel.debitLine = 0;
                                            }
                                        }
                                        //}
                                    }
                                    else
                                    {
                                        if (ddlInvoices.SelectedItem == null)
                                        {
                                            RadMessageBox.Show("Select invoice, please !");
                                            return;
                                        }
                                        else
                                        {
                                            //=== ovde ispituje da li je ta stavka oznacena za zatvaranje ===//
                                            if (gridLookup.Rows[q].Cells["invoice"].Value.ToString() == ddlInvoices.SelectedItem.Text &&
                                                   Convert.ToInt32(gridLookup.Rows[q].Cells["term"].Value.ToString()) == Convert.ToInt32(ddlInvoices.SelectedValue))
                                            {
                                                //if (Math.Abs(diffamount) > Math.Abs(razl))
                                                //{
                                                //      translateRadMessageBox tr = new translateRadMessageBox();
                                                //      tr.translateAllMessageBox("Amount greater then closing item !");
                                                //      multimodel.Clear();
                                                //      return;
                                                //}
                                                //    if (razl < 0)
                                                sum_nonselected = dajiznos(ddlInvoices.SelectedItem.Text);  // uzima sumu svih selektovanih stavki osim te koja se zatvara
                                                decimal total = amount - sum_nonselected;
                                                if (side == "D")
                                                {
                                                    if (total > 0)
                                                    {
                                                        linemodel.creditLine = Math.Abs(total);//Math.Abs(Math.Abs(amount) - Math.Abs(sum_nonselected));
                                                        linemodel.debitLine = 0;
                                                    }
                                                    else
                                                    {
                                                        linemodel.debitLine = Math.Abs(total);//Math.Abs(Math.Abs(amount) - Math.Abs(sum_nonselected));
                                                        linemodel.creditLine = 0;
                                                    }
                                              //    linemodel.creditLine = Math.Abs(diffamount); // *-1;
                                                    // if (total_diff < 0)
                                               // linemodel.creditLine = Math.Abs(razl - diffamount); // *-1;
                                                    // linemodel.creditLine = Math.Abs(total_diff); // * -1;
                                                    //  else
                                                    //   linemodel.debitLine = (razl + diffamount);// *-1;
                                                    // linemodel.creditLine =  Math.Abs(total_diff);
                                                }
                                                else
                                                {
                                                    if (total < 0)
                                                    {
                                                        linemodel.debitLine = Math.Abs(total);// Math.Abs(Math.Abs(amount) - Math.Abs(sum_nonselected)); // - Math.Abs(creditsum)); Math.Abs(
                                                        linemodel.creditLine = 0;
                                                    }
                                                    else
                                                    {
                                                        linemodel.creditLine = Math.Abs(total);//Math.Abs(Math.Abs(amount) - Math.Abs(sum_nonselected));
                                                        linemodel.debitLine = 0;
                                                    }
                                            //    linemodel.debitLine = Math.Abs(diffamount);
                                                    //   if (diffamount < 0)
                                            //    linemodel.debitLine = Math.Abs(Math.Abs(razl) - Math.Abs(diffamount));
                                                    //linemodel.debitLine = Math.Abs(total_diff);
                                                    //  else
                                                    //     linemodel.creditLine = Math.Abs(Math.Abs(razl) - Math.Abs(diffamount));
                                                    // linemodel.debitLine = Math.Abs(total_diff);
                                                }
                                            }
                                            //==novvo kad unese suprotno da ne brlja ==
                                            else
                                            {
                                                if (side == "C" && gridSide == "D")
                                                {
                                                    linemodel.debitLine = Math.Abs(Convert.ToDecimal(razl));
                                                    linemodel.creditLine = 0;
                                                }
                                                else
                                                {
                                                    if (side == "D" && gridSide == "C")
                                                    {
                                                        linemodel.creditLine = Math.Abs(Convert.ToDecimal(razl));
                                                        linemodel.debitLine = 0;
                                                    }
                                                }

                                            }
                                            //=============================
                                        }
                                    }

                                    linemodel.dtLine = DateTime.Now;
                                    linemodel.idAccDaily = idDaily;
                                    multimodel.Add(linemodel);
                                }
                            }
                        }
                    }

                }

            
                    this.DialogResult = DialogResult.Yes;
                  //  this.Close();
            

        }

        private void MasterTemplate_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
     
        }

        bool isokvalc = false;
        bool isok = false;
        int noSelect = 0;

        private void gridLookup_ValueChanged(object sender, EventArgs e)
        {
            string aname = gridLookup.CurrentCell.ColumnInfo.Name;      // izracunava razliku izmedju debita i credita i sabira broj selektovanih stavki 
            
            if (this.gridLookup.ActiveEditor is RadCheckBoxEditor && aname=="selected")
            {
                if (isokvalc == false)
                {
                    isokvalc = true;
                    AccOpenLinesModel mod = (AccOpenLinesModel)gridLookup.CurrentRow.DataBoundItem;
                    decimal valueM = (decimal)mod.debitOpenLine - (decimal)mod.creditOpenLine;
                    //int id = (int)gridLookup.CurrentRow.Cells["selected"].Value;
                    bool chechstate = Convert.ToBoolean(gridLookup.ActiveEditor.Value);
                    if (chechstate == true)
                    {
                        gridLookup.CurrentRow.Cells["closeamount"].Value = Convert.ToDecimal(valueM);
                       // totselect += valueM;
                     
                            totselect = totselect + valueM;
                            noSelect++;
                         if (noSelect > 1 && Math.Abs(totselect) > Math.Abs(amount))
                         {
                             DialogResult dr = RadMessageBox.Show("You exceeded amount !! Proceed ?" , "Select", MessageBoxButtons.YesNo);
                             if (dr == DialogResult.No)
                             {
                                 totselect = totselect - valueM;
                                 noSelect--;
                                 chechstate = false;
                                 gridLookup.ActiveEditor.Value = 0;
                             }
                         }
                    }
                    else
                    {
                       // gridLookup.CurrentRow.Cells["closeamount"].Value = Convert.ToDecimal("0,00");
                        //totselect -= valueM;
                        totselect = totselect - valueM;
                        noSelect--;

                    }

                    // txtselCre.Text = totselect.ToString();
                    decimal diff = 0;
                    if (side == "D")
                        diff = Convert.ToDecimal(txtCloseAmount.Text) - Math.Abs(totselect);
                    else
                        diff = Math.Abs(Convert.ToDecimal(txtCloseAmount.Text)) + totselect;
                    //if (Convert.ToDecimal(txtCloseAmount.Text) < 0)
                    //    diff = Convert.ToDecimal(txtCloseAmount.Text) + totselect;
                    //else
                    //    diff = Convert.ToDecimal(txtCloseAmount.Text) - totselect;


                    txtDiffAmount.Value = diff;

                    isokvalc = false;
                }
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
        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(lblAmountclose.Text) != null)
                    lblAmountclose.Text = resxSet.GetString(lblAmountclose.Text);

                if (resxSet.GetString(lblDiff.Text) != null)
                    lblDiff.Text = resxSet.GetString(lblDiff.Text);

                if (resxSet.GetString(btnGetLines.Text) != null)
                    btnGetLines.Text = resxSet.GetString(btnGetLines.Text);
                
            }
        }

        #region difference
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
        #endregion difference

        private void MasterTemplate_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
          //  RadMessageBox.Show("www");
            string aaa = gridLookup.CurrentCell.ColumnInfo.Name;
            if (aaa == "closeamount")
            {
                totamount = 0;
                for (int i = 0; i < gridLookup.Rows.Count; i++)
                {

                    if (Convert.ToBoolean(gridLookup.Rows[i].Cells["selected"].Value) == true) 
                    {
                        totamount = totamount + Convert.ToDecimal(gridLookup.Rows[i].Cells["closeamount"].Value);
                    }


                }
                decimal diff = 0;
                diff = Convert.ToDecimal(txtCloseAmount.Text) - totamount;
                txtDiffAmount.Value = diff;
                
            }
        }

        private void btnPdiff_Click(object sender, EventArgs e)
        {
            if (asm.paymentDiferenceAccount != null && asm.paymentDiferenceAccount != "")
            {
                txtAccount.Text = asm.paymentDiferenceAccount.ToString();

            }
        }

        private void btnTransf_Click(object sender, EventArgs e)
        {
            if (asm.defTransferingAcc != null && asm.defTransferingAcc != "")
            {
                txtAccount.Text = asm.defTransferingAcc.ToString();

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
        private void rbOpenLine_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (rbOpenLine.CheckState == CheckState.Checked)
            {
              //  gridLookup.Visible = true;
                btnPdiff.Visible = false;
                btnTransf.Visible = false;
                ddlInvoices.Visible = true;
            }
            else
            {
              //  gridLookup.Visible = false;
                ddlInvoices.Visible = false;
                btnPdiff.Visible = true;
                btnTransf.Visible = true;
                txtAccount.Visible = true;
                btnAccount.Visible = true;
                labelKonto.Visible = true;
               
            }
        }
        private void rbAccount_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (rbAccount.CheckState == CheckState.Checked)
            {
                        if (modelp != null)
                        {
                            txtAccount.Visible = true;
                            btnAccount.Visible = true;
                            labelKonto.Visible = true;
                          //  gridLookup.Visible = false;
                            btnPdiff.Visible = true;
                            btnTransf.Visible = true;
                            ddlInvoices.Visible = false;

                        }
                        else
                        {
                            txtAccount.Visible = true;
                            btnAccount.Visible = true;
                            labelKonto.Visible = true;
                         //   gridLookup.Visible = false;
                            btnPdiff.Visible = true;
                            btnTransf.Visible = true;
                            ddlInvoices.Visible = false;
                        }
            }
            else
            {
                    txtAccount.Visible = false;
                    btnAccount.Visible = false;
                    labelKonto.Visible = false;
                    btnPdiff.Visible = false;
                    btnTransf.Visible = false;
                    ddlInvoices.Visible = true;
            }
        }

        private void rbOpenLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
                ddlInvoices.Focus();
            else
                if (e.KeyCode == Keys.Enter)
                    rbAccount.Focus();
                else
                {
                    if (e.KeyData == Keys.F5)
                    {
                        btnGetLines.PerformClick();
                    }
                    else
                    {
                        if (e.KeyData == Keys.Escape)
                        {
                            DialogResult dr = RadMessageBox.Show("Do you want to Exit this form ?", "Delete", MessageBoxButtons.YesNo);
                            if (dr == DialogResult.Yes)
                            {
                                this.Close();
                            }
                        }
                    }
                }
        }

       private void ddlInvoices_GotFocus( object sender, EventArgs e)
    {
        ddlInvoices.DropDownListElement.ArrowButton.PerformClick();
    }

       private void ddlInvoices_KeyDown(object sender, KeyEventArgs e)
       {
           if (e.KeyCode == Keys.Down)
               ddlInvoices.DropDownListElement.ArrowButton.PerformClick();
           else
               if (e.KeyCode == Keys.Tab)
                   btnGetLines.Focus();
               else
               {
                   if (e.KeyData == Keys.F5)
                   {
                       btnGetLines.PerformClick();
                   }
                   else
                   {
                       if (e.KeyData == Keys.Escape)
                       {
                           DialogResult dr = RadMessageBox.Show("Do you want to Exit this form ?", "Delete", MessageBoxButtons.YesNo);
                           if (dr == DialogResult.Yes)
                           {
                               this.Close();
                           }
                       }
                   }
               }
       }
       public class InvoiceCombo
       {
           public string invoice { get; set; }
           public int broj { get; set; }

           public InvoiceCombo()
           {
               this.invoice = String.Empty;
               this.broj = 0;
           }
       }
        private void ddlInvoices_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
       {

       }
  

       private decimal dajiznos(string invoice)
       {
           decimal iznos = 0;
           decimal razl = 0;
           if (modelp != null)
           {
               if (modelp.Count > 0)
               {
                   foreach (AccOpenLinesModel mm in modelp)
                   {
                       if (mm.iselected == true && mm.invoiceOpenLine != invoice)
                       {
                           razl = Convert.ToDecimal(mm.debitOpenLine) - Convert.ToDecimal(mm.creditOpenLine);
                           iznos = iznos + razl;

                       }

                   }
               }
           }
           return iznos;
       }


    }
}
