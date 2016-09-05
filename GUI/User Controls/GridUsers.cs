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
    public partial class GridViewUsers : System.Windows.Forms.UserControl, IBISGrid
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
        
        public event EventHandler<UserStatusSelectedRowchanged> UserStatusSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(UsersAllModel usser)
        {
            UserStatusSelectedRowchanged(this, new UserStatusSelectedRowchanged { user = usser });
        }

        
        UsersAllBUS userBUS;
        private Telerik.WinControls.UI.RadGridView gridUsers;
        private UsersAllModel _selectedRowUser;
        private UsersAllModel _clickedUser;
        public bool isChanged = false;
        
        // Folder u kome cuva filtere za Usere
        private string filterFolder;

        // Folder u kome cuva labele za Usere
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridViewUsers()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\users")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\users"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\users")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\users"));
            } 
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,Login._user.username + "\\filters\\custom filters\\users");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\users");
            userBUS = new UsersAllBUS();

           InitializeComponent();        
        }
        private void gridUsers_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            GridViewDataColumn dataColumn = e.CellElement.ColumnInfo as GridViewDataColumn;

            if (dataColumn != null && dataColumn.Name == "password")
            {
                object value = e.CellElement.RowInfo.Cells["Password"].Value;
                string text = String.Empty;
                if (value != null)
                {
                    int passwordLen = Convert.ToString(value).Length;
                    text = String.Join("*", new string[passwordLen+1]);
                }

                e.CellElement.Text = text;
            }

        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridUsers = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridUsers.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridUsers
            // 
            this.gridUsers.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridUsers.EnableFastScrolling = true;
            this.gridUsers.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridUsers.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridUsers.MasterTemplate.AllowAddNewRow = false;
            this.gridUsers.MasterTemplate.AllowCellContextMenu = false;
            this.gridUsers.MasterTemplate.AllowDeleteRow = false;
            this.gridUsers.MasterTemplate.AllowEditRow = false;
            this.gridUsers.MasterTemplate.AllowSearchRow = true;
            this.gridUsers.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridUsers.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridUsers.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridUsers.MasterTemplate.EnableFiltering = true;
            this.gridUsers.MasterTemplate.EnablePaging = true;
            this.gridUsers.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridUsers.MasterTemplate.PageSize = 50;
            this.gridUsers.MasterTemplate.ShowGroupedColumns = true;
            this.gridUsers.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridUsers.Name = "gridUsers";
            this.gridUsers.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridUsers.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridUsers.Size = new System.Drawing.Size(150, 150);
            this.gridUsers.TabIndex = 0;
            this.gridUsers.Text = "Users Grid";
            this.gridUsers.ThemeName = "VisualStudio2012Light";
            this.gridUsers.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.gridUsers_CellFormatting);
            this.gridUsers.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridUsers_CellBeginEdit);
            this.gridUsers.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridUsers_CellEditorInitialized);
            this.gridUsers.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridUsers_CellEndEdit);
            this.gridUsers.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridUsers_CurrentRowChanged);
            this.gridUsers.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridUsers_CellClick);
            this.gridUsers.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridUsers_CellDoubleClick);
            this.gridUsers.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridUsers_GroupSummaryEvaluate);
            this.gridUsers.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridUsers_DataBindingComplete);
            this.gridUsers.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridUsers_FilterChanging);
            this.gridUsers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridUsers_KeyDown);
            // 
            // GridViewUsers
            // 
            this.Controls.Add(this.gridUsers);
            this.Name = "GridViewUsers";
            ((System.ComponentModel.ISupportInitialize)(this.gridUsers.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridUsers)).EndInit();
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
            get { return gridUsers; }
        }

        public UsersAllModel SelectedRowUser
        {
            get { return _selectedRowUser; }
        }
        public UsersAllModel ClickedUser
        {
            get { return _clickedUser; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridUsers.Columns; }
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
            return userBUS.GetAllUsers();
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
            this.gridUsers.DataSource = null;
            this.gridUsers.DataSource = binding;

        }

        public void ClearDescriptors()
        {
            this.gridUsers.MasterTemplate.SortDescriptors.Clear();
            this.gridUsers.MasterTemplate.GroupDescriptors.Clear();
            this.gridUsers.MasterTemplate.FilterDescriptors.Clear();
        }
        public void removeRow(UsersAllModel rw)
        {
            using (gridUsers.DeferRefresh())
            {
                GridViewRowInfo row = this.gridUsers.Rows.Where(s => s.Cells["idUser"].Value.ToString() == rw.idUser.ToString()).FirstOrDefault();
                if (row != null)
                    this.gridUsers.Rows.Remove(row);
            }
        }
        public void SaveLayout(string filename)
        {
            this.gridUsers.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridUsers.LoadLayout(filterFolder + "\\" + filename);
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
        private void gridUsers_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                dictionary.Add(column.HeaderText, column.HeaderText);
                //column.MaxWidth = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                column.MinWidth = column.Width;
                gridUsers.Columns["idEmployee"].IsVisible = false;
                gridUsers.Columns["idCompany"].IsVisible = false;
                if (column.GetType() == typeof(GridViewDateTimeColumn))
                {
                    if (column.Name.ToLower() != "dtUserModified".ToLower() && column.Name.ToLower() != "dtCreated".ToLower() && column.Name.ToLower() != "dtModified".ToLower() && column.Name.ToLower() != "dtUserLogout".ToLower() && column.Name.ToLower() != "dtUserLogin".ToLower() && column.Name.ToLower() != "dtUserCreated".ToLower())
                    {
                        column.FormatString = "{0: dd-MM-yyyy}";
                    }
                }

                for (int i = 0; i < grid.Columns.Count; i++)
                {

                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString(grid.Columns[i].HeaderText) != null)
                            grid.Columns[i].HeaderText = resxSet.GetString(grid.Columns[i].HeaderText);
                    }

                }

                if (gridUsers.Columns.Count > 0)
                {
                    //for number of rows
                    this.gridUsers.SummaryRowsTop.Clear();
                    gridUsers.MasterTemplate.EnablePaging = false;
                    gridUsers.MasterTemplate.ShowTotals = true;
                    GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                    summaryItem.Name = gridUsers.Columns[0].Name;
                    summaryItem.Aggregate = GridAggregateFunction.Count;

                    GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                    summaryRowItem.Add(summaryItem);
                    this.gridUsers.SummaryRowsTop.Add(summaryRowItem);
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
        }
       private void gridUsers_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
       {
           if (e.Parent == this.gridUsers.MasterTemplate)
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
        private void gridUsers_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            UsersAllModel selectedUser = new UsersAllModel();
            GridViewRowInfo info = this.gridUsers.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedUser = (UsersAllModel)info.DataBoundItem;

                if (selectedUser != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedUser = selectedUser;
                    frmUsers frm = new frmUsers(this._clickedUser);
                    frm.Show();
                  
                }
            }
        }
        private void gridUsers_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridUsers.MasterView.TableHeaderRow && e.CurrentRow != gridUsers.MasterView.TableFilteringRow && e.CurrentRow != gridUsers.MasterView.TableSearchRow)
            {
                UsersAllModel selectedUser = new UsersAllModel();
                selectedUser = (UsersAllModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowUser = selectedUser;
                    RaiseStatusChanged(selectedUser);
                }
            }
        }
        #endregion

        private void gridUsers_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.gridUsers.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {                    
                    UsersAllModel model = (UsersAllModel)this.gridUsers.CurrentRow.DataBoundItem;
                    frmUsers frm = new frmUsers(model);
                    frm.Show();
                    // e.SuppressKeyPress = true;
                    //e.Handled = true;
                    return;

                }


            }
        }

        private void gridUsers_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridUsers.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridUsers.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridUsers.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridUsers.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridUsers.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridUsers.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridUsers.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridUsers.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }

        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridUsers.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridUsers.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridUsers.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridUsers.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridUsers_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridUsers.IsInEditMode && !(this.gridUsers.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridUsers_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridUsers.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridUsers_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridUsers.FilterChanging -= gridUsers_FilterChanging;

                this.gridUsers.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridUsers.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridUsers.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridUsers.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    lastFilterDescriptor = null;
                }

                this.gridUsers.FilterChanging += gridUsers_FilterChanging;
            }  
        }

        private void gridUsers_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridUsers.EndEdit();
            }
        }




      

    }

    public class UserStatusSelectedRowchanged : EventArgs
    {
        public UsersAllModel user { get; set; }
    }


}
