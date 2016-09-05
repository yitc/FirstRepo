using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;

namespace BIS.DAO
{
    public class MenuDAO
    {
        private dbConnection conn;

        public MenuDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetUserSecurityDetails(int userid, string language)
        {
//            string query = string.Format(
//                @"SELECT m.idMenu,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,m.lngCode, m.sortMenu, m.initMethod, mrs.idSecurity, f.nameForm,m.imageMenu,m.imageNew,m.imageDelete, m.onClickMenu FROM Users u
//                    INNER JOIN MenuRoleSecurity mrs ON mrs.idRole = u.idRole
//                    INNER JOIN Menu m on m.idMenu = mrs.idMenu
//                    LEFT OUTER JOIN Forms f ON f.idForm = m.idForm 
//                    LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM(M.nameMenu)) = RTRIM(LTRIM(S.stringKey))
//                    WHERE u.idUser = @userid
//                    ORDER BY m.sortMenu");

            string query = string.Format(
                @" SELECT idMenu,nameMenu,lngCode, sortMenu, initMethod, idSecurity, nameForm,imageMenu,imageNew,imageDelete, onClickMenu FROM 
                    (SELECT m.idMenu,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,m.lngCode, m.sortMenu, m.initMethod, mrs.idSecurity, f.nameForm,m.imageMenu,m.imageNew,m.imageDelete, m.onClickMenu FROM Users u
                    INNER JOIN MenuRoleSecurity mrs ON mrs.idRole = u.idRole
                    INNER JOIN Menu m on m.idMenu = mrs.idMenu
                    LEFT OUTER JOIN Forms f ON f.idForm = m.idForm 
                    LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM(M.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                    WHERE u.idUser =  @userid and mrs.idMenu not in (SELECT idMenu from  MenuUserSecurity WHERE idUser =  @userid) and m.idMenuSuperior is null
                    UNION
                    SELECT m.idMenu,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,m.lngCode, m.sortMenu, m.initMethod, mrs.idSecurity, f.nameForm,m.imageMenu,m.imageNew,m.imageDelete, m.onClickMenu FROM Users u
                    INNER JOIN MenuUserSecurity mrs ON mrs.idUser = u.idUser
                    INNER JOIN Menu m on m.idMenu = mrs.idMenu
                    LEFT OUTER JOIN Forms f ON f.idForm = m.idForm 
                    LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM(M.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                    WHERE u.idUser =  @userid and m.idMenuSuperior is null) M
                    ORDER BY sortMenu");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@userid", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(userid);            

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetMenus(int idMenu,int userid, string language)
        {
            string query = string.Format(
                @" SELECT idMenu,nameMenu,lngCode, sortMenu, initMethod, idSecurity, idForm, nameForm,imageMenu,imageNew,imageDelete, onClickMenu, isGrid  FROM 
                    (SELECT m.idMenu,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,m.lngCode, m.sortMenu, m.initMethod, mrs.idSecurity,m.idForm, f.nameForm,CASE WHEN m.imageMenu IS NULL THEN (SELECT mS.imageMenu FROM Menu mS WHERE mS.idMenu = m.idMenuSuperior) ELSE m.imageMenu END AS imageMenu,CASE WHEN m.imageNew IS NULL THEN (SELECT mS.imageNew FROM Menu mS WHERE mS.idMenu = m.idMenuSuperior) ELSE m.imageNew END AS imageNew,CASE WHEN m.imageDelete IS NULL THEN (SELECT mS.imageDelete FROM Menu mS WHERE mS.idMenu = m.idMenuSuperior) ELSE m.imageDelete END AS imageDelete, m.onClickMenu, m.isGrid FROM Users u 
                    INNER JOIN MenuRoleSecurity mrs ON mrs.idRole = u.idRole
                    INNER JOIN Menu m on m.idMenu = mrs.idMenu
                    LEFT OUTER JOIN Forms f ON f.idForm = m.idForm 
                    LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM(M.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                    WHERE u.idUser =   @userid and mrs.idMenu not in (SELECT idMenu from  MenuUserSecurity WHERE idUser =  @userid) and m.idMenuSuperior = " + idMenu + @"
                    UNION
                    SELECT m.idMenu,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,m.lngCode, m.sortMenu, m.initMethod, mrs.idSecurity,m.idForm, f.nameForm,CASE WHEN m.imageMenu IS NULL THEN (SELECT mS.imageMenu FROM Menu mS WHERE mS.idMenu = m.idMenuSuperior) ELSE m.imageMenu END AS imageMenu,CASE WHEN m.imageNew IS NULL THEN (SELECT mS.imageNew FROM Menu mS WHERE mS.idMenu = m.idMenuSuperior) ELSE m.imageNew END AS imageNew,CASE WHEN m.imageDelete IS NULL THEN (SELECT mS.imageDelete FROM Menu mS WHERE mS.idMenu = m.idMenuSuperior) ELSE m.imageDelete END AS imageDelete, m.onClickMenu, m.isGrid FROM Users u 
                    INNER JOIN MenuUserSecurity mrs ON mrs.idUser = u.idUser
                    INNER JOIN Menu m on m.idMenu = mrs.idMenu
                    LEFT OUTER JOIN Forms f ON f.idForm = m.idForm 
                    LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM(M.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                    WHERE u.idUser =   @userid and m.idMenuSuperior = " + idMenu + @") M
                    ORDER BY sortMenu");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@userid", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(userid);
            return conn.executeSelectQuery(query, sqlParameters);
        }

         public DataTable GetMenuByName(string nameMenu,int userid, string language)
        {
            string query = string.Format(
                @"  SELECT idMenu,nameMenu,lngCode, sortMenu, initMethod, idSecurity, idForm, nameForm,imageMenu,imageNew,imageDelete, onClickMenu FROM 
                    (SELECT m.idMenu,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,m.lngCode, m.sortMenu, m.initMethod, mrs.idSecurity,m.idForm, f.nameForm,CASE WHEN m.imageMenu IS NULL THEN (SELECT mS.imageMenu FROM Menu mS WHERE mS.idMenu = m.idMenuSuperior) ELSE m.imageMenu END AS imageMenu,CASE WHEN m.imageNew IS NULL THEN (SELECT mS.imageNew FROM Menu mS WHERE mS.idMenu = m.idMenuSuperior) ELSE m.imageNew END AS imageNew,CASE WHEN m.imageDelete IS NULL THEN (SELECT mS.imageDelete FROM Menu mS WHERE mS.idMenu = m.idMenuSuperior) ELSE m.imageDelete END AS imageDelete, m.onClickMenu FROM Users u
                    INNER JOIN MenuRoleSecurity mrs ON mrs.idRole = u.idRole
                    INNER JOIN Menu m on m.idMenu = mrs.idMenu
                    LEFT OUTER JOIN Forms f ON f.idForm = m.idForm 
                    LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM(M.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                    WHERE u.idUser = @userid and mrs.idMenu not in (SELECT idMenu from  MenuUserSecurity WHERE idUser =  @userid) and m.nameMenu = '" + nameMenu + @"'
                    UNION
                    SELECT m.idMenu,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,m.lngCode, m.sortMenu, m.initMethod, mrs.idSecurity,m.idForm, f.nameForm,CASE WHEN m.imageMenu IS NULL THEN (SELECT mS.imageMenu FROM Menu mS WHERE mS.idMenu = m.idMenuSuperior) ELSE m.imageMenu END AS imageMenu,CASE WHEN m.imageNew IS NULL THEN (SELECT mS.imageNew FROM Menu mS WHERE mS.idMenu = m.idMenuSuperior) ELSE m.imageNew END AS imageNew,CASE WHEN m.imageDelete IS NULL THEN (SELECT mS.imageDelete FROM Menu mS WHERE mS.idMenu = m.idMenuSuperior) ELSE m.imageDelete END AS imageDelete, m.onClickMenu FROM Users u
                    INNER JOIN MenuUserSecurity mrs ON mrs.idUser = u.idUser
                    INNER JOIN Menu m on m.idMenu = mrs.idMenu
                    LEFT OUTER JOIN Forms f ON f.idForm = m.idForm 
                    LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM(M.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                    WHERE u.idUser = @userid and m.nameMenu =  '" + nameMenu + @"') M
                    ORDER BY sortMenu");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@userid", SqlDbType.Int);
            sqlParameters[0].Value = Convert.ToString(userid);
            return conn.executeSelectQuery(query, sqlParameters);
        }

         public DataTable GetMenusForGrid(string language)
         {
             string query = string.Format(@"SELECT DISTINCT mrs.idMenu,m.idMenuSuperior,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,mrs.idSecurity, 
                                           CASE WHEN SNS.stringKey IS NOT NULL THEN SNS.stringValue ELSE se.nameSecurity END nameSecurity
                                         FROM MenuRoleSecurity mrs 
                                         LEFT OUTER JOIN Menu m ON m.idMenu = mrs.idMenu
                                         LEFT OUTER JOIN Security se ON mrs.idSecurity = se.idSecurity
                                         LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM( m.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @"  SNS on RTRIM(LTRIM(se.nameSecurity)) = RTRIM(LTRIM(SNS.stringKey))
                                         WHERE  m.idMenuSuperior IS NULL");

             SqlParameter[] sqlParameters = new SqlParameter[0];

             return conn.executeSelectQuery(query, sqlParameters);
         }

         public DataTable GetSubMenusForGrid(string language)
         {
             string query = string.Format(@"SELECT DISTINCT mrs.idMenu,m.idMenuSuperior,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE m.nameMenu END nameMenu,mrs.idSecurity, 
                                           CASE WHEN SNS.stringKey IS NOT NULL THEN SNS.stringValue ELSE se.nameSecurity END nameSecurity
                                         FROM MenuRoleSecurity mrs 
                                         LEFT OUTER JOIN Menu m ON m.idMenu = mrs.idMenu
                                         LEFT OUTER JOIN Security se ON mrs.idSecurity = se.idSecurity
                                         LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM( m.nameMenu)) = RTRIM(LTRIM(S.stringKey))
                                         LEFT OUTER JOIN STRING" + language + @"  SNS on RTRIM(LTRIM(se.nameSecurity)) = RTRIM(LTRIM(SNS.stringKey))
                                         WHERE m.idMenuSuperior IS NOT NULL");

             SqlParameter[] sqlParameters = new SqlParameter[0];

             return conn.executeSelectQuery(query, sqlParameters);
         }

         public DataTable GetMenuIsGrid(int idMenu)
         {
             string query = string.Format(
                 @" SELECT isGrid FROM Menu
                    WHERE idMenu = '" + idMenu + "'");

             SqlParameter[] sqlParameters = new SqlParameter[0];
             return conn.executeSelectQuery(query, null);
         }
    }    
}
