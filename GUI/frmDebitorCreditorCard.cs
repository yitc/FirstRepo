using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Business;
using BIS.DAO;
using BIS.Model;
using GUI.ReportsForms;
using Telerik.WinControls.UI;

namespace GUI
{
    public partial class frmDebitorCreditorCard : Telerik.WinControls.UI.RadForm
    {
        private string from;
            private string to;
        private string nameUser;
        BindingList<DebCreLookupModel> debCreList;
        AccDebCreDAO dao;
        
        public frmDebitorCreditorCard()
        {  
            InitializeComponent();
            dao = new AccDebCreDAO();
            this.Icon = Login.iconForm;
            string nameForm = this.Text.Substring(0);

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(nameForm) != null)
                    nameForm = resxSet.GetString(nameForm);
            }
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + nameForm;
        }

        private void frmDebitorCreditorCard_Load(object sender, EventArgs e)
        {
            pickerFromDate.Value = DateTime.Now;
            pickerToDate.Value = DateTime.Now;
           
            Translation();
            this.radGridView1.MasterTemplate.ShowFilterCellOperatorText = false;
        }

        private void Translation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblFromDate.Text) != null)
                    lblFromDate.Text = resxSet.GetString(lblFromDate.Text);

                if (resxSet.GetString(lblToDate.Text) != null)
                    lblToDate.Text = resxSet.GetString(lblToDate.Text);

                if (resxSet.GetString(btnPrint.Text) != null)
                    btnPrint.Text = resxSet.GetString(btnPrint.Text);

                if (resxSet.GetString(btnCancel.Text) != null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);

                if (resxSet.GetString(rbDebitor.Text) != null)
                    rbDebitor.Text = resxSet.GetString(rbDebitor.Text);

                if (resxSet.GetString(rbCreditor.Text) != null)
                    rbCreditor.Text = resxSet.GetString(rbCreditor.Text);

                if (resxSet.GetString(rlCustomerFrom.Text) != null)
                    rlCustomerFrom.Text = resxSet.GetString(rlCustomerFrom.Text);

                if (resxSet.GetString(rlCustomerTo.Text) != null)
                    rlCustomerTo.Text = resxSet.GetString(rlCustomerTo.Text);

                if (resxSet.GetString(lblNameCustomerFrom.Text) != null)
                    lblNameCustomerFrom.Text = resxSet.GetString(lblNameCustomerFrom.Text);

                if (resxSet.GetString(lblNameCustomerTo.Text) != null)
                    lblNameCustomerTo.Text = resxSet.GetString(lblNameCustomerTo.Text);

                if (resxSet.GetString(chkWithBeginBalans.Text) != null)
                    chkWithBeginBalans.Text = resxSet.GetString(chkWithBeginBalans.Text);

                if (resxSet.GetString(rbnDetail.Text) != null)
                    rbnDetail.Text = resxSet.GetString(rbnDetail.Text);

                if (resxSet.GetString(rbnSum.Text) != null)
                    rbnSum.Text = resxSet.GetString(rbnSum.Text);

                if (resxSet.GetString(lblFromDate.Text) != null)
                    lblFromDate.Text = resxSet.GetString(lblFromDate.Text);

                if (resxSet.GetString(lblToDate.Text) != null)
                    lblToDate.Text = resxSet.GetString(lblToDate.Text);

                if (resxSet.GetString(btnDo.Text) != null)
                    btnDo.Text = resxSet.GetString(btnDo.Text);

                if (resxSet.GetString(btnPrint.Text) != null)
                    btnPrint.Text = resxSet.GetString(btnPrint.Text);

                if (resxSet.GetString(btnCancel.Text) != null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region butt
        private void btnCustomerFrom_Click(object sender, EventArgs e)
        {
            if (rbDebitor.IsChecked == true)
            {
                DebCreLookupBUS debcre = new DebCreLookupBUS();
                List<IModel> pm1 = new List<IModel>();

                pm1 = debcre.GetDebitors();
                var dlgSave = new GridLookupForm(pm1, "Debitor");

                if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                {
                    DebCreLookupModel pm1X = new DebCreLookupModel();
                    pm1X = (DebCreLookupModel)dlgSave.selectedRow;
                    //set textbox
                    txtCustomerFrom.Text = pm1X.accNumber;
                    lblNameCustomerFrom.Text = pm1X.name;                 
                    
                    txtCustomerTo.Focus();
                }              
               
            }
            else
            {

                DebCreLookupBUS debcre1 = new DebCreLookupBUS();
                List<IModel> pm11 = new List<IModel>();

                pm11 = debcre1.GetCreditors();
                var dlgSave1 = new GridLookupForm(pm11, "Creditor");

                if (dlgSave1.ShowDialog(this) == DialogResult.Yes)
                {
                    DebCreLookupModel pm1X1 = new DebCreLookupModel();
                    pm1X1 = (DebCreLookupModel)dlgSave1.selectedRow;
                    //set textbox
                    txtCustomerFrom.Text = pm1X1.accNumber;
                    lblNameCustomerFrom.Text = pm1X1.name;

                }
            }
        }


        private void btnCustomerTo_Click(object sender, EventArgs e)
        {
            if (rbDebitor.IsChecked == true)
            {
                DebCreLookupBUS debcre = new DebCreLookupBUS();
                List<IModel> pm1 = new List<IModel>();

                pm1 = debcre.GetDebitors();
                var dlgSave = new GridLookupForm(pm1, "Debitor");

                if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                {
                    DebCreLookupModel pm1X = new DebCreLookupModel();
                    pm1X = (DebCreLookupModel)dlgSave.selectedRow;
                    //set textbox
                    txtCustomerTo.Text = pm1X.accNumber;
                    lblNameCustomerTo.Text = pm1X.name;
                   
                }
                
            }
            else
            {

                DebCreLookupBUS debcre1 = new DebCreLookupBUS();
                List<IModel> pm11 = new List<IModel>();

                pm11 = debcre1.GetCreditors();
                var dlgSave1 = new GridLookupForm(pm11, "Creditor");

                if (dlgSave1.ShowDialog(this) == DialogResult.Yes)
                {
                    DebCreLookupModel pm1X1 = new DebCreLookupModel();
                    pm1X1 = (DebCreLookupModel)dlgSave1.selectedRow;
                    //set textbox
                    txtCustomerTo.Text = pm1X1.accNumber;
                    lblNameCustomerTo.Text = pm1X1.name;

                }
            }
        }
        #endregion

        private void rbDebitor_CheckStateChanged(object sender, EventArgs e)
        {
            // ako decekira brise se sadrzaj lookapa 
            if (rbDebitor.IsChecked == false)
            {
                txtCustomerFrom.Text = "";
                lblNameCustomerFrom.Text = "";
                txtCustomerTo.Text = "";
                lblNameCustomerTo.Text = "";
               
            }
            if(rbCreditor.IsChecked==false)
            {
                txtCustomerFrom.Text = "";
                lblNameCustomerFrom.Text = "";
                txtCustomerTo.Text = "";
                lblNameCustomerTo.Text = "";
            }
        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            bool debr = false;
            if (rbCreditor.CheckState == CheckState.Checked)
            {
                debr = false;
                
            }
            else if (rbDebitor.CheckState == CheckState.Checked)
                debr = true;

            //Za with balanse
            bool isBalans = true;
            if (chkWithBeginBalans.CheckState == CheckState.Checked)
                isBalans = true;
            else if (chkWithBeginBalans.CheckState == CheckState.Unchecked)
                isBalans = false;

            bool isSum = true;
            if (rbnSum.CheckState == CheckState.Checked)
                isSum = true;
            else if (rbnSum.CheckState == CheckState.Unchecked)
                isSum = false;


            if (isSum == true)
            {

                DebCreLookupBUS debcreBUS = new DebCreLookupBUS();
                List<IModel> debcreModel = new List<IModel>();
                debcreModel = debcreBUS.GetAllRange(txtCustomerFrom.Text, txtCustomerTo.Text, debr, isBalans, isSum, pickerFromDate.Value, pickerToDate.Value, nameUser);
                radGridView1.DataSource = null;
                radGridView1.DataSource = debcreModel;

            }
            else
            {
                DebCreLookupBUS debcreBUS = new DebCreLookupBUS();
                List<IModel> debcreModel = new List<IModel>();


                debcreModel = debcreBUS.GetDetailRange(txtCustomerFrom.Text, txtCustomerTo.Text, debr, isBalans, isSum, pickerFromDate.Value, pickerToDate.Value, nameUser);
                radGridView1.DataSource = null;

                radGridView1.DataSource = debcreModel;
            }
        }
            

        private void txtCustomerFrom_Leave(object sender, EventArgs e)
        {
            if(txtCustomerFrom.Text =="")
            {
                lblNameCustomerFrom.Text = "";
            }
        }

        private void txtCustomerTo_Leave(object sender, EventArgs e)
        {
            if(txtCustomerTo.Text=="")
            {
                lblNameCustomerTo.Text = "";
            }
        }



        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();

            if (rbDebitor.IsChecked == true)
            {
                DateTime dtFrom = pickerFromDate.Value;
                DateTime dtTo = pickerToDate.Value;

                bool debr = true;
                if (rbCreditor.CheckState == CheckState.Checked)
                    debr = false;
                else if (rbDebitor.CheckState == CheckState.Checked)
                    debr = true;


                bool isBalans = true;
                if (chkWithBeginBalans.CheckState == CheckState.Checked)
                    isBalans = true;
                else if (chkWithBeginBalans.CheckState == CheckState.Unchecked)
                    isBalans = false;

                bool isSum = true;
                if (rbnSum.CheckState == CheckState.Checked)
                    isSum = true;
                else if (rbnSum.CheckState == CheckState.Unchecked)
                    isSum = false;

                dao = new AccDebCreDAO();


                if (rbnDetail.CheckState == CheckState.Checked)
                {
                    if (chkWithBeginBalans.CheckState == CheckState.Checked)
                    {
                        dt1 = dao.GetDetailRange(txtCustomerFrom.Text, txtCustomerTo.Text, debr, isBalans, false, dtFrom, dtTo, Login._user.nameUser);
                        frmDebitorCreditorCardForDetailsReport frmDetails1 = new frmDebitorCreditorCardForDetailsReport(txtCustomerFrom.Text, txtCustomerTo.Text, debr, isBalans, false, dtFrom, dtTo, dt1);
                        frmDetails1.Show();
                    }
                    else
                    {
                        dt1 = dao.GetDetailRange(txtCustomerFrom.Text, txtCustomerTo.Text, debr, isBalans, false, dtFrom, dtTo, Login._user.nameUser);
                        frmDebitorCreditorCardForDetailsReport frmDetails = new frmDebitorCreditorCardForDetailsReport(txtCustomerFrom.Text, txtCustomerTo.Text, debr, isBalans, false, dtFrom, dtTo, dt1);
                        frmDetails.Show();
                    }
                }
                else if (rbnSum.CheckState == CheckState.Checked)
                {
                    dt1 = dao.GetAllRange(txtCustomerFrom.Text, txtCustomerTo.Text, debr, isBalans, true, dtFrom, dtTo, Login._user.nameUser);
                    frmDebitorCreditorCardForDetailsReport frmDetailsSum = new frmDebitorCreditorCardForDetailsReport(txtCustomerFrom.Text, txtCustomerTo.Text, debr, isBalans, true, dtFrom, dtTo, dt1);
                    frmDetailsSum.Show();
                }

                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You have to choose sum or detail report.");
                }
            }

            if (rbCreditor.IsChecked == true)
            {
                DateTime dtFrom = pickerFromDate.Value;
                DateTime dtTo = pickerToDate.Value;


                bool debr = true;
                if (rbCreditor.CheckState == CheckState.Checked)
                    debr = false;
                else if (rbDebitor.CheckState == CheckState.Checked)
                    debr = true;


                bool isBalans = true;
                if (chkWithBeginBalans.CheckState == CheckState.Checked)
                    isBalans = true;
                else if (chkWithBeginBalans.CheckState == CheckState.Unchecked)
                    isBalans = false;

                bool isSum = true;
                if (rbnSum.CheckState == CheckState.Checked)
                    isSum = true;
                else if (rbnSum.CheckState == CheckState.Unchecked)
                    isSum = false;

                dao = new AccDebCreDAO();

                if (rbnDetail.IsChecked == true)
                {
                    if (chkWithBeginBalans.CheckState == CheckState.Checked)
                    {
                        dt1 = dao.GetDetailRange(txtCustomerFrom.Text, txtCustomerTo.Text, debr, isBalans, false, dtFrom, dtTo, Login._user.nameUser);
                        frmDebitorCreditorCardForDetailsReport frmDetails1 = new frmDebitorCreditorCardForDetailsReport(txtCustomerFrom.Text, txtCustomerTo.Text, debr, isBalans, false, dtFrom, dtTo, dt1);
                        frmDetails1.Show();
                    }
                    else
                    {
                        dt1 = dao.GetDetailRange(txtCustomerFrom.Text, txtCustomerTo.Text, debr, isBalans, false, dtFrom, dtTo, Login._user.nameUser);
                        frmDebitorCreditorCardForDetailsReport frmDetails = new frmDebitorCreditorCardForDetailsReport(txtCustomerFrom.Text, txtCustomerTo.Text, debr, isBalans, false, dtFrom, dtTo, dt1);
                        frmDetails.Show();
                    }
                }
                else if (rbnSum.CheckState == CheckState.Checked)
                {
                    dt1 = dao.GetAllRange(txtCustomerFrom.Text, txtCustomerTo.Text, debr, isBalans, true, dtFrom, dtTo, Login._user.nameUser);
                    frmDebitorCreditorCardForDetailsReport frmDetailsSum = new frmDebitorCreditorCardForDetailsReport(txtCustomerFrom.Text, txtCustomerTo.Text, debr, isBalans, true, dtFrom, dtTo, dt1);
                    frmDetailsSum.Show();
                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You have to choose sum or detail report.");
                }

            }

        }

        private void radGridView1_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < radGridView1.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (radGridView1.Columns[i].HeaderText != null && resxSet.GetString(radGridView1.Columns[i].HeaderText) != null)
                        radGridView1.Columns[i].HeaderText = resxSet.GetString(radGridView1.Columns[i].HeaderText);
                }
            }

            if (this.radGridView1.Columns != null)
            {
                if (this.radGridView1.RowCount > 0)
                {
                    if (rbnSum.CheckState != CheckState.Checked)
                    {
                        this.radGridView1.Columns["dtLine"].FormatString = "{0: dd-MM-yyyy}";

                      
                    }
                    if(rbnSum.CheckState==CheckState.Checked)
                    {
                        if (this.radGridView1.Columns["numberLedAccount"] != null)
                            this.radGridView1.Columns["numberLedAccount"].IsVisible = false;
                    }
                    //if(chkWithBeginBalans.CheckState==CheckState.Checked)
                    //{
                    //    if (this.radGridView1.Columns["numberLedAccount"] != null)
                    //        this.radGridView1.Columns["numberLedAccount"].IsVisible = false;
                    //}
                    if (chkWithBeginBalans.CheckState != CheckState.Checked)
                    {
                        if (this.radGridView1.Columns["nameUser"] != null)
                            this.radGridView1.Columns["nameUser"].IsVisible = false;

                        if (this.radGridView1.Columns["from"] != null)
                            this.radGridView1.Columns["from"].IsVisible = false;

                        if (this.radGridView1.Columns["to"] != null)
                            this.radGridView1.Columns["to"].IsVisible = false;

                        if (this.radGridView1.Columns["deb"] != null)
                            this.radGridView1.Columns["deb"].IsVisible = false;

                        if (this.radGridView1.Columns["DebitBalans"] != null)
                            this.radGridView1.Columns["DebitBalans"].IsVisible = false;

                        if (this.radGridView1.Columns["CreditBalans"] != null)
                            this.radGridView1.Columns["CreditBalans"].IsVisible = false;

                      

                        if (rbnSum.CheckState == CheckState.Unchecked)
                        {
                            if (this.radGridView1.Columns["zip"] != null)
                                this.radGridView1.Columns["zip"].IsVisible = false;

                            if (this.radGridView1.Columns["address"] != null)
                                this.radGridView1.Columns["address"].IsVisible = false;

                            if (this.radGridView1.Columns["city"] != null)
                                this.radGridView1.Columns["city"].IsVisible = false;

                         

                        }
                    }
                    else if (rbnSum.CheckState == CheckState.Checked)
                    {
                        if (this.radGridView1.Columns["numberLedAccount"] != null)
                            this.radGridView1.Columns["numberLedAccount"].IsVisible = false;
                    }

                }

            }
        }

        private void radGridView1_ViewCellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
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

        
    }
}
