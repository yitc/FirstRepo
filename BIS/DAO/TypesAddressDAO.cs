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
    public class TypesAddressDAO
    {
        private dbConnection conn;
        public TypesAddressDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetTypeAddressById(int idType,string idLang)
        {

            string query = string.Format(@"SELECT idAddressType,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE nameAddressType END AS nameAddressType, showInControl FROM TypesAddress
                LEFT OUTER JOIN STRING" +idLang+@" s ON s.stringKey =  nameAddressType
                WHERE idAddressType = '" + idType.ToString() + "' ");

            return conn.executeSelectQuery(query, null);

        }
        public DataTable GetAllTypeAddress(string idLang)
        {

            string query = string.Format(@"SELECT idAddressType,CASE WHEN s.stringValue IS NOT NULL THEN s.stringValue ELSE nameAddressType END AS nameAddressType, showInControl FROM TypesAddress
                   LEFT OUTER JOIN STRING" +idLang+" s ON s.stringKey =  nameAddressType");
               
            return conn.executeSelectQuery(query, null);

        }
    }
}