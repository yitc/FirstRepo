using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.DAO;
using BIS.Model;
using System;
using System.Collections.Generic;

namespace BIS.Business
{
   public class ClientPersonBUS
    {
         private ClientPersonDAO clientPersonDAO;

        public ClientPersonBUS()
        {
            clientPersonDAO = new ClientPersonDAO();
        }


        public int SaveAndReturnID(ClientPersonModel model, string nameForm, int idUser)
        {
            int retval = 0;
            try
            {
                retval = clientPersonDAO.SaveAndReturnID(model, nameForm, idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool Update(ClientPersonModel clientPerson, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = clientPersonDAO.Update(clientPerson, nameForm, idUser);
            }
            catch(Exception ex)
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
                retval = clientPersonDAO.Delete(id, nameForm, idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public List<IModel> GetAllClientPerson()
        {
            List<IModel> compList = new List<IModel>();

            DataTable dataTable = new DataTable();
            dataTable = clientPersonDAO.GetAllClientPerson();

            if(dataTable != null)
            {
                foreach(DataRow dr in dataTable.Rows)
                {
                    ClientPersonModel model = new ClientPersonModel();

                    model.idCliPer = Int32.Parse(dr["idCliPer"].ToString());

                    if(dr["idClient"].ToString() != "")
                    {
                        model.idCLient = Int32.Parse(dr["idClient"].ToString());
                    }

                    if(dr["idContPerson"].ToString() != "")
                    {
                        model.idContPerson =Int32.Parse(dr["idContPerson"].ToString());
                    }

                    if(dr["idFunction"].ToString() != "")
                    {
                        model.idFunction = Int32.Parse(dr["idFunction"].ToString());
                    }

                    compList.Add(model);
                }

                return compList;
            }
            else
            {
                return null;
            }
        }

       public List<ClientPersonModel>GetAllClientsFromPerson(int idContPerson)
        {
            DataTable dataTable = new DataTable();
            dataTable = clientPersonDAO.GetAllClientsFromPerson(idContPerson);
            List<ClientPersonModel> clientPersonList = new List<ClientPersonModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ClientPersonModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ClientPersonModel();                        
                        
                        model.idCliPer = Int32.Parse(dr["idCliPer"].ToString());
                        

                        if (dr["idCLient"].ToString() != "")
                        {
                            model.idCLient = Int32.Parse(dr["idCLient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["idFunction"].ToString() != "")
                        {
                            model.idFunction = Int32.Parse(dr["idFunction"].ToString());
                        }
                        model.nameClient = dr["nameClient"].ToString();
                        model.nameFunction = dr["nameFunction"].ToString();

                        clientPersonList.Add(model);
                    }
                    return clientPersonList;
                }
                else
                    return null;
            }
            else
                return null;
        }

       public List<ClientPersonModel>GetAllPersonsFromClient(int idClient)//za formu Client
       {
           DataTable dataTable = new DataTable();
           dataTable = clientPersonDAO.GetAllPersonsFromClient(idClient);
           List<ClientPersonModel> clientPersonList = new List<ClientPersonModel>();

           if (dataTable != null)
           {
               if (dataTable.Rows.Count > 0)
               {
                   ClientPersonModel model = null;
                   foreach (DataRow dr in dataTable.Rows)
                   {
                       model = new ClientPersonModel();

                       if (dr["idCliPer"].ToString() != "")
                       {
                           model.idCliPer = Int32.Parse(dr["idCliPer"].ToString());
                       }

                       if (dr["idCLient"].ToString() != "")
                       {
                           model.idCLient = Int32.Parse(dr["idCLient"].ToString());
                       }

                       if (dr["idContPerson"].ToString() != "")
                       {
                           model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                       }

                       if (dr["idFunction"].ToString() != "")
                       {
                           model.idFunction = Int32.Parse(dr["idFunction"].ToString());
                       }

                       model.nameClient = dr["nameClient"].ToString();
                       model.nameFunction = dr["nameFunction"].ToString();
                       model.firstname = dr["firstname"].ToString();
                       model.lastname = dr["lastname"].ToString();
                       model.midname = dr["midname"].ToString();

                       clientPersonList.Add(model);
                   }
                   return clientPersonList;
               }
               else
                   return null;
           }
           else
               return null;
       }

        public List<ClientPersonModel> GetAllClientPersonList()
        {
            List<ClientPersonModel> compList = new List<ClientPersonModel>();

            DataTable dataTable = new DataTable();
            dataTable = clientPersonDAO.GetAllClientPerson();

            if(dataTable != null)
            {
                foreach(DataRow dr in dataTable.Rows)
                {
                    ClientPersonModel model = new ClientPersonModel();

                    model.idCliPer = Int32.Parse(dr["idCliPer"].ToString());

                    if(dr["idClient"].ToString() != "")
                    {
                        model.idCLient = Int32.Parse(dr["idClient"].ToString());
                    }

                    if(dr["idContPerson"].ToString() != "")
                    {
                        model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                    }

                    if(dr["idFunction"].ToString() != "")
                    {
                        model.idFunction = Int32.Parse(dr["idFunction"].ToString());
                    }

                    compList.Add(model);
                }
                return compList;
            }
            else
            {
                return null;
            }
        }

        public ClientPersonModel GetClientPersonById(int idCliPerson)
        {
            DataTable dataTable = new DataTable();
            dataTable = clientPersonDAO.GetClientPersonById(idCliPerson);
            ClientPersonModel clientPerson = new ClientPersonModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ClientPersonModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ClientPersonModel();

                        model.idCliPer = Int32.Parse(dr["idCliPer"].ToString());

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idCliPer = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["idFunction"].ToString() != "")
                        {
                            model.idFunction = Int32.Parse(dr["idFunction"].ToString());
                        }
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public ClientPersonModel GetContactPersonById(int idClient)
        {
            DataTable dataTable = new DataTable();
            dataTable = clientPersonDAO.GetContactPersonByClient(idClient);
            ClientPersonModel clientPerson = new ClientPersonModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ClientPersonModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model.idCliPer = Int32.Parse(dr["idCliPer"].ToString());

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idCLient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["idFunction"].ToString() != "")
                        {
                            model.idFunction = Int32.Parse(dr["idFunction"].ToString());
                        }
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public ClientPersonModel GetClientById(int idClient)
        {
            DataTable dataTable = new DataTable();
            dataTable = clientPersonDAO.GetClientById(idClient);
            ClientPersonModel clientPerson = new ClientPersonModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ClientPersonModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ClientPersonModel();

                        model.idCliPer = Int32.Parse(dr["idCliPer"].ToString());

                        if (dr["idClient"].ToString() != "")
                        {
                            model.idCLient = Int32.Parse(dr["idClient"].ToString());
                        }

                        if (dr["idContPerson"].ToString() != "")
                        {
                            model.idContPerson = Int32.Parse(dr["idContPerson"].ToString());
                        }

                        if (dr["idFunction"].ToString() != "")
                        {
                            model.idFunction = Int32.Parse(dr["idFunction"].ToString());
                        }

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
