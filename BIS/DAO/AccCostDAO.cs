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
    public class AccCostDAO
    {
        private dbConnection conn;

        public AccCostDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllCost()
        {
            string query = string.Format(@"SELECT idCost,codeCost,descCost,userCreated,dtCreated,userModified,dtModified FROM AccCost");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetCostByID(string idCost)
        {
            string query = string.Format(@"SELECT idCost,codeCost, descCost,userCreated,dtCreated,userModified,dtModified  FROM AccCost WHERE codeCost = @idCost");


            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idCost", SqlDbType.NVarChar);
            sqlParameters[0].Value = idCost;

            return conn.executeSelectQuery(query, sqlParameters);
          // return descCost;
        }
//       

        public Boolean Save(AccCostModel model, string nameForm, int idUser)    
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();


            string query = string.Format(@"INSERT INTO AccCost (codeCost,descCost,userCreated,dtCreated,userModified,dtModified ) 
                      VALUES(@codeCost, @descCost,@userCreated,@dtCreated,@userModified,@dtModified )");


            SqlParameter[] sqlParameter = new SqlParameter[6];
            sqlParameter[0] = new SqlParameter("@codeCost", SqlDbType.NVarChar);
            sqlParameter[0].Value = model.codeCost;

            sqlParameter[1] = new SqlParameter("@descCost", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.descCost;

            sqlParameter[2] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[2].Value = model.userCreated;

            sqlParameter[3] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[3].Value = DateTime.Now; //model.dtCreated;

            sqlParameter[4] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[4].Value = model.userModified;

            sqlParameter[5] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[5].Value = model.dtModified;



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
            sqlParameter[4].Value = model.codeCost;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "codeCost";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccCost";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean Update(AccCostModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE AccCost SET codeCost=@codeCost, descCost = @descCost,userCreated=@userCreated,dtCreated=@dtCreated,
                                        userModified=@userModified,dtModified=@dtModified
                                        WHERE  idCost=@idCost ");
                

            SqlParameter[] sqlParameter = new SqlParameter[7];

            sqlParameter[0] = new SqlParameter("@idCost", SqlDbType.Int);
            sqlParameter[0].Value = model.idCost;

            sqlParameter[1] = new SqlParameter("@codeCost", SqlDbType.NVarChar);
            sqlParameter[1].Value = model.codeCost;

            sqlParameter[2] = new SqlParameter("@descCost", SqlDbType.NVarChar);
            sqlParameter[2].Value = model.descCost;

            sqlParameter[3] = new SqlParameter("@userCreated", SqlDbType.Int);
            sqlParameter[3].Value = model.userCreated;

            sqlParameter[4] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[4].Value = model.dtCreated;

            sqlParameter[5] = new SqlParameter("@userModified", SqlDbType.Int);
            sqlParameter[5].Value = model.userModified;

            sqlParameter[6] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[6].Value = DateTime.Now; //model.dtModified;

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
            sqlParameter[4].Value = model.codeCost;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "codeCost";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccCost";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean Delete(string id, string nameForm, int idUser)   
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@" DELETE FROM AccCost  WHERE codeCost = @id ");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[0].Value = id;

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
            sqlParameter[4].Value = id;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "codeCost";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "AccCost";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
    
    }


}