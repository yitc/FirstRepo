using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;
using BIS.Model;
using System.Data.SqlTypes;

namespace BIS.DAO
{
    public class AccDailyTypeDAO
    {
        private dbConnection conn;
        public AccDailyTypeDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetAllTypes()
        {
            string query = string.Format(@" SELECT idDailyType,descDailyType FROM AccDailyType ");

            return conn.executeSelectQuery(query, null);
        }
    }
}