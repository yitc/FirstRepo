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
    public class TypeDAO
    {
        private dbConnection conn;

        public TypeDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAllType(string language)
        {
            string query = string.Format(
                @"(SELECT idTypeClient AS ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameTypeClient END AS Name, CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Client type' END AS Type
                  FROM ClientTypes ct
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = ct.nameTypeClient
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Client type'
                  UNION
                  SELECT idContactType as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE descContactType END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Contact type' END AS Type
                  FROM ContactType cct
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = cct.descContactType
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Contact type'
                  UNION
                  SELECT idAnsType as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameAnsType END AS Name,  CASE WHEN s2.stringValue IS NOT NULL THEN 'Medical answer type' END AS Type
                  FROM MedAnsType mdt
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = mdt.nameAnsType
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Medical answer type'
                  UNION
                  SELECT idToDoType as ID, CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE descriptionToDoType END AS Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'To do type' END AS Type
                  FROM ToDoType todo 
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = todo.descriptionToDoType
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'To do type'
                  UNION
                  SELECT idAddressType as ID, CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameAddressType END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Address type' END AS Type
                  FROM TypesAddress at
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = at.nameAddressType
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Address type' 
                  UNION
                  SELECT idEmailType as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE  nameEmailType END as Name, CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Email type' END AS Type
                  FROM TypesEmail et
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = et.nameEmailType
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Email type' 
                  UNION
                  SELECT idTypeNote as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameTypeNote END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Note type' END AS Type
                  FROM TypesNote nt
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = nt.nameTypeNote
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Note type' 
                  UNION
                  SELECT idTelType as ID, CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameTelType END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Telephone type' END AS Type
                  FROM TypesTel tt
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = tt.nameTelType
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey = 'Telephone type'
                  UNION
                  SELECT idAnsType as ID,CASE WHEN s.stringvalue IS NOT NULL THEN s.stringvalue ELSE nameAnsType END as Name,CASE WHEN s2.stringValue IS NOT NULL THEN  s2.stringValue ELSE 'Volontary answer type' END AS Type
                  FROM VolAnsType vat  
                  LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = vat.nameAnsType
                  LEFT JOIN STRING" + language + @" s2 ON s2.stringkey ='Volontary answer type' ) order by Type");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@language", SqlDbType.VarChar);
            sqlParameters[0].Value = language;

           

           
            return conn.executeSelectQuery(query, sqlParameters);
        }

        public Boolean UpdateTypesAddress(int idAddressType, string nameAddressType)
        {
            string query = string.Format(@"UPDATE TypesAddress
                SET nameAddressType = @nameAddressType
	        	WHERE idAddressType = @idAddressType");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@idAddressType", SqlDbType.Int);
            sqlParameters[0].Value = idAddressType;

            sqlParameters[1] = new SqlParameter("@nameAddressType", SqlDbType.VarChar);
            sqlParameters[1].Value = nameAddressType;

            return conn.executeUpdateQuery(query, sqlParameters);
        }

        public Boolean UpdateClientType(int idTypeClient, string nameTypeClient)
        {
            string query = string.Format(@"UPDATE ClientTypes
                SET nameTypeClient = @nameTypeClient
	        	WHERE idTypeClient = @idTypeClient");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@idTypeClient", SqlDbType.Int);
            sqlParameters[0].Value = idTypeClient;

            sqlParameters[1] = new SqlParameter("@nameTypeClient", SqlDbType.VarChar);
            sqlParameters[1].Value = nameTypeClient;

            return conn.executeUpdateQuery(query, sqlParameters);
        }

        public Boolean UpdateContactType(int idContactType, string descContactType)
        {
            string query = string.Format(@"UPDATE ContactType
                SET descContactType = @descContactType
	        	WHERE idContactType = @idContactType");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@idContactType", SqlDbType.Int);
            sqlParameters[0].Value = idContactType;

            sqlParameters[1] = new SqlParameter("@descContactType", SqlDbType.VarChar);
            sqlParameters[1].Value = descContactType;

            return conn.executeUpdateQuery(query, sqlParameters);
        }

        public Boolean UpdateMedAnsType(int idAnsType, string nameAnsType)
        {
            string query = string.Format(@"UPDATE MedAnsType
                SET nameAnsType = @nameAnsType
	        	WHERE idAnsType = @idAnsType");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@idAnsType", SqlDbType.Int);
            sqlParameters[0].Value = idAnsType;

            sqlParameters[1] = new SqlParameter("@nameAnsType", SqlDbType.VarChar);
            sqlParameters[1].Value = nameAnsType;

            return conn.executeUpdateQuery(query, sqlParameters);
        }

        public Boolean UpdateToDoType(int idToDoType, string descriptionToDoType)
        {
            string query = string.Format(@"UPDATE ToDoType
                SET descriptionToDoType = @descriptionToDoType
	        	WHERE idToDoType = @idToDoType");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@idToDoType", SqlDbType.Int);
            sqlParameters[0].Value = idToDoType;

            sqlParameters[1] = new SqlParameter("@descriptionToDoType", SqlDbType.VarChar);
            sqlParameters[1].Value = descriptionToDoType;

            return conn.executeUpdateQuery(query, sqlParameters);
        }

        public Boolean UpdateVolAnsType(int idAnsType, string nameAnsType)
        {
            string query = string.Format(@"UPDATE VolAnsType
                SET nameAnsType = @nameAnsType
	        	WHERE idAnsType = @idAnsType");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@idAnsType", SqlDbType.Int);
            sqlParameters[0].Value = idAnsType;

            sqlParameters[1] = new SqlParameter("@nameAnsType", SqlDbType.VarChar);
            sqlParameters[1].Value = nameAnsType;

            return conn.executeUpdateQuery(query, sqlParameters);
        }

        public Boolean UpdateTypesTel(int idTelType, string nameTelType)
        {
            string query = string.Format(@"UPDATE TypesTel
                SET nameTelType = @nameTelType
	        	WHERE idTelType = @idTelType");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@idTelType", SqlDbType.Int);
            sqlParameters[0].Value = idTelType;

            sqlParameters[1] = new SqlParameter("@nameTelType", SqlDbType.VarChar);
            sqlParameters[1].Value = nameTelType;

            return conn.executeUpdateQuery(query, sqlParameters);
        }

        public Boolean UpdateTypesNote(int idTypeNote, string nameTypeNote)
        {
            string query = string.Format(@"UPDATE TypesNote
                SET nameTypeNote = @nameTypeNote
	        	WHERE idTypeNote = @idTypeNote");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@idTypeNote", SqlDbType.Int);
            sqlParameters[0].Value = idTypeNote;

            sqlParameters[1] = new SqlParameter("@nameTypeNote", SqlDbType.VarChar);
            sqlParameters[1].Value = nameTypeNote;

            return conn.executeUpdateQuery(query, sqlParameters);
        }

        public Boolean UpdateTypesEmail(int idEmailType, string nameEmailType)
        {
            string query = string.Format(@"UPDATE TypesEmail
                SET nameEmailType = @nameEmailType
	        	WHERE idEmailType = @idEmailType");
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@idEmailType", SqlDbType.Int);
            sqlParameters[0].Value = idEmailType;

            sqlParameters[1] = new SqlParameter("@nameEmailType", SqlDbType.VarChar);
            sqlParameters[1].Value = nameEmailType;

            return conn.executeUpdateQuery(query, sqlParameters);
        }

        public bool InsertTypesAddress( string nameAddressType)
        {

            string query = string.Format(@"INSERT INTO 
                                   TypesAddress(nameAddressType)
                                    VALUES(@nameAddressType)"); 

            SqlParameter[] sqlParameters = new SqlParameter[1];


            sqlParameters[0] = new SqlParameter("@nameAddressType", SqlDbType.NVarChar);
            sqlParameters[0].Value = nameAddressType;

            return conn.executeInsertQuery(query, sqlParameters);

        }

        public bool InsertClientTypes(string nameTypeClient)
        {

            string query = string.Format(@"INSERT INTO 
                                   ClientTypes(nameTypeClient)
                                    VALUES(@nameTypeClient)");

            SqlParameter[] sqlParameters = new SqlParameter[1];


            sqlParameters[0] = new SqlParameter("@nameTypeClient", SqlDbType.NChar);
            sqlParameters[0].Value = nameTypeClient;

            return conn.executeInsertQuery(query, sqlParameters);

        }

        public bool InsertContactType(string descContactType)
        {

            string query = string.Format(@"INSERT INTO 
                                   ContactType(descContactType)
                                    VALUES(@descContactType)");

            SqlParameter[] sqlParameters = new SqlParameter[1];


            sqlParameters[0] = new SqlParameter("@descContactType", SqlDbType.NChar);
            sqlParameters[0].Value = descContactType;

            return conn.executeInsertQuery(query, sqlParameters);

        }


        public bool InsertTypesEmail(string nameEmailType)
        {

            string query = string.Format(@"INSERT INTO 
                                   TypesEmail(nameEmailType)
                                    VALUES(@nameEmailType)");

            SqlParameter[] sqlParameters = new SqlParameter[1];


            sqlParameters[0] = new SqlParameter("@nameEmailType", SqlDbType.NChar);
            sqlParameters[0].Value = nameEmailType;

            return conn.executeInsertQuery(query, sqlParameters);

        }

        public bool InsertMedAnsType(string nameAnsType)
        {

            string query = string.Format(@"INSERT INTO 
                                   MedAnsType(nameAnsType)
                                    VALUES(@nameAnsType)");

            SqlParameter[] sqlParameters = new SqlParameter[1];


            sqlParameters[0] = new SqlParameter("@nameAnsType", SqlDbType.NChar);
            sqlParameters[0].Value = nameAnsType;

            return conn.executeInsertQuery(query, sqlParameters);

        }

        public bool InsertTypesNote(string nameTypeNote)
        {

            string query = string.Format(@"INSERT INTO 
                                   TypesNote(nameTypeNote)
                                    VALUES(@nameTypeNote)");

            SqlParameter[] sqlParameters = new SqlParameter[1];


            sqlParameters[0] = new SqlParameter("@nameTypeNote", SqlDbType.NChar);
            sqlParameters[0].Value = nameTypeNote;

            return conn.executeInsertQuery(query, sqlParameters);

        }

        public bool InsertTypesTel(string nameTelType)
        {

            string query = string.Format(@"INSERT INTO 
                                   TypesTel(nameTelType)
                                    VALUES(@nameTelType)");

            SqlParameter[] sqlParameters = new SqlParameter[1];


            sqlParameters[0] = new SqlParameter("@nameTelType", SqlDbType.NChar);
            sqlParameters[0].Value = nameTelType;

            return conn.executeInsertQuery(query, sqlParameters);

        }

        public bool InsertToDoType(string descriptionToDoType)
        {

            string query = string.Format(@"INSERT INTO 
                                   ToDoType(descriptionToDoType)
                                    VALUES(@descriptionToDoType)");

            SqlParameter[] sqlParameters = new SqlParameter[1];


            sqlParameters[0] = new SqlParameter("@descriptionToDoType", SqlDbType.NChar);
            sqlParameters[0].Value = descriptionToDoType;

            return conn.executeInsertQuery(query, sqlParameters);

        }

        public bool InsertVolAnsType(string nameAnsType)
        {

            string query = string.Format(@"INSERT INTO 
                                   VolAnsType(nameAnsType)
                                    VALUES(@nameAnsType)");

            SqlParameter[] sqlParameters = new SqlParameter[1];


            sqlParameters[0] = new SqlParameter("@nameAnsType", SqlDbType.NChar);
            sqlParameters[0].Value = nameAnsType;

            return conn.executeInsertQuery(query, sqlParameters);

        }

        public bool DeleteTypesAddress(int idAddressType)
        {
            string query = string.Format(@"DELETE FROM  TypesAddress WHERE idAddressType = @idAddressType");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idAddressType", SqlDbType.Int);
            sqlParameters[0].Value = idAddressType;

            return conn.executeDeleteQuery(query, sqlParameters);
        }

        public bool DeleteClientTypes(int idTypeClient)
        {
            string query = string.Format(@"DELETE FROM  ClientTypes WHERE idTypeClient = @idTypeClient");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idTypeClient", SqlDbType.Int);
            sqlParameters[0].Value = idTypeClient;

            return conn.executeDeleteQuery(query, sqlParameters);
        }
        public bool DeleteContactType(int idContactType)
        {
            string query = string.Format(@"DELETE FROM  ContactType WHERE idContactType = @idContactType");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idContactType", SqlDbType.Int);
            sqlParameters[0].Value = idContactType;

            return conn.executeDeleteQuery(query, sqlParameters);
        }

        public bool DeleteTypesEmail(int idEmailType)
        {
            string query = string.Format(@"DELETE FROM  TypesEmail WHERE idEmailType = @idEmailType");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idEmailType", SqlDbType.Int);
            sqlParameters[0].Value = idEmailType;

            return conn.executeDeleteQuery(query, sqlParameters);
        }

        public bool DeleteMedAnsType(int idAnsType)
        {
            string query = string.Format(@"DELETE FROM  MedAnsType WHERE idAnsType = @idAnsType");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idAnsType", SqlDbType.Int);
            sqlParameters[0].Value = idAnsType;

            return conn.executeDeleteQuery(query, sqlParameters);
        }

        public bool DeleteTypesNote(int idTypeNote)
        {
            string query = string.Format(@"DELETE FROM  TypesNote WHERE idTypeNote = @idTypeNote");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idTypeNote", SqlDbType.Int);
            sqlParameters[0].Value = idTypeNote;

            return conn.executeDeleteQuery(query, sqlParameters);
        }

        public bool DeleteTypesTel(int idTelType)
        {
            string query = string.Format(@"DELETE FROM  TypesTel WHERE idTelType = @idTelType");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idTelType", SqlDbType.Int);
            sqlParameters[0].Value = idTelType;

            return conn.executeDeleteQuery(query, sqlParameters);
        }

        public bool DeleteToDoType(int idToDoType)
        {
            string query = string.Format(@"DELETE FROM  ToDoType WHERE idToDoType = @idToDoType");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idToDoType", SqlDbType.Int);
            sqlParameters[0].Value = idToDoType;

            return conn.executeDeleteQuery(query, sqlParameters);
        }

        public bool DeleteVolAnsType(int idAnsType)
        {
            string query = string.Format(@"DELETE FROM  VolAnsType WHERE idAnsType = @idAnsType");

            SqlParameter[] sqlParameters = new SqlParameter[1];

            sqlParameters[0] = new SqlParameter("@idAnsType", SqlDbType.Int);
            sqlParameters[0].Value = idAnsType;

            return conn.executeDeleteQuery(query, sqlParameters);
        }
    }
}
