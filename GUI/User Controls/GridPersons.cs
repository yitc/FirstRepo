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

    public partial class GridViewPersons : System.Windows.Forms.UserControl, IBISGrid
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
        public event EventHandler<StatusSelectedRowchanged> StatusRowSelectChanged = delegate { };
        public void RaiseStatusChanged(PersonModel person)
        {
            StatusRowSelectChanged(this, new StatusSelectedRowchanged { person = person });
        }

        PersonBUS personBUS;
        private Telerik.WinControls.UI.RadGridView gridPersons;
        private PersonModel _selectedRowPerson;
        private PersonModel _clickedPerson;
        private string filterFolder;
        private string labelFolder;
        private bool _bLoadTreeMenu = false;


        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public GridViewPersons()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\person")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\person"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\person")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\person"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\person");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\person");
            personBUS = new PersonBUS();

            InitializeComponent();

            this.gridPersons.GridBehavior = new MyGridPersonBehavior();
            //this.gridPersons.EnterKeyMode = RadGridViewEnterKeyMode.EnterMovesToNextCell;


        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridPersons = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridPersons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPersons.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridPersons
            // 
            this.gridPersons.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridPersons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPersons.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridPersons.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridPersons.MasterTemplate.AllowAddNewRow = false;
            this.gridPersons.MasterTemplate.AllowCellContextMenu = false;
            this.gridPersons.MasterTemplate.AllowDeleteRow = false;
            this.gridPersons.MasterTemplate.AllowEditRow = false;
            this.gridPersons.MasterTemplate.AllowSearchRow = true;
            this.gridPersons.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridPersons.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridPersons.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridPersons.MasterTemplate.EnableFiltering = true;
            this.gridPersons.MasterTemplate.EnablePaging = true;
            this.gridPersons.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridPersons.MasterTemplate.PageSize = 50;
            this.gridPersons.MasterTemplate.ShowFilterCellOperatorText = false;
            this.gridPersons.MasterTemplate.ShowGroupedColumns = true;
            this.gridPersons.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridPersons.Name = "gridPersons";
            this.gridPersons.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridPersons.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridPersons.Size = new System.Drawing.Size(150, 150);
            this.gridPersons.TabIndex = 0;
            this.gridPersons.Text = "Persons Grid";
            this.gridPersons.ThemeName = "VisualStudio2012Light";
            this.gridPersons.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.gridPerson_CellFormating);
            this.gridPersons.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridPersons_CellBeginEdit);
            this.gridPersons.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridPersons_CellEditorInitialized);
            this.gridPersons.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridPersons_CellEndEdit);
            this.gridPersons.ValueChanged += new System.EventHandler(this.gridPersons_ValueChanged);
            this.gridPersons.ValueChanging += new Telerik.WinControls.UI.ValueChangingEventHandler(this.gridPersons_ValueChanging);
            this.gridPersons.CurrentCellChanged += new Telerik.WinControls.UI.CurrentCellChangedEventHandler(this.gridPersons_CurrentCellChanged);
            this.gridPersons.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridPersons_CurrentRowChanged);
            this.gridPersons.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridPersons_CellClick);
            this.gridPersons.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridPersons_CellDoubleClick);
            this.gridPersons.CellValueChanged += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridPersons_CellValueChanged);
            this.gridPersons.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridPersons_GroupSummaryEvaluate);
            this.gridPersons.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridPersons_DataBindingComplete);
            this.gridPersons.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridPersons_FilterChanging);
            this.gridPersons.FilterChanged += new Telerik.WinControls.UI.GridViewCollectionChangedEventHandler(this.gridPersons_FilterChanged);
            this.gridPersons.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridPersons_KeyDown);
            // 
            // GridViewPersons
            // 
            this.Controls.Add(this.gridPersons);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GridViewPersons";
            this.Load += new System.EventHandler(this.GridViewPersons_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridPersons.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPersons)).EndInit();
            this.ResumeLayout(false);

        }

        private void GridViewPersons_Load(object sender, EventArgs e)
        {
            this.gridPersons.EnterKeyMode = RadGridViewEnterKeyMode.None;
            this.gridPersons.NewRowEnterKeyMode = RadGridViewNewRowEnterKeyMode.None;
        }

        #region GettersSetters
        // getters setters
        public bool bLoadTreeMenu
        {
            get { return _bLoadTreeMenu; }
            set { _bLoadTreeMenu = value; }
        }

        public Telerik.WinControls.UI.RadGridView PersonsGridView
        {
            get { return gridPersons; }
        }
        public PersonModel SelectedRowPerson
        {
            get { return _selectedRowPerson; }
        }
        public PersonModel ClickedPerson
        {
            get { return _clickedPerson; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridPersons.Columns; }
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
            return personBUS.GetAllPersons(Convert.ToInt32(selectedFilter), idLabelList, idLang);
        }
        public List<FilterModel> ReturnFilters()
        {
            return Login._personFilters;
        }
        public List<LabelModel> ReturnLabels()
        {
            return Login._personLabels;
        }
        public void SetDataPersonBinding(List<IModel> binding)
        {
            this.gridPersons.DataSource = null;
            this.gridPersons.DataSource = binding;

        }
        public void ClearDescriptors()
        {
            this.gridPersons.MasterTemplate.SortDescriptors.Clear();
            this.gridPersons.MasterTemplate.GroupDescriptors.Clear();
            this.gridPersons.MasterTemplate.FilterDescriptors.Clear();

        }

        public void removeRow(PersonModel rw)
        {
            using (gridPersons.DeferRefresh())
            {
                GridViewRowInfo row = this.gridPersons.Rows.Where(s => s.Cells["idContPers"].Value.ToString() == rw.idContPers.ToString()).FirstOrDefault();
                if (row != null)
                    this.gridPersons.Rows.Remove(row);
            }
        }
        public void SaveLayout(string filename)
        {
            this.gridPersons.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridPersons.LoadLayout(filterFolder + "\\" + filename);
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
        private void gridPersons_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                dictionary.Add(column.HeaderText, column.HeaderText);
                //column.MaxWidth = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38)*2;
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                column.MinWidth = column.Width;
            }
            for (int i = 0; i < gridPersons.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(gridPersons.Columns[i].HeaderText) != null)
                        gridPersons.Columns[i].HeaderText = resxSet.GetString(gridPersons.Columns[i].HeaderText);
                }

            }
            if (gridPersons.Columns.Count > 0)
            {
                gridPersons.Columns["idGender"].IsVisible = false;
                gridPersons.Columns["idTitle"].IsVisible = false;
                gridPersons.Columns["idUserCreated"].IsVisible = false;
                gridPersons.Columns["idUserModified"].IsVisible = false;
                gridPersons.Columns["idUserResponsible"].IsVisible = false;
                gridPersons.Columns["imageContPers"].IsVisible = false;
                gridPersons.Columns["dtOfDeath"].IsVisible = false;
                gridPersons.Columns["isNeedMail"].IsVisible = false;
                gridPersons.Columns["isPayInvoice"].IsVisible = false;
                gridPersons.Columns["identBSN"].IsVisible = false;

                //for number of rows
                this.gridPersons.SummaryRowsTop.Clear();
                gridPersons.MasterTemplate.EnablePaging = false;
                gridPersons.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridPersons.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridPersons.SummaryRowsTop.Add(summaryRowItem);
                GridViewSummaryRowItem summaryRowItem2 = new GridViewSummaryRowItem();
            }
        }

        private void gridPersons_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridPersons.MasterTemplate)
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

        private void gridPersons_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            PersonModel selectedPerson = new PersonModel();
            GridViewRowInfo info = this.gridPersons.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedPerson = (PersonModel)info.DataBoundItem;
                //  selectedPassport = passportBUS.GetPassport(selectedPerson.idContPers);


                if (selectedPerson != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedPerson = selectedPerson;

                    frmPerson frm = new frmPerson(this._clickedPerson);
                    //     frmClient form = new frmClient(this._clickedClient);

                    bool formfound = false;
                    foreach (Form f in Application.OpenForms)
                    {
                        if (f is frmPerson)
                        {
                            if ((int)f.Tag == this._clickedPerson.idContPers)
                            {
                                f.BringToFront();
                                formfound = true;
                                break;
                            }

                        }
                    }

                    if (formfound == false)
                    {
                        frm.Tag = this._clickedPerson.idContPers;
                        frm.Show();
                    }
                }
            }
        }

        private void gridPersons_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridPersons.MasterView.TableHeaderRow && e.CurrentRow != gridPersons.MasterView.TableFilteringRow && e.CurrentRow != gridPersons.MasterView.TableSearchRow)
            {
                PersonModel selectedPerson = new PersonModel();
                selectedPerson = (PersonModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowPerson = selectedPerson;
                    RaiseStatusChanged(selectedPerson);
                }
            }
        }


        private void gridPerson_CellFormating(object sender, CellFormattingEventArgs e)
        {
            if (e.Column.Name == "birthdate")
            {
                if (e.Column.IsVisible == true)
                {
                    //try
                    //{
                    //    //DateTime temp = DateTime.Parse(e.CellElement.Text);
                    //    DateTime temp = DateTime.Parse(e.CellElement.Value.ToString());
                    //    e.CellElement.Text = temp.ToString("dd-MM-yyyy");
                    //}
                    //catch (Exception ex)
                    //{

                    //}

                    if (e.CellElement.ColumnInfo is GridViewDateTimeColumn)
                    {
                        GridViewDataColumn dataColumn = e.CellElement.ColumnInfo as GridViewDataColumn;
                        if (dataColumn != null)
                        {
                            dataColumn.FormatString = "{0:dd-MM-yyyy}";
                        }
                    }
                }
            }

            if (e.Column.Name == "dtCreated")
            {
                if (e.Column.IsVisible == true)
                {
                    if (e.CellElement.ColumnInfo is GridViewDateTimeColumn)
                    {
                        GridViewDataColumn dataColumn = e.CellElement.ColumnInfo as GridViewDataColumn;
                        if (dataColumn != null)
                        {
                            dataColumn.FormatString = "{0:dd-MM-yyyy hh:mm}";
                        }
                    }
                }
            }

            if (e.Column.Name == "dtModified")
            {
                if (e.Column.IsVisible == true)
                {
                    if (e.CellElement.ColumnInfo is GridViewDateTimeColumn)
                    {
                        GridViewDataColumn dataColumn = e.CellElement.ColumnInfo as GridViewDataColumn;
                        if (dataColumn != null)
                        {
                            dataColumn.FormatString = "{0:dd-MM-yyyy hh:mm}";
                        }
                    }
                }
            }

            if (e.Column.Name == "dtOfDeath")
            {
                if (e.Column.IsVisible == true)
                {
                    if (e.CellElement.ColumnInfo is GridViewDateTimeColumn)
                    {
                        GridViewDataColumn dataColumn = e.CellElement.ColumnInfo as GridViewDataColumn;
                        if (dataColumn != null)
                        {
                            dataColumn.FormatString = "{0:dd-MM-yyyy hh:mm}";
                        }
                    }
                }
            }
        }
        #endregion Grid Events

        private void gridPersons_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //if (this.gridPersons.CurrentRow.Index - 1 >= 0)
            //{
            //    if (e.KeyData == Keys.Enter || e.KeyData == Keys.Return)
            //    {                                        
            //        int index = this.gridPersons.CurrentRow.Index - 1;
            //        PersonModel model = (PersonModel)this.gridPersons.Rows[index].DataBoundItem;

            //        this._clickedPerson = model;

            //        frmPerson frm = new frmPerson(model);

            //        bool formfound = false;
            //        foreach (Form f in Application.OpenForms)
            //        {
            //            if (f is frmPerson)
            //            {
            //                if ((int)f.Tag == this._clickedPerson.idContPers)
            //                {
            //                    f.BringToFront();
            //                    formfound = true;
            //                    break;
            //                }
            //            }
            //        }

            //        if (formfound == false)
            //        {
            //            frm.Tag = this._clickedPerson.idContPers;
            //            frm.Show();

            //        }

            //        e.SuppressKeyPress = true;

            //        //this.gridPersons.CurrentRow = gridPersons.Rows[index];
            //        //e.Handled = true;                    
            //    }                                
            //}

        }

        private void gridPersons_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridPersons.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridPersons.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        //Pick up one of the default formats
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Short;

                        //Or set a custom date format
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).CustomFormat = "t";
                                                
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;                                                
                    }
                }
            }

        }
         private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridPersons.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridPersons.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                        // this.gridPersons.CurrentCell.Value = null;                          
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridPersons.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridPersons.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                        // this.gridPersons.CurrentCell.Value = null;                          
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;
        private void gridPersons_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridPersons.IsInEditMode && !(this.gridPersons.CurrentColumn is GridViewCheckBoxColumn))
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
        private void gridPersons_FilterChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            
        }
        private void gridPersons_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridPersons.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridPersons_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))            
            {
                this.gridPersons.FilterChanging -= gridPersons_FilterChanging;
                                
                this.gridPersons.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridPersons.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridPersons.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc =  gridPersons.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    //if (cellinf.Value == lastFilterDescriptor.Value)
                    //    this.gridPersons.FilterDescriptors.Add(lastFilterDescriptor);                                      
                    //else                    
                    //    cellinf.Value = lastFilterDescriptor.Value;                    
                    
                    //e.Row.Cells[e.Column.Name].Value = lastFilterDescriptor.Value;
                    
                    lastFilterDescriptor = null;
                }
                else
                {
                    this.gridPersons.FilterChanged += gridPersons_FilterChanged;
                   // e.Row.Cells[e.Column.Name].Value = null;
                }                
                this.gridPersons.FilterChanging += gridPersons_FilterChanging;
                
            }                        
        }

       

        

        private void gridPersons_CellClick(object sender, GridViewCellEventArgs e)
        {
            if(e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridPersons.EndEdit();
            }
        }

        private void gridPersons_ValueChanged(object sender, EventArgs e)
        {

        }

        private void gridPersons_ValueChanging(object sender, ValueChangingEventArgs e)
        {

        }

        private void gridPersons_CurrentCellChanged(object sender, CurrentCellChangedEventArgs e)
        {
            
        }

        private void gridPersons_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
            
        }
    }
    public class StatusSelectedRowchanged : EventArgs
    {
        public PersonModel person { get; set; }
    }

    class MyGridPersonBehavior : BaseGridBehavior
    {        
        public override bool ProcessKeyDown(KeyEventArgs keys)
        {
            if (keys.KeyData == Keys.Enter || keys.KeyData == Keys.Return)
            {
                if (this.GridControl.CurrentRow != null && this.GridControl.CurrentRow.Index >= 0)
                {
                    if (keys.KeyData == Keys.Enter || keys.KeyData == Keys.Return)
                    {
                        int index = this.GridControl.CurrentRow.Index;
                        PersonModel model = (PersonModel)this.GridControl.Rows[index].DataBoundItem;
                        
                        frmPerson frm = new frmPerson(model);

                        bool formfound = false;
                        foreach (Form f in Application.OpenForms)
                        {
                            if (f is frmPerson)
                            {
                                if ((int)f.Tag == model.idContPers)
                                {
                                    f.BringToFront();
                                    formfound = true;
                                    break;
                                }
                            }
                        }

                        if (formfound == false)
                        {
                            frm.Tag = model.idContPers;
                            frm.Show();

                        }

                        keys.SuppressKeyPress = true;
                
                    }                    
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
            //else if (keys.KeyData == Keys.Left)
            //{
            //    this.GridControl.GridNavigator.SelectPreviousColumn();
            //}
            //else if (keys.KeyData == Keys.Right)
            //{
            //    this.GridControl.GridNavigator.SelectNextColumn();
            //}

            //return base.ProcessKey(keys);
            return true;
        }
    }
}
