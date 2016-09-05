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
    public class LedgerAccountDAO
    {
        private dbConnection conn;
        public string bookYear;


        public LedgerAccountDAO(string bookyear)
        {
            conn = new dbConnection();
            this.bookYear = bookyear;

        }

        public DataTable GetAllAccounts()
        {
            //am.debitAmount
            string query = string.Format(@" SELECT distinct  (acl.numberLedgerAccount),idLedgerAccount,descLedgerAccount,CASE WHEN am.beginDebit IS NULL THEN 0 ELSE am.beginDebit END as openDebitAccount ,CASE WHEN am.beginCredit IS NULL THEN 0 ELSE am.beginCredit END as openCreditAccount,
                                accountTypeAccount,CASE WHEN accountTypeAccount = '1' THEN 'Balans' ELSE  'Winst Verlies' END as nameTypeAccount,
                        idCostCenter, mandatoryCostAccount,mandatoryDebitorAccount, mandatoryCreditorAccount,mandatoryProjectAccount,isBudgetAccount, class1Account,class2Account,class3Account,class4Account,class5Account,   
                        am.debitAmount as debitAccount,am.creditAmount as creditAccount,am.transactionsNo as transactionNoAccount, acc.descCost  as nameCostLedgerAccount, cc1.descClass as nameClass1LedgerAccount,cc2.descClass as nameClass2LedgerAccount,
                        cc3.descClass as nameClass3LedgerAccount,cc4.descClass as nameClass4LedgerAccount,cc5.descClass as nameClass5LedgerAccount,
                         valutaDebitLedgerAccount,valutaCreditLedgerAccount,isBTWLedgerAccount,isActiveLedgerAccount,sideBooking,isBlockMemorial,btwId, acl.bookingYear                  
                        FROM AccLedgerAccount  acl
                        LEFT OUTER JOIN  AccCost acc on acl.idCostCenter = acc.codeCost
                        LEFT OUTER JOIN  AccLedgerClass cc1 on acl.class1Account = cc1.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc2 on acl.class2Account = cc2.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc3 on acl.class3Account = cc3.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc4 on acl.class4Account = cc4.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc5 on acl.class5Account = cc5.idClass
                        LEFT OUTER JOIN  AccLedgerAmounts am on acl.numberLedgerAccount = am.numberLedgerAccount and am.bookingYear = '" + bookYear+@"'
                        WHERE   isActiveLedgerAccount= 0  order by acl.numberLedgerAccount ");

            // LEFT OUTER  JOIN AccLine m on acl.numberLedgerAccount = m.numberLedAccount and m.bookingYear = '"+bookYear+ @"'
//            string query = string.Format(@"SELECT distinct  (numberLedgerAccount),idLedgerAccount,descLedgerAccount,openDebitAccount,openCreditAccount,
//                                accountTypeAccount,CASE WHEN accountTypeAccount = '1' THEN 'Balans' ELSE  'Winst Verlies' END as nameTypeAccount,
//                        idCostCenter, mandatoryCostAccount,mandatoryDebitorAccount, mandatoryCreditorAccount,mandatoryProjectAccount,isBudgetAccount, class1Account,class2Account,class3Account,class4Account,class5Account,   
//                         debitAccount,creditAccount,transactionNoAccount, acc.descCost  as nameCostLedgerAccount, cc1.descClass as nameClass1LedgerAccount,cc2.descClass as nameClass2LedgerAccount,
//                        cc3.descClass as nameClass3LedgerAccount,cc4.descClass as nameClass4LedgerAccount,cc5.descClass as nameClass5LedgerAccount,
//                         valutaDebitLedgerAccount,valutaCreditLedgerAccount,isBTWLedgerAccount,isActiveLedgerAccount,sideBooking,isBlockMemorial,btwId, acl.bookingYear                  
//                        FROM AccLedgerAccount  acl
//                        LEFT OUTER JOIN  AccCost acc on acl.idCostCenter = acc.codeCost
//                        LEFT OUTER JOIN  AccLedgerClass cc1 on acl.class1Account = cc1.idClass
//                        LEFT OUTER JOIN  AccLedgerClass cc2 on acl.class2Account = cc2.idClass
//                        LEFT OUTER JOIN  AccLedgerClass cc3 on acl.class3Account = cc3.idClass
//                        LEFT OUTER JOIN  AccLedgerClass cc4 on acl.class4Account = cc4.idClass
//                        LEFT OUTER JOIN  AccLedgerClass cc5 on acl.class5Account = cc5.idClass
//                         LEFT OUTER  JOIN AccLine m on acl.numberLedgerAccount = m.numberLedAccount 
//                       
//                        WHERE   isActiveLedgerAccount= 0 order by numberLedgerAccount ");
            //acl.bookingYear = '" + bookYear + "' and
            // LEFT OUTER JOIN (select numberLedaccount, sum(debitline)as debit from accline where  bookingYear = '" + bookYear + @"' group by numberLedaccount) d on d.numberLedaccount = acl.numberLedgerAccount + d.debit


            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAllAccountsPrint(string from, string to, string nameUser, bool isWithZero)

        {
            if (isWithZero == true)
            {
                //openDebitAccount,openCreditAccount, debitAccount,creditAccount,transactionNoAccount,
                string query = string.Format(@"SELECT distinct  (acl.numberLedgerAccount),idLedgerAccount,descLedgerAccount,
                                accountTypeAccount,CASE WHEN accountTypeAccount = '1' THEN 'Balans' ELSE  'Winst Verlies' END as nameTypeAccount,
                        idCostCenter, mandatoryCostAccount,mandatoryDebitorAccount, mandatoryCreditorAccount,mandatoryProjectAccount,isBudgetAccount, class1Account,class2Account,class3Account,class4Account,class5Account,   
                         acc.descCost  as nameCostLedgerAccount, cc1.descClass as nameClass1LedgerAccount,cc2.descClass as nameClass2LedgerAccount,
                        cc3.descClass as nameClass3LedgerAccount,cc4.descClass as nameClass4LedgerAccount,cc5.descClass as nameClass5LedgerAccount,
                         valutaDebitLedgerAccount,valutaCreditLedgerAccount,isBTWLedgerAccount,isActiveLedgerAccount,CASE WHEN sideBooking = 'D' THEN 'Debet' ELSE 'Credit' END as sideBooking
                        ,isBlockMemorial,btwId,'" + from + @"' as [from], '" + to + @"' as [to],'" + nameUser + @"' as nameUser ,  acl.bookingYear , m.beginDebit as openDebitAccount,m.beginCredit as openCreditAccount, 
                        m.debitAmount as debitAccount,m.creditAmount as creditAccount ,m.transactionsNo as transactionNoAccount             
                        FROM AccLedgerAccount  acl
                        LEFT OUTER JOIN  AccCost acc on acl.idCostCenter = acc.codeCost
                        LEFT OUTER JOIN  AccLedgerClass cc1 on acl.class1Account = cc1.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc2 on acl.class2Account = cc2.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc3 on acl.class3Account = cc3.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc4 on acl.class4Account = cc4.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc5 on acl.class5Account = cc5.idClass
                        LEFT OUTER  JOIN AccLedgerAmounts m on acl.numberLedgerAccount = m.numberLedgerAccount and m.bookingYear = '" +bookYear+@"'  
                        WHERE  acl.numberLedgerAccount between '" + from.ToString().Trim() + "' and '" + to.ToString().Trim() + "'order by acl.numberLedgerAccount ");
                //acl.bookingYear = '" + bookYear + "' and
                return conn.executeSelectQuery(query, null);
            }
            else
            {
                //debitAccount,creditAccount,transactionNoAccount,openDebitAccount,openCreditAccount,
                string query = string.Format(@"SELECT distinct  (acl.numberLedgerAccount),idLedgerAccount,descLedgerAccount,
                                accountTypeAccount,CASE WHEN accountTypeAccount = '1' THEN 'Balans' ELSE  'Winst Verlies' END as nameTypeAccount,
                        idCostCenter, mandatoryCostAccount,mandatoryDebitorAccount, mandatoryCreditorAccount,mandatoryProjectAccount,isBudgetAccount, class1Account,class2Account,class3Account,class4Account,class5Account,   
                          acc.descCost  as nameCostLedgerAccount, cc1.descClass as nameClass1LedgerAccount,cc2.descClass as nameClass2LedgerAccount,
                        cc3.descClass as nameClass3LedgerAccount,cc4.descClass as nameClass4LedgerAccount,cc5.descClass as nameClass5LedgerAccount,
                         valutaDebitLedgerAccount,valutaCreditLedgerAccount,isBTWLedgerAccount,isActiveLedgerAccount,CASE WHEN sideBooking = 'D' THEN 'Debet' ELSE 'Credit' END as sideBooking
                        ,isBlockMemorial,btwId,'" + from + @"' as [from], '" + to + @"' as [to],'" + nameUser + @"' as nameUser,  acl.bookingYear , m.beginDebit as openDebitAccount,m.beginCredit as openCreditAccount, 
                        m.debitAmount as debitAccount,m.creditAmount as creditAccount ,m.transactionsNo as transactionNoAccount                            
                        FROM AccLedgerAccount  acl
                        LEFT OUTER JOIN  AccCost acc on acl.idCostCenter = acc.codeCost
                        LEFT OUTER JOIN  AccLedgerClass cc1 on acl.class1Account = cc1.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc2 on acl.class2Account = cc2.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc3 on acl.class3Account = cc3.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc4 on acl.class4Account = cc4.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc5 on acl.class5Account = cc5.idClass
                        LEFT OUTER  JOIN AccLedgerAmounts m on acl.numberLedgerAccount = m.numberLedgerAccount and m.bookingYear = '" + bookYear + @"'  
                        WHERE  acl.numberLedgerAccount between '" + from.ToString().Trim() + "' and '" + to.ToString().Trim() + "'  AND (debitAccount != 0 OR creditAccount != 0) order by acl.numberLedgerAccount ");
                //acl.bookingYear = '" + bookYear + "'  and
                return conn.executeSelectQuery(query, null);
            }

           
        }



        public DataTable GetAccount(string numberLedgerAccount, string year)
        {
            string query = string.Format(@"  SELECT idLedgerAccount,acl.numberLedgerAccount,descLedgerAccount,CASE WHEN am.beginDebit IS NULL THEN 0 ELSE am.beginDebit END as openDebitAccount ,CASE WHEN am.beginCredit IS NULL THEN 0 ELSE am.beginCredit END as openCreditAccount,
                                accountTypeAccount,CASE WHEN accountTypeAccount = '1' THEN 'Balans' ELSE  'Winst Verlies' END as nameTypeAccount,
                        idCostCenter, mandatoryCostAccount,mandatoryDebitorAccount, mandatoryCreditorAccount,mandatoryProjectAccount,isBudgetAccount, class1Account, class2Account,class3Account,class4Account,class5Account, 
                        am.debitAmount as debitAccount,am.creditAmount as creditAccount,am.transactionsNo as transactionNoAccount, acc.descCost  as nameCostLedgerAccount, cc1.descClass as nameClass1LedgerAccount,cc2.descClass as nameClass2LedgerAccount,
                        cc3.descClass as nameClass3LedgerAccount,cc4.descClass as nameClass4LedgerAccount,cc5.descClass as nameClass5LedgerAccount,
                          valutaDebitLedgerAccount,valutaCreditLedgerAccount,isBTWLedgerAccount,isActiveLedgerAccount,sideBooking,isBlockMemorial,btwId,acl.bookingYear                    
                        FROM AccLedgerAccount  acl
                        LEFT OUTER JOIN  AccCost acc on acl.idCostCenter = acc.codeCost
                        LEFT OUTER JOIN  AccLedgerClass cc1 on acl.class1Account = cc1.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc2 on acl.class2Account = cc2.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc3 on acl.class3Account = cc3.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc4 on acl.class4Account = cc4.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc5 on acl.class5Account = cc5.idClass
                        LEFT OUTER JOIN  AccLedgerAmounts am on acl.numberLedgerAccount = am.numberLedgerAccount and am.bookingYear = '" + year + @"'
                        WHERE  acl.numberLedgerAccount = '" + numberLedgerAccount + "' ");
            //and acl.bookingYear = '" + year + "'
          
            return conn.executeSelectQuery(query, null);
        }



        //provera
        public DataTable GetGetCostCenterFromAccLedgerAccount(string idCostCenter)
        {
            string query = string.Format(@"SELECT idCostCenter 
                                           FROM AccLedgerAccount 
                                           WHERE idCostCenter = @idCostCenter  ");
            //and bookingYear = '" + bookYear + "'
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idCostCenter", SqlDbType.NVarChar);
            sqlParameters[0].Value = idCostCenter;

            return conn.executeSelectQuery(query, sqlParameters);
        }


        public bool Save(LedgerAccountModel ledgermodel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO  AccLedgerAccount (numberLedgerAccount, descLedgerAccount, openDebitAccount, openCreditAccount, 
                                 accountTypeAccount, idCostCenter, mandatoryCostAccount , mandatoryDebitorAccount , mandatoryCreditorAccount ,mandatoryProjectAccount, isBudgetAccount,
                                 class1Account, class2Account,class3Account,class4Account,class5Account,debitAccount,creditAccount,transactionNoAccount,valutaDebitLedgerAccount,valutaCreditLedgerAccount,
                                 isBTWLedgerAccount,isActiveLedgerAccount,sideBooking,isBlockMemorial,btwId, bookingYear )
                        Values ( @numberLedgerAccount,  @descLedgerAccount, @openDebitAccount,  @openCreditAccount,
                                @accountTypeAccount,  @idCostCenter, @mandatoryCostAccount,  @mandatoryDebitorAccount, @mandatoryCreditorAccount,@mandatoryProjectAccount,@isBudgetAccount,
                                @class1Account, @class2Account,@class3Account,@class4Account,@class5Account,@debitAccount,@creditAccount,@transactionNoAccount,@valutaDebitLedgerAccount,@valutaCreditLedgerAccount,
                                 @isBTWLedgerAccount,@isActiveLedgerAccount,@sideBooking,@isBlockMemorial,@btwId,@bookingYear )");


            SqlParameter[] sqlParameter = new SqlParameter[27];

            sqlParameter[0] = new SqlParameter("@numberLedgerAccount", SqlDbType.Char); 
            sqlParameter[0].Value = ledgermodel.numberLedgerAccount;

            sqlParameter[1] = new SqlParameter("@descLedgerAccount", SqlDbType.NVarChar);
            sqlParameter[1].Value = (ledgermodel.descLedgerAccount == null) ? SqlString.Null : ledgermodel.descLedgerAccount;

            sqlParameter[2] = new SqlParameter("@openDebitAccount", SqlDbType.Decimal);
            sqlParameter[2].Value = ledgermodel.openDebitAccount;

            sqlParameter[3] = new SqlParameter("@openCreditAccount", SqlDbType.Decimal);
            sqlParameter[3].Value = ledgermodel.openCreditAccount;

            sqlParameter[4] = new SqlParameter("@accountTypeAccount", SqlDbType.Decimal);
            sqlParameter[4].Value = ledgermodel.accountTypeAccount;

            sqlParameter[5] = new SqlParameter("@idCostCenter", SqlDbType.NVarChar);
            sqlParameter[5].Value = (ledgermodel.idCostCenter == null) ? SqlString.Null : ledgermodel.idCostCenter;

            sqlParameter[6] = new SqlParameter("@mandatoryCostAccount", SqlDbType.Bit);
            sqlParameter[6].Value = ledgermodel.mandatoryCostAccount;

            sqlParameter[7] = new SqlParameter("@mandatoryDebitorAccount", SqlDbType.Bit);
            sqlParameter[7].Value = ledgermodel.mandatoryDebitorAccount;

            sqlParameter[8] = new SqlParameter("@mandatoryCreditorAccount", SqlDbType.Bit);
            sqlParameter[8].Value = ledgermodel.mandatoryCreditorAccount;

            sqlParameter[9] = new SqlParameter("@mandatoryProjectAccount", SqlDbType.Bit);
            sqlParameter[9].Value = ledgermodel.mandatoryProjectAccount;

            sqlParameter[10] = new SqlParameter("@isBudgetAccount", SqlDbType.Bit);
            sqlParameter[10].Value = ledgermodel.isBudgetAccount;

            sqlParameter[11] = new SqlParameter("@class1Account", SqlDbType.Int);
            sqlParameter[11].Value = (ledgermodel.class1Account == 0) ? SqlInt32.Null : ledgermodel.class1Account;

            sqlParameter[12] = new SqlParameter("@class2Account", SqlDbType.Int);
            sqlParameter[12].Value = (ledgermodel.class2Account == 0) ? SqlInt32.Null : ledgermodel.class2Account;
                    
            sqlParameter[13] = new SqlParameter("@debitAccount", SqlDbType.Decimal);
            sqlParameter[13].Value = ledgermodel.debitAccount;

            sqlParameter[14] = new SqlParameter("@creditAccount", SqlDbType.Decimal);
            sqlParameter[14].Value = ledgermodel.creditAccount;

            sqlParameter[15] = new SqlParameter("@transactionNoAccount", SqlDbType.Int);
            sqlParameter[15].Value = (ledgermodel.transactionNoAccount == 0) ? SqlInt32.Null : ledgermodel.transactionNoAccount;

            sqlParameter[16] = new SqlParameter("@valutaDebitLedgerAccount", SqlDbType.Decimal);
            sqlParameter[16].Value = ledgermodel.valutaDebitLedgerAccount;

            sqlParameter[17] = new SqlParameter("@valutaCreditLedgerAccount", SqlDbType.Decimal);
            sqlParameter[17].Value = ledgermodel.valutaCreditLedgerAccount;

            sqlParameter[18] = new SqlParameter("@isBTWLedgerAccount", SqlDbType.Bit);
            sqlParameter[18].Value = ledgermodel.isBTWLedgerAccount;

            sqlParameter[19] = new SqlParameter("@isActiveLedgerAccount", SqlDbType.Bit);
            sqlParameter[19].Value = ledgermodel.isActiveLedgerAccount;

            sqlParameter[20] = new SqlParameter("@sideBooking", SqlDbType.Char);
            sqlParameter[20].Value = ledgermodel.sideBooking;

            sqlParameter[21] = new SqlParameter("@class3Account", SqlDbType.Int);
            sqlParameter[21].Value = (ledgermodel.class3Account == 0) ? SqlInt32.Null : ledgermodel.class3Account;

            sqlParameter[22] = new SqlParameter("@class4Account", SqlDbType.Int);
            sqlParameter[22].Value = (ledgermodel.class4Account == 0) ? SqlInt32.Null : ledgermodel.class4Account;

            sqlParameter[23] = new SqlParameter("@class5Account", SqlDbType.Int);
            sqlParameter[23].Value = (ledgermodel.class5Account == 0) ? SqlInt32.Null : ledgermodel.class5Account;

            sqlParameter[24] = new SqlParameter("@isBlockMemorial", SqlDbType.Bit);
            sqlParameter[24].Value = ledgermodel.isBlockMemorial;

            sqlParameter[25] = new SqlParameter("@btwId", SqlDbType.Int);
            sqlParameter[25].Value = ledgermodel.btwId;

            sqlParameter[26] = new SqlParameter("@bookingYear", SqlDbType.NVarChar);
            sqlParameter[26].Value = ledgermodel.bookingYear;

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
            sqlParameter[4].Value = conn.GetLastTableID("AccLedgerAccount") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idLedgerAccount";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccLedgerAccount";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save ledger account";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
        public bool Update(LedgerAccountModel ledgermodel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE AccLedgerAccount SET   numberLedgerAccount=@numberLedgerAccount, descLedgerAccount=@descLedgerAccount,
                                openDebitAccount=@openDebitAccount, openCreditAccount=@openCreditAccount,accountTypeAccount= @accountTypeAccount, 
                                idCostCenter=@idCostCenter, mandatoryCostAccount=@mandatoryCostAccount, mandatoryDebitorAccount=@mandatoryDebitorAccount,
                                mandatoryCreditorAccount=@mandatoryCreditorAccount,mandatoryProjectAccount=@mandatoryProjectAccount, isBudgetAccount=@isBudgetAccount,class1Account=@class1Account,
                                class2Account=@class2Account,class3Account=@class3Account,class4Account=@class4Account,class5Account=@class5Account,
                                debitAccount=@debitAccount,creditAccount=@creditAccount,transactionNoAccount=@transactionNoAccount,
                                valutaDebitLedgerAccount=@valutaDebitLedgerAccount,valutaCreditLedgerAccount=@valutaCreditLedgerAccount,
                                isBTWLedgerAccount=@isBTWLedgerAccount,isActiveLedgerAccount=@isActiveLedgerAccount,sideBooking=@sideBooking,isBlockMemorial=@isBlockMemorial,
                                btwId=@btwId, bookingYear=@bookingYear
                                WHERE idLedgerAccount=@idLedgerAccount  ");

            //and  bookingYear = '" + bookYear + "'

            SqlParameter[] sqlParameter = new SqlParameter[28];

            sqlParameter[0] = new SqlParameter("@numberLedgerAccount", SqlDbType.Char);
            sqlParameter[0].Value = ledgermodel.numberLedgerAccount;

            sqlParameter[1] = new SqlParameter("@descLedgerAccount", SqlDbType.NVarChar);
            sqlParameter[1].Value = (ledgermodel.descLedgerAccount == null) ? SqlString.Null : ledgermodel.descLedgerAccount;

            sqlParameter[2] = new SqlParameter("@openDebitAccount", SqlDbType.Decimal);
            sqlParameter[2].Value = ledgermodel.openDebitAccount;

            sqlParameter[3] = new SqlParameter("@openCreditAccount", SqlDbType.Decimal);
            sqlParameter[3].Value = ledgermodel.openCreditAccount;

            sqlParameter[4] = new SqlParameter("@accountTypeAccount", SqlDbType.Decimal);
            sqlParameter[4].Value = ledgermodel.accountTypeAccount;

            sqlParameter[5] = new SqlParameter("@idCostCenter", SqlDbType.NVarChar);
            sqlParameter[5].Value = (ledgermodel.idCostCenter == null) ? SqlString.Null : ledgermodel.idCostCenter;

            sqlParameter[6] = new SqlParameter("@mandatoryCostAccount", SqlDbType.Bit);
            sqlParameter[6].Value = ledgermodel.mandatoryCostAccount;

            sqlParameter[7] = new SqlParameter("@mandatoryDebitorAccount", SqlDbType.Bit);
            sqlParameter[7].Value = ledgermodel.mandatoryDebitorAccount;

            sqlParameter[8] = new SqlParameter("@mandatoryCreditorAccount", SqlDbType.Bit);
            sqlParameter[8].Value = ledgermodel.mandatoryCreditorAccount;

            sqlParameter[9] = new SqlParameter("@mandatoryProjectAccount", SqlDbType.Bit);
            sqlParameter[9].Value = ledgermodel.mandatoryProjectAccount;

            sqlParameter[10] = new SqlParameter("@isBudgetAccount", SqlDbType.Bit);
            sqlParameter[10].Value = ledgermodel.isBudgetAccount;

            sqlParameter[11] = new SqlParameter("@class1Account", SqlDbType.Int);
            sqlParameter[11].Value = (ledgermodel.class1Account == 0) ? SqlInt32.Null : ledgermodel.class1Account;

            sqlParameter[12] = new SqlParameter("@class2Account", SqlDbType.Int);
            sqlParameter[12].Value = (ledgermodel.class2Account == 0) ? SqlInt32.Null : ledgermodel.class2Account;

            sqlParameter[13] = new SqlParameter("@debitAccount", SqlDbType.Decimal);
            sqlParameter[13].Value = ledgermodel.debitAccount;

            sqlParameter[14] = new SqlParameter("@creditAccount", SqlDbType.Decimal);
            sqlParameter[14].Value = ledgermodel.creditAccount;

            sqlParameter[15] = new SqlParameter("@transactionNoAccount", SqlDbType.Int);
            sqlParameter[15].Value = (ledgermodel.transactionNoAccount == 0) ? SqlInt32.Null : ledgermodel.transactionNoAccount;

            sqlParameter[16] = new SqlParameter("@valutaDebitLedgerAccount", SqlDbType.Decimal);
            sqlParameter[16].Value = ledgermodel.valutaDebitLedgerAccount;

            sqlParameter[17] = new SqlParameter("@valutaCreditLedgerAccount", SqlDbType.Decimal);
            sqlParameter[17].Value = ledgermodel.valutaCreditLedgerAccount;

            sqlParameter[18] = new SqlParameter("@isBTWLedgerAccount", SqlDbType.Bit);
            sqlParameter[18].Value = ledgermodel.isBTWLedgerAccount;

            sqlParameter[19] = new SqlParameter("@isActiveLedgerAccount", SqlDbType.Bit);
            sqlParameter[19].Value = ledgermodel.isActiveLedgerAccount;

            sqlParameter[20] = new SqlParameter("@sideBooking", SqlDbType.Char);
            sqlParameter[20].Value = ledgermodel.sideBooking;

            sqlParameter[21] = new SqlParameter("@idLedgerAccount", SqlDbType.Int);
            sqlParameter[21].Value = ledgermodel.idLedgerAccount;

            sqlParameter[22] = new SqlParameter("@class3Account", SqlDbType.Int);
            sqlParameter[22].Value = (ledgermodel.class3Account == 0) ? SqlInt32.Null : ledgermodel.class3Account;

            sqlParameter[23] = new SqlParameter("@class4Account", SqlDbType.Int);
            sqlParameter[23].Value = (ledgermodel.class4Account == 0) ? SqlInt32.Null : ledgermodel.class4Account;

            sqlParameter[24] = new SqlParameter("@class5Account", SqlDbType.Int);
            sqlParameter[24].Value = (ledgermodel.class5Account == 0) ? SqlInt32.Null : ledgermodel.class5Account;

            sqlParameter[25] = new SqlParameter("@isBlockMemorial", SqlDbType.Bit);
            sqlParameter[25].Value = ledgermodel.isBlockMemorial;

            sqlParameter[26] = new SqlParameter("@btwId", SqlDbType.Int);
            sqlParameter[26].Value = ledgermodel.btwId;

            sqlParameter[27] = new SqlParameter("@bookingYear", SqlDbType.NVarChar);
            sqlParameter[27].Value = ledgermodel.bookingYear;

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
            sqlParameter[4].Value = ledgermodel.idLedgerAccount;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idLedgerAccount";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccLedgerAccount";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update ledger account";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

         //NOVO  20 04
        public DataTable GetAllLedgerDataPrint(string from, string to, string nameUser)
        {
            string query = string.Format(@"SELECT numberLedgerAccount, descLedgerAccount,CASE WHEN descCost IS NULL THEN '' ELSE descCost END AS descCost,
                                           CASE WHEN mandatoryCostAccount = 1 THEN 'x' ELSE '' END as mandatoryCostAccount,
                                           CASE WHEN mandatoryDebitorAccount = 1 THEN 'x' ELSE '' END as mandatoryDebitorAccount,
                                           CASE WHEN mandatoryCreditorAccount = 1 THEN 'x' ELSE '' END as mandatoryCreditorAccount,
                                           CASE WHEN mandatoryProjectAccount = 1 THEN 'x' ELSE '' END as mandatoryProjectAccount,
                                           CASE WHEN isBudgetAccount = 1 THEN 'x' ELSE '' END as isBudgetAccount,
                                           CASE WHEN alc.descClass IS NULL THEN '' ELSE alc.descClass END as accountclass1,
                                           CASE WHEN alc2.descClass IS NULL THEN '' ELSE alc2.descClass END as accountclass2,
                                           CASE WHEN alc3.descClass IS NULL THEN '' ELSE alc3.descClass END as accountclass3,
                                           CASE WHEN alc4.descClass IS NULL THEN '' ELSE alc4.descClass END as accountclass4,
                                           CASE WHEN alc5.descClass IS NULL THEN '' ELSE alc5.descClass END as accountclass5,
                                           CASE WHEN isBTWLedgerAccount = 1 THEN 'x' ELSE '' END as isBTWLedgerAccount,
                                           CASE WHEN isActiveLedgerAccount = 1 THEN 'x' ELSE '' END as isActiveLedgerAccount,
                                           CASE WHEN isBTWLedgerAccount = 1 THEN 'x' ELSE '' END as isBTWLedgerAccount,
                                           CASE WHEN isActiveLedgerAccount = 1 THEN 'x' ELSE '' END as  isActiveLedgerAccount,
                                           CASE WHEN sideBooking = 'c' THEN 'C' ELSE (CASE WHEN sideBooking = 'd' THEN 'D' ELSE '' END) END as sideBooking,
                                           CASE WHEN isBlockMemorial = 1 THEN 'x' ELSE '' END as isBlockMemorial,'" + nameUser + @"' as nameUser, '" + from + @"' as [from],'" + to + @"' as [to] , 
                                           bookingYear
                                           FROM AccLedgerAccount a
                                           LEFT OUTER JOIN AccCost ac ON a.idCostCenter = ac.idCost
                                           LEFT OUTER JOIN AccLedgerClass alc ON class1Account = alc.idClass  
                                           LEFT OUTER JOIN AccLedgerClass alc2 ON class2Account = alc2.idClass 
                                           LEFT OUTER JOIN AccLedgerClass alc3 ON class3Account = alc3.idClass 
                                           LEFT OUTER JOIN AccLedgerClass alc4 ON class4Account = alc4.idClass 
                                           LEFT OUTER JOIN AccLedgerClass alc5 ON class5Account = alc5.idClass 
                                            WHERE  a.numberLedgerAccount BETWEEN '" + from.ToString().Trim() + "' AND '" + to.ToString().Trim() + "'order by numberLedgerAccount ");

            //a.bookingYear = '" + bookYear + "' and

            return conn.executeSelectQuery(query, null);

        }
    }
}