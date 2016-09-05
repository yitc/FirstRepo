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
    public class LayoutDAO
    {
        private dbConnection conn;

        public LayoutDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllLayouts()
        {
            string query = string.Format(@"SELECT idLayout,nameLayout,typeDocument,languageLayout,fileLayout,bookmark,userCreated,dtCreted
                                        ,userModified,dtModified 
                                        FROM Layout");

            return conn.executeSelectQuery(query, null);
        }

        public Int32 GetLayoutID(string sNameLayout)
        {
            string query = string.Format(@"SELECT idLayout FROM Layout WHERE nameLayout = @nameLayout");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@nameLayout", SqlDbType.VarChar);
            sqlParameters[0].Value = sNameLayout;

            DataTable dt = conn.executeSelectQuery(query, sqlParameters);
            if (dt != null)
                if (dt.Rows.Count > 0)
                    return Int32.Parse(dt.Rows[0][0].ToString());

            return -1;
        }

    }
}

