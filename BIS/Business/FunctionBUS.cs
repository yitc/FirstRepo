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
    public class FunctionBUS
    {
        private FunctionDAO functionDAO;

        public FunctionBUS()
        {
            functionDAO = new FunctionDAO();
        }

        public List<FunctionModel> GetFunctions(string idLang)
        {
            List<FunctionModel> compList = new List<FunctionModel>();

            DataTable dataTable = new DataTable();
            dataTable = functionDAO.GetFunctions(idLang);


            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    FunctionModel model = new FunctionModel();


                    model.idFunction = Int32.Parse(dr["idFunction"].ToString());
                    model.nameFunction = dr["nameFunction"].ToString();
                  
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
