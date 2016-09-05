using BIS.Business;
using BIS.DAO;
using BIS.Model;
using GUI.ReportsForms;
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
    public partial class frmInsuranceSelection : Telerik.WinControls.UI.RadForm
    {

        DataTable model;
        DateTime dateFrom = DateTime.Now;
        DateTime dateTo = DateTime.Now;
     //   private string from;
        private int idArrangement = -1;

   //     private string to;
        private string name;
        public frmInsuranceSelection()
        {
            InitializeComponent();
            string nameForm = this.Text.Substring(0);
            this.Icon = Login.iconForm;
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(nameForm) != null)
                    nameForm = resxSet.GetString(nameForm);
            }
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + nameForm;
        }

        private void frmInsuranceSelection_Load(object sender, EventArgs e)
        {
            Translation();
            txtArrangementName.Visible = false;
            btnArrangementLookup.Visible = false;
        }

        private void Translation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblDtFrom.Text) != null)
                    lblDtFrom.Text = resxSet.GetString(lblDtFrom.Text);

                if (resxSet.GetString(lblDtTo.Text) != null)
                    lblDtTo.Text = resxSet.GetString(lblDtTo.Text);

                if (resxSet.GetString(rbDate.Text) != null)
                    rbDate.Text = resxSet.GetString(rbDate.Text);

                if (resxSet.GetString(rbArrangement.Text) != null)
                    rbArrangement.Text = resxSet.GetString(rbArrangement.Text);

                if (resxSet.GetString(rbCancelInsurance.Text) != null)
                    rbCancelInsurance.Text = resxSet.GetString(rbCancelInsurance.Text);

                if (resxSet.GetString(rbInsurance.Text) != null)
                    rbInsurance.Text = resxSet.GetString(rbInsurance.Text);

                if (resxSet.GetString(btnPrint.Text) != null)
                    btnPrint.Text = resxSet.GetString(btnPrint.Text);

                if (resxSet.GetString(btnCancel.Text) != null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);

                if (resxSet.GetString(rbCancelInsuranceWithoutArrangement.Text) != null)
                    rbCancelInsuranceWithoutArrangement.Text = resxSet.GetString(rbCancelInsuranceWithoutArrangement.Text);

                if (resxSet.GetString(rbInsuranceWithoutArrangemnt.Text) != null)
                    rbInsuranceWithoutArrangemnt.Text = resxSet.GetString(rbInsuranceWithoutArrangemnt.Text);

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            if (rbDate.IsChecked == true && rbInsurance.IsChecked==true)
            {

                dateFrom = Convert.ToDateTime(dtDtFrom.Value);
                dateTo = Convert.ToDateTime(dtDtTo.Value);

                model = new DataTable();
                InvoiceDAO lad = new InvoiceDAO();

                model = lad.GetInvoiceDataPrint(dateFrom, dateTo, "Insurance");

                if (model != null)
                {
                    if (model.Rows.Count > 0)
                    {
                        DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                        dc2.Caption = "Date from";
                        dc2.ColumnName = "ParamDateFrom";
                        model.Columns.Add(dc2);

                        DataColumn dc1 = new DataColumn("DateTo", typeof(string));
                        dc1.Caption = "Date to";
                        dc1.ColumnName = "ParamDateTo";
                        model.Columns.Add(dc1);

                        DataColumn dc3 = new DataColumn("userName, typeof(string)");
                        dc3.ColumnName = "userName";
                        model.Columns.Add(dc3);

                        model.Rows[0]["ParamDateFrom"] = dateFrom.ToString("dd-MM-yyyy");
                        model.Rows[0]["ParamDateTo"] = dateTo.ToString("dd-MM-yyyy");
                        model.Columns.Add("ParametarDateFrom");
                        model.Rows[0]["ParametarDateFrom"] = dateFrom;
                        model.Columns.Add("ParametarDateTo");
                        model.Rows[0]["userName"] = Login._user.nameUser;

                        frmInsuranceReport frm = new frmInsuranceReport(model, name);
                        frm.Show();
                      

                    }
                    else
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("There is nothing for print preview!");
                     
                    }
                }
               
                else 
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("There is nothing for print preview!");
                    
                }
                txtArrangementName.Text = "";
                idArrangement = -1;                   
            }


            if (rbDate.IsChecked == true && rbCancelInsuranceWithoutArrangement.IsChecked == true)
            {

                dateFrom = Convert.ToDateTime(dtDtFrom.Value);
                dateTo = Convert.ToDateTime(dtDtTo.Value);

                model = new DataTable();
                InvoiceDAO lad = new InvoiceDAO();

                model = lad.GetInvoiceDataPrint(dateFrom, dateTo, "Cancel insurance");

                if (model != null)
                {
                    if (model.Rows.Count > 0)
                    {
                        DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                        dc2.Caption = "Date from";
                        dc2.ColumnName = "ParamDateFrom";
                        model.Columns.Add(dc2);

                        DataColumn dc1 = new DataColumn("DateTo", typeof(string));
                        dc1.Caption = "Date to";
                        dc1.ColumnName = "ParamDateTo";
                        model.Columns.Add(dc1);

                        DataColumn dc3 = new DataColumn("userName, typeof(string)");
                        dc3.ColumnName = "userName";
                        model.Columns.Add(dc3);

                        model.Rows[0]["ParamDateFrom"] = dateFrom.ToString("dd-MM-yyyy");
                        model.Rows[0]["ParamDateTo"] = dateTo.ToString("dd-MM-yyyy");
                        model.Columns.Add("ParametarDateFrom");
                        model.Rows[0]["ParametarDateFrom"] = dateFrom;
                        model.Columns.Add("ParametarDateTo");
                        model.Rows[0]["userName"] = Login._user.nameUser;

                        frmCancelInsuranceWithoutArrGroupReport frm = new frmCancelInsuranceWithoutArrGroupReport(model, name);
                        frm.Show();
                      

                    }
                    else
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("There is nothing for print preview!");
                     
                    }
                }
               
                else 
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("There is nothing for print preview!");
                    
                }
                txtArrangementName.Text = "";
                idArrangement = -1;                   
            }

            if (rbDate.IsChecked == true && rbInsuranceWithoutArrangemnt.IsChecked == true)
            {

                dateFrom = Convert.ToDateTime(dtDtFrom.Value);
                dateTo = Convert.ToDateTime(dtDtTo.Value);

                model = new DataTable();
                InvoiceDAO lad = new InvoiceDAO();

                model = lad.GetInvoiceDataPrint(dateFrom, dateTo, "Insurance");

                if (model != null)
                {
                    if (model.Rows.Count > 0)
                    {
                        DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                        dc2.Caption = "Date from";
                        dc2.ColumnName = "ParamDateFrom";
                        model.Columns.Add(dc2);

                        DataColumn dc1 = new DataColumn("DateTo", typeof(string));
                        dc1.Caption = "Date to";
                        dc1.ColumnName = "ParamDateTo";
                        model.Columns.Add(dc1);

                        DataColumn dc3 = new DataColumn("userName, typeof(string)");
                        dc3.ColumnName = "userName";
                        model.Columns.Add(dc3);

                        model.Rows[0]["ParamDateFrom"] = dateFrom.ToString("dd-MM-yyyy");
                        model.Rows[0]["ParamDateTo"] = dateTo.ToString("dd-MM-yyyy");
                        model.Columns.Add("ParametarDateFrom");
                        model.Rows[0]["ParametarDateFrom"] = dateFrom;
                        model.Columns.Add("ParametarDateTo");
                        model.Rows[0]["userName"] = Login._user.nameUser;

                        frmInsuranceWithoutArrGroupReport frm = new frmInsuranceWithoutArrGroupReport(model, name);
                        frm.Show();


                    }
                    else
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("There is nothing for print preview!");

                    }
                }

                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("There is nothing for print preview!");

                }
                txtArrangementName.Text = "";
                idArrangement = -1;
            }

            if (rbDate.IsChecked == true && rbCancelInsurance.IsChecked == true)
            {

                dateFrom = Convert.ToDateTime(dtDtFrom.Value);
                dateTo = Convert.ToDateTime(dtDtTo.Value);

           
                model = new DataTable();
                InvoiceDAO lad = new InvoiceDAO();

                model = lad.GetInvoiceDataPrint(dateFrom, dateTo, "Cancel insurance");
                if (model != null)
                {
                    if (model.Rows.Count > 0)
                    {
                        DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                        dc2.Caption = "Date from";
                        dc2.ColumnName = "ParamDateFrom";
                        model.Columns.Add(dc2);

                        DataColumn dc1 = new DataColumn("DateTo", typeof(string));
                        dc1.Caption = "Date to";
                        dc1.ColumnName = "ParamDateTo";
                        model.Columns.Add(dc1);

                        DataColumn dc3 = new DataColumn("userName, typeof(string)");
                        dc3.ColumnName = "userName";
                        model.Columns.Add(dc3);

                        model.Rows[0]["ParamDateFrom"] = dateFrom.ToString("dd-MM-yyyy");
                        model.Rows[0]["ParamDateTo"] = dateTo.ToString("dd-MM-yyyy");
                        model.Columns.Add("ParametarDateFrom");
                        model.Rows[0]["ParametarDateFrom"] = dateFrom;
                        model.Columns.Add("ParametarDateTo");
                        model.Rows[0]["userName"] = Login._user.nameUser;

                        frmCancelInsuranceReport frm = new frmCancelInsuranceReport(model, name);
                        frm.Show();
                    }
                    else
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("There is nothing for print preview!");
                    }
                }
                else 
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("There is nothing for print preview!");
                }

                txtArrangementName.Text = "";
                idArrangement = -1;
            }

            if (rbArrangement.IsChecked == true && rbCancelInsurance.IsChecked == true)
            {
                if (idArrangement != -1)
                {
                 
                    dateFrom = Convert.ToDateTime(dtDtFrom.Value);
                    dateTo = Convert.ToDateTime(dtDtTo.Value);

                    model = new DataTable();
                    InvoiceDAO lad = new InvoiceDAO();

                    model = lad.GetInvoiceArrangementDataPrint("Cancel insurance", idArrangement);
                    if (model != null)
                    {
                        if (model.Rows.Count > 0)
                        {
                            
                            DataColumn dc3 = new DataColumn("userName, typeof(string)");
                            dc3.ColumnName = "userName";
                            model.Columns.Add(dc3);


                            model.Rows[0]["userName"] = Login._user.nameUser;
                            frmCancelInsuranceArrangementReport frm = new frmCancelInsuranceArrangementReport(model, name);
                            frm.Show();

                           
                        }
                        else
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("There is nothing for print preview!");
                        }
                       
                    }
                    else 
                    {
                       
                       translateRadMessageBox trs = new translateRadMessageBox();
                       trs.translateAllMessageBox("There is nothing for print preview!");                
                    }
                }
                else 
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Arrangement is required!");
                }

            }

            if (rbArrangement.IsChecked == true && rbInsurance.IsChecked == true)
            {
                if (idArrangement != -1)
                {
                    dateFrom = Convert.ToDateTime(dtDtFrom.Value);
                    dateTo = Convert.ToDateTime(dtDtTo.Value);

                    model = new DataTable();
                    InvoiceDAO lad = new InvoiceDAO();

                    model = lad.GetInvoiceArrangementDataPrint("Insurance", idArrangement);

                    if (model != null)
                    {
                        if (model.Rows.Count > 0)
                        {
                            DataColumn dc3 = new DataColumn("userName, typeof(string)");
                            dc3.ColumnName = "userName";
                            model.Columns.Add(dc3);

                            model.Rows[0]["userName"] = Login._user.nameUser;

                            frmInsuranceArrangementReport frm = new frmInsuranceArrangementReport(model, name);
                            frm.Show();
                           
                        }
                        else
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("There is nothing for print preview!");
                           // this.Close();
                        }
                    }
                    else
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("There is nothing for print preview!");
                     //   this.Close();
                    }
                   
                }
                else 
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Arrangement is required!");
                }
            }
        }
        #  region event
        private void rbDate_CheckStateChanged(object sender, EventArgs e)
        {
            lblDtFrom.Visible = true;
            dtDtFrom.Visible = true;
            lblDtTo.Visible = true;
            dtDtTo.Visible = true;
            txtArrangementName.Visible = false;
            btnArrangementLookup.Visible = false;
            rbCancelInsuranceWithoutArrangement.Visible = true;
            rbInsuranceWithoutArrangemnt.Visible = true;
            

        }

        private void rbArrangement_CheckStateChanged(object sender, EventArgs e)
        {
            lblDtFrom.Visible = false;
            dtDtFrom.Visible = false;           
            lblDtTo.Visible = false;
            dtDtTo.Visible = false;
            txtArrangementName.Visible = true;
            btnArrangementLookup.Visible = true;
            rbCancelInsuranceWithoutArrangement.Visible = false;
            rbInsuranceWithoutArrangemnt.Visible = false;
        }
        # endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnArrangementLookup_Click(object sender, EventArgs e)
        {
            ArrangementBUS accBUS = new ArrangementBUS();
            List<IModel> am = new List<IModel>();

            am = accBUS.GeArrangementsLookup();

            // ako se promeni "Travel papers" mora i u lookupu, jer je po nazivu lookup-a definisa sirina kolone u gridLookup_DataBindingComplete
            var dlgArrangement = new GridLookupInsuranceReportArrangement(am, "ArrangementInsurance");

            if (dlgArrangement.ShowDialog(this) == DialogResult.Yes)
            {
                idArrangement = -1;
                ArrangementLookupModel okm = new ArrangementLookupModel();
                okm = (ArrangementLookupModel)dlgArrangement.selectedRow;
                if (okm.codeArrangement != null)
                 txtArrangementName.Text = okm.codeArrangement.ToString();
                else
                 txtArrangementName.Text = "";
                idArrangement = okm.idArrangement;

            }
        }

      

      
    }
}
