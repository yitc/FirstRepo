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
using Telerik.WinControls.UI;
using System.Linq;

namespace GUI
{
    public partial class frmPurchaseReport : Telerik.WinControls.UI.RadForm
    {
        PurchaseReportModel model= new PurchaseReportModel();
        List<ArrTypeModel> list = new List<ArrTypeModel>();
        List<LabelModel> listLablel = new List<LabelModel>();

       // list4
        int sumNrTr;
        int sumMaxNrTr;
        
        int idArrType=0;
        int idLabel = 0;
        //list3
        string mounth = "";
        string mounthNr = "";
        int sumMounth = 0;
        int sumAll = 0;
        decimal sumOccupancy = 0;
        decimal sumAllOccupancy = 0;
        int sumMounthNr = 0;
        int sumAllNr = 0;
        string nazivmeseca;
        int noRow = 0;
        DataRow dr;



        public frmPurchaseReport()
        {
            InitializeComponent();
        }

        private void frmPurchaseReport_Load(object sender, EventArgs e)
        {
            dtFrom.Value = DateTime.Now;
            dtTo.Value = DateTime.Now;

            RadCheckBox rchk1;
            int Y1 = 0;
            ArrTypeBUS accBUS = new ArrTypeBUS();
            List<ArrTypeModel> am = new List<ArrTypeModel>();
            panelOption.Visible = false;

            am = accBUS.GetArrTypes();


            List<string> type = new List<string>();
            string s = "";

            for (int i = 0; i < am.Count; i++)
            {
                s = am[i].nameArrType.ToString();
                type.Add(s);
            }
            for (int i = 0; i < am.Count; i++)
            {
                rchk1 = new RadCheckBox();
                rchk1.Font = new Font("Verdana", 9);
                // rchk.ThemeName = radPageArrange.ThemeName;
                rchk1.Name = "chk" + am[i].idArrType;
                rchk1.Text = am[i].nameArrType.ToString();
                rchk1.Location = new Point(0, Y1);
                rchk1.AutoSize = true;
                Y1 = Y1 + 3 + rchk1.Height;
                panel1.Controls.Add(rchk1);
            }

            //  

            RadCheckBox rchk;
            int Y = 0;
            for (int i = 0; i < Login._arrLabels.Count; i++)
            {
                rchk = new RadCheckBox();
                rchk.Font = new Font("Verdana", 9);
                // rchk.ThemeName = radPageArrange.ThemeName;
                rchk.Name = "chk" + Login._arrLabels[i].idLabel.ToString();
                rchk.Text = Login._arrLabels[i].nameLabel;
                rchk.Location = new Point(0, Y);
                rchk.AutoSize = true;
                Y = Y + 3 + rchk.Height;
                panelLabels.Controls.Add(rchk);
            }

            ArrangementBookBUS bus = new ArrangementBookBUS();
            list = bus.AllArrType();
            dlArangementType.DataSource = list;
            dlArangementType.DisplayMember = "nameArrType";
            dlArangementType.ValueMember = "idArrType";


            for (int i = 0; i < Login._arrLabels.Count; i++)
            {

                dlLabel.Items.Add(Login._arrLabels[i].nameLabel.ToString());
                dlLabel.Items[i].Value = Login._arrLabels[i].idLabel.ToString();
            }

            if (dlLabel.Items.Count > 0)
            {
                dlLabel.SelectedIndex = 0;
            }
            if (ddlProvision.Items.Count > 0)
            {
                ddlProvision.SelectedIndex = 0;
            }

            btnNrBookings.Click += btnNrBookings_CheckStateChanged;
            btnNrOptions.Click += btnNrOptions_CheckStateChanged;
            btnNrPerEmployee.Click += btnNrPerEmployee_CheckStateChanged;
            btnCanceled.Click += btnCanceled_CheckStateChanged;
            btnDepartureList1.Click += btnDepartureList1_CheckStateChanged;
            btnDepartureList2.Click += btnDepartureList2_CheckStateChanged;
            btnDepartureList3.Click += btnDepartureList3_CheckStateChanged;
            btnDepartureList4.Click += btnDepartureList4_CheckStateChanged;

            this.Icon = Login.iconForm;
            //string formName = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();
            //formName = formName + " " + model.nameArrangement;
            string name = this.Name.Substring(3);

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(name) != null)
                    name = resxSet.GetString(name);
            }
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + name;

            ArrangementBookBUS ub = new ArrangementBookBUS();
            List<EmployeeModel> employeemodel = ub.AllEmployee(Convert.ToDateTime(dtFrom.Value.ToString()), Convert.ToDateTime(dtTo.Value.ToString()));
            dlEmployee.DataSource = employeemodel;
            dlEmployee.DisplayMember = "firstNameEmployee";
            dlEmployee.ValueMember = "idEmployee";

            setTranslation();
        }
        # region event
        private void btnDepartureList4_CheckStateChanged(object sender, EventArgs e)
        {
            dlEmployee.Visible = false;
            lblEmployee.Visible = false;
            panelLabels.Visible = false;
            panel1.Visible = false;
            lblArrangementType.Visible = true;
            dlArangementType.Visible = true;
            panelOption.Visible = true;
            dlLabel.Visible = true;
            lblLabel.Visible = true;
            lblBWT.Visible = true;
            ddlProvision.Visible = true;

        }

        private void btnDepartureList3_CheckStateChanged(object sender, EventArgs e)
        {
            dlEmployee.Visible = false;
            lblEmployee.Visible = false;
            panelLabels.Visible = false;
            panel1.Visible = false;
            lblArrangementType.Visible = true;
            dlArangementType.Visible = true;
            panelOption.Visible = true;
            dlLabel.Visible = true;
            lblLabel.Visible = true;
            lblBWT.Visible = false;
            ddlProvision.Visible = false;


        }

        private void btnDepartureList2_CheckStateChanged(object sender, EventArgs e)
        {
            dlEmployee.Visible = false;
            lblEmployee.Visible = false;
            panelLabels.Visible = false;
            panel1.Visible = false;
            lblArrangementType.Visible = false;
            dlArangementType.Visible = false;
            panelOption.Visible = true;
            dlLabel.Visible = false;
            lblLabel.Visible = false;
            lblBWT.Visible = false;
            ddlProvision.Visible = false;


        }

        private void btnDepartureList1_CheckStateChanged(object sender, EventArgs e)
        {
            dlEmployee.Visible = false;
            lblEmployee.Visible = false;
            panelLabels.Visible = false;
            panel1.Visible = false;
            lblArrangementType.Visible = false;
            dlArangementType.Visible = false;
            panelOption.Visible = true;
            dlLabel.Visible = false;
            lblLabel.Visible = false;
            lblBWT.Visible = false;
            ddlProvision.Visible = false;


        }

        private void btnCanceled_CheckStateChanged(object sender, EventArgs e)
        {
            dlEmployee.Visible = false;
            lblEmployee.Visible = false;
            panelLabels.Visible = true;
            panel1.Visible = true;
            lblArrangementType.Visible = false;
            dlArangementType.Visible = false;
            panelOption.Visible = false;
            dlLabel.Visible = false;
            lblLabel.Visible = false;
            lblBWT.Visible = false;
            ddlProvision.Visible = false;


        }

        private void btnNrPerEmployee_CheckStateChanged(object sender, EventArgs e)
        {
            dlEmployee.Visible = true;
            lblEmployee.Visible = true;
            panelLabels.Visible = true;
            panel1.Visible = true;
            lblArrangementType.Visible = false;
            dlArangementType.Visible = false;
            panelOption.Visible = false;
            dlLabel.Visible = false;
            lblLabel.Visible = false;
            lblBWT.Visible = false;
            ddlProvision.Visible = false;

        }

        private void btnNrOptions_CheckStateChanged(object sender, EventArgs e)
        {
            dlEmployee.Visible = false;
            lblEmployee.Visible = false;
            panelLabels.Visible = true;
            panel1.Visible = true;
            lblArrangementType.Visible = false;
            dlArangementType.Visible = false;
            panelOption.Visible = false;
            dlLabel.Visible = false;
            lblLabel.Visible = false;
            lblBWT.Visible = false;
            ddlProvision.Visible = false;

        }

        private void btnNrBookings_CheckStateChanged(object sender, EventArgs e)
        {
            dlEmployee.Visible = false;
            lblEmployee.Visible = false;
            panelLabels.Visible = true;
            panel1.Visible = true;
            lblArrangementType.Visible = false;
            dlArangementType.Visible = false;
            panelOption.Visible = false;
            dlLabel.Visible = false;
            lblLabel.Visible = false;
            lblBWT.Visible = false;
            ddlProvision.Visible = false;
        }

        # endregion
        private void btnOK_Click(object sender, EventArgs e)
        {
         //   string type = ddlType.SelectedItem.ToString();
           
            // =================== ovo je bio original ==================
            //DataTable dt10 = new DataTable();
            //if (btnNrBookings.IsChecked == true)
            //{
            //    NrBookings();

            //    dt10 = NrBookings();
           
              //za test
               //rgvResult.DataSource = NrBookings();
            //}
            //==============================================================
            DataTable dt9 = new DataTable();
            if (btnNrOptions.IsChecked == true)
            {
                NrOptions();
                // za test
                dt9 = NrOptions();
                // rgvResult.DataSource = NrOptions();
            }
            DataTable dt10 = new DataTable();
            if (btnNrBookings.IsChecked == true)
            {
                NrBookingsCountBooked();

                dt10 = NrBookingsCountBooked();
                //za test
                //rgvResult.DataSource = NrBookings();
            }

            DataTable dt11 = new DataTable();
            if (btnNrPerEmployee.IsChecked == true)
            {
                NrPerEmployee();
                //za test

                dt11 = NrPerEmployee();
              //rgvResult.DataSource = NrPerEmployee();
            }
            //NOVO

            DataTable dt1 = new DataTable();
            DataTable dt63 = new DataTable();
            if (btnDepartureList1.IsChecked == true)
            {
                               
                if(btnExclusive.IsChecked==true)
                {
                    
                    DepartureList1Ex();
                    //rgvResult.DataSource = DepartureList1Ex();
                    dt63 = DepartureList1Ex();
                }
                if (btnInclusive.IsChecked == true)
                { 
                DepartureList1();
                dt1 = DepartureList1();
                //rgvResult.DataSource = DepartureList1();
                }
            }
            DataTable dt28 = new DataTable();
            DataTable dt29 = new DataTable();
            if (btnDepartureList2.IsChecked == true)
            {
                if (btnExclusive.IsChecked == true)
                {
                    DepartureList2Ex();
                    dt28 = DepartureList2Ex();
                    //rgvResult.DataSource = DepartureList2Ex();
                }
                else
                {
                    DepartureList2();
                    dt29 = DepartureList2();
                    //rgvResult.DataSource = DepartureList2();
                }
            }
            DataTable dt70 = new DataTable();
            DataTable dt71 = new DataTable();
            if (btnDepartureList3.IsChecked == true)
            {
                // select type arrangement
                string typeArrangement = dlArangementType.SelectedItem.ToString();
                string arrangement = dlArangementType.SelectedValue.ToString();
                if (arrangement != null)
                    idArrType = Convert.ToInt32(arrangement);

                //selec label
                string labelName = dlLabel.SelectedItem.ToString();

                int index = dlLabel.SelectedIndex;
                string lebidLabelelId = dlLabel.Items[index].Value.ToString();
                if (labelName != null)
                {
                    idLabel = Convert.ToInt32(lebidLabelelId);
                }
                if (btnExclusive.IsChecked == true)
                {
                    DepartureList3Ex(idArrType, idLabel);
                    dt71 = DepartureList3Ex(idArrType, idLabel);
                    //rgvResult.DataSource = DepartureList3Ex(idArrType, idLabel);

                }
                if (btnInclusive.IsChecked == true)
                {
                    DepartureList3In(idArrType, idLabel);
                    dt70=DepartureList3In(idArrType, idLabel);
                    //rgvResult.DataSource = DepartureList3In(idArrType,idLabel);
                }
            }
            DataTable dt72 = new DataTable();
            DataTable dt73 = new DataTable();
            if (btnDepartureList4.IsChecked == true)
            {
                string typeArrangement = dlArangementType.SelectedItem.ToString();
                string arrangement = dlArangementType.SelectedValue.ToString();
                if (arrangement != null)
                    idArrType = Convert.ToInt32(arrangement);

                // select provision:
                string provision = ddlProvision.SelectedItem.ToString();

                
                //selec label
                string labelName = dlLabel.SelectedItem.ToString();
            
                int index = dlLabel.SelectedIndex;
                string lebidLabelelId = dlLabel.Items[index].Value.ToString();
                if (labelName != null)

                    idLabel = Convert.ToInt32(lebidLabelelId);

                if (btnExclusive.IsChecked == true)
                {
                    DepartureList4Ex(idArrType, provision, idLabel);
                    dt73 = DepartureList4Ex(idArrType, provision, idLabel);
                    //rgvResult.DataSource = DepartureList4Ex(idArrType, provision, idLabel);

                }
                if (btnInclusive.IsChecked == true)
                {
                    DepartureList4xIn(idArrType, provision,idLabel);
                    dt72 = DepartureList4xIn(idArrType, provision, idLabel);
                    //rgvResult.DataSource = DepartureList4xIn(idArrType, provision, idLabel);
                }
       
            }
            //Novo Canceled report 
            DataTable dt19 = new DataTable();
            if (btnCanceled.IsChecked == true)
            {
                Canceled();

                dt19 = Canceled();
                //rgvResult.DataSource = Canceled();
            }
            //Konstruktori 
            if (btnNrOptions.IsChecked == true)
            {
                frmReportNumberOptions frm9 = new frmReportNumberOptions(dt9);
                frm9.Show();
            }
            else if(btnNrBookings.IsChecked == true)
            {
                frmReportNumberBookings frm10 = new frmReportNumberBookings(dt10);
                frm10.Show();
            }
            else if(btnNrPerEmployee.IsChecked == true)
            {
                frmReportNrOfPerEmployee frm11 = new frmReportNrOfPerEmployee(dt11);
                frm11.Show();
            }
            else if(btnDepartureList1.IsChecked == true)
            {
                if (btnInclusive.IsChecked == true)
                {
                    frmReportDepartureList frm1 = new frmReportDepartureList(dt1);
                    frm1.Show();
                }
                if(btnExclusive.IsChecked==true)
                {
                    frmReportDepartureList1Ex frm63 = new frmReportDepartureList1Ex(dt63);
                    frm63.Show();
                }
            }
            else if(btnDepartureList2.IsChecked == true)
            {
                if(btnExclusive.IsChecked == true)
                {
                    frmReportDepartureList2Ex frm28 = new frmReportDepartureList2Ex(dt28);
                    frm28.Show();
                }
                else
                {
                    frmReportDepartureList2Inclusive frm29 = new frmReportDepartureList2Inclusive(dt29);
                    frm29.Show();
                }
            }
            //Novi
            else if(btnDepartureList3.IsChecked == true)
            {
                if (btnInclusive.IsChecked == true)
                {
                    string label = "";
                    if (dlLabel.SelectedItem!=null)
                    label = dlLabel.SelectedItem.ToString();

                    string type = "";
                    if (dlArangementType.SelectedItem != null)
                        type = dlArangementType.SelectedItem.ToString();
                    frmReportDepartureList3Inclusive frm70 = new frmReportDepartureList3Inclusive(dt70, label, type);
                    frm70.Show();
                }
                else
                {
                    string label = "";
                    if (dlLabel.SelectedItem != null)
                        label = dlLabel.SelectedItem.ToString();

                    string type = "";
                    if (dlArangementType.SelectedItem != null)
                        type = dlArangementType.SelectedItem.ToString();

                    frmReportDepartureList3Exclusive frm71 = new frmReportDepartureList3Exclusive(dt71,label,type);
                    frm71.Show();
                }
            }
            else if(btnDepartureList4.IsChecked == true)
            {
                if(btnInclusive.IsChecked == true)
                {
                    string label = "";
                    if (dlLabel.SelectedItem != null)
                        label = dlLabel.SelectedItem.ToString();

                    string type = "";
                    if (dlArangementType.SelectedItem != null)
                        type = dlArangementType.SelectedItem.ToString();

                    string btw = "";
                    if (ddlProvision.SelectedItem != null)
                        btw = ddlProvision.SelectedItem.ToString();
                    frmReportDepartureList4Inclusive frm72 = new frmReportDepartureList4Inclusive(dt72,label,type,btw);
                    frm72.Show();
                }
                if (btnExclusive.IsChecked == true)
                {
                    string label = "";
                    if (dlLabel.SelectedItem != null)
                        label = dlLabel.SelectedItem.ToString();

                    string type = "";
                    if (dlArangementType.SelectedItem != null)
                        type = dlArangementType.SelectedItem.ToString();

                    string btw = "";
                    if (ddlProvision.SelectedItem != null)
                        btw = ddlProvision.SelectedItem.ToString();
                    frmReportDepartureList4Exclusive frm73 = new frmReportDepartureList4Exclusive(dt73, label, type, btw);
                    frm73.Show();
                }
            }
            else if (btnCanceled.IsChecked == true)
            {
                frmReportCencelledTrips frm19 = new frmReportCencelledTrips(dt19);
                frm19.Show();
            }
           
        }



        private DataTable NrPerEmployee()
        {
            ArrangementBookBUS bus = new ArrangementBookBUS();
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);
            DataTable data = new DataTable();
            int nrOfColumns = 0;
            data = NrBookingsEmployee();

            if (data != null)
            {
                //nrOfColumns = data.Columns.Count;
                //DataColumn dc = new DataColumn("Employee name", typeof(string));
                //data.Columns.Add(dc);


                int n = 1;
                // Za ubijanje space u poljima
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    if (data.Columns[i].ColumnName.Contains(" "))
                    {
                        data.Columns[i].Caption = data.Columns[i].ColumnName;
                        data.Columns[i].ColumnName = "Column_" + n.ToString();
                    }
                    n++;
                }
                // end
                //for (int i = 0; i < data.Rows.Count; i++)
                //{
                //    for (int j = nrOfColumns; j < data.Columns.Count; j++)
                //    {
                //        if (data.Rows[i]["idArrangement"].ToString() != "")
                //        {
                //            int idArr = Convert.ToInt32(data.Rows[i]["idArrangement"].ToString());


                //            List<EmployeeModel> result = new List<EmployeeModel>();
                //            result = bus.GetEmployee(idArr);
                //            if (result != null)
                //            {
                //                if (result.Count > 0)
                //                {
                //                    data.Rows[i][j] = result[0].firstNameEmployee;
                //                }
                //            }
                //        }

                //    }
                //}

            }
            return data;
        }
        private DataTable NrBookingsEmployee()
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);

            List<int> nameLabel = new List<int>();

            foreach (Control c in panelLabels.Controls)
            {
                RadCheckBox rchk = (RadCheckBox)c;
                if (rchk.Checked == true)
                    nameLabel.Add(Convert.ToInt32(rchk.Name.Replace("chk", "")));
            }


            List<int> arrType = new List<int>();

            foreach (Control c in panel1.Controls)
            {
                RadCheckBox rchk = (RadCheckBox)c;
                if (rchk.Checked == true)
                    arrType.Add(Convert.ToInt32(rchk.Name.Replace("chk", "")));
            }
            ArrangementBookBUS pbus = new ArrangementBookBUS();
            DataTable data = new DataTable();
            int idEmployee = -1;
            if (dlEmployee.SelectedItem != null)
                idEmployee = Convert.ToInt32(dlEmployee.SelectedValue.ToString());
            data = new ArrangementBookDAO().NrOfOptionsEmployee(arrType, nameLabel, model.dateFrom, model.dateTo, idEmployee);

            if (data != null)
            {
                if (data.Rows.Count > 0)
                {
                    if (data.Columns["nameLabel"] != null)
                        data.Columns["nameLabel"].Caption = "Label";
                    if (data.Columns["codeArrangement"] != null)
                        data.Columns["codeArrangement"].Caption = "Arrangement";
                    if (data.Columns["idArrangement"] != null)
                        data.Columns["idArrangement"].Caption = " ID arrangement";
                    if (data.Columns["nameArrType"] != null)
                        data.Columns["nameArrType"].Caption = "Type";
                    if (data.Columns["dateFrom"] != null)
                        data.Columns["dateFrom"].Caption = "Date from";
                    if (data.Columns["dateTo"] != null)
                        data.Columns["dateTo"].Caption = "Date to";
                    if (data.Columns["nrTraveler"] != null)
                        data.Columns["nrTraveler"].Caption = "Number of traveler";
                    if (data.Columns["bookedTravelers"] != null)
                        data.Columns["bookedTravelers"].Caption = "Booked travelers";
                    if (data.Columns["Employee"] != null)
                        data.Columns["Employee"].Caption = "Employee";

                    int nr1 = data.Columns.Count;


                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    data.Columns.Add(dc2);

                    DataColumn dc1 = new DataColumn("DateTo", typeof(string));
                    dc1.Caption = "Date to";
                    dc1.ColumnName = "DateTo";
                    data.Columns.Add(dc1);

                    data.Rows[0][nr1] = model.dateFrom.ToString("dd/MM/yyyy");
                    data.Rows[0][nr1 + 1] = model.dateTo.ToString("dd/MM/yyyy");

                }
            }
            return data;

            //rgvResult.DataSource = null;
            //rgvResult.DataSource = personModelList;

        }
        private DataTable NrOptions()
        {
            ArrangementBookBUS bus = new ArrangementBookBUS();
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);
            DataTable data = new DataTable();
            data = NrBookings();
            List<OptionsModel> statusi = new List<OptionsModel>();
            statusi = bus.NrStatus();
            int nrOfColumns = 0;
            if (data != null)
            {
                nrOfColumns = data.Columns.Count;
                List<string> nameStatusa = new List<string>();
                if (statusi != null)
                {
                    for (int i = 0; i < statusi.Count; i++)
                    {
                        nameStatusa.Add(statusi[i].nameOption.ToString());
                    }


                    foreach (OptionsModel item in statusi)
                    {
                        DataColumn dc = new DataColumn(("Column" + "_" + item.idOption), typeof(int));
                        dc.DefaultValue = false;
                        dc.Caption = item.nameOption;
                        data.Columns.Add(dc);

                    }

                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        for (int j = nrOfColumns; j < data.Columns.Count; j++)
                        {
                            if (data.Rows[i]["idArrangement"].ToString() != "")
                            {
                                int idArr = Convert.ToInt32(data.Rows[i]["idArrangement"].ToString());
                                string[] nn = data.Columns[j].ColumnName.ToString().Split('_');
                                int idStatusa = Convert.ToInt32(nn[nn.Length - 1]);

                                List<OptionsModel> result = new List<OptionsModel>();
                                result = bus.checkStatus(idArr, idStatusa);
                                if (result != null)
                                {
                                    if (result.Count > 0)
                                    {
                                        data.Rows[i][j] = result[0].idOption;
                                    }
                                }
                            }

                        }
                    }

                }
            }
            return data;
        }

        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(btnNrOptions.Text) != null)
                    btnNrOptions.Text = resxSet.GetString(btnNrOptions.Text);
                if (resxSet.GetString(btnNrBookings.Text) != null)
                    btnNrBookings.Text = resxSet.GetString(btnNrBookings.Text);
                if (resxSet.GetString(btnNrPerEmployee.Text) != null)
                    btnNrPerEmployee.Text = resxSet.GetString(btnNrPerEmployee.Text);
                if (resxSet.GetString(btnCanceled.Text) != null)
                    btnCanceled.Text = resxSet.GetString(btnCanceled.Text);

                if (resxSet.GetString(lblDtFrom.Text) != null)
                    lblDtFrom.Text = resxSet.GetString(lblDtFrom.Text);
                if (resxSet.GetString(lblDtTo.Text) != null)
                    lblDtTo.Text = resxSet.GetString(lblDtTo.Text);

                if (resxSet.GetString(btnDepartureList1.Text) != null)
                    btnDepartureList1.Text = resxSet.GetString(btnDepartureList1.Text);
                if (resxSet.GetString(btnDepartureList2.Text) != null)
                    btnDepartureList2.Text = resxSet.GetString(btnDepartureList2.Text);
                if (resxSet.GetString(btnDepartureList3.Text) != null)
                    btnDepartureList3.Text = resxSet.GetString(btnDepartureList3.Text);
                if (resxSet.GetString(btnDepartureList4.Text) != null)
                    btnDepartureList4.Text = resxSet.GetString(btnDepartureList4.Text);

                if (resxSet.GetString(lblArrangementType.Text) != null)
                    lblArrangementType.Text = resxSet.GetString(lblArrangementType.Text);
                if (resxSet.GetString(lblLabel.Text) != null)
                    lblLabel.Text = resxSet.GetString(lblLabel.Text);
                if (resxSet.GetString(lblBWT.Text) != null)
                    lblBWT.Text = resxSet.GetString(lblBWT.Text);
                if (resxSet.GetString(lblEmployee.Text) != null)
                    lblEmployee.Text = resxSet.GetString(lblEmployee.Text);
            }
        }

        private DataTable NrBookings()
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);

            List<int> nameLabel = new List<int>();

            foreach (Control c in panelLabels.Controls)
            {
                RadCheckBox rchk = (RadCheckBox)c;
                if (rchk.Checked == true)
                    nameLabel.Add(Convert.ToInt32(rchk.Name.Replace("chk", "")));
            }


            List<int> arrType = new List<int>();

            foreach (Control c in panel1.Controls)
            {
                RadCheckBox rchk = (RadCheckBox)c;
                if (rchk.Checked == true)
                    arrType.Add(Convert.ToInt32(rchk.Name.Replace("chk", "")));
            }
            ArrangementBookBUS pbus = new ArrangementBookBUS();
            DataTable data = new DataTable();
            data = new ArrangementBookDAO().NrOfOptions(arrType, nameLabel, model.dateFrom, model.dateTo);
                                      
            //data.Columns["nameLabel"].Caption = "Label";
            ////data.Columns["nameArrangement"].Caption = "Arrangement";
            //data.Columns["idArrangement"].Caption = " ID arrangement";
            //data.Columns["nameArrType"].Caption = "Type";           
            //data.Columns["dateFrom"].Caption = "Date from";
            //data.Columns["dateTo"].Caption = "Date to";
            //data.Columns["nrTraveler"].Caption = "Number of traveler";
            //data.Columns["codeArrangement"].Caption = "Code arrangement";

            if (data.Columns["nameLabel"] != null)
                data.Columns["nameLabel"].Caption = "Label";
            if (data.Columns["codeArrangement"] != null)
                data.Columns["codeArrangement"].Caption = "Arrangement";
            if (data.Columns["idArrangement"] != null)
                data.Columns["idArrangement"].Caption = " ID arrangement";
            if (data.Columns["nameArrType"] != null)
                data.Columns["nameArrType"].Caption = "Type";
            if (data.Columns["dateFrom"] != null)
                data.Columns["dateFrom"].Caption = "Date from";
            if (data.Columns["dateTo"] != null)
                data.Columns["dateTo"].Caption = "Date to";
            if (data.Columns["nrTraveler"] != null)
                data.Columns["nrTraveler"].Caption = "Number of traveler";

            if (data != null)
            {
                if (data.Rows.Count > 0)
                {
                    int nr1 = data.Columns.Count;


                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    data.Columns.Add(dc2);

                    DataColumn dc1 = new DataColumn("DateTo", typeof(string));
                    dc1.Caption = "Date to";
                    dc1.ColumnName = "DateTo";
                    data.Columns.Add(dc1);

                    data.Rows[0][nr1] = model.dateFrom.ToString("dd/MM/yyyy");
                    data.Rows[0][nr1 + 1] = model.dateTo.ToString("dd/MM/yyyy");

                }
            }
            return data;
          
            //rgvResult.DataSource = null;
            //rgvResult.DataSource = personModelList;

        }

        public DataTable DepartureList1()
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);

            ArrangementBookDAO abus = new ArrangementBookDAO();
            DataTable dt = new DataTable();
            dt = abus.DepartureList1(model.dateFrom, model.dateTo);
            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["Label"] != null)
                        dt.Columns["Label"].Caption = "Label";
                    if (dt.Columns["Type"] != null)
                        dt.Columns["Type"].Caption = "Type";
                    if (dt.Columns["TotalBooked"] != null)
                        dt.Columns["TotalBooked"].Caption = "TotalBooked";
                    if (dt.Columns["Maximum"] != null)
                        dt.Columns["Maximum"].Caption = "Maximum";
                    if (dt.Columns["Occupancy"] != null)
                        dt.Columns["Occupancy"].Caption = "Occupancy(%)";

                }
                if (dt.Rows.Count > 0)
                {
                    //DataRow dr = dt.NewRow();
                    //dr["Label"] = "Total";
                    //dt.Rows.Add(dr);


                    //sumNrTr = 0;
                    //sumMaxNrTr = 0;
                    //sumOccupancy = 0;
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    if (dt.Rows[i]["TotalBooked"].ToString() != "")
                    //    {
                    //        sumNrTr += Convert.ToInt32(dt.Rows[i]["TotalBooked"]);
                    //    }
                    //    if (dt.Rows[i]["Maximum"].ToString() != "")
                    //    {
                    //        sumMaxNrTr += Convert.ToInt32(dt.Rows[i]["Maximum"]);
                    //    }

                    //}
                    //dt.Rows[dt.Rows.Count - 1]["TotalBooked"] = sumNrTr;
                    //dt.Rows[dt.Rows.Count - 1]["Maximum"] = sumMaxNrTr;
                    //dt.Rows[dt.Rows.Count - 1]["Occupancy"] = Convert.ToDouble(sumNrTr * 100 / sumMaxNrTr);

                    int nr1 = dt.Columns.Count;


                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    dt.Columns.Add(dc2);

                    DataColumn dc1 = new DataColumn("DateTo", typeof(string));
                    dc1.Caption = "Date to";
                    dc1.ColumnName = "DateTo";
                    dt.Columns.Add(dc1);

                    dt.Rows[0][nr1] = model.dateFrom.ToString("dd/MM/yyyy");
                    dt.Rows[0][nr1 + 1] = model.dateTo.ToString("dd/MM/yyyy");

                }

            }
            return dt;


        }

        public DataTable DepartureList1Ex()
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);

            ArrangementBookDAO abus = new ArrangementBookDAO();
            DataTable dt = new DataTable();
            dt = abus.DepartureList1Ex(model.dateFrom, model.dateTo);
            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["Label"] != null)
                        dt.Columns["Label"].Caption = "Label";
                    if (dt.Columns["Type"] != null)
                        dt.Columns["Type"].Caption = "Type";
                    if (dt.Columns["TotalBooked"] != null)
                        dt.Columns["TotalBooked"].Caption = "TotalBooked";
                    if (dt.Columns["Maximum"] != null)
                        dt.Columns["Maximum"].Caption = "Maximum";
                    if (dt.Columns["Occupancy(%)"] != null)
                        dt.Columns["Occupancy(%)"].Caption = "Occupancy(%)";

                    int nr1 = dt.Columns.Count;


                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    dt.Columns.Add(dc2);

                    DataColumn dc1 = new DataColumn("DateTo", typeof(string));
                    dc1.Caption = "Date to";
                    dc1.ColumnName = "DateTo";
                    dt.Columns.Add(dc1);

                    if (dt.Rows.Count > 0)
                    {
                        dt.Rows[0][nr1] = model.dateFrom.ToString("dd/MM/yyyy");
                        dt.Rows[0][nr1 + 1] = model.dateTo.ToString("dd/MM/yyyy");
                    }

                }

            }
            return dt;

        }

        public DataTable DepartureList2Ex()
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);
            int nr = 0;
            DataTable dt = new DataTable();
            ArrangementBookDAO dao = new ArrangementBookDAO();
            dt = dao.DepartureList2Ex(model.dateFrom, model.dateTo);

            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["mounth"] != null)
                        dt.Columns["mounth"].Caption = "Mounth";
                    if (dt.Columns["dateFrom"] != null)
                        dt.Columns["dateFrom"].Caption = "Date from";
                    if (dt.Columns["dayarr"] != null)
                        dt.Columns["dayarr"].Caption = "Day";
                    if (dt.Columns["arrangement"] != null)
                        dt.Columns["arrangement"].Caption = "Code arrangement";

                    if (dt.Columns["nr"] != null)
                        dt.Columns["nr"].Caption = "Booked travelers";
                    if (dt.Columns["nrTraveler"] != null)
                        dt.Columns["nrTraveler"].Caption = "Max travelers";
                    nr = dt.Columns.Count;

                }
                // sum nrTravelers
                if (dt.Rows.Count > 0)
                {
                    DataColumn dc = new DataColumn("Sum", typeof(int));
                    dc.Caption = "Sum of max";
                    // dc.DefaultValue = "";
                    dt.Columns.Add(dc);
                    DataColumn dc1 = new DataColumn("SumNr", typeof(int));
                    dc1.Caption = "Sum of pax";
                    // dc.DefaultValue = "";
                    dt.Columns.Add(dc1);

                    mounth = dt.Rows[0]["mounth"].ToString();
                    DataRow dr = dt.NewRow();
                    dr[0] = "Grand total sum";
                    dt.Rows.Add(dr);


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string mesec = dt.Rows[i]["mounth"].ToString();
                        if (mesec != mounth)
                        {
                            mounth = dt.Rows[i]["mounth"].ToString();
                            dr = dt.NewRow();

                            dr["mounth"] = "Sum";
                            dt.Rows.InsertAt(dr, i);
                        }

                    }


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string mesec = dt.Rows[i]["mounth"].ToString();
                        if (mesec != "Sum")
                        {
                            if (dt.Rows[i]["nr"].ToString() != "")
                            {
                                sumMounth += Convert.ToInt32(dt.Rows[i]["nr"].ToString());
                                sumAll += Convert.ToInt32(dt.Rows[i]["nr"].ToString());
                            }
                            if (dt.Rows[i]["nrTraveler"].ToString() != "")
                            {
                                sumMounthNr += Convert.ToInt32(dt.Rows[i]["nrTraveler"].ToString());
                                sumAllNr += Convert.ToInt32(dt.Rows[i]["nrTraveler"].ToString());
                            }
                        }
                        else if (mesec == "Sum")
                        {
                            //ZA POZICIONIRANJE POLJA 
                            dt.Rows[i]["nr"] = sumMounth;
                            dt.Rows[i]["nrTraveler"] = sumMounthNr;
                            sumMounthNr = 0;
                            sumMounth = 0;

                        }


                        if (i == dt.Rows.Count - 1)
                        {
                            dt.Rows[i]["nr"] = sumAll;
                            dt.Rows[i]["nrTraveler"] = sumAllNr;
                            sumAll = 0;
                            sumAllNr = 0;

                        }
                        if (mesec == "Sum")
                        {
                            //Prevod
                            dt.Rows[i][0] = dt.Rows[i - 1][0];
                            string str = "Subtotal";
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {

                                if (resxSet.GetString(str) != null)
                                    str = resxSet.GetString(str); // provera da li postoji prevod

                            }
                            dt.Rows[i][2] = str;
                        }
                        if (mesec == "Grand total sum")
                        {
                            //Prevod
                            dt.Rows[i][0] = dt.Rows[i - 1][0];
                            string str = "Total";
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {

                                if (resxSet.GetString(str) != null)
                                    str = resxSet.GetString(str); // provera da li postoji prevod

                            }
                            dt.Rows[i][2] = str;
                        }
                    }
                    int nr1 = dt.Columns.Count;


                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    dt.Columns.Add(dc2);

                    DataColumn dc3 = new DataColumn("DateTo", typeof(string));
                    dc3.Caption = "Date to";
                    dc3.ColumnName = "DateTo";
                    dt.Columns.Add(dc3);

                    dt.Rows[0][nr1] = model.dateFrom.ToString("dd/MM/yyyy");
                    dt.Rows[0][nr1 + 1] = model.dateTo.ToString("dd/MM/yyyy");
                }

                dt.Columns["mounth"].SetOrdinal(0);
                dt.Columns["dateFrom"].SetOrdinal(1);
                dt.Columns["dayarr"].SetOrdinal(2);
                dt.Columns["arrangement"].SetOrdinal(3);
                dt.Columns["nr"].SetOrdinal(4);
                if (dt.Columns["SumNr"] != null)
                {
                    dt.Columns["SumNr"].SetOrdinal(5);
                    dt.Columns["nrTraveler"].SetOrdinal(6);
                    dt.Columns["Sum"].SetOrdinal(7);
                }




            }
            //

            return dt;

        }
        public DataTable DepartureList2()
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);
            int nr = 0;
            DataTable dt = new DataTable();
            ArrangementBookDAO dao = new ArrangementBookDAO();
            dt = dao.DepartureList2(model.dateFrom, model.dateTo);

            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["mounth"] != null)
                        dt.Columns["mounth"].Caption = "Mounth";
                    if (dt.Columns["dateFrom"] != null)
                        dt.Columns["dateFrom"].Caption = "Date from";
                    if (dt.Columns["dayarr"] != null)
                        dt.Columns["dayarr"].Caption = "Day";
                    //if (dt.Columns["Label"] != null)
                    //    dt.Columns["Label"].Caption = "Label";
                    if (dt.Columns["arrangement"] != null)
                        dt.Columns["arrangement"].Caption = "Code arrangement";

                    if (dt.Columns["nr"] != null)
                        dt.Columns["nr"].Caption = "Booked travelers";
                    if (dt.Columns["Occupancy"] != null)
                        dt.Columns["Occupancy"].Caption = "Occupancy";


                    if (dt.Columns["nrTraveler"] != null)
                        dt.Columns["nrTraveler"].Caption = "Max travelers";
                    nr = dt.Columns.Count;

                }
                // sum nrTravelers
                if (dt.Rows.Count > 0)
                {
                    DataColumn dc = new DataColumn("Sum", typeof(int));
                    dc.Caption = "Sum of max";
                    // dc.DefaultValue = "";
                    dt.Columns.Add(dc);
                    DataColumn dc1 = new DataColumn("SumNr", typeof(int));
                    dc1.Caption = "Sum of pax";
                    // dc.DefaultValue = "";
                    dt.Columns.Add(dc1);

                    mounth = dt.Rows[0]["mounth"].ToString();
                    DataRow dr = dt.NewRow();
                    dr[0] = "Grand total sum";
                    dt.Rows.Add(dr);


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string mesec = dt.Rows[i]["mounth"].ToString();
                        if (mesec != mounth)
                        {
                            mounth = dt.Rows[i]["mounth"].ToString();
                            dr = dt.NewRow();

                            dr["mounth"] = "Sum";
                            dt.Rows.InsertAt(dr, i);
                        }

                    }


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string mesec = dt.Rows[i]["mounth"].ToString();
                        if (mesec != "Sum")
                        {
                            if (dt.Rows[i]["nr"].ToString() != "")
                            {
                                sumMounth += Convert.ToInt32(dt.Rows[i]["nr"].ToString());
                                sumAll += Convert.ToInt32(dt.Rows[i]["nr"].ToString());
                                // sumOccupancy +=  Convert.ToDecimal(sumAll/sumMounthNr*100); 
                            }
                            if (dt.Rows[i]["nrTraveler"].ToString() != "")
                            {
                                sumMounthNr += Convert.ToInt32(dt.Rows[i]["nrTraveler"].ToString());
                                sumAllNr += Convert.ToInt32(dt.Rows[i]["nrTraveler"].ToString());
                                // sumAllOccupancy += Convert.ToDecimal(sumAllNr / sumMounthNr * 100);

                            }

                        }
                        else if (mesec == "Sum")
                        {
                            if (sumMounthNr != 0)

                                sumOccupancy = Convert.ToDecimal(sumMounth / Convert.ToDecimal(sumMounthNr) * 100);
                            sumOccupancy = Convert.ToDecimal(string.Format("{0:0.00}", sumOccupancy));
                            //ZA POZICIONIRANJE POLJA 
                            dt.Rows[i]["nr"] = sumMounth;
                            dt.Rows[i]["nrTraveler"] = sumMounthNr;
                            dt.Rows[i]["Occupancy"] = sumOccupancy;

                            sumMounthNr = 0;
                            sumMounth = 0;
                            //sumOccupancy = 0;

                            //Convert.ToDecimal(sumMounth / sumMounthNr * 100);
                        }


                        if (i == dt.Rows.Count - 1)
                        {

                            dt.Rows[i]["nr"] = sumAll;
                            dt.Rows[i]["nrTraveler"] = sumAllNr;
                            dt.Rows[i]["Occupancy"] = sumAllOccupancy;
                            sumAll = 0;
                            sumAllNr = 0;
                            sumAllOccupancy = 0;

                        }
                        if (mesec == "Sum")
                        {
                            //Prevod
                            dt.Rows[i][0] = dt.Rows[i - 1][0];
                            string str = "Subtotal";
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {

                                if (resxSet.GetString(str) != null)
                                    str = resxSet.GetString(str); // provera da li postoji prevod

                            }
                            dt.Rows[i][3] = str;
                            //dt.Rows[i][1] = "";

                        }
                        if (mesec == "Grand total sum")
                        {
                            //Prevod
                            dt.Rows[i][0] = dt.Rows[i - 1][0];
                            string str = "Total";
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {

                                if (resxSet.GetString(str) != null)
                                    str = resxSet.GetString(str); // provera da li postoji prevod

                            }
                            dt.Rows[i][3] = str;
                            //dt.Rows[i][1] = "";

                            if (Convert.ToDecimal(dt.Rows[i]["nrTraveler"]) != 0)
                                dt.Rows[i]["Occupancy"] = decimal.Round(Convert.ToDecimal(dt.Rows[i]["nr"]) / Convert.ToDecimal(dt.Rows[i]["nrTraveler"]) * 100, 2);


                        }
                    }
                    int nr1 = dt.Columns.Count;


                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    dt.Columns.Add(dc2);

                    DataColumn dc3 = new DataColumn("DateTo", typeof(string));
                    dc3.Caption = "Date to";
                    dc3.ColumnName = "DateTo";
                    dt.Columns.Add(dc3);

                    dt.Rows[0][nr1] = model.dateFrom.ToString("dd/MM/yyyy");
                    dt.Rows[0][nr1 + 1] = model.dateTo.ToString("dd/MM/yyyy");
                }

                dt.Columns["mounth"].SetOrdinal(0);
                dt.Columns["dateFrom"].SetOrdinal(1);
                dt.Columns["dayarr"].SetOrdinal(2);
                dt.Columns["Label"].SetOrdinal(3);
                dt.Columns["arrangement"].SetOrdinal(4);
                dt.Columns["nr"].SetOrdinal(5);
                if (dt.Columns["SumNr"] != null)
                {
                    dt.Columns["SumNr"].SetOrdinal(5);
                    dt.Columns["nrTraveler"].SetOrdinal(6);
                    dt.Columns["Sum"].SetOrdinal(7);

                }


            }
            //

            return dt;

        }

        private DataTable DepartureList3In(int idArrType,int idLabel)
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);
            DataTable dt = new DataTable();
            ArrangementBookDAO ab = new ArrangementBookDAO();
            dt = ab.DepartureList3(model.dateFrom, model.dateTo, idArrType, idLabel);
            if (dt != null)
            {
               if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["dateFrom"] != null)
                        dt.Columns["dateFrom"].Caption = "Date from";
                    if (dt.Columns["arrangement"] != null)
                        dt.Columns["arrangement"].Caption = "Code arrangement";
                    if (dt.Columns["dateFrom"] != null)
                        dt.Columns["dateFrom"].Caption = "Date from";
                    if (dt.Columns["bookedTravelers"] != null)
                        dt.Columns["bookedTravelers"].Caption = "Booked travelers";
                    if (dt.Columns["maxTravelers"] != null)
                        dt.Columns["maxTravelers"].Caption = "Max travelers";
                    if (dt.Columns["Occupancy"] != null)
                        dt.Columns["Occupancy"].Caption = "Occupancy %";
                 
                 
                }
                if (dt.Rows.Count > 0)
                {
                   //DataRow dr = dt.NewRow();
                   //dt.Rows.Add(dr);


                   //sumNrTr=0;
                   //sumMaxNrTr=0;
                   //sumOccupancy=0;
                   //for (int i = 0; i < dt.Rows.Count;i++ )
                   //{
                   //    if (dt.Rows[i]["bookedTravelers"].ToString() != "")
                   //    {
                   //        sumNrTr += Convert.ToInt32(dt.Rows[i]["bookedTravelers"]);
                   //    }
                   //    if (dt.Rows[i]["maxTravelers"].ToString() != "")
                   //    {
                   //        sumMaxNrTr += Convert.ToInt32(dt.Rows[i]["maxTravelers"]);
                   //    }
                     
                   //}
                   //dt.Rows[dt.Rows.Count-1]["bookedTravelers"] = sumNrTr;
                   //dt.Rows[dt.Rows.Count-1]["maxTravelers"] = sumMaxNrTr;
                   //dt.Rows[dt.Rows.Count - 1]["Occupancy"] = Convert.ToDouble(sumNrTr * 100 / sumMaxNrTr);

                   int nr1 = dt.Columns.Count;


                   DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                   dc2.Caption = "Date from";
                   dc2.ColumnName = "DateFrom";
                   dt.Columns.Add(dc2);

                   DataColumn dc1 = new DataColumn("DateTo", typeof(string));
                   dc1.Caption = "Date to";
                   dc1.ColumnName = "DateTo";
                   dt.Columns.Add(dc1);

                   dt.Rows[0][nr1] = model.dateFrom.ToString("dd/MM/yyyy");
                   dt.Rows[0][nr1 + 1] = model.dateTo.ToString("dd/MM/yyyy");

                }
            
            }



            return dt;
        
        
        }

        private DataTable DepartureList3Ex(int idArrType,int idLabel)
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);
            DataTable dt = new DataTable();
            ArrangementBookDAO ab = new ArrangementBookDAO();
            dt = ab.DepartureList3Ex(model.dateFrom, model.dateTo, idArrType,idLabel);
            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["dateFrom"] != null)
                        dt.Columns["dateFrom"].Caption = "Date from";
                    if (dt.Columns["arrangement"] != null)
                        dt.Columns["arrangement"].Caption = "Code arrangement";
                    if (dt.Columns["dateFrom"] != null)
                        dt.Columns["dateFrom"].Caption = "Date from";
                    if (dt.Columns["bookedTravelers"] != null)
                        dt.Columns["bookedTravelers"].Caption = "Booked travelers";
                    if (dt.Columns["maxTravelers"] != null)
                        dt.Columns["maxTravelers"].Caption = "Max travelers";
                    if (dt.Columns["Occupancy"] != null)
                        dt.Columns["Occupancy"].Caption = "Occupancy %";

                    int nr1 = dt.Columns.Count;


                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    dt.Columns.Add(dc2);

                    DataColumn dc1 = new DataColumn("DateTo", typeof(string));
                    dc1.Caption = "Date to";
                    dc1.ColumnName = "DateTo";
                    dt.Columns.Add(dc1);

                    if (dt.Rows.Count > 0) // provera da li ima redova
                    {
                        dt.Rows[0][nr1] = model.dateFrom.ToString("dd/MM/yyyy");
                        dt.Rows[0][nr1 + 1] = model.dateTo.ToString("dd/MM/yyyy");
                    }

                }
               
            }



            return dt;


        }

        private DataTable DepartureList4xIn(int idArrType, string provision,int idLabel)
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);
            DataTable dt = new DataTable();
            ArrangementBookDAO ab = new ArrangementBookDAO();
            dt = ab.DepartureList4(model.dateFrom, model.dateTo, idArrType, provision,idLabel);
            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["dateFrom"] != null)
                        dt.Columns["dateFrom"].Caption = "Date from";
                    if (dt.Columns["arrangement"] != null)
                        dt.Columns["arrangement"].Caption = "Arrangement";
                    if (dt.Columns["dateFrom"] != null)
                        dt.Columns["dateFrom"].Caption = "Date from";
                    if (dt.Columns["bookedTravelers"] != null)
                        dt.Columns["bookedTravelers"].Caption = "Booked travelers";
                    if (dt.Columns["maxTravelers"] != null)
                        dt.Columns["maxTravelers"].Caption = "Max travelers";
                    if (dt.Columns["Occupancy"] != null)
                        dt.Columns["Occupancy"].Caption = "Occupancy %";


                }
                if (dt.Rows.Count > 0)
                {
                    //DataRow dr = dt.NewRow();
                    //dt.Rows.Add(dr);


                    //sumNrTr = 0;
                    //sumMaxNrTr = 0;
                    //sumOccupancy = 0;
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    if (dt.Rows[i]["bookedTravelers"].ToString() != "")
                    //    {
                    //        sumNrTr += Convert.ToInt32(dt.Rows[i]["bookedTravelers"]);
                    //    }
                    //    if (dt.Rows[i]["maxTravelers"].ToString() != "")
                    //    {
                    //        sumMaxNrTr += Convert.ToInt32(dt.Rows[i]["maxTravelers"]);
                    //    }

                    //}
                    //dt.Rows[dt.Rows.Count - 1]["bookedTravelers"] = sumNrTr;
                    //dt.Rows[dt.Rows.Count - 1]["maxTravelers"] = sumMaxNrTr;
                    //dt.Rows[dt.Rows.Count - 1]["Occupancy"] = Convert.ToDouble(sumNrTr * 100 / sumMaxNrTr);

                    int nr1 = dt.Columns.Count;


                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    dt.Columns.Add(dc2);

                    DataColumn dc1 = new DataColumn("DateTo", typeof(string));
                    dc1.Caption = "Date to";
                    dc1.ColumnName = "DateTo";
                    dt.Columns.Add(dc1);

                    dt.Rows[0][nr1] = model.dateFrom.ToString("dd/MM/yyyy");
                    dt.Rows[0][nr1 + 1] = model.dateTo.ToString("dd/MM/yyyy");

                }

            }

            return dt;


        }

        private DataTable DepartureList4Ex(int idArrType, string provision, int idLabel)
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);
            DataTable dt = new DataTable();
            ArrangementBookDAO ab = new ArrangementBookDAO();
            dt = ab.DepartureList4Ex(model.dateFrom, model.dateTo, idArrType, provision,  idLabel);
            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["dateFrom"] != null)
                        dt.Columns["dateFrom"].Caption = "Date from";
                    if (dt.Columns["arrangement"] != null)
                        dt.Columns["arrangement"].Caption = "Arrangement";
                    if (dt.Columns["dateFrom"] != null)
                        dt.Columns["dateFrom"].Caption = "Date from";
                    if (dt.Columns["bookedTravelers"] != null)
                        dt.Columns["bookedTravelers"].Caption = "Booked travelers";
                    if (dt.Columns["maxTravelers"] != null)
                        dt.Columns["maxTravelers"].Caption = "Max travelers";
                    if (dt.Columns["Occupancy"] != null)
                        dt.Columns["Occupancy"].Caption = "Occupancy %";
                    int nr1 = dt.Columns.Count;


                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    dt.Columns.Add(dc2);

                    DataColumn dc1 = new DataColumn("DateTo", typeof(string));
                    dc1.Caption = "Date to";
                    dc1.ColumnName = "DateTo";
                    dt.Columns.Add(dc1);

                    if (dt.Rows.Count > 0) // provera da li ima redova
                    {
                        dt.Rows[0][nr1] = model.dateFrom.ToString("dd/MM/yyyy");
                        dt.Rows[0][nr1 + 1] = model.dateTo.ToString("dd/MM/yyyy");

                    }
                }

            }



            return dt;


        }
        private DataTable NrBookingsCountBooked()
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);

            List<int> nameLabel = new List<int>();

            foreach (Control c in panelLabels.Controls)
            {
                RadCheckBox rchk = (RadCheckBox)c;
                if (rchk.Checked == true)
                    nameLabel.Add(Convert.ToInt32(rchk.Name.Replace("chk", "")));
            }


            List<int> arrType = new List<int>();

            foreach (Control c in panel1.Controls)
            {
                RadCheckBox rchk = (RadCheckBox)c;
                if (rchk.Checked == true)
                    arrType.Add(Convert.ToInt32(rchk.Name.Replace("chk", "")));
            }
            ArrangementBookBUS pbus = new ArrangementBookBUS();
            DataTable data = new DataTable();
            data = new ArrangementBookDAO().NrOfOptionsBooked(arrType, nameLabel, model.dateFrom, model.dateTo);

            if (data != null)
            {
                if (data.Rows.Count > 0)
                {

                    if (data.Columns["nameLabel"] != null)
                        data.Columns["nameLabel"].Caption = "Label";
                    if (data.Columns["codeArrangement"] != null)
                        data.Columns["codeArrangement"].Caption = "Arrangement";
                    if (data.Columns["idArrangement"] != null)
                        data.Columns["idArrangement"].Caption = " ID arrangement";
                    if (data.Columns["nameArrType"] != null)
                        data.Columns["nameArrType"].Caption = "Type";
                    if (data.Columns["dateFrom"] != null)
                        data.Columns["dateFrom"].Caption = "Date from";
                    if (data.Columns["dateTo"] != null)
                        data.Columns["dateTo"].Caption = "Date to";
                    if (data.Columns["nrTraveler"] != null)
                        data.Columns["nrTraveler"].Caption = "Number of traveler";
                    if (data.Columns["bookedTravelers"] != null)
                        data.Columns["bookedTravelers"].Caption = "Booked traveler";


                    int nr1 = data.Columns.Count;


                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    data.Columns.Add(dc2);

                    DataColumn dc1 = new DataColumn("DateTo", typeof(string));
                    dc1.Caption = "Date to";
                    dc1.ColumnName = "DateTo";
                    data.Columns.Add(dc1);

                    data.Rows[0][nr1] = model.dateFrom.ToString("dd/MM/yyyy");
                    data.Rows[0][nr1 + 1] = model.dateTo.ToString("dd/MM/yyyy");

                }
            }
            return data;


        }

        //Novo Canceled report
        private DataTable Canceled()
        {
            ArrangementBookBUS bus = new ArrangementBookBUS();
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);
            DataTable data = new DataTable();
            List<int> nameLabel = new List<int>();

            foreach (Control c in panelLabels.Controls)
            {
                RadCheckBox rchk = (RadCheckBox)c;
                if (rchk.Checked == true)
                    nameLabel.Add(Convert.ToInt32(rchk.Name.Replace("chk", "")));
            }


            List<int> arrType = new List<int>();

            foreach (Control c in panel1.Controls)
            {
                RadCheckBox rchk = (RadCheckBox)c;
                if (rchk.Checked == true)
                    arrType.Add(Convert.ToInt32(rchk.Name.Replace("chk", "")));
            }
            data = new ArrangementBookDAO().NrOfOptionsCanceled(arrType, nameLabel, model.dateFrom, model.dateTo);


            if (data != null)
            {
                if (data.Columns.Count > 0)
                {
                    if (data.Columns["nameLabel"] != null)
                        data.Columns["nameLabel"].Caption = "Label";
                    if (data.Columns["codeArrangement"] != null)
                        data.Columns["codeArrangement"].Caption = "Arrangement";
                    if (data.Columns["idArrangement"] != null)
                        data.Columns["idArrangement"].Caption = " ID arrangement";
                    if (data.Columns["nameArrType"] != null)
                        data.Columns["nameArrType"].Caption = "Type";
                    if (data.Columns["dateFrom"] != null)
                        data.Columns["dateFrom"].Caption = "Date from";
                    if (data.Columns["dateTo"] != null)
                        data.Columns["dateTo"].Caption = "Date to";
                    if (data.Columns["nrTraveler"] != null)
                        data.Columns["nrTraveler"].Caption = "Number of traveler";
                    if (data.Columns["Canceled"] != null)
                        data.Columns["Canceled"].Caption = "Canceled";

                    int nr1 = data.Columns.Count;


                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    data.Columns.Add(dc2);

                    DataColumn dc1 = new DataColumn("DateTo", typeof(string));
                    dc1.Caption = "Date to";
                    dc1.ColumnName = "DateTo";
                    data.Columns.Add(dc1);

                    if (data.Rows.Count > 0) // provera da li ima redova
                    {
                        data.Rows[0][nr1] = model.dateFrom.ToString("dd/MM/yyyy");
                        data.Rows[0][nr1 + 1] = model.dateTo.ToString("dd/MM/yyyy");
                    }

                }

            }

            return data;
        }
       

       
        private void dtFrom_Leave(object sender, EventArgs e)
        {
            ArrangementBookBUS ub = new ArrangementBookBUS();
            List<EmployeeModel> employeemodel = ub.AllEmployee(Convert.ToDateTime(dtFrom.Value.ToString()), Convert.ToDateTime(dtTo.Value.ToString()));
            dlEmployee.DataSource = employeemodel;
            dlEmployee.DisplayMember = "firstNameEmployee";
            dlEmployee.ValueMember = "idEmployee";
        }

        private void dtTo_Leave(object sender, EventArgs e)
        {
            ArrangementBookBUS ub = new ArrangementBookBUS();
            List<EmployeeModel> employeemodel = ub.AllEmployee(Convert.ToDateTime(dtFrom.Value.ToString()), Convert.ToDateTime(dtTo.Value.ToString()));
            dlEmployee.DataSource = employeemodel;
            dlEmployee.DisplayMember = "firstNameEmployee";
            dlEmployee.ValueMember = "idEmployee";
        }
    

  


 
    }
}
