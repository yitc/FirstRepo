namespace GUI
{
    partial class frmReportReasonIn
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
            this.reportViewer7 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.AvailabilitySkillsModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.AvailabilitySkillsModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer7
            // 
            this.reportViewer7.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "ReasonInDataSet";
            reportDataSource1.Value = this.AvailabilitySkillsModelBindingSource;
            this.reportViewer7.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer7.LocalReport.ReportEmbeddedResource = "GUI.ReasonInPreview.rdlc";
            this.reportViewer7.Location = new System.Drawing.Point(0, 0);
            this.reportViewer7.Name = "reportViewer7";
            this.reportViewer7.Size = new System.Drawing.Size(1221, 581);
            this.reportViewer7.TabIndex = 0;
            // 
            // AvailabilitySkillsModelBindingSource
            // 
            this.AvailabilitySkillsModelBindingSource.DataSource = typeof(BIS.Model.AvailabilitySkillsModel);
            // 
            // frmReportReasonIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 581);
            this.Controls.Add(this.reportViewer7);
            this.Name = "frmReportReasonIn";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "frmReportReasonIn";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmReportReasonIn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AvailabilitySkillsModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer7;
        private System.Windows.Forms.BindingSource AvailabilitySkillsModelBindingSource;
    }
}
