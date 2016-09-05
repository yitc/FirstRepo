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
    public class VolAvailabilityDAO
    {
         private dbConnection conn;

        public VolAvailabilityDAO()
        {
            conn = new dbConnection();
        }

        public DataTable GetAvailabilityByVolontary(int idContPers)
        {
            string query = string.Format(@"SELECT id, idContPers,dateFrom,dateTo, nrTimes FROM VolAvailability WHERE idContPers = @idContPers");

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;

            return conn.executeSelectQuery(query, sqlParameters);
        }

        public object GetSignedupNrTimesForPeriod(int idArrangement, int idContPers)
        {

            string query = string.Format(@"SELECT CASE WHEN a.nrTimes IS NULL THEN 0 ELSE a.nrTimes END nrTimes
                    FROM ContactPerson cp
                    LEFT OUTER JOIN (SELECT nrTimes,idContPers FROM VolAvailability 
                    WHERE (SELECT dtFromArrangement FROM Arrangement WHERE idArrangement = @idArrangement)>=dateFrom 
                    AND (SELECT dtToArrangement FROM Arrangement WHERE idArrangement = @idArrangement)<=dateTo) a ON a.idContPers = cp.idContPers
                    WHERE cp.idContPers = @idContPers ");


            SqlParameter[] sqlParameters = new SqlParameter[2];
            
            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            sqlParameters[1] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[1].Value = idContPers;

            return conn.executeScalarQuery(query, sqlParameters);
            
        }

        public object GetFinishedNrTimesForPeriod(int idArrangement, int idContPers)
        {

            string query = string.Format(@"SELECT Count(cp.idContPers)
                    FROM ContactPerson cp
                    INNER JOIN  (SELECT distinct ab.idContPers FROM ArrangementBook ab LEFT OUTER JOIN Arrangement a ON ab.idArrangement = a.idArrangement
                    LEFT OUTER JOIN VolAvailability va ON va.idContPers = ab.idContPers
                     WHERE a.dtTOArrangement <= va.dateTo
                    AND a.dtfromArrangement >= va.dateFrom) ap ON ap.idContPers = cp.idContPers
                    WHERE cp.idContPers IN (SELECT DISTINCT idContPers FROM VolAvailability 
                    WHERE (SELECT dtFromArrangement FROM Arrangement WHERE idArrangement = @idArrangement)>=dateFrom 
                    AND (SELECT dtToArrangement FROM Arrangement WHERE idArrangement = @idArrangement)<=dateTo)
                    AND cp.idContPers = @idContPers");


            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@idArrangement", SqlDbType.Int);
            sqlParameters[0].Value = idArrangement;

            sqlParameters[1] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[1].Value = idContPers;

            return conn.executeScalarQuery(query, sqlParameters);

        }

       
        public int SaveAndReturnID(VolAvailabilityModel model, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"INSERT INTO VolAvailability (idContPers, dateFrom, dateTo, nrTimes) 
                      VALUES(@idContPers, @dateFrom, @dateTo, @nrTimes); SELECT SCOPE_IDENTITY() ");


            SqlParameter[] sqlParameter = new SqlParameter[4];
            sqlParameter[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameter[0].Value = model.idContPers;

            sqlParameter[1] = new SqlParameter("@dateFrom", SqlDbType.DateTime);
            sqlParameter[1].Value = model.dateFrom;

            sqlParameter[2] = new SqlParameter("@dateTo", SqlDbType.DateTime);
            sqlParameter[2].Value = model.dateTo;

            sqlParameter[3] = new SqlParameter("@nrTimes", SqlDbType.Int);
            sqlParameter[3].Value = model.nrTimes;

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
            sqlParameter[4].Value = conn.GetLastTableID("VolAvailability") + 1;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "id";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolAvailability";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Save and return ID";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransactionSelectLastID(_query, sqlParameters);
        }
         

        

        public Boolean UpdateNrTimes(int nrTimes, int id, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"UPDATE VolAvailability SET nrTimes = @nrTimes WHERE id = @id");

            SqlParameter[] sqlParameter = new SqlParameter[2];
            
            sqlParameter[0] = new SqlParameter("@nrTimes", SqlDbType.Int);
            sqlParameter[0].Value = nrTimes;

            sqlParameter[1] = new SqlParameter("@id", SqlDbType.Int);
            sqlParameter[1].Value = id;

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
            sqlParameter[4].Value = id;

            sqlParameter[5] = new SqlParameter("@nameId", SqlDbType.NVarChar);
            sqlParameter[5].Value = "id";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolAvailability";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Update nr times";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);
        }

        public Boolean Delete(int id, string nameForm, int idUser)
        {
            List<string> _query = new List<string>();
            List<SqlParameter[]> sqlParameters = new List<SqlParameter[]>();

            string query = string.Format(@"DELETE FROM VolAvailability  WHERE  id = @id ");

            SqlParameter[] sqlParameter = new SqlParameter[1];

            sqlParameter[0] = new SqlParameter("@id", SqlDbType.Int);
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
            sqlParameter[5].Value = "id";

            sqlParameter[6] = new SqlParameter("@tableName", SqlDbType.NVarChar);
            sqlParameter[6].Value = "VolAvailability";

            sqlParameter[7] = new SqlParameter("@description", SqlDbType.NVarChar);
            sqlParameter[7].Value = "Delete";

            _query.Add(query);
            sqlParameters.Add(sqlParameter);


            return conn.executQueryTransaction(_query, sqlParameters);

        }
        //========jeca
        public DataTable GetAvailabilityByVolontarySkills(DateTime dtFrom, DateTime dtTo)
        {
            string query = string.Format(@"select distinct l.nameLabel,cpl.idLabel, cp.idContPers,t.nameTitle ,cp.firstname,cp.lastname,cast(va.dateFrom as date) as dateFrom,CONVERT(DATE,va.dateTo) as dateTo ,
                        case 
	                    when ((MONTH(cp.birthdate) * 100) + DAY(cp.birthdate)) > ((MONTH(getdate()) * 100) + DAY(getdate()))
                        then DATEDIFF(year,cp.birthdate,getdate()) - 1
	                    else DATEDIFF(year,cp.birthdate,getdate()) End As Age,
                        g.nameGender , space(20) AS dateFrom1, space(20) AS dateTo1
 
                         from ContactPerson cp
                         INNER JOIN VolAvailability va ON va.idContPers = cp.idContPers
                         LEFT OUTER JOIN Title t on t.idTitle=cp.idTitle
                         LEFT OUTER JOIN Gender g on g.idGender=cp.idGender
                         LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= cp.idContPers
                         LEFT OUTER JOIN Labels l on l.idLabel= cpl.idLabel
                         LEFT OUTER JOIN ContactPersonFilter cpf on cpf.idContPers= cp.idContPers
                         WHERE va.dateFrom<='" + dtFrom.ToString("MM/dd/yyyy HH:mm:ss.fff") + "' AND va.dateTo>='" + dtTo.ToString("MM/dd/yyyy HH:mm:ss.fff") + "' AND cpf.idFilter='4' order by  l.nameLabel");
            //    AND cpl.idLabel='" + idlbl + "'

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;

            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;



            return conn.executeSelectQuery(query, sqlParameters);
        }


        public DataTable GetAvailabilityByVolontaryAge(DateTime dtFrom, DateTime dtTo)
        {
            string query = string.Format(@" select distinct l.nameLabel,cpl.idLabel,cp.idContPers,t.nameTitle ,cp.firstname,cp.lastname,
                         case 
	                     when ((MONTH(cp.birthdate) * 100) + DAY(cp.birthdate)) > ((MONTH(getdate()) * 100) + DAY(getdate()))
                         then DATEDIFF(year,cp.birthdate,getdate()) - 1
	                     else DATEDIFF(year,cp.birthdate,getdate()) End As Age,
                         space(20) AS dateFrom1, space(20) AS dateTo1                   
                         from ContactPerson cp
                         INNER JOIN VolAvailability va ON va.idContPers = cp.idContPers
                         LEFT OUTER JOIN Title t on t.idTitle=cp.idTitle
                         LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= cp.idContPers
                         LEFT OUTER JOIN Labels l on l.idLabel= cpl.idLabel
                         LEFT OUTER JOIN ContactPersonFilter cpf on cpf.idContPers= cp.idContPers                  
                         WHERE cpf.idFilter='4' AND va.dateFrom<='" + dtFrom.ToString("MM/dd/yyyy HH:mm:ss.fff") + "' AND va.dateTo>='" + dtTo.ToString("MM/dd/yyyy HH:mm:ss.fff") + "'  order by l.nameLabel");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;

            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;



            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAvailabilityNotBooked(DateTime dtFrom, DateTime dtTo)
        {
            string query = string.Format(@" select distinct l.nameLabel,cpl.idLabel, cp.idContPers,t.nameTitle ,cp.firstname,cp.lastname,va.dateFrom,va.dateTo ,
                                     case 
	                                 when ((MONTH(cp.birthdate) * 100) + DAY(cp.birthdate)) > ((MONTH(getdate()) * 100) + DAY(getdate()))
                                     then DATEDIFF(year,cp.birthdate,getdate()) - 1
	                                 else DATEDIFF(year,cp.birthdate,getdate()) End As Age,
                                     space(20) AS dateFrom1, space(20) AS dateTo1
                     
                         from (select idContPers,firstname,lastname, idTitle, idGender,birthdate FROM ContactPerson
                       WHERE idContPers NOT in (select idContPers FROM ArrangementBook))  cp
                         LEFT OUTER JOIN VolAvailability va ON va.idContPers = cp.idContPers
                         LEFT OUTER JOIN Title t on t.idTitle=cp.idTitle
                         LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= cp.idContPers
                         LEFT OUTER JOIN Labels l on l.idLabel= cpl.idLabel
                         LEFT OUTER JOIN ContactPersonFilter cpf on cpf.idContPers= cp.idContPers
                         WHERE va.dateFrom<='" + dtFrom.ToString("MM/dd/yyyy HH:mm:ss.fff") + "' AND va.dateTo>= '" + dtTo.ToString("MM/dd/yyyy HH:mm:ss.fff") + "' AND cpf.idFilter='4' order by l.nameLabel");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;

            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;



            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAvailabilitySkills()
        {
            string query = string.Format(@" SELECT DISTINCT  cast(vfq.txtQuest as varchar(max)) as quest
                 FROM VolQuest vfq INNER JOIN (select distinct idQuest FROM VolCpr) vcp ON vcp.idQuest=vfq.idQuest
                 LEFT OUTER JOIN VolAns va on va.idQuest= vcp.idQuest
                ");

            //              string query = string.Format(@" SELECT DISTINCT vfq.idQuest as ID, cast(vfq.txtQuest as varchar(max)) as quest
            //                 FROM VolQuest vfq INNER JOIN (select distinct idQuest FROM VolCpr) vcp ON vcp.idQuest=vfq.idQuest
            //                 LEFT OUTER JOIN VolAns va on va.idQuest= vcp.idQuest
            //  where idQueryType=@idLabel 

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAvailabilityFunction()
        {
            string query = string.Format(@" SELECT DISTINCT  cast(vfq.txtQuest as varchar(max)) as quest
                 FROM VolFunctionQuest vfq INNER JOIN (select distinct idQuest FROM VolFunctionCpr) vcp ON vcp.idQuest=vfq.idQuest
                 LEFT OUTER JOIN VolFunctionAns va on va.idQuest= vfq.idQuest
                  ");


            return conn.executeSelectQuery(query, null);
        }

        public DataTable IsCheckedSkills(int idContPers, string txtQuest)
        {
            string query = string.Format(@"select vcp.idQuest AS ID,vq.txtQuest as quest 
                                           FROM VolCpr vcp
                                           INNER JOIN  VolQuest vq on vq.idQuest= vcp.idQuest
                                           WHERE idcpr=@idContPers and txtQuest LIKE @txtQuest ");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;

            sqlParameters[1] = new SqlParameter("@txtQuest", SqlDbType.NVarChar);
            sqlParameters[1].Value = txtQuest;


            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable IsCheckedFunction(int idContPers, string txtQuest)
        {
            string query = string.Format(@"select vcp.idQuest AS ID,vq.txtQuest as quest 
                                           FROM VolFunctionCpr vcp
                                           INNER JOIN  VolFunctionQuest vq on vq.idQuest= vcp.idQuest
                                           WHERE idcpr=@idContPers and txtQuest LIKE @txtQuest ");

            SqlParameter[] sqlParameters = new SqlParameter[2];

            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;

            sqlParameters[1] = new SqlParameter("@txtQuest", SqlDbType.NVarChar);
            sqlParameters[1].Value = txtQuest;


            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable ExpiredVokGoKPass(DateTime dtFrom, DateTime dtTo, string language)
        {
            string query = string.Format(@"select cp.idContPers,t.nameTitle ,cp.firstname,cp.lastname, CASE WHEN s.stringValue IS NOT NULL THEN  s.stringValue ELSE vs.idSimilarity END as Type, cast(vs.dtExpirationDate as date) as dtExpirationDate,
                                           space(20) AS dateFrom1, space(20) AS dateTo1
                                          FROM  ContactPerson cp
                                          LEFT OUTER JOIN VolSimilarity vs ON  vs.idContPers=cp.idContPers
                                          LEFT OUTER JOIN VolAvailability va ON va.idContPers = cp.idContPers
                                          LEFT OUTER JOIN Title t on t.idTitle=cp.idTitle
                                          LEFT OUTER JOIN STRING" + language + @"  S on s.stringkey = vs.idSimilarity
                                          LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= cp.idContPers
                                          LEFT OUTER JOIN Labels l on l.idLabel= cpl.idLabel        
                                          LEFT OUTER JOIN ContactPersonFilter cpf on cpf.idContPers= cp.idContPers                                                 
                                          WHERE  vs.dtExpirationDate < GETDATE() 
                                          AND va.dateFrom<='" + dtFrom.ToString("MM/dd/yyyy HH:mm:ss.fff") + "' AND va.dateTo>= '" + dtTo.ToString("MM/dd/yyyy HH:mm:ss.fff") + "' AND cpf.idFilter = '4' "
                                         + @" UNION
                                          select  cp.idContPers,t.nameTitle ,cp.firstname,cp.lastname, 'Pass' as Type, cpp.dtPassportValid,
                                           space(20) AS dateFrom1, space(20) AS dateTo1
                                          FROM ContactPerson cp
                                          LEFT OUTER JOIN VolAvailability va ON va.idContPers = cp.idContPers
                                          LEFT OUTER JOIN  ContactPersonPassport cpp ON cpp.idContPers=cp.idContPers
                                          LEFT OUTER JOIN Title t on t.idTitle=cp.idTitle
                                          LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= cp.idContPers
                                          LEFT OUTER JOIN Labels l on l.idLabel= cpl.idLabel
                                          LEFT OUTER JOIN ContactPersonFilter cpf on cpf.idContPers= cp.idContPers     
                                          WHERE  cpp.dtPassportValid < GETDATE() AND va.dateFrom<='" + dtFrom.ToString("MM/dd/yyyy HH:mm:ss.fff") + "' AND va.dateTo>='" + dtTo.ToString("MM/dd/yyyy HH:mm:ss.fff") + "' AND cpf.idFilter = '4'");

            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@language", SqlDbType.NVarChar);
            sqlParameters[0].Value = language;

            sqlParameters[1] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[1].Value = dtFrom;

            sqlParameters[2] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[2].Value = dtTo;


            return conn.executeSelectQuery(query, sqlParameters);

        }

        public DataTable GetAvailabilityByVolontaryClining(DateTime dtFrom, DateTime dtTo)
        {
            string query = string.Format(@" select  distinct l.nameLabel,cpl.idLabel,cp.idContPers,t.nameTitle ,cp.firstname,cp.lastname,
                           space(20) AS dateFrom1, space(20) AS dateTo1                     
                         from ContactPerson cp
                         INNER JOIN VolAvailability va ON va.idContPers = cp.idContPers
                         LEFT OUTER JOIN Title t on t.idTitle=cp.idTitle    
                         LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= cp.idContPers 
                         LEFT OUTER JOIN Labels l on l.idLabel= cpl.idLabel              
                         LEFT OUTER JOIN ContactPersonFilter cpf on cpf.idContPers= cp.idContPers                                         
                         WHERE va.dateFrom<='" + dtFrom.ToString("MM/dd/yyyy HH:mm:ss.fff") + "' AND va.dateTo>='" + dtTo.ToString("MM/dd/yyyy HH:mm:ss.fff") + "'  order by l.nameLabel ");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;

            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;




            return conn.executeSelectQuery(query, sqlParameters);
        }
        //============
        //NOVO jelena:
        public DataTable GetAvailabilityByVolontaryReasonIn(DateTime dtFrom, DateTime dtTo)
        {
            string query = string.Format(@" select distinct l.nameLabel,cpl.idLabel,cp.idContPers,t.nameTitle ,cp.firstname,cp.lastname,Year(GETDATE())-year(cp.birthdate) as Age,nameReasonIn,
                        space(20) AS dateFrom1, space(20) AS dateTo1              
                         from ContactPerson cp
                         INNER JOIN VolAvailability va ON va.idContPers = cp.idContPers
                         LEFT OUTER JOIN Title t on t.idTitle=cp.idTitle
                         LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= cp.idContPers
                         LEFT OUTER JOIN Labels l on l.idLabel= cpl.idLabel
                         LEFT OUTER JOIN ContactPersonFilter cpf on cpf.idContPers= cp.idContPers                        
                         LEFT OUTER JOIN VoluntaryReasonIn cin on cp.idReasonIn= cin.idReasonIn               
                         WHERE cpf.idFilter='4' AND va.dateFrom<='" + dtFrom.ToString("MM/dd/yyyy HH:mm:ss.fff") + "' AND va.dateTo>='" + dtTo.ToString("MM/dd/yyyy HH:mm:ss.fff") + "'  order by l.nameLabel");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;

            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;



            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAvailabilityByVolontaryReasonOut(DateTime dtFrom, DateTime dtTo)
        {
            string query = string.Format(@" select distinct l.nameLabel,cpl.idLabel,cp.idContPers,t.nameTitle ,cp.firstname,cp.lastname,Year(GETDATE())-year(cp.birthdate) as Age,nameReasonOut,
                         space(20) AS dateFrom1, space(20) AS dateTo1        
                         from ContactPerson cp
                         INNER JOIN VolAvailability va ON va.idContPers = cp.idContPers
                         LEFT OUTER JOIN Title t on t.idTitle=cp.idTitle
                         LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= cp.idContPers
                         LEFT OUTER JOIN Labels l on l.idLabel= cpl.idLabel
                         LEFT OUTER JOIN ContactPersonFilter cpf on cpf.idContPers= cp.idContPers
                         LEFT OUTER JOIN VoluntaryReasonOut cout on cp.idReasonOut= cout.idReasonOut               
                         WHERE cpf.idFilter='4' AND va.dateFrom<='" + dtFrom.ToString("MM/dd/yyyy HH:mm:ss.fff") + "' AND va.dateTo>='" + dtTo.ToString("MM/dd/yyyy HH:mm:ss.fff") + "'  order by l.nameLabel");

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParameters[0].Value = dtFrom;

            sqlParameters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParameters[1].Value = dtTo;



            return conn.executeSelectQuery(query, sqlParameters);
        }

        public DataTable GetAvailabilityByVolontaryTel(int idContPers)
        {
            string query = string.Format(@" select  cpt.numberTel             
                         from ContactPerson cp                             
                         LEFT OUTER JOIN ContactPersonTel cpt on cp.idContPers= cpt.idContPers                                    
                         WHERE cpt.isDefaultTel='1' and cpt.idContPers='" + idContPers + "'");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;


            return conn.executeSelectQuery(query, sqlParameters);
        }
        public DataTable GetAvailabilityByVolontaryEmail(int idContPers)
        {
            string query = string.Format(@" select  cpe.email             
                         from ContactPerson cp                             
                         LEFT OUTER JOIN ContactPersonEmail cpe on cp.idContPers= cpe.idContPers                                    
                         WHERE cpe.isCommunication='1' and cpe.idContPers='" + idContPers + "'");
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@idContPers", SqlDbType.Int);
            sqlParameters[0].Value = idContPers;


            return conn.executeSelectQuery(query, sqlParameters);
        }
        //============
        public DataTable GetNrVolQueryType()
        {
            string query = string.Format(@" SELECT idQueryType AS ID, nameQueryType as quest
                                           FROM VolQueryType
                                           ORDER BY nameQueryType ");

            return conn.executeSelectQuery(query, null);
        }
    }
}
