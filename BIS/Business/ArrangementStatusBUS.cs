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
    public class ArrangementStatusBUS
    {
        private ArrangementStatusDAO arrangementStatusDAO;

        public ArrangementStatusBUS()
        {
            arrangementStatusDAO = new ArrangementStatusDAO();
        }

       

        public List<IModel> GetAllArrangementStatusList()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = arrangementStatusDAO.GetAllArrangementStatus();
                List<IModel> status = new List<IModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            ArrangementStatusModel model = new ArrangementStatusModel();

                            if (dr["idArrangementStatus"].ToString() != "")
                            {
                                model.idArrangementStatus = Int32.Parse(dr["idArrangementStatus"].ToString());
                            }

                            model.nameArrangementStatus = dr["nameArrangementStatus"].ToString();

                            status.Add(model);
                        }
                        return status;
                    }
                    else
                        return status;
                }
                else
                    return status;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ArrangementStatusModel> GetAllArrangementStatus()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangementStatusDAO.GetAllArrangementStatus();
            List<ArrangementStatusModel> compList = new List<ArrangementStatusModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementStatusModel model = new ArrangementStatusModel();

                        if (dr["idArrangementStatus"].ToString() != "")
                        {
                            model.idArrangementStatus = Int32.Parse(dr["idArrangementStatus"].ToString());
                        }
                        model.nameArrangementStatus = dr["nameArrangementStatus"].ToString();

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

        public ArrangementStatusModel GetAllArrangementStatusByID( int idArrangementStatus)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangementStatusDAO.GetArrangementStatusByID(idArrangementStatus);
            ArrangementStatusModel compList = new ArrangementStatusModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementStatusModel model = new ArrangementStatusModel();

                        if (dr["idArrangementStatus"].ToString() != "")
                        {
                            model.idArrangementStatus = Int32.Parse(dr["idArrangementStatus"].ToString());
                        }

                        model.nameArrangementStatus = dr["nameArrangementStatus"].ToString();
                        
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