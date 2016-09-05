using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BIS.Core;
using BIS.Model;
using System.Data.SqlTypes;

namespace BIS.DAO
{
    public class AgeCategoryDAO
    {
        private dbConnection conn;
        public AgeCategoryDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllAgeCategory()
        {
            string query = string.Format(@"SELECT idAgeCategory, descAgeCategory, minAgeCategory, maxAgeCategory 
                                           FROM AgeCategory ");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllAgeCategoryByID(string idAgeCategory)
        {
            string query = string.Format(@"SELECT idAgeCategory, descAgeCategory, minAgeCategory, maxAgeCategory
                                           FROM AgeCategory
                                        WHERE descAgeCategory=@idAgeCategory ");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idAgeCategory", SqlDbType.NVarChar);
            sqlParameters[0].Value = idAgeCategory;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public bool Save(AgeCategoryModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@" INSERT INTO AgeCategory
            (idAgeCategory,descAgeCategory, minAgeCategory, maxAgeCategory) VALUES (@idAgeCategory,@descAgeCategory, @minAgeCategory, @maxAgeCategory) ");

            SqlParameter[] sqlParameter = new SqlParameter[4];

            sqlParameter[0] = new SqlParameter("@idAgeCategory", SqlDbType.Int);
            sqlParameter[0].Value = model.idAgeCategory;

            sqlParameter[1] = new SqlParameter("@descAgeCategory", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.descAgeCategory;

            sqlParameter[2] = new SqlParameter("@minAgeCategory", SqlDbType.Int);
            sqlParameter[2].Value= model.minAgeCategory;         
           
            sqlParameter[3] = new SqlParameter("@maxAgeCategory", SqlDbType.Int);
            sqlParameter[3].Value = model.maxAgeCategory;

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
            sqlParameter[4].Value = model.idAgeCategory;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAgeCategory";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AgeCategory";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean Update(AgeCategoryModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE AgeCategory SET descAgeCategory = @descAgeCategory, minAgeCategory = @minAgeCategory, maxAgeCategory = @maxAgeCategory
                                        WHERE idAgeCategory = @idAgeCategory");

            SqlParameter[] sqlParameter = new SqlParameter[4];

            sqlParameter[0] = new SqlParameter("@idAgeCategory", SqlDbType.Int);
            sqlParameter[0].Value = model.idAgeCategory;

            sqlParameter[1] = new SqlParameter("@descAgeCategory", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.descAgeCategory;

            sqlParameter[2] = new SqlParameter("@minAgeCategory", SqlDbType.Int);
            sqlParameter[2].Value = model.minAgeCategory;

            sqlParameter[3] = new SqlParameter("@maxAgeCategory", SqlDbType.Int);
            sqlParameter[3].Value = model.maxAgeCategory;

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
            sqlParameter[4].Value = model.idAgeCategory;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAgeCategory";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AgeCategory";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean Delete(int idAgeCategory, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format("DELETE AgeCategory WHERE idAgeCategory = @idAgeCategory");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idAgeCategory", SqlDbType.Int);
            sqlParameter[0].Value = idAgeCategory;

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
            sqlParameter[4].Value = idAgeCategory;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAgeCategory";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AgeCategory";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
        public DataTable LastId()
        {
            string query = string.Format(@"SELECT top(1) idAgeCategory
                                   FROM AgeCategory  ORDER BY idAgeCategory DESC ");
          
            return conn.executeSelectQuery(query, null);
        
        }

        public DataTable isIn(int idAgeCategory)
        {
            string query = string.Format(@"SELECT distinct idAgeCategory as idAgeCategory
                                           FROM Arrangement WHERE idAgeCategory=@idAgeCategory");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idAgeCategory", SqlDbType.Int);
            sqlParameters[0].Value = idAgeCategory;

            return conn.executeSelectQuery(query, sqlParameters);
        }
        ///
        //NOVO DELETE:
        public DataTable checkIsInArrangemnet(int idAgeCategory)
        {
            string query = string.Format(@"
                SELECT * FROM Arrangement 
                WHERE idAgeCategory = '" + idAgeCategory + "'");

            return conn.executeSelectQuery(query, null);
        }
        public Boolean DeleteAgeCategoryScript(int idAgeCategory, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM AgeCategory WHERE idAgeCategory = @idAgeCategory ");

            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idAgeCategory", SqlDbType.Int);
            sqlParameter[0].Value = idAgeCategory;

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
            sqlParameter[4].Value = idAgeCategory;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idAgeCategory";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AgeCategory";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete age category script";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
    }
}