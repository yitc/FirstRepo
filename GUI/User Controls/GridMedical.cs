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
    public partial class GridViewMedical : System.Windows.Forms.UserControl, IBISGrid
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
        
        public event EventHandler<MedicalStatusSelectedRowchanged> MedicalStatusSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(MedicalVoluntaryQuestModel medd)
        {
            MedicalStatusSelectedRowchanged(this, new MedicalStatusSelectedRowchanged { med = medd });
        }

        
        MedicalVoluntaryBUS medBUS;
        private Telerik.WinControls.UI.RadGridView gridMedical;
        private MedicalVoluntaryQuestModel _selectedRowMedical;
        private MedicalVoluntaryQuestModel _clickedMedical;
        public List<IModel> modelData;
        
        // Folder u kome cuva filtere za medical
        private string filterFolder;

        // Folder u kome cuva labele za medical
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridViewMedical()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\medical")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\medical"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\medical")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\medical"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\medical");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\medical");
            medBUS = new MedicalVoluntaryBUS();

           InitializeComponent();        
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridMedical = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridMedical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMedical.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridMedical
            // 
            this.gridMedical.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridMedical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMedical.EnableFastScrolling = true;
            this.gridMedical.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridMedical.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridMedical.MasterTemplate.AllowAddNewRow = false;
            this.gridMedical.MasterTemplate.AllowCellContextMenu = false;
            this.gridMedical.MasterTemplate.AllowDeleteRow = false;
            this.gridMedical.MasterTemplate.AllowEditRow = false;
            this.gridMedical.MasterTemplate.AllowSearchRow = true;
            this.gridMedical.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridMedical.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridMedical.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridMedical.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridMedical.MasterTemplate.EnableFiltering = true;
            this.gridMedical.MasterTemplate.EnablePaging = true;
            this.gridMedical.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridMedical.MasterTemplate.PageSize = 50;
            this.gridMedical.MasterTemplate.ShowGroupedColumns = true;
            this.gridMedical.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridMedical.Name = "gridMedical";
            this.gridMedical.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridMedical.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridMedical.Size = new System.Drawing.Size(150, 150);
            this.gridMedical.TabIndex = 0;
            this.gridMedical.Text = "Medical Grid";
            this.gridMedical.ThemeName = "VisualStudio2012Light";
            this.gridMedical.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridMedical_CellBeginEdit);
            this.gridMedical.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridMedical_CellEditorInitialized);
            this.gridMedical.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridMedical_CellEndEdit);
            this.gridMedical.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridUsers_CurrentRowChanged);
            this.gridMedical.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridMedical_CellClick);
            this.gridMedical.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridMedical_CellDoubleClick);
            this.gridMedical.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridMedical_GroupSummaryEvaluate);
            this.gridMedical.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridMedical_DataBindingComplete);
            this.gridMedical.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridMedical_FilterChanging);
            this.gridMedical.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridMedical_KeyDown);
            // 
            // GridViewMedical
            // 
            this.Controls.Add(this.gridMedical);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GridViewMedical";
            ((System.ComponentModel.ISupportInitialize)(this.gridMedical.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridMedical)).EndInit();
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
            get { return gridMedical; }
        }

        public MedicalVoluntaryQuestModel SelectedRowMedical
        {
            get { return _selectedRowMedical; }
        }
        public MedicalVoluntaryQuestModel ClickedMedical
        {
            get { return _clickedMedical; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridMedical.Columns; }
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
            return medBUS.GetMedicalQuestDetails(idLabelList);
        }
        public List<FilterModel> ReturnFilters()
        {
            return Login._medicalFilters;
        }
        public List<LabelModel> ReturnLabels()
        {
            return Login._medicalLabels;
        }       
      
        public void SetDataPersonBinding(List<IModel> binding)
        {
            this.gridMedical.DataSource = null;
            this.gridMedical.DataSource = binding;

        }

        public void ClearDescriptors()
        {
            this.gridMedical.MasterTemplate.SortDescriptors.Clear();
            this.gridMedical.MasterTemplate.GroupDescriptors.Clear();
            this.gridMedical.MasterTemplate.FilterDescriptors.Clear();
        }

        public void removeRow(MedicalVoluntaryQuestModel rw)
        {
            using (gridMedical.DeferRefresh())
            {
                GridViewRowInfo row = this.gridMedical.Rows.Where(s => s.Cells["idQuestGroup"].Value.ToString() == rw.idQuestGroup.ToString()).FirstOrDefault();
                if (row != null)
                    this.gridMedical.Rows.Remove(row);
            }
        }
        public void SaveLayout(string filename)
        {
            this.gridMedical.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridMedical.LoadLayout(filterFolder + "\\" + filename);
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
        private void gridMedical_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                dictionary.Add(column.HeaderText, column.HeaderText);
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                column.MinWidth = column.Width;
            }
            //if (gridMedical.Columns.Count > 0)
            //{
            //    gridMedical.Columns["idQuest"].IsVisible = false;
            //    gridMedical.Columns["idAns"].IsVisible = false;
            //    gridMedical.Columns["idAnsType"].IsVisible = false;
            //    gridMedical.Columns["questSort"].IsVisible = false;
            //    gridMedical.Columns["ansSort"].IsVisible = false;
            //    gridMedical.Columns["idQuestGroup"].IsVisible = false;
            //}

            for (int i = 0; i < gridMedical.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(gridMedical.Columns[i].HeaderText) != null)
                        gridMedical.Columns[i].HeaderText = resxSet.GetString(gridMedical.Columns[i].HeaderText);
                }

            }
            if (gridMedical.Columns.Count > 0)
            {
                //for number of rows
                this.gridMedical.SummaryRowsTop.Clear();
                gridMedical.MasterTemplate.EnablePaging = false;
                gridMedical.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridMedical.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridMedical.SummaryRowsTop.Add(summaryRowItem);
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

        private void gridMedical_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridMedical.MasterTemplate)
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
        private void gridMedical_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            MedicalVoluntaryQuestModel selectedMedical = new MedicalVoluntaryQuestModel();
            GridViewRowInfo info = this.gridMedical.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedMedical = (MedicalVoluntaryQuestModel)info.DataBoundItem;

                if (selectedMedical != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedMedical = selectedMedical;
                    frmMedical frm = new frmMedical(this._clickedMedical);

                    frm.ShowDialog();
                    modelData = medBUS.GetMedicalQuestDetails(MainForm.idLabelList);
                    this.SetDataPersonBinding(modelData);
                    if (File.Exists(filterFolder + "\\Standard.xml"))
                    {
                        this.gridMedical.LoadLayout(filterFolder + "\\Standard.xml");
                    }
                }
            }
        }
        private void gridUsers_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridMedical.MasterView.TableHeaderRow && e.CurrentRow != gridMedical.MasterView.TableFilteringRow && e.CurrentRow != gridMedical.MasterView.TableSearchRow)
            {
                MedicalVoluntaryQuestModel selectedMedical = new MedicalVoluntaryQuestModel();
                selectedMedical = (MedicalVoluntaryQuestModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowMedical = selectedMedical;
                    RaiseStatusChanged(selectedMedical);
                }
            }
        }
        #endregion

        private void gridMedical_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.gridMedical.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    MedicalVoluntaryQuestModel model = (MedicalVoluntaryQuestModel)this.gridMedical.CurrentRow.DataBoundItem;
                    frmMedical frm = new frmMedical(model);
                    frm.Show();
                    return;
                }
            }
        }

        private void gridMedical_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridMedical.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridMedical.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridMedical.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridMedical.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridMedical.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridMedical.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridMedical.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridMedical.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridMedical.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridMedical.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridMedical.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridMedical.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridMedical_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridMedical.IsInEditMode && !(this.gridMedical.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridMedical_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridMedical.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridMedical_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridMedical.FilterChanging -= gridMedical_FilterChanging;

                this.gridMedical.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridMedical.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridMedical.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridMedical.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridMedical.FilterChanging += gridMedical_FilterChanging;
            }  
        }

        private void gridMedical_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridMedical.EndEdit();
            }
        }





      

    }

    public class MedicalStatusSelectedRowchanged : EventArgs
    {
        public MedicalVoluntaryQuestModel med { get; set; }
    }


}
