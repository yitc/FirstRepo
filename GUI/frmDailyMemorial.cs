using BIS.Business;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.Linq;

namespace GUI
{
    public partial class frmDailyMemorial : RadForm
    {
        public string newOrEdit = "Edit";
        public AccDailyModel selectedDaily;
        public AccDailyMemModel selectedDailyMemo;
        public AccSettingsBUS asb;
        public AccSettingsModel asm;

        private BindingList<AccLineModel> dailyMemorialList;
        private List<AccLineModel> dailyOldMemorialList;
        private List<AccLineModel> deletedMemorialList;
        private List<AccLineModel> beginList;

        private string layoutDailyMemorial;
        private bool isSuccessfully = false;
        private bool mandatoryDebitor = false;
        private bool mandatoryCreditor = false;
        private bool mandatoryProject = false;
        private bool mandatoryCost = false;


        private bool statementSaved = false;
        public bool editorLookupKeyPressed = false;

        private string defaultAccCreditor = "";
        private string defaultAccDebitor = "";

        BaseGridEditor _gridEditor;

        public frmDailyMemorial(AccDailyModel selectedDaily, AccDailyMemModel selectedDailyMemo)
        {
            InitializeComponent();

            this.selectedDaily = selectedDaily;
            this.selectedDailyMemo = selectedDailyMemo;

            this.Text = Login._companyModelList[0].nameCompany + " " + Login._bookyear + " - " + "Daily Memorial";
            this.Icon = Login.iconForm;
        }

        public frmDailyMemorial(AccDailyModel selectedDaily, AccDailyMemModel selectedDailyMemo, List<AccLineModel> begin)
        {
            InitializeComponent();

            this.selectedDaily = selectedDaily;
            this.selectedDailyMemo = selectedDailyMemo;
            this.beginList = begin;

            this.Text = Login._companyModelList[0].nameCompany + " " + Login._bookyear + " - " + "Daily Memorial";
            this.Icon = Login.iconForm;
        }

        private void frmDailyMemorial_Load(object sender, EventArgs e)
        {
            layoutDailyMemorial = MainForm.gridFiltersFolder + "\\layoutAccDailyMemorial2.xml";

            this.gridDailyMemorial.CellEditorInitialized += gridDailyMemorial_CellEditorInitialized;

            txtRefNo.Text = selectedDailyMemo.refNo.ToString();
            radDateTimeDailyMemo.Value = (DateTime)selectedDailyMemo.dtMem;
            //if (radDateTimeDailyMemo.Value.Year.ToString() != Login._bookyear)
            //{
            //    translateRadMessageBox tr = new translateRadMessageBox();
            //    tr.translateAllMessageBox("Booking year NOT same as date !! Change Booking year, please!!");
            //    return;
            //}
            

            txtRefNo.ReadOnly = true;
            radDateTimeDailyMemo.ReadOnly = false;



            Translation();
            // layoutLines = MainForm.gridFiltersFolder + "\\layoutAccLines.xml";


            // Read parameters
            asb = new AccSettingsBUS();
            asm = new AccSettingsModel();
            asm = asb.GetSettingsByID(Login._bookyear);
            if (asm != null)
            {
                //if (asm.isVat == false)
                //{
                //    lblBtw.Visible = false;
                //    txtBtwCode.Visible = false;
                //    labelBtw.Visible = false;
                //    btnBtw.Visible = false;
                //}

                defaultAccCreditor = asm.defCreditorAccount;
                defaultAccDebitor = asm.defDebitorAccount;
            }
            else
            {
                RadMessageBox.Show("No settings !!! Enter settings, please! Program will be close!");
                this.Close();
            }


            dailyMemorialList = new BindingList<AccLineModel>();
            dailyOldMemorialList = new List<AccLineModel>();
            deletedMemorialList = new List<AccLineModel>();

            AccLineBUS aclineBUS = new AccLineBUS(Login._bookyear);
            List<AccLineModel> accLineModel = new List<AccLineModel>();
            accLineModel = aclineBUS.GetAllLinesByIdCurrency(selectedDailyMemo.idDailyMem, (int)selectedDaily.idDaily, 0);
            //==== ovde switchuje ako je pozvan za pocetno stanje ubacuje stavke
            if (beginList != null && beginList.Count > 0)
            {
              //  accLineModel.Clear();
                accLineModel = new List<AccLineModel>();
                accLineModel = beginList;

            }

            if (accLineModel != null)
            {
                //dailyMemorialList = new BindingList<AccLineModel>(accLineModel);

                dailyMemorialList.Clear();
                foreach (var model in accLineModel)
                {
                    dailyMemorialList.Add(model);

                }

                gridDailyMemorial.DataSource = dailyMemorialList;

                //if (asm.isVat == false)
                  //  gridDailyMemorial.Columns["idBtw"].IsVisible = false;
                
                foreach (var model in accLineModel)
                {
                    AccLineModel newmod = new AccLineModel();
                    newmod.bookingYear = model.bookingYear;
                    newmod.booksort = 1; // model.booksort; saki 28.7.
                    newmod.cond1 = model.cond1;
                    newmod.cond2 = model.cond2;
                    newmod.cond3 = model.cond3;
                    newmod.creditBTW = model.creditBTW;
                    newmod.creditCurr = model.creditCurr;
                    newmod.creditLine = model.creditLine;
                    newmod.currrate = model.currrate;
                    newmod.debitBTW = model.debitBTW;
                    newmod.debitCurr = model.debitCurr;
                    newmod.debitLine = model.debitLine;
                    newmod.descDaily = model.descDaily;
                    newmod.descLine = model.descLine;
                    newmod.dtBooking = model.dtBooking;
                    newmod.dtLine = model.dtLine;
                    newmod.iban = model.iban;
                    newmod.idAccDaily = model.idAccDaily;
                    newmod.idAccLine = model.idAccLine;
                    newmod.idBTW = model.idBTW;
                    newmod.idClientLine = model.idClientLine;
                    newmod.idCostLine = model.idCostLine;
                    newmod.idCurrency = model.idCurrency;
                    newmod.idDetail = model.idDetail;
                    newmod.idMaster = model.idMaster;
                    newmod.idPersonLine = model.idPersonLine;
                    newmod.idProjectLine = model.idProjectLine;
                    newmod.idSepa = model.idSepa;
                    newmod.incopNr = model.incopNr;
                    newmod.invoiceNr = model.invoiceNr;
                    newmod.numberLedAccount = model.numberLedAccount;
                    newmod.periodLine = model.periodLine;
                    newmod.statusLine = model.statusLine;
                    newmod.term = model.term;
                    newmod.userN = model.userN;
                    newmod.versil = model.versil;

                    dailyOldMemorialList.Add(newmod);
                }

                //if (dailyMemorialList[0].periodLine == 0)
                if (selectedDailyMemo.beginPeriod == true)
                {
                    chkBegin.CheckState = CheckState.Checked;
                    // chkBegin.Enabled = false;
                    lblBegin.Visible = true;
                }
                else
                {
                    chkBegin.CheckState = CheckState.Unchecked;
                    lblBegin.Visible = false;
                }

            }
            else
            {
                dailyMemorialList = new BindingList<AccLineModel>();
                gridDailyMemorial.DataSource = dailyMemorialList;
                
            }

            //if(dailyOldMemorialList.Count > 0)
            //{
            //    radDateTimeDailyMemo.Enabled = false;
            //    btnUpdateDates.Enabled = false;
            //}

            CalculateDebitCredit();

            //===============
            //GridViewDateTimeColumn column = (GridViewDateTimeColumn)this.gridDailyMemorial.Columns["dtLine"];
            //column.Format = DateTimePickerFormat.Short;
            //gridDailyMemorial.Focus();
        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            //if (File.Exists(layoutDailyMemorial))
            //{
            //    File.Delete(layoutDailyMemorial);
            //}
            //gridDailyMemorial.SaveLayout(layoutDailyMemorial);

            //RadMessageBox.Show("Layout Saved");
        }

        private void gridDailyMemorial_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < gridDailyMemorial.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (gridDailyMemorial.Columns[i].HeaderText != null && resxSet.GetString(gridDailyMemorial.Columns[i].HeaderText) != null)
                        gridDailyMemorial.Columns[i].HeaderText = resxSet.GetString(gridDailyMemorial.Columns[i].HeaderText);
                }
            }

            gridDailyMemorial.Columns["idAccLine"].IsVisible = false;
            gridDailyMemorial.Columns["idAccDaily"].IsVisible = false;
            gridDailyMemorial.Columns["statusLine"].IsVisible = false;
            gridDailyMemorial.Columns["periodLine"].IsVisible = false;
            gridDailyMemorial.Columns["idPersonLine"].IsVisible = false;
            gridDailyMemorial.Columns["debitBTW"].IsVisible = false;
            gridDailyMemorial.Columns["creditBTW"].IsVisible = false;
            gridDailyMemorial.Columns["idCurrency"].IsVisible = false;
            gridDailyMemorial.Columns["debitCurr"].IsVisible = false;
            gridDailyMemorial.Columns["creditCurr"].IsVisible = false;
            gridDailyMemorial.Columns["booksort"].IsVisible = false;
            gridDailyMemorial.Columns["currrate"].IsVisible = false;
            gridDailyMemorial.Columns["incopNr"].IsVisible = false;
            gridDailyMemorial.Columns["iban"].IsVisible = false;
            gridDailyMemorial.Columns["bookingYear"].IsVisible = false;
            gridDailyMemorial.Columns["dtBooking"].IsVisible = false;
            gridDailyMemorial.Columns["versil"].IsVisible = false;
            gridDailyMemorial.Columns["term"].IsVisible = false;

            gridDailyMemorial.Columns["idSepa"].IsVisible = false;
            gridDailyMemorial.Columns["descDaily"].IsVisible = false;
            gridDailyMemorial.Columns["cond1"].IsVisible = false;
            gridDailyMemorial.Columns["cond2"].IsVisible = false;
            gridDailyMemorial.Columns["cond3"].IsVisible = false;
            gridDailyMemorial.Columns["userN"].IsVisible = false;
            gridDailyMemorial.Columns["idMaster"].IsVisible = false;
            gridDailyMemorial.Columns["idDetail"].IsVisible = false;           
            gridDailyMemorial.Columns["idBtw"].IsVisible = false;
            gridDailyMemorial.Columns["descLedgerAccount"].IsVisible = false;
            gridDailyMemorial.Columns["dtline"].ReadOnly = true;

            gridDailyMemorial.Columns["userCreated"].IsVisible = false;
            gridDailyMemorial.Columns["dtCreated"].IsVisible = false;
            gridDailyMemorial.Columns["userModified"].IsVisible = false;
            gridDailyMemorial.Columns["dtModified"].IsVisible = false;

            gridDailyMemorial.Columns["numberLedAccount"].NullValue = "";
            gridDailyMemorial.Columns["invoiceNr"].NullValue = "";
            gridDailyMemorial.Columns["descLine"].NullValue = "";
            gridDailyMemorial.Columns["idClientLine"].NullValue = "";
            gridDailyMemorial.Columns["idPersonLine"].NullValue = "";
            gridDailyMemorial.Columns["idCostLine"].NullValue = "";
            gridDailyMemorial.Columns["idProjectLine"].NullValue = "";
            gridDailyMemorial.Columns["incopNr"].NullValue = "";
            gridDailyMemorial.Columns["debitLine"].NullValue = 0;
            gridDailyMemorial.Columns["creditLine"].NullValue = 0;


            //GridViewDateTimeColumn column = (GridViewDateTimeColumn)this.gridDailyMemorial.Columns["dtLine"];
            //column.Format = DateTimePickerFormat.Custom;
            //column.CustomFormat = "dd-MM-yyyy";

            //if (gridDailyMemorial.Columns != null && gridDailyMemorial.Columns.Count > 0)
              //  gridDailyMemorial.Columns["dtLine"].FormatString = "{0: dd-MM-yyyy}";

            if (gridDailyMemorial.Columns != null && gridDailyMemorial.Columns.Count > 0)
                gridDailyMemorial.Columns["debitLine"].FormatString = "{0:N2}";

            //gridDailyMemorial.Columns["debitLine"].InitializeEditor(_gridEditor);

            if (gridDailyMemorial.Columns != null && gridDailyMemorial.Columns.Count > 0)
                gridDailyMemorial.Columns["creditLine"].FormatString = "{0:N2}";

            if (gridDailyMemorial.Columns != null && gridDailyMemorial.Columns.Count > 0)
                gridDailyMemorial.Columns["idBtw"].FormatString = "{0:D}";

            //if (File.Exists(layoutDailyMemorial))
            //{
            //    gridDailyMemorial.LoadLayout(layoutDailyMemorial);
            //}
        }



        private void CalculateDebitCredit()
        {
            decimal credit = 0;
            decimal debit = 0;
            decimal diff = 0;

            txtAllCredit.Text = credit.ToString();
            txtAllDebit.Text = debit.ToString();
            txtDiff.Text = diff.ToString();

            if (dailyMemorialList != null)
            {
                if (dailyMemorialList.Count > 0)
                {
                    foreach (AccLineModel am in dailyMemorialList)
                    {
                        credit += am.creditLine;
                        debit += am.debitLine;

                    }
                    diff = debit - credit;
                    txtAllCredit.Text = credit.ToString();
                    txtAllDebit.Text = debit.ToString();
                    txtDiff.Text = diff.ToString();

                }
            }
        }
  
        private void gridDailyMemorial_UserAddedRow(object sender, GridViewRowEventArgs e)
        {                        
            if (e.Row != null && e.Row.DataBoundItem != null)
            {
                
                AccLineModel model = (AccLineModel)e.Row.DataBoundItem;
                AccAcountUpdate aU = new AccAcountUpdate();
                if (model != null)
                {
                    //int maxID = FindMaxValueInMemList(dailyMemorialList);
                    //tmpm.idAccLine = maxID + 1;

                    //MessageBox.Show("Changed: " + tmpm.invoiceNr);  
                    model.dtLine = radDateTimeDailyMemo.Value;
                    model.idCurrency = selectedDailyMemo.idDailyMem;
                    model.statusLine = false;
                    model.dtBooking = DateTime.Now;

                    model.booksort = 1;
                    //  DateTime mper = Convert.ToDateTime(tmpm.dtLine);
                    model.periodLine = aU.Period(model.dtLine);  //mper.Month;
                    if (chkBegin.Checked == true)
                        model.periodLine = 0;
                    model.idAccDaily = selectedDaily.idDaily;

                    model.idPersonLine = "";

                    if (model.idBTW == null)
                        model.idBTW = 0;
                    if (model.debitBTW == null)
                        model.debitBTW = 0;
                    if (model.creditBTW == null)
                        model.creditBTW = 0;
                    if (model.debitCurr == null)
                        model.debitCurr = 0;
                    if (model.creditCurr == null)
                        model.creditCurr = 0;
                    if (model.currrate == null)
                        model.currrate = 0;

                    model.incopNr = getIncopNr(Convert.ToInt32(selectedDailyMemo.codeDaily));
                    labelName.Text = "";
                }

                e.Row.InvalidateRow();
            }

            CalculateDebitCredit();
        }
            
        private void radButtonSave_Click(object sender, EventArgs e)
        {
            gridDailyMemorial.EndEdit();

            if (radDateTimeDailyMemo.Value.Year.ToString() != Login._bookyear)  // kontrola da ne dozvoli knjizenje jedne godine u drugu
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Booking year NOT same as date !!");
                return;
            }

            try
            {
                if (dailyMemorialList.Count <= 0 && dailyOldMemorialList.Count <= 0)
                {
                    translateRadMessageBox msg = new translateRadMessageBox();
                    msg.translateAllMessageBox("Nothing to save.");
                    return;
                }

                var hashset = new HashSet<string>();
                foreach (var vdate in dailyMemorialList)
                {
                    string date = vdate.dtLine.ToString("dd-MM-yyyy");
                    hashset.Add(date);
                }
                if (hashset.Count > 1)
                {
                    translateRadMessageBox msg = new translateRadMessageBox();
                    msg.translateAllMessageBox("Dates must be same.");
                    return;
                }

                int iID = 0;
                DialogResult dr = RadMessageBox.Show("Do you want to save values ?", "Save", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    decimal totalDebit = 0;
                    decimal totalCredit = 0;
                    decimal difference = 0;

                    foreach (AccLineModel m in dailyMemorialList)
                    {
                        totalDebit += m.debitLine;
                        totalCredit += m.creditLine;
                    }
                    foreach (AccLineModel s in dailyMemorialList)
                    {
                        if (s.numberLedAccount == null || s.numberLedAccount.Trim() == "")
                        {
                            RadMessageBox.Show("Can't save without account !!!");
                            return;
                        }
                    }


                    difference = totalDebit - totalCredit;

                    if (difference == 0)
                    {

                        DeleteRemovedGridRows(deletedMemorialList);

                        // iz liste sa grida
                        AccAcountUpdate aaU1 = new AccAcountUpdate();
                        if (dailyOldMemorialList.Count > 0)
                        {
                            for (int js = 0; js < dailyOldMemorialList.Count; js++)
                            {
                                isSuccessfully = aaU1.SubstractAmount(dailyOldMemorialList[js], this.Name, Login._user.idUser);
                                iID = 0;

                            }
                        }



                        AccLineBUS acBUS = new AccLineBUS(Login._bookyear);
                        acBUS.DeleteByCurrencyID(selectedDailyMemo.idDailyMem, selectedDaily.idDaily, 0, this.Name, Login._user.idUser);
                        AccAcountUpdate aaU1m = new AccAcountUpdate();
                        foreach (AccLineModel m in dailyMemorialList)
                        {
                            if (chkBegin.Checked == true)
                            {
                                m.periodLine = 0;
                                chkBegin.Enabled = false;
                            }
                            else
                            {
                                // AccAcountUpdate aaU1c = new AccAcountUpdate();
                                m.periodLine = aaU1m.Period(m.dtLine);
                            }
                            m.booksort = 1;
                            if (chkBegin.CheckState == CheckState.Checked)  // 29.7 ako je pocetno stanje da ubaci godinu iz gornjeg datuma
                                 m.bookingYear = radDateTimeDailyMemo.Value.Year.ToString();


                            acBUS.Save(m, this.Name, Login._user.idUser);
                            isSuccessfully = aaU1m.AddAmount(m, this.Name, Login._user.idUser);

                        }

                        //=============================================================================  rasknjizava otvorene stavke
                        if (chkBegin.CheckState == CheckState.Unchecked)  //== radi samo kad nije pocetno stanje
                        {
                            if (dailyOldMemorialList.Count > 0)
                            {

                                AccOpenLinesBUS olbus = new AccOpenLinesBUS();
                                AccOpenLinesModel olmod = new AccOpenLinesModel();


                                foreach (AccLineModel itmol in dailyOldMemorialList)
                                {
                                    //if (itmol.invoiceNr == "" && itmol.idClientLine == "")
                                    //{
                                    //    return;

                                    //}
                                    if (itmol.invoiceNr.Trim() != "" && itmol.idClientLine.Trim() != "")
                                    {
                                        olmod = olbus.GetAccOpenLinesByInvoice(itmol.invoiceNr, itmol.term);
                                        if (olmod != null)
                                        {
                                            if (olmod.invoiceOpenLine.Trim() != "")
                                            {
                                                if (olmod.invoiceOpenLine == itmol.invoiceNr && olmod.term == itmol.term)
                                                {

                                                    olmod.iselected = false;
                                                    olmod.dtPayOpenLine = DateTime.Now;

                                                    if (olmod.referencePay.Trim() == itmol.incopNr.Trim()) // edituje istu stavku od koje je napravljena otvorena stavka
                                                    {                                                          // onda samo upisuje novi iznos;
                                                        if (itmol.debitLine > 0)
                                                            olmod.debitOpenLine = itmol.debitLine;
                                                        if (itmol.creditLine > 0)
                                                            olmod.creditOpenLine = itmol.creditLine;
                                                        olmod.referencePay = itmol.incopNr;
                                                    }
                                                    else
                                                    {
                                                        if (olmod.debitOpenLine > 0)
                                                            olmod.debitOpenLine = olmod.debitOpenLine - itmol.debitLine;
                                                        if (olmod.creditOpenLine > 0)
                                                            olmod.creditOpenLine = olmod.creditOpenLine - itmol.creditLine;
                                                    }
                                                    olmod.descOpenLine = itmol.descLine;
                                                    olbus.Update(olmod, this.Name, Login._user.idUser);  // upisujemo open line stavku
                                                }
                                                // }
                                            }
                                        }
                                    }

                                }

                            }
                            // }
                            //}

                            //  }
                            //===================== ovde ide knjizenje otvorenih stavki ========================
                            if (chkBegin.CheckState == CheckState.Unchecked)  //== radi samo kad nije pocetno stanje
                            {

                                if (dailyMemorialList.Count > 0)
                                {
                                    AccOpenLinesBUS olbus = new AccOpenLinesBUS();
                                    AccOpenLinesModel olmod = new AccOpenLinesModel();
                                    foreach (AccLineModel itmol in dailyMemorialList)
                                    {
                                        if (itmol.invoiceNr.Trim() != "" && itmol.idClientLine.Trim() != "")
                                        {
                                            olmod = olbus.GetAccOpenLinesByInvoice(itmol.invoiceNr, itmol.term);
                                            if (olmod != null)
                                            {

                                                if (olmod.invoiceOpenLine != null && olmod.invoiceOpenLine.Trim() != "")
                                                {
                                                    if (olmod.invoiceOpenLine == itmol.invoiceNr && olmod.term == itmol.term)
                                                    {

                                                        olmod.iselected = false;
                                                        olmod.dtPayOpenLine = DateTime.Now;

                                                        if (olmod.referencePay == itmol.incopNr) // edituje istu stavku od koje je napravljena otvorena stavka
                                                        {                                                          // onda samo upisuje novi iznos;
                                                            if (itmol.debitLine > 0)
                                                                olmod.debitOpenLine = itmol.debitLine;
                                                            if (itmol.creditLine > 0)
                                                                olmod.creditOpenLine = itmol.creditLine;
                                                            olmod.referencePay = itmol.incopNr;
                                                        }
                                                        else
                                                        {
                                                            if (iID != -1)
                                                            {
                                                                if (olmod.debitOpenLine > 0)
                                                                    olmod.debitOpenLine = olmod.debitOpenLine - itmol.debitLine;
                                                                if (olmod.creditOpenLine > 0)
                                                                    olmod.creditOpenLine = olmod.creditOpenLine - itmol.creditLine;

                                                                // olmod.referencePay = itmol.incopNr;
                                                            }
                                                            else
                                                            {
                                                                //   olmod.referencePay = itmol.incopNr;
                                                                olmod.debitOpenLine = olmod.debitOpenLine + itmol.debitLine;                     //itmol.debitLine;

                                                                olmod.creditOpenLine = olmod.creditOpenLine + itmol.creditLine;                   // itmol.creditLine;
                                                                // olmod.debitOpenLine = olmod.debitOpenLine + itmol.creditLine; 
                                                                //}
                                                            }
                                                        }

                                                        olmod.descOpenLine = itmol.descLine;
                                                        olbus.Update(olmod, this.Name, Login._user.idUser);  // upisujemo open line stavku
                                                    }
                                                    else
                                                    {
                                                        if (olmod.invoiceOpenLine != null && olmod.invoiceOpenLine.Trim() != "")
                                                        {
                                                            olmod.descOpenLine = itmol.descLine;
                                                            olmod.dtOpenLine = Convert.ToDateTime(itmol.dtBooking);
                                                            olmod.account = itmol.numberLedAccount;
                                                            olmod.invoiceOpenLine = itmol.invoiceNr;
                                                            olmod.dtCreationLine = Convert.ToDateTime("1900-01-01");//DateTime.Now;
                                                            olmod.idDebCre = itmol.idClientLine;
                                                            olmod.idOption = 0;
                                                            olmod.idProject = itmol.idProjectLine;
                                                            olmod.codeCost = itmol.idCostLine;
                                                            olmod.codeArr = itmol.idProjectLine;
                                                            olmod.referencePay = itmol.incopNr;
                                                            if (olmod.debitOpenLine > 0)
                                                                olmod.typeOpenLine = "D";
                                                            else
                                                                olmod.typeOpenLine = "C";


                                                            olbus.Save(olmod, this.Name, Login._user.idUser);
                                                        }
                                                    }
                                                    // }

                                                }
                                                else
                                                {
                                                    if (itmol.numberLedAccount.Trim() == defaultAccCreditor.Trim() || itmol.numberLedAccount.Trim() == defaultAccDebitor.Trim())
                                                    {
                                                        olmod.descOpenLine = itmol.descLine;
                                                        olmod.debitOpenLine = itmol.debitLine;
                                                        olmod.creditOpenLine = itmol.creditLine;
                                                        olmod.dtOpenLine = Convert.ToDateTime(itmol.dtBooking);
                                                        olmod.account = itmol.numberLedAccount;
                                                        olmod.invoiceOpenLine = itmol.invoiceNr;
                                                        olmod.dtCreationLine = DateTime.Now;
                                                        olmod.idDebCre = itmol.idClientLine;
                                                        olmod.idOption = 0;
                                                        olmod.idProject = itmol.idProjectLine;
                                                        olmod.codeCost = itmol.idCostLine;
                                                        olmod.codeArr = itmol.idProjectLine;
                                                        if (olmod.debitOpenLine > 0)
                                                            olmod.typeOpenLine = "D";
                                                        else
                                                            olmod.typeOpenLine = "C";

                                                        olbus.Save(olmod, this.Name, Login._user.idUser);
                                                    }
                                                }

                                            }

                                        }

                                    }

                                }
                            }
                            //==================================================================================
                        }

                        AccLineBUS aclineBUS = new AccLineBUS(Login._bookyear);
                        List<AccLineModel> accLineModel = new List<AccLineModel>();
                        accLineModel = aclineBUS.GetAllLinesByIdCurrency(selectedDailyMemo.idDailyMem, (int)selectedDaily.idDaily, 0);
                        if (accLineModel != null)
                        {
                            dailyMemorialList.Clear();
                            foreach (var model in accLineModel)
                            {
                                dailyMemorialList.Add(model);
                            }

                            dailyOldMemorialList.Clear();
                            foreach (var model in accLineModel)
                            {
                                AccLineModel newmod = new AccLineModel();
                                newmod.bookingYear = model.bookingYear;
                                newmod.booksort = 1; // model.booksort; saki izmenio 28.7
                                newmod.cond1 = model.cond1;
                                newmod.cond2 = model.cond2;
                                newmod.cond3 = model.cond3;
                                newmod.creditBTW = model.creditBTW;
                                newmod.creditCurr = model.creditCurr;
                                newmod.creditLine = model.creditLine;
                                newmod.currrate = model.currrate;
                                newmod.debitBTW = model.debitBTW;
                                newmod.debitCurr = model.debitCurr;
                                newmod.debitLine = model.debitLine;
                                newmod.descDaily = model.descDaily;
                                newmod.descLine = model.descLine;
                                newmod.dtBooking = model.dtBooking;
                                newmod.dtLine = model.dtLine;
                                newmod.iban = model.iban;
                                newmod.idAccDaily = model.idAccDaily;
                                newmod.idAccLine = model.idAccLine;
                                newmod.idBTW = model.idBTW;
                                newmod.idClientLine = model.idClientLine;
                                newmod.idCostLine = model.idCostLine;
                                newmod.idCurrency = model.idCurrency;
                                newmod.idDetail = model.idDetail;
                                newmod.idMaster = model.idMaster;
                                newmod.idPersonLine = model.idPersonLine;
                                newmod.idProjectLine = model.idProjectLine;
                                newmod.idSepa = model.idSepa;
                                newmod.incopNr = model.incopNr;
                                newmod.invoiceNr = model.invoiceNr;
                                newmod.numberLedAccount = model.numberLedAccount;
                                newmod.periodLine = model.periodLine;
                                newmod.statusLine = model.statusLine;
                                newmod.term = model.term;
                                newmod.userN = model.userN;
                                newmod.versil = model.versil;

                                dailyOldMemorialList.Add(newmod);
                            }
                        }
                        RadMessageBox.Show("Saved");

                    }
                    else
                    {
                        RadMessageBox.Show("No conditions for save.");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (selectedDailyMemo != null)
            {
                bool isOk = false;
                selectedDailyMemo.userModified = Login._user.idUser;
                AccDailyMemBUS dmb = new AccDailyMemBUS(Login._bookyear);
                isOk = dmb.Update(selectedDailyMemo, this.Name, Login._user.idUser);
                if (isOk == false)
                    RadMessageBox.Show("Error updating modified informations");
            }

        }

        
        private void gridDailyMemorial_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            //Console.WriteLine(e.Column.Name);
          
        }

        private void gridDailyMemorial_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            
            
            if(e.Row != null && e.Row.DataBoundItem != null)
            {
                AccLineModel model = (AccLineModel)e.Row.DataBoundItem;
                if (model != null)
                {
                    if (model.numberLedAccount == null) model.numberLedAccount = String.Empty;
                    if (model.invoiceNr == null) model.invoiceNr = String.Empty;
                    if (model.descLine == null) model.descLine = String.Empty;
                    if (model.idClientLine == null) model.idClientLine = String.Empty;
                    if (model.idPersonLine == null) model.idPersonLine = String.Empty;
                    if (model.idCostLine == null) model.idCostLine = String.Empty;
                    if (model.idProjectLine == null) model.idProjectLine = String.Empty;
                    if (model.incopNr == null) model.incopNr = String.Empty;

                   
                }
            }
                                 
            if (e.Column.Name == "debitLine")
            {
                if (e.Value != null)
                {
                    decimal d = (decimal)e.Value;
                    if (d != 0)
                    {
                        GridViewRowInfo info = e.Row.ViewInfo.CurrentRow;
                        info.Cells["creditLine"].Value = "0,00";

                    }
                }
            }

            if (e.Column.Name == "creditLine")
            {
                if (e.Value != null)
                {
                    decimal d = (decimal)e.Value;
                    if (d != 0)
                    {
                        GridViewRowInfo info = e.Row.ViewInfo.CurrentRow;
                        info.Cells["debitLine"].Value = "0,00";

                    }
                }
            }

            DisplayBottomDetails(e.Row);
        }

        private void gridDailyMemorial_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            if (e.PropertyName != null)
            {
                if (e.PropertyName != "")
                {
                    if (e.PropertyName == "debitLine" || e.PropertyName == "creditLine")
                        CalculateDebitCredit();
                }
            }
        }

        private void gridDailyMemorial_UserDeletedRow(object sender, GridViewRowEventArgs e)
        {
            CalculateDebitCredit();

        }

        private void gridDailyMemorial_RowsChanged_1(object sender, GridViewCollectionChangedEventArgs e)
        {
            CalculateDebitCredit();

        }

        private void debitor_Click(object sender, EventArgs e)
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
                string gClient = pm1X.accNumber;
                labelName.Text = pm1X.name;
                gridDailyMemorial.CurrentRow.Cells["idClientLine"].Value = gClient;

                if (gridDailyMemorial.ActiveEditor is RadTextBoxEditor)
                {
                    RadTextBoxEditor ted = gridDailyMemorial.ActiveEditor as RadTextBoxEditor;
                    RadTextBoxEditorElement editor = ted.EditorElement as RadTextBoxEditorElement;
                    editor.Text = gClient;
                }
            }
        }

        private void creditor_Click(object sender, EventArgs e)
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
                string gClient = pm1X.accNumber;
                labelName.Text = pm1X.name;
                gridDailyMemorial.CurrentRow.Cells["idClientLine"].Value = gClient;

                if (gridDailyMemorial.ActiveEditor is RadTextBoxEditor)
                {
                    RadTextBoxEditor ted = gridDailyMemorial.ActiveEditor as RadTextBoxEditor;
                    RadTextBoxEditorElement editor = ted.EditorElement as RadTextBoxEditorElement;
                    editor.Text = gClient;
                }
            }

        }
        private void Translation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblDate.Text) != null)
                    lblDate.Text = resxSet.GetString(lblDate.Text);

                if (resxSet.GetString(radLabelMemo.Text) != null)
                    radLabelMemo.Text = resxSet.GetString(radLabelMemo.Text);

                if (resxSet.GetString(radLabelDebit.Text) != null)
                    radLabelDebit.Text = resxSet.GetString(radLabelDebit.Text);

                if (resxSet.GetString(radLabelCredit.Text) != null)
                    radLabelCredit.Text = resxSet.GetString(radLabelCredit.Text);

                if (resxSet.GetString(lblDifference.Text) != null)
                    lblDifference.Text = resxSet.GetString(lblDifference.Text);

                if (resxSet.GetString(radButtonSave.Text) != null)
                    radButtonSave.Text = resxSet.GetString(radButtonSave.Text);

                if (resxSet.GetString(lblBegin.Text) != null)
                    lblBegin.Text = resxSet.GetString(lblBegin.Text);

                if (resxSet.GetString(btnUpdateDates.Text) != null)
                    btnUpdateDates.Text = resxSet.GetString(btnUpdateDates.Text);

                if (resxSet.GetString(btnExit.Text) != null)
                    btnExit.Text = resxSet.GetString(btnExit.Text);



            }
        }

        private void DeleteRemovedGridRows(List<AccLineModel> lista)
        {
            if (lista != null && lista.Count > 0)
            {
                foreach (var model in lista)
                {
                    AccAcountUpdate asl = new AccAcountUpdate();
                    var itemdel = dailyOldMemorialList.Find(item => item.idAccLine == model.idAccLine);
                    asl.SubstractAmount(itemdel, this.Name, Login._user.idUser);

                    AccLineBUS aclineBUS = new AccLineBUS(Login._bookyear);
                    aclineBUS.Delete(itemdel.idAccLine, this.Name, Login._user.idUser);

                    //gridDailyMemorial.CurrentRow.Delete();
                    //accLineModel = aclineBUS.GetAllLinesByIdCurrency(selectedDailyMemo.idDailyMem, (int)selectedDaily.idDaily, 0);
                    //gridDailyMemorial.DataSource = null;
                    //gridDailyMemorial.DataSource = dailyMemorialList;
                }

                deletedMemorialList.Clear();
            }
        }
        private void MasterTemplate_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {
            DialogResult dr = RadMessageBox.Show("Do you want to DELETE this line ?", "Delete", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                if (gridDailyMemorial.CurrentRow.DataBoundItem != null)
                {
                    AccLineBUS aclineBUS = new AccLineBUS(Login._bookyear);
                    //List<AccLineModel> accLineModel = new List<AccLineModel>();

                    AccLineModel selectedLine = new AccLineModel();
                    selectedLine = (AccLineModel)gridDailyMemorial.CurrentRow.DataBoundItem;                 

                    //var itemdel = dailyMemorialList.FirstOrDefault(item => item.idAccLine == selectedLine.idAccLine);
                    //dailyMemorialList.Remove(itemdel);

                    // ako je upisano u bazi dodaj ga za kasnije brisanje
                    // ako je smao u gridu a nije u bazi onda nista
                    if (selectedLine.idAccLine > 0)
                        deletedMemorialList.Add(selectedLine);

                    this.gridDailyMemorial.CurrentRow.Delete();
                    
                }
            }
            else
            {
                e.Cancel = true;
                return;
            }
            gridDailyMemorial.Show();
        }

        private void gridDailyMemorial_UserDeletedRow_1(object sender, GridViewRowEventArgs e)
        {
            // gridDailyMemorial.CurrentRow.Delete();

        }

        private void gridDailyMemorial_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {

            //string saveLayout = "Save Layout";
            //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            //{

            //    if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
            //        saveLayout = resxSet.GetString(saveLayout);
            //}
            //RadMenuItem customMenuItem = new RadMenuItem();
            //customMenuItem.Text = saveLayout;
            //customMenuItem.Click += SaveLayout;
            //RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            //e.ContextMenu.Items.Add(separator);
            //e.ContextMenu.Items.Add(customMenuItem);

        }
        private void SaveLayout(object sender, EventArgs e)
        {
            //if (File.Exists(layoutDailyMemorial))
            //{
            //    File.Delete(layoutDailyMemorial);
            //}
            //gridDailyMemorial.SaveLayout(layoutDailyMemorial);
            //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            //{
            //    if (resxSet.GetString("You have successfully save layout!") != null)
            //        RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
            //    else
            //        RadMessageBox.Show("You have successfully save layout!");
            //}
        }

        private void frmDailyMemorial_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {
                AccLineComparer comparer = new AccLineComparer();
                IEnumerable<AccLineModel> difference = dailyMemorialList.Except(dailyOldMemorialList, comparer);

                bool resultCompare = Utils.IsAny(difference);

                if (resultCompare == true || dailyMemorialList.Count != dailyOldMemorialList.Count)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    DialogResult dr = tr.translateAllMessageBoxDialog("There is changes on form. Continue ?", "Warrning");
                    if (dr == System.Windows.Forms.DialogResult.No || dr == System.Windows.Forms.DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            try
            {
                AccLineBUS aclineBUS = new AccLineBUS(Login._bookyear);
                List<AccLineModel> accLineModel = new List<AccLineModel>();
                AccDailyMemBUS bus = new AccDailyMemBUS(Login._bookyear);

                accLineModel = aclineBUS.GetAllLinesByIdCurrency(selectedDailyMemo.idDailyMem, (int)selectedDaily.idDaily, 0);
                if (accLineModel == null)
                {
                    bus.Delete2(selectedDailyMemo.idDailyMem, selectedDailyMemo.refNo, this.Name, Login._user.idUser);
                }
                else
                {
                    if (accLineModel.Count <= 0)
                    {
                        bus.Delete2(selectedDailyMemo.idDailyMem, selectedDailyMemo.refNo, this.Name, Login._user.idUser);
                    }
                }
            }
            catch(Exception ex)
            {

            }
            
        }

        private void gridDailyMemorial_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (gridDailyMemorial.Columns["debitLine"].IsCurrent || gridDailyMemorial.Columns["creditLine"].IsCurrent || gridDailyMemorial.Columns["idBTW"].IsCurrent)
            {
                GridSpinEditor spinEditor = this.gridDailyMemorial.ActiveEditor as GridSpinEditor;
                ((RadSpinEditorElement)spinEditor.EditorElement).ShowUpDownButtons = false;
            }
            //if (this.gridDailyMemorial.ActiveEditor is RadDateTimeEditor)
            //{
            //    RadDateTimeEditor editor = this.gridDailyMemorial.ActiveEditor as RadDateTimeEditor;
            //    if (editor != null)
            //    {
                    
            //        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridDailyMemorial.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
            //        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridDailyMemorial.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";
            //        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridDailyMemorial.ActiveEditor).EditorElement).ShowTimePicker = false;
            //        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridDailyMemorial.ActiveEditor).EditorElement).Value = radDateTimeDailyMemo.Value;
            //        //editor.Value = radDateTimeDailyMemo.Value;
            //    }
            //}

            if (e.Column.Name == "descLine")
            {
                if (e.Column is GridViewTextBoxColumn)
                {
                    ((RadTextBoxEditor)this.gridDailyMemorial.ActiveEditor).MaxLength = 60;
                }
            }

            if (e.Column.Name == "invoiceNr")
            {
                if (e.Column is GridViewTextBoxColumn)
                {
                    ((RadTextBoxEditor)this.gridDailyMemorial.ActiveEditor).MaxLength = 20;
                }
            }

            if (e.Column.Name == "numberLedAccount")
            {
                if (e.Column is GridViewTextBoxColumn)
                {
                    ((RadTextBoxEditor)this.gridDailyMemorial.ActiveEditor).MaxLength = 6;
                }
            }

            if (e.Column.Name == "idProjectLine")
            {
                if (e.Column is GridViewTextBoxColumn)
                {
                    ((RadTextBoxEditor)this.gridDailyMemorial.ActiveEditor).MaxLength = 28;
                }
            }

            if (e.Column.Name == "idCostLine")
            {
                if (e.Column is GridViewTextBoxColumn)
                {
                    ((RadTextBoxEditor)this.gridDailyMemorial.ActiveEditor).MaxLength = 6;
                }
            }
            if (e.Column.Name == "idClientLine")
            {
                if (e.Column is GridViewTextBoxColumn)
                {
                    ((RadTextBoxEditor)this.gridDailyMemorial.ActiveEditor).MaxLength = 6;
                }
            }

            //Restriction  mouse wheel and KayUp, KeyDown for grid when is in Edit mode Gorance 25 08
            var editor = e.ActiveEditor as GridSpinEditor;
            if (editor != null)
            {
                var element = editor.EditorElement as GridSpinEditorElement;
                element.InterceptArrowKeys = false;
                element.EnableMouseWheel = false;
            }

        }

        private void chkBegin_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
           // lledit = true;
        }
        private string getIncopNr(int xDaily)
        {
            AccLineBUS gn = new AccLineBUS(Login._bookyear);
            IdModel nid = new IdModel();
            int idDaily = 0;
            string x_incoNr = "";
            string yearId = Login._bookyear; //DateTime.Now.Year.ToString();
            if (xDaily != -1)
                idDaily = xDaily;
            nid = gn.GetIncop(yearId, idDaily, this.Name, Login._user.idUser);
            if (nid != null)
            {
                var result = nid.idNumber.ToString().PadLeft(6, '0');
                // DateTime YearDate = DateTime.Now;
                // string year2 = YearDate.ToString("yy");
                var aa = nid.idDaily.ToString().PadRight(6, '0');
                string SubString = nid.yearId.Substring(yearId.Length - 2);

                x_incoNr = SubString + aa + result;
            }
            return x_incoNr;
        }

        private void gridDailyMemorial_CellValidating(object sender, CellValidatingEventArgs e)
        {
            if (e.Row != null)
            {
                switch (e.Column.Name)
                {
                    
                    case "numberLedAccount":

                        e.Row.ErrorText = string.Empty;
                        RadTextBoxEditor tbEditor = e.ActiveEditor as RadTextBoxEditor;
                        //if (tbEditor != null && e.Column.Name == "numberLedAccount" && tbEditor.Value + "" == string.Empty && e.Row.Tag == null)
                        //{
                        //    e.Row.ErrorText = "Empty value is not allowed!";
                        //    e.Cancel = true;

                        //    GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                        //    endEdit(cell);
                        //}
                        //else
                        //{
                            
                        //    //if (msg == false && e.Column.Name == "numberLedAccount")
                        //    //{
                        //    //    e.Row.ErrorText = "Non extisting account";
                        //    //    e.Cancel = true;

                        //    //    if (_gridEditor != null)
                        //    //    {
                        //    //        RadItem element = _gridEditor.EditorElement as RadItem;
                        //    //        element.KeyDown -= cellEditorAccount_KeyDown;
                        //    //    }

                        //    //    _gridEditor = null;
                        //    //    GridViewCellInfo cell = gridItems.Rows[e.Row.Index].Cells[e.Column.Index];

                        //    //    endEdit(cell);
                        //    //}
                        //}

                        if (tbEditor != null)
                        {
                            if (tbEditor.Value.ToString().Trim() != "")
                            {
                                LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                                LedgerAccountModel lam = new LedgerAccountModel();

                                lam = ledbus.GetAccount(tbEditor.Value.ToString(), Login._bookyear);
                                if (lam == null)
                                {
                                    RadMessageBox.Show("Wrong account");

                                    e.Row.ErrorText = "Non extisting account";
                                    e.Cancel = true;

                                    GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                                   // endEdit(cell);
                                }
                                else
                                {
                                    mandatoryDebitor = lam.mandatoryDebitorAccount;
                                    mandatoryCreditor = lam.mandatoryCreditorAccount;
                                    mandatoryProject = lam.mandatoryProjectAccount;
                                    mandatoryCost = lam.mandatoryCostAccount;

                                }
                            }
                            //else
                            //{
                            //    msg = false;
                            //}
                        }

                        break;

                    case "invoiceNr":


                        //e.Row.ErrorText = string.Empty;
                        //RadTextBoxEditor tbEditor1 = e.ActiveEditor as RadTextBoxEditor;
                        //if (tbEditor1 != null && e.Column.Name == "invoiceNr" && tbEditor1.Value + "" == string.Empty && e.Row.Tag == null)
                        //{
                        //    e.Row.ErrorText = "Empty value is not allowed!";
                        //    e.Cancel = true;

                        //    GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                        //    endEdit(cell);
                        //    break;
                        //}
                        //else
                        //{
                        //    if (tbEditor1 != null)
                        //    {
                        //        String s = ((Object)tbEditor1.Value ?? "").ToString();
                        //        if (txtInvoiceNr2.Text != s)   //tbEditor1.Value.ToString())
                        //        {
                        //            e.Row.ErrorText = "Not the same invoice number";
                        //            e.Cancel = true;


                        //            GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                        //            endEdit(cell);
                        //            break;
                        //        }
                        //    }
                        //}

                        break;

                    case "idClientLine":
                        e.Row.ErrorText = string.Empty;
                        RadTextBoxEditor tbEditor2 = e.ActiveEditor as RadTextBoxEditor;
                        if (tbEditor2 != null && e.Column.Name == "idClientLine" && tbEditor2.Value + "" == string.Empty && (mandatoryCreditor || mandatoryDebitor))
                        {
                            e.Row.ErrorText = "Empty value is not allowed!";
                            e.Cancel = true;

                            GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                            //endEdit(cell);
                        }
                        else
                        {
                            if (tbEditor2 != null && tbEditor2.Value.ToString() != "" && e.Column.Name == "idClientLine")
                            {
                                AccDebCreBUS dcb = new AccDebCreBUS();
                                AccDebCreModel dcm = new AccDebCreModel();
                                dcm = dcb.GetCustomerByAccCode(tbEditor2.Value.ToString());
                                if (dcm == null)
                                {
                                    e.Row.ErrorText = "Not valid Client number";
                                    e.Cancel = true;
                                }

                                GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                               // endEdit(cell);
                            }
                        }
                        break;

                    case "idCostLine":

                        e.Row.ErrorText = string.Empty;
                        RadTextBoxEditor tbEditor3 = e.ActiveEditor as RadTextBoxEditor;
                        if (tbEditor3 != null && e.Column.Name == "idCostLine" && tbEditor3.Value + "" == string.Empty && mandatoryCost)
                        {
                            e.Row.ErrorText = "Empty value is not allowed!";
                            e.Cancel = true;

                            GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                            //endEdit(cell);
                        }
                        else
                        {
                            if (tbEditor3 != null && tbEditor3.Value.ToString() != "" && e.Column.Name == "idCostLine")
                            {
                                AccCostBUS dcb = new AccCostBUS();
                                AccCostModel dcm = new AccCostModel();
                                dcm = dcb.GetCostByID(tbEditor3.Value.ToString());
                                if (dcm == null)
                                {
                                    e.Row.ErrorText = "Not valid cost number";
                                    e.Cancel = true;
                                }

                                GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                               // endEdit(cell);
                            }
                        }

                        break;

                    case "idProjectLine":
                        e.Row.ErrorText = string.Empty;
                        RadTextBoxEditor tbEditor4 = e.ActiveEditor as RadTextBoxEditor;

                        if (tbEditor4 != null && tbEditor4.Value.ToString() != "" && e.Column.Name == "idProjectLine")
                        {
                            ArrangementBUS ab = new ArrangementBUS();
                            ArrangementModel am = new ArrangementModel();

                            am = ab.GetArrangementCodeProject(tbEditor4.Value.ToString());
                            if (am == null)
                            {

                                e.Row.ErrorText = "Not valid project code !";
                                e.Cancel = true;
                                GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                                //endEdit(cell);
                            }
                        }
                        break;


                    //case "idBTW":
                    //    e.Row.ErrorText = string.Empty;
                    //    RadTextBoxEditor tbEditor3 = e.ActiveEditor as RadTextBoxEditor;

                    //    if (tbEditor3 != null)
                    //    {
                    //        if (tbEditor3.Value.ToString() != "" && tbEditor3.Value.ToString() != "0")
                    //        {
                    //            AccTaxBUS ledbus = new AccTaxBUS();
                    //            AccTaxModel lam = new AccTaxModel();

                    //            lam = ledbus.GetTaxByID(Convert.ToInt32(tbEditor3.Value.ToString()));
                    //            if (lam == null)
                    //            {
                    //                RadMessageBox.Show("Wrong BTW ");

                    //                e.Row.ErrorText = "Non extisting BTW";
                    //                e.Cancel = true;

                    //                GridViewCellInfo cell = e.Row.Cells[e.Column.Index];

                    //                endEdit(cell);
                    //            }
                    //        }
                    //    }
                    //    break;

                    default:

                        break;

                }
            }
        }
        private void endEdit(GridViewCellInfo e)
        {

            if (!(e.RowInfo is GridViewFilteringRowInfo))
            {                               
                if (e.ColumnInfo.Name == "debitLine")
                {
                    if (e.Value != null)
                    {
                        decimal d = (decimal)e.Value;
                        if (d != 0)
                        {
                            GridViewRowInfo info = e.RowInfo.ViewInfo.CurrentRow;
                            info.Cells["creditLine"].Value = "0,00";
                        }
                    }
                }

                if (e.ColumnInfo.Name == "creditLine")
                {
                    if (e.Value != null)
                    {
                        decimal d = (decimal)e.Value;
                        if (d != 0)
                        {
                            GridViewRowInfo info = e.RowInfo.ViewInfo.CurrentRow;
                            info.Cells["debitLine"].Value = "0,00";
                        }
                    }
                }

                DisplayBottomDetails(e.RowInfo);
            }
        }

        private void gridDailyMemorial_SelectionChanged(object sender, EventArgs e)
        {

        }


        private void DisplayBottomDetails(GridViewRowInfo currentRow)
        {
            if (currentRow != null && currentRow.DataBoundItem != null)
            {
                lbl_account_value.Text = "...";
                lbl_debcre_value.Text = "...";
                lbl_project_value.Text = "...";
                lbl_cost_value.Text = "...";

                try
                {
                    AccLineModel model = (AccLineModel)currentRow.DataBoundItem;
                    if (model != null)
                    {
                        if (model.numberLedAccount != null && model.numberLedAccount.Trim() != "")
                        {
                            LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                            LedgerAccountModel lam = new LedgerAccountModel();
                            lam = ledbus.GetAccount(model.numberLedAccount, Login._bookyear);

                            if (lam != null)
                                lbl_account_value.Text = lam.descLedgerAccount;

                        }

                        if (model.idClientLine != null && model.idClientLine.Trim() != "")
                        {

                            AccDebCreBUS debpers = new AccDebCreBUS();
                            AccDebCreModel pm1 = new AccDebCreModel();
                            ClientBUS cb = new ClientBUS();
                            ClientModel cm = new ClientModel();
                            PersonBUS pbs = new PersonBUS();
                            PersonModel pmd = new PersonModel();

                            pm1 = debpers.GetCustomerByAccCode(model.idClientLine);
                            if (pm1 != null)
                            {
                                if (pm1.idClient != null && pm1.idContPerson == 0)
                                {
                                    cm = cb.GetClient(pm1.idClient);
                                    if (cm != null)
                                    {
                                        lbl_debcre_value.Text = cm.nameClient;
                                    }
                                }
                                else
                                {
                                    if (pm1.idContPerson != null && pm1.idClient == 0)
                                        pmd = pbs.GetPerson(pm1.idContPerson);
                                    if (pmd != null)
                                    {
                                        lbl_debcre_value.Text = pmd.firstname + " " + pmd.midname + " " + pmd.lastname;

                                    }
                                }
                            }

                            //lbl_debcre_value.Text = model.idClientLine;
                        }


                        if (model.idProjectLine != null && model.idProjectLine.Trim() != "")
                        {
                            ArrangementBUS ab = new ArrangementBUS();
                            ArrangementModel am = new ArrangementModel();

                            am = ab.GetArrangementByCode(model.idProjectLine);
                            if (am != null)
                                lbl_project_value.Text = am.nameArrangement;
                        }

                        if (model.idCostLine != null && model.idCostLine.Trim() != "")
                        {
                            AccCostBUS dcb = new AccCostBUS();
                            AccCostModel dcm = new AccCostModel();
                            dcm = dcb.GetCostByID(model.idCostLine);
                            if (dcm != null)
                            {
                                lbl_cost_value.Text = dcm.descCost;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void gridDailyMemorial_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            DisplayBottomDetails(e.CurrentRow);

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                DialogResult dr = msg.translateAllMessageBoxDialogYesNo("Cancel entries ?", "Cancel");
                if (dr == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    AccLineBUS aclineBUS = new AccLineBUS(Login._bookyear);
                    List<AccLineModel> accLineModel = new List<AccLineModel>();
                    accLineModel = aclineBUS.GetAllLinesByIdCurrency(selectedDailyMemo.idDailyMem, (int)selectedDaily.idDaily, 0);
                    if (accLineModel != null)
                    {
                        dailyMemorialList.Clear();
                        foreach (var model in accLineModel)
                        {
                            dailyMemorialList.Add(model);
                        }

                        dailyOldMemorialList.Clear();
                        foreach (var model in accLineModel)
                        {
                            AccLineModel newmod = new AccLineModel();
                            newmod.bookingYear = model.bookingYear;
                            newmod.booksort = 1; // model.booksort; saki 28.7
                            newmod.cond1 = model.cond1;
                            newmod.cond2 = model.cond2;
                            newmod.cond3 = model.cond3;
                            newmod.creditBTW = model.creditBTW;
                            newmod.creditCurr = model.creditCurr;
                            newmod.creditLine = model.creditLine;
                            newmod.currrate = model.currrate;
                            newmod.debitBTW = model.debitBTW;
                            newmod.debitCurr = model.debitCurr;
                            newmod.debitLine = model.debitLine;
                            newmod.descDaily = model.descDaily;
                            newmod.descLine = model.descLine;
                            newmod.dtBooking = model.dtBooking;
                            newmod.dtLine = model.dtLine;
                            newmod.iban = model.iban;
                            newmod.idAccDaily = model.idAccDaily;
                            newmod.idAccLine = model.idAccLine;
                            newmod.idBTW = model.idBTW;
                            newmod.idClientLine = model.idClientLine;
                            newmod.idCostLine = model.idCostLine;
                            newmod.idCurrency = model.idCurrency;
                            newmod.idDetail = model.idDetail;
                            newmod.idMaster = model.idMaster;
                            newmod.idPersonLine = model.idPersonLine;
                            newmod.idProjectLine = model.idProjectLine;
                            newmod.idSepa = model.idSepa;
                            newmod.incopNr = model.incopNr;
                            newmod.invoiceNr = model.invoiceNr;
                            newmod.numberLedAccount = model.numberLedAccount;
                            newmod.periodLine = model.periodLine;
                            newmod.statusLine = model.statusLine;
                            newmod.term = model.term;
                            newmod.userN = model.userN;
                            newmod.versil = model.versil;

                            dailyOldMemorialList.Add(newmod);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void gridDailyMemorial_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Column.Name == "dtLine")
            {
                if (e.Column.IsVisible == true)
                {
                    if (e.CellElement.ColumnInfo is GridViewDateTimeColumn)
                    {
                        GridViewDataColumn dataColumn = e.CellElement.ColumnInfo as GridViewDataColumn;
                        if (dataColumn != null)
                        {
                            dataColumn.FormatString = "{0:dd-MM-yyyy}";
                        }
                    }
                }
            }
        }

        private void gridDailyMemorial_UserAddingRow(object sender, GridViewRowCancelEventArgs e)
        {
            
         //   RadMessageBox.Show("FFF");
            
            
        }

        private void radDateTimeDailyMemo_ValueChanged(object sender, EventArgs e)
        {
            //Console.WriteLine(radDateTimeDailyMemo.Value.ToString());
        }

        private void btnUpdateDates_Click(object sender, EventArgs e)
        {
            if (dailyMemorialList.Count > 0)
            {
                translateRadMessageBox msgbox = new translateRadMessageBox();
                DialogResult dr = msgbox.translateAllMessageBoxDialogYesNo("All dates in daily memos will be updated to:\n " + radDateTimeDailyMemo.Value.ToString("dd-MM-yyyy") + "\n\nContinue ?", "Warrning");
                if (dr == DialogResult.Yes)
                {
                    foreach (AccLineModel m in dailyMemorialList)
                    {
                        m.dtLine = radDateTimeDailyMemo.Value;
                    }

                    foreach (GridViewRowInfo rowInfo in gridDailyMemorial.Rows)
                    {
                        rowInfo.InvalidateRow();
                    }
                }
            }

        }

        private void frmDailyMemorial_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (gridDailyMemorial.CurrentColumn != null)
                {
                    if (gridDailyMemorial.IsInEditMode == true && gridDailyMemorial.CurrentColumn.Name == "invoiceNr")
                    {
                        RadTextBoxEditor active = gridDailyMemorial.ActiveEditor as RadTextBoxEditor;
                        RadTextBoxEditorElement editor = active.EditorElement as RadTextBoxEditorElement;

                        // if (e.KeyCode == Keys.F5)
                        //   radButtonSave.PerformClick();

                        if (e.KeyCode == Keys.F2)
                        {                            
                            AccOpenLinesBUS ccentar = new AccOpenLinesBUS();
                            List<IModel> gmX = new List<IModel>();
                            
                            gmX = ccentar.GetAllOpenLines();
                            var dlgSave = new GridLookupForm(gmX, "Open lines");

                            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                            {
                                AccOpenLinesModel genmX = new AccOpenLinesModel();
                                genmX = (AccOpenLinesModel)dlgSave.selectedRow;

                                if (genmX != null)
                                {
                                    //set textbox
                                    if (genmX.invoiceOpenLine != null)
                                        editor.Text = genmX.invoiceOpenLine;

                                    AccLineModel newline = new AccLineModel();
                                    gridDailyMemorial.CurrentRow.Cells["descLine"].Value = genmX.descOpenLine;
                                    gridDailyMemorial.CurrentRow.Cells["idClientLine"].Value = genmX.idDebCre;
                                    gridDailyMemorial.CurrentRow.Cells["idCostLine"].Value = genmX.codeCost;
                                    gridDailyMemorial.CurrentRow.Cells["idProjectLine"].Value = genmX.idProject;

                                }
                            }
                        }
                    }
                    else if (gridDailyMemorial.IsInEditMode == true && gridDailyMemorial.CurrentColumn.Name == "numberLedAccount")
                    {

                        RadTextBoxEditor active = gridDailyMemorial.ActiveEditor as RadTextBoxEditor;
                        RadTextBoxEditorElement editor = active.EditorElement as RadTextBoxEditorElement;
                        //gridDailyMemorial.ActiveEditor as RadTextBoxEditorElement;                    
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

                                if (genmX != null)
                                {
                                    //set textbox
                                    if (genmX.numberLedgerAccount != null)
                                        if (genmX.isBlockMemorial == false)
                                        {
                                            editor.Text = genmX.numberLedgerAccount;

                                        }
                                        else
                                        {
                                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                            {
                                                if (resxSet.GetString("Account is blocked for Memorial !!") != null)
                                                    RadMessageBox.Show(resxSet.GetString("Account is blocked for Memorial !!"));
                                                else
                                                    RadMessageBox.Show("Account is blocked for Memorial !!");
                                                editor.Text = "";
                                                editor.Focus();
                                            }
                                        }
                                }
                            }
                        }

                        if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                        {

                            if (editor.Text != "")
                            {
                                LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                                LedgerAccountModel lam = new LedgerAccountModel();

                                lam = ledbus.GetAccount(editor.Text, Login._bookyear);
                                if (lam != null)
                                {
                                    if (lam.isBlockMemorial == false && lam.isActiveLedgerAccount == false)
                                    {
                                        labelName.Text = lam.descLedgerAccount;

                                        gridDailyMemorial.EnterKeyMode = RadGridViewEnterKeyMode.EnterMovesToNextCell;


                                    }
                                    else
                                    {
                                        if (lam.isBlockMemorial == true)
                                        {
                                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                            {
                                                if (resxSet.GetString("Account is blocked for Memorial !!") != null)
                                                    RadMessageBox.Show(resxSet.GetString("Account is blocked for Memorial !!"));
                                                else
                                                    RadMessageBox.Show("Account is blocked for Memorial !!");
                                                editor.Text = "";
                                                editor.Focus();

                                            }
                                        }
                                        if (lam.isActiveLedgerAccount == true)
                                        {
                                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                            {
                                                if (resxSet.GetString("Account not active !!") != null)
                                                    RadMessageBox.Show(resxSet.GetString("Account not active !!"));
                                                else
                                                    RadMessageBox.Show("Account not active !!");
                                                editor.Text = "";
                                                editor.Focus();

                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    labelName.Text = "Wrong account";
                                    editor.Focus();
                                    gridDailyMemorial.EnterKeyMode = RadGridViewEnterKeyMode.None;


                                }
                            }
                            else
                            {
                                gridDailyMemorial.EnterKeyMode = RadGridViewEnterKeyMode.None;

                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                {
                                    if (resxSet.GetString("Account is mandatory !!") != null)
                                        RadMessageBox.Show(resxSet.GetString("Account is mandatory !!"));
                                    else
                                        RadMessageBox.Show("Account is mandatory !!");
                                    editor.Text = "";
                                    editor.Focus();
                                }
                                labelName.Text = "";

                                editor.Focus();

                            }

                        }
                    }
                    else if (gridDailyMemorial.IsInEditMode == true && gridDailyMemorial.CurrentColumn.Name == "idClientLine")
                    {
                        RadTextBoxEditor active = gridDailyMemorial.ActiveEditor as RadTextBoxEditor;
                        RadTextBoxEditorElement editor = active.EditorElement as RadTextBoxEditorElement;

                        if (e.KeyCode == Keys.F2)
                        {
                            int xX = this.Location.X;
                            int yY = this.Location.Y;
                            int fWidth = this.Width / 2;
                            int fHight = this.Height / 2;

                            debcreGrid.Show(xX + fWidth, yY + fHight - 30);

                        }
                        if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                        {
                            if (editor.Text != "")
                            {
                                AccDebCreBUS debpers = new AccDebCreBUS();
                                AccDebCreModel pm1 = new AccDebCreModel();
                                ClientBUS cb = new ClientBUS();
                                ClientModel cm = new ClientModel();
                                PersonBUS pbs = new PersonBUS();
                                PersonModel pmd = new PersonModel();


                                pm1 = debpers.GetCustomerByAccCode(editor.Text.ToString());
                                if (pm1 != null)
                                {
                                    if (pm1.idClient != null && pm1.idContPerson == 0)
                                    {
                                        cm = cb.GetClient(pm1.idClient);
                                        if (cm != null)
                                        {

                                            labelName.Text = cm.nameClient;
                                            editor.Text = cm.accountCodeClient;

                                        }
                                        else
                                        {
                                            labelName.Text = "";
                                        }
                                    }
                                    else
                                    {
                                        if (pm1.idContPerson != null && pm1.idClient == 0)
                                            pmd = pbs.GetPerson(pm1.idContPerson);
                                        if (pmd != null)
                                        {
                                            labelName.Text = pmd.firstname + " " + pmd.midname + " " + pmd.lastname;

                                        }
                                        else
                                        {
                                            labelName.Text = "";

                                        }
                                    }
                                }
                                else
                                {
                                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                    {
                                        if (resxSet.GetString("Wrong  Customer !!!") != null)
                                            RadMessageBox.Show(resxSet.GetString("Wrong  Customer !!!"));
                                        else
                                            RadMessageBox.Show("Wrong  Customer !!!");
                                        editor.Text = "";
                                        editor.Focus();
                                    }
                                    //labelName.Text = "Wrong  Customer !!!";
                                    editor.Focus();
                                }
                            }
                            else
                            {
                                labelName.Text = "";
                            }
                        }
                    }
                    else if (gridDailyMemorial.IsInEditMode == true && gridDailyMemorial.CurrentColumn.Name == "idCostLine")
                    {
                        RadTextBoxEditor active = gridDailyMemorial.ActiveEditor as RadTextBoxEditor;
                        RadTextBoxEditorElement editor = active.EditorElement as RadTextBoxEditorElement;

                        if (e.KeyCode == Keys.F2)
                        {
                            AccCostBUS ccentar = new AccCostBUS();
                            List<IModel> gmX = new List<IModel>();

                            gmX = ccentar.GetAllCost();
                            var dlgSave = new GridLookupForm(gmX, "Cost");

                            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                            {
                                AccCostModel genmX = new AccCostModel();
                                genmX = (AccCostModel)dlgSave.selectedRow;

                                if (genmX != null)
                                {
                                    //set textbox
                                    if (genmX.codeCost != null)
                                        editor.Text = genmX.codeCost;

                                }
                            }
                        }
                        if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                        {
                            if (editor.Text != "")
                            {
                                AccCostBUS acc = new AccCostBUS();
                                AccCostModel amc = new AccCostModel();
                                amc = acc.GetCostByID(editor.Text);
                                if (amc != null)
                                {
                                    labelName.Text = amc.descCost;


                                }
                                else
                                {
                                    labelName.Text = "Wrong Cost code";
                                    editor.Focus();
                                }
                            }
                            else
                            {
                                labelName.Text = "";
                            }
                        }
                    }
                    else if (gridDailyMemorial.IsInEditMode == true && gridDailyMemorial.CurrentColumn.Name == "idProjectLine")
                    {
                        RadTextBoxEditor active = gridDailyMemorial.ActiveEditor as RadTextBoxEditor;
                        RadTextBoxEditorElement editor = active.EditorElement as RadTextBoxEditorElement;

                        if (e.KeyCode == Keys.F2)
                        {
                            ArrangementBUS ccentar = new ArrangementBUS();
                            List<IModel> gmX = new List<IModel>();

                            gmX = ccentar.GetAllArrangementsAccount(Login._bookyear);
                            var dlgSave = new GridLookupForm(gmX, "Arrangement");

                            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
                            {
                                ArrangementModel genmX = new ArrangementModel();
                                genmX = (ArrangementModel)dlgSave.selectedRow;

                                if (genmX != null)
                                {
                                    //set textbox
                                    if (genmX.codeProject != null)
                                        editor.Text = genmX.codeProject;
                                }
                            }
                        }
                        if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                        {
                            if (editor.Text != "")
                            {
                                ArrangementBUS arb = new ArrangementBUS();
                                ArrangementModel arm = new ArrangementModel();
                                arm = arb.GetArrangementByCode(editor.Text);
                                if (arm != null)
                                {
                                    labelName.Text = arm.nameArrangement;
                                    gridDailyMemorial.EnterKeyMode = RadGridViewEnterKeyMode.EnterMovesToNextCell;
                                }
                                else
                                {
                                    labelName.Text = "Wrong Arrangement code";

                                    editor.Focus();
                                }

                            }
                            else
                            {
                                labelName.Text = "";
                            }
                        }
                    }
                    else if (gridDailyMemorial.IsInEditMode == true && gridDailyMemorial.CurrentColumn.Name == "debitLine")
                    {

                    }
                    else if (gridDailyMemorial.IsInEditMode == true && gridDailyMemorial.CurrentColumn.Name == "creditLine")
                    {

                    }
                    else if (gridDailyMemorial.IsInEditMode == true && gridDailyMemorial.CurrentColumn.Name == "")
                    {

                    }

                }                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridDailyMemorial_RowValidating(object sender, RowValidatingEventArgs e)
        {
            
        }

        private void radDateTimeDailyMemo_Leave(object sender, EventArgs e)
        {
            if (radDateTimeDailyMemo.Value.Year.ToString() != Login._bookyear)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Booking year NOT same as date !!");
                radDateTimeDailyMemo.Focus();
            }
        }
    }
}
