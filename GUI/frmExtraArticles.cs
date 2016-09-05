using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Business;
using BIS.DAO;
using BIS.Model;
using GUI.ReportsForms;
using Telerik.WinControls.UI;


namespace GUI
{
    public partial class frmExtraArticles : Telerik.WinControls.UI.RadForm
    {
        DataTable model;
        private DateTime from;
        private DateTime to;
        private string name;
        private int user;
        List<LabelForArrangement> ArrangementLabel;
        public frmExtraArticles()
        {
            InitializeComponent();
            this.Icon = Login.iconForm;
            string nameForm = this.Text.Substring(0);

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(nameForm) != null)
                    nameForm = resxSet.GetString(nameForm);
            }
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + nameForm;
        }
        private Boolean saveLabels()
        {
            Boolean result = false;
            ArrangementLabel = new List<LabelForArrangement>();
            foreach (Control c in panelLabels.Controls)
            {
                RadCheckBox rch = (RadCheckBox)c;
                if (rch.Checked == true)
                {
                    LabelForArrangement lab = new LabelForArrangement();
                    lab.idLabel = Login._arrLabels.Find(item => item.nameLabel.TrimEnd() == rch.Text.TrimEnd()).idLabel;
                    ArrangementLabel.Add(lab);
                    result = true;
                }
            }
            return result;
        }
        private void frmExtraArticles_Load(object sender, EventArgs e)
        {
            dtFrom.Text = "";
            dtTo.Text = "";

            Translation();
            RadCheckBox rchk;
            int Y = 0;
            for (int i = 0; i < Login._arrLabels.Count; i++)
            {
                rchk = new RadCheckBox();
                rchk.Font = new Font("Verdana", 9);
                rchk.ThemeName = btnPrint.ThemeName;
                rchk.Name = "chkLabel" + Login._arrLabels[i].idLabel.ToString();
                rchk.Text = Login._arrLabels[i].nameLabel;
                rchk.Location = new Point(0, Y);
                rchk.AutoSize = true;
                Y = Y + 3 + rchk.Height;
                panelLabels.Controls.Add(rchk);
            }
        }

        private void Translation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblFromAccount.Text) != null)
                    lblFromAccount.Text = resxSet.GetString(lblFromAccount.Text);

                if (resxSet.GetString(lblToAccount.Text) != null)
                    lblToAccount.Text = resxSet.GetString(lblToAccount.Text);

                if (resxSet.GetString(btnPrint.Text) != null)
                    btnPrint.Text = resxSet.GetString(btnPrint.Text);

                if (resxSet.GetString(btnCancel.Text) != null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);

             
            }
        }
        #region butt
       

       

        private void btnPrint_Click(object sender, EventArgs e)
        {
            saveLabels();
            DataTable dt1 = new DataTable();
           
            from = dtFrom.Value;
            to = dtTo.Value;
               // maxDate= new DateTime();
               //if(dtFrom> )

                ExtraArticleDAO ead = new ExtraArticleDAO();
                int idLabel = 0;
                dt1 = ead.GetAllExtraArticle(from,to,ArrangementLabel,Login._user.nameUser);
                frmExtraArticlesReport frm = new frmExtraArticlesReport(dt1);
                frm.Show();                 
            //}
               
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion butt

    }
}
