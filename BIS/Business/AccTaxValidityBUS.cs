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
    public class AccTaxValidityBUS
    {
        private AccTaxValidityDAO taxValidityDAO;

        public AccTaxValidityBUS()
        {
            taxValidityDAO = new AccTaxValidityDAO();
        }

        public bool Save(AccTaxValidityModel taxValid, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = taxValidityDAO.Save(taxValid, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Delete(int idTaxValidity, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = taxValidityDAO.Delete(idTaxValidity, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(AccTaxValidityModel taxValid, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = taxValidityDAO.Update(taxValid, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        // public List<AccTaxValidityModel> GetTaxValidity(string codeTax, DateTime start)
            public List<AccTaxValidityModel> GetTaxValidity(string codeTax)
        {
            DataTable dataTable = new DataTable();
            dataTable = taxValidityDAO.GetTaxValidity(codeTax);
            List<AccTaxValidityModel> taxValid = new List<AccTaxValidityModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        AccTaxValidityModel model = new AccTaxValidityModel();

                        model.idTaxValidity = Int32.Parse(dr["idTaxValidity"].ToString());
                        model.codeTax = dr["codeTax"].ToString();
                        model.startDate = DateTime.Parse(dr["startDate"].ToString());
                        model.percentTax = Decimal.Parse(dr["percentTax"].ToString());
                        //if (dr["endDate"].ToString() != "")
                           model.endDate = DateTime.Parse(dr["endDate"].ToString());
                       
                        //if (dr["idEmailType"].ToString() != "")
                        //    model.idEmailType = Int32.Parse(dr["idEmailType"].ToString());
                        taxValid.Add(model);
                    }
                    return taxValid;
                }
                else
                    return taxValid;
            }
            else
                return taxValid;
        }
            public AccTaxValidityModel GetTaxValidityNew(string codeTax)
            {
                DataTable dataTable = new DataTable();
                dataTable = taxValidityDAO.GetTaxValidityNew(codeTax);
                AccTaxValidityModel taxValid = new AccTaxValidityModel();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                           // AccTaxValidityModel model = new AccTaxValidityModel();

                            taxValid.idTaxValidity = Int32.Parse(dr["idTaxValidity"].ToString());
                            taxValid.codeTax = dr["codeTax"].ToString();
                            taxValid.startDate = DateTime.Parse(dr["startDate"].ToString());
                            taxValid.percentTax = Decimal.Parse(dr["percentTax"].ToString());
                            //if (dr["endDate"].ToString() != "")
                            taxValid.endDate = DateTime.Parse(dr["endDate"].ToString());

                            //if (dr["idEmailType"].ToString() != "")
                            //    model.idEmailType = Int32.Parse(dr["idEmailType"].ToString());
                          //  taxValid.Add(model);
                        }
                        return taxValid;
                    }
                    else
                        return taxValid;
                }
                else
                    return taxValid;
            }
            public AccTaxValidityModel GetTaxValidityById(int idTaxValidity)
            {
                DataTable dataTable = new DataTable();
                dataTable = taxValidityDAO.GetTaxValidityById(idTaxValidity);
               AccTaxValidityModel taxValid = new AccTaxValidityModel();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                        //    AccTaxValidityModel model = new AccTaxValidityModel();

                            taxValid.idTaxValidity = Int32.Parse(dr["idTaxValidity"].ToString());
                            taxValid.codeTax = dr["codeTax"].ToString();
                            taxValid.startDate = DateTime.Parse(dr["startDate"].ToString());
                            taxValid.percentTax = Decimal.Parse(dr["percentTax"].ToString());
                            //if (dr["endDate"].ToString() != "")
                            taxValid.endDate = DateTime.Parse(dr["endDate"].ToString());

                            //if (dr["idEmailType"].ToString() != "")
                            //    model.idEmailType = Int32.Parse(dr["idEmailType"].ToString());
                           // taxValid.Add(model);
                        }
                        return taxValid;
                    }
                    else
                        return taxValid;
                }
                else
                    return taxValid;
            }

            public AccTaxValidityModel GetTaxValidityByCode(string codeTax)
            {
                DataTable dataTable = new DataTable();
                dataTable = taxValidityDAO.GetTaxValidityByCode(codeTax);
                AccTaxValidityModel taxValid = new AccTaxValidityModel();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            //    AccTaxValidityModel model = new AccTaxValidityModel();

                            taxValid.idTaxValidity = Int32.Parse(dr["idTaxValidity"].ToString());
                            taxValid.codeTax = dr["codeTax"].ToString();
                            taxValid.startDate = DateTime.Parse(dr["startDate"].ToString());
                            taxValid.percentTax = Decimal.Parse(dr["percentTax"].ToString());
                            //if (dr["endDate"].ToString() != "")
                            taxValid.endDate = DateTime.Parse(dr["endDate"].ToString());

                            //if (dr["idEmailType"].ToString() != "")
                            //    model.idEmailType = Int32.Parse(dr["idEmailType"].ToString());
                            // taxValid.Add(model);
                        }
                        return taxValid;
                    }
                    else
                        return taxValid;
                }
                else
                    return taxValid;
            }
    }
}

