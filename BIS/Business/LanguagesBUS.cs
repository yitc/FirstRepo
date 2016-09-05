using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIS.DAO;
using BIS.Model;
using System.Data;

namespace BIS.Business
{
    public class LanguagesBUS
    {

        private LanguagesDAO languagesDAO;

        public LanguagesBUS()
        {
            languagesDAO = new LanguagesDAO();
        }

        public List<LanguagesModel> GetLanguagesDetails()
        {
            List<LanguagesModel> langList = new List<LanguagesModel>();

            DataTable dataTable = new DataTable();
            dataTable = languagesDAO.GetLanguages();


            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    LanguagesModel langmod = new LanguagesModel();

                    if (dr["idLang"].ToString() != "")
                    {
                        langmod.idLang = Convert.ToInt32(dr["idLang"].ToString());
                    }
                    if (dr["nameLang"].ToString() != "")
                    {
                        langmod.nameLang = dr["nameLang"].ToString();
                    }
                    else
                        langmod.nameLang = null;

                    langList.Add(langmod);
                }

                return langList;
            }
            else
            {
                return null;
            }
        }

        public Boolean updateUserLanguage(string lang, int idUser, string nameForm)
        {
            return languagesDAO.UpdateLanguages(lang, idUser, nameForm);
        }
    }
}
