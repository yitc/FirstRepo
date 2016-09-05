using System;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using BIS.Business;
using BIS.Model;
using System.Drawing;
using System.Linq;
using System.Management;
using BIS.DAO;

namespace GUI
{
    public class PrintReport : IDisposable
    {
        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        ArrangementBookModel arrBookModel;            
        public LocalReport report = new LocalReport();

        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();

        string reportName;
        bool isTraveler;
        DataTable dataTableReports = new DataTable();

        int idLabel;

        bool printing;
        bool exportToPdf;
        bool withOutHeader = false;

        int invoiceRbr=0;

        public PrintReport(DataTable dtr, string reportName) 
        {
            dataTableReports = dtr;
            this.reportName = reportName;
            creatingReport2();
        }

        public PrintReport(int idArrangement, int idContPers,string reportName, bool isTraveler)
        {
            //if !!!

            dt1 = new TravelerPapersReportBUS().GetTravelerPaper(idArrangement, idContPers);
            dt2 = new TravelerPapersReportBUS().GetPapers(idArrangement);
            dt3 = new TravelerPapersReportBUS().GetTekst(idArrangement);
            dt4 = new TravelerPapersReportBUS().GetArrangementRemaining(idArrangement);
            this.reportName = reportName;
            this.isTraveler = isTraveler;
            creatingReport();
        }
         public PrintReport(DataTable irep, DataTable iirep, DataTable iirepOne, string name,int label,bool withOutHeaderOption, bool printing, bool exportToPdf)
        {
             //invoice one
            // TODO: Complete member initialization
            this.reportName = name;
            this.dt1 = irep;
            this.dt2 = iirep;
            this.dt3 = iirepOne;
            this.idLabel = label;
            this.printing = printing;
            this.exportToPdf = exportToPdf;
            this.withOutHeader = withOutHeaderOption;

            if (irep != null)
            {
                int idInvoice = 0;
                if (irep.Rows[0]["idInvoice"].ToString() != "")
                {
                    idInvoice = Convert.ToInt32(irep.Rows[0]["idInvoice"].ToString());
                    invoiceRbr = Convert.ToInt32(irep.Rows[0]["invoiceRbr"].ToString());
                    dt1 = new InvoiceBUS().GetReportInvoiceReportByIntID(idInvoice);
                    //dt2 = new InvoiceItemsBUS().GetReportInvoiceItemsByID(idInvoice, Login._user.lngUser);
                    int idContPers = 0;
                    idContPers = Convert.ToInt32(irep.Rows[0]["idContPerson"].ToString());
                    int idClient = 0;
                    idClient = Convert.ToInt32(irep.Rows[0]["idClient"].ToString());

                    ArrangementBookModel abm = new ArrangementBookModel();
                    abm = new ArrangementBookBUS().GetArrangementBook(Convert.ToInt32(irep.Rows[0]["idVoucher"].ToString()));
                    if (abm != null && (idContPers != 0 || idClient != 0))
                        dt3 = new ArrangementBookPersonsDAO().GetAllTravelersInvoicing(abm.idArrangementBook, true);
                }
               // this.iipaid = new InvoiceDAO().GetInvoicePaid(idInvoice);
               // if (iipaid == null)
               //     iipaid = new DataTable();
            }
            creatingReport3();
        
        }

         public PrintReport(DataTable irep, DataTable iirep, string name, int label, Boolean withOutHeaderOption, bool printing, bool exportToPdf)
         {
             // invoice 
             // TODO: Complete member initialization
             this.reportName = name;
             this.dt1 = irep;
             this.dt2 = iirep;             
             this.idLabel = label;
             this.printing = printing;
             this.exportToPdf = exportToPdf;
             this.withOutHeader = withOutHeaderOption;

             dt3 = new DataTable();
             if (irep != null)
             {
                 int idInvoice = 0;
                 if (irep.Rows[0]["idInvoice"].ToString() != "")
                 {
                     idInvoice = Convert.ToInt32(irep.Rows[0]["idInvoice"].ToString());
                     invoiceRbr = Convert.ToInt32(irep.Rows[0]["invoiceRbr"].ToString());
                     dt1 = new InvoiceBUS().GetReportInvoiceReportByIntID(idInvoice);
                    // dt2 = new InvoiceItemsBUS().GetReportInvoiceItemsByID(idInvoice, Login._user.lngUser);
                     int idContPers = 0;
                     int idClient = 0;
                     idContPers = Convert.ToInt32(irep.Rows[0]["idContPerson"].ToString());
                     idClient = Convert.ToInt32(irep.Rows[0]["idClient"].ToString());

                     ArrangementBookModel abm = new ArrangementBookModel();
                     abm = new ArrangementBookBUS().GetArrangementBook(Convert.ToInt32(irep.Rows[0]["idVoucher"].ToString()));
                     if (abm != null && (idContPers != 0 || idClient != 0))
                         dt4 = new ArrangementBookPersonsDAO().GetAllTravelersInvoicing(abm.idArrangementBook, true);
                 }
                  dt3 = new InvoiceDAO().GetInvoicePaid(idInvoice);
                  if (dt3 == null)
                      dt3 = new DataTable();
             }

             creatingReport4();
         }
        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.
        private Stream CreateStream(string name,
          string fileNameExtension, Encoding encoding,
          string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }
        // Export the given report as an EMF (Enhanced Metafile) file.


        private void Export()
        {
            
            string deviceInfo =
               @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>21cm</PageWidth>
                <PageHeight>29.7cm</PageHeight>
                <MarginTop>1cm</MarginTop>
                <MarginLeft>2cm</MarginLeft>
                <MarginRight>1cm</MarginRight>
                <MarginBottom>1cm</MarginBottom>
            </DeviceInfo>";

            Warning[] warnings;
            m_streams = new List<Stream>();
            //report.Refresh();
            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        private void ExportTo()
        {

            string deviceInfo =
               @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>21cm</PageWidth>
                <PageHeight>29.7cm</PageHeight>
                <MarginTop>1cm</MarginTop>
                <MarginLeft>2cm</MarginLeft>
                <MarginRight>1cm</MarginRight>
                <MarginBottom>1cm</MarginBottom>
            </DeviceInfo>";

            Warning[] warnings;
            m_streams = new List<Stream>();
            //report.Refresh();
            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        // Handler for PrintPageEvents
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);

            //Adjust rectangular area with printer margins.
            //Rectangle adjustedRect = new Rectangle(0,0,920,1169);
            //Rectangle adjustedRect = new Rectangle(0, 0, 2000, 2800);
            // Draw a white background for the report
            Rectangle adjustedRect = new Rectangle(
            ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
            ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
            //0,
            //0,
            ev.PageBounds.Width,
            ev.PageBounds.Height);



            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);
           // PrinterResolution p = ev.PageSettings.PrinterResolution;
            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);

            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
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
               /*  printDoc.DefaultPageSettings.Landscape = false;
                printDoc.DefaultPageSettings.Margins.Top = 0;
                printDoc.DefaultPageSettings.Margins.Bottom = 0;
                printDoc.DefaultPageSettings.Margins.Right = 0;
                printDoc.DefaultPageSettings.Margins.Left = 0;*/

                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }

        private void PrintTo(string printername)
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = printername;

            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                /*  printDoc.DefaultPageSettings.Landscape = false;
                 printDoc.DefaultPageSettings.Margins.Top = 0;
                 printDoc.DefaultPageSettings.Margins.Bottom = 0;
                 printDoc.DefaultPageSettings.Margins.Right = 0;
                 printDoc.DefaultPageSettings.Margins.Left = 0;*/

                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }
        // Create a local report for Report.rdlc, load the data,
        //    export the report to an .emf file, and print it.
        public void Run()
        {
            Export();
            Print();
            
        }

        public void RunTo(string printername)
        {
            ExportTo();
            PrintTo(printername);

        }

        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }

        private void creatingReport2() 
        {
            DataSet dataSet = new DataSet(); // deklarisanje dataSeta


            if (dataTableReports.Rows.Count == 0)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("There is nothing for print preview!");
            }

            else
            {
                DataTable dataTableCopy = new DataTable(); // kopiranje dataTable
                dataTableCopy = dataTableReports.Copy();
                dataSet.Tables.Add(dataTableCopy);

                //Za brisanje vremena iz datuma
                int g = 0;
                string d = "";
                string p = "";
                string h = "";
                dataTableCopy.Columns.Add("dtF", typeof(String));
                dataTableCopy.Columns.Add("dtT", typeof(String));
                dataTableCopy.Columns.Add("dtB", typeof(String));
                foreach (DataRow dr in dataTableCopy.Rows)
                {
                    d = dr["dtFromArrangement"].ToString();
                    p = dr["dtToArrangement"].ToString();
                    h = dr["birthdate"].ToString();
                    if (h == "")
                    {
                        h = DateTime.MinValue.ToString();
                    }
                    DateTime date = Convert.ToDateTime(d);
                    DateTime dateT = Convert.ToDateTime(p);
                    DateTime dateB = Convert.ToDateTime(h);

                    p = dateT.ToString("dd/MM/yyyy");
                    d = date.ToString("dd/MM/yyyy");
                    h = dateB.ToString("dd/MM/yyyy");
                    //dt.Rows[g]["dateFrom"] = d;
                    dataTableCopy.Rows[g]["dtF"] = d;
                    dataTableCopy.Rows[g]["dtT"] = p;
                    dataTableCopy.Rows[g]["dtB"] = h;

                    g++;

                    //Za dodeljivanje polja iz modela i ArrangemnentBookDAO radi prikaza u report i za skrivanje polja
                    dr["nameArrangement1"] = dr["nameArrangement"];
                    dr["dtFromArrangement1"] = dr["dtFromArrangement"];
                    dr["dtToArrangement1"] = dr["dtToArrangement"];
                    dr["codeArrangement1"] = dr["codeArrangement"];
                    //


                }

                dataTableCopy.Columns.Remove("dtFromArrangement1");
                dataTableCopy.Columns.Remove("dtToArrangement1");

                dataTableCopy.Columns.Remove("birthdate");

                dataTableCopy.Columns["dtF"].ColumnName = "dtFromArrangement1";
                dataTableCopy.Columns["dtT"].ColumnName = "dtToArrangement1";
                dataTableCopy.Columns["dtB"].ColumnName = "birthdate";
                ////end


                ReportDataSource rdSource = new ReportDataSource("TelephoneListDataSet", (DataTable)dataTableReports);

                rdSource.Value = dataTableCopy;

                report.DataSources.Add(rdSource);



                // prikaz podataka
                report.DataSources.Clear();
                report.DataSources.Add(rdSource);
                report.Refresh();


                ReportClassTelephoneList tr = new ReportClassTelephoneList();
                reportName = "Reports//ReportTelephoneListPreview.rdlc";

                tr.transl(reportName, dataTableReports);


                //Za putanju i uzimanje RDLC-a iz BIN foldera
                string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = Path.Combine(exeFolder, "Reports//ReportTelephoneListPreview.rdlc");
                report.ReportPath = reportPath;
                report.ReportEmbeddedResource = reportPath;

                ////generisanje PDF u pozadini !
                string nameRepTra = "";
                FileStream streamingReport = new FileStream(reportPath, FileMode.Open);
                report.LoadReportDefinition(streamingReport);
                streamingReport.Close();
                ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                rg.GenerateOutputPDF(report, nameRepTra);
            }

        }
        private void creatingReport() {
            //reportName = "AT_Reizigers_KB_Buitenland_Handdoeken_Mee";

            DataSet dataSet = new DataSet();
            DataTable dataTableCopy = new DataTable(); // kopiranje dataTable
            dataTableCopy = dt1.Copy();
            dataSet.Tables.Add(dataTableCopy);
            ReportDataSource rdSource= new  ReportDataSource();
            switch (reportName.Substring(0,2))
            { 
                case "MM":
                    rdSource = new ReportDataSource("MundoradoSubRepTravelerPaperDataSet", (DataTable)dt1);
                    break;
                case "AT":
                    rdSource = new ReportDataSource("AutiTravelSubRepTravelerPaperDataSet", (DataTable)dt1);
                    break;
                case "AR":
                    rdSource = new ReportDataSource("AutiTravelSubRepTravelerPaperDataSet", (DataTable)dt1);
                    break;
                case "BH":
                    rdSource = new ReportDataSource("SubRepTravelerPaperDataSet", (DataTable)dt1);
                    break;
            }

            rdSource.Value = dataTableCopy;
            //Za putanju i uzimanje RDLC-a iz BIN foldera
            //string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath;
            if (isTraveler)
            {
                 reportPath = "C://TravelPapers//" + reportName + ".rdlc";
            }
            else {
                 reportPath = "C://TravelPapersVolunteers//" + reportName + ".rdlc";
            }
            report.ReportPath = reportPath;
            report.ReportEmbeddedResource = reportPath;
            //report.LoadSubreportDefinition("C://TravelPapersVolunteers//Sub" + reportName + ".rdlc", new MemoryStream());
            report.EnableHyperlinks = true;
            report.EnableExternalImages = true;
            report.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessing);
            report.DataSources.Add(rdSource);
            report.Refresh();   
        }
        public void creatingReport3() 
        {            
            DataSet dataSet = new DataSet();
            DataSet dataSet1 = new DataSet();
            DataSet dataSet2 = new DataSet();
            DataTable dataTableCopy = new DataTable(); // kopiranje dataTable
            DataTable dataTableCopy1 = new DataTable();
            DataTable dataTableCopy2 = new DataTable();

            if (dt1 != null)
                dataTableCopy = dt1.Copy();
            if (dt2 != null)
                dataTableCopy1 = dt2.Copy();
            if (dt3 != null)
                dataTableCopy2 = dt3.Copy();

            dataSet.Tables.Add(dataTableCopy);
            dataSet1.Tables.Add(dataTableCopy1);
            dataSet2.Tables.Add(dataTableCopy2);

            ReportDataSource rdSource = new ReportDataSource("InvoideDS", (DataTable)dataTableCopy);
            ReportDataSource rdSource1 = new ReportDataSource("ItemsDS", (DataTable)dataTableCopy1);
            ReportDataSource rdSource2 = new ReportDataSource("TravelWith", (DataTable)dataTableCopy2);            

            rdSource.Value = dataTableCopy;
            rdSource1.Value = dataTableCopy1;
            rdSource2.Value = dataTableCopy2;

            if (withOutHeader == true)
            {
                report.ReportPath = "Reports//InvoiceOneWithoutHeader.rdlc";
            }
            else if (idLabel != 0)
            {
                if (idLabel == 1)
                    report.ReportPath = "Reports//InvoiceOne.rdlc";
                else
                    if (idLabel == 2)
                        report.ReportPath = "Reports//InvoiceOneMundo.rdlc";
                    else
                        report.ReportPath = "Reports//InvoiceOneAuti.rdlc";
            }
            else
            {
                report.ReportPath = "Reports//InvoiceOne.rdlc";
            }

            report.DataSources.Clear();
            report.DataSources.Add(rdSource);
            report.DataSources.Add(rdSource1);
            report.DataSources.Add(rdSource2);

            report.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);


            report.Refresh();


            if (this.printing == true)
            {
                Run();
            }
         
            string nameRepVol = this.reportName;
            if (this.exportToPdf == true)
            {
                ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                rg.GenerateOutputPDF(report, this.reportName);
            }
           
        }

        public void creatingReport4()
        {
            DataSet dataSet = new DataSet();
            DataSet dataSet1 = new DataSet();
            DataSet dataSet2 = new DataSet();
            DataSet dataSet3 = new DataSet();
            DataTable dataTableCopy = new DataTable(); // kopiranje dataTable
            DataTable dataTableCopy1 = new DataTable();
            DataTable dataTableCopy2 = new DataTable();
            DataTable dataTableCopy3 = new DataTable();
            if (dt1 != null)
                dataTableCopy = dt1.Copy();
            if (dt2 != null)
                dataTableCopy1 = dt2.Copy();
            if (dt3 != null)
                dataTableCopy2 = dt3.Copy();

            if (dt4 != null)
                dataTableCopy3 = dt4.Copy();
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



            //this.report.DataSources.Clear();
            //this.report.DataSources.Add(rdSource);
            //this.report.DataSources.Add(rdSource1);
            //this.report.DataSources.Add(rdSource2);
            //if (invoiceRbr < 999)
            //    this.report.DataSources.Add(rdSource3);
            //this.report.Refresh();

            //this.invoiceViewer.Reset();
            //invoiceViewer.ProcessingMode = ProcessingMode.Local;
            if (withOutHeader == true)
            {
                if (invoiceRbr < 999)
                    report.ReportPath = "Reports//InvoiceOneWithoutHeader.rdlc";
                else
                    report.ReportPath = "Reports//InvoiceWithoutHeader.rdlc";
            }
            else if (idLabel != 0)
            {
                if (idLabel == 1)
                {
                    if (invoiceRbr < 999)
                        report.ReportPath = "Reports//InvoiceOne.rdlc";
                    else
                        report.ReportPath = "Reports//Invoice.rdlc";
                }
                else
                    if (idLabel == 2)
                    {
                        if (invoiceRbr < 999)
                            report.ReportPath = "Reports//InvoiceOneMundo.rdlc";
                        else
                            report.ReportPath = "Reports//InvoiceMundo.rdlc";
                    }
                    else
                    {
                        if (invoiceRbr < 999)
                            report.ReportPath = "Reports//InvoiceOneAuti.rdlc";
                        else
                            report.ReportPath = "Reports//InvoiceAuti.rdlc";
                    }
            }
            else
            {
                if (invoiceRbr < 999)
                    report.ReportPath = "Reports//InvoiceOne.rdlc";
                else
                    report.ReportPath = "Reports//Invoice.rdlc";
            }


           report.DataSources.Clear();
            this.report.DataSources.Add(rdSource);
            this.report.DataSources.Add(rdSource1);
            this.report.DataSources.Add(rdSource2);
            //if (invoiceRbr < 999)
                this.report.DataSources.Add(rdSource3);
            this.report.Refresh();


            report.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler1);


            report.Refresh();
            
            string nameRepVol = this.reportName;
            if (this.printing == true)
            {
                Run();
            }            
            if (this.exportToPdf == true)
            {
                ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                rg.GenerateOutputPDF(report, this.reportName);
            }
        }
        private void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("ItemsDS", this.dt2));
            e.DataSources.Add(new ReportDataSource("TravelWith", this.dt3));
        }

        private void SubreportProcessingEventHandler1(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("InvoideDS", this.dt1));
            e.DataSources.Add(new ReportDataSource("ItemsDS", this.dt2));
            //if (invoiceRbr < 999)
            //u ovom slucaju je dt4 jer je dt3 nesto drugo
                e.DataSources.Add(new ReportDataSource("TravelWith", this.dt4));
        }
        public void SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            DataSet dataSet = new DataSet();
            DataSet dataSet2 = new DataSet();
            DataSet dataSet3 = new DataSet();
            DataSet dataSet4 = new DataSet();

            DataTable dataTableCopy = new DataTable(); // kopiranje dataTable
            dataTableCopy = dt1.Copy();
            dataSet.Tables.Add(dataTableCopy);



            DataTable dataTableCopy2 = new DataTable(); // kopiranje dataTable
            dataTableCopy2 = dt2.Copy();
            dataSet2.Tables.Add(dataTableCopy2);


            DataTable dataTableCopy3 = new DataTable(); // kopiranje dataTable
            dataTableCopy3 = dt3.Copy();
            dataSet3.Tables.Add(dataTableCopy3);
            
            DataTable dataTableCopy4 = new DataTable(); // kopiranje dataTable
            dataTableCopy4 = dt4.Copy();
            dataSet4.Tables.Add(dataTableCopy4);


            
          
            switch (reportName.Substring(0, 2))
            {
                case "MM":
                                e.DataSources.Clear();
                                e.DataSources.Add(new ReportDataSource("MundoradoSubRepTravelerPaperDataSet", dataTableCopy));
                                e.DataSources.Add(new ReportDataSource("MundoradoClientDataSet", dataTableCopy2));
                                e.DataSources.Add(new ReportDataSource("MundoradoSubTekstDataSet", dataTableCopy3));
                                e.DataSources.Add(new ReportDataSource("MundoradoArrangementRemaining", dataTableCopy4));
                                break;
                case "AT":
                                e.DataSources.Clear();
                                e.DataSources.Add(new ReportDataSource("AutiTravelSubRepTravelerPaperDataSet", dataTableCopy));
                                e.DataSources.Add(new ReportDataSource("AutiTravelClientDataSet", dataTableCopy2));
                                e.DataSources.Add(new ReportDataSource("AutiTravelSubTekstDataSet", dataTableCopy3));
                                e.DataSources.Add(new ReportDataSource("ClientDataSet", dataTableCopy2));
                                e.DataSources.Add(new ReportDataSource("SubTekstDataSet", dataTableCopy3));
                                e.DataSources.Add(new ReportDataSource("ArrangementRemaining", dataTableCopy4));
                                break;
                case "AR":
                               
                               e.DataSources.Clear();
                               e.DataSources.Add(new ReportDataSource("AutiTravelSubRepTravelerPaperDataSet", dataTableCopy));
                               e.DataSources.Add(new ReportDataSource("AutiTravelClientDataSet", dataTableCopy2));
                               e.DataSources.Add(new ReportDataSource("AutiTravelSubTekstDataSet", dataTableCopy3));
                               //e.DataSources.Clear();
                               //e.DataSources.Add(new ReportDataSource("AutiTravelSubRepTravelerPaperDataSet", dataTableCopy));
                               //e.DataSources.Add(new ReportDataSource("AutiTravelClientDataSet", dataTableCopy2));
                               //e.DataSources.Add(new ReportDataSource("AutiTravelSubTekstDataSet", dataTableCopy3));
                               //e.DataSources.Add(new ReportDataSource("ClientDataSet", dataTableCopy2));
                               //e.DataSources.Add(new ReportDataSource("SubTekstDataSet", dataTableCopy3));
                               //e.DataSources.Add(new ReportDataSource("ArrangementRemaining", dataTableCopy4));
                              
                               break;
                case "BH":
                               e.DataSources.Clear();
                               e.DataSources.Add(new ReportDataSource("SubRepTravelerPaperDataSet", dataTableCopy));
                               e.DataSources.Add(new ReportDataSource("ClientDataSet", dataTableCopy2));
                               e.DataSources.Add(new ReportDataSource("SubTekstDataSet", dataTableCopy3));
                               e.DataSources.Add(new ReportDataSource("ArrangementRemaining", dataTableCopy4));
                               break;
            }


        }
    }
}
