using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.IO;

namespace GUI
{
    public partial class frmReportOpenLines : Telerik.WinControls.UI.RadForm
    {

        string reportName;
        DataTable dataTable = new DataTable();


        public frmReportOpenLines(DataTable dt28)
        {
            dataTable = dt28;
            InitializeComponent();
        }

        private void frmReportOpenLines_Load(object sender, EventArgs e)
        {

            DataSet dataSet = new DataSet(); // deklarisanje dataSeta
            if (dataTable != null)
            {

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

                    ReportDataSource rdSource = new ReportDataSource("OpenLines", (DataTable)dataTable);

                    rdSource.Value = dataTableCopy;

                    this.reportViewer1.LocalReport.DataSources.Add(rdSource);



                    // prikaz podataka
                    this.reportViewer1.LocalReport.DataSources.Clear();
                    this.reportViewer1.LocalReport.DataSources.Add(rdSource);
                    this.reportViewer1.LocalReport.Refresh();
                    this.reportViewer1.Refresh();
                    
                    //Za putanju i uzimanje RDLC-a iz BIN foldera
                    string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                    string reportPath = Path.Combine(exeFolder, "Reports//OpenLines.rdlc");
                    reportViewer1.LocalReport.ReportPath = reportPath;
                    reportViewer1.LocalReport.ReportEmbeddedResource = reportPath;

                    ////generisanje PDF u pozadini !
                    string nameRepTra = "";
                    FileStream streamingReport = new FileStream(reportPath, FileMode.Open);
                    reportViewer1.LocalReport.LoadReportDefinition(streamingReport);
                    streamingReport.Close();
                    ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                    rg.GenerateOutputPDF(reportViewer1.LocalReport, nameRepTra);

                    this.reportViewer1.RefreshReport();
                }

               
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("There is nothing for print preview!");

                this.Close();
            }            
        }
    }
}
