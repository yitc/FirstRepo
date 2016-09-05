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
    public class FiltersDAO
    {
        private dbConnection conn;

        public FiltersDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetPersonFilters(string language)
        {
            string query = string.Format(
                @" SELECT idFilter, CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE nameFilter END nameFilter,sortFilter FROM
                    (SELECT idFilter, nameFilter,sortFilter
                    from Filters
                    UNION
                    SELECT idFilter, nameFilter,sortFilter
                    from Filters WHERE idFilter IN (SELECT distinct idFilter
                    FROM ContactPersonFilter)) F
                    LEFT OUTER JOIN STRING"+language+"  S on RTRIM(LTRIM(F.nameFilter)) = RTRIM(LTRIM(S.stringKey))");
         //  WHERE idFilter = 0   
            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetClientFilters(string language)
        {
            string query = string.Format(
                @"SELECT idFilter, CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE nameFilter END nameFilter,sortFilter FROM
                    (SELECT idFilter, nameFilter,sortFilter
                    from Filters WHERE idFilter = 0
                    UNION
                    SELECT idFilter, nameFilter,sortFilter
                    from Filters WHERE idFilter IN (SELECT distinct idFilter
                    FROM ClientFilter)) F
                    LEFT OUTER JOIN STRING" + language + "  S on RTRIM(LTRIM(F.nameFilter)) = RTRIM(LTRIM(S.stringKey))");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetEmployeeFilters(string language)
        {
            string query = string.Format(
                @"SELECT idFilter, CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE nameFilter END nameFilter,sortFilter FROM
                    (SELECT idFilter, nameFilter,sortFilter
                    from Filters WHERE idFilter = 0
                    UNION
                    SELECT idFilter, nameFilter,sortFilter
                    from Filters WHERE idFilter IN (SELECT distinct idFilter
                    FROM EmployeeFilter)) F
                    LEFT OUTER JOIN STRING" + language + "  S on RTRIM(LTRIM(F.nameFilter)) = RTRIM(LTRIM(S.stringKey))");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetUsersFilters(string language)
        {
            string query = string.Format(
                @"SELECT idFilter, CASE WHEN S.stringKey IS NOT NULL THEN S.stringValue ELSE nameFilter END nameFilter,sortFilter FROM
                    (SELECT idFilter, nameFilter,sortFilter
                    from Filters WHERE idFilter = 0
                    UNION
                    SELECT idFilter, nameFilter,sortFilter
                    from Filters WHERE idFilter IN (SELECT distinct idFilter
                    FROM UsersFilter)) F
                    LEFT OUTER JOIN STRING" + language + "  S on RTRIM(LTRIM(F.nameFilter)) = RTRIM(LTRIM(S.stringKey))");

            return conn.executeSelectQuery(query, null);
        }

        public Boolean insertFilters(FilterForPerson filter, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

                string query = string.Format(@"INSERT INTO ContactPersonFilter (idContPers, idFilter) 
                      VALUES(@idContPers, @idFilter)");


                SqlParameter[] sqlParameter = new SqlParameter[2];

                sqlParameter[0] = new SqlParameter("@idContPers", SqlDbType.Int);
                sqlParameter[0].Value = filter.idContPers;

                sqlParameter[1] = new SqlParameter("@idFilter", SqlDbType.Int);
                sqlParameter[1].Value = filter.idFilter;


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
                sqlParameter[4].Value = filter.idFilter.ToString();

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idFilter";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "ContactPersonFilter";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "insert filter";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);


                return conn.executQueryTransaction(_query, sqlParameters);

        }
        public Boolean deleteFilters(int idContPers, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM ContactPersonFilter WHERE idContPers = @idContPers ");


                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@idContPers", SqlDbType.Int);
                sqlParameter[0].Value = idContPers;


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
                sqlParameter[4].Value = idContPers;

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idContPers";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "ContactPersonFilter";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "delete filter";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);


                return conn.executQueryTransaction(_query, sqlParameters);

        }
    }
}
