namespace GUI
{
    partial class GridLookupTraveler
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.radSplitLookupContainer = new Telerik.WinControls.UI.RadSplitContainer();
            this.topPanel = new Telerik.WinControls.UI.SplitPanel();
            this.pageViewFilters = new Telerik.WinControls.UI.RadPageView();
            this.tabMedical = new Telerik.WinControls.UI.RadPageViewPage();
            this.splitContTrips = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel3 = new Telerik.WinControls.UI.SplitPanel();
            this.chkCheckMedical = new Telerik.WinControls.UI.RadCheckBox();
            this.splitPanel4 = new Telerik.WinControls.UI.SplitPanel();
            this.radListPref = new Telerik.WinControls.UI.RadListView();
            this.bottomPanel = new Telerik.WinControls.UI.SplitPanel();
            this.gridTraveler = new Telerik.WinControls.UI.RadGridView();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitLookupContainer)).BeginInit();
            this.radSplitLookupContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topPanel)).BeginInit();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pageViewFilters)).BeginInit();
            this.pageViewFilters.SuspendLayout();
            this.tabMedical.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContTrips)).BeginInit();
            this.splitContTrips.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel3)).BeginInit();
            this.splitPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheckMedical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel4)).BeginInit();
            this.splitPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radListPref)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomPanel)).BeginInit();
            this.bottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTraveler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTraveler.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radSplitLookupContainer
            // 
            this.radSplitLookupContainer.Controls.Add(this.topPanel);
            this.radSplitLookupContainer.Controls.Add(this.bottomPanel);
            this.radSplitLookupContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSplitLookupContainer.Location = new System.Drawing.Point(0, 0);
            this.radSplitLookupContainer.Name = "radSplitLookupContainer";
            // 
            // 
            // 
            this.radSplitLookupContainer.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.radSplitLookupContainer.Size = new System.Drawing.Size(801, 596);
            this.radSplitLookupContainer.TabIndex = 0;
            this.radSplitLookupContainer.TabStop = false;
            this.radSplitLookupContainer.ThemeName = "Windows8";
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.pageViewFilters);
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            // 
            // 
            // 
            this.topPanel.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.topPanel.Size = new System.Drawing.Size(216, 596);
            this.topPanel.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.2289837F, -0.2635135F);
            this.topPanel.SizeInfo.SplitterCorrection = new System.Drawing.Size(-182, -156);
            this.topPanel.TabIndex = 0;
            this.topPanel.TabStop = false;
            this.topPanel.ThemeName = "Windows8";
            // 
            // pageViewFilters
            // 
            this.pageViewFilters.Controls.Add(this.tabMedical);
            this.pageViewFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageViewFilters.ItemSizeMode = ((Telerik.WinControls.UI.PageViewItemSizeMode)((Telerik.WinControls.UI.PageViewItemSizeMode.EqualWidth | Telerik.WinControls.UI.PageViewItemSizeMode.EqualHeight)));
            this.pageViewFilters.Location = new System.Drawing.Point(0, 0);
            this.pageViewFilters.Name = "pageViewFilters";
            this.pageViewFilters.SelectedPage = this.tabMedical;
            this.pageViewFilters.Size = new System.Drawing.Size(216, 596);
            this.pageViewFilters.TabIndex = 0;
            this.pageViewFilters.ThemeName = "Windows8";
            this.pageViewFilters.ViewMode = Telerik.WinControls.UI.PageViewMode.Outlook;
            // 
            // tabMedical
            // 
            this.tabMedical.Controls.Add(this.splitContTrips);
            this.tabMedical.ItemSize = new System.Drawing.SizeF(214F, 26F);
            this.tabMedical.Location = new System.Drawing.Point(6, 29);
            this.tabMedical.Name = "tabMedical";
            this.tabMedical.Size = new System.Drawing.Size(204, 497);
            this.tabMedical.Text = "Medical";
            // 
            // splitContTrips
            // 
            this.splitContTrips.Controls.Add(this.splitPanel3);
            this.splitContTrips.Controls.Add(this.splitPanel4);
            this.splitContTrips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContTrips.Location = new System.Drawing.Point(0, 0);
            this.splitContTrips.Name = "splitContTrips";
            this.splitContTrips.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.splitContTrips.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitContTrips.Size = new System.Drawing.Size(204, 497);
            this.splitContTrips.SplitterWidth = 1;
            this.splitContTrips.TabIndex = 0;
            this.splitContTrips.TabStop = false;
            this.splitContTrips.ThemeName = "VisualStudio2012Light";
            // 
            // splitPanel3
            // 
            this.splitPanel3.Controls.Add(this.chkCheckMedical);
            this.splitPanel3.Location = new System.Drawing.Point(0, 0);
            this.splitPanel3.Name = "splitPanel3";
            // 
            // 
            // 
            this.splitPanel3.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel3.Size = new System.Drawing.Size(204, 33);
            this.splitPanel3.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, -0.4341826F);
            this.splitPanel3.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, -205);
            this.splitPanel3.TabIndex = 0;
            this.splitPanel3.TabStop = false;
            this.splitPanel3.Text = "splitPanel3";
            this.splitPanel3.ThemeName = "VisualStudio2012Light";
            // 
            // chkCheckMedical
            // 
            this.chkCheckMedical.BackColor = System.Drawing.Color.Transparent;
            this.chkCheckMedical.Font = new System.Drawing.Font("Verdana", 10F);
            this.chkCheckMedical.Location = new System.Drawing.Point(2, 6);
            this.chkCheckMedical.Name = "chkCheckMedical";
            this.chkCheckMedical.Size = new System.Drawing.Size(87, 20);
            this.chkCheckMedical.TabIndex = 1;
            this.chkCheckMedical.Text = "Check All";
            this.chkCheckMedical.ThemeName = "Windows8";
            this.chkCheckMedical.CheckStateChanged += new System.EventHandler(this.chkBoxMedical_CheckStateChanged);
            // 
            // splitPanel4
            // 
            this.splitPanel4.Controls.Add(this.radListPref);
            this.splitPanel4.Location = new System.Drawing.Point(0, 34);
            this.splitPanel4.Name = "splitPanel4";
            // 
            // 
            // 
            this.splitPanel4.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel4.Size = new System.Drawing.Size(204, 463);
            this.splitPanel4.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.4341826F);
            this.splitPanel4.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 205);
            this.splitPanel4.TabIndex = 1;
            this.splitPanel4.TabStop = false;
            this.splitPanel4.Text = "splitPanel4";
            this.splitPanel4.ThemeName = "VisualStudio2012Light";
            // 
            // radListPref
            // 
            this.radListPref.AllowEdit = false;
            this.radListPref.AllowRemove = false;
            this.radListPref.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radListPref.Location = new System.Drawing.Point(0, 0);
            this.radListPref.Name = "radListPref";
            this.radListPref.ShowCheckBoxes = true;
            this.radListPref.Size = new System.Drawing.Size(204, 463);
            this.radListPref.TabIndex = 0;
            this.radListPref.ThemeName = "Windows8";
            this.radListPref.ItemCheckedChanged += new Telerik.WinControls.UI.ListViewItemEventHandler(this.radListTrips_ItemCheckedChanged);
            this.radListPref.ItemDataBound += new Telerik.WinControls.UI.ListViewItemEventHandler(this.radListTrips_ItemDataBound);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.gridTraveler);
            this.bottomPanel.Location = new System.Drawing.Point(220, 0);
            this.bottomPanel.Name = "bottomPanel";
            // 
            // 
            // 
            this.bottomPanel.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.bottomPanel.Size = new System.Drawing.Size(581, 596);
            this.bottomPanel.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.2289837F, 0.2635135F);
            this.bottomPanel.SizeInfo.SplitterCorrection = new System.Drawing.Size(182, 156);
            this.bottomPanel.TabIndex = 1;
            this.bottomPanel.TabStop = false;
            this.bottomPanel.ThemeName = "Windows8";
            // 
            // gridTraveler
            // 
            this.gridTraveler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTraveler.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridTraveler.MasterTemplate.AllowAddNewRow = false;
            this.gridTraveler.MasterTemplate.AllowDeleteRow = false;
            this.gridTraveler.MasterTemplate.AllowEditRow = false;
            this.gridTraveler.MasterTemplate.AllowSearchRow = true;
            this.gridTraveler.MasterTemplate.EnableFiltering = true;
            this.gridTraveler.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridTraveler.Name = "gridTraveler";
            this.gridTraveler.Size = new System.Drawing.Size(581, 596);
            this.gridTraveler.TabIndex = 0;
            this.gridTraveler.ThemeName = "VisualStudio2012Light";
            this.gridTraveler.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridTraveler_CellDoubleClick);
            this.gridTraveler.ContextMenuOpening += new Telerik.WinControls.UI.ContextMenuOpeningEventHandler(this.gridVolontary_ContextMenuOpening);
            this.gridTraveler.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridVolontary_DataBindingComplete);
            this.gridTraveler.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridVolontary_KeyDown);
            // 
            // GridLookupTraveler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 596);
            this.Controls.Add(this.radSplitLookupContainer);
            this.Name = "GridLookupTraveler";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GridLookupTraveler";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.GridLookupVoluntary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radSplitLookupContainer)).EndInit();
            this.radSplitLookupContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.topPanel)).EndInit();
            this.topPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pageViewFilters)).EndInit();
            this.pageViewFilters.ResumeLayout(false);
            this.tabMedical.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContTrips)).EndInit();
            this.splitContTrips.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel3)).EndInit();
            this.splitPanel3.ResumeLayout(false);
            this.splitPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCheckMedical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel4)).EndInit();
            this.splitPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radListPref)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomPanel)).EndInit();
            this.bottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTraveler.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTraveler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadSplitContainer radSplitLookupContainer;
        private Telerik.WinControls.UI.SplitPanel topPanel;
        private Telerik.WinControls.UI.RadPageView pageViewFilters;
        private Telerik.WinControls.UI.SplitPanel bottomPanel;
        private Telerik.WinControls.UI.RadGridView gridTraveler;
        private Telerik.WinControls.UI.RadPageViewPage tabMedical;
        private Telerik.WinControls.UI.RadListView radListPref;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private Telerik.WinControls.UI.RadSplitContainer splitContTrips;
        private Telerik.WinControls.UI.SplitPanel splitPanel3;
        private Telerik.WinControls.UI.RadCheckBox chkCheckMedical;
        private Telerik.WinControls.UI.SplitPanel splitPanel4;

    }
}