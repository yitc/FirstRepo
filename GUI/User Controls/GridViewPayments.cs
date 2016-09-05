using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI;
using BIS.Model;
using BIS.Business;
using System.IO;
using System.Resources;
using Telerik.WinControls;
using System.Windows.Forms;
using Telerik.WinControls.Data;


namespace GUI
{    
    public partial class GridViewPayments : System.Windows.Forms.UserControl, IBISGrid
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

        public event EventHandler<PaymentSelectedRowchanged> PaymentSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(AccPaymentModel art)
        {
            PaymentSelectedRowchanged(this, new PaymentSelectedRowchanged { a = art });
        }


        AccPaymentBUS AccPaymentBUS;
        private Telerik.WinControls.UI.RadGridView gridPayments;
        private AccPaymentModel _selectedRowPayment;
        private AccPaymentModel _clickedPayment;
        public List<IModel> modelData;
        // Folder u kome cuva filtere za Payment
        private string filterFolder;

        // Folder u kome cuva labele za Payment
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridViewPayments()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\payment")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\payment"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\payment")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\payment"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\payment");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\payment");
            AccPaymentBUS = new AccPaymentBUS();

            InitializeComponent();        
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridPayments = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridPayments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPayments.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridPayments
            // 
            this.gridPayments.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPayments.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridPayments.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridPayments.MasterTemplate.AllowAddNewRow = false;
            this.gridPayments.MasterTemplate.AllowCellContextMenu = false;
            this.gridPayments.MasterTemplate.AllowDeleteRow = false;
            this.gridPayments.MasterTemplate.AllowEditRow = false;
            this.gridPayments.MasterTemplate.AllowSearchRow = true;
            this.gridPayments.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridPayments.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridPayments.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridPayments.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridPayments.MasterTemplate.EnableFiltering = true;
            this.gridPayments.MasterTemplate.EnablePaging = true;
            this.gridPayments.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridPayments.MasterTemplate.PageSize = 50;
            this.gridPayments.MasterTemplate.ShowGroupedColumns = true;
            this.gridPayments.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridPayments.Name = "gridPayments";
            this.gridPayments.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridPayments.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridPayments.Size = new System.Drawing.Size(150, 150);
            this.gridPayments.TabIndex = 0;
            this.gridPayments.Text = "gridPayments";
            this.gridPayments.ThemeName = "VisualStudio2012Light";
            this.gridPayments.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridPayments_CellBeginEdit);
            this.gridPayments.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridPayments_CellEditorInitialized);
            this.gridPayments.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridPayments_CellEndEdit);
            this.gridPayments.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridPayment_CurrentRowChanged);
            this.gridPayments.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridPayments_CellClick);
            this.gridPayments.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridPayment_CellDoubleClick);
            this.gridPayments.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridPayment_GroupSummaryEvaluate);
            this.gridPayments.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridPayment_DataBindingComplete);
            this.gridPayments.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridPayments_FilterChanging);
            this.gridPayments.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridPayments_KeyDown);
            // 
            // GridViewPayments
            // 
            this.Controls.Add(this.gridPayments);
            this.Name = "GridViewPayments";
            ((System.ComponentModel.ISupportInitialize)(this.gridPayments.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPayments)).EndInit();
            this.ResumeLayout(false);

        }

        #region GettersSetters
        //getters setters
        public bool bLoadTreeMenu
        {
            get { return _bLoadTreeMenu; }
            set { _bLoadTreeMenu = value; }
        }
        public Telerik.WinControls.UI.RadGridView PersonsGridView
        {
            get { return gridPayments; }
        }

        public AccPaymentModel SelectedRowPayment
        {
            get { return _selectedRowPayment; }
        }
        public AccPaymentModel ClickedPayment
        {
            get { return _clickedPayment; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridPayments.Columns; }
        }
        public string FilterFolder
        {
            get { return filterFolder; }
        }

        public string LabelFolder
        {
            get { return labelFolder; }
        }
        #endregion GettersSetters

        #region Function
        //function implementation from IGrid.cs interface
        //
        public List<IModel> GetData(int selectedFilter, List<int> idLabelList, string idLang)
        {
            return AccPaymentBUS.GetAllAccPayment();  
        }
        public List<FilterModel> ReturnFilters()
        {
            return Login._paymentFilters;
        }
        public List<LabelModel> ReturnLabels()
        {
            return Login._paymentLabels;
        }       
      
        public void SetDataPersonBinding(List<IModel> binding)
        {
            this.gridPayments.DataSource = null;
            this.gridPayments.DataSource = binding;

        }

        public void ClearDescriptors()
        {
            this.gridPayments.MasterTemplate.SortDescriptors.Clear();
            this.gridPayments.MasterTemplate.GroupDescriptors.Clear();
            this.gridPayments.MasterTemplate.FilterDescriptors.Clear();
        }
        public void SaveLayout(string filename)
        {
            this.gridPayments.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridPayments.LoadLayout(filterFolder + "\\" + filename);
        }
              

        public void LoadTreeFavorites(IList<RadTreeNode> nodes, List<FilterModel> lista)
        {
            if (bLoadTreeMenu == true)
            {
                if (lista != null)
                {
                    RadTreeNode node;
                    foreach (var filter in lista)
                    {
                        node = new RadTreeNode(filter.nameFilter);
                        node.ImageKey = filter.nameFilter.Trim().ToLower();
                        node.Name = filter.idFilter.ToString();
                        nodes.Add(node);
                    }
                }
            }
        }
        public void LoadCustomFilters(IList<RadTreeNode> nodes, Boolean isSaveLayoutDialogClicked)
        {
            if (bLoadTreeMenu == true)
            {
                if (isSaveLayoutDialogClicked == false)
                {
                    string[] files = BIS.Core.Files.GetFileNames(filterFolder, "*.xml");
                    foreach (string file in files)
                    {
                        RadTreeNode node = new RadTreeNode(file.Replace(".xml", ""));
                        node.ImageKey = "customfilters";

                        nodes.Add(node);
                    }
                }
            }
        }

        public void LoadTreeViewRootLabels(IList<RadTreeNode> nodes, List<LabelModel> lista)
        {
            if (bLoadTreeMenu == true)
            {
                if (lista != null)
                {
                    RadTreeNode node;
                    foreach (var label in lista)
                    {
                        node = new RadTreeNode(label.nameLabel);
                        node.ImageKey = label.nameLabel.Trim().ToLower();
                        node.Name = label.idLabel.ToString();
                        nodes.Add(node);
                    }
                }
            }
        }       

        public UsersAllModel ReturnRowData(UsersAllModel data)
        {
            return data;
        }
        #endregion Functions

        #region Grid Events
        // eventi na gridu
        private void gridPayment_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();

            if (gridPayments != null)
                if (gridPayments.Columns.Count > 0)
                {
                    {
                        var grid = sender as RadGridView;
                        foreach (var column in grid.Columns)
                        {
                            dictionary.Add(column.HeaderText, column.HeaderText);
                            column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                            column.MinWidth = column.Width;
                        }
                        for (int i = 0; i < gridPayments.Columns.Count; i++)
                        {

                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {
                                if (resxSet.GetString(gridPayments.Columns[i].HeaderText) != null)
                                    gridPayments.Columns[i].HeaderText = resxSet.GetString(gridPayments.Columns[i].HeaderText);
                            }



                        }
                    }
                }

            if (gridPayments != null)
            {
                if (gridPayments.Columns.Count > 0)
                {
                    //for number of rows
                    this.gridPayments.SummaryRowsTop.Clear();
                    gridPayments.MasterTemplate.EnablePaging = false;
                    gridPayments.MasterTemplate.ShowTotals = true;
                    GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                    summaryItem.Name = gridPayments.Columns[0].Name;
                    summaryItem.Aggregate = GridAggregateFunction.Count;

                    GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                    summaryRowItem.Add(summaryItem);
                    this.gridPayments.SummaryRowsTop.Add(summaryRowItem);
                    GridViewSummaryRowItem summaryRowItem2 = new GridViewSummaryRowItem();

                    //grid.Columns["idUserCreated"].IsVisible = false;
                    //grid.Columns["nameUserCreated"].IsVisible = false;
                    //grid.Columns["dtUserCreated"].IsVisible = false;
                    //grid.Columns["idUserModifies"].IsVisible = false;
                    //grid.Columns["nameUserModified"].IsVisible = false;
                    //grid.Columns["dtUserModified"].IsVisible = false;
                }
            }
        }

        private void gridPayment_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridPayments.MasterTemplate)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Total") != null)
                        e.FormatString = String.Format(resxSet.GetString("Total") + " " + e.Value, e.Value);
                    else
                        e.FormatString = String.Format("Total " + e.Value, e.Value);
                }
            }
        }
        private void gridPayment_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            AccPaymentModel selectedPayment = new AccPaymentModel();
            GridViewRowInfo info = this.gridPayments.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedPayment = (AccPaymentModel)info.DataBoundItem;

                if (selectedPayment != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedPayment = selectedPayment;
                    frmAccPayment frm = new frmAccPayment(this._clickedPayment);

                    frm.ShowDialog();
                    AccPaymentBUS arbus = new AccPaymentBUS();
                    modelData = arbus.GetAllAccPayment(); ;
                    this.SetDataPersonBinding(modelData);  
                }
            }
        }
        private void gridPayment_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridPayments.MasterView.TableHeaderRow && e.CurrentRow != gridPayments.MasterView.TableFilteringRow && e.CurrentRow != gridPayments.MasterView.TableSearchRow)
            {
                AccPaymentModel selectedPayment = new AccPaymentModel();
                selectedPayment = (AccPaymentModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowPayment = selectedPayment;
                    RaiseStatusChanged(selectedPayment);
                }
            }
        }
        #endregion

        private void gridPayments_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.gridPayments.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    AccPaymentModel model = (AccPaymentModel)this.gridPayments.CurrentRow.DataBoundItem;
                    frmAccPayment frm = new frmAccPayment(model);
                    frm.Show();
                    // e.SuppressKeyPress = true;
                    //e.Handled = true;
                    return;

                }

            }
        }

        private void gridPayments_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridPayments.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridPayments.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPayments.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPayments.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPayments.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPayments.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPayments.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }
        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridPayments.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridPayments.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridPayments.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridPayments.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridPayments_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridPayments.IsInEditMode && !(this.gridPayments.CurrentColumn is GridViewCheckBoxColumn))
            {

                if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    lastFilterDescriptor = null;
                }
                else
                {
                    lastFilterDescriptor = e.NewItems[0] as FilterDescriptor;
                }

                e.Cancel = true;
            }
        }

        private void gridPayments_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridPayments.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridPayments_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridPayments.FilterChanging -= gridPayments_FilterChanging;

                this.gridPayments.FilterDescriptors.Remove(e.Column.Name);
                if (lastFilterDescriptor != null)
                {

                    if (e.Column is GridViewTextBoxColumn)
                        lastFilterDescriptor.Operator = FilterOperator.Contains;
                    else if (e.Column is GridViewDateTimeColumn)
                        lastFilterDescriptor.Operator = FilterOperator.IsGreaterThan;
                    else if (e.Column is GridViewDecimalColumn)
                        lastFilterDescriptor.Operator = FilterOperator.IsEqualTo;
                    else if (e.Column is GridViewCheckBoxColumn)
                        lastFilterDescriptor.Operator = FilterOperator.IsEqualTo;

                    GridViewCellInfo cellinf = e.Row.Cells[e.Column.Name];
                    cellinf.Value = lastFilterDescriptor.Value;
                    if (!this.gridPayments.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridPayments.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridPayments.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridPayments.FilterChanging += gridPayments_FilterChanging;
            } 
        }

        private void gridPayments_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridPayments.EndEdit();
            }
        }


    }

    public class PaymentSelectedRowchanged : EventArgs
    {
         public AccPaymentModel a { get; set; }
    }


}
