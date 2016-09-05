using BIS.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.DAO
{
   public class ProvincesDAO
    {
        private dbConnection conn;
        public ProvincesDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllProvinces( int idCountry)
        {
            string query = string.Format(@"SELECT p.idProvinces, p.codeProvinces, p.nameProvinces, c.nameCountry as nameCountry
                                        FROM Provinces p
                                        LEFT OUTER JOIN Country c on p.idCountry=c.idCountry
                                        WHERE p.idCountry=@idCountry");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idCountry", SqlDbType.Int);
            sqlParameters[0].Value = idCountry;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public bool Insert(int idProvinces,string codeProvinces, int idCountry, string nameProvinces, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO 
                                   Provinces(idProvinces,codeProvinces,idCountry, nameProvinces)
                                    VALUES(@idProvinces,@codeProvinces,@idCountry, @nameProvinces)");


            SqlParameter[] sqlParameter = new SqlParameter[4];

            sqlParameter[0] = new SqlParameter("@idProvinces", SqlDbType.Int);
            sqlParameter[0].Value = Convert.ToInt32(idProvinces);

            sqlParameter[1] = new SqlParameter("@codeProvinces", SqlDbType.NVarChar);
            sqlParameter[1].Value = Convert.ToString(codeProvinces.Trim());

            sqlParameter[2] = new SqlParameter("@idCountry", SqlDbType.Int);
            sqlParameter[2].Value = Convert.ToInt32(idCountry);

            sqlParameter[3] = new SqlParameter("@nameProvinces", SqlDbType.NVarChar);
            sqlParameter[3].Value = Convert.ToString(nameProvinces.Trim());


            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");


            sqlParameter = new SqlParameter[8];


            sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameForm;

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = idUser;

            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "I";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = idProvinces;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idProvinces";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Provinces";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Insert provinces";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public bool Delete( int idProvinces, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  Provinces
            WHERE idProvinces =@idProvinces");


            SqlParameter[] sqlParameter = new SqlParameter[1];


            sqlParameter[0] = new SqlParameter("@idProvinces", SqlDbType.Int);
            sqlParameter[0].Value = Convert.ToInt32(idProvinces);


            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");


            sqlParameter = new SqlParameter[8];


            sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameForm;

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = idUser;

            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "D";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = idProvinces;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idProvinces";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Provinces";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete provinces";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public DataTable idProvinces()
        {
            string query = string.Format(@"SELECT TOP 1 idProvinces FROM  Provinces ORDER BY idProvinces DESC");

            return conn.executeSelectQuery(query, null);
        }

        public bool Update(int idProvinces, string codeProvinces, string nameProvinces, string nameForm, int idUser)
        {

            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE         
                                 Provinces SET codeProvinces=@codeProvinces,nameProvinces=@nameProvinces
                                    WHERE idProvinces=@idProvinces");

            SqlParameter[] sqlParameter = new SqlParameter[3];


            sqlParameter[0] = new SqlParameter("@idProvinces", SqlDbType.Int);
            sqlParameter[0].Value = idProvinces;

            sqlParameter[1] = new SqlParameter("@codeProvinces", SqlDbType.NVarChar);
            sqlParameter[1].Value = codeProvinces;

            sqlParameter[2] = new SqlParameter("@nameProvinces", SqlDbType.NVarChar);
            sqlParameter[2].Value = nameProvinces;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId,tableName,description)
                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId,@tableName,@description)");


            sqlParameter = new SqlParameter[8];


            sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
            sqlParameter[0].Value = nameForm;

            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameter[1].Value = idUser;

            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
            sqlParameter[2].Value = DateTime.Now;

            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
            sqlParameter[3].Value = "U";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = idProvinces;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idProvinces";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Provinces";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update provinces";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
    }
}
