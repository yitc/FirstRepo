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
    // IN =============================================================================================================
    public class VolontaryReasonInBUS
    {
        private VoluntaryReasonInDAO vreasonInDAO;

        public VolontaryReasonInBUS()
        {
            vreasonInDAO = new VoluntaryReasonInDAO();
        }

        public List<VoluntaryReasonInModel> GetAllReasonIn()
        {
            DataTable dataTable = new DataTable();
            dataTable = vreasonInDAO.GetAllReasonIn();
            List<VoluntaryReasonInModel> compList = new List<VoluntaryReasonInModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    VoluntaryReasonInModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new VoluntaryReasonInModel();

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

        public List<VoluntaryReasonInModel> GetAllReasonInLookup()
        {
            DataTable dataTable = new DataTable();
            dataTable = vreasonInDAO.GetAllReasonInLookup();
            List<VoluntaryReasonInModel> compList = new List<VoluntaryReasonInModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    VoluntaryReasonInModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new VoluntaryReasonInModel();

                        if (dr["idReasonIn"].ToString() != "")
                            model.idReasonIn = Int32.Parse(dr["idReasonIn"].ToString());
                        model.nameReasonIn = dr["nameReasonIn"].ToString();

                        model.select = bool.Parse("false");

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
  // OUT ======================================================================================================================
    public class VolontaryReasonOutBUS
    {
        private VoluntaryReasonOutDAO vreasonOutDAO;

        public VolontaryReasonOutBUS()
        {
            vreasonOutDAO = new VoluntaryReasonOutDAO();
        }

   
        public List<VoluntaryReasonOutModel> GetAllReasonOut()
        {
            DataTable dataTable = new DataTable();
            dataTable = vreasonOutDAO.GetAllReasonOut();
            List<VoluntaryReasonOutModel> compList = new List<VoluntaryReasonOutModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    VoluntaryReasonOutModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new VoluntaryReasonOutModel();

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