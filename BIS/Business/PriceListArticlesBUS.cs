using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.DAO;
using BIS.Model;


namespace BIS.Business
{
    public class PriceListArticlesBUS
    {
        private PriceListArticlesDAO priceDAO;

        public PriceListArticlesBUS()
        {
            priceDAO = new PriceListArticlesDAO();
        }


        //odavde valja
        public List<PriceListArticlesModel> GetAllArticlesByPriceList(int idPriceList)
        {
            DataTable dataTable = new DataTable();
            dataTable = priceDAO.GetAllArticlesByPriceList(idPriceList);
            List<PriceListArticlesModel> arrange = new List<PriceListArticlesModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PriceListArticlesModel model = new PriceListArticlesModel();

                        if (dr["idPriceListArticle"].ToString() != "")
                            model.idPriceListArticle = Convert.ToInt32(dr["idPriceListArticle"].ToString());

                        if (dr["idPricelist"].ToString() != "")
                            model.idPriceList = Convert.ToInt32(dr["idPricelist"].ToString());

                        if (dr["idArticle"].ToString() != "")
                            model.IdArticle = dr["idArticle"].ToString();

                        if (dr["nameArtical"].ToString() != "")
                            model.nameArtical = dr["nameArtical"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.IdClient = Convert.ToInt32(dr["idClient"].ToString());

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

                        if (dr["nrArticle"].ToString() != "")
                            model.NrArticle = Convert.ToInt32(dr["nrArticle"].ToString());

                        if (dr["quantity"].ToString() != "")
                            model.quantity = Convert.ToInt32(dr["quantity"].ToString());

                        if (dr["dtFrom"].ToString() != "")
                            model.DtFrom = Convert.ToDateTime(dr["dtFrom"].ToString());

                        if (dr["dtTo"].ToString() != "")
                            model.DtTo = Convert.ToDateTime(dr["dtTo"].ToString());

                        if (dr["nrDays"].ToString() != "")
                            model.NrDays = Convert.ToInt32(dr["nrDays"].ToString());

                        if (dr["isExtra"].ToString() != "")
                            model.IsExtra = Convert.ToBoolean(dr["isExtra"].ToString());

                        if (dr["isAway"].ToString() != "")
                            model.IsAway = Convert.ToBoolean(dr["isAway"].ToString());

                        if (dr["isBack"].ToString() != "")
                            model.IsBack = Convert.ToBoolean(dr["isBack"].ToString());

                        if (dr["isAccomodation"].ToString() != "")
                            model.IsAccomodation = Convert.ToBoolean(dr["isAccomodation"].ToString());

                        if (dr["isNotInAccompaniment"].ToString() != "")
                            model.IsNotInAccompaniment = Convert.ToBoolean(dr["isNotInAccompaniment"].ToString());

                        if (dr["isNotForTraveler"].ToString() != "")
                            model.IsNotForTraveler = Convert.ToBoolean(dr["isNotForTraveler"].ToString());

                        if (dr["commission"].ToString() != "")
                            model.Commission = Convert.ToDecimal(dr["commission"].ToString());

                        if (dr["pricePerArticle"].ToString() != "")
                            model.PricePerArticle = Convert.ToDecimal(dr["pricePerArticle"].ToString());

                        if (dr["pricePerQuantity"].ToString() != "")
                            model.PricePerQuantity = Convert.ToDecimal(dr["pricePerQuantity"].ToString());

                        if (dr["priceTotal"].ToString() != "")
                            model.priceTotal = Convert.ToDecimal(dr["priceTotal"].ToString());

                        arrange.Add(model);
                    }
                    return arrange;

                }
                else
                    return null;
            }
            else
                return null;
        }
        //==== provera radi brisanja artikla
        public List<PriceListArticlesModel> GetArticleByPriceList(string idArticle)
        {
            DataTable dataTable = new DataTable();
            dataTable = priceDAO.GetArticlePriceList(idArticle);
            List<PriceListArticlesModel> arrange = new List<PriceListArticlesModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PriceListArticlesModel model = new PriceListArticlesModel();

                        if (dr["idPriceListArticle"].ToString() != "")
                            model.idPriceListArticle = Convert.ToInt32(dr["idPriceListArticle"].ToString());

                        if (dr["idPricelist"].ToString() != "")
                            model.idPriceList = Convert.ToInt32(dr["idPricelist"].ToString());

                        if (dr["idArticle"].ToString() != "")
                            model.IdArticle = dr["idArticle"].ToString();

                        if (dr["nameArtical"].ToString() != "")
                            model.nameArtical = dr["nameArtical"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.IdClient = Convert.ToInt32(dr["idClient"].ToString());

                        if (dr["nrArticle"].ToString() != "")
                            model.NrArticle = Convert.ToInt32(dr["nrArticle"].ToString());

                        if (dr["quantity"].ToString() != "")
                            model.quantity = Convert.ToInt32(dr["quantity"].ToString());

                        if (dr["dtFrom"].ToString() != "")
                            model.DtFrom = Convert.ToDateTime(dr["dtFrom"].ToString());

                        if (dr["dtTo"].ToString() != "")
                            model.DtTo = Convert.ToDateTime(dr["dtTo"].ToString());

                        if (dr["nrDays"].ToString() != "")
                            model.NrDays = Convert.ToInt32(dr["nrDays"].ToString());

                        if (dr["isExtra"].ToString() != "")
                            model.IsExtra = Convert.ToBoolean(dr["isExtra"].ToString());

                        if (dr["isAway"].ToString() != "")
                            model.IsAway = Convert.ToBoolean(dr["isAway"].ToString());

                        if (dr["isBack"].ToString() != "")
                            model.IsBack = Convert.ToBoolean(dr["isBack"].ToString());

                        if (dr["isAccomodation"].ToString() != "")
                            model.IsAccomodation = Convert.ToBoolean(dr["isAccomodation"].ToString());

                        if (dr["isNotInAccompaniment"].ToString() != "")
                            model.IsNotInAccompaniment = Convert.ToBoolean(dr["isNotInAccompaniment"].ToString());

                        if (dr["isNotForTraveler"].ToString() != "")
                            model.IsNotForTraveler = Convert.ToBoolean(dr["isNotForTraveler"].ToString());

                        if (dr["commission"].ToString() != "")
                            model.Commission = Convert.ToDecimal(dr["commission"].ToString());

                        if (dr["pricePerArticle"].ToString() != "")
                            model.PricePerArticle = Convert.ToDecimal(dr["pricePerArticle"].ToString());

                        if (dr["pricePerQuantity"].ToString() != "")
                            model.PricePerQuantity = Convert.ToDecimal(dr["pricePerQuantity"].ToString());

                        if (dr["priceTotal"].ToString() != "")
                            model.priceTotal = Convert.ToDecimal(dr["priceTotal"].ToString());

                        arrange.Add(model);
                    }
                    return arrange;

                }
                else
                    return null;
            }
            else
                return null;
        }
        //=============

        public Boolean GetArticleByPriceListArticle(int idPriceListArticle, int idPriceList)
        {
            Boolean res = false;
            DataTable dataTable = new DataTable();
            dataTable = priceDAO.GetArticleByPriceListArticle(idPriceListArticle,idPriceList);
            List<PriceListArticlesModel> arrange = new List<PriceListArticlesModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    res = true;
                }
            }
            return res;
        }


        public int Save(PriceListArticlesModel model, string nameForm, int idUser)
        {
            int retval = 0;
            try
            {

                retval = priceDAO.Save(model,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(PriceListArticlesModel model, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = priceDAO.Update(model,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }



    }
}