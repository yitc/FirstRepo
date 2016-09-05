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
    public class ArrNrBUS
    {
        private ArrNrDAO arrNrDAO;

        public ArrNrBUS()
        {
            arrNrDAO = new ArrNrDAO();
        }

       
       
        public ArrNrModel GetInvoiceNr()
        {
            ArrNrModel model = new ArrNrModel();

            DataTable dataTable = new DataTable();
            dataTable = arrNrDAO.GetInvoice();

            if(dataTable !=null)
            {
                foreach(DataRow dr in dataTable.Rows)
                {
                   // ArrNrModel model = new ArrNrModel();

                   // model.idTbl = Int32.Parse(dr["idTbl"].ToString());

                    if (dr["nrArrFak"].ToString() != "")
                        model.nrArrFak = Int32.Parse(dr["nrArrFak"].ToString());
                 
                  //  compList.Add(model);
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        public ArrNrModel GetSepaNr()
        {
            ArrNrModel model = new ArrNrModel();

            DataTable dataTable = new DataTable();
            dataTable = arrNrDAO.GetSepa();

            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    // ArrNrModel model = new ArrNrModel();

                    // model.idTbl = Int32.Parse(dr["idTbl"].ToString());

                    if (dr["nrSEPA"].ToString() != "")
                        model.nrSEPA = Int32.Parse(dr["nrSEPA"].ToString());

                    //  compList.Add(model);
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        public ArrNrModel GetArrNrByID(string idArrNr)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrNrDAO.GetArrNrByID(idArrNr);

            ArrNrModel arrNr = new ArrNrModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ArrNrModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrNrModel();

                        model.idTbl = Int32.Parse(dr["idTbl"].ToString());

                        if (dr["nrArrFak"].ToString() != "")
                        {
                            model.nrArrFak = Int32.Parse(dr["nrArrFak"].ToString());
                        }
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public ArrNrModel GetSepaNoIncrement()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrNrDAO.GetSepaNoIncrement();

            ArrNrModel arrNr = new ArrNrModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ArrNrModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArrNrModel();

                        model.idTbl = Int32.Parse(dr["idTbl"].ToString());

                        if (dr["nrSepa"].ToString() != "")
                        {
                            model.nrSEPA = Int32.Parse(dr["nrSepa"].ToString());
                        }
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}