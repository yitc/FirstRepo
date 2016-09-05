using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BIS.Business;
using BIS.Model;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.Resources;
using BIS;
using BIS.DAO;

namespace GUI
{
    public partial class frmTravelData : Form
    {
        UsersBUS ubus = new UsersBUS();
        List<TranslateUstrModel> translate;
        string prevodioc = "";

        private CodeTrainingFromVolFeaturesModel type;
        private List<CodeTrainingFromVolFeaturesModel> fmodel;
        List<IModel> binding;
        public bool isChanged;
        List<LastIdModel> idList;

        private TravelDataDAO _travelDataDAO;
        private TravelDataBUS _travelDataBUS;
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        

        public frmTravelData()
        {
            InitializeComponent();
            lblCode.Visible = false;
        }

        public frmTravelData(IModel model)
        {
            InitializeComponent();

            type = (CodeTrainingFromVolFeaturesModel)model;

            if (type.type == "Arrangement type" || type.type == "Soort regeling" || type.type == "Arrangement status" || type.type == "Arrangement statuut")
            {
                txtCode.Visible = false;
                lblCode.Visible = false;
            }
            else
            {
                txtCode.Visible = true;
                txtCode.Text = type.code;
            }           
          
        }
        private void radButtonSave_Click(object sender, EventArgs e)
        {        
            //if(txtCode.Text=="")
            //{
            //    translateRadMessageBox trs = new translateRadMessageBox();
            //    trs.translateAllMessageBox("Enter code");
            //}
             
            //else
           // { 
             
            if (type != null)
                Update();

            if (type == null)
                Insert();
           // }
            
        }

        #region Insert
        
        private void InsertArrType()
        {
            _travelDataDAO = new TravelDataDAO();
            DataTable dataTable = _travelDataDAO.idArrangementType();
            if(dataTable.Rows.Count > 0)
            {
                int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                if (_travelDataBUS.InsertArrangement(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Something went wrong with inserting!");
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You inserted arrangement type successfully!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }

            }

            else // ukoliko u bazi nema upisa onda setuje vrednost na LastId 0 kako bi dodao slog
            {
                int Lastid = 0;
                if (_travelDataBUS.InsertArrangement(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Something went wrong with inserting!");
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You inserted arrangement type successfully!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
            }
        }

        private void InsertCertificates()
        {
            if (txtCode.Text == "")
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Enter code");
            }
            else
            {
                _travelDataDAO = new TravelDataDAO();
                DataTable dataTable = _travelDataDAO.idCertificateType();

                if (dataTable.Rows.Count > 0)
                {
                    int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                    if (_travelDataBUS.InsertCertificate(Lastid + 1, txtNewName.Text.ToString(), txtCode.Text.ToString(), this.Name, Login._user.idUser) == false)
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("Something went wrong with inserting!");
                    }

                    else
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You inserted certificates type successfully!");
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                }
                else // ukoliko u bazi nema upisa onda setuje vrednost na LastId 0 kako bi dodao slog
                {
                    int Lastid = 0;
                    if (_travelDataBUS.InsertCertificate(Lastid + 1, txtNewName.Text.ToString(), txtCode.Text.ToString(), this.Name, Login._user.idUser) == false)
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("Something went wrong with inserting!");
                    }
                    else
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You inserted certificates type successfully!");
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                }
            }
            
        }

        private void InsertTraining()
        {
            if (txtCode.Text == "")
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Enter code");
            }

            else
            {
                _travelDataDAO = new TravelDataDAO();
                DataTable dataTable = _travelDataDAO.idTrainingType();
                if (dataTable.Rows.Count > 0)
                {
                    int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                    if (_travelDataBUS.InsertTraining(Lastid + 1, txtNewName.Text.ToString(), txtCode.Text.ToString(), this.Name, Login._user.idUser) == false)
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("Something went wrong with inserting!");
                    }
                    else
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You inserted training type successfully!");
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                }

                else // ukoliko u bazi nema upisa onda setuje vrednost na LastId 0 kako bi dodao slog
                {
                    int Lastid = 0;
                    if (_travelDataBUS.InsertTraining(Lastid + 1, txtNewName.Text.ToString(), txtCode.Text.ToString(), this.Name, Login._user.idUser) == false)
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("Something went wrong with inserting!");
                    }
                    else
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You inserted training type successfully!");
                        this.DialogResult = DialogResult.Yes;
                        this.Close();
                    }
                }
            }
        }

        private void InsertArrangementStatus()
        {
            _travelDataDAO = new TravelDataDAO();
            DataTable dataTable = _travelDataDAO.idArrangementStatus();
            if (dataTable.Rows.Count > 0)
            {
                int Lastid = Convert.ToInt32(dataTable.Rows[0][0]);

                if (_travelDataBUS.InsertArrangementStatus(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Something went wrong with inserting!");
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You inserted arrangement status successfully!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }

            }

            else // ukoliko u bazi nema upisa onda setuje vrednost na LastId 0 kako bi dodao slog
            {
                int Lastid = 0;
                if (_travelDataBUS.InsertArrangementStatus(Lastid + 1, txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Something went wrong with inserting!");
                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You inserted arrangement status successfully!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
            }
        }

        #endregion
       

        #region Update
        private void UpdateArrType()
        {
            if (_travelDataBUS.UpdateArrType(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
            }
            else
            {
                
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated arr type successfully!");               
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

        private void UpdateCertificates()
        {
            if (txtCode.Text == "")
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Enter code");
            }
            else
            {
                if (_travelDataBUS.UpdateCertificate(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), txtCode.Text.ToString(), this.Name, Login._user.idUser) == false)
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Something went wrong with updating!");
                }

                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You updated certificates type successfully!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
            }
        }

        private void UpdateTraining()
        {
            if (txtCode.Text == "")
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Enter code");
            }

            else
            {
                if (_travelDataBUS.UpdateTraining(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), txtCode.Text.ToString(), this.Name, Login._user.idUser) == false)
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Something went wrong with updating!");
                }

                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You updated training type successfully!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
            }
        }

        private void UpdateArrangementStatus()
        {
            if (_travelDataBUS.UpdateArrangementStatus(Convert.ToInt32(type.ID), txtNewName.Text.ToString(), this.Name, Login._user.idUser) == false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
            }
            else
            {

                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated Arrangement status successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

       
        #endregion

        

        //public void Update()
        //{
        //    _travelDataBUS = new TravelDataBUS();
        //    type.name = txtNewName.Text;          
        //     translate = ubus.Translate("Arrangement type", Login._user.lngUser);

        //    if (type.type == "Arrangement type")
        //    {
        //        UpdateArrType();
        //    }

        //    if (type.type == "Certificates type")
        //    {
        //        UpdateCertificates();
        //    }

        //    if (type.type == "Training type")
        //    {
        //        UpdateTraining();
        //    }
        //}


        public void Update()
        {
            // Za update kod prevoda da dozvoli update
            _travelDataBUS = new TravelDataBUS();
            type.name = txtNewName.Text;

            translate = ubus.Translate("Arrangement type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {

                        UpdateArrType();
                    }
                }
            }
            

            translate = ubus.Translate("Certificates type", Login._user.lngUser);
            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {
                        UpdateCertificates();

                    }
                }
            }

            translate = ubus.Translate("Training type", Login._user.lngUser);

            if (translate != null)
            {
                if (translate.Count > 0)
                {
                    prevodioc = translate[0].stringValue;
                    if (type.type == prevodioc)
                    {
                        UpdateTraining();
                    }
                }
            }

            translate = ubus.Translate("Arrangement status", Login._user.lngUser);
            {
                if(translate != null)
                {
                    if(translate.Count > 0)
                    {
                        prevodioc = translate[0].stringValue;
                        if(type.type==prevodioc)
                        {
                            UpdateArrangementStatus();
                        }
                    }
                }
            }
        }
        // Za update kod prevoda da dozvoli update


        public void Insert()
        {
            _travelDataBUS = new TravelDataBUS();           
           

            if (ddlType.SelectedIndex != 0 && txtNewName.Text != "")
            {
                if (ddlType.SelectedIndex == 1) // index 1 u drop down listi je Arrangement
                {
                    InsertArrType();
                }               

                if (ddlType.SelectedIndex == 2) // index 2 u drop down listi je Certificates
                {

                    InsertCertificates();
                }

                if (ddlType.SelectedIndex == 3) // index 3 u drop down listi je Training
                {
                    InsertTraining();
                }

                if(ddlType.SelectedIndex == 4) // index 4 u drop down listi je Arrangement Status
                {
                    InsertArrangementStatus();
                }
            }
        }
       


        private void radButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                lblCode.Text = resxSet.GetString("Code Training / Certificate");
                
                if (btnSave.Text != null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);
                if (btnCancel.Text != null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);
                if (lblNew.Text != null)
                    lblNew.Text = resxSet.GetString(lblNew.Text);                

            }            

        }

        private void frmTravelData_Load(object sender, EventArgs e)
        {

            setTranslation();

            if (type != null)
            {
                ddlType.Visible = false;
                txtNewName.Text = type.name;
            }
        }
                              
            

        

        private void ddlType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if(ddlType.SelectedIndex == 2 || ddlType.SelectedIndex == 3)
            {
                txtCode.Visible = true;
                lblCode.Visible = true;
            }
            else
            {
                txtCode.Visible = false;
                lblCode.Visible = false;
            }
        }
    }
}
