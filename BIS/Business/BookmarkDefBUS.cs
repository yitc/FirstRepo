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

    public class BookmarkDefBUS
    {
        private BookmarkDefDAO bookmarkDAO;

        public BookmarkDefBUS()
        {
            bookmarkDAO = new BookmarkDefDAO();
        }

        public List<BookmarkDefModel> GetBookmarks()
        {
            DataTable dataTable = new DataTable();
            dataTable = bookmarkDAO.GetBookmarks();
            List<BookmarkDefModel> bookmarks = new List<BookmarkDefModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    BookmarkDefModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new BookmarkDefModel();

                        model.Id = Int32.Parse(dr["Id"].ToString());
                        model.tableDisplayName = dr["tableDisplayName"].ToString();
                        model.tableName = dr["tableName"].ToString();
                        model.fieldBookmark = dr["fieldBookmark"].ToString();
                        model.nameBookmark = dr["nameBookmark"].ToString();
                        model.displayNameBookmark = dr["displayNameBookmark"].ToString();

                        if (dr["isRelationBmk"].ToString() != "")
                            model.isRelationBmk = Boolean.Parse(dr["isRelationBmk"].ToString());

                        model.relationTableName = dr["relationTableName"].ToString();
                        model.relationFieldName = dr["relationFieldName"].ToString();

                        if (dr["hasType"].ToString() != "")
                            model.hasType = Boolean.Parse(dr["hasType"].ToString());

                        model.typeTableName = dr["typeTableName"].ToString();
                        model.typeFieldName = dr["typeFieldName"].ToString();

                        if (dr["typeFieldValue"].ToString() != "")
                            model.typeFieldValue = Int32.Parse(dr["typeFieldValue"].ToString());

                        if (dr["isActive"].ToString() != "")
                            model.isActive = Boolean.Parse(dr["isActive"].ToString());

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());

                        bookmarks.Add(model);
                    }

                    return bookmarks;
                }
                else
                    return null;
            }
            else
                return null;
        }


        public BookmarkDefModel GetBookmarkByID(int id)
        {
            DataTable dataTable = new DataTable();
            dataTable = bookmarkDAO.GetBookmarkByID(id);
            
            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    BookmarkDefModel model = new BookmarkDefModel(); 

                    foreach (DataRow dr in dataTable.Rows)
                    {                        
                        model.Id = Int32.Parse(dr["Id"].ToString());
                        model.tableDisplayName = dr["tableDisplayName"].ToString();
                        model.tableName = dr["tableName"].ToString();
                        model.fieldBookmark = dr["fieldBookmark"].ToString();
                        model.nameBookmark = dr["nameBookmark"].ToString();
                        model.displayNameBookmark = dr["displayNameBookmark"].ToString();

                        if (dr["isRelationBmk"].ToString() != "")
                            model.isRelationBmk = Boolean.Parse(dr["isRelationBmk"].ToString());

                        model.relationTableName = dr["relationTableName"].ToString();
                        model.relationFieldName = dr["relationFieldName"].ToString();

                        if (dr["hasType"].ToString() != "")
                            model.hasType = Boolean.Parse(dr["hasType"].ToString());

                        model.typeTableName = dr["typeTableName"].ToString();
                        model.typeFieldName = dr["typeFieldName"].ToString();

                        if (dr["typeFieldValue"].ToString() != "")
                            model.typeFieldValue = Int32.Parse(dr["typeFieldValue"].ToString());

                        if (dr["isActive"].ToString() != "")
                            model.isActive = Boolean.Parse(dr["isActive"].ToString());

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());                        
                    }

                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public List<BookmarkDefModel> GetBookmarksByTableName(string tablename)
        {
            DataTable dataTable = new DataTable();
            dataTable = bookmarkDAO.GetBookmarksByTableName(tablename);
            List<BookmarkDefModel> bookmarks = new List<BookmarkDefModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    BookmarkDefModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new BookmarkDefModel();

                        model.Id = Int32.Parse(dr["Id"].ToString());
                        model.tableDisplayName = dr["tableDisplayName"].ToString();
                        model.tableName = dr["tableName"].ToString();
                        model.fieldBookmark = dr["fieldBookmark"].ToString();
                        model.nameBookmark = dr["nameBookmark"].ToString();
                        model.displayNameBookmark = dr["displayNameBookmark"].ToString();

                        if (dr["isRelationBmk"].ToString() != "")
                            model.isRelationBmk = Boolean.Parse(dr["isRelationBmk"].ToString());

                        model.relationTableName = dr["relationTableName"].ToString();
                        model.relationFieldName = dr["relationFieldName"].ToString();

                        if (dr["hasType"].ToString() != "")
                            model.hasType = Boolean.Parse(dr["hasType"].ToString());

                        model.typeTableName = dr["typeTableName"].ToString();
                        model.typeFieldName = dr["typeFieldName"].ToString();

                        if (dr["typeFieldValue"].ToString() != "")
                            model.typeFieldValue = Int32.Parse(dr["typeFieldValue"].ToString());

                        if (dr["isActive"].ToString() != "")
                            model.isActive = Boolean.Parse(dr["isActive"].ToString());

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());

                        bookmarks.Add(model);
                    }

                    return bookmarks;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public BookmarkDefModel GetBookmarksByNameBookmark(string namebookmark)
        {
            DataTable dataTable = new DataTable();
            dataTable = bookmarkDAO.GetBookmarksByNameBookmark(namebookmark);            

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    BookmarkDefModel model = new BookmarkDefModel(); 

                    foreach (DataRow dr in dataTable.Rows)
                    {                        
                        model.Id = Int32.Parse(dr["Id"].ToString());
                        model.tableDisplayName = dr["tableDisplayName"].ToString();
                        model.tableName = dr["tableName"].ToString();
                        model.fieldBookmark = dr["fieldBookmark"].ToString();
                        model.nameBookmark = dr["nameBookmark"].ToString();
                        model.displayNameBookmark = dr["displayNameBookmark"].ToString();

                        if (dr["isRelationBmk"].ToString() != "")
                            model.isRelationBmk = Boolean.Parse(dr["isRelationBmk"].ToString());

                        model.relationTableName = dr["relationTableName"].ToString();
                        model.relationFieldName = dr["relationFieldName"].ToString();

                        if (dr["hasType"].ToString() != "")
                            model.hasType = Boolean.Parse(dr["hasType"].ToString());

                        model.typeTableName = dr["typeTableName"].ToString();
                        model.typeFieldName = dr["typeFieldName"].ToString();

                        if (dr["typeFieldValue"].ToString() != "")
                            model.typeFieldValue = Int32.Parse(dr["typeFieldValue"].ToString());

                        if (dr["isActive"].ToString() != "")
                            model.isActive = Boolean.Parse(dr["isActive"].ToString());

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["userModified"].ToString() != "")
                            model.userModified = Int32.Parse(dr["userModified"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());                        
                    }

                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }


        public List<BookmarkDefModel> GetBookmarksByDistinctTableName()
        {
            DataTable dataTable = new DataTable();
            dataTable = bookmarkDAO.GetBookmarksByDistinctTableName();
            List<BookmarkDefModel> bookmarks = new List<BookmarkDefModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    BookmarkDefModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new BookmarkDefModel();

                        //model.Id = Int32.Parse(dr["Id"].ToString());
                        model.tableDisplayName = dr["tableDisplayName"].ToString();
                        model.tableName = dr["tableName"].ToString();
                        //model.fieldBookmark = dr["fieldBookmark"].ToString();
                        //model.nameBookmark = dr["nameBookmark"].ToString();
                        //model.displayNameBookmark = dr["displayNameBookmark"].ToString();

                        //if (dr["isRelationBmk"].ToString() != "")
                        //    model.isRelationBmk = Boolean.Parse(dr["isRelationBmk"].ToString());

                        //model.relationTableName = dr["relationTableName"].ToString();
                        //model.relationFieldName = dr["relationFieldName"].ToString();

                        //if (dr["hasType"].ToString() != "")
                        //    model.hasType = Boolean.Parse(dr["hasType"].ToString());

                        //model.typeTableName = dr["typeTableName"].ToString();
                        //model.typeFieldName = dr["typeFieldName"].ToString();

                        //if (dr["typeFieldValue"].ToString() != "")
                        //    model.typeFieldValue = Int32.Parse(dr["typeFieldValue"].ToString());

                        //if (dr["isActive"].ToString() != "")
                        //    model.isActive = Boolean.Parse(dr["isActive"].ToString());

                        //if (dr["dtCreated"].ToString() != "")
                        //    model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        //if (dr["dtModified"].ToString() != "")
                        //    model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        //if (dr["userModified"].ToString() != "")
                        //    model.userModified = Int32.Parse(dr["userModified"].ToString());

                        //if (dr["userCreated"].ToString() != "")
                        //    model.userCreated = Int32.Parse(dr["userCreated"].ToString());
                        
                        bookmarks.Add(model);
                    }

                    return bookmarks;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public object CustomBookmarksQuery(string tableName, string fieldName, string idFieldName, int value, string typeField, int? typevalue)
        {
            try
            {
                return bookmarkDAO.CustomBookmarksQuery(tableName, fieldName, idFieldName, value, typeField, typevalue);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public object CustomBookmarksQueryForTypes(string tableName, string fieldName, string idFieldName, int value, string typeField, int? typevalue, string parentTable, string parentID)
        {
            try
            {
                return bookmarkDAO.CustomBookmarksQueryForTypes(tableName, fieldName, idFieldName, value, typeField, typevalue, parentTable, parentID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<BookmarkTableModel> GetTableBookmarks()
        {
            DataTable dataTable = new DataTable();
            dataTable = bookmarkDAO.GetTableBookmarks();
            List<BookmarkTableModel> bookmarks = new List<BookmarkTableModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    BookmarkTableModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new BookmarkTableModel();

                        model.idTable = Int32.Parse(dr["idTable"].ToString());
                        model.nameTable = dr["nameTable"].ToString();
                        model.displayNameTable = dr["displayNameTable"].ToString();

                        if (dr["active"].ToString() != "")
                            model.active = Boolean.Parse(dr["active"].ToString());

                        if (dr["fontSizeFields"].ToString() != "")
                            model.fontSizeFields = Int32.Parse(dr["fontSizeFields"].ToString());

                        if (dr["leftMargin"].ToString() != "")
                            model.leftMargin = float.Parse(dr["leftMargin"].ToString());

                        if (dr["rightMargin"].ToString() != "")
                            model.rightMargin = float.Parse(dr["rightMargin"].ToString());
                        
                        bookmarks.Add(model);
                    }

                    return bookmarks;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public BookmarkTableModel GetTableByName(string tablename)
        {
            DataTable dataTable = new DataTable();
            dataTable = bookmarkDAO.GetTableByName(tablename);            

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    BookmarkTableModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new BookmarkTableModel();

                        model.idTable = Int32.Parse(dr["idTable"].ToString());
                        model.nameTable = dr["nameTable"].ToString();
                        model.displayNameTable = dr["displayNameTable"].ToString();

                        if (dr["active"].ToString() != "")
                            model.active = Boolean.Parse(dr["active"].ToString());

                        if (dr["fontSizeFields"].ToString() != "")
                            model.fontSizeFields = Int32.Parse(dr["fontSizeFields"].ToString());

                        if (dr["leftMargin"].ToString() != "")
                            model.leftMargin = float.Parse(dr["leftMargin"].ToString());

                        if (dr["rightMargin"].ToString() != "")
                            model.rightMargin = float.Parse(dr["rightMargin"].ToString());
                        
                    }

                    return model;
                }
                else
                    return null;
            }
            else
                return null;
        }


        public List<BookmarkTableFieldsModel> GetTableBookmarksFields(int idtable)
        {
            DataTable dataTable = new DataTable();
            dataTable = bookmarkDAO.GetTableBookmarksFields(idtable);
            List<BookmarkTableFieldsModel> bookmarks = new List<BookmarkTableFieldsModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    BookmarkTableFieldsModel model = null;

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new BookmarkTableFieldsModel();

                        model.nameField = dr["nameField"].ToString();

                        if (dr["widthField"].ToString() != "")
                            model.widthField = float.Parse(dr["widthField"].ToString());

                        model.displayNameField = dr["displayNameField"].ToString();

                        if (dr["visible"].ToString() != "")
                            model.visible = Boolean.Parse(dr["visible"].ToString());

                        if (dr["isTotal"].ToString() != "")
                            model.isTotal = Boolean.Parse(dr["isTotal"].ToString());

                        bookmarks.Add(model);
                    }

                    return bookmarks;
                }
                else
                    return null;
            }
            else
                return null;
        }
    
    }
}
