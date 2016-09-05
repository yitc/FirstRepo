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

    public partial class GridViewEmployee : System.Windows.Forms.UserControl, IBISGrid
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
        public event EventHandler<EmployeeStatusSelectedRowchanged> EmployeeStatusSelectedRowchanged = delegate { };
        public void RaiseStatusChanged(EmployeeModel employee)
        {
            EmployeeStatusSelectedRowchanged(this, new EmployeeStatusSelectedRowchanged { employee = employee });
        }
        
        EmployeeBUS EmployeeBUS;
        private Telerik.WinControls.UI.RadGridView gridEmployee;
        private EmployeeModel _selectedRowEmployee;
        private EmployeeModel _clickedEmployee;
        private string filterFolder;
        private string labelFolder;
        private bool  _bLoadTreeMenu = false;
        public bool modelChanged = false;


        Dictionary<string, string> dictionary = new Dictionary<string, string>();                
        
        public GridViewEmployee()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\employees")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\employees"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\employees")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\employees"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username+"\\filters\\custom filters\\employees");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\employees");
            EmployeeBUS = new EmployeeBUS();

            InitializeComponent();

            this.gridEmployee.GridBehavior = new MyGridEmployeeBehavior();
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridEmployee = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridEmployee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEmployee.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridEmployee
            // 
            this.gridEmployee.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridEmployee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridEmployee.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.gridEmployee.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridEmployee.MasterTemplate.AllowAddNewRow = false;
            this.gridEmployee.MasterTemplate.AllowCellContextMenu = false;
            this.gridEmployee.MasterTemplate.AllowDeleteRow = false;
            this.gridEmployee.MasterTemplate.AllowEditRow = false;
            this.gridEmployee.MasterTemplate.AllowSearchRow = true;
            this.gridEmployee.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridEmployee.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridEmployee.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridEmployee.MasterTemplate.EnableFiltering = true;
            this.gridEmployee.MasterTemplate.EnablePaging = true;
            this.gridEmployee.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridEmployee.MasterTemplate.PageSize = 50;
            this.gridEmployee.MasterTemplate.ShowFilterCellOperatorText = false;
            this.gridEmployee.MasterTemplate.ShowGroupedColumns = true;
            this.gridEmployee.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridEmployee.Name = "gridEmployee";
            this.gridEmployee.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridEmployee.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridEmployee.Size = new System.Drawing.Size(150, 150);
            this.gridEmployee.TabIndex = 0;
            this.gridEmployee.Text = "Employee Grid";
            this.gridEmployee.ThemeName = "VisualStudio2012Light";
            this.gridEmployee.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.gridEmployee_CellFormating);
            this.gridEmployee.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridEmployee_CellBeginEdit);
            this.gridEmployee.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridEmployee_CellEditorInitialized);
            this.gridEmployee.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridEmployee_CellEndEdit);
            this.gridEmployee.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridEmployee_CurrentRowChanged);
            this.gridEmployee.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridEmployee_CellClick);
            this.gridEmployee.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridEmployee_CellDoubleClick);
            this.gridEmployee.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridEmployee_GroupSummaryEvaluate);
            this.gridEmployee.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridEmployee_DataBindingComplete);
            this.gridEmployee.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridEmployee_FilterChanging);
            this.gridEmployee.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridEmployee_KeyDown);
            // 
            // GridViewEmployee
            // 
            this.Controls.Add(this.gridEmployee);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GridViewEmployee";
            ((System.ComponentModel.ISupportInitialize)(this.gridEmployee.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEmployee)).EndInit();
            this.ResumeLayout(false);

        }

        #region GettersSetters
        // getters setters
        public bool bLoadTreeMenu
        {
            get { return _bLoadTreeMenu; }
            set { _bLoadTreeMenu = value; }
        }

        public Telerik.WinControls.UI.RadGridView EmployeeGridView
        {
            get { return gridEmployee; }
        }
        public EmployeeModel SelectedRowPerson
        {
            get { return _selectedRowEmployee; }
        }
        public EmployeeModel ClickedEmployee
        {
            get { return _clickedEmployee; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridEmployee.Columns; }
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

        public List<IModel> GetData(int selectedFilter, List<int> idLabelList, string idLang)
        {
            return EmployeeBUS.GetAllEmployees(Convert.ToInt32(selectedFilter), idLabelList,idLang);
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
            this.gridEmployee.DataSource = null;
            this.gridEmployee.DataSource = binding;

        }
        public void ClearDescriptors()
        {
            this.gridEmployee.MasterTemplate.SortDescriptors.Clear();
            this.gridEmployee.MasterTemplate.GroupDescriptors.Clear();
            this.gridEmployee.MasterTemplate.FilterDescriptors.Clear();

        }
        public void  removeRow(EmployeeModel rw)
        {
            using (gridEmployee.DeferRefresh())
            {
                GridViewRowInfo row = this.gridEmployee.Rows.Where(s => s.Cells["idEmployee"].Value.ToString() == rw.idEmployee.ToString()).FirstOrDefault();
                if (row != null)
                    this.gridEmployee.Rows.Remove(row);
            }
        }
        public void SaveLayout(string filename)
        {            
            this.gridEmployee.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridEmployee.LoadLayout(filterFolder + "\\" + filename);
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
        private void gridEmployee_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
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
            if (gridEmployee.Columns.Count > 0)
            {
                //for number of rows
                this.gridEmployee.SummaryRowsTop.Clear();
                gridEmployee.MasterTemplate.EnablePaging = false;
                gridEmployee.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = grid.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridEmployee.SummaryRowsTop.Add(summaryRowItem);
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

        private void gridEmployee_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridEmployee.MasterTemplate)
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
        
        private void gridEmployee_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            EmployeeModel selectedEmployee = new EmployeeModel();
            GridViewRowInfo info = this.gridEmployee.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedEmployee = (EmployeeModel)info.DataBoundItem;
                //  selectedPassport = passportBUS.GetPassport(selectedPerson.idContPers);


                if (selectedEmployee != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedEmployee = selectedEmployee;

                    EmployeePassportModel selectedPassport = new EmployeePassportModel();
                    EmployeePassportBUS passportBUS = new EmployeePassportBUS();
                    selectedPassport = passportBUS.GetEmpoyeePassport(this._clickedEmployee.idEmployee);

                    frmEmployee frm = new frmEmployee(this._clickedEmployee, selectedPassport);                    

                    bool formfound = false;
                    foreach (Form f in Application.OpenForms)
                    {
                        if (f is frmEmployee)
                        {
                            if ((int)f.Tag == this._clickedEmployee.idEmployee)
                            {
                                f.BringToFront();
                                formfound = true;
                                break;
                            }
                        }
                    }

                    if (formfound == false)
                    {
                        frm.Tag = this._clickedEmployee.idEmployee;
                        frm.Show();
                    }
                }
            }
        }

        private void gridEmployee_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridEmployee.MasterView.TableHeaderRow && e.CurrentRow != gridEmployee.MasterView.TableFilteringRow && e.CurrentRow != gridEmployee.MasterView.TableSearchRow)
            {
                EmployeeModel selectedEmployee = new EmployeeModel();
                selectedEmployee = (EmployeeModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowEmployee = selectedEmployee;
                    RaiseStatusChanged(selectedEmployee);
                }
            }
        }

        private void gridEmployee_CellFormating(object sender, CellFormattingEventArgs e)
        {
            if (e.Column.Name == "dtBirthDateEmployee")
            {
                if (e.Column.IsVisible == true)
                {
                    try
                    {
                        //DateTime temp = DateTime.Parse(e.CellElement.Text);
                        DateTime temp = DateTime.Parse(e.CellElement.Value.ToString());
                        e.CellElement.Text = temp.ToString("dd-MM-yyyy");
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            if (e.Column.Name == "dtHireDateEmployee")
            {
                if (e.Column.IsVisible == true)
                {
                    try
                    {
                        //DateTime temp = DateTime.Parse(e.CellElement.Text);
                        DateTime temp = DateTime.Parse(e.CellElement.Value.ToString());
                        e.CellElement.Text = temp.ToString("dd-MM-yyyy");
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            
        }
        #endregion Grid Events

        private void gridEmployee_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //if (this.gridEmployee.CurrentRow.DataBoundItem != null)
            //{
            //    if (e.KeyData == Keys.Enter)
            //    {                    
            //        GridViewRowInfo info = this.gridEmployee.CurrentRow;
            //        EmployeeModel selectedEmployee = (EmployeeModel)info.DataBoundItem;
            //        _selectedRowEmployee = new EmployeeModel();
            //        _clickedEmployee = new EmployeeModel();

            //        _selectedRowEmployee = selectedEmployee;

            //        EmployeePassportModel selectedPassport = new EmployeePassportModel();
            //        EmployeePassportBUS passportBUS = new EmployeePassportBUS();
            //        selectedPassport = passportBUS.GetEmpoyeePassport(selectedEmployee.idEmployee);

            //        //if (info != null && e.RowIndex >= 0)
            //        if (selectedEmployee != null)
            //        {
            //            frmEmployee frm = new frmEmployee(_selectedRowEmployee, selectedPassport);
            //            frm.ShowDialog();

            //            EmployeeBUS bu = new EmployeeBUS();
            //            List<IModel> ss = new List<IModel>();
            //            List<int> lab = new List<int>();

            //            //ss = GetData(Convert.ToInt32(selectedFilter), 0, Login._user.lngUser);//bu.GetAllEmpl("NL");
            //            ss = EmployeeBUS.GetAllEmployees(0, lab, Login._user.lngUser);
            //            this.gridEmployee.DataSource = null;
            //            this.gridEmployee.DataSource = ss;

            //        }

            //    }


            //}
        }

        private void gridEmployee_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridEmployee.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridEmployee.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridEmployee.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridEmployee.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridEmployee.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridEmployee.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridEmployee.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }
        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridEmployee.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridEmployee.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");                        
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridEmployee.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridEmployee.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;                                             
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridEmployee_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridEmployee.IsInEditMode && !(this.gridEmployee.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridEmployee_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridEmployee.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridEmployee_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridEmployee.FilterChanging -= gridEmployee_FilterChanging;

                this.gridEmployee.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridEmployee.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridEmployee.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridEmployee.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }
                
                this.gridEmployee.FilterChanging += gridEmployee_FilterChanging;
            }  
        }

        private void gridEmployee_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridEmployee.EndEdit();
            }
        }

    }
    public class EmployeeStatusSelectedRowchanged : EventArgs
    {
      public EmployeeModel employee { get; set; }
      //  public PersonModel person { get; set; }
    }

    class MyGridEmployeeBehavior : BaseGridBehavior
    {
        public override bool ProcessKeyDown(KeyEventArgs keys)
        {
            if (keys.KeyData == Keys.Enter || keys.KeyData == Keys.Return)
            {
                if (this.GridControl.CurrentRow.Index >= 0)
                {
                    int index = this.GridControl.CurrentRow.Index;
                    EmployeeModel model = (EmployeeModel)this.GridControl.Rows[index].DataBoundItem;
                    EmployeePassportModel selectedPassport = new EmployeePassportModel();
                    EmployeePassportBUS passportBUS = new EmployeePassportBUS();
                    selectedPassport = passportBUS.GetEmpoyeePassport(model.idEmployee);

                    frmEmployee frm = new frmEmployee(model, selectedPassport);

                    bool formfound = false;
                    foreach (Form f in Application.OpenForms)
                    {
                        if (f is frmEmployee)
                        {
                            if ((int)f.Tag == model.idEmployee)
                            {
                                f.BringToFront();
                                formfound = true;
                                break;
                            }
                        }
                    }

                    if (formfound == false)
                    {
                        frm.Tag = model.idEmployee;
                        frm.Show();
                    }

                    keys.SuppressKeyPress = true;
                        
                        //keys.SuppressKeyPress = true;

                    
                }
                else if (this.GridControl.CurrentRow != null && this.GridControl.CurrentRow is GridViewFilteringRowInfo && this.GridControl.IsInEditMode)
                {
                    this.GridControl.EndEdit();
                }
            }
            else if (keys.KeyData == Keys.Down)
            {
                this.GridControl.GridNavigator.SelectNextRow(1);
            }
            else if (keys.KeyData == Keys.Up)
            {
                this.GridControl.GridNavigator.SelectPreviousRow(1);
            }

            //return base.ProcessKey(keys);
            return true;
        }
    }
}
