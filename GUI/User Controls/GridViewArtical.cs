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
    public partial class GridViewArtical : System.Windows.Forms.UserControl, IBISGrid
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

        public event EventHandler<ArticalSelectedRowchanged> ArticalSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(ArticalModel art)
        {
            ArticalSelectedRowchanged(this, new ArticalSelectedRowchanged { a = art });
        }


        ArticalBUS ArticalBUS;
        private Telerik.WinControls.UI.RadGridView gridArtical;
        private ArticalModel _selectedRowArtical;
        private ArticalModel _clickedArtical;
        public List<IModel> modelData;
        // Folder u kome cuva filtere za Artical
        private string filterFolder;

        // Folder u kome cuva labele za Artical
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridViewArtical()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\artical")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\artical"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\artical")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\artical"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\artical");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\artical");
            ArticalBUS = new ArticalBUS();

            InitializeComponent();        
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridArtical = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridArtical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridArtical.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridArtical
            // 
            this.gridArtical.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridArtical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridArtical.EnableFastScrolling = true;
            this.gridArtical.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridArtical.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridArtical.MasterTemplate.AllowAddNewRow = false;
            this.gridArtical.MasterTemplate.AllowCellContextMenu = false;
            this.gridArtical.MasterTemplate.AllowDeleteRow = false;
            this.gridArtical.MasterTemplate.AllowEditRow = false;
            this.gridArtical.MasterTemplate.AllowSearchRow = true;
            this.gridArtical.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridArtical.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridArtical.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridArtical.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridArtical.MasterTemplate.EnableFiltering = true;
            this.gridArtical.MasterTemplate.EnablePaging = true;
            this.gridArtical.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridArtical.MasterTemplate.PageSize = 50;
            this.gridArtical.MasterTemplate.ShowFilterCellOperatorText = false;
            this.gridArtical.MasterTemplate.ShowGroupedColumns = true;
            this.gridArtical.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridArtical.Name = "gridArtical";
            this.gridArtical.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridArtical.Size = new System.Drawing.Size(150, 150);
            this.gridArtical.TabIndex = 0;
            this.gridArtical.Text = "gridArtical";
            this.gridArtical.ThemeName = "VisualStudio2012Light";
            this.gridArtical.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridArtical_CellBeginEdit);
            this.gridArtical.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridArtical_CellEditorInitialized);
            this.gridArtical.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridArtical_CellEndEdit);
            this.gridArtical.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridArtical_CurrentRowChanged);
            this.gridArtical.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridArtical_CellClick);
            this.gridArtical.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridArtical_CellDoubleClick);
            this.gridArtical.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridArtical_GroupSummaryEvaluate);
            this.gridArtical.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridArtical_DataBindingComplete);
            this.gridArtical.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridArtical_FilterChanging);
            this.gridArtical.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridArtical_KeyDown);
            // 
            // GridViewArtical
            // 
            this.Controls.Add(this.gridArtical);
            this.Name = "GridViewArtical";
            ((System.ComponentModel.ISupportInitialize)(this.gridArtical.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridArtical)).EndInit();
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
            get { return gridArtical; }
        }

        public ArticalModel SelectedRowArtical
        {
            get { return _selectedRowArtical; }
        }
        public ArticalModel ClickedArtical
        {
            get { return _clickedArtical; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridArtical.Columns; }
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
            return ArticalBUS.GetAllArticals();  
        }
        public List<FilterModel> ReturnFilters()
        {
            return Login._articalFilters;
        }
        public List<LabelModel> ReturnLabels()
        {
            return Login._articalLabels;
        }       
      
        public void SetDataPersonBinding(List<IModel> binding)
        {
            this.gridArtical.DataSource = null;
            this.gridArtical.DataSource = binding;

        }

        public void ClearDescriptors()
        {
            this.gridArtical.MasterTemplate.SortDescriptors.Clear();
            this.gridArtical.MasterTemplate.GroupDescriptors.Clear();
            this.gridArtical.MasterTemplate.FilterDescriptors.Clear();
        }
        public void SaveLayout(string filename)
        {
            this.gridArtical.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridArtical.LoadLayout(filterFolder + "\\" + filename);
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
        private void gridArtical_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                dictionary.Add(column.HeaderText, column.HeaderText);
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                column.MinWidth = column.Width;
            }
            for (int i = 0; i < gridArtical.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(gridArtical.Columns[i].HeaderText) != null)
                        gridArtical.Columns[i].HeaderText = resxSet.GetString(gridArtical.Columns[i].HeaderText);
                }

            }
            if (gridArtical.Columns.Count > 0)
            {
                //for number of rows
                this.gridArtical.SummaryRowsTop.Clear();
                gridArtical.MasterTemplate.EnablePaging = false;
                gridArtical.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridArtical.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridArtical.SummaryRowsTop.Add(summaryRowItem);
                GridViewSummaryRowItem summaryRowItem2 = new GridViewSummaryRowItem();

                grid.Columns["idUserCreated"].IsVisible = false;
                grid.Columns["nameUserCreated"].IsVisible = false;
                grid.Columns["dtUserCreated"].IsVisible = false;
                grid.Columns["idUserModifies"].IsVisible = false;
                grid.Columns["nameUserModified"].IsVisible = false;
                grid.Columns["dtUserModified"].IsVisible = false;
            }
        }

        private void gridArtical_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridArtical.MasterTemplate)
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
        private void gridArtical_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            ArticalModel selectedArtical = new ArticalModel();
            GridViewRowInfo info = this.gridArtical.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedArtical = (ArticalModel)info.DataBoundItem;

                if (selectedArtical != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedArtical = selectedArtical;
                    frmArticle frm = new frmArticle(this._clickedArtical);

                    frm.ShowDialog();
                    //ArticalBUS arbus = new ArticalBUS();
                    //modelData = arbus.GetAllArticals(); 
                    //this.SetDataPersonBinding(modelData);  
                }
            }
        }
        private void gridArtical_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridArtical.MasterView.TableHeaderRow && e.CurrentRow != gridArtical.MasterView.TableFilteringRow && e.CurrentRow != gridArtical.MasterView.TableSearchRow)
            {
                ArticalModel selectedArtical = new ArticalModel();
                selectedArtical = (ArticalModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowArtical = selectedArtical;
                    RaiseStatusChanged(selectedArtical);
                }
            }
        }
        #endregion

        private void gridArtical_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {            
            if (e.KeyData == Keys.Enter || e.KeyData == Keys.Return)
            {
                ArticalModel selectedArtical = new ArticalModel();
                GridViewRowInfo info = this.gridArtical.CurrentRow;

                if (info != null && info.Index >= 0)
                {
                    selectedArtical = (ArticalModel)info.DataBoundItem;
                    if (selectedArtical != null)
                    {                        
                        this._clickedArtical = selectedArtical;
                        frmArticle frm = new frmArticle(this._clickedArtical);

                        frm.ShowDialog();
                       // ArticalBUS arbus = new ArticalBUS();
                        //modelData = arbus.GetAllArticals();
                        //this.SetDataPersonBinding(modelData);
                    }
                }
            }            
        }

        private void gridArtical_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridArtical.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridArtical.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArtical.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArtical.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArtical.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArtical.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArtical.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridArtical.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridArtical.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridArtical.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridArtical.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }         
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridArtical_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridArtical.IsInEditMode && !(this.gridArtical.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridArtical_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridArtical.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridArtical_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridArtical.FilterChanging -= gridArtical_FilterChanging;

                this.gridArtical.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridArtical.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridArtical.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridArtical.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridArtical.FilterChanging += gridArtical_FilterChanging;
            }  
        }

        private void gridArtical_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridArtical.EndEdit();
            }
        }

      

    }

    public class ArticalSelectedRowchanged : EventArgs
    {
         public ArticalModel a { get; set; }
    }


}
