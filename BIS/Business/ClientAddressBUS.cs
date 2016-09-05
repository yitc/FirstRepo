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
    public class ClientAddressBUS
    {
        private ClientAddressDAO clientAddressDAO;

        public ClientAddressBUS()
        {
            clientAddressDAO = new ClientAddressDAO();
        }


        public bool Save(ClientAddressModel address, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = clientAddressDAO.Save(address, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(ClientAddressModel address, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = clientAddressDAO.Update(address, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Delete(int idContPers, int idAdressType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = clientAddressDAO.Delete(idContPers, idAdressType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<ClientAddressModel> GetClientAddresses(int idClient)
        {
            DataTable dataTable = new DataTable();
            dataTable = clientAddressDAO.GetClientAddresses(idClient);
            List<ClientAddressModel> clientsAddress = new List<ClientAddressModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ClientAddressModel model = new ClientAddressModel();
                        model.idClient = Int32.Parse(dr["idClient"].ToString());
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
                        clientsAddress.Add(model);


                    }
                    return clientsAddress;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<ClientAddressModel> GetClientAddressesByType(int idAddressType, int idCLient)
        {
            DataTable dataTable = new DataTable();
            dataTable = clientAddressDAO.GetClientAddressesByType(idAddressType, idCLient);
            List<ClientAddressModel> clientsAddress = new List<ClientAddressModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ClientAddressModel model = new ClientAddressModel();
                        model.idClient = Int32.Parse(dr["idClient"].ToString());
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
                        clientsAddress.Add(model);


                    }
                    return clientsAddress;
                }
                else
                    return null;
            }
            else
                return null;
        }

    }
}
