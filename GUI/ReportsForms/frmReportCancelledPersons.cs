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
    public partial class frmReportCancelledPersons : Telerik.WinControls.UI.RadForm
    {
        string reportName;

        DataTable dataTable = new DataTable();


        //Konstruktor za 
        public frmReportCancelledPersons(DataTable dt15)
        {

            dataTable = dt15;
            InitializeComponent();

        }

        private void frmReportCancelledPersons_Load(object sender, EventArgs e)
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

                //Za brisanje vremena iz datuma
                int g = 0;
                string d = "";
                string p = "";
                dataTableCopy.Columns.Add("dtF", typeof(String));
                dataTableCopy.Columns.Add("dtT", typeof(String));
                foreach (DataRow dr in dataTableCopy.Rows)
                {
                    d = dr["dtFromArrangement"].ToString();
                    p = dr["dtToArrangement"].ToString();

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
                    //

                }

                dataTableCopy.Columns.Remove("dtFromArrangement");
                dataTableCopy.Columns.Remove("dtToArrangement");

                dataTableCopy.Columns["dtF"].ColumnName = "dtFromArrangement";
                dataTableCopy.Columns["dtT"].ColumnName = "dtToArrangement";
                ////end


                ReportDataSource rdSource = new ReportDataSource("CencelledPersonsDataSet", (DataTable)dataTable);

                rdSource.Value = dataTableCopy;

                this.reportViewer15.LocalReport.DataSources.Add(rdSource);



                // prikaz podataka
                this.reportViewer15.LocalReport.DataSources.Clear();
                this.reportViewer15.LocalReport.DataSources.Add(rdSource);
                this.reportViewer15.LocalReport.Refresh();
                this.reportViewer15.Refresh();

                ReportClassCencelledPersons tr = new ReportClassCencelledPersons();
                reportName = "Reports//ReportCancelledPersonsPreview.rdlc";

                tr.transl(reportName, dataTable);


                //Za putanju i uzimanje RDLC-a iz BIN foldera
                string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = Path.Combine(exeFolder, "Reports//ReportCancelledPersonsPreview.rdlc");
                reportViewer15.LocalReport.ReportPath = reportPath;
                reportViewer15.LocalReport.ReportEmbeddedResource = reportPath;

                ////generisanje PDF u pozadini !
                string nameRepTra = "";
                FileStream streamingReport = new FileStream(reportPath, FileMode.Open);
                reportViewer15.LocalReport.LoadReportDefinition(streamingReport);
                streamingReport.Close();
                ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                rg.GenerateOutputPDF(reportViewer15.LocalReport, nameRepTra);

                this.reportViewer15.RefreshReport();
            }


        }

       
    }
}
