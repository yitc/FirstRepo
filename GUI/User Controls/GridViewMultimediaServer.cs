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
    public partial class GridViewMultimediaServer : System.Windows.Forms.UserControl, IBISGrid
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

        public event EventHandler<MultimediaServerSelectedRowchanged> MultimediaServerSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(MultimediaServersModel art)
        {
            MultimediaServerSelectedRowchanged(this, new MultimediaServerSelectedRowchanged { a = art });
        }


        MultimediaServerBUS MultimediaServerBUS;
        private Telerik.WinControls.UI.RadGridView gridMultimediaServer;
        private MultimediaServersModel _selectedRowMultimediaServer;
        private MultimediaServersModel _clickedMultimediaServer;
        public List<IModel> modelData;
        // Folder u kome cuva filtere za MultimediaServer
        private string filterFolder;

        // Folder u kome cuva labele za MultimediaServer
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridViewMultimediaServer()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\multimediaserver")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\multimediaserver"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\multimediaserver")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\multimediaserver"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\multimediaserver");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\multimediaserver");
            MultimediaServerBUS = new MultimediaServerBUS();

            InitializeComponent();        
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridMultimediaServer = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridMultimediaServer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMultimediaServer.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridMultimediaServer
            // 
            this.gridMultimediaServer.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridMultimediaServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMultimediaServer.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridMultimediaServer.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridMultimediaServer.MasterTemplate.AllowAddNewRow = false;
            this.gridMultimediaServer.MasterTemplate.AllowCellContextMenu = false;
            this.gridMultimediaServer.MasterTemplate.AllowDeleteRow = false;
            this.gridMultimediaServer.MasterTemplate.AllowEditRow = false;
            this.gridMultimediaServer.MasterTemplate.AllowSearchRow = true;
            this.gridMultimediaServer.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridMultimediaServer.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridMultimediaServer.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridMultimediaServer.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridMultimediaServer.MasterTemplate.EnableFiltering = true;
            this.gridMultimediaServer.MasterTemplate.EnablePaging = true;
            this.gridMultimediaServer.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridMultimediaServer.MasterTemplate.PageSize = 50;
            this.gridMultimediaServer.MasterTemplate.ShowGroupedColumns = true;
            this.gridMultimediaServer.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridMultimediaServer.Name = "gridMultimediaServer";
            this.gridMultimediaServer.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridMultimediaServer.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridMultimediaServer.Size = new System.Drawing.Size(150, 150);
            this.gridMultimediaServer.TabIndex = 0;
            this.gridMultimediaServer.Text = "gridMultimediaServer";
            this.gridMultimediaServer.ThemeName = "VisualStudio2012Light";
            this.gridMultimediaServer.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridMultimediaServer_CellBeginEdit);
            this.gridMultimediaServer.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridMultimediaServer_CellEditorInitialized);
            this.gridMultimediaServer.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridMultimediaServer_CellEndEdit);
            this.gridMultimediaServer.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridMultimediaServer_CurrentRowChanged);
            this.gridMultimediaServer.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridMultimediaServer_CellClick);
            this.gridMultimediaServer.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridMultimediaServer_CellDoubleClick);
            this.gridMultimediaServer.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridMultimediaServer_GroupSummaryEvaluate);
            this.gridMultimediaServer.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridMultimediaServer_DataBindingComplete);
            this.gridMultimediaServer.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridMultimediaServer_FilterChanging);
            this.gridMultimediaServer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridMultimediaServer_KeyDown);
            // 
            // GridViewMultimediaServer
            // 
            this.Controls.Add(this.gridMultimediaServer);
            this.Name = "GridViewMultimediaServer";
            ((System.ComponentModel.ISupportInitialize)(this.gridMultimediaServer.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMultimediaServer)).EndInit();
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
            get { return gridMultimediaServer; }
        }

        public MultimediaServersModel SelectedRowMultimediaServer
        {
            get { return _selectedRowMultimediaServer; }
        }
        public MultimediaServersModel ClickedMultimediaServer
        {
            get { return _clickedMultimediaServer; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridMultimediaServer.Columns; }
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
            return MultimediaServerBUS.GetAllMultimedia();  
        }
        public List<FilterModel> ReturnFilters()
        {
            return Login._multimediaServerFilters;
        }
        public List<LabelModel> ReturnLabels()
        {
            return Login._multimediaServerLabels;
        }       
      
        public void SetDataPersonBinding(List<IModel> binding)
        {
            this.gridMultimediaServer.DataSource = null;
            this.gridMultimediaServer.DataSource = binding;

        }

        public void ClearDescriptors()
        {
            this.gridMultimediaServer.MasterTemplate.SortDescriptors.Clear();
            this.gridMultimediaServer.MasterTemplate.GroupDescriptors.Clear();
            this.gridMultimediaServer.MasterTemplate.FilterDescriptors.Clear();
        }

        public void removeRow(MultimediaServersModel rw)
        {
            using (gridMultimediaServer.DeferRefresh())
            {
                GridViewRowInfo row = this.gridMultimediaServer.Rows.Where(s => s.Cells["idServer"].Value.ToString() == rw.idServer.ToString()).FirstOrDefault();
                if (row != null)
                    this.gridMultimediaServer.Rows.Remove(row);
            }
        }
        public void SaveLayout(string filename)
        {
            this.gridMultimediaServer.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridMultimediaServer.LoadLayout(filterFolder + "\\" + filename);
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
        private void gridMultimediaServer_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                dictionary.Add(column.HeaderText, column.HeaderText);
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                column.MinWidth = column.Width;
            }
            for (int i = 0; i < gridMultimediaServer.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(gridMultimediaServer.Columns[i].HeaderText) != null)
                        gridMultimediaServer.Columns[i].HeaderText = resxSet.GetString(gridMultimediaServer.Columns[i].HeaderText);
                }

            }
            if (gridMultimediaServer.Columns.Count > 0)
            {
                //for number of rows
                this.gridMultimediaServer.SummaryRowsTop.Clear();
                gridMultimediaServer.MasterTemplate.EnablePaging = false;
                gridMultimediaServer.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridMultimediaServer.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridMultimediaServer.SummaryRowsTop.Add(summaryRowItem);
                GridViewSummaryRowItem summaryRowItem2 = new GridViewSummaryRowItem();

                //grid.Columns["idUserCreated"].IsVisible = false;
                //grid.Columns["nameUserCreated"].IsVisible = false;
                //grid.Columns["dtUserCreated"].IsVisible = false;
                //grid.Columns["idUserModified"].IsVisible = false;
                //grid.Columns["nameUserModified"].IsVisible = false;
                //grid.Columns["dtUserModified"].IsVisible = false;
            }
        }

        private void gridMultimediaServer_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridMultimediaServer.MasterTemplate)
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
        private void gridMultimediaServer_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            MultimediaServersModel selectedMultimediaServer = new MultimediaServersModel();
            GridViewRowInfo info = this.gridMultimediaServer.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedMultimediaServer = (MultimediaServersModel)info.DataBoundItem;

                if (selectedMultimediaServer != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedMultimediaServer = selectedMultimediaServer;
                    frmMultimediaServer frm = new frmMultimediaServer(this._clickedMultimediaServer);

                    frm.ShowDialog();
                    MultimediaServerBUS arbus = new MultimediaServerBUS();
                    modelData = arbus.GetAllMultimedia(); 
                    this.SetDataPersonBinding(modelData);  
                }
            }
        }
        private void gridMultimediaServer_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridMultimediaServer.MasterView.TableHeaderRow && e.CurrentRow != gridMultimediaServer.MasterView.TableFilteringRow && e.CurrentRow != gridMultimediaServer.MasterView.TableSearchRow)
            {
                MultimediaServersModel selectedMultimediaServer = new MultimediaServersModel();
                selectedMultimediaServer = (MultimediaServersModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowMultimediaServer = selectedMultimediaServer;
                    RaiseStatusChanged(selectedMultimediaServer);
                }
            }
        }
        #endregion

        private void gridMultimediaServer_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.gridMultimediaServer.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    MultimediaServersModel model = (MultimediaServersModel)this.gridMultimediaServer.CurrentRow.DataBoundItem;
                    frmMultimediaServer frm = new frmMultimediaServer(model);
                    frm.Show();
                    return;
                }
            }
        }

        private void gridMultimediaServer_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridMultimediaServer.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridMultimediaServer.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridMultimediaServer.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridMultimediaServer.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridMultimediaServer.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridMultimediaServer.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridMultimediaServer.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridMultimediaServer.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }
        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridMultimediaServer.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridMultimediaServer.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridMultimediaServer.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridMultimediaServer.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridMultimediaServer_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridMultimediaServer.IsInEditMode && !(this.gridMultimediaServer.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridMultimediaServer_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridMultimediaServer.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridMultimediaServer_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridMultimediaServer.FilterChanging -= gridMultimediaServer_FilterChanging;

                this.gridMultimediaServer.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridMultimediaServer.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridMultimediaServer.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridMultimediaServer.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridMultimediaServer.FilterChanging += gridMultimediaServer_FilterChanging;
            }  
        }

        private void gridMultimediaServer_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridMultimediaServer.EndEdit();
            }
        }






      

    }

    public class MultimediaServerSelectedRowchanged : EventArgs
    {
         public MultimediaServersModel a { get; set; }
    }


}
