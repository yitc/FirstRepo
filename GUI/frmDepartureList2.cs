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
    public partial class frmDepartureList2 : Telerik.WinControls.UI.RadForm
    {
       // ArrangementModel model = new ArrangementModel();
        PurchaseReportModel model = new PurchaseReportModel(); 
                string mounth="";
                string mounthNr = "";
                decimal sumOccupancy = 0;
                decimal sumAllOccupancy = 0;
                int sumMounth = 0;
                int sumAll = 0;
                int sumMounthNr = 0;
                int sumAllNr = 0;
                string nazivmeseca;
                int noRow = 0;
                DataRow dr;

                decimal sumMinNumTra = 0;
                decimal sumMounthNumTra = 0;
                

        List <int> sumaPoMesecima = new List<int>();
        List<int> noviRed = new List<int>();
        List<string> nazivSume= new List<string>();
        public frmDepartureList2()
        {
            InitializeComponent();
        }

        private void frmDepartureList2_Load(object sender, EventArgs e)
        {
            dtFrom.Value = DateTime.Now;
            dtTo.Value = DateTime.Now;


            this.Icon = Login.iconForm;
            string name = "Report departure list 2";

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(name) != null)
                    name = resxSet.GetString(name);
            }
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + name;


            setTranslation();
        }
        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblDateFrom.Text) != null)
                    lblDateFrom.Text = resxSet.GetString(lblDateFrom.Text);

                if (resxSet.GetString(lblDateTo.Text) != null)
                    lblDateTo.Text = resxSet.GetString(lblDateTo.Text);
                              
                
                if (resxSet.GetString(btnInclusive.Text) != null)
                    btnOK.Text = resxSet.GetString(btnInclusive.Text);

                if (resxSet.GetString(btnExlusive.Text) != null)
                    btnOK.Text = resxSet.GetString(btnExlusive.Text);

                if (resxSet.GetString(btnOK.Text) != null)
                    btnOK.Text = resxSet.GetString(btnOK.Text);

            }
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
                    if (dt.Columns["minNrTraveler"] != null)
                        dt.Columns["minNrTraveler"].Caption = "Min travelers";
                    if (dt.Columns["Occupancy"] != null)
                        dt.Columns["Occupancy"].Caption = "Occupancy";
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
                            if (dt.Rows[i]["minNrTraveler"].ToString() != "")
                            {
                                sumMounthNumTra += Convert.ToInt32(dt.Rows[i]["minNrTraveler"].ToString());
                                sumMinNumTra += Convert.ToInt32(dt.Rows[i]["minNrTraveler"].ToString());
                            }
                        }
                        else if (mesec == "Sum")
                            if (sumMounthNr != 0)
                        {
                            sumOccupancy = Convert.ToDecimal(sumMounth / Convert.ToDecimal(sumMounthNr) * 100);
                            sumOccupancy = Convert.ToDecimal(string.Format("{0:0.00}", sumOccupancy));
                            //ZA POZICIONIRANJE POLJA 
                            dt.Rows[i]["nr"] = sumMounth;
                            dt.Rows[i]["nr"] = sumMounth;
                            dt.Rows[i]["nrTraveler"] = sumMounthNr;
                            dt.Rows[i]["minNrTraveler"] = sumMounthNumTra;
                            dt.Rows[i]["Occupancy"] = sumOccupancy;

                            sumMounthNr = 0;
                            sumMounth = 0;
                            sumMounthNumTra = 0;

                        }


                        if (i == dt.Rows.Count - 1)
                        {
                            dt.Rows[i]["nr"] = sumAll;
                            dt.Rows[i]["nrTraveler"] = sumAllNr;
                            dt.Rows[i]["minNrTraveler"] = sumMinNumTra;
                            sumAll = 0;
                            sumAllNr = 0;
                            sumMinNumTra = 0;

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

                //dt.Columns["mounth"].SetOrdinal(0);
                dt.Columns["dateFrom"].SetOrdinal(0);
                //dt.Columns["dayarr"].SetOrdinal(2);
                dt.Columns["arrangement"].SetOrdinal(1);
                dt.Columns["nr"].SetOrdinal(4);
                dt.Columns["minNrTraveler"].SetOrdinal(2);
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

                    if (dt.Columns["minNrTraveler"] != null)
                        dt.Columns["minNrTraveler"].Caption = "Min travelers";
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

                            if(dt.Rows[i]["minNrTraveler"].ToString()!="")
                            {
                                sumMounthNumTra+=Convert.ToInt32(dt.Rows[i]["minNrTraveler"].ToString());
                                sumMinNumTra += Convert.ToInt32(dt.Rows[i]["minNrTraveler"].ToString());
                            }
                                
                           
                        }
                        else if (mesec == "Sum")
                        {
                            if(sumMounthNr!= 0)
                            
                            sumOccupancy =Convert.ToDecimal(sumMounth / Convert.ToDecimal(sumMounthNr) * 100);
                            sumOccupancy = Convert.ToDecimal(string.Format("{0:0.00}", sumOccupancy));
                            //ZA POZICIONIRANJE POLJA 
                            dt.Rows[i]["nr"] = sumMounth;
                            dt.Rows[i]["nrTraveler"] = sumMounthNr;
                           dt.Rows[i]["Occupancy"] = sumOccupancy;
                           dt.Rows[i]["minNrTraveler"] = sumMounthNumTra;
                           
                            sumMounthNr = 0;
                            sumMounth = 0;
                            sumMounthNumTra = 0;
                            //sumOccupancy = 0;

                            //Convert.ToDecimal(sumMounth / sumMounthNr * 100);
                        }


                        if (i == dt.Rows.Count - 1)
                        {
                           
                            dt.Rows[i]["nr"] = sumAll;
                            dt.Rows[i]["nrTraveler"] = sumAllNr;
                            dt.Rows[i]["Occupancy"] = sumAllOccupancy;
                            dt.Rows[i]["minNrTraveler"] = sumMinNumTra;
                            sumAll = 0;
                            sumAllNr = 0;
                            sumAllOccupancy = 0;
                            sumMinNumTra = 0;

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
                            dt.Rows[i][1] = str;
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
                            dt.Rows[i][1] = str;
                            //dt.Rows[i][1] = "";

                            if( Convert.ToDecimal(dt.Rows[i]["nrTraveler"])!=0)
                            dt.Rows[i]["Occupancy"] = decimal.Round(Convert.ToDecimal(dt.Rows[i]["nr"]) / Convert.ToDecimal(dt.Rows[i]["nrTraveler"]) * 100,2);
                            

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

               // dt.Columns["mounth"].SetOrdinal(0);
                dt.Columns["dateFrom"].SetOrdinal(0);
               // dt.Columns["dayarr"].SetOrdinal(2);
                dt.Columns["Label"].SetOrdinal(1);
                dt.Columns["arrangement"].SetOrdinal(2);
                dt.Columns["minNrTraveler"].SetOrdinal(3);
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
        
        //sum nr


        //

        private void btnOK_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            //  btnStragglersList

            DataTable dt28 = new DataTable();
            DataTable dt29 = new DataTable();
            if (btnExlusive.IsChecked == true)
            {
                DepartureList2Ex();
                //rgvResult.DataSource = DepartureList2Ex();
                dt28 = DepartureList2Ex();
            }

            else
            {                
                DepartureList2();
                //rgvResult.DataSource = DepartureList2();
                dt29 = DepartureList2();
            }

            if (btnExlusive.IsChecked == true)
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

    



    
 

 
    }
}
