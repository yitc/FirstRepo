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
    public class AccIbanBUS
    {
        private AccIbanDAO ibanDAO;

        public AccIbanBUS()
        {
            ibanDAO = new AccIbanDAO();
        }

        public int Save(AccIbanModel model, string nameForm, int idUser)
        {
            int retval = 0;
            try
            {

                retval = ibanDAO.Save(model,nameForm, idUser);

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

                retval = ibanDAO.Delete(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public List<AccIbanModel> GetIBANForPerson(int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = ibanDAO.GetIBANForPerson(idContPers);
            List<AccIbanModel> lista = new List<AccIbanModel>();

            if (dataTable != null)
            {

                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    AccIbanModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccIbanModel();

                        if (dr["Id"].ToString() != "")
                            model.Id = Int32.Parse(dr["Id"].ToString());

                        model.accNumber = dr["accNumber"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Int32.Parse(dr["idClient"].ToString());

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        model.ibanNumber = dr["ibanNumber"].ToString();

                        lista.Add(model);
                    }
                    return lista;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<AccIbanModel> GetIBANForClient(int idClient)
        {
            DataTable dataTable = new DataTable();
            dataTable = ibanDAO.GetIBANForClient(idClient);
            List<AccIbanModel> lista = new List<AccIbanModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    AccIbanModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccIbanModel();

                        model.Id = Int32.Parse(dr["id"].ToString());
                        model.accNumber = dr["accNumber"].ToString();
                        model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["idContPers"].ToString()!="")
                        model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.ibanNumber = dr["ibanNumber"].ToString();

                        lista.Add(model);
                    }
                    return lista;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<IModel> GetIBANForClientString(string idClient)
        {
            DataTable dataTable = new DataTable();
            dataTable = ibanDAO.GetIBANForClientString(idClient);
            List<IModel> lista = new List<IModel>();

            if (dataTable != null)
            {
               if (dataTable.Rows.Count > 0)
                {
                    AccIbanModel model = null; 
                    
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccIbanModel();

                        model.Id = Int32.Parse(dr["id"].ToString());
                        model.accNumber = dr["accNumber"].ToString();
                        model.idClient = Int32.Parse(dr["idClient"].ToString());
                        if (dr["idContPers"].ToString() != "")
                           model.idContPers = Int32.Parse(dr["idContPers"].ToString());
                        model.ibanNumber = dr["ibanNumber"].ToString();

                        lista.Add(model);
                    }
                    return lista;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<AccIbanModel> CheckIbanForClient(string iban, int idClient)
        {
            DataTable dataTable = new DataTable();
            dataTable = ibanDAO.CheckIbanForClient(iban,idClient);
            List<AccIbanModel> lista = new List<AccIbanModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    AccIbanModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccIbanModel();                        
                        model.ibanNumber = dr["ibanNumber"].ToString();

                        lista.Add(model);
                    }
                    return lista;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<AccIbanModel> CheckIbanForPerson(string iban, int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = ibanDAO.CheckIbanForPerson(iban, idContPers);
            List<AccIbanModel> lista = new List<AccIbanModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();
                    AccIbanModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccIbanModel();
                        model.ibanNumber = dr["ibanNumber"].ToString();

                        lista.Add(model);
                    }
                    return lista;
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}
