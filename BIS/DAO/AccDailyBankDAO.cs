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
    public class AccDailyBankDAO
    {
        private dbConnection conn;
        public string bookYear;

        public AccDailyBankDAO(string bookyear)
        {
            conn = new dbConnection();
            this.bookYear = bookyear;

        }
        public    DataTable GetAllByDaily(string idDaily)
        {
            string query = string.Format(@" SELECT   idDailyBank, codeDaily, refNo, dtStatement,  begSaldo, CAST(0 as decimal(10,2)) as difference,  endSaldo, CAST(0 as decimal(10,2)) as Booked, pdfFile, bookingYear
                                        ,userCreated,dtCreated,userModified,dtModified
                                        FROM AccDailyBank  WHERE codeDaily='" + idDaily.ToString() + "' and bookingYear = '" + bookYear + "' ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetLastBank(string iddaily)
        {
            string query = string.Format(@" SELECT  TOP 1 idDailyBank, codeDaily, refNo, dtStatement,  begSaldo,   endSaldo,pdfFile,userCreated,dtCreated,userModified,dtModified 
                                        FROM AccDailyBank WHERE codeDaily= '" + iddaily + "' and bookingYear = '" + bookYear + "' ORDER BY idDailyBank DESC  ");

            return conn.executeSelectQuery(query, null);
        }

        public bool Delete(int id, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  AccDailyBank WHERE idDailyBank = @idDailyBank and bookingYear = '" + bookYear + "'");

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
            sqlParameter[5].Value = "idDailyBank";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccDailyBank";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public int SaveAndReturnID(AccDailyBankModel linemodel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO  AccDailyBank ( codeDaily, refNo, dtStatement,  begSaldo,  endSaldo,pdfFile, bookingYear,userCreated,dtCreated,userModified,dtModified )
                                 Values ( @codeDaily, @refNo, @dtStatement,  @begSaldo,  @endSaldo,@pdfFile, @bookingYear,@userCreated,@dtCreated,@userModified,@dtModified ); SELECT SCOPE_IDENTITY()");


            SqlParameter[] sqlParameter = new SqlParameter[11];

            sqlParameter[0] = new SqlParameter("@codeDaily", SqlDbType.NVarChar);
            sqlParameter[0].Value = linemodel.codeDaily;

            sqlParameter[1] = new SqlParameter("@refNo", SqlDbType.Int);
            sqlParameter[1].Value = linemodel.refNo;

            sqlParameter[2] = new SqlParameter("@dtStatement", SqlDbType.Date);
            sqlParameter[2].Value = linemodel.dtStatement;

            sqlParameter[3] = new SqlParameter("@begSaldo", SqlDbType.Decimal);
            sqlParameter[3].Value = linemodel.begSaldo;

            sqlParameter[4] = new SqlParameter("@endSaldo", SqlDbType.Decimal);
            sqlParameter[4].Value = linemodel.endSaldo;

            sqlParameter[5] = new SqlParameter("@pdfFile", SqlDbType.NVarChar);
            sqlParameter[5].Value = linemodel.pdfFile;

            sqlParameter[6] = new SqlParameter("@bookingYear", SqlDbType.NVarChar);
            sqlParameter[6].Value = linemodel.bookingYear;

            sqlParameter[7] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[7].Value = linemodel.userCreated;

            sqlParameter[8] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[8].Value = DateTime.Now; // linemodel.dtCreated;

            sqlParameter[9] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[9].Value = linemodel.userModified;

            sqlParameter[10] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[10].Value = linemodel.dtModified;

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
            sqlParameter[4].Value = conn.GetLastTableID("AccDailyBank") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idDailyBank";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccCreditLine";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save and return last ID";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransactionSelectLastID(_query, sqlParameters);

        }

        public bool Save(AccDailyBankModel linemodel,string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO  AccDailyBank ( codeDaily, refNo, dtStatement,  begSaldo,  endSaldo,pdfFile, bookingYear,userCreated,dtCreated,userModified,dtModified )
                                 Values ( @codeDaily, @refNo, @dtStatement,  @begSaldo,  @endSaldo,@pdfFile, @bookingYear,@userCreated,@dtCreated,@userModified,@dtModified )");


            SqlParameter[] sqlParameter = new SqlParameter[11];

            sqlParameter[0] = new SqlParameter("@codeDaily", SqlDbType.NVarChar);
            sqlParameter[0].Value = linemodel.codeDaily;

            sqlParameter[1] = new SqlParameter("@refNo", SqlDbType.Int);
            sqlParameter[1].Value = linemodel.refNo;

            sqlParameter[2] = new SqlParameter("@dtStatement", SqlDbType.Date);
            sqlParameter[2].Value = linemodel.dtStatement;

            sqlParameter[3] = new SqlParameter("@begSaldo", SqlDbType.Decimal);
            sqlParameter[3].Value = linemodel.begSaldo;

            sqlParameter[4] = new SqlParameter("@endSaldo", SqlDbType.Decimal);
            sqlParameter[4].Value = linemodel.endSaldo;

            sqlParameter[5] = new SqlParameter("@pdfFile", SqlDbType.NVarChar);
            sqlParameter[5].Value = linemodel.pdfFile;

            sqlParameter[6] = new SqlParameter("@bookingYear", SqlDbType.NVarChar);
            sqlParameter[6].Value = linemodel.bookingYear;

            sqlParameter[7] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[7].Value = linemodel.userCreated;

            sqlParameter[8] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[8].Value = DateTime.Now;  //linemodel.dtCreated;

            sqlParameter[9] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[9].Value = linemodel.userModified;

            sqlParameter[10] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[10].Value = linemodel.dtModified;

            //sqlParameters[5] = new SqlParameter("@bankKas", SqlDbType.Int);
            //sqlParameters[5].Value = linemodel.bankKas;

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
            sqlParameter[4].Value = conn.GetLastTableID("AccDailyBank") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idDailyBank";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccDailyBank";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";


            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public bool Update(AccDailyBankModel linemodel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE AccDailyBank SET  codeDaily=@codeDaily,refNo=@refNo,
                                        dtStatement=@dtStatement, begSaldo=@begSaldo,endSaldo=@endSaldo,pdfFile=@pdfFile, bookingYear=@bookingYear,
                                        userCreated=@userCreated,dtCreated=@dtCreated,userModified=@userModified,dtModified=@dtModified
                                        WHERE idDailyBank=@idDailyBank and bookingYear = '" + bookYear + "'");

            
            SqlParameter[] sqlParameter = new SqlParameter[12];

            sqlParameter[0] = new SqlParameter("@codeDaily", SqlDbType.NVarChar);
            sqlParameter[0].Value = linemodel.codeDaily;

            sqlParameter[1] = new SqlParameter("@refNo", SqlDbType.Int);
            sqlParameter[1].Value = linemodel.refNo;

            sqlParameter[2] = new SqlParameter("@dtStatement", SqlDbType.Date);
            sqlParameter[2].Value = linemodel.dtStatement;

            sqlParameter[3] = new SqlParameter("@begSaldo", SqlDbType.Decimal);
            sqlParameter[3].Value = linemodel.begSaldo;

            sqlParameter[4] = new SqlParameter("@endSaldo", SqlDbType.Decimal);
            sqlParameter[4].Value = linemodel.endSaldo;


            sqlParameter[5] = new SqlParameter("@idDailyBank", SqlDbType.Int);
            sqlParameter[5].Value = linemodel.idDailyBank;

            sqlParameter[6] = new SqlParameter("@pdfFile", SqlDbType.NVarChar);
            sqlParameter[6].Value = linemodel.pdfFile;

            sqlParameter[7] = new SqlParameter("@bookingYear", SqlDbType.NVarChar);
            sqlParameter[7].Value = linemodel.bookingYear;

            sqlParameter[8] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[8].Value = linemodel.userCreated;

            sqlParameter[9] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[9].Value = linemodel.dtCreated;

            sqlParameter[10] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[10].Value = linemodel.userModified;

            sqlParameter[11] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[11].Value = DateTime.Now; //linemodel.dtModified;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,description)");

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
            sqlParameter[4].Value = linemodel.idDailyBank.ToString() + "_" + bookYear.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idDailyBank_bookingYear";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccDailyBank";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
    }
}