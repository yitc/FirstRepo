using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BIS.Business;
using BIS.Model;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using System.Resources;
using BIS.DAO;

namespace GUI
{
    public partial class frmFiltersLabels : Form
    {
        private FiltersLabelsModel fl;
        private NameMenuModel nmodel;
        private List<NameMenuModel> menumodel= new List<NameMenuModel>();
        private List<FilterModel> fmodel;
        private List<LabelModel> lmodel;
        List<IModel> binding;
        public bool isChanged;
  


        private FiltersLabelsBUS _flBUS;
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        public frmFiltersLabels()
        {
            InitializeComponent();
        }
        public frmFiltersLabels(IModel model)
        {
            fl = (FiltersLabelsModel)model;

            InitializeComponent();
        }
        private void radButtonSave_Click(object sender, EventArgs e)
        {
            if (fl != null)
                Update();
            if (fl == null)
                Insert();
                      
        }

        private int setUniqueId()
        {
            
            int id = -1;
            List<LabelModel> isContainNameLabel = new List<LabelModel>();
            List<LabelModel> isNotContainNameLabel = new List<LabelModel>();
            List<LabelModel> isContainId = new List<LabelModel>();
            List<LabelModel> lista = new List<LabelModel>();
            isContainNameLabel = _flBUS.NameLabelIsExist(txtNewName.Text);
            // da li postoji labela sa tim nazivom
            if (isContainNameLabel != null)
            {
                if (isContainNameLabel.Count > 0)
                {
                    if (isContainNameLabel[0].idLabel >= 0)
                    {
                        isContainId = _flBUS.ForExistLabelidIsExist(txtNewName.Text);
                        if (isContainId != null)
                        {
                            if (isContainId.Count > 0)
                            {
                                // da li za labelu koja vec postoji sa tim imenom postoji id
                                if (isContainId[0].idLabel >= 0)
                                {
                                    lista = _flBUS.GetIdLabelForExistName(txtNewName.Text);

                                    if (lista != null)
                                    {
                                        if (lista[0].idLabel >= 0)
                                        {
                                            id = lista[0].idLabel;
                                        }
                                    }

                                }
                                if (isContainId[0].idLabel == -1)
                                {
                                    // prvi slobodan id
                                    isNotContainNameLabel = _flBUS.GetIdLabelForNotExistName();
                                    if (isNotContainNameLabel != null)
                                    {
                                        if (isNotContainNameLabel[0].idLabel >= 0)
                                        {
                                            id = isNotContainNameLabel[0].idLabel + 1;

                                        }
                                    }
                                }
                            }

                        }
                    }

                    else
                    {
                        //prvi slobodan id
                        isNotContainNameLabel = _flBUS.GetIdLabelForNotExistName();
                        if (isNotContainNameLabel != null)
                        {
                            if (isNotContainNameLabel[0].idLabel >= 0)
                            {
                                id = isNotContainNameLabel[0].idLabel + 1;

                            }
                        }

                    }
                }
                else
                {
                    //prvi slobodan id
                    isNotContainNameLabel = _flBUS.GetIdLabelForNotExistName();
                    if (isNotContainNameLabel != null)
                    {
                        if (isNotContainNameLabel[0].idLabel >= 0)
                        {
                            id = isNotContainNameLabel[0].idLabel + 1;

                        }
                    }

                }
            }


            else
            {
                //prvi slobodan id
                isNotContainNameLabel = _flBUS.GetIdLabelForNotExistName();
                if (isNotContainNameLabel != null)
                {
                    if (isNotContainNameLabel[0].idLabel >= 0)
                    {
                        id = isNotContainNameLabel[0].idLabel + 1;

                    }
                }

            }

           return id;
        }

        public void Update() 
        {
            _flBUS = new FiltersLabelsBUS();
            fl.name = txtNewName.Text;
            fl.nameMenu = ddlMenu.SelectedItem.ToString();
            int id = -1;
            if (chkUniq.IsChecked == true)
            {
             id=   setUniqueId();

            }
           
            List<MenuIDModel> menuID= new List<MenuIDModel>();
            menuID = _flBUS.GetIdMenu(fl.nameMenu);
            int idMenu = Convert.ToInt32(menuID[0].idMenu.ToString());
     
            if (fl.type == "Filter" && fl.name != "")
            {
                if (_flBUS.UpdateFilterName(fl.ID, fl.name, this.Name, Login._user.idUser) == false)
                {
                     translateRadMessageBox trs = new translateRadMessageBox();
                 trs.translateAllMessageBox("Something went wrong with update!");

                }

                else
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You updated filter successfuly!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
            }
            if (fl.type == "Label" && fl.name != "")
            {
                if (_flBUS.UpdateLabelName(fl.ID, fl.name, idMenu, id, this.Name, Login._user.idUser) == false)
                {
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("Something went wrong with updating!");

                }

                else
                {
                   // this.Close();
                    translateRadMessageBox trs = new translateRadMessageBox();
                    trs.translateAllMessageBox("You updated label successfully!");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
            }
            isChanged = true;
            
        }
        private void Insert()
        {
            _flBUS = new FiltersLabelsBUS();
            // da li je oznacen neki  radio button
            if (rbFilter.CheckState != CheckState.Checked && rbLabel.CheckState != CheckState.Checked)
        {
            translateRadMessageBox trs = new translateRadMessageBox();
            trs.translateAllMessageBox("Check filter or label!");
        }
           
                if (rbFilter.CheckState == CheckState.Checked)
                {
                    if (txtNewName.Text != "")
                    {
                        int sortFilter = GetLastSortFilter();
                        int idFilter = GelLastIdFilter();
                        if (_flBUS.InsertFilter(idFilter, txtNewName.Text, sortFilter, this.Name, Login._user.idUser) == true)
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("You inserted filter successfully!");
                        }
                        else 
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("Something went wrong with inserting!");
                        }
                        
                    }
                    else
                    {

                        translateRadMessageBox trs = new translateRadMessageBox();
                        trs.translateAllMessageBox("Filter name requiered!");
                    }
                }
                if (rbLabel.CheckState == CheckState.Checked)
                {
                    int idMenu = Convert.ToInt32(ddlMenu.SelectedValue);

                    int idLabel = GelLastIdLabel();
                    int id = -1;

                    if (chkUniq.IsChecked == true)
                   {

                     id= setUniqueId();

                 if (_flBUS.InsertLabel(idLabel, txtNewName.Text, idMenu, id, this.Name, Login._user.idUser) == true)
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You inserted label successfully!");
            }
            else
            {
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("Something went wrong with inserting!");
            }
                    }
                    else 
                    {
                         idMenu = Convert.ToInt32(ddlMenu.SelectedValue);

                         idLabel = GelLastIdLabel();
                        if (_flBUS.InsertLabel(idLabel, txtNewName.Text, idMenu, -1, this.Name, Login._user.idUser)== true)
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("You inserted label successfully!");
                        }
                        else
                        {
                            translateRadMessageBox trs = new translateRadMessageBox();
                            trs.translateAllMessageBox("Something went wrong with inserting!");
                        }
                    }
                    }

                isChanged = true;
           
        }
        private void radButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void frmFiltersLabels_Load(object sender, EventArgs e)
        {
            setTranslation();
            if (fl != null)
            
            {
                rbFilter.Visible = false;
                rbLabel.Visible = false;
                txtNewName.Text = fl.name;
                ddlMenu.Visible = false;
                lblLabelFor.Visible = false;
                chkUniq.Visible = false;
                lblUnq.Visible = false;
                
                if (fl.type == "Label")
                {
                    ddlMenu.Visible = true;
                    lblLabelFor.Visible = true;
                    chkUniq.Visible = true;
                     lblUnq.Visible = true;
                    lblNew.Text = "New label name:";
                    if (fl.uniques.ToString() != "")
                    { 
                    chkUniq.Checked=true;
                    }
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        lblNew.Text = resxSet.GetString(lblNew.Text);
                    }
                    
                   
                }
                if (fl.type == "Filter")
                {
                    lblNew.Text = "New filter name:";
                    using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                    {
                        lblNew.Text = resxSet.GetString(lblNew.Text);
                    }
                }
                FiltersLabelsBUS ub = new FiltersLabelsBUS();
                menumodel = ub.GetNameMenu();
                ddlMenu.DataSource = menumodel;
                ddlMenu.DisplayMember = "nameMenu";
                ddlMenu.ValueMember = "idMenu";

                string nameMenu = fl.nameMenu.ToString();
                if (nameMenu != "")
                    ddlMenu.SelectedIndex = menumodel.FindIndex(s => s.nameMenu.ToString().TrimEnd() == nameMenu.TrimEnd());
                int i = Convert.ToInt32(ddlMenu.SelectedValue);

            }

            else if (fl==null)
            {
                ddlMenu.Visible = false;
                lblLabelFor.Visible = false;
                chkUniq.Visible = false;
                lblUnq.Visible = false;
                FiltersLabelsBUS ub = new FiltersLabelsBUS();
                menumodel = ub.GetNameMenu();
                ddlMenu.DataSource = menumodel;
                ddlMenu.DisplayMember = "nameMenu";
                ddlMenu.ValueMember = "idMenu";

            }
           
            rbLabel.Click += rbLabel_CheckStateChanged;
            rbFilter.Click += rbFilter_CheckStateChanged;
         
            
        }

        private void rbLabel_CheckStateChanged(object sender, EventArgs e)
        {
            ddlMenu.Visible = true;
            lblLabelFor.Visible = true;
            chkUniq.Visible = true;
            lblUnq.Visible = true;
            
        }
        private void rbFilter_CheckStateChanged(object sender, EventArgs e)
        {
            lblLabelFor.Visible = false;
            ddlMenu.Visible = false;
            chkUniq.Visible = false;
            lblUnq.Visible = false;
   
        }

        private int GetLastSortFilter() 
        { 
            fmodel=new List<FilterModel>();
           _flBUS = new FiltersLabelsBUS();
            fmodel= _flBUS.GetAllFilter();

            int rez=0;
           
                if (fmodel.Count > 0)
                {

                    rez = Convert.ToInt32(fmodel[0].sortFilter.ToString()) + 1;
                    // rez = fmodel.
                }
            
            return rez;
            
        }


        private int GelLastIdFilter()
        {
            fmodel = new List<FilterModel>();
            _flBUS = new FiltersLabelsBUS();
            fmodel = _flBUS.GetLastIdFilter();

            int rez=0;
            
                if (fmodel.Count > 0)
                {
                    rez = Convert.ToInt32(fmodel[0].idFilter.ToString()) + 1;
                    // rez = fmodel.
                }
            
            return rez;
        
        }

        private int GelLastIdLabel()
        {
            lmodel = new List<LabelModel>();
            _flBUS = new FiltersLabelsBUS();
            lmodel = _flBUS.GetLastIdLabel();

            int rez=0;
         
                if (lmodel.Count > 0)
                {
                    rez = Convert.ToInt32(lmodel[0].idLabel.ToString()) + 1;
                }
        
            return rez;

       }
        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (btnSave.Text!=null)
                    btnSave.Text = resxSet.GetString(btnSave.Text);
                if (btnCancel.Text!=null)
                    btnCancel.Text = resxSet.GetString(btnCancel.Text);
                if (lblLabelFor.Text!=null)
                    lblLabelFor.Text = resxSet.GetString(lblLabelFor.Text);
                //if(lblUnique.Text!=null)
                //    lblUnique.Text = resxSet.GetString(lblUnique.Text);

                if (fl != null)
                {
                    if (fl.type == "Filter")
                        lblNew.Text = resxSet.GetString("New filter name:");
                    if (fl.type == "Label")
                        lblNew.Text = resxSet.GetString("New label name:");
                }
                if (fl == null)
                    lblNew.Text = resxSet.GetString("Enter new name:");



            }
        }

     }

   

     
    }


