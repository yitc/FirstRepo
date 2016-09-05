namespace GUI
{
    partial class frmInvoiceSelectionForBooking
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
            this.txtArrangement = new Telerik.WinControls.UI.RadTextBox();
            this.btnArrangement = new Telerik.WinControls.UI.RadButton();
            this.lblStatus = new Telerik.WinControls.UI.RadLabel();
            this.ddlStatus = new Telerik.WinControls.UI.RadDropDownList();
            this.btnExit = new Telerik.WinControls.UI.RadButton();
            this.ddlLabel = new Telerik.WinControls.UI.RadDropDownList();
            this.panelSearchBy = new Telerik.WinControls.UI.RadPanel();
            this.panelSendBy = new Telerik.WinControls.UI.RadPanel();
            this.radioByPost = new Telerik.WinControls.UI.RadRadioButton();
            this.btnSendByEmail = new Telerik.WinControls.UI.RadButton();
            this.radioByEmail = new Telerik.WinControls.UI.RadRadioButton();
            this.btnSendByPost = new Telerik.WinControls.UI.RadButton();
            this.btnDo = new Telerik.WinControls.UI.RadButton();
            this.radioSeatchByLabel = new Telerik.WinControls.UI.RadRadioButton();
            this.radioSearchByArr = new Telerik.WinControls.UI.RadRadioButton();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.rgvInvoice = new Telerik.WinControls.UI.RadGridView();
            this.radMenu1 = new Telerik.WinControls.UI.RadMenu();
            this.radMenuButtonSaveLayout = new Telerik.WinControls.UI.RadMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.txtArrangement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnArrangement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelSearchBy)).BeginInit();
            this.panelSearchBy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelSendBy)).BeginInit();
            this.panelSendBy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioByPost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSendByEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioByEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSendByPost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioSeatchByLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioSearchByArr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvInvoice.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // txtArrangement
            // 
            this.txtArrangement.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtArrangement.Location = new System.Drawing.Point(123, 22);
            this.txtArrangement.Name = "txtArrangement";
            this.txtArrangement.ReadOnly = true;
            this.txtArrangement.Size = new System.Drawing.Size(200, 20);
            this.txtArrangement.TabIndex = 11;
            this.txtArrangement.ThemeName = "Windows8";
            // 
            // btnArrangement
            // 
            this.btnArrangement.Location = new System.Drawing.Point(329, 22);
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
            this.lblStatus.Location = new System.Drawing.Point(368, 22);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(46, 18);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "Status";
            // 
            // ddlStatus
            // 
            this.ddlStatus.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.ddlStatus.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ddlStatus.Location = new System.Drawing.Point(420, 20);
            this.ddlStatus.Name = "ddlStatus";
            this.ddlStatus.Size = new System.Drawing.Size(200, 20);
            this.ddlStatus.TabIndex = 14;
            this.ddlStatus.ThemeName = "Windows8";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnExit.Location = new System.Drawing.Point(1105, 665);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(110, 24);
            this.btnExit.TabIndex = 16;
            this.btnExit.Text = "Exit";
            this.btnExit.ThemeName = "Windows8";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ddlLabel
            // 
            this.ddlLabel.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.ddlLabel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ddlLabel.Location = new System.Drawing.Point(123, 48);
            this.ddlLabel.Name = "ddlLabel";
            this.ddlLabel.Size = new System.Drawing.Size(200, 20);
            this.ddlLabel.TabIndex = 18;
            this.ddlLabel.ThemeName = "Windows8";
            // 
            // panelSearchBy
            // 
            this.panelSearchBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSearchBy.Controls.Add(this.panelSendBy);
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
            this.panelSearchBy.Size = new System.Drawing.Size(1203, 83);
            this.panelSearchBy.TabIndex = 19;
            this.panelSearchBy.Text = "Search by";
            this.panelSearchBy.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            // 
            // panelSendBy
            // 
            this.panelSendBy.Controls.Add(this.radioByPost);
            this.panelSendBy.Controls.Add(this.btnSendByEmail);
            this.panelSendBy.Controls.Add(this.radioByEmail);
            this.panelSendBy.Controls.Add(this.btnSendByPost);
            this.panelSendBy.Location = new System.Drawing.Point(626, 8);
            this.panelSendBy.Name = "panelSendBy";
            this.panelSendBy.Size = new System.Drawing.Size(290, 58);
            this.panelSendBy.TabIndex = 20;
            // 
            // radioByPost
            // 
            this.radioByPost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioByPost.Font = new System.Drawing.Font("Verdana", 9F);
            this.radioByPost.Location = new System.Drawing.Point(64, 3);
            this.radioByPost.Name = "radioByPost";
            this.radioByPost.Size = new System.Drawing.Size(50, 18);
            this.radioByPost.TabIndex = 11;
            this.radioByPost.TabStop = false;
            this.radioByPost.Text = "Post";
            this.radioByPost.ThemeName = "Windows8";
            this.radioByPost.CheckStateChanging += new Telerik.WinControls.UI.CheckStateChangingEventHandler(this.radioByPost_CheckStateChanging);
            this.radioByPost.CheckStateChanged += new System.EventHandler(this.radioByPost_CheckStateChanged);
            // 
            // btnSendByEmail
            // 
            this.btnSendByEmail.BackColor = System.Drawing.Color.Transparent;
            this.btnSendByEmail.Image = global::GUI.Properties.Resources.Email;
            this.btnSendByEmail.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSendByEmail.Location = new System.Drawing.Point(163, 5);
            this.btnSendByEmail.Name = "btnSendByEmail";
            this.btnSendByEmail.Size = new System.Drawing.Size(59, 46);
            this.btnSendByEmail.TabIndex = 8;
            this.btnSendByEmail.Visible = false;
            this.btnSendByEmail.Click += new System.EventHandler(this.btnSendByEmail_Click);
            // 
            // radioByEmail
            // 
            this.radioByEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioByEmail.Font = new System.Drawing.Font("Verdana", 9F);
            this.radioByEmail.Location = new System.Drawing.Point(3, 4);
            this.radioByEmail.Name = "radioByEmail";
            this.radioByEmail.Size = new System.Drawing.Size(58, 18);
            this.radioByEmail.TabIndex = 10;
            this.radioByEmail.TabStop = false;
            this.radioByEmail.Text = "Email";
            this.radioByEmail.ThemeName = "Windows8";
            this.radioByEmail.CheckStateChanging += new Telerik.WinControls.UI.CheckStateChangingEventHandler(this.radioByEmail_CheckStateChanging);
            this.radioByEmail.CheckStateChanged += new System.EventHandler(this.radioByEmail_CheckStateChanged);
            // 
            // btnSendByPost
            // 
            this.btnSendByPost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendByPost.BackColor = System.Drawing.Color.Transparent;
            this.btnSendByPost.BackgroundImage = global::GUI.Properties.Resources.raport_x32;
            this.btnSendByPost.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSendByPost.Location = new System.Drawing.Point(228, 5);
            this.btnSendByPost.Name = "btnSendByPost";
            this.btnSendByPost.Size = new System.Drawing.Size(59, 46);
            this.btnSendByPost.TabIndex = 5;
            this.btnSendByPost.ThemeName = "Windows8";
            this.btnSendByPost.Visible = false;
            this.btnSendByPost.Click += new System.EventHandler(this.btnSendByPost_Click);
            // 
            // btnDo
            // 
            this.btnDo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDo.Location = new System.Drawing.Point(510, 44);
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
            this.radioSeatchByLabel.Location = new System.Drawing.Point(12, 48);
            this.radioSeatchByLabel.Name = "radioSeatchByLabel";
            this.radioSeatchByLabel.Size = new System.Drawing.Size(56, 18);
            this.radioSeatchByLabel.TabIndex = 1;
            this.radioSeatchByLabel.TabStop = false;
            this.radioSeatchByLabel.Text = "Label";
            this.radioSeatchByLabel.ThemeName = "Windows8";
            // 
            // radioSearchByArr
            // 
            this.radioSearchByArr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.radioSearchByArr.Font = new System.Drawing.Font("Verdana", 9F);
            this.radioSearchByArr.Location = new System.Drawing.Point(12, 24);
            this.radioSearchByArr.Name = "radioSearchByArr";
            this.radioSearchByArr.Size = new System.Drawing.Size(105, 18);
            this.radioSearchByArr.TabIndex = 0;
            this.radioSearchByArr.Text = "Arrangement";
            this.radioSearchByArr.ThemeName = "Windows8";
            this.radioSearchByArr.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
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
            this.rgvInvoice.Location = new System.Drawing.Point(12, 101);
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
            this.rgvInvoice.Size = new System.Drawing.Size(1203, 532);
            this.rgvInvoice.TabIndex = 20;
            this.rgvInvoice.ThemeName = "VisualStudio2012Light";
            this.rgvInvoice.RowFormatting += new Telerik.WinControls.UI.RowFormattingEventHandler(this.rgvInvoice_RowFormatting);
            this.rgvInvoice.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.rgvInvoice_CellFormatting);
            this.rgvInvoice.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.rgvInvoice_DataBindingComplete);
            // 
            // radMenu1
            // 
            this.radMenu1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radMenu1.Dock = System.Windows.Forms.DockStyle.None;
            this.radMenu1.Font = new System.Drawing.Font("Verdana", 9F);
            this.radMenu1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radMenuButtonSaveLayout});
            this.radMenu1.Location = new System.Drawing.Point(12, 639);
            this.radMenu1.Name = "radMenu1";
            this.radMenu1.Size = new System.Drawing.Size(1203, 20);
            this.radMenu1.TabIndex = 25;
            this.radMenu1.Text = "radMenu1";
            this.radMenu1.ThemeName = "Windows8";
            // 
            // radMenuButtonSaveLayout
            // 
            this.radMenuButtonSaveLayout.Name = "radMenuButtonSaveLayout";
            this.radMenuButtonSaveLayout.Text = "Save Layout";
            this.radMenuButtonSaveLayout.Click += new System.EventHandler(this.radMenuButtonSaveLayout_Click);
            // 
            // frmInvoiceSelectionForBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(1227, 695);
            this.Controls.Add(this.radMenu1);
            this.Controls.Add(this.rgvInvoice);
            this.Controls.Add(this.panelSearchBy);
            this.Controls.Add(this.btnExit);
            this.Name = "frmInvoiceSelectionForBooking";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invoice Selection for Booking";
            this.ThemeName = "Windows8";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmInvoiceSelectionForBooking_FormClosing);
            this.Load += new System.EventHandler(this.frmInvoiceSelectionForBooking_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtArrangement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnArrangement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelSearchBy)).EndInit();
            this.panelSearchBy.ResumeLayout(false);
            this.panelSearchBy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelSendBy)).EndInit();
            this.panelSendBy.ResumeLayout(false);
            this.panelSendBy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioByPost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSendByEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioByEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSendByPost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioSeatchByLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioSearchByArr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvInvoice.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBox txtArrangement;
        private Telerik.WinControls.UI.RadButton btnArrangement;
        private Telerik.WinControls.UI.RadLabel lblStatus;
        private Telerik.WinControls.UI.RadDropDownList ddlStatus;
        private Telerik.WinControls.UI.RadButton btnExit;
        private Telerik.WinControls.UI.RadDropDownList ddlLabel;
        private Telerik.WinControls.UI.RadPanel panelSearchBy;
        private Telerik.WinControls.UI.RadRadioButton radioSeatchByLabel;
        private Telerik.WinControls.UI.RadRadioButton radioSearchByArr;
        private Telerik.WinControls.UI.RadButton btnDo;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Telerik.WinControls.UI.RadGridView rgvInvoice;
        private Telerik.WinControls.UI.RadPanel panelSendBy;
        private Telerik.WinControls.UI.RadRadioButton radioByPost;
        private Telerik.WinControls.UI.RadButton btnSendByEmail;
        private Telerik.WinControls.UI.RadRadioButton radioByEmail;
        private Telerik.WinControls.UI.RadButton btnSendByPost;
        private Telerik.WinControls.UI.RadMenu radMenu1;
        private Telerik.WinControls.UI.RadMenuItem radMenuButtonSaveLayout;
    }
}
