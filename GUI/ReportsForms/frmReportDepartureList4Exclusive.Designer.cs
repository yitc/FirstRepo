namespace GUI
{
    partial class frmReportDepartureList4Exclusive
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
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.reportViewer73 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer73
            // 
            this.reportViewer73.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer73.Location = new System.Drawing.Point(0, 0);
            this.reportViewer73.Name = "reportViewer73";
            this.reportViewer73.Size = new System.Drawing.Size(1204, 397);
            this.reportViewer73.TabIndex = 0;
            // 
            // frmReportDepartureList4Exclusive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 397);
            this.Controls.Add(this.reportViewer73);
            this.Name = "frmReportDepartureList4Exclusive";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "frmReportDepartureList4Exclusive";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmReportDepartureList4Exclusive_Load);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer73;
    }
}
