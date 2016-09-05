using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;
using System.Data.SqlTypes;
using BIS.Model;

namespace BIS.DAO
{
    public class TranslationDAO
    {
        private dbConnection conn;

        public TranslationDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllTranslation(string idLang)
        {
            string query = string.Format(
                @"SELECT idLang,stringKey,stringValue,dtString
                 FROM String"+idLang+"");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable CheckIfTranslationExists(string idLang, string stringKey)
        {
            string query = string.Format(
                @"SELECT idLang,stringKey,stringValue,dtString
                 FROM String" + idLang + " WHERE stringKey = '"+stringKey+"'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable CheckIfTranslationValueExists(string idLang, string stringKey)
        {
            string query = string.Format(
                @"SELECT idLang,stringKey,stringValue,dtString
                 FROM String" + idLang + " WHERE stringValue = '" + stringKey + "'");

            return conn.executeSelectQuery(query, null);
        }


        public bool Save(TranslationModel translation, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO 
                    StringNL(stringKey,stringValue,dtString) 
                    VALUES (@stringKey,@stringValue,@dtString);INSERT INTO 
                    StringEN(stringKey,stringValue,dtString) 
                    VALUES (@stringKey,@stringKey,@dtString)");


            SqlParameter[] sqlParameter = new SqlParameter[3];

            sqlParameter[0] = new SqlParameter("@stringKey", SqlDbType.NVarChar);
            sqlParameter[0].Value = translation.stringKey;

            sqlParameter[1] = new SqlParameter("@stringValue", SqlDbType.NVarChar);
            sqlParameter[1].Value = (translation.stringValue == null) ? SqlString.Null : translation.stringValue;

            sqlParameter[2] = new SqlParameter("@dtString", SqlDbType.DateTime);
            sqlParameter[2].Value = (translation.dtString == null || translation.dtString == DateTime.MinValue) ? SqlDateTime.Null : translation.dtString;


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
            sqlParameter[4].Value = (conn.GetLastTableID("StringNL") + 1).ToString() + "_" + (conn.GetLastTableID("StringEN") + 1).ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idLang_idLang";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "StringNL";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save string NL";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool Update(TranslationModel translation, string idLang, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE 
                    String"+idLang+" SET stringValue = @stringValue WHERE idLang=@idLang");


            SqlParameter[] sqlParameter = new SqlParameter[2];

            

            sqlParameter[0] = new SqlParameter("@stringValue", SqlDbType.NVarChar);
            sqlParameter[0].Value = (translation.stringValue == null) ? SqlString.Null : translation.stringValue;

            sqlParameter[1] = new SqlParameter("@idLang", SqlDbType.Int);
            sqlParameter[1].Value = translation.idLang;


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
            sqlParameter[4].Value = idLang;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idLang";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "String";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update string";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

    }
}
