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
    public class TravelerPapersReportDAO
    {
        private dbConnection conn;

        public TravelerPapersReportDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetTravelerPapers(int idArrangement, int idContPers)
        {
            string query = string.Format(@"SELECT DISTINCT ab.idContPers,CASE WHEN cpr.initialsContPers IS NULL THEN '' ELSE cpr.initialsContPers END as initials, 
										CASE WHEN cpr.midname IS NOT NULL  THEN cpr.midname ELSE '' END AS midname
										,CASE WHEN t.nameTitle IS NULL OR t.idTitle = 0 THEN '' ELSE t.nameTitle END as title ,CASE WHEN (cpr.firstname IS NOT NULL) THEN cpr.firstname ELSE '' END + CASE WHEN (cpr.firstname IS NOT NULL AND cpr.firstname<> '') 
                                        THEN ' ' ELSE '' END  + CASE WHEN (cpr.midname IS NOT NULL)
                                         THEN cpr.midname ELSE '' END + CASE WHEN (cpr.midname IS NOT NULL AND cpr.midname<> '') 
                                         THEN ' ' ELSE '' END  + CASE WHEN (cpr.lastname IS NOT NULL) THEN cpr.lastname ELSE '' END as name
                                        ,CASE WHEN (t.nameTitle IS NOT NULL) THEN t.nameTitle ELSE '' END + CASE WHEN (t.nameTitle IS NOT NULL AND t.nameTitle<> '') 
                                        THEN ' ' ELSE '' END  + CASE WHEN (cpr.initialsContPers IS NOT NULL)
                                         THEN cpr.initialsContPers ELSE '' END + CASE WHEN (cpr.initialsContPers IS NOT NULL AND cpr.initialsContPers<> '') 
                                         THEN ' ' ELSE '' END  + CASE WHEN (cpr.midname IS NOT NULL) THEN cpr.midname ELSE '' END + CASE WHEN (cpr.midname IS NOT NULL AND cpr.midname<> '') 
                                        THEN ' ' ELSE '' END  + CASE WHEN (cpr.lastname IS NOT NULL)
                                         THEN cpr.lastname ELSE '' END as lastMiddInitTitle
                                        ,lastname,street + CASE WHEN (street IS NOT NULL AND street<> '')  
                                        THEN ' ' ELSE '' END + housenr + CASE WHEN (housenr is not null and housenr<>'' and extension is not null and extension<>'') then '-' else '' end + CASE WHEN (extension IS NOT NULL)
                                         THEN extension ELSE '' END as address, postalCode + CASE WHEN (postalCode IS NOT NULL AND postalCode<> '') 
                                         THEN ' ' ELSE '' END + city as city,CASE WHEN vfc.idContPers IS NULL THEN (CASE WHEN vfc2.idContPers  IS NULL THEN '' ELSE 'coördinator' END) ELSE (CASE WHEN vfc2.idContPers IS NULL THEN 'begeleider ' ELSE 'begeleider ,coördinator' END ) END as [Function],
                                        a.nameArrangement,a.dtFromArrangement,cpr.firstname,
                                        bp.nameBoardingPoint as nameBP,bp.addressBoardingPoint AS adressBP, CONVERT(DATE,abp.departure) as departureBP, CONVERT(DATE,abp.arrivel) as arrivelBP,CONVERT(TIME,abp.departure) as departureTBP, CONVERT(TIME,abp.arrivel) as arrivelTBP
                                        ,aba.idContPers,aba.name as namePasengers,aba.departure, aba.arrivel,CASE WHEN aba.sortBoardingPoint IS NOT NULL THEN 'Route ' + CONVERT (nvarchar,aba.sortBoardingPoint) else 'No route' end as sort,CONVERT(nvarchar,aba.sortBoardingPoint) as nr,aba.nameBoardingPoint, aba.Type
                                        ,Convert(NVARCHAR,mcp.txt) as medical
                                        FROM ArrangementBook ab
                                        LEFT JOIN ContactPerson cpr ON cpr.idContPers = ab.idContPers 
                                        LEFT JOIN ContactPersonAddress cpra ON cpra.idContPers = cpr.idContPers 
                                        LEFT JOIN Arrangement a ON a.idArrangement = ab.idArrangement
                                        LEFT JOIN Title t ON t.idTitle = cpr.idTitle                                       
                                                                               
                                        LEFT JOIN (SELECT ab.idArrangement,ab.idContPers,CASE WHEN (cpr.firstname IS NOT NULL) THEN cpr.firstname ELSE '' END + CASE WHEN (cpr.firstname IS NOT NULL AND cpr.firstname<> '') 
                                        THEN ' ' ELSE '' END  + CASE WHEN (cpr.midname IS NOT NULL) THEN cpr.midname ELSE '' END + CASE WHEN (cpr.midname IS NOT NULL AND cpr.midname<> '') 
                                         THEN ' ' ELSE '' END  + CASE WHEN (cpr.lastname IS NOT NULL) THEN cpr.lastname ELSE '' END as name
                                        ,abp.departure, abp.arrivel,abp.sortBoardingPoint,bp.nameBoardingPoint,  CASE WHEN (SELECT idFilter FROM ContactPersonFilter WHERE idContPers = ab.idContPers AND idFilter='4') = '4'  THEN 
                                        (CASE WHEN (SELECT count(*) FROM  ArrangementBook
          WHERE idArrangement = @idArrangement AND idBoarding = abp.idBoardingPoint AND idStatus = '2' and idContPers NOT IN (SELECT idContPers FROM ContactPersonFilter WHERE idFilter='4')) = 0 THEN 'Opstapplaats:             Begeleiding              ' ELSE 'Begeleiding' END) ELSE 'Opstapplaats:' END as Type
                                        FROM ArrangementBook ab 
                                        LEFT JOIN ContactPerson cpr ON cpr.idContPers = ab.idContPers
                                        LEFT JOIN ArrangementBoardingPoint abp ON abp.idBoardingPoint = ab.idBoarding AND abp.idArrangement = ab.idArrangement
                                        LEFT JOIN BoardingPoint bp ON abp.idBoardingPoint = bp.idBoardingPoint
                                        
                                   
                                        WHERE ab.idArrangement = @idArrangement AND ab.idStatus = '2') aba ON aba.idArrangement = ab.idArrangement
                                        LEFT OUTER JOIN ArrangementBoardingPoint abp ON abp.idBoardingPoint = ab.idBoarding AND abp.idArrangement = ab.idArrangement
                                        LEFT  JOIN BoardingPoint bp ON abp.idBoardingPoint = bp.idBoardingPoint

                                        LEFT  JOIN VolLookup vfc ON vfc.idContPers=aba.idContPers AND vfc.id='2'and vfc.idArrangement=aba.idArrangement and vfc.type='F' and aba.idContPers  IN (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4')
                                        LEFT  JOIN VolLookup vfc2 ON vfc2.idContPers=aba.idContPers AND vfc2.id='5' and vfc2.idArrangement=aba.idArrangement and vfc2.type='F' and aba.idContPers  IN (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4')
                                             LEFT JOIN (SELECT mca.txtAns as txt , mcp.idcpr
                                        FROM
                                        (SELECT MAX(mcp.idAns) as idAns, mcp.idcpr
                                        FROM ContactPerson cpr
                                        LEFT JOIN MedCpr mcp ON mcp.idcpr = cpr.idContPers  
                                        WHERE  mcp.idQuest ='310' and cpr.idContPers NOT IN (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4')
                                        GROUP BY mcp.idcpr) mcp
                                        LEFT JOIN MedAns mca ON mcp.idAns = mca.idAns) mcp ON mcp.idcpr = aba.idContPers
                                         
                                        WHERE ab.idContPers = @idContPers and ab.idArrangement = @idArrangement AND cpra.idAddressType = '1' AND ab.idStatus = '2'
                                        
                                        ORDER by sort desc, nr ,aba.type
                                        
                                       ");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            sqlParameters[1] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[1].Value = idContPers;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetPapers(int idArrangement)
        {
            string query = string.Format(@"SELECT  dd.idArrangement,dd.dtFrom ,dd.nameClient, dd.dtTo, dd.postalcode, dd.city2, dd.Address2, 
            dd.TelephoneNumber, dd.TelephoneFax, dd.Email, dd.Country, dd.isAway, dd.isBack, dd.isAccomodation
            FROM
           (SELECT DISTINCT ac.dtFrom,c.nameClient, ac.dtTo, 
            CASE WHEN  (ca.postalCode = '' OR ca.postalCode  IS NULL) THEN '' ELSE ca.postalCode end AS postalcode,
            CASE WHEN  (ca.city = '' OR ca.city  IS NULL) THEN '' ELSE ca.city end AS city2 ,CASE WHEN (ca.street='' OR ca.street IS NULL) 
            THEN (CASE WHEN housenr IS NULL THEN '' ELSE housenr END) ELSE ca.street + ' ' + (CASE WHEN housenr IS NULL THEN '' ELSE housenr END) END +        
            CASE WHEN (ca.street='' or ca.street is null) AND (ca.housenr = '' or ca.housenr is null) THEN ''
            ELSE (CASE WHEN ca.extension is null or ca.extension='' THEN '' else '-' END) END + CASE WHEN ca.extension is null or ca.extension='' THEN '' else ca.extension END AS Address2,
            CASE WHEN  (cpt.numberTel = '' OR cpt.numberTel  IS NULL) THEN '' ELSE cpt.numberTel END AS TelephoneNumber,
            CASE WHEN  (cpt2.numberTel = '' OR cpt2.numberTel  IS NULL) THEN '' ELSE cpt2.numberTel END AS TelephoneFax,
            CASE WHEN  (ce.email = '' OR ce.email  IS NULL) THEN '' ELSE ce.email END AS Email,
            CASE WHEN  (co.nameCountry = '' OR co.nameCountry  IS NULL) THEN '' ELSE co.nameCountry END AS Country,
            ac.isAway, ac.isBack, ac.isAccomodation,ac.idArrangement
 
             FROM (SELECT idArrangementPrice ,idArrangement,idArticle,a.nameArtical as nameArticle,nrArticle,a.quantity,'' as idContract,'No contract' as nameContract ,ap.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,ap.isExtra,ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler,  ap.idUserCreated, ap.dtUserCreated,ap.idUserModified,ap.dtUserModified
                                           FROM ArrangementPrice ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           WHERE idArrangement = @idArrangement
                                      UNION     
                                           SELECT idPriceListArticle as idArrangementPrice,pl.idArrangement,pla.idArticle,a.nameArtical as nameArticle,pla.nrArticle,a.quantity,pl.idPriceList as idContract,'Contract' as nameContract,pl.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,pla.isExtra,pla.pricePerArticle,pla.pricePerQuantity,pla.priceTotal,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, pl.idUserCreated, pl.dtUserCreated,pl.idUserModified,pl.dtUserModified
                                           FROM PriceList pl
                                           INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                                           LEFT OUTER JOIN  CLient c ON c.idClient = pl.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                                           WHERE pl.idArrangement = @idArrangement)  ac
             LEFT OUTER JOIN Client  c ON ac.idClient=c.idClient
             LEFT OUTER JOIN (SELECT  street,housenr,postalcode,idClient,city,country,extension  FROM ClientAddress WHERE idAddressType='1') ca ON ac.idClient=ca.idClient
             LEFT OUTER JOIN Country co ON co.internationalCode=ca.country
             LEFT OUTER JOIN ClientTel cpt ON c.idClient = cpt.idClient AND cpt.idDefaultTel='1'
             LEFT OUTER JOIN ClientEmail ce ON c.idClient = ce.idClient AND ce.isCommunication='1' 
             LEFT OUTER JOIN ClientTel cpt2 ON c.idClient = cpt2.idClient AND cpt2.descriptionTel='fax'
              WHERE  ac.idArrangement=@idArrangement AND (ac.isAway='1' OR ac.isBack='1' OR ac.isAccomodation='1' ) ) dd
                            
                              ORDER BY  dtFrom DESC
							");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;


            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetTekst(int idArrangement)
        {
            string query = string.Format(@"SELECT letterImage as letter,program,rulesAppointment
                                        FROM ArrangementRemaining ar
                                        WHERE ar.idArrangement=@idArrangement");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAllTravelerPapers()
        {
            string query = string.Format(@"SELECT nameTravelPapers 
                                           FROM TravelPapers 
                                           ORDER BY nameTravelPapers");
            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetArrangementRemaining(int idArrangement)
        {
            string query = string.Format(@"SELECT awayDt,awayDt2,awayAirport,awayAirport2,awayFlightNr,awayFlightNr2,arrivalDt,arrivalDt2,arrivalAirport,arrivalAirport2,backDt,backDt2,backAirport,backAirport2,
                                          backFlightNr,backFlightNr2,arrivalDt3,arrivalDt4,arrivalAirport3,arrivalAirport4,collectTime,airportSociety,special,twoFlight
                                          FROM ArrangementRemaining ar  
                                          WHERE idArrangement=@idArrangement");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;



            return conn.executeSelectQuery(query, sqlParameters);
        }
    }
}