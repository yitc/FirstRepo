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
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.Data;

namespace GUI
{
    public partial class frmBTW : frmTemplate
    {
        public AccTaxModel tax;
        List<AccTaxValidityModel> taxValidity;
        public Int32 iID;
        public string scodeTax;
        public DateTime sstart;
        public Boolean modelChanged=false;
       // public Boolean isChanged = false;
        Boolean  isChanged= false;
        Boolean isAddNewTaks = false;
        Boolean isFirst = true;
        public string Namef;

        public frmBTW()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("BTW");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            iID = -1;
            InitializeComponent();
        }

        public frmBTW( AccTaxModel ctax)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("BTW");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            tax = ctax;
            iID = tax.idTax;
            InitializeComponent();
        }
        public frmBTW(int eID)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("BTW");
            }
            ribbonExampleMenu.Text = "";
            // this.Text = Namef;   //"";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
           
            iID = eID;
            InitializeComponent();
        }

        private void frmBTW_Load(object sender, EventArgs e)
        {
            radRibbonDocuments.Visibility = ElementVisibility.Collapsed;
            btnReport.Visibility = ElementVisibility.Collapsed;
            btnEmail.Visibility = ElementVisibility.Collapsed;
            btnWord.Visibility = ElementVisibility.Collapsed;
            btnNewMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
           // radRibbonTask.Visibility = ElementVisibility.Collapsed;
            radRibbonMemo.Visibility = ElementVisibility.Collapsed;
            btnDelTask.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;
           // radRibbonTask.Text = "Belastingtarief";
            radRibbonTask.Text = "Tax rate";
            btnNewTask.Text = "New";
            //btnWord.Image. = "GUI.Properties.Resources.Doc_add.png";
            btnNewTask.AccessibleName = "NewTax";
            btnNewTask.AccessibleDescription = "NewTax";
            btnNewTask.Visibility = ElementVisibility.Visible;
            btnNewTask.Click += radButton1_Click;
            setTranslation();
            taxValidity = new List<AccTaxValidityModel>();
            if (iID != -1)
            {
             
                // polja na formi
                txtIdTax.Text = tax.idTax.ToString();
                txtCodeTax.Text = tax.codeTax.ToString();
                txtDescTax.Text = tax.descTax.ToString();
                if (tax.typeTax == 1)
                {
                    rbIncl.CheckState = CheckState.Checked;
                }
                else
                {
                    if (tax.typeTax == 2)
                        rbExcl.CheckState = CheckState.Checked;
                }
                if (tax.numberLedAccount != "" && tax.numberLedAccount != null)
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel acm = new LedgerAccountModel();
                    acm = lab.GetAccount(tax.numberLedAccount, Login._bookyear);
                    if (acm != null)
                    {
                        txtAccount.Text = acm.numberLedgerAccount;
                        labelAccount.Text = acm.descLedgerAccount;
                        tax.numberLedAccount = acm.numberLedgerAccount;
                    }

                }
                   
                //==== kontrola da li je nesto knjizeno na btw (onad disebluje polja na formi za izmenu)
                AccLineBUS alb = new AccLineBUS(Login._bookyear);
                List<AccLineModel> alm = new List<AccLineModel>();
                if (tax.idTax != null && tax.idTax != 0)
                {
                    alm = alb.GetLinesByBTW(tax.idTax);
                    if (alm != null && alm.Count > 0)
                    {
                        txtCodeTax.Enabled = false;
                        txtAccount.Enabled = false;
                        btnAccount.Enabled = false;
                        txtDescTax.Enabled = false;
                        rbExcl.Enabled = false;
                        rbIncl.Enabled = false;
                    }
                }

                //

                //txtAccount.Text = tax.numberLedAccount.ToString();
                AccTaxValidityBUS acb = new AccTaxValidityBUS();
                scodeTax = tax.codeTax;
                // Cita podatke za grid
                taxValidity = acb.GetTaxValidity(scodeTax);
                rgvTaxValidity.DataSource = taxValidity;
                rgvTaxValidity.Show();
            }
            else
            {
                tax = new AccTaxModel();
                taxValidity = new List<AccTaxValidityModel>();
            }
            //if (rgvTaxValidity != null)
            //{
            //    GridViewDateTimeColumn column = (GridViewDateTimeColumn)this.rgvTaxValidity.Columns["startDate"];
            //    column.Format = DateTimePickerFormat.Short;
            //    GridViewDateTimeColumn column1 = (GridViewDateTimeColumn)this.rgvTaxValidity.Columns["endDate"];
            //    column1.Format = DateTimePickerFormat.Short;
            //}
        }

        private void setTranslation()
        {

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                lblIdTax.Text = resxSet.GetString("Id");
                lblCodeTax.Text = resxSet.GetString("Code BTW");
                lblDescTax.Text = resxSet.GetString("Description");
                rbIncl.Text = resxSet.GetString("Inclusive");
                rbExcl.Text = resxSet.GetString("Exclusive");
                btnSave.Text = resxSet.GetString("Save");
                btnNewTask.Text = resxSet.GetString("New");
                radRibbonTask.Text = resxSet.GetString("Tax rate");
                lblAccount.Text = resxSet.GetString("Account");
            }
        }

        private void rgvTaxValidity_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvTaxValidity.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvTaxValidity.Columns[i].HeaderText != null && resxSet.GetString(rgvTaxValidity.Columns[i].HeaderText) != null)
                        rgvTaxValidity.Columns[i].HeaderText = resxSet.GetString(rgvTaxValidity.Columns[i].HeaderText);
                }
            }


            rgvTaxValidity.Columns["idTaxValidity"].IsVisible = false;
            rgvTaxValidity.Columns["codeTax"].IsVisible = false;
            if (rgvTaxValidity.Columns != null && rgvTaxValidity.Columns.Count > 0)
                rgvTaxValidity.Columns["startDate"].FormatString = "{0: dd/MM/yyyy}";
            if (rgvTaxValidity.Columns != null && rgvTaxValidity.Columns.Count > 0)
                rgvTaxValidity.Columns["endDate"].FormatString = "{0: dd/MM/yyyy}";

            GridViewDateTimeColumn column = (GridViewDateTimeColumn)this.rgvTaxValidity.Columns["startDate"];
            column.Format = DateTimePickerFormat.Short;
            GridViewDateTimeColumn column1 = (GridViewDateTimeColumn)this.rgvTaxValidity.Columns["endDate"];
            column1.Format = DateTimePickerFormat.Short;
               
                    
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
             try
            {
                //if (iID == -1)
               
                tax = new AccTaxModel();
                tax.typeTax = 0;
                AccTaxValidityBUS peb = new AccTaxValidityBUS();
                Boolean isSuccessfully = false;

                tax.codeTax = txtCodeTax.Text;
                tax.descTax = txtDescTax.Text;
                if (rbIncl.CheckState == CheckState.Checked)
                    tax.typeTax = 1;
                else if (rbExcl.CheckState == CheckState.Checked)
                    tax.typeTax = 2;

                if (txtAccount.Text == "")
                {
                    RadMessageBox.Show("Can't save without account  !!!"); // tax.numberLedAccount = "";
                    return;
                }
                else
                {
                    LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();
                    lam = lab.GetAccount(txtAccount.Text,Login._bookyear);
                    if (lam != null)
                    {
                        if (lam.numberLedgerAccount != txtAccount.Text)
                        {
                            RadMessageBox.Show("Wrong account !!!");
                            return;
                        }
                        else
                        {
                            tax.numberLedAccount = txtAccount.Text;
                        }
                    }
                    else
                    {
                        RadMessageBox.Show("Can't save without account  !!!"); // tax.numberLedAccount = "";
                        return;
                    }
                    
                }

                AccTaxBUS bus = new AccTaxBUS();
                if (iID != -1)
                {
                    tax.idTax = iID;
                    isSuccessfully= bus.Update(tax, this.Name, Login._user.idUser);
                    if (isSuccessfully == false)
                    { 
                        RadMessageBox.Show("Error Update Tax !!!");
                        return;
                    }
                    else
                    {
                    modelChanged = true;
                    if (taxValidity.Count > 0 ) // != null)
                    {
                        saveTax();
                        for (int i = 0; i < taxValidity.Count; i++)
                        {
                            if (peb.Update(taxValidity[i], this.Name, Login._user.idUser) == true)
                            {
                                isSuccessfully = true;
                            }
                            else
                            {
                                RadMessageBox.Show("ERROR !!! UNsuccessfully try to save data for Tax Validity " + (i + 1).ToString());
                            }
                        }
                    }
                    }
                
                }
                else
                {
                    if (isFirst == false)
                    {
                        isSuccessfully = bus.Update(tax,this.Name,Login._user.idUser);

                    }
                    else
                    {
                        tax.typeTax = 0;
                        tax.codeTax = txtCodeTax.Text;
                        tax.descTax = txtDescTax.Text;
                        if (rbIncl.CheckState == CheckState.Checked)
                            tax.typeTax = 1;
                        else if (rbExcl.CheckState == CheckState.Checked)
                            tax.typeTax = 2;

                        if (txtAccount.Text == "")
                        {
                            RadMessageBox.Show("Can't save without account  !!!"); // tax.numberLedAccount = "";
                            return;
                        }
                        else
                        {
                            LedgerAccountBUS lab = new LedgerAccountBUS(Login._bookyear);
                            LedgerAccountModel lam = new LedgerAccountModel();
                            lam = lab.GetAccount(txtAccount.Text, Login._bookyear);
                            if (lam != null)
                            {
                                if (lam.numberLedgerAccount != txtAccount.Text)
                                {
                                    RadMessageBox.Show("Wrong account !!!");
                                    return;
                                }
                                else
                                {
                                    tax.numberLedAccount = txtAccount.Text;
                                }
                            }
                            else
                            {
                                RadMessageBox.Show("Can't save without account  !!!"); // tax.numberLedAccount = "";
                                return;
                            }

                        }
                        isSuccessfully = bus.Save(tax, this.Name, Login._user.idUser);
                        isFirst = false;
                    }
                        if (isSuccessfully == false)
                        {
                            RadMessageBox.Show("Error Saving Tax !!!");
                            return;
                        }
                        else
                        {
                            if (taxValidity.Count > 0)  // != null)
                            {
                                saveTax();
                                for (int i = 0; i < taxValidity.Count; i++)
                                {
                                    if (peb.Delete(taxValidity[i].idTaxValidity, this.Name, Login._user.idUser) == true)
                                    {
                                        isSuccessfully = true;
                                    }
                                    else
                                    {
                                        RadMessageBox.Show("ERROR !!! UNsuccessfully try to update data for Tax Validity " + (i + 1).ToString());
                                    }
                                }

                                for (int i = 0; i < taxValidity.Count; i++)
                                {
                                    if (peb.Save(taxValidity[i], this.Name, Login._user.idUser) == true)
                                    {
                                        isSuccessfully = true;
                                    }
                                    else
                                    {
                                        RadMessageBox.Show("ERROR !!! UNsuccessfully try to update data for Tax Validity " + (i + 1).ToString());
                                    }
                                }
                            }
                            modelChanged = true;
                        }
                    
                }
                modelChanged = true;
              //  this.Close();
                if (modelChanged == true)
                {
                    rgvTaxValidity.DataSource = null;
                    scodeTax = tax.codeTax;
                    AccTaxValidityBUS acb = new AccTaxValidityBUS();
                    taxValidity = acb.GetTaxValidity(scodeTax);
                    rgvTaxValidity.DataSource = taxValidity;
                }
                rgvTaxValidity.AllowAddNewRow = false;
            }
            catch (Exception ex)
            {
                RadMessageBox.Show("Error saving.\nMessage: " + ex.Message, "Save", MessageBoxButtons.OK, RadMessageIcon.Error);
            }


        }
        private void saveTax()
        {
            taxValidity = new List<AccTaxValidityModel>();
            for (int i = 0; i < rgvTaxValidity.Rows.Count; i++)
            {
                AccTaxValidityModel atm = new AccTaxValidityModel();
                if (rgvTaxValidity.Rows[i].Cells["idTaxValidity"].Value != null)
                    atm.idTaxValidity = Convert.ToInt32(rgvTaxValidity.Rows[i].Cells["idTaxValidity"].Value.ToString());
                if (rgvTaxValidity.Rows[i].Cells["startDate"].Value != null)
                {
                    atm.startDate = Convert.ToDateTime(rgvTaxValidity.Rows[i].Cells["startDate"].Value.ToString());
                }
              
                atm.codeTax = txtCodeTax.Text;  //tax.codeTax;
              
                if (rgvTaxValidity.Rows[i].Cells["percentTax"].Value != null)
                {
                    atm.percentTax = Convert.ToDecimal(rgvTaxValidity.Rows[i].Cells["percentTax"].Value.ToString());
                }
                if (rgvTaxValidity.Rows[i].Cells["endDate"].Value != null)
                {
                    atm.endDate = Convert.ToDateTime(rgvTaxValidity.Rows[i].Cells["endDate"].Value.ToString());
                }
                else
                {
                    atm.endDate = Convert.ToDateTime("1-1-1900");
                }
              
                taxValidity.Add(atm);

                isAddNewTaks = false;

            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            AccTaxValidityBUS acb = new AccTaxValidityBUS();
            AccTaxValidityModel mdlac = new AccTaxValidityModel();

           
            if (tax.codeTax != null)
            {
                mdlac = acb.GetTaxValidityNew(tax.codeTax);

                if (mdlac.idTaxValidity > 0)
                {
                    if (DialogResult.Yes == RadMessageBox.Show("You have a valid record ... Do yo wand to close that record?", "Question", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {
                        try
                        {
                            frmCLoseTax frm = new frmCLoseTax(tax.codeTax, mdlac.idTaxValidity);
                            frm.ShowDialog();
                            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
                            {
                                //System.Windows.Forms.DialogResult.OK.ToString()
                                rgvTaxValidity.DataSource = null;
                                taxValidity = acb.GetTaxValidity(scodeTax);
                                rgvTaxValidity.DataSource = taxValidity;

                                rgvTaxValidity.AllowAddNewRow = true;
                            }

                            //if (isChanged == true)
                            //{
                            //    rgvTaxValidity.DataSource = null;
                            //    taxValidity = acb.GetTaxValidity(scodeTax);
                            //    rgvTaxValidity.DataSource = taxValidity;

                            //    rgvTaxValidity.AllowAddNewRow = true;
                            //}

                        }
                        catch (Exception ex)
                        {
                            // RadMessageBox.Show("Error deleting document. \nMessage: " + ex.Message, "Error delete", MessageBoxButtons.OK, RadMessageIcon.Error);
                        }
                    }
                }
                else
                {
                    rgvTaxValidity.AllowAddNewRow = true; //RadMessageBox.Show("No records !");
                }
            }
            else 
            {
                RadMessageBox.Show("First you have to save btw!");
            }

            
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ccentar = new LedgerAccountBUS(Login._bookyear);
            List<IModel> gmX = new List<IModel>();

            gmX = ccentar.GetAllAccounts();
            var dlgSave = new GridLookupForm(gmX, "Account");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel genmX = new LedgerAccountModel();
                genmX = (LedgerAccountModel)dlgSave.selectedRow;
                //set textbox
                txtAccount.Text = genmX.numberLedgerAccount;
                labelAccount.Text = genmX.descLedgerAccount;
                tax.numberLedAccount = genmX.numberLedgerAccount;
                
            }
        }
        private void rgvTaxValidity_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
           
      
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Add)
            {
                isAddNewTaks = true;
            }
         
        }

        private void rgvTaxValidity_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (sender.GetType() == typeof(MasterGridViewTemplate))
                if (rgvTaxValidity.SelectedRows.Count > 0)
                {
                    MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
                    MasterGridViewTemplate mgvt = (MasterGridViewTemplate)rgvTaxValidity.MasterTemplate;
                  

                        if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Add)
                        {

                       
                        }
                     
                        else
                        {
                            e.Cancel = true;
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("You haven't successfully change answer");
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You have to feel type");
                    }
                }

        private void frmBTW_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isAddNewTaks == true)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                DialogResult dr = tr.translateAllMessageBoxDialog("There is add new taks. Do you wont close without save?", "");

                if (dr == System.Windows.Forms.DialogResult.Cancel || dr == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                }
            }

        }
                
        

    }
}
