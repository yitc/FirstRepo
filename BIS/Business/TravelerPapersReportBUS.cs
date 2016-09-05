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
    public class TravelerPapersReportBUS
    {
        private TravelerPapersReportDAO travelerPapersReportDAO;

        public TravelerPapersReportBUS()
        {
            travelerPapersReportDAO = new TravelerPapersReportDAO();
        }

        public DataTable GetTravelerPaper(int idArrangement, int idContPers)
        {
           return travelerPapersReportDAO.GetTravelerPapers(idArrangement, idContPers);    
        }

        public DataTable GetPapers(int idArrangement)
        {
           return travelerPapersReportDAO.GetPapers(idArrangement);
        }

        public DataTable GetTekst(int idArrangement)
        {
            DataTable dataTable = new DataTable();

            dataTable = travelerPapersReportDAO.GetTekst(idArrangement);
           
            return dataTable;
        }

        public List<IModel> GetAllTravelerPapers()
        {
            DataTable dataTable = new DataTable();
            dataTable = travelerPapersReportDAO.GetAllTravelerPapers();

            List<IModel> list = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        TravelPapersModel model = new TravelPapersModel();

                        //if (dr["idTravelPapers"].ToString() != "")
                        //{
                        //    model.idTravelPapers = Int32.Parse(dr["idTravelPapers"].ToString());
                        //}
                        if (dr["nameTravelPapers"].ToString() != "")
                        {
                            model.nameTravelPapers = dr["nameTravelPapers"].ToString();
                        }

                        list.Add(model);
                    }
                    return list;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public DataTable GetArrangementRemaining(int idArrangement)
        {

            return travelerPapersReportDAO.GetArrangementRemaining(idArrangement);
        }
    }
}