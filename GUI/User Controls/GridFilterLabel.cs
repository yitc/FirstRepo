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
    public partial class GridViewFilterLabel : System.Windows.Forms.UserControl, IBISGrid
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
       private System.ComponentModel.IContainer components = null;
        public bool isChanged = false;

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

        public event EventHandler<FilterLabelStatusSelectedRowchanged> FilterLabelStatusSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(FiltersLabelsModel filterLLabel)
        {
            FilterLabelStatusSelectedRowchanged(this, new FilterLabelStatusSelectedRowchanged { filterLabel = filterLLabel });
        }

        
        FiltersLabelsBUS flBUS;
        public Telerik.WinControls.UI.RadGridView gridFilterLabel;
        private FiltersLabelsModel _selectedRowFiltersLabels;
        private FiltersLabelsModel _clickedFiltersLabels;
        
        // Folder u kome cuva filtere za Usere
        private string filterFolder;
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridViewFilterLabel()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\filtersLabels")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\filtersLabels"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\filtersLabels")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\filtersLabels"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\filtersLabels");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\filtersLabels");

            flBUS = new FiltersLabelsBUS();

           InitializeComponent();        
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridFilterLabel = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridFilterLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFilterLabel.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridFilterLabel
            // 
            this.gridFilterLabel.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridFilterLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridFilterLabel.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridFilterLabel.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridFilterLabel.MasterTemplate.AllowAddNewRow = false;
            this.gridFilterLabel.MasterTemplate.AllowCellContextMenu = false;
            this.gridFilterLabel.MasterTemplate.AllowDeleteRow = false;
            this.gridFilterLabel.MasterTemplate.AllowEditRow = false;
            this.gridFilterLabel.MasterTemplate.AllowSearchRow = true;
            this.gridFilterLabel.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridFilterLabel.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridFilterLabel.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridFilterLabel.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridFilterLabel.MasterTemplate.EnableFiltering = true;
            this.gridFilterLabel.MasterTemplate.EnablePaging = true;
            this.gridFilterLabel.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridFilterLabel.MasterTemplate.PageSize = 50;
            this.gridFilterLabel.MasterTemplate.ShowGroupedColumns = true;
            this.gridFilterLabel.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridFilterLabel.Name = "gridFilterLabel";
            this.gridFilterLabel.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridFilterLabel.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridFilterLabel.Size = new System.Drawing.Size(150, 150);
            this.gridFilterLabel.TabIndex = 0;
            this.gridFilterLabel.Text = "Filter /label Grid";
            this.gridFilterLabel.ThemeName = "VisualStudio2012Light";
            this.gridFilterLabel.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridFilterLabel_CellBeginEdit);
            this.gridFilterLabel.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridFilterLabel_CellEditorInitialized);
            this.gridFilterLabel.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridFilterLabel_CellEndEdit);
            this.gridFilterLabel.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridFiltersLabels_CurrentRowChanged);
            this.gridFilterLabel.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridFilterLabel_CellClick);
            this.gridFilterLabel.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridFiltersLabels_CellDoubleClick);
            this.gridFilterLabel.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridFilterLabel_GroupSummaryEvaluate);
            this.gridFilterLabel.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.rgv_DataBindingComplete);
            this.gridFilterLabel.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridFilterLabel_FilterChanging);
            this.gridFilterLabel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridFilterLabel_KeyDown);
            // 
            // GridViewFilterLabel
            // 
            this.Controls.Add(this.gridFilterLabel);
            this.Name = "GridViewFilterLabel";
            ((System.ComponentModel.ISupportInitialize)(this.gridFilterLabel.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridFilterLabel)).EndInit();
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
            get { return gridFilterLabel; }
        }

        public FiltersLabelsModel SelectedRowFiltersLabels
        {
            get { return _selectedRowFiltersLabels; }
        }
        public FiltersLabelsModel ClickedFiltersLabels
        {
            get { return _clickedFiltersLabels; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridFilterLabel.Columns; }
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

        private void rgv_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                dictionary.Add(column.HeaderText, column.HeaderText);
                //column.MaxWidth = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                //column.Width = column.MaxWidth;
                //column.MinWidth = column.MaxWidth;
            }
            for (int i = 0; i < gridFilterLabel.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(gridFilterLabel.Columns[i].HeaderText)!=null)
                    gridFilterLabel.Columns[i].HeaderText = resxSet.GetString(gridFilterLabel.Columns[i].HeaderText);
                }

            //    gridFilterLabel.Columns["nameMenu"].IsVisible = false;
               
            }
            if (gridFilterLabel.Columns.Count > 0)
            {
                //for number of rows
                this.gridFilterLabel.SummaryRowsTop.Clear();
                gridFilterLabel.MasterTemplate.EnablePaging = false;
                gridFilterLabel.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = grid.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridFilterLabel.SummaryRowsTop.Add(summaryRowItem);
                GridViewSummaryRowItem summaryRowItem2 = new GridViewSummaryRowItem();
            }
            //grid.Columns["Account Id"].IsVisible = false;
            //grid.Columns["genderEmployee"].IsVisible = false;
            //grid.Columns["idCountry"].IsVisible = false;
            //grid.Columns["Department"].IsVisible = false;
            //grid.Columns["Function"].IsVisible = false;
            //grid.Columns["WishFunction"].IsVisible = false;
            //grid.Columns["statusEmployee"].IsVisible = false;
            //grid.Columns["imageEmployee"].IsVisible = false;
            gridFilterLabel.Columns["IDLabelUnique"].IsVisible = false;
        }

        private void gridFilterLabel_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridFilterLabel.MasterTemplate)
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
        #region Function
        //function implementation from IGrid.cs interface
        //
        public List<IModel> GetData(int selectedFilter, List<int> idLabelList, string lang)
        {
            return flBUS.GetAllFiltersLabels(Login._user.lngUser);
        }
        public List<FilterModel> ReturnFilters()
        {
            return Login._usersFilters;
        }
        public List<LabelModel> ReturnLabels()
        {
            return Login._usersLabels;
        }       
      
        public void SetDataPersonBinding(List<IModel> binding)
        {
            this.gridFilterLabel.DataSource = null;
            this.gridFilterLabel.DataSource = binding;

        }

        public void ClearDescriptors()
        {
            this.gridFilterLabel.MasterTemplate.SortDescriptors.Clear();
            this.gridFilterLabel.MasterTemplate.GroupDescriptors.Clear();
            this.gridFilterLabel.MasterTemplate.FilterDescriptors.Clear();
        }
        public void removeRow(FiltersLabelsModel rw)
        {
            using (gridFilterLabel.DeferRefresh())
            {
                GridViewRowInfo row = this.gridFilterLabel.Rows.Where(s => s.Cells["ID"].Value.ToString() == rw.ID.ToString()).FirstOrDefault();
                if (row != null)
                    this.gridFilterLabel.Rows.Remove(row);
            }
        }

        public void SaveLayout(string filename)
        {
            this.gridFilterLabel.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridFilterLabel.LoadLayout(filterFolder + "\\" + filename);
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

        public FiltersLabelsModel ReturnRowData(FiltersLabelsModel data)
        {
            return data;
        }
        #endregion Functions

        #region Grid Events
        // eventi na gridu
    
        private void gridFiltersLabels_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            FiltersLabelsModel selectedFiltersLabels = new FiltersLabelsModel();
            GridViewRowInfo info = this.gridFilterLabel.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedFiltersLabels = (FiltersLabelsModel)info.DataBoundItem;

                if (selectedFiltersLabels != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedFiltersLabels = selectedFiltersLabels;
                    frmFiltersLabels frm = new frmFiltersLabels(this._clickedFiltersLabels);
                    frm.ShowDialog();
                    if (frm.isChanged == true)
                    {
                        FiltersLabelsBUS nbus = new FiltersLabelsBUS();

                   
                        gridFilterLabel.DataSource = null;
                        gridFilterLabel.DataSource = nbus.GetAllFiltersLabels(Login._user.lngUser);
                    }
                }
            }
        }
        private void gridFiltersLabels_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridFilterLabel.MasterView.TableHeaderRow && e.CurrentRow != gridFilterLabel.MasterView.TableFilteringRow && e.CurrentRow != gridFilterLabel.MasterView.TableSearchRow)
            {
                FiltersLabelsModel selectedFiltersLabels = new FiltersLabelsModel();
                selectedFiltersLabels = (FiltersLabelsModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowFiltersLabels = selectedFiltersLabels;
                    RaiseStatusChanged(selectedFiltersLabels);
                }
             
            }
           
        }
        #endregion

        private void gridFilterLabel_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.gridFilterLabel.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    FiltersLabelsModel model = (FiltersLabelsModel)this.gridFilterLabel.CurrentRow.DataBoundItem;
                    frmFiltersLabels frm = new frmFiltersLabels(model);
                    frm.ShowDialog();
                    return;
                }
            }
        }

        private void gridFilterLabel_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridFilterLabel.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridFilterLabel.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridFilterLabel.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridFilterLabel.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridFilterLabel.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridFilterLabel.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridFilterLabel.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridFilterLabel.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }
        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridFilterLabel.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridFilterLabel.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridFilterLabel.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridFilterLabel.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridFilterLabel_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridFilterLabel.IsInEditMode && !(this.gridFilterLabel.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridFilterLabel_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridFilterLabel.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridFilterLabel_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridFilterLabel.FilterChanging -= gridFilterLabel_FilterChanging;

                this.gridFilterLabel.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridFilterLabel.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridFilterLabel.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridFilterLabel.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridFilterLabel.FilterChanging += gridFilterLabel_FilterChanging;
            }  
        }

        private void gridFilterLabel_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridFilterLabel.EndEdit();
            }
        }

      

    }

    public class FilterLabelStatusSelectedRowchanged : EventArgs
    {
        public FiltersLabelsModel filterLabel { get; set; }
        //user=filterLabel
    }


}
