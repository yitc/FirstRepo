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

namespace GUI
{    
    public partial class GridReason : System.Windows.Forms.UserControl, IBISGrid
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
        
        public event EventHandler<ReasonStatusSelectedRowchanged> ReasonStatusSelectedRowchanged = delegate { };

        public void RaiseStatusChanged(ReasonModel tyype)
        {
            ReasonStatusSelectedRowchanged(this, new ReasonStatusSelectedRowchanged { type = tyype });
        }

        
        ReasonBUS typeBUS;
        private Telerik.WinControls.UI.RadGridView gridReason;
        private ReasonModel _selectedRowReason;
        private ReasonModel _clickedReason;
        
        // Folder u kome cuva filtere za tipove
        private string filterFolder;

        // Folder u kome cuva labele za tipove
        private string labelFolder;

        Dictionary<string, string> dictionary = new Dictionary<string, string>();        

        private bool _bLoadTreeMenu = false;

        public GridReason()
        {
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\reason")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\reason"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\reason")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\reason"));
            }
            filterFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom filters\\reason");
            labelFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\custom labels\\reason");
            typeBUS = new ReasonBUS();

           InitializeComponent();        
        }
        public void InitializeComponent()
        {
            this.gridReason = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridReason)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridReason.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // gridReason
            // 
            this.gridReason.ColumnChooserSortOrder = Telerik.WinControls.UI.RadSortOrder.Ascending;
            this.gridReason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridReason.EnableFastScrolling = true;
            this.gridReason.Font = new System.Drawing.Font("Verdana", 9F);
            this.gridReason.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.gridReason.MasterTemplate.AllowAddNewRow = false;
            this.gridReason.MasterTemplate.AllowCellContextMenu = false;
            this.gridReason.MasterTemplate.AllowDeleteRow = false;
            this.gridReason.MasterTemplate.AllowEditRow = false;
            this.gridReason.MasterTemplate.AllowSearchRow = true;
            this.gridReason.MasterTemplate.ClipboardCopyMode = Telerik.WinControls.UI.GridViewClipboardCopyMode.Disable;
            this.gridReason.MasterTemplate.ClipboardPasteMode = Telerik.WinControls.UI.GridViewClipboardPasteMode.Disable;
            this.gridReason.MasterTemplate.EnableAlternatingRowColor = true;
            this.gridReason.MasterTemplate.EnableFiltering = true;
            this.gridReason.MasterTemplate.EnablePaging = true;
            this.gridReason.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.gridReason.MasterTemplate.PageSize = 50;
            this.gridReason.MasterTemplate.ShowGroupedColumns = true;
            this.gridReason.Name = "gridReason";
            this.gridReason.NewRowEnterKeyMode = Telerik.WinControls.UI.RadGridViewNewRowEnterKeyMode.None;
            this.gridReason.Padding = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.gridReason.Size = new System.Drawing.Size(150, 150);
            this.gridReason.TabIndex = 0;
            this.gridReason.Text = "Reason Grid";
            this.gridReason.ThemeName = "VisualStudio2012Light";
            this.gridReason.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.gridReason_CurrentRowChanged);
            this.gridReason.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.gridReason_CellDoubleClick);
            this.gridReason.GroupSummaryEvaluate += new Telerik.WinControls.UI.GroupSummaryEvaluateEventHandler(this.gridReason_GroupSummaryEvaluate);
            this.gridReason.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.gridReason_DataBindingComplete);
            this.gridReason.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridReason_KeyDown);
            // 
            // GridReason
            // 
            this.Controls.Add(this.gridReason);
            this.Name = "GridReason";
            ((System.ComponentModel.ISupportInitialize)(this.gridReason.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridReason)).EndInit();
            this.ResumeLayout(false);

        }

        #region GettersSetters
        //getters setters
        public bool bLoadTreeMenu
        {
            get { return _bLoadTreeMenu; }
            set { _bLoadTreeMenu = value; }
        }
        public Telerik.WinControls.UI.RadGridView ReasonGridView
        {
            get { return gridReason; }
        }

        public ReasonModel SelectedRowReason
        {
            get { return _selectedRowReason; }
        }
        public ReasonModel ClickedReason
        {
            get { return _clickedReason; }
        }
        public GridViewColumnCollection Columns
        {
            get { return this.gridReason.Columns; }
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
            return typeBUS.GetAll(Login._user.lngUser);
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
            this.gridReason.DataSource = null;
            this.gridReason.DataSource = binding;

        }

        public void ClearDescriptors()
        {
            this.gridReason.MasterTemplate.SortDescriptors.Clear();
            this.gridReason.MasterTemplate.GroupDescriptors.Clear();
            this.gridReason.MasterTemplate.FilterDescriptors.Clear();
        }
        public void SaveLayout(string filename)
        {
            this.gridReason.SaveLayout(filterFolder + "\\" + filename);
        }
        public void LoadLayout(string filename)
        {
            this.gridReason.LoadLayout(filterFolder + "\\" + filename);
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

        public ReasonModel ReturnRowData(ReasonModel data)
        {
            return data;
        }
        #endregion Functions

        #region Grid Events
        // eventi na gridu
       private void gridReason_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                dictionary.Add(column.HeaderText, column.HeaderText);
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 280);
                column.MinWidth = column.Width;

            }

            for (int i = 0; i < gridReason.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(gridReason.Columns[i].HeaderText)!=null)
                    gridReason.Columns[i].HeaderText = resxSet.GetString(gridReason.Columns[i].HeaderText);
                   
                }



            }
            if (gridReason.Columns.Count > 0)
            {
                //for number of rows
                this.gridReason.SummaryRowsTop.Clear();
                gridReason.MasterTemplate.EnablePaging = false;
                gridReason.MasterTemplate.ShowTotals = true;
                GridViewSummaryItem summaryItem = new GridViewSummaryItem();
                summaryItem.Name = gridReason.Columns[0].Name;
                summaryItem.Aggregate = GridAggregateFunction.Count;

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                this.gridReason.SummaryRowsTop.Add(summaryRowItem);
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

       private void gridReason_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
       {
           if (e.Parent == this.gridReason.MasterTemplate)
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
        private void gridReason_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            ReasonModel selectedReason = new ReasonModel();
            GridViewRowInfo info = this.gridReason.CurrentRow;

            if (info != null && e.RowIndex >= 0)
            {
                selectedReason = (ReasonModel)info.DataBoundItem;

                if (selectedReason != null)
                {
                    //System.Windows.Forms.MessageBox.Show(selectedPerson.lastname + " " + selectedPerson.midname + " " + selectedPerson.lastname);
                    this._clickedReason = selectedReason;
                    frmReason frm = new frmReason(this._clickedReason);
                    frm.ShowDialog();

                    if (frm.isChanged == true)
                    {
                        ReasonBUS nbus = new ReasonBUS();


                        gridReason.DataSource = null;
                        gridReason.DataSource = nbus.GetAll(Login._user.lngUser);
                    }
                }
            }
        }
        private void gridReason_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null && e.CurrentRow != gridReason.MasterView.TableHeaderRow && e.CurrentRow != gridReason.MasterView.TableFilteringRow && e.CurrentRow != gridReason.MasterView.TableSearchRow)
            {
                ReasonModel selectedReason = new ReasonModel();
                selectedReason = (ReasonModel)e.CurrentRow.DataBoundItem;
                if (e.CurrentRow.DataBoundItem != null)
                {
                    this._selectedRowReason = selectedReason;
                    RaiseStatusChanged(selectedReason);
                }
            }
        }
        #endregion

        private void gridReason_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.gridReason.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    ReasonModel model = (ReasonModel)this.gridReason.CurrentRow.DataBoundItem;
                    frmReason frm = new frmReason(model);
                    frm.Show();
                    return;
                }
            }
        }

      

    }

    public class ReasonStatusSelectedRowchanged : EventArgs
    {
        public ReasonModel type { get; set; }
    }


}
