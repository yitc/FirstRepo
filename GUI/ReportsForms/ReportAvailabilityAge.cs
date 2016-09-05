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
    public partial class ReportAvailabilityAge : Telerik.WinControls.UI.RadForm
    {
        string reportName;

        DataTable dataTable = new DataTable();


        //Konstruktor za VolAvailabilityAge
        public ReportAvailabilityAge(DataTable VolAvailabilityAge)
        {

            dataTable = VolAvailabilityAge;
            InitializeComponent();


        }

        private void ReportAvailabilityAge_Load(object sender, EventArgs e)
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

                ReportDataSource rdSource = new ReportDataSource("AgeCategoryDataSet", (DataTable)dataTable);

                rdSource.Value = dataTableCopy;

                this.reportViewer5.LocalReport.DataSources.Add(rdSource);

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
                this.reportViewer5.LocalReport.DataSources.Clear();
                this.reportViewer5.LocalReport.DataSources.Add(rdSource);
                this.reportViewer5.LocalReport.Refresh();
                this.reportViewer5.Refresh();

                ReportClassAgeList tr = new ReportClassAgeList();
                reportName = "Reports//VolAvailabilityAgePreview.rdlc";
                tr.transl(reportName, dataTable);


                //Za putanju i uzimanje RDLC-a iz BIN foldera
                string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = Path.Combine(exeFolder, "Reports//VolAvailabilityAgePreview.rdlc");
                reportViewer5.LocalReport.ReportPath = reportPath;
                reportViewer5.LocalReport.ReportEmbeddedResource = reportPath;

                ////generisanje PDF u pozadini!
                string nameRepTra = "";
                FileStream streamingReport = new FileStream(reportPath, FileMode.Open);
                reportViewer5.LocalReport.LoadReportDefinition(streamingReport);
                streamingReport.Close();
                ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                rg.GenerateOutputPDF(reportViewer5.LocalReport, nameRepTra);

                this.reportViewer5.RefreshReport();
            }
        }

    }
}