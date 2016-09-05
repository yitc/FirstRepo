using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Model;
using BIS.Business;
using System.Linq;
using Telerik.WinControls.UI;
using Telerik.WinControls.Data;
using System.IO;
using System.Resources;

namespace GUI
{
    public partial class frmOpenLines : RadForm
    {
        readonly List<string> _tempFolders = new List<string>();

        BindingList<AccOpenLinesReportModel> openLines;
        BindingList<AccOpenLinesReportModel> openLinesLetters = new BindingList<AccOpenLinesReportModel>();

        List<AccOpenLinesReportModel> openLinesForPrint;        

        DataTable dtOpenLiens; // everything on grid letters
        DataTable dtOpenLinesForPrint; // open lines that selected for printing

        DataTable dtOpenLiensForReport; // for microsoft report

        int clientID;
        int personID;
        int warningTypeSelection = 0;

        string tempFolder = null;

        private string layoutOpenLines = MainForm.gridFiltersFolder + "\\layoutFrmOpenLines.xml";
        private string layoutOpenLineLetters = MainForm.gridFiltersFolder + "\\layoutFrmOpenLinesLetters.xml";

        public frmOpenLines()
        {
            personID = 0;
            clientID = 0;

            InitializeComponent();

            radio1st.CheckState = CheckState.Checked;

            this.Icon = Login.iconForm;

        }
        private void Translation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(this.Text) != null)
                    this.Text = resxSet.GetString(this.Text);

                if (resxSet.GetString(tabOverview.Text) != null)
                    tabOverview.Text = resxSet.GetString(tabOverview.Text);

                if (resxSet.GetString(tabLetters.Text) != null)
                    tabLetters.Text = resxSet.GetString(tabLetters.Text);

                if (resxSet.GetString(radio_filter_all.Text) != null)
                    radio_filter_all.Text = resxSet.GetString(radio_filter_all.Text);

                if (resxSet.GetString(radio_filter_debitor.Text) != null)
                    radio_filter_debitor.Text = resxSet.GetString(radio_filter_debitor.Text);

                if (resxSet.GetString(radio_filter_creditor.Text) != null)
                    radio_filter_creditor.Text = resxSet.GetString(radio_filter_creditor.Text);

                if (resxSet.GetString(panelFilter.Text) != null)
                    panelFilter.Text = resxSet.GetString(panelFilter.Text);

                if (resxSet.GetString(chkClientFilter.Text) != null)
                    chkClientFilter.Text = resxSet.GetString(chkClientFilter.Text);

                if (resxSet.GetString(radioDebitor.Text) != null)
                    radioDebitor.Text = resxSet.GetString(radioDebitor.Text);

                if (resxSet.GetString(radioCreditor.Text) != null)
                    radioCreditor.Text = resxSet.GetString(radioCreditor.Text);

                if (resxSet.GetString(btnWordDocument.Text) != null)
                    btnWordDocument.Text = resxSet.GetString(btnWordDocument.Text);                

                if (resxSet.GetString(btnPrint.Text) != null)
                    btnPrint.Text = resxSet.GetString(btnPrint.Text);
                
                if (resxSet.GetString(printWithDays.Text) != null)
                    printWithDays.Text = resxSet.GetString(printWithDays.Text);

                if (resxSet.GetString(radio1st.Text) != null)
                    radio1st.Text = resxSet.GetString(radio1st.Text);

                if (resxSet.GetString(radio2nd.Text) != null)
                    radio2nd.Text = resxSet.GetString(radio2nd.Text);

                if (resxSet.GetString(radio3rd.Text) != null)
                    radio3rd.Text = resxSet.GetString(radio3rd.Text);

                if (resxSet.GetString(radioByEmail.Text) != null)
                    radioByEmail.Text = resxSet.GetString(radioByEmail.Text);

                if (resxSet.GetString(radioByPost.Text) != null)
                    radioByPost.Text = resxSet.GetString(radioByPost.Text);

                if (resxSet.GetString(btnShowByWarning.Text) != null)
                    btnShowByWarning.Text = resxSet.GetString(btnShowByWarning.Text);

                if (resxSet.GetString(radMenuButtonSaveLayout.Text) != null)
                    radMenuButtonSaveLayout.Text = resxSet.GetString(radMenuButtonSaveLayout.Text);

                if (resxSet.GetString(radMenuButtonSaveLayoutLetters.Text) != null)
                    radMenuButtonSaveLayoutLetters.Text = resxSet.GetString(radMenuButtonSaveLayoutLetters.Text);

            }
        }
        private void Overview_RemoveRowWithZeros()
        {
            foreach (AccOpenLinesReportModel m in openLines)
            {
                if (m.dif == 0)
                {
                    openLines.Remove(m);
                }
            }
        }
        
        private void Letters_RemoveRowWithZeros()
        {
            foreach (AccOpenLinesReportModel m in openLinesLetters)
            {
                if (m.dif == 0)
                {
                    openLinesLetters.Remove(m);
                }
            }
        }
        private void onCheckDebitCreditChange()
        {
            if (chkClientFilter.Checked == true)
            {
                txtClient.Enabled = true;
                txtClientName.Enabled = true;
                btnClient.Enabled = true;
                radioDebitor.Enabled = true;
                radioCreditor.Enabled = true;

                gridOpenLines.EnableFiltering = false;
                radio_filter_all.Enabled = false;
                radio_filter_debitor.Enabled = false;
                radio_filter_creditor.Enabled = false;
                
                if (txtClient.Text.Trim() != "")
                {
                    FilterByDateAndDebCre(txtClient.Text);                    

                    DebCreLookupBUS debpers = new DebCreLookupBUS();
                    DataTable dt = debpers.GetIDByAccNumber(txtClient.Text);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int personID = Int32.Parse(dt.Rows[0]["idContPerson"].ToString());
                        int clientID = Int32.Parse(dt.Rows[0]["idClient"].ToString());                        
                    }

                    btnWordDocument.Visible = true;
                }
            }
            else
            {
                txtClient.Enabled = false;
                txtClientName.Enabled = false;
                btnClient.Enabled = false;
                radioDebitor.Enabled = false;
                radioCreditor.Enabled = false;

                btnWordDocument.Visible = false;
                gridOpenLines.EnableFiltering = true;
                radio_filter_all.Enabled = true;
                radio_filter_debitor.Enabled = true;
                radio_filter_creditor.Enabled = true;

                personID = 0;
                clientID = 0;

                FilterByDate();
            }
        }
        private void frmReportOpenLines_Load(object sender, EventArgs e)
        {
            tempFolder = GetTemporaryFolder();
            _tempFolders.Add(tempFolder);

            //ribbonExampleMenu.Visible = false;
            openLines = new BindingList<AccOpenLinesReportModel>();
            //openLinesLetters = 

            ddlLabel.DataSource = new LabelsBUS().GetDistinctLabels();
            ddlLabel.DisplayMember = "nameLabel";
            ddlLabel.ValueMember = "idLabel";

            AccOpenLinesBUS bus = new AccOpenLinesBUS();
            List<AccOpenLinesReportModel> lista = new List<AccOpenLinesReportModel>();
            lista = bus.GetAccOpenLineReport(dateTo.Value,0);
            dtOpenLiensForReport = bus.GetAccOpenLineReport_dt(dateTo.Value,0);
            dtOpenLiens = new DataTable();

            if(lista != null)
            {
                openLines = new BindingList<AccOpenLinesReportModel>(lista);
            }
            openLinesForPrint = new List<AccOpenLinesReportModel>();            
            onCheckDebitCreditChange();

            Overview_RemoveRowWithZeros();
            gridOpenLines.DataSource = openLines;
            gridOpenLinesLetters.DataSource = openLinesLetters;

            gridOpenLines.EnableFiltering = true;
            dateTo.Format = DateTimePickerFormat.Custom;
            dateTo.CustomFormat = "dd - MM - yyyy";
            this.pageViewOpenLines.SelectedPage = tabOverview;

            Translation();
            
        }

        private void gridOpenLines_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            if (e.Column.Name == "dtOpenLine" || e.Column.Name == "dtPayOpenLine"
                || e.Column.Name == "dtFirstWarrning" || e.Column.Name == "dtSecondWarrning" || e.Column.Name == "dtCreationLine")
            {
                if (e.Column.IsVisible == true)
                {
                    try
                    {
                        //DateTime temp = DateTime.Parse(e.CellElement.Text);
                        if (e.CellElement.Value != null)
                        {
                            DateTime temp = DateTime.Parse(e.CellElement.Value.ToString());
                            e.CellElement.Text = temp.ToString("dd-MM-yyyy");
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }                  
        }
        private void gridOpenLinesLetters_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Column.Name == "dtOpenLine" || e.Column.Name == "dtPayOpenLine"
                || e.Column.Name == "dtFirstWarrning" || e.Column.Name == "dtSecondWarrning" || e.Column.Name == "dtCreationLine")
            {
                if (e.Column.IsVisible == true)
                {
                    try
                    {
                        if (e.CellElement.Value != null)
                        {
                            //DateTime temp = DateTime.Parse(e.CellElement.Text);
                            DateTime temp = DateTime.Parse(e.CellElement.Value.ToString());
                            e.CellElement.Text = temp.ToString("dd-MM-yyyy");
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            
        }

        
        private void FilterByDate()
        {
            Cursor.Current = Cursors.WaitCursor;

            openLines = new BindingList<AccOpenLinesReportModel>();

            AccOpenLinesBUS bus = new AccOpenLinesBUS();
            List<AccOpenLinesReportModel> lista = new List<AccOpenLinesReportModel>();
            lista = bus.GetAccOpenLineReport(dateTo.Value,0);
            dtOpenLiensForReport = bus.GetAccOpenLineReport_dt(dateTo.Value,0);

            if (lista != null)
            {
                openLines = new BindingList<AccOpenLinesReportModel>(lista);
            }

            Overview_RemoveRowWithZeros();
            //openLinesForPrint = new List<AccOpenLinesReportModel>();
            //gridOpenLines.DataSource = null;
            gridOpenLines.DataSource = openLines;

            Cursor.Current = Cursors.Default;
        }

        private void FilterByDateAndDebCre(string accDebCre)
        {
            Cursor.Current = Cursors.WaitCursor;

            openLines = new BindingList<AccOpenLinesReportModel>();

            AccOpenLinesBUS bus = new AccOpenLinesBUS();
            List<AccOpenLinesReportModel> lista = new List<AccOpenLinesReportModel>();
            lista = bus.GetAccOpenLineReportDateAndAccNumber(dateTo.Value, accDebCre);
            //dtOpenLiens = bus.GetAccOpenLineReportDateAndAccNumber_dt(dateTo.Value, accDebCre);
           
            dtOpenLiensForReport = bus.GetAccOpenLineReportDateAndAccNumber_dt(dateTo.Value, accDebCre);

            if (lista != null)
            {
                openLines = new BindingList<AccOpenLinesReportModel>(lista);
            }
     

            Overview_RemoveRowWithZeros();
            //openLinesForPrint = new List<AccOpenLinesReportModel>();
            //gridOpenLines.DataSource = null;
            gridOpenLines.DataSource = openLines;

            Cursor.Current = Cursors.Default;
        }

        public void ShowDebitorsCreditorsInGrid()
        {            
            if(radio_filter_debitor.IsChecked == true)
            {
                gridOpenLines.FilterDescriptors.Clear();
                FilterDescriptor filter = new FilterDescriptor();
                filter.PropertyName = "isDebitor";
                filter.Operator = FilterOperator.IsEqualTo;
                filter.Value = true;
                filter.IsFilterEditor = true;
                this.gridOpenLines.FilterDescriptors.Add(filter);
                //this.gridOpenLines.Columns["isDebitor"].FilterDescriptor = filter;

            }
            else if(radio_filter_creditor.IsChecked == true)
            {
                gridOpenLines.FilterDescriptors.Clear();
                FilterDescriptor filter = new FilterDescriptor();
                filter.PropertyName = "isCreditor";
                filter.Operator = FilterOperator.IsEqualTo;
                filter.Value = true;
                filter.IsFilterEditor = true;
                this.gridOpenLines.FilterDescriptors.Add(filter);
                //this.gridOpenLines.Columns["isCreditor"].FilterDescriptor = filter;
            }
            else
            {
                gridOpenLines.FilterDescriptors.Clear();
            }
        }


        private void dateTo_ValueChanged(object sender, EventArgs e)
        {
            FilterByDate();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //Color[] c = new Color[8];
            //c[0] = Color.LightGray;
            //c[1] = Color.Black;
            //c[2] = Color.White;
            //c[3] = Color.Black;
            //c[4] = Color.LightGray;
            //c[5] = Color.Black;
            //c[6] = Color.White;
            //c[7] = Color.Black;

            //try
            //{
            //    System.Drawing.Image image1 = System.Drawing.Image.FromFile(@"..\\Test.jpeg");        
            //    ReportViewerDepartureList rvf = new ReportViewerDepartureList(dtOpenLiens, c, null, null, null, openLines, "", "", "", this.Text.Replace(Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - ", ""));
            //    rvf.Show();

            //}catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            if (chkClientFilter.Checked == false)
            {
                Cursor.Current = Cursors.WaitCursor;
                AccOpenLinesBUS bus = new AccOpenLinesBUS();
                int param = 0;
                if (radio_filter_all.IsChecked == true)
                    param = 0;
                else if (radio_filter_debitor.IsChecked == true)
                    param = 1;
                else if (radio_filter_creditor.IsChecked == true)
                    param = 2;
                dtOpenLiensForReport = bus.GetAccOpenLineReport_dt(dateTo.Value, param);
                Cursor.Current = Cursors.Default;
            }
            
            frmReportOpenLines frmOpenLines = new frmReportOpenLines(dtOpenLiensForReport);
            frmOpenLines.ShowDialog();

        }

        private void btnClient_Click(object sender, EventArgs e)
        {           
           if(radioDebitor.IsChecked == true)
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
                   txtClient.Text = pm1X.accNumber;                   
                   txtClientName.Text = pm1X.name;

                   DataTable dt = debpers.GetIDByAccNumber(pm1X.accNumber);
                   if (dt != null && dt.Rows.Count > 0)
                   {
                       personID = Int32.Parse(dt.Rows[0]["idContPerson"].ToString());
                       clientID = Int32.Parse(dt.Rows[0]["idClient"].ToString());                                              
                   }

                   FilterByDateAndDebCre(pm1X.accNumber);
                   btnWordDocument.Visible = true;
               }
           }
           else
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
                   txtClient.Text = pm1X.accNumber;                   
                   txtClientName.Text = pm1X.name;

                   DataTable dt = debpers.GetIDByAccNumber(pm1X.accNumber);
                   if (dt != null && dt.Rows.Count > 0)
                   {
                       personID = Int32.Parse(dt.Rows[0]["idContPerson"].ToString());
                       clientID = Int32.Parse(dt.Rows[0]["idClient"].ToString());                                                                                                                       
                   }

                   FilterByDateAndDebCre(pm1X.accNumber);
               }               
           }                                   
        }

        private void chkClientFilter_CheckStateChanged(object sender, EventArgs e)
        {
            onCheckDebitCreditChange();
        }

        private Microsoft.Office.Interop.Word.Application wordApp;
        private void btnWordDocument_Click(object sender, EventArgs e)
        {

            if(personID != 0)
            {
                List<BIS.Model.IModel> lookupModel = new List<BIS.Model.IModel>();
                BIS.Business.LayoutsBUS bBUS = new BIS.Business.LayoutsBUS();
                lookupModel = bBUS.GetAllLayoutsbyTemplateTable("ContactPerson");

                var lookfrm = new GridLookupForm(lookupModel, "Templates");
                if (lookfrm.ShowDialog(this) == DialogResult.Yes)
                {
                    BookmarkFunctions.ReadTemplateFile(wordApp, "ContactPerson", "idContPers", personID, dtOpenLiensForReport, (BIS.Model.LayoutsModel)lookfrm.selectedRow, this.Name, Login._user.idUser);
                }
            }

            if (clientID != 0)
            {
                //BookmarkFunctions.ReadTemplateFile(wordApp, "Client", "idClient", personID, dtOpenLiens);
            }
        }        

        private void gridOpenLines_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
            if (File.Exists(layoutOpenLines))
            {
                gridOpenLines.LoadLayout(layoutOpenLines);
            }

            if (this.gridOpenLines.Columns != null)
            {                
                //this.gridOpenLines.Columns["idContPers"].IsVisible = false;
                //this.gridOpenLines.Columns["idClient"].IsVisible = false;
                //this.gridOpenLines.Columns["chk"].IsVisible = false;
                //this.gridOpenLines.Columns["isInvoicing"].IsVisible = false;
                //this.gridOpenLines.Columns["isDebitor"].IsVisible = false;
                //this.gridOpenLines.Columns["isCreditor"].IsVisible = false;
                //this.gridOpenLines.Columns["email"].IsVisible = false;
                //this.gridOpenLines.Columns["dtFirstWarrning"].IsVisible = false;
                //this.gridOpenLines.Columns["dtSecondWarrning"].IsVisible = false;
                //this.gridOpenLines.Columns["bookingYear"].IsVisible = false;


                foreach(GridViewColumn col in  gridOpenLines.Columns)
                {
                    if(col.Name != "chk")
                    {
                        col.ReadOnly = true;
                    }
                }
                if (gridOpenLines.Columns["chk"] != null)
                    gridOpenLines.Columns["chk"].IsVisible = false;

                SortDescriptor descriptor = new SortDescriptor();
                descriptor.PropertyName = "dtOpenLine";
                descriptor.Direction = ListSortDirection.Descending;
                this.gridOpenLines.MasterTemplate.SortDescriptors.Add(descriptor);

                for (int i = 0; i < gridOpenLines.Columns.Count; i++)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (gridOpenLines.Columns[i].HeaderText != null && resxSet.GetString(gridOpenLines.Columns[i].HeaderText) != null)
                            gridOpenLines.Columns[i].HeaderText = resxSet.GetString(gridOpenLines.Columns[i].HeaderText);
                    }
                }
            }
        }

        private void gridOpenLines_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            
        }

        private void gridOpenLines_ValueChanged(object sender, EventArgs e)
        {
            if (this.gridOpenLines.ActiveEditor is RadCheckBoxEditor)
            {
                if (this.gridOpenLines.CurrentRow != null && this.gridOpenLines.CurrentRow.DataBoundItem != null)
                {
                    AccOpenLinesReportModel model = (AccOpenLinesReportModel)this.gridOpenLines.CurrentRow.DataBoundItem;
                    
                    
                    //ide obrnuto ako je cekiran
                    if (this.gridOpenLines.ActiveEditor.Value.ToString() == "On")
                    {
                        //openLinesForPrint.Add(model);

                        foreach (AccOpenLinesReportModel m in openLines)
                        {
                            if (model.idContPers != 0)
                            {
                                if (m.idContPers == model.idContPers)
                                {
                                    //if (m.chk == false)
                                    m.chk = true;
                                }
                            }
                            else
                            {
                                //client
                                if (m.idClient == model.idClient)
                                {
                                    //if (m.chk == false)
                                    m.chk = true;
                                }
                            }
                        }

                    }
                    else
                    {
                        //openLinesForPrint.Remove(model);
                        foreach (AccOpenLinesReportModel m in openLines)
                        {
                            if (model.idContPers != 0)
                            {
                                if (m.idContPers == model.idContPers)
                                {
                                    m.chk = false;
                                }
                            }
                            else
                            {
                                if (m.idClient == model.idClient)
                                {
                                    m.chk = false;
                                }
                            }
                        }
                        
                    }                    
                }

                this.gridOpenLines.EndEdit();
                //this.gridOpenLines.Update();
                //this.gridOpenLines.Invalidate();
                //this.gridOpenLines.DataSource = openLines;
            }
        }
        private static string GetTemporaryFolder()
        {
            var tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }
        private void PrintOpenLines(bool sendByEmail)
        {
            //sendByEmail = true; - sending by email
            //sendByEmail = false; - sending by post

            if (dtOpenLiens.Rows.Count <= 0)
            {
                MessageBox.Show("Nothing to print.");
                return;
            }

            gridOpenLinesLetters.EndEdit();

            translateRadMessageBox tr = new translateRadMessageBox();
            DialogResult dr = tr.translateAllMessageBoxDialog("Print open lines documents for selected person ?", "Print");
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    openLinesForPrint = new List<AccOpenLinesReportModel>();
                    dtOpenLinesForPrint = dtOpenLiens.Clone();
                    List<int> personsToPrint = new List<int>();
                    
                    
                    //dodaj prvo cekirane koji su za print
                    foreach (AccOpenLinesReportModel m in openLinesLetters)
                    {
                        if (m.chk == true)
                            openLinesForPrint.Add(m);
                    }

                    if (openLinesForPrint.Count <= 0)
                    {
                        MessageBox.Show("Please select what you want to print.");
                        return;
                    }

                    // onda od cekiranih izdvoji samo distinct persone
                    foreach (AccOpenLinesReportModel m in openLinesForPrint)
                    {

                        int isInList = personsToPrint.IndexOf(m.idContPers);
                        if (m.idContPers != 0 && isInList == -1)
                        {
                            personsToPrint.Add(m.idContPers);
                        }                      
                    }

                    //print documents

                    List<BIS.Model.IModel> lookupModel = new List<BIS.Model.IModel>();
                    BIS.Business.LayoutsBUS bBUS = new BIS.Business.LayoutsBUS();
                    lookupModel = bBUS.GetAllLayoutsbyTemplateTable("ContactPerson");

                    var lookfrm = new GridLookupForm(lookupModel, "Templates");
                    if (lookfrm.ShowDialog(this) == DialogResult.Yes)
                    {
                        foreach (int n in personsToPrint)
                        {
                            //if (personID != 0)
                            // {
                            dtOpenLinesForPrint.Rows.Clear();
                            foreach (DataRow row in dtOpenLiens.Rows)
                            {
                                if (row["idContPerson"].ToString().Trim() == n.ToString())
                                {
                                    //dtOpenLinesForPrint.Rows.Add(newRow);
                                    dtOpenLinesForPrint.ImportRow(row);
                                }
                            }

                            PrintDialog printDialog1 = new PrintDialog();
                                //printDialog1.Document = printDocument1;            
                                DialogResult result = printDialog1.ShowDialog();
                                if (result == DialogResult.OK)
                                {
                                    BookmarkFunctions.ReadTemplateFileAndPrint(wordApp, "ContactPerson", "idContPers", n, dtOpenLinesForPrint,
                                        (BIS.Model.LayoutsModel)lookfrm.selectedRow, tempFolder, printDialog1.PrinterSettings.PrinterName, this.Name, Login._user.idUser);
                                }
                            
                            

                            if (clientID != 0)
                            {
                                //ReadTemplateFile(wordApp, "Client", "idClient", personID, dtOpenLiens);
                            }
                        }
                    }

                    Cursor.Current = Cursors.Default;
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void PrintOrEmailOpenLInes(bool sendByEmail, string printername, string subject, string body_message)
        {
            //sendByEmail = true; - sending by email
            //sendByEmail = false; - sending by post            

            if (dtOpenLiens.Rows.Count <= 0)
            {
                MessageBox.Show("Nothing to print.");
                return;
            }

            gridOpenLinesLetters.EndEdit();

            //translateRadMessageBox tr = new translateRadMessageBox();
            //DialogResult dr = tr.translateAllMessageBoxDialog("Print open lines documents for selected person ?", "Print");
            //if (dr == System.Windows.Forms.DialogResult.Yes)
            //{
                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    openLinesForPrint = new List<AccOpenLinesReportModel>();
                    dtOpenLinesForPrint = dtOpenLiens.Clone();
                    //List<int> personsToPrint = new List<int>();
                    List<AccOpenLinesReportModel> personsToPrint = new List<AccOpenLinesReportModel>();

                    //dodaj prvo cekirane koji su za print
                    foreach (AccOpenLinesReportModel m in openLinesLetters)
                    {
                        if (m.chk == true)
                            openLinesForPrint.Add(m);
                    }

                    if (openLinesForPrint.Count <= 0)
                    {
                        MessageBox.Show("Please select what you want to print.");
                        return;
                    }

                   
                    // onda od cekiranih izdvoji samo distinct persone
                    foreach (AccOpenLinesReportModel m in openLinesForPrint)
                    {

                        //int isInList = personsToPrint.IndexOf(m.idContPers);
                        //if (m.idContPers != 0 && isInList == -1)
                        //{
                        //    personsToPrint.Add(m.idContPers);
                        //}

                        var isInList = personsToPrint.Find(s => s.idContPers == m.idContPers);
                        if ((m.idContPers != 0 || m.idClient != 0) && isInList == null)
                        {                            
                            personsToPrint.Add(m);
                        }
                    }

                    //print documents

                    List<BIS.Model.IModel> lookupModel = new List<BIS.Model.IModel>();
                    BIS.Business.LayoutsBUS bBUS = new BIS.Business.LayoutsBUS();
                    lookupModel = bBUS.GetAllLayoutsbyTemplateTable("ContactPerson");

                    var lookfrm = new GridLookupForm(lookupModel, "Templates");
                    if (lookfrm.ShowDialog(this) == DialogResult.Yes)
                    {
                        foreach (var n in personsToPrint)
                        {
                            //if (personID != 0)
                            // {
                            dtOpenLinesForPrint.Rows.Clear();
                            foreach (DataRow row in dtOpenLiens.Rows)
                            {
                                if (row["idContPerson"].ToString().Trim() == n.idContPers.ToString())
                                {
                                    //dtOpenLinesForPrint.Rows.Add(newRow);
                                    dtOpenLinesForPrint.ImportRow(row);
                                }
                            }

                            if (sendByEmail == true)
                            {
                                BookmarkFunctions.ReadTemplateFileAndSendEmail(wordApp, "ContactPerson", "idContPers", n.idContPers,
                                   dtOpenLinesForPrint, (BIS.Model.LayoutsModel)lookfrm.selectedRow,
                                   n.email, tempFolder, subject, body_message, this.Name, Login._user.idUser);                                
                            }
                            else
                            {                                                                
                                BookmarkFunctions.ReadTemplateFileAndPrint(wordApp, "ContactPerson", "idContPers", n.idContPers,
                                    dtOpenLinesForPrint, (BIS.Model.LayoutsModel)lookfrm.selectedRow, tempFolder, printername, this.Name, Login._user.idUser);       
                            }
                            
                            // }

                            if (clientID != 0)
                            {
                                //ReadTemplateFile(wordApp, "Client", "idClient", personID, dtOpenLiens);
                            }
                        }

                        UpdateSentWarningStatus(dtOpenLinesForPrint);
                    }

                    Cursor.Current = Cursors.Default;
                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(ex.Message);
                }
            //} //dialog message
        }


        private void UpdateSentWarningStatus(DataTable dt)
        {

            if (radio3rd.IsChecked == false)
            {
                if (warningTypeSelection == 1)
                {
                    translateRadMessageBox msg = new translateRadMessageBox();
                    DialogResult dr = msg.translateAllMessageBoxDialogYesNo("Save first warning ?", "Save");
                    if (dr == DialogResult.Yes)
                    {
                        AccOpenLinesBUS abus = new AccOpenLinesBUS();
                        bool b = abus.SaveAccOpenLinesReportSent_1stWarning(dt, 1, this.Name, Login._user.idUser);
                        if (b == true)
                            LoadOpenLines();
                    }
                }
                else if (warningTypeSelection == 2)
                {
                    translateRadMessageBox msg = new translateRadMessageBox();
                    DialogResult dr = msg.translateAllMessageBoxDialogYesNo("Save second warning ?", "Save");
                    if (dr == DialogResult.Yes)
                    {
                        AccOpenLinesBUS abus = new AccOpenLinesBUS();
                        bool b = abus.SaveAccOpenLinesReportSent_1stWarning(dt, 2, this.Name, Login._user.idUser);
                        if (b == true)
                            LoadOpenLines();
                    }
                }
                else
                {

                }                
            }
                        
        }


        private void radButton1_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog1 = new PrintDialog();
            //printDialog1.Document = printDocument1;    
            printDialog1.AllowSomePages = false;
            printDialog1.AllowSelection = false;
            printDialog1.AllowPrintToFile = false;
            printDialog1.AllowCurrentPage = false;
            //printDialog1.
            
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                PrintOrEmailOpenLInes(false, printDialog1.PrinterSettings.PrinterName,"","");
            }
                         
        }

        private void gridOpenLines_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
            
        }

        private void gridOpenLines_CellClick(object sender, GridViewCellEventArgs e)
        {
            
            
        }

        private void pageViewOpenLines_SelectedPageChanged(object sender, EventArgs e)
        {
            RadPageView rpv = (RadPageView)sender;
            string sName = ((RadPageView)sender).SelectedPage.Name;
            
            switch (sName)
            {
                case "tabLetters":
                    OnTabLetters();
                    break;
                default:
                    break;

            }
        }

        private void OnTabLetters()
        {

        }

        private void btnShowByWarning_Click(object sender, EventArgs e)
        {
            LoadOpenLines();   
        }

        private void LoadOpenLines()
        {
            if (radio1st.IsChecked == true)
            {
                radioByEmail.IsChecked = false;
                radioByPost.IsChecked = false;
                btnSendByEmail.Visible = false;
                btnSendByPost.Visible = false;

                if (chkLabel.Checked == true)
                    GetOpenLines_1stWarning_ByLabel((int)ddlLabel.SelectedValue);
                else
                    GetOpenLines_1stWarning();

                warningTypeSelection = 1;

            }
            else if (radio2nd.IsChecked == true)
            {
                radioByEmail.IsChecked = false;
                radioByPost.IsChecked = false;
                btnSendByEmail.Visible = false;
                btnSendByPost.Visible = false;

                if (chkLabel.Checked == true)
                    GetOpenLines_2ndWarning_ByLabel((int)ddlLabel.SelectedValue);
                else
                    GetOpenLines_2ndWarning();

                warningTypeSelection = 2;
            }
            else if (radio3rd.IsChecked == true)
            {
                radioByEmail.IsChecked = false;
                radioByPost.IsChecked = false;
                btnSendByEmail.Visible = false;
                btnSendByPost.Visible = false;

                if (chkLabel.Checked == true)
                    GetOpenLines_3rdWarning_ByLabel((int)ddlLabel.SelectedValue);
                else
                    GetOpenLines_3rdWarning();

                warningTypeSelection = 3;

            }
        }

        private void GetOpenLines_1stWarning()
        {
            Cursor.Current = Cursors.WaitCursor;
            
            AccOpenLinesBUS bus = new AccOpenLinesBUS();
            List<AccOpenLinesReportModel> lista = new List<AccOpenLinesReportModel>();
            lista = bus.GetAccOpenLineReport_1stWarringn();
            dtOpenLiens = bus.GetAccOpenLineReport_1stWarringn_dt();
            
            
            openLinesForPrint = new List<AccOpenLinesReportModel>();

            RemoveFromOpenLinesLetter();
            AddToOpenLinesLetter(lista);


            Cursor.Current = Cursors.Default;
        }
        private void GetOpenLines_1stWarning_ByLabel(int label)
        {
            Cursor.Current = Cursors.WaitCursor;

            AccOpenLinesBUS bus = new AccOpenLinesBUS();
            List<AccOpenLinesReportModel> lista = new List<AccOpenLinesReportModel>();
            lista = bus.GetAccOpenLineReport_1stWarringn_ByLabel(label);
            dtOpenLiens = bus.GetAccOpenLineReport_1stWarringn_ByLabel_dt(label);


            openLinesForPrint = new List<AccOpenLinesReportModel>();

            RemoveFromOpenLinesLetter();
            AddToOpenLinesLetter(lista);


            Cursor.Current = Cursors.Default;
        }

        private void GetOpenLines_2ndWarning()
        {
            Cursor.Current = Cursors.WaitCursor;
            
            AccOpenLinesBUS bus = new AccOpenLinesBUS();
            List<AccOpenLinesReportModel> lista = new List<AccOpenLinesReportModel>();
            lista = bus.GetAccOpenLineReport_2ndWarringn();
            dtOpenLiens = bus.GetAccOpenLineReport_2ndWarringn_dt();
            
            openLinesForPrint = new List<AccOpenLinesReportModel>();


            RemoveFromOpenLinesLetter();
            AddToOpenLinesLetter(lista);
            
        }

        private void GetOpenLines_2ndWarning_ByLabel(int label)
        {
            Cursor.Current = Cursors.WaitCursor;

            AccOpenLinesBUS bus = new AccOpenLinesBUS();
            List<AccOpenLinesReportModel> lista = new List<AccOpenLinesReportModel>();
            lista = bus.GetAccOpenLineReport_2ndWarringn_ByLabel(label);
            dtOpenLiens = bus.GetAccOpenLineReport_2ndWarringn_ByLabel_dt(label);

            openLinesForPrint = new List<AccOpenLinesReportModel>();


            RemoveFromOpenLinesLetter();
            AddToOpenLinesLetter(lista);

        }

        private void GetOpenLines_3rdWarning()
        {
            Cursor.Current = Cursors.WaitCursor;

            AccOpenLinesBUS bus = new AccOpenLinesBUS();
            List<AccOpenLinesReportModel> lista = new List<AccOpenLinesReportModel>();
            lista = bus.GetAccOpenLineReport_3rdWarringn();
            dtOpenLiens = bus.GetAccOpenLineReport_3rdWarringn_dt();            
           
            openLinesForPrint = new List<AccOpenLinesReportModel>();
            
            RemoveFromOpenLinesLetter();
            AddToOpenLinesLetter(lista);
        }
        private void GetOpenLines_3rdWarning_ByLabel(int label)
        {
            Cursor.Current = Cursors.WaitCursor;

            AccOpenLinesBUS bus = new AccOpenLinesBUS();
            List<AccOpenLinesReportModel> lista = new List<AccOpenLinesReportModel>();
            lista = bus.GetAccOpenLineReport_3rdWarringn_ByLabel(label);
            dtOpenLiens = bus.GetAccOpenLineReport_3rdWarringn_ByLabel_dt(label);

            openLinesForPrint = new List<AccOpenLinesReportModel>();

            RemoveFromOpenLinesLetter();
            AddToOpenLinesLetter(lista);
        }

        private void AddToOpenLinesLetter(List<AccOpenLinesReportModel> lista)
        {
            if(lista != null)
            {
                //BindingList<AccOpenLinesReportModel>  tmp = new BindingList<AccOpenLinesReportModel>(lista);
                foreach (AccOpenLinesReportModel m in lista)
                {
                    if (m.dif != 0)
                        openLinesLetters.Add(m);
                }
            }
        }

        private void RemoveFromOpenLinesLetter()
        {
            foreach(AccOpenLinesReportModel m in openLinesLetters)
            {
                m.chk = false;
            }
            openLinesLetters.Clear();            
        }

        private void gridOpenLinesLetters_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if (File.Exists(layoutOpenLineLetters))
            {
                gridOpenLinesLetters.LoadLayout(layoutOpenLineLetters);
            }

            if (this.gridOpenLinesLetters.Columns != null)
            {
                //this.gridOpenLinesLetters.Columns["idContPers"].IsVisible = false;
                //this.gridOpenLinesLetters.Columns["idClient"].IsVisible = false;
                //this.gridOpenLinesLetters.Columns["isInvoicing"].IsVisible = false;
                //this.gridOpenLinesLetters.Columns["isDebitor"].IsVisible = false;
                //this.gridOpenLinesLetters.Columns["isCreditor"].IsVisible = false;
                //this.gridOpenLinesLetters.Columns["email"].IsVisible = false;
                //this.gridOpenLinesLetters.Columns["dtFirstWarrning"].IsVisible = false;
                //this.gridOpenLinesLetters.Columns["dtSecondWarrning"].IsVisible = false;
                //this.gridOpenLinesLetters.Columns["bookingYear"].IsVisible = false;

                //this.gridOpenLines.Columns["chk"].IsVisible = false;

                foreach (GridViewColumn col in gridOpenLinesLetters.Columns)
                {
                    if (col.Name != "chk")
                    {
                        col.ReadOnly = true;
                    }
                }
                if (gridOpenLinesLetters.Columns["chk"] != null)
                    gridOpenLinesLetters.Columns["chk"].IsVisible = true;

                //SortDescriptor descriptor = new SortDescriptor();
                //descriptor.PropertyName = "dtOpenLine";
                //descriptor.Direction = ListSortDirection.Descending;
                //this.gridOpenLinesLetters.MasterTemplate.SortDescriptors.Add(descriptor);

                for (int i = 0; i < gridOpenLinesLetters.Columns.Count; i++)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (gridOpenLinesLetters.Columns[i].HeaderText != null && resxSet.GetString(gridOpenLinesLetters.Columns[i].HeaderText) != null)
                            gridOpenLinesLetters.Columns[i].HeaderText = resxSet.GetString(gridOpenLinesLetters.Columns[i].HeaderText);
                    }
                }
            }
        }


        private void radButton2_Click(object sender, EventArgs e)
        {
            if (Login.isOutlookInstalled == false)
            {
                translateRadMessageBox msgbox = new translateRadMessageBox();
                msgbox.translateAllMessageBox("Cannot find Outlook.");
                return;
            }
            //email
            List<string> persons = new List<string>();
            foreach(var p in openLinesLetters)
            {
                int isInList = persons.IndexOf(p.name);
                if(p.chk == true && isInList == -1)
                    persons.Add(p.name);
            }

            if (persons.Count > 0)
            {
                using (frmOpenLinesSendEmailForm frm = new frmOpenLinesSendEmailForm(persons))
                {
                    frm.ShowDialog();

                    if (frm.DialogResult == DialogResult.OK)
                    {
                        PrintOrEmailOpenLInes(true, "", frm.subject, frm.message);
                    }
                }
            }
            else
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("You must select at least one debitor.");
            }
        }

        private void frmOpenLines_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var tempFolder in _tempFolders)
            {
                if (Directory.Exists(tempFolder))
                    Directory.Delete(tempFolder, true);
            }
        }

        private void chkSelectAll_Validating(object sender, CancelEventArgs e)
        {
            
        }
        
        private void radioByEmail_Click(object sender, EventArgs e)
        {

        }

        private void radioByEmail_CheckStateChanging(object sender, CheckStateChangingEventArgs args)
        {
            if (openLinesLetters.Count <= 0)
            {
                translateRadMessageBox tmsg = new translateRadMessageBox();
                tmsg.translateAllMessageBox("Nothing to select");
                args.Cancel = true;
            }
        }

        private void radioByEmail_CheckStateChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            foreach (AccOpenLinesReportModel m in openLinesLetters)
            {                
                if (m.isInvoicing == true)
                    m.chk = true;  
                else
                    m.chk = false;  
            }
            
            gridOpenLinesLetters.DataSource = null;
            gridOpenLinesLetters.DataSource = openLinesLetters;

            btnSendByEmail.Visible = true;
            btnSendByPost.Visible = false;

            Cursor.Current = Cursors.Default;
        }

        private void radioByPost_CheckStateChanging(object sender, CheckStateChangingEventArgs args)
        {
            if (openLinesLetters.Count <= 0)
            {
                translateRadMessageBox tmsg = new translateRadMessageBox();
                tmsg.translateAllMessageBox("Nothing to select");
                args.Cancel = true;
            }
           
        }

        private void radioByPost_CheckStateChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            foreach (AccOpenLinesReportModel m in openLinesLetters)
            {
                if (m.isInvoicing == false)
                    m.chk = true;
                else
                    m.chk = false;
            }

            gridOpenLinesLetters.DataSource = null;
            gridOpenLinesLetters.DataSource = openLinesLetters;

            btnSendByEmail.Visible = false;
            btnSendByPost.Visible = true;
          
            Cursor.Current = Cursors.Default;
        }

        private void gridOpenLinesLetters_RowFormatting(object sender, RowFormattingEventArgs e)
        {           
            if (radioByEmail.IsChecked == true)
            {
                if ((bool)e.RowElement.RowInfo.Cells["isInvoicing"].Value == true)
                {
                    if (BookmarkFunctions.IsEmailValid((string)e.RowElement.RowInfo.Cells["email"].Value) == false)
                    {
                        e.RowElement.ForeColor = Color.Red;
                        e.RowElement.RowInfo.Cells["chk"].Value = false;
                    }
                    else
                    {
                        e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                        e.RowElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local);
                        e.RowElement.ResetValue(LightVisualElement.NumberOfColorsProperty, ValueResetFlags.Local);
                        e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
                    }
                }
                else
                {
                    e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                    e.RowElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local);
                    e.RowElement.ResetValue(LightVisualElement.NumberOfColorsProperty, ValueResetFlags.Local);
                    e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
                }
            }
            else
            {
                e.RowElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                e.RowElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local);
                e.RowElement.ResetValue(LightVisualElement.NumberOfColorsProperty, ValueResetFlags.Local);
                e.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
            }
        }

        private void radio_filter_all_Click(object sender, EventArgs e)
        {
            
        }

        private void radio_filter_debitor_Click(object sender, EventArgs e)
        {
            
        }

        private void radio_filter_creditor_Click(object sender, EventArgs e)
        {
            
        }

        private void radio_filter_all_CheckStateChanged(object sender, EventArgs e)
        {
            ShowDebitorsCreditorsInGrid();
        }

        private void radio_filter_debitor_CheckStateChanged(object sender, EventArgs e)
        {
            ShowDebitorsCreditorsInGrid();
        }

        private void radio_filter_creditor_CheckStateChanged(object sender, EventArgs e)
        {
            ShowDebitorsCreditorsInGrid();
        }

        private void printWithDays_Click(object sender, EventArgs e)
        {            

            if (chkClientFilter.Checked == false)
            {
                Cursor.Current = Cursors.WaitCursor;
                AccOpenLinesBUS bus = new AccOpenLinesBUS();
                int param = 0;
                if (radio_filter_all.IsChecked == true)
                    param = 0;
                else if (radio_filter_debitor.IsChecked == true)
                    param = 1;
                else if (radio_filter_creditor.IsChecked == true)
                    param = 2;
                dtOpenLiensForReport = bus.GetAccOpenLineReport_dt(dateTo.Value, param);
                Cursor.Current = Cursors.Default;
            }

            DataColumn dc = new DataColumn("DaySelected",typeof(DateTime));
            DataColumn dc1 = new DataColumn("DebCre", typeof(string));
            DataColumn dc2 = new DataColumn("UserName", typeof(string));

            DataTable dataTableCopy = dtOpenLiensForReport.Copy();
            dataTableCopy.Columns.Add(dc);
            dataTableCopy.Columns.Add(dc1);
            dataTableCopy.Columns.Add(dc2);
            string s = "";
            dataTableCopy.Rows[0]["DaySelected"] = DateTime.Now;
            if (radio_filter_all.IsChecked)
            {
                s= "All";
            }
            if (radio_filter_creditor.IsChecked)
            {
                s= "Creditor";
            }
            if (radio_filter_debitor.IsChecked)
            {
                s = "Debitor";
            }
            dataTableCopy.Rows[0]["DebCre"] = s;
            dataTableCopy.Rows[0]["UserName"] = Login._user.nameEmployee;
            dataTableCopy.Rows[0]["DaySelected"] = dateTo.Value;
            frmOpenLinesWithDays ol = new frmOpenLinesWithDays(dataTableCopy);
            ol.ShowDialog();
        }

        private void radMenuButtonSaveLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutOpenLines))
            {
                File.Delete(layoutOpenLines);
            }
            gridOpenLines.SaveLayout(layoutOpenLines);

            if (gridOpenLines.Columns["chk"] != null)
                gridOpenLines.Columns["chk"].IsVisible = false;

            translateRadMessageBox tr = new translateRadMessageBox();
            tr.translateAllMessageBox("Layout saved");
        }

        private void radMenuButtonSaveLayoutLetters_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutOpenLineLetters))
            {
                File.Delete(layoutOpenLineLetters);
            }
            gridOpenLinesLetters.SaveLayout(layoutOpenLineLetters);

            if (gridOpenLinesLetters.Columns["chk"] != null)
                gridOpenLinesLetters.Columns["chk"].IsVisible = true;

            translateRadMessageBox tr = new translateRadMessageBox();
            tr.translateAllMessageBox("Layout saved");
        }

        
    }
}
