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
    public class ArrangementBookStatusDAO
    {
        private dbConnection conn;
        public ArrangementBookStatusDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetAllStatus(string idLang)
        {
            string query = string.Format(@"SELECT idStatus,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE nameStatus END AS nameStatus
              FROM ArrangementBookStatus abs
              LEFT OUTER JOIN STRING" + idLang + " s On s.stringKey = abs.nameStatus ");
               // WHERE idContPers = '" + idPerson.ToString() + "' 

            return conn.executeSelectQuery(query, null);

        }

        public DataTable GetAllArrangementBookStatus()
        {
            string query = string.Format(@"SELECT idStatus,nameStatus FROM ArrangementBookStatus
                                            union
                                            select -1 as idstatus,'' as nameStatus");

            return conn.executeSelectQuery(query, null);
        }

    }
}