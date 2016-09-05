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
    public partial class frmBH_Reizigers_Vlieg_Binnen_EU : Telerik.WinControls.UI.RadForm
    {
        string reportName;

        DataTable dataTable = new DataTable();
        DataTable dataTableNew = new DataTable();
        DataTable dataTableNew2 = new DataTable();
        DataTable dataTableNew3 = new DataTable();

        string res = "";
        public frmBH_Reizigers_Vlieg_Binnen_EU(DataTable dt1, DataTable dt2, DataTable dt3, DataTable dt4)
        {
            dataTable = dt1;
            dataTableNew = dt2;
            dataTableNew2 = dt3;
            dataTableNew3 = dt4;
            InitializeComponent();
            this.Icon = Login.iconForm;
           
            string name = this.Name.Substring(3);

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(name) != null)
                    name = resxSet.GetString(name);
            }
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + name;
        }

        private void frmBH_Reizigers_Vlieg_Binnen_EU_Load(object sender, EventArgs e)
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
                string reportPath = Path.Combine(exeFolder, "C:\\TravelPapers\\BH_Reizigers_Vlieg_Binnen_EU.rdlc");
                reportViewer1.LocalReport.ReportPath = reportPath;
                reportViewer1.LocalReport.ReportEmbeddedResource = reportPath;



                this.reportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessing);



                this.reportViewer1.LocalReport.Refresh();
                this.reportViewer1.RefreshReport();
            }

        }

        void SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
     

            DataTable dataTableCopy = new DataTable(); // kopiranje dataTable
            if(dataTable!=null)
            dataTableCopy = dataTable.Copy();
      

            DataTable dataTableCopy2 = new DataTable(); // kopiranje dataTable
            if (dataTableNew != null)
            dataTableCopy2 = dataTableNew.Copy();
           

            DataTable dataTableCopy3 = new DataTable(); // kopiranje dataTable
            if (dataTableNew2 != null)
            dataTableCopy3 = dataTableNew2.Copy();
           

            DataTable dataTableCopy4 = new DataTable(); // kopiranje dataTable
            if (dataTableNew3 != null)
            dataTableCopy4 = dataTableNew3.Copy();
           

            e.DataSources.Clear();
            e.DataSources.Add(new ReportDataSource("SubRepTravelerPaperDataSet", dataTableCopy));
            e.DataSources.Add(new ReportDataSource("ClientDataSet", dataTableCopy2));
            e.DataSources.Add(new ReportDataSource("SubTekstDataSet", dataTableCopy3));
            e.DataSources.Add(new ReportDataSource("ArrangementRemaining", dataTableCopy4));
         
        }

    }
}
