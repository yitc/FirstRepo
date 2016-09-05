namespace GUI
{
    partial class frmInvoiceSelection
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
            this.panelSearchBy = new Telerik.WinControls.UI.RadPanel();
            this.btnDaily = new Telerik.WinControls.UI.RadButton();
            this.txtDaily = new Telerik.WinControls.UI.RadTextBox();
            this.btnBooking = new Telerik.WinControls.UI.RadButton();
            this.radioSearchNone = new Telerik.WinControls.UI.RadRadioButton();
            this.btnDo = new Telerik.WinControls.UI.RadButton();
            this.radioSeatchByLabel = new Telerik.WinControls.UI.RadRadioButton();
            this.ddlLabel = new Telerik.WinControls.UI.RadDropDownList();
            this.radioSearchByArr = new Telerik.WinControls.UI.RadRadioButton();
            this.txtArrangement = new Telerik.WinControls.UI.RadTextBox();
            this.ddlStatus = new Telerik.WinControls.UI.RadDropDownList();
            this.btnArrangement = new Telerik.WinControls.UI.RadButton();
            this.lblStatus = new Telerik.WinControls.UI.RadLabel();
            this.rgvInvoice = new Telerik.WinControls.UI.RadGridView();
            this.btnExit = new Telerik.WinControls.UI.RadButton();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.btnSelectAll = new Telerik.WinControls.UI.RadCheckBox();
            this.radMenuButtonSaveLayout = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenu1 = new Telerik.WinControls.UI.RadMenu();
            ((System.ComponentModel.ISupportInitialize)(this.panelSearchBy)).BeginInit();
            this.panelSearchBy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnDaily)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDaily)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioSearchNone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioSeatchByLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioSearchByArr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtArrangement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnArrangement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvInvoice.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSearchBy
            // 
            this.panelSearchBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSearchBy.Controls.Add(this.btnDaily);
            this.panelSearchBy.Controls.Add(this.txtDaily);
            this.panelSearchBy.Controls.Add(this.btnBooking);
            this.panelSearchBy.Controls.Add(this.radioSearchNone);
            this.panelSearchBy.Controls.Add(this.btnDo);
            this.panelSearchBy.Controls.Add(this.radioSeatchByLabel);
            this.panelSearchBy.Controls.Add(this.ddlLabel);
            this.panelSearchBy.Controls.Add(this.radioSearchByArr);
            this.panelSearchBy.Controls.Add(this.txtArrangement);
            this.panelSearchBy.Controls.Add(this.ddlStatus);
            this.panelSearchBy.Controls.Add(this.btnArrangement);
            this.panelSearchBy.Controls.Add(this.lblStatus);
            this.panelSearchBy.Location = new System.Drawing.Point(12, 12);
            this.panelSearchBy.Name = "panelSearchBy";
            this.panelSearchBy.Size = new System.Drawing.Size(1203, 103);
            this.panelSearchBy.TabIndex = 20;
            this.panelSearchBy.Text = "Search by";
            this.panelSearchBy.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            // 
            // btnDaily
            // 
            this.btnDaily.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDaily.Image = global::GUI.Properties.Resources.lookup_x20;
            this.btnDaily.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDaily.Location = new System.Drawing.Point(877, 37);
            this.btnDaily.Name = "btnDaily";
            this.btnDaily.Size = new System.Drawing.Size(20, 20);
            this.btnDaily.TabIndex = 23;
            this.btnDaily.Text = "...";
            this.btnDaily.ThemeName = "Windows8";
            this.btnDaily.Click += new System.EventHandler(this.btnDaily_Click);
            // 
            // txtDaily
            // 
            this.txtDaily.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtDaily.Location = new System.Drawing.Point(671, 38);
            this.txtDaily.Name = "txtDaily";
            this.txtDaily.ReadOnly = true;
            this.txtDaily.Size = new System.Drawing.Size(200, 20);
            this.txtDaily.TabIndex = 22;
            this.txtDaily.ThemeName = "Windows8";
            // 
            // btnBooking
            // 
            this.btnBooking.Enabled = false;
            this.btnBooking.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnBooking.Location = new System.Drawing.Point(761, 64);
            this.btnBooking.Name = "btnBooking";
            this.btnBooking.Size = new System.Drawing.Size(110, 24);
            this.btnBooking.TabIndex = 21;
            this.btnBooking.Text = "Booking";
            this.btnBooking.ThemeName = "Windows8";
            this.btnBooking.Click += new System.EventHandler(this.btnBooking_Click);
            // 
            // radioSearchNone
            // 
            this.radioSearchNone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.radioSearchNone.Font = new System.Drawing.Font("Verdana", 9F);
            this.radioSearchNone.Location = new System.Drawing.Point(12, 20);
            this.radioSearchNone.Name = "radioSearchNone";
            this.radioSearchNone.Size = new System.Drawing.Size(55, 18);
            this.radioSearchNone.TabIndex = 20;
            this.radioSearchNone.TabStop = false;
            this.radioSearchNone.Text = "None";
            this.radioSearchNone.ThemeName = "Windows8";
            this.radioSearchNone.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // btnDo
            // 
            this.btnDo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDo.Location = new System.Drawing.Point(514, 64);
            this.btnDo.Name = "btnDo";
            this.btnDo.Size = new System.Drawing.Size(110, 24);
            this.btnDo.TabIndex = 19;
            this.btnDo.Text = "Do";
            this.btnDo.ThemeName = "Windows8";
            this.btnDo.Click += new System.EventHandler(this.btnDo_Click);
            // 
            // radioSeatchByLabel
            // 
            this.radioSeatchByLabel.Font = new System.Drawing.Font("Verdana", 9F);
            this.radioSeatchByLabel.Location = new System.Drawing.Point(12, 68);
            this.radioSeatchByLabel.Name = "radioSeatchByLabel";
            this.radioSeatchByLabel.Size = new System.Drawing.Size(56, 18);
            this.radioSeatchByLabel.TabIndex = 1;
            this.radioSeatchByLabel.TabStop = false;
            this.radioSeatchByLabel.Text = "Label";
            this.radioSeatchByLabel.ThemeName = "Windows8";
            // 
            // ddlLabel
            // 
            this.ddlLabel.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.ddlLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ddlLabel.Location = new System.Drawing.Point(123, 68);
            this.ddlLabel.Name = "ddlLabel";
            this.ddlLabel.Size = new System.Drawing.Size(200, 20);
            this.ddlLabel.TabIndex = 18;
            this.ddlLabel.ThemeName = "Windows8";
            // 
            // radioSearchByArr
            // 
            this.radioSearchByArr.Font = new System.Drawing.Font("Verdana", 9F);
            this.radioSearchByArr.Location = new System.Drawing.Point(12, 44);
            this.radioSearchByArr.Name = "radioSearchByArr";
            this.radioSearchByArr.Size = new System.Drawing.Size(105, 18);
            this.radioSearchByArr.TabIndex = 0;
            this.radioSearchByArr.TabStop = false;
            this.radioSearchByArr.Text = "Arrangement";
            this.radioSearchByArr.ThemeName = "Windows8";
            // 
            // txtArrangement
            // 
            this.txtArrangement.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtArrangement.Location = new System.Drawing.Point(123, 42);
            this.txtArrangement.Name = "txtArrangement";
            this.txtArrangement.ReadOnly = true;
            this.txtArrangement.Size = new System.Drawing.Size(200, 20);
            this.txtArrangement.TabIndex = 11;
            this.txtArrangement.ThemeName = "Windows8";
            // 
            // ddlStatus
            // 
            this.ddlStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.ddlStatus.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ddlStatus.Location = new System.Drawing.Point(424, 40);
            this.ddlStatus.Name = "ddlStatus";
            this.ddlStatus.Size = new System.Drawing.Size(200, 20);
            this.ddlStatus.TabIndex = 14;
            this.ddlStatus.ThemeName = "Windows8";
            // 
            // btnArrangement
            // 
            this.btnArrangement.Image = global::GUI.Properties.Resources.lookup_x20;
            this.btnArrangement.Location = new System.Drawing.Point(329, 42);
            this.btnArrangement.Name = "btnArrangement";
            this.btnArrangement.Size = new System.Drawing.Size(20, 20);
            this.btnArrangement.TabIndex = 12;
            this.btnArrangement.Text = "...";
            this.btnArrangement.ThemeName = "Windows8";
            this.btnArrangement.Click += new System.EventHandler(this.btnArrangement_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblStatus.Location = new System.Drawing.Point(372, 42);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(46, 18);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "Status";
            // 
            // rgvInvoice
            // 
            this.rgvInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rgvInvoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.rgvInvoice.Cursor = System.Windows.Forms.Cursors.Default;
            this.rgvInvoice.Font = new System.Drawing.Font("Verdana", 9F);
            this.rgvInvoice.ForeColor = System.Drawing.Color.Black;
            this.rgvInvoice.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rgvInvoice.Location = new System.Drawing.Point(12, 143);
            // 
            // 
            // 
            this.rgvInvoice.MasterTemplate.AllowAddNewRow = false;
            this.rgvInvoice.MasterTemplate.AllowDeleteRow = false;
            this.rgvInvoice.MasterTemplate.AllowRowResize = false;
            this.rgvInvoice.MasterTemplate.AllowSearchRow = true;
            this.rgvInvoice.MasterTemplate.EnableFiltering = true;
            this.rgvInvoice.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.rgvInvoice.Name = "rgvInvoice";
            this.rgvInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rgvInvoice.Size = new System.Drawing.Size(1203, 518);
            this.rgvInvoice.TabIndex = 21;
            this.rgvInvoice.ThemeName = "VisualStudio2012Light";
            this.rgvInvoice.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.rgvInvoice_DataBindingComplete);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnExit.Location = new System.Drawing.Point(1105, 689);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(110, 24);
            this.btnExit.TabIndex = 22;
            this.btnExit.Text = "Exit";
            this.btnExit.ThemeName = "Windows8";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Font = new System.Drawing.Font("Verdana", 9F);
            this.btnSelectAll.Location = new System.Drawing.Point(12, 121);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(81, 18);
            this.btnSelectAll.TabIndex = 23;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.ThemeName = "Windows8";
            this.btnSelectAll.CheckStateChanged += new System.EventHandler(this.btnSelectAll_CheckStateChanged);
            // 
            // radMenuButtonSaveLayout
            // 
            this.radMenuButtonSaveLayout.Name = "radMenuButtonSaveLayout";
            this.radMenuButtonSaveLayout.Text = "Save Layout";
            this.radMenuButtonSaveLayout.Click += new System.EventHandler(this.radMenuButtonSaveLayout_Click);
            // 
            // radMenu1
            // 
            this.radMenu1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radMenu1.Dock = System.Windows.Forms.DockStyle.None;
            this.radMenu1.Font = new System.Drawing.Font("Verdana", 9F);
            this.radMenu1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radMenuButtonSaveLayout});
            this.radMenu1.Location = new System.Drawing.Point(12, 663);
            this.radMenu1.Name = "radMenu1";
            this.radMenu1.Size = new System.Drawing.Size(1203, 20);
            this.radMenu1.TabIndex = 24;
            this.radMenu1.Text = "radMenu1";
            this.radMenu1.ThemeName = "Windows8";
            // 
            // frmInvoiceSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 725);
            this.Controls.Add(this.radMenu1);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.rgvInvoice);
            this.Controls.Add(this.panelSearchBy);
            this.Name = "frmInvoiceSelection";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invoice Selection";
            this.ThemeName = "Windows8";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmInvoiceSelection_FormClosing);
            this.Load += new System.EventHandler(this.frmInvoiceSelection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelSearchBy)).EndInit();
            this.panelSearchBy.ResumeLayout(false);
            this.panelSearchBy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnDaily)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDaily)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioSearchNone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioSeatchByLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioSearchByArr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtArrangement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnArrangement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvInvoice.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel panelSearchBy;
        private Telerik.WinControls.UI.RadButton btnDo;
        private Telerik.WinControls.UI.RadRadioButton radioSeatchByLabel;
        private Telerik.WinControls.UI.RadDropDownList ddlLabel;
        private Telerik.WinControls.UI.RadRadioButton radioSearchByArr;
        private Telerik.WinControls.UI.RadTextBox txtArrangement;
        private Telerik.WinControls.UI.RadDropDownList ddlStatus;
        private Telerik.WinControls.UI.RadButton btnArrangement;
        private Telerik.WinControls.UI.RadLabel lblStatus;
        private Telerik.WinControls.UI.RadGridView rgvInvoice;
        private Telerik.WinControls.UI.RadButton btnExit;
        private Telerik.WinControls.UI.RadRadioButton radioSearchNone;
        private Telerik.WinControls.UI.RadButton btnBooking;
        private Telerik.WinControls.UI.RadButton btnDaily;
        private Telerik.WinControls.UI.RadTextBox txtDaily;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Telerik.WinControls.UI.RadCheckBox btnSelectAll;
        private Telerik.WinControls.UI.RadMenuItem radMenuButtonSaveLayout;
        private Telerik.WinControls.UI.RadMenu radMenu1;

    }
}
