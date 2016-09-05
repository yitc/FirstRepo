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
    public class ArrangementCalculationFirstArticlesDAO
    {
        private dbConnection conn;

        public ArrangementCalculationFirstArticlesDAO()
        {
            conn = new dbConnection();
        }


        public DataTable GetNotArticles(int idArrangement)
        {
            string query = string.Format(@"SELECT idArrangement,dtFromArrangement,dtToArrangement,nrOfNights,nrTraveler,minNrTraveler,nrVoluntaryHelper,idCountry,idUserFinished,dtUserFinished FROM ArrangementCalculationFirstNotArticles WHERE idArrangement = '" + idArrangement + "'");

            return conn.executeSelectQuery(query, null);
        }


        public DataTable GetLabelsFirst(int idArrangement)
        {
            string query = string.Format(@"SELECT idLabel, idArrangement FROM ArrangementLabelFirst WHERE idArrangement = @idArrangement");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }


        public Boolean SaveFirst(int idArrangement, List<IModel> dtPrice, string nameForm, int idUser)
        {

            Boolean res = false;
            List<string> _queryList = new List<string>();
            List<SqlParameter[]> sqlParametersList = new List<SqlParameter[]>();

            //insert articles in first calculation
            ArrangementCalculationFirstArticlesModel mod = new ArrangementCalculationFirstArticlesModel();
            ArrangementCalculationFirstArticlesDAO dao = new ArrangementCalculationFirstArticlesDAO();
            string query;
            SqlParameter[] sqlParameter;

            if (dtPrice != null && dtPrice.Count > 0)
            {
                int i = 0;
                foreach (IModel drmodel in dtPrice)
                {
                    ArrangementPriceModel dr = (ArrangementPriceModel) drmodel;
                    if(i==0)
                    {
                        string queryDelete = string.Format(@"DELETE FROM ArrangementCalculationFirstArticles WHERE idArrangement = @idArrangement");
                        SqlParameter[] sqlParametersDelete = new SqlParameter[1];
                        sqlParametersDelete[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
                        sqlParametersDelete[0].Value = mod.idArrangement;
                        _queryList.Add(queryDelete);
                        sqlParametersList.Add(sqlParametersDelete);

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
                        sqlParameter[4].Value = mod.idArrangement.ToString();

                        sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                        sqlParameter[5].Value = "idArrangement";

                        sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                        sqlParameter[6].Value = "ArrangementCalculationFirstArticles";

                        sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                        sqlParameter[7].Value = "Delete";

                        _queryList.Add(query);
                        sqlParametersList.Add(sqlParameter);


                      //  return conn.executQueryTransaction(_queryList, sqlParametersList);
                    }
                    if (dr.idArticle.ToString() != "")
                    {
                        query = string.Format(@"INSERT INTO ArrangementCalculationFirstArticles(idArrangementPrice,idArrangement, idArticle,  idClient, idContract, nrArticle, pricePerArticle, pricePerQuantity, priceTotal,dtFrom, dtTo, idUserCreated, dtUserCreated, isExtra,commission,isBack,isAway,isAccomodation,isNotInAccompaniment,isNotForTraveler  )
                        VALUES (@idArrangementPrice,@idArrangement, @idArticle,  @idClient, @idContract, @nrArticle, @pricePerArticle, @pricePerQuantity,@priceTotal,@dtFrom, @dtTo, @idUserCreated, @dtUserCreated,@isExtra,@commission,@isBack,@isAway,@isAccomodation,@isNotInAccompaniment,@isNotForTraveler )");

                        sqlParameter = new SqlParameter[20];

                        sqlParameter[0] = new SqlParameter("@idArrangementPrice", SqlDbType.Int);
                        sqlParameter[0].Value = (dr.idArrangementPrice == 0) ? SqlInt32.Null : dr.idArrangementPrice;

                        sqlParameter[1] = new SqlParameter("@idArrangement", SqlDbType.Int);
                        sqlParameter[1].Value = (dr.idArrangement == 0) ? SqlInt32.Null : dr.idArrangement;

                        sqlParameter[2] = new SqlParameter("@idArticle", SqlDbType.NVarChar);
                        sqlParameter[2].Value = (dr.idArticle == null) ? SqlString.Null : dr.idArticle;

                        sqlParameter[3] = new SqlParameter("@idClient", SqlDbType.Int);
                        sqlParameter[3].Value = (dr.idClient == 0) ? SqlInt32.Null : dr.idClient;

                        sqlParameter[4] = new SqlParameter("@nrArticle", SqlDbType.Int);
                        sqlParameter[4].Value = (dr.nrArticle == 0) ? SqlInt32.Null : dr.nrArticle;

                        sqlParameter[5] = new SqlParameter("@pricePerArticle", SqlDbType.Decimal);
                        sqlParameter[5].Value = (dr.pricePerArticle == 0) ? SqlDecimal.Null : dr.pricePerArticle;

                        sqlParameter[6] = new SqlParameter("@pricePerQuantity", SqlDbType.Decimal);
                        sqlParameter[6].Value = (dr.pricePerQuantity == 0) ? SqlDecimal.Null : dr.pricePerQuantity;

                        sqlParameter[7] = new SqlParameter("@priceTotal", SqlDbType.Decimal);
                        sqlParameter[7].Value = (dr.priceTotal == 0) ? SqlDecimal.Null : dr.priceTotal;

                        sqlParameter[8] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
                        sqlParameter[8].Value = (dr.dtFrom == null) ? SqlDateTime.MinValue : dr.dtFrom;

                        sqlParameter[9] = new SqlParameter("@dtTo", SqlDbType.DateTime);
                        sqlParameter[9].Value = (dr.dtTo == null) ? SqlDateTime.MinValue : dr.dtTo;

                        sqlParameter[10] = new SqlParameter("@idUserCreated", SqlDbType.Int);
                        sqlParameter[10].Value = (dr.idUserCreated == 0) ? SqlInt32.Null : dr.idUserCreated;

                        sqlParameter[11] = new SqlParameter("@dtUserCreated", SqlDbType.DateTime);
                        sqlParameter[11].Value = (dr.dtUserCreated == null) ? SqlDateTime.MinValue : dr.dtUserCreated;

                        sqlParameter[12] = new SqlParameter("@isExtra", SqlDbType.Bit);
                        sqlParameter[12].Value = (dr.isExtra == null) ? false : dr.isExtra;

                        sqlParameter[13] = new SqlParameter("@idContract", SqlDbType.Int);
                        sqlParameter[13].Value = (dr.idContract == SqlInt32.Null) ?  0 : dr.idContract;

                        sqlParameter[14] = new SqlParameter("@commission", SqlDbType.Decimal);
                        sqlParameter[14].Value = dr.commission;

                        sqlParameter[15] = new SqlParameter("@isAway", SqlDbType.Bit);
                        sqlParameter[15].Value = dr.isAway;

                        sqlParameter[16] = new SqlParameter("@isBack", SqlDbType.Bit);
                        sqlParameter[16].Value = dr.isBack;

                        sqlParameter[17] = new SqlParameter("@isAccomodation", SqlDbType.Bit);
                        sqlParameter[17].Value = dr.isAccomodation;

                        sqlParameter[18] = new SqlParameter("@isNotInAccompaniment", SqlDbType.Bit);
                        sqlParameter[18].Value = dr.isNotInAccompaniment;

                        sqlParameter[19] = new SqlParameter("@isNotForTraveler", SqlDbType.Bit);
                        sqlParameter[19].Value = dr.isNotForTraveler;

                        _queryList.Add(query);
                        sqlParametersList.Add(sqlParameter);

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
                        sqlParameter[4].Value = dr.idArrangementPrice.ToString() + "_" + idArrangement.ToString() + "_" + dr.idArticle.ToString();

                        sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                        sqlParameter[5].Value = "idArrangementPrice_idArrangement";

                        sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                        sqlParameter[6].Value = "ArrangementCalculationFirstArticles";

                        sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                        sqlParameter[7].Value = "Insert";

                        _queryList.Add(query);
                        sqlParametersList.Add(sqlParameter);


                        i++;
                    }
                }
                res = conn.executQueryTransaction(_queryList, sqlParametersList);
            }
            return res;

        }

        public Boolean insertArrangementLabelsFirst(List<LabelForArrangement> label,string nameForm,int idUser)
        {

            Boolean res = false;
            List<string> _queryList = new List<string>();
            List<SqlParameter[]> sqlParametersList = new List<SqlParameter[]>();
            string query;
            SqlParameter[] sqlParameter;

                int i = 0;
                foreach (LabelForArrangement drmodel in label)
                {
                    if (i == 0)
                    {
                        string queryDelete = string.Format(@"DELETE FROM ArrangementLabelFirst WHERE idArrangement = @idArrangement");
                        SqlParameter[] sqlParametersDelete = new SqlParameter[1];
                        sqlParametersDelete[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
                        sqlParametersDelete[0].Value = drmodel.idArrangement;
                        _queryList.Add(queryDelete);
                        sqlParametersList.Add(sqlParametersDelete);

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
                        sqlParameter[4].Value = drmodel.idArrangement.ToString();

                        sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                        sqlParameter[5].Value = "idArrangement";

                        sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                        sqlParameter[6].Value = "ArrangementLabelFirst";

                        sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                        sqlParameter[7].Value = "Delete";

                        _queryList.Add(query);
                        sqlParametersList.Add(sqlParameter);


                      //  return conn.executQueryTransaction(_queryList, sqlParametersList);
                    }
                         query = string.Format(@"INSERT INTO ArrangementLabelFirst(idLabel,idArrangement)
                        VALUES (@idLabel,@idArrangement)");

                         sqlParameter = new SqlParameter[2];

                        sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
                        sqlParameter[0].Value = (drmodel.idArrangement == 0) ? 0 : drmodel.idArrangement;

                        sqlParameter[1] = new SqlParameter("@idLabel", SqlDbType.Int);
                        sqlParameter[1].Value = (drmodel.idLabel == 0) ? 0 : drmodel.idLabel;

                        _queryList.Add(query);
                        sqlParametersList.Add(sqlParameter);


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
                        sqlParameter[4].Value = drmodel.idArrangement.ToString() + "_" + drmodel.idLabel.ToString();

                        sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                        sqlParameter[5].Value = "idArrangement_idLabel";

                        sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                        sqlParameter[6].Value = "ArrangementLabelFirst";

                        sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                        sqlParameter[7].Value = "Insert";

                        _queryList.Add(query);
                        sqlParametersList.Add(sqlParameter);


                        i++;
                }
                res = conn.executQueryTransaction(_queryList, sqlParametersList);
                return res;

        }

        public Boolean SaveNotArticles(ArrangementCalculationFirstNotArticlesModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"SELECT idArrangement FROM ArrangementCalculationFirstNotArticles WHERE idArrangement = @idArrangement");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[0].Value = model.idArrangement;

            if (conn.executeSelectQuery(query, sqlParameter).Rows.Count <= 0)
            {

                query = string.Format(@"INSERT INTO ArrangementCalculationFirstNotArticles (idArrangement,dtFromArrangement,dtToArrangement,nrOfNights,nrTraveler,minNrTraveler,nrVoluntaryHelper,idCountry,idUserFinished,dtUserFinished) 
                      VALUES(@idArrangement,@dtFromArrangement,@dtToArrangement,@nrOfNights,@nrTraveler,@minNrTraveler,@nrVoluntaryHelper,@idCountry,@idUserFinished,@dtUserFinished)");


                sqlParameter = new SqlParameter[10];

                sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
                sqlParameter[0].Value = model.idArrangement;

                sqlParameter[1] = new SqlParameter("@dtFromArrangement", SqlDbType.DateTime);
                sqlParameter[1].Value = model.dtFromArrangement;

                sqlParameter[2] = new SqlParameter("@dtToArrangement", SqlDbType.DateTime);
                sqlParameter[2].Value = model.dtToArrangement;

                sqlParameter[3] = new SqlParameter("@nrOfNights", SqlDbType.Int);
                sqlParameter[3].Value = model.nrOfNights;

                sqlParameter[4] = new SqlParameter("@nrTraveler", SqlDbType.Int);
                sqlParameter[4].Value = model.nrTraveler;

                sqlParameter[5] = new SqlParameter("@minNrTraveler", SqlDbType.Int);
                sqlParameter[5].Value = model.minNrTraveler;

                sqlParameter[6] = new SqlParameter("@nrVoluntaryHelper", SqlDbType.Int);
                sqlParameter[6].Value = model.nrVoluntaryHelper;

                sqlParameter[7] = new SqlParameter("@idCountry", SqlDbType.Int);
                sqlParameter[7].Value = model.idCountry;

                sqlParameter[8] = new SqlParameter("@idUserFinished", SqlDbType.Int);
                sqlParameter[8].Value = model.idUserFinished;

                sqlParameter[9] = new SqlParameter("@dtUserFinished", SqlDbType.DateTime);
                sqlParameter[9].Value = model.dtUserFinished;

               // sqlParameters[10] = new SqlParameter("@buSupportingArms", SqlDbType.Int);
              //  sqlParameters[10].Value = model.buSupportingArms;

               // sqlParameters[11] = new SqlParameter("@nrAnchorage", SqlDbType.Int);
              //  sqlParameters[11].Value = model.nrAnchorage;

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
                sqlParameter[4].Value = model.idArrangement;

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idArrangement";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "ArrangementCalculationFirstNotArticles";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Insert";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);


                return conn.executQueryTransaction(_query, sqlParameters);

            }
            else
            {
                query = string.Format(@"UPDATE ArrangementCalculationFirstNotArticles SET dtFromArrangement = @dtFromArrangement,dtToArrangement=@dtToArrangement,nrOfNights=@nrOfNights,nrTraveler=@nrTraveler, 
                                       minNrTraveler=@minNrTraveler,nrVoluntaryHelper=@nrVoluntaryHelper,idCountry = @idCountry,idUserFinished = @idUserFinished,dtUserFinished = @dtUserFinished WHERE idArrangement = @idArrangement");


                sqlParameter = new SqlParameter[10];

                sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
                sqlParameter[0].Value = model.idArrangement;

                sqlParameter[1] = new SqlParameter("@dtFromArrangement", SqlDbType.DateTime);
                sqlParameter[1].Value = model.dtFromArrangement;

                sqlParameter[2] = new SqlParameter("@dtToArrangement", SqlDbType.DateTime);
                sqlParameter[2].Value = model.dtToArrangement;

                sqlParameter[3] = new SqlParameter("@nrOfNights", SqlDbType.Int);
                sqlParameter[3].Value = model.nrOfNights;

                sqlParameter[4] = new SqlParameter("@nrTraveler", SqlDbType.Int);
                sqlParameter[4].Value = model.nrTraveler;

                sqlParameter[5] = new SqlParameter("@minNrTraveler", SqlDbType.Int);
                sqlParameter[5].Value = model.minNrTraveler;

                sqlParameter[6] = new SqlParameter("@nrVoluntaryHelper", SqlDbType.Int);
                sqlParameter[6].Value = model.nrVoluntaryHelper;

                sqlParameter[7] = new SqlParameter("@idCountry", SqlDbType.Int);
                sqlParameter[7].Value = model.idCountry;

                sqlParameter[8] = new SqlParameter("@idUserFinished", SqlDbType.Int);
                sqlParameter[8].Value = model.idUserFinished;

                sqlParameter[9] = new SqlParameter("@dtUserFinished", SqlDbType.DateTime);
                sqlParameter[9].Value = model.dtUserFinished;

                //sqlParameters[10] = new SqlParameter("@buSupportingArms", SqlDbType.Int);
                //sqlParameters[10].Value = model.buSupportingArms;

                //sqlParameters[11] = new SqlParameter("@nrAnchorage", SqlDbType.Int);
                //sqlParameters[11].Value = model.nrAnchorage;
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
                sqlParameter[4].Value = model.idArrangement;

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idArrangement";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "ArrangementCalculationFirstNotArticles";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "UPDATE";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);


                return conn.executQueryTransaction(_query, sqlParameters);

            }

        }

    }
}
