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
    public class AccPageBUS
    {
        private AccPageDAO pageDAO;

        public AccPageBUS()
        {
            pageDAO = new AccPageDAO();
        }

        public bool Save(AccPageModel pagemodel)
        {
            bool retval = false;
            try
            {

                retval = pageDAO.Save(pagemodel);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }



        public bool Update(AccPageModel pagemodel)
        {
            bool retval = false;
            try
            {

                retval = pageDAO.Update(pagemodel);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Delete(int id)
        {
            bool retval = false;
            try
            {

                retval = pageDAO.Delete(id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<AccPageModel> GetAllPages(int idDaily)
        {
            DataTable dataTable = new DataTable();
            dataTable = pageDAO.GetAllPages(idDaily);
            List<AccPageModel> pagemodel = new List<AccPageModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    
                    AccPageModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccPageModel();

                        model.idAccPage = Int32.Parse(dr["idAccPage"].ToString());

                        if (dr["codeDaily"].ToString() != "")
                            model.codeDaily = dr["codeDaily"].ToString();

                        if (dr["periodPage"].ToString() != "")
                            model.periodPage = Int32.Parse(dr["periodPage"].ToString());

                        if (dr["numberPage"].ToString() != "")
                            model.numberPage = Int32.Parse(dr["numberPage"].ToString());

                        if (dr["descPage"].ToString() != "")
                            model.descPage = dr["descPage"].ToString();

                        if (dr["prevDebAmtPage"].ToString() != "")
                            model.prevDebAmtPage = Decimal.Parse(dr["prevDebAmtPage"].ToString());

                        if (dr["prevCreAmtPage"].ToString() != "")
                            model.prevCreAmtPage = Decimal.Parse(dr["prevCreAmtPage"].ToString());

                         if (dr["prevDVatPage"].ToString() != "")
                            model.prevDVatPage = Decimal.Parse(dr["prevDVatPage"].ToString());

                        if (dr["prevCVatPage"].ToString() != "")
                            model.prevCVatPage = Decimal.Parse(dr["prevCVatPage"].ToString());

                         if (dr["amountDebPage"].ToString() != "")
                            model.amountDebPage = Decimal.Parse(dr["amountDebPage"].ToString());

                        if (dr["amountCrePage"].ToString() != "")
                            model.amountCrePage = Decimal.Parse(dr["amountCrePage"].ToString());

                         if (dr["vatDebPage"].ToString() != "")
                            model.vatDebPage = Decimal.Parse(dr["vatDebPage"].ToString());

                        if (dr["vatCrePage"].ToString() != "")
                            model.vatCrePage = Decimal.Parse(dr["vatCrePage"].ToString());
                                            

                        model.statusPage = Boolean.Parse(dr["statusPage"].ToString());

                        pagemodel.Add(model);
                    }
                    return pagemodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public AccPageModel GetAllPageByID(int idAccPage)
        {
            DataTable dataTable = new DataTable();
            dataTable = pageDAO.GetAllPageByID(idAccPage);
            AccPageModel pagemodel = new AccPageModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                 //   AccPageModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        pagemodel = new AccPageModel();

                        pagemodel.idAccPage = Int32.Parse(dr["idAccPage"].ToString());

                        if (dr["codeDaily"].ToString() != "")
                            pagemodel.codeDaily = dr["codeDaily"].ToString();

                        if (dr["periodPage"].ToString() != "")
                            pagemodel.periodPage = Int32.Parse(dr["periodPage"].ToString());

                        if (dr["numberPage"].ToString() != "")
                            pagemodel.numberPage = Int32.Parse(dr["numberPage"].ToString());

                        if (dr["descPage"].ToString() != "")
                            pagemodel.descPage = dr["descPage"].ToString();

                        if (dr["prevDebAmtPage"].ToString() != "")
                            pagemodel.prevDebAmtPage = Decimal.Parse(dr["prevDebAmtPage"].ToString());

                        if (dr["prevCreAmtPage"].ToString() != "")
                            pagemodel.prevCreAmtPage = Decimal.Parse(dr["prevCreAmtPage"].ToString());

                        if (dr["prevDVatPage"].ToString() != "")
                            pagemodel.prevDVatPage = Decimal.Parse(dr["prevDVatPage"].ToString());

                        if (dr["prevCVatPage"].ToString() != "")
                            pagemodel.prevCVatPage = Decimal.Parse(dr["prevCVatPage"].ToString());

                        if (dr["amountDebPage"].ToString() != "")
                            pagemodel.amountDebPage = Decimal.Parse(dr["amountDebPage"].ToString());

                        if (dr["amountCrePage"].ToString() != "")
                            pagemodel.amountCrePage = Decimal.Parse(dr["amountCrePage"].ToString());

                        if (dr["vatDebPage"].ToString() != "")
                            pagemodel.vatDebPage = Decimal.Parse(dr["vatDebPage"].ToString());

                        if (dr["vatCrePage"].ToString() != "")
                            pagemodel.vatCrePage = Decimal.Parse(dr["vatCrePage"].ToString());


                        pagemodel.statusPage = Boolean.Parse(dr["statusPage"].ToString());

                       // pagemodel           //.Add(model);
                    }
                    return pagemodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
        public AccPageModel GetPageNewID(int idDaily, int number)
        {
            DataTable dataTable = new DataTable();
            dataTable = pageDAO.GetPageNewID(idDaily, number);
            AccPageModel pagemodel = new AccPageModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    //   AccPageModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        pagemodel = new AccPageModel();

                        pagemodel.idAccPage = Int32.Parse(dr["idAccPage"].ToString());

                        if (dr["codeDaily"].ToString() != "")
                            pagemodel.codeDaily = dr["codeDaily"].ToString();

                        if (dr["periodPage"].ToString() != "")
                            pagemodel.periodPage = Int32.Parse(dr["periodPage"].ToString());

                        if (dr["numberPage"].ToString() != "")
                            pagemodel.numberPage = Int32.Parse(dr["numberPage"].ToString());

                        if (dr["descPage"].ToString() != "")
                            pagemodel.descPage = dr["descPage"].ToString();

                        if (dr["prevDebAmtPage"].ToString() != "")
                            pagemodel.prevDebAmtPage = Decimal.Parse(dr["prevDebAmtPage"].ToString());

                        if (dr["prevCreAmtPage"].ToString() != "")
                            pagemodel.prevCreAmtPage = Decimal.Parse(dr["prevCreAmtPage"].ToString());

                        if (dr["prevDVatPage"].ToString() != "")
                            pagemodel.prevDVatPage = Decimal.Parse(dr["prevDVatPage"].ToString());

                        if (dr["prevCVatPage"].ToString() != "")
                            pagemodel.prevCVatPage = Decimal.Parse(dr["prevCVatPage"].ToString());

                        if (dr["amountDebPage"].ToString() != "")
                            pagemodel.amountDebPage = Decimal.Parse(dr["amountDebPage"].ToString());

                        if (dr["amountCrePage"].ToString() != "")
                            pagemodel.amountCrePage = Decimal.Parse(dr["amountCrePage"].ToString());

                        if (dr["vatDebPage"].ToString() != "")
                            pagemodel.vatDebPage = Decimal.Parse(dr["vatDebPage"].ToString());

                        if (dr["vatCrePage"].ToString() != "")
                            pagemodel.vatCrePage = Decimal.Parse(dr["vatCrePage"].ToString());


                        pagemodel.statusPage = Boolean.Parse(dr["statusPage"].ToString());

                        // pagemodel           //.Add(model);
                    }
                    return pagemodel;

                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}