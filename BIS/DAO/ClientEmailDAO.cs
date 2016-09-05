using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;
using BIS.Model;
using System.Data.SqlTypes;

namespace BIS.DAO
{
    public class ClientEmailDAO
    {
        private dbConnection conn;
        public ClientEmailDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetClientEmails(int idClient)
        {
            string query = string.Format(@"SELECT idEmail,idClient,email,isCommunication,isNewsletters,isInvoicing,idEmailType
              FROM ClientEmail
                WHERE idClient = '" + idClient.ToString() + "' ");

            return conn.executeSelectQuery(query, null);

        }
        public DataTable GetClientEmailsByType(int idEmailType, int idClient)
        {
            string query = string.Format(@"SELECT idEmail,idClient,email,isCommunication,isNewsletters,isInvoicing,idEmailType
              FROM ClientEmail
                WHERE idEmailType = '" + idEmailType + "' AND idClient = '" + idClient + "'");

            return conn.executeSelectQuery(query, null);

        }

        public bool Save(ClientEmailModel email, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO ClientEmail (idClient, email,isCommunication,
                    isNewsletters,isInvoicing,idEmailType) 
                    VALUES(@idClient, @email, @isCommunication, @isNewsletters, @isInvoicing, @idEmailType)");


            SqlParameter[] sqlParameter = new SqlParameter[6];

            sqlParameter[0] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[0].Value = email.idClient;

            sqlParameter[1] = new SqlParameter("@email", SqlDbType.NVarChar);
            sqlParameter[1].Value = (email.email == null) ? SqlString.Null : email.email;

            sqlParameter[2] = new SqlParameter("@isCommunication", SqlDbType.Bit);
            sqlParameter[2].Value = email.isCommunication;

            sqlParameter[3] = new SqlParameter("@isNewsletters", SqlDbType.Bit);
            sqlParameter[3].Value = email.isNewsletters;

            sqlParameter[4] = new SqlParameter("@isInvoicing", SqlDbType.Bit);
            sqlParameter[4].Value = email.isInvoicing;

            sqlParameter[5] = new SqlParameter("@idEmailType", SqlDbType.Int);
            sqlParameter[5].Value = (email.idEmailType == null) ? 0 : email.idEmailType;

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
            sqlParameter[4].Value = email.idClient.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idClient";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ClientEmail";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public bool Update(ClientEmailModel email, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string querySelect = string.Format(@"SELECT idClient,email,isCommunication,isNewsletters,isInvoicing,idEmailType
                FROM ClientEmail
                WHERE idEmail = '" + email.idEmail.ToString() + "'");

            DataTable dt = conn.executeSelectQuery(querySelect, null);

            if (dt != null && dt.Rows.Count > 0)
            {
                string query = string.Format(@"UPDATE ClientEmail SET  email = @email, 
                isCommunication = @isCommunication, 
                isNewsletters = @isNewsletters,isInvoicing = @isInvoicing ,idEmailType = @idEmailType
                WHERE idEmail = @idEmail ");


                SqlParameter[] sqlParameter = new SqlParameter[7];

                sqlParameter[0] = new SqlParameter("@email", SqlDbType.NVarChar);
                sqlParameter[0].Value = (email.email == null) ? SqlString.Null : email.email;

                sqlParameter[1] = new SqlParameter("@isCommunication", SqlDbType.Bit);
                sqlParameter[1].Value = email.isCommunication;

                sqlParameter[2] = new SqlParameter("@isNewsletters", SqlDbType.Bit);
                sqlParameter[2].Value = email.isNewsletters;

                sqlParameter[3] = new SqlParameter("@isInvoicing", SqlDbType.Bit);
                sqlParameter[3].Value = email.isInvoicing;

                sqlParameter[4] = new SqlParameter("@idClient", SqlDbType.Int);
                sqlParameter[4].Value = email.idClient;

                sqlParameter[5] = new SqlParameter("@idEmailType", SqlDbType.Int);
                sqlParameter[5].Value = (email.idEmailType == null) ? 0 : email.idEmailType;

                sqlParameter[6] = new SqlParameter("@idEmail", SqlDbType.Int);
                sqlParameter[6].Value = email.idEmail;

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
                sqlParameter[4].Value = email.idEmail.ToString();

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idEmail";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "ClientEmail";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Update";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);


                return conn.executQueryTransaction(_query, sqlParameters);
            }
            else
            {
                return Save(email, nameForm, idUser);
            }
        }

        public bool Delete(int idEmail, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  ClientEmail WHERE idEmail = @idEmail");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idEmail", SqlDbType.Int);
            sqlParameter[0].Value = idEmail;

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
            sqlParameter[4].Value = idEmail;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idEmail";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ClientEmail";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
    }
}