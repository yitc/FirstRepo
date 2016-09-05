using BIS.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.DAO
{
    public class ClientTypesDAO
    {
         private dbConnection conn;

         public ClientTypesDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllClientsTypes(string language)
        {
            string query = string.Format(@"SELECT idTypeClient,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE nameTypeClient END nameTypeClient
                    FROM ClientTypes CT
                    LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM(CT.nameTypeClient)) = RTRIM(LTRIM(S.stringKey))");

            return conn.executeSelectQuery(query, null);
        }
    }
}
