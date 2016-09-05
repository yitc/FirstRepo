namespace GUI
{
    partial class ClientDetailView
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
            this.radSplitContainer1 = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.lblContactPersonName = new Telerik.WinControls.UI.RadLabel();
            this.lblCompanyName = new Telerik.WinControls.UI.RadLabel();
            this.radListControl1 = new Telerik.WinControls.UI.RadListControl();
            this.lblContactPerson = new Telerik.WinControls.UI.RadLabel();
            this.pictureLetter = new System.Windows.Forms.PictureBox();
            this.pictureEmail = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).BeginInit();
            this.radSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblContactPersonName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCompanyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblContactPerson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLetter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEmail)).BeginInit();
            this.SuspendLayout();
            // 
            // radSplitContainer1
            // 
            this.radSplitContainer1.Controls.Add(this.splitPanel1);
            this.radSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.radSplitContainer1.Name = "radSplitContainer1";
            this.radSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.radSplitContainer1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.radSplitContainer1.Size = new System.Drawing.Size(327, 409);
            this.radSplitContainer1.TabIndex = 0;
            this.radSplitContainer1.TabStop = false;
            this.radSplitContainer1.Text = "radSplitContainer1";
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radSplitContainer1.GetChildAt(0).GetChildAt(1))).AutoSize = false;
            // 
            // splitPanel1
            // 
            this.splitPanel1.Controls.Add(this.lblContactPersonName);
            this.splitPanel1.Controls.Add(this.pictureLetter);
            this.splitPanel1.Controls.Add(this.pictureEmail);
            this.splitPanel1.Controls.Add(this.lblCompanyName);
            this.splitPanel1.Controls.Add(this.radListControl1);
            this.splitPanel1.Controls.Add(this.lblContactPerson);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            this.splitPanel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // 
            // 
            this.splitPanel1.RootElement.AutoSize = false;
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel1.Size = new System.Drawing.Size(327, 409);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.3697917F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 100);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            ((Telerik.WinControls.UI.SplitPanelElement)(this.splitPanel1.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(5);
            ((Telerik.WinControls.UI.SplitPanelElement)(this.splitPanel1.GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.splitPanel1.GetChildAt(0).GetChildAt(0))).AutoSize = false;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.splitPanel1.GetChildAt(0).GetChildAt(1))).AutoSize = false;
            // 
            // lblContactPersonName
            // 
            this.lblContactPersonName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContactPersonName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblContactPersonName.Location = new System.Drawing.Point(73, 40);
            this.lblContactPersonName.Name = "lblContactPersonName";
            this.lblContactPersonName.Size = new System.Drawing.Size(45, 18);
            this.lblContactPersonName.TabIndex = 6;
            this.lblContactPersonName.Text = "name";
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCompanyName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblCompanyName.Location = new System.Drawing.Point(9, 9);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(128, 18);
            this.lblCompanyName.TabIndex = 4;
            this.lblCompanyName.Text = "lblCompanyName";
            // 
            // radListControl1
            // 
            this.radListControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radListControl1.AutoScroll = true;
            this.radListControl1.BackColor = System.Drawing.SystemColors.Control;
            this.radListControl1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.radListControl1.Location = new System.Drawing.Point(9, 64);
            this.radListControl1.Name = "radListControl1";
            this.radListControl1.Size = new System.Drawing.Size(295, 220);
            this.radListControl1.TabIndex = 3;
            this.radListControl1.Text = "radListControl1";
            ((Telerik.WinControls.UI.RadListElement)(this.radListControl1.GetChildAt(0))).BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            ((Telerik.WinControls.UI.RadListElement)(this.radListControl1.GetChildAt(0))).BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            ((Telerik.WinControls.UI.RadListElement)(this.radListControl1.GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            // 
            // lblContactPerson
            // 
            this.lblContactPerson.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContactPerson.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblContactPerson.Location = new System.Drawing.Point(7, 40);
            this.lblContactPerson.Name = "lblContactPerson";
            this.lblContactPerson.Size = new System.Drawing.Size(60, 18);
            this.lblContactPerson.TabIndex = 5;
            this.lblContactPerson.Text = "Contact:";
            // 
            // pictureLetter
            // 
            this.pictureLetter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureLetter.Image = global::GUI.Properties.Resources.Word_add;
            this.pictureLetter.Location = new System.Drawing.Point(47, 357);
            this.pictureLetter.Name = "pictureLetter";
            this.pictureLetter.Size = new System.Drawing.Size(32, 32);
            this.pictureLetter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureLetter.TabIndex = 7;
            this.pictureLetter.TabStop = false;
            this.pictureLetter.Click += new System.EventHandler(this.pictureLetter_Click);
            // 
            // pictureEmail
            // 
            this.pictureEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureEmail.Image = global::GUI.Properties.Resources.Email;
            this.pictureEmail.Location = new System.Drawing.Point(9, 357);
            this.pictureEmail.Name = "pictureEmail";
            this.pictureEmail.Size = new System.Drawing.Size(32, 32);
            this.pictureEmail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureEmail.TabIndex = 6;
            this.pictureEmail.TabStop = false;
            this.pictureEmail.Click += new System.EventHandler(this.pictureEmail_Click);
            // 
            // ClientDetailView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radSplitContainer1);
            this.Name = "ClientDetailView";
            this.Size = new System.Drawing.Size(327, 409);
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).EndInit();
            this.radSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            this.splitPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblContactPersonName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCompanyName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblContactPerson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLetter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEmail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadSplitContainer radSplitContainer1;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        public Telerik.WinControls.UI.RadListControl radListControl1;
        private Telerik.WinControls.UI.RadLabel lblContactPerson;
        private Telerik.WinControls.UI.RadLabel lblCompanyName;
        private System.Windows.Forms.PictureBox pictureLetter;
        private System.Windows.Forms.PictureBox pictureEmail;
        private Telerik.WinControls.UI.RadLabel lblContactPersonName;
    }
}
