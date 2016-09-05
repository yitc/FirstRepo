using BIS.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BIS.DAO
{
    public class GenderDAO
    {

         private dbConnection conn;
         public GenderDAO()
        {
            conn = new dbConnection();
  
        }

        public DataTable GetGenders(string language)
        {
            string query = string.Format(@"SELECT G.idGender,CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE G.nameGender END nameGender
              FROM Gender G LEFT OUTER JOIN STRING" + language + @"  S on RTRIM(LTRIM(G.nameGender)) = RTRIM(LTRIM(S.stringKey)) ");

         return conn.executeSelectQuery(query, null);
        
        }  
    }
}
