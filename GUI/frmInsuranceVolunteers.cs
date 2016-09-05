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
    public partial class frmInsuranceVolunteers : Telerik.WinControls.UI.RadForm
    {
        DataTable model;
        private DateTime from;
        private DateTime to;
        private string name;
        private int user;

        public frmInsuranceVolunteers()
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

        private void frmInsuranceVolunteers_Load(object sender, EventArgs e)
        {
            //dtFrom.Text = "";
            //dtTo.Text = "";

            Translation();
        }

        private void Translation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblFromAccount.Text) != null)
                    lblFromAccount.Text = resxSet.GetString(lblFromAccount.Text);

                if (resxSet.GetString(lblToAccount.Text) != null)
                    lblToAccount.Text = resxSet.GetString(lblToAccount.Text);

                if (resxSet.GetString(btnPrint.Text) != null)
                    btnPrint.Text = resxSet.GetString(btnPrint.Text);

                if (resxSet.GetString(btnCancel.Text) != null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);

             
            }
        }
        #region butt
       

       

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
           
            from = dtFrom.Value;
            to = dtTo.Value;
               // maxDate= new DateTime();
               //if(dtFrom> )

                VoluntaryInsuranceDAO vid = new VoluntaryInsuranceDAO();

                dt1 = vid.GetAllVoluntaryInsurance(from,to,Login._user.nameUser);
                frmInsuranceVolunteersReport frm = new frmInsuranceVolunteersReport(dt1, from, to);
                frm.Show();                 
            //}
               
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion butt

    }
}
