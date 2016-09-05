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
    public class ArrangementBookPersonsDAO
    {
        private dbConnection conn;
        public ArrangementBookPersonsDAO()
        {
            conn = new dbConnection();

        }

        public Boolean DeletePersonFromGrid(int idArrangementBook, int idContPers, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE ArrangementBookPersons  WHERE idArrangementBook = @idArrangementBook and idContPers=@idContPers");

            SqlParameter[] sqlParameter = new SqlParameter[2];

            sqlParameter[0] = new SqlParameter("@idArrangementBook", SqlDbType.Int);
            sqlParameter[0].Value = idArrangementBook;

            sqlParameter[1] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[1].Value = idContPers;


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
            sqlParameter[4].Value = idArrangementBook.ToString() + "_" + idContPers.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangementBook_idContPers";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementBookPersons";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete person from grid";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public DataTable GetAllTravelersForArrangement(int idArrangement, int idContPers)
        {
            string query = string.Format(@"  select distinct ab.idContPers, cp.initialsContPers, cp.firstname, cp.lastname, cp.midname, cp.maidenname from ArrangementBook ab 
                left outer join ContactPerson cp On cp.idContPers = ab.idContPers
                where idArrangement = '" + idArrangement + @"' and ab.idContPers in (select distinct idContPers from ContactPersonFilter where idFilter<>4)
                and ab.idContPers <> '" + idContPers + @"' and ab.idContPers NOT IN (SELECT DISTINCT at.idContPers
                 FROM ArrangementTravelers at 
                 left outer join ContactPerson cp On cp.idContPers = at.idContPers
                 WHERE at.idArrangement = '" + idArrangement + @"' )
                 and ab.idContPers NOT IN (SELECT DISTINCT at.idTravelWithPerson
                 FROM ArrangementTravelers at 
                 left outer join ContactPerson cp On cp.idContPers = at.idTravelWithPerson
                 WHERE at.idArrangement ='" + idArrangement + @"') and  (ab.idStatus = '2' OR ab.idStatus = '1')");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllTravelersWith(int idContPers, int idArrangement)
        {
            string query = string.Format(@"SELECT 
                 at.id,at.idArrangement,at.idContPers,at.idTravelWithPerson, cp.firstname,cp.midname,cp.lastname,cp.birthdate,cp.firstname + ' '+ cp.lastname as fullname 
                 FROM ArrangementTravelers at 
                 left outer join ContactPerson cp On cp.idContPers = at.idTravelWithPerson
                 WHERE at.idArrangement = '" + idArrangement + @"' and at.idContPers IN (
                 SELECT at.idContPers
                 FROM ArrangementTravelers at 
                 left outer join ContactPerson cp On cp.idContPers = at.idContPers
                 WHERE at.idArrangement = '" + idArrangement + "' and at.idContPers = '" + idContPers + @"') 
                 UNION
                 SELECT 
                 at.id,at.idArrangement,at.idContPers,at.idTravelWithPerson, cp.firstname,cp.midname,cp.lastname,cp.birthdate,cp.firstname + ' '+ cp.lastname as fullname 
                 FROM ArrangementTravelers at 
                 left outer join ContactPerson cp On cp.idContPers = at.idTravelWithPerson
                 WHERE at.idArrangement = '" + idArrangement + @"' and at.idContPers IN (
                 SELECT at.idContPers
                 FROM ArrangementTravelers at 
                 left outer join ContactPerson cp On cp.idContPers = at.idTravelWithPerson
                 WHERE at.idArrangement = '" + idArrangement + "' and at.idTravelWithPerson = '" + idContPers + @"') and at.idTravelWithPerson<>'" + idContPers + @"'
                 UNION
                 SELECT 
                 at.id,at.idArrangement,at.idContPers,at.idTravelWithPerson, cp.firstname,cp.midname,cp.lastname,cp.birthdate,cp.firstname + ' '+ cp.lastname as fullname 
                 FROM ArrangementTravelers at 
                 left outer join ContactPerson cp On cp.idContPers = at.idContPers
                 WHERE at.idArrangement = '" + idArrangement + @"' and at.idContPers IN (
                 SELECT at.idContPers
                 FROM ArrangementTravelers at 
                 left outer join ContactPerson cp On cp.idContPers = at.idTravelWithPerson
                 WHERE at.idArrangement = '" + idArrangement + "' and at.idTravelWithPerson = '" + idContPers + @"') and at.idTravelWithPerson='" + idContPers + @"'");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetTravelerForTravelerWith(int idContPers, int idArrangement)
        {
            string query = string.Format(@"SELECT at.id,at.idArrangement,at.idContPers,at.idTravelWithPerson,cp.firstname,cp.lastname,cp.birthdate,cp.firstname + ' '+ cp.lastname as fullname 
                 FROM ArrangementTravelers at 
                 left outer join ContactPerson cp On cp.idContPers = at.idTravelWithPerson
                 WHERE at.idArrangement = '" + idArrangement + "' and at.idTravelWithPerson = '" + idContPers + @"'
                 UNION 
                 SELECT at.id,at.idArrangement,at.idContPers,at.idTravelWithPerson,cp.firstname,cp.lastname,cp.birthdate,cp.firstname + ' '+ cp.lastname as fullname 
                 FROM ArrangementTravelers at 
                 left outer join ContactPerson cp On cp.idContPers = at.idContPers
                 WHERE at.idArrangement = '" + idArrangement + "' and at.idContPers = '" + idContPers + @"'");
            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllTravelersInvoicing(int idArrangementBook, Boolean isNamePassport)
        {
            string query = string.Format(@"SELECT idArrangementBook,idArrangement,idContPers,idPayInvoice,firstname,lastname,birthdate,fullname, sort
				FROM
                (SELECT at.idArrangementBook,at.idArrangement,at.idContPers,at.idPayInvoice,
                 CASE WHEN cp.firstname IS NULL THEN '' ELSE cp.firstname END as firstname,
                 CASE WHEN cp.lastname IS NULL THEN '' ELSE cp.lastname END as lastname,cp.birthdate
                 ,CASE WHEN cp.firstname IS NULL THEN '' ELSE cp.firstname END + ' '+ CASE WHEN cp.midname IS NULL THEN '' ELSE cp.midname END + ' ' +  CASE WHEN cp.lastname IS NULL THEN '' ELSE cp.lastname END as fullname, 0 as sort
                 FROM ArrangementBook at 
                 left outer join ContactPerson cp On cp.idContPers = at.idContPers
                 WHERE at.idArrangementBook = @idArrangementBook
				 UNION
                 SELECT at.idArrangementBook,at.idArrangement,abp.idContPers,at.idPayInvoice,
                 CASE WHEN cp.firstname IS NULL THEN '' ELSE cp.firstname END as firstname,
                 CASE WHEN cp.lastname IS NULL THEN '' ELSE cp.lastname END as lastname,cp.birthdate
                 ,CASE WHEN cp.firstname IS NULL THEN '' ELSE cp.firstname END + ' '+ CASE WHEN cp.midname IS NULL THEN '' ELSE cp.midname END + ' ' +  CASE WHEN cp.lastname IS NULL THEN '' ELSE cp.lastname END as fullname, 1 as sort
                 FROM ArrangementBookPersons abp
                 left outer join ArrangementBook at  On at.idArrangementBook = abp.idArrangementBook
                 left outer join ContactPerson cp On cp.idContPers = abp.idContPers
                 WHERE at.idArrangementBook = @idArrangementBook
                  ) dd
                order by sort, fullname");

            if(isNamePassport==true)
                query = string.Format(@"SELECT idArrangementBook,idArrangement,idContPers,idPayInvoice,firstname,lastname,birthdate,fullname, sort
                FROM
                (SELECT at.idArrangementBook,at.idArrangement,at.idContPers,at.idPayInvoice,
                 CASE WHEN (CASE WHEN p.namePassport IS NULL THEN '' ELSE p.namePassport END)='' THEN (CASE WHEN cp.firstname IS NULL THEN '' ELSE cp.firstname END) ELSE (CASE WHEN p.namePassport IS NULL THEN '' ELSE p.namePassport END)  END as firstname,
                 CASE WHEN (CASE WHEN p.lastNamePassport IS NULL THEN '' ELSE p.lastNamePassport END)='' THEN (CASE WHEN cp.lastname IS NULL THEN '' ELSE cp.lastname END) ELSE (CASE WHEN p.lastNamePassport IS NULL THEN '' ELSE p.lastNamePassport END) END as lastname
                ,cp.birthdate,CASE WHEN (CASE WHEN p.namePassport IS NULL THEN '' ELSE p.namePassport END + ' ' + CASE WHEN p.lastNamePassport IS NULL THEN '' ELSE p.lastNamePassport END)='' THEN CASE WHEN cp.firstname IS NULL THEN '' ELSE cp.firstname END + ' ' + CASE WHEN cp.midname IS NULL THEN '' ELSE cp.midname END + ' ' + CASE WHEN cp.lastname IS NULL THEN '' ELSE cp.lastname END ELSE (CASE WHEN p.namePassport IS NULL THEN '' ELSE p.namePassport END + ' ' + CASE WHEN p.lastNamePassport IS NULL THEN '' ELSE p.lastNamePassport END) END as fullname , 0 as sort
                 FROM ArrangementBook at 
                 left outer join ContactPerson cp On cp.idContPers = at.idContPers
                 left outer join ContactPersonPassport p On cp.idContPers = p.idContPers
                WHERE at.idArrangementBook = @idArrangementBook 
				 UNION
                 SELECT at.idArrangementBook,at.idArrangement,at.idContPers,at.idPayInvoice,CASE WHEN (CASE WHEN p.namePassport IS NULL THEN '' ELSE p.namePassport END)='' THEN (CASE WHEN cp.firstname IS NULL THEN '' ELSE cp.firstname END) ELSE (CASE WHEN p.namePassport IS NULL THEN '' ELSE p.namePassport END)  END as firstname,
                 CASE WHEN (CASE WHEN p.lastNamePassport IS NULL THEN '' ELSE p.lastNamePassport END)='' THEN (CASE WHEN cp.lastname IS NULL THEN '' ELSE cp.lastname END) ELSE (CASE WHEN p.lastNamePassport IS NULL THEN '' ELSE p.lastNamePassport END) END as lastname
                ,cp.birthdate,CASE WHEN (CASE WHEN p.namePassport IS NULL THEN '' ELSE p.namePassport END + ' ' + CASE WHEN p.lastNamePassport IS NULL THEN '' ELSE p.lastNamePassport END)='' THEN CASE WHEN cp.firstname IS NULL THEN '' ELSE cp.firstname END + ' ' + CASE WHEN cp.midname IS NULL THEN '' ELSE cp.midname END + ' ' + CASE WHEN cp.lastname IS NULL THEN '' ELSE cp.lastname END ELSE (CASE WHEN p.namePassport IS NULL THEN '' ELSE p.namePassport END + ' ' + CASE WHEN p.lastNamePassport IS NULL THEN '' ELSE p.lastNamePassport END) END as fullname , 1 as sort
                   FROM ArrangementBookPersons abp
                 left outer join ArrangementBook at  On at.idArrangementBook = abp.idArrangementBook
                 left outer join ContactPerson cp On cp.idContPers = abp.idContPers
                 left outer join ContactPersonPassport p On cp.idContPers = p.idContPers
                 WHERE at.idArrangementBook = @idArrangementBook) dd 
                order by sort, fullname");

            SqlParameter[] sqlParameter = new SqlParameter[1];
            sqlParameter[0] = new SqlParameter("@idArrangementBook", SqlDbType.Int);
            sqlParameter[0].Value = idArrangementBook;

            return conn.executeSelectQuery(query, sqlParameter);
        }

        public Boolean SaveTravelWith(System.ComponentModel.BindingList<ArrangementTravelersModel> lista, int idArrangement, int idContPers, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM ArrangementTravelers WHERE (idContPers = @idContPers OR idContPers IN (SELECT DISTINCT idContPers FROM ArrangementTravelers WHERE idTravelWithPerson = @idContPers )) AND idArrangement = @idArrangement");

            SqlParameter[] sqlParameter = new SqlParameter[2];
            sqlParameter[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[0].Value = idContPers;
            sqlParameter[1] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[1].Value = idArrangement;


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
            sqlParameter[4].Value = idContPers.ToString() + "_" + idArrangement.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idContPers_idArrangement";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementTravelers";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save travel with";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);

            foreach(ArrangementTravelersModel m in lista)
            {
                query = string.Format(@"INSERT INTO ArrangementTravelers(idArrangement, idContPers, idTravelWithPerson) 
                    VALUES(@idArrangement, @idContPers, @idTravelWithPerson)");

                SqlParameter[] sqlParameter1 = new SqlParameter[3];
                
                sqlParameter1[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
                sqlParameter1[0].Value = m.idArrangement;

                sqlParameter1[1] = new SqlParameter("@idContPers", SqlDbType.Int);
                sqlParameter1[1].Value = m.idContPers;

                sqlParameter1[2] = new SqlParameter("@idTravelWithPerson", SqlDbType.Int);
                sqlParameter1[2].Value = m.idTravelWithPerson;

                _query.Add(query);
                sqlParameters.Add(sqlParameter1);

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
                sqlParameter[4].Value = conn.GetLastTableID("ArrangementTravelers")+1;

                sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
                sqlParameter[5].Value = "id";

                sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
                sqlParameter[6].Value = "ArrangementTravelers";

                sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
                sqlParameter[7].Value = "Save travel with";

                _query.Add(query);
                sqlParameters.Add(sqlParameter);

            }


            return conn.executQueryTransaction(_query, sqlParameters);

        }

        public Boolean UpdatePayInvoicePerson(int idArrangement, int idContPers, int idPayInvoice, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE ArrangementBook SET idPayInvoice=@idPayInvoice
                                          WHERE idArrangement = @idArrangement and idContPers=@idContPers");

            SqlParameter[] sqlParameter = new SqlParameter[3];

            sqlParameter[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameter[0].Value = idArrangement;

            sqlParameter[1] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[1].Value = idContPers;

            sqlParameter[2] = new SqlParameter("@idPayInvoice", SqlDbType.Int);
            sqlParameter[2].Value = idPayInvoice;

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
            sqlParameter[4].Value = idArrangement.ToString() + "_" + idContPers.ToString() + "_" + idPayInvoice.ToString();

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "idArrangement_idContPers_idPayInvoice";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "ArrangementBook";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update pay invoice person";

            return conn.executQueryTransaction(_query, sqlParameters);

        }
    }
}