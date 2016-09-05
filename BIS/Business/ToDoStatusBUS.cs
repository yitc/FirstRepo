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
    public class ToDoStatusBUS
    {
        private ToDoStatusDAO todoStatusDAO;

        public ToDoStatusBUS()
        {
            todoStatusDAO = new ToDoStatusDAO();
        }

        public List<ToDoStatusModel> GetAllToDoStatus(string idLang)
        {
            DataTable dataTable = todoStatusDAO.GetAllToDoStatus(idLang);
            List<ToDoStatusModel> lst = new List<ToDoStatusModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ToDoStatusModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ToDoStatusModel();
                        model.idStatusToDo = Int32.Parse(dr["idStatusToDo"].ToString());
                        model.descriptionStatus = dr["descriptionStatus"].ToString();
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