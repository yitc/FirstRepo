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
    public partial class frmReportStagglersList : Telerik.WinControls.UI.RadForm
    {
        string reportName;

        DataTable dataTable = new DataTable();


        //Konstruktor za 
        public frmReportStagglersList(DataTable dt10)
        {

            dataTable = dt10;
            InitializeComponent();

        }

        private void frmReportStagglersList_Load(object sender, EventArgs e)
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
                //string h = "";
                dataTableCopy.Columns.Add("dtF", typeof(String));
                dataTableCopy.Columns.Add("dtT", typeof(String));
                //dataTableCopy.Columns.Add("dtB", typeof(String));
                foreach (DataRow dr in dataTableCopy.Rows)
                {
                    d = dr["dtFromArrangement"].ToString();
                    p = dr["dtToArrangement"].ToString();
                   // h = dr["birthdate"].ToString();
                   //if(h=="" )
                   //{
                   //    h = DateTime.MinValue.ToString();
                   //}
                    DateTime date = Convert.ToDateTime(d);
                    DateTime dateT = Convert.ToDateTime(p);
                   // DateTime dateB = Convert.ToDateTime(h);

                    p = dateT.ToString("dd/MM/yyyy");
                    d = date.ToString("dd/MM/yyyy");
                   // h = date.ToString("dd/MM/yyyy");
                    //dt.Rows[g]["dateFrom"] = d;
                    dataTableCopy.Rows[g]["dtF"] = d;
                    dataTableCopy.Rows[g]["dtT"] = p;
                    //dataTableCopy.Rows[g]["dtB"] = h;

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

                //dataTableCopy.Columns.Remove("birthdate");

                dataTableCopy.Columns["dtF"].ColumnName = "dtFromArrangement1";
                dataTableCopy.Columns["dtT"].ColumnName = "dtToArrangement1";
                //dataTableCopy.Columns["dtB"].ColumnName = "birthdate";
                ////end


                ReportDataSource rdSource = new ReportDataSource("StagglersListDataSet", (DataTable)dataTable);

                rdSource.Value = dataTableCopy;

                this.reportViewer10.LocalReport.DataSources.Add(rdSource);



                // prikaz podataka
                this.reportViewer10.LocalReport.DataSources.Clear();
                this.reportViewer10.LocalReport.DataSources.Add(rdSource);
                this.reportViewer10.LocalReport.Refresh();
                this.reportViewer10.Refresh();

                ReportClassStagglerList tr = new ReportClassStagglerList();
                reportName = "Reports//ReportStagglersList.rdlc";

                tr.transl(reportName, dataTable);


                //Za putanju i uzimanje RDLC-a iz BIN foldera
                string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = Path.Combine(exeFolder, "Reports//ReportStagglersList.rdlc");
                reportViewer10.LocalReport.ReportPath = reportPath;
                reportViewer10.LocalReport.ReportEmbeddedResource = reportPath;

                ////generisanje PDF u pozadini !
                string nameRepTra = "";
                FileStream streamingReport = new FileStream(reportPath, FileMode.Open);
                reportViewer10.LocalReport.LoadReportDefinition(streamingReport);
                streamingReport.Close();
                ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                rg.GenerateOutputPDF(reportViewer10.LocalReport, nameRepTra);

                this.reportViewer10.RefreshReport();
            }


        }
    }
}
