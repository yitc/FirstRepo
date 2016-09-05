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
    public class ContactReasonBUS
    {
        private ContactReasonDAO contactReasonDAO;

        public ContactReasonBUS()
        {
            contactReasonDAO = new ContactReasonDAO();
        }

        public List<ContactReasonModel> GetAllContactReason(string idLang)
        {
            DataTable dataTable = contactReasonDAO.GetAllContactReason(idLang);
            List<ContactReasonModel> lst = new List<ContactReasonModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ContactReasonModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ContactReasonModel();
                        model.idContactReason = Int32.Parse(dr["idContactReason"].ToString());
                        model.descContactReason = dr["descContactReason"].ToString();
                        lst.Add(model);
                    }
                    return lst;
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}