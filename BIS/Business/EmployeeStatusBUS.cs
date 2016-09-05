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
    public class EmployeeStatusBUS
    {
        private EmployeeStatusDAO employeeStatusDAO;

        public EmployeeStatusBUS()
        {
            employeeStatusDAO = new EmployeeStatusDAO();
        }

        public List<EmployeeStatusModel> GetAllEmployeeStatus(string idLang)
        {
            List<EmployeeStatusModel> compList = new List<EmployeeStatusModel>();

            DataTable dataTable = new DataTable();
            dataTable = employeeStatusDAO.GetAllEmployeeStatus(idLang);


            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    EmployeeStatusModel model = new EmployeeStatusModel();


                    model.idStatusEmployee = Int32.Parse(dr["idStatusEmployee"].ToString());
                    model.descriptionEmployee = dr["descriptionEmployee"].ToString();

                    compList.Add(model);
                }

                //   return model;
                return compList;
            }
            else
            {
                return null;
            }
        }
    }


}
