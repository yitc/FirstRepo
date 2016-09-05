using BIS.Core;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.DAO
{
 public  class RoleDAO
    {
   
        private dbConnection conn;

        public RoleDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllRole()
        {
            string query = string.Format(
                @"SELECT idRole,nameRole FROM Roles");
         
            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetMenus(string language,int idRole)
        {
            string query = string.Format(@"SELECT DISTINCT mrs.idMenu,m.idMenuSuperior,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,mrs.idSecurity, 
                                           CASE WHEN SNS.stringKey IS NOT NULL THEN SNS.stringValue ELSE se.nameSecurity END nameSecurity
                                         FROM MenuRoleSecurity mrs 
                                         LEFT OUTER JOIN Menu m ON m.idMenu = mrs.idMenu
                                         LEFT OUTER JOIN Security se ON mrs.idSecurity = se.idSecurity
                                         LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM( m.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @"  SNS on RTRIM(LTRIM(se.nameSecurity)) = RTRIM(LTRIM(SNS.stringKey))
                                         WHERE idRole= @idRole AND m.idMenuSuperior IS NULL");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idRole", SqlDbType.Int);
            sqlParameters[0].Value =idRole ;

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

        public DataTable GetSubMenusForSuperiorForRole(string language, int idRole, int idMenuSuperior)
        {
            string query = string.Format(@"SELECT DISTINCT mrs.idMenu,m.idMenuSuperior,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,mrs.idSecurity, 
                                           CASE WHEN SNS.stringKey IS NOT NULL THEN SNS.stringValue ELSE se.nameSecurity END nameSecurity
                                         FROM MenuRoleSecurity mrs 
                                         LEFT OUTER JOIN Menu m ON m.idMenu = mrs.idMenu
                                         LEFT OUTER JOIN Security se ON mrs.idSecurity = se.idSecurity
                                         LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM( m.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @"  SNS on RTRIM(LTRIM(se.nameSecurity)) = RTRIM(LTRIM(SNS.stringKey))
                                         WHERE idRole= @idRole AND m.idMenuSuperior = '"+idMenuSuperior+@"'");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idRole", SqlDbType.Int);
            sqlParameters[0].Value = idRole;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetMainMenusForRole(string language, int idRole)
        {
            string query = string.Format(@"SELECT DISTINCT mrs.idMenu,m.idMenuSuperior,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,mrs.idSecurity, 
                                           CASE WHEN SNS.stringKey IS NOT NULL THEN SNS.stringValue ELSE se.nameSecurity END nameSecurity
                                         FROM MenuRoleSecurity mrs 
                                         LEFT OUTER JOIN Menu m ON m.idMenu = mrs.idMenu
                                         LEFT OUTER JOIN Security se ON mrs.idSecurity = se.idSecurity
                                         LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM( m.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @"  SNS on RTRIM(LTRIM(se.nameSecurity)) = RTRIM(LTRIM(SNS.stringKey))
                                         WHERE idRole= @idRole AND m.idMenuSuperior IS NULL");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idRole", SqlDbType.Int);
            sqlParameters[0].Value = idRole;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetSubMenusForSuperior(string language,int idRole, int idMenuSuperior)
        {
            string query = string.Format(@"SELECT DISTINCT mrs.idMenu,m.idMenuSuperior,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,mrs.idSecurity, 
                                           CASE WHEN SNS.stringKey IS NOT NULL THEN SNS.stringValue ELSE se.nameSecurity END nameSecurity
                                         FROM MenuRoleSecurity mrs 
                                         LEFT OUTER JOIN Menu m ON m.idMenu = mrs.idMenu
                                         LEFT OUTER JOIN Security se ON mrs.idSecurity = se.idSecurity
                                         LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM( m.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @"  SNS on RTRIM(LTRIM(se.nameSecurity)) = RTRIM(LTRIM(SNS.stringKey))
                                         WHERE idRole= @idRole AND m.idMenuSuperior = '" + idMenuSuperior + @"' 
                                         UNION
                                         SELECT DISTINCT m.idMenu,m.idMenuSuperior,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,'1' as idSecurity, 
                                         CASE WHEN SNS.stringKey IS NOT NULL THEN SNS.stringValue ELSE se.nameSecurity END as nameSecurity
                                         FROM Menu m 
                                         LEFT OUTER JOIN Security se ON '1' = se.idSecurity
                                         LEFT OUTER JOIN STRING" + language + @"  SNS on RTRIM(LTRIM(se.nameSecurity)) = RTRIM(LTRIM(SNS.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM( m.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                                         WHERE  m.idMenuSuperior ='" + idMenuSuperior + @"' AND m.idMenu NOT IN (SELECT DISTINCT mrs.idMenu
                                         FROM MenuRoleSecurity mrs 
                                         LEFT OUTER JOIN Menu m ON m.idMenu = mrs.idMenu
                                         WHERE idRole= @idRole AND m.idMenuSuperior ='" + idMenuSuperior + @"')");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idRole", SqlDbType.Int);
            sqlParameters[0].Value = idRole;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetMainMenus(string language, int idRole)
        {
            string query = string.Format(@"SELECT DISTINCT mrs.idMenu,m.idMenuSuperior,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,mrs.idSecurity, 
                                           CASE WHEN SNS.stringKey IS NOT NULL THEN SNS.stringValue ELSE se.nameSecurity END nameSecurity
                                         FROM MenuRoleSecurity mrs 
                                         LEFT OUTER JOIN Menu m ON m.idMenu = mrs.idMenu
                                         LEFT OUTER JOIN Security se ON mrs.idSecurity = se.idSecurity
                                         LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM( m.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @"  SNS on RTRIM(LTRIM(se.nameSecurity)) = RTRIM(LTRIM(SNS.stringKey))
                                         WHERE idRole= @idRole AND m.idMenuSuperior IS NULL
                                         UNION
                                         SELECT DISTINCT m.idMenu,m.idMenuSuperior,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,'1' as idSecurity, 
                                         CASE WHEN SNS.stringKey IS NOT NULL THEN SNS.stringValue ELSE se.nameSecurity END as nameSecurity
                                         FROM Menu m 
                                         LEFT OUTER JOIN Security se ON '1' = se.idSecurity
                                         LEFT OUTER JOIN STRING" + language + @"  SNS on RTRIM(LTRIM(se.nameSecurity)) = RTRIM(LTRIM(SNS.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM( m.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                                         WHERE  m.idMenuSuperior IS NULL AND m.idMenu NOT IN (SELECT DISTINCT mrs.idMenu
                                         FROM MenuRoleSecurity mrs 
                                         LEFT OUTER JOIN Menu m ON m.idMenu = mrs.idMenu
                                         WHERE idRole= @idRole AND m.idMenuSuperior IS NULL)");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idRole", SqlDbType.Int);
            sqlParameters[0].Value = idRole;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public bool Delete(int idMenu, int idRole, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM MenuRoleSecurity WHERE 
                                          (idMenu IN (SELECT DISTINCT idMenu FROM Menu WHERE idMenuSuperior = @idMenu) and idRole = @idRole)
                                          OR idMenu IN (SELECT DISTINCT m.idMenu FROM Menu m LEFT OUTER JOIN MenuRoleSecurity mrs ON m.idMenu = mrs.idMenu 
                                          WHERE idMenuSuperior IS NOT NULL AND mrs.idRole = @idRole
                                          AND m.idMenuSuperior NOT IN (SELECT DISTINCT idMenu FROM MenuRoleSecurity WHERE idRole = @idRole))");


            SqlParameter[] sqlParameter = new SqlParameter[2];



            sqlParameter[0] = new SqlParameter("@idRole", SqlDbType.Int);
            sqlParameter[0].Value = idRole;

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
            sqlParameter[1].Value = idUser;

            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "D";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = idRole+ "_" +idMenu;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idRole_idMenu";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MenuRoleSecurity";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteMainMenus(int idRole, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM MenuRoleSecurity WHERE 
                                          (idMenu IN (SELECT DISTINCT idMenu FROM Menu WHERE idMenuSuperior IS NULL) and idRole = @idRole)
                                          OR idMenu IN (SELECT DISTINCT m.idMenu FROM Menu m LEFT OUTER JOIN MenuRoleSecurity mrs ON m.idMenu = mrs.idMenu
                                          WHERE idMenuSuperior IS NOT NULL AND mrs.idRole = @idRole
                                          AND m.idMenuSuperior NOT IN (SELECT DISTINCT idMenu FROM MenuRoleSecurity WHERE idRole = @idRole))");


            SqlParameter[] sqlParameter = new SqlParameter[1];



            sqlParameter[0] = new SqlParameter("@idRole", SqlDbType.Int);
            sqlParameter[0].Value = idRole;


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
            sqlParameter[3].Value = "D";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = idRole;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idRole";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MenuRoleSecurity";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete menu role security";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool Insert(int idMenu, int idSecurity, int idRole, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO MenuRoleSecurity (idMenu,idRole,idSecurity) 
                                            VALUES (@idMenu,@idRole,@idSecurity)");


            SqlParameter[] sqlParameter = new SqlParameter[3];



            sqlParameter[0] = new SqlParameter("@idRole", SqlDbType.Int);
            sqlParameter[0].Value = idRole;

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
            sqlParameter[1].Value = idUser;

            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "I";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = idRole + "_" + idMenu + "_" + idSecurity;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idRole_idMenu_idSecurity";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "MenuRoleSecurity";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool InsertRole(string nameRole, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO Roles (nameRole) 
                                            VALUES (@nameRole)");


            SqlParameter[] sqlParameter = new SqlParameter[1];



            sqlParameter[0] = new SqlParameter("@nameRole", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameRole;


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
            sqlParameter[4].Value = conn.GetLastTableID("Roles")+1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idRole_idMenu_idSecurity";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Roles";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert roles";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }


        public DataTable GetAllSecurity()
        {

            string query = string.Format(@"SELECT idSecurity,nameSecurity FROM Security ");

            return conn.executeSelectQuery(query, null);

        }
        public DataTable GetLastRoleID()
        {

            string query = string.Format(@"SELECT TOP 1 idRole FROM Roles ORDER BY idRole DESC ");

            return conn.executeSelectQuery(query, null);

        }

//        string query = string.Format(@"
//                SELECT * FROM MenuRoleSecurity 
//                WHERE idRole = '" + idRole + "'");

     // NOVO DELETE ROLA
        public DataTable checkIsInUsers(int idRole)
        {
            string query = string.Format(@"
                SELECT * FROM Users 
                WHERE idRole = '" + idRole + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable checkIsInMenuRoleSecurity(int idRole)
        {
            string query = string.Format(@"
                SELECT * 
              FROM MenuRoleSecurity 
                WHERE idRole = '" + idRole + "'");

            return conn.executeSelectQuery(query, null);
        }
        public Boolean DeleteRoleScript(int idRole, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM Roles WHERE idRole = '" + idRole + "' ");

            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idRole", SqlDbType.Int);
            sqlParameter[0].Value = idRole;

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
            sqlParameter[3].Value = "D";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = idRole;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idRole";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Roles";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete role script";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
       

    }
}
