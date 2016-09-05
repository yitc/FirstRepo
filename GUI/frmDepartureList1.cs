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

namespace GUI
{
    public partial class frmDepartureList1 : Telerik.WinControls.UI.RadForm
    {
        PurchaseReportModel model= new PurchaseReportModel();
        List<ArrTypeModel> list = new List<ArrTypeModel>();
        List<LabelModel> listLablel = new List<LabelModel>();
       
        int sumNrTr;
        int sumMaxNrTr;
        int sumOccupancy;
        //int idArrType=0;
        //int idLabel = 0;
        //list3
        //string mounth = "";
        //string mounthNr = "";
        //int sumMounth = 0;
        //int sumAll = 0;
        //int sumMounthNr = 0;
        //int sumAllNr = 0;
        //string nazivmeseca;
        //int noRow = 0;
        //DataRow dr;

        public frmDepartureList1()
        {
            InitializeComponent();
        }

        private void frmDepartureList1_Load(object sender, EventArgs e)
        {
            RadCheckBox rchk1;
            int Y1 = 0;
            ArrTypeBUS accBUS = new ArrTypeBUS();
            List<ArrTypeModel> am = new List<ArrTypeModel>();
            dtFrom.Value = DateTime.Now;
            dtTo.Value = DateTime.Now;

            am = accBUS.GetArrTypes();


            List<string> type = new List<string>();
            string s = "";

            for (int i = 0; i < am.Count; i++)
            {
                s = am[i].nameArrType.ToString();
                type.Add(s);
            }
            for (int i = 0; i <am.Count; i++)
            {
                rchk1 = new RadCheckBox();
                rchk1.Font = new Font("Verdana", 9);
                // rchk.ThemeName = radPageArrange.ThemeName;
                rchk1.Name = "chk" + am[i].idArrType;
                rchk1.Text = am[i].nameArrType.ToString();
                rchk1.Location = new Point(0, Y1);
                rchk1.AutoSize = true;
                Y1 = Y1 + 3 + rchk1.Height;
               
            }

            this.Icon = Login.iconForm;
            string name = "Report departure list 1";

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(name) != null)
                    name = resxSet.GetString(name);
            }
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + name;

            setTranslation();
          //  
           
            RadCheckBox rchk;
           int  Y = 0;
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
              
            }

            ArrangementBookBUS bus= new ArrangementBookBUS();
            list = bus.AllArrType();         
           
        }



        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblDtFrom.Text) != null)
                    lblDtFrom.Text = resxSet.GetString(lblDtFrom.Text);

                if (resxSet.GetString(lblDtTo.Text) != null)
                    lblDtTo.Text = resxSet.GetString(lblDtTo.Text);


                if (resxSet.GetString(btnInclusive1.Text) != null)
                    btnOK.Text = resxSet.GetString(btnInclusive1.Text);

                if (resxSet.GetString(btnExlusive1.Text) != null)
                    btnOK.Text = resxSet.GetString(btnExlusive1.Text);

                if (resxSet.GetString(btnOK.Text) != null)
                    btnOK.Text = resxSet.GetString(btnOK.Text);

            }
        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {      
          

            DataTable dt1 = new DataTable();
            DataTable dt63 = new DataTable();


            if (btnExlusive1.IsChecked == true)
                {

                    DepartureList1Ex();
                    rgvResult.DataSource = DepartureList1Ex();
                    dt63 = DepartureList1Ex();
                }
                if (btnInclusive1.IsChecked == true)
                {
                    DepartureList1();
                    dt1 = DepartureList1();
                    rgvResult.DataSource = DepartureList1();
                }          

            //
            //Konstruktori 

            
                if (btnInclusive1.IsChecked == true)
                {
                    frmReportDepartureList frm1 = new frmReportDepartureList(dt1);
                    frm1.Show();
                }
                if (btnExlusive1.IsChecked == true)
                {
                    frmReportDepartureList1Ex frm63 = new frmReportDepartureList1Ex(dt63);
                    frm63.Show();
                }
            
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
                    DataRow dr = dt.NewRow();
                    dr["Label"] = "Total";
                    dt.Rows.Add(dr);


                    sumNrTr = 0;
                    sumMaxNrTr = 0;
                    sumOccupancy = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["TotalBooked"].ToString() != "")
                        {
                            sumNrTr += Convert.ToInt32(dt.Rows[i]["TotalBooked"]);
                        }
                        if (dt.Rows[i]["Maximum"].ToString() != "")
                        {
                            sumMaxNrTr += Convert.ToInt32(dt.Rows[i]["Maximum"]);
                        }

                    }
                    dt.Rows[dt.Rows.Count - 1]["TotalBooked"] = sumNrTr;
                    dt.Rows[dt.Rows.Count - 1]["Maximum"] = sumMaxNrTr;
                    dt.Rows[dt.Rows.Count - 1]["Occupancy"] = Convert.ToDouble(sumNrTr * 100 / sumMaxNrTr);
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

                    dt.Rows[0][nr1] = model.dateFrom.ToString("dd/MM/yyyy");
                    dt.Rows[0][nr1 + 1] = model.dateTo.ToString("dd/MM/yyyy");
                }

            }
            return dt;

        }

     
 
    }
}
