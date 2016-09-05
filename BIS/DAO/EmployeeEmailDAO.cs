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
    public class EmployeeEmailDAO
    {
        private dbConnection conn;
        public EmployeeEmailDAO()
        {
            conn = new dbConnection();

        }

//        public DataTable GetEmployeeEmails(int idEmployee)
//        {
//            //idEmpEmail,idEmployee, izbaceno iz grida
//            string query = string.Format(@"SELECT email,emailType
//              FROM EmployeeEmail
//                WHERE idEmployee = '" + idEmployee.ToString() + "' ");

//            return conn.executeSelectQuery(query, null);

//        }

        public DataTable GetEmployeeEmails(int idEmployee)
        {
            string query = string.Format(@"SELECT idEmpEmail, idEmployee, email, emailType
                                           FROM EmployeeEmail
                                           WHERE idEmployee = '" + idEmployee.ToString() + "' ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetEmployeeEmailsByType(int emailType, int idEmployee)
        {
            string query = string.Format(@"SELECT idEmpEmail, idEmployee, email, emailType
                                        FROM EmployeeEmail
                                        WHERE emailType = '" + emailType + "' AND idEmployee = '" + idEmployee + "'");

            return conn.executeSelectQuery(query, null);
        }

        public bool Save(EmployeeEmailModel email, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query=string.Format(@"INSERT INTO EmployeeEmail (idEmployee, email, emailType)
                                        VALUES (@idEmployee, @email, @emailType)");

            SqlParameter[] sqlParameter = new SqlParameter[3];

            sqlParameter[0] = new SqlParameter("@idEmployee", SqlDbType.Int);
            sqlParameter[0].Value = email.idEmployee;

            sqlParameter[1] = new SqlParameter("@email", SqlDbType.NVarChar);
            sqlParameter[1].Value = (email.email == null) ? SqlString.Null : email.email;

            sqlParameter[2] = new SqlParameter("@emailType", SqlDbType.Int);
            sqlParameter[2].Value = (email.emailType == null) ? 0 : email.emailType;

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
            sqlParameter[4].Value = conn.GetLastTableID("EmployeeEmail") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idEmpEmail";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "EmployeeEmail";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save employee email";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool Update(EmployeeEmailModel email, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string querySelect = string.Format(@"SELECT idEmployee, email, emailType
                                               FROM EmployeeEmail
                                               WHERE idEmpEmail= '" + email.idEmpEmail.ToString() + "'");

            DataTable dt = conn.executeSelectQuery(querySelect, null);

            if(dt!=null && dt.Rows.Count > 0)
            {
                string query = string.Format(@"UPDATE EmployeeEmail SET idEmployee = @idEmployee,
                email = @email, emailType = @emailType
                WHERE idEmpEmail = @idEmpEmail ");

                SqlParameter[] sqlParameter = new SqlParameter[4];

                sqlParameter[0] = new SqlParameter("@idEmployee", SqlDbType.Int);
                sqlParameter[0].Value = email.idEmployee;

                sqlParameter[1] = new SqlParameter("@email", SqlDbType.NVarChar);
                sqlParameter[1].Value = (email.email == null) ? SqlString.Null : email.email;

                sqlParameter[2] = new SqlParameter("@emailType", SqlDbType.Int);
                sqlParameter[2].Value = (email.emailType == null) ? 0 : email.emailType;

                sqlParameter[3] = new SqlParameter("@idEmpEmail", SqlDbType.Int);
                sqlParameter[3].Value = email.idEmpEmail;

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
                sqlParameter[4].Value = email.idEmpEmail;

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idEmpEmail";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "EmployeeEmail";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Update employee email";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);


                return conn.executQueryTransaction(_query, sqlParameters);
            }
            else
            {
                return Save(email,nameForm,idUser);
            }
        }

        public bool Delete(int idEmpEmail, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM EmployeeEmail 
                                        WHERE idEmpEmail = @idEmpEmail ");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idEmpEmail", SqlDbType.Int);
            sqlParameter[0].Value = idEmpEmail;

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
            sqlParameter[4].Value = idEmpEmail;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idEmpEmail";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "EmployeeEmail";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete employee email";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
    }
}