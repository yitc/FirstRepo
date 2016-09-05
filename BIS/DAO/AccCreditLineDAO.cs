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
    public class AccCreditLineDAO
    {
        private dbConnection conn;
        public AccCreditLineDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetLine(int idAccDaily)
            

        {
            string query = string.Format(@" SELECT   idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,l.descLedgerAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate,incopNr,iban, idMaster,idDetail,userCreated, dtCreated,userModified,dtModified
                                        FROM AccCreditLine  
                                        RIGHT JOIN AccLedgerAccount l on l.numberLedgerAccount = numberLedAccount
                                        WHERE idAccDaily='" + idAccDaily.ToString() + "'  order by booksort");
            return conn.executeSelectQuery(query, null);
        }

        //provera radi brisanja

        public bool Delete(int id, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM AccCreditLine WHERE idAccLine = @idAccLine");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idAccLine", SqlDbType.Int);
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
            sqlParameter[5].Value = "idAccLine";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccCreditLine";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";
            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteDaily(int id, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM AccCreditLine WHERE idAccDaily = @idAccLine");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idAccLine", SqlDbType.Int);
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
            sqlParameter[5].Value = "idAccDaily";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccCreditLine";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool Save(AccLineModel linemodel,string nameForm,int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO  AccCreditLine ( idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine, debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate, idProjectLine,incopNr,iban,idMaster,idDetail,
                                        userCreated, dtCreated, userModified, dtModified)
                                 Values ( @idAccDaily,@statusLine,@periodLine, @dtLine,@numberLedAccount,@invoiceNr,@descLine,
                                        @idClientLine,@idPersonLine,@idCostLine, @debitLine,@creditLine,@idBTW,@debitBTW,@creditBTW,
                                        @idCurrency,@debitCurr, @creditCurr,@dtBooking, @booksort,@currrate,@idProjectLine,@incopNr,@iban,@idMaster,@idDetail,
                                        @userCreated, @dtCreated, @userModified, @dtModified)");


            SqlParameter[] sqlParameter = new SqlParameter[30];

            sqlParameter[0] = new SqlParameter("@idAccDaily", SqlDbType.Int);
            sqlParameter[0].Value = linemodel.idAccDaily;

            sqlParameter[1] = new SqlParameter("@statusLine", SqlDbType.Bit);
            sqlParameter[1].Value = linemodel.statusLine;

            sqlParameter[2] = new SqlParameter("@periodLine", SqlDbType.Int);
            sqlParameter[2].Value = linemodel.periodLine;

            sqlParameter[3] = new SqlParameter("@dtLine", SqlDbType.DateTime);
            sqlParameter[3].Value = (linemodel.dtLine == null || linemodel.dtLine == DateTime.MinValue) ? SqlDateTime.Null : linemodel.dtLine;



            sqlParameter[4] = new SqlParameter("@numberLedAccount", SqlDbType.NVarChar);
            sqlParameter[4].Value = (linemodel.numberLedAccount == null || linemodel.numberLedAccount == "") ? String.Empty : linemodel.numberLedAccount; ;

            sqlParameter[5] = new SqlParameter("@invoiceNr", SqlDbType.NVarChar);
            sqlParameter[5].Value = (linemodel.invoiceNr == null || linemodel.invoiceNr == "") ? String.Empty : linemodel.invoiceNr;

            sqlParameter[6] = new SqlParameter("@descLine", SqlDbType.NVarChar);
            sqlParameter[6].Value = (linemodel.descLine == null || linemodel.descLine == "") ? String.Empty : linemodel.descLine;

            sqlParameter[7] = new SqlParameter("@idClientLine", SqlDbType.NVarChar);
            sqlParameter[7].Value = (linemodel.idClientLine == null || linemodel.idClientLine == "") ? String.Empty : linemodel.idClientLine;

            sqlParameter[8] = new SqlParameter("@idPersonLine", SqlDbType.NVarChar);
            sqlParameter[8].Value = (linemodel.idPersonLine == null || linemodel.idPersonLine == "") ? String.Empty : linemodel.idPersonLine;

            sqlParameter[9] = new SqlParameter("@idCostLine", SqlDbType.NVarChar);
            sqlParameter[9].Value = (linemodel.idCostLine == null || linemodel.idCostLine == "") ? String.Empty : linemodel.idCostLine;

            //sqlParameters[10] = new SqlParameter("@idProjectLine", SqlDbType.NVarChar);
            //sqlParameters[10].Value = linemodel.idProjectLine;

            sqlParameter[10] = new SqlParameter("@debitLine", SqlDbType.Decimal);
            sqlParameter[10].Value = linemodel.debitLine;

            sqlParameter[11] = new SqlParameter("@creditLine", SqlDbType.Decimal);
            sqlParameter[11].Value = linemodel.creditLine;

            sqlParameter[12] = new SqlParameter("@idBTW", SqlDbType.Int);
            sqlParameter[12].Value = linemodel.idBTW;

            sqlParameter[13] = new SqlParameter("@debitBTW", SqlDbType.Decimal);
            sqlParameter[13].Value = linemodel.debitBTW;

            sqlParameter[14] = new SqlParameter("@creditBTW", SqlDbType.Decimal);
            sqlParameter[14].Value = linemodel.creditBTW;

            sqlParameter[15] = new SqlParameter("@idCurrency", SqlDbType.Int);
            sqlParameter[15].Value = linemodel.idCurrency;

            sqlParameter[16] = new SqlParameter("@debitCurr", SqlDbType.Decimal);
            sqlParameter[16].Value = linemodel.debitCurr;

            sqlParameter[17] = new SqlParameter("@creditCurr", SqlDbType.Decimal);
            sqlParameter[17].Value = linemodel.creditCurr;

            sqlParameter[18] = new SqlParameter("@dtBooking", SqlDbType.DateTime);
            sqlParameter[18].Value = linemodel.dtBooking;

            sqlParameter[19] = new SqlParameter("@booksort", SqlDbType.Int);
            sqlParameter[19].Value = linemodel.booksort;

            sqlParameter[20] = new SqlParameter("@currrate", SqlDbType.Decimal);
            sqlParameter[20].Value = linemodel.currrate;

            sqlParameter[21] = new SqlParameter("@idProjectLine", SqlDbType.NVarChar);
            sqlParameter[21].Value = (linemodel.idProjectLine == null || linemodel.idProjectLine == "") ? String.Empty : linemodel.idProjectLine; ;

            sqlParameter[22] = new SqlParameter("@incopNr", SqlDbType.NVarChar);
            sqlParameter[22].Value = linemodel.incopNr ;

            sqlParameter[23] = new SqlParameter("@iban", SqlDbType.NVarChar);
            sqlParameter[23].Value = linemodel.iban;

            sqlParameter[24] = new SqlParameter("@idMaster", SqlDbType.NVarChar);
            sqlParameter[24].Value = linemodel.idMaster;

            sqlParameter[25] = new SqlParameter("@idDetail", SqlDbType.NVarChar);
            sqlParameter[25].Value = linemodel.idDetail;

            sqlParameter[26] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[26].Value = linemodel.userCreated;

            sqlParameter[27] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[27].Value = linemodel.dtCreated;

            sqlParameter[28] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[28].Value = linemodel.userModified;

            sqlParameter[29] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[29].Value = linemodel.dtModified;

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
            sqlParameter[4].Value = conn.GetLastTableID("AccCreditLine") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAccLine";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccCreditLine";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);



            return conn.executQueryTransaction(_query, sqlParameters);

        }
    }
}