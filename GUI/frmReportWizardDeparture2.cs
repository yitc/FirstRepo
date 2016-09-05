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
    public partial class frmReportWizardDeparture2 : RadForm
    {
        //for reports
        DataTable dt72;
        DataTable seldt72;
        Color[] c = new Color[8];
        Image img;
        BindingList<ReportModel> selReportModel = new BindingList<ReportModel>();
        List<ReportModel> reportModel = new List<ReportModel>();
        BindingList<ReportLayoutModel> layouts;
        string layoutPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\report filters\\departure list 2");

        //preselection part
        PurchaseReportModel model = new PurchaseReportModel();
        List<ArrTypeModel> list = new List<ArrTypeModel>();
        List<LabelModel> listLablel = new List<LabelModel>();
       
        string mounth = "";
        string mounthNr = "";
        decimal sumOccupancy = 0;
        decimal sumAllOccupancy = 0;
        int sumMounth = 0;
        int sumAll = 0;
        int sumMounthNr = 0;
        int sumAllNr = 0;
        string nazivmeseca;
        int noRow = 0;
        DataRow dr;

        decimal sumMinNumTra = 0;
        decimal sumMounthNumTra = 0;


        List<int> sumaPoMesecima = new List<int>();
        List<int> noviRed = new List<int>();
        List<string> nazivSume = new List<string>();

        

        public frmReportWizardDeparture2()
        {
            InitializeComponent();

            dtFrom.Value = DateTime.Now;
            dtTo.Value = DateTime.Now;
            //preselection part


            //icon, form name translation
            this.Icon = Login.iconForm;
            string name = "Departure list 2";

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
                    string label = "";
                    string type = "";
                    string btw = "";

                    ReportViewerDepartureList2 rvf = new ReportViewerDepartureList2(seldt72, c, img, Convert.ToDateTime(dtFrom.Value), Convert.ToDateTime(dtTo.Value), selReportModel, type, label, btw, this.Text.Replace(Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - ", ""),"");
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

                if (ddlLayout.SelectedItem != null)
                    loadLayout(ddlLayout.SelectedItem.DisplayValue.ToString());

                
                dt72 = new DataTable();
                if (btnExclusive.IsChecked == true)
                {

                    //DepartureList2Ex();
                    dt72 = DepartureList2Ex();
                }
                if (btnInclusive.IsChecked == true)
                {
                    //DepartureList2();
                    dt72 = DepartureList2();
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

        public DataTable DepartureList2Ex()
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);
            int nr = 0;
            DataTable dt = new DataTable();
            ArrangementBookDAO dao = new ArrangementBookDAO();
            dt = dao.DepartureList2Ex(model.dateFrom, model.dateTo);

            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["mounth"] != null)
                        dt.Columns["mounth"].Caption = "Mounth";
                    if (dt.Columns["dateFrom"] != null)
                        dt.Columns["dateFrom"].Caption = "Date from";
                    if (dt.Columns["dayarr"] != null)
                        dt.Columns["dayarr"].Caption = "Day";
                    if (dt.Columns["arrangement"] != null)
                        dt.Columns["arrangement"].Caption = "Code arrangement";

                    if (dt.Columns["nr"] != null)
                        dt.Columns["nr"].Caption = "Booked travelers";
                    if (dt.Columns["nrTraveler"] != null)
                        dt.Columns["nrTraveler"].Caption = "Max travelers";
                    if (dt.Columns["minNrTraveler"] != null)
                        dt.Columns["minNrTraveler"].Caption = "Min travelers";
                    if (dt.Columns["Occupancy"] != null)
                        dt.Columns["Occupancy"].Caption = "Occupancy";
                    nr = dt.Columns.Count;

                }
                // sum nrTravelers
                if (dt.Rows.Count > 0)
                {
                    DataColumn dc = new DataColumn("Sum", typeof(int));
                    dc.Caption = "Sum of max";
                    // dc.DefaultValue = "";
                    dt.Columns.Add(dc);
                    DataColumn dc1 = new DataColumn("SumNr", typeof(int));
                    dc1.Caption = "Sum of pax";
                    // dc.DefaultValue = "";
                    dt.Columns.Add(dc1);

                    mounth = dt.Rows[0]["mounth"].ToString();
                    DataRow dr = dt.NewRow();
                    dr[0] = "Grand total sum";
                    dt.Rows.Add(dr);


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string mesec = dt.Rows[i]["mounth"].ToString();
                        if (mesec != mounth)
                        {
                            mounth = dt.Rows[i]["mounth"].ToString();
                            dr = dt.NewRow();

                            dr["mounth"] = "Sum";
                            dt.Rows.InsertAt(dr, i);
                        }

                    }


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string mesec = dt.Rows[i]["mounth"].ToString();
                        if (mesec != "Sum")
                        {
                            if (dt.Rows[i]["nr"].ToString() != "")
                            {
                                sumMounth += Convert.ToInt32(dt.Rows[i]["nr"].ToString());
                                sumAll += Convert.ToInt32(dt.Rows[i]["nr"].ToString());
                            }
                            if (dt.Rows[i]["nrTraveler"].ToString() != "")
                            {
                                sumMounthNr += Convert.ToInt32(dt.Rows[i]["nrTraveler"].ToString());
                                sumAllNr += Convert.ToInt32(dt.Rows[i]["nrTraveler"].ToString());
                            }
                            if (dt.Rows[i]["minNrTraveler"].ToString() != "")
                            {
                                sumMounthNumTra += Convert.ToInt32(dt.Rows[i]["minNrTraveler"].ToString());
                                sumMinNumTra += Convert.ToInt32(dt.Rows[i]["minNrTraveler"].ToString());
                            }
                        }
                        else if (mesec == "Sum")
                            if (sumMounthNr != 0)
                            {
                                sumOccupancy = Convert.ToDecimal(sumMounth / Convert.ToDecimal(sumMounthNr) * 100);
                                sumOccupancy = Convert.ToDecimal(string.Format("{0:0.00}", sumOccupancy));
                                //ZA POZICIONIRANJE POLJA 
                                dt.Rows[i]["nr"] = sumMounth;
                                dt.Rows[i]["nr"] = sumMounth;
                                dt.Rows[i]["nrTraveler"] = sumMounthNr;
                                dt.Rows[i]["minNrTraveler"] = sumMounthNumTra;
                                dt.Rows[i]["Occupancy"] = sumOccupancy;

                                sumMounthNr = 0;
                                sumMounth = 0;
                                sumMounthNumTra = 0;

                            }


                        if (i == dt.Rows.Count - 1)
                        {
                            dt.Rows[i]["nr"] = sumAll;
                            dt.Rows[i]["nrTraveler"] = sumAllNr;
                            dt.Rows[i]["minNrTraveler"] = sumMinNumTra;
                            sumAll = 0;
                            sumAllNr = 0;
                            sumMinNumTra = 0;

                        }
                        if (mesec == "Sum")
                        {
                            //Prevod
                            dt.Rows[i][0] = dt.Rows[i - 1][0];
                            string str = "Subtotal";
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {

                                if (resxSet.GetString(str) != null)
                                    str = resxSet.GetString(str); // provera da li postoji prevod

                            }
                            dt.Rows[i][1] = str;
                        }
                        if (mesec == "Grand total sum")
                        {
                            //Prevod
                            dt.Rows[i][0] = dt.Rows[i - 1][0];
                            string str = "Total";
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {

                                if (resxSet.GetString(str) != null)
                                    str = resxSet.GetString(str); // provera da li postoji prevod

                            }


                            dt.Rows[i][1] = str;
                            if (Convert.ToDecimal(dt.Rows[i]["nrTraveler"]) != 0)
                                dt.Rows[i]["Occupancy"] = decimal.Round(Convert.ToDecimal(dt.Rows[i]["nr"]) / Convert.ToDecimal(dt.Rows[i]["nrTraveler"]) * 100, 2);

                        }
                    }
                    int nr1 = dt.Columns.Count;


                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    dt.Columns.Add(dc2);

                    DataColumn dc3 = new DataColumn("DateTo", typeof(string));
                    dc3.Caption = "Date to";
                    dc3.ColumnName = "DateTo";
                    dt.Columns.Add(dc3);

                    dt.Rows[0][nr1] = model.dateFrom.ToString("dd/MM/yyyy");
                    dt.Rows[0][nr1 + 1] = model.dateTo.ToString("dd/MM/yyyy");
                }
                // dt.Columns["mounth"].SetOrdinal(0);
                dt.Columns["dateFrom"].SetOrdinal(0);
                // dt.Columns["dayarr"].SetOrdinal(2);
                dt.Columns["Label"].SetOrdinal(1);
                dt.Columns["arrangement"].SetOrdinal(2);
                dt.Columns["minNrTraveler"].SetOrdinal(3);
                dt.Columns["nr"].SetOrdinal(5);
                if (dt.Columns["SumNr"] != null)
                {
                    dt.Columns["SumNr"].SetOrdinal(5);
                    dt.Columns["nrTraveler"].SetOrdinal(6);
                    dt.Columns["Sum"].SetOrdinal(7);

                }



                //// dt.Columns["mounth"].SetOrdinal(0);
                //dt.Columns["dateFrom"].SetOrdinal(0);
                //// dt.Columns["dayarr"].SetOrdinal(2);
                //dt.Columns["Label"].SetOrdinal(1);
                //dt.Columns["arrangement"].SetOrdinal(2);
                //dt.Columns["minNrTraveler"].SetOrdinal(3);
                //dt.Columns["nr"].SetOrdinal(5);
                //if (dt.Columns["SumNr"] != null)
                //{
                //    dt.Columns["SumNr"].SetOrdinal(5);
                //    dt.Columns["nrTraveler"].SetOrdinal(6);
                //    dt.Columns["Sum"].SetOrdinal(7);

                //}


                #region zaWizardDeo


                if (dt != null)
                {
                    if (dt.Columns.Count > 0)
                    {
                        if (dt.Columns.Contains("mounth") == true)
                        {
                            dt.Columns.Remove("mounth");
                        }
                        if (dt.Columns.Contains("dateFrom1") == true)
                        {
                            dt.Columns.Remove("dateFrom1");
                        }
                        if (dt.Columns.Contains("dateTo1") == true)
                        {
                            dt.Columns.Remove("dateTo1");
                        }
                        if (dt.Columns.Contains("DateFrom") == true)
                        {
                            dt.Columns.Remove("DateFrom");
                        }
                        if (dt.Columns.Contains("DateTo") ==true)
                        {
                            dt.Columns.Remove("DateTo");
                        }
                        if (dt.Columns.Contains("Sum") == true)
                        {
                            dt.Columns.Remove("Sum");
                        }
                        if (dt.Columns.Contains("SumNr") == true)
                        {
                            dt.Columns.Remove("SumNr");
                        }
                        if (dt.Columns.Contains("dayarr")==true)
                        { 
                        dt.Columns.Remove("dayarr");
                        }
                    }
                }

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
                            wColumn = (decimal)1.3;
                        }
                        else if (dt.Columns[i].ColumnName == "Label")
                        {
                            wColumn = (decimal)1.0;
                        }
                        else if (dt.Columns[i].ColumnName == "minNrTraveler")
                        {
                            wColumn = (decimal)0.6;
                        }
                        else if (dt.Columns[i].ColumnName == "nr")
                        {
                            wColumn = (decimal)0.7;
                        }
                        else if (dt.Columns[i].ColumnName == "nrTraveler")
                        {
                            wColumn = (decimal)0.7;
                        }
                        else if (dt.Columns[i].ColumnName == "Occupancy")
                        {
                            wColumn = (decimal)0.7;
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
            //

           

            return dt;

                #endregion
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

        public DataTable DepartureList2()
        {
            model.dateFrom = Convert.ToDateTime(dtFrom.Value);
            model.dateTo = Convert.ToDateTime(dtTo.Value);
            int nr = 0;
            DataTable dt = new DataTable();
            ArrangementBookDAO dao = new ArrangementBookDAO();
            dt = dao.DepartureList2(model.dateFrom, model.dateTo);

            if (dt != null)
            {
                if (dt.Columns.Count > 0)
                {
                    if (dt.Columns["mounth"] != null)
                        dt.Columns["mounth"].Caption = "Mounth";
                    if (dt.Columns["dateFrom"] != null)
                        dt.Columns["dateFrom"].Caption = "Date from";
                    if (dt.Columns["dayarr"] != null)
                        dt.Columns["dayarr"].Caption = "Day";
                    //if (dt.Columns["Label"] != null)
                    //    dt.Columns["Label"].Caption = "Label";
                    if (dt.Columns["arrangement"] != null)
                        dt.Columns["arrangement"].Caption = "Code arrangement";

                    if (dt.Columns["nr"] != null)
                        dt.Columns["nr"].Caption = "Booked travelers";
                    if (dt.Columns["Occupancy"] != null)
                        dt.Columns["Occupancy"].Caption = "Occupancy";


                    if (dt.Columns["nrTraveler"] != null)
                        dt.Columns["nrTraveler"].Caption = "Max travelers";

                    if (dt.Columns["minNrTraveler"] != null)
                        dt.Columns["minNrTraveler"].Caption = "Min travelers";
                    nr = dt.Columns.Count;

                }
                // sum nrTravelers
                if (dt.Rows.Count > 0)
                {
                    DataColumn dc = new DataColumn("Sum", typeof(int));
                    dc.Caption = "Sum of max";
                    // dc.DefaultValue = "";
                    dt.Columns.Add(dc);
                    DataColumn dc1 = new DataColumn("SumNr", typeof(int));
                    dc1.Caption = "Sum of pax";
                    // dc.DefaultValue = "";
                    dt.Columns.Add(dc1);

                    mounth = dt.Rows[0]["mounth"].ToString();
                    DataRow dr = dt.NewRow();
                    dr[0] = "Grand total sum";
                    dt.Rows.Add(dr);


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string mesec = dt.Rows[i]["mounth"].ToString();
                        if (mesec != mounth)
                        {
                            mounth = dt.Rows[i]["mounth"].ToString();
                            dr = dt.NewRow();

                            dr["mounth"] = "Sum";
                            dt.Rows.InsertAt(dr, i);
                        }

                    }


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string mesec = dt.Rows[i]["mounth"].ToString();
                        if (mesec != "Sum")
                        {
                            if (dt.Rows[i]["nr"].ToString() != "")
                            {
                                sumMounth += Convert.ToInt32(dt.Rows[i]["nr"].ToString());
                                sumAll += Convert.ToInt32(dt.Rows[i]["nr"].ToString());
                                // sumOccupancy +=  Convert.ToDecimal(sumAll/sumMounthNr*100); 
                            }
                            if (dt.Rows[i]["nrTraveler"].ToString() != "")
                            {
                                sumMounthNr += Convert.ToInt32(dt.Rows[i]["nrTraveler"].ToString());
                                sumAllNr += Convert.ToInt32(dt.Rows[i]["nrTraveler"].ToString());
                                // sumAllOccupancy += Convert.ToDecimal(sumAllNr / sumMounthNr * 100);

                            }

                            if (dt.Rows[i]["minNrTraveler"].ToString() != "")
                            {
                                sumMounthNumTra += Convert.ToInt32(dt.Rows[i]["minNrTraveler"].ToString());
                                sumMinNumTra += Convert.ToInt32(dt.Rows[i]["minNrTraveler"].ToString());
                            }


                        }
                        else if (mesec == "Sum")
                        {
                            if (sumMounthNr != 0)

                                sumOccupancy = Convert.ToDecimal(sumMounth / Convert.ToDecimal(sumMounthNr) * 100);
                            sumOccupancy = Convert.ToDecimal(string.Format("{0:0.00}", sumOccupancy));
                            //ZA POZICIONIRANJE POLJA 
                            dt.Rows[i]["nr"] = sumMounth;
                            dt.Rows[i]["nrTraveler"] = sumMounthNr;
                            dt.Rows[i]["Occupancy"] = sumOccupancy;
                            dt.Rows[i]["minNrTraveler"] = sumMounthNumTra;

                            sumMounthNr = 0;
                            sumMounth = 0;
                            sumMounthNumTra = 0;
                            //sumOccupancy = 0;

                            //Convert.ToDecimal(sumMounth / sumMounthNr * 100);
                        }


                        if (i == dt.Rows.Count - 1)
                        {

                            dt.Rows[i]["nr"] = sumAll;
                            dt.Rows[i]["nrTraveler"] = sumAllNr;
                            dt.Rows[i]["Occupancy"] = sumAllOccupancy;
                            dt.Rows[i]["minNrTraveler"] = sumMinNumTra;
                            sumAll = 0;
                            sumAllNr = 0;
                            sumAllOccupancy = 0;
                            sumMinNumTra = 0;

                        }
                        if (mesec == "Sum")
                        {
                            //Prevod
                            dt.Rows[i][0] = dt.Rows[i - 1][0];
                            string str = "Subtotal";
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {

                                if (resxSet.GetString(str) != null)
                                    str = resxSet.GetString(str); // provera da li postoji prevod

                            }
                            dt.Rows[i][1] = str;
                            //dt.Rows[i][1] = "";

                        }
                        if (mesec == "Grand total sum")
                        {
                            //Prevod
                            dt.Rows[i][0] = dt.Rows[i - 1][0];
                            string str = "Total";
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {

                                if (resxSet.GetString(str) != null)
                                    str = resxSet.GetString(str); // provera da li postoji prevod

                            }
                            dt.Rows[i][1] = str;
                            //dt.Rows[i][1] = "";

                            if (Convert.ToDecimal(dt.Rows[i]["nrTraveler"]) != 0)
                                dt.Rows[i]["Occupancy"] = decimal.Round(Convert.ToDecimal(dt.Rows[i]["nr"]) / Convert.ToDecimal(dt.Rows[i]["nrTraveler"]) * 100, 2);


                        }
                    }
                    int nr1 = dt.Columns.Count;


                    DataColumn dc2 = new DataColumn("DateFrom", typeof(string));
                    dc2.Caption = "Date from";
                    dc2.ColumnName = "DateFrom";
                    dt.Columns.Add(dc2);

                    DataColumn dc3 = new DataColumn("DateTo", typeof(string));
                    dc3.Caption = "Date to";
                    dc3.ColumnName = "DateTo";
                    dt.Columns.Add(dc3);

                    dt.Rows[0][nr1] = model.dateFrom.ToString("dd/MM/yyyy");
                    dt.Rows[0][nr1 + 1] = model.dateTo.ToString("dd/MM/yyyy");
                }

                // dt.Columns["mounth"].SetOrdinal(0);
                dt.Columns["dateFrom"].SetOrdinal(0);
                // dt.Columns["dayarr"].SetOrdinal(2);
                dt.Columns["Label"].SetOrdinal(1);
                dt.Columns["arrangement"].SetOrdinal(2);
                dt.Columns["minNrTraveler"].SetOrdinal(3);
                dt.Columns["nr"].SetOrdinal(5);
                if (dt.Columns["SumNr"] != null)
                {
                    dt.Columns["SumNr"].SetOrdinal(5);
                    dt.Columns["nrTraveler"].SetOrdinal(6);
                    dt.Columns["Sum"].SetOrdinal(7);

                }

                if (dt != null)
                {
                    if (dt.Columns.Count > 0)
                    {
                        if (dt.Columns.Contains("mounth") == true)
                        {
                            dt.Columns.Remove("mounth");
                        }
                        if (dt.Columns.Contains("dateFrom1") == true)
                        {
                            dt.Columns.Remove("dateFrom1");
                        }
                        if (dt.Columns.Contains("dateTo1") == true)
                        {
                            dt.Columns.Remove("dateTo1");
                        }
                        if (dt.Columns.Contains("DateFrom") == true)
                        {
                            dt.Columns.Remove("DateFrom");
                        }
                        if (dt.Columns.Contains("DateTo") == true)
                        {
                            dt.Columns.Remove("DateTo");
                        }
                        if (dt.Columns.Contains("Sum") == true)
                        {
                            dt.Columns.Remove("Sum");
                        }
                        if (dt.Columns.Contains("SumNr") == true)
                        {
                            dt.Columns.Remove("SumNr");
                        }
                        if (dt.Columns.Contains("dayarr") == true)
                        {
                            dt.Columns.Remove("dayarr");
                        }
                    }
                }

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
                            wColumn = (decimal)1.3;
                        }
                        else if (dt.Columns[i].ColumnName == "Label")
                        {
                            wColumn = (decimal)1.0;
                        }
                        else if (dt.Columns[i].ColumnName == "minNrTraveler")
                        {
                            wColumn = (decimal)0.6;
                        }
                        else if (dt.Columns[i].ColumnName == "nr")
                        {
                            wColumn = (decimal)0.7;
                        }
                        else if (dt.Columns[i].ColumnName == "nrTraveler")
                        {
                            wColumn = (decimal)0.7;
                        }
                        else if (dt.Columns[i].ColumnName == "Occupancy")
                        {
                            wColumn = (decimal)0.7;
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
            //

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
                    if (Convert.ToDecimal(e.CellElement.Value) > (decimal)6.0)
                    {
                        e.CellElement.BackColor = System.Drawing.Color.Red;
                        e.CellElement.NumberOfColors = 1;
                        e.CellElement.DrawFill = true;
                    }
                    else if (Convert.ToDecimal(e.CellElement.Value) < (decimal)6.0)
                    {
                        e.CellElement.BackColor = System.Drawing.Color.Yellow;
                        e.CellElement.NumberOfColors = 1;
                        e.CellElement.DrawFill = true;
                    }
                    else if (Convert.ToDecimal(e.CellElement.Value) == (decimal)6.0)
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
                                wColumn = (decimal)1.3;
                            }
                            else
                                wColumn = (decimal)1;
                            m.widthColumn = wColumn;
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
