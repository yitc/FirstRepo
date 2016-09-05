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

    public partial class GridViewAccountClass : System.Windows.Forms.UserControl, IBISGrid 
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
        public event EventHandler<ClassStatusSelectedRowchanged> ClassStatusSelectedRowchanged = delegate { };
        public void RaiseStatusChanged(AccLedgerClassModel aclass)
        {
            ClassStatusSelectedRowchanged(this, new ClassStatusSelectedRowchanged { aclass = aclass });
        }

        AccLedgerClassBUS AccLedgerClassBUS;
        private List<IModel> modelData;
        private Telerik.WinControls.UI.RadGridView gridClass;
        private AccLedgerClassModel _selectedRowClass;
        private AccLedgerClassModel _clickedClass;
        private string filterFolder;
        private string labelFolder;
        private bool  _bLoadTreeMenu = false;


        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public GridViewAccountClass()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\accountClass")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\accountClass"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\accountClass")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\accountClass"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username+"\\filters\\custom filters\\accountClass");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\accountClass");
            AccLedgerClassBUS = new AccLedgerClassBUS();

            InitializeComponent();
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridClass = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridClass.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridClass
            // 
            this.gridClass.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridClass.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridClass.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.gridClass.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridClass.MasterTemplate.AllowAddNewRow = false;
            this.gridClass.MasterTemplate.AllowCellContextMenu = false;
            this.gridClass.MasterTemplate.AllowDeleteRow = false;
            this.gridClass.MasterTemplate.AllowEditRow = false;
            this.gridClass.MasterTemplate.AllowSearchRow = true;
            this.gridClass.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridClass.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridClass.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridClass.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridClass.MasterTemplate.EnableFiltering = true;
            this.gridClass.MasterTemplate.EnablePaging = true;
            this.gridClass.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridClass.MasterTemplate.PageSize = 50;
            this.gridClass.MasterTemplate.ShowGroupedColumns = true;
            this.gridClass.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridClass.Name = "gridClass";
            this.gridClass.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridClass.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridClass.Size = new System.Drawing.Size(150, 150);
            this.gridClass.TabIndex = 0;
            this.gridClass.Text = "Ledger Grid";
            this.gridClass.ThemeName = "VisualStudio2012Light";
            this.gridClass.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridClass_CellBeginEdit);
            this.gridClass.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridClass_CellEditorInitialized);
            this.gridClass.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridClass_CellEndEdit);
            this.gridClass.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridClass_CurrentRowChanged);
            this.gridClass.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridClass_CellClick);
            this.gridClass.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridClass_CellDoubleClick);
            this.gridClass.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridClass_GroupSummaryEvaluate);
            this.gridClass.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridClass_DataBindingComplete);
            this.gridClass.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridClass_FilterChanging);
            this.gridClass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridClass_KeyDown);
            // 
            // GridViewAccountClass
            // 
            this.Controls.Add(this.gridClass);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GridViewAccountClass";
            ((System.ComponentModel.ISupportInitialize)(this.gridClass.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridClass)).EndInit();
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
            get { return gridClass; }
        }
        public AccLedgerClassModel SelectedRowPerson
        {
            get { return _selectedRowClass; }
        }
        public AccLedgerClassModel ClickedClass
        {
            get { return _clickedClass; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridClass.Columns; }
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
            return AccLedgerClassBUS.GetAllClass();
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
            this.gridClass.DataSource = null;
            this.gridClass.DataSource = binding;

        }
        public void ClearDescriptors()
        {
            this.gridClass.MasterTemplate.SortDescriptors.Clear();
            this.gridClass.MasterTemplate.GroupDescriptors.Clear();
            this.gridClass.MasterTemplate.FilterDescriptors.Clear();

        }
        public void SaveLayout(string filename)
        {            
            this.gridClass.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridClass.LoadLayout(filterFolder + "\\" + filename);
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
        private void gridClass_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
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
            if (gridClass.Columns.Count > 0)
            {
                //for number of rows
                this.gridClass.SummaryRowsTop.Clear();
                gridClass.MasterTemplate.EnablePaging = false;
                gridClass.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = grid.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridClass.SummaryRowsTop.Add(summaryRowItem);
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

        private void gridClass_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridClass.MasterTemplate)
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
        
        private void gridClass_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.gridClass.CurrentRow;
            AccLedgerClassModel selectedClass = (AccLedgerClassModel)info.DataBoundItem;
            _selectedRowClass = new AccLedgerClassModel();
            _clickedClass = new AccLedgerClassModel();

            //_selectedRowLedger = selectedLedger;

            //EmployeePassportModel selectedPassport = new EmployeePassportModel();
            //EmployeePassportBUS passportBUS = new EmployeePassportBUS();
            //selectedPassport = passportBUS.GetEmpoyeePassport(selectedLedger.idEmployee);

            if (info != null && e.RowIndex >= 0)
                if (selectedClass != null)
                {
                    frmAccClass frm = new frmAccClass(selectedClass);
                 //   frmLedgerAccount frm = new frmLedgerAccount();
                    frm.ShowDialog();
                    modelData = AccLedgerClassBUS.GetAllClass(); ;
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

        private void gridClass_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridClass.MasterView.TableHeaderRow && e.CurrentRow != gridClass.MasterView.TableFilteringRow && e.CurrentRow != gridClass.MasterView.TableSearchRow)
            {
                AccLedgerClassModel selectedClass = new AccLedgerClassModel();
                selectedClass = (AccLedgerClassModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowClass = selectedClass;
                    RaiseStatusChanged(selectedClass);
                }
            }
        }
        #endregion Grid Events

        private void gridClass_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.gridClass.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    AccLedgerClassModel model = (AccLedgerClassModel)this.gridClass.CurrentRow.DataBoundItem;
                    frmAccClass frm = new frmAccClass(model);
                    frm.Show();
                    // e.SuppressKeyPress = true;
                    //e.Handled = true;
                    return;

                }

            }
        }

        private void gridClass_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridClass.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridClass.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridClass.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridClass.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridClass.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridClass.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridClass.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridClass.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridClass.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridClass.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridClass.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridClass.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }            
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridClass_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridClass.IsInEditMode && !(this.gridClass.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridClass_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridClass.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridClass_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridClass.FilterChanging -= gridClass_FilterChanging;

                this.gridClass.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridClass.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridClass.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridClass.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridClass.FilterChanging += gridClass_FilterChanging;
            }  
        }

        private void gridClass_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridClass.EndEdit();
            }
        }




    }
    public class ClassStatusSelectedRowchanged : EventArgs
    {
        public AccLedgerClassModel aclass { get; set; }
      //  public PersonModel person { get; set; }
    }
}
