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
using System.Windows.Forms;
using Telerik.WinControls.Data;

namespace GUI
{    
    public partial class GridViewBooking : System.Windows.Forms.UserControl, IBISGrid
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

        public event EventHandler<BookingSelectedRowchanged> BookingSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(ArrangementModel vodd)
        {
            BookingSelectedRowchanged(this, new BookingSelectedRowchanged { vod = vodd });
        }

        
        ArrangementBUS arrBUS;
        private Telerik.WinControls.UI.RadGridView gridBooking;
        private ArrangementModel _selectedRowBooking;
        private ArrangementModel _clickedBooking;
        public List<IModel> modelData;
        // Folder u kome cuva filtere za arrangmente
        private string filterFolder;

        // Folder u kome cuva labele za arrangmente
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridViewBooking()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\Booking")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\Booking"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\Booking")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\Booking"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\Booking");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\Booking");
            arrBUS = new ArrangementBUS();

            InitializeComponent();        
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridBooking = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBooking.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridBooking
            // 
            this.gridBooking.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridBooking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBooking.EnterKeyMode = Telerik.WinControls.UI.RadGridViewEnterKeyMode.EnterMovesToNextRow;
            this.gridBooking.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridBooking.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridBooking.MasterTemplate.AllowAddNewRow = false;
            this.gridBooking.MasterTemplate.AllowCellContextMenu = false;
            this.gridBooking.MasterTemplate.AllowDeleteRow = false;
            this.gridBooking.MasterTemplate.AllowEditRow = false;
            this.gridBooking.MasterTemplate.AllowSearchRow = true;
            this.gridBooking.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridBooking.MasterTemplate.EnableFiltering = true;
            this.gridBooking.MasterTemplate.EnablePaging = true;
            this.gridBooking.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridBooking.MasterTemplate.PageSize = 50;
            this.gridBooking.MasterTemplate.ShowFilterCellOperatorText = false;
            this.gridBooking.MasterTemplate.ShowGroupedColumns = true;
            this.gridBooking.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridBooking.Name = "gridBooking";
            this.gridBooking.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridBooking.Size = new System.Drawing.Size(150, 150);
            this.gridBooking.TabIndex = 0;
            this.gridBooking.Text = "gridBooking";
            this.gridBooking.ThemeName = "VisualStudio2012Light";
            this.gridBooking.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridBooking_CellBeginEdit);
            this.gridBooking.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridBooking_CellEditorInitialized);
            this.gridBooking.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridBooking_CellEndEdit);
            this.gridBooking.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridBooking_CurrentRowChanged);
            this.gridBooking.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridBooking_CellClick);
            this.gridBooking.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridBooking_CellDoubleClick);
            this.gridBooking.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridBooking_GroupSummaryEvaluate);
            this.gridBooking.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridBooking_DataBindingComplete);
            this.gridBooking.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridBooking_FilterChanging);
            // 
            // GridViewBooking
            // 
            this.Controls.Add(this.gridBooking);
            this.Name = "GridViewBooking";
            ((System.ComponentModel.ISupportInitialize)(this.gridBooking.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridBooking)).EndInit();
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
            get { return gridBooking; }
        }

        public ArrangementModel SelectedRowBooking
        {
            get { return _selectedRowBooking; }
        }
        public ArrangementModel ClickedBooking
        {
            get { return _clickedBooking; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridBooking.Columns; }
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
            return arrBUS.GetAllArrangementsMainGrid(selectedFilter,idLabelList,idLang);    //GetVoluntaryQuestDetails(idLabelList);
        }
        public List<FilterModel> ReturnFilters()
        {
            return Login._arrangeFilters;
        }
        public List<LabelModel> ReturnLabels()
        {
            return Login._arrLabels;
        }       
      
        public void SetDataPersonBinding(List<IModel> binding)
        {
            this.gridBooking.DataSource = null;
            this.gridBooking.DataSource = binding;

        }

        public void ClearDescriptors()
        {
            this.gridBooking.MasterTemplate.SortDescriptors.Clear();
            this.gridBooking.MasterTemplate.GroupDescriptors.Clear();
            this.gridBooking.MasterTemplate.FilterDescriptors.Clear();
        }
        public void SaveLayout(string filename)
        {
            this.gridBooking.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridBooking.LoadLayout(filterFolder + "\\" + filename);
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
        private void gridBooking_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
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
                //gridBooking.Columns["idQuest"].IsVisible = false;
                //gridBooking.Columns["idAns"].IsVisible = false;
                //gridBooking.Columns["idAnsType"].IsVisible = false;
                //gridBooking.Columns["questSort"].IsVisible = false;
                //gridBooking.Columns["ansSort"].IsVisible = false;
            }
            for (int i = 0; i < gridBooking.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(gridBooking.Columns[i].HeaderText) != null)
                        gridBooking.Columns[i].HeaderText = resxSet.GetString(gridBooking.Columns[i].HeaderText);
                }

            }
            if (gridBooking.Columns.Count > 0)
            {
                //for number of rows
                this.gridBooking.SummaryRowsTop.Clear();
                gridBooking.MasterTemplate.EnablePaging = false;
                gridBooking.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridBooking.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridBooking.SummaryRowsTop.Add(summaryRowItem);
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

        private void gridBooking_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridBooking.MasterTemplate)
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
        private void gridBooking_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            ArrangementModel selectedBooking = new ArrangementModel();
            GridViewRowInfo info = this.gridBooking.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedBooking = (ArrangementModel)info.DataBoundItem;

                if (selectedBooking != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedBooking = selectedBooking;

                    frmArrangementBook frm = new frmArrangementBook(this._clickedBooking);
                    frm.ShowDialog();

                    selectedBooking.statusArrangement = arrBUS.GetArrangementById(selectedBooking.idArrangement).statusArrangement;
                    info.InvalidateRow();
 
                }
            }
        }
        private void gridBooking_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridBooking.MasterView.TableHeaderRow && e.CurrentRow != gridBooking.MasterView.TableFilteringRow && e.CurrentRow != gridBooking.MasterView.TableSearchRow)
            {
                ArrangementModel selectedBooking = new ArrangementModel();
                selectedBooking = (ArrangementModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowBooking = selectedBooking;
                    RaiseStatusChanged(selectedBooking);
                }
            }
        }
        #endregion

        private void gridBooking_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridBooking.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridBooking.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridBooking.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridBooking.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridBooking.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridBooking.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridBooking.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridBooking.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridBooking.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridBooking.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridBooking.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridBooking.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridBooking_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridBooking.IsInEditMode && !(this.gridBooking.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridBooking_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridBooking.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridBooking_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridBooking.FilterChanging -= gridBooking_FilterChanging;

                this.gridBooking.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridBooking.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridBooking.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridBooking.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridBooking.FilterChanging += gridBooking_FilterChanging;
            }  
        }

        private void gridBooking_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridBooking.EndEdit();
            }
        }



    }

    public class BookingSelectedRowchanged : EventArgs
    {
         public ArrangementModel vod { get; set; }
    }


}
