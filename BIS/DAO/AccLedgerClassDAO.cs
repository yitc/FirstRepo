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
    public class AccLedgerClassDAO
    {
        private dbConnection conn;

        public AccLedgerClassDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllClass()
        {
            string query = string.Format(@"SELECT idClass,codeClass,descClass, levelClass,orderClass,userCreated,dtCreated,userModified,dtModified FROM AccLedgerClass");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetCostByLevel(int level)
        {
            string query = string.Format(@"SELECT idClass,codeClass,descClass, levelClass,orderClass,userCreated,dtCreated,userModified,dtModified  FROM AccLedgerClass WHERE levelClass = @level");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@level", SqlDbType.Int);
            sqlParameters[0].Value = level;

           // DataTable dt = conn.executeSelectQuery(query, sqlParameters);
           // return level;
            return conn.executeSelectQuery(query, sqlParameters); 
        }

        public DataTable GetClassById(int idClass)
        {
            string query = string.Format(@"SELECT idClass,codeClass,descClass, levelClass,orderClass,userCreated,dtCreated,userModified,dtModified  FROM AccLedgerClass WHERE idClass = @idClass");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idClass", SqlDbType.Int);
            sqlParameters[0].Value = idClass;

            // DataTable dt = conn.executeSelectQuery(query, sqlParameters);
            // return level;
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public Boolean Save(AccLedgerClassModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO AccLedgerClass (codeClass,descClass,levelClass,orderClass,userCreated,dtCreated,userModified,dtModified) 
                      VALUES(@codeClass, @descClass,@levelClass,@orderClass,@userCreated,@dtCreated,@userModified,@dtModified)");


            SqlParameter[] sqlParameter = new SqlParameter[8];
            sqlParameter[0] = new SqlParameter("@codeClass", SqlDbType.NVarChar);
            sqlParameter[0].Value = model.codeClass;

            sqlParameter[1] = new SqlParameter("@descClass", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.descClass;

            sqlParameter[2] = new SqlParameter("@levelClass", SqlDbType.Int);
            sqlParameter[2].Value = model.levelClass;

            sqlParameter[3] = new SqlParameter("@orderClass", SqlDbType.Int);
            sqlParameter[3].Value = model.orderClass;

            sqlParameter[4] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[4].Value = model.userCreated;

            sqlParameter[5] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[5].Value = DateTime.Now;//model.dtCreated;

            sqlParameter[6] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[6].Value = model.userModified;

            sqlParameter[7] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[7].Value = model.dtModified;



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
            sqlParameter[4].Value = conn.GetLastTableID("AccLedgerClass") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idClass";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccLedgerClass";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save acc ledger class ";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean Update(AccLedgerClassModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE AccLedgerClass SET codeClass=@codeClass, descClass = @descClass,
                                levelClass = @levelClass, orderClass=@orderClass,
                                userCreated=@userCreated,dtCreated=@dtCreated,userModified=@userModified,dtModified=@dtModified
                                WHERE  idClass=@idClass ");


            SqlParameter[] sqlParameter = new SqlParameter[9];

            sqlParameter[0] = new SqlParameter("@idClass", SqlDbType.Int);
            sqlParameter[0].Value = model.idClass;

            sqlParameter[1] = new SqlParameter("@codeClass", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.codeClass;

            sqlParameter[2] = new SqlParameter("@descClass", SqlDbType.NVarChar);
            sqlParameter[2].Value = model.descClass;

            sqlParameter[3] = new SqlParameter("@levelClass", SqlDbType.Int);
            sqlParameter[3].Value = model.levelClass;

            sqlParameter[4] = new SqlParameter("@orderClass", SqlDbType.Int);
            sqlParameter[4].Value = model.orderClass;

            sqlParameter[5] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[5].Value = model.userCreated;

            sqlParameter[6] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[6].Value = model.dtCreated;

            sqlParameter[7] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[7].Value = model.userModified;

            sqlParameter[8] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[8].Value = DateTime.Now;  //model.dtModified;


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
            sqlParameter[4].Value =  model.idClass;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idClass";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccLedgerClass";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update acc ledger class";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public DataTable GetBalansSelectionReport(DateTime dateFrom, DateTime dateTo)
        {
            string query = string.Format(@"SELECT distinct
                        class1Account,class2Account,class3Account,class4Account,class5Account,   
                        cc1.descClass as class1,cc2.descClass as class2,
                        cc3.descClass as class3,cc4.descClass as class4,cc5.descClass as class5,numberLedgerAccount,descLedgerAccount, 
                        CASE WHEN cdBegin.sumBeginCredit IS NOT NULL THEN cdBegin.sumBeginCredit ELSE 0 END as beginCredit,
                        CASE WHEN cdBegin.sumBeginDebit IS NOT NULL THEN cdBegin.sumBeginDebit ELSE 0 END as beginDebit,
                        CASE WHEN cd.sumCredit IS NOT NULL THEN cd.sumCredit ELSE 0 END as sumCredit,
                        CASE WHEN cd.sumDebit IS NOT NULL THEN cd.sumDebit ELSE 0 END as sumDebit
                        
                        FROM AccLedgerAccount  acl
                        LEFT OUTER JOIN  AccCost acc on acl.idCostCenter = acc.codeCost
                        LEFT OUTER JOIN  AccLedgerClass cc1 on acl.class1Account = cc1.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc2 on acl.class2Account = cc2.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc3 on acl.class3Account = cc3.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc4 on acl.class4Account = cc4.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc5 on acl.class5Account = cc5.idClass
                       
                        LEFT OUTER  JOIN AccLine m on acl.numberLedgerAccount = m.numberLedAccount 
                      
                        LEFT OUTER JOIN (select sum(debitLine)as sumBeginDebit,sum(creditLine)as sumBeginCredit,numberLedAccount 
                                         FROM AccLine WHERE  dtLine<'" + dateFrom.ToString("MM/dd/yyyy") + @"' AND  YEAR(dtLine)=YEAR('" + dateFrom.ToString("MM/dd/yyyy") + @"')
                                         group by numberLedAccount) cdBegin on  acl.numberLedgerAccount = cdBegin.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)as sumDebit,sum(creditLine)as sumCredit,numberLedAccount 
                                         FROM AccLine WHERE  dtLine >= '" + dateFrom.ToString("MM/dd/yyyy") + "' AND dtLine<='" + dateTo.ToString("MM/dd/yyyy") + @"'
                                         group by numberLedAccount) cd on  acl.numberLedgerAccount = cd.numberLedAccount 
                        WHERE  accountTypeAccount='1' and  dtLine >= '" + dateFrom.ToString("MM/dd/yyyy") + "' AND dtLine<='" + dateTo.ToString("MM/dd/yyyy") + "'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetWinstSelectionReport(DateTime dateFrom, DateTime dateTo)
        {

            string query = string.Format(@"SELECT distinct
                        class1Account,class2Account,class3Account,class4Account,class5Account,   
                        cc1.descClass as class1,cc2.descClass as class2,
                        cc3.descClass as class3,cc4.descClass as class4,cc5.descClass as class5,numberLedgerAccount,descLedgerAccount, 
                        CASE WHEN cdBegin.sumBeginCredit IS NOT NULL THEN cdBegin.sumBeginCredit ELSE 0 END as beginCredit,
                        CASE WHEN cdBegin.sumBeginDebit IS NOT NULL THEN cdBegin.sumBeginDebit ELSE 0 END as beginDebit,
                        CASE WHEN cd.sumCredit IS NOT NULL THEN cd.sumCredit ELSE 0 END as sumCredit,
                        CASE WHEN cd.sumDebit IS NOT NULL THEN cd.sumDebit ELSE 0 END as sumDebit
                        
                        FROM AccLedgerAccount  acl
                        LEFT OUTER JOIN  AccCost acc on acl.idCostCenter = acc.codeCost
                        LEFT OUTER JOIN  AccLedgerClass cc1 on acl.class1Account = cc1.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc2 on acl.class2Account = cc2.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc3 on acl.class3Account = cc3.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc4 on acl.class4Account = cc4.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc5 on acl.class5Account = cc5.idClass
                       
                        LEFT OUTER  JOIN AccLine m on acl.numberLedgerAccount = m.numberLedAccount 
                      
                        LEFT OUTER JOIN (select sum(debitLine)as sumBeginDebit,sum(creditLine)as sumBeginCredit,numberLedAccount 
                                         FROM AccLine WHERE  dtLine<'" + dateFrom.ToString("MM/dd/yyyy") + @"' AND  YEAR(dtLine)=YEAR('" + dateFrom.ToString("MM/dd/yyyy") + @"')
                                         group by numberLedAccount) cdBegin on  acl.numberLedgerAccount = cdBegin.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)as sumDebit,sum(creditLine)as sumCredit,numberLedAccount 
                                         FROM AccLine WHERE  dtLine >= '" + dateFrom.ToString("MM/dd/yyyy") + "' AND dtLine<='" + dateTo.ToString("MM/dd/yyyy") + @"'
                                         group by numberLedAccount) cd on  acl.numberLedgerAccount = cd.numberLedAccount 
                        WHERE  accountTypeAccount='2' and  dtLine >= '" + dateFrom.ToString("MM/dd/yyyy") + "' AND dtLine<='" + dateTo.ToString("MM/dd/yyyy") + "'");


            return conn.executeSelectQuery(query, null);
        }
        /////
        public DataTable BilansPeriodReport(int sta, string year, int startPeriod, int endPeriod)
        {
            int broj = sta;
            string query = "";
            string condition = "";

            if (startPeriod > 0 && endPeriod > 0)
            {

                int count = startPeriod;

                for (int i = startPeriod; i <= endPeriod; i++)
                {
                    if (count == startPeriod)
                    {
                        condition += " AND (period" + i + " >0";
                    }

                    if (count > startPeriod)
                    {
                        condition += " OR period" + i + " >0";
                    }
                    if (count == endPeriod)
                    {
                        condition += ")";
                    }
                    count++;
                }

            }

            if (broj == 3)
            {
                query = string.Format(@"SELECT distinct
                        al.bookingYear, class1Account,class2Account,class3Account,class4Account,class5Account,   
                         cc1.descClass as class1,cc2.descClass as class2,
                         cc3.descClass as class3,cc4.descClass as class4, cc5.descClass as class5,
                         numberLedgerAccount,descLedgerAccount,
                         CASE WHEN period0 is not null then period0 else 0 end as period0, 
                         CASE WHEN period1 is not null then period1 else 0 end as period1,
                         CASE WHEN period2 is not null then period2 else 0 end as period2,
                         CASE WHEN period3 is not null then period3 else 0 end as period3,
                         CASE WHEN period4 is not null then period4 else 0 end as period4,
                         CASE WHEN period5 is not null then period5 else 0 end as period5,
                         CASE WHEN period6 is not null then period6 else 0 end as period6,
                         CASE WHEN period7 is not null then period7 else 0 end as period7,
                         CASE WHEN period8 is not null then period8 else 0 end as period8,
                         CASE WHEN period9 is not null then period9 else 0 end as period9,
                         CASE WHEN period10 is not null then period10 else 0 end as period10,
                         CASE WHEN period11 is not null then period11 else 0 end as period11,
                         CASE WHEN period12 is not null then period12 else 0 end as period12, 'Saldibalans' as typeClassification
                      --  CASE WHEN cdBegin.sumBeginCredit IS NOT NULL THEN cdBegin.sumBeginCredit ELSE 0 END as beginCredit,
                      --  CASE WHEN cdBegin.sumBeginDebit IS NOT NULL THEN cdBegin.sumBeginDebit ELSE 0 END as beginDebit,
                     --   CASE WHEN cd.sumCredit IS NOT NULL THEN cd.sumCredit ELSE 0 END as sumCredit,
                      --  CASE WHEN cd.sumDebit IS NOT NULL THEN cd.sumDebit ELSE 0 END as sumDebit
                        
                       FROM AccLedgerAccount  acl
                       LEFT OUTER JOIN  AccCost acc on acl.idCostCenter = acc.codeCost
                       LEFT OUTER JOIN  AccLedgerClass cc1 on acl.class1Account = cc1.idClass
                       LEFT OUTER JOIN  AccLedgerClass cc2 on acl.class2Account = cc2.idClass
                       LEFT OUTER JOIN  AccLedgerClass cc3 on acl.class3Account = cc3.idClass
                       LEFT OUTER JOIN  AccLedgerClass cc4 on acl.class4Account = cc4.idClass
                       LEFT OUTER JOIN  AccLedgerClass cc5 on acl.class5Account = cc5.idClass
                       
                       LEFT OUTER  JOIN AccLine m on acl.numberLedgerAccount = m.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)- sum(creditLine)as period0,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=0
                                         group by numberLedAccount) cdBegin on  acl.numberLedgerAccount = cdBegin.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period1,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=1
                                         group by numberLedAccount) cd1 on  acl.numberLedgerAccount = cd1.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period2,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=2
                                         group by numberLedAccount) cd2 on  acl.numberLedgerAccount = cd2.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period3,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=3
                                         group by numberLedAccount) cd3 on  acl.numberLedgerAccount = cd3.numberLedAccount                                     
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period4,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=4
                                         group by numberLedAccount) cd4 on  acl.numberLedgerAccount = cd4.numberLedAccount                  
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period5,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=5
                                         group by numberLedAccount) cd5 on  acl.numberLedgerAccount = cd5.numberLedAccount   
                        
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period6,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=6
                                         group by numberLedAccount) cd6 on  acl.numberLedgerAccount = cd6.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period7,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=7
                                         group by numberLedAccount) cd7 on  acl.numberLedgerAccount = cd7.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period8,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=8
                                         group by numberLedAccount) cd8 on  acl.numberLedgerAccount = cd8.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period9,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=9
                                         group by numberLedAccount) cd9 on  acl.numberLedgerAccount = cd9.numberLedAccount
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period10,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=10
                                         group by numberLedAccount) cd10 on  acl.numberLedgerAccount = cd10.numberLedAccount
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period11,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=11
                                         group by numberLedAccount) cd11 on  acl.numberLedgerAccount = cd11.numberLedAccount
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period12,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=12
                                         group by numberLedAccount) cd12 on  acl.numberLedgerAccount = cd12.numberLedAccount
                       LEFT OUTER JOIN AccLine al on al.numberLedAccount = acl.numberLedgerAccount
                        WHERE  accountTypeAccount='1' " + condition + " AND al.bookingYear='" + year + @"'  
                        
                        UNION

                         SELECT distinct
                         al.bookingYear, class1Account,class2Account,class3Account,class4Account,class5Account,   
                         cc1.descClass as class1,cc2.descClass as class2,
                         cc3.descClass as class3,cc4.descClass as class4, cc5.descClass as class5,
                         numberLedgerAccount,descLedgerAccount,
                         CASE WHEN period0 is not null then period0 else 0 end as period0, 
                         CASE WHEN period1 is not null then period1 else 0 end as period1,
                         CASE WHEN period2 is not null then period2 else 0 end as period2,
                         CASE WHEN period3 is not null then period3 else 0 end as period3,
                         CASE WHEN period4 is not null then period4 else 0 end as period4,
                         CASE WHEN period5 is not null then period5 else 0 end as period5,
                         CASE WHEN period6 is not null then period6 else 0 end as period6,
                         CASE WHEN period7 is not null then period7 else 0 end as period7,
                         CASE WHEN period8 is not null then period8 else 0 end as period8,
                         CASE WHEN period9 is not null then period9 else 0 end as period9,
                         CASE WHEN period10 is not null then period10 else 0 end as period10,
                         CASE WHEN period11 is not null then period11 else 0 end as period11,
                         CASE WHEN period12 is not null then period12 else 0 end as period12, 'Winst verlies' as typeClassification
                    
                        
                       FROM AccLedgerAccount  acl
                       LEFT OUTER JOIN  AccCost acc on acl.idCostCenter = acc.codeCost
                       LEFT OUTER JOIN  AccLedgerClass cc1 on acl.class1Account = cc1.idClass
                       LEFT OUTER JOIN  AccLedgerClass cc2 on acl.class2Account = cc2.idClass
                       LEFT OUTER JOIN  AccLedgerClass cc3 on acl.class3Account = cc3.idClass
                       LEFT OUTER JOIN  AccLedgerClass cc4 on acl.class4Account = cc4.idClass
                       LEFT OUTER JOIN  AccLedgerClass cc5 on acl.class5Account = cc5.idClass
                       
                       LEFT OUTER  JOIN AccLine m on acl.numberLedgerAccount = m.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)- sum(creditLine)as period0,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=0
                                         group by numberLedAccount) cdBegin on  acl.numberLedgerAccount = cdBegin.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period1,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=1
                                         group by numberLedAccount) cd1 on  acl.numberLedgerAccount = cd1.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period2,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=2
                                         group by numberLedAccount) cd2 on  acl.numberLedgerAccount = cd2.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period3,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=3
                                         group by numberLedAccount) cd3 on  acl.numberLedgerAccount = cd3.numberLedAccount                                     
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period4,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=4
                                         group by numberLedAccount) cd4 on  acl.numberLedgerAccount = cd4.numberLedAccount                  
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period5,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=5
                                         group by numberLedAccount) cd5 on  acl.numberLedgerAccount = cd5.numberLedAccount   
                        
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period6,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=6
                                         group by numberLedAccount) cd6 on  acl.numberLedgerAccount = cd6.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period7,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=7
                                         group by numberLedAccount) cd7 on  acl.numberLedgerAccount = cd7.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period8,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=8
                                         group by numberLedAccount) cd8 on  acl.numberLedgerAccount = cd8.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period9,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=9
                                         group by numberLedAccount) cd9 on  acl.numberLedgerAccount = cd9.numberLedAccount
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period10,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=10
                                         group by numberLedAccount) cd10 on  acl.numberLedgerAccount = cd10.numberLedAccount
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period11,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=11
                                         group by numberLedAccount) cd11 on  acl.numberLedgerAccount = cd11.numberLedAccount
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period12,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=12
                                         group by numberLedAccount) cd12 on  acl.numberLedgerAccount = cd12.numberLedAccount
                       LEFT OUTER JOIN AccLine al on al.numberLedAccount = acl.numberLedgerAccount                        
                        WHERE  accountTypeAccount='2' " + condition + " AND al.bookingYear='" + year + "'  ");
            }
            else
            {

                query = string.Format(@"SELECT distinct
                        al.bookingYear, class1Account,class2Account,class3Account,class4Account,class5Account,   
                         cc1.descClass as class1,cc2.descClass as class2,
                         cc3.descClass as class3,cc4.descClass as class4, cc5.descClass as class5,
                         numberLedgerAccount,descLedgerAccount,
                         CASE WHEN period0 is not null then period0 else 0 end as period0, 
                         CASE WHEN period1 is not null then period1 else 0 end as period1,
                         CASE WHEN period2 is not null then period2 else 0 end as period2,
                         CASE WHEN period3 is not null then period3 else 0 end as period3,
                         CASE WHEN period4 is not null then period4 else 0 end as period4,
                         CASE WHEN period5 is not null then period5 else 0 end as period5,
                         CASE WHEN period6 is not null then period6 else 0 end as period6,
                         CASE WHEN period7 is not null then period7 else 0 end as period7,
                         CASE WHEN period8 is not null then period8 else 0 end as period8,
                         CASE WHEN period9 is not null then period9 else 0 end as period9,
                         CASE WHEN period10 is not null then period10 else 0 end as period10,
                         CASE WHEN period11 is not null then period11 else 0 end as period11,
                         CASE WHEN period12 is not null then period12 else 0 end as period12
                      --  CASE WHEN cdBegin.sumBeginCredit IS NOT NULL THEN cdBegin.sumBeginCredit ELSE 0 END as beginCredit,
                      --  CASE WHEN cdBegin.sumBeginDebit IS NOT NULL THEN cdBegin.sumBeginDebit ELSE 0 END as beginDebit,
                     --   CASE WHEN cd.sumCredit IS NOT NULL THEN cd.sumCredit ELSE 0 END as sumCredit,
                      --  CASE WHEN cd.sumDebit IS NOT NULL THEN cd.sumDebit ELSE 0 END as sumDebit
                        
                       FROM AccLedgerAccount  acl
                       LEFT OUTER JOIN  AccCost acc on acl.idCostCenter = acc.codeCost
                       LEFT OUTER JOIN  AccLedgerClass cc1 on acl.class1Account = cc1.idClass
                       LEFT OUTER JOIN  AccLedgerClass cc2 on acl.class2Account = cc2.idClass
                       LEFT OUTER JOIN  AccLedgerClass cc3 on acl.class3Account = cc3.idClass
                       LEFT OUTER JOIN  AccLedgerClass cc4 on acl.class4Account = cc4.idClass
                       LEFT OUTER JOIN  AccLedgerClass cc5 on acl.class5Account = cc5.idClass
                       
                       LEFT OUTER  JOIN AccLine m on acl.numberLedgerAccount = m.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)- sum(creditLine)as period0,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=0
                                         group by numberLedAccount) cdBegin on  acl.numberLedgerAccount = cdBegin.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period1,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=1
                                         group by numberLedAccount) cd1 on  acl.numberLedgerAccount = cd1.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period2,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=2
                                         group by numberLedAccount) cd2 on  acl.numberLedgerAccount = cd2.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period3,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=3
                                         group by numberLedAccount) cd3 on  acl.numberLedgerAccount = cd3.numberLedAccount                                     
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period4,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=4
                                         group by numberLedAccount) cd4 on  acl.numberLedgerAccount = cd4.numberLedAccount                  
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period5,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=5
                                         group by numberLedAccount) cd5 on  acl.numberLedgerAccount = cd5.numberLedAccount   
                        
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period6,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=6
                                         group by numberLedAccount) cd6 on  acl.numberLedgerAccount = cd6.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period7,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=7
                                         group by numberLedAccount) cd7 on  acl.numberLedgerAccount = cd7.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period8,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=8
                                         group by numberLedAccount) cd8 on  acl.numberLedgerAccount = cd8.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period9,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=9
                                         group by numberLedAccount) cd9 on  acl.numberLedgerAccount = cd9.numberLedAccount
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period10,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=10
                                         group by numberLedAccount) cd10 on  acl.numberLedgerAccount = cd10.numberLedAccount
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period11,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=11
                                         group by numberLedAccount) cd11 on  acl.numberLedgerAccount = cd11.numberLedAccount
                       LEFT OUTER JOIN (select sum(debitLine)-sum(creditLine)as period12,numberLedAccount 
                                         FROM AccLine WHERE  periodLine=12
                                         group by numberLedAccount) cd12 on  acl.numberLedgerAccount = cd12.numberLedAccount
                       LEFT OUTER JOIN AccLine al on al.numberLedAccount = acl.numberLedgerAccount
                                         
                            
        
                        
                        WHERE  accountTypeAccount='" + sta.ToString() + "' " + condition + " AND al.bookingYear='" + year + "'  ");
            }


            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetBalansTotalReport(DateTime dateFrom, DateTime dateTo)
        {


            string query = string.Format(@"SELECT distinct
                        class1Account,class2Account,class3Account,class4Account,class5Account,   
                        cc1.descClass as class1,cc2.descClass as class2,
                        cc3.descClass as class3,cc4.descClass as class4,cc5.descClass as class5,numberLedgerAccount,descLedgerAccount as description,
                        CASE WHEN cdBegin.sumBeginCredit IS NOT NULL THEN cdBegin.sumBeginCredit ELSE 0 END as beginCredit,
                        CASE WHEN cdBegin.sumBeginDebit IS NOT NULL THEN cdBegin.sumBeginDebit ELSE 0 END as beginDebit,
                        CASE WHEN cd.sumCredit IS NOT NULL THEN cd.sumCredit ELSE 0 END as sumCredit,
                        CASE WHEN cd.sumDebit IS NOT NULL THEN cd.sumDebit ELSE 0 END as sumDebit, 'Saldibalans' as type
                        
                         FROM AccLedgerAccount  acl
                        LEFT OUTER JOIN  AccCost acc on acl.idCostCenter = acc.codeCost
                        LEFT OUTER JOIN  AccLedgerClass cc1 on acl.class1Account = cc1.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc2 on acl.class2Account = cc2.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc3 on acl.class3Account = cc3.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc4 on acl.class4Account = cc4.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc5 on acl.class5Account = cc5.idClass
                       
                        LEFT OUTER  JOIN AccLine m on acl.numberLedgerAccount = m.numberLedAccount 
                      
                        LEFT OUTER JOIN (select sum(debitLine)as sumBeginDebit,sum(creditLine)as sumBeginCredit,numberLedAccount 
                                         FROM AccLine WHERE  dtLine<'" + dateFrom.ToString("MM/dd/yyyy") + @"' AND  YEAR(dtLine)=YEAR('" + dateFrom.ToString("MM/dd/yyyy") + @"')
                                         group by numberLedAccount) cdBegin on  acl.numberLedgerAccount = cdBegin.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)as sumDebit,sum(creditLine)as sumCredit,numberLedAccount 
                                         FROM AccLine WHERE  dtLine >= '" + dateFrom.ToString("MM/dd/yyyy") + "' AND dtLine<='" + dateTo.ToString("MM/dd/yyyy") + @"'
                                         group by numberLedAccount) cd on  acl.numberLedgerAccount = cd.numberLedAccount 
                        WHERE  accountTypeAccount='1'   and  dtLine >= '" + dateFrom.ToString("MM/dd/yyyy") + "' AND dtLine<='" + dateTo.ToString("MM/dd/yyyy") + @"'
                        UNION
                        SELECT distinct
                        class1Account,class2Account,class3Account,class4Account,class5Account,   
                        cc1.descClass as class1,cc2.descClass as class2,
                        cc3.descClass as class3,cc4.descClass as class4,cc5.descClass as class5,numberLedgerAccount,descLedgerAccount as description,
                        CASE WHEN cdBegin.sumBeginCredit IS NOT NULL THEN cdBegin.sumBeginCredit ELSE 0 END as beginCredit,
                        CASE WHEN cdBegin.sumBeginDebit IS NOT NULL THEN cdBegin.sumBeginDebit ELSE 0 END as beginDebit,
                        CASE WHEN cd.sumCredit IS NOT NULL THEN cd.sumCredit ELSE 0 END as sumCredit,
                        CASE WHEN cd.sumDebit IS NOT NULL THEN cd.sumDebit ELSE 0 END as sumDebit, 'Saldibalans' as type
                        
                        FROM 
                         (select sum(debitLine)as sumBeginDebit,sum(creditLine)as sumBeginCredit,numberLedAccount 
                                         FROM AccLine WHERE  dtLine<'" + dateFrom.ToString("MM/dd/yyyy") + "' AND  YEAR(dtLine)=YEAR('" + dateFrom.ToString("MM/dd/yyyy") + @"')
                                         group by numberLedAccount) cdBegin  
                         LEFT OUTER JOIN AccLedgerAccount  acl on  acl.numberLedgerAccount = cdBegin.numberLedAccount
                        LEFT OUTER JOIN  AccCost acc on acl.idCostCenter = acc.codeCost
                        LEFT OUTER JOIN  AccLedgerClass cc1 on acl.class1Account = cc1.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc2 on acl.class2Account = cc2.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc3 on acl.class3Account = cc3.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc4 on acl.class4Account = cc4.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc5 on acl.class5Account = cc5.idClass
                       
                        LEFT OUTER  JOIN AccLine m on acl.numberLedgerAccount = m.numberLedAccount 
                      
                       LEFT OUTER JOIN (select sum(debitLine)as sumDebit,sum(creditLine)as sumCredit,numberLedAccount 
                                         FROM AccLine WHERE  dtLine >= '" + dateFrom.ToString("MM/dd/yyyy") + "' AND dtLine<='" + dateTo.ToString("MM/dd/yyyy") + @"'
                                         group by numberLedAccount) cd on  acl.numberLedgerAccount = cd.numberLedAccount 
                        WHERE  accountTypeAccount='1'    AND dtLine<'" + dateFrom.ToString("MM/dd/yyyy") + "' AND  YEAR(dtLine)=YEAR('" + dateFrom.ToString("MM/dd/yyyy") + @"')
                       UNION 
                        SELECT distinct
                        class1Account,class2Account,class3Account,class4Account,class5Account,   
                        cc1.descClass as class1,cc2.descClass as class2,
                        cc3.descClass as class3,cc4.descClass as class4,cc5.descClass as class5,numberLedgerAccount,descLedgerAccount as description, 
                        CASE WHEN cdBegin.sumBeginCredit IS NOT NULL THEN cdBegin.sumBeginCredit ELSE 0 END as beginCredit,
                        CASE WHEN cdBegin.sumBeginDebit IS NOT NULL THEN cdBegin.sumBeginDebit ELSE 0 END as beginDebit,
                        CASE WHEN cd.sumCredit IS NOT NULL THEN cd.sumCredit ELSE 0 END as sumCredit,
                        CASE WHEN cd.sumDebit IS NOT NULL THEN cd.sumDebit ELSE 0 END as sumDebit, 'Winst verlies' as type
                        
                        FROM AccLedgerAccount  acl
                        LEFT OUTER JOIN  AccCost acc on acl.idCostCenter = acc.codeCost
                        LEFT OUTER JOIN  AccLedgerClass cc1 on acl.class1Account = cc1.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc2 on acl.class2Account = cc2.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc3 on acl.class3Account = cc3.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc4 on acl.class4Account = cc4.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc5 on acl.class5Account = cc5.idClass
                       
                        LEFT OUTER  JOIN AccLine m on acl.numberLedgerAccount = m.numberLedAccount 
                      
                        LEFT OUTER JOIN (select sum(debitLine)as sumBeginDebit,sum(creditLine)as sumBeginCredit,numberLedAccount 
                                         FROM AccLine WHERE  dtLine<'" + dateFrom.ToString("MM/dd/yyyy") + @"' AND  YEAR(dtLine)=YEAR('" + dateFrom.ToString("MM/dd/yyyy") + @"')
                                         group by numberLedAccount) cdBegin on  acl.numberLedgerAccount = cdBegin.numberLedAccount 
                       LEFT OUTER JOIN (select sum(debitLine)as sumDebit,sum(creditLine)as sumCredit,numberLedAccount 
                                         FROM AccLine WHERE  dtLine >= '" + dateFrom.ToString("MM/dd/yyyy") + "' AND dtLine<='" + dateTo.ToString("MM/dd/yyyy") + @"'
                                         group by numberLedAccount) cd on  acl.numberLedgerAccount = cd.numberLedAccount 
                        WHERE  accountTypeAccount='2'  and  dtLine >= '" + dateFrom.ToString("MM/dd/yyyy") + "' AND dtLine<='" + dateTo.ToString("MM/dd/yyyy") + @"' 
                        UNION 
                        SELECT distinct
                        class1Account,class2Account,class3Account,class4Account,class5Account,   
                        cc1.descClass as class1,cc2.descClass as class2,
                        cc3.descClass as class3,cc4.descClass as class4,cc5.descClass as class5,numberLedgerAccount,descLedgerAccount as description, 
                        CASE WHEN cdBegin.sumBeginCredit IS NOT NULL THEN cdBegin.sumBeginCredit ELSE 0 END as beginCredit,
                        CASE WHEN cdBegin.sumBeginDebit IS NOT NULL THEN cdBegin.sumBeginDebit ELSE 0 END as beginDebit,
                        CASE WHEN cd.sumCredit IS NOT NULL THEN cd.sumCredit ELSE 0 END as sumCredit,
                        CASE WHEN cd.sumDebit IS NOT NULL THEN cd.sumDebit ELSE 0 END as sumDebit, 'Winst verlies' as type
                        
                         FROM (select sum(debitLine)as sumBeginDebit,sum(creditLine)as sumBeginCredit,numberLedAccount 
                                         FROM AccLine WHERE dtLine<'" + dateFrom.ToString("MM/dd/yyyy") + "' AND  YEAR(dtLine)=YEAR('" + dateFrom.ToString("MM/dd/yyyy") + @"')
                                         group by numberLedAccount) cdBegin 
                        
                        LEFT OUTER JOIN AccLedgerAccount  acl on  acl.numberLedgerAccount = cdBegin.numberLedAccount 
                        LEFT OUTER JOIN  AccCost acc on acl.idCostCenter = acc.codeCost
                        LEFT OUTER JOIN  AccLedgerClass cc1 on acl.class1Account = cc1.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc2 on acl.class2Account = cc2.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc3 on acl.class3Account = cc3.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc4 on acl.class4Account = cc4.idClass
                        LEFT OUTER JOIN  AccLedgerClass cc5 on acl.class5Account = cc5.idClass
                       
                        LEFT OUTER  JOIN AccLine m on acl.numberLedgerAccount = m.numberLedAccount 
                      
                       
                       LEFT OUTER JOIN (select sum(debitLine)as sumDebit,sum(creditLine)as sumCredit,numberLedAccount 
                                         FROM AccLine WHERE  dtLine >= '" + dateFrom.ToString("MM/dd/yyyy") + "' AND dtLine<='" + dateTo.ToString("MM/dd/yyyy") + @"'
                                         group by numberLedAccount) cd on  acl.numberLedgerAccount = cd.numberLedAccount 
                        WHERE  accountTypeAccount='2'  AND dtLine<'" + dateFrom.ToString("MM/dd/yyyy") + "' AND  YEAR(dtLine)=YEAR('" + dateFrom.ToString("MM/dd/yyyy") + @"')");
            // and dtLine >= '" + dateFrom.ToString("MM/dd/yyyy") + "' AND dtLine<='" + dateTo.ToString("MM/dd/yyyy") + "'
            return conn.executeSelectQuery(query, null);
        }
    }


}