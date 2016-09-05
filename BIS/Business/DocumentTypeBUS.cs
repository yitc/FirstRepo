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
    public class DocumentTypeBUS
    {
        private DocumentTypeDAO DocumentTypeDAO;

        public DocumentTypeBUS()
        {
            DocumentTypeDAO = new DocumentTypeDAO();
        }

        public List<IModel> GetALLDocumentTypesCombo()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = DocumentTypeDAO.GetAllDocumentTypes();
                List<IModel> types = new List<IModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            DocumentTypeModel model = new DocumentTypeModel();

                            model.idDocumentType = Int32.Parse(dr["idDocumentType"].ToString());
                            model.typeDocument = dr["typeDocument"].ToString();
                            model.nameDocumentType = dr["nameDocumentType"].ToString();
                            model.extendDocumentType = dr["extendDocumentType"].ToString();
                            model.haveLayout = Boolean.Parse(dr["haveLayout"].ToString());
                            model.tableDocumentType = dr["tableDocumentType"].ToString();
                            model.defaultBookmark = dr["defaultBookmark"].ToString();
                            if (dr["dtCreted"].ToString() != "")
                                model.dtCreted = DateTime.Parse(dr["dtCreted"].ToString());
                            if (dr["dtModified"].ToString() != "")
                                model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                            if (dr["idModifiedUser"].ToString() != "")
                                model.idModifiedUser = Int32.Parse(dr["idModifiedUser"].ToString());
                            if (dr["idCreatedUser"].ToString() != "")
                                model.idCreatedUser = Int32.Parse(dr["idCreatedUser"].ToString());
                            if (dr["nameUserCreated"].ToString() != "")
                                model.nameUserCreated = dr["nameUserCreated"].ToString();
                            if (dr["nameUserModified"].ToString() != "")
                                model.nameUserModified = dr["nameUserModified"].ToString();


                            types.Add(model);
                        }
                        return types;
                    }
                    else
                        return types;
                }
                else
                    return types;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<DocumentTypeModel> GetALLDocumentTypes()
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable = DocumentTypeDAO.GetAllDocumentTypes();
                List<DocumentTypeModel> types = new List<DocumentTypeModel>();

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            DocumentTypeModel model = new DocumentTypeModel();

                            model.idDocumentType = Int32.Parse(dr["idDocumentType"].ToString());
                            model.typeDocument = dr["typeDocument"].ToString();
                            model.nameDocumentType = dr["nameDocumentType"].ToString();
                            model.extendDocumentType = dr["extendDocumentType"].ToString();
                            model.haveLayout = Boolean.Parse(dr["haveLayout"].ToString());
                            model.tableDocumentType = dr["tableDocumentType"].ToString();
                            model.defaultBookmark = dr["defaultBookmark"].ToString();
                            if (dr["dtCreted"].ToString() != "")
                                model.dtCreted = DateTime.Parse(dr["dtCreted"].ToString());
                            if (dr["dtModified"].ToString() != "")
                                model.dtModified = DateTime.Parse(dr["dtModified"].ToString());
                            if (dr["idModifiedUser"].ToString() != "")
                                model.idModifiedUser = Int32.Parse(dr["idModifiedUser"].ToString());
                            if (dr["idCreatedUser"].ToString() != "")
                                model.idCreatedUser = Int32.Parse(dr["idCreatedUser"].ToString());


                            types.Add(model);
                        }
                        return types;
                    }
                    else
                        return types;
                }
                else
                    return types;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
