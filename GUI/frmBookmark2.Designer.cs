namespace GUI
{
    partial class frmBookmark2
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
            this.radDropDownListTables = new Telerik.WinControls.UI.RadDropDownList();
            this.radButtonAdd = new Telerik.WinControls.UI.RadButton();
            this.radButtonRemove = new Telerik.WinControls.UI.RadButton();
            this.radButtonLoadBookmarks = new Telerik.WinControls.UI.RadButton();
            this.radListViewDBBookmarks = new Telerik.WinControls.UI.RadListView();
            this.radListViewDocBookmarks = new Telerik.WinControls.UI.RadListView();
            this.radButtonCreateTemplate = new Telerik.WinControls.UI.RadButton();
            this.radLabelRemoveBookmarks = new Telerik.WinControls.UI.RadLabel();
            this.radCheckBoxSaveAs = new Telerik.WinControls.UI.RadCheckBox();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.radPageViewBookmarks = new Telerik.WinControls.UI.RadPageView();
            this.pageClientPerson = new Telerik.WinControls.UI.RadPageViewPage();
            this.pageTables = new Telerik.WinControls.UI.RadPageViewPage();
            this.radListViewReports = new Telerik.WinControls.UI.RadListView();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radListViewReportsAdded = new Telerik.WinControls.UI.RadListView();
            this.chkDateTime = new Telerik.WinControls.UI.RadCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownListTables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonRemove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonLoadBookmarks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListViewDBBookmarks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListViewDocBookmarks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCreateTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelRemoveBookmarks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCheckBoxSaveAs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPageViewBookmarks)).BeginInit();
            this.radPageViewBookmarks.SuspendLayout();
            this.pageClientPerson.SuspendLayout();
            this.pageTables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radListViewReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListViewReportsAdded)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDateTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radDropDownListTables
            // 
            this.radDropDownListTables.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.radDropDownListTables.Location = new System.Drawing.Point(12, 12);
            this.radDropDownListTables.Name = "radDropDownListTables";
            this.radDropDownListTables.Size = new System.Drawing.Size(260, 23);
            this.radDropDownListTables.TabIndex = 0;
            this.radDropDownListTables.Text = "radDropDownListTables";
            // 
            // radButtonAdd
            // 
            this.radButtonAdd.Location = new System.Drawing.Point(278, 57);
            this.radButtonAdd.Name = "radButtonAdd";
            this.radButtonAdd.Size = new System.Drawing.Size(118, 24);
            this.radButtonAdd.TabIndex = 3;
            this.radButtonAdd.Text = "Add Bookmark";
            this.radButtonAdd.ThemeName = "Windows8";
            this.radButtonAdd.Click += new System.EventHandler(this.radButtonAdd_Click);
            // 
            // radButtonRemove
            // 
            this.radButtonRemove.Location = new System.Drawing.Point(278, 87);
            this.radButtonRemove.Name = "radButtonRemove";
            this.radButtonRemove.Size = new System.Drawing.Size(118, 24);
            this.radButtonRemove.TabIndex = 4;
            this.radButtonRemove.Text = "Remove Bookmark";
            this.radButtonRemove.ThemeName = "Windows8";
            this.radButtonRemove.Click += new System.EventHandler(this.radButtonRemove_Click);
            // 
            // radButtonLoadBookmarks
            // 
            this.radButtonLoadBookmarks.Location = new System.Drawing.Point(278, 11);
            this.radButtonLoadBookmarks.Name = "radButtonLoadBookmarks";
            this.radButtonLoadBookmarks.Size = new System.Drawing.Size(118, 24);
            this.radButtonLoadBookmarks.TabIndex = 4;
            this.radButtonLoadBookmarks.Text = "Load Bookmarks";
            this.radButtonLoadBookmarks.ThemeName = "Windows8";
            this.radButtonLoadBookmarks.Click += new System.EventHandler(this.radButtonLoadBookmarks_Click);
            // 
            // radListViewDBBookmarks
            // 
            this.radListViewDBBookmarks.AllowEdit = false;
            this.radListViewDBBookmarks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radListViewDBBookmarks.Location = new System.Drawing.Point(0, 0);
            this.radListViewDBBookmarks.Name = "radListViewDBBookmarks";
            this.radListViewDBBookmarks.Size = new System.Drawing.Size(247, 367);
            this.radListViewDBBookmarks.TabIndex = 5;
            this.radListViewDBBookmarks.Text = "radListView1";
            this.radListViewDBBookmarks.ItemMouseDoubleClick += new Telerik.WinControls.UI.ListViewItemEventHandler(this.radListViewDBBookmarks_ItemMouseDoubleClick);
            // 
            // radListViewDocBookmarks
            // 
            this.radListViewDocBookmarks.AllowDragDrop = true;
            this.radListViewDocBookmarks.AllowDrop = true;
            this.radListViewDocBookmarks.AllowEdit = false;
            this.radListViewDocBookmarks.Location = new System.Drawing.Point(402, 57);
            this.radListViewDocBookmarks.Name = "radListViewDocBookmarks";
            this.radListViewDocBookmarks.ShowCheckBoxes = true;
            this.radListViewDocBookmarks.Size = new System.Drawing.Size(260, 333);
            this.radListViewDocBookmarks.TabIndex = 6;
            this.radListViewDocBookmarks.Text = "radListView1";
            this.radListViewDocBookmarks.BindingContextChanged += new System.EventHandler(this.radListViewDocBookmarks_BindingContextChanged);
            this.radListViewDocBookmarks.ItemMouseDoubleClick += new Telerik.WinControls.UI.ListViewItemEventHandler(this.radListViewDocBookmarks_ItemMouseDoubleClick);
            this.radListViewDocBookmarks.ItemCheckedChanging += new Telerik.WinControls.UI.ListViewItemCancelEventHandler(this.radListViewDocBookmarks_ItemCheckedChanging);
            this.radListViewDocBookmarks.ItemCheckedChanged += new Telerik.WinControls.UI.ListViewItemEventHandler(this.radListViewDocBookmarks_ItemCheckedChanged);
            this.radListViewDocBookmarks.VisualItemFormatting += new Telerik.WinControls.UI.ListViewVisualItemEventHandler(this.radListViewDocBookmarks_VisualItemFormatting);
            this.radListViewDocBookmarks.ItemCreating += new Telerik.WinControls.UI.ListViewItemCreatingEventHandler(this.radListViewDocBookmarks_ItemCreating);
            this.radListViewDocBookmarks.VisualItemCreating += new Telerik.WinControls.UI.ListViewVisualItemCreatingEventHandler(this.radListViewDocBookmarks_VisualItemCreating);
            this.radListViewDocBookmarks.ItemRemoved += new Telerik.WinControls.UI.ListViewItemEventHandler(this.radListViewDocBookmarks_ItemRemoved);
            this.radListViewDocBookmarks.DragDrop += new System.Windows.Forms.DragEventHandler(this.radListViewDocBookmarks_DragDrop);
            this.radListViewDocBookmarks.DragEnter += new System.Windows.Forms.DragEventHandler(this.radListViewDocBookmarks_DragEnter);
            this.radListViewDocBookmarks.DragOver += new System.Windows.Forms.DragEventHandler(this.radListViewDocBookmarks_DragOver);
            this.radListViewDocBookmarks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.radListViewDocBookmarks_MouseDown);
            // 
            // radButtonCreateTemplate
            // 
            this.radButtonCreateTemplate.Enabled = false;
            this.radButtonCreateTemplate.Location = new System.Drawing.Point(402, 558);
            this.radButtonCreateTemplate.Name = "radButtonCreateTemplate";
            this.radButtonCreateTemplate.Size = new System.Drawing.Size(118, 24);
            this.radButtonCreateTemplate.TabIndex = 4;
            this.radButtonCreateTemplate.Text = "Create Template";
            this.radButtonCreateTemplate.ThemeName = "Windows8";
            this.radButtonCreateTemplate.Click += new System.EventHandler(this.radButtonCreateTemplate_Click);
            // 
            // radLabelRemoveBookmarks
            // 
            this.radLabelRemoveBookmarks.Location = new System.Drawing.Point(402, 33);
            this.radLabelRemoveBookmarks.Name = "radLabelRemoveBookmarks";
            this.radLabelRemoveBookmarks.Size = new System.Drawing.Size(237, 18);
            this.radLabelRemoveBookmarks.TabIndex = 7;
            this.radLabelRemoveBookmarks.Text = "Check bookmarks if you want to remove them";
            // 
            // radCheckBoxSaveAs
            // 
            this.radCheckBoxSaveAs.Location = new System.Drawing.Point(535, 558);
            this.radCheckBoxSaveAs.Name = "radCheckBoxSaveAs";
            this.radCheckBoxSaveAs.Size = new System.Drawing.Size(59, 18);
            this.radCheckBoxSaveAs.TabIndex = 8;
            this.radCheckBoxSaveAs.Text = "Save As";
            // 
            // radPageViewBookmarks
            // 
            this.radPageViewBookmarks.Controls.Add(this.pageClientPerson);
            this.radPageViewBookmarks.Controls.Add(this.pageTables);
            this.radPageViewBookmarks.ItemSizeMode = ((Telerik.WinControls.UI.PageViewItemSizeMode)((Telerik.WinControls.UI.PageViewItemSizeMode.EqualWidth | Telerik.WinControls.UI.PageViewItemSizeMode.EqualHeight)));
            this.radPageViewBookmarks.Location = new System.Drawing.Point(13, 57);
            this.radPageViewBookmarks.Name = "radPageViewBookmarks";
            this.radPageViewBookmarks.SelectedPage = this.pageClientPerson;
            this.radPageViewBookmarks.Size = new System.Drawing.Size(259, 491);
            this.radPageViewBookmarks.TabIndex = 10;
            this.radPageViewBookmarks.ThemeName = "Windows8";
            this.radPageViewBookmarks.ViewMode = Telerik.WinControls.UI.PageViewMode.Outlook;
            // 
            // pageClientPerson
            // 
            this.pageClientPerson.Controls.Add(this.radListViewDBBookmarks);
            this.pageClientPerson.ItemSize = new System.Drawing.SizeF(257F, 26F);
            this.pageClientPerson.Location = new System.Drawing.Point(6, 29);
            this.pageClientPerson.Name = "pageClientPerson";
            this.pageClientPerson.Size = new System.Drawing.Size(247, 367);
            this.pageClientPerson.Text = "Bookmarks";
            // 
            // pageTables
            // 
            this.pageTables.Controls.Add(this.radListViewReports);
            this.pageTables.ItemSize = new System.Drawing.SizeF(257F, 26F);
            this.pageTables.Location = new System.Drawing.Point(6, 29);
            this.pageTables.Name = "pageTables";
            this.pageTables.Size = new System.Drawing.Size(247, 367);
            this.pageTables.Text = "Reports";
            // 
            // radListViewReports
            // 
            this.radListViewReports.AllowEdit = false;
            this.radListViewReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radListViewReports.Location = new System.Drawing.Point(0, 0);
            this.radListViewReports.Name = "radListViewReports";
            this.radListViewReports.Size = new System.Drawing.Size(247, 367);
            this.radListViewReports.TabIndex = 6;
            this.radListViewReports.ItemMouseDoubleClick += new Telerik.WinControls.UI.ListViewItemEventHandler(this.radListViewReports_ItemMouseDoubleClick);
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(402, 396);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(75, 18);
            this.radLabel1.TabIndex = 11;
            this.radLabel1.Text = "Report Tables";
            // 
            // radListViewReportsAdded
            // 
            this.radListViewReportsAdded.AllowDragDrop = true;
            this.radListViewReportsAdded.AllowDrop = true;
            this.radListViewReportsAdded.AllowEdit = false;
            this.radListViewReportsAdded.Location = new System.Drawing.Point(402, 420);
            this.radListViewReportsAdded.Name = "radListViewReportsAdded";
            this.radListViewReportsAdded.Size = new System.Drawing.Size(260, 80);
            this.radListViewReportsAdded.TabIndex = 12;
            this.radListViewReportsAdded.ItemMouseDoubleClick += new Telerik.WinControls.UI.ListViewItemEventHandler(this.radListViewReportsAdded_ItemMouseDoubleClick);
            // 
            // chkDateTime
            // 
            this.chkDateTime.Location = new System.Drawing.Point(402, 507);
            this.chkDateTime.Name = "chkDateTime";
            this.chkDateTime.Size = new System.Drawing.Size(68, 18);
            this.chkDateTime.TabIndex = 13;
            this.chkDateTime.Text = "DateTime";
            // 
            // frmBookmark2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 619);
            this.Controls.Add(this.chkDateTime);
            this.Controls.Add(this.radListViewReportsAdded);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.radPageViewBookmarks);
            this.Controls.Add(this.radCheckBoxSaveAs);
            this.Controls.Add(this.radLabelRemoveBookmarks);
            this.Controls.Add(this.radButtonCreateTemplate);
            this.Controls.Add(this.radListViewDocBookmarks);
            this.Controls.Add(this.radButtonLoadBookmarks);
            this.Controls.Add(this.radButtonRemove);
            this.Controls.Add(this.radButtonAdd);
            this.Controls.Add(this.radDropDownListTables);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBookmark2";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmBookmark2";
            this.ThemeName = "Windows8";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmBookmark2_FormClosed);
            this.Load += new System.EventHandler(this.frmBookmark2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownListTables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonRemove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonLoadBookmarks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListViewDBBookmarks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListViewDocBookmarks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButtonCreateTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabelRemoveBookmarks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCheckBoxSaveAs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPageViewBookmarks)).EndInit();
            this.radPageViewBookmarks.ResumeLayout(false);
            this.pageClientPerson.ResumeLayout(false);
            this.pageTables.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radListViewReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListViewReportsAdded)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDateTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadDropDownList radDropDownListTables;
        private Telerik.WinControls.UI.RadButton radButtonAdd;
        private Telerik.WinControls.UI.RadButton radButtonRemove;
        private Telerik.WinControls.UI.RadButton radButtonLoadBookmarks;
        private Telerik.WinControls.UI.RadListView radListViewDBBookmarks;
        private Telerik.WinControls.UI.RadListView radListViewDocBookmarks;
        private Telerik.WinControls.UI.RadButton radButtonCreateTemplate;
        private Telerik.WinControls.UI.RadLabel radLabelRemoveBookmarks;
        private Telerik.WinControls.UI.RadCheckBox radCheckBoxSaveAs;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Telerik.WinControls.UI.RadPageView radPageViewBookmarks;
        private Telerik.WinControls.UI.RadPageViewPage pageClientPerson;
        private Telerik.WinControls.UI.RadPageViewPage pageTables;
        private Telerik.WinControls.UI.RadListView radListViewReports;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadListView radListViewReportsAdded;
        private Telerik.WinControls.UI.RadCheckBox chkDateTime;
    }
}