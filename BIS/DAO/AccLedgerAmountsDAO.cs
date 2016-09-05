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
    public class AccLedgerAmountsDAO
    {
        private dbConnection conn;

        public AccLedgerAmountsDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllAmounts(string year)
        {
            string query = string.Format(@"SELECT idAccount, numberLedgerAccount, bookingYear, beginDebit, beginCredit, debitAmount,creditAmount,transactionsNo,
                                        userCreated,dtCreated,userModified,dtModified 
                                        FROM AccLedgerAmounts WHERE bookingYear = '" +year+"'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAmountPerYear(string konto, string year)
        {
            string query = string.Format(@"SELECT idAccount, numberLedgerAccount, bookingYear, beginDebit, beginCredit, debitAmount,creditAmount,transactionsNo,
                                        userCreated,dtCreated,userModified,dtModified
                                        FROM AccLedgerAmounts  WHERE numberLedgerAccount='" + konto + "' and bookingYear = '" + year + "'");

            return conn.executeSelectQuery(query, null);
        }
        public bool Delete(string konto, string year, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE  FROM AccLedgerAmounts  WHERE numberLedgerAccount='" + konto + "' and bookingYear = '" + year + "'");

            _query.Add(query);
            sqlParameters.Add(null);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");


            SqlParameter[] sqlParameter = new SqlParameter[8];


            sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameForm;

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = idUser;

            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "D";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = konto + "_"+ year;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "numberLedgerAccount_bookingYear";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccLedgerAmounts";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean Save(AccLedgerAmountsModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO AccLedgerAmounts
                                            (idAccount, numberLedgerAccount, bookingYear, beginDebit, beginCredit, debitAmount,creditAmount,transactionsNo,userCreated,dtCreated,userModified,dtModified )
                                            VALUES 
                                            (@idAccount, @numberLedgerAccount, @bookingYear, @beginDebit, @beginCredit, @debitAmount,@creditAmount,@transactionsNo,@userCreated,@dtCreated,@userModified,@dtModified)");

            SqlParameter[] sqlParameter = new SqlParameter[12];

            sqlParameter[0] = new SqlParameter("@idAccount", SqlDbType.Int);
            sqlParameter[0].Value = model.idAccount;

            sqlParameter[1] = new SqlParameter("@numberLedgerAccount", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.numberLedgerAccount;

            sqlParameter[2] = new SqlParameter("@bookingYear", SqlDbType.NVarChar);
            sqlParameter[2].Value = model.bookingYear;

            sqlParameter[3] = new SqlParameter("@beginDebit", SqlDbType.Decimal);
            sqlParameter[3].Value = model.beginDebit;

            sqlParameter[4] = new SqlParameter("@beginCredit", SqlDbType.Decimal);
            sqlParameter[4].Value = model.beginCredit;

            sqlParameter[5] = new SqlParameter("@debitAmount", SqlDbType.Decimal);
            sqlParameter[5].Value = model.debitAmount;

            sqlParameter[6] = new SqlParameter("@creditAmount", SqlDbType.Decimal);
            sqlParameter[6].Value = model.creditAmount;

            sqlParameter[7] = new SqlParameter("@transactionsNo", SqlDbType.Int);
            sqlParameter[7].Value = model.transactionsNo;

            sqlParameter[8] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[8].Value = model.userCreated;

            sqlParameter[9] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[9].Value = DateTime.Now; // model.dtCreated;

            sqlParameter[10] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[10].Value = model.userModified;

            sqlParameter[11] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[11].Value = model.dtModified;

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
            sqlParameter[4].Value = model.numberLedgerAccount.ToString() +"_"+ model.bookingYear.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "numberLedgerAccount_bookingYear";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccLedgerAmounts";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


           return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean Update(AccLedgerAmountsModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE AccLedgerAmounts SET idAccount=@idAccount, numberLedgerAccount=@numberLedgerAccount, bookingYear=@bookingYear,beginDebit=@beginDebit, beginCredit=@beginCredit,
                                            debitAmount=@debitAmount,creditAmount=@creditAmount,transactionsNo=@transactionsNo,
                                            userCreated=@userCreated,dtCreated=@dtCreated,userModified=@userModified,dtModified=@dtModified
                                            WHERE numberLedgerAccount=@numberLedgerAccount and bookingYear=@bookingYear");

            SqlParameter[] sqlParameter = new SqlParameter[12];

            sqlParameter[0] = new SqlParameter("@idAccount", SqlDbType.Int);
            sqlParameter[0].Value = model.idAccount;

            sqlParameter[1] = new SqlParameter("@numberLedgerAccount", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.numberLedgerAccount;

            sqlParameter[2] = new SqlParameter("@bookingYear", SqlDbType.NVarChar);
            sqlParameter[2].Value = model.bookingYear;

            sqlParameter[3] = new SqlParameter("@beginDebit", SqlDbType.Decimal);
            sqlParameter[3].Value = model.beginDebit;

            sqlParameter[4] = new SqlParameter("@beginCredit", SqlDbType.Decimal);
            sqlParameter[4].Value = model.beginCredit;

            sqlParameter[5] = new SqlParameter("@debitAmount", SqlDbType.Decimal);
            sqlParameter[5].Value = model.debitAmount;

            sqlParameter[6] = new SqlParameter("@creditAmount", SqlDbType.Decimal);
            sqlParameter[6].Value = model.creditAmount;

            sqlParameter[7] = new SqlParameter("@transactionsNo", SqlDbType.Int);
            sqlParameter[7].Value = model.transactionsNo;

            sqlParameter[8] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[8].Value = model.userCreated;

            sqlParameter[9] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[9].Value = model.dtCreated;

            sqlParameter[10] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[10].Value = model.userModified;

            sqlParameter[11] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[11].Value = DateTime.Now; //model.dtModified;

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
            sqlParameter[4].Value = model.numberLedgerAccount.ToString() + "_" + model.bookingYear.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "numberLedgerAccount_bookingYear";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccLedgerAmounts";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

          
    }
}