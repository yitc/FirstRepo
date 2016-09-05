namespace GUI
{
    partial class MessageBoxCalendar
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
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.radDateTimePicker1 = new Telerik.WinControls.UI.RadDateTimePicker();
            this.lblMessage = new Telerik.WinControls.UI.RadLabel();
            this.btnYes = new Telerik.WinControls.UI.RadButton();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.btnNo = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radDateTimePicker1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnYes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radDateTimePicker1
            // 
            this.radDateTimePicker1.CustomFormat = "";
            this.radDateTimePicker1.Font = new System.Drawing.Font("Verdana", 9F);
            this.radDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.radDateTimePicker1.Location = new System.Drawing.Point(33, 36);
            this.radDateTimePicker1.Name = "radDateTimePicker1";
            this.radDateTimePicker1.Size = new System.Drawing.Size(280, 20);
            this.radDateTimePicker1.TabIndex = 0;
            this.radDateTimePicker1.TabStop = false;
            this.radDateTimePicker1.Text = "1/28/2016";
            this.radDateTimePicker1.ThemeName = "Windows8";
            this.radDateTimePicker1.Value = new System.DateTime(2016, 1, 28, 20, 30, 33, 163);
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblMessage.Location = new System.Drawing.Point(33, 12);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(280, 18);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "Are you sure you want cancel this traveller?";
            // 
            // btnYes
            // 
            this.btnYes.Font = new System.Drawing.Font("Verdana", 9F);
            this.btnYes.Location = new System.Drawing.Point(33, 75);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(110, 24);
            this.btnYes.TabIndex = 2;
            this.btnYes.Text = "Yes";
            this.btnYes.ThemeName = "VisualStudio2012Light";
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.Font = new System.Drawing.Font("Verdana", 9F);
            this.btnNo.Location = new System.Drawing.Point(203, 75);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(110, 24);
            this.btnNo.TabIndex = 3;
            this.btnNo.Text = "No";
            this.btnNo.ThemeName = "VisualStudio2012Light";
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // MessageBoxCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 114);
            this.ControlBox = false;
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.radDateTimePicker1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageBoxCalendar";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cancel traveler";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.MessageBoxCalendar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radDateTimePicker1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnYes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Telerik.WinControls.UI.RadDateTimePicker radDateTimePicker1;
        private Telerik.WinControls.UI.RadLabel lblMessage;
        private Telerik.WinControls.UI.RadButton btnYes;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private Telerik.WinControls.UI.RadButton btnNo;
    }
}