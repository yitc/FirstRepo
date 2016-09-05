using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace GUI
{
    public partial class frmMeetingPerson : RadForm
    {
        int perosnID = -1;
        string personName = "";

        public frmMeetingPerson(int perosnid, string personName)
        {
            InitializeComponent();

            this.perosnID = perosnid;
            this.personName = personName;
        }

        private void radSplitContainerForm_Click(object sender, EventArgs e)
        {

        }

        private void frmMeetingPerson_Load(object sender, EventArgs e)
        {
            MeetingSchedluer_uc msc = new MeetingSchedluer_uc();
            msc.Dock = DockStyle.Fill;
            msc.perosnId = perosnID;
            msc.perosnName = personName;

            radPanel1.Controls.Add(msc);
        }
    }
}
