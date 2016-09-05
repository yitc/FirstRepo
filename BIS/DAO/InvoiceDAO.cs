using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;
using System.Data.SqlTypes;
using BIS.Model;

namespace BIS.DAO
{
    public class InvoiceDAO
    {
        private dbConnection conn;
        public InvoiceDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllInvoice()
        {
            string query = string.Format(@"SELECT idInvoice, invoiceNr,invoiceRbr, idVoucher, i.idInvoiceStatus,st.descInvoiceStatus, descriptionInvoice, idClient,
                    idContPerson, dtInvoice, dtValuta, brutoAmount, netoAmount, idBtw, isBooked, noteInvoice, userCreated, dtCreated,userModified,dtModified,
                     dtFirstPay,dtLastPay,percentFrstPay,reservationCost,firstReferencePay,secondReferencePay,typeInvoice,roomComment  
                    FROM Invoice i
                    LEFT JOIN InvoiceStatus st ON st.idInvoiceStatus = i.idInvoiceStatus");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllInvoiceByVoucher(int idVoucher)
        {
            string query = string.Format(@"SELECT idInvoice, invoiceNr,invoiceRbr, idVoucher, i.idInvoiceStatus,st.descInvoiceStatus, descriptionInvoice, idClient,
                    idContPerson, dtInvoice, dtValuta, brutoAmount, netoAmount, idBtw, isBooked, noteInvoice, userCreated, dtCreated,userModified,dtModified,
                     dtFirstPay,dtLastPay,percentFrstPay,reservationCost,firstReferencePay,secondReferencePay,typeInvoice,roomComment  
                    FROM Invoice i
                    LEFT JOIN InvoiceStatus st ON st.idInvoiceStatus = i.idInvoiceStatus 
                    WHERE  idVoucher= '" + idVoucher.ToString() + "'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetInvoicePaid(int idInvoice)
        {
//            string query = string.Format(@"SELECT SUM(ISNULL(dd.paid,0)) as paid,dateadd(day,CASE WHEN a.daysLastPayment IS NULL THEN 0 ELSE a.daysLastPayment END,i.dtInvoice) as dtUntil
//                                            FROM Invoice i 
//                                            LEFT OUTER JOIN 
//                                            (select al.invoiceNr,SUM(creditLine) as paid  FROM AccLine al
//                                            RIGHT JOIN (SELECT DISTINCT invoiceNr,invoiceRbr FROM Invoice WHERE invoiceNr IN (SELECT DISTINCT invoiceNr FROm Invoice WHERE idInvoice = '" + idInvoice + @"'))  i ON al.invoiceNr = i.invoiceNr+'-'+i.invoiceRbr
//                                            LEFT OUTER JOIN AccDaily ad ON ad.idDaily = al.idAccDaily
//                                            WHERE ad.idDailyType='1' AND numberLedAccount = (SELECT defDebitorAccount FROM AccSettings WHERE yearSettings = DATEPART(year,getdate()))
//                                            GROUP BY al.invoiceNr) dd
//                                            ON SUBSTRING(dd.invoiceNr,1,LEN(dd.invoiceNr)-4) = i.invoiceNr
//                                            LEFT OUTER JOIN ArrangementBook ab ON i.idVoucher = ab.idArrangementBook
//                                            LEFT OUTER JOIN Arrangement a ON ab.idArrangement = a.idArrangement
//                                            WHERE i.idInvoice = '" + idInvoice + @"'
//                                            GROUP BY dateadd(day,CASE WHEN a.daysLastPayment IS NULL THEN 0 ELSE a.daysLastPayment END,i.dtInvoice) ");
            // prebaceno da gleda na otvorene stavke zbog placanja vise faktura jednim iznosom
            string query = string.Format(@" SELECT SUM(ISNULL(dd.paid,0)) as paid,dateadd(day,CASE WHEN a.daysLastPayment IS NULL THEN 0 ELSE a.daysLastPayment END,i.dtInvoice) as dtUntil
                                            FROM Invoice i 
                                            LEFT OUTER JOIN 
                                            (select al.invoiceOpenLine,SUM(creditOpenLine) as paid  FROM AccOpenLines al
                                            RIGHT JOIN (SELECT DISTINCT invoiceNr,invoiceRbr FROM Invoice WHERE invoiceNr IN (SELECT DISTINCT invoiceNr FROm Invoice WHERE idInvoice = '" + idInvoice + @"'))  i ON al.invoiceOpenLine = i.invoiceNr+'-'+i.invoiceRbr
                                            WHERE   al.account = (SELECT defDebitorAccount FROM AccSettings WHERE yearSettings = DATEPART(year,getdate()))
                                            GROUP BY al.invoiceOpenLine) dd
                                            ON SUBSTRING(dd.invoiceOpenLine,1,LEN(dd.invoiceOpenLine)-4) = i.invoiceNr
                                            LEFT OUTER JOIN ArrangementBook ab ON i.idVoucher = ab.idArrangementBook
                                            LEFT OUTER JOIN Arrangement a ON ab.idArrangement = a.idArrangement
                                            WHERE i.idInvoice = '" + idInvoice + @"'
                                            GROUP BY dateadd(day,CASE WHEN a.daysLastPayment IS NULL THEN 0 ELSE a.daysLastPayment END,i.dtInvoice) ");

            return conn.executeSelectQuery(query, null);
        }
//        public DataTable GetInvoicePaid(int idInvoice)
//        {
//            string query = string.Format(@"SELECT ISNULL(dd.paid,0) as paid,dateadd(day,CASE WHEN a.daysLastPayment IS NULL THEN 0 ELSE a.daysLastPayment END,i.dtInvoice) as dtUntil
//                                            FROM Invoice i 
//                                            LEFT OUTER JOIN 
//                                            (select al.invoiceNr,SUM(creditLine) as paid  FROM AccLine al
//                                            RIGHT JOIN (SELECT DISTINCT invoiceNr,invoiceRbr FROM Invoice WHERE invoiceNr IN (SELECT DISTINCT invoiceNr FROm Invoice WHERE idInvoice = '"+idInvoice+ @"'))  i ON al.invoiceNr = i.invoiceNr+'-'+i.invoiceRbr
//                                            LEFT OUTER JOIN AccDaily ad ON ad.idDaily = al.idAccDaily
//                                            WHERE ad.idDailyType='1' AND numberLedAccount = (SELECT defDebitorAccount FROM AccSettings WHERE yearSettings = DATEPART(year,getdate()))
//                                            GROUP BY al.invoiceNr) dd
//                                            ON SUBSTRING(dd.invoiceNr,1,LEN(dd.invoiceNr)-4) = i.invoiceNr
//                                            LEFT OUTER JOIN ArrangementBook ab ON i.idVoucher = ab.idArrangementBook
//                                            LEFT OUTER JOIN Arrangement a ON ab.idArrangement = a.idArrangement
//                                            WHERE i.idInvoice = '" + idInvoice+"'");

//            return conn.executeSelectQuery(query, null);
//        }

        public DataTable GetAllInvoiceCustomerP(int idDebCre)
        {
            string query = string.Format(@"SELECT idInvoice, invoiceNr,invoiceRbr, idVoucher, i.idInvoiceStatus,st.descInvoiceStatus, descriptionInvoice, idClient,
                    idContPerson, dtInvoice, dtValuta, brutoAmount, netoAmount, idBtw, isBooked, noteInvoice, userCreated, dtCreated,userModified,dtModified, 
                     dtFirstPay,dtLastPay,percentFrstPay,reservationCost,firstReferencePay,secondReferencePay,typeInvoice,roomComment  
                    FROM Invoice i
                    LEFT JOIN InvoiceStatus st ON st.idInvoiceStatus = i.idInvoiceStatus
                    WHERE idContPerson = '" + idDebCre.ToString() + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAllInvoiceCustomerC(int idDebCre)
        {
            string query = string.Format(@"SELECT idInvoice, invoiceNr,invoiceRbr, idVoucher, i.idInvoiceStatus,st.descInvoiceStatus, descriptionInvoice, idClient,
                    idContPerson, dtInvoice, dtValuta, brutoAmount, netoAmount, idBtw, isBooked, noteInvoice, userCreated, dtCreated,userModified,dtModified, 
                     dtFirstPay,dtLastPay,percentFrstPay,reservationCost,firstReferencePay,secondReferencePay,typeInvoice,roomComment  
                    FROM Invoice i
                    LEFT JOIN InvoiceStatus st ON st.idInvoiceStatus = i.idInvoiceStatus
                    WHERE idClient = '" + idDebCre.ToString() + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetInvoiceCustomerAndVoucher( int idVoucher)
        {
            string query = string.Format(@"SELECT idInvoice, invoiceNr,invoiceRbr, idVoucher, i.idInvoiceStatus,st.descInvoiceStatus as descInvoiceStatus, descriptionInvoice, idClient,
                    idContPerson, dtInvoice, dtValuta, brutoAmount, netoAmount, idBtw, isBooked, noteInvoice, userCreated, dtCreated,userModified,dtModified,
                     dtFirstPay,dtLastPay,percentFrstPay,reservationCost,firstReferencePay,secondReferencePay,typeInvoice,roomComment   
                    FROM Invoice i
                    LEFT JOIN InvoiceStatus st ON st.idInvoiceStatus = i.idInvoiceStatus
                    WHERE  idVoucher= '" + idVoucher.ToString() +"' ORDER BY invoiceRbr");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetInvoiceByVoucher( int idVoucher)
        {
            string query = string.Format(@"SELECT idInvoice, invoiceNr,invoiceRbr, idVoucher, i.idInvoiceStatus,st.descInvoiceStatus as descInvoiceStatus, descriptionInvoice, idClient,
                    idContPerson, dtInvoice, dtValuta, brutoAmount, netoAmount, idBtw, isBooked, noteInvoice, userCreated, dtCreated,userModified,dtModified,
                     dtFirstPay,dtLastPay,percentFrstPay,reservationCost,firstReferencePay,secondReferencePay,typeInvoice,roomComment  
                    FROM Invoice i
                    LEFT JOIN InvoiceStatus st ON st.idInvoiceStatus = i.idInvoiceStatus
                    WHERE  idVoucher= '" + idVoucher.ToString() + "' and invoiceRbr = '001'");

            return conn.executeSelectQuery(query, null);
        }

        //===
        public DataTable GetCountExtension(int idVoucher)
        {
            string query = string.Format(@" select count(invoiceNR) as number from invoice
                    WHERE idVoucher= '" + idVoucher.ToString() + "' and invoiceRbr LIKE '0%' ");

            return conn.executeSelectQuery(query, null);
        }
        //===
        public DataTable GetSumInvoicePerson(string invoice, Boolean isSumNotRest)
        {
            string query = string.Format(@"SELECT sum(price*quantity) AS number 
                                            FROM InvoiceItems 
                                            WHERE idInvoice IN (
                                            SELECT idInvoice FROM Invoice
                                            WHERE invoiceNr = '"+invoice+@"' and invoiceRbr < '998'  )
                                            AND idArtical <> 'Reservation cost'
                                            AND idArtical <> 'Calamitait Fond'
                                            AND idArtical <> 'Money group'
                                            AND idArtical <> 'Insurance'
                                            AND idArtical <> 'Cancel insurance'");

            if (isSumNotRest == false)
            {
                query = string.Format(@"SELECT sum(price*quantity) AS number 
                                        FROM InvoiceItems 
                                        WHERE idInvoice IN (
                                        SELECT idInvoice FROM Invoice
                                        WHERE invoiceNr = '" + invoice + @"' and invoiceRbr < '998'  )
                                        AND (idArtical = 'Reservation cost'
                                        OR idArtical = 'Cancel insurance')");
            }

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetSumCancelInsurance(string invoice)
        {
            string query = string.Format(@" select sum(quantity*price) as number from 
                    InvoiceItems where idInvoice IN (SELECT idInvoice FROM invoice
                    WHERE invoiceNr = '" + invoice.ToString() + @"' and invoiceRbr < '998')
                    and idArtical = 'Cancel insurance'  ");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetInvoiceByID(string idInvoice)
        {
            string query = string.Format(@"SELECT idInvoice, invoiceNr,invoiceRbr, idVoucher, i.idInvoiceStatus,st.descInvoiceStatus, descriptionInvoice, idClient,
                    idContPerson, dtInvoice, dtValuta, brutoAmount, netoAmount, idBtw, isBooked, noteInvoice, userCreated, dtCreated,userModified,dtModified,
                     dtFirstPay,dtLastPay,percentFrstPay,reservationCost,firstReferencePay,secondReferencePay,typeInvoice ,roomComment  
                    FROM Invoice i 
                    LEFT JOIN InvoiceStatus st ON st.idInvoiceStatus = i.idInvoiceStatus 
                    WHERE invoiceNr = @idInvoice");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idInvoice", SqlDbType.Int);
            sqlParameters[0].Value = idInvoice;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetBasicInvoices(string idInvoice)
        {
            string query = string.Format(@"SELECT idInvoice, invoiceNr,invoiceRbr, idVoucher, i.idInvoiceStatus,st.descInvoiceStatus, descriptionInvoice, idClient,
                    idContPerson, dtInvoice, dtValuta, brutoAmount, netoAmount, idBtw, isBooked, noteInvoice, userCreated, dtCreated,userModified,dtModified,
                     dtFirstPay,dtLastPay,percentFrstPay,reservationCost,firstReferencePay,secondReferencePay,typeInvoice,roomComment   
                    FROM Invoice i 
                    LEFT JOIN InvoiceStatus st ON st.idInvoiceStatus = i.idInvoiceStatus 
                    WHERE invoiceNr IN (SELECT DISTINCT invoiceNr  FROM Invoice WHERE idInvoice = @idInvoice) AND brutoAmount>0 AND (invoiceRbr='000' OR invoiceRbr='001') and i.idInvoiceStatus = '1'");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idInvoice", SqlDbType.Int);
            sqlParameters[0].Value = idInvoice;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetInvoiceByInvoiceAndExtension(string idInvoice, string ext)
        {
            string query = string.Format(@"SELECT idInvoice, invoiceNr,invoiceRbr, idVoucher, i.idInvoiceStatus,st.descInvoiceStatus, descriptionInvoice, idClient,
                    idContPerson, dtInvoice, dtValuta, brutoAmount, netoAmount, idBtw, isBooked, noteInvoice, userCreated, dtCreated,userModified,dtModified,
                     dtFirstPay,dtLastPay,percentFrstPay,reservationCost,firstReferencePay,secondReferencePay,typeInvoice,roomComment   
                    FROM Invoice i 
                    LEFT JOIN InvoiceStatus st ON st.idInvoiceStatus = i.idInvoiceStatus 
                    WHERE invoiceNr = @idInvoice and invoiceRbr = @ext");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@idInvoice", SqlDbType.NVarChar);
            sqlParameters[0].Value = idInvoice;
            sqlParameters[1] = new SqlParameter("@ext", SqlDbType.NVarChar);
            sqlParameters[1].Value = ext;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetInvoiceByInvoiceAndExtension999(string idInvoice, string ext, bool isCanceled)
        {
            string condition = " AND i.brutoAmount>0";

            if(isCanceled==true)
                condition = " AND i.brutoAmount<0";

            string query = string.Format(@"SELECT idInvoice, invoiceNr,invoiceRbr, idVoucher, i.idInvoiceStatus,st.descInvoiceStatus, descriptionInvoice, idClient,
                    idContPerson, dtInvoice, dtValuta, brutoAmount, netoAmount, idBtw, isBooked, noteInvoice, userCreated, dtCreated,userModified,dtModified,
                     dtFirstPay,dtLastPay,percentFrstPay,reservationCost,firstReferencePay,secondReferencePay,typeInvoice ,roomComment  
                    FROM Invoice i 
                    LEFT JOIN InvoiceStatus st ON st.idInvoiceStatus = i.idInvoiceStatus 
                    WHERE invoiceNr = @idInvoice and invoiceRbr = @ext" + condition);

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@idInvoice", SqlDbType.NVarChar);
            sqlParameters[0].Value = idInvoice;
            sqlParameters[1] = new SqlParameter("@ext", SqlDbType.NVarChar);
            sqlParameters[1].Value = ext;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetInvoiceByIntID(int idInvoice)
        {
            string query = string.Format(@"SELECT idInvoice, invoiceNr,invoiceRbr, idVoucher, i.idInvoiceStatus,st.descInvoiceStatus, descriptionInvoice, idClient,
                    idContPerson, dtInvoice, dtValuta, brutoAmount, netoAmount, idBtw, isBooked, noteInvoice, userCreated, dtCreated,userModified,dtModified,
                     dtFirstPay,dtLastPay,percentFrstPay,reservationCost,firstReferencePay,secondReferencePay,typeInvoice,roomComment   
                    FROM Invoice i 
                    LEFT JOIN InvoiceStatus st ON st.idInvoiceStatus = i.idInvoiceStatus 
                    WHERE idInvoice = @idInvoice");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idInvoice", SqlDbType.Int);
            sqlParameters[0].Value = idInvoice;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetInvoicesForPrint(int idArrangement, int invStatus)
        {
            string query = string.Format(@" select f.descInvoicestatus, e.firstname + ' '+ e.lastname as namePerson, * from invoice a
                                             left join arrangementbook c on c.idArrangementBook = idVoucher
                                             left join arrangement d on d.idArrangement = c.idArrangement
                                             left join ContactPerson e on e.idContPers = a.idcontperson
                                             left join InvoiceStatus f on f.idInvoiceStatus = a.idInvoiceStatus
                                             where c.idArrangement = @idArrangement and a.idInvoiceStatus = @invStatus order by idVoucher");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            sqlParameters[1] = new SqlParameter("@invStatus", SqlDbType.Int);
            sqlParameters[1].Value = invStatus;

            return conn.executeSelectQuery(query, sqlParameters);
        }


        public DataTable GetReportInvoiceByIntID(int idInvoice)
        {
            string query = string.Format(@"SELECT idInvoice, invoiceNr,invoiceRbr, idVoucher, i.idInvoiceStatus,st.descInvoiceStatus, descriptionInvoice, i.idClient,
                    idContPerson, dtInvoice, dtValuta, brutoAmount, netoAmount, idBtw, isBooked, noteInvoice, i.userCreated, i.dtCreated,i.userModified,i.dtModified,
                     dtFirstPay,dtLastPay,percentFrstPay,reservationCost, 
                     case when i.idContPerson=0 then ca.street else cpa.street  end as street, case when i.idContPerson=0 then ca.housenr else cpa.housenr end as housenr, 
                     case when i.idContPerson=0 then ca.extension  else cpa.extension end as extend,case when i.idContPerson=0 then ca.postalCode else cpa.postalCode end as zip,
                    case when i.idContPerson=0 then ca.city else cpa.city end as City, space(36) as country,
                    CASE WHEN cp.firstName IS NULL THEN '' ELSE cp.firstName END + ' ' +
                    CASE WHEN cp.lastName IS NULL THEN '' ELSE cp.lastName END  as namePerson, space(30) as arrName, space(10) as noDays, space(26) as boarding,
                    space(12) as dateFrom, space(12) as dateTo, space(30) as service, space(30) as firstAmount, space(30) as restAmount, space(20) as firstReference,
                    space(20) as restReference,firstReferencePay,secondReferencePay,typeInvoice,roomComment 
                    FROM Invoice i 
                    LEFT  JOIN InvoiceStatus st ON st.idInvoiceStatus = i.idInvoiceStatus 
                    left outer join ContactPerson cp On cp.idContPers = i.idContPerson
                    LEFT   JOIN ContactPersonAddress cpa ON i.idContPerson = cpa.idContPers and cpa.idAddressType = 1
                    left outer join Client c On c.idClient = i.idClient
                    LEFT   JOIN ClientAddress ca ON c.idClient = ca.idClient and ca.idAddressType = 1                  
                    WHERE idInvoice = @idInvoice");
            // LEFT JOIN ContactPerson cp ON i.idContPerson = cp.idContPers
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idInvoice", SqlDbType.Int);
            sqlParameters[0].Value = idInvoice;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetNumberCancelInsurance(int idVoucher)
        {
            string query = string.Format(@"SELECT COUNT(abp.idContPers)+ (SELECT COUNT(idArrangementBook) 
                                            FROM ArrangementBook 
                                            WHERE idArrangementBook = @idVoucher AND idStatus = 2 AND isCancelInsurance = 'true') as number 
                                            FROM ArrangementBookPersons abp
                                            INNER JOIN ArrangementBook ab ON ab.idContPers = abp.idContPers AND  ab.idArrangement = (SELECT idArrangement FROM ArrangementBook WHERE idArrangementBook = @idVoucher)
                                            WHERE abp.idArrangementBook = @idVoucher AND idStatus = 2 AND isCancelInsurance = 'true'");
            // LEFT JOIN ContactPerson cp ON i.idContPerson = cp.idContPers
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idVoucher", SqlDbType.Int);
            sqlParameters[0].Value = idVoucher;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetReportInvoiceFor999ByIntID(int idInvoice)
        {

            string query = string.Format(@"SELECT idInvoice, invoiceNr,invoiceRbr, idVoucher, i.idInvoiceStatus,st.descInvoiceStatus, descriptionInvoice, i.idClient,
                    idContPerson, dtInvoice, dtValuta, brutoAmount, netoAmount, idBtw, isBooked, noteInvoice, userCreated, i.dtCreated,userModified,i.dtModified,
                     dtFirstPay,dtLastPay,percentFrstPay,reservationCost, cpa.street, cpa.housenr, cpa.extension as extend,cpa.postalCode as zip,
                    cpa.city as City, space(36) as country,CASE WHEN cp.firstName IS NULL THEN '' ELSE cp.firstName END + ' ' +
                     CASE WHEN cp.lastName IS NULL THEN '' ELSE cp.lastName END  as namePerson, space(30) as arrName, space(10) as noDays, space(26) as boarding,
                    space(12) as dateFrom, space(12) as dateTo, space(30) as service, space(30) as firstAmount, space(30) as restAmount, space(20) as firstReference,
                    space(20) as restReference,firstReferencePay,secondReferencePay,typeInvoice,roomComment 
                    FROM Invoice i 
                    LEFT JOIN InvoiceStatus st ON st.idInvoiceStatus = i.idInvoiceStatus 
                    left outer join ContactPerson cp On cp.idContPers = i.idContPerson
                    LEFT JOIN ContactPersonAddress cpa ON i.idContPerson = cpa.idContPers
                    WHERE invoiceNr IN (SELECT DISTINCT invoiceNr FROM Invoice WHERE idInvoice = @idInvoice) AND ((invoiceRbr > '001' AND brutoAmount<0)  OR (invoiceRbr >= '999')) and cpa.idAddressType = 1 ORDER by InvoiceRbr");
            // LEFT JOIN ContactPerson cp ON i.idContPerson = cp.idContPers
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idInvoice", SqlDbType.Int);
            sqlParameters[0].Value = idInvoice;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetReportInvoiceByIntID999(int idInvoice)
        {
            string query = string.Format(@"SELECT idInvoice, invoiceNr,invoiceRbr, idVoucher, i.idInvoiceStatus,st.descInvoiceStatus, descriptionInvoice, idClient,
                    idContPerson, dtInvoice, dtValuta, brutoAmount, netoAmount, idBtw, isBooked, noteInvoice, i.userCreated, i.dtCreated, i.userModified, i.dtModified,
                     dtFirstPay,dtLastPay,percentFrstPay,reservationCost, 
                     case when i.idContPerson=0 then ca.street else cpa.street  end as street, case when i.idContPerson=0 then ca.housenr else cpa.housenr end as housenr, 
                     case when i.idContPerson=0 then ca.extension  else cpa.extension end as extend,case when i.idContPerson=0 then ca.postalCode else cpa.postalCode end as zip,
                    case when i.idContPerson=0 then ca.city else cpa.city end as City, space(36) as country, 
                    space(60) as namePerson, space(30) as arrName, space(10) as noDays, space(26) as boarding,
                    space(12) as dateFrom, space(12) as dateTo, space(30) as service, space(30) as firstAmount, space(30) as restAmount, space(20) as firstReference,
                    space(20) as restReference,firstReferencePay,secondReferencePay,typeInvoice,roomComment 
                    FROM Invoice i 
                    LEFT JOIN InvoiceStatus st ON st.idInvoiceStatus = i.idInvoiceStatus 
                    LEFT   JOIN ContactPersonAddress cpa ON i.idContPerson = cpa.idContPers and cpa.idAddressType = 1
                    left outer join Client c On c.idClient = i.idClient
                    LEFT   JOIN ClientAddress ca ON c.idClient = ca.idClient and ca.idAddressType = 1  
                    WHERE idInvoice =  ( SELECT top 1 ii.idInvoice FROM Invoice i
                    LEFT OUTER JOIN Invoice ii ON ii.InvoiceNr=i.InvoiceNr WHERE ii.invoiceRbr = '001' AND ii.brutoAmount<0 AND i.idInvoice = @idInvoice)");
            // LEFT JOIN ContactPerson cp ON i.idContPerson = cp.idContPers
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idInvoice", SqlDbType.Int);
            sqlParameters[0].Value = idInvoice;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable checkIfThereIsInvoiceForCancelingWithBasicNotForCanceling(int idArrangementBook)
        {
            string query = string.Format(@"SELECT COUNT(*) as number
                                            FROM Invoice 
                                            WHERE idVoucher  IN (SELECT DISTINCT idArrangementBook FROM ArrangementBook where idArrangementBook = @idArrangementBook) AND InvoiceNr IN (SELECT DISTINCT InvoiceNr
                                            FROM Invoice 
                                            WHERE ((invoiceRbr = '000' OR invoiceRbr = '001') AND (idInvoiceStatus  = '1' OR idInvoiceStatus  = '6')) and idVoucher  IN (SELECT DISTINCT idArrangementBook FROM ArrangementBook where idArrangementBook = @idArrangementBook))
                                            AND invoiceRbr <> '000' AND invoiceRbr <> '001' AND idInvoiceStatus <>'1' AND idInvoiceStatus <>'6'");
            // LEFT JOIN ContactPerson cp ON i.idContPerson = cp.idContPers
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idArrangementBook", SqlDbType.Int);
            sqlParameters[0].Value = idArrangementBook;


            return conn.executeSelectQuery(query, sqlParameters);
        }



        public bool Save(InvoiceModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO Invoice (invoiceNr,invoiceRbr, idVoucher, idInvoiceStatus, descriptionInvoice, idClient, idContPerson, 
                  dtInvoice, dtValuta, brutoAmount, netoAmount, idBtw, isBooked, noteInvoice, userCreated, dtCreated, userModified, dtModified,
                     dtFirstPay,dtLastPay,percentFrstPay,reservationCost,firstReferencePay,secondReferencePay,typeInvoice,roomComment )
                  VALUES
                  (@invoiceNr,@invoiceRbr, @idVoucher, @idInvoiceStatus, @descriptionInvoice, @idClient, @idContPerson, @dtInvoice, @dtValuta, 
                   @brutoAmount, @netoAmount, @idBtw, @isBooked, @noteInvoice, @userCreated, @dtCreated, @userModified, @dtModified,
                      @dtFirstPay,@dtLastPay,@percentFrstPay,@reservationCost,@firstReferencePay,@secondReferencePay,@typeInvoice, @roomComment )");

            SqlParameter[] sqlParameter = new SqlParameter[26];

            sqlParameter[0] = new SqlParameter("@invoiceNr", SqlDbType.NVarChar);
            sqlParameter[0].Value = model.invoiceNr;

            sqlParameter[1] = new SqlParameter("@idVoucher", SqlDbType.Int);
            sqlParameter[1].Value = model.idVoucher;

            sqlParameter[2] = new SqlParameter("@idInvoiceStatus", SqlDbType.Int);
            sqlParameter[2].Value = model.idInvoiceStatus;

            sqlParameter[3] = new SqlParameter("@descriptionInvoice", SqlDbType.NVarChar);
            sqlParameter[3].Value = model.descriptionInvoice;

            sqlParameter[4] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[4].Value = model.idClient;

            sqlParameter[5] = new SqlParameter("@idContPerson", SqlDbType.Int);
            sqlParameter[5].Value = model.idContPerson;

            sqlParameter[6] = new SqlParameter("@dtInvoice", SqlDbType.DateTime);
            sqlParameter[6].Value = model.dtInvoice;

            sqlParameter[7] = new SqlParameter("@dtValuta", SqlDbType.DateTime);
            sqlParameter[7].Value = model.dtValuta;

            sqlParameter[8] = new SqlParameter("@brutoAmount", SqlDbType.Decimal);
            sqlParameter[8].Value = model.brutoAmount;

            sqlParameter[9] = new SqlParameter("@netoAmount", SqlDbType.Decimal);
            sqlParameter[9].Value = model.netoAmount;

            sqlParameter[10] = new SqlParameter("@idBtw", SqlDbType.Int);
            sqlParameter[10].Value = model.idBtw;

            sqlParameter[11] = new SqlParameter("@isBooked", SqlDbType.Bit);
            sqlParameter[11].Value = model.isBooked;

            sqlParameter[12] = new SqlParameter("@noteInvoice", SqlDbType.NVarChar);
            sqlParameter[12].Value = model.noteInvoice;

            sqlParameter[13] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[13].Value = model.userCreated;

            sqlParameter[14] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[14].Value = model.dtCreated;

            sqlParameter[15] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[15].Value = model.userModified;

            sqlParameter[16] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[16].Value = model.dtModified;

            sqlParameter[17] = new SqlParameter("@invoiceRbr", SqlDbType.NVarChar);
            sqlParameter[17].Value = model.invoiceRbr;

            sqlParameter[18] = new SqlParameter("@dtFirstPay", SqlDbType.DateTime);
            sqlParameter[18].Value = model.dtFirstPay ;
           
            sqlParameter[19] = new SqlParameter("@dtLastPay", SqlDbType.DateTime); 
            sqlParameter[19].Value = model.dtLastPay; // == null || model.dtLastPay == DateTime.MinValue) ? SqlDateTime.Null : model.dtLastPay;// model.dtLastPay;

            sqlParameter[20] = new SqlParameter("@percentFrstPay", SqlDbType.Decimal);
            sqlParameter[20].Value = model.percentFrstPay;

            sqlParameter[21] = new SqlParameter("@reservationCost", SqlDbType.Decimal);
            sqlParameter[21].Value = model.reservationCost;

            sqlParameter[22] = new SqlParameter("@firstreferencePay", SqlDbType.NVarChar);
            sqlParameter[22].Value = model.firstreferencePay;

            sqlParameter[23] = new SqlParameter("@secondreferencePay", SqlDbType.NVarChar);
            sqlParameter[23].Value = model.secondreferencePay;

            sqlParameter[24] = new SqlParameter("@typeinvoice", SqlDbType.Int);
            sqlParameter[24].Value = model.typeinvoice;

            sqlParameter[25] = new SqlParameter("@roomComment", SqlDbType.NVarChar);
            sqlParameter[25].Value = model.roomComment;

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
            sqlParameter[4].Value = conn.GetLastTableID("Invoice") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idInvoice";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Invoice";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean Update(InvoiceModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE Invoice SET 
                    invoiceNr = @invoiceNr, invoiceRbr=@invoiceRbr, idVoucher = @idVoucher, idInvoiceStatus = @idInvoiceStatus, 
                    descriptionInvoice = @descriptionInvoice, idClient = @idClient, idContPerson = @idContPerson, 
                    dtInvoice = @dtInvoice, dtValuta = @dtValuta, brutoAmount = @brutoAmount, netoAmount = @netoAmount, 
                    idBtw = @idBtw, isBooked = @isBooked, noteInvoice = @noteInvoice, userCreated = @userCreated, dtCreated = @dtCreated,
                    userModified = @userModified, dtModified = @dtModified,dtFirstPay=@dtFirstPay,dtLastPay=@dtLastPay,
                    percentFrstPay=@percentFrstPay,reservationCost=@reservationCost,firstReferencePay=@firstReferencePay,
                    secondReferencePay=@secondReferencePay,typeInvoice=@typeInvoice , roomComment = @roomComment
                    WHERE idInvoice = @idInvoice ");
            
            SqlParameter[] sqlParameter = new SqlParameter[27];

            sqlParameter[0] = new SqlParameter("@invoiceNr", SqlDbType.NVarChar);
            sqlParameter[0].Value = model.invoiceNr;

            sqlParameter[1] = new SqlParameter("@idVoucher", SqlDbType.Int);
            sqlParameter[1].Value = model.idVoucher;

            sqlParameter[2] = new SqlParameter("@idInvoiceStatus", SqlDbType.Int);
            sqlParameter[2].Value = model.idInvoiceStatus;

            sqlParameter[3] = new SqlParameter("@descriptionInvoice", SqlDbType.NVarChar);
            sqlParameter[3].Value = model.descriptionInvoice;

            sqlParameter[4] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[4].Value = model.idClient;

            sqlParameter[5] = new SqlParameter("@idContPerson", SqlDbType.Int);
            sqlParameter[5].Value = model.idContPerson;

            sqlParameter[6] = new SqlParameter("@dtInvoice", SqlDbType.DateTime);
            sqlParameter[6].Value = model.dtInvoice;

            sqlParameter[7] = new SqlParameter("@dtValuta", SqlDbType.DateTime);
            sqlParameter[7].Value = model.dtValuta;

            sqlParameter[8] = new SqlParameter("@brutoAmount", SqlDbType.Decimal);
            sqlParameter[8].Value = model.brutoAmount;

            sqlParameter[9] = new SqlParameter("@netoAmount", SqlDbType.Decimal);
            sqlParameter[9].Value = model.netoAmount;

            sqlParameter[10] = new SqlParameter("@idBtw", SqlDbType.Int);
            sqlParameter[10].Value = model.idBtw;

            sqlParameter[11] = new SqlParameter("@isBooked", SqlDbType.Bit);
            sqlParameter[11].Value = model.isBooked;

            sqlParameter[12] = new SqlParameter("@noteInvoice", SqlDbType.NVarChar);
            sqlParameter[12].Value = model.noteInvoice;

            sqlParameter[13] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[13].Value = model.userCreated;

            sqlParameter[14] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[14].Value = model.dtCreated;

            sqlParameter[15] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[15].Value = model.userModified;

            sqlParameter[16] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[16].Value = model.dtModified;

            sqlParameter[17] = new SqlParameter("@idInvoice", SqlDbType.Int);
            sqlParameter[17].Value = model.idInvoice;

            sqlParameter[18] = new SqlParameter("@invoiceRbr", SqlDbType.NVarChar);
            sqlParameter[18].Value = model.invoiceRbr;

            sqlParameter[19] = new SqlParameter("@dtFirstPay", SqlDbType.DateTime);
            sqlParameter[19].Value = model.dtFirstPay;

            sqlParameter[20] = new SqlParameter("@dtLastPay", SqlDbType.DateTime);
            sqlParameter[20].Value = model.dtLastPay;

            sqlParameter[21] = new SqlParameter("@percentFrstPay", SqlDbType.Decimal);
            sqlParameter[21].Value = model.percentFrstPay;

            sqlParameter[22] = new SqlParameter("@reservationCost", SqlDbType.Decimal);
            sqlParameter[22].Value = model.reservationCost;

            sqlParameter[23] = new SqlParameter("@firstreferencePay", SqlDbType.NVarChar);
            sqlParameter[23].Value = model.firstreferencePay;

            sqlParameter[24] = new SqlParameter("@secondreferencePay", SqlDbType.NVarChar);
            sqlParameter[24].Value = model.secondreferencePay;

            sqlParameter[25] = new SqlParameter("@typeInvoice", SqlDbType.Int);
            sqlParameter[25].Value = model.typeinvoice;

            sqlParameter[26] = new SqlParameter("@roomComment", SqlDbType.NVarChar);
            sqlParameter[26].Value = model.roomComment;

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
            sqlParameter[4].Value = model.idInvoice.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idInvoice";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Invoice";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public DataTable GetInvoicesForPrintForBooking(int idArrangement, int invStatus)
        {
            string query = string.Format(@"select f.descInvoicestatus, 
               CASE WHEN a.idContPerson = 0 THEN ec.nameClient ELSE e.firstname + ' '+ e.lastname END  as namePerson, *,
               CASE WHEN a.idContPerson = 0 THEN
               (
                CASE WHEN 
                (SELECT TOP 1 isInvoicing FROM ClientEmail cpe WHERE cpe.idClient = a.idClient AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END)
                ELSE
                (CASE WHEN 
                (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = e.idContPers AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END) END as isInvoicing,
                CASE WHEN a.idContPerson = 0 THEN
               (CASE WHEN (SELECT TOP 1 isInvoicing FROM ClientEmail cpe WHERE cpe.idClient = a.idClient AND isInvoicing = 'true') IS NULL THEN '' ELSE (SELECT TOP 1 email FROM ClientEmail cpe WHERE cpe.idClient = a.idClient AND isInvoicing = 'true') END)
                ELSE
                ( CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = e.idContPers AND isInvoicing = 'true') IS NULL THEN '' ELSE (SELECT TOP 1 email FROM ContactPersonEmail cpe WHERE cpe.idContPers = e.idContPers AND isInvoicing = 'true') END) END as email  
                 from invoice a
                 left join arrangementbook c on c.idArrangementBook = idVoucher
                 left join arrangement d on d.idArrangement = c.idArrangement
                 left join ContactPerson e on e.idContPers = a.idcontperson
                 left join Client ec on ec.idClient = a.idClient
                 left join InvoiceStatus f on f.idInvoiceStatus = a.idInvoiceStatus
                where c.idArrangement = @idArrangement and a.idInvoiceStatus = @invStatus and a.invoiceRbr <> '000' and a.brutoAmount > 0 order by idVoucher");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            sqlParameters[1] = new SqlParameter("@invStatus", SqlDbType.Int);
            sqlParameters[1].Value = invStatus;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetInvoicesForPrintForBookingByLabel(int idLabel, int invStatus)
        {
            string query = string.Format(@"select f.descInvoicestatus, 
               CASE WHEN a.idContPerson = 0 THEN ec.nameClient ELSE e.firstname + ' '+ e.lastname END  as namePerson, *,
               CASE WHEN a.idContPerson = 0 THEN
               (
                CASE WHEN 
                (SELECT TOP 1 isInvoicing FROM ClientEmail cpe WHERE cpe.idClient = a.idClient AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END)
                ELSE
                (CASE WHEN 
                (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = e.idContPers AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END) END as isInvoicing,
                CASE WHEN a.idContPerson = 0 THEN
               (CASE WHEN (SELECT TOP 1 isInvoicing FROM ClientEmail cpe WHERE cpe.idClient = a.idClient AND isInvoicing = 'true') IS NULL THEN '' ELSE (SELECT TOP 1 email FROM ClientEmail cpe WHERE cpe.idClient = a.idClient AND isInvoicing = 'true') END)
                ELSE
                ( CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = e.idContPers AND isInvoicing = 'true') IS NULL THEN '' ELSE (SELECT TOP 1 email FROM ContactPersonEmail cpe WHERE cpe.idContPers = e.idContPers AND isInvoicing = 'true') END) END as email  
                 from invoice a
                 left join arrangementbook c on c.idArrangementBook = idVoucher
                 left join arrangement d on d.idArrangement = c.idArrangement
                 left join ContactPerson e on e.idContPers = a.idcontperson
                 left join Client ec on ec.idClient = a.idClient
                 left join InvoiceStatus f on f.idInvoiceStatus = a.idInvoiceStatus
                where c.idArrangement IN (SELECT distinct idArrangement FROM ArrangementLabel WHERE idLabel = @idLabel) and a.idInvoiceStatus = @invStatus and a.invoiceRbr <> '000' and a.brutoAmount > 0 order by idVoucher");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@idLabel", SqlDbType.Int);
            sqlParameters[0].Value = idLabel;

            sqlParameters[1] = new SqlParameter("@invStatus", SqlDbType.Int);
            sqlParameters[1].Value = invStatus;

            return conn.executeSelectQuery(query, sqlParameters);
        }


        public Boolean UpdateStatus(int status, int idInvoice, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE Invoice SET idInvoiceStatus = @status
                                           WHERE idInvoice = @idInvoice ");

            SqlParameter[] sqlParameter = new SqlParameter[2];

            sqlParameter[0] = new SqlParameter("@status", SqlDbType.Int);
            sqlParameter[0].Value = status;

            sqlParameter[1] = new SqlParameter("@idInvoice", SqlDbType.Int);
            sqlParameter[1].Value = idInvoice;

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
            sqlParameter[4].Value = idInvoice.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idInvoice";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Invoice";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update status";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean Detele(int idInvoice, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE Invoice WHERE idInvoice = @idInvoice");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idInvoice", SqlDbType.Int);
            sqlParameter[0].Value = idInvoice;

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
            sqlParameter[4].Value = idInvoice.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idInvoice";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Invoice";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Detele";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        //NOVO:

//        public DataTable GetInvoiceDataPrint(DateTime dateFrom, DateTime dateTo, string insurance)
//        {
//            string query = string.Format(@"SELECT inv.invoiceNr +'-'+inv.invoiceRbr as invoiceNr,inv.dtInvoice,CASE WHEN t.nameTitle IS NULL THEN '' ELSE  t.nameTitle END + ' ' +CASE WHEN cpr.initialsContPers IS NULL THEN '' ELSE  cpr.initialsContPers END + ' ' + CASE WHEN cpr.firstname IS NULL THEN '' ELSE  cpr.firstname END + ' ' + 
//            CASE WHEN cpr.midname IS NULL THEN  '' ELSE  cpr.midname END+ ' ' + CASE WHEN cpr.lastname IS NULL THEN  '' ELSE  cpr.lastname END AS namePerson,a.dtFromArrangement as dtFrom,a.dtToArrangement as dtTo,a.nameArrangement,
//            item.price
//            FROM Invoice inv
//            LEFT OUTER JOIN ArrangementBook ab on inv.idVoucher=ab.idArrangementBook 
//            LEFT OUTER JOIN ContactPerson cpr on ab.idContPers=cpr.idContPers
//            LEFT OUTER JOIN Arrangement a on ab.idArrangement=a.idArrangement 
//            LEFT OUTER JOIN (select iitem.price, iitem.idArtical,iitem.idInvoice  from InvoiceItems iitem WHERE iitem.idArtical='" + insurance + @"') item on item.idInvoice= inv.idInvoice 
//            LEFT OUTER JOIN Title t on t.idTitle=cpr.idTitle
//            WHERE  item.idArtical='" + insurance + "' AND inv.dtInvoice BETWEEN '" + dateFrom.ToString("MM/dd/yyyy") + "' AND '" + dateTo.ToString("MM/dd/yyyy") + "'");

//            return conn.executeSelectQuery(query, null);

//        }
        public DataTable GetInvoiceDataPrint(DateTime dateFrom, DateTime dateTo, string insurance)
        {
            string query = string.Format(@"SELECT a.codeArrangement,inv.invoiceNr +'-'+inv.invoiceRbr as invoiceNr,inv.dtInvoice,CASE WHEN t.nameTitle IS NULL THEN '' ELSE  t.nameTitle END + ' ' +CASE WHEN cpr.initialsContPers IS NULL THEN '' ELSE  cpr.initialsContPers END + ' ' + CASE WHEN cpr.firstname IS NULL THEN '' ELSE  cpr.firstname END + ' ' + 
            CASE WHEN cpr.midname IS NULL THEN  '' ELSE  cpr.midname END+ ' ' + CASE WHEN cpr.lastname IS NULL THEN  '' ELSE  cpr.lastname END AS namePerson,a.dtFromArrangement as dtFrom,a.dtToArrangement as dtTo,a.nameArrangement,
            item.price
            FROM Invoice inv
            LEFT OUTER JOIN ArrangementBook ab on inv.idVoucher=ab.idArrangementBook 
            LEFT OUTER JOIN ContactPerson cpr on ab.idContPers=cpr.idContPers
            LEFT OUTER JOIN Arrangement a on ab.idArrangement=a.idArrangement 
            LEFT OUTER JOIN (select iitem.price, iitem.idArtical,iitem.idInvoice  from InvoiceItems iitem WHERE iitem.idArtical='" + insurance + @"') item on item.idInvoice= inv.idInvoice 
            LEFT OUTER JOIN Title t on t.idTitle=cpr.idTitle
            WHERE  item.idArtical='" + insurance + "' AND a.dtFromArrangement BETWEEN '" + dateFrom.ToString("MM/dd/yyyy") + "' AND '" + dateTo.ToString("MM/dd/yyyy") + "'");

            return conn.executeSelectQuery(query, null);

        }

//        public DataTable GetInvoiceArrangementDataPrint(string insurance, int idArrangement)
//        {
//            string query = string.Format(@"SELECT inv.invoiceNr +'-'+inv.invoiceRbr as invoiceNr,inv.dtInvoice,CASE WHEN t.nameTitle IS NULL THEN '' ELSE  t.nameTitle END + ' ' +CASE WHEN cpr.initialsContPers IS NULL THEN '' ELSE  cpr.initialsContPers END + ' ' + CASE WHEN cpr.firstname IS NULL THEN '' ELSE  cpr.firstname END + ' ' + 
//            CASE WHEN cpr.midname IS NULL THEN  '' ELSE  cpr.midname END+ ' ' + CASE WHEN cpr.lastname IS NULL THEN  '' ELSE  cpr.lastname END AS namePerson,a.dtFromArrangement as dtFrom,a.dtToArrangement as dtTo,a.nameArrangement,
//            item.price
//            FROM Invoice inv
//            LEFT OUTER JOIN ArrangementBook ab on inv.idVoucher=ab.idArrangementBook 
//            LEFT OUTER JOIN ContactPerson cpr on ab.idContPers=cpr.idContPers
//            LEFT OUTER JOIN Arrangement a on ab.idArrangement=a.idArrangement 
//            LEFT OUTER JOIN (select iitem.price, iitem.idArtical,iitem.idInvoice  from InvoiceItems iitem WHERE iitem.idArtical='" + insurance + @"') item on item.idInvoice= inv.idInvoice 
//            LEFT OUTER JOIN Title t on t.idTitle=cpr.idTitle
//             WHERE  item.idArtical='" + insurance + "' and ab.idArrangement='" + idArrangement + "'");

//            return conn.executeSelectQuery(query, null);

//        }
        public DataTable GetInvoiceArrangementDataPrint(string insurance, int idArrangement)
        {
            string query = string.Format(@"SELECT a.codeArrangement, inv.invoiceNr +'-'+inv.invoiceRbr as invoiceNr,inv.dtInvoice,CASE WHEN t.nameTitle IS NULL THEN '' ELSE  t.nameTitle END + ' ' +CASE WHEN cpr.initialsContPers IS NULL THEN '' ELSE  cpr.initialsContPers END + ' ' + CASE WHEN cpr.firstname IS NULL THEN '' ELSE  cpr.firstname END + ' ' + 
            CASE WHEN cpr.midname IS NULL THEN  '' ELSE  cpr.midname END+ ' ' + CASE WHEN cpr.lastname IS NULL THEN  '' ELSE  cpr.lastname END AS namePerson,a.dtFromArrangement as dtFrom,a.dtToArrangement as dtTo,a.nameArrangement,
            item.price
            FROM Invoice inv
            LEFT OUTER JOIN ArrangementBook ab on inv.idVoucher=ab.idArrangementBook 
            LEFT OUTER JOIN ContactPerson cpr on ab.idContPers=cpr.idContPers
            LEFT OUTER JOIN Arrangement a on ab.idArrangement=a.idArrangement 
            LEFT OUTER JOIN (select iitem.price, iitem.idArtical,iitem.idInvoice  from InvoiceItems iitem WHERE iitem.idArtical='" + insurance + @"') item on item.idInvoice= inv.idInvoice 
            LEFT OUTER JOIN Title t on t.idTitle=cpr.idTitle
             WHERE  item.idArtical='" + insurance + "' and ab.idArrangement='" + idArrangement + "'");

            return conn.executeSelectQuery(query, null);

        }

        public DataTable GetInvoicesForAccounting(int idArrangement, int invStatus)
        {
            string query = string.Format(@"select f.descInvoicestatus, 
               CASE WHEN a.idContPerson = 0 THEN ec.nameClient ELSE e.firstname + ' '+ e.lastname END  as namePerson, *,
               CASE WHEN a.idContPerson = 0 THEN
               (
                CASE WHEN 
                (SELECT TOP 1 isInvoicing FROM ClientEmail cpe WHERE cpe.idClient = a.idClient AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END)
                ELSE
                (CASE WHEN 
                (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = e.idContPers AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END) END as isInvoicing,
                CASE WHEN a.idContPerson = 0 THEN
               (CASE WHEN (SELECT TOP 1 isInvoicing FROM ClientEmail cpe WHERE cpe.idClient = a.idClient AND isInvoicing = 'true') IS NULL THEN '' ELSE (SELECT TOP 1 email FROM ClientEmail cpe WHERE cpe.idClient = a.idClient AND isInvoicing = 'true') END)
                ELSE
                ( CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = e.idContPers AND isInvoicing = 'true') IS NULL THEN '' ELSE (SELECT TOP 1 email FROM ContactPersonEmail cpe WHERE cpe.idContPers = e.idContPers AND isInvoicing = 'true') END) END as email  
                 from invoice a
                 left join arrangementbook c on c.idArrangementBook = idVoucher
                 left join arrangement d on d.idArrangement = c.idArrangement
                 left join ContactPerson e on e.idContPers = a.idcontperson
                 left join Client ec on ec.idClient = a.idClient
                 left join InvoiceStatus f on f.idInvoiceStatus = a.idInvoiceStatus
                where c.idArrangement = @idArrangement and a.idInvoiceStatus = @invStatus order by idVoucher");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            sqlParameters[1] = new SqlParameter("@invStatus", SqlDbType.Int);
            sqlParameters[1].Value = invStatus;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetAllInvoicesForAccounting(int invStatus)
        {
            string query = string.Format(@"select f.descInvoicestatus, 
               CASE WHEN a.idContPerson = 0 THEN ec.nameClient ELSE e.firstname + ' '+ e.lastname END  as namePerson, *,
               CASE WHEN a.idContPerson = 0 THEN
               (
                CASE WHEN 
                (SELECT TOP 1 isInvoicing FROM ClientEmail cpe WHERE cpe.idClient = a.idClient AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END)
                ELSE
                (CASE WHEN 
                (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = e.idContPers AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END) END as isInvoicing,
                CASE WHEN a.idContPerson = 0 THEN
               (CASE WHEN (SELECT TOP 1 isInvoicing FROM ClientEmail cpe WHERE cpe.idClient = a.idClient AND isInvoicing = 'true') IS NULL THEN '' ELSE (SELECT TOP 1 email FROM ClientEmail cpe WHERE cpe.idClient = a.idClient AND isInvoicing = 'true') END)
                ELSE
                ( CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = e.idContPers AND isInvoicing = 'true') IS NULL THEN '' ELSE (SELECT TOP 1 email FROM ContactPersonEmail cpe WHERE cpe.idContPers = e.idContPers AND isInvoicing = 'true') END) END as email  
                 from invoice a
                 left join arrangementbook c on c.idArrangementBook = idVoucher
                 left join arrangement d on d.idArrangement = c.idArrangement
                 left join ContactPerson e on e.idContPers = a.idcontperson
                 left join Client ec on ec.idClient = a.idClient
                 left join InvoiceStatus f on f.idInvoiceStatus = a.idInvoiceStatus
                where a.idInvoiceStatus = @invStatus order by idVoucher");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@invStatus", SqlDbType.Int);
            sqlParameters[0].Value = invStatus;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetInvoicesForAccountingByLabel(int idLabel, int invStatus)
        {
            string query = string.Format(@"select f.descInvoicestatus, 
              CASE WHEN a.idContPerson = 0 THEN ec.nameClient ELSE e.firstname + ' '+ e.lastname END  as namePerson, *,
               CASE WHEN a.idContPerson = 0 THEN
               (
                CASE WHEN 
                (SELECT TOP 1 isInvoicing FROM ClientEmail cpe WHERE cpe.idClient = a.idClient AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END)
                ELSE
                (CASE WHEN 
                (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = e.idContPers AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END) END as isInvoicing,
                CASE WHEN a.idContPerson = 0 THEN
               (CASE WHEN (SELECT TOP 1 isInvoicing FROM ClientEmail cpe WHERE cpe.idClient = a.idClient AND isInvoicing = 'true') IS NULL THEN '' ELSE (SELECT TOP 1 email FROM ClientEmail cpe WHERE cpe.idClient = a.idClient AND isInvoicing = 'true') END)
                ELSE
                ( CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = e.idContPers AND isInvoicing = 'true') IS NULL THEN '' ELSE (SELECT TOP 1 email FROM ContactPersonEmail cpe WHERE cpe.idContPers = e.idContPers AND isInvoicing = 'true') END) END as email  
                 from invoice a
                 left join arrangementbook c on c.idArrangementBook = idVoucher
                 left join arrangement d on d.idArrangement = c.idArrangement
                 left join ContactPerson e on e.idContPers = a.idcontperson
                 left join Client ec on ec.idClient = a.idClient
                 left join InvoiceStatus f on f.idInvoiceStatus = a.idInvoiceStatus
                where c.idArrangement IN (SELECT distinct idArrangement FROM ArrangementLabel WHERE idLabel = @idLabel) and a.idInvoiceStatus = @invStatus order by idVoucher");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@idLabel", SqlDbType.Int);
            sqlParameters[0].Value = idLabel;

            sqlParameters[1] = new SqlParameter("@invStatus", SqlDbType.Int);
            sqlParameters[1].Value = invStatus;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetReportInvoiceReportByIntID(int idInvoice)
        {
            string query = string.Format(@"                    SELECT idInvoice, invoiceNr,invoiceRbr, idVoucher, i.idInvoiceStatus,st.descInvoiceStatus, descriptionInvoice, i.idClient,
                    idContPerson, dtInvoice, dtValuta, brutoAmount,CASE WHEN (invoiceRbr = '001' OR invoiceRbr = '000') and brutoAmount>0 
                    THEN 
                    CASE WHEN (CASE WHEN invoiceRbr = '000' OR invoiceRbr = '001' THEN (SELECT TOP 1 brutoAmount from Invoice where invoiceRbr = '000' and invoiceNr = i.invoiceNr AND brutoAmount>0) ELSE brutoAmount END) IS NULL THEN 0 ELSE (CASE WHEN invoiceRbr = '000' OR invoiceRbr = '001' THEN (SELECT TOP 1 brutoAmount from Invoice where invoiceRbr = '000' and invoiceNr = i.invoiceNr AND brutoAmount>0) ELSE brutoAmount END) END +
                    CASE WHEN ( CASE WHEN invoiceRbr = '000' OR invoiceRbr = '001' THEN (SELECT TOP 1 brutoAmount from Invoice where invoiceRbr = '001' and invoiceNr = i.invoiceNr AND brutoAmount>0) ELSE brutoAmount END ) IS NULL  THEN 0 ELSE ( CASE WHEN invoiceRbr = '000' OR invoiceRbr = '001' THEN (SELECT TOP 1 brutoAmount from Invoice where invoiceRbr = '001' and invoiceNr = i.invoiceNr AND brutoAmount>0) ELSE brutoAmount END ) END
                    ELSE brutoAmount END as netoAmount, idBtw, isBooked, noteInvoice, i.userCreated, i.dtCreated,i.userModified,i.dtModified,
                    dtFirstPay,dtLastPay,percentFrstPay,reservationCost, 
                    CASE WHEN ((a.idClientInvoice IS NULL OR a.idClientInvoice=0) AND i.idContPerson<>0) AND i.idContPerson<>0  THEN CASE WHEN ((cpa.street IS NULL OR cpa.street='') AND
                    (cpa.extension IS NULL OR cpa.extension='') AND (cpa.postalCode IS NULL OR cpa.postalCode='') AND (cpa.city IS NULL OR cpa.city='') AND (cpa.housenr IS NULL OR cpa.housenr='')) THEN cpa2.street ELSE cpa.street END 
                    ELSE (CASE WHEN ((cla.street IS NULL OR cla.street='') AND
                    (cla.extension IS NULL OR cla.extension='') AND (cla.postalCode IS NULL OR cla.postalCode='') AND (cla.city IS NULL OR cla.city='') AND (cla.housenr IS NULL OR cla.housenr='')) THEN cla2.street ELSE cla.street END) END as street,
                    CASE WHEN (a.idClientInvoice IS NULL OR a.idClientInvoice=0) AND i.idContPerson<>0 THEN  CASE WHEN ((cpa.street IS NULL OR cpa.street='') AND
                    (cpa.extension IS NULL OR cpa.extension='') AND (cpa.postalCode IS NULL OR cpa.postalCode='') AND (cpa.city IS NULL OR cpa.city='') AND (cpa.housenr IS NULL OR cpa.housenr='')) THEN cpa2.housenr ELSE cpa.housenr END  ELSE (CASE WHEN ((cla.street IS NULL OR cla.street='') AND
                    (cla.extension IS NULL OR cla.extension='') AND (cla.postalCode IS NULL OR cla.postalCode='') AND (cla.city IS NULL OR cla.city='') AND (cla.housenr IS NULL OR cla.housenr='')) THEN cla2.housenr ELSE cla.housenr END) END as housenr
                    ,CASE WHEN (a.idClientInvoice IS NULL OR a.idClientInvoice=0) AND i.idContPerson<>0 THEN CASE WHEN ((cpa.street IS NULL OR cpa.street='') AND
                    (cpa.extension IS NULL OR cpa.extension='') AND (cpa.postalCode IS NULL OR cpa.postalCode='') AND (cpa.city IS NULL OR cpa.city='') AND (cpa.housenr IS NULL OR cpa.housenr='')) THEN cpa2.extension ELSE cpa.extension END ELSE (CASE WHEN ((cla.street IS NULL OR cla.street='') AND
                    (cla.extension IS NULL OR cla.extension='') AND (cla.postalCode IS NULL OR cla.postalCode='') AND (cla.city IS NULL OR cla.city='') AND (cla.housenr IS NULL OR cla.housenr='')) THEN cla2.extension ELSE cla.extension END)  END as extend,
                    CASE WHEN (a.idClientInvoice IS NULL OR a.idClientInvoice=0) AND i.idContPerson<>0 THEN
                     CASE WHEN ((cpa.street IS NULL OR cpa.street='') AND
                    (cpa.extension IS NULL OR cpa.extension='') AND (cpa.postalCode IS NULL OR cpa.postalCode='') AND (cpa.city IS NULL OR cpa.city='') AND (cpa.housenr IS NULL OR cpa.housenr='')) 
                    THEN  (CASE WHEN LEN(cpa2.postalCode)=6 THEN SUBSTRING(cpa2.postalCode,1,4) + ' ' + SUBSTRING(cpa2.postalCode,5,2) ELSE cpa2.postalCode END) 
                    ELSE (CASE WHEN LEN(cpa.postalCode)=6 THEN SUBSTRING(cpa.postalCode,1,4) + ' ' + SUBSTRING(cpa.postalCode,5,2) ELSE cpa.postalCode END)  END
                    ELSE (CASE WHEN ((cla.street IS NULL OR cla.street='') AND
                    (cla.extension IS NULL OR cla.extension='') AND (cla.postalCode IS NULL OR cla.postalCode='') AND (cla.city IS NULL OR cla.city='') AND (cla.housenr IS NULL OR cla.housenr='')) THEN 
                    (CASE WHEN LEN(cla2.postalCode)=6 THEN SUBSTRING(cla2.postalCode,1,4) + ' ' + SUBSTRING(cla2.postalCode,5,2) ELSE cla2.postalCode END) ELSE
                    (CASE WHEN LEN(cla.postalCode)=6 THEN SUBSTRING(cla.postalCode,1,4) + ' ' + SUBSTRING(cla.postalCode,5,2) ELSE cla.postalCode END) END) END  as zip,
                    CASE WHEN (a.idClientInvoice IS NULL OR a.idClientInvoice=0) AND i.idContPerson<>0 THEN CASE WHEN ((cpa.street IS NULL OR cpa.street='') AND
                    (cpa.extension IS NULL OR cpa.extension='') AND (cpa.postalCode IS NULL OR cpa.postalCode='') AND (cpa.city IS NULL OR cpa.city='') AND (cpa.housenr IS NULL OR cpa.housenr='')) THEN cpa2.city ELSE cpa.city END ELSE (CASE WHEN ((cla.street IS NULL OR cla.street='') AND
                    (cla.extension IS NULL OR cla.extension='') AND (cla.postalCode IS NULL OR cla.postalCode='') AND (cla.city IS NULL OR cla.city='')) THEN cla2.city ELSE cla.city END) END as City, space(36) as country,
                    CASE WHEN (a.idClientInvoice IS NULL OR a.idClientInvoice=0) AND i.idContPerson<>0 THEN (CASE WHEN t.nameTitle IS NULL THEN '' ELSE t.nameTitle + '. ' END +
                    CASE WHEN cp.initialsContPers IS NULL THEN '' ELSE cp.initialsContPers + ' ' END +
                    CASE WHEN cp.midname IS NULL THEN '' ELSE cp.midname + ' ' END +
                    CASE WHEN cp.lastName IS NULL THEN '' ELSE cp.lastName END) ELSE cl.nameClient END as namePerson,a.nameArrangement as arrName, DATEDIFF(day,a.dtFromArrangement,a.dtToArrangement)+1  as noDays, abp.nameBoardingPoint as boarding,
                    a.dtFromArrangement as dateFrom, a.dtToArrangement as dateTo, hs.nameHotelService as service,CASE WHEN invoiceRbr = '000' OR invoiceRbr = '001' THEN Convert(nvarchar, ab.idContPers) + '/' + invoiceNr+'-000' ELSE Convert(nvarchar, ab.idContPers) + '/' + invoiceNr+'-'+invoiceRbr END as firstReference,
                    CASE WHEN invoiceRbr = '000' OR invoiceRbr = '001' THEN Convert(nvarchar, ab.idContPers) + '/' + invoiceNr+'-001' ELSE Convert(nvarchar, ab.idContPers) + '/' + invoiceNr+'-'+invoiceRbr END as restReference,firstReferencePay, secondReferencePay, typeInvoice 
                    ,CASE WHEN invoiceRbr = '000' OR invoiceRbr = '001' THEN (SELECT TOP 1 brutoAmount from Invoice where invoiceRbr = '000' and invoiceNr = i.invoiceNr AND brutoAmount>0) ELSE brutoAmount END as firstAmount,
                    CASE WHEN invoiceRbr = '000' OR invoiceRbr = '001' THEN (SELECT TOP 1 brutoAmount from Invoice where invoiceRbr = '001' and invoiceNr = i.invoiceNr AND brutoAmount>0) ELSE brutoAmount END  as restAmount
                    ,(SELECT invoiceDescription FROm Arrangement WHERE idArrangement = ab.idArrangement) as extraInformation,roomComment
                    ,ab.idContPers as idTraveler 
                    FROM Invoice i 
                    LEFT OUTER JOIN InvoiceStatus st ON st.idInvoiceStatus = i.idInvoiceStatus 
                    left outer join ContactPerson cp On cp.idContPers = i.idContPerson
                    left outer join Title t On t.idTitle= cp.idTitle
                    LEFT OUTER JOIN ContactPersonAddress cpa ON i.idContPerson = cpa.idContPers and cpa.idAddressType = 2
                    LEFT OUTER JOIN ContactPersonAddress cpa2 ON i.idContPerson = cpa2.idContPers and cpa2.idAddressType = 1 
                                  LEFT OUTER JOIN ArrangementBook ab ON ab.idArrangementBook = i.idVoucher
                                  LEFT OUTER JOIN Arrangement a ON ab.idArrangement = a.idArrangement
                                  LEFT OUTER JOIN Client cl On a.idClientInvoice = cl.idClient OR i.idClient = cl.idClient
                                  LEFT OUTER JOIN  ClientAddress  cla 
                                  ON cl.idClient = cla.idClient AND cla.idAddressType = 2
                                  LEFT OUTER JOIN  ClientAddress  cla2 
                                  ON cl.idClient = cla2.idClient AND cla2.idAddressType = 1 
                                  LEFT OUTER JOIN HotelService hs ON a.idHotelService = hs.idHotelService
                                  LEFT OUTER JOIN BoardingPoint abp ON ab.idBoarding = abp.idBoardingPoint
                    WHERE idInvoice = @idInvoice");

            // LEFT JOIN ContactPerson cp ON i.idContPerson = cp.idContPers
            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idInvoice", SqlDbType.Int);
            sqlParameters[0].Value = idInvoice;

            return conn.executeSelectQuery(query, sqlParameters);
        }



    }

    
}