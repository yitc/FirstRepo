namespace GUI
{
    partial class frmReportDepartureList3Exclusive
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
            this.reportViewer71 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DepartureList2ModelInclusiveModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DepartureList2ModelInclusiveModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer71
            // 
            this.reportViewer71.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DepartureList3ExclusiveDataSet";
            reportDataSource1.Value = this.DepartureList2ModelInclusiveModelBindingSource;
            this.reportViewer71.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer71.LocalReport.ReportEmbeddedResource = "GUI.ReportDepartureList3ExclusivePreview.rdlc";
            this.reportViewer71.Location = new System.Drawing.Point(0, 0);
            this.reportViewer71.Name = "reportViewer71";
            this.reportViewer71.Size = new System.Drawing.Size(1216, 428);
            this.reportViewer71.TabIndex = 0;
            // 
            // DepartureList2ModelInclusiveModelBindingSource
            // 
            this.DepartureList2ModelInclusiveModelBindingSource.DataSource = typeof(BIS.Model.DepartureList2InclusiveModel);
            // 
            // frmReportDepartureList3Exclusive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 428);
            this.Controls.Add(this.reportViewer71);
            this.Name = "frmReportDepartureList3Exclusive";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "frmReportDepartureList3Exclusive";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmReportDepartureList3Exclusive_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DepartureList2ModelInclusiveModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer71;
        private System.Windows.Forms.BindingSource DepartureList2ModelInclusiveModelBindingSource;
    }
}
