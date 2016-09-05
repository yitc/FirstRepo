using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;
using BIS.Model;

namespace BIS.DAO
{
    public class BookmarksDAO
    {
        private dbConnection conn;

        public BookmarksDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetBookmarks()
        {
            string query = string.Format(@"
            SELECT idField, idBookmark, nameBookmark, tableName, fieldName, fieldBookmark,fieldValue, dtCreated, dtModified, userModifiel, um.nameuser as nameUserModified, userCreated,um.nameuser as nameUserCreated
            from Bookmarks b
            LEFT OUTER JOIN Users um ON um.idUser = b.userModifiel
            LEFT OUTER JOIN Users uc ON uc.idUser = b.userCreated ");

            return conn.executeSelectQuery(query, null);
        }

       
        public DataTable GetBookmarksById(int id)
        {
            string query = string.Format(@"
            SELECT idField, idBookmark, nameBookmark, tableName, fieldName, fieldBookmark,fieldValue, dtCreated, dtModified, userModifiel, um.nameuser as nameUserModified, userCreated,um.nameuser as nameUserCreated
            from Bookmarks b LEFT OUTER JOIN Users um ON um.idUser = b.userModifiel
            LEFT OUTER JOIN Users uc ON uc.idUser = b.userCreated WHERE id = @id");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@id", SqlDbType.Int);
            sqlParameters[0].Value = id;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetBookmarksByBookmarkId(Guid? idBookmark)
        {
            string query = string.Format(@"
            SELECT idField, idBookmark, nameBookmark, tableName, fieldName, fieldBookmark, fieldValue, dtCreated, dtModified, userModifiel, um.nameuser as nameUserModified, userCreated,um.nameuser as nameUserCreated
            from Bookmarks b LEFT OUTER JOIN Users um ON um.idUser = b.userModifiel
            LEFT OUTER JOIN Users uc ON uc.idUser = b.userCreated WHERE idBookmark = @idBookmark");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idBookmark", SqlDbType.UniqueIdentifier);
            sqlParameters[0].Value = idBookmark;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable SelectAditionalValues(string tableName)
        {
            string query = string.Format(@"
            SELECT id" +tableName+ "Type,name"+tableName + "Type" +
                 " FROM Types" +tableName+"");

            return conn.executeSelectQuery(query, null);
        }
//        public object CustomPersonQuery(string tableName, string fieldName, int id, string idFieldName)
//        {
//            string query = string.Format(@"
//            SELECT " + fieldName + " FROM " + tableName + " WHERE " + idFieldName + " = '" + id  +"'" );

//            return conn.executeScalarQuery(query, null);
//        }

        public object CustomPersonQuery(string tableName, string fieldName, int id, string idFieldName)
        {
            string query = string.Format(@"
            SELECT " + fieldName + @" FROM (SELECT c.*,g.nameGender,t.nameTitle
                FROM " + tableName + @" c
                LEFT JOIN Gender g ON g.idGender = c.idGender
                LEFT JOIN Title t ON t.idTitle = c.idTitle) AS ContactPerson WHERE " + idFieldName + " = '" + id +"'");

            return conn.executeScalarQuery(query, null);
        }

        public object CustomClientQuery(string tableName, string fieldName, int id, string idFieldName)
        {
            string query = string.Format(@"
            SELECT " + fieldName + @" FROM (SELECT c.*,g.nameCountry,t.nameTypeClient 
                FROM " + tableName + @" c
                LEFT JOIN Country g ON g.idCountry = c.countryClient
                LEFT JOIN ClientTypes t ON t.idTypeClient = c.idTypeClient) AS Client WHERE " + idFieldName + " = '" + id + "'");

            return conn.executeScalarQuery(query, null);
        }

        public void SaveBookmars(List<BookmarksModel> arrayToSave)
        {

            if(arrayToSave != null)
            {
                foreach(BookmarksModel m in arrayToSave)
                {
                    string query = string.Format(@"INSERT INTO  Bookmarks (idBookmark, nameBookmark, tableName, fieldName, fieldBookmark,fieldValue, dtCreated, dtModified, userModifiel, userCreated )
                        VALUES (@idBookmark, @nameBookmark, @tableName, @fieldName, @fieldBookmark, @fieldValue, @dtCreated, @dtModified, @userModifiel, @userCreated )");

                    SqlParameter[] sqlParameters = new SqlParameter[10];

                    sqlParameters[0] = new SqlParameter("@idBookmark", SqlDbType.UniqueIdentifier);
                    sqlParameters[0].Value = m.idBookmark;

                    sqlParameters[1] = new SqlParameter("@nameBookmark", SqlDbType.NVarChar);
                    sqlParameters[1].Value = m.nameBookmark;

                    sqlParameters[2] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                    sqlParameters[2].Value = m.tableName;

                    sqlParameters[3] = new SqlParameter("@fieldName", SqlDbType.NVarChar);
                    sqlParameters[3].Value = m.fieldName;

                    sqlParameters[4] = new SqlParameter("@fieldBookmark", SqlDbType.NVarChar);
                    sqlParameters[4].Value = m.fieldBookmark;

                    sqlParameters[5] = new SqlParameter("@fieldValue", SqlDbType.NVarChar);
                    sqlParameters[5].Value = m.fieldValue;

                    sqlParameters[6] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
                    sqlParameters[6].Value = m.dtCreated;

                    sqlParameters[7] = new SqlParameter("@dtModified", SqlDbType.DateTime);
                    sqlParameters[7].Value = m.dtModified;

                    sqlParameters[8] = new SqlParameter("@userModifiel", SqlDbType.Int);
                    sqlParameters[8].Value = m.userModifiel;

                    sqlParameters[9] = new SqlParameter("@userCreated", SqlDbType.Int);
                    sqlParameters[9].Value = m.userCreated;

                    conn.executeInsertQuery(query, sqlParameters);
                }
            }
        }

        public bool DeleteBookmarksByBookmarkID(Guid ID)
        {
            string query = string.Format(@"DELETE FROM Bookmarks WHERE idBookmark = @idBookmark");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idBookmark", SqlDbType.UniqueIdentifier);
            sqlParameters[0].Value = ID;

            return conn.executeDeleteQuery(query, sqlParameters);
        }
    }
}
