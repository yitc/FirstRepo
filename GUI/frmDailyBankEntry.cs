using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Model;
using BIS.DAO;
using System.Resources;
using BIS.Business;
using Telerik.WinControls.UI;
using Telerik.WinControls.Data;
using System.IO;
using System.Linq;


namespace GUI
{
    public partial class frmDailyBankEntry : frmTempAccount
    {
        // select iz prethodne forme
        private AccDailyModel selectedDaily;
        private AccDailyBankModel selectedDailyBank;
        private AccDailyMemModel selectedDailyMem;
        private AccDailyKasModel selectedDailyKas;
        private AccDailyBankModel selectedBankDelete;

        private BindingList<AccDailyBankModel> dailyBankList;
        private BindingList<AccDailyMemModel> dailyMemoList;
        private BindingList<AccLineModel> dailyMemoListSubItems;

        private BindingList<AccDailyKasModel> dailyKasList;

        private string layoutDailyBank;
        private string layoutDailyMemo;
        private string layoutDailyKas;
        private int idDaily;
        private int Code;
        private bool nonew;

        public frmDailyBankEntry(AccDailyModel dailymodel)
        {
            InitializeComponent();

            this.selectedDaily = dailymodel;

            this.Text = Login._companyModelList[0].nameCompany + " " + Login._bookyear + " - " + "Daily Bank";
        }

        private void frmDailyBankEntry_Load(object sender, EventArgs e)
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
            btnDelete.Visibility = ElementVisibility.Collapsed;

            layoutDailyBank = MainForm.gridFiltersFolder + "\\layoutAccDailyBankEntry.xml";
            layoutDailyMemo = MainForm.gridFiltersFolder + "\\layoutAccDailyMemorial.xml";
            Translation();



            if (selectedDaily != null)
            {
                this.labelDaily.Text = selectedDaily.codeDaily;
                this.labelDescription.Text = selectedDaily.descDaily;
                this.labelAccount.Text = selectedDaily.descLedgerAccount;
                this.labelIban.Text = selectedDaily.ibanBank;
                this.labelDailyType.Text = selectedDaily.descDailyType;
               
                this.labelDailyType.Text = selectedDaily.descDailyType;
            }

            if (selectedDaily.idDailyType == 1 )
            {
                btnDelete.Visibility = ElementVisibility.Visible;

                AccDailyBankBUS dbBus = new AccDailyBankBUS(Login._bookyear);
                List<AccDailyBankModel> dbmod = new List<AccDailyBankModel>();
                List<AccLineModel> limod = new List<AccLineModel>();



                dbmod = dbBus.GetAllByDaily(selectedDaily.codeDaily);          // GetAllBanks();
                nonew = false;
                if (dbmod != null)
                {
                      for (int w = 0; w < dbmod.Count; w++ )
                    {
                        dbmod[w].difference = Convert.ToDecimal(CalcDiff(dbmod[w].idDailyBank, Convert.ToDecimal(dbmod[w].begSaldo),  Convert.ToDecimal(dbmod[w].endSaldo)));
                        dbmod[w].booked = Convert.ToDecimal(dbmod[w].endSaldo) - Convert.ToDecimal(dbmod[w].begSaldo);
                        if (dbmod[w].difference != 0)
                            nonew = true;
                        else
                            nonew = false;
                    }
                      if (nonew == true)
                          btnNew.Enabled = false;
                    dailyBankList = new BindingList<AccDailyBankModel>(dbmod);

                    gridDailyBank.DataSource = dailyBankList;

                    if (gridDailyBank.Columns.Count > 0)
                    {
                    

                      //  gridDailyBank.Columns["codeDaily"].Width = 200;
                        gridDailyBank.Columns["codeDaily"].IsVisible = false;
                        gridDailyBank.Columns["idDailyBank"].IsVisible = false;
                        gridDailyBank.Columns["refNo"].Width = 100;
                        gridDailyBank.Columns["dtStatement"].Width = 100;
                        gridDailyBank.Columns["begSaldo"].Width = 200;
                        gridDailyBank.Columns["endSaldo"].Width = 200;
                        gridDailyBank.Columns["booked"].Width = 150;
                        gridDailyBank.Columns["difference"].Width = 100;
                       
                    }
                    
                    //bind hirarchical data
                    AccLineBUS alb = new AccLineBUS(Login._bookyear);
                    // get all where iddailytype = 1 and opensort and booksrot =1
                    limod = alb.GetAllLinesByDaily(selectedDaily.idDaily, 0);

                    GridViewTemplate template = new GridViewTemplate();
                    template.DataSource = limod;
                    template.AllowEditRow = false;
                    template.AllowDeleteRow = false;
                    template.AllowAddNewRow = false;
                    template.AllowColumnResize = true;

                    if (template.Columns.Count > 0)
                    {
                        template.Columns["idAccLine"].IsVisible = false;
                        template.Columns["idAccDaily"].IsVisible = false;
                        template.Columns["statusLine"].IsVisible = false;
                        template.Columns["periodLine"].IsVisible = false;
                        template.Columns["numberLedAccount"].IsVisible = false;
                        template.Columns["idClientLine"].IsVisible = false;
                        template.Columns["idPersonLine"].IsVisible = false;
                        template.Columns["idCostLine"].IsVisible = false;

                        template.Columns["idBTW"].IsVisible = false;
                        template.Columns["debitBTW"].IsVisible = false;
                        template.Columns["creditBTW"].IsVisible = false;
                        template.Columns["idCurrency"].IsVisible = false;
                        template.Columns["debitCurr"].IsVisible = false;
                        template.Columns["creditCurr"].IsVisible = false;
                        template.Columns["dtBooking"].IsVisible = false;
                        template.Columns["booksort"].IsVisible = false;
                        template.Columns["currrate"].IsVisible = false;
                        template.Columns["incopNr"].IsVisible = false;
                        template.Columns["iban"].IsVisible = false;
                        template.Columns["bookingYear"].IsVisible = false;

                        template.Columns["dtLine"].Width = 70;
                        template.Columns["invoiceNr"].Width = 70;
                        template.Columns["idClientLine"].Width = 70;
                        template.Columns["descLine"].Width = 200;
                        template.Columns["debitLine"].Width = 200;
                        template.Columns["creditLine"].Width = 200;
                        template.Columns["idBTW"].Width = 70;
                        template.Columns["idCostLine"].Width = 100;
                        template.Columns["idProjectLine"].Width = 100;
                    }
                                        
                    gridDailyBank.MasterTemplate.Templates.Add(template);
                    GridViewRelation relation = new GridViewRelation(gridDailyBank.MasterTemplate);
                    relation.ChildTemplate = template;
                    relation.RelationName = "DailyBank";
                    relation.ParentColumnNames.Add("idDailyBank");
                    relation.ChildColumnNames.Add("idCurrency");
                    gridDailyBank.Relations.Add(relation);




                    if (gridDailyBank.Rows.Count > 0)
                    {
                        gridDailyBank.GridNavigator.SelectFirstRow();
                        selectedDailyBank = (AccDailyBankModel)gridDailyBank.CurrentRow.DataBoundItem;
                    }
                    else
                        selectedDailyBank = null;

                    selectedDailyMem = null;
                    selectedDailyKas = null;
                }
                gridDailyBank.Visible = true;
                gridDailyBank.Dock = DockStyle.Fill;
                splitPanelDown.Controls.Add(gridDailyBank);

                
            }

            if (selectedDaily.idDailyType == 4)
            {
                this.Text = Login._companyModelList[0].nameCompany + " " + Login._bookyear + " - " + "Daily Memorial";

                AccDailyMemBUS dbBus = new AccDailyMemBUS(Login._bookyear);
                List<AccDailyMemModel> dbmod = new List<AccDailyMemModel>();
                List<AccLineModel> limod = new List<AccLineModel>();

                if (selectedDaily.automaticBook == true)
                    btnNew.Visibility = ElementVisibility.Hidden;

                dailyMemoList = new BindingList<AccDailyMemModel>();
                dailyMemoListSubItems = new BindingList<AccLineModel>();
                gridDailyMemo.DataSource = dailyMemoList;

                dbmod = dbBus.GetMemoByIdWithDebitCredit(selectedDaily.idDaily);                  //GetAllMemos();
                if (dbmod != null)
                {

                    //dailyMemoList = new BindingList<AccDailyMemModel>(dbmod);
                    foreach(var m in dbmod)
                    {
                        dailyMemoList.Add(m); ;
                    }

                    if (gridDailyMemo.Columns.Count > 0)
                    {
                      //  gridDailyMemo.Columns["idDailyMem"].ReadOnly = true;
                        gridDailyMemo.Columns["idDailyMem"].IsVisible = false;
                        gridDailyMemo.Columns["codeDaily"].IsVisible = false;
                     //   gridDailyMemo.Columns["codeDaily"].Width = 200;
                        gridDailyMemo.Columns["refNo"].Width = 100;
                        gridDailyMemo.Columns["dtMem"].Width = 200;
                    }

                    if (gridDailyMemo.Columns != null && gridDailyMemo.Columns.Count > 0)
                        gridDailyMemo.Columns["dtMem"].FormatString = "{0: dd-MM-yyyy}";

                    //bind hirarchical data
                    AccLineBUS alb = new AccLineBUS(Login._bookyear);
                    // get all where iddailytype = 1 and opensort and booksrot =1
                    limod = alb.GetAllLinesByDaily(selectedDaily.idDaily, 0);
                    dailyMemoListSubItems.Clear();
                    if(limod!=null)
                    foreach(var m in limod)
                    {
                        dailyMemoListSubItems.Add(m);
                    }

                    GridViewTemplate template = new GridViewTemplate();
                    template.DataSource = dailyMemoListSubItems;
                    template.AllowEditRow = false;
                    template.AllowDeleteRow = false;
                    template.AllowAddNewRow = false;
                    template.AllowColumnResize = true;

                    if (template.Columns.Count > 0)
                    {
                        template.Columns["idAccLine"].IsVisible = false;
                        template.Columns["idAccDaily"].IsVisible = false;
                        template.Columns["statusLine"].IsVisible = false;
                        template.Columns["periodLine"].IsVisible = false;
                        template.Columns["numberLedAccount"].IsVisible = false;
                        template.Columns["idClientLine"].IsVisible = false;
                        template.Columns["idPersonLine"].IsVisible = false;
                        template.Columns["idCostLine"].IsVisible = false;

                        template.Columns["idBTW"].IsVisible = false;
                        template.Columns["debitBTW"].IsVisible = false;
                        template.Columns["creditBTW"].IsVisible = false;
                        template.Columns["idCurrency"].IsVisible = false;
                        template.Columns["debitCurr"].IsVisible = false;
                        template.Columns["creditCurr"].IsVisible = false;
                        template.Columns["dtBooking"].IsVisible = false;
                        template.Columns["booksort"].IsVisible = false;
                        template.Columns["currrate"].IsVisible = false;
                        template.Columns["incopNr"].IsVisible = false;
                        template.Columns["debitLine"].IsVisible = true;
                        template.Columns["creditLine"].IsVisible = true;

                        template.Columns["dtLine"].Width = 70;
                        template.Columns["invoiceNr"].Width = 70;
                        template.Columns["idClientLine"].Width = 70;
                        template.Columns["descLine"].Width = 200;
                        template.Columns["debitLine"].Width = 200;
                        template.Columns["creditLine"].Width = 200;
                        template.Columns["idBTW"].Width = 70;
                        template.Columns["idCostLine"].Width = 100;
                        template.Columns["idProjectLine"].Width = 100;

                    }
                    gridDailyMemo.MasterTemplate.Templates.Add(template);
                    GridViewRelation relation = new GridViewRelation(gridDailyMemo.MasterTemplate);
                    relation.ChildTemplate = template;
                    relation.RelationName = "idDailyMem";
                    relation.ParentColumnNames.Add("idDailyMem");
                    relation.ChildColumnNames.Add("idCurrency");
                    gridDailyMemo.Relations.Add(relation);



                    if (gridDailyMemo.Rows.Count > 0)
                    {
                        gridDailyMemo.GridNavigator.SelectFirstRow();
                        selectedDailyMem = (AccDailyMemModel)gridDailyMemo.CurrentRow.DataBoundItem;
                    }
                    else
                        selectedDailyMem = null;


                    selectedDailyBank = null;
                }
                gridDailyMemo.Visible = true;
                gridDailyMemo.Dock = DockStyle.Fill;
                splitPanelDown.Controls.Add(gridDailyMemo);
            }

            if (selectedDaily.idDailyType == 5)
            {
                this.Text = Login._companyModelList[0].nameCompany + " " + Login._bookyear + " - " + "Kas";

                btnDelete.Visibility = ElementVisibility.Visible;

                AccDailyKasBUS dbBus = new AccDailyKasBUS();
                List<AccDailyKasModel> dbmod = new List<AccDailyKasModel>();
                List<AccLineModel> limod = new List<AccLineModel>();

                dbmod = dbBus.GetAllByDaily(selectedDaily.codeDaily, Login._bookyear);          // GetAllBanks();
                bool nonew = false;
                if (dbmod != null)
                {
                    for (int w = 0; w < dbmod.Count; w++)
                    {
                        dbmod[w].difference = Convert.ToDecimal(CalcDiff(dbmod[w].idAccDailyKas, Convert.ToDecimal(dbmod[w].begSaldo), Convert.ToDecimal(dbmod[w].endSaldo)));
                        dbmod[w].booked = Convert.ToDecimal(dbmod[w].endSaldo) - Convert.ToDecimal(dbmod[w].begSaldo);
                        if (dbmod[w].difference != 0)
                            nonew = true;
                        else
                            nonew = false;
                    }
                    if (nonew == true)
                        btnNew.Enabled = false;
                }
                   

                dbmod = dbBus.GetAllKas();
                if (dbmod != null)
                {
                   dailyKasList = new BindingList<AccDailyKasModel>(dbmod);

                    gridDailyKas.DataSource = dailyKasList;

                    if (gridDailyKas.Columns.Count > 0)
                    {
                        gridDailyKas.Columns["idAccDailyKas"].IsVisible = false;
                        gridDailyKas.Columns["codeDaily"].IsVisible = false;
                        gridDailyKas.Columns["refnoKas"].Width = 300;
                        gridDailyKas.Columns["dtKas"].Width = 200;
                        gridDailyKas.Columns["begSaldo"].Width = 200;
                        gridDailyKas.Columns["endSaldo"].Width = 200;
                        gridDailyKas.Columns["booked"].Width = 150;
                        gridDailyKas.Columns["difference"].Width = 100;
                    }

                    if (gridDailyKas.Columns != null && gridDailyKas.Columns.Count > 0)
                        gridDailyKas.Columns["dtKas"].FormatString = "{0: dd/MM/yyyy}";

                    //bind hirarchical data
                    AccLineBUS alb = new AccLineBUS(Login._bookyear);
                    // get all where iddailytype = 1 and opensort and booksrot =1
                    limod = alb.GetAllLinesByDaily(selectedDaily.idDaily, 0);

                    GridViewTemplate template = new GridViewTemplate();
                    template.DataSource = limod;
                    template.AllowEditRow = false;
                    template.AllowDeleteRow = false;
                    template.AllowAddNewRow = false;
                    template.AllowColumnResize = true;

                    if (template.Columns.Count > 0)
                    {
                        template.Columns["idAccLine"].IsVisible = false;
                        template.Columns["idAccDaily"].IsVisible = false;
                        template.Columns["statusLine"].IsVisible = false;
                        template.Columns["periodLine"].IsVisible = false;
                        template.Columns["numberLedAccount"].IsVisible = false;
                        template.Columns["idClientLine"].IsVisible = false;
                        template.Columns["idPersonLine"].IsVisible = false;
                        template.Columns["idCostLine"].IsVisible = false;

                        template.Columns["idBTW"].IsVisible = false;
                        template.Columns["debitBTW"].IsVisible = false;
                        template.Columns["creditBTW"].IsVisible = false;
                        template.Columns["idCurrency"].IsVisible = false;
                        template.Columns["debitCurr"].IsVisible = false;
                        template.Columns["creditCurr"].IsVisible = false;
                        template.Columns["dtBooking"].IsVisible = false;
                        template.Columns["booksort"].IsVisible = false;
                        template.Columns["currrate"].IsVisible = false;
                        template.Columns["incopNr"].IsVisible = false;

                        template.Columns["dtLine"].Width = 70;
                        template.Columns["invoiceNr"].Width = 70;
                        template.Columns["idClientLine"].Width = 70;
                        template.Columns["descLine"].Width = 200;
                        template.Columns["debitLine"].Width = 200;
                        template.Columns["creditLine"].Width = 200;
                        template.Columns["idBTW"].Width = 70;
                        template.Columns["idCostLine"].Width = 100;
                        template.Columns["idProjectLine"].Width = 100;
                    }

                    gridDailyKas.MasterTemplate.Templates.Add(template);
                    GridViewRelation relation = new GridViewRelation(gridDailyKas.MasterTemplate);
                    relation.ChildTemplate = template;
                    relation.RelationName = "DailyKas";
                    relation.ParentColumnNames.Add("idAccDailyKas");
                    relation.ChildColumnNames.Add("idCurrency");
                    gridDailyKas.Relations.Add(relation);


                    if (gridDailyKas.Rows.Count > 0)
                    {
                        gridDailyKas.GridNavigator.SelectFirstRow();
                        selectedDailyKas = (AccDailyKasModel)gridDailyKas.CurrentRow.DataBoundItem;
                    }
                    else
                       selectedDailyKas = null;

                    selectedDailyMem = null;
                    selectedDailyBank = null;
                }
                gridDailyKas.Visible = true;
                gridDailyKas.Dock = DockStyle.Fill;
                splitPanelDown.Controls.Add(gridDailyKas);

            }
        }

        private void gridDailyBank_Click(object sender, EventArgs e)
        {

        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutDailyBank))
            {
                File.Delete(layoutDailyBank);
            }
            gridDailyBank.SaveLayout(layoutDailyBank);

            RadMessageBox.Show("Layout Saved");
        }

        private void gridDailyBank_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if (gridDailyBank != null)
                if (gridDailyBank.Columns.Count > 0)
                    for (int i = 0; i < gridDailyBank.Columns.Count; i++)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (gridDailyBank.Columns[i].HeaderText != null && resxSet.GetString(gridDailyBank.Columns[i].HeaderText) != null)
                                gridDailyBank.Columns[i].HeaderText = resxSet.GetString(gridDailyBank.Columns[i].HeaderText);
                        }
                    }
            if (gridDailyBank.Columns != null && gridDailyBank.Columns.Count > 0)
                gridDailyBank.Columns["dtStatement"].FormatString = "{0: dd/MM/yyyy}";

        }
        private void gridDailyMemo_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if (gridDailyMemo != null)
                if (gridDailyMemo.Columns.Count > 0)
                    for (int i = 0; i < gridDailyMemo.Columns.Count; i++)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (gridDailyMemo.Columns[i].HeaderText != null && resxSet.GetString(gridDailyMemo.Columns[i].HeaderText) != null)
                                gridDailyMemo.Columns[i].HeaderText = resxSet.GetString(gridDailyMemo.Columns[i].HeaderText);
                        }
                    }
        }
        private void gridDailyKas_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if (gridDailyKas != null)
            {
                if (gridDailyKas.Columns.Count > 0)
                {
                    for (int i = 0; i < gridDailyKas.Columns.Count; i++)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (gridDailyKas.Columns[i].HeaderText != null && resxSet.GetString(gridDailyKas.Columns[i].HeaderText) != null)
                                gridDailyKas.Columns[i].HeaderText = resxSet.GetString(gridDailyKas.Columns[i].HeaderText);
                        }
                    }
                }
            }
        }

        private void gridDailyBank_UserAddedRow(object sender, GridViewRowEventArgs e)
        {            
            
            e.Row.IsSelected = true;
            e.Row.IsCurrent = true;
            selectedDailyBank = (AccDailyBankModel)e.Row.DataBoundItem;
    
        }

        private void gridDailyMemo_UserAddedRow(object sender, GridViewRowEventArgs e)
        {

            e.Row.IsSelected = true;
            e.Row.IsCurrent = true;
            selectedDailyMem = (AccDailyMemModel)e.Row.DataBoundItem;

        }


        private void gridDailyBank_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row.DataBoundItem != null)
            {
                Type t = e.Row.DataBoundItem.GetType();
                if (t == typeof(AccDailyBankModel))
                {
                    selectedDailyBank = (AccDailyBankModel)e.Row.DataBoundItem;

                    if (selectedDailyBank != null && selectedDaily != null)
                    {
                        //  frmDailyBank frm = new frmDailyBank(selectedDaily, selectedDailyBank);
                        frmDailyBankView frm = new frmDailyBankView(selectedDaily, selectedDailyBank);
                        frm.ShowDialog();

                    }
                    AccDailyBankBUS dbBus = new AccDailyBankBUS(Login._bookyear);
                    List<AccDailyBankModel> dbmod = new List<AccDailyBankModel>();
                     List<AccLineModel> limod = new List<AccLineModel>();

                    // dbmod = dbBus.GetAllBanks();
                    dbmod = dbBus.GetAllByDaily(selectedDaily.codeDaily);
                    //===================
                    bool nonew = false;
                    if (dbmod != null)
                    {
                        for (int w = 0; w < dbmod.Count; w++)
                        {
                            dbmod[w].difference = Convert.ToDecimal(CalcDiff(dbmod[w].idDailyBank, Convert.ToDecimal(dbmod[w].begSaldo), Convert.ToDecimal(dbmod[w].endSaldo)));
                            dbmod[w].booked = Convert.ToDecimal(dbmod[w].endSaldo) - Convert.ToDecimal(dbmod[w].begSaldo);
                            if (dbmod[w].difference != 0)
                                nonew = true;
                            else
                                nonew = false;
                        }
                        if (nonew == true)
                            btnNew.Enabled = false;
                        else
                            btnNew.Enabled = true;
                    }
                    //==================
                    dailyBankList = new BindingList<AccDailyBankModel>(dbmod);
                    gridDailyBank.DataSource = dailyBankList;
                    if (gridDailyBank.Columns.Count > 0)
                    {
                    

                      //  gridDailyBank.Columns["codeDaily"].Width = 200;
                        gridDailyBank.Columns["codeDaily"].IsVisible = false;
                        gridDailyBank.Columns["idDailyBank"].IsVisible = false;
                        gridDailyBank.Columns["refNo"].Width = 100;
                        gridDailyBank.Columns["dtStatement"].Width = 100;
                        gridDailyBank.Columns["begSaldo"].Width = 200;
                        gridDailyBank.Columns["endSaldo"].Width = 200;
                        gridDailyBank.Columns["booked"].Width = 150;
                        gridDailyBank.Columns["difference"].Width = 100;
                       
                    }
                    
                    //bind hirarchical data
                    AccLineBUS alb = new AccLineBUS(Login._bookyear);
                    // get all where iddailytype = 1 and opensort and booksrot =1
                    limod = alb.GetAllLinesByDaily(selectedDaily.idDaily, 0);

                    GridViewTemplate template = new GridViewTemplate();
                    template.DataSource = limod;
                    template.AllowEditRow = false;
                    template.AllowDeleteRow = false;
                    template.AllowAddNewRow = false;
                    template.AllowColumnResize = true;

                    if (template.Columns.Count > 0)
                    {
                        template.Columns["idAccLine"].IsVisible = false;
                        template.Columns["idAccDaily"].IsVisible = false;
                        template.Columns["statusLine"].IsVisible = false;
                        template.Columns["periodLine"].IsVisible = false;
                        template.Columns["numberLedAccount"].IsVisible = false;
                        template.Columns["idClientLine"].IsVisible = false;
                        template.Columns["idPersonLine"].IsVisible = false;
                        template.Columns["idCostLine"].IsVisible = false;

                        template.Columns["idBTW"].IsVisible = false;
                        template.Columns["debitBTW"].IsVisible = false;
                        template.Columns["creditBTW"].IsVisible = false;
                        template.Columns["idCurrency"].IsVisible = false;
                        template.Columns["debitCurr"].IsVisible = false;
                        template.Columns["creditCurr"].IsVisible = false;
                        template.Columns["dtBooking"].IsVisible = false;
                        template.Columns["booksort"].IsVisible = false;
                        template.Columns["currrate"].IsVisible = false;
                        template.Columns["incopNr"].IsVisible = false;
                        template.Columns["iban"].IsVisible = false;
                        template.Columns["bookingYear"].IsVisible = false;

                        template.Columns["dtLine"].Width = 70;
                        template.Columns["invoiceNr"].Width = 70;
                        template.Columns["idClientLine"].Width = 70;
                        template.Columns["descLine"].Width = 200;
                        template.Columns["debitLine"].Width = 200;
                        template.Columns["creditLine"].Width = 200;
                        template.Columns["idBTW"].Width = 70;
                        template.Columns["idCostLine"].Width = 100;
                        template.Columns["idProjectLine"].Width = 100;
                    }
                                        
                    gridDailyBank.MasterTemplate.Templates.Add(template);
                    GridViewRelation relation = new GridViewRelation(gridDailyBank.MasterTemplate);
                    relation.ChildTemplate = template;
                    relation.RelationName = "DailyBank";
                    relation.ParentColumnNames.Add("idDailyBank");
                    relation.ChildColumnNames.Add("idCurrency");
                    gridDailyBank.Relations.Add(relation);

                    if (gridDailyBank.Rows.Count > 0)
                    {
                        gridDailyBank.GridNavigator.SelectFirstRow();
                        selectedDailyBank = (AccDailyBankModel)gridDailyBank.CurrentRow.DataBoundItem;
                    }
                    else
                        selectedDailyBank = null;

                    selectedDailyMem = null;
                    selectedDailyKas = null;
                }
                    gridDailyBank.Visible = true;
                    gridDailyBank.Dock = DockStyle.Fill;
                    splitPanelDown.Controls.Add(gridDailyBank);
            }

        }
        private void gridDailyMemo_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row.DataBoundItem != null)
            {
                Type t = e.Row.DataBoundItem.GetType();
                if (t == typeof(AccDailyMemModel))
                {
                    selectedDailyMem = (AccDailyMemModel)e.Row.DataBoundItem;

                    if (selectedDailyMem != null && selectedDaily != null)
                    {
                        using (frmDailyMemorial frm = new frmDailyMemorial(selectedDaily, selectedDailyMem))
                        {
                            frm.ShowDialog();

                            AccDailyMemBUS dbBus = new AccDailyMemBUS(Login._bookyear);
                            List<AccDailyMemModel> dbmod = new List<AccDailyMemModel>();

                            dbmod = dbBus.GetAllByDaily(selectedDaily.codeDaily);

                            if (dbmod != null)
                            {
                                dailyMemoList.Clear();
                                foreach (var m in dbmod)
                                {
                                    dailyMemoList.Add(m); ;
                                }
                            }

                            AccLineBUS alb = new AccLineBUS(Login._bookyear);
                            List<AccLineModel> limod = alb.GetAllLinesByDaily(selectedDaily.idDaily, 0);
                            if (limod != null)
                            {
                                dailyMemoListSubItems.Clear();
                                foreach (var m in limod)
                                {
                                    dailyMemoListSubItems.Add(m);
                                }

                                gridDailyMemo.MasterTemplate.CollapseAll();
                            }
                        }

                    }
                }
            }

        }


        private void gridDailyBank_UserDeletedRow(object sender, GridViewRowEventArgs e)
        {            
            if(e.Row.DataBoundItem != null)
            {
                AccDailyBankBUS dbus = new AccDailyBankBUS(Login._bookyear);
                AccDailyBankModel dmodel = (AccDailyBankModel)e.Row.DataBoundItem;

                dbus.Delete(dmodel.idDailyBank, this.Name, Login._user.idUser);
            }
        }
        private void gridDailyMemo_UserDeletedRow(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataBoundItem != null)
            {
                AccDailyMemBUS dbus = new AccDailyMemBUS(Login._bookyear);
                AccDailyMemModel dmodel = (AccDailyMemModel)e.Row.DataBoundItem;

                dbus.Delete(dmodel.idDailyMem, this.Name, Login._user.idUser);
            }
        }
        private void gridDailyBank_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {
            if(gridDailyBank.CurrentRow.DataBoundItem != null)
            {
                 AccDailyBankModel dmodel = (AccDailyBankModel)gridDailyBank.CurrentRow.DataBoundItem;
                 DialogResult dr = RadMessageBox.Show("DELETE " + dmodel.refNo + " ? ", "Delete", MessageBoxButtons.YesNo);

                if(dr == DialogResult.Yes)
                {
                    AccDailyBankBUS dbus = new AccDailyBankBUS(Login._bookyear);
                    dbus.Delete(dmodel.idDailyBank, this.Name, Login._user.idUser);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void gridDailyMemo_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {
            if (gridDailyMemo.CurrentRow.DataBoundItem != null)
            {
                AccDailyMemModel dmodel = (AccDailyMemModel)gridDailyMemo.CurrentRow.DataBoundItem;
                DialogResult dr = RadMessageBox.Show("DELETE " + dmodel.refNo + " ? ", "Delete", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    AccDailyMemBUS dbus = new AccDailyMemBUS(Login._bookyear);
                    dbus.Delete(dmodel.idDailyMem, this.Name, Login._user.idUser);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            int idType = Convert.ToInt32(selectedDaily.idDailyType);
            string codeDly = selectedDaily.codeDaily;


            if (selectedDaily.idDailyType == 4)
            {
                AccDailyMemModel dailyMemo = new AccDailyMemModel();
                AccDailyMemModel db = new AccDailyMemModel();
                AccDailyMemBUS bus = new AccDailyMemBUS(Login._bookyear);

                db = bus.GetLastMemByStatement(selectedDaily.idDaily.ToString());

                string RefNo = "1";
                string strBookYear = Login._bookyear;
                bool beginPeriod = false;
                if (db != null)
                {
                    RefNo = (db.refNo + 1).ToString();
                    strBookYear = db.bookingYear;
                    if (db.beginPeriod == true)
                        beginPeriod = true;
                }

                dailyMemo.codeDaily = selectedDaily.codeDaily;
                dailyMemo.refNo = Int32.Parse(RefNo);
                dailyMemo.dtMem = DateTime.Now;
                dailyMemo.bookingYear = strBookYear;
                dailyMemo.beginPeriod = beginPeriod;
                dailyMemo.userCreated = Login._user.idUser;
                dailyMemo.idDailyMem = bus.SaveAndReturnID(dailyMemo, this.Name, Login._user.idUser);

                using (frmDailyMemorial frmMemo = new frmDailyMemorial(selectedDaily, dailyMemo))
                {
                    frmMemo.ShowDialog();

                    AccDailyMemBUS dbBus = new AccDailyMemBUS(Login._bookyear);
                    List<AccDailyMemModel> dbmod = new List<AccDailyMemModel>();

                    dbmod = dbBus.GetAllByDaily(selectedDaily.codeDaily);

                    if (dbmod != null)
                    {
                        dailyMemoList.Clear();
                        foreach (var m in dbmod)
                        {
                            dailyMemoList.Add(m); ;
                        }
                    }

                    AccLineBUS alb = new AccLineBUS(Login._bookyear);
                    List<AccLineModel> limod = alb.GetAllLinesByDaily(selectedDaily.idDaily, 0);
                    if (limod != null)
                    {
                        dailyMemoListSubItems.Clear();
                        foreach (var m in limod)
                        {
                            dailyMemoListSubItems.Add(m);
                        }
                        gridDailyMemo.MasterTemplate.CollapseAll();
                    }
                }
            }
            else
            {

                frmDailyBankAddNew frm = new frmDailyBankAddNew(codeDly, idType);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.Yes)
                {
                    if (selectedDaily.idDailyType == 1)
                    {
                        AccDailyBankBUS dbBus = new AccDailyBankBUS(Login._bookyear);
                        List<AccDailyBankModel> dbmod = new List<AccDailyBankModel>();
                        dbmod = dbBus.GetAllByDaily(selectedDaily.codeDaily);
                        dailyBankList = new BindingList<AccDailyBankModel>(dbmod);

                        gridDailyBank.DataSource = dailyBankList;
                    }
                    //====
                    else
                    {
                        if (selectedDaily.idDailyType == 5)
                        {
                            AccDailyKasBUS kasBus = new AccDailyKasBUS();
                            List<AccDailyKasModel> kasmod = new List<AccDailyKasModel>();
                            kasmod = kasBus.GetAllByDaily(selectedDaily.codeDaily, Login._bookyear);
                            dailyKasList = new BindingList<AccDailyKasModel>(kasmod);

                            gridDailyKas.DataSource = dailyKasList;
                        }
                    }
                    //===

                }
            }
            
        }
        
        private void Translation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                lblDaily.Text = resxSet.GetString("Dbk.Code");
                lblDescription.Text = resxSet.GetString("Description");
                lblDailyType.Text = resxSet.GetString("Daily type");
                lblAccount.Text = resxSet.GetString("Dbk.Account");
                lblIban.Text = resxSet.GetString("IBAN");
                btnNew.Text = resxSet.GetString("New");
                btnDelete.Text = resxSet.GetString("Delete");
            }


        }

        private void gridDailyKas_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row.DataBoundItem != null)
            {
                Type t = e.Row.DataBoundItem.GetType();
                if (t == typeof(AccDailyKasModel))
                {
                    selectedDailyKas = (AccDailyKasModel)e.Row.DataBoundItem;

                    if (selectedDailyKas != null && selectedDaily != null)
                    {
                        //  frmDailyBank frm = new frmDailyBank(selectedDaily, selectedDailyBank);
                        frmDailyKasView frm = new frmDailyKasView(selectedDaily, selectedDailyKas);
                        frm.ShowDialog();

                    }
                    AccDailyKasBUS dbBus = new AccDailyKasBUS();
                    List<AccDailyKasModel> dbmod = new List<AccDailyKasModel>();
                    // dbmod = dbBus.GetAllBanks();
                    
                    dbmod = dbBus.GetAllByDaily(selectedDaily.codeDaily, Login._bookyear);
                    //===================
                    bool nonew = false;
                    if (dbmod != null)
                    {
                        for (int w = 0; w < dbmod.Count; w++)
                        {
                            dbmod[w].difference = Convert.ToDecimal(CalcDiff(dbmod[w].idAccDailyKas, Convert.ToDecimal(dbmod[w].begSaldo), Convert.ToDecimal(dbmod[w].endSaldo)));
                            dbmod[w].booked = Convert.ToDecimal(dbmod[w].endSaldo) - Convert.ToDecimal(dbmod[w].begSaldo);
                            if (dbmod[w].difference != 0)
                                nonew = true;
                            else
                                nonew = false;
                        }
                        if (nonew == true)
                            btnNew.Enabled = false;
                        else
                            btnNew.Enabled = true;
                    }
                }
            }
        }

        private decimal CalcDiff(int idbank, decimal begs, decimal ends)
        {
            decimal sumDebT = 0;
            decimal sumCreT = 0;
            decimal begsld = 0;
            decimal endsld = 0;
            int idDbank = 0;
            idDbank = idbank;
            decimal stDif = 0;
            begsld = begs;
            endsld = ends;


            AccLineDAO sumis = new AccLineDAO(Login._bookyear);
            object adeb = sumis.SumDebitLinesByNalog(idDbank);
            object acre = sumis.SumCreditLinesByNalog(idDbank);
            if (adeb != null && acre != null)
            {
                sumDebT = Convert.ToDecimal(sumis.SumDebitLinesByNalog(idDbank));

                sumCreT = Convert.ToDecimal(sumis.SumCreditLinesByNalog(idDbank));

                stDif = 0;
                if (sumCreT == 0 && sumDebT == 0)
                {
                    stDif = Convert.ToDecimal(endsld) - Convert.ToDecimal(begsld);
                    nonew = true;
                }
                else
                    stDif = Convert.ToDecimal(endsld) - Convert.ToDecimal(begsld) - (Convert.ToDecimal(sumDebT) - Convert.ToDecimal(sumCreT));  //ovo je bilo pre sada je razdvojeno  Convert.ToDecimal(endsld) - Convert.ToDecimal(begsld) -
                 //txtDiffBook.Text = stDif.ToString();
            }
            else
            {
                stDif = Convert.ToDecimal(endsld) - Convert.ToDecimal(begsld);
                if (stDif != 0)
                    nonew = true;
            }
            return stDif;


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedDaily != null)
                if (selectedDaily.idDailyType == 1)
                    deleteBank();
                else
                {
                    if (selectedDaily.idDailyType == 5)
                        deleteKas();
                }

        }
        private void deleteBank()
        {
            if (gridDailyBank != null)
                if (gridDailyBank.SelectedRows!=null)
                    if (gridDailyBank.SelectedRows.Count >0)
            if (selectedDailyBank != null)
            {
                translateRadMessageBox dr = new translateRadMessageBox();
                if (dr.translateAllMessageBoxDialogYesNo("Do you want to DELETE this line ?","Delete") == DialogResult.Yes)
                {
                    AccDailyBankBUS dbus = new AccDailyBankBUS(Login._bookyear);

                    AccLineBUS alb = new AccLineBUS(Login._bookyear);
                    List<AccLineModel> alm = new List<AccLineModel>();
                    alm = alb.GetAllLinesByIdCurrency(idDaily, Code, 0);
                    if (alm != null && alm.Count > 0)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Can't delete, there are lines booked !");
                        return;
                    }
                    else
                    {

                        AccDailyBankModel acbRow = new AccDailyBankModel();
                        acbRow = (AccDailyBankModel)gridDailyBank.SelectedRows[0].DataBoundItem;
                        //List<int> list =  new List<int>(); 
                        List<AccDailyBankModel> acbmList = new  List<AccDailyBankModel>(dailyBankList);

                        if(acbmList!=null)
                        {

                        AccDailyBankModel acbm = new AccDailyBankModel();
                        acbm = acbmList.OrderByDescending(item => item.refNo).FirstOrDefault();

                        if (acbm.refNo != null)
                        {

                            if (acbm.refNo != acbRow.refNo)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("You can't delete beacause its no last one!");
                                return;
                            }
                            bool llok = false;
                            llok = dbus.Delete(idDaily, this.Name, Login._user.idUser);
                            if (llok == false)
                            {

                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("Error deleting !");
                                return;
                            }
                            else
                            {
                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                {
                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translateAllMessageBox("Deleted !");
                                }
                            }

                            AccDailyBankBUS dbBus = new AccDailyBankBUS(Login._bookyear);
                            List<AccDailyBankModel> dbmod = new List<AccDailyBankModel>();
                            List<AccLineModel> limod = new List<AccLineModel>();
                            dbmod = dbBus.GetAllByDaily(selectedDaily.codeDaily);          // GetAllBanks();
                            bool nonew = false;
                            if (dbmod != null)
                            {
                                for (int w = 0; w < dbmod.Count; w++)
                                {
                                    dbmod[w].difference = Convert.ToDecimal(CalcDiff(dbmod[w].idDailyBank, Convert.ToDecimal(dbmod[w].begSaldo), Convert.ToDecimal(dbmod[w].endSaldo)));
                                    dbmod[w].booked = Convert.ToDecimal(dbmod[w].endSaldo) - Convert.ToDecimal(dbmod[w].begSaldo);
                                    if (dbmod[w].difference != 0)
                                        nonew = true;
                                    else
                                        nonew = false;
                                }
                            }
                            if (nonew == true)
                                btnNew.Enabled = false;
                            if (dbmod != null)
                                dailyBankList = new BindingList<AccDailyBankModel>(dbmod);

                            gridDailyBank.DataSource = dailyBankList;
                        }
                    }
                    }
                }
                else
                {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Nothing to delete !");
                }
            }
          
        }

        //==
        private void deleteKas()
        {
            if (gridDailyKas != null)
                if (gridDailyKas.SelectedRows != null)
                    if (gridDailyKas.SelectedRows.Count > 0)
                        if (selectedDailyKas != null)
                        {
                            translateRadMessageBox dr = new translateRadMessageBox();
                            if (dr.translateAllMessageBoxDialogYesNo("Do you want to DELETE this line ?", "Delete") == DialogResult.Yes)
                            {
                                AccDailyKasBUS dbus = new AccDailyKasBUS();

                                AccLineBUS alb = new AccLineBUS(Login._bookyear);
                                List<AccLineModel> alm = new List<AccLineModel>();
                                alm = alb.GetAllLinesByIdCurrency(selectedDailyKas.idAccDailyKas, Convert.ToInt32(selectedDailyKas.codeDaily), 0);
                                if (alm != null && alm.Count > 0)
                                {
                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translateAllMessageBox("Can't delete, there are lines booked !");
                                    return;
                                }
                                else
                                {

                                    AccDailyKasModel acbRow = new AccDailyKasModel();
                                    acbRow = (AccDailyKasModel)gridDailyKas.SelectedRows[0].DataBoundItem;
                                    //List<int> list =  new List<int>(); 
                                    List<AccDailyKasModel> acbmList = new List<AccDailyKasModel>(dailyKasList);

                                    if (acbmList != null)
                                    {

                                        AccDailyKasModel acbm = new AccDailyKasModel();
                                        acbm = acbmList.OrderByDescending(item => item.refnoKas).FirstOrDefault();

                                        if (acbm.refnoKas != null)
                                        {

                                            if (acbm.refnoKas != acbRow.refnoKas)
                                            {
                                                translateRadMessageBox tr = new translateRadMessageBox();
                                                tr.translateAllMessageBox("You can't delete beacause its no last one!");
                                                return;
                                            }
                                            bool llok = false;
                                            llok = dbus.Delete(acbRow.idAccDailyKas, this.Name, Login._user.idUser, Login._bookyear);
                                            if (llok == false)
                                            {

                                                translateRadMessageBox tr = new translateRadMessageBox();
                                                tr.translateAllMessageBox("Error deleting !");
                                                return;
                                            }
                                            else
                                            {
                                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                                {
                                                    translateRadMessageBox tr = new translateRadMessageBox();
                                                    tr.translateAllMessageBox("Deleted !");
                                                }
                                            }

                                            AccDailyKasBUS dbBus = new AccDailyKasBUS();
                                            List<AccDailyKasModel> dbmod = new List<AccDailyKasModel>();
                                            List<AccLineModel> limod = new List<AccLineModel>();
                                            dbmod = dbBus.GetAllByDaily(selectedDaily.codeDaily,Login._bookyear);          // GetAllBanks();
                                            bool nonew = false;
                                            if (dbmod != null)
                                            {
                                                for (int w = 0; w < dbmod.Count; w++)
                                                {
                                                    dbmod[w].difference = Convert.ToDecimal(CalcDiff(dbmod[w].idAccDailyKas, Convert.ToDecimal(dbmod[w].begSaldo), Convert.ToDecimal(dbmod[w].endSaldo)));
                                                    dbmod[w].booked = Convert.ToDecimal(dbmod[w].endSaldo) - Convert.ToDecimal(dbmod[w].begSaldo);
                                                    if (dbmod[w].difference != 0)
                                                        nonew = true;
                                                    else
                                                        nonew = false;
                                                }
                                            }
                                            if (nonew == true)
                                                btnNew.Enabled = false;
                                            if (dbmod != null)
                                                dailyKasList = new BindingList<AccDailyKasModel>(dbmod);
                                            gridDailyKas.DataSource = null;
                                            gridDailyKas.DataSource = dailyKasList;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("Nothing to delete !");
                            }
                        }

        }

        //===
        private void gridDailyBank_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (gridDailyBank.CurrentRow != null)
            {
                GridViewRowInfo info = this.gridDailyBank.CurrentRow;
                if (info.DataBoundItem.GetType() == typeof(AccDailyBankModel))
                {
                    AccDailyBankModel selectedBankDelete = (AccDailyBankModel)info.DataBoundItem;
                    idDaily = selectedBankDelete.idDailyBank;
                    Code = Convert.ToInt32(selectedBankDelete.codeDaily);
                }
              
            }
           
        }

        private void gridDailyBank_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            if (gridDailyBank.CurrentRow != null)
            {
                GridViewRowInfo info = this.gridDailyBank.CurrentRow;
                if (info.DataBoundItem.GetType() == typeof(AccDailyBankModel))
                {
                    AccDailyBankModel selectedBankDelete = (AccDailyBankModel)info.DataBoundItem;
                    idDaily = selectedBankDelete.idDailyBank;
                    Code = Convert.ToInt32(selectedBankDelete.codeDaily);
                }

            }
        }

        private bool IsExpandable(GridViewRowInfo rowInfo)
        {
            if (rowInfo.ChildRows != null && rowInfo.ChildRows.Count > 0)
            {
                return true;
            }

            return false;
        }

        private void gridDailyKas_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            
        }

        private void gridDailyKas_ChildViewExpanding(object sender, ChildViewExpandingEventArgs e)
        {
           
        }

        private void gridDailyMemo_ChildViewExpanding(object sender, ChildViewExpandingEventArgs e)
        {
            e.Cancel = !IsExpandable(e.ParentRow); 
        }

        private void gridDailyMemo_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            GridGroupExpanderCellElement cell = e.CellElement as GridGroupExpanderCellElement;
            if (cell != null && e.CellElement.RowElement is GridDataRowElement)
            {
                if (!IsExpandable(cell.RowInfo))
                {
                    cell.Expander.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
                }
                else
                {
                    cell.Expander.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                }
            }
        }


        
    }
}
