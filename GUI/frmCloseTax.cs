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
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.Data;

namespace GUI
{
    public partial class frmCLoseTax : Telerik.WinControls.UI.RadForm
    {
        public int iID;
        public string codTax = "";
        AccTaxValidityModel taxValid;
        public Boolean isOk = false;
        private DateTime adate;
        public string aCode = "";
        public int idRec;
         Boolean isChanged = false;

        public frmCLoseTax()
        {
            InitializeComponent();
        }

        public frmCLoseTax(string codeTax, int eID)
        {
            codTax = codeTax;
            iID = eID;
            InitializeComponent();
        }

        private void frmCLoseTax_Load(object sender, EventArgs e)
        {
            
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            
            
            adate = Convert.ToDateTime(rdpDateClose.Text);
            AccTaxValidityBUS acb1 = new AccTaxValidityBUS();

            taxValid = acb1.GetTaxValidityById(iID);
          
            if (taxValid != null)
            {
                if (taxValid.startDate > adate)
                {
                    RadMessageBox.Show("Close date is not valid");
                    return;
                }
                else
                {

                    taxValid.endDate = adate;        //Convert.ToDateTime(rdpDateClose.Text);
                    taxValid.idTaxValidity = iID;
                    taxValid.codeTax = codTax;
                    isOk = acb1.Update(taxValid, this.Name, Login._user.idUser);
                    if (isOk == false)
                    {
                        RadMessageBox.Show("Error Updating record");
                        return;
                    }
                    else
                    {
                        RadMessageBox.Show("Sucessfully close Tax");
                        isChanged = true;
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;    
                        this.Close();
                        
                        
                    }
                }
            }
            else
            {
                RadMessageBox.Show("ERROR !!!  Can't find a record");
                return;
            }
        }
    }
}
