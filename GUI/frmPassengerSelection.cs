using BIS.Business;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Resources;
//using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Outlook = Microsoft.Office.Interop.Outlook;


namespace GUI
{
    public partial class frmPassengerSelection : Telerik.WinControls.UI.RadForm
    {
        public ArrangementModel arrange;
        string layoutLookup;
        public int iID = -1;
        ArrangementModel model = new ArrangementModel();
        List<ArrangementThemeTripModel> arrangeThemeTrip = new List<ArrangementThemeTripModel>();
        List<ArrangementBoardingPointModel> arrangeBoardingPoint = new List<ArrangementBoardingPointModel>();
        List<ArrangementTargetGroupModel> arrangeTargetGroup = new List<ArrangementTargetGroupModel>();
        List<IModel> personModelList;
        List<MedicalVoluntaryModel> medical;
        List<RadRadioButton> status = new List<RadRadioButton>();
        List<RadRadioButton> gender = new List<RadRadioButton>();
        List<RadRadioButton> travelPapers = new List<RadRadioButton>();
        string statusName = "";
        string genderName = "";
        string travelPapersName = "";
        int idStatus = -1;
        int idGender = -1;
        int idTravelPapers = -1;
        private bool pageLoaded = false;
        List<VolontaryFunctionModel> arrVoluntary1;
        List<VolontaryTripModel> arrVoluntary2;
        List<MedicalVoluntaryArrangementModel> arrVoluntary3;
        string activTab = "T";// za Traveler odnosno "V" za Volontary 

        public int selectTab = 1; // promenljiva za selekciju taba
          List<PersonModel> persons;

        UsersBUS ubus = new UsersBUS();
        List<TranslateUstrModel> translate;


        public frmPassengerSelection()
        {
            InitializeComponent();
            this.Icon = Login.iconForm;
            setTranslation();

        }
        public frmPassengerSelection(ArrangementModel model)
        {
            arrange = model;
            iID = arrange.idArrangement;
            iID = model.idArrangement;
            InitializeComponent();
            this.Icon = Login.iconForm;
            setTranslation();

        }
        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblName.Text) != null)
                    lblName.Text = resxSet.GetString(lblName.Text);

                if (resxSet.GetString(lblDateFrom.Text) != null)
                    lblDateFrom.Text = resxSet.GetString(lblDateFrom.Text);

                if (resxSet.GetString(lblDaysArrangement.Text) != null)
                    lblDaysArrangement.Text = resxSet.GetString(lblDaysArrangement.Text);

                if (resxSet.GetString(lblMedicalQuestion.Text) != null)
                    lblMedicalQuestion.Text = resxSet.GetString(lblMedicalQuestion.Text);

                if (resxSet.GetString(btnPrintR.Text) != null)
                    btnPrintR.Text = resxSet.GetString(btnPrintR.Text);

                if (resxSet.GetString(btnPreviewR.Text) != null)
                    btnPreviewR.Text = resxSet.GetString(btnPreviewR.Text);

                if (resxSet.GetString(btnLeterR.Text) != null)
                    btnLeterR.Text = resxSet.GetString(btnLeterR.Text);

                if (resxSet.GetString(btnEmailR.Text) != null)
                    btnEmailR.Text = resxSet.GetString(btnEmailR.Text);


                if (resxSet.GetString(lblSkils.Text) != null)
                    lblSkils.Text = resxSet.GetString(lblSkils.Text);
                if (resxSet.GetString(lblFunction.Text) != null)
                    lblFunction.Text = resxSet.GetString(lblFunction.Text);
                if (resxSet.GetString(lblTipPreferences.Text) != null)
                    lblTipPreferences.Text = resxSet.GetString(lblTipPreferences.Text);
                //translate all tabs - Selection i result
                for (int i = 0; i < pageReports.Pages.Count; i++)
                {
                    if (resxSet.GetString(pageReports.Pages[i].Text) != null)
                        pageReports.Pages[i].Text = resxSet.GetString(pageReports.Pages[i].Text);
                }
                // translate tabova traveler i volontary
                for (int i = 0; i < radPageSelection.Pages.Count; i++)
                {
                    if (resxSet.GetString(radPageSelection.Pages[i].Text) != null)
                        radPageSelection.Pages[i].Text = resxSet.GetString(radPageSelection.Pages[i].Text);
                }
            }

        }

        protected void ReplaceBookmarkText(Microsoft.Office.Interop.Word.Document doc, BIS.Model.BookmarkSpecModel oBooks)
        {
            if (doc.Bookmarks.Exists(oBooks.field))
            {
                Object name = oBooks.field;

                Microsoft.Office.Interop.Word.Range range = doc.Bookmarks.get_Item(ref name).Range;

                range.Text = oBooks.value;
                object newRange = range;
                doc.Bookmarks.Add(oBooks.field, ref newRange);
            }
        }
        protected void ReplaceBookmarkText(Microsoft.Office.Interop.Word.Document doc, string bookmarkName, string text)
        {
            if (doc.Bookmarks.Exists(bookmarkName))
            {
                Object name = bookmarkName;

                Microsoft.Office.Interop.Word.Range range = doc.Bookmarks.get_Item(ref name).Range;

                range.Text = text;
                object newRange = range;
                doc.Bookmarks.Add(bookmarkName, ref newRange);
            }
        }
        protected void DeleteEmptyBookmarks(Microsoft.Office.Interop.Word.Document doc, List<BIS.Model.BookmarkSpecModel> existingBookmarks)
        {
            foreach (Microsoft.Office.Interop.Word.Bookmark b in doc.Bookmarks)
            {
                //MessageBox.Show(b.Name + " - " + b.Range);
                bool exist = existingBookmarks.Exists(x => x.field == b.Name);

                if (exist == false)
                {
                    object bmkname = b.Name;
                    Microsoft.Office.Interop.Word.Range deleteRange = doc.Range();

                    deleteRange.Start = doc.Bookmarks.get_Item(ref bmkname).Range.Start;
                    deleteRange.End = doc.Bookmarks.get_Item(ref bmkname).Range.End + 1;
                    //MessageBox.Show(deleteRange.Text);
                    deleteRange.Delete();
                }
            }

            Microsoft.Office.Interop.Word.Paragraphs paragraphs = doc.Paragraphs;

            foreach (Microsoft.Office.Interop.Word.Paragraph paragraph in paragraphs)
            {

                if (paragraph.Range.Text.Trim() == string.Empty)
                {
                    //izbrisi prazne redove
                    //  paragraph.Range.Delete();
                }
                else
                {
                    // sko ima space na pocetku reda izbrisi ga
                    if (paragraph.Range.Characters[1].Text == " ")
                    {
                        paragraph.Range.Characters[1].Delete();
                    }
                }
            }

        }

        protected string CreateDocName(Int32 iID)
        {
            //return iID.ToString("00000000") + DateTime.Now.ToString("yyyyMMdd") + (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second).ToString("00000");
            return iID.ToString() + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + (DateTime.Now.Hour * 3600 + DateTime.Now.Minute * 60 + DateTime.Now.Second).ToString();
        }

        private void btnArrangement_Click(object sender, EventArgs e)
        {
            ArrangementBUS boardingBUS = new ArrangementBUS();
            List<IModel> gm3 = new List<IModel>();


            gm3 = boardingBUS.GetAllArrangements();


            var dlgSave = new GridLookupForm(gm3, "Arrangement");


            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                iID = -1;
                Cursor.Current = Cursors.WaitCursor;
                ArrangementModel genm3 = new ArrangementModel();
                genm3 = (ArrangementModel)dlgSave.selectedRow;
                model.idArrangement = genm3.idArrangement;
                txtName.Text = genm3.nameArrangement;


                txtName.Text = genm3.nameArrangement.ToString();
                string dateFrom = (genm3.dtToArrangement.Year + "-" + genm3.dtToArrangement.Month + "-" + genm3.dtToArrangement.Day);
                lblDateFromm.Text = dateFrom;
                string dateTo = (genm3.dtFromArrangement.Year + "-" + genm3.dtFromArrangement.Month + "-" + genm3.dtFromArrangement.Day);
                lblDateToo.Text = dateTo;

                DateTime dt1 = new DateTime(genm3.dtToArrangement.Year, genm3.dtToArrangement.Month, genm3.dtToArrangement.Day);
                DateTime dt2 = new DateTime(genm3.dtFromArrangement.Year, genm3.dtFromArrangement.Month, genm3.dtFromArrangement.Day);
                int days = Convert.ToInt32((dt1 - dt2).TotalDays) + 1;
                lblDays.Text = days.ToString();
                model = (ArrangementModel)dlgSave.selectedRow;
                getCheckedBoardingPoint();


                getCheckedMedQuestion();
                fillListStatus();

                fillPanelTravelPapers();
                fillPanelGender();
                GridLookupVoluntary_Load();

                Cursor.Current = Cursors.Default;
            }
        }

        // Tab Traveler
        private void getCheckedBoardingPoint()
        {
            if (iID > -1)
                model = arrange;
            ArrangementBoardingPointBUS abpb = new ArrangementBoardingPointBUS();
            List<BoardingPointModel> arrBoardingPoint = new List<BoardingPointModel>();
            arrBoardingPoint = abpb.GetArrangementBoardingPoint(model.idArrangement);



            radListViewBoardingPoint.DataSource = arrBoardingPoint;
            radListViewBoardingPoint.DataMember = "idBoardingPoint";
            radListViewBoardingPoint.DisplayMember = "nameBoardingPoint";


        }
        private void radListViewBoardingPoint_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            e.Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
        }

        private void getCheckedMedQuestion()
        {
            List<string> idQueryType = new List<string>();
            foreach (int i in MainForm.idLabelList)
            {
                idQueryType.Add(i.ToString());
            }

            MedicalVoluntaryBUS vtb = new MedicalVoluntaryBUS();
            medical = vtb.GetMedicalForBooking(idQueryType);

            radListMedQuestion.DataSource = medical;
            radListMedQuestion.DataMember = "idQuest";
            radListMedQuestion.DisplayMember = "txtQuest";


        }

        private void LoadGridData()
        {
            List<string> selectedIDAns = new List<string>();
            List<string> selectedIDQuests = new List<string>();

            List<int> selectedIDAns_Medical = new List<int>();
            List<int> selectedIDQuests_Medical = new List<int>();
            List<int> selectedIDQ_BPoint = new List<int>();



            foreach (ListViewDataItem item in radListViewBoardingPoint.Items)
            {
                if (item.DataBoundItem != null)
                {
                    if (item.CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
                    {
                        BoardingPointModel m = (BoardingPointModel)item.DataBoundItem;
                        selectedIDQ_BPoint.Add((int)m.idBoardingPoint);

                    }
                }
            }
            foreach (ListViewDataItem item in radListMedQuestion.Items)
            {
                if (item.DataBoundItem != null)
                {
                    if (item.CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
                    {
                        MedicalVoluntaryModel m = (MedicalVoluntaryModel)item.DataBoundItem;
                        selectedIDAns_Medical.Add((int)m.idAns);
                        selectedIDQuests_Medical.Add(m.idQuest);
                    }
                }
            }
            PersonBUS pbus = new PersonBUS();
            int idContP = 0;
            int idArrangement = model.idArrangement;
            personModelList = pbus.GetTravelersReport(idArrangement, idContP, Login._user.lngUser, idGender, idStatus, idTravelPapers, selectedIDAns_Medical, selectedIDQuests_Medical);
            rgvResult.DataSource = null;
            rgvResult.DataSource = personModelList;

            for (int i = 0; i < rgvResult.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvResult.Columns[i].HeaderText != null && rgvResult.Columns[i].HeaderText != "" && resxSet.GetString(rgvResult.Columns[i].HeaderText) != null)
                    {
                        rgvResult.Columns[i].HeaderText = resxSet.GetString(rgvResult.Columns[i].HeaderText);
                    }

                }

            }

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
        private void radListTrips_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            e.Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
        }

        private void fillListStatus()
        {
            ArrangementBookStatusBUS arrBookStatus = new ArrangementBookStatusBUS();
            List<ArrangementBookStatusModel> arrBookStatusModel = new List<ArrangementBookStatusModel>();
            arrBookStatusModel = arrBookStatus.GetAllStatus(Login._user.lngUser);


            if (arrBookStatusModel != null)
            {
                if (arrBookStatusModel.Count > 0)
                {
                    int Y = 2;

                    for (int i = 0; i < arrBookStatusModel.Count; i++)
                    {
                        RadRadioButton rbn = new RadRadioButton();
                        rbn.Font = new Font("Verdana", 9);
                        //   rbn.ThemeName = panelInformation.ThemeName;
                        rbn.BackColor = Color.Transparent;
                        rbn.Name = "rbnStatus" + arrBookStatusModel[i].idStatus.ToString();
                        rbn.Text = arrBookStatusModel[i].nameStatus;
                        rbn.CheckStateChanged += rbnStatus_CheckStateChanged;
                        rbn.Location = new Point(2, Y);
                        rbn.AutoSize = true;
                        Y = Y + 3 + rbn.Height;
                        radListStatus.Controls.Add(rbn);
                        status.Add(rbn);

                    }
                }
            }
        }
        private void fillPanelTravelPapers()
        {
            ArrangementBookTravelPapersBUS arrBookTravelPapers = new ArrangementBookTravelPapersBUS();
            List<ArrangementBookTravelPapersModel> arrBookTravelPapersModel = new List<ArrangementBookTravelPapersModel>();
            arrBookTravelPapersModel = arrBookTravelPapers.GetAllTravelPapers(Login._user.lngUser);
            if (arrBookTravelPapersModel != null)
            {
                if (arrBookTravelPapersModel.Count > 0)
                {
                    int Y = 2;

                    for (int i = 0; i < arrBookTravelPapersModel.Count; i++)
                    {
                        RadRadioButton rbn = new RadRadioButton();
                        rbn.Font = new Font("Verdana", 9);
                        rbn.BackColor = Color.Transparent;
                        rbn.Name = "rbnTravelPapers" + arrBookTravelPapersModel[i].idTravelPapers.ToString();
                        rbn.Text = arrBookTravelPapersModel[i].nameTravelPapers;
                        rbn.CheckStateChanged += rbnTravelPapers_CheckStateChanged;
                        rbn.Location = new Point(2, Y);
                        rbn.AutoSize = true;
                        Y = Y + 3 + rbn.Height;
                        radListTravelPapers.Controls.Add(rbn);
                        travelPapers.Add(rbn);
                    }
                }
            }
        }
        private void fillPanelGender()
        {
            GenderBUS arrBookTravelPapers = new GenderBUS();
            List<GenderModel> genderModel = new List<GenderModel>();
            genderModel = arrBookTravelPapers.GetAllGenders(Login._user.lngUser);
            if (genderModel != null)
            {
                if (genderModel.Count > 0)
                {
                    int Y = 2;

                    for (int i = 0; i < genderModel.Count; i++)
                    {
                        RadRadioButton rbn = new RadRadioButton();
                        rbn.Font = new Font("Verdana", 9);
                        rbn.BackColor = Color.Transparent;
                        rbn.Name = "rbnGenders" + genderModel[i].idGender.ToString();
                        rbn.Text = genderModel[i].nameGender;
                        rbn.CheckStateChanged += rbnGenders_CheckStateChanged;
                        rbn.Location = new Point(2, Y);
                        rbn.AutoSize = true;
                        Y = Y + 3 + rbn.Height;
                        listGender.Controls.Add(rbn);
                        gender.Add(rbn);
                    }
                }
            }
        }


        private void rbnStatus_CheckStateChanged(object sender, EventArgs e)
        {
            ArrangementBookStatusBUS arrBookStatus = new ArrangementBookStatusBUS();
            List<ArrangementBookStatusModel> arrBookStatusModel = new List<ArrangementBookStatusModel>();
            arrBookStatusModel = arrBookStatus.GetAllStatus(Login._user.lngUser);

            PersonBUS p = new PersonBUS();
            for (int i = 0; i < status.Count; i++)
            {

                if (status[i].CheckState == CheckState.Checked)
                {
                    statusName = status[i].Text + arrBookStatusModel[i].nameStatus;
                    idStatus = arrBookStatusModel[i].idStatus;
                }
            }

        }
        private void rbnGenders_CheckStateChanged(object sender, EventArgs e)
        {
            PersonBUS p = new PersonBUS();
            GenderBUS arrBookTravelPapers = new GenderBUS();
            List<GenderModel> genderModel = new List<GenderModel>();
            genderModel = arrBookTravelPapers.GetAllGenders(Login._user.lngUser);
            for (int i = 0; i < gender.Count; i++)
            {
                if (gender[i].CheckState == CheckState.Checked)
                {
                    genderName = gender[i].Text;
                    idGender = genderModel[i].idGender;
                }
            }

        }
        private void rbnTravelPapers_CheckStateChanged(object sender, EventArgs e)
        {
            ArrangementBookTravelPapersBUS arrBookTravelPapers = new ArrangementBookTravelPapersBUS();
            List<ArrangementBookTravelPapersModel> arrBookTravelPapersModel = new List<ArrangementBookTravelPapersModel>();
            arrBookTravelPapersModel = arrBookTravelPapers.GetAllTravelPapers(Login._user.lngUser);
            PersonBUS p = new PersonBUS();
            for (int i = 0; i < travelPapers.Count; i++)
            {

                if (travelPapers[i].CheckState == CheckState.Checked)
                {
                    travelPapersName = travelPapers[i].Text + arrBookTravelPapersModel[i].nameTravelPapers;
                    idTravelPapers = arrBookTravelPapersModel[i].idTravelPapers;
                }
            }

        }

        private void pageReports_SelectedPageChanged(object sender, EventArgs e)
        {
            RadPageView rpv = (RadPageView)sender;
            string sName = ((RadPageView)sender).SelectedPage.Name;
            if (sName == "tabResult")
            {
                if (activTab == "V")
                {
                    // tabV = 0; // dodato
                    LoadGridDataVoluntary();

                }
                else
                {
                    //tabV = 1;// dodato
                    LoadGridData();
                }
            }
        }
        private void radPageSelection_SelectedPageChanged(object sender, EventArgs e)
        {
            RadPageView rpv = (RadPageView)sender;
            string sName = ((RadPageView)sender).SelectedPage.Name;
            if (sName == "tabVolontary")
            {
                activTab = "V";
                selectTab = 0;
                //  GridLookupVoluntary_Load();
            }
            if (sName == "tabTraveler")
            {
                activTab = "T";
                selectTab = 1;
            }
        }

        ////  TAB Volontary

        private void GridLookupVoluntary_Load()
        {
            if (iID > -1)
            {
                model = arrange;
            }
            List<string> idQueryType = new List<string>();
            foreach (int i in MainForm.idLabelList)
            {
                idQueryType.Add(i.ToString());
            }
            Boolean isDefaultSort = true;
            Boolean isAll = true;

            VolontaryFunctionBUS vfb = new VolontaryFunctionBUS();
            arrVoluntary1 = vfb.GetVoluntaryArrangmentDetails(idQueryType, model.idArrangement, isDefaultSort, isAll);

            radListFunction.DataSource = arrVoluntary1;
            radListFunction.DataMember = "idQuest";
            radListFunction.DisplayMember = "txtQuest";

            //int ii= arrange.idArrangement;
            VolontaryTripBUS vtb = new VolontaryTripBUS();
            arrVoluntary2 = vtb.GetVoluntaryTripArrDetails(idQueryType, model.idArrangement, isDefaultSort, isAll);

            radListPreferences.DataSource = arrVoluntary2;
            radListPreferences.DataMember = "idQuest";
            radListPreferences.DisplayMember = "txtQuest";

            MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
            arrVoluntary3 = mvb.GetVoluntaryForArrangement(idQueryType, model.idArrangement, isDefaultSort, isAll);

            radListSkils.DataSource = arrVoluntary3;
            radListSkils.DataMember = "idQuest";
            radListSkils.DisplayMember = "txtQuest";

        }

        private void radListFunction_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            e.Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
        }

        private void radListPreferences_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            e.Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
        }

        private void radListSkils_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            e.Item.CheckState = Telerik.WinControls.Enumerations.ToggleState.Off;
        }

        private void LoadGridDataVoluntary()
        {
            List<int> selectedIDAns = new List<int>();
            List<int> selectedIDQuests = new List<int>();

            List<int> selectedIDAns_Trips = new List<int>();
            List<int> selectedIDQuests_Trips = new List<int>();

            List<int> selectedIDAns_Skills = new List<int>();
            List<int> selectedIDQuests_Skills = new List<int>();

            if (radListFunction.Items.Count > 0)
            {
                foreach (ListViewDataItem item in radListFunction.Items)
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
            foreach (ListViewDataItem item in radListPreferences.Items)
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

            foreach (ListViewDataItem item in radListSkils.Items)
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
            int idContPers = 0;
            personModelList = pbus.GetVHPersons(model.idArrangement, idContPers, Login._user.lngUser, selectedIDAns, selectedIDQuests, selectedIDAns_Trips, selectedIDQuests_Trips, selectedIDAns_Skills, selectedIDQuests_Skills);
            rgvResult.DataSource = null;
            rgvResult.DataSource = personModelList;
            for (int i = 0; i < rgvResult.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (rgvResult.Columns[i].HeaderText != null && rgvResult.Columns[i].HeaderText != "" && resxSet.GetString(rgvResult.Columns[i].HeaderText) != null)
                    {
                        rgvResult.Columns[i].HeaderText = resxSet.GetString(rgvResult.Columns[i].HeaderText);
                    }

                }

            }
        }

        private void listGender_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void frmPassengerSelection_Load(object sender, EventArgs e)
        {
            if (iID != -1)
            {
                model = arrange;
                Cursor.Current = Cursors.WaitCursor;
                txtName.Text = model.nameArrangement.ToString();
                string dateFrom = (model.dtToArrangement.Year + "-" + model.dtToArrangement.Month + "-" + model.dtToArrangement.Day);
                lblDateFromm.Text = dateFrom;
                string dateTo = (model.dtFromArrangement.Year + "-" + model.dtFromArrangement.Month + "-" + model.dtFromArrangement.Day);
                lblDateToo.Text = dateTo;

                DateTime dt1 = new DateTime(model.dtToArrangement.Year, model.dtToArrangement.Month, model.dtToArrangement.Day);
                DateTime dt2 = new DateTime(model.dtFromArrangement.Year, model.dtFromArrangement.Month, model.dtFromArrangement.Day);
                int days = Convert.ToInt32((dt1 - dt2).TotalDays) + 1;
                lblDays.Text = days.ToString();
                getCheckedBoardingPoint();


                getCheckedMedQuestion();
                fillListStatus();

                fillPanelTravelPapers();
                fillPanelGender();
                GridLookupVoluntary_Load();

                Cursor.Current = Cursors.Default;

            }
            layoutLookup = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_lookup\\PassengerSelection.xml");
            if (File.Exists(layoutLookup))
            {
                rgvResult.LoadLayout(layoutLookup);
            }
        }

        private void radMenuItemSaveLookupLayout_Click(object sender, EventArgs e)
        {

            if (File.Exists(layoutLookup))
            {
                File.Delete(layoutLookup);
            }
            rgvResult.SaveLayout(layoutLookup);
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("You have successfully save layout!") != null)
                    RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
                else
                    RadMessageBox.Show("You have successfully save layout!");
            }

        }



        private void btnPreviewR_Click(object sender, EventArgs e)
        {
            if (selectTab == 1)
            {
                // svi parametri koji se prosledjuju na reportView kontrolu moraju biti dodati iz metode GetTravelersReport iz LoadGridData 
                List<int> selectedIDAns = new List<int>();
                List<int> selectedIDQuests = new List<int>();

                List<int> selectedIDAns_Medical = new List<int>();
                List<int> selectedIDQuests_Medical = new List<int>();


                foreach (ListViewDataItem item in radListMedQuestion.Items)
                {
                    if (item.DataBoundItem != null)
                    {
                        if (item.CheckState == Telerik.WinControls.Enumerations.ToggleState.On)
                        {
                            MedicalVoluntaryModel m = (MedicalVoluntaryModel)item.DataBoundItem;
                            selectedIDAns_Medical.Add((int)m.idAns);
                            selectedIDQuests_Medical.Add(m.idQuest);
                        }
                    }
                }

                int idContP = 0;
                int idArrangement = model.idArrangement;
                //konstruktor za traveler
                frmReport1 frm = new frmReport1(idArrangement, idContP, idGender, idStatus, idTravelPapers, selectedIDAns_Medical, selectedIDQuests_Medical);
                frm.Show();
            }
            else
            {
                //Parametri za volontere
                int idContPers = 0;
                List<int> selectedIDAns_Trips = new List<int>();
                List<int> selectedIDQuests_Trips = new List<int>();
                List<int> selectedIDAns = new List<int>();
                List<int> selectedIDQuests = new List<int>();

                List<int> selectedIDAns_Skills = new List<int>();
                List<int> selectedIDQuests_Skills = new List<int>();
                int idArrangement = model.idArrangement;

                foreach (ListViewDataItem item in radListSkils.Items)
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

                // konstruktor za volontera
                frmReport1 frm1 = new frmReport1(idArrangement, idContPers, selectedIDAns, selectedIDQuests, selectedIDAns_Trips, selectedIDQuests_Trips, selectedIDAns_Skills, selectedIDQuests_Skills);
                frm1.Show();
            }
        }


        private void btnEmailR_Click(object sender, EventArgs e)
        {

            if (Login.isOutlookInstalled == true)
            {
                if (personModelList != null)
                {
                    List<PersonEmailModel> mailToList = new List<PersonEmailModel>();

                    PersonEmailBUS emailbus = new PersonEmailBUS();
                    persons = new List<PersonModel>();
                    foreach (IModel m in personModelList)
                    {
                        if (m != null)
                        {
                            PersonModel p = (PersonModel)m;
                            persons.Add(p);

                            List<PersonEmailModel> personmails = new List<PersonEmailModel>();
                            personmails = emailbus.GetPersonEmailsIsCommunication(p.idContPers);
                            if (personmails != null)
                            {
                                foreach (PersonEmailModel em in personmails)
                                {
                                    mailToList.Add(em);
                                }
                            }
                        }
                    }

                    try
                    {
                        if (mailToList.Count > 0)
                        {
                            Microsoft.Office.Interop.Outlook.Application outlookApp = new Microsoft.Office.Interop.Outlook.Application();
                            outlookApp.ItemSend += new Microsoft.Office.Interop.Outlook.ApplicationEvents_11_ItemSendEventHandler(outlookApp_ItemSend);

                            Microsoft.Office.Interop.Outlook._MailItem oMailItem = (Microsoft.Office.Interop.Outlook._MailItem)outlookApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
                            Microsoft.Office.Interop.Outlook.Inspector oInspector = oMailItem.GetInspector;
                            oMailItem.DeleteAfterSubmit = false;

                            // Recipient
                            Microsoft.Office.Interop.Outlook.Recipients oRecips = (Microsoft.Office.Interop.Outlook.Recipients)oMailItem.Recipients;
                            foreach (PersonEmailModel recipient in mailToList)
                            {
                                Microsoft.Office.Interop.Outlook.Recipient oRecip = (Microsoft.Office.Interop.Outlook.Recipient)oRecips.Add(recipient.email);
                                oRecip.Resolve();
                            }
                            //Add CC
                            // Outlook.Recipient oCCRecip = oRecips.Add("THIYAGARAJAN.DURAIRAJAN@testmail.com");
                            //oCCRecip.Type = (int)Outlook.OlMailRecipientType.olCC;
                            //oCCRecip.Resolve();

                            //Add Subject                                       
                          //  oMailItem.Subject = arrange.nameArrangement + " - " + DateTime.Now.ToShortDateString();
                            oMailItem.Subject = arrange.nameArrangement + " " + arrange.dtFromArrangement.ToString("dd-MM-yyyy");
                            oMailItem.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatRichText;
                            oMailItem.Body = "Beste Reiziger, \r\n";

                           Outlook.Folder outlookfolder = Login.GetOutlookBisFolder();
                            if (outlookfolder != null)
                                oMailItem.SaveSentMessageFolder = outlookfolder;
                          //  oMailItem.SaveSentMessageFolder = Login.sentFolder;

                            //generate report and add as attachment
                            DataSet dataSet = new DataSet();
                            DataTable dataTable = new DataTable();

                            PersonBUS personBUS = new PersonBUS();

                            // dataTable = personBUS.GetTravelersReportDataTable(arrange.idArrangement, idContPR, Login._user.lngUser, idGenderR, idStatusR, idTravelPapersR, selectedAns_MedicalR, selectedQuests_MedicalR);

                            //Display the mailbox
                            oMailItem.Display(true);
                        }
                        else
                        {
                            translateRadMessageBox msgbox = new translateRadMessageBox();
                            msgbox.translateAllMessageBox("Theres no valid email address.");
                        }

                    }
                    catch (Exception objEx)
                    {
                        RadMessageBox.Show(objEx.ToString());
                    }
                }

            }
            else
            {
                translateRadMessageBox msgbox = new translateRadMessageBox();
                msgbox.translateAllMessageBox("Cannot find Outlook.");
            }
        }
        void outlookApp_ItemSend(object Item, ref bool Cancel)
        {
            if (Item is Microsoft.Office.Interop.Outlook.MailItem)
            {
                Microsoft.Office.Interop.Outlook.MailItem item = (Microsoft.Office.Interop.Outlook.MailItem)Item;
                item.Save();

                DocumentsBUS sbus = new DocumentsBUS();
                PersonEmailBUS emailbus = new PersonEmailBUS();


                string locationOnDisk = MainForm.myEmailFolder + "\\" + item.EntryID + ".msg";

                if (!File.Exists(locationOnDisk))
                    item.SaveAs(locationOnDisk, Microsoft.Office.Interop.Outlook.OlSaveAsType.olMSG);

                if (persons != null)
                {
                    foreach (PersonModel m in persons)
                    {
                        // if person has defined email that isCommunitacion it will save him
                        List<PersonEmailModel> personmails = new List<PersonEmailModel>();
                        personmails = emailbus.GetPersonEmailsIsCommunication(m.idContPers);
                        if (personmails != null && personmails.Count > 0)
                        {
                            DocumentsModel model = new DocumentsModel();
                            model.idContPers = m.idContPers;
                            model.idClient = 0;
                            model.descriptionDocument = "Email";
                            model.fileDocument = item.EntryID + ".msg";
                            model.typeDocument = "EML";
                            model.idDocumentStatus = 2;
                            model.idEmployee = 0;
                            model.idResponsableEmployee = 0;
                            model.inOutDocument = 0;
                            model.noteDocument = "Sent Email";
                            model.idArrangement = arrange.idArrangement;
                            //model.id

                            model.dtCreated = DateTime.Now;
                            model.dtModified = DateTime.Now;
                            model.userCreated = Login._user.idUser;
                            model.userModified = Login._user.idUser;

                            sbus.Save(model, this.Name, Login._user.idUser);

                        }
                    }
                }

                Cancel = false;

            }
        }

        void ThisAddIn_Close(ref bool Cancel)
        {
            //MessageBox.Show("MailItem is closed");
        }
        
    }
}
                   
               
                         
            
          
               