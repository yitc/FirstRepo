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
    public class MultimediaBUS
    {
        private MultimediaDAO multimediaDAO;

        public MultimediaBUS()
        {
            multimediaDAO = new MultimediaDAO();
        }


        public List<IModel> GetAllMultimedia()
        {
            DataTable dataTable = new DataTable();
            dataTable = multimediaDAO.GetAllMultimedias();
            List<IModel> multimedia = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        MultimediaModel model = new MultimediaModel();

                        if (dr["idMultimedia"].ToString() != "")
                            model.idMultimedia = Convert.ToInt32(dr["idMultimedia"].ToString());


                        if (dr["idArticle"].ToString() != "")
                            model.idArticle = dr["idArticle"].ToString();

                        if (dr["idServer"].ToString() != "")
                            model.idServer = Convert.ToInt32(dr["idServer"].ToString());

                        if (dr["nameArtical"].ToString() != "")
                            model.nameArtical= dr["nameArtical"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.idClient =  Convert.ToInt32(dr["idClient"].ToString());

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

                        if (dr["idPeriod"].ToString() != "")
                            model.idPeriod = Convert.ToInt32(dr["idPeriod"].ToString());

                        if (dr["namePeriod"].ToString() != "")
                            model.namePeriod = dr["namePeriod"].ToString();

                        if (dr["description"].ToString() != "")
                            model.description = dr["description"].ToString();

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

        public MultimediaModel GetMultimediaByArticleClientPeriod(string idArticle, int idClient, int idPeriod, int idMultimedia)
        {
            DataTable dataTable = new DataTable();
            dataTable = multimediaDAO.GetMultimediaByArticleClientPeriod(idArticle, idClient, idPeriod, idMultimedia);
            MultimediaModel model = new MultimediaModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {                        
                        if (dr["idMultimedia"].ToString() != "")
                            model.idMultimedia = Convert.ToInt32(dr["idMultimedia"].ToString());

                        if (dr["idArticle"].ToString() != "")
                            model.idArticle = dr["idArticle"].ToString();

                        if (dr["idServer"].ToString() != "")
                            model.idServer = Convert.ToInt32(dr["idServer"].ToString());

                        if (dr["nameArtical"].ToString() != "")
                            model.nameArtical = dr["nameArtical"].ToString();

                        if (dr["idClient"].ToString() != "")
                            model.idClient = Convert.ToInt32(dr["idClient"].ToString());

                        if (dr["nameClient"].ToString() != "")
                            model.nameClient = dr["nameClient"].ToString();

                        if (dr["idPeriod"].ToString() != "")
                            model.idPeriod = Convert.ToInt32(dr["idPeriod"].ToString());

                        if (dr["namePeriod"].ToString() != "")
                            model.namePeriod = dr["namePeriod"].ToString();

                        if (dr["description"].ToString() != "")
                            model.description = dr["description"].ToString();                        
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<IModel> GetAllMultimediaServers()
        {
            DataTable dataTable = new DataTable();
            dataTable = multimediaDAO.GetAllMultimediaServers();
            List<IModel> multimedia = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        MultimediaServerModel model = new MultimediaServerModel();

                        if (dr["idServer"].ToString() != "")
                            model.idServer = Convert.ToInt32(dr["idServer"].ToString());
                        
                        model.path = dr["path"].ToString();
                        model.folder = dr["folder"].ToString();                     
                     
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

        public MultimediaServerModel GetMultimediaServersByID(int idServer)
        {
            DataTable dataTable = new DataTable();
            dataTable = multimediaDAO.GetMultimediaServersByID(idServer);
            MultimediaServerModel model = new MultimediaServerModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {                        
                        if (dr["idServer"].ToString() != "")
                            model.idServer = Convert.ToInt32(dr["idServer"].ToString());

                        model.path = dr["path"].ToString();
                        model.folder = dr["folder"].ToString();                        
                    }
                    return model;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public MultimediaServerCredentialsModel GetMultimediaCredentials(int idServer)
        {
            DataTable dataTable = new DataTable();
            dataTable = multimediaDAO.GetMultimediaCredentials(idServer);
            MultimediaServerCredentialsModel model = new MultimediaServerCredentialsModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {                        
                        model.username = dr["username"].ToString();
                        model.password = dr["password"].ToString();                        
                    }

                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<IModel> GetAllPhotosByMultimedia(int idMultimedia)
        {
            DataTable dataTable = new DataTable();
            dataTable = multimediaDAO.GetAllPhotosByMultimedia(idMultimedia);
            List<IModel> multimedia = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PhotosModel model = new PhotosModel();

                        if (dr["idPhotos"].ToString() != "")
                            model.idPhotos = Convert.ToInt32(dr["idPhotos"].ToString());

                        model.namePhotos = dr["namePhotos"].ToString(); 

                        if (dr["idMultimedia"].ToString() != "")
                            model.idMultimedia = Convert.ToInt32(dr["idMultimedia"].ToString());
                        
                        model.descMultimedia = dr["description"].ToString();                                      

                        if (dr["isActive"].ToString() != "")
                            model.isActive = Boolean.Parse(dr["isActive"].ToString());

                        if (dr["idUserCreator"].ToString() != "")
                            model.idUserCreator = Convert.ToInt32(dr["idUserCreator"].ToString());

                        if (dr["dtUserCreator"].ToString() != "")
                            model.dtUserCreator = DateTime.Parse(dr["dtUserCreator"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Convert.ToInt32(dr["idUserModified"].ToString());                        

                        if (dr["dtUserModified"].ToString() != "")
                            model.dtUserModified = DateTime.Parse(dr["dtUserModified"].ToString());

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
       
        public List<IModel> GetAllPeriods()
        {
            DataTable dataTable = new DataTable();
            dataTable = multimediaDAO.GetAllPeriods();
            List<IModel> multimedia = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        PeriodModel model = new PeriodModel();


                        if (dr["idPeriod"].ToString() != "")
                            model.idPeriod = Convert.ToInt32(dr["idPeriod"].ToString());

                        if (dr["descPeriod"].ToString() != "")
                            model.descPeriod = dr["descPeriod"].ToString();

                        if (dr["monthFrom"].ToString() != "")
                            model.monthFrom = Convert.ToInt32(dr["monthFrom"].ToString());

                        if (dr["monthTo"].ToString() != "")
                            model.monthTo = Convert.ToInt32(dr["monthTo"].ToString());

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

        public int SaveAndReturnID(MultimediaModel model, string nameForm, int idUser)
        {
            int retval = -1;
            try
            {

                retval = multimediaDAO.SaveAndReturnID(model,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(MultimediaModel model, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = multimediaDAO.Update(model,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public int SavePhotosAndReturnID(PhotosModel model, string nameForm, int idUser)
        {
            int retval = 0;
            try
            {

                retval = multimediaDAO.SavePhotosAndReturnID(model,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdatePhotosIsActive(bool isActive, int idPhotos, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = multimediaDAO.UpdatePhotosIsActive(isActive, idPhotos,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeletePhoto(int idPhotos, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = multimediaDAO.DeletePhoto( idPhotos,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        
    }
}