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
    public class UsersBUS
    {
        private UserDAO usersDAO;
        private FiltersDAO filtersDAO;
        private LabelDAO labelDAO;

        public UsersBUS()
        {
            usersDAO = new UserDAO();
            filtersDAO = new FiltersDAO();
            labelDAO = new LabelDAO();
        }

        public UsersModel updatelng(UsersModel user, string lng)
        {
            user.lngUser = lng;
            return user;
        }

        public string getDBName()
        {
            return usersDAO.getDBName();
        }

        public UsersModel Login(string username, string password)
        {
            DataTable dataTable = new DataTable();
            dataTable = usersDAO.LogIn(username, password);

            if (dataTable.Rows.Count > 0)
            {
                UsersModel user = new UsersModel();

                foreach (DataRow dr in dataTable.Rows)
                {
                    user.idUser = Int32.Parse(dr["idUser"].ToString());

                    user.idRole = Int32.Parse(dr["idRole"].ToString());
                    user.username = dr["username"].ToString();
                    user.password = dr["password"].ToString();
                    user.nameUser = dr["nameUser"].ToString();

                    if (dr["isUserLogin"].ToString() != "")
                        user.isUserLogin = Boolean.Parse(dr["isUserLogin"].ToString());

                    if (dr["dtUserLogin"].ToString() != "")
                        user.dtUserLogin = DateTime.Parse(dr["dtUserLogin"].ToString());

                    if (dr["dtUserLogout"].ToString() != "")
                        user.dtUserLogout = DateTime.Parse(dr["dtUserLogout"].ToString());
                    
                    if (dr["idCompany"].ToString() != "")
                        user.idCompany = Int32.Parse(dr["idCompany"].ToString());
                    
                    if (dr["idEmployee"].ToString() != "")
                        user.idEmployee = Int32.Parse(dr["idEmployee"].ToString());
                    
                    if (dr["nameEmployee"].ToString() != "")
                        user.nameEmployee = dr["nameEmployee"].ToString();

                    if (dr["isNotActive"].ToString() != "")
                        user.isNotActive = Boolean.Parse(dr["isNotActive"].ToString());

                    if (dr["isFinishCalculation"].ToString() != "")
                        user.isFinishCalculation = Boolean.Parse(dr["isFinishCalculation"].ToString());

                    if (dr["isDontSeeMedVol"].ToString() != "")
                        user.isDontSeeMedVol = Boolean.Parse(dr["isDontSeeMedVol"].ToString());

                    if (dr["isUserManager"].ToString() != "")
                        user.isUserManager = Boolean.Parse(dr["isUserManager"].ToString());

                    if (dr["isFirstTimeStarted"].ToString() != "")
                        user.isFirstTimeStarted = Boolean.Parse(dr["isFirstTimeStarted"].ToString());

                    if (dr["isAccountUser"].ToString() != "")
                        user.isAccountUser = Boolean.Parse(dr["isAccountUser"].ToString());

                    if (dr["dtPassChanged"].ToString() != "")
                        user.dtPassChanged = DateTime.Parse(dr["dtPassChanged"].ToString());

                    if (dr["numDaysPassValid"].ToString() != "")
                        user.numDaysPassValid = Decimal.Parse(dr["numDaysPassValid"].ToString());

                    if (dr["numDaysStartWarn"].ToString() != "")
                        user.numDaysStartWarn = Decimal.Parse(dr["numDaysStartWarn"].ToString());

                    if (dr["dtUserCreated"].ToString() != "")
                        user.dtUserCreated = DateTime.Parse(dr["dtUserCreated"].ToString());

                    if (dr["dtUserModified"].ToString() != "")
                        user.dtUserModified = DateTime.Parse(dr["dtUserModified"].ToString());

                    if (dr["isAccountManager"].ToString() != "")
                        user.isAccountManager = Boolean.Parse(dr["isAccountManager"].ToString());

                    user.lngUser = dr["lngUser"].ToString();
                    user.emailUser = dr["emailUser"].ToString();

                }

                return user;
            }
            else
            {
                return null;
            }



        }

        public List<RoleSecurityModel> IsInRoleSecurity(string language, int idUser)
        {
            DataTable dataTable = new DataTable();

            dataTable = usersDAO.IsInRoleSecurity(language, idUser);
            List<RoleSecurityModel> menu = new List<RoleSecurityModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        RoleSecurityModel model = new RoleSecurityModel();
                        if (dr["idMenu"].ToString() != "")
                            model.idMenu = Int32.Parse(dr["idMenu"].ToString());
                        model.By = dr["By"].ToString();
                        if (dr["idSecurity"].ToString() != "")
                            model.idSecurity = Int32.Parse(dr["idSecurity"].ToString());
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

        public UsersModel getUserExact(int iduser)
        {
            DataTable dataTable = new DataTable();
            dataTable = usersDAO.UserExact(iduser);

            if (dataTable.Rows.Count > 0)
            {
                UsersModel user = new UsersModel();

                foreach (DataRow dr in dataTable.Rows)
                {
                    user.idUser = Int32.Parse(dr["idUser"].ToString());
                    user.idRole = Int32.Parse(dr["idRole"].ToString());
                    user.username = dr["username"].ToString();
                    user.password = dr["password"].ToString();
                    user.nameUser = dr["nameUser"].ToString();
                    user.isUserLogin = Boolean.Parse(dr["isUserLogin"].ToString());


                    user.dtUserLogin = DateTime.Parse(dr["dtUserLogin"].ToString());
                    user.dtUserLogout = DateTime.Parse(dr["dtUserLogout"].ToString());
                    user.idCompany = Int32.Parse(dr["idCompany"].ToString());
                    user.idEmployee = Int32.Parse(dr["idEmployee"].ToString());


                    if (dr["isNotActive"].ToString() != "")
                        user.isNotActive = Boolean.Parse(dr["isNotActive"].ToString());

                    if (dr["isFinishCalculation"].ToString() != "")
                        user.isFinishCalculation = Boolean.Parse(dr["isFinishCalculation"].ToString());

                    if (dr["isDontSeeMedVol"].ToString() != "")
                        user.isDontSeeMedVol = Boolean.Parse(dr["isDontSeeMedVol"].ToString());

                    if (dr["isUserManager"].ToString() != "")
                        user.isUserManager = Boolean.Parse(dr["isUserManager"].ToString());

                    if (dr["isFirstTimeStarted"].ToString() != "")
                        user.isFirstTimeStarted = Boolean.Parse(dr["isFirstTimeStarted"].ToString());

                    if (dr["isAccountUser"].ToString() != "")
                        user.isAccountUser = Boolean.Parse(dr["isAccountUser"].ToString());

                    if (dr["dtPassChanged"].ToString() != "")
                        user.dtPassChanged = DateTime.Parse(dr["dtPassChanged"].ToString());

                    if (dr["numDaysPassValid"].ToString() != "")
                        user.numDaysPassValid = Decimal.Parse(dr["numDaysPassValid"].ToString());

                    if (dr["numDaysStartWarn"].ToString() != "")
                        user.numDaysStartWarn = Decimal.Parse(dr["numDaysStartWarn"].ToString());

                    if (dr["dtUserCreated"].ToString() != "")
                        user.dtUserCreated = DateTime.Parse(dr["dtUserCreated"].ToString());

                    if (dr["dtUserModified"].ToString() != "")
                        user.dtUserModified = DateTime.Parse(dr["dtUserModified"].ToString());

                    if (dr["isAccountManager"].ToString() != "")
                        user.isAccountManager = Boolean.Parse(dr["isAccountManager"].ToString());

                    user.lngUser = dr["lngUser"].ToString();
                    user.emailUser = dr["emailUser"].ToString();

                }

                return user;
            }
            else
            {
                return null;
            }

        }

        public List<RoleSecurityModel> GetAllRoleSecurity(string language, int idUser)
        {
            DataTable dataTable = new DataTable();

            dataTable = usersDAO.GetRoleSecurity(language, idUser);
            List<RoleSecurityModel> menu = new List<RoleSecurityModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        RoleSecurityModel model = new RoleSecurityModel();
                        model.idMenu = Int32.Parse(dr["idMenu"].ToString());
                        model.By = dr["By"].ToString();
                        model.idSecurity = Int32.Parse(dr["idSecurity"].ToString());
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

        public List<TypesMenuModel> GetAllMenu()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = usersDAO.GetAllMenu();

                List<TypesMenuModel> type = new List<TypesMenuModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            TypesMenuModel model = new TypesMenuModel();

                            model.idMenu = Int32.Parse(dr["idMenu"].ToString());
                            model.nameMenu = dr["nameMenu"].ToString();


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

        public String encryptPassword(string password)
        {
            return usersDAO.CryptPass(password);
        }

        public Boolean ChangePassword(string idUser, string password, string nameForm, int idUser1)
        {
            return usersDAO.ChangePass(idUser, password,nameForm,idUser1);
        }

        public Boolean ComparePassword(string oldPassword, string newPassword)
        {
            return usersDAO.ComparePass(oldPassword, newPassword);
        }

        public List<FilterModel> GetPersonFilters(string language)
        {
            DataTable dataTable = new DataTable();
            dataTable = filtersDAO.GetPersonFilters(language);

            if (dataTable.Rows.Count > 0)
            {
                List<FilterModel> filters = new List<FilterModel>();

                foreach (DataRow dr in dataTable.Rows)
                {
                    FilterModel model = new FilterModel();

                    model.idFilter = Int32.Parse(dr["idFilter"].ToString());
                    model.nameFilter = dr["nameFilter"].ToString();

                    if (dr["sortFilter"].ToString() != "")
                        model.sortFilter = Int32.Parse(dr["sortFilter"].ToString());

                    filters.Add(model);
                }

                return filters;
            }
            else
            {
                return null;
            }


        }

        public List<FilterModel> GetClientFilters(string language)
        {
            DataTable dataTable = new DataTable();
            dataTable = filtersDAO.GetClientFilters(language);

            if (dataTable.Rows.Count > 0)
            {
                List<FilterModel> filters = new List<FilterModel>();

                foreach (DataRow dr in dataTable.Rows)
                {
                    FilterModel model = new FilterModel();

                    model.idFilter = Int32.Parse(dr["idFilter"].ToString());
                    model.nameFilter = dr["nameFilter"].ToString();

                    if (dr["sortFilter"].ToString() != "")
                        model.sortFilter = Int32.Parse(dr["sortFilter"].ToString());

                    filters.Add(model);
                }

                return filters;
            }
            else
            {
                return null;
            }


        }

        public List<FilterModel> GetUsersFilters(string language)
        {
            DataTable dataTable = new DataTable();
            dataTable = filtersDAO.GetUsersFilters(language);

            if (dataTable.Rows.Count > 0)
            {
                List<FilterModel> filters = new List<FilterModel>();

                foreach (DataRow dr in dataTable.Rows)
                {
                    FilterModel model = new FilterModel();

                    model.idFilter = Int32.Parse(dr["idFilter"].ToString());
                    model.nameFilter = dr["nameFilter"].ToString();

                    if (dr["sortFilter"].ToString() != "")
                        model.sortFilter = Int32.Parse(dr["sortFilter"].ToString());

                    filters.Add(model);
                }

                return filters;
            }
            else
            {
                return null;
            }


        }

        public List<FilterModel> GetEmployeeFilters(string language)
        {
            DataTable dataTable = new DataTable();
            dataTable = filtersDAO.GetEmployeeFilters(language);

            if (dataTable.Rows.Count > 0)
            {
                List<FilterModel> filters = new List<FilterModel>();

                foreach (DataRow dr in dataTable.Rows)
                {
                    FilterModel model = new FilterModel();

                    model.idFilter = Int32.Parse(dr["idFilter"].ToString());
                    model.nameFilter = dr["nameFilter"].ToString();

                    if (dr["sortFilter"].ToString() != "")
                        model.sortFilter = Int32.Parse(dr["sortFilter"].ToString());

                    filters.Add(model);
                }

                return filters;
            }
            else
            {
                return null;
            }


        }

        public List<TypesSecurityModel> GetAllSecurity()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = usersDAO.GetAllSecurity();

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

        public bool IdRoleUpdate(int idUser, int idRole, string nameForm, int idUser1)
        {
            bool retval = false;
            try
            {

                retval = usersDAO.IdRoleUpdate(idUser, idRole,nameForm,idUser1);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool MenuUserUpdate(int idMenu, int idUser, int idSecurity, string nameForm, int idUser1)
        {
            bool retval = false;
            try
            {

                retval = usersDAO.MenuUserUpdate(idMenu, idUser, idSecurity, nameForm, idUser1);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertMenuSecurity(int idMenu, int idSecurity, int idUser, string nameForm, int idUser1)
        {
            bool retval = false;
            try
            {

                retval = usersDAO.InsertMenuSecurity(idMenu, idSecurity, idUser, nameForm, idUser1);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteMenuUser(int idMenu, int idUser, int idSecurity, string nameForm, int idUser1)
        {
            bool retval = false;
            try
            {

                retval = usersDAO.DeleteMenuUser(idMenu, idUser, idSecurity, nameForm, idUser1);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;


        }

        public List<TranslateUstrModel> Translate(string stringKey, string language)
        {
            DataTable dataTable = new DataTable();

            dataTable = usersDAO.Translate(stringKey, language);
            List<TranslateUstrModel> menu = new List<TranslateUstrModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        TranslateUstrModel model = new TranslateUstrModel();
                        model.stringValue = (dr["stringValue"].ToString());

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
        
        public List<LanguagesModel> GetAllLanguages()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = usersDAO.GetAllLanguages();

                List<LanguagesModel> language = new List<LanguagesModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            LanguagesModel model = new LanguagesModel();

                            model.idLang = Int32.Parse(dr["idLang"].ToString());
                            model.nameLang = dr["nameLang"].ToString();


                            language.Add(model);
                        }
                        return language;
                    }
                    else
                        return language;
                }
                else
                    return language;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<RoleSecurityModel> MenuUserSecurity(string language, int idUser)
        {
            DataTable dataTable = new DataTable();

            dataTable = usersDAO.MenuUserSecurity(language, idUser);
            List<RoleSecurityModel> menu = new List<RoleSecurityModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        RoleSecurityModel model = new RoleSecurityModel();
                        model.idMenu = Int32.Parse(dr["idMenu"].ToString());
                        model.By = dr["By"].ToString();
                        model.idSecurity = Int32.Parse(dr["idSecurity"].ToString());
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

        public List<RoleSecurityModel> MenuRoleSecurity(string language, int idUser)
        {
            DataTable dataTable = new DataTable();

            dataTable = usersDAO.MenuRoleSecurity(language, idUser);
            List<RoleSecurityModel> menu = new List<RoleSecurityModel>();

            if (dataTable != null )
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        RoleSecurityModel model = new RoleSecurityModel();
                    
                        if (dr["idMenu"].ToString() != "")
                            model.idMenu = Int32.Parse(dr["idMenu"].ToString());
                        if (dr["idMenu"].ToString() != "" || dr["idSecurity"].ToString() != "")
                            model.nameMenu = dr["nameMenu"].ToString();
                            model.By = dr["By"].ToString();
                        if (dr["idSecurity"].ToString() != "")
                            model.idSecurity = Int32.Parse(dr["idSecurity"].ToString());
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

            dataTable = usersDAO.GetSubMenus(language, idRole);
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

        public List<RoleSecurityModel> GetSubMenusUser(string language, int idUser)
        {
            DataTable dataTable = new DataTable();

            dataTable = usersDAO.GetSubMenusUser(language, idUser);
            List<RoleSecurityModel> menu = new List<RoleSecurityModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        RoleSecurityModel model = new RoleSecurityModel();
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

        public List<RoleSecurityModel> GetSubMenusForSuperiorForRole(string language, int idUser, int idMenuSuperior)
        {
            DataTable dataTable = new DataTable();

            dataTable = usersDAO.GetSubMenusForSuperiorForRole(language, idUser, idMenuSuperior);
            List<RoleSecurityModel> menu = new List<RoleSecurityModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        RoleSecurityModel model = new RoleSecurityModel();
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

        public List<RoleSecurityModel> GetSubMenusForSuperior(string language, int idUser, int idMenuSuperior)
        {
            DataTable dataTable = new DataTable();

            dataTable = usersDAO.GetSubMenusForSuperior(language, idUser, idMenuSuperior);
            List<RoleSecurityModel> menu = new List<RoleSecurityModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        RoleSecurityModel model = new RoleSecurityModel();
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

        public bool Insert(int idMenu, int idUser, int idSecurity, string nameForm, int idUser1)
        {
            bool retval = false;
            try
            {

                retval = usersDAO.Insert(idMenu, idUser, idSecurity, nameForm, idUser1);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Delete(int idMenu, int idUser, string nameForm, int idUser1)
        {
            bool retval = false;
            try
            {

                retval = usersDAO.Delete(idMenu, idUser, nameForm, idUser1);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteMainMenus(int idUser, string nameForm, int idUser1)
        {
            bool retval = false;
            try
            {

                retval = usersDAO.DeleteMainMenus(idUser, nameForm, idUser1);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool loginLogOut(DateTime dt, int idUser, bool isUserLogin)
        {
            return usersDAO.logInLogOut(dt, idUser, isUserLogin);
        }
        public int checkUser(string ip)
        {
            int a;
            
            try
            {
                DataTable dt = usersDAO.checkUser(ip);
                try
                {
                    a = (int)dt.Rows[0]["time"];
                }
                catch (Exception)
                {
                    a = -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return a;
        }
        public bool insertUsersIp(string ip)
        {
            bool retval;
            try
            {

                retval = usersDAO.insertUsersIp(ip);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }
    }
}

