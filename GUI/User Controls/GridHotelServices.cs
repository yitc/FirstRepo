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
    public partial class GridHotelServices : System.Windows.Forms.UserControl, IBISGrid
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

        public event EventHandler<HotelservicesStatusSelectedRowchanged> HotelservicesStatusSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(HotelServicesModel hottel)
        {
            HotelservicesStatusSelectedRowchanged(this, new HotelservicesStatusSelectedRowchanged { hotel = hottel });
        }


        HotelServicesBUS hotelBUS;
        private Telerik.WinControls.UI.RadGridView gridHotel;
        private HotelServicesModel _selectedRowHotelServices;
        private HotelServicesModel _clickedHotelServices;
        
        // Folder u kome cuva filtere za tipove
        private string filterFolder;

        // Folder u kome cuva labele za tipove
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridHotelServices()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\HotelServices")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\HotelServices"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\HotelServices")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\HotelServices"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\HotelServices");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\HotelServices");
            hotelBUS = new HotelServicesBUS();

           InitializeComponent();        
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridHotel = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridHotel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHotel.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridHotel
            // 
            this.gridHotel.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridHotel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHotel.EnableFastScrolling = true;
            this.gridHotel.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridHotel.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridHotel.MasterTemplate.AllowAddNewRow = false;
            this.gridHotel.MasterTemplate.AllowCellContextMenu = false;
            this.gridHotel.MasterTemplate.AllowDeleteRow = false;
            this.gridHotel.MasterTemplate.AllowEditRow = false;
            this.gridHotel.MasterTemplate.AllowSearchRow = true;
            this.gridHotel.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridHotel.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridHotel.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridHotel.MasterTemplate.EnableFiltering = true;
            this.gridHotel.MasterTemplate.EnablePaging = true;
            this.gridHotel.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridHotel.MasterTemplate.PageSize = 50;
            this.gridHotel.MasterTemplate.ShowGroupedColumns = true;
            this.gridHotel.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridHotel.Name = "gridHotel";
            this.gridHotel.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridHotel.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridHotel.Size = new System.Drawing.Size(150, 150);
            this.gridHotel.TabIndex = 0;
            this.gridHotel.Text = "HotelServices Grid";
            this.gridHotel.ThemeName = "VisualStudio2012Light";
            this.gridHotel.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridHotel_CellBeginEdit);
            this.gridHotel.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridHotel_CellEditorInitialized);
            this.gridHotel.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridHotel_CellEndEdit);
            this.gridHotel.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridAgeCategoy_CurrentRowChanged);
            this.gridHotel.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridHotel_CellClick);
            this.gridHotel.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridAgeCategoy_CellDoubleClick);
            this.gridHotel.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridAgeCategoy_GroupSummaryEvaluate);
            this.gridHotel.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridAgeCategoy_DataBindingComplete);
            this.gridHotel.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridHotel_FilterChanging);
            // 
            // GridHotelServices
            // 
            this.Controls.Add(this.gridHotel);
            this.Name = "GridHotelServices";
            ((System.ComponentModel.ISupportInitialize)(this.gridHotel.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridHotel)).EndInit();
            this.ResumeLayout(false);

        }

        #region GettersSetters
        //getters setters
        public bool bLoadTreeMenu
        {
            get { return _bLoadTreeMenu; }
            set { _bLoadTreeMenu = value; }
        }
        public Telerik.WinControls.UI.RadGridView HotelServicesGridView
        {
            get { return gridHotel; }
        }

        public HotelServicesModel SelectedRowHotelServices
        {
            get { return _selectedRowHotelServices; }
        }
        public HotelServicesModel ClickedHotelServices
        {
            get { return _clickedHotelServices; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridHotel.Columns; }
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
            return hotelBUS.GetAllHotelServices();
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
            this.gridHotel.DataSource = null;
            this.gridHotel.DataSource = binding;

        }

        public void ClearDescriptors()
        {
            this.gridHotel.MasterTemplate.SortDescriptors.Clear();
            this.gridHotel.MasterTemplate.GroupDescriptors.Clear();
            this.gridHotel.MasterTemplate.FilterDescriptors.Clear();
        }
        public void removeRow(HotelServicesModel rw)
        {
            using (gridHotel.DeferRefresh())
            {
                GridViewRowInfo row = this.gridHotel.Rows.Where(s => s.Cells["idHotelService"].Value.ToString() == rw.idHotelService.ToString()).FirstOrDefault();
                if (row != null)
                    this.gridHotel.Rows.Remove(row);
            }
        }
        public void SaveLayout(string filename)
        {
            this.gridHotel.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridHotel.LoadLayout(filterFolder + "\\" + filename);
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
                Columns[0].Width = (int)(this.CreateGraphics().MeasureString(Columns[0].HeaderText, this.Font).Width + 96);
                Columns[0].MinWidth = Columns[0].Width;
                Columns[1].Width = (int)(this.CreateGraphics().MeasureString(Columns[1].HeaderText, this.Font).Width + 661);
            }
            for (int i = 0; i < gridHotel.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(gridHotel.Columns[i].HeaderText)!=null)
                    gridHotel.Columns[i].HeaderText = resxSet.GetString(gridHotel.Columns[i].HeaderText);
                   
                }



            }
            if (gridHotel.Columns.Count > 0)
            {
                //for number of rows
                this.gridHotel.SummaryRowsTop.Clear();
                gridHotel.MasterTemplate.EnablePaging = false;
                gridHotel.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridHotel.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridHotel.SummaryRowsTop.Add(summaryRowItem);
                GridViewSummaryRowItem summaryRowItem2 = new GridViewSummaryRowItem();
            }

        }

        private void gridAgeCategoy_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
       {
           if (e.Parent == this.gridHotel.MasterTemplate)
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
            HotelServicesModel selectedHotelServices = new HotelServicesModel();
            GridViewRowInfo info = this.gridHotel.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedHotelServices = (HotelServicesModel)info.DataBoundItem;

                if (selectedHotelServices != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedHotelServices = selectedHotelServices;
                    frmHotelServices frm = new frmHotelServices(this._clickedHotelServices);
                    frm.ShowDialog();

                    if (frm.isChanged == true)
                    {
                        HotelServicesBUS abus = new HotelServicesBUS();


                        gridHotel.DataSource = null;
                        gridHotel.DataSource = abus.GetAllHotelServices();
                    }
                }
            }
        }
        private void gridAgeCategoy_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridHotel.MasterView.TableHeaderRow && e.CurrentRow != gridHotel.MasterView.TableFilteringRow && e.CurrentRow != gridHotel.MasterView.TableSearchRow)
            {
                HotelServicesModel selectedHotelServices = new HotelServicesModel();
                selectedHotelServices = (HotelServicesModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowHotelServices = selectedHotelServices;
                    RaiseStatusChanged(selectedHotelServices);
                }
            }
        }
        #endregion

        private void gridHotelservices_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.gridHotel.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    TypeModel model = (TypeModel)this.gridHotel.CurrentRow.DataBoundItem;
                    frmType frm = new frmType(model);
                    frm.ShowDialog();
                    return;
                }
            }
        }

        private void gridHotel_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridHotel.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridHotel.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridHotel.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridHotel.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridHotel.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridHotel.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridHotel.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridHotel.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridHotel.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridHotel.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridHotel.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridHotel.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridHotel_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridHotel.IsInEditMode && !(this.gridHotel.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridHotel_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridHotel.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridHotel_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridHotel.FilterChanging -= gridHotel_FilterChanging;

                this.gridHotel.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridHotel.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridHotel.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridHotel.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridHotel.FilterChanging += gridHotel_FilterChanging;
            }  
        }

        private void gridHotel_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridHotel.EndEdit();
            }
        }






    }

    public class HotelservicesStatusSelectedRowchanged : EventArgs
    {
        public HotelServicesModel hotel { get; set; }
    }


}
