using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using System.Resources;


namespace GUI.User_Controls
{
    public partial class ReportsManagmentPanel : UserControl
    {

        public ReportsManagmentPanel()
        {
            InitializeComponent();
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                for (int i = 0; i < radPanorama1.Groups.Count; i++)
                {
                    if (resxSet.GetString(radPanorama1.Groups[i].Text) != null)
                        radPanorama1.Groups[i].Text = resxSet.GetString(radPanorama1.Groups[i].Text);

                    
                    for (int j = 0; j < radPanorama1.Groups[i].Children.Count; j++)
                    {
                        for (int n = 0; n < radPanorama1.Groups[i].Children[j].Children.Count; n++)
                        {
                            RadTileElement rte = (RadTileElement) radPanorama1.Groups[i].Children[j].Children[n];
                            if (resxSet.GetString(rte.Text) != null)
                                rte.Text = resxSet.GetString(rte.Text);
                        }
                    }
                }
            }
        }
       

        private void rtPurchase_Click(object sender, EventArgs e)
        {
            frmPurchaseReport frmm = new frmPurchaseReport();
            frmm.Show();
        }

        private void rtSales_Click(object sender, EventArgs e)
        {
            frmSalesReport frms = new frmSalesReport();
            frms.Show();
        }

        private void rtVoluntary_Click(object sender, EventArgs e)
        {
            frmVolunteerReport ffr = new frmVolunteerReport();
            ffr.Show();
        }

    

        private void rtDepartureList_Click(object sender, EventArgs e)
        {
            frmReportWizardDeparture1 frm = new frmReportWizardDeparture1();
            frm.Show();
        }

        private void rtDepartureList2_Click(object sender, EventArgs e)
        {
            //frmDepartureList2 frm = new frmDepartureList2();
            //frm.Show();
            frmReportWizardDeparture2 frm = new frmReportWizardDeparture2();
            frm.ShowDialog();
        }

        private void rtOverviewBooking_Click(object sender, EventArgs e)
        {
            //frmOverwievBookingReport frm = new frmOverwievBookingReport();
            //frm.Show();
            OverviewBooking ob = new OverviewBooking();
            ob.Show();
        }


        private void rtDepartureList3_Click(object sender, EventArgs e)
        {
            frmReportWizardDeparture3 frmm = new frmReportWizardDeparture3();
            frmm.Show();
        }

        private void rtDepartureList4_Click(object sender, EventArgs e)
        {
            frmReportWizardDeparture4 frmm = new frmReportWizardDeparture4();
            frmm.Show();
        }

        private void rtJournal_Click(object sender, EventArgs e)
        {
            frmJurnal frmJ = new frmJurnal();
            frmJ.Show();
        }
        private void rtLedger_Click(object sender, EventArgs e)
        {
            frmLedgerSelection frmJ = new frmLedgerSelection();
            frmJ.Show();
          
        }

        private void rtMe940_Click(object sender, EventArgs e)
        {
            ImportGmu gmu = new ImportGmu();
            gmu.Show();
        }

        private void rtInvoiceSel_Click(object sender, EventArgs e)
        {
            frmInvoiceSelection fis = new frmInvoiceSelection();
            fis.ShowDialog();
        }

        private void rtClosing_Click(object sender, EventArgs e)
        {
            frmLineClose frmC = new frmLineClose();
            frmC.Show();
        }

        private void rtCreditPay_Click(object sender, EventArgs e)
        {
            frmCreditPay frc = new frmCreditPay();
            frc.Show();
        }

        private void tgAccounting_Click(object sender, EventArgs e)
        {

        }

        private void rtOpenLInes_Click(object sender, EventArgs e)
        {
            frmOpenLines openLlines = new frmOpenLines();
            openLlines.ShowDialog();
        }

        private void rtApproving_Click(object sender, EventArgs e)
        {
            frmAccCreditorApprove fca = new frmAccCreditorApprove();
            fca.Show();
        }

        private void rtBankPay_Click(object sender, EventArgs e)
        {
            frmBankCreditPay fbp = new frmBankCreditPay();
            fbp.Show();

          
        }

        private void rtLedgerCard_Click(object sender, EventArgs e)
        {
            frmLedgerCard lcr = new frmLedgerCard();
            lcr.Show();
        }

        private void rtInsuranceVolunteers_Click(object sender, EventArgs e)
        {
            frmInsuranceVolunteers fiv = new frmInsuranceVolunteers();
            fiv.Show();
        }

        private void rtDebitCreditCard_Click(object sender, EventArgs e)
        {
            //frmDebitorCreditorCard fdc = new frmDebitorCreditorCard();
            //fdc.Show();

            frmReportWizardDeparture2 frm = new frmReportWizardDeparture2();
            frm.ShowDialog();
        }
    }
}
