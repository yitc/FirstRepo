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
    public partial class frmThemeTrip : Telerik.WinControls.UI.RadForm
    {
        bool isEdit;
        ThemeTripModel model = new ThemeTripModel();
        public frmThemeTrip()
        {
            InitializeComponent();

        }

        private void frmThemeTrip_Load(object sender, EventArgs e)
        {
            model.idThemeTrip = -1;
            setTranslation();
        }
        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblName.Text) != null)
                    lblName.Text = resxSet.GetString(lblName.Text);
                if (resxSet.GetString(btnSave.Text) != null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);
                if (resxSet.GetString(btnCancel.Text) != null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);
                if (resxSet.GetString(btnDelete.Text) != null)
                    btnDelete.Text = resxSet.GetString(btnDelete.Text);
            }
        }
        private int LastId()
        {
            int Lastid = -1;
            ThemeTripDAO _bpDAO = new ThemeTripDAO();
            DataTable dataTable = _bpDAO.idThemeTrip();
            if (dataTable.Rows.Count > 0)
            {
                Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
            }
            return Lastid + 1;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            ThemeTripBUS _bpBUS = new ThemeTripBUS();
            if (model.idThemeTrip == -1)
            {
                if (txtName.Text != null)
                {
                    if (_bpBUS.Save(LastId(), txtName.Text, this.Name, Login._user.idUser) != true)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Something went wrong with save!");

                    }
                    else{
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You inserted data successfully!");
                        model.idThemeTrip = LastId() - 1;
                        this.Close();
                    }
                }
                else
                {

                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Name theme trip is required!");
                }
            }
            else 
            {

                if (_bpBUS.Update(model.idThemeTrip, txtName.Text, this.Name, Login._user.idUser) != false)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You updated data successfully!");
                }
                else 
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Something went wrong with updating!");

                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (model.idThemeTrip != -1)
            {
                translateRadMessageBox t = new translateRadMessageBox();
                DialogResult dialog = t.translateAllMessageBoxDialog("Click Yes to delete these theme.", "");


                if (dialog == DialogResult.Yes)
                {
                    ThemeTripBUS _bpBUS = new ThemeTripBUS();
                     bool isIn=false;
                    List<ThemeTripModel> listIsIn = new  List<ThemeTripModel>();
                    listIsIn=_bpBUS.isInTheme(model.idThemeTrip);
                    for(int i=0; i<listIsIn.Count;i++)
                    {
                        if (listIsIn[i].idThemeTrip == model.idThemeTrip)
                        {
                            isIn = true;
                            break;
                        }
                        else 
                        {
                            isIn = false;
                        }

                    }
                    if (listIsIn.Count == 0)
                    {
                        isIn = false;
                    }
                    if (isIn == false)
                    {


                        if (_bpBUS.Delete(model.idThemeTrip, this.Name, Login._user.idUser) != false)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("You deleted data successfully!");
                            txtName.Text = "";
                            model.idThemeTrip = -1;
                            this.Close();
                        }
                        else
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Something went wrong with deleting!");
                        }
                    }
                    else 
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("This theme trip cannot be deleting!");
                    }
                }
            }
            else
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Selected theme trip!");
            }
              
            


        }

        private void btnThemeTrip_Click(object sender, EventArgs e)
        {
            ThemeTripBUS themeBUS = new ThemeTripBUS();
            List<IModel> gm3 = new List<IModel>();


            gm3 = themeBUS.GetAllThemeTrip();


            var dlgSave = new GridLookupThemeTrip(gm3, "ThemeTrip");


            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {

                ThemeTripModel genm3 = new ThemeTripModel();
                genm3 = (ThemeTripModel)dlgSave.selectedRow;
                model.idThemeTrip = genm3.idThemeTrip;
                txtName.Text = genm3.nameThemeTrip;
              

                txtName.Text = genm3.nameThemeTrip.ToString();
                
            }





        }
    }
}
