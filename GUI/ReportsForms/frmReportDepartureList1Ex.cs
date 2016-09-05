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
using System.Resources;


namespace GUI
{
    public partial class frmReportDepartureList1Ex : Telerik.WinControls.UI.RadForm
    {
        string reportName;

        DataTable dataTable = new DataTable();


        //Konstruktor za 
        public frmReportDepartureList1Ex(DataTable dt63)
        {

            dataTable = dt63;
            InitializeComponent();

        }

        private void frmReportDepartureList1Ex_Load(object sender, EventArgs e)
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




                    ReportDataSource rdSource = new ReportDataSource("DepartureList1ExDataSet", (DataTable)dataTable);

                    rdSource.Value = dataTableCopy;

                    this.reportViewer63.LocalReport.DataSources.Add(rdSource);

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
                    this.reportViewer63.LocalReport.DataSources.Clear();
                    this.reportViewer63.LocalReport.DataSources.Add(rdSource);
                    this.reportViewer63.LocalReport.Refresh();
                    this.reportViewer63.Refresh();

                    ReportClassDepartureList1Ex tr = new ReportClassDepartureList1Ex();
                     
                    reportName = "Reports//ReportDepartureList1ExPreview.rdlc";

                    tr.transl(reportName, dataTable);


                    //Za putanju i uzimanje RDLC-a iz BIN foldera
                    string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                    string reportPath = Path.Combine(exeFolder, "Reports//ReportDepartureList1ExPreview.rdlc");
                    reportViewer63.LocalReport.ReportPath = reportPath;
                    reportViewer63.LocalReport.ReportEmbeddedResource = reportPath;


                    //// za prevod dijalogBoxa
                    //string newName = "";
                    //string zika = "ReportDepartureList";
                    //using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    //{
                    //    if (resxSet.GetString(zika) != null)

                    //        newName = zika;
                    //}


                    //reportViewer3.LocalReport.DisplayName = newName;

                    //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                    //saveFileDialog1.FileName = newName;


                    ////Prevod dijaloga

                    ////generisanje PDF u pozadini !
                    string nameRepTra = "";
                    FileStream streamingReport = new FileStream(reportPath, FileMode.Open);
                    reportViewer63.LocalReport.LoadReportDefinition(streamingReport);
                    streamingReport.Close();
                    ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                    rg.GenerateOutputPDF(reportViewer63.LocalReport, nameRepTra);




                    this.reportViewer63.RefreshReport();
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

