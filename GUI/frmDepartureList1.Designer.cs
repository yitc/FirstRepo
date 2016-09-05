namespace GUI
{
    partial class frmDepartureList1
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition8 = new Telerik.WinControls.UI.TableViewDefinition();
            this.dtFrom = new Telerik.WinControls.UI.RadDateTimePicker();
            this.rgvResult = new Telerik.WinControls.UI.RadGridView();
            this.btnOK = new Telerik.WinControls.UI.RadButton();
            this.lblDtTo = new Telerik.WinControls.UI.RadLabel();
            this.lblDtFrom = new Telerik.WinControls.UI.RadLabel();
            this.dtTo = new Telerik.WinControls.UI.RadDateTimePicker();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.btnInclusive1 = new Telerik.WinControls.UI.RadRadioButton();
            this.btnExlusive1 = new Telerik.WinControls.UI.RadRadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvResult.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDtTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDtFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInclusive1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExlusive1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // dtFrom
            // 
            this.dtFrom.AutoSize = false;
            this.dtFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFrom.Location = new System.Drawing.Point(107, 32);
            this.dtFrom.Margin = new System.Windows.Forms.Padding(2);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(103, 20);
            this.dtFrom.TabIndex = 327;
            this.dtFrom.TabStop = false;
            this.dtFrom.ThemeName = "Windows8";
            this.dtFrom.Value = new System.DateTime(((long)(0)));
            // 
            // rgvResult
            // 
            this.rgvResult.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rgvResult.Location = new System.Drawing.Point(12, 277);
            // 
            // 
            // 
            this.rgvResult.MasterTemplate.ViewDefinition = tableViewDefinition8;
            this.rgvResult.Name = "rgvResult";
            this.rgvResult.Size = new System.Drawing.Size(479, 210);
            this.rgvResult.TabIndex = 333;
            this.rgvResult.Text = "radGridView1";
            this.rgvResult.ThemeName = "Windows8";
            this.rgvResult.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnOK.Location = new System.Drawing.Point(339, 130);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(110, 24);
            this.btnOK.TabIndex = 331;
            this.btnOK.Text = "0K";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblDtTo
            // 
            this.lblDtTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDtTo.Location = new System.Drawing.Point(272, 32);
            this.lblDtTo.Name = "lblDtTo";
            this.lblDtTo.Size = new System.Drawing.Size(52, 18);
            this.lblDtTo.TabIndex = 329;
            this.lblDtTo.Text = "Date to";
            // 
            // lblDtFrom
            // 
            this.lblDtFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDtFrom.Location = new System.Drawing.Point(15, 32);
            this.lblDtFrom.Name = "lblDtFrom";
            this.lblDtFrom.Size = new System.Drawing.Size(69, 18);
            this.lblDtFrom.TabIndex = 328;
            this.lblDtFrom.Text = "Date from";
            // 
            // dtTo
            // 
            this.dtTo.AutoSize = false;
            this.dtTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTo.Location = new System.Drawing.Point(349, 32);
            this.dtTo.Margin = new System.Windows.Forms.Padding(2);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(103, 20);
            this.dtTo.TabIndex = 338;
            this.dtTo.TabStop = false;
            this.dtTo.ThemeName = "Windows8";
            this.dtTo.Value = new System.DateTime(((long)(0)));
            // 
            // btnInclusive1
            // 
            this.btnInclusive1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnInclusive1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnInclusive1.Location = new System.Drawing.Point(15, 84);
            this.btnInclusive1.Name = "btnInclusive1";
            this.btnInclusive1.Size = new System.Drawing.Size(155, 18);
            this.btnInclusive1.TabIndex = 356;
            this.btnInclusive1.Text = "Inclusive status Optie";
            this.btnInclusive1.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // btnExlusive1
            // 
            this.btnExlusive1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnExlusive1.Location = new System.Drawing.Point(15, 108);
            this.btnExlusive1.Name = "btnExlusive1";
            this.btnExlusive1.Size = new System.Drawing.Size(157, 18);
            this.btnExlusive1.TabIndex = 357;
            this.btnExlusive1.TabStop = false;
            this.btnExlusive1.Text = "Exclusive status Optie";
            // 
            // frmDepartureList1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 177);
            this.Controls.Add(this.btnInclusive1);
            this.Controls.Add(this.btnExlusive1);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.rgvResult);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblDtTo);
            this.Controls.Add(this.lblDtFrom);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "frmDepartureList1";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DepartureList1";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmDepartureList1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvResult.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgvResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDtTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDtFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInclusive1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExlusive1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadDateTimePicker dtFrom;
        private Telerik.WinControls.UI.RadGridView rgvResult;
        private Telerik.WinControls.UI.RadButton btnOK;
        private Telerik.WinControls.UI.RadLabel lblDtTo;
        private Telerik.WinControls.UI.RadLabel lblDtFrom;
        private Telerik.WinControls.UI.RadDateTimePicker dtTo;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Telerik.WinControls.UI.RadRadioButton btnInclusive1;
        private Telerik.WinControls.UI.RadRadioButton btnExlusive1;
    }
}
