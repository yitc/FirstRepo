using BIS.DAO;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.Business
{
    public class ClientTypesBUS
    {
         private ClientTypesDAO clientTypesDAO;

        public ClientTypesBUS()
        {
            clientTypesDAO = new ClientTypesDAO();
        }

        public List<ClientTypesModel> GetAllClientsTypes(string lang)
        {
            DataTable dataTable = new DataTable();
            dataTable = clientTypesDAO.GetAllClientsTypes(lang);
            List<ClientTypesModel> clientsTypes = new List<ClientTypesModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ClientTypesModel model = new ClientTypesModel();

                        model.idTypeClient = Int32.Parse(dr["idTypeClient"].ToString());
                        model.nameTypeClient = dr["nameTypeClient"].ToString();

                        clientsTypes.Add(model);
                    }
                    return clientsTypes;
                }
                else
                    return clientsTypes;
            }
            else
                return clientsTypes;
        }
    }
}
