namespace GUI
{
    partial class frmAT_Begeleiders_KB_Buitenland_Handdoeken_Mee
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
            this.TravelerPapersReportModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            ((System.ComponentModel.ISupportInitialize)(this.TravelerPapersReportModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // TravelerPapersReportModelBindingSource
            // 
            this.TravelerPapersReportModelBindingSource.DataSource = typeof(BIS.Model.TravelerPapersReportModel);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "AutiTravelSubRepTravelerPaperDataSet";
            reportDataSource1.Value = this.TravelerPapersReportModelBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.EnableHyperlinks = true;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.TravelPapersVolunteers.AT_Begeleiders_KB_Buitenland_Handdoeken_Mee.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(996, 516);
            this.reportViewer1.TabIndex = 0;
            // 
            // frmAT_Begeleiders_KB_Buitenland_Handdoeken_Mee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 516);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmAT_Begeleiders_KB_Buitenland_Handdoeken_Mee";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reispapieren AT begeleiders - KB via opstapplaats buitenland - handdoeken mee";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmAT_Begeleiders_KB_Buitenland_Handdoeken_Mee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TravelerPapersReportModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource TravelerPapersReportModelBindingSource;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
    }
}
