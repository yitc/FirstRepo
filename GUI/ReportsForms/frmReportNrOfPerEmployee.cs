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
    public partial class frmReportNrOfPerEmployee : Telerik.WinControls.UI.RadForm
    {
        string reportName;

        DataTable dataTable = new DataTable();


        //Konstruktor za NrOfPerEmployee
        public frmReportNrOfPerEmployee(DataTable NrPerEmployee)
        {

            dataTable = NrPerEmployee;
            InitializeComponent();

        }

        private void frmReportNrOfPerEmployee_Load(object sender, EventArgs e)
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

                    //Za brisanje vremena iz datuma
                    int g = 0;
                    string d = "";
                    string p = "";
                    dataTableCopy.Columns.Add("dtF", typeof(String));
                    dataTableCopy.Columns.Add("dtT", typeof(String));
                    foreach (DataRow dr in dataTableCopy.Rows)
                    {
                        d = dr["dateFrom"].ToString();
                        p = dr["dateTo"].ToString();

                        DateTime date = Convert.ToDateTime(d);
                        DateTime dateT = Convert.ToDateTime(p);

                        p = dateT.ToString("dd/MM/yyyy");
                        d = date.ToString("dd/MM/yyyy");
                        //dt.Rows[g]["dateFrom"] = d;
                        dataTableCopy.Rows[g]["dtF"] = d;
                        dataTableCopy.Rows[g]["dtT"] = p;


                        g++;

                        //Za dodeljivanje polja iz modela i DAO radi prikaza u report i za skrivanje polja
                        dr["dateFrom1"] = dr["DateFrom"];
                        dr["dateTo1"] = dr["DateTo"];
                        dr["Employee1"] = dr["Employee"];
                        //

                    }

                    dataTableCopy.Columns.Remove("dateFrom");
                    dataTableCopy.Columns.Remove("dateTo");

                    dataTableCopy.Columns["dtF"].ColumnName = "dateFrom";
                    dataTableCopy.Columns["dtT"].ColumnName = "dateTo";
                    ////end


                    ReportDataSource rdSource = new ReportDataSource("NrOfPerEmployee", (DataTable)dataTable);

                    rdSource.Value = dataTableCopy;

                    this.reportViewer11.LocalReport.DataSources.Add(rdSource);



                    // prikaz podataka
                    this.reportViewer11.LocalReport.DataSources.Clear();
                    this.reportViewer11.LocalReport.DataSources.Add(rdSource);
                    this.reportViewer11.LocalReport.Refresh();
                    this.reportViewer11.Refresh();

                    ReportClassNrPerOfEmployee tr = new ReportClassNrPerOfEmployee();
                    reportName = "Reports//ReportNrOfPerEmployee.rdlc";

                    tr.transl(reportName, dataTable);


                    //Za putanju i uzimanje RDLC-a iz BIN foldera
                    string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                    string reportPath = Path.Combine(exeFolder, "Reports//ReportNrOfPerEmployee.rdlc");
                    reportViewer11.LocalReport.ReportPath = reportPath;
                    reportViewer11.LocalReport.ReportEmbeddedResource = reportPath;

                    ////generisanje PDF u pozadini !
                    string nameRepTra = "";
                    FileStream streamingReport = new FileStream(reportPath, FileMode.Open);
                    reportViewer11.LocalReport.LoadReportDefinition(streamingReport);
                    streamingReport.Close();
                    ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                    rg.GenerateOutputPDF(reportViewer11.LocalReport, nameRepTra);

                    this.reportViewer11.RefreshReport();
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
