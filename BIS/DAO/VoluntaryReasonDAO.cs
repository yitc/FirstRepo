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
    // za IN =====================================================================================================
    public class VoluntaryReasonInDAO
    {

        private dbConnection conn;
        public VoluntaryReasonInDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetAllReasonIn()
        {
            string query = string.Format(@"SELECT idReasonIn, nameReasonIn FROM VoluntaryReasonIn ");

            return conn.executeSelectQuery(query, null);

        }

        //Za IN lookup 13 6 2016

        public DataTable GetAllReasonInLookup()
        {
            string query = string.Format(@"SELECT idReasonIn, nameReasonIn FROM VoluntaryReasonIn ");

            return conn.executeSelectQuery(query, null);
        }


    }

    // za OUT ===============================================================================================================
    public class VoluntaryReasonOutDAO
    {

        private dbConnection conn;
        public VoluntaryReasonOutDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetAllReasonOut()
        {
            string query = string.Format(@"SELECT idReasonOut, nameReasonOut FROM VoluntaryReasonOut ");

            return conn.executeSelectQuery(query, null);

        }



    }
}