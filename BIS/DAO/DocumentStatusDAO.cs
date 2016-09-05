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
    public class DocumentStatusDAO
    {
        private dbConnection conn;

        public DocumentStatusDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetStatuses(string idLang)
        {
            string query = string.Format(@"SELECT idDocumentStatus, valueStatus, CASE WHEN std.StringValue IS NOT NULL THEN std.StringValue ELSE ds.descriptionStatus END AS descriptionStatus  FROM DocumentStatus ds
            LEFT JOIN String"+idLang+" std ON std.stringKey = ds.descriptionStatus");

            return conn.executeSelectQuery(query, null);
        }

    }
}
