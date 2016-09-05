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
    public class MultimediaDAO
    {
        private dbConnection conn;

        public MultimediaDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllMultimedias()
        {
            string query = string.Format(@"SELECT idMultimedia,m.idArticle ,a.nameArtical,m.idClient,m.idServer, c.nameClient,[description]
                                          ,m.idPeriod,p.descPeriod as namePeriod,m.idUserCreated,m.dtUserCreated,m.idUserModified ,m.dtUserModified
                                          FROM Multimedia m
                                          LEFT OUTER JOIN Period p ON p.idPeriod = m.idPeriod 
                                          LEFT OUTER JOIN Artical a ON a.codeArtical = m.idArticle 
                                          LEFT OUTER JOIN Client c ON c.idClient = m.idClient");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetMultimediaByArticleClientPeriod(string idArticle, int idClient, int idPeriod, int idMultimedia)
        {
            string query = string.Format(@"SELECT idMultimedia,m.idArticle ,a.nameArtical,m.idClient,m.idServer, c.nameClient,[description]
                                          ,m.idPeriod,p.descPeriod as namePeriod,m.idUserCreated,m.dtUserCreated,m.idUserModified ,m.dtUserModified
                                          FROM Multimedia m
                                          LEFT OUTER JOIN Period p ON p.idPeriod = m.idPeriod 
                                          LEFT OUTER JOIN Artical a ON a.codeArtical = m.idArticle 
                                          LEFT OUTER JOIN Client c ON c.idClient = m.idClient 
                                          WHERE m.idArticle = @idArticle AND m.idClient = @idClient AND m.idPeriod = @idPeriod AND idMultimedia <> @idMultimedia");


            SqlParameter[] sqlParameters = new SqlParameter[4];

            sqlParameters[0] = new SqlParameter("@idArticle", SqlDbType.NVarChar);
            sqlParameters[0].Value = idArticle;

            sqlParameters[1] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameters[1].Value = idClient;

            sqlParameters[2] = new SqlParameter("@idPeriod", SqlDbType.Int);
            sqlParameters[2].Value = idPeriod;

            sqlParameters[3] = new SqlParameter("@idMultimedia", SqlDbType.Int);
            sqlParameters[3].Value = idMultimedia;


            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAllMultimediaServers()
        {
            string query = string.Format(@"SELECT idServer, path, folder FROM MultimediaServer");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetMultimediaServersByID(int idServer)
        {
            string query = string.Format(@"SELECT idServer, path, folder FROM MultimediaServer WHERE idServer = '" + idServer.ToString() + "'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetMultimediaCredentials(int idServer)
        {
            string query = string.Format(@"SELECT username, password FROM MultimediaServer WHERE idServer = '"+ idServer +"'");

            return conn.executeSelectQuery(query, null);
        }
        
        public DataTable GetAllPhotosByMultimedia(int idMultimedia)
        {
            string query = string.Format(@"SELECT p.idPhotos,p.namePhotos, p.idMultimedia, m.description,  p.isActive, p.idUserCreator, p.dtUserCreator, p.idUserModified, p.dtUserModified                                           
                                          FROM Photos p 
                                          LEFT OUTER JOIN Multimedia m ON m.idMultimedia = p.idMultimedia                                           
                                          WHERE p.idMultimedia = '" + idMultimedia + "'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllPeriods()
        {
            string query = string.Format(@"SELECT idPeriod ,monthFrom ,monthTo ,descPeriod
                                           FROM Period");
            return conn.executeSelectQuery(query, null);
        }

        public int SaveAndReturnID(MultimediaModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO Multimedia(idArticle, idClient, idServer, idPeriod, [description],  idUserCreated, dtUserCreated )
                      VALUES (@idArticle, @idClient, @idServer, @idPeriod, @description,  @idUserCreated, @dtUserCreated); SELECT SCOPE_IDENTITY()");


            SqlParameter[] sqlParameter = new SqlParameter[7];

            sqlParameter[0] = new SqlParameter("@idArticle", SqlDbType.NVarChar);
            sqlParameter[0].Value = (model.idArticle == null) ? SqlString.Null : model.idArticle;

            sqlParameter[1] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[1].Value = (model.idClient == 0) ? SqlInt32.Null : model.idClient;

            sqlParameter[2] = new SqlParameter("@idServer", SqlDbType.Int);
            sqlParameter[2].Value = (model.idServer == 0) ? SqlInt32.Null : model.idServer;

            sqlParameter[3] = new SqlParameter("@idPeriod", SqlDbType.Int);
            sqlParameter[3].Value = (model.idPeriod == 0) ? SqlInt32.Null : model.idPeriod;

            sqlParameter[4] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[4].Value = (model.description == null) ? SqlString.Null : model.description;

            sqlParameter[5] = new SqlParameter("@idUserCreated", SqlDbType.Int);
            sqlParameter[5].Value = (model.idUserCreated == 0) ? SqlInt32.Null : model.idUserCreated;

            sqlParameter[6] = new SqlParameter("@dtUserCreated", SqlDbType.DateTime);
            sqlParameter[6].Value = (model.dtUserCreated == null) ? SqlDateTime.MinValue : model.dtUserCreated;


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
            sqlParameter[4].Value = conn.GetLastTableID("Multimedia")+1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idMultimedia";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Multimedia";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save multimedia";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransactionSelectLastID(_query, sqlParameters);
        }

        public Boolean Update(MultimediaModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE Multimedia SET
                    idArticle = @idArticle, idClient = @idClient, idServer=@idServer, idPeriod = @idPeriod, 
                    description = @description, idUserModified = @idUserModified, dtUserModified = @dtUserModified 
                    WHERE idMultimedia = @idMultimedia");
           

            SqlParameter[] sqlParameter = new SqlParameter[8];

            sqlParameter[0] = new SqlParameter("@idArticle", SqlDbType.NVarChar);
            sqlParameter[0].Value = (model.idArticle == null) ? SqlString.Null : model.idArticle;

            sqlParameter[1] = new SqlParameter("@idClient", SqlDbType.Int);
            sqlParameter[1].Value = (model.idClient == 0) ? SqlInt32.Null : model.idClient;

            sqlParameter[2] = new SqlParameter("@idServer", SqlDbType.Int);
            sqlParameter[2].Value = (model.idServer == 0) ? SqlInt32.Null : model.idServer;

            sqlParameter[3] = new SqlParameter("@idPeriod", SqlDbType.Int);
            sqlParameter[3].Value = (model.idPeriod == 0) ? SqlInt32.Null : model.idPeriod;

            sqlParameter[4] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[4].Value = (model.description == null) ? SqlString.Null : model.description;


            sqlParameter[5] = new SqlParameter("@idUserModified", SqlDbType.Int);
            sqlParameter[5].Value = (model.idUserModified == 0) ? SqlInt32.Null : model.idUserModified;

            sqlParameter[6] = new SqlParameter("@dtUserModified", SqlDbType.DateTime);
            sqlParameter[6].Value = (model.dtUserModified == null) ? SqlDateTime.MinValue : model.dtUserModified;

            sqlParameter[7] = new SqlParameter("@idMultimedia", SqlDbType.Int);
            sqlParameter[7].Value = (model.idMultimedia == 0) ? SqlInt32.Null : model.idMultimedia;



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
            sqlParameter[4].Value = model.idMultimedia;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idMultimedia";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Multimedia";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update multimedia";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
        public int SavePhotosAndReturnID(PhotosModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO Photos(namePhotos, idMultimedia, isActive, idUserCreator,  dtUserCreator, idUserModified, dtUserModified )
                      VALUES (@namePhotos, @idMultimedia, @isActive, @idUserCreator,  @dtUserCreator, @idUserModified, @dtUserModified); SELECT SCOPE_IDENTITY() ");


            SqlParameter[] sqlParameter = new SqlParameter[7];

            sqlParameter[0] = new SqlParameter("@namePhotos", SqlDbType.NVarChar);
            sqlParameter[0].Value = model.namePhotos;

            sqlParameter[1] = new SqlParameter("@idMultimedia", SqlDbType.Int);
            sqlParameter[1].Value = (model.idMultimedia == null) ? SqlInt32.Null : model.idMultimedia;

            sqlParameter[2] = new SqlParameter("@isActive", SqlDbType.Bit);
            sqlParameter[2].Value = model.isActive;

            sqlParameter[3] = new SqlParameter("@idUserCreator", SqlDbType.Int);
            sqlParameter[3].Value = model.idUserCreator;

            sqlParameter[4] = new SqlParameter("@dtUserCreator", SqlDbType.DateTime);
            sqlParameter[4].Value = (model.dtUserCreator == null) ? SqlDateTime.MinValue : model.dtUserCreator;

            sqlParameter[5] = new SqlParameter("@idUserModified", SqlDbType.Int);
            sqlParameter[5].Value = model.idUserModified;

            sqlParameter[6] = new SqlParameter("@dtUserModified", SqlDbType.DateTime);
            sqlParameter[6].Value = (model.dtUserModified == null) ? SqlDateTime.MinValue : model.dtUserModified;


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
            sqlParameter[4].Value = conn.GetLastTableID("Photos");

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPhotos";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Photos";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save photos and return ID";


            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransactionSelectLastID(_query, sqlParameters);

        }

        public Boolean UpdatePhotosIsActive(bool isActive, int idPhotos, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE Photos SET
                    isActive = @isActive 
                    WHERE idPhotos = @idPhotos");


            SqlParameter[] sqlParameter = new SqlParameter[2];

            sqlParameter[0] = new SqlParameter("@isActive", SqlDbType.Bit);
            sqlParameter[0].Value = isActive;

            sqlParameter[1] = new SqlParameter("@idPhotos", SqlDbType.Int);
            sqlParameter[1].Value = idPhotos;


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
            sqlParameter[4].Value = idPhotos;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPhotos";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Photos";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update photos is active";


            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean DeletePhoto(int idPhotos, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM Photos WHERE idPhotos = @idPhotos");


            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idPhotos", SqlDbType.Int);
            sqlParameter[0].Value = idPhotos;


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
            sqlParameter[4].Value = idPhotos;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idPhotos";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "Photos";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete photo";


            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }
    }
}
