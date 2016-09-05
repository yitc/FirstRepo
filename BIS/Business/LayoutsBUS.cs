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
    public class LayoutsBUS
    {
        private LayoutsDAO layoutDAO;

        public LayoutsBUS()
        {
            layoutDAO = new LayoutsDAO();
        }

        public List<LayoutsModel> GetAllLayouts()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = layoutDAO.GetAllLayouts();
                List<LayoutsModel> layout = new List<LayoutsModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dataTable.Rows)
                        {
                            LayoutsModel model = new LayoutsModel();

                            model.idLayout = Int32.Parse(dr["idLayout"].ToString());
                            model.nameLayout = dr["nameLayout"].ToString();
                            model.typeDocument = dr["typeDocument"].ToString();
                            model.languageLayout = dr["languageLayout"].ToString();
                            
                            model.bookmarks = dr["bookmarks"].ToString();
                            model.templateTable = dr["templateTable"].ToString(); 

                            model.fileLayout = dr["fileLayout"].ToString();
                          
                            if (dr["dtCreted"].ToString() != "")
                                model.dtCreated= DateTime.Parse(dr["dtCreted"].ToString());

                            if (dr["dtModified"].ToString() != "")
                                model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                            if (dr["userModified"].ToString() != "")
                                model.userModified = Int32.Parse(dr["userModified"].ToString());
                            if (dr["userCreated"].ToString() != "")
                                model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                            if (dr["nameUserModified"].ToString() != "")
                                model.nameUserModified = dr["nameUserModified"].ToString();

                            if (dr["nameUserCreated"].ToString() != "")
                                model.nameUserCreated = dr["nameUserCreated"].ToString();

                            layout.Add(model);
                        }
                        return layout;
                    }
                    else
                        return layout;
                }
                else
                    return layout;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<IModel> GetAllLayoutsbyTemplateTable(string templatetable)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = layoutDAO.GetAllLayoutsbyTemplateTable(templatetable);
                List<IModel> layout = new List<IModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dataTable.Rows)
                        {
                            LayoutsModel model = new LayoutsModel();

                            model.idLayout = Int32.Parse(dr["idLayout"].ToString());
                            model.nameLayout = dr["nameLayout"].ToString();
                            model.typeDocument = dr["typeDocument"].ToString();
                            model.languageLayout = dr["languageLayout"].ToString();

                            model.bookmarks = dr["bookmarks"].ToString();
                            model.templateTable = dr["templateTable"].ToString(); 

                            model.fileLayout = dr["fileLayout"].ToString();

                            if (dr["dtCreted"].ToString() != "")
                                model.dtCreated = DateTime.Parse(dr["dtCreted"].ToString());

                            if (dr["dtModified"].ToString() != "")
                                model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                            if (dr["userModified"].ToString() != "")
                                model.userModified = Int32.Parse(dr["userModified"].ToString());
                            if (dr["userCreated"].ToString() != "")
                                model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                            if (dr["nameUserModified"].ToString() != "")
                                model.nameUserModified = dr["nameUserModified"].ToString();

                            if (dr["nameUserCreated"].ToString() != "")
                                model.nameUserCreated = dr["nameUserCreated"].ToString();

                            layout.Add(model);
                        }
                        return layout;
                    }
                    else
                        return layout;
                }
                else
                    return layout;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<IModel> GetAllLayoutsAsIMODEL()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = layoutDAO.GetAllLayouts();
                List<IModel> layout = new List<IModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dataTable.Rows)
                        {
                            LayoutsModel model = new LayoutsModel();

                            model.idLayout = Int32.Parse(dr["idLayout"].ToString());
                            model.nameLayout = dr["nameLayout"].ToString();
                            model.typeDocument = dr["typeDocument"].ToString();
                            model.languageLayout = dr["languageLayout"].ToString();

                            model.bookmarks = dr["bookmarks"].ToString();
                            model.templateTable = dr["templateTable"].ToString(); 
                            model.fileLayout = dr["fileLayout"].ToString();

                            if (dr["dtCreted"].ToString() != "")
                                model.dtCreated = DateTime.Parse(dr["dtCreted"].ToString());

                            if (dr["dtModified"].ToString() != "")
                                model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                            if (dr["userModified"].ToString() != "")
                                model.userModified = Int32.Parse(dr["userModified"].ToString());
                            if (dr["userCreated"].ToString() != "")
                                model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                            if (dr["nameUserModified"].ToString() != "")
                                model.nameUserModified = dr["nameUserModified"].ToString();

                            if (dr["nameUserCreated"].ToString() != "")
                                model.nameUserCreated = dr["nameUserCreated"].ToString();

                            layout.Add(model);
                        }
                        return layout;
                    }
                    else
                        return layout;
                }
                else
                    return layout;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    
        
        
        public bool SaveLayout(LayoutsModel model, string nameForm, int idUser)
        {
            bool retval = false;

            try
            {
               retval = layoutDAO.SaveLayout(model, nameForm, idUser);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }
        public bool DeleteLayoutByID(int ID, string nameForm, int idUser)
        {
            bool retval = false;

            try
            {
                retval = layoutDAO.DeleteLayoutByID(ID, nameForm, idUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }
        public bool UpdateLayout(LayoutsModel model, string nameForm, int idUser)
        {
            bool retval = false;

            try
            {
                retval = layoutDAO.UpdateLayout(model, nameForm, idUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return retval;
        }
       
    }
}
