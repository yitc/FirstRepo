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
    public partial class ReportListWithExpired : Telerik.WinControls.UI.RadForm
    {
        //Parametri za ListWithExpired

        string reportName;

        DataTable dataTable = new DataTable();


        //Konstruktor za ListWithExpired
        public ReportListWithExpired(DataTable VogCokVogPass)
        {

            dataTable = VogCokVogPass;
            InitializeComponent();
            

        }

        private void ReportListWithExpired_Load(object sender, EventArgs e)
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
                dataTableCopy.Columns.Add("dtE", typeof(String));
               
                foreach (DataRow dr in dataTableCopy.Rows)
                {
                    d = dr["dtExpirationDate"].ToString();                  

                    DateTime date = Convert.ToDateTime(d);               
                                   
                    d = date.ToString("dd/MM/yyyy");                   
                    dataTableCopy.Rows[g]["dtE"] = d;
                  
                    g++;

                    //Za dodeljivanje polja iz modela i DAO radi prikaza u report i za skrivanje polja
                    dr["dateFrom1"] = dr["DateFrom"];
                    dr["dateTo1"] = dr["DateTo"];
                    //

                }

                dataTableCopy.Columns.Remove("dtExpirationDate");           
                dataTableCopy.Columns["dtE"].ColumnName = "dtExpirationDate";               
                //end

                ReportDataSource rdSource = new ReportDataSource("ListWithExpiredDataSet", (DataTable)dataTable);

                rdSource.Value = dataTableCopy;

                this.reportViewer6.LocalReport.DataSources.Add(rdSource);

                // prikaz podataka
                this.reportViewer6.LocalReport.DataSources.Clear();
                this.reportViewer6.LocalReport.DataSources.Add(rdSource);
                this.reportViewer6.LocalReport.Refresh();
                this.reportViewer6.Refresh();

                ReportClassListWithExpired tr = new ReportClassListWithExpired();
                reportName = "Reports//ListWithExpiredPreview.rdlc";
                tr.transl(reportName, dataTable);
               

                //Za putanju i uzimanje RDLC-a iz BIN foldera
                string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = Path.Combine(exeFolder, "Reports//ListWithExpiredPreview.rdlc");
                reportViewer6.LocalReport.ReportPath = reportPath;
                reportViewer6.LocalReport.ReportEmbeddedResource = reportPath;

                ////generisanje PDF u pozadini!
                string nameRepTra = "";
                FileStream streamingReport = new FileStream(reportPath, FileMode.Open);
                reportViewer6.LocalReport.LoadReportDefinition(streamingReport);
                streamingReport.Close();
                ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
                rg.GenerateOutputPDF(reportViewer6.LocalReport, nameRepTra);

                this.reportViewer6.RefreshReport();
            }

            
        }
    }
}
