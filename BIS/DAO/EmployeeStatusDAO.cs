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
    public class EmployeeStatusDAO
    {
        private dbConnection conn;
        public EmployeeStatusDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetAllEmployeeStatus(string idLang)
        {
            string query = string.Format(@"SELECT idStatusEmployee,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE descriptionEmployee END AS descriptionEmployee
              FROM EmployeeStatus
              LEFT OUTER JOIN STRING"+idLang+" s ON s.stringKey = descriptionEmployee");
            // WHERE idContPers = '" + idPerson.ToString() + "' 

            return conn.executeSelectQuery(query, null);

        }

    }
}