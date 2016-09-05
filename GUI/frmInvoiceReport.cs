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
using BIS.Business;
using Microsoft.Reporting.WinForms;
using Microsoft.ReportingServices;
using System.IO;


namespace GUI
{
    public partial class frmInvoiceReport : Telerik.WinControls.UI.RadForm
    {
        InvoiceReportModel InvoideDS;
        List<InvoiceItemsReportModel> ItemsDS;
        private DataTable irep1;
        private DataTable iirep1;
        private DataTable iipaid;
        private DataTable iirep2;
        private DataTable iirepOne1;
        private string namePdf;
         private int idLabel;
         private Boolean withOutHeader;
         private int invoiceRbr;


         public frmInvoiceReport(DataTable irep, DataTable iirep, string name, int label, Boolean withOutHeaderOption)
        {
            // TODO: Complete member initialization
            this.irep1 = irep;
            this.iirep1 = iirep;             
             this.iirep2 = new DataTable();
             this.iirepOne1 = new DataTable();


            iipaid = new DataTable();
            if (irep != null)
            {
                int idInvoice = 0;
                if (irep.Rows[0]["idInvoice"].ToString() != "")
                {
                    idInvoice = Convert.ToInt32(irep.Rows[0]["idInvoice"].ToString());
                    invoiceRbr = Convert.ToInt32(irep.Rows[0]["invoiceRbr"].ToString());
                    irep1 = new InvoiceBUS().GetReportInvoiceReportByIntID(idInvoice);
                    int idContPers = 0;
                    idContPers = Convert.ToInt32(irep.Rows[0]["idContPerson"].ToString());
                    int idClient = 0;
                    idClient = Convert.ToInt32(irep.Rows[0]["idClient"].ToString());
                    ArrangementBookModel abm = new ArrangementBookModel();
                    abm = new ArrangementBookBUS().GetArrangementBook(Convert.ToInt32(irep.Rows[0]["idVoucher"].ToString()));

                    if(abm!=null && (idContPers!=0 || idClient !=0))
                        iirep2 = new ArrangementBookPersonsDAO().GetAllTravelersInvoicing(abm.idArrangementBook, true);

                }
                this.iipaid = new InvoiceDAO().GetInvoicePaid(idInvoice);
                if (iipaid == null)
                    iipaid = new DataTable();
            }
            namePdf = name;
            withOutHeader = withOutHeaderOption;
            InitializeComponent();
            this.Icon = Login.iconForm;
            string formName = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();
            formName = formName + " " + this.Text;
            this.Text = formName;
            idLabel = label;
        }

        private void frmInvoiceReport_Load(object sender, EventArgs e)
        {


            DataSet dataSet = new DataSet();
            DataSet dataSet1 = new DataSet();
            DataSet dataSet2 = new DataSet();
            DataSet dataSet3 = new DataSet();
            DataTable dataTableCopy = new DataTable(); // kopiranje dataTable
            DataTable dataTableCopy1 = new DataTable();
            DataTable dataTableCopy2 = new DataTable();
            DataTable dataTableCopy3 = new DataTable();
            if (irep1 != null)
                dataTableCopy = irep1.Copy();
            if (iirep1 != null)
                dataTableCopy1 = iirep1.Copy();
            if (iipaid != null)
                dataTableCopy2 = iipaid.Copy();

            if (iirep2 != null)
                dataTableCopy3 = iirep2.Copy();
            dataSet.Tables.Add(dataTableCopy);
            dataSet1.Tables.Add(dataTableCopy1);
            dataSet2.Tables.Add(dataTableCopy2);
            dataSet3.Tables.Add(dataTableCopy3);

            ReportDataSource rdSource = new ReportDataSource("InvoideDS", (DataTable)dataTableCopy);
            ReportDataSource rdSource1 = new ReportDataSource("ItemsDS", (DataTable)dataTableCopy1);
            ReportDataSource rdSource2 = new ReportDataSource("InvoicePaid", (DataTable)dataTableCopy2);
            ReportDataSource rdSource3 = new ReportDataSource("TravelWith", (DataTable)dataTableCopy3);
            rdSource.Value = dataTableCopy;
            rdSource1.Value = dataTableCopy1;
            rdSource2.Value = dataTableCopy2;
            rdSource3.Value = dataTableCopy3;



            //this.invoiceViewer.LocalReport.DataSources.Clear();
            //this.invoiceViewer.LocalReport.DataSources.Add(rdSource);
            //this.invoiceViewer.LocalReport.DataSources.Add(rdSource1);
            //this.invoiceViewer.LocalReport.DataSources.Add(rdSource2);
            //if (invoiceRbr < 999)
            //    this.invoiceViewer.LocalReport.DataSources.Add(rdSource3);
            //this.invoiceViewer.LocalReport.Refresh();

            this.invoiceViewer.Reset();
            invoiceViewer.ProcessingMode = ProcessingMode.Local;
            if(withOutHeader==true)
            {
                if(invoiceRbr<999)
                    invoiceViewer.LocalReport.ReportPath = "Reports//InvoiceOneWithoutHeader.rdlc";
                else
                invoiceViewer.LocalReport.ReportPath = "Reports//InvoiceWithoutHeader.rdlc";
            }
            else if (idLabel != 0)
            {
                if (idLabel == 1)
                {
                    if (invoiceRbr < 999)
                        invoiceViewer.LocalReport.ReportPath = "Reports//InvoiceOne.rdlc";
                    else
                        invoiceViewer.LocalReport.ReportPath = "Reports//Invoice.rdlc";
                }
                else
                    if (idLabel == 2)
                    {
                        if (invoiceRbr < 999)
                            invoiceViewer.LocalReport.ReportPath = "Reports//InvoiceOneMundo.rdlc";
                        else
                            invoiceViewer.LocalReport.ReportPath = "Reports//InvoiceMundo.rdlc";
                    }
                    else
                    {
                        if (invoiceRbr < 999)
                            invoiceViewer.LocalReport.ReportPath = "Reports//InvoiceOneAuti.rdlc";
                        else
                            invoiceViewer.LocalReport.ReportPath = "Reports//InvoiceAuti.rdlc";
                    }
            }
            else
            {
                if (invoiceRbr < 999)
                    invoiceViewer.LocalReport.ReportPath = "Reports//InvoiceOne.rdlc";
                else
                    invoiceViewer.LocalReport.ReportPath = "Reports//Invoice.rdlc";
            }


            this.invoiceViewer.LocalReport.DataSources.Clear();
            this.invoiceViewer.LocalReport.DataSources.Add(rdSource);
            this.invoiceViewer.LocalReport.DataSources.Add(rdSource1);
            this.invoiceViewer.LocalReport.DataSources.Add(rdSource2);
           // if (invoiceRbr < 999)
                this.invoiceViewer.LocalReport.DataSources.Add(rdSource3);
            this.invoiceViewer.LocalReport.Refresh();


            this.invoiceViewer.ProcessingMode = ProcessingMode.Local;
            invoiceViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);


            this.invoiceViewer.LocalReport.Refresh();


            this.invoiceViewer.RefreshReport();

            string nameRepVol = namePdf;
            ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
            rg.GenerateOutputPDF(invoiceViewer.LocalReport, nameRepVol); 
        }

        private void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("InvoideDS", this.irep1));
            e.DataSources.Add(new ReportDataSource("ItemsDS", this.iirep1));
           // if (invoiceRbr < 999)
            e.DataSources.Add(new ReportDataSource("TravelWith", this.iirep2));
        }

      
    }
}
