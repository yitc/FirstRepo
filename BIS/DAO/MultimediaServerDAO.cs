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
   public class MultimediaServerDAO
    {
        private dbConnection conn;

        public MultimediaServerDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllMultimediaServer()
        {
            string query = string.Format(@"SELECT idServer, path, folder, username,password
                                          FROM MultimediaServer");

            return conn.executeSelectQuery(query, null);
        }

     

        public bool Insert(int idServer,string path, string folder,string username,string password)
        {

            string query = string.Format(@" SET IDENTITY_INSERT MultimediaServer ON
                                INSERT INTO MultimediaServer (idServer,path,folder, username,password)   
                        VALUES(@idServer,@path,@folder,@username,@password)
                                SET IDENTITY_INSERT MultimediaServer  OFF");


            SqlParameter[] sqlParameters = new SqlParameter[5];

            sqlParameters[0] = new SqlParameter("@idServer", SqlDbType.Int);
            sqlParameters[0].Value = idServer;

            sqlParameters[1] = new SqlParameter("@folder", SqlDbType.NVarChar);
            sqlParameters[1].Value = folder;

            sqlParameters[2] = new SqlParameter("@path", SqlDbType.NVarChar);
            sqlParameters[2].Value = path;

            sqlParameters[3] = new SqlParameter("@username", SqlDbType.NVarChar);
            sqlParameters[3].Value = username;

            sqlParameters[4] = new SqlParameter("@password", SqlDbType.NVarChar);
            sqlParameters[4].Value = password;

           


            return conn.executeInsertQuery(query, sqlParameters);

        }

        public bool Update(int idServer, string path, string folder, string username, string password)
        {

            string query = string.Format(@"UPDATE MultimediaServer SET 
                    path = @path, folder = @folder, 
                    username = @username,  password = @password
                    WHERE idServer = @idServer");
         
            SqlParameter[] sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@idServer", SqlDbType.Int);
            sqlParameters[0].Value = idServer;

            sqlParameters[1] = new SqlParameter("@path", SqlDbType.NVarChar);
            sqlParameters[1].Value = path;

            sqlParameters[2] = new SqlParameter("@folder", SqlDbType.NVarChar);
            sqlParameters[2].Value = folder;

            sqlParameters[3] = new SqlParameter("@username", SqlDbType.NVarChar);
            sqlParameters[3].Value = username;

            sqlParameters[4] = new SqlParameter("@password", SqlDbType.NVarChar);
            sqlParameters[4].Value = password;

            return conn.executeUpdateQuery(query, sqlParameters);

        }

        public bool Delete(int idServer)
        {
            string query = string.Format(@"DELETE FROM  MultimediaServer WHERE idServer = @idServer");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idServer", SqlDbType.Int);
            sqlParameters[0].Value = idServer;

            return conn.executeDeleteQuery(query, sqlParameters);
        }
        public DataTable GetLastIdServer()
        {
            string query = string.Format(
               @"SELECT Top 1 idServer, path, folder, username, password
                  FROM MultimediaServer order by  idServer desc");

            return conn.executeSelectQuery(query, null);

        }

        public DataTable AllServerId()
        {
            string query = string.Format(
               @"SELECT distinct idServer, null As path, null as folder, null as username, null as password
                  FROM Multimedia ");

            return conn.executeSelectQuery(query, null);

        }
        // DELETE NOVO

        public DataTable checkIsInMultimedia(int idServer)
        {
            string query = string.Format(@"
                SELECT * FROM Multimedia 
                WHERE idServer = '" + idServer + "'");

            return conn.executeSelectQuery(query, null);
        }

        public Boolean DeleteMultimediaServerSript(int idServer)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  MultimediaServer WHERE idServer = @idServer");

            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idServer", SqlDbType.Int);
            sqlParameter[0].Value = idServer;

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

    }
}
