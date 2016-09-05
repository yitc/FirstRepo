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
    public class CountryBUS
    {
        private CountryDAO countryDAO;

        public CountryBUS()
        {
            countryDAO = new CountryDAO();
        }

        public bool Save(CountryModel countries, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = countryDAO.Save(countries, nameForm, idUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }
        public bool Update(CountryModel countries, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = countryDAO.Update(countries, nameForm, idUser);
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
                retval = countryDAO.Delete(id, nameForm, idUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public List<IModel> GetCountries()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = countryDAO.GetCountries();
                List<IModel> countries = new List<IModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            CountryModel model = new CountryModel();

                            model.idCountry = Int32.Parse(dr["idCountry"].ToString());
                            model.interNationalCode = dr["interNationalCode"].ToString();
                            model.nameCountry = dr["nameCountry"].ToString();
                            model.nacionality = dr["nacionality"].ToString();
                            model.provision = dr["provision"].ToString();
                            model.premie = dr["premie"].ToString();

                            countries.Add(model);
                        }
                        return countries;
                    }
                    else
                        return countries;
                }
                else
                    return countries;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CountryModel> GetCountriesWithCountryModel()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = countryDAO.GetCountries();
                List<CountryModel> countries = new List<CountryModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            CountryModel model = new CountryModel();

                            model.idCountry = Int32.Parse(dr["idCountry"].ToString());
                            model.interNationalCode = dr["interNationalCode"].ToString();
                            model.nameCountry = dr["nameCountry"].ToString();
                            model.nacionality = dr["nacionality"].ToString();
                            model.provision = dr["provision"].ToString();
                            model.premie = dr["premie"].ToString();

                            countries.Add(model);
                        }
                        return countries;
                    }
                    else
                        return countries;
                }
                else
                    return countries;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CountryModel GetCountryByID(int idCountry)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = countryDAO.GetCountryByID(idCountry);
                //List<IModel> countries = new List<IModel>();
                CountryModel model = new CountryModel();
                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        model = null;
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            model = new CountryModel();

                            model.idCountry = Int32.Parse(dr["idCountry"].ToString());
                            model.interNationalCode = dr["interNationalCode"].ToString();
                            model.nameCountry = dr["nameCountry"].ToString();
                            model.nacionality = dr["nacionality"].ToString();
                            model.provision = dr["provision"].ToString();
                            model.premie = dr["premie"].ToString();

                          //  countries.Add(model);
                        }
                        return model;
                    }
                    else
                        return model;
                }
                else
                    return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public CountryModel GetCountryByCodeOrName(string codeNameCountry)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = countryDAO.GetCountryByCodeOrName(codeNameCountry);
                //List<IModel> countries = new List<IModel>();
                CountryModel model = new CountryModel();
                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        model = null;
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            model = new CountryModel();

                            model.idCountry = Int32.Parse(dr["idCountry"].ToString());
                            model.interNationalCode = dr["interNationalCode"].ToString();
                            model.nameCountry = dr["nameCountry"].ToString();
                            model.nacionality = dr["nacionality"].ToString();
                            model.provision = dr["provision"].ToString();
                            model.premie = dr["premie"].ToString();

                            //  countries.Add(model);
                        }
                        return model;
                    }
                    else
                        return model;
                }
                else
                    return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CountryModel> IsIn(int idCountry)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = countryDAO.IsIn(idCountry);
                List<CountryModel> countries = new List<CountryModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            CountryModel model = new CountryModel();

                            model.idCountry = Int32.Parse(dr["idCountry"].ToString());

                            countries.Add(model);
                        }
                        return countries;
                    }
                    else
                        return countries;
                }
                else
                    return countries;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int checkIsInArrrangement(int idCountry)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = countryDAO.checkIsInArrrangement(idCountry);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }
        public int checkIsInEmployees(int idCountry)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = countryDAO.checkIsInEmployees(idCountry);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }
        public int checkIsInCompany(int idCountry)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = countryDAO.checkIsInCompany(idCountry);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }
        public int checkIsInContactPersonPassport(int idCountry)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = countryDAO.checkIsInContactPersonPassport(idCountry);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }
        public int checkIsInProvinces(int idCountry)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = countryDAO.checkIsInProvinces(idCountry);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }
        public int checkIsInArrangementCalculationFirstNotArticles(int idCountry)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = countryDAO.checkIsInArrangementCalculationFirstNotArticles(idCountry);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }
        public bool DeleteCountrySript(int idCountry)
        {
            bool retval = false;
            try
            {

                retval = countryDAO.DeleteCountryScript(idCountry);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
     
    }
}
