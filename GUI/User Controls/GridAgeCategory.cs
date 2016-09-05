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
    public partial class GridAgeCategory : System.Windows.Forms.UserControl, IBISGrid
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /// 
        public bool isChanged = false;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public event EventHandler<AgeCategoryStatusSelectedRowchanged> AgeCategoryStatusSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(AgeCategoryModel agge)
        {
            AgeCategoryStatusSelectedRowchanged(this, new AgeCategoryStatusSelectedRowchanged { age = agge });
        }


        AgeCategoryBUS ageBUS;
        private Telerik.WinControls.UI.RadGridView gridAgeCategoy;
        private AgeCategoryModel _selectedRowAgeCategory;
        private AgeCategoryModel _clickedAgeCategory;
        
        // Folder u kome cuva filtere za tipove
        private string filterFolder;

        // Folder u kome cuva labele za tipove
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridAgeCategory()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\ageCategory")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\ageCategory"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\ageCategory")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\ageCategory"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\ageCategory");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\ageCategory");
            ageBUS = new AgeCategoryBUS();

           InitializeComponent();        
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridAgeCategoy = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridAgeCategoy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAgeCategoy.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridAgeCategoy
            // 
            this.gridAgeCategoy.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridAgeCategoy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridAgeCategoy.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridAgeCategoy.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridAgeCategoy.MasterTemplate.AllowAddNewRow = false;
            this.gridAgeCategoy.MasterTemplate.AllowCellContextMenu = false;
            this.gridAgeCategoy.MasterTemplate.AllowDeleteRow = false;
            this.gridAgeCategoy.MasterTemplate.AllowEditRow = false;
            this.gridAgeCategoy.MasterTemplate.AllowSearchRow = true;
            this.gridAgeCategoy.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridAgeCategoy.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridAgeCategoy.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridAgeCategoy.MasterTemplate.EnableFiltering = true;
            this.gridAgeCategoy.MasterTemplate.EnablePaging = true;
            this.gridAgeCategoy.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridAgeCategoy.MasterTemplate.PageSize = 50;
            this.gridAgeCategoy.MasterTemplate.ShowGroupedColumns = true;
            this.gridAgeCategoy.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridAgeCategoy.Name = "gridAgeCategoy";
            this.gridAgeCategoy.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridAgeCategoy.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridAgeCategoy.Size = new System.Drawing.Size(150, 150);
            this.gridAgeCategoy.TabIndex = 0;
            this.gridAgeCategoy.Text = "AgeCategory Grid";
            this.gridAgeCategoy.ThemeName = "VisualStudio2012Light";
            this.gridAgeCategoy.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridAgeCategoy_CellBeginEdit);
            this.gridAgeCategoy.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridAgeCategoy_CellEditorInitialized);
            this.gridAgeCategoy.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridAgeCategoy_CellEndEdit);
            this.gridAgeCategoy.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridAgeCategoy_CurrentRowChanged);
            this.gridAgeCategoy.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridAgeCategoy_CellClick);
            this.gridAgeCategoy.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridAgeCategoy_CellDoubleClick);
            this.gridAgeCategoy.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridAgeCategoy_GroupSummaryEvaluate);
            this.gridAgeCategoy.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridAgeCategoy_DataBindingComplete);
            this.gridAgeCategoy.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridAgeCategoy_FilterChanging);
            this.gridAgeCategoy.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridAgeCategoy_KeyDown);
            // 
            // GridAgeCategory
            // 
            this.Controls.Add(this.gridAgeCategoy);
            this.Name = "GridAgeCategory";
            ((System.ComponentModel.ISupportInitialize)(this.gridAgeCategoy.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAgeCategoy)).EndInit();
            this.ResumeLayout(false);

        }

        #region GettersSetters
        //getters setters
        public bool bLoadTreeMenu
        {
            get { return _bLoadTreeMenu; }
            set { _bLoadTreeMenu = value; }
        }
        public Telerik.WinControls.UI.RadGridView AgeCategoryGridView
        {
            get { return gridAgeCategoy; }
        }

        public AgeCategoryModel SelectedRowAgeCategory
        {
            get { return _selectedRowAgeCategory; }
        }
        public AgeCategoryModel ClickedAgeCategory
        {
            get { return _clickedAgeCategory; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridAgeCategoy.Columns; }
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
        public List<IModel> GetData(int selectedFilter, List<int> idLabelList, string lang)
        {
            return ageBUS.GetAllAgeCategory();
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
            this.gridAgeCategoy.DataSource = null;
            this.gridAgeCategoy.DataSource = binding;

        }

        public void ClearDescriptors()
        {
            this.gridAgeCategoy.MasterTemplate.SortDescriptors.Clear();
            this.gridAgeCategoy.MasterTemplate.GroupDescriptors.Clear();
            this.gridAgeCategoy.MasterTemplate.FilterDescriptors.Clear();
        }
        public void removeRow(AgeCategoryModel rw)
        {
            using (gridAgeCategoy.DeferRefresh())
            {
                GridViewRowInfo row = this.gridAgeCategoy.Rows.Where(s => s.Cells["idAgeCategory"].Value.ToString() == rw.idAgeCategory.ToString()).FirstOrDefault();
                if (row != null)
                    this.gridAgeCategoy.Rows.Remove(row);
            }
        }
        public void SaveLayout(string filename)
        {
            this.gridAgeCategoy.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridAgeCategoy.LoadLayout(filterFolder + "\\" + filename);
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

        public TypeModel ReturnRowData(TypeModel data)
        {
            return data;
        }
        #endregion Functions

        #region Grid Events
        // eventi na gridu
        private void gridAgeCategoy_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                dictionary.Add(column.HeaderText, column.HeaderText);
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 136);
                column.MinWidth = column.Width;

            }

            for (int i = 0; i < gridAgeCategoy.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(gridAgeCategoy.Columns[i].HeaderText)!=null)
                    gridAgeCategoy.Columns[i].HeaderText = resxSet.GetString(gridAgeCategoy.Columns[i].HeaderText);
                   
                }



            }
            if (gridAgeCategoy.Columns.Count > 0)
            {
                //for number of rows
                this.gridAgeCategoy.SummaryRowsTop.Clear();
                gridAgeCategoy.MasterTemplate.EnablePaging = false;
                gridAgeCategoy.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridAgeCategoy.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridAgeCategoy.SummaryRowsTop.Add(summaryRowItem);
                GridViewSummaryRowItem summaryRowItem2 = new GridViewSummaryRowItem();
            }

        }

        private void gridAgeCategoy_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
       {
           if (e.Parent == this.gridAgeCategoy.MasterTemplate)
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
        private void gridAgeCategoy_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            AgeCategoryModel selectedAgeCategory = new AgeCategoryModel();
            GridViewRowInfo info = this.gridAgeCategoy.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedAgeCategory = (AgeCategoryModel)info.DataBoundItem;

                if (selectedAgeCategory != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedAgeCategory = selectedAgeCategory;
                    frmAgeCategory frm = new frmAgeCategory(this._clickedAgeCategory);
                    frm.ShowDialog();

                    if (frm.isChanged == true)
                    {
                    AgeCategoryBUS abus = new AgeCategoryBUS();


                    gridAgeCategoy.DataSource = null;
                    gridAgeCategoy.DataSource = abus.GetAllAgeCategory();
                    }
                }
            }
        }
        private void gridAgeCategoy_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridAgeCategoy.MasterView.TableHeaderRow && e.CurrentRow != gridAgeCategoy.MasterView.TableFilteringRow && e.CurrentRow != gridAgeCategoy.MasterView.TableSearchRow)
            {
                AgeCategoryModel selectedAgeCategory = new AgeCategoryModel();
                selectedAgeCategory = (AgeCategoryModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowAgeCategory = selectedAgeCategory;
                    RaiseStatusChanged(selectedAgeCategory);
                }
            }
        }
        #endregion

        private void gridAgeCategoy_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.gridAgeCategoy.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    TypeModel model = (TypeModel)this.gridAgeCategoy.CurrentRow.DataBoundItem;
                    frmType frm = new frmType(model);
                    frm.ShowDialog();
                    return;
                }
            }
        }

        private void gridAgeCategoy_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridAgeCategoy.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridAgeCategoy.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridAgeCategoy.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridAgeCategoy.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridAgeCategoy.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridAgeCategoy.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridAgeCategoy.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridAgeCategoy.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridAgeCategoy.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridAgeCategoy.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridAgeCategoy.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridAgeCategoy.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridAgeCategoy_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridAgeCategoy.IsInEditMode && !(this.gridAgeCategoy.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridAgeCategoy_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridAgeCategoy.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridAgeCategoy_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridAgeCategoy.FilterChanging -= gridAgeCategoy_FilterChanging;

                this.gridAgeCategoy.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridAgeCategoy.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridAgeCategoy.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridAgeCategoy.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridAgeCategoy.FilterChanging += gridAgeCategoy_FilterChanging;
            }  
        }

        private void gridAgeCategoy_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridAgeCategoy.EndEdit();
            }
        }



    }



    public class AgeCategoryStatusSelectedRowchanged : EventArgs
    {
        public AgeCategoryModel age { get; set; }
    }


}
