using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.DAO;
using BIS.Model;

namespace BIS.Business
{
    public class MenuBUS
    {
        private MenuDAO menuDAO;

        public MenuBUS()
        {
            menuDAO = new MenuDAO();
        }

        public List<MenuModel> GetUserSecurityDetails(int userid, string language)
        {
            List<MenuModel> menusList = new List<MenuModel>();

            DataTable dataTable = new DataTable();
            dataTable = menuDAO.GetUserSecurityDetails(userid, language);


            if(dataTable != null)
            {
                 foreach (DataRow dr in dataTable.Rows)
                 {
                     MenuModel menumod = new MenuModel();

                     menumod.idMenu = Int32.Parse(dr["idMenu"].ToString());
                     menumod.idSecurity = Int32.Parse(dr["idSecurity"].ToString());
                     menumod.lngCode = dr["lngCode"].ToString();
                     menumod.nameForm = dr["nameForm"].ToString();
                     menumod.nameMenu = dr["nameMenu"].ToString();
                     menumod.sortMenu = Int32.Parse(dr["sortMenu"].ToString());
                     menumod.initMethod = dr["initMethod"].ToString();
                     if (dr["imageMenu"].ToString() != "")
                     {
                         menumod.imageMenu = dr["imageMenu"].ToString();
                     }
                     else
                         menumod.imageMenu = null;

                     if (dr["imageNew"].ToString() != "")
                     {
                         menumod.imageNew = dr["imageNew"].ToString();
                     }
                     else
                         menumod.imageNew = null;

                     if (dr["imageDelete"].ToString() != "")
                     {
                         menumod.imageDelete = dr["imageDelete"].ToString();
                     }
                     else
                         menumod.imageDelete = null;


                     if (dr["onClickMenu"].ToString() != "")
                     {
                         menumod.onClickMenu = dr["onClickMenu"].ToString();
                     }
                     else
                         menumod.onClickMenu = null;

                     menusList.Add(menumod);
                 }

                 return menusList;
            }
            else
            {
                return null;
            }
        }

        public MenuModel GetMenuByName(string nameMenu, int userid, string language)
        {
            MenuModel menusList = new MenuModel();

            DataTable dataTable = new DataTable();
            dataTable = menuDAO.GetMenuByName(nameMenu,userid, language);


            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {

                    menusList.idMenu = Int32.Parse(dr["idMenu"].ToString());
                    menusList.idSecurity = Int32.Parse(dr["idSecurity"].ToString());
                    menusList.lngCode = dr["lngCode"].ToString();
                    if (dr["idForm"].ToString() != "")
                       menusList.idForm = Convert.ToInt32(dr["idForm"].ToString());
                    menusList.nameForm = dr["nameForm"].ToString();
                    menusList.nameMenu = dr["nameMenu"].ToString();
                    menusList.sortMenu = Int32.Parse(dr["sortMenu"].ToString());
                    menusList.initMethod = dr["initMethod"].ToString();
                    if (dr["imageMenu"].ToString() != "")
                    {
                        menusList.imageMenu = dr["imageMenu"].ToString();
                    }
                    else
                        menusList.imageMenu = null;

                    if (dr["imageNew"].ToString() != "")
                    {
                        menusList.imageNew = dr["imageNew"].ToString();
                    }
                    else
                        menusList.imageNew = null;

                    if (dr["imageDelete"].ToString() != "")
                    {
                        menusList.imageDelete = dr["imageDelete"].ToString();
                    }
                    else
                        menusList.imageDelete = null;


                    if (dr["onClickMenu"].ToString() != "")
                    {
                        menusList.onClickMenu = dr["onClickMenu"].ToString();
                    }
                    else
                        menusList.onClickMenu = null;

                    break;
                }

                return menusList;
            }
            else
            {
                return null;
            }
        }

        public List<MenuModel> GetMenus(int idMenu, int userid, string language)
        {
            DataTable dataTable = new DataTable();
            dataTable = menuDAO.GetMenus(idMenu,userid, language);
            List<MenuModel> menus = new List<MenuModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        MenuModel model = new MenuModel();

                        int parentId = Int32.Parse(dr["idMenu"].ToString());
                        model.idMenu = Int32.Parse(dr["idMenu"].ToString());
                        model.nameMenu = dr["nameMenu"].ToString();
                        if (dr["initMethod"] != null)
                            model.initMethod = dr["initMethod"].ToString();
                        if (dr["onClickMenu"] != null)
                            model.onClickMenu = dr["onClickMenu"].ToString();
                        if (dr["isGrid"].ToString() != "")
                            model.isGrid = Boolean.Parse(dr["isGrid"].ToString());
                        DataTable childItems = menuDAO.GetMenus(parentId, userid, language);
                        List<MenuModel> childNodes = new List<MenuModel>();
                        if (childItems.Rows.Count > 0)
                        {
                            foreach (DataRow cn in childItems.Rows)
                            {
                                MenuModel child = new MenuModel();

                                child.idMenu = Int32.Parse(cn["idMenu"].ToString());

                                if (cn["initMethod"].ToString() != "")
                                    child.initMethod = cn["initMethod"].ToString();
                                if (cn["onClickMenu"].ToString() != "")
                                    child.onClickMenu = cn["onClickMenu"].ToString();

                                if (cn["idForm"].ToString() != "")
                                    child.idForm = Int32.Parse(cn["idForm"].ToString());

                                child.nameMenu = cn["nameMenu"].ToString();

                                //child.UserControlName = cn["UserControl"].ToString();
                                if (dr["imageMenu"].ToString() != "")
                                {
                                    child.imageMenu = dr["imageMenu"].ToString();
                                }
                                else
                                    child.imageMenu = null;

                                if (dr["imageNew"].ToString() != "")
                                {
                                    child.imageNew = dr["imageNew"].ToString();
                                }
                                else
                                    child.imageNew = null;

                                if (dr["imageDelete"].ToString() != "")
                                {
                                    child.imageDelete = dr["imageDelete"].ToString();
                                }
                                else
                                    child.imageDelete = null;


                                if (cn["isGrid"].ToString() != "")
                                    child.isGrid = Boolean.Parse(cn["isGrid"].ToString());

                                childNodes.Add(child);
                            }
                        }
                        else
                        {
                            childNodes = null;
                        }

                        model.subMenus = childNodes;

                        menus.Add(model);
                    }
                    return menus;
                }
                else
                    return menus;
            }
            else
                return menus;
        }

        public List<IModel> GetMenusForGrid(string language)
        {
            DataTable dataTable = new DataTable();
            dataTable = menuDAO.GetMenusForGrid(language);
            List<IModel> menus = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        MenuRoleModel model = new MenuRoleModel();

                        if (dr["idMenu"].ToString() != "")
                            model.idMenu = Int32.Parse(dr["idMenu"].ToString());
                        if (dr["nameMenu"].ToString() != "")
                            model.nameMenu = dr["nameMenu"].ToString();
                        if (dr["idMenuSuperior"].ToString() != "")
                            model.idMenuSuperior = Int32.Parse(dr["idMenuSuperior"].ToString());
                        if (dr["idSecurity"].ToString() != "")
                            model.idSecurity = Int32.Parse(dr["idSecurity"].ToString());
                        if (dr["nameSecurity"].ToString() != "")
                            model.nameSecurity = dr["nameSecurity"].ToString();

                        menus.Add(model);
                    }
                    return menus;
                }
                else
                    return menus;
            }
            else
                return menus;
        }

        public List<IModel> GetSubMenusForGrid(string language)
        {
            DataTable dataTable = new DataTable();
            dataTable = menuDAO.GetSubMenusForGrid(language);
            List<IModel> menus = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        MenuRoleModel model = new MenuRoleModel();

                        if (dr["idMenu"].ToString() != "")
                            model.idMenu = Int32.Parse(dr["idMenu"].ToString());
                        if (dr["nameMenu"].ToString() != "")
                            model.nameMenu = dr["nameMenu"].ToString();
                        if (dr["idMenuSuperior"].ToString() != "")
                            model.idMenuSuperior = Int32.Parse(dr["idMenuSuperior"].ToString());
                        if (dr["idSecurity"].ToString() != "")
                            model.idSecurity = Int32.Parse(dr["idSecurity"].ToString());
                        if (dr["nameSecurity"].ToString() != "")
                            model.nameSecurity = dr["nameSecurity"].ToString();

                        menus.Add(model);
                    }
                    return menus;
                }
                else
                    return menus;
            }
            else
                return menus;
        }

        public Boolean GetMenuIsGrid(int idMenu)
        {
            Boolean isGrid = false;
            DataTable dataTable = new DataTable();
            dataTable = menuDAO.GetMenuIsGrid(idMenu);


            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    if (dr["isGrid"].ToString() != "")
                        isGrid = Boolean.Parse(dr["isGrid"].ToString());
                    break;
                }
                return isGrid;
            }
            else
            {
                return isGrid;
            }
        }
    }

    
}
