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

    public partial class GridViewArrangementInsurancePremie : System.Windows.Forms.UserControl, IBISGrid 
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
        public event EventHandler<ArrangementInsurancePremieStatusSelectedRowchanged> ArrangementInsurancePremieStatusSelectedRowchanged = delegate { };
        public void RaiseStatusChanged(ArrangementInsurancePremieModel aArrangementInsurancePremie)
        {
            ArrangementInsurancePremieStatusSelectedRowchanged(this, new ArrangementInsurancePremieStatusSelectedRowchanged { aArrangementInsurancePremie = aArrangementInsurancePremie });
        }

        ArrangementInsurancePremieBUS arrangementInsurancePremieBUS;
        private List<IModel> modelData;
        private Telerik.WinControls.UI.RadGridView gridArrangementInsurancePremie;
        private ArrangementInsurancePremieModel _selectedRowArrangementInsurancePremie;
        private ArrangementInsurancePremieModel _clickedArrangementInsurancePremie;
        private string filterFolder;
        private string labelFolder;
        private bool  _bLoadTreeMenu = false;


        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public GridViewArrangementInsurancePremie()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\ArrangementInsurancePremie")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\ArrangementInsurancePremie"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\ArrangementInsurancePremie")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\ArrangementInsurancePremie"));
            } 
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username+"\\filters\\custom filters\\ArrangementInsurancePremie");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\ArrangementInsurancePremie");
            arrangementInsurancePremieBUS = new ArrangementInsurancePremieBUS();

            InitializeComponent();
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridArrangementInsurancePremie = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridArrangementInsurancePremie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridArrangementInsurancePremie.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridArrangementInsurancePremie
            // 
            this.gridArrangementInsurancePremie.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridArrangementInsurancePremie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridArrangementInsurancePremie.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gridArrangementInsurancePremie.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridArrangementInsurancePremie.MasterTemplate.AllowAddNewRow = false;
            this.gridArrangementInsurancePremie.MasterTemplate.AllowCellContextMenu = false;
            this.gridArrangementInsurancePremie.MasterTemplate.AllowDeleteRow = false;
            this.gridArrangementInsurancePremie.MasterTemplate.AllowEditRow = false;
            this.gridArrangementInsurancePremie.MasterTemplate.AllowSearchRow = true;
            this.gridArrangementInsurancePremie.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridArrangementInsurancePremie.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridArrangementInsurancePremie.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridArrangementInsurancePremie.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridArrangementInsurancePremie.MasterTemplate.EnableFiltering = true;
            this.gridArrangementInsurancePremie.MasterTemplate.EnablePaging = true;
            this.gridArrangementInsurancePremie.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridArrangementInsurancePremie.MasterTemplate.PageSize = 50;
            this.gridArrangementInsurancePremie.MasterTemplate.ShowGroupedColumns = true;
            this.gridArrangementInsurancePremie.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridArrangementInsurancePremie.Name = "gridArrangementInsurancePremie";
            this.gridArrangementInsurancePremie.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridArrangementInsurancePremie.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridArrangementInsurancePremie.Size = new System.Drawing.Size(150, 150);
            this.gridArrangementInsurancePremie.TabIndex = 0;
            this.gridArrangementInsurancePremie.Text = "ArrangementInsurancePremie Grid";
            this.gridArrangementInsurancePremie.ThemeName = "VisualStudio2012Light";
            this.gridArrangementInsurancePremie.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridArrangementInsurancePremie_CellBeginEdit);
            this.gridArrangementInsurancePremie.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridArrangementInsurancePremie_CellEditorInitialized);
            this.gridArrangementInsurancePremie.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridArrangementInsurancePremie_CellEndEdit);
            this.gridArrangementInsurancePremie.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridArrangementInsurancePremie_CurrentRowChanged);
            this.gridArrangementInsurancePremie.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridArrangementInsurancePremie_CellClick);
            this.gridArrangementInsurancePremie.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridArrangementInsurancePremie_CellDoubleClick);
            this.gridArrangementInsurancePremie.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridArrangementInsurancePremie_GroupSummaryEvaluate);
            this.gridArrangementInsurancePremie.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridArrangementInsurancePremie_DataBindingComplete);
            this.gridArrangementInsurancePremie.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridArrangementInsurancePremie_FilterChanging);
            this.gridArrangementInsurancePremie.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridArrangementInsurancePremie_KeyDown);
            // 
            // GridViewArrangementInsurancePremie
            // 
            this.Controls.Add(this.gridArrangementInsurancePremie);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GridViewArrangementInsurancePremie";
            ((System.ComponentModel.ISupportInitialize)(this.gridArrangementInsurancePremie.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridArrangementInsurancePremie)).EndInit();
            this.ResumeLayout(false);

        }

        #region GettersSetters
        // getters setters
        public bool bLoadTreeMenu
        {
            get { return _bLoadTreeMenu; }
            set { _bLoadTreeMenu = value; }
        }

        public Telerik.WinControls.UI.RadGridView ArrangementInsurancePremieGridView
        {
            get { return gridArrangementInsurancePremie; }
        }
        public ArrangementInsurancePremieModel SelectedRowArrangementInsurancePremie
        {
            get { return _selectedRowArrangementInsurancePremie; }
        }
        public ArrangementInsurancePremieModel ClickedArrangementInsurancePremie
        {
            get { return _clickedArrangementInsurancePremie; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridArrangementInsurancePremie.Columns; }
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
            return arrangementInsurancePremieBUS.GetAllArrangementInsurancePremie();
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
            this.gridArrangementInsurancePremie.DataSource = null;
            this.gridArrangementInsurancePremie.DataSource = binding;            
        }
        public void ClearDescriptors()
        {
            this.gridArrangementInsurancePremie.MasterTemplate.SortDescriptors.Clear();
            this.gridArrangementInsurancePremie.MasterTemplate.GroupDescriptors.Clear();
            this.gridArrangementInsurancePremie.MasterTemplate.FilterDescriptors.Clear();

        }
        public void removeRow(ArrangementInsurancePremieModel rw)
        {
            using (gridArrangementInsurancePremie.DeferRefresh())
            {
                GridViewRowInfo row = this.gridArrangementInsurancePremie.Rows.Where(s => s.Cells["idPremie"].Value.ToString() == rw.idPremie.ToString()).FirstOrDefault();
                if (row != null)
                    this.gridArrangementInsurancePremie.Rows.Remove(row);
            }
        }
        public void SaveLayout(string filename)
        {            
            this.gridArrangementInsurancePremie.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridArrangementInsurancePremie.LoadLayout(filterFolder + "\\" + filename);
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
        private void gridArrangementInsurancePremie_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
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
            //if (gridArrangementInsurancePremie.Columns.Count > 0)
            //{
            //    //for number of rows
            //    this.gridArrangementInsurancePremie.SummaryRowsTop.Clear();
            //    gridArrangementInsurancePremie.MasterTemplate.EnablePaging = false;
            //    gridArrangementInsurancePremie.MasterTemplate.ShowTotals = true;
            //    GridViewSummaryItem summaryItem = new GridViewSummaryItem();
            //    summaryItem.Name = gridArrangementInsurancePremie.Columns[0].Name;
            //    summaryItem.Aggregate = GridAggregateFunction.Count;

            //    GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
            //    summaryRowItem.Add(summaryItem);
            //    this.gridArrangementInsurancePremie.SummaryRowsTop.Add(summaryRowItem);
            //    GridViewSummaryRowItem summaryRowItem2 = new GridViewSummaryRowItem();
            //}


            if (gridArrangementInsurancePremie.Columns != null)
            {
                if (gridArrangementInsurancePremie.RowCount > 0)
                {
                    this.gridArrangementInsurancePremie.Columns["dtValidFrom"].FormatString = "{0: dd-MM-yyyy}";
                    this.gridArrangementInsurancePremie.Columns["dtValidTo"].FormatString = "{0: dd-MM-yyyy}";


                }
            }
        }

        private void gridArrangementInsurancePremie_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            //if (e.Parent == this.gridArrangementInsurancePremie.MasterTemplate)
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
        
        private void gridArrangementInsurancePremie_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.gridArrangementInsurancePremie.CurrentRow;
            ArrangementInsurancePremieModel selectedArrangementInsurancePremie = (ArrangementInsurancePremieModel)info.DataBoundItem;
            _selectedRowArrangementInsurancePremie = new ArrangementInsurancePremieModel();
            _clickedArrangementInsurancePremie = new ArrangementInsurancePremieModel();

            //_selectedRowLedger = selectedLedger;

            //EmployeePassportModel selectedPassport = new EmployeePassportModel();
            //EmployeePassportBUS passportBUS = new EmployeePassportBUS();
            //selectedPassport = passportBUS.GetEmpoyeePassport(selectedLedger.idEmployee);

            if (info != null && e.RowIndex >= 0)
                if (selectedArrangementInsurancePremie != null)
                {
                    frmArrangementInsurancePremie frm = new frmArrangementInsurancePremie(selectedArrangementInsurancePremie);
                    frm.ShowDialog();
                    modelData = arrangementInsurancePremieBUS.GetAllArrangementInsurancePremie();
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

        private void gridArrangementInsurancePremie_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridArrangementInsurancePremie.MasterView.TableHeaderRow && e.CurrentRow != gridArrangementInsurancePremie.MasterView.TableFilteringRow && e.CurrentRow != gridArrangementInsurancePremie.MasterView.TableSearchRow)
            {
                ArrangementInsurancePremieModel selectedArrangementInsurancePremie = new ArrangementInsurancePremieModel();
                selectedArrangementInsurancePremie = (ArrangementInsurancePremieModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowArrangementInsurancePremie = selectedArrangementInsurancePremie;
                    RaiseStatusChanged(selectedArrangementInsurancePremie);
                }
            }
        }
        #endregion Grid Events

        private void gridArrangementInsurancePremie_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.gridArrangementInsurancePremie.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    ArrangementInsurancePremieModel model = (ArrangementInsurancePremieModel)this.gridArrangementInsurancePremie.CurrentRow.DataBoundItem;
                    frmArrangementInsurancePremie frm = new frmArrangementInsurancePremie(model);
                    frm.ShowDialog();
                    modelData = arrangementInsurancePremieBUS.GetAllArrangementInsurancePremie();
                    this.SetDataPersonBinding(modelData);

                }

            }
        }

        private void gridArrangementInsurancePremie_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridArrangementInsurancePremie.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridArrangementInsurancePremie.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArrangementInsurancePremie.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArrangementInsurancePremie.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArrangementInsurancePremie.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArrangementInsurancePremie.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArrangementInsurancePremie.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArrangementInsurancePremie.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridArrangementInsurancePremie.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridArrangementInsurancePremie.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridArrangementInsurancePremie.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridArrangementInsurancePremie.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridArrangementInsurancePremie_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridArrangementInsurancePremie.IsInEditMode && !(this.gridArrangementInsurancePremie.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridArrangementInsurancePremie_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridArrangementInsurancePremie.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridArrangementInsurancePremie_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridArrangementInsurancePremie.FilterChanging -= gridArrangementInsurancePremie_FilterChanging;

                this.gridArrangementInsurancePremie.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridArrangementInsurancePremie.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridArrangementInsurancePremie.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridArrangementInsurancePremie.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridArrangementInsurancePremie.FilterChanging += gridArrangementInsurancePremie_FilterChanging;
            }  
        }

        private void gridArrangementInsurancePremie_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridArrangementInsurancePremie.EndEdit();
            }
        }



    }
    public class ArrangementInsurancePremieStatusSelectedRowchanged : EventArgs
    {
        public ArrangementInsurancePremieModel aArrangementInsurancePremie { get; set; }
      //  public PersonModel person { get; set; }
    }
}
