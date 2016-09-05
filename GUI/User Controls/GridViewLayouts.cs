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
    public class GridViewLayouts : System.Windows.Forms.UserControl, IBISGrid
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        public event EventHandler<LayoutsSelectedRowchanged> layoutsRowSelectChanged = delegate { };

        public void RaiseStatusChanged(LayoutsModel layouts)
        {
            layoutsRowSelectChanged(this, new LayoutsSelectedRowchanged { layouts = layouts });
        }

        LayoutsBUS layoutsBUS;
        private Telerik.WinControls.UI.RadGridView gridLayouts;
        private LayoutsModel _selectedRowLayout;
        private LayoutsModel _clickedLayout;
        private string filterFolder;
        private bool _bLoadTreeMenu = false;
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public GridViewLayouts()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\layouts")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\layouts"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\layouts");
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\layouts")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\layouts"));
            }
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\layouts");
            
            layoutsBUS = new LayoutsBUS();
            InitializeComponent();
           
        }

        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridLayouts = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridLayouts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLayouts.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridLayouts
            // 
            this.gridLayouts.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridLayouts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLayouts.EnableFastScrolling = true;
            this.gridLayouts.EnterKeyMode = Telerik.WinControls.UI.RadGridViewEnterKeyMode.EnterMovesToNextRow;
            this.gridLayouts.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridLayouts.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridLayouts.MasterTemplate.AllowAddNewRow = false;
            this.gridLayouts.MasterTemplate.AllowCellContextMenu = false;
            this.gridLayouts.MasterTemplate.AllowDeleteRow = false;
            this.gridLayouts.MasterTemplate.AllowEditRow = false;
            this.gridLayouts.MasterTemplate.AllowSearchRow = true;
            this.gridLayouts.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridLayouts.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridLayouts.MasterTemplate.EnableFiltering = true;
            this.gridLayouts.MasterTemplate.EnablePaging = true;
            this.gridLayouts.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridLayouts.MasterTemplate.PageSize = 50;
            this.gridLayouts.MasterTemplate.ShowGroupedColumns = true;
            this.gridLayouts.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridLayouts.Name = "gridLayouts";
            this.gridLayouts.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridLayouts.Size = new System.Drawing.Size(150, 150);
            this.gridLayouts.TabIndex = 0;
            this.gridLayouts.Text = "Layouts Grid";
            this.gridLayouts.ThemeName = "VisualStudio2012Light";
            this.gridLayouts.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridLayouts_CellBeginEdit);
            this.gridLayouts.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridLayouts_CellEditorInitialized);
            this.gridLayouts.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridLayouts_CellEndEdit);
            this.gridLayouts.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridLayouts_CurrentRowChanged);
            this.gridLayouts.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridLayouts_CellClick);
            this.gridLayouts.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridLayouts_CellDoubleClick);
            this.gridLayouts.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridLayouts_GroupSummaryEvaluate);
            this.gridLayouts.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridLayouts_DataBindingComplete);
            this.gridLayouts.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridLayouts_FilterChanging);
            // 
            // GridViewLayouts
            // 
            this.Controls.Add(this.gridLayouts);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GridViewLayouts";
            ((System.ComponentModel.ISupportInitialize)(this.gridLayouts.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLayouts)).EndInit();
            this.ResumeLayout(false);

        }

        #region GettersSetters
        // getters setters
        public bool bLoadTreeMenu
        {
            get { return _bLoadTreeMenu; }
            set { _bLoadTreeMenu = value; }
        }

        public Telerik.WinControls.UI.RadGridView LayoutsGridView
        {
            get { return gridLayouts; }
        }
        public LayoutsModel SelectedRowLayout
        {
            get { return _selectedRowLayout; }
        }
        public LayoutsModel ClickedLayout
        {
            get { return _clickedLayout; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridLayouts.Columns; }
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

        public List<IModel> GetData(int selectedFilter, List<int> idLabelList, string idLang)
        {
            return layoutsBUS.GetAllLayoutsAsIMODEL();
        }
        public List<FilterModel> ReturnFilters()
        {
            return Login._personFilters;
        }
        public List<LabelModel> ReturnLabels()
        {
            return Login._personLabels;
        }
        public void SetDataPersonBinding(List<IModel> binding)
        {
            System.ComponentModel.BindingList<IModel> bindList = new System.ComponentModel.BindingList<IModel>(binding);

            this.gridLayouts.DataSource = null;
            this.gridLayouts.DataSource = bindList;

        }
        public void ClearDescriptors()
        {
            this.gridLayouts.MasterTemplate.SortDescriptors.Clear();
            this.gridLayouts.MasterTemplate.GroupDescriptors.Clear();
            this.gridLayouts.MasterTemplate.FilterDescriptors.Clear();

        }
        public void removeRow(LayoutsModel rw)
        {
            using (gridLayouts.DeferRefresh())
            {
                GridViewRowInfo row = this.gridLayouts.Rows.Where(s => s.Cells["idLayout"].Value.ToString() == rw.idLayout.ToString()).FirstOrDefault();
                if (row != null)
                    this.gridLayouts.Rows.Remove(row);
            }
        }
        public void SaveLayout(string filename)
        {
            this.gridLayouts.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridLayouts.LoadLayout(filterFolder + "\\" + filename);
        }
        public IModel ReturnRowData(IModel data)
        {
            return data;
        }
        public void LoadTreeFavorites(IList<RadTreeNode> nodes, List<FilterModel> lista)
        {
            
        }
        public void LoadCustomFilters(IList<RadTreeNode> nodes, Boolean isSaveLayoutDialogClicked)
        {
            
        }

        public void LoadTreeViewRootLabels(IList<RadTreeNode> nodes, List<LabelModel> lista)
        {
            
        }
        #endregion Functions


        #region Grid Events
        private void gridLayouts_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                dictionary.Add(column.HeaderText, column.HeaderText);
                //column.MaxWidth = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38)*2;
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                column.MinWidth = column.Width;
            }
            for (int i = 0; i < gridLayouts.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(gridLayouts.Columns[i].HeaderText) != null)
                        gridLayouts.Columns[i].HeaderText = resxSet.GetString(gridLayouts.Columns[i].HeaderText);
                }

            }
            if (gridLayouts.Columns.Count > 0)
            {
                gridLayouts.Columns["idLayout"].IsVisible = false;
                gridLayouts.Columns["languageLayout"].IsVisible = false;                
                gridLayouts.Columns["bookmarks"].IsVisible = false;
                gridLayouts.Columns["userModified"].IsVisible = false;
                gridLayouts.Columns["userCreated"].IsVisible = false;
                gridLayouts.Columns["nameUserModified"].IsVisible = false;
                gridLayouts.Columns["nameUserCreated"].IsVisible = false;
                gridLayouts.Columns["dtModified"].IsVisible = false;
                gridLayouts.Columns["dtCreated"].IsVisible = false;                

                //for number of rows
                this.gridLayouts.SummaryRowsTop.Clear();
                gridLayouts.MasterTemplate.EnablePaging = false;
                gridLayouts.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridLayouts.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridLayouts.SummaryRowsTop.Add(summaryRowItem);
                GridViewSummaryRowItem summaryRowItem2 = new GridViewSummaryRowItem();
            }
        }

        private void gridLayouts_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridLayouts.MasterTemplate)
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

        private void gridLayouts_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            LayoutsModel selectedLayout = new LayoutsModel();
            GridViewRowInfo info = this.gridLayouts.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedLayout = (LayoutsModel)info.DataBoundItem;
                //  selectedPassport = passportBUS.GetPassport(selectedPerson.idContPers);


                if (selectedLayout != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedLayout = selectedLayout;
                    
                    if (selectedLayout != null)
                    {
                        System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show("Edit " + selectedLayout.nameLayout + " ?", "Edit Template", System.Windows.Forms.MessageBoxButtons.YesNo);

                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            frmBookmark2 frmBookmarks = new frmBookmark2(selectedLayout);
                            frmBookmarks.ShowDialog();

                            List<IModel> model = new List<IModel>();                            
                            model = this.GetData(0,null,"");
                            this.SetDataPersonBinding(model);
                            
                        }
                    }
                    
                }
            }
        }

        private void gridLayouts_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridLayouts.MasterView.TableHeaderRow && e.CurrentRow != gridLayouts.MasterView.TableFilteringRow && e.CurrentRow != gridLayouts.MasterView.TableSearchRow)
            {
                LayoutsModel selectedLayout = new LayoutsModel();
                selectedLayout = (LayoutsModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowLayout = selectedLayout;
                    
                    RaiseStatusChanged(selectedLayout);
                }
            }
        }


        #endregion Grid Events

        private void gridLayouts_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridLayouts.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridLayouts.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridLayouts.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridLayouts.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridLayouts.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridLayouts.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridLayouts.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridLayouts.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridLayouts.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridLayouts.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridLayouts.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridLayouts.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridLayouts_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridLayouts.IsInEditMode && !(this.gridLayouts.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridLayouts_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridLayouts.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridLayouts_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridLayouts.FilterChanging -= gridLayouts_FilterChanging;

                this.gridLayouts.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridLayouts.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridLayouts.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridLayouts.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridLayouts.FilterChanging += gridLayouts_FilterChanging;
            }  
        }

        private void gridLayouts_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridLayouts.EndEdit();
            }
        }


    

    
    }

    public class LayoutsSelectedRowchanged : EventArgs
    {
        public LayoutsModel layouts { get; set; }
    }
}
