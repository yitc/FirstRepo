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
  public class MultimediaServerBUS
    {
       private MultimediaServerDAO multimediaServerDAO;

       public MultimediaServerBUS()
        {
            multimediaServerDAO = new MultimediaServerDAO();
        }


        public List<IModel> GetAllMultimedia()
        {
            DataTable dataTable = new DataTable();
            dataTable = multimediaServerDAO.GetAllMultimediaServer();
            List<IModel> multimedia = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        MultimediaServersModel model = new MultimediaServersModel();

                        if (dr["idServer"].ToString() != "")
                            model.idServer = Convert.ToInt32(dr["idServer"].ToString());


                        if (dr["path"].ToString() != "")
                            model.path = dr["path"].ToString();

                        if (dr["folder"].ToString() != "")
                            model.folder = (dr["folder"].ToString());

                        if (dr["username"].ToString() != "")
                            model.username= dr["username"].ToString();

                        if (dr["password"].ToString() != "")
                            model.password =  (dr["password"].ToString());

                       

                        multimedia.Add(model);
                    }
                    return multimedia;

                }
                else
                    return null;
            }
            else
                return null;
        }
       

        public bool Update(int idServer, string path, string folder, string username, string password)
        {
            bool retval = false;
            try
            {

                retval = multimediaServerDAO.Update(idServer, path, folder,  username, password);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Insert(int idServer, string path, string folder, string username, string password)
        {
            bool retval = false;
            try
            {

                retval = multimediaServerDAO.Insert(idServer, path, folder, username, password);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool Delete(int idServer)
        {
            bool retval = false;
            try
            {

                retval = multimediaServerDAO.Delete(idServer);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public List<MultimediaServersModel> GetLastIdServer()
        {
            DataTable dataTable = new DataTable();
            dataTable = multimediaServerDAO.GetLastIdServer();
            List<MultimediaServersModel> filters = new List<MultimediaServersModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        MultimediaServersModel model = new MultimediaServersModel();

                        if (dr["idServer"].ToString() != "" && dr["idServer"].ToString() != null)
                            model.idServer = Int32.Parse(dr["idServer"].ToString());

                        model.path = dr["path"].ToString();
                        model.folder = dr["folder"].ToString();
                        model.username = dr["username"].ToString();
                        model.password = dr["password"].ToString();

                        filters.Add(model);
                    }
                    return filters;
                }
                else
                    return filters;
            }
            else
                return filters;
        }

        public List<MultimediaServersModel> AllServerId()
        {
            DataTable dataTable = new DataTable();
            dataTable = multimediaServerDAO.AllServerId();
            List<MultimediaServersModel> filters = new List<MultimediaServersModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        MultimediaServersModel model = new MultimediaServersModel();

                        if (dr["idServer"].ToString() != "" && dr["idServer"].ToString() != null)
                            model.idServer = Int32.Parse(dr["idServer"].ToString());

                        model.path = dr["path"].ToString();
                        model.folder = dr["folder"].ToString();
                        model.username = dr["username"].ToString();
                        model.password = dr["password"].ToString();

                        filters.Add(model);
                    }
                    return filters;
                }
                else
                    return filters;
            }
            else
                return filters;
        }

      // NOVO DELETE
        public int checkIsInMultimedia(int idServer)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = multimediaServerDAO.checkIsInMultimedia(idServer);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }

        public bool DeleteMultimediaServerSript(int idServer)
        {
            bool retval = false;
            try
            {

                retval = multimediaServerDAO.DeleteMultimediaServerSript(idServer);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

    }
}
