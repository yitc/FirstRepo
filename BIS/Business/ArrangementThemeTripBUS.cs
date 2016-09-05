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
    public class ArrangementThemeTripBUS
    {
        private ArrangementThemeTripDAO arrangeDAO;

        public ArrangementThemeTripBUS()
        {
            arrangeDAO = new ArrangementThemeTripDAO();
        }

        public string GetThemeTripName(int idThemeTrip)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetThemeTripName(idThemeTrip);
            string name = "";

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ThemeTripModel model = new ThemeTripModel();

                        if (dr["nameThemeTrip"].ToString() != "")
                            name = dr["nameThemeTrip"].ToString();

                        break;

                    }

                }
            }

            return name;
        }

        public List<ThemeTripModel> GetArrangementThemeTrip(int idArrangement)
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetArrangementThemeTrip(idArrangement);
            List<ThemeTripModel> list = new List<ThemeTripModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                   
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ThemeTripModel  model = new ThemeTripModel();

                        if (dr["idThemeTrip"].ToString() != "")
                        model.idThemeTrip = Int32.Parse(dr["idThemeTrip"].ToString());

                        if (dr["nameThemeTrip"].ToString() != "")
                            model.nameThemeTrip = dr["nameThemeTrip"].ToString();
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

        public List<ThemeTripModel> GetAllThemeTrip()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetAllThemeTrip();
            List<ThemeTripModel> list = new List<ThemeTripModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ThemeTripModel model = new ThemeTripModel();

                        if (dr["idThemeTrip"].ToString() != "")
                            model.idThemeTrip = Int32.Parse(dr["idThemeTrip"].ToString());

                        if (dr["nameThemeTrip"].ToString() != "")
                            model.nameThemeTrip = dr["nameThemeTrip"].ToString().TrimEnd();
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

        public List<ThemeTripModel> GetAllThemeTripForDepartureList3()
        {
            DataTable dataTable = new DataTable();
            dataTable = arrangeDAO.GetAllThemeTripForDepartureList3();
            List<ThemeTripModel> list = new List<ThemeTripModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ThemeTripModel model = new ThemeTripModel();

                        if (dr["idThemeTrip"].ToString() != "")
                            model.idThemeTrip = Int32.Parse(dr["idThemeTrip"].ToString());

                        if (dr["nameThemeTrip"].ToString() != "")
                            model.nameThemeTrip = dr["nameThemeTrip"].ToString().TrimEnd();
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

        public Boolean Save(ArrangementThemeTripModel model, string nameForm, int idUser)
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
