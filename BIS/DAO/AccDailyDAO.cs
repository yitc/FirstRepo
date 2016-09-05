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
    public class AccDailyDAO
    {
        private dbConnection conn;
        public string bookYear;
        public AccDailyDAO(string bookyear)
        {
            conn = new dbConnection();
            this.bookYear = bookyear;

        }
       
//        public DataTable GetAllDailys()
//        {
//           // isLocked,
//            string query = string.Format(@" SELECT idDaily,codeDaily,descDaily,acd.idDailyType, c.descDailyType,acd.numberLedgerAccount, d.descLedgerAccount,
//                                idBank,ibanBank,
//                                (select count(idAccLine) from AccLine where idAccDaily = acd.idDaily and statusline=0 and booksort = 1) as unBooked,
//                                isUseCounter,inkop, acd.bookingYear, acd.automaticBook, beginPeriod
//                                FROM AccDaily  acd
//                                LEFT OUTER JOIN  AccDailyType     c on acd.idDailyType = c.idDailyType
//                                LEFT OUTER JOIN  (Select *  FROM AccLedgerAccount WHERE bookingYear = '"+bookYear+@"')  d on acd.numberLedgerAccount = d.numberLedgerAccount
//                                
//                                where acd.bookingYear = '" + bookYear + @"' order by codeDaily");
                    
//                // nameBank ostavljeno za kasnije kad bude tabele
//            //LEFT OUTER JOIN  AccDailyVerIn    e on acd.idDailyVerIn = e.idDailyVerIn    acd.idDailyVerIn, e.nameDailyVerIn, 

//             //SELECT idDaily,codeDaily,descDaily,idDailyType, c.descDailyType,numberLedgerAccount, d.descLedgerAccount
//             //                   idBank,nameBank,ibanBank,isLocked,idDailyVerIn, e.nameDailyVerIn
//             //                   FROM AccDaily  acd
//             //                   LEFT OUTER JOIN  AccDailyType     c on acd.idDailyType = c.idDailyType
//             //                   LEFT OUTER JOIN  AccLedgerAccount d on acd.numberLedgerAccount = d.numberLedgerAccount
//             //                   LEFT OUTER JOIN  AccDailyVerIn    e on acd.idDailyVerIn = e.idDailyVerIn
//             //                   order by codeDaily 



//            return conn.executeSelectQuery(query, null);
//        }

        public DataTable GetAllDailys()
        {
            // isLocked,
            string query = string.Format(@" SELECT idDaily,codeDaily,descDaily,acd.idDailyType, c.descDailyType,acd.numberLedgerAccount, d.descLedgerAccount,
                                idBank,ibanBank,
                                (select count(idAccLine) from AccLine bb where bb.idAccDaily = acd.idDaily and statusline=0 and booksort = 1 and bb.bookingYear = '" + bookYear + @"') as unBooked,
                                isUseCounter,inkop, acd.bookingYear, acd.automaticBook, beginPeriod,userCreated,dtCreated,userModified,dtModified
                                FROM AccDaily  acd
                                LEFT OUTER JOIN  AccDailyType     c on acd.idDailyType = c.idDailyType
                                LEFT OUTER JOIN  AccLedgerAccount   d on acd.numberLedgerAccount = d.numberLedgerAccount
                                
                                where acd.bookingYear = '" + bookYear + @"' order by codeDaily");

            // nameBank ostavljeno za kasnije kad bude tabele
            //LEFT OUTER JOIN  AccDailyVerIn    e on acd.idDailyVerIn = e.idDailyVerIn    acd.idDailyVerIn, e.nameDailyVerIn, 

            //SELECT idDaily,codeDaily,descDaily,idDailyType, c.descDailyType,numberLedgerAccount, d.descLedgerAccount
            //                   idBank,nameBank,ibanBank,isLocked,idDailyVerIn, e.nameDailyVerIn
            //                   FROM AccDaily  acd
            //                   LEFT OUTER JOIN  AccDailyType     c on acd.idDailyType = c.idDailyType
            //                   LEFT OUTER JOIN  AccLedgerAccount d on acd.numberLedgerAccount = d.numberLedgerAccount
            //                   LEFT OUTER JOIN  AccDailyVerIn    e on acd.idDailyVerIn = e.idDailyVerIn
            //                   order by codeDaily 



            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetBeginDaily()
        {
            string query = string.Format(@" SELECT * FROM AccDaily  where beginPeriod=1 and idDailyType = 4 and bookingYear = '" + bookYear + @"' ");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetBookingDailys()
        {
            // isLocked,
            string query = string.Format(@" SELECT idDaily,codeDaily,descDaily,acd.idDailyType, c.descDailyType,acd.numberLedgerAccount, d.descLedgerAccount,
                                idBank,ibanBank,
                                (select count(idAccLine) from AccLine where idAccDaily = acd.idDaily and statusline=0 and booksort = 1) as unBooked,
                                isUseCounter,inkop, acd.bookingYear,automaticBook, beginPeriod,userCreated,dtCreated,userModified,dtModified
                                FROM AccDaily  acd
                                LEFT OUTER JOIN  AccDailyType     c on acd.idDailyType = c.idDailyType
                                LEFT OUTER JOIN  (Select *  FROM AccLedgerAccount WHERE bookingYear = '" + bookYear + @"') d on acd.numberLedgerAccount = d.numberLedgerAccount
                           
                                WHERE acd.idDailyType = 3 and acd.bookingYear = '" + bookYear + @"' and automaticBook =1 order by codeDaily");

            // nameBank ostavljeno za kasnije kad bude tabele
            //      LEFT OUTER JOIN  AccDailyVerIn    e on acd.idDailyVerIn = e.idDailyVerIn   acd.idDailyVerIn, e.nameDailyVerIn, 

            //SELECT idDaily,codeDaily,descDaily,idDailyType, c.descDailyType,numberLedgerAccount, d.descLedgerAccount
            //                   idBank,nameBank,ibanBank,isLocked,idDailyVerIn, e.nameDailyVerIn
            //                   FROM AccDaily  acd
            //                   LEFT OUTER JOIN  AccDailyType     c on acd.idDailyType = c.idDailyType
            //                   LEFT OUTER JOIN  AccLedgerAccount d on acd.numberLedgerAccount = d.numberLedgerAccount
            //                   LEFT OUTER JOIN  AccDailyVerIn    e on acd.idDailyVerIn = e.idDailyVerIn
            //                   order by codeDaily 



            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetBookingDailysInkop()
        {
            // isLocked,
            string query = string.Format(@" SELECT idDaily,codeDaily,descDaily,acd.idDailyType, c.descDailyType,acd.numberLedgerAccount, d.descLedgerAccount,
                                idBank,ibanBank,acd.idDailyVerIn, e.nameDailyVerIn, 
                                (select count(idAccLine) from AccLine where idAccDaily = acd.idDaily and statusline=0 and booksort = 1) as unBooked,
                                isUseCounter,inkop, acd.bookingYear,automaticBook, beginPeriod,userCreated,dtCreated,userModified,dtModified
                                FROM AccDaily  acd
                                LEFT OUTER JOIN  AccDailyType     c on acd.idDailyType = c.idDailyType
                                LEFT OUTER JOIN  (Select *  FROM AccLedgerAccount WHERE bookingYear = '" + bookYear + @"') d on acd.numberLedgerAccount = d.numberLedgerAccount
                                LEFT OUTER JOIN  AccDailyVerIn    e on acd.idDailyVerIn = e.idDailyVerIn
                                WHERE acd.idDailyType = 2 and acd.bookingYear = '" + bookYear + @"' and automaticBook =1 order by codeDaily");

          

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetBookingDailysMemo()
        {
            // isLocked,
            string query = string.Format(@" SELECT idDaily,codeDaily,descDaily,acd.idDailyType, c.descDailyType,acd.numberLedgerAccount, d.descLedgerAccount,
                                idBank,ibanBank,acd.idDailyVerIn, e.nameDailyVerIn, 
                                (select count(idAccLine) from AccLine where idAccDaily = acd.idDaily and statusline=0 and booksort = 1) as unBooked,
                                isUseCounter,inkop, acd.bookingYear,automaticBook, beginPeriod,userCreated,dtCreated,userModified,dtModified
                                FROM AccDaily  acd
                                LEFT OUTER JOIN  AccDailyType     c on acd.idDailyType = c.idDailyType
                                LEFT OUTER JOIN  (Select *  FROM AccLedgerAccount WHERE bookingYear = '" + bookYear + @"') d on acd.numberLedgerAccount = d.numberLedgerAccount
                                LEFT OUTER JOIN  AccDailyVerIn    e on acd.idDailyVerIn = e.idDailyVerIn
                                WHERE acd.idDailyType = 4 and acd.bookingYear = '" + bookYear + @"'  and automaticBook =1 order by codeDaily");



            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetMemoBeginPeriod()
        {
            // isLocked,
            string query = string.Format(@" SELECT idDaily,codeDaily,descDaily,acd.idDailyType,acd.numberLedgerAccount, 
                                idBank,ibanBank, isUseCounter,inkop, acd.bookingYear,automaticBook, beginPeriod,userCreated,dtCreated,userModified,dtModified
                                FROM AccDaily  acd
                                WHERE acd.idDailyType = 4 and acd.bookingYear = '" + bookYear + @"'  and beginPeriod =1 order by codeDaily");



            return conn.executeSelectQuery(query, null);
        }
          public DataTable GetDailyById(int idDaily)
        {
            //  isLocked,
            string query = string.Format(@" SELECT idDaily,codeDaily,descDaily,acd.idDailyType, c.descDailyType,acd.numberLedgerAccount, d.descLedgerAccount,
                                idBank,ibanBank,acd.idDailyVerIn, e.nameDailyVerIn, 
                                (select count(idAccLine) from AccLine where idAccDaily = acd.idDaily and statusline=0 and booksort = 1) as unBooked,
                                isUseCounter,inkop, acd.bookingYear,automaticBook, beginPeriod,userCreated,dtCreated,userModified,dtModified
                                FROM AccDaily  acd
                                LEFT OUTER JOIN  AccDailyType     c on acd.idDailyType = c.idDailyType
                                LEFT OUTER JOIN  (Select *  FROM AccLedgerAccount WHERE bookingYear = '" + bookYear + @"') d on acd.numberLedgerAccount = d.numberLedgerAccount
                                LEFT OUTER JOIN  AccDailyVerIn    e on acd.idDailyVerIn = e.idDailyVerIn
                                WHERE idDaily = '" + idDaily.ToString() + "'  and acd.bookingYear = '" + bookYear + @"' order by codeDaily");
            return conn.executeSelectQuery(query, null);
        }
          public DataTable GetDailyByCode(string codeDaily)
          {
             // isLocked,
              string query = string.Format(@" SELECT idDaily,codeDaily,descDaily,acd.idDailyType, c.descDailyType,acd.numberLedgerAccount, d.descLedgerAccount,
                                idBank,ibanBank,acd.idDailyVerIn, e.nameDailyVerIn, 
                                (select count(idAccLine) from AccLine where idAccDaily = acd.idDaily and statusline=0 and booksort = 1) as unBooked,
                                isUseCounter,inkop, acd.bookingYear, automaticBook, beginPeriod,userCreated,dtCreated,userModified,dtModified
                                FROM AccDaily  acd
                                LEFT OUTER JOIN  AccDailyType     c on acd.idDailyType = c.idDailyType
                                LEFT OUTER JOIN  (Select *  FROM AccLedgerAccount WHERE bookingYear = '" + bookYear + @"') d on acd.numberLedgerAccount = d.numberLedgerAccount
                                LEFT OUTER JOIN  AccDailyVerIn    e on acd.idDailyVerIn = e.idDailyVerIn
                                WHERE codeDaily = '" + codeDaily.ToString() + "' and acd.bookingYear = '" + bookYear + @"' order by codeDaily");
              return conn.executeSelectQuery(query, null);
          }

          public DataTable GetDailyByIban(string iban)
          {
              // isLocked,
              string query = string.Format(@" SELECT idDaily,codeDaily,descDaily,acd.idDailyType, c.descDailyType,acd.numberLedgerAccount, d.descLedgerAccount,
                                idBank,ibanBank,acd.idDailyVerIn, e.nameDailyVerIn, 
                                (select count(idAccLine) from AccLine where idAccDaily = acd.idDaily and statusline=0 and booksort = 1) as unBooked,
                                isUseCounter,inkop, acd.bookingYear,automaticBook, beginPeriod,userCreated,dtCreated,userModified,dtModified
                                FROM AccDaily  acd
                                LEFT OUTER JOIN  AccDailyType     c on acd.idDailyType = c.idDailyType
                                LEFT OUTER JOIN  (Select *  FROM AccLedgerAccount WHERE bookingYear = '" + bookYear + @"') d on acd.numberLedgerAccount = d.numberLedgerAccount
                                LEFT OUTER JOIN  AccDailyVerIn    e on acd.idDailyVerIn = e.idDailyVerIn
                                WHERE ibanBank = '" + iban.ToString() + "'  and acd.bookingYear = '" + bookYear + @"' ");
              return conn.executeSelectQuery(query, null);
          }
        public bool Delete(int id, string nameForm, int idUser)
          {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  AccDaily WHERE idDaily = '"+id.ToString()+"' and bookingYear='" + bookYear + @"'");

     //       SqlParameter[] sqlParameter = new SqlParameter[0];

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
            sqlParameter[4].Value = id + "_" + bookYear;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "id + bookYear";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccDaily";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
        public bool Save(AccDailyModel dailymodel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            //  
//            string query = string.Format(@"SET IDENTITY_INSERT AccDaily ON
//                                           INSERT INTO  AccDaily (idDaily, codeDaily, descDaily, idDailyType, 
//                                         numberLedgerAccount, idBank, ibanBank , idDailyVerIn )    
//                                        Values ( @idDaily,  @codeDaily, @descDaily,  @idDailyType, @numberLedgerAccount,  @idBank, @ibanBank,  @idDailyVerIn) 
//                                        SET IDENTITY_INSERT AccDaily OFF");
            string query = string.Format(@"INSERT INTO  AccDaily (idDaily, codeDaily, descDaily, idDailyType, 
                                         numberLedgerAccount, idBank, ibanBank , isUseCounter,inkop,bookingYear,automaticBook, beginPeriod,
                                         userCreated,dtCreated,userModified,dtModified)    
                                 Values ( @idDaily,  @codeDaily, @descDaily,  @idDailyType,
                                         @numberLedgerAccount,  @idBank, @ibanBank, @isUseCounter,@inkop,@bookingYear,@automaticBook, @beginPeriod,
                                         @userCreated,@dtCreated,@userModified,@dtModified )");
            // isLocked , @isLocked,



            SqlParameter[] sqlParameter = new SqlParameter[16];

            sqlParameter[0] = new SqlParameter("@codeDaily", SqlDbType.NVarChar);
            sqlParameter[0].Value = dailymodel.codeDaily;

            sqlParameter[1] = new SqlParameter("@descDaily", SqlDbType.NVarChar);
            sqlParameter[1].Value = (dailymodel.descDaily == null) ? SqlString.Null : dailymodel.descDaily;

            sqlParameter[2] = new SqlParameter("@idDailyType", SqlDbType.Int);
            sqlParameter[2].Value = dailymodel.idDailyType;

            sqlParameter[3] = new SqlParameter("@numberLedgerAccount", SqlDbType.NVarChar);
            sqlParameter[3].Value = (dailymodel.numberLedgerAccount == null) ? SqlString.Null : dailymodel.numberLedgerAccount;

            sqlParameter[4] = new SqlParameter("@idBank", SqlDbType.Int);
            sqlParameter[4].Value = (dailymodel.idBank == 0) ? SqlInt32.Null : dailymodel.idBank; 

            sqlParameter[5] = new SqlParameter("@ibanBank", SqlDbType.NVarChar);
            sqlParameter[5].Value = (dailymodel.ibanBank == null) ? SqlString.Null : dailymodel.ibanBank;

            //sqlParameters[6] = new SqlParameter("@isLocked", SqlDbType.Bit);
            //sqlParameters[6].Value = dailymodel.isLocked;
             
           // sqlParameters[6] = new SqlParameter("@idDailyVerIn", SqlDbType.Int);
           //// sqlParameters[7].Value = (dailymodel.idDailyVerIn == 0) ? SqlInt32.Null : dailymodel.idDailyVerIn;
           // sqlParameters[6].Value = dailymodel.idDailyVerIn;

            sqlParameter[6] = new SqlParameter("@idDaily", SqlDbType.Int);
            sqlParameter[6].Value = dailymodel.idDaily;

            sqlParameter[7] = new SqlParameter("@isUseCounter", SqlDbType.Bit);
            sqlParameter[7].Value = dailymodel.isUseCounter;

            sqlParameter[8] = new SqlParameter("@inkop", SqlDbType.Int);
            sqlParameter[8].Value = dailymodel.inkop;

            sqlParameter[9] = new SqlParameter("@bookingYear", SqlDbType.NVarChar);
            sqlParameter[9].Value = dailymodel.bookingYear;

            sqlParameter[10] = new SqlParameter("@automaticBook", SqlDbType.Bit);
            sqlParameter[10].Value = dailymodel.automaticBook;

            sqlParameter[11] = new SqlParameter("@beginPeriod", SqlDbType.Bit);
            sqlParameter[11].Value = dailymodel.beginPeriod;

            sqlParameter[12] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[12].Value = dailymodel.userCreated;

            sqlParameter[13] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[13].Value = DateTime.Now; //model.dtCreated;

            sqlParameter[14] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[14].Value = dailymodel.userModified;

            sqlParameter[15] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[15].Value = dailymodel.dtModified;


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
            sqlParameter[4].Value = dailymodel.idDaily;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idDaily";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccDaily";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
        public bool Update(AccDailyModel dailymodel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string query = string.Format(@"UPDATE AccDaily SET   codeDaily=@codeDaily, descDaily=@descDaily,
                                idDailyType=@idDailyType, numberLedgerAccount=@numberLedgerAccount,idBank= @idBank, 
                                ibanBank=@ibanBank, isUseCounter=@isUseCounter,inkop=@inkop,bookingYear=@bookingYear,automaticBook=@automaticBook,
                                beginPeriod=@beginPeriod,userCreated=@userCreated,dtCreated=@dtCreated,userModified=@userModified,dtModified=@dtModified
                                WHERE idDaily=@idDaily and bookingYear = '" + bookYear + @"' ");

            //isLocked=@isLocked,

            SqlParameter[] sqlParameter = new SqlParameter[16];

            sqlParameter[0] = new SqlParameter("@codeDaily", SqlDbType.NVarChar);
            sqlParameter[0].Value = dailymodel.codeDaily;

            sqlParameter[1] = new SqlParameter("@descDaily", SqlDbType.NVarChar);
            sqlParameter[1].Value = (dailymodel.descDaily == null) ? SqlString.Null : dailymodel.descDaily;

            sqlParameter[2] = new SqlParameter("@idDailyType", SqlDbType.Int);
            sqlParameter[2].Value = dailymodel.idDailyType;

            sqlParameter[3] = new SqlParameter("@numberLedgerAccount", SqlDbType.NVarChar);
            sqlParameter[3].Value = (dailymodel.numberLedgerAccount == null) ? SqlString.Null : dailymodel.numberLedgerAccount;

            sqlParameter[4] = new SqlParameter("@idBank", SqlDbType.Int);
            sqlParameter[4].Value = (dailymodel.idBank == 0) ? SqlInt32.Null : dailymodel.idBank;

            sqlParameter[5] = new SqlParameter("@ibanBank", SqlDbType.NVarChar);
            sqlParameter[5].Value = (dailymodel.ibanBank == null) ? SqlString.Null : dailymodel.ibanBank;

            //sqlParameters[6] = new SqlParameter("@isLocked", SqlDbType.Bit);
            //sqlParameters[6].Value = dailymodel.isLocked;

            //sqlParameters[6] = new SqlParameter("@idDailyVerIn", SqlDbType.Int);
            ////sqlParameters[7].Value = (dailymodel.idDailyVerIn == 0) ? SqlInt32.Null : dailymodel.idDailyVerIn;
            //sqlParameters[6].Value = dailymodel.idDailyVerIn;
                  

            sqlParameter[6] = new SqlParameter("@idDaily", SqlDbType.Int);
            sqlParameter[6].Value = dailymodel.idDaily;

            sqlParameter[7] = new SqlParameter("@isUseCounter", SqlDbType.Bit);
            sqlParameter[7].Value = dailymodel.isUseCounter;

            sqlParameter[8] = new SqlParameter("@inkop", SqlDbType.Int);
            sqlParameter[8].Value = dailymodel.inkop;

            sqlParameter[9] = new SqlParameter("@bookingYear", SqlDbType.NVarChar);
            sqlParameter[9].Value = dailymodel.bookingYear;

            sqlParameter[10] = new SqlParameter("@automaticBook", SqlDbType.Bit);
            sqlParameter[10].Value = dailymodel.automaticBook;

            sqlParameter[11] = new SqlParameter("@beginPeriod", SqlDbType.Bit);
            sqlParameter[11].Value = dailymodel.beginPeriod;

            sqlParameter[12] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[12].Value = dailymodel.userCreated;

            sqlParameter[13] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[13].Value = dailymodel.dtCreated;

            sqlParameter[14] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[14].Value = dailymodel.userModified;

            sqlParameter[15] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[15].Value = DateTime.Now; //dailymodel.dtModified;


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
            sqlParameter[4].Value = dailymodel.idDaily.ToString() +"_"+ bookYear.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idDaily + bookYear";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccDaily";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
    }
}