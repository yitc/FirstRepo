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
    public partial class frmPrognoseSelection : Telerik.WinControls.UI.RadForm
    {

        DataTable model;
        DateTime dateFrom = DateTime.Now;
        DateTime dateTo = DateTime.Now;
        int idLabel = 0;
       
        public frmPrognoseSelection()
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

        private void frmPrognoseSelection_Load(object sender, EventArgs e)
        {
            //=== sale ubacio sve kao prvu stavku comba
            dlLabel.Items.Add("Alle");
            dlLabel.Items[0].Value = 0;

            for (int i = 0; i < Login._arrLabels.Count; i++)
            {

                dlLabel.Items.Add(Login._arrLabels[i].nameLabel.ToString());
                dlLabel.Items[i+1].Value = Login._arrLabels[i].idLabel.ToString();
            }

            if (dlLabel.Items.Count > 0)
            {
                dlLabel.SelectedIndex = 0;
            }
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

                if (resxSet.GetString(lblLabel.Text) != null)
                    lblLabel.Text = resxSet.GetString(lblLabel.Text);

               
                if (resxSet.GetString(btnPrint.Text) != null)
                    btnPrint.Text = resxSet.GetString(btnPrint.Text);

                if (resxSet.GetString(btnCancel.Text) != null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);


            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
           DataTable dt1 = new DataTable();
           dateFrom = Convert.ToDateTime(dtDtFrom.Value);
           dateTo = Convert.ToDateTime(dtDtTo.Value);

           string labelName = dlLabel.SelectedItem.ToString();

           int index = dlLabel.SelectedIndex;
            string lebidLabelelId;
            //==== sale ubacio 19-5-2016 da moze da stampa za sve
            if (index == 0)
            {
                lebidLabelelId = "0";
                labelName = "Alle";
            }
            else
                lebidLabelelId = dlLabel.Items[index].Value.ToString();
           if (labelName != null)
           {
               idLabel = Convert.ToInt32(lebidLabelelId);
           }

         
         //  int idLabel = 1;
                model = new DataTable();
                ArrangementCalculationDAO lad = new ArrangementCalculationDAO();

                model = lad.GetACalculationRepotr(dateFrom, dateTo, idLabel);
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

                        DataColumn dc4 = new DataColumn("ParamLabel, typeof(string)");
                        dc4.ColumnName = "ParamLabel";
                        model.Columns.Add(dc4);

                        model.Rows[0]["ParamDateFrom"] = dateFrom.ToString("dd-MM-yyyy");
                        model.Rows[0]["ParamDateTo"] = dateTo.ToString("dd-MM-yyyy");
                        model.Columns.Add("ParametarDateFrom");
                        model.Rows[0]["ParametarDateFrom"] = dateFrom;
                        model.Columns.Add("ParametarDateTo");
                        model.Rows[0]["userName"] = Login._user.nameUser;
                        model.Rows[0]["ParamLabel"] = labelName;
                       


                    

                        frmPrognoseReport frm = new frmPrognoseReport(model, "nameString");
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
       
    

  

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        
    }
}
