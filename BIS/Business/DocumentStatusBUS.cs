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
    public class DocumentStatusBUS
    {
        private DocumentStatusDAO documentStatusDAO;

        public DocumentStatusBUS()
        {
            documentStatusDAO = new DocumentStatusDAO();
        }

        public List<DocumentStatusModel> GetStatuses(string idLang)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = documentStatusDAO.GetStatuses(idLang);
                List<DocumentStatusModel> status = new List<DocumentStatusModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        DocumentStatusModel model = null;
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            model = new DocumentStatusModel();

                            model.idDocumentStatus = Int32.Parse(dr["idDocumentStatus"].ToString());
                            model.valueStatus = Int32.Parse(dr["valueStatus"].ToString());
                            model.descriptionStatus = dr["descriptionStatus"].ToString();
                            

                            status.Add(model);
                        }
                        return status;
                    }
                    else
                        return status;
                }
                else
                    return status;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

     
    }
}
