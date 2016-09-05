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
    public class AgeCategoryBUS
    {
        private AgeCategoryDAO ageCategoryDAO;
        public AgeCategoryBUS()
        {
            ageCategoryDAO = new AgeCategoryDAO();
        }

        public bool Save(AgeCategoryModel ageCategory, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = ageCategoryDAO.Save(ageCategory, nameForm, idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public bool Update(AgeCategoryModel ageCategory, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = ageCategoryDAO.Update(ageCategory, nameForm, idUser);
            }
            catch(Exception ex)
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
                retval = ageCategoryDAO.Delete(id, nameForm, idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }

        public List<IModel>GetAllAgeCategory()
        {
            DataTable dataTable = new DataTable();
            dataTable = ageCategoryDAO.GetAllAgeCategory();

            List<IModel> ageCategoryModel = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        AgeCategoryModel model = new AgeCategoryModel();

                        model.idAgeCategory = Int32.Parse(dr["idAgeCategory"].ToString());
                        model.descAgeCategory = dr["descAgeCategory"].ToString();

                        if (dr["minAgeCategory"].ToString() != "")
                        {
                            model.minAgeCategory = Int32.Parse(dr["minAgeCategory"].ToString());
                        }

                        if (dr["maxAgeCategory"].ToString() != "")
                        {
                            model.maxAgeCategory = Int32.Parse(dr["maxAgeCategory"].ToString());
                        }

                        ageCategoryModel.Add(model);

                    }
                    return ageCategoryModel;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<AgeCategoryModel>GetAllAgeCategoryes()
        {
            DataTable dataTable = new DataTable();
            dataTable = ageCategoryDAO.GetAllAgeCategory();
            List<AgeCategoryModel> compList = new List<AgeCategoryModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        AgeCategoryModel model = new AgeCategoryModel();

                        model.idAgeCategory = Int32.Parse(dr["idAgeCategory"].ToString());
                        model.descAgeCategory = dr["descAgeCategory"].ToString();

                        if (dr["minAgeCategory"].ToString() != "")
                        {
                            model.minAgeCategory = Int32.Parse(dr["minAgeCategory"].ToString());
                        }

                        if (dr["maxAgeCategory"].ToString() != "")
                        {
                            model.maxAgeCategory = Int32.Parse(dr["maxAgeCategory"].ToString());
                        }

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

        public AgeCategoryModel GetAgeCategoryByID(string idAgeCategory)
        {
            DataTable dataTable = new DataTable();
            dataTable = ageCategoryDAO.GetAllAgeCategoryByID(idAgeCategory);
            AgeCategoryModel ageCategory = new AgeCategoryModel();

            if(dataTable != null)
            {
                if(dataTable.Rows.Count > 0)
                {
                    AgeCategoryModel model = null;

                    foreach(DataRow dr in dataTable.Rows)
                    {
                        model = new AgeCategoryModel();

                        model.idAgeCategory = Int32.Parse(dr["idAgeCategory"].ToString());
                        model.descAgeCategory = dr["descAgeCategory"].ToString();

                        if(dr["minAgeCategory"].ToString() != "")
                        {
                            model.minAgeCategory = Int32.Parse(dr["minAgeCategory"].ToString());
                        }

                        if(dr["maxAgeCategory"].ToString() != "")
                        {
                            model.maxAgeCategory=Int32.Parse(dr["maxAgeCategory"].ToString());
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

        public List<AgeCategoryModel> LastId()
        {
            DataTable dataTable = new DataTable();
            dataTable = ageCategoryDAO.LastId();

            List<AgeCategoryModel> ageCategoryModel = new List<AgeCategoryModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        AgeCategoryModel model = new AgeCategoryModel();

                        model.idAgeCategory = Int32.Parse(dr["idAgeCategory"].ToString());
                       
                        ageCategoryModel.Add(model);

                    }
                    return ageCategoryModel;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public List<AgeCategoryModel> isIn(int idAgeCategory)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = ageCategoryDAO.isIn(idAgeCategory);
                List<AgeCategoryModel> ageCategory = new List<AgeCategoryModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            AgeCategoryModel model = new AgeCategoryModel();

                            model.idAgeCategory = Int32.Parse(dr["idAgeCategory"].ToString());


                            ageCategory.Add(model);
                        }
                        return ageCategory;
                    }
                    else
                        return ageCategory;
                }
                else
                    return ageCategory;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        // NOVO DELETE
        public int checkIsInArrangemnet(int idAgeCategory)
        {
            int num = 0;
            DataTable dataTable = new DataTable();
            dataTable = ageCategoryDAO.checkIsInArrangemnet(idAgeCategory);

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    num = 1;
                }

            }
            return num;
        }
        public bool DeleteAgeCategorySript(int idAgeCategory, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = ageCategoryDAO.DeleteAgeCategoryScript(idAgeCategory, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

    }
}