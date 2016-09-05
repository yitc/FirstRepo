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
    public class AccPageDAO
    {
        private dbConnection conn;
        public AccPageDAO()
        {
            conn = new dbConnection();

        }
      



        public DataTable GetAllPages(int idDaily)
        {
            string query = string.Format(@" SELECT idAccPage,idDaily,codeDaily,numberPage, periodPage,descPage, prevDebAmtPage,prevCreAmtPage,
                                prevDVatPage,prevCVatPage,amountDebPage,amountCrePage,vatDebPage,vatCrePage,statusPage
                                FROM AccDaily  WHERE idDaily = @idDaily order by periodPage,numberPage");
                                //LEFT OUTER JOIN  AccDailyType     c on acd.idDailyType = c.idDailyType
                                //LEFT OUTER JOIN  AccLedgerAccount d on acd.numberLedgerAccount = d.numberLedgerAccount
                                //LEFT OUTER JOIN  AccDailyVerIn    e on acd.idDailyVerIn = e.idDailyVerIn
                                //order by codeDaily");

            // nameBank ostavljeno za kasnije kad bude tabele


            //SELECT idDaily,codeDaily,descDaily,idDailyType, c.descDailyType,numberLedgerAccount, d.descLedgerAccount
            //                   idBank,nameBank,ibanBank,isLocked,idDailyVerIn, e.nameDailyVerIn
            //                   FROM AccDaily  acd
            //                   LEFT OUTER JOIN  AccDailyType     c on acd.idDailyType = c.idDailyType
            //                   LEFT OUTER JOIN  AccLedgerAccount d on acd.numberLedgerAccount = d.numberLedgerAccount
            //                   LEFT OUTER JOIN  AccDailyVerIn    e on acd.idDailyVerIn = e.idDailyVerIn
            //                   order by codeDaily 



            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllPageByID(int idAccPage)
        {
            string query = string.Format(@" SELECT idAccPage,idDaily,codeDaily,numberPage, periodPage,descPage, prevDebAmtPage,prevCreAmtPage,
                                prevDVatPage,prevCVatPage,amountDebPage,amountCrePage,vatDebPage,vatCrePage,statusPage
                                FROM AccDaily  WHERE idAccPage = @idAccPage ");
        


            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetPageNewID(int idDaily, int numberPage)
        {
            string query = string.Format(@" SELECT idAccPage,idDaily,codeDaily,numberPage, periodPage,descPage, prevDebAmtPage,prevCreAmtPage,
                                prevDVatPage,prevCVatPage,amountDebPage,amountCrePage,vatDebPage,vatCrePage,statusPage
                                FROM AccDaily  WHERE idDaily = @idDaily and numberPage=@numberPage");
        

            return conn.executeSelectQuery(query, null);
        }
        public bool Delete(int id)
        {
            string query = string.Format(@"DELETE FROM  AccPage WHERE idAccPage = @idAccPage");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idAccPage", SqlDbType.Int);
            sqlParameters[0].Value = id;

            return conn.executeDeleteQuery(query, sqlParameters);
        }
        public bool Save(AccPageModel pagemodel)
        {
            

            string query = string.Format(@"INSERT INTO  AccPage (idDaily,codeDaily,numberPage,periodPage,descPage,prevDebAmtPage,
                                            prevCreAmtPage,prevDVatPage,prevCVatPage,amountDebPage, amountCrePage,
                                            vatDebPage,vatCrePage,statusPage  )
                                 Values ( @idDaily,@codeDaily,@numberPage,@periodPage,@descPage,@prevDebAmtPage,
                                            @prevCreAmtPage,@prevDVatPage,@prevCVatPage,@amountDebPage, @amountCrePage,
                                            @vatDebPage,@vatCrePage,@statusPage )");


            SqlParameter[] sqlParameters = new SqlParameter[14];

            sqlParameters[0] = new SqlParameter("@idDaily", SqlDbType.Int);
            sqlParameters[0].Value = pagemodel.idDaily;

            sqlParameters[1] = new SqlParameter("@codeDaily", SqlDbType.NVarChar);
            sqlParameters[1].Value = (pagemodel.codeDaily == null) ? SqlString.Null : pagemodel.codeDaily;

            sqlParameters[2] = new SqlParameter("@numberPage", SqlDbType.Int);
            sqlParameters[2].Value = pagemodel.numberPage;

            sqlParameters[3] = new SqlParameter("@periodPage", SqlDbType.Int);
            sqlParameters[3].Value = pagemodel.periodPage;

            sqlParameters[4] = new SqlParameter("@descPage", SqlDbType.NVarChar);
            sqlParameters[4].Value = pagemodel.descPage;

            sqlParameters[5] = new SqlParameter("@prevDebAmtPage", SqlDbType.Decimal);
            sqlParameters[5].Value = pagemodel.prevDebAmtPage;

            sqlParameters[6] = new SqlParameter("@prevCreAmtPage", SqlDbType.Decimal);
            sqlParameters[6].Value = pagemodel.prevCreAmtPage;

            sqlParameters[7] = new SqlParameter("@prevDVatPage", SqlDbType.Decimal);
            sqlParameters[7].Value = pagemodel.prevDVatPage;

            sqlParameters[8] = new SqlParameter("@prevCVatPage", SqlDbType.Decimal);
            sqlParameters[8].Value = pagemodel.prevCVatPage;

            sqlParameters[9] = new SqlParameter("@amountDebPage", SqlDbType.Decimal);
            sqlParameters[9].Value = pagemodel.amountDebPage;

            sqlParameters[10] = new SqlParameter("@amountCrePage", SqlDbType.Decimal);
            sqlParameters[10].Value = pagemodel.amountCrePage;

            sqlParameters[11] = new SqlParameter("@vatDebPage", SqlDbType.Decimal);
            sqlParameters[11].Value = pagemodel.vatDebPage;

            sqlParameters[12] = new SqlParameter("@vatCrePage", SqlDbType.Decimal);
            sqlParameters[12].Value = pagemodel.vatCrePage;

            sqlParameters[13] = new SqlParameter("@statusPage", SqlDbType.Bit);
            sqlParameters[13].Value = pagemodel.statusPage;


            return conn.executeUpdateQuery(query, sqlParameters);

        }
        public bool Update(AccPageModel pagemodel)
        {
            string query = string.Format(@"UPDATE AccPage SET   idDaily=@idDaily,codeDaily=@codeDaily,numberPage=@numberPage,
                                        periodPage=@periodPage,descPage=@descPage,prevDebAmtPage=@prevDebAmtPage,
                                        prevCreAmtPage=@prevCreAmtPage,prevDVatPage=@prevDVatPage,prevCVatPage=@prevCVatPage,
                                        amountDebPage=@amountDebPage, amountCrePage=@amountCrePage,vatDebPage=@vatDebPage,vatCrePage=@vatCrePage,
                                        statusPage=@statusPage WHERE idAccPage=@idAccPage ");



            SqlParameter[] sqlParameters = new SqlParameter[15];

            sqlParameters[0] = new SqlParameter("@idDaily", SqlDbType.Int);
            sqlParameters[0].Value = pagemodel.idDaily;

            sqlParameters[1] = new SqlParameter("@codeDaily", SqlDbType.NVarChar);
            sqlParameters[1].Value = (pagemodel.codeDaily == null) ? SqlString.Null : pagemodel.codeDaily;

            sqlParameters[2] = new SqlParameter("@numberPage", SqlDbType.Int);
            sqlParameters[2].Value = pagemodel.numberPage;

            sqlParameters[3] = new SqlParameter("@periodPage", SqlDbType.Int);
            sqlParameters[3].Value = pagemodel.periodPage;

            sqlParameters[4] = new SqlParameter("@descPage", SqlDbType.NVarChar);
            sqlParameters[4].Value = pagemodel.descPage;

            sqlParameters[5] = new SqlParameter("@prevDebAmtPage", SqlDbType.Decimal);
            sqlParameters[5].Value = pagemodel.prevDebAmtPage;

            sqlParameters[6] = new SqlParameter("@prevCreAmtPage", SqlDbType.Decimal);
            sqlParameters[6].Value = pagemodel.prevCreAmtPage;

            sqlParameters[7] = new SqlParameter("@prevDVatPage", SqlDbType.Decimal);
            sqlParameters[7].Value = pagemodel.prevDVatPage;

            sqlParameters[8] = new SqlParameter("@prevCVatPage", SqlDbType.Decimal);
            sqlParameters[8].Value = pagemodel.prevCVatPage;

            sqlParameters[9] = new SqlParameter("@amountDebPage", SqlDbType.Decimal);
            sqlParameters[9].Value = pagemodel.amountDebPage;

            sqlParameters[10] = new SqlParameter("@amountCrePage", SqlDbType.Decimal);
            sqlParameters[10].Value = pagemodel.amountCrePage;

            sqlParameters[11] = new SqlParameter("@vatDebPage", SqlDbType.Decimal);
            sqlParameters[11].Value = pagemodel.vatDebPage;

            sqlParameters[12] = new SqlParameter("@vatCrePage", SqlDbType.Decimal);
            sqlParameters[12].Value = pagemodel.vatCrePage;

            sqlParameters[13] = new SqlParameter("@statusPage", SqlDbType.Bit);
            sqlParameters[13].Value = pagemodel.statusPage;

            sqlParameters[14] = new SqlParameter("@idAccPage", SqlDbType.Int);
            sqlParameters[14].Value = pagemodel.idAccPage;


            return conn.executeUpdateQuery(query, sqlParameters);

        }
    }
}