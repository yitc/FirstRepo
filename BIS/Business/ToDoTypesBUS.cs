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
    public class ToDoTypeBUS
    {
        private ToDoTypeDAO todoTypeDAO;

        public ToDoTypeBUS()
        {
            todoTypeDAO = new ToDoTypeDAO();
        }

        public List<ToDoTypeModel> GetAllToDoTypes(string idLang)
        {
            DataTable dataTable = todoTypeDAO.GetAllToDoTypes(idLang);
            List<ToDoTypeModel> lst = new List<ToDoTypeModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ToDoTypeModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ToDoTypeModel();
                        model.idToDoType = Int32.Parse(dr["idToDoType"].ToString());
                        model.descriptionToDoType = dr["descriptionToDoType"].ToString();
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