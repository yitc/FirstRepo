using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BIS.Model;
using System.Resources;
using BIS.Business;
using Telerik.WinControls.UI;
using Telerik.WinControls.Data;
using System.IO;

namespace GUI
{
    public partial class AccountData : UserControl
    {
        AccLineModel linemodel;
        LedgerAccountModel lamodel;
        AccOpenLinesModel oplmodel;
        ClientModel clmodel;
        PersonModel persmodel;
        private List<AccLineModel>  debdata;
        private List<AccLineModel> credata;
        private List<AccOpenLinesModel> oldata;
        private List<InvoiceModel> invdata;
        private bool debtab = false;
        private bool cretab = false;
        private bool opltab = false;
        private string idDebCre;
        private string layoutDebit;
        private string layoutCredit;
        private string layoutOpenLines;
        private string layoutInvoice;
        private int idPerson;
        InvoiceModel _selectedRowInvoice;
        InvoiceModel _clickedInvoice;
        bool isFrmClient;

        public AccountData()
        {
            layoutDebit = MainForm.gridFiltersFolder + "\\layoutDebitAccData.xml";
            layoutCredit = MainForm.gridFiltersFolder + "\\layoutCreditAccData.xml";
            layoutInvoice = MainForm.gridFiltersFolder + "\\layoutInvoiceHeader.xml";

            InitializeComponent();
        }

        public AccountData(string debcre, int idPerson)
        {
            this.idPerson = idPerson;
            debtab = false;
            cretab = false;
            opltab = false;
            idDebCre = debcre;
            isFrmClient = true;
            layoutDebit = MainForm.gridFiltersFolder + "\\layoutDebitAccData.xml";
            layoutCredit = MainForm.gridFiltersFolder + "\\layoutCreditAccData.xml";
            layoutInvoice = MainForm.gridFiltersFolder + "\\layoutInvoiceHeader.xml";

            InitializeComponent();
            
        }
        public AccountData(int debcre)
        {
            if (debcre > 0)
            {
                debtab = false;
                cretab = false;
                opltab = false;
                idPerson = debcre;
                idDebCre = "";
                AccDebCreBUS pomb = new AccDebCreBUS();
                AccDebCreModel mod = new AccDebCreModel();
                mod = pomb.GetPersonDebCre(idPerson);   // pronalazi osobu po Id i prebacuje na accountBroj koji se koristi u knjigovodstvu
                if (mod != null)
                {
                    if (mod.accNumber != null)
                        idDebCre = mod.accNumber;
                }
            }
            layoutDebit = MainForm.gridFiltersFolder + "\\layoutDebitAccData.xml";
            layoutCredit = MainForm.gridFiltersFolder + "\\layoutCreditAccData.xml";
            layoutInvoice = MainForm.gridFiltersFolder + "\\layoutInvoiceHeader.xml";

            InitializeComponent();

        }



        private void rgvDebitor_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvDebitor.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvDebitor.Columns[i].HeaderText != null && resxSet.GetString(rgvDebitor.Columns[i].HeaderText) != null)
                        rgvDebitor.Columns[i].HeaderText = resxSet.GetString(rgvDebitor.Columns[i].HeaderText);
                }
            }
            layoutDebit = MainForm.gridFiltersFolder + "\\layoutDebitAccData.xml";
            if (File.Exists(layoutDebit))
            {
                rgvDebitor.LoadLayout(layoutDebit);
            }
        }

        private void rgvCreditor_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvCreditor.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvCreditor.Columns[i].HeaderText != null && resxSet.GetString(rgvCreditor.Columns[i].HeaderText) != null)
                        rgvCreditor.Columns[i].HeaderText = resxSet.GetString(rgvCreditor.Columns[i].HeaderText);
                }
                if (rgvCreditor.Columns[i].GetType() == typeof(GridViewDateTimeColumn))
                {
                    if (rgvCreditor.Columns[i].Name.ToLower() != "dtUserModified".ToLower() && rgvCreditor.Columns[i].Name.ToLower() != "dtUserCreated".ToLower())
                    {
                        rgvCreditor.Columns[i].FormatString = "{0: dd-MM-yyyy}";
                    }
                }
            }
           

            layoutCredit = MainForm.gridFiltersFolder + "\\layoutCreditAccData.xml";
            if (File.Exists(layoutCredit))
            {
                rgvCreditor.LoadLayout(layoutCredit);
            }
        }

        private void rgvOpenlines_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvOpenlines.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvOpenlines.Columns[i].HeaderText != null && resxSet.GetString(rgvOpenlines.Columns[i].HeaderText) != null)
                        rgvOpenlines.Columns[i].HeaderText = resxSet.GetString(rgvOpenlines.Columns[i].HeaderText);
                }
                if (rgvOpenlines.Columns[i].GetType() == typeof(GridViewDateTimeColumn))
                {
                    if (rgvOpenlines.Columns[i].Name.ToLower() != "dtUserModified".ToLower() && rgvOpenlines.Columns[i].Name.ToLower() != "dtUserCreated".ToLower())
                    {
                        rgvOpenlines.Columns[i].FormatString = "{0: dd-MM-yyyy}";
                    }
                }
            }
            

            layoutOpenLines = MainForm.gridFiltersFolder + "\\layoutOpenAccData.xml";
            if (File.Exists(layoutOpenLines))
            {
                rgvOpenlines.LoadLayout(layoutOpenLines);
            }
        }
        private void rgvInvoice_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < rgvInvoice.Columns.Count; i++)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvInvoice.Columns[i].HeaderText != null && resxSet.GetString(rgvInvoice.Columns[i].HeaderText) != null)
                        rgvInvoice.Columns[i].HeaderText = resxSet.GetString(rgvInvoice.Columns[i].HeaderText);
                }
            }
            layoutInvoice = MainForm.gridFiltersFolder + "\\layoutInvoiceHeader.xml";
            if (File.Exists(layoutInvoice))
            {
                rgvInvoice.LoadLayout(layoutInvoice);
            }

        }

        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                tabDebitor.Text = resxSet.GetString("Debitor");
                tabCreditor.Text = resxSet.GetString("Creditor");
                tabOpenLines.Text = resxSet.GetString("Open lines");
                tabInvoice.Text = resxSet.GetString("Invoice");

            }
        }
        private void setDataGrid()
        {
            if (idDebCre != "")
            {
                AccDebCreBUS dbbus = new AccDebCreBUS();
                AccDebCreModel dbmod = new AccDebCreModel();

                dbmod = dbbus.GetCustomerByAccCode(idDebCre);
                if (dbmod != null)
                {
                    if (dbmod.isDebitor == true && dbmod.debAccount != "")
                    {
                        debtab = true;
                    }
                    if (dbmod.isCreditor == true && dbmod.creditAccount != "")
                    {
                        cretab = true;
                    }
                    AccLineBUS stv = new AccLineBUS(Login._bookyear);
                    AccOpenLinesBUS opb = new AccOpenLinesBUS();
                    InvoiceBUS inb = new InvoiceBUS();
                    debdata = new List<AccLineModel>();
                    credata = new List<AccLineModel>();
                    oldata = new List<AccOpenLinesModel>();
                    invdata = new List<InvoiceModel>();

                    if (dbmod.debAccount != "" && dbmod.debAccount != null)
                    {
                        debdata = stv.GetLinesByAccountAndCustomer(dbmod.debAccount, 0, idDebCre);
                        rgvDebitor.DataSource = debdata;
                        rgvDebitor.Show();
                    }
                    if (dbmod.creditAccount != "" && dbmod.creditAccount != null)
                    {
                        credata = stv.GetLinesByAccountAndCustomer(dbmod.creditAccount, 0, idDebCre);
                        rgvCreditor.DataSource = credata;
                        rgvCreditor.Show();
                    }
                    oldata = opb.GetAccOpenLinesByIDwList(idDebCre);
                    if (oldata != null)
                    {
                        if (oldata.Count > 0)
                        {
                            opltab = true;
                            rgvOpenlines.DataSource = oldata;
                            rgvOpenlines.Show();
                        }
                    }
                    invdata = inb.GetAllInvoiceCustomer(idPerson,isFrmClient);
                    rgvInvoice.DataSource = invdata;
                    rgvInvoice.Show();
                    showGridData();

                }
            }
        }
            private void showGridData()
        {
          //  rpvAccountData
            this.tabDebitor.Enabled = debtab;
            this.tabCreditor.Enabled = cretab;
            this.tabOpenLines.Enabled = opltab;

        }

            private void AccountData_Load(object sender, EventArgs e)
            {
                setTranslation();
                if ((idPerson > 0) || (idPerson == 0 && idDebCre != "" && idDebCre != null))
                setDataGrid();

            }

            private void rgvDebitor_ViewCellFormatting(object sender, CellFormattingEventArgs e)
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

            private void rgvCreditor_ViewCellFormatting(object sender, CellFormattingEventArgs e)
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

            private void debitorMenuItem1_Click(object sender, EventArgs e)
            {
                if (File.Exists(layoutDebit))
                {
                    File.Delete(layoutDebit);
                }
                rgvDebitor.SaveLayout(layoutDebit);

                //RadMessageBox.Show("Layout Saved");
            }

         
            private void creditorMenuItem1_Click(object sender, EventArgs e)
            {
                 if (File.Exists(layoutCredit))
                    {
                        File.Delete(layoutCredit);
                    }
                    rgvCreditor.SaveLayout(layoutCredit);

                    //RadMessageBox.Show("Layout Saved");
            }

            private void openMenuItem1_Click(object sender, EventArgs e)
            {
                if (File.Exists(layoutOpenLines))
                {
                    File.Delete(layoutOpenLines);
                }
                rgvCreditor.SaveLayout(layoutOpenLines);

                //RadMessageBox.Show("Layout Saved");

            }
            private void toolStripMenuItem1_Click(object sender, EventArgs e)
            {
                if (File.Exists(layoutInvoice))
                {
                    File.Delete(layoutInvoice);
                }
                rgvInvoice.SaveLayout(layoutInvoice);
            }

         

            private void rgvInvoice_CellDoubleClick(object sender, GridViewCellEventArgs e)
            {
                if (e.Row.DataBoundItem != null)
                {
                    Type t = e.Row.DataBoundItem.GetType();
                    if (t == typeof(InvoiceModel))
                    {
                        GridViewRowInfo info = this.rgvInvoice.CurrentRow;
                        InvoiceModel selectedInvoice = (InvoiceModel)info.DataBoundItem;
                        _selectedRowInvoice = new InvoiceModel();
                        _clickedInvoice = new InvoiceModel();

                        if (info != null && e.RowIndex >= 0)
                            if (selectedInvoice != null)
                            {
                                frmInvoice frm = new frmInvoice(selectedInvoice);
                                frm.ShowDialog();
                                //     RadMessageBox.Show("Invoice: " + selectedInvoice.idInvoice.ToString());
                                //modelData = invoiceBUS.GetAllInvoices();
                                //this.SetDataPersonBinding(modelData);  

                            }
                    }
                }
            }

            private void rgvOpenlines_ViewCellFormatting(object sender, CellFormattingEventArgs e)
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

         
         
      
    }
}
