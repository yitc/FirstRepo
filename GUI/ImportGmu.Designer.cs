namespace GUI
{
    partial class ImportGmu
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.btnClose = new Telerik.WinControls.UI.RadButton();
            this.lblFile = new Telerik.WinControls.UI.RadLabel();
            this.txtPath = new Telerik.WinControls.UI.RadTextBox();
            this.btnFind = new Telerik.WinControls.UI.RadButton();
            this.btnImport = new Telerik.WinControls.UI.RadButton();
            this.txtNumber = new Telerik.WinControls.UI.RadTextBox();
            this.txtAccount = new Telerik.WinControls.UI.RadTextBox();
            this.txtStatement = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.txtAmount = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.txtview = new Telerik.WinControls.UI.RadTextBox();
            this.txtDC = new Telerik.WinControls.UI.RadTextBox();
            this.txtEndsaldo = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.lvaaa = new Telerik.WinControls.UI.RadListView();
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            this.txtTotal = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.radLabel6 = new Telerik.WinControls.UI.RadLabel();
            this.btnReadXml = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndsaldo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lvaaa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReadXml)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(797, 537);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 24);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblFile
            // 
            this.lblFile.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblFile.Location = new System.Drawing.Point(48, 36);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(53, 18);
            this.lblFile.TabIndex = 1;
            this.lblFile.Text = "ME 940";
            // 
            // txtPath
            // 
            this.txtPath.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPath.Location = new System.Drawing.Point(128, 34);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(512, 20);
            this.txtPath.TabIndex = 2;
            this.txtPath.ThemeName = "Windows8";
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(655, 34);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(20, 20);
            this.btnFind.TabIndex = 1;
            this.btnFind.Text = "...";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(797, 32);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(110, 24);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "Import";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // txtNumber
            // 
            this.txtNumber.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumber.Location = new System.Drawing.Point(126, 83);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(115, 20);
            this.txtNumber.TabIndex = 3;
            this.txtNumber.ThemeName = "Windows8";
            // 
            // txtAccount
            // 
            this.txtAccount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAccount.Location = new System.Drawing.Point(316, 83);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(115, 20);
            this.txtAccount.TabIndex = 4;
            this.txtAccount.ThemeName = "Windows8";
            // 
            // txtStatement
            // 
            this.txtStatement.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatement.Location = new System.Drawing.Point(523, 83);
            this.txtStatement.Name = "txtStatement";
            this.txtStatement.Size = new System.Drawing.Size(115, 20);
            this.txtStatement.TabIndex = 5;
            this.txtStatement.ThemeName = "Windows8";
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radLabel1.Location = new System.Drawing.Point(48, 85);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(56, 18);
            this.radLabel1.TabIndex = 2;
            this.radLabel1.Text = "Number";
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radLabel2.Location = new System.Drawing.Point(247, 85);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(64, 18);
            this.radLabel2.TabIndex = 3;
            this.radLabel2.Text = "Rekening";
            // 
            // radLabel3
            // 
            this.radLabel3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radLabel3.Location = new System.Drawing.Point(441, 85);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(72, 18);
            this.radLabel3.TabIndex = 4;
            this.radLabel3.Text = "Statement";
            // 
            // radLabel4
            // 
            this.radLabel4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radLabel4.Location = new System.Drawing.Point(655, 85);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(30, 18);
            this.radLabel4.TabIndex = 5;
            this.radLabel4.Text = "D/C";
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtAmount.Location = new System.Drawing.Point(733, 83);
            this.txtAmount.Mask = "n2";
            this.txtAmount.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(133, 20);
            this.txtAmount.TabIndex = 6;
            this.txtAmount.TabStop = false;
            this.txtAmount.Text = "0,00";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.ThemeName = "Windows8";
            // 
            // txtview
            // 
            this.txtview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtview.AutoScroll = true;
            this.txtview.AutoSize = false;
            this.txtview.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtview.Location = new System.Drawing.Point(48, 147);
            this.txtview.Multiline = true;
            this.txtview.Name = "txtview";
            this.txtview.ReadOnly = true;
            this.txtview.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtview.Size = new System.Drawing.Size(144, 17);
            this.txtview.TabIndex = 7;
            this.txtview.Visible = false;
            // 
            // txtDC
            // 
            this.txtDC.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDC.Location = new System.Drawing.Point(691, 83);
            this.txtDC.Name = "txtDC";
            this.txtDC.Size = new System.Drawing.Size(31, 20);
            this.txtDC.TabIndex = 6;
            this.txtDC.ThemeName = "Windows8";
            // 
            // txtEndsaldo
            // 
            this.txtEndsaldo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtEndsaldo.Location = new System.Drawing.Point(733, 497);
            this.txtEndsaldo.Mask = "n2";
            this.txtEndsaldo.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.txtEndsaldo.Name = "txtEndsaldo";
            this.txtEndsaldo.Size = new System.Drawing.Size(133, 20);
            this.txtEndsaldo.TabIndex = 8;
            this.txtEndsaldo.TabStop = false;
            this.txtEndsaldo.Text = "0,00";
            this.txtEndsaldo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEndsaldo.ThemeName = "Windows8";
            // 
            // radLabel5
            // 
            this.radLabel5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radLabel5.Location = new System.Drawing.Point(646, 499);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(67, 18);
            this.radLabel5.TabIndex = 5;
            this.radLabel5.Text = "End saldo";
            // 
            // lvaaa
            // 
            this.lvaaa.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lvaaa.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.lvaaa.ItemSize = new System.Drawing.Size(400, 20);
            this.lvaaa.Location = new System.Drawing.Point(22, 170);
            this.lvaaa.Name = "lvaaa";
            this.lvaaa.Size = new System.Drawing.Size(1008, 30);
            this.lvaaa.TabIndex = 9;
            this.lvaaa.Visible = false;
            // 
            // radGridView1
            // 
            this.radGridView1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radGridView1.Location = new System.Drawing.Point(20, 113);
            // 
            // 
            // 
            this.radGridView1.MasterTemplate.AllowAddNewRow = false;
            this.radGridView1.MasterTemplate.AllowEditRow = false;
            this.radGridView1.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.Size = new System.Drawing.Size(1007, 374);
            this.radGridView1.TabIndex = 10;
            this.radGridView1.ThemeName = "VisualStudio2012Light";
            this.radGridView1.ContextMenuOpening += new Telerik.WinControls.UI.ContextMenuOpeningEventHandler(this.radGridView1_ContextMenuOpening);
            this.radGridView1.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.radGridView1_DataBindingComplete);
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtTotal.Location = new System.Drawing.Point(421, 498);
            this.txtTotal.Mask = "n2";
            this.txtTotal.MaskType = Telerik.WinControls.UI.MaskType.Numeric;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(143, 20);
            this.txtTotal.TabIndex = 11;
            this.txtTotal.TabStop = false;
            this.txtTotal.Text = "0,00";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotal.ThemeName = "Windows8";
            // 
            // radLabel6
            // 
            this.radLabel6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radLabel6.Location = new System.Drawing.Point(263, 500);
            this.radLabel6.Name = "radLabel6";
            this.radLabel6.Size = new System.Drawing.Size(104, 18);
            this.radLabel6.TabIndex = 5;
            this.radLabel6.Text = "Statement total";
            // 
            // btnReadXml
            // 
            this.btnReadXml.Location = new System.Drawing.Point(82, 547);
            this.btnReadXml.Name = "btnReadXml";
            this.btnReadXml.Size = new System.Drawing.Size(110, 24);
            this.btnReadXml.TabIndex = 1;
            this.btnReadXml.Text = "ReadXML";
            this.btnReadXml.Click += new System.EventHandler(this.btnReadXml_Click);
            // 
            // ImportGmu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 583);
            this.Controls.Add(this.btnReadXml);
            this.Controls.Add(this.radLabel6);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.radGridView1);
            this.Controls.Add(this.lvaaa);
            this.Controls.Add(this.radLabel5);
            this.Controls.Add(this.txtEndsaldo);
            this.Controls.Add(this.txtDC);
            this.Controls.Add(this.txtview);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.radLabel4);
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.txtStatement);
            this.Controls.Add(this.txtAccount);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.btnClose);
            this.Name = "ImportGmu";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Gmu";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.ImportGmu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndsaldo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lvaaa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReadXml)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnClose;
        private Telerik.WinControls.UI.RadLabel lblFile;
        private Telerik.WinControls.UI.RadTextBox txtPath;
        private Telerik.WinControls.UI.RadButton btnFind;
        private Telerik.WinControls.UI.RadButton btnImport;
        private Telerik.WinControls.UI.RadTextBox txtNumber;
        private Telerik.WinControls.UI.RadTextBox txtAccount;
        private Telerik.WinControls.UI.RadTextBox txtStatement;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadMaskedEditBox txtAmount;
        private Telerik.WinControls.UI.RadTextBox txtview;
        private Telerik.WinControls.UI.RadTextBox txtDC;
        private Telerik.WinControls.UI.RadMaskedEditBox txtEndsaldo;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadListView lvaaa;
        private Telerik.WinControls.UI.RadGridView radGridView1;
        private Telerik.WinControls.UI.RadMaskedEditBox txtTotal;
        private Telerik.WinControls.UI.RadLabel radLabel6;
        private Telerik.WinControls.UI.RadButton btnReadXml;
    }
}
