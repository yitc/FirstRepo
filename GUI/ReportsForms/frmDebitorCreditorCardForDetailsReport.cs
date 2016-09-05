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
    public partial class frmDebitorCreditorCardForDetailsReport : Telerik.WinControls.UI.RadForm
    {
        //Parametri za 

        string reportName;
        private string namePdf;
        private string fromr;
        private string tor;
        DataTable dataTable = new DataTable();
        private bool isBalansr;
        private bool sumr;
        //DataTable dataTableS = new DataTable();


        public frmDebitorCreditorCardForDetailsReport(string from, string to, bool debr, bool isBalans, bool sum, DateTime dtFrom, DateTime dtTo, DataTable dt1)
        {

            dataTable = dt1;
            isBalansr = isBalans ;
            fromr = from;
            tor = to;
            sumr = sum;
            

            InitializeComponent();
            this.Icon = Login.iconForm;

            string nameForm = this.Text.Substring(0);

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(nameForm) != null)
                    nameForm = resxSet.GetString(nameForm);
            }
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + nameForm;

        }

        private void frmDebitorCreditorCardForDetailsReport_Load(object sender, EventArgs e)
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

                    ReportParameter[] parameters = new ReportParameter[1];
                    if (isBalansr == true)
                    {
                        parameters[0] = new ReportParameter("IsBalans", "True");
                    }
                    else
                    {
                        parameters[0] = new ReportParameter("IsBalans", "False");
                    }




                    DataTable dataTableCopy = new DataTable(); // kopiranje dataTable
                    dataTableCopy = dataTable.Copy();
                    dataSet.Tables.Add(dataTableCopy);


                    ReportDataSource rdSource = new ReportDataSource("DebCreCardDetailDS", (DataTable)dataTable);

                    rdSource.Value = dataTableCopy;

                    this.reportViewer1.LocalReport.DataSources.Add(rdSource);


                    // prikaz podataka
                    this.reportViewer1.LocalReport.DataSources.Clear();
                    this.reportViewer1.LocalReport.DataSources.Add(rdSource);

                    this.reportViewer1.LocalReport.Refresh();
                    //this.reportViewer1.LocalReport.SetParameters(parameters);
                    this.reportViewer1.Refresh();


                    //Za putanju i uzimanje RDLC-a iz BIN foldera
                    string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);

                    string reportPath = Path.Combine(exeFolder, "Reports//DebitorCreditorCardForDetailsReport.rdlc");
                    if (sumr == true)
                    {
                        reportPath = Path.Combine(exeFolder, "Reports//DebitorSumReport.rdlc");
                    }
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


                    this.reportViewer1.LocalReport.SetParameters(parameters);


                }
            }

        }


    }
}
