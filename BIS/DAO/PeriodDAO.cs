using BIS.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.DAO
{
    public class PeriodDAO
    {
        private dbConnection conn;
        public PeriodDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllPeriod()
        {
            string query = string.Format(@"
                SELECT idPeriod, monthFrom as monthFrom, monthTo as monthTo, descPeriod                       
                FROM Period");

            return conn.executeSelectQuery(query, null);
        }

        public Boolean Save(int idPeriod, int monthFrom, int monthTo, string descPeriod, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@" INSERT INTO Period
            (idPeriod,monthFrom, monthTo, descPeriod) VALUES (@idPeriod, @monthFrom,@monthTo,@descPeriod) ");

            SqlParameter[] sqlParameter = new SqlParameter[4];

            sqlParameter[0] = new SqlParameter("@idPeriod", SqlDbType.Int);
            sqlParameter[0].Value = idPeriod;

            sqlParameter[1] = new SqlParameter("@monthFrom", SqlDbType.Int);
            sqlParameter[1].Value = monthFrom;

            sqlParameter[2] = new SqlParameter("@monthTo", SqlDbType.Int);
            sqlParameter[2].Value = monthTo;

            sqlParameter[3] = new SqlParameter("@descPeriod", SqlDbType.NVarChar);
            sqlParameter[3].Value = descPeriod;

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
            sqlParameter[4].Value = idPeriod;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPeriod";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Period";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save Period";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean Update(int idPeriod, int monthFrom, int monthTo, string descPeriod, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE Period SET monthFrom = @monthFrom, monthTo = @monthTo, descPeriod = @descPeriod
                                        WHERE idPeriod = @idPeriod");

            SqlParameter[] sqlParameter = new SqlParameter[4];

            sqlParameter[0] = new SqlParameter("@idPeriod", SqlDbType.Int);
            sqlParameter[0].Value = idPeriod;

            sqlParameter[1] = new SqlParameter("@monthFrom", SqlDbType.Int);
            sqlParameter[1].Value = monthFrom;

            sqlParameter[2] = new SqlParameter("@monthTo", SqlDbType.Int);
            sqlParameter[2].Value = monthTo;

            sqlParameter[3] = new SqlParameter("@descPeriod", SqlDbType.NVarChar);
            sqlParameter[3].Value = descPeriod;

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
            sqlParameter[4].Value = idPeriod;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPeriod";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Period";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update Period";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public DataTable LastId()
        {
            string query = string.Format(@"SELECT top(1) idPeriod
                                   FROM Period  ORDER BY idPeriod DESC ");

            return conn.executeSelectQuery(query, null);

        }

        public Boolean DeletePriodScript(int idPeriod, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM Period WHERE idPeriod = @idPeriod ");

            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idPeriod", SqlDbType.Int);
            sqlParameter[0].Value = idPeriod;

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
            sqlParameter[4].Value = idPeriod;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPeriod";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Period";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete Period";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
    }
}
