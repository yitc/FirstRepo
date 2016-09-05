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
    public class DepartmentsDAO
    {

        private dbConnection conn;
        public DepartmentsDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetAllDepartments()
        {
            string query = string.Format(@"SELECT idDepartment,nameDepartment,telephoneDepartment,emailDepartment
                                           FROM Departments ");

            return conn.executeSelectQuery(query, null);

        }
        public DataTable GetDepartmentsByID(string idDepartments)
        {
            string query = string.Format(@"SELECT idDepartment,nameDepartment,telephoneDepartment,emailDepartment 
                                            FROM Departments 
                                            WHERE nameDepartment=@idDepartment");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idDepartment", SqlDbType.NVarChar);
            sqlParameters[0].Value = idDepartments;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public Boolean Save(DepartmentsModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO Departments (nameDepartment,telephoneDepartment,emailDepartment)
                                            VALUES (@nameDepartment,@telephoneDepartment,@emailDepartment)");

            SqlParameter[] sqlParameter = new SqlParameter[3];

            sqlParameter[0] = new SqlParameter("@nameDepartment", SqlDbType.NVarChar);
            sqlParameter[0].Value = model.nameDepartment;

            sqlParameter[1] = new SqlParameter("@telephoneDepartment", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.telephoneDepartment;

            sqlParameter[2] = new SqlParameter("@emailDepartment", SqlDbType.NVarChar);
            sqlParameter[2].Value = model.emailDepartment;

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
            sqlParameter[4].Value = conn.GetLastTableID("Departments")+1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idDepartment";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Departments";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
        public Boolean Update(DepartmentsModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE Departments SET nameDepartment=@nameDepartment, 
                                          telephoneDepartment=@telephoneDepartment, emailDepartment=@emailDepartment
                                          WHERE idDepartment=@idDepartment ");

            SqlParameter[] sqlParameter = new SqlParameter[4];

            sqlParameter[0] = new SqlParameter("@idDepartment", SqlDbType.Int);
            sqlParameter[0].Value = model.idDepartment;

            sqlParameter[1] = new SqlParameter("@nameDepartment", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.nameDepartment;

            sqlParameter[2] = new SqlParameter("@telephoneDepartment", SqlDbType.NVarChar);
            sqlParameter[2].Value = model.telephoneDepartment;

            sqlParameter[3] = new SqlParameter("@emailDepartment", SqlDbType.NVarChar);
            sqlParameter[3].Value = model.emailDepartment;

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
            sqlParameter[4].Value = model.idDepartment;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idDepartment";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Departments";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
       
    }
}