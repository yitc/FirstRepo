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
using System.Linq;


namespace GUI
{
    public partial class frmDailyKasView : frmTempAccount

    {
        public string layoutDailyKasView;
        private AccDailyKasModel selectedDailyKas;
        private AccLineModel selectedAccLine;
        private AccDailyModel selectedDaily;
        private AccLineModel selectedLine;
        private AccLineModel selectedRowNo;
        private int xDaily;
        private decimal beginSld = 0;
        private decimal endSld = 0;
        private decimal totDeb = 0;
        private decimal totCre = 0;
        private decimal totdiff = 0;
        private string defDebitor = "";
        private string defCreditor = "";
        private string defSepa = "";
        
        //public frmDailyKasView()
        //{
        //    InitializeComponent();
        //}

        public frmDailyKasView(AccDailyModel selectedDaily1, AccDailyKasModel selectedDailyKas1)
        {
            selectedDailyKas = selectedDailyKas1;
            selectedDaily = selectedDaily1;

            xDaily = selectedDaily.idDaily ;
            //beginSld = Convert.ToDecimal(selectedDailyKas.begSaldo);
            //endSld = Convert.ToDecimal(selectedDailyKas.endSaldo);
            InitializeComponent();
           
            this.Icon = Login.iconForm;
        
        
          //  InitializeComponent();
        }


        private void frmDailyKasView_Load(object sender, EventArgs e)
        {
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

            Translation();

            labelKonto.Text = selectedDaily.numberLedgerAccount + "  " + selectedDaily.descLedgerAccount;

            if (selectedDailyKas != null)
            {
                txtStatement.Text = selectedDailyKas.refnoKas.ToString();
                txtBeginSaldo.Text = selectedDailyKas.begSaldo.ToString();
                txtEndSaldo.Text = selectedDailyKas.endSaldo.ToString();
                dpDateStatement.Text = selectedDailyKas.dtKas.ToString();

                //====
                AccDailyKasModel acbm = new AccDailyKasModel();
                //List<int> list =  new List<int>(); 
                List<AccDailyKasModel> acbmList = new List<AccDailyKasModel>();
                AccDailyKasBUS dbBus = new AccDailyKasBUS();
                acbmList = dbBus.GetAllByDaily(selectedDaily.codeDaily,Login._bookyear);

                if (acbmList != null)
                { 
                    acbm = acbmList.OrderByDescending(item => item.refnoKas).FirstOrDefault();
                }

                if (acbm != null)
                    if (acbm.refnoKas != selectedDailyKas.refnoKas)
                    {
                        btnNew.Enabled = false;
                        btnDelete.Enabled = false;
                    }
                //===

                AccLineBUS alb = new AccLineBUS(Login._bookyear);
                List<AccLineModel> alm = new List<AccLineModel>();

                alm = alb.GetAllLinesByIdCurrency(selectedDailyKas.idAccDailyKas, xDaily, 0);
                if (alm != null)
                {
                    if (alm.Count <= 0)
                    {
                        RadMessageBox.Show("Enter new");

                    }
                    else
                    {
                        gridBank.DataSource = null;
                        gridBank.DataSource = alm;
                        gridBank.Show();
                        txtStatement.Focus();
                    }
                }
            }

            layoutDailyKasView = MainForm.gridFiltersFolder + "\\layoutAccDailyBankView.xml";
            if (File.Exists(layoutDailyKasView))
            {
                gridBank.LoadLayout(layoutDailyKasView);
            }
          
        }

        private void gridBank_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
            if (gridBank != null)
                if (gridBank.Columns.Count > 0)
                {
                    for (int i = 0; i < gridBank.Columns.Count; i++)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (gridBank.Columns[i].HeaderText != null && resxSet.GetString(gridBank.Columns[i].HeaderText) != null)
                                gridBank.Columns[i].HeaderText = resxSet.GetString(gridBank.Columns[i].HeaderText);
                        }
                    }

                }

            if (File.Exists(layoutDailyKasView))
            {
                gridBank.LoadLayout(layoutDailyKasView);
            }



            if (gridBank != null)
                if (gridBank.RowCount > 0)
                    if (gridBank.SelectedRows != null)
                        if (gridBank.SelectedRows.Count > 0)
                        {
                            AccLineModel selectedRowNo = (AccLineModel)gridBank.SelectedRows[0].DataBoundItem;
                        }
        }

        private void gridBank_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row != null)
                if (e.RowIndex != -1)
                {
                    selectedLine = (AccLineModel)e.Row.DataBoundItem;

                    if (selectedDailyKas != null)
                    {
                        AccDailyBankModel adb = new AccDailyBankModel();
                        if (adb != null)
                        {
                            adb.idDailyBank = selectedDailyKas.idAccDailyKas;
                            adb.refNo = selectedDailyKas.refnoKas;
                            adb.begSaldo = selectedDailyKas.begSaldo;
                            adb.booked = selectedDailyKas.booked;
                            adb.bookingYear = selectedDailyKas.bookingYear;
                            adb.codeDaily = selectedDailyKas.codeDaily;
                            adb.difference = selectedDailyKas.difference;
                            adb.endSaldo = selectedDailyKas.endSaldo;
                        }

                        frmDailyBankNew frm = new frmDailyBankNew(selectedDaily, adb, selectedLine);
                     //  frmDailyKasView frm = new frmDailyKasView(selectedDaily, selectedDailyKas);
                        frm.ShowDialog();
                    }
                    AccLineBUS alb = new AccLineBUS(Login._bookyear);
                    List<AccLineModel> alm = new List<AccLineModel>();

                    alm = alb.GetAllLinesByIdCurrency(selectedDailyKas.idAccDailyKas, xDaily, 0);
                    //if (alm != null)
                    //{
                    gridBank.DataSource = null;
                    gridBank.DataSource = alm;
                    gridBank.Show();


                    // }
                }
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutDailyKasView))
            {
                File.Delete(layoutDailyKasView);
            }
            gridBank.SaveLayout(layoutDailyKasView);

            RadMessageBox.Show("Layout Saved");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int xDaily = selectedDaily.idDaily;
            int ixID = -1;
            string em = "new";
            //frmDailyKas frm = new frmDailyKas(ixID, xDaily, em, selectedDailyKas);

            AccDailyBankModel adb = new AccDailyBankModel();
            if (adb != null)
            {
                adb.idDailyBank = selectedDailyKas.idAccDailyKas;
                adb.refNo = selectedDailyKas.refnoKas;
                adb.begSaldo = selectedDailyKas.begSaldo;
                adb.booked = selectedDailyKas.booked;
                adb.bookingYear = selectedDailyKas.bookingYear;
                adb.codeDaily = selectedDailyKas.codeDaily;
                adb.difference = selectedDailyKas.difference;
                adb.endSaldo = selectedDailyKas.endSaldo;
            }

            frmDailyBankNew frm = new frmDailyBankNew(ixID, xDaily, em, adb);
         //  frmDailyKas frm = new frmDailyKas(ixID, xDaily, em, adb);
            frm.ShowDialog();
            AccLineBUS alb = new AccLineBUS(Login._bookyear);
            List<AccLineModel> alm = new List<AccLineModel>();

            alm = alb.GetAllLinesByIdCurrency(selectedDailyKas.idAccDailyKas, xDaily, 0);
            //if (alm != null)
            //{
                gridBank.DataSource = null;
                gridBank.DataSource = alm;
                gridBank.Show();


           // }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (txtDiff.Text == "0,00")
            {
                this.Close();
            }
            else
            {
                RadMessageBox.Show("Can't quit with difference !");
                return;
            }

        }

        private void gridBank_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            totDeb = 0;
            totCre = 0;
            totdiff = 0;
            for (int i = 0; i < gridBank.Rows.Count; i++)
            {
                if (gridBank.Rows[i].Cells["debitLine"].Value != null)
                    totDeb = totDeb + Convert.ToDecimal(gridBank.Rows[i].Cells["debitLine"].Value.ToString());
                if (gridBank.Rows[i].Cells["creditLine"].Value != null)
                    totCre = totCre + Convert.ToDecimal(gridBank.Rows[i].Cells["creditLine"].Value.ToString());

            }
            decimal ad = 0;
            ad = beginSld + totDeb - totCre;
            
            totdiff = endSld - ad;
                     
            txtDiff.Text = totdiff.ToString();
            
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

        private void gridLines_ViewCellFormatting(object sender, CellFormattingEventArgs e)
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
        private void Translation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                //  lblIdDaily.Text = resxSet.GetString("Id Daily");
                lblDateStatement.Text = resxSet.GetString("Date");
                lblStatement.Text = resxSet.GetString("Statement");
                lblBegin.Text = resxSet.GetString("Begin saldo");
                lblEnd.Text = resxSet.GetString("End Saldo");
                //  lblBank.Text = resxSet.GetString("Bank");
                lblUnlisted.Text = resxSet.GetString("Unlisted");
                //lblLock.Text = resxSet.GetString("Locked");
                //lblInkVer.Text = resxSet.GetString("Sales/Purchase");
                //radMenuItemSaveLayoutLines.Text = resxSet.GetString("Save Layout");

                btnNew.Text = resxSet.GetString("New");
                btnDelete.Text = resxSet.GetString("Delete");
            }


        }

        private void gridBank_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
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

            //===delete

            string saveLayout1 = "Delete Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem1 = new RadMenuItem();
            customMenuItem1.Text = saveLayout1;
            customMenuItem1.Click += SaveLayoutDailyKas;
            RadMenuSeparatorItem separator1 = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator1);
            e.ContextMenu.Items.Add(customMenuItem1);

        }
        private void SaveLayout(object sender, EventArgs e)
        {
            if (File.Exists(layoutDailyKasView))
            {
                File.Delete(layoutDailyKasView);
            }
            gridBank.SaveLayout(layoutDailyKasView);
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("You have successfully save layout!") != null)
                    RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
                else
                    RadMessageBox.Show("You have successfully save layout!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
               GridViewRowInfo info = this.gridBank.CurrentRow;
            if (info != null)
            {
                selectedRowNo = (AccLineModel)info.DataBoundItem;
                if (selectedRowNo != null)
                {
                    if (selectedRowNo.statusLine == false)
                    {
                        translateRadMessageBox dr = new translateRadMessageBox();
                        if (dr.translateAllMessageBoxDialogYesNo("Do you want to DELETE this Line ?", "Delete") == DialogResult.Yes)
                        {
                            deleteKasLine();
                        }
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Can't delete closed line!");
                        return;
                    }
                }

            }

        }
        private void deleteKasLine()
        {
            bool isSuccessfully = false;
            AccSettingsBUS asb = new AccSettingsBUS();
            AccSettingsModel asm = new AccSettingsModel();
            asm = asb.GetSettingsByID(Login._bookyear);
            if (asm != null)
            {
                defDebitor = asm.defDebitorAccount;
                defCreditor = asm.defCreditorAccount;
                defSepa = asm.defSepaAcc;
            }

            AccLineBUS alb = new AccLineBUS(Login._bookyear);
            List<AccLineModel> delmod = new List<AccLineModel>();
            delmod = alb.GetAllLinesByNumberALL(selectedRowNo.incopNr, 0);   // alb.GetAllLinesByNumber1610(selectedRowNo.incopNr, 0);

            AccAcountUpdate aaU = new AccAcountUpdate();
            isSuccessfully = aaU.SubstractAmount(selectedRowNo, this.Name, Login._user.idUser);  // oduzima osnovnu stavku
            if (isSuccessfully == false)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Error updating basic item!");
            }
            //===============  otvorena stavka ako postoji ============================
            AccOpenLinesBUS olbus = new AccOpenLinesBUS();
            AccOpenLinesModel olmod = new AccOpenLinesModel();

            //=========================================================================
            if (delmod != null)
            {
                for (int w = 1; w < delmod.Count; w++)  // oduzima ostale stavke
                {
                    isSuccessfully = aaU.SubstractAmount(delmod[w], this.Name, Login._user.idUser);
                    if (isSuccessfully == false)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Error updating items!");
                    }
                    // ==== normalan slucaj
                    if ((defDebitor != null && defDebitor != "") || (defCreditor != null && defCreditor != "")) // ovde treba udje ako je zatvorio vise racuna jednom
                    {                                                                                               // ulpatom
                        if (delmod[w].numberLedAccount.Trim() == defDebitor.Trim() || delmod[w].numberLedAccount.Trim() == defCreditor.Trim() || delmod[w].numberLedAccount.Trim() == defSepa.Trim())
                        {
                            olmod = olbus.GetAccOpenLinesByInvoice(delmod[w].invoiceNr, delmod[w].term);
                            if (olmod != null)
                            {
                                if (olmod.invoiceOpenLine != null)
                                {
                                    if (olmod.invoiceOpenLine == delmod[w].invoiceNr && olmod.term == delmod[w].term)
                                    {

                                        olmod.dtPayOpenLine = DateTime.Now;
                                        olmod.referencePay = selectedRowNo.incopNr; 
                                        if (delmod[w].debitLine != 0)
                                            olmod.debitOpenLine = olmod.debitOpenLine - delmod[w].debitLine;
                                        else
                                            if (delmod[w].creditLine != 0)
                                                olmod.creditOpenLine = olmod.creditOpenLine - delmod[w].creditLine;// itmol.creditLine;
                                        olbus.Update(olmod, this.Name, Login._user.idUser);  // upisujemo open line stavku
                                    }

                                }
                            }
                        }
                    }
                
                }
            }


            isSuccessfully = false;
            isSuccessfully = alb.Delete(selectedRowNo.idAccLine, this.Name, Login._user.idUser);    // brise osnovnu stavku
            if (isSuccessfully == false)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Error deleting basic item!");
            }

            if (delmod != null)
            {
                for (int w = 1; w < delmod.Count; w++)  // oduzima ostale stavke
                {
                    isSuccessfully = alb.Delete(delmod[w].idAccLine, this.Name, Login._user.idUser);
                    if (isSuccessfully == false)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Error deleting items!");
                    }
                }
            }

            translateRadMessageBox trr = new translateRadMessageBox();
            trr.translateAllMessageBox("Deleted");

            AccLineBUS alb1 = new AccLineBUS(Login._bookyear);
            List<AccLineModel> alm = new List<AccLineModel>();

            alm = alb1.GetAllLinesByIdCurrency(selectedDailyKas.idAccDailyKas, xDaily, 0);

            gridBank.DataSource = null;
            gridBank.DataSource = alm;


            // }
        
        }
        private void SaveLayoutDailyKas(object sender, EventArgs e)
        {
            if (File.Exists(layoutDailyKasView))
            {
                File.Delete(layoutDailyKasView);
            }

            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully delete layout!");


        }
    }
}
