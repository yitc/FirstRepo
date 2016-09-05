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
    public class PathsDAO
    {
        private dbConnection conn;

        public PathsDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllPaths()
        {
            string query = string.Format(@"SELECT idPath, typePath, namePath, path FROM Paths");

            return conn.executeSelectQuery(query, null);
        }
        
        public DataTable GetAllPathsByID(string idPath)
        {
            string query = string.Format(@"SELECT idPath, typePath, namePath, path FROM Paths
                                            WHERE idPath=@idPath");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idPath", SqlDbType.Int);
            sqlParameter[0].Value = idPath;

            return conn.executeSelectQuery(query, sqlParameter);
        }

        public DataTable GetPathsByType(string type)
        {
            string query = string.Format(@"SELECT idPath, typePath, namePath, path FROM Paths
                                            WHERE typePath=@typePath");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@typePath", SqlDbType.NVarChar);
            sqlParameter[0].Value = type;

            return conn.executeSelectQuery(query, sqlParameter);
        }

//        public Boolean Save(PathsModel model, string nameForm,int idUser,int selectLabel)
//        {
//            List<string> _query = new List<string>();
//            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

//            string query = string.Format(@"INSERT INTO Paths (namePath,path,idPath)
//                                        VALUES (@namePath,@path)");

//            SqlParameter[] sqlParameter = new SqlParameter[2];

//            sqlParameter[0] = new SqlParameter("@namePath", SqlDbType.NVarChar);
//            sqlParameter[0].Value = model.namePath;

//            sqlParameter[1] = new SqlParameter("@path", SqlDbType.NVarChar);
//            sqlParameter[1].Value = model.path;
           

//            _query.Add(query);
//            sqlParameters.Add(sqlParameter);

//            query = string.Format(@"INSERT INTO Log(textForm,idUser,dtLog,typeChange,id,nameId)
//                                    VALUES(@textForm,@idUser,@dtLog,@typeChange,@id,@nameId)");

//            sqlParameter = new SqlParameter[6];

//            sqlParameter[0] = new SqlParameter("@textForm", SqlDbType.NVarChar);
//            sqlParameter[0].Value = nameForm;

//            sqlParameter[1] = new SqlParameter("@idUser", SqlDbType.Int);
//            sqlParameter[1].Value = idUser;

//            sqlParameter[2] = new SqlParameter("@dtLog", SqlDbType.DateTime);
//            sqlParameter[2].Value = DateTime.Now;

//            sqlParameter[3] = new SqlParameter("@typeChange", SqlDbType.NVarChar);
//            sqlParameter[3].Value = "I";

//            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
//            sqlParameter[4].Value = conn.GetLastTableID("Paths") + 1;

//            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
//            sqlParameter[5].Value = "idPath";

//            _query.Add(query);
//            sqlParameters.Add(sqlParameter);

//            return conn.executQueryTransaction(_query, sqlParameters);

//        }

        public Boolean Update(string namePath, string nameForm, int idUser,int selLabel)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE Paths SET path = @namePath
                                           WHERE idPath = @idPath");

            SqlParameter[] sqlParameter = new SqlParameter[2];

            sqlParameter[0] = new SqlParameter("@idPath", SqlDbType.Int);
            sqlParameter[0].Value = selLabel;

            sqlParameter[1] = new SqlParameter("@namePath", SqlDbType.NVarChar);
            sqlParameter[1].Value = namePath;


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
            sqlParameter[4].Value = selLabel;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPath";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Paths";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
    }
}