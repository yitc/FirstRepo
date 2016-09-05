namespace GUI
{
    partial class GridLookupFormPreselection
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
            this.gridLookupPreselection = new Telerik.WinControls.UI.RadGridView();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.radMenuMeetings = new Telerik.WinControls.UI.RadMenu();
            this.radMenuItemSaveLookupLayout = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenuItemSave = new Telerik.WinControls.UI.RadMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookupPreselection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookupPreselection.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenuMeetings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // gridLookupPreselection
            // 
            this.gridLookupPreselection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridLookupPreselection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.gridLookupPreselection.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridLookupPreselection.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridLookupPreselection.ForeColor = System.Drawing.Color.Black;
            this.gridLookupPreselection.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.gridLookupPreselection.Location = new System.Drawing.Point(2, 1);
            // 
            // 
            // 
            this.gridLookupPreselection.MasterTemplate.AllowAddNewRow = false;
            this.gridLookupPreselection.MasterTemplate.AllowDeleteRow = false;
            this.gridLookupPreselection.MasterTemplate.EnableFiltering = true;
            this.gridLookupPreselection.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridLookupPreselection.Name = "gridLookupPreselection";
            this.gridLookupPreselection.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gridLookupPreselection.Size = new System.Drawing.Size(735, 516);
            this.gridLookupPreselection.TabIndex = 0;
            this.gridLookupPreselection.Text = "Grid Lookup Preselection";
            this.gridLookupPreselection.ThemeName = "VisualStudio2012Light";
            this.gridLookupPreselection.ValueChanged += new System.EventHandler(this.gridLookupPreselection_ValueChanged);
            this.gridLookupPreselection.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridLookup_DataBindingComplete);
            this.gridLookupPreselection.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridLookup_KeyDown);
            // 
            // radMenuMeetings
            // 
            this.radMenuMeetings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radMenuMeetings.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radMenuItemSaveLookupLayout,
            this.radMenuItemSave});
            this.radMenuMeetings.Location = new System.Drawing.Point(0, 523);
            this.radMenuMeetings.Name = "radMenuMeetings";
            this.radMenuMeetings.Size = new System.Drawing.Size(739, 20);
            this.radMenuMeetings.TabIndex = 1;
            this.radMenuMeetings.ThemeName = "Windows8";
            // 
            // radMenuItemSaveLookupLayout
            // 
            this.radMenuItemSaveLookupLayout.Name = "radMenuItemSaveLookupLayout";
            this.radMenuItemSaveLookupLayout.Text = "Save Layout";
            this.radMenuItemSaveLookupLayout.Click += new System.EventHandler(this.radMenuItemSaveLookupLayout_Click);
            // 
            // radMenuItemSave
            // 
            this.radMenuItemSave.Name = "radMenuItemSave";
            this.radMenuItemSave.Text = "Save";
            this.radMenuItemSave.Click += new System.EventHandler(this.radMenuItemSave_Click);
            // 
            // GridLookupFormPreselection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 543);
            this.Controls.Add(this.radMenuMeetings);
            this.Controls.Add(this.gridLookupPreselection);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GridLookupFormPreselection";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GridForm";
            this.ThemeName = "Windows8";
            ((System.ComponentModel.ISupportInitialize)(this.gridLookupPreselection.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookupPreselection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenuMeetings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView gridLookupPreselection;                
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Telerik.WinControls.UI.RadMenu radMenuMeetings;
        private Telerik.WinControls.UI.RadMenuItem radMenuItemSaveLookupLayout;
        private Telerik.WinControls.UI.RadMenuItem radMenuItemSave;
    }
}
