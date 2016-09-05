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
    public class ArrangementInvoicePriceBUS
    {
        private ArrangementInvoicePriceDAO arrangeInvoiceDAO;


        public ArrangementInvoicePriceBUS()
        {
            arrangeInvoiceDAO = new ArrangementInvoicePriceDAO();

        }


        public Boolean Save(ArrangementInvoicePriceModel arrangemodel, string nameForm, int idUser)
        {
            Boolean retval = false;
            try
            {

                retval = arrangeInvoiceDAO.Save(arrangemodel,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }



        
        public bool Delete(int id, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = arrangeInvoiceDAO.Delete(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }



        public List<ArrangementInvoicePriceModel> GetArrangementById(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeInvoiceDAO.GetArrangementById(idArrangement);
            List<ArrangementInvoicePriceModel> arrange = new List<ArrangementInvoicePriceModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    ArrangementInvoicePriceModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementInvoicePriceModel();

                        model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["idArticle"].ToString() != "")
                            model.idArticle = dr["idArticle"].ToString();

                        if (dr["descriptionArticle"].ToString() != "")
                            model.descriptionArticle = dr["descriptionArticle"].ToString();

                        if (dr["nrArticle"].ToString() != "")
                            model.nrArticle = Int32.Parse(dr["nrArticle"].ToString());

                        if (dr["purchasePrice"].ToString() != "")
                            model.purchasePrice = Decimal.Parse(dr["purchasePrice"].ToString());

                        if (dr["purchasePriceTotal"].ToString() != "")
                            model.purchasePriceTotal = Decimal.Parse(dr["purchasePriceTotal"].ToString());

                        if (dr["sellingPrice"].ToString() != "")
                            model.sellingPrice = Decimal.Parse(dr["sellingPrice"].ToString());

                        if (dr["calculation"].ToString() != "")
                            model.calculation = Boolean.Parse(dr["calculation"].ToString());

                        if (dr["isExtra"].ToString() != "")
                            model.isExtra = Boolean.Parse(dr["isExtra"].ToString());

                        if (dr["isOption"].ToString() != "")
                            model.isOption = Boolean.Parse(dr["isOption"].ToString());


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
        public List<ArrangementInvoicePriceModel> GetInvoicePrice(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeInvoiceDAO.GetInvoicePrice(idArrangement);
            List<ArrangementInvoicePriceModel> arrange = new List<ArrangementInvoicePriceModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    ArrangementInvoicePriceModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementInvoicePriceModel();

                        model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["idArticle"].ToString() != "")
                            model.idArticle = dr["idArticle"].ToString();

                        if (dr["descriptionArticle"].ToString() != "")
                            model.descriptionArticle = dr["descriptionArticle"].ToString();

                        if (dr["nrArticle"].ToString() != "")
                            model.nrArticle = Int32.Parse(dr["nrArticle"].ToString());

                        if (dr["purchasePrice"].ToString() != "")
                            model.purchasePrice = Decimal.Parse(dr["purchasePrice"].ToString());

                        if (dr["purchasePriceTotal"].ToString() != "")
                            model.purchasePriceTotal = Decimal.Parse(dr["purchasePriceTotal"].ToString());

                        if (dr["sellingPrice"].ToString() != "")
                            model.sellingPrice = Decimal.Parse(dr["sellingPrice"].ToString());

                        if (dr["calculation"].ToString() != "")
                            model.calculation = Boolean.Parse(dr["calculation"].ToString());

                        if (dr["isExtra"].ToString() != "")
                            model.isExtra = Boolean.Parse(dr["isExtra"].ToString());

                        if (dr["isOption"].ToString() != "")
                            model.isOption = Boolean.Parse(dr["isOption"].ToString());


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

        public List<ArrangementInvoicePriceModel> GetInvoicePriceItemsOption(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeInvoiceDAO.GetInvoicePriceItemsOption(idArrangement);
            List<ArrangementInvoicePriceModel> arrange = new List<ArrangementInvoicePriceModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    ArrangementInvoicePriceModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementInvoicePriceModel();

                        model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["idArticle"].ToString() != "")
                            model.idArticle = dr["idArticle"].ToString();

                        if (dr["descriptionArticle"].ToString() != "")
                            model.descriptionArticle = dr["descriptionArticle"].ToString();

                        if (dr["nrArticle"].ToString() != "")
                            model.nrArticle = Int32.Parse(dr["nrArticle"].ToString());

                        if (dr["purchasePrice"].ToString() != "")
                            model.purchasePrice = Decimal.Parse(dr["purchasePrice"].ToString());

                        if (dr["purchasePriceTotal"].ToString() != "")
                            model.purchasePriceTotal = Decimal.Parse(dr["purchasePriceTotal"].ToString());

                        if (dr["sellingPrice"].ToString() != "")
                            model.sellingPrice = Decimal.Parse(dr["sellingPrice"].ToString());

                        if (dr["calculation"].ToString() != "")
                            model.calculation = Boolean.Parse(dr["calculation"].ToString());

                        if (dr["isExtra"].ToString() != "")
                            model.isExtra = Boolean.Parse(dr["isExtra"].ToString());

                        if (dr["isOption"].ToString() != "")
                            model.isOption = Boolean.Parse(dr["isOption"].ToString());


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
        public List<ArrangementInvoicePriceModel> GetInvoicePriceItemsExtra(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeInvoiceDAO.GetInvoicePriceItemsExtra(idArrangement);
            List<ArrangementInvoicePriceModel> arrange = new List<ArrangementInvoicePriceModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    ArrangementInvoicePriceModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementInvoicePriceModel();

                        model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["idArticle"].ToString() != "")
                            model.idArticle = dr["idArticle"].ToString();

                        if (dr["descriptionArticle"].ToString() != "")
                            model.descriptionArticle = dr["descriptionArticle"].ToString();

                        if (dr["nrArticle"].ToString() != "")
                            model.nrArticle = Int32.Parse(dr["nrArticle"].ToString());

                        if (dr["purchasePrice"].ToString() != "")
                            model.purchasePrice = Decimal.Parse(dr["purchasePrice"].ToString());

                        if (dr["purchasePriceTotal"].ToString() != "")
                            model.purchasePriceTotal = Decimal.Parse(dr["purchasePriceTotal"].ToString());

                        if (dr["sellingPrice"].ToString() != "")
                            model.sellingPrice = Decimal.Parse(dr["sellingPrice"].ToString());

                        if (dr["calculation"].ToString() != "")
                            model.calculation = Boolean.Parse(dr["calculation"].ToString());

                        if (dr["isExtra"].ToString() != "")
                            model.isExtra = Boolean.Parse(dr["isExtra"].ToString());

                        if (dr["isOption"].ToString() != "")
                            model.isOption = Boolean.Parse(dr["isOption"].ToString());


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

        public List<ArrangementInvoicePriceModel> GetSellingByIdArrangement(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeInvoiceDAO.GetSellingByIdArrangement(idArrangement);
            List<ArrangementInvoicePriceModel> arrange = new List<ArrangementInvoicePriceModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    ArrangementInvoicePriceModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrangementInvoicePriceModel();

                        model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        if (dr["idArticle"].ToString() != "")
                            model.idArticle = dr["idArticle"].ToString();

                        if (dr["descriptionArticle"].ToString() != "")
                            model.descriptionArticle = dr["descriptionArticle"].ToString();

                        if (dr["nrArticle"].ToString() != "")
                            model.nrArticle = Int32.Parse(dr["nrArticle"].ToString());

                        if (dr["purchasePrice"].ToString() != "")
                            model.purchasePrice = Decimal.Parse(dr["purchasePrice"].ToString());

                        if (dr["purchasePriceTotal"].ToString() != "")
                            model.purchasePriceTotal = Decimal.Parse(dr["purchasePriceTotal"].ToString());

                        if (dr["sellingPrice"].ToString() != "")
                            model.sellingPrice = Decimal.Parse(dr["sellingPrice"].ToString());

                        if (dr["calculation"].ToString() != "")
                            model.calculation = Boolean.Parse(dr["calculation"].ToString());

                        if (dr["isExtra"].ToString() != "")
                            model.isExtra = Boolean.Parse(dr["isExtra"].ToString());

                        if (dr["isOption"].ToString() != "")
                            model.isOption = Boolean.Parse(dr["isOption"].ToString());


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
       
    }
}