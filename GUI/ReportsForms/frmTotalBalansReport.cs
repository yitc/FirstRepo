using BIS.Model;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace GUI.ReportsForms
{
    public partial class frmTotalBalansReport : Telerik.WinControls.UI.RadForm
    {
        AccLineModel InvoideDS;
        List<AccLineModel> Lines;
        private DataTable irep1;
        private DataTable iirep1;
        private string namePdf;



        public frmTotalBalansReport(DataTable irep, string name)
        {
            this.irep1 = irep;

            namePdf = name;
       

            this.Icon = Login.iconForm;

            string nameForm = this.Text.Substring(0);

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(nameForm) != null)
                    nameForm = resxSet.GetString(nameForm);
            }
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + nameForm;

            InitializeComponent();
        }

        private void frmTotalBalansReport_Load(object sender, EventArgs e)
        {
                 DataSet dataSet = new DataSet();
           
            DataTable dataTableCopy = new DataTable(); // kopiranje dataTable
            
            if (irep1 != null)
              dataTableCopy = irep1.Copy();
         
            dataSet.Tables.Add(dataTableCopy);
         

            ReportDataSource rdSource = new ReportDataSource("Saldo", (DataTable)dataTableCopy);
         
            rdSource.Value = dataTableCopy;
         
            
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rdSource);
         
            this.reportViewer1.LocalReport.Refresh();
         

           // this.reportViewer2.Reset();
            reportViewer1.ProcessingMode = ProcessingMode.Local;
       //     reportViewer1.LocalReport.ReportPath = "Reports//LedgerReport.rdlc";
            
           
         
          

         //   this.reportViewer2.ProcessingMode = ProcessingMode.Local;
       //     reportViewer2.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);


           // this.reportViewer2.LocalReport.Refresh();
 
           
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
