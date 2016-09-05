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
    public partial class frmDailyBankView : frmTempAccount
    {
        public string layoutDailyBankView;
        private AccDailyBankModel selectedDailyBank;
        private AccLineModel selectedAccLine;
        private AccLineModel selectedRowNo;
        private AccDailyModel selectedDaily;
        private AccLineModel selectedLine;
        private List<AccLineModel> alm;
        private int xDaily;
        private decimal beginSld = 0;
        private decimal endSld = 0;
        private string defDebitor = "";
        private string defCreditor = "";
        private string defSepa = "";



        public frmDailyBankView(AccDailyModel selectedDaily1, AccDailyBankModel selectedDailyBank1)
        {
            selectedDailyBank = selectedDailyBank1;
            selectedDaily = selectedDaily1;

            xDaily = selectedDaily.idDaily;
            beginSld = Convert.ToDecimal(selectedDailyBank.begSaldo);
            endSld = Convert.ToDecimal(selectedDailyBank.endSaldo);
            InitializeComponent();

            this.Icon = Login.iconForm;


            //  InitializeComponent();
        }


        private void frmDailyBankView_Load(object sender, EventArgs e)
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
            btnExit.Visibility = ElementVisibility.Visible;

            if (selectedDaily.automaticBook == true)
            {
                btnDelete.Visibility = ElementVisibility.Collapsed;
                btnNew.Visibility = ElementVisibility.Collapsed;
            }

            Translation();


            if (selectedDailyBank != null)
            {
                AccDailyBankModel acbm = new AccDailyBankModel();
                //List<int> list =  new List<int>(); 
                List<AccDailyBankModel> acbmList = new List<AccDailyBankModel>();
                AccDailyBankBUS dbBus = new AccDailyBankBUS(Login._bookyear);
                acbmList = dbBus.GetAllByDaily(selectedDaily.codeDaily);

                if (acbmList != null)
                {
                    acbm = acbmList.OrderByDescending(item => item.refNo).FirstOrDefault();
                }

                if (acbm != null)
                    if (acbm.refNo != selectedDailyBank.refNo)
                    {
                        btnNew.Enabled = false;
                        btnDelete.Enabled = false;
                    }
                txtStatement.Text = selectedDailyBank.refNo.ToString();
                txtBeginSaldo.Text = selectedDailyBank.begSaldo.ToString();
                txtEndSaldo.Text = selectedDailyBank.endSaldo.ToString();
                labelKonto.Text = selectedDaily.numberLedgerAccount + " " + selectedDaily.descLedgerAccount;
                // dpDateStatement.Text = selectedDailyBank.dtStatement.ToString();
                txtDateStatement.Text = (selectedDailyBank.dtStatement).Value.ToShortDateString();

                AccLineBUS alb = new AccLineBUS(Login._bookyear);
                List<AccLineModel> alm = new List<AccLineModel>();

                alm = alb.GetAllLinesByIdCurrency(selectedDailyBank.idDailyBank, xDaily, 0);

                if (alm != null)
                {
                    if (alm.Count <= 0)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Enter new");

                    }
                    else
                    {
                        gridBank.DataSource = null;
                        gridBank.DataSource = alm;
                        gridBank.Show();
                        // txtStatement.Focus();
                    }
                }
            }

            layoutDailyBankView = MainForm.gridFiltersFolder + "\\layoutAccDailyBankView.xml";
            if (File.Exists(layoutDailyBankView))
            {
                gridBank.LoadLayout(layoutDailyBankView);

            }
           // btnNew.Focus();
        }

        private void gridBank_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < gridBank.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (gridBank.Columns[i].HeaderText != null && resxSet.GetString(gridBank.Columns[i].HeaderText) != null)
                        gridBank.Columns[i].HeaderText = resxSet.GetString(gridBank.Columns[i].HeaderText);

                }


            }
            if (gridBank.Columns.Count > 0)
            {
                //   gridBank.Columns["codeDaily"].IsVisible = false;
                gridBank.Columns["idAccLine"].IsVisible = false;
                gridBank.Columns["idAccDaily"].IsVisible = false;
                gridBank.Columns["statusLine"].IsVisible = false;
                gridBank.Columns["periodLine"].IsVisible = false;
                // gridBank.Columns["numberLedAccount"].IsVisible = false;
                //  gridBank.Columns["idClientLine"].IsVisible = false;
                gridBank.Columns["idPersonLine"].IsVisible = false;
                //  gridBank.Columns["idCostLine"].IsVisible = false;
                gridBank.Columns["numberLedAccount"].IsVisible = false;
                gridBank.Columns["idBTW"].IsVisible = false;
                gridBank.Columns["debitBTW"].IsVisible = false;
                gridBank.Columns["creditBTW"].IsVisible = false;
                gridBank.Columns["idCurrency"].IsVisible = false;
                gridBank.Columns["debitCurr"].IsVisible = false;
                gridBank.Columns["creditCurr"].IsVisible = false;
                gridBank.Columns["dtBooking"].IsVisible = false;
                gridBank.Columns["booksort"].IsVisible = false;
                gridBank.Columns["currrate"].IsVisible = false;
                gridBank.Columns["incopNr"].IsVisible = false;
                gridBank.Columns["iban"].IsVisible = false;
                gridBank.Columns["bookingYear"].IsVisible = false;
                gridBank.Columns["term"].IsVisible = false;
                gridBank.Columns["idSepa"].IsVisible = false;
                gridBank.Columns["descDaily"].IsVisible = false;
                gridBank.Columns["cond1"].IsVisible = false;
                gridBank.Columns["cond2"].IsVisible = false;
                gridBank.Columns["cond3"].IsVisible = false;
                gridBank.Columns["userN"].IsVisible = false;


            }
            if (gridBank.Columns != null && gridBank.Columns.Count > 0)
                gridBank.Columns["dtLine"].FormatString = "{0: dd/MM/yyyy}";
            if (gridBank.Columns != null && gridBank.Columns.Count > 0)
                gridBank.Columns["dtBooking"].FormatString = "{0: dd/MM/yyyy}";

            if (File.Exists(layoutDailyBankView))
            {
                gridBank.LoadLayout(layoutDailyBankView);
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
            selectedLine = (AccLineModel)e.Row.DataBoundItem;

            if (selectedLine != null)        //selectedDailyBank
            {
                frmDailyBankNew frm = new frmDailyBankNew(selectedDaily, selectedDailyBank, selectedLine);
                // frmDailyBankView frm = new frmDailyBankView(selectedDaily, selectedDailyBank);
                frm.ShowDialog();
            }
            AccLineBUS alb = new AccLineBUS(Login._bookyear);
            List<AccLineModel> alm = new List<AccLineModel>();

            alm = alb.GetAllLinesByIdCurrency(selectedDailyBank.idDailyBank, xDaily, 0);
            if (alm != null)
            {
                gridBank.DataSource = null;
                gridBank.DataSource = alm;
                gridBank.Show();


            }
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutDailyBankView))
            {
                File.Delete(layoutDailyBankView);
            }
            gridBank.SaveLayout(layoutDailyBankView);

            translateRadMessageBox tr = new translateRadMessageBox();
            tr.translateAllMessageBox("Layout Saved");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int xDaily = selectedDaily.idDaily;
            int ixID = -1;
            string em = "new";
            frmDailyBankNew frm = new frmDailyBankNew(ixID, xDaily, em, selectedDailyBank);
            frm.ShowDialog();
            AccLineBUS alb = new AccLineBUS(Login._bookyear);
            List<AccLineModel> alm = new List<AccLineModel>();

            alm = alb.GetAllLinesByIdCurrency(selectedDailyBank.idDailyBank, xDaily, 0);
            if (alm != null)
            {
                gridBank.DataSource = null;
                gridBank.DataSource = alm;
                gridBank.Show();


            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (txtDiff.Text == "0,00")
            {
                this.Close();
            }
            else
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Can't quit with difference !");
                return;
            }

        }

        private void gridBank_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            decimal totDeb = 0;
            decimal totCre = 0;
            decimal totdiff = 0;
            decimal totBooked = 0;
            for (int i = 0; i < gridBank.Rows.Count; i++)
            {
                if (gridBank.Rows[i].Cells["debitLine"].Value != null)
                    totDeb = totDeb + Convert.ToDecimal(gridBank.Rows[i].Cells["debitLine"].Value.ToString());
                if (gridBank.Rows[i].Cells["creditLine"].Value != null)
                    totCre = totCre + Convert.ToDecimal(gridBank.Rows[i].Cells["creditLine"].Value.ToString());
                if (gridBank.Rows[i].Cells["versil"].Value != null)
                    totBooked = totBooked + Convert.ToDecimal(gridBank.Rows[i].Cells["versil"].Value.ToString());
            }
            decimal ad = 0;
            ad = beginSld + totDeb - totCre;
            totdiff = endSld - ad;
            txtDiff.Text = totdiff.ToString();
            txtBook.Text = totBooked.ToString();

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
        private void Translation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                //  lblIdDaily.Text = resxSet.GetString("Id Daily");
                if (resxSet.GetString(lblDateStatement.Text) != null)
                    lblDateStatement.Text = resxSet.GetString(lblDateStatement.Text);
                if (resxSet.GetString(lblStatement.Text) != null)
                    lblStatement.Text = resxSet.GetString(lblStatement.Text);
                if (resxSet.GetString(lblBegin.Text) != null)
                    lblBegin.Text = resxSet.GetString(lblBegin.Text);
                if (resxSet.GetString(lblEnd.Text) != null)
                    lblEnd.Text = resxSet.GetString(lblEnd.Text);
                if (resxSet.GetString(lblBooked.Text)!=null)
                    lblBooked.Text = resxSet.GetString(lblBooked.Text);
                if (resxSet.GetString(lblUnlisted.Text) != null)
                    lblUnlisted.Text = resxSet.GetString(lblUnlisted.Text);
                if (resxSet.GetString(btnNew.Text) != null)
                    btnNew.Text = resxSet.GetString(btnNew.Text);
                if (resxSet.GetString(btnDelete.Text) != null)
                    btnDelete.Text = resxSet.GetString(btnDelete.Text);
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

            string saveLayout1 = "Delete Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem1 = new RadMenuItem();
            customMenuItem1.Text = saveLayout1;
            customMenuItem1.Click += SaveLayoutDailyBank;
            RadMenuSeparatorItem separator1 = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator1);
            e.ContextMenu.Items.Add(customMenuItem1);

        }
        private void SaveLayout(object sender, EventArgs e)
        {
            if (File.Exists(layoutDailyBankView))
            {
                File.Delete(layoutDailyBankView);
            }
            gridBank.SaveLayout(layoutDailyBankView);
            translateRadMessageBox tr = new translateRadMessageBox();
            tr.translateAllMessageBox("You have successfully save layout!");
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
                        if (dr.translateAllMessageBoxDialogYesNo("Do you want to DELETE this Statement ?", "Delete") == DialogResult.Yes)
                        {
                            deleteBankLine();
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
        private void deleteBankLine()
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
            delmod = alb.GetAllLinesByNumber1610(selectedRowNo.incopNr, 0);

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
                for (int w = 0; w < delmod.Count; w++)  // oduzima ostale stavke
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
                for (int w = 0; w < delmod.Count; w++)  // oduzima ostale stavke
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

            alm = alb1.GetAllLinesByIdCurrency(selectedDailyBank.idDailyBank, xDaily, 0);

            gridBank.DataSource = null;
            gridBank.DataSource = alm;


            // }
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            if (selectedDailyBank != null)
            {
                if (selectedDailyBank.pdfFile != null && selectedDailyBank.pdfFile != "") // ima dokument
                {

                    string sDest = System.Reflection.Assembly.GetEntryAssembly().Location;
                    sDest = sDest.Substring(0, sDest.LastIndexOf('\\')) + "\\Documents\\";
                    string fullname = selectedDailyBank.pdfFile;
                    if (System.IO.File.Exists(fullname))
                        OpenDocument(selectedDailyBank.pdfFile);  // sDest,
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Error opening document");
                    }
                }
                else
                {
                    translateRadMessageBox dr = new translateRadMessageBox();
                    if (dr.translateAllMessageBoxDialogYesNo("No pdf file ... Do you want to add ?", "Delete") == DialogResult.Yes)
                    {
                        OpenFileDialog dialog = new OpenFileDialog();

                        string ext = "pdf";//dtm.Find(item => item.typeDocument.TrimEnd() == txtext.Text.TrimEnd()).extendDocumentType;
                        dialog.Filter = "( *." + ext + ")|*." + ext + "|All Files (*.*)|*.*";
                        string sDest = System.Reflection.Assembly.GetEntryAssembly().Location;
                        sDest = sDest.Substring(0, sDest.LastIndexOf('\\')) + "\\Documents\\";
                        dialog.InitialDirectory = sDest;

                        string str = "Please select a file";
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString(str) != null)
                            {
                                str = resxSet.GetString(str);
                            }
                        }
                        dialog.Title = str;

                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            string sFile = dialog.FileName;
                            string sFileName = sFile.Split('\\')[sFile.Split('\\').Length - 1];

                            // txtdocName.Text = CreateDocName(idConstPers) + "." + ext;


                            string fullname = sFile;
                            if (selectedDailyBank != null)
                                selectedDailyBank.pdfFile = fullname;
                            bool bOpen = true;

                            if (bOpen)
                                OpenDocument(selectedDailyBank.pdfFile); //sDest, 
                            AccDailyBankBUS ab = new AccDailyBankBUS(Login._bookyear);
                            bool isOk = false;
                            isOk = ab.Update(selectedDailyBank, this.Name, Login._user.idUser);
                            if (isOk == false)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("Error updating Bank statement ...");
                            }
                        }

                    }
                }
                //else
                //{

            }
        }
        private void OpenDocument(string sFileName)  //string sDest,
        {
            string sExtention = sFileName.Split('.')[sFileName.Split('.').Length - 1];
            string sFullName = sFileName;  // sDest + 
            System.Diagnostics.Process.Start(sFullName);
        }

        private void btnErasePdf_Click(object sender, EventArgs e)
        {
            translateRadMessageBox dr = new translateRadMessageBox();
            if (dr.translateAllMessageBoxDialogYesNo("Do you want to delete Pdf ?", "Delete") == DialogResult.Yes)
            {
                selectedDailyBank.pdfFile = "";
                AccDailyBankBUS ab = new AccDailyBankBUS(Login._bookyear);
                bool isOk = false;
                isOk = ab.Update(selectedDailyBank, this.Name, Login._user.idUser);
                if (isOk == false)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Error updating Bank statement ...");
                }
            }
        }

        private void SaveLayoutDailyBank(object sender, EventArgs e)
        {
            if (File.Exists(layoutDailyBankView))
            {
                File.Delete(layoutDailyBankView);
            }

            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully delete layout!");


        }
    }
}
