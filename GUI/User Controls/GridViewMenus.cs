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
    public partial class GridViewMenus : System.Windows.Forms.UserControl, IBISGrid
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
        
        public event EventHandler<MenusStatusSelectedRowchanged> MenusStatusSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(MenuRoleModel Menus)
        {
            MenusStatusSelectedRowchanged(this, new MenusStatusSelectedRowchanged { Menu = Menus });
        }

        
        MenuBUS MenusBUS;
        private Telerik.WinControls.UI.RadGridView GridMenus;
        private MenuRoleModel _selectedRowMenus;
        private MenuRoleModel _clickedMenus;
        public bool isChanged = false;
        GridViewTemplate template;
        GridViewTemplate secondChildtemplate;
        
        // Folder u kome cuva filtere za Menu
        private string filterFolder;

        // Folder u kome cuva labele za Menu
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridViewMenus()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\menus")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\menus"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\menus")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\menus"));
            } 
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,Login._user.username + "\\filters\\custom filters\\menus");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\menus");
            MenusBUS = new MenuBUS();

           InitializeComponent();        
        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.GridMenus = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.GridMenus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridMenus.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // GridMenus
            // 
            this.GridMenus.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.GridMenus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridMenus.EnterKeyMode = Telerik.WinControls.UI.RadGridViewEnterKeyMode.EnterMovesToNextRow;
            this.GridMenus.Font = new System.Drawing.Font("Verdana", 9F);
            this.GridMenus.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.GridMenus.MasterTemplate.AllowAddNewRow = false;
            this.GridMenus.MasterTemplate.AllowCellContextMenu = false;
            this.GridMenus.MasterTemplate.AllowDeleteRow = false;
            this.GridMenus.MasterTemplate.AllowEditRow = false;
            this.GridMenus.MasterTemplate.AllowSearchRow = true;
            this.GridMenus.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.GridMenus.MasterTemplate.EnableAlternatingRowColor = true;
            this.GridMenus.MasterTemplate.EnableFiltering = true;
            this.GridMenus.MasterTemplate.EnablePaging = true;
            this.GridMenus.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.GridMenus.MasterTemplate.PageSize = 50;
            this.GridMenus.MasterTemplate.ShowGroupedColumns = true;
            this.GridMenus.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.GridMenus.Name = "GridMenus";
            this.GridMenus.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.GridMenus.Size = new System.Drawing.Size(150, 150);
            this.GridMenus.TabIndex = 0;
            this.GridMenus.Text = "Menus Grid";
            this.GridMenus.ThemeName = "VisualStudio2012Light";
            this.GridMenus.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.GridMenus_CellBeginEdit);
            this.GridMenus.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.GridMenus_CellEditorInitialized);
            this.GridMenus.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.GridMenus_CellEndEdit);
            this.GridMenus.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.GridMenus_CurrentRowChanged);
            this.GridMenus.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.GridMenus_CellClick);
            this.GridMenus.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.GridMenus_CellDoubleClick);
            this.GridMenus.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.GridMenus_GroupSummaryEvaluate);
            this.GridMenus.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.GridMenus_DataBindingComplete);
            this.GridMenus.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.GridMenus_FilterChanging);
            // 
            // GridViewMenus
            // 
            this.Controls.Add(this.GridMenus);
            this.Name = "GridViewMenus";
            ((System.ComponentModel.ISupportInitialize)(this.GridMenus.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridMenus)).EndInit();
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
            get { return GridMenus; }
        }

        public MenuRoleModel SelectedRowMenus
        {
            get { return _selectedRowMenus; }
        }
        public MenuRoleModel ClickedMenus
        {
            get { return _clickedMenus; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.GridMenus.Columns; }
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
            return MenusBUS.GetMenusForGrid(lang);
        }
        public List<FilterModel> ReturnFilters()
        {
            return Login._menusFilters;
        }
        public List<LabelModel> ReturnLabels()
        {
            return Login._menusLabels; 
        }       
      
        public void SetDataPersonBinding(List<IModel> binding)
        {
            this.GridMenus.DataSource = null;
            this.GridMenus.DataSource = binding;

            template = new GridViewTemplate();
            template.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            template.AllowAddNewRow = false;
            template.AllowDeleteRow = false;
            template.AllowEditRow = false;
            template.DataSource = new MenuBUS().GetSubMenusForGrid(Login._user.lngUser);
           
            GridMenus.MasterTemplate.Templates.Add(template);

            GridViewRelation relation = new GridViewRelation(GridMenus.MasterTemplate);
            relation.ChildTemplate = template;
            relation.RelationName = "MenusSubMenus";
            relation.ParentColumnNames.Add("idMenu");
            relation.ChildColumnNames.Add("idMenuSuperior");
            GridMenus.Relations.Add(relation);

            secondChildtemplate = new GridViewTemplate();
            secondChildtemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            secondChildtemplate.AllowAddNewRow = false;
            secondChildtemplate.AllowDeleteRow = false;
            secondChildtemplate.AllowEditRow = false;
            secondChildtemplate.DataSource = template.DataSource;
           
            template.Templates.Add(secondChildtemplate);

            GridViewRelation relation2 = new GridViewRelation(template);
            relation2.ChildTemplate = secondChildtemplate;
            relation2.RelationName = "SubMenusSubMenus";
            relation2.ParentColumnNames.Add("idMenu");
            relation2.ChildColumnNames.Add("idMenuSuperior");
            GridMenus.Relations.Add(relation2);

           
        }




        public void ClearDescriptors()
        {
            this.GridMenus.MasterTemplate.SortDescriptors.Clear();
            this.GridMenus.MasterTemplate.GroupDescriptors.Clear();
            this.GridMenus.MasterTemplate.FilterDescriptors.Clear();
        }
        public void SaveLayout(string filename)
        {
            this.GridMenus.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.GridMenus.LoadLayout(filterFolder + "\\" + filename);
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

        public MenuRoleModel ReturnRowData(MenuRoleModel data)
        {
            return data;
        }
        #endregion Functions

        #region Grid Events
        // eventi na gridu
       private void GridMenus_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                dictionary.Add(column.HeaderText, column.HeaderText);
                //column.MaxWidth = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                column.MinWidth = column.Width;
                //GridMenus.Columns["idEmployee"].IsVisible = false;
                //GridMenus.Columns["idCompany"].IsVisible = false;
            }

            for (int i = 0; i < grid.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(grid.Columns[i].HeaderText) != null)
                        grid.Columns[i].HeaderText = resxSet.GetString(grid.Columns[i].HeaderText);
                }

            }

            if (GridMenus.Columns.Count > 0)
            {
                //for number of rows
                this.GridMenus.SummaryRowsTop.Clear();
                GridMenus.MasterTemplate.EnablePaging = false;
                GridMenus.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = GridMenus.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.GridMenus.SummaryRowsTop.Add(summaryRowItem);
                GridViewSummaryRowItem summaryRowItem2 = new GridViewSummaryRowItem();


                //set idMenu column on maximum size
                grid.Columns["idMenu"].MaxWidth = (int)(this.CreateGraphics().MeasureString(grid.Columns["idMenu"].HeaderText, this.Font).Width + 70);


                GridMenus.Columns["idMenu"].IsVisible = false;
                GridMenus.Columns["idMenuSuperior"].IsVisible = false;
                GridMenus.Columns["idSecurity"].IsVisible = false;

            }
            if (template != null)
            {
                if (template.Columns.Count > 0)
                {
                    template.Columns["idMenu"].IsVisible = false;
                    template.Columns["idMenuSuperior"].IsVisible = false;
                    template.Columns["idSecurity"].IsVisible = false;
                }
            }

            if (secondChildtemplate != null)
            {
                if (secondChildtemplate.Columns.Count > 0)
                {
                    secondChildtemplate.Columns["idMenu"].IsVisible = false;
                    secondChildtemplate.Columns["idMenuSuperior"].IsVisible = false;
                    secondChildtemplate.Columns["idSecurity"].IsVisible = false;
                }
            }
            
            
        }

       private void GridMenus_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
       {
           if (e.Parent == this.GridMenus.MasterTemplate)
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
        private void GridMenus_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            MenuRoleModel selectedMenu = new MenuRoleModel();
            GridViewRowInfo info = this.GridMenus.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedMenu = (MenuRoleModel)info.DataBoundItem;

                if (selectedMenu != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedMenus = selectedMenu;
                    frmMenus frm = new frmMenus(this._clickedMenus);
                    frm.Show();
                  
                }
            }
        }
        private void GridMenus_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != GridMenus.MasterView.TableHeaderRow && e.CurrentRow != GridMenus.MasterView.TableFilteringRow && e.CurrentRow != GridMenus.MasterView.TableSearchRow)
            {
                MenuRoleModel selectedMenu = new MenuRoleModel();
                selectedMenu = (MenuRoleModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowMenus = selectedMenu;
                    RaiseStatusChanged(selectedMenu);
                }
            }
        }
        #endregion

        private void GridMenus_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.GridMenus.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.GridMenus.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.GridMenus.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.GridMenus.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.GridMenus.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.GridMenus.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.GridMenus.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.GridMenus.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.GridMenus.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.GridMenus.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.GridMenus.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.GridMenus.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void GridMenus_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.GridMenus.IsInEditMode && !(this.GridMenus.CurrentColumn is GridViewCheckBoxColumn))
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

        private void GridMenus_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = GridMenus.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void GridMenus_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.GridMenus.FilterChanging -= GridMenus_FilterChanging;

                this.GridMenus.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.GridMenus.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.GridMenus.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = GridMenus.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.GridMenus.FilterChanging += GridMenus_FilterChanging;
            }  
        }

        private void GridMenus_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.GridMenus.EndEdit();
            }
        }



    }

    public class MenusStatusSelectedRowchanged : EventArgs
    {
        public MenuRoleModel Menu { get; set; }
    }


}
