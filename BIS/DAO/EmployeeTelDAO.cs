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
    public class EmployeeTelDAO
    {
        private dbConnection conn;
        public EmployeeTelDAO()
        {
            conn = new dbConnection();

        }

//        public DataTable GetEmployeeTels(int idEmployee, string idLang)
//        {
//            //idtelemp,idEmployee, izbaceno iz grida

//            string query = string.Format(@"SELECT telephone,telephoneType,isDefault,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE description END AS description
//              FROM EmployeeTel
//              LEFT OUTER JOIN STRING"+idLang+@" s ON s.stringKey = description
//              WHERE idEmployee = '" + idEmployee.ToString() + "' ");

//            return conn.executeSelectQuery(query, null);

//        }

        public DataTable GetEmployeeTels(int idEmployee)
        {
            string query = string.Format(@"SELECT idtelemp, idEmployee, telephone, telephoneType, isDefault, description
                                        FROM EmployeeTel
                                        WHERE idEmployee = '" + idEmployee.ToString() + "' ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetEmployeeTelsByType(int telephoneType, int idEmployee)
        {
            string query = string.Format(@"SELECT idtelemp, idEmployee, telephone, telephoneType, isDefault, description
                                            FROM EmployeeTel
                                            WHERE telephoneType = '" + telephoneType + "' AND idEmployee= '" + idEmployee + "'");

            return conn.executeSelectQuery(query, null);
        }

        public bool Save(EmployeeTelModel tel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO EmployeeTel (idEmployee, telephone, telephoneType, isDefault, description)
                                        VALUES(@idEmployee, @telephone, @telephoneType, @isDefault, @description )");

            SqlParameter[] sqlParameter = new SqlParameter[5];

            sqlParameter[0] = new SqlParameter("@idEmployee", SqlDbType.Int);
            sqlParameter[0].Value = tel.idEmployee;

            sqlParameter[1] = new SqlParameter("@telephone", SqlDbType.NVarChar);
            sqlParameter[1].Value = (tel.telephone == null) ? SqlString.Null : tel.telephone;

            sqlParameter[2] = new SqlParameter("@telephoneType", SqlDbType.Int);
            sqlParameter[2].Value = (tel.telephoneType == null) ? 0 : tel.telephoneType;

            sqlParameter[3] = new SqlParameter("@isDefault", SqlDbType.Bit);
            sqlParameter[3].Value = tel.isDefault;

            sqlParameter[4] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[4].Value = (tel.description == null) ? SqlString.Null : tel.description;

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
            sqlParameter[4].Value = conn.GetLastTableID("EmployeeTel") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idtelemp";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "EmployeeTel";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool Update(EmployeeTelModel tel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string querySelect = string.Format(@"SELECT idEmployee,telephone, telephoneType, isDefault, description
                                                FROM EmployeeTel
                                                WHERE idtelemp ='" + tel.idtelemp.ToString() + "'");

            DataTable dt = conn.executeSelectQuery(querySelect, null);

            if( dt!=null && dt.Rows.Count > 0 )
            {
                string query = string.Format(@"UPDATE EmployeeTel SET 
                idEmployee = @idEmployee, telephone = @telephone, telephoneType = @telephoneType, isDefault= @isDefault, description = @description
                WHERE idtelemp = @idtelemp ");

                SqlParameter[] sqlParameter = new SqlParameter[6];

                sqlParameter[0] = new SqlParameter("@idEmployee", SqlDbType.Int);
                sqlParameter[0].Value = tel.idEmployee;

                sqlParameter[1] = new SqlParameter("@telephone", SqlDbType.NVarChar);
                sqlParameter[1].Value = (tel.telephone == null) ? SqlString.Null : tel.telephone;

                sqlParameter[2] = new SqlParameter("@telephoneType", SqlDbType.Int);
                sqlParameter[2].Value = (tel.telephoneType == null) ? 0 : tel.telephoneType;

                sqlParameter[3] = new SqlParameter("@isDefault", SqlDbType.Bit);
                sqlParameter[3].Value = tel.isDefault;

                sqlParameter[4] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[4].Value = (tel.description == null) ? SqlString.Null : tel.description;

                sqlParameter[5] = new SqlParameter("@idtelemp", SqlDbType.Int);
                sqlParameter[5].Value = tel.idtelemp;

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
                sqlParameter[4].Value = tel.idtelemp;

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idtelemp";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "EmployeeTel";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Update";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);


                return conn.executQueryTransaction(_query, sqlParameters);
            }
            else
            {
                return Save(tel,nameForm,idUser);
            }
        }

    }
}