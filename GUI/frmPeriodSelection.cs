using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Business;
using BIS.DAO;
using BIS.Model;
using GUI.ReportsForms;

namespace GUI
{
    public partial class frmPeriodSelection : Telerik.WinControls.UI.RadForm
    {
        DataTable model;
        private string nameUser;
        int from = 0;
        int to = 0;
      int param=1;
     
     

        public frmPeriodSelection()
        {
            InitializeComponent();
            this.Icon = Login.iconForm;
            string nameForm = this.Text.Substring(0);

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(nameForm) != null)
                    nameForm = resxSet.GetString(nameForm);
            }
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + nameForm;
        }

        private void frmPeriodSelection_Load(object sender, EventArgs e)
        {
            ddlPeriodFrom.SelectedIndex = 0;
            ddlPeriodTo.SelectedIndex = 11;
            ddlYear.SelectedIndex = 0;

            Translation();
        }

        private void Translation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblFromTo.Text) != null)
                    lblFromTo.Text = resxSet.GetString(lblFromTo.Text);

                if (resxSet.GetString(btnPrint.Text) != null)
                    btnPrint.Text = resxSet.GetString(btnPrint.Text);

                if (resxSet.GetString(btnCancel.Text) != null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);

                if (resxSet.GetString(lblYear.Text) != null)
                    lblYear.Text = resxSet.GetString(lblYear.Text);

                if (resxSet.GetString(rbTotal.Text) != null)
                    rbTotal.Text = resxSet.GetString(rbTotal.Text);

                if (resxSet.GetString(rbBalans.Text) != null)
                    rbBalans.Text = resxSet.GetString(rbBalans.Text);

                if (resxSet.GetString(rbWinst.Text) != null)
                    rbWinst.Text = resxSet.GetString(rbWinst.Text);
                            
            }
        }
        #region butt
   
     

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            string yyyy = ddlYear.Text.ToString();
            
             from = Convert.ToInt32(ddlPeriodFrom.SelectedIndex)+1;
             to = Convert.ToInt32(ddlPeriodTo.SelectedIndex) +1;
             //int startPeriod = Convert.ToInt32(ddlPeriodFrom.SelectedValue.ToString());
             //int endPeriod = Convert.ToInt32(ddlPeriodTo.SelectedValue.ToString());
             if (from <= to)
             {
                 if (yyyy != "")
                 {
                     model = new DataTable();
                     AccLedgerClassDAO lad = new AccLedgerClassDAO();

                     if (rbBalans.IsChecked == true)
                     {
                         param = 1;
                    

                     }
                     if (rbWinst.IsChecked == true)
                     {
                         param =2;
                       

                     }
                     if (rbTotal.IsChecked == true)
                     {
                         param = 3;


                     }
                     model = lad.BilansPeriodReport(param, yyyy, from,to);
                     if (model != null)
                     {
                         if (model.Rows.Count > 0)
                         {


                             DataColumn dc1 = new DataColumn("period1Visible", typeof(Boolean));
                             dc1.ColumnName = "period1Visible";
                             model.Columns.Add(dc1);

                             DataColumn dc2 = new DataColumn("period2Visible", typeof(Boolean));
                             dc2.ColumnName = "period2Visible";
                             model.Columns.Add(dc2);

                             DataColumn dc3 = new DataColumn("period3Visible", typeof(Boolean));
                             dc3.ColumnName = "period3Visible";
                             model.Columns.Add(dc3);

                             DataColumn dc4 = new DataColumn("period4Visible", typeof(Boolean));
                             dc4.ColumnName = "period4Visible";
                             model.Columns.Add(dc4);

                             DataColumn dc5 = new DataColumn("period5Visible", typeof(Boolean));
                             dc5.ColumnName = "period5Visible";
                             model.Columns.Add(dc5);

                             DataColumn dc6 = new DataColumn("period6Visible", typeof(Boolean));
                             dc6.ColumnName = "period6Visible";
                             model.Columns.Add(dc6);

                             DataColumn dc7 = new DataColumn("period7Visible", typeof(Boolean));
                             dc7.ColumnName = "period7Visible";
                             model.Columns.Add(dc7);

                             DataColumn dc8 = new DataColumn("period8Visible", typeof(Boolean));
                             dc8.ColumnName = "period8Visible";
                             model.Columns.Add(dc8);

                             DataColumn dc9 = new DataColumn("period9Visible", typeof(Boolean));
                             dc9.ColumnName = "period9Visible";
                             model.Columns.Add(dc9);

                             DataColumn dc10 = new DataColumn("period10Visible", typeof(Boolean));
                             dc10.ColumnName = "period10Visible";
                             model.Columns.Add(dc10);

                             DataColumn dc11 = new DataColumn("period11Visible", typeof(Boolean));
                             dc11.ColumnName = "period11Visible";
                             model.Columns.Add(dc11);

                             DataColumn dc12 = new DataColumn("period12Visible", typeof(Boolean));
                             dc12.ColumnName = "period12Visible";
                             model.Columns.Add(dc12);

                             DataColumn dc13 = new DataColumn("type", typeof(string));
                             dc13.ColumnName = "type";
                             model.Columns.Add(dc13);

                             DataColumn dc14 = new DataColumn("year", typeof(string));
                             dc14.ColumnName = "year";
                             model.Columns.Add(dc14);

                             if (from <= 1 && 1 <= to)
                                 model.Rows[0]["period1Visible"] = true;
                             else
                                 model.Rows[0]["period1Visible"] = false;


                             if (from <= 2 && 2 <= to)
                                 model.Rows[0]["period2Visible"] = true;
                             else
                                 model.Rows[0]["period2Visible"] = false;

                             if (from <= 3 && 3 <= to)
                                 model.Rows[0]["period3Visible"] = true;
                             else
                                 model.Rows[0]["period3Visible"] = false;

                             if (from <= 4 && 4 <= to)
                                 model.Rows[0]["period4Visible"] = true;
                             else
                                 model.Rows[0]["period4Visible"] = false;

                             if (from <= 5 && 5 <= to)
                                 model.Rows[0]["period5Visible"] = true;
                             else
                                 model.Rows[0]["period5Visible"] = false;


                             if (from <= 6 && 6 <= to)
                                 model.Rows[0]["period6Visible"] = true;
                             else
                                 model.Rows[0]["period6Visible"] = false;

                             if (from <= 7 && 7 <= to)
                                 model.Rows[0]["period7Visible"] = true;
                             else
                                 model.Rows[0]["period7Visible"] = false;

                             if (from <= 8 && 8 <= to)
                                 model.Rows[0]["period8Visible"] = true;
                             else
                                 model.Rows[0]["period8Visible"] = false;

                             if (from <= 9 && 9 <= to)
                                 model.Rows[0]["period9Visible"] = true;
                             else
                                 model.Rows[0]["period9Visible"] = false;


                             if (from <= 10 && 10 <= to)
                                 model.Rows[0]["period10Visible"] = true;
                             else
                                 model.Rows[0]["period10Visible"] = false;

                             if (from <= 11 && 11 <= to)
                                 model.Rows[0]["period11Visible"] = true;
                             else
                                 model.Rows[0]["period11Visible"] = false;

                             if (from <= 12 && 12 <= to)
                                 model.Rows[0]["period12Visible"] = true;
                             else
                                 model.Rows[0]["period12Visible"] = false;

                             model.Rows[0]["type"] = param.ToString();
                             model.Rows[0]["year"] = yyyy.ToString();


                             if (param == 3)
                             {
                                 frmPeriodSelectionTotalReport frm = new frmPeriodSelectionTotalReport(model, nameUser);
                                 frm.Show();
                             }
                             else
                             {

                                 frmPeriodSelectionReport frm = new frmPeriodSelectionReport(model, nameUser);
                                 frm.Show();
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
                         trs.translateAllMessageBox("There is nothing for print preview!");

                     }

                 }
                 else
                 {
                     translateRadMessageBox trs = new translateRadMessageBox();
                     trs.translateAllMessageBox("Year is required!");
                 }
             }

             else
             {
                 translateRadMessageBox trs = new translateRadMessageBox();
                 trs.translateAllMessageBox("Period is out of range!");
             }
               
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion butt

        private void ddlPeriodTo_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

        }

      

       

    }
}
