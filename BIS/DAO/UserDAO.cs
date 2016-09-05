using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;
using System.Data.SqlTypes;

namespace BIS.DAO
{
    public class UserDAO
    {
        private dbConnection conn;
        private Crypt crypt;

        public UserDAO()
        {
            conn = new dbConnection();
            crypt = new Crypt();
        }

        public string getDBName()
        {
            return conn.DB;
        }

        public DataTable UsersAll()
        {
            string query = string.Format(@"SELECT u.idUser,u.idRole,r.nameRole,u.idCompany,u.username,u.password,u.nameUser,u.isUserLogin,u.dtUserLogin,u.dtUserLogout,u.idEmployee,e.firstNameEmployee + ' ' +e.lastNameEmployee  as nameEmployee,u.isNotActive,
                u.isFinishCalculation,u.isUserManager,u.isFirstTimeStarted,u.dtPassChanged,u.numDaysPassValid,u.numDaysStartWarn,u.lngUser,u.emailUser, u.isAccountUser, u.isDontSeeMedVol,u.isAccountManager
                           FROM Users u
                INNER JOIN Roles r on u.idRole=r.idRole
                LEFT OUTER JOIN Employees e on e.idEmployee = u.idEmployee");
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable UserExact(int idUser)
        {
            string query = string.Format(@" SELECT idUser,idRole,username,password,nameUser,isUserLogin,dtUserLogin,dtUserLogout,idCompany,idEmployee,isNotActive,
                isFinishCalculation,isUserManager,isFirstTimeStarted,dtPassChanged,numDaysPassValid,numDaysStartWarn,dtUserCreated, 
                dtUserModified,lngUser,emailUser,isAccountUser, isDontSeeMedVol,isAccountManager 
                FROM Users  WHERE idUser='" + idUser + "'");
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable LogIn(string username, string password)
        {
            string query = string.Format(@"SELECT u.idUser,u.idRole,u.username,u.password,e.firstNameEmployee + ' ' +e.midNameEmployee + ' ' + e.lastNameEmployee as nameUser,u.isUserLogin,u.dtUserLogin,u.dtUserLogout,u.idCompany,u.idEmployee,u.isNotActive,
                u.isFinishCalculation,u.isUserManager,u.isFirstTimeStarted,u.dtPassChanged,u.numDaysPassValid,u.numDaysStartWarn,u.dtUserCreated, 
                u.dtUserModified,u.lngUser,u.emailUser,e.firstNameEmployee + ' ' +e.midNameEmployee + ' ' + e.lastNameEmployee  as nameEmployee,u.isAccountUser, u.isDontSeeMedVol,u.isAccountManager 
                FROM Users u
                LEFT OUTER JOIN Employees e on e.idEmployee = u.idEmployee
	        	WHERE u.username = @username AND u.password = @password");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@username", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(username.Trim());
            sqlParameters[1] = new SqlParameter("@password", SqlDbType.VarChar);
            sqlParameters[1].Value = Convert.ToString(password.Trim());
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public Boolean ChangePass(string idUser, string password, string nameForm, int idUser1)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE Users
                SET password = @password, dtPassChanged = @dtPassChanged, 
                isFirstTimeStarted = @isFirstTimeStarted
	        	WHERE idUser = @idUser");

            SqlParameter[] sqlParameter = new SqlParameter[4];
            sqlParameter[0] = new SqlParameter("@password", SqlDbType.VarChar);
            sqlParameter[0].Value = crypt.Encrypt(Convert.ToString(password.Trim()));
            sqlParameter[1] = new SqlParameter("@dtPassChanged", SqlDbType.DateTime);
            sqlParameter[1].Value = DateTime.Now;
            sqlParameter[2] = new SqlParameter("@isFirstTimeStarted", SqlDbType.Bit);
            sqlParameter[2].Value = false;
            sqlParameter[3] = new SqlParameter("@idUser", SqlDbType.VarChar);
            sqlParameter[3].Value = Convert.ToString(idUser.Trim());


            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");

            sqlParameter = new SqlParameter[8];


            sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameForm;

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = idUser1;

            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "U";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = idUser.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idUser";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Users";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Change pass";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean ComparePass(string oldPassword, string newPassword)
        {

            if (oldPassword.Equals(crypt.Encrypt(newPassword)))
            {
                return true;
            }
            else
                return false;
        }

        public string CryptPass(string password)
        {
            return crypt.Encrypt(password);
        }

        public string DecryptPass(string password)
        {
            return crypt.Decrypt(password);
        }

        public DataTable GetRoleSecurity(string language, int idUser)
        {
            string query = string.Format(@"SELECT mrs.idMenu,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,
                                           CASE WHEN SB.stringKey IS NOT NULL THEN SB.stringValue ELSE 'Role' END [By],mrs.idSecurity, 
                                           CASE WHEN SNS.stringKey IS NOT NULL THEN SNS.stringValue ELSE se.nameSecurity END nameSecurity
                                         FROM Users u 
                                         LEFT OUTER JOIN MenuRoleSecurity mrs ON mrs.idRole = u.idRole
                                         LEFT OUTER JOIN Menu m ON m.idMenu = mrs.idMenu
                                         LEFT OUTER JOIN Security se ON mrs.idSecurity = se.idSecurity
                                         LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM( m.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @" SB on RTRIM(LTRIM('Role')) = RTRIM(LTRIM(SB.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @"  SNS on RTRIM(LTRIM(se.nameSecurity)) = RTRIM(LTRIM(SNS.stringKey))
                                         WHERE idUser=@idUser AND mrs.idMenu NOT IN (SELECT DISTINCT idMenu FROM MenuUserSecurity WHERE idUser=@idUser)
                                         UNION
                                         SELECT mrs.idMenu,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,CASE WHEN SB.stringKey IS NOT NULL THEN SB.stringValue ELSE 'User' END [By],mrs.idSecurity, CASE WHEN SNS.stringKey IS NOT NULL THEN SNS.stringValue ELSE se.nameSecurity END nameSecurity
                                         FROM Users u 
                                         LEFT OUTER JOIN MenuUserSecurity mrs ON mrs.idUser = u.idUser
                                         LEFT OUTER JOIN Menu m ON m.idMenu = mrs.idMenu
                                         LEFT OUTER JOIN Security se ON mrs.idSecurity = se.idSecurity
                                         LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM( m.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @"  SB on RTRIM(LTRIM('User')) = RTRIM(LTRIM(SB.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @"  SNS on RTRIM(LTRIM(se.nameSecurity)) = RTRIM(LTRIM(SNS.stringKey))
                                         WHERE mrs.idUser=@idUser");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@language", SqlDbType.NVarChar);
            sqlParameters[0].Value = Convert.ToString(language.Trim());

            sqlParameters[1] = new SqlParameter("@idUser", SqlDbType.NVarChar);
            sqlParameters[1].Value = idUser;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAllMenu()
        {

            string query = string.Format(@"SELECT idMenu,nameMenu FROM Menu ");

            return conn.executeSelectQuery(query, null);

        }

        public bool UsersUpdate(string username, string password, string emailUser, int idEmployee,  bool isUserLogin, bool isNotActive, bool isFinishCalculation, bool isUserManager, bool isFirstTimeStarted, DateTime dtPassChanged,
                                DateTime dtLogOnTime, DateTime dtLogOffTime, decimal numDaysPassValid, decimal numDaysStartWarn, int iduser, string nameUser, string lngUser, bool isAccountUser, bool isDontSeeMedVol, bool isAccountManager, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE Users
                SET   username=@username, password=@password, emailUser =@emailUser,idEmployee=@idEmployee,isUserLogin=@isUserLogin, isNotActive=@isNotActive, isFinishCalculation=@isFinishCalculation, isUserManager=@isUserManager,
                                   isFirstTimeStarted=@isFirstTimeStarted, dtPassChanged=@dtPassChanged, dtUserLogin=@dtLogOnTime,
                                   dtUserLogout=@dtLogOffTime,numDaysPassValid=@numDaysPassValid,numDaysStartWarn=@numDaysStartWarn, nameUser=@nameUser, lngUser=@lngUser, isAccountUser=@isAccountUser, isDontSeeMedVol=@isDontSeeMedVol,
                 isAccountManager=@isAccountManager
                 WHERE idUser = @idUser ");

            SqlParameter[] sqlParameter = new SqlParameter[20];

            sqlParameter[0] = new SqlParameter("@username", SqlDbType.NVarChar);
            sqlParameter[0].Value = Convert.ToString(username.Trim());


            sqlParameter[1] = new SqlParameter("@emailUser", SqlDbType.NVarChar);
            sqlParameter[1].Value = Convert.ToString(emailUser.Trim());

            sqlParameter[2] = new SqlParameter("@idEmployee", SqlDbType.Int);
            sqlParameter[2].Value = (idEmployee == -1) ? SqlInt32.Null : idEmployee;

            sqlParameter[3] = new SqlParameter("@isUserLogin", SqlDbType.Bit);
            sqlParameter[3].Value = isUserLogin;

            sqlParameter[4] = new SqlParameter("@isNotActive", SqlDbType.Bit);
            sqlParameter[4].Value = isNotActive;

            sqlParameter[5] = new SqlParameter("@isFinishCalculation", SqlDbType.Bit);
            sqlParameter[5].Value = isFinishCalculation;

            sqlParameter[6] = new SqlParameter("@isUserManager", SqlDbType.Bit);
            sqlParameter[6].Value = isUserManager;

            sqlParameter[7] = new SqlParameter("@isFirstTimeStarted", SqlDbType.Bit);
            sqlParameter[7].Value = isFirstTimeStarted;

            sqlParameter[8] = new SqlParameter("@dtPassChanged", SqlDbType.DateTime);
            sqlParameter[8].Value = (dtPassChanged == null || dtPassChanged == DateTime.MinValue) ? SqlDateTime.Null : dtPassChanged;

            sqlParameter[9] = new SqlParameter("@dtLogOnTime", SqlDbType.DateTime);
            sqlParameter[9].Value = (dtLogOnTime == null || dtLogOnTime == DateTime.MinValue) ? SqlDateTime.Null : dtLogOnTime;

            sqlParameter[10] = new SqlParameter("@dtLogOffTime", SqlDbType.DateTime);
            sqlParameter[10].Value = (dtLogOffTime == null || dtLogOffTime == DateTime.MinValue) ? SqlDateTime.Null : dtLogOffTime;


            sqlParameter[11] = new SqlParameter("@numDaysPassValid", SqlDbType.Decimal);
            sqlParameter[11].Value = (numDaysPassValid == -1) ? SqlDecimal.Null : numDaysPassValid;

            sqlParameter[12] = new SqlParameter("@numDaysStartWarn", SqlDbType.Decimal);
            sqlParameter[12].Value = (numDaysStartWarn == -1) ? SqlDecimal.Null : numDaysStartWarn;

            sqlParameter[13] = new SqlParameter("@idUser", SqlDbType.NVarChar);
            sqlParameter[13].Value = iduser;

            sqlParameter[14] = new SqlParameter("@nameUser", SqlDbType.NVarChar);
            sqlParameter[14].Value = Convert.ToString(nameUser.Trim());

            sqlParameter[15] = new SqlParameter("@lngUser", SqlDbType.NVarChar);
            sqlParameter[15].Value = Convert.ToString(lngUser.Trim());

            sqlParameter[16] = new SqlParameter("@isAccountUser", SqlDbType.Bit);
            sqlParameter[16].Value = isAccountUser;

            sqlParameter[17] = new SqlParameter("@password", SqlDbType.NVarChar);
            sqlParameter[17].Value = Convert.ToString(password.Trim());

            sqlParameter[18] = new SqlParameter("@isDontSeeMedVol", SqlDbType.Bit);
            sqlParameter[18].Value = isDontSeeMedVol;

            sqlParameter[19] = new SqlParameter("@isAccountManager", SqlDbType.Bit);
            sqlParameter[19].Value = isAccountManager;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");

            sqlParameter = new SqlParameter[8];


            sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameForm;

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = idUser;

            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "U";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = iduser.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idUser";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Users";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Users update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
           
        }

        public bool UsersInsert(string username, string password, string nameUser, int idRole, int idEmployee,int idCompany, string emailUser, bool isUserLogin, bool isNotActive, bool isFinishCalculation, bool isUserManager, bool isFirstTimeStarted, DateTime dtPassChanged,
                                 DateTime dtLogOnTime, DateTime dtLogOffTime, decimal numDaysPassValid, decimal numDaysStartWarn, string lngUser, bool isAccountUser, bool isDontSeeMedVol, bool isAccountManager, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO 
                                   Users(username,password,nameUser,idRole,idEmployee,idCompany,emailUser,isUserLogin, isNotActive, isFinishCalculation, isUserManager,isFirstTimeStarted, dtPassChanged, dtUserLogin,
                                   dtUserLogout,numDaysPassValid,numDaysStartWarn,lngUser,isAccountUser, isDontSeeMedVol,isAccountManager)
                                    VALUES(@username,@password,@nameUser,@idRole,@idEmployee,@idCompany,@emailUser,@isUserLogin,@isNotActive, @isFinishCalculation, @isUserManager, @isFirstTimeStarted, @dtPassChanged,
                                           @dtLogOnTime, @dtLogOffTime, @numDaysPassValid, @numDaysStartWarn,@lngUser,@isAccountUser, @isDontSeeMedVol,@isAccountManager)");



            SqlParameter[] sqlParameter = new SqlParameter[21];

            sqlParameter[0] = new SqlParameter("@username", SqlDbType.NVarChar);
            sqlParameter[0].Value = Convert.ToString(username.Trim());

            sqlParameter[1] = new SqlParameter("@password", SqlDbType.NVarChar);
            sqlParameter[1].Value = Convert.ToString(password.Trim());

            sqlParameter[2] = new SqlParameter("@nameUser", SqlDbType.NVarChar);
            sqlParameter[2].Value = nameUser;

            sqlParameter[3] = new SqlParameter("@idRole", SqlDbType.NVarChar);
            sqlParameter[3].Value = idRole;

            sqlParameter[4] = new SqlParameter("@idEmployee", SqlDbType.Int);
            sqlParameter[4].Value = (idEmployee == -1 || idEmployee == null) ? SqlInt32.Null : idEmployee;

            sqlParameter[5] = new SqlParameter("@idCompany", SqlDbType.Int);
            sqlParameter[5].Value = (idCompany == -1 || idCompany == null) ? SqlInt32.Null : idCompany;

            sqlParameter[6] = new SqlParameter("@emailUser", SqlDbType.NVarChar);
            sqlParameter[6].Value = Convert.ToString(emailUser.Trim());

            sqlParameter[7] = new SqlParameter("@isUserLogin", SqlDbType.Bit);
            sqlParameter[7].Value = isUserLogin;

            sqlParameter[8] = new SqlParameter("@isNotActive", SqlDbType.Bit);
            sqlParameter[8].Value = isNotActive;

            sqlParameter[9] = new SqlParameter("@isFinishCalculation", SqlDbType.Bit);
            sqlParameter[9].Value = isFinishCalculation;

            sqlParameter[10] = new SqlParameter("@isUserManager", SqlDbType.Bit);
            sqlParameter[10].Value = isUserManager;

            sqlParameter[11] = new SqlParameter("@isFirstTimeStarted", SqlDbType.Bit);
            sqlParameter[11].Value = isFirstTimeStarted;

            sqlParameter[12] = new SqlParameter("@dtPassChanged", SqlDbType.DateTime);
            sqlParameter[12].Value = (dtPassChanged == null || dtPassChanged == DateTime.MinValue) ? SqlDateTime.Null : dtPassChanged;

            sqlParameter[13] = new SqlParameter("@dtLogOnTime", SqlDbType.DateTime);
            sqlParameter[13].Value = (dtLogOnTime == null || dtLogOnTime == DateTime.MinValue) ? SqlDateTime.Null : dtLogOnTime;

            sqlParameter[14] = new SqlParameter("@dtLogOffTime", SqlDbType.DateTime);
            sqlParameter[14].Value = (dtLogOffTime == null || dtLogOffTime == DateTime.MinValue) ? SqlDateTime.Null : dtLogOffTime;

            sqlParameter[15] = new SqlParameter("@numDaysPassValid", SqlDbType.Decimal);
            sqlParameter[15].Value = (numDaysPassValid == -1 || numDaysPassValid == null) ? SqlDecimal.Null : numDaysPassValid;

            sqlParameter[16] = new SqlParameter("@numDaysStartWarn", SqlDbType.Decimal);
            sqlParameter[16].Value = (numDaysStartWarn == -1 || numDaysStartWarn == null) ? SqlDecimal.Null : numDaysStartWarn;

            sqlParameter[17] = new SqlParameter("@lngUser", SqlDbType.Char);
            sqlParameter[17].Value = Convert.ToString(lngUser.Trim());

            sqlParameter[18] = new SqlParameter("@isAccountUser", SqlDbType.Bit);
            sqlParameter[18].Value = isAccountUser;

            sqlParameter[19] = new SqlParameter("@isDontSeeMedVol", SqlDbType.Bit);
            sqlParameter[19].Value = isDontSeeMedVol;

            sqlParameter[20] = new SqlParameter("@isAccountManager", SqlDbType.Bit);
            sqlParameter[20].Value = isAccountManager;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");

            sqlParameter = new SqlParameter[8];


            sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameForm;

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = idUser;

            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "I";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = conn.GetLastTableID("Users") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idUser";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Users";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Users insert";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public bool DeleteMenuUser(int idMenu, int idUser, int idSecurity, string nameForm, int idUser1)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  MenuUserSecurity
     WHERE idUser = @idUser AND  idMenu=@idMenu AND idSecurity=@idSecurity");

            SqlParameter[] sqlParameter = new SqlParameter[3];

            sqlParameter[0] = new SqlParameter("@idMenu", SqlDbType.Int);
            sqlParameter[0].Value = idMenu;

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = idUser;

            sqlParameter[2] = new SqlParameter("@idSecurity", SqlDbType.Int);
            sqlParameter[2].Value = idSecurity;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");

            sqlParameter = new SqlParameter[8];


            sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameForm;

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = idUser1;

            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "D";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = idUser.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idUser";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MenuUserSecurity";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete menu user";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public DataTable GetAllSecurity()
        {

            string query = string.Format(@"SELECT idSecurity,nameSecurity FROM Security ");

            return conn.executeSelectQuery(query, null);

        }

        public bool IdRoleUpdate(int idUser, int idRole, string nameForm, int idUser1)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();


            string query = string.Format(@"UPDATE 
                                   Users SET idRole=@idRole
                                    WHERE idUser=@idUser");

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[0].Value = idUser;

            sqlParameter[1] = new SqlParameter("@idRole", SqlDbType.Int);
            sqlParameter[1].Value = idRole;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");

            sqlParameter = new SqlParameter[8];


            sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameForm;

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = idUser1;

            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "U";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = idUser.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idUser";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Users";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Id role update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public bool MenuUserUpdate(int idMenu, int idUser, int idSecurity, string nameForm, int idUser1)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE MenuUserSecurity 
                SET    idSecurity=@idSecurity
                 WHERE idMenu = @idMenu AND idUser=@idUser");

            SqlParameter[] sqlParameter = new SqlParameter[3];

            sqlParameter[0] = new SqlParameter("idMenu", SqlDbType.Int);
            sqlParameter[0].Value = idMenu;

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = idUser;

            sqlParameter[2] = new SqlParameter("idSecurity", SqlDbType.Int);
            sqlParameter[2].Value = idSecurity;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");

            sqlParameter = new SqlParameter[8];


            sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameForm;

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = idUser1;

            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "U";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = idMenu.ToString() + "_" + idUser.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idMenu _ idUser";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MenuUserSecurity";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Menu user update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertMenuSecurity(int idMenu, int idSecurity, int idUser, string nameForm, int idUser1)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO 
                    MenuUserSecurity (idMenu,idUser,idSecurity) 
                    VALUES (@idMenu, @idUser,@idSecurity)");

            SqlParameter[] sqlParameter = new SqlParameter[3];

            sqlParameter[0] = new SqlParameter("@idMenu", SqlDbType.Int);
            sqlParameter[0].Value = (idMenu);

            sqlParameter[2] = new SqlParameter("@idSecurity", SqlDbType.Int);
            sqlParameter[2].Value = (idSecurity);

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = (idUser);

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");

            sqlParameter = new SqlParameter[8];


            sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameForm;

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = idUser1;

            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "I";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = idMenu.ToString() + "_" + idUser.ToString() + "_" + idSecurity.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idMenu _ idUser_idSecurity";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MenuUserSecurity";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert menu security";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public DataTable IsInRoleSecurity(string language, int idUser)
        {
            string query = string.Format(@"SELECT DISTINCT mrs.idMenu,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,
                                           CASE WHEN SB.stringKey IS NOT NULL THEN SB.stringValue ELSE 'Role' END [By],mrs.idSecurity, 
                                           CASE WHEN SNS.stringKey IS NOT NULL THEN SNS.stringValue ELSE se.nameSecurity END nameSecurity
                                         FROM Users u 
                                         LEFT OUTER JOIN MenuRoleSecurity mrs ON mrs.idRole = u.idRole
                                         LEFT OUTER JOIN Menu m ON m.idMenu = mrs.idMenu
                                         LEFT OUTER JOIN Security se ON mrs.idSecurity = se.idSecurity
                                         LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM( m.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @" SB on RTRIM(LTRIM('Role')) = RTRIM(LTRIM(SB.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @"  SNS on RTRIM(LTRIM(se.nameSecurity)) = RTRIM(LTRIM(SNS.stringKey))
                                         WHERE idUser=@idUser AND mrs.idMenu NOT IN (SELECT DISTINCT idMenu FROM MenuUserSecurity WHERE idUser=@idUser)
                                        ");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@language", SqlDbType.NVarChar);
            sqlParameters[0].Value = Convert.ToString(language.Trim());

            sqlParameters[1] = new SqlParameter("@idUser", SqlDbType.NVarChar);
            sqlParameters[1].Value = idUser;

            return conn.executeSelectQuery(query, sqlParameters);
        }

       

        public DataTable Translate(string stringKey, string language)
        {
            string query = string.Format(@"SELECT CASE WHEN stringValue IS NOT NULL THEN stringValue ELSE stringKey END stringValue
                FROM  STRING" + language + @" 
                WHERE stringKey=@stringKey");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@stringKey", SqlDbType.NVarChar);
            sqlParameters[0].Value = Convert.ToString(stringKey.Trim());

            sqlParameters[1] = new SqlParameter("@language", SqlDbType.NVarChar);
            sqlParameters[1].Value = language;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAllLanguages()
        {
            string query = string.Format(@" SELECT  idLang, nameLang FROM Languages");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable MenuUserSecurity(string language, int idUser)
        {
            string query = string.Format(@"SELECT mrs.idMenu,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,CASE WHEN SB.stringKey IS NOT NULL THEN SB.stringValue ELSE 'User' END [By],mrs.idSecurity, CASE WHEN SNS.stringKey IS NOT NULL THEN SNS.stringValue ELSE se.nameSecurity END nameSecurity
                                         FROM Users u 
                                         LEFT OUTER JOIN MenuUserSecurity mrs ON mrs.idUser = u.idUser
                                         LEFT OUTER JOIN Menu m ON m.idMenu = mrs.idMenu
                                         LEFT OUTER JOIN Security se ON mrs.idSecurity = se.idSecurity
                                         LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM( m.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @"  SB on RTRIM(LTRIM('User')) = RTRIM(LTRIM(SB.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @"  SNS on RTRIM(LTRIM(se.nameSecurity)) = RTRIM(LTRIM(SNS.stringKey))
                                         WHERE mrs.idUser=@idUser AND m.idMenuSuperior IS NULL");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@language", SqlDbType.NVarChar);
            sqlParameters[0].Value = Convert.ToString(language.Trim());

            sqlParameters[1] = new SqlParameter("@idUser", SqlDbType.NVarChar);
            sqlParameters[1].Value = idUser;

            return conn.executeSelectQuery(query, sqlParameters);
        }


        public DataTable MenuRoleSecurity(string language, int idUser)
        {
            string query = string.Format(@"SELECT mrs.idMenu,m.idMenuSuperior,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,
                                           CASE WHEN SB.stringKey IS NOT NULL THEN SB.stringValue ELSE 'Role' END [By],mrs.idSecurity, 
                                           CASE WHEN SNS.stringKey IS NOT NULL THEN SNS.stringValue ELSE se.nameSecurity END nameSecurity
                                         FROM Users u 
                                         LEFT OUTER JOIN MenuRoleSecurity mrs ON mrs.idRole = u.idRole
                                         LEFT OUTER JOIN Menu m ON m.idMenu = mrs.idMenu
                                         LEFT OUTER JOIN Security se ON mrs.idSecurity = se.idSecurity
                                         LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM( m.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @" SB on RTRIM(LTRIM('Role')) = RTRIM(LTRIM(SB.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @"  SNS on RTRIM(LTRIM(se.nameSecurity)) = RTRIM(LTRIM(SNS.stringKey))
                                         WHERE idUser=@idUser AND mrs.idMenu NOT IN (SELECT DISTINCT idMenu FROM MenuUserSecurity WHERE idUser=@idUser) AND m.idMenuSuperior IS NULL ");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@language", SqlDbType.NVarChar);
            sqlParameters[0].Value = Convert.ToString(language.Trim());

            sqlParameters[1] = new SqlParameter("@idUser", SqlDbType.NVarChar);
            sqlParameters[1].Value = idUser;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetSubMenus(string language, int idRole)
        {
            string query = string.Format(@"SELECT DISTINCT mrs.idMenu,m.idMenuSuperior,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,mrs.idSecurity, 
                                           CASE WHEN SNS.stringKey IS NOT NULL THEN SNS.stringValue ELSE se.nameSecurity END nameSecurity
                                         FROM MenuRoleSecurity mrs 
                                         LEFT OUTER JOIN Menu m ON m.idMenu = mrs.idMenu
                                         LEFT OUTER JOIN Security se ON mrs.idSecurity = se.idSecurity
                                         LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM( m.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @"  SNS on RTRIM(LTRIM(se.nameSecurity)) = RTRIM(LTRIM(SNS.stringKey))
                                         WHERE idRole= @idRole AND m.idMenuSuperior IS NOT NULL");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idRole", SqlDbType.Int);
            sqlParameters[0].Value = idRole;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetSubMenusUser(string language, int idUser)
        {
            string query = string.Format(@"SELECT DISTINCT mrs.idMenu,m.idMenuSuperior,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,mrs.idSecurity, 
                                           CASE WHEN SNS.stringKey IS NOT NULL THEN SNS.stringValue ELSE se.nameSecurity END nameSecurity
                                         FROM MenuUserSecurity mrs 
                                         LEFT OUTER JOIN Menu m ON m.idMenu = mrs.idMenu
                                         LEFT OUTER JOIN Security se ON mrs.idSecurity = se.idSecurity
                                         LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM( m.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @"  SNS on RTRIM(LTRIM(se.nameSecurity)) = RTRIM(LTRIM(SNS.stringKey))
                                         
                                         WHERE idUser= @idUser AND m.idMenuSuperior IS NOT NULL");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameters[0].Value = idUser;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetSubMenusForSuperiorForRole(string language, int idUser, int idMenuSuperior)
        {
            string query = string.Format(@"SELECT DISTINCT mrs.idMenu,m.idMenuSuperior,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,mrs.idSecurity, 
                                           CASE WHEN SNS.stringKey IS NOT NULL THEN SNS.stringValue ELSE se.nameSecurity END nameSecurity
                                         FROM MenuUserSecurity mrs 
                                         LEFT OUTER JOIN Menu m ON m.idMenu = mrs.idMenu
                                         LEFT OUTER JOIN Security se ON mrs.idSecurity = se.idSecurity
                                         LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM( m.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @"  SNS on RTRIM(LTRIM(se.nameSecurity)) = RTRIM(LTRIM(SNS.stringKey))
                                         WHERE idUser= @idUser AND m.idMenuSuperior = '" + idMenuSuperior + @"'");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameters[0].Value = idUser;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetSubMenusForSuperior(string language, int idUser, int idMenuSuperior)
        {
            string query = string.Format(@"SELECT DISTINCT mrs.idMenu,m.idMenuSuperior,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,
                                           mrs.idSecurity, 
                                           CASE WHEN SNS.stringKey IS NOT NULL THEN SNS.stringValue ELSE se.nameSecurity END nameSecurity
                                         FROM MenuUserSecurity mrs 
                                         LEFT OUTER JOIN Menu m ON m.idMenu = mrs.idMenu
                                         LEFT OUTER JOIN Security se ON mrs.idSecurity = se.idSecurity
                                         LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM( m.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @"  SNS on RTRIM(LTRIM(se.nameSecurity)) = RTRIM(LTRIM(SNS.stringKey))
                                         WHERE idUser= @idUser AND m.idMenuSuperior = '" + idMenuSuperior + @"' 
                                         UNION
                                         SELECT DISTINCT m.idMenu,m.idMenuSuperior,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,'1' as idSecurity, 
                                         NULL AS nameSecurity
                                         FROM Menu m 
                                         LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM( m.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                                         WHERE  m.idMenuSuperior ='" + idMenuSuperior + @"' AND m.idMenu NOT IN (SELECT DISTINCT mrs.idMenu
                                         FROM MenuUserSecurity mrs 
                                         LEFT OUTER JOIN Menu m ON m.idMenu = mrs.idMenu
                                         WHERE idUser= @idUser AND m.idMenuSuperior ='" + idMenuSuperior + @"')");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameters[0].Value = idUser;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public bool Insert(int idMenu, int idUser, int idSecurity, string nameForm, int idUser1)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO MenuUserSecurity (idMenu,idUser,idSecurity) 
                                            VALUES (@idMenu,@idUser,@idSecurity)");


            SqlParameter[] sqlParameter = new SqlParameter[3];



            sqlParameter[0] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[0].Value = idUser;

            sqlParameter[1] = new SqlParameter("@idMenu", SqlDbType.Int);
            sqlParameter[1].Value = idMenu;

            sqlParameter[2] = new SqlParameter("@idSecurity", SqlDbType.Int);
            sqlParameter[2].Value = idSecurity;


            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");

            sqlParameter = new SqlParameter[8];


            sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameForm;

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = idUser1;

            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "I";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = idMenu.ToString() + "_" + idUser.ToString() + "_" + idSecurity.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idMenu _ idUser_idSecurity";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MenuUserSecurity";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool Delete(int idMenu, int idUser, string nameForm, int idUser1)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM MenuUserSecurity WHERE 
                                          (idMenu IN (SELECT DISTINCT idMenu FROM Menu WHERE idMenuSuperior = @idMenu) and idUser = @idUser)
                                          OR idMenu IN (SELECT DISTINCT m.idMenu FROM Menu m LEFT OUTER JOIN MenuUserSecurity mrs ON m.idMenu = mrs.idMenu 
                                          WHERE idMenuSuperior IS NOT NULL AND mrs.idUser = @idUser
                                          AND m.idMenuSuperior NOT IN (SELECT DISTINCT idMenu FROM MenuUserSecurity WHERE idUser = @idUser))");


            SqlParameter[] sqlParameter = new SqlParameter[2];



            sqlParameter[0] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[0].Value = idUser;

            sqlParameter[1] = new SqlParameter("@idMenu", SqlDbType.Int);
            sqlParameter[1].Value = idMenu;


            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");

            sqlParameter = new SqlParameter[8];


            sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameForm;

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = idUser1;

            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "D";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = idUser.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idUser";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MenuUserSecurity";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteMainMenus(int idUser, string nameForm, int idUser1)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM MenuUserSecurity WHERE 
                                          (idMenu IN (SELECT DISTINCT idMenu FROM Menu WHERE idMenuSuperior IS NULL) and idUser = @idUser)
                                          OR idMenu IN (SELECT DISTINCT m.idMenu FROM Menu m LEFT OUTER JOIN MenuUserSecurity mrs ON m.idMenu = mrs.idMenu
                                          WHERE idMenuSuperior IS NOT NULL AND mrs.idUser = @idUser
                                          AND m.idMenuSuperior NOT IN (SELECT DISTINCT idMenu FROM MenuUserSecurity WHERE idUser = @idUser))");


            SqlParameter[] sqlParameter = new SqlParameter[1];



            sqlParameter[0] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[0].Value = idUser;


            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");

            sqlParameter = new SqlParameter[8];


            sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameForm;

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = idUser1;

            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "D";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = idUser.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idUser";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MenuUserSecurity";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete main menus";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean DeleteUserSript(int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  Users WHERE idUser = @idUser");

            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[0].Value = idUser;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public bool logInLogOut(DateTime dt, int idUser, bool isUserLogin)
        {
          

            string din;
            if (isUserLogin)
            {
                din = "dtUserLogin";
            }
            else
            {
                din = "dtUserLogout";
            }
            string query = string.Format(@"UPDATE Users
                                           SET " + din + @" = @dt, isUserLogin = @isUserLogin
                                           WHERE idUser = @idUser");


            SqlParameter[] sqlParameters = new SqlParameter[3];

            sqlParameters[0] = new SqlParameter("@dt", SqlDbType.DateTime);
            sqlParameters[0].Value = dt;

            sqlParameters[1] = new SqlParameter("@isUserLogin", SqlDbType.Bit);
            sqlParameters[1].Value = isUserLogin;

            sqlParameters[2] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameters[2].Value = idUser;

            return conn.executeUpdateQuery(query, sqlParameters);

        }
        public DataTable checkUser(string ip)
        {
            string query = "select min(DATEDIFF(minute, LastloginDateTime,GETDATE())) as time  from LoginCheck where IpAddress ='"+ip+"' and DATEDIFF(minute, LastloginDateTime,GETDATE())<30";

            return conn.executeSelectQuery(query, null);
        }
        public bool insertUsersIp(string ip)
        {
            string query = string.Format(@"INSERT INTO LoginCheck(LastloginDateTime,IpAddress)
                                    VALUES(@LastloginDateTime,@IpAddress)");
            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@LastloginDateTime", SqlDbType.DateTime);
            sqlParameters[0].Value = DateTime.Now;

            sqlParameters[1] = new SqlParameter("@IpAddress", SqlDbType.NVarChar);
            sqlParameters[1].Value = ip;
            return conn.executeInsertQuery(query, sqlParameters);
        }
    }    
}
