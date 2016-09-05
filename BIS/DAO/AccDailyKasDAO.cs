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
    public class AccDailyKasDAO
    {
        private dbConnection conn;

        public AccDailyKasDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllAccDailyKas()
        {
            string query = string.Format(@"SELECT idAccDailyKas, codeDaily, refnoKas, dtKas, begSaldo, endSaldo,bookingYear,userCreated,dtCreated,userModified,dtModified FROM AccDailyKas");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAllByDaily(string idDaily, string bookYear)
        {
            string query = string.Format(@" SELECT   idAccDailyKas, codeDaily, refnoKas, dtKas,  begSaldo,bookingYear, CAST(0 as decimal(10,2)) as difference,  endSaldo, CAST(0 as decimal(10,2)) as Booked
                                        ,userCreated,dtCreated,userModified,dtModified
                                        FROM AccDailyKas  WHERE codeDaily='" + idDaily.ToString() + "' and bookingYear ='"+bookYear+"' ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllAccDailyKasByID(string idAccDailyKas)
        {
            string query = string.Format(@"SELECT idAccDailyKas, codeDaily, refnoKas, dtKas,begSaldo, endSaldo,bookingYear,userCreated,dtCreated,userModified,dtModified
                                            FROM AccDailyKas
                                            WHERE codeDaily=@idAccDailyKas");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idAccDailyKas", SqlDbType.NVarChar);
            sqlParameters[0].Value = idAccDailyKas;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetLastKas()
        {
            string query = string.Format(@" SELECT  TOP 1 idAccDailyKas, codeDaily, refnoKas, dtKas,  begSaldo, endSaldo, bookingYear,userCreated,dtCreated,userModified,dtModified 
                                        FROM AccDailyKas ORDER BY idAccDailyKas DESC  ");

            return conn.executeSelectQuery(query, null);
        }


        public int SaveAndReturnID(AccDailyKasModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO  AccDailyKas (codeDaily,refnoKas, dtKas,begSaldo, endSaldo, bookingYear,userCreated,dtCreated,userModified,dtModified )
                                 Values ( @codeDaily,@refnoKas,@dtKas,@begSaldo, @endSaldo,@bookingYear,@userCreated,@dtCreated,@userModified,@dtModified ); SELECT SCOPE_IDENTITY()");


            SqlParameter[] sqlParameter = new SqlParameter[10];

            sqlParameter[0] = new SqlParameter("@codeDaily", SqlDbType.NVarChar);
            sqlParameter[0].Value = model.codeDaily;

            sqlParameter[1] = new SqlParameter("@refnoKas", SqlDbType.Int);
            sqlParameter[1].Value = model.refnoKas;

            sqlParameter[2] = new SqlParameter("@dtKas", SqlDbType.DateTime);
            sqlParameter[2].Value = model.dtKas;

            sqlParameter[3] = new SqlParameter("@begSaldo", SqlDbType.Decimal);
            sqlParameter[3].Value = model.begSaldo;

            sqlParameter[4] = new SqlParameter("@endSaldo", SqlDbType.Decimal);
            sqlParameter[4].Value = model.endSaldo;

            sqlParameter[5] = new SqlParameter("@bookingYear", SqlDbType.NVarChar);
            sqlParameter[5].Value = model.bookingYear;

            sqlParameter[6] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[6].Value = model.userCreated;

            sqlParameter[7] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[7].Value = DateTime.Now; //model.dtCreated;

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
            sqlParameter[4].Value = (conn.GetLastTableID("AccDailyKas") + 1).ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAccDailyKas";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccDailyKas";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save and return id";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransactionSelectLastID(_query, sqlParameters);

        }

        public bool Delete(int id, string nameForm, int idUser, string bookYear)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  AccDailyKas WHERE idAccDailyKas = @idDailyBank and bookingYear = '" + bookYear + "'");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idDailyBank", SqlDbType.Int);
            sqlParameter[0].Value = id;

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
            sqlParameter[4].Value = id;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idDailyKas";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccDailyKas";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
    }
}