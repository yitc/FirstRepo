namespace GUI
{
    partial class frmReportDepartureList4Inclusive
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
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.reportViewer72 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DepartureList1ModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DepartureList1ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer72
            // 
            this.reportViewer72.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DepartureList4InclusiveDataSet";
            reportDataSource1.Value = this.DepartureList1ModelBindingSource;
            this.reportViewer72.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer72.LocalReport.ReportEmbeddedResource = "GUI.ReportDepartureList4Inclusive.rdlc";
            this.reportViewer72.Location = new System.Drawing.Point(0, 0);
            this.reportViewer72.Name = "reportViewer72";
            this.reportViewer72.Size = new System.Drawing.Size(1211, 359);
            this.reportViewer72.TabIndex = 0;
            // 
            // DepartureList1ModelBindingSource
            // 
            this.DepartureList1ModelBindingSource.DataSource = typeof(BIS.Model.DepartureList1Model);
            // 
            // frmReportDepartureList4Inclusive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1211, 359);
            this.Controls.Add(this.reportViewer72);
            this.Name = "frmReportDepartureList4Inclusive";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "frmReportDepartureList4Inclusive";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmReportDepartureList4Inclusive_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DepartureList1ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer72;
        private System.Windows.Forms.BindingSource DepartureList1ModelBindingSource;
    }
}
