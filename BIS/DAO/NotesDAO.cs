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
    public class NotesDAO
    {
        private dbConnection conn;
        public NotesDAO()
        {
            conn = new dbConnection();

        }

        public DataTable GetPersonNotes(int idPerson)
        {
            string query = string.Format(@"SELECT idNote,cpn.idContPers,cp.firstname+' '+cp.midname+' '+cp.lastname as nameContPers,cpn.idEmployee,e.firstnameEmployee +' '+e.midnameEmployee+' '+e.lastnameEmployee as nameEmployee,dtNoteDate,noteText,cpn.dtCreated,cpn.dtModified,cpn.idUserModified,um.nameUser as nameUserModified,cpn.idUserCreated, cpn.idTypeNote, u.nameUser AS nameUserCreated,tn.nameTypeNote AS nameType
              FROM ContactPersonNotes cpn
              LEFT OUTER JOIN Users u ON u.idUser = cpn.idUserCreated
              LEFT OUTER JOIN Users um ON um.idUser = cpn.idUserModified
              LEFT OUTER JOIN Employees e ON e.idEmployee = cpn.idEmployee
              LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = cpn.idContPers
              LEFT OUTER JOIN TypesNote tn ON tn.idTypeNote = cpn.idTypeNote
                WHERE cpn.idContPers = @idPerson ");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idPerson", SqlDbType.Int);
            sqlParameters[0].Value = idPerson;
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetPersonStatus2(int idPerson)
        {
            string query = string.Format(@"SELECT idNote,cpn.idContPers,cp.firstname+' '+cp.midname+' '+cp.lastname as nameContPers,cpn.idEmployee,e.firstnameEmployee +' '+e.midnameEmployee+' '+e.lastnameEmployee as nameEmployee,dtNoteDate,noteText,cpn.dtCreated,cpn.dtModified,cpn.idUserModified,um.nameUser as nameUserModified,cpn.idUserCreated, cpn.idTypeNote, u.nameUser AS nameUserCreated,tn.nameTypeNote AS nameType
              FROM ContactPersonNotes cpn
              LEFT OUTER JOIN Users u ON u.idUser = cpn.idUserCreated
              LEFT OUTER JOIN Users um ON um.idUser = cpn.idUserModified
              LEFT OUTER JOIN Employees e ON e.idEmployee = cpn.idEmployee
              LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = cpn.idContPers
              LEFT OUTER JOIN TypesNote tn ON tn.idTypeNote = cpn.idTypeNote
                WHERE cpn.idContPers = @idContPers and cpn.idTypeNote = 2 ");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idPerson;
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetPersonNote(int idNote)
        {
            string query = string.Format(@"SELECT idNote,cpn.idContPers,cp.firstname+' '+cp.midname+' '+cp.lastname as nameContPers,cpn.idEmployee,e.firstnameEmployee +' '+e.midnameEmployee+' '+e.lastnameEmployee as nameEmployee,dtNoteDate,noteText,cpn.dtCreated,cpn.dtModified,cpn.idUserModified,um.nameUser as nameUserModified,cpn.idUserCreated, cpn.idTypeNote, u.nameUser AS nameUserCreated,tn.nameTypeNote AS nameType
              FROM ContactPersonNotes cpn
              LEFT OUTER JOIN Users u ON u.idUser = cpn.idUserCreated
              LEFT OUTER JOIN Users um ON um.idUser = cpn.idUserModified
              LEFT OUTER JOIN Employees e ON e.idEmployee = cpn.idEmployee
              LEFT OUTER JOIN ContactPerson cp ON cp.idContPers = cpn.idContPers
              LEFT OUTER JOIN TypesNote tn ON tn.idTypeNote = cpn.idTypeNote
                WHERE cpn.idNote = @idNote ");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idNote", SqlDbType.Int);
            sqlParameters[0].Value = idNote;
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public bool Save(NotesModel notes, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO ContactPersonNotes (idContPers,idEmployee,dtNoteDate,noteText,dtCreated,dtModified,
                        idUserModified,idUserCreated, idTypeNote) 
                      VALUES(@idContPers, @idEmployee, @dtNoteDate, @noteText, @dtCreated, @dtModified, @idUserModified,
                        @idUserCreated, @idTypeNote)");


            SqlParameter[] sqlParameter = new SqlParameter[9];
            
            sqlParameter[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[0].Value = notes.idContPers;

            sqlParameter[1] = new SqlParameter("@idEmployee", SqlDbType.Int);
            sqlParameter[1].Value = notes.idEmployee;

            sqlParameter[2] = new SqlParameter("@dtNoteDate", SqlDbType.DateTime);
            sqlParameter[2].Value = (notes.dtNoteDate == null || notes.dtCreated == DateTime.MinValue) ? SqlDateTime.Null : notes.dtNoteDate;

            sqlParameter[3] = new SqlParameter("@noteText", SqlDbType.NVarChar);
            sqlParameter[3].Value = notes.noteText;

            sqlParameter[4] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            sqlParameter[4].Value = (notes.dtCreated == null || notes.dtCreated == DateTime.MinValue) ? SqlDateTime.Null : notes.dtCreated; ;

            sqlParameter[5] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[5].Value = (notes.dtModified == null || notes.dtModified == DateTime.MinValue) ? SqlDateTime.Null : notes.dtModified; 

            sqlParameter[6] = new SqlParameter("@idUserModified", SqlDbType.Int);
            sqlParameter[6].Value = notes.idUserModified;

            sqlParameter[7] = new SqlParameter("@idUserCreated", SqlDbType.Int);
            sqlParameter[7].Value = notes.idUserCreated;

            sqlParameter[8] = new SqlParameter("@idTypeNote", SqlDbType.Int);
            sqlParameter[8].Value = notes.idTypeNote;


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
            sqlParameter[4].Value = conn.GetLastTableID("ContactPersonNotes") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idNote";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ContactPersonNotes";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save notes";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public bool Update(NotesModel notes, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE ContactPersonNotes SET 
                    idContPers = @idContPers, idEmployee = @idEmployee, dtNoteDate = @dtNoteDate, 
                    noteText = @noteText,  dtModified = @dtModified, idUserModified = @idUserModified,
                     idTypeNote = @idTypeNote
                      WHERE idNote = @idNote");
            //  dtCreated = @dtCreated,idUserCreated = @idUserCreated,   ovo brise 

            //  SqlParameter[] sqlParameters = new SqlParameter[10];
            SqlParameter[] sqlParameter = new SqlParameter[8];
            sqlParameter[0] = new SqlParameter("@idNote", SqlDbType.Int);
            sqlParameter[0].Value = notes.idNote;

            sqlParameter[1] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[1].Value = notes.idContPers;

            sqlParameter[2] = new SqlParameter("@idEmployee", SqlDbType.Int);
            sqlParameter[2].Value = notes.idEmployee;

            sqlParameter[3] = new SqlParameter("@dtNoteDate", SqlDbType.DateTime);
            sqlParameter[3].Value = (notes.dtNoteDate == null || notes.dtNoteDate == DateTime.MinValue) ? SqlDateTime.Null : notes.dtNoteDate;

            sqlParameter[4] = new SqlParameter("@noteText", SqlDbType.NVarChar);
            sqlParameter[4].Value = notes.noteText;

            //sqlParameters[5] = new SqlParameter("@dtCreated", SqlDbType.DateTime);
            //sqlParameters[5].Value = (notes.dtCreated == null || notes.dtCreated == DateTime.MinValue) ? SqlDateTime.Null : notes.dtCreated; ;

            sqlParameter[5] = new SqlParameter("@dtModified", SqlDbType.DateTime);
            sqlParameter[5].Value = (notes.dtModified == null || notes.dtModified == DateTime.MinValue) ? SqlDateTime.Null : notes.dtModified;

            sqlParameter[6] = new SqlParameter("@idUserModified", SqlDbType.Int);
            sqlParameter[6].Value = notes.idUserModified;

            //sqlParameters[7] = new SqlParameter("@idUserCreated", SqlDbType.Int);
            //sqlParameters[7].Value = (notes.idUserCreated == null) ? SqlInt32.Null : notes.idUserCreated;

            sqlParameter[7] = new SqlParameter("@idTypeNote", SqlDbType.Int);
            sqlParameter[7].Value = notes.idTypeNote;

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
            sqlParameter[4].Value = notes.idNote;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idNote";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ContactPersonNotes";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update notes";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public bool Delete(int id, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM  ContactPersonNotes WHERE idNote = @idNote");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@idNote", SqlDbType.Int);
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
            sqlParameter[5].Value = "idNote";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ContactPersonNotes";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete notes";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

    }
}

