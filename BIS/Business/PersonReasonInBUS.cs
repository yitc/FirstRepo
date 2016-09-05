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
    public class PersonReasonInBUS
    {
        private PersonReasonInDAO reasonInDAO;

        public PersonReasonInBUS()
        {
            reasonInDAO = new PersonReasonInDAO();
        }

       
        public List<PersonReasonInModel> GetAllReasonIn()
        {
            DataTable dataTable = new DataTable();
            dataTable = reasonInDAO.GetAllReasonIn();
            List<PersonReasonInModel> compList = new List<PersonReasonInModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    PersonReasonInModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new PersonReasonInModel();
                        
                       if (dr["idReasonIn"].ToString() != "")
                          model.idReasonIn = Int32.Parse(dr["idReasonIn"].ToString());
                       model.nameReasonIn = dr["nameReasonIn"].ToString();

                        compList.Add(model);
                    }
                    return compList;
                }
                else
                    return null;
            }
            else
                return null;
        }

    
    }


}