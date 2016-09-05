using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Outlook = Microsoft.Office.Interop.Outlook;
using BIS.Business;
using BIS.Model;
using Telerik.WinControls.UI;
using System.IO;
using System.Diagnostics;
using System;
using System.IO;
using System.Runtime.InteropServices;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI.Export.ExcelML;

namespace GUI
{


    public partial class frmVolAvailabilityPreselection : Telerik.WinControls.UI.RadForm
    {
        List<VolAvailabilityPreselectionModel> volSkillsList = new List<VolAvailabilityPreselectionModel>();
        List<VolAvailabilityPreselectionModel> volFunctionList = new List<VolAvailabilityPreselectionModel>();
        List<VolAvailabilityPreselectionModel> volFunctionListExit = new List<VolAvailabilityPreselectionModel>();
        List<VolAvailabilityPreselectionModel> volTripList = new List<VolAvailabilityPreselectionModel>();
        List<VolAvailabilityPreselectionModel> volFunctionNotBooked = new List<VolAvailabilityPreselectionModel>();

        //ReasonIn
        List<VoluntaryReasonInModel> reasonInList = new List<VoluntaryReasonInModel>();
        List<VolAvailabilityPreselectionModel> volFunctionListRI = new List<VolAvailabilityPreselectionModel>();

        //ReasonOUT
        List<VolAvailabilityPreselectionModel> volFunctionListRO = new List<VolAvailabilityPreselectionModel>();
        List<VoluntaryReasonOutModel> volReasonOutRO = new List<VoluntaryReasonOutModel>();


        // expirationDate
        DateTime dateFrom = DateTime.Now;
        DateTime dateTo = DateTime.Now;
        private string layoutExpDate;

        DateTime dateExpiration = DateTime.Now;
        List<VolVogCokGokPassModel> lista = new List<VolVogCokGokPassModel>();

        //exitList
        DateTime dateFromExit = DateTime.Now;
        DateTime dateToExit = DateTime.Now;
        private string layoutExitlist;
        List<int> labelExitList = new List<int>(); //za labele ExitList

        List<int> label = new List<int>(); //za labele
        private string layout; //save Layout     
        List<int> labelExpiried = new List<int>(); //za labele
        List<int> labelReasonIn = new List<int>(); //za labele ReasonIn

        List<int> labelFunctionReasonIn = new List<int>(); // za labele FunctionReason IN
        private string layoutReasonIn; //saveLayoutReasonIn 

        //ReasonOUT
        List<int> labelReasonOut = new List<int>();
        List<int> reasonOut = new List<int>();
        private string layoutRO;

        //AgeList
        List<VolAvailabilityPreselectionModel> volFunctionListAl = new List<VolAvailabilityPreselectionModel>();
        List<int> labelAgeList = new List<int>();
        DateTime referenceAl = new DateTime();
        int ageFromAl;
        int ageToAl = 200;
        private string layoutAgeList;

        //Unique volunteers
        List<int> labelUniqueVolunteers = new List<int>(); //Za labele Unique volunteers     
        List<VolAvailabilityPreselectionModel> volFunctionUniqueList = new List<VolAvailabilityPreselectionModel>();
        private string layoutUniqueVol; //saveLayoutUnique



        //Not booked
        DateTime dateFromNotBooked = DateTime.Now;
        DateTime dateToNotBooked = DateTime.Now;
        private string layoutNotBooked;
        List<int> labelNotBooked = new List<int>(); //za labele ExitList

        //All booking
        List<VolAvailabilityPreselectionModel> volFunctionListAb = new List<VolAvailabilityPreselectionModel>();
        List<int> labelAllBooking = new List<int>();
        DateTime dateFromAB = DateTime.Now;
        DateTime dateToAB = DateTime.Now;
        private string layoutAllBooking;

        public frmVolAvailabilityPreselection()
        {

            this.Icon = Login.iconForm;

            string name = this.Text.Substring(0);

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(name) != null)
                    name = resxSet.GetString(name);
            }
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + name;

            InitializeComponent();
        }

        private void frmVolAvailabilityPreselection_Load(object sender, EventArgs e)
        {
            //Sakrivanje dugmica za Export i Print
            btnVolAvaPrint.Visible = false; 
            btnExportToExcel.Visible = false;

            btnPrintExpirance.Visible = false;
            btnExportToExcelExpirance.Visible = false;

            btnPrintExitList.Visible = false;
            btnExportToExcelExitList.Visible = false;

            btnPrintPreviewReasonIn.Visible = false;
            btnExportToExcelReasonIn.Visible = false;

            btnPrintPreviewRo.Visible = false;
            btnExportToExcelRo.Visible = false;

            btnPrintPreviewUnique.Visible = false;
            btnExportToExcelUnique.Visible = false;

            btnPrintPreviewAl.Visible = false;
            btnExportToExcelAl.Visible = false;

            btnPrintNotBooked.Visible = false;
            btnExportToExcelNotBooked.Visible = false;

            btnPrintPreviewAb.Visible = false;
            btnExportToExcelAb.Visible = false;

            //Labels insert

            int Y = 0;
            int X = 0;
            RadRadioButton rck = new RadRadioButton();
            rck.Font = new Font("Verdana", 9);
            rck.Name = "chkLabel";
            rck.Text = "None";
            rck.Location = new Point(0, Y);
            rck.CheckStateChanged += rck_CheckStateChanged;
            rck.AutoSize = true;
            rck.IsChecked = true;

            Y = Y + 3 + rck.Height;

            panelLabels.Controls.Add(rck);

            Y = 21;

            for (int i = 0; i < Login._arrLabels.Count; i++)
            {
                rck = new RadRadioButton();
                rck.Font = new Font("Verdana", 9);
                rck.Name = "chkLabel" + Login._arrLabels[i].idLabel.ToString();
                rck.Text = Login._arrLabels[i].nameLabel;
                rck.Location = new Point(0, Y);
                rck.CheckStateChanged += rck_CheckStateChanged;
                rck.AutoSize = true;
                Y = Y + 3 + rck.Height;
                panelLabels.Controls.Add(rck);

            }

            setTranslation();

            //ReasonIn and Availability with skills
            layout = MainForm.gridFiltersFolder + "\\layoutVolAvailabilityPreselection.xml";

            layoutReasonIn = MainForm.gridFiltersFolder + "\\layoutReasonIn.xml";
            pickerDtFrom.Value = DateTime.Now;
            pickerDtTo.Value = DateTime.Now;
            reasonInDtFrom.Value = DateTime.Now;
            dtToReasonIn.Value = DateTime.Now;
            loadLabelReasonIn();

            //expirationDate
            dtFromExpiration.Value = DateTime.Now;
            dtToExpiration.Value = DateTime.Now;
            dtExpiration.Value = DateTime.Now;
            loadLabelExpiried();

            layoutExpDate = MainForm.gridFiltersFolder + "\\layoutVolAvailabilityPreselectionExpDate.xml";

            //exitList
            dtFromExit.Value = DateTime.Now;
            dtToExit.Value = DateTime.Now;
            loadLabelExitList();
            layoutExitlist = MainForm.gridFiltersFolder + "\\layoutVolAvailabilityPreselectionExitlist.xml";

            //Reason out
            layoutRO = MainForm.gridFiltersFolder + "\\layoutVolAvailabilityPreselectionRO.xml";
            dtFromRo.Value = DateTime.Now;
            dtToRo.Value = DateTime.Now;
            loadLabelReasonOut();

            //Age list
            layoutAgeList = MainForm.gridFiltersFolder + "\\layoutVolAvailabilityPreselectionAgeList.xml";
            dtReferenceAl.Value = DateTime.Now;
            loadLabelAgeList();

            //Unique volunteers
            pickerDtFormUnique.Value = DateTime.Now;
            pickerDtToUnique.Value = DateTime.Now;
            layoutUniqueVol = MainForm.gridFiltersFolder + "\\layoutVolUnique.xml";
            LoadUniqueVolunteers();

            //NotBooked
            dtFromNotBooked.Value = DateTime.Now;
            dtToNotBooked.Value = DateTime.Now;
            loadLabelNotBooked();
            layoutNotBooked = MainForm.gridFiltersFolder + "\\layoutVolAvailabilityPreselectionNotBooked.xml";

            //All booking
            loadLabelAllBookings();
            dtFromAB.Value = DateTime.Now;
            dtToAB.Value = DateTime.Now;
            layoutAllBooking = MainForm.gridFiltersFolder + "\\layoutVolAvailabilityPreselectionAllBooking.xml";
        }
        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                //Availability with skills
                if (resxSet.GetString(lblDateFrom.Text) != null)
                    lblDateFrom.Text = resxSet.GetString(lblDateFrom.Text);
                if (resxSet.GetString(lblDateTo.Text) != null)
                    lblDateTo.Text = resxSet.GetString(lblDateTo.Text);
                if (resxSet.GetString(lblTripPreferences.Text) != null)
                    lblTripPreferences.Text = resxSet.GetString(lblTripPreferences.Text);
                if (resxSet.GetString(lblSkills.Text) != null)
                    lblSkills.Text = resxSet.GetString(lblSkills.Text);
                if (resxSet.GetString(lblFunctions.Text) != null)
                    lblFunctions.Text = resxSet.GetString(lblFunctions.Text);
                if (resxSet.GetString(btnPrint.Text) != null)
                    btnPrint.Text = resxSet.GetString(btnPrint.Text);

                //expirationDate
                if (resxSet.GetString(lblDateFromExpiration.Text) != null)
                    lblDateFromExpiration.Text = resxSet.GetString(lblDateFromExpiration.Text);
                if (resxSet.GetString(lblDateToExpiration.Text) != null)
                    lblDateToExpiration.Text = resxSet.GetString(lblDateToExpiration.Text);
                if (resxSet.GetString(lbldtExpiration.Text) != null)
                    lbldtExpiration.Text = resxSet.GetString(lbldtExpiration.Text);
                if (resxSet.GetString(radMenuItemSaveTasksNotBooked.Text) != null)
                    radMenuItemSaveTasksNotBooked.Text = resxSet.GetString(radMenuItemSaveTasksNotBooked.Text);

                if (resxSet.GetString(rbnCokExpiration.Text) != null)
                    rbnCokExpiration.Text = resxSet.GetString(rbnCokExpiration.Text);
                if (resxSet.GetString(rbnVokExpiration.Text) != null)
                    rbnVokExpiration.Text = resxSet.GetString(rbnVokExpiration.Text);
                if (resxSet.GetString(rbnVogExpiration.Text) != null)
                    rbnVogExpiration.Text = resxSet.GetString(rbnVogExpiration.Text);
                if (resxSet.GetString(rbnVokExpiration.Text) != null)
                    rbnVokExpiration.Text = resxSet.GetString(rbnVokExpiration.Text);
                if (resxSet.GetString(rbnPassExpiration.Text) != null)
                    rbnPassExpiration.Text = resxSet.GetString(rbnPassExpiration.Text);
                if (resxSet.GetString(rbNoneExpiration.Text) != null)
                    rbNoneExpiration.Text = resxSet.GetString(rbNoneExpiration.Text);
                if (resxSet.GetString(btnDoExpirance.Text) != null)
                    btnDoExpirance.Text = resxSet.GetString(btnDoExpirance.Text);
                //if (resxSet.GetString(btnCancelExpiration.Text) != null)
                //    btnCancelExpiration.Text = resxSet.GetString(btnCancelExpiration.Text);
                //Exitlist

                if (resxSet.GetString(lblDtFromExit.Text) != null)
                    lblDtFromExit.Text = resxSet.GetString(lblDtFromExit.Text);

                if (resxSet.GetString(lblDtToExit.Text) != null)
                    lblDtToExit.Text = resxSet.GetString(lblDtToExit.Text);

                if (resxSet.GetString(lblFunctionsExitlist.Text) != null)
                    lblFunctionsExitlist.Text = resxSet.GetString(lblFunctionsExitlist.Text);

                if (resxSet.GetString(btnDoExitList.Text) != null)
                    btnDoExitList.Text = resxSet.GetString(btnDoExitList.Text);

                if (resxSet.GetString(this.Text) != null)
                    this.Text = resxSet.GetString(this.Text);

                if (resxSet.GetString(radMenuItemSaveTasksExitList.Text) != null)
                    radMenuItemSaveTasksExitList.Text = resxSet.GetString(radMenuItemSaveTasksExitList.Text);

                //ReasonIn
                if (resxSet.GetString(lblReasonInDtFrom.Text) != null)
                    lblReasonInDtFrom.Text = resxSet.GetString(lblReasonInDtFrom.Text);

                if (resxSet.GetString(lblReasonIn.Text) != null)
                    lblReasonIn.Text = resxSet.GetString(lblReasonIn.Text);

                if (resxSet.GetString(lblDtToReasonIn.Text) != null)
                    lblDtToReasonIn.Text = resxSet.GetString(lblDtToReasonIn.Text);

                if (resxSet.GetString(btnPrintReasonIn.Text) != null)
                    btnPrintReasonIn.Text = resxSet.GetString(btnPrintReasonIn.Text);

                if (resxSet.GetString(lblFunctionReasonIn.Text) != null)
                    lblFunctionReasonIn.Text = resxSet.GetString(lblFunctionReasonIn.Text);

                //Reason Out
                if (resxSet.GetString(btnPrintRo.Text) != null)
                    btnPrintRo.Text = resxSet.GetString(btnPrintRo.Text);

                if (resxSet.GetString(lblFunctionsRO.Text) != null)
                    lblFunctionsRO.Text = resxSet.GetString(lblFunctionsRO.Text);

                if (resxSet.GetString(lblReasonOutRo.Text) != null)
                    lblReasonOutRo.Text = resxSet.GetString(lblReasonOutRo.Text);

                if (resxSet.GetString(lblDateFromRO.Text) != null)
                    lblDateFromRO.Text = resxSet.GetString(lblDateFromRO.Text);

                if (resxSet.GetString(lblDateToRO.Text) != null)
                    lblDateToRO.Text = resxSet.GetString(lblDateToRO.Text);

                //Reason Out   //Age list
                if (resxSet.GetString(lblDateAl.Text) != null)
                    lblDateAl.Text = resxSet.GetString(lblDateAl.Text);

                if (resxSet.GetString(lblAgeFromAl.Text) != null)
                    lblAgeFromAl.Text = resxSet.GetString(lblAgeFromAl.Text);

                if (resxSet.GetString(lblAgeToAl.Text) != null)
                    lblAgeToAl.Text = resxSet.GetString(lblAgeToAl.Text);

                if (resxSet.GetString(lblFunctionsAl.Text) != null)
                    lblFunctionsAl.Text = resxSet.GetString(lblFunctionsAl.Text);

                if (resxSet.GetString(btnPrintAl.Text) != null)
                    btnPrintAl.Text = resxSet.GetString(btnPrintAl.Text);
                //AgeList

                //Unique volunteers 
                if (resxSet.GetString(lblDtFromUnique.Text) != null)
                    lblDtFromUnique.Text = resxSet.GetString(lblDtFromUnique.Text);

                if (resxSet.GetString(lblDtToUnique.Text) != null)
                    lblDtToUnique.Text = resxSet.GetString(lblDtToUnique.Text);

                if (resxSet.GetString(lblFunctionUnique.Text) != null)
                    lblFunctionUnique.Text = resxSet.GetString(lblFunctionUnique.Text);

                if (resxSet.GetString(btnPrintUnique.Text) != null)
                    btnPrintUnique.Text = resxSet.GetString(btnPrintUnique.Text);

                //NOT BOOKED
                if (resxSet.GetString(lblDateFromNotBooked.Text) != null)
                    lblDateFromNotBooked.Text = resxSet.GetString(lblDateFromNotBooked.Text);

                if (resxSet.GetString(lblDateToNotBooked.Text) != null)
                    lblDateToNotBooked.Text = resxSet.GetString(lblDateToNotBooked.Text);

                if (resxSet.GetString(lblFunctionsNotBooked.Text) != null)
                    lblFunctionsNotBooked.Text = resxSet.GetString(lblFunctionsNotBooked.Text);

                if (resxSet.GetString(btnDoNotBooked.Text) != null)
                    btnDoNotBooked.Text = resxSet.GetString(btnDoNotBooked.Text);

                if (resxSet.GetString(radMenuItemSaveTasksNotBooked.Text) != null)
                    radMenuItemSaveTasksNotBooked.Text = resxSet.GetString(radMenuItemSaveTasksNotBooked.Text);
                //All Booking
                if (resxSet.GetString(lblDateFromAb.Text) != null)
                    lblDateFromAb.Text = resxSet.GetString(lblDateFromAb.Text);

                if (resxSet.GetString(lblDtToAB.Text) != null)
                    lblDtToAB.Text = resxSet.GetString(lblDtToAB.Text);

                if (resxSet.GetString(lblFunctionsAB.Text) != null)
                    lblFunctionsAB.Text = resxSet.GetString(lblFunctionsAB.Text);

                if (resxSet.GetString(btnPrintAb.Text) != null)
                    btnPrintAb.Text = resxSet.GetString(btnPrintAb.Text);
                // Za prevod tabova
                for (int i = 0; i < radPageVolPreselection.Pages.Count; i++)
                {
                    if (resxSet.GetString(radPageVolPreselection.Pages[i].Text) != null)
                        radPageVolPreselection.Pages[i].Text = resxSet.GetString(radPageVolPreselection.Pages[i].Text);
                }

            }
        }

        #region Availability with skills


        private void rck_CheckStateChanged(object sender, EventArgs e)
        {

            RadRadioButton rb = (RadRadioButton)sender;
            label = new List<int>();
            if (rb.Name.Replace("chkLabel", "") != "")
                label.Add(Convert.ToInt32(rb.Name.Replace("chkLabel", "")));
            else
            {
                //Za odredjene labele uzima ID i prikazuje u lookup-u
                for (int i = 0; i < Login._arrLabels.Count; i++)
                {

                    label.Add(Login._arrLabels[i].idLabel);
                }
            }

            //Reset 
            volSkillsList = new List<VolAvailabilityPreselectionModel>();
            volFunctionList = new List<VolAvailabilityPreselectionModel>();
            volTripList = new List<VolAvailabilityPreselectionModel>();
            panelSkills.Controls.Clear();
            //Za reset autoScroll-a da se ne pojavi kada je panel prazan prilikom promene labele                    
            panelSkills.AutoScroll = true;
            panelSkills.VerticalScroll.Enabled = false;
            panelFunctions.Controls.Clear();
            panelFunctions.AutoScroll = true;
            panelFunctions.VerticalScroll.Enabled = false;
            panelPreferences.Controls.Clear();

            //Za reset autoScroll-a da se ne pojavi kada je panel prazan prilikom promene labele          
            panelFunctions.AutoScroll = true;
            panelFunctions.VerticalScroll.Enabled = false;

        }



        private void btnTripPrferences_Click(object sender, EventArgs e)
        {
            VolAvailabilityPreselectionBUS ccentar3g = new VolAvailabilityPreselectionBUS();
            List<VolAvailabilityPreselectionModel> gmX3g = new List<VolAvailabilityPreselectionModel>();


            gmX3g = ccentar3g.GetAllTripPreferences(label);
            using (var dlgSave3g = new GridLookupFormPreselection(volTripList, gmX3g, "Preferences"))
            {
                volTripList = new List<VolAvailabilityPreselectionModel>();

                if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
                {
                    panelPreferences.Controls.Clear();

                    volTripList = new List<VolAvailabilityPreselectionModel>();
                    volTripList = dlgSave3g.volList;

                    RadLabel rl = new RadLabel();
                    int Y = 5;
                    for (int i = 0; i < volTripList.Count; i++)
                    {
                        rl = new RadLabel();
                        rl.Font = new Font("Verdana", 9);
                        rl.Text = volTripList[i].txtQuest;
                        rl.Location = new Point(0, Y);
                        rl.AutoSize = true;
                        Y = Y + 3 + rl.Height;


                        panelPreferences.Controls.Add(rl);

                    }

                }

            }
        }

        private void btnSkils_Click(object sender, EventArgs e)
        {


            VolAvailabilityPreselectionBUS ccentar3g = new VolAvailabilityPreselectionBUS();
            List<VolAvailabilityPreselectionModel> gmX3g = new List<VolAvailabilityPreselectionModel>();

            gmX3g = ccentar3g.GetAllSkills(label);

            using (var dlgSave3g = new GridLookupFormPreselection(volSkillsList, gmX3g, "Skills"))
            {
                //Reset panela kada se sacuva pa odcekira
                volSkillsList = new List<VolAvailabilityPreselectionModel>();

                if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
                {
                    //Reset panela kada se sacuva pa odcekira
                    panelSkills.Controls.Clear();

                    volSkillsList = new List<VolAvailabilityPreselectionModel>();
                    volSkillsList = dlgSave3g.volList;

                    RadLabel rl = new RadLabel();
                    int Y = 5;
                    for (int i = 0; i < volSkillsList.Count; i++)
                    {
                        rl = new RadLabel();
                        rl.Font = new Font("Verdana", 9);
                        rl.Text = volSkillsList[i].txtQuest;
                        rl.Location = new Point(0, Y);
                        rl.AutoSize = true;
                        Y = Y + 3 + rl.Height;


                        panelSkills.Controls.Add(rl);


                    }


                }


            }

        }

        private void btnFunctions_Click(object sender, EventArgs e)
        {
            VolAvailabilityPreselectionBUS ccentar3g = new VolAvailabilityPreselectionBUS();
            List<VolAvailabilityPreselectionModel> gmX3g = new List<VolAvailabilityPreselectionModel>();

            gmX3g = ccentar3g.GetAllFunction(label);
            using (var dlgSave3g = new GridLookupFormPreselection(volFunctionList, gmX3g, "Function"))
            {
                //Reset panela kada se sacuva pa odcekira
                volFunctionList = new List<VolAvailabilityPreselectionModel>();


                if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
                {
                    //Reset panela kada se sacuva pa odcekira
                    panelFunctions.Controls.Clear();

                    volFunctionList = new List<VolAvailabilityPreselectionModel>();
                    volFunctionList = dlgSave3g.volList;

                    RadLabel rl = new RadLabel();
                    int Y = 5;
                    for (int i = 0; i < volFunctionList.Count; i++)
                    {
                        rl = new RadLabel();
                        rl.Font = new Font("Verdana", 9);
                        rl.Text = volFunctionList[i].txtQuest;
                        rl.Location = new Point(0, Y);
                        rl.AutoSize = true;
                        Y = Y + 3 + rl.Height;

                        panelFunctions.Controls.Add(rl);

                    }
                }

            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            btnVolAvaPrint.Visible = false; // sakrivanje Print dugmeta
            btnExportToExcel.Visible = false;

            rgvPreselection.DataSource = null;
            VolAvailabilityPreselectionBUS bus = new VolAvailabilityPreselectionBUS();
            List<VolAvailabilityContPersPreselectionModel> listaVolAva = new List<VolAvailabilityContPersPreselectionModel>();

            if (!SaveAndValidateRo(pickerDtFrom.Value, pickerDtTo.Value))
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("You need to enter dates!") != null)
                        RadMessageBox.Show(resxSet.GetString("You need to enter dates!"));
                    else
                        RadMessageBox.Show("You need to enter dates!");
                }
                return;
            }
            else
            {
                listaVolAva = bus.GetContactPersonPreselection(pickerDtFrom.Value, pickerDtTo.Value, volFunctionList, volSkillsList, volTripList, label);

                if (listaVolAva != null)
                {
                    #region tel
                    PersonTelBUS ptb = new PersonTelBUS();
                    List<PersonTelModel> telAll = new List<PersonTelModel>();
                    telAll = ptb.GetAllPersonTels();

                    if (telAll != null)
                    {
                        if (listaVolAva != null)
                        {
                            for (int i = 0; i < listaVolAva.Count; i++)
                            {
                                List<PersonTelModel> telForPerson = new List<PersonTelModel>();
                                telForPerson = telAll.FindAll(s => s.idContPers == listaVolAva[i].idContPers);

                                if (telForPerson != null)
                                {
                                    string tel = "";
                                    for (int j = 0; j < telForPerson.Count; j++)
                                    {
                                        if (telForPerson[j].descriptionTel != "")
                                            tel = tel + telForPerson[j].descriptionTel + ": ";

                                        if (telForPerson[j].numberTel != "")
                                        {
                                            if (j == telForPerson.Count - 1)
                                                tel = tel + telForPerson[j].numberTel;
                                            else
                                                tel = tel + telForPerson[j].numberTel + ", ";
                                        }
                                    }
                                    if (tel != "")
                                        listaVolAva[i].numberTel = tel;

                                }


                            }
                        }
                    }
                    #endregion

                    #region email
                    //Odavde

                    PersonEmailBUS peb = new PersonEmailBUS();
                    List<PersonEmailModel> emailAll = new List<PersonEmailModel>();
                    emailAll = peb.GetAllPersonEmails();

                    if (emailAll != null)
                    {
                        if (listaVolAva != null)
                        {
                            for (int i = 0; i < listaVolAva.Count; i++)
                            {
                                List<PersonEmailModel> emailForPerson = new List<PersonEmailModel>();
                                emailForPerson = emailAll.FindAll(s => s.idContPers == listaVolAva[i].idContPers);

                                if (emailForPerson != null)
                                {
                                    string emails = "";
                                    for (int j = 0; j < emailForPerson.Count; j++)
                                    {

                                        if (emailForPerson[j].email != "")
                                        {
                                            if (j == emailForPerson.Count - 1)
                                                emails = emails + emailForPerson[j].email;
                                            else
                                                emails = emails + emailForPerson[j].email + ", ";
                                        }
                                    }
                                    if (emails != "")
                                        listaVolAva[i].email = emails;

                                }


                            }
                        }
                    }
                    #endregion

                    #region function
                    VolontaryFunctionModel vfm = new VolontaryFunctionModel();
                    VolontaryFunctionBUS vfBus = new VolontaryFunctionBUS();
                    List<VolontaryFunctionModel> functionAll = new List<VolontaryFunctionModel>();
                    functionAll = vfBus.GetAllFromVolFunction();

                    if (functionAll != null)
                    {
                        if (listaVolAva != null)
                        {
                            for (int i = 0; i < listaVolAva.Count; i++)
                            {
                                List<VolontaryFunctionModel> functionForPerson = new List<VolontaryFunctionModel>();
                                functionForPerson = functionAll.FindAll(s => s.idcpr == listaVolAva[i].idContPers);

                                if (functionForPerson != null)
                                {
                                    string func = "";
                                    for (int j = 0; j < functionForPerson.Count; j++)
                                    {
                                        if (j == functionForPerson.Count - 1)
                                            func = func + functionForPerson[j].txtQuest;
                                        else
                                            func = func + functionForPerson[j].txtQuest + ", ";
                                    }
                                    if (func != "")
                                        listaVolAva[i].function = func;

                                }


                            }
                        }
                    }
                    #endregion

                    rgvPreselection.DataSource = listaVolAva;

                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("No data for curent conditions!") != null)
                            RadMessageBox.Show(resxSet.GetString("No data for curent conditions!"));
                        else
                            RadMessageBox.Show("No data for curent conditions!");
                    }
                }
            }

        }

        private void rgvPreselection_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {

            if (rgvPreselection != null)
            {
                if (rgvPreselection.Columns.Count > 0)
                {
                    foreach (var column in rgvPreselection.Columns)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString(column.HeaderText) != null)
                                column.HeaderText = resxSet.GetString(column.HeaderText);
                        }

                        column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 95);

                    }
                }
                btnVolAvaPrint.Visible = true;
                btnExportToExcel.Visible = true;
            }

            else
            {
                btnVolAvaPrint.Visible = false;
                btnExportToExcel.Visible = false;
            }


            if (this.rgvPreselection.Columns != null)
            {
                if (this.rgvPreselection.RowCount > 0)
                {
                    if (this.rgvPreselection.Columns["nameGender"] != null)
                        this.rgvPreselection.Columns["nameGender"].IsVisible = false;
                }
            }

            if (File.Exists(layout))
            {
                rgvPreselection.LoadLayout(layout);
            }


        }


        private void radMenuItemSaveTasksLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layout))
            {
                File.Delete(layout);
            }
            rgvPreselection.SaveLayout(layout);

            RadMessageBox.Show("Layout Saved");
        }

        private void rgvPreselection_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.rgvPreselection.CurrentRow;
            VolAvailabilityContPersPreselectionModel vam = new VolAvailabilityContPersPreselectionModel();
            vam = (VolAvailabilityContPersPreselectionModel)info.DataBoundItem;

            VolAvailabilityContPersPreselectionModel volaAvaModel = (VolAvailabilityContPersPreselectionModel)info.DataBoundItem;

            PersonModel amodel = new PersonModel();
            amodel = new PersonBUS().GetPerson(vam.idContPers);

            if (info != null && e.RowIndex >= 0)
                if (volaAvaModel != null)
                {
                    frmPerson frm = new frmPerson(amodel);

                    frm.ShowDialog();

                }
        }

        private void rgvPreselection_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.rgvPreselection.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    GridViewRowInfo info = this.rgvPreselection.CurrentRow;
                    VolAvailabilityContPersPreselectionModel vam = new VolAvailabilityContPersPreselectionModel();
                    vam = (VolAvailabilityContPersPreselectionModel)info.DataBoundItem;

                    VolAvailabilityContPersPreselectionModel volaAvaModel = (VolAvailabilityContPersPreselectionModel)info.DataBoundItem;

                    PersonModel amodel = new PersonModel();
                    amodel = new PersonBUS().GetPerson(vam.idContPers);


                    frmPerson frm = new frmPerson(amodel);

                    frm.ShowDialog();

                    return;

                }

            }
        }

        private void btnVolAvaPrint_Click(object sender, EventArgs e)
        {
            if (rgvPreselection != null)
                if (rgvPreselection.Columns.Count > 0)
                {

                    this.rgvPreselection.PrintPreview();
                }
                else
                {
                    RadMessageBox.Show("Nothing for print preview!");
                }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (rgvPreselection.DataSource != null)
                if (rgvPreselection.Columns.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.InitialDirectory = @"C:\";
                    sfd.RestoreDirectory = true;
                    sfd.FileName = radPageVolPreselection.SelectedPage.Text;
                    sfd.DefaultExt = "xls";
                    sfd.Filter = "Excel file (*.xls) | *.xls";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string path = sfd.FileName;
                        ExportToExcelML exporter = new ExportToExcelML(this.rgvPreselection);
                        exporter.SheetMaxRows = ExcelMaxRows._1048576;

                        exporter.RunExport(path);
                        RadMessageBox.Show("Export to excel!");


                    }
                }

            ////RADI STARO
            //if (rgvPreselection.DataSource != null)
            //    if(rgvPreselection.Columns.Count>0)
            //{
            //    string path = @"C:\Users\Goran\Desktop\sample.xls";
            //    ExportToExcelML exporter = new ExportToExcelML(this.rgvPreselection);
            //    exporter.SheetMaxRows = ExcelMaxRows._1048576;

        }

        #endregion

        #region ExpirationDate

        private void loadLabelExpiried()
        {

            int Y = 0;
            int X = 0;
            RadRadioButton rckExp = new RadRadioButton();
            rckExp.Font = new Font("Verdana", 9);
            rckExp.Name = "chkLabel";
            rckExp.Text = "None";
            rckExp.Location = new Point(0, Y);
            rckExp.CheckStateChanged += rckExp_CheckStateChangedExpiried;
            rckExp.AutoSize = true;
            rckExp.IsChecked = true;

            Y = Y + 3 + rckExp.Height;


            panelLabelExpaired.Controls.Add(rckExp);



            Y = 21;

            for (int i = 0; i < Login._arrLabels.Count; i++)
            {
                rckExp = new RadRadioButton();
                rckExp.Font = new Font("Verdana", 9);
                rckExp.Name = "chkLabel" + Login._arrLabels[i].idLabel.ToString();
                rckExp.Text = Login._arrLabels[i].nameLabel;
                rckExp.Location = new Point(0, Y);
                rckExp.CheckStateChanged += rck_CheckStateChanged;
                rckExp.AutoSize = true;
                Y = Y + 3 + rckExp.Height;

                panelLabelExpaired.Controls.Add(rckExp);

            }
        }

        private void rckExp_CheckStateChangedExpiried(object sender, EventArgs e)
        {

            RadRadioButton rb = (RadRadioButton)sender;
            labelExpiried = new List<int>();
            if (rb.Name.Replace("chkLabel", "") != "")
                labelExpiried.Add(Convert.ToInt32(rb.Name.Replace("chkLabel", "")));
            else
            {
                //Za odredjene labele uzima ID i rpokazuje u lookup-u
                for (int i = 0; i < Login._arrLabels.Count; i++)
                {

                    labelExpiried.Add(Login._arrLabels[i].idLabel);
                }
            }



        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            btnPrintExpirance.Visible = false;
            btnExportToExcelExpirance.Visible = false;

            Cursor.Current = Cursors.WaitCursor;
            if (!SaveAndValidateRo(dtFromExpiration.Value, dtToExpiration.Value))
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("You need to enter dates!") != null)
                        RadMessageBox.Show(resxSet.GetString("You need to enter dates!"));
                    else
                        RadMessageBox.Show("You need to enter dates!");
                }
                return;
            }
            else
            {

                dateFrom = Convert.ToDateTime(dtFromExpiration.Value);
                dateTo = Convert.ToDateTime(dtToExpiration.Value);
                dateExpiration = Convert.ToDateTime(dtExpiration.Value);
                VolAvailabilityPreselectionBUS bus = new VolAvailabilityPreselectionBUS();
                DataTable dt = new DataTable();
                rgvDateExp.DataSource = null;

                if (dateFrom <= dateTo)
                {
                    List<VolVogCokGokPassModel> listaVolAva = new List<VolVogCokGokPassModel>();
                    if (rbnVogExpiration.CheckState == CheckState.Checked)
                    {
                        lista = bus.GetGOK(dateFrom, dateTo, dateExpiration, labelExpiried);
                        rgvDateExp.DataSource = lista;
                        listaVolAva = bus.GetGOK(dateFrom, dateTo, dateExpiration, labelExpiried);
                    }
                    if (rbnVokExpiration.CheckState == CheckState.Checked)
                    {
                        lista = bus.GetVOK(dateFrom, dateTo, dateExpiration, labelExpiried);
                        rgvDateExp.DataSource = lista;
                        listaVolAva = bus.GetVOK(dateFrom, dateTo, dateExpiration, labelExpiried);

                    }
                    if (rbnCokExpiration.CheckState == CheckState.Checked)
                    {
                        lista = bus.GetCok(dateFrom, dateTo, dateExpiration, labelExpiried);
                        rgvDateExp.DataSource = lista;
                        listaVolAva = bus.GetCok(dateFrom, dateTo, dateExpiration, label);

                    }
                    if (rbnPassExpiration.CheckState == CheckState.Checked)
                    {
                        lista = bus.GetPassport(dateFrom, dateTo, dateExpiration, labelExpiried);
                        rgvDateExp.DataSource = lista;
                        listaVolAva = bus.GetPassport(dateFrom, dateTo, dateExpiration, labelExpiried);

                    }
                    if (rbNoneExpiration.CheckState == CheckState.Checked)
                    {
                        lista = bus.GetAllVokCokGokPass(dateFrom, dateTo, dateExpiration, labelExpiried);
                        rgvDateExp.DataSource = lista;
                        listaVolAva = bus.GetAllVokCokGokPass(dateFrom, dateTo, dateExpiration, labelExpiried);
                    }
                    if (listaVolAva != null)
                    {

                        #region telExp
                        PersonTelBUS ptb = new PersonTelBUS();
                        List<PersonTelModel> telAll = new List<PersonTelModel>();
                        telAll = ptb.GetAllPersonTels();

                        if (telAll != null)
                        {
                            if (listaVolAva != null)
                            {
                                for (int i = 0; i < listaVolAva.Count; i++)
                                {
                                    List<PersonTelModel> telForPerson = new List<PersonTelModel>();
                                    telForPerson = telAll.FindAll(s => s.idContPers == listaVolAva[i].idContPers);

                                    if (telForPerson != null)
                                    {
                                        string tel = "";
                                        for (int j = 0; j < telForPerson.Count; j++)
                                        {
                                            if (telForPerson[j].descriptionTel != "")
                                                tel = tel + telForPerson[j].descriptionTel + ": ";

                                            if (telForPerson[j].numberTel != "")
                                            {
                                                if (j == telForPerson.Count - 1)
                                                    tel = tel + telForPerson[j].numberTel;
                                                else
                                                    tel = tel + telForPerson[j].numberTel + ", ";
                                            }
                                        }
                                        if (tel != "")
                                            listaVolAva[i].phone = tel;

                                    }


                                }
                            }
                        }
                        #endregion

                        #region emailExp
                        //Odavde

                        PersonEmailBUS peb = new PersonEmailBUS();
                        List<PersonEmailModel> emailAll = new List<PersonEmailModel>();
                        emailAll = peb.GetAllPersonEmails();

                        if (emailAll != null)
                        {
                            if (listaVolAva != null)
                            {
                                for (int i = 0; i < listaVolAva.Count; i++)
                                {
                                    List<PersonEmailModel> emailForPerson = new List<PersonEmailModel>();
                                    emailForPerson = emailAll.FindAll(s => s.idContPers == listaVolAva[i].idContPers);

                                    if (emailForPerson != null)
                                    {
                                        string emails = "";
                                        for (int j = 0; j < emailForPerson.Count; j++)
                                        {

                                            if (emailForPerson[j].email != "")
                                            {
                                                if (j == emailForPerson.Count - 1)
                                                    emails = emails + emailForPerson[j].email;
                                                else
                                                    emails = emails + emailForPerson[j].email + ", ";
                                            }
                                        }
                                        if (emails != "")
                                            listaVolAva[i].email = emails;

                                    }


                                }
                            }
                        }
                        #endregion

                        rgvDateExp.DataSource = listaVolAva;

                        rgvDateExp.Columns["Age"].IsVisible = false;
                        //   rgvDateExp.Columns["Gender"].IsVisible = false;
                        rgvDateExp.Columns["Function"].IsVisible = false;

                        if (File.Exists(layoutExpDate))
                        {
                            rgvDateExp.LoadLayout(layoutExpDate);
                        }
                    }
                    else
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("No data for curent conditions!") != null)
                                RadMessageBox.Show(resxSet.GetString("No data for curent conditions!"));
                            else
                                RadMessageBox.Show("No data for curent conditions!");
                        }
                    }

                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Date is out of range.");
                }

            }

            Cursor.Current = Cursors.Default;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void gridDateExp_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
            if (rgvDateExp.DataSource != null)
            {
                if (rgvDateExp.Columns.Count > 0)
                {

                    for (int i = 0; i < rgvDateExp.Columns.Count; i++)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (rgvDateExp.Columns[i].HeaderText != null && resxSet.GetString(rgvDateExp.Columns[i].HeaderText) != null)
                                rgvDateExp.Columns[i].HeaderText = resxSet.GetString(rgvDateExp.Columns[i].HeaderText);
                        }
                        rgvDateExp.Columns[i].Width = (int)(this.CreateGraphics().MeasureString(rgvDateExp.Columns[i].HeaderText, this.Font).Width + 101);

                    }
                }

                if (this.rgvDateExp.Columns != null)
                {
                    if (this.rgvDateExp.RowCount > 0)
                    {
                        if (this.rgvDateExp.Columns["NrTravel"] != null)
                            this.rgvDateExp.Columns["NrTravel"].IsVisible = false;
                    }
                }

                if (rgvDateExp.Columns != null)
                {
                    if (rgvDateExp.RowCount > 0)
                    {
                        this.rgvDateExp.Columns["dateExpiried"].FormatString = "{0: dd-MM-yyyy}";
                        
                    }
                }

                btnPrintExpirance.Visible = true;
                btnExportToExcelExpirance.Visible = true;
            }
            else
            {
                btnPrintExpirance.Visible = false;
                btnExportToExcelExpirance.Visible = false;
            }

        }
        private void gridDateExp_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            GridViewRowInfo info = this.rgvDateExp.CurrentRow;
            VolVogCokGokPassModel vam = new VolVogCokGokPassModel();
            vam = (VolVogCokGokPassModel)info.DataBoundItem;

            VolVogCokGokPassModel volModel = (VolVogCokGokPassModel)info.DataBoundItem;

            PersonModel amodel = new PersonModel();
            amodel = new PersonBUS().GetPerson(vam.idContPers);

            if (info != null && e.RowIndex >= 0)
                if (volModel != null)
                {
                    frmPerson frm = new frmPerson(amodel);

                    frm.ShowDialog();

                }
            Cursor.Current = Cursors.Default;
        }

        private void gridDataExp_KeyDown(object sender, KeyEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (this.rgvDateExp.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    GridViewRowInfo info = this.rgvPreselection.CurrentRow;
                    VolVogCokGokPassModel vam = new VolVogCokGokPassModel();
                    vam = (VolVogCokGokPassModel)info.DataBoundItem;

                    VolVogCokGokPassModel volaAvaModel = (VolVogCokGokPassModel)info.DataBoundItem;

                    PersonModel amodel = new PersonModel();
                    amodel = new PersonBUS().GetPerson(vam.idContPers);


                    frmPerson frm = new frmPerson(amodel);

                    frm.ShowDialog();

                    return;

                }

            }
            Cursor.Current = Cursors.Default;
        }
        private void radMenuItemSaveTasksExpDate_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutExpDate))
            {
                File.Delete(layoutExpDate);
            }
            rgvDateExp.SaveLayout(layoutExpDate);

            RadMessageBox.Show("Layout Saved");
        }

        private void btnPrintExpirance_Click(object sender, System.EventArgs e)
        {
            if (rgvDateExp != null)
                if (rgvDateExp.Columns.Count > 0)
                {

                    this.rgvDateExp.PrintPreview();
                }
                else
                {
                    RadMessageBox.Show("Nothing for print preview!");
                }
        }

        private void btnExportToExcelExpirance_Click(object sender, System.EventArgs e)
        {
            if (rgvDateExp.DataSource != null)
                if (rgvDateExp.Columns.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.InitialDirectory = @"C:\";
                    sfd.RestoreDirectory = true;
                    sfd.FileName = radPageVolPreselection.SelectedPage.Text;
                    sfd.DefaultExt = "xls";
                    sfd.Filter = "Excel file (*.xls) | *.xls";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string path = sfd.FileName;
                        ExportToExcelML exporter = new ExportToExcelML(this.rgvDateExp);
                        exporter.SheetMaxRows = ExcelMaxRows._1048576;

                        exporter.RunExport(path);
                        RadMessageBox.Show("Export to excel!");


                    }
                }
        }

        #  endregion


        # region ExitList

        private void loadLabelExitList()
        {

            int Y = 0;
            int X = 0;
            RadRadioButton rckExp = new RadRadioButton();
            rckExp.Font = new Font("Verdana", 9);
            rckExp.Name = "chkLabel";
            rckExp.Text = "None";
            rckExp.Location = new Point(0, Y);
            rckExp.CheckStateChanged += rck_CheckStateChangedExitList;
            rckExp.AutoSize = true;
            rckExp.IsChecked = true;

            Y = Y + 3 + rckExp.Height;


            panelLabelExitList.Controls.Add(rckExp);



            Y = 21;

            for (int i = 0; i < Login._arrLabels.Count; i++)
            {
                rckExp = new RadRadioButton();
                rckExp.Font = new Font("Verdana", 9);
                rckExp.Name = "chkLabel" + Login._arrLabels[i].idLabel.ToString();
                rckExp.Text = Login._arrLabels[i].nameLabel;
                rckExp.Location = new Point(0, Y);
                rckExp.CheckStateChanged += rck_CheckStateChangedExitList;
                rckExp.AutoSize = true;
                Y = Y + 3 + rckExp.Height;

                panelLabelExitList.Controls.Add(rckExp);

            }

        }

        private void rck_CheckStateChangedExitList(object sender, EventArgs e)
        {

            RadRadioButton rb = (RadRadioButton)sender;
            labelExitList = new List<int>();
            if (rb.Name.Replace("chkLabel", "") != "")
                labelExitList.Add(Convert.ToInt32(rb.Name.Replace("chkLabel", "")));
            else
            {
                //Za odredjene labele uzima ID i rpokazuje u lookup-u
                for (int i = 0; i < Login._arrLabels.Count; i++)
                {

                    labelExitList.Add(Login._arrLabels[i].idLabel);
                }
            }

            volFunctionListExit = new List<VolAvailabilityPreselectionModel>();
            panelFilterExitList.Controls.Clear();

        }

        private void rgvExitList_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
            if (rgvExitlist.DataSource != null)
            {
                if (rgvExitlist.Columns.Count > 0)
                {
                    for (int i = 0; i < rgvExitlist.Columns.Count; i++)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (rgvExitlist.Columns[i].HeaderText != null && resxSet.GetString(rgvExitlist.Columns[i].HeaderText) != null)
                                rgvExitlist.Columns[i].HeaderText = resxSet.GetString(rgvExitlist.Columns[i].HeaderText);
                        }
                        rgvExitlist.Columns[i].Width = (int)(this.CreateGraphics().MeasureString(rgvExitlist.Columns[i].HeaderText, this.Font).Width + 101);



                    }
                }

                btnPrintExitList.Visible = true;
                btnExportToExcelExitList.Visible = true;
            }
            else
            {
                btnPrintExitList.Visible = false;
                btnExportToExcelExitList.Visible = false;
            }


        }

        private void btnDoExitList_Click(object sender, EventArgs e)
        {
            btnPrintExitList.Visible = false;
            btnExportToExcelExitList.Visible = false;

            rgvExitlist.DataSource = null;
            Cursor.Current = Cursors.WaitCursor;
            Cursor.Current = Cursors.WaitCursor;
            if (!SaveAndValidateRo(dtFromExpiration.Value, dtToExpiration.Value))
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("You need to enter dates!") != null)
                        RadMessageBox.Show(resxSet.GetString("You need to enter dates!"));
                    else
                        RadMessageBox.Show("You need to enter dates!");
                }
                return;
            }
            else
            {

                dateFromExit = Convert.ToDateTime(dtFromExit.Value);
                dateToExit = Convert.ToDateTime(dtToExit.Value);

                VolAvailabilityPreselectionBUS bus = new VolAvailabilityPreselectionBUS();
                // DataTable dt = new DataTable();
                rgvExitlist.DataSource = null;

                List<VolVogCokGokPassModel> listaVolAva = new List<VolVogCokGokPassModel>();
                if (dateFromExit <= dateToExit)
                {
                    lista = bus.GeExitListData(dateFromExit, dateToExit, volFunctionListExit, labelExitList);
                    rgvExitlist.DataSource = lista;
                    listaVolAva = lista;
                    if (listaVolAva != null)
                    {
                        #region function
                        VolontaryFunctionModel vfm = new VolontaryFunctionModel();
                        VolontaryFunctionBUS vfBus = new VolontaryFunctionBUS();
                        List<VolontaryFunctionModel> functionAll = new List<VolontaryFunctionModel>();
                        functionAll = vfBus.GetAllFromVolFunction();

                        if (functionAll != null)
                        {
                            if (listaVolAva != null)
                            {
                                for (int i = 0; i < listaVolAva.Count; i++)
                                {
                                    List<VolontaryFunctionModel> functionForPerson = new List<VolontaryFunctionModel>();
                                    functionForPerson = functionAll.FindAll(s => s.idcpr == listaVolAva[i].idContPers);

                                    if (functionForPerson != null)
                                    {
                                        string func = "";
                                        for (int j = 0; j < functionForPerson.Count; j++)
                                        {
                                            if (j == functionForPerson.Count - 1)
                                                func = func + functionForPerson[j].txtQuest;
                                            else
                                                func = func + functionForPerson[j].txtQuest + ", ";
                                        }
                                        if (func != "")
                                            listaVolAva[i].function = func;

                                    }


                                }
                            }
                        }
                        #endregion

                        #region telExp
                        PersonTelBUS ptb = new PersonTelBUS();
                        List<PersonTelModel> telAll = new List<PersonTelModel>();
                        telAll = ptb.GetAllPersonTels();

                        if (telAll != null)
                        {
                            if (listaVolAva != null)
                            {
                                for (int i = 0; i < listaVolAva.Count; i++)
                                {
                                    List<PersonTelModel> telForPerson = new List<PersonTelModel>();
                                    telForPerson = telAll.FindAll(s => s.idContPers == listaVolAva[i].idContPers);

                                    if (telForPerson != null)
                                    {
                                        string tel = "";
                                        for (int j = 0; j < telForPerson.Count; j++)
                                        {
                                            if (telForPerson[j].descriptionTel != "")
                                                tel = tel + telForPerson[j].descriptionTel + ": ";

                                            if (telForPerson[j].numberTel != "")
                                            {
                                                if (j == telForPerson.Count - 1)
                                                    tel = tel + telForPerson[j].numberTel;
                                                else
                                                    tel = tel + telForPerson[j].numberTel + ", ";
                                            }
                                        }
                                        if (tel != "")
                                            listaVolAva[i].phone = tel;

                                    }


                                }
                            }
                        }
                        #endregion

                        #region emailExp
                        //Odavde

                        PersonEmailBUS peb = new PersonEmailBUS();
                        List<PersonEmailModel> emailAll = new List<PersonEmailModel>();
                        emailAll = peb.GetAllPersonEmails();

                        if (emailAll != null)
                        {
                            if (listaVolAva != null)
                            {
                                for (int i = 0; i < listaVolAva.Count; i++)
                                {
                                    List<PersonEmailModel> emailForPerson = new List<PersonEmailModel>();
                                    emailForPerson = emailAll.FindAll(s => s.idContPers == listaVolAva[i].idContPers);

                                    if (emailForPerson != null)
                                    {
                                        string emails = "";
                                        for (int j = 0; j < emailForPerson.Count; j++)
                                        {

                                            if (emailForPerson[j].email != "")
                                            {
                                                if (j == emailForPerson.Count - 1)
                                                    emails = emails + emailForPerson[j].email;
                                                else
                                                    emails = emails + emailForPerson[j].email + ", ";
                                            }
                                        }
                                        if (emails != "")
                                            listaVolAva[i].email = emails;

                                    }


                                }
                            }
                        }
                        #endregion

                        rgvExitlist.DataSource = listaVolAva;

                        //rgvExitlist.Columns["Gender"].IsVisible = false;
                        rgvExitlist.Columns["dateExpiried"].IsVisible = false;
                        rgvExitlist.Columns["type"].IsVisible = false;
                    }
                    else
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("No data for curent conditions!") != null)
                                RadMessageBox.Show(resxSet.GetString("No data for curent conditions!"));
                            else
                                RadMessageBox.Show("No data for curent conditions!");
                        }
                    }
                    if (File.Exists(layoutExitlist))
                    {
                        rgvExitlist.LoadLayout(layoutExitlist);
                    }
                }
                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Date is out of range.");

                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void gridExitList_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            GridViewRowInfo info = this.rgvExitlist.CurrentRow;
            VolVogCokGokPassModel vam = new VolVogCokGokPassModel();
            vam = (VolVogCokGokPassModel)info.DataBoundItem;

            VolVogCokGokPassModel volModel = (VolVogCokGokPassModel)info.DataBoundItem;

            PersonModel amodel = new PersonModel();
            amodel = new PersonBUS().GetPerson(vam.idContPers);

            if (info != null && e.RowIndex >= 0)
                if (volModel != null)
                {
                    frmPerson frm = new frmPerson(amodel);

                    frm.ShowDialog();

                }
            Cursor.Current = Cursors.Default;
        }

        private void gridExitList_KeyDown(object sender, KeyEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (this.rgvExitlist.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    GridViewRowInfo info = this.rgvExitlist.CurrentRow;
                    VolVogCokGokPassModel vam = new VolVogCokGokPassModel();
                    vam = (VolVogCokGokPassModel)info.DataBoundItem;

                    VolVogCokGokPassModel volaAvaModel = (VolVogCokGokPassModel)info.DataBoundItem;

                    PersonModel amodel = new PersonModel();
                    amodel = new PersonBUS().GetPerson(vam.idContPers);


                    frmPerson frm = new frmPerson(amodel);

                    frm.ShowDialog();

                    return;

                }

            }
            Cursor.Current = Cursors.Default;
        }

        private void btnFunctionExitlist_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            VolAvailabilityPreselectionBUS ccentar3g = new VolAvailabilityPreselectionBUS();
            List<VolAvailabilityPreselectionModel> gmX3g = new List<VolAvailabilityPreselectionModel>();
            //   List<int> labelNone = new List<int>();
            gmX3g = ccentar3g.GetAllFunction(labelExitList);
            using (var dlgSave3g = new GridLookupFormPreselection(volFunctionListExit, gmX3g, "Functions"))
            {
                //Reset panela kada se sacuva pa odcekira
                volFunctionListExit = new List<VolAvailabilityPreselectionModel>();


                if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
                {
                    //Reset panela kada se sacuva pa odcekira
                    panelFilterExitList.Controls.Clear();

                    volFunctionListExit = new List<VolAvailabilityPreselectionModel>();
                    volFunctionListExit = dlgSave3g.volList;

                    RadLabel rl = new RadLabel();
                    int Y = 5;
                    for (int i = 0; i < volFunctionListExit.Count; i++)
                    {
                        rl = new RadLabel();
                        rl.Font = new Font("Verdana", 9);
                        rl.Text = volFunctionListExit[i].txtQuest;
                        rl.Location = new Point(0, Y);
                        rl.AutoSize = true;
                        Y = Y + 3 + rl.Height;

                        panelFilterExitList.Controls.Add(rl);

                    }
                }



            }
            Cursor.Current = Cursors.Default;
        }

        private void radMenuItemSaveTasksExitlist_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutExitlist))
            {
                File.Delete(layoutExitlist);
            }
            rgvExitlist.SaveLayout(layoutExitlist);

            RadMessageBox.Show("Layout Saved");
        }

        private void btnPrintExitList_Click(object sender, System.EventArgs e)
        {
            if (rgvExitlist != null)
                if (rgvExitlist.Columns.Count > 0)
                {

                    this.rgvExitlist.PrintPreview();
                }
                else
                {
                    RadMessageBox.Show("Nothing for print preview!");
                }
        }

        private void btnExportToExcelExitList_Click(object sender, System.EventArgs e)
        {
            if (rgvExitlist.DataSource != null)
                if (rgvExitlist.Columns.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.InitialDirectory = @"C:\";
                    sfd.RestoreDirectory = true;
                    sfd.FileName = radPageVolPreselection.SelectedPage.Text;
                    sfd.DefaultExt = "xls";
                    sfd.Filter = "Excel file (*.xls) | *.xls";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string path = sfd.FileName;
                        ExportToExcelML exporter = new ExportToExcelML(this.rgvExitlist);
                        exporter.SheetMaxRows = ExcelMaxRows._1048576;

                        exporter.RunExport(path);
                        RadMessageBox.Show("Export to excel!");


                    }
                }
        }

        # endregion


        #region ReasonIn

        private void loadLabelReasonIn()
        {

            int Y = 0;
            int X = 0;
            RadRadioButton rckReIn = new RadRadioButton();
            rckReIn.Font = new Font("Verdana", 9);
            rckReIn.Name = "chkLabel";
            rckReIn.Text = "None";
            rckReIn.Location = new Point(0, Y);
            rckReIn.CheckStateChanged += rckReIn_CheckStateChangedReasonIn;
            rckReIn.AutoSize = true;
            rckReIn.IsChecked = true;

            Y = Y + 3 + rckReIn.Height;


            panelReasonInLabel.Controls.Add(rckReIn);



            Y = 21;

            for (int i = 0; i < Login._arrLabels.Count; i++)
            {
                rckReIn = new RadRadioButton();
                rckReIn.Font = new Font("Verdana", 9);
                rckReIn.Name = "chkLabel" + Login._arrLabels[i].idLabel.ToString();
                rckReIn.Text = Login._arrLabels[i].nameLabel;
                rckReIn.Location = new Point(0, Y);
                rckReIn.CheckStateChanged += rckReIn_CheckStateChangedReasonIn;
                rckReIn.AutoSize = true;
                Y = Y + 3 + rckReIn.Height;

                panelReasonInLabel.Controls.Add(rckReIn);

            }

            //Za reset autoScroll-a da se ne pojavi kada je panel prazan prilikom promene labele          
            panelFunctionReasonIn.AutoScroll = true;
            panelFunctionReasonIn.VerticalScroll.Enabled = false;

        }

        private void rckReIn_CheckStateChangedReasonIn(object sender, EventArgs e)
        {
            RadRadioButton rbr = (RadRadioButton)sender;
            labelFunctionReasonIn = new List<int>();
            if (rbr.Name.Replace("chkLabel", "") != "")
                labelFunctionReasonIn.Add(Convert.ToInt32(rbr.Name.Replace("chkLabel", "")));
            else
            {
                //Za odredjene labele uzima ID i prikazuje u lookup-u
                for (int i = 0; i < Login._arrLabels.Count; i++)
                {

                    labelFunctionReasonIn.Add(Login._arrLabels[i].idLabel);
                }
            }


            volFunctionListRI = new List<VolAvailabilityPreselectionModel>();
            panelFunctionReasonIn.Controls.Clear();

            //Za reset autoScroll-a da se ne pojavi kada je panel prazan prilikom promene labele                    
            panelFunctionReasonIn.AutoScroll = true;
            panelFunctionReasonIn.VerticalScroll.Enabled = false;
        }

        private void btnLookupReasonIn_Click(object sender, EventArgs e)
        {
            VolontaryReasonInBUS ccentar3g = new VolontaryReasonInBUS();
            List<VoluntaryReasonInModel> gmX3g = new List<VoluntaryReasonInModel>();

            gmX3g = ccentar3g.GetAllReasonIn();
            using (var dlgSave3g = new GridLookupFormReasonInOut(reasonInList, gmX3g, "ReasonIn"))
            {
                //Reset panela kada se sacuva pa odcekira
                reasonInList = new List<VoluntaryReasonInModel>();


                if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
                {
                    //Reset panela kada se sacuva pa odcekira
                    panelReasonIn.Controls.Clear();

                    reasonInList = new List<VoluntaryReasonInModel>();
                    reasonInList = dlgSave3g.volList;

                    RadLabel rl = new RadLabel();
                    int Y = 5;
                    for (int i = 0; i < reasonInList.Count; i++)
                    {
                        rl = new RadLabel();
                        rl.Font = new Font("Verdana", 9);
                        rl.Text = reasonInList[i].nameReasonIn;
                        rl.Location = new Point(0, Y);
                        rl.AutoSize = true;
                        Y = Y + 3 + rl.Height;

                        panelReasonIn.Controls.Add(rl);

                    }
                }

            }
        }

        private void btnPrintReasonIn_Click(object sender, EventArgs e)
        {
            btnPrintPreviewReasonIn.Visible = false;
            btnExportToExcelReasonIn.Visible = false;

            gridReasonIn.DataSource = null;
            VolAvailabilityPreselectionBUS bus = new VolAvailabilityPreselectionBUS();
            List<VoluntaryContPersReasonInModel> listaVolReasonIn = new List<VoluntaryContPersReasonInModel>();

            if (!SaveAndValidateRo(reasonInDtFrom.Value, dtToReasonIn.Value))
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("You need to enter dates!") != null)
                        RadMessageBox.Show(resxSet.GetString("You need to enter dates!"));
                    else
                        RadMessageBox.Show("You need to enter dates!");
                }
                return;
            }
            else
            {
                listaVolReasonIn = bus.GetContactPersonReasionIn(reasonInDtFrom.Value, dtToReasonIn.Value, volFunctionListRI, label, reasonInList);

                if (listaVolReasonIn != null)
                {
                    #region tel
                    PersonTelBUS ptb = new PersonTelBUS();
                    List<PersonTelModel> telAll = new List<PersonTelModel>();
                    telAll = ptb.GetAllPersonTels();

                    if (telAll != null)
                    {
                        if (listaVolReasonIn != null)
                        {
                            for (int i = 0; i < listaVolReasonIn.Count; i++)
                            {
                                List<PersonTelModel> telForPerson = new List<PersonTelModel>();
                                telForPerson = telAll.FindAll(s => s.idContPers == listaVolReasonIn[i].idContPers);

                                if (telForPerson != null)
                                {
                                    string tel = "";
                                    for (int j = 0; j < telForPerson.Count; j++)
                                    {
                                        if (telForPerson[j].descriptionTel != "")
                                            tel = tel + telForPerson[j].descriptionTel + ": ";

                                        if (telForPerson[j].numberTel != "")
                                        {
                                            if (j == telForPerson.Count - 1)
                                                tel = tel + telForPerson[j].numberTel;
                                            else
                                                tel = tel + telForPerson[j].numberTel + ", ";
                                        }
                                    }
                                    if (tel != "")
                                        listaVolReasonIn[i].numberTel = tel;

                                }


                            }
                        }
                    }
                    #endregion

                    #region email

                    PersonEmailBUS peb = new PersonEmailBUS();
                    List<PersonEmailModel> emailAll = new List<PersonEmailModel>();
                    emailAll = peb.GetAllPersonEmails();

                    if (emailAll != null)
                    {
                        if (listaVolReasonIn != null)
                        {
                            for (int i = 0; i < listaVolReasonIn.Count; i++)
                            {
                                List<PersonEmailModel> emailForPerson = new List<PersonEmailModel>();
                                emailForPerson = emailAll.FindAll(s => s.idContPers == listaVolReasonIn[i].idContPers);

                                if (emailForPerson != null)
                                {
                                    string emails = "";
                                    for (int j = 0; j < emailForPerson.Count; j++)
                                    {

                                        if (emailForPerson[j].email != "")
                                        {
                                            if (j == emailForPerson.Count - 1)
                                                emails = emails + emailForPerson[j].email;
                                            else
                                                emails = emails + emailForPerson[j].email + ", ";
                                        }
                                    }
                                    if (emails != "")
                                        listaVolReasonIn[i].email = emails;

                                }


                            }
                        }
                    }
                    #endregion

                    #region function
                    VolontaryFunctionModel vfm = new VolontaryFunctionModel();
                    VolontaryFunctionBUS vfBus = new VolontaryFunctionBUS();
                    List<VolontaryFunctionModel> functionAll = new List<VolontaryFunctionModel>();
                    functionAll = vfBus.GetAllFromVolFunction();

                    if (functionAll != null)
                    {
                        if (listaVolReasonIn != null)
                        {
                            for (int i = 0; i < listaVolReasonIn.Count; i++)
                            {
                                List<VolontaryFunctionModel> functionForPerson = new List<VolontaryFunctionModel>();
                                functionForPerson = functionAll.FindAll(s => s.idcpr == listaVolReasonIn[i].idContPers);

                                if (functionForPerson != null)
                                {
                                    string func = "";
                                    for (int j = 0; j < functionForPerson.Count; j++)
                                    {
                                        if (j == functionForPerson.Count - 1)
                                            func = func + functionForPerson[j].txtQuest;
                                        else
                                            func = func + functionForPerson[j].txtQuest + ", ";
                                    }
                                    if (func != "")
                                        listaVolReasonIn[i].function = func;

                                }


                            }
                        }
                    }
                    #endregion

                    gridReasonIn.DataSource = listaVolReasonIn;

                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("No data for curent conditions!") != null)
                            RadMessageBox.Show(resxSet.GetString("No data for curent conditions!"));
                        else
                            RadMessageBox.Show("No data for curent conditions!");
                    }
                }
            }
        }

        private void leyoutReasonIn_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutReasonIn))
            {
                File.Delete(layoutReasonIn);
            }
            gridReasonIn.SaveLayout(layoutReasonIn);

            RadMessageBox.Show("Layout Saved");
        }

        private void gridReasonIn_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if (gridReasonIn != null)
            {
                if (gridReasonIn.Columns.Count > 0)
                {
                    foreach (var column in gridReasonIn.Columns)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString(column.HeaderText) != null)
                                column.HeaderText = resxSet.GetString(column.HeaderText);
                        }

                        column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 101);

                    }
                }

                btnPrintPreviewReasonIn.Visible = true;
                btnExportToExcelReasonIn.Visible = true;
            }
            else
            {
                btnPrintPreviewReasonIn.Visible = false;
                btnExportToExcelReasonIn.Visible = false;
            }

            if (File.Exists(layoutReasonIn))
            {
                gridReasonIn.LoadLayout(layoutReasonIn);
            }
        }

        private void gridReasonIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.gridReasonIn.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    GridViewRowInfo info = this.gridReasonIn.CurrentRow;
                    VoluntaryContPersReasonInModel vam = new VoluntaryContPersReasonInModel();
                    vam = (VoluntaryContPersReasonInModel)info.DataBoundItem;

                    VoluntaryContPersReasonInModel volaAvaModel = (VoluntaryContPersReasonInModel)info.DataBoundItem;

                    PersonModel amodel = new PersonModel();
                    amodel = new PersonBUS().GetPerson(vam.idContPers);


                    frmPerson frm = new frmPerson(amodel);

                    frm.ShowDialog();

                    return;

                }

            }
        }

        private void gridReasonIn_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.gridReasonIn.CurrentRow;
            VoluntaryContPersReasonInModel vam = new VoluntaryContPersReasonInModel();
            vam = (VoluntaryContPersReasonInModel)info.DataBoundItem;

            VoluntaryContPersReasonInModel volaAvaModel = (VoluntaryContPersReasonInModel)info.DataBoundItem;

            PersonModel amodel = new PersonModel();
            amodel = new PersonBUS().GetPerson(vam.idContPers);

            if (info != null && e.RowIndex >= 0)
                if (volaAvaModel != null)
                {
                    frmPerson frm = new frmPerson(amodel);

                    frm.ShowDialog();

                }
        }

        private void btnFunctionReasonIn_Click(object sender, EventArgs e)
        {
            VolAvailabilityPreselectionBUS ccentar3g = new VolAvailabilityPreselectionBUS();
            List<VolAvailabilityPreselectionModel> gmX3g = new List<VolAvailabilityPreselectionModel>();

            gmX3g = ccentar3g.GetAllFunction(labelFunctionReasonIn);
            using (var dlgSave3g = new GridLookupFormPreselection(volFunctionListRI, gmX3g, "Function"))
            {
                //Reset panela kada se sacuva pa odcekira
                volFunctionListRI = new List<VolAvailabilityPreselectionModel>();


                if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
                {
                    //Reset panela kada se sacuva pa odcekira
                    panelFunctionReasonIn.Controls.Clear();

                    volFunctionListRI = new List<VolAvailabilityPreselectionModel>();
                    volFunctionListRI = dlgSave3g.volList;

                    RadLabel rl = new RadLabel();
                    int Y = 5;
                    for (int i = 0; i < volFunctionListRI.Count; i++)
                    {
                        rl = new RadLabel();
                        rl.Font = new Font("Verdana", 9);
                        rl.Text = volFunctionListRI[i].txtQuest;
                        rl.Location = new Point(0, Y);
                        rl.AutoSize = true;
                        Y = Y + 3 + rl.Height;

                        panelFunctionReasonIn.Controls.Add(rl);

                    }
                }

            }
        }

        private void btnPrintPreviewReasonIn_Click(object sender, System.EventArgs e)
        {
            if (gridReasonIn != null)
                if (gridReasonIn.Columns.Count > 0)
                {

                    this.gridReasonIn.PrintPreview();
                }
                else
                {
                    RadMessageBox.Show("Nothing for print preview!");
                }
        }

        private void btnExportToExcelReasonIn_Click(object sender, System.EventArgs e)
        {
            if (gridReasonIn.DataSource != null)
                if (gridReasonIn.Columns.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.InitialDirectory = @"C:\";
                    sfd.RestoreDirectory = true;
                    sfd.FileName = radPageVolPreselection.SelectedPage.Text;
                    sfd.DefaultExt = "xls";
                    sfd.Filter = "Excel file (*.xls) | *.xls";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string path = sfd.FileName;
                        ExportToExcelML exporter = new ExportToExcelML(this.gridReasonIn);
                        exporter.SheetMaxRows = ExcelMaxRows._1048576;

                        exporter.RunExport(path);
                        RadMessageBox.Show("Export to excel!");


                    }
                }
        }


        #endregion

        #region ReasonOut

        private void btnFunctionsRo_Click(object sender, EventArgs e)
        {
            VolAvailabilityPreselectionBUS ccentar3g = new VolAvailabilityPreselectionBUS();
            List<VolAvailabilityPreselectionModel> gmX3g = new List<VolAvailabilityPreselectionModel>();

            gmX3g = ccentar3g.GetAllFunction(labelReasonOut);
            using (var dlgSave3g = new GridLookupFormPreselection(volFunctionListRO, gmX3g, "Function"))
            {
                //Reset panela kada se sacuva pa odcekira
                volFunctionListRO = new List<VolAvailabilityPreselectionModel>();


                if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
                {
                    //Reset panela kada se sacuva pa odcekira
                    panelFunctionsRo.Controls.Clear();

                    volFunctionListRO = new List<VolAvailabilityPreselectionModel>();
                    volFunctionListRO = dlgSave3g.volList;

                    RadLabel rl = new RadLabel();
                    int Y = 5;
                    for (int i = 0; i < volFunctionListRO.Count; i++)
                    {
                        rl = new RadLabel();
                        rl.Font = new Font("Verdana", 9);
                        rl.Text = volFunctionListRO[i].txtQuest;
                        rl.Location = new Point(0, Y);
                        rl.AutoSize = true;
                        Y = Y + 3 + rl.Height;

                        panelFunctionsRo.Controls.Add(rl);

                    }
                }

            }
        }

        private void rck_CheckStateChangedRo(object sender, EventArgs e)
        {

            RadRadioButton rb = (RadRadioButton)sender;
            labelReasonOut = new List<int>();
            if (rb.Name.Replace("chkLabel", "") != "")
                labelReasonOut.Add(Convert.ToInt32(rb.Name.Replace("chkLabel", "")));
            else
            {
                //Za odredjene labele uzima ID i rpokazuje u lookup-u
                for (int i = 0; i < Login._arrLabels.Count; i++)
                {

                    labelReasonOut.Add(Login._arrLabels[i].idLabel);
                }
            }

            //Reset 

            volFunctionListRO = new List<VolAvailabilityPreselectionModel>();
            panelFunctionsRo.Controls.Clear();


        }

        private void loadLabelReasonOut()
        {

            int Y = 0;
            int X = 0;
            RadRadioButton rckRO = new RadRadioButton();
            rckRO.Font = new Font("Verdana", 9);
            rckRO.Name = "chkLabel";
            rckRO.Text = "None";
            rckRO.Location = new Point(0, Y);
            rckRO.CheckStateChanged += rck_CheckStateChangedRo;
            rckRO.AutoSize = true;
            rckRO.IsChecked = true;

            Y = Y + 3 + rckRO.Height;


            panelLabelsRo.Controls.Add(rckRO);



            Y = 21;

            for (int i = 0; i < Login._arrLabels.Count; i++)
            {
                rckRO = new RadRadioButton();
                rckRO.Font = new Font("Verdana", 9);
                rckRO.Name = "chkLabel" + Login._arrLabels[i].idLabel.ToString();
                rckRO.Text = Login._arrLabels[i].nameLabel;
                rckRO.Location = new Point(0, Y);
                rckRO.CheckStateChanged += rck_CheckStateChangedRo;
                rckRO.AutoSize = true;
                Y = Y + 3 + rckRO.Height;

                panelLabelsRo.Controls.Add(rckRO);

            }
        }

        private bool SaveAndValidateRo(DateTime dtFromValue, DateTime dtToValue)
        {
            bool value = true;
            if (dtFromValue != null)
            {
                if (dtFromValue.Year < 1900)
                {
                    value = false;
                }
            }
            else
            {
                value = false;
            }
            if (dtToValue != null)
            {
                if (dtToValue.Year > 9999)
                {
                    value = false;
                }
            }
            else
            {
                value = false;
            }
            return value;
        }

        private void btnPrintRo_Click(object sender, EventArgs e)
        {
            btnPrintPreviewRo.Visible = false;
            btnExportToExcelRo.Visible = false;

            rgvReasonOut.DataSource = null;
            VolAvailabilityPreselectionBUS vapb = new VolAvailabilityPreselectionBUS();
            List<VolAvailabilityReasonOutPreselectionModel> list = new List<VolAvailabilityReasonOutPreselectionModel>();
            if (!SaveAndValidateRo(dtFromRo.Value, dtToRo.Value))
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("You need to enter dates!") != null)
                        RadMessageBox.Show(resxSet.GetString("You need to enter dates!"));
                    else
                        RadMessageBox.Show("You need to enter dates!");
                }
                return;
            }
            else
            {
                list = vapb.GetAllForReasonOut(dtFromRo.Value, dtToRo.Value, volFunctionListRO, volReasonOutRO, labelReasonOut);
                if (list != null)
                {
                    #region tel
                    PersonTelBUS ptb = new PersonTelBUS();
                    List<PersonTelModel> telAll = new List<PersonTelModel>();
                    telAll = ptb.GetAllPersonTels();

                    if (telAll != null)
                    {
                        if (list != null)
                        {
                            for (int i = 0; i < list.Count; i++)
                            {
                                List<PersonTelModel> telForPerson = new List<PersonTelModel>();
                                telForPerson = telAll.FindAll(s => s.idContPers == list[i].idContPers);

                                if (telForPerson != null)
                                {
                                    string tel = "";
                                    for (int j = 0; j < telForPerson.Count; j++)
                                    {
                                        if (telForPerson[j].descriptionTel != "")
                                            tel = tel + telForPerson[j].descriptionTel + ": ";

                                        if (telForPerson[j].numberTel != "")
                                        {
                                            if (j == telForPerson.Count - 1)
                                                tel = tel + telForPerson[j].numberTel;
                                            else
                                                tel = tel + telForPerson[j].numberTel + ", ";
                                        }
                                    }
                                    if (tel != "")
                                        list[i].numberTel = tel;

                                }


                            }
                        }
                    }
                    #endregion

                    #region email
                    //Odavde

                    PersonEmailBUS peb = new PersonEmailBUS();
                    List<PersonEmailModel> emailAll = new List<PersonEmailModel>();
                    emailAll = peb.GetAllPersonEmails();

                    if (emailAll != null)
                    {
                        if (list != null)
                        {
                            for (int i = 0; i < list.Count; i++)
                            {
                                List<PersonEmailModel> emailForPerson = new List<PersonEmailModel>();
                                emailForPerson = emailAll.FindAll(s => s.idContPers == list[i].idContPers);

                                if (emailForPerson != null)
                                {
                                    string emails = "";
                                    for (int j = 0; j < emailForPerson.Count; j++)
                                    {

                                        if (emailForPerson[j].email != "")
                                        {
                                            if (j == emailForPerson.Count - 1)
                                                emails = emails + emailForPerson[j].email;
                                            else
                                                emails = emails + emailForPerson[j].email + ", ";
                                        }
                                    }
                                    if (emails != "")
                                        list[i].email = emails;

                                }


                            }
                        }
                    }
                    #endregion

                    #region function
                    VolontaryFunctionModel vfm = new VolontaryFunctionModel();
                    VolontaryFunctionBUS vfBus = new VolontaryFunctionBUS();
                    List<VolontaryFunctionModel> functionAll = new List<VolontaryFunctionModel>();
                    functionAll = vfBus.GetAllFromVolFunction();

                    if (functionAll != null)
                    {
                        if (list != null)
                        {
                            for (int i = 0; i < list.Count; i++)
                            {
                                List<VolontaryFunctionModel> functionForPerson = new List<VolontaryFunctionModel>();
                                functionForPerson = functionAll.FindAll(s => s.idcpr == list[i].idContPers);

                                if (functionForPerson != null)
                                {
                                    string func = "";
                                    for (int j = 0; j < functionForPerson.Count; j++)
                                    {
                                        if (j == functionForPerson.Count - 1)
                                            func = func + functionForPerson[j].txtQuest;
                                        else
                                            func = func + functionForPerson[j].txtQuest + ", ";
                                    }
                                    if (func != "")
                                        list[i].function = func;

                                }


                            }
                        }
                    }
                    #endregion


                    rgvReasonOut.DataSource = list;
                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("No data for curent conditions!") != null)
                            RadMessageBox.Show(resxSet.GetString("No data for curent conditions!"));
                        else
                            RadMessageBox.Show("No data for curent conditions!");
                    }
                }
            }

        }

        private void btnReasonOutRo_Click(object sender, EventArgs e)
        {
            VolAvailabilityPreselectionBUS ccentar3g = new VolAvailabilityPreselectionBUS();
            List<VoluntaryReasonOutModel> gmX3g = new List<VoluntaryReasonOutModel>();

            gmX3g = ccentar3g.GetReasonOut();
            using (var dlgSave3g = new GridLookupFormReasonOut(volReasonOutRO, gmX3g, "ReasonOut"))
            {
                //Reset panela kada se sacuva pa odcekira
                volReasonOutRO = new List<VoluntaryReasonOutModel>();


                if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
                {
                    //Reset panela kada se sacuva pa odcekira
                    panelReasonOutRo.Controls.Clear();

                    volReasonOutRO = new List<VoluntaryReasonOutModel>();
                    volReasonOutRO = dlgSave3g.volList;

                    RadLabel rl = new RadLabel();
                    int Y = 5;
                    for (int i = 0; i < volReasonOutRO.Count; i++)
                    {
                        rl = new RadLabel();
                        rl.Font = new Font("Verdana", 9);
                        rl.Text = volReasonOutRO[i].nameReasonOut;
                        rl.Location = new Point(0, Y);
                        rl.AutoSize = true;
                        Y = Y + 3 + rl.Height;

                        panelReasonOutRo.Controls.Add(rl);

                    }
                }

            }
        }

        private void rgvReasonOut_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if (rgvReasonOut != null)
            {
                if (rgvReasonOut.Columns.Count > 0)
                {
                    foreach (var column in rgvReasonOut.Columns)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString(column.HeaderText) != null)
                                column.HeaderText = resxSet.GetString(column.HeaderText);
                        }

                        column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 101);


                    }
                }

                btnPrintPreviewRo.Visible = true;
                btnExportToExcelRo.Visible = true;
            }
            else
            {
                btnPrintPreviewRo.Visible = false;
                btnExportToExcelRo.Visible = false;
            }

            if (File.Exists(layoutRO))
            {
                rgvReasonOut.LoadLayout(layoutRO);
            }
        }

        private void rgvReasonOut_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.rgvReasonOut.CurrentRow;
            VolAvailabilityReasonOutPreselectionModel vam = new VolAvailabilityReasonOutPreselectionModel();
            vam = (VolAvailabilityReasonOutPreselectionModel)info.DataBoundItem;

            VolAvailabilityReasonOutPreselectionModel volaAvaModel = (VolAvailabilityReasonOutPreselectionModel)info.DataBoundItem;

            PersonModel amodel = new PersonModel();
            amodel = new PersonBUS().GetPerson(vam.idContPers);

            if (info != null && e.RowIndex >= 0)
                if (volaAvaModel != null)
                {
                    frmPerson frm = new frmPerson(amodel);

                    frm.ShowDialog();

                }
        }

        private void rgvReasonOut_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.rgvReasonOut.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    GridViewRowInfo info = this.rgvReasonOut.CurrentRow;
                    VolAvailabilityReasonOutPreselectionModel vam = new VolAvailabilityReasonOutPreselectionModel();
                    vam = (VolAvailabilityReasonOutPreselectionModel)info.DataBoundItem;

                    VolAvailabilityReasonOutPreselectionModel volaAvaModel = (VolAvailabilityReasonOutPreselectionModel)info.DataBoundItem;

                    PersonModel amodel = new PersonModel();
                    amodel = new PersonBUS().GetPerson(vam.idContPers);


                    frmPerson frm = new frmPerson(amodel);

                    frm.ShowDialog();

                    return;

                }

            }
        }

        private void radMenuItemSaveLayoutRO_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutRO))
            {
                File.Delete(layoutRO);
            }
            rgvReasonOut.SaveLayout(layoutRO);

            RadMessageBox.Show("Layout Saved");
        }

        private void btnPrintPreviewRo_Click(object sender, System.EventArgs e)
        {
            if (rgvReasonOut != null)
                if (rgvReasonOut.Columns.Count > 0)
                {

                    this.rgvReasonOut.PrintPreview();
                }
                else
                {
                    RadMessageBox.Show("Nothing for print preview!");
                }
        }

        private void btnExportToExcelRo_Click(object sender, System.EventArgs e)
        {
            if (rgvReasonOut.DataSource != null)
                if (rgvReasonOut.Columns.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.InitialDirectory = @"C:\";
                    sfd.RestoreDirectory = true;
                    sfd.FileName = radPageVolPreselection.SelectedPage.Text;
                    sfd.DefaultExt = "xls";
                    sfd.Filter = "Excel file (*.xls) | *.xls";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string path = sfd.FileName;
                        ExportToExcelML exporter = new ExportToExcelML(this.rgvReasonOut);
                        exporter.SheetMaxRows = ExcelMaxRows._1048576;

                        exporter.RunExport(path);
                        RadMessageBox.Show("Export to excel!");


                    }
                }
        }

        #endregion

        #region Age list

        private void btnFunctionsAl_Click(object sender, EventArgs e)
        {
            VolAvailabilityPreselectionBUS ccentar3g = new VolAvailabilityPreselectionBUS();
            List<VolAvailabilityPreselectionModel> gmX3g = new List<VolAvailabilityPreselectionModel>();

            gmX3g = ccentar3g.GetAllFunction(labelAgeList);
            using (var dlgSave3g = new GridLookupFormPreselection(volFunctionListAl, gmX3g, "Function"))
            {
                //Reset panela kada se sacuva pa odcekira
                volFunctionListAl = new List<VolAvailabilityPreselectionModel>();


                if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
                {
                    //Reset panela kada se sacuva pa odcekira
                    panelFunctionsAl.Controls.Clear();

                    volFunctionListAl = new List<VolAvailabilityPreselectionModel>();
                    volFunctionListAl = dlgSave3g.volList;

                    RadLabel rl = new RadLabel();
                    int Y = 5;
                    for (int i = 0; i < volFunctionListAl.Count; i++)
                    {
                        rl = new RadLabel();
                        rl.Font = new Font("Verdana", 9);
                        rl.Text = volFunctionListAl[i].txtQuest;
                        rl.Location = new Point(0, Y);
                        rl.AutoSize = true;
                        Y = Y + 3 + rl.Height;

                        panelFunctionsAl.Controls.Add(rl);

                    }
                }

            }
        }

        private void rck_CheckStateChangedAl(object sender, EventArgs e)
        {

            RadRadioButton rb = (RadRadioButton)sender;
            labelAgeList = new List<int>();
            if (rb.Name.Replace("chkLabel", "") != "")
                labelAgeList.Add(Convert.ToInt32(rb.Name.Replace("chkLabel", "")));
            else
            {
                //Za odredjene labele uzima ID i rpokazuje u lookup-u
                for (int i = 0; i < Login._arrLabels.Count; i++)
                {

                    labelAgeList.Add(Login._arrLabels[i].idLabel);
                }
            }

            //Reset 

            volFunctionListAl = new List<VolAvailabilityPreselectionModel>();
            panelFunctionsAl.Controls.Clear();

            //Za reset autoScroll-a da se ne pojavi kada je panel prazan prilikom promene labele          
            panelFunctionsAl.AutoScroll = true;
            panelFunctionsAl.VerticalScroll.Enabled = false;


        }

        private void loadLabelAgeList()
        {

            int Y = 0;
            int X = 0;
            RadRadioButton rckAl = new RadRadioButton();
            rckAl.Font = new Font("Verdana", 9);
            rckAl.Name = "chkLabel";
            rckAl.Text = "None";
            rckAl.Location = new Point(0, Y);
            rckAl.CheckStateChanged += rck_CheckStateChangedAl;
            rckAl.AutoSize = true;
            rckAl.IsChecked = true;

            Y = Y + 3 + rckAl.Height;


            panelLabelsAl.Controls.Add(rckAl);



            Y = 21;

            for (int i = 0; i < Login._arrLabels.Count; i++)
            {
                rckAl = new RadRadioButton();
                rckAl.Font = new Font("Verdana", 9);
                rckAl.Name = "chkLabel" + Login._arrLabels[i].idLabel.ToString();
                rckAl.Text = Login._arrLabels[i].nameLabel;
                rckAl.Location = new Point(0, Y);
                rckAl.CheckStateChanged += rck_CheckStateChangedAl;
                rckAl.AutoSize = true;
                Y = Y + 3 + rckAl.Height;

                panelLabelsAl.Controls.Add(rckAl);

            }
        }

        private bool validateFromTo()
        {
            bool validate = true;
            if (txtAgeFrom.Text != null)
                if (txtAgeFrom.Text != "")
                {
                    try
                    {
                        ageFromAl = Int32.Parse(txtAgeFrom.Text);
                        if (ageFromAl > 100 || ageFromAl < 0)
                        {
                            ageFromAl = 0;
                            throw new Exception();
                        }
                    }
                    catch (Exception Ex)
                    {
                        validate = false;
                    }
                }
            if (txtAgeToAl.Text != null)
                if (txtAgeToAl.Text != "")
                {
                    try
                    {
                        ageToAl = Int32.Parse(txtAgeToAl.Text);
                        if (ageToAl > 100 || ageToAl < 0)
                        {
                            ageToAl = 200;
                            throw new Exception();
                        }
                    }
                    catch (Exception Ex)
                    {
                        validate = false;
                    }
                }
            return validate;
        }

        private void btnPrintAl_Click(object sender, EventArgs e)
        {
            btnPrintPreviewAl.Visible = false;
            btnExportToExcelAl.Visible = false;

            rgvAgeList.DataSource = null;

            VolAvailabilityPreselectionBUS vapb = new VolAvailabilityPreselectionBUS();
            List<VolAvailabilityAgeListPreselectionModel> list = new List<VolAvailabilityAgeListPreselectionModel>();
            if (!validateFromTo())
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Not a valid number for age limits!");
                return;
            }
            if (dtReferenceAl != null)
                if (dtReferenceAl.Value.Year > 1800)
                {
                    list = vapb.GetAllForAgeList(dtReferenceAl.Value, ageFromAl, ageToAl, volFunctionListAl, labelAgeList);
                    if (list != null)
                    {
                        #region tel
                        PersonTelBUS ptb = new PersonTelBUS();
                        List<PersonTelModel> telAll = new List<PersonTelModel>();
                        telAll = ptb.GetAllPersonTels();

                        if (telAll != null)
                        {
                            if (list != null)
                            {
                                for (int i = 0; i < list.Count; i++)
                                {
                                    List<PersonTelModel> telForPerson = new List<PersonTelModel>();
                                    telForPerson = telAll.FindAll(s => s.idContPers == list[i].idContPers);

                                    if (telForPerson != null)
                                    {
                                        string tel = "";
                                        for (int j = 0; j < telForPerson.Count; j++)
                                        {
                                            if (telForPerson[j].descriptionTel != "")
                                                tel = tel + telForPerson[j].descriptionTel + ": ";

                                            if (telForPerson[j].numberTel != "")
                                            {
                                                if (j == telForPerson.Count - 1)
                                                    tel = tel + telForPerson[j].numberTel;
                                                else
                                                    tel = tel + telForPerson[j].numberTel + ", ";
                                            }
                                        }
                                        if (tel != "")
                                            list[i].numberTel = tel;

                                    }


                                }
                            }
                        }
                        #endregion

                        #region email
                        //Odavde

                        PersonEmailBUS peb = new PersonEmailBUS();
                        List<PersonEmailModel> emailAll = new List<PersonEmailModel>();
                        emailAll = peb.GetAllPersonEmails();

                        if (emailAll != null)
                        {
                            if (list != null)
                            {
                                for (int i = 0; i < list.Count; i++)
                                {
                                    List<PersonEmailModel> emailForPerson = new List<PersonEmailModel>();
                                    emailForPerson = emailAll.FindAll(s => s.idContPers == list[i].idContPers);

                                    if (emailForPerson != null)
                                    {
                                        string emails = "";
                                        for (int j = 0; j < emailForPerson.Count; j++)
                                        {

                                            if (emailForPerson[j].email != "")
                                            {
                                                if (j == emailForPerson.Count - 1)
                                                    emails = emails + emailForPerson[j].email;
                                                else
                                                    emails = emails + emailForPerson[j].email + ", ";
                                            }
                                        }
                                        if (emails != "")
                                            list[i].email = emails;

                                    }


                                }
                            }
                        }
                        #endregion

                        #region function
                        VolontaryFunctionModel vfm = new VolontaryFunctionModel();
                        VolontaryFunctionBUS vfBus = new VolontaryFunctionBUS();
                        List<VolontaryFunctionModel> functionAll = new List<VolontaryFunctionModel>();
                        functionAll = vfBus.GetAllFromVolFunction();

                        if (functionAll != null)
                        {
                            if (list != null)
                            {
                                for (int i = 0; i < list.Count; i++)
                                {
                                    List<VolontaryFunctionModel> functionForPerson = new List<VolontaryFunctionModel>();
                                    functionForPerson = functionAll.FindAll(s => s.idcpr == list[i].idContPers);

                                    if (functionForPerson != null)
                                    {
                                        string func = "";
                                        for (int j = 0; j < functionForPerson.Count; j++)
                                        {
                                            if (j == functionForPerson.Count - 1)
                                                func = func + functionForPerson[j].txtQuest;
                                            else
                                                func = func + functionForPerson[j].txtQuest + ", ";
                                        }
                                        if (func != "")
                                            list[i].function = func;

                                    }


                                }
                            }
                        }
                        #endregion


                        rgvAgeList.DataSource = list;
                    }
                    else
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString("No data for curent conditions!") != null)
                                RadMessageBox.Show(resxSet.GetString("No data for curent conditions!"));
                            else
                                RadMessageBox.Show("No data for curent conditions!");
                        }
                    }
                }

        }

        private void rgvAgeList_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if (rgvAgeList != null)
            {
                if (rgvAgeList.Columns.Count > 0)
                {
                    foreach (var column in rgvAgeList.Columns)
                    {

                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString(column.HeaderText) != null)
                                column.HeaderText = resxSet.GetString(column.HeaderText);
                        }

                        column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 101);
                    }
                }

                btnPrintPreviewAl.Visible = true;
                btnExportToExcelAl.Visible = true;
            }

            else
            {
                btnPrintPreviewAl.Visible = false;
                btnExportToExcelAl.Visible = false;
            }

            if (File.Exists(layoutAgeList))
            {
                rgvAgeList.LoadLayout(layoutAgeList);
            }
        }

        private void rgvAgeList_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.rgvAgeList.CurrentRow;
            VolAvailabilityAgeListPreselectionModel vam = new VolAvailabilityAgeListPreselectionModel();
            vam = (VolAvailabilityAgeListPreselectionModel)info.DataBoundItem;

            VolAvailabilityAgeListPreselectionModel volaAvaModel = (VolAvailabilityAgeListPreselectionModel)info.DataBoundItem;

            PersonModel amodel = new PersonModel();
            amodel = new PersonBUS().GetPerson(vam.idContPers);

            if (info != null && e.RowIndex >= 0)
                if (volaAvaModel != null)
                {
                    frmPerson frm = new frmPerson(amodel);

                    frm.ShowDialog();
                }
        }

        private void rgvAgeList_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.rgvAgeList.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    GridViewRowInfo info = this.rgvAgeList.CurrentRow;
                    VolAvailabilityAgeListPreselectionModel vam = new VolAvailabilityAgeListPreselectionModel();
                    vam = (VolAvailabilityAgeListPreselectionModel)info.DataBoundItem;

                    VolAvailabilityAgeListPreselectionModel volaAvaModel = (VolAvailabilityAgeListPreselectionModel)info.DataBoundItem;
                    PersonModel amodel = new PersonModel();
                    amodel = new PersonBUS().GetPerson(vam.idContPers);


                    frmPerson frm = new frmPerson(amodel);

                    frm.ShowDialog();

                    return;

                }

            }
        }

        private void radMenuItemAgeList_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutAgeList))
            {
                File.Delete(layoutAgeList);
            }
            rgvAgeList.SaveLayout(layoutAgeList);

            RadMessageBox.Show("Layout Saved");
        }

        private void btnExportToExcelAl_Click(object sender, System.EventArgs e)
        {

            if (rgvAgeList.DataSource != null)
                if (rgvAgeList.Columns.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.InitialDirectory = @"C:\";
                    sfd.RestoreDirectory = true;
                    sfd.FileName = radPageVolPreselection.SelectedPage.Text;
                    sfd.DefaultExt = "xls";
                    sfd.Filter = "Excel file (*.xls) | *.xls";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string path = sfd.FileName;
                        ExportToExcelML exporter = new ExportToExcelML(this.rgvAgeList);
                        exporter.SheetMaxRows = ExcelMaxRows._1048576;

                        exporter.RunExport(path);
                        RadMessageBox.Show("Export to excel!");


                    }
                }
        }

        private void btnPrintPreviewAl_Click(object sender, System.EventArgs e)
        {
            if (rgvAgeList != null)
                if (rgvAgeList.Columns.Count > 0)
                {

                    this.rgvAgeList.PrintPreview();
                }
                else
                {
                    RadMessageBox.Show("Nothing for print preview!");
                }
        }


        #endregion


        #region Unique volunteers

        private void LoadUniqueVolunteers()
        {

            int Y = 0;
            int X = 0;
            RadRadioButton rckUv = new RadRadioButton();
            rckUv.Font = new Font("Verdana", 9);
            rckUv.Name = "chkLabel";
            rckUv.Text = "None";
            rckUv.Location = new Point(0, Y);
            rckUv.CheckStateChanged += rckUv_CheckStateChangedUnique;
            rckUv.AutoSize = true;
            rckUv.IsChecked = true;

            Y = Y + 3 + rckUv.Height;


            panelUnique.Controls.Add(rckUv);



            Y = 21;

            for (int i = 0; i < Login._arrLabels.Count; i++)
            {
                rckUv = new RadRadioButton();
                rckUv.Font = new Font("Verdana", 9);
                rckUv.Name = "chkLabel" + Login._arrLabels[i].idLabel.ToString();
                rckUv.Text = Login._arrLabels[i].nameLabel;
                rckUv.Location = new Point(0, Y);
                rckUv.CheckStateChanged += rckUv_CheckStateChangedUnique;
                rckUv.AutoSize = true;
                Y = Y + 3 + rckUv.Height;

                panelUnique.Controls.Add(rckUv);

            }

            //Za ukidanje scroll-a nakon promene labele
            panelFunctionUnique.AutoScroll = true;
            panelFunctionUnique.VerticalScroll.Enabled = false;

        }

        private void rckUv_CheckStateChangedUnique(object sender, EventArgs e)
        {
            RadRadioButton rbu = (RadRadioButton)sender;
            labelUniqueVolunteers = new List<int>();
            if (rbu.Name.Replace("chkLabel", "") != "")
                labelUniqueVolunteers.Add(Convert.ToInt32(rbu.Name.Replace("chkLabel", "")));
            else
            {
                //Za odredjene labele uzima ID i prikazuje u lookup-u
                for (int i = 0; i < Login._arrLabels.Count; i++)
                {

                    labelUniqueVolunteers.Add(Login._arrLabels[i].idLabel);
                }
            }


            volFunctionUniqueList = new List<VolAvailabilityPreselectionModel>();
            panelFunctionUnique.Controls.Clear();

            //Za reset autoScroll-a da se ne pojavi kada je panel prazan prilikom promene labele          
            panelFunctionUnique.AutoScroll = true;
            panelFunctionUnique.VerticalScroll.Enabled = false;


        }

        private void btnUniqueLookup_Click(object sender, EventArgs e)
        {
            VolAvailabilityPreselectionBUS ccentar3g = new VolAvailabilityPreselectionBUS();
            List<VolAvailabilityPreselectionModel> gmX3g = new List<VolAvailabilityPreselectionModel>();

            gmX3g = ccentar3g.GetAllFunction(labelUniqueVolunteers);
            using (var dlgSave3g = new GridLookupFormPreselection(volFunctionUniqueList, gmX3g, "Function"))
            {
                //Reset panela kada se sacuva pa odcekira
                volFunctionUniqueList = new List<VolAvailabilityPreselectionModel>();


                if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
                {
                    //Reset panela kada se sacuva pa odcekira
                    panelFunctionUnique.Controls.Clear();

                    volFunctionUniqueList = new List<VolAvailabilityPreselectionModel>();
                    volFunctionUniqueList = dlgSave3g.volList;

                    RadLabel rl = new RadLabel();
                    int Y = 5;
                    for (int i = 0; i < volFunctionUniqueList.Count; i++)
                    {
                        rl = new RadLabel();
                        rl.Font = new Font("Verdana", 9);
                        rl.Text = volFunctionUniqueList[i].txtQuest;
                        rl.Location = new Point(0, Y);
                        rl.AutoSize = true;
                        Y = Y + 3 + rl.Height;

                        panelFunctionUnique.Controls.Add(rl);

                    }
                }

            }
        }

        private void btnPrintUnique_Click(object sender, EventArgs e)
        {
            btnPrintPreviewUnique.Visible = false;
            btnExportToExcelUnique.Visible = false;
            rgvUnique.DataSource = null;

            VolAvailabilityPreselectionBUS bus = new VolAvailabilityPreselectionBUS();
            List<VoluntaryContPersReasonInModel> listaUniqueVolunteers = new List<VoluntaryContPersReasonInModel>();

            if (!SaveAndValidateRo(pickerDtFormUnique.Value, pickerDtToUnique.Value))
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("You need to enter dates!") != null)
                        RadMessageBox.Show(resxSet.GetString("You need to enter dates!"));
                    else
                        RadMessageBox.Show("You need to enter dates!");
                }
                return;
            }
            else
            {
                listaUniqueVolunteers = bus.UniqueVolunteers(pickerDtFormUnique.Value, pickerDtToUnique.Value, volFunctionUniqueList, labelUniqueVolunteers);

                if (listaUniqueVolunteers != null)
                {
                    #region tel
                    PersonTelBUS ptb = new PersonTelBUS();
                    List<PersonTelModel> telAll = new List<PersonTelModel>();
                    telAll = ptb.GetAllPersonTels();

                    if (telAll != null)
                    {
                        if (listaUniqueVolunteers != null)
                        {
                            for (int i = 0; i < listaUniqueVolunteers.Count; i++)
                            {
                                List<PersonTelModel> telForPerson = new List<PersonTelModel>();
                                telForPerson = telAll.FindAll(s => s.idContPers == listaUniqueVolunteers[i].idContPers);

                                if (telForPerson != null)
                                {
                                    string tel = "";
                                    for (int j = 0; j < telForPerson.Count; j++)
                                    {
                                        if (telForPerson[j].descriptionTel != "")
                                            tel = tel + telForPerson[j].descriptionTel + ": ";

                                        if (telForPerson[j].numberTel != "")
                                        {
                                            if (j == telForPerson.Count - 1)
                                                tel = tel + telForPerson[j].numberTel;
                                            else
                                                tel = tel + telForPerson[j].numberTel + ", ";
                                        }
                                    }
                                    if (tel != "")
                                        listaUniqueVolunteers[i].numberTel = tel;

                                }


                            }
                        }
                    }
                    #endregion

                    #region email

                    PersonEmailBUS peb = new PersonEmailBUS();
                    List<PersonEmailModel> emailAll = new List<PersonEmailModel>();
                    emailAll = peb.GetAllPersonEmails();

                    if (emailAll != null)
                    {
                        if (listaUniqueVolunteers != null)
                        {
                            for (int i = 0; i < listaUniqueVolunteers.Count; i++)
                            {
                                List<PersonEmailModel> emailForPerson = new List<PersonEmailModel>();
                                emailForPerson = emailAll.FindAll(s => s.idContPers == listaUniqueVolunteers[i].idContPers);

                                if (emailForPerson != null)
                                {
                                    string emails = "";
                                    for (int j = 0; j < emailForPerson.Count; j++)
                                    {

                                        if (emailForPerson[j].email != "")
                                        {
                                            if (j == emailForPerson.Count - 1)
                                                emails = emails + emailForPerson[j].email;
                                            else
                                                emails = emails + emailForPerson[j].email + ", ";
                                        }
                                    }
                                    if (emails != "")
                                        listaUniqueVolunteers[i].email = emails;

                                }


                            }
                        }
                    }
                    #endregion

                    #region function
                    VolontaryFunctionModel vfm = new VolontaryFunctionModel();
                    VolontaryFunctionBUS vfBus = new VolontaryFunctionBUS();
                    List<VolontaryFunctionModel> functionAll = new List<VolontaryFunctionModel>();
                    functionAll = vfBus.GetAllFromVolFunction();

                    if (functionAll != null)
                    {
                        if (listaUniqueVolunteers != null)
                        {
                            for (int i = 0; i < listaUniqueVolunteers.Count; i++)
                            {
                                List<VolontaryFunctionModel> functionForPerson = new List<VolontaryFunctionModel>();
                                functionForPerson = functionAll.FindAll(s => s.idcpr == listaUniqueVolunteers[i].idContPers);

                                if (functionForPerson != null)
                                {
                                    string func = "";
                                    for (int j = 0; j < functionForPerson.Count; j++)
                                    {
                                        if (j == functionForPerson.Count - 1)
                                            func = func + functionForPerson[j].txtQuest;
                                        else
                                            func = func + functionForPerson[j].txtQuest + ", ";
                                    }
                                    if (func != "")
                                        listaUniqueVolunteers[i].function = func;

                                }


                            }
                        }
                    }
                    #endregion

                    rgvUnique.DataSource = listaUniqueVolunteers;

                }
                else
                {
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("No data for curent conditions!") != null)
                            RadMessageBox.Show(resxSet.GetString("No data for curent conditions!"));
                        else
                            RadMessageBox.Show("No data for curent conditions!");
                    }
                }
            }
        }

        private void rgvUnique_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if (rgvUnique != null)
            {
                if (rgvUnique.Columns.Count > 0)
                {
                    foreach (var column in rgvUnique.Columns)
                    {

                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString(column.HeaderText) != null)
                                column.HeaderText = resxSet.GetString(column.HeaderText);
                        }

                        column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 101);

                    }
                }

                btnPrintPreviewUnique.Visible = true;
                btnExportToExcelUnique.Visible = true;
            }
            else
            {
                btnPrintPreviewUnique.Visible = false;
                btnExportToExcelUnique.Visible = false;
            }

            if (this.rgvUnique.Columns != null)
            {
                if (this.rgvUnique.RowCount > 0)
                {
                    if (this.rgvUnique.Columns["nameReasonIn"] != null)
                        this.rgvUnique.Columns["nameReasonIn"].IsVisible = false;
                }
            }

            if (File.Exists(layoutUniqueVol))
            {
                rgvUnique.LoadLayout(layoutUniqueVol);
            }
        }

        private void rgvUnique_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.rgvUnique.CurrentRow;
            VoluntaryContPersReasonInModel vam = new VoluntaryContPersReasonInModel();
            vam = (VoluntaryContPersReasonInModel)info.DataBoundItem;

            VoluntaryContPersReasonInModel volaAvaModel = (VoluntaryContPersReasonInModel)info.DataBoundItem;

            PersonModel amodel = new PersonModel();
            amodel = new PersonBUS().GetPerson(vam.idContPers);

            if (info != null && e.RowIndex >= 0)
                if (volaAvaModel != null)
                {
                    frmPerson frm = new frmPerson(amodel);

                    frm.ShowDialog();

                }
        }

        private void rgvUnique_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.rgvUnique.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    GridViewRowInfo info = this.rgvUnique.CurrentRow;
                    VoluntaryContPersReasonInModel vam = new VoluntaryContPersReasonInModel();
                    vam = (VoluntaryContPersReasonInModel)info.DataBoundItem;

                    VoluntaryContPersReasonInModel volaAvaModel = (VoluntaryContPersReasonInModel)info.DataBoundItem;

                    PersonModel amodel = new PersonModel();
                    amodel = new PersonBUS().GetPerson(vam.idContPers);


                    frmPerson frm = new frmPerson(amodel);

                    frm.ShowDialog();

                    return;

                }

            }
        }

        private void radMenuItemUniqueSaveLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutUniqueVol))
            {
                File.Delete(layoutUniqueVol);
            }
            rgvUnique.SaveLayout(layoutUniqueVol);

            RadMessageBox.Show("Layout Saved");
        }

        private void btnPrintPreviewUnique_Click(object sender, System.EventArgs e)
        {
            if (rgvUnique != null)
                if (rgvUnique.Columns.Count > 0)
                {

                    this.rgvUnique.PrintPreview();
                }
                else
                {
                    RadMessageBox.Show("Nothing for print preview!");
                }
        }

        private void btnExportToExcelUnique_Click(object sender, System.EventArgs e)
        {
            if (rgvUnique.DataSource != null)
                if (rgvUnique.Columns.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.InitialDirectory = @"C:\";
                    sfd.RestoreDirectory = true;
                    sfd.FileName = radPageVolPreselection.SelectedPage.Text;
                    sfd.DefaultExt = "xls";
                    sfd.Filter = "Excel file (*.xls) | *.xls";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string path = sfd.FileName;
                        ExportToExcelML exporter = new ExportToExcelML(this.rgvUnique);
                        exporter.SheetMaxRows = ExcelMaxRows._1048576;

                        exporter.RunExport(path);
                        RadMessageBox.Show("Export to excel!");


                    }
                }
        }

        #endregion


        # region NotBooked

        private void loadLabelNotBooked()
        {

            int Y = 0;
            int X = 0;
            RadRadioButton rckExp = new RadRadioButton();
            rckExp.Font = new Font("Verdana", 9);
            rckExp.Name = "chkLabel";
            rckExp.Text = "None";
            rckExp.Location = new Point(0, Y);
            rckExp.CheckStateChanged += rck_CheckStateChangedNotBooked;
            rckExp.AutoSize = true;
            rckExp.IsChecked = true;

            Y = Y + 3 + rckExp.Height;


            panelLabelNotBooked.Controls.Add(rckExp);



            Y = 21;

            for (int i = 0; i < Login._arrLabels.Count; i++)
            {
                rckExp = new RadRadioButton();
                rckExp.Font = new Font("Verdana", 9);
                rckExp.Name = "chkLabel" + Login._arrLabels[i].idLabel.ToString();
                rckExp.Text = Login._arrLabels[i].nameLabel;
                rckExp.Location = new Point(0, Y);
                rckExp.CheckStateChanged += rck_CheckStateChangedNotBooked;
                rckExp.AutoSize = true;
                Y = Y + 3 + rckExp.Height;

                panelLabelNotBooked.Controls.Add(rckExp);

            }

        }

        private void rck_CheckStateChangedNotBooked(object sender, EventArgs e)
        {
            RadRadioButton rb = (RadRadioButton)sender;
            labelNotBooked = new List<int>();
            if (rb.Name.Replace("chkLabel", "") != "")
                labelNotBooked.Add(Convert.ToInt32(rb.Name.Replace("chkLabel", "")));
            else
            {
                //Za odredjene labele uzima ID i prikazuje u lookup-u
                for (int i = 0; i < Login._arrLabels.Count; i++)
                {

                    labelNotBooked.Add(Login._arrLabels[i].idLabel);
                }
            }


            volFunctionNotBooked = new List<VolAvailabilityPreselectionModel>();


            panelFunctionNotBooked.Controls.Clear();
            panelFunctionNotBooked.AutoScroll = true;
            panelFunctionNotBooked.VerticalScroll.Enabled = false;
            panelFunctionNotBooked.Controls.Clear();
        }

        private void rgvNotBooked_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
            if (rgvNotBooked.DataSource != null)
            {
                if (rgvNotBooked.Columns.Count > 0)
                {

                    for (int i = 0; i < rgvNotBooked.Columns.Count; i++)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (rgvNotBooked.Columns[i].HeaderText != null && resxSet.GetString(rgvNotBooked.Columns[i].HeaderText) != null)
                                rgvNotBooked.Columns[i].HeaderText = resxSet.GetString(rgvNotBooked.Columns[i].HeaderText);
                        }
                        rgvNotBooked.Columns[i].Width = (int)(this.CreateGraphics().MeasureString(rgvNotBooked.Columns[i].HeaderText, this.Font).Width + 101);

                    }
                }

                if (this.rgvNotBooked.Columns != null)
                {
                    if (this.rgvNotBooked.RowCount > 0)
                    {
                        if (this.rgvNotBooked.Columns["NrTravel"] != null)
                            this.rgvNotBooked.Columns["NrTravel"].IsVisible = false;
                    }
                }

                btnPrintNotBooked.Visible = true;
                btnExportToExcelNotBooked.Visible = true;
            }
            else
            {
                btnPrintNotBooked.Visible = false;
                btnExportToExcelNotBooked.Visible = false;
            }

        }

        private void btnNotBooked_Click(object sender, EventArgs e)
        {
            btnPrintNotBooked.Visible = false;
            btnExportToExcelNotBooked.Visible = false;

            Cursor.Current = Cursors.WaitCursor;
            dateFromNotBooked = Convert.ToDateTime(dtFromNotBooked.Value);
            dateToNotBooked = Convert.ToDateTime(dtToNotBooked.Value);

            VolAvailabilityPreselectionBUS bus = new VolAvailabilityPreselectionBUS();
            // DataTable dt = new DataTable();
            rgvNotBooked.DataSource = null;

            List<VolVogCokGokPassModel> listaVolAva = new List<VolVogCokGokPassModel>();
            if (dateFromNotBooked <= dateToNotBooked)
            {
                lista = bus.GetNotBookedData(dateFromNotBooked, dateToNotBooked, volFunctionNotBooked, labelNotBooked);
                if (lista != null)
                {
                    rgvNotBooked.DataSource = lista;
                    listaVolAva = lista;

                    #region function
                    VolontaryFunctionModel vfm = new VolontaryFunctionModel();
                    VolontaryFunctionBUS vfBus = new VolontaryFunctionBUS();
                    List<VolontaryFunctionModel> functionAll = new List<VolontaryFunctionModel>();
                    functionAll = vfBus.GetAllFromVolFunction();

                    if (functionAll != null)
                    {
                        if (listaVolAva != null)
                        {
                            for (int i = 0; i < listaVolAva.Count; i++)
                            {
                                List<VolontaryFunctionModel> functionForPerson = new List<VolontaryFunctionModel>();
                                functionForPerson = functionAll.FindAll(s => s.idcpr == listaVolAva[i].idContPers);

                                if (functionForPerson != null)
                                {
                                    string func = "";
                                    for (int j = 0; j < functionForPerson.Count; j++)
                                    {
                                        if (j == functionForPerson.Count - 1)
                                            func = func + functionForPerson[j].txtQuest;
                                        else
                                            func = func + functionForPerson[j].txtQuest + ", ";
                                    }
                                    if (func != "")
                                        listaVolAva[i].function = func;

                                }


                            }
                        }
                    }
                    #endregion

                    #region telExp
                    PersonTelBUS ptb = new PersonTelBUS();
                    List<PersonTelModel> telAll = new List<PersonTelModel>();
                    telAll = ptb.GetAllPersonTels();

                    if (telAll != null)
                    {
                        if (listaVolAva != null)
                        {
                            for (int i = 0; i < listaVolAva.Count; i++)
                            {
                                List<PersonTelModel> telForPerson = new List<PersonTelModel>();
                                telForPerson = telAll.FindAll(s => s.idContPers == listaVolAva[i].idContPers);

                                if (telForPerson != null)
                                {
                                    string tel = "";
                                    for (int j = 0; j < telForPerson.Count; j++)
                                    {
                                        if (telForPerson[j].descriptionTel != "")
                                            tel = tel + telForPerson[j].descriptionTel + ": ";

                                        if (telForPerson[j].numberTel != "")
                                        {
                                            if (j == telForPerson.Count - 1)
                                                tel = tel + telForPerson[j].numberTel;
                                            else
                                                tel = tel + telForPerson[j].numberTel + ", ";
                                        }
                                    }
                                    if (tel != "")
                                        listaVolAva[i].phone = tel;

                                }


                            }
                        }
                    }

                    #endregion

                    #region emailExp
                    //Odavde

                    PersonEmailBUS peb = new PersonEmailBUS();
                    List<PersonEmailModel> emailAll = new List<PersonEmailModel>();
                    emailAll = peb.GetAllPersonEmails();

                    if (emailAll != null)
                    {
                        if (listaVolAva != null)
                        {
                            for (int i = 0; i < listaVolAva.Count; i++)
                            {
                                List<PersonEmailModel> emailForPerson = new List<PersonEmailModel>();
                                emailForPerson = emailAll.FindAll(s => s.idContPers == listaVolAva[i].idContPers);

                                if (emailForPerson != null)
                                {
                                    string emails = "";
                                    for (int j = 0; j < emailForPerson.Count; j++)
                                    {

                                        if (emailForPerson[j].email != "")
                                        {
                                            if (j == emailForPerson.Count - 1)
                                                emails = emails + emailForPerson[j].email;
                                            else
                                                emails = emails + emailForPerson[j].email + ", ";
                                        }
                                    }
                                    if (emails != "")
                                        listaVolAva[i].email = emails;

                                }


                            }
                        }
                    }
                    #endregion

                    rgvNotBooked.DataSource = listaVolAva;
                    if (listaVolAva != null)
                    {
                        //  rgvNotBooked.Columns["Gender"].IsVisible = false;
                        rgvNotBooked.Columns["dateExpiried"].IsVisible = false;
                        rgvNotBooked.Columns["type"].IsVisible = false;
                    }

                    if (File.Exists(layoutNotBooked))
                    {
                        rgvNotBooked.LoadLayout(layoutNotBooked);
                    }
                }
                else
                {

                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("No data for curent conditions!") != null)
                            RadMessageBox.Show(resxSet.GetString("No data for curent conditions!"));
                        else
                            RadMessageBox.Show("No data for curent conditions!");
                    }

                }

            }
            else
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Date is out of range.");

            }
            Cursor.Current = Cursors.Default;
        }

        private void gridNotBooked_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.rgvNotBooked.CurrentRow;
            VolVogCokGokPassModel vam = new VolVogCokGokPassModel();
            vam = (VolVogCokGokPassModel)info.DataBoundItem;

            VolVogCokGokPassModel volaAvaModel = (VolVogCokGokPassModel)info.DataBoundItem;

            PersonModel amodel = new PersonModel();
            amodel = new PersonBUS().GetPerson(vam.idContPers);

            if (info != null && e.RowIndex >= 0)
                if (volaAvaModel != null)
                {
                    frmPerson frm = new frmPerson(amodel);

                    frm.ShowDialog();

                }
        }

        private void gridNotBooked_KeyDown(object sender, KeyEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (this.rgvNotBooked.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    GridViewRowInfo info = this.rgvNotBooked.CurrentRow;
                    VolVogCokGokPassModel vam = new VolVogCokGokPassModel();
                    vam = (VolVogCokGokPassModel)info.DataBoundItem;

                    VolVogCokGokPassModel volaAvaModel = (VolVogCokGokPassModel)info.DataBoundItem;

                    PersonModel amodel = new PersonModel();
                    amodel = new PersonBUS().GetPerson(vam.idContPers);


                    frmPerson frm = new frmPerson(amodel);

                    frm.ShowDialog();

                    return;

                }

            }
            Cursor.Current = Cursors.Default;
        }

        private void btnFunctionNotBooked_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            VolAvailabilityPreselectionBUS ccentar3g = new VolAvailabilityPreselectionBUS();
            List<VolAvailabilityPreselectionModel> gmX3g = new List<VolAvailabilityPreselectionModel>();
            //   List<int> labelNone = new List<int>();
            gmX3g = ccentar3g.GetAllFunction(labelNotBooked);
            using (var dlgSave3g = new GridLookupFormPreselection(volFunctionNotBooked, gmX3g, "FunctionsVolontary"))
            {
                //Reset panela kada se sacuva pa odcekira
                volFunctionNotBooked = new List<VolAvailabilityPreselectionModel>();


                if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
                {
                    //Reset panela kada se sacuva pa odcekira
                    panelFunctionNotBooked.Controls.Clear();

                    volFunctionNotBooked = new List<VolAvailabilityPreselectionModel>();
                    volFunctionNotBooked = dlgSave3g.volList;

                    RadLabel rl = new RadLabel();
                    int Y = 5;
                    for (int i = 0; i < volFunctionNotBooked.Count; i++)
                    {
                        rl = new RadLabel();
                        rl.Font = new Font("Verdana", 9);
                        rl.Text = volFunctionNotBooked[i].txtQuest;
                        rl.Location = new Point(0, Y);
                        rl.AutoSize = true;
                        Y = Y + 3 + rl.Height;

                        panelFunctionNotBooked.Controls.Add(rl);

                    }
                }



            }
            Cursor.Current = Cursors.Default;
        }

        private void radMenuItemSaveTasksNotBooked_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutNotBooked))
            {
                File.Delete(layoutNotBooked);
            }
            rgvNotBooked.SaveLayout(layoutNotBooked);

            RadMessageBox.Show("Layout Saved");
        }

        private void btnPrintNotBooked_Click(object sender, System.EventArgs e)
        {
            if (rgvNotBooked != null)
                if (rgvNotBooked.Columns.Count > 0)
                {

                    this.rgvNotBooked.PrintPreview();
                }
                else
                {
                    RadMessageBox.Show("Nothing for print preview!");
                }
        }

        private void btnExportToExcelNotBooked_Click(object sender, System.EventArgs e)
        {
            if (rgvNotBooked.DataSource != null)
                if (rgvNotBooked.Columns.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.InitialDirectory = @"C:\";
                    sfd.RestoreDirectory = true;
                    sfd.FileName = radPageVolPreselection.SelectedPage.Text;
                    sfd.DefaultExt = "xls";
                    sfd.Filter = "Excel file (*.xls) | *.xls";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string path = sfd.FileName;
                        ExportToExcelML exporter = new ExportToExcelML(this.rgvNotBooked);
                        exporter.SheetMaxRows = ExcelMaxRows._1048576;

                        exporter.RunExport(path);
                        RadMessageBox.Show("Export to excel!");


                    }
                }
        }


        # endregion


        #region All Bookings

        private void btnFunctionsAB_Click(object sender, EventArgs e)
        {
            VolAvailabilityPreselectionBUS ccentar3g = new VolAvailabilityPreselectionBUS();
            List<VolAvailabilityPreselectionModel> gmX3g = new List<VolAvailabilityPreselectionModel>();

            gmX3g = ccentar3g.GetAllFunction(labelAllBooking);
            using (var dlgSave3g = new GridLookupFormPreselection(volFunctionListAb, gmX3g, "Function"))
            {
                //Reset panela kada se sacuva pa odcekira
                volFunctionListAb = new List<VolAvailabilityPreselectionModel>();


                if (dlgSave3g.ShowDialog(this) == DialogResult.Yes)
                {
                    //Reset panela kada se sacuva pa odcekira
                    panelFunctionAb.Controls.Clear();

                    volFunctionListAb = new List<VolAvailabilityPreselectionModel>();
                    volFunctionListAb = dlgSave3g.volList;

                    RadLabel rl = new RadLabel();
                    int Y = 5;
                    for (int i = 0; i < volFunctionListAb.Count; i++)
                    {
                        rl = new RadLabel();
                        rl.Font = new Font("Verdana", 9);
                        rl.Text = volFunctionListAb[i].txtQuest;
                        rl.Location = new Point(0, Y);
                        rl.AutoSize = true;
                        Y = Y + 3 + rl.Height;

                        panelFunctionAb.Controls.Add(rl);

                    }
                }

            }

        }

        private void btnPrintAb_Click(object sender, EventArgs e)
        {
            btnPrintPreviewAb.Visible = false;
            btnExportToExcelAb.Visible = false;

            rgvAllBookings.DataSource = null;

            VolAvailabilityPreselectionBUS vapb = new VolAvailabilityPreselectionBUS();
            List<VolAvailabilityAllBookingsPreselectionModel> list = new List<VolAvailabilityAllBookingsPreselectionModel>();
            if (!validateFromToAb())
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Select a date please!");
                return;
            }
            list = vapb.GetAllForAllBooking(dateFromAB, dateToAB, volFunctionListAb, labelAllBooking);
            if (list != null)
            {
                #region tel
                PersonTelBUS ptb = new PersonTelBUS();
                List<PersonTelModel> telAll = new List<PersonTelModel>();
                telAll = ptb.GetAllPersonTels();

                if (telAll != null)
                {
                    if (list != null)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            List<PersonTelModel> telForPerson = new List<PersonTelModel>();
                            telForPerson = telAll.FindAll(s => s.idContPers == list[i].idContPers);

                            if (telForPerson != null)
                            {
                                string tel = "";
                                for (int j = 0; j < telForPerson.Count; j++)
                                {
                                    if (telForPerson[j].descriptionTel != "")
                                        tel = tel + telForPerson[j].descriptionTel + ": ";

                                    if (telForPerson[j].numberTel != "")
                                    {
                                        if (j == telForPerson.Count - 1)
                                            tel = tel + telForPerson[j].numberTel;
                                        else
                                            tel = tel + telForPerson[j].numberTel + ", ";
                                    }
                                }
                                if (tel != "")
                                    list[i].numberTel = tel;

                            }


                        }
                    }
                }
                #endregion

                #region email
                //Odavde

                PersonEmailBUS peb = new PersonEmailBUS();
                List<PersonEmailModel> emailAll = new List<PersonEmailModel>();
                emailAll = peb.GetAllPersonEmails();

                if (emailAll != null)
                {
                    if (list != null)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            List<PersonEmailModel> emailForPerson = new List<PersonEmailModel>();
                            emailForPerson = emailAll.FindAll(s => s.idContPers == list[i].idContPers);

                            if (emailForPerson != null)
                            {
                                string emails = "";
                                for (int j = 0; j < emailForPerson.Count; j++)
                                {

                                    if (emailForPerson[j].email != "")
                                    {
                                        if (j == emailForPerson.Count - 1)
                                            emails = emails + emailForPerson[j].email;
                                        else
                                            emails = emails + emailForPerson[j].email + ", ";
                                    }
                                }
                                if (emails != "")
                                    list[i].email = emails;

                            }


                        }
                    }
                }
                #endregion

                #region function
                VolontaryFunctionModel vfm = new VolontaryFunctionModel();
                VolontaryFunctionBUS vfBus = new VolontaryFunctionBUS();
                List<VolontaryFunctionModel> functionAll = new List<VolontaryFunctionModel>();
                functionAll = vfBus.GetAllFromVolFunctionAB();

                if (functionAll != null)
                {
                    if (list != null)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            List<VolontaryFunctionModel> functionForPerson = new List<VolontaryFunctionModel>();
                            functionForPerson = functionAll.FindAll(s => s.idcpr == list[i].idContPers && s.idAns == list[i].idArrangement);

                            if (functionForPerson != null)
                            {
                                string func = "";
                                for (int j = 0; j < functionForPerson.Count; j++)
                                {
                                    if (j == functionForPerson.Count - 1)
                                        func = func + functionForPerson[j].txtQuest;
                                    else
                                        func = func + functionForPerson[j].txtQuest + ", ";
                                }
                                if (func != "")
                                    list[i].function = func;

                            }


                        }
                    }
                }
                #endregion

                rgvAllBookings.DataSource = list;
            }
            else
            {
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString("No data for curent conditions!") != null)
                        RadMessageBox.Show(resxSet.GetString("No data for curent conditions!"));
                    else
                        RadMessageBox.Show("No data for curent conditions!");
                }
            }
        }

        private bool validateFromToAb()
        {
            bool value = false;
            if (dtFromAB.Value != null && dtToAB.Value != null)
            {
                if (dtToAB.Value < dtFromAB.Value)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Dates were not valid!");
                }
                else
                {
                    dateToAB = dtToAB.Value;
                    dateFromAB = dtFromAB.Value;
                    value = true;
                }
            }
            return value;
        }

        private void rgvAllBookings_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.rgvAllBookings.CurrentRow;
            VolAvailabilityAllBookingsPreselectionModel vam = new VolAvailabilityAllBookingsPreselectionModel();
            vam = (VolAvailabilityAllBookingsPreselectionModel)info.DataBoundItem;

            VolAvailabilityAllBookingsPreselectionModel volaAvaModel = (VolAvailabilityAllBookingsPreselectionModel)info.DataBoundItem;

            PersonModel amodel = new PersonModel();
            amodel = new PersonBUS().GetPerson(vam.idContPers);

            if (info != null && e.RowIndex >= 0)
                if (volaAvaModel != null)
                {
                    frmPerson frm = new frmPerson(amodel);

                    frm.ShowDialog();
                }
        }

        private void rgvAllBookings_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            if (rgvAllBookings != null)
            {
                if (rgvAllBookings.Columns.Count > 0)
                {
                    foreach (var column in rgvAllBookings.Columns)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString(column.HeaderText) != null)
                                column.HeaderText = resxSet.GetString(column.HeaderText);
                        }

                        column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 85);

                    }
                }
                btnPrintPreviewAb.Visible = true;
                btnExportToExcelAb.Visible = true;
            }

            else
            {
                btnPrintPreviewAb.Visible = false;
                btnExportToExcelAb.Visible = false;
            }


            if (this.rgvAllBookings.Columns != null)
            {
                if (this.rgvAllBookings.RowCount > 0)
                {
                    if (this.rgvAllBookings.Columns["idArrangement"] != null)
                        this.rgvAllBookings.Columns["idArrangement"].IsVisible = false;
                }
            }

            if (rgvAllBookings.Columns != null)
            {
                if (rgvAllBookings.RowCount > 0)
                {
                    this.rgvAllBookings.Columns["departureDate"].FormatString = "{0: dd-MM-yyyy}";
                    this.rgvAllBookings.Columns["returnDate"].FormatString = "{0: dd-MM-yyyy}";
                }
            }


            if (File.Exists(layoutAllBooking))
            {
                rgvAllBookings.LoadLayout(layoutAllBooking);
            }
        }

        private void rgvAllBookings_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.rgvAllBookings.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    GridViewRowInfo info = this.rgvAllBookings.CurrentRow;
                    VolAvailabilityAllBookingsPreselectionModel vam = new VolAvailabilityAllBookingsPreselectionModel();
                    vam = (VolAvailabilityAllBookingsPreselectionModel)info.DataBoundItem;

                    VolAvailabilityAllBookingsPreselectionModel volaAvaModel = (VolAvailabilityAllBookingsPreselectionModel)info.DataBoundItem;
                    PersonModel amodel = new PersonModel();
                    amodel = new PersonBUS().GetPerson(vam.idContPers);


                    frmPerson frm = new frmPerson(amodel);

                    frm.ShowDialog();

                    return;

                }

            }
        }

        private void loadLabelAllBookings()
        {

            int Y = 0;
            int X = 0;
            RadRadioButton rckAb = new RadRadioButton();
            rckAb.Font = new Font("Verdana", 9);
            rckAb.Name = "chkLabel";
            rckAb.Text = "None";
            rckAb.Location = new Point(0, Y);
            rckAb.CheckStateChanged += rck_CheckStateChangedAB;
            rckAb.AutoSize = true;
            rckAb.IsChecked = true;

            Y = Y + 3 + rckAb.Height;


            panelLabelsAB.Controls.Add(rckAb);



            Y = 21;

            for (int i = 0; i < Login._arrLabels.Count; i++)
            {
                rckAb = new RadRadioButton();
                rckAb.Font = new Font("Verdana", 9);
                rckAb.Name = "chkLabel" + Login._arrLabels[i].idLabel.ToString();
                rckAb.Text = Login._arrLabels[i].nameLabel;
                rckAb.Location = new Point(0, Y);
                rckAb.CheckStateChanged += rck_CheckStateChangedAB;
                rckAb.AutoSize = true;
                Y = Y + 3 + rckAb.Height;

                panelLabelsAB.Controls.Add(rckAb);

            }
        }

        private void rck_CheckStateChangedAB(object sender, EventArgs e)
        {

            RadRadioButton rb = (RadRadioButton)sender;
            labelAllBooking = new List<int>();
            if (rb.Name.Replace("chkLabel", "") != "")
                labelAllBooking.Add(Convert.ToInt32(rb.Name.Replace("chkLabel", "")));
            else
            {
                //Za odredjene labele uzima ID i rpokazuje u lookup-u
                for (int i = 0; i < Login._arrLabels.Count; i++)
                {
                    labelAllBooking.Add(Login._arrLabels[i].idLabel);
                }
            }

            //Reset 

            volFunctionListAb = new List<VolAvailabilityPreselectionModel>();
            panelFunctionAb.Controls.Clear();


        }

        private void radMenuItemAllBokkingsSaveLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutAllBooking))
            {
                File.Delete(layoutAllBooking);
            }
            rgvAllBookings.SaveLayout(layoutAllBooking);

            RadMessageBox.Show("Layout Saved");
        }

        private void btnExportToExcelAb_Click(object sender, System.EventArgs e)
        {
            if (rgvAllBookings.DataSource != null)
                if (rgvAllBookings.Columns.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.InitialDirectory = @"C:\";
                    sfd.RestoreDirectory = true;
                    sfd.FileName = radPageVolPreselection.SelectedPage.Text;
                    sfd.DefaultExt = "xls";
                    sfd.Filter = "Excel file (*.xls) | *.xls";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string path = sfd.FileName;
                        ExportToExcelML exporter = new ExportToExcelML(this.rgvAllBookings);
                        exporter.SheetMaxRows = ExcelMaxRows._1048576;

                        exporter.RunExport(path);
                        RadMessageBox.Show("Export to excel!");


                    }
                }
        }

        private void btnPrintPreviewAb_Click(object sender, System.EventArgs e)
        {
            if (rgvAllBookings != null)
                if (rgvAllBookings.Columns.Count > 0)
                {

                    this.rgvAllBookings.PrintPreview();
                }
                else
                {
                    RadMessageBox.Show("Nothing for print preview!");
                }
        }

        #endregion

      
      

      

       

       


























































    }

}
 


      