namespace GUI
{
    partial class frmReportDepartureList2Ex
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
            this.DepartureList2ModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.reportViewer28 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.DepartureList2ModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // DepartureList2ModelBindingSource
            // 
            this.DepartureList2ModelBindingSource.DataSource = typeof(BIS.Model.DepartureList2ModelEx);
            // 
            // reportViewer28
            // 
            this.reportViewer28.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DepartureList2DataSet";
            reportDataSource1.Value = this.DepartureList2ModelBindingSource;
            this.reportViewer28.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer28.LocalReport.ReportEmbeddedResource = "GUI.ReportDepartureList2Preview.rdlc";
            this.reportViewer28.Location = new System.Drawing.Point(0, 0);
            this.reportViewer28.Name = "reportViewer28";
            this.reportViewer28.Size = new System.Drawing.Size(1197, 476);
            this.reportViewer28.TabIndex = 0;
            // 
            // frmReportDepartureList2Ex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1197, 476);
            this.Controls.Add(this.reportViewer28);
            this.Name = "frmReportDepartureList2Ex";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "frmReportDepartureList2Ex";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmReportDepartureList2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DepartureList2ModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer28;
        private System.Windows.Forms.BindingSource DepartureList2ModelBindingSource;
    }
}
