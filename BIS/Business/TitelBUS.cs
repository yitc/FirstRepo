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
    public class TitleBUS
    {
        private TitleDAO TitleDAO;

        public TitleBUS()
        {
            TitleDAO = new TitleDAO();
        }

        public List<TitleModel> GetAllTitle()
        {
            DataTable dataTable = new DataTable();
            dataTable = TitleDAO.GetAllTitle();
            List<TitleModel> titel = new List<TitleModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        TitleModel model = new TitleModel();

                                              
                        if (dr["idTitle"].ToString() != "")
                            model.idTitle = Int32.Parse(dr["idTitle"].ToString());
                        model.nameTitle = dr["nameTitle"].ToString();


                        titel.Add(model);
                    }
                    return titel;
                }
                else
                    return titel;
            }
            else
                return titel;
        }

    }
}

  