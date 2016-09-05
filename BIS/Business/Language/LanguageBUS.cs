using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.DAO;
using BIS.Model;

using System.Windows.Forms;
using System.Resources;



namespace BIS.Business
{
    public class LanguageBUS
    {
        private LanguageDAO languageDAO;
        private string resxFile = @"LanguageResources.resx";

        public LanguageBUS()
        {
            languageDAO = new LanguageDAO();
        }
        public DataTable GetLanguageStrings(string idLangFromUsers)
        {
            DataTable dataTable = new DataTable();
            dataTable = languageDAO.GetLanguageStrings(idLangFromUsers);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    using (ResXResourceWriter resx = new ResXResourceWriter(resxFile))
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            resx.AddResource(dr["stringKey"].ToString(), dr["stringValue"].ToString());                            
                        }
                    }
                                         
                    return dataTable;
                }
                else 
                    return null;
            }
            else
                return null;
        }
    }
}
