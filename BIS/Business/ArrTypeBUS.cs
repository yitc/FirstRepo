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
    public class ArrTypeBUS
    {
        private ArrTypeDAO arrtypeDAO;

        public ArrTypeBUS()
        {
            arrtypeDAO = new ArrTypeDAO();
        }

   

        public List<IModel> GetAllArrTypes()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrtypeDAO.GetAllArrTypes();
            List<IModel> arrtype = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    ArrTypeModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrTypeModel();

                        model.idArrType = Int32.Parse(dr["idArrType"].ToString());

                        if (dr["nameArrType"].ToString() != "")
                            model.nameArrType = dr["nameArrType"].ToString();

                        arrtype.Add(model);
                    }
                    return arrtype;

                }
                else
                    return null;
            }
            else
                return null;
        }
        //jelena
        public List<ArrTypeModel> GetArrTypes()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrtypeDAO.GetAllArrTypes();
            List<ArrTypeModel> arrtype = new List<ArrTypeModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    ArrTypeModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrTypeModel();

                        model.idArrType = Int32.Parse(dr["idArrType"].ToString());

                        if (dr["nameArrType"].ToString() != "")
                            model.nameArrType = dr["nameArrType"].ToString();

                        arrtype.Add(model);
                    }
                    return arrtype;

                }
                else
                    return null;
            }
            else
                return null;
        }


    }
}