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
    public class TravelDataBUS
    {
        private TravelDataDAO tdDAO;
        public TravelDataBUS()
        {
            tdDAO = new TravelDataDAO();
        }

        //public List<IModel> GetAllTypeTravelData(string language)
        //{
        //    DataTable dataTable = new DataTable();
        //    dataTable = tdDAO.GetAllTypeTravelData(language);
        //    List<IModel> filters = new List<IModel>();

        //    if (dataTable != null)
        //    {
        //        if (dataTable.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dataTable.Rows)
        //            {
        //                TypeModel model = new TypeModel();

        //                if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
        //                    model.ID = Int32.Parse(dr["ID"].ToString());

        //                model.name = dr["name"].ToString();
        //                model.type = dr["type"].ToString();
                        

        //                filters.Add(model);
        //            }
        //            return filters;
        //        }
        //        else
        //            return filters;
        //    }
        //    else
        //        return filters;
        //}

        public List<IModel> GetAllCodeTrainingFromVolFeatures(string language)
        {
            DataTable dataTable = new DataTable();
            dataTable = tdDAO.GetAllTypeTravelData(language);
            List<IModel> filters = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        CodeTrainingFromVolFeaturesModel model = new CodeTrainingFromVolFeaturesModel();

                        if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                            model.ID = Int32.Parse(dr["ID"].ToString());

                        model.name = dr["name"].ToString();
                        model.type = dr["type"].ToString();
                        model.nameArrangementStatus = dr["name"].ToString();

                       
                        if (dr["code"].ToString() != "" || dr["code"].ToString() != null)
                        { 
                            model.code = dr["code"].ToString();
                        }

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

        public bool UpdateArrType(int idArrType, string nameArrType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tdDAO.UpdateArrType(idArrType, nameArrType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateCertificate(int idCertificate, string nameCertificate, string codeCertificate, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tdDAO.UpdateCertificates(idCertificate, nameCertificate, codeCertificate, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateTraining(int idTraining, string nameTraining, string codeTraining, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tdDAO.UpdateTraining(idTraining, nameTraining, codeTraining, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool UpdateArrangementStatus(int idArrangementStatus, string nameArrangementStatus, string nameForm, int idUser)
        {
            bool retval = false;

            try
            {
                retval = tdDAO.UpdateArrangementStatus(idArrangementStatus, nameArrangementStatus,nameForm,idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool InsertArrangement(int idArrType, string nameArrType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tdDAO.InsertArrangement(idArrType, nameArrType, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertCertificate(int idCertificate, string nameCertificate, string codeCertificate, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tdDAO.InsertCertificate(idCertificate, nameCertificate, codeCertificate, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertTraining(int idTraining, string nameTraining, string codeTraining, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tdDAO.InsertTraining(idTraining, nameTraining, codeTraining, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool InsertArrangementStatus(int idArrangementStatus, string nameArrangementStatus, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = tdDAO.InsertArrangementStatus(idArrangementStatus, nameArrangementStatus, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool DeleteArrType(int idArrType, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = tdDAO.DeleteArrType(idArrType, nameForm, idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool DeleteCertificates(int idCertificates, string nameForm, int idUser)
       {
           bool retval = false;
           try
           {
               retval = tdDAO.DeleteCertificates(idCertificates, nameForm, idUser);
           }
           catch(Exception ex)
           {
               throw new Exception(ex.Message);
           }
           return retval;
       }

        public bool DeleteTraining(int idTraining, string nameForm, int idUser)
       {
           bool retval = false;
           try
           {
               retval = tdDAO.DeteleTraining(idTraining, nameForm, idUser);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
           return retval;
       }

        public bool DeleteArrangementStatus(int idArrangementStatus, string nameForm, int idUser)
       {
           bool retval = false;
            try
            {
                retval = tdDAO.DeleteArrangementStatus(idArrangementStatus, nameForm, idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
       }

       #region IsIn
       public List<LastIdModel>isInArrType()
       {
           DataTable dataTable = new DataTable();
           dataTable = tdDAO.isInArrType();
           List<LastIdModel> f = new List<LastIdModel>();

           if (dataTable != null)
           {
               if (dataTable.Rows.Count > 0)
               {
                   foreach (DataRow dr in dataTable.Rows)
                   {
                       LastIdModel model = new LastIdModel();

                       if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                       {
                           model.ID = Int32.Parse(dr["ID"].ToString());
                           f.Add(model);
                       }
                   }
                   return f;
               }
               else
                   return f;
           }
           else
               return f;
       }

       public List<CodeTrainingFromVolFeaturesModel> isInTrainingInVolFeatures(string codeTraining)
       {
           DataTable dataTable = new DataTable();
           dataTable =tdDAO.isInTrainingInVolFeatures(codeTraining);
           List<CodeTrainingFromVolFeaturesModel> f = new List<CodeTrainingFromVolFeaturesModel>();

           if (dataTable != null)
           {
               if (dataTable.Rows.Count > 0)
               {
                   foreach (DataRow dr in dataTable.Rows)
                   {
                       CodeTrainingFromVolFeaturesModel model = new CodeTrainingFromVolFeaturesModel();
                       if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                           model.ID = Int32.Parse(dr["ID"].ToString());

                       model.name = dr["name"].ToString();
                       model.type = dr["type"].ToString();
                       model.nameArrangementStatus = dr["name"].ToString();

                       if (dr["code"].ToString() != "" || dr["code"].ToString() != null)
                       {
                           model.code = dr["code"].ToString();
                       }

                       f.Add(model);

                   }
                   return f;
               }
               else
                   return f;
           }
           else

               return f;
       }

       public List<CodeTrainingFromVolFeaturesModel> isInCertificatesInVolFeatures(string codeCertificate)
       {
           DataTable dataTable = new DataTable();
           dataTable = tdDAO.isInCertificatesInVolFeatures(codeCertificate);
           List<CodeTrainingFromVolFeaturesModel> f = new List<CodeTrainingFromVolFeaturesModel>();

           if (dataTable != null)
           {
               if (dataTable.Rows.Count > 0)
               {
                   foreach (DataRow dr in dataTable.Rows)
                   {
                       CodeTrainingFromVolFeaturesModel model = new CodeTrainingFromVolFeaturesModel();
                       if (dr["ID"].ToString() != "" || dr["ID"].ToString() != null)
                           model.ID = Int32.Parse(dr["ID"].ToString());

                       model.name = dr["name"].ToString();
                       model.type = dr["type"].ToString();
                       model.nameArrangementStatus = dr["name"].ToString();

                       if (dr["code"].ToString() != "" || dr["code"].ToString() != null)
                       {
                           model.code = dr["code"].ToString();
                       }

                       f.Add(model);

                   }
                   return f;
               }
               else
                   return f;
           }
           else
               return f;
       }
       #endregion
    }
}