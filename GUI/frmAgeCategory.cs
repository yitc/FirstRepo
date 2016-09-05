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
    public partial class frmAgeCategory : Telerik.WinControls.UI.RadForm
    {

        AgeCategoryModel model;
        AgeCategoryBUS aBUS = new AgeCategoryBUS();
        public bool isChanged;

        public frmAgeCategory()
        {
            
            InitializeComponent();
        }
        public frmAgeCategory(AgeCategoryModel ageModel)
        {
            model = new AgeCategoryModel();
            model = ageModel;
            InitializeComponent();
        }

        private void frmAgeCategory_Load(object sender, EventArgs e)
        {
            if (model != null)
            {
                txtDesciption.Text = model.descAgeCategory;
                txtMin.Text = model.minAgeCategory.ToString();
                txtMax.Text = model.maxAgeCategory.ToString();
            }

            txtMin.MaskedEditBoxElement.EnableMouseWheel = false;
            txtMax.MaskedEditBoxElement.EnableMouseWheel = false;

            setTranslation();
        }
        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblDescription.Text) != null)
                    lblDescription.Text = resxSet.GetString(lblDescription.Text);
                if (resxSet.GetString(btnSave.Text)!=null)
                btnSave.Text = resxSet.GetString(btnSave.Text);
                if (resxSet.GetString(btnCancel.Text) != null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);
                if (resxSet.GetString(lblMinAgeCategory.Text) != null)
                    lblMinAgeCategory.Text = resxSet.GetString(lblMinAgeCategory.Text);
                if (resxSet.GetString(lblMaxAgeCategory.Text) != null)
                    lblMaxAgeCategory.Text = resxSet.GetString(lblMaxAgeCategory.Text);
                if (resxSet.GetString(this.Text)!=null)
                this.Text = resxSet.GetString(this.Text);
            
            }
        }
        private int LastId()
        {
            int Lastid = 0;
            AgeCategoryBUS bus = new AgeCategoryBUS();
            List<AgeCategoryModel> list = new List<AgeCategoryModel>();
                list= bus.LastId();
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        Lastid = Convert.ToInt32(list[0].idAgeCategory);
                    }
                }
            return Lastid + 1;

        }

     
        private void Update()
        {
           
            model.descAgeCategory = txtDesciption.Text;
            model.minAgeCategory = Convert.ToInt32(txtMin.Text);
            model.maxAgeCategory = Convert.ToInt32(txtMax.Text);

            if (aBUS.Update(model,this.Name, Login._user.idUser) != false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated Age category successfully!");
            }
            else 
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
            }
        }
        private void Insert()
        {
          
                model = new AgeCategoryModel();
                model.idAgeCategory = LastId();
                if (txtDesciption.Text != "")
                    model.descAgeCategory = txtDesciption.Text;
                if (txtMin.Text != "")
                    model.minAgeCategory = Convert.ToInt32(txtMin.Text);


                if (txtMax.Text != "")
                    model.maxAgeCategory = Convert.ToInt32(txtMax.Text);
                if (model.idAgeCategory == 0)
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Something went wrong with inserting!");
                }
                else
                {
                    if (aBUS.Save(model, this.Name, Login._user.idUser) != false)
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You inserted Age category successfully!");
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

        private void txtMin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = true;
            }
        }

        private void txtMax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = true;
            }
        }


       
    }
}
