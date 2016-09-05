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
    public class PersonEmailDAO
    {
        private dbConnection conn;
        public PersonEmailDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetPersonEmails(int idPerson)
        {
            string query = string.Format(@"SELECT idEmail,idContPers,email,isCommunication,isProspect,isInvoicing,isNewsletters,idEmailType, lastQuestionForm 
              FROM ContactPersonEmail
                WHERE idContPers = '" + idPerson.ToString() + "' ");

            return conn.executeSelectQuery(query, null);

        }

        public DataTable GetAllPersonEmails()
        {
            string query = string.Format(@"SELECT idEmail,idContPers,email,isCommunication,isProspect,isInvoicing,isNewsletters,idEmailType, lastQuestionForm 
              FROM ContactPersonEmail  ");

            return conn.executeSelectQuery(query, null);

        }

        public DataTable GetPersonEmailsISCommunication(int idPerson)
        {
            string query = string.Format(@"SELECT idEmail,idContPers,email,isCommunication,isProspect,isInvoicing,isNewsletters,idEmailType, lastQuestionForm 
              FROM ContactPersonEmail
                WHERE idContPers = '" + idPerson.ToString() + "' AND isCommunication = 'TRUE'");

            return conn.executeSelectQuery(query, null);

        }

        public DataTable GetPersonEmailsISInoicing(int idPerson)
        {
            string query = string.Format(@"SELECT 
                CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END as isInvoicing,
                CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN '' ELSE (SELECT TOP 1 email FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') END as email  
                FROM ContactPerson p
                WHERE p.idContPers = '" + idPerson.ToString() +"'");

            return conn.executeSelectQuery(query, null);

        }

        public object CheckPErsonForInvoicingEmail(int idContPers)
        {
            string query = string.Format(@"SELECT 
                        CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END as isInvoicing
                        FROM ContactPerson p
                        WHERE p.idContPers = '" + idContPers.ToString() +"'");

            return conn.executeScalarQuery(query, null);
        }

        public DataTable GetPersonEmailsByType(int idEmailType, int idContPers)
        {
            string query = string.Format(@"SELECT idEmail,idContPers,email,isCommunication,isProspect,isInvoicing,isNewsletters,idEmailType, lastQuestionForm 
              FROM ContactPersonEmail
                WHERE idEmailType = '" + idEmailType + "' AND idContPers = '" + idContPers + "'");

            return conn.executeSelectQuery(query, null);

        }
        public bool Save(PersonEmailModel email, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

                    string query = string.Format(@"INSERT INTO ContactPersonEmail (idContPers, email,isCommunication,
                    isProspect,isInvoicing,isNewsletters,idEmailType,lastQuestionForm) 
                    VALUES(@idContPers, @email, @isCommunication, @isProspect, @isInvoicing,@isNewsletters, @idEmailType,@lastQuestionForm)");


                    SqlParameter[] sqlParameter = new SqlParameter[8];

                    sqlParameter[0] = new SqlParameter("@idContPers", SqlDbType.Int);
                    sqlParameter[0].Value = email.idContPers;

                    sqlParameter[1] = new SqlParameter("@email", SqlDbType.NVarChar);
                    sqlParameter[1].Value = (email.email == null) ? SqlString.Null : email.email;

                    sqlParameter[2] = new SqlParameter("@isCommunication", SqlDbType.Bit);
                    sqlParameter[2].Value = email.isCommunication;

                    sqlParameter[3] = new SqlParameter("@isProspect", SqlDbType.Bit);
                    sqlParameter[3].Value = email.isProspect;

                    sqlParameter[4] = new SqlParameter("@isInvoicing", SqlDbType.Bit);
                    sqlParameter[4].Value = email.isInvoicing;

                    sqlParameter[5] = new SqlParameter("@isNewsletters", SqlDbType.Bit);
                    sqlParameter[5].Value = email.isNewsletters;

                    sqlParameter[6] = new SqlParameter("@idEmailType", SqlDbType.Int);
                    sqlParameter[6].Value = (email.idEmailType == null) ? 0 : email.idEmailType;

                    sqlParameter[7] = new SqlParameter("@lastQuestionForm", SqlDbType.Bit);
                    sqlParameter[7].Value = email.lastQuestionForm;

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
                    sqlParameter[4].Value = conn.GetLastTableID("ContactPersonEmail") + 1;

                    sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                    sqlParameter[5].Value = "idEmail";

                    sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                    sqlParameter[6].Value = "ContactPersonEmail";

                    sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                    sqlParameter[7].Value = "Save contact person email";

                    _query.Add(query);
                    sqlParameters.Add(sqlParameter);


                    return conn.executQueryTransaction(_query, sqlParameters);
           
        }

        public bool Update(PersonEmailModel email, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string querySelect = string.Format(@"SELECT idContPers,email,isCommunication,isProspect,isInvoicing, isNewsletters, idEmailType, lastQuestionForm 
                FROM ContactPersonEmail
                WHERE idEmail = '" + email.idEmail.ToString() + "'");

                DataTable dt = conn.executeSelectQuery(querySelect, null);

                if (dt!=null && dt.Rows.Count > 0)
                {
                    string query = string.Format(@"UPDATE ContactPersonEmail SET  email = @email, 
                isCommunication = @isCommunication, 
                isProspect = @isProspect,isInvoicing = @isInvoicing , isNewsletters = @isNewsletters, idEmailType = @idEmailType, lastQuestionForm = @lastQuestionForm 
                WHERE idEmail = @idEmail ");


                    SqlParameter[] sqlParameter = new SqlParameter[9];

                    sqlParameter[0] = new SqlParameter("@email", SqlDbType.NVarChar);
                    sqlParameter[0].Value = (email.email == null) ? SqlString.Null : email.email;

                    sqlParameter[1] = new SqlParameter("@isCommunication", SqlDbType.Bit);
                    sqlParameter[1].Value = email.isCommunication;

                    sqlParameter[2] = new SqlParameter("@isProspect", SqlDbType.Bit);
                    sqlParameter[2].Value = email.isProspect;

                    sqlParameter[3] = new SqlParameter("@isInvoicing", SqlDbType.Bit);
                    sqlParameter[3].Value = email.isInvoicing;

                    sqlParameter[4] = new SqlParameter("@idContPers", SqlDbType.Int);
                    sqlParameter[4].Value = email.idContPers;

                    sqlParameter[5] = new SqlParameter("@isNewsletters", SqlDbType.Bit);
                    sqlParameter[5].Value = email.isNewsletters;

                    sqlParameter[6] = new SqlParameter("@idEmailType", SqlDbType.Int);
                    sqlParameter[6].Value = (email.idEmailType == null) ? 0 : email.idEmailType;

                    sqlParameter[7] = new SqlParameter("@lastQuestionForm", SqlDbType.Bit);
                    sqlParameter[7].Value = email.lastQuestionForm;

                    sqlParameter[8] = new SqlParameter("@idEmail", SqlDbType.Int);
                    sqlParameter[8].Value = email.idEmail;

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
                    sqlParameter[4].Value = email.idEmail;

                    sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                    sqlParameter[5].Value = "idEmail";

                    sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                    sqlParameter[6].Value = "ContactPersonEmail";

                    sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                    sqlParameter[7].Value = "Update contact person email";

                    _query.Add(query);
                    sqlParameters.Add(sqlParameter);


                    return conn.executQueryTransaction(_query, sqlParameters);
                }
                else
                {
                    return Save(email,nameForm,idUser);
                }
        }

        public bool Delete(int idEmail, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  ContactPersonEmail WHERE idEmail = @idEmail");

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
            sqlParameter[6].Value = "ContactPersonEmail";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete contact person email";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
    }
}