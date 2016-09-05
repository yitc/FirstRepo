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
    public class BookmarkDefDAO
    {
        private dbConnection conn;
        public BookmarkDefDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetBookmarks()
        {
            string query = string.Format(@"
            SELECT Id, tableDisplayName, tableName, fieldBookmark, nameBookmark, displayNameBookmark,isRelationBmk, relationTableName, relationFieldName, 
                hasType, typeTableName, typeFieldName, typeFieldValue, isActive, dtCreated, dtModified, userModified, userCreated 
            from BookmarkDef b ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetBookmarkByID(int id)
        {
            string query = string.Format(@"
            SELECT Id, tableDisplayName, tableName, fieldBookmark, nameBookmark, displayNameBookmark,isRelationBmk, relationTableName, relationFieldName, 
                hasType, typeTableName, typeFieldName, typeFieldValue, isActive, dtCreated, dtModified, userModified, userCreated 
            from BookmarkDef b WHERE Id = " + id + "");

            return conn.executeSelectQuery(query, null);

        }
        public DataTable GetBookmarksByTableName(string tablename)
        {
            string query = string.Format(@"
            SELECT Id, tableDisplayName, tableName, fieldBookmark, nameBookmark, displayNameBookmark,isRelationBmk, relationTableName, relationFieldName, 
                hasType, typeTableName, typeFieldName, typeFieldValue, isActive, dtCreated, dtModified, userModified, userCreated 
            from BookmarkDef b WHERE tableName = '" + tablename +"'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetBookmarksByNameBookmark(string namebookmark)
        {
            string query = string.Format(@"
            SELECT Id, tableDisplayName, tableName, fieldBookmark, nameBookmark, displayNameBookmark,isRelationBmk, relationTableName, relationFieldName, 
                hasType, typeTableName, typeFieldName, typeFieldValue, isActive, dtCreated, dtModified, userModified, userCreated 
            from BookmarkDef b WHERE nameBookmark = '" + namebookmark + "'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetBookmarksByDistinctTableName()
        {
            string query = string.Format(@"SELECT tableDisplayName, tableName from BookmarkDef WHERE isActive = '1' GROUP BY tableDisplayName, tablename");

            return conn.executeSelectQuery(query, null);
        }

        public object CustomBookmarksQuery(string tableName, string fieldName, string idFieldName, int value, string typeField, int? typevalue)
        {
            string query = "";
            if (typeField == null)
            {
                query = string.Format(@"
                    SELECT " + fieldName + @" FROM " + tableName + @" WHERE  " + idFieldName + " = '" + value + "'");
            }
            else
            {
                query = string.Format(@"
                    SELECT " + fieldName + @" FROM " + tableName + @" WHERE  " + idFieldName + " = '" + value + "' AND " + typeField + " = '" + typevalue + "'");
            }

            return conn.executeScalarQuery(query, null);
        }

        public object CustomBookmarksQueryForTypes(string tableName, string fieldName, string idFieldName, int value, string typeField, int? typevalue, string parentTableName, string parentID)
        {
            string query = "";
            
                query = string.Format(@"
                    SELECT " + fieldName + @" FROM " + tableName + @" WHERE  " + idFieldName + " =  (SELECT " + idFieldName  +" FROM " + parentTableName  + " WHERE  " + parentID + " = '" + value + "')");
            
            return conn.executeScalarQuery(query, null);
        }


        public DataTable GetTableBookmarks()
        {
            string query = string.Format(@"SELECT idTable, nameTable, displayNameTable, active, fontSizeFields,
                leftMargin, rightMargin FROM BookmarkTable");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetTableByName(string tablename)
        {
            string query = string.Format(@"SELECT idTable, nameTable, displayNameTable, active, fontSizeFields,
                leftMargin, rightMargin FROM BookmarkTable WHERE nameTable = '"+ tablename +"'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetTableBookmarksFields(int idtable)
        {
            string query = string.Format(@"SELECT nameField, widthField, displayNameField, visible, isTotal 
                FROM BookmarkTableFields WHERE visible = 'true' AND idTable = '" + idtable.ToString() + "' ORDER BY rbr_sort");

            return conn.executeSelectQuery(query, null);
        }
    }
}
