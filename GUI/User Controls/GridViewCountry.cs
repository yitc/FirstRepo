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
    public partial class GridViewCountry : System.Windows.Forms.UserControl, IBISGrid
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

        public event EventHandler<CountrySelectedRowchanged> CountrySelectedRowchanged = delegate { };

        public void RaiseStatusChanged(CountryModel art)
        {
            CountrySelectedRowchanged(this, new CountrySelectedRowchanged { a = art });
        }


        CountryBUS CountyBUS;
        private Telerik.WinControls.UI.RadGridView gridCountry;
        private CountryModel _selectedRowCountry;
        private CountryModel _clickedCountry;
        public List<IModel> modelData;
        // Folder u kome cuva filtere za Country
        private string filterFolder;

        // Folder u kome cuva labele za Country
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridViewCountry()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\country")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\country"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\country")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\country"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\country");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\country");
            CountyBUS = new CountryBUS();

            InitializeComponent();        
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridCountry = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCountry.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridCountry
            // 
            this.gridCountry.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridCountry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCountry.EnableFastScrolling = true;
            this.gridCountry.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridCountry.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridCountry.MasterTemplate.AllowAddNewRow = false;
            this.gridCountry.MasterTemplate.AllowCellContextMenu = false;
            this.gridCountry.MasterTemplate.AllowDeleteRow = false;
            this.gridCountry.MasterTemplate.AllowEditRow = false;
            this.gridCountry.MasterTemplate.AllowSearchRow = true;
            this.gridCountry.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridCountry.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridCountry.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridCountry.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridCountry.MasterTemplate.EnableFiltering = true;
            this.gridCountry.MasterTemplate.EnablePaging = true;
            this.gridCountry.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridCountry.MasterTemplate.PageSize = 50;
            this.gridCountry.MasterTemplate.ShowGroupedColumns = true;
            this.gridCountry.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridCountry.Name = "gridCountry";
            this.gridCountry.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridCountry.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridCountry.Size = new System.Drawing.Size(150, 150);
            this.gridCountry.TabIndex = 0;
            this.gridCountry.Text = "gridCountry";
            this.gridCountry.ThemeName = "VisualStudio2012Light";
            this.gridCountry.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridCountry_CellBeginEdit);
            this.gridCountry.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridCountry_CellEditorInitialized);
            this.gridCountry.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridCountry_CellEndEdit);
            this.gridCountry.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridCountry_CurrentRowChanged);
            this.gridCountry.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridCountry_CellClick);
            this.gridCountry.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridCountry_CellDoubleClick);
            this.gridCountry.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridCountry_GroupSummaryEvaluate);
            this.gridCountry.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridCountry_DataBindingComplete);
            this.gridCountry.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridCountry_FilterChanging);
            this.gridCountry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridCountry_KeyDown);
            // 
            // GridViewCountry
            // 
            this.Controls.Add(this.gridCountry);
            this.Name = "GridViewCountry";
            ((System.ComponentModel.ISupportInitialize)(this.gridCountry.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCountry)).EndInit();
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
            get { return gridCountry; }
        }

        public CountryModel SelectedRowCountry
        {
            get { return _selectedRowCountry; }
        }
        public CountryModel ClickedCountry
        {
            get { return _clickedCountry; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridCountry.Columns; }
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
            return CountyBUS.GetCountries();  
        }
        public List<FilterModel> ReturnFilters()
        {
            return Login._countryFilters;
        }
        public List<LabelModel> ReturnLabels()
        {
            return Login._countryLabels;
        }       
      
        public void SetDataPersonBinding(List<IModel> binding)
        {
            this.gridCountry.DataSource = null;
            this.gridCountry.DataSource = binding;

        }

        public void ClearDescriptors()
        {
            this.gridCountry.MasterTemplate.SortDescriptors.Clear();
            this.gridCountry.MasterTemplate.GroupDescriptors.Clear();
            this.gridCountry.MasterTemplate.FilterDescriptors.Clear();
        }
        public void removeRow(CountryModel rw)
        {
            using (gridCountry.DeferRefresh())
            {
                GridViewRowInfo row = this.gridCountry.Rows.Where(s => s.Cells["idCountry"].Value.ToString() == rw.idCountry.ToString()).FirstOrDefault();
                if (row != null)
                    this.gridCountry.Rows.Remove(row);
            }
        }

        public void SaveLayout(string filename)
        {
            this.gridCountry.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridCountry.LoadLayout(filterFolder + "\\" + filename);
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
        private void gridCountry_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                dictionary.Add(column.HeaderText, column.HeaderText);
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                column.MinWidth = column.Width;
            }
            for (int i = 0; i < gridCountry.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(gridCountry.Columns[i].HeaderText) != null)
                        gridCountry.Columns[i].HeaderText = resxSet.GetString(gridCountry.Columns[i].HeaderText);
                }

            }
            if (gridCountry.Columns.Count > 0)
            {
                //for number of rows
                this.gridCountry.SummaryRowsTop.Clear();
                gridCountry.MasterTemplate.EnablePaging = false;
                gridCountry.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridCountry.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridCountry.SummaryRowsTop.Add(summaryRowItem);
                GridViewSummaryRowItem summaryRowItem2 = new GridViewSummaryRowItem();

                if (Login._companyModelList[0].flag == "W") // da se ne vidi na Wi
                { 
                grid.Columns["provision"].IsVisible = false;
                grid.Columns["premie"].IsVisible = false;
                }
                //grid.Columns["dtUserCreated"].IsVisible = false;
                //grid.Columns["idUserModifies"].IsVisible = false;
                //grid.Columns["nameUserModified"].IsVisible = false;
                //grid.Columns["dtUserModified"].IsVisible = false;
            }
        }

        private void gridCountry_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridCountry.MasterTemplate)
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
        private void gridCountry_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            CountryModel selectedCountry = new CountryModel();
            GridViewRowInfo info = this.gridCountry.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedCountry = (CountryModel)info.DataBoundItem;

                if (selectedCountry != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedCountry = selectedCountry;

                   
                    
                        frmCountry frm = new frmCountry(this._clickedCountry);

                        frm.ShowDialog();
                    

                    CountryBUS arbus = new CountryBUS();
                    modelData = arbus.GetCountries(); ;
                    this.SetDataPersonBinding(modelData);  
                }
            }
        }
        private void gridCountry_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridCountry.MasterView.TableHeaderRow && e.CurrentRow != gridCountry.MasterView.TableFilteringRow && e.CurrentRow != gridCountry.MasterView.TableSearchRow)
            {
                CountryModel selectedCountry = new CountryModel();
                selectedCountry = (CountryModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowCountry = selectedCountry;
                    RaiseStatusChanged(selectedCountry);
                }
            }
        }
        #endregion

        private void gridCountry_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.gridCountry.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    CountryModel model = (CountryModel)this.gridCountry.CurrentRow.DataBoundItem;
                    frmCountry frm = new frmCountry(model);
                    frm.ShowDialog();
                    return;
                }
            }
        }

        private void gridCountry_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridCountry.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridCountry.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridCountry.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridCountry.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridCountry.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridCountry.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridCountry.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridCountry.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridCountry.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridCountry.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridCountry.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridCountry.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridCountry_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridCountry.IsInEditMode && !(this.gridCountry.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridCountry_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridCountry.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridCountry_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridCountry.FilterChanging -= gridCountry_FilterChanging;

                this.gridCountry.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridCountry.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridCountry.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridCountry.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridCountry.FilterChanging += gridCountry_FilterChanging;
            }  
        }

        private void gridCountry_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridCountry.EndEdit();
            }
        }



      

    }

    public class CountrySelectedRowchanged : EventArgs
    {
        public CountryModel a { get; set; }
    }


}
