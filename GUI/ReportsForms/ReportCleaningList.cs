using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Business;
using BIS.DAO;
using System.IO;
using BIS.Model;
using System.Xml;


namespace GUI
{
    public partial class ReportCleaningList : Telerik.WinControls.UI.RadForm
    {
        //Parametri za CleaningList

        string reportName;

        DataTable dataTable = new DataTable();


        //Konstruktor za CleaningList
        public ReportCleaningList(DataTable VolAvailabilityClining)
        {

            dataTable = VolAvailabilityClining;
            InitializeComponent();
            

        }

        private void CleaningListReport_Load(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet(); // deklarisanje dataSeta


            if (dataTable.Rows.Count == 0)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("There is nothing for print preview!");

                this.Close();
            }

            else
            {
                DataTable dataTableCopy = new DataTable(); // kopiranje dataTable
                dataTableCopy = dataTable.Copy();
                dataSet.Tables.Add(dataTableCopy);

                ReportDataSource rdSource = new ReportDataSource("CleaningListDataSet", (DataTable)dataTable);

                rdSource.Value = dataTableCopy;

                this.reportViewer7.LocalReport.DataSources.Add(rdSource);

                int g = 0;
                foreach (DataRow dr in dataTableCopy.Rows)
                {
                    g++;

                    //Za dodeljivanje polja iz modela i DAO radi prikaza u report i za skrivanje polja
                    dr["dateFrom1"] = dr["DateFrom"];
                    dr["dateTo1"] = dr["DateTo"];
                    //


                }

                // prikaz podataka
                this.reportViewer7.LocalReport.DataSources.Clear();
                this.reportViewer7.LocalReport.DataSources.Add(rdSource);
                this.reportViewer7.LocalReport.Refresh();
                this.reportViewer7.Refresh();

                ReportClassCleaningList tr = new ReportClassCleaningList();
                reportName = "Reports//CleaningListPreview.rdlc";
                tr.transl(reportName, dataTable);
               

                //Za putanju i uzimanje RDLC-a iz BIN foldera
                string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = Path.Combine(exeFolder, "Reports//CleaningListPreview.rdlc");
                reportViewer7.LocalReport.ReportPath = reportPath;
                reportViewer7.LocalReport.ReportEmbeddedResource = reportPath;

                ////generisanje PDF u pozadini!
                string nameRepTra = "";
                FileStream streamingReport = new FileStream(reportPath, FileMode.Open);
                reportViewer7.LocalReport.LoadReportDefinition(streamingReport);
                streamingReport.Close();
                ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                rg.GenerateOutputPDF(reportViewer7.LocalReport, nameRepTra);

                this.reportViewer7.RefreshReport();
            }

            
        }
    }
}
