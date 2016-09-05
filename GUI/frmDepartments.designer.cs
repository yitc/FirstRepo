namespace GUI
{
    partial class frmDepartments
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
            this.txtIdDepartments = new Telerik.WinControls.UI.RadTextBox();
            this.lblDepartmentsId = new Telerik.WinControls.UI.RadLabel();
            this.txtDepartments = new Telerik.WinControls.UI.RadTextBox();
            this.lblDepartments = new Telerik.WinControls.UI.RadLabel();
            this.txtTelephone = new Telerik.WinControls.UI.RadTextBox();
            this.lblTelephone = new Telerik.WinControls.UI.RadLabel();
            this.txtEmail = new Telerik.WinControls.UI.RadTextBox();
            this.lblEmail = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.txtIdDepartments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepartmentsId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepartments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelephone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTelephone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ribbonTab1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ribbonTab1.BorderGradientStyle = Telerik.WinControls.GradientStyles.Solid;
            this.ribbonTab1.Bounds = new System.Drawing.Rectangle(0, 0, 52, 28);
            this.ribbonTab1.ClipDrawing = true;
            this.ribbonTab1.ClipText = true;
            this.ribbonTab1.DrawBorder = true;
            this.ribbonTab1.DrawFill = true;
            this.ribbonTab1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbonTab1.ForeColor = System.Drawing.Color.Black;
            this.ribbonTab1.GradientStyle = Telerik.WinControls.GradientStyles.Solid;
            this.ribbonTab1.ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.ribbonTab1.ImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ribbonTab1.MinSize = new System.Drawing.Size(8, 8);
            this.ribbonTab1.NumberOfColors = 1;
            this.ribbonTab1.Padding = new System.Windows.Forms.Padding(4);
            this.ribbonTab1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.ribbonTab1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.CanFocus = true;
            this.btnSave.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.Transparent;
            this.btnReport.CanFocus = true;
            this.btnReport.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radRibbonDocuments
            // 
            this.radRibbonDocuments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.radRibbonDocuments.Bounds = new System.Drawing.Rectangle(0, 0, 136, 99);
            this.radRibbonDocuments.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radRibbonDocuments.ForeColor = System.Drawing.Color.Transparent;
            this.radRibbonDocuments.Margin = new System.Windows.Forms.Padding(2);
            this.radRibbonDocuments.MaxSize = new System.Drawing.Size(0, 100);
            this.radRibbonDocuments.MinSize = new System.Drawing.Size(20, 86);
            // 
            // btnNewDoc
            // 
            this.btnNewDoc.BackColor = System.Drawing.Color.Transparent;
            this.btnNewDoc.CanFocus = true;
            this.btnNewDoc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radRibbonMemo
            // 
            this.radRibbonMemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.radRibbonMemo.Bounds = new System.Drawing.Rectangle(0, 0, 136, 99);
            this.radRibbonMemo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radRibbonMemo.ForeColor = System.Drawing.Color.Transparent;
            this.radRibbonMemo.Margin = new System.Windows.Forms.Padding(2);
            this.radRibbonMemo.MaxSize = new System.Drawing.Size(0, 100);
            this.radRibbonMemo.MinSize = new System.Drawing.Size(20, 86);
            // 
            // btnDeleteDoc
            // 
            this.btnDeleteDoc.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteDoc.CanFocus = true;
            this.btnDeleteDoc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnNewMemo
            // 
            this.btnNewMemo.BackColor = System.Drawing.Color.Transparent;
            this.btnNewMemo.CanFocus = true;
            this.btnNewMemo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnDeleteMemo
            // 
            this.btnDeleteMemo.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteMemo.CanFocus = true;
            this.btnDeleteMemo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnWord
            // 
            this.btnWord.BackColor = System.Drawing.Color.Transparent;
            this.btnWord.CanFocus = true;
            this.btnWord.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnEmail
            // 
            this.btnEmail.BackColor = System.Drawing.Color.Transparent;
            this.btnEmail.CanFocus = true;
            this.btnEmail.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radRibbonContact
            // 
            this.radRibbonContact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.radRibbonContact.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radRibbonContact.ForeColor = System.Drawing.Color.Transparent;
            this.radRibbonContact.Margin = new System.Windows.Forms.Padding(2);
            this.radRibbonContact.MaxSize = new System.Drawing.Size(0, 100);
            this.radRibbonContact.MinSize = new System.Drawing.Size(20, 86);
            this.radRibbonContact.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            // 
            // btnNewContact
            // 
            this.btnNewContact.BackColor = System.Drawing.Color.Transparent;
            this.btnNewContact.CanFocus = true;
            this.btnNewContact.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnDelContact
            // 
            this.btnDelContact.BackColor = System.Drawing.Color.Transparent;
            this.btnDelContact.CanFocus = true;
            this.btnDelContact.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radRibbonTask
            // 
            this.radRibbonTask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.radRibbonTask.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radRibbonTask.ForeColor = System.Drawing.Color.Transparent;
            this.radRibbonTask.Margin = new System.Windows.Forms.Padding(2);
            this.radRibbonTask.MaxSize = new System.Drawing.Size(0, 100);
            this.radRibbonTask.MinSize = new System.Drawing.Size(20, 86);
            this.radRibbonTask.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            // 
            // btnNewTask
            // 
            this.btnNewTask.BackColor = System.Drawing.Color.Transparent;
            this.btnNewTask.CanFocus = true;
            this.btnNewTask.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnDelTask
            // 
            this.btnDelTask.BackColor = System.Drawing.Color.Transparent;
            this.btnDelTask.CanFocus = true;
            this.btnDelTask.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnNewMeeting
            // 
            this.btnNewMeeting.BackColor = System.Drawing.Color.Transparent;
            this.btnNewMeeting.CanFocus = true;
            this.btnNewMeeting.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewMeeting.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            // 
            // radRibbonMeeting
            // 
            this.radRibbonMeeting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.radRibbonMeeting.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radRibbonMeeting.ForeColor = System.Drawing.Color.Transparent;
            this.radRibbonMeeting.Margin = new System.Windows.Forms.Padding(2);
            this.radRibbonMeeting.MaxSize = new System.Drawing.Size(0, 100);
            this.radRibbonMeeting.MinSize = new System.Drawing.Size(20, 86);
            this.radRibbonMeeting.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            // 
            // txtIdDepartments
            // 
            this.txtIdDepartments.Enabled = false;
            this.txtIdDepartments.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtIdDepartments.Location = new System.Drawing.Point(132, 170);
            this.txtIdDepartments.Name = "txtIdDepartments";
            this.txtIdDepartments.Size = new System.Drawing.Size(100, 20);
            this.txtIdDepartments.TabIndex = 14;
            this.txtIdDepartments.ThemeName = "Windows8";
            // 
            // lblDepartmentsId
            // 
            this.lblDepartmentsId.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblDepartmentsId.Location = new System.Drawing.Point(5, 171);
            this.lblDepartmentsId.Name = "lblDepartmentsId";
            this.lblDepartmentsId.Size = new System.Drawing.Size(19, 18);
            this.lblDepartmentsId.TabIndex = 15;
            this.lblDepartmentsId.Text = "Id";
            this.lblDepartmentsId.ThemeName = "Windows8";
            // 
            // txtDepartments
            // 
            this.txtDepartments.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtDepartments.Location = new System.Drawing.Point(132, 196);
            this.txtDepartments.Name = "txtDepartments";
            this.txtDepartments.Size = new System.Drawing.Size(178, 20);
            this.txtDepartments.TabIndex = 15;
            this.txtDepartments.ThemeName = "Windows8";
            // 
            // lblDepartments
            // 
            this.lblDepartments.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblDepartments.Location = new System.Drawing.Point(5, 196);
            this.lblDepartments.Name = "lblDepartments";
            this.lblDepartments.Size = new System.Drawing.Size(87, 18);
            this.lblDepartments.TabIndex = 11;
            this.lblDepartments.Text = "Departments";
            this.lblDepartments.ThemeName = "Windows8";
            // 
            // txtTelephone
            // 
            this.txtTelephone.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtTelephone.Location = new System.Drawing.Point(132, 222);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(178, 20);
            this.txtTelephone.TabIndex = 16;
            this.txtTelephone.ThemeName = "Windows8";
            // 
            // lblTelephone
            // 
            this.lblTelephone.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblTelephone.Location = new System.Drawing.Point(5, 220);
            this.lblTelephone.Name = "lblTelephone";
            this.lblTelephone.Size = new System.Drawing.Size(70, 18);
            this.lblTelephone.TabIndex = 12;
            this.lblTelephone.Text = "Telephone";
            this.lblTelephone.ThemeName = "Windows8";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtEmail.Location = new System.Drawing.Point(132, 248);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(178, 20);
            this.txtEmail.TabIndex = 17;
            this.txtEmail.ThemeName = "Windows8";
            // 
            // lblEmail
            // 
            this.lblEmail.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblEmail.Location = new System.Drawing.Point(5, 248);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(53, 18);
            this.lblEmail.TabIndex = 13;
            this.lblEmail.Text = "E - Mail";
            this.lblEmail.ThemeName = "Windows8";
            // 
            // frmDepartments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.ClientSize = new System.Drawing.Size(413, 279);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblTelephone);
            this.Controls.Add(this.txtTelephone);
            this.Controls.Add(this.lblDepartments);
            this.Controls.Add(this.txtDepartments);
            this.Controls.Add(this.lblDepartmentsId);
            this.Controls.Add(this.txtIdDepartments);
            this.Name = "frmDepartments";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Load += new System.EventHandler(this.frmDepartments_Load);
            this.Controls.SetChildIndex(this.txtIdDepartments, 0);
            this.Controls.SetChildIndex(this.lblDepartmentsId, 0);
            this.Controls.SetChildIndex(this.txtDepartments, 0);
            this.Controls.SetChildIndex(this.lblDepartments, 0);
            this.Controls.SetChildIndex(this.txtTelephone, 0);
            this.Controls.SetChildIndex(this.lblTelephone, 0);
            this.Controls.SetChildIndex(this.txtEmail, 0);
            this.Controls.SetChildIndex(this.lblEmail, 0);
            ((System.ComponentModel.ISupportInitialize)(this.txtIdDepartments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepartmentsId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepartments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelephone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTelephone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBox txtIdDepartments;
        private Telerik.WinControls.UI.RadLabel lblDepartmentsId;
        private Telerik.WinControls.UI.RadTextBox txtDepartments;
        private Telerik.WinControls.UI.RadLabel lblDepartments;
        private Telerik.WinControls.UI.RadTextBox txtTelephone;
        private Telerik.WinControls.UI.RadLabel lblTelephone;
        private Telerik.WinControls.UI.RadTextBox txtEmail;
        private Telerik.WinControls.UI.RadLabel lblEmail;
    }
}
