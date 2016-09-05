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
  public class BoardingPointDAO
    {
        private dbConnection conn;

        public BoardingPointDAO()
        {
            conn = new dbConnection();
        }
        public bool Save(int idBoardingPoint, string nameBoardingPoint, string addressBoardingPoint, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO BoardingPoint (idBoardingPoint,nameBoardingPoint, addressBoardingPoint)
                      VALUES(@idBoardingPoint, @nameBoardingPoint, @addressBoardingPoint)");


            SqlParameter[] sqlParameter = new SqlParameter[3];

            sqlParameter[0] = new SqlParameter("@idBoardingPoint", SqlDbType.Int);
            sqlParameter[0].Value = idBoardingPoint;

            sqlParameter[1] = new SqlParameter("@nameBoardingPoint", SqlDbType.NVarChar);
            sqlParameter[1].Value = nameBoardingPoint;


            sqlParameter[2] = new SqlParameter("@addressBoardingPoint", SqlDbType.NVarChar);
            sqlParameter[2].Value = addressBoardingPoint;

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
            sqlParameter[4].Value = idBoardingPoint.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idBoardingPoint";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "BoardingPoint";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
        public DataTable idBoardingPoint()
        {
            string query = string.Format(@"SELECT TOP 1 idBoardingPoint FROM  BoardingPoint ORDER BY idBoardingPoint DESC");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetAll()
        {
            string query = string.Format(@"SELECT idBoardingPoint,nameBoardingPoint, addressBoardingPoint
                                           FROM BoardingPoint");

            return conn.executeSelectQuery(query, null);
        }

        public bool Delete(int idBoardingPoint, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  BoardingPoint WHERE idBoardingPoint=@idBoardingPoint");


            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idBoardingPoint", SqlDbType.Int);
            sqlParameter[0].Value = idBoardingPoint;

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
            sqlParameter[4].Value = idBoardingPoint.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idBoardingPoint";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "BoardingPoint";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool Update(int idBoardingPoint, string nameBoardingPoint, string addressBoardingPoint, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE BoardingPoint SET   nameBoardingPoint=@nameBoardingPoint, addressBoardingPoint=@addressBoardingPoint
                                    WHERE idBoardingPoint =@idBoardingPoint");


            SqlParameter[] sqlParameter = new SqlParameter[3];

            sqlParameter[0] = new SqlParameter("@idBoardingPoint", SqlDbType.Int);
            sqlParameter[0].Value = idBoardingPoint;

            sqlParameter[1] = new SqlParameter("@nameBoardingPoint", SqlDbType.NVarChar);
            sqlParameter[1].Value = nameBoardingPoint;

            sqlParameter[2] = new SqlParameter("@addressBoardingPoint", SqlDbType.NVarChar);
            sqlParameter[2].Value = addressBoardingPoint;


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
            sqlParameter[4].Value = idBoardingPoint.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idBoardingPoint";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "BoardingPoint";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);


        }

      // NOVO

        public DataTable isInBoarding(int idBoardingPoint)
        {
            string query = string.Format(@"SELECT idBoardingPoint,'' as nameBoardingPoint
                                           FROM ArrangementBoardingPoint WHERE idBoardingPoint=@idBoardingPoint");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idBoardingPoint", SqlDbType.Int);
            sqlParameters[0].Value = idBoardingPoint;

            return conn.executeSelectQuery(query, sqlParameters);
        }
    }
}
