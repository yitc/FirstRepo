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
    public class TranslationBUS
    {
        private TranslationDAO translationDAO;


        public TranslationBUS()
        {
            translationDAO = new TranslationDAO();
        }

        public List<IModel> GetAllTranslation(string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = translationDAO.GetAllTranslation(idLang);
            List<IModel> translation = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        TranslationModel model = new TranslationModel();

                        model.idLang = Int32.Parse(dr["idLang"].ToString());

                        if (dr["stringKey"].ToString() != "")
                            model.stringKey = dr["stringKey"].ToString();

                        if (dr["stringValue"].ToString() != "")
                            model.stringValue = dr["stringValue"].ToString();

                        if (dr["dtString"].ToString() != "")
                            model.dtString = Convert.ToDateTime(dr["dtString"].ToString());

                        translation.Add(model);
                    }
                    return translation;
                }
                else
                    return translation;
            }
            else
                return translation;
        }

        public List<TranslationModel> CheckIfTranslationExists(string idLang, string stringKey)
        {
            DataTable dataTable = new DataTable();
            dataTable = translationDAO.CheckIfTranslationExists(idLang, stringKey);
            List<TranslationModel> translation = new List<TranslationModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        TranslationModel model = new TranslationModel();

                        model.idLang = Int32.Parse(dr["idLang"].ToString());

                        if (dr["stringKey"].ToString() != "")
                            model.stringKey = dr["stringKey"].ToString();

                        if (dr["stringValue"].ToString() != "")
                            model.stringValue = dr["stringValue"].ToString();

                        if (dr["dtString"].ToString() != "")
                            model.dtString = Convert.ToDateTime(dr["dtString"].ToString());

                        translation.Add(model);
                    }
                    return translation;
                }
                else
                    return translation;
            }
            else
                return translation;
        }

        public List<TranslationModel> CheckIfTranslationValueExists(string idLang, string stringKey)
        {
            DataTable dataTable = new DataTable();
            dataTable = translationDAO.CheckIfTranslationValueExists(idLang, stringKey);
            List<TranslationModel> translation = new List<TranslationModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        TranslationModel model = new TranslationModel();

                        model.idLang = Int32.Parse(dr["idLang"].ToString());

                        if (dr["stringKey"].ToString() != "")
                            model.stringKey = dr["stringKey"].ToString();

                        if (dr["stringValue"].ToString() != "")
                            model.stringValue = dr["stringValue"].ToString();

                        if (dr["dtString"].ToString() != "")
                            model.dtString = Convert.ToDateTime(dr["dtString"].ToString());

                        translation.Add(model);
                    }
                    return translation;
                }
                else
                    return translation;
            }
            else
                return translation;
        }

        public bool Save(TranslationModel translation, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = translationDAO.Save(translation,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Update(TranslationModel translation, string idLang, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = translationDAO.Update(translation,idLang,nameForm,idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

    }
}
