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
    public class VolSimilarityBUS
    {
        private VolSimilarityDAO simDAO;

        public VolSimilarityBUS()
        {
            simDAO = new VolSimilarityDAO();
        }

        public VolSimilarityModel GetSimilarityById(string idSimilarity, int idContPers)
        {
            DataTable dataTable = new DataTable();
            dataTable = simDAO.GetSimilarityById(idSimilarity, idContPers);
            VolSimilarityModel model = null;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();                    
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new VolSimilarityModel();

                        model.idSimilarity = dr["idSimilarity"].ToString();

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        model.optionSimilarity = dr["optionSimilarity"].ToString();

                        if (dr["dtEffectiveDate"].ToString() != "")
                            model.dtEffectiveDate = DateTime.Parse(dr["dtEffectiveDate"].ToString());

                        if (dr["dtExpirationDate"].ToString() != "")
                            model.dtExpirationDate = DateTime.Parse(dr["dtExpirationDate"].ToString());

                        if (dr["dtSent"].ToString() != "")
                            model.dtSent = DateTime.Parse(dr["dtSent"].ToString());
                    }

                    return model;
                }
                else
                    return null;
            }
            else
                return null;

        }

        public VolSimilarityArchiveModel GetSimilarityArchiveByDate(string idSimilarity, int idContPers, DateTime expdate)
        {
            DataTable dataTable = new DataTable();
            dataTable = simDAO.GetSimilarityArchiveByDate(idSimilarity, idContPers, expdate);
            VolSimilarityArchiveModel model = null;

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    //    DocumentsModel model = new DocumentsModel();                    
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new VolSimilarityArchiveModel();

                        if (dr["id"].ToString() != "")
                            model.id = Int32.Parse(dr["id"].ToString());

                        model.idSimilarity = dr["idSimilarity"].ToString();

                        if (dr["idContPers"].ToString() != "")
                            model.idContPers = Int32.Parse(dr["idContPers"].ToString());

                        model.optionSimilarity = dr["optionSimilarity"].ToString();

                        if (dr["dtEffectiveDate"].ToString() != "")
                            model.dtEffectiveDate = DateTime.Parse(dr["dtEffectiveDate"].ToString());

                        if (dr["dtExpirationDate"].ToString() != "")
                            model.dtExpirationDate = DateTime.Parse(dr["dtExpirationDate"].ToString());


                        if (dr["dtSent"].ToString() != "")
                            model.dtSent = DateTime.Parse(dr["dtSent"].ToString());
                    }

                    return model;
                }
                else
                    return null;
            }
            else
                return null;

        }

        public bool Save(List<VolSimilarityModel> list, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = simDAO.Save(list, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool SaveToArchive(VolSimilarityModel model, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = simDAO.SaveToArchive(model,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Delete(List<VolSimilarityModel> list, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = simDAO.Delete(list, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
    }
}
