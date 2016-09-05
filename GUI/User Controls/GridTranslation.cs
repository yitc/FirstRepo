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
    public partial class GridViewTranslation : System.Windows.Forms.UserControl, IBISGrid
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

        public event EventHandler<TranslationStatusSelectedRowchanged> TranslationStatusSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(TranslationModel translate)
        {
            TranslationStatusSelectedRowchanged(this, new TranslationStatusSelectedRowchanged { translation = translate });
        }

        
        TranslationBUS translationBUS;
        private Telerik.WinControls.UI.RadGridView gridTranslation;
        private TranslationModel _selectedRowTranslation;
        private TranslationModel _clickedTranslation;
        
        // Folder u kome cuva filtere za translation
        private string filterFolder;

        // Folder u kome cuva labele za translation
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridViewTranslation()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\translation")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\translation"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\translation")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\translation"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\translation");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\translation");
            translationBUS = new TranslationBUS();

           InitializeComponent();        
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridTranslation = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridTranslation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTranslation.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridTranslation
            // 
            this.gridTranslation.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridTranslation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTranslation.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridTranslation.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridTranslation.MasterTemplate.AllowAddNewRow = false;
            this.gridTranslation.MasterTemplate.AllowCellContextMenu = false;
            this.gridTranslation.MasterTemplate.AllowDeleteRow = false;
            this.gridTranslation.MasterTemplate.AllowEditRow = false;
            this.gridTranslation.MasterTemplate.AllowSearchRow = true;
            this.gridTranslation.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridTranslation.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridTranslation.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridTranslation.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridTranslation.MasterTemplate.EnableFiltering = true;
            this.gridTranslation.MasterTemplate.EnablePaging = true;
            this.gridTranslation.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridTranslation.MasterTemplate.PageSize = 50;
            this.gridTranslation.MasterTemplate.ShowGroupedColumns = true;
            this.gridTranslation.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridTranslation.Name = "gridTranslation";
            this.gridTranslation.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridTranslation.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridTranslation.Size = new System.Drawing.Size(150, 150);
            this.gridTranslation.TabIndex = 0;
            this.gridTranslation.Text = "Users Grid";
            this.gridTranslation.ThemeName = "VisualStudio2012Light";
            this.gridTranslation.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridTranslation_CellBeginEdit);
            this.gridTranslation.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridTranslation_CellEditorInitialized);
            this.gridTranslation.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridTranslation_CellEndEdit);
            this.gridTranslation.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridTranslation_CurrentRowChanged);
            this.gridTranslation.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridTranslation_CellClick);
            this.gridTranslation.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridTranslation_CellDoubleClick);
            this.gridTranslation.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridTranslation_GroupSummaryEvaluate);
            this.gridTranslation.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridTranslation_DataBindingComplete);
            this.gridTranslation.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridTranslation_FilterChanging);
            this.gridTranslation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridTranslation_KeyDown);
            // 
            // GridViewTranslation
            // 
            this.Controls.Add(this.gridTranslation);
            this.Name = "GridViewTranslation";
            ((System.ComponentModel.ISupportInitialize)(this.gridTranslation.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTranslation)).EndInit();
            this.ResumeLayout(false);

        }

        #region GettersSetters
        //getters setters
        public bool bLoadTreeMenu
        {
            get { return _bLoadTreeMenu; }
            set { _bLoadTreeMenu = value; }
        }
        public Telerik.WinControls.UI.RadGridView TranslationGridView
        {
            get { return gridTranslation; }
        }

        public TranslationModel SelectedRowTranslation
        {
            get { return _selectedRowTranslation; }
        }
        public TranslationModel ClickedTranslation
        {
            get { return _clickedTranslation; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridTranslation.Columns; }
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
            return translationBUS.GetAllTranslation(lang);
        }
        public List<FilterModel> ReturnFilters()
        {
            return Login._translationFilters;
        }
        public List<LabelModel> ReturnLabels()
        {
            return Login._translationLabels;
        }       
      
        public void SetDataPersonBinding(List<IModel> binding)
        {
            this.gridTranslation.DataSource = null;
            this.gridTranslation.DataSource = binding;

        }

        public void ClearDescriptors()
        {
            this.gridTranslation.MasterTemplate.SortDescriptors.Clear();
            this.gridTranslation.MasterTemplate.GroupDescriptors.Clear();
            this.gridTranslation.MasterTemplate.FilterDescriptors.Clear();
        }
        public void SaveLayout(string filename)
        {
            this.gridTranslation.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridTranslation.LoadLayout(filterFolder + "\\" + filename);
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

        public TranslationModel ReturnRowData(TranslationModel data)
        {
            return data;
        }
        #endregion Functions

        #region Grid Events
        // eventi na gridu
       private void gridTranslation_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
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

           
            if (gridTranslation.Columns.Count > 0)
            {
                grid.Columns["idLang"].IsVisible = false;

                //for number of rows
                this.gridTranslation.SummaryRowsTop.Clear();
                gridTranslation.MasterTemplate.EnablePaging = false;
                gridTranslation.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridTranslation.Columns[1].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridTranslation.SummaryRowsTop.Add(summaryRowItem);
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

            if (gridTranslation.Columns != null)
            {
                if (gridTranslation.RowCount > 0)
                {
                    this.gridTranslation.Columns["dtString"].FormatString = "{0: dd-MM-yyyy}";
                   

                }
            }

        }

       private void gridTranslation_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
       {
           if (e.Parent == this.gridTranslation.MasterTemplate)
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
       private void gridTranslation_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            TranslationModel selectedTranslation = new TranslationModel();
            GridViewRowInfo info = this.gridTranslation.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedTranslation = (TranslationModel)info.DataBoundItem;

                if (selectedTranslation != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedTranslation = selectedTranslation;
                    frmTranslation frm = new frmTranslation(this._clickedTranslation);
                    frm.Show();
                }
            }
        }
        private void gridTranslation_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridTranslation.MasterView.TableHeaderRow && e.CurrentRow != gridTranslation.MasterView.TableFilteringRow && e.CurrentRow != gridTranslation.MasterView.TableSearchRow)
            {
                TranslationModel selectedTranslation = new TranslationModel();
                selectedTranslation = (TranslationModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowTranslation = selectedTranslation;
                    RaiseStatusChanged(selectedTranslation);
                }
            }
        }
        #endregion

        private void gridTranslation_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.gridTranslation.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    TranslationModel model = (TranslationModel)this.gridTranslation.CurrentRow.DataBoundItem;
                    frmTranslation frm = new frmTranslation(model);
                    frm.Show();
                    return;
                }
            }
        }

        private void gridTranslation_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridTranslation.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridTranslation.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridTranslation.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridTranslation.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridTranslation.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridTranslation.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridTranslation.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridTranslation.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridTranslation.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridTranslation.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridTranslation.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridTranslation_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridTranslation.IsInEditMode && !(this.gridTranslation.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridTranslation_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridTranslation.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridTranslation_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridTranslation.FilterChanging -= gridTranslation_FilterChanging;

                this.gridTranslation.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridTranslation.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridTranslation.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridTranslation.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridTranslation.FilterChanging += gridTranslation_FilterChanging;
            }  
        }

        private void gridTranslation_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridTranslation.EndEdit();
            }
        }

      

    }

    public class TranslationStatusSelectedRowchanged : EventArgs
    {
        public TranslationModel translation { get; set; }
    }


}
