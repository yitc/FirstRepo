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
    public class ClientBUS
    {
        private ClientDAO clientDAO;



        public int Save(ClientModel client, string nameForm, int idUser)
        {
            int retval = -1;
            try
            {

                retval = clientDAO.Save(client,nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool Update(ClientModel client, int idClient, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = clientDAO.Update(client, idClient, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateContactPersonName(int idClient, string contactpersonname, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = clientDAO.UpdateContactPersonName(idClient, contactpersonname, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public ClientBUS()
        {
            clientDAO = new ClientDAO();
        }

        public List<IModel> GetAllClients(string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = clientDAO.GetAllClients(idLang);
            List<IModel> clients = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ClientModel model = new ClientModel();

                        model.idClient = Int32.Parse(dr["idClient"].ToString());
                        model.accountCodeClient = dr["accountCodeClient"].ToString();
                        model.nameClient = dr["nameClient"].ToString();
                        model.contactPersonName = dr["contactPersonName"].ToString();
                        //model.addressClient = dr["addressClient"].ToString();
                        //model.zipCodeClient = dr["zipCodeClient"].ToString();
                        //model.cityClient = dr["cityClient"].ToString();

                        //if (dr["countryClient"].ToString() != "")
                        //    model.countryClient = Int32.Parse(dr["countryClient"].ToString());

                        //model.visitAddressClient = dr["visitAddressClient"].ToString();

                        //model.visitZipCodeClient = dr["visitZipCodeClient"].ToString();

                        //model.visitCityClient = dr["visitCityClient"].ToString();                        

                        //model.emailClient = dr["emailClient"].ToString();

                        model.webClient = dr["webClient"].ToString();

                        if (dr["idTypeClient"].ToString() != "")
                            model.idTypeClient = Int32.Parse(dr["idTypeClient"].ToString());

                        if (dr["nameTypeClient"].ToString() != "")
                            model.nameTypeClient = dr["nameTypeClient"].ToString();


                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["nameUserModified"].ToString() != "")
                            model.nameUserModified = dr["nameUserModified"].ToString();

                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();


                        model.isActiveClient = Boolean.Parse(dr["isActiveClient"].ToString());

                        clients.Add(model);
                    }
                    return clients;
                }
                else
                    return clients;
            }
            else
                return clients;
        }

        public string GetContactPersonName(int idClient)
        {
            DataTable dataTable = new DataTable();
            dataTable = clientDAO.GetContactPersonName(idClient);
            string name = "";
           
            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    DataRow dr = dataTable.Rows[0];
                    name = dr["contactPersonName"].ToString();                                     
                }               
            }

            return name;
            
        }

        public ClientModel GetClient(Int32 idClient)
        {
            DataTable dataTable = new DataTable();
            dataTable = clientDAO.GetClient(idClient);
            ClientModel clients = new ClientModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ClientModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ClientModel();

                        model.idClient = Int32.Parse(dr["idClient"].ToString());
                        model.accountCodeClient = dr["accountCodeClient"].ToString();
                        model.nameClient = dr["nameClient"].ToString();
                        model.contactPersonName = dr["contactPersonName"].ToString();
                        //model.addressClient = dr["addressClient"].ToString();
                        //model.zipCodeClient = dr["zipCodeClient"].ToString();
                        //model.cityClient = dr["cityClient"].ToString();

                        //if (dr["countryClient"].ToString() != "")
                        //    model.countryClient = Int32.Parse(dr["countryClient"].ToString());

                        //model.visitAddressClient = dr["visitAddressClient"].ToString();

                        //model.visitZipCodeClient = dr["visitZipCodeClient"].ToString();

                        //model.visitCityClient = dr["visitCityClient"].ToString();

                        //model.emailClient = dr["emailClient"].ToString();

                        model.webClient = dr["webClient"].ToString();

                        if (dr["idTypeClient"].ToString() != "")
                            model.idTypeClient = Int32.Parse(dr["idTypeClient"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["nameUserModified"].ToString() != "")
                            model.nameUserModified = dr["nameUserModified"].ToString();

                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();

                        model.isActiveClient = Boolean.Parse(dr["isActiveClient"].ToString());
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<IModel> GetClientDebitor(int side)
        {
            DataTable dataTable = new DataTable();
            dataTable = clientDAO.GetClientDebitor(side);
            List<IModel> clients = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ClientModel model = new ClientModel();

                        model.idClient = Int32.Parse(dr["idClient"].ToString());
                        model.accountCodeClient = dr["accountCodeClient"].ToString();
                        model.nameClient = dr["nameClient"].ToString();
                        model.contactPersonName = dr["contactPersonName"].ToString();
                        model.webClient = dr["webClient"].ToString();

                        if (dr["idTypeClient"].ToString() != "")
                            model.idTypeClient = Int32.Parse(dr["idTypeClient"].ToString());

                        model.isActiveClient = Boolean.Parse(dr["isActiveClient"].ToString());

                        clients.Add(model);
                    }
                    return clients;
                }
                else
                    return clients;
            }
            else
                return clients;
        }
        public List<IModel> GetClientCreditor(int side)
        {
            DataTable dataTable = new DataTable();
            dataTable = clientDAO.GetClientCreditor(side);
            List<IModel> clients = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ClientModel model = new ClientModel();

                        model.idClient = Int32.Parse(dr["idClient"].ToString());
                        model.accountCodeClient = dr["accountCodeClient"].ToString();
                        model.nameClient = dr["nameClient"].ToString();
                        model.contactPersonName = dr["contactPersonName"].ToString();
                        model.webClient = dr["webClient"].ToString();

                        if (dr["idTypeClient"].ToString() != "")
                            model.idTypeClient = Int32.Parse(dr["idTypeClient"].ToString());

                        model.isActiveClient = Boolean.Parse(dr["isActiveClient"].ToString());

                        clients.Add(model);
                    }
                    return clients;
                }
                else
                    return clients;
            }
            else
                return clients;
        }
        public List<IModel> GetClientByContract(Int32 idContract)
        {
            DataTable dataTable = new DataTable();
            dataTable = clientDAO.GetClientByContract(idContract);
            List<IModel> clients = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ClientModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ClientModel();

                        model.idClient = Int32.Parse(dr["idClient"].ToString());
                        model.accountCodeClient = dr["accountCodeClient"].ToString();
                        model.nameClient = dr["nameClient"].ToString();
                        model.contactPersonName = dr["contactPersonName"].ToString();
                        //model.addressClient = dr["addressClient"].ToString();
                        //model.zipCodeClient = dr["zipCodeClient"].ToString();
                        //model.cityClient = dr["cityClient"].ToString();

                        //if (dr["countryClient"].ToString() != "")
                        //    model.countryClient = Int32.Parse(dr["countryClient"].ToString());

                        //model.visitAddressClient = dr["visitAddressClient"].ToString();

                        //model.visitZipCodeClient = dr["visitZipCodeClient"].ToString();

                        //model.visitCityClient = dr["visitCityClient"].ToString();

                        //model.emailClient = dr["emailClient"].ToString();

                        model.webClient = dr["webClient"].ToString();

                        if (dr["idTypeClient"].ToString() != "")
                            model.idTypeClient = Int32.Parse(dr["idTypeClient"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["nameUserModified"].ToString() != "")
                            model.nameUserModified = dr["nameUserModified"].ToString();

                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();

                        model.isActiveClient = Boolean.Parse(dr["isActiveClient"].ToString());

                        clients.Add(model);
                    }
                    return clients;
                }
                else
                    return null;
            }
            else
                return null;
        }

        //===========  delete client ===========================================================



        public int checkIsInInvoice(int idClient)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = clientDAO.checkIsInInvoice(idClient);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }
        public int checkIsInMeetings(int idClient)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = clientDAO.checkIsInMeetings(idClient);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }
        public int checkIsInMultimedia(int idClient)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = clientDAO.checkIsInMultimedia(idClient);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }
        public int checkIsInArrangementPrice(int idClient)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = clientDAO.checkIsInArrangementPrice(idClient);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }
        public int checkIsInArrangementBookArticles(int idClient)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = clientDAO.checkIsInArrangementBookArticles(idClient);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }

        public bool DeleteClient(int idClient, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = clientDAO.DeleteClient(idClient, nameForm, idUser );

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }


    }
}
