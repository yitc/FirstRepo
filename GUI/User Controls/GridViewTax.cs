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

    public partial class GridViewTax : System.Windows.Forms.UserControl, IBISGrid 
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
        public event EventHandler<TaxStatusSelectedRowchanged> TaxStatusSelectedRowchanged = delegate { };
        public void RaiseStatusChanged(AccTaxModel atax)
        {
            TaxStatusSelectedRowchanged(this, new TaxStatusSelectedRowchanged { atax = atax });
        }

        AccTaxBUS AccTaxBUS;
        private List<IModel> modelData;
        private Telerik.WinControls.UI.RadGridView gridTax;
        private AccTaxModel _selectedRowTax;
        private AccTaxModel _clickedTax;
        private string filterFolder;
        private string labelFolder;
        private bool  _bLoadTreeMenu = false;


        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public GridViewTax()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\tax")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\tax"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\tax")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\tax"));
            } 
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username+"\\filters\\custom filters\\tax");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\tax");
            AccTaxBUS = new AccTaxBUS();

            InitializeComponent();
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridTax = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridTax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTax.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridTax
            // 
            this.gridTax.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridTax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTax.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gridTax.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridTax.MasterTemplate.AllowAddNewRow = false;
            this.gridTax.MasterTemplate.AllowCellContextMenu = false;
            this.gridTax.MasterTemplate.AllowDeleteRow = false;
            this.gridTax.MasterTemplate.AllowEditRow = false;
            this.gridTax.MasterTemplate.AllowSearchRow = true;
            this.gridTax.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridTax.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridTax.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridTax.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridTax.MasterTemplate.EnableFiltering = true;
            this.gridTax.MasterTemplate.EnablePaging = true;
            this.gridTax.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridTax.MasterTemplate.PageSize = 50;
            this.gridTax.MasterTemplate.ShowGroupedColumns = true;
            this.gridTax.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridTax.Name = "gridTax";
            this.gridTax.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridTax.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridTax.Size = new System.Drawing.Size(150, 150);
            this.gridTax.TabIndex = 0;
            this.gridTax.Text = "Tax Grid";
            this.gridTax.ThemeName = "VisualStudio2012Light";
            this.gridTax.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridTax_CellBeginEdit);
            this.gridTax.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridTax_CellEditorInitialized);
            this.gridTax.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridTax_CellEndEdit);
            this.gridTax.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridTax_CurrentRowChanged);
            this.gridTax.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridTax_CellClick);
            this.gridTax.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridTax_CellDoubleClick);
            this.gridTax.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridTax_GroupSummaryEvaluate);
            this.gridTax.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridTax_DataBindingComplete);
            this.gridTax.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridTax_FilterChanging);
            this.gridTax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridTax_KeyDown);
            // 
            // GridViewTax
            // 
            this.Controls.Add(this.gridTax);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GridViewTax";
            ((System.ComponentModel.ISupportInitialize)(this.gridTax.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTax)).EndInit();
            this.ResumeLayout(false);

        }

        #region GettersSetters
        // getters setters
        public bool bLoadTreeMenu
        {
            get { return _bLoadTreeMenu; }
            set { _bLoadTreeMenu = value; }
        }

        public Telerik.WinControls.UI.RadGridView TaxGridView
        {
            get { return gridTax; }
        }
        public AccTaxModel SelectedRowTax
        {
            get { return _selectedRowTax; }
        }
        public AccTaxModel ClickedTax
        {
            get { return _clickedTax; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridTax.Columns; }
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
            return AccTaxBUS.GetAllTax(Login._user.lngUser);
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
            this.gridTax.DataSource = null;
            this.gridTax.DataSource = binding;
            //LoadLayout("standard.xml");

        }
        public void ClearDescriptors()
        {
            this.gridTax.MasterTemplate.SortDescriptors.Clear();
            this.gridTax.MasterTemplate.GroupDescriptors.Clear();
            this.gridTax.MasterTemplate.FilterDescriptors.Clear();

        }
        public void SaveLayout(string filename)
        {            
            this.gridTax.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridTax.LoadLayout(filterFolder + "\\" + filename);
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
        private void gridTax_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
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
            if (gridTax.Columns.Count > 0)
            {
                //for number of rows
                this.gridTax.SummaryRowsTop.Clear();
                gridTax.MasterTemplate.EnablePaging = false;
                gridTax.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridTax.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridTax.SummaryRowsTop.Add(summaryRowItem);
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

        private void gridTax_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridTax.MasterTemplate)
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
        
        private void gridTax_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.gridTax.CurrentRow;
            AccTaxModel selectedTax = (AccTaxModel)info.DataBoundItem;
            _selectedRowTax = new AccTaxModel();
            _clickedTax = new AccTaxModel();

            //_selectedRowLedger = selectedLedger;

            //EmployeePassportModel selectedPassport = new EmployeePassportModel();
            //EmployeePassportBUS passportBUS = new EmployeePassportBUS();
            //selectedPassport = passportBUS.GetEmpoyeePassport(selectedLedger.idEmployee);

            if (info != null && e.RowIndex >= 0)
                if (selectedTax != null)
                {
                    frmBTW frm = new frmBTW(selectedTax);
                    //   frmLedgerAccount frm = new frmLedgerAccount();
                    frm.ShowDialog();
                    modelData = AccTaxBUS.GetAllTax(Login._user.lngUser);
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

        private void gridTax_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridTax.MasterView.TableHeaderRow && e.CurrentRow != gridTax.MasterView.TableFilteringRow && e.CurrentRow != gridTax.MasterView.TableSearchRow)
            {
                AccTaxModel selectedTax = new AccTaxModel();
                selectedTax = (AccTaxModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowTax = selectedTax;
                    RaiseStatusChanged(selectedTax);
                }
            }
        }
        #endregion Grid Events

        private void gridTax_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.gridTax.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    AccTaxModel model = (AccTaxModel)this.gridTax.CurrentRow.DataBoundItem;
                    frmBTW frm = new frmBTW(model);
                    frm.Show();
                  
                    return;

                }

            }
        }

        private void gridTax_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridTax.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridTax.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridTax.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridTax.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridTax.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridTax.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridTax.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridTax.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }
        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridTax.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridTax.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridTax.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridTax.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridTax_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridTax.IsInEditMode && !(this.gridTax.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridTax_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridTax.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridTax_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridTax.FilterChanging -= gridTax_FilterChanging;

                this.gridTax.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridTax.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridTax.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridTax.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridTax.FilterChanging += gridTax_FilterChanging;
            }  
        }

        private void gridTax_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridTax.EndEdit();
            }
        }






    }
    public class TaxStatusSelectedRowchanged : EventArgs
    {
        public AccTaxModel atax { get; set; }
      //  public PersonModel person { get; set; }
    }
}
