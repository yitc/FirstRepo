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
  public class ThemeTripDAO
    {
        private dbConnection conn;

        public ThemeTripDAO()
        {
            conn = new dbConnection();
        }
        public bool Save(int idThemeTrip, string nameThemeTrip, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO ThemeTrip (idThemeTrip,nameThemeTrip)
                      VALUES(@idThemeTrip, @nameThemeTrip)");


            SqlParameter[] sqlParameter = new SqlParameter[2];

            sqlParameter[0] = new SqlParameter("@idThemeTrip", SqlDbType.Int);
            sqlParameter[0].Value = idThemeTrip;

            sqlParameter[1] = new SqlParameter("@nameThemeTrip", SqlDbType.NVarChar);
            sqlParameter[1].Value = nameThemeTrip;

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
            sqlParameter[4].Value = idThemeTrip.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idThemeTrip";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ThemeTrip";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save theme trip";


            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
        public DataTable idThemeTrip()
        {
            string query = string.Format(@"SELECT TOP 1 idThemeTrip FROM  ThemeTrip ORDER BY idThemeTrip DESC");

            return conn.executeSelectQuery(query, null);
        }


        public DataTable GetAllThemeTrip()
        {
            string query = string.Format(@" SELECT '0' AS idThemeTrip, '' AS nameThemetrip
                 UNION
                 SELECT idThemeTrip, nameThemetrip
                FROM ThemeTrip Order by nameThemetrip");


            return conn.executeSelectQuery(query, null);
        }

        public bool Delete(int idThemeTrip, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
               
            string query = string.Format(@"DELETE FROM  ThemeTrip WHERE idThemeTrip=@idThemeTrip");


            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idThemeTrip", SqlDbType.Int);
            sqlParameter[0].Value = idThemeTrip;

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
            sqlParameter[4].Value = idThemeTrip;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idThemeTrip";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ThemeTrip";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete theme trip";


            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool Update(int idThemeTrip, string nameThemeTrip, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE ThemeTrip SET   nameThemeTrip=@nameThemeTrip
                                    WHERE idThemeTrip =@idThemeTrip");


            SqlParameter[] sqlParameter = new SqlParameter[2];

            sqlParameter[0] = new SqlParameter("@idThemeTrip", SqlDbType.Int);
            sqlParameter[0].Value = idThemeTrip;

            sqlParameter[1] = new SqlParameter("@nameThemeTrip", SqlDbType.NVarChar);
            sqlParameter[1].Value = nameThemeTrip;

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
            sqlParameter[4].Value = idThemeTrip;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idThemeTrip";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ThemeTrip";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update theme trip";


            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
        // NOVO

        public DataTable isInTheme(int idThemeTrip)
        {
            string query = string.Format(@"SELECT idThemeTrip,'' as nameThemeTrip
                                           FROM ArrangementThemeTrip WHERE idThemeTrip=@idThemeTrip");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idThemeTrip", SqlDbType.Int);
            sqlParameters[0].Value = idThemeTrip;

            return conn.executeSelectQuery(query, sqlParameters);
        }

    }
}
