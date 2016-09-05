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
    public partial class frmSaldoBalansSelection : Telerik.WinControls.UI.RadForm
    {
        DataTable model;

        private string name;
        private string nameUser;
        DateTime dateFrom = DateTime.Now;
        DateTime dateTo = DateTime.Now;
 

        public frmSaldoBalansSelection()
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

        private void frmSaldoBalansSelection_Load(object sender, EventArgs e)
        {
            dtDtFrom.Value = dateFrom;
            dtDtTo.Value = dateTo;
            Translation();
        }

        private void Translation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblDtFrom.Text) != null)
                    lblDtFrom.Text = resxSet.GetString(lblDtFrom.Text);

                if (resxSet.GetString(lblDtTo.Text) != null)
                    lblDtTo.Text = resxSet.GetString(lblDtTo.Text);

                if (resxSet.GetString(btnPrint.Text) != null)
                    btnPrint.Text = resxSet.GetString(btnPrint.Text);

                if (resxSet.GetString(btnCancel.Text) != null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);

                if (resxSet.GetString(rbTotal.Text) != null)
                    rbTotal.Text = resxSet.GetString(rbTotal.Text);

                            
            }
        }
        #region butt
   
     

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            dateFrom = Convert.ToDateTime(dtDtFrom.Value);
            dateTo = Convert.ToDateTime(dtDtTo.Value);

            model = new DataTable();
            AccLedgerClassDAO lad = new AccLedgerClassDAO();
            if (rbBalans.IsChecked == true)
            {
                model = lad.GetBalansSelectionReport(dateFrom, dateTo);

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

                        DataColumn dc3 = new DataColumn("paramLineYear", typeof(string));
                        dc3.Caption = "Year line";
                        dc3.ColumnName = "paramLineYear";
                        model.Columns.Add(dc3);

                        model.Rows[0]["ParamDateFrom"] = dateFrom.ToString("dd-MM-yyyy");
                        model.Rows[0]["ParamDateTo"] = dateTo.ToString("dd-MM-yyyy");
                        model.Columns.Add("ParametarDateFrom");
                        model.Rows[0]["ParametarDateFrom"] = dateFrom;
                        model.Columns.Add("ParametarDateTo");
                        model.Rows[0]["paramLineYear"] = dateFrom.Year.ToString();



                        frmSaldoBalabsReport frm = new frmSaldoBalabsReport(model, nameUser);
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
            else if (rbWinst.IsChecked == true)
            {

                model = lad.GetWinstSelectionReport(dateFrom, dateTo);

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

                        DataColumn dc3 = new DataColumn("paramLineYear", typeof(string));
                        dc3.Caption = "Year line";
                        dc3.ColumnName = "paramLineYear";
                        model.Columns.Add(dc3);

                        model.Rows[0]["ParamDateFrom"] = dateFrom.ToString("dd-MM-yyyy");
                        model.Rows[0]["ParamDateTo"] = dateTo.ToString("dd-MM-yyyy");
                        model.Columns.Add("ParametarDateFrom");
                        model.Rows[0]["ParametarDateFrom"] = dateFrom;
                        model.Columns.Add("ParametarDateTo");
                        model.Rows[0]["paramLineYear"] = dateFrom.Year.ToString();



                        frmWinstVerliesReport frm = new frmWinstVerliesReport(model, nameUser);
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
            else if  (rbTotal.IsChecked == true)
            {
                model = lad.GetBalansTotalReport(dateFrom, dateTo);

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

                        DataColumn dc3 = new DataColumn("paramLineYear", typeof(string));
                        dc3.Caption = "Year line";
                        dc3.ColumnName = "paramLineYear";
                        model.Columns.Add(dc3);

                        model.Rows[0]["ParamDateFrom"] = dateFrom.ToString("dd-MM-yyyy");
                        model.Rows[0]["ParamDateTo"] = dateTo.ToString("dd-MM-yyyy");
                        model.Columns.Add("ParametarDateFrom");
                        model.Rows[0]["ParametarDateFrom"] = dateFrom;
                        model.Columns.Add("ParametarDateTo");
                        model.Rows[0]["paramLineYear"] = dateFrom.Year.ToString();



                        frmTotalBalansReport frm = new frmTotalBalansReport(model, nameUser);
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
                trs.translateAllMessageBox("There is nothing for print preview!");
            
            }
               
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion butt

      

       

    }
}
