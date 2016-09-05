using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using BIS.Model;
using BIS.Business;
using System.IO;
using System.Resources;
using Telerik.WinControls;

namespace GUI
{
    public partial class GridLookupTraveler : RadForm
    {
        List<IModel> personModelList;
        List<PersonAddressModel> city;
        List<MedicalVoluntaryModel> medical;

        private int idArrangement = 0;
        private int idContPers = 0;

        public IModel selectedRow;
        string layoutLookup;

        private bool pageLoaded = false;

        private string strCheckAll = "Check All";
        private string strUncheckAll = "Uncheck All";

        public GridLookupTraveler(int idArrangement, int idContPers, string nameForm)
        {
            InitializeComponent();

            gridTraveler.DataSource = null;
            gridTraveler.AllowAutoSizeColumns = true;
            this.idArrangement = idArrangement;
            this.idContPers = idContPers;

            //set name form and icon
            this.Name = nameForm;
            this.Icon = Login.iconForm;

            TranslationBUS tb = new TranslationBUS();
            List<TranslationModel> tm = new List<TranslationModel>();
            tm = tb.CheckIfTranslationExists(Login._user.lngUser, nameForm);
            if (tm != null)
            {
                if (tm.Count > 0)
                {
                    nameForm = tm[0].stringKey;
                }
            }

            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_lookup")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_lookup"));

            }
            layoutLookup = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_lookup\\grid_lookup_traveler.xml");


            //do translate form form text 
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(nameForm) != null)
                    this.Text = resxSet.GetString(nameForm);

                if (resxSet.GetString(strCheckAll) != null)
                    strCheckAll = resxSet.GetString(strCheckAll);

                if (resxSet.GetString(strUncheckAll) != null)
                    strUncheckAll = resxSet.GetString(strUncheckAll);

                if (resxSet.GetString(nameForm) != null)
                    this.Text = resxSet.GetString(nameForm);

                for (int i = 0; i < pageViewFilters.Pages.Count; i++)
                {
                    if (resxSet.GetString(pageViewFilters.Pages[i].Text) != null)
                        pageViewFilters.Pages[i].Text = resxSet.GetString(pageViewFilters.Pages[i].Text);
                }
            }
            
            if (File.Exists(layoutLookup))
            {
                gridTraveler.LoadLayout(layoutLookup);
            }
        }


        private void GridLookupVoluntary_Load(object sender, EventArgs e)
        {
            List<string> idQueryType = new List<string>();
            foreach(int i in MainForm.idLabelList)
            {
                idQueryType.Add(i.ToString());
            }

            MedicalVoluntaryBUS vtb = new MedicalVoluntaryBUS();
            medical = vtb.GetMedicalForBooking(idQueryType);

            radListPref.DataSource = medical;
            radListPref.DataMember = "idQuest";
            radListPref.DisplayMember = "txtQuest";



            LoadGridData();

            pageLoaded = true;

            pageViewFilters.SelectedPage = tabMedical;
            
        }

        private void LoadGridData()
        {
            List<string> selectedIDAns = new List<string>();
            List<string> selectedIDQuests = new List<string>();

            List<int> selectedIDAns_Medical = new List<int>();
            List<int> selectedIDQuests_Medical = new List<int>();

            
            foreach (ListViewDataItem item in radListPref.Items)
            {
                if (item.DataBoundItem != null)
                {
                    if (item.CheckState == Telerik.WinControls.Enumerations.ToggleState.On)  //On
                    {
                        MedicalVoluntaryModel m = (MedicalVoluntaryModel)item.DataBoundItem;
                        selectedIDAns_Medical.Add((int)m.idAns);
                        selectedIDQuests_Medical.Add(m.idQuest);
                    }
                }
            }

            PersonBUS pbus = new PersonBUS();
            personModelList = pbus.GetTravelers(idArrangement, idContPers, Login._user.lngUser, selectedIDAns, selectedIDQuests, selectedIDAns_Medical, selectedIDQuests_Medical);
            gridTraveler.DataSource = personModelList;

        }
        private void radListFunctions_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
                e.Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off; //On
        }

        private void radListFunctions_ItemCheckedChanged(object sender, ListViewItemEventArgs e)
        {
            if (pageLoaded == true)
            {
                if (e.Item.DataBoundItem != null)
                {
                    LoadGridData();

                 
                }
            }
        }

        private void radListTrips_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            e.Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off; //On
        }

        private void radListTrips_ItemCheckedChanged(object sender, ListViewItemEventArgs e)
        {
            if (pageLoaded == true)
            {
                if (e.Item.DataBoundItem != null)
                {
                    LoadGridData();
                }
            }
        }
        private void radListSkills_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            e.Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;  //On
        }
        private void radListSkills_ItemCheckedChanged(object sender, ListViewItemEventArgs e)
        {
            if (pageLoaded == true)
            {
                if (e.Item.DataBoundItem != null)
                {
                    LoadGridData();
                }
            }
        }

        private void gridTraveler_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.gridTraveler.CurrentRow;
            if (e.RowIndex >= 0)
            {
                
                //set selected row in model so you can use any data you'll need
                selectedRow = (IModel)info.DataBoundItem;
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
        }

        private void gridVolontary_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            var grid = sender as RadGridView;

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                foreach (var column in grid.Columns)
                {
                    if (resxSet.GetString(column.HeaderText) != null && resxSet.GetString(column.HeaderText) != "")
                        column.HeaderText = resxSet.GetString(column.HeaderText);
                    if (column.GetType() == typeof(GridViewDateTimeColumn))
                    {
                        if (column.Name.ToLower() != "dtUserModified".ToLower() && column.Name.ToLower() != "dtUserCreated".ToLower())
                        {
                            column.FormatString = "{0: dd-MM-yyyy}";
                        }
                    }
                }
            }
        }

        private void gridVolontary_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            string saveLayout = "Save Layout";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(saveLayout) != null && resxSet.GetString(saveLayout) != "")
                    saveLayout = resxSet.GetString(saveLayout);
            }
            RadMenuItem customMenuItem = new RadMenuItem();
            customMenuItem.Text = saveLayout;
            customMenuItem.Click += SaveLayout;
            RadMenuSeparatorItem separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(customMenuItem);

            string addNewPerson = "New Person";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(addNewPerson) != null && resxSet.GetString(addNewPerson) != "")
                    addNewPerson = resxSet.GetString(addNewPerson);
            }
            RadMenuItem customMenuItem1 = new RadMenuItem();
            customMenuItem1.Text = addNewPerson;
            customMenuItem1.Click += AddNewPerson;
            RadMenuSeparatorItem separator1 = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator1);
            e.ContextMenu.Items.Add(customMenuItem1);
        }
        private void AddNewPerson(object sender, EventArgs e)
        {
            frmPerson fpn = new frmPerson();
            fpn.ShowDialog();
            gridTraveler.DataSource = null;
            LoadGridData();
        }
        private void SaveLayout(object sender, EventArgs e)
        {
            if (File.Exists(layoutLookup))
            {
                File.Delete(layoutLookup);
            }
            gridTraveler.SaveLayout(layoutLookup);
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("You have successfully save layout!") != null)
                    RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
                else
                    RadMessageBox.Show("You have successfully save layout!");
            }
        }

        private void gridVolontary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)  // namesteno da se i sa ENTER bira slog
            {
                GridViewRowInfo info = this.gridTraveler.CurrentRow;
                if (info.Index >= 0)
                {
                    //set selected row in model so you can use any data you'll need
                    selectedRow = (IModel)info.DataBoundItem;
                    this.DialogResult = DialogResult.Yes;
                    this.Close();

                }
            }

             if (e.KeyData == Keys.Escape)
             {
                 this.Close();
             }
        }

        private void chkBoxMedical_CheckStateChanged(object sender, EventArgs e)
        {
            pageLoaded = false;
            foreach (ListViewDataItem item in radListPref.Items)
            {
                if (chkCheckMedical.Checked == false)
                {
                    item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                    chkCheckMedical.Text = strCheckAll;
                }
                else
                {
                    item.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                    chkCheckMedical.Text = strUncheckAll;
                }
            }
            pageLoaded = true;
            LoadGridData();
        }

                
    }
}
