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
    public class AccOpenLinesDAO
    {
        private dbConnection conn;

        public AccOpenLinesDAO()
        {
            conn = new dbConnection();
        }
        
        public DataTable GetAllAccOpenLines()
        {
            string query = string.Format(@"SELECT idOpenLine, idDebCre, typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
                                           creditOpenLine, codeCost, codeArr, idProject, idPayCondition, creditDays, discauntDays, periodOnenLines,account,iselected,referencePay,idOption,
                                            iban,term,idSepa,isFirstWarrningSent,dtFirstWarrning,isSecondWarrningSent,dtSecondWarrning,dtCreationLine
                                           FROM AccOpenLines WHERE debitOpenLine - creditOpenLine != 0");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetOpenLinesByDates(DateTime valuta, DateTime dateplus)
        {
//            string query = string.Format(@"SELECT idOpenLine, idDebCre, typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
//                                           creditOpenLine, codeCost, codeArr, idProject, idPayCondition, creditDays, discauntDays, periodOnenLines,account,iselected,referencePay,idOption,
//                                            iban
//                                           FROM AccOpenLines WHERE debitOpenLine - creditOpenLine != 0 and dtOpenline >= '" + valuta.ToString("yyyy-MM-dd") + "' and dtOpenline <= '"+dateplus.ToString("yyyy-MM-dd")+"' and idOption=1");
            //< '" + valuta.ToString("yyyy-MM-dd") + "' and dtOpenline
            string query = string.Format(@"SELECT idOpenLine, idDebCre, 
                    CASE WHEN adc.idContPerson = 0 OR adc.idContPerson is NULL then c.nameClient 
                    ELSE (CASE WHEN p.firstname IS NULL THEN p.lastname 
                    ELSE (CASE WHEN p.lastname IS NULL THEN '' ELSE p.firstname + ' ' + p.lastname END) END) END as name,                   
                    typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
                    creditOpenLine, codeCost, codeArr, idProject, idPayCondition, creditDays, discauntDays, periodOnenLines,account,iselected,referencePay,idOption,aop.iban,
                    term,idSepa,isFirstWarrningSent,dtFirstWarrning,isSecondWarrningSent,dtSecondWarrning,dtCreationLine
                    FROM AccOpenLines aop
                    LEFT JOIN AccDebCre adc ON aop.idDebCre = adc.accNumber
                    LEFT JOIN Client c ON c.idClient = adc.idClient
                    LEFT JOIN ContactPerson p ON p.idContPers = adc.idContPerson
                    WHERE debitOpenLine - creditOpenLine != 0 and creditOpenLine > debitOpenLine and dtOpenline  <= '" + dateplus.ToString("yyyy-MM-dd") + @"' 
                    and idOption=1 and idSepa=0 and account = (SELECT defCreditorAccount FROM AccSettings WHERE DATEPART(yyyy,GETDATE()) = yearSettings)  ");
                  //  ORDER BY days");
            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAccOpenLinesByID(string idClient)
        {
            string query = string.Format(@"SELECT idOpenLine, idDebCre, typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
                                           creditOpenLine, codeCost, codeArr, idProject, idPayCondition, creditDays, discauntDays, periodOnenLines,account,iselected,referencePay,idOption,
                                            iban, term,idSepa,isFirstWarrningSent,dtFirstWarrning,isSecondWarrningSent,dtSecondWarrning,dtCreationLine
                                           FROM AccOpenLines
                                           WHERE idDebCre=@idClient  and debitOpenLine - creditOpenLine != 0");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idClient", SqlDbType.NVarChar);
            sqlParameters[0].Value = idClient;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetAccOpenLinesSepa(int sepa)
        {
            string query = string.Format(@"SELECT idOpenLine, idDebCre, typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
                                           creditOpenLine, codeCost, codeArr, idProject, idPayCondition, creditDays, discauntDays, periodOnenLines,account,iselected,referencePay,idOption,
                                            iban, term,idSepa,isFirstWarrningSent,dtFirstWarrning,isSecondWarrningSent,dtSecondWarrning,dtCreationLine
                                           FROM AccOpenLines
                                           WHERE  idSepa = @sepa");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@sepa", SqlDbType.Int);
            sqlParameters[0].Value = sepa;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAccOpenLineReport(DateTime dtCombo, int deb_cre_all)
        {
            //deb_cre_all 
            //0 - all
            //1 - debitor
            //2 - creditor
            string query = "";

            if (deb_cre_all == 1)
            {
                query = string.Format(@"SELECT idOpenLine, idDebCre, 
                    CASE WHEN adc.idContPerson = 0 OR adc.idContPerson is NULL then c.nameClient 
                    ELSE (CASE WHEN p.firstname IS NULL THEN p.lastname 
                    ELSE (CASE WHEN p.lastname IS NULL THEN '' ELSE p.firstname + ' ' + p.lastname END) END) END as name,                   
                    typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
                    creditOpenLine,
                    DATEDIFF(d, dtOpenLine,@dateCombo) as days, debitOpenLine - creditOpenLine as dif , term,
                   adc.idContPerson, adc.idClient,isFirstWarrningSent,dtFirstWarrning,isSecondWarrningSent,dtSecondWarrning,dtCreationLine,
                    CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END as isInvoicing,
                    adc.isDebitor, adc.isCreditor,CASE WHEN aop.account = (SELECT defDebitorAccount FROM AccSettings WHERE yearSettings=(DATEPART(YYYY,GETDATE()))) THEN 'D' ELSE 
                    (CASE WHEN aop.account = (SELECT defCreditorAccount FROM AccSettings WHERE yearSettings=(DATEPART(YYYY,GETDATE()))) THEN 'C' ELSE '' END) END as [Debitor/Creditor]
                    FROM AccOpenLines aop
                    LEFT JOIN AccDebCre adc ON aop.idDebCre = adc.accNumber
                    LEFT JOIN Client c ON c.idClient = adc.idClient
                    LEFT JOIN ContactPerson p ON p.idContPers = adc.idContPerson
                    WHERE debitOpenLine - creditOpenLine != 0 AND isDebitor = '1'  AND CASE WHEN aop.account = (SELECT defDebitorAccount FROM AccSettings WHERE yearSettings=(DATEPART(YYYY,GETDATE()))) THEN 'D' ELSE 
                    (CASE WHEN aop.account = (SELECT defCreditorAccount FROM AccSettings WHERE yearSettings=(DATEPART(YYYY,GETDATE()))) THEN 'C' ELSE '' END) END ='D'
                    ORDER BY days");
            }
            else if (deb_cre_all == 2)
            {
                query = string.Format(@"SELECT idOpenLine, idDebCre, 
                    CASE WHEN adc.idContPerson = 0 OR adc.idContPerson is NULL then c.nameClient 
                    ELSE (CASE WHEN p.firstname IS NULL THEN p.lastname 
                    ELSE (CASE WHEN p.lastname IS NULL THEN '' ELSE p.firstname + ' ' + p.lastname END) END) END as name,                   
                    typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
                    creditOpenLine,
                    DATEDIFF(d, dtOpenLine,@dateCombo) as days, debitOpenLine - creditOpenLine as dif , term,
                    adc.idContPerson, adc.idClient,isFirstWarrningSent,dtFirstWarrning,isSecondWarrningSent,dtSecondWarrning,dtCreationLine,
                    CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END as isInvoicing,
                    adc.isDebitor, adc.isCreditor , CASE WHEN aop.account = (SELECT defDebitorAccount FROM AccSettings WHERE yearSettings=(DATEPART(YYYY,GETDATE()))) THEN 'D' ELSE 
                    (CASE WHEN aop.account = (SELECT defCreditorAccount FROM AccSettings WHERE yearSettings=(DATEPART(YYYY,GETDATE()))) THEN 'C' ELSE '' END) END as [Debitor/Creditor]
                    FROM AccOpenLines aop
                    LEFT JOIN AccDebCre adc ON aop.idDebCre = adc.accNumber
                    LEFT JOIN Client c ON c.idClient = adc.idClient
                    LEFT JOIN ContactPerson p ON p.idContPers = adc.idContPerson
                    WHERE debitOpenLine - creditOpenLine != 0 AND isCreditor = '1'  AND CASE WHEN aop.account = (SELECT defDebitorAccount FROM AccSettings WHERE yearSettings=(DATEPART(YYYY,GETDATE()))) THEN 'D' ELSE 
                    (CASE WHEN aop.account = (SELECT defCreditorAccount FROM AccSettings WHERE yearSettings=(DATEPART(YYYY,GETDATE()))) THEN 'C' ELSE '' END) END ='C'
                    ORDER BY days");
            }
            else
            {
                query = string.Format(@"SELECT idOpenLine, idDebCre, 
                    CASE WHEN adc.idContPerson = 0 OR adc.idContPerson is NULL then c.nameClient 
                    ELSE (CASE WHEN p.firstname IS NULL THEN p.lastname 
                    ELSE (CASE WHEN p.lastname IS NULL THEN '' ELSE p.firstname + ' ' + p.lastname END) END) END as name,                   
                    typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
                    creditOpenLine,
                    DATEDIFF(d, dtOpenLine,@dateCombo) as days, debitOpenLine - creditOpenLine as dif , term,
                    adc.idContPerson, adc.idClient,isFirstWarrningSent,dtFirstWarrning,isSecondWarrningSent,dtSecondWarrning,dtCreationLine,
                    CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END as isInvoicing,
                    adc.isDebitor, adc.isCreditor, CASE WHEN aop.account = (SELECT defDebitorAccount FROM AccSettings WHERE yearSettings=(DATEPART(YYYY,GETDATE()))) THEN 'D' ELSE 
                    (CASE WHEN aop.account = (SELECT defCreditorAccount FROM AccSettings WHERE yearSettings=(DATEPART(YYYY,GETDATE()))) THEN 'C' ELSE '' END) END as [Debitor/Creditor]
                    FROM AccOpenLines aop
                    LEFT JOIN AccDebCre adc ON aop.idDebCre = adc.accNumber
                    LEFT JOIN Client c ON c.idClient = adc.idClient
                    LEFT JOIN ContactPerson p ON p.idContPers = adc.idContPerson
                    WHERE debitOpenLine - creditOpenLine != 0
                    ORDER BY days");
            }

            //WHERE idDebCre = @idDebCre  and
            //codeCost, codeArr, idProject, idPayCondition, creditDays, discauntDays,
            //periodOnenLines,account,iselected,referencePay

            SqlParameter[] sqlParameters = new SqlParameter[1];

            //sqlParameters[0] = new SqlParameter("@idDebCre", SqlDbType.NVarChar);
            //sqlParameters[0].Value = idDebCre;

            sqlParameters[0] = new SqlParameter("@dateCombo", SqlDbType.DateTime);
            sqlParameters[0].Value = dtCombo;

            return conn.executeSelectQuery(query, sqlParameters);
        }



//        public DataTable GetAccOpenLineReport(DateTime dtCombo, int deb_cre_all)
//        {
//            //deb_cre_all 
//            //0 - all
//            //1 - debitor
//            //2 - creditor
//            string query = "";

//            if(deb_cre_all == 1)
//            {
//                query = string.Format(@"SELECT idOpenLine, idDebCre, 
//                    CASE WHEN adc.idContPerson = 0 OR adc.idContPerson is NULL then c.nameClient 
//                    ELSE (CASE WHEN p.firstname IS NULL THEN p.lastname 
//                    ELSE (CASE WHEN p.lastname IS NULL THEN '' ELSE p.firstname + ' ' + p.lastname END) END) END as name,                   
//                    typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
//                    creditOpenLine,
//                    DATEDIFF(d, dtOpenLine,@dateCombo) as days, debitOpenLine - creditOpenLine as dif , term,
//                    adc.idContPerson, adc.idClient,isFirstWarrningSent,dtFirstWarrning,isSecondWarrningSent,dtSecondWarrning,dtCreationLine,
//                    CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END as isInvoicing,
//                    adc.isDebitor, adc.isCreditor 
//                    FROM AccOpenLines aop
//                    LEFT JOIN AccDebCre adc ON aop.idDebCre = adc.accNumber
//                    LEFT JOIN Client c ON c.idClient = adc.idClient
//                    LEFT JOIN ContactPerson p ON p.idContPers = adc.idContPerson
//                    WHERE debitOpenLine - creditOpenLine != 0 AND isDebitor = '1' 
//                    ORDER BY days");
//            }
//            else if(deb_cre_all == 2)
//            {
//                query = string.Format(@"SELECT idOpenLine, idDebCre, 
//                    CASE WHEN adc.idContPerson = 0 OR adc.idContPerson is NULL then c.nameClient 
//                    ELSE (CASE WHEN p.firstname IS NULL THEN p.lastname 
//                    ELSE (CASE WHEN p.lastname IS NULL THEN '' ELSE p.firstname + ' ' + p.lastname END) END) END as name,                   
//                    typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
//                    creditOpenLine,
//                    DATEDIFF(d, dtOpenLine,@dateCombo) as days, debitOpenLine - creditOpenLine as dif , term,
//                    adc.idContPerson, adc.idClient,isFirstWarrningSent,dtFirstWarrning,isSecondWarrningSent,dtSecondWarrning,dtCreationLine,
//                    CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END as isInvoicing,
//                    adc.isDebitor, adc.isCreditor 
//                    FROM AccOpenLines aop
//                    LEFT JOIN AccDebCre adc ON aop.idDebCre = adc.accNumber
//                    LEFT JOIN Client c ON c.idClient = adc.idClient
//                    LEFT JOIN ContactPerson p ON p.idContPers = adc.idContPerson
//                    WHERE debitOpenLine - creditOpenLine != 0 AND isCreditor = '1' 
//                    ORDER BY days");
//            }
//            else
//            {
//                query = string.Format(@"SELECT idOpenLine, idDebCre, 
//                    CASE WHEN adc.idContPerson = 0 OR adc.idContPerson is NULL then c.nameClient 
//                    ELSE (CASE WHEN p.firstname IS NULL THEN p.lastname 
//                    ELSE (CASE WHEN p.lastname IS NULL THEN '' ELSE p.firstname + ' ' + p.lastname END) END) END as name,                   
//                    typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
//                    creditOpenLine,
//                    DATEDIFF(d, dtOpenLine,@dateCombo) as days, debitOpenLine - creditOpenLine as dif , term,
//                    adc.idContPerson, adc.idClient,isFirstWarrningSent,dtFirstWarrning,isSecondWarrningSent,dtSecondWarrning,dtCreationLine,
//                    CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END as isInvoicing,
//                    adc.isDebitor, adc.isCreditor 
//                    FROM AccOpenLines aop
//                    LEFT JOIN AccDebCre adc ON aop.idDebCre = adc.accNumber
//                    LEFT JOIN Client c ON c.idClient = adc.idClient
//                    LEFT JOIN ContactPerson p ON p.idContPers = adc.idContPerson
//                    WHERE debitOpenLine - creditOpenLine != 0
//                    ORDER BY days");
//            }

//            //WHERE idDebCre = @idDebCre  and
//            //codeCost, codeArr, idProject, idPayCondition, creditDays, discauntDays,
//                    //periodOnenLines,account,iselected,referencePay

//            SqlParameter[] sqlParameters = new SqlParameter[1];

//            //sqlParameters[0] = new SqlParameter("@idDebCre", SqlDbType.NVarChar);
//            //sqlParameters[0].Value = idDebCre;

//            sqlParameters[0] = new SqlParameter("@dateCombo", SqlDbType.DateTime);
//            sqlParameters[0].Value = dtCombo;

//            return conn.executeSelectQuery(query, sqlParameters);
//        }

        public DataTable GetAccOpenLineReportDateAndAccNumber(DateTime dtCombo, string accnumber)
        {
            string query = string.Format(@"SELECT idOpenLine, idDebCre, 
                    CASE WHEN adc.idContPerson = 0 OR adc.idContPerson is NULL then c.nameClient 
                    ELSE (CASE WHEN p.firstname IS NULL THEN p.lastname 
                    ELSE (CASE WHEN p.lastname IS NULL THEN '' ELSE p.firstname + ' ' + p.lastname END) END) END as name,                   
                    typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
                    creditOpenLine,
                    DATEDIFF(d, dtOpenLine,@dateCombo) as days, debitOpenLine - creditOpenLine as dif , term,
                    adc.idContPerson, adc.idClient,isFirstWarrningSent,dtFirstWarrning,isSecondWarrningSent,dtSecondWarrning,dtCreationLine,
                    CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END as isInvoicing,
                    adc.isDebitor, adc.isCreditor 
                    FROM AccOpenLines aop
                    LEFT JOIN AccDebCre adc ON aop.idDebCre = adc.accNumber
                    LEFT JOIN Client c ON c.idClient = adc.idClient
                    LEFT JOIN ContactPerson p ON p.idContPers = adc.idContPerson
                    WHERE debitOpenLine - creditOpenLine != 0 AND adc.accNumber = @accNumber 
                    ORDER BY days");

            //WHERE idDebCre = @idDebCre  and
            //codeCost, codeArr, idProject, idPayCondition, creditDays, discauntDays,
            //periodOnenLines,account,iselected,referencePay

            SqlParameter[] sqlParameters = new SqlParameter[2];

            //sqlParameters[0] = new SqlParameter("@idDebCre", SqlDbType.NVarChar);
            //sqlParameters[0].Value = idDebCre;

            sqlParameters[0] = new SqlParameter("@dateCombo", SqlDbType.DateTime);
            sqlParameters[0].Value = dtCombo;

            sqlParameters[1] = new SqlParameter("@accNumber", SqlDbType.NVarChar);
            sqlParameters[1].Value = accnumber;

            return conn.executeSelectQuery(query, sqlParameters);
        }


        public DataTable GetAccOpenLineReport_1stWarringn()
        {
            string query = string.Format(@"SELECT idOpenLine, idDebCre, 
                    CASE WHEN adc.idContPerson = 0 OR adc.idContPerson is NULL then c.nameClient 
                    ELSE (CASE WHEN p.firstname IS NULL THEN p.lastname 
                    ELSE (CASE WHEN p.lastname IS NULL THEN '' ELSE p.firstname + ' ' + p.lastname END) END) END as name,                   
                    typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
                    creditOpenLine,
                    DATEDIFF(d, dtOpenLine,GETDATE()) as days, debitOpenLine - creditOpenLine as dif , term,
                    adc.idContPerson, adc.idClient,isFirstWarrningSent,dtFirstWarrning,isSecondWarrningSent,dtSecondWarrning,dtCreationLine,
                    CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END as isInvoicing,
                    CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN '' ELSE (SELECT TOP 1 email FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') END as email 
                    FROM AccOpenLines aop
                    LEFT JOIN AccDebCre adc ON aop.idDebCre = adc.accNumber
                    LEFT JOIN Client c ON c.idClient = adc.idClient
                    LEFT JOIN ContactPerson p ON p.idContPers = adc.idContPerson
                    WHERE debitOpenLine - creditOpenLine != 0 AND dtOpenLine < GETDATE() and aop.isFirstWarrningSent = 'false'
                    AND aop.account = (SELECT defDebitorAccount FROM AccSettings WHERE DATEPART(yyyy,GETDATE()) = yearSettings)
                    ORDER BY days");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAccOpenLineReport_1stWarringn_ByLabel(int label)
        {
            string query = string.Format(@"SELECT DISTINCT idOpenLine, idDebCre, 
                    CASE WHEN adc.idContPerson = 0 OR adc.idContPerson is NULL then c.nameClient 
                    ELSE (CASE WHEN p.firstname IS NULL THEN p.lastname 
                    ELSE (CASE WHEN p.lastname IS NULL THEN '' ELSE p.firstname + ' ' + p.lastname END) END) END as name,                   
                    typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
                    creditOpenLine,
                    DATEDIFF(d, dtOpenLine,GETDATE()) as days, debitOpenLine - creditOpenLine as dif , term,
                    adc.idContPerson, adc.idClient,isFirstWarrningSent,dtFirstWarrning,isSecondWarrningSent,dtSecondWarrning,dtCreationLine,
                    CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END as isInvoicing,
                    CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN '' ELSE (SELECT TOP 1 email FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') END as email 
                    FROM AccOpenLines aop
                    LEFT JOIN AccDebCre adc ON aop.idDebCre = adc.accNumber
                    LEFT JOIN Client c ON c.idClient = adc.idClient
                    LEFT JOIN ContactPerson p ON p.idContPers = adc.idContPerson
					LEFT JOIN Invoice i ON i.invoiceNr+'-'+i.invoiceRbr = invoiceOpenLine
					left join arrangementbook ab on ab.idArrangementBook = i.idVoucher
					left join arrangement d on d.idArrangement = ab.idArrangement		
                    WHERE debitOpenLine - creditOpenLine != 0 AND dtOpenLine < GETDATE() and aop.isFirstWarrningSent = 'false'
                    AND aop.account = (SELECT defDebitorAccount FROM AccSettings WHERE DATEPART(yyyy,GETDATE()) = yearSettings) 
					AND ab.idArrangement IN (SELECT distinct idArrangement FROM ArrangementLabel WHERE idLabel = @idLabel)
                    ORDER BY days");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idLabel", SqlDbType.Int);
            sqlParameters[0].Value = label;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAccOpenLineReport_2ndWarringn()
        {
            string query = string.Format(@"SELECT idOpenLine, idDebCre, 
                    CASE WHEN adc.idContPerson = 0 OR adc.idContPerson is NULL then c.nameClient 
                    ELSE (CASE WHEN p.firstname IS NULL THEN p.lastname 
                    ELSE (CASE WHEN p.lastname IS NULL THEN '' ELSE p.firstname + ' ' + p.lastname END) END) END as name,                   
                    typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
                    creditOpenLine,
                    DATEDIFF(d, dtOpenLine,GETDATE()) as days, debitOpenLine - creditOpenLine as dif , term,
                    adc.idContPerson, adc.idClient,isFirstWarrningSent,dtFirstWarrning,isSecondWarrningSent,dtSecondWarrning,dtCreationLine,
                    CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END as isInvoicing,
                    CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN '' ELSE (SELECT TOP 1 email FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') END as email  
                    FROM AccOpenLines aop
                    LEFT JOIN AccDebCre adc ON aop.idDebCre = adc.accNumber
                    LEFT JOIN Client c ON c.idClient = adc.idClient
                    LEFT JOIN ContactPerson p ON p.idContPers = adc.idContPerson
                    WHERE debitOpenLine - creditOpenLine != 0 AND DATEADD(day,(SELECT noDayFrstWarning FROM AccSettings WHERE DATEPART(yyyy,GETDATE()) = yearSettings),dtFirstWarrning)<GETDATE() 
                    AND aop.account = (SELECT defDebitorAccount FROM AccSettings WHERE DATEPART(yyyy,GETDATE()) = yearSettings) AND aop.isSecondWarrningSent = 'false' AND aop.isFirstWarrningSent = 'true'
                    ORDER BY days");
           
            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAccOpenLineReport_2ndWarringn_ByLabel(int label)
        {
            string query = string.Format(@"SELECT DISTINCT idOpenLine, idDebCre, 
                    CASE WHEN adc.idContPerson = 0 OR adc.idContPerson is NULL then c.nameClient 
                    ELSE (CASE WHEN p.firstname IS NULL THEN p.lastname 
                    ELSE (CASE WHEN p.lastname IS NULL THEN '' ELSE p.firstname + ' ' + p.lastname END) END) END as name,                   
                    typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
                    creditOpenLine,
                    DATEDIFF(d, dtOpenLine,GETDATE()) as days, debitOpenLine - creditOpenLine as dif , term,
                    adc.idContPerson, adc.idClient,isFirstWarrningSent,dtFirstWarrning,isSecondWarrningSent,dtSecondWarrning,dtCreationLine,
                    CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END as isInvoicing,
                    CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN '' ELSE (SELECT TOP 1 email FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') END as email  
                    FROM AccOpenLines aop
                    LEFT JOIN AccDebCre adc ON aop.idDebCre = adc.accNumber
                    LEFT JOIN Client c ON c.idClient = adc.idClient
                    LEFT JOIN ContactPerson p ON p.idContPers = adc.idContPerson
					LEFT JOIN Invoice i ON i.invoiceNr+'-'+i.invoiceRbr = invoiceOpenLine
					left join arrangementbook ab on ab.idArrangementBook = i.idVoucher
					left join arrangement d on d.idArrangement = ab.idArrangement		
                    WHERE debitOpenLine - creditOpenLine != 0 AND DATEADD(day,(SELECT noDayFrstWarning FROM AccSettings WHERE DATEPART(yyyy,GETDATE()) = yearSettings),dtFirstWarrning)<GETDATE() 
                    AND aop.account = (SELECT defDebitorAccount FROM AccSettings WHERE DATEPART(yyyy,GETDATE()) = yearSettings) AND aop.isSecondWarrningSent = 'false' AND aop.isFirstWarrningSent = 'true' 
					AND ab.idArrangement IN (SELECT distinct idArrangement FROM ArrangementLabel WHERE idLabel = @idLabel)
                    ORDER BY days");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idLabel", SqlDbType.Int);
            sqlParameters[0].Value = label;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAccOpenLineReport_3rdWarringn()
        {
            string query = string.Format(@"SELECT idOpenLine, idDebCre, 
                    CASE WHEN adc.idContPerson = 0 OR adc.idContPerson is NULL then c.nameClient 
                    ELSE (CASE WHEN p.firstname IS NULL THEN p.lastname 
                    ELSE (CASE WHEN p.lastname IS NULL THEN '' ELSE p.firstname + ' ' + p.lastname END) END) END as name,                   
                    typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
                    creditOpenLine,
					DATEDIFF(d, dtOpenLine,GETDATE()) as days, debitOpenLine - creditOpenLine as dif , term,
                    adc.idContPerson, adc.idClient,isFirstWarrningSent,dtFirstWarrning,isSecondWarrningSent,dtSecondWarrning,dtCreationLine, 
                    CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END as isInvoicing,
                    CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN '' ELSE (SELECT TOP 1 email FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') END as email 
                    FROM AccOpenLines aop
                    LEFT JOIN AccDebCre adc ON aop.idDebCre = adc.accNumber
                    LEFT JOIN Client c ON c.idClient = adc.idClient
                    LEFT JOIN ContactPerson p ON p.idContPers = adc.idContPerson
                    WHERE debitOpenLine - creditOpenLine != 0 AND DATEADD(day,(SELECT noDaySecondWorning FROM AccSettings WHERE DATEPART(yyyy,GETDATE()) = yearSettings),dtSecondWarrning) < GETDATE() 
                    AND aop.account = (SELECT defDebitorAccount FROM AccSettings WHERE DATEPART(yyyy,GETDATE()) = yearSettings) AND aop.isSecondWarrningSent = 'true' AND aop.isFirstWarrningSent = 'true'
                    ORDER BY days");
         
            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAccOpenLineReport_3rdWarringn_ByLabel(int label)
        {
            string query = string.Format(@"SELECT DISTINCT idOpenLine, idDebCre, 
                    CASE WHEN adc.idContPerson = 0 OR adc.idContPerson is NULL then c.nameClient 
                    ELSE (CASE WHEN p.firstname IS NULL THEN p.lastname 
                    ELSE (CASE WHEN p.lastname IS NULL THEN '' ELSE p.firstname + ' ' + p.lastname END) END) END as name,                   
                    typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
                    creditOpenLine,
					DATEDIFF(d, dtOpenLine,GETDATE()) as days, debitOpenLine - creditOpenLine as dif , term,
                    adc.idContPerson, adc.idClient,isFirstWarrningSent,dtFirstWarrning,isSecondWarrningSent,dtSecondWarrning,dtCreationLine, 
                    CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN 'false' ELSE 'true' END as isInvoicing,
                    CASE WHEN (SELECT TOP 1 isInvoicing FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') IS NULL THEN '' ELSE (SELECT TOP 1 email FROM ContactPersonEmail cpe WHERE cpe.idContPers = p.idContPers AND isInvoicing = 'true') END as email 
                    FROM AccOpenLines aop
                    LEFT JOIN AccDebCre adc ON aop.idDebCre = adc.accNumber
                    LEFT JOIN Client c ON c.idClient = adc.idClient
                    LEFT JOIN ContactPerson p ON p.idContPers = adc.idContPerson
					LEFT JOIN Invoice i ON i.invoiceNr+'-'+i.invoiceRbr = invoiceOpenLine
					left join arrangementbook ab on ab.idArrangementBook = i.idVoucher
					left join arrangement d on d.idArrangement = ab.idArrangement	
                    WHERE debitOpenLine - creditOpenLine != 0 AND DATEADD(day,(SELECT noDaySecondWorning FROM AccSettings WHERE DATEPART(yyyy,GETDATE()) = yearSettings),dtSecondWarrning) < GETDATE() 
                    AND aop.account = (SELECT defDebitorAccount FROM AccSettings WHERE DATEPART(yyyy,GETDATE()) = yearSettings) AND aop.isSecondWarrningSent = 'true' AND aop.isFirstWarrningSent = 'true' 
					AND ab.idArrangement IN (SELECT distinct idArrangement FROM ArrangementLabel WHERE idLabel = @idLabel)
                    ORDER BY days");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idLabel", SqlDbType.Int);
            sqlParameters[0].Value = label;

            return conn.executeSelectQuery(query, sqlParameters);
        }


        public bool SaveAccOpenLinesReportSent_1stWarning(DataTable dt, int nrWarning, string nameForm, int idUser)
        {            
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            if (nrWarning == 1)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string query = string.Format(@"UPDATE AccOpenLines SET isFirstWarrningSent = @isFirstWarrningSent, dtFirstWarrning = @dtFirstWarrning WHERE idOpenLine = @idOpenLine ");

                    SqlParameter[] sqlParameter = new SqlParameter[3];

                    sqlParameter[0] = new SqlParameter("@isFirstWarrningSent", SqlDbType.Bit);
                    sqlParameter[0].Value = true;

                    sqlParameter[1] = new SqlParameter("@dtFirstWarrning", SqlDbType.DateTime);
                    sqlParameter[1].Value = DateTime.Now;

                    sqlParameter[2] = new SqlParameter("@idOpenLine", SqlDbType.Int);
                    sqlParameter[2].Value = Int32.Parse(dr["idOpenLine"].ToString());

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
                    sqlParameter[4].Value = Int32.Parse(dr["idOpenLine"].ToString());

                    sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                    sqlParameter[5].Value = "idOpenLine";

                    sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                    sqlParameter[6].Value = "AccOpenLines";

                    sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                    sqlParameter[7].Value = "Save acc open lines report sent_1stWarning";

                    _query.Add(query);
                    sqlParameters.Add(sqlParameter);
                }
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string query = string.Format(@"UPDATE AccOpenLines SET isSecondWarrningSent = @isSecondWarrningSent, dtSecondWarrning = @dtSecondWarrning WHERE idOpenLine = @idOpenLine ");

                    SqlParameter[] sqlParameter = new SqlParameter[3];

                    sqlParameter[0] = new SqlParameter("@isSecondWarrningSent", SqlDbType.Bit);
                    sqlParameter[0].Value = true;

                    sqlParameter[1] = new SqlParameter("@dtSecondWarrning", SqlDbType.DateTime);
                    sqlParameter[1].Value = DateTime.Now;

                    sqlParameter[2] = new SqlParameter("@idOpenLine", SqlDbType.Int);
                    sqlParameter[2].Value = Int32.Parse(dr["idOpenLine"].ToString());

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
                    sqlParameter[4].Value = Int32.Parse(dr["idOpenLine"].ToString());

                    sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                    sqlParameter[5].Value = "idOpenLine";

                    sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                    sqlParameter[6].Value = "AccOpenLines";

                    sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                    sqlParameter[7].Value = "Save acc open lines report sent_1stWarning";

                    _query.Add(query);
                    sqlParameters.Add(sqlParameter);
                }
            }
            
            return conn.executQueryTransaction(_query, sqlParameters);
        }


        public DataTable GetAccOpenLinesByInvoiceClient(string invoice, string client)
        {
            string query = string.Format(@"SELECT idOpenLine, idDebCre, typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
                                           creditOpenLine, codeCost, codeArr, idProject, idPayCondition, creditDays, discauntDays, periodOnenLines,account,iselected,referencePay,
                                            idOption, iban, term,idSepa,isFirstWarrningSent,dtFirstWarrning,isSecondWarrningSent,dtSecondWarrning,dtCreationLine
                                           FROM AccOpenLines
                                           WHERE invoiceOpenLine=@invoice and idDebCre=@client");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@invoice", SqlDbType.NVarChar);
            sqlParameters[0].Value = invoice;

            sqlParameters[1] = new SqlParameter("@client", SqlDbType.NVarChar);
            sqlParameters[1].Value = client;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAccOpenLinesByInvoice(string invoice,int term)
        {
            string query = string.Format(@"SELECT idOpenLine, idDebCre, typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
                                           creditOpenLine, codeCost, codeArr, idProject, idPayCondition, creditDays, discauntDays, periodOnenLines,account,iselected,referencePay,
                                            idOption, iban, term,idSepa,isFirstWarrningSent,dtFirstWarrning,isSecondWarrningSent,dtSecondWarrning,dtCreationLine
                                           FROM AccOpenLines
                                           WHERE invoiceOpenLine=@invoice and term=@term");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@invoice", SqlDbType.NVarChar);
            sqlParameters[0].Value = invoice;

            sqlParameters[1] = new SqlParameter("@term", SqlDbType.Int);
            sqlParameters[1].Value = term;

          

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAccOpenLinesByInvoiceNoTerm(string invoice)
        {
            string query = string.Format(@"SELECT idOpenLine, idDebCre, typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
                                           creditOpenLine, codeCost, codeArr, idProject, idPayCondition, creditDays, discauntDays, periodOnenLines,account,iselected,referencePay,
                                            idOption, iban, term,idSepa,isFirstWarrningSent,dtFirstWarrning,isSecondWarrningSent,dtSecondWarrning,dtCreationLine
                                           FROM AccOpenLines
                                           WHERE invoiceOpenLine=@invoice ");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@invoice", SqlDbType.NVarChar);
            sqlParameters[0].Value = invoice;

          
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public Boolean Save(AccOpenLinesModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO AccOpenLines 
                                           (idDebCre, typeOpenLine, invoiceOpenLine, dtOpenLine, dtPayOpenLine, descOpenLine, debitOpenLine,
                                           creditOpenLine, codeCost, codeArr, idProject, idPayCondition, creditDays, discauntDays, periodOnenLines,account,iselected,referencePay,idOption,iban,
                                            term, bookingYear,idSepa,isFirstWarrningSent,dtFirstWarrning,isSecondWarrningSent,dtSecondWarrning,dtCreationLine)
                                           VALUES (@idDebCre, @typeOpenLine, @invoiceOpenLine, @dtOpenLine, @dtPayOpenLine, @descOpenLine, @debitOpenLine,
                                           @creditOpenLine, @codeCost, @codeArr, @idProject, @idPayCondition, @creditDays, @discauntDays, @periodOnenLines,@account,@iselected,@referencePay,@idOption,@iban,
                                            @term,@bookingYear,@idSepa,@isFirstWarrningSent,@dtFirstWarrning,@isSecondWarrningSent,@dtSecondWarrning,@dtCreationLine ) ");

            SqlParameter[] sqlParameter = new SqlParameter[28];

            sqlParameter[0] = new SqlParameter("@idDebCre", SqlDbType.NVarChar);
            sqlParameter[0].Value = model.idDebCre;

            sqlParameter[1] = new SqlParameter("@typeOpenLine", SqlDbType.Char);
            sqlParameter[1].Value = model.typeOpenLine;

            sqlParameter[2] = new SqlParameter("@invoiceOpenLine", SqlDbType.NVarChar);
            sqlParameter[2].Value = model.invoiceOpenLine;

            sqlParameter[3] = new SqlParameter("@dtOpenLine", SqlDbType.DateTime);
            sqlParameter[3].Value = (model.dtOpenLine == null || model.dtOpenLine == DateTime.MinValue) ? SqlDateTime.Null : model.dtOpenLine;

            sqlParameter[4] = new SqlParameter("@dtPayOpenLine", SqlDbType.DateTime);
            sqlParameter[4].Value = (model.dtPayOpenLine == null || model.dtPayOpenLine == DateTime.MinValue) ? SqlDateTime.Null : model.dtPayOpenLine; 
     

            sqlParameter[5] = new SqlParameter("@descOpenLine", SqlDbType.NVarChar);
            sqlParameter[5].Value = model.descOpenLine;

            sqlParameter[6] = new SqlParameter("@debitOpenLine", SqlDbType.Decimal);
            sqlParameter[6].Value = model.debitOpenLine;

            sqlParameter[7] = new SqlParameter("@creditOpenLine", SqlDbType.Decimal);
            sqlParameter[7].Value = model.creditOpenLine;

            sqlParameter[8] = new SqlParameter("@codeCost", SqlDbType.NVarChar);
            sqlParameter[8].Value = model.codeCost;

            sqlParameter[9] = new SqlParameter("@codeArr", SqlDbType.NVarChar);
            sqlParameter[9].Value = model.codeArr;

            sqlParameter[10] = new SqlParameter("@idProject", SqlDbType.NVarChar);
            sqlParameter[10].Value = model.idProject;

            sqlParameter[11] = new SqlParameter("@idPayCondition", SqlDbType.Int);
            sqlParameter[11].Value = model.idPayCondition;

            sqlParameter[12] = new SqlParameter("@creditDays", SqlDbType.Int);
            sqlParameter[12].Value = model.creditDays;

            sqlParameter[13] = new SqlParameter("@discauntDays", SqlDbType.Int);
            sqlParameter[13].Value = model.discauntDays;

            sqlParameter[14] = new SqlParameter("@periodOnenLines", SqlDbType.Int);
            sqlParameter[14].Value = model.periodOnenLines;

            sqlParameter[15] = new SqlParameter("@account", SqlDbType.NVarChar);
            sqlParameter[15].Value = model.account;

            sqlParameter[16] = new SqlParameter("@iselected", SqlDbType.Bit);
            sqlParameter[16].Value = model.iselected;

            sqlParameter[17] = new SqlParameter("@referencePay", SqlDbType.NVarChar);
            sqlParameter[17].Value = model.referencePay;

            sqlParameter[18] = new SqlParameter("@idOption", SqlDbType.Int);
            sqlParameter[18].Value = model.idOption;

            sqlParameter[19] = new SqlParameter("@iban", SqlDbType.NVarChar);
            sqlParameter[19].Value = model.iban;

            sqlParameter[20] = new SqlParameter("@term", SqlDbType.Int);
            sqlParameter[20].Value = model.term;

            sqlParameter[21] = new SqlParameter("@bookingYear", SqlDbType.NVarChar);
            sqlParameter[21].Value = model.bookingYear;

            sqlParameter[22] = new SqlParameter("@idSepa", SqlDbType.Int);
            sqlParameter[22].Value = model.idSepa;

            sqlParameter[23] = new SqlParameter("@isFirstWarrningSent", SqlDbType.Bit);
            sqlParameter[23].Value = model.isFirstWarrningSent;

            sqlParameter[24] = new SqlParameter("@dtFirstWarrning", SqlDbType.DateTime);
            sqlParameter[24].Value = (model.dtFirstWarrning == null || model.dtFirstWarrning == DateTime.MinValue) ? SqlDateTime.Null : model.dtFirstWarrning;

            sqlParameter[25] = new SqlParameter("@isSecondWarrningSent", SqlDbType.Bit);
            sqlParameter[25].Value = model.isSecondWarrningSent;

            sqlParameter[26] = new SqlParameter("@dtSecondWarrning", SqlDbType.DateTime);
            sqlParameter[26].Value = (model.dtSecondWarrning == null || model.dtSecondWarrning == DateTime.MinValue) ? SqlDateTime.Null : model.dtSecondWarrning;

            sqlParameter[27] = new SqlParameter("@dtCreationLine", SqlDbType.DateTime);
            sqlParameter[27].Value = (model.dtCreationLine == null || model.dtCreationLine == DateTime.MinValue) ? SqlDateTime.Null : model.dtCreationLine;


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
            sqlParameter[4].Value = conn.GetLastTableID("AccOpenLines")+1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idOpenLine";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccOpenLines";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean Update(AccOpenLinesModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE AccOpenLines SET idDebCre=@idDebCre, typeOpenLine=@typeOpenLine,
                                                                    invoiceOpenLine=@invoiceOpenLine, dtOpenLine=@dtOpenLine, dtPayOpenLine=@dtPayOpenLine,
                                                                    descOpenLine=@descOpenLine, debitOpenLine=@debitOpenLine, creditOpenLine=@creditOpenLine,
                                                                    codeCost=@codeCost, codeArr=@codeArr, idProject=@idProject, 
                                                                    idPayCondition=@idPayCondition, creditDays=@creditDays, 
                                                                    discauntDays=@discauntDays, periodOnenLines=@periodOnenLines,account=@account,iselected=@iselected,referencePay=@referencePay,
                                                                    idOption=@idOption,iban=@iban, term=@term, bookingYear=@bookingYear,idSepa=@idSepa,
                                                                    isFirstWarrningSent=@isFirstWarrningSent,dtFirstWarrning=@dtFirstWarrning,
                                                                    isSecondWarrningSent=@isSecondWarrningSent,dtSecondWarrning=@dtSecondWarrning,dtCreationLine=@dtCreationLine
                                                                    WHERE idOpenLine=@idOpenLine ");

            SqlParameter[] sqlParameter = new SqlParameter[29];

            sqlParameter[0] = new SqlParameter("@idOpenLine", SqlDbType.Int);
            sqlParameter[0].Value = model.idOpenLine;

            sqlParameter[1] = new SqlParameter("@idDebCre", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.idDebCre;

            sqlParameter[2] = new SqlParameter("@typeOpenLine", SqlDbType.Char);
            sqlParameter[2].Value = model.typeOpenLine;

            sqlParameter[3] = new SqlParameter("@invoiceOpenLine", SqlDbType.NVarChar);
            sqlParameter[3].Value = model.invoiceOpenLine;

            sqlParameter[4] = new SqlParameter("@dtOpenLine", SqlDbType.DateTime);
            sqlParameter[4].Value = model.dtOpenLine;

            sqlParameter[5] = new SqlParameter("@dtPayOpenLine", SqlDbType.DateTime);
            sqlParameter[5].Value = model.dtPayOpenLine;

            sqlParameter[6] = new SqlParameter("@descOpenLine", SqlDbType.NVarChar);
            sqlParameter[6].Value = model.descOpenLine;

            sqlParameter[7] = new SqlParameter("@debitOpenLine", SqlDbType.Decimal);
            sqlParameter[7].Value = model.debitOpenLine;

            sqlParameter[8] = new SqlParameter("@creditOpenLine", SqlDbType.Decimal);
            sqlParameter[8].Value = model.creditOpenLine;

            sqlParameter[9] = new SqlParameter("@codeCost", SqlDbType.NVarChar);
            sqlParameter[9].Value = model.codeCost;

            sqlParameter[10] = new SqlParameter("@codeArr", SqlDbType.NVarChar);
            sqlParameter[10].Value = model.codeArr;

            sqlParameter[11] = new SqlParameter("@idProject", SqlDbType.NVarChar);
            sqlParameter[11].Value = model.idProject;

            sqlParameter[12] = new SqlParameter("@idPayCondition", SqlDbType.Int);
            sqlParameter[12].Value = model.idPayCondition;

            sqlParameter[13] = new SqlParameter("@creditDays", SqlDbType.Int);
            sqlParameter[13].Value = model.creditDays;

            sqlParameter[14] = new SqlParameter("@discauntDays", SqlDbType.Int);
            sqlParameter[14].Value = model.discauntDays;

            sqlParameter[15] = new SqlParameter("@periodOnenLines", SqlDbType.Int);
            sqlParameter[15].Value = model.periodOnenLines;

            sqlParameter[16] = new SqlParameter("@account", SqlDbType.NVarChar);
            sqlParameter[16].Value = model.account;

            sqlParameter[17] = new SqlParameter("@iselected", SqlDbType.Bit);
            sqlParameter[17].Value = model.iselected;

            sqlParameter[18] = new SqlParameter("@referencePay", SqlDbType.NVarChar);
            sqlParameter[18].Value = model.referencePay;

            sqlParameter[19] = new SqlParameter("@idOption", SqlDbType.Int);
            sqlParameter[19].Value = model.idOption;

            sqlParameter[20] = new SqlParameter("@iban", SqlDbType.NVarChar);
            sqlParameter[20].Value = model.iban;

            sqlParameter[21] = new SqlParameter("@term", SqlDbType.Int);
            sqlParameter[21].Value = model.term;

            sqlParameter[22] = new SqlParameter("@bookingYear", SqlDbType.NVarChar);
            sqlParameter[22].Value = model.bookingYear;

            sqlParameter[23] = new SqlParameter("@idSepa", SqlDbType.Int);
            sqlParameter[23].Value = model.idSepa;

            sqlParameter[24] = new SqlParameter("@isFirstWarrningSent", SqlDbType.Bit);
            sqlParameter[24].Value = model.isFirstWarrningSent;

            sqlParameter[25] = new SqlParameter("@dtFirstWarrning", SqlDbType.DateTime);
            sqlParameter[25].Value = (model.dtFirstWarrning == null || model.dtFirstWarrning == DateTime.MinValue) ? SqlDateTime.Null : model.dtFirstWarrning;

            sqlParameter[26] = new SqlParameter("@isSecondWarrningSent", SqlDbType.Bit);
            sqlParameter[26].Value = model.isSecondWarrningSent;

            sqlParameter[27] = new SqlParameter("@dtSecondWarrning", SqlDbType.DateTime);
            sqlParameter[27].Value = (model.dtSecondWarrning == null || model.dtSecondWarrning == DateTime.MinValue) ? SqlDateTime.Null : model.dtSecondWarrning;

            sqlParameter[28] = new SqlParameter("@dtCreationLine", SqlDbType.DateTime);
            sqlParameter[28].Value = (model.dtCreationLine == null || model.dtCreationLine == DateTime.MinValue) ? SqlDateTime.Null : model.dtCreationLine;

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
            sqlParameter[4].Value = model.idOpenLine;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idOpenLine";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccOpenLines";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean Delete(int idOpenLine, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM AccOpenLines
                                           WHERE idOpenLine=@idOpenLine ");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idOpenLine", SqlDbType.Int);
            sqlParameter[0].Value = idOpenLine;

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
            sqlParameter[4].Value = idOpenLine;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idOpenLine";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccOpenLines";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
        public Boolean DeleteByInvoice(string invoice, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM AccOpenLines
                                           WHERE invoiceOpenLine=@invoice ");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@invoice", SqlDbType.NVarChar);
            sqlParameter[0].Value = invoice;

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
            sqlParameter[4].Value = invoice;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "invoice";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccOpenLines";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete by invoice";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
    }
}