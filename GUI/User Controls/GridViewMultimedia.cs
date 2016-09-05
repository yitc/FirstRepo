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
    public partial class GridViewMultimedia : System.Windows.Forms.UserControl, IBISGrid
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

        public event EventHandler<MultimediaSelectedRowchanged> MultimediaSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(MultimediaModel art)
        {
            MultimediaSelectedRowchanged(this, new MultimediaSelectedRowchanged { a = art });
        }


        MultimediaBUS MultimediaBUS;
        private Telerik.WinControls.UI.RadGridView gridMultimedia;
        private MultimediaModel _selectedRowMultimedia;
        private MultimediaModel _clickedMultimedia;
        public List<IModel> modelData;
        // Folder u kome cuva filtere za Multimedia
        private string filterFolder;

        // Folder u kome cuva labele za Multimedia
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridViewMultimedia()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\multimedia")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\multimedia"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\multimedia")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\multimedia"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\multimedia");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\multimedia");
            MultimediaBUS = new MultimediaBUS();

            InitializeComponent();        
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridMultimedia = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridMultimedia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMultimedia.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridMultimedia
            // 
            this.gridMultimedia.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridMultimedia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMultimedia.EnableFastScrolling = true;
            this.gridMultimedia.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridMultimedia.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridMultimedia.MasterTemplate.AllowAddNewRow = false;
            this.gridMultimedia.MasterTemplate.AllowCellContextMenu = false;
            this.gridMultimedia.MasterTemplate.AllowDeleteRow = false;
            this.gridMultimedia.MasterTemplate.AllowEditRow = false;
            this.gridMultimedia.MasterTemplate.AllowSearchRow = true;
            this.gridMultimedia.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridMultimedia.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridMultimedia.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridMultimedia.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridMultimedia.MasterTemplate.EnableFiltering = true;
            this.gridMultimedia.MasterTemplate.EnablePaging = true;
            this.gridMultimedia.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridMultimedia.MasterTemplate.PageSize = 50;
            this.gridMultimedia.MasterTemplate.ShowGroupedColumns = true;
            this.gridMultimedia.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridMultimedia.Name = "gridMultimedia";
            this.gridMultimedia.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridMultimedia.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridMultimedia.Size = new System.Drawing.Size(150, 150);
            this.gridMultimedia.TabIndex = 0;
            this.gridMultimedia.Text = "gridMultimedia";
            this.gridMultimedia.ThemeName = "VisualStudio2012Light";
            this.gridMultimedia.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridMultimedia_CellBeginEdit);
            this.gridMultimedia.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridMultimedia_CellEditorInitialized);
            this.gridMultimedia.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridMultimedia_CellEndEdit);
            this.gridMultimedia.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridMultimedia_CurrentRowChanged);
            this.gridMultimedia.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridMultimedia_CellClick);
            this.gridMultimedia.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridMultimedia_CellDoubleClick);
            this.gridMultimedia.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridMultimedia_GroupSummaryEvaluate);
            this.gridMultimedia.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridMultimedia_DataBindingComplete);
            this.gridMultimedia.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridMultimedia_FilterChanging);
            this.gridMultimedia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridMultimedia_KeyDown);
            // 
            // GridViewMultimedia
            // 
            this.Controls.Add(this.gridMultimedia);
            this.Name = "GridViewMultimedia";
            ((System.ComponentModel.ISupportInitialize)(this.gridMultimedia.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMultimedia)).EndInit();
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
            get { return gridMultimedia; }
        }

        public MultimediaModel SelectedRowMultimedia
        {
            get { return _selectedRowMultimedia; }
        }
        public MultimediaModel ClickedMultimedia
        {
            get { return _clickedMultimedia; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridMultimedia.Columns; }
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
            return MultimediaBUS.GetAllMultimedia();  
        }
        public List<FilterModel> ReturnFilters()
        {
            return Login._multimediaFilters;
        }
        public List<LabelModel> ReturnLabels()
        {
            return Login._multimediaLabels;
        }       
      
        public void SetDataPersonBinding(List<IModel> binding)
        {
            this.gridMultimedia.DataSource = null;
            this.gridMultimedia.DataSource = binding;

        }

        public void ClearDescriptors()
        {
            this.gridMultimedia.MasterTemplate.SortDescriptors.Clear();
            this.gridMultimedia.MasterTemplate.GroupDescriptors.Clear();
            this.gridMultimedia.MasterTemplate.FilterDescriptors.Clear();
        }
        public void SaveLayout(string filename)
        {
            this.gridMultimedia.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridMultimedia.LoadLayout(filterFolder + "\\" + filename);
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
        private void gridMultimedia_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                dictionary.Add(column.HeaderText, column.HeaderText);
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                column.MinWidth = column.Width;
            }
            for (int i = 0; i < gridMultimedia.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(gridMultimedia.Columns[i].HeaderText) != null)
                        gridMultimedia.Columns[i].HeaderText = resxSet.GetString(gridMultimedia.Columns[i].HeaderText);
                }

            }
            if (gridMultimedia.Columns.Count > 0)
            {
                //for number of rows
                this.gridMultimedia.SummaryRowsTop.Clear();
                gridMultimedia.MasterTemplate.EnablePaging = false;
                gridMultimedia.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridMultimedia.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridMultimedia.SummaryRowsTop.Add(summaryRowItem);
                GridViewSummaryRowItem summaryRowItem2 = new GridViewSummaryRowItem();

                gridMultimedia.Columns["idClient"].IsVisible = false;
                gridMultimedia.Columns["idServer"].IsVisible = false;
                gridMultimedia.Columns["idPeriod"].IsVisible = false;
                gridMultimedia.Columns["idUserCreated"].IsVisible = false;
                gridMultimedia.Columns["dtUserCreated"].IsVisible = false;
                gridMultimedia.Columns["idUserModified"].IsVisible = false;
                gridMultimedia.Columns["dtUserModified"].IsVisible = false;

            }
        }

        private void gridMultimedia_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridMultimedia.MasterTemplate)
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
        private void gridMultimedia_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            MultimediaModel selectedMultimedia = new MultimediaModel();
            GridViewRowInfo info = this.gridMultimedia.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedMultimedia = (MultimediaModel)info.DataBoundItem;

                if (selectedMultimedia != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedMultimedia = selectedMultimedia;
                    frmMultimedia frm = new frmMultimedia(this._clickedMultimedia);

                    frm.ShowDialog();
                    //MultimediaBUS arbus = new MultimediaBUS();
                    //modelData = arbus.GetAllMultimedia(); ;
                    //this.SetDataPersonBinding(modelData);  
                }
            }
        }
        private void gridMultimedia_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridMultimedia.MasterView.TableHeaderRow && e.CurrentRow != gridMultimedia.MasterView.TableFilteringRow && e.CurrentRow != gridMultimedia.MasterView.TableSearchRow)
            {
                MultimediaModel selectedMultimedia = new MultimediaModel();
                selectedMultimedia = (MultimediaModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowMultimedia = selectedMultimedia;
                    RaiseStatusChanged(selectedMultimedia);
                }
            }
        }
        #endregion

        private void gridMultimedia_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyData == Keys.Enter)
            {
                MultimediaModel selectedMultimedia = new MultimediaModel();
                GridViewRowInfo info = this.gridMultimedia.CurrentRow;

                if (info != null && info.Index >= 0)
                {
                    selectedMultimedia = (MultimediaModel)info.DataBoundItem;

                    if (selectedMultimedia != null)
                    {
                        //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                        this._clickedMultimedia = selectedMultimedia;
                        frmMultimedia frm = new frmMultimedia(this._clickedMultimedia);

                        frm.ShowDialog();
                        //MultimediaBUS arbus = new MultimediaBUS();
                        //modelData = arbus.GetAllMultimedia(); ;
                        //this.SetDataPersonBinding(modelData);
                    }
                }
            }

            
        }

        private void gridMultimedia_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridMultimedia.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridMultimedia.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridMultimedia.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridMultimedia.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridMultimedia.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridMultimedia.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridMultimedia.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridMultimedia.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridMultimedia.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridMultimedia.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridMultimedia.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridMultimedia_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridMultimedia.IsInEditMode && !(this.gridMultimedia.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridMultimedia_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridMultimedia.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridMultimedia_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridMultimedia.FilterChanging -= gridMultimedia_FilterChanging;

                this.gridMultimedia.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridMultimedia.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridMultimedia.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridMultimedia.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridMultimedia.FilterChanging += gridMultimedia_FilterChanging;
            }
        }

        private void gridMultimedia_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridMultimedia.EndEdit();
            }
        }



      

    }

    public class MultimediaSelectedRowchanged : EventArgs
    {
         public MultimediaModel a { get; set; }
    }


}
