using BIS.Business;
using BIS.DAO;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace GUI
{
    public partial class frmSalesReport : Telerik.WinControls.UI.RadForm
    {
        int idArrangement = -1;
        ArrangementModel model = new ArrangementModel();
        List <ArrangementModel> list= new List<ArrangementModel>();
        public frmSalesReport()
        {
            InitializeComponent();
        }

        // AllArrangementName
        private void btnOK_Click(object sender, EventArgs e)
        {
           // string nameArrangement = dlArangement.SelectedItem.ToString();
          //    string  arrangement= dlArangement.SelectedValue.ToString();
          //    if (arrangement != null)
           

                //   idArrangement = Convert.ToInt32(dlArangement.ValueMember);
                DataTable result = new DataTable();

                Cursor.Current = Cursors.WaitCursor;

                //  btnStragglersList
                if(idArrangement>0)
                { 

                DataTable dt1 = new DataTable();
                if (btnDevice.IsChecked == true)
                {
                    
                   Device(idArrangement);
                   //rgvResult.DataSource = Device(idArrangement);
                   dt1 = Device(idArrangement);
                                          
                }

                DataTable dt2 = new DataTable();
                if (btnDeviceOnTrip.IsChecked == true)
                {
                    DeviceOnTrip(idArrangement);
                    //rgvResult.DataSource = DeviceOnTrip(idArrangement);
                    dt2 = DeviceOnTrip(idArrangement);
                }

                DataTable dt3 = new DataTable();
                if (btnDeviceToRent.IsChecked == true)
                {

                    DeviceTorent(idArrangement);
                    //rgvResult.DataSource = DeviceTorent(idArrangement);
                    dt3 = DeviceTorent(idArrangement);
                }

                DataTable dt4 = new DataTable();
                if (btnRoomList.IsChecked == true)
                {

                    RoomList(idArrangement);
                    //     rgvResult.DataSource = RoomList(idArrangement);
                    dt4 = RoomList(idArrangement);

                }

                DataTable dt5 = new DataTable();
                if (btnPassengerList.IsChecked == true)
                {

                    PassengerList(idArrangement);
                    //    rgvResult.DataSource = PassengerList(idArrangement);
                    dt5 = PassengerList(idArrangement);
                }

                //Novo 26.1
                DataTable dt16 = new DataTable();
                if (btnParticipanList.IsChecked == true)
                {

                    ParticipanList(idArrangement);
                    //rgvResult.DataSource = ParticipanList(idArrangement);
                    dt16 = ParticipanList(idArrangement);
                }
                if (btnTeamList.IsChecked == true)
                {

                    TeamList(idArrangement);
                    //   rgvResult.DataSource = TeamList(idArrangement);

                }

                DataTable dt8 = new DataTable();
                if (btnPasportList.IsChecked == true)
                {
                    PasportList(idArrangement);
                    //rgvResult.DataSource = PasportList(idArrangement);
                    dt8 = PasportList(idArrangement);
                }

                DataTable dt9 = new DataTable();
                if (btnTelephoneList.IsChecked == true)
                {
                    TelephoneList(idArrangement);
                    //rgvResult.DataSource = TelephoneList(idArrangement);
                    dt9 = TelephoneList(idArrangement);
                }

                DataTable dt10 = new DataTable();
                if (btnStragglersList.IsChecked == true)
                {
                    StragglersList(idArrangement);
                    //rgvResult.DataSource = StragglersList(idArrangement);
                    dt10 = StragglersList(idArrangement);
                }
              

                //TeamList
                DataTable dt13 = new DataTable();
                if (btnTeamList.IsChecked == true)
                {
                    TeamList(idArrangement);
                    //rgvResult.DataSource = TeamList(idArrangement);
                    dt13 = TeamList(idArrangement);

                }

                DataTable dt12 = new DataTable();
                if (btnDivisionList.IsChecked == true)
                {
                    DivisionList(idArrangement);
                    //rgvResult.DataSource = DivisionList(idArrangement);
                    dt12 = DivisionList(idArrangement);
                }

                //Novo
                DataTable dt14 = new DataTable();
                if (btnBoardingPoint.IsChecked == true)
                {
                    BoardingPoint(idArrangement);
                    //rgvResult.DataSource = BoardingPoint(idArrangement);
                    dt14 = BoardingPoint(idArrangement);
                }

              

                //Konstruktori
                if (btnDevice.IsChecked == true)
                {
                    frmReportDevice frm1 = new frmReportDevice(dt1);
                    frm1.Show();
                }
                else if (btnDeviceOnTrip.IsChecked == true)
                {
                    frmReportDevicesOnTrip frm2 = new frmReportDevicesOnTrip(dt2);
                    frm2.Show();
                }
                else if (btnDeviceToRent.IsChecked == true)
                {
                    frmDevicesForRent frm3 = new frmDevicesForRent(dt3);
                    frm3.Show();
                }
                else if (btnRoomList.IsChecked == true)
                {
                    frmReportRoomList frm4 = new frmReportRoomList(dt4);
                    frm4.Show();
                }
                else if (btnPassengerList.IsChecked == true)
                {
                    frmReportPassengerList frm5 = new frmReportPassengerList(dt5);
                    frm5.Show();
                }
                else if (btnPasportList.IsChecked == true)
                {
                    frmReportPasseportList frm8 = new frmReportPasseportList(dt8);
                    frm8.Show();
                }
                else if (btnTelephoneList.IsChecked == true)
                {
                    frmReportTelephoneList frm9 = new frmReportTelephoneList(dt9);
                    frm9.Show();
                }
                else if (btnStragglersList.IsChecked == true)
                {
                    frmReportStagglersList frm10 = new frmReportStagglersList(dt10);
                    frm10.Show();
                }
                //else if (btnStatusOption.IsChecked == true)
                //{
                //    frmReportBookedPersonOption frm11 = new frmReportBookedPersonOption(dt11);
                //    frm11.Show();
                //}
                else if (btnDivisionList.IsChecked == true)
                {
                    frmReportDivisionList frm12 = new frmReportDivisionList(dt12);
                    frm12.Show();
                }
                else if (btnTeamList.IsChecked == true)
                {
                    frmReportTeamList frm13 = new frmReportTeamList(dt13);
                    frm13.Show();
                }
                else if (btnBoardingPoint.IsChecked == true)
                {
                    frmReportBorderingPoint frm14 = new frmReportBorderingPoint(dt14);
                    frm14.Show();
                }
                ////Novo CencelledPersons
                //else if (btnCancelledPerson.IsChecked == true)
                //{
                //    frmReportCancelledPersons frm15 = new frmReportCancelledPersons(dt15);
                //    frm15.Show();
                //}
                //Novo 26.1
                else if (btnParticipanList.IsChecked == true)
                {
                    frmReportParticipantList frm16 = new frmReportParticipantList(dt16);
                    frm16.Show();
                }

                }
                else
                {
                    if (btnStatusOption.IsChecked == false && btnCancelledPerson.IsChecked == false)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Code arrangement is required");
                    }
                }
                DataTable dt11 = new DataTable();
                if (btnStatusOption.IsChecked == true)
                {
                    StatusOption();
                    //rgvResult.DataSource = StatusOption(idArrangement);
                    dt11 = StatusOption();
                    frmReportBookedPersonOption frm11 = new frmReportBookedPersonOption(dt11);
                    frm11.Show();
                }
              //Novo za CanceledPersonReport
                DataTable dt15 = new DataTable();
                if (btnCancelledPerson.IsChecked == true)
                {
                    CanceledOption();
                    //rgvResult.DataSource = CanceledOption();
                    //dt11 = StatusOption(idArrangement);
                    dt15 = CanceledOption();
                    frmReportCancelledPersons frm15 = new frmReportCancelledPersons(dt15);
                    frm15.Show();
                }
        }

        private void frmSalesReport_Load(object sender, EventArgs e)
        {

          

            dtFrom.Value = DateTime.Now;
            dtTo.Value = DateTime.Now;
            lblDateFrom.Visible = false;
            lblDateTo.Visible = false;
            dtFrom.Visible = false;
            dtTo.Visible = false;

            //ArrangementBookBUS bus = new ArrangementBookBUS();
            //list = bus.AllArrangementName();
            //dlArangement.DataSource = list;
            //dlArangement.DisplayMember = "codeArrangement";
            //dlArangement.ValueMember = "idArrangement";

            btnDevice.Click += btnDevice_CheckStateChanged;
            btnDeviceOnTrip.Click += btnDeviceOnTrip_CheckStateChanged;
            btnDeviceToRent.Click += btnDeviceToRent_CheckStateChanged;
            btnRoomList.Click += btnRoomList_CheckStateChanged;
            btnParticipanList.Click += btnParticipanList_CheckStateChanged;
            btnPassengerList.Click += btnPassengerList_CheckStateChanged;
            btnPasportList.Click += btnPasportList_CheckStateChanged;
            btnBoardingPoint.Click += btnBoardingPoint_CheckStateChanged;
            btnTelephoneList.Click += btnTelephoneList_CheckStateChanged;
            btnStragglersList.Click += btnStragglersList_CheckStateChanged;
            btnStatusOption.Click += btnStatusOption_CheckStateChanged;
            btnCancelledPerson.Click += btnCancelledPerson_CheckStateChanged;
            btnTeamList.Click += btnTeamList_CheckStateChanged;
            btnDivisionList.Click += btnDivisionList_CheckStateChanged;


            this.Icon = Login.iconForm;
            //string formName = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();
            //formName = formName + " " + model.nameArrangement;
            string name= Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString()+"- "+ this.Name.Substring(3);

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(name) != null)
                    name = resxSet.GetString(name);
            }
            this.Text = name;

            setTranslation();
           
        }

        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblArrangement.Text) != null)
                    lblArrangement.Text = resxSet.GetString(lblArrangement.Text);
                if (resxSet.GetString(btnStragglersList.Text) != null)
                    btnStragglersList.Text = resxSet.GetString(btnStragglersList.Text);
                if (resxSet.GetString(btnDeviceOnTrip.Text) != null)
                    btnDeviceOnTrip.Text = resxSet.GetString(btnDeviceOnTrip.Text);
                if (resxSet.GetString(btnDeviceToRent.Text) != null)
                    btnDeviceToRent.Text = resxSet.GetString(btnDeviceToRent.Text);
                if (resxSet.GetString(btnRoomList.Text) != null)
                    btnRoomList.Text = resxSet.GetString(btnRoomList.Text);

                if (resxSet.GetString(btnParticipanList.Text) != null)
                    btnParticipanList.Text = resxSet.GetString(btnParticipanList.Text);
                if (resxSet.GetString(btnPassengerList.Text) != null)
                    btnPassengerList.Text = resxSet.GetString(btnPassengerList.Text);
                if (resxSet.GetString(btnPasportList.Text) != null)
                    btnPasportList.Text = resxSet.GetString(btnPasportList.Text);
                if (resxSet.GetString(btnBoardingPoint.Text) != null)
                    btnBoardingPoint.Text = resxSet.GetString(btnBoardingPoint.Text);
                if (resxSet.GetString(btnTelephoneList.Text) != null)
                    btnTelephoneList.Text = resxSet.GetString(btnTelephoneList.Text);
                if (resxSet.GetString(btnPassengerList.Text) != null)
                    btnPassengerList.Text = resxSet.GetString(btnPassengerList.Text);
                if (resxSet.GetString(btnDevice.Text) != null)
                    btnDevice.Text = resxSet.GetString(btnDevice.Text);
                if (resxSet.GetString(btnStatusOption.Text) != null)
                    btnStatusOption.Text = resxSet.GetString(btnStatusOption.Text);
                if (resxSet.GetString(btnCancelledPerson.Text) != null)
                    btnCancelledPerson.Text = resxSet.GetString(btnCancelledPerson.Text);
                if (resxSet.GetString(btnTeamList.Text) != null)
                    btnTeamList.Text = resxSet.GetString(btnTeamList.Text);
                if (resxSet.GetString(btnDivisionList.Text) != null)
                    btnDivisionList.Text = resxSet.GetString(btnDivisionList.Text);
                     if (resxSet.GetString(btnOK.Text) != null)
                    btnOK.Text = resxSet.GetString(btnOK.Text);
             
            }
        }

        private DataTable PasportList(int idArrangement)
        {
           // string arrangement = (dlArangement.ValueMember);

            DataTable dt = new DataTable();
            ArrangementBookDAO ab = new ArrangementBookDAO();
            dt = ab.PassportList(idArrangement);
            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["nameArrangement"] != null)
                        dt.Columns["nameArrangement"].Caption = "Arrangement";
                    if (dt.Columns["dtFromArrangement"] != null)
                        dt.Columns["dtFromArrangement"].Caption = "Date from";
                    if (dt.Columns["dtToArrangement"] != null)
                        dt.Columns["dtToArrangement"].Caption = "Date to";
                    if (dt.Columns["nameTitle"] != null)
                        dt.Columns["nameTitle"].Caption = "Title";
                    if (dt.Columns["passName"] != null)
                        dt.Columns["passName"].Caption = "Passport name";
                    if (dt.Columns["birthdate"] != null)
                        dt.Columns["birthdate"].Caption = "Birth date";
                    if (dt.Columns["birthPlacePassport"] != null)
                        dt.Columns["birthPlacePassport"].Caption = "Birth place";
                    if (dt.Columns["numberPassport"] != null)
                        dt.Columns["numberPassport"].Caption = "Passport number";
                    if (dt.Columns["nacionality"] != null)
                        dt.Columns["nacionality"].Caption = "Nacionality";
                    if (dt.Columns["issuePlacePassport"] != null)
                        dt.Columns["issuePlacePassport"].Caption = "Issue place";
                    if (dt.Columns["dtPassportIssued"] != null)
                        dt.Columns["dtPassportIssued"].Caption = "Issue date";
                    if (dt.Columns["dtPassportValid"] != null)
                        dt.Columns["dtPassportValid"].Caption = "Valid to";
                }
            }
            return dt;


        }
        private DataTable Device(int idArrangement)
        {
            DataTable dt = new DataTable();
            ArrangementBookDAO ab = new ArrangementBookDAO();
            ArrangementBookBUS abus = new ArrangementBookBUS();
            int nr = 0;
            dt = ab.Device(idArrangement);
            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["nameArrangement"] != null)
                        dt.Columns["nameArrangement"].Caption = "Arrangement";
                    if (dt.Columns["dtFromArrangement"] != null)
                        dt.Columns["dtFromArrangement"].Caption = "Date from";
                    if (dt.Columns["dtToArrangement"] != null)
                        dt.Columns["dtToArrangement"].Caption = "Date to";
                    if (dt.Columns["nameTitle"] != null)
                        dt.Columns["nameTitle"].Caption = "Title";
                    if (dt.Columns["name"] != null)
                        dt.Columns["name"].Caption = "Name";
                    if (dt.Columns["address"] != null)
                        dt.Columns["address"].Caption = "Address";
                    if (dt.Columns["birthdate"] != null)
                        dt.Columns["birthdate"].Caption = "Birth date";
                    if (dt.Columns["gender"] != null)
                        dt.Columns["gender"].Caption = "Gender";
                    if (dt.Columns["idContPers"] != null)
                        dt.Columns["idContPers"].Caption = "ID person";


                    nr = dt.Columns.Count; // broj kolona pre prvog prosirenja tabele
                }

                List<MedicalVoluntaryModel> ansList = new List<MedicalVoluntaryModel>();
                ansList = abus.AnsAirportcodes();
                if (ansList != null)
                {

                    int n = 1;
                    foreach (MedicalVoluntaryModel item in ansList)
                    {
                        DataColumn dc = new DataColumn("Column_" + n, typeof(string));
                        dc.DefaultValue = "";
                        dc.Caption = item.txtAns;
                        dt.Columns.Add(dc);
                        n++;
                    }


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = nr; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i]["idContPers"].ToString() != "")
                            {
                                string idP = dt.Rows[i]["idContPers"].ToString();
                                string txtAns = dt.Columns[j].Caption;


                                List<MedicalVoluntaryModel> result = new List<MedicalVoluntaryModel>();
                                result = abus.CheckAnsAirportcodes(Convert.ToInt32(idP), txtAns);
                                if (result != null)
                                {
                                    if (result.Count > 0)
                                    {
                                        dt.Rows[i][j] = "x";
                                    }

                                }
                                else
                                {
                                    dt.Rows[i][j] = "";

                                }
                            }
                        }
                    }
                }
            }
            return dt;

        }

        private DataTable DeviceOnTrip(int idArrangement)
        {
            DataTable dt = new DataTable();
            ArrangementBookDAO ab = new ArrangementBookDAO();
            ArrangementBookBUS abus = new ArrangementBookBUS();
            int nr = 0;

            dt = ab.DeviceOnTrip(idArrangement);
            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["nameArrangement"] != null)
                        dt.Columns["nameArrangement"].Caption = "Arrangement";
                    if (dt.Columns["dtFromArrangement"] != null)
                        dt.Columns["dtFromArrangement"].Caption = "Date from";
                    if (dt.Columns["dtToArrangement"] != null)
                        dt.Columns["dtToArrangement"].Caption = "Date to";
                    if (dt.Columns["nameTitle"] != null)
                        dt.Columns["nameTitle"].Caption = "Title";
                    if (dt.Columns["name"] != null)
                        dt.Columns["name"].Caption = "Name";
                    if (dt.Columns["address"] != null)
                        dt.Columns["address"].Caption = "Address";
                    if (dt.Columns["birthdate"] != null)
                        dt.Columns["birthdate"].Caption = "Birth date";
                    if (dt.Columns["gender"] != null)
                        dt.Columns["gender"].Caption = "Gender";
                }
            }
            return dt;

        }

        private DataTable DeviceTorent(int idArrangement)
        {
            DataTable dt = new DataTable();
            ArrangementBookDAO ab = new ArrangementBookDAO();
            ArrangementBookBUS abus = new ArrangementBookBUS();
            int nr = 0;
            dt = ab.DeviceToRent(idArrangement);
            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["nameArrangement"] != null)
                        dt.Columns["nameArrangement"].Caption = "Arrangement";
                    if (dt.Columns["dtFromArrangement"] != null)
                        dt.Columns["dtFromArrangement"].Caption = "Date from";
                    if (dt.Columns["dtToArrangement"] != null)
                        dt.Columns["dtToArrangement"].Caption = "Date to";
                    if (dt.Columns["nameTitle"] != null)
                        dt.Columns["nameTitle"].Caption = "Title";
                    if (dt.Columns["name"] != null)
                        dt.Columns["name"].Caption = "Name";
                    if (dt.Columns["address"] != null)
                        dt.Columns["address"].Caption = "Address";
                    if (dt.Columns["birthdate"] != null)
                        dt.Columns["birthdate"].Caption = "Birth date";
                    if (dt.Columns["gender"] != null)
                        dt.Columns["gender"].Caption = "Gender";
                    if (dt.Columns["idContPers"] != null)
                        dt.Columns["idContPers"].Caption = "ID person";


                    nr = dt.Columns.Count; // broj kolona pre prvog prosirenja tabele
                }
                //rented devices (question 32)
                List<MedicalVoluntaryModel> ansList = new List<MedicalVoluntaryModel>();
                ansList = abus.AnsRentedDevice();
                if (ansList != null)
                {

                    int n = 1;
                    foreach (MedicalVoluntaryModel item in ansList)
                    {
                        DataColumn dc = new DataColumn("Column_" + n, typeof(string));
                        dc.DefaultValue = "";
                        dc.Caption = item.txtAns;
                        dt.Columns.Add(dc);
                        n++;
                    }


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = nr; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i]["idContPers"].ToString() != "")
                            {
                                string idP = dt.Rows[i]["idContPers"].ToString();
                                string txtAns = dt.Columns[j].Caption;


                                List<MedicalVoluntaryModel> result = new List<MedicalVoluntaryModel>();
                                result = abus.CheckRentedDevice(Convert.ToInt32(idP), txtAns);
                                if (result != null)
                                {
                                    if (result.Count > 0)
                                    {
                                        dt.Rows[i][j] = "x";
                                    }

                                }
                                else
                                {
                                    dt.Rows[i][j] = "";

                                }
                            }
                        }
                    }
                }
            }
            return dt;

        }

        private DataTable RoomList(int idArrangement)
        {
            DataTable dt = new DataTable();
            ArrangementBookDAO ab = new ArrangementBookDAO();
            ArrangementBookBUS abus = new ArrangementBookBUS();
            int nr = 0;
            dt = ab.RoomList(idArrangement);
            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["nameArrangement"] != null)
                        dt.Columns["nameArrangement"].Caption = "Arrangement";
                    if (dt.Columns["dtFromArrangement"] != null)
                        dt.Columns["dtFromArrangement"].Caption = "Date from";
                    if (dt.Columns["dtToArrangement"] != null)
                        dt.Columns["dtToArrangement"].Caption = "Date to";
                    if (dt.Columns["nameTitle"] != null)
                        dt.Columns["nameTitle"].Caption = "Title";
                    if (dt.Columns["name"] != null)
                        dt.Columns["name"].Caption = "Name";
                    if (dt.Columns["idRoom"] != null)
                        dt.Columns["idRoom"].Caption = "Nr room";
                    if (dt.Columns["idContPers"] != null)
                        dt.Columns["idContPers"].Caption = "ID person";



                    nr = dt.Columns.Count; // broj kolona pre prvog prosirenja tabele
                }
                //diets(question 14)
                List<MedicalVoluntaryModel> ansList = new List<MedicalVoluntaryModel>();
                ansList = abus.AnsDiets();
                if (ansList != null)
                {
                    int n1 = 111;
                    foreach (MedicalVoluntaryModel item in ansList)
                    {
                        DataColumn dc = new DataColumn("Column_" + n1, typeof(string));
                        dc.DefaultValue = "";
                        dc.Caption = item.txtAns;
                        dt.Columns.Add(dc);
                        n1++;

                    }




                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = nr; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i]["idContPers"].ToString() != "")
                            {
                                string idP = dt.Rows[i]["idContPers"].ToString();
                                string txtAns = dt.Columns[j].Caption;


                                List<MedicalVoluntaryModel> result = new List<MedicalVoluntaryModel>();
                                result = abus.CheckAnsDiets(Convert.ToInt32(idP), txtAns);
                                if (result != null)
                                {
                                    if (result.Count > 0)
                                    {
                                        dt.Rows[i][j] = "x";
                                    }

                                }
                                else
                                {
                                    dt.Rows[i][j] = "";

                                }
                            }
                        }
                    }
                }
                int nr1 = dt.Columns.Count;
                //devices(question 6)
                List<MedicalVoluntaryModel> ansList1 = new List<MedicalVoluntaryModel>();
                ansList1 = abus.AnsDevice();
                if (ansList1 != null)
                {
                    //foreach (MedicalVoluntaryModel item in ansList1)
                    //{
                    //    DataColumn dc = new DataColumn(item.txtAns, typeof(string));
                    //    dc.DefaultValue = "";
                    //    dc.Caption = item.txtAns;
                    //    dt.Columns.Add(dc);

                    //}
                    //Brise space u poljima

                    int n = 1;
                    foreach (MedicalVoluntaryModel item in ansList1)
                    {
                        DataColumn dc = new DataColumn("Column_" + n, typeof(string));
                        dc.DefaultValue = "";
                        dc.Caption = item.txtAns;
                        dt.Columns.Add(dc);
                        n++;
                    }


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = nr1; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i]["idContPers"].ToString() != "")
                            {
                                string idP = dt.Rows[i]["idContPers"].ToString();
                                string txtAns = dt.Columns[j].Caption;


                                List<MedicalVoluntaryModel> result = new List<MedicalVoluntaryModel>();
                                result = abus.CheckAnsDevice(Convert.ToInt32(idP), txtAns);
                                if (result != null)
                                {
                                    if (result.Count > 0)
                                    {
                                        dt.Rows[i][j] = "x";
                                    }

                                }
                                else
                                {
                                    dt.Rows[i][j] = "";

                                }
                            }
                        }
                    }
                }
            }

            return dt;

        }
        private DataTable ParticipanList(int idArrangement)
        {    
        
            DataTable dt = new DataTable();
            ArrangementBookDAO ab = new ArrangementBookDAO();
            ArrangementBookBUS abus = new ArrangementBookBUS();
            int nr = 0;
            dt = ab.ParticipationList(idArrangement);
            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["nameArrangement"] != null)
                        dt.Columns["nameArrangement"].Caption = "Arrangement";
                    if (dt.Columns["dtFromArrangement"] != null)
                        dt.Columns["dtFromArrangement"].Caption = "Date from";
                    if (dt.Columns["dtToArrangement"] != null)
                        dt.Columns["dtToArrangement"].Caption = "Date to";
                    if (dt.Columns["nameTitle"] != null)
                        dt.Columns["nameTitle"].Caption = "Title";
                    if (dt.Columns["name"] != null)
                        dt.Columns["name"].Caption = "Name";
                    if (dt.Columns["nameBoardingPoint"] != null)
                        dt.Columns["nameBoardingPoint"].Caption = "Boarding point";
                    if (dt.Columns["idContPers"] != null)
                        dt.Columns["idContPers"].Caption = "ID person";
                    if (dt.Columns["city"] != null)
                        dt.Columns["city"].Caption = "City";
                }

                nr = dt.Columns.Count; // broj kolona pre prvog prosirenja tabele

              //  DataTable dataResult = new DataTable();
                dt=telStatus1(dt, nr);
                int nr1 = dt.Columns.Count;
                dt=telStatus2(dt, nr1);
                int nr2 = dt.Columns.Count;
                dt=telStatus3(dt, nr2);
                int nr3 = dt.Columns.Count;
               dt= telStatus5(dt, nr3);
                int nr4 = dt.Columns.Count;
               dt= telStatus7(dt, nr4);

 

            }


            return dt;

        }
        # region ParticipanListTelephone
        private DataTable telStatus1( DataTable dt, int nr)
        {
            ArrangementBookBUS abus =new ArrangementBookBUS();
            List<PersonTelModel> listaTel = new List<PersonTelModel>();
            ;
            int brojTSt1 = 0;
            List<PersonTelModel> idContList = new List<PersonTelModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["idContPers"].ToString() != "")
                {
                    int id = Convert.ToInt32(dt.Rows[i]["idContPers"].ToString());
                    idContList = abus.TelSrtatus(id, 1);

                    if (idContList != null)
                    {
                        if (idContList.Count > 0)
                        {
                            if (idContList.Count > brojTSt1)
                                brojTSt1 = idContList.Count;

                        }

                    }

                }
                idContList = null;


            }
            List<string> listaBrojeva = new List<string>();
            for (int i = 0; i < brojTSt1; i++)
            {
                if (i == 0)
                {
                    listaBrojeva.Add("Private");
                }
                else
                {

                    listaBrojeva.Add("Private" + i.ToString());
                }
            }
            if (listaBrojeva.Count > 0)
            {
                int n = nr;
                foreach (var item in listaBrojeva)
                {
                    DataColumn dc = new DataColumn("Column_" + n, typeof(string));
                    dc.Caption = item;
                    dc.DefaultValue = "";
                    dt.Columns.Add(dc);
                    n++;
                }

                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    count = 0;
                    for (int j = nr; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i]["idContPers"].ToString() != "")
                        {
                            string idP = dt.Rows[i]["idContPers"].ToString();
                            //   string quest = dt.Columns[j].Caption;

                            List<PersonTelModel> result = new List<PersonTelModel>();
                            result = abus.TelSrtatus(Convert.ToInt32(idP),1);
                            if (result != null)
                            {
                                if (result.Count > 0)
                                {
                                    if (count < result.Count)
                                    {
                                        dt.Rows[i][j] = result[j - nr].numberTel;
                                        count++;
                                    }
                                }
                            }
                            else
                            {
                                dt.Rows[i][j] = "";

                            }
                        }

                    }
                }


            }
            return dt;
        }
        private DataTable telStatus2(DataTable dt, int nr)
        {
            ArrangementBookBUS abus = new ArrangementBookBUS();
            List<PersonTelModel> listaTel = new List<PersonTelModel>();
          
            int brojTSt1 = 0;
            List<PersonTelModel> idContList = new List<PersonTelModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["idContPers"].ToString() != "")
                {
                    int id = Convert.ToInt32(dt.Rows[i]["idContPers"].ToString());
                    idContList = abus.TelSrtatus(id, 2);

                    if (idContList != null)
                    {
                        if (idContList.Count > 0)
                        {
                            if (idContList.Count > brojTSt1)
                                brojTSt1 = idContList.Count;

                        }

                    }

                }
                idContList = null;


            }
            List<string> listaBrojeva = new List<string>();
            for (int i = 0; i < brojTSt1; i++)
            {
                if (i == 0)
                {
                    listaBrojeva.Add("Mobile");
                }
                else
                {

                    listaBrojeva.Add("Mobile" + i.ToString());
                }
            }
            if (listaBrojeva.Count > 0)
            {
                int n = nr;
                foreach (var item in listaBrojeva)
                {
                    DataColumn dc = new DataColumn("Column_" + n, typeof(string));
                    dc.Caption = item;
                    dc.DefaultValue = "";
                    dt.Columns.Add(dc);
                    n++;
                }

                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    count = 0;
                    for (int j = nr; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i]["idContPers"].ToString() != "")
                        {
                            string idP = dt.Rows[i]["idContPers"].ToString();
                            //   string quest = dt.Columns[j].Caption;

                            List<PersonTelModel> result = new List<PersonTelModel>();
                            result = abus.TelSrtatus(Convert.ToInt32(idP),2);
                            if (result != null)
                            {
                                if (result.Count > 0)
                                {
                                    if (count < result.Count)
                                    {
                                        dt.Rows[i][j] = result[j - nr].numberTel;
                                        count++;
                                    }
                                }
                            }
                            else
                            {
                                dt.Rows[i][j] = "";

                            }
                        }

                    }
                }


            }
            return dt;
        }

        private DataTable telStatus3(DataTable dt, int nr)
        {
            ArrangementBookBUS abus = new ArrangementBookBUS();
            List<PersonTelModel> listaTel = new List<PersonTelModel>();
          
            int brojTSt1 = 0;
            List<PersonTelModel> idContList = new List<PersonTelModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["idContPers"].ToString() != "")
                {
                    int id = Convert.ToInt32(dt.Rows[i]["idContPers"].ToString());
                    idContList = abus.TelSrtatus(id, 3);

                    if (idContList != null)
                    {
                        if (idContList.Count > 0)
                        {
                            if (idContList.Count > brojTSt1)
                                brojTSt1 = idContList.Count;

                        }

                    }

                }
                idContList = null;


            }
            List<string> listaBrojeva = new List<string>();
            for (int i = 0; i < brojTSt1; i++)
            {
                if (i == 0)
                {
                    listaBrojeva.Add("Home");
                }
                else
                {

                    listaBrojeva.Add("Home" + i.ToString());
                }
            }
            if (listaBrojeva.Count > 0)
            {
                int n = nr;
                foreach (var item in listaBrojeva)
                {
                    DataColumn dc = new DataColumn("Column_" + n, typeof(string));
                    dc.Caption = item;
                    dc.DefaultValue = "";
                    dt.Columns.Add(dc);
                    n++;
                }

                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    count = 0;
                    for (int j = nr; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i]["idContPers"].ToString() != "")
                        {
                            string idP = dt.Rows[i]["idContPers"].ToString();
                            //   string quest = dt.Columns[j].Caption;

                            List<PersonTelModel> result = new List<PersonTelModel>();
                            result = abus.TelSrtatus(Convert.ToInt32(idP),3);
                            if (result != null)
                            {
                                if (result.Count > 0)
                                {
                                    if (count < result.Count)
                                    {
                                        dt.Rows[i][j] = result[j - nr].numberTel;
                                        count++;
                                    }
                                }
                            }
                            else
                            {
                                dt.Rows[i][j] = "";

                            }
                        }

                    }
                }


            }
            return dt;
        }
        private DataTable telStatus5(DataTable dt, int nr)
        {
            ArrangementBookBUS abus = new ArrangementBookBUS();
            List<PersonTelModel> listaTel = new List<PersonTelModel>();
          
            int brojTSt1 = 0;
            List<PersonTelModel> idContList = new List<PersonTelModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["idContPers"].ToString() != "")
                {
                    int id = Convert.ToInt32(dt.Rows[i]["idContPers"].ToString());
                    idContList = abus.TelSrtatus(id, 5);

                    if (idContList != null)
                    {
                        if (idContList.Count > 0)
                        {
                            if (idContList.Count > brojTSt1)
                                brojTSt1 = idContList.Count;

                        }

                    }

                }
                idContList = null;


            }
            List<string> listaBrojeva = new List<string>();
            for (int i = 0; i < brojTSt1; i++)
            {
                if (i == 0)
                {
                    listaBrojeva.Add("Parents");
                }
                else
                {

                    listaBrojeva.Add("Parents" + i.ToString());
                }
            }
            if (listaBrojeva.Count > 0)
            {
                int n = nr;
                foreach (var item in listaBrojeva)
                {
                    DataColumn dc = new DataColumn("Column_" + n, typeof(string));
                    dc.Caption = item;
                    dc.DefaultValue = "";
                    dt.Columns.Add(dc);
                    n++;
                }
                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    count = 0;
                    for (int j = nr; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i]["idContPers"].ToString() != "")
                        {
                            string idP = dt.Rows[i]["idContPers"].ToString();
                            //   string quest = dt.Columns[j].Caption;

                            List<PersonTelModel> result = new List<PersonTelModel>();
                            result = abus.TelSrtatus(Convert.ToInt32(idP),5);
                            if (result != null)
                            {
                                if (result.Count > 0)
                                {
                                    if (count < result.Count)
                                    {
                                        dt.Rows[i][j] = result[j - nr].numberTel;
                                        count++;
                                    }
                                }
                            }
                            else
                            {
                                dt.Rows[i][j] = "";

                            }
                        }

                    }
                }


            }
            return dt;
        }
        private DataTable telStatus7(DataTable dt, int nr)
        {
            ArrangementBookBUS abus = new ArrangementBookBUS();
            List<PersonTelModel> listaTel = new List<PersonTelModel>();
         
            int brojTSt1 = 0;
            List<PersonTelModel> idContList = new List<PersonTelModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["idContPers"].ToString() != "")
                {
                    int id = Convert.ToInt32(dt.Rows[i]["idContPers"].ToString());
                    idContList = abus.TelSrtatus(id, 7);

                    if (idContList != null)
                    {
                        if (idContList.Count > 0)
                        {
                            if (idContList.Count > brojTSt1)
                                brojTSt1 = idContList.Count;

                        }

                    }

                }
                idContList = null;


            }
            List<string> listaBrojeva = new List<string>();
            for (int i = 0; i < brojTSt1; i++)
            {
                if (i == 0)
                {
                    listaBrojeva.Add("Noodnummer");
                }
                else
                {

                    listaBrojeva.Add("Noodnummer" + i.ToString());
                }
            }
            if (listaBrojeva.Count > 0)
            {
                int n = nr;
                foreach (var item in listaBrojeva)
                {
                    DataColumn dc = new DataColumn("Column_" + n, typeof(string));
                    dc.Caption = item;
                    dc.DefaultValue = "";
                    dt.Columns.Add(dc);
                    n++;
                }

                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    count = 0;
                    for (int j = nr; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i]["idContPers"].ToString() != "")
                        {
                            string idP = dt.Rows[i]["idContPers"].ToString();
                            //   string quest = dt.Columns[j].Caption;

                            List<PersonTelModel> result = new List<PersonTelModel>();
                            result = abus.TelSrtatus(Convert.ToInt32(idP),7);
                            if (result != null)
                            {
                                if (result.Count > 0)
                                {
                                    if (count < result.Count)
                                    {
                                        dt.Rows[i][j] = result[j - nr].numberTel;
                                        count++;
                                    }
                                }
                            }
                            else
                            {
                                dt.Rows[i][j] = "";

                            }
                        }

                    }
                }


            }
            return dt;
        }

        # endregion

        

        private DataTable PassengerList(int idArrangement)
        {
            DataTable dt = new DataTable();
            ArrangementBookDAO ab = new ArrangementBookDAO();
            ArrangementBookBUS abus = new ArrangementBookBUS();
            int nr = 0;
            dt = ab.PassengerList(idArrangement);
            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["nameArrangement"] != null)
                        dt.Columns["nameArrangement"].Caption = "Arrangement";
                    if (dt.Columns["dtFromArrangement"] != null)
                        dt.Columns["dtFromArrangement"].Caption = "Date from";
                    if (dt.Columns["dtToArrangement"] != null)
                        dt.Columns["dtToArrangement"].Caption = "Date to";
                    if (dt.Columns["nameTitle"] != null)
                        dt.Columns["nameTitle"].Caption = "Title";
                    if (dt.Columns["name"] != null)
                        dt.Columns["name"].Caption = "Name";
                    if (dt.Columns["idContPers"] != null)
                        dt.Columns["idContPers"].Caption = "ID person";
                    if (dt.Columns["nrBooked"] != null)
                        dt.Columns["nrBooked"].Caption="Nr booked trips";


                    nr = dt.Columns.Count; // broj kolona pre prvog prosirenja tabele
                }

                //diet(question 14)
                List<MedicalVoluntaryModel> ansList = new List<MedicalVoluntaryModel>();
                ansList = abus.AnsDiets();
                if (ansList != null)
                {


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = nr; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i]["idContPers"].ToString() != "")
                            {
                                string idP = dt.Rows[i]["idContPers"].ToString();
                                string txtAns = dt.Columns[j].Caption;


                                List<MedicalVoluntaryModel> result = new List<MedicalVoluntaryModel>();
                                result = abus.CheckAnsDiets(Convert.ToInt32(idP), txtAns);
                                if (result != null)
                                {
                                    if (result.Count > 0)
                                    {
                                        dt.Rows[i][j] = "x";
                                    }

                                }
                                else
                                {
                                    dt.Rows[i][j] = "";

                                }
                            }
                        }
                    }
                }
                int nr2 = dt.Columns.Count;
                //epilepsie(queston 15)
                List<MedicalVoluntaryModel> ansList2 = new List<MedicalVoluntaryModel>();
                ansList2 = abus.AnsEpilepsie();
                if (ansList2 != null)
                {


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = nr2; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i]["idContPers"].ToString() != "")
                            {
                                string idP = dt.Rows[i]["idContPers"].ToString();
                                string txtAns = dt.Columns[j].Caption;


                                List<MedicalVoluntaryModel> result2 = new List<MedicalVoluntaryModel>();
                                result2 = abus.CheckAnsEpilepsie(Convert.ToInt32(idP), txtAns);
                                if (result2 != null)
                                {
                                    if (result2.Count > 0)
                                    {
                                        dt.Rows[i][j] = "x";
                                    }

                                }
                                else
                                {
                                    dt.Rows[i][j] = "";

                                }
                            }
                        }
                    }
                }
                int nr1 = dt.Columns.Count;
                //   medication (question 16)
                List<MedicalVoluntaryModel> ansList1 = new List<MedicalVoluntaryModel>();
                ansList1 = abus.AnsMedication();
                if (ansList1 != null)
                {

                    int n = 1;
                    foreach (MedicalVoluntaryModel item in ansList1)
                    {
                        DataColumn dc = new DataColumn("Column_" + n, typeof(string));
                        dc.DefaultValue = "";
                        dc.Caption = item.txtAns;
                        dt.Columns.Add(dc);
                        n++;
                    }


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = nr1; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i]["idContPers"].ToString() != "")
                            {
                                string idP = dt.Rows[i]["idContPers"].ToString();
                                string txtAns = dt.Columns[j].Caption;


                                List<MedicalVoluntaryModel> result = new List<MedicalVoluntaryModel>();
                                result = abus.CheckAnsMedication(Convert.ToInt32(idP), txtAns);
                                if (result != null)
                                {
                                    if (result.Count > 0)
                                    {
                                        dt.Rows[i][j] = "x";
                                    }

                                }
                                else
                                {
                                    dt.Rows[i][j] = "";

                                }
                            }
                        }
                    }
                }

                int nr3 = dt.Columns.Count;
                DataColumn dc1 = new DataColumn("TripsBooked", typeof(int));
                dc1.Caption = "trips booked";
                dt.Columns.Add(dc1);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = nr3; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i]["idContPers"].ToString() != "")
                            {
                                string idP = dt.Rows[i]["idContPers"].ToString();

                                List<ArrangementModel> result = new List<ArrangementModel>();
                                result = abus.NrBookedTrips(Convert.ToInt32(idP));
                                if (result != null)
                                {
                                    if (result.Count > 0)
                                        dt.Rows[i][j] = result[0].nrTraveler;
                                }
                                else
                                {
                                    dt.Rows[i][j] = "";

                                }
                            }
                        }
                    }

                }
            }
            return dt;

        }

        private DataTable TelephoneList(int idArrangement)
        {
            DataTable dt = new DataTable();
            ArrangementBookDAO ab = new ArrangementBookDAO();
            ArrangementBookBUS abus = new ArrangementBookBUS();
            int nr = 0;
            dt = ab.Telephone(idArrangement);
            if (dt != null)
            {

                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["nameArrangement"] != null)
                        dt.Columns["nameArrangement"].Caption = "Arrangement";
                    if (dt.Columns["dtFromArrangement"] != null)
                        dt.Columns["dtFromArrangement"].Caption = "Date from";
                    if (dt.Columns["dtToArrangement"] != null)
                        dt.Columns["dtToArrangement"].Caption = "Date to";
                    if (dt.Columns["nameTitle"] != null)
                        dt.Columns["nameTitle"].Caption = "Title";
                    if (dt.Columns["name"] != null)
                        dt.Columns["name"].Caption = "Name";
                    if (dt.Columns["address"] != null)
                        dt.Columns["address"].Caption = "Address";
                    if (dt.Columns["birthdate"] != null)
                        dt.Columns["birthdate"].Caption = "Birth date";
                    if (dt.Columns["gender"] != null)
                        dt.Columns["gender"].Caption = "Gender";
                    if (dt.Columns["idContPers"] != null)
                        dt.Columns["idContPers"].Caption = "ID person";

                    nr = dt.Columns.Count; // broj kolona pre prvog prosirenja tabele

                }
                ArrangementBookBUS bus = new ArrangementBookBUS();

                List<PersonTelModel> listaTel = new List<PersonTelModel>();

                // broj koliko neka osoba ima brojeva telefona
                int brojT = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int idP = Convert.ToInt32(dt.Rows[i]["idContPers"]);

                    listaTel = bus.AllTelephoneList(idP);
                    // koliko brojeva telefona ima posmatrana osoba
                    if (listaTel != null)
                    {
                        if (listaTel.Count > brojT)
                            brojT = listaTel.Count;

                        listaTel = null;
                    }
                }
                List<string> listaBrojeva = new List<string>();
                if (brojT != 0)
                {
                    for (int i = 0; i < brojT; i++)
                    {
                        if (i == 0)
                        {
                            listaBrojeva.Add("Tel");
                        }
                        else
                        {

                            listaBrojeva.Add("Tel" + i.ToString());
                        }
                    }
                    foreach (string item in listaBrojeva)
                    {
                        DataColumn dc = new DataColumn(item, typeof(string));
                        dc.DefaultValue = "";
                        dt.Columns.Add(dc);
                    }
                }
                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    count = 0;
                    for (int j = nr; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i]["idContPers"].ToString() != "")
                        {
                            string idP = dt.Rows[i]["idContPers"].ToString();
                            //   string quest = dt.Columns[j].Caption;

                            List<PersonTelModel> result = new List<PersonTelModel>();
                            result = bus.AllTelephoneList(Convert.ToInt32(idP));
                            if (result != null)
                            {
                                if (result.Count > 0)
                                {
                                    if (count < result.Count)
                                    {
                                        dt.Rows[i][j] = result[j - nr].numberTel;
                                        count++;
                                    }
                                }
                            }
                            else
                            {
                                dt.Rows[i][j] = "";

                            }
                        }

                    }
                }
            }
            return dt;

        }

        private DataTable StragglersList(int idArrangement)
        {
            DataTable dt = new DataTable();
            ArrangementBookDAO ab = new ArrangementBookDAO();
            ArrangementBookBUS abus = new ArrangementBookBUS();
            int nr = 0;

            dt = ab.StragglersList(idArrangement);

            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["dtFromArrangement"] != null)
                        dt.Columns["dtFromArrangement"].Caption = "Date from";
                    if (dt.Columns["dtToArrangement"] != null)
                        dt.Columns["dtToArrangement"].Caption = "Date to";
                    if (dt.Columns["idClient"] != null)
                        dt.Columns["idClient"].Caption = "ID client";
                    if (dt.Columns["nameClient"] != null)
                        dt.Columns["nameClient"].Caption = "Client name";


                    nr = dt.Columns.Count; // broj kolona pre prvog prosirenja tabele
                }
                List<ClientTelModel> listaTel = new List<ClientTelModel>();

                // broj koliko neka osoba ima brojeva telefona
                int brojT = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int idP = Convert.ToInt32(dt.Rows[i]["idClient"]);

                    listaTel = abus.ClientTel(idP);
                    // koliko brojeva telefona ima posmatrana osoba
                    if (listaTel != null)
                    {
                        if (listaTel.Count > brojT)
                            brojT = listaTel.Count;

                        listaTel = null;
                    }
                }
                List<string> listaBrojeva = new List<string>();
                if (brojT != 0)
                {
                    for (int i = 0; i < brojT; i++)
                    {
                        if (i == 0)
                        {
                            listaBrojeva.Add("Tel");
                        }
                        else
                        {

                            listaBrojeva.Add("Tel" + i.ToString());
                        }
                    }
                    foreach (string item in listaBrojeva)
                    {
                        DataColumn dc = new DataColumn(item, typeof(string));
                        dc.DefaultValue = "";
                        dt.Columns.Add(dc);
                    }
                }
                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    count = 0;
                    for (int j = nr; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i]["idClient"].ToString() != "")
                        {
                            string idP = dt.Rows[i]["idClient"].ToString();
                            //   string quest = dt.Columns[j].Caption;

                            List<ClientTelModel> result = new List<ClientTelModel>();
                            result = abus.ClientTel(Convert.ToInt32(idP));
                            if (result != null)
                            {
                                if (result.Count > 0)
                                {
                                    if (count < result.Count)
                                    {
                                        dt.Rows[i][j] = result[j - nr].numberTel;
                                        count++;
                                    }
                                }
                            }
                            else
                            {
                                dt.Rows[i][j] = "";

                            }
                        }

                    }
                }
                // broj kolona posle dodavanja brojeva telefona
                int nr1 = dt.Columns.Count;

                // dodavanje emaila
                List<ClientEmailModel> listaEmail = new List<ClientEmailModel>();
                // broj koliko neka osoba ima brojeva defaultnih email-ova
                int brojE = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int idP = Convert.ToInt32(dt.Rows[i]["idClient"]);

                    listaEmail = abus.ClientEmail(idP);
                    // koliko emailova ima posmatrana osoba
                    if (listaEmail != null)
                    {
                        if (listaEmail.Count > brojE)
                            brojE = listaEmail.Count;

                        listaEmail = null;
                    }
                }
                List<string> listaEm = new List<string>();
                if (brojE != 0)
                {
                    for (int i = 0; i < brojE; i++)
                    {
                        if (i == 0)
                        {
                            listaEm.Add("Email");
                        }
                        else
                        {

                            listaEm.Add("Email" + i.ToString());
                        }
                    }


                    foreach (string item in listaEm)
                    {
                        DataColumn dc = new DataColumn(item, typeof(string));
                        dc.DefaultValue = "";
                        dt.Columns.Add(dc);
                    }
                }
                int countE = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    countE = 0;
                    for (int j = nr1; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i]["idClient"].ToString() != "")
                        {
                            string idP = dt.Rows[i]["idClient"].ToString();
                            //   string quest = dt.Columns[j].Caption;

                            List<ClientEmailModel> resultE = new List<ClientEmailModel>();
                            resultE = abus.ClientEmail(Convert.ToInt32(idP));
                            if (resultE != null)
                            {
                                if (resultE.Count > 0)
                                {
                                    if (countE < resultE.Count)
                                    {
                                        dt.Rows[i][j] = resultE[j - nr1].email;
                                        countE++;
                                    }
                                }
                            }
                            else
                            {
                                dt.Rows[i][j] = "";

                            }
                        }

                    }
                }



            }
            return dt;

        }

        # region event
        private void btnDivisionList_CheckStateChanged(object sender, EventArgs e)
        {
            btnarrangement.Visible = true;
            txtCodeArrangement.Visible = true;
            lblArrangement.Visible = true;
            lblDateFrom.Visible = false;
            lblDateTo.Visible = false;
            dtFrom.Visible = false;
            dtTo.Visible = false;
        }

        private void btnTeamList_CheckStateChanged(object sender, EventArgs e)
        {
            btnarrangement.Visible = true;
            txtCodeArrangement.Visible = true;
            lblArrangement.Visible = true;
            lblDateFrom.Visible = false;
            lblDateTo.Visible = false;
            dtFrom.Visible = false;
            dtTo.Visible = false;
        }

        private void btnCancelledPerson_CheckStateChanged(object sender, EventArgs e)
        {
            btnarrangement.Visible = false;
            txtCodeArrangement.Visible = false;
            lblArrangement.Visible = false;
            lblDateFrom.Visible = true;
            lblDateTo.Visible = true;
            dtFrom.Visible = true;
            dtTo.Visible = true;
        }

        private void btnDeviceToRent_CheckStateChanged(object sender, EventArgs e)
        {
            btnarrangement.Visible = true;
            txtCodeArrangement.Visible = true;
            lblArrangement.Visible = true;
            lblDateFrom.Visible = false;
            lblDateTo.Visible = false;
            dtFrom.Visible = false;
            dtTo.Visible = false;
        }

        private void btnStragglersList_CheckStateChanged(object sender, EventArgs e)
        {
            btnarrangement.Visible = true;
            txtCodeArrangement.Visible = true;
            lblArrangement.Visible = true;
            lblDateFrom.Visible = false;
            lblDateTo.Visible = false;
            dtFrom.Visible = false;
            dtTo.Visible = false;
        }

        private void btnTelephoneList_CheckStateChanged(object sender, EventArgs e)
        {
            btnarrangement.Visible = true;
            txtCodeArrangement.Visible = true;
            lblArrangement.Visible = true;
            lblDateFrom.Visible = false;
            lblDateTo.Visible = false;
            dtFrom.Visible = false;
            dtTo.Visible = false;
        }

        private void btnBoardingPoint_CheckStateChanged(object sender, EventArgs e)
        {
            btnarrangement.Visible = true;
            txtCodeArrangement.Visible = true;
            lblArrangement.Visible = true;
            lblDateFrom.Visible = false;
            lblDateTo.Visible = false;
            dtFrom.Visible = false;
            dtTo.Visible = false;
        }

        private void btnPasportList_CheckStateChanged(object sender, EventArgs e)
        {
            btnarrangement.Visible = true;
            txtCodeArrangement.Visible = true;
            lblArrangement.Visible = true;
            lblDateFrom.Visible = false;
            lblDateTo.Visible = false;
            dtFrom.Visible = false;
            dtTo.Visible = false;
        }

        private void btnPassengerList_CheckStateChanged(object sender, EventArgs e)
        {
            btnarrangement.Visible = true;
            txtCodeArrangement.Visible = true;
            lblArrangement.Visible = true;
            lblDateFrom.Visible = false;
            lblDateTo.Visible = false;
            dtFrom.Visible = false;
            dtTo.Visible = false;
        }

        private void btnParticipanList_CheckStateChanged(object sender, EventArgs e)
        {
            btnarrangement.Visible = true;
            txtCodeArrangement.Visible = true;
            lblArrangement.Visible = true;
            lblDateFrom.Visible = false;
            lblDateTo.Visible = false;
            dtFrom.Visible = false;
            dtTo.Visible = false;
        }

        private void btnRoomList_CheckStateChanged(object sender, EventArgs e)
        {
            btnarrangement.Visible = true;
            txtCodeArrangement.Visible = true;
            lblArrangement.Visible = true;
            lblDateFrom.Visible = false;
            lblDateTo.Visible = false;
            dtFrom.Visible = false;
            dtTo.Visible = false;
        }

        private void btnDeviceOnTrip_CheckStateChanged(object sender, EventArgs e)
        {
            btnarrangement.Visible = true;
            txtCodeArrangement.Visible = true;
            lblArrangement.Visible = true;
            lblDateFrom.Visible = false;
            lblDateTo.Visible = false;
            dtFrom.Visible = false;
            dtTo.Visible = false;
        }
        private void btnDevice_CheckStateChanged(object sender, EventArgs e)
        {
            btnarrangement.Visible = true;
            txtCodeArrangement.Visible = true;
            lblArrangement.Visible = true;
            lblDateFrom.Visible = false;
            lblDateTo.Visible = false;
            dtFrom.Visible = false;
            dtTo.Visible = false;
        }

        private void btnStatusOption_CheckStateChanged(object sender, EventArgs e)
        {
            btnarrangement.Visible = false;
            txtCodeArrangement.Visible = false;
            lblArrangement.Visible = false;
            lblDateFrom.Visible = true;
            lblDateTo.Visible = true;
            dtFrom.Visible = true;
            dtTo.Visible = true;
        }
        #endregion

        ////////////////////////////////////////////
        private DataTable StatusOption()
        {
            model.dtFromArrangement = Convert.ToDateTime(dtFrom.Value);
            model.dtToArrangement = Convert.ToDateTime(dtTo.Value);
            DataTable dt = new DataTable();
            ArrangementBookDAO ab = new ArrangementBookDAO();
            ArrangementBookBUS abus = new ArrangementBookBUS();
            int nr = 0;
            dt = ab.StatusOption(model.dtFromArrangement, model.dtToArrangement);

            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["nameArrangement"] != null)
                        dt.Columns["nameArrangement"].Caption = "Arrangement";
                    if (dt.Columns["dtFromArrangement"] != null)
                        dt.Columns["dtFromArrangement"].Caption = "Date from";
                    if (dt.Columns["dtToArrangement"] != null)
                        dt.Columns["dtToArrangement"].Caption = "Date to";
                    if (dt.Columns["nameTitle"] != null)
                        dt.Columns["nameTitle"].Caption = "Title";
                    if (dt.Columns["name"] != null)
                        dt.Columns["name"].Caption = "Name";
                    if (dt.Columns["idContPers"] != null)
                        dt.Columns["idContPers"].Caption = "ID person";

                    nr = dt.Columns.Count; // broj kolona pre prvog prosirenja tabele
                }
                List<PersonTelModel> listaTel = new List<PersonTelModel>();

                // broj koliko neka osoba ima brojeva telefona
                int brojT = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int idP = Convert.ToInt32(dt.Rows[i]["idContPers"]);

                    listaTel = abus.GetCpTel(idP);
                    // koliko brojeva telefona ima posmatrana osoba
                    if (listaTel != null)
                    {
                        if (listaTel.Count > brojT)
                            brojT = listaTel.Count;

                        listaTel = null;
                    }
                }
                List<string> listaBrojeva = new List<string>();
                if (brojT != 0)
                {
                    for (int i = 0; i < brojT; i++)
                    {
                        if (i == 0)
                        {
                            listaBrojeva.Add("Tel");
                        }
                        else
                        {

                            listaBrojeva.Add("Tel" + i.ToString());
                        }
                    }
                    foreach (string item in listaBrojeva)
                    {
                        DataColumn dc = new DataColumn(item, typeof(string));
                        dc.DefaultValue = "";
                        dt.Columns.Add(dc);
                    }
                }
                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    count = 0;
                    for (int j = nr; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i]["idContPers"].ToString() != "")
                        {
                            string idP = dt.Rows[i]["idContPers"].ToString();
                            //   string quest = dt.Columns[j].Caption;

                            List<PersonTelModel> result = new List<PersonTelModel>();
                            result = abus.GetCpTel(Convert.ToInt32(idP));
                            if (result != null)
                            {
                                if (result.Count > 0)
                                {
                                    if (count < result.Count)
                                    {
                                        dt.Rows[i][j] = result[j - nr].numberTel;
                                        count++;
                                    }
                                }
                            }
                            else
                            {
                                dt.Rows[i][j] = "";

                            }
                        }

                    }
                }
                // broj kolona posle dodavanja brojeva telefona
                int nr1 = dt.Columns.Count;

                // dodavanje emaila
                List<PersonEmailModel> listaEmail = new List<PersonEmailModel>();
                // broj koliko neka osoba ima brojeva defaultnih email-ova
                int brojE = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int idP = Convert.ToInt32(dt.Rows[i]["idContPers"]);

                    listaEmail = abus.GetCpEmail(idP);
                    // koliko emailova ima posmatrana osoba
                    if (listaEmail != null)
                    {
                        if (listaEmail.Count > brojE)
                            brojE = listaEmail.Count;

                        listaEmail = null;
                    }
                }
                List<string> listaEm = new List<string>();
                if (brojE != 0)
                {
                    for (int i = 0; i < brojE; i++)
                    {
                        if (i == 0)
                        {
                            listaEm.Add("Email");
                        }
                        else
                        {

                            listaEm.Add("Email" + i.ToString());
                        }
                    }


                    foreach (string item in listaEm)
                    {
                        DataColumn dc = new DataColumn(item, typeof(string));
                        dc.DefaultValue = "";
                        dt.Columns.Add(dc);
                    }
                }
                int countE = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    countE = 0;
                    for (int j = nr1; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i]["idContPers"].ToString() != "")
                        {
                            string idP = dt.Rows[i]["idContPers"].ToString();
                            //   string quest = dt.Columns[j].Caption;

                            List<PersonEmailModel> resultE = new List<PersonEmailModel>();
                            resultE = abus.GetCpEmail(Convert.ToInt32(idP));
                            if (resultE != null)
                            {
                                if (resultE.Count > 0)
                                {
                                    if (countE < resultE.Count)
                                    {
                                        dt.Rows[i][j] = resultE[j - nr1].email;
                                        countE++;
                                    }
                                }
                            }
                            else
                            {
                                dt.Rows[i][j] = "";

                            }
                        }

                    }
                }

                int nr4 = dt.Columns.Count;

                if (dt.Rows.Count > 0)
                {
                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    dt.Columns.Add(dc2);

                    DataColumn dc3 = new DataColumn("DateTo", typeof(string));
                    dc3.Caption = "Date to";
                    dc3.ColumnName = "DateTo";
                    dt.Columns.Add(dc3);

                    dt.Rows[0][nr4] = model.dtFromArrangement.ToString("MM/dd/yyyy");
                    dt.Rows[0][nr4 + 1] = model.dtToArrangement.ToString("MM/dd/yyyy");
                }


            }

            return dt;
        }
      
        ///////////////////////////////////////////////////////


        private DataTable TeamList(int idArrangement)
        {
            DataTable dt = new DataTable();
            ArrangementBookDAO ab = new ArrangementBookDAO();
            ArrangementBookBUS abus = new ArrangementBookBUS();

            List<VolontaryFunctionModel> arrVoluntary1;
            int nr = 0;
            dt = ab.TeamList(idArrangement);

            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["nameArrangement"] != null)
                        dt.Columns["nameArrangement"].Caption = "Arrangement";
                    if (dt.Columns["dtFromArrangement"] != null)
                        dt.Columns["dtFromArrangement"].Caption = "Date from";
                    if (dt.Columns["dtToArrangement"] != null)
                        dt.Columns["dtToArrangement"].Caption = "Date to";
                    if (dt.Columns["nameTitle"] != null)
                        dt.Columns["nameTitle"].Caption = "Title";
                    if (dt.Columns["name"] != null)
                        dt.Columns["name"].Caption = "Name";
                    if (dt.Columns["idContPers"] != null)
                        dt.Columns["idContPers"].Caption = "ID persons";

                    nr = dt.Columns.Count; // broj kolona pre prvog prosirenja tabele
                }


                arrVoluntary1 = abus.GetVoluntaryFunctionAll(idArrangement);

                if (arrVoluntary1 != null)
                {
                    foreach (VolontaryFunctionModel item in arrVoluntary1)
                    {
                        DataColumn dc = new DataColumn(item.txtQuest, typeof(string));
                        dc.DefaultValue = "";
                        dc.Caption = item.txtQuest;
                        dt.Columns.Add(dc);
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = nr; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i]["idContPers"].ToString() != "")
                            {
                                string idP = dt.Rows[i]["idContPers"].ToString();
                                string txtAns = dt.Columns[j].Caption;


                                List<VolontaryFunctionModel> result = new List<VolontaryFunctionModel>();
                                result = abus.GetVoluntaryFunction(Convert.ToInt32(idP), txtAns);
                                if (result != null)
                                {
                                    if (result.Count > 0)
                                    {
                                        dt.Rows[i][j] = "x";
                                    }

                                }
                                else
                                {
                                    dt.Rows[i][j] = "";

                                }
                            }
                        }
                    }
                }

            }
            return dt;

        }

        private DataTable DivisionList(int idArrangement)
        {
            DataTable dt = new DataTable();
            ArrangementBookDAO ab = new ArrangementBookDAO();
            ArrangementBookBUS abus = new ArrangementBookBUS();
            List<VolontaryTripModel> arrVoluntaryTrip;
            int nr = 0;

            dt = ab.DivisionList(idArrangement);
            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["nameArrangement"] != null)
                        dt.Columns["nameArrangement"].Caption = "Arrangement";
                    if (dt.Columns["dtFromArrangement"] != null)
                        dt.Columns["dtFromArrangement"].Caption = "Date from";
                    if (dt.Columns["dtToArrangement"] != null)
                        dt.Columns["dtToArrangement"].Caption = "Date to";
                    if (dt.Columns["nameTitle"] != null)
                        dt.Columns["nameTitle"].Caption = "Title";
                    if (dt.Columns["name"] != null)
                        dt.Columns["name"].Caption = "Name";
                }


                arrVoluntaryTrip = abus.GetVoluntaryTrip(idArrangement);


                if (arrVoluntaryTrip.Count > 0)
                {
                    //foreach (VolontaryTripModel item in arrVoluntaryTrip)
                    //{
                    //    DataColumn dc = new DataColumn(item.txtQuest, typeof(string));
                    //    dc.DefaultValue = "x";
                    //    dt.Columns.Add(dc);
                    //}

                    //Brise space u poljima

                    int n = 1;
                    foreach (VolontaryTripModel item in arrVoluntaryTrip)
                    {
                        DataColumn dc = new DataColumn("Column_" + n, typeof(string));
                        dc.DefaultValue = "x";
                        dc.Caption = item.txtQuest;
                        dt.Columns.Add(dc);
                        n++;
                    }

                }
            }

            return dt;

        }

        private DataTable BoardingPoint(int idArrangement)
        {
            DataTable dt = new DataTable();
            ArrangementBookDAO ab = new ArrangementBookDAO();
            ArrangementBookBUS abus = new ArrangementBookBUS();

            dt = ab.BoardingPoint(idArrangement);
            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {

                    if (dt.Columns["nameArrangement"] != null)
                        dt.Columns["nameArrangement"].Caption = "Arrangement";
                    if (dt.Columns["dtFromArrangement"] != null)
                        dt.Columns["dtFromArrangement"].Caption = "Date from";
                    if (dt.Columns["dtToArrangement"] != null)
                        dt.Columns["dtToArrangement"].Caption = "Date to";
                    if (dt.Columns["nameBoardingPoint"] != null)
                        dt.Columns["nameBoardingPoint"].Caption = "Boarding point";
                    if (dt.Columns["nameTitle"] != null)
                        dt.Columns["nameTitle"].Caption = "Title";
                    if (dt.Columns["name"] != null)
                        dt.Columns["name"].Caption = "Name";
                    if (dt.Columns["addressCity"] != null)
                        dt.Columns["addressCity"].Caption = "Address city";
                    if (dt.Columns["departure"] != null)
                        dt.Columns["departure"].Caption = "Departure";
                    if (dt.Columns["arrivel"] != null)
                        dt.Columns["arrivel"].Caption = "Arrivel";
                }
            }




            return dt;

        }

        //Novo CencelledReportPerson
        private DataTable CanceledOption()
        {
            model.dtFromArrangement = Convert.ToDateTime(dtFrom.Value);
            model.dtToArrangement = Convert.ToDateTime(dtTo.Value);
            DataTable dt = new DataTable();
            ArrangementBookDAO ab = new ArrangementBookDAO();
            ArrangementBookBUS abus = new ArrangementBookBUS();
            int nr = 0;
            dt = ab.CanceledOption( model.dtFromArrangement, model.dtToArrangement);

            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["nameArrangement"] != null)
                        dt.Columns["nameArrangement"].Caption = "Arrangement";
                    if (dt.Columns["dtFromArrangement"] != null)
                        dt.Columns["dtFromArrangement"].Caption = "Date from";
                    if (dt.Columns["dtToArrangement"] != null)
                        dt.Columns["dtToArrangement"].Caption = "Date to";
                    if (dt.Columns["nameTitle"] != null)
                        dt.Columns["nameTitle"].Caption = "Title";
                    if (dt.Columns["name"] != null)
                        dt.Columns["name"].Caption = "Name";
                    if (dt.Columns["idContPers"] != null)
                        dt.Columns["idContPers"].Caption = "ID person";                 

                    nr = dt.Columns.Count; // broj kolona pre prvog prosirenja tabele
                }
                List<PersonTelModel> listaTel = new List<PersonTelModel>();

                // broj koliko neka osoba ima brojeva telefona
                int brojT = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int idP = Convert.ToInt32(dt.Rows[i]["idContPers"]);

                    listaTel = abus.GetCpTel(idP);
                    // koliko brojeva telefona ima posmatrana osoba
                    if (listaTel != null)
                    {
                        if (listaTel.Count > brojT)
                            brojT = listaTel.Count;

                        listaTel = null;
                    }
                }
                List<string> listaBrojeva = new List<string>();
                if (brojT != 0)
                {
                    for (int i = 0; i < brojT; i++)
                    {
                        if (i == 0)
                        {
                            listaBrojeva.Add("Tel");
                        }
                        else
                        {

                            listaBrojeva.Add("Tel" + i.ToString());
                        }
                    }
                    foreach (string item in listaBrojeva)
                    {
                        DataColumn dc = new DataColumn(item, typeof(string));
                        dc.DefaultValue = "";
                        dt.Columns.Add(dc);
                    }

                                     


                }
                int count = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    count = 0;
                    for (int j = nr; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i]["idContPers"].ToString() != "")
                        {
                            string idP = dt.Rows[i]["idContPers"].ToString();
                            //   string quest = dt.Columns[j].Caption;

                            List<PersonTelModel> result = new List<PersonTelModel>();
                            result = abus.GetCpTel(Convert.ToInt32(idP));
                            if (result != null)
                            {
                                if (result.Count > 0)
                                {
                                    if (count < result.Count)
                                    {
                                        dt.Rows[i][j] = result[j - nr].numberTel;
                                        count++;
                                    }
                                }
                            }
                            else
                            {
                                dt.Rows[i][j] = "";

                            }
                        }

                    }
                }
                // broj kolona posle dodavanja brojeva telefona
                int nr1 = dt.Columns.Count;

                // dodavanje emaila
                List<PersonEmailModel> listaEmail = new List<PersonEmailModel>();
                // broj koliko neka osoba ima brojeva defaultnih email-ova
                int brojE = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int idP = Convert.ToInt32(dt.Rows[i]["idContPers"]);

                    listaEmail = abus.GetCpEmail(idP);
                    // koliko emailova ima posmatrana osoba
                    if (listaEmail != null)
                    {
                        if (listaEmail.Count > brojE)
                            brojE = listaEmail.Count;

                        listaEmail = null;
                    }
                }
                List<string> listaEm = new List<string>();
                if (brojE != 0)
                {
                    for (int i = 0; i < brojE; i++)
                    {
                        if (i == 0)
                        {
                            listaEm.Add("Email");
                        }
                        else
                        {

                            listaEm.Add("Email" + i.ToString());
                        }
                    }


                    foreach (string item in listaEm)
                    {
                        DataColumn dc = new DataColumn(item, typeof(string));
                        dc.DefaultValue = "";
                        dt.Columns.Add(dc);
                    }
                   

                   
                }
                int countE = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    countE = 0;
                    for (int j = nr1; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows[i]["idContPers"].ToString() != "")
                        {
                            string idP = dt.Rows[i]["idContPers"].ToString();
                            //   string quest = dt.Columns[j].Caption;

                            List<PersonEmailModel> resultE = new List<PersonEmailModel>();
                            resultE = abus.GetCpEmail(Convert.ToInt32(idP));
                            if (resultE != null)
                            {
                                if (resultE.Count > 0)
                                {
                                    if (countE < resultE.Count)
                                    {
                                        dt.Rows[i][j] = resultE[j - nr1].email;
                                        countE++;
                                    }
                                }
                            }
                            else
                            {
                                dt.Rows[i][j] = "";

                            }
                        }

                    }
                }

                int nr4 = dt.Columns.Count;

                if (dt.Rows.Count > 0)
                {
                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    dt.Columns.Add(dc2);

                    DataColumn dc3 = new DataColumn("DateTo", typeof(string));
                    dc3.Caption = "Date to";
                    dc3.ColumnName = "DateTo";
                    dt.Columns.Add(dc3);

                    dt.Rows[0][nr4] = model.dtFromArrangement.ToString("MM/dd/yyyy");
                    dt.Rows[0][nr4 + 1] = model.dtToArrangement.ToString("MM/dd/yyyy");

                }
            }

            return dt;
        }

        private void btnarrangement_Click(object sender, EventArgs e)
        {
            ArrangementBookBUS arrBUS = new ArrangementBookBUS();
            List<IModel> gm3 = new List<IModel>();

            gm3 = arrBUS.GetAllArangementCode();


            var dlgSave = new GridLookupForm(gm3, "Arrangement");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
               
                ArrangementCodeModel genm3 = new ArrangementCodeModel();
                genm3 = (ArrangementCodeModel)dlgSave.selectedRow;
                
                txtCodeArrangement.Text = genm3.codeArrangement;
                idArrangement = genm3.idArrangement;
          
                //if (User != null)
                //{
                //    User.idEmployee = genm3.idEmployee;
                //    User.nameEmployee = genm3.firstNameEmployee + " " + genm3.lastNameEmployee;
                //}
                //idEmpl = genm3.idEmployee;
                //txtUserFullName.Text = genm3.firstNameEmployee + " " + genm3.lastNameEmployee;
                //txtidEmployee.Text = genm3.idEmployee.ToString();
            }

        }
        //

    
    }

    }

