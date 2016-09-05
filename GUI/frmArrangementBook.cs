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
using NUnit.Framework;
using Telerik.WinControls.Data;
using System.Linq;

namespace GUI
{
    [TestFixture]
    public partial class frmArrangementBook : frmTemplate
    {
        ArrangementModel arrange;
        ArrangementBookModel arrBookModel;
        ArrangementRemainingModel remainingModel;
        List<LabelForArrangement> ArrangementLabel;
        public int insLabel;
        public int iID = -1;

        List<ArrangementThemeTripModel> arrangeThemeTrip = new List<ArrangementThemeTripModel>();
        List<ArrangementBoardingPointModel> arrangeBoardingPoint = new List<ArrangementBoardingPointModel>();
        List<ArrangementTargetGroupModel> arrangeTargetGroup = new List<ArrangementTargetGroupModel>();

        int numberColumnsFunc = 0;
        int numberUntilColumnsFunc = 0;
        int numColumnsSkills = 0;
        int numberUntilColumnsSkills = 0;


        string txtRolator = "";
        string txtRolstool = "";
        //string txtArmAlt = "";
        string txtArmAfa = "";
        string txtAnchorage = "";

        bool chkIfExpired = false;

        public frmArrangementBook(ArrangementModel model)
        {
            arrange = model;
            arrBookModel = new ArrangementBookModel();
            arrBookModel.idArrangement = arrange.idArrangement;
            InitializeComponent();
            btnAddTraveler.Click += btnAddTraveler_Click;
            btnAddVoluntary.Click += btnAddVoluntary_Click;
            btnDeleteTraveler.Click += btnDeleteTraveler_Click;
            btnCancelTraveler.Click += btnCancelTraveler_Click;
            btnRibbonTravelpapers.Click += btnRibbonTravelpapers_Click;

            this.Icon = Login.iconForm;
            string formName = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();
            formName = formName + " " + model.nameArrangement;
            this.Text = formName;

            //loadPurchase();
            loadTrip();
            LoadTargetGroup();
            LoadThemeTrip();
          //  loadRemaining()
            ArrangementBookBUS arrBUS = new ArrangementBookBUS();
            chkIfExpired = arrBUS.chkMinDatePriceList(arrange.idArrangement);
        }


        static int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }
        private void loadTrip()
        {
            try
            {
                if (arrange.idArrangement > 0)
                {
                    lblSttatus.Text = arrange.statusArrangement;
                    lblHootelServices.Text = arrange.nameHotelService.ToString();
                    lblAggeCateogory.Text = arrange.descAgeCategory.ToString();
                    if (arrange.isWeb == true)
                        chkWeb.CheckState = CheckState.Checked;
                    else chkWeb.Visible = false;
                    RadCheckBox rchk;
                    int Y = 0;
                    List<RadCheckBox> lista = new List<RadCheckBox>();
                    for (int i = 0; i < Login._arrLabels.Count; i++)
                    {
                        rchk = new RadCheckBox();
                        rchk.Font = new Font("Verdana", 9);
                        //  rchk.ThemeName = radPageArrange.ThemeName;
                        rchk.Name = "chkLabel" + Login._arrLabels[i].idLabel.ToString();
                        rchk.Text = Login._arrLabels[i].nameLabel;
                        rchk.Location = new Point(0, Y);
                        rchk.AutoSize = true;
                        Y = Y + 3 + rchk.Height;
                        panelLabels.Controls.Add(rchk);
                        lista.Add(rchk);
                        rchk.Visible = false;

                    }
                    ArrangementLabel = new List<LabelForArrangement>();
                    ArrangementBUS ab = new ArrangementBUS();
                    ArrangementLabel = ab.GetLabelsArrangement(arrange.idArrangement);

                    for (int i = 0; i < ArrangementLabel.Count; i++)
                    {

                        RadCheckBox chk = (RadCheckBox)Controls.Find("chkLabel" + ArrangementLabel[i].idLabel.ToString(), true)[0];
                        if (ArrangementLabel[i].idLabel.ToString() != "")
                            insLabel = ArrangementLabel[i].idLabel;
                        chk.CheckState = CheckState.Checked;
                        // lista[insLabel-1].Visible = true;
                        // lista[insLabel - 1].ReadOnly = true;

                    }

                    //=== part for medical
                    txtRolator = arrange.nrMaximumWheelchairs.ToString();
                    txtRolstool = arrange.whoseElectricWheelchairs.ToString();
                    txtArmAfa = arrange.buSupportingArms.ToString();
                    txtAnchorage = arrange.nrAnchorage.ToString();

                    ArrangementBookBUS arbus = new ArrangementBookBUS();
                    //Rollator
                    int fld1 = arbus.GetBookPersMedicMoreAns(new List<int> { 446, 447, 448 }, arrange.idArrangement);
                    lbl2.Text = fld1.ToString();

                    //Rolstoel
                    int fld2 = arbus.GetBookPersMedicMoreAns(new List<int> { 441, 442, 449, 450, 451, 452, 453 }, arrange.idArrangement);
                    lbl1.Text = fld2.ToString();

                    //Arm sometimes
                    int fld4 = arbus.GetBookPersMedicMoreAns(new List<int> { 439, 440 }, arrange.idArrangement);
                    lbl4.Text = fld4.ToString();

                    //Anchorage
                    int fldAnchorage = arbus.GetBookPersMedicMoreAns(new List<int> { 823}, arrange.idArrangement);

                    maskedBookedAnchorage.Text = fldAnchorage.ToString();


                    //====================

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        //private void loadBooking()
        // { 
        //     PersonBUS pb = new PersonBUS();
        //     rgvPerson.DataSource = pb.GetAllPersonsForArrangement(arrBookModel.idArrangement, Login._user.lngUser);
        // }

        private void loadBooking()
        {
            PersonBUS pb = new PersonBUS();
            rgvPerson.DataSource = null;
            rgvPerson.Columns.Clear();
            rgvPerson.DataSource = pb.GetAllPersonsForArrangement(arrBookModel.idArrangement, Login._user.lngUser);

        }
        private void LoadTargetGroup()
        {
            getCheckedTargetGroup();

        }
        private void LoadThemeTrip()
        {
            getCheckedThemeTrip();

        }


        private void loadRooms()
        {
            rgvRooms.DataSource = null;
            rgvRooms.Columns.Clear();
            rgvRooms.DataSource = new ArticalBUS().GetAllArticalsRoomsForArrangement(arrBookModel.idArrangement, Login._user.lngUser);

        }


        private void getCheckedTargetGroup()
        {
            panelTargetGroup.Controls.Clear();
            int lastBottom = 15;

            if (arrangeTargetGroup.Count == 0)
            {
                ArrangementTargetGroupBUS abpb = new ArrangementTargetGroupBUS();
                List<TargetGroupModel> arrTargetGroup = new List<TargetGroupModel>();
                arrTargetGroup = abpb.GetArrangementTargetGroup(arrange.idArrangement);


                if (arrTargetGroup != null)
                    if (arrTargetGroup.Count > 0)
                        for (int i = 0; i < arrTargetGroup.Count; i++)
                        {
                            ArrangementTargetGroupModel abpm = new ArrangementTargetGroupModel();
                            abpm.idTargetGroup = arrTargetGroup[i].idTargetGroup;
                            abpm.idArrangement = arrange.idArrangement;
                            arrangeTargetGroup.Add(abpm);

                            RadCheckBox chk = new RadCheckBox();
                            chk.Font = new Font("Verdana", 9);
                            chk.CheckState = CheckState.Checked;
                            //   chk.CheckStateChanged += radCheckBoxTG_CheckStateChanged;
                            chk.Name = arrTargetGroup[i].idTargetGroup.ToString();
                            chk.Text = arrTargetGroup[i].nameTargetGroup.ToString() + "(" + arrTargetGroup[i].shortcutTargeGroup.Trim() + ")";
                            chk.Location = new Point(15, lastBottom);
                            chk.ReadOnly = true;
                            lastBottom = lastBottom + 20;
                            panelTargetGroup.Controls.Add(chk);
                        }
            }
            else
            {
                if (arrangeTargetGroup.Count > 0)
                    for (int i = 0; i < arrangeTargetGroup.Count; i++)
                    {

                        RadCheckBox chk = new RadCheckBox();
                        chk.Font = new Font("Verdana", 9);
                        chk.CheckState = CheckState.Checked;
                        //  chk.CheckStateChanged += radCheckBoxTG_CheckStateChanged;
                        chk.Name = arrangeTargetGroup[i].idTargetGroup.ToString();
                        chk.Text = new ArrangementTargetGroupBUS().GetTargetGroupName(arrangeTargetGroup[i].idTargetGroup);
                        chk.Location = new Point(15, lastBottom);
                        chk.ReadOnly = true;
                        lastBottom = lastBottom + 20;
                        panelTargetGroup.Controls.Add(chk);
                    }
            }
        }
        private void getCheckedThemeTrip()
        {
            panelThemeTrip.Controls.Clear();
            int lastBottom = 15;

            if (arrangeThemeTrip.Count == 0)
            {
                ArrangementThemeTripBUS abpb = new ArrangementThemeTripBUS();
                List<ThemeTripModel> arrThemeTrip = new List<ThemeTripModel>();
                arrThemeTrip = abpb.GetArrangementThemeTrip(arrange.idArrangement);


                if (arrThemeTrip != null)
                    if (arrThemeTrip.Count > 0)
                        for (int i = 0; i < arrThemeTrip.Count; i++)
                        {
                            ArrangementThemeTripModel abpm = new ArrangementThemeTripModel();
                            abpm.idThemeTrip = arrThemeTrip[i].idThemeTrip;
                            abpm.idArrangement = arrange.idArrangement;
                            arrangeThemeTrip.Add(abpm);
                            RadCheckBox chk = new RadCheckBox();
                            chk.Font = new Font("Verdana", 9);
                            chk.CheckState = CheckState.Checked;
                            //  chk.CheckStateChanged += radCheckBoxTT_CheckStateChanged;
                            chk.Name = arrThemeTrip[i].idThemeTrip.ToString();
                            chk.Text = arrThemeTrip[i].nameThemeTrip.ToString();
                            chk.Location = new Point(15, lastBottom);
                            chk.ReadOnly = true;
                            lastBottom = lastBottom + 20;
                            panelThemeTrip.Controls.Add(chk);
                        }
            }
            else
            {
                if (arrangeThemeTrip.Count > 0)
                    for (int i = 0; i < arrangeThemeTrip.Count; i++)
                    {

                        RadCheckBox chk = new RadCheckBox();
                        chk.Font = new Font("Verdana", 9);
                        chk.CheckState = CheckState.Checked;
                        //   chk.CheckStateChanged += radCheckBoxTT_CheckStateChanged;
                        chk.Name = arrangeThemeTrip[i].idThemeTrip.ToString();
                        chk.Text = new ArrangementThemeTripBUS().GetThemeTripName(arrangeThemeTrip[i].idThemeTrip);
                        chk.Location = new Point(15, lastBottom);
                        chk.ReadOnly = true;
                        lastBottom = lastBottom + 20;
                        panelThemeTrip.Controls.Add(chk);
                    }
            }
        }

        private void btnDeleteTraveler_Click(object sender, EventArgs e)
        {
            //if (chkIfExpired)
            //{
            //    translateRadMessageBox trr = new translateRadMessageBox();
            //    trr.translateAllMessageBox("Not possible to book, releasedate from contract is expired!");
            //    return;
            //}
            PersonBookModel selectedModel = (PersonBookModel)rgvPerson.CurrentRow.DataBoundItem;
            if (rgvPerson != null)
            {
                if (rgvPerson.SelectedRows != null)
                {
                    if (rgvPerson.SelectedRows.Count > 0)
                    {
                        if (rgvPerson.SelectedRows[0].Cells["type"].Value.ToString() == "Traveler")
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            if (tr.translateAllMessageBoxDialog("Are you sure you want delete this traveller?", "Delete traveler") == System.Windows.Forms.DialogResult.Yes)
                            {
                                if (rgvPerson.SelectedRows[0].Cells["idStatus"].Value.ToString() == "4")
                                {
                                    ArrangementBookBUS abb = new ArrangementBookBUS();
                                    int person = Convert.ToInt32(rgvPerson.SelectedRows[0].Cells["idContPers"].Value.ToString());
                                    int arrangement = arrBookModel.idArrangement;
                                    if (abb.checkCancelInInvoiseFinal(arrangement, person) == true)
                                    {
                                        tr.translateAllMessageBox("Can't delete canceled person with invoice!");
                                        return;
                                    }
                                }
                                ArrangementBookBUS ab = new ArrangementBookBUS();
                                int idContPers = Convert.ToInt32(rgvPerson.SelectedRows[0].Cells["idContPers"].Value.ToString());
                                int idArr = arrBookModel.idArrangement;
                                if (ab.checkFinal(selectedModel.idArrangementBook) == false)
                                {
                                    translateRadMessageBox trr = new translateRadMessageBox();
                                    trr.translateAllMessageBox("You can't delete arrangement book if it's final!");
                                }
                                else if (ab.checkIsInTravelers(selectedModel.idArrangementBook) != "")
                                {
                                    translateRadMessageBox trr = new translateRadMessageBox();
                                    trr.translatePartAndNonTranslatedPart("You can't delete arrangement book because this person is added as traveler with ", ab.checkIsInTravelers(selectedModel.idArrangementBook));
                                }
                                else
                                {
                                    DataTable dt = ab.checkIfPersonsIsExtraAndStatus(selectedModel.idArrangementBook,idArr);
                                    int idStatus = 0;
                                    string nameContPers = "";
                                    if (dt != null)
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            if (dt.Rows[0]["idStatus"].ToString() != "")
                                                idStatus = Convert.ToInt32(dt.Rows[0]["idStatus"].ToString());
                                            if (dt.Rows[0]["nameContPers"].ToString() != "")
                                                nameContPers = dt.Rows[0]["nameContPers"].ToString();
                                        }
                                    }
                                    if (ab.checkIfPersonsHasExtraAndStatus(selectedModel.idArrangementBook) == true)
                                    {
                                        translateRadMessageBox trr = new translateRadMessageBox();
                                        trr.translateAllMessageBox("First you have to delete all extra persons for this booking!");
                                    }
                                    else if (idStatus == 1)
                                    {
                                        translateRadMessageBox trr = new translateRadMessageBox();
                                        trr.translatePartAndNonTranslatedPart("First you have to delete this person as extra person for other optional booking on this arrangement of", nameContPers);
                                    }
                                    else if (idStatus == 2)
                                    {
                                        translateRadMessageBox trr = new translateRadMessageBox();
                                        trr.translatePartAndNonTranslatedPart("You can't delete this person because it's extra person for other final booking on this arrangement of", nameContPers);
                                    }
                                    //else if (new ArrangementBookBUS().checkIfArrangementHasAnythingExtraAndNotFinal(idArr, idContPers) == true)
                                    //{
                                    else if (ab.DeleteBookingIfNotFinal(selectedModel.idArrangementBook, this.Name, Login._user.idUser) == true)
                                        {
                                            updateStatus();
                                            rgvPerson.DataSource = null;
                                            rgvPerson.Columns.Clear();
                                            PersonBUS pb = new PersonBUS();
                                            rgvPerson.DataSource = pb.GetAllPersonsForArrangement(arrBookModel.idArrangement, Login._user.lngUser);
                                            fillData();

                                            //==== ponovo ucitava book-ovane za kolica i ostalo
                                            ArrangementBookBUS arbus = new ArrangementBookBUS();
                                            int fld1 = arbus.GetBookPersMedicMoreAns(new List<int> { 446, 447, 448 }, arrange.idArrangement);
                                            lbl2.Text = fld1.ToString();
                                            int fld2 = arbus.GetBookPersMedicMoreAns(new List<int> { 441, 442, 449, 450, 451, 452, 453 }, arrange.idArrangement);
                                            lbl1.Text = fld2.ToString();
                                            int fld4 = arbus.GetBookPersMedicMoreAns(new List<int> { 439, 440 }, arrange.idArrangement);
                                            lbl4.Text = fld4.ToString();


                                            int fldAnchorage = arbus.GetBookPersMedicMoreAns(new List<int> { 823 }, arrange.idArrangement);

                                            maskedBookedAnchorage.Text = fldAnchorage.ToString();
                                        
                                        recalculateVolArr(arrBookModel.idArrangement, selectedModel.idContPers);



                                            //==================
                                        }
                                        else
                                        {
                                            translateRadMessageBox trr = new translateRadMessageBox();
                                            trr.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                                        }
                                    //}
                                    //else
                                    //{
                                    //    translateRadMessageBox trr = new translateRadMessageBox();
                                    //    trr.translateAllMessageBox("First you have to delete all extra persons or extra articles!");
                                    //}
                                }
                            }
                        }
                        else
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            if (tr.translateAllMessageBoxDialog("Are you sure you want delete this voluntary?", "Delete voluntary") == System.Windows.Forms.DialogResult.Yes)
                            {
                                if (rgvPerson.SelectedRows[0].Cells["idStatus"].Value.ToString() == "4")
                                {
                                    ArrangementBookBUS abb = new ArrangementBookBUS();
                                    int person = Convert.ToInt32(rgvPerson.SelectedRows[0].Cells["idContPers"].Value.ToString());
                                    int arrangement = arrBookModel.idArrangement;
                                    if (abb.checkCancelInInvoiseFinal(arrangement, person) == true)
                                    {
                                        tr.translateAllMessageBox("Can't delete canceled person with invoice!");
                                        return;
                                    }
                                }
                                ArrangementBookBUS ab = new ArrangementBookBUS();
                                int idContPers = Convert.ToInt32(rgvPerson.SelectedRows[0].Cells["idContPers"].Value.ToString());
                                int idArr = arrBookModel.idArrangement;
                                if (ab.checkFinal(selectedModel.idArrangementBook) == false)
                                {
                                    translateRadMessageBox trr = new translateRadMessageBox();
                                    trr.translateAllMessageBox("You can't delete arrangement book if it's final!");
                                }
                                else
                                {
                                    DataTable dt = ab.checkIfPersonsIsExtraAndStatus(selectedModel.idArrangementBook, idArr);
                                    int idStatus = 0;
                                    string nameContPers = "";
                                    if (dt != null)
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            if (dt.Rows[0]["idStatus"].ToString() != "")
                                                idStatus = Convert.ToInt32(dt.Rows[0]["idStatus"].ToString());
                                            if (dt.Rows[0]["nameContPers"].ToString() != "")
                                                nameContPers = dt.Rows[0]["nameContPers"].ToString();
                                        }
                                    }
                                    if (ab.checkIfPersonsHasExtraAndStatus(selectedModel.idArrangementBook) == true)
                                    {
                                        translateRadMessageBox trr = new translateRadMessageBox();
                                        trr.translateAllMessageBox("First you have to delete all extra persons for this booking!");
                                    }
                                    else if (idStatus == 1)
                                    {
                                        translateRadMessageBox trr = new translateRadMessageBox();
                                        trr.translatePartAndNonTranslatedPart("First you have to delete this person as extra person for other optional booking on this arrangement of", nameContPers);
                                    }
                                    else if (idStatus == 2)
                                    {
                                        translateRadMessageBox trr = new translateRadMessageBox();
                                        trr.translatePartAndNonTranslatedPart("You can't delete this person because it's extra person for other final booking on this arrangement of", nameContPers);
                                    }
                                    //else if (new ArrangementBookBUS().checkIfArrangementHasAnythingExtraAndNotFinal(idArr, idContPers) == true)
                                    //{
                                    else if (ab.DeleteBookingIfNotFinal(selectedModel.idArrangementBook, this.Name, Login._user.idUser) == true)
                                    {
                                        updateStatus();
                                        ArrangementBookBUS bus = new ArrangementBookBUS();
                                        bool deleted = bus.DeleteVolLookup(arrBookModel.idArrangement, idContPers, this.Name, Login._user.idUser);

                                        rgvPerson.DataSource = null;
                                        rgvPerson.Columns.Clear();
                                        PersonBUS pb = new PersonBUS();
                                        rgvPerson.DataSource = pb.GetAllPersonsForArrangement(arrBookModel.idArrangement, Login._user.lngUser);

                                    }
                                    else
                                    {
                                        translateRadMessageBox trr = new translateRadMessageBox();
                                        trr.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    translateRadMessageBox trr = new translateRadMessageBox();
                                        //    trr.translateAllMessageBox("First you have to delete all extra persons or extra articles!");
                                        //}
                                    }
                                }
                            }
                        }
                    }
                }

            }
        }

        private void btnRibbonTravelpapers_Click(object sender, EventArgs e)
        {
            using(frmArrangementTravelPapers frm = new frmArrangementTravelPapers(arrange))
            { 
                frm.ShowDialog();
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
        
        private void btnCancelTraveler_Click(object sender, EventArgs e)
        {
            //if (chkIfExpired)
            //{
            //    translateRadMessageBox trr = new translateRadMessageBox();
            //    trr.translateAllMessageBox("Not possible to book, releasedate from contract is expired!");
            //    return;
            //}
            if (rgvPerson != null)
            {
                if (rgvPerson.SelectedRows != null)
                {
                    if (rgvPerson.SelectedRows.Count > 0 && rgvPerson.CurrentRow.DataBoundItem != null)
                    {
                        PersonBookModel selectedModel = (PersonBookModel)rgvPerson.CurrentRow.DataBoundItem;
                        CancelArrangement ca = new CancelArrangement();
                        if (ca.cancel(selectedModel, arrange, this.Name, Login._user.idUser) == true)
                        {
                            updateStatus();
                            rgvPerson.DataSource = null;
                            PersonBUS pb = new PersonBUS();
                            rgvPerson.Columns.Clear();
                            rgvPerson.DataSource = pb.GetAllPersonsForArrangement(arrBookModel.idArrangement, Login._user.lngUser);
                        }
                    }
                }
            }
        }

        

        private void btnAddVoluntary_Click(object sender, EventArgs e)
        {
           
            //if (arrange.statusArrangement.ToUpper() == "CXD")
            //{
            //    translateRadMessageBox trr = new translateRadMessageBox();
            //    trr.translateAllMessageBox("Not possible to book, because status of  trip is cxd!");
            //    return;
            //}
            //else
            //{
                if (chkIfExpired)
                {
                    translateRadMessageBox trr = new translateRadMessageBox();
                    trr.translateAllMessageBox("Not possible to book, releasedate from contract is expired!");
                    return;
                }
                if (arrange.nrVoluntaryHelper > 0)
                {
                    int nrPerson = 0;
                    if (rgvPerson != null)
                    {
                        if (rgvPerson.Rows.Count > 0)
                        {
                            //nrPerson = rgvPerson.Rows.Count;
                            for (int i = 0; i < rgvPerson.Rows.Count; i++)
                                if (rgvPerson.Rows[i].Cells["type"].Value.ToString().Trim() == "Voluntary helper" && Convert.ToInt32(rgvPerson.Rows[i].Cells["idStatus"].Value.ToString()) != 3
                                    && Convert.ToInt32(rgvPerson.Rows[i].Cells["idStatus"].Value.ToString()) != 4)
                                    nrPerson = nrPerson + 1;
                        }
                    }
                    if (nrPerson < arrange.nrVoluntaryHelper)
                    {
                        using (frmArrangementBookingPerson_VH frm = new frmArrangementBookingPerson_VH(arrBookModel, true))
                        {
                            frm.ShowDialog();
                            if (frm.DialogResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                rgvPerson.DataSource = null;
                                rgvPerson.Columns.Clear();
                                PersonBUS pb = new PersonBUS();
                                arrBookModel = new ArrangementBookModel();
                                arrBookModel.idArrangement = arrange.idArrangement;
                                rgvPerson.DataSource = pb.GetAllPersonsForArrangement(arrBookModel.idArrangement, Login._user.lngUser);
                            }
                        }

                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("There is no more booking for this arrangement so you can only reserve!");

                        using (frmArrangementBookingPerson_VH frm = new frmArrangementBookingPerson_VH(arrBookModel, true, true))
                        {
                            frm.ShowDialog();
                        }
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();

                    }
                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You have to have number of voluntary helpers so you could add them!");
                }
           // }
        }
        private void btnAddTraveler_Click(object sender, EventArgs e)
        {
         
            if (arrange.statusArrangement.ToUpper() == "CXD")
            {
                translateRadMessageBox trr = new translateRadMessageBox();
                trr.translateAllMessageBox("Not possible to book, because status of  trip is cxd!");
                return;
            }
            else
            {
                if (chkIfExpired)
                {
                    translateRadMessageBox trr = new translateRadMessageBox();
                    trr.translateAllMessageBox("Not possible to book, releasedate from contract is expired!");
                    return;
                }
                if (arrange.nrTraveler > 0)
                {
                    int nrPerson = 0;
                    if (rgvPerson != null)
                    {
                        if (rgvPerson.Rows.Count > 0)
                        {
                            for (int i = 0; i < rgvPerson.Rows.Count; i++)
                                if (rgvPerson.Rows[i].Cells["type"].Value.ToString() == "Traveler" && Convert.ToInt32(rgvPerson.Rows[i].Cells["idStatus"].Value.ToString()) != 3
                                    && Convert.ToInt32(rgvPerson.Rows[i].Cells["idStatus"].Value.ToString()) != 4)
                                    nrPerson = nrPerson + 1;
                        }
                    }
                    arrBookModel = new ArrangementBookModel();
                    arrBookModel.idArrangement = arrange.idArrangement;

                    if (nrPerson >= arrange.nrTraveler)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("There is no more booking for this arrangement so you can only reserve!");

                        using (frmArrangementBookingPerson frm = new frmArrangementBookingPerson(arrBookModel, Convert.ToInt32(txtRolator), Convert.ToInt32(txtRolstool), Convert.ToInt32(txtArmAfa), Convert.ToInt32(txtAnchorage), true, true))
                        {
                            frm.ShowDialog();
                            if (frm.DialogResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                rgvPerson.DataSource = null;
                                rgvPerson.Columns.Clear();
                                PersonBUS pb = new PersonBUS();
                                rgvPerson.DataSource = pb.GetAllPersonsForArrangement(arrBookModel.idArrangement, Login._user.lngUser);
                                fillData();
                            }
                        }
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();

                    }
                    else
                    {
                        using (frmArrangementBookingPerson frm = new frmArrangementBookingPerson(arrBookModel, Convert.ToInt32(txtRolator), Convert.ToInt32(txtRolstool), Convert.ToInt32(txtArmAfa), Convert.ToInt32(txtAnchorage), true, false))
                        {
                            frm.ShowDialog();
                            if (frm.DialogResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                rgvPerson.DataSource = null;
                                rgvPerson.Columns.Clear();
                                PersonBUS pb = new PersonBUS();
                                rgvPerson.DataSource = pb.GetAllPersonsForArrangement(arrBookModel.idArrangement, Login._user.lngUser);
                                fillData();
                            }
                        }
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();
                    }
                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You have to have number of travelers so you could add them!");
                }
            }
            //==== ponovo ucitava book-ovane za kolica i ostalo
            ArrangementBookBUS arbus = new ArrangementBookBUS();
            int fld1 = arbus.GetBookPersMedicMoreAns(new List<int> { 446, 447, 448 }, arrange.idArrangement);
            lbl2.Text = fld1.ToString();
            int fld2 = arbus.GetBookPersMedicMoreAns(new List<int> { 441, 442, 449, 450, 451, 452, 453 }, arrange.idArrangement);
            lbl1.Text = fld2.ToString();
            int fld4 = arbus.GetBookPersMedicMoreAns(new List<int> { 439, 440 }, arrange.idArrangement);
            lbl4.Text = fld4.ToString();


            int fldAnchorage = arbus.GetBookPersMedicMoreAns(new List<int> { 823 }, arrange.idArrangement);

            maskedBookedAnchorage.Text = fldAnchorage.ToString();


            //==================
        }
        private void setTranslation()
        {

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblArrId.Text) != null)
                    lblArrId.Text = resxSet.GetString(lblArrId.Text);
                if (resxSet.GetString(lblArrCode.Text) != null)
                    lblArrCode.Text = resxSet.GetString(lblArrCode.Text);
                if (resxSet.GetString(lblArrName.Text) != null)
                    lblArrName.Text = resxSet.GetString(lblArrName.Text);
                if (resxSet.GetString(lblDateFrom.Text) != null)
                    lblDateFrom.Text = resxSet.GetString(lblDateFrom.Text);
                if (resxSet.GetString(lblDateTo.Text) != null)
                    lblDateTo.Text = resxSet.GetString(lblDateTo.Text);
                if (resxSet.GetString(lblCity.Text) != null)
                    lblCity.Text = resxSet.GetString(lblCity.Text);
                if (resxSet.GetString(lblCountry.Text) != null)
                    lblCountry.Text = resxSet.GetString(lblCountry.Text);
                if (resxSet.GetString(lblArrType.Text) != null)
                    lblArrType.Text = resxSet.GetString(lblArrType.Text);
                if (resxSet.GetString(btnSave.Text) != null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);
                if (resxSet.GetString(lblNrTravelers.Text) != null)
                    lblNrTravelers.Text = resxSet.GetString(lblNrTravelers.Text);
                if (resxSet.GetString(lblMinNrTravelers.Text) != null)
                    lblMinNrTravelers.Text = resxSet.GetString(lblMinNrTravelers.Text);
                if (resxSet.GetString(lblNrVoluntaryHelpers.Text) != null)
                    lblNrVoluntaryHelpers.Text = resxSet.GetString(lblNrVoluntaryHelpers.Text);
                if (resxSet.GetString(lblNrBookTravelers.Text) != null)
                    lblNrBookTravelers.Text = resxSet.GetString(lblNrBookTravelers.Text);
                if (resxSet.GetString(lblNrBookVH.Text) != null)
                    lblNrBookVH.Text = resxSet.GetString(lblNrBookVH.Text);
                if (resxSet.GetString(lblNrBookTotal.Text) != null)
                    lblNrBookTotal.Text = resxSet.GetString(lblNrBookTotal.Text);
                if (resxSet.GetString(lblTotal.Text) != null)
                    lblTotal.Text = resxSet.GetString(lblTotal.Text);
                // tab trip info
                if (resxSet.GetString(lblStatus.Text) != null)
                    lblStatus.Text = resxSet.GetString(lblStatus.Text);
                if (resxSet.GetString(lblAgeCateogory.Text) != null)
                    lblAgeCateogory.Text = resxSet.GetString(lblAgeCateogory.Text);
                if (resxSet.GetString(lblHotelService.Text) != null)
                    lblHotelService.Text = resxSet.GetString(lblHotelService.Text);
                if (resxSet.GetString(chkWeb.Text) != null)
                    chkWeb.Text = resxSet.GetString(chkWeb.Text);
                //

                if (resxSet.GetString(btnTgAll.Text) != null)
                    btnTgAll.Text = resxSet.GetString(btnTgAll.Text);
                if (resxSet.GetString(btnAll.Text) != null)
                    btnAll.Text = resxSet.GetString(btnAll.Text);
                if (resxSet.GetString(btnTgAll.Text) != null)
                    btnTgAll.Text = resxSet.GetString(btnTgAll.Text);
                if (resxSet.GetString(lblNrMaleHelpers.Text) != null)
                    lblNrMaleHelpers.Text = resxSet.GetString(lblNrMaleHelpers.Text);
             
                if (resxSet.GetString(btnAddTraveler.Text) != null)
                    btnAddTraveler.Text = resxSet.GetString(btnAddTraveler.Text);
             
                if (resxSet.GetString(btnDeleteTraveler.Text) != null)
                    btnDeleteTraveler.Text = resxSet.GetString(btnDeleteTraveler.Text);
                if (resxSet.GetString(btnAddVoluntary.Text) != null)
                    btnAddVoluntary.Text = resxSet.GetString(btnAddVoluntary.Text);
                if (resxSet.GetString(btnCancelTraveler.Text) != null)
                    btnCancelTraveler.Text = resxSet.GetString(btnCancelTraveler.Text);


                // tab remaining

                if (resxSet.GetString(lblNrMaximumWheelchairs.Text) != null)
                    lblNrMaximumWheelchairs.Text = resxSet.GetString(lblNrMaximumWheelchairs.Text);
                if (resxSet.GetString(lblWhoseWheelchairs.Text) != null)
                    lblWhoseWheelchairs.Text = resxSet.GetString(lblWhoseWheelchairs.Text);
                if (resxSet.GetString(lblSupportingArms.Text) != null)
                    lblSupportingArms.Text = resxSet.GetString(lblSupportingArms.Text);
                if (resxSet.GetString(lblAnchorage.Text) != null)
                    lblAnchorage.Text = resxSet.GetString(lblAnchorage.Text);
                //

                //   jelena
                if (resxSet.GetString(lblBookeFVH.Text) != null)
                    lblBookeFVH.Text = resxSet.GetString(lblBookeFVH.Text);

                if (resxSet.GetString(lblAvrAgeTravelers.Text) != null)
                    lblAvrAgeTravelers.Text = resxSet.GetString(lblAvrAgeTravelers.Text);
                // end

 
                if (resxSet.GetString(lblFTravelers.Text) != null)
                    lblFTravelers.Text = resxSet.GetString(lblFTravelers.Text);
                if (resxSet.GetString(lblMTravelers.Text) != null)
                    lblMTravelers.Text = resxSet.GetString(lblMTravelers.Text);

                if (resxSet.GetString(lblFTravelers.Text) != null)
                    lblFTravelers.Text = resxSet.GetString(lblFTravelers.Text);
                if (resxSet.GetString(lblMTravelers.Text) != null)
                    lblMTravelers.Text = resxSet.GetString(lblMTravelers.Text);

                for (int i = 0; i < ribbonExampleMenu.CommandTabs.Count; i++)
                {
                    if (resxSet.GetString(ribbonExampleMenu.CommandTabs[i].Text) != null)
                        ribbonExampleMenu.CommandTabs[i].Text = resxSet.GetString(ribbonExampleMenu.CommandTabs[i].Text);
                    RibbonTab ri = (RibbonTab)ribbonExampleMenu.CommandTabs[i];
                    for (int j = 0; j < ri.Items.Count; j++)
                    {
                        if (ri.Items[j].Visibility == ElementVisibility.Visible)
                        {
                            if (resxSet.GetString(ri.Items[j].Text) != null)
                                ri.Items[j].Text = resxSet.GetString(ri.Items[j].Text);
                            RadRibbonBarGroup rgb = (RadRibbonBarGroup)ri.Items[j];
                            for (int n = 0; n < rgb.Items.Count; n++)
                            {
                                if (resxSet.GetString(rgb.Items[n].Text) != null)
                                    rgb.Items[n].Text = resxSet.GetString(rgb.Items[n].Text);
                            }
                        }
                    }
                }

                for (int i = 0; i < radPageBooking.Pages.Count; i++)
                {
                    if (resxSet.GetString(radPageBooking.Pages[i].Text) != null)
                        radPageBooking.Pages[i].Text = resxSet.GetString(radPageBooking.Pages[i].Text);
                }
                for (int i = 0; i < radPageExtras.Pages.Count; i++)
                {
                    if (resxSet.GetString(radPageExtras.Pages[i].Text) != null)
                        radPageExtras.Pages[i].Text = resxSet.GetString(radPageExtras.Pages[i].Text);
                }

                //for (int i = 0; i < radPageTravelPapers.Pages.Count; i++)
                //{
                //    if (resxSet.GetString(radPageTravelPapers.Pages[i].Text) != null)
                //        radPageTravelPapers.Pages[i].Text = resxSet.GetString(radPageTravelPapers.Pages[i].Text);
                //}

            }
        }

        private void fillData()
        {
            if (arrange.idArrangement > 0)
                lblIdArrangement.Text = arrange.idArrangement.ToString();
            if (arrange.codeArrangement != null)
                lblCodeArrangement.Text = arrange.codeArrangement;
            if (arrange.nameArrangement != null)
                lblNameArrangement.Text = arrange.nameArrangement;

            if (arrange.dtFromArrangement != null)
                lblDtFrom.Text = arrange.dtFromArrangement.ToString("dd/MM/yyyy");
            if (arrange.dtToArrangement != null)
                lblDtTo.Text = arrange.dtToArrangement.ToString("dd/MM/yyyy");
            if (arrange.cityArrangement != null)
                lblPlace.Text = arrange.cityArrangement.ToString();
            if (arrange.countryNameArrangement != null)
                lblCountryArrangement.Text = arrange.countryNameArrangement.ToString();

            if (arrange.typeNameArrangement != null)
                lblTypeArrangement.Text = arrange.typeNameArrangement.ToString();


            if (arrange.nrTraveler > 0)
                lblNumberTravelers.Text = arrange.nrTraveler.ToString();
            else
                lblNumberTravelers.Text = "0";
            if (arrange.minNrTraveler > 0)
                lblMinNumberTravelers.Text = arrange.minNrTraveler.ToString();
            else
                lblMinNumberTravelers.Text = "0";
            if (arrange.nrVoluntaryHelper > 0)
                lblVoluntaryHelperNumbers.Text = arrange.nrVoluntaryHelper.ToString();
            else
                lblVoluntaryHelperNumbers.Text = "0";

            if (arrange.nrMaleVoluntary > 0)
                lblMaleHelpersNumber.Text = arrange.nrMaleVoluntary.ToString();
            else
                lblMaleHelpersNumber.Text = "0";

            //lblNrTotal.Text = (Convert.ToInt32(lblNumberTravelers.Text) + Convert.ToInt32(lblVoluntaryHelperNumbers.Text)).ToString();
            lblNrTotal.Text = new ArrangementBookBUS().GetNrBookedTraveler(arrange.idArrangement).ToString();


            lblBookFTravelers.Text = new ArrangementBookBUS().GetNrTravelerByGender(2, arrange.idArrangement).ToString();
            lblBookMTravelers.Text = new ArrangementBookBUS().GetNrTravelerByGender(1, arrange.idArrangement).ToString();

            //jelena
            lblAvrAgeTraveler.Text = new ArrangementBookBUS().GetAverageAge(arrange.idArrangement).ToString();
            //end

        }
        [Test]
        private void frmArrangementBook_Load(object sender, EventArgs e)
        {
            radRibbonMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonReports.Visibility = ElementVisibility.Collapsed;
            btnReport.Visibility = ElementVisibility.Collapsed;
            btnEmail.Visibility = ElementVisibility.Collapsed;
            btnWord.Visibility = ElementVisibility.Collapsed;
            btnNewMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteDoc.Visibility = ElementVisibility.Visible;
            btnNewDoc.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;
            btnDeleteDoc.Visibility = ElementVisibility.Collapsed;
            radRibbonDocuments.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupPurchase.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupContracts.Visibility = ElementVisibility.Collapsed;
            btnPurchase.Visibility = ElementVisibility.Collapsed;
            btnDelPurchase.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupTraveler.Visibility = ElementVisibility.Visible;
            btnAddTraveler.Visibility = ElementVisibility.Visible;
            btnAddVoluntary.Visibility = ElementVisibility.Visible;
            btnDeleteTraveler.Visibility = ElementVisibility.Visible;
            btnCancelTraveler.Visibility = ElementVisibility.Visible;
            
            radRibbonBarGroupTravelpapers.Visibility = ElementVisibility.Visible;
            btnRibbonTravelpapers.Visibility = ElementVisibility.Visible;
            btnRibbonTravelpapers.ForeColor = Color.Black;

            this.gridBoardingPoint.CellEditorInitialized += gridBoardingPoint_CellEditorInitialized;

            setTranslation();
            fillData();

            lblSupportingArms.Visible = true;
            maskedArms.Visible = true;

            maskedNrMaximumWheelchairs.Text = arrange.nrMaximumWheelchairs.ToString(); //   i buitenhof hoce ovaj tab ali ne sava p
            maskedWhooseWheelchairs.Text = arrange.whoseElectricWheelchairs.ToString();
            maskedArms.Text = arrange.buSupportingArms.ToString();
            maskedAnchorage.Text = arrange.nrAnchorage.ToString();
            radPageExtras.SelectedPage = tabBoardingPoint;
            tabTargetGroup.Item.Visibility = ElementVisibility.Collapsed;
            //    tabThemeTrip.Item.Visibility = ElementVisibility.Collapsed;
          
            loadBooking();
            LoadBoardingPoint();
        }

        //===== neta 2.12.
        private void rgvPerson_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            var grid = sender as RadGridView;
            foreach (var column in grid.Columns)
            {
                column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
            }
            if (rgvPerson.Columns.Count > 0)
            {
                for (int i = 0; i < rgvPerson.Columns.Count; i++)
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString(rgvPerson.Columns[i].HeaderText) != null)
                            rgvPerson.Columns[i].HeaderText = resxSet.GetString(rgvPerson.Columns[i].HeaderText);
                    }
                }

                if (rgvPerson.Columns != null)
                {
                    if (rgvPerson.RowCount > 0)
                    {
                        this.rgvPerson.Columns["dtBooked"].FormatString = "{0: dd-MM-yyyy}";
                        this.rgvPerson.Columns["birthdate"].FormatString = "{0: dd-MM-yyyy}";

                    }
                }

                rgvPerson.AllowMultiColumnSorting = true;
                SortDescriptor type = new SortDescriptor();
                type.PropertyName = "type";
                type.Direction = ListSortDirection.Ascending;
                SortDescriptor status = new SortDescriptor();
                status.PropertyName = "idStatus";
                status.Direction = ListSortDirection.Ascending;
                rgvPerson.SortDescriptors.Add(type);
                rgvPerson.SortDescriptors.Add(status);
            }
            // sakrivanje viska kolona //
            rgvPerson.Columns["idContPers"].IsVisible = false;
            rgvPerson.Columns["idTitle"].IsVisible = false;
            rgvPerson.Columns["idGender"].IsVisible = false;
            rgvPerson.Columns["dtCreated"].IsVisible = false;
            rgvPerson.Columns["idUserCreated"].IsVisible = false;
            rgvPerson.Columns["dtModified"].IsVisible = false;
            rgvPerson.Columns["idUserModified"].IsVisible = false;
            rgvPerson.Columns["idUserResponsible"].IsVisible = false;
            rgvPerson.Columns["isMaried"].IsVisible = false;
            rgvPerson.Columns["isActive"].IsVisible = false;
            rgvPerson.Columns["isDied"].IsVisible = false;
            rgvPerson.Columns["dtOfDeath"].IsVisible = false;
            rgvPerson.Columns["isNeedProspect"].IsVisible = false;
            rgvPerson.Columns["isNeedMail"].IsVisible = false;
            rgvPerson.Columns["identBSN"].IsVisible = false;
            rgvPerson.Columns["isPayInvoice"].IsVisible = false;
            rgvPerson.Columns["isSharePicture"].IsVisible = false;
            rgvPerson.Columns["isPaperByMail"].IsVisible = false;
            rgvPerson.Columns["isContactPerson"].IsVisible = false;
            rgvPerson.Columns["idClient"].IsVisible = false;
            rgvPerson.Columns["livesIn"].IsVisible = false;
            rgvPerson.Columns["idCpFunction"].IsVisible = false;
            rgvPerson.Columns["isRequestBrochure"].IsVisible = false;
            rgvPerson.Columns["idStatus"].IsVisible = false;
            rgvPerson.Columns["isInsurance"].IsVisible = false;
            rgvPerson.Columns["nameFunction"].IsVisible = false;
            rgvPerson.Columns["nameLiving"].IsVisible = false;
            rgvPerson.Columns["nameClient"].IsVisible = false;
            rgvPerson.Columns["fullname_with_title"].IsVisible = false;
            rgvPerson.Columns["imageContPers"].IsVisible = false;
            rgvPerson.Columns["fullname"].IsVisible = false;
            rgvPerson.Columns["maidenname"].IsVisible = false;
            rgvPerson.Columns["nameTitle"].IsVisible = false;
            rgvPerson.Columns["idArrangementBook"].IsVisible = false;
           

            int nrBookedTravelers = 0;
            if (rgvPerson != null)
                if (rgvPerson.Rows.Count > 0)
                    for (int i = 0; i < rgvPerson.Rows.Count; i++)
                        if (rgvPerson.Rows[i].Cells["type"].Value.ToString() == "Traveler" && Convert.ToInt32(rgvPerson.Rows[i].Cells["idStatus"].Value.ToString()) != 3)
                            nrBookedTravelers = nrBookedTravelers + 1;
            //  lblBookTravelers.Text = nrBookedTravelers.ToString();
            int nrBookedVH = 0;
            nrBookedVH = new PersonBUS().GetNrPersonsVHForArrangement(arrange.idArrangement, Login._user.lngUser);
            lblBookVH.Text = nrBookedVH.ToString();
            //=== ubaceno za Male helpers
            int nrMaleBooked = 0;
            nrMaleBooked = new PersonBUS().GetNrMalePersonsVHForArrangement(arrange.idArrangement, Login._user.lngUser);
            lblBookTravelers.Text = nrMaleBooked.ToString();
            //======
            lblBookTotal.Text = (nrBookedVH + nrBookedTravelers).ToString();

            //====== jelena
            // female helpers
            int nrFemaleBooked = 0;
            nrFemaleBooked = new PersonBUS().GetNrFemalePersonsVHForArrangement(arrange.idArrangement, Login._user.lngUser);
            lblBookeFVolH1.Text = nrFemaleBooked.ToString();
            //end

            lblNrTotal.Text = new ArrangementBookBUS().GetNrBookedTraveler(arrange.idArrangement).ToString();
            lblBookFTravelers.Text = new ArrangementBookBUS().GetNrTravelerByGender(2, arrange.idArrangement).ToString();
            lblBookMTravelers.Text = new ArrangementBookBUS().GetNrTravelerByGender(1, arrange.idArrangement).ToString();

            //jelena
            lblAvrAgeTraveler.Text = new ArrangementBookBUS().GetAverageAge(arrange.idArrangement).ToString();
            //end

            //======Neta
            //nr of reserves
            int nrReservesVH = 0;
            nrReservesVH = new PersonBUS().GetReservesVHForArrangement(arrange.idArrangement, Login._user.lngUser);
            numBookVHReserves.Text = nrReservesVH.ToString();

            int nrReservesTraveler = 0;
            nrReservesTraveler = new PersonBUS().GetReservesTravelerForArrangement(arrange.idArrangement, Login._user.lngUser);
            numBookTReserves.Text = nrReservesTraveler.ToString();

            //loadBooking();

            List<ArrangementFuncSkillsModel> functions = new List<ArrangementFuncSkillsModel>();
            functions = new ArrangementBookBUS().GetFunctionsForArrangement(arrBookModel.idArrangement);

            List<ArrangementFuncSkillsModel> skills = new List<ArrangementFuncSkillsModel>();
            skills = new ArrangementBookBUS().GetSkillsForArrangement(arrBookModel.idArrangement);

            List<ArrangementSelectedFuncSkillsModel> selFunctions = new List<ArrangementSelectedFuncSkillsModel>();
            selFunctions = new ArrangementBookBUS().GetSelectedSkillsOrFunctionsForArrangement("F", arrBookModel.idArrangement);

            List<ArrangementSelectedFuncSkillsModel> selSkills = new List<ArrangementSelectedFuncSkillsModel>();
            selSkills = new ArrangementBookBUS().GetSelectedSkillsOrFunctionsForArrangement("S", arrBookModel.idArrangement);




            if (functions != null)
                if (functions.Count > 0)
                {

                    for (int i = 0; i < functions.Count; i++)
                    {
                        GridViewCheckBoxColumn chk3 = new GridViewCheckBoxColumn();
                        chk3.Name = "F" + functions[i].ID.ToString();
                        chk3.HeaderText = functions[i].Quest;
                        rgvPerson.Columns.Add(chk3);
                        rgvPerson.Columns[chk3.Name].Width = (int)(this.CreateGraphics().MeasureString(functions[i].Quest, this.Font).Width + 38);
                        rgvPerson.Columns[chk3.Name].ReadOnly = true;

                        if (i == 0)
                            numberColumnsFunc = rgvPerson.Columns[chk3.Name].Index;
                        if (i == functions.Count - 1)
                            numberUntilColumnsFunc = rgvPerson.Columns[chk3.Name].Index + 1;

                    }
                }


            if (skills != null)
                if (skills.Count > 0)
                {

                    for (int i = 0; i < skills.Count; i++)
                    {
                        GridViewCheckBoxColumn chk3 = new GridViewCheckBoxColumn();
                        chk3.Name = "S" + skills[i].ID.ToString();
                        chk3.HeaderText = skills[i].Quest;
                        rgvPerson.Columns.Add(chk3);
                        rgvPerson.Columns[chk3.Name].Width = (int)(this.CreateGraphics().MeasureString(skills[i].Quest, this.Font).Width + 38);
                        rgvPerson.Columns[chk3.Name].ReadOnly = true;
                        if (i == 0)
                            numColumnsSkills = rgvPerson.Columns[chk3.Name].Index;
                        if (i == skills.Count - 1)
                            numberUntilColumnsSkills = rgvPerson.Columns[chk3.Name].Index + 1;

                    }
                }



            if (rgvPerson != null)
                if (rgvPerson.RowCount > 0)
                    for (int j = 0; j < rgvPerson.RowCount; j++)
                    {
                        if (numberColumnsFunc > 0)

                            for (int i = numberColumnsFunc; i < numberUntilColumnsFunc; i++)
                            {
                                if (rgvPerson.Rows[j].Cells["idContPers"].Value.ToString() != "")
                                {
                                    if (selFunctions.Find(s => s.id == Convert.ToInt32(rgvPerson.Columns[i].Name.Replace("F", "")) && s.idContPers == Convert.ToInt32(rgvPerson.Rows[j].Cells["idContPers"].Value.ToString())) != null)
                                    {
                                        rgvPerson.Rows[j].Cells[i].Value = true;
                                    }
                                }
                            }

                        if (numColumnsSkills > 0)
                            if (skills != null)
                                if (skills.Count > 0)
                                    for (int i = numColumnsSkills; i < numberUntilColumnsSkills; i++)
                                    {
                                        if (rgvPerson.Rows[j].Cells["idContPers"].Value.ToString() != "")
                                        {
                                            if (selSkills.Find(s => s.id == Convert.ToInt32(rgvPerson.Columns[i].Name.Replace("S", "")) && s.idContPers == Convert.ToInt32(rgvPerson.Rows[j].Cells["idContPers"].Value.ToString())) != null)
                                            {
                                                rgvPerson.Rows[j].Cells[i].Value = true;
                                            }
                                        }
                                    }
                    }
        }
        private bool isAutitravel()
        {
            bool isChecked = false;
            ArrangementBookBUS arrBUS = new ArrangementBookBUS();

            if (arrBUS.isAutitravelChecked(arrBookModel.idArrangement) > -1)
                isChecked = true;
            return isChecked;
        }

        private void updateStatus()
        {
            List<ArrangementStatusModel> listStatus = new List<ArrangementStatusModel>();
            listStatus = new ArrangementStatusBUS().GetAllArrangementStatus();
            if (listStatus != null)
            {
                if (listStatus.Count > 0)
                {

                    if (listStatus.SingleOrDefault(s => s.idArrangementStatus == 3).nameArrangementStatus == arrange.statusArrangement)
                    {
                        arrange.statusArrangement = listStatus.SingleOrDefault(s => s.idArrangementStatus == 2).nameArrangementStatus;
                        new ArrangementBUS().Update(arrange, this.Name, Login._user.idUser);
                    }
                }
            }

        }

        private void rgvPerson_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            //chkIfExpired = true;
            //if (chkIfExpired=false)
            //{
            //    translateRadMessageBox trr = new translateRadMessageBox();
            //    trr.translateAllMessageBox("Not possible to book, releasedate from contract is expired!");
            //    return;
            //}
            GridViewRowInfo info = this.rgvPerson.CurrentRow;
            ArrangementBookBUS arrBUS = new ArrangementBookBUS();

            if (info.DataBoundItem != null)
            {
                //arrBookModel.idArrangementBook = arrBUS.GetIdArrangement(arrBookModel.idArrangement, Convert.ToInt32(info.Cells["idContPers"].Value.ToString()));

                arrBookModel.idArrangementBook = Convert.ToInt32(info.Cells["idArrangementBook"].Value.ToString());

                if (arrBookModel.idArrangementBook != 0)
                {
                    //arrBookModel.idArrangementBook = arrBUS.GetIdArrangement(arrBookModel.idArrangement, Convert.ToInt32(info.Cells["idContPers"].Value.ToString()));
                    if (info.Cells["dtBooked"].Value != null)
                    {
                        DateTime dateBook = Convert.ToDateTime(info.Cells["dtBooked"].Value.ToString());
                        arrBookModel.dtBooked = dateBook;
                    }
                    if (info.Cells["type"] != null)
                        if (info.Cells["type"].Value.ToString() != "")
                            if (info.Cells["type"].Value.ToString() == "Traveler")
                            {
                                using (frmArrangementBookingPerson frm = new frmArrangementBookingPerson(arrBookModel, Convert.ToInt32(txtRolator), Convert.ToInt32(txtRolstool), Convert.ToInt32(txtArmAfa), Convert.ToInt32(txtAnchorage), false, false))
                                {
                                    frm.ShowDialog();
                                    if (frm.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        rgvPerson.DataSource = null;
                                        PersonBUS pb = new PersonBUS();
                                        rgvPerson.Columns.Clear();
                                        rgvPerson.DataSource = pb.GetAllPersonsForArrangement(arrBookModel.idArrangement, Login._user.lngUser);
                                    }
                                    arrBookModel.idArrangementBook = 0;
                                }
                                GC.Collect();
                                GC.WaitForPendingFinalizers();
                                GC.Collect();
                            }
                            else
                            {
                                using (frmArrangementBookingPerson_VH frm = new frmArrangementBookingPerson_VH(arrBookModel, false))
                                {
                                    frm.ShowDialog();
                                    if (frm.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        rgvPerson.DataSource = null;
                                        PersonBUS pb = new PersonBUS();
                                        rgvPerson.Columns.Clear();
                                        rgvPerson.DataSource = pb.GetAllPersonsForArrangement(arrBookModel.idArrangement, Login._user.lngUser);
                                    }
                                    arrBookModel.idArrangementBook = 0;
                                }
                                GC.Collect();
                                GC.WaitForPendingFinalizers();
                                GC.Collect();
                            }
                }
            }
        }

        private void frmArrangementBook_SizeChanged(object sender, EventArgs e)
        {
            radRibbonBarGroupTraveler.Visibility = ElementVisibility.Visible;
            btnAddTraveler.Visibility = ElementVisibility.Visible;
            btnAddVoluntary.Visibility = ElementVisibility.Visible;
            btnDeleteTraveler.Visibility = ElementVisibility.Visible;
            radRibbonBarGroupTravelpapers.Visibility = ElementVisibility.Visible;
            btnRibbonTravelpapers.Visibility = ElementVisibility.Visible;
        }

        private void radPageBooking_SelectedPageChanged(object sender, EventArgs e)
        {

            RadPageView rpv = (RadPageView)sender;
            string sName = ((RadPageView)sender).SelectedPage.Name;
            switch (sName)
            {
                case "tabTripInfo":
                    // loadPurchase();
                    loadTrip();
                    break;
                case "tabBooking":
                    loadBooking();
                    break;
                case "tabRooms":
                    loadRooms();
                    break;
                case "tabVolunteers":
                    loadVolunteersFunctionsAndSkills();
                    break;
                //case "tabTravelpapers":
                //    loadRemaining();
                //    break;
                default:
                    break;
            }
        }

        private void loadVolunteersFunctionsAndSkills()
        {
            ArrangementBookBUS bus = new ArrangementBookBUS();
            listFunctions.DataSource = null;

            List<ArrangementFuncSkillsModel> functions = new List<ArrangementFuncSkillsModel>();
            functions = bus.GetFunctionsForArrangement(arrBookModel.idArrangement);
            //listFunctions.DataSource = functions;

            listFunctions.Columns.Clear();
            listFunctions.Columns.Add("Quest");
            listFunctions.Columns.Add("Required");
            listFunctions.Columns.Add("Booked");
            listFunctions.Columns.Add("Available");


            listFunctions.Columns["Quest"].Width = 200;
            listFunctions.Columns["Required"].Width = 80;
            listFunctions.Columns["Booked"].Width = 80;
            listFunctions.Columns["Available"].Width = 80;

            listFunctions.Items.Clear();

            if (functions != null)
            {
                foreach (ArrangementFuncSkillsModel m in functions)
                {
                    listFunctions.Items.Add(m.Quest, m.Required, m.Booked, m.Available);
                }
            }

            listSkills.DataSource = null;
            List<ArrangementFuncSkillsModel> skills = new List<ArrangementFuncSkillsModel>();
            skills = bus.GetSkillsForArrangement(arrBookModel.idArrangement);

            listSkills.Columns.Clear();
            listSkills.Columns.Add("Quest");
            listSkills.Columns.Add("Required");
            listSkills.Columns.Add("Booked");
            listSkills.Columns.Add("Available");


            listSkills.Columns["Quest"].Width = 200;
            listSkills.Columns["Required"].Width = 80;
            listSkills.Columns["Booked"].Width = 80;
            listSkills.Columns["Available"].Width = 80;

            listSkills.Items.Clear();
            if (skills != null)
            {
                foreach (ArrangementFuncSkillsModel m in skills)
                {
                    listSkills.Items.Add(m.Quest, m.Required, m.Booked, m.Available);
                }
            }

        }

        private void btnAddRooms_Click(object sender, EventArgs e)
        {


            //grid accomodatio
            List<ArrangementArticalModel_Rooms> modelArticlesAccomodation = new List<ArrangementArticalModel_Rooms>();
            modelArticlesAccomodation = new ArticalBUS().GetAllArticalsForArrangemetAccomodation1(arrBookModel.idArrangement);

            ArrangementRoomsBUS arb = new ArrangementRoomsBUS();


            using (GridLookupFormRooms glfr = new GridLookupFormRooms(modelArticlesAccomodation, null, arrBookModel.idArrangement, "Rooms"))
            {
                glfr.ShowDialog();
                loadRooms();
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void btnDelRooms_Click(object sender, EventArgs e)
        {
            if (rgvRooms.CurrentRow != null)
            {
                if (rgvRooms.CurrentRow.DataBoundItem != null)
                {
                    ArrangementRoomsModel m = (ArrangementRoomsModel)rgvRooms.CurrentRow.DataBoundItem;
                    ArticalBUS abus = new ArticalBUS();
                    ArticalModel amod = new ArticalModel();

                    amod = abus.GetArticalByID(m.idArticle);
                    translateRadMessageBox trr = new translateRadMessageBox();
                    DialogResult dr = trr.translateAllMessageBoxDialog("Delete Rooms for article: " + amod.nameArtical, "Delete Room");
                    //MessageBox.Show(m.idArticle + " - " + m.idRoom + " - " + m.isContract.ToString());
                    if (dr == DialogResult.Yes)
                    {
                        ArrangementRoomsBUS bus = new ArrangementRoomsBUS();
                        bool b = bus.Delete(m, this.Name, Login._user.idUser);

                        if (b == true)
                        {
                            loadRooms();
                        }
                        else
                        {
                            //translateRadMessageBox trr = new translateRadMessageBox();
                            trr.translateAllMessageBox("Something went wrong. Please contact your administrator!");
                        }
                    }
                }
            }
        }

        private void listFunctions_CellFormatting(object sender, ListViewCellFormattingEventArgs e)
        {
            e.CellElement.TextAlignment = ContentAlignment.MiddleCenter;
        }

        private void listSkills_CellFormatting(object sender, ListViewCellFormattingEventArgs e)
        {
            e.CellElement.TextAlignment = ContentAlignment.MiddleCenter;
        }

        BindingList<BoardingPointModel> listaBoardingPoint;
        private void LoadBoardingPoint()
        {
            ArrangementBoardingPointBUS abpb = new ArrangementBoardingPointBUS();
            List<BoardingPointModel> arrBoardingPoint = new List<BoardingPointModel>();
            arrBoardingPoint = abpb.GetArrangementBoardingPoint(arrange.idArrangement);


            if (arrBoardingPoint != null)
            {
                foreach (BoardingPointModel m in arrBoardingPoint)
                {
                    string tmp1 = m.dtDeparture.ToString("dd.MM.yyyy");
                    if (tmp1 == "01.01.1900")
                        m.dtDeparture = arrange.dtFromArrangement;

                    string tmp2 = m.dtArrival.ToString("dd.MM.yyyy");
                    if (tmp2 == "01.01.1900")
                    m.dtArrival = arrange.dtToArrangement;
                }

                listaBoardingPoint = new BindingList<BoardingPointModel>(arrBoardingPoint);
            }
            else
            {
                listaBoardingPoint = new BindingList<BoardingPointModel>();

                //foreach (BoardingPointModel m in arrBoardingPoint)
                //{
                //    m.dtDeparture = arrange.dtFromArrangement;
                //    m.dtArrival = arrange.dtToArrangement;
                //}
            }
            gridBoardingPoint.DataSource = listaBoardingPoint;

        }


        private void gridBoardingPoint_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            var grid = sender as RadGridView;
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                foreach (var column in grid.Columns)
                {
                    if (resxSet.GetString(column.HeaderText) != null && resxSet.GetString(column.HeaderText) != "")
                        column.HeaderText = resxSet.GetString(column.HeaderText);
                    column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 38);
                }
            }

            if (gridBoardingPoint != null)
                if (gridBoardingPoint.ColumnCount > 0)
                {
                    gridBoardingPoint.Columns["isChecked"].Width = 50;
                    gridBoardingPoint.Columns["isChecked"].IsVisible = false;

                    gridBoardingPoint.Columns["idBoardingPoint"].IsVisible = false;

                    //gridBoardingPoint.Columns["nameBoardingPoint"].Width = 250;
                    gridBoardingPoint.Columns["nameBoardingPoint"].ReadOnly = true;
                    gridBoardingPoint.Columns["addressBoardingPoint"].ReadOnly = true;

                    gridBoardingPoint.Columns["dtDeparture"].Width = 200;
                    ((GridViewDateTimeColumn)gridBoardingPoint.Columns["dtDeparture"]).Format = DateTimePickerFormat.Time;
                    ((GridViewDateTimeColumn)gridBoardingPoint.Columns["dtDeparture"]).FormatString = "{0: HH:mm}";
                    gridBoardingPoint.Columns["dtDeparture"].FormatString = "{0:dd.MM.yyyy HH:mm 'h'}";

                    gridBoardingPoint.Columns["dtArrival"].Width = 200;
                    ((GridViewDateTimeColumn)gridBoardingPoint.Columns["dtArrival"]).Format = DateTimePickerFormat.Time;
                    ((GridViewDateTimeColumn)gridBoardingPoint.Columns["dtArrival"]).FormatString = "{0: HH:mm}";
                    gridBoardingPoint.Columns["dtArrival"].FormatString = "{0:dd.MM.yyyy HH:mm 'h'}";
                }
        }

        private void saveBoardingPoint()
        {

            arrangeBoardingPoint.Clear();
            foreach (BoardingPointModel m in listaBoardingPoint)
            {
                //if (m.isChecked == true)
                //{
                ArrangementBoardingPointModel model = new ArrangementBoardingPointModel();
                model.idArrangement = arrange.idArrangement;
                model.idBoardingPoint = m.idBoardingPoint;
                model.dtDeparture = m.dtDeparture;
                model.dtArrival = m.dtArrival;
                model.sortBoardingPoint = m.sortBoardingPoint;

                arrangeBoardingPoint.Add(model);
                //}
            }


            Boolean isSuccessfull = false;
            ArrangementBoardingPointBUS ttb = new ArrangementBoardingPointBUS();
            if (arrangeBoardingPoint != null)
                if (arrangeBoardingPoint.Count > 0)
                    for (int i = 0; i < arrangeBoardingPoint.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (ttb.Delete(arrange.idArrangement, this.Name, Login._user.idUser) == true)
                                isSuccessfull = true;
                            else
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("You have not succesufully inserted boarding point. Please check!");
                            }
                        }
                        if (isSuccessfull == true)
                        {
                            if (ttb.Save(arrangeBoardingPoint[i], this.Name, Login._user.idUser) == false)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translatePartAndNonTranslatedPart("You have not succesufully inserted boarding point ", ttb.GetBoardingPointName(arrangeBoardingPoint[i].idBoardingPoint));
                                isSuccessfull = false;
                            }
                            else
                            {
                                isSuccessfull = true;
                            }

                        }
                    }

            if (isSuccessfull == true)
            {
                LoadBoardingPoint();
            }
        }
    
        private void btnSave_Click(object sender, EventArgs e)
        {

            saveBoardingPoint();            
        }

        private void gridBoardingPoint_CellValueChanged(object sender, GridViewCellEventArgs e)
        {

            GridDataCellElement gdce = (GridDataCellElement)sender;
            GridViewColumn gvc = (GridViewColumn)gdce.ColumnInfo;
            if (gvc.FieldName == "sortBoardingPoint")
            {
                gridBoardingPoint.SortDescriptors.Remove("sortBoardingPoint");
                gridBoardingPoint.SortDescriptors.Add("sortBoardingPoint", ListSortDirection.Ascending);
            }
        }
        
        private bool isInArrangementRemaining()
        {
            bool result = false;
            ArrangementBookBUS arbus = new ArrangementBookBUS();

            int contains = arbus.isInArrangementRemaining(arrange.idArrangement);
            if (contains > -1)
                result = true;
            return result;

        }
        
        private bool isCheckedTwoFlight()
        {
            bool result = false;
            ArrangementBookBUS arbus = new ArrangementBookBUS();
            List<ArrangementRemainingModel> lista = new List<ArrangementRemainingModel>();
            lista = arbus.isCheckedTwoFlight(arrange.idArrangement);
            if (lista[0].twoFlight == true)
                result = true;
            return result;

        }
        private void recalculateVolArr(int idArrangement, int idContPers)
        {

            ArrangementBookBUS ab = new ArrangementBookBUS();

            // broj skilova iz tabele medLookup
            List<MedicalVoluntaryQuestModel> listNrSkill = new List<MedicalVoluntaryQuestModel>();
            // svi skilovi iz tabele volArr za taj arrangement
            List<MedicalVoluntaryQuestModel> listSkillVolArr = new List<MedicalVoluntaryQuestModel>();
            //svi skilovi koji su izbrisani za posmatranu osobu i aranzman iz MedLookup-a
            List<MedicalVoluntaryQuestModel> listDeleteSkillMedLookup = new List<MedicalVoluntaryQuestModel>();


            // listSkill = ab.GetSkillForPerson(arrBookModel.idContPers);
            listDeleteSkillMedLookup = ab.GetSkillsForArrAndPerson(idArrangement, idContPers);
            ab.DeletePrsonFromMedLookup(idArrangement, idContPers);
            listNrSkill = ab.GetNrForSkillsArrangement(idArrangement);
            listSkillVolArr = ab.GetAllSkillsVolArr(idArrangement);

            if (listDeleteSkillMedLookup != null)
            {
                for (int i = 0; i < listDeleteSkillMedLookup.Count; i++)
                {
                    var nrSkillIsOne = listSkillVolArr.Find(s => s.idQuest == listDeleteSkillMedLookup[i].idQuest && (s.nameQuestGroup) == "1");

                    if (nrSkillIsOne != null)
                    {
                        ab.DeleteVolArr(idArrangement, listDeleteSkillMedLookup[i].idQuest);
                    }
                }
            }

            if (listSkillVolArr != null)
            {
                if (listNrSkill != null)
                {
                    for (int i = 0; i < listNrSkill.Count; i++)
                    {
                        // List<MedicalVoluntaryQuestModel> telForPerson = new List<MedicalVoluntaryQuestModel>(); 
                        var skillExistInVolarr = listSkillVolArr.Find(s => s.idQuest == listNrSkill[i].idQuest);
                        //insert                             
                        if (skillExistInVolarr != null)
                        {
                            string txt = listNrSkill[i].nameQuestGroup;
                            int idQuest = listNrSkill[i].idQuest;


                            if (ab.UpdateVolArr(listNrSkill[i], txt, idQuest, idArrangement) == false)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("You have not succesufully update skill. Please check!");
                            }

                        }

                    }
                }


            }


        }

        private void gridBoardingPoint_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            //Restriction  mouse wheel and KayUp, KeyDown for grid when is in Edit mode Gorance 29 08
            var editor = e.ActiveEditor as GridSpinEditor;
            if (editor != null)
            {
                var element = editor.EditorElement as GridSpinEditorElement;
                element.InterceptArrowKeys = false;
                element.EnableMouseWheel = false;
            }
        }

    }
}