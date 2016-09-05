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
    public class PersonReasonOutBUS
    {
        private PersonReasonOutDAO reasonOutDAO;

        public PersonReasonOutBUS()
        {
            reasonOutDAO = new PersonReasonOutDAO();
        }

       
        public List<PersonReasonOutModel> GetAllReasonOut()
        {
            DataTable dataTable = new DataTable();
            dataTable = reasonOutDAO.GetAllReasonOut();
            List<PersonReasonOutModel> compList = new List<PersonReasonOutModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    PersonReasonOutModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new PersonReasonOutModel();

                        if (dr["idReasonOut"].ToString() != "")
                            model.idReasonOut = Int32.Parse(dr["idReasonOut"].ToString());
                        model.nameReasonOut = dr["nameReasonOut"].ToString();

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