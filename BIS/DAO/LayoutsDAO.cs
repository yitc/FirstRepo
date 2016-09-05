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
    public class LayoutsDAO
    {

        private dbConnection conn;

        public LayoutsDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllLayouts()
        {
            string query = string.Format(@"SELECT idLayout,nameLayout,typeDocument,languageLayout,fileLayout, bookmarks, templateTable, userCreated,dtCreted
                ,userModified,dtModified,um.nameuser as nameUserCreated, um.nameuser as nameUserModified
                FROM Layouts l
                LEFT OUTER JOIN Users um ON um.idUser = l.userModified
                LEFT OUTER JOIN Users uc ON uc.idUser = l.userCreated
                ORDER BY idLayout");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllLayoutsbyTemplateTable(string templatetable)
        {
            string query = string.Format(@"SELECT idLayout,nameLayout,typeDocument,languageLayout,fileLayout, bookmarks, templateTable, userCreated,dtCreted
                ,userModified,dtModified,um.nameuser as nameUserCreated, um.nameuser as nameUserModified
                FROM Layouts l
                LEFT OUTER JOIN Users um ON um.idUser = l.userModified
                LEFT OUTER JOIN Users uc ON uc.idUser = l.userCreated 
                WHERE templateTable = '" + templatetable + @"' 
                ORDER BY idLayout");

            return conn.executeSelectQuery(query, null);
        }
       
        
        public bool UpdateLayout(LayoutsModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE Layouts SET 
                nameLayout = @nameLayout, typeDocument = @typeDocument, languageLayout = @languageLayout, fileLayout = @fileLayout, bookmarks = @bookmarks, 
                templateTable = @templateTable, userCreated = @userCreated, dtCreted = @dtCreted, userModified = @userModified, dtModified = @dtModified 
                WHERE idLayout = @idLayout");

            SqlParameter[] sqlParameter = new SqlParameter[11];

            sqlParameter[0] = new SqlParameter("@nameLayout", SqlDbType.NVarChar);
            sqlParameter[0].Value = model.nameLayout;

            sqlParameter[1] = new SqlParameter("@typeDocument", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.typeDocument;

            sqlParameter[2] = new SqlParameter("@languageLayout", SqlDbType.NVarChar);
            sqlParameter[2].Value = model.languageLayout;

            sqlParameter[3] = new SqlParameter("@fileLayout", SqlDbType.NVarChar);
            sqlParameter[3].Value = model.fileLayout;
            
            sqlParameter[4] = new SqlParameter("@bookmarks", SqlDbType.NVarChar);
            sqlParameter[4].Value = model.bookmarks;

            sqlParameter[5] = new SqlParameter("@templateTable", SqlDbType.NVarChar);
            sqlParameter[5].Value = model.templateTable;

            sqlParameter[6] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[6].Value = model.userCreated;

            sqlParameter[7] = new SqlParameter("@dtCreted", SqlDbType.DateTime);
            sqlParameter[7].Value = model.dtCreated;

            sqlParameter[8] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[8].Value = model.userModified;

            sqlParameter[9] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[9].Value = model.dtModified;

            sqlParameter[10] = new SqlParameter("@idLayout", SqlDbType.Int);
            sqlParameter[10].Value = model.idLayout;


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
            sqlParameter[4].Value = model.idLayout;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idLayout";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Layouts";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update layout";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
       
        public bool SaveLayout(LayoutsModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO 
                Layouts (nameLayout, typeDocument, languageLayout, fileLayout, bookmarks, templateTable, userCreated, dtCreted, userModified, dtModified ) 
                VALUES (@nameLayout, @typeDocument, @languageLayout, @fileLayout, @bookmarks,@templateTable, @userCreated, @dtCreted , @userModified, @dtModified )");

            SqlParameter[] sqlParameter = new SqlParameter[10];

            sqlParameter[0] = new SqlParameter("@nameLayout", SqlDbType.NVarChar);
            sqlParameter[0].Value = model.nameLayout;

            sqlParameter[1] = new SqlParameter("@typeDocument", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.typeDocument;

            sqlParameter[2] = new SqlParameter("@languageLayout", SqlDbType.NVarChar);
            sqlParameter[2].Value = model.languageLayout;

            sqlParameter[3] = new SqlParameter("@fileLayout", SqlDbType.NVarChar);
            sqlParameter[3].Value = model.fileLayout;

            sqlParameter[4] = new SqlParameter("@bookmarks", SqlDbType.NVarChar);
            sqlParameter[4].Value = model.bookmarks;

            sqlParameter[5] = new SqlParameter("@templateTable", SqlDbType.NVarChar);
            sqlParameter[5].Value = model.templateTable;

            sqlParameter[6] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[6].Value = model.userCreated;

            sqlParameter[7] = new SqlParameter("@dtCreted", SqlDbType.DateTime);
            sqlParameter[7].Value = model.dtCreated;

            sqlParameter[8] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[8].Value = model.userModified;

            sqlParameter[9] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[9].Value = model.dtModified;


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
            sqlParameter[4].Value = conn.GetLastTableID("Layouts");

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idLayout";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Layouts";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save layout";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
    
       
        public bool DeleteLayoutByID(int ID, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM Layouts WHERE idLayout = @idLayout");

            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idLayout", SqlDbType.Int);
            sqlParameter[0].Value = ID;

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
            sqlParameter[4].Value = ID;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idLayout";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Layouts";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete layout";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
    
    }
}
