namespace GUI
{
    partial class frmInvoiceOneReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.InvoiceReportModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.InvoiceItemsReportModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceReportModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceItemsReportModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // InvoiceReportModelBindingSource
            // 
            this.InvoiceReportModelBindingSource.DataSource = typeof(BIS.Model.InvoiceReportModel);
            // 
            // InvoiceItemsReportModelBindingSource
            // 
            this.InvoiceItemsReportModelBindingSource.DataSource = typeof(BIS.Model.InvoiceItemsReportModel);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer1.AutoSize = true;
            reportDataSource1.Name = "InvoideDS";
            reportDataSource1.Value = this.InvoiceReportModelBindingSource;
            reportDataSource2.Name = "InvoiceItemsDS";
            reportDataSource2.Value = this.InvoiceItemsReportModelBindingSource;
            reportDataSource3.Name = "InvoiceItemsOne";
            reportDataSource3.Value = this.InvoiceItemsReportModelBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.EnableHyperlinks = true;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.InvoiceOne.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 12);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(838, 581);
            this.reportViewer1.TabIndex = 0;
            // 
            // frmInvoiceOneReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 621);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmInvoiceOneReport";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Factuur";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmInvoiceOneReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceReportModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceItemsReportModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource InvoiceItemsReportModelBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource InvoiceReportModelBindingSource;
    }
}
