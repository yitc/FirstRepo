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
    public partial class frmReason : Telerik.WinControls.UI.RadForm
    {

        ReasonModel model;
        ReasonBUS aBUS = new ReasonBUS();
        public bool isChanged;
        List<TranslateUstrModel> translate;
        string prevodioc = "";
        UsersBUS ubus = new UsersBUS();

        public frmReason()
        {

            InitializeComponent();
        }
        public frmReason(IModel reason)
        {
            model = (ReasonModel)reason;

            InitializeComponent();
        }

        private void frmReason_Load(object sender, EventArgs e)
        {
            if (model != null)
            {

                if (model.type.ToString() != null)
                {
                    ddlType.SelectedText = model.type.ToString();
                    ddlType.ReadOnly = true;
                }
                txtName.Text = model.name.ToString();

            }
            setTranslation();
        }
        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblName.Text) != null)
                    lblName.Text = resxSet.GetString(lblName.Text);
                if (resxSet.GetString(lblName.Text) != null)
                    lblType.Text = resxSet.GetString(lblType.Text);
                if (resxSet.GetString(btnSave.Text) != null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);
                if (resxSet.GetString(btnCancel.Text) != null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);

            }
        }

        # region Update
        private void UpdateContactPersonReasonOut()
        {
            if (aBUS.UpdateContactPersonReasonOut(model.ID, model.name, this.Name, Login._user.idUser) != false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated Contact person reason in successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
               
            }
        }
        private void UpdateContactPersonReasonIn()
        {
            if (aBUS.UpdateContactPersonReasonIn(model.ID, model.name, this.Name, Login._user.idUser) != false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated Contact person reason in successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
      
            }
        }
        private void UpdateVoluntaryReasonIn()
        {
            if (aBUS.UpdateVoluntaryReasonIn(model.ID, model.name, this.Name, Login._user.idUser) != false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated Voluntary reason in successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
       
            }
        }
        private void UpdateVoluntaryReasonOut()
        {
            if (aBUS.UpdateVoluntaryReasonOut(model.ID, model.name, this.Name, Login._user.idUser) != false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated Voluntary reason out successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }

            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
      
            }

        }
        # endregion


        # region Insert


        private void InsertContactPersonReasonOut()
        {

            if (aBUS.InsertContactPersonReasonOut(txtName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with inserting!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You inserted reason out successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }

        }
        private void InsertContactPersonReasonIn()
        {

            if (aBUS.InsertContactPersonReasonIn(txtName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with inserting!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You inserted reason in successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }

        }

        private void InsertVoluntaryReasonIn()
        {

            if (aBUS.InsertVoluntaryReasonIn(txtName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with inserting!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You inserted  voluntary reason in successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }

        }

        private void InsertVoluntaryReasonOut()
        {

            if (aBUS.InsertVoluntaryReasonOut(txtName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with inserting!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You inserted  voluntary reason out successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }

        }
        # endregion
        private void Update()
        {

            model.name = txtName.Text;


            aBUS = new ReasonBUS();


            translate = ubus.Translate("Contact person reason out", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (model.type == prevodioc)
                    {

                        UpdateContactPersonReasonOut();
                    }
                }
            }


            translate = ubus.Translate("Contact person reason out", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (model.type == prevodioc)
                    {

                        UpdateContactPersonReasonIn();
                    }
                }
            }

            translate = ubus.Translate("Voluntary reason in", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (model.type == prevodioc)
                    {

                        UpdateVoluntaryReasonIn();
                    }
                }
            }
            translate = ubus.Translate("Voluntary reason out", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (model.type == prevodioc)
                    {

                        UpdateVoluntaryReasonOut();
                    }
                }
            }
        }
        private void Insert()
        {

            if (ddlType.SelectedIndex != 0 && txtName.Text != "")
            {

                if (ddlType.SelectedIndex == 1)// indeks 1 u drop down listi je Contact person reason out
                {
                    InsertContactPersonReasonOut();
                }

                if (ddlType.SelectedIndex == 2)// indeks 2 u drop down listi je Contact person reason in
                {
                    InsertContactPersonReasonIn();
                }

                if (ddlType.SelectedIndex == 3)// indeks 4 u drop down listi je Voluntary reason out
                {
                    InsertVoluntaryReasonOut();
                }

                if (ddlType.SelectedIndex == 4)// indeks 3 u drop down listi je Voluntary reason in
                {
                    InsertVoluntaryReasonIn();
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



    }

}