using BIS.Business;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace GUI
{
    public partial class frmRoles : frmTemplate
    {
        RoleModel rm;
        List<RoleSecurityModel> mm;
        GridViewTemplate secondChildtemplate;
        GridViewTemplate template;
        GridViewRelation relation;
        GridViewRelation relation2;

        public frmRoles()
        {
            InitializeComponent();
        }
        public frmRoles(RoleModel rolem)
        {
            InitializeComponent();
            rm = rolem;
            fillData();
        }

        private void frmRoles_Load(object sender, EventArgs e)
        {
            radRibbonDocuments.Visibility = ElementVisibility.Collapsed;
            btnReport.Visibility = ElementVisibility.Collapsed;
            btnEmail.Visibility = ElementVisibility.Collapsed;
            btnWord.Visibility = ElementVisibility.Collapsed;
            btnNewMemo.Visibility = ElementVisibility.Collapsed;
            btnDeleteMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonContact.Visibility = ElementVisibility.Collapsed;
            radRibbonTask.Visibility = ElementVisibility.Collapsed;
            radRibbonMemo.Visibility = ElementVisibility.Collapsed;
            radRibbonMeeting.Visibility = ElementVisibility.Collapsed;

            this.Icon = Login.iconForm;
            string formName = Login._companyModelList[0].nameCompany + " " + DateTime.Now.Year.ToString();
            this.Text = formName;

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(this.Name.Replace("frm", "")) != null)
                    formName = formName + " " + resxSet.GetString(this.Name.Replace("frm", ""));
                else
                    formName = formName + " " + this.Name.Replace("frm", "");
            }
            setTranslation();
        }

        private void setTranslation()
        {
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString(lblRoleName.Text) != null)
                    lblRoleName.Text = resxSet.GetString(lblRoleName.Text);
            }
        }

        private void fillData()
        {
            txtRoleName.Text = rm.nameRole;

            rgvMenusForRole.DataSource = new RoleBUS().GetMenus(Login._user.lngUser,rm.idRole);
            rgvMenusForRole.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            rgvMenusForRole.AllowAddNewRow = false;
            rgvMenusForRole.AllowDeleteRow = false;
            rgvMenusForRole.AllowEditRow = false;

            template = new GridViewTemplate();
            template.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            template.AllowAddNewRow = false;
            template.AllowDeleteRow = false;
            template.AllowEditRow = false;
            template.DataSource = new RoleBUS().GetSubMenus(Login._user.lngUser,rm.idRole);
            rgvMenusForRole.MasterTemplate.Templates.Add(template);

            GridViewRelation relation = new GridViewRelation(rgvMenusForRole.MasterTemplate);
            relation.ChildTemplate = template;
            relation.RelationName = "MenusSubMenus";
            relation.ParentColumnNames.Add("idMenu");
            relation.ChildColumnNames.Add("idMenuSuperior");
            rgvMenusForRole.Relations.Add(relation);

            secondChildtemplate = new GridViewTemplate();
            secondChildtemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            secondChildtemplate.AllowAddNewRow = false;
            secondChildtemplate.AllowDeleteRow = false;
            secondChildtemplate.AllowEditRow = false;
            secondChildtemplate.DataSource = template.DataSource;
            template.Templates.Add(secondChildtemplate);

            GridViewRelation relation2 = new GridViewRelation(template);
            relation2.ChildTemplate = secondChildtemplate;
            relation2.RelationName = "SubMenusSubMenus";
            relation2.ParentColumnNames.Add("idMenu");
            relation2.ChildColumnNames.Add("idMenuSuperior");
            rgvMenusForRole.Relations.Add(relation2);

            rgvMenusForRole.Columns["idMenu"].IsVisible = false;
            rgvMenusForRole.Columns["idMenuSuperior"].IsVisible = false;
            rgvMenusForRole.Columns["idSecurity"].IsVisible = false;
            template.Columns["idMenu"].IsVisible = false;
            template.Columns["idMenuSuperior"].IsVisible = false;
            template.Columns["idSecurity"].IsVisible = false;
            secondChildtemplate.Columns["idMenu"].IsVisible = false;
            secondChildtemplate.Columns["idMenuSuperior"].IsVisible = false;
            secondChildtemplate.Columns["idSecurity"].IsVisible = false;

        }

        private void btnMainMenus_Click(object sender, EventArgs e)
        {
            if (txtRoleName.Text != ""  && rm!=null)
            {
                if (rm.idRole != null)
                {
                    List<MenuRoleModel> listMenus = new List<MenuRoleModel>();
                    listMenus = new RoleBUS().GetMainMenus(Login._user.lngUser, rm.idRole);
                    List<MenuRoleModel> listSelectedMenus = new List<MenuRoleModel>();
                    listSelectedMenus = new RoleBUS().GetMainMenusForRole(Login._user.lngUser, rm.idRole);
                    if (listMenus.Count > 0)
                    {
                        GridLookupFormMenus frm = new GridLookupFormMenus(listMenus, listSelectedMenus, rm.idRole, "Role");
                        frm.ShowDialog();
                        if (frm.DialogResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            rgvMenusForRole.DataSource = new List<MenuModel>();
                            if (secondChildtemplate!=null)
                            secondChildtemplate.DataSource = new List<MenuModel>();
                            if (template != null)
                            template.DataSource = new List<MenuModel>();
                            for (int i = 0; i < rgvMenusForRole.MasterTemplate.Templates.Count; i++)
                            {
                                rgvMenusForRole.MasterTemplate.Templates.Remove(rgvMenusForRole.MasterTemplate.Templates[i]);
                            }
                            fillData();
                        }
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("There is no submenus for this menu");
                    }
                }
            }
            else
            {
                if(saveRole()==true)
                {
                    List<MenuRoleModel> listMenus = new List<MenuRoleModel>();
                    listMenus = new RoleBUS().GetMainMenus(Login._user.lngUser, rm.idRole);
                    List<MenuRoleModel> listSelectedMenus = new List<MenuRoleModel>();
                    listSelectedMenus = new RoleBUS().GetMainMenusForRole(Login._user.lngUser, rm.idRole);
                    if (listMenus.Count > 0)
                    {
                        GridLookupFormMenus frm = new GridLookupFormMenus(listMenus, listSelectedMenus, rm.idRole, "Role");
                        frm.ShowDialog();
                        if (frm.DialogResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            fillData();
                        }
                    }
                    else
                    {
                        translateRadMessageBox tr = new translateRadMessageBox();
                        tr.translateAllMessageBox("There is no submenus for this menu");
                    }
                }
            }

        }

        private Boolean saveRole()
        {
            Boolean isSuccessfull = false;
            if (txtRoleName.Text == "")
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to add role name first!");
                
            }
            else if (txtRoleName.Text != "" && rm == null)
            {
                if (new RoleBUS().InsertRole(txtRoleName.Text, this.Name, Login._user.idUser) == false)
                {
                    translateRadMessageBox tr = new translateRadMessageBox();
                    tr.translateAllMessageBox("Something went wrong with inserting role");
                }
                else
                {
                            rm = new RoleModel();
                            rm = new RoleBUS().GetLastRoleID(); 
                            rm.nameRole = txtRoleName.Text;
                            isSuccessfull = true;
                }
            }
            return isSuccessfull;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveRole();
        }

        private void rgvMenusForRole_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {

            if(e.RowIndex!=null & e.RowIndex>=0)
            { 
            GridViewRowInfo info = this.rgvMenusForRole.CurrentRow;

            if (info != null && info.Index >= 0)
            {
                MenuRoleModel selectedMenu = (MenuRoleModel)info.DataBoundItem;
                if (selectedMenu != null)
                {
                    if (selectedMenu.idMenu != null && selectedMenu.idMenu > 0)
                    {
                        List<MenuRoleModel> listMenus = new List<MenuRoleModel>();
                        listMenus = new RoleBUS().GetSubMenusForSuperior(Login._user.lngUser, rm.idRole, selectedMenu.idMenu);
                        List<MenuRoleModel> listSelectedMenus = new List<MenuRoleModel>();
                        listSelectedMenus = new RoleBUS().GetSubMenusForSuperiorForRole(Login._user.lngUser, rm.idRole, selectedMenu.idMenu);
                        if (listMenus.Count > 0)
                        {
                            GridLookupFormMenus frm = new GridLookupFormMenus(listMenus, listSelectedMenus, rm.idRole, "Role");
                            frm.ShowDialog();
                            if (frm.DialogResult == System.Windows.Forms.DialogResult.Yes)
                            {

                                rgvMenusForRole.DataSource = new List<MenuModel>();
                                if (secondChildtemplate != null)
                                    secondChildtemplate.DataSource = new List<MenuModel>();
                                if (template != null)
                                    template.DataSource = new List<MenuModel>();
                                for (int i = 0; i < rgvMenusForRole.MasterTemplate.Templates.Count; i++)
                                {
                                    rgvMenusForRole.MasterTemplate.Templates.Remove(rgvMenusForRole.MasterTemplate.Templates[i]);
                                }
                                fillData();
                            }
                        }
                        else
                        {
                            translateRadMessageBox tr = new translateRadMessageBox();
                            tr.translateAllMessageBox("There is no submenus for this menu");
                        }
                    }
                }
            }
        }

        }

        
    }
}
