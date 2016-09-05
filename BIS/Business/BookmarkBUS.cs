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
    public class BookmarkBUS
    {
        private BookmarkDAO bookmarkDAO;

        public BookmarkBUS()
        {
            bookmarkDAO = new BookmarkDAO();
        }

        public List<BookmarkModel> GetBookmarks(string sTemplate)
        {
            DataTable dataTable = new DataTable();
            dataTable = bookmarkDAO.GetBookmarksSpec(sTemplate);
            List<BookmarkModel> bookmarks = new List<BookmarkModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    BookmarkModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new BookmarkModel();

                        if (dr["idbmk"].ToString() != "")
                            model.idbmk = Int32.Parse(dr["idbmk"].ToString());

                        model.idstrtbl = dr["idstrtbl"].ToString();
                        model.tbnamebmk = dr["tbnamebmk"].ToString();
                        model.tablebmk = dr["tablebmk"].ToString();
                        model.fieldbmk = dr["fieldbmk"].ToString();
                        model.namebmk = dr["namebmk"].ToString();
                        model.capbmk = dr["capbmk"].ToString();
                        model.idstrbmk = dr["idstrbmk"].ToString();

                        model.isidbmk = Boolean.Parse(dr["isidbmk"].ToString());
                        model.isfidbmk = Boolean.Parse(dr["isfidbmk"].ToString());

                        if (dr["reltypbmk"].ToString() != "")
                            model.reltypbmk = Decimal.Parse(dr["reltypbmk"].ToString());

                        model.ftablebmk = dr["ftablebmk"].ToString();
                        model.idftbbmk = dr["idftbbmk"].ToString();

                        model.islytbmk = Boolean.Parse(dr["islytbmk"].ToString());

                        model.aliasbmk = dr["aliasbmk"].ToString();

                        model.gblrepbmk = Boolean.Parse(dr["gblrepbmk"].ToString());

                        if (dr["dtc"].ToString() != "")
                            model.dtc = DateTime.Parse(dr["dtc"].ToString());

                        if (dr["dtm"].ToString() != "")
                            model.dtm = DateTime.Parse(dr["dtm"].ToString());

                        model.mus = dr["mus"].ToString();
                        model.cus = dr["cus"].ToString();

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

        public List<BookmarkSpecModel> GetBookmarkSpec(string sTemplate)
        {
            DataTable dataTable = new DataTable();
            dataTable = bookmarkDAO.GetBookmarksSpec(sTemplate);
            List<BookmarkSpecModel> bookmarks = new List<BookmarkSpecModel>();

            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    BookmarkSpecModel model = null;
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        model = new BookmarkSpecModel();

                        model.value = "";
                        model.table = dr["table"].ToString().Trim();
                        model.field = dr["field"].ToString().Trim();

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
