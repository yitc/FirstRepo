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
    public partial class GridViewArticalGroups : System.Windows.Forms.UserControl, IBISGrid
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

        public event EventHandler<ArticalGroupsSelectedRowchanged> ArticalGroupsSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(ArticalGroupsModel art)
        {
            ArticalGroupsSelectedRowchanged(this, new ArticalGroupsSelectedRowchanged { a = art });
        }


        ArticalGroupsBUS articalgroupsBUS;
        private Telerik.WinControls.UI.RadGridView gridArticalGroups;
        private ArticalGroupsModel _selectedRowArticalGroups;
        private ArticalGroupsModel _clickedArticalGroups;
        public List<IModel> modelData;
        // Folder u kome cuva filtere za ArticalGroups
        private string filterFolder;

        // Folder u kome cuva labele za ArticalGroups
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridViewArticalGroups()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\articalgroups")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\articalgroups"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\articalgroups")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\articalgroups"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\articalgroups");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\articalgroups");
            articalgroupsBUS = new ArticalGroupsBUS();

            InitializeComponent();        
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridArticalGroups = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridArticalGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridArticalGroups.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridArticalGroups
            // 
            this.gridArticalGroups.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridArticalGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridArticalGroups.EnableFastScrolling = true;
            this.gridArticalGroups.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridArticalGroups.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridArticalGroups.MasterTemplate.AllowAddNewRow = false;
            this.gridArticalGroups.MasterTemplate.AllowCellContextMenu = false;
            this.gridArticalGroups.MasterTemplate.AllowDeleteRow = false;
            this.gridArticalGroups.MasterTemplate.AllowEditRow = false;
            this.gridArticalGroups.MasterTemplate.AllowSearchRow = true;
            this.gridArticalGroups.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridArticalGroups.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridArticalGroups.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridArticalGroups.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridArticalGroups.MasterTemplate.EnableFiltering = true;
            this.gridArticalGroups.MasterTemplate.EnablePaging = true;
            this.gridArticalGroups.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridArticalGroups.MasterTemplate.PageSize = 50;
            this.gridArticalGroups.MasterTemplate.ShowGroupedColumns = true;
            this.gridArticalGroups.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridArticalGroups.Name = "gridArticalGroups";
            this.gridArticalGroups.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridArticalGroups.Size = new System.Drawing.Size(150, 150);
            this.gridArticalGroups.TabIndex = 0;
            this.gridArticalGroups.Text = "gridArticalGroups";
            this.gridArticalGroups.ThemeName = "VisualStudio2012Light";
            this.gridArticalGroups.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridArticalGroups_CellBeginEdit);
            this.gridArticalGroups.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridArticalGroups_CellEditorInitialized);
            this.gridArticalGroups.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridArticalGroups_CellEndEdit);
            this.gridArticalGroups.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridArticalGroups_CurrentRowChanged);
            this.gridArticalGroups.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridArticalGroups_CellClick);
            this.gridArticalGroups.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridArticalGroups_CellDoubleClick);
            this.gridArticalGroups.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridArticalGroups_GroupSummaryEvaluate);
            this.gridArticalGroups.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridArticalGroups_DataBindingComplete);
            this.gridArticalGroups.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridArticalGroups_FilterChanging);
            this.gridArticalGroups.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridArticalGroups_KeyDown);
            // 
            // GridViewArticalGroups
            // 
            this.Controls.Add(this.gridArticalGroups);
            this.Name = "GridViewArticalGroups";
            ((System.ComponentModel.ISupportInitialize)(this.gridArticalGroups.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridArticalGroups)).EndInit();
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
            get { return gridArticalGroups; }
        }

        public ArticalGroupsModel SelectedRowArticalGroups
        {
            get { return _selectedRowArticalGroups; }
        }
        public ArticalGroupsModel ClickedArticalGroups
        {
            get { return _clickedArticalGroups; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridArticalGroups.Columns; }
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
            return articalgroupsBUS.GetAllArticalGroups();  
        }
        public List<FilterModel> ReturnFilters()
        {
            return Login._articalgroupsFilters;
        }
        public List<LabelModel> ReturnLabels()
        {
            return Login._articalgroupsLabels;
        }       
      
        public void SetDataPersonBinding(List<IModel> binding)
        {
            this.gridArticalGroups.DataSource = null;
            this.gridArticalGroups.DataSource = binding;

        }

        public void ClearDescriptors()
        {
            this.gridArticalGroups.MasterTemplate.SortDescriptors.Clear();
            this.gridArticalGroups.MasterTemplate.GroupDescriptors.Clear();
            this.gridArticalGroups.MasterTemplate.FilterDescriptors.Clear();
        }
        public void SaveLayout(string filename)
        {
            this.gridArticalGroups.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridArticalGroups.LoadLayout(filterFolder + "\\" + filename);
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
        private void gridArticalGroups_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                dictionary.Add(column.HeaderText, column.HeaderText);
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                column.MinWidth = column.Width;
            }
            for (int i = 0; i < gridArticalGroups.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(gridArticalGroups.Columns[i].HeaderText) != null)
                        gridArticalGroups.Columns[i].HeaderText = resxSet.GetString(gridArticalGroups.Columns[i].HeaderText);
                }

            }
            if (gridArticalGroups.Columns.Count > 0)
            {
                //for number of rows
                this.gridArticalGroups.SummaryRowsTop.Clear();
                gridArticalGroups.MasterTemplate.EnablePaging = false;
                gridArticalGroups.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridArticalGroups.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridArticalGroups.SummaryRowsTop.Add(summaryRowItem);
                GridViewSummaryRowItem summaryRowItem2 = new GridViewSummaryRowItem();

                grid.Columns["idUserCreated"].IsVisible = false;
                grid.Columns["nameUserCreated"].IsVisible = false;
                grid.Columns["dtUserCreated"].IsVisible = false;
                grid.Columns["idUserModified"].IsVisible = false;
                grid.Columns["nameUserModified"].IsVisible = false;
                grid.Columns["dtUserModified"].IsVisible = false;
            }
        }

        private void gridArticalGroups_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridArticalGroups.MasterTemplate)
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
        private void gridArticalGroups_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            ArticalGroupsModel selectedArticalGroups = new ArticalGroupsModel();
            GridViewRowInfo info = this.gridArticalGroups.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedArticalGroups = (ArticalGroupsModel)info.DataBoundItem;

                if (selectedArticalGroups != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedArticalGroups = selectedArticalGroups;
                    frmArticleGroups frm = new frmArticleGroups(this._clickedArticalGroups);

                    frm.ShowDialog();
                    //ArticalGroupsBUS arbus = new ArticalGroupsBUS();
                    //modelData = arbus.GetAllArticalGroups(); ;
                    //this.SetDataPersonBinding(modelData);  
                }
            }
        }
        private void gridArticalGroups_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridArticalGroups.MasterView.TableHeaderRow && e.CurrentRow != gridArticalGroups.MasterView.TableFilteringRow && e.CurrentRow != gridArticalGroups.MasterView.TableSearchRow)
            {
                ArticalGroupsModel selectedArticalGroups = new ArticalGroupsModel();
                selectedArticalGroups = (ArticalGroupsModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowArticalGroups = selectedArticalGroups;
                    RaiseStatusChanged(selectedArticalGroups);
                }
            }
        }
        #endregion

        private void gridArticalGroups_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            
            if (e.KeyData == Keys.Enter)
            {
                ArticalGroupsModel selectedArticalGroups = new ArticalGroupsModel();
                GridViewRowInfo info = this.gridArticalGroups.CurrentRow;

                if (info != null && info.Index >= 0)
                {
                    selectedArticalGroups = (ArticalGroupsModel)info.DataBoundItem;

                    if (selectedArticalGroups != null)
                    {
                        //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                        this._clickedArticalGroups = selectedArticalGroups;
                        frmArticleGroups frm = new frmArticleGroups(this._clickedArticalGroups);

                        frm.ShowDialog();
                        //ArticalGroupsBUS arbus = new ArticalGroupsBUS();
                        //modelData = arbus.GetAllArticalGroups(); ;
                        //this.SetDataPersonBinding(modelData);
                    }
                }

            }            
        }

        private void gridArticalGroups_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridArticalGroups.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridArticalGroups.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArticalGroups.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArticalGroups.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArticalGroups.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArticalGroups.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArticalGroups.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridArticalGroups.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridArticalGroups.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridArticalGroups.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridArticalGroups.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridArticalGroups_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridArticalGroups.IsInEditMode && !(this.gridArticalGroups.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridArticalGroups_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridArticalGroups.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridArticalGroups_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridArticalGroups.FilterChanging -= gridArticalGroups_FilterChanging;

                this.gridArticalGroups.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridArticalGroups.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridArticalGroups.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridArticalGroups.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridArticalGroups.FilterChanging += gridArticalGroups_FilterChanging;
            }
        }

        private void gridArticalGroups_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridArticalGroups.EndEdit();
            }
        }


    }

    public class ArticalGroupsSelectedRowchanged : EventArgs
    {
         public ArticalGroupsModel a { get; set; }
    }


}
