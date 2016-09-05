using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Outlook = Microsoft.Office.Interop.Outlook;
using BIS.Business;
using BIS.Model;
using Telerik.WinControls.UI;
using System.IO;
using System.Diagnostics;


namespace GUI
{
    public partial class frmInvoiceSelection : Telerik.WinControls.UI.RadForm
    {

        private int idArr = 0;
        private string codeD;
        AccDailyModel genmX;
        private BindingList<InvoiceModel> limBind;

        readonly List<string> _tempFolders = new List<string>();
        string tempFolder;

        private string layoutInvoiceSelectionForAccounting = MainForm.gridFiltersFolder + "\\layoutInvoiceSelectionForAccounting.xml";

        public frmInvoiceSelection()
        {
            InitializeComponent();
            genmX = new AccDailyModel();
        }

        private void frmInvoiceSelection_Load(object sender, EventArgs e)
        {
            this.Icon = Login.iconForm;

            tempFolder = MainForm.GetTemporaryFolder();
            _tempFolders.Add(tempFolder);

            ddlStatus.DataSource = new InvoiceStatusBUS().GeInvoiceStatus(2);
            ddlStatus.DisplayMember = "descInvoiceStatus";
            ddlStatus.ValueMember = "idInvoiceStatus";

            ddlLabel.DataSource = new LabelsBUS().GetDistinctLabels();
            ddlLabel.DisplayMember = "nameLabel";
            ddlLabel.ValueMember = "idLabel";

            limBind = new BindingList<InvoiceModel>();

            rgvInvoice.DataSource = limBind;

            setTranslation();
        }

        private void setTranslation()
        {

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(this.Text) != null)
                    this.Text = resxSet.GetString(this.Text);

                if (resxSet.GetString(radioSearchByArr.Text) != null)
                    radioSearchByArr.Text = resxSet.GetString(radioSearchByArr.Text);
                if (resxSet.GetString(radioSeatchByLabel.Text) != null)
                    radioSeatchByLabel.Text = resxSet.GetString(radioSeatchByLabel.Text);
                if (resxSet.GetString(lblStatus.Text) != null)
                    lblStatus.Text = resxSet.GetString(lblStatus.Text);
                if (resxSet.GetString(btnExit.Text) != null)
                    btnExit.Text = resxSet.GetString(btnExit.Text);
                if (resxSet.GetString(radMenuButtonSaveLayout.Text) != null)
                    radMenuButtonSaveLayout.Text = resxSet.GetString(radMenuButtonSaveLayout.Text);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {      
                if(radioSearchNone.IsChecked == true)
                {
                    InvoiceBUS ib = new InvoiceBUS();
                    List<InvoiceModel> lim = new List<InvoiceModel>();
                    lim = ib.GetAllInvoicesForAccounting((int)ddlStatus.SelectedValue);
                    if (lim != null)
                    {
                        limBind.Clear();
                        foreach (InvoiceModel m in lim)
                        {
                            limBind.Add(m);
                        }
                    }
                    else
                        limBind.Clear();
                }
                else if (radioSearchByArr.IsChecked == true)
                {
                    if (txtArrangement.Text.Trim() == "")
                    {
                        translateRadMessageBox msgbox = new translateRadMessageBox();
                        msgbox.translateAllMessageBox("Select arrangement first");
                        return;
                    }
                    InvoiceBUS ib = new InvoiceBUS();
                    List<InvoiceModel> lim = new List<InvoiceModel>();
                    lim = ib.GetInvoicesForAccounting(idArr, (int)ddlStatus.SelectedValue);
                    if (lim != null)
                    {
                        limBind.Clear();

                        foreach (InvoiceModel m in lim)
                        {
                            limBind.Add(m);
                        }

                    }
                    else
                        limBind.Clear();
                }
                else if (radioSeatchByLabel.IsChecked == true)
                {
                    InvoiceBUS ib = new InvoiceBUS();
                    List<InvoiceModel> lim = new List<InvoiceModel>();
                    lim = ib.GetInvoicesForAccountingByLabel((int)ddlLabel.SelectedValue, (int)ddlStatus.SelectedValue);
                    if (lim != null)
                    {
                        limBind.Clear();
                        foreach (InvoiceModel m in lim)
                        {
                            limBind.Add(m);                            
                        }
                    }
                    else
                        limBind.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                SelectAllDeselectAll(btnSelectAll.Checked);
            }

            Cursor.Current = Cursors.Default;
        }

        private void btnArrangement_Click(object sender, EventArgs e)
        {
            ArrangementBUS ccentar1 = new ArrangementBUS();
            List<IModel> gmX1 = new List<IModel>();

            gmX1 = ccentar1.GetAllArrangements();
            var dlgSave1 = new GridLookupForm(gmX1, "Arrangement");

            if (dlgSave1.ShowDialog(this) == DialogResult.Yes)
            {
                ArrangementModel genmX1 = new ArrangementModel();
                genmX1 = (ArrangementModel)dlgSave1.selectedRow;
                //set textbox
                txtArrangement.Text = genmX1.codeArrangement + " - " + genmX1.nameArrangement;
                idArr = genmX1.idArrangement;

                //invStatus = ddlStatus.SelectedIndex + 1;
                //getdata();
            }




        }

        private void rgvInvoice_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if (File.Exists(layoutInvoiceSelectionForAccounting))
            {
                rgvInvoice.LoadLayout(layoutInvoiceSelectionForAccounting);
            }

            if (this.rgvInvoice.Columns != null)
            {
                //this.gridOpenLines.Columns["chk"].IsVisible = false;

                foreach (GridViewColumn col in rgvInvoice.Columns)
                {
                    if (col.Name != "select")
                    {
                        col.ReadOnly = true;
                        //col.Width = 100;
                    }
                }

                if (rgvInvoice.Columns["select"] != null)
                    rgvInvoice.Columns["select"].IsVisible = true;

                //SortDescriptor descriptor = new SortDescriptor();
                //descriptor.PropertyName = "dtOpenLine";
                //descriptor.Direction = ListSortDirection.Descending;
                //this.gridOpenLinesLetters.MasterTemplate.SortDescriptors.Add(descriptor);

                for (int i = 0; i < rgvInvoice.Columns.Count; i++)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (rgvInvoice.Columns[i].HeaderText != null && resxSet.GetString(rgvInvoice.Columns[i].HeaderText) != null)
                            rgvInvoice.Columns[i].HeaderText = resxSet.GetString(rgvInvoice.Columns[i].HeaderText);
                    }
                    if (rgvInvoice.Columns[i].GetType() == typeof(GridViewDateTimeColumn))
                    {
                        if (rgvInvoice.Columns[i].Name.ToLower() != "dtModified".ToLower() && rgvInvoice.Columns[i].Name.ToLower() != "dtCreated".ToLower())
                        {
                            rgvInvoice.Columns[i].FormatString = "{0: dd-MM-yyyy}";
                        }
                    }
                }                
            }
        }

        private void frmInvoiceSelection_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var tempFolder in _tempFolders)
            {
                if (Directory.Exists(tempFolder))
                    Directory.Delete(tempFolder, true);
            }
        }

       

        private void btnBooking_Click(object sender, EventArgs e)
        {
            try
            {
                if (codeD != null && codeD != "")
                {
                    rgvInvoice.EndEdit();
                    List<InvoiceModel> selected = new List<InvoiceModel>();
                    List<InvoiceModel> booked = new List<InvoiceModel>();
                    List<InvoiceModel> notbooked = new List<InvoiceModel>();
                    List<InvoiceItemsModel> stavke = new List<InvoiceItemsModel>();
                    InvoiceBUS ib = new InvoiceBUS();
                    InvoiceItemsBUS iib = new InvoiceItemsBUS();
                    List<InvoiceItemsModel> iim1 = new List<InvoiceItemsModel>();
                    AccAcountUpdate book = new AccAcountUpdate();
                    ArrangementModel am = new ArrangementModel();

                    //get selected invoices
                    foreach (var m in limBind)
                    {
                        if (m.select == true)
                        {
                            selected.Add(m);
                        }
                    }

                    if (selected.Count <= 0)
                    {
                        translateRadMessageBox msgbox = new translateRadMessageBox();
                        msgbox.translateAllMessageBox("Nothing selected.");
                        return;
                    }

                    bool llbookOk = false;
                    int idLabel = 0;

                    //go trough selected nad process them
                    foreach (var m in selected)
                    {
                        iim1 = new List<InvoiceItemsModel>();
                        iim1 = iib.GetInvoiceItemsByInvoice(m.idInvoice.ToString(), Login._user.lngUser);

                        if (iim1 != null)
                        {
                            am = new ArrangementBUS().GetArrangementByArrangementBook(Convert.ToInt32(m.idVoucher));
                            List<LabelForArrangement> alm = new List<LabelForArrangement>();

                            if (am != null)
                            {
                                

                                alm = new ArrangementBUS().GetLabelsArrangement(am.idArrangement);
                                if (alm != null && alm.Count > 0)
                                    idLabel = alm[0].idLabel;

                                //book cuurent invoice
                                llbookOk = book.InvoiceBookingWithoutMessageBoxes(m, iim1, codeD, idLabel, am.codeProject, this.Name, Login._user.idUser);

                                if (llbookOk == false)
                                    notbooked.Add(m);
                                else
                                    booked.Add(m);
                            }
                            else
                                notbooked.Add(m);
                        }
                        else
                            notbooked.Add(m);
                    }

                    if (booked.Count > 0)
                    {
                        foreach (var m in booked)
                        {
                            bool res = ib.UpdateStatus(4, m.idInvoice, this.Name, Login._user.idUser);
                            if(res = true)
                            {
                                UpdateDebitorCheckbox((int)m.idContPerson, (int)m.idClient);
                            }
                            
                        }
                    }

                    if (notbooked.Count > 0)
                    {
                        string strNotBooked = "Next invoices are not booked: \n";

                        foreach (var m in notbooked)
                        {
                            strNotBooked += m.invoiceNr + " - " + m.invoiceRbr + " \n";
                        }

                        translateRadMessageBox msgbox = new translateRadMessageBox();
                        msgbox.translateAllMessageBox(strNotBooked);
                    }

                    translateRadMessageBox msgbox1 = new translateRadMessageBox();
                    msgbox1.translateAllMessageBox("Booking completed!!!");

                    txtDaily.Text = "";
                    btnBooking.Enabled = false;
                    btnDo.PerformClick();
                }
                else
                {
                    translateRadMessageBox msgbox = new translateRadMessageBox();
                    msgbox.translateAllMessageBox("Can't booking without Verkoop book !!!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateDebitorCheckbox(int idPerson, int idClient)
        {
            //updejtuje is debitor = 1 
            //ako ne postoji person ili client u tabeli AccDebCre dodaje novi row 

            if (idClient > 0)
            {
                AccDebCreBUS accbus = new AccDebCreBUS();
                AccDebCreModel cmodel = accbus.GetClientDebCre(idClient);
                
                if (cmodel != null)
                {
                    if (cmodel.isDebitor == false)
                    {
                        cmodel.isDebitor = true;
                        accbus.UpdateDebitorToTrue(cmodel.idAccDebCre, this.Name, Login._user.idUser);
                    }
                }
                else
                {
                    ClientBUS cbus = new ClientBUS();
                    AccIbanBUS accibanbus = new AccIbanBUS();
                    List<AccIbanModel> ibanlist = accibanbus.GetIBANForClient(idClient);
                    AccDebCreModel newmodel = new AccDebCreModel();
                    ClientModel tmpmod = cbus.GetClient(idClient);

                    if (tmpmod != null)
                    {
                        //AccSettingsBUS accsetBUS = new AccSettingsBUS();
                        //AccSettingsModel accsetmodel = accsetBUS.

                        newmodel.idClient = idClient;
                        newmodel.idContPerson = 0;
                        newmodel.isDebitor = true;
                        newmodel.isCreditor = false;
                        newmodel.accNumber = tmpmod.accountCodeClient;

                        if (genmX != null && genmX.numberLedgerAccount != null)
                            newmodel.debAccount = genmX.numberLedgerAccount;

                        if (ibanlist != null && ibanlist.Count > 0)
                        {
                            newmodel.iban = ibanlist[0].ibanNumber;
                        }

                        accbus.Save(newmodel, this.Name, Login._user.idUser);
                    }

                }
            }

            if (idPerson > 0)
            {
                AccDebCreBUS accbus = new AccDebCreBUS();
                AccDebCreModel cmodel = accbus.GetPersonDebCre(idPerson);

                if (cmodel != null)
                {
                    if (cmodel.isDebitor == false)
                    {
                        cmodel.isDebitor = true;
                        accbus.UpdateDebitorToTrue(cmodel.idAccDebCre, this.Name, Login._user.idUser);
                    }
                }
                else
                {
                    PersonBUS cbus = new PersonBUS();                                        
                    AccDebCreModel newmodel = new AccDebCreModel();
                    PersonModel tmpmod = cbus.GetPerson(idPerson);

                    if (tmpmod != null)
                    {
                        newmodel.idClient = 0;
                        newmodel.idContPerson = idPerson;
                        newmodel.isDebitor = true;
                        newmodel.isCreditor = false;
                        newmodel.accNumber = idPerson.ToString().PadLeft(6, '0');

                        if (genmX != null && genmX.numberLedgerAccount != null)
                            newmodel.debAccount = genmX.numberLedgerAccount;

                        accbus.Save(newmodel, this.Name, Login._user.idUser);
                    }
                }
            }
        }
        
        private void btnDaily_Click(object sender, EventArgs e)
        {
            AccDailyBUS acd = new AccDailyBUS(Login._bookyear);
            List<IModel> acm = new List<IModel>();


            acm = acd.GetBookingDailys();
            var dlgSave = new GridLookupForm(acm, "Daily");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                genmX = new AccDailyModel();
                genmX = (AccDailyModel)dlgSave.selectedRow;

                if (genmX != null)
                {
                    //set textbox
                    if (genmX.codeDaily != null)
                    {
                        txtDaily.Text = genmX.codeDaily + "   " + genmX.descDaily;
                        codeD = genmX.codeDaily;
                        
                        btnBooking.Enabled = true;
                    }
                }
                else
                {
                    RadMessageBox.Show("Can't booking without Verkoop book !!!");
                    btnBooking.Enabled = false;
                }
            }
        }

        private void btnSelectAll_CheckStateChanged(object sender, EventArgs e)
        {
            SelectAllDeselectAll(btnSelectAll.Checked);
        }

        private void SelectAllDeselectAll(bool select)
        {
            if(select == true)
            {
                foreach(var m in limBind)
                {
                    m.select = true;
                }
            }
            else
            {
                foreach (var m in limBind)
                {
                    m.select = false;
                }
            }

            this.rgvInvoice.MasterTemplate.Refresh(null);
        }

        private void radMenuButtonSaveLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutInvoiceSelectionForAccounting))
            {
                File.Delete(layoutInvoiceSelectionForAccounting);
            }
            rgvInvoice.SaveLayout(layoutInvoiceSelectionForAccounting);

            if (rgvInvoice.Columns["select"] != null)
                rgvInvoice.Columns["select"].IsVisible = true;

            translateRadMessageBox tr = new translateRadMessageBox();
            tr.translateAllMessageBox("Layout saved");
        }
    }
}
