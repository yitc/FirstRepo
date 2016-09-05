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
    public class EmployeePassportDAO
    {
        private dbConnection conn;

        public EmployeePassportDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllEmployeePassport(int idEmployee)
        {
            string query = string.Format(@"SELECT idemppass,idEmployee,passname,passnumber,passbrplace,passisplace,passisued,passvalid,passnational
              FROM EmployeePassport
                WHERE idEmployee = '" + idEmployee.ToString() + "' ");

            return conn.executeSelectQuery(query, null);

        }

        //public DataTable GetLastEmployeePasseportID()
        //{
        //    string query = string.Format(@"SELECT TOP 1 idemppass FROM EmployeePasseport ORDER BY idemppass DESC");
        //    SqlParameter[] sqlParameters = new SqlParameter[0];
        //    return conn.executeSelectQuery(query, sqlParameters);
        //}
        public DataTable GetEmployeePasseportByID(string idemppass)
        {
            string query = string.Format(@"SELECT idemppass,idEmployee,passname,passnumber,passbrplace,passisplace,passisued,passvalid,passnational
                                           FROM EmployeePassport 
                                           WHERE idemppass=@idEmployee ");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter(@"idempass",SqlDbType.Int);
            sqlParameters[0].Value = idemppass;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public Boolean Save(EmployeePassportModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO EmployeePassport 
                                                    (idEmployee,passname,passnumber,passbrplace,passisplace,passisued,passvalid,passnational)
                                            VALUES (@idEmployee, @passname, @passnumber, @passbrplace, @passisplace, @passisued, @passvalid, @passnational)");

            SqlParameter[] sqlParameter = new SqlParameter[8];

            sqlParameter[0] = new SqlParameter("@idEmployee", SqlDbType.Int);
            sqlParameter[0].Value = model.idEmployee;

            sqlParameter[1] = new SqlParameter("@passname", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.passname;

            sqlParameter[2] = new SqlParameter("@passnumber", SqlDbType.NVarChar);
            sqlParameter[2].Value = model.passnumber;

            sqlParameter[3] = new SqlParameter("@passbrplace", SqlDbType.NVarChar);
            sqlParameter[3].Value = model.passbrplace;

            sqlParameter[4] = new SqlParameter("@passisplace", SqlDbType.NVarChar);
            sqlParameter[4].Value = model.passisplace;

            sqlParameter[5] = new SqlParameter("@passisued", SqlDbType.Date);
            sqlParameter[5].Value = model.passisued;

            sqlParameter[6] = new SqlParameter("@passvalid", SqlDbType.Date);
            sqlParameter[6].Value = model.passvalid;

            sqlParameter[7] = new SqlParameter("@passnational", SqlDbType.Int);
            sqlParameter[7].Value = model.passnational;

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
            sqlParameter[4].Value = conn.GetLastTableID("EmployeePassport") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idemppass";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "EmployeePassport";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean Update(EmployeePassportModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE EmployeePassport SET idEmployee=@idEmployee, passname=@passname, passnumber=@passnumber, 
                                        passbrplace=@passbrplace, passisplace=@passisplace, passisued=@passisued, passvalid=@passvalid, passnational=@passnational
                                        WHERE idemppass=@idemppass ");

            SqlParameter[] sqlParameter = new SqlParameter[9];

            sqlParameter[0] = new SqlParameter("@idemppass", SqlDbType.Int);
            sqlParameter[0].Value = model.idemppass;

            sqlParameter[1] = new SqlParameter("@idEmployee", SqlDbType.Int);
            sqlParameter[1].Value = model.idEmployee;

            sqlParameter[2] = new SqlParameter("@passname", SqlDbType.NVarChar);
            sqlParameter[2].Value = model.passname;

            sqlParameter[3] = new SqlParameter("@passnumber", SqlDbType.NVarChar);
            sqlParameter[3].Value = model.passnumber;

            sqlParameter[4] = new SqlParameter("@passbrplace", SqlDbType.NVarChar);
            sqlParameter[4].Value = model.passbrplace;

            sqlParameter[5] = new SqlParameter("@passisplace", SqlDbType.NVarChar);
            sqlParameter[5].Value = model.passisplace;

            sqlParameter[6] = new SqlParameter("@passisued", SqlDbType.Date);
            sqlParameter[6].Value = model.passisued;

            sqlParameter[7] = new SqlParameter("@passvalid", SqlDbType.Date);
            sqlParameter[7].Value = model.passvalid;

            sqlParameter[8] = new SqlParameter("@passnational", SqlDbType.Int);
            sqlParameter[8].Value = model.passnational;


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
            sqlParameter[4].Value = model.idemppass;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idemppass";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "EmployeePassport";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

       
    }
}

