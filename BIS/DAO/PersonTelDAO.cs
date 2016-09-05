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
    public class PersonTelDAO
    {
        private dbConnection conn;
        public PersonTelDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetPersonTels(int idPerson)
        {
            string query = string.Format(@"SELECT idTel,idContPers,numberTel,isDefaultTel,descriptionTel,idTelType
              FROM ContactPersonTel 
                WHERE idContPers = '" + idPerson.ToString() + "' ");

            return conn.executeSelectQuery(query, null);

        }

        public DataTable GetAllPersonTels()
        {
            string query = string.Format(@"SELECT idTel,idContPers,numberTel,isDefaultTel,descriptionTel,idTelType
              FROM ContactPersonTel 
                 ");

            return conn.executeSelectQuery(query, null);

        }
        public DataTable GetPersonTelsByType(int idTelType, int idContPers)
        {
            string query = string.Format(@"SELECT idTel,idContPers,numberTel,isDefaultTel,descriptionTel,idTelType
              FROM ContactPersonTel 
                WHERE idTelType = '" + idTelType + "' AND idContPers = '" + idContPers + "'");

            return conn.executeSelectQuery(query, null);

        }
        public bool Save(PersonTelModel tel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

                     string query = string.Format(@"INSERT INTO ContactPersonTel (idContPers, numberTel, isDefaultTel, descriptionTel, idTelType) 
                      VALUES(@idContPers, @numberTel, @isDefaultTel, @descriptionTel, @idTelType)");


                     SqlParameter[] sqlParameter = new SqlParameter[5];

                     sqlParameter[0] = new SqlParameter("@idContPers", SqlDbType.Int);
                     sqlParameter[0].Value = tel.idContPers;

                     sqlParameter[1] = new SqlParameter("@numberTel", SqlDbType.NVarChar);
                     sqlParameter[1].Value = (tel.numberTel == null) ? SqlString.Null : tel.numberTel;

                     sqlParameter[2] = new SqlParameter("@isDefaultTel", SqlDbType.Bit);
                     sqlParameter[2].Value = tel.isDefaultTel;

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
                     sqlParameter[4].Value = conn.GetLastTableID("ContactPersonTel") + 1;

                     sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                     sqlParameter[5].Value = "idTel";

                     sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                     sqlParameter[6].Value = "ContactPersonTel";

                     sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                     sqlParameter[7].Value = "Save contact person tel";

                     _query.Add(query);
                     sqlParameters.Add(sqlParameter);


                     return conn.executQueryTransaction(_query, sqlParameters);

        }

        public bool Update(PersonTelModel tel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string querySelect = string.Format(@"SELECT idContPers,numberTel,isDefaultTel,descriptionTel,idTelType
              FROM ContactPersonTel
                WHERE idTel = '" + tel.idTel.ToString() + "'");

            DataTable dt = conn.executeSelectQuery(querySelect, null);

            if (dt != null && dt.Rows.Count > 0)
            {

                string query = string.Format(@"UPDATE ContactPersonTel SET 
                idContPers = @idContPers, numberTel = @numberTel, isDefaultTel = @isDefaultTel, descriptionTel = @descriptionTel,
                idTelType = @idTelType 
                WHERE idTel = @idTel ");


                SqlParameter[] sqlParameter = new SqlParameter[6];

                sqlParameter[0] = new SqlParameter("@idContPers", SqlDbType.Int);
                sqlParameter[0].Value = tel.idContPers;

                sqlParameter[1] = new SqlParameter("@numberTel", SqlDbType.NVarChar);
                sqlParameter[1].Value = (tel.numberTel == null) ? SqlString.Null : tel.numberTel;

                sqlParameter[2] = new SqlParameter("@isDefaultTel", SqlDbType.Bit);
                sqlParameter[2].Value = tel.isDefaultTel;

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
                sqlParameter[6].Value = "ContactPersonTel";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Update contact person tel";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);


                return conn.executQueryTransaction(_query, sqlParameters);
            }
            else
            {
                return Save(tel,nameForm,idUser);
            }


        }

        public bool Delete(int idTel, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();
            string query = string.Format(@"DELETE FROM  ContactPersonTel WHERE idTel = @idTel");

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
            sqlParameter[6].Value = "ContactPersonTel";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete contact person tel";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
    }
}