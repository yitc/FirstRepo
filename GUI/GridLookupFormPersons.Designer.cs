namespace GUI
{
    partial class GridLookupFormPersons
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
            this.gridLookup = new Telerik.WinControls.UI.RadGridView();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.radMenuMeetings = new Telerik.WinControls.UI.RadMenu();
            this.radMenuItemSaveLookupLayout = new Telerik.WinControls.UI.RadMenuItem();
            this.btnSaveMenus = new Telerik.WinControls.UI.RadMenuButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookup.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenuMeetings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // gridLookup
            // 
            this.gridLookup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridLookup.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridLookup.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridLookup.MasterTemplate.AllowAddNewRow = false;
            this.gridLookup.MasterTemplate.AllowDeleteRow = false;
            this.gridLookup.MasterTemplate.EnableFiltering = true;
            this.gridLookup.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridLookup.Name = "gridLookup";
            this.gridLookup.Size = new System.Drawing.Size(635, 457);
            this.gridLookup.TabIndex = 0;
            this.gridLookup.Text = "Grid Lookup";
            this.gridLookup.ThemeName = "VisualStudio2012Light";
            this.gridLookup.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridLookup_DataBindingComplete);
            // 
            // radMenuMeetings
            // 
            this.radMenuMeetings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radMenuMeetings.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radMenuItemSaveLookupLayout,
            this.btnSaveMenus});
            this.radMenuMeetings.Location = new System.Drawing.Point(0, 458);
            this.radMenuMeetings.Name = "radMenuMeetings";
            this.radMenuMeetings.Size = new System.Drawing.Size(635, 23);
            this.radMenuMeetings.TabIndex = 3;
            this.radMenuMeetings.Text = "radMenu2";
            this.radMenuMeetings.ThemeName = "Windows8";
            // 
            // radMenuItemSaveLookupLayout
            // 
            this.radMenuItemSaveLookupLayout.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.radMenuItemSaveLookupLayout.Name = "radMenuItemSaveLookupLayout";
            this.radMenuItemSaveLookupLayout.Text = "Save Layout";
            this.radMenuItemSaveLookupLayout.Click += new System.EventHandler(this.radMenuItemSaveLookupLayout_Click);
            // 
            // btnSaveMenus
            // 
            this.btnSaveMenus.AccessibleDescription = "Save menus";
            this.btnSaveMenus.AccessibleName = "Save menus";
            // 
            // 
            // 
            this.btnSaveMenus.ButtonElement.AccessibleDescription = "Save menus";
            this.btnSaveMenus.ButtonElement.AccessibleName = "Save menus";
            this.btnSaveMenus.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveMenus.Name = "btnSaveMenus";
            this.btnSaveMenus.Text = "Save persons";
            this.btnSaveMenus.Click += new System.EventHandler(this.btnSavePersons_Click);
            // 
            // GridLookupFormPersons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 481);
            this.Controls.Add(this.radMenuMeetings);
            this.Controls.Add(this.gridLookup);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GridLookupFormPersons";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GridForm";
            this.ThemeName = "Windows8";
            ((System.ComponentModel.ISupportInitialize)(this.gridLookup.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenuMeetings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView gridLookup;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Telerik.WinControls.UI.RadMenu radMenuMeetings;
        private Telerik.WinControls.UI.RadMenuItem radMenuItemSaveLookupLayout;
        private Telerik.WinControls.UI.RadMenuButtonItem btnSaveMenus;
    }
}
