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
    public class ArrangementBookStatusBUS
    {
        private ArrangementBookStatusDAO arrBookStatusDAO;

        public ArrangementBookStatusBUS()
        {
            arrBookStatusDAO = new ArrangementBookStatusDAO();
        }

        public List<ArrangementBookStatusModel> GetAllStatus(string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookStatusDAO.GetAllStatus(idLang);
            List<ArrangementBookStatusModel> status = new List<ArrangementBookStatusModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementBookStatusModel model = new ArrangementBookStatusModel();

                                              
                        if (dr["idStatus"].ToString() != "")
                            model.idStatus = Int32.Parse(dr["idStatus"].ToString());
                        model.nameStatus = dr["nameStatus"].ToString();


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

        public List<ArrangementBookStatusModel>GetAllArrangementBookStatus()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookStatusDAO.GetAllArrangementBookStatus();
            List<ArrangementBookStatusModel> status = new List<ArrangementBookStatusModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementBookStatusModel model = new ArrangementBookStatusModel();

                        if (dr["idStatus"].ToString() != null)
                        {
                            model.idStatus = Int32.Parse(dr["idStatus"].ToString());
                        }

                        if (dr["nameStatus"].ToString() != null)
                        {
                            model.nameStatus = dr["nameStatus"].ToString();
                        }

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

    }
}

  