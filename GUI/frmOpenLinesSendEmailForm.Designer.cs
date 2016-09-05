namespace GUI
{
    partial class frmOpenLinesSendEmailForm
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
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.listRecipients = new Telerik.WinControls.UI.RadListView();
            this.txtSubject = new Telerik.WinControls.UI.RadTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMessage = new Telerik.WinControls.UI.RadTextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listRecipients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radButton1
            // 
            this.radButton1.Font = new System.Drawing.Font("Verdana", 9F);
            this.radButton1.Location = new System.Drawing.Point(431, 409);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(110, 24);
            this.radButton1.TabIndex = 0;
            this.radButton1.Text = "Send To All";
            this.radButton1.ThemeName = "Windows8";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // listRecipients
            // 
            this.listRecipients.Location = new System.Drawing.Point(12, 31);
            this.listRecipients.Name = "listRecipients";
            this.listRecipients.Size = new System.Drawing.Size(207, 355);
            this.listRecipients.TabIndex = 1;
            this.listRecipients.Text = "radListView1";
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(236, 31);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(305, 20);
            this.txtSubject.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(233, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "Subject:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(233, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "Message:";
            // 
            // txtMessage
            // 
            this.txtMessage.AcceptsReturn = true;
            this.txtMessage.AcceptsTab = true;
            this.txtMessage.AutoSize = false;
            this.txtMessage.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtMessage.Location = new System.Drawing.Point(236, 87);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(305, 299);
            this.txtMessage.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "Recipients:";
            // 
            // frmOpenLinesSendEmailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 445);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.listRecipients);
            this.Controls.Add(this.radButton1);
            this.Font = new System.Drawing.Font("Verdana", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOpenLinesSendEmailForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Send Email";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmOpenLinesSendEmailForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listRecipients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadListView listRecipients;
        private Telerik.WinControls.UI.RadTextBox txtSubject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.RadTextBox txtMessage;
        private System.Windows.Forms.Label label3;
    }
}
