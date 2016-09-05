using BIS.Core;
using BIS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIS.DAO
{
    public class VolAvailabilityPreselectionDAO
    {

        private dbConnection conn;

        public VolAvailabilityPreselectionDAO()
        {
            conn = new dbConnection();
        }
        public DataTable GetAllSkills(List<int> label)
        {
            //Dinamicki puni gridLookup kada je selektovana odredjena labela
            string condition = "";

            if (label.Count > 0)
            {

                int count = 0;

                foreach (var idQueryType in label)
                {
                    if (count == 0)
                    {
                        condition += " WHERE idQueryType = '" + idQueryType.ToString() + "' ";
                    }

                    if (count > 0)
                    {
                        condition += " OR idQueryType = '" + idQueryType.ToString() + "' ";
                    }

                    count++;
                }

            }

            string query = string.Format(@"SELECT idQuest,idQuestGroup,txtQuest,idAns,idQueryType,DD.nameLabel
                                            FROM
                                            (
                                            SELECT DISTINCT  vq.idQuest,vq.idQuestGroup,convert(NVARCHAR,vq.txtQuest) AS txtQuest,va.idAns,vq.questSort,va.idQueryType,l.nameLabel
                                            FROM VolQuest vq
                                            LEFT OUTER JOIN VolAns va ON va.idQuest=vq.idQuest
                                            INNER JOIN (SELECT DISTINCT CASE WHEN id IS NOT NULL THEN id ELSE idLabel END as id , nameLabel FROM labels) l ON l.id = va.idQueryType
                                                                                                                                         
                                            ) DD
                                            " + condition + @" 
                                           
                                            ORDER BY questSort");




            return conn.executeSelectQuery(query, null);

        }

        public DataTable GetAllTripPreferences(List<int> label)
        {
            string condition = "";

            if (label.Count > 0)
            {

                int count = 0;

                foreach (var idQueryType in label)
                {
                    if (count == 0)
                    {
                        condition += " WHERE idQueryType = '" + idQueryType.ToString() + "' ";
                    }

                    if (count > 0)
                    {
                        condition += " OR idQueryType = '" + idQueryType.ToString() + "' ";
                    }

                    count++;
                }

            }

            string query = string.Format(@"SELECT idQuest,idQuestGroup,txtQuest,idAns,idQueryType,DD.nameLabel
                                            FROM
                                            (
                                            SELECT DISTINCT  vq.idQuest,vq.idQuestGroup,convert(NVARCHAR,vq.txtQuest) AS txtQuest,va.idAns,vq.questSort,va.idQueryType,l.nameLabel
                                            FROM VolTripQuest vq
                                            LEFT OUTER JOIN VolTripAns va ON va.idQuest=vq.idQuest
                                            INNER JOIN (SELECT DISTINCT CASE WHEN id IS NOT NULL THEN id ELSE idLabel END as id , nameLabel FROM labels) l ON l.id = va.idQueryType
                                                                                                                                         
                                            ) DD
                                            " + condition + @" 
                                           
                                            ORDER BY questSort");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GetAllFunction(List<int> label)
        {
            string condition = "";

            if (label.Count > 0)
            {

                int count = 0;

                foreach (var idQueryType in label)
                {
                    if (count == 0)
                    {
                        condition += " WHERE idQueryType = '" + idQueryType.ToString() + "' ";
                    }

                    if (count > 0)
                    {
                        condition += " OR idQueryType = '" + idQueryType.ToString() + "' ";
                    }

                    count++;
                }

            }

            string query = string.Format(@"SELECT idQuest,idQuestGroup,txtQuest,idAns,idQueryType,DD.nameLabel
                                            FROM
                                            (
                                            SELECT DISTINCT  vq.idQuest,vq.idQuestGroup,convert(NVARCHAR,vq.txtQuest) AS txtQuest,va.idAns,vq.questSort,va.idQueryType,l.nameLabel
                                            FROM VolFunctionQuest vq
                                            LEFT OUTER JOIN VolFunctionAns va ON va.idQuest=vq.idQuest
                                            INNER JOIN (SELECT DISTINCT CASE WHEN id IS NOT NULL THEN id ELSE idLabel END as id , nameLabel FROM labels) l ON l.id = va.idQueryType
                                                                                                                                         
                                            ) DD
                                            " + condition + @" 
                                           
                                            ORDER BY questSort");

            return conn.executeSelectQuery(query, null);
        }




        public DataTable GetContactPersonPreselection(DateTime dtFrom, DateTime dtTo, List<VolAvailabilityPreselectionModel> listFunction, List<VolAvailabilityPreselectionModel> listSkills, List<VolAvailabilityPreselectionModel> listPreferences, List<int> label)
        {
            #region function
            //string tableName = "";
            //string allCondition = "";


            //string conditionFunction = "";
            //if (listFunction != null)
            //    if (listFunction.Count > 0)
            //    {
            //        tableName = "VolFunctionCpr";
            //        for (int i = 0; i < listFunction.Count; i++)
            //        {
            //            if (i == listFunction.Count - 1)
            //                conditionFunction = "(" + conditionFunction + "(idQuest = " + listFunction[i].idQuest + "))";
            //            else
            //                conditionFunction = conditionFunction + "(idQuest = " + listFunction[i].idQuest + ") AND ";
            //        }

            //        conditionFunction = " AND cp.idContPers IN (SELECT DISTINCT idcpr FROM " + tableName + " WHERE " + conditionFunction + ")";
            //    }

            string tableName = "";
            string conditionFunction = "";
            if (listFunction != null)
                if (listFunction.Count > 0)
                {
                    tableName = "VolFunctionCpr";
                    for (int i = 0; i < listFunction.Count; i++)
                    {
                        conditionFunction += " AND cp.idContPers IN (SELECT DISTINCT idcpr FROM " + tableName + " WHERE idQuest = " + listFunction[i].idQuest + ") ";
                    }
                }

            #endregion

            #region skills

            string tableNameSkills = "";
            string allConditionSkills = "";


            string conditionSkills = "";
            if (listSkills != null)
                if (listSkills.Count > 0)
                {
                    tableNameSkills = "VolCpr";
                    for (int i = 0; i < listSkills.Count; i++)
                    {
                        conditionSkills += " AND cp.idContPers IN (SELECT DISTINCT idcpr FROM " + tableNameSkills + " WHERE idQuest = " + listSkills[i].idQuest + ")";
                    }


                }

            //string tableName = "";
            //string conditionFunctionRI = "";
            //if (listFunctionReasonIn != null)
            //    if (listFunctionReasonIn.Count > 0)
            //    {
            //        tableName = "VolFunctionCpr";
            //        for (int i = 0; i < listFunctionReasonIn.Count; i++)
            //        {
            //            conditionFunctionRI += " AND cp.idContPers IN (SELECT DISTINCT idcpr FROM " + tableName + " WHERE idQuest = " + listFunctionReasonIn[i].idQuest + ") ";
            //        }
            //    }

            #endregion

            #region tripPreferences

            string tableNamePreferences = "";
            string allConditionPreferences = "";


            string conditionPreferences = "";
            if (listPreferences != null)
                if (listPreferences.Count > 0)
                {
                    tableNamePreferences = "VolTripCpr";
                    for (int i = 0; i < listPreferences.Count; i++)
                    {
                        conditionPreferences = conditionPreferences + " AND cp.idContPers IN (SELECT DISTINCT idcpr FROM " + tableNamePreferences + " WHERE idQuest = " + listPreferences[i].idQuest + " AND  idAns=" + listPreferences[i].idAns + ")";
                    }
                }

            #endregion

            #region labele


            string conditionLabel = "";

            if (label.Count > 0)
            {

                int count = 0;

                foreach (var idLabel in label)
                {
                    if (idLabel.ToString() != "0")
                    {
                        if (count == 0)
                        {
                            conditionLabel += " and (l.idLabels = '" + idLabel.ToString() + "'  ";
                        }

                        if (count > 0)
                        {
                            conditionLabel += " OR l.idLabels = '" + idLabel.ToString() + "' ";
                        }
                    }
                    count++;
                }

            }

            #endregion

            string query = string.Format(@" SELECT DISTINCT cp.idContPers,t.nameTitle as title , CASE WHEN cp.firstname IS NULL THEN '' ELSE cp.firstname END AS firstname,CASE WHEN cp.midname IS NULL THEN '' ELSE cp.midname END AS midname,   cp.lastname,
     
                                          CASE WHEN ((MONTH(cp.birthdate) * 100) + DAY(cp.birthdate)) > ((MONTH(getdate()) * 100) + DAY(getdate()))
               THEN DATEDIFF(year,cp.birthdate,getdate()) - 1
               ELSE DATEDIFF(year,cp.birthdate,getdate())  End As [Age],
               CASE WHEN va.nrTimes IS NULL THEN 0 ELSE  va.nrTimes  END as Availability,
               CASE WHEN pp.nrBooked IS NULL THEN 0 ELSE pp.nrBooked END nrBooked, '' AS [Functions]               
                                          FROM ContactPerson cp 
                                             LEFT OUTER JOIN ContactPersonFilter cpf ON cpf.idContPers=cp.idContPers
                                             LEFT OUTER JOIN Title t ON t.idTitle=cp.idTitle
 LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= cp.idContPers
                                             LEFT OUTER JOIN Gender g ON g.idGender = cp.idGender
                                             INNER JOIN (
                                             SELECT  idContPers,nrTimes FROM VolAvailability va     
                                             WHERE va.dateFrom  >= @dtFrom and va.dateTo <= @dtTo
                                             
                                             ) va ON va.idContPers = cp.idContPers
LEFT OUTER JOIN (SELECT CASE WHEN id is not NULL then id else idLabel END  as idLabels FROM Labels)  l on l.idLabels= cpl.idLabel
                                             LEFT OUTER JOIN(
                                              SELECT idContPers,COUNT(idContPers) as nrBooked from ArrangementBook ab
                                             LEFT OUTER JOIN Arrangement a ON a.idArrangement = ab.idArrangement
                                             WHERE  (idStatus='1' or idStatus='2') 
                                             AND a.dtFromArrangement  >= @dtFrom and a.dtToArrangement <= @dtTo 
               GROUP BY  idContPers ) pp ON pp.idContPers = cp.idContPers 
                                             
                                             WHERE  cpf.idFilter = '4' AND cp.isActive='0' " + conditionFunction + @"  " + conditionSkills + @"  " + conditionPreferences + @"  " + conditionLabel + ")  ORDER BY cp.lastname  ");



            SqlParameter[] sqlParemeters = new SqlParameter[2];

            sqlParemeters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParemeters[0].Value = dtFrom;

            sqlParemeters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParemeters[1].Value = dtTo;

            return conn.executeSelectQuery(query, sqlParemeters);
        }

        public DataTable GetCok(DateTime dtFrom, DateTime dtTo, DateTime expiredDate, List<int> label)
        {

            string condition = "";

            if (label.Count > 0)
            {

                int count = 0;

                foreach (var idLabel in label)
                {
                    if (count == 0)
                    {
                        condition += " and (l.idLabels = '" + idLabel.ToString() + "' ";
                    }

                    if (count > 0)
                    {
                        condition += " OR l.idLabels = '" + idLabel.ToString() + "' ";
                    }

                    count++;
                }

            }
            string query = string.Format(@"  SELECT distinct cpr.idContPers, cpr.nameTitle as title,
                        CASE WHEN cpr.firstname is not null then cpr.firstname else '' end  as firstName,
                        CASE WHEN cpr.lastname is not null then cpr.lastname else '' end  as  lastName,
                        CASE WHEN cpr.midname is not null then cpr.midname else '' end  as  midName,
                       'COK' as type, dtExpirationDate as dateExpiried,'' as email, '' as phone
                        FROM VolSimilarity vs 
                        LEFT OUTER JOIN (SELECT idContPers, idGender, firstname,midname,t.nameTitle, lastname, birthdate,isActive
                        FROM ContactPerson cp 
                        LEFT OUTER JOIN Title t on cp.idTitle= t.idTitle
                        WHERE idContPers  IN (SELECT DISTINCT a.idContPers FROM ArrangementBook a INNER JOIN ContactPersonFilter f on a.idContPers= f.idContPers WHERE f.idFilter='4' and dtBooked>='" + dtFrom.ToString("MM/dd/yyyy") + "' AND dtBooked<='" + dtTo.ToString("MM/dd/yyyy") + @"' ) ) 
                                cpr on vs.idContPers=cpr.idContPers
                        LEFT OUTER JOIN ContactPersonFilter cpf on cpr.idContPers=cpf.idContPers 
                        LEFT OUTER JOIN Gender g on cpr.idGender= g.idGender 
                        LEFT OUTER JOIN ContactPersonPassport cpp on cpp.idContPers= cpr.idContPers
                        LEFT OUTER JOIN ArrangementBook ab on cpr.idContPers= ab.idContPers
                        LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= cpr.idContPers
                        LEFT OUTER JOIN (SELECT CASE WHEN id is not NULL then id else idLabel END  as idLabels FROM Labels)  l on l.idLabels= cpl.idLabel                             
                        WHERE idSimilarity ='COK'  and cpr.isActive='0' AND dtExpirationDate<='" + expiredDate.ToString("MM/dd/yyyy") + "' AND cpf.idFilter='4' and (ab.idStatus='2' or ab.idStatus='1')  " + condition + ") order by lastName");
            //  WHERE idSimilarity ='COK' AND dtExpirationDate='" + expiredDate.ToString("MM/dd/yyyy") + "' AND cpf.idFilter='4'   " + condition + ") order by lastName");
            return conn.executeSelectQuery(query, null);
        }
/////////////////////////////////////////////


        public DataTable GetVOK(DateTime dtFrom, DateTime dtTo, DateTime expiredDate, List<int> label)
        {

            string condition = "";

            if (label.Count > 0)
            {

                int count = 0;

                foreach (var idLabel in label)
                {
                    if (count == 0)
                    {
                        condition += " and (l.idLabels = '" + idLabel.ToString() + "' ";
                    }

                    if (count > 0)
                    {
                        condition += " OR l.idLabels = '" + idLabel.ToString() + "' ";
                    }

                    count++;
                }

            }

            string query = string.Format(@"  SELECT distinct cpr.idContPers, cpr.nameTitle as title,
                        CASE WHEN cpr.firstname is not null then cpr.firstname else '' end  as firstName,
                        CASE WHEN cpr.lastname is not null then cpr.lastname else '' end  as  lastName,
                        CASE WHEN cpr.midname is not null then cpr.midname else '' end  as  midName,
                       'VOK' as type,dtExpirationDate as dateExpiried,'' as email, '' as phone
                        FROM VolSimilarity vs 
                        LEFT OUTER JOIN (SELECT idContPers, idGender, firstname,midname,t.nameTitle, lastname, birthdate,isActive
                        FROM ContactPerson cp 
                        LEFT OUTER JOIN Title t on cp.idTitle= t.idTitle
                        WHERE idContPers  IN (SELECT DISTINCT a.idContPers FROM ArrangementBook a INNER JOIN ContactPersonFilter f on a.idContPers= f.idContPers WHERE f.idFilter='4' and dtBooked>='" + dtFrom.ToString("MM/dd/yyyy") + "' AND dtBooked<='" + dtTo.ToString("MM/dd/yyyy") + @"' ) ) 
                                cpr on vs.idContPers=cpr.idContPers
                        LEFT OUTER JOIN ContactPersonFilter cpf on cpr.idContPers=cpf.idContPers 
                        LEFT OUTER JOIN Gender g on cpr.idGender= g.idGender 
                        LEFT OUTER JOIN ContactPersonPassport cpp on cpp.idContPers= cpr.idContPers
                        LEFT OUTER JOIN ArrangementBook ab on cpr.idContPers= ab.idContPers
                        LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= cpr.idContPers
                        LEFT OUTER JOIN (SELECT CASE WHEN id is not NULL then id else idLabel END  as idLabels FROM Labels)  l on l.idLabels= cpl.idLabel                             
                        WHERE idSimilarity ='VOK' and cpr.isActive='0' AND dtExpirationDate<='" + expiredDate.ToString("MM/dd/yyyy") + "' AND cpf.idFilter='4' and (ab.idStatus='2' or ab.idStatus='1')  " + condition + ") order by lastName");

            return conn.executeSelectQuery(query, null);
        }

      public DataTable GetPassport(DateTime dtFrom, DateTime dtTo, DateTime expiredDate, List<int> label)
      {
          string condition = "";

          if (label.Count > 0)
          {

              int count = 0;

              foreach (var idLabel in label)
              {
                  if (count == 0)
                  {
                      condition += " and (l.idLabels = '" + idLabel.ToString() + "' ";
                  }

                  if (count > 0)
                  {
                      condition += " OR l.idLabels = '" + idLabel.ToString() + "' ";
                  }

                  count++;
              }

          }

          string query = string.Format(@"  SELECT distinct cpr.idContPers, cpr.nameTitle as title,
                        CASE WHEN cpr.firstname is not null then cpr.firstname else '' end  as firstName,
                        CASE WHEN cpr.lastname is not null then cpr.lastname else '' end  as  lastName,
                        CASE WHEN cpr.midname is not null then cpr.midname else '' end  as  midName,
                       'Pass' as type,cpp.dtPassportValid as dateExpiried,'' as email, '' as phone
                        FROM ContactPersonPassport cpp 
                        LEFT OUTER JOIN (SELECT idContPers, idGender, firstname,midname,t.nameTitle, lastname, birthdate,isActive
                        FROM ContactPerson cp 
                        LEFT OUTER JOIN Title t on cp.idTitle= t.idTitle
                        WHERE idContPers  IN (SELECT DISTINCT a.idContPers FROM ArrangementBook a INNER JOIN ContactPersonFilter f on a.idContPers= f.idContPers WHERE f.idFilter='4' and dtBooked>='" + dtFrom.ToString("MM/dd/yyyy") + "' AND dtBooked<='" + dtTo.ToString("MM/dd/yyyy") + @"' ) ) 
                                cpr on cpp.idContPers=cpr.idContPers
                        LEFT OUTER JOIN ContactPersonFilter cpf on cpr.idContPers=cpf.idContPers 
                        LEFT OUTER JOIN Gender g on cpr.idGender= g.idGender 
                       -- LEFT OUTER JOIN ContactPersonPassport cpp on cpp.idContPers= cpr.idContPers
                        LEFT OUTER JOIN ArrangementBook ab on cpr.idContPers= ab.idContPers
                        LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= cpr.idContPers
                        LEFT OUTER JOIN (SELECT CASE WHEN id is not NULL then id else idLabel END  as idLabels FROM Labels)  l on l.idLabels= cpl.idLabel                             
                        WHERE  cpp.dtPassportValid<='" + expiredDate.ToString("MM/dd/yyyy") + "' and cpr.isActive='0' AND cpf.idFilter='4' and (ab.idStatus='2' or ab.idStatus='1')  " + condition + ") order by lastName");


          return conn.executeSelectQuery(query, null);
      }


      public DataTable GetAllVokCokGokPass(DateTime dtFrom, DateTime dtTo, DateTime expiredDate, List<int> label)
      {
          string condition = "";

          if (label.Count > 0)
          {

              int count = 0;

              foreach (var idLabel in label)
              {
                  if (count == 0)
                  {
                      condition += " and (l.idLabels = '" + idLabel.ToString() + "' ";
                  }

                  if (count > 0)
                  {
                      condition += " OR l.idLabels = '" + idLabel.ToString() + "' ";
                  }

                  count++;
              }

          }
          string query = string.Format(@"  SELECT distinct cpr.idContPers, cpr.nameTitle as title,
                        CASE WHEN cpr.firstname is not null then cpr.firstname else '' end  as firstName,
                        CASE WHEN cpr.lastname is not null then cpr.lastname else '' end  as  lastName,
                        CASE WHEN cpr.midname is not null then cpr.midname else '' end  as  midName,
                       'VOK' as type,dtExpirationDate as dateExpiried,'' as email, '' as phone
                        FROM VolSimilarity vs 
                        LEFT OUTER JOIN (SELECT idContPers, idGender, firstname,midname,t.nameTitle, lastname, birthdate,isActive
                        FROM ContactPerson cp 
                        LEFT OUTER JOIN Title t on cp.idTitle= t.idTitle
                        WHERE idContPers  IN (SELECT DISTINCT a.idContPers FROM ArrangementBook a INNER JOIN ContactPersonFilter f on a.idContPers= f.idContPers WHERE f.idFilter='4' and dtBooked>='" + dtFrom.ToString("MM/dd/yyyy") + "' AND dtBooked<='" + dtTo.ToString("MM/dd/yyyy") + @"' ) ) 
                                cpr on vs.idContPers=cpr.idContPers
                        LEFT OUTER JOIN ContactPersonFilter cpf on cpr.idContPers=cpf.idContPers 
                        LEFT OUTER JOIN Gender g on cpr.idGender= g.idGender 
                        LEFT OUTER JOIN ContactPersonPassport cpp on cpp.idContPers= cpr.idContPers
                        LEFT OUTER JOIN ArrangementBook ab on cpr.idContPers= ab.idContPers
                        LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= cpr.idContPers
                        LEFT OUTER JOIN (SELECT CASE WHEN id is not NULL then id else idLabel END  as idLabels FROM Labels)  l on l.idLabels= cpl.idLabel                             
                        WHERE idSimilarity ='VOK' AND dtExpirationDate<='" + expiredDate.ToString("MM/dd/yyyy") + "' and cpr.isActive='0' AND cpf.idFilter='4' and (ab.idStatus='2' or ab.idStatus='1')  " + condition + ") " +

                     @"UNION     
                       SELECT distinct cpr.idContPers, cpr.nameTitle as title,
                       CASE WHEN cpr.firstname is not null then cpr.firstname else '' end  as firstName,
                        CASE WHEN cpr.lastname is not null then cpr.lastname else '' end  as  lastName,
                        CASE WHEN cpr.midname is not null then cpr.midname else '' end  as  midName,
                       'COK' as type,dtExpirationDate as dateExpiried,'' as email, '' as phone
                        FROM VolSimilarity vs 
                        LEFT OUTER JOIN (SELECT idContPers, idGender, firstname,midname,t.nameTitle, lastname, birthdate,isActive
                        FROM ContactPerson cp 
                        LEFT OUTER JOIN Title t on cp.idTitle= t.idTitle
                        WHERE idContPers  IN (SELECT DISTINCT a.idContPers FROM ArrangementBook a INNER JOIN ContactPersonFilter f on a.idContPers= f.idContPers WHERE f.idFilter='4' and dtBooked>='" + dtFrom.ToString("MM/dd/yyyy") + "' AND dtBooked<='" + dtTo.ToString("MM/dd/yyyy") + @"' ) ) 
                                cpr on vs.idContPers=cpr.idContPers
                        LEFT OUTER JOIN ContactPersonFilter cpf on cpr.idContPers=cpf.idContPers 
                        LEFT OUTER JOIN Gender g on cpr.idGender= g.idGender 
                        LEFT OUTER JOIN ContactPersonPassport cpp on cpp.idContPers= cpr.idContPers
                        LEFT OUTER JOIN ArrangementBook ab on cpr.idContPers= ab.idContPers
                        LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= cpr.idContPers
                        LEFT OUTER JOIN (SELECT CASE WHEN id is not NULL then id else idLabel END  as idLabels FROM Labels)  l on l.idLabels= cpl.idLabel                             
                        WHERE idSimilarity ='COK' AND dtExpirationDate<='" + expiredDate.ToString("MM/dd/yyyy") + "' and cpr.isActive='0' AND cpf.idFilter='4' and (ab.idStatus='2' or ab.idStatus='1')  " + condition + ") " +

                     @"  UNION
                        SELECT distinct cpr.idContPers, cpr.nameTitle as title,
                        CASE WHEN cpr.firstname is not null then cpr.firstname else '' end  as firstName,
                        CASE WHEN cpr.lastname is not null then cpr.lastname else '' end  as  lastName,
                        CASE WHEN cpr.midname is not null then cpr.midname else '' end  as  midName,
                       'VOG' as type,dtExpirationDate as dateExpiried,'' as email, '' as phone
                        FROM VolSimilarity vs 
                        LEFT OUTER JOIN (SELECT idContPers, idGender, firstname,midname,t.nameTitle, lastname, birthdate,isActive
                        FROM ContactPerson cp 
                        LEFT OUTER JOIN Title t on cp.idTitle= t.idTitle
                        WHERE idContPers  IN (SELECT DISTINCT a.idContPers FROM ArrangementBook a INNER JOIN ContactPersonFilter f on a.idContPers= f.idContPers WHERE f.idFilter='4' and dtBooked>='" + dtFrom.ToString("MM/dd/yyyy") + "' AND dtBooked<='" + dtTo.ToString("MM/dd/yyyy") + @"' ) ) 
                                cpr on vs.idContPers=cpr.idContPers
                        LEFT OUTER JOIN ContactPersonFilter cpf on cpr.idContPers=cpf.idContPers 
                        LEFT OUTER JOIN Gender g on cpr.idGender= g.idGender 
                        LEFT OUTER JOIN ContactPersonPassport cpp on cpp.idContPers= cpr.idContPers
                        LEFT OUTER JOIN ArrangementBook ab on cpr.idContPers= ab.idContPers
                        LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= cpr.idContPers
                        LEFT OUTER JOIN (SELECT CASE WHEN id is not NULL then id else idLabel END  as idLabels FROM Labels)  l on l.idLabels= cpl.idLabel                             
                        WHERE idSimilarity ='VOG' AND dtExpirationDate<='" + expiredDate.ToString("MM/dd/yyyy") + "' and cpr.isActive='0' AND cpf.idFilter='4' and (ab.idStatus='2' or ab.idStatus='1')  " + condition + ") " +

                     @"UNION 
                        SELECT distinct cpr.idContPers, cpr.nameTitle as title,
                        CASE WHEN cpr.firstname is not null then cpr.firstname else '' end  as firstName,
                        CASE WHEN cpr.lastname is not null then cpr.lastname else '' end  as  lastName,
                        CASE WHEN cpr.midname is not null then cpr.midname else '' end  as  midName,
                       'Pass' as type,cpp.dtPassportValid as dateExpiried,'' as email, '' as phone
                        FROM ContactPersonPassport cpp 
                        LEFT OUTER JOIN (SELECT idContPers, idGender, firstname,midname,t.nameTitle, lastname, birthdate,isActive
                        FROM ContactPerson cp 
                        LEFT OUTER JOIN Title t on cp.idTitle= t.idTitle
                        WHERE idContPers  IN (SELECT DISTINCT a.idContPers FROM ArrangementBook a INNER JOIN ContactPersonFilter f on a.idContPers= f.idContPers WHERE f.idFilter='4' and dtBooked>='" + dtFrom.ToString("MM/dd/yyyy") + "' AND dtBooked<='" + dtTo.ToString("MM/dd/yyyy") + @"' ) ) 
                                cpr on cpp.idContPers=cpr.idContPers
                        LEFT OUTER JOIN ContactPersonFilter cpf on cpr.idContPers=cpf.idContPers 
                        LEFT OUTER JOIN Gender g on cpr.idGender= g.idGender 
                       -- LEFT OUTER JOIN ContactPersonPassport cpp on cpp.idContPers= cpr.idContPers
                        LEFT OUTER JOIN ArrangementBook ab on cpr.idContPers= ab.idContPers
                        LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= cpr.idContPers
                        LEFT OUTER JOIN (SELECT CASE WHEN id is not NULL then id else idLabel END  as idLabels FROM Labels)  l on l.idLabels= cpl.idLabel                             
                        WHERE  cpp.dtPassportValid<='" + expiredDate.ToString("MM/dd/yyyy") + "' and cpr.isActive='0' AND cpf.idFilter='4' and (ab.idStatus='2' or ab.idStatus='1')  " + condition + ") order by lastName");

          return conn.executeSelectQuery(query, null);
      }
        public DataTable GetGOK(DateTime dtFrom, DateTime dtTo, DateTime expiredDate, List<int> label)
        {
            string condition = "";

            if (label.Count > 0)
            {

                int count = 0;

                foreach (var idLabel in label)
                {
                    if (count == 0)
                    {
                        condition += " and (l.idLabels = '" + idLabel.ToString() + "' ";
                    }

                    if (count > 0)
                    {
                        condition += " OR l.idLabels = '" + idLabel.ToString() + "' ";
                    }

                    count++;
                }

            }

            string query = string.Format(@"  SELECT distinct l.idLabels,cpr.idContPers, cpr.nameTitle as title,
                        CASE WHEN cpr.firstname is not null then cpr.firstname else '' end  as firstName,
                        CASE WHEN cpr.lastname is not null then cpr.lastname else '' end  as  lastName,
                        CASE WHEN cpr.midname is not null then cpr.midname else '' end  as  midName,
                       'VOG' as type,dtExpirationDate as dateExpiried,'' as email, '' as phone
                        FROM VolSimilarity vs 
                        LEFT OUTER JOIN (SELECT idContPers, idGender, firstname,midname,t.nameTitle, lastname, birthdate,isActive
                        FROM ContactPerson cp 
                        LEFT OUTER JOIN Title t on cp.idTitle= t.idTitle
                        WHERE idContPers  IN (SELECT DISTINCT a.idContPers FROM ArrangementBook a INNER JOIN ContactPersonFilter f on a.idContPers= f.idContPers WHERE f.idFilter='4' and dtBooked>='" + dtFrom.ToString("MM/dd/yyyy") + "' AND dtBooked<='" + dtTo.ToString("MM/dd/yyyy") + @"' ) ) 
                                cpr on vs.idContPers=cpr.idContPers
                        LEFT OUTER JOIN ContactPersonFilter cpf on cpr.idContPers=cpf.idContPers 
                        LEFT OUTER JOIN Gender g on cpr.idGender= g.idGender 
                        LEFT OUTER JOIN ContactPersonPassport cpp on cpp.idContPers= cpr.idContPers
                        LEFT OUTER JOIN ArrangementBook ab on cpr.idContPers= ab.idContPers
                        LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= cpr.idContPers
                        LEFT OUTER JOIN (SELECT CASE WHEN id is not NULL then id else idLabel END  as idLabels FROM Labels)  l on l.idLabels= cpl.idLabel                             
                        WHERE idSimilarity ='VOG' and cpr.isActive='0' AND dtExpirationDate<='" + expiredDate.ToString("MM/dd/yyyy") + "' AND cpf.idFilter='4' and (ab.idStatus='2' or ab.idStatus='1')  " + condition + ") order by lastName");

            return conn.executeSelectQuery(query, null);
        }

        public DataTable GeExitListData(DateTime dtFrom, DateTime dtTo, List<VolAvailabilityPreselectionModel> listFunction, List<int> label)
        {
            string conditionLabel = "";

            #region Label

            if (label.Count > 0)
            {

                int count = 0;

                foreach (var idLabel in label)
                {
                    if (count == 0)
                    {
                        conditionLabel += " and (l.idLabels = '" + idLabel.ToString() + "' ";
                    }

                    if (count > 0)
                    {
                        conditionLabel += " OR l.idLabels = '" + idLabel.ToString() + "' ";
                    }

                    count++;
                }

            }
            conditionLabel += ")";
            #endregion



            string tableName = "";
            string conditionFunction = "";
            if (listFunction != null)
                if (listFunction.Count > 0)
                {
                    tableName = "VolFunctionCpr";
                    for (int i = 0; i < listFunction.Count; i++)
                    {
                        conditionFunction += " AND cp.idContPers IN (SELECT DISTINCT idcpr FROM " + tableName + " WHERE idQuest = " + listFunction[i].idQuest + ") ";
                    }
                }

            string query = string.Format(@"SELECT distinct cp.idContPers, t.nameTitle as title,
                        CASE WHEN cp.firstname is not null then cp.firstname else '' end  as firstName,
                        CASE WHEN cp.lastname is not null then cp.lastname else '' end  as  lastName,
                        CASE WHEN cp.midname is not null then cp.midname else '' end  as  midName,
                        case 
	                    when ((MONTH(cp.birthdate) * 100) + DAY(cp.birthdate)) > ((MONTH(getdate()) * 100) + DAY(getdate()))
                        then DATEDIFF(year,cp.birthdate,getdate()) - 1
	                    else DATEDIFF(year,cp.birthdate,getdate()) End As age, '' as email, '' as phone,

 
	                    (SELECT Count(idContPers)+ (SELECT oldTripCount FROM ContactPerson WHERE idContPers=cp.idContPers) as br FROM ArrangementBook a
                        LEFT JOIN Arrangement e on e.idArrangement = a.idArrangement
						WHERE idContPers=cp.idContPers and idStatus < 3 and LEFT(e.codeArrangement,3) != 'TRC'
					    AND  LEFT(e.codeArrangement,3) != 'BHC' AND dtBooked >= '09-01-2016 00:00:000' ) as NrTravel

                        FROM ( SELECT distinct a.idContPers,a.dtBooked,cf.idFilter,a.idStatus FROM ArrangementBook a
                             LEFT OUTER JOIN ContactPersonFilter cf on a.idContPers= cf.idContPers
                             where idFilter='4' AND (a.dtBooked<'" + dtFrom.ToString("MM/dd/yyyy") + "' OR a.dtBooked>'" + dtTo.ToString("MM/dd/yyyy") + @"') ) ab
                        LEFT OUTER JOIN ContactPerson cp on ab.idContPers=cp.idContPers
                        LEFT OUTER JOIN ContactPersonFilter cpf on cp.idContPers= cpf.idContPers
                        LEFT OUTER JOIN Title t on cp.idTitle= t.idTitle
                        LEFT OUTER JOIN Gender g on cp.idGender= g.idGender         
                        LEFT OUTER JOIN VolFunctionCpr vcpr  on vcpr.idcpr= ab.idContPers
                        LEFT OUTER JOIN(SELECT DISTINCT  convert(NVARCHAR,vq.txtQuest) AS txtQuest,vq.idQuest
                         FROM VolFunctionQuest vq
                          LEFT OUTER JOIN VolFunctionAns va ON va.idQuest=vq.idQuest) DD ON DD.idQuest = vcpr.idQuest 
                        LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= ab.idContPers
                        LEFT OUTER JOIN (SELECT CASE WHEN id is not NULL then id else idLabel END  as idLabels FROM Labels)  l on l.idLabels= cpl.idLabel    
                        WHERE cpf.idFilter='4' " + conditionFunction + conditionLabel + " and (ab.idStatus='2' or ab.idStatus='1') AND ab.dtBooked<'" + dtFrom.ToString("MM/dd/yyyy") + "' OR ab.dtBooked>'" + dtTo.ToString("MM/dd/yyyy") + "' order by lastName ");


            return conn.executeSelectQuery(query, null);
        }
        public DataTable GetContactPersonReasionIn(DateTime dtFrom, DateTime dtTo, List<VolAvailabilityPreselectionModel> listFunctionReasonIn, List<int> label, List<VoluntaryReasonInModel> reasonIn)
        {
            #region function
            //string tableName = "";
            //string allCondition = "";


            //string conditionFunctionRI = "";
            //if (listFunctionReasonIn != null)
            //    if (listFunctionReasonIn.Count > 0)
            //    {
            //        tableName = "VolFunctionCpr";
            //        for (int i = 0; i < listFunctionReasonIn.Count; i++)
            //        {
            //            if (i == listFunctionReasonIn.Count - 1)
            //                conditionFunctionRI = "(" + conditionFunctionRI + "(idQuest = " + listFunctionReasonIn[i].idQuest + "))";
            //            else
            //                conditionFunctionRI = conditionFunctionRI + "(idQuest = " + listFunctionReasonIn[i].idQuest + ") AND ";
            //        }

            //        conditionFunctionRI = " AND cp.idContPers IN (SELECT DISTINCT idcpr FROM " + tableName + " WHERE " + conditionFunctionRI + ")";
            //    }

            string tableName = "";
            string conditionFunctionRI = "";
            if (listFunctionReasonIn != null)
                if (listFunctionReasonIn.Count > 0)
                {
                    tableName = "VolFunctionCpr";
                    for (int i = 0; i < listFunctionReasonIn.Count; i++)
                    {
                        conditionFunctionRI += " AND cp.idContPers IN (SELECT DISTINCT idcpr FROM " + tableName + " WHERE idQuest = " + listFunctionReasonIn[i].idQuest + ") ";
                    }
                }

            #endregion

            #region labele


            string conditionLabel = "";

            if (label.Count > 0)
            {

                int count = 0;

                foreach (var idLabel in label)
                {
                    if (idLabel.ToString() != "0")
                    {
                        if (count == 0)
                        {
                            conditionLabel += " and (cpl.idLabel = '" + idLabel.ToString() + "'  ";
                        }

                        if (count > 0)
                        {
                            conditionLabel += " OR cpl.idLabel = '" + idLabel.ToString() + "' ";
                        }
                    }
                    count++;
                }

            }

            #endregion

            #region reason In
            string conditionReasonIn = "";
            if (reasonIn != null)
                if (reasonIn.Count > 0)
                {

                    int count = 0;

                    foreach (VoluntaryReasonInModel idReasonIn in reasonIn)
                    {
                        if (count == reasonIn.Count - 1)
                        {
                            conditionReasonIn = " AND (" + conditionReasonIn + " cp.idReasonIn = '" + idReasonIn.idReasonIn.ToString() + "')";
                        }

                        else
                        {
                            conditionReasonIn += " cp.idReasonIn = '" + idReasonIn.idReasonIn.ToString() + "'  OR  ";
                        }

                        count++;
                    }
                }
            #endregion


            string query = string.Format(@" SELECT DISTINCT CASE WHEN cp.firstname IS NULL THEN '' ELSE cp.firstname END AS firstname ,
                            cp.lastname,CASE WHEN ((MONTH(cp.birthdate) * 100) + DAY(cp.birthdate)) > ((MONTH(getdate()) * 100) + DAY(getdate()))
                             THEN DATEDIFF(year,cp.birthdate,getdate()) - 1
                            ELSE DATEDIFF(year,cp.birthdate,getdate())  End AS [Age],
                            CASE WHEN  cp.midname IS NULL THEN '' ELSE cp.midname END AS midname ,
                            t.nameTitle as title,cp.idContPers,
                            CASE WHEN vri.nameReasonIn IS NULL THEN '' ELSE vri.nameReasonIn END as nameReasonIn, '' AS [functions]
                            FROM ContactPerson cp                           
                            LEFT OUTER JOIN Title t ON t.idTitle=cp.idTitle
LEFT OUTER JOIN ContactPersonFilter cpf ON cpf.idContPers=cp.idContPers
LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= cp.idContPers

                            LEFT OUTER JOIN VoluntaryReasonIn vri ON vri.idReasonIn=cp.idReasonIn
  
                            WHERE cpf.idFilter = '4' and cp.IsActive='0' AND cp.dtCreated BETWEEN @dtFrom AND @dtTo  " + conditionFunctionRI + " " + conditionLabel + ")  " + conditionReasonIn + " ORDER BY cp.lastname ");


            SqlParameter[] sqlParemeters = new SqlParameter[2];

            sqlParemeters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParemeters[0].Value = dtFrom;

            sqlParemeters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParemeters[1].Value = dtTo;

            return conn.executeSelectQuery(query, sqlParemeters);
        }

        #region ReasonOut
        public DataTable GetAllForReasonOut(DateTime dtFrom, DateTime dtTo, List<VolAvailabilityPreselectionModel> listFunction, List<VoluntaryReasonOutModel> reasonOut, List<int> label)
        {
            #region function
            string tableName = "";
            string conditionFunction = "";
            if (listFunction != null)
                if (listFunction.Count > 0)
                {
                    tableName = "VolFunctionCpr";
                    for (int i = 0; i < listFunction.Count; i++)
                    {
                        conditionFunction += " AND cp.idContPers IN (SELECT DISTINCT idcpr FROM " + tableName + " WHERE idQuest = " + listFunction[i].idQuest + ") ";
                    }
                }
            #endregion

            #region label
            string condition = "";
            if (label != null)
                if (label.Count > 0)
                {
                    int count = 0;

                    foreach (var idLabel in label)
                    {
                        if (idLabel.ToString() != "0")
                            if (count == label.Count - 1)
                            {
                                condition = " AND (" + condition + " cpl.idLabel = '" + idLabel.ToString() + "')";
                            }
                            else
                            {
                                condition += " cpl.idLabel = '" + idLabel.ToString() + "' OR ";
                            }

                        count++;
                    }
                }
            #endregion

            #region reason out
            string conditionReasonOut = "";
            if (reasonOut != null)
                if (reasonOut.Count > 0)
                {

                    int count = 0;

                    foreach (VoluntaryReasonOutModel idReasonOut in reasonOut)
                    {
                        if (count == reasonOut.Count - 1)
                        {
                            conditionReasonOut = " AND (" + conditionReasonOut + " cp.idReasonOut = '" + idReasonOut.idReasonOut.ToString() + "')";
                        }
                        else
                        {
                            conditionReasonOut += " cp.idReasonOut = '" + idReasonOut.idReasonOut.ToString() + "'  OR  ";
                        }

                        count++;
                    }
                }
            #endregion
            string query = string.Format(@"SELECT cp.idContPers, t.nameTitle as title, cp.firstname, cp.midname, cp.lastname,'' as functions, CASE WHEN ((MONTH(cp.birthdate) * 100) + DAY(cp.birthdate)) > ((MONTH(getdate()) * 100) + DAY(getdate()))
             THEN DATEDIFF(year,cp.birthdate,getdate()) - 1
             ELSE DATEDIFF(year,cp.birthdate,getdate())  End as age, vro.nameReasonOut, '' as email, '' as telephoner
             FROM ContactPerson as cp
                                           LEFT JOIN Title as t on t.idTitle = cp.idTitle
                                           LEFT JOIN VoluntaryReasonOut as vro on vro.idReasonOut=cp.idReasonOut
                                           LEFT JOIN ContactPersonLabel cpl ON cpl.idContPers = cp.idContPers
                                           WHERE cp.isActive = 1 and cp.dtOfActive>=@dtFrom and cp.dtOfActive<=@dtTo" + conditionFunction + condition + conditionReasonOut + @"
                                            AND cp.idContPers IN (SELECT DISTINCT idContPers FROM ContactPersonFilter where idFilter = '4')
            
                                            ORDER BY cp.lastname");

            SqlParameter[] sqlParemeters = new SqlParameter[2];

            sqlParemeters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParemeters[0].Value = dtFrom;

            sqlParemeters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParemeters[1].Value = dtTo;

            return conn.executeSelectQuery(query, sqlParemeters);
        }
        //
        public DataTable GetReasonOut()
        {
            string query = string.Format(@"SELECT idReasonOut, nameReasonOut
             FROM VoluntaryReasonOut");//+ condition
            return conn.executeSelectQuery(query, null);
        }
        #endregion


        
        public DataTable GetNotBookedData(DateTime dtFrom, DateTime dtTo, List<VolAvailabilityPreselectionModel> listFunction, List<int> label)
        {
            string conditionLabel = "";

            #region Label

            if (label.Count > 0)
            {

                int count = 0;

                foreach (var idLabel in label)
                {
                    if (count == 0)
                    {
                        conditionLabel += " and (l.idLabels = '" + idLabel.ToString() + "' ";
                    }

                    if (count > 0)
                    {
                        conditionLabel += " OR l.idLabels = '" + idLabel.ToString() + "' ";
                    }

                    count++;
                }

            }
            conditionLabel += ")";
            #endregion



            string tableName = "";
            string conditionFunction = "";
            if (listFunction != null)
                if (listFunction.Count > 0)
                {
                    tableName = "VolFunctionCpr";
                    for (int i = 0; i < listFunction.Count; i++)
                    {
                        conditionFunction += " AND cp.idContPers IN (SELECT DISTINCT idcpr FROM " + tableName + " WHERE idQuest = " + listFunction[i].idQuest + ") ";
                    }
                }

            string query = string.Format(@"SELECT distinct cp.idContPers, t.nameTitle as title,
                        CASE WHEN cp.firstname is not null then cp.firstname else '' end  as firstName,
                        CASE WHEN cp.lastname is not null then cp.lastname else '' end  as  lastName,
                        CASE WHEN cp.midname is not null then cp.midname else '' end  as  midName,
                        case 
	                    when ((MONTH(cp.birthdate) * 100) + DAY(cp.birthdate)) > ((MONTH('" + dtFrom.ToString("MM/dd/yyyy") + @"') * 100) + DAY('" + dtFrom.ToString("MM/dd/yyyy") + @"'))
                        then DATEDIFF(year,cp.birthdate,'" + dtFrom.ToString("MM/dd/yyyy") + @"') - 1
	                    else DATEDIFF(year,cp.birthdate,'" + dtFrom.ToString("MM/dd/yyyy") + @"') End As age, '' as email, '' as phone
                        FROM ( SELECT va.idContPers,cf.idFilter, va.nrTimes
                        -COUNT(a.idArrangementBook) as number
                             FROM ContactPersonFilter cf 
                             LEFT OUTER JOIN VolAvailability va on va.idContPers= cf.idContPers
                             LEFT OUTER JOIN ArrangementBook a on va.idContPers= a.idContPers   and (a.idStatus = '1' OR a.idStatus = '2')
                             LEFT OUTER JOIN Arrangement ar ON a.idArrangement = ar.idArrangement AND (ar.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + @"' AND ar.dtToArrangement<='" + dtTo.ToString("MM/dd/yyyy") + @"')
                             where idFilter='4' 
                             and va.dateFrom<='" + dtFrom.ToString("MM/dd/yyyy") + @"'
                             and va.dateTo>='" + dtTo.ToString("MM/dd/yyyy") + @"' 
                             GROUP BY va.idContPers,cf.idFilter,va.nrTimes
                             HAVING va.nrTimes-COUNT(a.idArrangementBook)>0
                             ) ab
                        LEFT OUTER JOIN ContactPerson cp on ab.idContPers=cp.idContPers
                        LEFT OUTER JOIN ContactPersonFilter cpf on cp.idContPers= cpf.idContPers
                        LEFT OUTER JOIN Title t on cp.idTitle= t.idTitle
                        LEFT OUTER JOIN Gender g on cp.idGender= g.idGender         
                        LEFT OUTER JOIN VolFunctionCpr vcpr  on vcpr.idcpr= ab.idContPers
                        LEFT OUTER JOIN(SELECT DISTINCT  convert(NVARCHAR,vq.txtQuest) AS txtQuest,vq.idQuest
                         FROM VolFunctionQuest vq
                          LEFT OUTER JOIN VolFunctionAns va ON va.idQuest=vq.idQuest) DD ON DD.idQuest = vcpr.idQuest 
                        LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= ab.idContPers
                        LEFT OUTER JOIN (SELECT CASE WHEN id is not NULL then id else idLabel END  as idLabels FROM Labels)  l on l.idLabels= cpl.idLabel    
                        WHERE cp.IsActive='0' AND cpf.idFilter='4' "  + conditionFunction + conditionLabel + @"  
                        AND ab.idContPers NOT IN
                        (  SELECT ab.idContPers FROM ( SELECT distinct a.idContPers FROM ArrangementBook a
                        LEFT OUTER JOIN Arrangement ar ON a.idArrangement = ar.idArrangement
                             LEFT OUTER JOIN ContactPersonFilter cf on a.idContPers= cf.idContPers
                             where idFilter='4' AND (ar.dtFromArrangement>='" + dtFrom.ToString("MM/dd/yyyy") + @"' AND ar.dtToArrangement<='" + dtTo.ToString("MM/dd/yyyy") + @"') and (a.idStatus = '1' OR a.idStatus = '2')) ab)

                        order by lastName ");


            return conn.executeSelectQuery(query, null);
        }

//        public DataTable UniqueVolunteers(DateTime dtFrom, DateTime dtTo, List<VolAvailabilityPreselectionModel> listFunctionUv,List<int>label)
//        {
//            #region function
           
//            string tableNameUv = "";
//            string conditionFunctionUV = "";
//            if (listFunctionUv != null)
//                if (listFunctionUv.Count > 0)
//                {
//                    tableNameUv = "VolFunctionCpr";
//                    for (int i = 0; i < listFunctionUv.Count; i++)
//                    {
//                        conditionFunctionUV += " AND cp.idContPers IN (SELECT DISTINCT idcpr FROM " + tableNameUv + " WHERE idQuest = " + listFunctionUv[i].idQuest + ") ";
//                    }
//                }

//            #endregion

//            #region labele


//            string conditionLabelUV = "";

//            if (label.Count > 0)
//            {

//                int count = 0;

//                foreach (var idLabel in label)
//                {
//                    if (idLabel.ToString() != "0")
//                    {
//                        if (count == 0)
//                        {
//                            conditionLabelUV += " and (cpl.idLabel = '" + idLabel.ToString() + "'  ";
//                        }

//                        if (count > 0)
//                        {
//                            conditionLabelUV += " OR cpl.idLabel = '" + idLabel.ToString() + "' " ;
//                        }
//                    }
//                    count++;
//                }

//            }

//            #endregion

//            string query = string.Format(@" SELECT DISTINCT CASE WHEN cp.firstname IS NULL THEN '' ELSE cp.firstname END AS firstname ,
//                            cp.lastname,CASE WHEN ((MONTH(cp.birthdate) * 100) + DAY(cp.birthdate)) > ((MONTH(getdate()) * 100) + DAY(getdate()))
//                             THEN DATEDIFF(year,cp.birthdate,getdate()) - 1
//                            ELSE DATEDIFF(year,cp.birthdate,getdate())  End AS [Age],
//                            CASE WHEN  cp.midname IS NULL THEN '' ELSE cp.midname END AS midname ,
//                            t.nameTitle as title,cp.idContPers,	 '' AS [functions]
//                            FROM ContactPerson cp                           
//                            LEFT OUTER JOIN Title t ON t.idTitle=cp.idTitle
//LEFT OUTER JOIN ContactPersonFilter cpf ON cpf.idContPers=cp.idContPers
//LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= cp.idContPers
//
//  
//                            WHERE cpf.idFilter = '4' and cp.IsActive='0' AND cp.dtCreated BETWEEN @dtFrom AND @dtTo   " + conditionLabelUV +  @" ) 
//                            
//                           AND cp.idContPers in(
//                                              SELECT DISTINCT idContPers from ArrangementBook ab
//                                             LEFT OUTER JOIN Arrangement a ON a.idArrangement = ab.idArrangement
//                                             WHERE  (idStatus='1' or idStatus='2') 
//                                             AND a.dtFromArrangement  >= @dtFrom and a.dtToArrangement <= @dtTo	  ) " +conditionFunctionUV+@" 
//                            
//                            ORDER BY cp.lastname ");

//            SqlParameter[] sqlParemeters = new SqlParameter[2];

//            sqlParemeters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
//            sqlParemeters[0].Value = dtFrom;

//            sqlParemeters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
//            sqlParemeters[1].Value = dtTo;

//            return conn.executeSelectQuery(query, sqlParemeters);
//        }
        public DataTable UniqueVolunteers(DateTime dtFrom, DateTime dtTo, List<VolAvailabilityPreselectionModel> listFunctionUv, List<int> label)
        {
            #region function

            string tableNameUv = "";
            string conditionFunctionUV = "";
            if (listFunctionUv != null)
                if (listFunctionUv.Count > 0)
                {
                    tableNameUv = "VolFunctionCpr";
                    for (int i = 0; i < listFunctionUv.Count; i++)
                    {
                        conditionFunctionUV += " AND cp.idContPers IN (SELECT DISTINCT idcpr FROM " + tableNameUv + " WHERE idQuest = " + listFunctionUv[i].idQuest + ") ";
                    }
                }

            #endregion

            #region labele


            string conditionLabelUV = "";

            if (label.Count > 0)
            {

                int count = 0;

                foreach (var idLabel in label)
                {
                    if (idLabel.ToString() != "0")
                    {
                        if (count == 0)
                        {
                            conditionLabelUV += " and (cpl.idLabel = '" + idLabel.ToString() + "'  ";
                        }

                        if (count > 0)
                        {
                            conditionLabelUV += " OR cpl.idLabel = '" + idLabel.ToString() + "' ";
                        }
                    }
                    count++;
                }

            }

            #endregion

            string query = string.Format(@" SELECT DISTINCT CASE WHEN cp.firstname IS NULL THEN '' ELSE cp.firstname END AS firstname ,
                            cp.lastname,CASE WHEN ((MONTH(cp.birthdate) * 100) + DAY(cp.birthdate)) > ((MONTH(getdate()) * 100) + DAY(getdate()))
                             THEN DATEDIFF(year,cp.birthdate,getdate()) - 1
                            ELSE DATEDIFF(year,cp.birthdate,getdate())  End AS [Age],
                            CASE WHEN  cp.midname IS NULL THEN '' ELSE cp.midname END AS midname ,
                            t.nameTitle as title,cp.idContPers,	 '' AS [functions]
                            FROM ContactPerson cp                           
                            LEFT OUTER JOIN Title t ON t.idTitle=cp.idTitle
LEFT OUTER JOIN ContactPersonFilter cpf ON cpf.idContPers=cp.idContPers
LEFT OUTER JOIN ContactPersonLabel cpl on cpl.idContPers= cp.idContPers

  
                            WHERE cpf.idFilter = '4' and cp.IsActive='0'
  " + conditionLabelUV + @" ) 
                            
                           AND cp.idContPers in(
                                              SELECT DISTINCT idContPers from ArrangementBook ab
                                             LEFT OUTER JOIN Arrangement a ON a.idArrangement = ab.idArrangement
                                             WHERE  (idStatus='1' or idStatus='2') 
                                             AND a.dtFromArrangement  >= @dtFrom and a.dtToArrangement <= @dtTo	  ) " + conditionFunctionUV + @" 
                            
                            ORDER BY cp.lastname ");

            SqlParameter[] sqlParemeters = new SqlParameter[2];

            sqlParemeters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParemeters[0].Value = dtFrom;

            sqlParemeters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParemeters[1].Value = dtTo;

            return conn.executeSelectQuery(query, sqlParemeters);
        }     

        #region Age list
        public DataTable GetAllForAgeList(DateTime dtReference, int ageFrom, int ageTo, List<VolAvailabilityPreselectionModel> listFunction, List<int> label)
        {
            #region function
            string tableName = "";
            string conditionFunction = "";
            if (listFunction != null)
                if (listFunction.Count > 0)
                {
                    tableName = "VolFunctionCpr";
                    for (int i = 0; i < listFunction.Count; i++)
                    {
                        conditionFunction += " AND cp.idContPers IN (SELECT DISTINCT idcpr FROM " + tableName + " WHERE idQuest = " + listFunction[i].idQuest + ") ";
                    }
                }
            #endregion

            #region label
            string condition = "";
            if (label != null)
                if (label.Count > 0)
                {
                    int count = 0;

                    foreach (var idLabel in label)
                    {
                        if (idLabel.ToString() != "0")
                            if (count == label.Count - 1)
                            {
                                condition = " AND (" + condition + " cpl.idLabel = '" + idLabel.ToString() + "')";
                            }
                            else
                            {
                                condition += " cpl.idLabel = '" + idLabel.ToString() + "' OR ";
                            }

                        count++;
                    }
                }
            #endregion
            string query = string.Format(@"SELECT DISTINCT cp.idContPers, t.nameTitle, cp.firstname, cp.midname, cp.lastname,'' as functions, CASE WHEN ((MONTH(cp.birthdate) * 100) + DAY(cp.birthdate)) > ((MONTH(@dtReference) * 100) + DAY(@dtReference))
                                           THEN DATEDIFF(year,cp.birthdate,@dtReference) - 1
                                           ELSE DATEDIFF(year,cp.birthdate,@dtReference)  End as age, '' as email, '' as telephoner
                                           FROM ContactPerson as cp
                                           LEFT JOIN Title as t on t.idTitle = cp.idTitle
                                           LEFT JOIN ContactPersonLabel cpl ON cpl.idContPers = cp.idContPers
										   where CASE WHEN ((MONTH(cp.birthdate) * 100) + DAY(cp.birthdate)) > ((MONTH(@dtReference) * 100) + DAY(@dtReference))
                                           THEN DATEDIFF(year,cp.birthdate,@dtReference) - 1
                                           ELSE DATEDIFF(year,cp.birthdate,@dtReference) end<=" + ageTo + @" and CASE WHEN ((MONTH(cp.birthdate) * 100) + DAY(cp.birthdate)) > ((MONTH(@dtReference) * 100) + DAY(@dtReference))
                                           THEN DATEDIFF(year,cp.birthdate,@dtReference) - 1
                                           ELSE DATEDIFF(year,cp.birthdate,@dtReference) end >= " + ageFrom + " " + conditionFunction + condition + @"
                                            AND cp.idContPers IN (SELECT DISTINCT idContPers FROM ContactPersonFilter where idFilter = '4') and cp.IsActive='0'
            
                                            ORDER BY cp.lastname");

            SqlParameter[] sqlParemeters = new SqlParameter[1];

            sqlParemeters[0] = new SqlParameter("@dtReference", SqlDbType.DateTime);
            sqlParemeters[0].Value = dtReference;
            return conn.executeSelectQuery(query, sqlParemeters);
        }
        #endregion

        #region All bookings
        public DataTable GetAllForAllBookings(DateTime dtFrom, DateTime dtTo, List<VolAvailabilityPreselectionModel> listFunction, List<int> label)
        {
            string condition = "";
            #region label

            if (label != null)
                if (label.Count > 0)
                {
                    int count = 0;

                    foreach (var idLabel in label)
                    {
                        if (idLabel.ToString() != "0")
                            if (count == label.Count - 1)
                            {
                                condition = " AND (" + condition + " cpl.idLabel = '" + idLabel.ToString() + "')";
                            }
                            else
                            {
                                condition += " cpl.idLabel = '" + idLabel.ToString() + "' OR ";
                            }

                        count++;
                    }
                }
            #endregion
            #region function
            string tableName = "";
            string conditionFunction = "";
            if (listFunction != null)
                if (listFunction.Count > 0)
                {
                    for (int i = 0; i < listFunction.Count; i++)
                    {
                        conditionFunction += " AND id = " + listFunction[i].idQuest + " AND type = 'F' ";
                    }
                }
            #endregion
            string query = string.Format(@"Select DISTINCT vl.idContPers, t.nameTitle , a.idArrangement, cp.firstname, cp.midname, cp.lastname,'' as txtQuest,a.nameArrangement, a.dtFromArrangement, a.dtToArrangement, CASE WHEN ((MONTH(cp.birthdate) *  100) + DAY(cp.birthdate)) > ((MONTH(a.dtFromArrangement) * 100) + DAY(a.dtFromArrangement))
                                           THEN DATEDIFF(year,cp.birthdate,a.dtFromArrangement) - 1
                                           ELSE DATEDIFF(year,cp.birthdate,a.dtFromArrangement)  End as age, '' as telephoner, '' as email
                                            from ArrangementBook ab 
                                           left join VolLookup vl ON ab.idArrangement = vl.idArrangement and ab.idContPers = vl.idContPers
                                           left join Arrangement a on a.idArrangement = vl.idArrangement
                                           left join ContactPerson cp on cp.idContPers = vl.idContPers 
                                           left join ContactPersonFilter cpf on cp.idContPers= cpf.idContPers
                                           left join Title t on t.idTitle = cp.idTitle
                                           left join ContactPersonLabel cpl on cp.idContPers = cpl.idContPers
                                           left join Country c on c.idCountry = a.countryArrangement
                                           where cp.isActive='0' and cpf.idFilter = '4' and a.dtFromArrangement>=@dtFrom and a.dtToArrangement<=@dtTo" + condition + conditionFunction + @"
                                           AND (ab.idStatus = '1' OR ab.idStatus = '2')
                                           ORDER BY a.dtFromArrangement");

            SqlParameter[] sqlParemeters = new SqlParameter[2];

            sqlParemeters[0] = new SqlParameter("@dtFrom", SqlDbType.DateTime);
            sqlParemeters[0].Value = dtFrom;
            sqlParemeters[1] = new SqlParameter("@dtTo", SqlDbType.DateTime);
            sqlParemeters[1].Value = dtTo;
            return conn.executeSelectQuery(query, sqlParemeters);
        }

        #endregion
    }
}