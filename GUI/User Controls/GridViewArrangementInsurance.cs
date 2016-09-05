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

    public partial class GridViewArrangementInsurance : System.Windows.Forms.UserControl, IBISGrid 
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
        public event EventHandler<ArrangementInsuranceStatusSelectedRowchanged> ArrangementInsuranceStatusSelectedRowchanged = delegate { };
        public void RaiseStatusChanged(ArrangementInsuranceModel aArrangementInsurance)
        {
            ArrangementInsuranceStatusSelectedRowchanged(this, new ArrangementInsuranceStatusSelectedRowchanged { aArrangementInsurance = aArrangementInsurance });
        }

        ArrangementInsuranceBUS arrangementInsuranceBUS;
        private List<IModel> modelData;
        private Telerik.WinControls.UI.RadGridView gridArrangementInsurance;
        private ArrangementInsuranceModel _selectedRowArrangementInsurance;
        private ArrangementInsuranceModel _clickedArrangementInsurance;
        private string filterFolder;
        private string labelFolder;
        private bool  _bLoadTreeMenu = false;


        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public GridViewArrangementInsurance()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\ArrangementInsurance")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\ArrangementInsurance"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\ArrangementInsurance")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\ArrangementInsurance"));
            } 
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username+"\\filters\\custom filters\\ArrangementInsurance");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\ArrangementInsurance");
            arrangementInsuranceBUS = new ArrangementInsuranceBUS();

            InitializeComponent();
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridArrangementInsurance = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridArrangementInsurance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridArrangementInsurance.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridArrangementInsurance
            // 
            this.gridArrangementInsurance.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridArrangementInsurance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridArrangementInsurance.EnableFastScrolling = true;
            this.gridArrangementInsurance.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gridArrangementInsurance.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridArrangementInsurance.MasterTemplate.AllowAddNewRow = false;
            this.gridArrangementInsurance.MasterTemplate.AllowCellContextMenu = false;
            this.gridArrangementInsurance.MasterTemplate.AllowDeleteRow = false;
            this.gridArrangementInsurance.MasterTemplate.AllowEditRow = false;
            this.gridArrangementInsurance.MasterTemplate.AllowSearchRow = true;
            this.gridArrangementInsurance.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridArrangementInsurance.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridArrangementInsurance.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridArrangementInsurance.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridArrangementInsurance.MasterTemplate.EnableFiltering = true;
            this.gridArrangementInsurance.MasterTemplate.EnablePaging = true;
            this.gridArrangementInsurance.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridArrangementInsurance.MasterTemplate.PageSize = 50;
            this.gridArrangementInsurance.MasterTemplate.ShowGroupedColumns = true;
            this.gridArrangementInsurance.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridArrangementInsurance.Name = "gridArrangementInsurance";
            this.gridArrangementInsurance.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridArrangementInsurance.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridArrangementInsurance.Size = new System.Drawing.Size(150, 150);
            this.gridArrangementInsurance.TabIndex = 0;
            this.gridArrangementInsurance.Text = "ArrangementInsurance Grid";
            this.gridArrangementInsurance.ThemeName = "VisualStudio2012Light";
            this.gridArrangementInsurance.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridArrangementInsurance_CellBeginEdit);
            this.gridArrangementInsurance.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridArrangementInsurance_CellEditorInitialized);
            this.gridArrangementInsurance.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridArrangementInsurance_CellEndEdit);
            this.gridArrangementInsurance.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridArrangementInsurance_CurrentRowChanged);
            this.gridArrangementInsurance.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridArrangementInsurance_CellClick);
            this.gridArrangementInsurance.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridArrangementInsurance_CellDoubleClick);
            this.gridArrangementInsurance.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridArrangementInsurance_GroupSummaryEvaluate);
            this.gridArrangementInsurance.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridArrangementInsurance_DataBindingComplete);
            this.gridArrangementInsurance.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridArrangementInsurance_FilterChanging);
            this.gridArrangementInsurance.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridArrangementInsurance_KeyDown);
            // 
            // GridViewArrangementInsurance
            // 
            this.Controls.Add(this.gridArrangementInsurance);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GridViewArrangementInsurance";
            ((System.ComponentModel.ISupportInitialize)(this.gridArrangementInsurance.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridArrangementInsurance)).EndInit();
            this.ResumeLayout(false);

        }

        #region GettersSetters
        // getters setters
        public bool bLoadTreeMenu
        {
            get { return _bLoadTreeMenu; }
            set { _bLoadTreeMenu = value; }
        }

        public Telerik.WinControls.UI.RadGridView ArrangementInsuranceGridView
        {
            get { return gridArrangementInsurance; }
        }
        public ArrangementInsuranceModel SelectedRowArrangementInsurance
        {
            get { return _selectedRowArrangementInsurance; }
        }
        public ArrangementInsuranceModel ClickedArrangementInsurance
        {
            get { return _clickedArrangementInsurance; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridArrangementInsurance.Columns; }
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
            return arrangementInsuranceBUS.GetAllArrangementInsurance();
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
            this.gridArrangementInsurance.DataSource = null;
            this.gridArrangementInsurance.DataSource = binding;            
        }
        public void ClearDescriptors()
        {
            this.gridArrangementInsurance.MasterTemplate.SortDescriptors.Clear();
            this.gridArrangementInsurance.MasterTemplate.GroupDescriptors.Clear();
            this.gridArrangementInsurance.MasterTemplate.FilterDescriptors.Clear();

        }

        public void removeRow(ArrangementInsuranceModel rw)
        {
            using (gridArrangementInsurance.DeferRefresh())
            {
                GridViewRowInfo row = this.gridArrangementInsurance.Rows.Where(s => s.Cells["idInsurance"].Value.ToString() == rw.idInsurance.ToString()).FirstOrDefault();
                if (row != null)
                    this.gridArrangementInsurance.Rows.Remove(row);
            }
        }
        public void SaveLayout(string filename)
        {            
            this.gridArrangementInsurance.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridArrangementInsurance.LoadLayout(filterFolder + "\\" + filename);
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
        private void gridArrangementInsurance_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
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
            //if (gridArrangementInsurance.Columns.Count > 0)
            //{
            //    //for number of rows
            //    this.gridArrangementInsurance.SummaryRowsTop.Clear();
            //    gridArrangementInsurance.MasterTemplate.EnablePaging = false;
            //    gridArrangementInsurance.MasterTemplate.ShowTotals = true;
            //    GridViewSummaryItem summaryItem = new GridViewSummaryItem();
            //    summaryItem.Name = gridArrangementInsurance.Columns[0].Name;
            //    summaryItem.Aggregate = GridAggregateFunction.Count;

            //    GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
            //    summaryRowItem.Add(summaryItem);
            //    this.gridArrangementInsurance.SummaryRowsTop.Add(summaryRowItem);
            //    GridViewSummaryRowItem summaryRowItem2 = new GridViewSummaryRowItem();
            //}


            if (gridArrangementInsurance.Columns != null)
            {
                if (gridArrangementInsurance.RowCount > 0)
                {
                    this.gridArrangementInsurance.Columns["dtValidFrom"].FormatString = "{0: dd-MM-yyyy}";
                    this.gridArrangementInsurance.Columns["dtValidTo"].FormatString = "{0: dd-MM-yyyy}";


                }
            }
        }

        private void gridArrangementInsurance_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            //if (e.Parent == this.gridArrangementInsurance.MasterTemplate)
            //{
            //    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            //    {
            //        if (resxSet.GetString("Total") != null)
            //            e.FormatString = String.Format(resxSet.GetString("Total") + " " + e.Value, e.Value);
            //        else
            //            e.FormatString = String.Format("Total " + e.Value, e.Value);
            //    }
            //}
        }
        
        private void gridArrangementInsurance_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.gridArrangementInsurance.CurrentRow;
            ArrangementInsuranceModel selectedArrangementInsurance = (ArrangementInsuranceModel)info.DataBoundItem;
            _selectedRowArrangementInsurance = new ArrangementInsuranceModel();
            _clickedArrangementInsurance = new ArrangementInsuranceModel();

            //_selectedRowLedger = selectedLedger;

            //EmployeePassportModel selectedPassport = new EmployeePassportModel();
            //EmployeePassportBUS passportBUS = new EmployeePassportBUS();
            //selectedPassport = passportBUS.GetEmpoyeePassport(selectedLedger.idEmployee);

            if (info != null && e.RowIndex >= 0)
                if (selectedArrangementInsurance != null)
                {                    
                    frmArrangementInsurance frm = new frmArrangementInsurance(selectedArrangementInsurance);
                    frm.ShowDialog();
                    modelData = arrangementInsuranceBUS.GetAllArrangementInsurance();
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

        private void gridArrangementInsurance_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridArrangementInsurance.MasterView.TableHeaderRow && e.CurrentRow != gridArrangementInsurance.MasterView.TableFilteringRow && e.CurrentRow != gridArrangementInsurance.MasterView.TableSearchRow)
            {
                ArrangementInsuranceModel selectedArrangementInsurance = new ArrangementInsuranceModel();
                selectedArrangementInsurance = (ArrangementInsuranceModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowArrangementInsurance = selectedArrangementInsurance;
                    RaiseStatusChanged(selectedArrangementInsurance);
                }
            }
        }
        #endregion Grid Events

        private void gridArrangementInsurance_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.gridArrangementInsurance.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    ArrangementInsuranceModel model = (ArrangementInsuranceModel)this.gridArrangementInsurance.CurrentRow.DataBoundItem;                    
                    frmArrangementInsurance frm = new frmArrangementInsurance(model);
                    frm.ShowDialog();

                    modelData = arrangementInsuranceBUS.GetAllArrangementInsurance();
                    this.SetDataPersonBinding(modelData);
                }
            }
        }

        private void gridArrangementInsurance_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridArrangementInsurance.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridArrangementInsurance.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArrangementInsurance.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArrangementInsurance.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArrangementInsurance.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArrangementInsurance.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArrangementInsurance.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArrangementInsurance.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }
        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridArrangementInsurance.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridArrangementInsurance.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridArrangementInsurance.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridArrangementInsurance.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridArrangementInsurance_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridArrangementInsurance.IsInEditMode && !(this.gridArrangementInsurance.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridArrangementInsurance_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridArrangementInsurance.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridArrangementInsurance_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridArrangementInsurance.FilterChanging -= gridArrangementInsurance_FilterChanging;

                this.gridArrangementInsurance.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridArrangementInsurance.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridArrangementInsurance.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridArrangementInsurance.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridArrangementInsurance.FilterChanging += gridArrangementInsurance_FilterChanging;
            }  
        }

        private void gridArrangementInsurance_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridArrangementInsurance.EndEdit();
            }
        }



    }
    public class ArrangementInsuranceStatusSelectedRowchanged : EventArgs
    {
        public ArrangementInsuranceModel aArrangementInsurance { get; set; }
      //  public PersonModel person { get; set; }
    }
}
