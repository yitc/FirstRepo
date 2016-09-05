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
    public class AccCreditLinePayDAO
    {
        private dbConnection conn;
        public AccCreditLinePayDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetAllLinesByCreditor(string creditor, string invoice)
        {
     
            string query = string.Format(@" SELECT   idCreditLinePay,dtDate,accNumber,invoiceNr,term,percentpay,amount
                                        FROM AccCreditLinePay  WHERE accNumber='" + creditor.ToString() + "' and invoiceNr='" + invoice.ToString() + "'");
            return conn.executeSelectQuery(query, null);
        }

        public bool Delete(int ID, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM AccCreditLinePay WHERE idCreditLinePay = @ID ");
            //AND booksort = @booksort
            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@ID", SqlDbType.Int);
            sqlParameter[0].Value = ID;

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
            sqlParameter[4].Value = ID;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idCreditLinePay";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccCreditLinePay";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);



            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public bool Save(AccCreditLinePayModel linemodel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO  AccCreditLinePay ( dtDate,accNumber,invoiceNr,term,percentpay,amount )
                                 Values (  @dtDate,@accNumber,@invoiceNr,@term,@percentpay,@amount)");


            SqlParameter[] sqlParameter = new SqlParameter[6];

            sqlParameter[0] = new SqlParameter("@dtDate", SqlDbType.DateTime);
            sqlParameter[0].Value = linemodel.dtDate;

            sqlParameter[1] = new SqlParameter("@accNumber", SqlDbType.NVarChar);
            sqlParameter[1].Value = linemodel.accNumber;

            sqlParameter[2] = new SqlParameter("@invoiceNr", SqlDbType.NVarChar);
            sqlParameter[2].Value = linemodel.invoiceNr;

            sqlParameter[3] = new SqlParameter("@term", SqlDbType.Int);
            sqlParameter[3].Value = linemodel.term;

            sqlParameter[4] = new SqlParameter("@percentpay", SqlDbType.Decimal);
            sqlParameter[4].Value = linemodel.percentpay;

            sqlParameter[5] = new SqlParameter("@amount", SqlDbType.Decimal);
            sqlParameter[5].Value = linemodel.amount;

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
            sqlParameter[4].Value = conn.GetLastTableID("AccCreditLinePay");

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "accNumber";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccCreditLinePay";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);



            return conn.executQueryTransaction(_query, sqlParameters);

        }


    }
}