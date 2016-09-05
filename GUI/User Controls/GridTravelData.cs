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
    public partial class GridTravelData : System.Windows.Forms.UserControl, IBISGrid
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

        public event EventHandler<TravelDataStatusSelectedRowchanged> TravelDataStatusSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(CodeTrainingFromVolFeaturesModel tyype)
        {
            TravelDataStatusSelectedRowchanged(this, new TravelDataStatusSelectedRowchanged { type = tyype });
        }

        
        TravelDataBUS travelDataBUS;
        private Telerik.WinControls.UI.RadGridView gridType;
        private CodeTrainingFromVolFeaturesModel _selectedRowType;
        private CodeTrainingFromVolFeaturesModel _clickedType;
        private CodeTrainingFromVolFeaturesModel _clickedTypeForCode;
        
        // Folder u kome cuva filtere za tipove
        private string filterFolder;

        // Folder u kome cuva labele za tipove
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridTravelData()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\traveldata")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\traveldata"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\traveldata")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\traveldata"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\traveldata");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\traveldata");
            travelDataBUS = new TravelDataBUS();

           InitializeComponent();        
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridType = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridType.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridType
            // 
            this.gridType.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridType.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridType.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridType.MasterTemplate.AllowAddNewRow = false;
            this.gridType.MasterTemplate.AllowCellContextMenu = false;
            this.gridType.MasterTemplate.AllowDeleteRow = false;
            this.gridType.MasterTemplate.AllowEditRow = false;
            this.gridType.MasterTemplate.AllowSearchRow = true;
            this.gridType.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridType.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridType.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridType.MasterTemplate.EnableFiltering = true;
            this.gridType.MasterTemplate.EnablePaging = true;
            this.gridType.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridType.MasterTemplate.PageSize = 50;
            this.gridType.MasterTemplate.ShowGroupedColumns = true;
            this.gridType.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridType.Name = "gridType";
            this.gridType.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridType.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridType.Size = new System.Drawing.Size(150, 150);
            this.gridType.TabIndex = 0;
            this.gridType.Text = "Type Grid";
            this.gridType.ThemeName = "VisualStudio2012Light";
            this.gridType.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridType_CellBeginEdit);
            this.gridType.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridType_CellEditorInitialized);
            this.gridType.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridType_CellEndEdit);
            this.gridType.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridType_CurrentRowChanged);
            this.gridType.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridType_CellClick);
            this.gridType.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridType_CellDoubleClick);
            this.gridType.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridType_GroupSummaryEvaluate);
            this.gridType.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridType_DataBindingComplete);
            this.gridType.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridType_FilterChanging);
            this.gridType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridType_KeyDown);
            // 
            // GridTravelData
            // 
            this.Controls.Add(this.gridType);
            this.Name = "GridTravelData";
            ((System.ComponentModel.ISupportInitialize)(this.gridType.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridType)).EndInit();
            this.ResumeLayout(false);

        }

        #region GettersSetters
        //getters setters
        public bool bLoadTreeMenu
        {
            get { return _bLoadTreeMenu; }
            set { _bLoadTreeMenu = value; }
        }
        public Telerik.WinControls.UI.RadGridView TypeGridView
        {
            get { return gridType; }
        }

        public CodeTrainingFromVolFeaturesModel SelectedRowType
        {
            get { return _selectedRowType; }
        }
        //public TypeModel ClickedType
        //{
        //    get { return _clickedType; }
        //}

        public CodeTrainingFromVolFeaturesModel ClickedType
        {
            get { return _clickedTypeForCode; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridType.Columns; }
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
            return travelDataBUS.GetAllCodeTrainingFromVolFeatures(Login._user.lngUser);
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
            this.gridType.DataSource = null;
            this.gridType.DataSource = binding;

        }

        public void ClearDescriptors()
        {
            this.gridType.MasterTemplate.SortDescriptors.Clear();
            this.gridType.MasterTemplate.GroupDescriptors.Clear();
            this.gridType.MasterTemplate.FilterDescriptors.Clear();
        }
        public void SaveLayout(string filename)
        {
            this.gridType.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridType.LoadLayout(filterFolder + "\\" + filename);
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

        //public TypeModel ReturnRowData(TypeModel data)
        //{
        //    return data;
        //}

        public CodeTrainingFromVolFeaturesModel ReturnRowData(CodeTrainingFromVolFeaturesModel data)
        {
            return data;
        }

        #endregion Functions

        #region Grid Events
        // eventi na gridu
       private void gridType_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                dictionary.Add(column.HeaderText, column.HeaderText);
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 280);
                column.MinWidth = column.Width;

            }

            for (int i = 0; i < gridType.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(gridType.Columns[i].HeaderText)!=null)
                    gridType.Columns[i].HeaderText = resxSet.GetString(gridType.Columns[i].HeaderText);
                   
                }

            }
            if (gridType.Columns.Count > 0)
            {
                //for number of rows
                this.gridType.SummaryRowsTop.Clear();
                gridType.MasterTemplate.EnablePaging = false;
                gridType.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridType.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridType.SummaryRowsTop.Add(summaryRowItem);
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

       private void gridType_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
       {
           if (e.Parent == this.gridType.MasterTemplate)
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
        private void gridType_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            CodeTrainingFromVolFeaturesModel selectedType = new CodeTrainingFromVolFeaturesModel();
            GridViewRowInfo info = this.gridType.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedType = (CodeTrainingFromVolFeaturesModel)info.DataBoundItem;

                if (selectedType != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedType = selectedType;
                    frmTravelData frm = new frmTravelData(this._clickedType);
                     frm.ShowDialog();

                   // if (frm.isChanged == true)
                    
                        TravelDataBUS nbus = new TravelDataBUS();

                        gridType.DataSource = null; //refresh grida
                        gridType.DataSource = nbus.GetAllCodeTrainingFromVolFeatures(Login._user.lngUser); //refresh grida
                    
                }
            }
        }
        private void gridType_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridType.MasterView.TableHeaderRow && e.CurrentRow != gridType.MasterView.TableFilteringRow && e.CurrentRow != gridType.MasterView.TableSearchRow)
            {
                CodeTrainingFromVolFeaturesModel selectedType = new CodeTrainingFromVolFeaturesModel();
                selectedType = (CodeTrainingFromVolFeaturesModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowType = selectedType;
                    RaiseStatusChanged(selectedType);
                }
            }
        }
        #endregion

        private void gridType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.gridType.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    CodeTrainingFromVolFeaturesModel model = (CodeTrainingFromVolFeaturesModel)this.gridType.CurrentRow.DataBoundItem;
                    //frmTravelData frm = new frmTravelData(model);
                    frmTravelData frm = new frmTravelData();
                    frm.Show();
                    return;
                }
            }
        }

        private void gridType_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridType.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridType.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridType.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridType.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridType.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridType.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridType.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridType.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridType.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridType.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridType.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridType_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridType.IsInEditMode && !(this.gridType.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridType_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridType.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridType_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridType.FilterChanging -= gridType_FilterChanging;

                this.gridType.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridType.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridType.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridType.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridType.FilterChanging += gridType_FilterChanging;
            } 
        }

        private void gridType_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridType.EndEdit();
            }
        }

      

    }

    public class TravelDataStatusSelectedRowchanged : EventArgs
    {
        public CodeTrainingFromVolFeaturesModel type { get; set; }
    }


}
