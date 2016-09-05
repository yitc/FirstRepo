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
    public partial class GridViewArrangement : System.Windows.Forms.UserControl, IBISGrid
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

        public event EventHandler<ArrangementSelectedRowchanged> ArrangementSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(ArrangementModel vodd)
        {
            ArrangementSelectedRowchanged(this, new ArrangementSelectedRowchanged { vod = vodd });
        }


        ArrangementBUS arrBUS;
        private Telerik.WinControls.UI.RadGridView gridArrangement;
        private ArrangementModel _selectedRowArrangement;
        private ArrangementModel _clickedArrangement;
        public List<IModel> modelData;
        // Folder u kome cuva filtere za arrangmente
        private string filterFolder;

        // Folder u kome cuva labele za arrangmente
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;
        public short onClickReference = 1;        

        public GridViewArrangement()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\arrangement")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\arrangement"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\arrangement")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\arrangement"));
            }

            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\arrangementTraveler")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\arrangementTraveler"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\arrangementTraveler")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\arrangementTraveler"));
            }

            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\arrangementVH")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\arrangementVH"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\arrangementVH")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\arrangementVH"));
            }
            setFiltersAndLabels();
            arrBUS = new ArrangementBUS();

            InitializeComponent();        
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridArrangement = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridArrangement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridArrangement.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridArrangement
            // 
            this.gridArrangement.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridArrangement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridArrangement.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridArrangement.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridArrangement.MasterTemplate.AllowAddNewRow = false;
            this.gridArrangement.MasterTemplate.AllowCellContextMenu = false;
            this.gridArrangement.MasterTemplate.AllowDeleteRow = false;
            this.gridArrangement.MasterTemplate.AllowEditRow = false;
            this.gridArrangement.MasterTemplate.AllowSearchRow = true;
            this.gridArrangement.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridArrangement.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridArrangement.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridArrangement.MasterTemplate.EnableFiltering = true;
            this.gridArrangement.MasterTemplate.EnablePaging = true;
            this.gridArrangement.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridArrangement.MasterTemplate.PageSize = 50;
            this.gridArrangement.MasterTemplate.ShowFilterCellOperatorText = false;
            this.gridArrangement.MasterTemplate.ShowGroupedColumns = true;
            this.gridArrangement.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridArrangement.Name = "gridArrangement";
            this.gridArrangement.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridArrangement.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridArrangement.Size = new System.Drawing.Size(150, 150);
            this.gridArrangement.TabIndex = 0;
            this.gridArrangement.Text = "gridArrangement";
            this.gridArrangement.ThemeName = "VisualStudio2012Light";
            this.gridArrangement.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.gridArrangement_CellFormating);
            this.gridArrangement.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridArrangement_CellBeginEdit);
            this.gridArrangement.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridArrangement_CellEditorInitialized);
            this.gridArrangement.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridArrangement_CellEndEdit);
            this.gridArrangement.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridArrangement_CurrentRowChanged);
            this.gridArrangement.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridArrangement_CellClick);
            this.gridArrangement.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridArrangement_CellDoubleClick);
            this.gridArrangement.CellValueChanged += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridArrangement_CellValueChanged);
            this.gridArrangement.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridArrangement_GroupSummaryEvaluate);
            this.gridArrangement.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridArrangement_DataBindingComplete);
            this.gridArrangement.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridArrangement_FilterChanging);
            this.gridArrangement.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridArrangement_KeyDown);
            // 
            // GridViewArrangement
            // 
            this.Controls.Add(this.gridArrangement);
            this.Name = "GridViewArrangement";
            ((System.ComponentModel.ISupportInitialize)(this.gridArrangement.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridArrangement)).EndInit();
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
            get { return gridArrangement; }
        }

        public ArrangementModel SelectedRowArrangement
        {
            get { return _selectedRowArrangement; }
        }
        public ArrangementModel ClickedArrangement
        {
            get { return _clickedArrangement; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridArrangement.Columns; }
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
            this.gridArrangement.DataSource = null;
            this.gridArrangement.DataSource = binding;

        }

        public void ClearDescriptors()
        {
            this.gridArrangement.MasterTemplate.SortDescriptors.Clear();
            this.gridArrangement.MasterTemplate.GroupDescriptors.Clear();
            this.gridArrangement.MasterTemplate.FilterDescriptors.Clear();
        }



        public void SaveLayout(string filename)
        {
            setFiltersAndLabels();
            this.gridArrangement.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            setFiltersAndLabels();
            this.gridArrangement.LoadLayout(filterFolder + "\\" + filename);
        }

        public void setFiltersAndLabels()
        {
            if (onClickReference == 1)
            {
                filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\arrangement");
                labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\arrangement");
            }
            else if (onClickReference == 2)
            {
                filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\arrangementTraveler");
                labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\arrangementTraveler");
            }
            else if (onClickReference == 3)
            {
                filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\arrangementTraveler");
                labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\arrangementTraveler");
            }
            //else if (onClickReference == 3)
            //{
            //    filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\arrangementVH");
            //    labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\arrangementVH");
            //}
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
        private void gridArrangement_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                dictionary.Add(column.HeaderText, column.HeaderText);
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
               // column.MinWidth = column.Width;
            }
            for (int i = 0; i < gridArrangement.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(gridArrangement.Columns[i].HeaderText) != null)
                        gridArrangement.Columns[i].HeaderText = resxSet.GetString(gridArrangement.Columns[i].HeaderText);
                }

            }
            if (gridArrangement.Columns.Count > 0)
            {
                //for number of rows
                this.gridArrangement.SummaryRowsTop.Clear();
                gridArrangement.MasterTemplate.EnablePaging = false;
                gridArrangement.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridArrangement.Columns["codeArrangement"].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridArrangement.SummaryRowsTop.Add(summaryRowItem);
            }
        }

        private void gridArrangement_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridArrangement.MasterTemplate)
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

        private void gridArrangement_CellFormating(object sender, CellFormattingEventArgs e)
        {
            if (e.Column.Name == "dtToArrangement" || e.Column.Name == "dtFromArrangement")
            {
                if (e.Column.IsVisible == true)
                {
                    try
                    {
                        //DateTime temp = DateTime.Parse(e.CellElement.Text);
                        DateTime temp = DateTime.Parse(e.CellElement.Value.ToString());
                        e.CellElement.Text = temp.ToString("dd-MM-yyyy");
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        private void gridArrangement_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            ArrangementModel selectedArrangement = new ArrangementModel();
            GridViewRowInfo info = this.gridArrangement.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedArrangement = (ArrangementModel)info.DataBoundItem;

                if (selectedArrangement != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedArrangement = selectedArrangement;

                    if (onClickReference == 1)
                    {

                        using (frmArrangement frm = new frmArrangement(this._clickedArrangement))
                        {
                            frm.ShowDialog();
                        }
                       
                       
                        //modelData = arrBUS.GetAllArrangementsMainGrid(0, MainForm.idLabelList, Login._user.lngUser);
                        //this.SetDataPersonBinding(modelData);
                        //if (File.Exists(filterFolder + "\\Standard.xml"))
                        //{
                        //    this.gridArrangement.LoadLayout(filterFolder + "\\Standard.xml");
                        //}

                    }
                    else if(onClickReference == 2)
                    {
                        using (frmArrangementBook frm = new frmArrangementBook(this._clickedArrangement))
                        {
                            if (new ArrangementCalculationBUS().isCalculationFinished(this._clickedArrangement.idArrangement) == true)
                            {
                                frm.ShowDialog();
                                selectedArrangement.statusArrangement = arrBUS.GetArrangementById(selectedArrangement.idArrangement).statusArrangement;
                                this.gridArrangement.CurrentRow.InvalidateRow();
                            }
                            else
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("You can't open booking on arrangement unless it is finished!");
                            }

                        }
                        //if (File.Exists(filterFolder + "\\Standard.xml"))
                        //{
                        //    gridArrangement.LoadLayout("Standard");
                        //}
                    }
                    else if (onClickReference == 3)
                    {
                        using (frmArrangementTourleading frm = new frmArrangementTourleading(this._clickedArrangement))
                        {
                            frm.ShowDialog();
                        }
                        //if (File.Exists(filterFolder + "\\Standard.xml"))
                        //{
                        //    gridArrangement.LoadLayout("Standard");
                        //}
                    }

                    //else if(onClickReference == 3)
                    //{
                    //    frmArrangementBook_VH frm = new frmArrangementBook_VH(this._clickedArrangement);
                    //    frm.ShowDialog();
                    //    if (File.Exists(filterFolder + "\\Standard.xml"))
                    //    {
                    //        gridArrangement.LoadLayout("Standard");
                    //    }
                    //}
                    //else
                    //{

                    //}


                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }
        }
        private void gridArrangement_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridArrangement.MasterView.TableHeaderRow && e.CurrentRow != gridArrangement.MasterView.TableFilteringRow && e.CurrentRow != gridArrangement.MasterView.TableSearchRow)
            {
                ArrangementModel selectedArrangement = new ArrangementModel();
                selectedArrangement = (ArrangementModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowArrangement = selectedArrangement;
                    RaiseStatusChanged(selectedArrangement);
                }
            }
        }
        #endregion

        private void gridArrangement_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                ArrangementModel selectedArrangement = new ArrangementModel();
                GridViewRowInfo info = this.gridArrangement.CurrentRow;

                if (info != null && info.Index >= 0)
                {
                    selectedArrangement = (ArrangementModel)info.DataBoundItem;

                    if (selectedArrangement != null)
                    {
                        this._clickedArrangement = selectedArrangement;

                        if (onClickReference == 1)
                        {

                            using (frmArrangement frm = new frmArrangement(this._clickedArrangement))
                            {
                                frm.ShowDialog();
                            }
                        }
                        else if (onClickReference == 2)
                        {
                            using (frmArrangementBook frm = new frmArrangementBook(this._clickedArrangement))
                            {
                                if (new ArrangementCalculationBUS().isCalculationFinished(this._clickedArrangement.idArrangement) == true)
                                {
                                    frm.ShowDialog();
                                    selectedArrangement.statusArrangement = arrBUS.GetArrangementById(selectedArrangement.idArrangement).statusArrangement;
                                    this.gridArrangement.CurrentRow.InvalidateRow();
                                }
                                else
                                {
                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translateAllMessageBox("You can't open booking on arrangement unless it is finished!");
                                }

                            }
                        }
                        else if (onClickReference == 3)
                        {
                            using (frmArrangementTourleading frm = new frmArrangementTourleading(this._clickedArrangement))
                            {
                                frm.ShowDialog();
                            }
                        }
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();
                    }
                }
                
            }
        }

        private void gridArrangement_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
           
        }

        private void gridArrangement_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridArrangement.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridArrangement.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArrangement.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArrangement.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArrangement.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArrangement.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridArrangement.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridArrangement.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridArrangement.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");                                             
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridArrangement.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridArrangement.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;                                                
                    }
                }
            }
            //else if(e.KeyData == Keys.Enter || e.KeyData == Keys.Return)
            //{
            //    if (this.gridArrangement.CurrentRow != null && this.gridArrangement.CurrentRow is GridViewFilteringRowInfo && this.gridArrangement.IsInEditMode)
            //    {
            //        this.gridArrangement.EndEdit();
            //    }
            //}
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridArrangement_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridArrangement.IsInEditMode && !(this.gridArrangement.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridArrangement_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridArrangement.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridArrangement_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridArrangement.FilterChanging -= gridArrangement_FilterChanging;

                this.gridArrangement.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridArrangement.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridArrangement.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridArrangement.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }                  

                    lastFilterDescriptor = null;
                }

                this.gridArrangement.FilterChanging += gridArrangement_FilterChanging;
            }  
        }

        private void gridArrangement_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridArrangement.EndEdit();
            }
        }


    }

    public class ArrangementSelectedRowchanged : EventArgs
    {
         public ArrangementModel vod { get; set; }
    }


}
