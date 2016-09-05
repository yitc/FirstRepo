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
    public class PriceListBUS
    {
        private PriceListDAO priceDAO;

        public PriceListBUS()
        {
            priceDAO = new PriceListDAO();
        }


        public int Save(PriceListModel model, string nameForm, int idUser)
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

        public bool Delete(int idPriceList, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = priceDAO.Delete(idPriceList,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteArticle(int idPriceList, string idArticle, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = priceDAO.DeleteArticle(idPriceList, idArticle,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(PriceListModel model, string nameForm, int idUser)
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


        public int checkForDelete(int idPriceList)
        {
            int ret = 0;
            DataTable dataTable = new DataTable();
            dataTable = priceDAO.checkForDelete(idPriceList);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                        ret = dataTable.Rows.Count;
                }
            }
            return ret;
        }

        //==== provera radi brisanja artikla iz ugovora
        public int checkForDeleteArticle(int idPriceList, string idArticle)
        {
            int ret = 0;
            DataTable dataTable = new DataTable();
            dataTable = priceDAO.checkForDeleteArticle(idPriceList, idArticle);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ret = dataTable.Rows.Count;
                }
            }
            return ret;
        }
        //=============

        public List<PriceListModel> GetAllPriceLists(int idClient)
        {
            DataTable dataTable = new DataTable();
            dataTable = priceDAO.GetAllPriceLists(idClient);
            List<PriceListModel> arrange = new List<PriceListModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PriceListModel model = new PriceListModel();

                        if (dr["idPricelist"].ToString() != "")
                            model.idPriceList = Convert.ToInt32(dr["idPricelist"].ToString());

                        if (dr["dtPriceList"].ToString() != "")
                            model.dtPriceList = Convert.ToDateTime(dr["dtPriceList"].ToString());

                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Convert.ToInt32(dr["idArrangement"].ToString());

                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Convert.ToInt32(dr["idClient"].ToString());

                        if (dr["dtFrom"].ToString() != "")
                            model.dtFrom = Convert.ToDateTime(dr["dtFrom"].ToString());

                        if (dr["dtTo"].ToString() != "")
                            model.dtTo = Convert.ToDateTime(dr["dtTo"].ToString());

                        if (dr["isActive"].ToString() != "")
                            model.isActive = Convert.ToBoolean(dr["isActive"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Convert.ToInt32(dr["idUserCreated"].ToString());

                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();

                        if (dr["dtUserCreated"].ToString() != "")
                            model.dtUserCreated = Convert.ToDateTime(dr["dtUserCreated"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Convert.ToInt32(dr["idUserModified"].ToString());

                        if (dr["nameUserModified"].ToString() != "")
                            model.nameUserModified = dr["nameUserModified"].ToString();

                        if (dr["dtUserModified"].ToString() != "")
                            model.dtUserModified = Convert.ToDateTime(dr["dtUserModified"].ToString());

                        if (dr["isReleaseDate"].ToString() != "")
                            model.isReleaseDate = Convert.ToBoolean(dr["isReleaseDate"].ToString());

                        if (dr["idHotelService"].ToString() != "")
                            model.idHotelService = Convert.ToInt32(dr["idHotelService"].ToString());

                        if (dr["nameHotelService"].ToString() != "")
                            model.nameHotelService = dr["nameHotelService"].ToString();

                        if (dr["statusArrangement"].ToString() != "")
                            model.statusArrangement = dr["statusArrangement"].ToString();
                      
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

        public PriceListModel GetPriceList(int idPriceList)
        {
            DataTable dataTable = new DataTable();
            dataTable = priceDAO.GetPriceList(idPriceList);
            PriceListModel model = new PriceListModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {

                        if (dr["idPricelist"].ToString() != "")
                            model.idPriceList = Convert.ToInt32(dr["idPricelist"].ToString());

                        if (dr["dtPriceList"].ToString() != "")
                            model.dtPriceList = Convert.ToDateTime(dr["dtPriceList"].ToString());

                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Convert.ToInt32(dr["idArrangement"].ToString());

                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Convert.ToInt32(dr["idClient"].ToString());

                        if (dr["dtFrom"].ToString() != "")
                            model.dtFrom = Convert.ToDateTime(dr["dtFrom"].ToString());

                        if (dr["dtTo"].ToString() != "")
                            model.dtTo = Convert.ToDateTime(dr["dtTo"].ToString());

                        if (dr["isActive"].ToString() != "")
                            model.isActive = Convert.ToBoolean(dr["isActive"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Convert.ToInt32(dr["idUserCreated"].ToString());

                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();

                        if (dr["dtUserCreated"].ToString() != "")
                            model.dtUserCreated = Convert.ToDateTime(dr["dtUserCreated"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Convert.ToInt32(dr["idUserModified"].ToString());

                        if (dr["nameUserModified"].ToString() != "")
                            model.nameUserModified = dr["nameUserModified"].ToString();

                        if (dr["dtUserModified"].ToString() != "")
                            model.dtUserModified = Convert.ToDateTime(dr["dtUserModified"].ToString());

                        if (dr["isReleaseDate"].ToString() != "")
                            model.isReleaseDate = Convert.ToBoolean(dr["isReleaseDate"].ToString());

                        if (dr["idHotelService"].ToString() != "")
                            model.idHotelService = Convert.ToInt32(dr["idHotelService"].ToString());

                        break;

                    }
                    return model;

                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}