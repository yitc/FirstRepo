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
    public class ContactTypeBUS
    {
        private ContactTypeDAO contactTypeDAO;

        public ContactTypeBUS()
        {
            contactTypeDAO = new ContactTypeDAO();
        }

        public List<ContactTypeModel> GetAllContactType(string idLang)
        {
            DataTable dataTable = contactTypeDAO.GetAllContactType(idLang);
            List<ContactTypeModel> lst = new List<ContactTypeModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ContactTypeModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ContactTypeModel();
                        model.idContactType = Int32.Parse(dr["idContactType"].ToString());
                        model.descContactType = dr["descContactType"].ToString();
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