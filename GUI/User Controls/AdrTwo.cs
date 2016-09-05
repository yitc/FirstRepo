using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BIS.Business;
using BIS.Model;
using System.Resources;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace GUI.User_Controls
{
    public partial class AdrTwo : UserControl
    {
        //public AdrTwo()
        //{
           AddressDataBUS adressBUS;
        //save last data
        private string address1;
        private string address2;
        private string address3;
        private string city1;
        private string city2;
        private string city3;
        private string country1;
        private string country2;
        private string country3;
        private string houseno1;
        private string houseno2;
        private string houseno3;
        private string zip1;
        private string zip2;
        private string zip3;
        private string ext1;
        private string ext2;
        private string ext3;
        private Boolean isInternational1;
        private Boolean isInternational2;
        private Boolean isInternational3;

        private Boolean isSaved = false;


        public bool showBillingAddress { get; set; }
        public bool showEmergencyAddress { get; set; }
        public AdrTwo()
        {

            showBillingAddress = true;
            showEmergencyAddress = true;
            InitializeComponent();
            adressBUS = new AddressDataBUS();
            setTranslation();
            RadMessageBox.SetThemeName("Windows8");


        }

        private void btn_adr_get_Click(object sender, EventArgs e)
        {
            string postcode = txt_adr_zip.Text.Trim();
            postcode = postcode.Replace(" ", "");
            string houseno = txt_adr_houseno.Text.Trim();



            if (rad_adr_nl.IsChecked == true)
            {

                if (postcode != "" && houseno != "")
                {
                    int houseno_num = Int32.Parse(houseno);
                    if (postcode.Length == 6)
                    {
                        int digitAdr;
                        bool isNumericAdr = int.TryParse(postcode.Substring(0, 4), out digitAdr);
                        string letteradr = postcode.Substring(4, 2);
                        int indicator = (houseno_num % 2 == 1 ? 0 : 1);

                        if (isNumericAdr == true)
                        {
                            DataTable dt = adressBUS.GetAddress(letteradr.ToUpper(), indicator, digitAdr, houseno_num);

                            if (dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.Rows[0];

                                txt_adr_zip.TextBoxElement.TextBoxItem.BackColor = Color.White;
                                txt_adr_houseno.TextBoxElement.TextBoxItem.BackColor = Color.White;

                                txt_adr_city.Text = dr["citynenadr"].ToString();
                                txt_adr_street.Text = dr["strenenadr"].ToString();

                            }
                            else
                            {
                                txt_adr_houseno.TextBoxElement.TextBoxItem.BackColor = Color.Red;
                                txt_adr_zip.TextBoxElement.TextBoxItem.BackColor = Color.Red;
                            }


                        }
                        else
                            RadMessageBox.Show("Ivalid Zip code and House No. Zip code must contain 4 numbers and 2 letters. House must be numbers");

                    }
                    else
                        RadMessageBox.Show("Ivalid Zip code format. Zip code must contain 4 numbers and 2 letters.");

                }
                else
                {
                    RadMessageBox.Show("Zip Code and House No. must be entered");
                }
            }
        }

        private void btn_badr_get_Click(object sender, EventArgs e)
        {
            string postcode = txt_badr_zip.Text.Trim();
            postcode = postcode.Replace(" ", "");
            string houseno = txt_badr_houseno.Text.Trim();



            if (rad_badr_nl.IsChecked == true)
            {

                if (postcode != "" && houseno != "")
                {
                    int houseno_num = Int32.Parse(houseno);
                    if (postcode.Length == 6)
                    {
                        int digitAdr;
                        bool isNumericAdr = int.TryParse(postcode.Substring(0, 4), out digitAdr);
                        string letteradr = postcode.Substring(4, 2);
                        int indicator = (houseno_num % 2 == 1 ? 0 : 1);

                        if (isNumericAdr == true)
                        {
                            DataTable dt = adressBUS.GetAddress(letteradr.ToUpper(), indicator, digitAdr, houseno_num);

                            if (dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.Rows[0];

                                txt_badr_zip.TextBoxElement.TextBoxItem.BackColor = Color.White;
                                txt_badr_houseno.TextBoxElement.TextBoxItem.BackColor = Color.White;

                                txt_badr_city.Text = dr["citynenadr"].ToString();
                                txt_badr_street.Text = dr["strenenadr"].ToString();

                            }
                            else
                            {
                                txt_badr_houseno.TextBoxElement.TextBoxItem.BackColor = Color.Red;
                                txt_badr_zip.TextBoxElement.TextBoxItem.BackColor = Color.Red;
                            }


                        }
                        else
                            RadMessageBox.Show("Ivalid Zip code and House No. Zip code must contain 4 numbers and 2 letters. House must be numbers");

                    }
                    else
                        RadMessageBox.Show("Ivalid Zip code format. Zip code must contain 4 numbers and 2 letters.");

                }
                else
                {
                    RadMessageBox.Show("Zip Code and House No. must be entered");
                }
            }

        }

        private void btn_em_get_Click(object sender, EventArgs e)
        {
            string postcode = txt_em_zip.Text.Trim();
            string houseno = txt_em_houseno.Text.Trim();



            if (rad_em_nl.IsChecked == true)
            {

                if (postcode != "" && houseno != "")
                {
                    int houseno_num = Int32.Parse(houseno);
                    if (postcode.Length == 6)
                    {
                        int digitAdr;
                        bool isNumericAdr = int.TryParse(postcode.Substring(0, 4), out digitAdr);
                        string letteradr = postcode.Substring(4, 2);
                        int indicator = (houseno_num % 2 == 1 ? 0 : 1);

                        if (isNumericAdr == true)
                        {
                            DataTable dt = adressBUS.GetAddress(letteradr.ToUpper(), indicator, digitAdr, houseno_num);

                            if (dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.Rows[0];

                                txt_em_zip.TextBoxElement.TextBoxItem.BackColor = Color.White;
                                txt_em_houseno.TextBoxElement.TextBoxItem.BackColor = Color.White;

                                txt_em_city.Text = dr["citynenadr"].ToString();
                                txt_em_street.Text = dr["strenenadr"].ToString();

                            }
                            else
                            {
                                txt_em_houseno.TextBoxElement.TextBoxItem.BackColor = Color.Red;
                                txt_em_zip.TextBoxElement.TextBoxItem.BackColor = Color.Red;
                            }


                        }
                        else
                            RadMessageBox.Show("Ivalid Zip code and House No. Zip code must contain 4 numbers and 2 letters. House must be numbers");

                    }
                    else
                        RadMessageBox.Show("Ivalid Zip code format. Zip code must contain 4 numbers and 2 letters.");

                }
                else
                {
                    RadMessageBox.Show("Zip Code and House No. must be entered");
                }
            }

        }

        private void txt_adr_houseno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_badr_houseno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_em_houseno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void rad_em_nl_Click(object sender, EventArgs e)
        {
            btn_em_get.Visible = true;
            txt_em_country.Visible = false;
            lbl_em_country.Visible = false;
            txt_em_country.Text = rad_em_nl.Text;
        }

        private void rad_em_inter_Click(object sender, EventArgs e)
        {
            btn_em_get.Visible = false;
            txt_em_country.Visible = true;
            lbl_em_country.Visible = true;
        }

        private void rad_adr_nl_Click(object sender, EventArgs e)
        {
            btn_adr_get.Visible = true;
            txt_adr_country.Visible = false;
            lbl_adr_country.Visible = false;
            txt_adr_country.Text = rad_adr_nl.Text;
        }

        private void rad_adr_inter_Click(object sender, EventArgs e)
        {
            btn_adr_get.Visible = false;
            txt_adr_country.Visible = true;
            lbl_adr_country.Visible = true;
        }

        private void rad_badr_nl_Click(object sender, EventArgs e)
        {
            btn_badr_get.Visible = true;
            txt_badr_country.Visible = false;
            lbl_badr_country.Visible = false;
            txt_badr_country.Text = rad_badr_nl.Text;
        }

        private void rad_badr_inter_Click(object sender, EventArgs e)
        {
            btn_badr_get.Visible = false;
            txt_badr_country.Visible = true;
            lbl_badr_country.Visible = true;
        }



        private void lbl_adr_country_Click(object sender, EventArgs e)
        {
            CountryBUS countryBUS = new CountryBUS();
            List<IModel> gm = new List<IModel>();
            gm = countryBUS.GetCountries();

            var dlgSave = new GridLookupForm(gm, "Countries");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                CountryModel genm = new CountryModel();
                genm = (CountryModel)dlgSave.selectedRow;
                //set textbox
                txt_adr_country.Text = genm.interNationalCode;
                //update model
                
            }
        }

        private void lbl_badr_country_Click(object sender, EventArgs e)
        {
            CountryBUS countryBUS = new CountryBUS();
            List<IModel> gm = new List<IModel>();
            gm = countryBUS.GetCountries();

            var dlgSave = new GridLookupForm(gm, "Countries");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                CountryModel genm = new CountryModel();
                genm = (CountryModel)dlgSave.selectedRow;
                //set textbox
                txt_badr_country.Text = genm.interNationalCode;
                //update model

            }
        }

        private void lbl_em_country_Click(object sender, EventArgs e)
        {
            CountryBUS countryBUS = new CountryBUS();
            List<IModel> gm = new List<IModel>();
            gm = countryBUS.GetCountries();

            var dlgSave = new GridLookupForm(gm, "Countries");

            if (dlgSave.ShowDialog(this) == DialogResult.Yes)
            {
                CountryModel genm = new CountryModel();
                genm = (CountryModel)dlgSave.selectedRow;
                //set textbox
                txt_em_country.Text = genm.interNationalCode;
                //update model

            }
        }

        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                tabAddressContainer.Pages[0].Text = resxSet.GetString("Address");
                tabAddressContainer.Pages[1].Text = resxSet.GetString("Visiting address");
                tabAddressContainer.Pages[2].Text = resxSet.GetString("Emergency address");
                lbl_adr_street.Text = resxSet.GetString("Street");
                lbl_badr_street.Text = resxSet.GetString("Street");
                lbl_em_street.Text = resxSet.GetString("Street");
                lbl_em_city.Text = resxSet.GetString("City");
                lbl_badr_city.Text = resxSet.GetString("City");
                lbl_adr_city.Text = resxSet.GetString("City");
                lbl_adr_zip.Text = resxSet.GetString("Zip code");
                lbl_em_zip.Text = resxSet.GetString("Zip code");
                lbl_badr_zip.Text = resxSet.GetString("Zip code");
                lbl_adr_houseno.Text = resxSet.GetString("House nr");
                lbl_badr_houseno.Text = resxSet.GetString("House nr");
                lbl_em_houseno.Text = resxSet.GetString("House nr");
                lbl_adr_country.Text = resxSet.GetString("Country");
                lbl_badr_country.Text = resxSet.GetString("Country");
                lbl_em_country.Text = resxSet.GetString("Country");
                chk_adr_all.Text = resxSet.GetString("All");
                chk_badr_all.Text = resxSet.GetString("All");
                chk_em_all.Text = resxSet.GetString("All");
                rad_adr_inter.Text = resxSet.GetString("International");
                rad_badr_inter.Text = resxSet.GetString("International");
                rad_em_inter.Text = resxSet.GetString("International");

            }
        }

        private void ClientAddress_Load(object sender, EventArgs e)
        {

            if (showBillingAddress == false)
                //tabAddressContainer.TabPages.Remove(tabBillingAddress);
                 tabBillingAddress.Item.Visibility = ElementVisibility.Collapsed;

            if (showEmergencyAddress == false)
               // tabAddressContainer.TabPages.Remove(tabEmergency);
                tabEmergency.Item.Visibility = ElementVisibility.Collapsed;
            txt_adr_country.Text = rad_adr_nl.Text;
            txt_badr_country.Text = rad_badr_nl.Text;
            txt_em_country.Text = rad_em_nl.Text;
        }

        private void chk_adr_all_CheckStateChanged(object sender, EventArgs e)
        {
            RadCheckBox rck = (RadCheckBox)sender;
            if(isSaved!=true)
            {
                savedLastData();
            }
            if (rck.CheckState == CheckState.Checked)
            {
                DialogResult dr = RadMessageBox.Show("Are you sure that you want put this address in all tabs? ", "", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                   
                    txt_badr_street.Text = txt_adr_street.Text;
                    txt_em_street.Text = txt_adr_street.Text;
                    txt_em_city.Text = txt_adr_city.Text;
                    txt_badr_city.Text = txt_adr_city.Text;
                    txt_em_zip.Text = txt_adr_zip.Text;
                    txt_badr_zip.Text = txt_adr_zip.Text;
                    txt_badr_houseno.Text = txt_adr_houseno.Text;
                    txt_em_houseno.Text = txt_adr_houseno.Text;
                    txt_em_ext.Text = txt_adr_ext.Text;
                    txt_badr_ext.Text = txt_adr_ext.Text;
                    txt_badr_country.Text = txt_adr_country.Text;
                    lbl_badr_country.Visible = lbl_adr_country.Visible;
                    txt_badr_country.Visible = txt_adr_country.Visible;
                    txt_em_country.Text = txt_adr_country.Text;
                    lbl_em_country.Visible = lbl_adr_country.Visible;
                    txt_em_country.Visible = txt_adr_country.Visible;
                    rad_badr_nl.CheckState = rad_adr_nl.CheckState;
                    rad_em_nl.CheckState = rad_adr_nl.CheckState;
                    rad_badr_inter.CheckState = rad_adr_inter.CheckState;
                    rad_em_inter.CheckState = rad_adr_inter.CheckState;
                    btn_badr_get.Visible = btn_adr_get.Visible;
                    btn_em_get.Visible = btn_adr_get.Visible;
                }
            }
            else
            {
                fillLastData();
            }
        }

        private void fillLastData()
        {
            txt_adr_street.Text = address1;
            txt_badr_street.Text = address2;
            txt_em_street.Text = address3;
            txt_adr_city.Text = city1;
            txt_badr_city.Text = city2;
            txt_em_city.Text = city3;
            txt_adr_zip.Text = zip1;
            txt_badr_zip.Text = zip2;
            txt_em_zip.Text = zip3;
            txt_adr_houseno.Text = houseno1;
            txt_badr_houseno.Text = houseno2;
            txt_em_houseno.Text = houseno3;
            txt_adr_ext.Text = ext1;
            txt_badr_ext.Text = ext2;
            txt_em_ext.Text = ext3;
            txt_adr_country.Text = country1;
            txt_badr_country.Text = country2;
            txt_em_country.Text = country3;
            if (isInternational1 == true)
            {
                btn_adr_get.Visible = false;
                txt_adr_country.Visible = true;
                lbl_adr_country.Visible = true;

            }
            else
            {
                btn_adr_get.Visible = true;
                txt_adr_country.Visible = false;
                lbl_adr_country.Visible = false;
            }
            if (isInternational2 == true)
            {
                btn_badr_get.Visible = false;
                txt_badr_country.Visible = true;
                lbl_badr_country.Visible = true;

            }
            else
            {
                btn_badr_get.Visible = true;
                txt_badr_country.Visible = false;
                lbl_badr_country.Visible = false;
            }
            if (isInternational3 == true)
            {
                btn_em_get.Visible = false;
                txt_em_country.Visible = true;
                lbl_em_country.Visible = true;

            }
            else
            {
                btn_em_get.Visible = true;
                txt_em_country.Visible = false;
                lbl_em_country.Visible = false;
            }
        }

        private void savedLastData()
        {
             
                        address1 = txt_adr_street.Text;
                        address2 = txt_badr_street.Text;
                        address3 = txt_em_street.Text;

                        country1 = txt_adr_country.Text;
                        country2 = txt_badr_country.Text;
                        country3 = txt_em_country.Text;

                        city1 = txt_adr_city.Text;
                        city2 = txt_badr_city.Text;
                        city3 = txt_em_city.Text;

                        zip1 = txt_adr_zip.Text;
                        zip2 = txt_badr_zip.Text;
                        zip3 = txt_em_zip.Text;

                        ext1 = txt_adr_ext.Text;
                        ext2 = txt_badr_ext.Text;
                        ext3 = txt_em_ext.Text;

                        houseno1 = txt_adr_houseno.Text;
                        houseno2 = txt_badr_houseno.Text;
                        houseno3 = txt_em_houseno.Text;

                        if (rad_adr_inter.CheckState == CheckState.Checked)
                            isInternational1 = true;
                        else
                            isInternational1 = false;
                        if (rad_badr_inter.CheckState == CheckState.Checked)
                            isInternational2 = true;
                        else
                            isInternational2 = false;
                        if (rad_em_inter.CheckState == CheckState.Checked)
                            isInternational3 = true;
                        else
                            isInternational3 = false;
                        isSaved = true;

        }

        private void chk_badr_all_CheckStateChanged(object sender, EventArgs e)
        {
            RadCheckBox rck = (RadCheckBox)sender;
            if (isSaved != true)
            {
                savedLastData();
            }
            if (rck.CheckState == CheckState.Checked)
            {
                DialogResult dr = RadMessageBox.Show("Are you sure that you want put this address in all tabs? ", "", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {

                    txt_adr_street.Text = txt_badr_street.Text;
                    txt_em_street.Text = txt_badr_street.Text;
                    txt_em_city.Text = txt_badr_city.Text;
                    txt_adr_city.Text = txt_badr_city.Text;
                    txt_em_zip.Text = txt_badr_zip.Text;
                    txt_adr_zip.Text = txt_badr_zip.Text;
                    txt_adr_houseno.Text = txt_badr_houseno.Text;
                    txt_em_houseno.Text = txt_badr_houseno.Text;
                    txt_em_ext.Text = txt_badr_ext.Text;
                    txt_adr_ext.Text = txt_badr_ext.Text;
                    txt_adr_country.Text = txt_badr_country.Text;
                    lbl_adr_country.Visible = lbl_badr_country.Visible;
                    txt_adr_country.Visible = txt_badr_country.Visible;
                    txt_em_country.Text = txt_badr_country.Text;
                    lbl_em_country.Visible = lbl_badr_country.Visible;
                    txt_em_country.Visible = txt_badr_country.Visible;
                    rad_adr_nl.CheckState = rad_badr_nl.CheckState;
                    rad_em_nl.CheckState = rad_badr_nl.CheckState;
                    rad_adr_inter.CheckState = rad_badr_inter.CheckState;
                    rad_em_inter.CheckState = rad_badr_inter.CheckState;
                    btn_adr_get.Visible = btn_badr_get.Visible;
                    btn_em_get.Visible = btn_badr_get.Visible;
                }
            }
            else
            {
                fillLastData();
            }
        }

        private void chk_em_all_CheckStateChanged(object sender, EventArgs e)
        {
            RadCheckBox rck = (RadCheckBox)sender;
            if (isSaved != true)
            {
                savedLastData();
            }
            if (rck.CheckState == CheckState.Checked)
            {
                DialogResult dr = RadMessageBox.Show("Are you sure that you want put this address in all tabs? ", "", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {

                    txt_badr_street.Text = txt_em_street.Text;
                    txt_adr_street.Text = txt_em_street.Text;
                    txt_adr_city.Text = txt_em_city.Text;
                    txt_badr_city.Text = txt_em_city.Text;
                    txt_adr_zip.Text = txt_em_zip.Text;
                    txt_badr_zip.Text = txt_em_zip.Text;
                    txt_badr_houseno.Text = txt_em_houseno.Text;
                    txt_adr_houseno.Text = txt_em_houseno.Text;
                    txt_adr_ext.Text = txt_em_ext.Text;
                    txt_badr_ext.Text = txt_em_ext.Text;
                    txt_badr_country.Text = txt_em_country.Text;
                    lbl_badr_country.Visible = lbl_em_country.Visible;
                    txt_badr_country.Visible = txt_em_country.Visible;
                    txt_adr_country.Text = txt_em_country.Text;
                    lbl_adr_country.Visible = lbl_em_country.Visible;
                    txt_adr_country.Visible = txt_em_country.Visible;
                    rad_badr_nl.CheckState = rad_em_nl.CheckState;
                    rad_adr_nl.CheckState = rad_em_nl.CheckState;
                    rad_badr_inter.CheckState = rad_em_inter.CheckState;
                    rad_adr_inter.CheckState = rad_em_inter.CheckState;
                    btn_badr_get.Visible = btn_em_get.Visible;
                    btn_adr_get.Visible = btn_em_get.Visible;
                }
            }
            else
            {
                fillLastData();
            }
        }

        private void txt_adr_zip_Leave(object sender, EventArgs e)
        {
            if (rad_adr_nl.IsChecked == true)
            {
                if (txt_adr_zip.Text.Length > 4)
                {
                    char s = txt_adr_zip.Text[4];

                    if (s != ' ')
                    {
                        txt_adr_zip.Text = txt_adr_zip.Text.Insert(4, " ");
                    }

                }
            }
        }

        private void txt_badr_zip_Leave(object sender, EventArgs e)
        {
            if (rad_badr_nl.IsChecked == true)
            {
                if (txt_badr_zip.Text.Length > 4)
                {
                    char s = txt_badr_zip.Text[4];

                    if (s != ' ')
                    {
                        txt_badr_zip.Text = txt_badr_zip.Text.Insert(4, " ");
                    }

                }
            }
        }

        private void txt_em_zip_Leave(object sender, EventArgs e)
        {
            if (rad_em_nl.IsChecked == true)
            {
                if (txt_em_zip.Text.Length > 4)
                {
                    char s = txt_em_zip.Text[4];

                    if (s != ' ')
                    {
                        txt_em_zip.Text = txt_em_zip.Text.Insert(4, " ");
                    }

                }
            }
        }

        private void tabAddress_Click(object sender, EventArgs e)
        {

        }
    }
}
