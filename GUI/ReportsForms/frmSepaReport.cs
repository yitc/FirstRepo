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
using System.Drawing.Printing;
using System.Drawing.Imaging;

namespace GUI.ReportsForms
{
    public partial class frmSepaReport : Telerik.WinControls.UI.RadForm
    {
        InvoiceReportModel InvoideDS;
        List<InvoiceItemsReportModel> ItemsDS;
        List<InvoiceItemsReportModel> ItemsOneDS;
        private string namePdf;
        private DataTable irep1;
        private DataTable iirep1;
        private DataTable iirepOne1;
        private int idLabel;
        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        private bool direct;

        public frmSepaReport(DataTable irep, DataTable iirep, string name)
        {
            namePdf = name;
            this.irep1 = irep;
            this.iirep1 = iirep;
          
         //   direct = printing;


            InitializeComponent();
            this.Icon = Login.iconForm;
            string formName = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();
            formName = formName + " " + this.Text;
            this.Text = formName;
        }

        private void frmSepaReport_Load(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet();
            DataSet dataSet1 = new DataSet();
            
            DataTable dataTableCopy = new DataTable(); // kopiranje dataTable
            DataTable dataTableCopy1 = new DataTable();
            

            if (irep1 != null)
                dataTableCopy = irep1.Copy();
            if (iirep1 != null)
                dataTableCopy1 = iirep1.Copy();
         

            dataSet.Tables.Add(dataTableCopy);
            dataSet1.Tables.Add(dataTableCopy1);
         

            ReportDataSource rdSource = new ReportDataSource("SepaDS", (DataTable)dataTableCopy);
            ReportDataSource rdSource1 = new ReportDataSource("SepaItems", (DataTable)dataTableCopy1);
      

            //ReportDataSource rdSource = new ReportDataSource("InvoideDS", (DataTable)dataTableCopy);
            //ReportDataSource rdSource2 = new ReportDataSource("ItemsOneDS", (DataTable)dataTableCopy2);
            //ReportDataSource rdSource1 = new ReportDataSource("ItemsDS", (DataTable)dataTableCopy1);

            rdSource.Value = dataTableCopy;
            rdSource1.Value = dataTableCopy1;
        



            //rdSource.Value = dataTableCopy;
            //rdSource2.Value = dataTableCopy2;
            //rdSource1.Value = dataTableCopy1;

            this.reportViewer1.Reset();
            reportViewer1.ProcessingMode = ProcessingMode.Local;

            reportViewer1.LocalReport.ReportPath = "Reports//Sepa.rdlc";
          
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rdSource);
            this.reportViewer1.LocalReport.DataSources.Add(rdSource1);
            

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            reportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);


            this.reportViewer1.LocalReport.Refresh();


            //if (!direct)
            //{
            //    this.reportViewer1.RefreshReport();
            //}
            //else
            //{
            //    Export(this.reportViewer1.LocalReport);
            //    Print();
            //}



            this.reportViewer1.RefreshReport();
            string nameRepVol = namePdf;
            ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
            rg.GenerateOutputPDF(reportViewer1.LocalReport, nameRepVol);

        }

        private void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("SepaDS", this.irep1));
            e.DataSources.Add(new ReportDataSource("SepaItems", this.iirep1));
        }

        private void Export(LocalReport report)
        {
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.5in</PageWidth>
                <PageHeight>11in</PageHeight>
                <MarginTop>0.25in</MarginTop>
                <MarginLeft>0.25in</MarginLeft>
                <MarginRight>0.25in</MarginRight>
                <MarginBottom>0.25in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }
        private void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }
        private Stream CreateStream(string name,
          string fileNameExtension, Encoding encoding,
          string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
            Metafile(m_streams[m_currentPageIndex]);

            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
             ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
             ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
             ev.PageBounds.Width,
             ev.PageBounds.Height);

            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

      

        
    }
}
