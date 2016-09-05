namespace GUI
{
    partial class frmPaths
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
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.txtPath = new Telerik.WinControls.UI.RadTextBox();
            this.lblPath = new Telerik.WinControls.UI.RadLabel();
            this.btnClose = new Telerik.WinControls.UI.RadButton();
            this.panelPaths = new System.Windows.Forms.Panel();
            this.btnGetPath = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGetPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSave.Location = new System.Drawing.Point(16, 239);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(127, 24);
            this.btnSave.TabIndex = 59;
            this.btnSave.Text = "Save";
            this.btnSave.ThemeName = "Windows8";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(84, 168);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(412, 20);
            this.txtPath.TabIndex = 63;
            // 
            // lblPath
            // 
            this.lblPath.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPath.Location = new System.Drawing.Point(12, 169);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(44, 18);
            this.lblPath.TabIndex = 64;
            this.lblPath.Text = "Path :";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnClose.Location = new System.Drawing.Point(446, 239);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(127, 24);
            this.btnClose.TabIndex = 65;
            this.btnClose.Text = "Close";
            this.btnClose.ThemeName = "Windows8";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelPaths
            // 
            this.panelPaths.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelPaths.Location = new System.Drawing.Point(22, 11);
            this.panelPaths.Margin = new System.Windows.Forms.Padding(2);
            this.panelPaths.Name = "panelPaths";
            this.panelPaths.Size = new System.Drawing.Size(152, 129);
            this.panelPaths.TabIndex = 66;
            // 
            // btnGetPath
            // 
            this.btnGetPath.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnGetPath.Location = new System.Drawing.Point(502, 169);
            this.btnGetPath.Name = "btnGetPath";
            this.btnGetPath.Size = new System.Drawing.Size(34, 21);
            this.btnGetPath.TabIndex = 67;
            this.btnGetPath.Text = "...";
            this.btnGetPath.ThemeName = "Windows8";
            this.btnGetPath.Click += new System.EventHandler(this.btnGetPath_Click);
            // 
            // frmPaths
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 366);
            this.Controls.Add(this.btnGetPath);
            this.Controls.Add(this.panelPaths);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnSave);
            this.Name = "frmPaths";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Paths";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmParths_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGetPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadTextBox txtPath;
        private Telerik.WinControls.UI.RadLabel lblPath;
        private Telerik.WinControls.UI.RadButton btnClose;
        private System.Windows.Forms.Panel panelPaths;
        private Telerik.WinControls.UI.RadButton btnGetPath;
    }
}
