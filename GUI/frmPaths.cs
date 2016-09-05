using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Model;
using System.Resources;
using BIS.Business;
using Telerik.WinControls.UI;
using Telerik.WinControls.Data;
using System.IO;
using BIS.DAO;
using Telerik.WinControls.UI.Export;


namespace GUI
{
    public partial class frmPaths : Telerik.WinControls.UI.RadForm
    {
        private int iID;
        public PathsModel model;
        public bool modelChanged = false;
        List<string> label = new List<string>(); //za labele
        public static List<PathsModel> _paths;
        //Paths
        private PathsBUS _pathsBUS;
        int sel;
       
        public frmPaths()
        {
           
            InitializeComponent();
        }

        private void frmParths_Load(object sender, EventArgs e)
        {
            
            //Paths            
            _pathsBUS = new PathsBUS();
            _paths = new List<PathsModel>();
            _paths = _pathsBUS.GetAllPaths();


            this.Icon = Login.iconForm;
            string name = "Path";

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(name) != null)
                    name = resxSet.GetString(name);
            }
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + name;

            setTranslation();

            int Y = 0;
           
            RadRadioButton rck = new RadRadioButton();            
                     
            Y = 15;

            for (int i = 0; i < _paths.Count; i++)
            {
                rck = new RadRadioButton();
                rck.Font = new Font("Verdana", 9);
                rck.Name = "chkPath" + _paths[i].idPath.ToString();
                rck.Text = _paths[i].namePath;
                rck.Location = new Point(0, Y);
                rck.CheckStateChanged += rck_CheckStateChanged;
                rck.AutoSize = true;               
                Y = Y + 3 + rck.Height;

                if(i==0)
                {
                    rck.CheckState =CheckState.Checked ;
                }
                panelPaths.Controls.Add(rck);
            }
            
        }

        private void rck_CheckStateChanged(object sender, EventArgs e)
        {  
            RadRadioButton rb = (RadRadioButton)sender;
            
            string id = rb.Name.Replace("chkPath", "");

            PathsModel model = new PathsModel();
            PathsBUS bus = new PathsBUS();

            if (id != "")
            {
                model = bus.GetAllPathsByID(id);
                txtPath.Text = model.path;
            }
            else
            {
                txtPath.Text = "";
            }

        }


         private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                if (resxSet.GetString(lblPath.Text) != null)
                    lblPath.Text = resxSet.GetString(lblPath.Text);

                if (resxSet.GetString(btnClose.Text) != null)
                    btnClose.Text = resxSet.GetString(btnClose.Text);

                if (resxSet.GetString(btnSave.Text) != null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);

            }
        }

         private void btnClose_Click(object sender, EventArgs e)
         {
             this.Close();
         }

         private void btnSave_Click(object sender, EventArgs e)
         {           
             int idPath = 0;
             foreach (Control c in panelPaths.Controls)
             {
                 RadRadioButton rbc = (RadRadioButton)c;
                 if (rbc.IsChecked == true)
                 {
                      idPath =Convert.ToInt32( rbc.Name.Replace("chkPath", ""));
                     break;
                 }                  
                
             }
     

             try
             {
                 PathsModel model = new PathsModel();
                 model.path = txtPath.Text;                
                 
                 PathsBUS bus = new PathsBUS();
                           
               
                bus.Update(txtPath.Text, this.Name, Login._user.idUser,idPath);

                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Save path!");
                
             }
             catch (Exception ex)
             {
                 RadMessageBox.Show("Error saving.\nMessage: " + ex.Message, "Save", MessageBoxButtons.OK, RadMessageIcon.Error);
             }
            
                 
         }

         private void btnGetPath_Click(object sender, EventArgs e)
         {
             //OpenFileDialog ofd = new OpenFileDialog();
             //if(ofd.ShowDialog()==System.Windows.Forms.DialogResult.OK)
             //{
             //    string strFileName = ofd.FileName;
             //    txtPath.Text = strFileName;
             //}
             FolderBrowserDialog fbs = new FolderBrowserDialog();
             if ((fbs.ShowDialog() == DialogResult.OK))
             {
                 txtPath.Text = fbs.SelectedPath;
             }
         }
    }
}
