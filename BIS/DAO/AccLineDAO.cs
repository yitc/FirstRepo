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
    public class AccLineDAO
    {
        public  string bookYear;
        private dbConnection conn;
        public AccLineDAO(string bookyear)
        {
            conn = new dbConnection();
            this.bookYear = bookyear;

        }
        
        public DataTable GetAllLinesByDaily(int idDaily, int openclose)
        {
            //and statusLine='" + openclose.ToString() + "'  
            string query = string.Format(@" SELECT   idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate,incopNr,iban,term,idSepa,bookingYear,userCreated,dtCreated,userModified,dtModified
                                        FROM AccLine  WHERE idAccDaily='" + idDaily.ToString() + "' and booksort=1 and bookingYear = '"+bookYear+"'");
            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetLinesByAccount(string account, int openclose)
        {
            string query = string.Format(@" SELECT   idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate,incopNr,iban,term,idSepa,bookingYear
                                        FROM AccLine  WHERE numberLedAccount='" + account.ToString() + "' and bookingYear = '" + bookYear + "' ");
            // and statusLine='" + openclose.ToString() + "'
            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAllLinesYear(string year)
        {
            //and statusLine='" + openclose.ToString() + "'  
            string query = string.Format(@" SELECT   idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate,incopNr,iban,term,idSepa, bookingYear
                                        FROM AccLine  WHERE bookingYear='" + year + "' ");
            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetLinesByOnlyAccount(string account)
        {
            string query = string.Format(@" SELECT   idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate,incopNr,iban,term,idSepa,bookingYear
                                        FROM AccLine  WHERE numberLedAccount='" + account.ToString() + "' and bookingYear = '" + bookYear + "' ");
            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetLinesByBTW(int Btw)
        {
            string query = string.Format(@" SELECT   idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate,incopNr,iban,term,idSepa,bookingYear
                                        FROM AccLine  WHERE idBTW ='" + Btw.ToString() + "' and bookingYear = '" + bookYear + "' ");
            return conn.executeSelectQuery(query, null);
        }


        public DataTable GetLinesByAccountAndCustomer(string account, int openclose, string customer)
        {
            //and statusLine='" + openclose.ToString() + "'
            string query = string.Format(@" SELECT   idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate,incopNr,iban,term,idSepa,bookingYear
                                        FROM AccLine  WHERE numberLedAccount='" + account.ToString() + "'  and idCLientLine = '" + customer.ToString() + "' and bookingYear = '" + bookYear + "' ");
            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetLinesByAccountAndCustomerAndProject(string account, string customer,string project)
        {
            //and statusLine='" + openclose.ToString() + "'
            string query = string.Format(@" SELECT DISTINCT  idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate,incopNr,iban,term,idSepa,a.bookingYear
                                        FROM AccLine a 
                                        left join AccDaily m on m.idDaily = a.idAccDaily
              WHERE numberLedAccount='" + account.ToString() + "'  and idCLientLine = '" + customer.ToString() + "' and m.idDailyType = '2' and idProjectLine = '" + project.ToString() + "' and a.bookingYear = '" + bookYear + "'");
            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllLinesByIdCurrency(int idCurrency, int idDaily, int openclose)
        {
            //and statusLine='" + openclose.ToString() + "'
            string query = string.Format(@" SELECT   idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,debitLine-creditLine as Versil,dtBooking, booksort,currrate,incopNr,iban,term,idSepa,bookingYear,dtCreated,dtModified
                                        FROM AccLine  WHERE idCurrency='" + idCurrency.ToString() + "' and idAccDaily = '" + idDaily.ToString() + "' and booksort=1 and bookingYear = '" + bookYear + "' ");
            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAllLinesByinvoice(string invoice, int openclose)
        {
            string query = string.Format(@" SELECT   idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate,incopNr,iban,term,idSepa,bookingYear
                                        FROM AccLine  WHERE invoiceNr='" + invoice.ToString() + "' and statusLine='" + openclose.ToString() + "' and booksort>1 and bookingYear = '" + bookYear + "' ");
            //@idDaily and statusLine=@openclose
            //SqlParameter[] sqlParameters = new SqlParameter[2];
            //sqlParameters[0] = new SqlParameter("@idDaily", SqlDbType.Int);
            //sqlParameters[0].Value = idDaily;
            //sqlParameters[1] = new SqlParameter("@openclose", SqlDbType.Int);
            //sqlParameters[1].Value = openclose;

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAllLinesByNumber(string incopNr, int openclose)
        {
            string reserv_acc = "9999";//"1610";  
            string debit_reserv = "9999"; //"130500";
            int yearPlus = Convert.ToInt32(bookYear) + 1;
            //and statusLine='" + openclose.ToString() + "'
            //(numberLedAccount != '" + reserv_acc + "' and numberLedAccount != '" + debit_reserv + "') and and booksort>1
            string query = string.Format(@" SELECT   idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate,incopNr,iban,term,idSepa,a.bookingYear,idMaster,idDetail,d.descLedgerAccount
                                        FROM AccLine  a
                                        LEFT JOIN AccLedgerAccount d ON numberLedAccount = d.numberLedgerAccount
                                        WHERE incopNr='" + incopNr.ToString() + "'   and  (a.bookingYear = '" + bookYear + "' or a.bookingYear = '" + yearPlus.ToString() + "') ");

            //a.bookingYear = '" + bookYear + "'
            return conn.executeSelectQuery(query, null);
        }
       
        public DataTable GetAllLinesByNumberAutomatic(string incopNr, int openclose)
        {
            string reserv_acc = "9999";//"1610";
            string debit_reserv = "9999"; //"130500";
            int yearPlus = Convert.ToInt32(bookYear) + 1;
            //and statusLine='" + openclose.ToString() + "'
            //(numberLedAccount != '" + reserv_acc + "' and numberLedAccount != '" + debit_reserv + "') and and booksort>1
            string query = string.Format(@" SELECT   idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate,incopNr,iban,term,idSepa,a.bookingYear,idMaster,idDetail,d.descLedgerAccount
                                        FROM AccLine  a
                                        LEFT JOIN AccLedgerAccount d ON numberLedAccount = d.numberLedgerAccount
                                        WHERE incopNr='" + incopNr.ToString() + "' and (a.bookingYear = '" + bookYear + "' or a.bookingYear = '" + yearPlus.ToString() + "') ");
            //a.bookingYear = '" + bookYear + "'

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAllLinesByNumberByIncop(string incopNr, int openclose)
        {
            string reserv_acc = "9999";//"1610";
            string debit_reserv = "9999"; //"130500";
            int yearPlus = Convert.ToInt32(bookYear) + 1;
            //and statusLine='" + openclose.ToString() + "'
            //(numberLedAccount != '" + reserv_acc + "' and numberLedAccount != '" + debit_reserv + "') and  and booksort>1
            string query = string.Format(@" SELECT   idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate,incopNr,iban,term,idSepa,a.bookingYear,idMaster,idDetail,d.descLedgerAccount
                                        FROM AccLine a 
                                        LEFT JOIN AccLedgerAccount d ON numberLedAccount = d.numberLedgerAccount
                                        WHERE incopNr='" + incopNr.ToString() + "'  and (a.bookingYear = '" + bookYear + "' or a.bookingYear = '" + yearPlus.ToString() + "') ");
            
            //a.bookingYear = '" + bookYear + "' 
            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAllLinesByNumberand1610(string incopNr, int openclose)
        {
        
            string query = string.Format(@" SELECT   idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate,incopNr,iban,term,idSepa,bookingYear
                                        FROM AccLine  WHERE incopNr='" + incopNr.ToString() + "'  and booksort>1 and bookingYear = '" + bookYear + "' ");


            return conn.executeSelectQuery(query, null);
        }
        public DataTable CheckLines(string incopNr, string idClientLine)
        {
            string query = string.Format(@" SELECT   idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate,incopNr,iban,term,idSepa,bookingYear
                                        FROM AccLine  WHERE invoiceNr='" + incopNr.ToString() + "' and idClientLine='" + idClientLine.ToString() + "' and bookingYear = '" + bookYear + "' ");


            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAllLinesByNumberALL(string incopNr, int openclose)
        {
            string query = string.Format(@" SELECT   idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate,incopNr,iban,term,idSepa,bookingYear
                                        FROM AccLine  WHERE incopNr='" + incopNr.ToString() + "'  and bookingYear = '" + bookYear + "' ");

            //and statusLine='" + openclose.ToString() + "'
            return conn.executeSelectQuery(query, null);
        }
         public DataTable GetAllLinesByinvoiceAndIdDaily(string invoice, int idAccDaily, int openclose)
        {
            string query = string.Format(@" SELECT   idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate,incopNr,iban,term,idSepa,bookingYear
                                        FROM AccLine  WHERE invoiceNr='" + invoice.ToString() + "' and statusLine='" + openclose.ToString() + "' and idAccDaily = '" + idAccDaily.ToString() + "' and booksort>1 and bookingYear = '" + bookYear + "'");
            //@idDaily and statusLine=@openclose
            //SqlParameter[] sqlParameters = new SqlParameter[2];
            //sqlParameters[0] = new SqlParameter("@idDaily", SqlDbType.Int);
            //sqlParameters[0].Value = idDaily;
            //sqlParameters[1] = new SqlParameter("@openclose", SqlDbType.Int);
            //sqlParameters[1].Value = openclose;
            
            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetLine(int idAccLine)
        {
            string reserv_acc = "1610";
            string debit_reserv = "130500";
            int yearPlus = Convert.ToInt32(bookYear) + 1;
            //and (numberLedAccount != '" + reserv_acc + "' and numberLedAccount != '" + debit_reserv + "')
            string query = string.Format(@" SELECT   idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate,incopNr,iban,term,idSepa,bookingYear,debitLine-creditLine as versil
                                        FROM AccLine  WHERE idAccline='" + idAccLine.ToString() + "'  and (bookingYear = '" + bookYear + "' or bookingYear = '" + yearPlus.ToString()+ "') ");
            return conn.executeSelectQuery(query, null);
        }

        public object GetSumDebit(string account)  // suma debit
        {
            string query = string.Format(@" SELECT  SUM(debitLine) as Debit,bookingYear,periodLine FROM AccLine  WHERE numberLedAccount='" + account.ToString() + "' and bookingYear = '" + bookYear + "' and periodLine > 0 Group by bookingYear,periodLine");
            //return conn.executeSelectQuery(query, null);
             return conn.executeScalarQuery(query, null);
           
            
        }
        public object GetSumCredit(string account)  // suma credit
        {
            string query = string.Format(@" SELECT  SUM(creditLine) as Credit,bookingYear,periodLine FROM AccLine  WHERE numberLedAccount='" + account.ToString() + "' and bookingYear = '" + bookYear + "' and periodLine > 0  Group by bookingYear,periodLine");
            return conn.executeScalarQuery(query, null);
        }
        public object GetSumTrans(string account) // number transactions
        {
            string query = string.Format(@" SELECT  COUNT(idAccLine) as Trans,bookingYear FROM AccLine  WHERE numberLedAccount='" + account.ToString() + "'  and bookingYear = '" + bookYear + "'  Group by bookingYear");
            return conn.executeScalarQuery(query, null);
        }

        public object GetSumDebitBegin(string account)  // suma debit
        {
            string query = string.Format(@" SELECT  SUM(debitLine) as Debit,bookingYear,periodLine FROM AccLine  WHERE numberLedAccount='" + account.ToString() + "' and periodLine=0 and bookingYear = '" + bookYear + "' and periodLine = 0 Group by bookingYear,periodLine");
            //return conn.executeSelectQuery(query, null);
            return conn.executeScalarQuery(query, null);
        }
        public object GetSumCreditBegin(string account)  // suma credit
        {
            string query = string.Format(@" SELECT  SUM(creditLine) as Credit,bookingYear,periodLine FROM AccLine  WHERE numberLedAccount='" + account.ToString() + "'and periodLine=0 and bookingYear = '" + bookYear + "' and periodLine = 0 Group by bookingYear,periodLine");
            return conn.executeScalarQuery(query, null);
        }
        //============ sume za naloge ================
        public object SumCreditLinesByNalog(int idCurrency)  // suma credit
        {
            string query = string.Format(@" SELECT  SUM(creditLine) as Credit,bookingYear FROM AccLine  WHERE idCurrency='" + idCurrency.ToString() + "' and booksort = 1 and bookingYear = '" + bookYear + "'  Group by bookingYear");
            return conn.executeScalarQuery(query, null);
        }
        public object SumDebitLinesByNalog(int idCurrency)  // suma credit
        {
            string query = string.Format(@" SELECT  SUM(debitLine) as Debit,bookingYear FROM AccLine  WHERE idCurrency='" + idCurrency.ToString() + "' and booksort = 1 and bookingYear = '" + bookYear + "'  Group by bookingYear");
            return conn.executeScalarQuery(query, null);
        }
        //=============================================
        public DataTable GetAllCounters(string year)
        {
            string query = string.Format(@" SELECT * from Id WHERE yearId = '" + year.ToString()+ "'");

            return conn.executeSelectQuery(query, null);
         
        }

        //==================== update brojac inkop faktura
        public DataTable GetIncop(string year, int daily, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@" UPDATE id SET idNumber = idNumber + 1 WHERE yearId = '" + year.ToString() + "' and idDaily = '" + daily.ToString() + "'");

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
            sqlParameter[3].Value = "U";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = year +"_"+ daily.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "yearId_idDaily";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccCost";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            conn.executQueryTransaction(_query, sqlParameters);

           // conn.executeSelectQuery(query, null);
            string query1 = string.Format(@" SELECT * from id WHERE yearId = '" + year.ToString() + "' and idDaily = '" + daily.ToString() + "'");
            return conn.executeSelectQuery(query1, null);
        }
        public DataTable GetIncopView(string year, int daily)
        {
            string query1 = string.Format(@" SELECT * from id WHERE yearId = '" + year.ToString() + "' and idDaily = '" + daily.ToString() + "'");
            return conn.executeSelectQuery(query1, null);
        }
        public DataTable GetBankNr()  // brojac banka
        {
            string query = string.Format(@" UPDATE id SET idNumberBank = idNumberBank + 1");

            conn.executeUpdateQuery(query, null);
            // conn.executeSelectQuery(query, null);
            string query1 = string.Format(@" SELECT idNumberBank from id");
            return conn.executeSelectQuery(query1, null);
        }
        public DataTable GetVerkopNr()   // brojac izlaznih faktura
        {
            string query = string.Format(@" UPDATE id SET idNumVerkop = idNumVerkop + 1");

            conn.executeUpdateQuery(query, null);
            // conn.executeSelectQuery(query, null);
            string query1 = string.Format(@" SELECT idNumVerkop from id");
            return conn.executeSelectQuery(query1, null);
        }


        //provera radi brisanja
        public DataTable GetCostLineFromAccLine(string idCostLine)
        {
            string query = string.Format(@"SELECT idCostLine,bookingYear
                                           FROM AccLine
                                           WHERE idCostLine=@idCostLine and bookingYear = '" + bookYear + "'  ");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idCostLine", SqlDbType.NVarChar);
            sqlParameters[0].Value = idCostLine;

            return conn.executeSelectQuery(query, sqlParameters);
        }


        public bool Delete(int id, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  AccLine WHERE idAccLine = @idAccLine  and bookingYear = '" + bookYear + "'  ");

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
            sqlParameter[4].Value = id ;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "id";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccLine";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool DeleteByCurrencyID(int idCurrency, int idDaily, int openclose, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  AccLine WHERE idCurrency = @idCurrency AND idAccDaily = @idAccDaily AND statusLine = @statusLine and bookingYear = '" + bookYear + "'  ");
//AND booksort = @booksort
            SqlParameter[] sqlParameter = new SqlParameter[3];

            sqlParameter[0] = new SqlParameter("@idCurrency", SqlDbType.Int);
            sqlParameter[0].Value = idCurrency;

            sqlParameter[1] = new SqlParameter("@idAccDaily", SqlDbType.Int);
            sqlParameter[1].Value = idDaily;

            sqlParameter[2] = new SqlParameter("@statusLine", SqlDbType.Bit);
            sqlParameter[2].Value = openclose;

            //sqlParameters[3] = new SqlParameter("@booksort", SqlDbType.Int);
            //sqlParameters[3].Value = 1;

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
            sqlParameter[4].Value = idCurrency + "_" + idDaily + "_" + openclose;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idCurrency + idDaily + openclose";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccLine";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete by currency id";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
        public bool DeleteByReference(string incopNr, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  AccLine WHERE incopNr = @incopNr AND  booksort > 1 and bookingYear = '" + bookYear + "'  ");
            //AND booksort = @booksort
            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@incopNr", SqlDbType.NVarChar);
            sqlParameter[0].Value = incopNr;

            //sqlParameters[1] = new SqlParameter("@idAccDaily", SqlDbType.Int);
            //sqlParameters[1].Value = idDaily;

            //sqlParameters[2] = new SqlParameter("@statusLine", SqlDbType.Bit);
            //sqlParameters[2].Value = openclose;

            //sqlParameters[3] = new SqlParameter("@booksort", SqlDbType.Int);
            //sqlParameters[3].Value = 1;

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
            sqlParameter[4].Value = incopNr + "_" + bookYear;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "id + bookYear";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccLine";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete by reference";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
        public bool DeleteByReferenceALL(string incopNr, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  AccLine WHERE incopNr = @incopNr AND   bookingYear = '" + bookYear + "'  ");
            //AND booksort = @booksort
            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@incopNr", SqlDbType.NVarChar);
            sqlParameter[0].Value = incopNr;

            //sqlParameters[1] = new SqlParameter("@idAccDaily", SqlDbType.Int);
            //sqlParameters[1].Value = idDaily;

            //sqlParameters[2] = new SqlParameter("@statusLine", SqlDbType.Bit);
            //sqlParameters[2].Value = openclose;

            //sqlParameters[3] = new SqlParameter("@booksort", SqlDbType.Int);
            //sqlParameters[3].Value = 1;

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
            sqlParameter[4].Value = incopNr + "_" + bookYear;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "incopNr + bookYear";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccLine";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete by reference all";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }




//        public bool SaveTransact(AccLineModel linemodel, string nameForm, int idUser)
//        {

//            List<string> _query = new List<string>();
//            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

//            string query = string.Format(@"INSERT INTO  AccLine ( idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
//                                        idClientLine,idPersonLine,idCostLine, debitLine,creditLine,idBTW,debitBTW,creditBTW,
//                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate, idProjectLine,incopNr,iban,bookingYear,term,idSepa,idMaster,idDetail,userCreated,userModified,dtCreated,dtModified )
//                                 Values ( @idAccDaily,@statusLine,@periodLine, @dtLine,@numberLedAccount,@invoiceNr,@descLine,
//                                        @idClientLine,@idPersonLine,@idCostLine, @debitLine,@creditLine,@idBTW,@debitBTW,@creditBTW,
//                                        @idCurrency,@debitCurr, @creditCurr,@dtBooking, @booksort,@currrate,@idProjectLine,@incopNr,@iban,@bookingYear,@term,@idSepa,@idMaster,@idDetail,@userCreated,@userModified,@dtCreated,@dtModified )");


//            // SqlParameter[] sqlParameters = new SqlParameter[33];
//            SqlParameter[] sqlParameter = new SqlParameter[33];

//            sqlParameter[0] = new SqlParameter("@idAccDaily", SqlDbType.Int);
//            sqlParameter[0].Value = linemodel.idAccDaily;

//            sqlParameter[1] = new SqlParameter("@statusLine", SqlDbType.Bit);
//            sqlParameter[1].Value = linemodel.statusLine;

//            sqlParameter[2] = new SqlParameter("@periodLine", SqlDbType.Int);
//            sqlParameter[2].Value = linemodel.periodLine;

//            sqlParameter[3] = new SqlParameter("@dtLine", SqlDbType.DateTime);
//            sqlParameter[3].Value = (linemodel.dtLine == null || linemodel.dtLine == DateTime.MinValue) ? SqlDateTime.Null : linemodel.dtLine;



//            sqlParameter[4] = new SqlParameter("@numberLedAccount", SqlDbType.NVarChar);
//            sqlParameter[4].Value = (linemodel.numberLedAccount == null || linemodel.numberLedAccount == "") ? String.Empty : linemodel.numberLedAccount; ;

//            sqlParameter[5] = new SqlParameter("@invoiceNr", SqlDbType.NVarChar);
//            sqlParameter[5].Value = (linemodel.invoiceNr == null || linemodel.invoiceNr == "") ? String.Empty : linemodel.invoiceNr;

//            sqlParameter[6] = new SqlParameter("@descLine", SqlDbType.NVarChar);
//            sqlParameter[6].Value = (linemodel.descLine == null || linemodel.descLine == "") ? String.Empty : linemodel.descLine;

//            sqlParameter[7] = new SqlParameter("@idClientLine", SqlDbType.NVarChar);
//            sqlParameter[7].Value = (linemodel.idClientLine == null || linemodel.idClientLine == "") ? String.Empty : linemodel.idClientLine;

//            sqlParameter[8] = new SqlParameter("@idPersonLine", SqlDbType.NVarChar);
//            sqlParameter[8].Value = (linemodel.idPersonLine == null || linemodel.idPersonLine == "") ? String.Empty : linemodel.idPersonLine;

//            sqlParameter[9] = new SqlParameter("@idCostLine", SqlDbType.NVarChar);
//            sqlParameter[9].Value = (linemodel.idCostLine == null || linemodel.idCostLine == "") ? String.Empty : linemodel.idCostLine;

//            //sqlParameters[10] = new SqlParameter("@idProjectLine", SqlDbType.NVarChar);
//            //sqlParameters[10].Value = linemodel.idProjectLine;

//            sqlParameter[10] = new SqlParameter("@debitLine", SqlDbType.Decimal);
//            sqlParameter[10].Value = linemodel.debitLine;

//            sqlParameter[11] = new SqlParameter("@creditLine", SqlDbType.Decimal);
//            sqlParameter[11].Value = linemodel.creditLine;

//            sqlParameter[12] = new SqlParameter("@idBTW", SqlDbType.Int);
//            sqlParameter[12].Value = linemodel.idBTW;

//            sqlParameter[13] = new SqlParameter("@debitBTW", SqlDbType.Decimal);
//            sqlParameter[13].Value = linemodel.debitBTW;

//            sqlParameter[14] = new SqlParameter("@creditBTW", SqlDbType.Decimal);
//            sqlParameter[14].Value = linemodel.creditBTW;

//            sqlParameter[15] = new SqlParameter("@idCurrency", SqlDbType.Int);
//            sqlParameter[15].Value = linemodel.idCurrency;

//            sqlParameter[16] = new SqlParameter("@debitCurr", SqlDbType.Decimal);
//            sqlParameter[16].Value = linemodel.debitCurr;

//            sqlParameter[17] = new SqlParameter("@creditCurr", SqlDbType.Decimal);
//            sqlParameter[17].Value = linemodel.creditCurr;

//            sqlParameter[18] = new SqlParameter("@dtBooking", SqlDbType.DateTime);
//            sqlParameter[18].Value = linemodel.dtBooking;

//            sqlParameter[19] = new SqlParameter("@booksort", SqlDbType.Int);
//            sqlParameter[19].Value = linemodel.booksort;

//            sqlParameter[20] = new SqlParameter("@currrate", SqlDbType.Decimal);
//            sqlParameter[20].Value = linemodel.currrate;

//            sqlParameter[21] = new SqlParameter("@idProjectLine", SqlDbType.NVarChar);
//            sqlParameter[21].Value = (linemodel.idProjectLine == null || linemodel.idProjectLine == "") ? String.Empty : linemodel.idProjectLine; ;

//            sqlParameter[22] = new SqlParameter("@incopNr", SqlDbType.NVarChar);
//            sqlParameter[22].Value = (linemodel.incopNr == null || linemodel.incopNr == "") ? String.Empty : linemodel.incopNr;

//            sqlParameter[23] = new SqlParameter("@iban", SqlDbType.NVarChar);
//            sqlParameter[23].Value = (linemodel.iban == null || linemodel.iban == "") ? String.Empty : linemodel.iban;

//            sqlParameter[24] = new SqlParameter("@bookingYear", SqlDbType.NVarChar);
//            sqlParameter[24].Value = (linemodel.bookingYear == null || linemodel.bookingYear == "") ? DateTime.Now.Year.ToString() : linemodel.bookingYear;

//            sqlParameter[25] = new SqlParameter("@term", SqlDbType.Int);
//            sqlParameter[25].Value = linemodel.term;

//            sqlParameter[26] = new SqlParameter("@idSepa", SqlDbType.Int);
//            sqlParameter[26].Value = linemodel.idSepa;

//            sqlParameter[27] = new SqlParameter("@idMaster", SqlDbType.NVarChar);
//            sqlParameter[27].Value = linemodel.idMaster;

//            sqlParameter[28] = new SqlParameter("@idDetail", SqlDbType.NVarChar);
//            sqlParameter[28].Value = linemodel.idDetail;

//            sqlParameter[29] = new SqlParameter("@userCreated", SqlDbType.NVarChar);
//            sqlParameter[29].Value = linemodel.userCreated;

//            sqlParameter[30] = new SqlParameter("@userModified", SqlDbType.NVarChar);
//            sqlParameter[30].Value = linemodel.userModified;

//            sqlParameter[31] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
//            sqlParameter[31].Value = DateTime.Now;

//            sqlParameter[32] = new SqlParameter("@dtModified", SqlDbType.DateTime);
//            sqlParameter[32].Value = DateTime.Now;

//            _query.Add(query);
//            sqlParameters.Add(sqlParameter);


//            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId)
//                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId)");


//            sqlParameter = new SqlParameter[6]; //6


//            sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
//            sqlParameter[0].Value = nameForm;

//            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
//            sqlParameter[1].Value = idUser;

//            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
//            sqlParameter[2].Value = DateTime.Now;

//            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
//            sqlParameter[3].Value = "I";

//            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
//            sqlParameter[4].Value = linemodel.idAccLine;

//            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
//            sqlParameter[5].Value = "idAccLine";

//            _query.Add(query);
//            sqlParameters.Add(sqlParameter);


//            return conn.executQueryTransaction(_query, sqlParameters);


//            //  return conn.executeInsertQuery(query, sqlParameters);

//        }

        // NETA 10-8-2016
        public bool SaveTransact(List<AccLineModel> linemodelList, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            foreach (AccLineModel linemodel in linemodelList)
            {

                string query = string.Format(@"INSERT INTO  AccLine ( idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine, debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate, idProjectLine,incopNr,iban,bookingYear,term,idSepa,idMaster,idDetail,userCreated,userModified,dtCreated,dtModified )
                                 Values ( @idAccDaily,@statusLine,@periodLine, @dtLine,@numberLedAccount,@invoiceNr,@descLine,
                                        @idClientLine,@idPersonLine,@idCostLine, @debitLine,@creditLine,@idBTW,@debitBTW,@creditBTW,
                                        @idCurrency,@debitCurr, @creditCurr,@dtBooking, @booksort,@currrate,@idProjectLine,@incopNr,@iban,@bookingYear,@term,@idSepa,@idMaster,@idDetail,@userCreated,@userModified,@dtCreated,@dtModified )");


                // SqlParameter[] sqlParameters = new SqlParameter[33];
                SqlParameter[] sqlParameter = new SqlParameter[33];

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
                sqlParameter[22].Value = (linemodel.incopNr == null || linemodel.incopNr == "") ? String.Empty : linemodel.incopNr;

                sqlParameter[23] = new SqlParameter("@iban", SqlDbType.NVarChar);
                sqlParameter[23].Value = (linemodel.iban == null || linemodel.iban == "") ? String.Empty : linemodel.iban;

                sqlParameter[24] = new SqlParameter("@bookingYear", SqlDbType.NVarChar);
                sqlParameter[24].Value = (linemodel.bookingYear == null || linemodel.bookingYear == "") ? DateTime.Now.Year.ToString() : linemodel.bookingYear;

                sqlParameter[25] = new SqlParameter("@term", SqlDbType.Int);
                sqlParameter[25].Value = linemodel.term;

                sqlParameter[26] = new SqlParameter("@idSepa", SqlDbType.Int);
                sqlParameter[26].Value = linemodel.idSepa;

                sqlParameter[27] = new SqlParameter("@idMaster", SqlDbType.NVarChar);
                sqlParameter[27].Value = linemodel.idMaster;

                sqlParameter[28] = new SqlParameter("@idDetail", SqlDbType.NVarChar);
                sqlParameter[28].Value = linemodel.idDetail;

                sqlParameter[29] = new SqlParameter("@userCreated", SqlDbType.NVarChar);
                sqlParameter[29].Value = linemodel.userCreated;

                sqlParameter[30] = new SqlParameter("@userModified", SqlDbType.NVarChar);
                sqlParameter[30].Value = linemodel.userModified;

                sqlParameter[31] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
                sqlParameter[31].Value = DateTime.Now;

                sqlParameter[32] = new SqlParameter("@dtModified", SqlDbType.DateTime);
                sqlParameter[32].Value = DateTime.Now;

                _query.Add(query);
                sqlParameters.Add(sqlParameter);


                query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");


                sqlParameter = new SqlParameter[8]; //6


                sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
                sqlParameter[0].Value = nameForm;

                sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
                sqlParameter[1].Value = idUser;

                sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
                sqlParameter[2].Value = DateTime.Now;

                sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
                sqlParameter[3].Value = "I";

                sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
                sqlParameter[4].Value = conn.GetLastTableID("AccLine") + 1;

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idAccLine";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "AccLine";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Save transact";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);
            }
            return conn.executQueryTransaction(_query, sqlParameters);


            //  return conn.executeInsertQuery(query, sqlParameters);

        }

        public bool Save(AccLineModel linemodel, string nameForm, int idUser)
        {

            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO  AccLine ( idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine, debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate, idProjectLine,incopNr,iban,bookingYear,term,idSepa,idMaster,idDetail,userCreated,userModified,dtCreated,dtModified )
                                 Values ( @idAccDaily,@statusLine,@periodLine, @dtLine,@numberLedAccount,@invoiceNr,@descLine,
                                        @idClientLine,@idPersonLine,@idCostLine, @debitLine,@creditLine,@idBTW,@debitBTW,@creditBTW,
                                        @idCurrency,@debitCurr, @creditCurr,@dtBooking, @booksort,@currrate,@idProjectLine,@incopNr,@iban,@bookingYear,@term,@idSepa,@idMaster,@idDetail,@userCreated,@userModified,@dtCreated,@dtModified )");


            SqlParameter[] sqlParameter = new SqlParameter[33];

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
            sqlParameter[22].Value = (linemodel.incopNr == null || linemodel.incopNr == "") ? String.Empty : linemodel.incopNr;

            sqlParameter[23] = new SqlParameter("@iban", SqlDbType.NVarChar);
            sqlParameter[23].Value = (linemodel.iban == null || linemodel.iban == "") ? String.Empty : linemodel.iban;

            sqlParameter[24] = new SqlParameter("@bookingYear", SqlDbType.NVarChar);
            sqlParameter[24].Value = (linemodel.bookingYear == null || linemodel.bookingYear == "") ? DateTime.Now.Year.ToString() : linemodel.bookingYear;

            sqlParameter[25] = new SqlParameter("@term", SqlDbType.Int);
            sqlParameter[25].Value = linemodel.term;

            sqlParameter[26] = new SqlParameter("@idSepa", SqlDbType.Int);
            sqlParameter[26].Value = linemodel.idSepa;

            sqlParameter[27] = new SqlParameter("@idMaster", SqlDbType.NVarChar);
            sqlParameter[27].Value = linemodel.idMaster;

            sqlParameter[28] = new SqlParameter("@idDetail", SqlDbType.NVarChar);
            sqlParameter[28].Value = linemodel.idDetail;

            sqlParameter[29] = new SqlParameter("@userCreated", SqlDbType.NVarChar);
            sqlParameter[29].Value = linemodel.userCreated;

            sqlParameter[30] = new SqlParameter("@userModified", SqlDbType.NVarChar);
            sqlParameter[30].Value = linemodel.userModified;

            sqlParameter[31] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[31].Value = DateTime.Now;

            sqlParameter[32] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[32].Value = DateTime.Now;



            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");


            sqlParameter = new SqlParameter[8]; //6


            sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameForm;

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = idUser;

            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "I";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = conn.GetLastTableID("AccLine") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAccLine";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccLine";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            return conn.executQueryTransaction(_query, sqlParameters);

        }
        public bool UpdateStatus(AccLineModel model, string nameForm, int idUser)
        {

            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE AccLine SET  statusLine=1,userModified=@userModified,dtModified=@dtModified WHERE idAccLine=@idAccLine  ");
            SqlParameter[] sqlParameter = new SqlParameter[3];

            sqlParameter[0] = new SqlParameter("@idAccLine", SqlDbType.Int);
            sqlParameter[0].Value = model.idAccLine;

            sqlParameter[1] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[1].Value = model.userModified;

            sqlParameter[2] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

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
            sqlParameter[4].Value = model.idAccLine;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAccLine";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccLine";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update status";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            return conn.executQueryTransaction(_query, sqlParameters);
        }
        public bool Update(AccLineModel linemodel, string nameForm, int idUser)
        {

            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE AccLine SET   idAccDaily=@idAccDaily,statusLine=@statusLine,periodLine=@periodLine,
                                        dtLine=@dtLine, numberLedAccount=@numberLedAccount,invoiceNr=@invoiceNr,descLine=@descLine,
                                        idClientLine=@idClientLine,idPersonLine=@idPersonLine,idCostLine=@idCostLine,idProjectLine=@idProjectLine, 
                                        debitLine=@debitLine,creditLine=@creditLine,idBTW=@idBTW,debitBTW=@debitBTW,creditBTW=@creditBTW,
                                        idCurrency=@idCurrency,debitCurr=@debitCurr,creditCurr=@creditCurr,dtBooking=@dtBooking,booksort=@booksort,
                                        currrate=@currrate,incopNr=@incopNr,iban=@iban,bookingYear=@bookingYear,term=@term,idSepa=@idSepa,userModified=@userModified,dtModified=@dtModified
                                        WHERE idAccLine=@idAccLine ");

            SqlParameter[] sqlParameter = new SqlParameter[30];

            sqlParameter[0] = new SqlParameter("@idAccDaily", SqlDbType.Int);
            sqlParameter[0].Value = linemodel.idAccDaily;

            sqlParameter[1] = new SqlParameter("@statusLine", SqlDbType.Bit);
            sqlParameter[1].Value = linemodel.statusLine;

            sqlParameter[2] = new SqlParameter("@periodLine", SqlDbType.Int);
            sqlParameter[2].Value = linemodel.periodLine;

            sqlParameter[3] = new SqlParameter("@dtLine", SqlDbType.DateTime);
            sqlParameter[3].Value = linemodel.dtLine;

            sqlParameter[4] = new SqlParameter("@numberLedAccount", SqlDbType.NVarChar);
            sqlParameter[4].Value = linemodel.numberLedAccount;

            sqlParameter[5] = new SqlParameter("@invoiceNr", SqlDbType.NVarChar);
            sqlParameter[5].Value = linemodel.invoiceNr;

            sqlParameter[6] = new SqlParameter("@descLine", SqlDbType.NVarChar);
            sqlParameter[6].Value = linemodel.descLine;

            sqlParameter[7] = new SqlParameter("@idClientLine", SqlDbType.NVarChar);
            sqlParameter[7].Value = linemodel.idClientLine;

            sqlParameter[8] = new SqlParameter("@idPersonLine", SqlDbType.NVarChar);
            sqlParameter[8].Value = linemodel.idPersonLine;

            sqlParameter[9] = new SqlParameter("@idCostLine", SqlDbType.NVarChar);
            sqlParameter[9].Value = linemodel.idCostLine;

            sqlParameter[10] = new SqlParameter("@idProjectLine", SqlDbType.NVarChar);
            sqlParameter[10].Value = linemodel.idProjectLine;

            sqlParameter[11] = new SqlParameter("@debitLine", SqlDbType.Decimal);
            sqlParameter[11].Value = linemodel.debitLine;

            sqlParameter[12] = new SqlParameter("@creditLine", SqlDbType.Decimal);
            sqlParameter[12].Value = linemodel.creditLine;

            sqlParameter[13] = new SqlParameter("@idBTW", SqlDbType.Int);
            sqlParameter[13].Value = linemodel.idBTW;

            sqlParameter[14] = new SqlParameter("@debitBTW", SqlDbType.Decimal);
            sqlParameter[14].Value = linemodel.debitBTW;

            sqlParameter[15] = new SqlParameter("@creditBTW", SqlDbType.Decimal);
            sqlParameter[15].Value = linemodel.creditBTW;

            sqlParameter[16] = new SqlParameter("@idCurrency", SqlDbType.Int);
            sqlParameter[16].Value = linemodel.idCurrency;

            sqlParameter[17] = new SqlParameter("@debitCurr", SqlDbType.Decimal);
            sqlParameter[17].Value = linemodel.debitCurr;

            sqlParameter[18] = new SqlParameter("@creditCurr", SqlDbType.Decimal);
            sqlParameter[18].Value = linemodel.creditCurr;

            sqlParameter[19] = new SqlParameter("@dtBooking", SqlDbType.DateTime);
            sqlParameter[19].Value = linemodel.dtBooking;

            sqlParameter[20] = new SqlParameter("@booksort", SqlDbType.Int);
            sqlParameter[20].Value = linemodel.booksort;

            sqlParameter[21] = new SqlParameter("@currrate", SqlDbType.Decimal);
            sqlParameter[21].Value = linemodel.currrate;

            sqlParameter[22] = new SqlParameter("@incopNr", SqlDbType.NVarChar);
            sqlParameter[22].Value = linemodel.incopNr;

            sqlParameter[23] = new SqlParameter("@idAccLine", SqlDbType.Int);
            sqlParameter[23].Value = linemodel.idAccLine;

            sqlParameter[24] = new SqlParameter("@iban", SqlDbType.NVarChar);
            sqlParameter[24].Value = linemodel.iban;

            sqlParameter[25] = new SqlParameter("@bookingYear", SqlDbType.NVarChar);
            sqlParameter[25].Value = linemodel.bookingYear;

            sqlParameter[26] = new SqlParameter("@term", SqlDbType.Int);
            sqlParameter[26].Value = linemodel.term;

            sqlParameter[27] = new SqlParameter("@idSepa", SqlDbType.Int);
            sqlParameter[27].Value = linemodel.idSepa;

            sqlParameter[28] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[28].Value = linemodel.userModified;

            sqlParameter[29] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[29].Value = DateTime.Now;

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
            sqlParameter[4].Value = linemodel.idAccLine;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAccLine";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccLine";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            return conn.executQueryTransaction(_query, sqlParameters);

        }
          public bool MakeCounter(string year, int idDaily, int idNumber)
        {
            string query = string.Format(@"INSERT INTO  Id ( yearId,idDaily,idNumber)
                                 Values ( @yearId,@idDaily, @idNumber)");

            SqlParameter[] sqlParameters = new SqlParameter[3];

            sqlParameters[0] = new SqlParameter("@yearId", SqlDbType.NVarChar);
            sqlParameters[0].Value = year;
            sqlParameters[1] = new SqlParameter("@idDaily", SqlDbType.Int);
            sqlParameters[1].Value = idDaily;
            sqlParameters[2] = new SqlParameter("@idNumber", SqlDbType.Int);
            sqlParameters[2].Value = idNumber;

            return conn.executeInsertQuery(query, sqlParameters);
        }
          public DataTable GetLinesJournal(string daily, string account, string customer, DateTime fromdate, DateTime todate,string fromperiod,string toperiod,string cost, string project, int ord, string factur)
          {
              string order = "";
              string datumi = "(dtLine >=  '" + fromdate.ToString("MM/dd/yyyy HH:mm:ss.fff") + "'  AND dtLine <=  '" + todate.ToString("MM/dd/yyyy HH:mm:ss.fff").Substring(0, 10) + " 23:59:59.000" + "' )";
              string where = "WHERE ";
              string condition = "";
              string period = "";
              string trosak = "";
              string projekat = "";
              string invoice = " invoiceNr = '" + factur + "' ";
              string yearbook = " and b.bookingYear = '" + bookYear + "' ";
              string yearbook1 = " WHERE  b.bookingYear = '" + bookYear + "' ";
              if (ord == 1)
                  order = "ORDER By idAccDaily, periodLine";
              else
                  if (ord == 2)
                      order = "ORDER By idAccDaily, numberLedAccount";
                  else
                      order = "";

              if (fromdate != DateTime.MinValue && todate != DateTime.MinValue)
                  condition = datumi;
              else
                  if (fromdate != DateTime.MinValue)
                      condition = " dtLine >=  '" + fromdate.ToString("MM/dd/yyyy HH:mm:ss.fff") + "' and dtLine <= '" + fromdate.ToString("MM/dd/yyyy HH:mm:ss.fff").Substring(0, 10) + " 23:59:59.000" + "'";

              if (fromperiod == "" && toperiod == "")
                  period = "";
              else
                  if (fromperiod != "")
                      period = " periodLine >= '" + fromperiod.ToString() + "' and  periodLine <= '" + toperiod.ToString() + "' ";
             
              //==== daily ====
              if (daily != "")
                  where = where + "idAccDaily = '" + daily.ToString() + "' ";
              //=== account =====
              if (account != "")
                  if (where != "WHERE ")
                         where = where + " and numberLedAccount = '" + account.ToString() + "' ";
                  else
                      where = where + " numberLedAccount = '" + account.ToString() + "' ";
              //==== client && person
              if (customer != "")
                  if (where != "WHERE ")
                      where = where + " and idClientLine = '" + customer.ToString() + "' ";
                  else
                      where = where + " idClientLine = '" + customer.ToString() + "' ";
              //==== cost =====
              if (cost != "")
                  if (where != "WHERE ")
                      where = where + " and idCostLine = '" + cost.ToString() + "' ";
                  else
                      where = where + " idCostLine = '" + cost.ToString() + "' ";
              //==== project ====
              if (project != "")
                  if (where != "WHERE ")
                      where = where + " and idProjectLine = '" + project.ToString() + "' ";
                  else
                      where = where + " idProjectLine = '" + project.ToString() + "' ";
              //==== date =====
              if (condition != "")
                  if (where != "WHERE ")
                      where = where + " and " + condition;
                  else
                      where = where + condition;
              //=====invoice =====
              if (factur != "")
                  if (where != "WHERE ")
                      where = where + " and " + " invoiceNr = '" + factur + "' ";
                  else
                      where = where + invoice;
              //==================


              //==== period ===
              if (period != "")
                  if (where != "WHERE ")
                      where = where + " and " + period;
                  else
                      where = where + period;

               if (where != "WHERE ")
               {
                   string query = string.Format(@" SELECT   idAccLine,a.idAccDaily,b.descDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,convert(varchar(10), dtBooking, 120) as dtBooking, booksort,currrate,incopNr,a.bookingYear,term,idSepa,
                                        SPACE(60) as cond1,SPACE(60) as cond2, SPACE(60) as cond3,SPACE(40) as userN
                                        FROM AccLine a
                                        LEFT JOIN AccDaily b on b.idDaily = a.idAccDaily 
                                        " + where + yearbook + order);
                   return conn.executeSelectQuery(query, null);
               }
               else
               {
                   string query = string.Format(@" SELECT   idAccLine,a.idAccDaily,b.descDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate,incopNr,a.bookingYear,term,idSepa,
                                        SPACE(60) as cond1,SPACE(60) as cond2, SPACE(60) as cond3, SPACE(40) as userN
                                        FROM AccLine a
                                        LEFT JOIN AccDaily b on b.idDaily = a.idAccDaily 
                                        " + yearbook1 + order);
                   return conn.executeSelectQuery(query, null);
               }

              //CAST(dtBooking as DATE) as 
              //CAST(dtBooking as DATE) as 
              //and statusLine='" + openclose.ToString() + "' and idCLientLine = '" + customer.ToString() + "'
          }
//          public DataTable GetLinesOPEN(string daily, DateTime fromdate, DateTime todate, string fromperiod, string toperiod)
//          {
//              // 
//              // HH:mm:ss.fff
//              string datumi = "(dtBooking >=  '" + fromdate.ToString("MM/dd/yyyy HH:mm:ss.fff") + "'  AND dtBooking <=  '" + todate.ToString("MM/dd/yyyy HH:mm:ss.fff").Substring(0, 10) + " 23:59:59.000" + "' )";
//              string where = "WHERE ";
//              string condition = "";
//              string period = "";
//              string trosak = "";
//              string projekat = "";
//              if (fromdate != DateTime.MinValue && todate != DateTime.MinValue)
//                  condition = datumi;
//              else
//                 //  HH:mm:ss.fff
//                  //.Substring(0, 10) + " 23:59:59.000" + "'
//                  if (fromdate != DateTime.MinValue)
//                      condition = " dtBooking >=  '" + fromdate.ToString("MM/dd/yyyy HH:mm:ss.fff") + "' and dtBooking <= '" + fromdate.ToString("MM/dd/yyyyHH:mm:ss.fff").Substring(0, 10) + " 23:59:59.000" + "'";

//              if (fromperiod == "" && toperiod == "")
//                  period = "";
//              else
//                  if (fromperiod != "")
//                      period = " periodLine >= '" + fromperiod.ToString() + "' and  periodLine <= '" + toperiod.ToString() + "' ";

//              //==== daily ====
//              if (daily != "")
//                  where = where + "idAccDaily = '" + daily.ToString() + "' ";
//              //=== account =====
//              //if (account != "")
//              //    if (where != "WHERE ")
//              //        where = where + " and numberLedAccount = '" + account.ToString() + "' ";
//              //    else
//              //        where = where + " numberLedAccount = '" + account.ToString() + "' ";
//              ////==== client && person
//              //if (customer != "")
//              //    if (where != "WHERE ")
//              //        where = where + " and idClientLine = '" + customer.ToString() + "' ";
//              //    else
//              //        where = where + " idClientLine = '" + customer.ToString() + "' ";
//              ////==== cost =====
//              //if (cost != "")
//              //    if (where != "WHERE ")
//              //        where = where + " and idCostLine = '" + cost.ToString() + "' ";
//              //    else
//              //        where = where + " idCostLine = '" + cost.ToString() + "' ";
//              ////==== project ====
//              //if (project != "")
//              //    if (where != "WHERE ")
//              //        where = where + " and idProjectLine = '" + project.ToString() + "' ";
//              //    else
//              //        where = where + " idProjectLine = '" + project.ToString() + "' ";
//              //==== date =====
//              if (condition != "")
//                  if (where != "WHERE ")
//                      where = where + " and " + condition;
//                  else
//                      where = where + condition;
//              //==== period ===
//              if (period != "")
//                  if (where != "WHERE ")
//                      where = where + " and " + period;
//                  else
//                      where = where + period;
//              // convert(varchar(10), dtBooking, 120)
//              if (where != "WHERE ")
//              {
//                  string query = string.Format(@" SELECT   idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
//                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
//                                        idCurrency,debitCurr, creditCurr,convert(varchar(10), dtBooking, 120) as dtBooking, booksort,currrate,incopNr,bookingYear,term,idSepa
//                                        FROM AccLine " + where + "and statusline = 0 and bookingYear = '" + bookYear + "' order by incopNr, booksort");
//                  return conn.executeSelectQuery(query, null);
//              }
//              else
//              {
//                  string query = string.Format(@" SELECT   idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
//                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
//                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate,incopNr,bookingYear,term,idSepa
//                                        FROM AccLine WHERE statusline = 0 and bookingYear = '"+bookYear+"' order by incopNr, booksort");
//                  return conn.executeSelectQuery(query, null);
//              }

//              //CAST(dtBooking as DATE) as 
//              //CAST(dtBooking as DATE) as 
//              //and statusLine='" + openclose.ToString() + "' and idCLientLine = '" + customer.ToString() + "'
//          }

          public DataTable GetLinesOPEN(string daily, DateTime fromdate, DateTime todate, string fromperiod, string toperiod, string invoiceNr)
          {
              // 
              // HH:mm:ss.fff
              string datumi = "(dtBooking >=  '" + fromdate.ToString("MM/dd/yyyy HH:mm:ss.fff") + "'  AND dtBooking <=  '" + todate.ToString("MM/dd/yyyy HH:mm:ss.fff").Substring(0, 10) + " 23:59:59.000" + "' )";
              string where = "WHERE ";
              string condition = "";
              string period = "";
              string trosak = "";
              string projekat = "";
              if (fromdate != DateTime.MinValue && todate != DateTime.MinValue)
                  condition = datumi;
              else
                  //  HH:mm:ss.fff
                  //.Substring(0, 10) + " 23:59:59.000" + "'
                  if (fromdate != DateTime.MinValue)
                      condition = " dtBooking >=  '" + fromdate.ToString("MM/dd/yyyy HH:mm:ss.fff") + "' and dtBooking <= '" + fromdate.ToString("MM/dd/yyyyHH:mm:ss.fff").Substring(0, 10) + " 23:59:59.000" + "'";

              if (fromperiod == "" && toperiod == "")
                  period = "";
              else
                  if (fromperiod != "")
                      period = " periodLine >= '" + fromperiod.ToString() + "' and  periodLine <= '" + toperiod.ToString() + "' ";

              //==== daily ====
              if (daily != "")
                  where = where + "idAccDaily = '" + daily.ToString() + "' ";

              if (invoiceNr != "")
              {
                  if (where != "WHERE ")
                  {
                      where += " and invoiceNr = '" + invoiceNr + "' ";
                  }
                  else
                  {
                      where += " invoiceNr = '" + invoiceNr + "' ";
                  }
              }
              //=== account =====
              //if (account != "")
              //    if (where != "WHERE ")
              //        where = where + " and numberLedAccount = '" + account.ToString() + "' ";
              //    else
              //        where = where + " numberLedAccount = '" + account.ToString() + "' ";
              ////==== client && person
              //if (customer != "")
              //    if (where != "WHERE ")
              //        where = where + " and idClientLine = '" + customer.ToString() + "' ";
              //    else
              //        where = where + " idClientLine = '" + customer.ToString() + "' ";
              ////==== cost =====
              //if (cost != "")
              //    if (where != "WHERE ")
              //        where = where + " and idCostLine = '" + cost.ToString() + "' ";
              //    else
              //        where = where + " idCostLine = '" + cost.ToString() + "' ";
              ////==== project ====
              //if (project != "")
              //    if (where != "WHERE ")
              //        where = where + " and idProjectLine = '" + project.ToString() + "' ";
              //    else
              //        where = where + " idProjectLine = '" + project.ToString() + "' ";
              //==== date =====
              if (condition != "")
                  if (where != "WHERE ")
                      where = where + " and " + condition;
                  else
                      where = where + condition;
              //==== period ===
              if (period != "")
                  if (where != "WHERE ")
                      where = where + " and " + period;
                  else
                      where = where + period;
              // convert(varchar(10), dtBooking, 120)
              if (where != "WHERE ")
              {
                  string query = string.Format(@" SELECT   idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,convert(varchar(10), dtBooking, 120) as dtBooking, booksort,currrate,incopNr,bookingYear,term,idSepa
                                        FROM AccLine " + where + "and statusline = 0 and bookingYear = '" + bookYear + "' order by incopNr, booksort");
                  return conn.executeSelectQuery(query, null);
              }
              else
              {
                  string query = string.Format(@" SELECT   idAccLine,idAccDaily,statusLine,periodLine, dtLine, numberLedAccount,invoiceNr,descLine,
                                        idClientLine,idPersonLine,idCostLine,idProjectLine,  debitLine,creditLine,idBTW,debitBTW,creditBTW,
                                        idCurrency,debitCurr, creditCurr,dtBooking, booksort,currrate,incopNr,bookingYear,term,idSepa
                                        FROM AccLine WHERE statusline = 0 and bookingYear = '" + bookYear + "' order by incopNr, booksort");
                  return conn.executeSelectQuery(query, null);
              }

              //CAST(dtBooking as DATE) as 
              //CAST(dtBooking as DATE) as 
              //and statusLine='" + openclose.ToString() + "' and idCLientLine = '" + customer.ToString() + "'
          }


          public DataTable GetAllAccLineReport(string from, string to, string nameUser, DateTime dtPeriodFrom, DateTime dtPeriodTo)
          {

              int month = 1;
              int year = 1;
              if (dtPeriodTo.Month == 12)
              {
                  month = 1;
                  year = dtPeriodTo.Year + 1;


              }
              else
              {
                  month = Convert.ToInt32(dtPeriodTo.Month) + 1;
                  year = dtPeriodTo.Year;
              }

              string query = string.Format(@"SELECT al.idAccLine,al.periodLine,al.dtLine,al.invoiceNr,al.descLine,al.numberLedAccount,
              CASE WHEN lac.descLedgerAccount IS NULL THEN '' ELSE lac.descLedgerAccount END AS descLedgerAccount,al.debitLine,
              al.creditLine,
              idClientLine,CASE WHEN adc.idContPerson = 0 OR adc.idContPerson IS NULL THEN c.nameClient
              ELSE (CASE WHEN p.firstname IS NULL THEN p.lastname
              ELSE (CASE WHEN p.lastname IS NULL THEN '' ELSE p.firstname + ' ' + p.lastname END) END) END as cName,
              al.idCostLine,al.idProjectLine,'" + nameUser + @"' as nameUser, '" + from + @"' as [from],'" + to + @"' as [to],al.bookingYear
              FROM AccLine al
              LEFT OUTER JOIN AccDebCre adc ON al.idClientLine = adc.accNumber
              LEFT OUTER JOIN AccLedgerAccount lac ON lac.numberLedgerAccount = al.numberLedAccount
              LEFT JOIN Client c ON c.idClient = adc.idClient
              LEFT JOIN ContactPerson p ON p.idContPers = adc.idContPerson
              WHERE al.numberLedAccount BETWEEN '" + from.ToString().Trim() + "' AND '" + to.ToString().Trim() + "' and al.bookingYear BETWEEN '" + dtPeriodFrom.Year + "' AND '" + dtPeriodTo.Year + @"'
              and al.dtLine >=  '" + dtPeriodFrom.Month + "-1-" + dtPeriodFrom.Year + "' AND al.dtLine <'" + month.ToString() + "-1-" + year.ToString() + "' AND DATEPART(YEAR,al.dtLine) BETWEEN '" + dtPeriodFrom.Year + "' AND '" + dtPeriodTo.Year + "'");
              return conn.executeSelectQuery(query, null);

          }

          public DataTable GetBeginAmounts(string bookyear,string creditor, string debitor)
          {
              string query = string.Format(@" SELECT numberLedAccount,sum(debitLine) as debit, sum(creditLine) as credit, sum(debitLine) - sum(creditLine) as diff  
              FROM Accline a
              WHERE a.numberLedAccount  IN (SELECT numberLedgerAccount FROM ACCLEDGERACCOUNT  WHERE accountTypeAccount = 1) 
              and bookingyear='" + bookyear + "'   group by a.numberledAccount");
//              string query = string.Format(@"SELECT numberLedAccount, space(6) as idClientLine, sum(debitLine) as debit, sum(creditLine) as credit, sum(debitLine) - sum(creditLine) as diff  
//              FROM Accline a
//              LEFT OUTER JOIN AccLedgerAccount j on a.numberLedAccount = j.numberLedgerAccount
//              WHERE a.numberLedAccount  IN (SELECT numberLedgerAccount FROM ACCLEDGERACCOUNT  WHERE accountTypeAccount = 1) 
//              and a.numberLedAccount != '"+creditor+@"' and a.numberLedAccount != '"+debitor+@"' 
//              and a.bookingyear='" + bookyear + @"'   group by a.numberledAccount
//              UNION 
//              SELECT numberLedAccount,idClientLine, sum(debitLine) as debit, sum(creditLine) as credit, sum(debitLine) - sum(creditLine) as diff  
//              FROM Accline b
//              WHERE b.numberLedAccount  IN (SELECT numberLedgerAccount FROM ACCLEDGERACCOUNT  WHERE 
//              numberLedgerAccount = (SELECT defCreditorAccount from AccSettings c where c.yearSettings = '" + bookyear + @"' ) )
//              and bookingyear='" + bookyear + @"'   group by b.numberledAccount, idClientLine
//              union 
//              SELECT numberLedAccount,idClientLine, sum(debitLine) as debit, sum(creditLine) as credit, sum(debitLine) - sum(creditLine) as diff  
//              FROM Accline d
//              WHERE d.numberLedAccount  IN (SELECT numberLedgerAccount FROM ACCLEDGERACCOUNT  WHERE 
//              numberLedgerAccount = (SELECT defDebitorAccount from AccSettings e where e.yearSettings = '" + bookyear + @"' ) )
//              and bookingyear='" + bookyear + @"'   group by d.numberledAccount, idClientLine ");



              return conn.executeSelectQuery(query, null);
              //,idClientLine,idCostLine,idProjectLine
              //, a.idClientLine ,a.idCostLine,a.idProjectLine
          }
          public DataTable GetBeginSUM4Amounts(string bookyear)
          {
              string query = string.Format(@" SELECT numberLedAccount,sum(debitLine) as debit, sum(creditLine) as credit, sum(debitLine) - sum(creditLine) as diff  
              FROM Accline a
              WHERE a.numberLedAccount  IN (SELECT numberLedgerAccount FROM ACCLEDGERACCOUNT  WHERE accountTypeAccount = 2 ) 
              and a.bookingyear='" + bookyear + "'   group by a.numberledAccount");
              return conn.executeSelectQuery(query, null);
              //,idClientLine,idCostLine,idProjectLine
              //, a.idClientLine ,a.idCostLine,a.idProjectLine
              //and numberLedgerAccount LIKE '4%'
          }
          public DataTable GetBeginSUM8Amounts(string bookyear)
          {
              string query = string.Format(@" SELECT numberLedAccount,sum(debitLine) as debit, sum(creditLine) as credit, sum(debitLine) - sum(creditLine) as diff  
              FROM Accline a
              WHERE a.numberLedAccount  IN (SELECT numberLedgerAccount FROM ACCLEDGERACCOUNT  WHERE accountTypeAccount = 2 and numberLedgerAccount LIKE '8%') 
              and a.bookingyear='" + bookyear + "'   group by a.numberledAccount");
              return conn.executeSelectQuery(query, null);
              //,idClientLine,idCostLine,idProjectLine
              //, a.idClientLine ,a.idCostLine,a.idProjectLine
          }
    }
}