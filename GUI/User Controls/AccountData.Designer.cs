namespace GUI
{
    partial class AccountData
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
            Telerik.WinControls.UI.GridViewSummaryItem gridViewSummaryItem1 = new Telerik.WinControls.UI.GridViewSummaryItem();
            Telerik.WinControls.UI.GridViewSummaryItem gridViewSummaryItem2 = new Telerik.WinControls.UI.GridViewSummaryItem();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            Telerik.WinControls.UI.GridViewSummaryItem gridViewSummaryItem3 = new Telerik.WinControls.UI.GridViewSummaryItem();
            Telerik.WinControls.UI.GridViewSummaryItem gridViewSummaryItem4 = new Telerik.WinControls.UI.GridViewSummaryItem();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
            Telerik.WinControls.UI.GridViewSummaryItem gridViewSummaryItem5 = new Telerik.WinControls.UI.GridViewSummaryItem();
            Telerik.WinControls.UI.GridViewSummaryItem gridViewSummaryItem6 = new Telerik.WinControls.UI.GridViewSummaryItem();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition3 = new Telerik.WinControls.UI.TableViewDefinition();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition4 = new Telerik.WinControls.UI.TableViewDefinition();
            this.rpvAccountData = new Telerik.WinControls.UI.RadPageView();
            this.tabDebitor = new Telerik.WinControls.UI.RadPageViewPage();
            this.rgvDebitor = new Telerik.WinControls.UI.RadGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.debitorMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabCreditor = new Telerik.WinControls.UI.RadPageViewPage();
            this.rgvCreditor = new Telerik.WinControls.UI.RadGridView();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.creditorMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabOpenLines = new Telerik.WinControls.UI.RadPageViewPage();
            this.rgvOpenlines = new Telerik.WinControls.UI.RadGridView();
            this.menuStrip3 = new System.Windows.Forms.MenuStrip();
            this.openMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabInvoice = new Telerik.WinControls.UI.RadPageViewPage();
            this.rgvInvoice = new Telerik.WinControls.UI.RadGridView();
            this.menuStrip4 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            ((System.ComponentModel.ISupportInitialize)(this.rpvAccountData)).BeginInit();
            this.rpvAccountData.SuspendLayout();
            this.tabDebitor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgvDebitor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvDebitor.MasterTemplate)).BeginInit();
            this.rgvDebitor.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabCreditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgvCreditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvCreditor.MasterTemplate)).BeginInit();
            this.rgvCreditor.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.tabOpenLines.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgvOpenlines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvOpenlines.MasterTemplate)).BeginInit();
            this.rgvOpenlines.SuspendLayout();
            this.menuStrip3.SuspendLayout();
            this.tabInvoice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgvInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvInvoice.MasterTemplate)).BeginInit();
            this.rgvInvoice.SuspendLayout();
            this.menuStrip4.SuspendLayout();
            this.SuspendLayout();
            // 
            // rpvAccountData
            // 
            this.rpvAccountData.Controls.Add(this.tabDebitor);
            this.rpvAccountData.Controls.Add(this.tabCreditor);
            this.rpvAccountData.Controls.Add(this.tabOpenLines);
            this.rpvAccountData.Controls.Add(this.tabInvoice);
            this.rpvAccountData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpvAccountData.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rpvAccountData.Location = new System.Drawing.Point(0, 0);
            this.rpvAccountData.Name = "rpvAccountData";
            this.rpvAccountData.SelectedPage = this.tabOpenLines;
            this.rpvAccountData.Size = new System.Drawing.Size(823, 537);
            this.rpvAccountData.TabIndex = 0;
            this.rpvAccountData.Text = "radPageView1";
            this.rpvAccountData.ThemeName = "VisualStudio2012Light";
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.rpvAccountData.GetChildAt(0))).StripButtons = Telerik.WinControls.UI.StripViewButtons.None;
            // 
            // tabDebitor
            // 
            this.tabDebitor.Controls.Add(this.rgvDebitor);
            this.tabDebitor.Enabled = false;
            this.tabDebitor.ItemSize = new System.Drawing.SizeF(58F, 24F);
            this.tabDebitor.Location = new System.Drawing.Point(5, 30);
            this.tabDebitor.Name = "tabDebitor";
            this.tabDebitor.Size = new System.Drawing.Size(813, 502);
            this.tabDebitor.Text = "Debitor";
            // 
            // rgvDebitor
            // 
            this.rgvDebitor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.rgvDebitor.Controls.Add(this.menuStrip1);
            this.rgvDebitor.Cursor = System.Windows.Forms.Cursors.Default;
            this.rgvDebitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rgvDebitor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rgvDebitor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rgvDebitor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rgvDebitor.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.rgvDebitor.MasterTemplate.AllowAddNewRow = false;
            this.rgvDebitor.MasterTemplate.AllowSearchRow = true;
            this.rgvDebitor.MasterTemplate.EnableFiltering = true;
            gridViewSummaryItem1.Aggregate = Telerik.WinControls.UI.GridAggregateFunction.Sum;
            gridViewSummaryItem1.AggregateExpression = null;
            gridViewSummaryItem1.FormatString = "{0}";
            gridViewSummaryItem1.Name = "debitLine";
            gridViewSummaryItem2.Aggregate = Telerik.WinControls.UI.GridAggregateFunction.Sum;
            gridViewSummaryItem2.AggregateExpression = null;
            gridViewSummaryItem2.FormatString = "{0}";
            gridViewSummaryItem2.Name = "creditLine";
            this.rgvDebitor.MasterTemplate.SummaryRowsBottom.Add(new Telerik.WinControls.UI.GridViewSummaryRowItem(new Telerik.WinControls.UI.GridViewSummaryItem[] {
                gridViewSummaryItem1,
                gridViewSummaryItem2}));
            this.rgvDebitor.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.rgvDebitor.Name = "rgvDebitor";
            this.rgvDebitor.ReadOnly = true;
            this.rgvDebitor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rgvDebitor.Size = new System.Drawing.Size(813, 502);
            this.rgvDebitor.TabIndex = 0;
            this.rgvDebitor.ThemeName = "VisualStudio2012Light";
            this.rgvDebitor.ViewCellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.rgvDebitor_ViewCellFormatting);
            this.rgvDebitor.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.rgvDebitor_DataBindingComplete);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debitorMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 478);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(813, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // debitorMenuItem1
            // 
            this.debitorMenuItem1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.debitorMenuItem1.Name = "debitorMenuItem1";
            this.debitorMenuItem1.Size = new System.Drawing.Size(97, 20);
            this.debitorMenuItem1.Text = "Save Layout";
            this.debitorMenuItem1.Click += new System.EventHandler(this.debitorMenuItem1_Click);
            // 
            // tabCreditor
            // 
            this.tabCreditor.Controls.Add(this.rgvCreditor);
            this.tabCreditor.Enabled = false;
            this.tabCreditor.ItemSize = new System.Drawing.SizeF(63F, 24F);
            this.tabCreditor.Location = new System.Drawing.Point(5, 30);
            this.tabCreditor.Name = "tabCreditor";
            this.tabCreditor.Size = new System.Drawing.Size(813, 502);
            this.tabCreditor.Text = "Creditor";
            // 
            // rgvCreditor
            // 
            this.rgvCreditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.rgvCreditor.Controls.Add(this.menuStrip2);
            this.rgvCreditor.Cursor = System.Windows.Forms.Cursors.Default;
            this.rgvCreditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rgvCreditor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rgvCreditor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rgvCreditor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rgvCreditor.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.rgvCreditor.MasterTemplate.AllowAddNewRow = false;
            this.rgvCreditor.MasterTemplate.AllowSearchRow = true;
            this.rgvCreditor.MasterTemplate.EnableFiltering = true;
            gridViewSummaryItem3.Aggregate = Telerik.WinControls.UI.GridAggregateFunction.Sum;
            gridViewSummaryItem3.AggregateExpression = null;
            gridViewSummaryItem3.FormatString = "{0}";
            gridViewSummaryItem3.Name = "debitLine";
            gridViewSummaryItem4.Aggregate = Telerik.WinControls.UI.GridAggregateFunction.Sum;
            gridViewSummaryItem4.AggregateExpression = null;
            gridViewSummaryItem4.FormatString = "{0}";
            gridViewSummaryItem4.Name = "creditLine";
            this.rgvCreditor.MasterTemplate.SummaryRowsBottom.Add(new Telerik.WinControls.UI.GridViewSummaryRowItem(new Telerik.WinControls.UI.GridViewSummaryItem[] {
                gridViewSummaryItem3,
                gridViewSummaryItem4}));
            this.rgvCreditor.MasterTemplate.ViewDefinition = tableViewDefinition2;
            this.rgvCreditor.Name = "rgvCreditor";
            this.rgvCreditor.ReadOnly = true;
            this.rgvCreditor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rgvCreditor.Size = new System.Drawing.Size(813, 502);
            this.rgvCreditor.TabIndex = 1;
            this.rgvCreditor.ThemeName = "VisualStudio2012Light";
            this.rgvCreditor.ViewCellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.rgvCreditor_ViewCellFormatting);
            this.rgvCreditor.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.rgvCreditor_DataBindingComplete);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.creditorMenuItem1});
            this.menuStrip2.Location = new System.Drawing.Point(0, 478);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(813, 24);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // creditorMenuItem1
            // 
            this.creditorMenuItem1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.creditorMenuItem1.Name = "creditorMenuItem1";
            this.creditorMenuItem1.Size = new System.Drawing.Size(97, 20);
            this.creditorMenuItem1.Text = "Save Layout";
            this.creditorMenuItem1.Click += new System.EventHandler(this.creditorMenuItem1_Click);
            // 
            // tabOpenLines
            // 
            this.tabOpenLines.Controls.Add(this.rgvOpenlines);
            this.tabOpenLines.Enabled = false;
            this.tabOpenLines.ItemSize = new System.Drawing.SizeF(78F, 24F);
            this.tabOpenLines.Location = new System.Drawing.Point(5, 30);
            this.tabOpenLines.Name = "tabOpenLines";
            this.tabOpenLines.Size = new System.Drawing.Size(813, 502);
            this.tabOpenLines.Text = "Open lines";
            // 
            // rgvOpenlines
            // 
            this.rgvOpenlines.Controls.Add(this.menuStrip3);
            this.rgvOpenlines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rgvOpenlines.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rgvOpenlines.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.rgvOpenlines.MasterTemplate.AllowAddNewRow = false;
            this.rgvOpenlines.MasterTemplate.AllowSearchRow = true;
            this.rgvOpenlines.MasterTemplate.EnableFiltering = true;
            gridViewSummaryItem5.Aggregate = Telerik.WinControls.UI.GridAggregateFunction.Sum;
            gridViewSummaryItem5.AggregateExpression = null;
            gridViewSummaryItem5.FormatString = "{0}";
            gridViewSummaryItem5.Name = "debitOpenLine";
            gridViewSummaryItem6.Aggregate = Telerik.WinControls.UI.GridAggregateFunction.Sum;
            gridViewSummaryItem6.AggregateExpression = null;
            gridViewSummaryItem6.FormatString = "{0}";
            gridViewSummaryItem6.Name = "creditOpenLine";
            this.rgvOpenlines.MasterTemplate.SummaryRowsBottom.Add(new Telerik.WinControls.UI.GridViewSummaryRowItem(new Telerik.WinControls.UI.GridViewSummaryItem[] {
                gridViewSummaryItem5,
                gridViewSummaryItem6}));
            this.rgvOpenlines.MasterTemplate.ViewDefinition = tableViewDefinition3;
            this.rgvOpenlines.Name = "rgvOpenlines";
            this.rgvOpenlines.ReadOnly = true;
            this.rgvOpenlines.Size = new System.Drawing.Size(813, 502);
            this.rgvOpenlines.TabIndex = 2;
            this.rgvOpenlines.ThemeName = "VisualStudio2012Light";
            this.rgvOpenlines.ViewCellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.rgvOpenlines_ViewCellFormatting);
            this.rgvOpenlines.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.rgvOpenlines_DataBindingComplete);
            // 
            // menuStrip3
            // 
            this.menuStrip3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMenuItem1});
            this.menuStrip3.Location = new System.Drawing.Point(0, 478);
            this.menuStrip3.Name = "menuStrip3";
            this.menuStrip3.Size = new System.Drawing.Size(813, 24);
            this.menuStrip3.TabIndex = 1;
            this.menuStrip3.Text = "menuStrip3";
            // 
            // openMenuItem1
            // 
            this.openMenuItem1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.openMenuItem1.Name = "openMenuItem1";
            this.openMenuItem1.Size = new System.Drawing.Size(97, 20);
            this.openMenuItem1.Text = "Save Layout";
            this.openMenuItem1.Click += new System.EventHandler(this.openMenuItem1_Click);
            // 
            // tabInvoice
            // 
            this.tabInvoice.Controls.Add(this.rgvInvoice);
            this.tabInvoice.ItemSize = new System.Drawing.SizeF(58F, 24F);
            this.tabInvoice.Location = new System.Drawing.Point(5, 30);
            this.tabInvoice.Name = "tabInvoice";
            this.tabInvoice.Size = new System.Drawing.Size(813, 502);
            this.tabInvoice.Text = "Invoice";
            // 
            // rgvInvoice
            // 
            this.rgvInvoice.Controls.Add(this.menuStrip4);
            this.rgvInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rgvInvoice.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rgvInvoice.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.rgvInvoice.MasterTemplate.AllowAddNewRow = false;
            this.rgvInvoice.MasterTemplate.ViewDefinition = tableViewDefinition4;
            this.rgvInvoice.Name = "rgvInvoice";
            this.rgvInvoice.ReadOnly = true;
            this.rgvInvoice.Size = new System.Drawing.Size(813, 502);
            this.rgvInvoice.TabIndex = 0;
            this.rgvInvoice.ThemeName = "VisualStudio2012Light";
            this.rgvInvoice.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.rgvInvoice_CellDoubleClick);
            this.rgvInvoice.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.rgvInvoice_DataBindingComplete);
            // 
            // menuStrip4
            // 
            this.menuStrip4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip4.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip4.Location = new System.Drawing.Point(0, 478);
            this.menuStrip4.Name = "menuStrip4";
            this.menuStrip4.Size = new System.Drawing.Size(813, 24);
            this.menuStrip4.TabIndex = 1;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(82, 20);
            this.toolStripMenuItem1.Text = "Save Layout";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // AccountData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rpvAccountData);
            this.Name = "AccountData";
            this.Size = new System.Drawing.Size(823, 537);
            this.Load += new System.EventHandler(this.AccountData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rpvAccountData)).EndInit();
            this.rpvAccountData.ResumeLayout(false);
            this.tabDebitor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgvDebitor.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvDebitor)).EndInit();
            this.rgvDebitor.ResumeLayout(false);
            this.rgvDebitor.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabCreditor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgvCreditor.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvCreditor)).EndInit();
            this.rgvCreditor.ResumeLayout(false);
            this.rgvCreditor.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.tabOpenLines.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgvOpenlines.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvOpenlines)).EndInit();
            this.rgvOpenlines.ResumeLayout(false);
            this.rgvOpenlines.PerformLayout();
            this.menuStrip3.ResumeLayout(false);
            this.menuStrip3.PerformLayout();
            this.tabInvoice.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rgvInvoice.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvInvoice)).EndInit();
            this.rgvInvoice.ResumeLayout(false);
            this.rgvInvoice.PerformLayout();
            this.menuStrip4.ResumeLayout(false);
            this.menuStrip4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPageView rpvAccountData;
        private Telerik.WinControls.UI.RadPageViewPage tabDebitor;
        private Telerik.WinControls.UI.RadPageViewPage tabCreditor;
        private Telerik.WinControls.UI.RadPageViewPage tabOpenLines;
        private Telerik.WinControls.UI.RadGridView rgvDebitor;
        private Telerik.WinControls.UI.RadGridView rgvCreditor;
        private Telerik.WinControls.UI.RadGridView rgvOpenlines;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem debitorMenuItem1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem creditorMenuItem1;
        private System.Windows.Forms.MenuStrip menuStrip3;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem1;
        private Telerik.WinControls.UI.RadPageViewPage tabInvoice;
        private Telerik.WinControls.UI.RadGridView rgvInvoice;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private System.Windows.Forms.MenuStrip menuStrip4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}
