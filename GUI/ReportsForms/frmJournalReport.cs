using BIS.Model;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace GUI.ReportsForms
{
    public partial class frmJournalReport : Telerik.WinControls.UI.RadForm
    {
        AccLineModel InvoideDS;
        List<AccLineModel> Lines;
        private DataTable irep1;
        private DataTable iirep1;
        private string namePdf;
        private string range1;
        private string range2;
        private string order;
        private string user;


        public frmJournalReport(DataTable irep,string name, string r1, string r2, string or, string usr)
        {
            this.irep1 = irep;
            range1 = r1;
            range2 = r2;
            order = or;
            user = usr;

            namePdf = name;

            InitializeComponent();
            this.Icon = Login.iconForm;
        }

        private void frmJournalReport_Load(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet();
           
            DataTable dataTableCopy = new DataTable(); // kopiranje dataTable

            if (irep1 != null)
            {
                if (irep1.Rows.Count > 0)
                {
                    foreach (DataRow dr in irep1.Rows)
                    {
                        dr["cond1"] = range1;
                        dr["cond2"] = range2;
                        dr["cond3"] = order;
                        dr["userN"] = user;
                    }
                }

                dataTableCopy = irep1.Copy();
            }
         
            dataSet.Tables.Add(dataTableCopy);
         

            ReportDataSource rdSource = new ReportDataSource("Lines", (DataTable)dataTableCopy);
         
            rdSource.Value = dataTableCopy;
         
            
            this.reportViewer2.LocalReport.DataSources.Clear();
            this.reportViewer2.LocalReport.DataSources.Add(rdSource);
         
            this.reportViewer2.LocalReport.Refresh();
         

           // this.reportViewer2.Reset();
            reportViewer2.ProcessingMode = ProcessingMode.Local;
         //   reportViewer2.LocalReport.ReportPath = "Reports//Journal.rdlc";
            
           
         
          

         //   this.reportViewer2.ProcessingMode = ProcessingMode.Local;
       //     reportViewer2.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);


           // this.reportViewer2.LocalReport.Refresh();
 
           
            this.reportViewer2.RefreshReport();

            string nameRepVol = namePdf;
            ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
            rg.GenerateOutputPDF(reportViewer2.LocalReport, nameRepVol);
            this.reportViewer2.RefreshReport();
        }

        private void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("Lines", this.irep1));
           // e.DataSources.Add(new ReportDataSource("ItemsDS", this.iirep1));
        }

        
    }
}
