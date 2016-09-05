using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using BIS.Business;
using BIS.Model;

namespace GUI
{
    public partial class frmArrangementInsurance : RadForm
    {
        public string Namef = "Arrangement Insurance";

        List<string> codeInsuranceList;

        ArrangementInsuranceBUS busI;
        ArrangementInsuranceModel insuranceModel = null;

        public frmArrangementInsurance()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(Namef) != null)
                    Namef = resxSet.GetString(Namef);
            }
            insuranceModel = null;

            InitializeComponent();

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            this.Icon = Login.iconForm;
        }

        public frmArrangementInsurance(ArrangementInsuranceModel model)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(Namef) != null)
                    Namef = resxSet.GetString(Namef);
            }

            insuranceModel = model;

            InitializeComponent();

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            this.Icon = Login.iconForm;
        }
        private void frmArrangementInsurance_Load(object sender, EventArgs e)
        {
            busI = new ArrangementInsuranceBUS();

            codeInsuranceList = new List<string>();
            codeInsuranceList.Add("NL");
            codeInsuranceList.Add("EU");
            codeInsuranceList.Add("NON-EU");
            dropdownCode.DataSource = codeInsuranceList;

            maskedAmmount.MaskedEditBoxElement.EnableMouseWheel = false;


            List<ArrangementInsuranceLabelModel> labelList = busI.GetUniqueLabelNames();
            if(labelList != null)
            {
                dropdownLabel.DataSource = labelList;
                dropdownLabel.ValueMember = "Name";
                dropdownLabel.DisplayMember = "Name";
            }

            if(insuranceModel != null)
            {
                //dropdownLabel.SelectedText = insuranceModel.labelInsurance;
                //dropdownCode.SelectedText = insuranceModel.codeInsurance;

                int n = dropdownCode.FindStringExact(insuranceModel.codeInsurance);
                if(n >= 0)
                    dropdownCode.SelectedIndex = n;

                n = dropdownLabel.FindStringExact(insuranceModel.labelInsurance);
                if (n >= 0)
                    dropdownLabel.SelectedIndex = n;

                maskedAmmount.Value = insuranceModel.amountInsurance;
                dtValidFrom.Value = (DateTime) insuranceModel.dtValidFrom;
                dtValidTo.Value = (DateTime)insuranceModel.dtValidTo;
            }

            setTranslate();
        }

        private void setTranslate()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblLabelInsurance.Text) != null)
                    lblLabelInsurance.Text = resxSet.GetString(lblLabelInsurance.Text);
                if (resxSet.GetString(lblCodeInsurance.Text) != null)
                    lblCodeInsurance.Text = resxSet.GetString(lblCodeInsurance.Text);
                if (resxSet.GetString(lblAmmount.Text) != null)
                    lblAmmount.Text = resxSet.GetString(lblAmmount.Text);
                if (resxSet.GetString(lblDateFrom.Text) != null)
                    lblDateFrom.Text = resxSet.GetString(lblDateFrom.Text);
                if (resxSet.GetString(lblDateTo.Text) != null)
                    lblDateTo.Text = resxSet.GetString(lblDateTo.Text);
                if (resxSet.GetString(btnSave.Text) != null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);
            }
        }

      
        private void btnSave_Click(object sender, EventArgs e)
        {            
            decimal val = Convert.ToDecimal(maskedAmmount.Value);
            if(val > 0)
            {
                if(insuranceModel == null)
                {
                    ArrangementInsuranceModel model = new ArrangementInsuranceModel();
                    model.amountInsurance = Convert.ToDecimal(maskedAmmount.Value);
                    model.codeInsurance = dropdownCode.Text;
                    model.labelInsurance = dropdownLabel.Text;
                    model.dtValidFrom = dtValidFrom.Value;
                    model.dtValidTo = dtValidTo.Value;
                    bool b = busI.Save(model, this.Name, Login._user.idUser);

                    if(b == false)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Error: Data not saved.");
                        return;
                    }
                }
                else
                {
                    insuranceModel.labelInsurance = dropdownLabel.Text;
                    insuranceModel.codeInsurance = dropdownCode.Text;
                    insuranceModel.amountInsurance = Convert.ToDecimal(maskedAmmount.Value);
                    insuranceModel.dtValidFrom = dtValidFrom.Value;
                    insuranceModel.dtValidTo = dtValidTo.Value;

                    bool b = busI.Update(insuranceModel, this.Name, Login._user.idUser);
                    if (b == false)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Error: Data not saved.");
                        return;
                    }
                }

                this.Close();
            }
            else
            {
                translateRadMessageBox msgox = new translateRadMessageBox();
                msgox.translateAllMessageBox("Please enter ammount");
            }
        }

        private void maskedAmmount_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Up || e.KeyCode==Keys.Down)
            {
                e.Handled = true;
            }
        }
    }
}
