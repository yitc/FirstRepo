using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using BIS.Business;
using BIS.Model;
using System.IO;

namespace GUI
{
    public partial class frmSplitOpenLines : Telerik.WinControls.UI.RadForm
    {

        BaseGridEditor _gridEditor;
        private decimal xamount;
        public List<AccCreditLinePayModel> multimodel;
        private string creditor;
        private string invoice;
        public string exit;
        decimal sumpercent = 0;
        decimal sumamount = 0;
        private string layoutSplitOpenLines;

        public frmSplitOpenLines(List<AccCreditLinePayModel> cbm, decimal amt, string accNumber, string Invoice)
        {
            multimodel = new List<AccCreditLinePayModel>();
            multimodel = cbm;
            xamount = amt;
            creditor = accNumber;
            invoice = Invoice;
            InitializeComponent();
        }

        private void frmSplitOpenLines_Load(object sender, EventArgs e)
        {
            this.Icon = Login.iconForm;

            layoutSplitOpenLines = MainForm.gridFiltersFolder + "\\layoutSplitOpenLines.xml";

            setTranslate();
            txtAmount.Text = xamount.ToString();
            gridSplit.DataSource = null;
            gridSplit.DataSource = multimodel;
            gridSplit.Focus();

            
        }

        private void gridSplit_CellBeginEdit(object sender, Telerik.WinControls.UI.GridViewCellCancelEventArgs e)
        {
            if (e.Column.Name == "date")
            {
                _gridEditor = this.gridSplit.ActiveEditor as BaseGridEditor;
                if (_gridEditor != null)
                {

                    RadItem element = _gridEditor.EditorElement as RadItem;
                   // element.KeyDown += new KeyEventHandler(cellEditordate_KeyDown);
                }
            }
            if (e.Column.Name == "percent")
            {

                _gridEditor = this.gridSplit.ActiveEditor as BaseGridEditor;
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    // element.KeyDown += new KeyEventHandler(cellEditorpercent_KeyDown);
                }
            }
            if (e.Column.Name == "amount")
            {

                _gridEditor = this.gridSplit.ActiveEditor as BaseGridEditor;
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                    // element.KeyDown += new KeyEventHandler(cellEditorpercent_KeyDown);
                }
            }

            //}
        }

        private void gridSplit_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.Column.Name == "date")
            {
                if (_gridEditor != null)
                {
                    RadItem element = _gridEditor.EditorElement as RadItem;
                //    element.KeyDown -= cellEditordate_KeyDown;
                }
                _gridEditor = null;
            }
            if (e.Column.Name == "percent")
            {
                if (e.Value != null)
                {

                    decimal per = (decimal)e.Value;
               
                    gridSplit.CurrentRow.Cells["amount"].Value = Math.Abs(Convert.ToDecimal(txtAmount.Value) * per / 100);
                }
               // _gridEditor = null;
            }
            if (e.Column.Name == "amount")
            {
                if (e.Value != null)
                {
                    decimal amt = (decimal)e.Value;
                    if (amt != 0)
                    {
                       gridSplit.CurrentRow.Cells["percent"].Value = Math.Abs(amt / Convert.ToDecimal(txtAmount.Value) * 100);
                        //GridViewRowInfo info = e.Row.ViewInfo.CurrentRow;

                        //info.Cells["difference"].Value = (Convert.ToDecimal(gridBank.CurrentRow.Cells["debit"].Value) - Convert.ToDecimal(gridBank.CurrentRow.Cells["credit"].Value)) - Convert.ToDecimal(gridBank.CurrentRow.Cells["amount"].Value);
                    }
                }
            }
        }
       

        private void gridSplit_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
            if (gridSplit != null)
            {
                if (gridSplit.Columns.Count > 0)
                {
                    for (int i = 0; i < gridSplit.Columns.Count; i++)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (gridSplit.Columns[i].HeaderText != null && resxSet.GetString(gridSplit.Columns[i].HeaderText) != null)
                                gridSplit.Columns[i].HeaderText = resxSet.GetString(gridSplit.Columns[i].HeaderText);
                        }
                    }


                    if (gridSplit.Columns != null && gridSplit.Columns.Count > 0)
                        gridSplit.Columns["date"].FormatString = "{0: dd/MM/yyyy}";
                    GridViewDateTimeColumn column = (GridViewDateTimeColumn)this.gridSplit.Columns["date"];
                    column.Format = DateTimePickerFormat.Short;
                    if (gridSplit.Columns != null && gridSplit.Columns.Count > 0)
                        gridSplit.Columns["amount"].FormatString = "{0:N2}";
                    if (gridSplit.Columns != null && gridSplit.Columns.Count > 0)
                        gridSplit.Columns["percent"].FormatString = "{0:N2}";

                    gridSplit.Columns["amount"].InitializeEditor(_gridEditor);

                }
            }
            if (File.Exists(layoutSplitOpenLines))
            {
                gridSplit.LoadLayout(layoutSplitOpenLines);
            }
        }

        private void gridSplit_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (gridSplit.Columns["amount"].IsCurrent || gridSplit.Columns["percent"].IsCurrent)
            {
                GridSpinEditor spinEditor = this.gridSplit.ActiveEditor as GridSpinEditor;
                ((GridSpinEditorElement)spinEditor.EditorElement).ShowUpDownButtons = false;
            }
        }

        private void gridSplit_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridSummaryCellElement)
            {
                if (!String.IsNullOrEmpty(e.CellElement.Text))
                {
                    e.CellElement.BorderLeftWidth = e.CellElement.BorderRightWidth = 1;
                    e.CellElement.Font = new Font("Verdana", 8, FontStyle.Bold);
                    e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                   
                }
                else
                {
                    e.CellElement.ResetValue(LightVisualElement.BorderLeftWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
                    e.CellElement.ResetValue(LightVisualElement.BorderRightWidthProperty, Telerik.WinControls.ValueResetFlags.Local);
                }
            }
        }

        private void gridSplit_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.gridSplit.CurrentCell != null)
            {
                string aa = this.gridSplit.CurrentCell.ColumnInfo.Name;
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && aa == "amount")
                {
                   // int bb = gridSplit.Rows.Count;
                    gridSplit.CurrentRow = gridSplit.Rows.AddNew();
                    gridSplit.CurrentRow.Cells["date"].BeginEdit();

                    SendKeys.Send("{UP}");
                }
            }
        }
        bool isokvalc = false;
        private void gridSplit_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            exit = "no";
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            exit = "no";
             DialogResult dr = RadMessageBox.Show("Do you want to SAVE payments ?", "Save", MessageBoxButtons.YesNo);
             if (dr == DialogResult.Yes)
             {
                 sumgrid();
                 if (sumpercent == 100)
                 {
                     if (sumamount == Convert.ToDecimal(txtAmount.Text))
                     {
                         multimodel = new List<AccCreditLinePayModel>();
                         AccCreditLinePayModel model = new AccCreditLinePayModel();
                         int m = 0;
                         for (int i = 0; i < gridSplit.RowCount; i++)
                         {
                             model = new AccCreditLinePayModel();
                             model.dtDate = Convert.ToDateTime(gridSplit.Rows[i].Cells["date"].Value);
                             model.percentpay = Convert.ToDecimal(gridSplit.Rows[i].Cells["percent"].Value);
                             model.amount = Convert.ToDecimal(gridSplit.Rows[i].Cells["amount"].Value);
                             multimodel.Add(model);
                         }
                         exit = "yes";
                     }
                 }
                 else
                 {
                     if (sumamount == 0 )
                     {
                         exit = "yes";
                         this.Close(); 
                     }
                     else
                     {
                         using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                         {
                             if (resxSet.GetString("Wrong calculation !!") != null)
                                 RadMessageBox.Show(resxSet.GetString("Wrong calculation !!"));
                             else
                                 RadMessageBox.Show("Wrong calculation !!");
                         }
                         return;
                     }
                 }
             }
             this.Close();
        }
        private void sumgrid()
        {
            sumpercent = 0;
            sumamount = 0;
            for (int i = 0; i < gridSplit.RowCount; i++)
            {
                sumpercent = sumpercent + Convert.ToDecimal(gridSplit.Rows[i].Cells["percent"].Value);
                sumamount = sumamount + Convert.ToDecimal(gridSplit.Rows[i].Cells["amount"].Value);
            }
        }

        private void setTranslate()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblAmount.Text) != null)
                    lblAmount.Text = resxSet.GetString(lblAmount.Text);

                if (resxSet.GetString(btnSave.Text) != null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);

                if (resxSet.GetString(btnExit.Text) != null)
                    btnExit.Text = resxSet.GetString(btnExit.Text);
            }
        }

        private void gridSplit_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            string saveLayout = "Save Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem = new RadMenuItem();
            customMenuItem.Text = saveLayout;
            customMenuItem.Click += SaveLayoutOL;
            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);

            //== delete

            string saveLayout1 = "Delete Layout";

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                {
                    saveLayout = resxSet.GetString(saveLayout);
                }
            }
            RadMenuItem customMenuItem1 = new RadMenuItem();
            customMenuItem1.Text = saveLayout1;
            customMenuItem1.Click += SaveLayoutSplitOpenLines;
            RadMenuSeparatorItem separator1 = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator1);
            e.ContextMenu.Items.Add(customMenuItem1);
        }

        private void SaveLayoutOL(object sender, EventArgs e)
        {
            if (File.Exists(layoutSplitOpenLines))
            {
                File.Delete(layoutSplitOpenLines);
            }
            gridSplit.SaveLayout(layoutSplitOpenLines);

            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully save layout!");

        }

        private void SaveLayoutSplitOpenLines(object sender, EventArgs e)
        {
            if (File.Exists(layoutSplitOpenLines))
            {
                File.Delete(layoutSplitOpenLines);
            }

            translateRadMessageBox msg = new translateRadMessageBox();
            msg.translateAllMessageBox("You have successfully delete layout!");
        }
    }
}
