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

namespace GUI
{
    public partial class frmLedgerSelection : Telerik.WinControls.UI.RadForm
    {
        DataTable model;
        private string from;
        private string to;
        private string name;
        private string nameUser;
        private bool isWithZero = false;


        public frmLedgerSelection()
        {
            InitializeComponent();
            this.Icon = Login.iconForm;
            string nameForm = this.Text.Substring(0);

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(nameForm) != null)
                    nameForm = resxSet.GetString(nameForm);
            }
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + nameForm;
        }

        private void frmLedgerSelection_Load(object sender, EventArgs e)
        {
            labelFromAccount.Text = "";
            labelToAccount.Text = "";

            Translation();
        }

        private void Translation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblFromAccount.Text) != null)
                    lblFromAccount.Text = resxSet.GetString(lblFromAccount.Text);

                if (resxSet.GetString(lblToAccount.Text) != null)
                    lblToAccount.Text = resxSet.GetString(lblToAccount.Text);

                if (resxSet.GetString(btnPrint.Text) != null)
                    btnPrint.Text = resxSet.GetString(btnPrint.Text);

                if (resxSet.GetString(btnCancel.Text) != null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);

                if (resxSet.GetString(chkZero.Text) != null)
                    chkZero.Text = resxSet.GetString(chkZero.Text);

             
            }
        }
        #region butt
        private void btnFromAccount_Click(object sender, EventArgs e)
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
             
                txtFromAccount.Text = genmX3.numberLedgerAccount;
                labelFromAccount.Text = genmX3.descLedgerAccount;
               
            }
        }

        private void btnToAccount_Click(object sender, EventArgs e)
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
               
                txtToAccount.Text = genmX3.numberLedgerAccount;
                labelToAccount.Text = genmX3.descLedgerAccount;
               
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            if (rbAmounts.IsChecked == true)
            {
                from = txtFromAccount.Text;
                to = txtToAccount.Text;
                if (to == "")
                    to = "999999";

                if (to == "999999")
                {
                    to = "Select all";
                }
                model = new DataTable();
                LedgerAccountDAO lad = new LedgerAccountDAO(Login._bookyear);
                if (chkZero.CheckState == CheckState.Checked)
                    isWithZero = true;
                else
                    isWithZero = false;
                model = lad.GetAllAccountsPrint(from, to, Login._user.nameUser, isWithZero);

                frmLedgerReport frm = new frmLedgerReport(model, nameUser, from, to);
                frm.Show();
            }
            
            else if (rbLedger.IsChecked == true)
            {
                from = txtFromAccount.Text;
                to = txtToAccount.Text;
                if (to == "")
                    to = "999999";

                if (to == "999999")
                {
                    to = "Select all";
                }

                LedgerAccountDAO lad = new LedgerAccountDAO(Login._bookyear);

                dt1 = lad.GetAllLedgerDataPrint(from, to, Login._user.nameUser);

                frmLedgerDataReport frm = new frmLedgerDataReport(dt1, nameUser, from, to);
                frm.Show();
            }
               
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion butt

        private void rbAmounts_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (rbAmounts.CheckState == CheckState.Checked)
                chkZero.Visible = true;
            else
                chkZero.Visible = false;
        }

    }
}
