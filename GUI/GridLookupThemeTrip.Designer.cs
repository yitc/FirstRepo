namespace GUI
{
    partial class GridLookupThemeTrip
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
            this.gridLookup = new Telerik.WinControls.UI.RadGridView();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookup.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // gridLookup
            // 
            this.gridLookup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLookup.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridLookup.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridLookup.MasterTemplate.AllowAddNewRow = false;
            this.gridLookup.MasterTemplate.AllowDeleteRow = false;
            this.gridLookup.MasterTemplate.AllowEditRow = false;
            this.gridLookup.MasterTemplate.EnableFiltering = true;
            this.gridLookup.Name = "gridLookup";
            this.gridLookup.Size = new System.Drawing.Size(635, 481);
            this.gridLookup.TabIndex = 0;
            this.gridLookup.Text = "Grid Lookup";
            this.gridLookup.ThemeName = "VisualStudio2012Light";
            this.gridLookup.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.radGridView1_CellDoubleClick);
            this.gridLookup.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridLookup_DataBindingComplete);
            this.gridLookup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridLookup_KeyDown);
            // 
            // GridLookupThemeTrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 481);
            this.Controls.Add(this.gridLookup);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GridLookupThemeTrip";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GridForm";
            this.ThemeName = "Windows8";
            ((System.ComponentModel.ISupportInitialize)(this.gridLookup.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView gridLookup;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
    }
}
