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
    public class AccDebCreDAO
    {
        private dbConnection conn;
        public AccDebCreDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetPersonDebCre(int idContPers)
        {
            string query = string.Format(@"SELECT c.idAccDebCre,c.idContPerson,cp.firstname + '' +cp.midname + '' +cp.lastname as namePerson,
                    c.idClient,c.isDebitor,c.isCreditor,c.debAccount,
                    acd.descLedgerAccount as debNameAccount,c.creditAccount,acc.descLedgerAccount as creNameAccount,c.payCondition,c.iban, c.accNumber
                   
                        FROM AccDebCre c
                        LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = c.idContPerson
                        LEFT OUTER JOIN AccLedgerAccount acd ON acd.numberLedgerAccount = c.debAccount
                        LEFT OUTER JOIN AccLedgerAccount acc ON acc.numberLedgerAccount = c.creditAccount
                        WHERE c.idContPerson ='" + idContPers.ToString() + "' ");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idContPerson", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;
            return conn.executeSelectQuery(query, sqlParameters);

        }
        public DataTable GetCustomerByAccCode(string accNumber)
        {
            string query = string.Format(@"SELECT c.idAccDebCre,c.idContPerson,cp.firstname + '' +cp.midname + '' +cp.lastname as namePerson,
                    c.idClient,c.isDebitor,c.isCreditor,c.debAccount, cc.nameClient,
                    acd.descLedgerAccount as debNameAccount,c.creditAccount,acc.descLedgerAccount as creNameAccount,c.payCondition,c.iban, c.accNumber
                   
                        FROM AccDebCre c
                        LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = c.idContPerson
                        LEFT OUTER JOIN Client cc ON cc.idClient = c.idClient
                        LEFT OUTER JOIN AccLedgerAccount acd ON acd.numberLedgerAccount = c.debAccount
                        LEFT OUTER JOIN AccLedgerAccount acc ON acc.numberLedgerAccount = c.creditAccount
                        WHERE c.accNumber ='" + accNumber.ToString() + "' ");

            //SqlParameter[] sqlParameters = new SqlParameter[1];
            //sqlParameters[0] = new SqlParameter("@idContPerson", SqlDbType.Int);
            //sqlParameters[0].Value = idContPers;
            return conn.executeSelectQuery(query, null);

        }

        public DataTable GetClientDebCre(int idClient)
        {
            string query = string.Format(@"SELECT c.idAccDebCre,c.idContPerson,c.idClient,cl.nameClient,c.isDebitor,c.isCreditor,c.debAccount,
                    acd.descLedgerAccount as debNameAccount,c.creditAccount,acc.descLedgerAccount as creNameAccount,c.payCondition,c.iban,c.accNumber
                   
                        FROM AccDebCre c
                        LEFT OUTER JOIN Client cl ON cl.idClient = c.idClient
                        LEFT OUTER JOIN AccLedgerAccount acd ON acd.numberLedgerAccount = c.debAccount
                        LEFT OUTER JOIN AccLedgerAccount acc ON acc.numberLedgerAccount = c.creditAccount
                        WHERE c.idClient ='" + idClient.ToString() + "' ");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameters[0].Value = idClient;
            return conn.executeSelectQuery(query, sqlParameters);

        }

        public DataTable GetCreditors()
        {
            //            string query = string.Format(@" SELECT distinct c.accNumber, b.firstName + b.midname + b.Lastname as name, d.nameclient as name, ab.street + ab.housenr as [address],am.street + am.housenr as [address],
            //                                        ab.postalCode as zip,ab.city as city, am.postalCode as zip, am.city as city
            //                   
            //                        FROM AccDebCre c
            //                        LEFT OUTER JOIN ContactPerson b ON c.idContPerson = b.idContPers
            //                        LEFT OUTER JOIN ContactPersonAddress ab ON ab.idContPers = b.idContPers
            //                        LEFT OUTER JOIN Client d ON d.idClient = c.idClient
            //                        LEFT OUTER JOIN ClientAddress am ON am.idClient = d.idClient
            //                       
            //                        WHERE c.iscreditor = 1 and (ab.idAddressType = 1 or am.idAddressType = 1) ");

            //SqlParameter[] sqlParameters = new SqlParameter[1];
            //sqlParameters[0] = new SqlParameter("@idClient", SqlDbType.Int);
            //sqlParameters[0].Value = idClient;
            string query = string.Format(@"SELECT DISTINCT accNumber as AccNumber, CASE WHEN firstname is null THEN SPACE(1) else firstname end + ' ' + 
							CASE WHEN midname is null THEN SPACE(1) ELSE midname end + ' ' + 
							CASE WHEN lastname is null THEN SPACE(1) ELSE lastname end as Name,  street + ' ' + housenr as [address], postalCode as zip, city  as city
                            from AccDebCre a
                            LEFT OUTER JOIN ContactPersonAddress ab ON ab.idContPers = a.idContPerson
                            LEFT OUTER JOIN ContactPerson ac ON ac.idContPers = a.idContPerson
                            WHERE (a.idContPerson is Not Null and a.idClient=0 ) and (ab.idAddressType=1) and a.isCreditor = 1  
                            UNION
                            select DISTINCT accNumber as AccNumber, nameClient as Name, street + ' ' +  housenr as [address], postalCode as zip, city as city
                            from AccDebCre a
                            LEFT OUTER JOIN ClientAddress ab ON ab.idClient = a.idClient
                            LEFT OUTER JOIN Client ac ON ac.idClient = a.idClient
                            where a.idClient is not null and idContPerson = 0 and ab.idAddressType=1 and a.isCreditor = 1  ");



            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetCreditorsCredPay()
        {
         
            string query = string.Format(@" select DISTINCT accNumber as AccNumber, nameClient as Name, street + ' ' +  housenr as [address], postalCode as zip, city as city
                            from AccDebCre a
                            LEFT OUTER JOIN ClientAddress ab ON ab.idClient = a.idClient
                            LEFT OUTER JOIN Client ac ON ac.idClient = a.idClient
                            where a.idClient is not null and idContPerson = 0 and ab.idAddressType=1 and a.isCreditor = 1  ");

            
            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetCreditorName(string accnumberaa)
        {

            string query = string.Format(@"SELECT * from AccDebCre   where accNumber = '" + accnumberaa + "' ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetIDByAccNumber(string accnumberaa)
        {

            string query = string.Format(@"SELECT * from AccDebCre   where accNumber = '" + accnumberaa + "' ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetDebitors()
        {
            //            string query = string.Format(@" SELECT distinct c.accNumber, b.firstName + b.midname + b.Lastname as name, d.nameclient as name, ab.street + ab.housenr as [address],am.street + am.housenr as [address],
            //                                        ab.postalCode as zip,ab.city as city, am.postalCode as zip, am.city as city
            //                   
            //                        FROM AccDebCre c
            //                        LEFT OUTER JOIN ContactPerson b ON c.idContPerson = b.idContPers
            //                        LEFT OUTER JOIN ContactPersonAddress ab ON ab.idContPers = b.idContPers
            //                        LEFT OUTER JOIN Client d ON d.idClient = c.idClient
            //                        LEFT OUTER JOIN ClientAddress am ON am.idClient = d.idClient
            //                       
            //                        WHERE c.isdebitor = 1 and (ab.idAddressType = 1 or am.idAddressType = 1) ");

            //SqlParameter[] sqlParameters = new SqlParameter[1];
            //sqlParameters[0] = new SqlParameter("@idClient", SqlDbType.Int);
            //sqlParameters[0].Value = idClient;
            string query = string.Format(@" SELECT DISTINCT accNumber as AccNumber, CASE WHEN firstname is null THEN SPACE(1) else firstname end + ' ' + 
							CASE WHEN midname is null THEN SPACE(1) ELSE midname end + ' ' + 
							CASE WHEN lastname is null THEN SPACE(1) ELSE lastname end as Name,  street + ' ' + housenr as [address], postalCode as zip, city  as city 
                            from AccDebCre a
                            LEFT OUTER JOIN ContactPersonAddress ab ON ab.idContPers = a.idContPerson
                            LEFT OUTER JOIN ContactPerson ac ON ac.idContPers = a.idContPerson
                            WHERE (a.idContPerson is Not Null and a.idClient=0 ) and (ab.idAddressType=1) and a.isDebitor = 1  
                            UNION
                            select DISTINCT accNumber as AccNumber, nameClient as Name, street + ' ' +  housenr as [address], postalCode as zip, city as city 
                            from AccDebCre a
                            LEFT OUTER JOIN ClientAddress ab ON ab.idClient = a.idClient
                            LEFT OUTER JOIN Client ac ON ac.idClient = a.idClient
                            where a.idClient is not null and idContPerson = 0 and ab.idAddressType=1 and a.isDebitor = 1 ");
            return conn.executeSelectQuery(query, null);

        }

        public bool Update(AccDebCreModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE AccDebCre SET   idContPerson=@idContPerson,idClient=@idClient,isDebitor=@isDebitor,isCreditor=@isCreditor,
                                    debAccount=@debAccount, creditAccount=@creditAccount,payCondition=@payCondition,iban=@iban, accNumber=@accNumber
                                    WHERE idAccDebCre =@idAccDebCre");


            SqlParameter[] sqlParameter = new SqlParameter[10];

            sqlParameter[0] = new SqlParameter("@idContPerson", SqlDbType.Int);
            sqlParameter[0].Value = (model.idContPerson == null) ? SqlInt32.Null : model.idContPerson;

            sqlParameter[1] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[1].Value = (model.idClient == null) ? SqlInt32.Null : model.idClient;

            sqlParameter[2] = new SqlParameter("@isDebitor", SqlDbType.Bit);
            sqlParameter[2].Value = model.isDebitor;

            sqlParameter[3] = new SqlParameter("@isCreditor", SqlDbType.Bit);
            sqlParameter[3].Value = model.isCreditor;

            sqlParameter[4] = new SqlParameter("@debAccount", SqlDbType.NVarChar);
            sqlParameter[4].Value = (model.debAccount == null) ? SqlString.Null : model.debAccount;

            sqlParameter[5] = new SqlParameter("@creditAccount", SqlDbType.NVarChar);
            sqlParameter[5].Value = (model.creditAccount == null) ? SqlString.Null : model.creditAccount;

            sqlParameter[6] = new SqlParameter("@payCondition", SqlDbType.Int);
            sqlParameter[6].Value = (model.payCondition == null) ? SqlInt32.Null : model.payCondition;

            sqlParameter[7] = new SqlParameter("@iban", SqlDbType.NVarChar);
            sqlParameter[7].Value = (model.iban == null) ? SqlString.Null : model.iban;

            sqlParameter[8] = new SqlParameter("@accNumber", SqlDbType.NVarChar);
            sqlParameter[8].Value = (model.accNumber == null) ? SqlString.Null : model.accNumber;

            sqlParameter[9] = new SqlParameter("@idAccDebCre", SqlDbType.Int);
            sqlParameter[9].Value = model.idAccDebCre;

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
            sqlParameter[4].Value = model.idAccDebCre;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAccDebCre";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccDebCre";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);


        }

        public bool UpdateDebitorToTrue(int id, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE AccDebCre SET isDebitor = 'true' 
                                    WHERE idAccDebCre =@idAccDebCre");


            SqlParameter[] sqlParameter = new SqlParameter[1];
            
            sqlParameter[0] = new SqlParameter("@idAccDebCre", SqlDbType.Int);
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
            sqlParameter[3].Value = "U";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = id;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAccDebCre";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccDebCre";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update debitor to true";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);


        }
        public bool Save(AccDebCreModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO AccDebCre  (idContPerson,idClient,isDebitor,isCreditor,
                                    debAccount, creditAccount,payCondition,iban, accNumber)
                             VALUES (@idContPerson,@idClient,@isDebitor,@isCreditor,@debAccount,@creditAccount,@payCondition,@iban, @accNumber)");



            SqlParameter[] sqlParameter = new SqlParameter[9];

            sqlParameter[0] = new SqlParameter("@idContPerson", SqlDbType.Int);
            sqlParameter[0].Value = (model.idContPerson == null) ? SqlInt32.Null : model.idContPerson;

            sqlParameter[1] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[1].Value = (model.idClient == null) ? SqlInt32.Null : model.idClient;

            sqlParameter[2] = new SqlParameter("@isDebitor", SqlDbType.Bit);
            sqlParameter[2].Value = model.isDebitor;

            sqlParameter[3] = new SqlParameter("@isCreditor", SqlDbType.Bit);
            sqlParameter[3].Value = model.isCreditor;

            sqlParameter[4] = new SqlParameter("@debAccount", SqlDbType.NVarChar);
            sqlParameter[4].Value = (model.debAccount == null) ? SqlString.Null : model.debAccount;

            sqlParameter[5] = new SqlParameter("@creditAccount", SqlDbType.NVarChar);
            sqlParameter[5].Value = (model.creditAccount == null) ? SqlString.Null : model.creditAccount;

            sqlParameter[6] = new SqlParameter("@payCondition", SqlDbType.Int);
            sqlParameter[6].Value = (model.payCondition == null) ? SqlInt32.Null : model.payCondition;

            sqlParameter[7] = new SqlParameter("@iban", SqlDbType.NVarChar);
            sqlParameter[7].Value = (model.iban == null) ? SqlString.Null : model.iban;

            sqlParameter[8] = new SqlParameter("@accNumber", SqlDbType.NVarChar);
            sqlParameter[8].Value = (model.accNumber == null) ? SqlString.Null : model.accNumber;

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
            sqlParameter[4].Value = conn.GetLastTableID("AccDebCre") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAccDebCre";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccDebCre";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);


        }


       

        //POSTOJI//

        //POSTOJI//

      public DataTable GetDetailRange(string from, string to, bool deb, bool isBalans, bool sum, DateTime dtFrom, DateTime dtTo, string nameUser)
        {
            string debCreCondition = "";
            if (from == "" && to != "")
            {
                from = "0";

            }
            if (from == "0")
            {
                from = " ";
            }
            else if (from == "" && to == "")
            {
                from = "0";
                to = "100000";
            }
            if (from == "0")
            {
                from = " ";
            }
            else if (from != " " && to == " ")
            {
                to = from;
            }
            if (to == "100000")
            {
                to = "Select all";
            }

            if (deb == false)
            {
                debCreCondition = "AND numberLedAccount = (SELECT defCreditorAccount FROM AccSettings WHERE DATEPART(yyyy,GETDATE()) = yearSettings)";
            }
            else
            {
                debCreCondition = "AND numberLedAccount = (SELECT defDebitorAccount FROM AccSettings WHERE DATEPART(yyyy,GETDATE()) = yearSettings)";
            }



            string query = string.Format(@" SELECT DISTINCT accNumber as AccNumber, al.idAccDaily,  CASE WHEN a.idContPerson = 0 OR a.idContPerson is NULL then c.nameClient 
           ELSE CASE WHEN firstname is null THEN '' else firstname end + ' ' + 
           CASE WHEN midname is null THEN '' ELSE midname end + ' ' +
           CASE WHEN lastname is null THEN '' ELSE lastname end END as Name,
           CASE WHEN  invoiceNr IS NULL THEN '' ELSE invoiceNr END AS invoiceNr,incopNr,dtLine,periodLine,descLine 
           ,CASE WHEN db.amount IS NULL THEN 0 ELSE db.amount END as DebitBalans, 
           CASE WHEN cb.amount IS NULL THEN 0 ELSE cb.amount END as CreditBalans ,
           CASE WHEN dbb.amount IS NULL THEN 0 ELSE dbb.amount END  as Debit,
           CASE WHEN cbb.amount IS NULL THEN 0 ELSE cbb.amount END  as Credit
           ,CASE WHEN db.amount IS NULL THEN 0 ELSE db.amount END  +  
           CASE WHEN  dbb.amount  IS NULL THEN 0 ELSE  dbb.amount  END  - 
           CASE WHEN cb.amount IS NULL THEN 0 ELSE cb.amount END - 
           CASE WHEN cbb.amount IS NULL THEN 0 ELSE cbb.amount END as Saldo, '" + from + @"' as [from], '" + to + @"' as [to],'" + nameUser + @"' as nameUser,'" + deb + @"' as [deb],numberLedAccount
           from AccDebCre a                            
           LEFT OUTER JOIN ContactPerson ac ON ac.idContPers = a.idContPerson
           LEFT OUTER JOIN AccLine al ON al.idClientLine = a.accNumber
           LEFT JOIN Client c ON c.idClient = a.idClient
           LEFT OUTER JOIN (SELECT idAccLine,debitLine as amount from AccLine
           WHERE idClientLine BETWEEN '" + from + @"' AND '" + to + @"' 
           AND  dtLine  < @dtFrom " + debCreCondition + @") db ON db.idAccLine = al.idAccLine
           LEFT OUTER JOIN (SELECT idAccLine,creditLine as amount from AccLine
           WHERE idClientLine BETWEEN '" + from + @"' AND '" + to + @"' 
           AND  dtLine < @dtFrom " + debCreCondition + @") cb ON cb.idAccLine = al.idAccLine
        LEFT OUTER JOIN (SELECT idAccLine,debitLine as amount from AccLine
           WHERE idClientLine BETWEEN '" + from + @"' AND '" + to + @"' 
           AND periodLine <> '0'  AND dtLine BETWEEN @dtFrom AND @dtTo " + debCreCondition + @") dbb ON dbb.idAccLine = al.idAccLine
           LEFT OUTER JOIN (SELECT idAccLine,creditLine as amount from AccLine
           WHERE  idClientLine BETWEEN '" + from + @"' AND '" + to + @"' 
           AND periodLine <> '0'  AND dtLine BETWEEN @dtFrom AND @dtTo " + debCreCondition + @") cbb ON cbb.idAccLine = al.idAccLine
           WHERE (db.amount IS NOT NULL OR dbb.amount IS NOT NULL OR cb.amount IS NOT NULL OR cbb.amount IS NOT NULL)
           AND a.accNumber BETWEEN '" + from + "' AND '" + to + "' ");


            // Za balans
            if (isBalans == false)
            {
                query = @"  SELECT DISTINCT accNumber as AccNumber,al.idAccDaily,  CASE WHEN a.idContPerson = 0 OR a.idContPerson is NULL then c.nameClient 
       ELSE CASE WHEN firstname is null THEN '' else firstname end + ' ' + 
       CASE WHEN midname is null THEN '' ELSE midname end + ' ' +
       CASE WHEN lastname is null THEN '' ELSE lastname end END as Name,
       CASE WHEN  invoiceNr IS NULL THEN '' ELSE invoiceNr END AS invoiceNr,incopNr,dtLine,periodLine,descLine,
       CASE WHEN dbb.amount IS NULL THEN 0 ELSE dbb.amount END  as Debit,
       CASE WHEN cbb.amount IS NULL THEN 0 ELSE cbb.amount END  as Credit
       ,CASE WHEN  dbb.amount  IS NULL THEN 0 ELSE  dbb.amount  END  - 
       CASE WHEN cbb.amount IS NULL THEN 0 ELSE cbb.amount END as Saldo,'" + from + @"' as [from], '" + to + @"' as [to],'" + nameUser + @"' as nameUser,'" + deb + @"' as [deb],numberLedAccount
       from AccDebCre a                            
       LEFT OUTER JOIN ContactPerson ac ON ac.idContPers = a.idContPerson
       LEFT OUTER JOIN AccLine al ON al.idClientLine = a.accNumber
       LEFT JOIN Client c ON c.idClient = a.idClient
       LEFT OUTER JOIN (SELECT idAccLine,debitLine as amount from AccLine
       WHERE  idClientLine BETWEEN '" + from + @"' AND '" + to + @"' 
       AND periodLine <> '0'  AND dtLine BETWEEN @dtFrom AND @dtTo " + debCreCondition + @") dbb ON dbb.idAccLine = al.idAccLine
       LEFT OUTER JOIN (SELECT idAccLine,creditLine as amount from AccLine
       WHERE idClientLine BETWEEN '" + from + @"' AND '" + to + @"' 
       AND periodLine <> '0'  AND dtLine BETWEEN @dtFrom AND @dtTo " + debCreCondition + @") cbb ON cbb.idAccLine = al.idAccLine
       WHERE (  dbb.amount IS NOT NULL OR cbb.amount IS NOT NULL) AND a.accNumber BETWEEN '" + from + "' AND '" + to + "'  AND dtLine BETWEEN @dtFrom AND @dtTo  ";
            }

            if (deb == false)
            {
                query += "AND a.creditAccount = (SELECT defCreditorAccount FROM AccSettings WHERE DATEPART(yyyy,GETDATE()) = yearSettings)";
            }
            else
            {
                query += "AND a.debAccount = (SELECT defDebitorAccount FROM AccSettings WHERE DATEPART(yyyy,GETDATE()) = yearSettings)";
            }

            SqlParameter[] sqlParemeters = new SqlParameter[2];

            sqlParemeters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParemeters[0].Value = dtFrom;

            sqlParemeters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParemeters[1].Value = dtTo;


            return conn.executeSelectQuery(query, sqlParemeters);
        }
 ///////////////////////////////////
     public DataTable GetAllRange(string from, string to, bool deb, bool isBalans, bool sum, DateTime dtFrom, DateTime dtTo, string nameUser)
        {
            string debCreCondition = "";
            if (from == "" && to != "")
            {
                from = "0";

            }
            if (from == "0")
            {
                from = " ";
            }
            else if (from == "" && to == "")
            {
                from = "0";
                to = "100000";
            }
            if (from == "0")
            {
                from = " ";
            }
            else if (from != "" && to == "")
            {
                to = from;
            }
            if (to == "100000")
            {
                to = "Select all";
            }

            if (deb == false)
            {
                debCreCondition = "AND numberLedAccount = (SELECT defCreditorAccount FROM AccSettings WHERE DATEPART(yyyy,GETDATE()) = yearSettings)";
            }
            else
            {
                debCreCondition = "AND numberLedAccount = (SELECT defDebitorAccount FROM AccSettings WHERE DATEPART(yyyy,GETDATE()) = yearSettings)";
            }


            string query = string.Format(@"SELECT DISTINCT accNumber as AccNumber,   CASE WHEN a.idContPerson = 0 OR a.idContPerson is NULL then cl.nameClient 
           ELSE CASE WHEN firstname is null THEN '' else firstname end + ' ' + 
           CASE WHEN midname is null THEN '' ELSE midname end + ' ' +
           CASE WHEN lastname is null THEN '' ELSE lastname end END as Name,
       street + ' ' + housenr as [address], postalCode as zip, city  as city
       ,CASE WHEN db.amount IS NULL THEN 0 ELSE db.amount END as DebitBalans, CASE WHEN cb.amount IS NULL THEN 0 ELSE cb.amount END as CreditBalans ,CASE WHEN d.amount IS NULL THEN 0 ELSE d.amount END  as Debit,CASE WHEN c.amount IS NULL THEN 0 ELSE c.amount END  as Credit
       ,CASE WHEN db.amount IS NULL THEN 0 ELSE db.amount END  +  CASE WHEN d.amount IS NULL THEN 0 ELSE d.amount END  - CASE WHEN cb.amount IS NULL THEN 0 ELSE cb.amount END - CASE WHEN c.amount IS NULL THEN 0 ELSE c.amount END  as Saldo,'" + from + @"' as [from], '" + to + @"' as [to],'" + nameUser + @"' as nameUser,'" + deb + @"' as [deb]
       from AccDebCre a                            
       LEFT OUTER JOIN ContactPersonAddress ab ON ab.idContPers = a.idContPerson and idAddressType = '1'
       LEFT OUTER JOIN ContactPerson ac ON ac.idContPers = a.idContPerson
       LEFT OUTER JOIN AccLine al ON al.idClientLine = a.accNumber
        LEFT JOIN Client cl ON cl.idClient = a.idClient
       LEFT OUTER JOIN (SELECT idClientLine,SUM(debitLine) as amount from AccLine
       WHERE   dtLine < @dtFrom  " + debCreCondition + @"  GROUP BY idClientLine) db ON db.idClientLine = al.idClientLine
       LEFT OUTER JOIN (SELECT idClientLine,SUM(creditLine) as amount from AccLine
       WHERE   dtLine  < @dtFrom " + debCreCondition + @"  GROUP BY idClientLine) cb ON cb.idClientLine = al.idClientLine
        LEFT OUTER JOIN (SELECT idClientLine,SUM(debitLine) as amount from AccLine
       WHERE periodLine <> '0' AND dtLine BETWEEN @dtFrom AND @dtTo " + debCreCondition + @"  GROUP BY idClientLine) d ON d.idClientLine = al.idClientLine
       LEFT OUTER JOIN (SELECT idClientLine,SUM(creditLine) as amount from AccLine
       WHERE periodLine <> '0' AND dtLine BETWEEN @dtFrom AND @dtTo " + debCreCondition + @"  GROUP BY idClientLine) c ON c.idClientLine = al.idClientLine
       WHERE (db.amount IS NOT NULL OR d.amount IS NOT NULL OR cb.amount IS NOT NULL OR c.amount IS NOT NULL )
       AND a.accNumber BETWEEN '" + from + "' AND '" + to + "'  ");


            // Za balans
            if (isBalans == false)
            {
                query = @"SELECT DISTINCT accNumber as AccNumber,  CASE WHEN a.idContPerson = 0 OR a.idContPerson is NULL then cl.nameClient 
           ELSE CASE WHEN firstname is null THEN '' else firstname end + ' ' + 
           CASE WHEN midname is null THEN '' ELSE midname end + ' ' +
           CASE WHEN lastname is null THEN '' ELSE lastname end END as Name,
        street + ' ' + housenr as [address], postalCode as zip, city  as city
       ,CASE WHEN d.amount IS NULL THEN 0 ELSE d.amount END  as Debit,CASE WHEN c.amount IS NULL THEN 0 ELSE c.amount END  as Credit
       ,CASE WHEN d.amount IS NULL THEN 0 ELSE d.amount END  -  CASE WHEN c.amount IS NULL THEN 0 ELSE c.amount END  as Saldo,'" + from + @"' as [from], '" + to + @"' as [to],'" + nameUser + @"' as nameUser,'" + deb + @"' as [deb]
       from AccDebCre a                            
       LEFT OUTER JOIN ContactPersonAddress ab ON ab.idContPers = a.idContPerson and idAddressType = '1'
       LEFT OUTER JOIN ContactPerson ac ON ac.idContPers = a.idContPerson
       LEFT OUTER JOIN AccLine al ON al.idClientLine = a.accNumber
        LEFT JOIN Client cl ON cl.idClient = a.idClient
       LEFT OUTER JOIN (SELECT idClientLine,SUM(debitLine) as amount from AccLine
       WHERE  periodLine <> '0' AND dtLine BETWEEN @dtFrom AND @dtTo " + debCreCondition + @" GROUP BY idClientLine ) d ON d.idClientLine = al.idClientLine
       LEFT OUTER JOIN (SELECT idClientLine,SUM(creditLine) as amount from AccLine
       WHERE periodLine <> '0' AND dtLine BETWEEN @dtFrom AND @dtTo " + debCreCondition + @" GROUP BY idClientLine ) c ON c.idClientLine = al.idClientLine
       WHERE ( d.amount IS NOT NULL OR  c.amount IS NOT NULL ) AND a.accNumber BETWEEN '" + from + "' AND '" + to + "' AND dtLine BETWEEN @dtFrom AND @dtTo  ";
            }

            if (deb == false)
            {
                query += "AND a.creditAccount = (SELECT defCreditorAccount FROM AccSettings WHERE DATEPART(yyyy,GETDATE()) = yearSettings)";
            }
            else
            {
                query += "AND a.debAccount = (SELECT defDebitorAccount FROM AccSettings WHERE DATEPART(yyyy,GETDATE()) = yearSettings)";
            }

            SqlParameter[] sqlParemeters = new SqlParameter[2];

            sqlParemeters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParemeters[0].Value = dtFrom;

            sqlParemeters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParemeters[1].Value = dtTo;

            return conn.executeSelectQuery(query, sqlParemeters);
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

        public DataTable GetDebitorsPerson()
        {
            string query = string.Format(@"SELECT DISTINCT a.idContPerson, accNumber as AccNumber, CASE WHEN firstname is null THEN SPACE(1) else firstname end + ' ' + 
							CASE WHEN midname is null THEN SPACE(1) ELSE midname end + ' ' + 
							CASE WHEN lastname is null THEN SPACE(1) ELSE lastname end as Name, postalCode as zip, city  as city,
                            ab.street, ab.housenr,ab.extension, ab.country 
                            from AccDebCre a
                            LEFT OUTER JOIN ContactPersonAddress ab ON ab.idContPers = a.idContPerson
                            LEFT OUTER JOIN ContactPerson ac ON ac.idContPers = a.idContPerson
                            WHERE (a.idContPerson is Not Null and a.idClient=0 ) and (ab.idAddressType=1) and a.isDebitor = 1 and a.idContPerson > 0 
                            UNION
                            select DISTINCT a.idContPerson, accNumber as AccNumber, nameClient as Name, postalCode as zip, city as city,
							ab.street, ab.housenr,ab.extension, ab.country
                            from AccDebCre a
                            LEFT OUTER JOIN ClientAddress ab ON ab.idClient = a.idClient
                            LEFT OUTER JOIN Client ac ON ac.idClient = a.idClient
                            where a.idClient is not null and idContPerson = 0 and ab.idAddressType=1 and a.isDebitor = 1 and a.idContPerson > 0");

            return conn.executeSelectQuery(query, null);

        }
        public DataTable GetDebitorsClient()
        {
            string query = string.Format(@"SELECT DISTINCT a.idClient, accNumber as AccNumber, CASE WHEN firstname is null THEN SPACE(1) else firstname end + ' ' + 
							CASE WHEN midname is null THEN SPACE(1) ELSE midname end + ' ' + 
							CASE WHEN lastname is null THEN SPACE(1) ELSE lastname end as Name, postalCode as zip, city  as city,
							street, housenr,extension, country 
                            from AccDebCre a
                            LEFT OUTER JOIN ContactPersonAddress ab ON ab.idContPers = a.idContPerson
                            LEFT OUTER JOIN ContactPerson ac ON ac.idContPers = a.idContPerson
                            WHERE (a.idContPerson is Not Null and a.idClient=0 ) and (ab.idAddressType=1) and a.isDebitor = 1  and a.idClient > 0 
                            UNION
                            select DISTINCT a.idClient, accNumber as AccNumber, nameClient as Name, postalCode as zip, city as city,
						    street, housenr,extension, country 
                            from AccDebCre a
                            LEFT OUTER JOIN ClientAddress ab ON ab.idClient = a.idClient
                            LEFT OUTER JOIN Client ac ON ac.idClient = a.idClient
                            where a.idClient is not null and idContPerson = 0 and ab.idAddressType=1 and a.isDebitor = 1 and a.idClient > 0");

            return conn.executeSelectQuery(query, null);

        }
    }
}


