using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.DAO;
using BIS.Model;
using System;
using System.Collections.Generic;

namespace BIS.Business
{
    public class ContactPersonFunctionBUS
    {
        private ContactPersonFunctionDAO contactPersonFunctionDAO;

        public ContactPersonFunctionBUS()
        {
            contactPersonFunctionDAO = new ContactPersonFunctionDAO();
        }

       

        public List<ContactPersonFunctionModel> GetAllContactPersonFunction(string idLang)
        {
            List<ContactPersonFunctionModel> compList = new List<ContactPersonFunctionModel>();

            DataTable dataTable = new DataTable();
            dataTable = contactPersonFunctionDAO.GetAllContactPersonFunction(idLang);

            if(dataTable != null)
            {
                foreach(DataRow dr in dataTable.Rows)
                {
                    ContactPersonFunctionModel model = new ContactPersonFunctionModel();

                    model.idFunction = Int32.Parse(dr["idFunction"].ToString());
                    model.nameFunction = dr["nameFunction"].ToString();

                    compList.Add(model);
                }

                return compList;
            }
            else
            {
                return null;
            }
        }

        public ContactPersonFunctionModel GetContactPersonFunctionById(int idFunction)
        {
            DataTable dataTable = new DataTable();
            dataTable = contactPersonFunctionDAO.GetContactPersonFunctionById(idFunction);
            ContactPersonFunctionModel contactPersonFunction = new ContactPersonFunctionModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ContactPersonFunctionModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ContactPersonFunctionModel();

                        model.idFunction = Int32.Parse(dr["idFunction"].ToString());
                        model.nameFunction = dr["nameFunction"].ToString();
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}