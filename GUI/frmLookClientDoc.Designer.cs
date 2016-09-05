namespace GUI
{
    partial class frmLookClientDoc
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
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.txtClient = new Telerik.WinControls.UI.RadTextBox();
            this.gridDocuments = new Telerik.WinControls.UI.RadGridView();
            this.btnNew = new Telerik.WinControls.UI.RadButton();
            this.btnGet = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDocuments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDocuments.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.radLabel1.Location = new System.Drawing.Point(15, 24);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(39, 17);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "Client";
            // 
            // txtClient
            // 
            this.txtClient.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtClient.Location = new System.Drawing.Point(116, 22);
            this.txtClient.Name = "txtClient";
            this.txtClient.ReadOnly = true;
            this.txtClient.Size = new System.Drawing.Size(500, 19);
            this.txtClient.TabIndex = 1;
            this.txtClient.ThemeName = "Windows8";
            // 
            // gridDocuments
            // 
            this.gridDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridDocuments.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gridDocuments.Location = new System.Drawing.Point(15, 79);
            // 
            // 
            // 
            this.gridDocuments.MasterTemplate.AllowAddNewRow = false;
            this.gridDocuments.MasterTemplate.AllowDeleteRow = false;
            this.gridDocuments.MasterTemplate.AllowSearchRow = true;
            this.gridDocuments.MasterTemplate.EnableFiltering = true;
            this.gridDocuments.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridDocuments.Name = "gridDocuments";
            this.gridDocuments.Size = new System.Drawing.Size(991, 312);
            this.gridDocuments.TabIndex = 2;
            this.gridDocuments.ThemeName = "VisualStudio2012Light";
            this.gridDocuments.ValueChanged += new System.EventHandler(this.gridDocuments_ValueChanged);
            this.gridDocuments.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridDocuments_CurrentRowChanged);
            this.gridDocuments.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridDocuments_CellClick);
            this.gridDocuments.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridDocuments_CellDoubleClick);
            this.gridDocuments.ContextMenuOpening += new Telerik.WinControls.UI.ContextMenuOpeningEventHandler(this.gridDocuments_ContextMenuOpening);
            this.gridDocuments.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridDocuments_DataBindingComplete);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNew.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnNew.Location = new System.Drawing.Point(173, 416);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(110, 24);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "New document";
            this.btnNew.ThemeName = "Windows8";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnGet
            // 
            this.btnGet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGet.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnGet.Location = new System.Drawing.Point(734, 416);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(110, 24);
            this.btnGet.TabIndex = 4;
            this.btnGet.Text = "Get document";
            this.btnGet.ThemeName = "Windows8";
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // frmLookClientDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 458);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.gridDocuments);
            this.Controls.Add(this.txtClient);
            this.Controls.Add(this.radLabel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Name = "frmLookClientDoc";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Client documents";
            this.ThemeName = "Windows8";
            this.Load += new System.EventHandler(this.frmLookClientDoc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDocuments.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDocuments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadTextBox txtClient;
        private Telerik.WinControls.UI.RadGridView gridDocuments;
        private Telerik.WinControls.UI.RadButton btnNew;
        private Telerik.WinControls.UI.RadButton btnGet;
    }
}
