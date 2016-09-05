using BIS.Business;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;


namespace GUI
{
    public partial class frmTemplate : Telerik.WinControls.UI.RadRibbonForm
    {
        public frmTemplate()
        {
            InitializeComponent();
            this.Icon = Login.iconForm;
            radRibbonBarGroupContracts.Visibility = ElementVisibility.Collapsed;
            btnNewContract.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupPurchase.Visibility = ElementVisibility.Collapsed;
            btnPurchase.Visibility = ElementVisibility.Collapsed;
            btnCancelTraveler.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupTravelpapers.Visibility = ElementVisibility.Collapsed;
            btnRibbonTravelpapers.Visibility = ElementVisibility.Collapsed;
            btnNewMeeting.Visibility = ElementVisibility.Collapsed;
            //this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();
           //ribbonExampleMenu.Text = "";
            //translate();
            
        }              

        private void ribbonExampleMenu_SizeChanged(object sender, EventArgs e)
        {
            radRibbonBarGroupContracts.Visibility = ElementVisibility.Collapsed;
            btnNewContract.Visibility = ElementVisibility.Collapsed;
            if( btnCopyContract.Visibility == ElementVisibility.Collapsed)
                btnCopyContract.Visibility = ElementVisibility.Collapsed;
            btnDeleteContract.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupPurchase.Visibility = ElementVisibility.Collapsed;
            btnPurchase.Visibility = ElementVisibility.Collapsed;
            btnDelPurchase.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupTraveler.Visibility = ElementVisibility.Collapsed;
            btnAddTraveler.Visibility = ElementVisibility.Collapsed;
            btnAddVoluntary.Visibility = ElementVisibility.Collapsed;
            btnDeleteTraveler.Visibility = ElementVisibility.Collapsed;
            btnCancelTraveler.Visibility = ElementVisibility.Collapsed;
            btnRibbonTravelpapers.Visibility = ElementVisibility.Collapsed;
            radRibbonBarGroupTravelpapers.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;
            btnNewMeeting.Visibility = ElementVisibility.Collapsed;


             
        }
        
    }
}
