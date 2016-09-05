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
    public class ArticalDAO
    {
        private dbConnection conn;

        public ArticalDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllArticals()
        {
            string query = string.Format(@"SELECT a.codeArtical, a.nameArtical, a.codeArtikalGroup,a.quantity, ag.nameArticalGroup, a.purchasePrice, a.sellingPrice,a.isGroup, a.idUserCreated, a.dtUserCreated,
                        a.idUserModifies, a.dtUserModified,a.isOptional
                        FROM Artical a 
                        LEFT OUTER JOIN ArticalGroups ag ON a.codeArtikalGroup = ag.codeArticalGroup ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetArticalByID(string codeArtical)
        {
            string query = string.Format(@"SELECT a.codeArtical, a.nameArtical, a.codeArtikalGroup, a.quantity,ag.nameArticalGroup, a.purchasePrice, a.sellingPrice,a.isGroup, a.idUserCreated, a.dtUserCreated,
                        a.idUserModifies, a.dtUserModified
                        FROM Artical a 
                        LEFT OUTER JOIN ArticalGroups ag ON a.codeArtikalGroup = ag.codeArticalGroup 
                        WHERE a.codeArtical = '" + codeArtical +"'");

            return conn.executeSelectQuery(query, null);
        }

        
        public Boolean Save(ArticalModel model,string nameForm,int idUser)
        {

            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            

            string query = string.Format(@"INSERT INTO Artical(codeArtical, nameArtical, codeArtikalGroup,quantity, purchasePrice, sellingPrice, isGroup, idUserCreated, dtUserCreated, idUserModifies, dtUserModified, isOptional )
                      VALUES (@codeArtical, @nameArtical, @codeArtikalGroup, @quantity,@purchasePrice, @sellingPrice, @isGroup, @idUserCreated, @dtUserCreated, @idUserModifies, @dtUserModified, @isOptional )");
           

            SqlParameter[] sqlParameter = new SqlParameter[12];

            sqlParameter[0] = new SqlParameter("@codeArtical", SqlDbType.NVarChar);
            sqlParameter[0].Value = (model.codeArtical == null) ? SqlString.Null : model.codeArtical;

            sqlParameter[1] = new SqlParameter("@nameArtical", SqlDbType.NVarChar);
            sqlParameter[1].Value = (model.nameArtical == null) ? SqlString.Null : model.nameArtical;

            sqlParameter[2] = new SqlParameter("@codeArtikalGroup", SqlDbType.NVarChar);
            sqlParameter[2].Value = (model.codeArtikalGroup == null) ? SqlString.Null : model.codeArtikalGroup;

            sqlParameter[3] = new SqlParameter("@purchasePrice", SqlDbType.Decimal);
            sqlParameter[3].Value = model.purchasePrice;

            sqlParameter[4] = new SqlParameter("@sellingPrice", SqlDbType.Decimal);
            sqlParameter[4].Value = model.sellingPrice;

            sqlParameter[5] = new SqlParameter("@idUserCreated", SqlDbType.Int);
            sqlParameter[5].Value = model.idUserCreated;

            sqlParameter[6] = new SqlParameter("@dtUserCreated", SqlDbType.DateTime);
            sqlParameter[6].Value = model.dtUserCreated;

            sqlParameter[7] = new SqlParameter("@idUserModifies", SqlDbType.Int);
            sqlParameter[7].Value = model.idUserModifies;

            sqlParameter[8] = new SqlParameter("@dtUserModified", SqlDbType.DateTime);
            sqlParameter[8].Value = model.dtUserModified;

            sqlParameter[9] = new SqlParameter("@quantity", SqlDbType.Int);
            sqlParameter[9].Value = model.quantity;

            sqlParameter[10] = new SqlParameter("@isGroup", SqlDbType.Bit);
            sqlParameter[10].Value = model.isGroup;


            sqlParameter[11] = new SqlParameter("@isOptional", SqlDbType.Bit);
            sqlParameter[11].Value = model.isOptional;

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
            sqlParameter[2].Value = DateTime.Now ;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "I";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = model.codeArtical;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "codeArtical";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Artical";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            return conn.executQueryTransaction(_query, sqlParameters);

        }


        public Boolean Update(ArticalModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            
            string query = string.Format(@"UPDATE Artical SET
                    nameArtical = @nameArtical, codeArtikalGroup = @codeArtikalGroup,quantity = @quantity, purchasePrice = @purchasePrice, 
                    sellingPrice = @sellingPrice, isGroup = @isGroup , idUserCreated = @idUserCreated, dtUserCreated = @dtUserCreated, idUserModifies = @idUserModifies, dtUserModified = @dtUserModified,isOptional=@isOptional
                    WHERE codeArtical = @codeArtical");


            SqlParameter[] sqlParameter = new SqlParameter[12];

            sqlParameter[0] = new SqlParameter("@nameArtical", SqlDbType.NVarChar);
            sqlParameter[0].Value = (model.nameArtical == null) ? SqlString.Null : model.nameArtical;

            sqlParameter[1] = new SqlParameter("@codeArtikalGroup", SqlDbType.NVarChar);
            sqlParameter[1].Value = (model.codeArtikalGroup == null) ? SqlString.Null : model.codeArtikalGroup;

            sqlParameter[2] = new SqlParameter("@purchasePrice", SqlDbType.Decimal);
            sqlParameter[2].Value = model.purchasePrice;

            sqlParameter[3] = new SqlParameter("@sellingPrice", SqlDbType.Decimal);
            sqlParameter[3].Value = model.sellingPrice;

            sqlParameter[4] = new SqlParameter("@idUserCreated", SqlDbType.Int);
            sqlParameter[4].Value = model.idUserCreated;

            sqlParameter[5] = new SqlParameter("@dtUserCreated", SqlDbType.DateTime);
            sqlParameter[5].Value = model.dtUserCreated;

            sqlParameter[6] = new SqlParameter("@idUserModifies", SqlDbType.Int);
            sqlParameter[6].Value = model.idUserModifies;

            sqlParameter[7] = new SqlParameter("@dtUserModified", SqlDbType.DateTime);
            sqlParameter[7].Value = model.dtUserModified;

            sqlParameter[8] = new SqlParameter("@codeArtical", SqlDbType.NVarChar);
            sqlParameter[8].Value = (model.codeArtical == null) ? SqlString.Null : model.codeArtical;

            sqlParameter[9] = new SqlParameter("@quantity", SqlDbType.Int);
            sqlParameter[9].Value = model.quantity;

            sqlParameter[10] = new SqlParameter("@isGroup", SqlDbType.Bit);
            sqlParameter[10].Value = model.isGroup;

            sqlParameter[11] = new SqlParameter("@isOptional", SqlDbType.Bit);
            sqlParameter[11].Value = model.isOptional;

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
            sqlParameter[4].Value = model.codeArtical;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "codeArtical";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Artical";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            return conn.executQueryTransaction(_query, sqlParameters);

        }
       
        public DataTable checkIfArticleIsInExtraCalculation(int idArrangement, string idArticle)
        {
            string query = string.Format(@"SELECT idArticle FROM ArrangementInvoicePrice aip
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = aip.idArticle
                                           WHERE aip.idArrangement = '" + idArrangement + @"'  and aip.isExtra = 0 and aip.idArticle = '" + idArticle + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAllArticalsForArrangemetAccomodation(int idArrangement)
        {
            string query = string.Format(@"SELECT idArticle,a.nameArtical as nameArticle,nrArticle*a.quantity -(SELECT COUNT(DISTINCT idArticle)
                                           FROM ArrangementBook ab
                                           INNER JOIN ArrangementBookArticles aba ON ab.idArrangementBook=aba.idArrangementBook
                                           WHERE idArrangement = '" + idArrangement + @"' AND isContract = '0' AND id = ap.idArrangementPrice) as number,idArrangementPrice as id,'false' as isContract,ap.idClient,c.nameClient,ap.dtFrom,ap.dtTo,ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal
                                           FROM ArrangementPrice ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag  ON a.codeArtikalGroup = ag.codeArticalGroup
                                           WHERE idArrangement = '" + idArrangement + @"' AND ag.classArticalGroup = 'Accomod' 
                                           UNION     
                                           SELECT pla.idArticle,a.nameArtical as nameArticle,pla.nrArticle*a.quantity-(SELECT COUNT(DISTINCT idArticle)
                                           FROM ArrangementBook ab
                                           INNER JOIN ArrangementBookArticles aba ON ab.idArrangementBook=aba.idArrangementBook
                                           WHERE idArrangement = '" + idArrangement + @"' AND isContract = '1' AND id = pla.idPriceListArticle) as number,pla.idPriceListArticle as id,'true' as isContract, pl.idClient,c.nameClient,pla.dtFrom,pla.dtTo,pla.pricePerArticle,pla.pricePerQuantity,pla.priceTotal
                                           FROM PriceList pl
                                           INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                                           LEFT OUTER JOIN  CLient c ON c.idClient = pl.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag  ON a.codeArtikalGroup = ag.codeArticalGroup
                                           WHERE pl.idArrangement = '" + idArrangement + @"'  AND pla.isExtra = 'false' AND ag.classArticalGroup = 'Accomod' 
                                           UNION 
                                           SELECT pla.idArticle,a.nameArtical as nameArticle,pla.nrArticle*a.quantity-(SELECT COUNT(DISTINCT idArticle)
                                           FROM ArrangementBook ab
                                           INNER JOIN ArrangementBookArticles aba ON ab.idArrangementBook=aba.idArrangementBook
                                           WHERE idArrangement = '" + idArrangement + @"' AND isContract = '1' AND id = pla.idPriceListArticle) as number,pla.idPriceListArticle as id,'true' as isContract, pl.idClient,c.nameClient,pla.dtFrom,pla.dtTo,pla.pricePerArticle,pla.pricePerQuantity,pla.priceTotal
                                           FROM PriceList pl
                                           INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                                           LEFT OUTER JOIN  CLient c ON c.idClient = pl.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag  ON a.codeArtikalGroup = ag.codeArticalGroup
                                           INNER JOIN  ArrangementInvoicePrice aip ON pl.idArrangement = aip.idArrangement AND pla.idArticle = aip.idArticle
                                           WHERE pl.idArrangement = '" + idArrangement + @"'  AND pla.isExtra = 'true' AND ag.classArticalGroup = 'Accomod' 
                                           ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllArticalsForArrangemetAccomodationSAKI(int idArrangement)
        {
            string query = string.Format(@" SELECT idArticle,a.nameArtical as nameArticle,nrArticle,quantity,nrArticle*a.quantity -(SELECT COUNT(DISTINCT idArticle)
                     FROM ArrangementBook ab
                     INNER JOIN ArrangementBookArticles aba ON ab.idArrangementBook=aba.idArrangementBook
                     WHERE idArrangement = '" + idArrangement + @"' AND isContract = '0' AND id = ap.idArrangementPrice) as number,idArrangementPrice as id,'false' as isContract,ap.idClient,c.nameClient,ap.dtFrom,ap.dtTo,ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal
                    FROM ArrangementPrice ap
                    LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                     LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                     LEFT OUTER JOIN  ArticalGroups ag  ON a.codeArtikalGroup = ag.codeArticalGroup
                     WHERE idArrangement = '" + idArrangement + @"' AND ag.classArticalGroup = 'Accomod' 
                     UNION     
                     SELECT pla.idArticle,a.nameArtical as nameArticle,pla.nrArticle,a.quantity,pla.nrArticle*a.quantity-(SELECT COUNT(DISTINCT idArticle)
                      FROM ArrangementBook ab
                      INNER JOIN ArrangementBookArticles aba ON ab.idArrangementBook=aba.idArrangementBook
                      WHERE idArrangement = '" + idArrangement + @"' AND isContract = '1' AND id = pla.idPriceListArticle) as number,pla.idPriceListArticle as id,'true' as isContract, pl.idClient,c.nameClient,pla.dtFrom,pla.dtTo,pla.pricePerArticle,pla.pricePerQuantity,pla.priceTotal
                    FROM PriceList pl
                    INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                     LEFT OUTER JOIN  CLient c ON c.idClient = pl.idClient
                     LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                     LEFT OUTER JOIN  ArticalGroups ag  ON a.codeArtikalGroup = ag.codeArticalGroup
                     WHERE pl.idArrangement = '" + idArrangement + @"'   AND ag.classArticalGroup = 'Accomod' 
                     ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllArticalsForUpdateArrangementAccomodation(int idArrangement)
        {
            string query = string.Format(@" SELECT dd.idArticle,nameArticle,nrArticle-ar.numberRooms as nrArticle,quantity,number,dd.id,dd.isContract,idClient,nameClient,dtFrom,dtTo,pricePerArticle,pricePerQuantity,priceTotal,letterRoom + CONVERT(nvarchar,numberRooms+1) as nrLast
                    FROM (SELECT idArticle,a.nameArtical as nameArticle,nrArticle,quantity,nrArticle*a.quantity -(SELECT COUNT(DISTINCT idArticle)
                     FROM ArrangementBook ab
                     INNER JOIN ArrangementBookArticles aba ON ab.idArrangementBook=aba.idArrangementBook
                     WHERE idArrangement = '" + idArrangement + @"' AND isContract = '0' AND id = ap.idArrangementPrice) as number,idArrangementPrice as id,'false' as isContract,ap.idClient,c.nameClient,ap.dtFrom,ap.dtTo,ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal
                    FROM ArrangementPrice ap
                    LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                     LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                     LEFT OUTER JOIN  ArticalGroups ag  ON a.codeArtikalGroup = ag.codeArticalGroup
                     WHERE idArrangement = '" + idArrangement + @"' AND ag.classArticalGroup = 'Accomod' 
                     UNION     
                     SELECT pla.idArticle,a.nameArtical as nameArticle,pla.nrArticle,a.quantity,pla.nrArticle*a.quantity-(SELECT COUNT(DISTINCT idArticle)
                      FROM ArrangementBook ab
                      INNER JOIN ArrangementBookArticles aba ON ab.idArrangementBook=aba.idArrangementBook
                      WHERE idArrangement = '" + idArrangement + @"' AND isContract = '1' AND id = pla.idPriceListArticle) as number,pla.idPriceListArticle as id,'true' as isContract, pl.idClient,c.nameClient,pla.dtFrom,pla.dtTo,pla.pricePerArticle,pla.pricePerQuantity,pla.priceTotal
                    FROM PriceList pl
                    INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                     LEFT OUTER JOIN  CLient c ON c.idClient = pl.idClient
                     LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                     LEFT OUTER JOIN  ArticalGroups ag  ON a.codeArtikalGroup = ag.codeArticalGroup
                     WHERE pl.idArrangement = '" + idArrangement + @"'   AND ag.classArticalGroup = 'Accomod' )dd
                     INNER JOIN 
                     (SELECT  idArticle,id,isContract,SUBSTRING ( idRoom ,1 ,1) as letterRoom,MAX(CAST( SUBSTRING ( idRoom ,2 , Len(idRoom) - (Len(idRoom) - CHARINDEX('-',idRoom) + 2)) as nvarchar)) as numberRooms FROM ArrangementRooms WHERE idArrangement = '" + idArrangement + @"'
                     GROUP BY idArticle,id,isContract,SUBSTRING ( idRoom ,1 ,1) 
                     ) ar 
                     ON  ar.idArticle = dd.idArticle AND ar.id = dd.id AND ar.isContract = dd.isContract
					WHERE nrArticle- ar.numberRooms <> 0");

            return conn.executeSelectQuery(query, null);
        }


        public DataTable GetAllBookedRoomsForArrangement(int idArrangement)
        {
            string query = string.Format(@" SELECT DISTINCT ar.idArticle,a.nameArtical as nameArticle,ar.idRoom,ar.isContract,ar.id,ab.idContPers
                                            FROM ArrangementRooms ar
                                            LEFT OUTER JOIN  Artical a ON a.codeArtical = ar.idArticle
                                            LEFT OUTER JOIN  ArticalGroups ag  ON a.codeArtikalGroup = ag.codeArticalGroup
                                            INNER JOIN  
                                            (SELECT idContPers,idRoom,idArticle,isContract,id FROM ArrangementBookArticles aba LEFT OUTER JOIN ArrangementBook abb  ON abb.idArrangementBook = aba.idArrangementBook WHERE abb.idArrangement = '" + idArrangement + @"') ab 
                                            ON ab.idRoom = ar.idRoom AND a.codeArtical = ab.idArticle AND ab.isContract = ar.isContract AND ab.id = ar.id 
                                            WHERE idArrangement = '" + idArrangement + "' ");

            return conn.executeSelectQuery(query, null);
        }

        public Boolean Delete(string id, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM Artical 
                    WHERE codeArtical = @id");


            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[0].Value = id ;

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
            sqlParameter[4].Value = id;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "codeArtical";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Artical";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public DataTable GetAllArticalsForBookingLookup(int idContPers, int idArrangement)
        {
            string query = string.Format(@"SELECT ap.idArticle,a.nameArtical as nameArticle,aaab.idRoom,nrArticle*a.quantity -(SELECT COUNT(idArticle)
                                           FROM ArrangementBook ab
                                           INNER JOIN ArrangementBookArticles aba ON ab.idArrangementBook=aba.idArrangementBook
                                           WHERE idArrangement = '" + idArrangement + @"' AND isContract = '0' AND id = ap.idArrangementPrice) as number,idArrangementPrice as id,'false' as isContract,ap.idClient,c.nameClient,ap.dtFrom,ap.dtTo,ap.pricePerArticle,ap.pricePerQuantity,ap.priceTotal,aaab.idUserCreated, aaab.dtUserCreated, aaab.idUserModified,aaab.dtUserModified

                                           FROM ArrangementPrice ap
                                           LEFT JOIN  (SELECT arba.idRoom,arba.id,arba.idArticle,arba.idUserCreated, arba.dtUserCreated, arba.idUserModified,arba.dtUserModified FROM ArrangementBook arb LEFT OUTER JOIN ArrangementBookArticles arba ON arb.idArrangementBook = arba.idArrangementBook WHERE arb.idArrangement = '" + idArrangement + @"' AND arba.isContract = 'false' AND idContPers = '" + idContPers + @"') aaab ON ap.idArrangementPrice = aaab.id AND ap.idArticle = aaab.idArticle 
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle

                                           WHERE idArrangement = '" + idArrangement + @"' 
                                           UNION     
                                           SELECT pla.idArticle,a.nameArtical as nameArticle,aaab.idRoom,pla.nrArticle*a.quantity-(SELECT COUNT(idArticle)
                                           FROM ArrangementBook ab
                                           INNER JOIN ArrangementBookArticles aba ON ab.idArrangementBook=aba.idArrangementBook
                                           WHERE idArrangement = '" + idArrangement + @"' AND isContract = '1' AND id = pla.idPriceListArticle) as number,pla.idPriceListArticle as id,'true' as isContract, pl.idClient,c.nameClient,pla.dtFrom,pla.dtTo,pla.pricePerArticle,pla.pricePerQuantity,pla.priceTotal,aaab.idUserCreated, aaab.dtUserCreated, aaab.idUserModified,aaab.dtUserModified
                                           FROM PriceList pl
                                           INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                                           LEFT OUTER JOIN  (SELECT arba.idRoom,arba.id,arba.idArticle,arba.idUserCreated, arba.dtUserCreated, arba.idUserModified,arba.dtUserModified FROM ArrangementBook arb LEFT OUTER JOIN ArrangementBookArticles arba ON arb.idArrangementBook = arba.idArrangementBook WHERE arb.idArrangement = '" + idArrangement + @"' and arba.isContract = 'true' AND idContPers = '" + idContPers + @"') aaab ON pla.idPriceListArticle = aaab.id AND pla.idArticle = aaab.idArticle
                                           LEFT OUTER JOIN  CLient c ON c.idClient = pl.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                                           WHERE pl.idArrangement = '" + idArrangement + @"'   
                                           UNION 
                                           SELECT pla.idArticle,a.nameArtical as nameArticle,aaab.idRoom,pla.nrArticle*a.quantity-(SELECT COUNT(idArticle)
                                           FROM ArrangementBook ab
                                           INNER JOIN ArrangementBookArticles aba ON ab.idArrangementBook=aba.idArrangementBook
                                           WHERE idArrangement = '" + idArrangement + @"' AND isContract = '1' AND id = pla.idPriceListArticle) as number,pla.idPriceListArticle as id,'true' as isContract, pl.idClient,c.nameClient,pla.dtFrom,pla.dtTo,pla.pricePerArticle,pla.pricePerQuantity,pla.priceTotal,aaab.idUserCreated, aaab.dtUserCreated, aaab.idUserModified,aaab.dtUserModified
                                           FROM PriceList pl
                                           INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                                           LEFT OUTER JOIN  (SELECT arba.idRoom,arba.id,arba.idArticle,arba.idUserCreated, arba.dtUserCreated, arba.idUserModified,arba.dtUserModified FROM ArrangementBook arb LEFT OUTER JOIN ArrangementBookArticles arba ON arb.idArrangementBook = arba.idArrangementBook WHERE arb.idArrangement = '" + idArrangement + @"' and arba.isContract = 'true' AND idContPers = '" + idContPers + @"') aaab ON pla.idPriceListArticle = aaab.id AND pla.idArticle = aaab.idArticle
                                           LEFT OUTER JOIN  CLient c ON c.idClient = pl.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                                           INNER JOIN  ArrangementInvoicePrice aip ON pl.idArrangement = aip.idArrangement AND pla.idArticle = aip.idArticle
                                           WHERE pl.idArrangement = '" + idArrangement + @"'  AND pla.isExtra = 'true' 
                                           UNION
                                           SELECT aip.idArticle,a.nameArtical as nameArticle,aaab.idRoom,1 as number,'' as id,'true' as isContract,'' as idClient,'' as nameClient,'' as dtFrom,'' as dtTo,0 as pricePerArticle,0 as pricePerQuantity,aip.purchasePriceTotal as priceTotal,aaab.idUserCreated, aaab.dtUserCreated, aaab.idUserModified,aaab.dtUserModified
                                           FROM ArrangementInvoicePrice aip
                                           LEFT OUTER JOIN  (SELECT arba.idRoom,arba.id,arba.idArticle,arba.idUserCreated, arba.dtUserCreated, arba.idUserModified,arba.dtUserModified FROM ArrangementBook arb LEFT OUTER JOIN ArrangementBookArticles arba ON arb.idArrangementBook = arba.idArrangementBook WHERE arb.idArrangement = '" + idArrangement + @"' and arba.isContract = 'true' AND idContPers = '" + idContPers + @"') aaab ON aip.idArticle = aaab.idArticle
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = aip.idArticle
                                           WHERE aip.idArrangement = '" + idArrangement + @"' AND aip.idArticle <> 'Reis Pakket' AND aip.idArticle <> 'Insurance'");

            return conn.executeSelectQuery(query, null);
        }
    public DataTable GetAllArticalsRoomsForArrangement(int idArrangement, string idLang)
        {
            string query = string.Format(@"SELECT ar.idArticle,a.nameArtical as nameArticle,ar.idRoom, ar.isContract,ar.id,ab.idContPers, cp.firstname+ ' ' +cp.midname + ' ' + cp.lastname as name,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender, 'Voluntary' as type
                                            FROM ArrangementRooms ar
                                            LEFT OUTER JOIN  Artical a ON a.codeArtical = ar.idArticle
                                            LEFT OUTER JOIN  ArticalGroups ag  ON a.codeArtikalGroup = ag.codeArticalGroup
                                            LEFT OUTER JOIN  
                                            (SELECT idContPers,idRoom,idArticle,isContract,id FROM ArrangementBookArticles aba LEFT OUTER JOIN ArrangementBook abb  ON abb.idArrangementBook = aba.idArrangementBook WHERE abb.idArrangement = '" + idArrangement + @"') ab 
                                            ON ab.idRoom = ar.idRoom AND a.codeArtical = ab.idArticle AND ab.isContract = ar.isContract AND ab.id = ar.id
                                            LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = ab.idContPers
                                            LEFT OUTER JOIN Gender g ON cp.idGender = g.idGender
                                            LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey
                                            WHERE idArrangement = '" + idArrangement + @"' AND cp.idContPers IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4') 
                                            UNION
                                            SELECT ar.idArticle,a.nameArtical as nameArticle,ar.idRoom, ar.isContract,ar.id,ab.idContPers, cp.firstname+ ' ' +cp.midname + ' ' + cp.lastname as name,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender, 'Traveler' as type
                                            FROM ArrangementRooms ar
                                            LEFT OUTER JOIN  Artical a ON a.codeArtical = ar.idArticle
                                            LEFT OUTER JOIN  ArticalGroups ag  ON a.codeArtikalGroup = ag.codeArticalGroup
                                            LEFT OUTER JOIN  
                                            (SELECT idContPers,idRoom,idArticle,isContract,id FROM ArrangementBookArticles aba LEFT OUTER JOIN ArrangementBook abb  ON abb.idArrangementBook = aba.idArrangementBook WHERE abb.idArrangement = '" + idArrangement + @"') ab 
                                            ON ab.idRoom = ar.idRoom AND a.codeArtical = ab.idArticle AND ab.isContract = ar.isContract AND ab.id = ar.id
                                            LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = ab.idContPers
                                            LEFT OUTER JOIN Gender g ON cp.idGender = g.idGender
                                            LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey
                                            WHERE idArrangement = '" + idArrangement + @"' AND cp.idContPers NOT IN  (SELECT DISTINCT idContPers FROM ContactPersonFilter WHERE idFilter = '4')
                                            UNION
                                            SELECT ar.idArticle,a.nameArtical as nameArticle,ar.idRoom, ar.isContract,ar.id,ab.idContPers, cp.firstname+ ' ' +cp.midname + ' ' + cp.lastname as name,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE g.nameGender END AS nameGender, '' as type
                                            FROM ArrangementRooms ar
                                            LEFT OUTER JOIN  Artical a ON a.codeArtical = ar.idArticle
                                            LEFT OUTER JOIN  ArticalGroups ag  ON a.codeArtikalGroup = ag.codeArticalGroup
                                            LEFT OUTER JOIN  
                                            (SELECT idContPers,idRoom,idArticle,isContract,id FROM ArrangementBookArticles aba LEFT OUTER JOIN ArrangementBook abb  ON abb.idArrangementBook = aba.idArrangementBook WHERE abb.idArrangement = '" + idArrangement + @"') ab 
                                            ON ab.idRoom = ar.idRoom AND a.codeArtical = ab.idArticle AND ab.isContract = ar.isContract AND ab.id = ar.id
                                            LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = ab.idContPers
                                            LEFT OUTER JOIN Gender g ON cp.idGender = g.idGender
                                            LEFT JOIN STRING" + idLang + @" s ON g.nameGender=s.stringKey
                                            WHERE idArrangement = '" + idArrangement + "' AND cp.idContPers IS NULL");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetExtraOptionalData(int idArrangement)
        {
            string query = string.Format(@" 
                    SELECT codeArtical as idArticle,nameArtical,0 as nrArticle,sellingPrice, 'false' as isExtra,isOptional
                    FROM Artical
                    WHERE  isOptional = '1' 
                    UNION 
                    SELECT idArticle,descriptionArticle as nameArtical,nrArticle,sellingPrice,isExtra,isOption as isOptional
                    FROM ArrangementInvoicePrice
                    WHERE isExtra = '1' and isOption = '1' AND 
                    idArticle NOT IN (SELECT DISTINCT codeArtical FROM Artical a 
                                      LEFT JOIN ArticalGroups ag ON a.codeArtikalGroup = ag.codeArticalGroup 
                                      WHERE ag.classArticalGroup = 'Accomod')
                    AND idArrangement = '" + idArrangement + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetArrangementBookOptionalForPerson(int idArrangementBook)
        {
            string query = string.Format(@" 
                    SELECT ao.idArrangementBook as idArrBook,ao.idArticle,ao.sellingPrice,ao.isExtra,ao.isOptional
                    FROM ArrangementBookOptionalArticles ao
                    LEFT OUTER JOIN ArrangementBook  ab  on ao.idArrangementBook= ab.idArrangementBook
                    WHERE ab.idArrangementBook = '" + idArrangementBook + "'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetArrangementBookOptionalForTravelers(List<int> idArrangementBook)
        {

            string condition = "";
            if (idArrangementBook != null)
                if (idArrangementBook.Count > 0)
                {
                    for (int i = 0; i < idArrangementBook.Count; i++)
                    {
                        if (i == idArrangementBook.Count - 1)
                            condition = " WHERE (" + condition + " ab.idArrangementBook = '" + idArrangementBook[i] + "' )";
                        else
                            condition = condition + " ab.idArrangementBook = '" + idArrangementBook[i] + "' OR";
                    }
                }

            string query = string.Format(@" 
                    SELECT DISTINCT ao.idArticle,ao.sellingPrice,ao.isExtra,ao.isOptional
                    FROM ArrangementBookOptionalArticles ao
                    LEFT OUTER JOIN ArrangementBook  ab  on ao.idArrangementBook= ab.idArrangementBook
                    " + condition );

            return conn.executeSelectQuery(query, null);
        }

        public DataTable numberOptionalArticle(List<int> idArrangementBook, string idArticle, decimal sellingPrice)
        {
            string condition = "";
            if (idArrangementBook != null)
                if (idArrangementBook.Count > 0)
                {
                    for (int i = 0; i < idArrangementBook.Count; i++)
                    {
                        if (i == idArrangementBook.Count - 1)
                            condition = "(" + condition + " ab.idArrangementBook = '" + idArrangementBook[i] + "' )";
                        else
                            condition = condition + " ab.idArrangementBook = '" + idArrangementBook[i] + "' OR";
                    }
                }


            string query = string.Format(@" 
                    SELECT ao.idArticle,ao.sellingPrice,COUNT(idArticle) as quantity
                    FROM ArrangementBookOptionalArticles ao
                    LEFT OUTER JOIN ArrangementBook  ab  on ao.idArrangementBook= ab.idArrangementBook
                    WHERE  ao.idArticle = '" + idArticle + @"' AND sellingPrice = @sellingPrice AND "+condition+@"
                    GROUP BY ao.idArticle,ao.sellingPrice");
            
            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@sellingPrice", SqlDbType.Decimal);
            sqlParameter[0].Value = sellingPrice;

            return conn.executeSelectQuery(query, sqlParameter);
        }

        public DataTable numberExtraArticle(List<int> idArrangementBookList, int idArrangement, int MinNumTravelers, string idArticle, decimal sellingPrice)
        {
            string condition = "";
            if (idArrangementBookList != null)
                if (idArrangementBookList.Count > 0)
                {
                    for (int i = 0; i < idArrangementBookList.Count; i++)
                    {
                        if (i == idArrangementBookList.Count - 1)
                            condition = " WHERE (" + condition + " ab.idArrangementBook = '" + idArrangementBookList[i] + "' )";
                        else
                            condition = condition + " ab.idArrangementBook = '" + idArrangementBookList[i] + "' OR";
                    }
                }


            string query = string.Format(@" 
                    SELECT ao.idArticle,ao.sellingPrice,COUNT(idArticle) as quantity
                    FROM ( SELECT cc.idArticle,cc.pricePerArticle as sellingPrice,cc.idArrangementBook FROM 
                                           (SELECT idArrangementPrice ,ap.idArrangement,ap.idArticle,ap.isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, a.nameArtical as nameArticle,
                                           ap.nrArticle,a.quantity,'' as idContract,'No contract' as nameContract ,ap.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,
                                           CASE WHEN aip.sellingPrice IS NULL THEN ap.pricePerArticle ELSE aip.sellingPrice END AS pricePerArticle,ap.pricePerQuantity,ap.priceTotal, CASE WHEN a.isGroup=1 THEN CAST((ap.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST(ap.priceTotal as numeric(18,2)) END as Total,
                                           ap.idUserCreated, ap.dtUserCreated,ap.idUserModified,ap.dtUserModified,aba.idArrangementBook
                                           FROM ArrangementPrice ap
                                           LEFT OUTER JOIN  CLient c ON c.idClient = ap.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = ap.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           RIGHT OUTER JOIN (SELECT DISTINCT idArticle,idArrangementBook FROM ArrangementBookArticles ab " + condition + @") aba ON aba.idArticle = ap.idArticle
                                           INNER JOIN ArrangementInvoicePrice aip ON aip.idArticle = ap.idArticle AND aip.idArrangement = '" + idArrangement + @"'
                                           WHERE ap.idArrangement = '" + idArrangement + @"' AND ap.isExtra = '1' and ag.classArticalGroup = 'Accomod'
                                           AND CASE WHEN aip.sellingPrice IS NULL THEN ap.pricePerArticle ELSE aip.sellingPrice END>0 AND priceTotal IS NOT NULL
                                           UNION
                                           SELECT idPriceListArticle as idArrangementPrice,pl.idArrangement,pla.idArticle,pla.isExtra,commission,isBack,isAway,isAccomodation,
                                           isNotInAccompaniment, isNotForTraveler, a.nameArtical as nameArticle,pla.nrArticle,a.quantity,pl.idPriceList as idContract,
                                           'Contract' as nameContract,pl.idClient,c.nameClient,dtFrom,dtTo,a.isGroup,CASE WHEN aip.sellingPrice IS NULL THEN pla.pricePerArticle ELSE aip.sellingPrice END AS pricePerArticle,pla.pricePerQuantity,pla.priceTotal,
                                           CASE WHEN a.isGroup=1 THEN CAST(( pla.priceTotal / " + MinNumTravelers + @") as numeric(18,2)) ELSE CAST( pla.priceTotal as numeric(18,2)) END as Total,
                                           pl.idUserCreated, pl.dtUserCreated,pl.idUserModified,pl.dtUserModified,aba.idArrangementBook
                                           FROM PriceList pl
                                           INNER JOIN PriceListArticles pla ON pla.idPriceList = pl.idPriceList
                                           LEFT OUTER JOIN  CLient c ON c.idClient = pl.idClient
                                           LEFT OUTER JOIN  Artical a ON a.codeArtical = pla.idArticle
                                           LEFT OUTER JOIN  ArticalGroups ag ON a.codeArtikalGroup  = ag.codeArticalGroup
                                           INNER JOIN ArrangementInvoicePrice aip ON aip.idArticle = pla.idArticle AND aip.idArrangement = '" + idArrangement + @"'
                                           RIGHT OUTER JOIN (SELECT DISTINCT idArticle,idArrangementBook FROM ArrangementBookArticles ab " + condition + @") aba ON aba.idArticle = pla.idArticle
                                           WHERE pl.idArrangement = '" + idArrangement + @"' AND pla.isExtra=1 and ag.classArticalGroup = 'Accomod'
                                           AND CASE WHEN aip.sellingPrice IS NULL THEN pla.pricePerArticle ELSE aip.sellingPrice END>0 AND priceTotal IS NOT NULL) cc
                    ) ao
                    LEFT OUTER JOIN ArrangementBook  ab  on ao.idArrangementBook= ab.idArrangementBook
                    WHERE  ao.idArticle = '" + idArticle + @"' AND sellingPrice = @sellingPrice AND " + condition.Replace("WHERE","") + @"
                    GROUP BY ao.idArticle,ao.sellingPrice");

            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@sellingPrice", SqlDbType.Decimal);
            sqlParameter[0].Value = sellingPrice;

            return conn.executeSelectQuery(query, sqlParameter);
        }

        public DataTable GetArrangementBookID(int idContPers, int idArrangement)
        {
            string query = string.Format(@" 
                    SELECT ab.idArrangementBook as arranementBookID
                    FROM  ArrangementBook  ab 
                    WHERE ab.idContPers = '" + idContPers + "' and ab.idArrangement= '" + idArrangement + "'");

            return conn.executeSelectQuery(query, null);
        }
        public Boolean DeleteSaveScript(int iID, List<ArticalExtraOptionalModel> model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            SqlParameter[] sqlParameter;

            string query = string.Format(@"DELETE ArrangementBookOptionalArticles  WHERE idArrangementBook = @idArrangementBook ");

            sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idArrangementBook", SqlDbType.Int);
            sqlParameter[0].Value = iID;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);



            if (model.Count > 0)
            {
                for (int i = 0; i < model.Count; i++)
                {
                    query = string.Format(@"INSERT INTO ArrangementBookOptionalArticles (idArrangementBook,sellingPrice,idArticle,isOptional,isExtra)
                      VALUES(@idArrangementBook,@sellingPrice,@idArticle,@isOptional,@isExtra)");

                    sqlParameter = new SqlParameter[5];
                    sqlParameter[0] = new SqlParameter("@idArrangementBook", SqlDbType.Int);
                    sqlParameter[0].Value = iID;

                    sqlParameter[1] = new SqlParameter("@sellingPrice", SqlDbType.Decimal);
                    sqlParameter[1].Value = model[i].sellingPrice;

                    sqlParameter[2] = new SqlParameter("@idArticle", SqlDbType.NVarChar);
                    sqlParameter[2].Value = model[i].idArticle;

                    sqlParameter[3] = new SqlParameter("@isOptional", SqlDbType.Bit);
                    sqlParameter[3].Value = model[i].isOptional;

                    sqlParameter[4] = new SqlParameter("@isExtra", SqlDbType.Bit);
                    sqlParameter[4].Value = model[i].isExtra;

                    _query.Add(query);
                    sqlParameters.Add(sqlParameter);
                }
            }
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
            sqlParameter[3].Value = "DI";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = iID;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangementBook";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementBookOptionalArticles";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            return conn.executQueryTransaction(_query, sqlParameters);

        }
    }
}
