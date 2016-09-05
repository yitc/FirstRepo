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
    public class AccDailyVerInDAO
    {
        private dbConnection conn;
        public AccDailyVerInDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetAllClass()
        {
            string query = string.Format(@" SELECT idDailyVerIn,nameDailyVerIn FROM AccDailyVerIn ");

            return conn.executeSelectQuery(query, null);
        }
    }
}