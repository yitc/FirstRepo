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
    public class ArrangementBookTravelPapersBUS
    {
        private ArrangementBookTravelPapersDAO arrBookTravelPapersDAO;

        public ArrangementBookTravelPapersBUS()
        {
            arrBookTravelPapersDAO = new ArrangementBookTravelPapersDAO();
        }

        public List<ArrangementBookTravelPapersModel> GetAllTravelPapers(string idLang)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrBookTravelPapersDAO.GetAllTravelPapers(idLang);
            List<ArrangementBookTravelPapersModel> travelPapers = new List<ArrangementBookTravelPapersModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArrangementBookTravelPapersModel model = new ArrangementBookTravelPapersModel();


                        if (dr["idTravelPapers"].ToString() != "")
                            model.idTravelPapers = Int32.Parse(dr["idTravelPapers"].ToString());
                        model.nameTravelPapers = dr["nameTravelPapers"].ToString();


                        travelPapers.Add(model);
                    }
                    return travelPapers;
                }
                else
                    return travelPapers;
            }
            else
                return travelPapers;
        }

    }
}

  