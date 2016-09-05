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
    public partial class GridViewVoluntaryFunc : System.Windows.Forms.UserControl, IBISGrid
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
        
        public event EventHandler<VoluntaryFunctionSelectedRowchanged> VoluntaryFunctionSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(MedicalVoluntaryQuestModel vodd)
        {
            VoluntaryFunctionSelectedRowchanged(this, new VoluntaryFunctionSelectedRowchanged { vod = vodd });
        }


        VolontaryFunctionBUS medBUS;
        private Telerik.WinControls.UI.RadGridView gridVoluntary;
        private MedicalVoluntaryQuestModel _selectedRowVoluntary;
        private MedicalVoluntaryQuestModel _clickedVoluntary;

        public List<IModel> modelData;
        
        // Folder u kome cuva filtere za voluntary function
        private string filterFolder;

        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridViewVoluntaryFunc()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\voluntaryFunc")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\voluntaryFunc"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\voluntaryFunc")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\voluntaryFunc"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\voluntaryFunc");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\voluntaryFunc");
            medBUS = new VolontaryFunctionBUS();

            InitializeComponent();        
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridVoluntary = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridVoluntary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVoluntary.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridVoluntary
            // 
            this.gridVoluntary.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridVoluntary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridVoluntary.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridVoluntary.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridVoluntary.MasterTemplate.AllowAddNewRow = false;
            this.gridVoluntary.MasterTemplate.AllowCellContextMenu = false;
            this.gridVoluntary.MasterTemplate.AllowDeleteRow = false;
            this.gridVoluntary.MasterTemplate.AllowEditRow = false;
            this.gridVoluntary.MasterTemplate.AllowSearchRow = true;
            this.gridVoluntary.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridVoluntary.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridVoluntary.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridVoluntary.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridVoluntary.MasterTemplate.EnableFiltering = true;
            this.gridVoluntary.MasterTemplate.EnablePaging = true;
            this.gridVoluntary.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridVoluntary.MasterTemplate.PageSize = 50;
            this.gridVoluntary.MasterTemplate.ShowGroupedColumns = true;
            this.gridVoluntary.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridVoluntary.Name = "gridVoluntary";
            this.gridVoluntary.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridVoluntary.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridVoluntary.Size = new System.Drawing.Size(150, 150);
            this.gridVoluntary.TabIndex = 0;
            this.gridVoluntary.Text = "Voluntary Grid Function";
            this.gridVoluntary.ThemeName = "VisualStudio2012Light";
            this.gridVoluntary.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridVoluntary_CellBeginEdit);
            this.gridVoluntary.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridVoluntary_CellEditorInitialized);
            this.gridVoluntary.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridVoluntary_CellEndEdit);
            this.gridVoluntary.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridVoluntary_CurrentRowChanged);
            this.gridVoluntary.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridVoluntary_CellClick);
            this.gridVoluntary.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridVoluntary_CellDoubleClick);
            this.gridVoluntary.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridVoluntary_GroupSummaryEvaluate);
            this.gridVoluntary.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridVoluntary_DataBindingComplete);
            this.gridVoluntary.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridVoluntary_FilterChanging);
            this.gridVoluntary.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridVoluntary_KeyDown);
            // 
            // GridViewVoluntaryFunc
            // 
            this.Controls.Add(this.gridVoluntary);
            this.Name = "GridViewVoluntaryFunc";
            ((System.ComponentModel.ISupportInitialize)(this.gridVoluntary.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridVoluntary)).EndInit();
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
            get { return gridVoluntary; }
        }

        public MedicalVoluntaryQuestModel SelectedRowMedical
        {
            get { return _selectedRowVoluntary; }
        }
        public MedicalVoluntaryQuestModel ClickedVoluntary
        {
            get { return _clickedVoluntary; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridVoluntary.Columns; }
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
            return medBUS.GetVoluntaryQuestDetails(idLabelList);
        }
        public List<FilterModel> ReturnFilters()
        {
            return Login._voluntaryFilters;
        }
        public List<LabelModel> ReturnLabels()
        {
            return Login._voluntaryLabels;
        }       
      
        public void SetDataPersonBinding(List<IModel> binding)
        {
            this.gridVoluntary.DataSource = null;
            this.gridVoluntary.DataSource = binding;

        }

        public void ClearDescriptors()
        {
            this.gridVoluntary.MasterTemplate.SortDescriptors.Clear();
            this.gridVoluntary.MasterTemplate.GroupDescriptors.Clear();
            this.gridVoluntary.MasterTemplate.FilterDescriptors.Clear();
        }

        public void removeRow(MedicalVoluntaryQuestModel rw)
        {
            using (gridVoluntary.DeferRefresh())
            {
                GridViewRowInfo row = this.gridVoluntary.Rows.Where(s => s.Cells["idQuestGroup"].Value.ToString() == rw.idQuestGroup.ToString()).FirstOrDefault();
                if (row != null)
                    this.gridVoluntary.Rows.Remove(row);
            }
        }
        public void SaveLayout(string filename)
        {
            this.gridVoluntary.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridVoluntary.LoadLayout(filterFolder + "\\" + filename);
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

        public UsersAllModel ReturnRowData(UsersAllModel data)
        {
            return data;
        }
        #endregion Functions

        #region Grid Events
        // eventi na gridu
        private void gridVoluntary_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                dictionary.Add(column.HeaderText, column.HeaderText);
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                column.MinWidth = column.Width;
            }
            if (grid.Columns.Count > 0)
            {
                gridVoluntary.Columns["idQuest"].IsVisible = false;
                gridVoluntary.Columns["idAns"].IsVisible = false;
                gridVoluntary.Columns["idAnsType"].IsVisible = false;
                gridVoluntary.Columns["questSort"].IsVisible = false;
                gridVoluntary.Columns["ansSort"].IsVisible = false;
            }
            for (int i = 0; i < gridVoluntary.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(gridVoluntary.Columns[i].HeaderText) != null)
                        gridVoluntary.Columns[i].HeaderText = resxSet.GetString(gridVoluntary.Columns[i].HeaderText);
                }

            }
            if (gridVoluntary.Columns.Count > 0)
            {
                //for number of rows
                this.gridVoluntary.SummaryRowsTop.Clear();
                gridVoluntary.MasterTemplate.EnablePaging = false;
                gridVoluntary.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridVoluntary.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridVoluntary.SummaryRowsTop.Add(summaryRowItem);
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

        private void gridVoluntary_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridVoluntary.MasterTemplate)
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
        private void gridVoluntary_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            MedicalVoluntaryQuestModel selectedMedical = new MedicalVoluntaryQuestModel();
            GridViewRowInfo info = this.gridVoluntary.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedMedical = (MedicalVoluntaryQuestModel)info.DataBoundItem;

                if (selectedMedical != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedVoluntary = selectedMedical;
                    frmVoluntaryFunc frm = new frmVoluntaryFunc(this._clickedVoluntary);

                    frm.ShowDialog();

                    modelData = medBUS.GetVoluntaryQuestDetails(MainForm.idLabelList);
                    this.SetDataPersonBinding(modelData);
                    if (File.Exists(filterFolder + "\\Standard.xml"))
                    {
                        this.gridVoluntary.LoadLayout(filterFolder + "\\Standard.xml");
                    }
                }
            }
        }
        private void gridVoluntary_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridVoluntary.MasterView.TableHeaderRow && e.CurrentRow != gridVoluntary.MasterView.TableFilteringRow && e.CurrentRow != gridVoluntary.MasterView.TableSearchRow)
            {
                MedicalVoluntaryQuestModel selectedVoluntary = new MedicalVoluntaryQuestModel();
                selectedVoluntary = (MedicalVoluntaryQuestModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowVoluntary = selectedVoluntary;
                    RaiseStatusChanged(selectedVoluntary);
                }
            }
        }
        #endregion

        private void gridVoluntary_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.gridVoluntary.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    MedicalVoluntaryQuestModel model = (MedicalVoluntaryQuestModel)this.gridVoluntary.CurrentRow.DataBoundItem;
                    frmVoluntaryFunc frm = new frmVoluntaryFunc(model);
                    frm.Show();
                    return;
                }
            }
        }

        private void gridVoluntary_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridVoluntary.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridVoluntary.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridVoluntary.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridVoluntary.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridVoluntary.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridVoluntary.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridVoluntary.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridVoluntary.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }
        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridVoluntary.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridVoluntary.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridVoluntary.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridVoluntary.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridVoluntary_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridVoluntary.IsInEditMode && !(this.gridVoluntary.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridVoluntary_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridVoluntary.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridVoluntary_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridVoluntary.FilterChanging -= gridVoluntary_FilterChanging;

                this.gridVoluntary.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridVoluntary.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridVoluntary.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridVoluntary.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridVoluntary.FilterChanging += gridVoluntary_FilterChanging;
            }  
        }

        private void gridVoluntary_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridVoluntary.EndEdit();
            }
        }


      

    }

    public class VoluntaryFunctionSelectedRowchanged : EventArgs
    {
         public MedicalVoluntaryQuestModel vod { get; set; }
    }


}
