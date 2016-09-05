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

    public partial class GridViewCost : System.Windows.Forms.UserControl, IBISGrid 
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
        public event EventHandler<CostStatusSelectedRowchanged> CostStatusSelectedRowchanged = delegate { };
        public void RaiseStatusChanged(AccCostModel acost)
        {
            CostStatusSelectedRowchanged(this, new CostStatusSelectedRowchanged { acost = acost });
        }

        AccCostBUS AccCostBUS;
        private List<IModel> modelData;
        private Telerik.WinControls.UI.RadGridView gridCost;
        private AccCostModel _selectedRowCost;
        private AccCostModel _clickedCost;
        private string filterFolder;
        private string labelFolder;
        private bool  _bLoadTreeMenu = false;


        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public GridViewCost()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\costs")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\costs"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\costs")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\costs"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username+"\\filters\\custom filters\\costs");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\costs");
            AccCostBUS = new AccCostBUS();

            InitializeComponent();
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridCost = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridCost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCost.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridCost
            // 
            this.gridCost.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridCost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCost.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.gridCost.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridCost.MasterTemplate.AllowAddNewRow = false;
            this.gridCost.MasterTemplate.AllowCellContextMenu = false;
            this.gridCost.MasterTemplate.AllowDeleteRow = false;
            this.gridCost.MasterTemplate.AllowEditRow = false;
            this.gridCost.MasterTemplate.AllowSearchRow = true;
            this.gridCost.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridCost.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridCost.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridCost.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridCost.MasterTemplate.EnableFiltering = true;
            this.gridCost.MasterTemplate.EnablePaging = true;
            this.gridCost.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridCost.MasterTemplate.PageSize = 50;
            this.gridCost.MasterTemplate.ShowFilterCellOperatorText = false;
            this.gridCost.MasterTemplate.ShowGroupedColumns = true;
            this.gridCost.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridCost.Name = "gridCost";
            this.gridCost.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridCost.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridCost.Size = new System.Drawing.Size(150, 150);
            this.gridCost.TabIndex = 0;
            this.gridCost.Text = "Cost Grid";
            this.gridCost.ThemeName = "VisualStudio2012Light";
            this.gridCost.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridCost_CellBeginEdit);
            this.gridCost.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridCost_CellEditorInitialized);
            this.gridCost.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridCost_CellEndEdit);
            this.gridCost.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridCost_CurrentRowChanged);
            this.gridCost.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridCost_CellClick);
            this.gridCost.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridCost_CellDoubleClick);
            this.gridCost.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridCost_GroupSummaryEvaluate);
            this.gridCost.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridCost_DataBindingComplete);
            this.gridCost.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridCost_FilterChanging);
            this.gridCost.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridCost_KeyDown);
            // 
            // GridViewCost
            // 
            this.Controls.Add(this.gridCost);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GridViewCost";
            ((System.ComponentModel.ISupportInitialize)(this.gridCost.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCost)).EndInit();
            this.ResumeLayout(false);

        }

        #region GettersSetters
        // getters setters
        public bool bLoadTreeMenu
        {
            get { return _bLoadTreeMenu; }
            set { _bLoadTreeMenu = value; }
        }

        public Telerik.WinControls.UI.RadGridView LedgerGridView
        {
            get { return gridCost; }
        }
        public AccCostModel SelectedRowCost
        {
            get { return _selectedRowCost; }
        }
        public AccCostModel ClickedCost
        {
            get { return _clickedCost; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridCost.Columns; }
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
            return AccCostBUS.GetAllCost();
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
            this.gridCost.DataSource = null;
            this.gridCost.DataSource = binding;

        }
        public void ClearDescriptors()
        {
            this.gridCost.MasterTemplate.SortDescriptors.Clear();
            this.gridCost.MasterTemplate.GroupDescriptors.Clear();
            this.gridCost.MasterTemplate.FilterDescriptors.Clear();

        }
        public void SaveLayout(string filename)
        {            
            this.gridCost.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridCost.LoadLayout(filterFolder + "\\" + filename);
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
        private void gridCost_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                dictionary.Add(column.HeaderText, column.HeaderText);
                //column.MaxWidth = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                column.MinWidth = column.Width;
            }
            for (int i = 0; i < grid.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(grid.Columns[i].HeaderText)!=null)
                    grid.Columns[i].HeaderText = resxSet.GetString(grid.Columns[i].HeaderText);
                }

            }

            if (gridCost.Columns.Count > 0)
            {
                //for number of rows
                this.gridCost.SummaryRowsTop.Clear();
                gridCost.MasterTemplate.EnablePaging = false;
                gridCost.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = grid.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridCost.SummaryRowsTop.Add(summaryRowItem);
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
        }

        private void gridCost_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridCost.MasterTemplate)
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
        
        private void gridCost_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.gridCost.CurrentRow;
            AccCostModel selectedCost = (AccCostModel)info.DataBoundItem;
            _selectedRowCost = new AccCostModel();
            _clickedCost = new AccCostModel();

            //_selectedRowLedger = selectedLedger;

            //EmployeePassportModel selectedPassport = new EmployeePassportModel();
            //EmployeePassportBUS passportBUS = new EmployeePassportBUS();
            //selectedPassport = passportBUS.GetEmpoyeePassport(selectedLedger.idEmployee);

            if (info != null && e.RowIndex >= 0)
                if (selectedCost != null)
                {
                    frmCost frm = new frmCost(selectedCost);
                 //   frmLedgerAccount frm = new frmLedgerAccount();
                    frm.ShowDialog();
                    modelData = AccCostBUS.GetAllCost(); 
                    this.SetDataPersonBinding(modelData);  
                   
                }

           // EmployeeModel selectedEmployee = new EmployeeModel();

            
           // PersonNotesModel selectedNotes = null;

           
            //PersonNotesBUS personNotesBUS = new PersonNotesBUS();

           // GridViewRowInfo info = this.gridEmployee.CurrentRow;

            //if (info != null && e.RowIndex >= 0)
            //{
            //    selectedPerson = (PersonModel)info.DataBoundItem;
            
            //    selectedPassport = passportBUS.GetPassport(selectedPerson.idContPers);
            //    selectedNotes = personNotesBUS.GetPersonNotes(selectedPerson.idContPers).FirstOrDefault();
               

            //    if (selectedPerson != null)
            //    {
            //        //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
              //    this._clickedPerson = selectedPerson;

           // frmEmployee frm = new frmEmployee(this._clickedPerson, selectedPassport, selectedNotes);
       //     frmEmployee form = new frmEmployee(this._clickedEmployee);

         //   form.Show();
            //    }
            //}
        }

        private void gridCost_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridCost.MasterView.TableHeaderRow && e.CurrentRow != gridCost.MasterView.TableFilteringRow && e.CurrentRow != gridCost.MasterView.TableSearchRow)
            {
                AccCostModel selectedCost = new AccCostModel();
                selectedCost = (AccCostModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowCost = selectedCost;
                    RaiseStatusChanged(selectedCost);
                }
            }
        }
        #endregion Grid Events

        private void gridCost_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.gridCost.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {                    
                    AccCostModel model = (AccCostModel)this.gridCost.CurrentRow.DataBoundItem;
                    frmCost frm = new frmCost(model);
                    frm.Show();
                    // e.SuppressKeyPress = true;
                    //e.Handled = true;
                    return;

                }

            }
        }

        private void gridCost_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridCost.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridCost.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridCost.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridCost.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridCost.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridCost.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridCost.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }
        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridCost.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridCost.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridCost.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridCost.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }
        private FilterDescriptor lastFilterDescriptor;

        private void gridCost_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridCost.IsInEditMode && !(this.gridCost.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridCost_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridCost.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridCost_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridCost.FilterChanging -= gridCost_FilterChanging;

                this.gridCost.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridCost.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridCost.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridCost.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridCost.FilterChanging += gridCost_FilterChanging;
            }  
        }

        private void gridCost_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridCost.EndEdit();
            }
        }





    }
    public class CostStatusSelectedRowchanged : EventArgs
    {
        public AccCostModel acost { get; set; }
      //  public PersonModel person { get; set; }
    }
}
