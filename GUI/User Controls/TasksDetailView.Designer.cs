namespace GUI
{
    partial class TasksDetailView
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
            this.listTasks = new Telerik.WinControls.UI.RadListView();
            this.radSplitControlContainer = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanelList = new Telerik.WinControls.UI.SplitPanel();
            this.splitPanelFilters = new Telerik.WinControls.UI.SplitPanel();
            this.splitPanelCombo3 = new Telerik.WinControls.UI.SplitPanel();
            this.splitPanelCombo2 = new Telerik.WinControls.UI.SplitPanel();
            this.splitPanelCombo1 = new Telerik.WinControls.UI.SplitPanel();
            this.radSplitContainerCombos = new Telerik.WinControls.UI.RadSplitContainer();
            this.radDropDownType = new Telerik.WinControls.UI.RadDropDownList();
            this.radDropDownListStatus = new Telerik.WinControls.UI.RadDropDownList();
            this.radDropDownPriority = new Telerik.WinControls.UI.RadDropDownList();
            ((System.ComponentModel.ISupportInitialize)(this.listTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitControlContainer)).BeginInit();
            this.radSplitControlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelList)).BeginInit();
            this.splitPanelList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelFilters)).BeginInit();
            this.splitPanelFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelCombo3)).BeginInit();
            this.splitPanelCombo3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelCombo2)).BeginInit();
            this.splitPanelCombo2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelCombo1)).BeginInit();
            this.splitPanelCombo1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainerCombos)).BeginInit();
            this.radSplitContainerCombos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownListStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownPriority)).BeginInit();
            this.SuspendLayout();
            // 
            // listTasks
            // 
            this.listTasks.AllowEdit = false;
            this.listTasks.AllowRemove = false;
            this.listTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listTasks.EnableFiltering = true;
            this.listTasks.EnableSorting = true;
            this.listTasks.ItemSpacing = -1;
            this.listTasks.Location = new System.Drawing.Point(0, 0);
            this.listTasks.Name = "listTasks";
            this.listTasks.ShowGridLines = true;
            this.listTasks.Size = new System.Drawing.Size(312, 377);
            this.listTasks.TabIndex = 0;
            this.listTasks.Text = "TASKS";
            this.listTasks.ThemeName = "Windows8";
            this.listTasks.ViewType = Telerik.WinControls.UI.ListViewType.DetailsView;
            this.listTasks.ItemMouseDoubleClick += new Telerik.WinControls.UI.ListViewItemEventHandler(this.listTasks_ItemMouseDoubleClick);
            // 
            // radSplitControlContainer
            // 
            this.radSplitControlContainer.Controls.Add(this.splitPanelFilters);
            this.radSplitControlContainer.Controls.Add(this.splitPanelList);
            this.radSplitControlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSplitControlContainer.Location = new System.Drawing.Point(0, 0);
            this.radSplitControlContainer.Name = "radSplitControlContainer";
            this.radSplitControlContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.radSplitControlContainer.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.radSplitControlContainer.Size = new System.Drawing.Size(312, 399);
            this.radSplitControlContainer.SplitterWidth = 0;
            this.radSplitControlContainer.TabIndex = 1;
            this.radSplitControlContainer.TabStop = false;
            this.radSplitControlContainer.ThemeName = "ControlDefault";
            // 
            // splitPanelList
            // 
            this.splitPanelList.Controls.Add(this.listTasks);
            this.splitPanelList.Location = new System.Drawing.Point(0, 22);
            this.splitPanelList.Name = "splitPanelList";
            // 
            // 
            // 
            this.splitPanelList.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanelList.Size = new System.Drawing.Size(312, 377);
            this.splitPanelList.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.4443038F);
            this.splitPanelList.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 176);
            this.splitPanelList.TabIndex = 1;
            this.splitPanelList.TabStop = false;
            this.splitPanelList.ThemeName = "ControlDefault";
            // 
            // splitPanelFilters
            // 
            this.splitPanelFilters.Controls.Add(this.radSplitContainerCombos);
            this.splitPanelFilters.Location = new System.Drawing.Point(0, 0);
            this.splitPanelFilters.Name = "splitPanelFilters";
            // 
            // 
            // 
            this.splitPanelFilters.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanelFilters.Size = new System.Drawing.Size(312, 22);
            this.splitPanelFilters.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, -0.4443038F);
            this.splitPanelFilters.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, -176);
            this.splitPanelFilters.TabIndex = 0;
            this.splitPanelFilters.TabStop = false;
            this.splitPanelFilters.Text = "splitPanel1";
            this.splitPanelFilters.ThemeName = "ControlDefault";
            // 
            // splitPanelCombo3
            // 
            this.splitPanelCombo3.Controls.Add(this.radDropDownType);
            this.splitPanelCombo3.Location = new System.Drawing.Point(152, 0);
            this.splitPanelCombo3.Name = "splitPanelCombo3";
            // 
            // 
            // 
            this.splitPanelCombo3.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanelCombo3.Size = new System.Drawing.Size(160, 22);
            this.splitPanelCombo3.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0.183114F, 0F);
            this.splitPanelCombo3.SizeInfo.SplitterCorrection = new System.Drawing.Size(55, 0);
            this.splitPanelCombo3.TabIndex = 2;
            this.splitPanelCombo3.TabStop = false;
            // 
            // splitPanelCombo2
            // 
            this.splitPanelCombo2.Controls.Add(this.radDropDownListStatus);
            this.splitPanelCombo2.Location = new System.Drawing.Point(55, 0);
            this.splitPanelCombo2.Name = "splitPanelCombo2";
            // 
            // 
            // 
            this.splitPanelCombo2.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanelCombo2.Size = new System.Drawing.Size(96, 22);
            this.splitPanelCombo2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.0241228F, 0F);
            this.splitPanelCombo2.SizeInfo.SplitterCorrection = new System.Drawing.Size(-7, 0);
            this.splitPanelCombo2.TabIndex = 1;
            this.splitPanelCombo2.TabStop = false;
            // 
            // splitPanelCombo1
            // 
            this.splitPanelCombo1.Controls.Add(this.radDropDownPriority);
            this.splitPanelCombo1.Location = new System.Drawing.Point(0, 0);
            this.splitPanelCombo1.Name = "splitPanelCombo1";
            // 
            // 
            // 
            this.splitPanelCombo1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanelCombo1.Size = new System.Drawing.Size(54, 22);
            this.splitPanelCombo1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(-0.1589912F, 0F);
            this.splitPanelCombo1.SizeInfo.SplitterCorrection = new System.Drawing.Size(-48, 0);
            this.splitPanelCombo1.TabIndex = 0;
            this.splitPanelCombo1.TabStop = false;
            // 
            // radSplitContainerCombos
            // 
            this.radSplitContainerCombos.Controls.Add(this.splitPanelCombo1);
            this.radSplitContainerCombos.Controls.Add(this.splitPanelCombo2);
            this.radSplitContainerCombos.Controls.Add(this.splitPanelCombo3);
            this.radSplitContainerCombos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSplitContainerCombos.Location = new System.Drawing.Point(0, 0);
            this.radSplitContainerCombos.Name = "radSplitContainerCombos";
            // 
            // 
            // 
            this.radSplitContainerCombos.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.radSplitContainerCombos.Size = new System.Drawing.Size(312, 22);
            this.radSplitContainerCombos.SplitterWidth = 1;
            this.radSplitContainerCombos.TabIndex = 0;
            this.radSplitContainerCombos.TabStop = false;
            this.radSplitContainerCombos.Text = "radSplitContainer1";
            // 
            // radDropDownType
            // 
            this.radDropDownType.AutoSize = false;
            this.radDropDownType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radDropDownType.Location = new System.Drawing.Point(0, 0);
            this.radDropDownType.Name = "radDropDownType";
            this.radDropDownType.Size = new System.Drawing.Size(160, 22);
            this.radDropDownType.TabIndex = 0;
            this.radDropDownType.Text = "radDropDownList2";
            this.radDropDownType.ThemeName = "Windows8";
            this.radDropDownType.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.radDropDownType_SelectedIndexChanged);
            // 
            // radDropDownListStatus
            // 
            this.radDropDownListStatus.AutoSize = false;
            this.radDropDownListStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radDropDownListStatus.Location = new System.Drawing.Point(0, 0);
            this.radDropDownListStatus.Name = "radDropDownListStatus";
            this.radDropDownListStatus.Size = new System.Drawing.Size(96, 22);
            this.radDropDownListStatus.TabIndex = 0;
            this.radDropDownListStatus.Text = "radDropDownStatus";
            this.radDropDownListStatus.ThemeName = "Windows8";
            this.radDropDownListStatus.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.radDropDownListStatus_SelectedIndexChanged);
            // 
            // radDropDownPriority
            // 
            this.radDropDownPriority.AutoSize = false;
            this.radDropDownPriority.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radDropDownPriority.Location = new System.Drawing.Point(0, 0);
            this.radDropDownPriority.Name = "radDropDownPriority";
            this.radDropDownPriority.Size = new System.Drawing.Size(54, 22);
            this.radDropDownPriority.TabIndex = 0;
            this.radDropDownPriority.Text = "radDropDownList1";
            this.radDropDownPriority.ThemeName = "Windows8";
            this.radDropDownPriority.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.radDropDownPriority_SelectedIndexChanged);
            // 
            // TasksDetailView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radSplitControlContainer);
            this.Name = "TasksDetailView";
            this.Size = new System.Drawing.Size(312, 399);
            this.Load += new System.EventHandler(this.TasksDetailView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.listTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitControlContainer)).EndInit();
            this.radSplitControlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelList)).EndInit();
            this.splitPanelList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelFilters)).EndInit();
            this.splitPanelFilters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelCombo3)).EndInit();
            this.splitPanelCombo3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelCombo2)).EndInit();
            this.splitPanelCombo2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelCombo1)).EndInit();
            this.splitPanelCombo1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainerCombos)).EndInit();
            this.radSplitContainerCombos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownListStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownPriority)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadListView listTasks;
        private Telerik.WinControls.UI.RadSplitContainer radSplitControlContainer;
        private Telerik.WinControls.UI.SplitPanel splitPanelList;
        private Telerik.WinControls.UI.SplitPanel splitPanelFilters;
        private Telerik.WinControls.UI.RadSplitContainer radSplitContainerCombos;
        private Telerik.WinControls.UI.SplitPanel splitPanelCombo1;
        private Telerik.WinControls.UI.RadDropDownList radDropDownPriority;
        private Telerik.WinControls.UI.SplitPanel splitPanelCombo2;
        private Telerik.WinControls.UI.RadDropDownList radDropDownListStatus;
        private Telerik.WinControls.UI.SplitPanel splitPanelCombo3;
        private Telerik.WinControls.UI.RadDropDownList radDropDownType;
    }
}
