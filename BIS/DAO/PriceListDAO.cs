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
    public class PriceListDAO
    {
        private dbConnection conn;

        public PriceListDAO()
        {
            conn = new dbConnection();
        }

        public int Save(PriceListModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO PriceList(dtPriceList, idArrangement, idClient, isActive, idUserCreated, dtUserCreated, idUserModified, dtUserModified,isReleaseDate,idHotelService )
                      VALUES (@dtPriceList,@idArrangement,@idClient,  @isActive, @idUserCreated, @dtUserCreated, @idUserModified, @dtUserModified,@isReleaseDate, @idHotelService);SELECT SCOPE_IDENTITY();");


            SqlParameter[] sqlParameter = new SqlParameter[10];

            sqlParameter[0] = new SqlParameter("@dtPriceList", SqlDbType.DateTime);
            sqlParameter[0].Value = model.dtPriceList;

            sqlParameter[1] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[1].Value = (model.idArrangement == null) ? SqlInt32.Null : model.idArrangement;

            sqlParameter[2] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[2].Value = (model.idClient == null) ? SqlInt32.Null : model.idClient;

            sqlParameter[3] = new SqlParameter("@isActive", SqlDbType.Bit);
            sqlParameter[3].Value = model.isActive;

            sqlParameter[4] = new SqlParameter("@idUserCreated", SqlDbType.Int);
            sqlParameter[4].Value = model.idUserCreated;

            sqlParameter[5] = new SqlParameter("@dtUserCreated", SqlDbType.DateTime);
            sqlParameter[5].Value = model.dtUserCreated;

            sqlParameter[6] = new SqlParameter("@idUserModified", SqlDbType.Int);
            sqlParameter[6].Value = model.idUserModified;

            sqlParameter[7] = new SqlParameter("@dtUserModified", SqlDbType.DateTime);
            sqlParameter[7].Value = model.dtUserModified;

            sqlParameter[8] = new SqlParameter("@isReleaseDate", SqlDbType.Bit);
            sqlParameter[8].Value = model.isReleaseDate;

            sqlParameter[9] = new SqlParameter("@idHotelService", SqlDbType.Int);
            sqlParameter[9].Value = model.idHotelService;

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
            sqlParameter[4].Value = conn.GetLastTableID("PriceList") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPricelist";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "PriceList";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save price list";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransactionSelectLastID(_query, sqlParameters);

        }

        public Boolean Update(PriceListModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE PriceList SET 
                    dtPriceList = @dtPriceList, idArrangement = @idArrangement,  idClient = @idClient, isActive = @isActive, idUserCreated = @idUserCreated, dtUserCreated = @dtUserCreated,
                    idUserModified = @idUserModified, dtUserModified = @dtUserModified,isReleaseDate=@isReleaseDate, idHotelService = @idHotelService 
                    WHERE idPricelist = @idPricelist");


            SqlParameter[] sqlParameter = new SqlParameter[11];

            sqlParameter[0] = new SqlParameter("@dtPriceList", SqlDbType.DateTime);
            sqlParameter[0].Value = model.dtPriceList;

            sqlParameter[1] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[1].Value = (model.idArrangement == null) ? SqlInt32.Null : model.idArrangement;

            sqlParameter[2] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[2].Value = (model.idClient == null) ? SqlInt32.Null : model.idClient;

            sqlParameter[3] = new SqlParameter("@isActive", SqlDbType.Bit);
            sqlParameter[3].Value = model.isActive;

            sqlParameter[4] = new SqlParameter("@idUserCreated", SqlDbType.Int);
            sqlParameter[4].Value = model.idUserCreated;

            sqlParameter[5] = new SqlParameter("@dtUserCreated", SqlDbType.DateTime);
            sqlParameter[5].Value = model.dtUserCreated;

            sqlParameter[6] = new SqlParameter("@idUserModified", SqlDbType.Int);
            sqlParameter[6].Value = model.idUserModified;

            sqlParameter[7] = new SqlParameter("@dtUserModified", SqlDbType.DateTime);
            sqlParameter[7].Value = model.dtUserModified;

            sqlParameter[8] = new SqlParameter("@idPricelist", SqlDbType.Int);
            sqlParameter[8].Value = (model.idPriceList == null) ? SqlInt32.Null : model.idPriceList;

            sqlParameter[9] = new SqlParameter("@isReleaseDate", SqlDbType.Bit);
            sqlParameter[9].Value = model.isReleaseDate;

            sqlParameter[10] = new SqlParameter("@idHotelService", SqlDbType.Int);
            sqlParameter[10].Value = model.idHotelService;

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
            sqlParameter[4].Value = model.idPriceList;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPricelist";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "PriceList";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update price list";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public DataTable GetAllPriceLists(int idClient)
        {


            string query = string.Format(@"SELECT idPricelist, pl.dtPriceList, pl.idArrangement,arr.nameArrangement,pl.idClient,
                                          arr.dtFromArrangement as dtFrom ,arr.dtToArrangement as dtTo,pl.isActive, pl.idUserCreated,u.username as nameUserCreated,pl.dtUserCreated,pl.idUserModified,u.username as nameUserModified,pl.dtUserModified,
                                          pl.isReleaseDate, pl.idHotelService, hs.nameHotelService, ar.statusArrangement
                                          FROM PriceList pl
                                          LEFT OUTER JOIN Arrangement arr ON arr.idArrangement=pl.idArrangement
                                          LEFT OUTER JOIN Users u ON u.idUser=pl.idUserCreated
                                          LEFT OUTER JOIN Users um ON um.idUser=pl.idUserModified
                                          LEFT OUTER JOIN HotelService hs on hs.idHotelService = pl.idHotelService
                                          LEFT OUTER JOIN Arrangement ar on ar.idArrangement = pl.idArrangement 
                                          WHERE pl.idClient = @idClient");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameters[0].Value = idClient;
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetPriceList(int idPricelist)
        {


            string query = string.Format(@"SELECT idPricelist, pl.dtPriceList, pl.idArrangement,arr.nameArrangement,pl.idClient,
                                          arr.dtFromArrangement as dtFrom ,arr.dtToArrangement as dtTo,pl.isActive, pl.idUserCreated,u.username as nameUserCreated,pl.dtUserCreated,pl.idUserModified,u.username as nameUserModified,pl.dtUserModified,
                                          pl.isReleaseDate, pl.idHotelService 
                                          FROM PriceList pl
                                          LEFT OUTER JOIN Arrangement arr ON arr.idArrangement=pl.idArrangement
                                          LEFT OUTER JOIN Users u ON u.idUser=pl.idUserCreated
                                          LEFT OUTER JOIN Users um ON um.idUser=pl.idUserModified
                                          WHERE pl.idPricelist = @idPricelist");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idPricelist", SqlDbType.Int);
            sqlParameters[0].Value = idPricelist;
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable checkForDelete(int idPriceList)
        {
            string query = string.Format(@" SELECT pla.idArticle
                                          FROM PriceListArticles pla
                                          INNER JOIN PriceList p ON p.idPriceList = pla.idPriceList
                                          INNER JOIN Arrangement a ON p.idArrangement = a.idArrangement
                                          LEFT OUTER JOIN ArrangementBook ab ON ab.idArrangement = a.idArrangement
                                          INNER JOIN ArrangementBookArticles aba ON ab.idArrangementBook = aba.idArrangementBook and pla.idArticle = aba.idArticle
                                          WHERE isContract = 'true' and p.idPriceList='" + idPriceList + "'");

            DataTable dt = new DataTable();
            return conn.executeSelectQuery(query, null);
        }

        //===== za proveru brisanja artikla iz priceliste - Neta
        public DataTable checkForDeleteArticle(int idPriceList, string idArticle)
        {
            string query = string.Format(@" SELECT pla.idArticle
                                          FROM PriceListArticles pla
                                          INNER JOIN PriceList p ON p.idPriceList = pla.idPriceList
                                          INNER JOIN Arrangement a ON p.idArrangement = a.idArrangement
                                          LEFT OUTER JOIN ArrangementBook ab ON ab.idArrangement = a.idArrangement
                                          INNER JOIN ArrangementBookArticles aba ON ab.idArrangementBook = aba.idArrangementBook and pla.idArticle = aba.idArticle
                                          WHERE isContract = 'true' and p.idPriceList='" + idPriceList + "' and pla.idArticle = '" + idArticle + "'");

            DataTable dt = new DataTable();
            return conn.executeSelectQuery(query, null);
        }

        //=====


        public Boolean Delete(int idPriceList, string nameForm, int idUser)
        {

                 List<string> _query = new List<string>();
                 List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

                 string query = string.Format(@"DELETE
                                    FROM PriceList
                                    WHERE idPriceList = @idPriceList");

                 SqlParameter[] sqlParameter = new SqlParameter[1];
                 sqlParameter[0] = new SqlParameter("@idPriceList", SqlDbType.Int);
                 sqlParameter[0].Value = idPriceList;

                 _query.Add(query);
                 sqlParameters.Add(sqlParameter);

                 query = string.Format(@" DELETE
                                    FROM PriceListArticles
                                    WHERE idPriceList = @idPriceList");


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
                 sqlParameter[4].Value = idPriceList;

                 sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                 sqlParameter[5].Value = "idPriceList";

                 sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                 sqlParameter[6].Value = "PriceList";

                 sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                 sqlParameter[7].Value = "Delete price list";

                 _query.Add(query);
                 sqlParameters.Add(sqlParameter);


                 return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean DeleteArticle(int idPriceList, string idArticle, string nameForm, int idUser)
        {

            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE
                                           FROM PriceListArticles
                                           WHERE idPriceList = @idPriceList AND idArticle = @idArticle");

            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idPriceList", SqlDbType.Int);
            sqlParameter[0].Value = idPriceList;
            sqlParameter[1] = new SqlParameter("@idArticle", SqlDbType.NVarChar);
            sqlParameter[1].Value = idArticle;

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
            sqlParameter[4].Value = idArticle;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArticle";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "PriceListArticles";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete price list articles";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

    }
}
