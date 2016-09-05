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
    public class LanguageDAO
    {
         private dbConnection conn;

         public LanguageDAO()
         {
             conn = new dbConnection();
         }

         public DataTable GetLanguageStrings(string idLangFromUsers)
         {
             
            
             string query = string.Format(
                     @"SELECT f.nameForm, stringKey, StringValue
                    FROM STRING" + idLangFromUsers + " s LEFT OUTER JOIN Forms f ON s.idForm = f.idForm");
             

             return conn.executeSelectQuery(query, null);
         }
    }


}
