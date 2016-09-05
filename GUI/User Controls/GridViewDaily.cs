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

    public partial class GridViewDaily : System.Windows.Forms.UserControl, IBISGrid 
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
        public event EventHandler<DailyStatusSelectedRowchanged> DailyStatusSelectedRowchanged = delegate { };
        public void RaiseStatusChanged(AccDailyModel adaily)
        {
            DailyStatusSelectedRowchanged(this, new DailyStatusSelectedRowchanged { adaily = adaily });
        }

        AccDailyBUS AccDailyBUS;
        private List<IModel> modelData;
        private Telerik.WinControls.UI.RadGridView gridDaily;
        private AccDailyModel _selectedRowDaily;
        private AccDailyModel _clickedDaily;
        private string filterFolder;
        private string labelFolder;
        private bool  _bLoadTreeMenu = false;


        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public GridViewDaily()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\daily")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\daily"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\daily")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\daily"));
            } 
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username+"\\filters\\custom filters\\daily");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\daily");
            AccDailyBUS = new AccDailyBUS(Login._bookyear);

            InitializeComponent();
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridDaily = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridDaily)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDaily.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridDaily
            // 
            this.gridDaily.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridDaily.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDaily.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gridDaily.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridDaily.MasterTemplate.AllowAddNewRow = false;
            this.gridDaily.MasterTemplate.AllowCellContextMenu = false;
            this.gridDaily.MasterTemplate.AllowDeleteRow = false;
            this.gridDaily.MasterTemplate.AllowEditRow = false;
            this.gridDaily.MasterTemplate.AllowSearchRow = true;
            this.gridDaily.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridDaily.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridDaily.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridDaily.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridDaily.MasterTemplate.EnableFiltering = true;
            this.gridDaily.MasterTemplate.EnablePaging = true;
            this.gridDaily.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridDaily.MasterTemplate.PageSize = 50;
            this.gridDaily.MasterTemplate.ShowFilterCellOperatorText = false;
            this.gridDaily.MasterTemplate.ShowGroupedColumns = true;
            this.gridDaily.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridDaily.Name = "gridDaily";
            this.gridDaily.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridDaily.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridDaily.Size = new System.Drawing.Size(150, 150);
            this.gridDaily.TabIndex = 0;
            this.gridDaily.Text = "Daily Grid";
            this.gridDaily.ThemeName = "VisualStudio2012Light";
            this.gridDaily.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridDaily_CellBeginEdit);
            this.gridDaily.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridDaily_CellEditorInitialized);
            this.gridDaily.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridDaily_CellEndEdit);
            this.gridDaily.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridDaily_CurrentRowChanged);
            this.gridDaily.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridDaily_CellClick);
            this.gridDaily.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridDaily_CellDoubleClick);
            this.gridDaily.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridDaily_GroupSummaryEvaluate);
            this.gridDaily.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridDaily_DataBindingComplete);
            this.gridDaily.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridDaily_FilterChanging);
            this.gridDaily.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridDaily_KeyDown);
            // 
            // GridViewDaily
            // 
            this.Controls.Add(this.gridDaily);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GridViewDaily";
            ((System.ComponentModel.ISupportInitialize)(this.gridDaily.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDaily)).EndInit();
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
            get { return gridDaily; }
        }
        public AccDailyModel SelectedRowDaily
        {
            get { return _selectedRowDaily; }
        }
        public AccDailyModel ClickedTax
        {
            get { return _clickedDaily; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridDaily.Columns; }
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
            AccDailyBUS ams = new AccDailyBUS(Login._bookyear);
            List<IModel> adm = new List<IModel>();
            adm = ams.GetAllDailys();
            // modelData = ams.GetAllDailys();
            this.SetDataPersonBinding1(adm);
            return adm; // AccDailyBUS.GetAllDailys();
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
            this.gridDaily.DataSource = null;
            //LoadLayout("standard.xml");
            this.gridDaily.DataSource = binding;

        }
        public void SetDataPersonBinding1(List<IModel> binding)
        {
            this.gridDaily.DataSource = null;
            //LoadLayout("standard.xml");
            this.gridDaily.DataSource = binding;

        }
        public void ClearDescriptors()
        {
            this.gridDaily.MasterTemplate.SortDescriptors.Clear();
            this.gridDaily.MasterTemplate.GroupDescriptors.Clear();
            this.gridDaily.MasterTemplate.FilterDescriptors.Clear();

        }
        public void SaveLayout(string filename)
        {            
            this.gridDaily.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridDaily.LoadLayout(filterFolder + "\\" + filename);
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
        private void gridDaily_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            if (gridDaily != null)
            {
                if (gridDaily.ColumnCount > 0)
                {

                    gridDaily.Columns["codeDaily"].IsVisible = false;
                    gridDaily.Columns["nameBank"].IsVisible = false;
                    gridDaily.Columns["idBank"].IsVisible = false;
                    gridDaily.Columns["idDailyType"].IsVisible = false;

                }
            }

            if (gridDaily != null)
            {
                if (gridDaily.Columns.Count > 0)
                {
                    var grid = sender as RadGridView;
                    foreach (var column in grid.Columns)
                    {
                        dictionary.Add(column.HeaderText, column.HeaderText);
                        //column.MaxWidth = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                        column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                        column.MinWidth = column.Width;
                    }
                    for (int i = 0; i < gridDaily.Columns.Count; i++)
                    {

                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString(gridDaily.Columns[i].HeaderText) != null)
                                //              grid.Columns[i].HeaderText = resxSet.GetString(grid.Columns[i].HeaderText);
                                gridDaily.Columns[i].HeaderText = resxSet.GetString(gridDaily.Columns[i].HeaderText);
                        }
                        if (gridDaily.Columns[i].GetType() == typeof(GridViewDateTimeColumn))
                        {
                            if (gridDaily.Columns[i].Name.ToLower() != "dtModified".ToLower() && gridDaily.Columns[i].Name.ToLower() != "dtCreated".ToLower())
                            {
                                gridDaily.Columns[i].FormatString = "{0: dd-MM-yyyy}";
                            }
                        }

                    }
                }
            }

            if (gridDaily != null)
            {
                if (gridDaily.Columns.Count > 0)
                {
                    //for number of rows
                    this.gridDaily.SummaryRowsTop.Clear();
                    gridDaily.MasterTemplate.EnablePaging = false;
                    gridDaily.MasterTemplate.ShowTotals = true;
                    GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                    summaryItem.Name = gridDaily.Columns[0].Name;
                    summaryItem.Aggregate = GridAggregateFunction.Count;

                    GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                    summaryRowItem.Add(summaryItem);
                    this.gridDaily.SummaryRowsTop.Add(summaryRowItem);
                    GridViewSummaryRowItem summaryRowItem2 = new GridViewSummaryRowItem();
                }
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

        private void gridDaily_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridDaily.MasterTemplate)
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
        
        private void gridDaily_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.gridDaily.CurrentRow;
            AccDailyModel selectedDaily = (AccDailyModel)info.DataBoundItem;
            _selectedRowDaily = new AccDailyModel();
            _clickedDaily = new AccDailyModel();
            _selectedRowDaily = selectedDaily;

            //_selectedRowLedger = selectedLedger;

            //EmployeePassportModel selectedPassport = new EmployeePassportModel();
            //EmployeePassportBUS passportBUS = new EmployeePassportBUS();
            //selectedPassport = passportBUS.GetEmpoyeePassport(selectedLedger.idEmployee);

            if (info != null && e.RowIndex >= 0)
                if (selectedDaily != null)
                {
                    frmDaily frm = new frmDaily(selectedDaily);
                 //   frmLedgerAccount frm = new frmLedgerAccount();
                    frm.ShowDialog();
                    AccDailyBUS ams = new AccDailyBUS(Login._bookyear);
                    List<IModel> adm = new List<IModel>();
                    adm = ams.GetAllDailys();
                   // modelData = ams.GetAllDailys();
                    this.SetDataPersonBinding1(adm);  
                   
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

        private void gridDaily_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridDaily.MasterView.TableHeaderRow && e.CurrentRow != gridDaily.MasterView.TableFilteringRow && e.CurrentRow != gridDaily.MasterView.TableSearchRow)
            {
                AccDailyModel selectedDaily = new AccDailyModel();
                selectedDaily = (AccDailyModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowDaily = selectedDaily;
                    RaiseStatusChanged(selectedDaily);
                }
            }
        }
        #endregion Grid Events

        private void gridDaily_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(this.gridDaily.CurrentRow.DataBoundItem !=null)
            {
                if(e.KeyData==Keys.Enter)
                {
                    AccDailyModel model = (AccDailyModel)this.gridDaily.CurrentRow.DataBoundItem;
                    frmDaily frm = new frmDaily(model);
                    frm.Show();
                    return;
                }
            }
        }

        private void gridDaily_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridDaily.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridDaily.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridDaily.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridDaily.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridDaily.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridDaily.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridDaily.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridDaily.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridDaily.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridDaily.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridDaily.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
            //else if(e.KeyData == Keys.Enter || e.KeyData == Keys.Return)
            //{
            //    if (this.gridArrangement.CurrentRow != null && this.gridArrangement.CurrentRow is GridViewFilteringRowInfo && this.gridArrangement.IsInEditMode)
            //    {
            //        this.gridArrangement.EndEdit();
            //    }
            //}
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridDaily_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridDaily.IsInEditMode && !(this.gridDaily.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridDaily_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridDaily.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridDaily_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridDaily.FilterChanging -= gridDaily_FilterChanging;

                this.gridDaily.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridDaily.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridDaily.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridDaily.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridDaily.FilterChanging += gridDaily_FilterChanging;
            }  
        }

        private void gridDaily_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridDaily.EndEdit();
            }
        }



    }
    public class DailyStatusSelectedRowchanged : EventArgs
    {
        public AccDailyModel adaily { get; set; }
      //  public PersonModel person { get; set; }
    }
}
