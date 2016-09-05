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
    public class ArrangementBoardingPointBUS
    {
        private ArrangementBoardingPointDAO arrangeDAO;

        public ArrangementBoardingPointBUS()
        {
            arrangeDAO = new ArrangementBoardingPointDAO();
        }



        public List<BoardingPointModel> GetArrangementBoardingPoint(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetArrangementBoardingPoint(idArrangement);
            List<BoardingPointModel> list = new List<BoardingPointModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        BoardingPointModel model = new BoardingPointModel();

                        if (dr["idBoardingPoint"].ToString() != "")
                            model.idBoardingPoint = Int32.Parse(dr["idBoardingPoint"].ToString());

                        if (dr["nameBoardingPoint"].ToString() != "")
                            model.nameBoardingPoint = dr["nameBoardingPoint"].ToString();

                        if (dr["addressBoardingPoint"].ToString() != "")
                            model.addressBoardingPoint = dr["addressBoardingPoint"].ToString();

                        if (dr["departure"].ToString() != "")
                            model.dtDeparture = DateTime.Parse(dr["departure"].ToString());

                        if (dr["arrivel"].ToString() != "")
                            model.dtArrival = DateTime.Parse(dr["arrivel"].ToString());

                        if (dr["sortBoardingPoint"].ToString() != "")
                        {
                            model.sortBoardingPoint = Int32.Parse(dr["sortBoardingPoint"].ToString());
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

        public string GetBoardingPointName(int idBoardingPoint)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetBoardingPointName(idBoardingPoint);
            string name = "";

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        BoardingPointModel model = new BoardingPointModel();

                        if (dr["nameBoardingPoint"].ToString() != "")
                            name = dr["nameBoardingPoint"].ToString();
                        return name;
                        break;

                    }

                }
            }

            return name;
        }

        public List<BoardingPointModel> GetAllBoardingPoint(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetAllBoardingPoint(idArrangement);
            List<BoardingPointModel> list = new List<BoardingPointModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        BoardingPointModel model = new BoardingPointModel();

                        if (dr["idBoardingPoint"].ToString() != "")
                            model.idBoardingPoint = Int32.Parse(dr["idBoardingPoint"].ToString());

                        if (dr["nameBoardingPoint"].ToString() != "")
                            model.nameBoardingPoint = dr["nameBoardingPoint"].ToString();

                        if (dr["addressBoardingPoint"].ToString() != "")
                            model.addressBoardingPoint = dr["addressBoardingPoint"].ToString();

                        if (dr["addressBoardingPoint"].ToString() != "")
                            model.addressBoardingPoint = dr["addressBoardingPoint"].ToString();

                        if (dr["arrivel"].ToString() != "")
                            model.dtArrival = Convert.ToDateTime(dr["arrivel"].ToString());

                        if (dr["departure"].ToString() != "")
                            model.dtDeparture = Convert.ToDateTime(dr["departure"].ToString());

                        if (dr["sortBoardingPoint"].ToString() != "")
                            model.sortBoardingPoint = Convert.ToInt32(dr["sortBoardingPoint"].ToString());

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

        public Boolean Save(ArrangementBoardingPointModel model, string nameForm, int idUser)
        {
            Boolean retval = false;
            try
            {

                retval = arrangeDAO.Save(model, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public bool Delete(int id, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = arrangeDAO.Delete(id, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

    }
}
