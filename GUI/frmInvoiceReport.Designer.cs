namespace GUI
{
    partial class frmInvoiceReport
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
            this.InvoiceReportModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.invoiceViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceReportModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // InvoiceReportModelBindingSource
            // 
            this.InvoiceReportModelBindingSource.DataSource = typeof(BIS.Model.InvoiceReportModel);
            // 
            // invoiceViewer
            // 
            this.invoiceViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "InvoideDS";
            reportDataSource1.Value = this.InvoiceReportModelBindingSource;
            this.invoiceViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.invoiceViewer.LocalReport.EnableHyperlinks = true;
            this.invoiceViewer.LocalReport.ReportEmbeddedResource = "GUI.Invoice.rdlc";
            this.invoiceViewer.Location = new System.Drawing.Point(0, 0);
            this.invoiceViewer.Name = "invoiceViewer";
            this.invoiceViewer.Size = new System.Drawing.Size(893, 748);
            this.invoiceViewer.TabIndex = 0;
            // 
            // frmInvoiceReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 748);
            this.Controls.Add(this.invoiceViewer);
            this.Name = "frmInvoiceReport";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Faktuur";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmInvoiceReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceReportModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource InvoiceReportModelBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer invoiceViewer;
    }
}
