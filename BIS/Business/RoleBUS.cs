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
    public class RoleBUS
    {
        private RoleDAO RoleDAO;

        public RoleBUS()
        {
            RoleDAO = new RoleDAO();
        }

        public List<IModel> GetAllRole()
        {
            DataTable dataTable = new DataTable();
            dataTable = RoleDAO.GetAllRole();
            List<IModel> roles = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        RoleModel model = new RoleModel();

                        model.idRole = Int32.Parse(dr["idRole"].ToString());
                        model.nameRole = dr["nameRole"].ToString(); ;


                        roles.Add(model);
                    }
                    return roles;
                }
                else
                    return roles;
            }
            else
                return roles;
        }

        public List<MenuRoleModel> GetMenus(string language, int idRole)
        {
            DataTable dataTable = new DataTable();

            dataTable = RoleDAO.GetMenus(language, idRole);
            List<MenuRoleModel> menu = new List<MenuRoleModel>();

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
                        menu.Add(model);
                    }
                    return menu;
                }
                else
                    return menu;
            }
            else
                return menu;
        }

        public List<MenuRoleModel> GetSubMenus(string language, int idRole)
        {
            DataTable dataTable = new DataTable();

            dataTable = RoleDAO.GetSubMenus(language, idRole);
            List<MenuRoleModel> menu = new List<MenuRoleModel>();

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
                        menu.Add(model);
                    }
                    return menu;
                }
                else
                    return menu;
            }
            else
                return menu;
        }

        public List<MenuRoleModel> GetSubMenusForSuperiorForRole(string language, int idRole, int idMenuSuperior)
        {
            DataTable dataTable = new DataTable();

            dataTable = RoleDAO.GetSubMenusForSuperiorForRole(language, idRole, idMenuSuperior);
            List<MenuRoleModel> menu = new List<MenuRoleModel>();

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
                        menu.Add(model);
                    }
                    return menu;
                }
                else
                    return menu;
            }
            else
                return menu;
        }

        public List<MenuRoleModel> GetMainMenusForRole(string language, int idRole)
        {
            DataTable dataTable = new DataTable();

            dataTable = RoleDAO.GetMainMenusForRole(language, idRole);
            List<MenuRoleModel> menu = new List<MenuRoleModel>();

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
                        menu.Add(model);
                    }
                    return menu;
                }
                else
                    return menu;
            }
            else
                return menu;
        }

        public List<MenuRoleModel> GetSubMenusForSuperior(string language, int idRole, int idMenuSuperior)
        {
            DataTable dataTable = new DataTable();

            dataTable = RoleDAO.GetSubMenusForSuperior(language, idRole, idMenuSuperior);
            List<MenuRoleModel> menu = new List<MenuRoleModel>();

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
                        menu.Add(model);
                    }
                    return menu;
                }
                else
                    return menu;
            }
            else
                return menu;
        }

        public List<MenuRoleModel> GetMainMenus(string language, int idRole)
        {
            DataTable dataTable = new DataTable();

            dataTable = RoleDAO.GetMainMenus(language, idRole);
            List<MenuRoleModel> menu = new List<MenuRoleModel>();

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
                        menu.Add(model);
                    }
                    return menu;
                }
                else
                    return menu;
            }
            else
                return menu;
        }

        public bool Delete(int idMenu, int idRole, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = RoleDAO.Delete(idMenu, idRole, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteMainMenus(int idRole, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = RoleDAO.DeleteMainMenus(idRole, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Insert(int idMenu, int idSecurity, int idRole, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = RoleDAO.Insert(idMenu, idSecurity, idRole, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertRole(string nameRole, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = RoleDAO.InsertRole(nameRole, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<TypesSecurityModel> GetAllSecurity()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = RoleDAO.GetAllSecurity();

                List<TypesSecurityModel> type = new List<TypesSecurityModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            TypesSecurityModel model = new TypesSecurityModel();

                            model.idSecurity = Int32.Parse(dr["idSecurity"].ToString());
                            model.nameSecurity = dr["nameSecurity"].ToString();


                            type.Add(model);
                        }
                        return type;
                    }
                    else
                        return type;
                }
                else
                    return type;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public RoleModel GetLastRoleID()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = RoleDAO.GetLastRoleID();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        RoleModel rm = new RoleModel();
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            if (dr["idRole"].ToString() != "")
                                rm.idRole = Int32.Parse(dr["idRole"].ToString());
                           
                        }
                        return rm;
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        // NOVO DELETE
        public int checkIsInUsers(int idRole)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = RoleDAO.checkIsInUsers(idRole);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }

        public int checkIsInMenuRoleSecurity(int idRole)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = RoleDAO.checkIsInMenuRoleSecurity(idRole);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }

        public bool DeleteRoleSript(int idRole, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = RoleDAO.DeleteRoleScript(idRole, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

           

    }
}

  