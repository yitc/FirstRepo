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
    public class ArrangementBookDAO
    {
        private dbConnection conn;
        public ArrangementBookDAO()
        {
            conn = new dbConnection();

        }

        public DataTable checkIfArrangementBookIsInInvoice(int idArrangementBook)
        {
            string query = string.Format(@"SELECT invoiceNr FROM Invoice WHERE idVoucher = '" + idArrangementBook + "'");
            return conn.executeSelectQuery(query, null);
        }

        public DataTable checkIfInvoiceIsInAccLine(int invoiceNr)
        {
            string query = string.Format(@"SELECT idAccLine FROM AccLine WHERE invoiceNr = '" + invoiceNr + "'");
            return conn.executeSelectQuery(query, null);
        }

        public DataTable checkFinal(int idArrangementBook)
        {
            string query = string.Format(@"SELECT idArrangementBook 
                                            FROM Arrangement a
                                            LEFT OUTER JOIN ArrangementBook ab ON ab.idArrangement = a.idArrangement
                                            WHERE idStatus='2' AND a.idArrangementBook =  '" + idArrangementBook + "'");
            return conn.executeSelectQuery(query, null);
        }

        public DataTable checkIsInTravelers(int idArrangementBook)
        {
            string query = string.Format(@"SELECT cp.firstname + ' ' +cp.midname + ' ' + cp.lastname as name
                                          FROM ArrangementTravelers at
                                          LEFT OUTER JOIN ContactPerson cp ON at.idTravelWithPerson = cp.idContPers
                                          WHERE idArrangementBook = '" + idArrangementBook + @"'");
            return conn.executeSelectQuery(query, null);
        }

        public DataTable checkIsInTravelersNotPaidFor(int idArrangementBook)
        {
            string query = string.Format(@"SELECT cp.firstname + ' ' +cp.midname + ' ' + cp.lastname as name
                                          FROM ArrangementTravelers at
                                          LEFT OUTER JOIN ContactPerson cp ON at.idTravelWithPerson = cp.idContPers
                                          WHERE idArrangementBook = '" + idArrangementBook + @"'  AND at.idTravelWithPerson NOT IN (SELECT DISTINCT idContPers FROM ArrangementBookPersons WHERE idArrangementBook IN (SELECT DISTINCT idArrangementBook FROM ArrangementBook WHERE idArrangementBook = '" + idArrangementBook + @"'))");
            return conn.executeSelectQuery(query, null);
        }


        public DataTable checkIfPersonsIsExtraAndStatus(int idArrangementBook, int idArrangement)
        {
            string query = string.Format(@"SELECT idStatus,cp.firstname + ' ' + cp.lastname as nameContPers
                                           FROM Arrangement a
                                           LEFT OUTER JOIN ArrangementBook ab ON ab.idArrangement = a.idArrangement
                                           LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = ab.idContPers
                                           WHERE ab.idArrangementBook IN (SELECT abp.idArrangementBook FROM ArrangementBookPersons abp 
                                           LEFT OUTER JOIN ArrangementBook ab ON abp.idArrangementBook = ab.idArrangementBook
                                           WHERE ab.idArrangement = '" + idArrangement + @"' AND abp.idContPers  IN 
                                           (SELECT DISTINCT idContPers FROM ArrangementBook WHERE idArrangementBook= '" + idArrangementBook + "') AND idStatus <> '4' )  AND ab.idArrangement = '" + idArrangement + "'");
            return conn.executeSelectQuery(query, null);
        }

        public DataTable checkIfPersonsHasExtraAndStatus(int idArrangementBook)
        {
            string query = string.Format(@"SELECT idStatus,cp.firstname + ' ' + cp.lastname as nameContPers
                                           FROM Arrangement a
                                           LEFT OUTER JOIN ArrangementBook ab ON ab.idArrangement = a.idArrangement
                                           LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = ab.idContPers
                                           WHERE ab.idArrangementBook IN (SELECT abp.idArrangementBook FROM ArrangementBookPersons abp 
                                          )  AND ab.idArrangementBook =  '" + idArrangementBook + "'");
            return conn.executeSelectQuery(query, null);
        }



        public DataTable GetIdArrangementBookByIdBookArrangement(int idArrangementBook)
        {
            string query = string.Format(@"SELECT idArrangementBook FROM ArrangementBook WHERE idArrangementBook = '" + idArrangementBook + "' ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetIdArrangementBook(int idArrangement, int idContPers)
        {
            string query = string.Format(@"SELECT idArrangementBook FROM ArrangementBook WHERE idArrangement = '" + idArrangement + "' AND idContPers = '" + idContPers + "' AND idStatus <> '4'");

            return conn.executeSelectQuery(query, null);
        }

      


        public DataTable GetArrangementBookBoardingPoint(int idArrangementBook, int idArrangement, int idContPers)
        {
            string query = string.Format(@"SELECT idBoarding FROM ArrangementBook WHERE idArrangementBook = '" + idArrangementBook + "' AND idArrangement = '" + idArrangement + "' AND idContPers = '" + idContPers + "'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetArrangementBook(int idArrangementBook)
        {
            string query = string.Format(@"SELECT idArrangementBook,idArrangement,idContPers, idDebitor, typeDebitor, idStatus,idTravelPapers,price, isInsurance, isCancelInsurance,idBoarding,idUserCreated, dtUserCreated, idUserModified, dtUserModified, isMedicalDevices,idPayInvoice FROM ArrangementBook WHERE idArrangementBook = '" + idArrangementBook + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetArrangementBookForTraveler(int idArrangement, int idContPers)
        {
            string query = string.Format(@"SELECT idArrangementBook,idArrangement,idContPers, idDebitor, typeDebitor,idStatus,idTravelPapers,price, isInsurance, isCancelInsurance,idBoarding,idUserCreated, dtUserCreated, idUserModified, dtUserModified, isMedicalDevices,idPayInvoice FROM ArrangementBook WHERE idArrangement = '" + idArrangement.ToString() + "' and idContPers = '" + idContPers.ToString() + "'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetArrangementArticles(int idArrangement, int idArrangementBook)
        {
            string query = string.Format(@"SELECT ap.idArticle,a.nameArtical as nameArticle,1 as number,idArrangementPrice as id,'false' as isContract,'No contract' as nameContract ,ap.idClient,c.nameClient,aba.idRoom,ap.dtFrom,ap.dtTo,ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal
                                           ,aba.idUserCreated, aba.dtUserCreated, aba.idUserModified,aba.dtUserModified

                                           FROM ArrangementPrice ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           INNER JOIN ArrangementBookArticles aba ON aba.idArticle = a.codeArtical AND id = ap.idArrangementPrice
                                           WHERE ap.idArrangement ='" + idArrangement + @"' AND aba.idArrangementBook ='" + idArrangementBook + @"' AND aba.isContract = 'false'
                                           UNION     
                                           SELECT pla.idArticle,a.nameArtical as nameArticle,1 as number,pla.idPriceListArticle as id,'true' as isContract,'Contract' as nameContract,pl.idClient,c.nameClient,aba.idRoom,pla.dtFrom,pla.dtTo,pla.pricePerArticle,pla.pricePerQuantity,pla.priceTotal
                                            ,aba.idUserCreated, aba.dtUserCreated, aba.idUserModified,aba.dtUserModified                                           
                                            FROM PriceList pl
                                           INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                                           LEFT OUTER JOIN  CLient c ON c.idClient = pl.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                                           INNER JOIN ArrangementBookArticles aba ON aba.idArticle = a.codeArtical AND id = pla.idPriceListArticle
                                           WHERE pl.idArrangement = '" + idArrangement + @"' AND aba.idArrangementBook ='" + idArrangementBook + @"' AND aba.isContract = 'true' 
                                           UNION
                                           SELECT aip.idArticle,a.nameArtical as nameArticle,1 as number,'' as id,'true' as isContract,'' as nameContract,'' as idClient,'' as nameClient,aba.idRoom,'' as dtFrom,'' as dtTo,0 as pricePerArticle,0 as pricePerQuantity,aip.purchasePriceTotal as priceTotal
                                            ,aba.idUserCreated, aba.dtUserCreated, aba.idUserModified,aba.dtUserModified                                               
                                            FROM ArrangementInvoicePrice aip
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = aip.idArticle
                                           INNER JOIN ArrangementBookArticles aba ON aba.idArticle = aip.idArticle
                                           WHERE aba.idArrangementBook ='" + idArrangementBook + @"' and aip.isExtra = 0");

            return conn.executeSelectQuery(query, null);
        }


        //==== medical count for 4 fields =========================
        public DataTable GetBookPersMedic(List<int> idAns, int idArrangement)  // left join [ArrangementBookPersons] c on c.idContPers = a.idContPers
        {
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
                 string query = string.Format(@" Select Count(a.idContpers) as num from ContactPerson a
                                             Left join MedCpr b on a.idContPers = b.idcpr 
                                             left join ArrangementBook d on d.idCOntPers = a.idContPers
                                             where  d.idArrangement ='" + idArrangement.ToString() + @"' and (d.idstatus = 1 OR d.idStatus=2) and " + ans + "");

                 return conn.executeSelectQuery(query, null);
             }
             else
                 return null;
        }
        public DataTable GetBookPersMedicMoreAns(List<int> idAns, int idArrangement)  
        {
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


                string query = string.Format(@"SELECT COUNT(num)as num FROM ( Select DISTINCT a.idContpers as num from ContactPerson a
                                             Left join MedCpr b on a.idContPers = b.idcpr 
                                             left join ArrangementBook d on d.idCOntPers = a.idContPers
                                             where  d.idArrangement ='" + idArrangement.ToString() + @"' and (d.idstatus = 1 OR d.idStatus=2) and "+ans+") d");

                return conn.executeSelectQuery(query, null);
            }
            else
                return null;
        }
        public DataTable GetBookPersMedicPers(List<int> idAns, int idPerson)
        {

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
                string query = string.Format(@" Select a.idcontPers as idcpr from ContactPerson a
                                             Left join MedCpr b on a.idContPers = b.idcpr 
                                             where a.idContPers = '" + idPerson.ToString() + "' and " + ans + "");
                // where idAns = '" + idAns.ToString() + @"'  and d.idArrangement ='" + idArrangement.ToString() + @"' and (d.idstatus = 1 OR d.idStatus=2) and  a.idContPers ='" + idPerson.ToString() + "' ");

                return conn.executeSelectQuery(query, null);
            }
            else
                return null;
        }
        //================================================
        public DataTable GetArticlesNumber(int idArrangement, Boolean isContract, int id, string idArticle)
        {
            string query = string.Format(@"SELECT number FROM
                                          (SELECT ap.idArrangement,idArticle,a.nameArtical as nameArticle,nrArticle*a.quantity -(SELECT COUNT(idArticle)
                                           FROM ArrangementBook ab
                                           INNER JOIN ArrangementBookArticles aba ON ab.idArrangementBook=aba.idArrangementBook
                                           WHERE idArrangement = '" + idArrangement + @"' AND isContract = '0' AND id = ap.idArrangementPrice) as number,idArrangementPrice as id,'false' as isContract,ap.idClient,c.nameClient,ap.dtFrom,ap.dtTo,ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal
                                           FROM ArrangementPrice ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           WHERE idArrangement = '" + idArrangement + @"'
                                           UNION     
                                           SELECT pl.idArrangement,pla.idArticle,a.nameArtical as nameArticle,pla.nrArticle*a.quantity-(SELECT COUNT(idArticle)
                                           FROM ArrangementBook ab
                                           INNER JOIN ArrangementBookArticles aba ON ab.idArrangementBook=aba.idArrangementBook
                                           WHERE idArrangement = '" + idArrangement + @"' AND isContract = '1' AND id = pla.idPriceListArticle) as number,pla.idPriceListArticle as id,'true' as isContract,pl.idClient,c.nameClient,pla.dtFrom,pla.dtTo,pla.pricePerArticle,pla.pricePerQuantity,pla.priceTotal
                                           FROM PriceList pl
                                           INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                                           LEFT OUTER JOIN  CLient c ON c.idClient = pl.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                                           WHERE pl.idArrangement = '" + idArrangement + @"') DD
                                           WHERE idArrangement = '" + idArrangement + @"' AND isContract = '" + isContract + @"' AND idArticle =  '" + idArticle + @"' AND id =  '" + id + @"'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetNrTravelerByGender(int idGender, int idArrangement)
        {
            string query = string.Format(@"SELECT COUNT (a.idContPers) as num
                                          FROM ArrangementBook a
                                          LEFT OUTER JOIN ContactPerson cpr ON cpr.idContPers = a.idContPers
                                          LEFT OUTER JOIN Gender g ON cpr.idGender = g.idGender
                                          WHERE a.idStatus <> '3' AND a.idStatus <> '4' AND cpr.idGender = '" + idGender + @"' AND idArrangement = '" + idArrangement + @"' AND a.idContPers NOT IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4')");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetNrBookedTraveler(int idArrangement)
        {
            string query = string.Format(@"SELECT COUNT (a.idContPers) as num
                                          FROM ArrangementBook a
                                          WHERE a.idStatus <> '3' AND a.idStatus <> '4' AND idArrangement = '" + idArrangement + @"' AND a.idContPers NOT IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4')");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable checkCancelInInvoiseFinal(int idArrangement, int idContPers)
        {
            string query = string.Format(@"SELECT COUNT (i.idInvoice) as num
                                          FROM Invoice i
                                          WHERE idVoucher IN (SELECT DISTINCT idArrangementBook FROM ArrangementBook WHERE idArrangement = '" + idArrangement + "' AND idContPers = '" + idContPers + "')");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetArrangementPriceListArticles(int idArrangement)
        {
            string query = string.Format(@"SELECT idArticle,a.nameArtical as nameArticle,nrArticle*a.quantity -(SELECT COUNT(idArticle)
                                           FROM ArrangementBook ab
                                           INNER JOIN ArrangementBookArticles aba ON ab.idArrangementBook=aba.idArrangementBook
                                           WHERE idArrangement = '"+idArrangement+@"' AND isContract = '0' AND id = ap.idArrangementPrice) as number,idArrangementPrice as id,'false' as isContract,ap.idClient,c.nameClient,ap.dtFrom,ap.dtTo,ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal
                                           FROM ArrangementPrice ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           WHERE idArrangement = '" + idArrangement + @"'
                                           UNION     
                                           SELECT pla.idArticle,a.nameArtical as nameArticle,pla.nrArticle*a.quantity-(SELECT COUNT(idArticle)
                                           FROM ArrangementBook ab
                                           INNER JOIN ArrangementBookArticles aba ON ab.idArrangementBook=aba.idArrangementBook
                                           WHERE idArrangement = '" + idArrangement + @"' AND isContract = '1' AND id = pla.idPriceListArticle) as number,pla.idPriceListArticle as id,'true' as isContract,pl.idClient,c.nameClient,pla.dtFrom,pla.dtTo,pla.pricePerArticle,pla.pricePerQuantity,pla.priceTotal
                                           FROM PriceList pl
                                           INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                                           LEFT OUTER JOIN  CLient c ON c.idClient = pl.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                                           WHERE pl.idArrangement = '" + idArrangement + @"'");

            return conn.executeSelectQuery(query, null);
        }
        public int Save(ArrangementBookModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO ArrangementBook (idArrangement,idContPers, idDebitor, typeDebitor, idStatus,idTravelPapers,price,dtBooked, isInsurance, idBoarding,isCancelInsurance, idUserCreated, dtUserCreated, idUserModified, dtUserModified, isMedicalDevices,idPayInvoice) 
                      VALUES(@idArrangement,@idContPers,@idDebitor,@typeDebitor,@idStatus,@idTravelPapers,@price,@dtBooked, @isInsurance, @idBoarding,@isCancelInsurance,@idUserCreated, @dtUserCreated, @idUserModified, @dtUserModified, @isMedicalDevices,@idPayInvoice);SELECT SCOPE_IDENTITY();");


            SqlParameter[] sqlParameter = new SqlParameter[17];
            sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[0].Value = model.idArrangement;

            sqlParameter[1] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[1].Value = model.idContPers;

            sqlParameter[2] = new SqlParameter("@idStatus", SqlDbType.Int);
            sqlParameter[2].Value = model.idStatus;

            sqlParameter[3] = new SqlParameter("@idTravelPapers", SqlDbType.Int);
            sqlParameter[3].Value = model.idTravelPapers;

            sqlParameter[4] = new SqlParameter("@price", SqlDbType.Decimal);
            sqlParameter[4].Value = model.price;

            sqlParameter[5] = new SqlParameter("@dtBooked", SqlDbType.DateTime);
            sqlParameter[5].Value = model.dtBooked;

            sqlParameter[6] = new SqlParameter("@isInsurance", SqlDbType.Bit);
            sqlParameter[6].Value = model.isInsurance;

            sqlParameter[7] = new SqlParameter("@idBoarding", SqlDbType.Int);
            sqlParameter[7].Value = model.idBoarding;

            sqlParameter[8] = new SqlParameter("@isCancelInsurance", SqlDbType.Bit);
            sqlParameter[8].Value = model.isCancelInsurance;

            sqlParameter[9] = new SqlParameter("@idUserCreated", SqlDbType.Int);
            sqlParameter[9].Value = model.idUserCreated;

            sqlParameter[10] = new SqlParameter("@dtUserCreated", SqlDbType.DateTime);
            sqlParameter[10].Value = model.dtUserCreated;

            sqlParameter[11] = new SqlParameter("@idUserModified", SqlDbType.Int);
            sqlParameter[11].Value = model.idUserModified;

            sqlParameter[12] = new SqlParameter("@dtUserModified", SqlDbType.DateTime);
            sqlParameter[12].Value = model.dtUserModified;

            sqlParameter[13] = new SqlParameter("@isMedicalDevices", SqlDbType.Bit);
            sqlParameter[13].Value = model.isMedicalDevices;

            sqlParameter[14] = new SqlParameter("@idPayInvoice", SqlDbType.Int);
            sqlParameter[14].Value = model.idPayInvoice;

            sqlParameter[15] = new SqlParameter("@idDebitor", SqlDbType.Int);
            sqlParameter[15].Value = model.idDebitor;

            sqlParameter[16] = new SqlParameter("@typeDebitor", SqlDbType.NVarChar);
            sqlParameter[16].Value = model.typeDebitor;

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
            sqlParameter[4].Value = conn.GetLastTableID("ArrangementBook") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangementBook";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementBook";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            return conn.executQueryTransactionSelectLastID(_query, sqlParameters);
        }
        public Boolean UpdateBoardingPoint(int idArrangementBook, int idBoarding, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE ArrangementBook SET  idBoarding = @idBoarding
                                        WHERE  idArrangementBook = @idArrangementBook");

            SqlParameter[] sqlParameter = new SqlParameter[2];


            sqlParameter[0] = new SqlParameter("@idArrangementBook", SqlDbType.Int);
            sqlParameter[0].Value = idArrangementBook;


            sqlParameter[1] = new SqlParameter("@idBoarding", SqlDbType.Int);
            sqlParameter[1].Value = idBoarding;

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
            sqlParameter[4].Value = idArrangementBook;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangementBook";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementBook";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update boarding point";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
        public Boolean Update(ArrangementBookModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE ArrangementBook SET idArrangement = @idArrangement,idContPers = @idContPers,idStatus = @idStatus,idTravelPapers = @idTravelPapers 
                                        ,price = @price, dtBooked=@dtBooked, isInsurance=@isInsurance, idBoarding = @idBoarding, isCancelInsurance=@isCancelInsurance 
                                        , idUserModified = @idUserModified, dtUserModified = @dtUserModified , isMedicalDevices = @isMedicalDevices, idPayInvoice=@idPayInvoice
                                        , idDebitor = @idDebitor, typeDebitor = @typeDebitor 
                                        WHERE  idArrangementBook = @idArrangementBook");


            SqlParameter[] sqlParameter = new SqlParameter[16];
            sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[0].Value = model.idArrangement;

            sqlParameter[1] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[1].Value = model.idContPers;

            sqlParameter[2] = new SqlParameter("@idStatus", SqlDbType.Int);
            sqlParameter[2].Value = model.idStatus;

            sqlParameter[3] = new SqlParameter("@idTravelPapers", SqlDbType.Int);
            sqlParameter[3].Value = model.idTravelPapers;

            sqlParameter[4] = new SqlParameter("@price", SqlDbType.Decimal);
            sqlParameter[4].Value = model.price;

            sqlParameter[5] = new SqlParameter("@idArrangementBook", SqlDbType.Int);
            sqlParameter[5].Value = model.idArrangementBook;

            sqlParameter[6] = new SqlParameter("@dtBooked", SqlDbType.DateTime);
            sqlParameter[6].Value = model.dtBooked;

            sqlParameter[7] = new SqlParameter("@isInsurance", SqlDbType.Bit);
            sqlParameter[7].Value = model.isInsurance;

            sqlParameter[8] = new SqlParameter("@idBoarding", SqlDbType.Int);
            sqlParameter[8].Value = model.idBoarding;

            sqlParameter[9] = new SqlParameter("@isCancelInsurance", SqlDbType.Bit);
            sqlParameter[9].Value = model.isCancelInsurance;
      
            sqlParameter[10] = new SqlParameter("@idUserModified", SqlDbType.Int);
            sqlParameter[10].Value = model.idUserModified;

            sqlParameter[11] = new SqlParameter("@dtUserModified", SqlDbType.DateTime);
            sqlParameter[11].Value = model.dtUserModified;

            sqlParameter[12] = new SqlParameter("@isMedicalDevices", SqlDbType.Bit);
            sqlParameter[12].Value = model.isMedicalDevices;

            sqlParameter[13] = new SqlParameter("@idPayInvoice", SqlDbType.Int);
            sqlParameter[13].Value = model.idPayInvoice;

            sqlParameter[14] = new SqlParameter("@idDebitor", SqlDbType.Int);
            sqlParameter[14].Value = model.idDebitor;

            sqlParameter[15] = new SqlParameter("@typeDebitor", SqlDbType.NVarChar);
            sqlParameter[15].Value = model.typeDebitor;

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
            sqlParameter[4].Value = model.idArrangementBook;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangementBook";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementBook";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean UpdateDebitor(ArrangementBookModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE ArrangementBook SET 
                                idDebitor = @idDebitor, typeDebitor = @typeDebitor 
                                WHERE  idArrangementBook = @idArrangementBook");

            SqlParameter[] sqlParameter = new SqlParameter[3];

            sqlParameter[0] = new SqlParameter("@idDebitor", SqlDbType.Int);
            sqlParameter[0].Value = model.idDebitor;

            sqlParameter[1] = new SqlParameter("@typeDebitor", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.typeDebitor;
            
            sqlParameter[2] = new SqlParameter("@idArrangementBook", SqlDbType.Int);
            sqlParameter[2].Value = model.idArrangementBook;


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
            sqlParameter[4].Value = model.idArrangementBook;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangementBook";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementBook";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update debitor";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean Delete(int idArrangementBook, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE ArrangementBookPersons  WHERE idArrangementBook = @idArrangementBook");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idArrangementBook", SqlDbType.Int);
            sqlParameter[0].Value = idArrangementBook;


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
            sqlParameter[4].Value = idArrangementBook.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangementBook";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementBookPersons";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean DeleteExtraArticles(int idArrangementBook, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE ArrangementBookArticles  WHERE idArrangementBook = @idArrangementBook");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idArrangementBook", SqlDbType.Int);
            sqlParameter[0].Value = idArrangementBook;


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
            sqlParameter[4].Value = idArrangementBook.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangementBook";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementBookArticles";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete extra articles";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean DeleteBookingIfNotFinal(int idArrangementBook, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM ArrangementBookArticles WHERE idArrangementBook= @idArrangementBook");
           

            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idArrangementBook", SqlDbType.Int);
            sqlParameter[0].Value = idArrangementBook;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"DELETE FROM ArrangementBookOptionalArticles
                    WHERE idArrangementBook = @idArrangementBook");

            sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idArrangementBook", SqlDbType.Int);
            sqlParameter[0].Value = idArrangementBook;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"DELETE FROM ArrangementBook  WHERE idArrangementBook= @idArrangementBook");

            sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idArrangementBook", SqlDbType.Int);
            sqlParameter[0].Value = idArrangementBook;


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
            sqlParameter[4].Value = idArrangementBook.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangementBook";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementBookArticles";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete booking if not final";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean SavePersons(int idArrangementBook, int idContPers, int idUserCreated, DateTime dtUserCreated, int idUserModified, DateTime dtUserModified, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO ArrangementBookPersons (idArrangementBook,idContPers, idUserCreated, dtUserCreated, idUserModified, dtUserModified) 
                      VALUES(@idArrangementBook,@idContPers, @idUserCreated, @dtUserCreated, @idUserModified, @dtUserModified)");


            SqlParameter[] sqlParameter = new SqlParameter[6];
            sqlParameter[0] = new SqlParameter("@idArrangementBook", SqlDbType.Int);
            sqlParameter[0].Value = idArrangementBook;

            sqlParameter[1] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[1].Value = idContPers;

            sqlParameter[2] = new SqlParameter("@idUserCreated", SqlDbType.Int);
            sqlParameter[2].Value = idUserCreated;

            sqlParameter[3] = new SqlParameter("@dtUserCreated", SqlDbType.DateTime);
            sqlParameter[3].Value = dtUserCreated;

            sqlParameter[4] = new SqlParameter("@idUserModified", SqlDbType.Int);
            sqlParameter[4].Value = idUserModified;

            sqlParameter[5] = new SqlParameter("@dtUserModified", SqlDbType.DateTime);
            sqlParameter[5].Value = dtUserModified;

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
            sqlParameter[4].Value = idArrangementBook.ToString() + "_" + idContPers.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangementBook_idContPers";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementBookPersons";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save persons";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean SaveArticles(int idArrangementBook, string idArticle, Boolean isContract, int id, string idRoom, int idUserCreated, DateTime dtUserCreated, int idUserModified, DateTime dtUserModified, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO ArrangementBookArticles (idArrangementBook,idArticle,isContract,id, idRoom, idUserCreated, dtUserCreated, idUserModified, dtUserModified) 
                      VALUES(@idArrangementBook,@idArticle,@isContract,@id, @idRoom, @idUserCreated, @dtUserCreated, @idUserModified, @dtUserModified)");


            SqlParameter[] sqlParameter = new SqlParameter[9];
            sqlParameter[0] = new SqlParameter("@idArrangementBook", SqlDbType.Int);
            sqlParameter[0].Value = idArrangementBook;

            sqlParameter[1] = new SqlParameter("@idArticle", SqlDbType.NVarChar);
            sqlParameter[1].Value = idArticle;

            sqlParameter[2] = new SqlParameter("@isContract", SqlDbType.Bit);
            sqlParameter[2].Value = isContract;

            sqlParameter[3] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[3].Value = id;

            sqlParameter[4] = new SqlParameter("@idRoom", SqlDbType.NVarChar);
            sqlParameter[4].Value = idRoom;

            sqlParameter[5] = new SqlParameter("@idUserCreated", SqlDbType.Int);
            sqlParameter[5].Value = idUserCreated;

            sqlParameter[6] = new SqlParameter("@dtUserCreated", SqlDbType.DateTime);
            sqlParameter[6].Value = dtUserCreated;

            sqlParameter[7] = new SqlParameter("@idUserModified", SqlDbType.Int);
            sqlParameter[7].Value = idUserModified;

            sqlParameter[8] = new SqlParameter("@dtUserModified", SqlDbType.DateTime);
            sqlParameter[8].Value = dtUserModified;



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
            sqlParameter[4].Value = idArrangementBook.ToString() + "_" + isContract.ToString() + "_" + id.ToString() + "_" + idArticle + "_" + idRoom;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangementBook_isContract_id_idArticle_idRoom";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementBookArticles";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save articles";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
        public bool CancelArrangement(int idArrangementBook, DateTime dt)
        {

            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = "";
            SqlParameter[] sqlParameter;

            query = string.Format(@"DELETE FROM ArrangementBookArticles
                    WHERE idArrangementBook IN (SELECT DISTINCT idArrangementBook FROM ArrangementBook WHERE idArrangementBook= @idArrangementBook)");

            sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idArrangementBook", SqlDbType.Int);
            sqlParameter[0].Value = idArrangementBook;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE FROM ArrangementBookOptionalArticles
                    WHERE idArrangementBook IN (SELECT DISTINCT idArrangementBook FROM ArrangementBook WHERE idArrangementBook= @idArrangementBook)");

            sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idArrangementBook", SqlDbType.Int);
            sqlParameter[0].Value = idArrangementBook;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE at FROM  ArrangementTravelers at LEFT JOIN ArrangementBook ab ON ab.idContPers = at.idContPers AND
                                    ab.idArrangement = at.idArrangement WHERE ab.idArrangementBook = @idArrangementBook ");
            sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idArrangementBook", SqlDbType.Int);
            sqlParameter[0].Value = idArrangementBook;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"UPDATE ArrangementBook SET idStatus = '4', dtStatus = @dtStatus WHERE idArrangementBook = @idArrangementBook");
            sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idArrangementBook", SqlDbType.Int);
            sqlParameter[0].Value = idArrangementBook;
            sqlParameter[1] = new SqlParameter("@dtStatus", SqlDbType.DateTime);
            sqlParameter[1].Value = dt;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            query = string.Format(@"DELETE vl FROM VolLookup vl LEFT JOIN ArrangementBook ab ON ab.idContPers = vl.idContPers AND
                                    ab.idArrangement = vl.idArrangement WHERE ab.idArrangementBook = @idArrangementBook");
            sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idArrangementBook", SqlDbType.Int);
            sqlParameter[0].Value = idArrangementBook;

            return conn.executQueryTransaction(_query, sqlParameters);
        }

//        public bool CancelArrangement(int idContPers, int idArrangement)
//        {
//            List<string> _query = new List<string>();
//            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

//            string query = "";
//            SqlParameter[] sqlParameter;

//            query = string.Format(@"DELETE FROM ArrangementBookArticles
//                    WHERE idArrangementBook IN (SELECT DISTINCT idArrangementBook FROM ArrangementBook WHERE idArrangement= @idArrangement AND idContPers = @idContPers)");
            
//            sqlParameter = new SqlParameter[2];
//            sqlParameter[0] = new SqlParameter("@idContPers", SqlDbType.Int);
//            sqlParameter[0].Value = idContPers;
//            sqlParameter[1] = new SqlParameter("@idArrangement", SqlDbType.Int);
//            sqlParameter[1].Value = idArrangement;

//            _query.Add(query);
//            sqlParameters.Add(sqlParameter);

//            query = string.Format(@"DELETE FROM  ArrangementTravelers WHERE idContPers = @idContPers AND idArrangement = @idArrangement");
//            sqlParameter = new SqlParameter[2];
//            sqlParameter[0] = new SqlParameter("@idContPers", SqlDbType.Int);
//            sqlParameter[0].Value = idContPers;
//            sqlParameter[1] = new SqlParameter("@idArrangement", SqlDbType.Int);
//            sqlParameter[1].Value = idArrangement;

//            _query.Add(query);
//            sqlParameters.Add(sqlParameter);

//            query = string.Format(@"UPDATE ArrangementBook SET idStatus = '4' WHERE idContPers = @idContPers AND idArrangement = @idArrangement");
//            sqlParameter = new SqlParameter[2];
//            sqlParameter[0] = new SqlParameter("@idContPers", SqlDbType.Int);
//            sqlParameter[0].Value = idContPers;
//            sqlParameter[1] = new SqlParameter("@idArrangement", SqlDbType.Int);
//            sqlParameter[1].Value = idArrangement;

//            _query.Add(query);
//            sqlParameters.Add(sqlParameter);

           

//            return conn.executQueryTransaction(_query, sqlParameters);
//        }


        public Boolean SaveVolLookup(int idArrangement, int idContPers, int id, string type, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO VolLookup (idArrangement, idContPers, id, type) 
                      VALUES(@idArrangement, @idContPers, @id, @type)");


            SqlParameter[] sqlParameter = new SqlParameter[4];
            sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[0].Value = idArrangement;

            sqlParameter[1] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[1].Value = idContPers;

            sqlParameter[2] = new SqlParameter("@id", SqlDbType.Int);
            sqlParameter[2].Value = id;

            sqlParameter[3] = new SqlParameter("@type", SqlDbType.NVarChar);
            sqlParameter[3].Value = type;

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
            sqlParameter[4].Value = idArrangement.ToString() + "_" + idContPers.ToString() + "_" + id.ToString() + "_" + type;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idClient_idContPers_id_type";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolLookup";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save vol lookup";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
        public Boolean DeleteVolLookup(int idArrangement, int idContPers, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM VolLookup WHERE idArrangement = @idArrangement AND idContPers = @idContPers");


            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[0].Value = idArrangement;

            sqlParameter[1] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[1].Value = idContPers;

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
            sqlParameter[4].Value = idArrangement.ToString() + "_" + idContPers.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangement_idContPers";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolLookup";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete vol lookup";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

//        public DataTable GetFunctionsForArrangement(int idArrangement)
//        {
//            string query = string.Format(@"SELECT ISNULL(vfq.txtQuest,'') as quest,ISNULL(vfa.txt,'0') as number,ISNULL(nn.number,'0') as numberBooked
//                                  FROM VolQuest vfq
//                                  Inner join (SELECT DISTINCT idQuest,CAST(txt AS NVARCHAR(100)) as txt FROM VolArr where idArr = @idArr) vfa 
//                                  ON vfa.idQuest = vfq.idQuest
//                                  LEFT OUTER JOIN (SELECT CAST(vfq2.txtQuest AS NVARCHAR(100)) as quest,vfa.idQuest, count(vfa.idQuest) as number
//                                  FROM VolArr vfa
//                                  INNER JOIN VolLookup vl ON vfa.idQuest = vl.id
//                                  INNER JOIN VolQuest vfq2 ON vfa.idQuest = vfq2.idQuest
//                                  WHERE vfa.idArr = @idArr and vl.type = 'S'
//                                  group by CAST(vfq2.txtQuest AS NVARCHAR(100)),vfa.idQuest) nn ON nn.idQuest = vfq.idQuest");


//            SqlParameter[] sqlParameters = new SqlParameter[1];
//            sqlParameters[0] = new SqlParameter("@idArr", SqlDbType.Int);
//            sqlParameters[0].Value = idArrangement;

//            return conn.executeSelectQuery(query, sqlParameters);
//        }

//        public DataTable GetSkillsForArrangement(int idArrangement)
//        {
//            string query = string.Format(@"SELECT ISNULL(vfq.txtQuest,'') as quest,ISNULL(vfa.txt,'0') as number,ISNULL(nn.number,'0') as numberBooked
//                                  FROM VolFunctionQuest vfq
//                                  Inner join (SELECT DISTINCT idQuest,CAST(txt AS NVARCHAR(100)) as txt FROM VolFunctionArr where idArrangement = @idArrangement) vfa 
//                                  ON vfa.idQuest = vfq.idQuest
//                                  LEFT OUTER JOIN (SELECT CAST(vfq2.txtQuest AS NVARCHAR(100)) as quest,vfa.idQuest, count(vfa.idQuest) as number
//                                  FROM VolFunctionArr vfa
//                                  INNER JOIN VolLookup vl ON vfa.idQuest = vl.id
//                                  INNER JOIN VolFunctionQuest vfq2 ON vfa.idQuest = vfq2.idQuest
//                                  WHERE vfa.idArrangement = @idArrangement and vl.type = 'F'
//                                  group by CAST(vfq2.txtQuest AS NVARCHAR(100)),vfa.idQuest) nn ON nn.idQuest = vfq.idQuest");


//            SqlParameter[] sqlParameters = new SqlParameter[1];
//            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
//            sqlParameters[0].Value = idArrangement;

//            return conn.executeSelectQuery(query, sqlParameters);
//        }       
        // Neta 2.12.

//        public DataTable GetSkillsForArrangement(int idArrangement)
//        {
//            string query = string.Format(@"SELECT vfq.idQuest as ID,ISNULL(vfq.txtQuest,'') as quest,ISNULL(vfa.txt,'0') as number,ISNULL(nn.number,'0') as numberBooked
//                                  FROM VolQuest vfq
//                                  Inner join (SELECT DISTINCT idQuest,CAST(txt AS NVARCHAR(100)) as txt FROM VolArr where idArr = @idArr) vfa 
//                                  ON vfa.idQuest = vfq.idQuest
//                                  LEFT OUTER JOIN (SELECT CAST(vfq2.txtQuest AS NVARCHAR(100)) as quest,vfa.idQuest, count(vfa.idQuest) as number
//                                  FROM VolArr vfa
//                                  INNER JOIN VolLookup vl ON vfa.idQuest = vl.id
//                                  INNER JOIN VolQuest vfq2 ON vfa.idQuest = vfq2.idQuest
//                                  WHERE vfa.idArr = @idArr and vl.type = 'S'
//                                  group by CAST(vfq2.txtQuest AS NVARCHAR(100)),vfa.idQuest) nn ON nn.idQuest = vfq.idQuest");


//            SqlParameter[] sqlParameters = new SqlParameter[1];
//            sqlParameters[0] = new SqlParameter("@idArr", SqlDbType.Int);
//            sqlParameters[0].Value = idArrangement;

//            return conn.executeSelectQuery(query, sqlParameters);
//        }
        // Neta
//        public DataTable GetFunctionsForArrangement(int idArrangement)
//        {
//            string query = string.Format(@"SELECT vfq.idQuest as ID,ISNULL(vfq.txtQuest,'') as quest,ISNULL(vfa.txt,'0') as number,ISNULL(nn.number,'0') as numberBooked
//                                  FROM VolFunctionQuest vfq
//                                  Inner join (SELECT DISTINCT idQuest,CAST(txt AS NVARCHAR(100)) as txt FROM VolFunctionArr where idArrangement = @idArrangement) vfa 
//                                  ON vfa.idQuest = vfq.idQuest
//                                  LEFT OUTER JOIN (SELECT CAST(vfq2.txtQuest AS NVARCHAR(100)) as quest,vfa.idQuest, count(vfa.idQuest) as number
//                                  FROM VolFunctionArr vfa
//                                  INNER JOIN VolLookup vl ON vfa.idQuest = vl.id
//                                  INNER JOIN VolFunctionQuest vfq2 ON vfa.idQuest = vfq2.idQuest
//                                  WHERE vfa.idArrangement = @idArrangement and vl.type = 'F'
//                                  group by CAST(vfq2.txtQuest AS NVARCHAR(100)),vfa.idQuest) nn ON nn.idQuest = vfq.idQuest");


//            SqlParameter[] sqlParameters = new SqlParameter[1];
//            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
//            sqlParameters[0].Value = idArrangement;

//            return conn.executeSelectQuery(query, sqlParameters);
//        }

        public DataTable GetSelectedSkillsOrFunctionsForArrangement(string type, int idArrangement)
        {
            string query = string.Format(@"SELECT ab.idContPers,vl.id FROM ArrangementBook ab
                                           INNER JOIN VolLookup vl ON ab.idContPers = vl.idContPers
                                           WHERE ab.idArrangement = @idArrangement AND type = @type");


            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;
            sqlParameters[1] = new SqlParameter("@type", SqlDbType.NVarChar);
            sqlParameters[1].Value = type;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        //Shimmy 10.12
//        public DataTable GetSkillsForArrangement(int idArrangement)
//        {
//            string query = string.Format(@"SELECT ISNULL(vfq.idQuest, '0') as ID,ISNULL(vfq.txtQuest,'') as quest,ISNULL(vfa.txt,'1') as number,ISNULL(nn.number,'0') as numberBooked
//                                  FROM VolQuest vfq
//                                  Inner join (SELECT DISTINCT idQuest,CAST(txt AS NVARCHAR(100)) as txt FROM VolArr where idArr = @idArr) vfa 
//                                  ON vfa.idQuest = vfq.idQuest
//                                  LEFT OUTER JOIN (SELECT CAST(vfq2.txtQuest AS NVARCHAR(100)) as quest,vfa.idQuest, count(vfa.idQuest) as number
//                                  FROM VolArr vfa
//                                  INNER JOIN VolLookup vl ON vfa.idQuest = vl.id
//                                  INNER JOIN VolQuest vfq2 ON vfa.idQuest = vfq2.idQuest
//                                  WHERE vfa.idArr = @idArr and vl.type = 'S'
//                                  group by CAST(vfq2.txtQuest AS NVARCHAR(100)),vfa.idQuest) nn ON nn.idQuest = vfq.idQuest");


//            SqlParameter[] sqlParameters = new SqlParameter[1];
//            sqlParameters[0] = new SqlParameter("@idArr", SqlDbType.Int);
//            sqlParameters[0].Value = idArrangement;

//            return conn.executeSelectQuery(query, sqlParameters);
//        }

//        public DataTable GetFunctionsForArrangement(int idArrangement)
//        {
//            string query = string.Format(@"SELECT ISNULL(vfq.idQuest, '0') as ID, vfq.idQuest as ID,ISNULL(vfq.txtQuest,'') as quest,ISNULL(vfa.txt,'1') as number,ISNULL(nn.number,'0') as numberBooked
//                                  FROM VolFunctionQuest vfq
//                                  Inner join (SELECT DISTINCT idQuest,CAST(txt AS NVARCHAR(100)) as txt FROM VolFunctionArr where idArrangement = @idArrangement) vfa 
//                                  ON vfa.idQuest = vfq.idQuest
//                                  LEFT OUTER JOIN (SELECT CAST(vfq2.txtQuest AS NVARCHAR(100)) as quest,vfa.idQuest, count(vfa.idQuest) as number
//                                  FROM VolFunctionArr vfa
//                                  INNER JOIN VolLookup vl ON vfa.idQuest = vl.id
//                                  INNER JOIN VolFunctionQuest vfq2 ON vfa.idQuest = vfq2.idQuest
//                                  WHERE vfa.idArrangement = @idArrangement and vl.type = 'F'
//                                  group by CAST(vfq2.txtQuest AS NVARCHAR(100)),vfa.idQuest) nn ON nn.idQuest = vfq.idQuest");


//            SqlParameter[] sqlParameters = new SqlParameter[1];
//            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
//            sqlParameters[0].Value = idArrangement;

//            return conn.executeSelectQuery(query, sqlParameters);
//        }
        public DataTable GetSkillsForArrangement(int idArrangement)
        {
            string query = string.Format(@"SELECT ISNULL(vfq.idQuest, '0') as ID,ISNULL(vfq.txtQuest,'') as quest,ISNULL(vfa.txt,'1') as number,ISNULL(nn.number,'0') as numberBooked
                                  FROM VolQuest vfq
                                  Inner join (SELECT DISTINCT idQuest,CAST(txt AS NVARCHAR(100)) as txt FROM VolArr where idArr = @idArr) vfa 
                                  ON vfa.idQuest = vfq.idQuest
                                  LEFT OUTER JOIN (SELECT CAST(vfq2.txtQuest AS NVARCHAR(100)) as quest,vfa.idQuest, count(vfa.idQuest) as number
                                  FROM VolArr vfa
                                  INNER JOIN VolLookup vl ON vfa.idQuest = vl.id
                                  INNER JOIN VolQuest vfq2 ON vfa.idQuest = vfq2.idQuest
                                  WHERE vfa.idArr = @idArr and vl.type = 'S' and vl.idArrangement = @idArr 
                                  group by CAST(vfq2.txtQuest AS NVARCHAR(100)),vfa.idQuest) nn ON nn.idQuest = vfq.idQuest");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArr", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetFunctionsForArrangement(int idArrangement)
        {
            string query = string.Format(@"SELECT ISNULL(vfq.idQuest, '0') as ID, vfq.idQuest as ID,ISNULL(vfq.txtQuest,'') as quest,ISNULL(vfa.txt,'1') as number,ISNULL(nn.number,'0') as numberBooked
                                  FROM VolFunctionQuest vfq
                                  Inner join (SELECT DISTINCT idQuest,CAST(txt AS NVARCHAR(100)) as txt FROM VolFunctionArr where idArrangement = @idArrangement) vfa 
                                  ON vfa.idQuest = vfq.idQuest
                                  LEFT OUTER JOIN (SELECT CAST(vfq2.txtQuest AS NVARCHAR(100)) as quest,vfa.idQuest, count(vfa.idQuest) as number
                                  FROM VolFunctionArr vfa
                                  INNER JOIN VolLookup vl ON vfa.idQuest = vl.id
                                  INNER JOIN VolFunctionQuest vfq2 ON vfa.idQuest = vfq2.idQuest
                                  WHERE vfa.idArrangement = @idArrangement and vl.type = 'F' and vl.idArrangement = @idArrangement 
                                  group by CAST(vfq2.txtQuest AS NVARCHAR(100)),vfa.idQuest) nn ON nn.idQuest = vfq.idQuest");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetAverageAge(int idArrangement)
        {
            string query = string.Format(@"SELECT  sum(Year(GETDATE())-year(cp.birthdate))/ count(year(cp.birthdate))as num
                                          FROM ContactPerson cp
                                          INNER JOIN ArrangementBook a ON a.idContPers=cp.idContPers
                                          LEFT OUTER JOIN ContactPerson cpr ON cpr.idContPers = a.idContPers
                                          LEFT OUTER JOIN Gender g ON cpr.idGender = g.idGender
                                          WHERE  a.idArrangement = '" + idArrangement + @"' AND a.idContPers NOT IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4')");

            return conn.executeSelectQuery(query, null);
        }
        public Boolean UpdateVolLookup(int idArrangement, int idContPers, int idStatus, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE VolLookup SET type = CASE WHEN LEN(type)=2 THEN SUBSTRING(type,2,1) ELSE type END WHERE idArrangement = @idArrangement and idContPers = @idContPers");

            if (idStatus == 3)
                query = string.Format(@"UPDATE VolLookup SET type = CASE WHEN LEN(type)=1 THEN 'R'+type ELSE type END WHERE idArrangement = @idArrangement and idContPers = @idContPers");

            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[0].Value = idArrangement;

            sqlParameter[1] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[1].Value = idContPers;

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
            sqlParameter[4].Value = idArrangement.ToString() + "_" + idContPers.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangement_idContPers";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolLookup";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update vol lookup";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
        //Jelena

        //VEC POSTOJI////////////////////////////////////


        public DataTable NrOfOptions(List<int> CheckedArr, List<int> CheckedLabel, DateTime dateFrom, DateTime dateTo)
        {
            // tip aranzmana
            List<string> tableNameList = new List<string>();

            List<string> tableLongNameList = new List<string>();
            // za labele
            List<string> tableNameListL = new List<string>();

            List<string> tableLongNameListL = new List<string>();
            //
            if (CheckedArr.Count > 0)
            {
                tableLongNameList.Add("ArrType");
                tableNameList.Add("at");
            }
            if (CheckedLabel.Count > 0)
            {
                tableLongNameListL.Add("ArrangementLabel");
                tableNameListL.Add("al");
            }

            string condition1 = "";
            string condition2 = "";


            List<int> newList = new List<int>();


            if (tableNameList.Count == 1)
            {

                newList = CheckedArr;

                for (int i = 0; i < newList.Count; i++)
                {
                    if (i == 0)
                    {
                        condition1 = "AND (" + tableNameList[0] + ".idArrType='" + CheckedArr[i].ToString() + "')";
                    }
                    else
                    {
                        //OR u AND ili obrnuto
                        condition1 = condition1 + " OR (" + tableNameList[0] + ".idArrType='" + CheckedArr[i].ToString() + "')";
                    }
                }

                if (newList.Count > 1)
                    condition1 = "AND (" + condition1.Substring(3, condition1.Length - 3) + ")";



            }

            List<int> newListL = new List<int>();


            if (tableNameListL.Count == 1)
            {

                newListL = CheckedLabel;

                for (int i = 0; i < newListL.Count; i++)
                {
                    if (i == 0)
                    {
                        condition2 = "AND (" + tableNameListL[0] + ".idLabel='" + CheckedLabel[i].ToString() + "')";
                    }
                    else
                    {
                        //OR u AND ili obrnuto
                        condition2 = condition2 + " OR (" + tableNameListL[0] + ".idLabel='" + CheckedLabel[i].ToString() + "')";
                    }
                }

                if (newListL.Count > 1)
                    condition2 = "AND (" + condition2.Substring(3, condition2.Length - 3) + ")";


                // select  
            }


            string query = string.Format(@"SELECT a.codeArrangement,a.dtFromArrangement as dateFrom, a.dtToArrangement as dateTo,l.nameLabel,a.idArrangement, at.nameArrType,
                                          a.nrTraveler, 
                                         space(20) AS dateFrom1, space(20) AS dateTo1 
                                        FROM ArrangementBook  ab
                                        LEFT OUTER JOIN Arrangement a ON a.idArrangement = ab.idArrangement
                                        LEFT OUTER JOIN ArrangementStatus ars on ars.idArrangementStatus= ab.idStatus
                                        LEFT OUTER JOIN ArrType at on at.idArrType=a.typeArrangement 
                                        LEFT OUTER JOIN ArrangementLabel al on  al.idArrangement=ab.idArrangement
                                        LEFT OUTER JOIN Labels l on l.id= al.idLabel
                                        WHERE  a.dtFromArrangement>='" + dateFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dateTo.ToString("MM/dd/yyyy") + "'" + condition1 + condition2 +
            @"   GROUP BY  l.nameLabel,a.idArrangement,a.codeArrangement, at.nameArrType, a.dtFromArrangement, a.dtToArrangement, a.nrTraveler
                ORDER BY  l.nameLabel,a.dtFromArrangement
               ");



            // HAVING count(" + tableNameList[0] + @".idArrType)='" + newList.Count + "'
            SqlParameter[] sqlParameters = new SqlParameter[2];


            sqlParameters[0] = new SqlParameter("@dateFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dateFrom;
            sqlParameters[1] = new SqlParameter("@dateTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dateTo;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable NrStatus()
        {
            string query = string.Format(@"SELECT distinct nameStatus as nameOption, abs.idStatus as idOption
                                            FROM  ArrangementBook ab 
                                             INNER JOIN ArrangementBookStatus abs on abs.idStatus=ab.idStatus
                                             ORDER BY abs.idStatus
                                          ");
            return conn.executeSelectQuery(query, null);
        }

        //VEC POSTOJI
      
        public DataTable GetEmployee(int idArrangement)
        {
            string query = string.Format(@" SELECT distinct firstNameEmployee +' '+ lastNameEmployee as firstNameEmployee
                                            FROM ArrangementPrice ap
                                            LEFT OUTER JOIN Users u on ap.idUserCreated= u.idUser
                                            LEFT OUTER JOIN Employees e on u.idEmployee= e.idEmployee
                                            WHERE ap.idArrangement =  '" + idArrangement + "'");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;


            return conn.executeSelectQuery(query, sqlParameters);
        }


        public DataTable checkStatus(int idArrangement, int idStatus)
        {
            string query = string.Format(@"SELECT Count(idArrangementBook) as idOption
                                            FROM ArrangementBook ab
                                            LEFT OUTER JOIN Arrangement a ON ab.idArrangement = a.idArrangement
                                            WHERE ab.idStatus='" + idStatus + "' AND ab.idArrangement =  '" + idArrangement + "'");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            sqlParameters[1] = new SqlParameter("@idStatus", SqlDbType.Int);
            sqlParameters[1].Value = idStatus;
            return conn.executeSelectQuery(query, sqlParameters);
        }

        //
        //jelena

        public DataTable AllArrangementName()
        {
            string query = string.Format(@"SELECT  distinct codeArrangement,idArrangement
                                           FROM Arrangement order by codeArrangement ");


            return conn.executeSelectQuery(query, null);
            //nameArrangement
        }


        public DataTable PassportList(int idArrangement)
        {
            string query = string.Format(@"SELECT a.nameArrangement,a.dtFromArrangement,a.dtToArrangement,t.nameTitle,cpp.namePassport+ ' '+ cpp.lastNamePassport as passName,cp.birthdate, cpp.birthPlacePassport, cpp.numberPassport,
                                         c.nacionality,cpp.issuePlacePassport,cpp.dtPassportIssued,cpp.dtPassportValid, a.codeArrangement,
                                         space(40) AS nameArrangement1, space(20) AS dtFromArrangement1, space(20) AS dtToArrangement1, space(20) AS codeArrangement1
                                         FROM Arrangement a
                                         LEFT OUTER JOIN ArrangementBook ab on a.idArrangement=ab.idArrangement
                                         LEFT OUTER JOIN ContactPerson cp on ab.idContPers=cp.idContPers
                                         LEFT OUTER JOIN ContactPersonPassport cpp on ab.idContPers=cpp.idContPers
                                         LEFT OUTER JOIN Title t ON cp.idTitle=t.idTitle
                                         LEFT OUTER JOIN Country c ON cpp.idCountry= c.idCountry
                                         WHERE a.idArrangement='" + idArrangement + "'");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }
      
        public DataTable AnsAirportcodes()
        {
            string query = string.Format(@"SELECT DISTINCT ma.txtAns  
                                         FROM MedAns ma
                                         INNER JOIN MedCpr mcpr ON ma.idAns = mcpr.idAns 
                                         WHERE ma.idQuest = '169' ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable CheckAnsAirportcodes(int idContPers, string txtAns)
        {
            string query = string.Format(@"SELECT DISTINCT ma.txtAns  
                                         FROM MedAns ma
                                         INNER JOIN MedCpr mcpr ON ma.idAns = mcpr.idAns 
                                          WHERE ma.idQuest = '169' AND txtAns LIKE '" + txtAns + "' AND mcpr.idcpr = '" + idContPers + "'");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;

            sqlParameters[1] = new SqlParameter("@txtAns", SqlDbType.NVarChar);
            sqlParameters[1].Value = txtAns;
            return conn.executeSelectQuery(query, null);
        }

        public DataTable DeviceOnTrip(int idArrangement)
        {
            string query = string.Format(@"SELECT CASE WHEN a.nameArrangement IS NULL THEN '' ELSE a.nameArrangement END as nameArrangement, a.dtFromArrangement, a.dtToArrangement, CASE WHEN t.nameTitle IS NULL THEN '' ELSE t.nameTitle END as nameTitle, CASE WHEN cpr.firstname + ' ' + midname + ' ' + cpr.lastname IS NULL THEN '' ELSE cpr.firstname + ' ' + midname + ' ' + cpr.lastname END as name,CASE WHEN cpra.street + ' ' + cpra.housenr + ',' + cpra.postalCode + ' ' +cpra.city = ' , ' THEN '' ELSE cpra.street + ' ' + cpra.housenr + ',' + cpra.postalCode + ' ' +cpra.city  END as address,cpr.birthdate, CASE WHEN g.NameGender IS NULL THEN '' ELSE g.NameGender END AS gender,a.codeArrangement,
                        space(40) AS nameArrangement1, space(20) AS dtFromArrangement1, space(20) AS dtToArrangement1 , space(20) AS codeArrangement1 
                       FROM ArrangementBook ab
                       LEFT OUTER JOIN ContactPerson cpr ON cpr.idContPers = ab.idContPers
                       LEFT OUTER JOIN Title t ON t.idTitle = cpr.idTitle
                       LEFT OUTER JOIN Gender g ON g.idGender = cpr.idGender
                       LEFT OUTER JOIN (SELECT distinct street,housenr,postalCode,city,idContPers FROM ContactPersonAddress WHERE idAddressType = '1') cpra ON cpr.idContPers = cpra.idContPers
                       LEFT OUTER JOIN Arrangement a ON a.idArrangement = ab.idArrangement
                       WHERE a.idArrangement = '" + idArrangement + @"' ");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable Device(int idArrangement)
        {
            string query = string.Format(@"SELECT CASE WHEN a.nameArrangement IS NULL THEN '' ELSE a.nameArrangement END as nameArrangement, a.dtFromArrangement, a.dtToArrangement, CASE WHEN t.nameTitle IS NULL THEN '' ELSE t.nameTitle END as nameTitle, CASE WHEN cpr.firstname + ' ' + midname + ' ' + cpr.lastname IS NULL THEN '' ELSE cpr.firstname + ' ' + midname + ' ' + cpr.lastname END as name, cpr.idContPers,CASE WHEN cpra.street + ' ' + cpra.housenr + ',' + cpra.postalCode + ' ' +cpra.city = ' , ' THEN '' ELSE cpra.street + ' ' + cpra.housenr + ',' + cpra.postalCode + ' ' +cpra.city  END as address,cpr.birthdate, CASE WHEN g.NameGender IS NULL THEN '' ELSE g.NameGender END AS gender,a.codeArrangement,
                         space(40) AS nameArrangement1, space(20) AS dtFromArrangement1, space(20) AS dtToArrangement1 , space(20) codeArrangement1 
                       FROM ArrangementBook ab
                       LEFT OUTER JOIN ContactPerson cpr ON cpr.idContPers = ab.idContPers
                       LEFT OUTER JOIN Title t ON t.idTitle = cpr.idTitle
                       LEFT OUTER JOIN Gender g ON g.idGender = cpr.idGender
                       LEFT OUTER JOIN (SELECT distinct street,housenr,postalCode,city,idContPers FROM ContactPersonAddress WHERE idAddressType = '1') cpra ON cpr.idContPers = cpra.idContPers
                       LEFT OUTER JOIN Arrangement a ON a.idArrangement = ab.idArrangement
                       WHERE a.idArrangement = '" + idArrangement + @"' AND cpr.idContPers IN (SELECT DISTINCT mcpr.idCpr
                       FROM MedAns ma
                       INNER JOIN MedCpr mcpr ON ma.idAns = mcpr.idAns 
                       WHERE ma.idQuest = '169')");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable DeviceToRent(int idArrangement)
        {
            string query = string.Format(@"SELECT CASE WHEN a.nameArrangement IS NULL THEN '' ELSE a.nameArrangement END as nameArrangement, a.dtFromArrangement, a.dtToArrangement, CASE WHEN t.nameTitle IS NULL THEN '' ELSE t.nameTitle END as nameTitle, CASE WHEN cpr.firstname + ' ' + midname + ' ' + cpr.lastname IS NULL THEN '' ELSE cpr.firstname + ' ' + midname + ' ' + cpr.lastname END as name, cpr.idContPers,CASE WHEN cpra.street + ' ' + cpra.housenr + ',' + cpra.postalCode + ' ' +cpra.city = ' , ' THEN '' ELSE cpra.street + ' ' + cpra.housenr + ',' + cpra.postalCode + ' ' +cpra.city  END as address,cpr.birthdate, CASE WHEN g.NameGender IS NULL THEN '' ELSE g.NameGender END AS gender , a.codeArrangement,
                       space(40) AS nameArrangement1, space(20) AS dtFromArrangement1, space(20) AS dtToArrangement1 , space(20) AS codeArrangement1   
                       FROM ArrangementBook ab
                       LEFT OUTER JOIN ContactPerson cpr ON cpr.idContPers = ab.idContPers
                       LEFT OUTER JOIN Title t ON t.idTitle = cpr.idTitle
                       LEFT OUTER JOIN Gender g ON g.idGender = cpr.idGender
                       LEFT OUTER JOIN (SELECT distinct street,housenr,postalCode,city,idContPers FROM ContactPersonAddress WHERE idAddressType = '1') cpra ON cpr.idContPers = cpra.idContPers
                       LEFT OUTER JOIN Arrangement a ON a.idArrangement = ab.idArrangement
                       WHERE a.idArrangement = '" + idArrangement + @"' AND cpr.idContPers IN (SELECT DISTINCT mcpr.idCpr
                       FROM MedAns ma
                       INNER JOIN MedCpr mcpr ON ma.idAns = mcpr.idAns 
                       WHERE ma.idQuest = '219')");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;




            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable AnsRentedDevice()
        {
            string query = string.Format(@"SELECT DISTINCT ma.txtAns  
                                         FROM MedAns ma
                                         INNER JOIN MedCpr mcpr ON ma.idAns = mcpr.idAns 
                                         WHERE ma.idQuest = '219' ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable CheckRentedDevice(int idContPers, string txtAns)
        {
            string query = string.Format(@"SELECT DISTINCT ma.txtAns  
                                         FROM MedAns ma
                                         INNER JOIN MedCpr mcpr ON ma.idAns = mcpr.idAns 
                                          WHERE ma.idQuest = '219' AND txtAns LIKE '" + txtAns + "' AND mcpr.idcpr = '" + idContPers + "'");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;

            sqlParameters[1] = new SqlParameter("@txtAns", SqlDbType.NVarChar);
            sqlParameters[1].Value = txtAns;
            return conn.executeSelectQuery(query, null);
        }

        public DataTable Telephone(int idArrangement)
        {
            string query = string.Format(@"SELECT CASE WHEN a.nameArrangement IS NULL THEN '' ELSE a.nameArrangement END as nameArrangement, a.dtFromArrangement, a.dtToArrangement, CASE WHEN t.nameTitle IS NULL THEN '' ELSE t.nameTitle END as nameTitle, CASE WHEN cpr.firstname + ' ' + midname + ' ' + cpr.lastname IS NULL THEN '' ELSE cpr.firstname + ' ' + midname + ' ' + cpr.lastname END as name, cpr.idContPers,CASE WHEN cpra.street + ' ' + cpra.housenr + ',' + cpra.postalCode + ' ' +cpra.city = ' , ' THEN '' ELSE cpra.street + ' ' + cpra.housenr + ',' + cpra.postalCode + ' ' +cpra.city  END as address,cpr.birthdate, CASE WHEN g.NameGender IS NULL THEN '' ELSE g.NameGender END AS gender, a.codeArrangement,
                        space(40) AS nameArrangement1, space(20) AS dtFromArrangement1, space(20) AS dtToArrangement1, space(20) AS codeArrangement1  
                       FROM ArrangementBook ab
                       LEFT OUTER JOIN ContactPerson cpr ON cpr.idContPers = ab.idContPers
                       LEFT OUTER JOIN Title t ON t.idTitle = cpr.idTitle
                       LEFT OUTER JOIN Gender g ON g.idGender = cpr.idGender
                       LEFT OUTER JOIN (SELECT distinct street,housenr,postalCode,city,idContPers FROM ContactPersonAddress WHERE idAddressType = '1') cpra ON cpr.idContPers = cpra.idContPers
                       LEFT OUTER JOIN Arrangement a ON a.idArrangement = ab.idArrangement
                       WHERE a.idArrangement = '" + idArrangement + "'");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable AllTelephoneList(int idContPers)
        {
            string query = string.Format(@"select  cpt.numberTel +' ' +cpt.descriptionTel   as numberTel           
                         from ContactPerson cp                             
                         LEFT OUTER JOIN ContactPersonTel cpt on cp.idContPers= cpt.idContPers                                    
                         WHERE cpt.idContPers='" + idContPers + "'");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;


            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable RoomList(int idArrangement)
        {
            string query = string.Format(@"SELECT CASE WHEN a.nameArrangement IS NULL THEN '' ELSE a.nameArrangement END as nameArrangement, 
                                         a.dtFromArrangement, a.dtToArrangement, CASE WHEN t.nameTitle IS NULL THEN '' ELSE t.nameTitle END as nameTitle, 
                                         CASE WHEN cpr.firstname + ' ' + midname + ' ' + cpr.lastname IS NULL THEN '' ELSE cpr.firstname + ' ' + midname + ' ' + cpr.lastname END as name,
                                         cpr.idContPers ,ar.idRoom,a.codeArrangement,
                                         space(40) AS nameArrangement1, space(20) AS dtFromArrangement1, space(20) AS dtToArrangement1, space(20) codeArrangement1
                       FROM ArrangementBook ab
                       LEFT OUTER JOIN ContactPerson cpr ON cpr.idContPers = ab.idContPers
                       LEFT OUTER JOIN Title t ON t.idTitle = cpr.idTitle
                       LEFT OUTER JOIN Arrangement a ON a.idArrangement = ab.idArrangement
                       LEFT OUTER JOIN ArrangementBookArticles ar on ar.idArrangementBook= ab.idArrangementBook
                       WHERE a.idArrangement = '" + idArrangement + @"' AND cpr.idContPers IN (SELECT DISTINCT mcpr.idCpr
                       FROM MedAns ma
                       INNER JOIN MedCpr mcpr ON ma.idAns = mcpr.idAns 
                       WHERE ma.idQuest = '168' OR ma.idQuest = '177' ) ");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable ParticipationList(int idArrangement)
        {
            string query = string.Format(@"SELECT CASE WHEN a.nameArrangement IS NULL THEN '' ELSE a.nameArrangement END as nameArrangement, 
                                         a.dtFromArrangement, a.dtToArrangement, CASE WHEN t.nameTitle IS NULL THEN '' ELSE t.nameTitle END as nameTitle, 
                                         CASE WHEN cpr.firstname IS NULL THEN '' ELSE  cpr.firstname END + ' ' + CASE WHEN cpr.midname IS NULL THEN  '' ELSE  cpr.midname END+ ' ' + CASE WHEN cpr.lastname IS NULL THEN  '' ELSE  cpr.lastname END AS name,
                                         cpr.idContPers , pb.nameBoardingPoint, a.cityArrangement, a.codeArrangement,
                                         space(40) AS nameArrangement1, space(20) AS dtFromArrangement1, space(20) AS dtToArrangement1, space(20) AS codeArrangement1
                       FROM ArrangementBook ab
                       LEFT OUTER JOIN ContactPerson cpr ON cpr.idContPers = ab.idContPers
                       LEFT OUTER JOIN Title t ON t.idTitle = cpr.idTitle
                       LEFT OUTER JOIN Arrangement a ON a.idArrangement = ab.idArrangement
                       LEFT OUTER JOIN BoardingPoint pb on pb.idBoardingPoint= ab.idBoarding
                       WHERE a.idArrangement = '" + idArrangement + "' ");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable AnsDiets()
        {
            string query = string.Format(@"SELECT DISTINCT ma.txtAns  
                                         FROM MedAns ma
                                         INNER JOIN MedCpr mcpr ON ma.idAns = mcpr.idAns 
                                         WHERE ma.idQuest = '177' ");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable CheckAnsDiets(int idContPers, string txtAns)
        {
            string query = string.Format(@"SELECT DISTINCT ma.txtAns  
                                         FROM MedAns ma
                                         INNER JOIN MedCpr mcpr ON ma.idAns = mcpr.idAns 
                                          WHERE ma.idQuest = '177' AND txtAns LIKE '" + txtAns + "' AND mcpr.idcpr = '" + idContPers + "'");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;

            sqlParameters[1] = new SqlParameter("@txtAns", SqlDbType.NVarChar);
            sqlParameters[1].Value = txtAns;
            return conn.executeSelectQuery(query, null);
        }

        public DataTable AnsDevice()
        {
            string query = string.Format(@"SELECT DISTINCT ma.txtAns  
                                         FROM MedAns ma
                                         INNER JOIN MedCpr mcpr ON ma.idAns = mcpr.idAns 
                                         WHERE ma.idQuest = '168' ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable CheckAnsDevice(int idContPers, string txtAns)
        {
            string query = string.Format(@"SELECT DISTINCT ma.txtAns  
                                         FROM MedAns ma
                                         INNER JOIN MedCpr mcpr ON ma.idAns = mcpr.idAns 
                                          WHERE ma.idQuest = '168' AND txtAns LIKE '" + txtAns + "' AND mcpr.idcpr = '" + idContPers + "'");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;

            sqlParameters[1] = new SqlParameter("@txtAns", SqlDbType.NVarChar);
            sqlParameters[1].Value = txtAns;
            return conn.executeSelectQuery(query, null);
        }

        public DataTable NrBookedTrips(int idContPers)
        {
            string query = string.Format(@"SELECT count(idArrangement)as nrTraveler
                                       FROM  ArrangementBook ab
                                         WHERE idContPers='" + idContPers + @"'");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable PassengerList(int idArrangement)
        {
            string query = string.Format(@"SELECT DISTINCT CASE WHEN a.nameArrangement IS NULL THEN '' ELSE a.nameArrangement END as nameArrangement, a.dtFromArrangement, 
             a.dtToArrangement, CASE WHEN t.nameTitle IS NULL THEN '' ELSE t.nameTitle END as nameTitle,
             CASE WHEN cpr.firstname + ' ' + midname + ' ' + cpr.lastname IS NULL THEN '' ELSE cpr.firstname + ' ' + midname + ' ' + cpr.lastname END as name,
             cpr.idContPers , (select count(idArrangement) FROM ArrangementBook WHERE idArrangement='" + idArrangement + @"')as nrBooked, a.codeArrangement,
                        space(40) AS nameArrangement1, space(20) AS dtFromArrangement1, space(20) AS dtToArrangement1, space(20) AS codeArrangement1
                       FROM ArrangementBook ab
                       LEFT OUTER JOIN ContactPerson cpr ON cpr.idContPers = ab.idContPers
                       LEFT OUTER JOIN Title t ON t.idTitle = cpr.idTitle
                       LEFT OUTER JOIN Arrangement a ON a.idArrangement = ab.idArrangement
                       LEFT OUTER JOIN ContactPersonFilter cf on cf.idContPers= cpr.idContPers
                       WHERE a.idArrangement = '" + idArrangement + @"' AND cpr.idContPers IN (SELECT DISTINCT mcpr.idCpr
                       FROM MedAns ma
                       INNER JOIN MedCpr mcpr ON ma.idAns = mcpr.idAns 
                       WHERE ma.idQuest = '177' OR ma.idQuest = '178' OR  ma.idQuest = '179' ) AND cf.idFilter!='4'");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable AnsEpilepsie()
        {
            string query = string.Format(@"SELECT DISTINCT ma.txtAns  
                                         FROM MedAns ma
                                         INNER JOIN MedCpr mcpr ON ma.idAns = mcpr.idAns 
                                         WHERE ma.idQuest = '178' ");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable CheckAnsEpilepsie(int idContPers, string txtAns)
        {
            string query = string.Format(@"SELECT DISTINCT ma.txtAns  
                                         FROM MedAns ma
                                         INNER JOIN MedCpr mcpr ON ma.idAns = mcpr.idAns 
                                          WHERE ma.idQuest = '178' AND txtAns LIKE '" + txtAns + "' AND mcpr.idcpr = '" + idContPers + "'");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;

            sqlParameters[1] = new SqlParameter("@txtAns", SqlDbType.NVarChar);
            sqlParameters[1].Value = txtAns;
            return conn.executeSelectQuery(query, null);
        }

        public DataTable AnsMedication()
        {
            string query = string.Format(@"SELECT DISTINCT ma.txtAns  
                                         FROM MedAns ma
                                         INNER JOIN MedCpr mcpr ON ma.idAns = mcpr.idAns 
                                         WHERE ma.idQuest = '179' ");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable CheckAnsMedication(int idContPers, string txtAns)
        {
            string query = string.Format(@"SELECT DISTINCT ma.txtAns  
                                         FROM MedAns ma
                                         INNER JOIN MedCpr mcpr ON ma.idAns = mcpr.idAns 
                                          WHERE ma.idQuest = '179' AND txtAns LIKE '" + txtAns + "' AND mcpr.idcpr = '" + idContPers + "'");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;

            sqlParameters[1] = new SqlParameter("@txtAns", SqlDbType.NVarChar);
            sqlParameters[1].Value = txtAns;
            return conn.executeSelectQuery(query, null);
        }

        public DataTable StatusOption(DateTime dtFromArrangement, DateTime ToArrangement)
        {
            string query = string.Format(@"SELECT CASE WHEN a.nameArrangement IS NULL THEN '' ELSE a.nameArrangement END as nameArrangement, 
                                         a.dtFromArrangement, a.dtToArrangement, CASE WHEN t.nameTitle IS NULL THEN '' ELSE t.nameTitle END as nameTitle, 
                                         CASE WHEN cpr.firstname + ' ' + midname + ' ' + cpr.lastname IS NULL THEN '' ELSE cpr.firstname + ' ' + midname + ' ' + cpr.lastname END as name,
                                         cpr.idContPers,a.codeArrangement,
                                         space(20) AS dateFrom1, space(20) AS dateTo1
                       FROM ArrangementBook ab
                       LEFT OUTER JOIN ContactPerson cpr ON cpr.idContPers = ab.idContPers
                       LEFT OUTER JOIN Title t ON t.idTitle = cpr.idTitle
                       LEFT OUTER JOIN Arrangement a ON a.idArrangement = ab.idArrangement
                        WHERE ab.idStatus='1' AND a.dtFromArrangement>='" + dtFromArrangement.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + ToArrangement.ToString("MM/dd/yyyy") + "'");

            SqlParameter[] sqlParameters = new SqlParameter[2];


            sqlParameters[0] = new SqlParameter("@dtFromArrangement", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFromArrangement;

            sqlParameters[1] = new SqlParameter("@ToArrangement", SqlDbType.DateTime);
            sqlParameters[1].Value = ToArrangement;


            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable StragglersList(int idArrangement)
        {
            string query = string.Format(@"SELECT  a.dtFromArrangement, a.dtToArrangement, pl.idClient, cl.nameClient, a.nameArrangement, a.codeArrangement,
                        space(40) AS nameArrangement1, space(20) AS dtFromArrangement1, space(20) AS dtToArrangement1, space(20) AS codeArrangement1
                       FROM PriceList pl 
                       LEFT OUTER JOIN Client cl on cl.idClient= pl.idClient
                       LEFT OUTER JOIN Arrangement a on a.idArrangement= pl.idArrangement 
                       WHERE pl.idArrangement = '" + idArrangement + "'");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable ClientTel(int idClient)
        {
            string query = string.Format(@" select distinct ct.numberTel             
                         from Client c                             
                         LEFT OUTER JOIN ClientTel ct on c.idClient= ct.idClient                                    
                         WHERE ct.idDefaultTel='1' and ct.idClient='" + idClient + "'");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameters[0].Value = idClient;


            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable ClientEmail(int idClient)
        {
            string query = string.Format(@" select distinct  cpe.email             
                         from Client cp                             
                         LEFT OUTER JOIN ClientEmail cpe on cp.idClient= cpe.idClient                                    
                         WHERE cpe.isCommunication='1' and cpe.idClient='" + idClient + "'");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameters[0].Value = idClient;


            return conn.executeSelectQuery(query, sqlParameters);
        }

        // jelena novo
        public DataTable GetCpTel(int idContPers)
        {
            string query = string.Format(@" select  cpt.numberTel             
                         from ContactPerson cp                             
                         LEFT OUTER JOIN ContactPersonTel cpt on cp.idContPers= cpt.idContPers                                    
                         WHERE cpt.isDefaultTel='1' and cpt.idContPers='" + idContPers + "'");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;


            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetCpEmail(int idContPers)
        {
            string query = string.Format(@" select  cpe.email             
                         from ContactPerson cp                             
                         LEFT OUTER JOIN ContactPersonEmail cpe on cp.idContPers= cpe.idContPers                                    
                         WHERE cpe.isCommunication='1' and cpe.idContPers='" + idContPers + "'");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;


            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable DivisionList(int idArrangement)
        {
            string query = string.Format(@"SELECT CASE WHEN a.nameArrangement IS NULL THEN '' ELSE a.nameArrangement END as nameArrangement, a.dtFromArrangement, a.dtToArrangement, CASE WHEN t.nameTitle IS NULL THEN '' ELSE t.nameTitle END as nameTitle, CASE WHEN cpr.firstname + ' ' + midname + ' ' + cpr.lastname IS NULL THEN '' ELSE cpr.firstname + ' ' + midname + ' ' + cpr.lastname END as name, a.codeArrangement,
                        space(40) AS nameArrangement1, space(20) AS dtFromArrangement1, space(20) AS dtToArrangement1, space(20) AS codeArrangement1  
                       FROM ArrangementBook ab
                       LEFT OUTER JOIN ContactPerson cpr ON cpr.idContPers = ab.idContPers
                       LEFT OUTER JOIN Title t ON t.idTitle = cpr.idTitle
                       LEFT OUTER JOIN Arrangement a ON a.idArrangement = ab.idArrangement
                       LEFT OUTER JOIN ContactPersonFilter cpf on cpf.idContPers= cpr.idContPers
                       WHERE a.idArrangement = '" + idArrangement + @"' AND cpf.idFilter='4'");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetVoluntaryArrangement(int idArrangement)
        {

            string join = "inner join";
            string sort = "txtQuest, vq.questSort, va.ansSort";
            string selectQuery = "select distinct vqg.nameQuestGroup,vq.idQuest, CONVERT(NVARCHAR(2000),vq.txtQuest) as txtQuest, va.idAns, va.txtAns,va.idAnsType," +
                "vc.idArrangement, CONVERT(NVARCHAR(2000),vc.txt) as txt,vq.questSort, va.ansSort from VolFunctionQuest vq "
                + join + " VolFunctionQuestGroup vqg on vq.idQuestGroup = vqg.idQuestGroup "
                + join + " VolFunctionAns va on " + "va.idQuest = vq.idQuest "
                + join + "  (select * from VolFunctionArr where (idArrangement = '" + idArrangement + "' or idArrangement is null)) vc on vc.idQuest = vq.idQuest and va.idAns = vc.idAns  " +
                " order by " + sort;


            return conn.executeSelectQuery(selectQuery, null);

        }

        public DataTable TeamList(int idArrangement)
        {
            string query = string.Format(@"SELECT CASE WHEN a.nameArrangement IS NULL THEN '' ELSE a.nameArrangement END as nameArrangement, a.dtFromArrangement, a.dtToArrangement,ab.idContPers, CASE WHEN t.nameTitle IS NULL THEN '' ELSE t.nameTitle END as nameTitle, a.codeArrangement,
                        space(40) AS nameArrangement1, space(20) AS dtFromArrangement1, space(20) AS dtToArrangement1, space(20) AS codeArrangement1  
                       FROM ArrangementBook ab
                       LEFT OUTER JOIN ContactPerson cpr ON cpr.idContPers = ab.idContPers
                       LEFT OUTER JOIN Title t ON t.idTitle = cpr.idTitle
                       LEFT OUTER JOIN Arrangement a ON a.idArrangement = ab.idArrangement
                       LEFT OUTER JOIN ContactPersonFilter cpf on cpf.idContPers= cpr.idContPers
                       WHERE a.idArrangement = '" + idArrangement + @"' AND cpf.idFilter='4'");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetVoluntaryFunction(int idContPers, string txtQuest)
        {
            string query = string.Format(@"select vcp.idQuest ,vq.txtQuest 
                                           FROM VolFunctionCpr vcp
                                           INNER JOIN  VolFunctionQuest vq on vq.idQuest= vcp.idQuest
                                           INNER JOIN   VolFunctionAns va on va.idQuest = vq.idQuest 
                                           WHERE vcp.idcpr=@idContPers and txtQuest LIKE @txtQuest ");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;

            sqlParameters[1] = new SqlParameter("@txtQuest", SqlDbType.NVarChar);
            sqlParameters[1].Value = txtQuest;


            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetVoluntaryFunctionAll(int idArrangement)
        {

            string join = "inner join";
            string sort = "txtQuest, vq.questSort, va.ansSort";
            string selectQuery = "select distinct vqg.nameQuestGroup,vq.idQuest, CONVERT(NVARCHAR(2000),vq.txtQuest) as txtQuest, va.idAns, va.txtAns,va.idAnsType," +
                " CONVERT(NVARCHAR(2000),vc.txt) as txt,vq.questSort, va.ansSort from VolFunctionQuest vq "
                + join + " VolFunctionQuestGroup vqg on vq.idQuestGroup = vqg.idQuestGroup "
                + join + " VolFunctionAns va on " + "va.idQuest = vq.idQuest "
                + join + "  (select * from VolFunctionCpr where (idcpr in (select cpr.idContPers FROM ArrangementBook ab" +
                           " LEFT OUTER JOIN ContactPerson cpr ON cpr.idContPers = ab.idContPers" +
                            " LEFT OUTER JOIN Arrangement a ON a.idArrangement = ab.idArrangement" +
                            " LEFT OUTER JOIN ContactPersonFilter cpf on cpf.idContPers= cpr.idContPers" +
                            " WHERE a.idArrangement = '" + idArrangement + @"' AND cpf.idFilter='4'))) vc on vc.idQuest = vq.idQuest and va.idAns = vc.idAns " +
                " order by " + sort;



            return conn.executeSelectQuery(selectQuery, null);

        }
        public DataTable GetVoluntaryTrip(int idArrangement)
        {


            string join = "inner join";
            string sort = "txtQuest, vq.questSort, va.ansSort";
            string selectQuery = "select distinct vqg.nameQuestGroup,vq.idQuest, CONVERT(NVARCHAR(2000),vq.txtQuest) as txtQuest, va.idAns, va.txtAns,va.idAnsType," +
                "vc.idArrangement, CONVERT(NVARCHAR(2000),vc.txt) as txt,vq.questSort, va.ansSort from VolFunctionQuest vq "
                + join + " VolFunctionQuestGroup vqg on vq.idQuestGroup = vqg.idQuestGroup "
                + join + " VolFunctionAns va on " + "va.idQuest = vq.idQuest "
                + join + "  (select * from VolFunctionArr where (idArrangement = '" + idArrangement + "' or idArrangement is null)) vc on vc.idQuest = vq.idQuest and va.idAns = vc.idAns  " +
                " order by " + sort;
            return conn.executeSelectQuery(selectQuery, null);
        }
        public DataTable BoardingPoint(int idArrangement)
        {
            string query = string.Format(@"SELECT CASE WHEN a.nameArrangement IS NULL THEN '' ELSE a.nameArrangement END as nameArrangement, a.dtFromArrangement, a.dtToArrangement,bp.nameBoardingPoint, CASE WHEN t.nameTitle IS NULL THEN '' ELSE t.nameTitle END as nameTitle, CASE WHEN cpr.firstname + ' ' + midname + ' ' + cpr.lastname IS NULL THEN '' ELSE cpr.firstname + ' ' + midname + ' ' + cpr.lastname END as name, cpra.city as addressCity,abp.departure, abp.arrivel, a.codeArrangement,
                        space(40) AS nameArrangement1, space(20) AS dtFromArrangement1, space(20) AS dtToArrangement1, space(20) AS codeArrangement1
                       FROM ArrangementBook ab
                       LEFT OUTER JOIN ContactPerson cpr ON cpr.idContPers = ab.idContPers
                       LEFT OUTER JOIN Title t ON t.idTitle = cpr.idTitle
                       LEFT OUTER JOIN  ArrangementBoardingPoint abp ON  abp.idBoardingPoint = ab.idBoarding
                       LEFT OUTER JOIN BoardingPoint bp on bp.idBoardingPoint= abp.idBoardingPoint
                       LEFT OUTER JOIN (SELECT distinct street,housenr,postalCode,city,idContPers FROM ContactPersonAddress WHERE idAddressType = '1') cpra ON cpr.idContPers = cpra.idContPers
                       LEFT OUTER JOIN Arrangement a ON a.idArrangement = ab.idArrangement
                       WHERE a.idArrangement = '" + idArrangement + "' order by bp.nameBoardingPoint");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        //jeca18.1

        public DataTable AllSatatus()
        {
            string query = string.Format(@"SELECT  distinct nameStatus,idStatus
                                           FROM ArrangementBookStatus order by nameStatus ");


            return conn.executeSelectQuery(query, null);


        }

        public DataTable AllTPapersStatus()
        {
            string query = string.Format(@"SELECT  distinct nameTravelPapers,idTravelPapers
                                           FROM ArrangementBookTravelPapers order by idTravelPapers ");


            return conn.executeSelectQuery(query, null);

        }
        public DataTable Invoice(int idStatus, int idTravelPapers)
        {

            string query = string.Format(@"
                      
               SELECT i.invoiceRbr,
                CASE WHEN f.firstNameEmployee IS NULL THEN '' ELSE  f.firstNameEmployee END + ' ' + CASE WHEN f.midNameEmployee IS NULL THEN  '' ELSE  f.midNameEmployee END+ ' ' + CASE WHEN f.lastNameEmployee IS NULL THEN  '' ELSE  f.lastNameEmployee END AS Employee ,
               (select sum(brutoAmount) from Invoice inv where inv.idVoucher=ab.idArrangementBook and idInvoiceStatus= '4') as priceInvoice,
                a.dtFromArrangement, a.dtToArrangement,
                CASE WHEN cp.firstname IS NULL THEN '' ELSE  cp.firstname END + ' ' + CASE WHEN cp.midname IS NULL THEN  '' ELSE  cp.midname END+ ' ' + CASE WHEN cp.lastname IS NULL THEN  '' ELSE  cp.lastname END AS bookedPerson,
                CASE WHEN a.codeArrangement IS NULL THEN '' ELSE a.codeArrangement END as codeArrangement
                     
                      FROM ArrangementBook ab
                      LEFT OUTER JOIN Arrangement a ON a.idArrangement = ab.idArrangement
                      LEFT OUTER JOIN ContactPerson cp on ab.idContPers=cp.idContPers
                      LEFT OUTER JOIN Invoice i on i.idVoucher=ab.idArrangementBook
                      LEFT OUTER JOIN Users e on e.idUser=i.userCreated
                      LEFT JOIN Employees f on f.idEmployee=e.idEmployee
                      WHERE ab.idStatus='" + idStatus + "'and ab.idTravelPapers='" + idTravelPapers + @"' AND a.nameArrangement is not null

                      ");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@idStatus", SqlDbType.Int);
            sqlParameters[0].Value = idStatus;

            sqlParameters[1] = new SqlParameter("@idTravelPapers", SqlDbType.Int);
            sqlParameters[1].Value = idTravelPapers;

            return conn.executeSelectQuery(query, sqlParameters);

        }
        public DataTable DepartureList1(DateTime dtFrom, DateTime dtTo)
        {

            string query = string.Format(@"
                      
             SELECT DISTINCT l.nameLabel as Label,CASE WHEN at.nameArrType IS NULL THEN '' ELSE at.nameArrType END as Type
             ,arr.bookedTravelers as TotalBooked,ar.nrTravelers as Maximum,
             CASE WHEN (ar.nrTravelers>0 and arr.bookedTravelers>0 ) THEN CONVERT(decimal(10,2), CONVERT(decimal(10,2),arr.bookedTravelers)/CONVERT(decimal(10,2),ar.nrTravelers)*100) ELSE '0' END as Occupancy,
             space(20) AS dateFrom1, space(20) AS dateTo1 
             FROM Arrangement a
             LEFT OUTER JOIN ArrType at on a.typeArrangement =  at.idArrType
             INNER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement
             LEFT OUTER JOIN Labels l on l.idLabel =  al.idLabel
             INNER JOIN (SELECT DISTINCT idArrangement FROM ArrangementBook) ab ON ab.idArrangement = a.idArrangement
             LEFT OUTER JOIN (SELECT l.idLabel,typeArrangement,SUM(nrTraveler) as nrTravelers FROM Arrangement a LEFT OUTER JOIN ArrangementLabel l On l.idArrangement = a.idArrangement GROUP BY l.idLabel,typeArrangement) ar  ON ar.typeArrangement = a.typeArrangement AND ar.idLabel =  al.idLabel
             LEFT OUTER JOIN (SELECT l.idLabel,typeArrangement, COUNT(ab.idContPers) as bookedTravelers FROM Arrangement a LEFT OUTER JOIN ArrangementLabel l On l.idArrangement = a.idArrangement LEFT OUTER JOIN  ArrangementBook ab ON ab.idArrangement = a.idArrangement   WHERE ab.idStatus='1' or ab.idStatus='2' GROUP BY l.idLabel,typeArrangement) arr  ON arr.typeArrangement = a.typeArrangement AND arr.idLabel =  al.idLabel
             
             WHERE arr.bookedTravelers IS NOT NULL and a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + @"'
             ORDER BY  l.nameLabel");




            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;
            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;


            return conn.executeSelectQuery(query, null);

        }

        public DataTable DepartureList1Ex(DateTime dtFrom, DateTime dtTo)
        {

            string query = string.Format(@"
                      
             SELECT DISTINCT l.nameLabel as Label,CASE WHEN at.nameArrType IS NULL THEN '' ELSE at.nameArrType END as Type
             ,arr.bookedTravelers as TotalBooked,ar.nrTravelers as Maximum,
             CASE WHEN (ar.nrTravelers>0 and arr.bookedTravelers>0 ) THEN CONVERT(decimal(10,2), CONVERT(decimal(10,2),arr.bookedTravelers)/CONVERT(decimal(10,2),ar.nrTravelers)*100) ELSE '0' END as Occupancy,
             space(20) AS dateFrom1, space(20) AS dateTo1 
             FROM Arrangement a
             LEFT OUTER JOIN ArrType at on a.typeArrangement =  at.idArrType
             INNER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement
             LEFT OUTER JOIN Labels l on l.idLabel =  al.idLabel
             INNER JOIN (SELECT DISTINCT idArrangement FROM ArrangementBook) ab ON ab.idArrangement = a.idArrangement
             LEFT OUTER JOIN (SELECT l.idLabel,typeArrangement,SUM(nrTraveler) as nrTravelers FROM Arrangement a LEFT OUTER JOIN ArrangementLabel l On l.idArrangement = a.idArrangement GROUP BY l.idLabel,typeArrangement) ar  ON ar.typeArrangement = a.typeArrangement AND ar.idLabel =  al.idLabel
             LEFT OUTER JOIN (SELECT l.idLabel,typeArrangement,COUNT(ab.idContPers) as bookedTravelers FROM Arrangement a LEFT OUTER JOIN ArrangementLabel l On l.idArrangement = a.idArrangement LEFT OUTER JOIN  ArrangementBook ab ON ab.idArrangement = a.idArrangement where ab.idStatus='2' GROUP BY l.idLabel,typeArrangement) arr  ON arr.typeArrangement = a.typeArrangement AND arr.idLabel =  al.idLabel
             LEFT OUTER JOIN ArrangementBook abook on a.idArrangement= abook.idArrangement
             WHERE abook.idStatus='2' and a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + @"'
             ORDER BY  l.nameLabel");




            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;
            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;


            return conn.executeSelectQuery(query, null);

        }
        public DataTable DepartureList2(DateTime dtFrom, DateTime dtTo)
        {
            string query = string.Format(@" SELECT   Convert(nvarchar(4),YEAR(a.dtFromArrangement)) + case when LEN (Convert(nvarchar(2),datepart(mm,a.dtFromArrangement))) = 1 THEN '0' + Convert(nvarchar(2),datepart(mm,a.dtFromArrangement)) ELSE  Convert(nvarchar(2),datepart(mm,a.dtFromArrangement)) END as mounth,l.nameLabel as Label, a.dtFromArrangement as dateFrom, 
                 dateName(dw, a.dtFromArrangement)as dayarr,
             CASE WHEN a.nameArrangement IS NULL THEN '' ELSE a.nameArrangement END as arrangement ,a.minNrTraveler, arr.nr,a.nrTraveler, space(20) AS dateFrom1, space(20) AS dateTo1,
             CASE WHEN (arr.nr>0 and a.nrTraveler>0 ) THEN CONVERT(decimal(10,2), CONVERT(decimal(10,2),arr.nr)/CONVERT(decimal(10,2),a.nrTraveler)*100) ELSE '0' END as Occupancy
             FROM Arrangement a
             INNER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement
             LEFT OUTER JOIN Labels l on l.idLabel =  al.idLabel
             LEFT OUTER JOIN ( select a.idArrangement,l.idLabel,COUNT(ab.idContPers) as nr FROM Arrangement a 
                              LEFT OUTER JOIN  ArrangementBook ab ON ab.idArrangement = a.idArrangement AND (ab.idStatus= '1' OR ab.idstatus='2')
                              LEFT OUTER JOIN ArrangementLabel l On l.idArrangement = a.idArrangement
                   INNER JOIN (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter<>'4' ) cpf ON cpf.idContPers=ab.idContPers     
             WHERE   a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and a.dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + @"'
             GROUP BY a.idArrangement,l.idLabel) arr  ON arr.idArrangement = a.idArrangement           
              WHERE arr.nr is not null and a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and a.dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + @"'
 
           GROUP BY a.dtFromArrangement,a.nameArrangement,a.codeArrangement,a.minNrTraveler,arr.nr,a.nrTraveler,l.nameLabel
               ORDER BY a.dtFromArrangement, Convert(nvarchar(4),YEAR(a.dtFromArrangement)) + case when LEN (Convert(nvarchar(2),datepart(mm,a.dtFromArrangement))) = 1 THEN '0' + Convert(nvarchar(2),datepart(mm,a.dtFromArrangement)) ELSE  Convert(nvarchar(2),datepart(mm,a.dtFromArrangement)) END ");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;
            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;

            return conn.executeSelectQuery(query, null);
        }

        public DataTable DepartureList2Ex(DateTime dtFrom, DateTime dtTo)
        {
            string query = string.Format(@"   SELECT Convert(nvarchar(4),YEAR(a.dtFromArrangement)) + case when LEN (Convert(nvarchar(2),datepart(mm,a.dtFromArrangement))) = 1 THEN '0' + Convert(nvarchar(2),datepart(mm,a.dtFromArrangement)) ELSE  Convert(nvarchar(2),datepart(mm,a.dtFromArrangement)) END  as mounth,l.nameLabel as Label, a.dtFromArrangement as dateFrom, dateName(dw, a.dtFromArrangement)as dayarr,
             a.nameArrangement as arrangement,a.minNrTraveler, arr.nr,a.nrTraveler, space(20) AS dateFrom1, space(20) AS dateTo1,
CASE WHEN (arr.nr>0 and a.nrTraveler>0 ) THEN CONVERT(decimal(10,2), CONVERT(decimal(10,2),arr.nr)/CONVERT(decimal(10,2),a.nrTraveler)*100) ELSE '0' END as Occupancy
             FROM Arrangement a
             INNER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement
             LEFT OUTER JOIN Labels l on l.idLabel =  al.idLabel
             LEFT OUTER JOIN ( select a.idArrangement,l.idLabel,COUNT(ab.idContPers) as nr FROM Arrangement a 
                              LEFT OUTER JOIN  ArrangementBook ab ON ab.idArrangement = a.idArrangement and (ab.idStatus='2')
                    INNER JOIN (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter<>'4' ) cpf ON cpf.idContPers=ab.idContPers     
                    LEFT OUTER JOIN ArrangementLabel l On l.idArrangement = a.idArrangement 
                              WHERE a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + @"'
             GROUP BY a.idArrangement,l.idLabel) arr  ON arr.idArrangement = a.idArrangement  
            
              WHERE arr.nr is not null and a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + @"'
                
              GROUP BY a.dtFromArrangement,a.nameArrangement,a.codeArrangement,a.minNrTraveler,arr.nr,a.nrTraveler,l.nameLabel
              ORDER BY  Convert(nvarchar(4),YEAR(a.dtFromArrangement)) + case when LEN (Convert(nvarchar(2),datepart(mm,a.dtFromArrangement))) = 1 THEN '0' + Convert(nvarchar(2),datepart(mm,a.dtFromArrangement)) ELSE  Convert(nvarchar(2),datepart(mm,a.dtFromArrangement)) END 
             
            ");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;
            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;

            return conn.executeSelectQuery(query, null);
        }
        public DataTable DepartureList3(DateTime dtFrom, DateTime dtTo, int idArrType, int idLabel)
        {
            string query = string.Format(@"  SELECT a.dtFromArrangement as dateFrom, a.nameArrangement as arrangement, arr.bookedTravelers,a.nrTraveler as maxTravelers
            ,CASE WHEN (a.nrTraveler>0 and arr.bookedTravelers>0 ) THEN CONVERT(decimal(10,2), CONVERT(decimal(10,2),arr.bookedTravelers)/CONVERT(decimal(10,2),a.nrTraveler)*100) ELSE '0' END as Occupancy, space(20) AS dateFrom1, space(20) AS dateTo1
             FROM Arrangement a
             LEFT OUTER JOIN ( select a.idArrangement,COUNT(ab.idContPers) as bookedTravelers FROM Arrangement a 
                              LEFT OUTER JOIN  ArrangementBook ab ON ab.idArrangement = a.idArrangement 
                              WHERE ab.idStatus='1' or ab.idStatus='2'
             GROUP BY a.idArrangement) arr  ON arr.idArrangement = a.idArrangement            
             LEFT OUTER JOIN Country c on a.countryArrangement=c.idCountry
             LEFT OUTER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement
             LEFT OUTER JOIN Labels l on l.idLabel =  al.idLabel
            WHERE arr.bookedTravelers is not null and l.idLabel='" + idLabel + "' AND a.typeArrangement='" + idArrType + "'AND a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + "'order by dateFrom");

            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;
            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;
            sqlParameters[2] = new SqlParameter("@idArrType", SqlDbType.Int);
            sqlParameters[2].Value = idArrType;
            sqlParameters[3] = new SqlParameter("@idLabel", SqlDbType.Int);
            sqlParameters[3].Value = idLabel;
            return conn.executeSelectQuery(query, null);
        }
        public DataTable DepartureList3Ex(DateTime dtFrom, DateTime dtTo, int idArrType, int idLabel)
        {
            string query = string.Format(@"  SELECT a.dtFromArrangement as dateFrom, a.nameArrangement as arrangement, arr.bookedTravelers,a.nrTraveler as maxTravelers
            ,CASE WHEN (a.nrTraveler>0 and arr.bookedTravelers>0 ) THEN CONVERT(decimal(10,2), CONVERT(decimal(10,2),arr.bookedTravelers)/CONVERT(decimal(10,2),a.nrTraveler)*100) ELSE '0' END as Occupancy, space(20) AS dateFrom1, space(20) AS dateTo1
             FROM Arrangement a
             LEFT OUTER JOIN ( select a.idArrangement,COUNT(ab.idContPers) as bookedTravelers FROM Arrangement a 
                              LEFT OUTER JOIN  ArrangementBook ab ON ab.idArrangement = a.idArrangement 
                              WHERE ab.idStatus='2' 
             GROUP BY a.idArrangement) arr  ON arr.idArrangement = a.idArrangement            
             LEFT OUTER JOIN Country c on a.countryArrangement=c.idCountry
             LEFT OUTER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement
             LEFT OUTER JOIN Labels l on l.idLabel =  al.idLabel
            WHERE arr.bookedTravelers is not null and l.idLabel='" + idLabel + "' AND a.typeArrangement='" + idArrType + "'AND a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + "'order by dateFrom");

            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;
            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;
            sqlParameters[2] = new SqlParameter("@idArrType", SqlDbType.Int);
            sqlParameters[2].Value = idArrType;
            sqlParameters[3] = new SqlParameter("@idLabel", SqlDbType.Int);
            sqlParameters[3].Value = idLabel;
            return conn.executeSelectQuery(query, null);
        }
        public DataTable DepartureList4(DateTime dtFrom, DateTime dtTo, int idArrType, string provision, int idLabel)
        {
            string query = string.Format(@" SELECT a.dtFromArrangement as dateFrom, a.nameArrangement+' '+ a.codeArrangement as arrangement, arr.bookedTravelers,a.nrTraveler as maxTravelers
            , CASE WHEN (a.nrTraveler>0 and arr.bookedTravelers>0 ) THEN CONVERT(decimal(10,2), CONVERT(decimal(10,2),arr.bookedTravelers)/CONVERT(decimal(10,2),a.nrTraveler)*100) ELSE '0' END as Occupancy, space(20) AS dateFrom1, space(20) AS dateTo1
             FROM Arrangement a
             LEFT OUTER JOIN ( select a.idArrangement,COUNT(ab.idContPers) as bookedTravelers FROM Arrangement a 
                              LEFT OUTER JOIN  ArrangementBook ab ON ab.idArrangement = a.idArrangement 
                              WHERE ab.idStatus='1' or ab.idStatus='2'
             GROUP BY a.idArrangement) arr  ON arr.idArrangement = a.idArrangement            
             LEFT OUTER JOIN Country c on a.countryArrangement=c.idCountry
             LEFT OUTER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement
             LEFT OUTER JOIN Labels l on l.idLabel =  al.idLabel
             WHERE arr.bookedTravelers is not null and c.provision='" + provision + "' AND l.idLabel='" + idLabel + "' AND a.typeArrangement='" + idArrType + "'AND a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + "'order by dateFrom");
            // LEFT OUTER JOIN (SELECT COUNT (idContPers)as bookedTravelers,idArrangement FROM ArrangementBook GROUP by idArrangement) ab on a.idArrangement=ab.idArrangement
            SqlParameter[] sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;
            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;
            sqlParameters[2] = new SqlParameter("@idArrType", SqlDbType.Int);
            sqlParameters[2].Value = idArrType;
            sqlParameters[3] = new SqlParameter("@provision", SqlDbType.Int);
            sqlParameters[3].Value = provision;
            sqlParameters[4] = new SqlParameter("@idLabel", SqlDbType.Int);
            sqlParameters[4].Value = idLabel;
            return conn.executeSelectQuery(query, null);
        }
        public DataTable DepartureList4Ex(DateTime dtFrom, DateTime dtTo, int idArrType, string provision, int idLabel)
        {
            string query = string.Format(@" SELECT a.dtFromArrangement as dateFrom, a.nameArrangement+' '+ a.codeArrangement as arrangement, arr.bookedTravelers,a.nrTraveler as maxTravelers
            , CASE WHEN (a.nrTraveler>0 and arr.bookedTravelers>0 ) THEN CONVERT(decimal(10,2), CONVERT(decimal(10,2),arr.bookedTravelers)/CONVERT(decimal(10,2),a.nrTraveler)*100) ELSE '0' END as Occupancy, space(20) AS dateFrom1, space(20) AS dateTo1
             FROM Arrangement a
             LEFT OUTER JOIN ( select a.idArrangement,COUNT(ab.idContPers) as bookedTravelers FROM Arrangement a 
                              LEFT OUTER JOIN  ArrangementBook ab ON ab.idArrangement = a.idArrangement 
                              WHERE  ab.idStatus='2'
             GROUP BY a.idArrangement) arr  ON arr.idArrangement = a.idArrangement            
             LEFT OUTER JOIN Country c on a.countryArrangement=c.idCountry
             LEFT OUTER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement
             LEFT OUTER JOIN Labels l on l.idLabel =  al.idLabel
             WHERE arr.bookedTravelers is not null and c.provision='" + provision + "' AND l.idLabel='" + idLabel + "' AND a.typeArrangement='" + idArrType + "'AND a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + "'order by dateFrom");
            // LEFT OUTER JOIN (SELECT COUNT (idContPers)as bookedTravelers,idArrangement FROM ArrangementBook GROUP by idArrangement) ab on a.idArrangement=ab.idArrangement
            SqlParameter[] sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;
            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;
            sqlParameters[2] = new SqlParameter("@idArrType", SqlDbType.Int);
            sqlParameters[2].Value = idArrType;
            sqlParameters[3] = new SqlParameter("@provision", SqlDbType.Int);
            sqlParameters[3].Value = provision;
            sqlParameters[4] = new SqlParameter("@idLabel", SqlDbType.Int);
            sqlParameters[4].Value = idLabel;
            return conn.executeSelectQuery(query, null);
        }

        public DataTable DepartureList1New(DateTime dtFrom, DateTime dtTo)
        {

            string query = string.Format(@"
                      
             SELECT DISTINCT l.nameLabel as Label,             
              CASE WHEN at.nameArrType IS NULL THEN '' ELSE at.nameArrType END as Type,
              CASE WHEN NrType IS NULL THEN '' ELSE NrType END AS NrType,
             arr.bookedTravelers as bookedTravelers,ar.nrTravelers as maxTravelers,
             CASE WHEN (ar.nrTravelers>0 and arr.bookedTravelers>0 ) THEN CONVERT(decimal(10,2), CONVERT(decimal(10,2),arr.bookedTravelers)/CONVERT(decimal(10,2),ar.nrTravelers)*100) ELSE '0' END as Occupancy
             FROM Arrangement a

             INNER JOIN ArrType at on a.typeArrangement =  at.idArrType

             INNER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement
             LEFT OUTER JOIN Labels l on l.idLabel =  al.idLabel

             LEFT OUTER JOIN( SELECT  COUNT(idArrType)as NrType from ArrType art
             LEFT OUTER JOIN Arrangement a ON a.typeArrangement=art.idArrType                         
             GROUP BY nameArrType,idArrType) ac ON ac.NrType= a.typeArrangement

             INNER JOIN (SELECT DISTINCT idArrangement FROM ArrangementBook) ab ON ab.idArrangement = a.idArrangement
             LEFT OUTER JOIN (SELECT l.idLabel,typeArrangement,SUM(nrTraveler) as nrTravelers 
             FROM Arrangement a 
             LEFT OUTER JOIN ArrangementLabel l On l.idArrangement = a.idArrangement GROUP BY l.idLabel,typeArrangement) ar  ON ar.typeArrangement = a.typeArrangement AND ar.idLabel =  al.idLabel
             LEFT OUTER JOIN (SELECT l.idLabel,typeArrangement, COUNT(ab.idContPers) as bookedTravelers FROM Arrangement a 
             LEFT OUTER JOIN ArrangementLabel l On l.idArrangement = a.idArrangement
             LEFT OUTER JOIN  ArrangementBook ab ON ab.idArrangement = a.idArrangement and (ab.idStatus='1' or ab.idStatus='2')
          INNER JOIN (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter<>'4' ) cpf ON cpf.idContPers=ab.idContPers     
             WHERE   a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + @"'
             GROUP BY l.idLabel,typeArrangement) arr  ON arr.typeArrangement = a.typeArrangement AND arr.idLabel =  al.idLabel
             
             WHERE arr.bookedTravelers IS NOT NULL and a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + @"'
             ORDER BY  l.nameLabel");




            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;
            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;


            return conn.executeSelectQuery(query, null);

        }

        public DataTable DepartureList1ExNew(DateTime dtFrom, DateTime dtTo)
        {

            string query = string.Format(@"
                      
             SELECT DISTINCT l.nameLabel as Label,
             CASE WHEN at.nameArrType IS NULL THEN '' ELSE at.nameArrType END as Type,
CASE WHEN NrType IS NULL THEN '' ELSE NrType END AS NrType,
             arr.bookedTravelers as bookedTravelers,ar.nrTravelers as maxTravelers,
             CASE WHEN (ar.nrTravelers>0 and arr.bookedTravelers>0 ) THEN CONVERT(decimal(10,2), CONVERT(decimal(10,2),arr.bookedTravelers)/CONVERT(decimal(10,2),ar.nrTravelers)*100) ELSE '0' END as Occupancy
             FROM Arrangement a
             INNER JOIN ArrType at on a.typeArrangement =  at.idArrType
             INNER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement
             LEFT OUTER JOIN Labels l on l.idLabel =  al.idLabel

             LEFT OUTER JOIN( SELECT  COUNT(idArrType)as NrType from ArrType art
             LEFT OUTER JOIN Arrangement a ON a.typeArrangement=art.idArrType                         
             GROUP BY nameArrType,idArrType) ac ON ac.NrType= a.typeArrangement

             INNER JOIN (SELECT DISTINCT idArrangement FROM ArrangementBook) ab ON ab.idArrangement = a.idArrangement
             LEFT OUTER JOIN (SELECT l.idLabel,typeArrangement,SUM(nrTraveler) as nrTravelers 
             FROM Arrangement a LEFT OUTER JOIN ArrangementLabel l On l.idArrangement = a.idArrangement GROUP BY l.idLabel,typeArrangement) ar  ON ar.typeArrangement = a.typeArrangement AND ar.idLabel =  al.idLabel
             LEFT OUTER JOIN (SELECT l.idLabel,typeArrangement,COUNT(ab.idContPers) as bookedTravelers
             FROM Arrangement a 
             LEFT OUTER JOIN ArrangementLabel l On l.idArrangement = a.idArrangement 
             LEFT OUTER JOIN  ArrangementBook ab ON ab.idArrangement = a.idArrangement   AND (ab.idStatus='2')
            INNER JOIN (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter<>'4' ) cpf ON cpf.idContPers=ab.idContPers     
             WHERE  a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + @"'
             GROUP BY l.idLabel,typeArrangement) arr  ON arr.typeArrangement = a.typeArrangement AND arr.idLabel =  al.idLabel
             LEFT OUTER JOIN ArrangementBook abook on a.idArrangement= abook.idArrangement
             WHERE abook.idStatus='2' and a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + @"'
             ORDER BY  l.nameLabel");




            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;
            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;


            return conn.executeSelectQuery(query, null);

        }

        public DataTable DepartureList3New(DateTime dtFrom, DateTime dtTo, int idArrType, int idLabel, int idTheme)
        {
            string query = string.Format(@"  SELECT a.dtFromArrangement as dateFrom, a.nameArrangement as arrangement,a.minNrTraveler,a.nrTraveler as maxTravelers,arr.bookedTravelers as bookedTravelers,a.idArrangement
            ,CASE WHEN (a.nrTraveler>0 and arr.bookedTravelers>0 ) THEN CONVERT(decimal(10,2), CONVERT(decimal(10,2),arr.bookedTravelers)/CONVERT(decimal(10,2),a.nrTraveler)*100) ELSE '0' END as Occupancy
             FROM Arrangement a
               INNER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement
                LEFT OUTER JOIN Labels l on l.idLabel =  al.idLabel
             LEFT OUTER JOIN ( SELECT a.idArrangement,COUNT(ab.idContPers) as bookedTravelers FROM Arrangement a 
                              LEFT OUTER JOIN  ArrangementBook ab ON ab.idArrangement = a.idArrangement AND (ab.idStatus= '1' OR ab.idstatus='2')
                               LEFT OUTER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement
                        INNER  JOIN (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter<>'4' ) cpf ON cpf.idContPers=ab.idContPers      
                              WHERE  a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + @"'  
             GROUP BY a.idArrangement) arr  ON arr.idArrangement = a.idArrangement            
             LEFT OUTER JOIN Country c on a.countryArrangement=c.idCountry         
  LEFT OUTER JOIN ArrangementThemeTrip at ON at.idArrangement=a.idArrangement
             WHERE arr.bookedTravelers is not null and l.idLabel='" + idLabel + "' AND at.idThemeTrip='" + idTheme + "' AND a.typeArrangement='" + idArrType + "'AND a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + "'order by dateFrom");

            if (idTheme == 0)
            {
                query = string.Format(@"   SELECT a.dtFromArrangement as dateFrom, a.nameArrangement as arrangement,a.minNrTraveler,a.nrTraveler as maxTravelers,arr.bookedTravelers as bookedTravelers,a.idArrangement
            ,CASE WHEN (a.nrTraveler>0 and arr.bookedTravelers>0 ) THEN CONVERT(decimal(10,2), CONVERT(decimal(10,2),arr.bookedTravelers)/CONVERT(decimal(10,2),a.nrTraveler)*100) ELSE '0' END as Occupancy
             FROM Arrangement a
               INNER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement
                LEFT OUTER JOIN Labels l on l.idLabel =  al.idLabel
             LEFT OUTER JOIN ( SELECT a.idArrangement,COUNT(ab.idContPers) as bookedTravelers FROM Arrangement a 
                              LEFT OUTER JOIN  ArrangementBook ab ON ab.idArrangement = a.idArrangement AND (ab.idStatus= '1' OR ab.idstatus='2')
                               LEFT OUTER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement
                        INNER  JOIN (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter<>'4' ) cpf ON cpf.idContPers=ab.idContPers      
                              WHERE  a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + @"'  
             GROUP BY a.idArrangement) arr  ON arr.idArrangement = a.idArrangement            
             LEFT OUTER JOIN Country c on a.countryArrangement=c.idCountry         
  LEFT OUTER JOIN ArrangementThemeTrip at ON at.idArrangement=a.idArrangement
            WHERE arr.bookedTravelers is not null and l.idLabel= '" + idLabel + "'  AND a.typeArrangement= '" + idArrType + "'  AND a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + @"'
            order by dateFrom");
            }

            SqlParameter[] sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;
            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;
            sqlParameters[2] = new SqlParameter("@idArrType", SqlDbType.Int);
            sqlParameters[2].Value = idArrType;
            sqlParameters[3] = new SqlParameter("@idLabel", SqlDbType.Int);
            sqlParameters[3].Value = idLabel;

            sqlParameters[4] = new SqlParameter("@idThemeTrip", SqlDbType.Int);
            sqlParameters[4].Value = idTheme;


            return conn.executeSelectQuery(query, null);
        }
        public DataTable DepartureList3ExNew(DateTime dtFrom, DateTime dtTo, int idArrType, int idLabel, int idTheme)
        {
            string query = string.Format(@"   SELECT a.dtFromArrangement as dateFrom, a.nameArrangement as arrangement,a.minNrTraveler,a.nrTraveler as maxTravelers, arr.bookedTravelers
            ,CASE WHEN (a.nrTraveler>0 and arr.bookedTravelers>0 ) THEN CONVERT(decimal(10,2), CONVERT(decimal(10,2),arr.bookedTravelers)/CONVERT(decimal(10,2),a.nrTraveler)*100) ELSE '0' END as Occupancy
             FROM Arrangement a
             
               INNER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement
                LEFT OUTER JOIN Labels l on l.idLabel =  al.idLabel
             
             LEFT OUTER JOIN ( SELECT a.idArrangement,COUNT(ab.idContPers) as bookedTravelers FROM Arrangement a 
                              LEFT OUTER JOIN  ArrangementBook ab ON ab.idArrangement = a.idArrangement and (ab.idStatus='2')
                                LEFT OUTER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement 
                     INNER  JOIN (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter<>'4' ) cpf ON cpf.idContPers=ab.idContPers      
                              WHERE  a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + @"'
             GROUP BY a.idArrangement) arr  ON arr.idArrangement = a.idArrangement            
             LEFT OUTER JOIN Country c on a.countryArrangement=c.idCountry
             
  LEFT OUTER JOIN ArrangementThemeTrip at ON at.idArrangement=a.idArrangement
          WHERE arr.bookedTravelers is not null and l.idLabel='" + idLabel + "' AND at.idThemeTrip='" + idTheme + "' AND a.typeArrangement='" + idArrType + "'AND a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + "'order by dateFrom");


            if (idTheme == 0)
            {
                query = string.Format(@"   SELECT a.dtFromArrangement as dateFrom, a.nameArrangement as arrangement,a.minNrTraveler,a.nrTraveler as maxTravelers, arr.bookedTravelers
            ,CASE WHEN (a.nrTraveler>0 and arr.bookedTravelers>0 ) THEN CONVERT(decimal(10,2), CONVERT(decimal(10,2),arr.bookedTravelers)/CONVERT(decimal(10,2),a.nrTraveler)*100) ELSE '0' END as Occupancy
             FROM Arrangement a
             
               INNER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement
                LEFT OUTER JOIN Labels l on l.idLabel =  al.idLabel
             
             LEFT OUTER JOIN ( SELECT a.idArrangement,COUNT(ab.idContPers) as bookedTravelers FROM Arrangement a 
                              LEFT OUTER JOIN  ArrangementBook ab ON ab.idArrangement = a.idArrangement and (ab.idStatus='2')
                                LEFT OUTER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement 
                     INNER  JOIN (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter<>'4' ) cpf ON cpf.idContPers=ab.idContPers      
                              WHERE  a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + @"'
             GROUP BY a.idArrangement) arr  ON arr.idArrangement = a.idArrangement            
             LEFT OUTER JOIN Country c on a.countryArrangement=c.idCountry
             
  LEFT OUTER JOIN ArrangementThemeTrip at ON at.idArrangement=a.idArrangement
            WHERE arr.bookedTravelers is not null and l.idLabel='" + idLabel + "' AND a.typeArrangement='" + idArrType + "'AND a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + "'order by dateFrom");


            }


            SqlParameter[] sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;
            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;
            sqlParameters[2] = new SqlParameter("@idArrType", SqlDbType.Int);
            sqlParameters[2].Value = idArrType;
            sqlParameters[3] = new SqlParameter("@idLabel", SqlDbType.Int);
            sqlParameters[3].Value = idLabel;

            sqlParameters[4] = new SqlParameter("@idThemeTrip", SqlDbType.Int);
            sqlParameters[4].Value = idTheme;

            return conn.executeSelectQuery(query, null);
        }
        public DataTable DepartureList4New(DateTime dtFrom, DateTime dtTo, int idArrType, string provision, int idLabel)
        {
            string query = string.Format(@" SELECT a.dtFromArrangement as dateFrom, a.nameArrangement+' '+ a.codeArrangement as arrangement, arr.bookedTravelers,a.nrTraveler as maxTravelers
            , CASE WHEN (a.nrTraveler>0 and arr.bookedTravelers>0 ) THEN CONVERT(decimal(10,2), CONVERT(decimal(10,2),arr.bookedTravelers)/CONVERT(decimal(10,2),a.nrTraveler)*100) ELSE '0' END as Occupancy
             FROM Arrangement a

 LEFT OUTER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement
             LEFT OUTER JOIN Labels l on l.idLabel =  al.idLabel

             LEFT OUTER JOIN ( select a.idArrangement,COUNT(ab.idContPers) as bookedTravelers FROM Arrangement a 
                              LEFT OUTER JOIN  ArrangementBook ab ON ab.idArrangement = a.idArrangement AND (ab.idStatus= '1' OR ab.idstatus='2') 
                              LEFT OUTER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement

                         INNER  JOIN (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter<>'4' ) cpf ON cpf.idContPers=ab.idContPers      
                            where  a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and a.dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + @"'

             GROUP BY a.idArrangement) arr  ON arr.idArrangement = a.idArrangement            
             LEFT OUTER JOIN Country c on a.countryArrangement=c.idCountry
            
             WHERE arr.bookedTravelers is not null and c.provision='" + provision + "' AND l.idLabel='" + idLabel + "' AND a.typeArrangement='" + idArrType + "'AND a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + "'order by dateFrom");
            // LEFT OUTER JOIN (SELECT COUNT (idContPers)as bookedTravelers,idArrangement FROM ArrangementBook GROUP by idArrangement) ab on a.idArrangement=ab.idArrangement
            SqlParameter[] sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;
            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;
            sqlParameters[2] = new SqlParameter("@idArrType", SqlDbType.Int);
            sqlParameters[2].Value = idArrType;
            sqlParameters[3] = new SqlParameter("@provision", SqlDbType.Int);
            sqlParameters[3].Value = provision;
            sqlParameters[4] = new SqlParameter("@idLabel", SqlDbType.Int);
            sqlParameters[4].Value = idLabel;

            return conn.executeSelectQuery(query, null);
        }

        public DataTable DepartureList4ExNew(DateTime dtFrom, DateTime dtTo, int idArrType, string provision, int idLabel)
        {
            string query = string.Format(@"  SELECT a.dtFromArrangement as dateFrom, a.nameArrangement+' '+ a.codeArrangement as arrangement, arr.bookedTravelers,a.nrTraveler as maxTravelers
            , CASE WHEN (a.nrTraveler>0 and arr.bookedTravelers>0 ) THEN CONVERT(decimal(10,2), CONVERT(decimal(10,2),arr.bookedTravelers)/CONVERT(decimal(10,2),a.nrTraveler)*100) ELSE '0' END as Occupancy
             FROM Arrangement a
              LEFT OUTER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement
             LEFT OUTER JOIN Labels l on l.idLabel =  al.idLabel
             LEFT OUTER JOIN ( select a.idArrangement,COUNT(ab.idContPers) as bookedTravelers FROM Arrangement a 
                              LEFT OUTER JOIN  ArrangementBook ab ON ab.idArrangement = a.idArrangement and  (ab.idStatus='2')
                                LEFT OUTER JOIN ArrangementLabel al on a.idArrangement =  al.idArrangement 
                                   
                        INNER  JOIN (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter<>'4' ) cpf ON cpf.idContPers=ab.idContPers      
                              WHERE  a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and a.dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + @"'
                              
             GROUP BY a.idArrangement) arr  ON arr.idArrangement = a.idArrangement            
             LEFT OUTER JOIN Country c on a.countryArrangement=c.idCountry
            
            WHERE arr.bookedTravelers is not null and c.provision='" + provision + "' AND l.idLabel='" + idLabel + "' AND a.typeArrangement='" + idArrType + "'AND a.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + "' and a.dtFromArrangement<='" + dtTo.ToString("MM/dd/yyyy") + "'order by dateFrom");

            // LEFT OUTER JOIN (SELECT COUNT (idContPers)as bookedTravelers,idArrangement FROM ArrangementBook GROUP by idArrangement) ab on a.idArrangement=ab.idArrangement
            SqlParameter[] sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;
            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;
            sqlParameters[2] = new SqlParameter("@idArrType", SqlDbType.Int);
            sqlParameters[2].Value = idArrType;
            sqlParameters[3] = new SqlParameter("@provision", SqlDbType.Int);
            sqlParameters[3].Value = provision;
            sqlParameters[4] = new SqlParameter("@idLabel", SqlDbType.Int);
            sqlParameters[4].Value = idLabel;

            return conn.executeSelectQuery(query, null);
        }

        public DataTable CanceledOption(DateTime dtFromArrangement, DateTime ToArrangement)
        {
            string query = string.Format(@"SELECT CASE WHEN a.nameArrangement IS NULL THEN '' ELSE a.nameArrangement END as nameArrangement, 
                                         a.dtFromArrangement, a.dtToArrangement, CASE WHEN t.nameTitle IS NULL THEN '' ELSE t.nameTitle END as nameTitle, 
                                          CASE WHEN cpr.firstname IS NULL THEN '' ELSE  cpr.firstname END + ' ' + CASE WHEN cpr.midname IS NULL THEN  '' ELSE  cpr.midname END+ ' ' + CASE WHEN cpr.lastname IS NULL THEN  '' ELSE  cpr.lastname END AS name,
                                         cpr.idContPers,a.codeArrangement,
                                        space(20) AS dateFrom1, space(20) AS dateTo1
                       FROM ArrangementBook ab
                       LEFT OUTER JOIN ContactPerson cpr ON cpr.idContPers = ab.idContPers
                       LEFT OUTER JOIN Title t ON t.idTitle = cpr.idTitle
                       LEFT OUTER JOIN Arrangement a ON a.idArrangement = ab.idArrangement
                        WHERE  ab.idStatus='4' AND a.dtFromArrangement>='" + dtFromArrangement.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + ToArrangement.ToString("MM/dd/yyyy") + "'");


            SqlParameter[] sqlParameters = new SqlParameter[2];


            sqlParameters[0] = new SqlParameter("@dtFromArrangement", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFromArrangement;

            sqlParameters[1] = new SqlParameter("@ToArrangement", SqlDbType.DateTime);
            sqlParameters[1].Value = ToArrangement;


            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable AllArrType()
        {
            string query = string.Format(@"SELECT  distinct nameArrType,idArrType
                                           FROM ArrType order by nameArrType ");


            return conn.executeSelectQuery(query, null);
            //nameArrangement
        }

        public DataTable TelSrtatus(int idContPers, int idTelType)
        {
            string query = string.Format(@" select  cpt.numberTel,cpt.idTelType, tt.nameTelType  as descriptionTel        
                         from ContactPerson cp                             
                         LEFT OUTER JOIN ContactPersonTel cpt on cp.idContPers= cpt.idContPers      
                         LEFT OUTER JOIN TypesTel tt on tt.idTelType=cpt.idTelType                              
                         WHERE cpt.idContPers='" + idContPers + "' and (cpt.idTelType='" + idTelType + @"') order by tt.nameTelType");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;
            sqlParameters[1] = new SqlParameter("@idTelType", SqlDbType.Int);
            sqlParameters[1].Value = idTelType;




            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetAllArangementCode()
        {
            string query = string.Format(@"SELECT distinct idArrangement,codeArrangement,nameArrangement,dtFromArrangement, dtToArrangement 
                                            FROM Arrangement a
                                          ");
            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetTravelerIsInsurance(int idArrangement, int idContPers)
        {
            string query = string.Format(@" SELECT cp.firstname,cp.midname,cp.lastname, *
                                          FROM ArrangementBook at
                                          LEFT OUTER JOIN ContactPerson cp ON at.idContPers = cp.idContPers
                                          left join Title aa on aa.idTitle = cp.idTitle
                                          WHERE idArrangement = '" + idArrangement + @"' AND at.idContPers = '" + idContPers + "'");
            return conn.executeSelectQuery(query, null);
        }

        // NOVO 
        public DataTable NrOfOptionsBooked(List<int> CheckedArr, List<int> CheckedLabel, DateTime dateFrom, DateTime dateTo)
        {
            // tip aranzmana
            List<string> tableNameList = new List<string>();

            List<string> tableLongNameList = new List<string>();
            // za labele
            List<string> tableNameListL = new List<string>();

            List<string> tableLongNameListL = new List<string>();
            //
            if (CheckedArr.Count > 0)
            {
                tableLongNameList.Add("ArrType");
                tableNameList.Add("at");
            }
            if (CheckedLabel.Count > 0)
            {
                tableLongNameListL.Add("ArrangementLabel");
                tableNameListL.Add("al");
            }

            string condition1 = "";
            string condition2 = "";


            List<int> newList = new List<int>();


            if (tableNameList.Count == 1)
            {

                newList = CheckedArr;

                for (int i = 0; i < newList.Count; i++)
                {
                    if (i == 0)
                    {
                        condition1 = "AND (" + tableNameList[0] + ".idArrType='" + CheckedArr[i].ToString() + "')";
                    }
                    else
                    {
                        //OR u AND ili obrnuto
                        condition1 = condition1 + " OR (" + tableNameList[0] + ".idArrType='" + CheckedArr[i].ToString() + "')";
                    }
                }

                if (newList.Count > 1)
                    condition1 = "AND (" + condition1.Substring(3, condition1.Length - 3) + ")";



            }

            List<int> newListL = new List<int>();


            if (tableNameListL.Count == 1)
            {

                newListL = CheckedLabel;

                for (int i = 0; i < newListL.Count; i++)
                {
                    if (i == 0)
                    {
                        condition2 = "AND (" + tableNameListL[0] + ".idLabel='" + CheckedLabel[i].ToString() + "')";
                    }
                    else
                    {
                        //OR u AND ili obrnuto
                        condition2 = condition2 + " OR (" + tableNameListL[0] + ".idLabel='" + CheckedLabel[i].ToString() + "')";
                    }
                }

                if (newListL.Count > 1)
                    condition2 = "AND (" + condition2.Substring(3, condition2.Length - 3) + ")";


                // select  
            }


            string query = string.Format(@" SELECT distinct a.codeArrangement,a.dtFromArrangement as dateFrom, a.dtToArrangement as dateTo, l.nameLabel,a.idArrangement,at.nameArrType,
                                          a.nrTraveler,arr.bookedTravelers,
                                         space(20) AS dateFrom1, space(20) AS dateTo1 
                                         
                                        FROM  ( select a.idArrangement,COUNT(ab.idContPers) as bookedTravelers, al.idLabel
                                        FROM Arrangement a 
                                        LEFT OUTER JOIN  ArrangementBook ab ON ab.idArrangement = a.idArrangement
                                        LEFT OUTER JOIN ArrangementLabel al ON al.idArrangement = a.idArrangement
                                         WHERE dtBooked>='" + dateFrom.ToString("MM/dd/yyyy") + @"' AND dtBooked<='" + dateTo.ToString("MM/dd/yyyy") + @"'
                                        GROUP BY a.idArrangement,al.idLabel) arr
                                        LEFT OUTER JOIN ArrangementBook ab ON arr.idArrangement= ab.idArrangement
                                        LEFT OUTER JOIN Arrangement a ON a.idArrangement = ab.idArrangement
                                        LEFT OUTER JOIN ArrangementStatus ars on ars.idArrangementStatus= ab.idStatus
                                        LEFT OUTER JOIN ArrType at on at.idArrType=a.typeArrangement 
                                        LEFT OUTER JOIN ArrangementLabel al on  al.idArrangement=ab.idArrangement
                                        LEFT OUTER JOIN Labels l on l.id= al.idLabel
                                        WHERE  dtBooked>='" + dateFrom.ToString("MM/dd/yyyy") + @"' AND dtBooked<='" + dateTo.ToString("MM/dd/yyyy") + "'" + condition1 + condition2 +
   @"  GROUP BY  l.nameLabel,a.idArrangement,a.codeArrangement, at.nameArrType, a.dtFromArrangement, a.dtToArrangement, a.nrTraveler, arr.bookedTravelers,ab.dtBooked
    ORDER BY  l.nameLabel,a.dtFromArrangement");



            // HAVING count(" + tableNameList[0] + @".idArrType)='" + newList.Count + "'
            SqlParameter[] sqlParameters = new SqlParameter[2];


            sqlParameters[0] = new SqlParameter("@dateFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dateFrom;
            sqlParameters[1] = new SqlParameter("@dateTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dateTo;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable AllEmployee(DateTime dateFrom, DateTime dateTo)
        {
            string query = string.Format(@"SELECT distinct CASE WHEN e.firstNameEmployee IS NULL THEN '' ELSE  e.firstNameEmployee END + ' ' + CASE WHEN e.lastNameEmployee IS NULL THEN  '' ELSE  e.lastNameEmployee END AS firstNameEmployee, e.idEmployee
                                            FROM  ArrangementBook ab 
                                             INNER JOIN Users u on u.idUser= ab.idUserCreated
                                             LEFT OUTER JOIN Employees e on u.idEmployee= e.idEmployee
                                            where dtBooked>='" + dateFrom.ToString("MM/dd/yyyy") + @"' AND dtBooked<='" + dateTo.ToString("MM/dd/yyyy") + "'");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@dateFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dateFrom;

            sqlParameters[1] = new SqlParameter("@dateTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dateTo;


            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable NrOfOptionsEmployee(List<int> CheckedArr, List<int> CheckedLabel, DateTime dateFrom, DateTime dateTo, int idEmployee)
        {
            // tip aranzmana
            List<string> tableNameList = new List<string>();

            List<string> tableLongNameList = new List<string>();
            // za labele
            List<string> tableNameListL = new List<string>();

            List<string> tableLongNameListL = new List<string>();
            //
            if (CheckedArr.Count > 0)
            {
                tableLongNameList.Add("ArrType");
                tableNameList.Add("at");
            }
            if (CheckedLabel.Count > 0)
            {
                tableLongNameListL.Add("ArrangementLabel");
                tableNameListL.Add("al");
            }

            string condition1 = "";
            string condition2 = "";


            List<int> newList = new List<int>();


            if (tableNameList.Count == 1)
            {

                newList = CheckedArr;

                for (int i = 0; i < newList.Count; i++)
                {
                    if (i == 0)
                    {
                        condition1 = "AND (" + tableNameList[0] + ".idArrType='" + CheckedArr[i].ToString() + "')";
                    }
                    else
                    {
                        //OR u AND ili obrnuto
                        condition1 = condition1 + " OR (" + tableNameList[0] + ".idArrType='" + CheckedArr[i].ToString() + "')";
                    }
                }

                if (newList.Count > 1)
                    condition1 = "AND (" + condition1.Substring(3, condition1.Length - 3) + ")";



            }

            List<int> newListL = new List<int>();


            if (tableNameListL.Count == 1)
            {

                newListL = CheckedLabel;

                for (int i = 0; i < newListL.Count; i++)
                {
                    if (i == 0)
                    {
                        condition2 = "AND (" + tableNameListL[0] + ".idLabel='" + CheckedLabel[i].ToString() + "')";
                    }
                    else
                    {
                        //OR u AND ili obrnuto
                        condition2 = condition2 + " OR (" + tableNameListL[0] + ".idLabel='" + CheckedLabel[i].ToString() + "')";
                    }
                }

                if (newListL.Count > 1)
                    condition2 = "AND (" + condition2.Substring(3, condition2.Length - 3) + ")";


                // select  
            }


            string query = string.Format(@"SELECT a.codeArrangement,a.dtFromArrangement as dateFrom, a.dtToArrangement as dateTo,l.nameLabel,a.idArrangement, at.nameArrType,
                                          a.nrTraveler, ab.bookedTravelers, CASE WHEN e.firstNameEmployee + ' ' + e.midNameEmployee + ' ' + e.lastNameEmployee IS NULL THEN '' ELSE e.firstNameEmployee + ' ' + e.midNameEmployee + ' ' + e.lastNameEmployee END as Employee,
                                         space(20) AS dateFrom1, space(20) AS dateTo1  , space (50) AS Employee1
                                        FROM (select a.idArrangement,COUNT(abb.idContPers) as bookedTravelers, al.idLabel
                                        FROM Arrangement a 
                                        LEFT OUTER JOIN  ArrangementBook abb ON abb.idArrangement = a.idArrangement
                                        LEFT OUTER JOIN ArrangementLabel al ON al.idArrangement = a.idArrangement
                                        LEFT OUTER JOIN Users uu on uu.idUser= abb.idUserCreated
                                        LEFT OUTER JOIN Employees ee on ee.idemployee= uu.idEmployee
                                        WHERE ee.idEmployee='" + idEmployee + "' AND dtBooked>='" + dateFrom.ToString("MM/dd/yyyy") + @"' AND dtBooked<='" + dateTo.ToString("MM/dd/yyyy") + @"'
                                        GROUP BY a.idArrangement,al.idLabel) ab
                                        LEFT OUTER JOIN Arrangement a ON a.idArrangement = ab.idArrangement
                                        LEFT OUTER JOIN ArrangementBook abb on abb.idArrangement= a.idArrangement
                                        LEFT OUTER JOIN ArrangementStatus ars on ars.idArrangementStatus= abb.idStatus
                                        LEFT OUTER JOIN ArrType at on at.idArrType=a.typeArrangement 
                                        LEFT OUTER JOIN ArrangementLabel al on  al.idArrangement=ab.idArrangement
                                        LEFT OUTER JOIN Labels l on l.id= al.idLabel
                                        LEFT OUTER JOIN Users u on u.idUser= abb.idUserCreated
                                        LEFT OUTER JOIN Employees e on e.idemployee= u.idEmployee 
                                        WHERE  abb.dtBooked>='" + dateFrom.ToString("MM/dd/yyyy") + "' and abb.dtBooked<='" + dateTo.ToString("MM/dd/yyyy") + "' AND e.idEmployee='" + idEmployee + "'" + condition1 + condition2 +
            @"   GROUP BY  l.nameLabel,a.idArrangement,a.codeArrangement, at.nameArrType, a.dtFromArrangement, a.dtToArrangement, a.nrTraveler, ab.bookedTravelers, e.firstNameEmployee , e.midNameEmployee , e.lastNameEmployee
              ORDER BY  l.nameLabel,a.dtFromArrangement ");






            // HAVING count(" + tableNameList[0] + @".idArrType)='" + newList.Count + "'
            SqlParameter[] sqlParameters = new SqlParameter[2];


            sqlParameters[0] = new SqlParameter("@dateFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dateFrom;
            sqlParameters[1] = new SqlParameter("@dateTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dateTo;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable NrOfOptionsCanceled(List<int> CheckedArr, List<int> CheckedLabel, DateTime dateFrom, DateTime dateTo)
        {
            // tip aranzmana
            List<string> tableNameList = new List<string>();

            List<string> tableLongNameList = new List<string>();
            // za labele
            List<string> tableNameListL = new List<string>();

            List<string> tableLongNameListL = new List<string>();
            //
            if (CheckedArr.Count > 0)
            {
                tableLongNameList.Add("ArrType");
                tableNameList.Add("at");
            }
            if (CheckedLabel.Count > 0)
            {
                tableLongNameListL.Add("ArrangementLabel");
                tableNameListL.Add("al");
            }

            string condition1 = "";
            string condition2 = "";


            List<int> newList = new List<int>();


            if (tableNameList.Count == 1)
            {

                newList = CheckedArr;

                for (int i = 0; i < newList.Count; i++)
                {
                    if (i == 0)
                    {
                        condition1 = "AND (" + tableNameList[0] + ".idArrType='" + CheckedArr[i].ToString() + "')";
                    }
                    else
                    {
                        //OR u AND ili obrnuto
                        condition1 = condition1 + " OR (" + tableNameList[0] + ".idArrType='" + CheckedArr[i].ToString() + "')";
                    }
                }

                if (newList.Count > 1)
                    condition1 = "AND (" + condition1.Substring(3, condition1.Length - 3) + ")";



            }

            List<int> newListL = new List<int>();


            if (tableNameListL.Count == 1)
            {

                newListL = CheckedLabel;

                for (int i = 0; i < newListL.Count; i++)
                {
                    if (i == 0)
                    {
                        condition2 = "AND (" + tableNameListL[0] + ".idLabel='" + CheckedLabel[i].ToString() + "')";
                    }
                    else
                    {
                        //OR u AND ili obrnuto
                        condition2 = condition2 + " OR (" + tableNameListL[0] + ".idLabel='" + CheckedLabel[i].ToString() + "')";
                    }
                }

                if (newListL.Count > 1)
                    condition2 = "AND (" + condition2.Substring(3, condition2.Length - 3) + ")";


                // select  
            }


            string query = string.Format(@"SELECT a.codeArrangement,a.dtFromArrangement as dateFrom, a.dtToArrangement as dateTo,l.nameLabel,a.idArrangement, at.nameArrType,
                                          a.nrTraveler, ab.Canceled,
                                         space(20) AS dateFrom1, space(20) AS dateTo1
                                        FROM Arrangement a
                                        LEFT OUTER JOIN  (select count(idArrangement) as Canceled, idArrangement FROM ArrangementBook where idStatus='4'  GROUP BY idArrangement)  
                                        ab ON a.idArrangement = ab.idArrangement
                                        LEFT OUTER JOIN ArrType at on at.idArrType=a.typeArrangement 
                                        LEFT OUTER JOIN ArrangementLabel al on  al.idArrangement=ab.idArrangement
                                        LEFT OUTER JOIN ArrangementStatus ass on a.statusArrangement=ass.nameArrangementStatus 
                                        LEFT OUTER JOIN Labels l on l.id= al.idLabel
                                        WHERE ass.idArrangementStatus= '9' and a.dtFromArrangement>='" + dateFrom.ToString("MM/dd/yyyy") + "' and dtFromArrangement<='" + dateTo.ToString("MM/dd/yyyy") + "'" + condition1 + condition2 +
            @"   GROUP BY  l.nameLabel,a.idArrangement,a.codeArrangement, at.nameArrType, a.dtFromArrangement, a.dtToArrangement, a.nrTraveler, ab.Canceled
               ORDER BY  l.nameLabel,a.dtFromArrangement ");

            SqlParameter[] sqlParameters = new SqlParameter[2];


            sqlParameters[0] = new SqlParameter("@dateFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dateFrom;
            sqlParameters[1] = new SqlParameter("@dateTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dateTo;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public Boolean SaveRemaining(ArrangementRemainingModel model)
        {
            string query = string.Format(@"INSERT INTO ArrangementRemaining (idArrangement,awayDt,awayDt2,awayAirport,awayAirport2,awayFlightNr,awayFlightNr2,arrivalDt ,arrivalDt2,arrivalAirport,arrivalAirport2,backDt, backDt2,backAirport,backAirport2 ,backFlightNr,backFlightNr2,arrivalDt3,arrivalDt4,arrivalAirport3,arrivalAirport4,collectTime,airportSociety,special,twoFlight, program, letter, letterImage, rulesAppointment) 
                      VALUES(@idArrangement, @awayDt,@awayDt2 ,@awayAirport,@awayAirport2 ,@awayFlightNr,@awayFlightNr2,@arrivalDt ,@arrivalDt2,@arrivalAirport,@arrivalAirport2,@backDt, @backDt2,@backAirport,@backAirport2,@backFlightNr,@backFlightNr2,@arrivalDt3,@arrivalDt4,@arrivalAirport3,@arrivalAirport4,@collectTime,@airportSociety,@special,@twoFlight, @program , @letter, @letterImage, @rulesAppointment)");


            SqlParameter[] sqlParameters = new SqlParameter[29];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = model.idArrangement;

            sqlParameters[1] = new SqlParameter("@awayDt", SqlDbType.DateTime);
            sqlParameters[1].Value = model.awayDt;

            sqlParameters[2] = new SqlParameter("@awayDt2", SqlDbType.DateTime);
            sqlParameters[2].Value = model.awayDt2;

            sqlParameters[3] = new SqlParameter("@awayAirport", SqlDbType.NVarChar);
            sqlParameters[3].Value = model.awayAirport;

            sqlParameters[4] = new SqlParameter("@awayAirport2", SqlDbType.NVarChar);
            sqlParameters[4].Value = model.awayAirport2;

            sqlParameters[5] = new SqlParameter("@awayFlightNr", SqlDbType.NVarChar);
            sqlParameters[5].Value = model.awayFlightNr;

            sqlParameters[6] = new SqlParameter("@awayFlightNr2", SqlDbType.NVarChar);
            sqlParameters[6].Value = model.awayFlightNr2;

            sqlParameters[7] = new SqlParameter("@arrivalDt", SqlDbType.DateTime);
            sqlParameters[7].Value = model.arrivalDt;

            sqlParameters[8] = new SqlParameter("@arrivalDt2", SqlDbType.DateTime);
            sqlParameters[8].Value = model.arrivalDt2;

            sqlParameters[9] = new SqlParameter("@arrivalAirport", SqlDbType.NVarChar);
            sqlParameters[9].Value = model.arrivalAirport;

            sqlParameters[10] = new SqlParameter("@arrivalAirport2", SqlDbType.NVarChar);
            sqlParameters[10].Value = model.arrivalAirport2;

            sqlParameters[11] = new SqlParameter("@backDt", SqlDbType.DateTime);
            sqlParameters[11].Value = model.backDt;

            sqlParameters[12] = new SqlParameter("@backDt2", SqlDbType.DateTime);
            sqlParameters[12].Value = model.backDt2;

            sqlParameters[13] = new SqlParameter("@backAirport", SqlDbType.NVarChar);
            sqlParameters[13].Value = model.backAirport;

            sqlParameters[14] = new SqlParameter("@backAirport2", SqlDbType.NVarChar);
            sqlParameters[14].Value = model.awayAirport2;

            sqlParameters[15] = new SqlParameter("@backFlightNr", SqlDbType.NVarChar);
            sqlParameters[15].Value = model.awayFlightNr;

            sqlParameters[16] = new SqlParameter("@backFlightNr2", SqlDbType.NVarChar);
            sqlParameters[16].Value = model.awayFlightNr2;

            sqlParameters[17] = new SqlParameter("@arrivalDt3", SqlDbType.DateTime);
            sqlParameters[17].Value = model.arrivalDt3;

            sqlParameters[18] = new SqlParameter("@arrivalDt4", SqlDbType.DateTime);
            sqlParameters[18].Value = model.arrivalDt4;

            sqlParameters[19] = new SqlParameter("@arrivalAirport3", SqlDbType.NVarChar);
            sqlParameters[19].Value = model.arrivalAirport3;

            sqlParameters[20] = new SqlParameter("@arrivalAirport4", SqlDbType.NVarChar);
            sqlParameters[20].Value = model.arrivalAirport4;

            sqlParameters[21] = new SqlParameter("@collectTime", SqlDbType.NVarChar);
            sqlParameters[21].Value = model.collectTime;

            sqlParameters[22] = new SqlParameter("@airportSociety", SqlDbType.NVarChar);
            sqlParameters[22].Value = model.airportSociety;

            sqlParameters[23] = new SqlParameter("@special", SqlDbType.NVarChar);
            sqlParameters[23].Value = model.special;

            sqlParameters[24] = new SqlParameter("@twoFlight", SqlDbType.Bit);
            sqlParameters[24].Value = model.twoFlight;

            sqlParameters[25] = new SqlParameter("@program", SqlDbType.NVarChar);
            sqlParameters[25].Value = model.program;

            sqlParameters[26] = new SqlParameter("@letter", SqlDbType.NVarChar);
            sqlParameters[26].Value = model.letter;

            sqlParameters[27] = new SqlParameter("@letterImage", SqlDbType.NVarChar);
            sqlParameters[27].Value = model.letterImage;

            sqlParameters[28] = new SqlParameter("@rulesAppointment", SqlDbType.NVarChar);
            sqlParameters[28].Value = model.rulesAppointment;

            return conn.executeInsertQuery(query, sqlParameters);

        }

        public Boolean UpdateRemaining(ArrangementRemainingModel model)
        {
            string query = string.Format(@"UPDATE ArrangementRemaining SET awayDt=@awayDt,awayDt2=@awayDt2,awayAirport=@awayAirport,awayAirport2=@awayAirport2,
                                        awayFlightNr=@awayFlightNr,awayFlightNr2=@awayFlightNr2,arrivalDt=@arrivalDt ,arrivalDt2=@arrivalDt2,arrivalAirport=@arrivalAirport,
                                        arrivalAirport2=@arrivalAirport2,backDt=@backDt, backDt2=@backDt2,backAirport=@backAirport,backAirport2=@backAirport2 ,
                                        backFlightNr=@backFlightNr,backFlightNr2=@backFlightNr2,arrivalDt3=@arrivalDt3,arrivalDt4=@arrivalDt4,arrivalAirport3=@arrivalAirport3,
                                        arrivalAirport4=@arrivalAirport4,collectTime=@collectTime,airportSociety=@airportSociety,special=@special,twoFlight=@twoFlight, program=@program, letter=@letter, letterImage=@letterImage, rulesAppointment=@rulesAppointment
                                        WHERE  idArrangement = @idArrangement");


            SqlParameter[] sqlParameters = new SqlParameter[29];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = model.idArrangement;

            sqlParameters[1] = new SqlParameter("@awayDt", SqlDbType.DateTime);
            sqlParameters[1].Value = model.awayDt;

            sqlParameters[2] = new SqlParameter("@awayDt2", SqlDbType.DateTime);
            sqlParameters[2].Value = model.awayDt2;

            sqlParameters[3] = new SqlParameter("@awayAirport", SqlDbType.NVarChar);
            sqlParameters[3].Value = model.awayAirport;

            sqlParameters[4] = new SqlParameter("@awayAirport2", SqlDbType.NVarChar);
            sqlParameters[4].Value = model.awayAirport2;

            sqlParameters[5] = new SqlParameter("@awayFlightNr", SqlDbType.NVarChar);
            sqlParameters[5].Value = model.awayFlightNr;

            sqlParameters[6] = new SqlParameter("@awayFlightNr2", SqlDbType.NVarChar);
            sqlParameters[6].Value = model.awayFlightNr2;

            sqlParameters[7] = new SqlParameter("@arrivalDt", SqlDbType.DateTime);
            sqlParameters[7].Value = model.arrivalDt;

            sqlParameters[8] = new SqlParameter("@arrivalDt2", SqlDbType.DateTime);
            sqlParameters[8].Value = model.arrivalDt2;

            sqlParameters[9] = new SqlParameter("@arrivalAirport", SqlDbType.NVarChar);
            sqlParameters[9].Value = model.arrivalAirport;

            sqlParameters[10] = new SqlParameter("@arrivalAirport2", SqlDbType.NVarChar);
            sqlParameters[10].Value = model.arrivalAirport2;

            sqlParameters[11] = new SqlParameter("@backDt", SqlDbType.DateTime);
            sqlParameters[11].Value = model.backDt;

            sqlParameters[12] = new SqlParameter("@backDt2", SqlDbType.DateTime);
            sqlParameters[12].Value = model.backDt2;

            sqlParameters[13] = new SqlParameter("@backAirport", SqlDbType.NVarChar);
            sqlParameters[13].Value = model.backAirport;

            sqlParameters[14] = new SqlParameter("@backAirport2", SqlDbType.NVarChar);
            sqlParameters[14].Value = model.awayAirport2;

            sqlParameters[15] = new SqlParameter("@backFlightNr", SqlDbType.NVarChar);
            sqlParameters[15].Value = model.awayFlightNr;

            sqlParameters[16] = new SqlParameter("@backFlightNr2", SqlDbType.NVarChar);
            sqlParameters[16].Value = model.awayFlightNr2;

            sqlParameters[17] = new SqlParameter("@arrivalDt3", SqlDbType.DateTime);
            sqlParameters[17].Value = model.arrivalDt3;

            sqlParameters[18] = new SqlParameter("@arrivalDt4", SqlDbType.DateTime);
            sqlParameters[18].Value = model.arrivalDt4;

            sqlParameters[19] = new SqlParameter("@arrivalAirport3", SqlDbType.NVarChar);
            sqlParameters[19].Value = model.arrivalAirport3;

            sqlParameters[20] = new SqlParameter("@arrivalAirport4", SqlDbType.NVarChar);
            sqlParameters[20].Value = model.arrivalAirport4;

            sqlParameters[21] = new SqlParameter("@collectTime", SqlDbType.NVarChar);
            sqlParameters[21].Value = model.collectTime;

            sqlParameters[22] = new SqlParameter("@airportSociety", SqlDbType.NVarChar);
            sqlParameters[22].Value = model.airportSociety;

            sqlParameters[23] = new SqlParameter("@special", SqlDbType.NVarChar);
            sqlParameters[23].Value = model.special;

            sqlParameters[24] = new SqlParameter("@twoFlight", SqlDbType.Bit);
            sqlParameters[24].Value = model.twoFlight;

            sqlParameters[25] = new SqlParameter("@program", SqlDbType.NVarChar);
            sqlParameters[25].Value = model.program;

            sqlParameters[26] = new SqlParameter("@letter", SqlDbType.NVarChar);
            sqlParameters[26].Value = model.letter;

            sqlParameters[27] = new SqlParameter("@letterImage", SqlDbType.NVarChar);
            sqlParameters[27].Value = model.letterImage;

            sqlParameters[28] = new SqlParameter("@rulesAppointment", SqlDbType.NVarChar);
            sqlParameters[28].Value = model.rulesAppointment;

            return conn.executeUpdateQuery(query, sqlParameters);

        }

        public DataTable isInArrangementRemaining(int idArrangement)
        {
            string query = string.Format(@"SELECT idArrangement
                                            FROM  ArrangementRemaining ab 
                                            where idArrangement ='" + idArrangement + "'");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;


            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable isCheckedTwoFlight(int idArrangement)
        {
            string query = string.Format(@"SELECT twoFlight, idArrangement
                                            FROM  ArrangementRemaining ab 
                                            where idArrangement='" + idArrangement + "'");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;


            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable getArrangementRemaining(int idArrangement)
        {
            string query = string.Format(@"SELECT awayDt,awayDt2,awayAirport,awayAirport2,awayFlightNr,awayFlightNr2,arrivalDt ,arrivalDt2,arrivalAirport,arrivalAirport2,backDt, backDt2,backAirport,backAirport2 ,backFlightNr,backFlightNr2,arrivalDt3,arrivalDt4,arrivalAirport3,arrivalAirport4,collectTime,airportSociety,special,twoFlight, program, letter, letterImage, rulesAppointment
                                            FROM  ArrangementRemaining ab 
                                            where idArrangement='" + idArrangement + "'");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;


            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable isAutitravelChecked(int idArrangement)
        {
            string query = string.Format(@"SELECT distinct a.idArrangement 
                                  FROM Arrangement a
                                  LEFT JOIN ArrangementLabel al on a.idArrangement=al.idArrangement
                                  INNER JOIN (SELECT CASE WHEN id IS NOT NULL THEN id ELSE idLabel END as id, nameLabel FROM Labels WHERE id ='3')l ON l.id= al.idLabel
                                  WHERE l.id='3' AND a.idArrangement='" + idArrangement + "'");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;


            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetPassingersForInvoicing(int idArrangement, int idPayInvoice)
        {
            string query = string.Format(@"SELECT * 
                                  FROM ArrangementBook  WHERE idArrangement= @idArrangement and idPayInvoice= @idPayInvoice AND idStatus <>'4'");
                                
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            sqlParameters[1] = new SqlParameter("@idPayInvoice", SqlDbType.Int);
            sqlParameters[1].Value = idPayInvoice;


            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable getMinDatePriceList(int idArrangement)
        {
            string query = string.Format(@"SELECT min(dtPriceList) as minDate
                                           FROM PriceList
                                           WHERE idArrangement  = @idArrangement and isReleaseDate = 1
                                           GROUP BY idArrangement");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetOverviewBooking(DateTime dtFrom, DateTime dtTo, int statusArrangement, int idArrangement, List<int> label, string travelPapers)
        {
            string condition = "";
            string conditionLabel = "";
            string conditionTravelPapers = "";

            #region status n name
            if (statusArrangement != -1)
            {
                condition += " AND ab.idStatus ='" + statusArrangement + "' ";
            }
            if (idArrangement != -1)
            {
                condition += " AND a.idArrangement='" + idArrangement + "' ";
            }
            if (!(travelPapers == "" || travelPapers == "None"))
            {
                conditionTravelPapers += " and abtp.nameTravelPapers = '" + travelPapers + "' ";
            }
            #endregion

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
                                conditionLabel = " AND (" + conditionLabel + " al.idLabel = '" + idLabel.ToString() + "')";
                            }
                            else
                            {
                                conditionLabel += " al.idLabel = '" + idLabel.ToString() + "' OR ";
                            }

                        count++;
                    }
                }
            #endregion




            string query = string.Format(@"SELECT cp.idContPers, CASE WHEN cp.firstname IS NULL THEN '' ELSE cp.firstname END + ' '+ 
                                           CASE WHEN cp.midname IS NULL THEN '' ELSE cp.midname END + ' ' +  CASE WHEN cp.lastname IS NULL THEN '' ELSE cp.lastname END as fullname,
                                           a.nameArrangement, a.dtFromArrangement, u.nameUser, ab.dtUserCreated, abst.nameStatus as status,abtp.nameTravelPapers
                                           FROM ArrangementBook ab
                                           left join Arrangement a on a.idArrangement = ab.idArrangement
                                           left join ContactPerson cp on cp.idContPers = ab.idContPers
                                           left join ArrangementBookTravelPapers abtp on abtp.idTravelPapers = ab.idTravelPapers   
                                           left join ArrangementLabel al on al.idArrangement = a.idArrangement
                                           left join ArrangementBookStatus abst on abst.idStatus= ab.idStatus
                                           left join Users u on u.idUser=ab.idUserCreated
                                           where a.dtFromArrangement>@dtFrom and a.dtToArrangement<@dtTo" + condition + conditionLabel + conditionTravelPapers +
                                           @" group by cp.idContPers,abtp.nameTravelPapers, CASE WHEN cp.firstname IS NULL THEN '' ELSE cp.firstname END + ' '+ 
                                           CASE WHEN cp.midname IS NULL THEN '' ELSE cp.midname END + ' ' +  CASE WHEN cp.lastname IS NULL THEN '' ELSE cp.lastname END, a.nameArrangement, a.dtFromArrangement,  ab.idUserCreated, ab.dtUserCreated , u.nameUser, abst.nameStatus");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;
            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable getArrangementDate(int idArrangement)
        {
            string query = string.Format(@"SELECT dtFromArrangement,dtToArrangement
                                            FROM  Arrangement a 
                                            where idArrangement='" + idArrangement + "'");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;


            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetSkillForPerson(int idContPerson)
        {
            string query = string.Format(@"SELECT distinct(CASE WHEN ma.idQuestSkills is not null  or ma.idQuestSkills<>'' THEN ma.idQuestSkills ELSE '' END )as idQuestSkills 
                           FROM MedCpr mc 
                           INNER  JOIN MedAns ma on ma.idQuest=mc.idQuest and ma.idAns=mc.idAns                       
                            WHERE idcpr= '" + idContPerson + "'  AND ma.idQuestSkills<>''");
            return conn.executeSelectQuery(query, null);
        }
        public Boolean MedLookupSriptSave(int idContPers, int idArrangement, List<MedicalVoluntaryQuestModel> idQuest)
        {

            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM MedLookup WHERE idContPers = @idContPers AND idArrangement=@idArrangement");


            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[0].Value = idContPers;

            sqlParameter[1] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[1].Value = idArrangement;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            if (idQuest != null)
            {
                for (int i = 0; i < idQuest.Count; i++)
                {
                    query = string.Format(@"INSERT INTO MedLookup (idArrangement,idContPers,idQuestSkill,type) 
                      VALUES(@idArrangement,@idContPers,@idQuestSkill,'M')");

                    sqlParameter = new SqlParameter[3];
                    sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
                    sqlParameter[0].Value = idArrangement;

                    sqlParameter[1] = new SqlParameter("@idContPers", SqlDbType.Int);
                    sqlParameter[1].Value = idContPers;

                    sqlParameter[2] = new SqlParameter("@idQuestSkill", SqlDbType.Int);
                    sqlParameter[2].Value = idQuest[i].idQuest;



                    _query.Add(query);
                    sqlParameters.Add(sqlParameter);

                }
            }



            return conn.executQueryTransaction(_query, sqlParameters);

        }
        // broj po skilu za taj aranzman        
        public DataTable GetNrForSkillsArrangement(int idArrangement)
        {
            string query = string.Format(@" SELECT m.nr, m.idQuest, (SELECT TOP 1 idAns FROM VolAns v where idQuest = m.idQuest) as idAns
                                  FROM (SELECT count(dd.idQuest)as nr, dd.idQuest
                                        FROM( SELECT (CASE WHEN idQuestSkill is not null  or idQuestSkill<>'' THEN idQuestSkill ELSE '' END )as idQuest 
                                              FROM  MedLookup 
                                              WHERE idQuestSkill<>'' and type='M' and idArrangement='" + idArrangement + @"') dd
                                        GROUP BY dd.idQuest) m");



            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAllSkillsVolArr(int idArrangement)
        {

            string query = string.Format(@"SELECT txt, idQuest, idArr
                                        FROM VolArr 
                                        WHERE idArr='" + idArrangement + "'");
            //                          
            return conn.executeSelectQuery(query, null);
        }
        public Boolean SaveVolArr(MedicalVoluntaryQuestModel listSkill, int idArr)
        {
            string query = string.Format(@"INSERT INTO VolArr (idArr,idQuest, idAns, txt) 
                      VALUES(@idArr,@idQuest, @idAns, @txt)");

            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@idArr", SqlDbType.Int);
            sqlParameters[0].Value = idArr;

            sqlParameters[1] = new SqlParameter("@idQuest", SqlDbType.Int);
            sqlParameters[1].Value = listSkill.idQuest;

            sqlParameters[2] = new SqlParameter("@idAns", SqlDbType.Int);
            sqlParameters[2].Value = listSkill.idAns;

            sqlParameters[3] = new SqlParameter("@txt", SqlDbType.Text);
            sqlParameters[3].Value = listSkill.nameQuestGroup;



            return conn.executeInsertQuery(query, sqlParameters);

        }
        public Boolean UpdateVolArr(MedicalVoluntaryQuestModel listSkill, string txt, int idQuest, int idArr)
        {
            string query = string.Format(@"UPDATE VolArr SET txt = @txt
                                        WHERE  idArr=@idArr AND idQuest=@idQuest");


            SqlParameter[] sqlParameters = new SqlParameter[3];


            sqlParameters[0] = new SqlParameter("@idArr", SqlDbType.Int);
            sqlParameters[0].Value = idArr;

            sqlParameters[1] = new SqlParameter("@idQuest", SqlDbType.Int);
            sqlParameters[1].Value = idQuest;


            sqlParameters[2] = new SqlParameter("@txt", SqlDbType.Text);
            sqlParameters[2].Value = txt;

            return conn.executeUpdateQuery(query, sqlParameters);
        }
        public DataTable GetSkillsForArrAndPerson(int idArrangement, int idContPers)
        {

            string query = string.Format(@"SELECT idQuestSkill
                                        FROM MedLookup 
                                        WHERE  idArrangement='" + idArrangement + "' AND idContPers = '" + idContPers + "'");
            //                          
            return conn.executeSelectQuery(query, null);
        }
        public Boolean DeletePrsonFromMedLookup(int idArrangement, int idContPers)
        {
            string query = string.Format(@"DELETE FROM MedLookup WHERE idContPers = @idContPers AND idArrangement=@idArrangement");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            sqlParameters[1] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[1].Value = idContPers;

            return conn.executeDeleteQuery(query, sqlParameters);

        }
        public Boolean DeleteVolArr(int idArr, int idQuest)
        {
            string query = string.Format(@"DELETE FROM VolArr WHERE  idArr=@idArr AND idQuest=@idQuest");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@idArr", SqlDbType.Int);
            sqlParameters[0].Value = idArr;

            sqlParameters[1] = new SqlParameter("@idQuest", SqlDbType.Int);
            sqlParameters[1].Value = idQuest;

            return conn.executeDeleteQuery(query, sqlParameters);

        }
    }
}