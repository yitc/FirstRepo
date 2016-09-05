namespace GUI
{
    partial class frmCLoseTax
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
            this.rdpDateClose = new Telerik.WinControls.UI.RadDateTimePicker();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.rdpDateClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // rdpDateClose
            // 
            this.rdpDateClose.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rdpDateClose.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.rdpDateClose.Location = new System.Drawing.Point(59, 23);
            this.rdpDateClose.Name = "rdpDateClose";
            this.rdpDateClose.Size = new System.Drawing.Size(110, 20);
            this.rdpDateClose.TabIndex = 0;
            this.rdpDateClose.TabStop = false;
            this.rdpDateClose.Text = "2-8-2015";
            this.rdpDateClose.ThemeName = "Windows8";
            this.rdpDateClose.Value = new System.DateTime(2015, 8, 2, 8, 55, 24, 562);
            // 
            // radButton1
            // 
            this.radButton1.Location = new System.Drawing.Point(59, 71);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(110, 24);
            this.radButton1.TabIndex = 1;
            this.radButton1.Text = "Ok";
            this.radButton1.ThemeName = "Windows8";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // frmCLoseTax
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 115);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.rdpDateClose);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCLoseTax";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmCLoseTax_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rdpDateClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadDateTimePicker rdpDateClose;
        private Telerik.WinControls.UI.RadButton radButton1;
    }
}
