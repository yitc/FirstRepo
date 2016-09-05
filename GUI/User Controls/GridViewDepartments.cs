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
using System.Windows.Forms;
using Telerik.WinControls.Data;

namespace GUI
{    
    public partial class GridViewDepartments : System.Windows.Forms.UserControl, IBISGrid
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

        public event EventHandler<DepartmentsSelectedRowchanged> DepartmentsSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(DepartmentsModel art)
        {
            DepartmentsSelectedRowchanged(this, new DepartmentsSelectedRowchanged { a = art });
        }


        DepartmentsBUS DepartmentsBUS;
        private Telerik.WinControls.UI.RadGridView gridDepartments;
        private DepartmentsModel _selectedRowDepartments;
        private DepartmentsModel _clickedDepartments;
        public List<IModel> modelData;
        // Folder u kome cuva filtere za Departments
        private string filterFolder;

        // Folder u kome cuva labele za Departments
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridViewDepartments()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\departments")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\departments"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\departments")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\departments"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\departments");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\departments");
            DepartmentsBUS = new DepartmentsBUS();

            InitializeComponent();        
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridDepartments = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridDepartments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDepartments.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridDepartments
            // 
            this.gridDepartments.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridDepartments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDepartments.EnterKeyMode = Telerik.WinControls.UI.RadGridViewEnterKeyMode.EnterMovesToNextRow;
            this.gridDepartments.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridDepartments.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridDepartments.MasterTemplate.AllowAddNewRow = false;
            this.gridDepartments.MasterTemplate.AllowCellContextMenu = false;
            this.gridDepartments.MasterTemplate.AllowDeleteRow = false;
            this.gridDepartments.MasterTemplate.AllowEditRow = false;
            this.gridDepartments.MasterTemplate.AllowSearchRow = true;
            this.gridDepartments.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridDepartments.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridDepartments.MasterTemplate.EnableFiltering = true;
            this.gridDepartments.MasterTemplate.EnablePaging = true;
            this.gridDepartments.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridDepartments.MasterTemplate.PageSize = 50;
            this.gridDepartments.MasterTemplate.ShowGroupedColumns = true;
            this.gridDepartments.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridDepartments.Name = "gridDepartments";
            this.gridDepartments.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridDepartments.Size = new System.Drawing.Size(150, 150);
            this.gridDepartments.TabIndex = 0;
            this.gridDepartments.Text = "gridDepartments";
            this.gridDepartments.ThemeName = "VisualStudio2012Light";
            this.gridDepartments.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridDepartments_CellBeginEdit);
            this.gridDepartments.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridDepartments_CellEditorInitialized);
            this.gridDepartments.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridDepartments_CellEndEdit);
            this.gridDepartments.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridDepartments_CurrentRowChanged);
            this.gridDepartments.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridDepartments_CellClick);
            this.gridDepartments.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridDepartments_CellDoubleClick);
            this.gridDepartments.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridDepartments_GroupSummaryEvaluate);
            this.gridDepartments.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridDepartments_DataBindingComplete);
            this.gridDepartments.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridDepartments_FilterChanging);
            // 
            // GridViewDepartments
            // 
            this.Controls.Add(this.gridDepartments);
            this.Name = "GridViewDepartments";
            ((System.ComponentModel.ISupportInitialize)(this.gridDepartments.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDepartments)).EndInit();
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
            get { return gridDepartments; }
        }

        public DepartmentsModel SelectedRowDepartments
        {
            get { return _selectedRowDepartments; }
        }
        public DepartmentsModel ClickedDepartments
        {
            get { return _clickedDepartments; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridDepartments.Columns; }
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
            return DepartmentsBUS.GetAllDepartments();  
        }
        public List<FilterModel> ReturnFilters()
        {
            return Login._departmentsFilters;
        }
        public List<LabelModel> ReturnLabels()
        {
            return Login._departmentsLabels;
        }       
      
        public void SetDataPersonBinding(List<IModel> binding)
        {
            this.gridDepartments.DataSource = null;
            this.gridDepartments.DataSource = binding;

        }

        public void ClearDescriptors()
        {
            this.gridDepartments.MasterTemplate.SortDescriptors.Clear();
            this.gridDepartments.MasterTemplate.GroupDescriptors.Clear();
            this.gridDepartments.MasterTemplate.FilterDescriptors.Clear();
        }
        public void SaveLayout(string filename)
        {
            this.gridDepartments.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridDepartments.LoadLayout(filterFolder + "\\" + filename);
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
        private void gridDepartments_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                dictionary.Add(column.HeaderText, column.HeaderText);
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                column.MinWidth = column.Width;
            }
            for (int i = 0; i < gridDepartments.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(gridDepartments.Columns[i].HeaderText) != null)
                        gridDepartments.Columns[i].HeaderText = resxSet.GetString(gridDepartments.Columns[i].HeaderText);
                }

            }
            if (gridDepartments.Columns.Count > 0)
            {
                //for number of rows
                this.gridDepartments.SummaryRowsTop.Clear();
                gridDepartments.MasterTemplate.EnablePaging = false;
                gridDepartments.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridDepartments.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridDepartments.SummaryRowsTop.Add(summaryRowItem);
                GridViewSummaryRowItem summaryRowItem2 = new GridViewSummaryRowItem();
            }
        }

        private void gridDepartments_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridDepartments.MasterTemplate)
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
        private void gridDepartments_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            DepartmentsModel selectedDepartments = new DepartmentsModel();
            GridViewRowInfo info = this.gridDepartments.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedDepartments = (DepartmentsModel)info.DataBoundItem;

                if (selectedDepartments != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedDepartments = selectedDepartments;
                   // frmDepartments frm = new frmDepartments(this._clickedDepartments);

                    //frm.ShowDialog();
                    DepartmentsBUS arbus = new DepartmentsBUS();
                    modelData = arbus.GetAllDepartments(); ;
                    this.SetDataPersonBinding(modelData);  
                }
            }
        }
        private void gridDepartments_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridDepartments.MasterView.TableHeaderRow && e.CurrentRow != gridDepartments.MasterView.TableFilteringRow && e.CurrentRow != gridDepartments.MasterView.TableSearchRow)
            {
                DepartmentsModel selectedDepartments = new DepartmentsModel();
                selectedDepartments = (DepartmentsModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowDepartments = selectedDepartments;
                    RaiseStatusChanged(selectedDepartments);
                }
            }
        }
        #endregion

        private void gridDepartments_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridDepartments.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridDepartments.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridDepartments.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridDepartments.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridDepartments.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridDepartments.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridDepartments.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridDepartments.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridDepartments.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridDepartments.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridDepartments.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridDepartments_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridDepartments.IsInEditMode && !(this.gridDepartments.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridDepartments_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridDepartments.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridDepartments_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridDepartments.FilterChanging -= gridDepartments_FilterChanging;

                this.gridDepartments.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridDepartments.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridDepartments.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridDepartments.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridDepartments.FilterChanging += gridDepartments_FilterChanging;
            }  
        }

        private void gridDepartments_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridDepartments.EndEdit();
            }
        }



    }

    public class DepartmentsSelectedRowchanged : EventArgs
    {
         public DepartmentsModel a { get; set; }
    }


}
