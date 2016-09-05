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
    public class ArrangementPriceDAO
    {
        private dbConnection conn;

        public ArrangementPriceDAO()
        {
            conn = new dbConnection();
        }
        public DataTable GetAllArrangementPrices(int idArrangment, Boolean isFirst)
        {
            string query = string.Format(@"SELECT idArrangementPrice ,idArrangement,idArticle,a.nameArtical as nameArticle,nrArticle,a.quantity,'' as idContract,'No contract' as nameContract ,ap.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,ap.isExtra,ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler,  ap.idUserCreated, ap.dtUserCreated,ap.idUserModified,ap.dtUserModified
                                           FROM ArrangementPrice ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           WHERE idArrangement = '" + idArrangment + @"'
                                      UNION     
                                           SELECT idPriceListArticle as idArrangementPrice,pl.idArrangement,pla.idArticle,a.nameArtical as nameArticle,pla.nrArticle,a.quantity,pl.idPriceList as idContract,'Contract' as nameContract,pl.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,pla.isExtra,pla.pricePerArticle,pla.pricePerQuantity,pla.priceTotal,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, pl.idUserCreated, pl.dtUserCreated,pl.idUserModified,pl.dtUserModified
                                           FROM PriceList pl
                                           INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                                           LEFT OUTER JOIN  CLient c ON c.idClient = pl.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                                           WHERE pl.idArrangement = '" + idArrangment + @"'");
            if (isFirst == true)
            {
                query = string.Format(@"SELECT idArrangementPrice ,idArrangement,idArticle,a.nameArtical as nameArticle,nrArticle,a.quantity,CASE WHEN (ap.idContract  IS NULL OR ap.idContract=0) then '' ELSE ap.idContract END  as idContract ,CASE WHEN (ap.idContract  IS NULL OR ap.idContract=0) then 'No contract' ELSE 'Contract' END as nameContract ,ap.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,ap.isExtra,ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, ap.idUserCreated, ap.dtUserCreated,ap.idUserModified,ap.dtUserModified
                                           FROM ArrangementCalculationFirstArticles ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           WHERE ap.idArrangement = '" + idArrangment + @"'");
            }
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = (idArrangment == null) ? SqlInt32.Null : idArrangment;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAllArrangementPricesByArticleGroup(int idArrangment, string articleGroup, int MinNumTravelers,  Boolean isFirst)
        {
            string query = string.Format(@"SELECT idArrangementPrice ,idArrangement,idArticle,ap.isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, a.nameArtical as nameArticle,nrArticle,a.quantity,'' as idContract,'No contract' as nameContract ,ap.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal, CASE WHEN a.isGroup=1 THEN CAST((ap.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST(ap.priceTotal as numeric(18,2)) END as Total,ap.idUserCreated, ap.dtUserCreated,ap.idUserModified,ap.dtUserModified
                                           FROM ArrangementPrice ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           WHERE idArrangement = '" + idArrangment + @"' AND ap.isExtra=0 AND ag.classArticalGroup = '" + articleGroup + @"' 
                                      UNION     
                                           SELECT idPriceListArticle as idArrangementPrice,pl.idArrangement,pla.idArticle,pla.isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, a.nameArtical as nameArticle,pla.nrArticle,a.quantity,pl.idPriceList as idContract,'Contract' as nameContract,pl.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,pla.pricePerArticle,pla.pricePerQuantity,pla.priceTotal, CASE WHEN a.isGroup=1 THEN CAST(( pla.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST( pla.priceTotal as numeric(18,2)) END as Total, pl.idUserCreated, pl.dtUserCreated,pl.idUserModified,pl.dtUserModified
                                           FROM PriceList pl
                                           INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                                           LEFT OUTER JOIN  CLient c ON c.idClient = pl.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           WHERE pl.idArrangement = '" + idArrangment + @"' AND pla.isExtra=0 AND ag.classArticalGroup = '" + articleGroup + @"'");
            if (isFirst == true)
            {
                query = string.Format(@"SELECT idArrangementPrice ,idArrangement,idArticle,a.nameArtical as nameArticle,nrArticle,a.quantity,CASE WHEN (ap.idContract  IS NULL OR ap.idContract=0) then '' ELSE ap.idContract END  as idContract ,CASE WHEN (ap.idContract  IS NULL OR ap.idContract=0) then 'No contract' ELSE 'Contract' END as nameContract ,ap.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,ap.isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal, CASE WHEN a.isGroup=1 THEN CAST((ap.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST(ap.priceTotal as numeric(18,2)) END as Total, ap.idUserCreated, ap.dtUserCreated,ap.idUserModified,ap.dtUserModified
                                           FROM ArrangementCalculationFirstArticles ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                            WHERE ap.idArrangement = '" + idArrangment + @"' AND ap.isExtra=0 AND ag.classArticalGroup = '" + articleGroup + @"'");
            }
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = (idArrangment == null) ? SqlInt32.Null : idArrangment;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        // za male gridove na Aranzman kalkulaciji
        public DataTable GetAllArrangementPricesByArticleGroupWithExtra(int idArrangment, string articleGroup, int MinNumTravelers,Boolean isFirst)  //a.quantity,'' as idContract,'No contract' as nameContract ,ap.idClient,c.nameClient,
        // a.quantity,pl.idPriceList as idContract,'Contract' as nameContract,pl.idClient,c.nameClient,
        {

            string query = string.Format(@"SELECT idArrangementPrice ,idArrangement,idArticle,ap.isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, a.nameArtical as nameArticle,nrArticle,a.quantity,'' as idContract,'No contract' as nameContract ,ap.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal, CASE WHEN a.isGroup=1 THEN CAST((ap.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST(ap.priceTotal as numeric(18,2)) END as Total,ap.idUserCreated, ap.dtUserCreated,ap.idUserModified,ap.dtUserModified
                                           FROM ArrangementPrice ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           WHERE idArrangement = '" + idArrangment + @"' AND ag.classArticalGroup = '" + articleGroup + @"'
                                      UNION     
                                           SELECT idPriceListArticle as idArrangementPrice,pl.idArrangement,pla.idArticle,pla.isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, a.nameArtical as nameArticle,pla.nrArticle,a.quantity,pl.idPriceList as idContract,'Contract' as nameContract,pl.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,pla.pricePerArticle,pla.pricePerQuantity,pla.priceTotal, CASE WHEN a.isGroup=1 THEN CAST(( pla.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST( pla.priceTotal as numeric(18,2)) END as Total, pl.idUserCreated, pl.dtUserCreated,pl.idUserModified,pl.dtUserModified
                                           FROM PriceList pl
                                           INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                                           LEFT OUTER JOIN  CLient c ON c.idClient = pl.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           WHERE pl.idArrangement = '" + idArrangment + @"' AND ag.classArticalGroup = '" + articleGroup + @"'");
            if (isFirst == true)
            {
                query = string.Format(@"SELECT idArrangementPrice ,idArrangement,idArticle,a.nameArtical as nameArticle,nrArticle,a.quantity,CASE WHEN (ap.idContract  IS NULL OR ap.idContract=0) then '' ELSE ap.idContract END  as idContract ,CASE WHEN (ap.idContract  IS NULL OR ap.idContract=0) then 'No contract' ELSE 'Contract' END as nameContract ,ap.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,ap.isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal, CASE WHEN a.isGroup=1 THEN CAST((ap.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST(ap.priceTotal as numeric(18,2)) END as Total, ap.idUserCreated, ap.dtUserCreated,ap.idUserModified,ap.dtUserModified
                                           FROM ArrangementCalculationFirstArticles ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           WHERE ap.idArrangement = '" + idArrangment + @"' AND ag.classArticalGroup = '" + articleGroup + "'");
            }
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = (idArrangment == null) ? SqlInt32.Null : idArrangment;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetAllTotalWithExtra(int idArrangment, int MinNumTravelers, Boolean isFirst, Boolean isTotalZero)  //a.quantity,'' as idContract,'No contract' as nameContract ,ap.idClient,c.nameClient,
        {
            string query = string.Format(@"SELECT idArrangementPrice ,idArrangement,idArticle,ap.isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, a.nameArtical as nameArticle,nrArticle,a.quantity,'' as idContract,'No contract' as nameContract ,ap.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal, CASE WHEN a.isGroup=1 THEN CAST((ap.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST(ap.priceTotal as numeric(18,2)) END as Total,ap.idUserCreated, ap.dtUserCreated,ap.idUserModified,ap.dtUserModified
                                           FROM ArrangementPrice ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           WHERE idArrangement = '" + idArrangment + @"' AND ap.isExtra = '1'
                                           UNION
                                           SELECT idPriceListArticle as idArrangementPrice,pl.idArrangement,pla.idArticle,pla.isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, a.nameArtical as nameArticle,pla.nrArticle,a.quantity,pl.idPriceList as idContract,'Contract' as nameContract,pl.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,pla.pricePerArticle,pla.pricePerQuantity,pla.priceTotal, CASE WHEN a.isGroup=1 THEN CAST(( pla.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST( pla.priceTotal as numeric(18,2)) END as Total, pl.idUserCreated, pl.dtUserCreated,pl.idUserModified,pl.dtUserModified
                                           FROM PriceList pl
                                           INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                                           LEFT OUTER JOIN  CLient c ON c.idClient = pl.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           WHERE pl.idArrangement = '" + idArrangment + @"' AND pla.isExtra=1 ");
            if(isTotalZero==true)
            {
                query = string.Format(@"SELECT idArrangementPrice ,idArrangement,idArticle,ap.isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, a.nameArtical as nameArticle,nrArticle,a.quantity,'' as idContract,'No contract' as nameContract ,ap.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal, CASE WHEN a.isGroup=1 THEN CAST((ap.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST(ap.priceTotal as numeric(18,2)) END as Total,ap.idUserCreated, ap.dtUserCreated,ap.idUserModified,ap.dtUserModified
                                           FROM ArrangementPrice ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           WHERE idArrangement = '" + idArrangment + @"' AND ap.isExtra = '1' AND ap.priceTotal >0
                                           UNION
                                           SELECT idPriceListArticle as idArrangementPrice,pl.idArrangement,pla.idArticle,pla.isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, a.nameArtical as nameArticle,pla.nrArticle,a.quantity,pl.idPriceList as idContract,'Contract' as nameContract,pl.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,pla.pricePerArticle,pla.pricePerQuantity,pla.priceTotal, CASE WHEN a.isGroup=1 THEN CAST(( pla.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST( pla.priceTotal as numeric(18,2)) END as Total, pl.idUserCreated, pl.dtUserCreated,pl.idUserModified,pl.dtUserModified
                                           FROM PriceList pl
                                           INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                                           LEFT OUTER JOIN  CLient c ON c.idClient = pl.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           WHERE pl.idArrangement = '" + idArrangment + @"' AND pla.isExtra=1 AND pla.priceTotal >0");
            }
            if (isFirst == true)
            {
                query = string.Format(@"SELECT idArrangementPrice ,idArrangement,idArticle,a.nameArtical as nameArticle,nrArticle,a.quantity,CASE WHEN (ap.idContract  IS NULL OR ap.idContract=0) then '' ELSE ap.idContract END  as idContract ,CASE WHEN (ap.idContract  IS NULL OR ap.idContract=0) then 'No contract' ELSE 'Contract' END as nameContract ,ap.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,ap.isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal, CASE WHEN a.isGroup=1 THEN CAST((ap.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST(ap.priceTotal as numeric(18,2)) END as Total, ap.idUserCreated, ap.dtUserCreated,ap.idUserModified,ap.dtUserModified
                                           FROM ArrangementCalculationFirstArticles ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           WHERE ap.idArrangement = '" + idArrangment + @"' AND ap.isExtra=1 ");
                if (isTotalZero == true)
                {
                    query = string.Format(@"SELECT idArrangementPrice ,idArrangement,idArticle,a.nameArtical as nameArticle,nrArticle,a.quantity,CASE WHEN (ap.idContract  IS NULL OR ap.idContract=0) then '' ELSE ap.idContract END  as idContract ,CASE WHEN (ap.idContract  IS NULL OR ap.idContract=0) then 'No contract' ELSE 'Contract' END as nameContract ,ap.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,ap.isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal, CASE WHEN a.isGroup=1 THEN CAST((ap.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST(ap.priceTotal as numeric(18,2)) END as Total, ap.idUserCreated, ap.dtUserCreated,ap.idUserModified,ap.dtUserModified
                                           FROM ArrangementCalculationFirstArticles ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           WHERE ap.idArrangement = '" + idArrangment + @"' AND ap.isExtra=1 AND ap.priceTotal >0");
                }
            }
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = (idArrangment == null) ? SqlInt32.Null : idArrangment;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAllTotalWithExtraInvoice(int idArrangment, int MinNumTravelers,  Boolean isTotalZero)  //a.quantity,'' as idContract,'No contract' as nameContract ,ap.idClient,c.nameClient,
        {
            string query = string.Format(@"SELECT idArrangementPrice ,ap.idArrangement,ap.idArticle,ap.isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, a.nameArtical as nameArticle,ap.nrArticle,
                                           a.quantity,'' as idContract,'No contract' as nameContract ,ap.idClient,c.nameClient,dtFrom,dtTo,
                                           a.isGroup,CASE WHEN aip.sellingPrice IS NULL THEN ap.pricePerArticle ELSE aip.sellingPrice END AS pricePerArticle,ap.pricePerQuantity,ap.priceTotal, 
                                           CASE WHEN a.isGroup=1 THEN CAST((ap.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST(ap.priceTotal as numeric(18,2)) END as Total,
                                           ap.idUserCreated, ap.dtUserCreated,ap.idUserModified,ap.dtUserModified
                                           FROM ArrangementPrice ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           LEFT OUTER JOIN ArrangementInvoicePrice aip ON aip.idArticle = ap.idArticle AND aip.idArrangement ='" + idArrangment + @"'
                                           WHERE ap.idArrangement = '" + idArrangment + @"' AND ap.isExtra = '1'
                                           UNION
                                           SELECT idPriceListArticle as idArrangementPrice,pl.idArrangement,pla.idArticle,pla.isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment,
                                            isNotForTraveler, a.nameArtical as nameArticle,pla.nrArticle,a.quantity,pl.idPriceList as idContract,'Contract' as nameContract,pl.idClient,c.nameClient,dtFrom,dtTo,
                                            a.isGroup,CASE WHEN aip.sellingPrice IS NULL THEN pla.pricePerArticle ELSE aip.sellingPrice END AS pricePerArticle,pla.pricePerQuantity,pla.priceTotal, 
                                            CASE WHEN a.isGroup=1 THEN CAST(( pla.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST( pla.priceTotal as numeric(18,2)) END as Total,
                                            pl.idUserCreated, pl.dtUserCreated,pl.idUserModified,pl.dtUserModified
                                           FROM PriceList pl
                                           INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                                           LEFT OUTER JOIN  CLient c ON c.idClient = pl.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           LEFT OUTER JOIN ArrangementInvoicePrice aip ON aip.idArticle = pla.idArticle AND aip.idArrangement ='" + idArrangment + @"'
                                           WHERE pl.idArrangement = '" + idArrangment + @"' AND pla.isExtra=1 ");
            if (isTotalZero == true)
            {
                query = string.Format(@"SELECT idArrangementPrice ,ap.idArrangement,ap.idArticle,ap.isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, 
                                            isNotForTraveler, a.nameArtical as nameArticle,ap.nrArticle,a.quantity,'' as idContract,'No contract' as nameContract ,ap.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,
                                            CASE WHEN aip.sellingPrice IS NULL THEN ap.pricePerArticle ELSE aip.sellingPrice END AS pricePerArticle,ap.pricePerQuantity,ap.priceTotal, 
                                            CASE WHEN a.isGroup=1 THEN CAST((ap.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST(ap.priceTotal as numeric(18,2)) END as Total,
                                            ap.idUserCreated, ap.dtUserCreated,ap.idUserModified,ap.dtUserModified
                                           FROM ArrangementPrice ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           LEFT OUTER JOIN ArrangementInvoicePrice aip ON aip.idArticle = ap.idArticle AND aip.idArrangement ='" + idArrangment + @"'
                                           WHERE ap.idArrangement = '" + idArrangment + @"' AND ap.isExtra = '1' AND ap.priceTotal <>0
                                           UNION
                                           SELECT idPriceListArticle as idArrangementPrice,pl.idArrangement,pla.idArticle,pla.isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, 
                                            isNotForTraveler, a.nameArtical as nameArticle,pla.nrArticle,a.quantity,pl.idPriceList as idContract,'Contract' as nameContract,pl.idClient,c.nameClient,
                                            dtFrom,dtTo,a.isGroup,CASE WHEN aip.sellingPrice IS NULL THEN pla.pricePerArticle ELSE aip.sellingPrice END AS pricePerArticle,pla.pricePerQuantity,pla.priceTotal, 
                                            CASE WHEN a.isGroup=1 THEN CAST(( pla.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST( pla.priceTotal as numeric(18,2)) END as Total, 
                                            pl.idUserCreated, pl.dtUserCreated,pl.idUserModified,pl.dtUserModified
                                           FROM PriceList pl
                                           INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                                           LEFT OUTER JOIN  CLient c ON c.idClient = pl.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           LEFT OUTER JOIN ArrangementInvoicePrice aip ON aip.idArticle = pla.idArticle AND aip.idArrangement ='" + idArrangment + @"'
                                           WHERE pl.idArrangement = '" + idArrangment + @"' AND pla.isExtra=1 AND pla.priceTotal <>0");
            }
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = (idArrangment == null) ? SqlInt32.Null : idArrangment;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetExtraAccomodation(int idArrangment, List<int> idArrangementBookList, int MinNumTravelers)  //a.quantity,'' as idContract,'No contract' as nameContract ,ap.idClient,c.nameClient,
        {

            string condition = "";
            if (idArrangementBookList != null)
                if (idArrangementBookList.Count > 0)
                {
                    for (int i = 0; i < idArrangementBookList.Count; i++)
                    {
                        if (i == idArrangementBookList.Count - 1)
                            condition = " WHERE (" + condition + " idArrangementBook = '" + idArrangementBookList[i] + "' )";
                        else
                            condition = condition + " idArrangementBook = '" + idArrangementBookList[i] + "' OR";
                    }
                }
            string query = string.Format(@"SELECT idArrangementPrice ,ap.idArrangement,ap.idArticle,ap.isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, a.nameArtical as nameArticle,
                                           ap.nrArticle,a.quantity,'' as idContract,'No contract' as nameContract ,ap.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,
                                           CASE WHEN aip.sellingPrice IS NULL THEN ap.pricePerArticle ELSE aip.sellingPrice END AS pricePerArticle,ap.pricePerQuantity,ap.priceTotal, CASE WHEN a.isGroup=1 THEN CAST((ap.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST(ap.priceTotal as numeric(18,2)) END as Total,ap.idUserCreated, ap.dtUserCreated,ap.idUserModified,ap.dtUserModified
                                           FROM ArrangementPrice ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           INNER JOIN ArrangementInvoicePrice aip ON aip.idArticle = ap.idArticle AND aip.idArrangement = '" + idArrangment + @"'
                                           WHERE ap.idArrangement = '" + idArrangment + @"' AND ap.isExtra = '1' and ag.classArticalGroup = 'Accomod'
                                           AND ap.idArticle IN (SELECT DISTINCT idArticle FROM ArrangementBookArticles " + condition + @") AND CASE WHEN aip.sellingPrice IS NULL THEN ap.pricePerArticle ELSE aip.sellingPrice END>0 AND priceTotal IS NOT NULL
                                           UNION
                                           SELECT idPriceListArticle as idArrangementPrice,pl.idArrangement,pla.idArticle,pla.isExtra,commission,isBack,isAway,isAccomodation,
                                           isNotInAccompaniment, isNotForTraveler, a.nameArtical as nameArticle,pla.nrArticle,a.quantity,pl.idPriceList as idContract,
                                           'Contract' as nameContract,pl.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,CASE WHEN aip.sellingPrice IS NULL THEN pla.pricePerArticle ELSE aip.sellingPrice END AS pricePerArticle,pla.pricePerQuantity,pla.priceTotal,
                                           CASE WHEN a.isGroup=1 THEN CAST(( pla.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST( pla.priceTotal as numeric(18,2)) END as Total, pl.idUserCreated, pl.dtUserCreated,pl.idUserModified,pl.dtUserModified
                                           FROM PriceList pl
                                           INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                                           LEFT OUTER JOIN  CLient c ON c.idClient = pl.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           INNER JOIN ArrangementInvoicePrice aip ON aip.idArticle = pla.idArticle AND aip.idArrangement = '" + idArrangment + @"'
                                           WHERE pl.idArrangement = '" + idArrangment + @"' AND pla.isExtra=1 and ag.classArticalGroup = 'Accomod'
                                           AND pla.idArticle IN (SELECT DISTINCT idArticle FROM ArrangementBookArticles " + condition + @") AND CASE WHEN aip.sellingPrice IS NULL THEN pla.pricePerArticle ELSE aip.sellingPrice END>0 AND priceTotal IS NOT NULL");
            
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = (idArrangment == null) ? SqlInt32.Null : idArrangment;

            return conn.executeSelectQuery(query, sqlParameters);
        }


        //=== ovo je ubaceno da ne racuna ako je group za Volontere
        public DataTable GetAllArrangementPricesByArticleNoGroup(int idArrangment, string articleGroup, int MinNumTravelers, Boolean isFirst)
        {
            string query = string.Format(@"SELECT idArrangementPrice ,idArrangement,idArticle,a.nameArtical as nameArticle,nrArticle,a.quantity,'' as idContract,'No contract' as nameContract ,ap.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,ap.isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal, CASE WHEN a.isGroup=1 THEN CAST((ap.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST(ap.priceTotal as numeric(18,2)) END as Total,ap.idUserCreated, ap.dtUserCreated,ap.idUserModified,ap.dtUserModified
                                           FROM ArrangementPrice ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           WHERE idArrangement = '" + idArrangment + @"' AND ag.classArticalGroup = '" + articleGroup + @"'  and a.isGroup=0 
                                      UNION     
                                           SELECT idPriceListArticle as idArrangementPrice,pl.idArrangement,pla.idArticle,a.nameArtical as nameArticle,pla.nrArticle,a.quantity,pl.idPriceList as idContract,'Contract' as nameContract,pl.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,pla.isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, pla.pricePerArticle,pla.pricePerQuantity,pla.priceTotal, CASE WHEN a.isGroup=1 THEN CAST(( pla.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST( pla.priceTotal as numeric(18,2)) END as Total, pl.idUserCreated, pl.dtUserCreated,pl.idUserModified,pl.dtUserModified
                                           FROM PriceList pl
                                           INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                                           LEFT OUTER JOIN  CLient c ON c.idClient = pl.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           WHERE pl.idArrangement = '" + idArrangment + @"' AND ag.classArticalGroup = '" + articleGroup + @"' and a.isGroup=0 and pla.isExtra = 0");    //isExtra = 0 16.12.
            if (isFirst == true)
            {
                query = string.Format(@"SELECT idArrangementPrice ,idArrangement,idArticle,a.nameArtical as nameArticle,nrArticle,a.quantity,CASE WHEN (ap.idContract  IS NULL OR ap.idContract=0) then '' ELSE ap.idContract END  as idContract ,CASE WHEN (ap.idContract  IS NULL OR ap.idContract=0) then 'No contract' ELSE 'Contract' END as nameContract ,ap.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,ap.isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal, CASE WHEN a.isGroup=1 THEN CAST((ap.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST(ap.priceTotal as numeric(18,2)) END as Total, ap.idUserCreated, ap.dtUserCreated,ap.idUserModified,ap.dtUserModified
                                           FROM ArrangementCalculationFirstArticles ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           WHERE ap.idArrangement = '" + idArrangment + @"' AND ag.classArticalGroup = '" + articleGroup + @"' and a.isGroup=0 and ap.isExtra = 0 "); 
            }
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = (idArrangment == null) ? SqlInt32.Null : idArrangment;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        //===

        public DataTable GetAllArticals(int idArrangement, int idClient)
        {
            string query = string.Format(@"SELECT a.codeArtical, a.nameArtical, a.codeArtikalGroup,a.quantity, ag.nameArticalGroup, a.purchasePrice, a.sellingPrice, a.isGroup, a.idUserCreated, a.dtUserCreated,
                        a.idUserModifies, a.dtUserModified
                        FROM Artical a 
                        LEFT OUTER JOIN ArticalGroups ag ON a.codeArtikalGroup = ag.codeArticalGroup 
                        WHERE a.codeArtical NOT IN (SELECT DISTINCT pla.idArticle FROM PriceList pl
                        INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList WHERE pl.idClient = '" +idClient+"' AND pl.idArrangement = '"+idArrangement+"' AND pla.isExtra='1' )");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAllArticalsModel(int idArrangement, int idClient)
        {
            string query = string.Format(@" SELECT pl.idPricelist,pl.idClient,pl.idArrangement, pla.idArticle, a.codeArtical,pla.nrArticle,  pla.priceperarticle,pla.priceperquantity,pla.priceTotal
                        
           FROM PriceList pl
           inner join pricelistArticles pla on pl.idpricelist = pla.idPricelist
           inner join Artical a on pla.idArticle = a.codeArtical
           where pl.idClient = '" + idClient + "' AND pl.idArrangement = '" + idArrangement + "'  and a.codeArtical IN (SELECT DISTINCT pla.idArticle FROM PriceList pl)");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAllArticalsModelNEW(int idArrangement, int idClient)
        {
            string query = string.Format(@"  SELECT pl.idPricelist,pl.idClient,pl.idArrangement, pla.idArticle,pla.nrArticle, a.quantity,a.nameArtical,
                        CASE WHEN  pla.priceperarticle IS NULL THEN 0 ELSE pla.priceperarticle END AS priceperarticle,
                        CASE WHEN  pla.priceperquantity IS NULL THEN 0 ELSE pla.priceperquantity END AS priceperquantity,
                        CASE WHEN  a.isGroup=1 THEN pla.priceTotal ELSE  pla.priceTotal*pla.nrArticle*a.quantity END AS priceTotal
            FROM PriceList pl
           inner join pricelistArticles pla on pl.idpricelist = pla.idPricelist
           inner join Artical a on pla.idArticle = a.codeArtical
           where pl.idClient = '" + idClient + "' AND pl.idArrangement = '" + idArrangement + @"'  and a.codeArtical IN (SELECT DISTINCT pla.idArticle FROM PriceList pl)  and pla.isExtra = 0");
//            string query = string.Format(@" SELECT pl.idPricelist,pl.idClient,pl.idArrangement, pla.idArticle, a.codeArtical,pla.nrArticle, 
//                        CASE WHEN  pla.priceperarticle IS NULL THEN 0 ELSE pla.priceperarticle END AS priceperarticle,
//                        CASE WHEN  pla.priceperquantity IS NULL THEN 0 ELSE pla.priceperquantity END AS priceperquantity,
//                        CASE WHEN  pla.priceTotal IS NULL THEN 0 ELSE pla.priceTotal*pla.nrArticle END AS priceTotal
//           FROM PriceList pl
//           inner join pricelistArticles pla on pl.idpricelist = pla.idPricelist
//           inner join Artical a on pla.idArticle = a.codeArtical
//           where pl.idClient = '" + idClient + "' AND pl.idArrangement = '" + idArrangement + @"'  and a.codeArtical IN (SELECT DISTINCT pla.idArticle FROM PriceList pl) and pla.isExtra = 0
//           UNION 
//           SELECT ap.idArrangementPrice,ap.idClient,ap.idArrangement, ap.idArticle, a.codeArtical,ap.nrArticle,  
//           CASE WHEN  ap.priceperarticle IS NULL THEN 0 ELSE ap.priceperarticle END AS priceperarticle,
//           CASE WHEN  ap.priceperquantity IS NULL THEN 0 ELSE ap.priceperquantity END AS priceperquantity,
//           CASE WHEN  ap.priceTotal IS NULL THEN 0 ELSE ap.priceTotal * ap.nrArticle END AS priceTotal
//           FROM ArrangementPrice ap
//           inner join Artical a on ap.idArticle = a.codeArtical
//           where ap.idClient = '" + idClient + "' AND ap.idArrangement = '" + idArrangement + "' and ap.isExtra = 0");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable checkIfArrangementPriceExist(int idArrangmentPrice, int idArrangement, string idArticle, int idClient, Boolean isInsert)
        {

            string condition = " AND a.codeArtical NOT IN (SELECT idArticle FROM ArrangementPrice WHERE idArrangement ='" + idArrangement + @"' AND idArticle ='" + idArticle + @"' AND idClient = '" + idClient + "')";
           
            if (isInsert == false)
            {
                condition = condition.Replace(")", " AND idArrangementPrice <> '" + idArrangmentPrice + "')");
            }

            
            string query = string.Format(@"SELECT a.codeArtical, a.nameArtical, a.codeArtikalGroup,a.quantity, ag.nameArticalGroup, a.purchasePrice, a.sellingPrice, a.idUserCreated, a.dtUserCreated,
                        a.idUserModifies, a.dtUserModified
                        FROM Artical a 
                        LEFT OUTER JOIN ArticalGroups ag ON a.codeArtikalGroup = ag.codeArticalGroup 
                        WHERE a.codeArtical NOT IN (SELECT DISTINCT pla.idArticle FROM PriceList pl
                        INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList WHERE pla.idArticle = '" + idArticle + @"' AND pl.idClient = '" + idClient + "' AND pl.idArrangement = '" + idArrangement + @"' AND pla.isExtra='1' )
                         "+ condition);

            return conn.executeSelectQuery(query, null);
        }

        public DataTable checkIfArrangementPriceAlreadyInRooms(int id, int idArrangement, string idArticle)
        {


            string query = string.Format(@"SELECT id FROM ArrangementRooms WHERE id = '"+id+"' AND idArticle = '"+idArticle+"' AND idArrangement = '"+idArrangement+"' AND isContract = '0'");

            return conn.executeSelectQuery(query, null);
        }

        //========
        public DataTable GetArticalByID(string idArtical)
        {
            string query = string.Format(@"SELECT idArrangementPrice ,idArrangement,idArticle,a.nameArtical as nameArticle,nrArticle,a.quantity,'No contract' as nameContract ,ap.idClient,c.nameClient,dtFrom,dtTo,ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal, ap.idUserCreated, ap.dtUserCreated,ap.idUserModified,ap.dtUserModified
                                           FROM ArrangementPrice ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           WHERE idArticle = @idArtical");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArtical", SqlDbType.NVarChar);
            sqlParameters[0].Value = idArtical;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        //========

        public DataTable GetArrangementPriceByID(int idArrangementPrice)
        {
            string query = string.Format(@"SELECT idArrangementPrice ,idArrangement,idArticle,a.nameArtical as nameArticle,nrArticle,a.quantity,'No contract' as nameContract ,ap.idClient,c.nameClient,dtFrom,dtTo,ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal, ap.idUserCreated, ap.dtUserCreated,ap.idUserModified,ap.dtUserModified
                                           FROM ArrangementPrice ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           WHERE idArrangementPrice = @idArrangementPrice");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangementPrice", SqlDbType.Int);
            sqlParameters[0].Value = (idArrangementPrice == null) ? SqlInt32.Null : idArrangementPrice;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public Boolean Save(ArrangementPriceModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO ArrangementPrice(idArrangement, idArticle,  idClient, nrArticle, pricePerArticle, pricePerQuantity, priceTotal,dtFrom, dtTo, idUserCreated, dtUserCreated, isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler )
                      VALUES (@idArrangement, @idArticle,  @idClient, @nrArticle, @pricePerArticle, @pricePerQuantity,@priceTotal,@dtFrom, @dtTo, @idUserCreated, @dtUserCreated,@isExtra,@commission,@isBack,@isAway,@isAccomodation,@isNotInAccompaniment, @isNotForTraveler)");


            SqlParameter[] sqlParameter = new SqlParameter[18];

            sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[0].Value = (model.idArrangement == 0) ? SqlInt32.Null : model.idArrangement;

            sqlParameter[1] = new SqlParameter("@idArticle", SqlDbType.NVarChar);
            sqlParameter[1].Value = (model.idArticle == null) ? SqlString.Null : model.idArticle;

            sqlParameter[2] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[2].Value = (model.idClient == 0) ? SqlInt32.Null : model.idClient;

            sqlParameter[3] = new SqlParameter("@nrArticle", SqlDbType.Int);
            sqlParameter[3].Value = (model.nrArticle == 0) ? SqlInt32.Null : model.nrArticle;

            sqlParameter[4] = new SqlParameter("@pricePerArticle", SqlDbType.Decimal);
            sqlParameter[4].Value = (model.pricePerArticle == 0) ? SqlDecimal.Null : model.pricePerArticle;

            sqlParameter[5] = new SqlParameter("@pricePerQuantity", SqlDbType.Decimal);
            sqlParameter[5].Value = (model.pricePerQuantity == 0) ? SqlDecimal.Null : model.pricePerQuantity;

            sqlParameter[6] = new SqlParameter("@priceTotal", SqlDbType.Decimal);
            sqlParameter[6].Value = (model.priceTotal == 0) ? SqlDecimal.Null : model.priceTotal;

            sqlParameter[7] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameter[7].Value = (model.dtFrom == null) ? SqlDateTime.MinValue : model.dtFrom;

            sqlParameter[8] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameter[8].Value = (model.dtTo == null) ? SqlDateTime.MinValue : model.dtTo;

            sqlParameter[9] = new SqlParameter("@idUserCreated", SqlDbType.Int);
            sqlParameter[9].Value = (model.idUserCreated == 0) ? SqlInt32.Null : model.idUserCreated;

            sqlParameter[10] = new SqlParameter("@dtUserCreated", SqlDbType.DateTime);
            sqlParameter[10].Value = (model.dtUserCreated == null) ? SqlDateTime.MinValue : model.dtUserCreated;

            sqlParameter[11] = new SqlParameter("@isExtra", SqlDbType.Bit);
            sqlParameter[11].Value = (model.isExtra == null) ? false : model.isExtra;

            sqlParameter[12] = new SqlParameter("@commission", SqlDbType.Decimal);
            sqlParameter[12].Value = model.commission;

            sqlParameter[13] = new SqlParameter("@isAway", SqlDbType.Bit);
            sqlParameter[13].Value = model.isAway;

            sqlParameter[14] = new SqlParameter("@isBack", SqlDbType.Bit);
            sqlParameter[14].Value = model.isBack;

            sqlParameter[15] = new SqlParameter("@isAccomodation", SqlDbType.Bit);
            sqlParameter[15].Value = model.isAccomodation;

            sqlParameter[16] = new SqlParameter("@isNotInAccompaniment", SqlDbType.Bit);
            sqlParameter[16].Value = model.isNotInAccompaniment;

            sqlParameter[17] = new SqlParameter("@isNotForTraveler", SqlDbType.Bit);
            sqlParameter[17].Value = model.isNotForTraveler;

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
            sqlParameter[4].Value = conn.GetLastTableID("ArrangementPrice") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangementPrice";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementPrice";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);
            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean Update(ArrangementPriceModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE ArrangementPrice SET
                     idArrangement = @idArrangement, idArticle = @idArticle, idClient = @idClient, nrArticle = @nrArticle,
                     pricePerArticle = @pricePerArticle,  pricePerQuantity = @pricePerQuantity, priceTotal = @priceTotal,  
                     dtFrom = @dtFrom, dtTo = @dtTo,idUserModified = @idUserModified, dtUserModified = @dtUserModified, isExtra = @isExtra,
                    commission=@commission,isBack=@isBack,isAway=@isAway,isAccomodation=@isAccomodation,isNotInAccompaniment=@isNotInAccompaniment, isNotForTraveler = @isNotForTraveler 
                     WHERE idArrangementPrice = @idArrangementPrice");


            SqlParameter[] sqlParameter = new SqlParameter[19];

            sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[0].Value = (model.idArrangement == 0) ? SqlInt32.Null : model.idArrangement;

            sqlParameter[1] = new SqlParameter("@idArticle", SqlDbType.NVarChar);
            sqlParameter[1].Value = (model.idArticle == null) ? SqlString.Null : model.idArticle;

            sqlParameter[2] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[2].Value = (model.idClient == 0) ? SqlInt32.Null : model.idClient;

            sqlParameter[3] = new SqlParameter("@nrArticle", SqlDbType.Int);
            sqlParameter[3].Value = (model.nrArticle == 0) ? SqlInt32.Null : model.nrArticle;

            sqlParameter[4] = new SqlParameter("@pricePerArticle", SqlDbType.Decimal);
            sqlParameter[4].Value = (model.pricePerArticle == 0) ? SqlDecimal.Null : model.pricePerArticle;

            sqlParameter[5] = new SqlParameter("@pricePerQuantity", SqlDbType.Decimal);
            sqlParameter[5].Value = (model.pricePerQuantity == 0) ? SqlDecimal.Null : model.pricePerQuantity;

            sqlParameter[6] = new SqlParameter("@priceTotal", SqlDbType.Decimal);
            sqlParameter[6].Value = (model.priceTotal == 0) ? SqlDecimal.Null : model.priceTotal;

            sqlParameter[7] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameter[7].Value = (model.dtFrom == null) ? SqlDateTime.MinValue : model.dtFrom;

            sqlParameter[8] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameter[8].Value = (model.dtTo == null) ? SqlDateTime.MinValue : model.dtTo;

            sqlParameter[9] = new SqlParameter("@idUserModified", SqlDbType.Int);
            sqlParameter[9].Value = (model.idUserModified == 0) ? SqlInt32.Null : model.idUserModified;

            sqlParameter[10] = new SqlParameter("@dtUserModified", SqlDbType.DateTime);
            sqlParameter[10].Value = (model.dtUserModified == null) ? SqlDateTime.MinValue : model.dtUserModified;

            sqlParameter[11] = new SqlParameter("@idArrangementPrice", SqlDbType.Int);
            sqlParameter[11].Value = (model.idArrangementPrice == null) ? SqlInt32.Null : model.idArrangementPrice;

            sqlParameter[12] = new SqlParameter("@isExtra", SqlDbType.Bit);
            sqlParameter[12].Value = (model.isExtra == null) ? false : model.isExtra;

            sqlParameter[13] = new SqlParameter("@commission", SqlDbType.Decimal);
            sqlParameter[13].Value = model.commission;

            sqlParameter[14] = new SqlParameter("@isAway", SqlDbType.Bit);
            sqlParameter[14].Value = model.isAway;

            sqlParameter[15] = new SqlParameter("@isBack", SqlDbType.Bit);
            sqlParameter[15].Value = model.isBack;

            sqlParameter[16] = new SqlParameter("@isAccomodation", SqlDbType.Bit);
            sqlParameter[16].Value = model.isAccomodation;

            sqlParameter[17] = new SqlParameter("@isNotInAccompaniment", SqlDbType.Bit);
            sqlParameter[17].Value = model.isNotInAccompaniment;

            sqlParameter[18] = new SqlParameter("@isNotForTraveler", SqlDbType.Bit);
            sqlParameter[18].Value = model.isNotForTraveler;

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
            sqlParameter[4].Value = model.idArrangementPrice.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangementPrice";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementPrice";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean Delete(int idArrangementPrice, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM ArrangementPrice 
                    WHERE idArrangementPrice = @idArrangementPrice");


            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idArrangementPrice", SqlDbType.Int);
            sqlParameter[0].Value = (idArrangementPrice == null) ? SqlInt32.Null : idArrangementPrice;

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
            sqlParameter[4].Value = idArrangementPrice.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangementPrice";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementPrice";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";


            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        //======================
        public DataTable GetACalculationRepotr(DateTime dateFrom, DateTime dateTo, int idLabel)
        {
            string query = string.Format(@"SELECT  DISTINCT a.idArrangement,codeArrangement,a.dtFromArrangement,
            CASE WHEN ac.price is not null and a.nrTraveler is not null THEN ac.price * a.nrTraveler ELSE 0 END as total,
            CASE WHEN acfa.sumPriceTotalArrCFirst IS NOT NULL THEN acfa.sumPriceTotalArrCFirst ELSE 0 END as subTotal,
            CASE WHEN abbook.num  is not null and ac.price is not null  THEN  ac.price*abbook.num ELSE 0 END as total2,
            CASE WHEN acfa.sumPriceTotalArrCFirst IS NOT NULL THEN acfa.sumPriceTotalArrCFirst ELSE 0 END as subTotal2,
            CASE WHEN acs.price IS NOT NULL AND abbook.num IS NOT NULL THEN acs.price*abbook.num ELSE 0 END as totalSecond,
            CASE WHEN pla.sumPriceTotalPLArticles IS not null  THEN pla.sumPriceTotalPLArticles  ELSE  0 END  subTotalSecondPla,
            CASE WHEN ap.sumPriceTotalArrPrice IS not null  THEN ap.sumPriceTotalArrPrice  ELSE  0 END  subTotalSecondAp


              FROM Arrangement a 
              LEFT OUTER JOIN ArrangementCalculation ac on a.idArrangement=ac.idArrangement
              LEFT OUTER JOIN (select SUM(CASE WHEN art.isGroup='true'  THEN priceTotal ELSE acf.priceTotal*acf.nrArticle*art.quantity END) as sumPriceTotalArrCFirst, idArrangement 
                         FROM  ArrangementCalculationFirstArticles acf 
                         LEFT OUTER JOIN Artical art on acf.idArticle=art.codeArtical  
                         WHERE isExtra='False'
                         GROUP BY idArrangement) acfa on a.idArrangement=acfa.idArrangement
              LEFT OUTER JOIN ArrangementCalculationSecond acs on a.idArrangement= acs.idArrangement
              LEFT OUTER JOIN PriceList pl on pl.idArrangement=a.idArrangement
              LEFT OUTER JOIN (SELECT SUM(CASE WHEN art.isGroup='true'  THEN pll.priceTotal ELSE pll.priceTotal*pll.nrArticle*art.quantity END)as sumPriceTotalPLArticles,pl.idArrangement
                         FROM  PriceList pl 
                         LEFT OUTER JOIN PriceListArticles pll on pll.idPricelist=pl.idPricelist AND isExtra='False'   
                         LEFT JOIN Artical art ON art.codeArtical = pll.idArticle  
                         GROUP BY pl.idArrangement )pla on  pla.idArrangement= a.idArrangement  
              LEFT OUTER JOIN ArrangementBook m on a.idArrangement = m.idArrangement
              LEFT OUTER JOIN (SELECT SUM(CASE WHEN art.isGroup='true'  THEN ap.priceTotal ELSE ap.priceTotal*ap.nrArticle*art.quantity END) as sumPriceTotalArrPrice, idArrangement 
                         FROM ArrangementPrice ap
                         LEFT JOIN Artical art ON art.codeArtical = ap.idArticle 
                         WHERE ap.isExtra='False'
                         GROUP BY ap.idArrangement) ap on ap.idArrangement= a.idArrangement 
              LEFT OUTER JOIN (SELECT COUNT (a.idContPers) as num, idArrangement
                       FROM ArrangementBook a
                       LEFT OUTER JOIN Invoice i on a.idArrangement=i.idVoucher
                       WHERE a.idStatus ='2' AND a.idContPers   IN (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter != '4') 
                          AND a.idArrangementBook IN (SELECT idArrangementBook FROM Invoice )
                       GROUP by a.idArrangement) abbook ON  a.idArrangement=abbook.idArrangement
              LEFT OUTER JOIN ArrangementLabel al on al.idArrangement=a.idArrangement
              LEFT OUTER JOIN (SELECT CASE WHEN id is not null then id ELSE idLabel END as idLab FROM Labels) l on l.idLab=al.idLabel
 
               WHERE l.idLab='" + idLabel + "' AND a.dtFromArrangement>='" + dateFrom.ToString("MM/dd/yyyy") + "' and dtToArrangement<='" + dateTo.ToString("MM/dd/yyyy") + "' order by codeArrangement");

            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@dateFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dateFrom;

            sqlParameters[1] = new SqlParameter("@dateTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dateTo;

            sqlParameters[2] = new SqlParameter("@idLabel", SqlDbType.Int);
            sqlParameters[2].Value = idLabel;
            return conn.executeSelectQuery(query, sqlParameters);
        }
    }


}