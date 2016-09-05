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
    public class SearchBookBUS
    {
        private SearchBookDAO searchBookDAO;

        public SearchBookBUS()
        {
            searchBookDAO = new SearchBookDAO();
        }

        #region stari bus
        //    public List<SearchBookModel> GetFilteredAArrangementsBook(DateTime dtFrom, DateTime dtTo, string nameCountry, string status,string article1, string article2,string article3)
        //    {
        //        DataTable dataTable = new DataTable();
        //        dataTable = searchBookDAO.GetFilteredAArrangementsBook(dtFrom, dtTo, nameCountry, status, article1, article2, article3);
        //        List<SearchBookModel> getFilteredArrangement = new List<SearchBookModel>();

        //        if (dataTable != null)
        //        {
        //            if (dataTable.Rows.Count > 0)
        //            {
        //                SearchBookModel model = null;

        //                foreach (DataRow dr in dataTable.Rows)
        //                {
        //                    model = new SearchBookModel();

        //                    if (dr["anchorage"].ToString() != "")
        //                        model.anchorage = Boolean.Parse(dr["Anchorage"].ToString());

        //                    if (dr["armSometimes"].ToString() != "")
        //                        model.armSometimes = Boolean.Parse(dr["ArmSometimes"].ToString());

        //                    if (dr["statusArrangement"].ToString() != "")
        //                        model.statusArrangement = dr["statusArrangement"].ToString();

        //                    if (dr["nameCountry"].ToString() != "")
        //                        model.nameCountry = dr["nameCountry"].ToString();

        //                    //if (dr["themeTrip"].ToString() != "")
        //                    //    model.themeTrip = dr["themeTrip"].ToString();

        //                    if (dr["dtFromArrangement"].ToString() != "")
        //                        model.dtFromArrangement = DateTime.Parse(dr["dtFromArrangement"].ToString());

        //                    if (dr["dtToArrangement"].ToString() != "")
        //                        model.dtToArrangement = DateTime.Parse(dr["dtToArrangement"].ToString());

        //                    //if (dr["ArticleId1"].ToString() != "")
        //                    //    model.ArticleId1 = Int32.Parse(dr["ArticleId1"].ToString());

        //                    //if (dr["ArticleId2"].ToString() != "")
        //                    //    model.ArticleId2 = Int32.Parse(dr["ArticleId2"].ToString());

        //                    //if (dr["ArticleId3"].ToString() != "")
        //                    //    model.ArticleId3 = Int32.Parse(dr["ArticleId3"].ToString());

        //                    if (dr["wheelchair"].ToString() != "")
        //                        model.wheelchair = Boolean.Parse(dr["Wheelchair"].ToString());

        //                    if (dr["Rollator"].ToString() != "")
        //                        model.Rollator = Boolean.Parse(dr["Rollator"].ToString());

        //                    if (dr["idRoom"].ToString() != "")
        //                        model.idRoom = dr["idRoom"].ToString();

        //                    if (dr["nameArrangement"].ToString() != "")
        //                        model.nameArrangement = dr["nameArrangement"].ToString();

        //                    //if (dr["filter"].ToString() != "")
        //                    //    model.filter = Int32.Parse(dr["filter"].ToString());

        //                    getFilteredArrangement.Add(model);
        //                }
        //                return getFilteredArrangement;
        //            }
        //            else
        //                return null;
        //        }
        //        else return null;

        //    }
        #endregion

        public List<SearchBookModel> GetFilteredAArrangementsBook(DateTime dtFrom, DateTime dtTo, int idCountry, string status, int help, List<int> themeTrip, List<string> articles, List<int> label)
        {

            DataTable dataTable = new DataTable();
            dataTable = searchBookDAO.GetFilteredAArrangementsBook(dtFrom, dtTo, idCountry, status,  help, themeTrip, articles,label);
            List<SearchBookModel> getFilteredArrangement = new List<SearchBookModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    SearchBookModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new SearchBookModel();

                        if (dr["anchorage"].ToString() != "")
                            model.anchorage = Int32.Parse(dr["Anchorage"].ToString());

                        if (dr["armSometimes"].ToString() != "")
                            model.armSometimes = Int32.Parse(dr["ArmSometimes"].ToString());

                        if (dr["statusArrangement"].ToString() != "")
                            model.statusArrangement = dr["statusArrangement"].ToString();

                        if (dr["nameCountry"].ToString() != "")
                            model.nameCountry = dr["nameCountry"].ToString();
                                                

                        if (dr["dtFromArrangement"].ToString() != "")
                            model.dtFromArrangement = DateTime.Parse(dr["dtFromArrangement"].ToString());

                        if (dr["dtToArrangement"].ToString() != "")
                            model.dtToArrangement = DateTime.Parse(dr["dtToArrangement"].ToString());


                        if (dr["wheelchair"].ToString() != "")
                            model.wheelchair = Int32.Parse(dr["Wheelchair"].ToString());

                        if (dr["Rollator"].ToString() != "")
                            model.Rollator = Int32.Parse(dr["Rollator"].ToString());


                        if (dr["nameArrangement"].ToString() != "")
                            model.nameArrangement = dr["nameArrangement"].ToString();

                        if (dr["idArrangement"].ToString() != "")
                            model.idArrangement = Int32.Parse(dr["idArrangement"].ToString());

                        //model.select = bool.Parse("false");

                        getFilteredArrangement.Add(model);
                    }
                    return getFilteredArrangement;
                }
                else
                    return null;
            }
            else return null;
        }

        public List<IModel> GetAllRooms(DateTime dtFrom, DateTime dtTo, int idCountry, string status, List<int> themeTrip)
        {
            DataTable dataTable = new DataTable();
            dataTable = searchBookDAO.GetAllRooms(dtFrom, dtTo, idCountry, status, themeTrip);
            List<IModel> getFilteredRooms = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ArticalModelRooms model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArticalModelRooms();



                        if (dr["nameArtical"].ToString() != "")
                            model.nameArtikal = dr["nameArtical"].ToString();

                        if (dr["idArticle"].ToString() != "")
                            model.codeArticle = dr["idArticle"].ToString();


                        getFilteredRooms.Add(model);
                    }
                    return getFilteredRooms;
                }
                else
                    return null;
            }
            else return null;
        }

        public List<IModel>GetAllRoomsWithArticle(DateTime dtFrom, DateTime dtTo, int idCountry, string status, List<int> themeTrip)
        {
            DataTable dataTable = new DataTable();
            dataTable = searchBookDAO.GetAllRoomsWithArticle(dtFrom, dtTo, idCountry, status, themeTrip);
            List<IModel> getFilteredRooms = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ArticalModelRooms model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArticalModelRooms();

                        if (dr["nameArticle"].ToString() != "")
                            model.nameArtikal = dr["nameArticle"].ToString();

                        if (dr["idArticle"].ToString() != "")
                            model.codeArticle = dr["idArticle"].ToString();


                        getFilteredRooms.Add(model);
                    }
                    return getFilteredRooms;
                }
                else
                    return null;
            }
            else return null;
        }
    }
}