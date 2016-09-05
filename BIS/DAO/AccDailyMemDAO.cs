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
    public class AccDailyMemDAO
    {
        private dbConnection conn;
        public string bookYear;

        public AccDailyMemDAO(string bookyear)
        {
            conn = new dbConnection();
            this.bookYear = bookyear;
        }

        public DataTable GetMemoById(int id)
        {
            string query = string.Format(@" SELECT   idDailyMem, codeDaily, refno, dtMem , bookingYear,beginPeriod ,userCreated,dtCreated,userModified,dtModified
                                        FROM AccDailyMem  WHERE codeDaily='" + id.ToString() + "' and bookingYear = '"+bookYear+"' ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetMemoByIdWithDebitCredit(int id)
        {
            string query = string.Format(@"SELECT  dm.idDailyMem, dm.codeDaily, dm.refno, dm.dtMem , dm.bookingYear, dm.beginPeriod, ISNULL(sum(acl.debitLine),0) as debit, ISNULL(sum(acl.creditLine),0) as credit,
                 dm.userCreated,dm.dtCreated,dm.userModified,dm.dtModified
                 FROM AccDailyMem  dm 
                 LEFT OUTER JOIN AccLine acl ON dm.idDailyMem = acl.idCurrency
                 WHERE dm.codeDaily='" + id.ToString() +"' and dm.bookingYear = '" + bookYear + @"' 
                 GROUP BY dm.idDailyMem, dm.codeDaily, dm.refno, dm.dtMem , dm.bookingYear, dm.beginPeriod, dm.userCreated,dm.dtCreated,dm.userModified,dm.dtModified");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllByDaily(string idDaily)
        {
            string query = string.Format(@"SELECT  dm.idDailyMem, dm.codeDaily, dm.refno, dm.dtMem , dm.bookingYear, dm.beginPeriod, ISNULL(sum(acl.debitLine),0) as debit, ISNULL(sum(acl.creditLine),0) as credit,
                dm.userCreated,dm.dtCreated,dm.userModified,dm.dtModified
                FROM AccDailyMem  dm 
                LEFT OUTER JOIN AccLine acl ON dm.idDailyMem = acl.idCurrency
                WHERE dm.codeDaily='" + idDaily + "' and dm.bookingYear = '" + bookYear + @"'
                GROUP BY dm.idDailyMem, dm.codeDaily, dm.refno, dm.dtMem , dm.bookingYear, dm.beginPeriod, dm.userCreated,dm.dtCreated,dm.userModified,dm.dtModified");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetLastMemByStatement(string code)
        {
            string query = string.Format(@" SELECT  TOP 1 idDailyMem, codeDaily, refNo, dtMem , bookingYear,beginPeriod ,userCreated,dtCreated,userModified,dtModified
                                        FROM AccDailyMem WHERE codeDaily = @code and bookingYear = '" + bookYear + "'  ORDER BY refNo DESC  ");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@code", SqlDbType.NVarChar);
            sqlParameters[0].Value = code;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public bool Delete(int id, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  AccDailyMem WHERE idDailyMem = @idDailyMem and bookingYear = '" + bookYear + "' ");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idDailyMem", SqlDbType.Int);
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
            sqlParameter[4].Value = id.ToString() + "_" + bookYear.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idDailyMem_bookingYear";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccDailyMem";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool Delete2(int id, int refno, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM AccDailyMem WHERE idDailyMem = @idDailyMem AND refno=@refno AND bookingYear = '" + bookYear + "' ");

            SqlParameter[] sqlParameter = new SqlParameter[2];

            sqlParameter[0] = new SqlParameter("@idDailyMem", SqlDbType.Int);
            sqlParameter[0].Value = id;

            sqlParameter[1] = new SqlParameter("@refno", SqlDbType.Int);
            sqlParameter[1].Value = refno;

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
            sqlParameter[4].Value = id.ToString() + "_" + bookYear.ToString() + "_" + refno.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idDailyMem_bookingYear_refno";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccDailyMem";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete2";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public int SaveAndReturnID(AccDailyMemModel linemodel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO  AccDailyMem ( codeDaily, refno, dtMem, bookingYear,beginPeriod,userCreated,dtCreated,userModified,dtModified)
                                 Values ( @codeDaily, @refno, @dtMem,@bookingYear,@beginPeriod,@userCreated,@dtCreated,@userModified,@dtModified); SELECT SCOPE_IDENTITY() ");


            SqlParameter[] sqlParameter = new SqlParameter[9];

            sqlParameter[0] = new SqlParameter("@codeDaily", SqlDbType.NVarChar);
            sqlParameter[0].Value = linemodel.codeDaily;

            sqlParameter[1] = new SqlParameter("@refno", SqlDbType.Int);
            sqlParameter[1].Value = linemodel.refNo;

            sqlParameter[2] = new SqlParameter("@dtMem", SqlDbType.Date);
            sqlParameter[2].Value = linemodel.dtMem;

            sqlParameter[3] = new SqlParameter("@bookingYear", SqlDbType.NVarChar);
            sqlParameter[3].Value = linemodel.bookingYear;

            sqlParameter[4] = new SqlParameter("@beginPeriod", SqlDbType.Bit);
            sqlParameter[4].Value = linemodel.beginPeriod;

            sqlParameter[5] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[5].Value = linemodel.userCreated;

            sqlParameter[6] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[6].Value = DateTime.Now; // model.dtCreated;

            sqlParameter[7] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[7].Value = linemodel.userModified;

            sqlParameter[8] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[8].Value = linemodel.dtModified;

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
            sqlParameter[4].Value = conn.GetLastTableID("AccDailyMem") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idDailyMem";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccDailyMem";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save and return id";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransactionSelectLastID(_query, sqlParameters);

        }

        public bool Update(AccDailyMemModel linemodel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE AccDailyMem SET codeDaily=@codeDaily, refno=@refno, dtMem=@dtMem, bookingYear=@bookingYear,beginPeriod=@beginPeriod, 
                                        userCreated=@userCreated,dtCreated=@dtCreated,userModified=@userModified,dtModified=@dtModified
                                        WHERE idDailyMem=@idDailyMem and bookingYear = '" + bookYear + "'");


            SqlParameter[] sqlParameter = new SqlParameter[10];

            sqlParameter[0] = new SqlParameter("@codeDaily", SqlDbType.NVarChar);
            sqlParameter[0].Value = linemodel.codeDaily;

            sqlParameter[1] = new SqlParameter("@refno", SqlDbType.Int);
            sqlParameter[1].Value = linemodel.refNo;

            sqlParameter[2] = new SqlParameter("@dtMem", SqlDbType.Date);
            sqlParameter[2].Value = linemodel.dtMem;

            sqlParameter[3] = new SqlParameter("@idDailyMem", SqlDbType.Int);
            sqlParameter[3].Value = linemodel.idDailyMem;

            sqlParameter[4] = new SqlParameter("@bookingYear", SqlDbType.NVarChar);
            sqlParameter[4].Value = linemodel.bookingYear;

            sqlParameter[5] = new SqlParameter("@beginPeriod", SqlDbType.Bit);
            sqlParameter[5].Value = linemodel.beginPeriod;

            sqlParameter[6] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[6].Value = linemodel.userCreated;

            sqlParameter[7] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[7].Value = linemodel.dtCreated;

            sqlParameter[8] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[8].Value = linemodel.userModified;

            sqlParameter[9] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[9].Value = DateTime.Now;//model.dtModified;


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
            sqlParameter[4].Value = linemodel.idDailyMem.ToString() + "_" + bookYear.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idDailyMem_bookingYear";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccDailyMem";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
    }
}
