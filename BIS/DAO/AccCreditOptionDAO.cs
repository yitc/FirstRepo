using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;
using BIS.Model;

namespace BIS.DAO
{
    public class AccCreditOptionDAO
    {
        private dbConnection conn;
        public AccCreditOptionDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllOptions()
        {
            string query = string.Format(@"SELECT idOption, descriptionOption FROM AccCreditOption");

            return conn.executeSelectQuery(query, null);
        }
    }
}