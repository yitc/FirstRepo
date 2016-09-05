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

namespace GUI
{
    public partial class frmPeriod: Telerik.WinControls.UI.RadForm
    {

        PeriodModel model;
        PeriodBUS aBUS = new PeriodBUS();
        public bool isChanged;

        public frmPeriod()
        {
            
            InitializeComponent();
        }
        public frmPeriod(PeriodModel periodModel)
        {
            model = new PeriodModel();
            model = periodModel;
            InitializeComponent();
        }

        private void frmPeriod_Load(object sender, EventArgs e)
        {
            if (model != null)
            {
                txtDescription.Text = model.descPeriod.ToString();
                txtMonthFrom.Text = model.monthFrom.ToString();
                txtMonthTo.Text = model.monthTo.ToString();
             
            }

            txtMonthFrom.MaskedEditBoxElement.EnableMouseWheel = false;
            txtMonthTo.MaskedEditBoxElement.EnableMouseWheel = false;

            setTranslation();
        }
        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblMonthTo.Text) != null)
                    lblMonthTo.Text = resxSet.GetString(lblMonthTo.Text);
                if (resxSet.GetString(lblMonthFrom.Text) != null)
                    lblMonthFrom.Text = resxSet.GetString(lblMonthFrom.Text);
                if (resxSet.GetString(lblDescription.Text) != null)
                    lblDescription.Text = resxSet.GetString(lblDescription.Text);
                if (resxSet.GetString(btnSave.Text)!=null)
                btnSave.Text = resxSet.GetString(btnSave.Text);
                if (resxSet.GetString(btnCancel.Text) != null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);
                this.Text = resxSet.GetString(this.Text);
            
            }
        }
        private int LastId()
        {
            int Lastid = 0;
            PeriodBUS bus = new PeriodBUS();
            List<PeriodModel> list = new List<PeriodModel>();
            list = bus.LastId();
            if (list != null)
            {
                if (list.Count > 0)
                {
                    Lastid = Convert.ToInt32(list[0].idPeriod);
                }
            }
            return Lastid + 1;

        }

     
        private void Update()
        {

            model.descPeriod = txtDescription.Text;
            model.monthFrom=Convert.ToInt32(txtMonthFrom.Text);
            model.monthTo= Convert.ToInt32(txtMonthTo.Text);

            if (aBUS.Update(model.idPeriod, model.monthFrom, model.monthTo, model.descPeriod, this.Name, Login._user.idUser) != false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated Period successfully!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
            }
        }
        private void Insert()
        {

            model = new PeriodModel();
            model.idPeriod = LastId();
            if (txtDescription.Text != "")
                model.descPeriod = txtDescription.Text;
            if (txtMonthFrom.Text != "")
                model.monthFrom = Convert.ToInt32(txtMonthFrom.Text);
            if (txtMonthTo.Text != "")
                model.monthTo= Convert.ToInt32(txtMonthTo.Text);

            if (model.idPeriod == 0)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with inserting!");
            }
            else
            { //int idPeriod, int monthFrom, int monthTo, string descPeriod)
                if (aBUS.Save(model.idPeriod, model.monthFrom, model.monthTo, model.descPeriod, this.Name, Login._user.idUser) != false)
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You inserted Period successfully!");
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Something went wrong with inserting!");
                }
            }

            }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (model != null)
                Update();
            if (model == null)
                Insert();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void txtMonthFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Up || e.KeyCode==Keys.Down)
            {
                e.Handled = true;
            }
        }

        private void txtMonthTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = true;
            }
        }


       
    }
}
