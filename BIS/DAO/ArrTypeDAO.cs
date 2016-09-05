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
    public class ArrTypeDAO
    {
        private dbConnection conn;

        public ArrTypeDAO()
        {
            conn = new dbConnection();
        }

        //public DataTable GetAllArrTypes()
        //{
        //    string query = string.Format(@"SELECT idArrType,nameArrType FROM ArrType");

        //    return conn.executeSelectQuery(query, null);
        //}

        public DataTable GetAllArrTypes()
        {
            string query = string.Format(@"SELECT idArrType,nameArrType FROM ArrType");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetArrTypeById(int idArrType)
        {
            string query = string.Format(@"SELECT idArrType,nameArrType FROM ArrType WHERE idArrType = @idArrType");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrType", SqlDbType.Int);
            sqlParameters[0].Value = idArrType;

         
            return conn.executeSelectQuery(query, sqlParameters);
        }
       
    }


}