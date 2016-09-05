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
    public class PriceListArticlesDAO
    {
        private dbConnection conn;

        public PriceListArticlesDAO()
        {
            conn = new dbConnection();
        }

        //odavde valja
        public DataTable GetAllArticlesByPriceList(int idPriceList)
        {
            string query = string.Format(@"SELECT idPriceListArticle,idPricelist ,pla.idClient, c.nameClient, idArticle, a.nameArtical, pla.nrArticle, a.quantity,pricePerArticle,pricePerQuantity,priceTotal ,dtFrom,dtTo,nrDays ,isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, pla.idUserCreated,pla.dtUserCreated,pla.idUserModified,pla.dtUserModified
                                          FROM PriceListArticles pla
                                          LEFT OUTER JOIN Client c ON c.idClient = pla.idClient
                                          LEFT OUTER JOIN Artical a ON a.codeArtical = pla.idArticle
                                          WHERE idPriceList = @idPriceList ");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idPriceList", SqlDbType.Int);
            sqlParameters[0].Value = idPriceList;
            return conn.executeSelectQuery(query, sqlParameters);
        }


        public DataTable GetArticleByPriceListArticle(int idPriceListArticle, int idPriceList)
        {
            string query = string.Format(@"SELECT idPriceListArticle,idPricelist ,pla.idClient, c.nameClient, idArticle, a.nameArtical, pla.nrArticle, a.quantity,pricePerArticle,pricePerQuantity,priceTotal ,dtFrom,dtTo,nrDays ,isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, pla.idUserCreated,pla.dtUserCreated,pla.idUserModified,pla.dtUserModified
                                          FROM PriceListArticles pla
                                          LEFT OUTER JOIN Client c ON c.idClient = pla.idClient
                                          LEFT OUTER JOIN Artical a ON a.codeArtical = pla.idArticle
                                          WHERE idPriceListArticle = @idPriceListArticle AND idPriceList = @idPriceList");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@idPriceList", SqlDbType.Int);
            sqlParameters[0].Value = idPriceList;

            sqlParameters[1] = new SqlParameter("@idPriceListArticle", SqlDbType.Int);
            sqlParameters[1].Value = idPriceListArticle;

            return conn.executeSelectQuery(query, sqlParameters);
        }


        //===== za proveru brisanja artikla
        public DataTable GetArticlePriceList(string idArtical)
        {
            string query = string.Format(@"SELECT idPriceListArticle,idPricelist ,pla.idClient, c.nameClient, idArticle, a.nameArtical, pla.nrArticle, a.quantity,pricePerArticle,pricePerQuantity,priceTotal ,dtFrom,dtTo,nrDays ,isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, pla.idUserCreated,pla.dtUserCreated,pla.idUserModified,pla.dtUserModified
                                          FROM PriceListArticles pla
                                          LEFT OUTER JOIN Client c ON c.idClient = pla.idClient
                                          LEFT OUTER JOIN Artical a ON a.codeArtical = pla.idArticle
                                          WHERE idArticle = @idArtical ");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idArtical", SqlDbType.NVarChar);
            sqlParameters[0].Value = idArtical;
            return conn.executeSelectQuery(query, sqlParameters);
        }

        //=====


        public int Save(PriceListArticlesModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO PriceListArticles(idPriceList, idArticle, idClient, nrArticle,  pricePerArticle, pricePerQuantity,priceTotal, dtFrom, dtTo, nrDays, isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment, isNotForTraveler, idUserCreated, dtUserCreated, idUserModified, dtUserModified )
                      VALUES (@idPriceList, @idArticle, @idClient, @nrArticle, @pricePerArticle, @pricePerQuantity,@priceTotal, @dtFrom, @dtTo, @nrDays, @isExtra,@commission,@isBack,@isAway,@isAccomodation,@isNotInAccompaniment, @isNotForTraveler, @idUserCreated, @dtUserCreated, @idUserModified, @dtUserModified);SELECT SCOPE_IDENTITY();");


            SqlParameter[] sqlParameter = new SqlParameter[21];

            sqlParameter[0] = new SqlParameter("@idPriceList", SqlDbType.Int);
            sqlParameter[0].Value = (model.idPriceList == null) ? SqlInt32.Null : model.idPriceList;

            sqlParameter[1] = new SqlParameter("@idArticle", SqlDbType.NVarChar);
            sqlParameter[1].Value = (model.IdArticle == null) ? SqlString.Null : model.IdArticle;

            sqlParameter[2] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[2].Value = (model.IdClient == null || model.IdClient ==0) ? SqlInt32.Null : model.IdClient;

            sqlParameter[3] = new SqlParameter("@nrArticle", SqlDbType.Int);
            sqlParameter[3].Value = model.NrArticle;

            sqlParameter[4] = new SqlParameter("@pricePerArticle", SqlDbType.Decimal);
            sqlParameter[4].Value = model.PricePerArticle;

            sqlParameter[5] = new SqlParameter("@pricePerQuantity", SqlDbType.Decimal);
            sqlParameter[5].Value = model.PricePerQuantity;

            sqlParameter[6] = new SqlParameter("@priceTotal", SqlDbType.Decimal);
            sqlParameter[6].Value = model.priceTotal;

            sqlParameter[7] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameter[7].Value = model.DtFrom;

            sqlParameter[8] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameter[8].Value = model.DtTo;

            sqlParameter[9] = new SqlParameter("@nrDays", SqlDbType.Int);
            sqlParameter[9].Value = model.NrDays;

            sqlParameter[10] = new SqlParameter("@isExtra", SqlDbType.Bit);
            sqlParameter[10].Value = model.IsExtra;

            sqlParameter[11] = new SqlParameter("@commission", SqlDbType.Decimal);
            sqlParameter[11].Value = model.Commission;

            sqlParameter[12] = new SqlParameter("@isAway", SqlDbType.Bit);
            sqlParameter[12].Value = model.IsAway;

            sqlParameter[13] = new SqlParameter("@isBack", SqlDbType.Bit);
            sqlParameter[13].Value = model.IsBack;

            sqlParameter[14] = new SqlParameter("@isAccomodation", SqlDbType.Bit);
            sqlParameter[14].Value = model.IsAccomodation;

            sqlParameter[15] = new SqlParameter("@isNotInAccompaniment", SqlDbType.Bit);
            sqlParameter[15].Value = model.IsNotInAccompaniment;

            sqlParameter[16] = new SqlParameter("@idUserCreated", SqlDbType.Int);
            sqlParameter[16].Value = model.idUserCreated;

            sqlParameter[17] = new SqlParameter("@dtUserCreated", SqlDbType.DateTime);
            sqlParameter[17].Value = model.dtUserCreated;

            sqlParameter[18] = new SqlParameter("@idUserModified", SqlDbType.Int);
            sqlParameter[18].Value = model.idUserModified;

            sqlParameter[19] = new SqlParameter("@dtUserModified", SqlDbType.DateTime);
            sqlParameter[19].Value = model.dtUserModified;

            sqlParameter[20] = new SqlParameter("@isNotForTraveler", SqlDbType.Bit);
            sqlParameter[20].Value = model.IsNotForTraveler;

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
            sqlParameter[4].Value = conn.GetLastTableID("PriceListArticles") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPriceListArticle";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "PriceListArticles";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save price list articles";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransactionSelectLastID(_query, sqlParameters);

        }

        public Boolean Update(PriceListArticlesModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE PriceListArticles SET 
                    idPriceList = @idPriceList, idArticle = @idArticle, idClient = @idClient, nrArticle = @nrArticle, pricePerArticle = @pricePerArticle, 
                    pricePerQuantity = @pricePerQuantity, priceTotal = @priceTotal,dtFrom = @dtFrom, dtTo = @dtTo, nrDays = @nrDays, isExtra = @isExtra,
                    commission=@commission,isBack=@isBack,isAway=@isAway,isAccomodation=@isAccomodation,isNotInAccompaniment=@isNotInAccompaniment, isNotForTraveler = @isNotForTraveler, idUserCreated = @idUserCreated, dtUserCreated = @dtUserCreated,
                    idUserModified = @idUserModified, dtUserModified = @dtUserModified 
                    WHERE idPriceListArticle = @idPriceListArticle");


            SqlParameter[] sqlParameter = new SqlParameter[22];


            sqlParameter[0] = new SqlParameter("@idPriceList", SqlDbType.Int);
            sqlParameter[0].Value = (model.idPriceList == null) ? SqlInt32.Null : model.idPriceList;

            sqlParameter[1] = new SqlParameter("@idArticle", SqlDbType.NVarChar);
            sqlParameter[1].Value = (model.IdArticle == null) ? SqlString.Null : model.IdArticle;

            sqlParameter[2] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[2].Value = (model.IdClient == null || model.IdClient == 0) ? SqlInt32.Null : model.IdClient;

            sqlParameter[3] = new SqlParameter("@nrArticle", SqlDbType.Int);
            sqlParameter[3].Value = model.NrArticle;

            sqlParameter[4] = new SqlParameter("@pricePerArticle", SqlDbType.Decimal);
            sqlParameter[4].Value = model.PricePerArticle;

            sqlParameter[5] = new SqlParameter("@pricePerQuantity", SqlDbType.Decimal);
            sqlParameter[5].Value = model.PricePerQuantity;

            sqlParameter[6] = new SqlParameter("@priceTotal", SqlDbType.Decimal);
            sqlParameter[6].Value = model.priceTotal;

            sqlParameter[7] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameter[7].Value = model.DtFrom;

            sqlParameter[8] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameter[8].Value = model.DtTo;

            sqlParameter[9] = new SqlParameter("@nrDays", SqlDbType.Int);
            sqlParameter[9].Value = model.NrDays;

            sqlParameter[10] = new SqlParameter("@isExtra", SqlDbType.Bit);
            sqlParameter[10].Value = model.IsExtra;

            sqlParameter[11] = new SqlParameter("@commission", SqlDbType.Decimal);
            sqlParameter[11].Value = model.Commission;

            sqlParameter[12] = new SqlParameter("@isAway", SqlDbType.Bit);
            sqlParameter[12].Value = model.IsAway;

            sqlParameter[13] = new SqlParameter("@isBack", SqlDbType.Bit);
            sqlParameter[13].Value = model.IsBack;

            sqlParameter[14] = new SqlParameter("@isAccomodation", SqlDbType.Bit);
            sqlParameter[14].Value = model.IsAccomodation;

            sqlParameter[15] = new SqlParameter("@isNotInAccompaniment", SqlDbType.Bit);
            sqlParameter[15].Value = model.IsNotInAccompaniment;

            sqlParameter[16] = new SqlParameter("@idUserCreated", SqlDbType.Int);
            sqlParameter[16].Value = model.idUserCreated;

            sqlParameter[17] = new SqlParameter("@dtUserCreated", SqlDbType.DateTime);
            sqlParameter[17].Value = model.dtUserCreated;

            sqlParameter[18] = new SqlParameter("@idUserModified", SqlDbType.Int);
            sqlParameter[18].Value = model.idUserModified;

            sqlParameter[19] = new SqlParameter("@dtUserModified", SqlDbType.DateTime);
            sqlParameter[19].Value = model.dtUserModified;

            sqlParameter[20] = new SqlParameter("@idPriceListArticle", SqlDbType.Int);
            sqlParameter[20].Value = (model.idPriceListArticle == null) ? SqlInt32.Null : model.idPriceListArticle;

            sqlParameter[21] = new SqlParameter("@isNotForTraveler", SqlDbType.Bit);
            sqlParameter[21].Value = model.IsNotForTraveler;

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
            sqlParameter[4].Value = model.idPriceListArticle;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPriceListArticle";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "PriceListArticles";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update price list articles";



            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

    }
}
