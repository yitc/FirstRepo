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
    public partial class GridViewPeriod: System.Windows.Forms.UserControl, IBISGrid
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /// 
        public bool isChanged = false;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public event EventHandler<PeriodStatusSelectedRowchanged> PeriodStatusSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(PeriodModel perriod)
        {
            PeriodStatusSelectedRowchanged(this, new PeriodStatusSelectedRowchanged { period = perriod });
        }


        PeriodBUS ageBUS;
        private Telerik.WinControls.UI.RadGridView gridPeriod;
        private PeriodModel _selectedRowPeriod;
        private PeriodModel _clickedPeriod;
        
        // Folder u kome cuva filtere za tipove
        private string filterFolder;

        // Folder u kome cuva labele za tipove
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridViewPeriod()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\period")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\period"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\period")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\period"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\period");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\period");
            ageBUS = new PeriodBUS();

           InitializeComponent();        
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridPeriod = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPeriod.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridPeriod
            // 
            this.gridPeriod.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridPeriod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPeriod.EnableFastScrolling = true;
            this.gridPeriod.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridPeriod.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridPeriod.MasterTemplate.AllowAddNewRow = false;
            this.gridPeriod.MasterTemplate.AllowCellContextMenu = false;
            this.gridPeriod.MasterTemplate.AllowDeleteRow = false;
            this.gridPeriod.MasterTemplate.AllowEditRow = false;
            this.gridPeriod.MasterTemplate.AllowSearchRow = true;
            this.gridPeriod.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridPeriod.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridPeriod.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridPeriod.MasterTemplate.EnableFiltering = true;
            this.gridPeriod.MasterTemplate.EnablePaging = true;
            this.gridPeriod.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridPeriod.MasterTemplate.PageSize = 50;
            this.gridPeriod.MasterTemplate.ShowGroupedColumns = true;
            this.gridPeriod.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridPeriod.Name = "gridPeriod";
            this.gridPeriod.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridPeriod.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridPeriod.Size = new System.Drawing.Size(150, 150);
            this.gridPeriod.TabIndex = 0;
            this.gridPeriod.Text = "Period Grid";
            this.gridPeriod.ThemeName = "VisualStudio2012Light";
            this.gridPeriod.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridPeriod_CellBeginEdit);
            this.gridPeriod.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridPeriod_CellEditorInitialized);
            this.gridPeriod.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridPeriod_CellEndEdit);
            this.gridPeriod.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridAgeCategoy_CurrentRowChanged);
            this.gridPeriod.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridPeriod_CellClick);
            this.gridPeriod.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridAgeCategoy_CellDoubleClick);
            this.gridPeriod.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridAgeCategoy_GroupSummaryEvaluate);
            this.gridPeriod.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridAgeCategoy_DataBindingComplete);
            this.gridPeriod.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridPeriod_FilterChanging);
            this.gridPeriod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridAgeCategoy_KeyDown);
            // 
            // GridViewPeriod
            // 
            this.Controls.Add(this.gridPeriod);
            this.Name = "GridViewPeriod";
            ((System.ComponentModel.ISupportInitialize)(this.gridPeriod.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridPeriod)).EndInit();
            this.ResumeLayout(false);

        }

        #region GettersSetters
        //getters setters
        public bool bLoadTreeMenu
        {
            get { return _bLoadTreeMenu; }
            set { _bLoadTreeMenu = value; }
        }
        public Telerik.WinControls.UI.RadGridView PeriodGridView
        {
            get { return gridPeriod; }
        }

        public PeriodModel SelectedRowPeriod
        {
            get { return _selectedRowPeriod; }
        }
        public PeriodModel ClickedPeriod
        {
            get { return _clickedPeriod; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridPeriod.Columns; }
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
        public List<IModel> GetData(int selectedFilter, List<int> idLabelList, string lang)
        {
            return ageBUS.GetAllPeriod();
        }
        public List<FilterModel> ReturnFilters()
        {
            return Login._usersFilters;
        }
        public List<LabelModel> ReturnLabels()
        {
            return Login._usersLabels;
        }       
      
        public void SetDataPersonBinding(List<IModel> binding)
        {
            this.gridPeriod.DataSource = null;
            this.gridPeriod.DataSource = binding;

        }

        public void ClearDescriptors()
        {
            this.gridPeriod.MasterTemplate.SortDescriptors.Clear();
            this.gridPeriod.MasterTemplate.GroupDescriptors.Clear();
            this.gridPeriod.MasterTemplate.FilterDescriptors.Clear();
        }

        public void removeRow(PeriodModel rw)
        {
            using (gridPeriod.DeferRefresh())
            {
                GridViewRowInfo row = this.gridPeriod.Rows.Where(s => s.Cells["idPeriod"].Value.ToString() == rw.idPeriod.ToString()).FirstOrDefault();
                if (row != null)
                    this.gridPeriod.Rows.Remove(row);
            }
        }
        public void SaveLayout(string filename)
        {
            this.gridPeriod.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridPeriod.LoadLayout(filterFolder + "\\" + filename);
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

        public TypeModel ReturnRowData(TypeModel data)
        {
            return data;
        }
        #endregion Functions

        #region Grid Events
        // eventi na gridu
        private void gridAgeCategoy_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                dictionary.Add(column.HeaderText, column.HeaderText);
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 181);
                column.MinWidth = column.Width;

            }

            for (int i = 0; i < gridPeriod.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(gridPeriod.Columns[i].HeaderText)!=null)
                    gridPeriod.Columns[i].HeaderText = resxSet.GetString(gridPeriod.Columns[i].HeaderText);
                   
                }



            }
            if (gridPeriod.Columns.Count > 0)
            {
                //for number of rows
                this.gridPeriod.SummaryRowsTop.Clear();
                gridPeriod.MasterTemplate.EnablePaging = false;
                gridPeriod.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridPeriod.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridPeriod.SummaryRowsTop.Add(summaryRowItem);
                GridViewSummaryRowItem summaryRowItem2 = new GridViewSummaryRowItem();
            }

        }

        private void gridAgeCategoy_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
       {
           if (e.Parent == this.gridPeriod.MasterTemplate)
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
        private void gridAgeCategoy_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            PeriodModel selectedPeriod = new PeriodModel();
            GridViewRowInfo info = this.gridPeriod.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedPeriod = (PeriodModel)info.DataBoundItem;

                if (selectedPeriod != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedPeriod = selectedPeriod;
                    frmPeriod frm = new frmPeriod(this._clickedPeriod);
                    frm.ShowDialog();

                    if (frm.isChanged == true)
                    {
                    PeriodBUS abus = new PeriodBUS();


                    gridPeriod.DataSource = null;
                    gridPeriod.DataSource = abus.GetAllPeriod();
                    }
                }
            }
        }
        private void gridAgeCategoy_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridPeriod.MasterView.TableHeaderRow && e.CurrentRow != gridPeriod.MasterView.TableFilteringRow && e.CurrentRow != gridPeriod.MasterView.TableSearchRow)
            {
                PeriodModel selectedPeriod = new PeriodModel();
                selectedPeriod = (PeriodModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowPeriod = selectedPeriod;
                    RaiseStatusChanged(selectedPeriod);
                }
            }
        }
        #endregion

        private void gridAgeCategoy_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.gridPeriod.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    TypeModel model = (TypeModel)this.gridPeriod.CurrentRow.DataBoundItem;
                    frmType frm = new frmType(model);
                    frm.ShowDialog();
                    return;
                }
            }
        }

        private void gridPeriod_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridPeriod.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridPeriod.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPeriod.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPeriod.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPeriod.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPeriod.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPeriod.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPeriod.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridPeriod.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridPeriod.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridPeriod.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridPeriod.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridPeriod_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridPeriod.IsInEditMode && !(this.gridPeriod.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridPeriod_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridPeriod.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridPeriod_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridPeriod.FilterChanging -= gridPeriod_FilterChanging;

                this.gridPeriod.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridPeriod.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridPeriod.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridPeriod.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridPeriod.FilterChanging += gridPeriod_FilterChanging;
            }  
        }

        private void gridPeriod_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridPeriod.EndEdit();
            }
        }




    }

    public class PeriodStatusSelectedRowchanged : EventArgs
    {
        public PeriodModel period { get; set; }
    }


}
