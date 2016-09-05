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
    public partial class frmBoardingPoint : Telerik.WinControls.UI.RadForm
    {

        BoardingPointModel model = new BoardingPointModel();
        public frmBoardingPoint()
        {

            InitializeComponent();
        }

        private void frmBoardingPoint_Load(object sender, EventArgs e)
        {
            model.idBoardingPoint = -1;
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
                if (resxSet.GetString(txtAddressBP.Text) != null)
                    txtAddressBP.Text = resxSet.GetString(txtAddressBP.Text);

            }
        }
        private int LastId()
        {
            int Lastid = -1;
            BoardingPointDAO _bpDAO = new BoardingPointDAO();
            DataTable dataTable = _bpDAO.idBoardingPoint();
            if (dataTable.Rows.Count > 0)
            {
                Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
            }
            return Lastid + 1;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BoardingPointBUS _bpBUS = new BoardingPointBUS();
            if (model.idBoardingPoint == -1)
            {
                if (txtName.Text != null)
                {
                    if (txtAddressBP.Text.Length <= 100)
                    {
                        if (_bpBUS.Save(LastId(), txtName.Text, txtAddressBP.Text, this.Name, Login._user.idUser) != true)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Something went wrong with save!");
                          

                        }
                        else
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("You inserted data successfully!");
                            model.idBoardingPoint = LastId() - 1;
                            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                            this.Close();
                        }
                    }

                    else
                    {

                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Max address length is one hundred of characters!");
                    }
                }
                else
                {

                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Name boarding point is required!");
                }
            }
            else
            {
                if (txtAddressBP.Text.Length <= 100)
                {

                    if (_bpBUS.Update(model.idBoardingPoint, txtName.Text, txtAddressBP.Text, this.Name, Login._user.idUser) != false)
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You updated data successfully!");
                        this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Something went wrong with updating");

                    }
                }
                else
                {

                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Max address length is one hundred of characters!");
                }

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (model.idBoardingPoint != -1)
            {
                translateRadMessageBox t = new translateRadMessageBox();

                DialogResult dialog = t.translateAllMessageBoxDialog("Click Yes to delete these point.", "");


                if (dialog == DialogResult.Yes)
                {
                    BoardingPointBUS _bpBUS = new BoardingPointBUS();
                    bool isIn = false;
                    List<BoardingPointModel> listIsIn = new List<BoardingPointModel>();
                    listIsIn = _bpBUS.isInBoarding(model.idBoardingPoint);
                    for (int i = 0; i < listIsIn.Count; i++)
                    {
                        if (listIsIn[i].idBoardingPoint == model.idBoardingPoint)
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
                        if (_bpBUS.Delete(model.idBoardingPoint, this.Name, Login._user.idUser) != false)
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("You deleted data successfully!");
                            txtName.Text = "";
                            model.idBoardingPoint = -1;
                            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
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
                        tr.translateAllMessageBox("This boarding point cannot be deleting!");
                    }

                }
            }
            else
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("Selected boarding point!");
            }



        }

        private void btnBoardingPoint_Click(object sender, EventArgs e)
        {
            BoardingPointBUS boardingBUS = new BoardingPointBUS();
            List<IModel> gm3 = new List<IModel>();


            gm3 = boardingBUS.GetAll();


            var dlgSave = new GridLookupBoardingPoint(gm3, "BoardingPoint");



            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {

                BoardingPointModel genm3 = new BoardingPointModel();
                genm3 = (BoardingPointModel)dlgSave.selectedRow;
                model.idBoardingPoint = genm3.idBoardingPoint;
                txtName.Text = genm3.nameBoardingPoint;
                txtAddressBP.Text = genm3.addressBoardingPoint.ToString();


                txtName.Text = genm3.nameBoardingPoint.ToString();
            }

        }
    }
}
