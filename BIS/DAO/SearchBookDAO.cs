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
    public class SearchBookDAO
    {
        private dbConnection conn;

        public SearchBookDAO()
        {
            conn = new dbConnection();
        }

        #region stari upit
        //        public DataTable GetFilteredAArrangementsBook(DateTime dtFrom, DateTime dtTo, string nameCountry, string status, string artical1, string artical2, string artical3)
        //        {
        //            string conditions = "";
        //            if (dtFrom != null || dtTo != null || nameCountry != null || status != null || artical1 != null || artical2 != null || artical3 != null ) 
        //            {
        //                conditions = "WHERE ";
        //            }
        //            if (dtFrom != null) 
        //            {
        //                conditions += "a.dtFromArrangement >= @dtFrom and ";
        //            }
        //            if (dtTo != null) 
        //            {
        //                conditions += "a.dtToArrangement <= @dtTo and ";
        //            }
        //            if (nameCountry != null) 
        //            {
        //                conditions += "c.nameCountry = '"+  nameCountry+"' and ";
        //            }
        //            if (status != null) 
        //            {
        //                conditions += "a.statusArrangement = '" + status + "' and ";
        //            }
        //            if(artical1 !=null)
        //            {

        //                conditions += "filArt.nameArticle = '" + artical1 + "' and ";
        //            }
        //            if ( artical2 != null)
        //            {

        //                conditions += "filArt.nameArticle = '" + artical1 + "' and ";
        //            }
        //            if (artical3 != null)
        //            {

        //                conditions += "filArt.nameArticle = '" + artical1 + "' and ";
        //            }
        //            if (conditions != "") 
        //            {
        //                conditions = conditions.Substring(0, conditions.Length - 5);
        //            }
        //            string query = string.Format(@"SELECT a.idArrangement,a.nameArrangement,a.dtFromArrangement,a.dtToArrangement , a.statusArrangement, c.nameCountry, whcount.wheelchair,rollCount.Rollator, asCount.armSometimes,anCount.anchorage,filArt.idRoom
        //                                          FROM Arrangement as a
        //                                          LEFT OUTER JOIN Country c on a.countryArrangement  = c.idCountry
        //
        //                                          LEFT OUTER JOIN (SELECT COUNT(a.idContpers) as wheelchair, d.idArrangement
        //				                                FROM ContactPerson a
        //				                                LEFT JOIN MedCpr b on a.idContPers = b.idcpr 
        //				                                LEFT JOIN ArrangementBook d on d.idContPers = a.idContPers
        //				                                WHERE (d.idstatus = 1 OR d.idStatus=2) and (idAns = 441 or idAns = 442 or idAns = 449 or idAns = 450 or idAns = 451)
        //                                                GROUP BY d.idArrangement) whcount on a.idArrangement = whcount.idArrangement
        //                 
        //                                          LEFT OUTER JOIN (SELECT count(a.idContpers) as Rollator, d.idArrangement
        //				                                FROM ContactPerson a
        //				                                LEFT JOIN MedCpr b on a.idContPers = b.idcpr 
        //				                                LEFT JOIN ArrangementBook d on d.idContPers = a.idContPers
        //				                                WHERE (d.idstatus = 1 OR d.idStatus=2) and (idAns =  446 or idAns = 447 or idAns = 448 )
        //                                                GROUP BY d.idArrangement) rollCount on a.idArrangement = whcount.idArrangement
        //                                          LEFT OUTER JOIN (SELECT idArrangement, nameArticle, idRoom
        //                                                FROM (SELECT idArrangement, ar.idArticle,a.nameArtical as nameArticle,ar.idRoom, ar.isContract,ar.id,ab.idContPers, cp.firstname+ ' ' +cp.lastname as name,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender, 'Voluntary' as type
        //                                                FROM ArrangementRooms ar
        //                                          LEFT OUTER JOIN  Artical a ON a.codeArtical = ar.idArticle
        //                                          LEFT OUTER JOIN  ArticalGroups ag  ON a.codeArtikalGroup = ag.codeArticalGroup
        //                                          LEFT OUTER JOIN  
        //                                                (SELECT idContPers,idRoom,idArticle,isContract,id FROM ArrangementBookArticles aba LEFT OUTER JOIN ArrangementBook abb  ON abb.idArrangementBook = aba.idArrangementBook) ab 
        //                                                ON ab.idRoom = ar.idRoom AND a.codeArtical = ab.idArticle AND ab.isContract = ar.isContract AND ab.id = ar.id
        //                                          LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = ab.idContPers
        //                                          LEFT OUTER JOIN Gender g ON cp.idGender = g.idGender
        //                                          LEFT JOIN STRINGEN  s ON g.nameGender=s.stringKey
        //                                                WHERE cp.idContPers IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4') 
        //                                          UNION
        //                                                SELECT idArrangement,ar.idArticle,a.nameArtical as nameArticle,ar.idRoom, ar.isContract,ar.id,ab.idContPers, cp.firstname+ ' ' +cp.lastname as name,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender, 'Traveler' as type
        //                                                    FROM ArrangementRooms ar
        //                                                LEFT OUTER JOIN  Artical a ON a.codeArtical = ar.idArticle
        //                                                LEFT OUTER JOIN  ArticalGroups ag  ON a.codeArtikalGroup = ag.codeArticalGroup
        //                                                LEFT OUTER JOIN  
        //                                                    (SELECT idContPers,idRoom,idArticle,isContract,id FROM ArrangementBookArticles aba LEFT OUTER JOIN ArrangementBook abb  ON abb.idArrangementBook = aba.idArrangementBook) ab 
        //                                                    ON ab.idRoom = ar.idRoom AND a.codeArtical = ab.idArticle AND ab.isContract = ar.isContract AND ab.id = ar.id
        //                                                LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = ab.idContPers
        //                                                LEFT OUTER JOIN Gender g ON cp.idGender = g.idGender
        //                                                LEFT JOIN STRINGEN  s ON g.nameGender=s.stringKey
        //                                                    WHERE cp.idContPers NOT IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4')
        //                                          UNION
        //                                            SELECT idArrangement, ar.idArticle,a.nameArtical as nameArticle,ar.idRoom, ar.isContract,ar.id,ab.idContPers, cp.firstname+ ' ' +cp.lastname as name,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender, '' as type
        //                                            FROM ArrangementRooms ar
        //                                            LEFT OUTER JOIN  Artical a ON a.codeArtical = ar.idArticle
        //                                            LEFT OUTER JOIN  ArticalGroups ag  ON a.codeArtikalGroup = ag.codeArticalGroup
        //                                            LEFT OUTER JOIN  
        //                                                (SELECT idContPers,idRoom,idArticle,isContract,id FROM ArrangementBookArticles aba LEFT OUTER JOIN ArrangementBook abb  ON abb.idArrangementBook = aba.idArrangementBook ) ab 
        //                                                ON ab.idRoom = ar.idRoom AND a.codeArtical = ab.idArticle AND ab.isContract = ar.isContract AND ab.id = ar.id
        //                                            LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = ab.idContPers
        //                                            LEFT OUTER JOIN Gender g ON cp.idGender = g.idGender
        //                                            LEFT JOIN STRINGEN  s ON g.nameGender=s.stringKey
        //                                                WHERE cp.idContPers IS NULL) dd
        //                                            where idContPers is null
        //                                            ) filArt on a.idArrangement = filArt.idArrangement
        //                                          LEFT OUTER JOIN (SELECT count(a.idContpers) as armSometimes, d.idArrangement
        //				                                FROM ContactPerson a
        //				                                LEFT JOIN MedCpr b on a.idContPers = b.idcpr 
        //				                                LEFT JOIN ArrangementBook d on d.idContPers = a.idContPers
        //				                                WHERE (d.idstatus = 1 OR d.idStatus=2) and (idAns = 439 or idAns = 440)
        //                                                GROUP BY d.idArrangement) asCount on a.idArrangement = whcount.idArrangement
        //                
        //                                          LEFT OUTER JOIN (SELECT count(a.idContpers) as anchorage, d.idArrangement
        //				                                FROM ContactPerson a
        //				                          LEFT JOIN MedCpr b on a.idContPers = b.idcpr 
        //				                          LEFT JOIN ArrangementBook d on d.idContPers = a.idContPers
        //				                                WHERE (d.idstatus = 1 OR d.idStatus=2) and (idAns = 823)
        //                                                GROUP BY d.idArrangement) anCount on a.idArrangement = whcount.idArrangement
        //                                             " + conditions);


        //            SqlParameter[] sqlParemeters = new SqlParameter[2];

        //            sqlParemeters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
        //            sqlParemeters[0].Value = dtFrom;

        //            sqlParemeters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
        //            sqlParemeters[1].Value = dtTo;

        //            return conn.executeSelectQuery(query, sqlParemeters);
        //        }

        #endregion

        public DataTable GetFilteredAArrangementsBook(DateTime dtFrom, DateTime dtTo, int idCountry, string status, int help, List<int> themeTrip, List<string> articles, List<int> label)
        {
            string conditions = "";
            string trip = "";
            string helpText1 = "0";
            string helpText2 = "0";
            string helpText3 = "0";
            string helpText4 = "0";
            string zeroCondition = "";
            string articlesCondition = "";
            string conditionLabel = "";

            #region label

            if (label != null)
                if (label.Count > 0)
                {
                    int count = 0;

                    foreach (var idLabel in label)
                    {
                        if (idLabel.ToString() != "0")
                            if (count == label.Count - 1)
                            {
                                conditionLabel = " AND (" + conditionLabel + " l.idLabel = '" + idLabel.ToString() + "')";
                            }
                            else
                            {
                                conditionLabel += " l.idLabel = '" + idLabel.ToString() + "' OR ";
                            }

                        count++;
                    }
                }
            #endregion

            if (idCountry!=0)
            {
                conditions = " and c.idCountry = '"+idCountry+"' ";
            }
            if (themeTrip != null)
                if (themeTrip.Count > 0)
                {
                    trip = " and idArrangement IN (SELECT idArrangement FROM ArrangementThemeTrip ";
                     string tripWhere = "";
                    for (int i = 0; i < themeTrip.Count; i++)
                        {
                           
                            if (themeTrip.Count == 1)
                            {
                                tripWhere = tripWhere + " WHERE idThemeTrip = '" + themeTrip[i].ToString() + "')";
                            }
                            else
                            {
                               
                                if (i == 0)
                                    tripWhere = tripWhere + " idThemeTrip = '" + themeTrip[i].ToString() + "'";
                                else
                                    if (i == themeTrip.Count - 1)
                                        tripWhere = "  WHERE " + tripWhere + " OR idThemeTrip = '" + themeTrip[i].ToString() + "' )";
                                    else
                                        tripWhere = tripWhere + " OR idThemeTrip = '" + themeTrip[i].ToString() + "'";
                            }
                        }
                    trip = trip + tripWhere;
                }
            //wheelchair
            if(help == 1 || help == 0)
            {
                List<int> idAns = new List<int> { 441, 442, 449, 450, 451, 452, 453 };
                 string ans = "";
            if (idAns.Count > 0)
            {
                for(int i = 0 ;i< idAns.Count;i++)
                {
                    if (idAns.Count == 1)
                    {
                        ans = " (idAns = '" + idAns[i].ToString() + "')";
                    }
                    else
                    {
                        if (i == 0)
                            ans = ans + " idAns = '" + idAns[i].ToString() + "'";
                        else
                            if(i== idAns.Count-1)
                                ans = " ( " + ans + " OR idAns = '" + idAns[i].ToString() + "' )";
                        else
                                ans = ans + " OR idAns = '" + idAns[i].ToString() + "'";
                    }
                }
            }
            helpText1 = string.Format(@" (SELECT DISTINCT whoseElectricWheelchairs - ( SELECT COUNT( a.idContpers) as num from ContactPerson a
                                             LEFT JOIN MedCpr b on a.idContPers = b.idcpr 
                                             LEFT JOIN ArrangementBook d on d.idCOntPers = a.idContPers
                                             WHERE  d.idArrangement =ac.idArrangement and (d.idstatus = 1 OR d.idStatus=2) and " + ans+@")  
                                             FROM Arrangement ac 
                                             WHERE idArrangement = a.idArrangement)");
            }

                 //rollator
            if(help == 2 || help == 0)
            {
                List<int> idAns = new List<int> { 446, 447, 448 };
                 string ans = "";
            if (idAns.Count > 0)
            {
                for(int i = 0 ;i< idAns.Count;i++)
                {
                    if (idAns.Count == 1)
                    {
                        ans = " (idAns = '" + idAns[i].ToString() + "')";
                    }
                    else
                    {
                        if (i == 0)
                            ans = ans + " idAns = '" + idAns[i].ToString() + "'";
                        else
                            if(i== idAns.Count-1)
                                ans = " ( " + ans + " OR idAns = '" + idAns[i].ToString() + "' )";
                        else
                                ans = ans + " OR idAns = '" + idAns[i].ToString() + "'";
                    }
                }

                helpText2 = string.Format(@" (SELECT DISTINCT buRollators - ( SELECT COUNT( a.idContpers) as num from ContactPerson a
                                             LEFT JOIN MedCpr b on a.idContPers = b.idcpr 
                                             LEFT JOIN ArrangementBook d on d.idCOntPers = a.idContPers
                                             WHERE  d.idArrangement =ac.idArrangement and (d.idstatus = 1 OR d.idStatus=2) and " + ans + @")  
                                             FROM Arrangement ac 
                                             WHERE idArrangement = a.idArrangement)");
            }
            }

            //arm somethimes
            if (help == 3 || help == 0)
            {
                List<int> idAns = new List<int> { 439, 440 };
                string ans = "";
                if (idAns.Count > 0)
                {
                    for (int i = 0; i < idAns.Count; i++)
                    {
                        if (idAns.Count == 1)
                        {
                            ans = " (idAns = '" + idAns[i].ToString() + "')";
                        }
                        else
                        {
                            if (i == 0)
                                ans = ans + " idAns = '" + idAns[i].ToString() + "'";
                            else
                                if (i == idAns.Count - 1)
                                    ans = " ( " + ans + " OR idAns = '" + idAns[i].ToString() + "' )";
                                else
                                    ans = ans + " OR idAns = '" + idAns[i].ToString() + "'";
                        }
                    }

                    helpText3 = string.Format(@" (SELECT DISTINCT buSupportingArms - ( SELECT COUNT( a.idContpers) as num from ContactPerson a
                                             LEFT JOIN MedCpr b on a.idContPers = b.idcpr 
                                             LEFT JOIN ArrangementBook d on d.idCOntPers = a.idContPers
                                             WHERE  d.idArrangement =ac.idArrangement and (d.idstatus = 1 OR d.idStatus=2) and " + ans + @")  
                                             FROM Arrangement ac 
                                             WHERE idArrangement = a.idArrangement)");
                }
            }

            //anchorage
            if (help == 4 || help == 0)
            {
                List<int> idAns = new List<int> { 823 };
                string ans = "";
                if (idAns.Count > 0)
                {
                    for (int i = 0; i < idAns.Count; i++)
                    {
                        if (idAns.Count == 1)
                        {
                            ans = " (idAns = '" + idAns[i].ToString() + "')";
                        }
                        else
                        {
                            if (i == 0)
                                ans = ans + " idAns = '" + idAns[i].ToString() + "'";
                            else
                                if (i == idAns.Count - 1)
                                    ans = " ( " + ans + " OR idAns = '" + idAns[i].ToString() + "' )";
                                else
                                    ans = ans + " OR idAns = '" + idAns[i].ToString() + "'";
                        }
                    }

                    helpText4 = string.Format(@" (SELECT DISTINCT nrAnchorage - ( SELECT COUNT( a.idContpers) as num FROM ContactPerson a
                                             LEFT JOIN MedCpr b on a.idContPers = b.idcpr 
                                             LEFT JOIN ArrangementBook d on d.idCOntPers = a.idContPers
                                             WHERE  d.idArrangement =ac.idArrangement and (d.idstatus = 1 OR d.idStatus=2) and " + ans + @")  
                                             FROM Arrangement ac 
                                             WHERE idArrangement = a.idArrangement)");
                }
            }


            //Obrisano zbog uslova NONE radio button-a
            //if (help == 0)
            //    zeroCondition = " AND (" + helpText1 + ">0 OR " + helpText2 + ">0 OR " + helpText3 + ">0 OR " + helpText4 + ">0 )";
            //else 
                if (help == 1)
                zeroCondition = " AND (" + helpText1 + ">0 )";
            else if (help == 2)
                zeroCondition = " AND (" + helpText2 + ">0 )";
            else if (help == 3)
                zeroCondition = " AND (" + helpText3 + ">0 )";
            else if (help == 4)
                zeroCondition = " AND (" + helpText4 + ">0 )";


            string article = "";

            if(articles!=null)
                if(articles.Count>0)
                    for (int i = 0; i < articles.Count; i++)
                    {
                        if (i < articles.Count-1)
                        {
                            article = article + "IdArticle = '" + articles[i] + "' AND ";
                        }
                        else
                        {
                            article = "(" + article + "IdArticle = '" + articles[i] + "')";
                            articlesCondition = @" LEFT JOIN 
								(
								SELECT ap.idArticle,ap. idArrangement,ap.nrArticle*a.quantity -  (SELECT COUNT (idRoom)
                                FROM ArrangementBookArticles aba 
                                LEFT OUTER JOIN ArrangementBook ab ON ab.idArrangementBook = aba.idArrangementBook
                                WHERE idArticle = ap.idArticle	 AND idArrangement = ap.idArrangement) as number
								FROM ArrangementPrice ap
                                LEFT OUTER JOIN  CLient cl ON cl.idClient = ap.idClient
                                LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                LEFT OUTER JOIN  ArticalGroups ag  ON a.codeArtikalGroup = ag.codeArticalGroup
                                LEFT OUTER JOIN Arrangement ar ON ar.idArrangement=ap.idArrangement
                                LEFT OUTER JOIN Country c ON ar.countryArrangement=c.idCountry
                                UNION
                                SELECT pla.idArticle,pl.idArrangement,pla.nrArticle*a.quantity- (SELECT COUNT (idRoom)
                                FROM ArrangementBookArticles aba 
                                LEFT OUTER JOIN ArrangementBook ab ON ab.idArrangementBook = aba.idArrangementBook
                                WHERE idArticle = pla.idArticle	 AND idArrangement = pl.idArrangement) as number
                                FROM PriceList pl
                                INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                                LEFT OUTER JOIN  CLient cl ON cl.idClient = pl.idClient
                                LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                                LEFT OUTER JOIN  ArticalGroups ag  ON a.codeArtikalGroup = ag.codeArticalGroup
                                LEFT OUTER JOIN Arrangement ar ON ar.idArrangement=pl.idArrangement
                                LEFT OUTER JOIN Country c ON ar.countryArrangement=c.idCountry ) dd ON a.idArrangement = dd.idArrangement AND " + article;
                            }
                        
                    }


            string query = string.Format(@"SELECT DISTINCT a.idArrangement,a.nameArrangement,a.dtFromArrangement,
								  a.dtToArrangement , a.statusArrangement, c.nameCountry," + helpText1 + @" as wheelchair," + helpText2 + @" as Rollator,
                                   " + helpText3 + @" as armSometimes," + helpText4 + @" as anchorage
                                  FROM Arrangement as a
                                  LEFT OUTER JOIN Country c on a.countryArrangement  = c.idCountry
LEFT OUTER JOIN ArrangementLabel l ON l.idArrangement=a.idArrangement
                                  WHERE a.statusArrangement = '" + status+"' and a.dtFromArrangement >= @dtFrom and a.dtToArrangement <= @dtTo " 
                                                                + conditions + trip + zeroCondition +conditionLabel
                                                                );

              if(articles!=null)
                if(articles.Count>0)
                    query = string.Format(@"SELECT DISTINCT a.idArrangement,a.nameArrangement,a.dtFromArrangement,
								  a.dtToArrangement , a.statusArrangement, c.nameCountry," + helpText1 + @" as wheelchair," + helpText2 + @" as Rollator,
                                   " + helpText3 + @" as armSometimes," + helpText4 + @" as anchorage
                                  FROM Arrangement as a
                                  LEFT OUTER JOIN Country c on a.countryArrangement  = c.idCountry
                                    " + articlesCondition+@"
                                  WHERE a.statusArrangement = '" + status + "' and a.dtFromArrangement >= @dtFrom and a.dtToArrangement <= @dtTo "  
                                                                + conditions + trip + zeroCondition + " AND dd.number>0" 
                                                                );
           

            SqlParameter[] sqlParemeters = new SqlParameter[2];

            sqlParemeters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParemeters[0].Value = dtFrom;

            sqlParemeters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParemeters[1].Value = dtTo;

            return conn.executeSelectQuery(query, sqlParemeters);
        }

        public DataTable GetAllRooms(DateTime dtFrom, DateTime dtTo, int idCountry, string status, List<int> themeTrip)
        {
            string conditions = "";
            string trip = "";
            if (idCountry != 0)
            {
                conditions = " and c.idCountry = '" + idCountry + "' ";
            }
            if (themeTrip != null)
                if (themeTrip.Count > 0)
                {
                    trip = " and idArrangement IN (SELECT idArrangement FROM ArrangementThemeTrip ";
                    string tripWhere = "";
                    for (int i = 0; i < themeTrip.Count; i++)
                    {

                        if (themeTrip.Count == 1)
                        {
                            tripWhere = tripWhere + " WHERE idThemeTrip = '" + themeTrip[i].ToString() + "')";
                        }
                        else
                        {

                            if (i == 0)
                                tripWhere = tripWhere + " idThemeTrip = '" + themeTrip[i].ToString() + "'";
                            else
                                if (i == themeTrip.Count - 1)
                                    tripWhere = "  WHERE " + tripWhere + " OR idThemeTrip = '" + themeTrip[i].ToString() + "' )";
                                else
                                    tripWhere = tripWhere + " OR idThemeTrip = '" + themeTrip[i].ToString() + "'";
                        }
                    }
                    trip = trip + tripWhere;
                }

            string query = string.Format(@"SELECT  DISTINCT idArticle,ar.nameArtical
                                        FROM ArrangementRooms arr
                                        LEFT OUTER JOIN Artical ar ON ar.codeArtical=arr.idArticle
                                        WHERE idArrangement IN (SELECT idArrangement
							FROM Arrangement a
                            LEFT OUTER JOIN Country c ON a.countryArrangement=c.idCountry  
							WHERE 
							statusArrangement = '" + status+@"' and
							dtToArrangement<=@dtTo and
							dtFromArrangement>=@dtFrom 
							" + conditions+@"
							"+trip+@"
							)");

            SqlParameter[] sqlParemeters = new SqlParameter[2];

            sqlParemeters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParemeters[0].Value = dtFrom;

            sqlParemeters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParemeters[1].Value = dtTo;

            return conn.executeSelectQuery(query, sqlParemeters);
        }

        public DataTable GetAllRoomsWithArticle(DateTime dtFrom, DateTime dtTo, int idCountry, string status, List<int> themeTrip)
        {
            string conditions = "";
            string trip = "";
            if (idCountry != 0)
            {
                conditions = " and au.idCountry = '" + idCountry + "' ";
            }
            if (themeTrip != null)
                if (themeTrip.Count > 0)
                {
                    trip = " and idArrangement IN (SELECT idArrangement FROM ArrangementThemeTrip ";
                    string tripWhere = "";
                    for (int i = 0; i < themeTrip.Count; i++)
                    {

                        if (themeTrip.Count == 1)
                        {
                            tripWhere = tripWhere + " WHERE idThemeTrip = '" + themeTrip[i].ToString() + "')";
                        }
                        else
                        {

                            if (i == 0)
                                tripWhere = tripWhere + " idThemeTrip = '" + themeTrip[i].ToString() + "'";
                            else
                                if (i == themeTrip.Count - 1)
                                    tripWhere = "  WHERE " + tripWhere + " OR idThemeTrip = '" + themeTrip[i].ToString() + "' )";
                                else
                                    tripWhere = tripWhere + " OR idThemeTrip = '" + themeTrip[i].ToString() + "'";
                        }
                    }
                    trip = trip + tripWhere;
                }
            string query = string.Format(@"  SELECT DISTINCT idArticle ,nameArticle 
                                             FROM                                           
                 (SELECT idArticle ,a.nameArtical as nameArticle  ,dtToArrangement,dtFromArrangement,countryArrangement as idCountry,statusArrangement,ap.idArrangement
                 FROM ArrangementPrice ap
                                LEFT OUTER JOIN  CLient cl ON cl.idClient = ap.idClient
                                LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                LEFT OUTER JOIN  ArticalGroups ag  ON a.codeArtikalGroup = ag.codeArticalGroup
                                LEFT OUTER JOIN Arrangement ar ON ar.idArrangement=ap.idArrangement
                                LEFT OUTER JOIN Country c ON ar.countryArrangement=c.idCountry             
                                WHERE  ag.classArticalGroup <> 'Accomod'  
                                UNION
                                SELECT pla.idArticle,a.nameArtical as nameArticle,dtToArrangement,dtFromArrangement,countryArrangement as idCountry,statusArrangement,pl.idArrangement
                                FROM PriceList pl
                                INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                                LEFT OUTER JOIN  CLient cl ON cl.idClient = pl.idClient
                                LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                                LEFT OUTER JOIN  ArticalGroups ag  ON a.codeArtikalGroup = ag.codeArticalGroup
                                LEFT OUTER JOIN Arrangement ar ON ar.idArrangement=pl.idArrangement
                                LEFT OUTER JOIN Country c ON ar.countryArrangement=c.idCountry
                                WHERE  ag.classArticalGroup <> 'Accomod'							
					            ) au
					            WHERE
                                statusArrangement = '" + status+@"' AND
					            dtToArrangement<=@dtTo AND
					            dtFromArrangement>=@dtFrom  
					            "+conditions+@"
                                " + trip + @" ");
					           

            SqlParameter[] sqlParemeters = new SqlParameter[2];

            sqlParemeters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParemeters[0].Value = dtFrom;

            sqlParemeters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParemeters[1].Value = dtTo;

            return conn.executeSelectQuery(query, sqlParemeters);
        }
    }
    
}

