namespace GUI
{
    partial class frmReportParticipantList
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
            this.reportViewer16 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ParticipantListReportModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ParticipantListReportModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer16
            // 
            this.reportViewer16.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "ParticipantListDataSet";
            reportDataSource1.Value = this.ParticipantListReportModelBindingSource;
            this.reportViewer16.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer16.LocalReport.ReportEmbeddedResource = "GUI.ReportParticipantListPreview.rdlc";
            this.reportViewer16.Location = new System.Drawing.Point(0, 0);
            this.reportViewer16.Name = "reportViewer16";
            this.reportViewer16.Size = new System.Drawing.Size(1209, 333);
            this.reportViewer16.TabIndex = 0;
            // 
            // ParticipantListReportModelBindingSource
            // 
            this.ParticipantListReportModelBindingSource.DataSource = typeof(BIS.Model.ParticipantListReportModel);
            // 
            // frmReportParticipantList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 333);
            this.Controls.Add(this.reportViewer16);
            this.Name = "frmReportParticipantList";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "frmReportParticipantList";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmReportParticipantList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ParticipantListReportModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer16;
        private System.Windows.Forms.BindingSource ParticipantListReportModelBindingSource;
    }
}
