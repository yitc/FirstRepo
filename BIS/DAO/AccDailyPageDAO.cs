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
    public class AccDailyPageDAO
    {
        private dbConnection conn;
        public AccDailyPageDAO()
        {
            conn = new dbConnection();

        }
        public int GetLastID()
        {
            return conn.GetLastTableID("AccDailyPage");
        }
        public DataTable GetAllByDaily(string idDaily)
        {
            string query = string.Format(@" SELECT   idAccPage, codeDaily, refNumber, pageDate,  beginSaldo,  endSaldo,
                                        debitPage,creditPage,typeDaily,descPage,periodPage 
                                        FROM AccDailyPage  WHERE codeDaily='" + idDaily.ToString() + "' ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllPages()
        {
            string query = string.Format(@"  SELECT   idAccPage, codeDaily, refNumber, pageDate,  beginSaldo,  endSaldo,
                                        debitPage,creditPage,typeDaily,descPage,periodPage 
                                         FROM AccDailyPage  ");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetLastPage(string idDaily)
        {
            string query = string.Format(@"SELECT  TOP 1 idAccPage, codeDaily, refNumber, pageDate
                                        FROM AccDailyPage where codeDaily = '"+idDaily+"' order by codedaily, refNo DESC ");

            return conn.executeSelectQuery(query, null);
        }

        public int SaveAndReturnID(AccDailyPageModel linemodel)
        {
            string query = string.Format(@"INSERT INTO  AccDailyPage ( codeDaily, refNumber, pageDate,  beginSaldo,  endSaldo,
                                debitPage,creditPage,typeDaily,descPage,periodPage )
                                 Values ( @codeDaily, @refNumber, @pageDate,  @beginSaldo,  @endSaldo,
                                 @debitPage,@creditPage,@typeDaily,@descPage,@periodPage); SELECT SCOPE_IDENTITY()");


            SqlParameter[] sqlParameters = new SqlParameter[10];

            sqlParameters[0] = new SqlParameter("@codeDaily", SqlDbType.NVarChar);
            sqlParameters[0].Value = linemodel.codeDaily;

            sqlParameters[1] = new SqlParameter("@refNumber", SqlDbType.Int);
            sqlParameters[1].Value = linemodel.refNumber;

            sqlParameters[2] = new SqlParameter("@pageDate", SqlDbType.Date);
            sqlParameters[2].Value = linemodel.pageDate;

            sqlParameters[3] = new SqlParameter("@beginSaldo", SqlDbType.Decimal);
            sqlParameters[3].Value = linemodel.beginSaldo;

            sqlParameters[4] = new SqlParameter("@endSaldo", SqlDbType.Decimal);
            sqlParameters[4].Value = linemodel.endSaldo;

            sqlParameters[5] = new SqlParameter("@debitPage", SqlDbType.Decimal);
            sqlParameters[5].Value = linemodel.debitPage;

            sqlParameters[6] = new SqlParameter("@creditPage", SqlDbType.Decimal);
            sqlParameters[6].Value = linemodel.creditPage;

            sqlParameters[7] = new SqlParameter("@typeDaily", SqlDbType.Int);
            sqlParameters[7].Value = linemodel.typeDaily;

            sqlParameters[8] = new SqlParameter("@descPage", SqlDbType.NVarChar);
            sqlParameters[8].Value = linemodel.descPage;

            sqlParameters[9] = new SqlParameter("@periodPage", SqlDbType.Int);
            sqlParameters[9].Value = linemodel.periodPage;

            return conn.executeInsertQuerySelectLastID(query, sqlParameters);

        }

        public bool Update(AccDailyPageModel linemodel)
        {
            string query = string.Format(@"UPDATE AccDailyPage SET  codeDaily=@codeDaily,refNumber=@refNumber,
                                        pageDate=@pageDate, beginSaldo=@beginSaldo,endSaldo=@endSaldo,
                                        debitPage=@debitPage, creditPage=@creditPage,typeDaily=@typeDaily,
                                        descPage=@descPage, periodPage=@periodPage
                                        WHERE idAccPage=@idAccPage ");


            SqlParameter[] sqlParameters = new SqlParameter[11];

            sqlParameters[0] = new SqlParameter("@codeDaily", SqlDbType.NVarChar);
            sqlParameters[0].Value = linemodel.codeDaily;

            sqlParameters[1] = new SqlParameter("@refNumber", SqlDbType.Int);
            sqlParameters[1].Value = linemodel.refNumber;

            sqlParameters[2] = new SqlParameter("@pageDate", SqlDbType.Date);
            sqlParameters[2].Value = linemodel.pageDate;

            sqlParameters[3] = new SqlParameter("@beginSaldo", SqlDbType.Decimal);
            sqlParameters[3].Value = linemodel.beginSaldo;

            sqlParameters[4] = new SqlParameter("@endSaldo", SqlDbType.Decimal);
            sqlParameters[4].Value = linemodel.endSaldo;

            sqlParameters[5] = new SqlParameter("@debitPage", SqlDbType.Decimal);
            sqlParameters[5].Value = linemodel.debitPage;

            sqlParameters[6] = new SqlParameter("@creditPage", SqlDbType.Decimal);
            sqlParameters[6].Value = linemodel.creditPage;

            sqlParameters[7] = new SqlParameter("@typeDaily", SqlDbType.Int);
            sqlParameters[7].Value = linemodel.typeDaily;

            sqlParameters[8] = new SqlParameter("@descPage", SqlDbType.NVarChar);
            sqlParameters[8].Value = linemodel.descPage;

            sqlParameters[9] = new SqlParameter("@periodPage", SqlDbType.Int);
            sqlParameters[9].Value = linemodel.periodPage;


            sqlParameters[10] = new SqlParameter("@idAccPage", SqlDbType.Int);
            sqlParameters[10].Value = linemodel.idAccPage;



            return conn.executeUpdateQuery(query, sqlParameters);

        }
        public bool UpdateCodeDaily(string codeDaily, int id)
        {
            string query = string.Format(@"UPDATE AccDailyBank SET codeDaily=@codeDaily WHERE idDailyBank=@idDailyBank ");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@codeDaily", SqlDbType.NVarChar);
            sqlParameters[0].Value = codeDaily;

            sqlParameters[1] = new SqlParameter("@idDailyBank", SqlDbType.Int);
            sqlParameters[1].Value = id;

            return conn.executeUpdateQuery(query, sqlParameters);
        }

        public bool UpdateRefNo(string refNo, int id)
        {
            string query = string.Format(@"UPDATE AccDailyBank SET refNo=@refNo WHERE idDailyBank=@idDailyBank ");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@refNo", SqlDbType.NVarChar);
            sqlParameters[0].Value = refNo;

            sqlParameters[1] = new SqlParameter("@idDailyBank", SqlDbType.Int);
            sqlParameters[1].Value = id;

            return conn.executeUpdateQuery(query, sqlParameters);
        }

        public bool UpdateDtStatement(DateTime date, int id)
        {
            string query = string.Format(@"UPDATE AccDailyBank SET dtStatement=@dtStatement WHERE idDailyBank=@idDailyBank ");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@dtStatement", SqlDbType.DateTime);
            sqlParameters[0].Value = date;

            sqlParameters[1] = new SqlParameter("@idDailyBank", SqlDbType.Int);
            sqlParameters[1].Value = id;

            return conn.executeUpdateQuery(query, sqlParameters);
        }

        public bool UpdateBegSaldo(decimal begSaldo, int id)
        {
            string query = string.Format(@"UPDATE AccDailyBank SET begSaldo=@begSaldo WHERE idDailyBank=@idDailyBank ");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@begSaldo", SqlDbType.Decimal);
            sqlParameters[0].Value = begSaldo;

            sqlParameters[1] = new SqlParameter("@idDailyBank", SqlDbType.Int);
            sqlParameters[1].Value = id;

            return conn.executeUpdateQuery(query, sqlParameters);
        }
        public bool UpdateEndSaldo(decimal endSaldo, int id)
        {
            string query = string.Format(@"UPDATE AccDailyBank SET endSaldo=@endSaldo WHERE idDailyBank=@idDailyBank ");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@endSaldo", SqlDbType.Decimal);
            sqlParameters[0].Value = endSaldo;

            sqlParameters[1] = new SqlParameter("@idDailyBank", SqlDbType.Int);
            sqlParameters[1].Value = id;

            return conn.executeUpdateQuery(query, sqlParameters);
        }
        public bool UpdateBankKas(int bankKas, int id)
        {
            string query = string.Format(@"UPDATE AccDailyBank SET bankKas=@bankKas WHERE idDailyBank=@idDailyBank ");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@bankKas", SqlDbType.Int);
            sqlParameters[0].Value = bankKas;

            sqlParameters[1] = new SqlParameter("@idDailyBank", SqlDbType.Int);
            sqlParameters[1].Value = id;

            return conn.executeUpdateQuery(query, sqlParameters);
        }
     
    
    }
}