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
    public partial class frmCancelInsuranceReport : Telerik.WinControls.UI.RadForm
    {
        AccLineModel InvoideDS;
        List<AccLineModel> Lines;
        private DataTable irep1;
        private DataTable iirep1;
        private string namePdf;
   


        public frmCancelInsuranceReport(DataTable irep, string name)
        {
            this.irep1 = irep;

            namePdf = name;

            this.Icon = Login.iconForm;
            InitializeComponent();
        }

        private void frmCancelInsuranceReport_Load(object sender, EventArgs e)
        {
                 DataSet dataSet = new DataSet();
           
            DataTable dataTableCopy = new DataTable(); // kopiranje dataTable
            
            if (irep1 != null)
              dataTableCopy = irep1.Copy();
         
            dataSet.Tables.Add(dataTableCopy);
         

            ReportDataSource rdSource = new ReportDataSource("Insurance", (DataTable)dataTableCopy);
         
            rdSource.Value = dataTableCopy;
         
            
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rdSource);
         
            this.reportViewer1.LocalReport.Refresh();
         

           // this.reportViewer2.Reset();
            reportViewer1.ProcessingMode = ProcessingMode.Local;
               
            this.reportViewer1.RefreshReport();

            string nameRepVol = namePdf;
            ReportGenerateOutputPDF rg = new ReportGenerateOutputPDF();
            rg.GenerateOutputPDF(reportViewer1.LocalReport, nameRepVol);
            this.reportViewer1.RefreshReport();
          //  this.reportViewer1.RefreshReport();
        }

        private void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("Lines", this.irep1));
           // e.DataSources.Add(new ReportDataSource("ItemsDS", this.iirep1));
        }
        
    }
}
