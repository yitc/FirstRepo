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
    public partial class frmAccountReports : Telerik.WinControls.UI.RadForm
    {
        AccountDetailView accountDetailView;


        public frmAccountReports()
        {
            InitializeComponent();

            this.Icon = Login.iconForm;
            
        }

        private void frmAccountReports_Load(object sender, EventArgs e)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(this.Text) != null)
                    this.Text = resxSet.GetString(this.Text);
            }
           
            accountDetailView = new AccountDetailView();
            accountDetailView.Dock = System.Windows.Forms.DockStyle.Fill;
            panelRepors.Controls.Add(accountDetailView);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
