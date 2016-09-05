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
    public class TypeNotesDAO
    {
        private dbConnection conn;

        public TypeNotesDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllTypeNotes(string idLang)
        {

            string query = string.Format(@"
                SELECT idTypeNote,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE nameTypeNote END AS nameTypeNote FROM TypesNote
                LEFT OUTER JOIN STRING"+idLang+" s ON s.stringKey =  nameTypeNote ");
            return conn.executeSelectQuery(query, null);

        }
    }
}
