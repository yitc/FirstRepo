namespace GUI.ReportsForms
{
    partial class frmTotalBalansReport
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
            this.LedgerAccountModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SaldoBalansModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.LedgerAccountModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaldoBalansModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // LedgerAccountModelBindingSource
            // 
            this.LedgerAccountModelBindingSource.DataSource = typeof(BIS.Model.LedgerAccountModel);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Saldo";
            reportDataSource1.Value = this.SaldoBalansModelBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.Reports.TotalBalansSelectionReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1122, 625);
            this.reportViewer1.TabIndex = 0;
            // 
            // SaldoBalansModelBindingSource
            // 
            this.SaldoBalansModelBindingSource.DataSource = typeof(BIS.Model.SaldoBalansModel);
            // 
            // frmTotalBalansReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 625);
            this.Controls.Add(this.reportViewer1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmTotalBalansReport";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Total balans report";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmTotalBalansReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LedgerAccountModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SaldoBalansModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource LedgerAccountModelBindingSource;
        private System.Windows.Forms.BindingSource SaldoBalansModelBindingSource;

    }
}
