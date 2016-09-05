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
    public class ArrangementBookTravelPapersDAO
    {
        private dbConnection conn;
        public ArrangementBookTravelPapersDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetAllTravelPapers(string idLang)
        {
            string query = string.Format(@"SELECT idTravelPapers,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE nameTravelPapers END AS nameTravelPapers
              FROM ArrangementBookTravelPapers atbp
              LEFT OUTER JOIN STRING" + idLang + " s On s.stringKey = atbp.nameTravelPapers ");
               // WHERE idContPers = '" + idPerson.ToString() + "' 

            return conn.executeSelectQuery(query, null);

        }

    }
}