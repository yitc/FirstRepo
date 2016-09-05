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
    public partial class frmReportOverviewBooking : Telerik.WinControls.UI.RadForm
    {
        string reportName;

        DataTable dataTable = new DataTable();


        //Konstruktor za NrOfPerEmployee
        public frmReportOverviewBooking(DataTable dt2)
        {

            dataTable = dt2;
            InitializeComponent();

        }

        private void frmReportOverviewBooking_Load(object sender, EventArgs e)
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

                // //Za brisanje vremena iz datuma
                int g = 0;
                string d = "";
                string p = "";
                // string h = "";
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
                    //DateTime dateB = Convert.ToDateTime(h);

                    p = dateT.ToString("dd/MM/yyyy");
                    d = date.ToString("dd/MM/yyyy");
                    // h = date.ToString("dd/MM/yyyy");
                    //dt.Rows[g]["dateFrom"] = d;
                    dataTableCopy.Rows[g]["dtF"] = d;
                    dataTableCopy.Rows[g]["dtT"] = p;
                    //dataTableCopy.Rows[g]["dtB"] = h;

                    g++;

                }

                dataTableCopy.Columns.Remove("dtFromArrangement");
                dataTableCopy.Columns.Remove("dtToArrangement");

                //dataTableCopy.Columns.Remove("birthdate");

                dataTableCopy.Columns["dtF"].ColumnName = "dtFromArrangement";
                dataTableCopy.Columns["dtT"].ColumnName = "dtToArrangement";
                //dataTableCopy.Columns["dtB"].ColumnName = "birthdate";
                // ////end


                ReportDataSource rdSource = new ReportDataSource("OverBookingDataSet", (DataTable)dataTable);

                rdSource.Value = dataTableCopy;

                this.reportViewer8.LocalReport.DataSources.Add(rdSource);



                // prikaz podataka
                this.reportViewer8.LocalReport.DataSources.Clear();
                this.reportViewer8.LocalReport.DataSources.Add(rdSource);
                this.reportViewer8.LocalReport.Refresh();
                this.reportViewer8.Refresh();

                ReportClassOverBooking tr = new ReportClassOverBooking();
                reportName = "Reports//ReportOverviwBookingPreview.rdlc";

                tr.transl(reportName, dataTable);


                //Za putanju i uzimanje RDLC-a iz BIN foldera
                string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = Path.Combine(exeFolder, "Reports//ReportOverviwBookingPreview.rdlc");
                reportViewer8.LocalReport.ReportPath = reportPath;
                reportViewer8.LocalReport.ReportEmbeddedResource = reportPath;

                ////generisanje PDF u pozadini !
                string nameRepTra = "";
                FileStream streamingReport = new FileStream(reportPath, FileMode.Open);
                reportViewer8.LocalReport.LoadReportDefinition(streamingReport);
                streamingReport.Close();
                ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                rg.GenerateOutputPDF(reportViewer8.LocalReport, nameRepTra);

                this.reportViewer8.RefreshReport();
            }


        }

    



    }
}

