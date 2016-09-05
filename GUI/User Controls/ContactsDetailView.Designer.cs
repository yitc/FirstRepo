namespace GUI
{
    partial class ContactsDetailView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listContacts = new Telerik.WinControls.UI.RadListView();
            this.radSplitContainerForm = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanelCombos = new Telerik.WinControls.UI.SplitPanel();
            this.radSplitContainer1 = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanelCombo1 = new Telerik.WinControls.UI.SplitPanel();
            this.radDropDownListReason = new Telerik.WinControls.UI.RadDropDownList();
            this.splitPanelCombo2 = new Telerik.WinControls.UI.SplitPanel();
            this.radDropDownListType = new Telerik.WinControls.UI.RadDropDownList();
            this.splitPanelList = new Telerik.WinControls.UI.SplitPanel();
            ((System.ComponentModel.ISupportInitialize)(this.listContacts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainerForm)).BeginInit();
            this.radSplitContainerForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelCombos)).BeginInit();
            this.splitPanelCombos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).BeginInit();
            this.radSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelCombo1)).BeginInit();
            this.splitPanelCombo1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownListReason)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelCombo2)).BeginInit();
            this.splitPanelCombo2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownListType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelList)).BeginInit();
            this.splitPanelList.SuspendLayout();
            this.SuspendLayout();
            // 
            // listContacts
            // 
            this.listContacts.AllowEdit = false;
            this.listContacts.AllowRemove = false;
            this.listContacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listContacts.ItemSpacing = -1;
            this.listContacts.Location = new System.Drawing.Point(0, 0);
            this.listContacts.Name = "listContacts";
            this.listContacts.ShowGridLines = true;
            this.listContacts.Size = new System.Drawing.Size(258, 321);
            this.listContacts.TabIndex = 0;
            this.listContacts.Text = "radListView1";
            this.listContacts.ThemeName = "Windows8";
            this.listContacts.ViewType = Telerik.WinControls.UI.ListViewType.DetailsView;
            this.listContacts.SelectedIndexChanged += new System.EventHandler(this.listContacts_SelectedIndexChanged);
            this.listContacts.ItemMouseDoubleClick += new Telerik.WinControls.UI.ListViewItemEventHandler(this.listContacts_ItemMouseDoubleClick);
            // 
            // radSplitContainerForm
            // 
            this.radSplitContainerForm.Controls.Add(this.splitPanelCombos);
            this.radSplitContainerForm.Controls.Add(this.splitPanelList);
            this.radSplitContainerForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSplitContainerForm.Location = new System.Drawing.Point(0, 0);
            this.radSplitContainerForm.Name = "radSplitContainerForm";
            this.radSplitContainerForm.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.radSplitContainerForm.Size = new System.Drawing.Size(258, 344);
            this.radSplitContainerForm.SplitterWidth = 1;
            this.radSplitContainerForm.TabIndex = 1;
            this.radSplitContainerForm.TabStop = false;
            this.radSplitContainerForm.Text = "radSplitContainer1";
            // 
            // splitPanelCombos
            // 
            this.splitPanelCombos.Controls.Add(this.radSplitContainer1);
            this.splitPanelCombos.Location = new System.Drawing.Point(0, 0);
            this.splitPanelCombos.Name = "splitPanelCombos";
            // 
            // 
            // 
            this.splitPanelCombos.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanelCombos.Size = new System.Drawing.Size(258, 22);
            this.splitPanelCombos.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, -0.4358601F);
            this.splitPanelCombos.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, -150);
            this.splitPanelCombos.TabIndex = 0;
            this.splitPanelCombos.TabStop = false;
            this.splitPanelCombos.Text = "splitPanel1";
            // 
            // radSplitContainer1
            // 
            this.radSplitContainer1.Controls.Add(this.splitPanelCombo1);
            this.radSplitContainer1.Controls.Add(this.splitPanelCombo2);
            this.radSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.radSplitContainer1.Name = "radSplitContainer1";
            // 
            // 
            // 
            this.radSplitContainer1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.radSplitContainer1.Size = new System.Drawing.Size(258, 22);
            this.radSplitContainer1.TabIndex = 0;
            this.radSplitContainer1.TabStop = false;
            this.radSplitContainer1.Text = "radSplitContainerCombos";
            // 
            // splitPanelCombo1
            // 
            this.splitPanelCombo1.Controls.Add(this.radDropDownListReason);
            this.splitPanelCombo1.Location = new System.Drawing.Point(0, 0);
            this.splitPanelCombo1.Name = "splitPanelCombo1";
            // 
            // 
            // 
            this.splitPanelCombo1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanelCombo1.Size = new System.Drawing.Size(124, 22);
            this.splitPanelCombo1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.01181102F, 0F);
            this.splitPanelCombo1.SizeInfo.SplitterCorrection = new System.Drawing.Size(-3, 0);
            this.splitPanelCombo1.TabIndex = 0;
            this.splitPanelCombo1.TabStop = false;
            // 
            // radDropDownListReason
            // 
            this.radDropDownListReason.AutoSize = false;
            this.radDropDownListReason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radDropDownListReason.Location = new System.Drawing.Point(0, 0);
            this.radDropDownListReason.Name = "radDropDownListReason";
            this.radDropDownListReason.Size = new System.Drawing.Size(124, 22);
            this.radDropDownListReason.TabIndex = 0;
            this.radDropDownListReason.ThemeName = "Windows8";
            this.radDropDownListReason.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.radDropDownListReason_SelectedIndexChanged);
            // 
            // splitPanelCombo2
            // 
            this.splitPanelCombo2.Controls.Add(this.radDropDownListType);
            this.splitPanelCombo2.Location = new System.Drawing.Point(128, 0);
            this.splitPanelCombo2.Name = "splitPanelCombo2";
            // 
            // 
            // 
            this.splitPanelCombo2.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanelCombo2.Size = new System.Drawing.Size(130, 22);
            this.splitPanelCombo2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.01181102F, 0F);
            this.splitPanelCombo2.SizeInfo.SplitterCorrection = new System.Drawing.Size(3, 0);
            this.splitPanelCombo2.TabIndex = 1;
            this.splitPanelCombo2.TabStop = false;
            // 
            // radDropDownListType
            // 
            this.radDropDownListType.AutoSize = false;
            this.radDropDownListType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radDropDownListType.Location = new System.Drawing.Point(0, 0);
            this.radDropDownListType.Name = "radDropDownListType";
            this.radDropDownListType.Size = new System.Drawing.Size(130, 22);
            this.radDropDownListType.TabIndex = 0;
            this.radDropDownListType.ThemeName = "Windows8";
            this.radDropDownListType.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.radDropDownListType_SelectedIndexChanged);
            // 
            // splitPanelList
            // 
            this.splitPanelList.Controls.Add(this.listContacts);
            this.splitPanelList.Location = new System.Drawing.Point(0, 23);
            this.splitPanelList.Name = "splitPanelList";
            // 
            // 
            // 
            this.splitPanelList.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanelList.Size = new System.Drawing.Size(258, 321);
            this.splitPanelList.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.4358601F);
            this.splitPanelList.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 150);
            this.splitPanelList.TabIndex = 1;
            this.splitPanelList.TabStop = false;
            this.splitPanelList.Text = "splitPanel2";
            // 
            // ContactsDetailView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radSplitContainerForm);
            this.Name = "ContactsDetailView";
            this.Size = new System.Drawing.Size(258, 344);
            this.Load += new System.EventHandler(this.ContactsDetailView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listContacts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainerForm)).EndInit();
            this.radSplitContainerForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelCombos)).EndInit();
            this.splitPanelCombos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).EndInit();
            this.radSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelCombo1)).EndInit();
            this.splitPanelCombo1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownListReason)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelCombo2)).EndInit();
            this.splitPanelCombo2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownListType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelList)).EndInit();
            this.splitPanelList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadListView listContacts;
        private Telerik.WinControls.UI.RadSplitContainer radSplitContainerForm;
        private Telerik.WinControls.UI.SplitPanel splitPanelCombos;
        private Telerik.WinControls.UI.RadSplitContainer radSplitContainer1;
        private Telerik.WinControls.UI.SplitPanel splitPanelCombo1;
        private Telerik.WinControls.UI.RadDropDownList radDropDownListReason;
        private Telerik.WinControls.UI.SplitPanel splitPanelCombo2;
        private Telerik.WinControls.UI.RadDropDownList radDropDownListType;
        private Telerik.WinControls.UI.SplitPanel splitPanelList;
    }
}
