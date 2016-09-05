namespace GUI
{
    partial class ChangePassword
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
            this.splitContainerForm = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.txtNewPass = new Telerik.WinControls.UI.RadTextBox();
            this.lblNewPass = new Telerik.WinControls.UI.RadLabel();
            this.object_750225c9_a2b0_44e6_a205_97ba9c5a8192 = new Telerik.WinControls.RootRadElement();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerForm)).BeginInit();
            this.splitContainerForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            this.radLabel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNewPass)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerForm
            // 
            this.splitContainerForm.Controls.Add(this.splitPanel1);
            this.splitContainerForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerForm.Location = new System.Drawing.Point(0, 0);
            this.splitContainerForm.Name = "splitContainerForm";
            this.splitContainerForm.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.splitContainerForm.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitContainerForm.Size = new System.Drawing.Size(536, 230);
            this.splitContainerForm.SplitterWidth = 0;
            this.splitContainerForm.TabIndex = 0;
            this.splitContainerForm.TabStop = false;
            this.splitContainerForm.Text = "radSplitContainer1";
            this.splitContainerForm.ThemeName = "VisualStudio2012Light";
            // 
            // splitPanel1
            // 
            this.splitPanel1.BackColor = System.Drawing.Color.Transparent;
            this.splitPanel1.Controls.Add(this.btnSave);
            this.splitPanel1.Controls.Add(this.btnCancel);
            this.splitPanel1.Controls.Add(this.radLabel1);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(0, 0);
            this.splitPanel1.Size = new System.Drawing.Size(536, 230);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.2237762F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 64);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            this.splitPanel1.ThemeName = "VisualStudio2012Light";
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.splitPanel1.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.Transparent;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(31, 146);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(135, 43);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.ThemeName = "VisualStudio2012Light";
            this.btnSave.Click += new System.EventHandler(this.radButtonSave_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Text = "Save";
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).ForeColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(127)))), ((int)(((byte)(180)))));
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(366, 147);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(135, 43);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.ThemeName = "VisualStudio2012Light";
            this.btnCancel.Click += new System.EventHandler(this.radButtonCancel_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCancel.GetChildAt(0))).Text = "Cancel";
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCancel.GetChildAt(0))).ForeColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCancel.GetChildAt(0))).Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.btnCancel.GetChildAt(0).GetChildAt(0))).ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.btnCancel.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(127)))), ((int)(((byte)(180)))));
            // 
            // radLabel1
            // 
            this.radLabel1.AutoSize = false;
            this.radLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.radLabel1.Controls.Add(this.txtNewPass);
            this.radLabel1.Controls.Add(this.lblNewPass);
            this.radLabel1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radLabel1.Location = new System.Drawing.Point(31, 57);
            this.radLabel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(470, 49);
            this.radLabel1.TabIndex = 1;
            this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNewPass
            // 
            this.txtNewPass.AutoSize = false;
            this.txtNewPass.BackColor = System.Drawing.Color.White;
            this.txtNewPass.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewPass.Location = new System.Drawing.Point(123, 3);
            this.txtNewPass.Name = "txtNewPass";
            this.txtNewPass.Size = new System.Drawing.Size(389, 43);
            this.txtNewPass.TabIndex = 2;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtNewPass.GetChildAt(0).GetChildAt(0))).NullText = "";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtNewPass.GetChildAt(0).GetChildAt(0))).NullTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtNewPass.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtNewPass.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.txtNewPass.GetChildAt(0).GetChildAt(1))).BackColor4 = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.txtNewPass.GetChildAt(0).GetChildAt(1))).BackColor = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtNewPass.GetChildAt(0).GetChildAt(2))).BottomColor = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtNewPass.GetChildAt(0).GetChildAt(2))).ForeColor = System.Drawing.Color.White;
            // 
            // lblNewPass
            // 
            this.lblNewPass.AutoSize = false;
            this.lblNewPass.BackColor = System.Drawing.Color.White;
            this.lblNewPass.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblNewPass.ForeColor = System.Drawing.Color.Gray;
            this.lblNewPass.Location = new System.Drawing.Point(2, 3);
            this.lblNewPass.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.lblNewPass.Name = "lblNewPass";
            this.lblNewPass.Size = new System.Drawing.Size(119, 43);
            this.lblNewPass.TabIndex = 0;
            this.lblNewPass.Text = "Enter new password:";
            this.lblNewPass.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // object_750225c9_a2b0_44e6_a205_97ba9c5a8192
            // 
            this.object_750225c9_a2b0_44e6_a205_97ba9c5a8192.Name = "object_750225c9_a2b0_44e6_a205_97ba9c5a8192";
            this.object_750225c9_a2b0_44e6_a205_97ba9c5a8192.StretchHorizontally = true;
            this.object_750225c9_a2b0_44e6_a205_97ba9c5a8192.StretchVertically = true;
            // 
            // ChangePassword
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(169)))), ((int)(((byte)(234)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(536, 230);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainerForm);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangePassword";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change password";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerForm)).EndInit();
            this.splitContainerForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            this.radLabel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNewPass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNewPass)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadSplitContainer splitContainerForm;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.RadLabel lblNewPass;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadTextBox txtNewPass;
        private Telerik.WinControls.RootRadElement object_750225c9_a2b0_44e6_a205_97ba9c5a8192;
    }
}