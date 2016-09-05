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
    public partial class GridLookupVoluntary : RadForm
    {
        List<IModel> personModelList;
        List<VolontaryFunctionModel> arrVoluntary1;
        List<VolontaryTripModel> arrVoluntary2;
        List<MedicalVoluntaryArrangementModel> arrVoluntary3;

        List<ArrangementFuncSkillsModel> functions;
        List<ArrangementFuncSkillsModel> skills;

        public List<int> selectedIDQuests = new List<int>();
        public List<int> selectedIDQuests_Trips = new List<int>();
        public List<int> selectedIDQuests_Skills = new List<int>();

        private int idArrangement = 0;
        private int idContPers = 0;

        public IModel selectedRow;
        string layoutLookup;

        private bool pageLoaded = false;

        private string strCheckAll = "Check All";
        private string strUncheckAll = "Uncheck All";

        public GridLookupVoluntary(int idArrangement, int idContPers, string nameForm)
        {
            InitializeComponent();

            gridVolontary.DataSource = null;
            gridVolontary.AllowAutoSizeColumns = true;
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
            layoutLookup = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_lookup\\grid_lookup_volontary.xml");


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

                for (int i = 0; i < pageVeiwFilters.Pages.Count; i++)
                {
                    if (resxSet.GetString(pageVeiwFilters.Pages[i].Text) != null)
                        pageVeiwFilters.Pages[i].Text = resxSet.GetString(pageVeiwFilters.Pages[i].Text);
                }
            }
            
            if (File.Exists(layoutLookup))
            {
                gridVolontary.LoadLayout(layoutLookup);
            }
        }

        private void pageVeiwFilters_SelectedPageChanged(object sender, EventArgs e)
        {

        }

        private void GridLookupVoluntary_Load(object sender, EventArgs e)
        {
            List<string> idQueryType = new List<string>();
            foreach(int i in MainForm.idLabelList)
            {
                idQueryType.Add(i.ToString());
            }
            Boolean isDefaultSort = true;
            Boolean isAll = true;

            ArrangementBookBUS bus = new ArrangementBookBUS();
            functions = new List<ArrangementFuncSkillsModel>();
            functions = bus.GetFunctionsForArrangement(idArrangement);

            skills = new List<ArrangementFuncSkillsModel>();
            skills = bus.GetSkillsForArrangement(idArrangement);

            VolontaryFunctionBUS vfb = new VolontaryFunctionBUS();
            arrVoluntary1 = vfb.GetVoluntaryArrangmentDetails(idQueryType, idArrangement, isDefaultSort, isAll);

            radListFunctions.DataSource = arrVoluntary1;
            radListFunctions.DataMember = "idQuest";
            radListFunctions.DisplayMember = "txtQuest";


            VolontaryTripBUS vtb = new VolontaryTripBUS();
            arrVoluntary2 = vtb.GetVoluntaryTripArrDetails(idQueryType, idArrangement, isDefaultSort, isAll);

            radListTrips.DataSource = arrVoluntary2;
            radListTrips.DataMember = "idQuest";
            radListTrips.DisplayMember = "txtQuest";

            MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
            arrVoluntary3 = mvb.GetVoluntaryForArrangement(idQueryType, idArrangement, isDefaultSort, isAll);

            radListSkills.DataSource = arrVoluntary3;
            radListSkills.DataMember = "idQuest";
            radListSkills.DisplayMember = "txtQuest";
            
            LoadGridData();

            pageLoaded = true;

            

            pageVeiwFilters.SelectedPage = tabFunction;
            
        }

        private void LoadGridData()
        {
            List<int> selectedIDAns = new List<int>();
            selectedIDQuests = new List<int>();

            List<int> selectedIDAns_Trips = new List<int>();
            selectedIDQuests_Trips = new List<int>();

            List<int> selectedIDAns_Skills = new List<int>();
            selectedIDQuests_Skills = new List<int>();

            if (radListFunctions.Items.Count > 0)
            {
                foreach (ListViewDataItem item in radListFunctions.Items)
                {
                    if (item.DataBoundItem != null)
                    {
                        if (item.CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
                        {
                            VolontaryFunctionModel m = (VolontaryFunctionModel)item.DataBoundItem;
                            selectedIDAns.Add((int)m.idAns);
                            selectedIDQuests.Add(m.idQuest);
                        }
                    }
                }
            }
            foreach (ListViewDataItem item in radListTrips.Items)
            {
                if (item.DataBoundItem != null)
                {
                    if (item.CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
                    {
                        VolontaryTripModel m = (VolontaryTripModel)item.DataBoundItem;
                        selectedIDAns_Trips.Add((int)m.idAns);
                        selectedIDQuests_Trips.Add(m.idQuest);
                    }
                }
            }

            foreach (ListViewDataItem item in radListSkills.Items)
            {
                if (item.DataBoundItem != null)
                {
                    if (item.CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
                    {
                        MedicalVoluntaryArrangementModel m = (MedicalVoluntaryArrangementModel)item.DataBoundItem;
                        selectedIDAns_Skills.Add((int)m.idAns);
                        selectedIDQuests_Skills.Add(m.idQuest);
                    }
                }
            }

            PersonBUS pbus = new PersonBUS();
            personModelList = pbus.GetVHPersons(idArrangement, idContPers, Login._user.lngUser, selectedIDAns, selectedIDQuests,selectedIDAns_Trips,selectedIDQuests_Trips, selectedIDAns_Skills, selectedIDQuests_Skills);
            gridVolontary.DataSource = personModelList;
        }
        private void radListFunctions_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if(e.Item.DataBoundItem != null && functions != null)
            {
                VolontaryFunctionModel model = (VolontaryFunctionModel)e.Item.DataBoundItem;                
                
                var result = functions.Find(s => s.ID == model.idQuest);
                if(result != null)
                {
                    int available = result.Required - result.Booked;
                    if (available <= 0)
                    {
                        e.Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        e.Item.Enabled = false;
                    }
                    else
                        e.Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                }
                else
                    e.Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
            }
            else
                e.Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
            
            //}
        }

        private void radListFunctions_ItemCheckedChanged(object sender, ListViewItemEventArgs e)
        {
            if (pageLoaded == true)
            {
                if (e.Item.DataBoundItem != null)
                {
                    //VolontaryFunctionModel model = (VolontaryFunctionModel)e.Item.DataBoundItem;
                    //MessageBox.Show(model.txtQuest);
                    LoadGridData();
                }
            }
        }

        private void radListTrips_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            e.Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;

            //VolontaryFunctionModel m = (VolontaryFunctionModel) e.Item.DataBoundItem;
            //m.idQuest

        }

        private void radListTrips_ItemCheckedChanged(object sender, ListViewItemEventArgs e)
        {
            if (pageLoaded == true)
            {
                if (e.Item.DataBoundItem != null)
                {
                    //VolontaryTripModel model = (VolontaryTripModel)e.Item.DataBoundItem;
                    //MessageBox.Show(model.txtQuest);
                    LoadGridData();
                }
            }
        }
        private void radListSkills_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if(e.Item.DataBoundItem != null && skills != null)
            {
                MedicalVoluntaryArrangementModel model = (MedicalVoluntaryArrangementModel)e.Item.DataBoundItem;                                
                var result = skills.Find(s => s.ID == model.idQuest);

                if(result != null)
                {
                    int available = result.Required - result.Booked;
                    if (available <= 0)
                    {
                        e.Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                        e.Item.Enabled = false;
                    }
                    else
                        e.Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                }
                else
                    e.Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
            }
            else
                e.Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
            
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

        private void gridVolontary_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.gridVolontary.CurrentRow;
            if (e.RowIndex >= 0)
            {
                VolAvailabilityBUS bus = new VolAvailabilityBUS();

                PersonModel m = (PersonModel)e.Row.DataBoundItem;

                object objSignedup = bus.GetSignedupNrTimesForPeriod(idArrangement, m.idContPers);
                object objFinished = bus.GetFinishedNrTimesForPeriod(idArrangement, m.idContPers);

                int intSignedup = 0;
                int intFinished = 0;

                if(objSignedup != null && objSignedup != DBNull.Value)
                    intSignedup = Convert.ToInt32(objSignedup);
                if (objFinished != null && objFinished != DBNull.Value)
                    intFinished = Convert.ToInt32(objFinished);

                if (intFinished >= intSignedup)
                {
                    translateRadMessageBox msgbox = new translateRadMessageBox();
                    msgbox.translateAllMessageBox("Volonteer is no longer available for this period.");
                    return;
                }
                
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
        }
        private void SaveLayout(object sender, EventArgs e)
        {
            if (File.Exists(layoutLookup))
            {
                File.Delete(layoutLookup);
            }
            gridVolontary.SaveLayout(layoutLookup);
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
                GridViewRowInfo info = this.gridVolontary.CurrentRow;
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

        private void chkCheckFunctions_CheckStateChanged(object sender, EventArgs e)
        {            
            pageLoaded = false;
            foreach (ListViewDataItem item in radListFunctions.Items)
            {
                if (chkCheckFunctions.Checked == false)
                {
                    item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                    chkCheckFunctions.Text = strCheckAll;
                }
                else
                {
                    if(item.Enabled == true)
                        item.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;

                    chkCheckFunctions.Text = strUncheckAll;
                }
            }
            pageLoaded = true;
            LoadGridData();

        }

        private void chkBoxTrips_CheckStateChanged(object sender, EventArgs e)
        {
            pageLoaded = false;
            foreach (ListViewDataItem item in radListTrips.Items)
            {
                if (chkCheckTrips.Checked == false)
                {
                    item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                    chkCheckTrips.Text = strCheckAll;
                }
                else
                {
                    item.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;
                    chkCheckTrips.Text = strUncheckAll;
                }
            }
            pageLoaded = true;
            LoadGridData();
        }

        private void chkCheckSkills_CheckStateChanged(object sender, EventArgs e)
        {
            pageLoaded = false;
            foreach (ListViewDataItem item in radListSkills.Items)
            {
                if (chkCheckSkills.Checked == false)
                {
                    item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
                    chkCheckSkills.Text = strCheckAll;
                }
                else
                {
                    if(item.Enabled == true)
                        item.CheckState = Telerik.WinControls.Enumerations.ToggleState.On;

                    chkCheckSkills.Text = strUncheckAll;
                }
            }
            pageLoaded = true;
            LoadGridData();
        }
                
    }
}
