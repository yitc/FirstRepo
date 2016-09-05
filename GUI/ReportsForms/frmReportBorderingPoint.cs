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
    public partial class frmReportBorderingPoint : Telerik.WinControls.UI.RadForm
    {
       string reportName;

        DataTable dataTable = new DataTable();


        //Konstruktor za 
        public frmReportBorderingPoint(DataTable dt14)
        {

            dataTable = dt14;
            InitializeComponent();

        }

        private void frmReportBorderingPoint_Load(object sender, EventArgs e)
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

                    //Za dodeljivanje polja iz modela i ArrangemnentBookDAO radi prikaza u report i za skrivanje polja
                    dr["nameArrangement1"] = dr["nameArrangement"];
                    dr["dtFromArrangement1"] = dr["dtFromArrangement"];
                    dr["dtToArrangement1"] = dr["dtToArrangement"];
                    dr["codeArrangement1"] = dr["codeArrangement"];
                    //
                }

                dataTableCopy.Columns.Remove("dtFromArrangement1");
                dataTableCopy.Columns.Remove("dtToArrangement1");

                dataTableCopy.Columns["dtF"].ColumnName = "dtFromArrangement1";
                dataTableCopy.Columns["dtT"].ColumnName = "dtToArrangement1";
                ////end


                ReportDataSource rdSource = new ReportDataSource("BorderingPointDataSet", (DataTable)dataTable);

                rdSource.Value = dataTableCopy;

                this.reportViewer14.LocalReport.DataSources.Add(rdSource);



                // prikaz podataka
                this.reportViewer14.LocalReport.DataSources.Clear();
                this.reportViewer14.LocalReport.DataSources.Add(rdSource);
                this.reportViewer14.LocalReport.Refresh();
                this.reportViewer14.Refresh();

                ReportClassBorderingPoint tr = new ReportClassBorderingPoint();
                reportName = "Reports//ReportBorderingPointPreview.rdlc";

                tr.transl(reportName, dataTable);


                //Za putanju i uzimanje RDLC-a iz BIN foldera
                string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = Path.Combine(exeFolder, "Reports//ReportBorderingPointPreview.rdlc");
                reportViewer14.LocalReport.ReportPath = reportPath;
                reportViewer14.LocalReport.ReportEmbeddedResource = reportPath;

                ////generisanje PDF u pozadini !
                string nameRepTra = "";
                FileStream streamingReport = new FileStream(reportPath, FileMode.Open);
                reportViewer14.LocalReport.LoadReportDefinition(streamingReport);
                streamingReport.Close();
                ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                rg.GenerateOutputPDF(reportViewer14.LocalReport, nameRepTra);

                this.reportViewer14.RefreshReport();
            }


        }

       
    }
}
