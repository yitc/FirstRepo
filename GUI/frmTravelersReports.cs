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
    public partial class frmTravelersReports : Telerik.WinControls.UI.RadForm
    {
        string reportName;

        DataTable dataTable = new DataTable();
        DataTable dataTableNew = new DataTable();
        DataTable dataTableNew2 = new DataTable();

        string res = "";
        public frmTravelersReports(DataTable dt1, DataTable dt2, DataTable dt3)
        {
            dataTable = dt1;
            dataTableNew = dt2;
            dataTableNew2 = dt3;
            InitializeComponent();
        }

        private void frmTravelersReports_Load(object sender, EventArgs e)
        {
           
            if (dataTable.Rows.Count == 0)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("There is nothing for print preview!");

                this.Close();
            }
            else
            {
                // prikaz podataka                
                this.reportViewer1.LocalReport.DataSources.Clear();
                DataSet dataSet = new DataSet();

                DataTable dataTableCopy = new DataTable(); // kopiranje dataTable
                dataTableCopy = dataTable.Copy();
                dataSet.Tables.Add(dataTableCopy);

                ReportDataSource rdSource = new ReportDataSource("SubRepTravelerPaperDataSet", (DataTable)dataTable);
                rdSource.Value = dataTableCopy;
                this.reportViewer1.LocalReport.DataSources.Add(rdSource);
             

                //Za putanju i uzimanje RDLC-a iz BIN foldera
                string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = Path.Combine(exeFolder, "Reports//SubRepTravelerPapers.rdlc");
                reportViewer1.LocalReport.ReportPath = reportPath;
                reportViewer1.LocalReport.ReportEmbeddedResource = reportPath;



                this.reportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessing);



                this.reportViewer1.LocalReport.Refresh();
                this.reportViewer1.RefreshReport();
            }

        }

        void SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            DataSet dataSet = new DataSet();
            DataSet dataSet2 = new DataSet();
            DataSet dataSet3 = new DataSet();

            DataTable dataTableCopy = new DataTable(); // kopiranje dataTable
            dataTableCopy = dataTable.Copy();
            dataSet.Tables.Add(dataTableCopy);



            DataTable dataTableCopy2 = new DataTable(); // kopiranje dataTable
            dataTableCopy2 = dataTableNew.Copy();
            dataSet2.Tables.Add(dataTableCopy2);


            DataTable dataTableCopy3 = new DataTable(); // kopiranje dataTable
            dataTableCopy3 = dataTableNew2.Copy();
            dataSet3.Tables.Add(dataTableCopy3);


            e.DataSources.Clear();
            e.DataSources.Add(new ReportDataSource("SubRepTravelerPaperDataSet", dataTableCopy));
            e.DataSources.Add(new ReportDataSource("ClientDataSet", dataTableCopy2));
            e.DataSources.Add(new ReportDataSource("SubTekstDataSet", dataTableCopy3));
         
        }

    }
}
