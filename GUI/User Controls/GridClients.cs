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
    public partial class GridViewClients : System.Windows.Forms.UserControl, IBISGrid
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
        
        public event EventHandler<ClientStatusSelectedRowchanged> ClientStatusSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(ClientModel celient)
        {
            ClientStatusSelectedRowchanged(this, new ClientStatusSelectedRowchanged { client = celient });
        }

        
        ClientBUS clientBUS;
        private Telerik.WinControls.UI.RadGridView gridClients;
        private ClientModel _selectedRowClient;
        private ClientModel _clickedClient;
        
        // Folder u kome cuva filtere za Cliente
        private string filterFolder;

        // Folder u kome cuva labele za Cliente
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridViewClients()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\client")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\client"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\client")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\client"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\client");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\client");
            clientBUS = new ClientBUS();
           
            InitializeComponent();
            this.gridClients.EnterKeyMode = RadGridViewEnterKeyMode.None;
            this.gridClients.GridBehavior = new MyGridClientBehavior();

        }
        public void InitializeComponent()
        {
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.gridClients = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridClients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridClients.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridClients
            // 
            this.gridClients.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridClients.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridClients.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridClients.MasterTemplate.AllowAddNewRow = false;
            this.gridClients.MasterTemplate.AllowCellContextMenu = false;
            this.gridClients.MasterTemplate.AllowDeleteRow = false;
            this.gridClients.MasterTemplate.AllowEditRow = false;
            this.gridClients.MasterTemplate.AllowSearchRow = true;
            this.gridClients.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridClients.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridClients.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridClients.MasterTemplate.EnableFiltering = true;
            this.gridClients.MasterTemplate.EnablePaging = true;
            this.gridClients.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridClients.MasterTemplate.PageSize = 50;
            this.gridClients.MasterTemplate.ShowFilterCellOperatorText = false;
            this.gridClients.MasterTemplate.ShowGroupedColumns = true;
            this.gridClients.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.gridClients.Name = "gridClients";
            this.gridClients.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridClients.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridClients.Size = new System.Drawing.Size(150, 150);
            this.gridClients.TabIndex = 0;
            this.gridClients.Text = "Clients Grid";
            this.gridClients.ThemeName = "VisualStudio2012Light";
            this.gridClients.CellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.gridClients_CellFormating);
            this.gridClients.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.gridClients_CellBeginEdit);
            this.gridClients.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridClients_CellEditorInitialized);
            this.gridClients.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridClients_CellEndEdit);
            this.gridClients.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridPersons_CurrentRowChanged);
            this.gridClients.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridClients_CellClick);
            this.gridClients.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridClients_CellDoubleClick);
            this.gridClients.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridClients_GroupSummaryEvaluate);
            this.gridClients.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridClients_DataBindingComplete);
            this.gridClients.FilterChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.gridClients_FilterChanging);
            this.gridClients.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridClients_KeyDown);
            // 
            // GridViewClients
            // 
            this.Controls.Add(this.gridClients);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GridViewClients";
            ((System.ComponentModel.ISupportInitialize)(this.gridClients.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridClients)).EndInit();
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
            get { return gridClients; }
        }

        public ClientModel SelectedRowClient
        {
            get { return _selectedRowClient; }
        }
        public ClientModel ClickedPerson
        {
            get { return _clickedClient; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridClients.Columns; }
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
            return clientBUS.GetAllClients(idLang);
        }
        public List<FilterModel> ReturnFilters()
        {
            return Login._clientFilters;
        }
        public List<LabelModel> ReturnLabels()
        {
            return Login._clientLabels;
        }       
      
        public void SetDataPersonBinding(List<IModel> binding)
        {
            this.gridClients.DataSource = null;
            this.gridClients.DataSource = binding;

        }

        public void ClearDescriptors()
        {
            this.gridClients.MasterTemplate.SortDescriptors.Clear();
            this.gridClients.MasterTemplate.GroupDescriptors.Clear();
            this.gridClients.MasterTemplate.FilterDescriptors.Clear();
        }
        public void
           removeRow(ClientModel rw)
        {
            using (gridClients.DeferRefresh())
            {
                GridViewRowInfo row = this.gridClients.Rows.Where(s => s.Cells["idClient"].Value.ToString() == rw.idClient.ToString()).FirstOrDefault();
                if (row != null)
                    this.gridClients.Rows.Remove(row);
            }
        }

        public void SaveLayout(string filename)
        {
            this.gridClients.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridClients.LoadLayout(filterFolder + "\\" + filename);
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

        public ClientModel ReturnRowData(ClientModel data)
        {
            return data;
        }
        #endregion Functions

        #region Grid Events
        // eventi na gridu
        private void gridClients_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
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

            
            if (gridClients.Columns.Count > 0)
            {
                grid.Columns["idTypeClient"].IsVisible = false;
                grid.Columns["userCreated"].IsVisible = false;
                grid.Columns["userModified"].IsVisible = false;
                //for number of rows
                this.gridClients.SummaryRowsTop.Clear();
                gridClients.MasterTemplate.EnablePaging = false;
                gridClients.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridClients.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridClients.SummaryRowsTop.Add(summaryRowItem);
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
      
        private void gridClients_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.Parent == this.gridClients.MasterTemplate)
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
        private void gridClients_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            ClientModel selectedClient = new ClientModel();
            GridViewRowInfo info = this.gridClients.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedClient = (ClientModel)info.DataBoundItem;

                if (selectedClient != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedClient = selectedClient;

                    frmClient frm = new frmClient(this._clickedClient);
                    //     frmClient form = new frmClient(this._clickedClient);
                    bool formfound = false;
                    foreach (Form f in Application.OpenForms)
                    {
                        if (f is frmClient)
                        {
                            if ((int)f.Tag == this._clickedClient.idClient)
                            {
                                f.BringToFront();
                                formfound = true;
                                break;
                            }

                        }
                    }

                    if (formfound == false)
                    {
                        frm.Tag = this._clickedClient.idClient;
                        frm.Show();
                    }                    

                }
            }
        }
        private void gridPersons_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridClients.MasterView.TableHeaderRow && e.CurrentRow != gridClients.MasterView.TableFilteringRow && e.CurrentRow != gridClients.MasterView.TableSearchRow)
            {
                ClientModel selectedClient = new ClientModel();
                selectedClient = (ClientModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowClient = selectedClient;
                    RaiseStatusChanged(selectedClient);
                }
            }
        }

        private void gridClients_CellFormating(object sender, CellFormattingEventArgs e)
        {            
            if (e.Column.Name == "dtCreated")
            {
                if (e.Column.IsVisible == true)
                {
                    try
                    {
                        //DateTime temp = DateTime.Parse(e.CellElement.Text);
                        DateTime temp = DateTime.Parse(e.CellElement.Value.ToString());
                        e.CellElement.Text = temp.ToString("dd-MM-yyyy hh:mm");
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            if (e.Column.Name == "dtModified")
            {
                if (e.Column.IsVisible == true)
                {
                    try
                    {
                        //DateTime temp = DateTime.Parse(e.CellElement.Text);
                        DateTime temp = DateTime.Parse(e.CellElement.Value.ToString());
                        e.CellElement.Text = temp.ToString("dd-MM-yyyy hh:mm");
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }           
        }
        #endregion

        private void gridClients_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //if (this.gridClients.CurrentRow.Index - 1 >= 0)
            //{
            //    if (e.KeyData == Keys.Enter)
            //    {
            //        //RadMessageBox.Show("LALA");
            //        int index = this.gridClients.CurrentRow.Index - 1;
            //        ClientModel model = (ClientModel)this.gridClients.Rows[index].DataBoundItem;

            //        this._clickedClient = model;

            //        frmClient frm = new frmClient(model);

            //        bool formfound = false;
            //        foreach (Form f in Application.OpenForms)
            //        {
            //            if (f is frmClient)
            //            {
            //                if ((int)f.Tag == this._clickedClient.idClient)
            //                {
            //                    f.BringToFront();
            //                    formfound = true;
            //                    break;
            //                }

            //            }
            //        }

            //        if (formfound == false)
            //        {
            //            frm.Tag = this._clickedClient.idClient;
            //            frm.Show();
            //        }

            //    }


            //}
        }

        private void gridClients_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
            {
                if (this.gridClients.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridClients.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridClients.ActiveEditor).EditorElement).Format = DateTimePickerFormat.Custom;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridClients.ActiveEditor).EditorElement).CustomFormat = "d-M-yyyy";

                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridClients.ActiveEditor).EditorElement).ShowTimePicker = false;
                        //((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridPersons.ActiveEditor).EditorElement).ShowUpDown = true;                        
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridClients.ActiveEditor).EditorElement).KeyDown -= KeyDown_FilterCell;
                        ((RadDateTimeEditorElement)((RadDateTimeEditor)this.gridClients.ActiveEditor).EditorElement).KeyDown += KeyDown_FilterCell;
                    }
                }
            }
        }
        private void KeyDown_FilterCell(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back)
            {
                if (this.gridClients.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridClients.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = DateTime.Parse("01/01/1900");
                        // this.gridPersons.CurrentCell.Value = null;                          
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (this.gridClients.ActiveEditor is RadDateTimeEditor)
                {
                    RadDateTimeEditor editor = this.gridClients.ActiveEditor as RadDateTimeEditor;
                    if (editor != null)
                    {
                        editor.Value = null;
                        // this.gridPersons.CurrentCell.Value = null;                          
                    }
                }
            }
        }

        private FilterDescriptor lastFilterDescriptor;

        private void gridClients_FilterChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (this.gridClients.IsInEditMode && !(this.gridClients.CurrentColumn is GridViewCheckBoxColumn))
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

        private void gridClients_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (lastFilterDescriptor == null && !(e.Column is GridViewCheckBoxColumn))
            {
                lastFilterDescriptor = gridClients.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
            }
        }

        private void gridClients_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            var filteringRow = e.Row as GridViewFilteringRowInfo;

            if (filteringRow != null && !(e.Column is GridViewCheckBoxColumn))
            {
                this.gridClients.FilterChanging -= gridClients_FilterChanging;

                this.gridClients.FilterDescriptors.Remove(e.Column.Name);
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
                    if (!this.gridClients.FilterDescriptors.Contains(e.Column.Name))
                    {
                        this.gridClients.FilterDescriptors.Add(lastFilterDescriptor);
                    }
                    else
                    {
                        //ako descriptr vec postoji, setuj samo operator
                        FilterDescriptor tmpdesc = gridClients.FilterDescriptors.FirstOrDefault(x => x.PropertyName == e.Column.Name);
                        tmpdesc.Operator = lastFilterDescriptor.Operator;
                    }

                    //if (cellinf.Value == lastFilterDescriptor.Value)
                    //    this.gridPersons.FilterDescriptors.Add(lastFilterDescriptor);                                      
                    //else                    
                    //    cellinf.Value = lastFilterDescriptor.Value;                    

                    //e.Row.Cells[e.Column.Name].Value = lastFilterDescriptor.Value;

                    lastFilterDescriptor = null;
                }
                else
                {
                    //this.gridClients.FilterChanged += gridClients_FilterChanging;
                    // e.Row.Cells[e.Column.Name].Value = null;
                }
                this.gridClients.FilterChanging += gridClients_FilterChanging;
            }  
        }

        private void gridClients_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column is GridViewCheckBoxColumn && e.Row is GridViewFilteringRowInfo)
            {
                this.gridClients.EndEdit();
            }
        }

    }

    public class ClientStatusSelectedRowchanged : EventArgs
    {
        public ClientModel client { get; set; }
    }

    class MyGridClientBehavior : BaseGridBehavior
    {
        public override bool ProcessKeyDown(KeyEventArgs keys)
        {
            if (keys.KeyData == Keys.Enter || keys.KeyData == Keys.Return)
            {
                if (this.GridControl.CurrentRow.Index >= 0)
                {
                    if (keys.KeyData == Keys.Enter || keys.KeyData == Keys.Return)
                    {
                        int index = this.GridControl.CurrentRow.Index;
                        ClientModel model = (ClientModel)this.GridControl.Rows[index].DataBoundItem;

                        frmClient frm = new frmClient(model);

                        bool formfound = false;
                        foreach (Form f in Application.OpenForms)
                        {
                            if (f is frmClient)
                            {
                                if ((int)f.Tag == model.idClient)
                                {
                                    f.BringToFront();
                                    formfound = true;
                                    break;
                                }
                            }
                        }

                        if (formfound == false)
                        {
                            frm.Tag = model.idClient;
                            frm.Show();
                        }

                        keys.SuppressKeyPress = true;
                                   
                    }
                }
                else if (this.GridControl.CurrentRow != null && this.GridControl.CurrentRow is GridViewFilteringRowInfo && this.GridControl.IsInEditMode)
                {
                    this.GridControl.EndEdit();
                }
            }           
            else if (keys.KeyData == Keys.Down)
            {
                this.GridControl.GridNavigator.SelectNextRow(1);
            }
            else if (keys.KeyData == Keys.Up)
            {
                this.GridControl.GridNavigator.SelectPreviousRow(1);
            }

            //return base.ProcessKey(keys);
            return true;
        }
    }

}
