using BIS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.Reporting;
using Telerik.WinControls;

namespace GUI
{
    public partial class ReportViewerDepartureList : Telerik.WinControls.UI.RadForm
    {
        BindingList<ReportModel> selReportModel;
        DataTable dt;
        Color[] c;
        Image logo;
        DateTime dateTo = DateTime.MinValue;
        DateTime dateFrom = DateTime.MinValue;
        string label = "";
        string type = "";
        string btw = "";
        string theme = "";
        public ReportViewerDepartureList(DataTable dtFollow, Color[] c1, Image img, DateTime dtFrom, DateTime dtTo, BindingList<ReportModel> sel, string typeForm, string labelForm, string btwForm, string nameForm,string themeForm)
        {
            InitializeComponent();
            dt = dtFollow;
            c = c1;
            dateFrom = dtFrom;
            dateTo = dtTo;

            this.Icon = Login.iconForm;
            string name = nameForm;

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(name) != null)
                    name = resxSet.GetString(name);
            }
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + name;
            logo = img;
            selReportModel = sel;
            label = labelForm;
            type = typeForm;
            btw = btwForm;
            theme = themeForm;
        }

        private void ReportViewerDepartureList_Load(object sender, EventArgs e)
        {
            InstanceReportSource irs = new InstanceReportSource();
            irs.ReportDocument = new ReportDepartureList(dt, c, logo, dateFrom, dateTo, selReportModel, type, label, btw, this.Text.Replace(Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - ",""),theme);
            this.rptViewerDeparturList4.ReportSource = irs;
            this.rptViewerDeparturList4.RefreshReport();
        }
    }
}
