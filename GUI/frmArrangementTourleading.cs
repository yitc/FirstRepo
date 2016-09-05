using BIS.Business;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinForms.Documents.FormatProviders;
using Telerik.WinForms.Documents.FormatProviders.Html;
using Telerik.WinForms.Documents.Model;

namespace GUI
{
    public partial class frmArrangementTourleading : frmTemplate
    {
        public int iID = -1;
        public ArrangementModel arrange;

        List<VolontaryFunctionModel> arrVoluntary1; // drugi tab na volonterima
        List<VolontaryTripModel> arrVoluntary2; // treci tab na volonterima
        List<MedicalVoluntaryArrangementModel> arrVoluntary3;

        Boolean isVolChanged = false;
        public decimal questSort;


        public frmArrangementTourleading(ArrangementModel model)
        {
            arrange = model;
            iID = arrange.idArrangement;
          
            InitializeComponent();

            this.Icon = Login.iconForm;
            string formName = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();
            formName = formName + " " + model.nameArrangement;
            this.Text = formName;

        }

        private void frmArrangementTourleading_Load(object sender, EventArgs e)
        {
            loadOnSkillsSectab();

            btnSave.Click += btnSave_Click;

            radRibbonMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonReports.Visibility = ElementVisibility.Collapsed;
            btnReport.Visibility = ElementVisibility.Collapsed;
            btnEmail.Visibility = ElementVisibility.Collapsed;
            btnWord.Visibility = ElementVisibility.Collapsed;
            btnNewMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupPurchase.Visibility = ElementVisibility.Collapsed;
            btnPurchase.Visibility = ElementVisibility.Collapsed;
            btnDelPurchase.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;
            radRibbonDocuments.Visibility = ElementVisibility.Collapsed;
            btnDeleteDoc.Visibility = ElementVisibility.Collapsed;
            btnNewDoc.Visibility = ElementVisibility.Collapsed;

            lblNrVolHelers.Text = arrange.nrVoluntaryHelper.ToString();
        }
        public void btnSave_Click(object sender, EventArgs e)
        {
            if (radPageVH.SelectedPage.Name == "tabFunction")
            {
                VolontaryFunctionBUS vfb = new VolontaryFunctionBUS();
                saveVoluntaryFunction();
                if (isVolChanged == true)
                {
                    if (vfb.DeleteVoluntaryArrangement(arrange.idArrangement, this.Name, Login._user.idUser) == true)
                    {
                        for (int j = 0; j < arrVoluntary1.Count; j++)
                        {
                            if (vfb.SaveArrangement(arrVoluntary1[j], this.Name, Login._user.idUser) == false)   // ovde ubacio Arrangement 
                            {                                
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                                break;
                            }

                        }
                    }
                    else
                    {                        
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                    }
                }
            }
            else if (radPageVH.SelectedPage.Name == "tabTrips")
            {
                VolontaryTripBUS vtb = new VolontaryTripBUS();
                saveVoluntaryTrip();
                if (isVolChanged == true)
                {
                    if (vtb.DeleteVoluntaryArrangement(arrange.idArrangement, this.Name, Login._user.idUser) == true)
                    {
                        for (int j = 0; j < arrVoluntary2.Count; j++)
                        {
                            if (vtb.SaveArrangement(arrVoluntary2[j], this.Name, Login._user.idUser) == false)
                            {                                
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                                break;
                            }

                        }
                    }
                    else
                    {                        
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                    }
                }
            }
            else if (radPageVH.SelectedPage.Name == "tabSkills")
            {
                MedicalVoluntaryBUS skillbus = new MedicalVoluntaryBUS();
                saveSkills();
                if (isVolChanged == true)
                {
                    if (skillbus.DeleteVoluntaryArrangement(arrange.idArrangement, this.Name, Login._user.idUser) == true)
                    {
                        for (int j = 0; j < arrVoluntary3.Count; j++)
                        {
                            if (skillbus.SaveVoluntaryArrangement(arrVoluntary3[j], this.Name, Login._user.idUser) == false)
                            {                                
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                                break;
                            }

                        }
                    }
                }
            }

            translateRadMessageBox tr1 = new translateRadMessageBox();
            tr1.translateAllMessageBox("You have successfully save data");
        }
        private void radPageVH_SelectedPageChanged(object sender, EventArgs e)
        {
            RadPageView rpvVV = (RadPageView)sender;
            string sName2 = ((RadPageView)sender).SelectedPage.Name;

            switch (sName2)
            {
                case "tabFunction":
                    loadOnVoluntarySectab();
                    //isVolChanged = true;
                    break;
                case "tabTrips":
                    loadOnVoluntaryThird();
                    //isVolChanged = true;
                    break;
                case "tabFeatures":
                    break;
                case "tabSkills":
                    loadOnSkillsSectab();
                    //isVolChanged = true;
                    break;
            }
        }


        private void loadOnVoluntaryThird()
        {
            Cursor.Current = Cursors.WaitCursor;
            firstPartVoluntaryThird();

            RadToggleButton btnAllVoluntary = new RadToggleButton();
            btnAllVoluntary.CheckState = CheckState.Checked;
            btnAllVoluntary.ThemeName = radPageVH.ThemeName;
            btnAllVoluntary.Name = "btnAllVoluntary";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("Filled data") != null)
                    btnAllVoluntary.Text = resxSet.GetString("Filled data");
                else
                    btnAllVoluntary.Text = "Filled data";
            }
            btnAllVoluntary.Location = new Point(26, 10);
            btnAllVoluntary.Font = new Font("Verdana", 9);
            btnAllVoluntary.CheckStateChanged += btnVoluntaryTripAll_Click;

            RadToggleButton btnSortVoluntary = new RadToggleButton();
            btnSortVoluntary.CheckState = CheckState.Checked;
            btnSortVoluntary.ThemeName = radPageVH.ThemeName;
            btnSortVoluntary.Width = 200;
            btnSortVoluntary.Name = "btnSortVoluntary";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("Alfabetical sort") != null)
                    btnSortVoluntary.Text = resxSet.GetString("Alfabetical sort");
                else
                    btnSortVoluntary.Text = "Alfabetical sort";
            }
            btnSortVoluntary.Location = new Point(250, 10);
            btnSortVoluntary.Font = new Font("Verdana", 9);
            btnSortVoluntary.CheckStateChanged += btnVoluntaryTripSort_Click;

            tabTrips.Controls.Add(btnAllVoluntary);
            tabTrips.Controls.Add(btnSortVoluntary);

            secondPartVoluntaryTrip();

            Cursor.Current = Cursors.Default;

        }

        private void btnVoluntaryTripSort_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (arrange.idArrangement != null && arrange.idArrangement != 0)
            {
                VolontaryTripBUS mvb = new VolontaryTripBUS();
                saveVoluntaryTrip();
                if (isVolChanged == true)
                {
                    if (mvb.DeleteVoluntaryArrangement(arrange.idArrangement, this.Name, Login._user.idUser) == true)
                    {
                        for (int j = 0; j < arrVoluntary2.Count; j++)
                        {
                            if (mvb.SaveArrangement(arrVoluntary2[j], this.Name, Login._user.idUser) == false)    // 
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                            }
                        }
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                    }
                }
            }


            RadToggleButton btnAllVoluntary = (RadToggleButton)tabTrips.Controls.Find("btnAllVoluntary", true)[0];

            if (btnAllVoluntary.CheckState == CheckState.Checked)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Filled data") != null)
                        btnAllVoluntary.Text = resxSet.GetString("Filled data");
                    else
                        btnAllVoluntary.Text = "Filled data";
                }
            }
            else
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("All data") != null)
                        btnAllVoluntary.Text = resxSet.GetString("All data");
                    else
                        btnAllVoluntary.Text = "All data";
                }

            }


            RadToggleButton btnSortVoluntary = (RadToggleButton)tabTrips.Controls.Find("btnSortVoluntary", true)[0];

            if (btnSortVoluntary.CheckState == CheckState.Checked)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Alfabetical sort") != null)
                        btnSortVoluntary.Text = resxSet.GetString("Alfabetical sort");
                    else
                        btnSortVoluntary.Text = "Alfabetical sort";
                }
            }
            else
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Default sort") != null)
                        btnSortVoluntary.Text = resxSet.GetString("Default sort");
                    else
                        btnSortVoluntary.Text = "Default sort";
                }
            }
            firstPartVoluntaryThird();
            tabTrips.Controls.Add(btnAllVoluntary);   //
            tabTrips.Controls.Add(btnSortVoluntary);   //
            secondPartVoluntaryTrip();


            Cursor.Current = Cursors.Default;
        }

        private void btnVoluntaryTripAll_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (arrange.idArrangement != null && arrange.idArrangement != 0)
            {
                VolontaryTripBUS vtb = new VolontaryTripBUS();
                saveVoluntaryTrip();
                if (isVolChanged == true)
                {
                    if (vtb.DeleteVoluntaryArrangement(arrange.idArrangement, this.Name, Login._user.idUser) == true)
                    {
                        for (int j = 0; j < arrVoluntary2.Count; j++)
                        {
                            if (vtb.SaveArrangement(arrVoluntary2[j], this.Name, Login._user.idUser) == false)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                            }

                        }
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                    }
                }


                RadToggleButton btnAllVoluntary = (RadToggleButton)tabTrips.Controls.Find("btnAllVoluntary", true)[0];

                if (btnAllVoluntary.CheckState == CheckState.Checked)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Filled data") != null)
                            btnAllVoluntary.Text = resxSet.GetString("Filled data");
                        else
                            btnAllVoluntary.Text = "Filled data";
                    }
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("All data") != null)
                            btnAllVoluntary.Text = resxSet.GetString("All data");
                        else
                            btnAllVoluntary.Text = "All data";
                    }

                }


                RadToggleButton btnSortVoluntary = (RadToggleButton)tabTrips.Controls.Find("btnSortVoluntary", true)[0];

                if (btnSortVoluntary.CheckState == CheckState.Checked)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Alfabetical sort") != null)
                            btnSortVoluntary.Text = resxSet.GetString("Alfabetical sort");
                        else
                            btnSortVoluntary.Text = "Alfabetical sort";
                    }
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Default sort") != null)
                            btnSortVoluntary.Text = resxSet.GetString("Default sort");
                        else
                            btnSortVoluntary.Text = "Default sort";
                    }
                }

                firstPartVoluntaryThird();
                tabTrips.Controls.Add(btnAllVoluntary);   //
                tabTrips.Controls.Add(btnSortVoluntary);   //
                secondPartVoluntaryTrip();

                Cursor.Current = Cursors.Default;
            }
        }

        private void saveVoluntaryTrip()
        {
            if (arrange.idArrangement != 0)
            {
                
                arrVoluntary2 = new List<VolontaryTripModel>();
                VolontaryTripBUS vtb = new VolontaryTripBUS();
                
                if (tabTrips.Controls != null)
                {
                    for (int i = 0; i < tabTrips.Controls.Count; i++)
                    {
                        Control c = tabTrips.Controls[i];
                        VolontaryTripModel mvm = new VolontaryTripModel();
                        if (c is RadCheckBox)
                        {
                            string idQuest = c.Name.Split('A')[0].Substring(1, c.Name.Split('A')[0].Length - 1);
                            string idAns = c.Name.Split('A')[1].Substring(0, c.Name.Split('A')[1].Length);
                            RadCheckBox chk = (RadCheckBox)c;
                            if (chk.CheckState == CheckState.Checked)
                            {
                                mvm.idcpr = arrange.idArrangement;
                                mvm.idQuest = Convert.ToInt32(idQuest);
                                mvm.idAns = Convert.ToInt32(idAns);
                                arrVoluntary2.Add(mvm);

                            }
                        }
                        if (c is RadGroupBox)
                        {
                            RadGroupBox rgb = (RadGroupBox)c;
                            for (int j = 0; j < rgb.Controls.Count; j++)
                            {
                                string idQuest = rgb.Controls[j].Name.Split('A')[0].Substring(1, rgb.Controls[j].Name.Split('A')[0].Length - 1);
                                string idAns = rgb.Controls[j].Name.Split('A')[1].Substring(0, rgb.Controls[j].Name.Split('A')[1].Length);
                                RadRadioButton rb = (RadRadioButton)rgb.Controls[j];
                                //=============

                                if (rb.CheckState == CheckState.Checked)
                                {
                                    mvm = new VolontaryTripModel();
                                    mvm.idcpr = arrange.idArrangement;
                                    mvm.idQuest = Convert.ToInt32(idQuest);
                                    mvm.idAns = Convert.ToInt32(idAns);
                                    arrVoluntary2.Add(mvm);
                                }
                                // } 
                            }
                        }
                        if (c is RadTextBox)
                        {
                            string idQuest = c.Name.Split('A')[0].Substring(4, c.Name.Split('A')[0].Length - 4);
                            string idAns = c.Name.Split('A')[1].Substring(0, c.Name.Split('A')[1].Length);
                            RadTextBox rtb = (RadTextBox)c;
                            mvm = arrVoluntary2.Find(item => item.idcpr == arrange.idArrangement && item.idQuest == Convert.ToInt32(idQuest) && item.idAns == Convert.ToInt32(idAns));
                            if (mvm != null)
                            {
                                if (rtb.Text != "")
                                {
                                    mvm.txt = rtb.Text;
                                }
                            }
                            else
                            {
                                if (rtb.Text != "")
                                {
                                    mvm = new VolontaryTripModel();
                                    mvm.idcpr = arrange.idArrangement;
                                    mvm.idQuest = Convert.ToInt32(idQuest);
                                    mvm.idAns = Convert.ToInt32(idAns);
                                    mvm.txt = rtb.Text;
                                    arrVoluntary2.Add(mvm);
                                }

                            }
                        }
                    }
                }
            }
            else
                isVolChanged = false;
        }

        private void secondPartVoluntaryTrip()
        {
            List<string> idQueryType = new List<string>();

            List<LabelForArrangement> ArrangementLabel = new List<LabelForArrangement>();
            ArrangementBUS ab = new ArrangementBUS();
            ArrangementLabel = ab.GetLabelsArrangement(arrange.idArrangement);
            if (ArrangementLabel != null)
            {
                foreach (LabelForArrangement m in ArrangementLabel)
                {
                    idQueryType.Add(m.idLabel.ToString());
                }
            }

            RadToggleButton btnSortVoluntary = (RadToggleButton)tabTrips.Controls.Find("btnSortVoluntary", true)[0];
            RadToggleButton btnAllVoluntary = (RadToggleButton)tabTrips.Controls.Find("btnAllVoluntary", true)[0];

            Boolean isDefaultSort = false;

            if (btnSortVoluntary.CheckState == CheckState.Checked)
                isDefaultSort = true;

            //  Boolean isAll = false;
            Boolean isAll = false;

            if (btnAllVoluntary.CheckState == CheckState.Checked)
                isAll = true;


            VolontaryTripBUS vtb = new VolontaryTripBUS();
            arrVoluntary2 = vtb.GetVoluntaryTripArrDetails(idQueryType, arrange.idArrangement, isDefaultSort, isAll);

            //Y
            int lastBottom = 60;
            //X
            int left = 26;
            string oldQuest = "";
            string oldQuestGroup = "";  // Saki

            if (arrVoluntary2 != null)
            {
                if (arrVoluntary2.Count > 0)
                {
                    //create dynamic controls
                    for (int i = 0; i < arrVoluntary2.Count; i++)
                    {
                        string questGroup = ""; // saki
                        string quest = "";
                        string questText = "";
                        string ans = "";
                        string ansText = "";
                        Boolean ischecked = false;
                        //checkbox width
                        int chkWidth = 150;
                        //radiobutton width
                        int rbWidth = 150;
                        //datetimepicker width
                        int dtWidth = 100;
                        //question label width 
                        int rlWidth = (int)(30 * tabTrips.Width / 100) - 20;
                        //answer width (for all controls)
                        int aWidth = (int)(70 * tabTrips.Width / 100) - 80;
                        //question label height 
                        int rlheight = 30;
                        //answer row height  sa 30 na 20 sale
                        int height = 20;

                        if (arrVoluntary2[i].idQuest.ToString() != "")
                        {
                            quest = arrVoluntary2[i].idQuest.ToString().TrimEnd();
                        }
                        if (arrVoluntary2[i].idAns.ToString() != "")
                        {
                            ans = arrVoluntary2[i].idAns.ToString().TrimEnd();
                        }
                        if (arrVoluntary2[i].txtQuest.ToString() != "")
                        {
                            questText = arrVoluntary2[i].txtQuest.ToString().TrimEnd();
                        }
                        if (arrVoluntary2[i].txtAns.ToString() != "")
                        {
                            ansText = arrVoluntary2[i].txtAns.ToString().TrimEnd();
                        }
                        if (arrVoluntary2[i].idcpr != null)
                        {
                            ischecked = true;
                        }

                        //====== Saki
                        questGroup = arrVoluntary2[i].nameQuestGroup.TrimEnd();
                        //==========
                        quest = arrVoluntary2[i].idQuest.ToString().TrimEnd();
                        if (arrVoluntary2[i].questSort != null && arrVoluntary2[i].questSort != 0)
                            questSort = Convert.ToDecimal(arrVoluntary2[i].questSort.ToString());
                        if (arrVoluntary2[i].idAns.ToString() != "")
                        {
                            ans = arrVoluntary2[i].idAns.ToString().TrimEnd();
                        }
                        questText = arrVoluntary2[i].txtQuest.ToString().TrimEnd();

                        ansText = arrVoluntary2[i].txtAns.ToString().TrimEnd();
                        if (arrVoluntary2[i].idcpr.ToString() != "")
                        {
                            ischecked = true;
                        }


                        //question label
                        RadLabel rl = new RadLabel();
                        RadLabel rlTitle = new RadLabel();  // Saki


                        // ============  ova dva ifa su umesto ORIGINALA ============================
                        if (questGroup.ToUpper() != oldQuestGroup.ToUpper())
                        {
                            rlTitle.Text = questGroup.ToUpper();   //==== Saki
                            rlTitle.Location = new Point(left, lastBottom);
                            rlTitle.Font = new Font("Verdana", 9);
                            rlTitle.ForeColor = Color.DarkOrange;
                            //set multi lines
                            rlTitle.MaximumSize = new Size(rlWidth, height * 3);
                            rlTitle.AutoSize = true;
                            //
                            rlTitle.Width = rlWidth;
                            tabTrips.Controls.Add(rlTitle);
                            //oldQuest = quest;

                            //set rows height depends on number of lines in label question
                            rlheight = height * numberOfLines(rlTitle);
                            lastBottom = lastBottom + rlheight + 5;
                            oldQuestGroup = questGroup;
                        }

                        if (quest != oldQuest)
                        {
                            string s = questSort.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                            string[] parts = s.Split('.');
                            int i1 = int.Parse(parts[0]);
                            int i2 = int.Parse(parts[1]);

                            rl.Text = i1.ToString() + "-" + i2.ToString() + "  " + questText;
                            rl.Location = new Point(left, lastBottom);
                            rl.Font = new Font("Verdana", 9);
                            //set multi lines
                            rl.MaximumSize = new Size(rlWidth, height * 3);
                            rl.AutoSize = true;
                            //
                            rl.Width = rlWidth;
                            tabTrips.Controls.Add(rl);
                            oldQuest = quest;

                            //set rows height depends on number of lines in label question
                            rlheight = height * numberOfLines(rl);
                        }

                        //ANSWER
                        //checkbox type
                        if (arrVoluntary2[i].idAnsType.ToString() == "1")
                        {
                            RadCheckBox chk = new RadCheckBox();
                            chk.Name = "Q" + quest + "A" + ans;
                            chk.Text = ansText;
                            chk.Width = aWidth;
                            chk.Height = height;
                            chk.Font = new Font("Verdana", 9);
                            chk.Location = new Point(left + rlWidth + 20, lastBottom);
                            chk.CheckStateChanged += radCheckBoxVH_CheckStateChanged;
                            if (ischecked == true)
                            {
                                chk.CheckState = CheckState.Checked;
                            }
                            tabTrips.Controls.Add(chk);

                        }
                        //radio button type
                        else if (arrVoluntary2[i].idAnsType.ToString() == "2")
                        {
                            RadRadioButton rb = new RadRadioButton();
                            rb.Name = "Q" + quest + "A" + ans;
                            rb.Text = ansText;
                            rb.MaximumSize = new Size(400, 0);
                            rb.MinimumSize = new Size(40, 0);
                            rb.AutoSize = true;
                            rb.Width = rbWidth;
                            rb.Height = height;
                            rb.Font = new Font("Verdana", 9);
                            rb.CheckStateChanged += radRadioButtonVH_CheckStateChanged;
                            if (ischecked == true)
                            {
                                rb.CheckState = CheckState.Checked;
                            }

                            RadGroupBox rgb = new RadGroupBox();
                            rgb.MaximumSize = new Size(400, 0);
                            rgb.MinimumSize = new Size(40, 0);
                            rgb.AutoSize = true;
                            if (tabTrips.Controls.Find("Group" + quest, true).Length > 0)
                            {
                                rgb = (RadGroupBox)tabTrips.Controls.Find("Group" + quest, true)[0];
                                rgb.Height = lastBottom - rgb.Location.Y + height;
                                if (rb.Width > rgb.Width)
                                    rgb.Width = rb.Width;
                            }
                            else
                            {
                                rgb.Name = "Group" + quest;
                                ((Telerik.WinControls.Primitives.FillPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).InnerColor = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).ForeColor = System.Drawing.Color.Transparent;
                                tabTrips.Controls.Add(rgb);
                                rgb.Width = rbWidth;
                                rgb.Height = height;
                                rgb.Font = new Font("Verdana", 9);
                                rgb.Location = new Point(left + rlWidth + 20, lastBottom);
                                rgb.Width = rb.Width;
                            }
                            rb.Location = new Point(0, lastBottom - rgb.Location.Y);
                            rgb.Controls.Add(rb);
                        }
                        //checkbox + textbox type
                        else if (arrVoluntary2[i].idAnsType.ToString() == "3")
                        {

                            RadCheckBox chk = new RadCheckBox();
                            chk.Name = "Q" + quest + "A" + ans;
                            chk.Text = ansText;
                            chk.MaximumSize = new Size(400, 0);
                            chk.MinimumSize = new Size(40, 0);
                            chk.AutoSize = true;
                            chk.Width = chkWidth;
                            chk.Height = height;
                            chk.Font = new Font("Verdana", 9);
                            chk.Location = new Point(left + rlWidth + 20, lastBottom);
                            chk.CheckStateChanged += radCheckBoxVH_CheckStateChanged;
                            tabTrips.Controls.Add(chk);
                            RadTextBox rtb = new RadTextBox();
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Width = aWidth - chk.Width - 20;
                            rtb.Height = height;
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Location = new Point(left + rlWidth + 20 + chk.Width + 20, lastBottom);

                            rtb.TextChanged += txtTextBoxVH_TextChanged;
                            if (ischecked == true)
                            {
                                chk.CheckState = CheckState.Checked;
                                if (arrVoluntary2[i].txt != null)
                                {
                                    rtb.Text = arrVoluntary2[i].txt.ToString();
                                }

                            }

                            tabTrips.Controls.Add(rtb);
                        }
                        //textbox type
                        else if (arrVoluntary2[i].idAnsType.ToString() == "4")
                        {
                            RadTextBox rtb = new RadTextBox();
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Width = aWidth;
                            rtb.Height = height;
                            rtb.Location = new Point(left + rlWidth + 20, lastBottom);
                            rtb.TextChanged += txtTextBoxVH_TextChanged;
                            if (ischecked == true)
                            {
                                if (arrVoluntary2[i].txt != null)
                                {
                                    rtb.Text = arrVoluntary2[i].txt.ToString();
                                }
                            }
                            tabTrips.Controls.Add(rtb);
                        }
                        //radio button + textbox type
                        else if (arrVoluntary2[i].idAnsType.ToString() == "5")
                        {
                            RadRadioButton rb = new RadRadioButton();
                            rb.CheckStateChanged += radRadioButtonVH_CheckStateChanged;
                            rb.Name = "Q" + quest + "A" + ans;
                            rb.Text = ansText;
                            rb.MaximumSize = new Size(400, 0);
                            rb.MinimumSize = new Size(40, 0);
                            rb.AutoSize = true;
                            rb.Width = rbWidth;
                            rb.Height = height;
                            rb.Font = new Font("Verdana", 9);

                            if (ischecked == true)
                            {
                                rb.CheckState = CheckState.Checked;
                            }

                            RadGroupBox rgb = new RadGroupBox();
                            rgb.MaximumSize = new Size(400, 0);
                            rgb.MinimumSize = new Size(40, 0);
                            rgb.AutoSize = true;
                            if (tabTrips.Controls.Find("Group" + quest, true).Length > 0)
                            {
                                rgb = (RadGroupBox)tabTrips.Controls.Find("Group" + quest, true)[0];
                                rgb.Height = lastBottom - rgb.Location.Y + height;
                            }
                            else
                            {
                                rgb.Name = "Group" + quest;
                                ((Telerik.WinControls.Primitives.FillPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).InnerColor = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).ForeColor = System.Drawing.Color.Transparent;
                                tabTrips.Controls.Add(rgb);
                                rgb.Width = rbWidth;
                                rgb.Height = height;
                                rgb.Font = new Font("Verdana", 9);
                                rgb.Location = new Point(left + rlWidth + 20, lastBottom);
                            }

                            rb.Location = new Point(0, lastBottom - rgb.Location.Y);
                            //rgb.Width = 50;
                            rgb.Controls.Add(rb);

                            if (tabTrips.Controls.Find("Group" + quest, true).Length > 0)
                            {
                                if (rb.Width > rgb.Width)
                                    rgb.Width = rb.Width;
                            }
                            else
                            {
                                rgb.Width = rb.Width;
                            }

                            RadTextBox rtb = new RadTextBox();
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Width = aWidth - rb.Width - 20;
                            rtb.Height = height;
                            rtb.Location = new Point(left + rlWidth + 20 + rb.Width + 20, lastBottom);
                            rtb.TextChanged += txtTextBoxVH_TextChanged;
                            if (ischecked == true)
                            {
                                rb.CheckState = CheckState.Checked;
                                if (arrVoluntary2[i].txt != null)
                                {
                                    rtb.Text = arrVoluntary2[i].txt.ToString();
                                }

                            }
                            tabTrips.Controls.Add(rtb);
                        }
                        //checkbox + textbox + datetime
                        else if (arrVoluntary2[i].idAnsType.ToString() == "6")
                        {

                        }

                        lastBottom = lastBottom + rlheight + 5;
                    }
                }
            }
            isVolChanged = false;
        }


        private void firstPartVoluntaryThird()
        {
            tabTrips.Controls.Clear();
        }


        private void loadOnVoluntarySectab()
        {
            Cursor.Current = Cursors.WaitCursor;
            firstPartVoluntarySectab();

            RadToggleButton btnAllVoluntary = new RadToggleButton();
            btnAllVoluntary.CheckState = CheckState.Checked;
            btnAllVoluntary.ThemeName = radPageVH.ThemeName;
            btnAllVoluntary.Name = "btnAllVoluntary";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("Filled data") != null)
                    btnAllVoluntary.Text = resxSet.GetString("Filled data");
                else
                    btnAllVoluntary.Text = "Filled data";
            }
            btnAllVoluntary.Location = new Point(26, 10);
            btnAllVoluntary.Font = new Font("Verdana", 9);
            btnAllVoluntary.CheckStateChanged += btnVoluntaryAll_Click;

            RadToggleButton btnSortVoluntary = new RadToggleButton();
            btnSortVoluntary.CheckState = CheckState.Checked;
            btnSortVoluntary.ThemeName = radPageVH.ThemeName;
            btnSortVoluntary.Width = 200;
            btnSortVoluntary.Name = "btnSortVoluntary";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("Alfabetical sort") != null)
                    btnSortVoluntary.Text = resxSet.GetString("Alfabetical sort");
                else
                    btnSortVoluntary.Text = "Alfabetical sort";
            }
            btnSortVoluntary.Location = new Point(250, 10);
            btnSortVoluntary.Font = new Font("Verdana", 9);
            btnSortVoluntary.CheckStateChanged += btnVoluntarySort_Click;

            tabFunction.Controls.Add(btnAllVoluntary);
            tabFunction.Controls.Add(btnSortVoluntary);

            secondPartVoluntaryFunction();

            Cursor.Current = Cursors.Default;

        }
        private void firstPartVoluntarySectab()
        {
            tabFunction.Controls.Clear();

        }
        private void btnVoluntarySort_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (arrange.idArrangement != null && arrange.idArrangement != 0)
            {
                VolontaryFunctionBUS mvb = new VolontaryFunctionBUS();
                saveVoluntaryFunction();
                if (isVolChanged == true)
                {
                    if (mvb.DeleteVoluntaryArrangement(arrange.idArrangement, this.Name, Login._user.idUser) == true)
                    {
                        for (int j = 0; j < arrVoluntary1.Count; j++)
                        {
                            if (mvb.SaveArrangement(arrVoluntary1[j], this.Name, Login._user.idUser) == false)    // 
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                            }
                        }
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                    }
                }
            }


            RadToggleButton btnAllVoluntary = (RadToggleButton)tabFunction.Controls.Find("btnAllVoluntary", true)[0];

            if (btnAllVoluntary.CheckState == CheckState.Checked)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Filled data") != null)
                        btnAllVoluntary.Text = resxSet.GetString("Filled data");
                    else
                        btnAllVoluntary.Text = "Filled data";
                }
            }
            else
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("All data") != null)
                        btnAllVoluntary.Text = resxSet.GetString("All data");
                    else
                        btnAllVoluntary.Text = "All data";
                }

            }


            RadToggleButton btnSortVoluntary = (RadToggleButton)tabFunction.Controls.Find("btnSortVoluntary", true)[0];

            if (btnSortVoluntary.CheckState == CheckState.Checked)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Alfabetical sort") != null)
                        btnSortVoluntary.Text = resxSet.GetString("Alfabetical sort");
                    else
                        btnSortVoluntary.Text = "Alfabetical sort";
                }
            }
            else
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Default sort") != null)
                        btnSortVoluntary.Text = resxSet.GetString("Default sort");
                    else
                        btnSortVoluntary.Text = "Default sort";
                }
            }
            firstPartVoluntarySectab();
            tabFunction.Controls.Add(btnAllVoluntary);   //
            tabFunction.Controls.Add(btnSortVoluntary);   //
            secondPartVoluntaryFunction();

            Cursor.Current = Cursors.Default;
        }
        private void btnVoluntaryAll_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (arrange.idArrangement != null && arrange.idArrangement != 0)
            {
                VolontaryFunctionBUS vfb = new VolontaryFunctionBUS();
                saveVoluntaryFunction();
                if (isVolChanged == true)
                {
                    if (vfb.DeleteVoluntaryArrangement(arrange.idArrangement, this.Name, Login._user.idUser) == true)
                    {
                        for (int j = 0; j < arrVoluntary1.Count; j++)
                        {
                            if (vfb.SaveArrangement(arrVoluntary1[j], this.Name, Login._user.idUser) == false)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                            }

                        }
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                    }
                }


                RadToggleButton btnAllVoluntary = (RadToggleButton)tabFunction.Controls.Find("btnAllVoluntary", true)[0];

                if (btnAllVoluntary.CheckState == CheckState.Checked)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Filled data") != null)
                            btnAllVoluntary.Text = resxSet.GetString("Filled data");
                        else
                            btnAllVoluntary.Text = "Filled data";
                    }
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("All data") != null)
                            btnAllVoluntary.Text = resxSet.GetString("All data");
                        else
                            btnAllVoluntary.Text = "All data";
                    }

                }


                RadToggleButton btnSortVoluntary = (RadToggleButton)tabFunction.Controls.Find("btnSortVoluntary", true)[0];

                if (btnSortVoluntary.CheckState == CheckState.Checked)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Alfabetical sort") != null)
                            btnSortVoluntary.Text = resxSet.GetString("Alfabetical sort");
                        else
                            btnSortVoluntary.Text = "Alfabetical sort";
                    }
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Default sort") != null)
                            btnSortVoluntary.Text = resxSet.GetString("Default sort");
                        else
                            btnSortVoluntary.Text = "Default sort";
                    }
                }

                firstPartVoluntarySectab();
                tabFunction.Controls.Add(btnAllVoluntary);   //
                tabFunction.Controls.Add(btnSortVoluntary);   //
                secondPartVoluntaryFunction();

                Cursor.Current = Cursors.Default;
            }
        }

        private void saveVoluntaryFunction()
        {
            if (arrange.idArrangement != 0)
            {                
                arrVoluntary1 = new List<VolontaryFunctionModel>();
                VolontaryFunctionBUS vfb = new VolontaryFunctionBUS();
                
                if (tabFunction.Controls != null)
                {
                    for (int i = 0; i < tabFunction.Controls.Count; i++)
                    {
                        Control c = tabFunction.Controls[i];
                        VolontaryFunctionModel mvm = new VolontaryFunctionModel();
                        if (c is RadCheckBox)
                        {
                            string idQuest = c.Name.Split('A')[0].Substring(1, c.Name.Split('A')[0].Length - 1);
                            string idAns = c.Name.Split('A')[1].Substring(0, c.Name.Split('A')[1].Length);
                            RadCheckBox chk = (RadCheckBox)c;
                            if (chk.CheckState == CheckState.Checked)
                            {
                                mvm.idcpr = arrange.idArrangement;
                                mvm.idQuest = Convert.ToInt32(idQuest);
                                mvm.idAns = Convert.ToInt32(idAns);
                                arrVoluntary1.Add(mvm);

                            }
                        }
                        if (c is RadGroupBox)
                        {
                            RadGroupBox rgb = (RadGroupBox)c;
                            for (int j = 0; j < rgb.Controls.Count; j++)
                            {
                                string idQuest = rgb.Controls[j].Name.Split('A')[0].Substring(1, rgb.Controls[j].Name.Split('A')[0].Length - 1);
                                string idAns = rgb.Controls[j].Name.Split('A')[1].Substring(0, rgb.Controls[j].Name.Split('A')[1].Length);
                                RadRadioButton rb = (RadRadioButton)rgb.Controls[j];
                                //=============

                                if (rb.CheckState == CheckState.Checked)
                                {
                                    mvm = new VolontaryFunctionModel();
                                    mvm.idcpr = arrange.idArrangement;
                                    mvm.idQuest = Convert.ToInt32(idQuest);
                                    mvm.idAns = Convert.ToInt32(idAns);
                                    arrVoluntary1.Add(mvm);
                                }
                                // } 
                            }
                        }
                        if (c is RadTextBox)
                        {
                            string idQuest = c.Name.Split('A')[0].Substring(4, c.Name.Split('A')[0].Length - 4);
                            string idAns = c.Name.Split('A')[1].Substring(0, c.Name.Split('A')[1].Length);
                            RadTextBox rtb = (RadTextBox)c;
                            mvm = arrVoluntary1.Find(item => item.idcpr == arrange.idArrangement && item.idQuest == Convert.ToInt32(idQuest) && item.idAns == Convert.ToInt32(idAns));
                            if (mvm != null)
                            {
                                if (rtb.Text != "")
                                {
                                    mvm.txt = rtb.Text;
                                }
                            }
                            else
                            {
                                if (rtb.Text != "")
                                {
                                    mvm = new VolontaryFunctionModel();
                                    mvm.idcpr = arrange.idArrangement;
                                    mvm.idQuest = Convert.ToInt32(idQuest);
                                    mvm.idAns = Convert.ToInt32(idAns);
                                    mvm.txt = rtb.Text;
                                    arrVoluntary1.Add(mvm);
                                }

                            }
                        }
                    }
                }                
            }
            else
                isVolChanged = false;
        }

        private void secondPartVoluntaryFunction()
        {
            List<string> idQueryType = new List<string>();
           
            List<LabelForArrangement> ArrangementLabel = new List<LabelForArrangement>();
            ArrangementBUS ab = new ArrangementBUS();
            ArrangementLabel = ab.GetLabelsArrangement(arrange.idArrangement);
            if (ArrangementLabel != null)
            {
                foreach (LabelForArrangement m in ArrangementLabel)
                {
                    idQueryType.Add(m.idLabel.ToString());
                }
            }
    

            RadToggleButton btnSortVoluntary = (RadToggleButton)tabFunction.Controls.Find("btnSortVoluntary", true)[0];
            RadToggleButton btnAllVoluntary = (RadToggleButton)tabFunction.Controls.Find("btnAllVoluntary", true)[0];

            Boolean isDefaultSort = false;

            if (btnSortVoluntary.CheckState == CheckState.Checked)
                isDefaultSort = true;

            //  Boolean isAll = false;
            Boolean isAll = false;

            if (btnAllVoluntary.CheckState == CheckState.Checked)
                isAll = true;


            VolontaryFunctionBUS vfb = new VolontaryFunctionBUS();
            arrVoluntary1 = vfb.GetVoluntaryArrangmentDetails(idQueryType, arrange.idArrangement, isDefaultSort, isAll);

            //Y
            int lastBottom = 60;
            //X
            int left = 26;
            string oldQuest = "";

            if (arrVoluntary1 != null)
            {
                if (arrVoluntary1.Count > 0)
                {
                    //create dynamic controls
                    for (int i = 0; i < arrVoluntary1.Count; i++)
                    {
                        string quest = "";
                        string questText = "";
                        string ans = "";
                        string ansText = "";
                        Boolean ischecked = false;
                        //checkbox width
                        int chkWidth = 150;
                        //radiobutton width
                        int rbWidth = 150;
                        //datetimepicker width
                        int dtWidth = 100;
                        //question label width 
                        int rlWidth = (int)(30 * tabFunction.Width / 100) - 20;
                        //answer width (for all controls)
                        int aWidth = (int)(70 * tabFunction.Width / 100) - 80;
                        //question label height 
                        int rlheight = 30;
                        //answer row height  sa 30 na 20 sale
                        int height = 20;

                        if (arrVoluntary1[i].idQuest.ToString() != "")
                        {
                            quest = arrVoluntary1[i].idQuest.ToString().TrimEnd();
                        }
                        if (arrVoluntary1[i].idAns.ToString() != "")
                        {
                            ans = arrVoluntary1[i].idAns.ToString().TrimEnd();
                        }
                        if (arrVoluntary1[i].txtQuest.ToString() != "")
                        {
                            questText = arrVoluntary1[i].txtQuest.ToString().TrimEnd();
                        }
                        if (arrVoluntary1[i].txtAns.ToString() != "")
                        {
                            ansText = arrVoluntary1[i].txtAns.ToString().TrimEnd();
                        }
                        if (arrVoluntary1[i].idcpr != null)
                        {
                            ischecked = true;
                        }

                        //question label
                        RadLabel rl = new RadLabel();

                        if (quest != oldQuest)
                        {
                            rl.Text = questText;
                            rl.Location = new Point(left, lastBottom);
                            rl.Font = new Font("Verdana", 9);
                            //set multi lines
                            rl.MaximumSize = new Size(rlWidth, height * 3);
                            rl.AutoSize = true;
                            //
                            rl.Width = rlWidth;
                            tabFunction.Controls.Add(rl);
                            oldQuest = quest;

                            //set rows height depends on number of lines in label question
                            rlheight = height * numberOfLines(rl);
                        }

                        //ANSWER
                        //checkbox type
                        if (arrVoluntary1[i].idAnsType.ToString() == "113")  // sa 1 na 3
                        {
                            RadCheckBox chk = new RadCheckBox();
                            chk.Name = "Q" + quest + "A" + ans;
                            chk.Text = ansText;
                            chk.Width = aWidth;
                            chk.Height = height;
                            chk.Font = new Font("Verdana", 9);
                            chk.Location = new Point(left + rlWidth + 20, lastBottom);
                            chk.CheckStateChanged += radCheckBoxVH_CheckStateChanged;
                            if (ischecked == true)
                            {
                                chk.CheckState = CheckState.Checked;
                            }
                            tabFunction.Controls.Add(chk);

                        }
                        //radio button type
                        else if (arrVoluntary1[i].idAnsType.ToString() == "2")
                        {
                            RadRadioButton rb = new RadRadioButton();
                            rb.Name = "Q" + quest + "A" + ans;
                            rb.Text = ansText;
                            rb.MaximumSize = new Size(400, 0);
                            rb.MinimumSize = new Size(40, 0);
                            rb.AutoSize = true;
                            rb.Width = rbWidth;
                            rb.Height = height;
                            rb.Font = new Font("Verdana", 9);
                            rb.CheckStateChanged += radRadioButtonVH_CheckStateChanged;
                            if (ischecked == true)
                            {
                                rb.CheckState = CheckState.Checked;
                            }

                            RadGroupBox rgb = new RadGroupBox();
                            rgb.MaximumSize = new Size(400, 0);
                            rgb.MinimumSize = new Size(40, 0);
                            rgb.AutoSize = true;
                            if (tabFunction.Controls.Find("Group" + quest, true).Length > 0)
                            {
                                rgb = (RadGroupBox)tabFunction.Controls.Find("Group" + quest, true)[0];
                                rgb.Height = lastBottom - rgb.Location.Y + height;
                                if (rb.Width > rgb.Width)
                                    rgb.Width = rb.Width;
                            }
                            else
                            {
                                rgb.Name = "Group" + quest;
                                ((Telerik.WinControls.Primitives.FillPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).InnerColor = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).ForeColor = System.Drawing.Color.Transparent;
                                tabFunction.Controls.Add(rgb);
                                rgb.Width = rbWidth;
                                rgb.Height = height;
                                rgb.Font = new Font("Verdana", 9);
                                rgb.Location = new Point(left + rlWidth + 20, lastBottom);
                                rgb.Width = rb.Width;
                            }
                            rb.Location = new Point(0, lastBottom - rgb.Location.Y);
                            rgb.Controls.Add(rb);
                        }
                        //checkbox + textbox type
                        else if (arrVoluntary1[i].idAnsType.ToString() == "1") /// sa 3 na 1
                        {

                            RadCheckBox chk = new RadCheckBox();
                            chk.Name = "Q" + quest + "A" + ans;
                            chk.Text = ansText;
                            chk.MaximumSize = new Size(400, 0);
                            chk.MinimumSize = new Size(40, 0);
                            chk.AutoSize = true;
                            chk.Width = chkWidth;
                            chk.Height = height;
                            chk.Font = new Font("Verdana", 9);
                            chk.Location = new Point(left + rlWidth + 20, lastBottom);
                            chk.CheckStateChanged += radCheckBoxVH_CheckStateChanged;
                            tabFunction.Controls.Add(chk);
                            RadTextBox rtb = new RadTextBox();
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            // rtb.Width = aWidth - chk.Width - 20;
                            rtb.Width = 60;
                            rtb.Height = height;  //height
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Location = new Point(10 + rlWidth + 20 + chk.Width + 20, lastBottom);  //left
                            rtb.TextChanged += txtTextBoxVH_TextChanged;
                            if (ischecked == true)
                            {
                                chk.CheckState = CheckState.Checked;
                                if (arrVoluntary1[i].txt != null)
                                {
                                    rtb.Text = arrVoluntary1[i].txt.ToString();
                                }

                            }

                            tabFunction.Controls.Add(rtb);
                        }
                        //textbox type
                        else if (arrVoluntary1[i].idAnsType.ToString() == "4")
                        {
                            RadTextBox rtb = new RadTextBox();
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Width = aWidth;
                            rtb.Height = height;
                            rtb.Location = new Point(left + rlWidth + 20, lastBottom);
                            rtb.TextChanged += txtTextBoxVH_TextChanged;
                            if (ischecked == true)
                            {
                                if (arrVoluntary1[i].txt != null)
                                {
                                    rtb.Text = arrVoluntary1[i].txt.ToString();
                                }
                            }
                            tabFunction.Controls.Add(rtb);
                        }
                        //radio button + textbox type
                        else if (arrVoluntary1[i].idAnsType.ToString() == "5")
                        {
                            RadRadioButton rb = new RadRadioButton();
                            rb.CheckStateChanged += radRadioButtonVH_CheckStateChanged;
                            rb.Name = "Q" + quest + "A" + ans;
                            rb.Text = ansText;
                            rb.MaximumSize = new Size(400, 0);
                            rb.MinimumSize = new Size(40, 0);
                            rb.AutoSize = true;
                            rb.Width = rbWidth;
                            rb.Height = height;
                            rb.Font = new Font("Verdana", 9);

                            if (ischecked == true)
                            {
                                rb.CheckState = CheckState.Checked;
                            }

                            RadGroupBox rgb = new RadGroupBox();
                            rgb.MaximumSize = new Size(400, 0);
                            rgb.MinimumSize = new Size(40, 0);
                            rgb.AutoSize = true;
                            if (tabFunction.Controls.Find("Group" + quest, true).Length > 0)
                            {
                                rgb = (RadGroupBox)tabFunction.Controls.Find("Group" + quest, true)[0];
                                rgb.Height = lastBottom - rgb.Location.Y + height;
                            }
                            else
                            {
                                rgb.Name = "Group" + quest;
                                ((Telerik.WinControls.Primitives.FillPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).InnerColor = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).ForeColor = System.Drawing.Color.Transparent;
                                tabFunction.Controls.Add(rgb);
                                rgb.Width = rbWidth;
                                rgb.Height = height;
                                rgb.Font = new Font("Verdana", 9);
                                rgb.Location = new Point(left + rlWidth + 20, lastBottom);
                            }

                            rb.Location = new Point(0, lastBottom - rgb.Location.Y);
                            //rgb.Width = 50;
                            rb.CheckStateChanged += radRadioButtonVH_CheckStateChanged;
                            rgb.Controls.Add(rb);

                            if (tabFunction.Controls.Find("Group" + quest, true).Length > 0)
                            {
                                if (rb.Width > rgb.Width)
                                    rgb.Width = rb.Width;
                            }
                            else
                            {
                                rgb.Width = rb.Width;
                            }

                            RadTextBox rtb = new RadTextBox();
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Width = aWidth - rb.Width - 20;
                            rtb.Height = height;
                            rtb.Location = new Point(left + rlWidth + 20 + rb.Width + 20, lastBottom);
                            rtb.TextChanged += txtTextBoxVH_TextChanged;
                            if (ischecked == true)
                            {
                                rb.CheckState = CheckState.Checked;
                                if (arrVoluntary1[i].txt != null)
                                {
                                    rtb.Text = arrVoluntary1[i].txt.ToString();
                                }

                            }

                            tabFunction.Controls.Add(rtb);
                        }
                        //checkbox + textbox + datetime
                        else if (arrVoluntary1[i].idAnsType.ToString() == "6")
                        {

                        }

                        lastBottom = lastBottom + rlheight + 5;
                    }
                }
            }

            isVolChanged = false;
        }

        //count number of lines in label
        private int numberOfLines(RadLabel rl)
        {
            Graphics g = rl.CreateGraphics();
            Single LineHeight = g.MeasureString("X", rl.Font).Height;
            Single TotalHeight = g.MeasureString(rl.Text, rl.Font, rl.Width).Height;
            int nl = (int)Math.Round(TotalHeight / LineHeight);

            return nl;
        }

        private void loadOnSkillsSectab()
        {
            Cursor.Current = Cursors.WaitCursor;
            firstPartSkillsThird();

            RadToggleButton btnAllSkills = new RadToggleButton();
            btnAllSkills.CheckState = CheckState.Checked;
            btnAllSkills.ThemeName = radPageVH.ThemeName;
            btnAllSkills.Name = "btnAllSkills";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("Filled data") != null)
                    btnAllSkills.Text = resxSet.GetString("Filled data");
                else
                    btnAllSkills.Text = "Filled data";
            }
            btnAllSkills.Location = new Point(26, 10);
            btnAllSkills.Font = new Font("Verdana", 9);
            btnAllSkills.CheckStateChanged += btnSkillsAll_Click;

            RadToggleButton btnSortSkills = new RadToggleButton();
            btnSortSkills.CheckState = CheckState.Checked;
            btnSortSkills.ThemeName = radPageVH.ThemeName;
            btnSortSkills.Width = 200;
            btnSortSkills.Name = "btnSortSkills";
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("Alfabetical sort") != null)
                    btnSortSkills.Text = resxSet.GetString("Alfabetical sort");
                else
                    btnSortSkills.Text = "Alfabetical sort";
            }
            btnSortSkills.Location = new Point(250, 10);
            btnSortSkills.Font = new Font("Verdana", 9);
            btnSortSkills.CheckStateChanged += btnSkillsSort_Click;

            tabSkills.Controls.Add(btnAllSkills);
            tabSkills.Controls.Add(btnSortSkills);

            secondPartSkills();

            Cursor.Current = Cursors.Default;

        }
        private void firstPartSkillsThird()
        {
            tabSkills.Controls.Clear();

        }
        private void btnSkillsAll_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (arrange.idArrangement != 0)
            {
                MedicalVoluntaryBUS vtb = new MedicalVoluntaryBUS();

                saveSkills();
                if (isVolChanged == true)
                {
                    if (vtb.DeleteVoluntaryArrangement(arrange.idArrangement, this.Name, Login._user.idUser) == true)
                    {
                        for (int j = 0; j < arrVoluntary3.Count; j++)
                        {
                            if (vtb.SaveVoluntaryArrangement(arrVoluntary3[j], this.Name, Login._user.idUser) == false)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                            }

                        }
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                    }
                }


                RadToggleButton btnAllSkills = (RadToggleButton)tabSkills.Controls.Find("btnAllSkills", true)[0];

                if (btnAllSkills.CheckState == CheckState.Checked)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Filled data") != null)
                            btnAllSkills.Text = resxSet.GetString("Filled data");
                        else
                            btnAllSkills.Text = "Filled data";
                    }
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("All data") != null)
                            btnAllSkills.Text = resxSet.GetString("All data");
                        else
                            btnAllSkills.Text = "All data";
                    }

                }


                RadToggleButton btnSortSkills = (RadToggleButton)tabSkills.Controls.Find("btnSortSkills", true)[0];

                if (btnSortSkills.CheckState == CheckState.Checked)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Alfabetical sort") != null)
                            btnSortSkills.Text = resxSet.GetString("Alfabetical sort");
                        else
                            btnSortSkills.Text = "Alfabetical sort";
                    }
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Default sort") != null)
                            btnSortSkills.Text = resxSet.GetString("Default sort");
                        else
                            btnSortSkills.Text = "Default sort";
                    }
                }

                firstPartSkillsThird();
                tabSkills.Controls.Add(btnAllSkills);   //
                tabSkills.Controls.Add(btnSortSkills);   //
                secondPartSkills();

                Cursor.Current = Cursors.Default;
            }
        }
        private void saveSkills()
        {
            if (arrange.idArrangement != 0)
            {                
                arrVoluntary3 = new List<MedicalVoluntaryArrangementModel>();
                MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
                
                if (tabSkills.Controls != null)
                {
                    for (int i = 0; i < tabSkills.Controls.Count; i++)
                    {
                        Control c = tabSkills.Controls[i];
                        MedicalVoluntaryArrangementModel mam = new MedicalVoluntaryArrangementModel();
                        if (c is RadCheckBox)
                        {
                            string idQuest = c.Name.Split('A')[0].Substring(1, c.Name.Split('A')[0].Length - 1);
                            string idAns = c.Name.Split('A')[1].Substring(0, c.Name.Split('A')[1].Length);
                            RadCheckBox chk = (RadCheckBox)c;
                            if (chk.CheckState == CheckState.Checked)
                            {
                                mam.idArr = arrange.idArrangement;
                                mam.idQuest = Convert.ToInt32(idQuest);
                                mam.idAns = Convert.ToInt32(idAns);
                                arrVoluntary3.Add(mam);

                            }
                        }
                        if (c is RadGroupBox)
                        {
                            RadGroupBox rgb = (RadGroupBox)c;
                            for (int j = 0; j < rgb.Controls.Count; j++)
                            {
                                string idQuest = rgb.Controls[j].Name.Split('A')[0].Substring(1, rgb.Controls[j].Name.Split('A')[0].Length - 1);
                                string idAns = rgb.Controls[j].Name.Split('A')[1].Substring(0, rgb.Controls[j].Name.Split('A')[1].Length);
                                RadRadioButton rb = (RadRadioButton)rgb.Controls[j];
                                //=============

                                if (rb.CheckState == CheckState.Checked)
                                {
                                    mam = new MedicalVoluntaryArrangementModel();
                                    mam.idArr = arrange.idArrangement;
                                    mam.idQuest = Convert.ToInt32(idQuest);
                                    mam.idAns = Convert.ToInt32(idAns);
                                    arrVoluntary3.Add(mam);
                                }
                                // } 
                            }
                        }
                        if (c is RadTextBox)
                        {
                            string idQuest = c.Name.Split('A')[0].Substring(4, c.Name.Split('A')[0].Length - 4);
                            string idAns = c.Name.Split('A')[1].Substring(0, c.Name.Split('A')[1].Length);
                            RadTextBox rtb = (RadTextBox)c;
                            mam = arrVoluntary3.Find(item => item.idArr == arrange.idArrangement && item.idQuest == Convert.ToInt32(idQuest) && item.idAns == Convert.ToInt32(idAns));
                            if (mam != null)
                            {
                                if (rtb.Text != "")
                                {
                                    mam.txt = rtb.Text;
                                }
                            }
                            else
                            {
                                if (rtb.Text != "")
                                {
                                    mam = new MedicalVoluntaryArrangementModel();
                                    mam.idArr = arrange.idArrangement;
                                    mam.idQuest = Convert.ToInt32(idQuest);
                                    mam.idAns = Convert.ToInt32(idAns);
                                    mam.txt = rtb.Text;
                                    arrVoluntary3.Add(mam);
                                }

                            }
                        }
                    }
                }
            }
            else
                isVolChanged = false;
        }

        private void btnSkillsSort_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (arrange.idArrangement != null && arrange.idArrangement != 0)
            {
                MedicalVoluntaryBUS mab = new MedicalVoluntaryBUS();
                saveSkills();
                if (isVolChanged == true)
                {
                    if (mab.DeleteVoluntaryArrangement(arrange.idArrangement, this.Name, Login._user.idUser) == true)
                    {
                        for (int j = 0; j < arrVoluntary3.Count; j++)
                        {
                            if (mab.SaveVoluntaryArrangement(arrVoluntary3[j], this.Name, Login._user.idUser) == false)    // 
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                            }
                        }
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                    }
                }
            }


            RadToggleButton btnAllSkills = (RadToggleButton)tabSkills.Controls.Find("btnAllSkills", true)[0];

            if (btnAllSkills.CheckState == CheckState.Checked)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Filled data") != null)
                        btnAllSkills.Text = resxSet.GetString("Filled data");
                    else
                        btnAllSkills.Text = "Filled data";
                }
            }
            else
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("All data") != null)
                        btnAllSkills.Text = resxSet.GetString("All data");
                    else
                        btnAllSkills.Text = "All data";
                }

            }


            RadToggleButton btnSortSkills = (RadToggleButton)tabSkills.Controls.Find("btnSortSkills", true)[0];

            if (btnSortSkills.CheckState == CheckState.Checked)
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Alfabetical sort") != null)
                        btnSortSkills.Text = resxSet.GetString("Alfabetical sort");
                    else
                        btnSortSkills.Text = "Alfabetical sort";
                }
            }
            else
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("Default sort") != null)
                        btnSortSkills.Text = resxSet.GetString("Default sort");
                    else
                        btnSortSkills.Text = "Default sort";
                }
            }
            firstPartSkillsThird();
            tabSkills.Controls.Add(btnAllSkills);   //
            tabSkills.Controls.Add(btnSortSkills);   //
            secondPartSkills();


            Cursor.Current = Cursors.Default;
        }

        private void secondPartSkills()
        {
            List<string> idQueryType = new List<string>();
            
            //List<LabelForArrangement> ArrangementLabel = new List<LabelForArrangement>();
            //ArrangementBUS ab = new ArrangementBUS();
            //ArrangementLabel = ab.GetLabelsArrangement(arrange.idArrangement);
            //if(ArrangementLabel != null)
            //{
            //    foreach(LabelForArrangement m in ArrangementLabel)
            //    {
            //        idQueryType.Add(m.idLabel.ToString());
            //    }
            //}
    
            RadToggleButton btnSortSkills = (RadToggleButton)tabSkills.Controls.Find("btnSortSkills", true)[0];
            RadToggleButton btnAllSkills = (RadToggleButton)tabSkills.Controls.Find("btnAllSkills", true)[0];

            Boolean isDefaultSort = false;

            if (btnSortSkills.CheckState == CheckState.Checked)
                isDefaultSort = true;

            //  Boolean isAll = false;
            Boolean isAll = false;

            if (btnAllSkills.CheckState == CheckState.Checked)
                isAll = true;

            MedicalVoluntaryBUS mvb = new MedicalVoluntaryBUS();
            arrVoluntary3 = mvb.GetVoluntaryForArrangement(idQueryType, arrange.idArrangement, isDefaultSort, isAll);

            //Y
            int lastBottom = 60;
            //X
            int left = 26;
            string oldQuest = "";
            string oldQuestGroup = "";  // Saki

            if (arrVoluntary3 != null)
            {
                if (arrVoluntary3.Count > 0)
                {
                    //create dynamic controls
                    for (int i = 0; i < arrVoluntary3.Count; i++)
                    {
                        string questGroup = ""; // saki
                        string quest = "";
                        string questText = "";
                        string ans = "";
                        string ansText = "";
                        Boolean ischecked = false;
                        //checkbox width
                        int chkWidth = 150;
                        //radiobutton width
                        int rbWidth = 150;
                        //datetimepicker width
                        int dtWidth = 100;
                        //question label width 
                        int rlWidth = (int)(30 * tabSkills.Width / 100) - 20;
                        //answer width (for all controls)
                        int aWidth = (int)(70 * tabSkills.Width / 100) - 80;
                        //question label height 
                        int rlheight = 30;
                        //answer row height  sa 30 na 20 sale
                        int height = 20;

                        if (arrVoluntary3[i].idQuest.ToString() != "")
                        {
                            quest = arrVoluntary3[i].idQuest.ToString().TrimEnd();
                        }
                        if (arrVoluntary3[i].idAns.ToString() != "")
                        {
                            ans = arrVoluntary3[i].idAns.ToString().TrimEnd();
                        }
                        if (arrVoluntary3[i].txtQuest.ToString() != "")
                        {
                            questText = arrVoluntary3[i].txtQuest.ToString().TrimEnd();
                        }
                        if (arrVoluntary3[i].txtAns.ToString() != "")
                        {
                            ansText = arrVoluntary3[i].txtAns.ToString().TrimEnd();
                        }
                        if (arrVoluntary3[i].idArr != null)
                        {
                            ischecked = true;
                        }

                        //====== Saki
                        questGroup = arrVoluntary3[i].nameQuestGroup.TrimEnd();
                        //==========
                        quest = arrVoluntary3[i].idQuest.ToString().TrimEnd();
                        if (arrVoluntary3[i].questSort != null && arrVoluntary3[i].questSort != 0)
                            questSort = Convert.ToDecimal(arrVoluntary3[i].questSort.ToString());
                        if (arrVoluntary3[i].idAns.ToString() != "")
                        {
                            ans = arrVoluntary3[i].idAns.ToString().TrimEnd();
                        }
                        questText = arrVoluntary3[i].txtQuest.ToString().TrimEnd();

                        ansText = arrVoluntary3[i].txtAns.ToString().TrimEnd();
                        if (arrVoluntary3[i].idArr.ToString() != "")
                        {
                            ischecked = true;
                        }


                        //question label
                        RadLabel rl = new RadLabel();
                        RadLabel rlTitle = new RadLabel();  // Saki


                        // ============  ova dva ifa su umesto ORIGINALA ============================
                        if (questGroup.ToUpper() != oldQuestGroup.ToUpper())
                        {
                            rlTitle.Text = questGroup.ToUpper();   //==== Saki
                            rlTitle.Location = new Point(left, lastBottom);
                            rlTitle.Font = new Font("Verdana", 9);
                            rlTitle.ForeColor = Color.DarkOrange;
                            //set multi lines
                            rlTitle.MaximumSize = new Size(rlWidth, height * 3);
                            rlTitle.AutoSize = true;
                            //
                            rlTitle.Width = rlWidth;
                            tabSkills.Controls.Add(rlTitle);
                            //oldQuest = quest;

                            //set rows height depends on number of lines in label question
                            rlheight = height * numberOfLines(rlTitle);
                            lastBottom = lastBottom + rlheight + 5;
                            oldQuestGroup = questGroup;
                        }

                        if (quest != oldQuest)
                        {
                            string s = questSort.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                            string[] parts = s.Split('.');
                            int i1 = int.Parse(parts[0]);
                            int i2 = int.Parse(parts[1]);

                            rl.Text = i1.ToString() + "-" + i2.ToString() + "  " + questText;
                            rl.Location = new Point(left, lastBottom);
                            rl.Font = new Font("Verdana", 9);
                            //set multi lines
                            rl.MaximumSize = new Size(rlWidth, height * 3);
                            rl.AutoSize = true;
                            //
                            rl.Width = rlWidth;
                            tabSkills.Controls.Add(rl);
                            oldQuest = quest;

                            //set rows height depends on number of lines in label question
                            rlheight = height * numberOfLines(rl);
                        }

                        //ANSWER
                        //checkbox type
                        if (arrVoluntary3[i].idAnsType.ToString() == "1")  //prebaceno da prikazuje check + text
                        {
                            RadCheckBox chk = new RadCheckBox();
                            chk.Name = "Q" + quest + "A" + ans;
                            chk.Text = ansText;
                            chk.MaximumSize = new Size(400, 0);
                            chk.MinimumSize = new Size(40, 0);
                            chk.AutoSize = true;
                            chk.Width = chkWidth;
                            chk.Height = height;
                            chk.Font = new Font("Verdana", 9);
                            chk.Location = new Point(left + rlWidth + 20, lastBottom);
                            chk.CheckStateChanged += radCheckBoxVH_CheckStateChanged;
                            tabSkills.Controls.Add(chk);
                            RadTextBox rtb = new RadTextBox();
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Width = 60;
                            rtb.Height = height;
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Location = new Point(left + rlWidth + 20 + chk.Width + 20, lastBottom);
                            rtb.TextChanged += txtTextBoxVH_TextChanged;
                            if (ischecked == true)
                            {
                                chk.CheckState = CheckState.Checked;
                                if (arrVoluntary3[i].txt != null)
                                {
                                    rtb.Text = arrVoluntary3[i].txt.ToString();
                                }

                            }
                            tabSkills.Controls.Add(rtb);


                        }
                        //radio button type
                        else if (arrVoluntary3[i].idAnsType.ToString() == "2")
                        {
                            RadRadioButton rb = new RadRadioButton();
                            rb.Name = "Q" + quest + "A" + ans;
                            rb.Text = ansText;
                            rb.MaximumSize = new Size(400, 0);
                            rb.MinimumSize = new Size(40, 0);
                            rb.AutoSize = true;
                            rb.Width = rbWidth;
                            rb.Height = height;
                            rb.Font = new Font("Verdana", 9);
                            rb.CheckStateChanged += radRadioButtonVH_CheckStateChanged;
                            if (ischecked == true)
                            {
                                rb.CheckState = CheckState.Checked;
                            }

                            RadGroupBox rgb = new RadGroupBox();
                            rgb.MaximumSize = new Size(400, 0);
                            rgb.MinimumSize = new Size(40, 0);
                            rgb.AutoSize = true;
                            if (tabSkills.Controls.Find("Group" + quest, true).Length > 0)
                            {
                                rgb = (RadGroupBox)tabSkills.Controls.Find("Group" + quest, true)[0];
                                rgb.Height = lastBottom - rgb.Location.Y + height;
                                if (rb.Width > rgb.Width)
                                    rgb.Width = rb.Width;
                            }
                            else
                            {
                                rgb.Name = "Group" + quest;
                                ((Telerik.WinControls.Primitives.FillPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).InnerColor = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).ForeColor = System.Drawing.Color.Transparent;
                                tabSkills.Controls.Add(rgb);
                                rgb.Width = rbWidth;
                                rgb.Height = height;
                                rgb.Font = new Font("Verdana", 9);
                                rgb.Location = new Point(left + rlWidth + 20, lastBottom);
                                rgb.Width = rb.Width;
                            }
                            rb.Location = new Point(0, lastBottom - rgb.Location.Y);
                            rgb.Controls.Add(rb);
                        }
                        //checkbox + textbox type
                        else if (arrVoluntary3[i].idAnsType.ToString() == "3")
                        {

                            RadCheckBox chk = new RadCheckBox();
                            chk.Name = "Q" + quest + "A" + ans;
                            chk.Text = ansText;
                            chk.MaximumSize = new Size(400, 0);
                            chk.MinimumSize = new Size(40, 0);
                            chk.AutoSize = true;
                            chk.Width = chkWidth;
                            chk.Height = height;
                            chk.Font = new Font("Verdana", 9);
                            chk.Location = new Point(left + rlWidth + 20, lastBottom);
                            chk.CheckStateChanged += radCheckBoxVH_CheckStateChanged;
                            tabSkills.Controls.Add(chk);
                            RadTextBox rtb = new RadTextBox();
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Width = 60;
                         //   rtb.Width = aWidth - chk.Width - 20;
                            rtb.Height = height;
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Location = new Point(left + rlWidth + 20 + chk.Width + 20, lastBottom);
                            if (ischecked == true)
                            {
                                chk.CheckState = CheckState.Checked;
                                if (arrVoluntary3[i].txt != null)
                                {
                                    rtb.Text = arrVoluntary3[i].txt.ToString();
                                }

                            }
                            tabSkills.Controls.Add(rtb);
                        }
                        //textbox type
                        else if (arrVoluntary3[i].idAnsType.ToString() == "4")
                        {
                            RadTextBox rtb = new RadTextBox();
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Width = aWidth;
                            rtb.Height = height;
                            rtb.Location = new Point(left + rlWidth + 20, lastBottom);
                            rtb.TextChanged += txtTextBoxVH_TextChanged;
                            if (ischecked == true)
                            {
                                if (arrVoluntary3[i].txt != null)
                                {
                                    rtb.Text = arrVoluntary3[i].txt.ToString();
                                }
                            }

                            tabSkills.Controls.Add(rtb);
                        }
                        //radio button + textbox type
                        else if (arrVoluntary3[i].idAnsType.ToString() == "5")
                        {
                            RadRadioButton rb = new RadRadioButton();
                            rb.CheckStateChanged += radRadioButtonVH_CheckStateChanged;
                            rb.Name = "Q" + quest + "A" + ans;
                            rb.Text = ansText;
                            rb.MaximumSize = new Size(400, 0);
                            rb.MinimumSize = new Size(40, 0);
                            rb.AutoSize = true;
                            rb.Width = rbWidth;
                            rb.Height = height;
                            rb.Font = new Font("Verdana", 9);

                            if (ischecked == true)
                            {
                                rb.CheckState = CheckState.Checked;
                            }

                            RadGroupBox rgb = new RadGroupBox();
                            rgb.MaximumSize = new Size(400, 0);
                            rgb.MinimumSize = new Size(40, 0);
                            rgb.AutoSize = true;
                            if (tabSkills.Controls.Find("Group" + quest, true).Length > 0)
                            {
                                rgb = (RadGroupBox)tabSkills.Controls.Find("Group" + quest, true)[0];
                                rgb.Height = lastBottom - rgb.Location.Y + height;
                            }
                            else
                            {
                                rgb.Name = "Group" + quest;
                                ((Telerik.WinControls.Primitives.FillPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).InnerColor = System.Drawing.Color.Transparent;
                                ((Telerik.WinControls.Primitives.BorderPrimitive)(rgb.GetChildAt(0).GetChildAt(0).GetChildAt(1))).ForeColor = System.Drawing.Color.Transparent;
                                tabSkills.Controls.Add(rgb);
                                rgb.Width = rbWidth;
                                rgb.Height = height;
                                rgb.Font = new Font("Verdana", 9);
                                rgb.Location = new Point(left + rlWidth + 20, lastBottom);
                            }

                            rb.Location = new Point(0, lastBottom - rgb.Location.Y);
                            //rgb.Width = 50;
                            rgb.Controls.Add(rb);

                            if (tabSkills.Controls.Find("Group" + quest, true).Length > 0)
                            {
                                if (rb.Width > rgb.Width)
                                    rgb.Width = rb.Width;
                            }
                            else
                            {
                                rgb.Width = rb.Width;
                            }

                            RadTextBox rtb = new RadTextBox();
                            rtb.Font = new Font("Verdana", 9);
                            rtb.Name = "txtQ" + quest + "A" + ans;
                            rtb.Width = aWidth - rb.Width - 20;
                            rtb.Height = height;
                            rtb.Location = new Point(left + rlWidth + 20 + rb.Width + 20, lastBottom);

                            rtb.TextChanged += txtTextBoxVH_TextChanged;
                            if (ischecked == true)
                            {
                                rb.CheckState = CheckState.Checked;
                                if (arrVoluntary3[i].txt != null)
                                {
                                    rtb.Text = arrVoluntary3[i].txt.ToString();
                                }

                            }
                            tabSkills.Controls.Add(rtb);
                        }
                        //checkbox + textbox + datetime
                        else if (arrVoluntary3[i].idAnsType.ToString() == "6")
                        {

                        }

                        lastBottom = lastBottom + rlheight + 5;
                    }
                }
            }
            isVolChanged = false;
        }

        private void radCheckBoxVH_CheckStateChanged(object sender, EventArgs e)
        {
            RadCheckBox chk = (RadCheckBox)sender;
            string txtName = "txt" + chk.Name;
            RadTextBox rtb = new RadTextBox();
            rtb.Name = txtName;
            if (chk.CheckState != CheckState.Checked)
            {
                if (this.Controls.Find(txtName, true) != null)
                    if (this.Controls.Find(txtName, true).Length > 0)
                    {
                        rtb = (RadTextBox)this.Controls.Find(txtName, true)[0];
                        rtb.Text = "";
                        Control[] dt = this.Controls.Find(txtName.Replace("txt", "dt"), true);
                        if (dt.Length > 0)
                        {
                            RadDateTimePicker rdt = (RadDateTimePicker)dt[0];
                            rdt.Value = DateTime.Now;
                        }
                    }
            }
            isVolChanged = true;
        }

        private void txtTextBoxVH_TextChanged(object sender, EventArgs e)
        {
            RadTextBox txtb = (RadTextBox)sender;
            string txtName = txtb.Name;
            txtName = txtName.Replace("txt", "");
            RadCheckBox chk = new RadCheckBox();
            chk.Name = txtName;

            chk = (RadCheckBox)this.Controls.Find(txtName, true)[0];
            if (chk.Checked == false)
                chk.Checked = true;

            isVolChanged = true;

        }

        private void radRadioButtonVH_CheckStateChanged(object sender, EventArgs e)
        {
            RadRadioButton rrb = (RadRadioButton)sender;
            string txtName = "txt" + rrb.Name;
            RadTextBox rtb = new RadTextBox();
            rtb.Name = txtName;
            if (rrb.CheckState != CheckState.Checked)
            {
                rtb = (RadTextBox)this.Controls.Find(txtName, true)[0];
                rtb.Text = "";
            }

            isVolChanged = true;
        }

        private void radPageVH_SelectedPageChanging(object sender, RadPageViewCancelEventArgs e)
        {
            RadPageView rpvVV = (RadPageView)sender;
            string sName2 = ((RadPageView)sender).SelectedPage.Name;
            if (isVolChanged == true)
            {
                if (arrange.idArrangement != 0)
                {
                    MedicalVoluntaryBUS vtb = new MedicalVoluntaryBUS();
                    if (sName2 == "tabSkills")
                    {
                        saveSkills();
                        if (vtb.DeleteVoluntaryArrangement(arrange.idArrangement, this.Name, Login._user.idUser) == true)
                        {
                            for (int j = 0; j < arrVoluntary3.Count; j++)
                            {
                                if (vtb.SaveVoluntaryArrangement(arrVoluntary3[j], this.Name, Login._user.idUser) == false)
                                {
                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                                }

                            }
                        }
                        else
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                        }
                    }
                    if (sName2 == "tabTrips")
                    {
                        VolontaryTripBUS vtb2 = new VolontaryTripBUS();
                        saveVoluntaryTrip();
                        if (vtb2.DeleteVoluntaryArrangement(arrange.idArrangement, this.Name, Login._user.idUser) == true)
                        {
                            for (int j = 0; j < arrVoluntary2.Count; j++)
                            {
                                if (vtb2.SaveArrangement(arrVoluntary2[j], this.Name, Login._user.idUser) == false)
                                {
                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                                }

                            }
                        }
                        else
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                        }
                    }
                    if (sName2 == "tabFunction")
                    {
                        VolontaryFunctionBUS mvb = new VolontaryFunctionBUS();
                        saveVoluntaryFunction();
                        if (mvb.DeleteVoluntaryArrangement(arrange.idArrangement, this.Name, Login._user.idUser) == true)
                        {
                            for (int j = 0; j < arrVoluntary1.Count; j++)
                            {
                                if (mvb.SaveArrangement(arrVoluntary1[j], this.Name, Login._user.idUser) == false)    // 
                                {
                                    translateRadMessageBox tr = new translateRadMessageBox();
                                    tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                                }
                            }
                        }
                        else
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("You have not succesufully inserted voluntary data. Please check!");
                        }
                    }
                }
            }
            if (isVolChanged == true)
                isVolChanged = false;
        }

               
    }
}
