using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Model;
using BIS.DAO;
using BIS.Business;
using System.Resources;
using Microsoft;
using Telerik.WinControls.UI;
using System.Linq;

namespace GUI
{
    public partial class frmAccPayment : frmTemplate
    {
        private int iID;
        public AccPaymentModel model;
        public bool modelChanged = false;
        public string Namef;

        public frmAccPayment()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Payment");
            }
            ribbonExampleMenu.Text = "";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;

            InitializeComponent();
            iID = -1;
        }

        public frmAccPayment(int eID)
        {
            using(ResXResourceSet resxSet= new ResXResourceSet(Login.resxFile))
            {
                Namef=resxSet.GetString("Payment");
            }
            ribbonExampleMenu.Text="";

            this.Text=Login._companyModelList[0].nameCompany +  " " + DateTime.Now.Year.ToString() + " - " + Namef;
            iID=eID;
            InitializeComponent();
        }

        public frmAccPayment(AccPaymentModel emodel)
        {
            using (ResXResourceSet resxSet=new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Payment");
            }
            ribbonExampleMenu.Text = "";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            model = emodel;
            iID = model.idPayment;
            InitializeComponent();
        }

        private void frmAccPayment_Load(object sender, EventArgs e)
        {
            radRibbonDocuments.Visibility = ElementVisibility.Visible;
            btnNewDoc.Visibility = ElementVisibility.Collapsed;
            btnReport.Visibility = ElementVisibility.Collapsed;
            btnEmail.Visibility = ElementVisibility.Collapsed;
            btnWord.Visibility = ElementVisibility.Collapsed;
            btnNewMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteDoc.Visibility = ElementVisibility.Visible;
            btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;
            radRibbonMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;
            radRibbonDocuments.Text = "";
            

           

            if(iID != - 1 )
            {
                txtDescription.Text = model.description.ToString();
                txtNumberDays.Text = model.numberDays.ToString();

                if(model.isDebitor == true)
                {
                    chkIsDebitor.CheckState = CheckState.Checked;
                }

                if(model.isCreditor==true)
                {
                    chkIsCreditor.CheckState = CheckState.Checked;
                }
            }

            setTranslation();
        }

         
        
        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblNumberDays.Text) != null)
                    lblNumberDays.Text = resxSet.GetString(lblNumberDays.Text);

                if (resxSet.GetString(lblIsDebitor.Text) != null)
                    lblIsDebitor.Text = resxSet.GetString(lblIsDebitor.Text);

                if (resxSet.GetString(lblIsCreditor.Text) != null)
                    lblIsCreditor.Text = resxSet.GetString(lblIsCreditor.Text);

                if (resxSet.GetString(lblDescription.Text) != null)
                    lblDescription.Text = resxSet.GetString(lblDescription.Text);


              //  lblNumberDays.Text = resxSet.GetString("Number Days");
                //lblIsDebitor.Text = resxSet.GetString("Debitor");
                //lblIsCreditor.Text = resxSet.GetString("Creditor");
                //chkIsDebitor.Text = resxSet.GetString("Debitor");
                //chkIsCreditor.Text = resxSet.GetString("Creditor");
               // lblDescription.Text = resxSet.GetString("Description");


                btnSave.Text = resxSet.GetString("Save");
                btnDeleteDoc.Text = resxSet.GetString("Delete");
                btnDeleteMemo.Text = resxSet.GetString("Delete");
                
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AccPaymentBUS bus = new AccPaymentBUS();

            if(iID != -1 )
            {
                model.idPayment = iID;
                model.description = txtDescription.Text;
               

                if(txtNumberDays.Text != "")
                {
                    model.numberDays = Convert.ToInt32(txtNumberDays.Text);
                }

                if(chkIsDebitor.Checked==true)
                {
                    model.isDebitor = true;
                }
                else
                {
                    model.isDebitor = false;
                }

                if(chkIsCreditor.Checked==true)
                {
                    model.isCreditor = true;
                }
                else
                {
                    model.isCreditor = false;
                }

                if (bus.Update(model, this.Name, Login._user.idUser) == true)
                {
                    modelChanged = true;
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        if (resxSet.GetString("Saved") != null)
                        {
                            RadMessageBox.Show(resxSet.GetString("Saved"));
                        }
                        else
                        {
                            RadMessageBox.Show("Saved");
                        }
                    }
                }
            }


            else
            {
                if(txtNumberDays.Text== " ")
                {
                    RadMessageBox.Show("Can't SAVE without Number Days ! ");
                    txtNumberDays.Focus();
                    return;
                }

                AccPaymentModel model = new AccPaymentModel();

                model.description = txtDescription.Text;

                if(txtNumberDays.Text != " ")
                { 
                model.numberDays = Convert.ToInt32(txtNumberDays.Text);
                }

                if(chkIsDebitor.Checked == true)
                {
                    model.isDebitor = true;
                }
                else
                {
                    model.isDebitor = false;
                }

                if(chkIsCreditor.Checked==true)
                {
                    model.isCreditor = true;
                }
                else
                {
                    model.isCreditor = false;
                }

                if (bus.Save(model, this.Name, Login._user.idUser) == true)
                {
                    modelChanged = true;

                    using (ResXResourceSet resxSet=new ResXResourceSet(Login.resxFile))
                    if(resxSet.GetString("Insert") != null)
                    {
                        RadMessageBox.Show(resxSet.GetString("Insert"));
                    }
                    else
                    {
                        RadMessageBox.Show("Insert");
                    }
                    this.Close();
                }
            }
            modelChanged = true;
        }

        //private void btnDeleteDoc_Click(object sender, EventArgs e)
        //{
        //    DialogResult dr = RadMessageBox.Show("Do you want to DELETE this payment ?", "Delete", MessageBoxButtons.YesNo);
        //    if (dr == DialogResult.Yes)
        //    {
        //        if (iID != -1)
        //        {
        //            AccPaymentBUS db = new AccPaymentBUS();
        //            db.Delete(iID);
        //            this.Close();
        //            modelChanged = true;
        //        }
        //    }
        //}
        private void btnDeleteDoc_Click(object sender, EventArgs e)
        {
            translateRadMessageBox tr = new translateRadMessageBox();
            if (tr.translateAllMessageBoxDialog("Do you want to DELETE this payment?", "Delete") == System.Windows.Forms.DialogResult.Yes)
            {
                if (iID != -1)
                {
                    AccPaymentBUS db = new AccPaymentBUS();
                    db.Delete(iID, this.Name, Login._user.idUser);

                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You have delete successfully!");

                    this.Close();
                    modelChanged = true;
                }
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You are not deleting this line!");
                return;

            }
        }
    }
    
}
