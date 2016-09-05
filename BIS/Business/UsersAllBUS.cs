using BIS.DAO;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Business
{
  public  class UsersAllBUS 
    {
         private UserDAO userDAO;

        public UsersAllBUS()
        {
            userDAO = new UserDAO();
        }

        public bool UpdateUser(string username, string password, string emailUser, int idEmployee, bool isUserLogin, bool isNotActive, bool isFinishCalculation, bool isUserManager, bool isFirstTimeStarted,
                                 DateTime dtPassChanged, DateTime dtLogOnTime, DateTime dtLogOffTime, decimal numDaysPassValid, decimal numDaysStartWarn, int iduser, string nameUser, string lngUser, bool isAccountUser, bool isDontSeeMedVol, bool isAccountManager, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = userDAO.UsersUpdate(username, password, emailUser, idEmployee, isUserLogin, isNotActive, isFinishCalculation, isUserManager, isFirstTimeStarted, dtPassChanged, dtLogOnTime, dtLogOffTime, numDaysPassValid, numDaysStartWarn, iduser, nameUser, lngUser, isAccountUser, isDontSeeMedVol, isAccountManager,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }




        public bool IsertUser(string username, string password, string fullName, int idRole, int idEmpl, int idCompany, string emailUser, bool isUserLogin, bool isNotActive, bool isFinishCalculation, bool isUserManager, bool isFirstTimeStarted,
                               DateTime dtPassChanged, DateTime dtLogOnTime, DateTime dtLogOffTime, decimal numDaysPassValid, decimal numDaysStartWarn, string lngUser, bool isAccountUser, bool isDontSeeMedVol, bool isAccountManager, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = userDAO.UsersInsert(username, password, fullName, idRole, idEmpl, idCompany, emailUser, isUserLogin, isNotActive, isFinishCalculation, isUserManager, isFirstTimeStarted, dtPassChanged, dtLogOnTime, dtLogOffTime, numDaysPassValid, numDaysStartWarn, lngUser, isAccountUser, isDontSeeMedVol, isAccountManager, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public List<IModel> GetAllUsers()
        {
            DataTable dataTable = new DataTable();
            dataTable = userDAO.UsersAll();
            List<IModel> users = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        UsersAllModel model = new UsersAllModel();
                        model.idUser = Int32.Parse(dr["idUser"].ToString());

                        if (dr["idRole"].ToString() != "" || dr["idRole"].ToString() != null)
                            model.idRole = Int32.Parse(dr["idRole"].ToString());

                        model.nameRole = dr["nameRole"].ToString();
                        model.username = dr["username"].ToString();
                        model.password = DecryptPassword(dr["password"].ToString());
                        model.nameUser = dr["nameUser"].ToString();

                        if (dr["isUserLogin"].ToString() != "")
                            model.isUserLogin = Boolean.Parse(dr["isUserLogin"].ToString());


                        if (dr["dtUserLogin"].ToString() != "")
                            model.dtUserLogin = DateTime.Parse(dr["dtUserLogin"].ToString());

                        if (dr["dtUserLogout"].ToString() != "")
                            model.dtUserLogout = DateTime.Parse(dr["dtUserLogout"].ToString());

                        if (dr["idEmployee"].ToString() != "")
                            model.idEmployee = Int32.Parse(dr["idEmployee"].ToString());

                        if (dr["nameEmployee"].ToString() != "")
                            model.nameEmployee = dr["nameEmployee"].ToString();

                        if (dr["idCompany"].ToString() != "")
                            model.idCompany = Int32.Parse(dr["idCompany"].ToString());

                        if (dr["isNotActive"].ToString() != "")
                            model.isNotActive = Boolean.Parse(dr["isNotActive"].ToString());

                        if (dr["isFinishCalculation"].ToString() != "")
                            model.isFinishCalculation = Boolean.Parse(dr["isFinishCalculation"].ToString());

                        if (dr["isDontSeeMedVol"].ToString() != "")
                            model.isDontSeeMedVol = Boolean.Parse(dr["isDontSeeMedVol"].ToString());

                        if (dr["isUserManager"].ToString() != "")
                            model.isUserManager = Boolean.Parse(dr["isUserManager"].ToString());

                        if (dr["isFirstTimeStarted"].ToString() != "")
                            model.isFirstTimeStarted = Boolean.Parse(dr["isFirstTimeStarted"].ToString());

                        if (dr["isAccountUser"].ToString() != "")
                            model.isAccountUser = Boolean.Parse(dr["isAccountUser"].ToString());

                        if (dr["dtPassChanged"].ToString() != "")
                            model.dtPassChanged = DateTime.Parse(dr["dtPassChanged"].ToString());

                        if (dr["numDaysPassValid"].ToString() != "")
                            model.numDaysPassValid = Decimal.Parse(dr["numDaysPassValid"].ToString());

                        if (dr["numDaysStartWarn"].ToString() != "")
                            model.numDaysStartWarn = Decimal.Parse(dr["numDaysStartWarn"].ToString());

                        if (dr["lngUser"].ToString() != "")
                            model.lngUser = dr["lngUser"].ToString();

                        if (dr["isAccountManager"].ToString() != "")
                            model.isAccountManager = Boolean.Parse(dr["isAccountManager"].ToString());

                        model.emailUser = dr["emailUser"].ToString();

                        users.Add(model);
                    }
                    return users;
                }
                else
                    return users;
            }
            else
                return users;
        }


        
        public String encryptPassword(string password)
        {
            return userDAO.CryptPass(password);
        }

        public String DecryptPassword(string password)
        {
            return userDAO.DecryptPass(password);
        }

        public bool DeleteUserSript(int idUser)
        {
            bool retval = false;
            try
            {

                retval = userDAO.DeleteUserSript(idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
    }
}
