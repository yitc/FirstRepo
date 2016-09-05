using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;

namespace BIS.DAO
{
    public class BookmarkDAO
    {
        private dbConnection conn;

        public BookmarkDAO()
        {
            conn = new dbConnection();
        }


        public DataTable GetBookmark(Int32 idbmk)
        {
            string query = string.Format(@"
            select idbmk, idstrtbl, tbnamebmk, tablebmk, fieldbmk, namebmk, capbmk, idstrbmk, isidbmk, isfidbmk, reltypbmk, ftablebmk, idftbbmk, islytbmk, aliasbmk, gblrepbmk, dtc, dtm, mus, cus
            from bookmark 
            where idbmk = @idbmk");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idbmk", SqlDbType.Int);
            sqlParameters[0].Value = idbmk;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetBookmarksSpec(string sTemplate)
        {
            string query = string.Format(@"
            select tablebmk [table], fieldbmk [field], '' [value]
            from bookmark 
            where tbnamebmk = @template");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@template", SqlDbType.VarChar);
            sqlParameters[0].Value = sTemplate;

            return conn.executeSelectQuery(query, sqlParameters);
        }


        public string GetEmailID(string sVal)
        {
            string query = string.Format(@"select ID from sifEmail where dsc = @val");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@val", SqlDbType.VarChar);
            sqlParameters[0].Value = sVal;

            DataTable dt = conn.executeSelectQuery(query, sqlParameters);
            if (dt != null)
                if (dt.Rows.Count > 0)
                    return dt.Rows[0][0].ToString();
            return "-1";
        }

        public string GetTelID(string sVal)
        {
            string query = string.Format(@"select ID from sifTel where dsc = @val");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@val", SqlDbType.VarChar);
            sqlParameters[0].Value = sVal;

            DataTable dt = conn.executeSelectQuery(query, sqlParameters);
            if (dt != null)
                if (dt.Rows.Count > 0)
                    return dt.Rows[0][0].ToString();
            return "-1";
        }


    }
}
