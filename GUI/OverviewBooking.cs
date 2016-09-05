using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Model;
using BIS.Business;
using System.Resources;
using System.IO;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;
using Telerik.WinControls.UI.Export.ExcelML;


namespace GUI
{
    public partial class OverviewBooking : Telerik.WinControls.UI.RadForm
    {
        private List<ArrangementBookStatusModel> OverviewBookingList;
        string layout;
        DateTime dateFrom;
        DateTime dateTo;
        int status = -1;
        List<int> labels;
        List<ArrangementBookStatusModel> statusList;
        int idArrangement = -1;
        string travelPapers = "";

        public OverviewBooking()
        {
            InitializeComponent();

        }

        private void OverviewBooking_Load(object sender, EventArgs e)
        {
            OverviewBookingList = new ArrangementBookStatusBUS().GetAllArrangementBookStatus();
            ddlStatus.DataSource = OverviewBookingList;
            ddlStatus.DisplayMember = "nameStatus";
            ddlStatus.ValueMember = "idStatus";
            ddlStatus.SelectedIndex = -1;
            ddlStatus.Text = "";
            dtFrom.Value = DateTime.Now;
            dtTo.Value = DateTime.Now;

            layout = MainForm.gridFiltersFolder + "\\layoutVolOverviewBooking.xml";

            setTranslation();
            loadLabel();
            fillPanelTravelPapers();
            this.Icon = Login.iconForm;


        }
        #region labels
        private void loadLabel()
        {

            int Y = 0;
            int X = 0;
            RadRadioButton rckExp = new RadRadioButton();
            rckExp.Font = new Font("Verdana", 9);
            rckExp.Name = "chkLabel";
            rckExp.Text = "None";
            rckExp.Location = new Point(0, Y);
            rckExp.CheckStateChanged += rck_CheckStateChanged;
            rckExp.AutoSize = true;
            rckExp.IsChecked = true;

            Y = Y + 3 + rckExp.Height;

            panelLabels.Controls.Add(rckExp);

            Y = 21;

            for (int i = 0; i < Login._arrLabels.Count; i++)
            {
                rckExp = new RadRadioButton();
                rckExp.Font = new Font("Verdana", 9);
                rckExp.Name = "chkLabel" + Login._arrLabels[i].idLabel.ToString();
                rckExp.Text = Login._arrLabels[i].nameLabel;
                rckExp.Location = new Point(0, Y);
                rckExp.CheckStateChanged += rck_CheckStateChanged;
                rckExp.AutoSize = true;
                Y = Y + 3 + rckExp.Height;

                panelLabels.Controls.Add(rckExp);

            }
        }

        private void rck_CheckStateChanged(object sender, EventArgs e)
        {

            RadRadioButton rb = (RadRadioButton)sender;
            labels = new List<int>();
            if (rb.Name.Replace("chkLabel", "") != "")
                labels.Add(Convert.ToInt32(rb.Name.Replace("chkLabel", "")));
            else
            {
                //Za odredjene labele uzima ID i rpokazuje u lookup-u
                for (int i = 0; i < Login._arrLabels.Count; i++)
                {

                    labels.Add(Login._arrLabels[i].idLabel);
                }
            }
        }
        #endregion

        #region travelPapers
        private void fillPanelTravelPapers()
        {
            ArrangementBookTravelPapersBUS arrBookTravelPapers = new ArrangementBookTravelPapersBUS();
            List<ArrangementBookTravelPapersModel> arrBookTravelPapersModel = new List<ArrangementBookTravelPapersModel>();
            arrBookTravelPapersModel = arrBookTravelPapers.GetAllTravelPapers(Login._user.lngUser);
            if (arrBookTravelPapersModel != null)
            {
                if (arrBookTravelPapersModel.Count > 0)
                {
                    int Y = 0;
                    ArrangementBookTravelPapersModel tpmodel = new ArrangementBookTravelPapersModel();
                    tpmodel.idTravelPapers = 0;
                    tpmodel.nameTravelPapers = "None";
                    arrBookTravelPapersModel.Insert(0, tpmodel);
                    for (int i = 0; i < arrBookTravelPapersModel.Count; i++)
                    {
                        RadRadioButton rbn = new RadRadioButton();
                        rbn.Font = new Font("Verdana", 9);
                        rbn.Name = "rbnTravelPapers" + arrBookTravelPapersModel[i].idTravelPapers.ToString();
                        rbn.Text = arrBookTravelPapersModel[i].nameTravelPapers;
                        rbn.CheckStateChanging += rbnTravelPapers_CheckStateChanging;
                        rbn.CheckStateChanged += rbnTravelPapers_CheckStateChanged;
                        rbn.Location = new Point(0, Y);
                        rbn.AutoSize = true;
                        Y = Y + 3 + rbn.Height;
                        if (rbn.Text == "None")
                        {
                            rbn.IsChecked = true;
                        }
                        panelTravelPapers.Controls.Add(rbn);
                    }
                }
            }
        }
        private void rbnTravelPapers_CheckStateChanged(object sender, EventArgs args)
        {
            RadRadioButton rrb = (RadRadioButton)sender;
            travelPapers = rrb.Text;

        }
        private void rbnTravelPapers_CheckStateChanging(object sender, CheckStateChangingEventArgs args)
        {
            RadRadioButton rrb = (RadRadioButton)sender;
            Control[] statusId2 = this.Controls.Find("rbnStatus2", true);
            if (statusId2 != null)
            {
                if (statusId2.Length > 0)
                {
                    RadRadioButton rrbStatusFinal = (RadRadioButton)statusId2[0];
                    if (rrbStatusFinal.CheckState == CheckState.Checked && Convert.ToInt32(rrb.Name.Replace("rbnTravelPapers", "")) != 1)
                    {
                        args.Cancel = true;
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You cannot check final status if status for travel papers isn't AF received!");

                    }

                }
            }
        }

        #endregion


        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(btDo.Text) != null)
                    btDo.Text = resxSet.GetString(btDo.Text);

                if (resxSet.GetString(lblDateFrom.Text) != null)
                    lblDateFrom.Text = resxSet.GetString(lblDateFrom.Text);

                if (resxSet.GetString(lblDateTo.Text) != null)
                    lblDateTo.Text = resxSet.GetString(lblDateTo.Text);

                if (resxSet.GetString(lblOverviewBooking.Text) != null)
                    lblOverviewBooking.Text = resxSet.GetString(lblOverviewBooking.Text);

                if (resxSet.GetString(lblStatusOB.Text) != null)
                    lblStatusOB.Text = resxSet.GetString(lblStatusOB.Text);
            }
        }

        private void rmiOverviewBooking_Click(object sender, EventArgs e)
        {
            if (File.Exists(layout))
            {
                File.Delete(layout);
            }
            rgvOverviewBooking.SaveLayout(layout);

            RadMessageBox.Show("Layout Saved");
        }
        private void rgvOverviewBooking_DataBindingComplete(object sender, Telerik.WinControls.UI.GridViewBindingCompleteEventArgs e)
        {
            if (rgvOverviewBooking != null)
            {
                if (rgvOverviewBooking.Columns != null)
                {
                    if (rgvOverviewBooking.Columns.Count > 0)
                    {

                        foreach (var column in rgvOverviewBooking.Columns)
                        {
                            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                            {
                                if (resxSet.GetString(column.HeaderText) != null)
                                    column.HeaderText = resxSet.GetString(column.HeaderText);
                            }

                           
                          
                            column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 95);
                            if (column.GetType() == typeof(GridViewDateTimeColumn))
                            {
                                if (column.Name.ToLower() != "dtUserModified".ToLower() && column.Name.ToLower() != "dtUserCreated".ToLower())
                                {
                                    column.FormatString = "{0: dd-MM-yyyy}";
                                }
                            }
                        }
                       
                    }

                    if (travelPapers == "None")
                   
                    {
                        rgvOverviewBooking.Columns["nameTravelPapers"].IsVisible = false;
                    }
                    else
                    {
                        rgvOverviewBooking.Columns["nameTravelPapers"].IsVisible = true;
                    }
                   
                }
                //btnVolAvaPrint.Visible = true;
            }
            rgvOverviewBooking.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            if (File.Exists(layout))
            {
                rgvOverviewBooking.LoadLayout(layout);
            }
        }
        private bool isValidDataOnForm()
        {
            bool value = true;
            if (dtFrom.Value != null)
            {
                dateFrom = dtFrom.Value;
            }
            else
            {
                value = false;
            }
            if (dtTo.Value != null)
            {
                dateTo = dtTo.Value;
            }
            else
            {
                value = false;
            }

            if (ddlStatus.SelectedIndex != -1)
            {
                status = (int)ddlStatus.SelectedValue;
            }

            return value;

        }
        private void btDo_Click(object sender, EventArgs e)
        {
            try
            {
                if (isValidDataOnForm())
                {
                    OverviewBookingBUS ob = new OverviewBookingBUS();
                    List<OverviewBookingFormModel> list = new List<OverviewBookingFormModel>();

                    //if(travelPapers=="None")
                    ////if (this.panelTravelPapers.Controls.Find("rbnTravelPapers",true)=="None")
                    //{
                    //    rgvOverviewBooking.Columns["nameTravelPapers"].IsVisible = false;
                    //}

                    list = ob.getOverviewBooking(dateFrom, dateTo, status, idArrangement, labels, travelPapers);

                    if (list != null)
                    {
                        rgvOverviewBooking.DataSource = list;
                        btnExcell.Visible = true;
                        btnPrint.Visible = true;
                    }
                    else
                    {
                        rgvOverviewBooking.DataSource = null;
                        btnExcell.Visible = false;
                        btnPrint.Visible = false;
                        translateRadMessageBox rs = new translateRadMessageBox();
                        rs.translateAllMessageBox("No data for curent conditions!");

                    }
                }
            } catch(Exception ex)
            {
                
            }

        }

        private void rbCountry_Click(object sender, EventArgs e)
        {
            ArrangementBUS accBUS = new ArrangementBUS();
            List<IModel> am = new List<IModel>();

            am = accBUS.GetAllArrangements();


            using (var dlgClient = new GridLookupForm(am, "ArrangementName"))
            {

                if (dlgClient.ShowDialog(this) == DialogResult.Yes)
                {
                    ArrangementModel okm = new ArrangementModel();
                    okm = (ArrangementModel)dlgClient.selectedRow;
                    idArrangement = okm.idArrangement;
                    txtArrangement.Text = okm.nameArrangement;
                }
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            txtArrangement.Text = "";
            idArrangement = -1;
        }

        private void btnExcell_Click(object sender, EventArgs e)
        {
            if (rgvOverviewBooking.DataSource != null)
                if (rgvOverviewBooking.Columns.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.InitialDirectory = @"C:\";
                    sfd.RestoreDirectory = true;
                    sfd.FileName = "OverviewBooking";
                    sfd.DefaultExt = "xls";
                    sfd.Filter = "Excel file (*.xls) | *.xls";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string path = sfd.FileName;
                        ExportToExcelML exporter = new ExportToExcelML(this.rgvOverviewBooking);
                        exporter.SheetMaxRows = ExcelMaxRows._1048576;

                        exporter.RunExport(path);
                        RadMessageBox.Show("Export to excel!");


                    }
                }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (rgvOverviewBooking != null)
                if (rgvOverviewBooking.Columns.Count > 0)
                {

                    this.rgvOverviewBooking.PrintPreview();
                }
                else
                {
                    RadMessageBox.Show("Nothing for print preview!");
                }
        }


    }


}

