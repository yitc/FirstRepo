namespace GUI
{
    partial class ReportViewerDepartureList
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
            this.rptViewerDeparturList4 = new Telerik.ReportViewer.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // rptViewerDeparturList4
            // 
            this.rptViewerDeparturList4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rptViewerDeparturList4.Location = new System.Drawing.Point(0, 0);
            this.rptViewerDeparturList4.Name = "rptViewerDeparturList4";
            this.rptViewerDeparturList4.Size = new System.Drawing.Size(696, 527);
            this.rptViewerDeparturList4.TabIndex = 0;
            // 
            // ReportViewerDepartureList4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 527);
            this.Controls.Add(this.rptViewerDeparturList4);
            this.Name = "ReportViewerDepartureList4";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "ReportViewerDepartureList4";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.ReportViewerDepartureList_Load);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.ReportViewer.WinForms.ReportViewer rptViewerDeparturList4;
    }
}
