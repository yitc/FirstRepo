using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIS.Core;
using System.Data;
using System.Data.SqlClient;

namespace BIS.DAO
{
    public class LanguagesDAO
    {
        private dbConnection conn;

        public LanguagesDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetLanguages()
        {
            string query = string.Format(
                @"SELECT idLang,nameLang FROM Languages");

            return conn.executeSelectQuery(query, null);
        }

        public Boolean UpdateLanguages(string lang, int idUser, string nameForm)
        {
            try
            {
                List<string> _query = new List<string>();
                List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

                string query = string.Format(@"UPDATE Users set lngUser = @lngUser
	        	WHERE idUser = @idUser");

                SqlParameter[] sqlParameter = new SqlParameter[2];
                sqlParameter[0] = new SqlParameter("@lngUser", SqlDbType.VarChar);
                sqlParameter[0].Value = lang;
                sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
                sqlParameter[1].Value = idUser;
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
                sqlParameter[4].Value = idUser;

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idUser";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "Users";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Update languages";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);


                return conn.executQueryTransaction(_query, sqlParameters);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
