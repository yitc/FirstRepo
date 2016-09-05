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
    public class AccDailyVerInBUS
    {
        private AccDailyVerInDAO dailyVerInDAO;

        public AccDailyVerInBUS()
        {
            dailyVerInDAO = new AccDailyVerInDAO();
        }



        public List<AccDailyVerInModel> GetAllClass()
        {
            DataTable dataTable = new DataTable();
            dataTable = dailyVerInDAO.GetAllClass();
            List<AccDailyVerInModel> dailyVerInmodel = new List<AccDailyVerInModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccDailyVerInModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccDailyVerInModel();

                        model.idDailyVerIn = Int32.Parse(dr["idDailyVerIn"].ToString());
                        if (dr["nameDailyVerIn"].ToString() != "")
                            model.nameDailyVerIn = dr["nameDailyVerIn"].ToString();
                        dailyVerInmodel.Add(model);
                    }
                    return dailyVerInmodel;

                }
                else
                    return null;
            }
            else
                return null;
        }


    }
}