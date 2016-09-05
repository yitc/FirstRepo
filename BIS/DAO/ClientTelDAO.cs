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
    class ClientTelDAO
    {
        private dbConnection conn;

        public ClientTelDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllClientTels(Int32 idClient)
        {
            string query = string.Format(@"SELECT idTel, idClient,numberTel,idDefaultTel,descriptionTel
                                             ,ct.idTelType,tt.nameTelType
                                              FROM ClientTel ct
                                              LEFT OUTER JOIN TypesTel tt
                                              ON ct.idTelType = tt.idTelType
                                              WHERE 
                                               idClient='" + idClient + "'");
            //    (RTRIM(LTRIM(descriptionTel))<>'fax' OR descriptionTel IS NULL) AND
            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllClientFaxes(Int32 idClient)
        {
            string query = string.Format(@"SELECT idTel, idClient,numberTel,idDefaultTel,descriptionTel
                                             ,ct.idTelType,tt.nameTelType
                                              FROM ClientTel ct
                                              LEFT OUTER JOIN TypesTel tt
                                              ON ct.idTelType = tt.idTelType
                                              WHERE RTRIM(LTRIM(descriptionTel))='fax'
                                              AND idClient='" + idClient + "'");

            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetClientTelsByType(int idTelType, int idclient)
        {
            string query = string.Format(@"SELECT idTel,idClient,numberTel,isDefaultTel,descriptionTel,idTelType
              FROM ClientTel 
                WHERE idTelType = '" + idTelType + "' AND idClient = '" + idclient + "'");

            return conn.executeSelectQuery(query, null);

        }
        public bool Save(ClientTelModel tel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO ClientTel (idClient, numberTel, idDefaultTel, descriptionTel, idTelType) 
                      VALUES(@idClient, @numberTel, @idDefaultTel, @descriptionTel, @idTelType) ");


            SqlParameter[] sqlParameter = new SqlParameter[5];

            sqlParameter[0] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[0].Value = tel.idClient;

            sqlParameter[1] = new SqlParameter("@numberTel", SqlDbType.NVarChar);
            sqlParameter[1].Value = (tel.numberTel == null) ? SqlString.Null : tel.numberTel;

            sqlParameter[2] = new SqlParameter("@idDefaultTel", SqlDbType.Bit);
            sqlParameter[2].Value = tel.idDefaultTel;

            sqlParameter[3] = new SqlParameter("@descriptionTel", SqlDbType.NVarChar);
            sqlParameter[3].Value = (tel.descriptionTel == null) ? SqlString.Null : tel.descriptionTel;

            sqlParameter[4] = new SqlParameter("@idTelType", SqlDbType.Int);
            sqlParameter[4].Value = (tel.idTelType == null) ? 0 : tel.idTelType;


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
            sqlParameter[4].Value = conn.GetLastTableID("ClientTel")+1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idTel";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ClientTel";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool Update(ClientTelModel tel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string querySelect = string.Format(@"SELECT idTel, idClient,numberTel,idDefaultTel,descriptionTel,idTelType
              FROM ClientTel
                WHERE idTel = '" + tel.idTel.ToString() + "'");

            DataTable dt = conn.executeSelectQuery(querySelect, null);

            if (dt != null && dt.Rows.Count > 0)
            {
                

                string query = string.Format(@"UPDATE ClientTel SET 
                idClient = @idClient, numberTel = @numberTel, idDefaultTel = @idDefaultTel, descriptionTel = @descriptionTel,
                idTelType = @idTelType 
                WHERE idTel = @idTel ");


                SqlParameter[] sqlParameter = new SqlParameter[6];

                sqlParameter[0] = new SqlParameter("@idClient", SqlDbType.Int);
                sqlParameter[0].Value = tel.idClient;

                sqlParameter[1] = new SqlParameter("@numberTel", SqlDbType.NVarChar);
                sqlParameter[1].Value = (tel.numberTel == null) ? SqlString.Null : tel.numberTel;

                sqlParameter[2] = new SqlParameter("@idDefaultTel", SqlDbType.Bit);
                sqlParameter[2].Value = tel.idDefaultTel;

                sqlParameter[3] = new SqlParameter("@descriptionTel", SqlDbType.NVarChar);
                sqlParameter[3].Value = (tel.descriptionTel == null) ? SqlString.Null : tel.descriptionTel;

                sqlParameter[4] = new SqlParameter("@idTelType", SqlDbType.Int);
                sqlParameter[4].Value = (tel.idTelType == null) ? 0 : tel.idTelType;

                sqlParameter[5] = new SqlParameter("@idTel", SqlDbType.Int);
                sqlParameter[5].Value = tel.idTel;

                
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
                sqlParameter[4].Value = tel.idTel;

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "idTel";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "ClientTel";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Update";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);


                return conn.executQueryTransaction(_query, sqlParameters);
            }
            else
            {
                return Save(tel, nameForm, idUser);
            }


        }

        public bool Delete(int idTel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  ClientTel WHERE idTel = @idTel");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idTel", SqlDbType.Int);
            sqlParameter[0].Value = idTel;

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
            sqlParameter[4].Value = idTel;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idTel";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ClientTel";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
    }
}
