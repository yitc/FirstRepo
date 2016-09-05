using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI;
using BIS.Model;
using BIS.Business;
using BIS.DAO;
using BIS.Core;
using System.IO;
using System.Resources;
using Telerik.WinControls;
using System.Windows.Forms;
using System.Xml;

namespace GUI
{
    public partial class ImportGmu : Telerik.WinControls.UI.RadForm
    {
        private string sDest;
        private string sStart;
        private string importname;
        private string fullname;
        private string newline;
        private string addline;
        private string heder;
        private List<string> items;
        private bool first = true;
        private string sumaperson;
        private string firstline;
        private bool prvi = true;
        private BankHederLinesBUS bus;
        private BankHederLinesDAO dao;
        private BankHederModel hedermodel;
        private BankLinesModel linesmodel;
        private int idHeder = 0;
        private string layoutImport;
        private string noStatement;
        private bool isExist = false;
        private string iban;
        private List<bookModel> bubumodel;

        public ImportGmu()
        {
            InitializeComponent();
        }

        private void ImportGmu_Load(object sender, EventArgs e)
        {
            layoutImport = MainForm.gridFiltersFolder + "\\layoutImport.xml";
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            
            string ext = "txt";
            dialog.Filter = "( *." + ext + ")|*." + ext + "|All Files (*.*)|*.*";
            string sDest = System.Reflection.Assembly.GetEntryAssembly().Location;
            string sStart = sDest.Substring(0, sDest.LastIndexOf('\\')) + "\\ForImport\\";
            sDest = sDest.Substring(0, sDest.LastIndexOf('\\')) + "\\Imported\\";
            dialog.InitialDirectory = sStart;
            dialog.Title = "Please select a file";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                importname = dialog.FileName;
                string sFileName = importname.Split('\\')[importname.Split('\\').Length - 1];

                txtPath.Text = sFileName;
                fullname = sDest + txtPath.Text;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            isExist = false;
            hedermodel = new BankHederModel();
            linesmodel = new BankLinesModel();
            bus = new BankHederLinesBUS();
            bool bOpen = true;
            items = new List<string>();
            string[] lines1 = System.IO.File.ReadAllLines(importname);
            foreach (string line in lines1)
            {
                if (line.Substring(0, 4) == ":28:")  //kontrola da li postoji taj izvod
                {
                    heder = line;
                    statement();
                    provera();
                }
            }
            if (isExist == false)
            {
                items = new List<string>();
                string[] lines = System.IO.File.ReadAllLines(importname);

                foreach (string line in lines)
                {
                    if (line.Substring(0, 4) == ":20:")  // broj i datum yymmdd
                    {
                        heder = line;
                        number();
                    }
                    if (line.Substring(0, 4) == ":25:")  // broj racuna
                    {
                        heder = line;
                        account();
                    }
                    if (line.Substring(0, 4) == ":28:")  // broj izvoda
                    {
                        heder = line;
                        statement();
                     
                    }
                    if (line.Substring(0, 5) == ":60F:")  // predhodni iznos
                    {
                        heder = line;
                        amount();
                        saveheder();
                    }
                    if (line.Substring(0, 5) == ":62F:")  // end iznos
                    {
                        heder = line;
                        endsaldo();
                    }
                    if (line.Substring(0, 4) == ":61:")  // stavka
                    {
                        first = true;
                        if (prvi == false)
                        {
                            txtview.Text = firstline + " " + sumaperson + " " + "\r\n";
                            string allinone = firstline + " " + sumaperson + "\r\n";
                            items.Add(allinone);
                        }
                        newline = line;
                        sumaperson = "";
                        person();
                    }
                    if (line.Substring(0, 4) == ":86:" && first == false)   // opis stavke
                    {
                        addline = line;
                        person1();
                    }

                }
            }
            else
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Already imorted") != null)
                        RadMessageBox.Show(resxSet.GetString("Already imorted"));
                    else
                        RadMessageBox.Show("Already imorted");
                }
                return;
            }
          //  txtview.Text = items;
            //try
            //{
            //    if (!System.IO.Directory.Exists(sDest))
            //        System.IO.Directory.CreateDirectory(sDest);

            //    System.IO.File.Copy(importname, fullname, true);
            //    System.IO.File.Delete(importname);
            //}
            //catch (Exception ex)
            //{
            //    bOpen = false;
            //    RadMessageBox.Show("Error copying document.\nMessage: " + ex.Message, "Copy error", MessageBoxButtons.OK, RadMessageIcon.Error);
            //}
            radGridView1.DataSource = null;
            List<BankLinesModel> aaa = new List<BankLinesModel>();
            aaa = bus.GetLinesByHeder(idHeder);
            radGridView1.DataSource = aaa;
            decimal sumaD = 0;
            decimal sumaC = 0;
            decimal tot = 0;
            if (aaa != null)
            {
                for(int i=0; i<aaa.Count; i++)
                {
                    if (aaa[i].debcreLine == "D")
                        sumaD = sumaD + Convert.ToDecimal(aaa[i].amountLine);
                    if (aaa[i].debcreLine == "C")
                        sumaC = sumaC + Convert.ToDecimal(aaa[i].amountLine);
                }
            }
            tot = sumaD - sumaC;
            txtTotal.Value = tot;


        }
        private void number()
        {
            txtNumber.Text = heder.Substring(4, 10);
            string aa = "20" + heder.Substring(8, 2)+"-"+heder.Substring(10,2)+"-"+heder.Substring(12,2);
            hedermodel.entryDate = Convert.ToDateTime(aa);
        }
        private void account()
        {
            txtAccount.Text = heder.Substring(4, 14);
            hedermodel.accountNumber = heder.Substring(4,14);
            iban = heder.Substring(4, 14);
        }
        private void statement()
        {
            txtStatement.Text = heder.Substring(4, 8);
            hedermodel.statementNo = heder.Substring(4, 8);
            noStatement = heder.Substring(4, 8);

        }
        private void amount()
        {
            txtDC.Text = heder.Substring(5, 1);
            hedermodel.debcrePrevius = heder.Substring(5, 1);
            string pp = heder.Substring(15, 15);
            pp = pp.Replace(".", ",");
            txtAmount.Value = Convert.ToDecimal(pp);
            hedermodel.amountPrevius = Convert.ToDecimal(txtAmount.Value);
        }
        private void person()
        {
            linesmodel = new BankLinesModel();
            linesmodel.idBankHeder = idHeder;
            string aa = "20" + newline.Substring(4, 2) + "-" + newline.Substring(6, 2) + "-" + newline.Substring(8, 2);
            linesmodel.valueDate = Convert.ToDateTime(aa);
            linesmodel.debcreLine = newline.Substring(10, 1);
            string pp = newline.Substring(11, 15);
            pp = pp.Replace(".", ",");
            linesmodel.amountLine = Convert.ToDecimal(pp);
            linesmodel.transactType = newline.Substring(26,4);
            linesmodel.accountNo = newline.Substring(30,16);
            linesmodel.payerLine = newline.Substring(46, newline.Length-46);
            bus.SaveLines(linesmodel, this.Name, Login._user.idUser);
            firstline = newline;
            first =false;
            prvi = false;
        }
        private void person1()
        {
            sumaperson = sumaperson + addline;
        }
        private void endsaldo()
        {
            string dc = heder.Substring(5,1);
            hedermodel.debcreEnd = dc;
            string datum = "20"+heder.Substring(6,2)+"-"+heder.Substring(8,2)+"-"+heder.Substring(10,2);
            hedermodel.dateEnd = Convert.ToDateTime(datum);
            string pp = heder.Substring(15, 15);
            pp = pp.Replace(".", ",");
            txtEndsaldo.Value = Convert.ToDecimal(pp);
            hedermodel.amountEnd = Convert.ToDecimal(txtEndsaldo.Value);
            bus.UpdateHeder(hedermodel, this.Name, Login._user.idUser);
            getDaily();
        }

        private void radGridView1_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < radGridView1.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (radGridView1.Columns[i].HeaderText != null && resxSet.GetString(radGridView1.Columns[i].HeaderText) != null)
                        radGridView1.Columns[i].HeaderText = resxSet.GetString(radGridView1.Columns[i].HeaderText);
                }
            }
            if (File.Exists(layoutImport))
            {
                radGridView1.LoadLayout(layoutImport);
            }
          
        }

        private void saveheder()
        {
            bus = new BankHederLinesBUS();
            bus.Save(hedermodel, this.Name, Login._user.idUser);
            dbConnection conect = new dbConnection();
            idHeder = conect.GetLastTableID("BankHeder");
            hedermodel.idBankHeder = idHeder;
        }

        private void radGridView1_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
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
            if (File.Exists(layoutImport))
            {
                File.Delete(layoutImport);
            }
            radGridView1.SaveLayout(layoutImport);
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("You have successfully save layout!") != null)
                    RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
                else
                    RadMessageBox.Show("You have successfully save layout!");
            }
        }
        private void provera()
        {
            bus = new BankHederLinesBUS();
            List<BankHederModel> hmod = new List<BankHederModel>();
            hmod = bus.CheckHeder(noStatement);
            if (hmod != null)
            {
                if (hmod.Count > 0)
                {
                    if (hmod[0].statementNo == noStatement)
                    {
                        isExist = true;
                    }
                }
            }
           
        }
        private void getDaily()
        {
            AccDailyBUS acd = new AccDailyBUS(Login._bookyear);
            AccDailyModel acm = new AccDailyModel();
            acm = acd.GetDailyByIban(iban);
            if (acm != null)
                if (acm.ibanBank == iban)
                {
                    AccDailyBankBUS abb = new AccDailyBankBUS(Login._bookyear);
                    List<AccDailyBankModel> abm = new List<AccDailyBankModel>();
                    abm = abb.GetAllByDaily(acm.codeDaily);
                    if (abm != null)
                        if (abm.Count > 0)
                        {
                            AccDailyBankModel modelBank = new AccDailyBankModel();
                            modelBank.begSaldo = hedermodel.amountPrevius;
                            modelBank.endSaldo = hedermodel.amountEnd;
                            modelBank.refNo = Convert.ToInt32(hedermodel.statementNo);
                            modelBank.dtStatement = hedermodel.entryDate;
                            modelBank.codeDaily = abm[0].codeDaily;
                            abb.Save(modelBank, this.Name, Login._user.idUser);
                        }
                }
                
        }
        #region ReadXML
        private void btnReadXml_Click(object sender, EventArgs e)
        {
            doread();
        }
        private void doread()
        {
            bubumodel = new List<bookModel>();
            XmlTextReader reader = new XmlTextReader("D:\\Download\\importAccounting\\GLENTRIES.xml");
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        Console.Write("<" + reader.Name);
                        Console.WriteLine(">");
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        Console.WriteLine(reader.Value);
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        Console.Write("</" + reader.Name);
                        Console.WriteLine(">");
                        break;
                }
            }

        }

        #endregion ReadXML

    }
}
