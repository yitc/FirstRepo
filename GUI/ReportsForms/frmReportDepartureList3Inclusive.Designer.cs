namespace GUI
{
    partial class frmReportDepartureList3Inclusive
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
            this.reportViewer70 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DepartureList1ModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DepartureList1ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer70
            // 
            this.reportViewer70.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DepartureList3InclusiveDataSet";
            reportDataSource1.Value = this.DepartureList1ModelBindingSource;
            this.reportViewer70.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer70.LocalReport.ReportEmbeddedResource = "GUI.ReportDepartureList3InclusivePreview.rdlc";
            this.reportViewer70.Location = new System.Drawing.Point(0, 0);
            this.reportViewer70.Name = "reportViewer70";
            this.reportViewer70.Size = new System.Drawing.Size(1203, 360);
            this.reportViewer70.TabIndex = 0;
            // 
            // DepartureList1ModelBindingSource
            // 
            this.DepartureList1ModelBindingSource.DataSource = typeof(BIS.Model.DepartureList1Model);
            // 
            // frmReportDepartureList3Inclusive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 360);
            this.Controls.Add(this.reportViewer70);
            this.Name = "frmReportDepartureList3Inclusive";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "frmReportDepartureList3Inclusive";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmReportDepartureList3Inclusive_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DepartureList1ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer70;
        private System.Windows.Forms.BindingSource DepartureList1ModelBindingSource;
    }
}
