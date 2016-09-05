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
using NUnit.Framework;
using Telerik.WinControls.Themes;
using GUI.User_Controls;
using Telerik.WinControls.Data;



namespace GUI
{
     [TestFixture]
    public partial class GridViewLedger : System.Windows.Forms.UserControl, IBISGrid 
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
        public event EventHandler<LedgerStatusSelectedRowchanged> LedgerStatusSelectedRowchanged = delegate { };
        public void RaiseStatusChanged(LedgerAccountModel ledger)
        {
            LedgerStatusSelectedRowchanged(this, new LedgerStatusSelectedRowchanged { ledger = ledger });
        }
        
        LedgerAccountBUS LedgerAccountBUS;
        private List<IModel> modelData;
        private Telerik.WinControls.UI.RadGridView gridLedger;
        private LedgerAccountModel _selectedRowLedger;
        private LedgerAccountModel _clickedLedger;
        private string filterFolder;
        private string labelFolder;
        private bool  _bLoadTreeMenu = false;


        Dictionary<string, string> dictionary = new Dictionary<string, string>();                
        
        public GridViewLedger()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\ledger")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\ledger"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\ledger")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\ledger"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\ledger");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\ledger");
            LedgerAccountBUS = new LedgerAccountBUS(Login._bookyear);

            InitializeComponent();

           
        }

 [Test]
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridLedger = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLedger.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridLedger
            // 
            this.gridLedger.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridLedger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLedger.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.gridLedger.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridLedger.MasterTemplate.AllowAddNewRow = false;
            this.gridLedger.MasterTemplate.AllowCellContextMenu = false;
            this.gridLedger.MasterTemplate.AllowDeleteRow = false;
            this.gridLedger.MasterTemplate.AllowEditRow = false;
            this.gridLedger.MasterTemplate.AllowSearchRow = true;
            this.gridLedger.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridLedger.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridLedger.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridLedger.MasterTemplate.EnableFiltering = true;
            this.gridLedger.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridLedger.MasterTemplate.PageSize = 50;
            this.gridLedger.MasterTemplate.ShowFilterCellOperatorText = false;
            this.gridLedger.MasterTemplate.ShowGroupedColumns = true;
            this.gridLedger.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridLedger.Name = "gridLedger";
            this.gridLedger.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridLedger.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridLedger.Size = new System.Drawing.Size(150, 150);
            this.gridLedger.TabIndex = 0;
            this.gridLedger.Text = "Ledger Grid";
            this.gridLedger.ThemeName = "VisualStudio2012Light";
            this.gridLedger.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridLedger_CellBeginEdit);
            this.gridLedger.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridLedger_CellEditorInitialized);
            this.gridLedger.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridLedger_CellEndEdit);
            this.gridLedger.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridLedger_CurrentRowChanged);
            this.gridLedger.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridLedger_CellClick);
            this.gridLedger.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridLedger_CellDoubleClick);
            this.gridLedger.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridLedger_GroupSummaryEvaluate);
            this.gridLedger.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridLedger_DataBindingComplete);
            this.gridLedger.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridLedger_FilterChanging);
            this.gridLedger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridLedger_KeyDown);
            // 
            // GridViewLedger
            // 
            this.Controls.Add(this.gridLedger);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GridViewLedger";
            ((System.ComponentModel.ISupportInitialize)(this.gridLedger.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLedger)).EndInit();
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
            get { return gridLedger; }
        }
        public LedgerAccountModel SelectedRowPerson
        {
            get { return _selectedRowLedger; }
        }
        public LedgerAccountModel ClickedLedger
        {
            get { return _clickedLedger; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridLedger.Columns; }
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
           return LedgerAccountBUS.GetAllAccounts();
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

            this.gridLedger.DataSource = null;
            //LoadLayout("standard.xml");

            //gridLedger.BeginUpdate();

            //foreach(LedgerAccountModel am in binding)
            //{
            //    this.gridLedger.Rows.Add(am);
            //}

            //gridLedger.EndUpdate();
            this.gridLedger.DataSource = binding;


        }
        public void ClearDescriptors()
        {
            this.gridLedger.MasterTemplate.SortDescriptors.Clear();
            this.gridLedger.MasterTemplate.GroupDescriptors.Clear();
            this.gridLedger.MasterTemplate.FilterDescriptors.Clear();

        }
        public void SaveLayout(string filename)
        {            
            this.gridLedger.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridLedger.LoadLayout(filterFolder + "\\" + filename);
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
        private void gridLedger_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
           
            dictionary.Clear();
            if (gridLedger != null)
            {
                if (gridLedger.Columns.Count > 0)
                {
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
                            if (resxSet.GetString(grid.Columns[i].HeaderText) != null)
                                grid.Columns[i].HeaderText = resxSet.GetString(grid.Columns[i].HeaderText);
                        }

                    }
                }
            }

            if (gridLedger != null)
            {
                if (gridLedger.Columns.Count > 0)
                {
                    //for number of rows
                    this.gridLedger.SummaryRowsTop.Clear();
                    gridLedger.MasterTemplate.EnablePaging = false;
                    gridLedger.MasterTemplate.ShowTotals = true;
                    GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                    summaryItem.Name = gridLedger.Columns[0].Name;
                    summaryItem.Aggregate = GridAggregateFunction.Count;

                    GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                    summaryRowItem.Add(summaryItem);
                    this.gridLedger.SummaryRowsTop.Add(summaryRowItem);
                    GridViewSummaryRowItem summaryRowItem2 = new GridViewSummaryRowItem();
                }
            }
        }

        private void gridLedger_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridLedger.MasterTemplate)
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
        
        private void gridLedger_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.gridLedger.CurrentRow;
            LedgerAccountModel selectedLedger = (LedgerAccountModel)info.DataBoundItem;
            _selectedRowLedger = new LedgerAccountModel();
            _clickedLedger = new LedgerAccountModel();

            //_selectedRowLedger = selectedLedger;

            //EmployeePassportModel selectedPassport = new EmployeePassportModel();
            //EmployeePassportBUS passportBUS = new EmployeePassportBUS();
            //selectedPassport = passportBUS.GetEmpoyeePassport(selectedLedger.idEmployee);

            if (info != null && e.RowIndex >= 0)
                if (selectedLedger != null)
                {
                    frmLedgerAccount frm = new frmLedgerAccount(selectedLedger);
                 //   frmLedgerAccount frm = new frmLedgerAccount();
                    frm.ShowDialog();
                    List<IModel> lam = new List<IModel>();
                    List<IModel> binding = new List<IModel>();
                    binding = new LedgerAccountBUS(Login._bookyear).GetAllAccounts();
                    this.SetDataPersonBinding(binding); 
                   // modelData =   LedgerAccountBUS.GetAllAccounts();
                   // this.SetDataPersonBinding(modelData);
                    //this.gridLedger.DataSource = null;
                    //this.gridLedger.DataSource = modelData;
                   
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

        private void gridLedger_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridLedger.MasterView.TableHeaderRow && e.CurrentRow != gridLedger.MasterView.TableFilteringRow && e.CurrentRow != gridLedger.MasterView.TableSearchRow)
            {
                LedgerAccountModel selectedLedger = new LedgerAccountModel();
                selectedLedger = (LedgerAccountModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowLedger = selectedLedger;
                    RaiseStatusChanged(selectedLedger);
                }
            }
        }
        #endregion Grid Events

        private void gridLedger_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                LedgerAccountModel model = (LedgerAccountModel)this.gridLedger.CurrentRow.DataBoundItem;
                if (model != null)
                {
                    frmLedgerAccount frm = new frmLedgerAccount(model);
                    frm.Show();
                    //return;
                }
            }            
        }

        private void gridLedger_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridLedger.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridLedger.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridLedger.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridLedger.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridLedger.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridLedger.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridLedger.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridLedger.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridLedger.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridLedger.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridLedger.ActiveEditor as RadDateTimeEditor;
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

         private void gridLedger_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
         {
             if (this.gridLedger.IsInEditMode && !(this.gridLedger.CurrentColumn is GridViewCheckBoxColumn))
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

         private void gridLedger_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
         {
             if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
             {
                 lastFilterDescriptor = gridLedger.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
             }
         }

         private void gridLedger_CellEndEdit(object sender, GridViewCellEventArgs e)
         {
             var filteringRow = e.Row as GridViewFilteringRowInfo;

             if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
             {
                 this.gridLedger.FilterChanging -= gridLedger_FilterChanging;

                 this.gridLedger.FilterDescriptors.Remove(e.Column.Name);
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
                     if (!this.gridLedger.FilterDescriptors.Contains(e.Column.Name))
                     {
                         this.gridLedger.FilterDescriptors.Add(lastFilterDescriptor);
                     }
                     else
                     {
                         //ako descriptr vec postoji, setuj samo operator
                         FilterDescriptor tmpdesc = gridLedger.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                         tmpdesc.Operator = lastFilterDescriptor.Operator;
                     }

                     lastFilterDescriptor = null;
                 }

                 this.gridLedger.FilterChanging += gridLedger_FilterChanging;
             }  
         }

         private void gridLedger_CellClick(object sender, GridViewCellEventArgs e)
         {
             if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
             {
                 this.gridLedger.EndEdit();
             }
         }




    }
    public class LedgerStatusSelectedRowchanged : EventArgs
    {
      public LedgerAccountModel ledger { get; set; }
      //  public PersonModel person { get; set; }
    }
}
