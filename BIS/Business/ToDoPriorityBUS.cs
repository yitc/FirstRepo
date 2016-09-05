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
    public class ToDoPriorityBUS
    {
        private ToDoPriorityDAO todoPriprityDAO;

        public ToDoPriorityBUS()
        {
            todoPriprityDAO = new ToDoPriorityDAO();
        }

        public List<ToDoPriorityModel> GetAllToDoPriority(string idLang)
        {
            DataTable dataTable = todoPriprityDAO.GetAllToDoPriority(idLang);
            List<ToDoPriorityModel> lst = new List<ToDoPriorityModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ToDoPriorityModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ToDoPriorityModel();
                        model.idPriorityToDo = Int32.Parse(dr["idPriorityToDo"].ToString());
                        model.descriptionPriority = dr["descriptionPriority"].ToString();
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