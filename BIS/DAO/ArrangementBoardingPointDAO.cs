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
    public class ArrangementBoardingPointDAO
    {
        private dbConnection conn;

        public ArrangementBoardingPointDAO()
        {
            conn = new dbConnection();
        }
        public DataTable GetArrangementBoardingPoint(int idArrangement)
        {
            string query = string.Format(@"SELECT bp.idBoardingPoint, nameBoardingPoint, addressBoardingPoint, a.departure, a.arrivel, a.sortBoardingPoint 
                FROM BoardingPoint bp
                INNER JOIN ArrangementBoardingPoint a ON bp.idBoardingPoint = a.idBoardingPoint WHERE a.idArrangement = @idArrangement order by sortBoardingPoint");


//            string query = string.Format(@"SELECT bp.idBoardingPoint, nameBoardingPoint, a.departure, a.arrivel, a.sortBoardingPoint 
//                FROM BoardingPoint bp
//                INNER JOIN ArrangementBoardingPoint a ON bp.idBoardingPoint = a.idBoardingPoint WHERE a.idArrangement = @idArrangement order by nameBoardingPoint");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAllBoardingPoint(int idArrangement)
        {
            string query = string.Format(@"SELECT bp.idBoardingPoint, nameBoardingPoint, addressBoardingPoint,departure,arrivel,sortBoardingPoint
                FROM BoardingPoint bp 
                LEFT OUTER JOIN (SELECT idBoardingPoint,departure,arrivel,sortBoardingPoint FROM ArrangementBoardingPoint WHERE idArrangement = @idArrangement) a ON bp.idBoardingPoint = a.idBoardingPoint order by nameBoardingPoint");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetBoardingPointName(int idBoardingPoint )
        {
            string query = string.Format(@"SELECT bp.idBoardingPoint, nameBoardingPoint
                FROM BoardingPoint bp WHERE idBoardingPoint = '" + idBoardingPoint + "'");


            return conn.executeSelectQuery(query, null);
        }

        public Boolean Save(ArrangementBoardingPointModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO ArrangementBoardingPoint (idBoardingPoint, idArrangement, departure, arrivel, sortBoardingPoint) 
                      VALUES(@idBoardingPoint, @idArrangement, @departure, @arrivel, @sortBoardingPoint)");


            SqlParameter[] sqlParameter = new SqlParameter[5];

            sqlParameter[0] = new SqlParameter("@idBoardingPoint", SqlDbType.Int);
            sqlParameter[0].Value = model.idBoardingPoint;

            sqlParameter[1] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[1].Value = model.idArrangement;

            sqlParameter[2] = new SqlParameter("@departure", SqlDbType.DateTime);
            sqlParameter[2].Value = model.dtDeparture;

            sqlParameter[3] = new SqlParameter("@arrivel", SqlDbType.DateTime);
            sqlParameter[3].Value = model.dtArrival;

            sqlParameter[4] = new SqlParameter("@sortBoardingPoint", SqlDbType.Int);
            sqlParameter[4].Value = model.sortBoardingPoint;


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
            sqlParameter[4].Value = model.idBoardingPoint;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idBoardingPoint";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementBoardingPoint";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean Delete(int idArrangement, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM ArrangementBoardingPoint WHERE idArrangement=@idArrangement ");


            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[0].Value = idArrangement;

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
            sqlParameter[3].Value = "DI";

            sqlParameter[4] = new SqlParameter("@id", SqlDbType.NVarChar);
            sqlParameter[4].Value = idArrangement;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangement";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementBoardingPoint";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
    }
}
