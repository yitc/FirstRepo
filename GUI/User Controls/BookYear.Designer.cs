namespace GUI
{
    partial class BookYear
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
            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem3 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem4 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem5 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem6 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem7 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem8 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem9 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem10 = new Telerik.WinControls.UI.RadListDataItem();
            this.ddlBook = new Telerik.WinControls.UI.RadDropDownList();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.lblBook = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.ddlBook)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBook)).BeginInit();
            this.SuspendLayout();
            // 
            // ddlBook
            // 
            this.ddlBook.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ddlBook.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            radListDataItem1.Text = "2015";
            radListDataItem2.Text = "2016";
            radListDataItem3.Text = "2017";
            radListDataItem4.Text = "2018";
            radListDataItem5.Text = "2019";
            radListDataItem6.Text = "2020";
            radListDataItem7.Text = "2021";
            radListDataItem8.Text = "2022";
            radListDataItem9.Text = "2023";
            radListDataItem10.Text = "2024";
            this.ddlBook.Items.Add(radListDataItem1);
            this.ddlBook.Items.Add(radListDataItem2);
            this.ddlBook.Items.Add(radListDataItem3);
            this.ddlBook.Items.Add(radListDataItem4);
            this.ddlBook.Items.Add(radListDataItem5);
            this.ddlBook.Items.Add(radListDataItem6);
            this.ddlBook.Items.Add(radListDataItem7);
            this.ddlBook.Items.Add(radListDataItem8);
            this.ddlBook.Items.Add(radListDataItem9);
            this.ddlBook.Items.Add(radListDataItem10);
            this.ddlBook.Location = new System.Drawing.Point(117, 0);
            this.ddlBook.MaxLength = 4;
            this.ddlBook.Name = "ddlBook";
            this.ddlBook.Size = new System.Drawing.Size(89, 20);
            this.ddlBook.TabIndex = 0;
            this.ddlBook.ThemeName = "Windows8";
            this.ddlBook.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ddlBook_SelectedIndexChanged);
            // 
            // lblBook
            // 
            this.lblBook.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblBook.Location = new System.Drawing.Point(3, 2);
            this.lblBook.Name = "lblBook";
            this.lblBook.Size = new System.Drawing.Size(88, 18);
            this.lblBook.TabIndex = 1;
            this.lblBook.Text = "Booking year";
            this.lblBook.ThemeName = "Windows8";
            // 
            // BookYear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblBook);
            this.Controls.Add(this.ddlBook);
            this.Name = "BookYear";
            this.Size = new System.Drawing.Size(220, 166);
            ((System.ComponentModel.ISupportInitialize)(this.ddlBook)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBook)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Telerik.WinControls.UI.RadLabel lblBook;
        public Telerik.WinControls.UI.RadDropDownList ddlBook;
    }
}
