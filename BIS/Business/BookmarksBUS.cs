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
    public class BookmarksBUS
    {
        private BookmarksDAO bookmarkDAO;

        public BookmarksBUS()
        {
            bookmarkDAO = new BookmarksDAO();
        }

        public List<BookmarksModel> GetBookmarks()
        {
            DataTable dataTable = new DataTable();
            dataTable = bookmarkDAO.GetBookmarks();
            List<BookmarksModel> bookmarks = new List<BookmarksModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    BookmarksModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new BookmarksModel();

                        
                        model.idField = Int32.Parse(dr["idField"].ToString());

                        if (dr["idBookmark"].ToString() != "")
                         model.idBookmark = Guid.Parse(dr["idBookmark"].ToString());

                        model.nameBookmark = dr["nameBookmark"].ToString();
                        model.tableName = dr["tableName"].ToString();
                        model.fieldName = dr["fieldName"].ToString();
                        model.fieldValue = dr["fieldValue"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                         model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["userModifiel"].ToString() != "")
                            model.userModifiel = Int32.Parse(dr["userModifiel"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());

                        if (dr["nameUserModified"].ToString() != "")
                            model.nameUserModified = dr["nameUserModified"].ToString();

                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();
                     
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
    
        public List<BookmarksModel> GetBookmarksById(int id)
        {
            DataTable dataTable = new DataTable();
            dataTable = bookmarkDAO.GetBookmarksById(id);
            List<BookmarksModel> bookmarks = new List<BookmarksModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    BookmarksModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new BookmarksModel();


                        model.idField = Int32.Parse(dr["idField"].ToString());

                        if (dr["idBookmark"].ToString() != "")
                            model.idBookmark = Guid.Parse(dr["idBookmark"].ToString());

                        model.nameBookmark = dr["nameBookmark"].ToString();
                        model.tableName = dr["tableName"].ToString();
                        model.fieldName = dr["fieldName"].ToString();
                        model.fieldValue = dr["fieldValue"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["userModifiel"].ToString() != "")
                            model.userModifiel = Int32.Parse(dr["userModifiel"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());

                        if (dr["nameUserModified"].ToString() != "")
                            model.nameUserModified = dr["nameUserModified"].ToString();

                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();

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
        
        public List<BookmarksModel> GetBookmarksByBookmarkId (Guid? id)
        {
            DataTable dataTable = new DataTable();
            dataTable = bookmarkDAO.GetBookmarksByBookmarkId(id);
            List<BookmarksModel> bookmarks = new List<BookmarksModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    BookmarksModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new BookmarksModel();


                        model.idField = Int32.Parse(dr["idField"].ToString());

                        if (dr["idBookmark"].ToString() != "")
                            model.idBookmark = Guid.Parse(dr["idBookmark"].ToString());

                        model.nameBookmark = dr["nameBookmark"].ToString();
                        model.tableName = dr["tableName"].ToString();
                        model.fieldName = dr["fieldName"].ToString();
                        model.fieldBookmark = dr["fieldBookmark"].ToString();
                        model.fieldValue = dr["fieldValue"].ToString();

                        if (dr["dtCreated"].ToString() != "")
                            model.dtCreated = DateTime.Parse(dr["dtCreated"].ToString());

                        if (dr["dtModified"].ToString() != "")
                            model.dtModified = DateTime.Parse(dr["dtModified"].ToString());

                        if (dr["userModifiel"].ToString() != "")
                            model.userModifiel = Int32.Parse(dr["userModifiel"].ToString());

                        if (dr["userCreated"].ToString() != "")
                            model.userCreated = Int32.Parse(dr["userCreated"].ToString());

                        if (dr["nameUserModified"].ToString() != "")
                            model.nameUserModified = dr["nameUserModified"].ToString();

                        if (dr["nameUserCreated"].ToString() != "")
                            model.nameUserCreated = dr["nameUserCreated"].ToString();

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
    
        public object CustomPersonQuery(string tableName, string fieldName, int id, string idFieldName)
        {
            try
            {
                return bookmarkDAO.CustomPersonQuery(tableName, fieldName, id, idFieldName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public object CustomClientQuery(string tableName, string fieldName, int id, string idFieldName)
        {
            try
            {
                return bookmarkDAO.CustomClientQuery(tableName, fieldName, id, idFieldName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void SaveBookmarks(List<BookmarksModel> arrayToSave)
        {
            try
            {
                bookmarkDAO.SaveBookmars(arrayToSave);

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteBookmarksByBookmarkID(Guid id)
        {
            try
            {
                bookmarkDAO.DeleteBookmarksByBookmarkID(id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
