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
    public class ArticalGroupsBUS
    {
        private ArticalGroupsDAO articalGroupsDAO;

        public ArticalGroupsBUS()
        {
            articalGroupsDAO = new ArticalGroupsDAO();
        }


        public List<IModel> GetAllArticalGroups()
        {
            DataTable dataTable = new DataTable();
            dataTable = articalGroupsDAO.GetAllArticalGroups();
            List<IModel> arrange = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArticalGroupsModel model = new ArticalGroupsModel();
                        if (dr["codeArticalGroup"].ToString() != "")
                            model.codeArticalGroup = dr["codeArticalGroup"].ToString();

                        if (dr["nameArticalGroup"].ToString() != "")
                            model.nameArticalGroup = dr["nameArticalGroup"].ToString();

                        if (dr["inkopArtical"].ToString() != "")
                            model.inkopArtical = dr["inkopArtical"].ToString();

                        if (dr["descInkopArtical"].ToString() != "")
                            model.descInkopArtical = dr["descInkopArtical"].ToString();

                        if (dr["verkopArtical"].ToString() != "")
                            model.verkopArtical = dr["verkopArtical"].ToString();

                        if (dr["descVerkopArtical"].ToString() != "")
                            model.descVerkopArtical = dr["descVerkopArtical"].ToString();

                        if (dr["isActive"].ToString() != "")
                            model.isActive = Convert.ToBoolean(dr["isActive"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Convert.ToInt32(dr["idUserCreated"].ToString());

                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();

                        if (dr["dtUserCreated"].ToString() != "")
                            model.dtUserCreated = Convert.ToDateTime(dr["dtUserCreated"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Convert.ToInt32(dr["idUserModified"].ToString());

                        if (dr["nameUserModified"].ToString() != "")
                            model.nameUserModified = dr["nameUserModified"].ToString();

                        //if (dr["dtUserModified"].ToString() != "")
                        //    model.dtUserModified = Convert.ToDateTime(dr["dtUserModified"].ToString());

                        if (dr["classArticalGroup"].ToString() != "")
                            model.classArticalGroup = dr["classArticalGroup"].ToString();
                      
                        arrange.Add(model);
                    }
                    return arrange;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<ArticalGroupsModel> GetAllArticalClass()
        {
            DataTable dataTable = new DataTable();
            dataTable = articalGroupsDAO.GetAllArticalClass();
            List<ArticalGroupsModel> arrange = new List<ArticalGroupsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        ArticalGroupsModel model = new ArticalGroupsModel();
                        //if (dr["codeArticalGroup"].ToString() != "")
                        //    model.codeArticalGroup = dr["codeArticalGroup"].ToString();

                        //if (dr["nameArticalGroup"].ToString() != "")
                        //    model.nameArticalGroup = dr["nameArticalGroup"].ToString();

                        //if (dr["inkopArtical"].ToString() != "")
                        //    model.inkopArtical = dr["inkopArtical"].ToString();

                        //if (dr["descInkopArtical"].ToString() != "")
                        //    model.descInkopArtical = dr["descInkopArtical"].ToString();

                        //if (dr["verkopArtical"].ToString() != "")
                        //    model.verkopArtical = dr["verkopArtical"].ToString();

                        //if (dr["descVerkopArtical"].ToString() != "")
                        //    model.descVerkopArtical = dr["descVerkopArtical"].ToString();

                        //if (dr["isActive"].ToString() != "")
                        //    model.isActive = Convert.ToBoolean(dr["isActive"].ToString());

                        //if (dr["idUserCreated"].ToString() != "")
                        //    model.idUserCreated = Convert.ToInt32(dr["idUserCreated"].ToString());

                        //if (dr["nameUserCreated"].ToString() != "")
                        //    model.nameUserCreated = dr["nameUserCreated"].ToString();

                        //if (dr["dtUserCreated"].ToString() != "")
                        //    model.dtUserCreated = Convert.ToDateTime(dr["dtUserCreated"].ToString());

                        //if (dr["idUserModified"].ToString() != "")
                        //    model.idUserModified = Convert.ToInt32(dr["idUserModified"].ToString());

                        //if (dr["nameUserModified"].ToString() != "")
                        //    model.nameUserModified = dr["nameUserModified"].ToString();

                        //if (dr["dtUserModified"].ToString() != "")
                        //    model.dtUserModified = Convert.ToDateTime(dr["dtUserModified"].ToString());

                        if (dr["classArticalGroup"].ToString() != "")
                            model.classArticalGroup = dr["classArticalGroup"].ToString();

                        arrange.Add(model);
                    }
                    return arrange;

                }
                else
                    return null;
            }
            else
                return null;
        }

        public int checkIfExist(string code)
        {
            DataTable dataTable = new DataTable();
            dataTable = articalGroupsDAO.checkIfExist(code);
            List<IModel> arrange = new List<IModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows.Count;

                }
                else
                    return 0;
            }
            else
                return 0;
        }

        public bool Save(ArticalGroupsModel model, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = articalGroupsDAO.Save(model, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }
        public bool Delete(string codeGroup, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {
                retval = articalGroupsDAO.Delete(codeGroup, nameForm, idUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }


        public bool Update(ArticalGroupsModel model, string nameForm, int idUser)
        {
            bool retval = false;
            try
            {

                retval = articalGroupsDAO.Update(model, nameForm, idUser);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retval;
        }

        public ArticalGroupsModel GetArticalGroup(string group)
        {
            DataTable dataTable = new DataTable();
            dataTable = articalGroupsDAO.GetArticalGroup(group);
            ArticalGroupsModel arrange = new ArticalGroupsModel();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ArticalGroupsModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArticalGroupsModel();
                        if (dr["codeArticalGroup"].ToString() != "")
                            model.codeArticalGroup = dr["codeArticalGroup"].ToString();

                        if (dr["nameArticalGroup"].ToString() != "")
                            model.nameArticalGroup = dr["nameArticalGroup"].ToString();

                        if (dr["inkopArtical"].ToString() != "")
                            model.inkopArtical = dr["inkopArtical"].ToString();

                        if (dr["descInkopArtical"].ToString() != "")
                            model.descInkopArtical = dr["descInkopArtical"].ToString();

                        if (dr["verkopArtical"].ToString() != "")
                            model.verkopArtical = dr["verkopArtical"].ToString();

                        if (dr["descVerkopArtical"].ToString() != "")
                            model.descVerkopArtical = dr["descVerkopArtical"].ToString();

                        if (dr["isActive"].ToString() != "")
                            model.isActive = Convert.ToBoolean(dr["isActive"].ToString());

                        if (dr["idUserCreated"].ToString() != "")
                            model.idUserCreated = Convert.ToInt32(dr["idUserCreated"].ToString());

                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();

                        if (dr["dtUserCreated"].ToString() != "")
                            model.dtUserCreated = Convert.ToDateTime(dr["dtUserCreated"].ToString());

                        if (dr["idUserModified"].ToString() != "")
                            model.idUserModified = Convert.ToInt32(dr["idUserModified"].ToString());

                        if (dr["nameUserModified"].ToString() != "")
                            model.nameUserModified = dr["nameUserModified"].ToString();

                        if (dr["dtUserModified"].ToString() != "")
                            model.dtUserModified = Convert.ToDateTime(dr["dtUserModified"].ToString());

                        if (dr["classArticalGroup"].ToString() != "")
                            model.classArticalGroup = dr["classArticalGroup"].ToString();

                        //arrange.Add(model);
                    }
                    return model;

                }
                else
                    return null;
            }
            else
                return null;
        }
        // nova lista provera radi brisanja Artikla
        public List<ArticalGroupsModel> GetCodeFromArtical(string codeArticle)
        {
            DataTable dataTable = new DataTable();
            dataTable = articalGroupsDAO.GetCodeFromArtical(codeArticle);
            List<ArticalGroupsModel> arrange = new List<ArticalGroupsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    ArticalGroupsModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new ArticalGroupsModel();
                        if (dr["codeArticalGroup"].ToString() != "")
                            model.codeArticalGroup = dr["codeArticalGroup"].ToString();


                        arrange.Add(model);
                    }
                    return arrange;

                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}