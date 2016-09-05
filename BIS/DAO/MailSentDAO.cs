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
    public class MailSentDAO
    {
        private dbConnection conn;

        public MailSentDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetMyEmails(int id)
        {
            string query = string.Format(@"SELECT id, entryId, idUser, Subject, idPersonTo, idClientTo, locationOnDisk, dtSent 
                FROM MailSent WHERE idUser = @idUser");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameters[0].Value = id;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetEmailByID(Guid entryId)
        {
            string query = string.Format(@"SELECT id, entryId, idUser, Subject, idPersonTo, idClientTo, locationOnDisk, dtSent 
                FROM MailSent WHERE entryId = @entryId");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@entryId", SqlDbType.UniqueIdentifier);
            sqlParameters[0].Value = entryId;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public Boolean Save(MailSentModel model)
        {
            string query = string.Format(@"INSERT INTO MailSent (entryId, idUser, Subject, idPersonTo, idClientTo, locationOnDisk, dtSent) 
                      VALUES(@entryId, @idUser, @Subject, @idPersonTo, @idClientTo, @locationOnDisk, @dtSent)");


            SqlParameter[] sqlParameters = new SqlParameter[7];

            sqlParameters[0] = new SqlParameter("@entryId", SqlDbType.NVarChar);
            sqlParameters[0].Value = model.entryId;

            sqlParameters[1] = new SqlParameter("@idUser", SqlDbType.Int);
            sqlParameters[1].Value = model.idUser;

            sqlParameters[2] = new SqlParameter("@Subject", SqlDbType.NVarChar);
            sqlParameters[2].Value = model.Subject;

            sqlParameters[3] = new SqlParameter("@idPersonTo", SqlDbType.Int);
            sqlParameters[3].Value = model.idPersonTo;

            sqlParameters[4] = new SqlParameter("@idClientTo", SqlDbType.Int);
            sqlParameters[4].Value = model.idClientTo;

            sqlParameters[5] = new SqlParameter("@locationOnDisk", SqlDbType.NVarChar);
            sqlParameters[5].Value = model.locationOnDisk;

            sqlParameters[6] = new SqlParameter("@dtSent", SqlDbType.DateTime);
            sqlParameters[6].Value = model.dtSent;

            return conn.executeInsertQuery(query, sqlParameters);

        }
    }
}
