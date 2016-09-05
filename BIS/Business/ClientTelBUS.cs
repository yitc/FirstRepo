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
    public class ClientTelBUS
    {
        private ClientTelDAO clientTelDAO;

        public ClientTelBUS()
        {
            clientTelDAO = new ClientTelDAO();
        }

        public bool Save(ClientTelModel ctelm, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = clientTelDAO.Save(ctelm,  nameForm,  idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Delete(int idTel, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = clientTelDAO.Delete(idTel, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(ClientTelModel ctelm, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = clientTelDAO.Update(ctelm, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public List<ClientTelModel> GetAllClientTels(Int32 idClient)
        {
            DataTable dataTable = new DataTable();
            dataTable = clientTelDAO.GetAllClientTels(idClient);
            List<ClientTelModel> clientsTel = new List<ClientTelModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ClientTelModel model = new ClientTelModel();

                        model.idTel = Int32.Parse(dr["idTel"].ToString());
                        model.idClient = Int32.Parse(dr["idClient"].ToString());
                        model.numberTel = dr["numberTel"].ToString();
                        model.idDefaultTel = Convert.ToBoolean(dr["idDefaultTel"].ToString());
                        model.descriptionTel = dr["descriptionTel"].ToString();
                        if (dr["idTelType"].ToString() != "")
                            model.idTelType = Int32.Parse(dr["idTelType"].ToString());
                        if (dr["nameTelType"].ToString() != "")
                            model.nameTelType = dr["nameTelType"].ToString();

                        clientsTel.Add(model);
                    }
                    return clientsTel;
                }
                else
                    return clientsTel;
            }
            else
                return clientsTel;
        }

        public List<ClientTelModel> GetAllClientTelsByType(int idTelType, int idclient)
        {
            DataTable dataTable = new DataTable();
            dataTable = clientTelDAO.GetClientTelsByType(idTelType, idclient);
            List<ClientTelModel> clientsTel = new List<ClientTelModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ClientTelModel model = new ClientTelModel();

                        model.idTel = Int32.Parse(dr["idTel"].ToString());
                        model.idClient = Int32.Parse(dr["idClient"].ToString());
                        model.numberTel = dr["numberTel"].ToString();
                        model.idDefaultTel = Convert.ToBoolean(dr["idDefaultTel"].ToString());
                        model.descriptionTel = dr["descriptionTel"].ToString();
                        if (dr["idTelType"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idTelType"].ToString());
                        if (dr["nameTelType"].ToString() != "")
                            model.nameTelType = dr["nameTelType"].ToString();

                        clientsTel.Add(model);
                    }
                    return clientsTel;
                }
                else
                    return clientsTel;
            }
            else
                return clientsTel;
        }

        public List<ClientTelModel> GetAllClientFaxes(Int32 idClient)
        {
            DataTable dataTable = new DataTable();
            dataTable = clientTelDAO.GetAllClientFaxes(idClient);
            List<ClientTelModel> clientsTel = new List<ClientTelModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ClientTelModel model = new ClientTelModel();

                        model.idTel = Int32.Parse(dr["idTel"].ToString());
                        model.idClient = Int32.Parse(dr["idClient"].ToString());
                        model.numberTel = dr["numberTel"].ToString();
                        model.idDefaultTel = Convert.ToBoolean(dr["idDefaultTel"].ToString());
                        model.descriptionTel = dr["descriptionTel"].ToString();
                        if (dr["idTelType"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idTelType"].ToString());
                        if (dr["nameTelType"].ToString() != "")
                            model.nameTelType = dr["nameTelType"].ToString();

                        clientsTel.Add(model);
                    }
                    return clientsTel;
                }
                else
                    return clientsTel;
            }
            else
                return clientsTel;
        }
    }
}
