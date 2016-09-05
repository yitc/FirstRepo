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
    public class ClientEmailBUS
    {
        private ClientEmailDAO clientEmailDAO;

        public ClientEmailBUS()
        {
            clientEmailDAO = new ClientEmailDAO();
        }

        public bool Save(ClientEmailModel clientEmail, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = clientEmailDAO.Save(clientEmail, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Delete(int idEmail, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = clientEmailDAO.Delete(idEmail, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(ClientEmailModel clientEmail, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = clientEmailDAO.Update(clientEmail, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<ClientEmailModel> GetClientEmails(int idClient)
        {
            DataTable dataTable = new DataTable();
            dataTable = clientEmailDAO.GetClientEmails(idClient);
            List<ClientEmailModel> clientsEmails = new List<ClientEmailModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ClientEmailModel model = new ClientEmailModel();

                        model.idEmail = Int32.Parse(dr["idEmail"].ToString());
                        model.idClient = Int32.Parse(dr["idClient"].ToString());
                        model.email = dr["email"].ToString();
                        model.isCommunication = Boolean.Parse(dr["isCommunication"].ToString());
                        model.isNewsletters = Boolean.Parse(dr["isNewsletters"].ToString());
                        model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());
                        model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());
                        if (dr["idEmailType"].ToString() != "")
                            model.idEmailType = Int32.Parse(dr["idEmailType"].ToString());
                        clientsEmails.Add(model);
                    }
                    return clientsEmails;
                }
                else
                    return clientsEmails;
            }
            else
                return clientsEmails;
        }

        public List<ClientEmailModel> GetClientEmailsByType(int idEmailType, int idClient)
        {
            DataTable dataTable = new DataTable();
            dataTable = clientEmailDAO.GetClientEmailsByType(idEmailType, idClient);
            List<ClientEmailModel> clientsEmails = new List<ClientEmailModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ClientEmailModel model = new ClientEmailModel();

                        model.idEmail = Int32.Parse(dr["idEmail"].ToString());
                        model.idClient = Int32.Parse(dr["idClient"].ToString());
                        model.email = dr["email"].ToString();
                        model.isCommunication = Boolean.Parse(dr["isCommunication"].ToString());
                        model.isNewsletters = Boolean.Parse(dr["isNewsletters"].ToString());
                        model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());
                        model.isInvoicing = Boolean.Parse(dr["isInvoicing"].ToString());
                        if (dr["idEmailType"].ToString() != "")
                            model.idEmailType = Int32.Parse(dr["idEmailType"].ToString());
                        clientsEmails.Add(model);
                    }
                    return clientsEmails;
                }
                else
                    return clientsEmails;
            }
            else
                return clientsEmails;
        }

        public DataTable GetClientEmailsTable(int idClient)
        {
            DataTable dataTable = new DataTable();
            dataTable = clientEmailDAO.GetClientEmails(idClient);

            return dataTable;

        }

    }
}

