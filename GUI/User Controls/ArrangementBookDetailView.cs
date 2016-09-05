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

namespace GUI.User_Controls
{
    public partial class ArrangementBookDetailView: UserControl
    {
        ArrangementModel arrModel = new ArrangementModel();
        RadListDataItem ritem = new RadListDataItem();

        public ArrangementBookDetailView()
        {
            InitializeComponent();
            RadMessageBox.SetThemeName("Windows8");

        }
        private void printvalue(string title, string value) {
                    ritem = new RadListDataItem();
                    ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Bold);
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString(title) != null)
                                ritem.Text = resxSet.GetString(title) + ": ";
                            else
                                ritem.Text = title +": ";
                        }
                    radListControl1.Items.Add(ritem);
                    ritem = new RadListDataItem();
                    ritem.Font = new System.Drawing.Font("Verdana", 10F, FontStyle.Regular);
                    ritem.Text = value;
                    radListControl1.Items.Add(ritem);
        }
        public void setArrangementBookDetails(ArrangementModel am)
        {
            arrModel = am;
            radListControl1.Items.Clear();
            if(arrModel!=null){
                if(arrModel.nameArrangement !="")
                    printvalue("Arrangement name", arrModel.nameArrangement);
                if (arrModel.dtFromArrangement != null && arrModel.dtToArrangement != null)
                    printvalue("Date from - date to", arrModel.dtFromArrangement.ToString("dd-MM-yyyy") + " - " + arrModel.dtToArrangement.ToString("dd-MM-yyyy"));
                if(arrModel.typeNameArrangement !="")
                    printvalue("Type", arrModel.typeNameArrangement);
                if(arrModel.statusArrangement !="")
                    printvalue("Status", arrModel.statusArrangement);
                if(arrModel.nrTraveler >=0)
                    printvalue("Max. nr. of travelers", arrModel.nrTraveler.ToString());
                if (arrModel.nrVoluntaryHelper >=0)
                    printvalue("Nr. of volunteers", arrModel.nrVoluntaryHelper.ToString());
                ArrangementBookBUS arbus = new ArrangementBookBUS();
                int maxWheelchairs = arrModel.nrMaximumWheelchairs;
                int bookedWheelchairs = arbus.GetBookPersMedicMoreAns(new List<int> { 441, 442, 449, 450, 451 }, arrModel.idArrangement);
                int freeWheelchairs = maxWheelchairs - bookedWheelchairs;
                if(freeWheelchairs >= 0)
                    printvalue("Free wheelchairs", freeWheelchairs.ToString());
                int maxRollator = arrModel.whoseElectricWheelchairs;
                int bookedRollator = arbus.GetBookPersMedicMoreAns(new List<int> { 446, 447, 448 }, arrModel.idArrangement);
                int freeRollator = maxRollator - bookedRollator;
                if (freeRollator >= 0)
                    printvalue("Free rollator", freeRollator.ToString());
                int maxArmSometimes = arrModel.buSupportingArms;
                int bookedArmSometimes = arbus.GetBookPersMedicMoreAns(new List<int> { 439, 440 }, arrModel.idArrangement);
                int freeArmSometimes = maxArmSometimes - bookedArmSometimes;
                if (freeArmSometimes >= 0)
                    printvalue("Free arm sometimes",freeArmSometimes.ToString());
                int maxAnchorage = arrModel.nrAnchorage;
                int bookedAnchorage = arbus.GetBookPersMedicMoreAns(new List<int> { 823 }, arrModel.idArrangement);
                int freeAnchorage = maxAnchorage - bookedAnchorage;
                if (freeAnchorage >= 0)
                    printvalue("Free anchorage", freeAnchorage.ToString());
                    
            }
        }
    }
}
