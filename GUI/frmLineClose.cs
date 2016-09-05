using BIS.Business;
using BIS.DAO;
using BIS.Model;
using GUI.ReportsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;


namespace GUI
{
    public partial class frmLineClose : Telerik.WinControls.UI.RadForm
    {
        private string layoutJournal;
        private string xClient;
        private AccLineBUS aclBus;
        private List<AccLineModel> model;
        private string account = "";
        private string customer = "";
        private string daily = "";
        private string project = "";
        private string cost = "";
        private DateTime fromdate;
        private DateTime todate;
        private string fromperiod;
        private string toperiod;
        private string invoiceNr="";
       


        public frmLineClose()
        {
            InitializeComponent();
        }

        private void frmLineClose_Load(object sender, EventArgs e)
        {
         //   labelCustomer.Text = "";
         //   labelKonto.Text = "";
            labelDaily.Text = "";
         //   labelCost.Text = "";
         //   labelProject.Text = "";
            layoutJournal = MainForm.gridFiltersFolder + "\\layoutJournal.xml";

            this.Icon = Login.iconForm;
            string name = "Closing";

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(name) != null)
                    name = resxSet.GetString(name);
            }
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + name;



            Translation();
        }
        private void Translation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblDate.Text) != null)
                    lblDate.Text = resxSet.GetString(lblDate.Text);

                if (resxSet.GetString(lblDaily.Text) != null)
                    lblDaily.Text = resxSet.GetString(lblDaily.Text);

                if (resxSet.GetString(lblAccount.Text) != null)
                    lblAccount.Text = resxSet.GetString(lblAccount.Text);

                if (resxSet.GetString(lblCustomer.Text) != null)
                    lblCustomer.Text = resxSet.GetString(lblCustomer.Text);

                if (resxSet.GetString(btnPrint.Text) != null)
                    btnPrint.Text = resxSet.GetString(btnPrint.Text);

                if (resxSet.GetString(btnOK.Text) != null)
                    btnOK.Text = resxSet.GetString(btnOK.Text);

                if (resxSet.GetString(btnExit.Text) != null)
                    btnExit.Text = resxSet.GetString(btnExit.Text);

                if (resxSet.GetString(lblCost.Text) != null)
                    lblCost.Text = resxSet.GetString(lblCost.Text);

                if (resxSet.GetString(lblProject.Text) != null)
                    lblProject.Text = resxSet.GetString(lblProject.Text);

                if (resxSet.GetString(radbDate.Text) != null)
                    radbDate.Text = resxSet.GetString(radbDate.Text);

                if (resxSet.GetString(radbPeriodDate.Text) != null)
                    radbPeriodDate.Text = resxSet.GetString(radbPeriodDate.Text);

                if (resxSet.GetString(btnSaveLayout.Text) != null)
                    btnSaveLayout.Text = resxSet.GetString(btnSaveLayout.Text); 
            }
        }

        #region Buttons
        private void btnDaily_Click(object sender, EventArgs e)
        {
            AccDailyBUS ad = new AccDailyBUS(Login._bookyear);
            List<IModel> adm = new List<IModel>();

            adm = ad.GetAllDailys();
            var dlgSave3 = new GridLookupForm(adm, "Daily");

            if (dlgSave3.ShowDialog(this) == DialogResult.Yes)
            {
                AccDailyModel admod = new AccDailyModel();
                admod = (AccDailyModel)dlgSave3.selectedRow;

                txtDaily.Text = admod.codeDaily.ToString();
                labelDaily.Text = admod.descDailyType;

            }
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            LedgerAccountBUS ledger = new LedgerAccountBUS(Login._bookyear);
            List<IModel> ldg = new List<IModel>();

            ldg = ledger.GetAllAccounts();
            var dlgSave3 = new GridLookupForm(ldg, "Ledger");

            if (dlgSave3.ShowDialog(this) == DialogResult.Yes)
            {
                LedgerAccountModel ldm = new LedgerAccountModel();
                ldm = (LedgerAccountModel)dlgSave3.selectedRow;
               
                txtAccount.Text = ldm.numberLedgerAccount;
                labelKonto.Text = ldm.descLedgerAccount;
               
            }
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            if (radbCreditor.CheckState == CheckState.Checked)
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
                    txtCustomer.Text = pm1X.accNumber;
                    xClient = pm1X.accNumber;
                    labelCustomer.Text = pm1X.name;

                }
            }
            if (radbDebitor.CheckState == CheckState.Checked)
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
                    txtCustomer.Text = pm1X.accNumber;
                    xClient = pm1X.accNumber;
                    labelCustomer.Text = pm1X.name;

                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            getValues();

            if (radbDate.CheckState == CheckState.Checked)
            {
                if (fromdate == DateTime.MinValue)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Enter Date, please") != null)
                            RadMessageBox.Show(resxSet.GetString("Enter Date, please"));
                        else
                            RadMessageBox.Show("Enter Date, please");
                    }
                    return;
                }
            }
            else
            {
                if (radbPeriodDate.CheckState == CheckState.Checked)
                {
                    if (fromdate == DateTime.MinValue)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("Enter Date, please") != null)
                                RadMessageBox.Show(resxSet.GetString("Enter Date, please"));
                            else
                                RadMessageBox.Show("Enter Date, please");
                        }
                        return;
                    }
                    else
                    {
                        if (todate == DateTime.MinValue)
                        {
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {
                                if (resxSet.GetString("Enter end Date, please") != null)
                                    RadMessageBox.Show(resxSet.GetString("Enter end Date, please"));
                                else
                                    RadMessageBox.Show("Enter end Date, please");
                            }
                            return;
                        }
                    }
                    if (fromdate > todate)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("Wrong range") != null)
                                RadMessageBox.Show(resxSet.GetString("Wrong range"));
                            else
                                RadMessageBox.Show("Wrong range");
                        }
                        return;
                    }
                }
            }
            if (fromperiod != null && fromperiod != "")
            {
                if (toperiod != null && toperiod != "")
                {
                    if (Convert.ToInt32(fromperiod) > Convert.ToInt32(toperiod))
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("Wrong range") != null)
                                RadMessageBox.Show(resxSet.GetString("Wrong range"));
                            else
                                RadMessageBox.Show("Wrong range");
                        }
                        return;
                    }
                }
                else
                {
                    toperiod = fromperiod;
                }
            }


            aclBus = new AccLineBUS(Login._bookyear);
            model = new List<AccLineModel>();
            model = aclBus.GetLinesOPEN(daily,fromdate, todate,fromperiod,toperiod, invoiceNr);
            gridJurnal.DataSource = model;
            clearForm();
        }
        private void clearForm()
        {
          //  account = "";
          //  customer = "";
            daily = "";
          //  project = "";
          //  cost = "";
            fromperiod = "";
            toperiod = "";
            fromdate = DateTime.MinValue;
            dpFromDate.Text = "";
            dpFromDate.Value = DateTime.MinValue;
            todate = DateTime.MinValue;
            dpToDate.Text = "";
            dpToDate.Value = DateTime.MinValue;
            invoiceNr = "";

        }
        private void btnCost_Click(object sender, EventArgs e)
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
             }
        }

        private void btnProject_Click(object sender, EventArgs e)
        {
            ArrangementBUS ccentar = new ArrangementBUS();
            List<IModel> gmX = new List<IModel>();

            gmX = ccentar.GetAllArrangements();
            var dlgSave = new GridLookupForm(gmX, "Project");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                ArrangementModel genmX = new ArrangementModel();
                genmX = (ArrangementModel)dlgSave.selectedRow;
                //set textbox
                txtProject.Text = genmX.codeArrangement;
                labelProject.Text = genmX.nameArrangement;
            }


        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (model == null)
            {
                  using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("No lines for closing") != null)
                            RadMessageBox.Show(resxSet.GetString("No lines for closing"));
                        else
                            RadMessageBox.Show("No lines for closing");
                    }
                    return;
            }
            translateRadMessageBox tr = new translateRadMessageBox();
              if (tr.translateAllMessageBoxDialog("Close lines ?", " ") == System.Windows.Forms.DialogResult.Yes)
              {
                  decimal sumD = 0;
                  decimal sumC = 0;
                    for (int i=0; i<model.Count;i++)
                    {
                        if (model[i].debitLine != 0)
                            sumD = sumD + model[i].debitLine;
                        else
                            sumC = sumC + model[i].creditLine;
                    }
                  if (sumD-sumC != 0)
                  {
                      using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                      {
                          if (resxSet.GetString("Not in balans, can't close !!!") != null)
                              RadMessageBox.Show(resxSet.GetString("Not in balans, can't close !!!"));
                          else
                              RadMessageBox.Show("Not in balans, can't close !!!");
                      }
                      return;
                  }

                  AccLineBUS alb = new AccLineBUS(Login._bookyear);
                  bool lnYesNo = false;
                  foreach(AccLineModel aim in model)
                  {
                      lnYesNo = alb.UpdateStatus(aim, this.Name, Login._user.idUser);
                      if (lnYesNo == false)
                      {
                          using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                          {
                              if (resxSet.GetString("Error updating line!!!") != null)
                                  RadMessageBox.Show(resxSet.GetString("Error updating line!!!"));
                              else
                                  RadMessageBox.Show("Error updating line!!!");
                          }
                          return;
                      }
                  }

                  using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                  {
                      if (resxSet.GetString("Finished") != null)
                          RadMessageBox.Show(resxSet.GetString("Finished"));
                      else
                          RadMessageBox.Show("Finished");
                  }
                  clearForm();
                  model = new List<AccLineModel>();
                  gridJurnal.DataSource = model;

              }

              AccLineDAO acld = new AccLineDAO(Login._bookyear);
            DataTable print = new DataTable();
            print = acld.GetLinesOPEN(daily,fromdate, todate,fromperiod,toperiod, invoiceNr);
           
            clearForm();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDaily.Text = "";
       //     txtAccount.Text = "";
       //     txtCustomer.Text = "";
       //     txtCost.Text = "";
      //      txtProject.Text = "";
            radbDate.CheckState = CheckState.Unchecked;
            radbPeriodDate.CheckState = CheckState.Unchecked;
          
            dpFromDate.Value = Convert.ToDateTime("01-01-0001");
            dpFromDate.Text = "";
            dpToDate.Value = Convert.ToDateTime("01-01-0001");
            dpToDate.Text = "";
            dpFromDate.Visible = false;
            dpToDate.Visible = false;
            ddlFromPeriod.Text = "";
            ddlToPeriod.Text = "";
            txtInvoice.Text = "";
            invoiceNr = "";
       //     radbCreditor.CheckState = CheckState.Unchecked;
       //     radbDebitor.CheckState = CheckState.Unchecked;
           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion Buttons

        #region Grid
        private void gridJurnal_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
            if (gridJurnal != null)
            {
                if (gridJurnal.Columns.Count > 0)
                {
                    for (int i = 0; i < gridJurnal.Columns.Count; i++)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (gridJurnal.Columns[i].HeaderText != null && resxSet.GetString(gridJurnal.Columns[i].HeaderText) != null)
                                gridJurnal.Columns[i].HeaderText = resxSet.GetString(gridJurnal.Columns[i].HeaderText);
                        }
                        if (gridJurnal.Columns[i].GetType() == typeof(GridViewDateTimeColumn))
                        {
                            if (gridJurnal.Columns[i].Name.ToLower() != "dtModified".ToLower() && gridJurnal.Columns[i].Name.ToLower() != "dtCreated".ToLower())
                            {
                                gridJurnal.Columns[i].FormatString = "{0: dd-MM-yyyy}";
                            }
                        }
                    }

                    gridJurnal.Columns["idAccLine"].IsVisible = false;
                    gridJurnal.Columns["statusLine"].IsVisible = false;
                    gridJurnal.Columns["idPersonLine"].IsVisible = false;
                    gridJurnal.Columns["debitBTW"].IsVisible = false;
                    gridJurnal.Columns["creditBTW"].IsVisible = false;
                    gridJurnal.Columns["idCurrency"].IsVisible = false;
                    gridJurnal.Columns["debitCurr"].IsVisible = false;
                    gridJurnal.Columns["creditCurr"].IsVisible = false;
                    gridJurnal.Columns["currrate"].IsVisible = false;
                    gridJurnal.Columns["dtLine"].FormatString = "{0: d/M/yyyy}";
                    gridJurnal.Columns["dtBooking"].FormatString = "{0: d/M/yyyy}";
                }
            }

            if (File.Exists(layoutJournal))
            {
                gridJurnal.LoadLayout(layoutJournal);
            }
        }
        private void gridJurnal_ViewCellFormatting(object sender, CellFormattingEventArgs e)
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
        private void saveLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutJournal))
            {
                File.Delete(layoutJournal);
            }
            gridJurnal.SaveLayout(layoutJournal);
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("Layout saved") != null)
                    RadMessageBox.Show(resxSet.GetString("Layout saved"));
                else
                    RadMessageBox.Show("Layout saved");
            }
           
        }

        #endregion Grid

        #region radiob
        private void radbDate_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            Telerik.WinControls.UI.RadRadioButton rb = (Telerik.WinControls.UI.RadRadioButton)sender;
            if (rb.CheckState == CheckState.Checked)
            {
                dpToDate.Visible = false;
                dpFromDate.Visible = true;
                dpToDate.Value = DateTime.MinValue;
            }
            else
            {
                dpToDate.Visible = true;
            }
        }

        private void radbPeriodDate_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            Telerik.WinControls.UI.RadRadioButton rb = (Telerik.WinControls.UI.RadRadioButton)sender;
            if (rb.CheckState == CheckState.Checked)
            {
                dpToDate.Visible = true;
                dpFromDate.Visible = true;
            }
            else
            {
                dpToDate.Visible = false;
                dpToDate.Value = DateTime.MinValue;
            }
               

        }
        private void radbDebitor_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            //Telerik.WinControls.UI.RadRadioButton rb = (Telerik.WinControls.UI.RadRadioButton)sender;
            //if (rb.CheckState == CheckState.Checked)
            //    rbNew.Visible = false;
            //else
            //    rbNew.Visible = true;
        }
        #endregion radiob

        private void getValues()
        {
            fromdate = DateTime.MinValue;
            todate = DateTime.MinValue;
            if (txtDaily.Text != "")
                daily = txtDaily.Text;
            //if (txtAccount.Text != "")
            //    account = txtAccount.Text;
            //if (txtCustomer.Text != "")
            //    customer = txtCustomer.Text;
            if (dpFromDate.Text != "")
                fromdate = Convert.ToDateTime(dpFromDate.Text);
            if (dpToDate.Text != "")
                todate = Convert.ToDateTime(dpToDate.Text);
            if (ddlFromPeriod.Text != "")
                fromperiod = ddlFromPeriod.Text;
            if (ddlToPeriod.Text != "")
                toperiod = ddlToPeriod.Text;
            //if (txtCost.Text != "")
            //    cost = txtCost.Text;
            //if (txtProject.Text != "")
            //    project = txtProject.Text;
            if (txtInvoice.Text != "")
                invoiceNr = txtInvoice.Text;
            if (fromperiod == null)
                fromperiod = "";
            if (toperiod == null)
                toperiod = "";

        }

        private void ddlToPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = true;
            }
        }

        private void ddlFromPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = true;
            }
        }

      

      

        

   

    

       
    }
}
