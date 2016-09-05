using BIS.Business;
using BIS.DAO;
using BIS.Model;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using BIS.Core;

namespace GUI
{
    public partial class frmReportWizardDeparture4 : RadForm
    {
        //for reports
        DataTable dt72;
        DataTable seldt72;
        Color[] c = new Color[8];
        Image img;
        BindingList<ReportModel> selReportModel = new BindingList<ReportModel>();
        List<ReportModel> reportModel = new List<ReportModel>();
        BindingList<ReportLayoutModel> layouts;
        string layoutPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\report filters\\departure list 4");

        //preselection part
        PurchaseReportModel model = new PurchaseReportModel();
        List<ArrTypeModel> list = new List<ArrTypeModel>();
        List<LabelModel> listLablel = new List<LabelModel>();
        int sumNrTr;
        int sumMaxNrTr;
        int idArrType = 0;
        int idLabel = 0;
        int sumOccupancy;

        public frmReportWizardDeparture4()
        {
            InitializeComponent();

            dtFrom.Value = DateTime.Now;
            dtTo.Value = DateTime.Now;

            //preselection part
            ArrTypeBUS accBUS = new ArrTypeBUS();
            List<ArrTypeModel> am = new List<ArrTypeModel>();

            am = accBUS.GetArrTypes();


            List<string> type = new List<string>();
            string s = "";

            for (int i = 0; i < am.Count; i++)
            {
                s = am[i].nameArrType.ToString();
                type.Add(s);
            }

            ArrangementBookBUS bus = new ArrangementBookBUS();
            list = bus.AllArrType();
            dlArangementType.DataSource = list;
            dlArangementType.DisplayMember = "nameArrType";
            dlArangementType.ValueMember = "idArrType";


            for (int i = 0; i < Login._arrLabels.Count; i++)
            {

                dlLabel.Items.Add(Login._arrLabels[i].nameLabel.ToString());
                dlLabel.Items[i].Value = Login._arrLabels[i].idLabel.ToString();
            }

            if (dlLabel.Items.Count > 0)
            {
                dlLabel.SelectedIndex = 0;
            }
            if (ddlProvision.Items.Count > 0)
            {
                ddlProvision.SelectedIndex = 0;
            }


            //icon, form name translation
            this.Icon = Login.iconForm;
            string name = "Departure list 4";

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(name) != null)
                    name = resxSet.GetString(name);
            }
            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + name;

            setTranslation();

            this.rgvColumns.CellEditorInitialized += rgvColumns_CellEditorInitialized;

            //layouts

            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\report filters")))
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\report filters"));
            }
            if (!Directory.Exists(layoutPath))
            {
                Directory.CreateDirectory(layoutPath);
            }
            
            getLayout(layoutPath);
            if (File.Exists(Path.Combine(layoutPath,"Standard.xml")))
            {
                loadLayout("Standard");
            }
            else
            {
                c = new Color[8];
                c[0] = Color.LightGray;
                c[1] = Color.Black;
                c[2] = Color.White;
                c[3] = Color.Black;
                c[4] = Color.LightGray;
                c[5] = Color.Black;
                c[6] = Color.White;
                c[7] = Color.Black;

                txtHeader1.BackColor = c[0];
                txtHeader2.BackColor = c[0];
                txtHeader1.ForeColor = c[1];
                txtHeader2.ForeColor = c[1];
                txtRow1.BackColor = c[2];
                txtRow2.BackColor = c[2];
                txtRow1.ForeColor = c[3];
                txtRow2.ForeColor = c[3];
                txtHeader3.BackColor = c[4];
                txtHeader3.ForeColor = c[5];
                txtRow3.BackColor = c[6];
                txtRow3.ForeColor = c[7];
                
            }
            
        }

        private void getLayout(string folderName)
        {
            layouts = new BindingList<ReportLayoutModel>();
            string[] files = Directory.GetFiles(folderName, "*.xml");
             if (File.Exists(Path.Combine(layoutPath, "Standard.xml")))
                {
                    ReportLayoutModel rlm = new ReportLayoutModel();
                    rlm.idLayout = 0;
                    rlm.nameLayout = "Standard";
                    layouts.Add(rlm);
                }
            int i = 1;
            foreach (string file in files)
            {
                if(file.Replace(folderName + "\\", "").Replace(".xml", "")!="Standard")
                {
                    ReportLayoutModel rlm = new ReportLayoutModel();
                    rlm.idLayout = i;
                    rlm.nameLayout = file.Replace(folderName + "\\", "").Replace(".xml", "");
                    layouts.Add(rlm);
                    i++;
                }

            }
            ddlLayout.DataSource = layouts;
            ddlLayout.DisplayMember = "nameLayout";
            ddlLayout.ValueMember = "idLayout";
        }

        private void saveLayout(string name, Boolean isNeededRemove)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement dataset = xmldoc.CreateElement("data-set");
                xmldoc.AppendChild(dataset);
                XmlElement colors = xmldoc.CreateElement("colors");
                for (int i = 0; i < c.Length; i++)
                {

                    XmlElement colorNumber = xmldoc.CreateElement("color");
                    colorNumber.InnerText = c[i].R + ","  + c[i].G + "," + c[i].B;
                    colors.AppendChild(colorNumber);
                  
                }
                dataset.AppendChild(colors);
                XmlElement columns = xmldoc.CreateElement("columns");
                for (int i = 0; i < selReportModel.Count; i++)
                {
                   
                    XmlElement column = xmldoc.CreateElement("column" );
                    column.SetAttribute("idColumn", selReportModel[i].idColumn);
                    column.SetAttribute("nameColumn", selReportModel[i].nameColumn);
                    column.InnerText = selReportModel[i].widthColumn.ToString();
                    columns.AppendChild(column);
                   
                }
                dataset.AppendChild(columns);

                XmlElement logo = xmldoc.CreateElement("logo");
                Image im = logoReport.Image;
                ImageDB ii = new ImageDB();
                byte[] bb = ii.ImageToBytes(im);
                logo.InnerText = Convert.ToString(Convert.ToBase64String(bb));
                dataset.AppendChild(logo);

                xmldoc.AppendChild(dataset);
                xmldoc.Save(Path.Combine(layoutPath,  name + ".xml"));
                int num = 0;
                for(int j = 0;j<layouts.Count;j++)
                {
                    if (layouts[j].idLayout > num)
                        num = layouts[j].idLayout;
                }

                if(isNeededRemove==true)
                {
                    layouts.Remove(layouts.SingleOrDefault(p => p.nameLayout == txtLayoutName.Text));
                }

                ReportLayoutModel rlm = new ReportLayoutModel();
                if (name == "Standard")
                {
                    rlm.idLayout = 0;
                }
                else
                {
                    rlm.idLayout = num+1;
                }
                rlm.nameLayout = name;
                layouts.Add(rlm);

                loadLayout(rlm.nameLayout);
                ddlLayout.SelectedValue = rlm.idLayout;
                
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have successfully save layout!");
            }
            catch(Exception e)
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have not successfully save layout!");
            }
        }

        private void loadLayout(string name)
        {
            if (name != "")
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(Path.Combine(layoutPath, name + ".xml"));
                XmlNodeList xmlNodes = xmldoc.SelectNodes("/data-set/colors/color");
                int i = 0;
                c = new Color[8];
                foreach (XmlNode xmlNode in xmlNodes)
                {
                    string[] colorArray = xmlNode.InnerXml.Split(',');
                    c[i] = Color.FromArgb(Convert.ToInt32(colorArray[0]), Convert.ToInt32(colorArray[1]), Convert.ToInt32(colorArray[2]));
                    i++;
                }
                txtHeader1.BackColor = c[0];
                txtHeader2.BackColor = c[0];
                txtHeader1.ForeColor = c[1];
                txtHeader2.ForeColor = c[1];
                txtRow1.BackColor = c[2];
                txtRow2.BackColor = c[2];
                txtRow1.ForeColor = c[3];
                txtRow2.ForeColor = c[3];
                txtHeader3.BackColor = c[4];
                txtHeader3.ForeColor = c[5];
                txtRow3.BackColor = c[6];
                txtRow3.ForeColor = c[7];
                xmlNodes = xmldoc.SelectNodes("/data-set/columns/column");
                i = 0;
                selReportModel = new BindingList<ReportModel>();
                foreach (XmlNode xmlNode in xmlNodes)
                {
                    ReportModel rp = new ReportModel();
                    rp.idColumn = xmlNode.Attributes["idColumn"].Value;
                    rp.nameColumn = xmlNode.Attributes["nameColumn"].Value;
                    rp.widthColumn = Convert.ToDecimal(xmlNode.InnerXml.ToString());
                    rp.isChecked = true;
                    selReportModel.Add(rp);
                    i++;
                }
                XmlNode x = xmldoc.SelectSingleNode("/data-set/logo");
                byte[] img = Convert.FromBase64String(x.InnerXml);
                ImageDB ii = new ImageDB();
                logoReport.Image = ii.BytesToImage(img);
            }
        }

        private void btnHeaderBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = c[0];

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                txtHeader1.BackColor = MyDialog.Color;
                txtHeader2.BackColor = MyDialog.Color;
            }
        }

        private void btnHeaderForeColor_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = c[1];

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                txtHeader1.ForeColor = MyDialog.Color;
                txtHeader2.ForeColor = MyDialog.Color;
            }
        }

        private void btnRowsBackColor_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = c[2];

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                txtRow1.BackColor = MyDialog.Color;
                txtRow2.BackColor = MyDialog.Color;
            }
        }

        private void btnRowsForeColor_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = c[3];

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                txtRow1.ForeColor = MyDialog.Color;
                txtRow2.ForeColor = MyDialog.Color;
            }
        }

        private void btnHeaderBackColor2_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = c[4];

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
                txtHeader3.BackColor = MyDialog.Color;
        }

        private void btnHeaderForeColor2_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = c[5];

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                txtHeader3.ForeColor = MyDialog.Color;
            }
        }

        private void btnRowsBackColor2_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = c[6];

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                txtRow3.BackColor = MyDialog.Color;
            }
        }

        private void btnRowsForeColor2_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = c[7];

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                txtRow3.ForeColor = MyDialog.Color;
            }
        }

        private void radWizard1_Finish(object sender, EventArgs e)
        {
            seldt72 = new DataTable();
            seldt72 = dt72.Copy();
            if(seldt72!=null)
                if (seldt72.Rows.Count > 0)
                {
                    for (int j = 0; j < seldt72.Columns.Count; j++)
                    {
                        if (selReportModel.SingleOrDefault(p => p.idColumn == seldt72.Columns[j].ColumnName.ToString()) == null)
                        {
                            if (seldt72.Columns.Contains(seldt72.Columns[j].ColumnName) == true)
                            {
                                seldt72.Columns.Remove(seldt72.Columns[j].ColumnName);
                                j--;
                            }
                        }
                    }
                    string label = dlLabel.SelectedItem.ToString();
                    string type = dlArangementType.SelectedItem.ToString();
                    string btw = ddlProvision.SelectedItem.ToString();

                    ReportViewerDepartureList rvf = new ReportViewerDepartureList(seldt72, c, img, Convert.ToDateTime(dtFrom.Value), Convert.ToDateTime(dtTo.Value), selReportModel, type, label, btw, this.Text.Replace(Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - ", ""),"");
                    rvf.Show();

                }
                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("There is nothing for print preview!");
                }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("There is nothing for print preview!");
            }
            
        }

        private void radWizard1_Next(object sender, Telerik.WinControls.UI.WizardCancelEventArgs e)
        {
            if (radWizard1.SelectedPage == radWizard1.WelcomePage)
            {
                dt72 = new DataTable();
                DataTable dt73 = new DataTable();
                string typeArrangement = dlArangementType.SelectedItem.ToString();
                string arrangement = dlArangementType.SelectedValue.ToString();
                if (arrangement != null)
                    idArrType = Convert.ToInt32(arrangement);

                // select provision:
                string provision = ddlProvision.SelectedItem.ToString();


                //selec label
                string labelName = dlLabel.SelectedItem.ToString();

                int index = dlLabel.SelectedIndex;
                string lebidLabelelId = dlLabel.Items[index].Value.ToString();
                if (labelName != null)

                    idLabel = Convert.ToInt32(lebidLabelelId);


                if (ddlLayout.SelectedItem != null)
                    loadLayout(ddlLayout.SelectedItem.DisplayValue.ToString());
                if (btnExclusive.IsChecked == true)
                {
                    DepartureList4Ex(idArrType, provision, idLabel);
                    dt72 = DepartureList4Ex(idArrType, provision, idLabel);

                }
                if (btnInclusive.IsChecked == true)
                {
                    DepartureList4xIn(idArrType, provision, idLabel);
                    dt72 = DepartureList4xIn(idArrType, provision, idLabel);
                }

            }
            else if (radWizard1.SelectedPage == radWizard1.Pages[1])
            {
            c[0] = txtHeader1.BackColor;
            c[1] = txtHeader1.ForeColor;
            c[2] = txtRow1.BackColor;
            c[3] = txtRow2.ForeColor;
            c[4] = txtHeader3.BackColor;
            c[5] = txtHeader3.ForeColor;
            c[6] = txtRow3.BackColor;
            c[7] = txtRow3.ForeColor;

            img = logoReport.Image;
            }
        }
        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {

                //preselection part
                if (resxSet.GetString(lblDtFrom.Text) != null)
                    lblDtFrom.Text = resxSet.GetString(lblDtFrom.Text);
                if (resxSet.GetString(lblDtTo.Text) != null)
                    lblDtTo.Text = resxSet.GetString(lblDtTo.Text);

                if (resxSet.GetString(lblArrangementType.Text) != null)
                    lblArrangementType.Text = resxSet.GetString(lblArrangementType.Text);
                if (resxSet.GetString(lblLabel.Text) != null)
                    lblLabel.Text = resxSet.GetString(lblLabel.Text);
                if (resxSet.GetString(lblBWT.Text) != null)
                    lblBWT.Text = resxSet.GetString(lblBWT.Text);

                //header and logo part

                if (resxSet.GetString(btnUpload.Text) != null)
                    btnUpload.Text = resxSet.GetString(btnUpload.Text);
                if (resxSet.GetString(btnHeaderBackColor.Text) != null)
                    btnHeaderBackColor.Text = resxSet.GetString(btnHeaderBackColor.Text);
                if (resxSet.GetString(btnHeaderForeColor.Text) != null)
                    btnHeaderForeColor.Text = resxSet.GetString(btnHeaderForeColor.Text);
                if (resxSet.GetString(btnRowsBackColor.Text) != null)
                    btnRowsBackColor.Text = resxSet.GetString(btnRowsBackColor.Text);
                if (resxSet.GetString(btnRowsForeColor.Text) != null)
                    btnRowsForeColor.Text = resxSet.GetString(btnRowsForeColor.Text);

                //pages on wizard
                for(int i = 0;i< radWizard1.Pages.Count;i++)
                {

                    if (resxSet.GetString( radWizard1.Pages[i].Title) != null)
                         radWizard1.Pages[i].Title = resxSet.GetString( radWizard1.Pages[i].Title);
                }


                //layouts part
                if (resxSet.GetString(lblLayout.Text) != null)
                    lblLayout.Text = resxSet.GetString(lblLayout.Text);
                if (resxSet.GetString(btnSaveLayouts.Text) != null)
                    btnSaveLayouts.Text = resxSet.GetString(btnSaveLayouts.Text);
            }
        }

        private DataTable DepartureList4xIn(int idArrType, string provision, int idLabel)
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);
            DataTable dt = new DataTable();
            ArrangementBookDAO ab = new ArrangementBookDAO();
            dt = ab.DepartureList4New(model.dateFrom, model.dateTo, idArrType, provision, idLabel);
            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["dateFrom"] != null)
                        dt.Columns["dateFrom"].Caption = "Date from";
                    if (dt.Columns["arrangement"] != null)
                        dt.Columns["arrangement"].Caption = "Arrangement";
                    if (dt.Columns["bookedTravelers"] != null)
                        dt.Columns["bookedTravelers"].Caption = "Booked travelers";
                    if (dt.Columns["maxTravelers"] != null)
                        dt.Columns["maxTravelers"].Caption = "Max travelers";
                    if (dt.Columns["Occupancy"] != null)
                        dt.Columns["Occupancy"].Caption = "Occupancy %";
                    
                    reportModel = new List<ReportModel>();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                       
                        ReportModel rp = new ReportModel();
                        rp.idColumn = dt.Columns[i].ColumnName;
                        rp.nameColumn = dt.Columns[i].Caption;
                        

                        string ColumnName = dt.Columns[i].Caption;
                        decimal wColumn = 0;
                        System.Drawing.Font f = new System.Drawing.Font("Arial", 8);

                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {

                            if (resxSet.GetString(dt.Columns[i].Caption) != null)
                                ColumnName = resxSet.GetString(dt.Columns[i].Caption);

                        }

                        if (ddlLayout.SelectedItem==null)
                        {
                            //za sirinu kolona
                            if (dt.Columns[i].ColumnName == "arrangement")
                            {
                                wColumn = (decimal)1.9;
                            }
                            else
                                wColumn = (decimal)1;
                            rp.isChecked = true;
                            if(i==0)
                            selReportModel = new BindingList<ReportModel>();
                            selReportModel.Add(rp);
                        }
                        else
                        {
                            ReportModel srpt = new ReportModel();
                            srpt = selReportModel.FirstOrDefault(s=>s.idColumn==dt.Columns[i].ColumnName);
                            if (srpt!=null)
                            {
                                wColumn = srpt.widthColumn;
                                rp.isChecked = true;
                            }
                            else if (dt.Columns[i].DataType == typeof(int) || dt.Columns[i].DataType == typeof(decimal))
                            {
                                wColumn = (decimal) 0;
                                rp.isChecked = false;
                            }
                            else
                            {
                                wColumn = (decimal)0;
                                rp.isChecked = false;
                            }
                            
                        }
                        rp.widthColumn = wColumn;
                        reportModel.Add(rp);
                    }
                    if (reportModel != null)
                    {
                        rgvColumns.DataSource = reportModel;

                    }
                }
            }
            return dt;

        }

        public static SizeF MeasureString(string s, System.Drawing.Font font)
        {
            SizeF result;
            using (var image = new Bitmap(1, 1))
            {
                using (var g = Graphics.FromImage(image))
                {
                    result = g.MeasureString(s, font);
                }
            }

            return result;
        }

        private DataTable DepartureList4Ex(int idArrType, string provision, int idLabel)
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);
            DataTable dt = new DataTable();
            ArrangementBookDAO ab = new ArrangementBookDAO();
            dt = ab.DepartureList4ExNew(model.dateFrom, model.dateTo, idArrType, provision, idLabel);
            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["dateFrom"] != null)
                        dt.Columns["dateFrom"].Caption = "Date from";
                    if (dt.Columns["arrangement"] != null)
                        dt.Columns["arrangement"].Caption = "Arrangement";
                    if (dt.Columns["dateFrom"] != null)
                        dt.Columns["dateFrom"].Caption = "Date from";
                    if (dt.Columns["bookedTravelers"] != null)
                        dt.Columns["bookedTravelers"].Caption = "Booked travelers";
                    if (dt.Columns["maxTravelers"] != null)
                        dt.Columns["maxTravelers"].Caption = "Max travelers";
                    if (dt.Columns["Occupancy"] != null)
                        dt.Columns["Occupancy"].Caption = "Occupancy %";

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        ReportModel rp = new ReportModel();
                        rp.idColumn = dt.Columns[i].ColumnName;
                        rp.nameColumn = dt.Columns[i].Caption;


                        string ColumnName = dt.Columns[i].Caption;
                        decimal wColumn = 0;
                        System.Drawing.Font f = new System.Drawing.Font("Arial", 8);

                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {

                            if (resxSet.GetString(dt.Columns[i].Caption) != null)
                                ColumnName = resxSet.GetString(dt.Columns[i].Caption);

                        }

                        if (ddlLayout.SelectedItem == null)
                        {
                            //za sirinu kolona
                            if (dt.Columns[i].ColumnName == "arrangement")
                            {
                                wColumn = (decimal)1.9;
                            }
                            else
                                wColumn = (decimal)1;
                            rp.isChecked = true;
                            if (i == 0)
                                selReportModel = new BindingList<ReportModel>();
                            selReportModel.Add(rp);
                        }
                        else
                        {
                            ReportModel srpt = new ReportModel();
                            srpt = selReportModel.FirstOrDefault(s => s.idColumn == dt.Columns[i].ColumnName);
                            if (srpt != null)
                            {
                                wColumn = srpt.widthColumn;
                                rp.isChecked = true;
                            }
                            else if (dt.Columns[i].DataType == typeof(int) || dt.Columns[i].DataType == typeof(decimal))
                            {
                                wColumn = (decimal)0;
                                rp.isChecked = false;
                            }
                            else
                            {
                                wColumn = (decimal)0;
                                rp.isChecked = false;
                            }

                        }
                        rp.widthColumn = wColumn;
                        reportModel.Add(rp);
                    }
                    if (reportModel != null)
                    {
                        rgvColumns.DataSource = reportModel;

                    }
                }
            }
            return dt;

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please select an image file to encrypt.";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //Get the file's path
                var filePath = dialog.FileName;
                //save image
                logoReport.Image = Image.FromFile(dialog.FileName);
            }       
        }


        private void rgvColumns_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            var grid = sender as RadGridView;
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                foreach (var column in grid.Columns)
                {
                    if (resxSet.GetString(column.HeaderText) != null && resxSet.GetString(column.HeaderText) != "")
                        column.HeaderText = resxSet.GetString(column.HeaderText);
                    column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 50);
                }
            }

            if (rgvColumns != null)
                if (rgvColumns.ColumnCount > 0)
                {
                    this.rgvColumns.SummaryRowsTop.Clear();
                    rgvColumns.MasterTemplate.EnablePaging = false;
                    rgvColumns.MasterTemplate.ShowTotals = true;
                    string expression = "Sum(IIf(isChecked=False,0,widthColumn))";
                    GridViewSummaryItem summaryItem = new GridViewSummaryItem("widthColumn", "{0}", expression);
                    summaryItem.FormatString = "{0:N2}";

                    GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                    summaryRowItem.Add(summaryItem);
                    this.rgvColumns.SummaryRowsTop.Add(summaryRowItem);

                    rgvColumns.Columns["isChecked"].Width = 50;
                    rgvColumns.Columns["nameColumn"].Width = 250;
                    rgvColumns.Columns["idColumn"].ReadOnly = true;
                    rgvColumns.Columns["idColumn"].IsVisible = false;
                    rgvColumns.Columns["nameColumn"].ReadOnly = true;

                }
        }


        private void btnSaveLayouts_Click(object sender, EventArgs e)
        {
            if (txtLayoutName.Text != "")
            {
                if (File.Exists(Path.Combine(layoutPath, txtLayoutName.Text + ".xml")))
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    if (tr.translateAllMessageBoxDialogYesNo("You already have this layout name. Are you sure that you want to override the existing one?", "Save report layout") == System.Windows.Forms.DialogResult.Yes)
                    {
                        File.Delete(Path.Combine(layoutPath, txtLayoutName.Text + ".xml"));
                        saveLayout(txtLayoutName.Text,true);
                    }
                }
                else
                {
                    saveLayout(txtLayoutName.Text, false);
                }

            }
            else
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to fill layout name");
            }
        }


        private void ddlLayout_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (ddlLayout.SelectedIndex != null)
            { 
                ReportLayoutModel rlm = new ReportLayoutModel();
                if (ddlLayout.SelectedValue.GetType() == typeof(int))
                rlm = layouts.SingleOrDefault(p => p.idLayout == Convert.ToInt32(ddlLayout.SelectedValue));
                if(rlm!=null)
                    loadLayout(rlm.nameLayout);
            }
        }


        private void rgvColumns_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is Telerik.WinControls.UI.GridSummaryCellElement)
            {
                e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                if (e.CellElement.Value != "")
                if(Convert.ToDecimal(e.CellElement.Value)>(decimal)5.9)
                {
                    e.CellElement.BackColor = System.Drawing.Color.Red;
                    e.CellElement.NumberOfColors = 1;
                    e.CellElement.DrawFill = true;
                }
                else if (Convert.ToDecimal(e.CellElement.Value) < (decimal)5.9)
                {
                    e.CellElement.BackColor = System.Drawing.Color.Yellow;
                    e.CellElement.NumberOfColors = 1;
                    e.CellElement.DrawFill = true;
                }
                else if (Convert.ToDecimal(e.CellElement.Value) == (decimal)5.9)
                {
                    e.CellElement.BackColor = System.Drawing.Color.DodgerBlue;
                    e.CellElement.NumberOfColors = 1;
                    e.CellElement.DrawFill = true;
                }
                else
                {
                    e.CellElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
                    e.CellElement.ResetValue(LightVisualElement.NumberOfColorsProperty, ValueResetFlags.Local);
                    e.CellElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                }
            }
        }


        private void rgvColumns_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
                if (e.Column.Name == "isChecked")
                {
                    if (e.Row.DataBoundItem != null)
                    {
                        ReportModel m = (ReportModel)e.Row.DataBoundItem;
                        if (m.isChecked != true)
                        {
                            selReportModel.Remove(selReportModel.SingleOrDefault(s=>s.idColumn == m.idColumn));
                            m.isChecked = false;
                            m.widthColumn = 0;
                        }
                        else
                        {
                            m.isChecked = true;
                            string ColumnName = m.nameColumn;
                            decimal wColumn = 0;
                            System.Drawing.Font f = new System.Drawing.Font("Arial", 8);


                            //za sirinu kolona
                            if (m.idColumn == "arrangement")
                            {
                                wColumn = (decimal)1.9;
                            }
                            else
                                wColumn = (decimal)1;
                            m.widthColumn = wColumn;
                            if(!selReportModel.Contains(m))
                            selReportModel.Add(m);
                        }
                       
                    }
                }
                if (e.Column.Name == "widthColumn")
                {
                    if (e.Row.DataBoundItem != null)
                    {
                        ReportModel m = (ReportModel)e.Row.DataBoundItem;
                        if (selReportModel.FirstOrDefault(s => s.idColumn == m.idColumn)!=null)
                        selReportModel.FirstOrDefault(s => s.idColumn == m.idColumn).widthColumn = (decimal) m.widthColumn;
                    }
                }
                       
        }

        private void radWizard1_Cancel(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rgvColumns_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            //Restriction  mouse wheel and KayUp, KeyDown for grid when is in Edit mode Gorance 25 08
            var editor = e.ActiveEditor as GridSpinEditor;
            if (editor != null)
            {
                var element = editor.EditorElement as GridSpinEditorElement;
                element.InterceptArrowKeys = false;
                element.EnableMouseWheel = false;
            }
        }



    }
}
