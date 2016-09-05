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
    public partial class frmDepartureList3 : Telerik.WinControls.UI.RadForm
    {
        PurchaseReportModel model= new PurchaseReportModel();
        List<ArrTypeModel> list = new List<ArrTypeModel>();
        List<LabelModel> listLablel = new List<LabelModel>();

       // list4
        int sumNrTr;
        int sumMaxNrTr;
        int sumOccupancy;
        int idArrType=0;
        int idLabel = 0;

        public frmDepartureList3()
        {
            InitializeComponent();
        }

        private void frmPurchaseReport_Load(object sender, EventArgs e)
        {
            ArrTypeBUS accBUS = new ArrTypeBUS();
            List<ArrTypeModel> am = new List<ArrTypeModel>();

            am = accBUS.GetArrTypes();


            List<string> type = new List<string>();
            string s = "";

            for (int i = 0; i < am.Count; i++)
            {
                s = am[i].nameArrType.ToString();
                type.Add(s);
            }


            ArrangementBookBUS bus= new ArrangementBookBUS();
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

            this.Icon = Login.iconForm;
            string name = "Report departure list 3";

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(name) != null)
                    name = resxSet.GetString(name);
            }
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + name;


            setTranslation();
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            DataTable dt70 = new DataTable();
            DataTable dt71 = new DataTable();
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
                   // rgvResult.DataSource = DepartureList3In(idArrType,idLabel);
                }

                if (btnInclusive.IsChecked == true)
                {
                    //frmReportDepartureList3Inclusive frm70 = new frmReportDepartureList3Inclusive(dt70);
                    //frm70.Show();
                }
                else
                {
                    //frmReportDepartureList3Exclusive frm71 = new frmReportDepartureList3Exclusive(dt71);
                    //frm71.Show();
                }
          
        }


        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblDtFrom.Text) != null)
                    lblDtFrom.Text = resxSet.GetString(lblDtFrom.Text);
                if (resxSet.GetString(lblDtTo.Text) != null)
                    lblDtTo.Text = resxSet.GetString(lblDtTo.Text);
                if (resxSet.GetString(lblArrangementType.Text) != null)
                    lblArrangementType.Text = resxSet.GetString(lblArrangementType.Text);
                if (resxSet.GetString(lblLabel.Text) != null)
                    lblLabel.Text = resxSet.GetString(lblLabel.Text);


            }
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
                   DataRow dr = dt.NewRow();
                   dt.Rows.Add(dr);


                   sumNrTr=0;
                   sumMaxNrTr=0;
                   sumOccupancy=0;
                   for (int i = 0; i < dt.Rows.Count;i++ )
                   {
                       if (dt.Rows[i]["bookedTravelers"].ToString() != "")
                       {
                           sumNrTr += Convert.ToInt32(dt.Rows[i]["bookedTravelers"]);
                       }
                       if (dt.Rows[i]["maxTravelers"].ToString() != "")
                       {
                           sumMaxNrTr += Convert.ToInt32(dt.Rows[i]["maxTravelers"]);
                       }
                     
                   }
                   dt.Rows[dt.Rows.Count-1]["bookedTravelers"] = sumNrTr;
                   dt.Rows[dt.Rows.Count-1]["maxTravelers"] = sumMaxNrTr;
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

        private DataTable DepartureList3Ex(int idArrType,int idLabel)
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);
            DataTable dt = new DataTable();
            ArrangementBookDAO ab = new ArrangementBookDAO();
            dt = ab.DepartureList3(model.dateFrom, model.dateTo, idArrType,idLabel);
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

                    dt.Rows[0][nr1] = model.dateFrom.ToString("dd/MM/yyyy");
                    dt.Rows[0][nr1 + 1] = model.dateTo.ToString("dd/MM/yyyy");

                }
               
            }



            return dt;


        }
  


 
    }
}
