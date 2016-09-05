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
    public partial class frmOverwievBookingReport : Telerik.WinControls.UI.RadForm
    {

        List<ArrangementBookStatusModel> statusLista = new List<ArrangementBookStatusModel>();
        List<ArrangementBookTravelPapersModel> statusTPapers = new List<ArrangementBookTravelPapersModel>();
         ArrangementBookBUS abus = new ArrangementBookBUS(); int idStatus = -1;
        public frmOverwievBookingReport()
        {
            InitializeComponent();
        }

        private void frmOverwievBookingReport_Load(object sender, EventArgs e)
        {

          
            statusLista = abus.AllStatus();
            ddlStatus.DataSource = statusLista;
            ddlStatus.DisplayMember = "nameStatus";
            ddlStatus.ValueMember = "idStatus";

            RadRadioButton rchk;
            int Y = 0;
            statusTPapers = abus.AllTPapersStatus();
            for (int i = 0; i < statusTPapers.Count; i++)
            {
                rchk = new RadRadioButton();
                rchk.Font = new Font("Verdana", 9);
                // rchk.ThemeName = radPageArrange.ThemeName;
                rchk.Name = "chk" + statusTPapers[i].idTravelPapers.ToString();
                rchk.Text = statusTPapers[i].nameTravelPapers;
                rchk.Location = new Point(0, Y);
                rchk.AutoSize = true;
                Y = Y + 3 + rchk.Height;
                panelTPapers.Controls.Add(rchk);
            }

            setTranslation();
        }

        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(lblStatus.Text) != null)
                    lblStatus.Text = resxSet.GetString(lblStatus.Text);

                if (resxSet.GetString(btnOK.Text) != null)
                    btnOK.Text = resxSet.GetString(btnOK.Text);

            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            DataTable dt2 = new DataTable();
            string status = ddlStatus.SelectedValue.ToString();
                   
            if (status != "")
            {
                idStatus = Convert.ToInt32(status);
            
            }
           // rgvResult.DataSource = Invoice();
            //rgvResult.Visible = true;
            dt2 = Invoice();
            frmReportOverviewBooking frm2 = new frmReportOverviewBooking(dt2);
            frm2.Show();
        }

        public DataTable Invoice()
        {

            int tPapers =0;

            foreach (Control c in panelTPapers.Controls)
            {
                RadRadioButton rchk = (RadRadioButton)c;
                if (rchk.IsChecked == true)
                    tPapers= (Convert.ToInt32(rchk.Name.Replace("chk", "")));
            }
            DataTable dt = new DataTable();
            ArrangementBookDAO dao = new ArrangementBookDAO();

            dt = dao.Invoice(idStatus, tPapers);
            if (dt != null)
            {
                //   i.,ab.idArrangementBook,(select sum(brutoAmount) from Invoice inv where inv.idVoucher=ab.idArrangementBook and idInvoiceStatus= '4') as priceInvoice,e.firstNameEmployee
                
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["codeArrangement"] != null)
                        dt.Columns["codeArrangement"].Caption = "Arrangement code";
                    if (dt.Columns["dtFromArrangement"] != null)
                        dt.Columns["dtFromArrangement"].Caption = "Date from";
                    if (dt.Columns["dtToArrangement"] != null)
                        dt.Columns["dtToArrangement"].Caption = "Date to";
                    if (dt.Columns["bookedPerson"] != null)
                        dt.Columns["bookedPerson"].Caption = "Booked person";
                    if (dt.Columns["invoiceRbr"] != null)
                        dt.Columns["invoiceRbr"].Caption = "Invoice nr";                    
                    if (dt.Columns["priceInvoice"] != null)
                        dt.Columns["priceInvoice"].Caption = "Price invoice";
                    if (dt.Columns["firstNameEmployee"] != null)
                        dt.Columns["Employee"].Caption = "Employee";

                }
            
            }
            return dt;
        
        }
     

        



    }
}
