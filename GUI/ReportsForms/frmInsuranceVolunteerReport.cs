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
    public partial class frmInsuranceVolunteersReport : Telerik.WinControls.UI.RadForm
    {


        string reportName;
        private string namePdf;
        private DateTime fromr;
        private DateTime tor;

        DataTable dataTable = new DataTable();



        public frmInsuranceVolunteersReport(DataTable dt1,  DateTime from, DateTime to)
        {

            dataTable = dt1;
           
            fromr = from;
            tor = to;

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

        private void frmInsuranceVolunteersReport_Load(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet(); // deklarisanje dataSeta
            if (dataTable != null) // ako je datatable null
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

                    //Za prevod
                    // for (int i = 0; i < dataTableCopy.Columns.Count; i++)
                    //{

                    //    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    //    {
                    //        if (resxSet.GetString(dataTableCopy.Columns[i].ColumnName) != null)
                    //            dataTableCopy.Columns[i].ColumnName = resxSet.GetString(dataTableCopy.Columns[i].ColumnName);
                    //    }

                    //}
                    //Za prevod

                    ReportDataSource rdSource = new ReportDataSource("InsuranceVolunteersDS", (DataTable)dataTable);

                    rdSource.Value = dataTableCopy;

                    this.reportViewer1.LocalReport.DataSources.Add(rdSource);



                    // prikaz podataka
                    this.reportViewer1.LocalReport.DataSources.Clear();
                    this.reportViewer1.LocalReport.DataSources.Add(rdSource);
                    this.reportViewer1.LocalReport.Refresh();
                    this.reportViewer1.Refresh();


                    //Za putanju i uzimanje RDLC-a iz BIN foldera
                    string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                    string reportPath = Path.Combine(exeFolder, "Reports//InsuranceVolunteersReport.rdlc");
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