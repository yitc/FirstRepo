using BIS.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.DAO
{
   public class MenuPictureDAO
    {
        private dbConnection conn;

        public MenuPictureDAO()
        {
            conn = new dbConnection();
        }
        public object GetImage(int idMenu)
        {
            string query = string.Format(@"
                SELECT imageMenu 
                FROM Menu
                WHERE idMenu = @idMenu");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idMenu", SqlDbType.Int);
            sqlParameters[0].Value = idMenu;
            return conn.executeScalarQuery(query, sqlParameters);
        }

        public object GetImageNew(int idMenu)
        {
            string query = string.Format(@"
                SELECT imageNew 
                FROM Menu
                WHERE idMenu = @idMenu");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idMenu", SqlDbType.Int);
            sqlParameters[0].Value = idMenu;
            return conn.executeScalarQuery(query, sqlParameters);
        }

        public object GetImageDelete(int idMenu)
        {
            string query = string.Format(@"
                SELECT imageDelete 
                FROM Menu
                WHERE idMenu = @idMenu");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idMenu", SqlDbType.Int);
            sqlParameters[0].Value = idMenu;
            return conn.executeScalarQuery(query, sqlParameters);
        }

        public Boolean UpdateImage(int idMenu,string imageMenu, string imageNew, string imageDelete, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE Menu
                SET imageMenu = @imageMenu, imageNew = @imageNew, 
                imageDelete = @imageDelete
	        	WHERE idMenu = @idMenu");

            SqlParameter[] sqlParameter = new SqlParameter[4];
            sqlParameter[0] = new SqlParameter("@idMenu", SqlDbType.Int);
            sqlParameter[0].Value = idMenu;

            sqlParameter[1] = new SqlParameter("@imageMenu", SqlDbType.NVarChar);
            sqlParameter[1].Value = (imageMenu == "") ? SqlString.Null : imageMenu;

          //  sqlParameters[1].Value = imageMenu;
            sqlParameter[2] = new SqlParameter("@imageNew", SqlDbType.NVarChar);
            sqlParameter[2].Value = (imageNew == "") ? SqlString.Null : imageNew;
                
            sqlParameter[3] = new SqlParameter("@imageDelete", SqlDbType.NVarChar);
            sqlParameter[3].Value = (imageDelete == "") ? SqlString.Null : imageDelete;

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
            sqlParameter[4].Value = idMenu;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idMenu";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Menu";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update image";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
    }
}
