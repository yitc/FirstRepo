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
    public partial class frmMultimediaServer : frmTemplate
    {
        MultimediaServersModel umodel = new MultimediaServersModel();
        MultimediaServerBUS mBUS=new MultimediaServerBUS();
      //  MultimediaServersModel MultimediaServer;
       List<MultimediaServersModel> msmodel;
        public frmMultimediaServer()
        {
            InitializeComponent();
          //  setTranslation();
            umodel = null;
        }
        public frmMultimediaServer(IModel model)
        {
            umodel = (MultimediaServersModel)model;
            InitializeComponent();
           
                  
        }

   

        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (lblPath.Text != null)
                    lblPath.Text = resxSet.GetString(lblPath.Text);
                if (lblFolder.Text != null)
                    lblFolder.Text = resxSet.GetString(lblFolder.Text);
                if (lblUsername.Text != null)
                    lblUsername.Text = resxSet.GetString(lblUsername.Text);
                if(lblPassword.Text!=null)
                    lblPassword.Text = resxSet.GetString(lblPassword.Text);
            }
        }

        private void UpdateData()
        {
            mBUS = new MultimediaServerBUS();
            int idServer = Convert.ToInt32(umodel.idServer);
            string path = txtPath.Text;
            string folder = txtFolder.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
          
            if (mBUS.Update(idServer, path, folder, username, password) == true)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You have update data successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong. Please contact your administrator!");
            }
        }
        private int GelLastIdServer()
        {
            msmodel = new List<MultimediaServersModel>();
            mBUS = new MultimediaServerBUS();
            msmodel = mBUS.GetLastIdServer();

            int rez = 0;

            if (msmodel.Count > 0)
            {
                rez = Convert.ToInt32(msmodel[0].idServer.ToString()) + 1;
                // rez = fmodel.
            }

            return rez;

        }

        private void InsertData()
        {
            mBUS = new MultimediaServerBUS();
            int idServer = GelLastIdServer();
            string path = txtPath.Text;
            string folder = txtFolder.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            umodel = new MultimediaServersModel();
            umodel.idServer = idServer;
            umodel.path = path;
            umodel.username = username;
            umodel.password = password;

            if (mBUS.Insert(idServer, path, folder, username, password) == true)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Insert data successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with inserting!");
            }
        
        }

     

        private void btnSave_Click(object sender, EventArgs e)
        {
         //   string id=umodel.idServer.ToString();
            if (umodel != null && umodel.idServer.ToString()!="0" )
            {
                UpdateData();
            }
            else
            {
                InsertData();
            }

        }
        private void Fill()
        {
            if (umodel != null)
            {
                if (umodel.path != null)
                    txtPath.Text = umodel.path.ToString();
                if (umodel.folder != null)
                    txtFolder.Text = umodel.folder.ToString();

                if (umodel.username != null)
                    txtUsername.Text = umodel.username.ToString();

                if (umodel.password != null)
                    txtPassword.Text = umodel.password.ToString();
            }
        }
        private void frmMultimediaServer_Load(object sender, EventArgs e)
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

            Fill();
        }
    }
}
