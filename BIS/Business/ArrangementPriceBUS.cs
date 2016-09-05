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
    public class ArrangementPriceBUS
    {
        private ArrangementPriceDAO arrangeDAO;

        public ArrangementPriceBUS()
        {
            arrangeDAO = new ArrangementPriceDAO();
        }

        public List<IModel> GetAllArticals(int idArrangement, int idClient)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetAllArticals(idArrangement, idClient);
            List<IModel> arrange = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArticalModel model = new ArticalModel();

                        model.codeArtical = dr["codeArtical"].ToString();
                        model.nameArtical = dr["nameArtical"].ToString();
                        model.codeArtikalGroup = dr["codeArtikalGroup"].ToString();
                        model.nameArtikalGroup = dr["nameArticalGroup"].ToString();

                        if (dr["quantity"].ToString() != "")
                            model.quantity = Convert.ToInt32(dr["quantity"].ToString());


                        if (dr["purchasePrice"].ToString() != "")
                            model.purchasePrice = Decimal.Parse(dr["purchasePrice"].ToString());

                        if (dr["sellingPrice"].ToString() != "")
                            model.sellingPrice = Decimal.Parse(dr["sellingPrice"].ToString());

                        if (dr["isGroup"].ToString() != "")
                            model.isGroup = Convert.ToBoolean(dr["isGroup"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtUserCreated"].ToString() != "")
                            model.dtUserCreated = DateTime.Parse(dr["dtUserCreated"].ToString());

                        if (dr["idUserModifies"].ToString() != "")
                            model.idUserModifies = Int32.Parse(dr["idUserModifies"].ToString());

                        if (dr["dtUserModified"].ToString() != "")
                            model.dtUserModified = DateTime.Parse(dr["dtUserModified"].ToString());

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

        public Boolean checkIfArrangementPriceAlreadyInRooms(int id, int idArrangement, string idArticle)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.checkIfArrangementPriceAlreadyInRooms(id, idArrangement, idArticle);
            Boolean res = false;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    res = true;

                }
            }
            return res;
        }

        public List<ArrangementPriceModel> GetArticalByID(string idArtical)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetArticalByID(idArtical);
            List<ArrangementPriceModel> arrange = new List<ArrangementPriceModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementPriceModel model = new ArrangementPriceModel();

                        if (dr["idArrangementPrice"].ToString() != "")
                            model.idArrangementPrice = Int32.Parse(dr["idArrangementPrice"].ToString());

                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["idArticle"].ToString() != "")
                            model.idArticle = dr["idArticle"].ToString();

                        if (dr["nameArticle"].ToString() != "")
                            model.nameArticle = dr["nameArticle"].ToString();

                        if (dr["nrArticle"].ToString() != "")
                            model.nrArticle = Int32.Parse(dr["nrArticle"].ToString());

                        if (dr["quantity"].ToString() != "")
                            model.quantity = Int32.Parse(dr["quantity"].ToString());

                        //if (dr["idContract"].ToString() != "")
                        //    model.idContract = Int32.Parse(dr["idContract"].ToString());

                        if (dr["nameContract"].ToString() != "")
                            model.nameContract = dr["nameContract"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

                        if (dr["dtFrom"].ToString() != "")
                            model.dtFrom = DateTime.Parse(dr["dtFrom"].ToString());

                        if (dr["dtTo"].ToString() != "")
                            model.dtTo = DateTime.Parse(dr["dtTo"].ToString());

                        if (dr["isExtra"].ToString() != "")
                            model.isExtra = Convert.ToBoolean(dr["isExtra"].ToString());

                        if (dr["isAway"].ToString() != "")
                            model.isAway = Convert.ToBoolean(dr["isAway"].ToString());

                        if (dr["isBack"].ToString() != "")
                            model.isBack = Convert.ToBoolean(dr["isBack"].ToString());

                        if (dr["isAccomodation"].ToString() != "")
                            model.isAccomodation = Convert.ToBoolean(dr["isAccomodation"].ToString());

                        if (dr["isNotInAccompaniment"].ToString() != "")
                            model.isNotInAccompaniment = Convert.ToBoolean(dr["isNotInAccompaniment"].ToString());

                        if (dr["isNotForTraveler"].ToString() != "")
                            model.isNotForTraveler = Convert.ToBoolean(dr["isNotForTraveler"].ToString());

                        if (dr["commission"].ToString() != "")
                            model.commission = Convert.ToDecimal(dr["commission"].ToString());

                        if (dr["pricePerArticle"].ToString() != "")
                            model.pricePerArticle = Decimal.Parse(dr["pricePerArticle"].ToString());

                        if (dr["pricePerQuantity"].ToString() != "")
                            model.pricePerQuantity = Decimal.Parse(dr["pricePerQuantity"].ToString());

                        if (dr["priceTotal"].ToString() != "")
                            model.priceTotal = Decimal.Parse(dr["priceTotal"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtUserCreated"].ToString() != "")
                            model.dtUserCreated = DateTime.Parse(dr["dtUserCreated"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["dtUserModified"].ToString() != "")
                            model.dtUserModified = DateTime.Parse(dr["dtUserModified"].ToString());

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
        public List<IModel> GetAllArrangementPricesByArticleNoGroup(int idArrangment, string articalGroup, int MinNumTravelers, Boolean isFirst)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetAllArrangementPricesByArticleNoGroup(idArrangment, articalGroup, MinNumTravelers, isFirst);
            List<IModel> arrange = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementPriceModel model = new ArrangementPriceModel();

                        if (dr["idArrangementPrice"].ToString() != "")
                            model.idArrangementPrice = Int32.Parse(dr["idArrangementPrice"].ToString());

                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["idArticle"].ToString() != "")
                            model.idArticle = dr["idArticle"].ToString();

                        if (dr["nameArticle"].ToString() != "")
                            model.nameArticle = dr["nameArticle"].ToString();

                        if (dr["nrArticle"].ToString() != "")
                            model.nrArticle = Int32.Parse(dr["nrArticle"].ToString());

                        if (dr["quantity"].ToString() != "")
                            model.quantity = Int32.Parse(dr["quantity"].ToString());

                        if (dr["idContract"].ToString() != "")
                            model.idContract = Int32.Parse(dr["idContract"].ToString());

                        if (dr["nameContract"].ToString() != "")
                            model.nameContract = dr["nameContract"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

                        if (dr["dtFrom"].ToString() != "")
                            model.dtFrom = DateTime.Parse(dr["dtFrom"].ToString());

                        if (dr["dtTo"].ToString() != "")
                            model.dtTo = DateTime.Parse(dr["dtTo"].ToString());

                        if (dr["isGroup"].ToString() != "")
                            model.isGroup = Convert.ToBoolean(dr["isGroup"].ToString());

                        if (dr["isExtra"].ToString() != "")
                            model.isExtra = Convert.ToBoolean(dr["isExtra"].ToString());

                        if (dr["isAway"].ToString() != "")
                            model.isAway = Convert.ToBoolean(dr["isAway"].ToString());

                        if (dr["isBack"].ToString() != "")
                            model.isBack = Convert.ToBoolean(dr["isBack"].ToString());

                        if (dr["isAccomodation"].ToString() != "")
                            model.isAccomodation = Convert.ToBoolean(dr["isAccomodation"].ToString());

                        if (dr["isNotInAccompaniment"].ToString() != "")
                            model.isNotInAccompaniment = Convert.ToBoolean(dr["isNotInAccompaniment"].ToString());

                        if (dr["isNotForTraveler"].ToString() != "")
                            model.isNotForTraveler = Convert.ToBoolean(dr["isNotForTraveler"].ToString());

                        if (dr["commission"].ToString() != "")
                            model.commission = Convert.ToDecimal(dr["commission"].ToString());

                        if (dr["pricePerArticle"].ToString() != "")
                            model.pricePerArticle = Decimal.Parse(dr["pricePerArticle"].ToString());

                        if (dr["pricePerQuantity"].ToString() != "")
                            model.pricePerQuantity = Decimal.Parse(dr["pricePerQuantity"].ToString());

                        if (dr["priceTotal"].ToString() != "")
                            model.priceTotal = Decimal.Parse(dr["priceTotal"].ToString());

                        if (dr["total"].ToString() != "")
                            model.total = Decimal.Parse(dr["total"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtUserCreated"].ToString() != "")
                            model.dtUserCreated = DateTime.Parse(dr["dtUserCreated"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["dtUserModified"].ToString() != "")
                            model.dtUserModified = DateTime.Parse(dr["dtUserModified"].ToString());

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
        public List<IModel> GetAllArrangementPrices(int idArrangment, Boolean isFirst)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetAllArrangementPrices(idArrangment, isFirst);
            List<IModel> arrange = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementPriceModel model = new ArrangementPriceModel();

                        if (dr["idArrangementPrice"].ToString() != "")
                            model.idArrangementPrice = Int32.Parse(dr["idArrangementPrice"].ToString());

                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["idArticle"].ToString() != "")
                            model.idArticle = dr["idArticle"].ToString();

                        if (dr["nameArticle"].ToString() != "")
                            model.nameArticle = dr["nameArticle"].ToString();

                        if (dr["nrArticle"].ToString() != "")
                            model.nrArticle = Int32.Parse(dr["nrArticle"].ToString());

                        if (dr["quantity"].ToString() != "")
                            model.quantity = Int32.Parse(dr["quantity"].ToString());

                        if (dr["idContract"].ToString() != "")
                            model.idContract = Int32.Parse(dr["idContract"].ToString());

                        if (dr["nameContract"].ToString() != "")
                            model.nameContract = dr["nameContract"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

                        if (dr["dtFrom"].ToString() != "")
                            model.dtFrom = DateTime.Parse(dr["dtFrom"].ToString());

                        if (dr["dtTo"].ToString() != "")
                            model.dtTo = DateTime.Parse(dr["dtTo"].ToString());

                        if (dr["isGroup"].ToString() != "")
                            model.isGroup = Convert.ToBoolean(dr["isGroup"].ToString());

                        if (dr["isExtra"].ToString() != "")
                            model.isExtra = Convert.ToBoolean(dr["isExtra"].ToString());

                        if (dr["isAway"].ToString() != "")
                            model.isAway = Convert.ToBoolean(dr["isAway"].ToString());

                        if (dr["isBack"].ToString() != "")
                            model.isBack = Convert.ToBoolean(dr["isBack"].ToString());

                        if (dr["isAccomodation"].ToString() != "")
                            model.isAccomodation = Convert.ToBoolean(dr["isAccomodation"].ToString());

                        if (dr["isNotInAccompaniment"].ToString() != "")
                            model.isNotInAccompaniment = Convert.ToBoolean(dr["isNotInAccompaniment"].ToString());

                        if (dr["isNotForTraveler"].ToString() != "")
                            model.isNotForTraveler = Convert.ToBoolean(dr["isNotForTraveler"].ToString());

                        if (dr["commission"].ToString() != "")
                            model.commission = Convert.ToDecimal(dr["commission"].ToString());

                        if (dr["pricePerArticle"].ToString() != "")
                            model.pricePerArticle = Decimal.Parse(dr["pricePerArticle"].ToString());

                        if (dr["pricePerQuantity"].ToString() != "")
                            model.pricePerQuantity = Decimal.Parse(dr["pricePerQuantity"].ToString());

                        if (dr["priceTotal"].ToString() != "")
                            model.priceTotal = Decimal.Parse(dr["priceTotal"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtUserCreated"].ToString() != "")
                            model.dtUserCreated = DateTime.Parse(dr["dtUserCreated"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["dtUserModified"].ToString() != "")
                            model.dtUserModified = DateTime.Parse(dr["dtUserModified"].ToString());

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

        public List<IModel> GetAllArrangementPricesByArticleGroup(int idArrangment, string articalGroup, int MinNumTravelers, Boolean isFirst)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetAllArrangementPricesByArticleGroup(idArrangment, articalGroup, MinNumTravelers, isFirst);
            List<IModel> arrange = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementPriceModel model = new ArrangementPriceModel();

                        if (dr["idArrangementPrice"].ToString() != "")
                            model.idArrangementPrice = Int32.Parse(dr["idArrangementPrice"].ToString());

                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["idArticle"].ToString() != "")
                            model.idArticle = dr["idArticle"].ToString();

                        if (dr["nameArticle"].ToString() != "")
                            model.nameArticle = dr["nameArticle"].ToString();

                        if (dr["nrArticle"].ToString() != "")
                            model.nrArticle = Int32.Parse(dr["nrArticle"].ToString());

                        if (dr["quantity"].ToString() != "")
                            model.quantity = Int32.Parse(dr["quantity"].ToString());

                        if (dr["idContract"].ToString() != "")
                            model.idContract = Int32.Parse(dr["idContract"].ToString());

                        if (dr["nameContract"].ToString() != "")
                            model.nameContract = dr["nameContract"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

                        if (dr["dtFrom"].ToString() != "")
                            model.dtFrom = DateTime.Parse(dr["dtFrom"].ToString());

                        if (dr["dtTo"].ToString() != "")
                            model.dtTo = DateTime.Parse(dr["dtTo"].ToString());

                        if (dr["isGroup"].ToString() != "")
                            model.isGroup = Convert.ToBoolean(dr["isGroup"].ToString());

                        if (dr["isExtra"].ToString() != "")
                            model.isExtra = Convert.ToBoolean(dr["isExtra"].ToString());

                        if (dr["isAway"].ToString() != "")
                            model.isAway = Convert.ToBoolean(dr["isAway"].ToString());

                        if (dr["isBack"].ToString() != "")
                            model.isBack = Convert.ToBoolean(dr["isBack"].ToString());

                        if (dr["isAccomodation"].ToString() != "")
                            model.isAccomodation = Convert.ToBoolean(dr["isAccomodation"].ToString());

                        if (dr["isNotInAccompaniment"].ToString() != "")
                            model.isNotInAccompaniment = Convert.ToBoolean(dr["isNotInAccompaniment"].ToString());

                        if (dr["isNotForTraveler"].ToString() != "")
                            model.isNotForTraveler = Convert.ToBoolean(dr["isNotForTraveler"].ToString());

                        if (dr["commission"].ToString() != "")
                            model.commission = Convert.ToDecimal(dr["commission"].ToString());

                        if (dr["pricePerArticle"].ToString() != "")
                            model.pricePerArticle = Decimal.Parse(dr["pricePerArticle"].ToString());

                        if (dr["pricePerQuantity"].ToString() != "")
                            model.pricePerQuantity = Decimal.Parse(dr["pricePerQuantity"].ToString());

                        if (dr["priceTotal"].ToString() != "")
                            model.priceTotal = Decimal.Parse(dr["priceTotal"].ToString());

                        if (dr["total"].ToString() != "")
                            model.total = Decimal.Parse(dr["total"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtUserCreated"].ToString() != "")
                            model.dtUserCreated = DateTime.Parse(dr["dtUserCreated"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["dtUserModified"].ToString() != "")
                            model.dtUserModified = DateTime.Parse(dr["dtUserModified"].ToString());

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
        public List<IModel> GetAllArrangementPricesByArticleGroupWithExtra(int idArrangment, string articalGroup, int MinNumTravelers, Boolean isFirst)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetAllArrangementPricesByArticleGroupWithExtra(idArrangment, articalGroup, MinNumTravelers, isFirst);
            List<IModel> arrange = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementPriceModel model = new ArrangementPriceModel();

                        if (dr["idArrangementPrice"].ToString() != "")
                            model.idArrangementPrice = Int32.Parse(dr["idArrangementPrice"].ToString());

                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["idArticle"].ToString() != "")
                            model.idArticle = dr["idArticle"].ToString();

                        if (dr["nameArticle"].ToString() != "")
                            model.nameArticle = dr["nameArticle"].ToString();

                        if (dr["nrArticle"].ToString() != "")
                            model.nrArticle = Int32.Parse(dr["nrArticle"].ToString());

                        if (dr["quantity"].ToString() != "")
                            model.quantity = Int32.Parse(dr["quantity"].ToString());

                        if (dr["idContract"].ToString() != "")
                            model.idContract = Int32.Parse(dr["idContract"].ToString());

                        if (dr["nameContract"].ToString() != "")
                            model.nameContract = dr["nameContract"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

                        if (dr["dtFrom"].ToString() != "")
                            model.dtFrom = DateTime.Parse(dr["dtFrom"].ToString());

                        if (dr["dtTo"].ToString() != "")
                            model.dtTo = DateTime.Parse(dr["dtTo"].ToString());

                        if (dr["isGroup"].ToString() != "")
                            model.isGroup = Convert.ToBoolean(dr["isGroup"].ToString());

                        if (dr["isExtra"].ToString() != "")
                            model.isExtra = Convert.ToBoolean(dr["isExtra"].ToString());

                        if (dr["isAway"].ToString() != "")
                            model.isAway = Convert.ToBoolean(dr["isAway"].ToString());

                        if (dr["isBack"].ToString() != "")
                            model.isBack = Convert.ToBoolean(dr["isBack"].ToString());

                        if (dr["isAccomodation"].ToString() != "")
                            model.isAccomodation = Convert.ToBoolean(dr["isAccomodation"].ToString());

                        if (dr["isNotInAccompaniment"].ToString() != "")
                            model.isNotInAccompaniment = Convert.ToBoolean(dr["isNotInAccompaniment"].ToString());

                        if (dr["isNotForTraveler"].ToString() != "")
                            model.isNotForTraveler = Convert.ToBoolean(dr["isNotForTraveler"].ToString());

                        if (dr["commission"].ToString() != "")
                            model.commission = Convert.ToDecimal(dr["commission"].ToString());

                        if (dr["pricePerArticle"].ToString() != "")
                            model.pricePerArticle = Decimal.Parse(dr["pricePerArticle"].ToString());

                        if (dr["pricePerQuantity"].ToString() != "")
                            model.pricePerQuantity = Decimal.Parse(dr["pricePerQuantity"].ToString());

                        if (dr["priceTotal"].ToString() != "")
                            model.priceTotal = Decimal.Parse(dr["priceTotal"].ToString());

                        if (dr["total"].ToString() != "")
                            model.total = Decimal.Parse(dr["total"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtUserCreated"].ToString() != "")
                            model.dtUserCreated = DateTime.Parse(dr["dtUserCreated"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["dtUserModified"].ToString() != "")
                            model.dtUserModified = DateTime.Parse(dr["dtUserModified"].ToString());

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
        public List<IModel> GetAllTotalWithExtra(int idArrangment, int MinNumTravelers, Boolean isFirst, Boolean isTotalZero)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetAllTotalWithExtra(idArrangment, MinNumTravelers, isFirst, isTotalZero);
            List<IModel> arrange = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementPriceModel model = new ArrangementPriceModel();
                        if (dr["idArrangementPrice"].ToString() != "")
                            model.idArrangementPrice = Int32.Parse(dr["idArrangementPrice"].ToString());

                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["idArticle"].ToString() != "")
                            model.idArticle = dr["idArticle"].ToString();

                        if (dr["nameArticle"].ToString() != "")
                            model.nameArticle = dr["nameArticle"].ToString();

                        if (dr["nrArticle"].ToString() != "")
                            model.nrArticle = Int32.Parse(dr["nrArticle"].ToString());

                        if (dr["quantity"].ToString() != "")
                            model.quantity = Int32.Parse(dr["quantity"].ToString());

                        if (dr["idContract"].ToString() != "")
                            model.idContract = Int32.Parse(dr["idContract"].ToString());

                        if (dr["nameContract"].ToString() != "")
                            model.nameContract = dr["nameContract"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

                        if (dr["dtFrom"].ToString() != "")
                            model.dtFrom = DateTime.Parse(dr["dtFrom"].ToString());

                        if (dr["dtTo"].ToString() != "")
                            model.dtTo = DateTime.Parse(dr["dtTo"].ToString());

                        if (dr["isGroup"].ToString() != "")
                            model.isGroup = Convert.ToBoolean(dr["isGroup"].ToString());

                        if (dr["isExtra"].ToString() != "")
                            model.isExtra = Convert.ToBoolean(dr["isExtra"].ToString());

                        if (dr["isAway"].ToString() != "")
                            model.isAway = Convert.ToBoolean(dr["isAway"].ToString());

                        if (dr["isBack"].ToString() != "")
                            model.isBack = Convert.ToBoolean(dr["isBack"].ToString());

                        if (dr["isAccomodation"].ToString() != "")
                            model.isAccomodation = Convert.ToBoolean(dr["isAccomodation"].ToString());

                        if (dr["isNotInAccompaniment"].ToString() != "")
                            model.isNotInAccompaniment = Convert.ToBoolean(dr["isNotInAccompaniment"].ToString());

                        if (dr["isNotForTraveler"].ToString() != "")
                            model.isNotForTraveler = Convert.ToBoolean(dr["isNotForTraveler"].ToString());

                        if (dr["commission"].ToString() != "")
                            model.commission = Convert.ToDecimal(dr["commission"].ToString());

                        if (dr["pricePerArticle"].ToString() != "")
                            model.pricePerArticle = Decimal.Parse(dr["pricePerArticle"].ToString());

                        if (dr["pricePerQuantity"].ToString() != "")
                            model.pricePerQuantity = Decimal.Parse(dr["pricePerQuantity"].ToString());

                        if (dr["priceTotal"].ToString() != "")
                            model.priceTotal = Decimal.Parse(dr["priceTotal"].ToString());

                        if (dr["total"].ToString() != "")
                            model.total = Decimal.Parse(dr["total"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtUserCreated"].ToString() != "")
                            model.dtUserCreated = DateTime.Parse(dr["dtUserCreated"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["dtUserModified"].ToString() != "")
                            model.dtUserModified = DateTime.Parse(dr["dtUserModified"].ToString());


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

        public List<IModel> GetAllTotalWithExtraInvoice(int idArrangment, int MinNumTravelers,  Boolean isTotalZero)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetAllTotalWithExtraInvoice(idArrangment, MinNumTravelers,  isTotalZero);
            List<IModel> arrange = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementPriceModel model = new ArrangementPriceModel();
                        if (dr["idArrangementPrice"].ToString() != "")
                            model.idArrangementPrice = Int32.Parse(dr["idArrangementPrice"].ToString());

                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["idArticle"].ToString() != "")
                            model.idArticle = dr["idArticle"].ToString();

                        if (dr["nameArticle"].ToString() != "")
                            model.nameArticle = dr["nameArticle"].ToString();

                        if (dr["nrArticle"].ToString() != "")
                            model.nrArticle = Int32.Parse(dr["nrArticle"].ToString());

                        if (dr["quantity"].ToString() != "")
                            model.quantity = Int32.Parse(dr["quantity"].ToString());

                        if (dr["idContract"].ToString() != "")
                            model.idContract = Int32.Parse(dr["idContract"].ToString());

                        if (dr["nameContract"].ToString() != "")
                            model.nameContract = dr["nameContract"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

                        if (dr["dtFrom"].ToString() != "")
                            model.dtFrom = DateTime.Parse(dr["dtFrom"].ToString());

                        if (dr["dtTo"].ToString() != "")
                            model.dtTo = DateTime.Parse(dr["dtTo"].ToString());

                        if (dr["isGroup"].ToString() != "")
                            model.isGroup = Convert.ToBoolean(dr["isGroup"].ToString());

                        if (dr["isExtra"].ToString() != "")
                            model.isExtra = Convert.ToBoolean(dr["isExtra"].ToString());

                        if (dr["isAway"].ToString() != "")
                            model.isAway = Convert.ToBoolean(dr["isAway"].ToString());

                        if (dr["isBack"].ToString() != "")
                            model.isBack = Convert.ToBoolean(dr["isBack"].ToString());

                        if (dr["isAccomodation"].ToString() != "")
                            model.isAccomodation = Convert.ToBoolean(dr["isAccomodation"].ToString());

                        if (dr["isNotInAccompaniment"].ToString() != "")
                            model.isNotInAccompaniment = Convert.ToBoolean(dr["isNotInAccompaniment"].ToString());

                        if (dr["isNotForTraveler"].ToString() != "")
                            model.isNotForTraveler = Convert.ToBoolean(dr["isNotForTraveler"].ToString());

                        if (dr["commission"].ToString() != "")
                            model.commission = Convert.ToDecimal(dr["commission"].ToString());

                        if (dr["pricePerArticle"].ToString() != "")
                            model.pricePerArticle = Decimal.Parse(dr["pricePerArticle"].ToString());

                        if (dr["pricePerQuantity"].ToString() != "")
                            model.pricePerQuantity = Decimal.Parse(dr["pricePerQuantity"].ToString());

                        if (dr["priceTotal"].ToString() != "")
                            model.priceTotal = Decimal.Parse(dr["priceTotal"].ToString());

                        if (dr["total"].ToString() != "")
                            model.total = Decimal.Parse(dr["total"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtUserCreated"].ToString() != "")
                            model.dtUserCreated = DateTime.Parse(dr["dtUserCreated"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["dtUserModified"].ToString() != "")
                            model.dtUserModified = DateTime.Parse(dr["dtUserModified"].ToString());


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

        public List<ArrangementPriceModel> GetExtraAccomodation (int idArrangment,List<int> idArrangmentBook, int MinNumTravelers)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetExtraAccomodation(idArrangment,idArrangmentBook, MinNumTravelers);
            List<ArrangementPriceModel> arrange = new List<ArrangementPriceModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementPriceModel model = new ArrangementPriceModel();
                        if (dr["idArrangementPrice"].ToString() != "")
                            model.idArrangementPrice = Int32.Parse(dr["idArrangementPrice"].ToString());

                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["idArticle"].ToString() != "")
                            model.idArticle = dr["idArticle"].ToString();

                        if (dr["nameArticle"].ToString() != "")
                            model.nameArticle = dr["nameArticle"].ToString();

                        if (dr["nrArticle"].ToString() != "")
                            model.nrArticle = Int32.Parse(dr["nrArticle"].ToString());

                        if (dr["quantity"].ToString() != "")
                            model.quantity = Int32.Parse(dr["quantity"].ToString());

                        if (dr["idContract"].ToString() != "")
                            model.idContract = Int32.Parse(dr["idContract"].ToString());

                        if (dr["nameContract"].ToString() != "")
                            model.nameContract = dr["nameContract"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

                        if (dr["dtFrom"].ToString() != "")
                            model.dtFrom = DateTime.Parse(dr["dtFrom"].ToString());

                        if (dr["dtTo"].ToString() != "")
                            model.dtTo = DateTime.Parse(dr["dtTo"].ToString());

                        if (dr["isGroup"].ToString() != "")
                            model.isGroup = Convert.ToBoolean(dr["isGroup"].ToString());

                        if (dr["isExtra"].ToString() != "")
                            model.isExtra = Convert.ToBoolean(dr["isExtra"].ToString());

                        if (dr["isAway"].ToString() != "")
                            model.isAway = Convert.ToBoolean(dr["isAway"].ToString());

                        if (dr["isBack"].ToString() != "")
                            model.isBack = Convert.ToBoolean(dr["isBack"].ToString());

                        if (dr["isAccomodation"].ToString() != "")
                            model.isAccomodation = Convert.ToBoolean(dr["isAccomodation"].ToString());

                        if (dr["isNotInAccompaniment"].ToString() != "")
                            model.isNotInAccompaniment = Convert.ToBoolean(dr["isNotInAccompaniment"].ToString());

                        if (dr["isNotForTraveler"].ToString() != "")
                            model.isNotForTraveler = Convert.ToBoolean(dr["isNotForTraveler"].ToString());

                        if (dr["commission"].ToString() != "")
                            model.commission = Convert.ToDecimal(dr["commission"].ToString());

                        if (dr["pricePerArticle"].ToString() != "")
                            model.pricePerArticle = Decimal.Parse(dr["pricePerArticle"].ToString());

                        if (dr["pricePerQuantity"].ToString() != "")
                            model.pricePerQuantity = Decimal.Parse(dr["pricePerQuantity"].ToString());

                        if (dr["priceTotal"].ToString() != "")
                            model.priceTotal = Decimal.Parse(dr["priceTotal"].ToString());

                        if (dr["total"].ToString() != "")
                            model.total = Decimal.Parse(dr["total"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Int32.Parse(dr["idUserCreated"].ToString());

                        if (dr["dtUserCreated"].ToString() != "")
                            model.dtUserCreated = DateTime.Parse(dr["dtUserCreated"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Int32.Parse(dr["idUserModified"].ToString());

                        if (dr["dtUserModified"].ToString() != "")
                            model.dtUserModified = DateTime.Parse(dr["dtUserModified"].ToString());


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


        public int checkIfArrangementPriceExist(int idArrangmentPrice, int idArrangment, string idArticle, int idClient, Boolean isInsert)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.checkIfArrangementPriceExist(idArrangmentPrice, idArrangment, idArticle, idClient, isInsert);
            List<ArrangementPriceModel> arrange = new List<ArrangementPriceModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows.Count;

                }
                else
                    return 0;
            }
            else
                return 0;
        }


        public bool Save(ArrangementPriceModel model, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = arrangeDAO.Save(model,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Delete(int idArrangementPrice, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = arrangeDAO.Delete(idArrangementPrice, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }



        public bool Update(ArrangementPriceModel model, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = arrangeDAO.Update(model, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<PriceListArticlesControlModel> GetAllArticalsModel(int idArrangement, int idClient)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetAllArticalsModel(idArrangement, idClient);
            List<PriceListArticlesControlModel> arrange = new List<PriceListArticlesControlModel>();

            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PriceListArticlesControlModel model = new PriceListArticlesControlModel();

                        model.codeArtical = dr["codeArtical"].ToString();

                        if (dr["idPricelist"].ToString() != "")
                            model.idPricelist = Convert.ToInt32(dr["idPricelist"].ToString());


                        if (dr["priceperarticle"].ToString() != "")
                            model.priceperarticle = Decimal.Parse(dr["priceperarticle"].ToString());
                        if (dr["priceperquantity"].ToString() != "")
                            model.priceperquantity = Decimal.Parse(dr["priceperquantity"].ToString());
                        if (dr["priceTotal"].ToString() != "")
                            model.priceTotal = Decimal.Parse(dr["priceTotal"].ToString());

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());

                        if (dr["nrArticle"].ToString() != "")
                            model.nrArticle = Int32.Parse(dr["nrArticle"].ToString());


                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        arrange.Add(model);
                    }
                    return arrange;

                }
                else
                    return null;

            }
        }
        public List<PriceListArticlesControlModel> GetAllArticalsModelNEW(int idArrangement, int idClient)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetAllArticalsModelNEW(idArrangement, idClient);
            List<PriceListArticlesControlModel> arrange = new List<PriceListArticlesControlModel>();

            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PriceListArticlesControlModel model = new PriceListArticlesControlModel();

                        model.codeArtical = dr["idArticle"].ToString();

                        if (dr["nameArtical"].ToString() != "")
                            model.nameArtical = dr["nameArtical"].ToString();

                        if (dr["idPricelist"].ToString() != "")
                            model.idPricelist = Convert.ToInt32(dr["idPricelist"].ToString());


                        if (dr["priceperarticle"].ToString() != "")
                            model.priceperarticle = Decimal.Parse(dr["priceperarticle"].ToString());
                        if (dr["priceperquantity"].ToString() != "")
                            model.priceperquantity = Decimal.Parse(dr["priceperquantity"].ToString());
                        if (dr["priceTotal"].ToString() != "")
                            model.priceTotal = Decimal.Parse(dr["priceTotal"].ToString());

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());

                        if (dr["nrArticle"].ToString() != "")
                            model.nrArticle = Int32.Parse(dr["nrArticle"].ToString());
                        if (dr["quantity"].ToString() != "")
                            model.quantity = Int32.Parse(dr["quantity"].ToString());

                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        arrange.Add(model);
                    }
                    return arrange;

                }
                else
                    return null;

            }
        }
    }
}