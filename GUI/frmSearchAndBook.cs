using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Outlook = Microsoft.Office.Interop.Outlook;
using BIS.Business;
using BIS.Model;
using Telerik.WinControls.UI;
using System.IO;
using System.Diagnostics;


namespace GUI
{
    public partial class frmSearchAndBook : Telerik.WinControls.UI.RadForm
    {
        
        List<ArrangementThemeTripModel> arrangeThemeTrip = new List<ArrangementThemeTripModel>();
        List<ArrangementBoardingPointModel> arrangeBoardingPoint = new List<ArrangementBoardingPointModel>();
        List<ArrangementTargetGroupModel> arrangeTargetGroup = new List<ArrangementTargetGroupModel>();

        List<int> labels;

        int idCountry = 0;
        string idArticle1 = "";
        string idArticle2 = "";
        string idArticle3 = "";
        private List<IModel> modelData;
        private ArrangementBookModel _selectedRowSeachBook;
        private ArrangementBookModel _clickedSearchBook;
        public SearchBookModel searchBookModel;
        private string layout;

        

        public frmSearchAndBook()
        {
            InitializeComponent();
        }

        private void frmSearchAndBook_Load(object sender, EventArgs e)
        {
            this.Icon = Login.iconForm;

            layout = MainForm.gridFiltersFolder + "\\layoutSearchBook.xml";

            ddlStatus.DataSource = null;
             ddlStatus.DataSource = new ArrangementStatusBUS().GetAllArrangementStatus();
             ddlStatus.DisplayMember = "nameArrangementStatus";
             ddlStatus.ValueMember = "nameArrangementStatus";

            if(ddlStatus != null)
                if(ddlStatus.Items.Count>=1)
            ddlStatus.SelectedIndex = 1; 
           




            getAllThemeTrips();

            loadLabel();

            setTranslation();

            pickerFromDate.Value = DateTime.Now;
            pickerToDate.Value = DateTime.Now;
        }

        private void setTranslation()
        {

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(rbRollator.Text) != null)
                    rbRollator.Text = resxSet.GetString(rbRollator.Text);
                if (resxSet.GetString(rbArmSometimes.Text) != null)
                    rbArmSometimes.Text = resxSet.GetString(rbArmSometimes.Text);
                if (resxSet.GetString(lblStatus.Text) != null)
                    lblStatus.Text = resxSet.GetString(lblStatus.Text);
                if (resxSet.GetString(btnExit.Text) != null)
                    btnExit.Text = resxSet.GetString(btnExit.Text);
                if (resxSet.GetString(lblCountry.Text) != null)
                    lblCountry.Text = resxSet.GetString(lblCountry.Text);
                if (resxSet.GetString(lblFromDate.Text) != null)
                    lblFromDate.Text = resxSet.GetString(lblFromDate.Text);
                if (resxSet.GetString(lblToDate.Text) != null)
                    lblToDate.Text = resxSet.GetString(lblToDate.Text);  
                if (resxSet.GetString(btnDo.Text) != null)
                    btnDo.Text = resxSet.GetString(btnDo.Text);
                if (resxSet.GetString(lblArticle1.Text) != null)
                    lblArticle1.Text = resxSet.GetString(lblArticle1.Text);
                if (resxSet.GetString(lblArticle2.Text) != null)
                    lblArticle2.Text = resxSet.GetString(lblArticle2.Text);
                if (resxSet.GetString(lblArticle3.Text) != null)
                    lblArticle3.Text = resxSet.GetString(lblArticle3.Text);
                if (resxSet.GetString(rbWheelchair.Text) != null)
                    rbWheelchair.Text = resxSet.GetString(rbWheelchair.Text);
                if (resxSet.GetString(rbRollator.Text) != null)
                    rbRollator.Text = resxSet.GetString(rbRollator.Text);
                if (resxSet.GetString(rbArmSometimes.Text) != null)
                    rbArmSometimes.Text = resxSet.GetString(rbArmSometimes.Text);
                if (resxSet.GetString(rbAnchorage.Text) != null)
                    rbAnchorage.Text = resxSet.GetString(rbAnchorage.Text); 
                if (resxSet.GetString(rbNone.Text) != null)
                    rbNone.Text = resxSet.GetString(rbNone.Text);

                
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDo_Click(object sender, EventArgs e)
        {
            SearchBookBUS sbb = new SearchBookBUS();
            DateTime date1 = new DateTime(2010, 8, 18);
            DateTime date2 = new DateTime(2020, 8, 18);
            //string filter= null;
            rgvSearchBook.DataSource = null;
            int help = 0;

            if (rbWheelchair.CheckState == CheckState.Checked)
                help = 1;
            else if(rbRollator.CheckState == CheckState.Checked)
                  help = 2;

            else  if (rbArmSometimes.CheckState == CheckState.Checked)
                  help = 3;
            else  if (rbAnchorage.CheckState == CheckState.Checked)
                  help = 4;


            List<string> articles= new List<string>();
            if(idArticle1!="")
                articles.Add(idArticle1);
            if (idArticle2 != "")
                articles.Add(idArticle2);
            if (idArticle3 != "")
                articles.Add(idArticle3);

            List<int> themeTrip = new List<int>();

            foreach(Control c in panelThemeTrip.Controls)
            {
                RadCheckBox chk = (RadCheckBox)c;
                if(chk.CheckState == CheckState.Checked)
                {
                    themeTrip.Add(Convert.ToInt32(chk.Name));
                }
            }            

            
            List<SearchBookModel> lista = sbb.GetFilteredAArrangementsBook(pickerFromDate.Value, pickerToDate.Value, idCountry, ddlStatus.SelectedItem.Value.ToString(), help, themeTrip, articles,labels);

            if(lista!=null)
                if(lista.Count>0)

            rgvSearchBook.DataSource = lista;
            if (lista != null)
            {
                if (lista.Count > 0)
                {
                    hideCollumns();
                }
            }

            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Nothing for print preview");
            }
               
        }

        private void btnCountry_Click(object sender, EventArgs e)
        {
            CountryBUS accBUS = new CountryBUS();
            List<IModel> am = new List<IModel>();

            am = accBUS.GetCountries();


            var dlgClient = new GridLookupForm(am, "Country");
            if (dlgClient.ShowDialog(this) == DialogResult.Yes)
            {
                CountryModel okm = new CountryModel();
                okm = (CountryModel)dlgClient.selectedRow;
                txtCountry.Text = okm.nameCountry;
                idCountry = okm.idCountry;
            }

        }



        private void rgvSearchBook_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {

            if (rgvSearchBook != null)
            {
                if (rgvSearchBook.Columns.Count > 0)
                {
                    foreach (var column in rgvSearchBook.Columns)
                    {
                        using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                        {
                            if (resxSet.GetString(column.HeaderText) != null)
                                column.HeaderText = resxSet.GetString(column.HeaderText);
                        }

                        column.Width = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 100);

                    }
                }
            }

            int help  =0;
            if (rbWheelchair.CheckState == CheckState.Checked)
                help = 1;
            else if (rbRollator.CheckState == CheckState.Checked)
                help = 2;

            else if (rbArmSometimes.CheckState == CheckState.Checked)
                help = 3;
            else if (rbAnchorage.CheckState == CheckState.Checked)
                help = 4;
           


            if (this.rgvSearchBook.Columns != null)
            {
                //this.gridOpenLines.Columns["chk"].IsVisible = false;

                foreach (GridViewColumn col in rgvSearchBook.Columns)
                {
                        col.ReadOnly = true;
                        col.IsVisible = true;
                        if (col.Name == "wheelchair" || col.Name == "Rollator" || col.Name == "armSometimes" || col.Name == "anchorage")
                        {
                            if (col.Name == "wheelchair" && (help == 1 || help == 0))
                            {
                                col.IsVisible = true;
                            }
                            else if (col.Name == "Rollator" && (help == 2 || help == 0))
                            {
                                col.IsVisible = true;
                            }
                            else if (col.Name == "armSometimes" && (help == 3 || help == 0))
                            {
                                col.IsVisible = true;
                            }
                            else if (col.Name == "anchorage" && (help == 4 || help == 0))
                            {
                                col.IsVisible = true;
                            }
                            else
                                col.IsVisible = false;
                        }
                        //if (col.Name == "select")
                        //{
                        //    col.ReadOnly = false;
                            
                        //}


                        //col.Width = 100;
                }

                //if (rgvSearchBook.Columns["select"] != null)
                //    rgvSearchBook.Columns["select"].IsVisible = true;


                if (File.Exists(layout))
                {
                    rgvSearchBook.LoadLayout(layout);
                }

                if (rgvSearchBook.Columns["idArrangement"] != null)
                    rgvSearchBook.Columns["idArrangement"].IsVisible = false;

             

               
            }
        }



        private void rgvSearchBook_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Column.Name == "dtFromArrangement" || e.Column.Name == "dtToArrangement")
                
            {
                if (e.Column.IsVisible == true)
                {
                    try
                    {                       
                        DateTime temp = DateTime.Parse(e.CellElement.Value.ToString());
                        e.CellElement.Text = temp.ToString("dd-MM-yyyy");
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }


        private void btnArticle1_Click(object sender, EventArgs e)
        {
            SearchBookBUS sBUS = new SearchBookBUS();
            List<IModel> ar = new List<IModel>();


            List<int> themeTrip = new List<int>();

            foreach (Control c in panelThemeTrip.Controls)
            {
                RadCheckBox chk = (RadCheckBox)c;
                if (chk.CheckState == CheckState.Checked)
                {
                    themeTrip.Add(Convert.ToInt32(chk.Name));
                }
            }

            ar = sBUS.GetAllRooms(pickerFromDate.Value, pickerToDate.Value,  idCountry, ddlStatus.SelectedItem.Value.ToString(), themeTrip);


            var dlgClient = new GridLookupForm(ar, "Article1");
             if (dlgClient.ShowDialog(this) == DialogResult.Yes)
            {
                ArticalModelRooms amr = (ArticalModelRooms)dlgClient.selectedRow;
                txtArticle1.Text = amr.nameArtikal;
                idArticle1 = amr.codeArticle;
            }
        }

        private void btnArticle2_Click(object sender, EventArgs e)
        {
            SearchBookBUS sBUS = new SearchBookBUS();
            List<IModel> ar = new List<IModel>();

            List<int> themeTrip = new List<int>();

            foreach (Control c in panelThemeTrip.Controls)
            {
                RadCheckBox chk = (RadCheckBox)c;
                if (chk.CheckState == CheckState.Checked)
                {
                    themeTrip.Add(Convert.ToInt32(chk.Name));
                }
            }

            ar = sBUS.GetAllRoomsWithArticle(pickerFromDate.Value, pickerToDate.Value, idCountry, ddlStatus.SelectedItem.Value.ToString(), themeTrip);


            var dlgClient = new GridLookupForm(ar, "Article2");
            if (dlgClient.ShowDialog(this) == DialogResult.Yes)
            {
                ArticalModelRooms amr = (ArticalModelRooms)dlgClient.selectedRow;
                txtArticle2.Text = amr.nameArtikal;
                idArticle2 = amr.codeArticle;
            }
        }

        private void btnArticle3_Click(object sender, EventArgs e)
        {
            SearchBookBUS sBUS = new SearchBookBUS();
            List<IModel> ar = new List<IModel>();

            List<int> themeTrip = new List<int>();

            foreach (Control c in panelThemeTrip.Controls)
            {
                RadCheckBox chk = (RadCheckBox)c;
                if (chk.CheckState == CheckState.Checked)
                {
                    themeTrip.Add(Convert.ToInt32(chk.Name));
                }
            }

            ar = sBUS.GetAllRoomsWithArticle(pickerFromDate.Value, pickerToDate.Value, idCountry, ddlStatus.SelectedItem.Value.ToString(), themeTrip);


            var dlgClient = new GridLookupForm(ar, "Article2");
            if (dlgClient.ShowDialog(this) == DialogResult.Yes)
            {
                ArticalModelRooms amr = (ArticalModelRooms)dlgClient.selectedRow;
                txtArticle3.Text = amr.nameArtikal;
                idArticle3 = amr.codeArticle;
            }
        }

     
        private void getAllThemeTrips()
        {
           
            
                panelThemeTrip.Controls.Clear();
                ArrangementThemeTripBUS abpb = new ArrangementThemeTripBUS();
                List<ThemeTripModel> arrThemeTrip = new List<ThemeTripModel>();
                arrThemeTrip = abpb.GetAllThemeTrip();

                int lastBottom = 15;
                if (arrThemeTrip != null)
                    if (arrThemeTrip.Count > 0)
                        for (int i = 0; i < arrThemeTrip.Count; i++)
                        {
                            RadCheckBox chk = new RadCheckBox();
                            chk.Font = new Font("Verdana", 9);
                            chk.CheckStateChanged += radCheckBoxTT_CheckStateChanged;
                            chk.Name = arrThemeTrip[i].idThemeTrip.ToString();
                            chk.Text = arrThemeTrip[i].nameThemeTrip.ToString();
                            chk.Location = new Point(15, lastBottom);                         
                            if (arrangeThemeTrip.Find(s => s.idThemeTrip == Convert.ToInt32(chk.Name)) != null)
                            {
                                chk.CheckState = CheckState.Checked;
                            }
                            lastBottom = lastBottom + 20;
                            panelThemeTrip.Controls.Add(chk);
                        }
            
           
            
               // getCheckedThemeTrip();
            
        }

        private void radCheckBoxTT_CheckStateChanged(object sender, EventArgs e)
        {
            RadCheckBox chk = (RadCheckBox)sender;
            if (chk.CheckState == CheckState.Checked)
            {
                if (arrangeThemeTrip.Find(s => s.idThemeTrip == Convert.ToInt32(chk.Name)) == null)
                {
                    ArrangementThemeTripModel abpm = new ArrangementThemeTripModel();
                    abpm.idThemeTrip = Convert.ToInt32(chk.Name);
                    //abpm.idArrangement = arrange.idArrangement;
                    arrangeThemeTrip.Add(abpm);
                }
            }
            else
            {
                arrangeThemeTrip.Remove(arrangeThemeTrip.Find(s => s.idThemeTrip == Convert.ToInt32(chk.Name)));
            }
        }

        private void hideCollumns()
        {
            //rgvSearchBook.Columns["Wheelchair"].IsVisible = false;
            //rgvSearchBook.Columns["Rollator"].IsVisible = false;
            //rgvSearchBook.Columns["ArmSometimes"].IsVisible = false;
            //rgvSearchBook.Columns["Anchorage"].IsVisible = false;
            rgvSearchBook.Columns["ArticleId1"].IsVisible = false;
            rgvSearchBook.Columns["ArticleId2"].IsVisible = false;
            rgvSearchBook.Columns["ArticleId3"].IsVisible = false;
            //rgvSearchBook.Columns["ArticleId3"].IsVisible = false;


            //if (rbWheelchair.IsChecked)
            //{
            //    rgvSearchBook.Columns["Wheelchair"].IsVisible = true;

            //}
            //if (rbArmSometimes.IsChecked)
            //{
            //    rgvSearchBook.Columns["Rollator"].IsVisible = true;
            //}
            //if (rbAnchorage.IsChecked)
            //{
            //    rgvSearchBook.Columns["ArmSometimes"].IsVisible = true;
            //}
            //if (rbRollator.IsChecked)
            //{
            //    rgvSearchBook.Columns["Anchorage"].IsVisible = true;
            //}
            if (txtArticle1.Text != "")
            {
                rgvSearchBook.Columns["ArticleId1"].IsVisible = true;
                rgvSearchBook.Columns["ArticleId1"].HeaderText = txtArticle1.Text;
            }
            if (txtArticle2.Text != "")
            {
                rgvSearchBook.Columns["ArticleId2"].IsVisible = true;
                rgvSearchBook.Columns["ArticleId2"].HeaderText = txtArticle2.Text;

            }
            if (txtArticle3.Text != "")
            {
                rgvSearchBook.Columns["ArticleId3"].IsVisible = true;
                rgvSearchBook.Columns["ArticleId3"].HeaderText = txtArticle3.Text;

            }

        }

        
        private void resetValues()
        {
            idArticle1 = "";
            idArticle2 = "";
            idArticle3 = "";
            txtArticle1.Text = "";
            txtArticle2.Text = "";
            txtArticle3.Text = "";
        }

       
        private void txtCountry_TextChanged(object sender, EventArgs e)
        {
            resetValues();
        }

        private void pickerFromDate_ValueChanged(object sender, EventArgs e)
        {
            resetValues();
        }

        private void pickerToDate_ValueChanged(object sender, EventArgs e)
        {
            resetValues();
        }

        private void ddlStatus_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            resetValues();
        }

        private void btnClearArticle1_Click(object sender, EventArgs e)
        {           
                txtArticle1.Text = "";
                idArticle1 = "";
                txtArticle2.Focus();          
        }

        private void btnClearArticle2_Click(object sender, EventArgs e)
        {
                txtArticle2.Text = "";
                idArticle2 = "";
                txtArticle2.Focus();
        }

        private void btnClearArticle3_Click(object sender, EventArgs e)
        {
                txtArticle3.Text = "";
                idArticle3 = "";
        }

        private void btnClearCountry_Click(object sender, EventArgs e)
        {
            txtCountry.Text = "";
            idCountry = 0;
        }


        private void rgvSearchBook_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            GridViewRowInfo info = this.rgvSearchBook.CurrentRow;
            SearchBookModel sbm = new SearchBookModel();
            sbm = (SearchBookModel)info.DataBoundItem;

            SearchBookModel selectedSearchBook = (SearchBookModel)info.DataBoundItem;

            ArrangementModel amodel = new ArrangementModel();
            amodel = new ArrangementBUS().GetArrangementById(sbm.idArrangement);

            if (info != null && e.RowIndex >= 0)
                if (selectedSearchBook != null)
                {
                    frmArrangementBook frm = new frmArrangementBook(amodel);
                   
                    frm.ShowDialog();

                }

        }

        private void txtCountry_Leave(object sender, EventArgs e)
        {
            if (txtCountry.Text != "")
            {
                CountryBUS cb = new CountryBUS();
                CountryModel cm = new CountryModel();
                cm = cb.GetCountryByCodeOrName(txtCountry.Text);

                if (cb != null)
                {
                    if (cm.idCountry != 0)
                    {
                        txtCountry.Text = cm.nameCountry;
                        idCountry = cm.idCountry;
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You didn't fill right the country.");                      
                    }
                }

                else
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You didn't fill right the country.");
                   
                }
            }
        }

        private void radMenuItemSaveTasksLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layout))
            {
                File.Delete(layout);
            }
            rgvSearchBook.SaveLayout(layout);

            RadMessageBox.Show("Layout Saved");
        }

        private void rgvSearchBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.rgvSearchBook.CurrentRow.DataBoundItem != null)
            {
                if (e.KeyData == Keys.Enter)
                {
                    GridViewRowInfo info = this.rgvSearchBook.CurrentRow;
                    SearchBookModel sbm = new SearchBookModel();
                    sbm = (SearchBookModel)info.DataBoundItem;

                    SearchBookModel selectedSearchBook = (SearchBookModel)info.DataBoundItem;

                    ArrangementModel amodel = new ArrangementModel();
                    amodel = new ArrangementBUS().GetArrangementById(sbm.idArrangement);

                    frmArrangementBook frm = new frmArrangementBook(amodel);

                   frm.ShowDialog();

                    return;
                }
            }
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

    }
}
