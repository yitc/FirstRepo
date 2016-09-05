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
    public partial class GridViewAccSettings : System.Windows.Forms.UserControl, IBISGrid
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

        public event EventHandler<AccSettingsSelectedRowchanged> AccSettingsSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(AccSettingsModel art)
        {
            AccSettingsSelectedRowchanged(this, new AccSettingsSelectedRowchanged { a = art });
        }


        AccSettingsBUS AccSettingsBUS;
        private Telerik.WinControls.UI.RadGridView gridAccSettings;
        private AccSettingsModel _selectedRowAccSettings;
        private AccSettingsModel _clickedAccSettings;
        public List<IModel> modelData;
        // Folder u kome cuva filtere za AccSettings
        private string filterFolder;

        // Folder u kome cuva labele za AccSettings
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridViewAccSettings()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\accsettings")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\accsettings"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\accsettings")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\accsettings"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\accsettings");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\accsettings");
            AccSettingsBUS = new AccSettingsBUS();

            InitializeComponent();        
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridAccSettings = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridAccSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAccSettings.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridAccSettings
            // 
            this.gridAccSettings.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridAccSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridAccSettings.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridAccSettings.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridAccSettings.MasterTemplate.AllowAddNewRow = false;
            this.gridAccSettings.MasterTemplate.AllowCellContextMenu = false;
            this.gridAccSettings.MasterTemplate.AllowDeleteRow = false;
            this.gridAccSettings.MasterTemplate.AllowEditRow = false;
            this.gridAccSettings.MasterTemplate.AllowSearchRow = true;
            this.gridAccSettings.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridAccSettings.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridAccSettings.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridAccSettings.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridAccSettings.MasterTemplate.EnableFiltering = true;
            this.gridAccSettings.MasterTemplate.EnablePaging = true;
            this.gridAccSettings.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridAccSettings.MasterTemplate.PageSize = 50;
            this.gridAccSettings.MasterTemplate.ShowGroupedColumns = true;
            this.gridAccSettings.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridAccSettings.Name = "gridAccSettings";
            this.gridAccSettings.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridAccSettings.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridAccSettings.Size = new System.Drawing.Size(150, 150);
            this.gridAccSettings.TabIndex = 0;
            this.gridAccSettings.Text = "gridAccSettings";
            this.gridAccSettings.ThemeName = "VisualStudio2012Light";
            this.gridAccSettings.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridAccSettings_CellBeginEdit);
            this.gridAccSettings.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridAccSettings_CellEditorInitialized);
            this.gridAccSettings.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridAccSettings_CellEndEdit);
            this.gridAccSettings.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridAccSettings_CurrentRowChanged);
            this.gridAccSettings.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridAccSettings_CellClick);
            this.gridAccSettings.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridAccSettings_CellDoubleClick);
            this.gridAccSettings.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridAccSettings_GroupSummaryEvaluate);
            this.gridAccSettings.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridAccSettings_DataBindingComplete);
            this.gridAccSettings.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridAccSettings_FilterChanging);
            this.gridAccSettings.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridAccSettings_KeyDown);
            // 
            // GridViewAccSettings
            // 
            this.Controls.Add(this.gridAccSettings);
            this.Name = "GridViewAccSettings";
            ((System.ComponentModel.ISupportInitialize)(this.gridAccSettings.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAccSettings)).EndInit();
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
            get { return gridAccSettings; }
        }

        public AccSettingsModel SelectedRowAccSettings
        {
            get { return _selectedRowAccSettings; }
        }
        public AccSettingsModel ClickedAccSettings
        {
            get { return _clickedAccSettings; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridAccSettings.Columns; }
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
           
            return AccSettingsBUS.GetAllAccountSettings(Login._bookyear);  
        }
        public List<FilterModel> ReturnFilters()
        {
            return Login._accsettingsFilters;
        }
        public List<LabelModel> ReturnLabels()
        {
            return Login._accsettingsLabels;
        }

        public void SetDataPersonBinding(List<IModel> databind)
        {

            //AccSettingsBUS acb = new AccSettingsBUS();
            //List<AccSettingsModel> acm = new List<AccSettingsModel>();
            //acm = acb.GetAllAccSettings(Login._bookyear);
            this.gridAccSettings.DataSource = null;
            this.gridAccSettings.DataSource = databind;

        }
      
        public void SetDataPersonBinding1(List<AccSettingsModel> acm)
        {
            
            //AccSettingsBUS acb = new AccSettingsBUS();
            //List<AccSettingsModel> acm = new List<AccSettingsModel>();
            //acm = acb.GetAllAccSettings(Login._bookyear);
            this.gridAccSettings.DataSource = null;
            this.gridAccSettings.DataSource = acm;

        }

        public void ClearDescriptors()
        {
            this.gridAccSettings.MasterTemplate.SortDescriptors.Clear();
            this.gridAccSettings.MasterTemplate.GroupDescriptors.Clear();
            this.gridAccSettings.MasterTemplate.FilterDescriptors.Clear();
        }
        public void SaveLayout(string filename)
        {
            this.gridAccSettings.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridAccSettings.LoadLayout(filterFolder + "\\" + filename);
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
        private void gridAccSettings_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                dictionary.Add(column.HeaderText, column.HeaderText);
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                column.MinWidth = column.Width;
            }
            for (int i = 0; i < gridAccSettings.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(gridAccSettings.Columns[i].HeaderText) != null)
                        gridAccSettings.Columns[i].HeaderText = resxSet.GetString(gridAccSettings.Columns[i].HeaderText);
                }

            }
            if (gridAccSettings.Columns.Count > 0)
            {
                //for number of rows
                this.gridAccSettings.SummaryRowsTop.Clear();
                gridAccSettings.MasterTemplate.EnablePaging = false;
                gridAccSettings.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridAccSettings.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridAccSettings.SummaryRowsTop.Add(summaryRowItem);
                GridViewSummaryRowItem summaryRowItem2 = new GridViewSummaryRowItem();

                gridAccSettings.Columns["idSettings"].IsVisible = false;
                gridAccSettings.Columns["labelSettings"].IsVisible = false;
                if (gridAccSettings.Columns != null && gridAccSettings.Columns.Count > 0)
                    gridAccSettings.Columns["beginBookYear"].FormatString = "{0: dd/MM/yyyy}";
                if (gridAccSettings.Columns != null && gridAccSettings.Columns.Count > 0)
                    gridAccSettings.Columns["endBookYear"].FormatString = "{0: dd/MM/yyyy}";
                //grid.Columns["nameUserCreated"].IsVisible = false;
                //grid.Columns["dtUserCreated"].IsVisible = false;
                //grid.Columns["idUserModifies"].IsVisible = false;
                //grid.Columns["nameUserModified"].IsVisible = false;
                //grid.Columns["dtUserModified"].IsVisible = false;
            }
        }

        private void gridAccSettings_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridAccSettings.MasterTemplate)
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
        private void gridAccSettings_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            AccSettingsModel selectedAccSettings = new AccSettingsModel();
            GridViewRowInfo info = this.gridAccSettings.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedAccSettings = (AccSettingsModel)info.DataBoundItem;

                if (selectedAccSettings != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedAccSettings = selectedAccSettings;
                    frmAccSettings frm = new frmAccSettings(this._clickedAccSettings);

                    frm.ShowDialog();
                    AccSettingsBUS arbus = new AccSettingsBUS();
                    List<AccSettingsModel> acm = new List<AccSettingsModel>();
                    acm = arbus.GetAllAccSettings(Login._bookyear);
                    this.SetDataPersonBinding1(acm); 
                    
                }
            }
        }
        private void gridAccSettings_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridAccSettings.MasterView.TableHeaderRow && e.CurrentRow != gridAccSettings.MasterView.TableFilteringRow && e.CurrentRow != gridAccSettings.MasterView.TableSearchRow)
            {
                AccSettingsModel selectedAccSettings = new AccSettingsModel();
                selectedAccSettings = (AccSettingsModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowAccSettings = selectedAccSettings;
                    RaiseStatusChanged(selectedAccSettings);
                }
            }
        }
        #endregion

        private void gridAccSettings_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.gridAccSettings.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    //RadMessageBox.Show("LALA");
                    AccSettingsModel model = (AccSettingsModel)this.gridAccSettings.CurrentRow.DataBoundItem;
                    frmAccSettings frm = new frmAccSettings(model);
                    frm.Show();
                    // e.SuppressKeyPress = true;
                    //e.Handled = true;
                    return;

                }

            }
        }

        private void gridAccSettings_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridAccSettings.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridAccSettings.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridAccSettings.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridAccSettings.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridAccSettings.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridAccSettings.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridAccSettings.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridAccSettings.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridAccSettings.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridAccSettings.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridAccSettings.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridAccSettings_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridAccSettings.IsInEditMode && !(this.gridAccSettings.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridAccSettings_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridAccSettings.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridAccSettings_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridAccSettings.FilterChanging -= gridAccSettings_FilterChanging;

                this.gridAccSettings.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridAccSettings.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridAccSettings.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridAccSettings.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridAccSettings.FilterChanging += gridAccSettings_FilterChanging;
            }  
        }

        private void gridAccSettings_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridAccSettings.EndEdit();
            }
        }



      

    }

    public class AccSettingsSelectedRowchanged : EventArgs
    {
         public AccSettingsModel a { get; set; }
    }


}
