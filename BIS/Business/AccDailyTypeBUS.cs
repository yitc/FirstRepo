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
    public class AccDailyTypeBUS
    {
        private AccDailyTypeDAO dailyTypeDAO;

        public AccDailyTypeBUS()
        {
            dailyTypeDAO = new AccDailyTypeDAO();
        }



        public List<AccDailyTypeModel> GetAllTypes()
        {
            DataTable dataTable = new DataTable();
            dataTable = dailyTypeDAO.GetAllTypes();
            List<AccDailyTypeModel> dailyTypemodel = new List<AccDailyTypeModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    AccDailyTypeModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new AccDailyTypeModel();

                        model.idDailyType = Int32.Parse(dr["idDailyType"].ToString());
                        if (dr["descDailyType"].ToString() != "")
                            model.descDailyType = dr["descDailyType"].ToString();
                        dailyTypemodel.Add(model);
                    }
                    return dailyTypemodel;

                }
                else
                    return null;
            }
            else
                return null;
        }


    }
}