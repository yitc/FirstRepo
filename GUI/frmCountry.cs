using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Model;
using System.Resources;
using BIS.Business;
using Telerik.WinControls.UI;
using Telerik.WinControls.Data;
using System.IO;
using BIS.DAO;


namespace GUI
{
    public partial class frmCountry :frmTemplate
    {
        private int iID;
        public CountryModel model;
        public bool modelChanged = false;
        public string Namef;
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public frmCountry()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Country");
            }
            ribbonExampleMenu.Text = "";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            InitializeComponent();
            iID = -1;
        }
        public frmCountry(int eID)
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Country");
            }
            ribbonExampleMenu.Text = "";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            iID = eID;
            InitializeComponent();
        }
        public frmCountry(CountryModel emodel)
        {
            using (ResXResourceSet resxSet= new ResXResourceSet(Login.resxFile))
            {
                Namef = resxSet.GetString("Country");
            }
            ribbonExampleMenu.Text = "";

            this.Text = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString() + " - " + Namef;
            model = emodel;
            iID = model.idCountry;
            InitializeComponent();
        }

        private void frmCountry_Load(object sender, EventArgs e)
        {
            radRibbonDocuments.Visibility = ElementVisibility.Visible;
            btnNewDoc.Visibility = ElementVisibility.Collapsed;
            btnReport.Visibility = ElementVisibility.Collapsed;
            btnEmail.Visibility = ElementVisibility.Collapsed;
            btnWord.Visibility = ElementVisibility.Collapsed;
            btnNewMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteDoc.Visibility = ElementVisibility.Visible;
            btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;
            radRibbonMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;

            if (Login._companyModelList[0].flag == "W")
            {
                ddlPremie.Visible = false;
                ddlProvision.Visible = false;
                lblPremie.Visible = false;
                lblProvision.Visible = false;
            }

            setTranslation();


         

            if (iID != -1)
            {
                txtCountryId.Text = model.idCountry.ToString();
                txtInternationalCode.Text = model.interNationalCode.ToString();
                txtNameCountry.Text = model.nameCountry.ToString();
                txtNacionality.Text = model.nacionality.ToString();
                ddlProvision.Text = model.provision.ToString();
                ddlPremie.Text = model.premie.ToString();
                rgvProvinces.Visible = true;
                ProvincesBUS pBUS = new ProvincesBUS();
                this.rgvProvinces.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.rgvProvinces_DataBindingComplete);
                rgvProvinces.DataSource = pBUS.GetAllProvinces(model.idCountry);
                rgvProvinces.Columns["idCountry"].IsVisible = false;
                rgvProvinces.Columns["idProvinces"].IsVisible = false;
               

            }
       
        }

        private void rgvProvinces_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            dictionary.Clear();
            var grid = sender as RadGridView;
            foreach (var column in rgvProvinces.Columns)
            {
                if (column.HeaderText != null)
                {
                    try
                    {
                        dictionary.Add(column.HeaderText, column.FieldName);

                    }
                    catch
                    {
                        continue;
                    }

                    column.MaxWidth = (int)(this.CreateGraphics().MeasureString(column.HeaderText, this.Font).Width + 95);
                    column.Width = column.MaxWidth;
                    column.MinWidth = column.MaxWidth;
                }
            }
            for (int i = 0; i < grid.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(grid.Columns[i].HeaderText) != null)
                        grid.Columns[i].HeaderText = resxSet.GetString(grid.Columns[i].HeaderText);
                }

            }
        }
        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblCountriesId.Text) != null)
                    lblCountriesId.Text = resxSet.GetString(lblCountriesId.Text);
                if (resxSet.GetString(lblInternationalCode.Text) != null)
                    lblInternationalCode.Text = resxSet.GetString(lblInternationalCode.Text);
                if (resxSet.GetString(lblNameCountry.Text) != null)
                    lblNameCountry.Text = resxSet.GetString(lblNameCountry.Text);
                if (resxSet.GetString(lblNacionality.Text) != null)
                    lblNacionality.Text = resxSet.GetString(lblNacionality.Text);
                if (resxSet.GetString(btnSave.Text) != null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);
                if (resxSet.GetString(btnDeleteDoc.Text) != null)
                    btnDeleteDoc.Text = resxSet.GetString(btnDeleteDoc.Text);
                if (resxSet.GetString(btnDeleteMemo.Text) != null)
                    btnDeleteMemo.Text = resxSet.GetString(btnDeleteMemo.Text);
                radRibbonDocuments.Text = "";
                if (resxSet.GetString(lblProvision.Text) != null)
                    lblProvision.Text = resxSet.GetString(lblProvision.Text);
                if (resxSet.GetString(lblPremie.Text) != null)
                    lblPremie.Text = resxSet.GetString(lblPremie.Text);
            }
        }

        private void setSize()
        {
            for (int i = 0; i < rgvProvinces.Columns.Count; i++)
            {
                rgvProvinces.Columns[i].MaxWidth = (int)(this.CreateGraphics().MeasureString(rgvProvinces.Columns[i].HeaderText, this.Font).Width + 95);
                rgvProvinces.Columns[i].Width = rgvProvinces.Columns[i].MaxWidth;
                rgvProvinces.Columns[i].MinWidth = rgvProvinces.Columns[i].MaxWidth;
            }

            for (int i = 0; i < rgvProvinces.Columns.Count; i++)
            {

                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(rgvProvinces.Columns[i].HeaderText) != null)
                        rgvProvinces.Columns[i].HeaderText = resxSet.GetString(rgvProvinces.Columns[i].HeaderText);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                CountryBUS bus = new CountryBUS();
                if(iID != - 1)
                {
                   
                    model.idCountry = iID;
                    model.interNationalCode = txtInternationalCode.Text;
                    model.nameCountry = txtNameCountry.Text;
                    model.nacionality = txtNacionality.Text;
                    if (ddlProvision.SelectedIndex >= 0)
                    {
                        model.provision = ddlProvision.SelectedItem.Text;
                    }
                    else
                    {
                        if (ddlProvision.Text != "")
                            model.provision = ddlProvision.Text;
                        else
                            model.provision = "";
                    }
                    if (ddlPremie.SelectedIndex >= 0)
                    {
                        model.premie = ddlPremie.SelectedItem.Text;
                    }
                    else
                    {
                        if (model.premie.ToString() != "")
                            model.premie = ddlPremie.Text;
                        else
                            model.premie = "";
                    }

                    bus.Update(model, this.Name, Login._user.idUser);
                    modelChanged = true;
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You updated data successfully!");
                
          
                }
                else
                {
                    model = new CountryModel();
                    
                    model.idCountry=idCountryLast();
                    model.interNationalCode = txtInternationalCode.Text;
                    model.nameCountry = txtNameCountry.Text;
                    model.nacionality = txtNacionality.Text;

                    if (ddlProvision.SelectedIndex >= 0)
                    {
                        model.provision = ddlProvision.SelectedItem.Text;
                    }
                    else
                    {       
                            model.provision = "";
                    }
                    if (ddlPremie.SelectedIndex >= 0)
                    {
                        model.premie = ddlPremie.SelectedItem.Text;
                    }
                    else
                    {
                            model.premie = "";
                    }

                    if (bus.Save(model, this.Name, Login._user.idUser) != false)
                    {

                        iID = idCountryLast();
                        modelChanged = true;
                        ProvincesBUS pBUS = new ProvincesBUS();
                        rgvProvinces.DataSource = pBUS.GetAllProvinces(model.idCountry);
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("You saved Country successfully!");
                        rgvProvinces.Columns["idCountry"].IsVisible = false;
                        rgvProvinces.Columns["idProvinces"].IsVisible = false;

                        setSize();
                    }
                  
                 
                }
                modelChanged = true;
               // this.Close();
            }
            catch(Exception ex)
            {
                RadMessageBox.Show("Error saving.\nMessage: " + ex.Message, "Save", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void btnDeleteDoc_Click(object sender, EventArgs e)
        {
            if(iID != -1)
            {
                CountryBUS db = new CountryBUS();
                db.Delete(iID, this.Name, Login._user.idUser);
                this.Close();
                modelChanged = true;
            }
        }


        private int idCountryLast()
        {
            int Lastid = -1;
            CountryDAO _countryDAO = new CountryDAO();
            DataTable dataTable = _countryDAO.LastidCountry();
            if (dataTable.Rows.Count > 0)
            {
                Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
            }
            return Lastid + 1;

        }

        private int idLast()
        { 
            int Lastid=-1;
             ProvincesDAO _provincesDAO = new ProvincesDAO();
                    DataTable dataTable = _provincesDAO.idProvinces();
                    if (dataTable.Rows.Count > 0)
                    {
                         Lastid = Convert.ToInt32(dataTable.Rows[0][0]);
                    }
                    return Lastid + 1;
        
        }


        private void rgvProvinces_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {

            ProvincesBUS mvb = new ProvincesBUS();
            MasterGridViewTemplate mgvt = (MasterGridViewTemplate)sender;

            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Add)
            {
                ProvincesBUS pBUS = new ProvincesBUS();

                if ((mgvt.CurrentRow.Cells["codeProvinces"].Value) != null && (mgvt.CurrentRow.Cells["nameProvinces"].Value) != null)
                {
                    if ((mgvt.CurrentRow.Cells["codeProvinces"].Value).ToString().Length <= 6)
                    {
                        if (pBUS.Insert(idLast(), (mgvt.CurrentRow.Cells["codeProvinces"].Value).ToString(), Convert.ToInt32(model.idCountry), (mgvt.CurrentRow.Cells["nameProvinces"].Value).ToString(), this.Name, Login._user.idUser) != true)
                        {
                            e.Cancel = true;
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Something went wrong with inserting row!");
                        }
                        else
                        {
                            rgvProvinces.DataSource = null;
                            rgvProvinces.DataSource = pBUS.GetAllProvinces(model.idCountry);
                            rgvProvinces.Columns["idCountry"].IsVisible = false;
                            rgvProvinces.Columns["idProvinces"].IsVisible = false;
                            setSize();
                            rgvProvinces.CancelEdit();

                        }
                    }
                    else 
                    {
                        e.Cancel = true;
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Maximum length Code province is 6 characters!");
                    }
                }
                else
                {
                    e.Cancel = true;
                    rgvProvinces.CancelEdit();
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You have add code province and name province!");

                }

            }

            else if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {
                translateRadMessageBox t = new translateRadMessageBox();

                DialogResult dialog = t.translateAllMessageBoxDialog("Click Yes to delete these rows.","");


                if (dialog == DialogResult.Yes)
                {
                    ProvincesBUS pBUS = new ProvincesBUS();
                    if (pBUS.Delete(Convert.ToInt32(mgvt.CurrentRow.Cells["idProvinces"].Value), this.Name, Login._user.idUser) != true)
                    {

                        rgvProvinces.CancelEdit();
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Something went wrong with deleting row!");
                    }

                }
                else 
                {
                    e.Cancel = true;
                }
            }
        }
        private void RgvProvinces_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            ProvincesBUS pBUS = new ProvincesBUS();
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.ItemChanged)
            {
                GridViewDataRowInfo newRow = e.NewItems[0] as GridViewDataRowInfo;
                if ((newRow.Cells["codeProvinces"].Value) != null && (newRow.Cells["nameProvinces"].Value) != null)
                {
                    if ((newRow.Cells["codeProvinces"].Value).ToString().Length <= 6)
                    {
                        if (pBUS.Update(Convert.ToInt32(newRow.Cells["idProvinces"].Value), (newRow.Cells["codeProvinces"].Value).ToString(), (newRow.Cells["nameProvinces"].Value).ToString(), this.Name, Login._user.idUser) != true)
                        {

                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("Something went wrong with editing row!");

                        }
                    }
                    else
                    {
                       
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("Maximum length Code province is 6 characters!");
                        rgvProvinces.DataSource = null;
                        rgvProvinces.DataSource = pBUS.GetAllProvinces(model.idCountry);
                        rgvProvinces.Columns["idCountry"].IsVisible = false;
                        rgvProvinces.Columns["idProvinces"].IsVisible = false;
                        setSize();
                    }
                }
                else 
                {
                   
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("You have add code and name province!");
                    rgvProvinces.DataSource = null;
                    rgvProvinces.DataSource = pBUS.GetAllProvinces(model.idCountry);
                    rgvProvinces.Columns["idCountry"].IsVisible = false;
                    rgvProvinces.Columns["idProvinces"].IsVisible = false;
                    setSize();
                }

            }

        }


    }
}
