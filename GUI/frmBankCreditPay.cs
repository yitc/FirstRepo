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
using BIS.Business;
using BIS.Model;
using BIS.DAO;
using GUI.ReportsForms;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;


namespace GUI
{
    public partial class frmBankCreditPay : Telerik.WinControls.UI.RadForm
    {
        private string layoutBankCreditPay;
        private List<AccOpenLinesModel> acm;
        private AccLineModel linemodel;
        private AccOpenLinesModel olinemodel;
        private int xDaily;
        private string xConto;
        private List<AccOpenLinesModel> model;
        private List<AccOpenLinesModel> openmodel;
        private AccOpenLinesModel opline;
        private AccSettingsModel acc_settings;
        private decimal cntrl_sum;
        private int no_items;
        private string name_file;
        private string real_name;
        private int Bui_sepa_nr;
        BaseGridEditor _gridEditor;
        private int xDailyMem;
        private int sepa_nr = 0;
        private string sepa_name;
        private AccSepaModel selectedRowGrid;
        private List<AccSepaModel> sepa_model;
        private List<AccSepaModel> sepa_hist;
        private string layoutHistory;
        private string layoutHisLines;
        private List<AccOpenLinesModel> lines;
        private List<AccOpenLinesModel> linesHistory;
        private List<AccOpenLinesModel> linesApproved;
        private string layoutProgres;
        private string layoutProgLines;
        private DataTable heder;
        private DataTable items;
        private int prolaz = 0;
        private string defaultSepaAcc;
        private string frmname;
        BindingList<SepaCombo> listaCombo = new BindingList<SepaCombo>();



        public frmBankCreditPay()
        {
            InitializeComponent();
        }
        public class SepaCombo
        {
            public int idSepa { get; set; }

            public SepaCombo()
            {
                this.idSepa = 0;
            }
        }
        private void frmBankCreditPay_Load(object sender, EventArgs e)
        {
            this.Icon = Login.iconForm;
            string name = "Bank Credit Pay";
            frmname = "frmBankCreditPay";

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(name) != null)
                    name = resxSet.GetString(name);
            }
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + name;

            layoutBankCreditPay = MainForm.gridFiltersFolder + "\\layoutBankCreditPay.xml";
            layoutHistory = MainForm.gridFiltersFolder + "\\layoutHistory.xml";
            layoutHisLines = MainForm.gridFiltersFolder + "\\layoutHisLines.xml";
            layoutProgres = MainForm.gridFiltersFolder + "\\layoutProgres.xml";
            layoutProgLines = MainForm.gridFiltersFolder + "\\layoutProgLines.xml";

            txtPlusDays.MaskedEditBoxElement.EnableMouseWheel = false;


            pvSepa.DefaultPage = pgSettings;
            setTranslation();
            dpValutaDate.Value = DateTime.Now;
            dpDateBook.Value = DateTime.Now;
           
            //===== manager =====
            if (Login._user.isUserManager == true)
                btnApprove.Enabled = true;
            else
                btnApprove.Enabled = false;

            //===================

            DateTime valuta = dpValutaDate.Value;

            DateTime dateplus = Convert.ToDateTime(dpValutaDate.Text).AddDays(Convert.ToInt32(txtPlusDays.Text));

            AccOpenLinesBUS abus = new AccOpenLinesBUS();
            acm = new List<AccOpenLinesModel>();
            acm = abus.GetOpenLinesByDates(valuta, dateplus);
            gridBank.DataSource = null;
            gridBank.DataSource = acm;

            AccSettingsBUS asb = new AccSettingsBUS();
            acc_settings = new AccSettingsModel();
            acc_settings = asb.GetSettingsByID(Login._bookyear);
            if (acc_settings != null)
            {
                defaultSepaAcc = acc_settings.defSepaAcc;
                txtIban.Text = acc_settings.myIban;
                txtBic.Text = acc_settings.myBic;
                txtFolder.Text = acc_settings.sepaPath;
                txtYear.Text = acc_settings.yearSettings;
            }
            ArrNrBUS an = new ArrNrBUS();
            ArrNrModel am = new ArrNrModel();
            am = an.GetSepaNoIncrement();
            if (am != null)
                txtCounter.Text = am.nrSEPA.ToString();



        }

        private void gridBank_ContextMenuOpening(object sender, Telerik.WinControls.UI.ContextMenuOpeningEventArgs e)
        {
            string saveLayout = "Save Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem = new RadMenuItem();
            customMenuItem.Text = saveLayout;
            customMenuItem.Click += SaveLayoutEB;
            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);
        }
        private void SaveLayoutEB(object sender, EventArgs e)
        {
            if (File.Exists(layoutBankCreditPay))
            {
                File.Delete(layoutBankCreditPay);
            }
            gridBank.SaveLayout(layoutBankCreditPay);
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("You have successfully save layout!") != null)
                    RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
                else
                    RadMessageBox.Show("You have successfully save layout!");
            }
        }

        private void gridBank_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
            if (gridBank != null)
            {
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



                    // gridBank.Columns["idCreditPay"].IsVisible = false;
                    // gridBank.Columns["idClient"].IsVisible = false;
                    // gridBank.Columns["idContPers"].IsVisible = false;
                    // gridBank.Columns["isApproved"].IsVisible = false;
                    // gridBank.Columns["isBooked"].IsVisible = false;
                    // gridBank.Columns["isSent"].IsVisible = false;
                    // gridBank.Columns["dtSent"].IsVisible = false;
                    // gridBank.Columns["namefile"].IsVisible = false;
                    // gridBank.Columns["approvedUser"].IsVisible = false;
                    // gridBank.Columns["createUser"].IsVisible = false;
                    // gridBank.Columns["dtCreation"].IsVisible = false;
                    // gridBank.Columns["payIban"].IsVisible = false;

                    // gridBank.Columns["amountInCurr"].IsVisible = false;
                    // gridBank.Columns["idDocument"].IsVisible = false;
                    // gridBank.Columns["idOption"].IsVisible = false;
                    // gridBank.Columns["paydays"].IsVisible = false;
                    // gridBank.Columns["idTask"].IsVisible = false;
                    // gridBank.Columns["isAprBook"].IsVisible = false;
                    //// gridBank.Columns["isSelected"].IsVisible = false;

                    // gridBank.Columns["isSelected"].ReadOnly = false;
                    // gridBank.Columns["dtItem"].ReadOnly = true;
                    // gridBank.Columns["dtValuta"].ReadOnly = true;
                    // gridBank.Columns["accNumber"].ReadOnly = true;
                    // gridBank.Columns["account"].ReadOnly = true;
                    // gridBank.Columns["invoiceNr"].ReadOnly = true;
                    // gridBank.Columns["inkopNr"].ReadOnly = true;
                    // gridBank.Columns["iban"].ReadOnly = true;
                    // gridBank.Columns["descItem"].ReadOnly = true;
                    // gridBank.Columns["amountC"].ReadOnly = true;
                    // gridBank.Columns["amountD"].ReadOnly = true;
                    // gridBank.Columns["cost"].ReadOnly = true;
                    // gridBank.Columns["project"].ReadOnly = true;
                    // gridBank.Columns["idBtw"].ReadOnly = true;
                    // gridBank.Columns["ndays"].ReadOnly = true;
                    // //gridBank.Columns["project"].ReadOnly = true;
                    // //gridBank.Columns["project"].ReadOnly = true;


                    gridBank.Columns["date"].FormatString = "{0: dd/MM/yyyy}";
                    gridBank.Columns["amount"].FormatString = "{0:N2}";
                }
            }


            if (File.Exists(layoutBankCreditPay))
            {
                gridBank.LoadLayout(layoutBankCreditPay);
            }

        }

        private void dpValutaDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime valuta = dpValutaDate.Value;

            DateTime dateplus = Convert.ToDateTime(dpValutaDate.Text).AddDays(Convert.ToInt32(txtPlusDays.Text));

            AccOpenLinesBUS abus = new AccOpenLinesBUS();
            acm = new List<AccOpenLinesModel>();
            acm = abus.GetOpenLinesByDates(valuta, dateplus);
            gridBank.DataSource = null;
            gridBank.DataSource = acm;

        }

        private void txtPlusDays_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void txtPlusDays_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                dpDateBook.Focus();
            }

            if(e.KeyCode==Keys.Up || e.KeyCode==Keys.Down)
            {
                e.Handled = true;
            }
        }
        private void txtPlusDays_Leave(object sender, EventArgs e)
        {
            DateTime valuta = dpValutaDate.Value;

            DateTime dateplus = Convert.ToDateTime(dpValutaDate.Text).AddDays(Convert.ToInt32(txtPlusDays.Text));

            AccOpenLinesBUS abus = new AccOpenLinesBUS();
            acm = new List<AccOpenLinesModel>();
            acm = abus.GetOpenLinesByDates(valuta, dateplus);
            gridBank.DataSource = null;
            gridBank.DataSource = acm;
        }

        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblValuta.Text) != null)
                    lblValuta.Text = resxSet.GetString(lblValuta.Text);

                if (resxSet.GetString(lblNrDays.Text) != null)
                    lblNrDays.Text = resxSet.GetString(lblNrDays.Text);

                if (resxSet.GetString(lblDateFile.Text) != null)
                    lblDateFile.Text = resxSet.GetString(lblDateFile.Text);

                if (resxSet.GetString(lblFile.Text) != null)
                    lblFile.Text = resxSet.GetString(lblFile.Text);

                if (resxSet.GetString(btnMake.Text) != null)
                    btnMake.Text = resxSet.GetString(btnMake.Text);

                if (resxSet.GetString(btnCancel.Text) != null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);

                if (resxSet.GetString(rbXml.Text) != null)
                    rbXml.Text = resxSet.GetString(rbXml.Text);
                if (resxSet.GetString(rbFinal.Text) != null)
                    rbFinal.Text = resxSet.GetString(rbFinal.Text);
                if (resxSet.GetString(rbProgress.Text) != null)
                    rbProgress.Text = resxSet.GetString(rbProgress.Text);


                for (int i = 0; i < pvSepa.Pages.Count; i++)
                {
                    if (resxSet.GetString(pvSepa.Pages[i].Text) != null)
                        pvSepa.Pages[i].Text = resxSet.GetString(pvSepa.Pages[i].Text);
                }

            
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMake_Click(object sender, EventArgs e)
        {
            AccOpenLinesBUS oplb = new AccOpenLinesBUS();


            if (txtNamefile.Text == "")
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Name is mandatory !!!") != null)
                        RadMessageBox.Show(resxSet.GetString("Name is mandatory !!!"));
                    else
                        RadMessageBox.Show("Name is mandatory !!!");
                }
                txtNamefile.Focus();
                return;
            }
            //if (txtDaily.Text == "")
            //{
            //    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            //    {
            //        if (resxSet.GetString("Enter Daily, please !!!") != null)
            //            RadMessageBox.Show(resxSet.GetString("Enter Daily, please !!!"));
            //        else
            //            RadMessageBox.Show("Enter Daily, please !!!");
            //    }
            //    btnDaily.PerformClick();
            //    return;
            //}
            // open line

           DialogResult dr = RadMessageBox.Show("Do you want to Save new SEPA ?" , "Delete", MessageBoxButtons.YesNo);
           if (dr == DialogResult.Yes)
           {
               decimal total_sepa = 0;
               ArrNrBUS anb = new ArrNrBUS();
               ArrNrModel anm = new ArrNrModel();
               anm = anb.GetSepaNr();
               if (anm != null)
                   sepa_nr = anm.nrSEPA;
               else
               {
                   using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                   {
                       if (resxSet.GetString("Error getting Sepa number !!!") != null)
                           RadMessageBox.Show(resxSet.GetString("Error getting Sepa number !!!"));
                       else
                           RadMessageBox.Show("Error getting Sepa number !!!");
                   }
                   return;
               }
               txtSepNr.Text = sepa_nr.ToString();
               //making sepa record
               AccSepaBUS asbus = new AccSepaBUS();
               AccSepaModel asmod = new AccSepaModel();
               asmod.idSepa = sepa_nr;
               asmod.dtCreationDate = DateTime.Now;
               asmod.dtSepa = dpDateBook.Value;
               asmod.nameSepa = txtNamefile.Text;
               asmod.status = 1;
             //  asmod.amountSepa = xxxx;


               //== upis sloga

               bool isSepaOk = false;
              
               isSepaOk = asbus.Save(asmod, frmname, Login._user.idUser);  // this.name_file
               txtSepNr.Text = "";
               if (isSepaOk == false)
               {
                   using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                   {
                       if (resxSet.GetString("Error writting Sepa header !!!") != null)
                           RadMessageBox.Show(resxSet.GetString("Error writting Sepa header !!!"));
                       else
                           RadMessageBox.Show("Error writting Sepa header !!!");
                   }
                   return;
               }
               else
               {
                   for (int y = 0; y < acm.Count;y++)
                   {
                       if(acm[y].iselected == true)
                       {
                           acm[y].idSepa = sepa_nr;
                           total_sepa = total_sepa + Convert.ToDecimal(acm[y].creditOpenLine);
                           bool isUpdate = false;
                           isUpdate = oplb.Update(acm[y], this.Name, Login._user.idUser);
                           if (isUpdate == false)
                           {
                               using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                               {
                                   if (resxSet.GetString("Error Updating open line !!!") != null)
                                       RadMessageBox.Show(resxSet.GetString("Error Updating open line !!!"));
                                   else
                                       RadMessageBox.Show("Error Updating open line !!!");
                               }
                               return;
                           }
                       }
                   }
                   asmod.amountSepa = total_sepa;
                   isSepaOk = false;
                   
                   isSepaOk = asbus.Update(asmod, frmname, Login._user.idUser); //this.name_file
                   if (isSepaOk == false)
                   {
                       using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                       {
                           if (resxSet.GetString("Error Updating Sepa amount !!!") != null)
                               RadMessageBox.Show(resxSet.GetString("Error Updating Sepa amount !!!"));
                           else
                               RadMessageBox.Show("Error Updating Sepa amount !!!");
                       }
                       return;
                   }
               

                   RadMessageBox.Show("Finish" + " making new SEPA "+sepa_nr);

                   gridBank.DataSource = null;
               }
           }

        }
        private void makeXml()
        {
            real_name = "";
            if (selectedRowGrid.status != 2)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Not in status for Xml") != null)
                        RadMessageBox.Show(resxSet.GetString("Not in status for Xml"));
                    else
                        RadMessageBox.Show("Not in status for Xml");
                }
                return;
            }
            no_items = lines.Count;
            cntrl_sum = 0;
            Bui_sepa_nr = selectedRowGrid.idSepa;             
            string datumi = selectedRowGrid.dtSepa.ToShortDateString().Replace("-", "");
          //  real_name = selectedRowGrid.nameSepa+"-"+selectedRowGrid.dtSepa.ToShortDateString()+"-"+Bui_sepa_nr.ToString();
            real_name = selectedRowGrid.nameSepa + "-" + datumi + "-" + Bui_sepa_nr.ToString();
            sepa_nr = selectedRowGrid.idSepa;
            for (int t=0; t < lines.Count; t++)
            {
                if (lines[t].creditOpenLine != 0)
                    cntrl_sum = cntrl_sum + Convert.ToDecimal(lines[t].creditOpenLine);
                else
                    if (lines[t].debitOpenLine != 0)
                        cntrl_sum = cntrl_sum + Convert.ToDecimal(lines[t].debitOpenLine);
            }
            MakeInvoice mk = new MakeInvoice();  // razne procedure

               try
            {  //string aaa = "'version="1.0'" +  encoding="UTF-8" '";
                XmlDocument bank = new XmlDocument();
                 //  urn:iso:std:iso:20022:tech:xsd:pain.001.001.03" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance

                string xml = @"<?xml version=""1.0"" encoding=""UTF-8""?><Document xmlns=""urn:iso:std:iso:20022:tech:xsd:pain.001.001.03"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""></Document>";

                try
                {
                    bank.LoadXml(xml);

                    // bank.DocumentElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                    // bank.DocumentElement.SetAttribute("xmlns", "urn:iso:std:iso:20022:tech:xsd:pain.008.001.02");
                    XmlElement CstmrCdtTrfInitn = bank.CreateElement("CstmrCdtTrfInitn", bank.DocumentElement.NamespaceURI);
                    XmlElement GrpHdr = bank.CreateElement("GrpHdr", bank.DocumentElement.NamespaceURI);
                    XmlElement MsgId = bank.CreateElement("MsgId", bank.DocumentElement.NamespaceURI);
                  //  XmlElement NbOfTxs = bank.CreateElement("NbOfTxs", bank.DocumentElement.NamespaceURI);
                    MsgId.InnerXml = acc_settings.myIban + "-" + Bui_sepa_nr.ToString(); 
                    GrpHdr.AppendChild(MsgId);

                    XmlElement CreDtTm = bank.CreateElement("CreDtTm", bank.DocumentElement.NamespaceURI);
                    CreDtTm.InnerXml = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");//"2016-03-18T14:21:56"; // datum sepe
                    GrpHdr.AppendChild(CreDtTm);

                    XmlElement NbOfTxs = bank.CreateElement("NbOfTxs", bank.DocumentElement.NamespaceURI);
                    NbOfTxs.InnerXml = no_items.ToString();                //   // broj stavki
                    GrpHdr.AppendChild(NbOfTxs);

                    XmlElement CtrlSum = bank.CreateElement("CtrlSum", bank.DocumentElement.NamespaceURI);
                    CtrlSum.InnerXml = cntrl_sum.ToString().Replace(",", ".");  // kontrolni iznos
                    GrpHdr.AppendChild(CtrlSum);

                    XmlElement InitgPty = bank.CreateElement("InitgPty", bank.DocumentElement.NamespaceURI);
                    GrpHdr.AppendChild(InitgPty);

                    XmlElement Nm = bank.CreateElement("Nm", bank.DocumentElement.NamespaceURI);
                    Nm.InnerXml = "Stichting Buitenhof";
                    InitgPty.AppendChild(Nm);
                    
                    CstmrCdtTrfInitn.AppendChild(GrpHdr);

                    XmlElement PmtInf = bank.CreateElement("PmtInf", bank.DocumentElement.NamespaceURI);
                    CstmrCdtTrfInitn.AppendChild(PmtInf);
                   
                    XmlElement PmtInfId = bank.CreateElement("PmtInfId", bank.DocumentElement.NamespaceURI);
                    PmtInfId.InnerXml = Bui_sepa_nr.ToString();       //"Payment 2016";
                    PmtInf.AppendChild(PmtInfId);

                    XmlElement PmtMtd = bank.CreateElement("PmtMtd", bank.DocumentElement.NamespaceURI);
                    PmtMtd.InnerXml = "TRF";
                    PmtInf.AppendChild(PmtMtd);

                    //XmlElement BtchBookg = bank.CreateElement("BtchBookg", bank.DocumentElement.NamespaceURI);
                    //BtchBookg.InnerXml = "true";
                    //PmtInf.AppendChild(BtchBookg);


                    XmlElement NbOfTxs1 = bank.CreateElement("NbOfTxs", bank.DocumentElement.NamespaceURI);
                    NbOfTxs1.InnerXml = no_items.ToString();  //no_items.ToString(); 
                    PmtInf.AppendChild(NbOfTxs1);

                    XmlElement CtrlSum1 = bank.CreateElement("CtrlSum", bank.DocumentElement.NamespaceURI);
                    CtrlSum1.InnerXml = cntrl_sum.ToString().Replace(",", "."); 
                    PmtInf.AppendChild(CtrlSum1);

                    XmlElement PmtTpInf = bank.CreateElement("PmtTpInf", bank.DocumentElement.NamespaceURI);
                    PmtInf.AppendChild(PmtTpInf);

                    XmlElement SvcLvl = bank.CreateElement("SvcLvl", bank.DocumentElement.NamespaceURI);
                    PmtTpInf.AppendChild(SvcLvl);

                    XmlElement Cd = bank.CreateElement("Cd", bank.DocumentElement.NamespaceURI);
                    Cd.InnerXml = "SEPA";
                    SvcLvl.AppendChild(Cd);

                  //  XmlElement LclInstrm = bank.CreateElement("LclInstrm", bank.DocumentElement.NamespaceURI);
                //    PmtTpInf.AppendChild(LclInstrm);

                    //XmlElement Cd1 = bank.CreateElement("Cd", bank.DocumentElement.NamespaceURI);
                    //Cd1.InnerXml = "CORE";
                    //LclInstrm.AppendChild(Cd1);

                    //XmlElement SeqTp = bank.CreateElement("SeqTp", bank.DocumentElement.NamespaceURI);
                    //SeqTp.InnerXml = "FRST";
                    //PmtTpInf.AppendChild(SeqTp);

                    XmlElement ReqdExctnDt = bank.CreateElement("ReqdExctnDt", bank.DocumentElement.NamespaceURI);
                    ReqdExctnDt.InnerXml = dpDateBook.Value.ToString("yyyy-MM-dd");  // DateTime.Now.ToString("yyyy-MM-dd");
                    PmtInf.AppendChild(ReqdExctnDt);

                    //===========================
                    XmlElement Dbtr = bank.CreateElement("Dbtr", bank.DocumentElement.NamespaceURI);
                    PmtInf.AppendChild(Dbtr);

                    XmlElement Nm1 = bank.CreateElement("Nm", bank.DocumentElement.NamespaceURI);
                    Nm1.InnerXml = "Stichting Buitenhof";
                    Dbtr.AppendChild(Nm1);

                    XmlElement DbtrAcct = bank.CreateElement("DbtrAcct", bank.DocumentElement.NamespaceURI);
                    PmtInf.AppendChild(DbtrAcct);

                    XmlElement Id = bank.CreateElement("Id", bank.DocumentElement.NamespaceURI);
                    DbtrAcct.AppendChild(Id);

                    XmlElement Iban = bank.CreateElement("IBAN", bank.DocumentElement.NamespaceURI);
                    Iban.InnerXml = acc_settings.myIban; //"NL53INGB0669160938"; // acm[i].referencePay.ToString().Trim();
                    Id.AppendChild(Iban);

                    //XmlElement Ccy = bank.CreateElement("Ccy", bank.DocumentElement.NamespaceURI);
                    //Ccy.InnerXml="EUR";
                    //DbtrAcct.AppendChild(Ccy);

                    XmlElement DbtrAgt = bank.CreateElement("DbtrAgt", bank.DocumentElement.NamespaceURI);
                    PmtInf.AppendChild(DbtrAgt);

                    XmlElement FinInstnId = bank.CreateElement("FinInstnId", bank.DocumentElement.NamespaceURI);
                    DbtrAgt.AppendChild(FinInstnId);

                    XmlElement BIC = bank.CreateElement("BIC", bank.DocumentElement.NamespaceURI);
                    BIC.InnerXml = acc_settings.myBic;             //"INGBNL2A";
                    FinInstnId.AppendChild(BIC);

                    XmlElement ChrgBr = bank.CreateElement("ChrgBr", bank.DocumentElement.NamespaceURI);
                    ChrgBr.InnerXml = "SLEV";
                    PmtInf.AppendChild(ChrgBr);

                    //===============================                      

                    decimal amount = 0;
                    // lines
                    for (int i = 0; i < lines.Count; i++)
                    {
                        //if (lines[i].iselected == true)
                        //{
                        if (lines[i].creditOpenLine != 0)
                            amount = Convert.ToDecimal(lines[i].creditOpenLine);
                        else
                            break;  //amount = Convert.ToDecimal(lines[i].debitOpenLine);
                            

                               
                            XmlElement CdtTrfTxInf = bank.CreateElement("CdtTrfTxInf", bank.DocumentElement.NamespaceURI);
                            PmtInf.AppendChild(CdtTrfTxInf);

                            XmlElement PmtId = bank.CreateElement("PmtId", bank.DocumentElement.NamespaceURI);
                            CdtTrfTxInf.AppendChild(PmtId);

                            XmlElement EndToEndId = bank.CreateElement("EndToEndId", bank.DocumentElement.NamespaceURI);
                            EndToEndId.InnerXml = lines[i].referencePay.ToString().Trim();
                            PmtId.AppendChild(EndToEndId);
                           
                            XmlElement Amt = bank.CreateElement("Amt", bank.DocumentElement.NamespaceURI);
                            CdtTrfTxInf.AppendChild(Amt);  //Amt

                            XmlElement InstdAmt = bank.CreateElement("InstdAmt", bank.DocumentElement.NamespaceURI);
                            InstdAmt.SetAttribute("Ccy", "EUR");
                            InstdAmt.InnerXml = amount.ToString().Replace(",", ".");
                            Amt.AppendChild(InstdAmt);

                            //XmlElement ChrgBr = bank.CreateElement("ChrgBr", bank.DocumentElement.NamespaceURI);
                            //ChrgBr.InnerXml = "SLEV";
                            ////PmtInf.AppendChild(ChrgBr);
                            //CdtTrfTxInf.AppendChild(ChrgBr);

                            XmlElement CdtrAgt = bank.CreateElement("CdtrAgt", bank.DocumentElement.NamespaceURI);
                            CdtTrfTxInf.AppendChild(CdtrAgt);

                            XmlElement FinInstnId1 = bank.CreateElement("FinInstnId", bank.DocumentElement.NamespaceURI);
                            CdtrAgt.AppendChild(FinInstnId1);

                            XmlElement Othr = bank.CreateElement("Othr", bank.DocumentElement.NamespaceURI);
                            Othr.InnerXml = "";
                            FinInstnId1.AppendChild(Othr);

                            //=============== NOTPROVIDED
                            XmlElement Id1 = bank.CreateElement("Id", bank.DocumentElement.NamespaceURI);
                            Id1.InnerXml = "NOTPROVIDED";
                            Othr.AppendChild(Id1);
                            //===============

                            XmlElement Cdtr = bank.CreateElement("Cdtr", bank.DocumentElement.NamespaceURI);
                            CdtTrfTxInf.AppendChild(Cdtr);

                            XmlElement Nm2 = bank.CreateElement("Nm", bank.DocumentElement.NamespaceURI);
                            Nm2.InnerXml = mk.GiveCustomerName(lines[i].idDebCre).ToString().Trim();
                            Cdtr.AppendChild(Nm2);

                            XmlElement CdtrAcct = bank.CreateElement("CdtrAcct", bank.DocumentElement.NamespaceURI);
                            CdtTrfTxInf.AppendChild(CdtrAcct);

                            XmlElement Id2 = bank.CreateElement("Id", bank.DocumentElement.NamespaceURI);
                            CdtrAcct.AppendChild(Id2);

                            XmlElement Iban2 = bank.CreateElement("IBAN", bank.DocumentElement.NamespaceURI);
                            Iban2.InnerXml = lines[i].iban;
                            Id2.AppendChild(Iban2);
                        //===
                            XmlElement RmtInf = bank.CreateElement("RmtInf", bank.DocumentElement.NamespaceURI);
                            CdtTrfTxInf.AppendChild(RmtInf);

                            XmlElement Ustrd = bank.CreateElement("Ustrd", bank.DocumentElement.NamespaceURI);
                            Ustrd.InnerXml = lines[i].invoiceOpenLine.ToString().Trim();
                            RmtInf.AppendChild(Ustrd);


                            amount = 0;

                            bank.DocumentElement.AppendChild(CstmrCdtTrfInitn);
                            //  xTextBox.SetAttribute("Name", "TextBox" + i);
                       // }



                    }

                    //   bankS.DocumentElement = new XmlElement();
                  
                   // real_name = txtNamefile.Text + dpDateBook.Value.ToShortDateString();
                    if (acc_settings.sepaPath != "")
                        name_file = acc_settings.sepaPath + "\\" + real_name.Trim() + ".xml";
                    else
                        name_file = AppDomain.CurrentDomain.BaseDirectory + "\\" + real_name.Trim() + ".xml";
                    bank.Save(name_file);

                    ////=================== kreira novu sepicu za istoriju ========================
                    // XmlDocument bankS = new XmlDocument();
                    // string xml1 = @"<?xml version=""1.0"" encoding=""UTF-8""?><Document xmlns=""urn:iso:std:iso:20022:tech:xsd:pain.001.001.03"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""></Document>";

                    ////try
                    ////{
                    //    bankS.LoadXml(xml1);

                    //    // bank.DocumentElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                    //    // bank.DocumentElement.SetAttribute("xmlns", "urn:iso:std:iso:20022:tech:xsd:pain.008.001.02");
                    //    XmlElement Lines = bankS.CreateElement("Lines", bankS.DocumentElement.NamespaceURI);


                    //    for (int i = 0; i < lines.Count; i++)
                    //    {
                    //        //if (lines[i].iselected == true)
                    //        //{
                    //        if (lines[i].creditOpenLine != 0)
                    //            amount = Convert.ToDecimal(lines[i].creditOpenLine);
                    //        else
                    //            amount = Convert.ToDecimal(lines[i].debitOpenLine);


                    //        //XmlElement Dbtr = bankS.CreateElement("Dbtr", bankS.DocumentElement.NamespaceURI);
                    //        //Lines.AppendChild(Dbtr);

                    //        //XmlElement Nm1 = bankS.CreateElement("Nm", bankS.DocumentElement.NamespaceURI);
                    //        //Nm1.InnerXml = "Stichting Buitenhof";
                    //        //Dbtr.AppendChild(Nm1);

                    //        //XmlElement DbtrAcct = bankS.CreateElement("DbtrAcct", bankS.DocumentElement.NamespaceURI);
                    //        //Lines.AppendChild(DbtrAcct);

                    //        //XmlElement Id = bankS.CreateElement("Id", bankS.DocumentElement.NamespaceURI);
                    //        //DbtrAcct.AppendChild(Id);

                    //        //XmlElement Iban = bankS.CreateElement("IBAN", bankS.DocumentElement.NamespaceURI);
                    //        //Iban.InnerXml = acc_settings.myIban; //"NL53INGB0669160938"; // acm[i].referencePay.ToString().Trim();
                    //        //Id.AppendChild(Iban);

                    //        ////XmlElement Ccy = bankS.CreateElement("Ccy", bankS.DocumentElement.NamespaceURI);
                    //        ////Ccy.InnerXml="EUR";
                    //        ////DbtrAcct.AppendChild(Ccy);

                    //        //XmlElement DbtrAgt = bankS.CreateElement("DbtrAgt", bankS.DocumentElement.NamespaceURI);
                    //        //Lines.AppendChild(DbtrAgt);

                    //        //XmlElement FinInstnId = bankS.CreateElement("FinInstnId", bankS.DocumentElement.NamespaceURI);
                    //        //DbtrAgt.AppendChild(FinInstnId);

                    //        //XmlElement BIC = bankS.CreateElement("BIC", bankS.DocumentElement.NamespaceURI);
                    //        //BIC.InnerXml = acc_settings.myBic;             //"INGBNL2A";
                    //        //FinInstnId.AppendChild(BIC);

                    //        //XmlElement ChrgBr = bankS.CreateElement("ChrgBr", bankS.DocumentElement.NamespaceURI);
                    //        //ChrgBr.InnerXml = "SLEV";
                    //        //Lines.AppendChild(ChrgBr);

                    //        //===============================                           
                    //        XmlElement Creditor = bankS.CreateElement("Creditor", bankS.DocumentElement.NamespaceURI);
                    //        Lines.AppendChild(Creditor);

                    //        //XmlElement PmtId = bankS.CreateElement("PmtId", bankS.DocumentElement.NamespaceURI);
                    //        //Creditor.AppendChild(PmtId);

                    //        XmlElement Reference = bankS.CreateElement("Reference", bankS.DocumentElement.NamespaceURI);
                    //        Reference.InnerXml = lines[i].referencePay.ToString().Trim();
                    //        Creditor.AppendChild(Reference);

                    //        //XmlElement Amt = bankS.CreateElement("Amt", bankS.DocumentElement.NamespaceURI);
                    //        //Creditor.AppendChild(Amt);  //Amt

                    //        XmlElement Amount = bankS.CreateElement("Amount", bankS.DocumentElement.NamespaceURI);
                    //       // InstdAmt.SetAttribute("Ccy", "EUR");
                    //        Amount.InnerXml = amount.ToString().Replace(",", ".");
                    //        Creditor.AppendChild(Amount);

                    //        //XmlElement ChrgBr = bankS.CreateElement("ChrgBr", bankS.DocumentElement.NamespaceURI);
                    //        //ChrgBr.InnerXml = "SLEV";
                    //        ////PmtInf.AppendChild(ChrgBr);
                    //        //CdtTrfTxInf.AppendChild(ChrgBr);

                    //        //XmlElement CdtrAgt = bankS.CreateElement("CdtrAgt", bankS.DocumentElement.NamespaceURI);
                    //        //CdtTrfTxInf.AppendChild(CdtrAgt);

                    //        //XmlElement FinInstnId1 = bankS.CreateElement("FinInstnId", bankS.DocumentElement.NamespaceURI);
                    //        //CdtrAgt.AppendChild(FinInstnId1);

                    //        //XmlElement Othr = bankS.CreateElement("Othr", bankS.DocumentElement.NamespaceURI);
                    //        //Othr.InnerXml = "";
                    //        //FinInstnId1.AppendChild(Othr);

                    //        ////=============== NOTPROVIDED
                    //        //XmlElement Id1 = bankS.CreateElement("Id", bankS.DocumentElement.NamespaceURI);
                    //        //Id1.InnerXml = "NOTPROVIDED";
                    //        //Othr.AppendChild(Id1);
                    //        ////===============

                    //        //XmlElement Cdtr = bankS.CreateElement("Cdtr", bankS.DocumentElement.NamespaceURI);
                    //        //CdtTrfTxInf.AppendChild(Cdtr);

                    //        XmlElement Nm2 = bankS.CreateElement("Name", bankS.DocumentElement.NamespaceURI);
                    //        Nm2.InnerXml = mk.GiveCustomerName(lines[i].idDebCre).ToString().Trim();
                    //        Creditor.AppendChild(Nm2);

                    //        //XmlElement CdtrAcct = bankS.CreateElement("CdtrAcct", bankS.DocumentElement.NamespaceURI);
                    //        //Creditor.AppendChild(CdtrAcct);

                    //        //XmlElement Id2 = bankS.CreateElement("Id", bankS.DocumentElement.NamespaceURI);
                    //        //Creditor.AppendChild(Id2);

                    //        XmlElement Iban2 = bankS.CreateElement("IBAN", bankS.DocumentElement.NamespaceURI);
                    //        Iban2.InnerXml = lines[i].iban;
                    //        Creditor.AppendChild(Iban2);
                    //        //===
                    //        //XmlElement RmtInf = bankS.CreateElement("RmtInf", bankS.DocumentElement.NamespaceURI);
                    //        //Creditor.AppendChild(RmtInf);

                    //        XmlElement Invoice = bankS.CreateElement("Invoice", bankS.DocumentElement.NamespaceURI);
                    //        Invoice.InnerXml = lines[i].invoiceOpenLine.ToString().Trim();
                    //        Creditor.AppendChild(Invoice);


                    //       // amount = 0;
                    //        bankS.DocumentElement.AppendChild(Lines);
                    //    }
                        
                    ////}
                    ////catch
                    ////{

                    ////}
                    //    string name_file111 = "";
                    // name_file111 = "BU" + dpDateBook.Value.ToShortDateString()+ "Sepica";
                    //if (acc_settings.sepaPath != "")
                    //    name_file111 = acc_settings.sepaPath + "\\" + name_file111.Trim() + ".xml";
                    //else
                    //    name_file111 = AppDomain.CurrentDomain.BaseDirectory + "\\" + name_file111.Trim() + ".xml";
                    //bankS.Save(name_file111);
//===========================================================================



                    //================ change status sepa =================
                    AccSepaModel sm = new AccSepaModel();
                    AccSepaBUS sb = new AccSepaBUS();
                    bool isOk = false;
                    selectedRowGrid.status = 3;
                    selectedRowGrid.amountSepa = cntrl_sum;
                    isOk = sb.Update(selectedRowGrid, frmname, Login._user.idUser);
                        if (isOk == false)
                        {
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {
                                if (resxSet.GetString("Error updating Sepa amount") != null)
                                    RadMessageBox.Show(resxSet.GetString("Error updating Sepa amount"));
                                else
                                    RadMessageBox.Show("Error updating Sepa amount");
                            }
                            return;
                        }
                    


                    //====================================================
                    RadMessageBox.Show("Finish");
                    AccSepaBUS acb = new AccSepaBUS();
                    sepa_model = new List<AccSepaModel>();
                    sepa_model = acb.GetAllSepaFinal();
                    gridProgress.DataSource = null;
                    gridProgLines.DataSource = null;
                    gridProgress.DataSource = sepa_model;
                    btnBook.Enabled = false;
                    txtDaily.Visible = false;
                    btnDaily.Visible = false;
                    btnConfirmXml.Enabled = false;


                }
                catch (Exception e)
                {

                }
                
                
            }
            catch (Exception e)
            {

        }
    
            
        }

        private void MasterTemplate_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridSummaryCellElement)
            {
                if (!String.IsNullOrEmpty(e.CellElement.Text))
                {
                    e.CellElement.BorderLeftWidth = e.CellElement.BorderRightWidth = 1;
                    e.CellElement.Font = new Font("Verdana", 9, FontStyle.Bold);  //Segoe UI
                    e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                }
                else
                {
                    e.CellElement.ResetValue(LightVisualElement.BorderLeftWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
                    e.CellElement.ResetValue(LightVisualElement.BorderRightWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
                }
            }
        }
        bool isokvalc = false;
        private void MasterTemplate_ValueChanged(object sender, EventArgs e)
        {
                 string aname = gridBank.CurrentCell.ColumnInfo.Name;

                 if (this.gridBank.ActiveEditor is RadCheckBoxEditor && aname == "selected")
                 {
                     if (isokvalc == false)
                     {
                         isokvalc = true;
                         AccOpenLinesModel mod = (AccOpenLinesModel)gridBank.CurrentRow.DataBoundItem;
                         decimal valueM = (decimal)mod.debitOpenLine - (decimal)mod.creditOpenLine;
                         //int id = (int)gridLookup.CurrentRow.Cells["selected"].Value;
                         bool chechstate = Convert.ToBoolean(gridBank.ActiveEditor.Value);
                         if (chechstate == true)
                         {
                             gridBank.CurrentRow.Cells["amount"].Value = Math.Abs(Convert.ToDecimal(gridBank.CurrentRow.Cells["debit"].Value) - Convert.ToDecimal(gridBank.CurrentRow.Cells["credit"].Value));
                             
                         }
                         else
                         {
                             gridBank.CurrentRow.Cells["amount"].Value = Convert.ToDecimal("0,00");
                             gridBank.CurrentRow.Cells["difference"].Value = Convert.ToDecimal("0,00");
                             gridBank.CurrentRow.Cells["bookacc"].Value = "";
                             
                         }

                     isokvalc = false;
                     }
                 }
                 if (aname == "amount")
                     gridBank.CurrentRow.Cells["difference"].Value = Math.Abs((Convert.ToDecimal(gridBank.CurrentRow.Cells["debit"].Value) - Convert.ToDecimal(gridBank.CurrentRow.Cells["credit"].Value))) - Convert.ToDecimal(gridBank.CurrentRow.Cells["amount"].Value);
                   
                 //if (aname == "difference")
                 //    RadMessageBox.Show("difference");
        }

        private void MasterTemplate_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (e.Column.Name == "bookacc")
            {
                _gridEditor = this.gridBank.ActiveEditor as BaseGridEditor;
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown += new KeyEventHandler(cellEditorAccount_KeyDown);
                }
            }
         
        }

        private void MasterTemplate_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            if (e.Column.Name == "bookacc")
            {
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    element.KeyDown -= cellEditorAccount_KeyDown;
                }
                _gridEditor = null;
            }
            if (e.Column.Name == "amount")
            {
                if (e.Value != null)
                {
                    decimal d = (decimal)e.Value;
                    if (d != 0)
                    {
                        GridViewRowInfo info = e.Row.ViewInfo.CurrentRow;

                        info.Cells["difference"].Value =  (Convert.ToDecimal(gridBank.CurrentRow.Cells["debit"].Value) - Convert.ToDecimal(gridBank.CurrentRow.Cells["credit"].Value)) - Convert.ToDecimal(gridBank.CurrentRow.Cells["amount"].Value); 
                    }
                }
            }
     
        }

        void cellEditorAccount_KeyDown(object sender, KeyEventArgs e)
        {
            RadTextBoxEditorElement editor = sender as RadTextBoxEditorElement;

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
                            editor.Text = genmX.numberLedgerAccount;
                    }
                }
            }
            if (e.KeyCode == Keys.Enter && e.KeyCode == Keys.Tab)
            {

                if (editor.Text != "")
                {
                    LedgerAccountBUS ledbus = new LedgerAccountBUS(Login._bookyear);
                    LedgerAccountModel lam = new LedgerAccountModel();

                    lam = ledbus.GetAccount(editor.Text, Login._bookyear);
                    if (lam == null)
                    {
                        RadMessageBox.Show("Wrong account");
                        editor.Text = "";
                    }
                    
                }

            }



        }
       
        private void gridBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.gridBank.CurrentCell != null)
            
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    string aa = this.gridBank.CurrentCell.ColumnInfo.Name;
                    
                    
                    if (e.KeyCode == Keys.Enter && aa == "bookacc")
                    {
                        RadMessageBox.Show("account for booking");
                    }
                    else
                    {
                        if (e.KeyCode == Keys.Enter && aa == "amount")
                        {
                            RadMessageBox.Show("amount");
                        }
                        //else
                        //{
                        //    if (e.KeyCode == Keys.Enter && aa == "difference")
                        //    {
                        //        RadMessageBox.Show("diff");
                        //    }
                        //}
                    }
                }
            }
        }

        private void createBook()
        {
            //AccDailyBUS ad = new AccDailyBUS();
            //List<AccDailyModel> am = new List<AccDailyModel>();

            //am = ad.GetBookingDailysInkop2();
            //if (am != null)
            //{
            //    xDaily = am[0].idDaily;
            //}
            //========== otvara novi izabrani memo ===================
            AccDailyMemBUS mbus = new AccDailyMemBUS(Login._bookyear);
            AccDailyMemModel mm = new AccDailyMemModel();
            int broj = 0;
            bool isOk = false;
            int xCurr = 0;   // za povezivanje stavki sa zaglavljem mema
            mm = mbus.GetLastMemByStatement(xDailyMem.ToString());
            if (mm != null)
            {
                if (mm.refNo == null && mm.refNo == 0)
                    mm.refNo = 0;
                broj = mm.refNo + 1;
                AccDailyMemModel newm = new AccDailyMemModel();
                newm.codeDaily = mm.codeDaily;
                newm.refNo = broj;
                newm.dtMem = dpDateBook.Value;
                newm.bookingYear = Login._bookyear;
                
                xCurr = mbus.SaveAndReturnID(newm, frmname, Login._user.idUser);
                if (xCurr < 1)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Error making new memo!") != null)
                            RadMessageBox.Show(resxSet.GetString("Error making new memo!"));
                        else
                            RadMessageBox.Show("Error making new memo!");
                    }
                    return;
                }
            }
            else
            {
                broj =  1;
                AccDailyMemModel newm = new AccDailyMemModel();
                newm.codeDaily = xDailyMem.ToString();
                newm.refNo = broj;
                newm.dtMem = dpDateBook.Value;
                newm.bookingYear = Login._bookyear;

                xCurr = mbus.SaveAndReturnID(newm, frmname, Login._user.idUser);
                if (xCurr < 1)

                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Error making new memo!") != null)
                            RadMessageBox.Show(resxSet.GetString("Error making new memo!"));
                        else
                            RadMessageBox.Show("Error making new memo!");
                    }
                    return;
                }
            }



            AccAcountUpdate mk = new AccAcountUpdate();
            AccLineBUS aclb = new AccLineBUS(Login._bookyear);
            List<AccLineModel> multimodel = new List<AccLineModel>();
            decimal grid_debit = 0;
            decimal grid_credit = 0;
            decimal grid_diff = 0;
            decimal grid_amount = 0;
            string grid_account = "";
            decimal total_sepa = 0;

            AccLineModel accline = new AccLineModel();

            if (gridProgLines != null)
            {
                if (gridProgLines.RowCount > 0)
                {
                    for (int i = 0; i < gridProgLines.RowCount; i++)
                    {

                        //if (Convert.ToBoolean(gridProgLines.Rows[i].Cells["selected"].Value) == true)
                        //{
                            grid_debit = Convert.ToDecimal(gridProgLines.Rows[i].Cells["debitOpenline"].Value);
                            grid_credit = Convert.ToDecimal(gridProgLines.Rows[i].Cells["creditOpenline"].Value);
                          //  grid_amount = Convert.ToDecimal(gridProgLines.Rows[i].Cells["amount"].Value);
                           // grid_diff = Convert.ToDecimal(gridBank.Rows[i].Cells["difference"].Value);

                            //============== prva stavka ================================================================
                            accline = new AccLineModel();
                            accline.idAccDaily = xDailyMem;
                            accline.dtLine = dpDateBook.Value;           //Convert.ToDateTime(lines[w].dtLine);
                            accline.idClientLine = gridProgLines.Rows[i].Cells["idDebCre"].Value.ToString();
                            accline.numberLedAccount = gridProgLines.Rows[i].Cells["account"].Value.ToString();
                            accline.invoiceNr = gridProgLines.Rows[i].Cells["invoiceOpenLine"].Value.ToString();
                            accline.incopNr = "";
                            accline.periodLine = mk.Period(dpDateBook.Value);
                            accline.idBTW = 0;
                            accline.descLine = gridProgLines.Rows[i].Cells["descOpenLine"].Value.ToString();
                            accline.idCostLine = gridProgLines.Rows[i].Cells["codeCost"].Value.ToString();
                            accline.idProjectLine = gridProgLines.Rows[i].Cells["idProject"].Value.ToString();
                            accline.dtBooking = dpDateBook.Value;
                            accline.idCurrency = xCurr;
                            if (grid_debit - grid_credit < 0)
                            {
                                accline.debitLine = grid_credit;   //grid_debit
                                total_sepa = total_sepa + grid_credit;   //grid_debit
                            }
                            else
                            {
                                accline.creditLine = grid_debit;    //grid_credit
                                total_sepa = total_sepa - grid_debit;
                            }
                            accline.iban = gridProgLines.Rows[i].Cells["iban"].Value.ToString();
                            accline.booksort = 1;
                            accline.incopNr = "";  // gridBank.Rows[i].Cells["client"].Value.ToString() + gridBank.Rows[i].Cells["invoice"].Value.ToString().Trim();
                            accline.idSepa = sepa_nr;

                            multimodel.Add(accline);

                            
                       // }
                    }
                    if (total_sepa != 0)
                    {
                        //=============== druga stavka ===================================================================
                        accline = new AccLineModel();
                        accline.idAccDaily = xDailyMem;
                        accline.dtLine = dpDateBook.Value;           //Convert.ToDateTime(lines[w].dtLine);
                        accline.idClientLine = "";
                        //===================================== ovde ubaciti novi account za sepu ========================
                        if (acc_settings != null)
                        {
                            if (acc_settings.defSepaAcc != null && acc_settings.defSepaAcc.Trim() != "")
                                accline.numberLedAccount = acc_settings.defSepaAcc.Trim();
                            else
                                accline.numberLedAccount = "1610";
                        }
                        else
                            accline.numberLedAccount = "1610";
                        accline.invoiceNr = sepa_nr.ToString(); 
                        accline.incopNr = "";
                        accline.periodLine = mk.Period(dpDateBook.Value);
                        accline.idBTW = 0;
                        accline.descLine = sepa_name.Trim();  // newline.Substring(46, newline.Length - 46);
                        accline.idCostLine = "";
                        accline.idProjectLine = "";
                        accline.dtBooking = dpDateBook.Value;
                        accline.idCurrency = xCurr;
                        if (total_sepa < 0)
                            accline.debitLine = Math.Abs(total_sepa);
                        else
                            accline.creditLine = total_sepa;
                        accline.iban = "";//acc_settings.myIban.Trim();
                        accline.booksort = 1;
                        accline.incopNr = "";
                        accline.bookingYear = Login._bookyear;
                        accline.idSepa = sepa_nr;
                        multimodel.Add(accline);


                        // === upis 
                        bool isOkwrite = false;
                        for (int q = 0; q < multimodel.Count; q++)
                        {
                            isOk = mk.AddAmount(multimodel[q], this.Name, Login._user.idUser);
                            if (isOk == false)
                            {
                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                {
                                    if (resxSet.GetString("Error updating account!") != null)
                                        RadMessageBox.Show(resxSet.GetString("Error updating account!"));
                                    else
                                        RadMessageBox.Show("Error updating account!");
                                }
                                return;
                            }
                            isOkwrite = aclb.Save(multimodel[q], this.Name, Login._user.idUser);
                            if (isOkwrite == false)
                            {
                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                {
                                    if (resxSet.GetString("Error writing line!") != null)
                                        RadMessageBox.Show(resxSet.GetString("Error writing line!"));
                                    else
                                        RadMessageBox.Show("Error writing line!");
                                }
                                return;
                            }
                           
                         
                        }
                          using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                {
                                    if (resxSet.GetString("Finish") != null)
                                        RadMessageBox.Show(resxSet.GetString("Finish"));
                                    else
                                        RadMessageBox.Show("Finish");
                                }
                         //==================== update sepa to history===
                          AccSepaModel sm = new AccSepaModel();
                          AccSepaBUS sb = new AccSepaBUS();
                          bool llOk = false;
                          selectedRowGrid.status = 5;
                        //  selectedRowGrid.amountSepa = cntrl_sum;
                          llOk = sb.Update(selectedRowGrid, frmname, Login._user.idUser);
                          if (llOk == false)
                          {
                              using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                              {
                                  if (resxSet.GetString("Error updating Sepa status") != null)
                                      RadMessageBox.Show(resxSet.GetString("Error updating Sepa status"));
                                  else
                                      RadMessageBox.Show("Error updating Sepa status");
                              }
                              return;
                          }



                          //====================================================
                          RadMessageBox.Show("Finish");
                          AccSepaBUS acb = new AccSepaBUS();
                          sepa_model = new List<AccSepaModel>();
                          sepa_model = acb.GetAllSepaXml();
                          gridProgress.DataSource = null;
                          gridProgLines.DataSource = null;
                          gridProgress.DataSource = sepa_model;
                          //===============================================
                    }
                }

            }
                    
                
           




           // stavka 1   iznos koji se uplacuje  amount to pay  - credit
         //    gridBank.CurrentRow.Cells["amount"].Value.

            // stavka 2 iznos cele fakture    debit
        //     Math.Abs( gridBank.CurrentRow.Cells["debit"].Value.  -  gridBank.CurrentRow.Cells["credit"].Value.)
            // stavka 3 iznos popusta ili razlika  - credit na keji konto
        //     gridBank.CurrentRow.Cells["diff"].Value.          Math.Abs( gridBank.CurrentRow.Cells["debit"].Value.  -  gridBank.CurrentRow.Cells["credit"].Value.) -     gridBank.CurrentRow.Cells["amount"].Value.


        }

        private void btnDaily_Click(object sender, EventArgs e)
        {
            AccDailyBUS ccentar1 = new AccDailyBUS(Login._bookyear);
            List<IModel> gmX1 = new List<IModel>();

            gmX1 = ccentar1.GetBookingDailysMemo();
            var dlgSave1 = new GridLookupForm(gmX1, "Daily");

            if (dlgSave1.ShowDialog(this) == DialogResult.Yes)
            {
                AccDailyModel genmX1 = new AccDailyModel();
                genmX1 = (AccDailyModel)dlgSave1.selectedRow;
                //set textbox
                if (genmX1 != null)
                {
                    txtDaily.Text = genmX1.codeDaily + " " + genmX1.descDaily;
                    xDailyMem = genmX1.idDaily;
                }
             
            }
        }


           //grid_debit = Convert.ToDecimal(gridBank.Rows[i].Cells["debit"].Value);
           //             grid_credit = Convert.ToDecimal(gridBank.Rows[i].Cells["credit"].Value);
           //             grid_amount = Convert.ToDecimal(gridBank.Rows[i].Cells["amount"].Value);
           //             grid_diff = Convert.ToDecimal(gridBank.Rows[i].Cells["difference"].Value);
           //             if (gridBank.Rows[i].Cells["bookacc"].Value != null)
           //                 grid_account = gridBank.Rows[i].Cells["bookacc"].Value.ToString();
           //             else
           //             {

           //             }
           //             if (grid_diff != 0)
           //             {

           //             }
           //             else
           //             {
           //                 ============== prva stavka ================================================================
           //                 accline.idAccDaily = xDailyMem;
           //                 accline.dtLine = dpDateBook.Value;           //Convert.ToDateTime(lines[w].dtLine);
           //                 accline.idClientLine = gridBank.Rows[i].Cells["client"].Value.ToString();
           //                 accline.numberLedAccount = gridBank.Rows[i].Cells["account"].Value.ToString();
           //                 accline.invoiceNr = gridBank.Rows[i].Cells["invoice"].Value.ToString();
           //                 accline.incopNr = "";
           //                 accline.periodLine = mk.Period(dpDateBook.Value);
           //                 accline.idBTW = 0;
           //                 accline.descLine = gridBank.Rows[i].Cells["description"].Value.ToString();
           //                 accline.idCostLine = gridBank.Rows[i].Cells["cost"].Value.ToString();
           //                 accline.idProjectLine = gridBank.Rows[i].Cells["project"].Value.ToString();
           //                 accline.dtBooking = dpDateBook.Value;
           //                 accline.idCurrency = xCurr;
           //                 if (grid_debit - grid_credit < 0)
           //                     accline.creditLine = grid_amount;
           //                 else
           //                     accline.debitLine = grid_amount;

           //                 accline.iban = gridBank.Rows[i].Cells["iban"].Value.ToString();
           //                 accline.booksort = 1;
           //                 accline.incopNr = gridBank.Rows[i].Cells["client"].Value.ToString() + gridBank.Rows[i].Cells["invoice"].Value.ToString().Trim();

           //                 multimodel.Add(accline);
           //                 =============== druga stavka ===============================================================
           //                 accline = new AccLineModel();
           //                 accline.idAccDaily = xDaily;
           //                 accline.dtLine = dpDateBook.Value;           //Convert.ToDateTime(lines[w].dtLine);
           //                 accline.idClientLine = gridBank.Rows[i].Cells["client"].Value.ToString();
           //                 accline.numberLedAccount = gridBank.Rows[i].Cells["account"].Value.ToString();
           //                 accline.invoiceNr = gridBank.Rows[i].Cells["invoice"].Value.ToString();
           //                 accline.incopNr = "";
           //                 accline.periodLine = mk.Period(dpDateBook.Value);
           //                 accline.idBTW = 0;
           //                 accline.descLine = gridBank.Rows[i].Cells["description"].Value.ToString();
           //                 accline.idCostLine = gridBank.Rows[i].Cells["cost"].Value.ToString();
           //                 accline.idProjectLine = gridBank.Rows[i].Cells["project"].Value.ToString();
           //                 accline.dtBooking = dpDateBook.Value;
           //                 if (grid_debit - grid_credit < 0)
           //                     accline.debitLine = Math.Abs(grid_debit - grid_credit);
           //                 else
           //                     accline.creditLine = Math.Abs(grid_debit - grid_credit);
           //                 accline.iban = gridBank.CurrentRow.Cells["iban"].Value.ToString();
           //                 accline.booksort = 2;
           //                 accline.incopNr = gridBank.Rows[i].Cells["client"].Value.ToString() + gridBank.Rows[i].Cells["invoice"].Value.ToString().Trim();
           //                 multimodel.Add(accline);
           //                 =============== treca stavka ================================================================
           //                 accline = new AccLineModel();
           //                 accline.idAccDaily = xDaily;
           //                 accline.dtLine = dpDateBook.Value;           //Convert.ToDateTime(lines[w].dtLine);
           //                 accline.idClientLine = gridBank.Rows[i].Cells["client"].Value.ToString();
           //                 accline.numberLedAccount = grid_account;
           //                 accline.invoiceNr = gridBank.Rows[i].Cells["invoice"].Value.ToString();
           //                 accline.incopNr = "";
           //                 accline.periodLine = mk.Period(dpDateBook.Value);
           //                 accline.idBTW = 0;
           //                 accline.descLine = gridBank.Rows[i].Cells["description"].Value.ToString();
           //                 accline.idCostLine = gridBank.Rows[i].Cells["cost"].Value.ToString();
           //                 accline.idProjectLine = gridBank.Rows[i].Cells["project"].Value.ToString();
           //                 accline.dtBooking = dpDateBook.Value;
           //                 if (grid_debit - grid_credit < 0)
           //                     accline.creditLine = grid_diff;
           //                 else
           //                     accline.debitLine = grid_diff;
           //                 accline.iban = gridBank.Rows[i].Cells["iban"].Value.ToString();
           //                 accline.booksort = 3;
           //                 accline.incopNr = gridBank.Rows[i].Cells["client"].Value.ToString() + gridBank.Rows[i].Cells["invoice"].Value.ToString().Trim();
           //                 multimodel.Add(accline);

           //                  === upis 
           //                 bool isOkwrite = false;
           //                 for (int q = 0; q < multimodel.Count; q++)
           //                 {
           //                     isOkwrite = aclb.Save(multimodel[q]);
           //                     if (isOkwrite == false)
           //                     {
           //                         using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
           //                         {
           //                             if (resxSet.GetString("Error writing line!") != null)
           //                                 RadMessageBox.Show(resxSet.GetString("Error writing line!"));
           //                             else
           //                                 RadMessageBox.Show("Error writing line!");
           //                         }
           //                         return;
           //                     }
           //                 }
           //             }

        private void makeReport()
        {

        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                string folderica = fbd.SelectedPath;
                txtFolder.Text = folderica;
            }
            btnUpdate.Focus();
        }

        private void txtIban_Leave(object sender, EventArgs e)
        {
            MakeInvoice au = new MakeInvoice();
            bool aa = false;
            if (txtIban.Text != "")
            {
                aa = au.ValidateIban(txtIban.Text.Trim());
                if (aa == false)
                {
                    RadMessageBox.Show("Wrong IBAN !!!");
                    txtIban.Focus();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            AccSettingsBUS asb = new AccSettingsBUS();
            if (acc_settings != null)
            {
                acc_settings.myIban = txtIban.Text;
                acc_settings.myBic = txtBic.Text;
                acc_settings.sepaPath = txtFolder.Text;
                bool isOk = false;
                isOk = asb.Update(acc_settings, this.Name, Login._user.idUser);
                if (isOk == false)
                    RadMessageBox.Show("Error updating data !!!");
                else
                    RadMessageBox.Show("Updated");

            }
            txtIban.Focus();
        }

        private void txtIban_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtBic.Focus();
              
            }

        }

        private void txtBic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                btnFolder.Focus();

            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtIban.Focus();

            }
        }

        private void rbFinal_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            //btnBook.Visible = false;
            //btnApprove.Visible = false;
            //btnPrint.Visible = true;
            //btnRemove.Visible = false;
            //btnAdd.Visible = false;
            AccSepaBUS acb = new AccSepaBUS();
            sepa_model = new List<AccSepaModel>();
            sepa_model = null;
            sepa_model = acb.GetAllSepaFinal();
            gridProgress.DataSource = null;
            gridProgLines.DataSource = null;
            gridProgress.DataSource = sepa_model;
            btnBook.Enabled = false;
            txtDaily.Visible = false;
            btnDaily.Visible = false;
            btnConfirmXml.Enabled = false;

        }

        private void rbProgress_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            //btnBook.Visible = true;
            //btnApprove.Visible = true;
            //btnPrint.Visible = true;
            //btnRemove.Visible = true;
            //btnAdd.Visible = true;

            AccSepaBUS acb = new AccSepaBUS();
             
            sepa_model = new List<AccSepaModel>();
            sepa_model = null;
            sepa_model = acb.GetAllSepaProgress();
            gridProgress.DataSource = null;
            gridProgLines.DataSource = null;
            gridProgress.DataSource = sepa_model;
            btnBook.Enabled = false;
            txtDaily.Visible = false;
            btnDaily.Visible = false;
            btnConfirmXml.Enabled = false;
        }
        private void rbXml_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            AccSepaBUS acb = new AccSepaBUS();
            sepa_model = new List<AccSepaModel>();
            sepa_model = null;
            sepa_model = acb.GetAllSepaXml();
            gridProgress.DataSource = null;
            gridProgLines.DataSource = null;
            gridProgress.DataSource = sepa_model;
            btnBook.Enabled = true;
            txtDaily.Visible = true;
            btnDaily.Visible = true;
            btnConfirmXml.Enabled = true;
        }

        private void gridHistory_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < gridHistory.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (gridHistory.Columns[i].HeaderText != null && resxSet.GetString(gridHistory.Columns[i].HeaderText) != null)
                        gridHistory.Columns[i].HeaderText = resxSet.GetString(gridHistory.Columns[i].HeaderText);
                }
            }
            if (gridHistory.ColumnCount > 0)
            {

                // gridBank.Columns["idCreditPay"].IsVisible = false;
            
            }
            if (File.Exists(layoutHistory))
            {
                gridHistory.LoadLayout(layoutHistory);
            }
            if (gridHistory.Columns != null && gridHistory.Columns.Count > 0)
                gridHistory.Columns["dtSepa"].FormatString = "{0: dd/MM/yyyy}";
            if (gridHistory.Columns != null && gridHistory.Columns.Count > 0)
                gridHistory.Columns["dtCreationDate"].FormatString = "{0: dd/MM/yyyy}";
            if (gridHistory.Columns != null && gridHistory.Columns.Count > 0)
                gridHistory.Columns["dtApprove"].FormatString = "{0: dd/MM/yyyy}";

            if (gridHistory.Columns != null && gridHistory.Columns.Count > 0)
                gridHistory.Columns["amountSepa"].FormatString = "{0:N2}";
        }

        private void gridHisLines_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < gridHisLines.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (gridHisLines.Columns[i].HeaderText != null && resxSet.GetString(gridHisLines.Columns[i].HeaderText) != null)
                        gridHisLines.Columns[i].HeaderText = resxSet.GetString(gridHisLines.Columns[i].HeaderText);
                }
            }
            if (gridHisLines.ColumnCount > 0)
            {

                // gridBank.Columns["idCreditPay"].IsVisible = false;
            }
            if (File.Exists(layoutHisLines))
            {
                gridHisLines.LoadLayout(layoutHisLines);
            }
            //if (gridHisLines.Columns != null && gridHisLines.Columns.Count > 0)
            //    gridHisLines.Columns["dtOpenLine"].FormatString = "{0: dd/MM/yyyy}";
            //if (gridHisLines.Columns != null && gridHisLines.Columns.Count > 0)
            //    gridHisLines.Columns["dtPayOpenLine"].FormatString = "{0: dd/MM/yyyy}";
            //if (gridHisLines.Columns != null && gridHisLines.Columns.Count > 0)
            //    gridHisLines.Columns["dtCreationLine"].FormatString = "{0: dd/MM/yyyy}";


            //if (gridHisLines.Columns != null && gridHisLines.Columns.Count > 0)
            //    gridHisLines.Columns["debitOpenLine"].FormatString = "{0:N2}";
            //if (gridHisLines.Columns != null && gridHisLines.Columns.Count > 0)
            //    gridHisLines.Columns["creditOpenLine"].FormatString = "{0:N2}";

        }

        private void gridHistory_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
          
                if (gridHistory.CurrentRow != null)
                {
                    GridViewRowInfo info = this.gridHistory.CurrentRow;
                    selectedRowGrid = (AccSepaModel)info.DataBoundItem; //AccCreditPayModel
                    if (selectedRowGrid != null)
                    {
                        
                        linesHistory = new List<AccOpenLinesModel>();
                        linesHistory = new AccOpenLinesBUS().GetAccOpenLinesSepa(selectedRowGrid.idSepa);

                        //DataSet xmlDataSet = new DataSet();
                        //xmlDataSet.ReadXml("D:\\A1\\BU15-5-2016Sepica.xml");
                        //this.gridHisLines.DataSource = xmlDataSet.Tables[0];

                        gridHisLines.DataSource = null;
                        gridHisLines.DataSource = linesHistory;
                      
                    }
                }
            
        }

        private void gridHistory_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
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
        }
        private void SaveLayout(object sender, EventArgs e)
        {
            if (File.Exists(layoutHistory))
            {
                File.Delete(layoutHistory);
            }
            gridHistory.SaveLayout(layoutHistory);
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("You have successfully save layout!") != null)
                    RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
                else
                    RadMessageBox.Show("You have successfully save layout!");
            }
        }

        private void gridHisLines_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            string saveLayout = "Save Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem = new RadMenuItem();
            customMenuItem.Text = saveLayout;
            customMenuItem.Click += SaveLayout1;
            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);
        }
        private void SaveLayout1(object sender, EventArgs e)
        {
            if (File.Exists(layoutHisLines))
            {
                File.Delete(layoutHisLines);
            }
            gridHisLines.SaveLayout(layoutHisLines);
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("You have successfully save layout!") != null)
                    RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
                else
                    RadMessageBox.Show("You have successfully save layout!");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (gridHisLines != null)
            {
                if (gridHisLines.SelectedRows != null)
                {
                    DialogResult dr = RadMessageBox.Show("Do you want to REMOVE this line ?" , "Delete", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        int idLine = Convert.ToInt32(gridHisLines.CurrentRow.Index);
                        lines[idLine].idSepa = 0;
                        AccOpenLinesBUS abs = new AccOpenLinesBUS();
                        bool oki = false;
                        oki = abs.Update(lines[idLine], this.Name, Login._user.idUser);
                        if (oki == false)
                        {
                            RadMessageBox.Show("Error updating line");
                            return;
                        }
                        else
                        {
                            lines = new List<AccOpenLinesModel>();
                            lines = new AccOpenLinesBUS().GetAccOpenLinesSepa(selectedRowGrid.idSepa);
                            gridHisLines.DataSource = null;
                            gridHisLines.DataSource = lines;
                        }

                    }
                }
            }
        }

        private void gridProgress_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < gridProgress.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (gridProgress.Columns[i].HeaderText != null && resxSet.GetString(gridProgress.Columns[i].HeaderText) != null)
                        gridProgress.Columns[i].HeaderText = resxSet.GetString(gridProgress.Columns[i].HeaderText);
                }
            }
            if (gridProgress.ColumnCount > 0)
            {

                // gridBank.Columns["idCreditPay"].IsVisible = false;

            }
            if (File.Exists(layoutProgres))
            {
                gridProgress.LoadLayout(layoutProgres);
            }
            try
            {
                if (gridProgress.Columns != null && gridProgress.Columns.Count > 0)
                    gridProgress.Columns["dtSepa"].FormatString = "{0: dd/MM/yyyy}";
                if (gridProgress.Columns != null && gridProgress.Columns.Count > 0)
                    gridProgress.Columns["amountSepa"].FormatString = "{0:N2}";

                if (gridProgress.Columns != null && gridProgress.Columns.Count > 0)
                    gridProgress.Columns["dtApprove"].FormatString = "{0: dd/MM/yyyy}";
                if (gridProgress.Columns != null && gridProgress.Columns.Count > 0)
                    gridProgress.Columns["dtCreationDate"].FormatString = "{0: dd/MM/yyyy}";
            }
            catch(Exception ex)
            {
                translateRadMessageBox msg = new translateRadMessageBox();
                msg.translateAllMessageBox("Error formating grid");
            }

        }

        private void gridProgLines_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < gridProgLines.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (gridProgLines.Columns[i].HeaderText != null && resxSet.GetString(gridProgLines.Columns[i].HeaderText) != null)
                        gridProgLines.Columns[i].HeaderText = resxSet.GetString(gridProgLines.Columns[i].HeaderText);
                }
            }
            if (gridProgLines.ColumnCount > 0)
            {

                // gridBank.Columns["idCreditPay"].IsVisible = false;

            }
            if (File.Exists(layoutProgLines))
            {
                gridProgLines.LoadLayout(layoutProgLines);
            }
            if (gridProgLines.Columns != null && gridProgLines.Columns.Count > 0)
                gridProgLines.Columns["dtOpenLine"].FormatString = "{0: dd/MM/yyyy}";
            if (gridProgLines.Columns != null && gridProgLines.Columns.Count > 0)
                gridProgLines.Columns["dtPayOpenLine"].FormatString = "{0: dd/MM/yyyy}";
            if (gridProgLines.Columns != null && gridProgLines.Columns.Count > 0)
                gridProgLines.Columns["dtCreationLine"].FormatString = "{0: dd/MM/yyyy}";


            if (gridProgLines.Columns != null && gridProgLines.Columns.Count > 0)
                gridProgLines.Columns["debitOpenLine"].FormatString = "{0:N2}";
            if (gridProgLines.Columns != null && gridProgLines.Columns.Count > 0)
                gridProgLines.Columns["creditOpenLine"].FormatString = "{0:N2}";
        }

        private void btnXml_Click(object sender, EventArgs e)
        {
                 DialogResult dr = RadMessageBox.Show("Do you want to make XML file ?" , "Delete", MessageBoxButtons.YesNo);
                 if (dr == DialogResult.Yes)
                 {
                     makeXml();
                 }
        }
        private void btnBooking_Click(object sender, EventArgs e)
        {
         
        

        }
        private void btnApprove_Click(object sender, EventArgs e)
        {

        }
        private void pvSepa_SelectedPageChanged(object sender, EventArgs e)
        {
               RadPageView rpv = (RadPageView)sender;
               string sName = ((RadPageView)sender).SelectedPage.Name;

            switch (sName)
            {
                case "pgSettings":
                    ArrNrBUS arnb = new ArrNrBUS();
                    ArrNrModel arnm = new ArrNrModel();
                    arnm=arnb.GetSepaNoIncrement();
                    if (arnm != null)
                        txtCounter.Text = arnm.nrSEPA.ToString();

                    break;

                case "pgHistory":
                       // btnBook.Visible = false;
                        //btnFinal.Visible = false;
                        //btnPrint.Visible = true;
                        //btnRemove.Visible = false;
                        //btnAdd.Visible = false;
                        AccSepaBUS acb = new AccSepaBUS();
                        sepa_hist = new List<AccSepaModel>();
                        gridHisLines.DataSource = null; 
                        sepa_hist = acb.GetAllSepaHistory();
                        gridHistory.DataSource = null;
                        gridHistory.DataSource = sepa_hist;
                                            
                    break;
                case "pgInProgerss":
                        // btnBook.Visible = true;
                        //btnFinal.Visible = true;
                        //btnPrint.Visible = true;
                        //btnRemove.Visible = true;
                        //btnAdd.Visible = true;
                        rbProgress.CheckState = CheckState.Checked;
                        AccSepaBUS acb1 = new AccSepaBUS();
                        sepa_model = new List<AccSepaModel>();
                        sepa_model = acb1.GetAllSepaProgress();
                        gridProgress.DataSource = null;
                        gridProgress.DataSource = sepa_model;
                    break;
                case "pgNewSepa":


                    break;
            }

        }

      
        private void gridProgress_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {

            if (gridProgress.CurrentRow != null)
                {
                    GridViewRowInfo info = this.gridProgress.CurrentRow;
                    selectedRowGrid = (AccSepaModel)info.DataBoundItem; //AccCreditPayModel
                    if (selectedRowGrid != null)
                    {

                        lines = new List<AccOpenLinesModel>();
                        lines = new AccOpenLinesBUS().GetAccOpenLinesSepa(selectedRowGrid.idSepa);
                        if (lines != null && lines.Count > 0)
                        {
                            for (int r= 0; r< lines.Count;r++)
                            {
                                lines[r].iselected = true;
                            }
                        }
                        gridProgLines.DataSource = null;
                        gridProgLines.DataSource = lines;
                    //=== buttons enable/disable ==============
                        if (selectedRowGrid.status == 2)
                        {
                            btnApprove.Enabled = false;
                           // btnXml.Enabled = true;
                        }
                        else
                            if (selectedRowGrid.status < 2)
                                btnApprove.Enabled = true;
                            else
                                btnApprove.Enabled = false;

                        if (selectedRowGrid.approveUser > 0)
                            btnXml.Enabled = true;
                        else
                            btnXml.Enabled = false;

                        if (selectedRowGrid.status >= 4)
                        {
                            btnBook.Enabled = true;
                            txtDaily.Visible = true;
                            btnDaily.Visible = true;
                            btnApprove.Enabled = false;
                        }
                        else
                        {
                            btnBook.Enabled = false;
                            txtDaily.Visible = false;
                            btnDaily.Visible = false;
                         
                        }

                    //====================================

                    }
                }
           
        }

        private void btnApprove_Click_1(object sender, EventArgs e)
        {
            AccOpenLinesBUS opls = new AccOpenLinesBUS();

                 DialogResult dr = RadMessageBox.Show("Do you want to Approve this SEPA ?" , "Delete", MessageBoxButtons.YesNo);
                 if (dr == DialogResult.Yes)
                 {
                     sepa_nr=selectedRowGrid.idSepa;

                     for (int y = 0; y < lines.Count; y++)
                     {
                         if (lines[y].iselected == true)
                         {
                             lines[y].idSepa = sepa_nr;
                             bool isUpdate = false;
                             isUpdate = opls.Update(lines[y], this.Name, Login._user.idUser);
                             if (isUpdate == false)
                             {
                                 using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                 {
                                     if (resxSet.GetString("Error Updating open line !!!") != null)
                                         RadMessageBox.Show(resxSet.GetString("Error Updating open line !!!"));
                                     else
                                         RadMessageBox.Show("Error Updating open line !!!");
                                 }
                                 return;
                             }
                         }
                     }

                     AccSepaBUS sbs = new AccSepaBUS();
                     selectedRowGrid.status = 2;
                     selectedRowGrid.approveUser = Login._user.idUser;
                     selectedRowGrid.dtApprove = DateTime.Now;
                     bool isups = false;
                     isups = sbs.Update(selectedRowGrid, frmname, Login._user.idUser);
                     if (isups == false)
                     {
                         using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                         {
                             if (resxSet.GetString("Error Updating Sepa status !!!") != null)
                                 RadMessageBox.Show(resxSet.GetString("Error Updating Sepa status !!!"));
                             else
                                 RadMessageBox.Show("Error Updating Sepa status !!!");
                         }
                         return;
                     }
                     else
                     {
                         using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                         {
                             if (resxSet.GetString("Updated") != null)
                                 RadMessageBox.Show(resxSet.GetString("Updated"));
                             else
                                 RadMessageBox.Show("Updated");
                         }
                         //AccSepaBUS acb = new AccSepaBUS();
                         //sepa_model = new List<AccSepaModel>();
                         //sepa_model = acb.GetAllSepaFinal();
                         //gridProgress.DataSource = null;
                         //gridProgLines.DataSource = null;
                         //gridProgress.DataSource = sepa_model;
                         rbFinal.CheckState = CheckState.Checked;
                     }


                 }
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            if (selectedRowGrid.status < 4)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Confirm Xml first, please") != null)
                        RadMessageBox.Show(resxSet.GetString("Confirm Xml first, please"));
                    else
                        RadMessageBox.Show("Confirm Xml first, please");
                }
                return;
            }
            DialogResult dr = RadMessageBox.Show("Do you want to Book this SEPA ?" , "Delete", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                if (txtDaily.Text == "")
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Select Daily, please") != null)
                            RadMessageBox.Show(resxSet.GetString("Select Daily, please"));
                        else
                            RadMessageBox.Show("Select Daily, please");
                    }
                    btnDaily.PerformClick();
                    return;
                }
                AccCreditPayBUS cpb = new AccCreditPayBUS();
                olinemodel = new AccOpenLinesModel();
                AccOpenLinesBUS opbus = new AccOpenLinesBUS();
                model = new List<AccOpenLinesModel>();
                List<AccDailyModel> adm = new List<AccDailyModel>();
                adm = new AccDailyBUS(Login._bookyear).GetBookingDailysInkop2();
                if (adm != null)
                {
                    xDaily = adm[0].idDaily;
                    xConto = adm[0].numberLedgerAccount;
                }
                bool llok = false;
                no_items = 0;
                cntrl_sum = 0;
                decimal sum = 0;
                int id = 0;
                decimal amount_close = 0;


                if (gridProgLines.Rows.Count > 0)
                {

                    for (int i = 0; i < gridProgLines.Rows.Count; i++)
                    {

                        //if (Convert.ToBoolean(gridProgLines.Rows[i].Cells["selected"].Value) == true)
                        //{
                        no_items = no_items + 1;

                        amount_close = Convert.ToDecimal(gridProgLines.Rows[i].Cells["creditOpenLine"].Value);
                        sum = sum + amount_close;

                        olinemodel = new AccOpenLinesModel();
                        // olinemodel = opbus.GetAccOpenLinesByInvoice(acm[i].invoiceOpenLine);  // trazi otvorenu stavku
                        olinemodel = opbus.GetAccOpenLinesByInvoice(gridProgLines.Rows[i].Cells["invoiceOpenLine"].Value.ToString(), lines[i].term); // GetAccOpenLinesByInvoiceClient
                        if (olinemodel != null)
                        {
                            if (olinemodel.idDebCre == gridProgLines.Rows[i].Cells["idDebCre"].Value.ToString())
                            {
                                olinemodel.dtPayOpenLine = dpDateBook.Value;  //Convert.ToDateTime(gridBank.Rows[i].Cells["date"].Value);
                                if (Convert.ToDecimal(gridProgLines.Rows[i].Cells["debitOpenLine"].Value) - Convert.ToDecimal(gridProgLines.Rows[i].Cells["creditOpenLine"].Value) < 0)
                                    olinemodel.debitOpenLine = olinemodel.debitOpenLine + amount_close;
                                else
                                    olinemodel.creditOpenLine = olinemodel.creditOpenLine + amount_close;

                                olinemodel.idOption = lines[i].idOption;
                                // olinemodel.term = acm[i].term;
                                olinemodel.iban = gridProgLines.Rows[i].Cells["iban"].Value.ToString();
                                olinemodel.dtCreationLine = DateTime.Now;
                            }
                            else
                            {
                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                {
                                    if (resxSet.GetString("No open line!") != null)
                                        RadMessageBox.Show(resxSet.GetString("No open line!"));
                                    else
                                        RadMessageBox.Show("No open line!");
                                }
                                return;
                            }

                        }
                        else
                        {
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {
                                if (resxSet.GetString("No open line!") != null)
                                    RadMessageBox.Show(resxSet.GetString("No open line!"));
                                else
                                    RadMessageBox.Show("No open line!");
                            }
                            return;
                        }

                        llok = opbus.Update(olinemodel, this.Name, Login._user.idUser);
                        if (llok == false)
                        {
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {
                                if (resxSet.GetString("Error updating open line!") != null)
                                    RadMessageBox.Show(resxSet.GetString("Error updating open line!"));
                                else
                                    RadMessageBox.Show("Error updating open line!");
                            }
                            return;
                        }
                    }
                    //}


                    // }

                    cntrl_sum = sum;

                    opline = new AccOpenLinesModel();
                    opline.account = defaultSepaAcc;
                    opline.idDebCre = "";
                    opline.invoiceOpenLine = sepa_nr.ToString();
                    opline.creditOpenLine = sum;
                    sepa_name = "SEPA " + real_name;
                    opline.descOpenLine = "SEPA " + real_name;
                    opline.typeOpenLine = "C";
                    opline.dtOpenLine = dpDateBook.Value;
                    opline.dtCreationLine = DateTime.Now;
                    Bui_sepa_nr = sepa_nr;
                    //=== ovara novu sepu u tabeli AccSepa===//
                    bool isSepaOk = false;
                    AccSepaBUS asb = new AccSepaBUS();
                    AccSepaModel asm = new AccSepaModel();
                    asm = new AccSepaModel();

                    //asm.dtSepa = dpDateBook.Value;
                    //asm.idSepa = sepa_nr;
                    //asm.amountSepa = sum;
                    //asm.nameSepa = sepa_name;
                    //isSepaOk = asb.Save(asm);
                    //if (isSepaOk == false)
                    //{
                    //    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    //    {
                    //        if (resxSet.GetString("Error making Sepa header!") != null)
                    //            RadMessageBox.Show(resxSet.GetString("Error making Sepa header!"));
                    //        else
                    //            RadMessageBox.Show("Error making Sepa header!");
                    //    }
                    //    return;
                    //}
                    //======================================//
                    opbus.Save(opline, this.Name, Login._user.idUser);

                    createBook();
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("No lines for booking!") != null)
                            RadMessageBox.Show(resxSet.GetString("No lines for booking!"));
                        else
                            RadMessageBox.Show("No lines for booking!");
                    }
                }
            }
        }

        private void btnPrintProgress_Click(object sender, EventArgs e)
        {
            AccSepaDAO rsb = new AccSepaDAO();
            AccOpenLinesDAO rop = new AccOpenLinesDAO();
            heder = new DataTable();
            items = new DataTable();
            string name = "Sepa.pdf";
            
            if (selectedRowGrid != null)
            {
                heder = rsb.GetSepaById(selectedRowGrid.idSepa);
                if (heder != null)
                {
                    if (heder.Columns.Count > 0)
                    {
                        items = rop.GetAccOpenLinesSepa(selectedRowGrid.idSepa);
                    }
                    frmSepaReport frf = new frmSepaReport(heder, items, name);
                    frf.Show();
 
                }
            }
            else
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Nothing to print!") != null)
                        RadMessageBox.Show(resxSet.GetString("Nothing to print!"));
                    else
                        RadMessageBox.Show("Nothing to print!");
                }
                return;
            }
        }

        private void btnAddToSEPA_Click(object sender, EventArgs e)
        {
             DialogResult dr = RadMessageBox.Show("Add to existing SEPA ?" , "Delete", MessageBoxButtons.YesNo);
             if (dr == DialogResult.Yes)
             {
                 if (gridBank.Rows.Count == 0)
                 {
                     using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                     {
                         if (resxSet.GetString("Nothing to add!") != null)
                             RadMessageBox.Show(resxSet.GetString("Nothing to add!"));
                         else
                             RadMessageBox.Show("Nothing to add!");
                     }
                     return;
                 }
                 AccSepaBUS asbb = new AccSepaBUS();
                 List<AccSepaModel> asmm = new List<AccSepaModel>();
                 asmm = asbb.GetAllSepaProgress();
                 if (asmm == null)
                 {
                     using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                     {
                         if (resxSet.GetString("No existing Sepa in progress") != null)
                             RadMessageBox.Show(resxSet.GetString("No existing Sepa in progress"));
                         else
                             RadMessageBox.Show("No existing Sepa in progress");
                     }
                     return;
                 }
                 else
                 {
                     SepaCombo ic1 = new SepaCombo();
                     ic1.idSepa = 0;
                     listaCombo.Add(ic1);
                     if (asmm.Count > 0)
                     {
                         for (int q = 0; q < asmm.Count; q++)
                         {
                             SepaCombo ic = new SepaCombo();
                             ic.idSepa = asmm[q].idSepa;
                             listaCombo.Add(ic);

                         }
                     }

                     ddlSepa.DataSource = listaCombo;
                     ddlSepa.ValueMember = "idSepa";
                     ddlSepa.DisplayMember = "idSepa";
                     ddlSepa.Visible = true;
                     ddlSepa.SelectedIndex = 0;
                 //    listaCombo.Clear();

                     //for (int q = 0; q < ddlInvoices.Items.Count; q++)
                     //{
                     //       ddlInvoices.Items.Remove(dataItem);

                     //}

                     prolaz = 1;

                 
                 }
             }
        }

        private void ddlSepa_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (prolaz == 1)
            {
                int selectSepa = 0;

                selectSepa = Convert.ToInt32(ddlSepa.SelectedValue);
                if (selectSepa != null)
                {
                    if (selectSepa == 0)
                    {
                        translateRadMessageBox trmb = new translateRadMessageBox();
                        trmb.translateAllMessageBox("Cannot add to SEPA 0  !!!");
                        return;
                    }
                }
                AccOpenLinesBUS ob = new AccOpenLinesBUS();
                AccSepaBUS sb = new AccSepaBUS();
                AccSepaModel sm = new AccSepaModel();
                DialogResult dr = RadMessageBox.Show("Add to SEPA " + selectSepa, "Delete", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    if (acm != null)
                        if (acm.Count > 0)
                        {
                            for (int w = 0; w < acm.Count; w++)
                            {
                                if (acm[w].iselected == true)
                                {
                                    acm[w].idSepa = selectSepa;
                                    bool isOk = false;
                                    isOk = ob.Update(acm[w], this.Name, Login._user.idUser);
                                    if (isOk == false)
                                    {
                                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                        {
                                            if (resxSet.GetString("Error updating line to sepa") != null)
                                                RadMessageBox.Show(resxSet.GetString("Error updating line to sepa"));
                                            else
                                                RadMessageBox.Show("Error updating line to sepa");
                                        }
                                        return;
                                    }
                                    else
                                    {
                                        sm = sb.GetSepaById(selectSepa);
                                        if (sm != null)
                                        {
                                            sm.amountSepa = sm.amountSepa + Convert.ToDecimal(acm[w].creditOpenLine);
                                            isOk = false;
                                            isOk = sb.Update(sm, frmname, Login._user.idUser);
                                            if (isOk == false)
                                            {
                                                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                                {
                                                    if (resxSet.GetString("Error updating Sepa amount") != null)
                                                        RadMessageBox.Show(resxSet.GetString("Error updating Sepa amount"));
                                                    else
                                                        RadMessageBox.Show("Error updating Sepa amount");
                                                }
                                                return;
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    gridBank.DataSource = null;
                }
            }
        }

        private void btnConfirmXml_Click(object sender, EventArgs e)
        {
            if (selectedRowGrid != null)
            {
                if (selectedRowGrid.status < 3)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Can't confirm this Sepa !") != null)
                            RadMessageBox.Show(resxSet.GetString("Can't confirm this Sepa !"));
                        else
                            RadMessageBox.Show("Can't confirm this Sepa !");
                    }
                    return;
                }
                else
                {
                    selectedRowGrid.status = 4;
                    AccSepaBUS nsb = new AccSepaBUS();
                    bool llok = false;
                    llok = nsb.Update(selectedRowGrid, frmname, Login._user.idUser);
                    gridProgress.DataSource = null;
                    gridProgress.DataSource = sepa_model;
                    if (llok==false)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("Error confirming  Sepa !") != null)
                                RadMessageBox.Show(resxSet.GetString("Error confirming  Sepa !"));
                            else
                                RadMessageBox.Show("Error confirming  Sepa !");
                        }
                        return;
                    }
                    else
                    {
                        
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("Finish") != null)
                                RadMessageBox.Show(resxSet.GetString("Finish"));
                            else
                                RadMessageBox.Show("Finish");
                        }
                    }
                }
            }
        }

        private void gridProgress_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {

        }
        private void SaveLayout5(object sender, EventArgs e)
        {
            if (File.Exists(layoutProgres))
            {
                File.Delete(layoutProgres);
            }
            gridProgLines.SaveLayout(layoutProgres);
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("You have successfully save layout!") != null)
                    RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
                else
                    RadMessageBox.Show("You have successfully save layout!");
            }

        }

        private void gridProgLines_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            string saveLayout = "Save Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem = new RadMenuItem();
            customMenuItem.Text = saveLayout;
            customMenuItem.Click += SaveLayout2;
            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);
        }
        private void SaveLayout2(object sender, EventArgs e)
        {
            if (File.Exists(layoutProgLines))
            {
                File.Delete(layoutProgLines);
            }
            gridProgLines.SaveLayout(layoutProgLines);
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("You have successfully save layout!") != null)
                    RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
                else
                    RadMessageBox.Show("You have successfully save layout!");
            }
        
        }

        private void gridProgLines_UserDeletingRow(object sender, GridViewRowCancelEventArgs e)
        {
             DialogResult dr = RadMessageBox.Show("Remove item from SEPA ?" , "Delete", MessageBoxButtons.YesNo);
             if (dr == DialogResult.Yes)
             {

                 if (rbProgress.CheckState == CheckState.Unchecked)
                 {
                     using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                     {
                         if (resxSet.GetString("Remove ONLY in status Progress !") != null)
                             RadMessageBox.Show(resxSet.GetString("Remove ONLY in status Progress !"));
                         else
                             RadMessageBox.Show("Remove ONLY in status Progress !");
                     }
                     e.Cancel = true;
                     return;
                 }


                 if (gridProgLines.CurrentRow.DataBoundItem != null)
                 {
                     AccOpenLinesBUS aclineBUS = new AccOpenLinesBUS();
                     List<AccOpenLinesModel> accLineModel = new List<AccOpenLinesModel>();

                     AccOpenLinesModel selectedLine = new AccOpenLinesModel();
                     selectedLine = (AccOpenLinesModel)gridProgLines.CurrentRow.DataBoundItem;

                     //int idDel = selectedLine.idOpenLine;
                     selectedLine.idSepa = 0;
                     bool llok = false;
                     llok = aclineBUS.Update(selectedLine, this.Name, Login._user.idUser);
                     if (llok == true)
                     {
                          AccSepaModel selectedSepagrid = new AccSepaModel();
                          selectedSepagrid = (AccSepaModel)gridProgress.CurrentRow.DataBoundItem;
                          AccSepaBUS sb = new AccSepaBUS();
                          AccSepaModel sm = new AccSepaModel();

                          sm = sb.GetSepaById(selectedSepagrid.idSepa);
                          if (sm != null)
                          {
                              sm.amountSepa = sm.amountSepa - Convert.ToDecimal(selectedLine.creditOpenLine);
                              bool isOk = false;
                              isOk = false;
                              isOk = sb.Update(sm, frmname, Login._user.idUser);
                              if (isOk == false)
                              {
                                  using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                                  {
                                      if (resxSet.GetString("Error updating Sepa amount") != null)
                                          RadMessageBox.Show(resxSet.GetString("Error updating Sepa amount"));
                                      else
                                          RadMessageBox.Show("Error updating Sepa amount");
                                  }
                                  return;
                              }
                              AccSepaBUS acb = new AccSepaBUS();
                              sepa_model = new List<AccSepaModel>();
                              sepa_model = acb.GetAllSepaProgress();
                              gridProgress.DataSource = null;
                              gridProgress.DataSource = sepa_model;
                          }

                     }


                       //  AccAcountUpdate asl = new AccAcountUpdate();
                         var itemdel = lines.Find(item => item.idOpenLine == selectedLine.idOpenLine);
                         lines.Remove(itemdel);
                       
                             gridProgLines.DataSource = null;
                             gridProgLines.DataSource = lines;

                      
                 }

             }
             else
             {
                 e.Cancel = true;
                 return;
             }

                 
        }

        private void gridProgLines_UserDeletedRow(object sender, GridViewRowEventArgs e)
        {
               

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("Removed !") != null)
                    RadMessageBox.Show(resxSet.GetString("Removed !"));
                else
                    RadMessageBox.Show("Removed !");
            }
        }

      

       
    }
}
