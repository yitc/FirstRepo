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
    public class AccPaymentDAO
    {
        private dbConnection conn;

        public AccPaymentDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllAccPayment()
        {
            string query = string.Format(@"SELECT idPayment, numberDays, isDebitor, isCreditor, description 
                                         FROM AccPayment" );

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAccPaymentByID(int idPayment)
        {
            string query = string.Format(@"SELECT idPayment, numberDays, isDebitor, isCreditor, description
                                        FROM AccPayment
                                        WHERE idPayment = @idPayment ");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idPayment", SqlDbType.Int);
            sqlParameters[0].Value = idPayment;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetAccPaymentForDelete(int idPayment)
        {
            string query = string.Format(@"SELECT *
                                        FROM AccDebCre
                                        WHERE payCondition = @idPayment ");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idPayment", SqlDbType.Int);
            sqlParameters[0].Value = idPayment;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public Boolean Save(AccPaymentModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format (@"INSERT INTO AccPayment (numberDays, isDebitor, isCreditor, description )
                                            VALUES (@numberDays , @isDebitor,  @isCreditor , @description)");

            SqlParameter[] sqlParameter = new SqlParameter[4];

            sqlParameter[0] = new SqlParameter("@numberDays", SqlDbType.Int);
            sqlParameter[0].Value = model.numberDays;

            sqlParameter[1] = new SqlParameter("@isDebitor", SqlDbType.Bit);
            sqlParameter[1].Value = model.isDebitor;

            sqlParameter[2] = new SqlParameter("@isCreditor", SqlDbType.Bit);
            sqlParameter[2].Value = model.isCreditor;

            sqlParameter[3] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[3].Value = model.description;

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
            sqlParameter[4].Value = conn.GetLastTableID("AccPayment")+1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPayment";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccPayment";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean Update(AccPaymentModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE AccPayment SET numberDays= @numberDays, isDebitor = @isDebitor, 
                                           isCreditor = @isCreditor, description = @description 
                                           WHERE idPayment = @idPayment");

            SqlParameter[] sqlParameter = new SqlParameter[5];

            sqlParameter[0] = new SqlParameter("@idPayment", SqlDbType.Int);
            sqlParameter[0].Value = model.idPayment;

            sqlParameter[1] = new SqlParameter("@numberDays", SqlDbType.Int);
            sqlParameter[1].Value = model.numberDays;

            sqlParameter[2] = new SqlParameter("@isDebitor", SqlDbType.Bit);
            sqlParameter[2].Value = model.isDebitor;

            sqlParameter[3] = new SqlParameter("@isCreditor", SqlDbType.Bit);
            sqlParameter[3].Value = model.isCreditor;

            sqlParameter[4] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[4].Value = model.description;

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
            sqlParameter[4].Value = model.idPayment;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPayment";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccPayment";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean Delete(int idPayment, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();


            string query = string.Format(@"DELETE AccPayment
                                           WHERE idPayment = @idPayment ");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idPayment", SqlDbType.Int);
            sqlParameter[0].Value = idPayment;

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
            sqlParameter[4].Value = idPayment;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPayment";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccPayment";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
    }
    
    
}