using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Business;
using Telerik.WinControls.UI;
using BIS.Model;
using System.Resources;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace GUI
{
    public partial class AccountDetailView : UserControl
    {
        private string fromr;
        private string tor;
        public AccountDetailView()
        {
           
            InitializeComponent();
            RadMessageBox.SetThemeName("Windows8");

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
                            RadTileElement rte = (RadTileElement)radPanorama1.Groups[i].Children[j].Children[n];
                            if (resxSet.GetString(rte.Text) != null)
                                rte.Text = resxSet.GetString(rte.Text);
                        }
                    }
                }
            }
           
        }

        public void SetPersonDetails(BIS.Model.PersonModel person)
        {

           
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
            frmLedgerCard flc = new frmLedgerCard();
            flc.Show();

           
        }

        private void rtInsuranceVolunteers_Click(object sender, EventArgs e)
        {
            frmInsuranceVolunteers fiv = new frmInsuranceVolunteers();
            fiv.Show();

          
        }

        private void rtDebitCreditCard_Click(object sender, EventArgs e)
        {
            frmDebitorCreditorCard frm = new frmDebitorCreditorCard();
            frm.Show();
        }

        private void rtInsurance_Click(object sender, EventArgs e)
        {
            frmInsuranceSelection frmIns = new frmInsuranceSelection();
            frmIns.Show();

        }

        private void rtExtraArticles_Click(object sender, EventArgs e)
        {
            frmExtraArticles frmE = new frmExtraArticles();
            frmE.Show();
        }

        private void rtPrognose_Click(object sender, EventArgs e)
        {
            frmPrognoseSelection ps = new frmPrognoseSelection();
            ps.ShowDialog();
        }

        private void rtBalans_Click(object sender, EventArgs e)
        {
            frmSaldoBalansSelection frmE = new frmSaldoBalansSelection();
            frmE.Show();
        }

        private void rtPeriodBalans_Click(object sender, EventArgs e)
        {
            frmPeriodSelection ps = new frmPeriodSelection();
            ps.ShowDialog();
        }
        
    }
}
