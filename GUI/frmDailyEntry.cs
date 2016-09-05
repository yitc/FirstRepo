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
    public partial class frmDailyEntry : frmTempAccount
    {

        private int idDay = -1;
        public AccDailyModel model;
        public LedgerAccountModel modeLedger;
        public AccLedgerClassModel modelClass;
        public bool modelChanged = false;
        List<AccDailyTypeModel> dltype;
        List<AccDailyVerInModel> dclass;
        List<AccLineModel> linesmodel;
        private string Namef;
        public bool isInit = true;
        private string layoutLines;
        public List<AccLineModel> delmodel;
        private bool isSuccessfully= false;

        public frmDailyEntry()
        {
            modelChanged = false;
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Dbk.Daily");
            }
            this.Text = "";
            ribbonExampleMenu.Text = "";
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            InitializeComponent();
        }

        public frmDailyEntry(AccDailyModel dailymodel)
        {
            modelChanged = false;
            model = dailymodel;
            // heder forme
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Dbk.Daily");
            }
            ribbonExampleMenu.Text = "";
           // this.Text = Namef;   //"";
           
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            
            if (model.idDaily.ToString() != "")
                idDay = model.idDaily;
            InitializeComponent();
        }

        private void frmDaily_Load(object sender, EventArgs e)
        {
            this.rgvLines.EnableFiltering = true;
            this.rgvLines.MasterTemplate.ShowHeaderCellButtons = true;
            this.rgvLines.MasterTemplate.ShowFilteringRow = false;
          //  this.rgvLines.Columns["ContactName"].AllowFiltering = false;

           // this.rgvLines.MasterTemplate.ShowFilterCellOperatorText = false;

            radRibbonMemo.Visibility = ElementVisibility.Collapsed;
            btnEmail.Visibility = ElementVisibility.Collapsed;
            btnBooking.Visibility = ElementVisibility.Collapsed;
            btnNewMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteItem.Visibility = ElementVisibility.Collapsed;
            btnNewItem.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;
            btnDeleteItem.Visibility = ElementVisibility.Collapsed;
            radRibbonDocuments.Visibility = ElementVisibility.Collapsed;
            radRibbonExit.Visibility = ElementVisibility.Collapsed;
            btnBooking.Visibility = ElementVisibility.Collapsed;
            btnDelete.Visibility = ElementVisibility.Visible;

            setTranslation();

             layoutLines = MainForm.gridFiltersFolder + "\\layoutAccLines.xml";

            // Punjenje combo-a
            AccDailyTypeBUS tb1 = new AccDailyTypeBUS();
            List<AccDailyTypeModel> dltype = new List<AccDailyTypeModel>();
            dltype = tb1.GetAllTypes();

            ddlDailytype.DataSource = dltype;
            ddlDailytype.DisplayMember = "descDailyType";
            ddlDailytype.ValueMember = "idDailyType";


            //AccDailyVerInBUS tb2 = new AccDailyVerInBUS();
            //List<AccDailyVerInModel> dclass = new List<AccDailyVerInModel>();
            //dclass = tb2.GetAllClass();

            //ddlVerIn.DataSource = dclass;
            //ddlVerIn.DisplayMember = "nameDailyVerIn";
            //ddlVerIn.ValueMember = "idDailyVerIn";

            
            // Provera da li ima knjizenja -- ako ima zabraniti edit svih polja sem naziva
          
            if (idDay != -1)
            {
              //  txtIdDaily.Text = model.idDaily.ToString();
                if (model.codeDaily != null)
                   txtDaily.Text = model.codeDaily.ToString();
                if (model.descDaily != null)
                   txtDailyName.Text = model.descDaily.ToString();
                if (model.ibanBank != null)
                    txtIban.Text = model.ibanBank.ToString();
                //if (model.isLocked == true)
                //    chkLocked.Checked = true;
                if (model.idDailyType != 0 && model.idDailyType != null)
                    ddlDailytype.SelectedItem = ddlDailytype.Items[dltype.FindIndex(item => item.idDailyType == model.idDailyType)];

                if (model.idDailyType == 1)
                {
                    txtType.Text = "BANK";
                }
                else
                {
                    if (model.idDailyType == 2)
                    {
                        txtType.Text = "INKOOP";
                    }
                    else
                    {
                        if (model.idDailyType == 3)
                        {
                            txtType.Text = "VERKOOP";
                        }
                        else
                        {
                            if (model.idDailyType == 4)
                            {
                                txtType.Text = "MEMORIAAL";
                            }
                            else
                            {
                                if (model.idDailyType == 5)
                                {
                                    txtType.Text = "KAS";
                                }
                            }
                        }
                    }
                }
                if (model.automaticBook == true)
                {
                    btnNew.Visibility = ElementVisibility.Collapsed;
                    btnDelete.Visibility = ElementVisibility.Collapsed;
                }
                //if (model.idDailyType != null)   //model.idDailyType != 0 &&
                //    ddlDailytype.SelectedItem = ddlDailytype.Items[dltype.FindIndex(item => item.idDailyType == model.idDailyType)];
                //if ( model.idDailyVerIn != null)
                //    ddlVerIn.SelectedItem = ddlVerIn.Items[dclass.FindIndex(item => item.idDailyVerIn == model.idDailyVerIn)];
                ddlDailytype.Enabled = false;
                ddlVerIn.Enabled = false;
               // model.idDailyVerIn != 0 &&
                LedgerAccountBUS lb = new LedgerAccountBUS(Login._bookyear);
                LedgerAccountModel lm = new LedgerAccountModel();
                if (model.numberLedgerAccount != null && model.numberLedgerAccount != null)
                {
                    lm = lb.GetAccount(model.numberLedgerAccount, Login._bookyear);
                    if (lm != null)
                      txtAccount.Text = lm.numberLedgerAccount + " " + lm.descLedgerAccount;
                }
                //if (model.sideBooking != null)
                //    txtSide.Text = model.sideBooking;
                // ovde treba da dodje lookup za banku
                //LedgerAccountBUS lb = new LedgerAccountBUS();
                //LedgerAccountModel lm = new LedgerAccountModel();
                //if (model.numberLedgerAccount != null && model.numberLedgerAccount != null)
                //{
                //    lm = lb.GetAccount(model.numberLedgerAccount);
                //    txtAccount.Text = lm.numberLedgerAccount + " " + lm.descLedgerAccount;
                //}
                AccLineBUS lnbus = new AccLineBUS(Login._bookyear);
                linesmodel = lnbus.GetAllLinesByDaily(model.idDaily, 0);
                rgvLines.DataSource = null;
                rgvLines.DataSource = linesmodel;
                rgvLines.Show();
             
            }
            else
            {
                model = new AccDailyModel();
            }
        }

        private void setTranslation()
        {

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
              //  lblIdDaily.Text = resxSet.GetString("Id Daily");
                if (resxSet.GetString(lblDaily.Text) != null)
                    lblDaily.Text = resxSet.GetString(lblDaily.Text);
               // lblDaily.Text = resxSet.GetString("Daily entry");
                lblDescription.Text = resxSet.GetString("Description");
                lblDailyType.Text = resxSet.GetString("Daily type");
                lblAccount.Text = resxSet.GetString("Dbk.Account");
                lblBank.Text = resxSet.GetString("Bank");
                lblIban.Text = resxSet.GetString("IBAN");
             //   lblLock.Text = resxSet.GetString("Locked");
                lblInkVer.Text = resxSet.GetString("Sales/Purchase");
                radMenuItemSaveLayoutLines.Text = resxSet.GetString("Save Layout");

                btnNew.Text = resxSet.GetString("New");
                btnDelete.Text = resxSet.GetString("Delete");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtDaily.Text == "")
                {
                    RadMessageBox.Show("Enter a Daily code, please!");
                    return;
                }
              


                //if (chkLocked.Checked == true)
                //{
                //    model.isLocked = true;
                //}
                //else
                //{
                //    model.isLocked = false;
                //}
                model.codeDaily = txtDaily.Text;
               
                if ((txtDailyName.Text).ToString() != "")
                    model.descDaily = txtDailyName.Text;
                if ((txtBank.Text).ToString() != "")
                    model.idBank = Convert.ToInt32(txtBank.Text);
               // if ((txtIban.Text).ToString() != "")
                    model.ibanBank = txtIban.Text;
                if (txtAccount.Text == "")
                    model.numberLedgerAccount = txtAccount.Text;
                model.idDailyType = Convert.ToInt32(ddlDailytype.SelectedValue);
                //model.sideBooking = txtSide.Text.ToUpper();
                int a = Convert.ToInt32(ddlVerIn.SelectedValue);
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
                   
                    model.idDaily = idDay;
                    bus.Update(model, this.Name, Login._user.idUser);
                }
                else
                {
                    bus.Save(model, this.Name, Login._user.idUser);

                }
                modelChanged = false;
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

        private void rgvPages_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (modelChanged == true)
            {
                if (DialogResult.Yes == RadMessageBox.Show("Do you want to Exit without Saving?", "Exit", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {
                    this.Close();
                }
                else
                {
                    btnSave_Click(sender, e);  //Save all work
                }
            }
            else
            {
                this.Close();
            }
        }

        private void txtIban_TextChanged(object sender, EventArgs e)
        {
            if (isInit == true)
            {
                isInit = false;  //okida event prilikom otvaranja forme
            }
            else
            {
            modelChanged = true;
            }
        }

        private void rgvLines_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvLines.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvLines.Columns[i].HeaderText != null && resxSet.GetString(rgvLines.Columns[i].HeaderText) != null)
                        rgvLines.Columns[i].HeaderText = resxSet.GetString(rgvLines.Columns[i].HeaderText);
                }
            }
               if (File.Exists(layoutLines))
            {
                rgvLines.LoadLayout(layoutLines);
            }

               if (rgvLines.Columns != null && rgvLines.Columns.Count > 0)
                   rgvLines.Columns["dtLine"].FormatString = "{0: dd/MM/yyyy}";
               if (rgvLines.Columns != null && rgvLines.Columns.Count > 0)
                   rgvLines.Columns["dtBooking"].FormatString = "{0: dd/MM/yyyy}";

               if (rgvLines.ColumnCount > 0)
               {
                   rgvLines.Columns["numberLedAccount"].IsVisible = false;
                   rgvLines.Columns["idAccLine"].IsVisible = false;
                   rgvLines.Columns["idPersonLine"].IsVisible = false;
                   rgvLines.Columns["debitBTW"].IsVisible = false;
                   rgvLines.Columns["creditBTW"].IsVisible = false;
                   rgvLines.Columns["idCurrency"].IsVisible = false;
                   rgvLines.Columns["creditCurr"].IsVisible = false;
                   rgvLines.Columns["debitCurr"].IsVisible = false;
                   rgvLines.Columns["booksort"].IsVisible = false;
                   rgvLines.Columns["currrate"].IsVisible = false;
                   rgvLines.Columns["iban"].IsVisible = false;
                   rgvLines.Columns["term"].IsVisible = false;
                   rgvLines.Columns["idSepa"].IsVisible = false;
                   rgvLines.Columns["descDaily"].IsVisible = false;
                   rgvLines.Columns["cond1"].IsVisible = false;
                   rgvLines.Columns["cond2"].IsVisible = false;
                   rgvLines.Columns["cond3"].IsVisible = false;
                   rgvLines.Columns["userN"].IsVisible = false;
                   rgvLines.Columns["versil"].IsVisible = false;
                   rgvLines.Columns["idAccDaily"].IsVisible = false;


       

               }

        }

        private void rgvLines_DoubleClick(object sender, EventArgs e)
        {
            //int iID = Int32.Parse(rgvLines.SelectedRows[0].Cells["idAccLine"].Value.ToString());
            //int idDaily = Int32.Parse(rgvLines.SelectedRows[0].Cells["idAccDaily"].Value.ToString());
            //int idType = Convert.ToInt32(model.idDailyType);


            //if (idType == 2)
            //{
            //    string tilt = "1";
            //    frmLineEntry frm = new frmLineEntry(iID, idDaily, tilt);
            //    frm.ShowDialog();
            //    AccLineBUS lnbus = new AccLineBUS();
            //    linesmodel = lnbus.GetAllLinesByDaily(model.idDaily, 0);
            //    rgvLines.DataSource = null;
            //    rgvLines.DataSource = linesmodel;
            //}
            //else
            //{
            //    if (idType == 3)
            //    {
            //        string tilt = "1";
            //        frmLineVerkop frm = new frmLineVerkop(iID, idDaily, tilt);
            //        frm.ShowDialog();
            //        AccLineBUS lnbus = new AccLineBUS();
            //        linesmodel = lnbus.GetAllLinesByDaily(model.idDaily, 0);
            //        rgvLines.DataSource = null;
            //        rgvLines.DataSource = linesmodel;
            //    }
            //    else
            //    {
            //        if (model.idDailyType == 1)
            //        {
            //            int iID5 = -1;
            //            frmDailyBankOLD frm = new frmDailyBankOLD(model, null);
            //            frm.ShowDialog();
            //            AccLineBUS lnbus = new AccLineBUS();
            //            linesmodel = lnbus.GetAllLinesByDaily(model.idDaily, 0);
            //            rgvLines.DataSource = null;
            //            rgvLines.DataSource = linesmodel;
            //        }
            //        else
            //        {
            //            if (model.idDailyType == 1)
            //            {
            //                int iID6 = -1;
            //                frmDailyMemorial frm = new frmDailyMemorial(model,null);
            //                frm.ShowDialog();
            //                AccLineBUS lnbus = new AccLineBUS();
            //                linesmodel = lnbus.GetAllLinesByDaily(model.idDaily, 0);
            //                rgvLines.DataSource = null;
            //                rgvLines.DataSource = linesmodel;
            //            }
            //        }
            //    }
            //}
        }

        private void btnNewItem_Click(object sender, EventArgs e)
        {
            if (model.idDailyType == 2)
            {
                int iID2 = -1;
              //  frmLineEntry frm = new frmLineEntry(iID2, model.idDaily);
                frmLineEntryNew frm = new frmLineEntryNew(iID2, model.idDaily);
                frm.ShowDialog();
                AccLineBUS lnbus2 = new AccLineBUS(Login._bookyear);
                linesmodel = lnbus2.GetAllLinesByDaily(model.idDaily, 0);
                rgvLines.DataSource = null;
                rgvLines.DataSource = linesmodel;
            }
            else
            {
                if (model.idDailyType == 1)
                {
                    int iID = -1;
                    frmDailyBankEntry frm = new frmDailyBankEntry(model);
                    frm.ShowDialog();
                    AccLineBUS lnbus = new AccLineBUS(Login._bookyear);
                    linesmodel = lnbus.GetAllLinesByDaily(model.idDaily, 0);
                    rgvLines.DataSource = null;
                    rgvLines.DataSource = linesmodel;
                }
                else
                {
                    if (model.idDailyType == 3)
                    {
                        int iID3 = -1;
                        AccDailyModel model1 = new AccDailyModel();
                    //    frmLineVerkop frm = new frmLineVerkop(iID3, model.idDaily);
                     //   frmLineVerkopNew frm = new frmLineVerkopNew(iID3, model.idDaily);
                        frmLineVerkopNew2 frm = new frmLineVerkopNew2(iID3, model.idDaily);
                        frm.ShowDialog();
                        AccLineBUS lnbus = new AccLineBUS(Login._bookyear);
                        linesmodel = lnbus.GetAllLinesByDaily(model.idDaily, 0);
                        rgvLines.DataSource = null;
                        rgvLines.DataSource = linesmodel;
                    }
                    else
                    {
                        if (model.idDailyType == 4)
                        {
                            int iID4 = -1;
                            frmDailyMemorial frm = new frmDailyMemorial(model, null);
                            frm.ShowDialog();
                            AccLineBUS lnbus = new AccLineBUS(Login._bookyear);
                            linesmodel = lnbus.GetAllLinesByDaily(model.idDaily, 0);
                            rgvLines.DataSource = null;
                            rgvLines.DataSource = linesmodel;
                        }
                        else
                        {
                            if (model.idDailyType == 5)
                            {
                                int iID = -1;
                                frmDailyBankEntry frm = new frmDailyBankEntry(model);
                                frm.ShowDialog();
                                AccLineBUS lnbus = new AccLineBUS(Login._bookyear);
                                linesmodel = lnbus.GetAllLinesByDaily(model.idDaily, 0);
                                rgvLines.DataSource = null;
                                rgvLines.DataSource = linesmodel;
                            }
                        }
                    }
                }

            
            }
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == RadMessageBox.Show("Are you sure to delete this Line?", "Delete", MessageBoxButtons.YesNo, RadMessageIcon.Question))
            {
                try
                {


                    AccLineBUS ldbus = new AccLineBUS(Login._bookyear);
                    AccAcountUpdate AcU = new AccAcountUpdate();
                    AccOpenLinesBUS Olb = new AccOpenLinesBUS();
                    int iIDel = Int32.Parse(rgvLines.SelectedRows[0].Cells["idAccLine"].Value.ToString());
                    string iInkop = rgvLines.SelectedRows[0].Cells["incopNr"].Value.ToString();
                    string iinvoice = rgvLines.SelectedRows[0].Cells["invoiceNr"].Value.ToString();
                  
                    isSuccessfully = AcU.CheckOpenLines(iinvoice);
                    if (isSuccessfully == true)
                    {
                       

                        List<AccLineModel> delmodel = new List<AccLineModel>();
                        delmodel = ldbus.GetAllLinesByNumberALL(iInkop, 0);
                        if (delmodel != null)
                        {
                            for (int js = 0; js < delmodel.Count; js++)
                            {
                                isSuccessfully = AcU.SubstractAmount(delmodel[js], this.Name, Login._user.idUser);
                                if (isSuccessfully == false)
                                {
                                    RadMessageBox.Show("Error Unbooking document !! ", "Error delete", MessageBoxButtons.OK, RadMessageIcon.Error);
                                    return;
                                }
                            }


                            ldbus.DeleteByReference(iInkop, this.Name, Login._user.idUser);
                        }
                        isSuccessfully = Olb.DeleteByInvoice(iinvoice, this.Name, Login._user.idUser);
                        if (isSuccessfully==false)
                        {
                            RadMessageBox.Show("Error deleting open lines");
                        }
                        //  ldbus.DeleteByReference(iInkop);
                        ldbus.Delete(iIDel, this.Name, Login._user.idUser);
                        AccLineBUS ldbus1 = new AccLineBUS(Login._bookyear);
                        linesmodel = ldbus1.GetAllLinesByDaily(model.idDaily, 0);
                        rgvLines.DataSource = null;
                        rgvLines.DataSource = linesmodel;
                    }
                    else
                    {
                        RadMessageBox.Show("Delete not allowed !!! There is open lines");
                        return;
                    }
                 
                }

                catch (Exception ex)
                {
                    RadMessageBox.Show("Error deleting document. \nMessage: " + ex.Message, "Error delete", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
            }

        }

        private void radMenuItemSaveLayoutLines_Click(object sender, EventArgs e)
       
        {
            if (File.Exists(layoutLines))
            {
                File.Delete(layoutLines);
            }
            rgvLines.SaveLayout(layoutLines);

            RadMessageBox.Show("Layout Saved");
        }
     private void Translation()
        {


        }

     private void rgvLines_CellDoubleClick(object sender, GridViewCellEventArgs e)
     {
         if (e.Row.DataBoundItem != null)
         {
             AccLineModel kliknutirow = (AccLineModel)e.Row.DataBoundItem;
             int iID = kliknutirow.idAccLine;
             int idDaily = kliknutirow.idAccDaily;
             int idType = Convert.ToInt32(model.idDailyType);
             //...



             //int iID = Int32.Parse(rgvLines.SelectedRows[0].Cells["idAccLine"].Value.ToString());
             //int idDaily = Int32.Parse(rgvLines.SelectedRows[0].Cells["idAccDaily"].Value.ToString());
             //int idType = Convert.ToInt32(model.idDailyType);


             if (idType == 2)
             {
                 string tilt = "1";
                 if (model.automaticBook == true)
                 {
                     frmLineEntry frm = new frmLineEntry(iID, idDaily, tilt);
                     frm.ShowDialog();
                 }
                 else
                 {
                     frmLineEntryNew frm = new frmLineEntryNew(iID, idDaily, tilt);
                     //frmLineVerkopNew frm = new frmLineVerkopNew(iID, idDaily, tilt);
                     //    frmLineVerkop frm = new frmLineVerkop(iID, idDaily, tilt);

                     frm.ShowDialog();

                     AccLineBUS lnbus = new AccLineBUS(Login._bookyear);
                     linesmodel = lnbus.GetAllLinesByDaily(model.idDaily, 0);
                     rgvLines.DataSource = null;
                     rgvLines.DataSource = linesmodel;
                 }
              //   frmLineEntry frm = new frmLineEntry(iID, idDaily, tilt);
                 //frmLineEntryNew frm = new frmLineEntryNew(iID, idDaily, tilt);
                 //frm.ShowDialog();
               
             }
             else
             {
                 if (idType == 3)
                 {
                     string tilt = "1";
                     if (model.automaticBook == true)
                     {
                         frmLineVerkop frm = new frmLineVerkop(iID, idDaily, tilt);
                         frm.ShowDialog();
                     }
                     else
                     {
                         frmLineVerkopNew2 frm = new frmLineVerkopNew2(iID, idDaily, tilt);
                       //  frmLineVerkopNew frm = new frmLineVerkopNew(iID, idDaily, tilt);
                         //    frmLineVerkop frm = new frmLineVerkop(iID, idDaily, tilt);

                         frm.ShowDialog();
                         AccLineBUS lnbus = new AccLineBUS(Login._bookyear);
                         linesmodel = lnbus.GetAllLinesByDaily(model.idDaily, 0);
                         rgvLines.DataSource = null;
                         rgvLines.DataSource = linesmodel;
                     }
                    
                 }
                 else
                 {
                     if (model.idDailyType == 1)
                     {
                         int iID5 = -1;
                         frmDailyBankNew frm = new frmDailyBankNew(model, null, null);
                         frm.ShowDialog();
                         AccLineBUS lnbus = new AccLineBUS(Login._bookyear);
                         linesmodel = lnbus.GetAllLinesByDaily(model.idDaily, 0);
                         rgvLines.DataSource = null;
                         rgvLines.DataSource = linesmodel;
                     }
                     else
                     {
                         if (model.idDailyType == 1)
                         {
                             int iID6 = -1;
                             frmDailyMemorial frm = new frmDailyMemorial(model, null);
                             frm.ShowDialog();
                             AccLineBUS lnbus = new AccLineBUS(Login._bookyear);
                             linesmodel = lnbus.GetAllLinesByDaily(model.idDaily, 0);
                             rgvLines.DataSource = null;
                             rgvLines.DataSource = linesmodel;
                         }
                     }
                 }
             }
         }
     }

     private void rgvLines_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
     {

         string saveLayout = "Save Layout";
         using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
         {

             if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                 saveLayout = resxSet.GetString(saveLayout);
         }
         RadMenuItem customMenuItem = new RadMenuItem();
         customMenuItem.Text = saveLayout;
         customMenuItem.Click += SaveLayout;
         RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
         e.ContextMenu.Items.Add(separator);
         e.ContextMenu.Items.Add(customMenuItem);


         //==delete

         string saveLayout1 = "Delete Layout";
         using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
         {

             if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                 saveLayout = resxSet.GetString(saveLayout);
         }
         RadMenuItem customMenuItem1 = new RadMenuItem();
         customMenuItem1.Text = saveLayout1;
         customMenuItem1.Click += SaveLayoutDailyEntry;
         RadMenuSeparatorItem separator1 = new RadMenuSeparatorItem();
         e.ContextMenu.Items.Add(separator1);
         e.ContextMenu.Items.Add(customMenuItem1);

     }
     private void SaveLayout(object sender, EventArgs e)
     {
         if (File.Exists(layoutLines))
         {
             File.Delete(layoutLines);
         }
         rgvLines.SaveLayout(layoutLines);
         using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
         {
             if (resxSet.GetString("You have successfully save layout!") != null)
                 RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
             else
                 RadMessageBox.Show("You have successfully save layout!");
         }
     }

     private void rgvLines_RowFormatting(object sender, RowFormattingEventArgs e)
     {
         //{
         //    if ((bool)e.RowElement.RowInfo.Cells["statusLine"].Value == true)
         //    {
         //        //e.RowElement.DrawFill = true;
         //        //e.RowElement.GradientStyle = GradientStyles.Solid;
         //        e.RowElement.BackColor = Color.LightGray;
         //    }
         //}
     }

     private void rgvLines_ViewCellFormatting(object sender, CellFormattingEventArgs e)
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

     private void SaveLayoutDailyEntry(object sender, EventArgs e)
     {
         if (File.Exists(layoutLines))
         {
             File.Delete(layoutLines);
         }

         translateRadMessageBox msg = new translateRadMessageBox();
         msg.translateAllMessageBox("You have successfully delete layout!");

     }


    }
}
