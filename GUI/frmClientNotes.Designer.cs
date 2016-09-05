namespace GUI
{
    partial class frmClientNotes
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
            this.mainPanel = new Telerik.WinControls.UI.RadPanel();
            this.comboTypeNotes = new System.Windows.Forms.ComboBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.txtnoteText = new Telerik.WinControls.UI.RadTextBox();
            this.datdtNoteDate = new Telerik.WinControls.UI.RadTextBox();
            this.lblnoteText = new Telerik.WinControls.UI.RadLabel();
            this.lbldtNoteDate = new Telerik.WinControls.UI.RadLabel();
            this.notePanel = new Telerik.WinControls.UI.RadPanel();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).BeginInit();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnoteText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datdtNoteDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblnoteText)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbldtNoteDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.notePanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonTab1
            // 
            this.ribbonTab1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.ribbonTab1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.ribbonTab1.BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(230)))), ((int)(((byte)(249)))));
            this.ribbonTab1.BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(243)))));
            this.ribbonTab1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.ribbonTab1.BorderGradientStyle = Telerik.WinControls.GradientStyles.Solid;
            this.ribbonTab1.Bounds = new System.Drawing.Rectangle(0, 0, 52, 28);
            this.ribbonTab1.ClipDrawing = true;
            this.ribbonTab1.ClipText = true;
            this.ribbonTab1.DrawBorder = true;
            this.ribbonTab1.DrawFill = true;
            this.ribbonTab1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbonTab1.ForeColor = System.Drawing.Color.Black;
            this.ribbonTab1.GradientPercentage = 0.2F;
            this.ribbonTab1.GradientPercentage2 = 0.2F;
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
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(232)))), ((int)(((byte)(236)))));
            this.btnSave.CanFocus = true;
            this.btnSave.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.Transparent;
            this.btnReport.CanFocus = true;
            this.btnReport.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.Black;
            // 
            // radRibbonDocuments
            // 
            this.radRibbonDocuments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.radRibbonDocuments.Bounds = new System.Drawing.Rectangle(0, 0, 136, 99);
            this.radRibbonDocuments.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radRibbonDocuments.ForeColor = System.Drawing.Color.Black;
            this.radRibbonDocuments.Margin = new System.Windows.Forms.Padding(2);
            this.radRibbonDocuments.MaxSize = new System.Drawing.Size(0, 100);
            this.radRibbonDocuments.MinSize = new System.Drawing.Size(20, 86);
            this.radRibbonDocuments.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            // 
            // btnNewDoc
            // 
            this.btnNewDoc.BackColor = System.Drawing.Color.Transparent;
            this.btnNewDoc.CanFocus = true;
            this.btnNewDoc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewDoc.ForeColor = System.Drawing.Color.Black;
            // 
            // radRibbonMemo
            // 
            this.radRibbonMemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.radRibbonMemo.Bounds = new System.Drawing.Rectangle(0, 0, 136, 99);
            this.radRibbonMemo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radRibbonMemo.ForeColor = System.Drawing.Color.Black;
            this.radRibbonMemo.Margin = new System.Windows.Forms.Padding(2);
            this.radRibbonMemo.MaxSize = new System.Drawing.Size(0, 100);
            this.radRibbonMemo.MinSize = new System.Drawing.Size(20, 86);
            this.radRibbonMemo.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            // 
            // btnDeleteDoc
            // 
            this.btnDeleteDoc.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteDoc.CanFocus = true;
            this.btnDeleteDoc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteDoc.ForeColor = System.Drawing.Color.Black;
            // 
            // btnNewMemo
            // 
            this.btnNewMemo.BackColor = System.Drawing.Color.Transparent;
            this.btnNewMemo.CanFocus = true;
            this.btnNewMemo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewMemo.ForeColor = System.Drawing.Color.Black;
            // 
            // btnDeleteMemo
            // 
            this.btnDeleteMemo.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteMemo.CanFocus = true;
            this.btnDeleteMemo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteMemo.ForeColor = System.Drawing.Color.Black;
            this.btnDeleteMemo.Click += new System.EventHandler(this.btnDeleteMemo_Click);
            // 
            // btnWord
            // 
            this.btnWord.BackColor = System.Drawing.Color.Transparent;
            this.btnWord.CanFocus = true;
            this.btnWord.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWord.ForeColor = System.Drawing.Color.Black;
            // 
            // btnEmail
            // 
            this.btnEmail.BackColor = System.Drawing.Color.Transparent;
            this.btnEmail.CanFocus = true;
            this.btnEmail.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmail.ForeColor = System.Drawing.Color.Black;
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.comboTypeNotes);
            this.mainPanel.Controls.Add(this.radLabel1);
            this.mainPanel.Controls.Add(this.txtnoteText);
            this.mainPanel.Controls.Add(this.datdtNoteDate);
            this.mainPanel.Controls.Add(this.lblnoteText);
            this.mainPanel.Controls.Add(this.lbldtNoteDate);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(1, 164);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(2);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(562, 273);
            this.mainPanel.TabIndex = 10;
            // 
            // comboTypeNotes
            // 
            this.comboTypeNotes.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboTypeNotes.FormattingEnabled = true;
            this.comboTypeNotes.Location = new System.Drawing.Point(129, 6);
            this.comboTypeNotes.Name = "comboTypeNotes";
            this.comboTypeNotes.Size = new System.Drawing.Size(191, 22);
            this.comboTypeNotes.TabIndex = 16;
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(6, 5);
            this.radLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(70, 18);
            this.radLabel1.TabIndex = 13;
            this.radLabel1.Text = "Note Type";
            this.radLabel1.ThemeName = "VisualStudio2012Light";
            // 
            // txtnoteText
            // 
            this.txtnoteText.AutoScroll = true;
            this.txtnoteText.AutoSize = false;
            this.txtnoteText.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnoteText.Location = new System.Drawing.Point(129, 50);
            this.txtnoteText.Margin = new System.Windows.Forms.Padding(2);
            this.txtnoteText.Multiline = true;
            this.txtnoteText.Name = "txtnoteText";
            this.txtnoteText.Size = new System.Drawing.Size(419, 210);
            this.txtnoteText.TabIndex = 13;
            this.txtnoteText.ThemeName = "Windows8";
            // 
            // datdtNoteDate
            // 
            this.datdtNoteDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datdtNoteDate.Location = new System.Drawing.Point(129, 29);
            this.datdtNoteDate.Margin = new System.Windows.Forms.Padding(2);
            this.datdtNoteDate.Name = "datdtNoteDate";
            this.datdtNoteDate.ReadOnly = true;
            this.datdtNoteDate.Size = new System.Drawing.Size(191, 20);
            this.datdtNoteDate.TabIndex = 11;
            this.datdtNoteDate.ThemeName = "Windows8";
            // 
            // lblnoteText
            // 
            this.lblnoteText.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblnoteText.Location = new System.Drawing.Point(6, 51);
            this.lblnoteText.Margin = new System.Windows.Forms.Padding(2);
            this.lblnoteText.Name = "lblnoteText";
            this.lblnoteText.Size = new System.Drawing.Size(64, 18);
            this.lblnoteText.TabIndex = 15;
            this.lblnoteText.Text = "Note text";
            this.lblnoteText.ThemeName = "VisualStudio2012Light";
            // 
            // lbldtNoteDate
            // 
            this.lbldtNoteDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldtNoteDate.Location = new System.Drawing.Point(6, 28);
            this.lbldtNoteDate.Margin = new System.Windows.Forms.Padding(2);
            this.lbldtNoteDate.Name = "lbldtNoteDate";
            this.lbldtNoteDate.Size = new System.Drawing.Size(67, 18);
            this.lbldtNoteDate.TabIndex = 14;
            this.lbldtNoteDate.Text = "Note date";
            this.lbldtNoteDate.ThemeName = "VisualStudio2012Light";
            // 
            // notePanel
            // 
            this.notePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.notePanel.Location = new System.Drawing.Point(1, 164);
            this.notePanel.Name = "notePanel";
            this.notePanel.Size = new System.Drawing.Size(562, 273);
            this.notePanel.TabIndex = 11;
            // 
            // frmClientNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 438);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.notePanel);
            this.Name = "frmClientNotes";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Load += new System.EventHandler(this.frmClientNotes_Load);
            this.Controls.SetChildIndex(this.notePanel, 0);
            this.Controls.SetChildIndex(this.mainPanel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.mainPanel)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtnoteText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.datdtNoteDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblnoteText)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbldtNoteDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.notePanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel mainPanel;
        private Telerik.WinControls.UI.RadTextBox txtnoteText;
        private Telerik.WinControls.UI.RadTextBox datdtNoteDate;
        private Telerik.WinControls.UI.RadLabel lblnoteText;
        private Telerik.WinControls.UI.RadLabel lbldtNoteDate;
        private System.Windows.Forms.ComboBox comboTypeNotes;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadPanel notePanel;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;

    }
}
