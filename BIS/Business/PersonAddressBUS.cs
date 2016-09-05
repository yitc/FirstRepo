using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.DAO;
using BIS.Model;
using System;
using System.Collections.Generic;
using BIS.Business;

namespace BIS.Business
{
    public class PersonAddressBUS
    {
        private PersonAddressDAO personAddressDAO;

        public PersonAddressBUS()
        {
            personAddressDAO = new PersonAddressDAO();
        }


        public bool Save(PersonAddressModel address, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = personAddressDAO.Save(address,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(PersonAddressModel address, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = personAddressDAO.Update(address,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Delete(int idPerson, int idAddressType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = personAddressDAO.Delete(idPerson, idAddressType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
       
        public List<PersonAddressModel> GetPersonAddresses(int idPerson)
        {
            DataTable dataTable = new DataTable();
            dataTable = personAddressDAO.GetPersonAddresses(idPerson);
            List<PersonAddressModel> personsAddress = new List<PersonAddressModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PersonAddressModel model = new PersonAddressModel();
                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["idAddressType"].ToString() != "")
                        model.idAddressType = Int32.Parse(dr["idAddressType"].ToString());
                        model.street = dr["street"].ToString();
                        model.housenr = dr["housenr"].ToString();
                        model.extension = dr["extension"].ToString();
                        model.postalCode = dr["postalCode"].ToString();
                        model.city = dr["city"].ToString();
                        model.country = dr["country"].ToString();
                        model.region1 = dr["region1"].ToString();
                        model.region2 = dr["region2"].ToString();
                        model.isInternational = Boolean.Parse(dr["isInternational"].ToString());
                        personsAddress.Add(model);
                              

                    }
                    return personsAddress;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<PersonAddressModel> GetPersonAddressesByType(int idAddresType, int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = personAddressDAO.GetPersonAddressesByType(idAddresType, idContPers);
            List<PersonAddressModel> personsAddress = new List<PersonAddressModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PersonAddressModel model = new PersonAddressModel();
                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["idAddressType"].ToString() != "")
                            model.idAddressType = Int32.Parse(dr["idAddressType"].ToString());
                        model.street = dr["street"].ToString();
                        model.housenr = dr["housenr"].ToString();
                        model.extension = dr["extension"].ToString();
                        model.postalCode = dr["postalCode"].ToString();
                        model.city = dr["city"].ToString();
                        model.country = dr["country"].ToString();
                        model.region1 = dr["region1"].ToString();
                        model.region2 = dr["region2"].ToString();
                        model.isInternational = Boolean.Parse(dr["isInternational"].ToString());
                        personsAddress.Add(model);


                    }
                    return personsAddress;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public PersonAddressModel GetPersonAddressesByTypeOne(int idAddresType, int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = personAddressDAO.GetPersonAddressesByType(idAddresType, idContPers);
            PersonAddressModel model = new PersonAddressModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new PersonAddressModel();
                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        if (dr["idAddressType"].ToString() != "")
                            model.idAddressType = Int32.Parse(dr["idAddressType"].ToString());
                        model.street = dr["street"].ToString();
                        model.housenr = dr["housenr"].ToString();
                        model.extension = dr["extension"].ToString();
                        model.postalCode = dr["postalCode"].ToString();
                        model.city = dr["city"].ToString();
                        model.country = dr["country"].ToString();
                        model.region1 = dr["region1"].ToString();
                        model.region2 = dr["region2"].ToString();
                        model.isInternational = Boolean.Parse(dr["isInternational"].ToString());
                      //  personsAddress.Add(model);


                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<PersonAddressModel> GetPersonCities()
        {
            DataTable dataTable = new DataTable();
            dataTable = personAddressDAO.GetPersonCities();
            List<PersonAddressModel> personsAddress = new List<PersonAddressModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PersonAddressModel model = new PersonAddressModel();
                        model.city = dr["city"].ToString();
                        personsAddress.Add(model);


                    }
                    return personsAddress;
                }
                else
                    return null;
            }
            else
                return null;
        }

    }
}
