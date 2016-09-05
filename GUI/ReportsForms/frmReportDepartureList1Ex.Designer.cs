namespace GUI
{
    partial class frmReportDepartureList1Ex
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
            this.reportViewer63 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DepartureList1ModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DepartureList1ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer63
            // 
            this.reportViewer63.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DepartureList1ExDataSet";
            reportDataSource1.Value = this.DepartureList1ModelBindingSource;
            this.reportViewer63.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer63.LocalReport.ReportEmbeddedResource = "GUI.ReportDepartureList1ExPreview.rdlc";
            this.reportViewer63.Location = new System.Drawing.Point(0, 0);
            this.reportViewer63.Name = "reportViewer63";
            this.reportViewer63.Size = new System.Drawing.Size(1207, 441);
            this.reportViewer63.TabIndex = 0;
            // 
            // DepartureList1ModelBindingSource
            // 
            this.DepartureList1ModelBindingSource.DataSource = typeof(BIS.Model.DepartureList1Model);
            // 
            // frmReportDepartureList1Ex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 441);
            this.Controls.Add(this.reportViewer63);
            this.Name = "frmReportDepartureList1Ex";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "frmReportDepartureList1Ex";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmReportDepartureList1Ex_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DepartureList1ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer63;
        private System.Windows.Forms.BindingSource DepartureList1ModelBindingSource;
    }
}
