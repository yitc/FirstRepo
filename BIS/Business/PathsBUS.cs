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
    public class PathsBUS
    {
        private PathsDAO pathsDAO;

        public PathsBUS()
        {
            pathsDAO = new PathsDAO();
        }

        //public bool Save(PathsModel paths, string nameForm, int idUser,List<int> selectLabel)
        //{
        //    bool retval = false;
        //    try
        //    {
        //        retval = pathsDAO.Save(paths, nameForm, idUser,selectLabel);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    return retval;
        //}
        public bool Update(string namePath, string nameForm, int idUser, int selLabel)
        {
            bool retval = false;
            try
            {
                retval = pathsDAO.Update(namePath, nameForm, idUser, selLabel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        //public List<IModel>GetAllPaths()
        //{
        //    List<IModel> compList = new List<IModel>();

        //    DataTable dataTable = new DataTable();
        //    dataTable = pathsDAO.GetAllPaths();

        //    if(dataTable!=null)
        //    {
        //        foreach(DataRow dr in dataTable.Rows)
        //        {
        //            PathsModel model = new PathsModel();

        //            if (dr["idPath"].ToString() != null)
        //            {
        //                model.idPath = Int32.Parse(dr["idPath"].ToString());
        //            }
        //            if (dr["namePath"].ToString() != null)
        //            {
        //                model.namePath = dr["namePath"].ToString();
        //            }
        //            if (dr["path"].ToString() != null)
        //            {
        //                model.path = dr["path"].ToString();
        //            }

        //            compList.Add(model);
        //        }
        //        return compList;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        
        public PathsModel GetAllPathsByID(string idPath)
        {
            DataTable dataTable = new DataTable();
            dataTable = pathsDAO.GetAllPathsByID(idPath);
            PathsModel pathModel = new PathsModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    PathsModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new PathsModel();

                        if (dr["idPath"].ToString() != null)
                            model.idPath = Int32.Parse(dr["idPath"].ToString());

                        model.typePath = dr["typePath"].ToString();

                        if (dr["namePath"].ToString() != null)
                            model.namePath = dr["namePath"].ToString();
                        if (dr["path"].ToString() != null)
                            model.path = dr["path"].ToString();
                    }
                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<PathsModel> GetAllPaths()
        {
            List<PathsModel> compList = new List<PathsModel>();

            DataTable dataTable = new DataTable();
            dataTable = pathsDAO.GetAllPaths();

            if (dataTable != null)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    PathsModel model = new PathsModel();

                    if (dr["idPath"].ToString() != null)
                    {
                        model.idPath = Int32.Parse(dr["idPath"].ToString());
                    }

                    model.typePath = dr["typePath"].ToString();

                    if (dr["namePath"].ToString() != null)
                    {
                        model.namePath = dr["namePath"].ToString();
                    }
                    if (dr["path"].ToString() != null)
                    {
                        model.path = dr["path"].ToString();
                    }

                    compList.Add(model);
                }
                return compList;
            }
            else
            {
                return null;
            }
        }


        public PathsModel GetPathsByType(string type)
        {
            DataTable dataTable = new DataTable();
            dataTable = pathsDAO.GetPathsByType(type);
            PathsModel pathModel = new PathsModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    PathsModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new PathsModel();

                        if (dr["idPath"].ToString() != null)
                            model.idPath = Int32.Parse(dr["idPath"].ToString());

                        model.typePath = dr["typePath"].ToString();

                        if (dr["namePath"].ToString() != null)
                            model.namePath = dr["namePath"].ToString();
                        if (dr["path"].ToString() != null)
                            model.path = dr["path"].ToString();
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