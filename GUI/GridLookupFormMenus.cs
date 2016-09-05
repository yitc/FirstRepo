using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using BIS.Model;
using Telerik.WinControls.UI;
using System.Resources;
using System.IO;
using BIS.Business;
 
   
namespace GUI
{
    public partial class GridLookupFormMenus : Telerik.WinControls.UI.RadForm
    {
        string layoutLookup;
        int idRole;
        List<MenuRoleModel> selMenus;


        public GridLookupFormMenus(List<MenuRoleModel> model, List<MenuRoleModel> selectedModel, int idRoleMenus, string nameForm)
        {
            InitializeComponent();

            try
            {
                gridLookup.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
                gridLookup.DataSource = model;
                gridLookup.AllowAutoSizeColumns = true;

                //set list of selected submenus for role
                selMenus = new List<MenuRoleModel>();
                selMenus = selectedModel;

                //set selected role
                idRole = idRoleMenus;

                //set name form and icon
                this.Name = nameForm;
                this.Icon = Login.iconForm;

                TranslationBUS tb = new TranslationBUS();
                List<TranslationModel> tm = new List<TranslationModel>();
                tm = tb.CheckIfTranslationExists(Login._user.lngUser, nameForm);
                if (tm != null)
                {
                    if (tm.Count > 0)
                    {
                        nameForm = tm[0].stringKey;
                    }
                }

                if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_role")))
                {
                    Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_role"));

                }


                layoutLookup = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Login._user.username + "\\filters\\grid_role\\" + nameForm.Replace("frm", "") + ".xml");


                //do translate form form text 
                using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
                {
                    if (resxSet.GetString(nameForm) != null)
                        this.Text = resxSet.GetString(nameForm);
                    if (resxSet.GetString(radMenuItemSaveLookupLayout.Text) != null)
                        radMenuItemSaveLookupLayout.Text = resxSet.GetString(radMenuItemSaveLookupLayout.Text);
                }

                if (File.Exists(layoutLookup))
                {
                    gridLookup.LoadLayout(layoutLookup);

                    foreach (GridViewColumn column in gridLookup.Columns)
                    {
                        if (column.Name == "nameSecurityNew")
                        {
                            List<TypesSecurityModel> ttm = new RoleBUS().GetAllSecurity();
                            GridViewComboBoxColumn ddl = (GridViewComboBoxColumn)column;
                            ddl.DataSource = ttm;
                            ddl.DisplayMember = "nameSecurity";
                            ddl.ValueMember = "idSecurity";
                            ddl.FieldName = "idSecurity";
                            ddl.Name = "nameSecurityNew";
                            ddl.HeaderText = "Security";
                        }
                    }
                }
                else
                {
                    //remove column nameSecurity and add combo
                    if (gridLookup.Columns.Count > 0)
                        gridLookup.Columns.Remove("nameSecurity");

                    List<TypesSecurityModel> ttm = new RoleBUS().GetAllSecurity();
                    GridViewComboBoxColumn ddl = new GridViewComboBoxColumn();
                    ddl.DataSource = ttm;
                    ddl.DisplayMember = "nameSecurity";
                    ddl.ValueMember = "idSecurity";
                    ddl.FieldName = "idSecurity";
                    ddl.Name = "nameSecurityNew";
                    ddl.HeaderText = "Security";
                    gridLookup.Columns.Add(ddl);

                    //add check box, move to firstplace and set column max size                
                    GridViewCheckBoxColumn chk = new GridViewCheckBoxColumn();
                    chk.Name = "isChecked";
                    chk.HeaderText = "Add/Not";
                    gridLookup.Columns.Add(chk);

                    gridLookup.Columns.Move(gridLookup.Columns.Count - 1, 0);
                    gridLookup.Columns["isChecked"].MaxWidth = (int)(this.CreateGraphics().MeasureString(gridLookup.Columns["isChecked"].HeaderText, this.Font).Width + 10);

                }
                
               
                //checked submenus that added for that exact role
                checkedRows();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }


        private void checkedRows()
        {
            for (int i = 0; i < gridLookup.RowCount; i++)
            {
                if (selMenus != null)
                {
                    if (selMenus.Find(s => s.idMenu == Convert.ToInt32(gridLookup.Rows[i].Cells["idMenu"].Value.ToString())) != null)
                    {

                        gridLookup.Rows[i].Cells["isChecked"].Value = true;

                    }
                }
            }
        }


        //sets column width
        private void gridLookup_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            var grid = sender as RadGridView;

            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                foreach (var column in grid.Columns)
                {
                    if (resxSet.GetString(column.HeaderText) != null && resxSet.GetString(column.HeaderText) != "")
                        column.HeaderText = resxSet.GetString(column.HeaderText);
                    if (column.GetType() == typeof(GridViewDateTimeColumn))
                    {
                        if (column.Name.ToLower() != "dtUserModified".ToLower() && column.Name.ToLower() != "dtUserCreated".ToLower())
                        {
                            column.FormatString = "{0: dd-MM-yyyy}";
                        }
                    }
                }
            }
            if(gridLookup.Columns.Count>0)
            {
                gridLookup.Columns["idMenu"].IsVisible = false;
                gridLookup.Columns["idMenuSuperior"].IsVisible = false;
                gridLookup.Columns["idSecurity"].IsVisible = false;
                gridLookup.Columns["idMenu"].ReadOnly = false;
                gridLookup.Columns["idMenuSuperior"].ReadOnly = true;
                gridLookup.Columns["idSecurity"].ReadOnly = true;
                gridLookup.Columns["nameMenu"].ReadOnly = true;
            }
            
        }

        
        //load layouts
        private void radMenuItemSaveLookupLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(layoutLookup))
            {
                File.Delete(layoutLookup);
            }
            gridLookup.SaveLayout(layoutLookup);
            using (ResXResourceSet resxSet = new ResXResourceSet(Login.resxFile))
            {
                if (resxSet.GetString("You have successfully save layout!") != null)
                    RadMessageBox.Show(resxSet.GetString("You have successfully save layout!"));
                else
                    RadMessageBox.Show("You have successfully save layout!");
            }
        }


        private void btnSaveMenus_Click(object sender, EventArgs e)
        {
            gridLookup.EndEdit();

            Boolean isAllSecurityThere = true;
            string nameMenu = "";
            //check if security is added for all checked submenus
            for (int i = 0; i < gridLookup.Rows.Count; i++)
            {
                if (gridLookup.Rows[i].Cells["isChecked"].Value != null)
                {
                    if (Convert.ToBoolean(gridLookup.Rows[i].Cells["isChecked"].Value.ToString()) == true)
                    {
                        if (gridLookup.Rows[i].Cells["idSecurity"].Value == null)
                        {
                            isAllSecurityThere = false;
                            if (gridLookup.Rows[i].Cells["nameMenu"].Value != null)
                                nameMenu = gridLookup.Rows[i].Cells["nameMenu"].Value.ToString();
                            break;
                        }
                    }
                }
               
            }
            if (isAllSecurityThere == true)
            {
                for (int i = 0; i < gridLookup.Rows.Count; i++)
                {
                    //first delete all menus that are on lookup control, and all submenus that are added but without their superior (if there is such)
                    if (i == 0)
                    {
                        if (gridLookup.Rows[i].Cells["idMenuSuperior"].Value != null)
                        {
                            if (new RoleBUS().Delete(Convert.ToInt32(gridLookup.Rows[i].Cells["idMenuSuperior"].Value.ToString()),idRole, this.Name, Login._user.idUser) == false)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("Something went wrong try again or call your administrator!");
                                break;
                            }
                        }
                        else
                        {
                            if (new RoleBUS().DeleteMainMenus(idRole, this.Name, Login._user.idUser) == false)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("Something went wrong try again or call your administrator!");
                                break;
                            }

                        }
                    }
                    //add those which are checked
                    if (gridLookup.Rows[i].Cells["isChecked"].Value != null)
                    {
                        if (Convert.ToBoolean(gridLookup.Rows[i].Cells["isChecked"].Value.ToString()) == true)
                        {
                            if (new RoleBUS().Insert(Convert.ToInt32(gridLookup.Rows[i].Cells["idMenu"].Value.ToString()), Convert.ToInt32(gridLookup.Rows[i].Cells["idSecurity"].Value.ToString()),idRole,this.Name, Login._user.idUser) == false)
                            {
                                translateRadMessageBox tr = new translateRadMessageBox();
                                tr.translateAllMessageBox("Something went wrong with inserting menu " + nameMenu + "!");
                            }
                        }
                    }
                }
                translateRadMessageBox trs = new translateRadMessageBox();
                trs.translateAllMessageBox("You inserted menus successfully!");
                this.DialogResult = DialogResult.Yes;
                this.Close();

            }
            else
            {
                translateRadMessageBox tr = new translateRadMessageBox();
                tr.translateAllMessageBox("You have to add the security on menu "+nameMenu+" so you can add menus!");
                
            }

        }

    
    }
}
