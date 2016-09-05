namespace GUI.ReportsForms
{
    partial class frmInsuranceWithoutArrGroupReport
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.InsuranceReportModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.LedgerAccountModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.InvoiceItemsReportModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.InvoiceReportModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ArrangementTravelersModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InsuranceReportModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LedgerAccountModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceItemsReportModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceReportModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ArrangementTravelersModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            reportDataSource1.Name = "Insurance";
            reportDataSource1.Value = this.InsuranceReportModelBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.Reports.InsuranceWithoutArrGroupReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1122, 625);
            this.reportViewer1.TabIndex = 0;
            // 
            // InsuranceReportModelBindingSource
            // 
            this.InsuranceReportModelBindingSource.DataSource = typeof(BIS.Model.InsuranceReportModel);
            // 
            // LedgerAccountModelBindingSource
            // 
            this.LedgerAccountModelBindingSource.DataSource = typeof(BIS.Model.LedgerAccountModel);
            // 
            // InvoiceItemsReportModelBindingSource
            // 
            this.InvoiceItemsReportModelBindingSource.DataSource = typeof(BIS.Model.InvoiceItemsReportModel);
            // 
            // InvoiceReportModelBindingSource
            // 
            this.InvoiceReportModelBindingSource.DataSource = typeof(BIS.Model.InvoiceReportModel);
            // 
            // ArrangementTravelersModelBindingSource
            // 
            this.ArrangementTravelersModelBindingSource.DataSource = typeof(BIS.Model.ArrangementTravelersModel);
            // 
            // frmInsuranceWithoutArrGroupReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 625);
            this.Controls.Add(this.reportViewer1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmInsuranceWithoutArrGroupReport";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InsuranceReport";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmInsuranceWithoutArrGroupReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.InsuranceReportModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LedgerAccountModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceItemsReportModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceReportModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ArrangementTravelersModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource LedgerAccountModelBindingSource;
        private System.Windows.Forms.BindingSource InvoiceItemsReportModelBindingSource;
        private System.Windows.Forms.BindingSource InvoiceReportModelBindingSource;
        private System.Windows.Forms.BindingSource ArrangementTravelersModelBindingSource;
        private System.Windows.Forms.BindingSource InsuranceReportModelBindingSource;

    }
}
