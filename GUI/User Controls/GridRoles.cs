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
    public partial class GridViewRoles : System.Windows.Forms.UserControl, IBISGrid
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
        
        public event EventHandler<RolesStatusSelectedRowchanged> RolesStatusSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(RoleModel roles)
        {
            RolesStatusSelectedRowchanged(this, new RolesStatusSelectedRowchanged { role = roles });
        }

        
        RoleBUS rolesBUS;
        private Telerik.WinControls.UI.RadGridView gridRoles;
        private RoleModel _selectedRowRoles;
        private RoleModel _clickedRoles;
        public bool isChanged = false;
        
        // Folder u kome cuva filtere za Role
        private string filterFolder;

        // Folder u kome cuva labele za Role
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridViewRoles()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\roles")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\roles"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\roles")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\roles"));
            } 
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,Login._user.username + "\\filters\\custom filters\\roles");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\roles");
            rolesBUS = new RoleBUS();

           InitializeComponent();        
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridRoles = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridRoles.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridRoles
            // 
            this.gridRoles.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRoles.EnableFastScrolling = true;
            this.gridRoles.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridRoles.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridRoles.MasterTemplate.AllowAddNewRow = false;
            this.gridRoles.MasterTemplate.AllowCellContextMenu = false;
            this.gridRoles.MasterTemplate.AllowDeleteRow = false;
            this.gridRoles.MasterTemplate.AllowEditRow = false;
            this.gridRoles.MasterTemplate.AllowSearchRow = true;
            this.gridRoles.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.gridRoles.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridRoles.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridRoles.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridRoles.MasterTemplate.EnableFiltering = true;
            this.gridRoles.MasterTemplate.EnablePaging = true;
            this.gridRoles.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridRoles.MasterTemplate.PageSize = 50;
            this.gridRoles.MasterTemplate.ShowGroupedColumns = true;
            this.gridRoles.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridRoles.Name = "gridRoles";
            this.gridRoles.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridRoles.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridRoles.Size = new System.Drawing.Size(150, 150);
            this.gridRoles.TabIndex = 0;
            this.gridRoles.Text = "Roless Grid";
            this.gridRoles.ThemeName = "VisualStudio2012Light";
            this.gridRoles.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridRoles_CellBeginEdit);
            this.gridRoles.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridRoles_CellEditorInitialized);
            this.gridRoles.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridRoles_CellEndEdit);
            this.gridRoles.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridRoles_CurrentRowChanged);
            this.gridRoles.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridRoles_CellClick);
            this.gridRoles.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridRoles_CellDoubleClick);
            this.gridRoles.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridRoles_GroupSummaryEvaluate);
            this.gridRoles.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridRoles_DataBindingComplete);
            this.gridRoles.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridRoles_FilterChanging);
            this.gridRoles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridRoles_KeyDown);
            // 
            // GridViewRoles
            // 
            this.Controls.Add(this.gridRoles);
            this.Name = "GridViewRoles";
            ((System.ComponentModel.ISupportInitialize)(this.gridRoles.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridRoles)).EndInit();
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
            get { return gridRoles; }
        }

        public RoleModel SelectedRowRoles
        {
            get { return _selectedRowRoles; }
        }
        public RoleModel ClickedRoles
        {
            get { return _clickedRoles; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridRoles.Columns; }
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
            return rolesBUS.GetAllRole();
        }
        public List<FilterModel> ReturnFilters()
        {
            return Login._rolesFilters;
        }
        public List<LabelModel> ReturnLabels()
        {
            return Login._rolesLabels;
        }       
      
        public void SetDataPersonBinding(List<IModel> binding)
        {
            this.gridRoles.DataSource = null;
            this.gridRoles.DataSource = binding;

        }

        public void ClearDescriptors()
        {
            this.gridRoles.MasterTemplate.SortDescriptors.Clear();
            this.gridRoles.MasterTemplate.GroupDescriptors.Clear();
            this.gridRoles.MasterTemplate.FilterDescriptors.Clear();
        }
        public void removeRow(RoleModel rw)
        {
            using (gridRoles.DeferRefresh())
            {
                GridViewRowInfo row = this.gridRoles.Rows.Where(s => s.Cells["idRole"].Value.ToString() == rw.idRole.ToString()).FirstOrDefault();
                if (row != null)
                    this.gridRoles.Rows.Remove(row);
            }
        }
        public void SaveLayout(string filename)
        {
            this.gridRoles.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridRoles.LoadLayout(filterFolder + "\\" + filename);
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

        public RoleModel ReturnRowData(RoleModel data)
        {
            return data;
        }
        #endregion Functions

        #region Grid Events
        // eventi na gridu
       private void gridRoles_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                dictionary.Add(column.HeaderText, column.HeaderText);
                //column.MaxWidth = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                column.MinWidth = column.Width;
                //gridRoles.Columns["idEmployee"].IsVisible = false;
                //gridRoles.Columns["idCompany"].IsVisible = false;
            }

            for (int i = 0; i < grid.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(grid.Columns[i].HeaderText) != null)
                        grid.Columns[i].HeaderText = resxSet.GetString(grid.Columns[i].HeaderText);
                }

            }

            if (gridRoles.Columns.Count > 0)
            {
                //for number of rows
                this.gridRoles.SummaryRowsTop.Clear();
                gridRoles.MasterTemplate.EnablePaging = false;
                gridRoles.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridRoles.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridRoles.SummaryRowsTop.Add(summaryRowItem);
                GridViewSummaryRowItem summaryRowItem2 = new GridViewSummaryRowItem();


                //set idRole column on maximum size
                grid.Columns["idRole"].MaxWidth = (int)(this.CreateGraphics().MeasureString(grid.Columns["idRole"].HeaderText, this.Font).Width + 70);
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

       private void gridRoles_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
       {
           if (e.Parent == this.gridRoles.MasterTemplate)
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
        private void gridRoles_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            RoleModel selectedRole = new RoleModel();
            GridViewRowInfo info = this.gridRoles.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedRole = (RoleModel)info.DataBoundItem;

                if (selectedRole != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedRoles = selectedRole;
                    frmRoles frm = new frmRoles(this._clickedRoles);
                    frm.Show();
                  
                }
            }
        }
        private void gridRoles_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridRoles.MasterView.TableHeaderRow && e.CurrentRow != gridRoles.MasterView.TableFilteringRow && e.CurrentRow != gridRoles.MasterView.TableSearchRow)
            {
                RoleModel selectedRole = new RoleModel();
                selectedRole = (RoleModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowRoles = selectedRole;
                    RaiseStatusChanged(selectedRole);
                }
            }
        }
        #endregion

        private void gridRoles_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.gridRoles.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    RoleModel model = (RoleModel)this.gridRoles.CurrentRow.DataBoundItem;
                    frmRoles frm = new frmRoles(model);
                    frm.Show();
                    return;
                }
            }
        }

        private void gridRoles_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridRoles.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridRoles.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridRoles.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridRoles.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridRoles.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridRoles.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridRoles.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridRoles.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }
        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridRoles.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridRoles.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridRoles.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridRoles.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridRoles_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridRoles.IsInEditMode && !(this.gridRoles.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridRoles_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridRoles.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridRoles_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridRoles.FilterChanging -= gridRoles_FilterChanging;

                this.gridRoles.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridRoles.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridRoles.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridRoles.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridRoles.FilterChanging += gridRoles_FilterChanging;
            }  
        }

        private void gridRoles_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridRoles.EndEdit();
            }
        }






      

    }

    public class RolesStatusSelectedRowchanged : EventArgs
    {
        public RoleModel role { get; set; }
    }


}
