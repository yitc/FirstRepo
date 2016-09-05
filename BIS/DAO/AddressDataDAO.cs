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
    // pretrezuje tabelu adressdata
    public class AddressDataDAO
    {
        private dbConnection conn;

        public AddressDataDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAddress(string letterAdress, int indicator, int districtAdress, int housenum)
        {
            string query = string.Format(
                @"SELECT * FROM addressdata WHERE letteradr = '" + letterAdress + "' AND indicatadr = '" + indicator + "' AND disctriadr = '" + districtAdress + "' AND firstadr <='" + housenum + "' AND lastadr >= '"+ housenum +"'");

            return conn.executeSelectQuery(query, null);
        }
    }
}
