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
    public partial class frmHotelServices : Telerik.WinControls.UI.RadForm
    {

        HotelServicesModel model;
        HotelServicesBUS aBUS = new HotelServicesBUS();
        public bool isChanged;

        public frmHotelServices()
        {
            
            InitializeComponent();
        }
        public frmHotelServices(HotelServicesModel hotelModel)
        {
            model = new HotelServicesModel();
            model = hotelModel;
            InitializeComponent();
        }

        private void frmHotelServices_Load(object sender, EventArgs e)
        {
            if (model != null)
            {
                txtName.Text = model.nameHotelService.ToString();
             
            }
            setTranslation();
        }
        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblName.Text) != null)
                    lblName.Text = resxSet.GetString(lblName.Text);
                if (resxSet.GetString(btnSave.Text)!=null)
                btnSave.Text = resxSet.GetString(btnSave.Text);
                if (resxSet.GetString(btnCancel.Text) != null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);             
            
            }
        }
        private int LastId()
        {
            int Lastid = 0;
            HotelServicesBUS bus = new HotelServicesBUS();
            List<HotelServicesModel> list = new List<HotelServicesModel>();
            list = bus.LastID();
            if (list != null)
            {
                if (list.Count > 0)
                {
                    Lastid = Convert.ToInt32(list[0].idHotelService);
                }
            }
            return Lastid + 1;

        }

     
        private void Update()
        {
           
            model.nameHotelService = txtName.Text;


            if (aBUS.Update(model.idHotelService,model.nameHotelService, this.Name, Login._user.idUser) != false)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You updated Hotel service successfully!");
            }
            else 
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
            }
        }
        private void Insert()
        {
          
                model = new HotelServicesModel();
                model.idHotelService= LastId();
                if (txtName.Text != "")
                    model.nameHotelService = txtName.Text;
               
                if (model.idHotelService == 0)
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Something went wrong with inserting!");
                }
                else
                {
                    if (aBUS.Save(model.idHotelService,model.nameHotelService, this.Name, Login._user.idUser) != false)
                    {
                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("You inserted Hotel service successfully!");
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


       
    }
}
