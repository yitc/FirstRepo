using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using BIS.Business;
using BIS.Model;
using System.Resources;

namespace GUI
{
    public partial class frmArrangementInsurancePremie : RadForm
    {
        public string Namef = "Arrangement Insurance Premie";

        List<string> codeInsuranceList;
        List<string> premieList;

        ArrangementInsurancePremieBUS busI;
        ArrangementInsurancePremieModel insuranceModel = null;

        public frmArrangementInsurancePremie()
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

        public frmArrangementInsurancePremie(ArrangementInsurancePremieModel model)
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

        private void frmArrangementInsurancePremie_Load(object sender, EventArgs e)
        {
            busI = new ArrangementInsurancePremieBUS();

            codeInsuranceList = new List<string>();
            codeInsuranceList.Add("NL");
            codeInsuranceList.Add("EU");
            codeInsuranceList.Add("NON-EU");
            dropdownCode.DataSource = codeInsuranceList;

            premieList = new List<string>();
            premieList.Add("Premie 1");
            premieList.Add("Premie 2");
            dropdownPremie.DataSource = premieList;

            maskedAmmount.MaskedEditBoxElement.EnableMouseWheel = false;

            if (insuranceModel != null)
            {                
                int n = dropdownCode.FindStringExact(insuranceModel.codeInsurance);
                if (n >= 0)
                    dropdownCode.SelectedIndex = n;

                n = dropdownPremie.FindStringExact(insuranceModel.premie);
                if (n >= 0)
                    dropdownPremie.SelectedIndex = n;

                maskedAmmount.Value = insuranceModel.amountPremie;

                dtValidFrom.Value = (DateTime)insuranceModel.dtValidFrom;
                dtValidTo.Value = (DateTime)insuranceModel.dtValidTo;
            }

            setTranslate();
        }

        private void setTranslate()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblPremie.Text) != null)
                    lblPremie.Text = resxSet.GetString(lblPremie.Text);
                if (resxSet.GetString(lblCodeInsurance.Text) != null)
                    lblCodeInsurance.Text = resxSet.GetString(lblCodeInsurance.Text);
                if (resxSet.GetString(lblAmmount.Text) != null)
                    lblAmmount.Text = resxSet.GetString(lblAmmount.Text);
                if (resxSet.GetString(chkSport.Text) != null)
                    chkSport.Text = resxSet.GetString(chkSport.Text);
                if (resxSet.GetString(chkMedicalDevices.Text) != null)
                    chkMedicalDevices.Text = resxSet.GetString(chkMedicalDevices.Text);
                if (resxSet.GetString(btnSave.Text) != null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);

                if (resxSet.GetString(lblDateFrom.Text) != null)
                    lblDateFrom.Text = resxSet.GetString(lblDateFrom.Text);
                if (resxSet.GetString(lblDateTo.Text) != null)
                    lblDateTo.Text = resxSet.GetString(lblDateTo.Text);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            decimal val = Convert.ToDecimal(maskedAmmount.Value);
            if (val > 0)
            {
                if (insuranceModel == null)
                {
                    ArrangementInsurancePremieModel model = new ArrangementInsurancePremieModel();
                    model.amountPremie = Convert.ToDecimal(maskedAmmount.Value);
                    model.codeInsurance = dropdownCode.Text;
                    model.premie = dropdownPremie.Text;
                    model.dtValidFrom = dtValidFrom.Value;
                    model.dtValidTo = dtValidTo.Value;

                    bool b = busI.Save(model, this.Name, Login._user.idUser);

                    if (b == false)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Error: Data not saved.");
                        return;
                    }
                }
                else
                {
                    insuranceModel.premie = dropdownPremie.Text;
                    insuranceModel.codeInsurance = dropdownCode.Text;
                    insuranceModel.amountPremie = Convert.ToDecimal(maskedAmmount.Value);
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
