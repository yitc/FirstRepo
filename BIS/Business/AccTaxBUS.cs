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
    public class AccTaxBUS
    {
        private AccTaxDAO taxDAO;

        public AccTaxBUS()
        {
            taxDAO = new AccTaxDAO();
        }

        public bool Save(AccTaxModel tax, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = taxDAO.Save(tax, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(AccTaxModel tax, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = taxDAO.Update(tax, nameForm, idUser);

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

                retval = taxDAO.Delete(id,nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<IModel> GetAllTax(string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = taxDAO.GetAllTax(idLang);
            List<IModel> tax = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    AccTaxModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccTaxModel();
                        model.idTax = Int32.Parse(dr["idTax"].ToString());
                        model.codeTax = dr["codeTax"].ToString();
                        model.descTax = dr["descTax"].ToString();
                        if (dr["typeTax"].ToString() != "")
                           model.typeTax = Decimal.Parse(dr["typeTax"].ToString());
                        if (dr["nameTax"].ToString() != "")
                                 model.nameTax = dr["nameTax"].ToString();
                        if (dr["numberLedAccount"].ToString() != "")
                            model.numberLedAccount = dr["numberLedAccount"].ToString();
                        if (dr["nameAccount"].ToString() != "")
                            model.nameAccount = dr["nameAccount"].ToString();

                        tax.Add(model);
                    }
                    return tax;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public AccTaxModel GetTaxByID(int idTax)
        {
            DataTable dataTable = new DataTable();
            dataTable = taxDAO.GetTaxByID(idTax);
            AccTaxModel tax = new AccTaxModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                  //  AccTaxModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        //model = new AccTaxModel();
                        tax.idTax = Int32.Parse(dr["idTax"].ToString());
                        tax.codeTax = dr["codeTax"].ToString();
                        tax.descTax = dr["descTax"].ToString();
                        if (dr["typeTax"].ToString() != "")
                            tax.typeTax = Decimal.Parse(dr["typeTax"].ToString());
                        if (dr["numberLedAccount"].ToString() != "")
                            tax.numberLedAccount = dr["numberLedAccount"].ToString();
                        //cost.Add(model);
                    }
                    return tax;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public AccTaxModel GetTaxByCode(string idTax)
        {
            DataTable dataTable = new DataTable();
            dataTable = taxDAO.GetTaxByCode(idTax);
            AccTaxModel tax = new AccTaxModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    //  AccTaxModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        //model = new AccTaxModel();
                        tax.idTax = Int32.Parse(dr["idTax"].ToString());
                        tax.codeTax = dr["codeTax"].ToString();
                        tax.descTax = dr["descTax"].ToString();
                        if (dr["typeTax"].ToString() != "")
                            tax.typeTax = Decimal.Parse(dr["typeTax"].ToString());
                        if (dr["numberLedAccount"].ToString() != "")
                            tax.numberLedAccount = dr["numberLedAccount"].ToString();
                        //cost.Add(model);
                    }
                    return tax;
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}
