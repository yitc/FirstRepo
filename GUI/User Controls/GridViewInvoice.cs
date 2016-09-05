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
using System.ComponentModel;
using Telerik.WinControls.Data;



namespace GUI
{

    public partial class GridViewInvoice : System.Windows.Forms.UserControl, IBISGrid 
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

        // Event kad se promeni selekcija rowa. Updejtuje personDetailView kontrolu.
        public event EventHandler<InvoiceStatusSelectedRowchanged> InvoiceStatusSelectedRowchanged = delegate { };
        public void RaiseStatusChanged(InvoiceModel Invoice)
        {
            InvoiceStatusSelectedRowchanged(this, new InvoiceStatusSelectedRowchanged { Invoice = Invoice });
        }
        
        InvoiceBUS invoiceBUS;
        private List<IModel> modelData;
        private Telerik.WinControls.UI.RadGridView gridInvoice;
        private InvoiceModel _selectedRowInvoice;
        private InvoiceModel _clickedInvoice;
        private string filterFolder;
        private string labelFolder;
        private bool  _bLoadTreeMenu = false;


        Dictionary<string, string> dictionary = new Dictionary<string, string>();                
        
        public GridViewInvoice()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\Invoice")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\Invoice"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\Invoice")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\Invoice"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\Invoice");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\Invoice");
            invoiceBUS = new InvoiceBUS();

            InitializeComponent();
        }
      

        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridInvoice = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoice.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridInvoice
            // 
            this.gridInvoice.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridInvoice.EnableFastScrolling = true;
            this.gridInvoice.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.gridInvoice.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridInvoice.MasterTemplate.AllowAddNewRow = false;
            this.gridInvoice.MasterTemplate.AllowCellContextMenu = false;
            this.gridInvoice.MasterTemplate.AllowDeleteRow = false;
            this.gridInvoice.MasterTemplate.AllowEditRow = false;
            this.gridInvoice.MasterTemplate.AllowSearchRow = true;
            this.gridInvoice.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridInvoice.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridInvoice.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridInvoice.MasterTemplate.EnableFiltering = true;
            this.gridInvoice.MasterTemplate.EnablePaging = true;
            this.gridInvoice.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridInvoice.MasterTemplate.PageSize = 50;
            this.gridInvoice.MasterTemplate.ShowFilterCellOperatorText = false;
            this.gridInvoice.MasterTemplate.ShowGroupedColumns = true;
            this.gridInvoice.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridInvoice.Name = "gridInvoice";
            this.gridInvoice.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridInvoice.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridInvoice.Size = new System.Drawing.Size(150, 150);
            this.gridInvoice.TabIndex = 0;
            this.gridInvoice.Text = "Invoice Grid";
            this.gridInvoice.ThemeName = "VisualStudio2012Light";
            this.gridInvoice.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridInvoice_CellBeginEdit);
            this.gridInvoice.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridInvoice_CellEditorInitialized);
            this.gridInvoice.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridInvoice_CellEndEdit);
            this.gridInvoice.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridInvoice_CurrentRowChanged);
            this.gridInvoice.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridInvoice_CellClick);
            this.gridInvoice.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridInvoice_CellDoubleClick);
            this.gridInvoice.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridInvoice_GroupSummaryEvaluate);
            this.gridInvoice.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridInvoice_DataBindingComplete);
            this.gridInvoice.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridInvoice_FilterChanging);
            this.gridInvoice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridInvoice_KeyDown);
            // 
            // GridViewInvoice
            // 
            this.Controls.Add(this.gridInvoice);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GridViewInvoice";
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoice.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridInvoice)).EndInit();
            this.ResumeLayout(false);

        }

        #region GettersSetters
        // getters setters
        public bool bLoadTreeMenu
        {
            get { return _bLoadTreeMenu; }
            set { _bLoadTreeMenu = value; }
        }

        public Telerik.WinControls.UI.RadGridView InvoiceGridView
        {
            get { return gridInvoice; }
        }
        public InvoiceModel SelectedRowPerson
        {
            get { return _selectedRowInvoice; }
        }
        public InvoiceModel ClickedInvoice
        {
            get { return _clickedInvoice; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridInvoice.Columns; }
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

        #region Functions
        //function implementation from IGrid.cs interface
        // 

          //public List<IModel> GetData()
         public List<IModel> GetData(int selectedFilter, List<int> idLabelList, string idLang)
        {
           // return  EmployeeBUS.GetAllEmployees(Convert.ToInt32(selectedFilter), idLabelList, idLang); 
            return invoiceBUS.GetAllInvoices();
        }
        public List<FilterModel> ReturnFilters()
        {
            return Login._employeeFilters;
        }
        public List<LabelModel> ReturnLabels()
        {
            return Login._employeeLabels;
        }                
        public void SetDataPersonBinding(List<IModel> binding)
        {
            BindingList<InvoiceModel> invoicelist = new BindingList<InvoiceModel>();
            if (binding != null)
            {
                foreach (IModel m in binding)
                {
                    invoicelist.Add((InvoiceModel)m);
                }

                this.gridInvoice.DataSource = null;
                this.gridInvoice.DataSource = invoicelist;

                InvoiceItemsBUS itembus = new InvoiceItemsBUS();
                List<InvoiceItemsModel> tmp = new List<InvoiceItemsModel>();
                tmp = itembus.GetAllInvoiceItems(Login._user.lngUser);
                BindingList<InvoiceItemsModel> invoiceItemsList;
                if (tmp != null)
                    invoiceItemsList = new BindingList<InvoiceItemsModel>(tmp);
                else
                    invoiceItemsList = new BindingList<InvoiceItemsModel>();

                GridViewTemplate template = new GridViewTemplate();
                template.DataSource = invoiceItemsList;
                template.AllowEditRow = false;
                template.AllowDeleteRow = false;
                template.AllowAddNewRow = false;
                template.AllowColumnResize = true;

                if (template.Columns.Count > 0)
                {
                    template.Columns["idInvItem"].IsVisible = false;
                    template.Columns["userCreated"].IsVisible = false;
                    template.Columns["dtCreated"].IsVisible = false;
                    template.Columns["userModified"].IsVisible = false;
                    template.Columns["dtModified"].IsVisible = false;

                    template.Columns["idInvoice"].Width = 70;
                    template.Columns["idArtical"].Width = 70;
                    template.Columns["price"].Width = 70;
                    template.Columns["quantity"].Width = 70;

                }

                gridInvoice.MasterTemplate.Templates.Add(template);
                GridViewRelation relation = new GridViewRelation(gridInvoice.MasterTemplate);
                relation.ChildTemplate = template;
                relation.RelationName = "Items";
                relation.ParentColumnNames.Add("idInvoice");
                relation.ChildColumnNames.Add("idInvoice");
                gridInvoice.Relations.Add(relation);
            }
                        
        }
        public void ClearDescriptors()
        {
            this.gridInvoice.MasterTemplate.SortDescriptors.Clear();
            this.gridInvoice.MasterTemplate.GroupDescriptors.Clear();
            this.gridInvoice.MasterTemplate.FilterDescriptors.Clear();

        }
        public void SaveLayout(string filename)
        {            
            this.gridInvoice.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridInvoice.LoadLayout(filterFolder + "\\" + filename);
        }
        public IModel ReturnRowData(IModel data)
        {
            return data;
        }
        public void LoadTreeFavorites(IList<RadTreeNode> nodes, List<FilterModel> lista)
        {
            if (bLoadTreeMenu == true)
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
        #endregion Functions

        #region Grid Events
        private void gridInvoice_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            if (gridInvoice != null)
            {
                if (gridInvoice.Columns.Count > 0)
                {
                    var grid = sender as RadGridView;
                    foreach (var column in grid.Columns)
                    {
                        dictionary.Add(column.HeaderText, column.HeaderText);
                        //column.MaxWidth = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                        column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                        column.MinWidth = column.Width;
                    }
                    for (int i = 0; i < gridInvoice.Columns.Count; i++)
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString(gridInvoice.Columns[i].HeaderText) != null)
                                //              grid.Columns[i].HeaderText = resxSet.GetString(grid.Columns[i].HeaderText);
                                gridInvoice.Columns[i].HeaderText = resxSet.GetString(gridInvoice.Columns[i].HeaderText);
                        }
                }
            }
            if (gridInvoice != null)
            {
                if (gridInvoice.Columns.Count > 0)
                {
                    //for number of rows
                    this.gridInvoice.SummaryRowsTop.Clear();
                    gridInvoice.MasterTemplate.EnablePaging = false;
                    gridInvoice.MasterTemplate.ShowTotals = true;
                    GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                    summaryItem.Name = gridInvoice.Columns[0].Name;
                    summaryItem.Aggregate = GridAggregateFunction.Count;

                    GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                    summaryRowItem.Add(summaryItem);
                    this.gridInvoice.SummaryRowsTop.Add(summaryRowItem);
                    GridViewSummaryRowItem summaryRowItem2 = new GridViewSummaryRowItem();
                }
            }

            if (gridInvoice.Columns != null)
            {
                if (gridInvoice.RowCount > 0)
                {
                    this.gridInvoice.Columns["dtInvoice"].FormatString = "{0: dd-MM-yyyy}";
                    this.gridInvoice.Columns["dtValuta"].FormatString = "{0: dd-MM-yyyy}";
                    this.gridInvoice.Columns["dtCreated"].FormatString = "{0: dd-MM-yyyy}";
                    this.gridInvoice.Columns["dtModified"].FormatString = "{0: dd-MM-yyyy}";
                    this.gridInvoice.Columns["dtFirstPay"].FormatString = "{0: dd-MM-yyyy}";
                    this.gridInvoice.Columns["dtLastPay"].FormatString = "{0: dd-MM-yyyy}";

                }
            }

        }

        private void gridInvoice_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridInvoice.MasterTemplate)
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
        
        private void gridInvoice_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row.DataBoundItem != null)
            {
                Type t = e.Row.DataBoundItem.GetType();
                if (t == typeof(InvoiceModel))
                {
                    GridViewRowInfo info = this.gridInvoice.CurrentRow;
                    InvoiceModel selectedInvoice = (InvoiceModel)info.DataBoundItem;
                    _selectedRowInvoice = new InvoiceModel();
                    _clickedInvoice = new InvoiceModel();

                    if (info != null && e.RowIndex >= 0)
                        if (selectedInvoice != null)
                        {
                            frmInvoice frm = new frmInvoice(selectedInvoice);
                            frm.ShowDialog();
                       //     RadMessageBox.Show("Invoice: " + selectedInvoice.idInvoice.ToString());
                            //modelData = invoiceBUS.GetAllInvoices();
                            //this.SetDataPersonBinding(modelData);  

                        }
                }
            }
        }

        private void gridInvoice_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            
            if (e.CurrentRow != null && e.CurrentRow != gridInvoice.MasterView.TableHeaderRow && e.CurrentRow != gridInvoice.MasterView.TableFilteringRow && e.CurrentRow != gridInvoice.MasterView.TableSearchRow)
            {
                if (e.CurrentRow.DataBoundItem != null)
                {
                    Type t = e.CurrentRow.DataBoundItem.GetType();
                    if (t == typeof(InvoiceModel))
                    {
                        InvoiceModel selectedInvoice = new InvoiceModel();
                        selectedInvoice = (InvoiceModel)e.CurrentRow.DataBoundItem;
                        if (e.CurrentRow.DataBoundItem != null)
                        {
                            this._selectedRowInvoice = selectedInvoice;
                            RaiseStatusChanged(selectedInvoice);
                        }
                    }
                }
            }
            
        }
        #endregion Grid Events

        private void gridInvoice_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter || e.KeyCode == Keys.Return)
            {
                //InvoiceModel model = (InvoiceModel)this.gridInvoice.CurrentRow.DataBoundItem;
                //frmInvoiceAccount frm = new frmInvoiceAccount(model);
                //frm.Show();                    
                //return;
            }                                                                         
        }

        private void gridInvoice_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridInvoice.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridInvoice.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridInvoice.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridInvoice.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridInvoice.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridInvoice.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridInvoice.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridInvoice.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridInvoice.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridInvoice.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridInvoice.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
            //else if(e.KeyData == Keys.Enter || e.KeyData == Keys.Return)
            //{
            //    if (this.gridArrangement.CurrentRow != null && this.gridArrangement.CurrentRow is GridViewFilteringRowInfo && this.gridArrangement.IsInEditMode)
            //    {
            //        this.gridArrangement.EndEdit();
            //    }
            //}
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridInvoice_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridInvoice.IsInEditMode && !(this.gridInvoice.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridInvoice_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridInvoice.FilterChanging -= gridInvoice_FilterChanging;

                this.gridInvoice.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridInvoice.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridInvoice.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridInvoice.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridInvoice.FilterChanging += gridInvoice_FilterChanging;
            }  
        }

        private void gridInvoice_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridInvoice.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }            
        }

        private void gridInvoice_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridInvoice.EndEdit();
            }
        }



    }
    public class InvoiceStatusSelectedRowchanged : EventArgs
    {
      public InvoiceModel Invoice { get; set; }
      //  public PersonModel person { get; set; }
    }
}
