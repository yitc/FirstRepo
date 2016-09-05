using BIS.Business;
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
    public partial class frmMenus : frmTemplate
        //Telerik.WinControls.UI.RadForm
    {
        MenuRoleModel umodel = new MenuRoleModel();
        MenuModel menu = new MenuModel();
        int broj = -1;
        public frmMenus()
        {
            InitializeComponent();
        }
        public frmMenus(MenuRoleModel menu)
        {
            umodel = (MenuRoleModel)menu;
            menu.idMenu = umodel.idMenu;
             broj = umodel.idMenu;

            
            InitializeComponent();
        }

        //public frmMenus()
        //{
        //    InitializeComponent();
        //}

        private void btnUpload_Click(object sender, EventArgs e)
        {
        
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"; 
            dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please select an image file to encrypt.";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                        //Get the file's path
                        var filePath = dialog.FileName;
                        //save image
                        picUser.Image = Image.FromFile(dialog.FileName);
            }       
        
        }
    

        private void btnDeleteUpload_Click(object sender, EventArgs e)
        {
            picUser.Image = null;
              //  GUI.Properties.Resources.DefaultPerson;
        }

        private void btnDeleteNew_Click(object sender, EventArgs e)
        {
            picNew.Image=null;
        }

        private void btnDeleteDelete_Click(object sender, EventArgs e)
        {
            picDelete.Image = null;
        }


        private void frmMenus_Load(object sender, EventArgs e)
        
        {
            
            radRibbonMemo.Visibility = ElementVisibility.Collapsed;
            btnReport.Visibility = ElementVisibility.Collapsed;
            btnEmail.Visibility = ElementVisibility.Collapsed;
            btnWord.Visibility = ElementVisibility.Collapsed;
            btnNewMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteDoc.Visibility = ElementVisibility.Visible;
            btnNewDoc.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;
            btnDeleteDoc.Visibility = ElementVisibility.Collapsed;
            radRibbonDocuments.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;
            setTranslation();
            MenuPictureBUS bus = new MenuPictureBUS();
            object objImage = bus.GetImage(umodel.idMenu);
            string strImage = "";
            if (objImage != null && objImage != DBNull.Value)
                strImage = (string)objImage;
            if (strImage != "")
            {
                BIS.Core.ImageDB im = new BIS.Core.ImageDB();
                picUser.BackgroundImage = null;
                picUser.Image = im.BytesToImage(Convert.FromBase64String(strImage));
            }
            // picture NewMenu
             bus = new MenuPictureBUS();
            object objImageNew = bus.GetImageNew(umodel.idMenu);
            string strImageNew = "";
            if (objImageNew != null && objImageNew != DBNull.Value)
                strImageNew = (string)objImageNew;
            if (strImageNew != "")
            {
                BIS.Core.ImageDB im = new BIS.Core.ImageDB();
                picNew.BackgroundImage = null;
                picNew.Image = im.BytesToImage(Convert.FromBase64String(strImageNew));
            }
            // picture DeleteMenu
            bus = new MenuPictureBUS();
            object objImageDelete = bus.GetImageDelete(umodel.idMenu);
            string strImageDelete = "";
            if (objImageDelete != null && objImageDelete != DBNull.Value)
                strImageDelete = (string)objImageDelete;
            if (strImageDelete != "")
            {
                BIS.Core.ImageDB im = new BIS.Core.ImageDB();
                picDelete.BackgroundImage = null;
                picDelete.Image = im.BytesToImage(Convert.FromBase64String(strImageDelete));
            }
        }

        private void btnUploadNew_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please select an image file to encrypt.";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //Get the file's path
                var filePath = dialog.FileName;
                //save image
                picNew.Image = Image.FromFile(dialog.FileName);
            }       
        

        }
        private void Save()
        {
            umodel = new MenuRoleModel();
           // picUser.Image != GUI.Properties.Resources.DefaultPerson &&
            if ( picUser.Image != null)
            {
                BIS.Core.ImageDB im = new BIS.Core.ImageDB();
                menu.imageMenu = Convert.ToBase64String(im.ImageToBytes(picUser.Image));
            }
            else 
            {
                menu.imageMenu = "";            
            }
            if ( picNew.Image != null)
            {
                BIS.Core.ImageDB im = new BIS.Core.ImageDB();
                menu.imageNew = Convert.ToBase64String(im.ImageToBytes(picNew.Image));
            }
            else 
            {
                menu.imageNew = "";
            }
            if ( picDelete.Image != null)
            {
                BIS.Core.ImageDB im = new BIS.Core.ImageDB();
                menu.imageDelete = Convert.ToBase64String(im.ImageToBytes(picDelete.Image));
            }
            else 
            {
                menu.imageDelete = "";
            }
        }

        private void btnUploadDelete_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please select an image file to encrypt.";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //Get the file's path
                var filePath = dialog.FileName;
                //save image
                picDelete.Image = Image.FromFile(dialog.FileName);
            }       
        }

 
        private void btnSave_Click(object sender, EventArgs e)
        {
            MenuPictureBUS bus = new MenuPictureBUS();
           int a= broj;
            Save();
            if (bus.UpdateImage(a, menu.imageMenu, menu.imageNew, menu.imageDelete, this.Name, Login._user.idUser) == true)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Update images successfuly!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with updating!");
            }
        }
        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (lblMenu.Text != null)
                {
                    if (resxSet.GetString(lblMenu.Text)!=null)
                    lblMenu.Text = resxSet.GetString(lblMenu.Text);
                }
                if (lblNewMenu.Text != null)
                {
                    if (resxSet.GetString(lblNewMenu.Text)!=null)
                    lblNewMenu.Text = resxSet.GetString(lblNewMenu.Text);
                }
                if (lblDelete.Text != null)
                {
                    if (resxSet.GetString(lblDelete.Text)!=null)
                    lblDelete.Text = resxSet.GetString(lblDelete.Text);
                }


                if (btnUpload.Text != null)
                    btnUpload.Text = resxSet.GetString(btnUpload.Text);
                if (btnDeleteUpload.Text != null)
                    btnDeleteUpload.Text = resxSet.GetString(btnDeleteUpload.Text);

                if (btnUploadNew.Text != null)
                    btnUploadNew.Text = resxSet.GetString(btnUploadDelete.Text);
                if (btnDeleteNew.Text != null)
                    btnDeleteNew.Text = resxSet.GetString(btnDeleteNew.Text);

              
                if (btnUploadDelete.Text != null)
                    btnUploadDelete.Text = resxSet.GetString(btnUploadDelete.Text);
                if (btnDeleteDelete.Text != null)
                    btnDeleteDelete.Text = resxSet.GetString(btnDeleteDelete.Text);
            }
        }

    

     
      

       
    }
}
